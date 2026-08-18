using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000361 RID: 865
	public class DynamicDataContextColumnNameAttribute : Attribute
	{
		// Token: 0x06001AD8 RID: 6872 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public DynamicDataContextColumnNameAttribute()
		{
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0001ECF0 File Offset: 0x0001CEF0
		public DynamicDataContextColumnNameAttribute(string dbColName)
		{
			this.DatabaseColumnName = dbColName;
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x0001ED02 File Offset: 0x0001CF02
		// (set) Token: 0x06001ADB RID: 6875 RVA: 0x0001ED0A File Offset: 0x0001CF0A
		public string DatabaseColumnName { get; set; }
	}
}
