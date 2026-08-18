using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200062B RID: 1579
	public class MexHttpsBindingElement : MexBindingElement<WSHttpBinding>
	{
		// Token: 0x06003C7D RID: 15485 RVA: 0x000E6DDF File Offset: 0x000E4FDF
		public MexHttpsBindingElement(string name) : base(name)
		{
		}

		// Token: 0x06003C7E RID: 15486 RVA: 0x000E6DE8 File Offset: 0x000E4FE8
		public MexHttpsBindingElement() : this(null)
		{
		}
	}
}
