using System;

namespace DatingApp.API.Dtos
{
    public class AddMessageDTO
    {
        public int SenderId { get; set; }
        public int RecipientId{ get; set; } 
        public string Content { get; set;}
        public DateTime MessageSent {get;set;}
        public AddMessageDTO()
        {
            MessageSent = DateTime.Now;
        }
    }
}