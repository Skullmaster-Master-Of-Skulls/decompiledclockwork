using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200081D RID: 2077
	internal class GenericMethodInfo
	{
		// Token: 0x060049D2 RID: 18898 RVA: 0x00100F46 File Offset: 0x000FFF46
		internal GenericMethodInfo(RuntimeMethodHandle method, RuntimeTypeHandle context)
		{
			this.m_method = method;
			this.m_context = context;
		}

		// Token: 0x040025C6 RID: 9670
		internal RuntimeMethodHandle m_method;

		// Token: 0x040025C7 RID: 9671
		internal RuntimeTypeHandle m_context;
	}
}
