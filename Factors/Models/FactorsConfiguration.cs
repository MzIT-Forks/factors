﻿using Factors.Models.Interfaces;

namespace Factors.Models
{
    public class FactorsConfiguration : IFactorConfiguration
    {
        /// <summary>
        /// The database to use for storage of user factors data
        /// </summary>
        public IFactorsDatabase StorageDatabase { get; set; }

        /// <summary>
        /// The encryption to use for securing user information
        /// </summary>
        public IFactorsEncryption EncryptionProvider { get; set; }

        /// <summary>
        /// The token generator to use for sending out two-factor tokens
        /// </summary>
        public ITokenProvider TokenProvider { get; set; }
    }
}
