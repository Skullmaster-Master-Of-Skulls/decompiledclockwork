using System;
using NLog.Config;

namespace NLog.Layouts
{
	// Token: 0x02000117 RID: 279
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class LayoutAttribute : NameBaseAttribute
	{
		// Token: 0x060007BD RID: 1981 RVA: 0x00010EEC File Offset: 0x0000F0EC
		public LayoutAttribute(string name) : base(name)
		{
		}
	}
}
