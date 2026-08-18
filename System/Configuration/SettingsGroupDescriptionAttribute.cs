using System;

namespace System.Configuration
{
	// Token: 0x0200070A RID: 1802
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SettingsGroupDescriptionAttribute : Attribute
	{
		// Token: 0x06003762 RID: 14178 RVA: 0x000EB504 File Offset: 0x000EA504
		public SettingsGroupDescriptionAttribute(string description)
		{
			this._desc = description;
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06003763 RID: 14179 RVA: 0x000EB513 File Offset: 0x000EA513
		public string Description
		{
			get
			{
				return this._desc;
			}
		}

		// Token: 0x040031CC RID: 12748
		private readonly string _desc;
	}
}
