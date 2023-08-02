using JsonWT.Models;

namespace JsonWT.Context
{
    public interface IUserRepository
    {
        User Create(User user);
        User getByEmail(string email);
        User getById(int Id);
    }
}
