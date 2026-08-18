using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004FA RID: 1274
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	public sealed class OutAttribute : Attribute
	{
		// Token: 0x0600317E RID: 12670 RVA: 0x000A94F0 File Offset: 0x000A84F0
		internal static Attribute GetCustomAttribute(ParameterInfo parameter)
		{
			if (!parameter.IsOut)
			{
				return null;
			}
			return new OutAttribute();
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x000A9501 File Offset: 0x000A8501
		internal static bool IsDefined(ParameterInfo parameter)
		{
			return parameter.IsOut;
		}
	}
}
