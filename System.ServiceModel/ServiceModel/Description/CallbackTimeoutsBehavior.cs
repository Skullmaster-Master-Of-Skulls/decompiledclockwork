using System;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x02000431 RID: 1073
	internal class CallbackTimeoutsBehavior : IEndpointBehavior
	{
		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x060029C0 RID: 10688 RVA: 0x000A1117 File Offset: 0x0009F317
		// (set) Token: 0x060029C1 RID: 10689 RVA: 0x000A1120 File Offset: 0x0009F320
		public TimeSpan TransactionTimeout
		{
			get
			{
				return this.transactionTimeout;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRange0")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.transactionTimeout = value;
			}
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x000A11A6 File Offset: 0x0009F3A6
		void IEndpointBehavior.Validate(ServiceEndpoint serviceEndpoint)
		{
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x000A11A8 File Offset: 0x0009F3A8
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x000A11AA File Offset: 0x0009F3AA
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFXEndpointBehaviorUsedOnWrongSide", new object[]
			{
				typeof(CallbackTimeoutsBehavior).Name
			})));
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x000A11E0 File Offset: 0x0009F3E0
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
		{
			if (this.transactionTimeout != TimeSpan.Zero)
			{
				ChannelDispatcher channelDispatcher = behavior.CallbackDispatchRuntime.ChannelDispatcher;
				if ((channelDispatcher != null && channelDispatcher.TransactionTimeout == TimeSpan.Zero) || channelDispatcher.TransactionTimeout > this.transactionTimeout)
				{
					channelDispatcher.TransactionTimeout = this.transactionTimeout;
				}
			}
		}

		// Token: 0x0400229D RID: 8861
		private TimeSpan transactionTimeout = TimeSpan.Zero;
	}
}
