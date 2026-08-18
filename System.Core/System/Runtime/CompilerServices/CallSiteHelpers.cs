using System;
using System.Dynamic;
using System.Reflection;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200013D RID: 317
	[__DynamicallyInvokable]
	public static class CallSiteHelpers
	{
		// Token: 0x06000A48 RID: 2632 RVA: 0x00025820 File Offset: 0x00023A20
		[__DynamicallyInvokable]
		public static bool IsInternalFrame(MethodBase mb)
		{
			return (mb.Name == "CallSite.Target" && mb.GetType() != CallSiteHelpers._knownNonDynamicMethodType) || mb.DeclaringType == typeof(UpdateDelegates);
		}

		// Token: 0x0400076C RID: 1900
		private static Type _knownNonDynamicMethodType = typeof(object).GetMethod("ToString").GetType();
	}
}
