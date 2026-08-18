using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000629 RID: 1577
	public class MexHttpBindingElement : MexBindingElement<WSHttpBinding>
	{
		// Token: 0x06003C78 RID: 15480 RVA: 0x000E6DA8 File Offset: 0x000E4FA8
		public MexHttpBindingElement(string name) : base(name)
		{
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x000E6DB1 File Offset: 0x000E4FB1
		public MexHttpBindingElement() : this(null)
		{
		}
	}
}
