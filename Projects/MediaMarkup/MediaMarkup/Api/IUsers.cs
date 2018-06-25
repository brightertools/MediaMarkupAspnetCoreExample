using System.Threading.Tasks;
using MediaMarkup.Api.Models;

namespace MediaMarkup.Api
{
    public interface IUsers
    {
        Task<UserInfo> GeByIdt(string id);

        Task<UserInfo> GetByEmail(string email);

        Task<UserCreateResult> Create(UserCreateParameters userCreateParameters);

        Task<UserUpdateResult> Update(UserUpdateParameters userUpdateParameters);

        Task<bool> Delete(string id);
    }
}