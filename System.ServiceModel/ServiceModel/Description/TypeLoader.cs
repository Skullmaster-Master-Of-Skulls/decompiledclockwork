using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace System.ServiceModel.Description
{
	// Token: 0x02000425 RID: 1061
	internal class TypeLoader
	{
		// Token: 0x060028DE RID: 10462 RVA: 0x0009999A File Offset: 0x00097B9A
		public TypeLoader()
		{
			this.thisLock = new object();
			this.contracts = new Dictionary<Type, ContractDescription>();
			this.messages = new Dictionary<Type, MessageDescriptionItems>();
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000999C4 File Offset: 0x00097BC4
		private ContractDescription LoadContractDescriptionHelper(Type contractType, Type serviceType, object serviceImplementation)
		{
			ContractDescription contractDescription;
			if (contractType == typeof(IOutputChannel))
			{
				contractDescription = this.LoadOutputChannelContractDescription();
			}
			else if (contractType == typeof(IRequestChannel))
			{
				contractDescription = this.LoadRequestChannelContractDescription();
			}
			else
			{
				ServiceContractAttribute contractAttr;
				Type contractTypeAndAttribute = ServiceReflector.GetContractTypeAndAttribute(contractType, out contractAttr);
				object obj = this.thisLock;
				lock (obj)
				{
					if (!this.contracts.TryGetValue(contractTypeAndAttribute, out contractDescription))
					{
						this.EnsureNoInheritanceWithContractClasses(contractTypeAndAttribute);
						this.EnsureNoOperationContractsOnNonServiceContractTypes(contractTypeAndAttribute);
						TypeLoader.ContractReflectionInfo reflectionInfo;
						contractDescription = this.CreateContractDescription(contractAttr, contractTypeAndAttribute, serviceType, out reflectionInfo, serviceImplementation);
						if (serviceImplementation != null && serviceImplementation is IContractBehavior)
						{
							contractDescription.Behaviors.Add((IContractBehavior)serviceImplementation);
						}
						if (serviceType != null)
						{
							TypeLoader.UpdateContractDescriptionWithAttributesFromServiceType(contractDescription, serviceType);
							foreach (ContractDescription description in contractDescription.GetInheritedContracts())
							{
								TypeLoader.UpdateContractDescriptionWithAttributesFromServiceType(description, serviceType);
							}
						}
						this.UpdateOperationsWithInterfaceAttributes(contractDescription, reflectionInfo);
						this.AddBehaviors(contractDescription, serviceType, false, reflectionInfo);
						this.contracts.Add(contractTypeAndAttribute, contractDescription);
					}
				}
			}
			return contractDescription;
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x00099B04 File Offset: 0x00097D04
		private void EnsureNoInheritanceWithContractClasses(Type actualContractType)
		{
			if (actualContractType.IsClass)
			{
				Type baseType = actualContractType.BaseType;
				while (baseType != null)
				{
					if (ServiceReflector.GetSingleAttribute<ServiceContractAttribute>(baseType) != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxContractInheritanceRequiresInterfaces", new object[]
						{
							actualContractType,
							baseType
						})));
					}
					baseType = baseType.BaseType;
				}
			}
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x00099B64 File Offset: 0x00097D64
		private void EnsureNoOperationContractsOnNonServiceContractTypes(Type actualContractType)
		{
			foreach (Type aParentType in actualContractType.GetInterfaces())
			{
				this.EnsureNoOperationContractsOnNonServiceContractTypes_Helper(aParentType);
			}
			Type baseType = actualContractType.BaseType;
			while (baseType != null)
			{
				this.EnsureNoOperationContractsOnNonServiceContractTypes_Helper(baseType);
				baseType = baseType.BaseType;
			}
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x00099BB4 File Offset: 0x00097DB4
		private void EnsureNoOperationContractsOnNonServiceContractTypes_Helper(Type aParentType)
		{
			if (ServiceReflector.GetSingleAttribute<ServiceContractAttribute>(aParentType) == null)
			{
				MethodInfo[] methods = aParentType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				int i = 0;
				while (i < methods.Length)
				{
					MethodInfo methodInfo = methods[i];
					Type operationContractProviderType = ServiceReflector.GetOperationContractProviderType(methodInfo);
					if (operationContractProviderType != null)
					{
						if (operationContractProviderType == TypeLoader.OperationContractAttributeType)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxOperationContractOnNonServiceContract", new object[]
							{
								methodInfo.Name,
								aParentType.Name
							})));
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxOperationContractProviderOnNonServiceContract", new object[]
						{
							operationContractProviderType.Name,
							methodInfo.Name,
							aParentType.Name
						})));
					}
					else
					{
						i++;
					}
				}
			}
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x00099C77 File Offset: 0x00097E77
		public ContractDescription LoadContractDescription(Type contractType)
		{
			return this.LoadContractDescriptionHelper(contractType, null, null);
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x00099C82 File Offset: 0x00097E82
		public ContractDescription LoadContractDescription(Type contractType, Type serviceType)
		{
			return this.LoadContractDescriptionHelper(contractType, serviceType, null);
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x00099C8D File Offset: 0x00097E8D
		public ContractDescription LoadContractDescription(Type contractType, Type serviceType, object serviceImplementation)
		{
			return this.LoadContractDescriptionHelper(contractType, serviceType, serviceImplementation);
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x00099C98 File Offset: 0x00097E98
		private ContractDescription LoadOutputChannelContractDescription()
		{
			Type typeFromHandle = typeof(IOutputChannel);
			XmlQualifiedName contractName = NamingHelper.GetContractName(typeFromHandle, null, "http://schemas.microsoft.com/2005/07/ServiceModel");
			ContractDescription contractDescription = new ContractDescription(contractName.Name, contractName.Namespace);
			contractDescription.ContractType = typeFromHandle;
			contractDescription.ConfigurationName = typeFromHandle.FullName;
			contractDescription.SessionMode = SessionMode.NotAllowed;
			OperationDescription operationDescription = new OperationDescription("Send", contractDescription);
			MessageDescription item = new MessageDescription("*", MessageDirection.Input);
			operationDescription.Messages.Add(item);
			contractDescription.Operations.Add(operationDescription);
			return contractDescription;
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x00099D1C File Offset: 0x00097F1C
		private ContractDescription LoadRequestChannelContractDescription()
		{
			Type typeFromHandle = typeof(IRequestChannel);
			XmlQualifiedName contractName = NamingHelper.GetContractName(typeFromHandle, null, "http://schemas.microsoft.com/2005/07/ServiceModel");
			ContractDescription contractDescription = new ContractDescription(contractName.Name, contractName.Namespace);
			contractDescription.ContractType = typeFromHandle;
			contractDescription.ConfigurationName = typeFromHandle.FullName;
			contractDescription.SessionMode = SessionMode.NotAllowed;
			OperationDescription operationDescription = new OperationDescription("Request", contractDescription);
			MessageDescription item = new MessageDescription("*", MessageDirection.Input);
			MessageDescription item2 = new MessageDescription("*", MessageDirection.Output);
			operationDescription.Messages.Add(item);
			operationDescription.Messages.Add(item2);
			contractDescription.Operations.Add(operationDescription);
			return contractDescription;
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x00099DBC File Offset: 0x00097FBC
		private void AddBehaviors(ContractDescription contractDesc, Type implType, bool implIsCallback, TypeLoader.ContractReflectionInfo reflectionInfo)
		{
			ServiceContractAttribute requiredSingleAttribute = ServiceReflector.GetRequiredSingleAttribute<ServiceContractAttribute>(reflectionInfo.iface);
			for (int i = 0; i < contractDesc.Operations.Count; i++)
			{
				OperationDescription operationDescription = contractDesc.Operations[i];
				if (operationDescription.DeclaringContract == contractDesc)
				{
					operationDescription.Behaviors.Add(new OperationInvokerBehavior());
				}
			}
			contractDesc.Behaviors.Add(new OperationSelectorBehavior());
			for (int j = 0; j < contractDesc.Operations.Count; j++)
			{
				OperationDescription opDesc = contractDesc.Operations[j];
				bool flag = opDesc.DeclaringContract != contractDesc;
				Type targetIface = implIsCallback ? opDesc.DeclaringContract.CallbackContractType : opDesc.DeclaringContract.ContractType;
				if (implType == null && !flag)
				{
					KeyedByTypeCollection<IOperationBehavior> ioperationBehaviorAttributesFromType = this.GetIOperationBehaviorAttributesFromType(opDesc, targetIface, null);
					for (int k = 0; k < ioperationBehaviorAttributesFromType.Count; k++)
					{
						opDesc.Behaviors.Add(ioperationBehaviorAttributesFromType[k]);
					}
				}
				else
				{
					TypeLoader.ApplyServiceInheritance<IOperationBehavior, KeyedByTypeCollection<IOperationBehavior>>(implType, opDesc.Behaviors, delegate(Type currentType, KeyedByTypeCollection<IOperationBehavior> behaviors)
					{
						KeyedByTypeCollection<IOperationBehavior> ioperationBehaviorAttributesFromType2 = this.GetIOperationBehaviorAttributesFromType(opDesc, targetIface, currentType);
						for (int n = 0; n < ioperationBehaviorAttributesFromType2.Count; n++)
						{
							behaviors.Add(ioperationBehaviorAttributesFromType2[n]);
						}
					});
					if (!flag)
					{
						TypeLoader.AddBehaviorsAtOneScope<IOperationBehavior, KeyedByTypeCollection<IOperationBehavior>>(targetIface, opDesc.Behaviors, delegate(Type currentType, KeyedByTypeCollection<IOperationBehavior> behaviors)
						{
							KeyedByTypeCollection<IOperationBehavior> ioperationBehaviorAttributesFromType2 = this.GetIOperationBehaviorAttributesFromType(opDesc, targetIface, null);
							for (int n = 0; n < ioperationBehaviorAttributesFromType2.Count; n++)
							{
								behaviors.Add(ioperationBehaviorAttributesFromType2[n]);
							}
						});
					}
				}
			}
			for (int l = 0; l < contractDesc.Operations.Count; l++)
			{
				OperationDescription operationDescription2 = contractDesc.Operations[l];
				if (operationDescription2.Behaviors.Find<OperationBehaviorAttribute>() == null)
				{
					OperationBehaviorAttribute item = new OperationBehaviorAttribute();
					operationDescription2.Behaviors.Add(item);
				}
			}
			Type type = implIsCallback ? reflectionInfo.callbackiface : reflectionInfo.iface;
			TypeLoader.AddBehaviorsAtOneScope<IContractBehavior, KeyedByTypeCollection<IContractBehavior>>(type, contractDesc.Behaviors, new TypeLoader.ServiceInheritanceCallback<IContractBehavior, KeyedByTypeCollection<IContractBehavior>>(this.GetIContractBehaviorsFromInterfaceType));
			bool flag2 = false;
			for (int m = 0; m < contractDesc.Operations.Count; m++)
			{
				OperationDescription operationDescription3 = contractDesc.Operations[m];
				bool flag3 = operationDescription3.DeclaringContract != contractDesc;
				MethodInfo operationMethod = operationDescription3.OperationMethod;
				Attribute formattingAttribute = TypeLoader.GetFormattingAttribute(operationMethod, TypeLoader.GetFormattingAttribute(operationDescription3.DeclaringContract.ContractType, TypeLoader.DefaultDataContractFormatAttribute));
				DataContractFormatAttribute dataContractFormatAttribute = formattingAttribute as DataContractFormatAttribute;
				if (dataContractFormatAttribute != null)
				{
					if (!flag3)
					{
						operationDescription3.Behaviors.Add(new DataContractSerializerOperationBehavior(operationDescription3, dataContractFormatAttribute, true));
						operationDescription3.Behaviors.Add(new DataContractSerializerOperationGenerator());
					}
				}
				else if (formattingAttribute != null && formattingAttribute is XmlSerializerFormatAttribute)
				{
					flag2 = true;
				}
			}
			if (flag2)
			{
				XmlSerializerOperationBehavior.AddBuiltInBehaviors(contractDesc);
			}
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x0009A088 File Offset: 0x00098288
		private void GetIContractBehaviorsFromInterfaceType(Type interfaceType, KeyedByTypeCollection<IContractBehavior> behaviors)
		{
			foreach (IContractBehavior item in ServiceReflector.GetCustomAttributes(interfaceType, typeof(IContractBehavior), false))
			{
				behaviors.Add(item);
			}
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x0009A0C8 File Offset: 0x000982C8
		private static void UpdateContractDescriptionWithAttributesFromServiceType(ContractDescription description, Type serviceType)
		{
			TypeLoader.ApplyServiceInheritance<IContractBehavior, KeyedByTypeCollection<IContractBehavior>>(serviceType, description.Behaviors, delegate(Type currentType, KeyedByTypeCollection<IContractBehavior> behaviors)
			{
				foreach (IContractBehavior contractBehavior in ServiceReflector.GetCustomAttributes(currentType, typeof(IContractBehavior), false))
				{
					IContractBehaviorAttribute contractBehaviorAttribute = contractBehavior as IContractBehaviorAttribute;
					if (contractBehaviorAttribute == null || contractBehaviorAttribute.TargetContract == null || contractBehaviorAttribute.TargetContract == description.ContractType)
					{
						behaviors.Add(contractBehavior);
					}
				}
			});
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x0009A100 File Offset: 0x00098300
		private void UpdateOperationsWithInterfaceAttributes(ContractDescription contractDesc, TypeLoader.ContractReflectionInfo reflectionInfo)
		{
			object[] customAttributes = ServiceReflector.GetCustomAttributes(reflectionInfo.iface, typeof(ServiceKnownTypeAttribute), false);
			IEnumerable<Type> knownTypes = this.GetKnownTypes(customAttributes, reflectionInfo.iface);
			foreach (Type item in knownTypes)
			{
				foreach (OperationDescription operationDescription in contractDesc.Operations)
				{
					if (!operationDescription.IsServerInitiated())
					{
						operationDescription.KnownTypes.Add(item);
					}
				}
			}
			if (reflectionInfo.callbackiface != null)
			{
				customAttributes = ServiceReflector.GetCustomAttributes(reflectionInfo.callbackiface, typeof(ServiceKnownTypeAttribute), false);
				knownTypes = this.GetKnownTypes(customAttributes, reflectionInfo.callbackiface);
				foreach (Type item2 in knownTypes)
				{
					foreach (OperationDescription operationDescription2 in contractDesc.Operations)
					{
						if (operationDescription2.IsServerInitiated())
						{
							operationDescription2.KnownTypes.Add(item2);
						}
					}
				}
			}
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x0009A278 File Offset: 0x00098478
		private IEnumerable<Type> GetKnownTypes(object[] knownTypeAttributes, ICustomAttributeProvider provider)
		{
			if (knownTypeAttributes.Length == 1)
			{
				ServiceKnownTypeAttribute serviceKnownTypeAttribute = (ServiceKnownTypeAttribute)knownTypeAttributes[0];
				if (!string.IsNullOrEmpty(serviceKnownTypeAttribute.MethodName))
				{
					Type type = serviceKnownTypeAttribute.DeclaringType;
					if (type == null)
					{
						type = (provider as Type);
						if (type == null)
						{
							type = ((MethodInfo)provider).DeclaringType;
						}
					}
					MethodInfo method = type.GetMethod(serviceKnownTypeAttribute.MethodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, TypeLoader.knownTypesMethodParamType, null);
					if (method == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxKnownTypeAttributeUnknownMethod3", new object[]
						{
							provider,
							serviceKnownTypeAttribute.MethodName,
							type.FullName
						})));
					}
					if (!typeof(IEnumerable<Type>).IsAssignableFrom(method.ReturnType))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxKnownTypeAttributeReturnType3", new object[]
						{
							provider,
							serviceKnownTypeAttribute.MethodName,
							type.FullName
						})));
					}
					return (IEnumerable<Type>)method.Invoke(null, new object[]
					{
						provider
					});
				}
			}
			List<Type> list = new List<Type>();
			foreach (ServiceKnownTypeAttribute serviceKnownTypeAttribute2 in knownTypeAttributes)
			{
				if (serviceKnownTypeAttribute2.Type == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxKnownTypeAttributeInvalid1", new object[]
					{
						provider.ToString()
					})));
				}
				list.Add(serviceKnownTypeAttribute2.Type);
			}
			return list;
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x0009A3F8 File Offset: 0x000985F8
		private KeyedByTypeCollection<IOperationBehavior> GetIOperationBehaviorAttributesFromType(OperationDescription opDesc, Type targetIface, Type implType)
		{
			KeyedByTypeCollection<IOperationBehavior> result = new KeyedByTypeCollection<IOperationBehavior>();
			InterfaceMapping ifaceMap = default(InterfaceMapping);
			bool useImplAttrs = false;
			if (implType != null)
			{
				if (!targetIface.IsAssignableFrom(implType) || !targetIface.IsInterface)
				{
					return result;
				}
				ifaceMap = implType.GetInterfaceMap(targetIface);
				useImplAttrs = true;
			}
			MethodInfo operationMethod = opDesc.OperationMethod;
			this.ProcessOpMethod(operationMethod, true, opDesc, result, ifaceMap, useImplAttrs);
			if (opDesc.SyncMethod != null && opDesc.BeginMethod != null)
			{
				this.ProcessOpMethod(opDesc.BeginMethod, false, opDesc, result, ifaceMap, useImplAttrs);
			}
			else if (opDesc.SyncMethod != null && opDesc.TaskMethod != null)
			{
				this.ProcessOpMethod(opDesc.TaskMethod, false, opDesc, result, ifaceMap, useImplAttrs);
			}
			else if (opDesc.TaskMethod != null && opDesc.BeginMethod != null)
			{
				this.ProcessOpMethod(opDesc.BeginMethod, false, opDesc, result, ifaceMap, useImplAttrs);
			}
			return result;
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x0009A4DC File Offset: 0x000986DC
		private void ProcessOpMethod(MethodInfo opMethod, bool canHaveBehaviors, OperationDescription opDesc, KeyedByTypeCollection<IOperationBehavior> result, InterfaceMapping ifaceMap, bool useImplAttrs)
		{
			MethodInfo methodInfo = null;
			if (useImplAttrs)
			{
				int num = Array.IndexOf<MethodInfo>(ifaceMap.InterfaceMethods, opMethod);
				if (num != -1)
				{
					MethodInfo methodInfo2 = ifaceMap.TargetMethods[num];
					if (methodInfo2 != null)
					{
						methodInfo = methodInfo2;
					}
				}
				if (methodInfo == null)
				{
					return;
				}
			}
			else
			{
				methodInfo = opMethod;
			}
			foreach (IOperationBehavior operationBehavior in ServiceReflector.GetCustomAttributes(methodInfo, typeof(IOperationBehavior), false))
			{
				if (canHaveBehaviors)
				{
					result.Add(operationBehavior);
				}
				else
				{
					if (opDesc.SyncMethod != null && opDesc.BeginMethod != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SyncAsyncMatchConsistency_Attributes6", new object[]
						{
							opDesc.SyncMethod.Name,
							opDesc.SyncMethod.DeclaringType,
							opDesc.BeginMethod.Name,
							opDesc.EndMethod.Name,
							opDesc.Name,
							operationBehavior.GetType().FullName
						})));
					}
					if (opDesc.SyncMethod != null && opDesc.TaskMethod != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SyncTaskMatchConsistency_Attributes6", new object[]
						{
							opDesc.SyncMethod.Name,
							opDesc.SyncMethod.DeclaringType,
							opDesc.TaskMethod.Name,
							opDesc.Name,
							operationBehavior.GetType().FullName
						})));
					}
					if (opDesc.TaskMethod != null && opDesc.BeginMethod != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TaskAsyncMatchConsistency_Attributes6", new object[]
						{
							opDesc.TaskMethod.Name,
							opDesc.TaskMethod.DeclaringType,
							opDesc.BeginMethod.Name,
							opDesc.EndMethod.Name,
							opDesc.Name,
							operationBehavior.GetType().FullName
						})));
					}
				}
			}
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x0009A700 File Offset: 0x00098900
		internal void AddBehaviorsSFx(ServiceEndpoint serviceEndpoint, Type contractType)
		{
			if (serviceEndpoint.Contract.IsDuplex() && serviceEndpoint.Behaviors.Find<CallbackBehaviorAttribute>() == null)
			{
				serviceEndpoint.Behaviors.Insert(0, new CallbackBehaviorAttribute());
			}
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x0009A73C File Offset: 0x0009893C
		internal void AddBehaviorsFromImplementationType(ServiceEndpoint serviceEndpoint, Type implementationType)
		{
			foreach (IEndpointBehavior endpointBehavior in ServiceReflector.GetCustomAttributes(implementationType, typeof(IEndpointBehavior), false))
			{
				if (endpointBehavior is CallbackBehaviorAttribute)
				{
					serviceEndpoint.Behaviors.Insert(0, endpointBehavior);
				}
				else
				{
					serviceEndpoint.Behaviors.Add(endpointBehavior);
				}
			}
			foreach (IContractBehavior item in ServiceReflector.GetCustomAttributes(implementationType, typeof(IContractBehavior), false))
			{
				serviceEndpoint.Contract.Behaviors.Add(item);
			}
			Type targetIface = serviceEndpoint.Contract.CallbackContractType;
			for (int k = 0; k < serviceEndpoint.Contract.Operations.Count; k++)
			{
				OperationDescription opDesc = serviceEndpoint.Contract.Operations[k];
				KeyedByTypeCollection<IOperationBehavior> keyedByTypeCollection = new KeyedByTypeCollection<IOperationBehavior>();
				TypeLoader.ApplyServiceInheritance<IOperationBehavior, KeyedByTypeCollection<IOperationBehavior>>(implementationType, keyedByTypeCollection, delegate(Type currentType, KeyedByTypeCollection<IOperationBehavior> behaviors)
				{
					KeyedByTypeCollection<IOperationBehavior> ioperationBehaviorAttributesFromType = this.GetIOperationBehaviorAttributesFromType(opDesc, targetIface, currentType);
					for (int m = 0; m < ioperationBehaviorAttributesFromType.Count; m++)
					{
						behaviors.Add(ioperationBehaviorAttributesFromType[m]);
					}
				});
				for (int l = 0; l < keyedByTypeCollection.Count; l++)
				{
					IOperationBehavior operationBehavior = keyedByTypeCollection[l];
					Type type = operationBehavior.GetType();
					if (opDesc.Behaviors.Contains(type))
					{
						opDesc.Behaviors.Remove(type);
					}
					opDesc.Behaviors.Add(operationBehavior);
				}
			}
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x0009A8C8 File Offset: 0x00098AC8
		internal static int CompareMessagePartDescriptions(MessagePartDescription a, MessagePartDescription b)
		{
			int num = a.SerializationPosition - b.SerializationPosition;
			if (num != 0)
			{
				return num;
			}
			int num2 = string.Compare(a.Namespace, b.Namespace, StringComparison.Ordinal);
			if (num2 != 0)
			{
				return num2;
			}
			return string.Compare(a.Name, b.Name, StringComparison.Ordinal);
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x0009A912 File Offset: 0x00098B12
		internal static XmlName GetBodyWrapperResponseName(string operationName)
		{
			return new XmlName(operationName + "Response");
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x0009A924 File Offset: 0x00098B24
		internal static XmlName GetBodyWrapperResponseName(XmlName operationName)
		{
			return new XmlName(operationName.EncodedName + "Response", true);
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x0009A93C File Offset: 0x00098B3C
		private void CreateOperationDescriptions(ContractDescription contractDescription, TypeLoader.ContractReflectionInfo reflectionInfo, Type contractToGetMethodsFrom, ContractDescription declaringContract, MessageDirection direction)
		{
			MessageDirection messageDirection = MessageDirectionHelper.Opposite(direction);
			if (!declaringContract.ContractType.IsAssignableFrom(contractDescription.ContractType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Bad contract inheritence. Contract {0} does not implement {1}", new object[]
				{
					declaringContract.ContractType.Name,
					contractDescription.ContractType.Name
				})));
			}
			foreach (MethodInfo methodInfo in contractToGetMethodsFrom.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (contractToGetMethodsFrom.IsInterface)
				{
					object[] customAttributes = ServiceReflector.GetCustomAttributes(methodInfo, typeof(OperationBehaviorAttribute), false);
					if (customAttributes.Length != 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxOperationBehaviorAttributeOnlyOnServiceClass", new object[]
						{
							methodInfo.Name,
							contractToGetMethodsFrom.Name
						})));
					}
				}
				ServiceReflector.ValidateParameterMetadata(methodInfo);
				OperationDescription operationDescription = this.CreateOperationDescription(contractDescription, methodInfo, direction, reflectionInfo, declaringContract);
				if (operationDescription != null)
				{
					contractDescription.Operations.Add(operationDescription);
				}
			}
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x0009AA40 File Offset: 0x00098C40
		internal static void EnsureCallbackType(Type callbackType)
		{
			if (callbackType != null && !callbackType.IsInterface && !callbackType.IsMarshalByRef)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxInvalidCallbackContractType", new object[]
				{
					callbackType.Name
				})));
			}
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x0009AA90 File Offset: 0x00098C90
		internal static void EnsureSubcontract(ServiceContractAttribute svcContractAttr, Type contractType)
		{
			Type callbackContract = svcContractAttr.CallbackContract;
			List<Type> inheritedContractTypes = ServiceReflector.GetInheritedContractTypes(contractType);
			for (int i = 0; i < inheritedContractTypes.Count; i++)
			{
				Type type = inheritedContractTypes[i];
				ServiceContractAttribute requiredSingleAttribute = ServiceReflector.GetRequiredSingleAttribute<ServiceContractAttribute>(type);
				if (requiredSingleAttribute.CallbackContract != null)
				{
					if (callbackContract == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InAContractInheritanceHierarchyIfParentHasCallbackChildMustToo", new object[]
						{
							type.Name,
							requiredSingleAttribute.CallbackContract.Name,
							contractType.Name
						})));
					}
					if (!requiredSingleAttribute.CallbackContract.IsAssignableFrom(callbackContract))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InAContractInheritanceHierarchyTheServiceContract3_2", new object[]
						{
							type.Name,
							contractType.Name
						})));
					}
				}
			}
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x0009AB70 File Offset: 0x00098D70
		private ContractDescription CreateContractDescription(ServiceContractAttribute contractAttr, Type contractType, Type serviceType, out TypeLoader.ContractReflectionInfo reflectionInfo, object serviceImplementation)
		{
			reflectionInfo = new TypeLoader.ContractReflectionInfo();
			XmlQualifiedName contractName = NamingHelper.GetContractName(contractType, contractAttr.Name, contractAttr.Namespace);
			ContractDescription contractDescription = new ContractDescription(contractName.Name, contractName.Namespace);
			contractDescription.ContractType = contractType;
			if (contractAttr.HasProtectionLevel)
			{
				contractDescription.ProtectionLevel = contractAttr.ProtectionLevel;
			}
			Type callbackContract = contractAttr.CallbackContract;
			TypeLoader.EnsureCallbackType(callbackContract);
			TypeLoader.EnsureSubcontract(contractAttr, contractType);
			reflectionInfo.iface = contractType;
			reflectionInfo.callbackiface = callbackContract;
			contractDescription.SessionMode = contractAttr.SessionMode;
			contractDescription.CallbackContractType = callbackContract;
			contractDescription.ConfigurationName = (contractAttr.ConfigurationName ?? contractType.FullName);
			List<Type> inheritedContractTypes = ServiceReflector.GetInheritedContractTypes(contractType);
			List<Type> list = new List<Type>();
			for (int i = 0; i < inheritedContractTypes.Count; i++)
			{
				Type type = inheritedContractTypes[i];
				ServiceContractAttribute requiredSingleAttribute = ServiceReflector.GetRequiredSingleAttribute<ServiceContractAttribute>(type);
				ContractDescription contractDescription2 = this.LoadContractDescriptionHelper(type, serviceType, serviceImplementation);
				foreach (OperationDescription operationDescription in contractDescription2.Operations)
				{
					if (!contractDescription.Operations.Contains(operationDescription))
					{
						Collection<OperationDescription> collection = contractDescription.Operations.FindAll(operationDescription.Name);
						foreach (OperationDescription operationDescription2 in collection)
						{
							if (operationDescription2.Messages[0].Direction == operationDescription.Messages[0].Direction)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotInheritTwoOperationsWithTheSameName3", new object[]
								{
									operationDescription.Name,
									contractDescription2.Name,
									operationDescription2.DeclaringContract.Name
								})));
							}
						}
						contractDescription.Operations.Add(operationDescription);
					}
				}
				if (contractDescription2.CallbackContractType != null)
				{
					list.Add(contractDescription2.CallbackContractType);
				}
			}
			this.CreateOperationDescriptions(contractDescription, reflectionInfo, contractType, contractDescription, MessageDirection.Input);
			if (callbackContract != null && !list.Contains(callbackContract))
			{
				this.CreateOperationDescriptions(contractDescription, reflectionInfo, callbackContract, contractDescription, MessageDirection.Output);
			}
			return contractDescription;
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x0009ADC4 File Offset: 0x00098FC4
		internal static Attribute GetFormattingAttribute(ICustomAttributeProvider attrProvider, Attribute defaultFormatAttribute)
		{
			if (attrProvider != null)
			{
				if (attrProvider.IsDefined(typeof(XmlSerializerFormatAttribute), false))
				{
					return ServiceReflector.GetSingleAttribute<XmlSerializerFormatAttribute>(attrProvider, TypeLoader.formatterAttributes);
				}
				if (attrProvider.IsDefined(typeof(DataContractFormatAttribute), false))
				{
					return ServiceReflector.GetSingleAttribute<DataContractFormatAttribute>(attrProvider, TypeLoader.formatterAttributes);
				}
			}
			return defaultFormatAttribute;
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x0009AE13 File Offset: 0x00099013
		private void VerifyConsistency(TypeLoader.OperationConsistencyVerifier verifier)
		{
			verifier.VerifyParameterLength();
			verifier.VerifyParameterType();
			verifier.VerifyOutParameterType();
			verifier.VerifyReturnType();
			verifier.VerifyFaultContractAttribute();
			verifier.VerifyKnownTypeAttribute();
			verifier.VerifyIsOneWayStatus();
			verifier.VerifyActionAndReplyAction();
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x0009AE48 File Offset: 0x00099048
		private OperationDescription CreateOperationDescription(ContractDescription contractDescription, MethodInfo methodInfo, MessageDirection direction, TypeLoader.ContractReflectionInfo reflectionInfo, ContractDescription declaringContract)
		{
			OperationContractAttribute operationContractAttribute = ServiceReflector.GetOperationContractAttribute(methodInfo);
			if (operationContractAttribute == null)
			{
				return null;
			}
			if (ServiceReflector.HasEndMethodShape(methodInfo))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EndMethodsCannotBeDecoratedWithOperationContractAttribute", new object[]
				{
					methodInfo.Name,
					reflectionInfo.iface
				})));
			}
			Type type;
			bool flag = ServiceReflector.IsTask(methodInfo, out type);
			bool flag2 = !flag && ServiceReflector.IsBegin(operationContractAttribute, methodInfo);
			XmlName operationName = NamingHelper.GetOperationName(ServiceReflector.GetLogicalName(methodInfo, flag2, flag), operationContractAttribute.Name);
			operationContractAttribute.EnsureInvariants(methodInfo, operationName.EncodedName);
			Collection<OperationDescription> collection = contractDescription.Operations.FindAll(operationName.EncodedName);
			int i = 0;
			while (i < collection.Count)
			{
				OperationDescription operationDescription = collection[i];
				if (operationDescription.Messages[0].Direction == direction)
				{
					if (operationDescription.TaskMethod != null && flag)
					{
						string name = operationDescription.OperationMethod.Name;
						string name2 = methodInfo.Name;
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotHaveTwoOperationsWithTheSameName3", new object[]
						{
							name,
							name2,
							reflectionInfo.iface
						})));
					}
					if (flag2 && operationDescription.BeginMethod != null)
					{
						string name3 = operationDescription.BeginMethod.Name;
						string name4 = methodInfo.Name;
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotHaveTwoOperationsWithTheSameName3", new object[]
						{
							name3,
							name4,
							reflectionInfo.iface
						})));
					}
					if (!flag2 && !flag && operationDescription.SyncMethod != null)
					{
						string name5 = operationDescription.SyncMethod.Name;
						string name6 = methodInfo.Name;
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotHaveTwoOperationsWithTheSameName3", new object[]
						{
							name5,
							name6,
							reflectionInfo.iface
						})));
					}
					contractDescription.Operations.Remove(operationDescription);
					OperationDescription operationDescription2 = this.CreateOperationDescription(contractDescription, methodInfo, direction, reflectionInfo, declaringContract);
					operationDescription2.HasNoDisposableParameters = ServiceReflector.HasNoDisposableParameters(methodInfo);
					if (flag)
					{
						operationDescription.TaskMethod = operationDescription2.TaskMethod;
						operationDescription.TaskTResult = operationDescription2.TaskTResult;
						if (operationDescription.SyncMethod != null)
						{
							this.VerifyConsistency(new TypeLoader.SyncTaskOperationConsistencyVerifier(operationDescription, operationDescription2));
						}
						else
						{
							this.VerifyConsistency(new TypeLoader.TaskAsyncOperationConsistencyVerifier(operationDescription2, operationDescription));
						}
						return operationDescription;
					}
					if (flag2)
					{
						operationDescription.BeginMethod = operationDescription2.BeginMethod;
						operationDescription.EndMethod = operationDescription2.EndMethod;
						if (operationDescription.SyncMethod != null)
						{
							this.VerifyConsistency(new TypeLoader.SyncAsyncOperationConsistencyVerifier(operationDescription, operationDescription2));
						}
						else
						{
							this.VerifyConsistency(new TypeLoader.TaskAsyncOperationConsistencyVerifier(operationDescription, operationDescription2));
						}
						return operationDescription;
					}
					operationDescription2.BeginMethod = operationDescription.BeginMethod;
					operationDescription2.EndMethod = operationDescription.EndMethod;
					operationDescription2.TaskMethod = operationDescription.TaskMethod;
					operationDescription2.TaskTResult = operationDescription.TaskTResult;
					if (operationDescription.TaskMethod != null)
					{
						this.VerifyConsistency(new TypeLoader.SyncTaskOperationConsistencyVerifier(operationDescription2, operationDescription));
					}
					else
					{
						this.VerifyConsistency(new TypeLoader.SyncAsyncOperationConsistencyVerifier(operationDescription2, operationDescription));
					}
					return operationDescription2;
				}
				else
				{
					i++;
				}
			}
			OperationDescription operationDescription3 = new OperationDescription(operationName.EncodedName, declaringContract);
			operationDescription3.IsInitiating = operationContractAttribute.IsInitiating;
			operationDescription3.IsTerminating = operationContractAttribute.IsTerminating;
			operationDescription3.IsSessionOpenNotificationEnabled = operationContractAttribute.IsSessionOpenNotificationEnabled;
			operationDescription3.HasNoDisposableParameters = ServiceReflector.HasNoDisposableParameters(methodInfo);
			if (operationContractAttribute.HasProtectionLevel)
			{
				operationDescription3.ProtectionLevel = operationContractAttribute.ProtectionLevel;
			}
			XmlQualifiedName contractName = new XmlQualifiedName(declaringContract.Name, declaringContract.Namespace);
			object[] customAttributes = ServiceReflector.GetCustomAttributes(methodInfo, typeof(FaultContractAttribute), false);
			if (operationContractAttribute.IsOneWay && customAttributes.Length != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("OneWayAndFaultsIncompatible2", new object[]
				{
					methodInfo.DeclaringType.FullName,
					operationName.EncodedName
				})));
			}
			foreach (FaultContractAttribute attr in customAttributes)
			{
				FaultDescription faultDescription = this.CreateFaultDescription(attr, contractName, declaringContract.Namespace, operationDescription3.XmlName);
				this.CheckDuplicateFaultContract(operationDescription3.Faults, faultDescription, operationName.EncodedName);
				operationDescription3.Faults.Add(faultDescription);
			}
			customAttributes = ServiceReflector.GetCustomAttributes(methodInfo, typeof(ServiceKnownTypeAttribute), false);
			IEnumerable<Type> knownTypes = this.GetKnownTypes(customAttributes, methodInfo);
			foreach (Type item in knownTypes)
			{
				operationDescription3.KnownTypes.Add(item);
			}
			MessageDirection direction2 = MessageDirectionHelper.Opposite(direction);
			string messageAction = NamingHelper.GetMessageAction(contractName, operationDescription3.CodeName, operationContractAttribute.Action, false);
			string messageAction2 = NamingHelper.GetMessageAction(contractName, operationDescription3.CodeName, operationContractAttribute.ReplyAction, true);
			XmlName wrapperName = operationName;
			XmlName bodyWrapperResponseName = TypeLoader.GetBodyWrapperResponseName(operationName);
			string @namespace = declaringContract.Namespace;
			MessageDescription messageDescription = this.CreateMessageDescription(methodInfo, flag2, flag, null, null, contractDescription.Namespace, messageAction, wrapperName, @namespace, direction);
			MessageDescription messageDescription2 = null;
			operationDescription3.Messages.Add(messageDescription);
			MethodInfo methodInfo2 = methodInfo;
			if (flag)
			{
				operationDescription3.TaskMethod = methodInfo;
				operationDescription3.TaskTResult = type;
			}
			else if (!flag2)
			{
				operationDescription3.SyncMethod = methodInfo;
			}
			else
			{
				methodInfo2 = ServiceReflector.GetEndMethod(methodInfo);
				operationDescription3.EndMethod = methodInfo2;
				operationDescription3.BeginMethod = methodInfo;
			}
			if (!operationContractAttribute.IsOneWay)
			{
				XmlName returnValueName = TypeLoader.GetReturnValueName(operationName);
				messageDescription2 = this.CreateMessageDescription(methodInfo2, flag2, flag, type, returnValueName, contractDescription.Namespace, messageAction2, bodyWrapperResponseName, @namespace, direction2);
				operationDescription3.Messages.Add(messageDescription2);
			}
			else
			{
				if ((!flag && methodInfo2.ReturnType != ServiceReflector.VoidType) || (flag && type != ServiceReflector.VoidType) || ServiceReflector.HasOutputParameters(methodInfo2, flag2))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ServiceOperationsMarkedWithIsOneWayTrueMust0")));
				}
				if (operationContractAttribute.ReplyAction != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("OneWayOperationShouldNotSpecifyAReplyAction1", new object[]
					{
						operationName
					})));
				}
			}
			if (!operationContractAttribute.IsOneWay)
			{
				if (messageDescription2.IsVoid && (messageDescription.IsUntypedMessage || messageDescription.IsTypedMessage))
				{
					messageDescription2.Body.WrapperName = (messageDescription2.Body.WrapperNamespace = null);
				}
				else if (messageDescription.IsVoid && (messageDescription2.IsUntypedMessage || messageDescription2.IsTypedMessage))
				{
					messageDescription.Body.WrapperName = (messageDescription.Body.WrapperNamespace = null);
				}
			}
			return operationDescription3;
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x0009B4EC File Offset: 0x000996EC
		private void CheckDuplicateFaultContract(FaultDescriptionCollection faultDescriptionCollection, FaultDescription fault, string operationName)
		{
			foreach (FaultDescription faultDescription in faultDescriptionCollection)
			{
				if (XmlName.IsNullOrEmpty(faultDescription.ElementName) && XmlName.IsNullOrEmpty(fault.ElementName) && faultDescription.DetailType == fault.DetailType)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxFaultContractDuplicateDetailType", new object[]
					{
						operationName,
						fault.DetailType
					})));
				}
				if (!XmlName.IsNullOrEmpty(faultDescription.ElementName) && !XmlName.IsNullOrEmpty(fault.ElementName) && faultDescription.ElementName == fault.ElementName && faultDescription.Namespace == fault.Namespace)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxFaultContractDuplicateElement", new object[]
					{
						operationName,
						fault.ElementName,
						fault.Namespace
					})));
				}
			}
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x0009B604 File Offset: 0x00099804
		private FaultDescription CreateFaultDescription(FaultContractAttribute attr, XmlQualifiedName contractName, string contractNamespace, XmlName operationName)
		{
			XmlName xmlName = new XmlName(attr.Name ?? (NamingHelper.TypeName(attr.DetailType) + "Fault"));
			FaultDescription faultDescription = new FaultDescription(NamingHelper.GetMessageAction(contractName, operationName.DecodedName + xmlName.DecodedName, attr.Action, false));
			if (attr.Name != null)
			{
				faultDescription.SetNameAndElement(xmlName);
			}
			else
			{
				faultDescription.SetNameOnly(xmlName);
			}
			faultDescription.Namespace = (attr.Namespace ?? contractNamespace);
			faultDescription.DetailType = attr.DetailType;
			if (attr.HasProtectionLevel)
			{
				faultDescription.ProtectionLevel = attr.ProtectionLevel;
			}
			return faultDescription;
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x0009B6A8 File Offset: 0x000998A8
		private MessageDescription CreateMessageDescription(MethodInfo methodInfo, bool isAsync, bool isTask, Type taskTResult, XmlName returnValueName, string defaultNS, string action, XmlName wrapperName, string wrapperNamespace, MessageDirection direction)
		{
			string name = methodInfo.Name;
			MessageDescription messageDescription;
			if (returnValueName == null)
			{
				ParameterInfo[] inputParameters = ServiceReflector.GetInputParameters(methodInfo, isAsync);
				if (inputParameters.Length == 1 && inputParameters[0].ParameterType.IsDefined(typeof(MessageContractAttribute), false))
				{
					messageDescription = this.CreateTypedMessageDescription(inputParameters[0].ParameterType, null, null, defaultNS, action, direction);
				}
				else
				{
					messageDescription = this.CreateParameterMessageDescription(inputParameters, null, null, null, name, defaultNS, action, wrapperName, wrapperNamespace, direction);
				}
			}
			else
			{
				ParameterInfo[] outputParameters = ServiceReflector.GetOutputParameters(methodInfo, isAsync);
				Type type = isTask ? taskTResult : methodInfo.ReturnType;
				if (type.IsDefined(typeof(MessageContractAttribute), false) && outputParameters.Length == 0)
				{
					messageDescription = this.CreateTypedMessageDescription(type, methodInfo.ReturnTypeCustomAttributes, returnValueName, defaultNS, action, direction);
				}
				else
				{
					messageDescription = this.CreateParameterMessageDescription(outputParameters, type, methodInfo.ReturnTypeCustomAttributes, returnValueName, name, defaultNS, action, wrapperName, wrapperNamespace, direction);
				}
			}
			bool flag = false;
			for (int i = 0; i < messageDescription.Headers.Count; i++)
			{
				MessageHeaderDescription messageHeaderDescription = messageDescription.Headers[i];
				if (messageHeaderDescription.IsUnknownHeaderCollection)
				{
					if (flag)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxMultipleUnknownHeaders", new object[]
						{
							methodInfo,
							methodInfo.DeclaringType
						})));
					}
					flag = true;
				}
			}
			return messageDescription;
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x0009B7F4 File Offset: 0x000999F4
		private MessageDescription CreateParameterMessageDescription(ParameterInfo[] parameters, Type returnType, ICustomAttributeProvider returnAttrProvider, XmlName returnValueName, string methodName, string defaultNS, string action, XmlName wrapperName, string wrapperNamespace, MessageDirection direction)
		{
			foreach (ParameterInfo parameterInfo in parameters)
			{
				if (TypeLoader.GetParameterType(parameterInfo).IsDefined(typeof(MessageContractAttribute), false))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidMessageContractSignature", new object[]
					{
						methodName
					})));
				}
			}
			if (returnType != null && returnType.IsDefined(typeof(MessageContractAttribute), false))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidMessageContractSignature", new object[]
				{
					methodName
				})));
			}
			MessageDescription messageDescription = new MessageDescription(action, direction);
			MessagePartDescriptionCollection parts = messageDescription.Body.Parts;
			for (int j = 0; j < parameters.Length; j++)
			{
				MessagePartDescription messagePartDescription = TypeLoader.CreateParameterPartDescription(new XmlName(parameters[j].Name), defaultNS, j, parameters[j], TypeLoader.GetParameterType(parameters[j]));
				if (parts.Contains(new XmlQualifiedName(messagePartDescription.Name, messagePartDescription.Namespace)))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidMessageContractException(SR.GetString("SFxDuplicateMessageParts", new object[]
					{
						messagePartDescription.Name,
						messagePartDescription.Namespace
					})));
				}
				messageDescription.Body.Parts.Add(messagePartDescription);
			}
			if (returnType != null)
			{
				messageDescription.Body.ReturnValue = TypeLoader.CreateParameterPartDescription(returnValueName, defaultNS, 0, returnAttrProvider, returnType);
			}
			if (messageDescription.IsUntypedMessage)
			{
				messageDescription.Body.WrapperName = null;
				messageDescription.Body.WrapperNamespace = null;
			}
			else
			{
				messageDescription.Body.WrapperName = wrapperName.EncodedName;
				messageDescription.Body.WrapperNamespace = wrapperNamespace;
			}
			return messageDescription;
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x0009B9AC File Offset: 0x00099BAC
		private static MessagePartDescription CreateParameterPartDescription(XmlName defaultName, string defaultNS, int index, ICustomAttributeProvider attrProvider, Type type)
		{
			MessageParameterAttribute singleAttribute = ServiceReflector.GetSingleAttribute<MessageParameterAttribute>(attrProvider);
			XmlName xmlName = (singleAttribute == null || !singleAttribute.IsNameSetExplicit) ? defaultName : new XmlName(singleAttribute.Name);
			return new MessagePartDescription(xmlName.EncodedName, defaultNS)
			{
				Type = type,
				Index = index,
				AdditionalAttributesProvider = attrProvider
			};
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x0009BA00 File Offset: 0x00099C00
		internal MessageDescription CreateTypedMessageDescription(Type typedMessageType, ICustomAttributeProvider returnAttrProvider, XmlName returnValueName, string defaultNS, string action, MessageDirection direction)
		{
			bool flag = false;
			MessageContractAttribute singleAttribute = ServiceReflector.GetSingleAttribute<MessageContractAttribute>(typedMessageType);
			MessageDescriptionItems items;
			MessageDescription messageDescription;
			if (this.messages.TryGetValue(typedMessageType, out items))
			{
				messageDescription = new MessageDescription(action, direction, items);
				flag = true;
			}
			else
			{
				messageDescription = new MessageDescription(action, direction, null);
			}
			messageDescription.MessageType = typedMessageType;
			messageDescription.MessageName = new XmlName(NamingHelper.TypeName(typedMessageType));
			if (singleAttribute.IsWrapped)
			{
				messageDescription.Body.WrapperName = TypeLoader.GetWrapperName(singleAttribute.WrapperName, messageDescription.MessageName).EncodedName;
				messageDescription.Body.WrapperNamespace = (singleAttribute.WrapperNamespace ?? defaultNS);
			}
			List<MemberInfo> list = new List<MemberInfo>();
			Type type = typedMessageType;
			while (type != null && type != typeof(object) && type != typeof(ValueType))
			{
				if (!type.IsDefined(typeof(MessageContractAttribute), false))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxMessageContractBaseTypeNotValid", new object[]
					{
						type,
						typedMessageType
					})));
				}
				if (!messageDescription.HasProtectionLevel)
				{
					MessageContractAttribute requiredSingleAttribute = ServiceReflector.GetRequiredSingleAttribute<MessageContractAttribute>(type);
					if (requiredSingleAttribute.HasProtectionLevel)
					{
						messageDescription.ProtectionLevel = requiredSingleAttribute.ProtectionLevel;
					}
				}
				if (!flag)
				{
					foreach (MemberInfo memberInfo in type.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
					{
						if (memberInfo.MemberType == MemberTypes.Field || memberInfo.MemberType == MemberTypes.Property)
						{
							PropertyInfo propertyInfo = memberInfo as PropertyInfo;
							if (propertyInfo != null)
							{
								MethodInfo getMethod = propertyInfo.GetGetMethod(true);
								if (getMethod != null && TypeLoader.IsMethodOverriding(getMethod))
								{
									goto IL_1DC;
								}
								MethodInfo setMethod = propertyInfo.GetSetMethod(true);
								if (setMethod != null && TypeLoader.IsMethodOverriding(setMethod))
								{
									goto IL_1DC;
								}
							}
							if (memberInfo.IsDefined(typeof(MessageBodyMemberAttribute), false) || memberInfo.IsDefined(typeof(MessageHeaderAttribute), false) || memberInfo.IsDefined(typeof(MessageHeaderArrayAttribute), false) || memberInfo.IsDefined(typeof(MessagePropertyAttribute), false))
							{
								list.Add(memberInfo);
							}
						}
						IL_1DC:;
					}
				}
				type = type.BaseType;
			}
			if (flag)
			{
				return messageDescription;
			}
			List<MessagePartDescription> list2 = new List<MessagePartDescription>();
			List<MessageHeaderDescription> list3 = new List<MessageHeaderDescription>();
			for (int j = 0; j < list.Count; j++)
			{
				MemberInfo memberInfo2 = list[j];
				Type type2;
				if (memberInfo2.MemberType == MemberTypes.Property)
				{
					type2 = ((PropertyInfo)memberInfo2).PropertyType;
				}
				else
				{
					type2 = ((FieldInfo)memberInfo2).FieldType;
				}
				if (memberInfo2.IsDefined(typeof(MessageHeaderArrayAttribute), false) || memberInfo2.IsDefined(typeof(MessageHeaderAttribute), false))
				{
					list3.Add(this.CreateMessageHeaderDescription(type2, memberInfo2, new XmlName(memberInfo2.Name), defaultNS, j, -1));
				}
				else if (memberInfo2.IsDefined(typeof(MessagePropertyAttribute), false))
				{
					messageDescription.Properties.Add(this.CreateMessagePropertyDescription(memberInfo2, new XmlName(memberInfo2.Name), j));
				}
				else
				{
					list2.Add(this.CreateMessagePartDescription(type2, memberInfo2, new XmlName(memberInfo2.Name), defaultNS, j, -1));
				}
			}
			if (returnAttrProvider != null)
			{
				messageDescription.Body.ReturnValue = this.CreateMessagePartDescription(typeof(void), returnAttrProvider, returnValueName, defaultNS, 0, 0);
			}
			this.AddSortedParts<MessagePartDescription>(list2, messageDescription.Body.Parts);
			this.AddSortedParts<MessageHeaderDescription>(list3, messageDescription.Headers);
			this.messages.Add(typedMessageType, messageDescription.Items);
			return messageDescription;
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x0009BD96 File Offset: 0x00099F96
		private static bool IsMethodOverriding(MethodInfo method)
		{
			return method.IsVirtual && (method.Attributes & MethodAttributes.VtableLayoutMask) == MethodAttributes.PrivateScope;
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x0009BDB4 File Offset: 0x00099FB4
		private MessagePartDescription CreateMessagePartDescription(Type bodyType, ICustomAttributeProvider attrProvider, XmlName defaultName, string defaultNS, int parameterIndex, int serializationIndex)
		{
			MessageBodyMemberAttribute singleAttribute = ServiceReflector.GetSingleAttribute<MessageBodyMemberAttribute>(attrProvider, TypeLoader.messageContractMemberAttributes);
			MessagePartDescription messagePartDescription;
			if (singleAttribute == null)
			{
				messagePartDescription = new MessagePartDescription(defaultName.EncodedName, defaultNS);
				messagePartDescription.SerializationPosition = serializationIndex;
			}
			else
			{
				XmlName xmlName = singleAttribute.IsNameSetExplicit ? new XmlName(singleAttribute.Name) : defaultName;
				string ns = singleAttribute.IsNamespaceSetExplicit ? singleAttribute.Namespace : defaultNS;
				messagePartDescription = new MessagePartDescription(xmlName.EncodedName, ns);
				messagePartDescription.SerializationPosition = ((singleAttribute.Order < 0) ? serializationIndex : singleAttribute.Order);
				if (singleAttribute.HasProtectionLevel)
				{
					messagePartDescription.ProtectionLevel = singleAttribute.ProtectionLevel;
				}
			}
			if (attrProvider is MemberInfo)
			{
				messagePartDescription.MemberInfo = (MemberInfo)attrProvider;
			}
			messagePartDescription.Type = bodyType;
			messagePartDescription.Index = parameterIndex;
			return messagePartDescription;
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x0009BE74 File Offset: 0x0009A074
		private MessageHeaderDescription CreateMessageHeaderDescription(Type headerParameterType, ICustomAttributeProvider attrProvider, XmlName defaultName, string defaultNS, int parameterIndex, int serializationPosition)
		{
			MessageHeaderAttribute requiredSingleAttribute = ServiceReflector.GetRequiredSingleAttribute<MessageHeaderAttribute>(attrProvider, TypeLoader.messageContractMemberAttributes);
			XmlName xmlName = requiredSingleAttribute.IsNameSetExplicit ? new XmlName(requiredSingleAttribute.Name) : defaultName;
			string ns = requiredSingleAttribute.IsNamespaceSetExplicit ? requiredSingleAttribute.Namespace : defaultNS;
			MessageHeaderDescription messageHeaderDescription = new MessageHeaderDescription(xmlName.EncodedName, ns);
			messageHeaderDescription.UniquePartName = defaultName.EncodedName;
			if (requiredSingleAttribute is MessageHeaderArrayAttribute)
			{
				if (!headerParameterType.IsArray || headerParameterType.GetArrayRank() != 1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidMessageHeaderArrayType", new object[]
					{
						defaultName
					})));
				}
				messageHeaderDescription.Multiple = true;
				headerParameterType = headerParameterType.GetElementType();
			}
			messageHeaderDescription.Type = TypedHeaderManager.GetHeaderType(headerParameterType);
			messageHeaderDescription.TypedHeader = (headerParameterType != messageHeaderDescription.Type);
			if (messageHeaderDescription.TypedHeader)
			{
				if (requiredSingleAttribute.IsMustUnderstandSet || requiredSingleAttribute.IsRelaySet || requiredSingleAttribute.Actor != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxStaticMessageHeaderPropertiesNotAllowed", new object[]
					{
						defaultName
					})));
				}
			}
			else
			{
				messageHeaderDescription.Actor = requiredSingleAttribute.Actor;
				messageHeaderDescription.MustUnderstand = requiredSingleAttribute.MustUnderstand;
				messageHeaderDescription.Relay = requiredSingleAttribute.Relay;
			}
			messageHeaderDescription.SerializationPosition = serializationPosition;
			if (requiredSingleAttribute.HasProtectionLevel)
			{
				messageHeaderDescription.ProtectionLevel = requiredSingleAttribute.ProtectionLevel;
			}
			if (attrProvider is MemberInfo)
			{
				messageHeaderDescription.MemberInfo = (MemberInfo)attrProvider;
			}
			messageHeaderDescription.Index = parameterIndex;
			return messageHeaderDescription;
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x0009BFE0 File Offset: 0x0009A1E0
		private MessagePropertyDescription CreateMessagePropertyDescription(ICustomAttributeProvider attrProvider, XmlName defaultName, int parameterIndex)
		{
			MessagePropertyAttribute singleAttribute = ServiceReflector.GetSingleAttribute<MessagePropertyAttribute>(attrProvider, TypeLoader.messageContractMemberAttributes);
			XmlName xmlName = singleAttribute.IsNameSetExplicit ? new XmlName(singleAttribute.Name) : defaultName;
			MessagePropertyDescription messagePropertyDescription = new MessagePropertyDescription(xmlName.EncodedName);
			messagePropertyDescription.Index = parameterIndex;
			if (attrProvider is MemberInfo)
			{
				messagePropertyDescription.MemberInfo = (MemberInfo)attrProvider;
			}
			return messagePropertyDescription;
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x0009C038 File Offset: 0x0009A238
		internal static XmlName GetReturnValueName(XmlName methodName)
		{
			return new XmlName(methodName.EncodedName + "Result", true);
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x0009C050 File Offset: 0x0009A250
		internal static XmlName GetReturnValueName(string methodName)
		{
			return new XmlName(methodName + "Result");
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x0009C064 File Offset: 0x0009A264
		internal static Type GetParameterType(ParameterInfo parameterInfo)
		{
			Type parameterType = parameterInfo.ParameterType;
			if (parameterType.IsByRef)
			{
				return parameterType.GetElementType();
			}
			return parameterType;
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x0009C088 File Offset: 0x0009A288
		internal static XmlName GetWrapperName(string wrapperName, XmlName defaultName)
		{
			if (string.IsNullOrEmpty(wrapperName))
			{
				return defaultName;
			}
			return new XmlName(wrapperName);
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x0009C09C File Offset: 0x0009A29C
		private void AddSortedParts<T>(List<T> partDescriptionList, KeyedCollection<XmlQualifiedName, T> partDescriptionCollection) where T : MessagePartDescription
		{
			MessagePartDescription[] array = partDescriptionList.ToArray();
			MessagePartDescription[] array2 = array;
			if (array2.Length > 1)
			{
				Array.Sort<MessagePartDescription>(array2, new Comparison<MessagePartDescription>(TypeLoader.CompareMessagePartDescriptions));
			}
			MessagePartDescription[] array3 = array2;
			for (int i = 0; i < array3.Length; i++)
			{
				T t = (T)((object)array3[i]);
				if (partDescriptionCollection.Contains(new XmlQualifiedName(t.Name, t.Namespace)))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidMessageContractException(SR.GetString("SFxDuplicateMessageParts", new object[]
					{
						t.Name,
						t.Namespace
					})));
				}
				partDescriptionCollection.Add(t);
			}
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x0009C150 File Offset: 0x0009A350
		public static void ApplyServiceInheritance<IBehavior, TBehaviorCollection>(Type serviceType, TBehaviorCollection descriptionBehaviors, TypeLoader.ServiceInheritanceCallback<IBehavior, TBehaviorCollection> callback) where IBehavior : class where TBehaviorCollection : KeyedByTypeCollection<IBehavior>
		{
			Type type = serviceType;
			while (type != null)
			{
				TypeLoader.AddBehaviorsAtOneScope<IBehavior, TBehaviorCollection>(type, descriptionBehaviors, callback);
				type = type.BaseType;
			}
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x0009C17C File Offset: 0x0009A37C
		private static void AddBehaviorsAtOneScope<IBehavior, TBehaviorCollection>(Type type, TBehaviorCollection descriptionBehaviors, TypeLoader.ServiceInheritanceCallback<IBehavior, TBehaviorCollection> callback) where IBehavior : class where TBehaviorCollection : KeyedByTypeCollection<IBehavior>
		{
			KeyedByTypeCollection<IBehavior> keyedByTypeCollection = new KeyedByTypeCollection<IBehavior>();
			callback(type, keyedByTypeCollection);
			for (int i = 0; i < keyedByTypeCollection.Count; i++)
			{
				IBehavior behavior = keyedByTypeCollection[i];
				if (!descriptionBehaviors.Contains(behavior.GetType()))
				{
					if (behavior is ServiceBehaviorAttribute || behavior is CallbackBehaviorAttribute)
					{
						descriptionBehaviors.Insert(0, behavior);
					}
					else
					{
						descriptionBehaviors.Add(behavior);
					}
				}
			}
		}

		// Token: 0x0400225B RID: 8795
		private static Type[] messageContractMemberAttributes = new Type[]
		{
			typeof(MessageHeaderAttribute),
			typeof(MessageBodyMemberAttribute),
			typeof(MessagePropertyAttribute)
		};

		// Token: 0x0400225C RID: 8796
		private static Type[] formatterAttributes = new Type[]
		{
			typeof(XmlSerializerFormatAttribute),
			typeof(DataContractFormatAttribute)
		};

		// Token: 0x0400225D RID: 8797
		private static Type[] knownTypesMethodParamType = new Type[]
		{
			typeof(ICustomAttributeProvider)
		};

		// Token: 0x0400225E RID: 8798
		internal static DataContractFormatAttribute DefaultDataContractFormatAttribute = new DataContractFormatAttribute();

		// Token: 0x0400225F RID: 8799
		internal static XmlSerializerFormatAttribute DefaultXmlSerializerFormatAttribute = new XmlSerializerFormatAttribute();

		// Token: 0x04002260 RID: 8800
		private static readonly Type OperationContractAttributeType = typeof(OperationContractAttribute);

		// Token: 0x04002261 RID: 8801
		internal const string ReturnSuffix = "Result";

		// Token: 0x04002262 RID: 8802
		internal const string ResponseSuffix = "Response";

		// Token: 0x04002263 RID: 8803
		internal const string FaultSuffix = "Fault";

		// Token: 0x04002264 RID: 8804
		internal const BindingFlags DefaultBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04002265 RID: 8805
		private readonly object thisLock;

		// Token: 0x04002266 RID: 8806
		private readonly Dictionary<Type, ContractDescription> contracts;

		// Token: 0x04002267 RID: 8807
		private readonly Dictionary<Type, MessageDescriptionItems> messages;

		// Token: 0x02000BDE RID: 3038
		private abstract class OperationConsistencyVerifier
		{
			// Token: 0x06007551 RID: 30033 RVA: 0x001B6FFC File Offset: 0x001B51FC
			public virtual void VerifyParameterLength()
			{
			}

			// Token: 0x06007552 RID: 30034 RVA: 0x001B6FFE File Offset: 0x001B51FE
			public virtual void VerifyParameterType()
			{
			}

			// Token: 0x06007553 RID: 30035 RVA: 0x001B7000 File Offset: 0x001B5200
			public virtual void VerifyOutParameterType()
			{
			}

			// Token: 0x06007554 RID: 30036 RVA: 0x001B7002 File Offset: 0x001B5202
			public virtual void VerifyReturnType()
			{
			}

			// Token: 0x06007555 RID: 30037 RVA: 0x001B7004 File Offset: 0x001B5204
			public virtual void VerifyFaultContractAttribute()
			{
			}

			// Token: 0x06007556 RID: 30038 RVA: 0x001B7006 File Offset: 0x001B5206
			public virtual void VerifyKnownTypeAttribute()
			{
			}

			// Token: 0x06007557 RID: 30039 RVA: 0x001B7008 File Offset: 0x001B5208
			public virtual void VerifyIsOneWayStatus()
			{
			}

			// Token: 0x06007558 RID: 30040 RVA: 0x001B700A File Offset: 0x001B520A
			public virtual void VerifyActionAndReplyAction()
			{
			}
		}

		// Token: 0x02000BDF RID: 3039
		private class SyncAsyncOperationConsistencyVerifier : TypeLoader.OperationConsistencyVerifier
		{
			// Token: 0x0600755A RID: 30042 RVA: 0x001B7014 File Offset: 0x001B5214
			public SyncAsyncOperationConsistencyVerifier(OperationDescription syncOperation, OperationDescription asyncOperation)
			{
				this.syncOperation = syncOperation;
				this.asyncOperation = asyncOperation;
				this.syncInputs = ServiceReflector.GetInputParameters(this.syncOperation.SyncMethod, false);
				this.asyncInputs = ServiceReflector.GetInputParameters(this.asyncOperation.BeginMethod, true);
				this.syncOutputs = ServiceReflector.GetOutputParameters(this.syncOperation.SyncMethod, false);
				this.asyncOutputs = ServiceReflector.GetOutputParameters(this.asyncOperation.EndMethod, true);
			}

			// Token: 0x0600755B RID: 30043 RVA: 0x001B7094 File Offset: 0x001B5294
			public override void VerifyParameterLength()
			{
				if (this.syncInputs.Length != this.asyncInputs.Length || this.syncOutputs.Length != this.asyncOutputs.Length)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SyncAsyncMatchConsistency_Parameters5", new object[]
					{
						this.syncOperation.SyncMethod.Name,
						this.syncOperation.SyncMethod.DeclaringType,
						this.asyncOperation.BeginMethod.Name,
						this.asyncOperation.EndMethod.Name,
						this.syncOperation.Name
					})));
				}
			}

			// Token: 0x0600755C RID: 30044 RVA: 0x001B7140 File Offset: 0x001B5340
			public override void VerifyParameterType()
			{
				for (int i = 0; i < this.syncInputs.Length; i++)
				{
					if (this.syncInputs[i].ParameterType != this.asyncInputs[i].ParameterType)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SyncAsyncMatchConsistency_Parameters5", new object[]
						{
							this.syncOperation.SyncMethod.Name,
							this.syncOperation.SyncMethod.DeclaringType,
							this.asyncOperation.BeginMethod.Name,
							this.asyncOperation.EndMethod.Name,
							this.syncOperation.Name
						})));
					}
				}
			}

			// Token: 0x0600755D RID: 30045 RVA: 0x001B7204 File Offset: 0x001B5404
			public override void VerifyOutParameterType()
			{
				for (int i = 0; i < this.syncOutputs.Length; i++)
				{
					if (this.syncOutputs[i].ParameterType != this.asyncOutputs[i].ParameterType)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SyncAsyncMatchConsistency_Parameters5", new object[]
						{
							this.syncOperation.SyncMethod.Name,
							this.syncOperation.SyncMethod.DeclaringType,
							this.asyncOperation.BeginMethod.Name,
							this.asyncOperation.EndMethod.Name,
							this.syncOperation.Name
						})));
					}
				}
			}

			// Token: 0x0600755E RID: 30046 RVA: 0x001B72C8 File Offset: 0x001B54C8
			public override void VerifyReturnType()
			{
				if (this.syncOperation.SyncMethod.ReturnType != this.syncOperation.EndMethod.ReturnType)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SyncAsyncMatchConsistency_ReturnType5", new object[]
					{
						this.syncOperation.SyncMethod.Name,
						this.syncOperation.SyncMethod.DeclaringType,
						this.asyncOperation.BeginMethod.Name,
						this.asyncOperation.EndMethod.Name,
						this.syncOperation.Name
					})));
				}
			}

			// Token: 0x0600755F RID: 30047 RVA: 0x001B7378 File Offset: 0x001B5578
			public override void VerifyFaultContractAttribute()
			{
				if (this.asyncOperation.Faults.Count != 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SyncAsyncMatchConsistency_Attributes6", new object[]
					{
						this.syncOperation.SyncMethod.Name,
						this.syncOperation.SyncMethod.DeclaringType,
						this.asyncOperation.BeginMethod.Name,
						this.asyncOperation.EndMethod.Name,
						this.syncOperation.Name,
						typeof(FaultContractAttribute).Name
					})));
				}
			}

			// Token: 0x06007560 RID: 30048 RVA: 0x001B7428 File Offset: 0x001B5628
			public override void VerifyKnownTypeAttribute()
			{
				if (this.asyncOperation.KnownTypes.Count != 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SyncAsyncMatchConsistency_Attributes6", new object[]
					{
						this.syncOperation.SyncMethod.Name,
						this.syncOperation.SyncMethod.DeclaringType,
						this.asyncOperation.BeginMethod.Name,
						this.asyncOperation.EndMethod.Name,
						this.syncOperation.Name,
						typeof(ServiceKnownTypeAttribute).Name
					})));
				}
			}

			// Token: 0x06007561 RID: 30049 RVA: 0x001B74D8 File Offset: 0x001B56D8
			public override void VerifyIsOneWayStatus()
			{
				if (this.syncOperation.Messages.Count != this.asyncOperation.Messages.Count)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SyncAsyncMatchConsistency_Property6", new object[]
					{
						this.syncOperation.SyncMethod.Name,
						this.syncOperation.SyncMethod.DeclaringType,
						this.asyncOperation.BeginMethod.Name,
						this.asyncOperation.EndMethod.Name,
						this.syncOperation.Name,
						"IsOneWay"
					})));
				}
			}

			// Token: 0x06007562 RID: 30050 RVA: 0x001B758C File Offset: 0x001B578C
			public override void VerifyActionAndReplyAction()
			{
				for (int i = 0; i < this.syncOperation.Messages.Count; i++)
				{
					if (this.syncOperation.Messages[i].Action != this.asyncOperation.Messages[i].Action)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SyncAsyncMatchConsistency_Property6", new object[]
						{
							this.syncOperation.SyncMethod.Name,
							this.syncOperation.SyncMethod.DeclaringType,
							this.asyncOperation.BeginMethod.Name,
							this.asyncOperation.EndMethod.Name,
							this.syncOperation.Name,
							(i == 0) ? "Action" : "ReplyAction"
						})));
					}
				}
			}

			// Token: 0x04004254 RID: 16980
			private OperationDescription syncOperation;

			// Token: 0x04004255 RID: 16981
			private OperationDescription asyncOperation;

			// Token: 0x04004256 RID: 16982
			private ParameterInfo[] syncInputs;

			// Token: 0x04004257 RID: 16983
			private ParameterInfo[] asyncInputs;

			// Token: 0x04004258 RID: 16984
			private ParameterInfo[] syncOutputs;

			// Token: 0x04004259 RID: 16985
			private ParameterInfo[] asyncOutputs;
		}

		// Token: 0x02000BE0 RID: 3040
		private class SyncTaskOperationConsistencyVerifier : TypeLoader.OperationConsistencyVerifier
		{
			// Token: 0x06007563 RID: 30051 RVA: 0x001B767C File Offset: 0x001B587C
			public SyncTaskOperationConsistencyVerifier(OperationDescription syncOperation, OperationDescription taskOperation)
			{
				this.syncOperation = syncOperation;
				this.taskOperation = taskOperation;
				this.syncInputs = ServiceReflector.GetInputParameters(this.syncOperation.SyncMethod, false);
				this.taskInputs = ServiceReflector.GetInputParameters(this.taskOperation.TaskMethod, false);
			}

			// Token: 0x06007564 RID: 30052 RVA: 0x001B76CC File Offset: 0x001B58CC
			public override void VerifyParameterLength()
			{
				if (this.syncInputs.Length != this.taskInputs.Length)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SyncTaskMatchConsistency_Parameters5", new object[]
					{
						this.syncOperation.SyncMethod.Name,
						this.syncOperation.SyncMethod.DeclaringType,
						this.taskOperation.TaskMethod.Name,
						this.syncOperation.Name
					})));
				}
			}

			// Token: 0x06007565 RID: 30053 RVA: 0x001B7754 File Offset: 0x001B5954
			public override void VerifyParameterType()
			{
				for (int i = 0; i < this.syncInputs.Length; i++)
				{
					if (this.syncInputs[i].ParameterType != this.taskInputs[i].ParameterType)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SyncTaskMatchConsistency_Parameters5", new object[]
						{
							this.syncOperation.SyncMethod.Name,
							this.syncOperation.SyncMethod.DeclaringType,
							this.taskOperation.TaskMethod.Name,
							this.syncOperation.Name
						})));
					}
				}
			}

			// Token: 0x06007566 RID: 30054 RVA: 0x001B7804 File Offset: 0x001B5A04
			public override void VerifyReturnType()
			{
				if (this.syncOperation.SyncMethod.ReturnType != this.syncOperation.TaskTResult)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SyncTaskMatchConsistency_ReturnType5", new object[]
					{
						this.syncOperation.SyncMethod.Name,
						this.syncOperation.SyncMethod.DeclaringType,
						this.taskOperation.TaskMethod.Name,
						this.syncOperation.Name
					})));
				}
			}

			// Token: 0x06007567 RID: 30055 RVA: 0x001B789C File Offset: 0x001B5A9C
			public override void VerifyFaultContractAttribute()
			{
				if (this.taskOperation.Faults.Count != 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SyncTaskMatchConsistency_Attributes6", new object[]
					{
						this.syncOperation.SyncMethod.Name,
						this.syncOperation.SyncMethod.DeclaringType,
						this.taskOperation.TaskMethod.Name,
						this.syncOperation.Name,
						typeof(FaultContractAttribute).Name
					})));
				}
			}

			// Token: 0x06007568 RID: 30056 RVA: 0x001B7934 File Offset: 0x001B5B34
			public override void VerifyKnownTypeAttribute()
			{
				if (this.taskOperation.KnownTypes.Count != 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SyncTaskMatchConsistency_Attributes6", new object[]
					{
						this.syncOperation.SyncMethod.Name,
						this.syncOperation.SyncMethod.DeclaringType,
						this.taskOperation.TaskMethod.Name,
						this.syncOperation.Name,
						typeof(ServiceKnownTypeAttribute).Name
					})));
				}
			}

			// Token: 0x06007569 RID: 30057 RVA: 0x001B79CC File Offset: 0x001B5BCC
			public override void VerifyIsOneWayStatus()
			{
				if (this.syncOperation.Messages.Count != this.taskOperation.Messages.Count)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SyncTaskMatchConsistency_Property6", new object[]
					{
						this.syncOperation.SyncMethod.Name,
						this.syncOperation.SyncMethod.DeclaringType,
						this.taskOperation.TaskMethod.Name,
						this.syncOperation.Name,
						"IsOneWay"
					})));
				}
			}

			// Token: 0x0600756A RID: 30058 RVA: 0x001B7A6C File Offset: 0x001B5C6C
			public override void VerifyActionAndReplyAction()
			{
				for (int i = 0; i < this.syncOperation.Messages.Count; i++)
				{
					if (this.syncOperation.Messages[i].Action != this.taskOperation.Messages[i].Action)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SyncTaskMatchConsistency_Property6", new object[]
						{
							this.syncOperation.SyncMethod.Name,
							this.syncOperation.SyncMethod.DeclaringType,
							this.taskOperation.TaskMethod.Name,
							this.syncOperation.Name,
							(i == 0) ? "Action" : "ReplyAction"
						})));
					}
				}
			}

			// Token: 0x0400425A RID: 16986
			private OperationDescription syncOperation;

			// Token: 0x0400425B RID: 16987
			private OperationDescription taskOperation;

			// Token: 0x0400425C RID: 16988
			private ParameterInfo[] syncInputs;

			// Token: 0x0400425D RID: 16989
			private ParameterInfo[] taskInputs;
		}

		// Token: 0x02000BE1 RID: 3041
		private class TaskAsyncOperationConsistencyVerifier : TypeLoader.OperationConsistencyVerifier
		{
			// Token: 0x0600756B RID: 30059 RVA: 0x001B7B48 File Offset: 0x001B5D48
			public TaskAsyncOperationConsistencyVerifier(OperationDescription taskOperation, OperationDescription asyncOperation)
			{
				this.taskOperation = taskOperation;
				this.asyncOperation = asyncOperation;
				this.taskInputs = ServiceReflector.GetInputParameters(this.taskOperation.TaskMethod, false);
				this.asyncInputs = ServiceReflector.GetInputParameters(this.asyncOperation.BeginMethod, true);
			}

			// Token: 0x0600756C RID: 30060 RVA: 0x001B7B98 File Offset: 0x001B5D98
			public override void VerifyParameterLength()
			{
				if (this.taskInputs.Length != this.asyncInputs.Length)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TaskAsyncMatchConsistency_Parameters5", new object[]
					{
						this.taskOperation.TaskMethod.Name,
						this.taskOperation.TaskMethod.DeclaringType,
						this.asyncOperation.BeginMethod.Name,
						this.asyncOperation.EndMethod.Name,
						this.taskOperation.Name
					})));
				}
			}

			// Token: 0x0600756D RID: 30061 RVA: 0x001B7C34 File Offset: 0x001B5E34
			public override void VerifyParameterType()
			{
				for (int i = 0; i < this.taskInputs.Length; i++)
				{
					if (this.taskInputs[i].ParameterType != this.asyncInputs[i].ParameterType)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TaskAsyncMatchConsistency_Parameters5", new object[]
						{
							this.taskOperation.TaskMethod.Name,
							this.taskOperation.TaskMethod.DeclaringType,
							this.asyncOperation.BeginMethod.Name,
							this.asyncOperation.EndMethod.Name,
							this.taskOperation.Name
						})));
					}
				}
			}

			// Token: 0x0600756E RID: 30062 RVA: 0x001B7CF8 File Offset: 0x001B5EF8
			public override void VerifyReturnType()
			{
				if (this.taskOperation.TaskTResult != this.asyncOperation.EndMethod.ReturnType)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TaskAsyncMatchConsistency_ReturnType5", new object[]
					{
						this.taskOperation.TaskMethod.Name,
						this.taskOperation.TaskMethod.DeclaringType,
						this.asyncOperation.BeginMethod.Name,
						this.asyncOperation.EndMethod.Name,
						this.taskOperation.Name
					})));
				}
			}

			// Token: 0x0600756F RID: 30063 RVA: 0x001B7DA4 File Offset: 0x001B5FA4
			public override void VerifyFaultContractAttribute()
			{
				if (this.asyncOperation.Faults.Count != 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TaskAsyncMatchConsistency_Attributes6", new object[]
					{
						this.taskOperation.TaskMethod.Name,
						this.taskOperation.TaskMethod.DeclaringType,
						this.asyncOperation.BeginMethod.Name,
						this.asyncOperation.EndMethod.Name,
						this.taskOperation.Name,
						typeof(FaultContractAttribute).Name
					})));
				}
			}

			// Token: 0x06007570 RID: 30064 RVA: 0x001B7E54 File Offset: 0x001B6054
			public override void VerifyKnownTypeAttribute()
			{
				if (this.asyncOperation.KnownTypes.Count != 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TaskAsyncMatchConsistency_Attributes6", new object[]
					{
						this.taskOperation.TaskMethod.Name,
						this.taskOperation.TaskMethod.DeclaringType,
						this.asyncOperation.BeginMethod.Name,
						this.asyncOperation.EndMethod.Name,
						this.taskOperation.Name,
						typeof(ServiceKnownTypeAttribute).Name
					})));
				}
			}

			// Token: 0x06007571 RID: 30065 RVA: 0x001B7F04 File Offset: 0x001B6104
			public override void VerifyIsOneWayStatus()
			{
				if (this.taskOperation.Messages.Count != this.asyncOperation.Messages.Count)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TaskAsyncMatchConsistency_Property6", new object[]
					{
						this.taskOperation.TaskMethod.Name,
						this.taskOperation.TaskMethod.DeclaringType,
						this.asyncOperation.BeginMethod.Name,
						this.asyncOperation.EndMethod.Name,
						this.taskOperation.Name,
						"IsOneWay"
					})));
				}
			}

			// Token: 0x06007572 RID: 30066 RVA: 0x001B7FB8 File Offset: 0x001B61B8
			public override void VerifyActionAndReplyAction()
			{
				for (int i = 0; i < this.taskOperation.Messages.Count; i++)
				{
					if (this.taskOperation.Messages[i].Action != this.asyncOperation.Messages[i].Action)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TaskAsyncMatchConsistency_Property6", new object[]
						{
							this.taskOperation.TaskMethod.Name,
							this.taskOperation.TaskMethod.DeclaringType,
							this.asyncOperation.BeginMethod.Name,
							this.asyncOperation.EndMethod.Name,
							this.taskOperation.Name,
							(i == 0) ? "Action" : "ReplyAction"
						})));
					}
				}
			}

			// Token: 0x0400425E RID: 16990
			private OperationDescription taskOperation;

			// Token: 0x0400425F RID: 16991
			private OperationDescription asyncOperation;

			// Token: 0x04004260 RID: 16992
			private ParameterInfo[] taskInputs;

			// Token: 0x04004261 RID: 16993
			private ParameterInfo[] asyncInputs;
		}

		// Token: 0x02000BE2 RID: 3042
		private class ContractReflectionInfo
		{
			// Token: 0x04004262 RID: 16994
			internal Type iface;

			// Token: 0x04004263 RID: 16995
			internal Type callbackiface;
		}

		// Token: 0x02000BE3 RID: 3043
		// (Invoke) Token: 0x06007575 RID: 30069
		public delegate void ServiceInheritanceCallback<IBehavior, TBehaviorCollection>(Type currentType, KeyedByTypeCollection<IBehavior> behaviors);
	}
}
