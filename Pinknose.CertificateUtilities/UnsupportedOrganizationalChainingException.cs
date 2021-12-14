using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinknose.CertificateUtilities
{
    public class UnsupportedOrganizationalChainingException : Exception
    {
        #region Constructors

        public UnsupportedOrganizationalChainingException(string? message) : base(message)
        {
        }

        #endregion Constructors
    }
}