//================================================================================
//
// MIT License
//
// Copyright(c) 2021 Cameron Mease
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
//================================================================================
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pinknose.CertificateUtilities.Test
{
    [TestClass]
    public class DisntinguisedNameBuilderTest
    {
        #region Fields

        private static readonly string title = "Mr.";
        private static readonly string cn = "John Doe";
        private static readonly string organization = "Bizco";
        private static readonly string email1 = "test@test.com";
        private static readonly string email2 = "test2@test.com";
        private static readonly string dc1 = "com";
        private static readonly string dc2 = "test";
        private static readonly string ou = "users";
        private static readonly string street = "123 Main St.";
        private static readonly string locality = "Anytown";
        private static readonly string state = "FL";
        private static readonly string country = "USA";
        private static readonly string expectedDN = $"DC={dc1}, DC={dc2}, OU={ou}, CN={cn}, T={title}, E={email1}, E={email2}, O={organization}, STREET={street}, L={locality}, S={state}, C={country}";

        #endregion Fields

        #region Methods

        [TestMethod]
        public void BuildTest()
        {
            var dnBuilder = new DistinguishedNameBuilder(cn)
                .AddOrganization(organization)
                .AddEmailAddress(email1)
                .AddEmailAddress(email2)
                .AddDomainComponent(dc1)
                    .AddDomainComponent(dc2)
                        .AddOrganizationalUnit(ou)
                            .EndOrganizationChain()
                .AddStreet(street)
                .AddState(state)
                .AddLocality(locality)
                .AddCountry(country)
                .AddTitle(title);

            //TODO: Finish this test

            var dn = dnBuilder.Build();

            Assert.AreEqual(dn.Name, expectedDN);
        }

        #endregion Methods
    }
}