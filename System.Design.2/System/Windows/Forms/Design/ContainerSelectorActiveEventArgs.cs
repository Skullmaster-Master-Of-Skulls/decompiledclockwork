using System;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002A8 RID: 680
	internal class ContainerSelectorActiveEventArgs : EventArgs
	{
		// Token: 0x06001AAE RID: 6830 RVA: 0x0009C228 File Offset: 0x0009A428
		public ContainerSelectorActiveEventArgs(object component) : this(component, ContainerSelectorActiveEventArgsType.Mouse)
		{
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x0009C232 File Offset: 0x0009A432
		public ContainerSelectorActiveEventArgs(object component, ContainerSelectorActiveEventArgsType eventType)
		{
			this.component = component;
			this.eventType = eventType;
		}

		// Token: 0x04001607 RID: 5639
		private readonly object component;

		// Token: 0x04001608 RID: 5640
		private readonly ContainerSelectorActiveEventArgsType eventType;
	}
}
