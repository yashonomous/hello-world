using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    /// <summary>
    /// <para>The IUser_OpportunityRepository interface.</para>
    /// <para>It contains methods which define all the HTTP request.</para>
    /// </summary>
    public interface IUser_OpportunityRepository
    {
        /// <summary>Gets all solutions.</summary>
        /// <returns>IEnumerable of type solution.</returns>
        IEnumerable<User_Opportunity> GetAllUserOpportunities();

        /// <summary>Gets all solutions.</summary>
        /// <returns>IEnumerable of type solution.</returns>
        User_Opportunity GetUserOpportunityById(int id);

        /// <summary>Creates the user and opportunity.</summary>
        /// <param name="userOpportunity">The userOpportunity object.</param>
        void CreateUserOpportunity(User_Opportunity userOpportunity);                                //this function will create a new group entry in db.

        /// <summary>Gets all opportunities.</summary>
        /// /// <param name="userId">The userOpportunity object.</param>
        /// <returns>user opportunity object.</returns>
        IEnumerable<User_Opportunity> GetAllOpportunitiesByUserId(int userId);

        /// <summary>Updates the opportunity.</summary>
        /// <param name="dbopportunity">The database opportunity.</param>
        /// <param name="opportunity">The opportunity object.</param>
        void UpdateUserOpportunity(User_Opportunity dbUserOpportunity, User_Opportunity userOpportunity);                 //this function lets you update details of opportunity and the admin.
    }
}
