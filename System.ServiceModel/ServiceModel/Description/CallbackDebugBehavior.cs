using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x02000430 RID: 1072
	public class CallbackDebugBehavior : IEndpointBehavior
	{
		// Token: 0x060029B9 RID: 10681 RVA: 0x000A1095 File Offset: 0x0009F295
		public CallbackDebugBehavior(bool includeExceptionDetailInFaults)
		{
			this.includeExceptionDetailInFaults = includeExceptionDetailInFaults;
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x060029BA RID: 10682 RVA: 0x000A10A4 File Offset: 0x0009F2A4
		// (set) Token: 0x060029BB RID: 10683 RVA: 0x000A10AC File Offset: 0x0009F2AC
		public bool IncludeExceptionDetailInFaults
		{
			get
			{
				return this.includeExceptionDetailInFaults;
			}
			set
			{
				this.includeExceptionDetailInFaults = value;
			}
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x000A10B5 File Offset: 0x0009F2B5
		void IEndpointBehavior.Validate(ServiceEndpoint serviceEndpoint)
		{
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x000A10B7 File Offset: 0x0009F2B7
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000A10B9 File Offset: 0x0009F2B9
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFXEndpointBehaviorUsedOnWrongSide", new object[]
			{
				typeof(CallbackDebugBehavior).Name
			})));
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x000A10EC File Offset: 0x0009F2EC
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
		{
			ChannelDispatcher channelDispatcher = behavior.CallbackDispatchRuntime.ChannelDispatcher;
			if (channelDispatcher != null && this.includeExceptionDetailInFaults)
			{
				channelDispatcher.IncludeExceptionDetailInFaults = true;
			}
		}

		// Token: 0x0400229C RID: 8860
		private bool includeExceptionDetailInFaults;
	}
}
