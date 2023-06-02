using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ogone;

namespace Ogone.Tests
{
    [TestClass]
    public class RequestTests
    {
        private const string SHA_IN_SIGNATURE = "Mysecretsig1875!?";
        private const string PSP = "CompanyName";
        private const string ORDER_ID = "123";
        private const decimal PRICE = 15.72M;
        private const string CUSTOMER_NAME = "鉄拳";

        [TestMethod]
        public void SHA1_ISO_NormalText_Signature_Successful()
        {
            // Arrange
            var Request = new Request(SHA.SHA1, SHA_IN_SIGNATURE, PSP, ORDER_ID, PRICE, Encoding.ISO_8859_1, Environment.Test);

            // Act
            var CalculatedSignature = Request.SHAOrder;

            // Assert
            Assert.AreEqual(CalculatedSignature, "229657B3494AEDEEF73152F08C0FBB7D88F3FD63");
        }

        [TestMethod]
        public void SHA256_ISO_NormalText_Signature_Successful()
        {
            // Arrange
            var Request = new Request(SHA.SHA256, SHA_IN_SIGNATURE, PSP, ORDER_ID, PRICE, Encoding.ISO_8859_1, Environment.Test);

            // Act
            var CalculatedSignature = Request.SHAOrder;

            // Assert
            Assert.AreEqual(CalculatedSignature, "29540221C711EA37472779CDCF4799FD30F9748B437D5885CD49E6D262ED3A55");
        }

        [TestMethod]
        public void SHA512_ISO_NormalText_Signature_Successful()
        {
            // Arrange
            var Request = new Request(SHA.SHA512, SHA_IN_SIGNATURE, PSP, ORDER_ID, PRICE, Encoding.ISO_8859_1, Environment.Test);

            // Act
            var CalculatedSignature = Request.SHAOrder;

            // Assert
            Assert.AreEqual(CalculatedSignature, "3148A0934C45F0541AB6D2365746B4D8629CB592FA4F76DE8CB9E58BFBB9ACC07F9D688DE878B44F60AAEADA28B8AF3834CFDD3E69808458638F85E40982BDA2");
        }

        [TestMethod]
        public void SHA1_UTF8_NormalText_Signature_Successful()
        {
            // Arrange
            var Request = new Request(SHA.SHA1, SHA_IN_SIGNATURE, PSP, ORDER_ID, PRICE, Encoding.UTF8, Environment.Test);

            // Act
            var CalculatedSignature = Request.SHAOrder;

            // Assert
            Assert.AreEqual(CalculatedSignature, "229657B3494AEDEEF73152F08C0FBB7D88F3FD63");
        }

        [TestMethod]
        public void SHA256_UTF8_NormalText_Signature_Successful()
        {
            // Arrange
            var Request = new Request(SHA.SHA256, SHA_IN_SIGNATURE, PSP, ORDER_ID, PRICE, Encoding.UTF8, Environment.Test);

            // Act
            var CalculatedSignature = Request.SHAOrder;

            // Assert
            Assert.AreEqual(CalculatedSignature, "29540221C711EA37472779CDCF4799FD30F9748B437D5885CD49E6D262ED3A55");
        }

        [TestMethod]
        public void SHA512_UTF8_NormalText_Signature_Successful()
        {
            // Arrange
            var Request = new Request(SHA.SHA512, SHA_IN_SIGNATURE, PSP, ORDER_ID, PRICE, Encoding.UTF8, Environment.Test);

            // Act
            var CalculatedSignature = Request.SHAOrder;

            // Assert
            Assert.AreEqual(CalculatedSignature, "3148A0934C45F0541AB6D2365746B4D8629CB592FA4F76DE8CB9E58BFBB9ACC07F9D688DE878B44F60AAEADA28B8AF3834CFDD3E69808458638F85E40982BDA2");
        }

        [TestMethod]
        public void SHA1_UTF8_SpecialText_Signature_Successful()
        {
            // Arrange
            var Request = new Request(SHA.SHA1, SHA_IN_SIGNATURE, PSP, ORDER_ID, PRICE, Encoding.UTF8, Environment.Test);
            Request.CustomerName = CUSTOMER_NAME;

            // Act
            var CalculatedSignature = Request.SHAOrder;

            // Assert
            Assert.AreEqual(CalculatedSignature, "44887035F471CE03E2B631295165262EA0DAAB59");
        }

        [TestMethod]
        public void SHA256_UTF8_SpecialText_Signature_Successful()
        {
            // Arrange
            var Request = new Request(SHA.SHA256, SHA_IN_SIGNATURE, PSP, ORDER_ID, PRICE, Encoding.UTF8, Environment.Test);
            Request.CustomerName = CUSTOMER_NAME;

            // Act
            var CalculatedSignature = Request.SHAOrder;

            // Assert
            Assert.AreEqual(CalculatedSignature, "8EA84BC89E18531305AF1B73F35910EEBCB7B82386411FD8C66C505DE7B5CE93");
        }

        [TestMethod]
        public void SHA512_UTF8_SpecialText_Signature_Successful()
        {
            // Arrange
            var Request = new Request(SHA.SHA512, SHA_IN_SIGNATURE, PSP, ORDER_ID, PRICE, Encoding.UTF8, Environment.Test);
            Request.CustomerName = CUSTOMER_NAME;

            // Act
            var CalculatedSignature = Request.SHAOrder;

            // Assert
            Assert.AreEqual(CalculatedSignature, "E110E061B72EF9D50A1C82B72B0F471DA79BF0553F8BC76A3DE64D52C7A3BAB01796767A7E3EE45C935ED8F820649EFB333DE7C2ECB621AA4D9B47BD7031F326");
        }
    }
}
