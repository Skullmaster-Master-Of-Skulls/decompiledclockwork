using System;

namespace System.Web.Profile
{
	// Token: 0x02000157 RID: 343
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ProfileProviderAttribute : Attribute
	{
		// Token: 0x060013B2 RID: 5042 RVA: 0x00038EC8 File Offset: 0x000370C8
		public ProfileProviderAttribute(string providerName)
		{
			this._ProviderName = providerName;
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00038ED7 File Offset: 0x000370D7
		public string ProviderName
		{
			get
			{
				return this._ProviderName;
			}
		}

		// Token: 0x040014EB RID: 5355
		private string _ProviderName;
	}
}
