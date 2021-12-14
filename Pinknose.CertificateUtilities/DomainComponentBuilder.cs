using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinknose.CertificateUtilities
{
    public class DomainComponentBuilder : OrganizationalUnitBuilderBase
    {
        #region Constructors

        internal DomainComponentBuilder(DistinguishedNameBuilder dnBuilder, string name) : base(dnBuilder, name, "DC")
        {
        }

        #endregion Constructors

        #region Methods

        public DomainComponentBuilder AddDomainComponent(string name)
        {
            ChildBuilder = new DomainComponentBuilder(DnBuilder, name);
            return (DomainComponentBuilder)ChildBuilder;
        }

        #endregion Methods
    }
}