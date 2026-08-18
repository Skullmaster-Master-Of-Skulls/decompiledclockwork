using System;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008A0 RID: 2208
	internal class LocalAddressProvider
	{
		// Token: 0x0600543C RID: 21564 RVA: 0x00136558 File Offset: 0x00134758
		public LocalAddressProvider(EndpointAddress localAddress, MessageFilter filter)
		{
			if (localAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localAddress");
			}
			if (filter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter");
			}
			this.localAddress = localAddress;
			this.filter = filter;
			if (localAddress.Headers.FindHeader(XD.UtilityDictionary.UniqueEndpointHeaderName.Value, XD.UtilityDictionary.UniqueEndpointHeaderNamespace.Value) == null)
			{
				this.priority = 2147483646;
				return;
			}
			this.priority = int.MaxValue;
		}

		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x0600543D RID: 21565 RVA: 0x001365E7 File Offset: 0x001347E7
		public EndpointAddress LocalAddress
		{
			get
			{
				return this.localAddress;
			}
		}

		// Token: 0x170014B6 RID: 5302
		// (get) Token: 0x0600543E RID: 21566 RVA: 0x001365EF File Offset: 0x001347EF
		public MessageFilter Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x0600543F RID: 21567 RVA: 0x001365F7 File Offset: 0x001347F7
		public int Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x04003304 RID: 13060
		private EndpointAddress localAddress;

		// Token: 0x04003305 RID: 13061
		private MessageFilter filter;

		// Token: 0x04003306 RID: 13062
		private int priority;
	}
}
