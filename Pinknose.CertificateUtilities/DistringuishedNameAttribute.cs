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