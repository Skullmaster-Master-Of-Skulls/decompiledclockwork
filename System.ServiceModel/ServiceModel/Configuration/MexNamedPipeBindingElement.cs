using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200065A RID: 1626
	public class MexNamedPipeBindingElement : MexBindingElement<CustomBinding>
	{
		// Token: 0x06003EAA RID: 16042 RVA: 0x000EE8B9 File Offset: 0x000ECAB9
		public MexNamedPipeBindingElement(string name) : base(name)
		{
		}

		// Token: 0x06003EAB RID: 16043 RVA: 0x000EE8C2 File Offset: 0x000ECAC2
		public MexNamedPipeBindingElement() : this(null)
		{
		}
	}
}
