using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class KiwiRepository : IKiwiRepository
    {
        public readonly DataContext _context;
        public KiwiRepository(DataContext context)
        {
            this._context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Like> GetLike(int userId, int recipientId)
        {
            return await _context.Likes.FirstOrDefaultAsync(l => l.LikerId == userId && l.LikeeId == recipientId);
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId)
            .FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }

        public async Task<User> GetUser(int Id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == Id);
            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            //var users = await _context.Users.Include(p=>p.Photos).ToListAsync();  //immediate execution
            var users = _context.Users.OrderByDescending(u => u.LastActive).AsQueryable();

            users = users.Where(u => u.Id != userParams.UserId);
            users = users.Where(u => u.Gender == userParams.Gender);

            if (userParams.Likers)
            {
                var userLikers = await GetUserLikes(userParams.UserId, userParams.Likers);
                users = users.Where(u => userLikers.Contains(u.Id));
            }

            if (userParams.Likees)
            {
                var userLikees = await GetUserLikes(userParams.UserId, userParams.Likers);
                users = users.Where(u => userLikees.Contains(u.Id));
            }

            if (userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }


        private async Task<IEnumerable<int>> GetUserLikes(int id, bool likers)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (likers)
            {
                return user.Likers.Where(u => u.LikeeId == id).Select(i => i.LikerId);
            }
            else
            {
                return user.Likees.Where(u => u.LikerId == id).Select(i => i.LikeeId);
            }
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }


        // Message Section 
        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        // Get InBox ,OutBox Unread Message by Current Login User
        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = _context.Messages.AsQueryable();

            switch (messageParams.MessageContainer)
            {
                case "Inbox":
                    messages = messages.Where(x => x.RecipientId == messageParams.UserId && !x.RecipientDeleted);
                    break;

                case "Outbox":
                    messages = messages.Where(x => x.SenderId == messageParams.UserId && !x.SenderDeleted);
                    break;

                default:
                    messages = messages.Where(x => x.RecipientId == messageParams.UserId && x.RecipientDeleted && !x.IsRead);
                    break;
            }
            messages = messages.OrderByDescending(x => x.MessageSent);
            return await PagedList<Message>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
        {
            var messages = await _context.Messages
            .Where(m => m.RecipientId == userId && !m.RecipientDeleted && m.SenderId == recipientId
                            || m.RecipientId == recipientId && m.SenderId == userId && !m.SenderDeleted)
                            .OrderByDescending(x => x.MessageSent)
                            .ToListAsync();

            return messages;
        }
    }
}