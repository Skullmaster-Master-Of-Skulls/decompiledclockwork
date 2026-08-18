using System;

namespace System.Configuration
{
	// Token: 0x0200070C RID: 1804
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsManageabilityAttribute : Attribute
	{
		// Token: 0x06003766 RID: 14182 RVA: 0x000EB532 File Offset: 0x000EA532
		public SettingsManageabilityAttribute(SettingsManageability manageability)
		{
			this._manageability = manageability;
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06003767 RID: 14183 RVA: 0x000EB541 File Offset: 0x000EA541
		public SettingsManageability Manageability
		{
			get
			{
				return this._manageability;
			}
		}

		// Token: 0x040031CE RID: 12750
		private readonly SettingsManageability _manageability;
	}
}
