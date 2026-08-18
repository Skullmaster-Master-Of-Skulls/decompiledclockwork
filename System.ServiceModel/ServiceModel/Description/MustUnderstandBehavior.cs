using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x020003F4 RID: 1012
	public class MustUnderstandBehavior : IEndpointBehavior
	{
		// Token: 0x0600261A RID: 9754 RVA: 0x000898EB File Offset: 0x00087AEB
		public MustUnderstandBehavior(bool validate)
		{
			this.validateMustUnderstand = validate;
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x0600261B RID: 9755 RVA: 0x000898FA File Offset: 0x00087AFA
		// (set) Token: 0x0600261C RID: 9756 RVA: 0x00089902 File Offset: 0x00087B02
		public bool ValidateMustUnderstand
		{
			get
			{
				return this.validateMustUnderstand;
			}
			set
			{
				this.validateMustUnderstand = value;
			}
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x0008990B File Offset: 0x00087B0B
		void IEndpointBehavior.Validate(ServiceEndpoint serviceEndpoint)
		{
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x0008990D File Offset: 0x00087B0D
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x0008990F File Offset: 0x00087B0F
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
		{
			if (endpointDispatcher == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("endpointDispatcher"));
			}
			endpointDispatcher.DispatchRuntime.ValidateMustUnderstand = this.ValidateMustUnderstand;
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x0008993A File Offset: 0x00087B3A
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
		{
			if (behavior == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("behavior"));
			}
			behavior.ValidateMustUnderstand = this.ValidateMustUnderstand;
		}

		// Token: 0x0400217B RID: 8571
		private bool validateMustUnderstand;
	}
}
