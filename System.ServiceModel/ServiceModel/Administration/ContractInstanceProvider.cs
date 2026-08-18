using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.ServiceModel.Description;

namespace System.ServiceModel.Administration
{
	// Token: 0x02000446 RID: 1094
	internal class ContractInstanceProvider : ProviderBase, IWmiProvider
	{
		// Token: 0x06002A92 RID: 10898 RVA: 0x000A4944 File Offset: 0x000A2B44
		internal static string ContractReference(string contractName)
		{
			return string.Format(CultureInfo.InvariantCulture, "Contract.Name='{0}',ProcessId={1},AppDomainId={2}", new object[]
			{
				contractName,
				AppDomainInfo.Current.ProcessId,
				AppDomainInfo.Current.Id
			});
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x000A4984 File Offset: 0x000A2B84
		internal static void RegisterContract(ContractDescription contract)
		{
			Dictionary<string, ContractDescription> obj = ContractInstanceProvider.knownContracts;
			lock (obj)
			{
				if (!ContractInstanceProvider.knownContracts.ContainsKey(contract.Name))
				{
					ContractInstanceProvider.knownContracts.Add(contract.Name, contract);
				}
			}
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x000A49E0 File Offset: 0x000A2BE0
		private static void FillContract(IWmiInstance contract, ContractDescription contractDescription)
		{
			contract.SetProperty("Type", contractDescription.ContractType.Name);
			if (null != contractDescription.CallbackContractType)
			{
				contract.SetProperty("CallbackContract", ContractInstanceProvider.ContractReference(contractDescription.CallbackContractType.Name));
			}
			contract.SetProperty("Name", contractDescription.Name);
			contract.SetProperty("Namespace", contractDescription.Namespace);
			contract.SetProperty("SessionMode", contractDescription.SessionMode.ToString());
			IWmiInstance[] array = new IWmiInstance[contractDescription.Operations.Count];
			for (int i = 0; i < array.Length; i++)
			{
				OperationDescription operationDescription = contractDescription.Operations[i];
				IWmiInstance wmiInstance = contract.NewInstance("Operation");
				ContractInstanceProvider.FillOperation(wmiInstance, operationDescription);
				array[i] = wmiInstance;
			}
			contract.SetProperty("Operations", array);
			ContractInstanceProvider.FillBehaviorsInfo(contract, contractDescription.Behaviors);
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x000A4ACC File Offset: 0x000A2CCC
		private static void FillOperation(IWmiInstance operation, OperationDescription operationDescription)
		{
			operation.SetProperty("Name", operationDescription.Name);
			operation.SetProperty("Action", ContractInstanceProvider.FixWildcardAction(operationDescription.Messages[0].Action));
			if (operationDescription.Messages.Count > 1)
			{
				operation.SetProperty("ReplyAction", ContractInstanceProvider.FixWildcardAction(operationDescription.Messages[1].Action));
			}
			operation.SetProperty("IsOneWay", operationDescription.IsOneWay);
			operation.SetProperty("IsInitiating", operationDescription.IsInitiating);
			operation.SetProperty("IsTerminating", operationDescription.IsTerminating);
			operation.SetProperty("AsyncPattern", null != operationDescription.BeginMethod);
			if (null != operationDescription.SyncMethod)
			{
				if (null != operationDescription.SyncMethod.ReturnType)
				{
					operation.SetProperty("ReturnType", operationDescription.SyncMethod.ReturnType.Name);
				}
				operation.SetProperty("MethodSignature", operationDescription.SyncMethod.ToString());
				ParameterInfo[] parameters = operationDescription.SyncMethod.GetParameters();
				string[] array = new string[parameters.Length];
				for (int i = 0; i < parameters.Length; i++)
				{
					array[i] = parameters[i].ParameterType.ToString();
				}
				operation.SetProperty("ParameterTypes", array);
			}
			operation.SetProperty("IsCallback", operationDescription.Messages[0].Direction == MessageDirection.Output);
			ContractInstanceProvider.FillBehaviorsInfo(operation, operationDescription.Behaviors);
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x000A4C5C File Offset: 0x000A2E5C
		private static void FillBehaviorsInfo(IWmiInstance operation, KeyedByTypeCollection<IOperationBehavior> behaviors)
		{
			List<IWmiInstance> list = new List<IWmiInstance>(behaviors.Count);
			foreach (IOperationBehavior behavior in behaviors)
			{
				IWmiInstance wmiInstance;
				ContractInstanceProvider.FillBehaviorInfo(behavior, operation, out wmiInstance);
				if (wmiInstance != null)
				{
					list.Add(wmiInstance);
				}
			}
			operation.SetProperty("Behaviors", list.ToArray());
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x000A4CD0 File Offset: 0x000A2ED0
		private static void FillBehaviorsInfo(IWmiInstance operation, KeyedByTypeCollection<IContractBehavior> behaviors)
		{
			List<IWmiInstance> list = new List<IWmiInstance>(behaviors.Count);
			foreach (IContractBehavior behavior in behaviors)
			{
				IWmiInstance wmiInstance;
				ContractInstanceProvider.FillBehaviorInfo(behavior, operation, out wmiInstance);
				if (wmiInstance != null)
				{
					list.Add(wmiInstance);
				}
			}
			operation.SetProperty("Behaviors", list.ToArray());
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x000A4D44 File Offset: 0x000A2F44
		private static void FillBehaviorInfo(IContractBehavior behavior, IWmiInstance existingInstance, out IWmiInstance instance)
		{
			instance = null;
			if (behavior is DeliveryRequirementsAttribute)
			{
				instance = existingInstance.NewInstance("DeliveryRequirementsAttribute");
				DeliveryRequirementsAttribute deliveryRequirementsAttribute = (DeliveryRequirementsAttribute)behavior;
				instance.SetProperty("QueuedDeliveryRequirements", deliveryRequirementsAttribute.QueuedDeliveryRequirements.ToString());
				instance.SetProperty("RequireOrderedDelivery", deliveryRequirementsAttribute.RequireOrderedDelivery);
				if (null != deliveryRequirementsAttribute.TargetContract)
				{
					instance.SetProperty("TargetContract", deliveryRequirementsAttribute.TargetContract.ToString());
				}
			}
			else if (behavior is IWmiInstanceProvider)
			{
				IWmiInstanceProvider wmiInstanceProvider = (IWmiInstanceProvider)behavior;
				instance = existingInstance.NewInstance(wmiInstanceProvider.GetInstanceType());
				wmiInstanceProvider.FillInstance(instance);
			}
			else
			{
				instance = existingInstance.NewInstance("Behavior");
			}
			if (instance != null)
			{
				instance.SetProperty("Type", behavior.GetType().FullName);
			}
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x000A4E20 File Offset: 0x000A3020
		private static void FillBehaviorInfo(IOperationBehavior behavior, IWmiInstance existingInstance, out IWmiInstance instance)
		{
			instance = null;
			if (behavior is DataContractSerializerOperationBehavior)
			{
				instance = existingInstance.NewInstance("DataContractSerializerOperationBehavior");
				DataContractSerializerOperationBehavior dataContractSerializerOperationBehavior = (DataContractSerializerOperationBehavior)behavior;
				instance.SetProperty("IgnoreExtensionDataObject", dataContractSerializerOperationBehavior.IgnoreExtensionDataObject);
				instance.SetProperty("MaxItemsInObjectGraph", dataContractSerializerOperationBehavior.MaxItemsInObjectGraph);
				if (dataContractSerializerOperationBehavior.DataContractFormatAttribute != null)
				{
					instance.SetProperty("Style", dataContractSerializerOperationBehavior.DataContractFormatAttribute.Style.ToString());
				}
			}
			else if (behavior is OperationBehaviorAttribute)
			{
				instance = existingInstance.NewInstance("OperationBehaviorAttribute");
				OperationBehaviorAttribute operationBehaviorAttribute = (OperationBehaviorAttribute)behavior;
				instance.SetProperty("AutoDisposeParameters", operationBehaviorAttribute.AutoDisposeParameters);
				instance.SetProperty("Impersonation", operationBehaviorAttribute.Impersonation.ToString());
				instance.SetProperty("ReleaseInstanceMode", operationBehaviorAttribute.ReleaseInstanceMode.ToString());
				instance.SetProperty("TransactionAutoComplete", operationBehaviorAttribute.TransactionAutoComplete);
				instance.SetProperty("TransactionScopeRequired", operationBehaviorAttribute.TransactionScopeRequired);
			}
			else if (behavior is TransactionFlowAttribute)
			{
				instance = existingInstance.NewInstance("TransactionFlowAttribute");
				TransactionFlowAttribute transactionFlowAttribute = (TransactionFlowAttribute)behavior;
				instance.SetProperty("TransactionFlowOption", transactionFlowAttribute.Transactions.ToString());
			}
			else if (behavior is XmlSerializerOperationBehavior)
			{
				instance = existingInstance.NewInstance("XmlSerializerOperationBehavior");
				XmlSerializerOperationBehavior xmlSerializerOperationBehavior = (XmlSerializerOperationBehavior)behavior;
				if (xmlSerializerOperationBehavior.XmlSerializerFormatAttribute != null)
				{
					instance.SetProperty("Style", xmlSerializerOperationBehavior.XmlSerializerFormatAttribute.Style.ToString());
					instance.SetProperty("Use", xmlSerializerOperationBehavior.XmlSerializerFormatAttribute.Use.ToString());
					instance.SetProperty("SupportFaults", xmlSerializerOperationBehavior.XmlSerializerFormatAttribute.SupportFaults.ToString());
				}
			}
			else if (behavior is IWmiInstanceProvider)
			{
				IWmiInstanceProvider wmiInstanceProvider = (IWmiInstanceProvider)behavior;
				instance = existingInstance.NewInstance(wmiInstanceProvider.GetInstanceType());
				wmiInstanceProvider.FillInstance(instance);
			}
			else
			{
				instance = existingInstance.NewInstance("Behavior");
			}
			if (instance != null)
			{
				instance.SetProperty("Type", behavior.GetType().FullName);
			}
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x000A508B File Offset: 0x000A328B
		private static string FixWildcardAction(string action)
		{
			if (action == null)
			{
				return "*";
			}
			return action;
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x000A5098 File Offset: 0x000A3298
		private static void UpdateContracts()
		{
			foreach (ServiceInfo serviceInfo in new ServiceInfoCollection(ManagementExtension.Services))
			{
				foreach (EndpointInfo endpointInfo in serviceInfo.Endpoints)
				{
					ContractInstanceProvider.RegisterContract(endpointInfo.Contract);
				}
			}
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x000A5124 File Offset: 0x000A3324
		void IWmiProvider.EnumInstances(IWmiInstances instances)
		{
			int processId = AppDomainInfo.Current.ProcessId;
			int id = AppDomainInfo.Current.Id;
			Dictionary<string, ContractDescription> obj = ContractInstanceProvider.knownContracts;
			lock (obj)
			{
				ContractInstanceProvider.UpdateContracts();
				foreach (ContractDescription contractDescription in ContractInstanceProvider.knownContracts.Values)
				{
					IWmiInstance wmiInstance = instances.NewInstance(null);
					wmiInstance.SetProperty("ProcessId", processId);
					wmiInstance.SetProperty("AppDomainId", id);
					ContractInstanceProvider.FillContract(wmiInstance, contractDescription);
					instances.AddInstance(wmiInstance);
				}
			}
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x000A51F8 File Offset: 0x000A33F8
		bool IWmiProvider.GetInstance(IWmiInstance contract)
		{
			bool result = false;
			if ((int)contract.GetProperty("ProcessId") == AppDomainInfo.Current.ProcessId && (int)contract.GetProperty("AppDomainId") == AppDomainInfo.Current.Id)
			{
				string key = (string)contract.GetProperty("Name");
				ContractInstanceProvider.UpdateContracts();
				ContractDescription contractDescription;
				if (ContractInstanceProvider.knownContracts.TryGetValue(key, out contractDescription))
				{
					result = true;
					ContractInstanceProvider.FillContract(contract, contractDescription);
				}
			}
			return result;
		}

		// Token: 0x040023F5 RID: 9205
		private static Dictionary<string, ContractDescription> knownContracts = new Dictionary<string, ContractDescription>();
	}
}
