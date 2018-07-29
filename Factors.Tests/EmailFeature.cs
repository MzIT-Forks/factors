﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Factors.Feature.Email;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Factors.Tests
{
    [TestClass]
    public class EmailFeature
    {
        private readonly string _userAccount = Guid.NewGuid().ToString();
        private readonly string _userEmailAddress = "user@domain.tld";

        [TestInitialize]
        public void Initalize()
        {
            Factor.Initalize(new Models.FactorsConfiguration
            {
                StorageDatabase = new Database.InMemory.Provider(),
                EncryptionProvider = new Encryption.PlainText.Provider(),
                TokenProvider = new Token.Number.Provider()
            }).InitializeEmailFactor(new Feature.Email.Models.EmailConfiguration
            {
                FromAddress = "factors@domain.tld",
                FromName = "My Application",
                MailProvider = new Feature.Email.Smtp.Provider("localhost", 25, false),
                TokenExpirationTime = TimeSpan.FromMinutes(5)
            });
        }

        [TestCleanup()]
        public void Dispose()
        {
            Factor.Dispose();
        }

        [TestMethod]
        public void VerifyIsInitalized()
        {
            Assert.IsTrue(!String.IsNullOrWhiteSpace(Factor.ForUser(_userAccount).UserAccount));
        }

        [TestMethod]
        public void CreateEmailCredential()
        {
            var emailCredential = Factor.ForUser(_userAccount).CreateEmailCredential(_userEmailAddress);

            Assert.IsNotNull(emailCredential);
            Assert.IsTrue(emailCredential.IsSuccess);
            Assert.IsNotNull(emailCredential.TokenDetails);
        }

        [TestMethod]
        public void VerifyEmailToken()
        {
            var emailCredential = Factor.ForUser(_userAccount).CreateEmailCredential(_userEmailAddress);
            var verificationResult = Factor.ForUser(_userAccount).VerifyToken(EmailProvider.FeatureType, emailCredential.TokenDetails.VerificationToken);

            Assert.IsTrue(verificationResult.Success);
        }

        [TestMethod]
        public void VerifyEmailAccountIsValidated()
        {
            var emailCredential = Factor.ForUser(_userAccount).CreateEmailCredential(_userEmailAddress);
            var verificationResult = Factor.ForUser(_userAccount).VerifyToken(EmailProvider.FeatureType, emailCredential.TokenDetails.VerificationToken);

            var accounts = Factor.ForUser(_userAccount).ListVerifiedAccountsFor(EmailProvider.FeatureType);

            Assert.IsTrue(accounts.Count() > 0);
        }

        [TestMethod]
        public void VerifyEmailAccountIsNotValidated()
        {
            var emailCredential = Factor.ForUser(_userAccount).CreateEmailCredential(_userEmailAddress);
            var accounts = Factor.ForUser(_userAccount).ListUnverifiedAccountsFor(EmailProvider.FeatureType);

            Assert.IsTrue(accounts.Count() > 0);
        }

        [TestMethod]
        public void GetErrorWhenCreatingDuplicateCredentials()
        {
            var emailCredential = Factor.ForUser(_userAccount).CreateEmailCredential(_userEmailAddress);
            var emailCredentialTwo = Factor.ForUser(_userAccount).CreateEmailCredential(_userEmailAddress);

            Assert.IsFalse(emailCredentialTwo.IsSuccess);
        }
    }
}
