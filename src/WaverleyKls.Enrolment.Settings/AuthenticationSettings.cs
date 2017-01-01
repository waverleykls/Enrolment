namespace WaverleyKls.Enrolment.Settings
{
    /// <summary>
    /// This represents the settings entity for authentication.
    /// </summary>
    public class AuthenticationSettings
    {
        /// <summary>
        /// Gets or sets the <see cref="AzureAdSettings"/> instance.
        /// </summary>
        public virtual AzureAdSettings AzureAd { get; set; }
    }

    /// <summary>
    /// This represents the settings entity for Azure AD.
    /// </summary>
    public class AzureAdSettings
    {
        /// <summary>
        /// Gets or sets the Azure AD login URL.
        /// </summary>
        public virtual string AadInstance { get; set; }

        /// <summary>
        /// Gets or sets the callback path.
        /// </summary>
        public virtual string CallbackPath { get; set; }

        /// <summary>
        /// Gets or sets the client Id.
        /// </summary>
        public virtual string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret key.
        /// </summary>
        public virtual string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the tenant domain.
        /// </summary>
        public virtual string Domain { get; set; }

        /// <summary>
        /// Gets or sets the tenant Id.
        /// </summary>
        public virtual string TenantId { get; set; }
    }
}
