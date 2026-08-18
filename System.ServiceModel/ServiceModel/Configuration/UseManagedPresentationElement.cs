using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006E1 RID: 1761
	public class UseManagedPresentationElement : BindingElementExtensionElement
	{
		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x060043F5 RID: 17397 RVA: 0x00100D27 File Offset: 0x000FEF27
		public override Type BindingElementType
		{
			get
			{
				return typeof(UseManagedPresentationBindingElement);
			}
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x00100D34 File Offset: 0x000FEF34
		protected internal override BindingElement CreateBindingElement()
		{
			UseManagedPresentationBindingElement useManagedPresentationBindingElement = new UseManagedPresentationBindingElement();
			this.ApplyConfiguration(useManagedPresentationBindingElement);
			return useManagedPresentationBindingElement;
		}
	}
}
