using System;
using System.Configuration;
using System.ServiceModel.PeerResolvers;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200066A RID: 1642
	public sealed class PeerResolverElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06003F1D RID: 16157 RVA: 0x000EFD74 File Offset: 0x000EDF74
		// (set) Token: 0x06003F1E RID: 16158 RVA: 0x000EFD86 File Offset: 0x000EDF86
		[ConfigurationProperty("mode", DefaultValue = PeerResolverMode.Auto)]
		[ServiceModelEnumValidator(typeof(PeerResolverModeHelper))]
		public PeerResolverMode Mode
		{
			get
			{
				return (PeerResolverMode)base["mode"];
			}
			set
			{
				base["mode"] = value;
			}
		}

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06003F1F RID: 16159 RVA: 0x000EFD99 File Offset: 0x000EDF99
		// (set) Token: 0x06003F20 RID: 16160 RVA: 0x000EFDAB File Offset: 0x000EDFAB
		[ConfigurationProperty("referralPolicy", DefaultValue = PeerReferralPolicy.Service)]
		[ServiceModelEnumValidator(typeof(PeerReferralPolicyHelper))]
		public PeerReferralPolicy ReferralPolicy
		{
			get
			{
				return (PeerReferralPolicy)base["referralPolicy"];
			}
			set
			{
				base["referralPolicy"] = value;
			}
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06003F21 RID: 16161 RVA: 0x000EFDBE File Offset: 0x000EDFBE
		[ConfigurationProperty("custom")]
		public PeerCustomResolverElement Custom
		{
			get
			{
				return (PeerCustomResolverElement)base["custom"];
			}
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x000EFDD0 File Offset: 0x000EDFD0
		internal void ApplyConfiguration(PeerResolverSettings settings)
		{
			if (settings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("settings");
			}
			settings.Mode = this.Mode;
			settings.ReferralPolicy = this.ReferralPolicy;
			this.Custom.ApplyConfiguration(settings.Custom);
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x000EFE10 File Offset: 0x000EE010
		internal void InitializeFrom(PeerResolverSettings settings)
		{
			if (settings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("settings");
			}
			base.SetPropertyValueIfNotDefaultValue<PeerResolverMode>("mode", settings.Mode);
			base.SetPropertyValueIfNotDefaultValue<PeerReferralPolicy>("referralPolicy", settings.ReferralPolicy);
			this.Custom.InitializeFrom(settings.Custom);
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06003F24 RID: 16164 RVA: 0x000EFE64 File Offset: 0x000EE064
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("mode", typeof(PeerResolverMode), PeerResolverMode.Auto, null, new ServiceModelEnumValidator(typeof(PeerResolverModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("referralPolicy", typeof(PeerReferralPolicy), PeerReferralPolicy.Service, null, new ServiceModelEnumValidator(typeof(PeerReferralPolicyHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("custom", typeof(PeerCustomResolverElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002CB1 RID: 11441
		private ConfigurationPropertyCollection properties;
	}
}
