using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200069F RID: 1695
	public sealed class AuthenticationSection : ConfigurationSection
	{
		// Token: 0x06005168 RID: 20840 RVA: 0x0011800C File Offset: 0x0011620C
		static AuthenticationSection()
		{
			AuthenticationSection._properties = new ConfigurationPropertyCollection();
			AuthenticationSection._properties.Add(AuthenticationSection._propForms);
			AuthenticationSection._properties.Add(AuthenticationSection._propPassport);
			AuthenticationSection._properties.Add(AuthenticationSection._propMode);
		}

		// Token: 0x1700175C RID: 5980
		// (get) Token: 0x0600516A RID: 20842 RVA: 0x001180A6 File Offset: 0x001162A6
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthenticationSection._properties;
			}
		}

		// Token: 0x1700175D RID: 5981
		// (get) Token: 0x0600516B RID: 20843 RVA: 0x001180AD File Offset: 0x001162AD
		[ConfigurationProperty("forms")]
		public FormsAuthenticationConfiguration Forms
		{
			get
			{
				return (FormsAuthenticationConfiguration)base[AuthenticationSection._propForms];
			}
		}

		// Token: 0x1700175E RID: 5982
		// (get) Token: 0x0600516C RID: 20844 RVA: 0x001180BF File Offset: 0x001162BF
		[ConfigurationProperty("passport")]
		[Obsolete("This property is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
		public PassportAuthentication Passport
		{
			get
			{
				return (PassportAuthentication)base[AuthenticationSection._propPassport];
			}
		}

		// Token: 0x1700175F RID: 5983
		// (get) Token: 0x0600516D RID: 20845 RVA: 0x001180D1 File Offset: 0x001162D1
		// (set) Token: 0x0600516E RID: 20846 RVA: 0x001180FE File Offset: 0x001162FE
		[ConfigurationProperty("mode", DefaultValue = AuthenticationMode.Windows)]
		public AuthenticationMode Mode
		{
			get
			{
				if (!this.authenticationModeCached)
				{
					this.authenticationModeCache = (AuthenticationMode)base[AuthenticationSection._propMode];
					this.authenticationModeCached = true;
				}
				return this.authenticationModeCache;
			}
			set
			{
				base[AuthenticationSection._propMode] = value;
				this.authenticationModeCache = value;
			}
		}

		// Token: 0x0600516F RID: 20847 RVA: 0x00118118 File Offset: 0x00116318
		protected override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
			this.authenticationModeCached = false;
		}

		// Token: 0x06005170 RID: 20848 RVA: 0x00118128 File Offset: 0x00116328
		internal void ValidateAuthenticationMode()
		{
			if (this.Mode == AuthenticationMode.Passport && UnsafeNativeMethods.PassportVersion() < 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Passport_not_installed"));
			}
		}

		// Token: 0x04002B0C RID: 11020
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002B0D RID: 11021
		private static readonly ConfigurationProperty _propForms = new ConfigurationProperty("forms", typeof(FormsAuthenticationConfiguration), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002B0E RID: 11022
		private static readonly ConfigurationProperty _propPassport = new ConfigurationProperty("passport", typeof(PassportAuthentication), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002B0F RID: 11023
		private static readonly ConfigurationProperty _propMode = new ConfigurationProperty("mode", typeof(AuthenticationMode), AuthenticationMode.Windows, ConfigurationPropertyOptions.None);

		// Token: 0x04002B10 RID: 11024
		private bool authenticationModeCached;

		// Token: 0x04002B11 RID: 11025
		private AuthenticationMode authenticationModeCache;
	}
}
