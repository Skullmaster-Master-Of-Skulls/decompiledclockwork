using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000601 RID: 1537
	internal static class JitHelpers
	{
		// Token: 0x06003800 RID: 14336
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void UnsafeSetArrayElement(object[] target, int index, object element);
	}
}
