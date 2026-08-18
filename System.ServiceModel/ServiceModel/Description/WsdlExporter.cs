using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime;
using System.ServiceModel.Channels;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Description
{
	// Token: 0x0200042A RID: 1066
	public class WsdlExporter : MetadataExporter
	{
		// Token: 0x06002939 RID: 10553 RVA: 0x0009C968 File Offset: 0x0009AB68
		public override void ExportContract(ContractDescription contract)
		{
			if (this.isFaulted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("WsdlExporterIsFaulted")));
			}
			if (contract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contract");
			}
			if (!this.exportedContracts.ContainsKey(contract))
			{
				try
				{
					PortType portType = this.CreateWsdlPortType(contract);
					WsdlContractConversionContext wsdlContractConversionContext = new WsdlContractConversionContext(contract, portType);
					foreach (OperationDescription operationDescription in contract.Operations)
					{
						bool flag;
						if (!WsdlExporter.OperationIsExportable(operationDescription, out flag))
						{
							string warningMessage = flag ? SR.GetString("WarnSkippingOpertationWithWildcardAction", new object[]
							{
								contract.Name,
								contract.Namespace,
								operationDescription.Name
							}) : SR.GetString("WarnSkippingOpertationWithSessionOpenNotificationEnabled", new object[]
							{
								"Action",
								"http://schemas.microsoft.com/2011/02/session/onopen",
								contract.Name,
								contract.Namespace,
								operationDescription.Name
							});
							this.LogExportWarning(warningMessage);
						}
						else
						{
							Operation operation = this.CreateWsdlOperation(operationDescription, contract);
							portType.Operations.Add(operation);
							wsdlContractConversionContext.AddOperation(operationDescription, operation);
							foreach (MessageDescription messageDescription in operationDescription.Messages)
							{
								OperationMessage operationMessage = this.CreateWsdlOperationMessage(messageDescription);
								operation.Messages.Add(operationMessage);
								wsdlContractConversionContext.AddMessage(messageDescription, operationMessage);
							}
							foreach (FaultDescription faultDescription in operationDescription.Faults)
							{
								OperationFault operationFault = this.CreateWsdlOperationFault(faultDescription);
								operation.Faults.Add(operationFault);
								wsdlContractConversionContext.AddFault(faultDescription, operationFault);
							}
						}
					}
					this.CallExportContract(wsdlContractConversionContext);
					this.exportedContracts.Add(contract, wsdlContractConversionContext);
				}
				catch
				{
					this.isFaulted = true;
					throw;
				}
			}
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public override void ExportEndpoint(ServiceEndpoint endpoint)
		{
			if (this.isFaulted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("WsdlExporterIsFaulted")));
			}
			if (endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
			}
			this.ExportEndpoint(endpoint, new XmlQualifiedName("service", "http://tempuri.org/"), null);
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x0009CC21 File Offset: 0x0009AE21
		public void ExportEndpoints(IEnumerable<ServiceEndpoint> endpoints, XmlQualifiedName wsdlServiceQName)
		{
			this.ExportEndpoints(endpoints, wsdlServiceQName, null);
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x0009CC2C File Offset: 0x0009AE2C
		internal void ExportEndpoints(IEnumerable<ServiceEndpoint> endpoints, XmlQualifiedName wsdlServiceQName, BindingParameterCollection bindingParameters)
		{
			if (this.isFaulted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("WsdlExporterIsFaulted")));
			}
			if (endpoints == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoints");
			}
			if (wsdlServiceQName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wsdlServiceQName");
			}
			foreach (ServiceEndpoint endpoint in endpoints)
			{
				this.ExportEndpoint(endpoint, wsdlServiceQName, bindingParameters);
			}
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x0009CCC4 File Offset: 0x0009AEC4
		public override MetadataSet GetGeneratedMetadata()
		{
			MetadataSet metadataSet = new MetadataSet();
			foreach (object obj in this.wsdlDocuments)
			{
				ServiceDescription serviceDescription = (ServiceDescription)obj;
				metadataSet.MetadataSections.Add(MetadataSection.CreateFromServiceDescription(serviceDescription));
			}
			foreach (object obj2 in this.xmlSchemas.Schemas())
			{
				XmlSchema schema = (XmlSchema)obj2;
				metadataSet.MetadataSections.Add(MetadataSection.CreateFromSchema(schema));
			}
			return metadataSet;
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x0600293E RID: 10558 RVA: 0x0009CD8C File Offset: 0x0009AF8C
		public ServiceDescriptionCollection GeneratedWsdlDocuments
		{
			get
			{
				return this.wsdlDocuments;
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x0600293F RID: 10559 RVA: 0x0009CD94 File Offset: 0x0009AF94
		public XmlSchemaSet GeneratedXmlSchemas
		{
			get
			{
				return this.xmlSchemas;
			}
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x0009CD9C File Offset: 0x0009AF9C
		private void ExportEndpoint(ServiceEndpoint endpoint, XmlQualifiedName wsdlServiceQName, BindingParameterCollection bindingParameters)
		{
			if (endpoint.Binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("EndpointsMustHaveAValidBinding1", new object[]
				{
					endpoint.Name
				})));
			}
			WsdlExporter.EndpointDictionaryKey key = new WsdlExporter.EndpointDictionaryKey(endpoint, wsdlServiceQName);
			try
			{
				if (!this.exportedEndpoints.ContainsKey(key))
				{
					this.ExportContract(endpoint.Contract);
					WsdlContractConversionContext contractContext = this.exportedContracts[endpoint.Contract];
					Port port;
					bool flag;
					bool flag2;
					System.Web.Services.Description.Binding binding = this.CreateWsdlBindingAndPort(endpoint, wsdlServiceQName, out port, out flag, out flag2);
					if (flag || port != null)
					{
						WsdlEndpointConversionContext wsdlEndpointConversionContext;
						if (flag)
						{
							wsdlEndpointConversionContext = new WsdlEndpointConversionContext(contractContext, endpoint, binding, port);
							foreach (OperationDescription operationDescription in endpoint.Contract.Operations)
							{
								if (WsdlExporter.OperationIsExportable(operationDescription))
								{
									OperationBinding operationBinding = this.CreateWsdlOperationBinding(endpoint.Contract, operationDescription);
									binding.Operations.Add(operationBinding);
									wsdlEndpointConversionContext.AddOperationBinding(operationDescription, operationBinding);
									foreach (MessageDescription messageDescription in operationDescription.Messages)
									{
										MessageBinding wsdlMessageBinding = this.CreateWsdlMessageBinding(messageDescription, endpoint.Binding, operationBinding);
										wsdlEndpointConversionContext.AddMessageBinding(messageDescription, wsdlMessageBinding);
									}
									foreach (FaultDescription faultDescription in operationDescription.Faults)
									{
										FaultBinding wsdlFaultBinding = this.CreateWsdlFaultBinding(faultDescription, endpoint.Binding, operationBinding);
										wsdlEndpointConversionContext.AddFaultBinding(faultDescription, wsdlFaultBinding);
									}
								}
							}
							PolicyConversionContext policyContext;
							if (bindingParameters == null)
							{
								policyContext = base.ExportPolicy(endpoint);
							}
							else
							{
								policyContext = base.ExportPolicy(endpoint, bindingParameters);
							}
							new WsdlExporter.WSPolicyAttachmentHelper(base.PolicyVersion).AttachPolicy(endpoint, wsdlEndpointConversionContext, policyContext);
							this.exportedBindings.Add(new WsdlExporter.BindingDictionaryKey(endpoint.Contract, endpoint.Binding), wsdlEndpointConversionContext);
						}
						else
						{
							wsdlEndpointConversionContext = new WsdlEndpointConversionContext(this.exportedBindings[new WsdlExporter.BindingDictionaryKey(endpoint.Contract, endpoint.Binding)], endpoint, port);
						}
						this.CallExportEndpoint(wsdlEndpointConversionContext);
						this.exportedEndpoints.Add(key, endpoint);
						if (flag2)
						{
							base.Errors.Add(new MetadataConversionError(SR.GetString("WarnDuplicateBindingQNameNameOnExport", new object[]
							{
								endpoint.Binding.Name,
								endpoint.Binding.Namespace,
								endpoint.Contract.Name
							}), true));
						}
					}
				}
			}
			catch
			{
				this.isFaulted = true;
				throw;
			}
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x0009D094 File Offset: 0x0009B294
		private void CallExportEndpoint(WsdlEndpointConversionContext endpointContext)
		{
			foreach (IWsdlExportExtension extension in endpointContext.ExportExtensions)
			{
				this.CallExtension(endpointContext, extension);
			}
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x0009D0E4 File Offset: 0x0009B2E4
		private void CallExportContract(WsdlContractConversionContext contractContext)
		{
			foreach (IWsdlExportExtension extension in contractContext.ExportExtensions)
			{
				this.CallExtension(contractContext, extension);
			}
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x0009D134 File Offset: 0x0009B334
		private PortType CreateWsdlPortType(ContractDescription contract)
		{
			XmlQualifiedName portTypeQName = WsdlExporter.WsdlNamingHelper.GetPortTypeQName(contract);
			ServiceDescription orCreateWsdl = this.GetOrCreateWsdl(portTypeQName.Namespace);
			PortType portType = new PortType();
			portType.Name = portTypeQName.Name;
			if (orCreateWsdl.PortTypes[portType.Name] != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("DuplicateContractQNameNameOnExport", new object[]
				{
					contract.Name,
					contract.Namespace
				})));
			}
			WsdlExporter.NetSessionHelper.AddUsingSessionAttributeIfNeeded(portType, contract);
			orCreateWsdl.PortTypes.Add(portType);
			return portType;
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x0009D1C4 File Offset: 0x0009B3C4
		private Operation CreateWsdlOperation(OperationDescription operation, ContractDescription contract)
		{
			Operation operation2 = new Operation();
			operation2.Name = WsdlExporter.WsdlNamingHelper.GetWsdlOperationName(operation, contract);
			WsdlExporter.NetSessionHelper.AddInitiatingTerminatingAttributesIfNeeded(operation2, operation, contract);
			return operation2;
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x0009D1F0 File Offset: 0x0009B3F0
		private OperationMessage CreateWsdlOperationMessage(MessageDescription message)
		{
			OperationMessage operationMessage;
			if (message.Direction == MessageDirection.Input)
			{
				operationMessage = new OperationInput();
			}
			else
			{
				operationMessage = new OperationOutput();
			}
			if (!XmlName.IsNullOrEmpty(message.MessageName))
			{
				operationMessage.Name = message.MessageName.EncodedName;
			}
			WsdlExporter.WSAddressingHelper.AddActionAttribute(message.Action, operationMessage, base.PolicyVersion);
			return operationMessage;
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x0009D244 File Offset: 0x0009B444
		private OperationFault CreateWsdlOperationFault(FaultDescription fault)
		{
			OperationFault operationFault = new OperationFault();
			operationFault.Name = fault.Name;
			WsdlExporter.WSAddressingHelper.AddActionAttribute(fault.Action, operationFault, base.PolicyVersion);
			return operationFault;
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x0009D278 File Offset: 0x0009B478
		private System.Web.Services.Description.Binding CreateWsdlBindingAndPort(ServiceEndpoint endpoint, XmlQualifiedName wsdlServiceQName, out Port wsdlPort, out bool newBinding, out bool bindingNameWasUniquified)
		{
			bool flag = WsdlExporter.IsWsdlExportable(endpoint.Binding);
			WsdlEndpointConversionContext wsdlEndpointConversionContext;
			XmlQualifiedName xmlQualifiedName;
			System.Web.Services.Description.Binding binding;
			if (!this.exportedBindings.TryGetValue(new WsdlExporter.BindingDictionaryKey(endpoint.Contract, endpoint.Binding), out wsdlEndpointConversionContext))
			{
				xmlQualifiedName = WsdlExporter.WsdlNamingHelper.GetBindingQName(endpoint, this, out bindingNameWasUniquified);
				ServiceDescription serviceDescription = this.GetOrCreateWsdl(xmlQualifiedName.Namespace);
				binding = new System.Web.Services.Description.Binding();
				binding.Name = xmlQualifiedName.Name;
				newBinding = true;
				PortType wsdlPortType = this.exportedContracts[endpoint.Contract].WsdlPortType;
				XmlQualifiedName xmlQualifiedName2 = new XmlQualifiedName(wsdlPortType.Name, wsdlPortType.ServiceDescription.TargetNamespace);
				binding.Type = xmlQualifiedName2;
				if (flag)
				{
					serviceDescription.Bindings.Add(binding);
				}
				WsdlExporter.EnsureWsdlContainsImport(serviceDescription, xmlQualifiedName2.Namespace);
			}
			else
			{
				xmlQualifiedName = new XmlQualifiedName(wsdlEndpointConversionContext.WsdlBinding.Name, wsdlEndpointConversionContext.WsdlBinding.ServiceDescription.TargetNamespace);
				bindingNameWasUniquified = false;
				ServiceDescription serviceDescription = this.wsdlDocuments[xmlQualifiedName.Namespace];
				binding = serviceDescription.Bindings[xmlQualifiedName.Name];
				XmlQualifiedName xmlQualifiedName2 = binding.Type;
				newBinding = false;
			}
			if (endpoint.Address != null)
			{
				Service orCreateWsdlService = this.GetOrCreateWsdlService(wsdlServiceQName);
				wsdlPort = new Port();
				string portName = WsdlExporter.WsdlNamingHelper.GetPortName(endpoint, orCreateWsdlService);
				wsdlPort.Name = portName;
				wsdlPort.Binding = xmlQualifiedName;
				SoapAddressBinding orCreateSoapAddressBinding = SoapHelper.GetOrCreateSoapAddressBinding(binding, wsdlPort, this);
				if (orCreateSoapAddressBinding != null)
				{
					orCreateSoapAddressBinding.Location = endpoint.Address.Uri.AbsoluteUri;
				}
				WsdlExporter.EnsureWsdlContainsImport(orCreateWsdlService.ServiceDescription, xmlQualifiedName.Namespace);
				if (flag)
				{
					orCreateWsdlService.Ports.Add(wsdlPort);
				}
			}
			else
			{
				wsdlPort = null;
			}
			return binding;
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x0009D41C File Offset: 0x0009B61C
		private OperationBinding CreateWsdlOperationBinding(ContractDescription contract, OperationDescription operation)
		{
			return new OperationBinding
			{
				Name = WsdlExporter.WsdlNamingHelper.GetWsdlOperationName(operation, contract)
			};
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x0009D440 File Offset: 0x0009B640
		private MessageBinding CreateWsdlMessageBinding(MessageDescription messageDescription, System.ServiceModel.Channels.Binding binding, OperationBinding wsdlOperationBinding)
		{
			MessageBinding messageBinding;
			if (messageDescription.Direction == MessageDirection.Input)
			{
				wsdlOperationBinding.Input = new InputBinding();
				messageBinding = wsdlOperationBinding.Input;
			}
			else
			{
				wsdlOperationBinding.Output = new OutputBinding();
				messageBinding = wsdlOperationBinding.Output;
			}
			if (!XmlName.IsNullOrEmpty(messageDescription.MessageName))
			{
				messageBinding.Name = messageDescription.MessageName.EncodedName;
			}
			return messageBinding;
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x0009D49C File Offset: 0x0009B69C
		private FaultBinding CreateWsdlFaultBinding(FaultDescription faultDescription, System.ServiceModel.Channels.Binding binding, OperationBinding wsdlOperationBinding)
		{
			FaultBinding faultBinding = new FaultBinding();
			wsdlOperationBinding.Faults.Add(faultBinding);
			if (faultDescription.Name != null)
			{
				faultBinding.Name = faultDescription.Name;
			}
			return faultBinding;
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x0009D4D4 File Offset: 0x0009B6D4
		internal static bool OperationIsExportable(OperationDescription operation)
		{
			bool flag;
			return WsdlExporter.OperationIsExportable(operation, out flag);
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x0009D4EC File Offset: 0x0009B6EC
		internal static bool OperationIsExportable(OperationDescription operation, out bool isWildcardAction)
		{
			isWildcardAction = false;
			if (operation.IsSessionOpenNotificationEnabled)
			{
				return false;
			}
			for (int i = 0; i < operation.Messages.Count; i++)
			{
				if (operation.Messages[i].Action == "*")
				{
					isWildcardAction = true;
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x0009D540 File Offset: 0x0009B740
		internal static bool IsBuiltInOperationBehavior(IWsdlExportExtension extension)
		{
			DataContractSerializerOperationBehavior dataContractSerializerOperationBehavior = extension as DataContractSerializerOperationBehavior;
			if (dataContractSerializerOperationBehavior != null)
			{
				return dataContractSerializerOperationBehavior.IsBuiltInOperationBehavior;
			}
			XmlSerializerOperationBehavior xmlSerializerOperationBehavior = extension as XmlSerializerOperationBehavior;
			return xmlSerializerOperationBehavior != null && xmlSerializerOperationBehavior.IsBuiltInOperationBehavior;
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x0600294E RID: 10574 RVA: 0x0009D570 File Offset: 0x0009B770
		private static XmlDocument XmlDoc
		{
			get
			{
				if (WsdlExporter.xmlDocument == null)
				{
					NameTable nameTable = new NameTable();
					nameTable.Add("Policy");
					nameTable.Add("All");
					nameTable.Add("ExactlyOne");
					nameTable.Add("PolicyURIs");
					nameTable.Add("Id");
					nameTable.Add("UsingAddressing");
					nameTable.Add("UsingAddressing");
					nameTable.Add("Addressing");
					nameTable.Add("AnonymousResponses");
					nameTable.Add("NonAnonymousResponses");
					WsdlExporter.xmlDocument = new XmlDocument(nameTable);
				}
				return WsdlExporter.xmlDocument;
			}
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x0009D618 File Offset: 0x0009B818
		internal ServiceDescription GetOrCreateWsdl(string ns)
		{
			ServiceDescriptionCollection serviceDescriptionCollection = this.wsdlDocuments;
			ServiceDescription serviceDescription = serviceDescriptionCollection[ns];
			if (serviceDescription == null)
			{
				serviceDescription = new ServiceDescription();
				serviceDescription.TargetNamespace = ns;
				XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces(new WsdlExporter.WsdlNamespaceHelper(base.PolicyVersion).SerializerNamespaces);
				if (!string.IsNullOrEmpty(serviceDescription.TargetNamespace))
				{
					xmlSerializerNamespaces.Add("tns", serviceDescription.TargetNamespace);
				}
				serviceDescription.Namespaces = xmlSerializerNamespaces;
				serviceDescriptionCollection.Add(serviceDescription);
			}
			return serviceDescription;
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x0009D688 File Offset: 0x0009B888
		private Service GetOrCreateWsdlService(XmlQualifiedName wsdlServiceQName)
		{
			ServiceDescription orCreateWsdl = this.GetOrCreateWsdl(wsdlServiceQName.Namespace);
			Service service = orCreateWsdl.Services[wsdlServiceQName.Name];
			if (service == null)
			{
				service = new Service();
				service.Name = wsdlServiceQName.Name;
				if (string.IsNullOrEmpty(orCreateWsdl.Name))
				{
					orCreateWsdl.Name = service.Name;
				}
				orCreateWsdl.Services.Add(service);
			}
			return service;
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x0009D6F0 File Offset: 0x0009B8F0
		private static void EnsureWsdlContainsImport(ServiceDescription srcWsdl, string target)
		{
			if (srcWsdl.TargetNamespace == target)
			{
				return;
			}
			foreach (object obj in srcWsdl.Imports)
			{
				Import import = (Import)obj;
				if (import.Namespace == target)
				{
					return;
				}
			}
			Import import2 = new Import();
			import2.Location = null;
			import2.Namespace = target;
			srcWsdl.Imports.Add(import2);
			WsdlExporter.WsdlNamespaceHelper.FindOrCreatePrefix("i", target, new DocumentableItem[]
			{
				srcWsdl
			});
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x0009D79C File Offset: 0x0009B99C
		private void LogExportWarning(string warningMessage)
		{
			base.Errors.Add(new MetadataConversionError(warningMessage, true));
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x0009D7B0 File Offset: 0x0009B9B0
		internal static XmlSchemaSet GetEmptySchemaSet()
		{
			return new XmlSchemaSet
			{
				XmlResolver = null
			};
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x0009D7CC File Offset: 0x0009B9CC
		private static bool IsWsdlExportable(System.ServiceModel.Channels.Binding binding)
		{
			BindingElementCollection bindingElementCollection = binding.CreateBindingElements();
			if (bindingElementCollection == null)
			{
				return true;
			}
			foreach (BindingElement bindingElement in bindingElementCollection)
			{
				MessageEncodingBindingElement messageEncodingBindingElement = bindingElement as MessageEncodingBindingElement;
				if (messageEncodingBindingElement != null && !messageEncodingBindingElement.IsWsdlExportable)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x0009D834 File Offset: 0x0009BA34
		private void CallExtension(WsdlContractConversionContext contractContext, IWsdlExportExtension extension)
		{
			try
			{
				extension.ExportContract(this, contractContext);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.ThrowExtensionException(contractContext.Contract, extension, ex));
			}
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x0009D880 File Offset: 0x0009BA80
		private void CallExtension(WsdlEndpointConversionContext endpointContext, IWsdlExportExtension extension)
		{
			try
			{
				extension.ExportEndpoint(this, endpointContext);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.ThrowExtensionException(endpointContext.Endpoint, extension, ex));
			}
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x0009D8CC File Offset: 0x0009BACC
		private Exception ThrowExtensionException(ContractDescription contract, IWsdlExportExtension exporter, Exception e)
		{
			string text = new XmlQualifiedName(contract.Name, contract.Namespace).ToString();
			string @string = SR.GetString("WsdlExtensionContractExportError", new object[]
			{
				exporter.GetType(),
				text
			});
			return new InvalidOperationException(@string, e);
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x0009D918 File Offset: 0x0009BB18
		private Exception ThrowExtensionException(ServiceEndpoint endpoint, IWsdlExportExtension exporter, Exception e)
		{
			string text;
			if (endpoint.Address != null && endpoint.Address.Uri != null)
			{
				text = endpoint.Address.Uri.ToString();
			}
			else
			{
				text = string.Format(CultureInfo.InvariantCulture, "Contract={1}:{0} ,Binding={3}:{2}", new object[]
				{
					endpoint.Contract.Name,
					endpoint.Contract.Namespace,
					endpoint.Binding.Name,
					endpoint.Binding.Namespace
				});
			}
			string @string = SR.GetString("WsdlExtensionEndpointExportError", new object[]
			{
				exporter.GetType(),
				text
			});
			return new InvalidOperationException(@string, e);
		}

		// Token: 0x0400227E RID: 8830
		private static XmlDocument xmlDocument;

		// Token: 0x0400227F RID: 8831
		private bool isFaulted;

		// Token: 0x04002280 RID: 8832
		private ServiceDescriptionCollection wsdlDocuments = new ServiceDescriptionCollection();

		// Token: 0x04002281 RID: 8833
		private XmlSchemaSet xmlSchemas = WsdlExporter.GetEmptySchemaSet();

		// Token: 0x04002282 RID: 8834
		private Dictionary<ContractDescription, WsdlContractConversionContext> exportedContracts = new Dictionary<ContractDescription, WsdlContractConversionContext>();

		// Token: 0x04002283 RID: 8835
		private Dictionary<WsdlExporter.BindingDictionaryKey, WsdlEndpointConversionContext> exportedBindings = new Dictionary<WsdlExporter.BindingDictionaryKey, WsdlEndpointConversionContext>();

		// Token: 0x04002284 RID: 8836
		private Dictionary<WsdlExporter.EndpointDictionaryKey, ServiceEndpoint> exportedEndpoints = new Dictionary<WsdlExporter.EndpointDictionaryKey, ServiceEndpoint>();

		// Token: 0x02000BEA RID: 3050
		internal static class WSAddressingHelper
		{
			// Token: 0x06007598 RID: 30104 RVA: 0x001B8A8C File Offset: 0x001B6C8C
			internal static void AddActionAttribute(string actionUri, OperationMessage wsdlOperationMessage, PolicyVersion policyVersion)
			{
				XmlAttribute xmlAttribute;
				if (policyVersion == PolicyVersion.Policy12)
				{
					xmlAttribute = WsdlExporter.XmlDoc.CreateAttribute("wsaw", "Action", "http://www.w3.org/2006/05/addressing/wsdl");
				}
				else
				{
					xmlAttribute = WsdlExporter.XmlDoc.CreateAttribute("wsam", "Action", "http://www.w3.org/2007/05/addressing/metadata");
				}
				xmlAttribute.Value = actionUri;
				wsdlOperationMessage.ExtensibleAttributes = new XmlAttribute[]
				{
					xmlAttribute
				};
			}

			// Token: 0x06007599 RID: 30105 RVA: 0x001B8AF0 File Offset: 0x001B6CF0
			internal static void AddAddressToWsdlPort(Port wsdlPort, EndpointAddress addr, AddressingVersion addressing)
			{
				if (addressing == AddressingVersion.None)
				{
					return;
				}
				MemoryStream memoryStream = new MemoryStream();
				XmlWriter xmlWriter = XmlWriter.Create(memoryStream);
				xmlWriter.WriteStartElement("temp");
				if (addressing == AddressingVersion.WSAddressing10)
				{
					xmlWriter.WriteAttributeString("xmlns", "wsa10", null, "http://www.w3.org/2005/08/addressing");
				}
				else
				{
					if (addressing != AddressingVersion.WSAddressingAugust2004)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("AddressingVersionNotSupported", new object[]
						{
							addressing
						})));
					}
					xmlWriter.WriteAttributeString("xmlns", "wsa", null, "http://schemas.xmlsoap.org/ws/2004/08/addressing");
				}
				addr.WriteTo(addressing, xmlWriter);
				xmlWriter.WriteEndElement();
				xmlWriter.Flush();
				memoryStream.Seek(0L, SeekOrigin.Begin);
				XmlReader xmlReader = XmlReader.Create(memoryStream);
				xmlReader.MoveToContent();
				XmlElement extension = (XmlElement)WsdlExporter.XmlDoc.ReadNode(xmlReader).ChildNodes[0];
				wsdlPort.Extensions.Add(extension);
			}

			// Token: 0x0600759A RID: 30106 RVA: 0x001B8BD8 File Offset: 0x001B6DD8
			internal static void AddWSAddressingAssertion(MetadataExporter exporter, PolicyConversionContext context, AddressingVersion addressVersion)
			{
				XmlElement xmlElement;
				if (addressVersion == AddressingVersion.WSAddressingAugust2004)
				{
					xmlElement = WsdlExporter.XmlDoc.CreateElement("wsap", "UsingAddressing", "http://schemas.xmlsoap.org/ws/2004/08/addressing/policy");
				}
				else if (addressVersion == AddressingVersion.WSAddressing10)
				{
					if (exporter.PolicyVersion == PolicyVersion.Policy12)
					{
						xmlElement = WsdlExporter.XmlDoc.CreateElement("wsaw", "UsingAddressing", "http://www.w3.org/2006/05/addressing/wsdl");
					}
					else
					{
						xmlElement = WsdlExporter.XmlDoc.CreateElement("wsam", "Addressing", "http://www.w3.org/2007/05/addressing/metadata");
						SupportedAddressingMode supportedAddressingMode = SupportedAddressingMode.Anonymous;
						string name = typeof(SupportedAddressingMode).Name;
						if (exporter.State.ContainsKey(name) && exporter.State[name] is SupportedAddressingMode)
						{
							supportedAddressingMode = (SupportedAddressingMode)exporter.State[name];
							if (!SupportedAddressingModeHelper.IsDefined(supportedAddressingMode))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SupportedAddressingModeNotSupported", new object[]
								{
									supportedAddressingMode
								})));
							}
						}
						if (supportedAddressingMode != SupportedAddressingMode.Mixed)
						{
							string localName;
							if (supportedAddressingMode == SupportedAddressingMode.Anonymous)
							{
								localName = "AnonymousResponses";
							}
							else
							{
								localName = "NonAnonymousResponses";
							}
							XmlElement xmlElement2 = WsdlExporter.XmlDoc.CreateElement("wsp", "Policy", "http://www.w3.org/ns/ws-policy");
							XmlElement newChild = WsdlExporter.XmlDoc.CreateElement("wsam", localName, "http://www.w3.org/2007/05/addressing/metadata");
							xmlElement2.AppendChild(newChild);
							xmlElement.AppendChild(xmlElement2);
						}
					}
				}
				else
				{
					if (addressVersion != AddressingVersion.None)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("AddressingVersionNotSupported", new object[]
						{
							addressVersion
						})));
					}
					xmlElement = null;
				}
				if (xmlElement != null)
				{
					context.GetBindingAssertions().Add(xmlElement);
				}
			}
		}

		// Token: 0x02000BEB RID: 3051
		private class WSPolicyAttachmentHelper
		{
			// Token: 0x0600759B RID: 30107 RVA: 0x001B8D71 File Offset: 0x001B6F71
			internal WSPolicyAttachmentHelper(PolicyVersion policyVersion)
			{
				this.policyVersion = policyVersion;
			}

			// Token: 0x0600759C RID: 30108 RVA: 0x001B8D80 File Offset: 0x001B6F80
			internal void AttachPolicy(ServiceEndpoint endpoint, WsdlEndpointConversionContext endpointContext, PolicyConversionContext policyContext)
			{
				SortedList<string, string> policyKeys = new SortedList<string, string>();
				NamingHelper.DoesNameExist doesNameExist = (string name, object nameCollection) => policyKeys.ContainsKey(name);
				ServiceDescription serviceDescription = endpointContext.WsdlBinding.ServiceDescription;
				ICollection<XmlElement> collection = policyContext.GetBindingAssertions();
				System.Web.Services.Description.Binding wsdlBinding = endpointContext.WsdlBinding;
				if (collection.Count > 0)
				{
					string baseName = WsdlExporter.WSPolicyAttachmentHelper.CreateBindingPolicyKey(wsdlBinding);
					string uniqueName = NamingHelper.GetUniqueName(baseName, doesNameExist, null);
					policyKeys.Add(uniqueName, uniqueName);
					this.AttachItemPolicy(collection, uniqueName, serviceDescription, wsdlBinding);
				}
				foreach (OperationDescription operationDescription in endpoint.Contract.Operations)
				{
					if (WsdlExporter.OperationIsExportable(operationDescription))
					{
						collection = policyContext.GetOperationBindingAssertions(operationDescription);
						if (collection.Count > 0)
						{
							OperationBinding operationBinding = endpointContext.GetOperationBinding(operationDescription);
							string baseName = WsdlExporter.WSPolicyAttachmentHelper.CreateOperationBindingPolicyKey(operationBinding);
							string uniqueName = NamingHelper.GetUniqueName(baseName, doesNameExist, null);
							policyKeys.Add(uniqueName, uniqueName);
							this.AttachItemPolicy(collection, uniqueName, serviceDescription, operationBinding);
						}
						foreach (MessageDescription messageDescription in operationDescription.Messages)
						{
							collection = policyContext.GetMessageBindingAssertions(messageDescription);
							if (collection.Count > 0)
							{
								MessageBinding messageBinding = endpointContext.GetMessageBinding(messageDescription);
								string baseName = WsdlExporter.WSPolicyAttachmentHelper.CreateMessageBindingPolicyKey(messageBinding, messageDescription.Direction);
								string uniqueName = NamingHelper.GetUniqueName(baseName, doesNameExist, null);
								policyKeys.Add(uniqueName, uniqueName);
								this.AttachItemPolicy(collection, uniqueName, serviceDescription, messageBinding);
							}
						}
						foreach (FaultDescription fault in operationDescription.Faults)
						{
							collection = policyContext.GetFaultBindingAssertions(fault);
							if (collection.Count > 0)
							{
								FaultBinding faultBinding = endpointContext.GetFaultBinding(fault);
								string baseName = WsdlExporter.WSPolicyAttachmentHelper.CreateFaultBindingPolicyKey(faultBinding);
								string uniqueName = NamingHelper.GetUniqueName(baseName, doesNameExist, null);
								policyKeys.Add(uniqueName, uniqueName);
								this.AttachItemPolicy(collection, uniqueName, serviceDescription, faultBinding);
							}
						}
					}
				}
			}

			// Token: 0x0600759D RID: 30109 RVA: 0x001B8FD4 File Offset: 0x001B71D4
			private void AttachItemPolicy(ICollection<XmlElement> assertions, string key, ServiceDescription policyWsdl, DocumentableItem item)
			{
				string policyKey = this.InsertPolicy(key, policyWsdl, assertions);
				this.InsertPolicyReference(policyKey, item);
			}

			// Token: 0x0600759E RID: 30110 RVA: 0x001B8FF4 File Offset: 0x001B71F4
			private void InsertPolicyReference(string policyKey, DocumentableItem item)
			{
				XmlElement xmlElement = WsdlExporter.XmlDoc.CreateElement("wsp", "PolicyReference", this.policyVersion.Namespace);
				XmlAttribute xmlAttribute = WsdlExporter.XmlDoc.CreateAttribute("URI");
				xmlAttribute.Value = policyKey;
				xmlElement.Attributes.Append(xmlAttribute);
				item.Extensions.Add(xmlElement);
			}

			// Token: 0x0600759F RID: 30111 RVA: 0x001B9054 File Offset: 0x001B7254
			private string InsertPolicy(string key, ServiceDescription policyWsdl, ICollection<XmlElement> assertions)
			{
				XmlElement xmlElement = this.CreatePolicyElement(assertions);
				XmlAttribute xmlAttribute = WsdlExporter.XmlDoc.CreateAttribute("wsu", "Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
				xmlAttribute.Value = key;
				xmlElement.SetAttributeNode(xmlAttribute);
				if (policyWsdl != null)
				{
					policyWsdl.Extensions.Add(xmlElement);
				}
				return string.Format(CultureInfo.InvariantCulture, "#{0}", new object[]
				{
					key
				});
			}

			// Token: 0x060075A0 RID: 30112 RVA: 0x001B90BC File Offset: 0x001B72BC
			private XmlElement CreatePolicyElement(ICollection<XmlElement> assertions)
			{
				XmlElement xmlElement = WsdlExporter.XmlDoc.CreateElement("wsp", "Policy", this.policyVersion.Namespace);
				XmlElement xmlElement2 = WsdlExporter.XmlDoc.CreateElement("wsp", "ExactlyOne", this.policyVersion.Namespace);
				xmlElement.AppendChild(xmlElement2);
				XmlElement xmlElement3 = WsdlExporter.XmlDoc.CreateElement("wsp", "All", this.policyVersion.Namespace);
				xmlElement2.AppendChild(xmlElement3);
				foreach (XmlElement node in assertions)
				{
					XmlNode newChild = WsdlExporter.XmlDoc.ImportNode(node, true);
					xmlElement3.AppendChild(newChild);
				}
				return xmlElement;
			}

			// Token: 0x060075A1 RID: 30113 RVA: 0x001B9188 File Offset: 0x001B7388
			private static string CreateBindingPolicyKey(System.Web.Services.Description.Binding wsdlBinding)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}_policy", new object[]
				{
					wsdlBinding.Name
				});
			}

			// Token: 0x060075A2 RID: 30114 RVA: 0x001B91A8 File Offset: 0x001B73A8
			private static string CreateOperationBindingPolicyKey(OperationBinding wsdlOperationBinding)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}_{1}_policy", new object[]
				{
					wsdlOperationBinding.Binding.Name,
					wsdlOperationBinding.Name
				});
			}

			// Token: 0x060075A3 RID: 30115 RVA: 0x001B91D8 File Offset: 0x001B73D8
			private static string CreateMessageBindingPolicyKey(MessageBinding wsdlMessageBinding, MessageDirection direction)
			{
				OperationBinding operationBinding = wsdlMessageBinding.OperationBinding;
				System.Web.Services.Description.Binding binding = operationBinding.Binding;
				if (direction == MessageDirection.Input)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0}_{1}_Input_policy", new object[]
					{
						binding.Name,
						operationBinding.Name
					});
				}
				return string.Format(CultureInfo.InvariantCulture, "{0}_{1}_output_policy", new object[]
				{
					binding.Name,
					operationBinding.Name
				});
			}

			// Token: 0x060075A4 RID: 30116 RVA: 0x001B9248 File Offset: 0x001B7448
			private static string CreateFaultBindingPolicyKey(FaultBinding wsdlFaultBinding)
			{
				OperationBinding operationBinding = wsdlFaultBinding.OperationBinding;
				System.Web.Services.Description.Binding binding = operationBinding.Binding;
				if (string.IsNullOrEmpty(wsdlFaultBinding.Name))
				{
					return string.Format(CultureInfo.InvariantCulture, "{0}_{1}_Fault", new object[]
					{
						binding.Name,
						operationBinding.Name
					});
				}
				return string.Format(CultureInfo.InvariantCulture, "{0}_{1}_{2}_Fault", new object[]
				{
					binding.Name,
					operationBinding.Name,
					wsdlFaultBinding.Name
				});
			}

			// Token: 0x0400427C RID: 17020
			private PolicyVersion policyVersion;
		}

		// Token: 0x02000BEC RID: 3052
		private class WsdlNamespaceHelper
		{
			// Token: 0x17001AFB RID: 6907
			// (get) Token: 0x060075A5 RID: 30117 RVA: 0x001B92C8 File Offset: 0x001B74C8
			internal XmlSerializerNamespaces SerializerNamespaces
			{
				get
				{
					if (this.xmlSerializerNamespaces == null)
					{
						WsdlExporter.WsdlNamespaceHelper.XmlSerializerNamespaceWrapper xmlSerializerNamespaceWrapper = new WsdlExporter.WsdlNamespaceHelper.XmlSerializerNamespaceWrapper();
						xmlSerializerNamespaceWrapper.Add("wsdl", "http://schemas.xmlsoap.org/wsdl/");
						xmlSerializerNamespaceWrapper.Add("xsd", "http://www.w3.org/2001/XMLSchema");
						xmlSerializerNamespaceWrapper.Add("wsp", this.policyVersion.Namespace);
						xmlSerializerNamespaceWrapper.Add("wsu", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
						xmlSerializerNamespaceWrapper.Add("wsa", "http://schemas.xmlsoap.org/ws/2004/08/addressing");
						xmlSerializerNamespaceWrapper.Add("wsap", "http://schemas.xmlsoap.org/ws/2004/08/addressing/policy");
						xmlSerializerNamespaceWrapper.Add("wsa10", "http://www.w3.org/2005/08/addressing");
						xmlSerializerNamespaceWrapper.Add("wsaw", "http://www.w3.org/2006/05/addressing/wsdl");
						xmlSerializerNamespaceWrapper.Add("wsam", "http://www.w3.org/2007/05/addressing/metadata");
						xmlSerializerNamespaceWrapper.Add("wsx", "http://schemas.xmlsoap.org/ws/2004/09/mex");
						xmlSerializerNamespaceWrapper.Add("msc", "http://schemas.microsoft.com/ws/2005/12/wsdl/contract");
						xmlSerializerNamespaceWrapper.Add("soapenc", "http://schemas.xmlsoap.org/soap/encoding/");
						xmlSerializerNamespaceWrapper.Add("soap12", "http://schemas.xmlsoap.org/wsdl/soap12/");
						xmlSerializerNamespaceWrapper.Add("soap", "http://schemas.xmlsoap.org/wsdl/soap/");
						this.xmlSerializerNamespaces = xmlSerializerNamespaceWrapper.GetNamespaces();
					}
					return this.xmlSerializerNamespaces;
				}
			}

			// Token: 0x060075A6 RID: 30118 RVA: 0x001B93DE File Offset: 0x001B75DE
			internal WsdlNamespaceHelper(PolicyVersion policyVersion)
			{
				this.policyVersion = policyVersion;
			}

			// Token: 0x060075A7 RID: 30119 RVA: 0x001B93F0 File Offset: 0x001B75F0
			internal static string FindOrCreatePrefix(string prefixBase, string ns, params DocumentableItem[] scopes)
			{
				if (scopes.Length == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "You must pass at least one namespaceScope", new object[0])));
				}
				string text = null;
				if (string.IsNullOrEmpty(ns))
				{
					text = string.Empty;
				}
				else
				{
					for (int i = 0; i < scopes.Length; i++)
					{
						if (WsdlExporter.WsdlNamespaceHelper.TryMatchNamespace(scopes[i].Namespaces.ToArray(), ns, out text))
						{
							return text;
						}
					}
					int num = 0;
					text = prefixBase + num.ToString(CultureInfo.InvariantCulture);
					while (WsdlExporter.WsdlNamespaceHelper.PrefixExists(scopes[0].Namespaces.ToArray(), text))
					{
						int num2;
						num = (num2 = num + 1);
						text = prefixBase + num2.ToString(CultureInfo.InvariantCulture);
					}
				}
				scopes[0].Namespaces.Add(text, ns);
				return text;
			}

			// Token: 0x060075A8 RID: 30120 RVA: 0x001B94B4 File Offset: 0x001B76B4
			private static bool PrefixExists(XmlQualifiedName[] prefixDefinitions, string prefix)
			{
				return Array.Exists<XmlQualifiedName>(prefixDefinitions, (XmlQualifiedName prefixDef) => prefixDef.Name == prefix);
			}

			// Token: 0x060075A9 RID: 30121 RVA: 0x001B94E0 File Offset: 0x001B76E0
			private static bool TryMatchNamespace(XmlQualifiedName[] prefixDefinitions, string ns, out string prefix)
			{
				string foundPrefix = null;
				Array.Find<XmlQualifiedName>(prefixDefinitions, delegate(XmlQualifiedName prefixDef)
				{
					if (prefixDef.Namespace == ns)
					{
						foundPrefix = prefixDef.Name;
						return true;
					}
					return false;
				});
				prefix = foundPrefix;
				return foundPrefix != null;
			}

			// Token: 0x0400427D RID: 17021
			private XmlSerializerNamespaces xmlSerializerNamespaces;

			// Token: 0x0400427E RID: 17022
			private PolicyVersion policyVersion;

			// Token: 0x02000F24 RID: 3876
			private class XmlSerializerNamespaceWrapper
			{
				// Token: 0x0600864E RID: 34382 RVA: 0x001F1D6E File Offset: 0x001EFF6E
				internal void Add(string prefix, string namespaceUri)
				{
					if (!this.lookup.ContainsKey(prefix))
					{
						this.namespaces.Add(prefix, namespaceUri);
						this.lookup.Add(prefix, namespaceUri);
					}
				}

				// Token: 0x0600864F RID: 34383 RVA: 0x001F1D98 File Offset: 0x001EFF98
				internal XmlSerializerNamespaces GetNamespaces()
				{
					return this.namespaces;
				}

				// Token: 0x04004DE9 RID: 19945
				private readonly XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();

				// Token: 0x04004DEA RID: 19946
				private readonly Dictionary<string, string> lookup = new Dictionary<string, string>();
			}
		}

		// Token: 0x02000BED RID: 3053
		internal static class WsdlNamingHelper
		{
			// Token: 0x060075AA RID: 30122 RVA: 0x001B9525 File Offset: 0x001B7725
			internal static XmlQualifiedName GetPortTypeQName(ContractDescription contract)
			{
				return new XmlQualifiedName(contract.Name, contract.Namespace);
			}

			// Token: 0x060075AB RID: 30123 RVA: 0x001B9538 File Offset: 0x001B7738
			internal static XmlQualifiedName GetBindingQName(ServiceEndpoint endpoint, WsdlExporter exporter, out bool wasUniquified)
			{
				string name = endpoint.Name;
				string @namespace = endpoint.Binding.Namespace;
				string uniqueName = NamingHelper.GetUniqueName(name, WsdlExporter.WsdlNamingHelper.WsdlBindingQNameExists(exporter, @namespace), null);
				wasUniquified = (name != uniqueName);
				return new XmlQualifiedName(uniqueName, @namespace);
			}

			// Token: 0x060075AC RID: 30124 RVA: 0x001B9578 File Offset: 0x001B7778
			private static NamingHelper.DoesNameExist WsdlBindingQNameExists(WsdlExporter exporter, string bindingWsdlNamespace)
			{
				return delegate(string localName, object nameCollection)
				{
					XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(localName, bindingWsdlNamespace);
					ServiceDescription serviceDescription = exporter.wsdlDocuments[bindingWsdlNamespace];
					return serviceDescription != null && serviceDescription.Bindings[localName] != null;
				};
			}

			// Token: 0x060075AD RID: 30125 RVA: 0x001B95A5 File Offset: 0x001B77A5
			internal static string GetPortName(ServiceEndpoint endpoint, Service wsdlService)
			{
				return NamingHelper.GetUniqueName(endpoint.Name, WsdlExporter.WsdlNamingHelper.ServiceContainsPort(wsdlService), null);
			}

			// Token: 0x060075AE RID: 30126 RVA: 0x001B95BC File Offset: 0x001B77BC
			private static NamingHelper.DoesNameExist ServiceContainsPort(Service service)
			{
				return delegate(string portName, object nameCollection)
				{
					foreach (object obj in service.Ports)
					{
						Port port = (Port)obj;
						if (port.Name == portName)
						{
							return true;
						}
					}
					return false;
				};
			}

			// Token: 0x060075AF RID: 30127 RVA: 0x001B95E2 File Offset: 0x001B77E2
			internal static string GetWsdlOperationName(OperationDescription operationDescription, ContractDescription parentContractDescription)
			{
				return operationDescription.Name;
			}
		}

		// Token: 0x02000BEE RID: 3054
		internal static class NetSessionHelper
		{
			// Token: 0x060075B0 RID: 30128 RVA: 0x001B95EC File Offset: 0x001B77EC
			internal static void AddUsingSessionAttributeIfNeeded(PortType wsdlPortType, ContractDescription contract)
			{
				bool b;
				if (contract.SessionMode == SessionMode.Required)
				{
					b = true;
				}
				else
				{
					if (contract.SessionMode != SessionMode.NotAllowed)
					{
						return;
					}
					b = false;
				}
				wsdlPortType.ExtensibleAttributes = WsdlExporter.NetSessionHelper.CloneAndAddToAttributes(wsdlPortType.ExtensibleAttributes, "msc", "usingSession", "http://schemas.microsoft.com/ws/2005/12/wsdl/contract", WsdlExporter.NetSessionHelper.ToValue(b));
			}

			// Token: 0x060075B1 RID: 30129 RVA: 0x001B963A File Offset: 0x001B783A
			internal static void AddInitiatingTerminatingAttributesIfNeeded(Operation wsdlOperation, OperationDescription operation, ContractDescription contract)
			{
				if (contract.SessionMode == SessionMode.Required)
				{
					WsdlExporter.NetSessionHelper.AddInitiatingAttribute(wsdlOperation, operation.IsInitiating);
					WsdlExporter.NetSessionHelper.AddTerminatingAttribute(wsdlOperation, operation.IsTerminating);
				}
			}

			// Token: 0x060075B2 RID: 30130 RVA: 0x001B965D File Offset: 0x001B785D
			private static void AddInitiatingAttribute(Operation wsdlOperation, bool isInitiating)
			{
				wsdlOperation.ExtensibleAttributes = WsdlExporter.NetSessionHelper.CloneAndAddToAttributes(wsdlOperation.ExtensibleAttributes, "msc", "isInitiating", "http://schemas.microsoft.com/ws/2005/12/wsdl/contract", WsdlExporter.NetSessionHelper.ToValue(isInitiating));
			}

			// Token: 0x060075B3 RID: 30131 RVA: 0x001B9685 File Offset: 0x001B7885
			private static void AddTerminatingAttribute(Operation wsdlOperation, bool isTerminating)
			{
				wsdlOperation.ExtensibleAttributes = WsdlExporter.NetSessionHelper.CloneAndAddToAttributes(wsdlOperation.ExtensibleAttributes, "msc", "isTerminating", "http://schemas.microsoft.com/ws/2005/12/wsdl/contract", WsdlExporter.NetSessionHelper.ToValue(isTerminating));
			}

			// Token: 0x060075B4 RID: 30132 RVA: 0x001B96B0 File Offset: 0x001B78B0
			private static XmlAttribute[] CloneAndAddToAttributes(XmlAttribute[] originalAttributes, string prefix, string localName, string ns, string value)
			{
				XmlAttribute xmlAttribute = WsdlExporter.XmlDoc.CreateAttribute(prefix, localName, ns);
				xmlAttribute.Value = value;
				int num = 0;
				if (originalAttributes != null)
				{
					num = originalAttributes.Length;
				}
				XmlAttribute[] array = new XmlAttribute[num + 1];
				if (originalAttributes != null)
				{
					originalAttributes.CopyTo(array, 0);
				}
				array[array.Length - 1] = xmlAttribute;
				return array;
			}

			// Token: 0x060075B5 RID: 30133 RVA: 0x001B96F9 File Offset: 0x001B78F9
			private static string ToValue(bool b)
			{
				if (!b)
				{
					return "false";
				}
				return "true";
			}

			// Token: 0x0400427F RID: 17023
			internal const string NamespaceUri = "http://schemas.microsoft.com/ws/2005/12/wsdl/contract";

			// Token: 0x04004280 RID: 17024
			internal const string Prefix = "msc";

			// Token: 0x04004281 RID: 17025
			internal const string UsingSession = "usingSession";

			// Token: 0x04004282 RID: 17026
			internal const string IsInitiating = "isInitiating";

			// Token: 0x04004283 RID: 17027
			internal const string IsTerminating = "isTerminating";

			// Token: 0x04004284 RID: 17028
			internal const string True = "true";

			// Token: 0x04004285 RID: 17029
			internal const string False = "false";
		}

		// Token: 0x02000BEF RID: 3055
		private sealed class BindingDictionaryKey
		{
			// Token: 0x060075B6 RID: 30134 RVA: 0x001B9709 File Offset: 0x001B7909
			public BindingDictionaryKey(ContractDescription contract, System.ServiceModel.Channels.Binding binding)
			{
				this.Contract = contract;
				this.Binding = binding;
			}

			// Token: 0x060075B7 RID: 30135 RVA: 0x001B9720 File Offset: 0x001B7920
			public override bool Equals(object obj)
			{
				WsdlExporter.BindingDictionaryKey bindingDictionaryKey = obj as WsdlExporter.BindingDictionaryKey;
				return bindingDictionaryKey != null && bindingDictionaryKey.Binding == this.Binding && bindingDictionaryKey.Contract == this.Contract;
			}

			// Token: 0x060075B8 RID: 30136 RVA: 0x001B9756 File Offset: 0x001B7956
			public override int GetHashCode()
			{
				return this.Contract.GetHashCode() ^ this.Binding.GetHashCode();
			}

			// Token: 0x04004286 RID: 17030
			public readonly ContractDescription Contract;

			// Token: 0x04004287 RID: 17031
			public readonly System.ServiceModel.Channels.Binding Binding;
		}

		// Token: 0x02000BF0 RID: 3056
		private sealed class EndpointDictionaryKey
		{
			// Token: 0x060075B9 RID: 30137 RVA: 0x001B976F File Offset: 0x001B796F
			public EndpointDictionaryKey(ServiceEndpoint endpoint, XmlQualifiedName serviceQName)
			{
				this.Endpoint = endpoint;
				this.ServiceQName = serviceQName;
			}

			// Token: 0x060075BA RID: 30138 RVA: 0x001B9788 File Offset: 0x001B7988
			public override bool Equals(object obj)
			{
				WsdlExporter.EndpointDictionaryKey endpointDictionaryKey = obj as WsdlExporter.EndpointDictionaryKey;
				return endpointDictionaryKey != null && endpointDictionaryKey.Endpoint == this.Endpoint && endpointDictionaryKey.ServiceQName == this.ServiceQName;
			}

			// Token: 0x060075BB RID: 30139 RVA: 0x001B97C3 File Offset: 0x001B79C3
			public override int GetHashCode()
			{
				return this.Endpoint.GetHashCode() ^ this.ServiceQName.GetHashCode();
			}

			// Token: 0x04004288 RID: 17032
			public readonly ServiceEndpoint Endpoint;

			// Token: 0x04004289 RID: 17033
			public readonly XmlQualifiedName ServiceQName;
		}
	}
}
