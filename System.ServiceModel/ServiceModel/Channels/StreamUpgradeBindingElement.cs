using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008A1 RID: 2209
	public abstract class StreamUpgradeBindingElement : BindingElement
	{
		// Token: 0x06005440 RID: 21568 RVA: 0x001365FF File Offset: 0x001347FF
		protected StreamUpgradeBindingElement()
		{
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x00136607 File Offset: 0x00134807
		protected StreamUpgradeBindingElement(StreamUpgradeBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
		}

		// Token: 0x06005442 RID: 21570
		public abstract StreamUpgradeProvider BuildClientStreamUpgradeProvider(BindingContext context);

		// Token: 0x06005443 RID: 21571
		public abstract StreamUpgradeProvider BuildServerStreamUpgradeProvider(BindingContext context);
	}
}
