using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel
{
	// Token: 0x02000108 RID: 264
	[__DynamicallyInvokable]
	public abstract class DuplexClientBase<TChannel> : ClientBase<TChannel> where TChannel : class
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x0001ACE5 File Offset: 0x00018EE5
		protected DuplexClientBase(object callbackInstance) : this(new InstanceContext(callbackInstance))
		{
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001ACF3 File Offset: 0x00018EF3
		protected DuplexClientBase(object callbackInstance, string endpointConfigurationName) : this(new InstanceContext(callbackInstance), endpointConfigurationName)
		{
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001AD02 File Offset: 0x00018F02
		protected DuplexClientBase(object callbackInstance, string endpointConfigurationName, string remoteAddress) : this(new InstanceContext(callbackInstance), endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001AD12 File Offset: 0x00018F12
		protected DuplexClientBase(object callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress) : this(new InstanceContext(callbackInstance), endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001AD22 File Offset: 0x00018F22
		protected DuplexClientBase(object callbackInstance, Binding binding, EndpointAddress remoteAddress) : this(new InstanceContext(callbackInstance), binding, remoteAddress)
		{
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001AD32 File Offset: 0x00018F32
		protected DuplexClientBase(object callbackInstance, ServiceEndpoint endpoint) : this(new InstanceContext(callbackInstance), endpoint)
		{
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001AD41 File Offset: 0x00018F41
		[__DynamicallyInvokable]
		protected DuplexClientBase(InstanceContext callbackInstance) : base(callbackInstance)
		{
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001AD4A File Offset: 0x00018F4A
		[__DynamicallyInvokable]
		protected DuplexClientBase(InstanceContext callbackInstance, string endpointConfigurationName) : base(callbackInstance, endpointConfigurationName)
		{
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001AD54 File Offset: 0x00018F54
		[__DynamicallyInvokable]
		protected DuplexClientBase(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : base(callbackInstance, endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001AD5F File Offset: 0x00018F5F
		[__DynamicallyInvokable]
		protected DuplexClientBase(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress) : base(callbackInstance, endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001AD6A File Offset: 0x00018F6A
		[__DynamicallyInvokable]
		protected DuplexClientBase(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress) : base(callbackInstance, binding, remoteAddress)
		{
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001AD75 File Offset: 0x00018F75
		protected DuplexClientBase(InstanceContext callbackInstance, ServiceEndpoint endpoint) : base(callbackInstance, endpoint)
		{
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0001AD7F File Offset: 0x00018F7F
		public IDuplexContextChannel InnerDuplexChannel
		{
			get
			{
				return (IDuplexContextChannel)base.InnerChannel;
			}
		}
	}
}
