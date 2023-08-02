using JsonWT.Models;
using Microsoft.EntityFrameworkCore;

namespace JsonWT.Context
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context) {
            _context = context;
        }

        public User Create(User user) {

            ExecuteRawSqlCommand("SET IDENTITY_INSERT Users ON");
            _context.Users.Add(user);
            _context.SaveChanges();
            ExecuteRawSqlCommand("SET IDENTITY_INSERT Users OFF");

            return user;
        }
        public void ExecuteRawSqlCommand(string sql)
        {
            _context.Database.ExecuteSqlRaw(sql);
        }

        public User getByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);

        }

        public User getById(int Id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == Id);
        }
    }
}
