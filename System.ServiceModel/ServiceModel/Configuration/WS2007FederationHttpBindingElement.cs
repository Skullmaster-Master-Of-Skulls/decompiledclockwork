using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006E3 RID: 1763
	public class WS2007FederationHttpBindingElement : WSFederationHttpBindingElement
	{
		// Token: 0x060043FB RID: 17403 RVA: 0x00100E1A File Offset: 0x000FF01A
		public WS2007FederationHttpBindingElement(string name) : base(name)
		{
		}

		// Token: 0x060043FC RID: 17404 RVA: 0x00100E23 File Offset: 0x000FF023
		public WS2007FederationHttpBindingElement() : this(null)
		{
		}

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x060043FD RID: 17405 RVA: 0x00100E2C File Offset: 0x000FF02C
		protected override Type BindingElementType
		{
			get
			{
				return typeof(WS2007FederationHttpBinding);
			}
		}
	}
}
