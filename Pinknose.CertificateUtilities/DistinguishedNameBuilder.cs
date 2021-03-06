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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Pinknose.CertificateUtilities
{
    public class DistinguishedNameBuilder
    {
        #region Fields

        private readonly DistinguishedNameAttribute<string> commonName = new DistinguishedNameAttribute<string>("CN");
        private readonly DistinguishedNameAttribute<string> userId = new("UID");
        private readonly DistinguishedNameAttribute<string> title = new("T");
        private readonly DistinguishedNameAttribute<string> organization = new("O");
        private readonly DistinguishedNameAttribute<string> street = new("STREET");
        private readonly DistinguishedNameAttribute<string> locality = new("L");
        private readonly DistinguishedNameAttribute<string> state = new("S");
        private readonly DistinguishedNameAttribute<string> postalCode = new("PC");
        private readonly DistinguishedNameAttribute<string> country = new("C");
        private List<DistinguishedNameAttribute<string>> emailAddresses = new();
        private OrganizationalUnitBuilderBase? childOuBuilder;

        #endregion Fields

        #region Constructors

        public DistinguishedNameBuilder(string commonName)
        {
            this.commonName.Value = commonName;
        }

        #endregion Constructors

        #region Methods

        public DistinguishedNameBuilder AddOrganization(string organization)
        {
            this.organization.Value = organization;
            return this;
        }

        public DistinguishedNameBuilder AddStreet(string street)
        {
            this.street.Value = street;
            return this;
        }

        public DistinguishedNameBuilder AddLocality(string locality)
        {
            this.locality.Value = locality;
            return this;
        }

        public DistinguishedNameBuilder AddState(string state)
        {
            this.state.Value = state;
            return this;
        }

        /*
        public DistinguishedNameBuilder AddPostalCode(string postalCode)
        {
            this.postalCode.Value = postalCode;
            return this;
        }
        */

        public DistinguishedNameBuilder AddCountry(string country)
        {
            this.country.Value = country;
            return this;
        }

        public X500DistinguishedName Build()
        {
            List<string> parts = new();

            if (childOuBuilder != null)
            {
                parts.Add(childOuBuilder.Build());
            }

            parts.Add(commonName);
            parts.Add(userId);
            parts.Add(title);

            foreach (var emailAddress in emailAddresses)
            {
                parts.Add(emailAddress);
            }

            parts.Add(organization);
            parts.Add(street);
            parts.Add(locality);
            parts.Add(state);
            parts.Add(postalCode);
            parts.Add(country);

            string temp = string.Join(", ", parts);
            return new X500DistinguishedName(temp);
        }

        public DistinguishedNameBuilder AddEmailAddress(string emailAddress)
        {
            emailAddresses.Add(new DistinguishedNameAttribute<string>("E", emailAddress));
            return this;
        }

        /*
        public DistinguishedNameBuilder AddUserId(string id)
        {
            userId.Value = id;
            return this;
        }
        */

        public DistinguishedNameBuilder AddTitle(string title)
        {
            this.title.Value = title;
            return this;
        }

        public DomainComponentBuilder AddDomainComponent(string name)
        {
            if (childOuBuilder != null)
            {
                throw new UnsupportedOrganizationalChainingException("An organizational object  cannot be added because an organizational object has already been added..");
            }

            childOuBuilder = new DomainComponentBuilder(this, name);
            return (DomainComponentBuilder)childOuBuilder;
        }

        public OrganizationalUnitBuilder AddOrganizationalUnit(string name)
        {
            if (childOuBuilder != null)
            {
                throw new UnsupportedOrganizationalChainingException("An organizational object  cannot be added because an organizational object has already been added..");
            }

            childOuBuilder = new OrganizationalUnitBuilder(this, name);
            return (OrganizationalUnitBuilder)childOuBuilder;
        }

        #endregion Methods
    }
}