using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020006F6 RID: 1782
	[__DynamicallyInvokable]
	public abstract class BindingElement
	{
		// Token: 0x06004453 RID: 17491 RVA: 0x00101DEC File Offset: 0x000FFFEC
		[__DynamicallyInvokable]
		protected BindingElement()
		{
		}

		// Token: 0x06004454 RID: 17492 RVA: 0x00101DF4 File Offset: 0x000FFFF4
		[__DynamicallyInvokable]
		protected BindingElement(BindingElement elementToBeCloned)
		{
		}

		// Token: 0x06004455 RID: 17493
		[__DynamicallyInvokable]
		public abstract BindingElement Clone();

		// Token: 0x06004456 RID: 17494 RVA: 0x00101DFC File Offset: 0x000FFFFC
		[__DynamicallyInvokable]
		public virtual IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return context.BuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06004457 RID: 17495 RVA: 0x00101E17 File Offset: 0x00100017
		public virtual IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context) where TChannel : class, IChannel
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return context.BuildInnerChannelListener<TChannel>();
		}

		// Token: 0x06004458 RID: 17496 RVA: 0x00101E32 File Offset: 0x00100032
		[__DynamicallyInvokable]
		public virtual bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return context.CanBuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06004459 RID: 17497 RVA: 0x00101E4D File Offset: 0x0010004D
		public virtual bool CanBuildChannelListener<TChannel>(BindingContext context) where TChannel : class, IChannel
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return context.CanBuildInnerChannelListener<TChannel>();
		}

		// Token: 0x0600445A RID: 17498
		[__DynamicallyInvokable]
		public abstract T GetProperty<T>(BindingContext context) where T : class;

		// Token: 0x0600445B RID: 17499 RVA: 0x00101E68 File Offset: 0x00100068
		internal T GetIndividualProperty<T>() where T : class
		{
			return this.GetProperty<T>(new BindingContext(new CustomBinding(), new BindingParameterCollection()));
		}

		// Token: 0x0600445C RID: 17500 RVA: 0x00101E7F File Offset: 0x0010007F
		internal virtual bool IsMatch(BindingElement b)
		{
			return false;
		}
	}
}
