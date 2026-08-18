using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000521 RID: 1313
	[ComVisible(true)]
	public sealed class ExtensibleClassFactory
	{
		// Token: 0x060032E3 RID: 13027 RVA: 0x000ABC2F File Offset: 0x000AAC2F
		private ExtensibleClassFactory()
		{
		}

		// Token: 0x060032E4 RID: 13028
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RegisterObjectCreationCallback(ObjectCreationDelegate callback);
	}
}
