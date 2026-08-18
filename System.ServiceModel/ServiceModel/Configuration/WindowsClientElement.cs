using System;
using System.Configuration;
using System.Security.Principal;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200069A RID: 1690
	public sealed class WindowsClientElement : ConfigurationElement
	{
		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x0600416D RID: 16749 RVA: 0x000F8410 File Offset: 0x000F6610
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("allowNtlm", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("allowedImpersonationLevel", typeof(TokenImpersonationLevel), TokenImpersonationLevel.Identification, null, new ServiceModelEnumValidator(typeof(TokenImpersonationLevelHelper)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x0600416F RID: 16751 RVA: 0x000F8494 File Offset: 0x000F6694
		// (set) Token: 0x06004170 RID: 16752 RVA: 0x000F84A6 File Offset: 0x000F66A6
		[ConfigurationProperty("allowNtlm", DefaultValue = true)]
		public bool AllowNtlm
		{
			get
			{
				return (bool)base["allowNtlm"];
			}
			set
			{
				base["allowNtlm"] = value;
			}
		}

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x06004171 RID: 16753 RVA: 0x000F84B9 File Offset: 0x000F66B9
		// (set) Token: 0x06004172 RID: 16754 RVA: 0x000F84CB File Offset: 0x000F66CB
		[ConfigurationProperty("allowedImpersonationLevel", DefaultValue = TokenImpersonationLevel.Identification)]
		[ServiceModelEnumValidator(typeof(TokenImpersonationLevelHelper))]
		public TokenImpersonationLevel AllowedImpersonationLevel
		{
			get
			{
				return (TokenImpersonationLevel)base["allowedImpersonationLevel"];
			}
			set
			{
				base["allowedImpersonationLevel"] = value;
			}
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x000F84E0 File Offset: 0x000F66E0
		public void Copy(WindowsClientElement from)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (from == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("from");
			}
			this.AllowNtlm = from.AllowNtlm;
			this.AllowedImpersonationLevel = from.AllowedImpersonationLevel;
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x000F853A File Offset: 0x000F673A
		internal void ApplyConfiguration(WindowsClientCredential windows)
		{
			if (windows == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("windows");
			}
			windows.AllowNtlm = this.AllowNtlm;
			windows.AllowedImpersonationLevel = this.AllowedImpersonationLevel;
		}

		// Token: 0x04002CE8 RID: 11496
		private ConfigurationPropertyCollection properties;
	}
}
