namespace SkiData.CP.FormatService.Client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Security.Cryptography.X509Certificates;
    using System.ServiceModel;
    using System.ServiceModel.Security;
    using System.Text;
    using System.Threading.Tasks;
    using SkiData.CP.FormatService.Contracts;

    /// <summary>
    /// Wraps call to the administration service and makes them perform
    /// asynchronously.
    /// </summary>
    public class FormattingServiceWrapper : IDisposable
    {
        #region private fields

        /// <summary>
        /// Holds the administration service proxy.
        /// </summary>
        private FormattingServiceClient formattingSvc = default(FormattingServiceClient);

        /// <summary>
        /// Holds the configuration for the service proxy.
        /// </summary>
        private FormattingServiceConfig config;

        #endregion

        #region private properties

        /// <summary>
        /// Gets a value indicating whether the service has been initialized
        /// or not.
        /// </summary>
        private bool IsInitialized
        {
            get
            {
                return this.formattingSvc != default(FormattingServiceClient);
            }
        }

        /// <summary>
        /// Gets the service proxy.
        /// </summary>
        private FormattingServiceClient Service
        {
            get
            {
                return this.formattingSvc;
            }
        }

        #endregion

        #region public methods

        /// <summary>
        /// Checks the state of the web service connection and re-initializes it,
        /// if necessary.
        /// </summary>
        public void CheckConnection()
        {
            if (this.formattingSvc == default(FormattingServiceClient))
            {
                return;
            }

            if (this.formattingSvc.State == CommunicationState.Faulted ||
                this.formattingSvc.State == CommunicationState.Closing ||
                this.formattingSvc.State == CommunicationState.Closed)
            {
                this.Initialize(this.config);
            }
        }

        /// <summary>
        /// Disposes of the instance.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Initializes the service wrapper.
        /// </summary>
        /// <param name="config">
        /// The service configuration.
        /// </param>
        public void Initialize(FormattingServiceConfig config)
        {
            this.config = config;

            var serviceCertificate = default(X509Certificate2);
            try
            {
                serviceCertificate = LoadServiceCertificate(config.ServiceCertificate);
            }
            catch (Exception ex)
            {
                throw new Exception("Loading the service certificate failed.", ex);
            }

            // endpoint address
            EndpointAddress formattingAddress =
                new EndpointAddress(
                    config.ServiceUri,
                    new X509CertificateEndpointIdentity(serviceCertificate));

            // binding
            WSHttpBinding binding = new WSHttpBinding(SecurityMode.TransportWithMessageCredential);
            binding.MaxReceivedMessageSize = 104857600; // 100 MB
            binding.MaxBufferPoolSize = 104857600; // 100 MB
            binding.ReaderQuotas.MaxStringContentLength = 104857600; // 100 MB
            binding.ReaderQuotas.MaxBytesPerRead = 104857600; // 100 MB
            binding.ReaderQuotas.MaxDepth = 32;
            binding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
            binding.Security.Message.EstablishSecurityContext = true;

            // trust all certificates
            if (config.DoTrustAllCertificates)
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback =
                    (sender, certificate, chain, sslPolicyErrors) =>
                    {
                        return true;
                    };
            }

            // service proxies
            this.formattingSvc = new FormattingServiceClient(binding, formattingAddress);

            // configure trust for service certificate
            this.formattingSvc.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode =
                config.CertificateValidationMode;

            // load & set client certificate
            try
            {
                if (string.IsNullOrWhiteSpace(config.ClientCertificate))
                {
                    throw new Exception("Client certificate info must not be empty.");
                }

                if (File.Exists(config.ClientCertificate))
                {
                    this.formattingSvc.ClientCredentials.ClientCertificate.Certificate.Import(config.ClientCertificate);
                }
                else
                {
                    this.formattingSvc.ClientCredentials.ClientCertificate.SetCertificate(
                        StoreLocation.CurrentUser,
                        StoreName.TrustedPeople,
                        X509FindType.FindBySubjectName,
                        config.ClientCertificate);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Loading the client certificate failed.", ex);
            }

            this.formattingSvc.GetVersion();
        }

        #endregion

        #region public service methods

        /// <summary>
        /// Retrieves the full account statements.
        /// </summary>
        /// <returns>The account statement as a <see cref="List{T}" /> of <see cref="FormatTypeAccoutnData" /> entries.</returns>
        public FormatTypeAccountData[] GetAccountStatement()
        {
            this.AssertInitialized();
            this.CheckConnection();

            return
                this.ExecuteWithBadContextTokenRetry(
                    () =>
                        {
                            return this.formattingSvc.GetAccountStatement();
                        });
        }

        /// <summary>
        /// Retrieves the full account statements asynchronously.
        /// </summary>
        /// <returns>The account statement as a <see cref="List{T}" /> of <see cref="FormatTypeAccoutnData" /> entries.</returns>
        public async Task<FormatTypeAccountData[]> GetAccountStatementAsync()
        {
            this.AssertInitialized();
            this.CheckConnection();

            return
                await this.ExecuteWithBadContextTokenRetryAsync(
                    () =>
                    {
                        return this.formattingSvc.GetAccountStatementAsync();
                    });
        }

        /// <summary>
        /// Gets the user's batches.
        /// </summary>
        /// <param name="openOnly">Determines whether the user's open batches shall be retrieved or not.</param>
        /// <returns>The list of open batch entries.</returns>
        public BatchInfoData[] GetAllBatches(bool openOnly)
        {
            this.AssertInitialized();
            this.CheckConnection();

            return
                this.ExecuteWithBadContextTokenRetry(
                    () =>
                    {
                        return this.formattingSvc.GetAllBatches(openOnly);
                    });
        }

        /// <summary>
        /// Gets the user's batches asynchronously.
        /// </summary>
        /// <param name="openOnly">Determines whether the user's open batches shall be retrieved or not.</param>
        /// <returns>The list of open batch entries.</returns>
        public async Task<BatchInfoData[]> GetAllBatchesAsync(bool openOnly)
        {
            this.AssertInitialized();
            this.CheckConnection();

            return
                await this.ExecuteWithBadContextTokenRetryAsync(
                    () =>
                    {
                        return this.formattingSvc.GetAllBatchesAsync(openOnly);
                    });
        }

        /// <summary>
        /// Gets the available format types for the calling user.
        /// </summary>
        /// <returns>
        /// The list of format types available to the calling user.
        /// </returns>
        public FormatTypeData[] GetAvailableFormatTypes()
        {
            this.AssertInitialized();
            this.CheckConnection();

            return
                this.ExecuteWithBadContextTokenRetry(
                    () =>
                    {
                        return this.formattingSvc.GetAvailableFormatTypes();
                    });
        }

        /// <summary>
        /// Gets the available format types for the calling user, asynchronously.
        /// </summary>
        /// <returns>
        /// The list of format types available to the calling user.
        /// </returns>
        public async Task<FormatTypeData[]> GetAvailableFormatTypesAsync()
        {
            this.AssertInitialized();
            this.CheckConnection();

            return
                await this.ExecuteWithBadContextTokenRetryAsync(
                    () =>
                    {
                        return this.formattingSvc.GetAvailableFormatTypesAsync();
                    });
        }

        /// <summary>
        /// Gets the final formatting data for a finished batch of UIDs.
        /// </summary>
        /// <param name="batchId">The ID of the batch whose data elements shall be retrieved.</param>
        /// <returns>The individual elements of the batch's formatted data entries.</returns>
        public FormattedData[] GetBatchData(Guid batchId)
        {
            this.AssertInitialized();
            this.CheckConnection();

            return
                this.ExecuteWithBadContextTokenRetry(
                    () =>
                    {
                        return this.formattingSvc.GetBatchData(batchId);
                    });
        }

        /// <summary>
        /// Gets the final formatting data for a finished batch of UIDs, asynchronously.
        /// </summary>
        /// <param name="batchId">The ID of the batch whose data elements shall be retrieved.</param>
        /// <returns>The individual elements of the batch's formatted data entries.</returns>
        public async Task<FormattedData[]> GetBatchDataAsync(Guid batchId)
        {
            this.AssertInitialized();
            this.CheckConnection();

            return
                await this.ExecuteWithBadContextTokenRetryAsync(
                    () =>
                    {
                        return this.formattingSvc.GetBatchDataAsync(batchId);
                    });
        }

        /// <summary>
        /// Gets the available contingents for the calling user.
        /// </summary>
        /// <returns>The available formatting contingents.</returns>
        public ContingentData[] GetContingents()
        {
            this.AssertInitialized();
            this.CheckConnection();

            return
                this.ExecuteWithBadContextTokenRetry(
                    () =>
                    {
                        return this.formattingSvc.GetContingents();
                    });
        }

        /// <summary>
        /// Gets the available contingents for the calling user, asynchronously.
        /// </summary>
        /// <returns>The available formatting contingents.</returns>
        public async Task<ContingentData[]> GetContingentsAsync()
        {
            this.AssertInitialized();
            this.CheckConnection();

            return
                await this.ExecuteWithBadContextTokenRetryAsync(
                    () =>
                    {
                        return this.formattingSvc.GetContingentsAsync();
                    });
        }

        /// <summary>
        /// Gets the available contingents for the calling user.
        /// </summary>
        /// <returns>The available formatting contingents.</returns>
        public string GetContingentsString()
        {

            string res;
            res = "";
            try
            {
                var contingents = this.GetContingents();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Available license contingents:");
                sb.AppendLine();


                if (contingents.Length == 0)
                {
                    sb.AppendLine("None.");
                }
                else
                {
                    foreach (var contingent in contingents)
                    {
                        sb.Append("Format ");
                        sb.Append(contingent.FormatTypeId);
                        sb.Append(": ");
                        sb.Append(contingent.Balance);
                        sb.AppendLine(" licenses");
                    }
                }

                res = sb.ToString();
            }
            catch (Exception ex)
            {
                res =
                    string.Format(
                        "Error:{1}{1}{0}",
                        ex.Message,
                        Environment.NewLine);
            }

            return res;
        }




        /// <summary>
        /// Calculates the chip memory data for a single UID.
        /// </summary>
        /// <param name="uid">The ISO/IEC 15693 UID for which the data shall be computed.</param>
        /// <param name="formatTypeId">The ID of the format type</param>
        /// <returns>The the format data for the given UID.</returns>
        public FormattedData GetSingleUidData(ulong uid, Guid formatTypeId)
        {
            this.AssertInitialized();
            this.CheckConnection();

            return
                this.ExecuteWithBadContextTokenRetry(
                    () =>
                    {
                        return this.formattingSvc.GetSingleUidData(uid, formatTypeId);
                    });
        }

        /// <summary>
        /// Calculates the chip memory data for a single UID, asynchronously.
        /// </summary>
        /// <param name="uid">The ISO/IEC 15693 UID for which the data shall be computed.</param>
        /// <param name="formatTypeId">The ID of the format type</param>
        /// <returns>The the format data for the given UID.</returns>
        public async Task<FormattedData> GetSingleUidDataAsync(ulong uid, Guid formatTypeId)
        {
            this.AssertInitialized();
            this.CheckConnection();

            return
                await this.ExecuteWithBadContextTokenRetryAsync(
                    () =>
                    {
                        return this.formattingSvc.GetSingleUidDataAsync(uid, formatTypeId);
                    });
        }


        public string GetSingleUidDataString(string UidText, string FormatType)
        {
            string res;
            res="";
             try
            {
                // try to parse UID
                ulong uid;
                if (!ulong.TryParse(UidText, out uid))
                {
                    res = "Invalid UID value supplied.";
                }

                // try to parse FormatTypeId
                Guid formatTypeId;
                if (!Guid.TryParse(FormatType, out formatTypeId))
                {
                    res = "Invalid UUID value supplied for format type ID.";
                }

                // synchronous call: var formattedData = this.formatting.Service.GetSingleUidData();
                var formattedData = this.GetSingleUidData(uid, formatTypeId);

                StringBuilder sb = new StringBuilder();
                sb.Append("Formatted data:");
                sb.Append("\t");

                sb.Append("UID (dec): ");
                sb.Append(formattedData.Uid.ToString());
                sb.Append("\t");

                if (formattedData.Dsf.HasValue)
                {
                    sb.Append("DSF (dec): ");
                    sb.Append(formattedData.Dsf.Value.ToString());
                }

                sb.Append("\t");

                foreach (var blockData in formattedData.Blocks)
                {
                    sb.Append("Block #");
                    sb.Append(blockData.BlockNumber);
                    sb.Append(": ");
                    sb.Append(blockData.BlockValue);
                    sb.Append("\t");
                }

                res = sb.ToString();
            }
            catch (Exception ex)
            {
                res =
                    string.Format(
                        "Error:{1}{1}{0}",
                        ex.Message,
                        Environment.NewLine);
            }

             return res;
        }


        /// <summary>
        /// Gets the service's version number.
        /// </summary>
        /// <returns>The service version.</returns>
        public string GetVersion()
        {
            this.AssertInitialized();
            this.CheckConnection();

            return
                this.ExecuteWithBadContextTokenRetry(
                    () =>
                    {
                        return this.formattingSvc.GetVersion();
                    });
        }

        /// <summary>
        /// Gets the service's version number asynchronously.
        /// </summary>
        /// <returns>The service version.</returns>
        public async Task<string> GetVersionAsync()
        {
            this.AssertInitialized();
            this.CheckConnection();

            return
                await this.ExecuteWithBadContextTokenRetryAsync(
                    () =>
                    {
                        return this.formattingSvc.GetVersionAsync();
                    });
        }

        /// <summary>
        /// Gets a value indicating whether the specified batch is finished or not.
        /// </summary>
        /// <param name="batchId">The ID of the batch whose status is to be checked.</param>
        /// <returns><c>true</c> if the batch is fully processed, <c>false</c> otherwise.</returns>
        public bool IsBatchFinished(Guid batchId)
        {
            this.AssertInitialized();
            this.CheckConnection();

            return
                this.ExecuteWithBadContextTokenRetry(
                    () =>
                    {
                        return this.formattingSvc.IsBatchFinished(batchId);
                    });
        }

        /// <summary>
        /// Gets a value indicating whether the specified batch is finished or not, asynchronously.
        /// </summary>
        /// <param name="batchId">The ID of the batch whose status is to be checked.</param>
        /// <returns><c>true</c> if the batch is fully processed, <c>false</c> otherwise.</returns>
        public async Task<bool> IsBatchFinishedAsync(Guid batchId)
        {
            this.AssertInitialized();
            this.CheckConnection();

            return
                await this.ExecuteWithBadContextTokenRetryAsync(
                    () =>
                    {
                        return this.formattingSvc.IsBatchFinishedAsync(batchId);
                    });
        }

        /// <summary>
        /// Places a batch of UIDs for chip memory calculation.
        /// </summary>
        /// <param name="uids">
        /// A list of UIDs for which the chip memory data shall be calculated.
        /// </param>
        /// <param name="formatTypeId">
        /// The ID of the format that should be applied.
        /// </param>
        /// <returns>The ID of the newly placed batch.</returns>
        public Guid PlaceUidBatch(ulong[] uids, Guid formatTypeId)
        {
            this.AssertInitialized();
            this.CheckConnection();

            return
                this.ExecuteWithBadContextTokenRetry(
                    () =>
                    {
                        return this.formattingSvc.PlaceUidBatch(uids, formatTypeId);
                    });
        }

        /// <summary>
        /// Places a batch of UIDs for chip memory calculation asynchronously.
        /// </summary>
        /// <param name="uids">
        /// A list of UIDs for which the chip memory data shall be calculated.
        /// </param>
        /// <param name="formatTypeId">
        /// The ID of the format that should be applied.
        /// </param>
        /// <returns>The ID of the newly placed batch.</returns>
        public async Task<Guid> PlaceUidBatchAsync(ulong[] uids, Guid formatTypeId)
        {
            this.AssertInitialized();
            this.CheckConnection();

            return
                await this.ExecuteWithBadContextTokenRetryAsync(
                    () =>
                    {
                        return this.formattingSvc.PlaceUidBatchAsync(uids, formatTypeId);
                    });
        }

        #endregion

        #region protected methods

        /// <summary>
        /// Disposes the instance.
        /// </summary>
        /// <param name="disposing">
        /// Indicates whether this method is called from <see cref="Dispose()"/>
        /// or not.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (this.formattingSvc != default(FormattingServiceClient))
                {
                    if (this.formattingSvc.State == CommunicationState.Created ||
                        this.formattingSvc.State == CommunicationState.Opened ||
                        this.formattingSvc.State == CommunicationState.Opening)
                    {
                        ((IDisposable)this.formattingSvc).Dispose();
                    }
                } 
            }

            // free native resources if there are any.
        }

        #endregion

        #region private static methods

        /// <summary>
        /// Loads an X.509 certificate.
        /// </summary>
        /// <param name="certificateInfo">
        /// Provides information on how to find the certificate.
        /// If it is a file name, the certificate will be loaded from the disk.
        /// Otherwise, it is assumed to be located in the <see cref="StoreName.TrustedPeople" /> store of the
        /// <see cref="StoreLocation.CurrentUser" />.
        /// </param>
        /// <returns>The loaded certificate</returns>
        private static X509Certificate2 LoadServiceCertificate(string certificateInfo)
        {
            if (string.IsNullOrWhiteSpace(certificateInfo))
            {
                throw new ArgumentException("Certificate info must not be empty.", certificateInfo);
            }

            var serviceCertificate = new X509Certificate2();
            if (File.Exists(certificateInfo))
            {
                serviceCertificate.Import(certificateInfo); // this throws of course if the import fails
            }
            else
            {
                // serviceCertificate = new X509Certificate2(
                X509Store store = new X509Store(StoreName.TrustedPeople, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                try
                {
                    X509Certificate2Collection cers =
                        store.Certificates.Find(X509FindType.FindBySubjectName, certificateInfo, true);

                    if (cers.Count > 0)
                    {
                        serviceCertificate = cers[0];
                    }

                    if (serviceCertificate == default(X509Certificate2))
                    {
                        throw new Exception("Certificate not found on disk and in CurrentUser/TrustedPeople certificate store.");
                    }
                }
                finally
                {
                    store.Close();
                }
            }

            return serviceCertificate;
        }

        #endregion

        #region private methods

        /// <summary>
        /// Checks whether the service has been properly initialized or not.
        /// If it hasn't been initialized, this method throws an
        /// <see cref="InvalidOperationException"/>
        /// </summary>
        private void AssertInitialized()
        {
            if (!this.IsInitialized)
            {
                throw new InvalidOperationException("The service has not been properly initialized.");
            }
        }

        /// <summary>
        /// Executes a method.
        /// If the method throws an exception, the exception is evaluated
        /// for the presence of a BadContextToken fault. If it is found,
        /// the method is retried once.
        /// </summary>
        /// <param name="method">The method to be executed.</param>
        private void ExecuteWithBadContextTokenRetry(Action method)
        {
            int retry = 1;
            while (true)
            {
                try
                {
                    method();
                    return;
                }
                catch (MessageSecurityException ex)
                {
                    if (ex.InnerException != null &&
                        ex.InnerException is FaultException)
                    {
                        var faultEx = ex.InnerException as FaultException;

                        if (faultEx.Code != null &&
                            faultEx.Code.SubCode != null &&
                            faultEx.Code.SubCode.Name == "BadContextToken")
                        {
                            --retry;
                            if (retry >= 0)
                            {
                                this.ReinitializeService();
                                continue;
                            }
                        }
                    }

                    throw;
                }
            }
        }

        /// <summary>
        /// Executes a method.
        /// If the method throws an exception, the exception is evaluated
        /// for the presence of a BadContextToken fault. If it is found,
        /// the method is retried once.
        /// </summary>
        /// <param name="method">The method to be executed.</param>
        /// <returns>The result of the method.</returns>
        /// <typeparam name="TResult">The result type of the called method</typeparam>
        private TResult ExecuteWithBadContextTokenRetry<TResult>(Func<TResult> method)
        {
            int retry = 1;
            while (true)
            {
                try
                {
                    return method();
                }
                catch (MessageSecurityException ex)
                {
                    if (ex.InnerException != null &&
                        ex.InnerException is FaultException)
                    {
                        var faultEx = ex.InnerException as FaultException;

                        if (faultEx.Code != null &&
                            faultEx.Code.SubCode != null &&
                            faultEx.Code.SubCode.Name == "BadContextToken")
                        {
                            --retry;
                            if (retry >= 0)
                            {
                                this.ReinitializeService();
                                continue;
                            }
                        }
                    }

                    throw;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Executes an asynchronous method.
        /// If the method throws an exception, the exception is evaluated
        /// for the presence of a BadContextToken fault. If it is found,
        /// the method is retried once.
        /// </summary>
        /// <param name="method">The method to be executed.</param>
        private void ExecuteWithBadContextTokenRetryAsync(Func<Task> method)
        {
            int retry = 1;
            while (true)
            {
                try
                {
                    method().Wait();
                    return;
                }
                catch (MessageSecurityException ex)
                {
                    if (ex.InnerException != null &&
                        ex.InnerException is FaultException)
                    {
                        var faultEx = ex.InnerException as FaultException;

                        if (faultEx.Code != null &&
                            faultEx.Code.SubCode != null &&
                            faultEx.Code.SubCode.Name == "BadContextToken")
                        {
                            --retry;
                            if (retry >= 0)
                            {
                                this.ReinitializeService();
                                continue;
                            }
                        }
                    }

                    throw;
                }
            }
        }

        /// <summary>
        /// Executes an asynchronous method.
        /// If the method throws an exception, the exception is evaluated
        /// for the presence of a BadContextToken fault. If it is found,
        /// the method is retried once.
        /// </summary>
        /// <param name="method">The method to be executed.</param>
        /// <returns>The result of the method.</returns>
        /// <typeparam name="TResult">The result type of the called method</typeparam>
        private async Task<TResult> ExecuteWithBadContextTokenRetryAsync<TResult>(Func<Task<TResult>> method)
        {
            int retry = 1;
            while (true)
            {
                try
                {
                    return await method();
                }
                catch (MessageSecurityException ex)
                {
                    if (ex.InnerException != null &&
                        ex.InnerException is FaultException)
                    {
                        var faultEx = ex.InnerException as FaultException;

                        if (faultEx.Code != null &&
                            faultEx.Code.SubCode != null &&
                            faultEx.Code.SubCode.Name == "BadContextToken")
                        {
                            --retry;
                            if (retry >= 0)
                            {
                                this.ReinitializeService();
                                continue;
                            }
                        }
                    }

                    throw;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Reinitializes the service instance.
        /// </summary>
        private void ReinitializeService()
        {
            this.Initialize(this.config);
        }

        #endregion
    }
}
