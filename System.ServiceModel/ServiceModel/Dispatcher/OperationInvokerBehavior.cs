using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000548 RID: 1352
	internal class OperationInvokerBehavior : IOperationBehavior
	{
		// Token: 0x0600336A RID: 13162 RVA: 0x000C6AA1 File Offset: 0x000C4CA1
		void IOperationBehavior.Validate(OperationDescription description)
		{
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x000C6AA3 File Offset: 0x000C4CA3
		void IOperationBehavior.AddBindingParameters(OperationDescription description, BindingParameterCollection parameters)
		{
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x000C6AA8 File Offset: 0x000C4CA8
		void IOperationBehavior.ApplyDispatchBehavior(OperationDescription description, DispatchOperation dispatch)
		{
			if (dispatch == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dispatch");
			}
			if (description == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("description");
			}
			if (description.TaskMethod != null)
			{
				dispatch.Invoker = new TaskMethodInvoker(description.TaskMethod, description.TaskTResult);
				return;
			}
			if (!(description.SyncMethod != null))
			{
				if (description.BeginMethod != null)
				{
					dispatch.Invoker = new AsyncMethodInvoker(description.BeginMethod, description.EndMethod);
				}
				return;
			}
			if (!(description.BeginMethod != null))
			{
				dispatch.Invoker = new SyncMethodInvoker(description.SyncMethod);
				return;
			}
			OperationBehaviorAttribute operationBehaviorAttribute = description.Behaviors.Find<OperationBehaviorAttribute>();
			if (operationBehaviorAttribute != null && operationBehaviorAttribute.PreferAsyncInvocation)
			{
				dispatch.Invoker = new AsyncMethodInvoker(description.BeginMethod, description.EndMethod);
				return;
			}
			dispatch.Invoker = new SyncMethodInvoker(description.SyncMethod);
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x000C6B95 File Offset: 0x000C4D95
		void IOperationBehavior.ApplyClientBehavior(OperationDescription description, ClientOperation proxy)
		{
		}
	}
}
