using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    /// <summary>Wrapper arount the Group repository class</summary>
    public interface IRepositoryWrapper         //wrapper interface for repository.
    {
        /// <summary>Gets the group object.</summary>
        /// <value>The GroupRepository object.</value>
        IUserRepository User { get; }

        /// <summary>Gets the opportunity object.</summary>
        /// <value>The OpportunityRepository object.</value>
        IOpportunityRepository Opportunity { get; }

        /// <summary>Gets the group object.</summary>
        /// <value>The GroupRepository object.</value>
        IUser_OpportunityRepository User_Opportunity { get; }

    }
}
