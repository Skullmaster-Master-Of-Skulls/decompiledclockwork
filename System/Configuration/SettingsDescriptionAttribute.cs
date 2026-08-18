using System;

namespace System.Configuration
{
	// Token: 0x02000709 RID: 1801
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingsDescriptionAttribute : Attribute
	{
		// Token: 0x06003760 RID: 14176 RVA: 0x000EB4ED File Offset: 0x000EA4ED
		public SettingsDescriptionAttribute(string description)
		{
			this._desc = description;
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06003761 RID: 14177 RVA: 0x000EB4FC File Offset: 0x000EA4FC
		public string Description
		{
			get
			{
				return this._desc;
			}
		}

		// Token: 0x040031CB RID: 12747
		private readonly string _desc;
	}
}
