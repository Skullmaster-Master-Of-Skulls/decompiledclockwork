using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Web.Resources;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000026 RID: 38
	[SecurityCritical]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	internal class VSWCFServiceContractGenerator
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00005AD8 File Offset: 0x00003CD8
		public IEnumerable<System.ServiceModel.Channels.Binding> BindingCollection
		{
			get
			{
				return this.bindingCollection;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00005AE0 File Offset: 0x00003CE0
		public IEnumerable<GeneratedContractType> ProxyGeneratedContractTypes
		{
			get
			{
				return this.proxyGeneratedContractTypes;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00005AE8 File Offset: 0x00003CE8
		public IEnumerable<ProxyGenerationError> ProxyGenerationErrors
		{
			get
			{
				return this.proxyGenerationErrors;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00005AF0 File Offset: 0x00003CF0
		public IEnumerable<ProxyGenerationError> ImportErrors
		{
			get
			{
				return this.importErrors;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00005AF8 File Offset: 0x00003CF8
		public IEnumerable<ContractDescription> ContractCollection
		{
			get
			{
				return this.contractCollection;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00005B00 File Offset: 0x00003D00
		public IEnumerable<ServiceEndpoint> EndpointCollection
		{
			get
			{
				return this.serviceEndpointList;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00005B08 File Offset: 0x00003D08
		public Dictionary<ServiceEndpoint, ChannelEndpointElement> EndpointMap
		{
			get
			{
				return this.serviceEndpointToChannelEndpointElementMap;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00005B10 File Offset: 0x00003D10
		public Configuration TargetConfiguration
		{
			get
			{
				return this.targetConfiguration;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00005B18 File Offset: 0x00003D18
		public CodeCompileUnit TargetCompileUnit
		{
			get
			{
				return this.targetCompileUnit;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00005B20 File Offset: 0x00003D20
		private static CodeAttributeDeclaration OutAttribute
		{
			get
			{
				if (VSWCFServiceContractGenerator.outAttribute == null)
				{
					VSWCFServiceContractGenerator.outAttribute = new CodeAttributeDeclaration(typeof(OutAttribute).FullName);
				}
				return VSWCFServiceContractGenerator.outAttribute;
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005B48 File Offset: 0x00003D48
		protected VSWCFServiceContractGenerator(List<ProxyGenerationError> importErrors, CodeCompileUnit targetCompileUnit, Configuration targetConfiguration, IEnumerable<System.ServiceModel.Channels.Binding> bindingCollection, IEnumerable<ContractDescription> contractCollection, List<ServiceEndpoint> serviceEndpointList, Dictionary<ServiceEndpoint, ChannelEndpointElement> serviceEndpointToChannelEndpointElementMap, List<GeneratedContractType> proxyGeneratedContractTypes, IEnumerable<ProxyGenerationError> proxyGenerationErrors)
		{
			if (importErrors == null)
			{
				throw new ArgumentNullException("importErrors");
			}
			if (targetCompileUnit == null)
			{
				throw new ArgumentNullException("targetCompileUnit");
			}
			if (bindingCollection == null)
			{
				throw new ArgumentNullException("bindingCollection");
			}
			if (contractCollection == null)
			{
				throw new ArgumentNullException("contractCollection");
			}
			if (serviceEndpointList == null)
			{
				throw new ArgumentNullException("serviceEndpointList");
			}
			if (serviceEndpointToChannelEndpointElementMap == null)
			{
				throw new ArgumentNullException("serviceEndpointToChannelEndpointElementMap");
			}
			if (proxyGeneratedContractTypes == null)
			{
				throw new ArgumentNullException("proxyGeneratedContractTypes");
			}
			if (proxyGenerationErrors == null)
			{
				throw new ArgumentNullException("proxyGenerationErrors");
			}
			this.importErrors = importErrors;
			this.targetCompileUnit = targetCompileUnit;
			this.targetConfiguration = targetConfiguration;
			this.bindingCollection = bindingCollection;
			this.contractCollection = contractCollection;
			this.serviceEndpointList = serviceEndpointList;
			this.serviceEndpointToChannelEndpointElementMap = serviceEndpointToChannelEndpointElementMap;
			this.proxyGeneratedContractTypes = proxyGeneratedContractTypes;
			this.proxyGenerationErrors = proxyGenerationErrors;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005C18 File Offset: 0x00003E18
		public static VSWCFServiceContractGenerator GenerateCodeAndConfiguration(SvcMapFile svcMapFile, Configuration toolConfiguration, CodeDomProvider codeDomProvider, string proxyNamespace, Configuration targetConfiguration, string configurationNamespace, IServiceProvider serviceProviderForImportExtensions, IContractGeneratorReferenceTypeLoader typeLoader, int targetFrameworkVersion, Type typedDataSetSchemaImporterExtension)
		{
			if (svcMapFile == null)
			{
				throw new ArgumentNullException("svcMapFile");
			}
			if (codeDomProvider == null)
			{
				throw new ArgumentNullException("codeDomProvider");
			}
			if (typedDataSetSchemaImporterExtension == null)
			{
				throw new ArgumentNullException("typedDataSetSchemaImporterExtension");
			}
			List<ProxyGenerationError> generationErrors = new List<ProxyGenerationError>();
			List<ProxyGenerationError> list = new List<ProxyGenerationError>();
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			WsdlImporter wsdlImporter = VSWCFServiceContractGenerator.CreateWsdlImporter(svcMapFile, toolConfiguration, codeCompileUnit, codeDomProvider, proxyNamespace, serviceProviderForImportExtensions, typeLoader, targetFrameworkVersion, generationErrors, typedDataSetSchemaImporterExtension);
			ServiceContractGenerator contractGenerator = VSWCFServiceContractGenerator.CreateContractGenerator(svcMapFile.ClientOptions, wsdlImporter, codeCompileUnit, proxyNamespace, targetConfiguration, typeLoader, targetFrameworkVersion, generationErrors);
			VSWCFServiceContractGenerator result;
			try
			{
				List<ServiceEndpoint> list2 = new List<ServiceEndpoint>();
				IEnumerable<System.ServiceModel.Channels.Binding> enumerable;
				IEnumerable<ContractDescription> enumerable2;
				VSWCFServiceContractGenerator.ImportWCFModel(wsdlImporter, codeCompileUnit, generationErrors, out list2, out enumerable, out enumerable2);
				Dictionary<ServiceEndpoint, ChannelEndpointElement> dictionary;
				List<GeneratedContractType> list3;
				VSWCFServiceContractGenerator.GenerateProxy(wsdlImporter, contractGenerator, codeCompileUnit, proxyNamespace, configurationNamespace, enumerable2, enumerable, list2, list, out dictionary, out list3);
				if (VSWCFServiceContractGenerator.IsVBCodeDomProvider(codeDomProvider))
				{
					VSWCFServiceContractGenerator.PatchOutParametersInVB(codeCompileUnit);
				}
				result = new VSWCFServiceContractGenerator(generationErrors, codeCompileUnit, targetConfiguration, enumerable, enumerable2, list2, dictionary, list3, list);
			}
			catch (Exception errorException)
			{
				list.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.GenerateCode, string.Empty, errorException, false));
				result = new VSWCFServiceContractGenerator(generationErrors, new CodeCompileUnit(), targetConfiguration, new List<System.ServiceModel.Channels.Binding>(), new List<ContractDescription>(), new List<ServiceEndpoint>(), new Dictionary<ServiceEndpoint, ChannelEndpointElement>(), new List<GeneratedContractType>(), list);
			}
			return result;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005D38 File Offset: 0x00003F38
		protected static ServiceContractGenerator CreateContractGenerator(ClientOptions proxyOptions, WsdlImporter wsdlImporter, CodeCompileUnit targetCompileUnit, string proxyNamespace, Configuration targetConfiguration, IContractGeneratorReferenceTypeLoader typeLoader, int targetFrameworkVersion, IList<ProxyGenerationError> importErrors)
		{
			ServiceContractGenerator serviceContractGenerator = new ServiceContractGenerator(targetCompileUnit, targetConfiguration);
			serviceContractGenerator.NamespaceMappings.Add("*", proxyNamespace);
			if (proxyOptions.GenerateInternalTypes)
			{
				serviceContractGenerator.Options |= ServiceContractGenerationOptions.InternalTypes;
			}
			else
			{
				serviceContractGenerator.Options &= ~ServiceContractGenerationOptions.InternalTypes;
			}
			serviceContractGenerator.Options &= ~(ServiceContractGenerationOptions.AsynchronousMethods | ServiceContractGenerationOptions.EventBasedAsynchronousMethods | ServiceContractGenerationOptions.TaskBasedAsynchronousMethod);
			if (proxyOptions.GenerateTaskBasedAsynchronousMethod)
			{
				serviceContractGenerator.Options |= ServiceContractGenerationOptions.TaskBasedAsynchronousMethod;
			}
			else if (proxyOptions.GenerateAsynchronousMethods)
			{
				serviceContractGenerator.Options |= ServiceContractGenerationOptions.AsynchronousMethods;
				if (targetFrameworkVersion >= 196613)
				{
					serviceContractGenerator.Options |= ServiceContractGenerationOptions.EventBasedAsynchronousMethods;
				}
			}
			if (proxyOptions.GenerateMessageContracts)
			{
				serviceContractGenerator.Options |= ServiceContractGenerationOptions.TypedMessages;
			}
			else
			{
				serviceContractGenerator.Options &= ~ServiceContractGenerationOptions.TypedMessages;
			}
			if (typeLoader != null)
			{
				foreach (ContractMapping contractMapping in proxyOptions.ServiceContractMappingList)
				{
					try
					{
						Type type = typeLoader.LoadType(contractMapping.TypeName);
						if (!VSWCFServiceContractGenerator.IsTypeShareable(type))
						{
							importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.GenerateCode, string.Empty, new FormatException(string.Format(CultureInfo.CurrentCulture, WCFModelStrings.ReferenceGroup_SharedTypeMustBePublic, new object[]
							{
								contractMapping.TypeName
							}))));
						}
						else
						{
							ContractDescription contract = ContractDescription.GetContract(type);
							if (!string.Equals(contractMapping.Name, contract.Name, StringComparison.Ordinal) || !string.Equals(contractMapping.TargetNamespace, contract.Namespace, StringComparison.Ordinal))
							{
								importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.GenerateCode, string.Empty, new FormatException(string.Format(CultureInfo.CurrentCulture, WCFModelStrings.ReferenceGroup_ServiceContractMappingMissMatch, new object[]
								{
									contractMapping.TypeName,
									contract.Namespace,
									contract.Name,
									contractMapping.TargetNamespace,
									contractMapping.Name
								}))));
							}
							XmlQualifiedName key = new XmlQualifiedName(contract.Name, contract.Namespace);
							wsdlImporter.KnownContracts.Add(key, contract);
							serviceContractGenerator.ReferencedTypes.Add(contract, type);
						}
					}
					catch (Exception errorException)
					{
						importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.GenerateCode, string.Empty, errorException));
					}
				}
			}
			foreach (NamespaceMapping namespaceMapping in proxyOptions.NamespaceMappingList)
			{
				serviceContractGenerator.NamespaceMappings.Add(namespaceMapping.TargetNamespace, namespaceMapping.ClrNamespace);
			}
			return serviceContractGenerator;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006000 File Offset: 0x00004200
		protected static void GenerateProxy(WsdlImporter importer, ServiceContractGenerator contractGenerator, CodeCompileUnit targetCompileUnit, string proxyNamespace, string configurationNamespace, IEnumerable<ContractDescription> contractCollection, IEnumerable<System.ServiceModel.Channels.Binding> bindingCollection, List<ServiceEndpoint> serviceEndpointList, IList<ProxyGenerationError> proxyGenerationErrors, out Dictionary<ServiceEndpoint, ChannelEndpointElement> serviceEndpointToChannelEndpointElementMap, out List<GeneratedContractType> proxyGeneratedContractTypes)
		{
			if (serviceEndpointList == null)
			{
				throw new ArgumentNullException("serviceEndpointList");
			}
			if (bindingCollection == null)
			{
				throw new ArgumentNullException("bindingCollection");
			}
			if (contractCollection == null)
			{
				throw new ArgumentNullException("contractCollection");
			}
			if (proxyGenerationErrors == null)
			{
				throw new ArgumentNullException("proxyGenerationErrors");
			}
			proxyGeneratedContractTypes = new List<GeneratedContractType>();
			serviceEndpointToChannelEndpointElementMap = new Dictionary<ServiceEndpoint, ChannelEndpointElement>();
			try
			{
				HttpBindingExtension httpBindingExtension = importer.WsdlImportExtensions.Find<HttpBindingExtension>();
				using (IEnumerator<ContractDescription> enumerator = contractCollection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ContractDescription contract = enumerator.Current;
						if (httpBindingExtension == null || !httpBindingExtension.IsHttpBindingContract(contract) || serviceEndpointList.Any((ServiceEndpoint endpoint) => endpoint.Contract == contract))
						{
							CodeTypeReference codeTypeReference = contractGenerator.GenerateServiceContractType(contract);
							if (codeTypeReference != null)
							{
								string baseType = codeTypeReference.BaseType;
								GeneratedContractType item = new GeneratedContractType(contract.Namespace, contract.Name, baseType, baseType);
								proxyGeneratedContractTypes.Add(item);
							}
						}
					}
				}
				if (contractGenerator.Configuration != null)
				{
					foreach (ServiceEndpoint serviceEndpoint in serviceEndpointList)
					{
						ChannelEndpointElement value = null;
						contractGenerator.GenerateServiceEndpoint(serviceEndpoint, out value);
						serviceEndpointToChannelEndpointElementMap[serviceEndpoint] = value;
					}
					foreach (System.ServiceModel.Channels.Binding binding in bindingCollection)
					{
						string text = null;
						string text2 = null;
						contractGenerator.GenerateBinding(binding, out text, out text2);
					}
				}
				VSWCFServiceContractGenerator.PatchConfigurationName(proxyNamespace, configurationNamespace, proxyGeneratedContractTypes, serviceEndpointToChannelEndpointElementMap.Values, targetCompileUnit);
			}
			finally
			{
				foreach (MetadataConversionError errorMessage in contractGenerator.Errors)
				{
					proxyGenerationErrors.Add(new ProxyGenerationError(errorMessage));
				}
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006260 File Offset: 0x00004460
		protected static XmlSerializerImportOptions CreateXmlSerializerImportOptions(ClientOptions proxyOptions, CodeCompileUnit targetCompileUnit, CodeDomProvider codeDomProvider, string proxyNamespace, Type typedDataSetSchemaImporterExtension)
		{
			XmlSerializerImportOptions xmlSerializerImportOptions = new XmlSerializerImportOptions(targetCompileUnit);
			WebReferenceOptions webReferenceOptions = new WebReferenceOptions();
			webReferenceOptions.CodeGenerationOptions = (CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateOrder);
			if (proxyOptions.EnableDataBinding)
			{
				webReferenceOptions.CodeGenerationOptions |= CodeGenerationOptions.EnableDataBinding;
			}
			webReferenceOptions.SchemaImporterExtensions.Add(typedDataSetSchemaImporterExtension.AssemblyQualifiedName);
			webReferenceOptions.SchemaImporterExtensions.Add(typeof(DataSetSchemaImporterExtension).AssemblyQualifiedName);
			xmlSerializerImportOptions.WebReferenceOptions = webReferenceOptions;
			xmlSerializerImportOptions.CodeProvider = codeDomProvider;
			xmlSerializerImportOptions.ClrNamespace = proxyNamespace;
			return xmlSerializerImportOptions;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000062E0 File Offset: 0x000044E0
		protected static XsdDataContractImporter CreateDataContractImporter(ClientOptions proxyOptions, CodeCompileUnit targetCompileUnit, CodeDomProvider codeDomProvider, string proxyNamespace, IContractGeneratorReferenceTypeLoader typeLoader, int targetFrameworkVersion, IList<ProxyGenerationError> importErrors)
		{
			XsdDataContractImporter xsdDataContractImporter = new XsdDataContractImporter(targetCompileUnit);
			ImportOptions importOptions = new ImportOptions();
			importOptions.CodeProvider = codeDomProvider;
			importOptions.Namespaces.Add("*", proxyNamespace);
			importOptions.GenerateInternal = proxyOptions.GenerateInternalTypes;
			importOptions.GenerateSerializable = proxyOptions.GenerateSerializableTypes;
			importOptions.EnableDataBinding = proxyOptions.EnableDataBinding;
			importOptions.ImportXmlType = proxyOptions.ImportXmlTypes;
			if (typeLoader != null)
			{
				IEnumerable<Type> enumerable = VSWCFServiceContractGenerator.LoadSharedDataContractTypes(proxyOptions, typeLoader, targetFrameworkVersion, importErrors);
				if (enumerable != null)
				{
					foreach (Type item in enumerable)
					{
						importOptions.ReferencedTypes.Add(item);
					}
				}
				IEnumerable<Type> enumerable2 = VSWCFServiceContractGenerator.LoadSharedCollectionTypes(proxyOptions, typeLoader, importErrors);
				if (enumerable2 != null)
				{
					foreach (Type item2 in enumerable2)
					{
						importOptions.ReferencedCollectionTypes.Add(item2);
					}
				}
			}
			foreach (NamespaceMapping namespaceMapping in proxyOptions.NamespaceMappingList)
			{
				importOptions.Namespaces.Add(namespaceMapping.TargetNamespace, namespaceMapping.ClrNamespace);
			}
			xsdDataContractImporter.Options = importOptions;
			return xsdDataContractImporter;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00006450 File Offset: 0x00004650
		protected static IEnumerable<Type> LoadSharedDataContractTypes(ClientOptions proxyOptions, IContractGeneratorReferenceTypeLoader typeLoader, int targetFrameworkVersion, IList<ProxyGenerationError> importErrors)
		{
			if (typeLoader == null)
			{
				throw new ArgumentNullException("typeLoader");
			}
			Dictionary<Type, ReferencedType> dictionary = new Dictionary<Type, ReferencedType>();
			IEnumerable<Assembly> enumerable = VSWCFServiceContractGenerator.LoadReferenedAssemblies(proxyOptions, typeLoader, importErrors);
			if (enumerable != null)
			{
				foreach (Assembly assembly in enumerable)
				{
					IContractGeneratorReferenceTypeLoader2 contractGeneratorReferenceTypeLoader = typeLoader as IContractGeneratorReferenceTypeLoader2;
					if (contractGeneratorReferenceTypeLoader != null)
					{
						using (IEnumerator<Type> enumerator2 = contractGeneratorReferenceTypeLoader.LoadExportedTypes(assembly).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								Type key = enumerator2.Current;
								dictionary.Add(key, null);
							}
							continue;
						}
					}
					foreach (Type type in assembly.GetExportedTypes())
					{
						try
						{
							if (typeLoader.LoadType(type.FullName) != null)
							{
								dictionary.Add(type, null);
							}
						}
						catch (NotSupportedException)
						{
						}
						catch (Exception errorException)
						{
							importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.GenerateCode, string.Empty, errorException, true));
						}
					}
				}
			}
			foreach (ReferencedType referencedType in proxyOptions.ReferencedDataContractTypeList)
			{
				try
				{
					Type type2 = typeLoader.LoadType(referencedType.TypeName);
					if (!VSWCFServiceContractGenerator.IsTypeShareable(type2))
					{
						importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.GenerateCode, string.Empty, new FormatException(string.Format(CultureInfo.CurrentCulture, WCFModelStrings.ReferenceGroup_SharedTypeMustBePublic, new object[]
						{
							referencedType.TypeName
						}))));
					}
					else
					{
						dictionary[type2] = referencedType;
					}
				}
				catch (Exception errorException2)
				{
					importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.GenerateCode, string.Empty, errorException2));
				}
			}
			foreach (ReferencedType referencedType2 in proxyOptions.ExcludedTypeList)
			{
				try
				{
					Type key2 = typeLoader.LoadType(referencedType2.TypeName);
					if (dictionary.ContainsKey(key2))
					{
						if (dictionary[key2] != null)
						{
							importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.GenerateCode, string.Empty, new Exception(string.Format(CultureInfo.CurrentCulture, WCFModelStrings.ReferenceGroup_DataContractExcludedAndIncluded, new object[]
							{
								referencedType2.TypeName
							}))));
						}
						dictionary.Remove(key2);
					}
				}
				catch (Exception errorException3)
				{
					importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.GenerateCode, string.Empty, errorException3, true));
				}
			}
			foreach (Type key3 in VSWCFServiceContractGenerator.GetUnsupportedTypes(targetFrameworkVersion))
			{
				dictionary.Remove(key3);
			}
			return dictionary.Keys;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00006754 File Offset: 0x00004954
		private static IEnumerable<Type> GetUnsupportedTypes(int targetFrameworkVersion)
		{
			if (targetFrameworkVersion < 196613)
			{
				return VSWCFServiceContractGenerator.unsupportedTypesInFramework30;
			}
			return new Type[0];
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000676C File Offset: 0x0000496C
		private static void PatchConfigurationName(string proxyNamespace, string configNamespace, IEnumerable<GeneratedContractType> generatedContracts, IEnumerable<ChannelEndpointElement> endpoints, CodeCompileUnit targetCompileUnit)
		{
			if (configNamespace != null && !configNamespace.Equals(proxyNamespace, StringComparison.Ordinal))
			{
				string originalNamespace = VSWCFServiceContractGenerator.MakePeriodTerminatedNamespacePrefix(proxyNamespace);
				string replacementNamespace = VSWCFServiceContractGenerator.MakePeriodTerminatedNamespacePrefix(configNamespace);
				foreach (GeneratedContractType generatedContractType in generatedContracts)
				{
					generatedContractType.ConfigurationName = VSWCFServiceContractGenerator.ReplaceNamespace(originalNamespace, replacementNamespace, generatedContractType.ConfigurationName);
				}
				foreach (ChannelEndpointElement channelEndpointElement in endpoints)
				{
					channelEndpointElement.Contract = VSWCFServiceContractGenerator.ReplaceNamespace(originalNamespace, replacementNamespace, channelEndpointElement.Contract);
				}
				VSWCFServiceContractGenerator.PatchConfigurationNameInServiceContractAttribute(targetCompileUnit, proxyNamespace, configNamespace);
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00006838 File Offset: 0x00004A38
		private static string ReplaceNamespace(string originalNamespace, string replacementNamespace, string typeName)
		{
			if (typeName.StartsWith(originalNamespace, StringComparison.Ordinal))
			{
				return replacementNamespace + typeName.Substring(originalNamespace.Length);
			}
			return typeName;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00006858 File Offset: 0x00004A58
		private static string MakePeriodTerminatedNamespacePrefix(string ns)
		{
			if (string.IsNullOrEmpty(ns))
			{
				return "";
			}
			if (!ns.EndsWith(".", StringComparison.Ordinal))
			{
				return ns + ".";
			}
			return ns;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006883 File Offset: 0x00004A83
		private static bool IsTypeShareable(Type t)
		{
			return !(t == null) && (t.IsPublic || t.IsNestedPublic);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000068A0 File Offset: 0x00004AA0
		private static IEnumerable<Assembly> LoadReferenedAssemblies(ClientOptions proxyOptions, IContractGeneratorReferenceTypeLoader typeLoader, IList<ProxyGenerationError> importErrors)
		{
			List<Assembly> list = new List<Assembly>();
			if (proxyOptions.ReferenceAllAssemblies)
			{
				try
				{
					IEnumerable<Exception> enumerable = null;
					IEnumerable<Assembly> enumerable2 = null;
					typeLoader.LoadAllAssemblies(out enumerable2, out enumerable);
					if (enumerable != null)
					{
						foreach (Exception errorException in enumerable)
						{
							importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.GenerateCode, string.Empty, errorException, true));
						}
					}
					if (enumerable2 != null)
					{
						list.AddRange(enumerable2);
					}
				}
				catch (Exception errorException2)
				{
					importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.GenerateCode, string.Empty, errorException2));
				}
			}
			foreach (ReferencedAssembly referencedAssembly in proxyOptions.ReferencedAssemblyList)
			{
				try
				{
					Assembly assembly = typeLoader.LoadAssembly(referencedAssembly.AssemblyName);
					if (assembly != null && !list.Contains(assembly))
					{
						list.Add(assembly);
					}
				}
				catch (Exception errorException3)
				{
					importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.GenerateCode, string.Empty, errorException3));
				}
			}
			return list;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000069D4 File Offset: 0x00004BD4
		protected static IEnumerable<Type> LoadSharedCollectionTypes(ClientOptions proxyOptions, IContractGeneratorReferenceTypeLoader typeLoader, IList<ProxyGenerationError> importErrors)
		{
			List<Type> list = new List<Type>();
			foreach (ReferencedCollectionType referencedCollectionType in proxyOptions.CollectionMappingList)
			{
				try
				{
					Type type = typeLoader.LoadType(referencedCollectionType.TypeName);
					if (!VSWCFServiceContractGenerator.IsTypeShareable(type))
					{
						importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.GenerateCode, string.Empty, new FormatException(string.Format(CultureInfo.CurrentCulture, WCFModelStrings.ReferenceGroup_SharedTypeMustBePublic, new object[]
						{
							referencedCollectionType.TypeName
						}))));
					}
					else
					{
						list.Add(type);
					}
				}
				catch (Exception errorException)
				{
					importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.GenerateCode, string.Empty, errorException));
				}
			}
			return list;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006AA0 File Offset: 0x00004CA0
		protected static WsdlImporter CreateWsdlImporter(SvcMapFile svcMapFile, Configuration toolConfiguration, CodeCompileUnit targetCompileUnit, CodeDomProvider codeDomProvider, string targetNamespace, IServiceProvider serviceProviderForImportExtensions, IContractGeneratorReferenceTypeLoader typeLoader, int targetFrameworkVersion, IList<ProxyGenerationError> importErrors, Type typedDataSetSchemaImporterExtension)
		{
			List<MetadataSection> list = VSWCFServiceContractGenerator.CollectMetadataDocuments(svcMapFile.MetadataList, importErrors);
			WsdlImporter wsdlImporter = null;
			ClientOptions.ProxySerializerType proxySerializerType = svcMapFile.ClientOptions.Serializer;
			if (proxySerializerType == ClientOptions.ProxySerializerType.Auto && VSWCFServiceContractGenerator.ContainsHttpBindings(list))
			{
				proxySerializerType = ClientOptions.ProxySerializerType.XmlSerializer;
			}
			if (toolConfiguration != null)
			{
				ServiceModelSectionGroup sectionGroup = ServiceModelSectionGroup.GetSectionGroup(toolConfiguration);
				if (sectionGroup != null)
				{
					Collection<IWsdlImportExtension> collection = sectionGroup.Client.Metadata.LoadWsdlImportExtensions();
					Collection<IPolicyImportExtension> policyImportExtensions = sectionGroup.Client.Metadata.LoadPolicyImportExtensions();
					switch (proxySerializerType)
					{
					case ClientOptions.ProxySerializerType.DataContractSerializer:
						VSWCFServiceContractGenerator.RemoveExtension(typeof(XmlSerializerMessageContractImporter), collection);
						break;
					case ClientOptions.ProxySerializerType.XmlSerializer:
						VSWCFServiceContractGenerator.RemoveExtension(typeof(DataContractSerializerMessageContractImporter), collection);
						break;
					}
					VSWCFServiceContractGenerator.ProvideImportExtensionsWithContextInformation(svcMapFile, serviceProviderForImportExtensions, collection, policyImportExtensions);
					collection.Add(new HttpBindingExtension());
					wsdlImporter = new WsdlImporter(new MetadataSet(list), policyImportExtensions, collection);
				}
			}
			if (wsdlImporter == null)
			{
				wsdlImporter = new WsdlImporter(new MetadataSet(list));
			}
			wsdlImporter.State.Add(typeof(XsdDataContractImporter), VSWCFServiceContractGenerator.CreateDataContractImporter(svcMapFile.ClientOptions, targetCompileUnit, codeDomProvider, targetNamespace, typeLoader, targetFrameworkVersion, importErrors));
			if (proxySerializerType != ClientOptions.ProxySerializerType.DataContractSerializer)
			{
				wsdlImporter.State.Add(typeof(XmlSerializerImportOptions), VSWCFServiceContractGenerator.CreateXmlSerializerImportOptions(svcMapFile.ClientOptions, targetCompileUnit, codeDomProvider, targetNamespace, typedDataSetSchemaImporterExtension));
			}
			FaultImportOptions faultImportOptions = new FaultImportOptions();
			faultImportOptions.UseMessageFormat = svcMapFile.ClientOptions.UseSerializerForFaults;
			wsdlImporter.State.Add(typeof(FaultImportOptions), faultImportOptions);
			WrappedOptions wrappedOptions = new WrappedOptions();
			wrappedOptions.WrappedFlag = svcMapFile.ClientOptions.Wrapped;
			wsdlImporter.State.Add(typeof(WrappedOptions), wrappedOptions);
			return wsdlImporter;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006C34 File Offset: 0x00004E34
		internal static void ProvideImportExtensionsWithContextInformation(SvcMapFile svcMapFile, IServiceProvider serviceProviderForImportExtensions, IEnumerable<IWsdlImportExtension> wsdlImportExtensions, IEnumerable<IPolicyImportExtension> policyImportExtensions)
		{
			Dictionary<string, byte[]> dictionary = null;
			foreach (IWsdlImportExtension wsdlImportExtension in wsdlImportExtensions)
			{
				IWcfReferenceReceiveContextInformation wcfReferenceReceiveContextInformation = wsdlImportExtension as IWcfReferenceReceiveContextInformation;
				if (wcfReferenceReceiveContextInformation != null)
				{
					if (dictionary == null)
					{
						dictionary = VSWCFServiceContractGenerator.CreateDictionaryOfCopiedExtensionFiles(svcMapFile);
					}
					wcfReferenceReceiveContextInformation.ReceiveImportContextInformation(dictionary, serviceProviderForImportExtensions);
				}
			}
			foreach (IPolicyImportExtension policyImportExtension in policyImportExtensions)
			{
				IWcfReferenceReceiveContextInformation wcfReferenceReceiveContextInformation2 = policyImportExtension as IWcfReferenceReceiveContextInformation;
				if (wcfReferenceReceiveContextInformation2 != null)
				{
					if (dictionary == null)
					{
						dictionary = VSWCFServiceContractGenerator.CreateDictionaryOfCopiedExtensionFiles(svcMapFile);
					}
					wcfReferenceReceiveContextInformation2.ReceiveImportContextInformation(dictionary, serviceProviderForImportExtensions);
				}
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006CEC File Offset: 0x00004EEC
		private static void RemoveExtension(Type extensionType, Collection<IWsdlImportExtension> wsdlImportExtensions)
		{
			for (int i = 0; i < wsdlImportExtensions.Count; i++)
			{
				if (wsdlImportExtensions[i].GetType() == extensionType)
				{
					wsdlImportExtensions.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006D28 File Offset: 0x00004F28
		private static Dictionary<string, byte[]> CreateDictionaryOfCopiedExtensionFiles(SvcMapFile svcMapFile)
		{
			Dictionary<string, byte[]> dictionary = new Dictionary<string, byte[]>();
			foreach (ExtensionFile extensionFile in svcMapFile.Extensions)
			{
				if (extensionFile.ContentBuffer != null && extensionFile.IsBufferValid)
				{
					dictionary.Add(extensionFile.Name, (byte[])extensionFile.ContentBuffer.Clone());
				}
			}
			return dictionary;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00006DA8 File Offset: 0x00004FA8
		protected static List<MetadataSection> CollectMetadataDocuments(IEnumerable<MetadataFile> metadataList, IList<ProxyGenerationError> importErrors)
		{
			List<MetadataSection> list = new List<MetadataSection>();
			foreach (MetadataFile metadataFile in metadataList)
			{
				if (!metadataFile.Ignore)
				{
					try
					{
						MetadataSection metadataSection = metadataFile.CreateMetadataSection();
						if (metadataSection != null)
						{
							list.Add(metadataSection);
						}
					}
					catch (Exception ex)
					{
						importErrors.Add(VSWCFServiceContractGenerator.ConvertMetadataErrorToProxyGenerationError(metadataFile, ex));
					}
				}
			}
			VSWCFServiceContractGenerator.RemoveDuplicatedSchemaItems(list, importErrors);
			VSWCFServiceContractGenerator.CheckDuplicatedWsdlItems(list, importErrors);
			return list;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006E38 File Offset: 0x00005038
		internal static ProxyGenerationError ConvertMetadataErrorToProxyGenerationError(MetadataFile metadataItem, Exception ex)
		{
			ProxyGenerationError result;
			if (ex is XmlSchemaException)
			{
				result = new ProxyGenerationError(ProxyGenerationError.GeneratorState.LoadMetadata, metadataItem.FileName, (XmlSchemaException)ex);
			}
			else if (ex is XmlException)
			{
				result = new ProxyGenerationError(ProxyGenerationError.GeneratorState.LoadMetadata, metadataItem.FileName, (XmlException)ex);
			}
			else if (ex is InvalidOperationException)
			{
				XmlSchemaException ex2 = ex.InnerException as XmlSchemaException;
				if (ex2 != null)
				{
					result = new ProxyGenerationError(ProxyGenerationError.GeneratorState.LoadMetadata, metadataItem.FileName, ex2);
				}
				else
				{
					XmlException ex3 = ex.InnerException as XmlException;
					if (ex3 != null)
					{
						result = new ProxyGenerationError(ProxyGenerationError.GeneratorState.LoadMetadata, metadataItem.FileName, ex3);
					}
					else
					{
						result = new ProxyGenerationError(ProxyGenerationError.GeneratorState.LoadMetadata, metadataItem.FileName, (InvalidOperationException)ex);
					}
				}
			}
			else
			{
				result = new ProxyGenerationError(ProxyGenerationError.GeneratorState.LoadMetadata, metadataItem.FileName, ex);
			}
			return result;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006EF0 File Offset: 0x000050F0
		private static void RemoveDuplicatedSchemaItems(List<MetadataSection> metadataCollection, IList<ProxyGenerationError> importErrors)
		{
			Dictionary<XmlSchema, MetadataSection> dictionary = new Dictionary<XmlSchema, MetadataSection>();
			foreach (MetadataSection metadataSection in metadataCollection)
			{
				if (metadataSection.Dialect == MetadataSection.XmlSchemaDialect)
				{
					XmlSchema key = (XmlSchema)metadataSection.Metadata;
					dictionary.Add(key, metadataSection);
				}
			}
			foreach (MetadataSection metadataSection2 in metadataCollection)
			{
				if (metadataSection2.Dialect == MetadataSection.ServiceDescriptionDialect)
				{
					System.Web.Services.Description.ServiceDescription serviceDescription = (System.Web.Services.Description.ServiceDescription)metadataSection2.Metadata;
					foreach (object obj in serviceDescription.Types.Schemas)
					{
						XmlSchema xmlSchema = (XmlSchema)obj;
						xmlSchema.SourceUri = serviceDescription.RetrievalUrl;
						dictionary.Add(xmlSchema, metadataSection2);
					}
				}
			}
			IEnumerable<XmlSchema> enumerable;
			SchemaMerger.MergeSchemas(dictionary.Keys, importErrors, out enumerable);
			if (enumerable != null)
			{
				foreach (XmlSchema xmlSchema2 in enumerable)
				{
					MetadataSection metadataSection3 = dictionary[xmlSchema2];
					if (metadataSection3.Dialect == MetadataSection.XmlSchemaDialect)
					{
						metadataCollection.Remove(metadataSection3);
					}
					else if (metadataSection3.Dialect == MetadataSection.ServiceDescriptionDialect)
					{
						System.Web.Services.Description.ServiceDescription serviceDescription2 = (System.Web.Services.Description.ServiceDescription)metadataSection3.Metadata;
						serviceDescription2.Types.Schemas.Remove(xmlSchema2);
					}
				}
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000070D4 File Offset: 0x000052D4
		private static void CheckDuplicatedWsdlItems(IList<MetadataSection> metadataCollection, IList<ProxyGenerationError> importErrors)
		{
			List<System.Web.Services.Description.ServiceDescription> list = new List<System.Web.Services.Description.ServiceDescription>();
			foreach (MetadataSection metadataSection in metadataCollection)
			{
				if (metadataSection.Dialect == MetadataSection.ServiceDescriptionDialect)
				{
					System.Web.Services.Description.ServiceDescription item = (System.Web.Services.Description.ServiceDescription)metadataSection.Metadata;
					list.Add(item);
				}
			}
			WsdlInspector.CheckDuplicatedWsdlItems(list, importErrors);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007148 File Offset: 0x00005348
		protected static void ImportWCFModel(WsdlImporter importer, CodeCompileUnit compileUnit, IList<ProxyGenerationError> generationErrors, out List<ServiceEndpoint> serviceEndpointList, out IEnumerable<System.ServiceModel.Channels.Binding> bindingCollection, out IEnumerable<ContractDescription> contractCollection)
		{
			IWsdlImportExtension wsdlImportExtension = new AsmxEndpointPickerExtension();
			wsdlImportExtension.BeforeImport(importer.WsdlDocuments, null, null);
			serviceEndpointList = new List<ServiceEndpoint>();
			importer.ImportAllEndpoints();
			foreach (object obj in importer.WsdlDocuments)
			{
				System.Web.Services.Description.ServiceDescription serviceDescription = (System.Web.Services.Description.ServiceDescription)obj;
				foreach (object obj2 in serviceDescription.Services)
				{
					Service service = (Service)obj2;
					foreach (object obj3 in service.Ports)
					{
						Port wsdlPort = (Port)obj3;
						try
						{
							ServiceEndpoint item = importer.ImportEndpoint(wsdlPort);
							serviceEndpointList.Add(item);
						}
						catch (InvalidOperationException)
						{
						}
						catch (Exception errorException)
						{
							generationErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.GenerateCode, serviceDescription.RetrievalUrl, errorException));
						}
					}
				}
			}
			bindingCollection = importer.ImportAllBindings();
			contractCollection = importer.ImportAllContracts();
			foreach (MetadataConversionError errorMessage in importer.Errors)
			{
				generationErrors.Add(new ProxyGenerationError(errorMessage));
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000072FC File Offset: 0x000054FC
		private static void PatchConfigurationNameInServiceContractAttribute(CodeCompileUnit proxyCodeUnit, string proxyNamespace, string configNamespace)
		{
			if (proxyNamespace == null)
			{
				proxyNamespace = string.Empty;
			}
			string originalNamespace = VSWCFServiceContractGenerator.MakePeriodTerminatedNamespacePrefix(proxyNamespace);
			string replacementNamespace = VSWCFServiceContractGenerator.MakePeriodTerminatedNamespacePrefix(configNamespace);
			if (proxyCodeUnit != null)
			{
				foreach (object obj in proxyCodeUnit.Namespaces)
				{
					CodeNamespace codeNamespace = (CodeNamespace)obj;
					if (string.Equals(proxyNamespace, codeNamespace.Name, StringComparison.Ordinal))
					{
						foreach (object obj2 in codeNamespace.Types)
						{
							CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)obj2;
							if (codeTypeDeclaration.IsInterface)
							{
								foreach (object obj3 in codeTypeDeclaration.CustomAttributes)
								{
									CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj3;
									if (string.Equals(codeAttributeDeclaration.AttributeType.BaseType, typeof(ServiceContractAttribute).FullName, StringComparison.Ordinal))
									{
										foreach (object obj4 in codeAttributeDeclaration.Arguments)
										{
											CodeAttributeArgument codeAttributeArgument = (CodeAttributeArgument)obj4;
											if (string.Equals(codeAttributeArgument.Name, "ConfigurationName", StringComparison.Ordinal))
											{
												CodePrimitiveExpression codePrimitiveExpression = codeAttributeArgument.Value as CodePrimitiveExpression;
												if (codePrimitiveExpression != null && codePrimitiveExpression.Value is string)
												{
													codePrimitiveExpression.Value = VSWCFServiceContractGenerator.ReplaceNamespace(originalNamespace, replacementNamespace, (string)codePrimitiveExpression.Value);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007520 File Offset: 0x00005720
		private static void PatchOutParametersInVB(CodeCompileUnit codeCompileUnit)
		{
			foreach (object obj in codeCompileUnit.Namespaces)
			{
				CodeNamespace codeNamespace = (CodeNamespace)obj;
				foreach (object obj2 in codeNamespace.Types)
				{
					CodeTypeDeclaration codeClass = (CodeTypeDeclaration)obj2;
					VSWCFServiceContractGenerator.PatchTypeDeclaration(codeClass);
				}
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000075C0 File Offset: 0x000057C0
		private static void PatchTypeDeclaration(CodeTypeDeclaration codeClass)
		{
			foreach (object obj in codeClass.Members)
			{
				CodeTypeMember codeTypeMember = (CodeTypeMember)obj;
				if (codeTypeMember is CodeTypeDeclaration)
				{
					VSWCFServiceContractGenerator.PatchTypeDeclaration((CodeTypeDeclaration)codeTypeMember);
				}
				else if (codeTypeMember is CodeMemberMethod)
				{
					CodeMemberMethod codeMemberMethod = codeTypeMember as CodeMemberMethod;
					foreach (object obj2 in codeMemberMethod.Parameters)
					{
						CodeParameterDeclarationExpression codeParameterDeclarationExpression = (CodeParameterDeclarationExpression)obj2;
						if (codeParameterDeclarationExpression.Direction == FieldDirection.Out && !VSWCFServiceContractGenerator.IsDefinedInCodeAttributeCollection(typeof(OutAttribute), codeParameterDeclarationExpression.CustomAttributes))
						{
							codeParameterDeclarationExpression.CustomAttributes.Add(VSWCFServiceContractGenerator.OutAttribute);
						}
					}
				}
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000076B8 File Offset: 0x000058B8
		private static bool IsDefinedInCodeAttributeCollection(Type type, CodeAttributeDeclarationCollection metadata)
		{
			foreach (object obj in metadata)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj;
				if (string.Equals(codeAttributeDeclaration.Name, type.FullName, StringComparison.Ordinal) || string.Equals(codeAttributeDeclaration.Name, type.Name, StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007734 File Offset: 0x00005934
		private static bool IsVBCodeDomProvider(CodeDomProvider codeDomProvider)
		{
			string fileExtension = codeDomProvider.FileExtension;
			bool result;
			try
			{
				string languageFromExtension = CodeDomProvider.GetLanguageFromExtension(fileExtension);
				result = string.Equals(languageFromExtension, "vb", StringComparison.OrdinalIgnoreCase);
			}
			catch (ConfigurationException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007774 File Offset: 0x00005974
		private static bool ContainsHttpBindings(IEnumerable<MetadataSection> metadataCollection)
		{
			foreach (MetadataSection metadataSection in metadataCollection)
			{
				if (metadataSection.Dialect == MetadataSection.ServiceDescriptionDialect)
				{
					System.Web.Services.Description.ServiceDescription wsdlFile = (System.Web.Services.Description.ServiceDescription)metadataSection.Metadata;
					if (VSWCFServiceContractGenerator.ContainsHttpBindings(wsdlFile))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000077E4 File Offset: 0x000059E4
		internal static bool ContainsHttpBindings(System.Web.Services.Description.ServiceDescription wsdlFile)
		{
			foreach (object obj in wsdlFile.Bindings)
			{
				System.Web.Services.Description.Binding binding = (System.Web.Services.Description.Binding)obj;
				foreach (object obj2 in binding.Extensions)
				{
					HttpBinding httpBinding = obj2 as HttpBinding;
					if (httpBinding != null)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000075 RID: 117
		private const string VB_LANGUAGE_NAME = "vb";

		// Token: 0x04000076 RID: 118
		private IEnumerable<System.ServiceModel.Channels.Binding> bindingCollection;

		// Token: 0x04000077 RID: 119
		private IEnumerable<ContractDescription> contractCollection;

		// Token: 0x04000078 RID: 120
		private List<ServiceEndpoint> serviceEndpointList;

		// Token: 0x04000079 RID: 121
		private Dictionary<ServiceEndpoint, ChannelEndpointElement> serviceEndpointToChannelEndpointElementMap;

		// Token: 0x0400007A RID: 122
		private List<GeneratedContractType> proxyGeneratedContractTypes;

		// Token: 0x0400007B RID: 123
		private CodeCompileUnit targetCompileUnit;

		// Token: 0x0400007C RID: 124
		private Configuration targetConfiguration;

		// Token: 0x0400007D RID: 125
		private IEnumerable<ProxyGenerationError> proxyGenerationErrors;

		// Token: 0x0400007E RID: 126
		private IList<ProxyGenerationError> importErrors;

		// Token: 0x0400007F RID: 127
		private static CodeAttributeDeclaration outAttribute;

		// Token: 0x04000080 RID: 128
		private const int FRAMEWORK_VERSION_35 = 196613;

		// Token: 0x04000081 RID: 129
		private static Type[] unsupportedTypesInFramework30 = new Type[]
		{
			typeof(DateTimeOffset)
		};
	}
}
