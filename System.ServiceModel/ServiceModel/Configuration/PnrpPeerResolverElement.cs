using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000671 RID: 1649
	public class PnrpPeerResolverElement : BindingElementExtensionElement
	{
		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06003F50 RID: 16208 RVA: 0x000F0551 File Offset: 0x000EE751
		public override Type BindingElementType
		{
			get
			{
				return typeof(PnrpPeerResolverBindingElement);
			}
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x000F055D File Offset: 0x000EE75D
		protected internal override BindingElement CreateBindingElement()
		{
			return new PnrpPeerResolverBindingElement();
		}
	}
}
