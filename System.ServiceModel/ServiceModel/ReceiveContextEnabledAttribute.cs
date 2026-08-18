using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel
{
	// Token: 0x020000DE RID: 222
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class ReceiveContextEnabledAttribute : Attribute, IOperationBehavior
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00015D29 File Offset: 0x00013F29
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x00015D31 File Offset: 0x00013F31
		public bool ManualControl { get; set; }

		// Token: 0x0600042D RID: 1069 RVA: 0x00015D3A File Offset: 0x00013F3A
		public void Validate(OperationDescription operationDescription)
		{
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00015D3C File Offset: 0x00013F3C
		public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
			if (operationDescription == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("operationDescription");
			}
			if (dispatchOperation == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dispatchOperation");
			}
			if (this.ManualControl)
			{
				dispatchOperation.ReceiveContextAcknowledgementMode = ReceiveContextAcknowledgementMode.ManualAcknowledgement;
				return;
			}
			dispatchOperation.ReceiveContextAcknowledgementMode = ReceiveContextAcknowledgementMode.AutoAcknowledgeOnRPCComplete;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00015D7B File Offset: 0x00013F7B
		public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00015D7D File Offset: 0x00013F7D
		public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{
		}
	}
}
