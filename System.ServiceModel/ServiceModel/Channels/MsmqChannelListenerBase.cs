using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008E6 RID: 2278
	internal abstract class MsmqChannelListenerBase : TransportChannelListener
	{
		// Token: 0x060056DD RID: 22237 RVA: 0x0013EDDB File Offset: 0x0013CFDB
		protected MsmqChannelListenerBase(MsmqBindingElementBase bindingElement, BindingContext context, MsmqReceiveParameters receiveParameters, MessageEncoderFactory messageEncoderFactory) : base(bindingElement, context, messageEncoderFactory)
		{
			this.receiveParameters = receiveParameters;
		}

		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x060056DE RID: 22238 RVA: 0x0013EDEE File Offset: 0x0013CFEE
		internal MsmqReceiveParameters ReceiveParameters
		{
			get
			{
				return this.receiveParameters;
			}
		}

		// Token: 0x060056DF RID: 22239 RVA: 0x0013EDF6 File Offset: 0x0013CFF6
		internal Exception NormalizePoisonException(long lookupId, Exception innerException)
		{
			if (this.ReceiveParameters.ExactlyOnce)
			{
				return DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqPoisonMessageException(lookupId, innerException));
			}
			if (innerException != null)
			{
				return DiagnosticUtility.ExceptionUtility.ThrowHelperError(innerException);
			}
			throw Fx.AssertAndThrow("System.ServiceModel.Channels.MsmqChannelListenerBase.NormalizePoisonException(): (innerException == null)");
		}

		// Token: 0x060056E0 RID: 22240 RVA: 0x0013EE30 File Offset: 0x0013D030
		internal void FaultListener()
		{
			base.Fault();
		}

		// Token: 0x0400358F RID: 13711
		private MsmqReceiveParameters receiveParameters;
	}
}
