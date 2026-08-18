using System;

namespace System.Reflection
{
	// Token: 0x02000004 RID: 4
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	internal sealed class AssemblyMetadataAttribute : Attribute
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002288 File Offset: 0x00000488
		public AssemblyMetadataAttribute(string key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000229E File Offset: 0x0000049E
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000022A6 File Offset: 0x000004A6
		public string Key { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022AF File Offset: 0x000004AF
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000022B7 File Offset: 0x000004B7
		public string Value { get; set; }
	}
}
