using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000216 RID: 534
	internal class ComPlusTypeLoader : IContractResolver
	{
		// Token: 0x0600103E RID: 4158 RVA: 0x0003A8AD File Offset: 0x00038AAD
		public ComPlusTypeLoader(ServiceInfo info)
		{
			this.info = info;
			this.transactionFlow = (info.TransactionOption == TransactionOption.Required || info.TransactionOption == TransactionOption.Supported);
			this.interfaceResolver = new TypeCacheManager();
			this.contracts = new Dictionary<Guid, ContractDescription>();
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0003A8F0 File Offset: 0x00038AF0
		private void ValidateInterface(Guid iid)
		{
			if (!ComPlusTypeValidator.IsValidInterface(iid))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("InvalidWebServiceInterface", new object[]
				{
					iid
				})));
			}
			bool flag = false;
			foreach (ContractInfo contractInfo in this.info.Contracts)
			{
				if (contractInfo.IID == iid)
				{
					if (contractInfo.Operations.Count == 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("RequireConfiguredMethods", new object[]
						{
							iid
						})));
					}
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("RequireConfiguredInterfaces", new object[]
				{
					iid
				})));
			}
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0003A9E8 File Offset: 0x00038BE8
		private ContractDescription CreateContractDescriptionInternal(Guid iid, Type type)
		{
			ComContractElement comContractElement = ConfigLoader.LookupComContract(iid);
			if (comContractElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("InterfaceNotFoundInConfig", new object[]
				{
					iid
				})));
			}
			if (string.IsNullOrEmpty(comContractElement.Name) || string.IsNullOrEmpty(comContractElement.Namespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("CannotHaveNullOrEmptyNameOrNamespaceForIID", new object[]
				{
					iid
				})));
			}
			ContractDescription contractDescription = new ContractDescription(comContractElement.Name, comContractElement.Namespace);
			contractDescription.ContractType = type;
			contractDescription.SessionMode = (comContractElement.RequiresSession ? SessionMode.Required : SessionMode.Allowed);
			List<Guid> list = new List<Guid>();
			foreach (object obj in comContractElement.PersistableTypes)
			{
				ComPersistableTypeElement comPersistableTypeElement = (ComPersistableTypeElement)obj;
				Guid item = Fx.CreateGuid(comPersistableTypeElement.ID);
				list.Add(item);
			}
			IDataContractSurrogate dataContractSurrogate = null;
			if (list.Count > 0 || comContractElement.PersistableTypes.EmitClear)
			{
				dataContractSurrogate = new DataContractSurrogateForPersistWrapper(list.ToArray());
			}
			foreach (object obj2 in comContractElement.ExposedMethods)
			{
				ComMethodElement comMethodElement = (ComMethodElement)obj2;
				bool flag = false;
				foreach (MethodInfo methodInfo in type.GetMethods())
				{
					if (methodInfo.Name == comMethodElement.ExposedMethod)
					{
						OperationDescription operationDescription = this.CreateOperationDescription(contractDescription, methodInfo, comContractElement, dataContractSurrogate != null);
						this.ConfigureOperationDescriptionBehaviors(operationDescription, dataContractSurrogate);
						contractDescription.Operations.Add(operationDescription);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("MethodGivenInConfigNotFoundOnInterface", new object[]
					{
						comMethodElement.ExposedMethod,
						iid
					})));
				}
			}
			if (contractDescription.Operations.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("NoneOfTheMethodsForInterfaceFoundInConfig", new object[]
				{
					iid
				})));
			}
			this.ConfigureContractDescriptionBehaviors(contractDescription);
			return contractDescription;
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0003AC50 File Offset: 0x00038E50
		private void ConfigureContractDescriptionBehaviors(ContractDescription contract)
		{
			contract.Behaviors.Add(new OperationSelectorBehavior());
			ComPlusContractBehavior item = new ComPlusContractBehavior(this.info);
			contract.Behaviors.Add(item);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0003AC88 File Offset: 0x00038E88
		private void ConfigureOperationDescriptionBehaviors(OperationDescription operation, IDataContractSurrogate contractSurrogate)
		{
			DataContractSerializerOperationBehavior dataContractSerializerOperationBehavior = new DataContractSerializerOperationBehavior(operation, TypeLoader.DefaultDataContractFormatAttribute);
			if (contractSurrogate != null)
			{
				dataContractSerializerOperationBehavior.DataContractSurrogate = contractSurrogate;
			}
			operation.Behaviors.Add(dataContractSerializerOperationBehavior);
			operation.Behaviors.Add(new OperationInvokerBehavior());
			if (this.info.TransactionOption == TransactionOption.Supported || this.info.TransactionOption == TransactionOption.Required)
			{
				operation.Behaviors.Add(new TransactionFlowAttribute(TransactionFlowOption.Allowed));
			}
			OperationBehaviorAttribute operationBehaviorAttribute = new OperationBehaviorAttribute();
			operationBehaviorAttribute.TransactionAutoComplete = true;
			operationBehaviorAttribute.TransactionScopeRequired = false;
			operation.Behaviors.Add(operationBehaviorAttribute);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0003AD14 File Offset: 0x00038F14
		private OperationDescription CreateOperationDescription(ContractDescription contract, MethodInfo methodInfo, ComContractElement config, bool allowReferences)
		{
			XmlName xmlName = new XmlName(ServiceReflector.GetLogicalName(methodInfo));
			XmlName returnValueName = TypeLoader.GetReturnValueName(xmlName);
			if (ServiceReflector.IsBegin(methodInfo) || ServiceReflector.IsTask(methodInfo))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.NoAsyncOperationsAllowed());
			}
			if (contract.Operations.FindAll(xmlName.EncodedName).Count != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.DuplicateOperation());
			}
			OperationDescription operationDescription = new OperationDescription(xmlName.EncodedName, contract);
			operationDescription.SyncMethod = methodInfo;
			operationDescription.IsInitiating = true;
			operationDescription.IsTerminating = false;
			operationDescription.KnownTypes.Add(typeof(Array));
			operationDescription.KnownTypes.Add(typeof(DBNull));
			operationDescription.KnownTypes.Add(typeof(CurrencyWrapper));
			operationDescription.KnownTypes.Add(typeof(ErrorWrapper));
			if (allowReferences)
			{
				operationDescription.KnownTypes.Add(typeof(PersistStreamTypeWrapper));
			}
			foreach (object obj in config.UserDefinedTypes)
			{
				ComUdtElement comUdtElement = (ComUdtElement)obj;
				Guid guid = Fx.CreateGuid(comUdtElement.TypeLibID);
				Type type;
				TypeCacheManager.Provider.FindOrCreateType(guid, comUdtElement.TypeLibVersion, Fx.CreateGuid(comUdtElement.TypeDefID), out type, false);
				this.info.AddUdt(type, guid);
				operationDescription.KnownTypes.Add(type);
			}
			string @namespace = contract.Namespace;
			XmlQualifiedName contractName = new XmlQualifiedName(contract.Name, @namespace);
			string messageAction = NamingHelper.GetMessageAction(contractName, xmlName.DecodedName, null, false);
			string messageAction2 = NamingHelper.GetMessageAction(contractName, xmlName.DecodedName, null, true);
			MessageDescription item = this.CreateIncomingMessageDescription(contract, methodInfo, @namespace, messageAction, allowReferences);
			MessageDescription item2 = this.CreateOutgoingMessageDescription(contract, methodInfo, returnValueName, @namespace, messageAction2, allowReferences);
			operationDescription.Messages.Add(item);
			operationDescription.Messages.Add(item2);
			return operationDescription;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0003AF18 File Offset: 0x00039118
		private MessageDescription CreateIncomingMessageDescription(ContractDescription contract, MethodInfo methodInfo, string ns, string action, bool allowReferences)
		{
			ParameterInfo[] inputParameters = ServiceReflector.GetInputParameters(methodInfo, false);
			return this.CreateParameterMessageDescription(contract, inputParameters, null, null, null, methodInfo.Name, ns, action, MessageDirection.Input, allowReferences);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0003AF44 File Offset: 0x00039144
		private MessageDescription CreateOutgoingMessageDescription(ContractDescription contract, MethodInfo methodInfo, XmlName returnValueName, string ns, string action, bool allowReferences)
		{
			ParameterInfo[] outputParameters = ServiceReflector.GetOutputParameters(methodInfo, false);
			return this.CreateParameterMessageDescription(contract, outputParameters, methodInfo.ReturnType, methodInfo.ReturnTypeCustomAttributes, returnValueName, methodInfo.Name, ns, action, MessageDirection.Output, allowReferences);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0003AF7C File Offset: 0x0003917C
		private MessageDescription CreateParameterMessageDescription(ContractDescription contract, ParameterInfo[] parameters, Type returnType, ICustomAttributeProvider returnCustomAttributes, XmlName returnValueName, string methodName, string ns, string action, MessageDirection direction, bool allowReferences)
		{
			MessageDescription messageDescription = new MessageDescription(action, direction);
			messageDescription.Body.WrapperNamespace = ns;
			for (int i = 0; i < parameters.Length; i++)
			{
				ParameterInfo parameterInfo = parameters[i];
				Type parameterType = TypeLoader.GetParameterType(parameterInfo);
				if (!ComPlusTypeValidator.IsValidParameter(parameterType, parameterInfo, allowReferences))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("InvalidWebServiceParameter", new object[]
					{
						parameterInfo.Name,
						parameterType.Name,
						methodName,
						contract.Name
					})));
				}
				MessagePartDescription item = this.CreateMessagePartDescription(parameterType, new XmlName(parameterInfo.Name), ns, i);
				messageDescription.Body.Parts.Add(item);
			}
			XmlName xmlName = new XmlName(methodName);
			if (returnType == null)
			{
				messageDescription.Body.WrapperName = xmlName.EncodedName;
			}
			else
			{
				messageDescription.Body.WrapperName = TypeLoader.GetBodyWrapperResponseName(xmlName).EncodedName;
				if (!ComPlusTypeValidator.IsValidParameter(returnType, returnCustomAttributes, allowReferences))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("InvalidWebServiceReturnValue", new object[]
					{
						returnType.Name,
						methodName,
						contract.Name
					})));
				}
				MessagePartDescription returnValue = this.CreateMessagePartDescription(returnType, returnValueName, ns, 0);
				messageDescription.Body.ReturnValue = returnValue;
			}
			return messageDescription;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0003B0D0 File Offset: 0x000392D0
		private MessagePartDescription CreateMessagePartDescription(Type bodyType, XmlName name, string ns, int index)
		{
			return new MessagePartDescription(name.EncodedName, ns)
			{
				SerializationPosition = index,
				MemberInfo = null,
				Type = bodyType,
				Index = index
			};
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0003B10C File Offset: 0x0003930C
		private ContractDescription ResolveIMetadataExchangeToContract()
		{
			TypeLoader typeLoader = new TypeLoader();
			return typeLoader.LoadContractDescription(typeof(IMetadataExchange));
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0003B130 File Offset: 0x00039330
		public ContractDescription ResolveContract(string contractTypeString)
		{
			Guid guid;
			if ("IMetadataExchange" == contractTypeString)
			{
				guid = typeof(IMetadataExchange).GUID;
			}
			else
			{
				if (!DiagnosticUtility.Utility.TryCreateGuid(contractTypeString, out guid))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(SR.GetString("ContractTypeNotAnIID", new object[]
					{
						contractTypeString
					})));
				}
				this.ValidateInterface(guid);
			}
			ContractDescription contractDescription;
			if (this.contracts.TryGetValue(guid, out contractDescription))
			{
				return contractDescription;
			}
			if (guid != typeof(IMetadataExchange).GUID)
			{
				Type type;
				try
				{
					this.interfaceResolver.FindOrCreateType(this.info.ServiceType, guid, out type, false, true);
				}
				catch (InvalidOperationException ex)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.ListenerInitFailed(ex.Message));
				}
				contractDescription = this.CreateContractDescriptionInternal(guid, type);
			}
			else
			{
				contractDescription = this.ResolveIMetadataExchangeToContract();
			}
			this.contracts.Add(guid, contractDescription);
			ComPlusServiceHostTrace.Trace(TraceEventType.Verbose, 327683, "TraceCodeComIntegrationServiceHostCreatedServiceContract", this.info, contractDescription);
			return contractDescription;
		}

		// Token: 0x04001867 RID: 6247
		private ServiceInfo info;

		// Token: 0x04001868 RID: 6248
		private bool transactionFlow;

		// Token: 0x04001869 RID: 6249
		private ITypeCacheManager interfaceResolver;

		// Token: 0x0400186A RID: 6250
		private Dictionary<Guid, ContractDescription> contracts;
	}
}
