using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000664 RID: 1636
	public class MexTcpBindingElement : MexBindingElement<CustomBinding>
	{
		// Token: 0x06003EF5 RID: 16117 RVA: 0x000EF561 File Offset: 0x000ED761
		public MexTcpBindingElement(string name) : base(name)
		{
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x000EF56A File Offset: 0x000ED76A
		public MexTcpBindingElement() : this(null)
		{
		}
	}
}
