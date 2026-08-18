using System;
using NLog.Config;

namespace NLog.Conditions
{
	// Token: 0x02000032 RID: 50
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public sealed class ConditionMethodAttribute : NameBaseAttribute
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00003619 File Offset: 0x00001819
		public ConditionMethodAttribute(string name) : base(name)
		{
		}
	}
}
