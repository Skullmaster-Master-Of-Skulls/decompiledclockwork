using System;

namespace Databases
{
	// Token: 0x02000009 RID: 9
	public class DatabaseSuffixAttribute : Attribute
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00005E56 File Offset: 0x00004056
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00005E5E File Offset: 0x0000405E
		public string DatabaseNameSuffix { get; set; }

		// Token: 0x060000A9 RID: 169 RVA: 0x00005E67 File Offset: 0x00004067
		public DatabaseSuffixAttribute(string databaseNameSuffix)
		{
			this.DatabaseNameSuffix = databaseNameSuffix;
		}
	}
}
