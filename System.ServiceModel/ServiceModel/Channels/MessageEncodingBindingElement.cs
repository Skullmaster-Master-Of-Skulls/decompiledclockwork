using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009E7 RID: 2535
	[__DynamicallyInvokable]
	public abstract class MessageEncodingBindingElement : BindingElement
	{
		// Token: 0x0600643F RID: 25663 RVA: 0x001764E0 File Offset: 0x001746E0
		[__DynamicallyInvokable]
		protected MessageEncodingBindingElement()
		{
		}

		// Token: 0x06006440 RID: 25664 RVA: 0x001764E8 File Offset: 0x001746E8
		[__DynamicallyInvokable]
		protected MessageEncodingBindingElement(MessageEncodingBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
		}

		// Token: 0x17001835 RID: 6197
		// (get) Token: 0x06006441 RID: 25665
		// (set) Token: 0x06006442 RID: 25666
		[__DynamicallyInvokable]
		public abstract MessageVersion MessageVersion { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

		// Token: 0x17001836 RID: 6198
		// (get) Token: 0x06006443 RID: 25667 RVA: 0x001764F1 File Offset: 0x001746F1
		internal virtual bool IsWsdlExportable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006444 RID: 25668 RVA: 0x001764F4 File Offset: 0x001746F4
		internal IChannelFactory<TChannel> InternalBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("context"));
			}
			context.BindingParameters.Add(this);
			return context.BuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06006445 RID: 25669 RVA: 0x00176520 File Offset: 0x00174720
		internal bool InternalCanBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("context"));
			}
			context.BindingParameters.Add(this);
			return context.CanBuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06006446 RID: 25670 RVA: 0x0017654C File Offset: 0x0017474C
		internal IChannelListener<TChannel> InternalBuildChannelListener<TChannel>(BindingContext context) where TChannel : class, IChannel
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("context"));
			}
			context.BindingParameters.Add(this);
			return context.BuildInnerChannelListener<TChannel>();
		}

		// Token: 0x06006447 RID: 25671 RVA: 0x00176578 File Offset: 0x00174778
		internal bool InternalCanBuildChannelListener<TChannel>(BindingContext context) where TChannel : class, IChannel
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("context"));
			}
			context.BindingParameters.Add(this);
			return context.CanBuildInnerChannelListener<TChannel>();
		}

		// Token: 0x06006448 RID: 25672
		[__DynamicallyInvokable]
		public abstract MessageEncoderFactory CreateMessageEncoderFactory();

		// Token: 0x06006449 RID: 25673 RVA: 0x001765A4 File Offset: 0x001747A4
		[__DynamicallyInvokable]
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(MessageVersion))
			{
				return (T)((object)this.MessageVersion);
			}
			return context.GetInnerProperty<T>();
		}

		// Token: 0x0600644A RID: 25674 RVA: 0x001765F1 File Offset: 0x001747F1
		internal virtual bool CheckEncodingVersion(EnvelopeVersion version)
		{
			return false;
		}

		// Token: 0x0600644B RID: 25675 RVA: 0x001765F4 File Offset: 0x001747F4
		internal override bool IsMatch(BindingElement b)
		{
			return b != null && b is MessageEncodingBindingElement;
		}
	}
}
