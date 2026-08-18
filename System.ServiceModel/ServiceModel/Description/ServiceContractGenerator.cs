using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net.Security;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace System.ServiceModel.Description
{
	// Token: 0x0200041F RID: 1055
	public class ServiceContractGenerator
	{
		// Token: 0x06002862 RID: 10338 RVA: 0x000979CA File Offset: 0x00095BCA
		public ServiceContractGenerator() : this(null, null)
		{
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x000979D4 File Offset: 0x00095BD4
		public ServiceContractGenerator(Configuration targetConfig) : this(null, targetConfig)
		{
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x000979DE File Offset: 0x00095BDE
		public ServiceContractGenerator(CodeCompileUnit targetCompileUnit) : this(targetCompileUnit, null)
		{
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x000979E8 File Offset: 0x00095BE8
		public ServiceContractGenerator(CodeCompileUnit targetCompileUnit, Configuration targetConfig)
		{
			this.compileUnit = (targetCompileUnit ?? new CodeCompileUnit());
			this.namespaceManager = new ServiceContractGenerator.NamespaceHelper(this.compileUnit.Namespaces);
			this.AddReferencedAssembly(typeof(ServiceContractGenerator).Assembly);
			this.configuration = targetConfig;
			if (targetConfig != null)
			{
				this.configWriter = new ConfigWriter(targetConfig);
			}
			this.generatedTypes = new Dictionary<ContractDescription, ServiceContractGenerationContext>();
			this.generatedOperations = new Dictionary<OperationDescription, OperationContractGenerationContext>();
			this.referencedTypes = new Dictionary<ContractDescription, Type>();
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x00097A85 File Offset: 0x00095C85
		internal CodeTypeReference GetCodeTypeReference(Type type)
		{
			this.AddReferencedAssembly(type.Assembly);
			return new CodeTypeReference(type);
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x00097A9C File Offset: 0x00095C9C
		internal void AddReferencedAssembly(Assembly assembly)
		{
			string fileName = Path.GetFileName(assembly.Location);
			bool flag = false;
			foreach (string strA in this.compileUnit.ReferencedAssemblies)
			{
				if (string.Compare(strA, fileName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.compileUnit.ReferencedAssemblies.Add(fileName);
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06002868 RID: 10344 RVA: 0x00097B24 File Offset: 0x00095D24
		// (set) Token: 0x06002869 RID: 10345 RVA: 0x00097B31 File Offset: 0x00095D31
		public ServiceContractGenerationOptions Options
		{
			get
			{
				return this.options.Options;
			}
			set
			{
				this.options = new ServiceContractGenerator.OptionsHelper(value);
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600286A RID: 10346 RVA: 0x00097B3F File Offset: 0x00095D3F
		internal ServiceContractGenerator.OptionsHelper OptionsInternal
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x0600286B RID: 10347 RVA: 0x00097B47 File Offset: 0x00095D47
		public Dictionary<ContractDescription, Type> ReferencedTypes
		{
			get
			{
				return this.referencedTypes;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x0600286C RID: 10348 RVA: 0x00097B4F File Offset: 0x00095D4F
		public CodeCompileUnit TargetCompileUnit
		{
			get
			{
				return this.compileUnit;
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x0600286D RID: 10349 RVA: 0x00097B57 File Offset: 0x00095D57
		public Configuration Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x0600286E RID: 10350 RVA: 0x00097B5F File Offset: 0x00095D5F
		public Dictionary<string, string> NamespaceMappings
		{
			get
			{
				return this.NamespaceManager.NamespaceMappings;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x0600286F RID: 10351 RVA: 0x00097B6C File Offset: 0x00095D6C
		public Collection<MetadataConversionError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06002870 RID: 10352 RVA: 0x00097B74 File Offset: 0x00095D74
		internal ServiceContractGenerator.NamespaceHelper NamespaceManager
		{
			get
			{
				return this.namespaceManager;
			}
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x00097B7C File Offset: 0x00095D7C
		public void GenerateBinding(Binding binding, out string bindingSectionName, out string configurationName)
		{
			this.configWriter.WriteBinding(binding, out bindingSectionName, out configurationName);
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x00097B8C File Offset: 0x00095D8C
		public CodeTypeReference GenerateServiceEndpoint(ServiceEndpoint endpoint, out ChannelEndpointElement channelElement)
		{
			if (endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
			}
			if (this.configuration == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxServiceContractGeneratorConfigRequired")));
			}
			Type type;
			CodeTypeReference codeTypeReference;
			string typeName;
			if (this.referencedTypes.TryGetValue(endpoint.Contract, out type))
			{
				codeTypeReference = this.GetCodeTypeReference(type);
				typeName = type.FullName;
			}
			else
			{
				codeTypeReference = this.GenerateServiceContractType(endpoint.Contract);
				typeName = codeTypeReference.BaseType;
			}
			channelElement = this.configWriter.WriteChannelDescription(endpoint, typeName);
			return codeTypeReference;
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x00097C18 File Offset: 0x00095E18
		public CodeTypeReference GenerateServiceContractType(ContractDescription contractDescription)
		{
			CodeTypeReference result = this.GenerateServiceContractTypeInternal(contractDescription);
			CodeGenerator.ValidateIdentifiers(this.TargetCompileUnit);
			return result;
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x00097C3C File Offset: 0x00095E3C
		private CodeTypeReference GenerateServiceContractTypeInternal(ContractDescription contractDescription)
		{
			if (contractDescription == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contractDescription");
			}
			Type type;
			if (this.referencedTypes.TryGetValue(contractDescription, out type))
			{
				return this.GetCodeTypeReference(type);
			}
			CodeNamespace codeNamespace = this.NamespaceManager.EnsureNamespace(contractDescription.Namespace);
			ServiceContractGenerationContext serviceContractGenerationContext;
			if (!this.generatedTypes.TryGetValue(contractDescription, out serviceContractGenerationContext))
			{
				serviceContractGenerationContext = new ServiceContractGenerator.ContextInitializer(this, new ServiceContractGenerator.CodeTypeFactory(this, this.options.IsSet(ServiceContractGenerationOptions.InternalTypes))).CreateContext(contractDescription);
				ServiceContractGenerator.ExtensionsHelper.CallContractExtensions(this.GetBeforeExtensionsBuiltInContractGenerators(), serviceContractGenerationContext);
				ServiceContractGenerator.ExtensionsHelper.CallOperationExtensions(this.GetBeforeExtensionsBuiltInOperationGenerators(), serviceContractGenerationContext);
				ServiceContractGenerator.ExtensionsHelper.CallBehaviorExtensions(serviceContractGenerationContext);
				ServiceContractGenerator.ExtensionsHelper.CallContractExtensions(this.GetAfterExtensionsBuiltInContractGenerators(), serviceContractGenerationContext);
				ServiceContractGenerator.ExtensionsHelper.CallOperationExtensions(this.GetAfterExtensionsBuiltInOperationGenerators(), serviceContractGenerationContext);
				this.generatedTypes.Add(contractDescription, serviceContractGenerationContext);
			}
			return serviceContractGenerationContext.ContractTypeReference;
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x00097CFE File Offset: 0x00095EFE
		private IEnumerable<IServiceContractGenerationExtension> GetBeforeExtensionsBuiltInContractGenerators()
		{
			return EmptyArray<IServiceContractGenerationExtension>.Instance;
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x00097D05 File Offset: 0x00095F05
		private IEnumerable<IOperationContractGenerationExtension> GetBeforeExtensionsBuiltInOperationGenerators()
		{
			yield return new ServiceContractGenerator.FaultContractAttributeGenerator();
			yield return new ServiceContractGenerator.TransactionFlowAttributeGenerator();
			yield break;
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x00097D0E File Offset: 0x00095F0E
		private IEnumerable<IServiceContractGenerationExtension> GetAfterExtensionsBuiltInContractGenerators()
		{
			if (this.options.IsSet(ServiceContractGenerationOptions.ChannelInterface))
			{
				yield return new ServiceContractGenerator.ChannelInterfaceGenerator();
			}
			if (this.options.IsSet(ServiceContractGenerationOptions.ClientClass))
			{
				bool tryAddHelperMethod = !this.options.IsSet(ServiceContractGenerationOptions.TypedMessages);
				bool generateEventAsyncMethods = this.options.IsSet(ServiceContractGenerationOptions.EventBasedAsynchronousMethods);
				yield return new ClientClassGenerator(tryAddHelperMethod, generateEventAsyncMethods);
			}
			yield break;
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x00097D1E File Offset: 0x00095F1E
		private IEnumerable<IOperationContractGenerationExtension> GetAfterExtensionsBuiltInOperationGenerators()
		{
			return EmptyArray<IOperationContractGenerationExtension>.Instance;
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x00097D25 File Offset: 0x00095F25
		internal static CodeExpression GetEnumReference<EnumType>(EnumType value)
		{
			return new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(EnumType)), Enum.Format(typeof(EnumType), value, "G"));
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x0600287A RID: 10362 RVA: 0x00097D55 File Offset: 0x00095F55
		internal Dictionary<MessageDescription, CodeTypeReference> GeneratedTypedMessages
		{
			get
			{
				if (this.generatedTypedMessages == null)
				{
					this.generatedTypedMessages = new Dictionary<MessageDescription, CodeTypeReference>(ServiceContractGenerator.MessageDescriptionComparer.Singleton);
				}
				return this.generatedTypedMessages;
			}
		}

		// Token: 0x0400223C RID: 8764
		private CodeCompileUnit compileUnit;

		// Token: 0x0400223D RID: 8765
		private ConfigWriter configWriter;

		// Token: 0x0400223E RID: 8766
		private Configuration configuration;

		// Token: 0x0400223F RID: 8767
		private ServiceContractGenerator.NamespaceHelper namespaceManager;

		// Token: 0x04002240 RID: 8768
		private ServiceContractGenerator.OptionsHelper options = new ServiceContractGenerator.OptionsHelper(ServiceContractGenerationOptions.ChannelInterface | ServiceContractGenerationOptions.ClientClass);

		// Token: 0x04002241 RID: 8769
		private Dictionary<ContractDescription, Type> referencedTypes;

		// Token: 0x04002242 RID: 8770
		private Dictionary<ContractDescription, ServiceContractGenerationContext> generatedTypes;

		// Token: 0x04002243 RID: 8771
		private Dictionary<OperationDescription, OperationContractGenerationContext> generatedOperations;

		// Token: 0x04002244 RID: 8772
		private Dictionary<MessageDescription, CodeTypeReference> generatedTypedMessages;

		// Token: 0x04002245 RID: 8773
		private Collection<MetadataConversionError> errors = new Collection<MetadataConversionError>();

		// Token: 0x02000BD0 RID: 3024
		internal class ContextInitializer
		{
			// Token: 0x06007503 RID: 29955 RVA: 0x001B58A0 File Offset: 0x001B3AA0
			internal ContextInitializer(ServiceContractGenerator parent, ServiceContractGenerator.CodeTypeFactory typeFactory)
			{
				this.parent = parent;
				this.typeFactory = typeFactory;
				this.asyncMethods = parent.OptionsInternal.IsSet(ServiceContractGenerationOptions.AsynchronousMethods);
				this.taskMethod = parent.OptionsInternal.IsSet(ServiceContractGenerationOptions.TaskBasedAsynchronousMethod);
			}

			// Token: 0x06007504 RID: 29956 RVA: 0x001B58EC File Offset: 0x001B3AEC
			public ServiceContractGenerationContext CreateContext(ContractDescription contractDescription)
			{
				this.VisitContract(contractDescription);
				return this.context;
			}

			// Token: 0x06007505 RID: 29957 RVA: 0x001B58FC File Offset: 0x001B3AFC
			private void VisitContract(ContractDescription contract)
			{
				this.Visit(contract);
				foreach (OperationDescription operationDescription in contract.Operations)
				{
					this.Visit(operationDescription);
				}
			}

			// Token: 0x06007506 RID: 29958 RVA: 0x001B5950 File Offset: 0x001B3B50
			private void Visit(ContractDescription contractDescription)
			{
				bool flag = ServiceContractGenerator.ContextInitializer.IsDuplex(contractDescription);
				this.contractMemberScope = new UniqueCodeIdentifierScope();
				this.callbackMemberScope = (flag ? new UniqueCodeIdentifierScope() : null);
				UniqueCodeNamespaceScope uniqueCodeNamespaceScope = new UniqueCodeNamespaceScope(this.parent.NamespaceManager.EnsureNamespace(contractDescription.Namespace));
				CodeTypeDeclaration codeTypeDeclaration = this.typeFactory.CreateInterfaceType();
				CodeTypeReference contractTypeReference = uniqueCodeNamespaceScope.AddUnique(codeTypeDeclaration, contractDescription.CodeName, "IContract");
				CodeTypeDeclaration codeTypeDeclaration2 = null;
				CodeTypeReference duplexCallbackTypeReference = null;
				if (flag)
				{
					codeTypeDeclaration2 = this.typeFactory.CreateInterfaceType();
					duplexCallbackTypeReference = uniqueCodeNamespaceScope.AddUnique(codeTypeDeclaration2, contractDescription.CodeName + "Callback", "IContract");
				}
				this.context = new ServiceContractGenerationContext(this.parent, contractDescription, codeTypeDeclaration, codeTypeDeclaration2);
				this.context.Namespace = uniqueCodeNamespaceScope.CodeNamespace;
				this.context.TypeFactory = this.typeFactory;
				this.context.ContractTypeReference = contractTypeReference;
				this.context.DuplexCallbackTypeReference = duplexCallbackTypeReference;
				this.AddServiceContractAttribute(this.context);
			}

			// Token: 0x06007507 RID: 29959 RVA: 0x001B5A4C File Offset: 0x001B3C4C
			private void Visit(OperationDescription operationDescription)
			{
				bool flag = operationDescription.IsServerInitiated();
				CodeTypeDeclaration codeTypeDeclaration = flag ? this.context.DuplexCallbackType : this.context.ContractType;
				UniqueCodeIdentifierScope uniqueCodeIdentifierScope = flag ? this.callbackMemberScope : this.contractMemberScope;
				string text = uniqueCodeIdentifierScope.AddUnique(operationDescription.CodeName, "Method");
				CodeMemberMethod codeMemberMethod = new CodeMemberMethod();
				codeMemberMethod.Name = text;
				codeTypeDeclaration.Members.Add(codeMemberMethod);
				CodeMemberMethod codeMemberMethod2 = null;
				CodeMemberMethod codeMemberMethod3 = null;
				OperationContractGenerationContext operationContractGenerationContext;
				if (this.asyncMethods)
				{
					codeMemberMethod2 = new CodeMemberMethod();
					codeMemberMethod2.Name = "Begin" + text;
					codeMemberMethod2.Parameters.Add(new CodeParameterDeclarationExpression(this.context.ServiceContractGenerator.GetCodeTypeReference(typeof(AsyncCallback)), "callback"));
					codeMemberMethod2.Parameters.Add(new CodeParameterDeclarationExpression(this.context.ServiceContractGenerator.GetCodeTypeReference(typeof(object)), "asyncState"));
					codeMemberMethod2.ReturnType = this.context.ServiceContractGenerator.GetCodeTypeReference(typeof(IAsyncResult));
					codeTypeDeclaration.Members.Add(codeMemberMethod2);
					codeMemberMethod3 = new CodeMemberMethod();
					codeMemberMethod3.Name = "End" + text;
					codeMemberMethod3.Parameters.Add(new CodeParameterDeclarationExpression(this.context.ServiceContractGenerator.GetCodeTypeReference(typeof(IAsyncResult)), "result"));
					codeTypeDeclaration.Members.Add(codeMemberMethod3);
					operationContractGenerationContext = new OperationContractGenerationContext(this.parent, this.context, operationDescription, codeTypeDeclaration, codeMemberMethod, codeMemberMethod2, codeMemberMethod3);
				}
				else
				{
					operationContractGenerationContext = new OperationContractGenerationContext(this.parent, this.context, operationDescription, codeTypeDeclaration, codeMemberMethod);
				}
				if (this.taskMethod)
				{
					if (flag)
					{
						if (codeMemberMethod2 == null)
						{
							operationContractGenerationContext = new OperationContractGenerationContext(this.parent, this.context, operationDescription, codeTypeDeclaration, codeMemberMethod);
						}
						else
						{
							operationContractGenerationContext = new OperationContractGenerationContext(this.parent, this.context, operationDescription, codeTypeDeclaration, codeMemberMethod, codeMemberMethod2, codeMemberMethod3);
						}
					}
					else
					{
						CodeMemberMethod value = new CodeMemberMethod
						{
							Name = text + "Async"
						};
						codeTypeDeclaration.Members.Add(value);
						if (codeMemberMethod2 == null)
						{
							operationContractGenerationContext = new OperationContractGenerationContext(this.parent, this.context, operationDescription, codeTypeDeclaration, codeMemberMethod, value);
						}
						else
						{
							operationContractGenerationContext = new OperationContractGenerationContext(this.parent, this.context, operationDescription, codeTypeDeclaration, codeMemberMethod, codeMemberMethod2, codeMemberMethod3, value);
						}
					}
				}
				operationContractGenerationContext.DeclaringTypeReference = (operationDescription.IsServerInitiated() ? this.context.DuplexCallbackTypeReference : this.context.ContractTypeReference);
				this.context.Operations.Add(operationContractGenerationContext);
				this.AddOperationContractAttributes(operationContractGenerationContext);
			}

			// Token: 0x06007508 RID: 29960 RVA: 0x001B5CF0 File Offset: 0x001B3EF0
			private void AddServiceContractAttribute(ServiceContractGenerationContext context)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(context.ServiceContractGenerator.GetCodeTypeReference(typeof(ServiceContractAttribute)));
				if (context.ContractType.Name != context.Contract.CodeName)
				{
					string value = (NamingHelper.XmlName(context.Contract.CodeName) == context.Contract.Name) ? context.Contract.CodeName : context.Contract.Name;
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Name", new CodePrimitiveExpression(value)));
				}
				if ("http://tempuri.org/" != context.Contract.Namespace)
				{
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(context.Contract.Namespace)));
				}
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("ConfigurationName", new CodePrimitiveExpression(ServiceContractGenerator.NamespaceHelper.GetCodeTypeReference(context.Namespace, context.ContractType).BaseType)));
				if (context.Contract.HasProtectionLevel)
				{
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("ProtectionLevel", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(ProtectionLevel)), context.Contract.ProtectionLevel.ToString())));
				}
				if (context.DuplexCallbackType != null)
				{
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("CallbackContract", new CodeTypeOfExpression(context.DuplexCallbackTypeReference)));
				}
				if (context.Contract.SessionMode != SessionMode.Allowed)
				{
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("SessionMode", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SessionMode)), context.Contract.SessionMode.ToString())));
				}
				context.ContractType.CustomAttributes.Add(codeAttributeDeclaration);
			}

			// Token: 0x06007509 RID: 29961 RVA: 0x001B5ED8 File Offset: 0x001B40D8
			private void AddOperationContractAttributes(OperationContractGenerationContext context)
			{
				if (context.SyncMethod != null)
				{
					context.SyncMethod.CustomAttributes.Add(this.CreateOperationContractAttributeDeclaration(context.Operation, false));
				}
				if (context.BeginMethod != null)
				{
					context.BeginMethod.CustomAttributes.Add(this.CreateOperationContractAttributeDeclaration(context.Operation, true));
				}
				if (context.TaskMethod != null)
				{
					context.TaskMethod.CustomAttributes.Add(this.CreateOperationContractAttributeDeclaration(context.Operation, false));
				}
			}

			// Token: 0x0600750A RID: 29962 RVA: 0x001B5F58 File Offset: 0x001B4158
			private CodeAttributeDeclaration CreateOperationContractAttributeDeclaration(OperationDescription operationDescription, bool asyncPattern)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(this.context.ServiceContractGenerator.GetCodeTypeReference(typeof(OperationContractAttribute)));
				if (operationDescription.IsOneWay)
				{
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("IsOneWay", new CodePrimitiveExpression(true)));
				}
				if (operationDescription.DeclaringContract.SessionMode == SessionMode.Required && operationDescription.IsTerminating)
				{
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("IsTerminating", new CodePrimitiveExpression(true)));
				}
				if (operationDescription.DeclaringContract.SessionMode == SessionMode.Required && !operationDescription.IsInitiating)
				{
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("IsInitiating", new CodePrimitiveExpression(false)));
				}
				if (asyncPattern)
				{
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("AsyncPattern", new CodePrimitiveExpression(true)));
				}
				if (operationDescription.HasProtectionLevel)
				{
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("ProtectionLevel", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(ProtectionLevel)), operationDescription.ProtectionLevel.ToString())));
				}
				return codeAttributeDeclaration;
			}

			// Token: 0x0600750B RID: 29963 RVA: 0x001B6088 File Offset: 0x001B4288
			private static bool IsDuplex(ContractDescription contract)
			{
				foreach (OperationDescription operationDescription in contract.Operations)
				{
					if (operationDescription.IsServerInitiated())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x04004235 RID: 16949
			private readonly ServiceContractGenerator parent;

			// Token: 0x04004236 RID: 16950
			private readonly ServiceContractGenerator.CodeTypeFactory typeFactory;

			// Token: 0x04004237 RID: 16951
			private readonly bool asyncMethods;

			// Token: 0x04004238 RID: 16952
			private readonly bool taskMethod;

			// Token: 0x04004239 RID: 16953
			private ServiceContractGenerationContext context;

			// Token: 0x0400423A RID: 16954
			private UniqueCodeIdentifierScope contractMemberScope;

			// Token: 0x0400423B RID: 16955
			private UniqueCodeIdentifierScope callbackMemberScope;
		}

		// Token: 0x02000BD1 RID: 3025
		private class ChannelInterfaceGenerator : IServiceContractGenerationExtension
		{
			// Token: 0x0600750C RID: 29964 RVA: 0x001B60E0 File Offset: 0x001B42E0
			void IServiceContractGenerationExtension.GenerateContract(ServiceContractGenerationContext context)
			{
				CodeTypeDeclaration codeTypeDeclaration = context.TypeFactory.CreateInterfaceType();
				codeTypeDeclaration.BaseTypes.Add(context.ContractTypeReference);
				codeTypeDeclaration.BaseTypes.Add(context.ServiceContractGenerator.GetCodeTypeReference(typeof(IClientChannel)));
				new UniqueCodeNamespaceScope(context.Namespace).AddUnique(codeTypeDeclaration, context.ContractType.Name + "Channel", "Channel");
			}
		}

		// Token: 0x02000BD2 RID: 3026
		internal class CodeTypeFactory
		{
			// Token: 0x0600750E RID: 29966 RVA: 0x001B6160 File Offset: 0x001B4360
			public CodeTypeFactory(ServiceContractGenerator parent, bool internalTypes)
			{
				this.parent = parent;
				this.internalTypes = internalTypes;
			}

			// Token: 0x0600750F RID: 29967 RVA: 0x001B6176 File Offset: 0x001B4376
			public CodeTypeDeclaration CreateClassType()
			{
				return this.CreateCodeType(false);
			}

			// Token: 0x06007510 RID: 29968 RVA: 0x001B6180 File Offset: 0x001B4380
			private CodeTypeDeclaration CreateCodeType(bool isInterface)
			{
				CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration();
				codeTypeDeclaration.IsClass = !isInterface;
				codeTypeDeclaration.IsInterface = isInterface;
				this.RunDecorators(codeTypeDeclaration);
				return codeTypeDeclaration;
			}

			// Token: 0x06007511 RID: 29969 RVA: 0x001B61AC File Offset: 0x001B43AC
			public CodeTypeDeclaration CreateInterfaceType()
			{
				return this.CreateCodeType(true);
			}

			// Token: 0x06007512 RID: 29970 RVA: 0x001B61B5 File Offset: 0x001B43B5
			private void RunDecorators(CodeTypeDeclaration codeType)
			{
				this.AddPartial(codeType);
				this.AddInternal(codeType);
				this.AddDebuggerStepThroughAttribute(codeType);
				this.AddGeneratedCodeAttribute(codeType);
			}

			// Token: 0x06007513 RID: 29971 RVA: 0x001B61D3 File Offset: 0x001B43D3
			private void AddDebuggerStepThroughAttribute(CodeTypeDeclaration codeType)
			{
				if (codeType.IsClass)
				{
					codeType.CustomAttributes.Add(new CodeAttributeDeclaration(this.parent.GetCodeTypeReference(typeof(DebuggerStepThroughAttribute))));
				}
			}

			// Token: 0x06007514 RID: 29972 RVA: 0x001B6204 File Offset: 0x001B4404
			private void AddGeneratedCodeAttribute(CodeTypeDeclaration codeType)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(this.parent.GetCodeTypeReference(typeof(GeneratedCodeAttribute)));
				AssemblyName name = Assembly.GetExecutingAssembly().GetName();
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(name.Name)));
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(name.Version.ToString())));
				codeType.CustomAttributes.Add(codeAttributeDeclaration);
			}

			// Token: 0x06007515 RID: 29973 RVA: 0x001B6281 File Offset: 0x001B4481
			private void AddInternal(CodeTypeDeclaration codeType)
			{
				if (this.internalTypes)
				{
					codeType.TypeAttributes &= ~TypeAttributes.Public;
				}
			}

			// Token: 0x06007516 RID: 29974 RVA: 0x001B629A File Offset: 0x001B449A
			private void AddPartial(CodeTypeDeclaration codeType)
			{
				if (codeType.IsClass)
				{
					codeType.IsPartial = true;
				}
			}

			// Token: 0x0400423C RID: 16956
			private ServiceContractGenerator parent;

			// Token: 0x0400423D RID: 16957
			private bool internalTypes;
		}

		// Token: 0x02000BD3 RID: 3027
		internal static class ExtensionsHelper
		{
			// Token: 0x06007517 RID: 29975 RVA: 0x001B62AC File Offset: 0x001B44AC
			internal static void CallBehaviorExtensions(ServiceContractGenerationContext context)
			{
				ServiceContractGenerator.ExtensionsHelper.CallContractExtensions(ServiceContractGenerator.ExtensionsHelper.EnumerateBehaviorExtensions(context.Contract), context);
				foreach (OperationContractGenerationContext operationContractGenerationContext in context.Operations)
				{
					ServiceContractGenerator.ExtensionsHelper.CallOperationExtensions(ServiceContractGenerator.ExtensionsHelper.EnumerateBehaviorExtensions(operationContractGenerationContext.Operation), operationContractGenerationContext);
				}
			}

			// Token: 0x06007518 RID: 29976 RVA: 0x001B6314 File Offset: 0x001B4514
			internal static void CallContractExtensions(IEnumerable<IServiceContractGenerationExtension> extensions, ServiceContractGenerationContext context)
			{
				foreach (IServiceContractGenerationExtension serviceContractGenerationExtension in extensions)
				{
					serviceContractGenerationExtension.GenerateContract(context);
				}
			}

			// Token: 0x06007519 RID: 29977 RVA: 0x001B635C File Offset: 0x001B455C
			internal static void CallOperationExtensions(IEnumerable<IOperationContractGenerationExtension> extensions, ServiceContractGenerationContext context)
			{
				foreach (OperationContractGenerationContext context2 in context.Operations)
				{
					ServiceContractGenerator.ExtensionsHelper.CallOperationExtensions(extensions, context2);
				}
			}

			// Token: 0x0600751A RID: 29978 RVA: 0x001B63AC File Offset: 0x001B45AC
			private static void CallOperationExtensions(IEnumerable<IOperationContractGenerationExtension> extensions, OperationContractGenerationContext context)
			{
				foreach (IOperationContractGenerationExtension operationContractGenerationExtension in extensions)
				{
					operationContractGenerationExtension.GenerateOperation(context);
				}
			}

			// Token: 0x0600751B RID: 29979 RVA: 0x001B63F4 File Offset: 0x001B45F4
			private static IEnumerable<IServiceContractGenerationExtension> EnumerateBehaviorExtensions(ContractDescription contract)
			{
				foreach (IContractBehavior contractBehavior in contract.Behaviors)
				{
					if (contractBehavior is IServiceContractGenerationExtension)
					{
						yield return (IServiceContractGenerationExtension)contractBehavior;
					}
				}
				IEnumerator<IContractBehavior> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x0600751C RID: 29980 RVA: 0x001B6404 File Offset: 0x001B4604
			private static IEnumerable<IOperationContractGenerationExtension> EnumerateBehaviorExtensions(OperationDescription operation)
			{
				foreach (IOperationBehavior operationBehavior in operation.Behaviors)
				{
					if (operationBehavior is IOperationContractGenerationExtension)
					{
						yield return (IOperationContractGenerationExtension)operationBehavior;
					}
				}
				IEnumerator<IOperationBehavior> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x02000BD4 RID: 3028
		private class FaultContractAttributeGenerator : IOperationContractGenerationExtension
		{
			// Token: 0x0600751D RID: 29981 RVA: 0x001B6414 File Offset: 0x001B4614
			void IOperationContractGenerationExtension.GenerateOperation(OperationContractGenerationContext context)
			{
				CodeMemberMethod codeMemberMethod = context.SyncMethod ?? context.BeginMethod;
				foreach (FaultDescription fault in context.Operation.Faults)
				{
					CodeAttributeDeclaration codeAttributeDeclaration = ServiceContractGenerator.FaultContractAttributeGenerator.CreateAttrDecl(context, fault);
					if (codeAttributeDeclaration != null)
					{
						codeMemberMethod.CustomAttributes.Add(codeAttributeDeclaration);
					}
				}
			}

			// Token: 0x0600751E RID: 29982 RVA: 0x001B6488 File Offset: 0x001B4688
			private static CodeAttributeDeclaration CreateAttrDecl(OperationContractGenerationContext context, FaultDescription fault)
			{
				CodeTypeReference codeTypeReference = (fault.DetailType != null) ? context.Contract.ServiceContractGenerator.GetCodeTypeReference(fault.DetailType) : fault.DetailTypeReference;
				if (codeTypeReference == null || codeTypeReference == ServiceContractGenerator.FaultContractAttributeGenerator.voidTypeReference)
				{
					return null;
				}
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(context.ServiceContractGenerator.GetCodeTypeReference(typeof(FaultContractAttribute)));
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(codeTypeReference)));
				if (fault.Action != null)
				{
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Action", new CodePrimitiveExpression(fault.Action)));
				}
				if (fault.HasProtectionLevel)
				{
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("ProtectionLevel", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(ProtectionLevel)), fault.ProtectionLevel.ToString())));
				}
				if (!XmlName.IsNullOrEmpty(fault.ElementName))
				{
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Name", new CodePrimitiveExpression(fault.ElementName.EncodedName)));
				}
				if (fault.Namespace != context.Contract.Contract.Namespace)
				{
					codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(fault.Namespace)));
				}
				return codeAttributeDeclaration;
			}

			// Token: 0x0400423E RID: 16958
			private static CodeTypeReference voidTypeReference = new CodeTypeReference(typeof(void));
		}

		// Token: 0x02000BD5 RID: 3029
		private class MessageDescriptionComparer : IEqualityComparer<MessageDescription>
		{
			// Token: 0x06007521 RID: 29985 RVA: 0x001B6600 File Offset: 0x001B4800
			private MessageDescriptionComparer()
			{
			}

			// Token: 0x06007522 RID: 29986 RVA: 0x001B6608 File Offset: 0x001B4808
			bool IEqualityComparer<MessageDescription>.Equals(MessageDescription x, MessageDescription y)
			{
				if (x.XsdTypeName != y.XsdTypeName)
				{
					return false;
				}
				if (x.Headers.Count != y.Headers.Count)
				{
					return false;
				}
				MessageHeaderDescription[] array = new MessageHeaderDescription[x.Headers.Count];
				x.Headers.CopyTo(array, 0);
				MessageHeaderDescription[] array2 = new MessageHeaderDescription[y.Headers.Count];
				y.Headers.CopyTo(array2, 0);
				if (x.Headers.Count > 1)
				{
					MessagePartDescription[] array3 = array;
					Array.Sort<MessagePartDescription>(array3, ServiceContractGenerator.MessageDescriptionComparer.MessagePartDescriptionComparer.Singleton);
					array3 = array2;
					Array.Sort<MessagePartDescription>(array3, ServiceContractGenerator.MessageDescriptionComparer.MessagePartDescriptionComparer.Singleton);
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (ServiceContractGenerator.MessageDescriptionComparer.MessagePartDescriptionComparer.Singleton.Compare(array[i], array2[i]) != 0)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06007523 RID: 29987 RVA: 0x001B66CB File Offset: 0x001B48CB
			int IEqualityComparer<MessageDescription>.GetHashCode(MessageDescription obj)
			{
				return obj.XsdTypeName.GetHashCode();
			}

			// Token: 0x0400423F RID: 16959
			internal static ServiceContractGenerator.MessageDescriptionComparer Singleton = new ServiceContractGenerator.MessageDescriptionComparer();

			// Token: 0x02000F21 RID: 3873
			private class MessagePartDescriptionComparer : IComparer<MessagePartDescription>
			{
				// Token: 0x06008645 RID: 34373 RVA: 0x001F1CFF File Offset: 0x001EFEFF
				private MessagePartDescriptionComparer()
				{
				}

				// Token: 0x06008646 RID: 34374 RVA: 0x001F1D08 File Offset: 0x001EFF08
				public int Compare(MessagePartDescription p1, MessagePartDescription p2)
				{
					if (p1 == null)
					{
						if (p2 != null)
						{
							return -1;
						}
						return 0;
					}
					else
					{
						if (p2 == null)
						{
							return 1;
						}
						int num = string.CompareOrdinal(p1.Namespace, p2.Namespace);
						if (num == 0)
						{
							num = string.CompareOrdinal(p1.Name, p2.Name);
						}
						return num;
					}
				}

				// Token: 0x04004DE7 RID: 19943
				internal static ServiceContractGenerator.MessageDescriptionComparer.MessagePartDescriptionComparer Singleton = new ServiceContractGenerator.MessageDescriptionComparer.MessagePartDescriptionComparer();
			}
		}

		// Token: 0x02000BD6 RID: 3030
		internal class NamespaceHelper
		{
			// Token: 0x06007525 RID: 29989 RVA: 0x001B66E4 File Offset: 0x001B48E4
			public NamespaceHelper(CodeNamespaceCollection namespaces)
			{
				this.codeNamespaces = namespaces;
			}

			// Token: 0x17001AF2 RID: 6898
			// (get) Token: 0x06007526 RID: 29990 RVA: 0x001B66F3 File Offset: 0x001B48F3
			public Dictionary<string, string> NamespaceMappings
			{
				get
				{
					if (this.namespaceMappings == null)
					{
						this.namespaceMappings = new Dictionary<string, string>();
					}
					return this.namespaceMappings;
				}
			}

			// Token: 0x06007527 RID: 29991 RVA: 0x001B6710 File Offset: 0x001B4910
			private string DescriptionToCode(string descriptionNamespace)
			{
				string empty = string.Empty;
				if (this.namespaceMappings != null && !this.namespaceMappings.TryGetValue(descriptionNamespace, out empty) && !this.namespaceMappings.TryGetValue("*", out empty))
				{
					return string.Empty;
				}
				return empty;
			}

			// Token: 0x06007528 RID: 29992 RVA: 0x001B6758 File Offset: 0x001B4958
			public CodeNamespace EnsureNamespace(string descriptionNamespace)
			{
				string text = this.DescriptionToCode(descriptionNamespace);
				CodeNamespace codeNamespace = this.FindNamespace(text);
				if (codeNamespace == null)
				{
					codeNamespace = new CodeNamespace(text);
					this.codeNamespaces.Add(codeNamespace);
				}
				return codeNamespace;
			}

			// Token: 0x06007529 RID: 29993 RVA: 0x001B6790 File Offset: 0x001B4990
			private CodeNamespace FindNamespace(string ns)
			{
				foreach (object obj in this.codeNamespaces)
				{
					CodeNamespace codeNamespace = (CodeNamespace)obj;
					if (codeNamespace.Name == ns)
					{
						return codeNamespace;
					}
				}
				return null;
			}

			// Token: 0x0600752A RID: 29994 RVA: 0x001B67F8 File Offset: 0x001B49F8
			public static CodeTypeDeclaration GetCodeType(CodeTypeReference codeTypeReference)
			{
				return codeTypeReference.UserData[ServiceContractGenerator.NamespaceHelper.referenceKey] as CodeTypeDeclaration;
			}

			// Token: 0x0600752B RID: 29995 RVA: 0x001B6810 File Offset: 0x001B4A10
			internal static CodeTypeReference GetCodeTypeReference(CodeNamespace codeNamespace, CodeTypeDeclaration codeType)
			{
				CodeTypeReference codeTypeReference = new CodeTypeReference(string.IsNullOrEmpty(codeNamespace.Name) ? codeType.Name : (codeNamespace.Name + "." + codeType.Name));
				codeTypeReference.UserData[ServiceContractGenerator.NamespaceHelper.referenceKey] = codeType;
				return codeTypeReference;
			}

			// Token: 0x04004240 RID: 16960
			private static readonly object referenceKey = new object();

			// Token: 0x04004241 RID: 16961
			private const string WildcardNamespaceMapping = "*";

			// Token: 0x04004242 RID: 16962
			private readonly CodeNamespaceCollection codeNamespaces;

			// Token: 0x04004243 RID: 16963
			private Dictionary<string, string> namespaceMappings;
		}

		// Token: 0x02000BD7 RID: 3031
		internal struct OptionsHelper
		{
			// Token: 0x0600752D RID: 29997 RVA: 0x001B686C File Offset: 0x001B4A6C
			public OptionsHelper(ServiceContractGenerationOptions options)
			{
				this.Options = options;
			}

			// Token: 0x0600752E RID: 29998 RVA: 0x001B6875 File Offset: 0x001B4A75
			public bool IsSet(ServiceContractGenerationOptions option)
			{
				return (this.Options & option) > ServiceContractGenerationOptions.None;
			}

			// Token: 0x0600752F RID: 29999 RVA: 0x001B6882 File Offset: 0x001B4A82
			private static bool IsSingleBit(int x)
			{
				return x != 0 && (x & x + -1) == 0;
			}

			// Token: 0x04004244 RID: 16964
			public readonly ServiceContractGenerationOptions Options;
		}

		// Token: 0x02000BD8 RID: 3032
		private static class Strings
		{
			// Token: 0x04004245 RID: 16965
			public const string AsyncCallbackArgName = "callback";

			// Token: 0x04004246 RID: 16966
			public const string AsyncStateArgName = "asyncState";

			// Token: 0x04004247 RID: 16967
			public const string AsyncResultArgName = "result";

			// Token: 0x04004248 RID: 16968
			public const string CallbackTypeSuffix = "Callback";

			// Token: 0x04004249 RID: 16969
			public const string ChannelTypeSuffix = "Channel";

			// Token: 0x0400424A RID: 16970
			public const string DefaultContractName = "IContract";

			// Token: 0x0400424B RID: 16971
			public const string DefaultOperationName = "Method";

			// Token: 0x0400424C RID: 16972
			public const string InterfaceTypePrefix = "I";
		}

		// Token: 0x02000BD9 RID: 3033
		private class TransactionFlowAttributeGenerator : IOperationContractGenerationExtension
		{
			// Token: 0x06007530 RID: 30000 RVA: 0x001B6894 File Offset: 0x001B4A94
			void IOperationContractGenerationExtension.GenerateOperation(OperationContractGenerationContext context)
			{
				TransactionFlowAttribute transactionFlowAttribute = context.Operation.Behaviors.Find<TransactionFlowAttribute>();
				if (transactionFlowAttribute != null && transactionFlowAttribute.Transactions != TransactionFlowOption.NotAllowed)
				{
					CodeMemberMethod codeMemberMethod = context.SyncMethod ?? context.BeginMethod;
					codeMemberMethod.CustomAttributes.Add(ServiceContractGenerator.TransactionFlowAttributeGenerator.CreateAttrDecl(context, transactionFlowAttribute));
				}
			}

			// Token: 0x06007531 RID: 30001 RVA: 0x001B68E4 File Offset: 0x001B4AE4
			private static CodeAttributeDeclaration CreateAttrDecl(OperationContractGenerationContext context, TransactionFlowAttribute attr)
			{
				return new CodeAttributeDeclaration(context.Contract.ServiceContractGenerator.GetCodeTypeReference(typeof(TransactionFlowAttribute)))
				{
					Arguments = 
					{
						new CodeAttributeArgument(ServiceContractGenerator.GetEnumReference<TransactionFlowOption>(attr.Transactions))
					}
				};
			}
		}
	}
}
