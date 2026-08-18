using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200081E RID: 2078
	internal class GenericFieldInfo
	{
		// Token: 0x060049D3 RID: 18899 RVA: 0x00100F5C File Offset: 0x000FFF5C
		internal GenericFieldInfo(RuntimeFieldHandle field, RuntimeTypeHandle context)
		{
			this.m_field = field;
			this.m_context = context;
		}

		// Token: 0x040025C8 RID: 9672
		internal RuntimeFieldHandle m_field;

		// Token: 0x040025C9 RID: 9673
		internal RuntimeTypeHandle m_context;
	}
}
