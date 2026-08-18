using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200081F RID: 2079
	internal class VarArgMethod
	{
		// Token: 0x060049D4 RID: 18900 RVA: 0x00100F72 File Offset: 0x000FFF72
		internal VarArgMethod(MethodInfo method, SignatureHelper signature)
		{
			this.m_method = method;
			this.m_signature = signature;
		}

		// Token: 0x040025CA RID: 9674
		internal MethodInfo m_method;

		// Token: 0x040025CB RID: 9675
		internal SignatureHelper m_signature;
	}
}
