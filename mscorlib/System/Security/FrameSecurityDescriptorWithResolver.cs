using System;
using System.Reflection.Emit;

namespace System.Security
{
	// Token: 0x02000671 RID: 1649
	internal class FrameSecurityDescriptorWithResolver : FrameSecurityDescriptor
	{
		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06003B67 RID: 15207 RVA: 0x000C9410 File Offset: 0x000C8410
		public DynamicResolver Resolver
		{
			get
			{
				return this.m_resolver;
			}
		}

		// Token: 0x04001EB7 RID: 7863
		private DynamicResolver m_resolver;
	}
}
