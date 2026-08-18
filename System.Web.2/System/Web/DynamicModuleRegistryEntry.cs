using System;

namespace System.Web
{
	// Token: 0x02000054 RID: 84
	internal struct DynamicModuleRegistryEntry
	{
		// Token: 0x060005CE RID: 1486 RVA: 0x00007D4F File Offset: 0x00005F4F
		public DynamicModuleRegistryEntry(string name, string type)
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x0400015F RID: 351
		public readonly string Name;

		// Token: 0x04000160 RID: 352
		public readonly string Type;
	}
}
