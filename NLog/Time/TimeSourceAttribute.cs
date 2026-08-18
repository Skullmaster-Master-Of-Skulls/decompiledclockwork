using System;
using NLog.Config;

namespace NLog.Time
{
	// Token: 0x0200018F RID: 399
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class TimeSourceAttribute : NameBaseAttribute
	{
		// Token: 0x06000EA0 RID: 3744 RVA: 0x00023A5F File Offset: 0x00021C5F
		public TimeSourceAttribute(string name) : base(name)
		{
		}
	}
}
