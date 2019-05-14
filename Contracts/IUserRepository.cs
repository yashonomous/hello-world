using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    /// <summary>
    /// <para>The GroupRepository interface.</para>
    /// <para>It contains methods which define all the HTTP request.</para>
    /// </summary>
    public interface IUserRepository : IRepositoryBase<User>              //interface for Group Repository(having all actions on repository)
    {
        /// <summary>Get all groups objects.</summary>
        /// <returns>IEnumerable of tye Group</returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>Get all users by id.</summary>
        /// <returns>IEnumerable of tye User</returns>
        User GetUserById(int userId);

        /// <summary>Get all users by name.</summary>
        /// <returns>IEnumerable of tye User</returns>
        User GetUserByName(string userName);

        /// <summary>Check if the user exists.</summary>
        /// <param name="userName">Associate Id.</param>
        /// /// <param name="password">Associate Id.</param>
        /// <returns>boolean value</returns>
        bool checkUserExists(string userName, string password);
    }
}
