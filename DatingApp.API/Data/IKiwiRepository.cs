using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Helpers;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IKiwiRepository
    {
         void Add<T>(T entity) where T:class;

         void Delete<T>(T entity) where T:class;
         Task<bool> SaveAll();

        // Task<IEnumerable<User>> GetUsers();
         Task<PagedList<User>> GetUsers(UserParams userParams);
         Task<User> GetUser(int Id);

            // Photos Section
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetMainPhotoForUser(int userId);

         // Likes Section
         Task<Like> GetLike(int userId,int recipientId);

         // Message Section
         Task<Message> GetMessage(int messageId);
         Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams); // Inbox ,Outbox etc
         Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
         
    }
}