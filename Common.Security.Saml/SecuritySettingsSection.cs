using System;
using System.Configuration;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x02000019 RID: 25
	public sealed class SecuritySettingsSection : ConfigurationSection
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000045FC File Offset: 0x000027FC
		// (set) Token: 0x060000DB RID: 219 RVA: 0x0000461E File Offset: 0x0000281E
		[ConfigurationProperty("tokenIssuer", IsRequired = false, IsKey = false)]
		public SecurityTokenElement TokenIssuer
		{
			get
			{
				return (SecurityTokenElement)base["tokenIssuer"];
			}
			set
			{
				base["tokenIssuer"] = value;
			}
		}
	}
}
