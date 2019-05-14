using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    /// <summary>
    /// <para>The OpportunityRepository interface.</para>
    /// <para>It contains methods which define all the HTTP request.</para>
    /// </summary>
    public interface IOpportunityRepository: IRepositoryBase<Opportunity>
    {
        /// <summary>Get all opportunitys objects.</summary>
        /// <returns>IEnumerable of tye opportunity</returns>
        IEnumerable<Opportunity> GetAllOpportunities();                            //this function will return all the opportunitys in db.

        /// <summary>Get opportunitys object by ID.</summary>
        /// <param name="o_id">id of the opportunity object</param>
        /// <returns>IEnumerable of tye opportunity</returns>
        Opportunity GetOpportunityById(int o_id);                            //this function will return all the opportunitys in db.

        /// <summary>Creates the opportunity.</summary>
        /// <param name="opportunity">The opportunity object.</param>
        void CreateOpportunity(Opportunity opportunity);                                //this function will create a new opportunity entry in db.


        /// <summary>Updates the opportunity.</summary>
        /// <param name="dbOpportunity">The database opportunity.</param>
        /// <param name="opportunity">The opportunity object.</param>
        void UpdateOpportunity(Opportunity dbOpportunity, Opportunity opportunity);                 //this function lets you update details of opportunity and the admin.


        /// <summary>Deletes the opportunity.</summary>
        /// <param name="opportunity">The opportunity object.</param>
        void DeleteOpportunity(Opportunity opportunity);                                //this lets you delete a particular opportunity entry from database.

    }
}
