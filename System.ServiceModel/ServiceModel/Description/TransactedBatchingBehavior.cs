using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x020003BE RID: 958
	public class TransactedBatchingBehavior : IEndpointBehavior
	{
		// Token: 0x060023D9 RID: 9177 RVA: 0x0008271F File Offset: 0x0008091F
		public TransactedBatchingBehavior(int maxBatchSize)
		{
			if (maxBatchSize < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("maxBatchSize", maxBatchSize, SR.GetString("ValueMustBeNonNegative")));
			}
			this.maxBatchSize = maxBatchSize;
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x060023DA RID: 9178 RVA: 0x00082757 File Offset: 0x00080957
		// (set) Token: 0x060023DB RID: 9179 RVA: 0x0008275F File Offset: 0x0008095F
		public int MaxBatchSize
		{
			get
			{
				return this.maxBatchSize;
			}
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeNonNegative")));
				}
				this.maxBatchSize = value;
			}
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x00082794 File Offset: 0x00080994
		void IEndpointBehavior.Validate(ServiceEndpoint serviceEndpoint)
		{
			BindingElementCollection bindingElementCollection = serviceEndpoint.Binding.CreateBindingElements();
			bool flag = false;
			foreach (BindingElement bindingElement in bindingElementCollection)
			{
				ITransactedBindingElement transactedBindingElement = bindingElement as ITransactedBindingElement;
				if (transactedBindingElement != null && transactedBindingElement.TransactedReceiveEnabled)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SfxTransactedBindingNeeded")));
			}
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x0008281C File Offset: 0x00080A1C
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x00082820 File Offset: 0x00080A20
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
		{
			if (endpointDispatcher.DispatchRuntime.ReleaseServiceInstanceOnTransactionComplete)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxNoBatchingForReleaseOnComplete")));
			}
			if (serviceEndpoint.Contract.SessionMode == SessionMode.Required)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxNoBatchingForSession")));
			}
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x0008287C File Offset: 0x00080A7C
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
		{
			if (serviceEndpoint.Contract.SessionMode == SessionMode.Required)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxNoBatchingForSession")));
			}
			behavior.CallbackDispatchRuntime.ChannelDispatcher.MaxTransactedBatchSize = this.MaxBatchSize;
		}

		// Token: 0x0400202C RID: 8236
		private int maxBatchSize;
	}
}
