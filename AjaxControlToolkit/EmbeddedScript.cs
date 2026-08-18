using System;
using System.Reflection;

namespace AjaxControlToolkit
{
	// Token: 0x02000014 RID: 20
	public class EmbeddedScript
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000040C2 File Offset: 0x000022C2
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000040CA File Offset: 0x000022CA
		public Assembly SourceAssembly { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000040D3 File Offset: 0x000022D3
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000040DB File Offset: 0x000022DB
		public string Name { get; private set; }

		// Token: 0x060000E0 RID: 224 RVA: 0x000040E4 File Offset: 0x000022E4
		public EmbeddedScript(string name, Assembly sourceAssembly)
		{
			this.Name = name;
			this.SourceAssembly = sourceAssembly;
		}
	}
}
