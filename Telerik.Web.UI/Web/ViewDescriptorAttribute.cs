using System;
using Telerik.Web.UI;

namespace Telerik.Web
{
	// Token: 0x02000140 RID: 320
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	internal sealed class ViewDescriptorAttribute : Attribute
	{
		// Token: 0x06000CD9 RID: 3289 RVA: 0x0002DDCB File Offset: 0x0002BFCB
		public ViewDescriptorAttribute(Type type, string resource, RenderMode mode)
		{
			this.LoadOrder = 0;
			this.RenderMode = mode;
			this.Type = type;
			this.ScriptResource = resource;
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0002DDEF File Offset: 0x0002BFEF
		// (set) Token: 0x06000CDB RID: 3291 RVA: 0x0002DDF7 File Offset: 0x0002BFF7
		public RenderMode RenderMode { get; set; }

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0002DE00 File Offset: 0x0002C000
		// (set) Token: 0x06000CDD RID: 3293 RVA: 0x0002DE08 File Offset: 0x0002C008
		public Type Type { get; set; }

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0002DE11 File Offset: 0x0002C011
		// (set) Token: 0x06000CDF RID: 3295 RVA: 0x0002DE19 File Offset: 0x0002C019
		public string ScriptResource { get; set; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0002DE22 File Offset: 0x0002C022
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x0002DE2A File Offset: 0x0002C02A
		public int LoadOrder { get; set; }
	}
}
