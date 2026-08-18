using System;

namespace System.Configuration
{
	// Token: 0x0200070F RID: 1807
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SpecialSettingAttribute : Attribute
	{
		// Token: 0x0600376D RID: 14189 RVA: 0x000EB58E File Offset: 0x000EA58E
		public SpecialSettingAttribute(SpecialSetting specialSetting)
		{
			this._specialSetting = specialSetting;
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x0600376E RID: 14190 RVA: 0x000EB59D File Offset: 0x000EA59D
		public SpecialSetting SpecialSetting
		{
			get
			{
				return this._specialSetting;
			}
		}

		// Token: 0x040031D1 RID: 12753
		private readonly SpecialSetting _specialSetting;
	}
}
