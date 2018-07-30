﻿using Factors.Models.UserAccount;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Factors.Models.Interfaces
{
    public interface IFactorsDatabaseProvider
    {
        /// <summary>
        /// Creates the initial database schema if the
        /// database does not currently exist
        /// </summary>
        void InitializeDatabaseSchema();

        /// <summary>
        /// Releases created resources
        /// </summary>
        void Dispose();

        /// <summary>
        /// Verifies that the token passed for the specified user is correct,
        /// and also updates the associated credential in the database as verified
        /// (assuming the token is correct)
        /// </summary>
        /// <param name="userAccountId"></param>
        /// <param name="credentialType"></param>
        /// <param name="tokenValue"></param>
        /// <returns></returns>
        FactorsCredentialCreationVerificationResult VerifyToken(string userAccountId, IFactorsFeatureType featureType, string tokenValue);

        /// <summary>
        /// Verifies that the token passed for the specified user is correct,
        /// and also updates the associated credential in the database as verified
        /// (assuming the token is correct)
        /// </summary>
        /// <param name="userAccountId"></param>
        /// <param name="credentialType"></param>
        /// <param name="tokenValue"></param>
        /// <returns></returns>
        Task<FactorsCredentialCreationVerificationResult> VerifyTokenAsync(string userAccountId, IFactorsFeatureType featureType, string tokenValue);

        /// <summary>
        /// Stores a two-factor token in the database. Tokens are used
        /// for verifying new and existing accounts
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        FactorsGeneratedToken StoreToken(FactorsGeneratedToken model);

        /// <summary>
        /// Stores a two-factor token in the database. Tokens are used
        /// for verifying new and existing accounts
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<FactorsGeneratedToken> StoreTokenAsync(FactorsGeneratedToken model);

        /// <summary>
        /// Creates a new credential for a user in the database. Will be created as a
        /// "pending verification" credential.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        FactorsCredential CreateCredential(FactorsCredential model);

        /// <summary>
        /// Creates a new credential for a user in the database. Will be created as a
        /// "pending verification" credential.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<FactorsCredential> CreateCredentialAsync(FactorsCredential model);

        /// <summary>
        /// Lists all factor credentials for the specified User Account ID
        /// </summary>
        /// <param name="userAccountId"></param>
        /// <param name="excludePendingVerification">If <c>false</c>, will include any credential pending approval</param>
        /// <returns></returns>
        IEnumerable<FactorsCredential> ListCredentialsFor(string userAccountId, IFactorsFeatureType featureType, FactorsCredentialListQueryType accountsToInclude);

        /// <summary>
        /// Lists all factor credentials for the specified User Account ID
        /// </summary>
        /// <param name="userAccountId"></param>
        /// <param name="excludePendingVerification">If <c>false</c>, will include any credential pending approval</param>
        /// <returns></returns>
        Task<IEnumerable<FactorsCredential>> ListCredentialsForAsync(string userAccountId, IFactorsFeatureType featureType, FactorsCredentialListQueryType accountsToInclude);
    }
}