using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000549 RID: 1353
	internal class OperationSelectorBehavior : IContractBehavior
	{
		// Token: 0x0600336E RID: 13166 RVA: 0x000C6B97 File Offset: 0x000C4D97
		void IContractBehavior.Validate(ContractDescription description, ServiceEndpoint endpoint)
		{
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x000C6B99 File Offset: 0x000C4D99
		void IContractBehavior.AddBindingParameters(ContractDescription description, ServiceEndpoint endpoint, BindingParameterCollection parameters)
		{
		}

		// Token: 0x06003370 RID: 13168 RVA: 0x000C6B9B File Offset: 0x000C4D9B
		void IContractBehavior.ApplyDispatchBehavior(ContractDescription description, ServiceEndpoint endpoint, DispatchRuntime dispatch)
		{
			if (dispatch.ClientRuntime != null)
			{
				dispatch.ClientRuntime.OperationSelector = new OperationSelectorBehavior.MethodInfoOperationSelector(description, MessageDirection.Output);
			}
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x000C6BB7 File Offset: 0x000C4DB7
		void IContractBehavior.ApplyClientBehavior(ContractDescription description, ServiceEndpoint endpoint, ClientRuntime proxy)
		{
			proxy.OperationSelector = new OperationSelectorBehavior.MethodInfoOperationSelector(description, MessageDirection.Input);
		}

		// Token: 0x02000C6E RID: 3182
		internal class MethodInfoOperationSelector : IClientOperationSelector
		{
			// Token: 0x060077F9 RID: 30713 RVA: 0x001C0D94 File Offset: 0x001BEF94
			internal MethodInfoOperationSelector(ContractDescription description, MessageDirection directionThatRequiresClientOpSelection)
			{
				this.operationMap = new Dictionary<object, string>();
				for (int i = 0; i < description.Operations.Count; i++)
				{
					OperationDescription operationDescription = description.Operations[i];
					if (operationDescription.Messages[0].Direction == directionThatRequiresClientOpSelection)
					{
						if (operationDescription.SyncMethod != null && !this.operationMap.ContainsKey(operationDescription.SyncMethod.MethodHandle))
						{
							this.operationMap.Add(operationDescription.SyncMethod.MethodHandle, operationDescription.Name);
						}
						if (operationDescription.BeginMethod != null && !this.operationMap.ContainsKey(operationDescription.BeginMethod.MethodHandle))
						{
							this.operationMap.Add(operationDescription.BeginMethod.MethodHandle, operationDescription.Name);
							this.operationMap.Add(operationDescription.EndMethod.MethodHandle, operationDescription.Name);
						}
						if (operationDescription.TaskMethod != null && !this.operationMap.ContainsKey(operationDescription.TaskMethod.MethodHandle))
						{
							this.operationMap.Add(operationDescription.TaskMethod.MethodHandle, operationDescription.Name);
						}
					}
				}
			}

			// Token: 0x17001B58 RID: 7000
			// (get) Token: 0x060077FA RID: 30714 RVA: 0x001C0EF7 File Offset: 0x001BF0F7
			public bool AreParametersRequiredForSelection
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060077FB RID: 30715 RVA: 0x001C0EFA File Offset: 0x001BF0FA
			public string SelectOperation(MethodBase method, object[] parameters)
			{
				if (this.operationMap.ContainsKey(method.MethodHandle))
				{
					return this.operationMap[method.MethodHandle];
				}
				return null;
			}

			// Token: 0x0400447D RID: 17533
			private Dictionary<object, string> operationMap;
		}
	}
}
