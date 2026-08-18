using System;
using System.Reflection;

namespace TechnoPro.Common.Public.Entities.DynamicCompile
{
	// Token: 0x020003C0 RID: 960
	public class DynamicCompileAssembly
	{
		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x00021292 File Offset: 0x0001F492
		// (set) Token: 0x06001D4B RID: 7499 RVA: 0x0002129A File Offset: 0x0001F49A
		public string Code { get; set; }

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x000212A3 File Offset: 0x0001F4A3
		// (set) Token: 0x06001D4D RID: 7501 RVA: 0x000212AB File Offset: 0x0001F4AB
		public Assembly Assembly { get; set; }
	}
}
