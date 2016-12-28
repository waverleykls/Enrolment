using System;
using System.Runtime.Serialization;

namespace WaverleyKls.Enrolment.Settings.Exceptions
{
    /// <summary>
    /// This represents the exception entity thrown relating to <c>appsettings.json</c>.
    /// </summary>
    public class AppSettingsException : ApplicationException
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AppSettingsException"/> class.
        /// </summary>
        public AppSettingsException()
            : base("Invalid app settings")
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="AppSettingsException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error. </param>
        public AppSettingsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="AppSettingsException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
        public AppSettingsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="AppSettingsException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data. </param>
        /// <param name="context">The contextual information about the source or destination. </param>
        protected AppSettingsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
