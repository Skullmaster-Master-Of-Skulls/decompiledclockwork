using System;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D7 RID: 215
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class LayoutRendererAttribute : NameBaseAttribute
	{
		// Token: 0x06000644 RID: 1604 RVA: 0x0000DFB1 File Offset: 0x0000C1B1
		public LayoutRendererAttribute(string name) : base(name)
		{
		}
	}
}
