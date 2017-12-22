namespace SkiData.CP.FormatService.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines known <see cref="FaultException" /> codes for Format.Service operation.
    /// </summary>
    public static class FaultCodes
    {
        /// <summary>
        /// Gets the fault code for when the given batch ID could not be found.
        /// </summary>
        public static FaultCode BatchNotFound
        {
            get
            {
                return new FaultCode("BatchNotFound");
            }
        }

        /// <summary>
        /// Gets the fault code for when the batch size exceeds the maximum the service allows.
        /// </summary>
        public static FaultCode BatchSizeExceedsMaximum
        {
            get
            {
                return new FaultCode("BatchSizeExceedsMaximum");
            }
        }

        /// <summary>
        /// Gets the fault code for when the batch size exceeds the maximum the service allows.
        /// </summary>
        public static FaultCode EmptyBatch
        {
            get
            {
                return new FaultCode("EmptyBatch");
            }
        }

        /// <summary>
        /// Gets the fault code for when an formatting error occurred.
        /// </summary>
        public static FaultCode FormattingError
        {
            get
            {
                return new FaultCode("FormattingError");
            }
        }

        /// <summary>
        /// Gets the fault code for when no formatting credits are available for the operation.
        /// </summary>
        public static FaultCode NoCreditsAvailable
        {
            get
            {
                return new FaultCode("NoCreditsAvailable");
            }
        }

        /// <summary>
        /// Gets the fault code for when an invalid format type was provided.
        /// </summary>
        public static FaultCode InvalidFormatType
        {
            get
            {
                return new FaultCode("InvalidFormatType");
            }
        }

        /// <summary>
        /// Gets the fault code for when the secure infrastructure is offline.
        /// </summary>
        public static FaultCode SecureInfrastructureOffline
        {
            get { return new FaultCode("SecureInfrastructureOffline"); }
        }
    }
}
