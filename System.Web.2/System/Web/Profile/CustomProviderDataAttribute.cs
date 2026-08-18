using System;

namespace System.Web.Profile
{
	// Token: 0x02000159 RID: 345
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class CustomProviderDataAttribute : Attribute
	{
		// Token: 0x060013B7 RID: 5047 RVA: 0x00038F01 File Offset: 0x00037101
		public CustomProviderDataAttribute(string customProviderData)
		{
			this._CustomProviderData = customProviderData;
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x00038F10 File Offset: 0x00037110
		public string CustomProviderData
		{
			get
			{
				return this._CustomProviderData;
			}
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00038F18 File Offset: 0x00037118
		public override bool IsDefaultAttribute()
		{
			return string.IsNullOrEmpty(this._CustomProviderData);
		}

		// Token: 0x040014ED RID: 5357
		private string _CustomProviderData;
	}
}
