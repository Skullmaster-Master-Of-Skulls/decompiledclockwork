using System;

namespace System.Web.Profile
{
	// Token: 0x02000158 RID: 344
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingsAllowAnonymousAttribute : Attribute
	{
		// Token: 0x060013B4 RID: 5044 RVA: 0x00038EDF File Offset: 0x000370DF
		public SettingsAllowAnonymousAttribute(bool allow)
		{
			this._Allow = allow;
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x00038EEE File Offset: 0x000370EE
		public bool Allow
		{
			get
			{
				return this._Allow;
			}
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00038EF6 File Offset: 0x000370F6
		public override bool IsDefaultAttribute()
		{
			return !this._Allow;
		}

		// Token: 0x040014EC RID: 5356
		private bool _Allow;
	}
}
