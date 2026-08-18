using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004F8 RID: 1272
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class PreserveSigAttribute : Attribute
	{
		// Token: 0x06003178 RID: 12664 RVA: 0x000A949C File Offset: 0x000A849C
		internal static Attribute GetCustomAttribute(RuntimeMethodInfo method)
		{
			if ((method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) == MethodImplAttributes.IL)
			{
				return null;
			}
			return new PreserveSigAttribute();
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x000A94B3 File Offset: 0x000A84B3
		internal static bool IsDefined(RuntimeMethodInfo method)
		{
			return (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL;
		}
	}
}
