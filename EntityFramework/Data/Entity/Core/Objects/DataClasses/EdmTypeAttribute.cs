using System;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000532 RID: 1330
	public abstract class EdmTypeAttribute : Attribute
	{
		// Token: 0x060032D8 RID: 13016 RVA: 0x000F068F File Offset: 0x000EE88F
		internal EdmTypeAttribute()
		{
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x060032D9 RID: 13017 RVA: 0x000F0697 File Offset: 0x000EE897
		// (set) Token: 0x060032DA RID: 13018 RVA: 0x000F069F File Offset: 0x000EE89F
		public string Name { get; set; }

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x060032DB RID: 13019 RVA: 0x000F06A8 File Offset: 0x000EE8A8
		// (set) Token: 0x060032DC RID: 13020 RVA: 0x000F06B0 File Offset: 0x000EE8B0
		public string NamespaceName { get; set; }
	}
}
