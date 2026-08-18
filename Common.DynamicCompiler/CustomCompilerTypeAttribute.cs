using System;
using System.Linq;

namespace TechnoPro.Common.DynamicCompiler
{
	// Token: 0x0200000B RID: 11
	public class CustomCompilerTypeAttribute : Attribute
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002E69 File Offset: 0x00001069
		public CustomCompilerTypeAttribute()
		{
			this.PropertiesCode = "";
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002E7F File Offset: 0x0000107F
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002E87 File Offset: 0x00001087
		public string[] DefaultImports { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002E90 File Offset: 0x00001090
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002E98 File Offset: 0x00001098
		public string[] DefaultUsings { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002EA1 File Offset: 0x000010A1
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002EA9 File Offset: 0x000010A9
		public string ConstructorCode { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002EB2 File Offset: 0x000010B2
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002EBA File Offset: 0x000010BA
		public string PropertiesCode { get; set; }

		// Token: 0x06000052 RID: 82 RVA: 0x00002EC4 File Offset: 0x000010C4
		public static CustomCompilerTypeAttribute GetAttribute(eCustomCompilerType enumeration)
		{
			return CustomCompilerTypeAttribute.GetAttribute<CustomCompilerTypeAttribute>(enumeration);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002EE4 File Offset: 0x000010E4
		private static T GetAttribute<T>(Enum enumeration) where T : Attribute
		{
			return enumeration.GetType().GetMember(enumeration.ToString())[0].GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault<T>();
		}
	}
}
