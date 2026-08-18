using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004F9 RID: 1273
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	public sealed class InAttribute : Attribute
	{
		// Token: 0x0600317B RID: 12667 RVA: 0x000A94CF File Offset: 0x000A84CF
		internal static Attribute GetCustomAttribute(ParameterInfo parameter)
		{
			if (!parameter.IsIn)
			{
				return null;
			}
			return new InAttribute();
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x000A94E0 File Offset: 0x000A84E0
		internal static bool IsDefined(ParameterInfo parameter)
		{
			return parameter.IsIn;
		}
	}
}
