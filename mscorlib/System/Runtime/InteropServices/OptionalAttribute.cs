using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004FB RID: 1275
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	public sealed class OptionalAttribute : Attribute
	{
		// Token: 0x06003181 RID: 12673 RVA: 0x000A9511 File Offset: 0x000A8511
		internal static Attribute GetCustomAttribute(ParameterInfo parameter)
		{
			if (!parameter.IsOptional)
			{
				return null;
			}
			return new OptionalAttribute();
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x000A9522 File Offset: 0x000A8522
		internal static bool IsDefined(ParameterInfo parameter)
		{
			return parameter.IsOptional;
		}
	}
}
