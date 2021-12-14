using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinknose.CertificateUtilities
{
    public class OrganizationalUnitBuilder : OrganizationalUnitBuilderBase
    {
        #region Constructors

        public OrganizationalUnitBuilder(DistinguishedNameBuilder dnBuilder, string name) : base(dnBuilder, name, "OU")
        {
        }

        #endregion Constructors
    }

    public abstract class OrganizationalUnitBuilderBase
    {
        #region Fields

        private string name;
        private string attributeDesignator;
        private OrganizationalUnitBuilderBase? childBuilder;

        #endregion Fields

        #region Constructors

        internal OrganizationalUnitBuilderBase(DistinguishedNameBuilder dnBuilder, string name, string attributeDesignator)
        {
            DnBuilder = dnBuilder;
            this.name = name;
            this.attributeDesignator = attributeDesignator;
        }

        #endregion Constructors

        #region Properties

        internal DistinguishedNameBuilder DnBuilder { get; private set; }

        internal OrganizationalUnitBuilderBase ChildBuilder
        {
            get => childBuilder;
            set
            {
                if (childBuilder != null)
                {
                    throw new UnsupportedOrganizationalChainingException("An organizational object  cannot be added because an organizational object has already been added.");
                }
                childBuilder = value;
            }
        }

        #endregion Properties

        #region Methods

        public OrganizationalUnitBuilder AddOrganizationalUnit(string name)
        {
            ChildBuilder = new OrganizationalUnitBuilder(DnBuilder, name);
            return (OrganizationalUnitBuilder)ChildBuilder;
        }

        public DistinguishedNameBuilder EndOrganizationChain() => DnBuilder;

        public string Build()
        {
            string formattedString = $"{attributeDesignator}={name}";

            if (ChildBuilder != null)
            {
                formattedString += ", " + ChildBuilder.Build();
            }

            return formattedString;
        }

        #endregion Methods
    }
}