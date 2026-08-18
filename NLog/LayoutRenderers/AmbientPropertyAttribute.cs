using System;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C1 RID: 193
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class AmbientPropertyAttribute : NameBaseAttribute
	{
		// Token: 0x060005AB RID: 1451 RVA: 0x0000CC8D File Offset: 0x0000AE8D
		public AmbientPropertyAttribute(string name) : base(name)
		{
		}
	}
}
