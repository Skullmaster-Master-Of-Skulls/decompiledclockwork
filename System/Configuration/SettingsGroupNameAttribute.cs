using System;

namespace System.Configuration
{
	// Token: 0x0200070B RID: 1803
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SettingsGroupNameAttribute : Attribute
	{
		// Token: 0x06003764 RID: 14180 RVA: 0x000EB51B File Offset: 0x000EA51B
		public SettingsGroupNameAttribute(string groupName)
		{
			this._groupName = groupName;
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06003765 RID: 14181 RVA: 0x000EB52A File Offset: 0x000EA52A
		public string GroupName
		{
			get
			{
				return this._groupName;
			}
		}

		// Token: 0x040031CD RID: 12749
		private readonly string _groupName;
	}
}
