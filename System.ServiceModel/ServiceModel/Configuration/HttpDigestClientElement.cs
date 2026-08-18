using System;
using System.Configuration;
using System.Security.Principal;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000623 RID: 1571
	public sealed class HttpDigestClientElement : ConfigurationElement
	{
		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06003C63 RID: 15459 RVA: 0x000E6B3C File Offset: 0x000E4D3C
		// (set) Token: 0x06003C64 RID: 15460 RVA: 0x000E6B4E File Offset: 0x000E4D4E
		[ConfigurationProperty("impersonationLevel", DefaultValue = TokenImpersonationLevel.Identification)]
		[ServiceModelEnumValidator(typeof(TokenImpersonationLevelHelper))]
		public TokenImpersonationLevel ImpersonationLevel
		{
			get
			{
				return (TokenImpersonationLevel)base["impersonationLevel"];
			}
			set
			{
				base["impersonationLevel"] = value;
			}
		}

		// Token: 0x06003C65 RID: 15461 RVA: 0x000E6B64 File Offset: 0x000E4D64
		public void Copy(HttpDigestClientElement from)
		{
			if (this.IsReadOnly())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigReadOnly")));
			}
			if (from == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("from");
			}
			this.ImpersonationLevel = from.ImpersonationLevel;
		}

		// Token: 0x06003C66 RID: 15462 RVA: 0x000E6BB2 File Offset: 0x000E4DB2
		internal void ApplyConfiguration(HttpDigestClientCredential digest)
		{
			if (digest == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("digest");
			}
			digest.AllowedImpersonationLevel = this.ImpersonationLevel;
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06003C67 RID: 15463 RVA: 0x000E6BD4 File Offset: 0x000E4DD4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("impersonationLevel", typeof(TokenImpersonationLevel), TokenImpersonationLevel.Identification, null, new ServiceModelEnumValidator(typeof(TokenImpersonationLevelHelper)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C81 RID: 11393
		private ConfigurationPropertyCollection properties;
	}
}
