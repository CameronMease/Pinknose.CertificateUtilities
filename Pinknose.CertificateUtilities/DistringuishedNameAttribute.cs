using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinknose.CertificateUtilities
{
    internal static class ListExtension
    {
        #region Methods

        public static void Add(this List<string> list, DistinguishedNameAttribute attribute)
        {
            string tempString = attribute.Build();

            if (tempString != null)
            {
                list.Add(tempString);
            }
        }

        #endregion Methods
    }

    internal abstract class DistinguishedNameAttribute
    {
        #region Methods

        public abstract string? Build();

        #endregion Methods
    }

    internal class DistinguishedNameAttribute<T> : DistinguishedNameAttribute
    {
        #region Fields

        private string attributeDesignator;

        #endregion Fields

        #region Constructors

        internal DistinguishedNameAttribute(string attributeDesignator)
        {
            this.attributeDesignator = attributeDesignator;
        }

        internal DistinguishedNameAttribute(string attributeDesignator, T value)
        {
            this.attributeDesignator = attributeDesignator;
            Value = value;
        }

        #endregion Constructors

        #region Properties

        public T? Value { set; private get; }

        #endregion Properties

        #region Methods

        public override string? Build()
        {
            if (Value != null)
            {
                return $"{attributeDesignator}={Value.ToString()}";
            }
            else
            {
                return null;
            }
        }

        #endregion Methods
    }
}