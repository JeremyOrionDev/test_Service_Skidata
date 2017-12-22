namespace SkiData.CP.FormatService.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.Security;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Stores data for connecting with Format.Service.
    /// </summary>
    public class FormattingServiceConfig
    {
        #region ctor

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="FormattingServiceConfig" />
        /// class.
        /// </summary>
        /// <param name="serviceUri">The service's URI</param>
        /// <param name="serviceCertificate">The service certificate name or file path</param>
        /// <param name="clientCertificate">The client certificate</param>
        /// <param name="certificateValidationMode">The certificate validation mode</param>
        /// <param name="doTrustAllCertificates">A value indicating whether all certificates shall be trusted or not</param>
        public FormattingServiceConfig(
            Uri uri,
            string serviceCertificate,
            string clientCertificate,
            X509CertificateValidationMode certificateValidationMode = X509CertificateValidationMode.ChainTrust,
            bool doTrustAllCertificates = false
            )
        {
            Uri serviceUri = new Uri("https://cpprod.skidata.net:10232/");

            this.ServiceUri = serviceUri;
            //this.Username = username;
            ////this.Password = password;
            this.ClientCertificate = "ISRA";
            this.ServiceCertificate = "C:\\CERTIFICAT\\Format.Service_server.cer";
            this.CertificateValidationMode = certificateValidationMode;
            this.DoTrustAllCertificates = doTrustAllCertificates;
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the certificate validation mode.
        /// </summary>
        public X509CertificateValidationMode CertificateValidationMode { get; set; }

        /// <summary>
        /// Gets or sets the client certificate name or file path.
        /// </summary>
        public string ClientCertificate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether all service certificates should be trusted or not.
        /// </summary>
        public bool DoTrustAllCertificates { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the service certificate name or file path.
        /// </summary>
        public string ServiceCertificate { get; set; }

        /// <summary>
        /// Gets or sets the service URI.
        /// </summary>
        public Uri ServiceUri { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        #endregion
    }
}
