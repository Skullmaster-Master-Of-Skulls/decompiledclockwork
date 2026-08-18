using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime;
using System.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Text;
using System.Web.Services.Configuration;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.Description
{
	// Token: 0x0200042B RID: 1067
	public class WsdlImporter : MetadataImporter
	{
		// Token: 0x0600295A RID: 10586 RVA: 0x0009DA0B File Offset: 0x0009BC0B
		public WsdlImporter(MetadataSet metadata) : this(metadata, null, null, MetadataImporterQuotas.Defaults)
		{
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x0009DA1B File Offset: 0x0009BC1B
		public WsdlImporter(MetadataSet metadata, IEnumerable<IPolicyImportExtension> policyImportExtensions, IEnumerable<IWsdlImportExtension> wsdlImportExtensions) : this(metadata, policyImportExtensions, wsdlImportExtensions, MetadataImporterQuotas.Defaults)
		{
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x0009DA2C File Offset: 0x0009BC2C
		public WsdlImporter(MetadataSet metadata, IEnumerable<IPolicyImportExtension> policyImportExtensions, IEnumerable<IWsdlImportExtension> wsdlImportExtensions, MetadataImporterQuotas quotas) : base(policyImportExtensions, quotas)
		{
			if (wsdlImportExtensions == null)
			{
				wsdlImportExtensions = WsdlImporter.LoadWsdlExtensionsFromConfig();
			}
			this.wsdlExtensions = new KeyedByTypeCollection<IWsdlImportExtension>(wsdlImportExtensions);
			if (metadata == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("metadata");
			}
			this.ProcessMetadataDocuments(metadata.MetadataSections);
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x0600295D RID: 10589 RVA: 0x0009DACF File Offset: 0x0009BCCF
		public KeyedByTypeCollection<IWsdlImportExtension> WsdlImportExtensions
		{
			get
			{
				return this.wsdlExtensions;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x0600295E RID: 10590 RVA: 0x0009DAD7 File Offset: 0x0009BCD7
		public ServiceDescriptionCollection WsdlDocuments
		{
			get
			{
				return this.wsdlDocuments;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x0600295F RID: 10591 RVA: 0x0009DADF File Offset: 0x0009BCDF
		public XmlSchemaSet XmlSchemas
		{
			get
			{
				return this.xmlSchemas;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06002960 RID: 10592 RVA: 0x0009DAE7 File Offset: 0x0009BCE7
		private WsdlImporter.WsdlPolicyReader PolicyReader
		{
			get
			{
				if (this.wsdlPolicyReader == null)
				{
					this.wsdlPolicyReader = new WsdlImporter.WsdlPolicyReader(this);
				}
				return this.wsdlPolicyReader;
			}
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x0009DB03 File Offset: 0x0009BD03
		internal override XmlElement ResolvePolicyReference(string policyReference, XmlElement contextAssertion)
		{
			return this.PolicyReader.ResolvePolicyReference(policyReference, contextAssertion);
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x0009DB14 File Offset: 0x0009BD14
		public override Collection<ContractDescription> ImportAllContracts()
		{
			if (this.isFaulted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("WsdlImporterIsFaulted")));
			}
			this.EnsureBeforeImportCalled();
			Collection<ContractDescription> collection = new Collection<ContractDescription>();
			foreach (object obj in this.wsdlDocuments)
			{
				ServiceDescription serviceDescription = (ServiceDescription)obj;
				foreach (object obj2 in serviceDescription.PortTypes)
				{
					PortType portType = (PortType)obj2;
					if (!this.IsBlackListed(portType))
					{
						ContractDescription contractDescription = this.ImportWsdlPortType(portType, WsdlImporter.WsdlPortTypeImportOptions.ReuseExistingContracts, WsdlImporter.ErrorBehavior.DoNotThrowExceptions);
						if (contractDescription != null)
						{
							collection.Add(contractDescription);
						}
					}
				}
			}
			return collection;
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x0009DC00 File Offset: 0x0009BE00
		public override ServiceEndpointCollection ImportAllEndpoints()
		{
			if (this.isFaulted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("WsdlImporterIsFaulted")));
			}
			this.EnsureBeforeImportCalled();
			ServiceEndpointCollection serviceEndpointCollection = new ServiceEndpointCollection();
			foreach (Port port in this.GetAllPorts())
			{
				if (!this.IsBlackListed(port))
				{
					ServiceEndpoint serviceEndpoint = this.ImportWsdlPort(port, WsdlImporter.ErrorBehavior.DoNotThrowExceptions);
					if (serviceEndpoint != null)
					{
						serviceEndpointCollection.Add(serviceEndpoint);
					}
				}
			}
			return serviceEndpointCollection;
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x0009DC94 File Offset: 0x0009BE94
		public Collection<System.ServiceModel.Channels.Binding> ImportAllBindings()
		{
			if (this.isFaulted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("WsdlImporterIsFaulted")));
			}
			this.EnsureBeforeImportCalled();
			Collection<System.ServiceModel.Channels.Binding> collection = new Collection<System.ServiceModel.Channels.Binding>();
			foreach (System.Web.Services.Description.Binding binding in this.GetAllBindings())
			{
				if (!this.IsBlackListed(binding))
				{
					WsdlEndpointConversionContext wsdlEndpointConversionContext = this.ImportWsdlBinding(binding, WsdlImporter.ErrorBehavior.DoNotThrowExceptions);
					if (wsdlEndpointConversionContext != null)
					{
						collection.Add(wsdlEndpointConversionContext.Endpoint.Binding);
					}
				}
			}
			return collection;
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x0009DD34 File Offset: 0x0009BF34
		public ContractDescription ImportContract(PortType wsdlPortType)
		{
			if (this.isFaulted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("WsdlImporterIsFaulted")));
			}
			if (wsdlPortType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wsdlPortType");
			}
			return this.ImportWsdlPortType(wsdlPortType, WsdlImporter.WsdlPortTypeImportOptions.ReuseExistingContracts, WsdlImporter.ErrorBehavior.RethrowExceptions);
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x0009DD74 File Offset: 0x0009BF74
		public System.ServiceModel.Channels.Binding ImportBinding(System.Web.Services.Description.Binding wsdlBinding)
		{
			if (this.isFaulted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("WsdlImporterIsFaulted")));
			}
			if (wsdlBinding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wsdlBinding");
			}
			return this.ImportWsdlBinding(wsdlBinding, WsdlImporter.ErrorBehavior.RethrowExceptions).Endpoint.Binding;
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x0009DDC8 File Offset: 0x0009BFC8
		public ServiceEndpoint ImportEndpoint(Port wsdlPort)
		{
			if (this.isFaulted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("WsdlImporterIsFaulted")));
			}
			if (wsdlPort == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wsdlPort");
			}
			return this.ImportWsdlPort(wsdlPort, WsdlImporter.ErrorBehavior.RethrowExceptions);
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x0009DE08 File Offset: 0x0009C008
		public ServiceEndpointCollection ImportEndpoints(PortType wsdlPortType)
		{
			if (this.isFaulted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("WsdlImporterIsFaulted")));
			}
			if (wsdlPortType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wsdlPortType");
			}
			if (this.IsBlackListed(wsdlPortType))
			{
				throw this.CreateAlreadyFaultedException(wsdlPortType);
			}
			this.ImportWsdlPortType(wsdlPortType, WsdlImporter.WsdlPortTypeImportOptions.ReuseExistingContracts, WsdlImporter.ErrorBehavior.RethrowExceptions);
			ServiceEndpointCollection serviceEndpointCollection = new ServiceEndpointCollection();
			foreach (System.Web.Services.Description.Binding binding in this.FindBindingsForPortType(wsdlPortType))
			{
				if (!this.IsBlackListed(binding))
				{
					foreach (ServiceEndpoint item in this.ImportEndpoints(binding))
					{
						serviceEndpointCollection.Add(item);
					}
				}
			}
			return serviceEndpointCollection;
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x0009DEF0 File Offset: 0x0009C0F0
		internal ServiceEndpointCollection ImportEndpoints(ContractDescription contract)
		{
			if (this.isFaulted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("WsdlImporterIsFaulted")));
			}
			if (contract == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contract");
			}
			if (!base.KnownContracts.ContainsKey(WsdlExporter.WsdlNamingHelper.GetPortTypeQName(contract)))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("WsdlImporterContractMustBeInKnownContracts")));
			}
			this.EnsureBeforeImportCalled();
			ServiceEndpointCollection serviceEndpointCollection = new ServiceEndpointCollection();
			foreach (System.Web.Services.Description.Binding binding in this.FindBindingsForContract(contract))
			{
				if (!this.IsBlackListed(binding))
				{
					foreach (ServiceEndpoint item in this.ImportEndpoints(binding))
					{
						serviceEndpointCollection.Add(item);
					}
				}
			}
			return serviceEndpointCollection;
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x0009DFF0 File Offset: 0x0009C1F0
		public ServiceEndpointCollection ImportEndpoints(System.Web.Services.Description.Binding wsdlBinding)
		{
			if (this.isFaulted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("WsdlImporterIsFaulted")));
			}
			if (wsdlBinding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wsdlBinding");
			}
			if (this.IsBlackListed(wsdlBinding))
			{
				throw this.CreateAlreadyFaultedException(wsdlBinding);
			}
			this.ImportWsdlBinding(wsdlBinding, WsdlImporter.ErrorBehavior.RethrowExceptions);
			ServiceEndpointCollection serviceEndpointCollection = new ServiceEndpointCollection();
			foreach (Port port in this.FindPortsForBinding(wsdlBinding))
			{
				if (!this.IsBlackListed(port))
				{
					ServiceEndpoint serviceEndpoint = this.ImportWsdlPort(port, WsdlImporter.ErrorBehavior.DoNotThrowExceptions);
					if (serviceEndpoint != null)
					{
						serviceEndpointCollection.Add(serviceEndpoint);
					}
				}
			}
			return serviceEndpointCollection;
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x0009E0AC File Offset: 0x0009C2AC
		public ServiceEndpointCollection ImportEndpoints(Service wsdlService)
		{
			if (this.isFaulted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("WsdlImporterIsFaulted")));
			}
			if (wsdlService == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wsdlService");
			}
			this.EnsureBeforeImportCalled();
			ServiceEndpointCollection serviceEndpointCollection = new ServiceEndpointCollection();
			foreach (object obj in wsdlService.Ports)
			{
				Port port = (Port)obj;
				if (!this.IsBlackListed(port))
				{
					ServiceEndpoint serviceEndpoint = this.ImportWsdlPort(port, WsdlImporter.ErrorBehavior.DoNotThrowExceptions);
					if (serviceEndpoint != null)
					{
						serviceEndpointCollection.Add(serviceEndpoint);
					}
				}
			}
			return serviceEndpointCollection;
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x0009E160 File Offset: 0x0009C360
		private bool IsBlackListed(NamedItem item)
		{
			return this.importErrors.ContainsKey(item);
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x0009E170 File Offset: 0x0009C370
		private ContractDescription ImportWsdlPortType(PortType wsdlPortType, WsdlImporter.WsdlPortTypeImportOptions importOptions, WsdlImporter.ErrorBehavior errorBehavior)
		{
			if (this.IsBlackListed(wsdlPortType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateAlreadyFaultedException(wsdlPortType));
			}
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(wsdlPortType.Name, wsdlPortType.ServiceDescription.TargetNamespace);
			ContractDescription contractDescription = null;
			if (importOptions == WsdlImporter.WsdlPortTypeImportOptions.IgnoreExistingContracts || !this.TryFindExistingContract(xmlQualifiedName, out contractDescription))
			{
				this.EnsureBeforeImportCalled();
				try
				{
					contractDescription = this.CreateContractDescription(wsdlPortType, xmlQualifiedName);
					WsdlContractConversionContext wsdlContractConversionContext = new WsdlContractConversionContext(contractDescription, wsdlPortType);
					foreach (object obj in wsdlPortType.Operations)
					{
						Operation operation = (Operation)obj;
						OperationDescription operationDescription = this.CreateOperationDescription(wsdlPortType, operation, contractDescription);
						wsdlContractConversionContext.AddOperation(operationDescription, operation);
						foreach (object obj2 in operation.Messages)
						{
							OperationMessage wsdlOperationMessage = (OperationMessage)obj2;
							MessageDescription messageDescription;
							if (WsdlImporter.TryCreateMessageDescription(wsdlOperationMessage, operationDescription, out messageDescription))
							{
								wsdlContractConversionContext.AddMessage(messageDescription, wsdlOperationMessage);
							}
						}
						foreach (object obj3 in operation.Faults)
						{
							OperationFault wsdlOperationFault = (OperationFault)obj3;
							FaultDescription faultDescription;
							if (WsdlImporter.TryCreateFaultDescription(wsdlOperationFault, operationDescription, out faultDescription))
							{
								wsdlContractConversionContext.AddFault(faultDescription, wsdlOperationFault);
							}
						}
					}
					this.CallImportContract(wsdlContractConversionContext);
					this.VerifyImportedWsdlPortType(wsdlPortType);
					this.importedPortTypes.Add(xmlQualifiedName, wsdlContractConversionContext);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					WsdlImporter.WsdlImportException ex2 = WsdlImporter.WsdlImportException.Create(wsdlPortType, ex);
					this.LogImportError(wsdlPortType, ex2);
					if (errorBehavior == WsdlImporter.ErrorBehavior.RethrowExceptions)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex2);
					}
					return null;
				}
				return contractDescription;
			}
			return contractDescription;
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x0009E394 File Offset: 0x0009C594
		private WsdlEndpointConversionContext ImportWsdlBinding(System.Web.Services.Description.Binding wsdlBinding, WsdlImporter.ErrorBehavior errorBehavior)
		{
			if (this.IsBlackListed(wsdlBinding))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateAlreadyFaultedException(wsdlBinding));
			}
			XmlQualifiedName key = new XmlQualifiedName(wsdlBinding.Name, wsdlBinding.ServiceDescription.TargetNamespace);
			WsdlEndpointConversionContext wsdlEndpointConversionContext = null;
			if (!this.importedBindings.TryGetValue(key, out wsdlEndpointConversionContext))
			{
				this.EnsureBeforeImportCalled();
				try
				{
					bool flag;
					ContractDescription orImportContractDescription = this.GetOrImportContractDescription(wsdlBinding.Type, out flag);
					WsdlContractConversionContext contractContext = null;
					this.importedPortTypes.TryGetValue(wsdlBinding.Type, out contractContext);
					ServiceEndpoint serviceEndpoint = new ServiceEndpoint(orImportContractDescription);
					wsdlEndpointConversionContext = new WsdlEndpointConversionContext(contractContext, serviceEndpoint, wsdlBinding, null);
					foreach (object obj in wsdlBinding.Operations)
					{
						OperationBinding operationBinding = (OperationBinding)obj;
						try
						{
							OperationDescription operationDescription = WsdlImporter.Binding2DescriptionHelper.FindOperationDescription(operationBinding, this.wsdlDocuments, wsdlEndpointConversionContext);
							wsdlEndpointConversionContext.AddOperationBinding(operationDescription, operationBinding);
							for (int i = 0; i < operationDescription.Messages.Count; i++)
							{
								MessageDescription messageDescription = operationDescription.Messages[i];
								MessageBinding wsdlMessageBinding = WsdlImporter.Binding2DescriptionHelper.FindMessageBinding(operationBinding, messageDescription);
								wsdlEndpointConversionContext.AddMessageBinding(messageDescription, wsdlMessageBinding);
							}
							foreach (FaultDescription faultDescription in operationDescription.Faults)
							{
								FaultBinding faultBinding = WsdlImporter.Binding2DescriptionHelper.FindFaultBinding(operationBinding, faultDescription);
								if (faultBinding != null)
								{
									wsdlEndpointConversionContext.AddFaultBinding(faultDescription, faultBinding);
								}
							}
						}
						catch (Exception ex)
						{
							if (Fx.IsFatal(ex))
							{
								throw;
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsdlImporter.WsdlImportException.Create(operationBinding, ex));
						}
					}
					XmlQualifiedName bindingName = WsdlImporter.WsdlNamingHelper.GetBindingName(wsdlBinding);
					serviceEndpoint.Binding = this.CreateBinding(wsdlEndpointConversionContext, bindingName);
					this.CallImportEndpoint(wsdlEndpointConversionContext);
					this.VerifyImportedWsdlBinding(wsdlBinding);
					this.importedBindings.Add(key, wsdlEndpointConversionContext);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					WsdlImporter.WsdlImportException ex3 = WsdlImporter.WsdlImportException.Create(wsdlBinding, ex2);
					this.LogImportError(wsdlBinding, ex3);
					if (errorBehavior == WsdlImporter.ErrorBehavior.RethrowExceptions)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex3);
					}
					return null;
				}
				return wsdlEndpointConversionContext;
			}
			return wsdlEndpointConversionContext;
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x0009E604 File Offset: 0x0009C804
		private ServiceEndpoint ImportWsdlPort(Port wsdlPort, WsdlImporter.ErrorBehavior errorBehavior)
		{
			if (this.IsBlackListed(wsdlPort))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateAlreadyFaultedException(wsdlPort));
			}
			ServiceEndpoint serviceEndpoint = null;
			if (!this.importedPorts.TryGetValue(wsdlPort, out serviceEndpoint))
			{
				this.EnsureBeforeImportCalled();
				try
				{
					System.Web.Services.Description.Binding binding = this.wsdlDocuments.GetBinding(wsdlPort.Binding);
					WsdlEndpointConversionContext wsdlEndpointConversionContext = this.ImportWsdlBinding(binding, WsdlImporter.ErrorBehavior.RethrowExceptions);
					serviceEndpoint = new ServiceEndpoint(wsdlEndpointConversionContext.Endpoint.Contract);
					serviceEndpoint.Name = WsdlImporter.WsdlNamingHelper.GetEndpointName(wsdlPort).EncodedName;
					WsdlEndpointConversionContext wsdlEndpointConversionContext2 = new WsdlEndpointConversionContext(wsdlEndpointConversionContext, serviceEndpoint, wsdlPort);
					if (WsdlImporter.WsdlPolicyReader.HasPolicy(wsdlPort))
					{
						XmlQualifiedName bindingName = WsdlImporter.WsdlNamingHelper.GetBindingName(wsdlPort);
						serviceEndpoint.Binding = this.CreateBinding(wsdlEndpointConversionContext2, bindingName);
					}
					else
					{
						serviceEndpoint.Binding = wsdlEndpointConversionContext.Endpoint.Binding;
					}
					this.CallImportEndpoint(wsdlEndpointConversionContext2);
					this.VerifyImportedWsdlPort(wsdlPort);
					this.importedPorts.Add(wsdlPort, serviceEndpoint);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					WsdlImporter.WsdlImportException ex2 = WsdlImporter.WsdlImportException.Create(wsdlPort, ex);
					this.LogImportError(wsdlPort, ex2);
					if (errorBehavior == WsdlImporter.ErrorBehavior.RethrowExceptions)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex2);
					}
					return null;
				}
				return serviceEndpoint;
			}
			return serviceEndpoint;
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x0009E728 File Offset: 0x0009C928
		private static bool TryCreateMessageDescription(OperationMessage wsdlOperationMessage, OperationDescription operationDescription, out MessageDescription messageDescription)
		{
			string wsaActionUri = WsdlImporter.WSAddressingHelper.GetWsaActionUri(wsdlOperationMessage);
			MessageDirection direction;
			if (wsdlOperationMessage is OperationInput)
			{
				direction = MessageDirection.Input;
			}
			else
			{
				if (!(wsdlOperationMessage is OperationOutput))
				{
					messageDescription = null;
					return false;
				}
				direction = MessageDirection.Output;
			}
			messageDescription = new MessageDescription(wsaActionUri, direction);
			messageDescription.MessageName = WsdlImporter.WsdlNamingHelper.GetOperationMessageName(wsdlOperationMessage);
			messageDescription.XsdTypeName = wsdlOperationMessage.Message;
			operationDescription.Messages.Add(messageDescription);
			return true;
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x0009E78C File Offset: 0x0009C98C
		private static bool TryCreateFaultDescription(OperationFault wsdlOperationFault, OperationDescription operationDescription, out FaultDescription faultDescription)
		{
			if (string.IsNullOrEmpty(wsdlOperationFault.Name))
			{
				faultDescription = null;
				return false;
			}
			string wsaActionUri = WsdlImporter.WSAddressingHelper.GetWsaActionUri(wsdlOperationFault);
			faultDescription = new FaultDescription(wsaActionUri);
			faultDescription.SetNameOnly(new XmlName(wsdlOperationFault.Name, true));
			operationDescription.Faults.Add(faultDescription);
			return true;
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x0009E7DC File Offset: 0x0009C9DC
		private ContractDescription CreateContractDescription(PortType wsdlPortType, XmlQualifiedName wsdlPortTypeQName)
		{
			XmlQualifiedName contractName = WsdlImporter.WsdlNamingHelper.GetContractName(wsdlPortTypeQName);
			ContractDescription contractDescription = new ContractDescription(contractName.Name, contractName.Namespace);
			WsdlImporter.NetSessionHelper.SetSession(contractDescription, wsdlPortType);
			return contractDescription;
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x0009E80C File Offset: 0x0009CA0C
		private OperationDescription CreateOperationDescription(PortType wsdlPortType, Operation wsdlOperation, ContractDescription contract)
		{
			string operationName = WsdlImporter.WsdlNamingHelper.GetOperationName(wsdlOperation);
			OperationDescription operationDescription = new OperationDescription(operationName, contract);
			WsdlImporter.NetSessionHelper.SetInitiatingTerminating(operationDescription, wsdlOperation);
			contract.Operations.Add(operationDescription);
			return operationDescription;
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x0009E83C File Offset: 0x0009CA3C
		private System.ServiceModel.Channels.Binding CreateBinding(WsdlEndpointConversionContext endpointContext, XmlQualifiedName bindingQName)
		{
			System.ServiceModel.Channels.Binding result;
			try
			{
				BindingElementCollection bindingElements = this.ImportPolicyFromWsdl(endpointContext);
				result = new CustomBinding(bindingElements)
				{
					Name = NamingHelper.CodeName(bindingQName.Name),
					Namespace = bindingQName.Namespace
				};
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsdlImporter.WsdlImportException.Create(endpointContext.WsdlBinding, ex));
			}
			return result;
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x0009E8AC File Offset: 0x0009CAAC
		private ContractDescription GetOrImportContractDescription(XmlQualifiedName wsdlPortTypeQName, out bool wasExistingContractDescription)
		{
			ContractDescription result;
			if (!this.TryFindExistingContract(wsdlPortTypeQName, out result))
			{
				PortType portType = this.wsdlDocuments.GetPortType(wsdlPortTypeQName);
				result = this.ImportWsdlPortType(portType, WsdlImporter.WsdlPortTypeImportOptions.IgnoreExistingContracts, WsdlImporter.ErrorBehavior.RethrowExceptions);
				wasExistingContractDescription = false;
			}
			wasExistingContractDescription = true;
			return result;
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x0009E8E4 File Offset: 0x0009CAE4
		private void ProcessMetadataDocuments(IEnumerable<MetadataSection> metadataSections)
		{
			foreach (MetadataSection metadataSection in metadataSections)
			{
				try
				{
					if (!(metadataSection.Metadata is MetadataReference) && !(metadataSection.Metadata is MetadataLocation))
					{
						if (metadataSection.Dialect == MetadataSection.ServiceDescriptionDialect)
						{
							this.wsdlDocuments.Add(this.TryConvert<ServiceDescription>(metadataSection));
						}
						if (metadataSection.Dialect == MetadataSection.XmlSchemaDialect)
						{
							this.xmlSchemas.Add(this.TryConvert<XmlSchema>(metadataSection));
						}
						if (metadataSection.Dialect == MetadataSection.PolicyDialect)
						{
							if (string.IsNullOrEmpty(metadataSection.Identifier))
							{
								this.LogImportWarning(SR.GetString("PolicyDocumentMustHaveIdentifier"));
							}
							else
							{
								this.policyDocuments.Add(metadataSection.Identifier, this.TryConvert<XmlElement>(metadataSection));
							}
						}
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(metadataSection.Identifier, ex));
				}
			}
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x0009EA10 File Offset: 0x0009CC10
		private T TryConvert<T>(MetadataSection doc)
		{
			T result;
			try
			{
				result = (T)((object)doc.Metadata);
			}
			catch (InvalidCastException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SFxBadMetadataDialect", new object[]
				{
					doc.Identifier,
					doc.Dialect,
					typeof(T).FullName,
					doc.GetType().FullName
				})));
			}
			return result;
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x0009EA90 File Offset: 0x0009CC90
		private bool TryFindExistingContract(XmlQualifiedName wsdlPortTypeQName, out ContractDescription existingContract)
		{
			XmlQualifiedName contractName = WsdlImporter.WsdlNamingHelper.GetContractName(wsdlPortTypeQName);
			if (base.KnownContracts.TryGetValue(contractName, out existingContract))
			{
				return true;
			}
			WsdlContractConversionContext wsdlContractConversionContext;
			if (this.importedPortTypes.TryGetValue(wsdlPortTypeQName, out wsdlContractConversionContext))
			{
				existingContract = wsdlContractConversionContext.Contract;
				return true;
			}
			return false;
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x0009EAD0 File Offset: 0x0009CCD0
		private void EnsureBeforeImportCalled()
		{
			if (!this.beforeImportCalled)
			{
				foreach (IWsdlImportExtension wsdlImportExtension in this.wsdlExtensions)
				{
					try
					{
						wsdlImportExtension.BeforeImport(this.wsdlDocuments, this.xmlSchemas, this.policyDocuments.Values);
					}
					catch (Exception ex)
					{
						this.isFaulted = true;
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsdlImporter.CreateBeforeImportExtensionException(wsdlImportExtension, ex));
					}
				}
				this.beforeImportCalled = true;
			}
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x0009EB74 File Offset: 0x0009CD74
		private void CallImportContract(WsdlContractConversionContext contractConversionContext)
		{
			foreach (IWsdlImportExtension wsdlImportExtension in this.wsdlExtensions)
			{
				try
				{
					wsdlImportExtension.ImportContract(this, contractConversionContext);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsdlImporter.CreateExtensionException(wsdlImportExtension, ex));
				}
			}
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x0009EBF0 File Offset: 0x0009CDF0
		private void CallImportEndpoint(WsdlEndpointConversionContext endpointConversionContext)
		{
			foreach (IWsdlImportExtension wsdlImportExtension in this.wsdlExtensions)
			{
				try
				{
					wsdlImportExtension.ImportEndpoint(this, endpointConversionContext);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsdlImporter.CreateExtensionException(wsdlImportExtension, ex));
				}
			}
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x0009EC6C File Offset: 0x0009CE6C
		private void VerifyImportedWsdlPortType(PortType wsdlPortType)
		{
			this.VerifyImportedExtensions(wsdlPortType);
			foreach (object obj in wsdlPortType.Operations)
			{
				Operation operation = (Operation)obj;
				this.VerifyImportedExtensions(operation);
				foreach (object obj2 in operation.Messages)
				{
					OperationMessage item = (OperationMessage)obj2;
					this.VerifyImportedExtensions(item);
				}
				foreach (object obj3 in operation.Faults)
				{
					OperationMessage item2 = (OperationMessage)obj3;
					this.VerifyImportedExtensions(item2);
				}
			}
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x0009ED74 File Offset: 0x0009CF74
		private void VerifyImportedWsdlBinding(System.Web.Services.Description.Binding wsdlBinding)
		{
			this.VerifyImportedExtensions(wsdlBinding);
			foreach (object obj in wsdlBinding.Operations)
			{
				OperationBinding operationBinding = (OperationBinding)obj;
				this.VerifyImportedExtensions(operationBinding);
				if (operationBinding.Input != null)
				{
					this.VerifyImportedExtensions(operationBinding.Input);
				}
				if (operationBinding.Output != null)
				{
					this.VerifyImportedExtensions(operationBinding.Output);
				}
				foreach (object obj2 in operationBinding.Faults)
				{
					MessageBinding item = (MessageBinding)obj2;
					this.VerifyImportedExtensions(item);
				}
			}
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x0009EE50 File Offset: 0x0009D050
		private void VerifyImportedWsdlPort(Port wsdlPort)
		{
			this.VerifyImportedExtensions(wsdlPort);
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x0009EE5C File Offset: 0x0009D05C
		private void VerifyImportedExtensions(NamedItem item)
		{
			foreach (object obj in item.Extensions)
			{
				if (!item.Extensions.IsHandled(obj))
				{
					XmlQualifiedName unhandledExtensionQName = this.GetUnhandledExtensionQName(obj, item);
					if (item.Extensions.IsRequired(obj) || WsdlImporter.IsNonSoapWsdl11BindingExtension(obj))
					{
						string @string = SR.GetString("RequiredWSDLExtensionIgnored", new object[]
						{
							unhandledExtensionQName.Name,
							unhandledExtensionQName.Namespace
						});
						WsdlImporter.WsdlImportException exception = WsdlImporter.WsdlImportException.Create(item, new InvalidOperationException(@string));
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
					}
					string text = WsdlImporter.CreateXPathString(item);
					string string2 = SR.GetString("OptionalWSDLExtensionIgnored", new object[]
					{
						unhandledExtensionQName.Name,
						unhandledExtensionQName.Namespace,
						text
					});
					base.Errors.Add(new MetadataConversionError(string2, true));
				}
			}
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x0009EF64 File Offset: 0x0009D164
		private static bool IsNonSoapWsdl11BindingExtension(object ext)
		{
			return ext is HttpAddressBinding || ext is HttpBinding || ext is HttpOperationBinding || ext is HttpUrlEncodedBinding || ext is HttpUrlReplacementBinding || ext is MimeContentBinding || ext is MimeMultipartRelatedBinding || ext is MimePart || ext is MimeTextBinding || ext is MimeXmlBinding;
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x0009EFC4 File Offset: 0x0009D1C4
		private XmlQualifiedName GetUnhandledExtensionQName(object extension, NamedItem item)
		{
			XmlElement xmlElement = extension as XmlElement;
			if (xmlElement != null)
			{
				return new XmlQualifiedName(xmlElement.LocalName, xmlElement.NamespaceURI);
			}
			if (extension is ServiceDescriptionFormatExtension)
			{
				XmlFormatExtensionAttribute[] array = (XmlFormatExtensionAttribute[])ServiceReflector.GetCustomAttributes(extension.GetType(), typeof(XmlFormatExtensionAttribute), false);
				if (array.Length != 0)
				{
					return new XmlQualifiedName(array[0].ElementName, array[0].Namespace);
				}
			}
			WsdlImporter.WsdlImportException exception = WsdlImporter.WsdlImportException.Create(item, new InvalidOperationException(SR.GetString("UnknownWSDLExtensionIgnored", new object[]
			{
				extension.GetType().AssemblyQualifiedName
			})));
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x0009F060 File Offset: 0x0009D260
		private IEnumerable<System.Web.Services.Description.Binding> FindBindingsForPortType(PortType wsdlPortType)
		{
			foreach (System.Web.Services.Description.Binding binding in this.GetAllBindings())
			{
				if (binding.Type.Name == wsdlPortType.Name && binding.Type.Namespace == wsdlPortType.ServiceDescription.TargetNamespace)
				{
					yield return binding;
				}
			}
			IEnumerator<System.Web.Services.Description.Binding> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x0009F077 File Offset: 0x0009D277
		private IEnumerable<System.Web.Services.Description.Binding> FindBindingsForContract(ContractDescription contract)
		{
			XmlQualifiedName qName = WsdlExporter.WsdlNamingHelper.GetPortTypeQName(contract);
			foreach (System.Web.Services.Description.Binding binding in this.GetAllBindings())
			{
				if (binding.Type.Name == qName.Name && binding.Type.Namespace == qName.Namespace)
				{
					yield return binding;
				}
			}
			IEnumerator<System.Web.Services.Description.Binding> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x0009F08E File Offset: 0x0009D28E
		private IEnumerable<Port> FindPortsForBinding(System.Web.Services.Description.Binding binding)
		{
			foreach (Port port in this.GetAllPorts())
			{
				if (port.Binding.Name == binding.Name && port.Binding.Namespace == binding.ServiceDescription.TargetNamespace)
				{
					yield return port;
				}
			}
			IEnumerator<Port> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x0009F0A5 File Offset: 0x0009D2A5
		private IEnumerable<System.Web.Services.Description.Binding> GetAllBindings()
		{
			foreach (object obj in this.WsdlDocuments)
			{
				ServiceDescription serviceDescription = (ServiceDescription)obj;
				foreach (object obj2 in serviceDescription.Bindings)
				{
					System.Web.Services.Description.Binding binding = (System.Web.Services.Description.Binding)obj2;
					yield return binding;
				}
				IEnumerator enumerator2 = null;
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x0009F0B5 File Offset: 0x0009D2B5
		private IEnumerable<Port> GetAllPorts()
		{
			foreach (object obj in this.WsdlDocuments)
			{
				ServiceDescription serviceDescription = (ServiceDescription)obj;
				foreach (object obj2 in serviceDescription.Services)
				{
					Service service = (Service)obj2;
					foreach (object obj3 in service.Ports)
					{
						Port port = (Port)obj3;
						yield return port;
					}
					IEnumerator enumerator3 = null;
				}
				IEnumerator enumerator2 = null;
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x0009F0C5 File Offset: 0x0009D2C5
		[SecuritySafeCritical]
		private static Collection<IWsdlImportExtension> LoadWsdlExtensionsFromConfig()
		{
			return ClientSection.UnsafeGetSection().Metadata.LoadWsdlImportExtensions();
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x0009F0D6 File Offset: 0x0009D2D6
		internal static IEnumerable<MetadataSection> CreateMetadataDocuments(ServiceDescriptionCollection wsdlDocuments, XmlSchemaSet xmlSchemas, IEnumerable<XmlElement> policyDocuments)
		{
			if (wsdlDocuments != null)
			{
				foreach (object obj in wsdlDocuments)
				{
					ServiceDescription serviceDescription = (ServiceDescription)obj;
					yield return MetadataSection.CreateFromServiceDescription(serviceDescription);
				}
				IEnumerator enumerator = null;
			}
			if (xmlSchemas != null)
			{
				foreach (object obj2 in xmlSchemas.Schemas())
				{
					XmlSchema schema = (XmlSchema)obj2;
					yield return MetadataSection.CreateFromSchema(schema);
				}
				IEnumerator enumerator = null;
			}
			if (policyDocuments != null)
			{
				foreach (XmlElement policy in policyDocuments)
				{
					yield return MetadataSection.CreateFromPolicy(policy, null);
				}
				IEnumerator<XmlElement> enumerator2 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x0009F0F4 File Offset: 0x0009D2F4
		private BindingElementCollection ImportPolicyFromWsdl(WsdlEndpointConversionContext endpointContext)
		{
			MetadataImporter.PolicyAlternatives policyAlternatives = this.PolicyReader.GetPolicyAlternatives(endpointContext);
			IEnumerable<PolicyConversionContext> policyConversionContextEnumerator = MetadataImporter.GetPolicyConversionContextEnumerator(endpointContext.Endpoint, policyAlternatives, this.Quotas);
			PolicyConversionContext policyConversionContext = null;
			StringBuilder stringBuilder = null;
			int num = 0;
			foreach (PolicyConversionContext policyConversionContext2 in policyConversionContextEnumerator)
			{
				if (policyConversionContext == null)
				{
					policyConversionContext = policyConversionContext2;
				}
				if (base.TryImportPolicy(policyConversionContext2))
				{
					return policyConversionContext2.BindingElements;
				}
				WsdlImporter.AppendUnImportedPolicyErrorMessage(ref stringBuilder, endpointContext, policyConversionContext2);
				if (++num >= this.Quotas.MaxPolicyConversionContexts)
				{
					break;
				}
			}
			if (policyConversionContext != null)
			{
				policyConversionContext.BindingElements.Insert(0, WsdlImporter.CollectUnrecognizedAssertions(policyConversionContext, endpointContext));
				this.LogImportWarning(stringBuilder.ToString());
				return policyConversionContext.BindingElements;
			}
			if (endpointContext.WsdlPort != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsdlImporter.WsdlImportException.Create(endpointContext.WsdlPort, new InvalidOperationException(SR.GetString("NoUsablePolicyAssertions"))));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsdlImporter.WsdlImportException.Create(endpointContext.WsdlBinding, new InvalidOperationException(SR.GetString("NoUsablePolicyAssertions"))));
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x0009F220 File Offset: 0x0009D420
		private static UnrecognizedAssertionsBindingElement CollectUnrecognizedAssertions(PolicyConversionContext policyContext, WsdlEndpointConversionContext endpointContext)
		{
			XmlQualifiedName wsdlBinding = new XmlQualifiedName(endpointContext.WsdlBinding.Name, endpointContext.WsdlBinding.ServiceDescription.TargetNamespace);
			UnrecognizedAssertionsBindingElement unrecognizedAssertionsBindingElement = new UnrecognizedAssertionsBindingElement(wsdlBinding, policyContext.GetBindingAssertions());
			foreach (OperationDescription operationDescription in policyContext.Contract.Operations)
			{
				if (policyContext.GetOperationBindingAssertions(operationDescription).Count != 0)
				{
					unrecognizedAssertionsBindingElement.Add(operationDescription, policyContext.GetOperationBindingAssertions(operationDescription));
				}
				foreach (MessageDescription message in operationDescription.Messages)
				{
					if (policyContext.GetMessageBindingAssertions(message).Count != 0)
					{
						unrecognizedAssertionsBindingElement.Add(message, policyContext.GetMessageBindingAssertions(message));
					}
				}
			}
			return unrecognizedAssertionsBindingElement;
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x0009F310 File Offset: 0x0009D510
		private static void AppendUnImportedPolicyErrorMessage(ref StringBuilder unImportedPolicyMessage, WsdlEndpointConversionContext endpointContext, PolicyConversionContext policyContext)
		{
			if (unImportedPolicyMessage == null)
			{
				unImportedPolicyMessage = new StringBuilder(SR.GetString("UnabletoImportPolicy"));
			}
			else
			{
				unImportedPolicyMessage.AppendLine();
			}
			if (policyContext.GetBindingAssertions().Count != 0)
			{
				WsdlImporter.AddUnImportedPolicyString(unImportedPolicyMessage, endpointContext.WsdlBinding, policyContext.GetBindingAssertions());
			}
			foreach (OperationDescription operationDescription in policyContext.Contract.Operations)
			{
				if (policyContext.GetOperationBindingAssertions(operationDescription).Count != 0)
				{
					WsdlImporter.AddUnImportedPolicyString(unImportedPolicyMessage, endpointContext.GetOperationBinding(operationDescription), policyContext.GetOperationBindingAssertions(operationDescription));
				}
				foreach (MessageDescription message in operationDescription.Messages)
				{
					if (policyContext.GetMessageBindingAssertions(message).Count != 0)
					{
						WsdlImporter.AddUnImportedPolicyString(unImportedPolicyMessage, endpointContext.GetMessageBinding(message), policyContext.GetMessageBindingAssertions(message));
					}
				}
			}
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x0009F418 File Offset: 0x0009D618
		private static void AddUnImportedPolicyString(StringBuilder stringBuilder, NamedItem item, IEnumerable<XmlElement> unimportdPolicy)
		{
			stringBuilder.AppendLine(SR.GetString("UnImportedAssertionList", new object[]
			{
				WsdlImporter.CreateXPathString(item)
			}));
			Dictionary<XmlElement, XmlElement> dictionary = new Dictionary<XmlElement, XmlElement>();
			int num = 0;
			foreach (XmlElement xmlElement in unimportdPolicy)
			{
				if (!dictionary.ContainsKey(xmlElement))
				{
					dictionary.Add(xmlElement, xmlElement);
					num++;
					if (num > 128)
					{
						stringBuilder.Append("..");
						stringBuilder.AppendLine();
						break;
					}
					WsdlImporter.WriteElement(xmlElement, stringBuilder);
				}
			}
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x0009F4BC File Offset: 0x0009D6BC
		private static void WriteElement(XmlElement element, StringBuilder stringBuilder)
		{
			stringBuilder.Append("    <");
			stringBuilder.Append(element.Name);
			if (!string.IsNullOrEmpty(element.NamespaceURI))
			{
				stringBuilder.Append(' ');
				stringBuilder.Append("xmlns");
				if (!string.IsNullOrEmpty(element.Prefix))
				{
					stringBuilder.Append(':');
					stringBuilder.Append(element.Prefix);
				}
				stringBuilder.Append('=');
				stringBuilder.Append('\'');
				stringBuilder.Append(element.NamespaceURI);
				stringBuilder.Append('\'');
			}
			stringBuilder.Append(">..</");
			stringBuilder.Append(element.Name);
			stringBuilder.Append('>');
			stringBuilder.AppendLine();
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x0009F578 File Offset: 0x0009D778
		private static string GetElementName(NamedItem item)
		{
			if (item is PortType)
			{
				return "wsdl:portType";
			}
			if (item is System.Web.Services.Description.Binding)
			{
				return "wsdl:binding";
			}
			if (item is ServiceDescription)
			{
				return "wsdl:definitions";
			}
			if (item is Service)
			{
				return "wsdl:service";
			}
			if (item is System.Web.Services.Description.Message)
			{
				return "wsdl:message";
			}
			if (item is Operation)
			{
				return "wsdl:operation";
			}
			if (item is Port)
			{
				return "wsdl:port";
			}
			return null;
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x0009F5E8 File Offset: 0x0009D7E8
		private static string CreateXPathString(NamedItem item)
		{
			if (item == null)
			{
				return SR.GetString("XPathUnavailable");
			}
			string name = item.Name;
			string empty = string.Empty;
			string str = string.Empty;
			string text;
			string text2;
			WsdlImporter.GetXPathParameters(item, out text, out text2, ref name, ref empty);
			string str2 = string.Format(CultureInfo.InvariantCulture, "//wsdl:definitions[@targetNamespace='{0}']", new object[]
			{
				text
			});
			if (text2 != null)
			{
				str = string.Format(CultureInfo.InvariantCulture, "/wsdl:{0}[@name='{1}']", new object[]
				{
					text2,
					name
				});
			}
			return str2 + str + empty;
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x0009F66C File Offset: 0x0009D86C
		private static void GetXPathParameters(NamedItem item, out string wsdlNs, out string localName, ref string nameValue, ref string rest)
		{
			if (item is ServiceDescription)
			{
				localName = null;
				wsdlNs = (((ServiceDescription)item).TargetNamespace ?? string.Empty);
			}
			if (item is PortType)
			{
				localName = "portType";
				wsdlNs = (((PortType)item).ServiceDescription.TargetNamespace ?? string.Empty);
				return;
			}
			if (item is System.Web.Services.Description.Binding)
			{
				localName = "binding";
				wsdlNs = (((System.Web.Services.Description.Binding)item).ServiceDescription.TargetNamespace ?? string.Empty);
				return;
			}
			if (item is ServiceDescription)
			{
				localName = "definitions";
				wsdlNs = (((ServiceDescription)item).TargetNamespace ?? string.Empty);
				return;
			}
			if (item is Service)
			{
				localName = "service";
				wsdlNs = (((Service)item).ServiceDescription.TargetNamespace ?? string.Empty);
				return;
			}
			if (item is System.Web.Services.Description.Message)
			{
				localName = "message";
				wsdlNs = (((System.Web.Services.Description.Message)item).ServiceDescription.TargetNamespace ?? string.Empty);
				return;
			}
			if (item is Port)
			{
				Service service = ((Port)item).Service;
				localName = "service";
				nameValue = service.Name;
				wsdlNs = (service.ServiceDescription.TargetNamespace ?? string.Empty);
				rest = string.Format(CultureInfo.InvariantCulture, "/wsdl:{0}[@name='{1}']", new object[]
				{
					"port",
					item.Name
				});
				return;
			}
			if (item is Operation)
			{
				PortType portType = ((Operation)item).PortType;
				localName = "portType";
				nameValue = portType.Name;
				wsdlNs = (portType.ServiceDescription.TargetNamespace ?? string.Empty);
				rest = string.Format(CultureInfo.InvariantCulture, "/wsdl:{0}[@name='{1}']", new object[]
				{
					"operation",
					item.Name
				});
				return;
			}
			if (item is OperationBinding)
			{
				OperationBinding operationBinding = (OperationBinding)item;
				localName = "binding";
				nameValue = operationBinding.Binding.Name;
				wsdlNs = (operationBinding.Binding.ServiceDescription.TargetNamespace ?? string.Empty);
				rest = string.Format(CultureInfo.InvariantCulture, "/wsdl:{0}[@name='{1}']", new object[]
				{
					"operation",
					item.Name
				});
				return;
			}
			if (!(item is MessageBinding))
			{
				localName = null;
				wsdlNs = null;
				return;
			}
			localName = "binding";
			OperationBinding operationBinding2 = ((MessageBinding)item).OperationBinding;
			wsdlNs = (operationBinding2.Binding.ServiceDescription.TargetNamespace ?? string.Empty);
			nameValue = operationBinding2.Binding.Name;
			string name = item.Name;
			string text = string.Empty;
			if (item is InputBinding)
			{
				text = "input";
			}
			else if (item is OutputBinding)
			{
				text = "output";
			}
			else if (item is FaultBinding)
			{
				text = "fault";
			}
			rest = string.Format(CultureInfo.InvariantCulture, "/wsdl:{0}[@name='{1}']", new object[]
			{
				"operation",
				operationBinding2.Name
			});
			if (string.IsNullOrEmpty(name))
			{
				rest += string.Format(CultureInfo.InvariantCulture, "/wsdl:{0}", new object[]
				{
					text
				});
				return;
			}
			rest += string.Format(CultureInfo.InvariantCulture, "/wsdl:{0}[@name='{1}']", new object[]
			{
				text,
				name
			});
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x0009F9AC File Offset: 0x0009DBAC
		private void LogImportWarning(string warningMessage)
		{
			if (this.warnings.ContainsKey(warningMessage))
			{
				return;
			}
			if (this.warnings.Count >= 1024)
			{
				this.warnings.Clear();
			}
			this.warnings.Add(warningMessage, warningMessage);
			base.Errors.Add(new MetadataConversionError(warningMessage, true));
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x0009FA04 File Offset: 0x0009DC04
		private void LogImportError(NamedItem item, WsdlImporter.WsdlImportException wie)
		{
			string string2;
			if (wie.InnerException != null && wie.InnerException is WsdlImporter.WsdlImportException)
			{
				WsdlImporter.WsdlImportException ex = wie.InnerException as WsdlImporter.WsdlImportException;
				string @string = SR.GetString("WsdlImportErrorDependencyDetail", new object[]
				{
					WsdlImporter.GetElementName(ex.SourceItem),
					WsdlImporter.GetElementName(item),
					WsdlImporter.CreateXPathString(ex.SourceItem)
				});
				string2 = SR.GetString("WsdlImportErrorMessageDetail", new object[]
				{
					WsdlImporter.GetElementName(item),
					WsdlImporter.CreateXPathString(wie.SourceItem),
					@string
				});
			}
			else
			{
				string2 = SR.GetString("WsdlImportErrorMessageDetail", new object[]
				{
					WsdlImporter.GetElementName(item),
					WsdlImporter.CreateXPathString(wie.SourceItem),
					wie.Message
				});
			}
			this.importErrors.Add(item, wie);
			base.Errors.Add(new MetadataConversionError(string2, false));
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x0009FAE8 File Offset: 0x0009DCE8
		private static Exception CreateBeforeImportExtensionException(IWsdlImportExtension importer, Exception e)
		{
			string @string = SR.GetString("WsdlExtensionBeforeImportError", new object[]
			{
				importer.GetType().AssemblyQualifiedName,
				e.Message
			});
			return new InvalidOperationException(@string, e);
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x0009FB24 File Offset: 0x0009DD24
		private Exception CreateAlreadyFaultedException(NamedItem item)
		{
			WsdlImporter.WsdlImportException innerException = this.importErrors[item];
			string @string = SR.GetString("WsdlItemAlreadyFaulted", new object[]
			{
				WsdlImporter.GetElementName(item)
			});
			return new WsdlImporter.AlreadyFaultedException(@string, innerException);
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x0009FB60 File Offset: 0x0009DD60
		private static Exception CreateExtensionException(IWsdlImportExtension importer, Exception e)
		{
			string @string = SR.GetString("WsdlExtensionImportError", new object[]
			{
				importer.GetType().FullName,
				e.Message
			});
			return new InvalidOperationException(@string, e);
		}

		// Token: 0x04002285 RID: 8837
		private readonly Dictionary<NamedItem, WsdlImporter.WsdlImportException> importErrors = new Dictionary<NamedItem, WsdlImporter.WsdlImportException>();

		// Token: 0x04002286 RID: 8838
		private bool isFaulted;

		// Token: 0x04002287 RID: 8839
		private readonly Dictionary<XmlQualifiedName, WsdlContractConversionContext> importedPortTypes = new Dictionary<XmlQualifiedName, WsdlContractConversionContext>();

		// Token: 0x04002288 RID: 8840
		private readonly Dictionary<XmlQualifiedName, WsdlEndpointConversionContext> importedBindings = new Dictionary<XmlQualifiedName, WsdlEndpointConversionContext>();

		// Token: 0x04002289 RID: 8841
		private readonly Dictionary<Port, ServiceEndpoint> importedPorts = new Dictionary<Port, ServiceEndpoint>();

		// Token: 0x0400228A RID: 8842
		private readonly KeyedByTypeCollection<IWsdlImportExtension> wsdlExtensions;

		// Token: 0x0400228B RID: 8843
		private readonly ServiceDescriptionCollection wsdlDocuments = new ServiceDescriptionCollection();

		// Token: 0x0400228C RID: 8844
		private readonly XmlSchemaSet xmlSchemas = WsdlExporter.GetEmptySchemaSet();

		// Token: 0x0400228D RID: 8845
		private readonly Dictionary<string, XmlElement> policyDocuments = new Dictionary<string, XmlElement>();

		// Token: 0x0400228E RID: 8846
		private readonly Dictionary<string, string> warnings = new Dictionary<string, string>();

		// Token: 0x0400228F RID: 8847
		private WsdlImporter.WsdlPolicyReader wsdlPolicyReader;

		// Token: 0x04002290 RID: 8848
		private bool beforeImportCalled;

		// Token: 0x04002291 RID: 8849
		private const string xPathDocumentFormatString = "//wsdl:definitions[@targetNamespace='{0}']";

		// Token: 0x04002292 RID: 8850
		private const string xPathItemSubFormatString = "/wsdl:{0}";

		// Token: 0x04002293 RID: 8851
		private const string xPathNamedItemSubFormatString = "/wsdl:{0}[@name='{1}']";

		// Token: 0x02000BF1 RID: 3057
		internal static class Binding2DescriptionHelper
		{
			// Token: 0x060075BC RID: 30140 RVA: 0x001B97DC File Offset: 0x001B79DC
			internal static OperationDescription FindOperationDescription(OperationBinding wsdlOperationBinding, ServiceDescriptionCollection wsdlDocuments, WsdlEndpointConversionContext endpointContext)
			{
				OperationDescription result;
				if (endpointContext.ContractConversionContext != null)
				{
					Operation operation = WsdlImporter.Binding2DescriptionHelper.FindWsdlOperation(wsdlOperationBinding, wsdlDocuments);
					result = endpointContext.ContractConversionContext.GetOperationDescription(operation);
				}
				else
				{
					result = WsdlImporter.Binding2DescriptionHelper.FindOperationDescription(endpointContext.Endpoint.Contract, wsdlOperationBinding);
				}
				return result;
			}

			// Token: 0x060075BD RID: 30141 RVA: 0x001B981C File Offset: 0x001B7A1C
			internal static MessageBinding FindMessageBinding(OperationBinding wsdlOperationBinding, MessageDescription message)
			{
				MessageBinding result;
				if (message.Direction == MessageDirection.Input)
				{
					result = wsdlOperationBinding.Input;
				}
				else
				{
					result = wsdlOperationBinding.Output;
				}
				return result;
			}

			// Token: 0x060075BE RID: 30142 RVA: 0x001B9844 File Offset: 0x001B7A44
			internal static FaultBinding FindFaultBinding(OperationBinding wsdlOperationBinding, FaultDescription fault)
			{
				foreach (object obj in wsdlOperationBinding.Faults)
				{
					FaultBinding faultBinding = (FaultBinding)obj;
					if (faultBinding.Name == fault.Name)
					{
						return faultBinding;
					}
				}
				return null;
			}

			// Token: 0x060075BF RID: 30143 RVA: 0x001B98B0 File Offset: 0x001B7AB0
			private static Operation FindWsdlOperation(OperationBinding wsdlOperationBinding, ServiceDescriptionCollection wsdlDocuments)
			{
				PortType portType = wsdlDocuments.GetPortType(wsdlOperationBinding.Binding.Type);
				if (wsdlOperationBinding.Name == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidWsdlBindingOpNoName", new object[]
					{
						wsdlOperationBinding.Binding.Name
					})));
				}
				Operation operation = null;
				foreach (object obj in portType.Operations)
				{
					Operation operation2 = (Operation)obj;
					switch (WsdlImporter.Binding2DescriptionHelper.Match(wsdlOperationBinding, operation2))
					{
					case WsdlImporter.Binding2DescriptionHelper.MatchResult.None:
						break;
					case WsdlImporter.Binding2DescriptionHelper.MatchResult.Partial:
						operation = operation2;
						break;
					case WsdlImporter.Binding2DescriptionHelper.MatchResult.Exact:
						return operation2;
					default:
						Fx.AssertAndFailFast("Unexpected MatchResult value.");
						break;
					}
				}
				if (operation != null)
				{
					return operation;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidWsdlBindingOpMismatch2", new object[]
				{
					wsdlOperationBinding.Binding.Name,
					wsdlOperationBinding.Name
				})));
			}

			// Token: 0x060075C0 RID: 30144 RVA: 0x001B99C8 File Offset: 0x001B7BC8
			internal static WsdlImporter.Binding2DescriptionHelper.MatchResult Match(OperationBinding wsdlOperationBinding, Operation wsdlOperation)
			{
				if (wsdlOperationBinding.Name != wsdlOperation.Name)
				{
					return WsdlImporter.Binding2DescriptionHelper.MatchResult.None;
				}
				WsdlImporter.Binding2DescriptionHelper.MatchResult result = WsdlImporter.Binding2DescriptionHelper.MatchResult.Exact;
				foreach (object obj in wsdlOperation.Messages)
				{
					OperationMessage operationMessage = (OperationMessage)obj;
					MessageBinding messageBinding;
					if (operationMessage is OperationInput)
					{
						messageBinding = wsdlOperationBinding.Input;
					}
					else
					{
						messageBinding = wsdlOperationBinding.Output;
					}
					if (messageBinding == null)
					{
						return WsdlImporter.Binding2DescriptionHelper.MatchResult.None;
					}
					WsdlImporter.Binding2DescriptionHelper.MatchResult matchResult = WsdlImporter.Binding2DescriptionHelper.MatchOperationParameterName(messageBinding, operationMessage);
					if (matchResult == WsdlImporter.Binding2DescriptionHelper.MatchResult.None)
					{
						return WsdlImporter.Binding2DescriptionHelper.MatchResult.None;
					}
					if (matchResult == WsdlImporter.Binding2DescriptionHelper.MatchResult.Partial)
					{
						result = WsdlImporter.Binding2DescriptionHelper.MatchResult.Partial;
					}
				}
				return result;
			}

			// Token: 0x060075C1 RID: 30145 RVA: 0x001B9A74 File Offset: 0x001B7C74
			private static WsdlImporter.Binding2DescriptionHelper.MatchResult MatchOperationParameterName(MessageBinding wsdlMessageBinding, OperationMessage wsdlOperationMessage)
			{
				string name = wsdlOperationMessage.Name;
				string name2 = wsdlMessageBinding.Name;
				if (name == name2)
				{
					return WsdlImporter.Binding2DescriptionHelper.MatchResult.Exact;
				}
				string decodedName = WsdlImporter.WsdlNamingHelper.GetOperationMessageName(wsdlOperationMessage).DecodedName;
				if (name == null && name2 == decodedName)
				{
					return WsdlImporter.Binding2DescriptionHelper.MatchResult.Partial;
				}
				if (name2 == null && name == decodedName)
				{
					return WsdlImporter.Binding2DescriptionHelper.MatchResult.Partial;
				}
				return WsdlImporter.Binding2DescriptionHelper.MatchResult.None;
			}

			// Token: 0x060075C2 RID: 30146 RVA: 0x001B9AC4 File Offset: 0x001B7CC4
			private static OperationDescription FindOperationDescription(ContractDescription contract, OperationBinding wsdlOperationBinding)
			{
				foreach (OperationDescription operationDescription in contract.Operations)
				{
					if (WsdlImporter.Binding2DescriptionHelper.CompareOperations(operationDescription, contract, wsdlOperationBinding))
					{
						return operationDescription;
					}
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnableToLocateOperation2", new object[]
				{
					wsdlOperationBinding.Name,
					contract.Name
				})));
			}

			// Token: 0x060075C3 RID: 30147 RVA: 0x001B9B4C File Offset: 0x001B7D4C
			private static bool CompareOperations(OperationDescription operationDescription, ContractDescription parentContractDescription, OperationBinding wsdlOperationBinding)
			{
				string wsdlOperationName = WsdlExporter.WsdlNamingHelper.GetWsdlOperationName(operationDescription, parentContractDescription);
				return !(wsdlOperationName != wsdlOperationBinding.Name) && operationDescription.Messages.Count <= 2 && WsdlImporter.Binding2DescriptionHelper.FindMessage(operationDescription.Messages, MessageDirection.Output) == (wsdlOperationBinding.Output != null) && WsdlImporter.Binding2DescriptionHelper.FindMessage(operationDescription.Messages, MessageDirection.Input) == (wsdlOperationBinding.Input != null);
			}

			// Token: 0x060075C4 RID: 30148 RVA: 0x001B9BB4 File Offset: 0x001B7DB4
			private static bool FindMessage(MessageDescriptionCollection messageDescriptionCollection, MessageDirection transferDirection)
			{
				foreach (MessageDescription messageDescription in messageDescriptionCollection)
				{
					if (messageDescription.Direction == transferDirection)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x02000F29 RID: 3881
			internal enum MatchResult
			{
				// Token: 0x04004DF2 RID: 19954
				None,
				// Token: 0x04004DF3 RID: 19955
				Partial,
				// Token: 0x04004DF4 RID: 19956
				Exact
			}
		}

		// Token: 0x02000BF2 RID: 3058
		internal static class WSAddressingHelper
		{
			// Token: 0x060075C5 RID: 30149 RVA: 0x001B9C08 File Offset: 0x001B7E08
			internal static string GetWsaActionUri(OperationMessage wsdlOperationMessage)
			{
				string text = WsdlImporter.WSAddressingHelper.FindWsaActionAttribute(wsdlOperationMessage);
				if (text != null)
				{
					return text;
				}
				return WsdlImporter.WSAddressingHelper.CreateDefaultWsaActionUri(wsdlOperationMessage);
			}

			// Token: 0x060075C6 RID: 30150 RVA: 0x001B9C28 File Offset: 0x001B7E28
			internal static string FindWsaActionAttribute(OperationMessage wsdlOperationMessage)
			{
				XmlAttribute[] extensibleAttributes = wsdlOperationMessage.ExtensibleAttributes;
				if (extensibleAttributes != null && extensibleAttributes.Length != 0)
				{
					foreach (XmlAttribute xmlAttribute in extensibleAttributes)
					{
						if ((xmlAttribute.NamespaceURI == "http://www.w3.org/2006/05/addressing/wsdl" || xmlAttribute.NamespaceURI == "http://www.w3.org/2007/05/addressing/metadata") && xmlAttribute.LocalName == "Action")
						{
							return xmlAttribute.Value;
						}
					}
				}
				return null;
			}

			// Token: 0x060075C7 RID: 30151 RVA: 0x001B9C98 File Offset: 0x001B7E98
			private static string CreateDefaultWsaActionUri(OperationMessage wsdlOperationMessage)
			{
				if (wsdlOperationMessage is OperationFault)
				{
					return AddressingVersion.WSAddressing10.DefaultFaultAction;
				}
				string text = wsdlOperationMessage.Operation.PortType.ServiceDescription.TargetNamespace ?? string.Empty;
				string name = wsdlOperationMessage.Operation.PortType.Name;
				XmlName operationMessageName = WsdlImporter.WsdlNamingHelper.GetOperationMessageName(wsdlOperationMessage);
				string text2 = text.StartsWith("urn:", StringComparison.OrdinalIgnoreCase) ? ":" : "/";
				string text3 = text.EndsWith(text2, StringComparison.OrdinalIgnoreCase) ? text : (text + text2);
				return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}", new object[]
				{
					text3,
					name,
					text2,
					operationMessageName.EncodedName
				});
			}

			// Token: 0x060075C8 RID: 30152 RVA: 0x001B9D50 File Offset: 0x001B7F50
			internal static EndpointAddress ImportAddress(Port wsdlPort)
			{
				if (wsdlPort != null)
				{
					XmlElement xmlElement = wsdlPort.Extensions.Find("EndpointReference", "http://www.w3.org/2005/08/addressing");
					XmlElement xmlElement2 = wsdlPort.Extensions.Find("EndpointReference", "http://schemas.xmlsoap.org/ws/2004/08/addressing");
					SoapAddressBinding soapAddressBinding = (SoapAddressBinding)wsdlPort.Extensions.Find(typeof(SoapAddressBinding));
					if (xmlElement != null)
					{
						return EndpointAddress.ReadFrom(AddressingVersion.WSAddressing10, new XmlNodeReader(xmlElement));
					}
					if (xmlElement2 != null)
					{
						return EndpointAddress.ReadFrom(AddressingVersion.WSAddressingAugust2004, new XmlNodeReader(xmlElement2));
					}
					if (soapAddressBinding != null)
					{
						return new EndpointAddress(soapAddressBinding.Location);
					}
				}
				return null;
			}

			// Token: 0x060075C9 RID: 30153 RVA: 0x001B9DE0 File Offset: 0x001B7FE0
			internal static AddressingVersion FindAddressingVersion(PolicyConversionContext policyContext)
			{
				if (PolicyConversionContext.FindAssertion(policyContext.GetBindingAssertions(), "UsingAddressing", "http://www.w3.org/2006/05/addressing/wsdl", true) != null)
				{
					return AddressingVersion.WSAddressing10;
				}
				if (PolicyConversionContext.FindAssertion(policyContext.GetBindingAssertions(), "Addressing", "http://www.w3.org/2007/05/addressing/metadata", true) != null)
				{
					return AddressingVersion.WSAddressing10;
				}
				if (PolicyConversionContext.FindAssertion(policyContext.GetBindingAssertions(), "UsingAddressing", "http://schemas.xmlsoap.org/ws/2004/08/addressing/policy", true) != null)
				{
					return AddressingVersion.WSAddressingAugust2004;
				}
				return AddressingVersion.None;
			}

			// Token: 0x060075CA RID: 30154 RVA: 0x001B9E4C File Offset: 0x001B804C
			internal static SupportedAddressingMode DetermineSupportedAddressingMode(MetadataImporter importer, PolicyConversionContext context)
			{
				XmlElement xmlElement = PolicyConversionContext.FindAssertion(context.GetBindingAssertions(), "Addressing", "http://www.w3.org/2007/05/addressing/metadata", false);
				if (xmlElement != null)
				{
					XmlElement xmlElement2 = null;
					foreach (object obj in xmlElement.ChildNodes)
					{
						XmlNode xmlNode = (XmlNode)obj;
						if (xmlNode is XmlElement && MetadataSection.IsPolicyElement((XmlElement)xmlNode))
						{
							xmlElement2 = (XmlElement)xmlNode;
							break;
						}
					}
					if (xmlElement2 == null)
					{
						string @string = SR.GetString("ElementRequired", new object[]
						{
							"wsam",
							"Addressing",
							"wsp",
							"Policy"
						});
						importer.Errors.Add(new MetadataConversionError(@string, false));
						return SupportedAddressingMode.Anonymous;
					}
					IEnumerable<IEnumerable<XmlElement>> enumerable = importer.NormalizePolicy(new XmlElement[]
					{
						xmlElement2
					});
					foreach (IEnumerable<XmlElement> enumerable2 in enumerable)
					{
						foreach (XmlElement xmlElement3 in enumerable2)
						{
							if (xmlElement3.NamespaceURI == "http://www.w3.org/2007/05/addressing/metadata")
							{
								if (xmlElement3.LocalName == "NonAnonymousResponses")
								{
									return SupportedAddressingMode.NonAnonymous;
								}
								if (xmlElement3.LocalName == "AnonymousResponses")
								{
									return SupportedAddressingMode.Anonymous;
								}
							}
						}
					}
					return SupportedAddressingMode.Anonymous;
				}
				return SupportedAddressingMode.Anonymous;
			}
		}

		// Token: 0x02000BF3 RID: 3059
		private static class WsdlNamingHelper
		{
			// Token: 0x060075CB RID: 30155 RVA: 0x001B9FF4 File Offset: 0x001B81F4
			internal static XmlQualifiedName GetBindingName(System.Web.Services.Description.Binding wsdlBinding)
			{
				XmlName xmlName = new XmlName(wsdlBinding.Name, true);
				return new XmlQualifiedName(xmlName.EncodedName, wsdlBinding.ServiceDescription.TargetNamespace);
			}

			// Token: 0x060075CC RID: 30156 RVA: 0x001BA024 File Offset: 0x001B8224
			internal static XmlQualifiedName GetBindingName(Port wsdlPort)
			{
				XmlName xmlName = new XmlName(string.Format(CultureInfo.InvariantCulture, "{0}_{1}", new object[]
				{
					wsdlPort.Service.Name,
					wsdlPort.Name
				}), true);
				return new XmlQualifiedName(xmlName.EncodedName, wsdlPort.Service.ServiceDescription.TargetNamespace);
			}

			// Token: 0x060075CD RID: 30157 RVA: 0x001BA07F File Offset: 0x001B827F
			internal static XmlName GetEndpointName(Port wsdlPort)
			{
				return new XmlName(wsdlPort.Name, true);
			}

			// Token: 0x060075CE RID: 30158 RVA: 0x001BA08D File Offset: 0x001B828D
			internal static XmlQualifiedName GetContractName(XmlQualifiedName wsdlPortTypeQName)
			{
				return wsdlPortTypeQName;
			}

			// Token: 0x060075CF RID: 30159 RVA: 0x001BA090 File Offset: 0x001B8290
			internal static string GetOperationName(Operation wsdlOperation)
			{
				return wsdlOperation.Name;
			}

			// Token: 0x060075D0 RID: 30160 RVA: 0x001BA098 File Offset: 0x001B8298
			internal static XmlName GetOperationMessageName(OperationMessage wsdlOperationMessage)
			{
				string name = null;
				if (!string.IsNullOrEmpty(wsdlOperationMessage.Name))
				{
					name = wsdlOperationMessage.Name;
				}
				else if (wsdlOperationMessage.Operation.Messages.Count == 1)
				{
					name = wsdlOperationMessage.Operation.Name;
				}
				else if (wsdlOperationMessage.Operation.Messages.IndexOf(wsdlOperationMessage) == 0)
				{
					if (wsdlOperationMessage is OperationInput)
					{
						name = wsdlOperationMessage.Operation.Name + "Request";
					}
					else if (wsdlOperationMessage is OperationOutput)
					{
						name = wsdlOperationMessage.Operation.Name + "Solicit";
					}
				}
				else if (wsdlOperationMessage.Operation.Messages.IndexOf(wsdlOperationMessage) == 1)
				{
					name = wsdlOperationMessage.Operation.Name + "Response";
				}
				return new XmlName(name, true);
			}
		}

		// Token: 0x02000BF4 RID: 3060
		internal static class NetSessionHelper
		{
			// Token: 0x060075D1 RID: 30161 RVA: 0x001BA168 File Offset: 0x001B8368
			internal static void SetInitiatingTerminating(OperationDescription operationDescription, Operation wsdlOperation)
			{
				XmlAttribute xmlAttribute = WsdlImporter.NetSessionHelper.FindAttribute(wsdlOperation.ExtensibleAttributes, "isInitiating", "http://schemas.microsoft.com/ws/2005/12/wsdl/contract");
				if (xmlAttribute != null)
				{
					if (xmlAttribute.Value == "true")
					{
						operationDescription.IsInitiating = true;
					}
					if (xmlAttribute.Value == "false")
					{
						operationDescription.IsInitiating = false;
					}
				}
				XmlAttribute xmlAttribute2 = WsdlImporter.NetSessionHelper.FindAttribute(wsdlOperation.ExtensibleAttributes, "isTerminating", "http://schemas.microsoft.com/ws/2005/12/wsdl/contract");
				if (xmlAttribute2 != null)
				{
					if (xmlAttribute2.Value == "true")
					{
						operationDescription.IsTerminating = true;
					}
					if (xmlAttribute2.Value == "false")
					{
						operationDescription.IsTerminating = false;
					}
				}
			}

			// Token: 0x060075D2 RID: 30162 RVA: 0x001BA20C File Offset: 0x001B840C
			internal static void SetSession(ContractDescription contractDescription, PortType wsdlPortType)
			{
				XmlAttribute xmlAttribute = WsdlImporter.NetSessionHelper.FindAttribute(wsdlPortType.ExtensibleAttributes, "usingSession", "http://schemas.microsoft.com/ws/2005/12/wsdl/contract");
				if (xmlAttribute != null)
				{
					if (xmlAttribute.Value == "true")
					{
						contractDescription.SessionMode = SessionMode.Required;
					}
					if (xmlAttribute.Value == "false")
					{
						contractDescription.SessionMode = SessionMode.NotAllowed;
					}
				}
			}

			// Token: 0x060075D3 RID: 30163 RVA: 0x001BA264 File Offset: 0x001B8464
			private static XmlAttribute FindAttribute(XmlAttribute[] attributes, string localName, string ns)
			{
				if (attributes != null)
				{
					foreach (XmlAttribute xmlAttribute in attributes)
					{
						if (xmlAttribute.LocalName == localName && xmlAttribute.NamespaceURI == ns)
						{
							return xmlAttribute;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x02000BF5 RID: 3061
		internal static class SoapInPolicyWorkaroundHelper
		{
			// Token: 0x060075D4 RID: 30164 RVA: 0x001BA2A8 File Offset: 0x001B84A8
			public static void InsertAdHocPolicy(System.Web.Services.Description.Binding wsdlBinding, string value, string key)
			{
				XmlQualifiedName wsdlBindingQName = new XmlQualifiedName(wsdlBinding.Name, wsdlBinding.ServiceDescription.TargetNamespace);
				string id = WsdlImporter.SoapInPolicyWorkaroundHelper.AddPolicyUri(wsdlBinding, key);
				WsdlImporter.SoapInPolicyWorkaroundHelper.InsertPolicy(key, id, wsdlBinding.ServiceDescription, value, wsdlBindingQName);
			}

			// Token: 0x060075D5 RID: 30165 RVA: 0x001BA2E3 File Offset: 0x001B84E3
			public static string FindAdHocTransportPolicy(PolicyConversionContext policyContext, out XmlQualifiedName wsdlBindingQName)
			{
				return WsdlImporter.SoapInPolicyWorkaroundHelper.FindAdHocPolicy(policyContext, "TransportBindingElementImporter.TransportUri", out wsdlBindingQName);
			}

			// Token: 0x060075D6 RID: 30166 RVA: 0x001BA2F4 File Offset: 0x001B84F4
			public static string FindAdHocPolicy(PolicyConversionContext policyContext, string key, out XmlQualifiedName wsdlBindingQName)
			{
				if (policyContext == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("policyContext");
				}
				XmlElement xmlElement = PolicyConversionContext.FindAssertion(policyContext.GetBindingAssertions(), key, "http://tempuri.org/temporaryworkaround", true);
				if (xmlElement != null)
				{
					wsdlBindingQName = new XmlQualifiedName(xmlElement.Attributes["bindingName"].Value, xmlElement.Attributes["bindingNamespace"].Value);
					return xmlElement.InnerText;
				}
				wsdlBindingQName = null;
				return null;
			}

			// Token: 0x060075D7 RID: 30167 RVA: 0x001BA368 File Offset: 0x001B8568
			private static string AddPolicyUri(System.Web.Services.Description.Binding wsdlBinding, string name)
			{
				string text = WsdlImporter.SoapInPolicyWorkaroundHelper.ReadPolicyUris(wsdlBinding);
				string text2 = string.Format(CultureInfo.InvariantCulture, "{0}_{1}_BindingAdHocPolicy", new object[]
				{
					wsdlBinding.Name,
					name
				});
				string newValue = string.Format(CultureInfo.InvariantCulture, "#{0} {1}", new object[]
				{
					text2,
					text
				}).Trim();
				WsdlImporter.SoapInPolicyWorkaroundHelper.WritePolicyUris(wsdlBinding, newValue);
				return text2;
			}

			// Token: 0x17001AFC RID: 6908
			// (get) Token: 0x060075D8 RID: 30168 RVA: 0x001BA3CC File Offset: 0x001B85CC
			private static XmlDocument XmlDoc
			{
				get
				{
					if (WsdlImporter.SoapInPolicyWorkaroundHelper.xmlDocument == null)
					{
						NameTable nameTable = new NameTable();
						nameTable.Add("Policy");
						nameTable.Add("All");
						nameTable.Add("ExactlyOne");
						nameTable.Add("PolicyURIs");
						nameTable.Add("Id");
						WsdlImporter.SoapInPolicyWorkaroundHelper.xmlDocument = new XmlDocument(nameTable);
					}
					return WsdlImporter.SoapInPolicyWorkaroundHelper.xmlDocument;
				}
			}

			// Token: 0x060075D9 RID: 30169 RVA: 0x001BA434 File Offset: 0x001B8634
			private static void WritePolicyUris(DocumentableItem item, string newValue)
			{
				XmlAttribute[] array = item.ExtensibleAttributes;
				int num;
				if (array != null && array.Length != 0)
				{
					foreach (XmlAttribute xmlAttribute in array)
					{
						if (MetadataImporter.PolicyHelper.IsPolicyURIs(xmlAttribute))
						{
							xmlAttribute.Value = newValue;
							return;
						}
					}
					num = array.Length;
					Array.Resize<XmlAttribute>(ref array, num + 1);
				}
				else
				{
					num = 0;
					array = new XmlAttribute[1];
				}
				array[num] = WsdlImporter.SoapInPolicyWorkaroundHelper.CreatePolicyURIsAttribute(newValue);
				item.ExtensibleAttributes = array;
			}

			// Token: 0x060075DA RID: 30170 RVA: 0x001BA4A0 File Offset: 0x001B86A0
			private static XmlAttribute CreatePolicyURIsAttribute(string value)
			{
				XmlAttribute xmlAttribute = WsdlImporter.SoapInPolicyWorkaroundHelper.XmlDoc.CreateAttribute("wsp", "PolicyURIs", "http://schemas.xmlsoap.org/ws/2004/09/policy");
				xmlAttribute.Value = value;
				return xmlAttribute;
			}

			// Token: 0x060075DB RID: 30171 RVA: 0x001BA4D0 File Offset: 0x001B86D0
			private static string ReadPolicyUris(DocumentableItem item)
			{
				XmlAttribute[] extensibleAttributes = item.ExtensibleAttributes;
				if (extensibleAttributes != null && extensibleAttributes.Length != 0)
				{
					foreach (XmlAttribute xmlAttribute in extensibleAttributes)
					{
						if (MetadataImporter.PolicyHelper.IsPolicyURIs(xmlAttribute))
						{
							return xmlAttribute.Value;
						}
					}
				}
				return string.Empty;
			}

			// Token: 0x060075DC RID: 30172 RVA: 0x001BA514 File Offset: 0x001B8714
			private static void InsertPolicy(string key, string id, ServiceDescription policyWsdl, string value, XmlQualifiedName wsdlBindingQName)
			{
				XmlElement xmlElement = WsdlImporter.SoapInPolicyWorkaroundHelper.CreatePolicyElement(key, value, wsdlBindingQName);
				XmlAttribute xmlAttribute = WsdlImporter.SoapInPolicyWorkaroundHelper.XmlDoc.CreateAttribute("wsu", "Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
				xmlAttribute.Value = id;
				xmlElement.SetAttributeNode(xmlAttribute);
				policyWsdl.Extensions.Add(xmlElement);
			}

			// Token: 0x060075DD RID: 30173 RVA: 0x001BA564 File Offset: 0x001B8764
			private static XmlElement CreatePolicyElement(string elementName, string value, XmlQualifiedName wsdlBindingQName)
			{
				XmlElement xmlElement = WsdlImporter.SoapInPolicyWorkaroundHelper.XmlDoc.CreateElement("wsp", "Policy", "http://schemas.xmlsoap.org/ws/2004/09/policy");
				XmlElement xmlElement2 = WsdlImporter.SoapInPolicyWorkaroundHelper.XmlDoc.CreateElement("wsp", "ExactlyOne", "http://schemas.xmlsoap.org/ws/2004/09/policy");
				xmlElement.AppendChild(xmlElement2);
				XmlElement xmlElement3 = WsdlImporter.SoapInPolicyWorkaroundHelper.XmlDoc.CreateElement("wsp", "All", "http://schemas.xmlsoap.org/ws/2004/09/policy");
				xmlElement2.AppendChild(xmlElement3);
				XmlElement xmlElement4 = WsdlImporter.SoapInPolicyWorkaroundHelper.xmlDocument.CreateElement(elementName, "http://tempuri.org/temporaryworkaround");
				xmlElement4.InnerText = value;
				XmlAttribute xmlAttribute = WsdlImporter.SoapInPolicyWorkaroundHelper.xmlDocument.CreateAttribute("bindingName");
				xmlAttribute.Value = wsdlBindingQName.Name;
				xmlElement4.Attributes.Append(xmlAttribute);
				XmlAttribute xmlAttribute2 = WsdlImporter.SoapInPolicyWorkaroundHelper.xmlDocument.CreateAttribute("bindingNamespace");
				xmlAttribute2.Value = wsdlBindingQName.Namespace;
				xmlElement4.Attributes.Append(xmlAttribute2);
				xmlElement4.Attributes.Append(xmlAttribute2);
				xmlElement3.AppendChild(xmlElement4);
				return xmlElement;
			}

			// Token: 0x060075DE RID: 30174 RVA: 0x001BA658 File Offset: 0x001B8858
			internal static void InsertAdHocTransportPolicy(ServiceDescriptionCollection wsdlDocuments)
			{
				foreach (object obj in wsdlDocuments)
				{
					ServiceDescription serviceDescription = (ServiceDescription)obj;
					if (serviceDescription != null)
					{
						foreach (object obj2 in serviceDescription.Bindings)
						{
							System.Web.Services.Description.Binding binding = (System.Web.Services.Description.Binding)obj2;
							if (WsdlImporter.WsdlPolicyReader.ContainsPolicy(binding))
							{
								SoapBinding soapBinding = (SoapBinding)binding.Extensions.Find(typeof(SoapBinding));
								if (soapBinding != null)
								{
									WsdlImporter.SoapInPolicyWorkaroundHelper.InsertAdHocPolicy(binding, soapBinding.Transport, "TransportBindingElementImporter.TransportUri");
								}
							}
						}
					}
				}
			}

			// Token: 0x0400428A RID: 17034
			public const string soapTransportUriKey = "TransportBindingElementImporter.TransportUri";

			// Token: 0x0400428B RID: 17035
			private const string workaroundNS = "http://tempuri.org/temporaryworkaround";

			// Token: 0x0400428C RID: 17036
			private const string bindingAttrName = "bindingName";

			// Token: 0x0400428D RID: 17037
			private const string bindingAttrNamespace = "bindingNamespace";

			// Token: 0x0400428E RID: 17038
			private static XmlDocument xmlDocument;
		}

		// Token: 0x02000BF6 RID: 3062
		private class AlreadyFaultedException : InvalidOperationException
		{
			// Token: 0x060075DF RID: 30175 RVA: 0x001BA730 File Offset: 0x001B8930
			internal AlreadyFaultedException(string message, WsdlImporter.WsdlImportException innerException) : base(message, innerException)
			{
			}
		}

		// Token: 0x02000BF7 RID: 3063
		private class WsdlImportException : Exception
		{
			// Token: 0x060075E0 RID: 30176 RVA: 0x001BA73A File Offset: 0x001B893A
			private WsdlImportException(NamedItem item, Exception innerException) : base(string.Empty, innerException)
			{
				this.xPath = WsdlImporter.CreateXPathString(item);
				this.sourceItem = item;
			}

			// Token: 0x060075E1 RID: 30177 RVA: 0x001BA75C File Offset: 0x001B895C
			internal static WsdlImporter.WsdlImportException Create(NamedItem item, Exception innerException)
			{
				WsdlImporter.WsdlImportException ex = innerException as WsdlImporter.WsdlImportException;
				if (ex != null && ex.IsChildNodeOf(item))
				{
					ex.sourceItem = item;
					return ex;
				}
				WsdlImporter.AlreadyFaultedException ex2 = innerException as WsdlImporter.AlreadyFaultedException;
				if (ex2 != null)
				{
					return new WsdlImporter.WsdlImportException(item, ex2.InnerException);
				}
				return new WsdlImporter.WsdlImportException(item, innerException);
			}

			// Token: 0x060075E2 RID: 30178 RVA: 0x001BA7A3 File Offset: 0x001B89A3
			internal bool IsChildNodeOf(NamedItem item)
			{
				return this.XPath.StartsWith(WsdlImporter.CreateXPathString(item), StringComparison.Ordinal);
			}

			// Token: 0x17001AFD RID: 6909
			// (get) Token: 0x060075E3 RID: 30179 RVA: 0x001BA7B7 File Offset: 0x001B89B7
			internal string XPath
			{
				get
				{
					return this.xPath;
				}
			}

			// Token: 0x17001AFE RID: 6910
			// (get) Token: 0x060075E4 RID: 30180 RVA: 0x001BA7BF File Offset: 0x001B89BF
			internal NamedItem SourceItem
			{
				get
				{
					return this.sourceItem;
				}
			}

			// Token: 0x17001AFF RID: 6911
			// (get) Token: 0x060075E5 RID: 30181 RVA: 0x001BA7C8 File Offset: 0x001B89C8
			public override string Message
			{
				get
				{
					Exception innerException = base.InnerException;
					while (innerException is WsdlImporter.WsdlImportException)
					{
						innerException = innerException.InnerException;
					}
					if (innerException == null)
					{
						return string.Empty;
					}
					return innerException.Message;
				}
			}

			// Token: 0x0400428F RID: 17039
			private NamedItem sourceItem;

			// Token: 0x04004290 RID: 17040
			private readonly string xPath;
		}

		// Token: 0x02000BF8 RID: 3064
		internal class WsdlPolicyReader
		{
			// Token: 0x060075E6 RID: 30182 RVA: 0x001BA7FC File Offset: 0x001B89FC
			internal WsdlPolicyReader(WsdlImporter importer)
			{
				this.importer = importer;
				this.policyDictionary = new WsdlImporter.WsdlPolicyReader.WsdlPolicyDictionary(importer);
				importer.PolicyWarningOccured += this.LogPolicyNormalizationWarning;
			}

			// Token: 0x060075E7 RID: 30183 RVA: 0x001BA82C File Offset: 0x001B8A2C
			private IEnumerable<IEnumerable<XmlElement>> GetPolicyAlternatives(NamedItem item, ServiceDescription wsdl)
			{
				Collection<XmlElement> collection = new Collection<XmlElement>();
				foreach (XmlElement item2 in this.GetReferencedPolicy(item, wsdl))
				{
					collection.Add(item2);
				}
				foreach (XmlElement xmlElement in WsdlImporter.WsdlPolicyReader.GetEmbeddedPolicy(item))
				{
					collection.Add(xmlElement);
					if (!this.policyDictionary.PolicySourceTable.ContainsKey(xmlElement))
					{
						this.policyDictionary.PolicySourceTable.Add(xmlElement, wsdl);
					}
				}
				return this.importer.NormalizePolicy(collection);
			}

			// Token: 0x060075E8 RID: 30184 RVA: 0x001BA8F4 File Offset: 0x001B8AF4
			private void LogPolicyNormalizationWarning(XmlElement contextAssertion, string warningMessage)
			{
				string text = null;
				if (contextAssertion != null)
				{
					text = this.policyDictionary.CreateIdXPath(contextAssertion);
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(warningMessage);
				if (!string.IsNullOrEmpty(text))
				{
					stringBuilder.AppendLine(SR.GetString("XPathPointer", new object[]
					{
						text
					}));
				}
				else
				{
					stringBuilder.AppendLine(SR.GetString("XPathPointer", new object[]
					{
						SR.GetString("XPathUnavailable")
					}));
				}
				this.importer.LogImportWarning(stringBuilder.ToString());
			}

			// Token: 0x060075E9 RID: 30185 RVA: 0x001BA97C File Offset: 0x001B8B7C
			internal static bool ContainsPolicy(System.Web.Services.Description.Binding wsdlBinding)
			{
				if (WsdlImporter.WsdlPolicyReader.HasPolicyAttached(wsdlBinding))
				{
					return true;
				}
				foreach (object obj in wsdlBinding.Operations)
				{
					OperationBinding operationBinding = (OperationBinding)obj;
					if (WsdlImporter.WsdlPolicyReader.HasPolicyAttached(operationBinding))
					{
						return true;
					}
					if (operationBinding.Input != null && WsdlImporter.WsdlPolicyReader.HasPolicyAttached(operationBinding.Input))
					{
						return true;
					}
					if (operationBinding.Output != null && WsdlImporter.WsdlPolicyReader.HasPolicyAttached(operationBinding.Output))
					{
						return true;
					}
					foreach (object obj2 in operationBinding.Faults)
					{
						FaultBinding item = (FaultBinding)obj2;
						if (WsdlImporter.WsdlPolicyReader.HasPolicyAttached(item))
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x060075EA RID: 30186 RVA: 0x001BAA7C File Offset: 0x001B8C7C
			internal static bool HasPolicy(Port wsdlPort)
			{
				return WsdlImporter.WsdlPolicyReader.HasPolicyAttached(wsdlPort);
			}

			// Token: 0x060075EB RID: 30187 RVA: 0x001BAA84 File Offset: 0x001B8C84
			internal static IEnumerable<XmlElement> GetEmbeddedPolicy(NamedItem item)
			{
				List<XmlElement> list = new List<XmlElement>();
				list.AddRange(item.Extensions.FindAll("Policy", "http://schemas.xmlsoap.org/ws/2004/09/policy"));
				list.AddRange(item.Extensions.FindAll("Policy", "http://www.w3.org/ns/ws-policy"));
				return list;
			}

			// Token: 0x060075EC RID: 30188 RVA: 0x001BAACE File Offset: 0x001B8CCE
			private IEnumerable<XmlElement> GetReferencedPolicy(NamedItem item, ServiceDescription wsdl)
			{
				string xPath = WsdlImporter.CreateXPathString(item);
				foreach (string text in this.GetPolicyReferenceUris(item, xPath))
				{
					XmlElement xmlElement = this.policyDictionary.ResolvePolicyReference(text, wsdl);
					if (xmlElement == null)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendLine(SR.GetString("UnableToFindPolicyWithId", new object[]
						{
							text
						}));
						stringBuilder.AppendLine(SR.GetString("XPathPointer", new object[]
						{
							xPath
						}));
						this.importer.LogImportWarning(stringBuilder.ToString());
					}
					else
					{
						yield return xmlElement;
					}
				}
				IEnumerator<string> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x060075ED RID: 30189 RVA: 0x001BAAEC File Offset: 0x001B8CEC
			private IEnumerable<string> GetPolicyReferenceUris(NamedItem item, string xPath)
			{
				foreach (string text in WsdlImporter.WsdlPolicyReader.ReadPolicyUrisAttribute(item))
				{
					yield return text;
				}
				string[] array = null;
				foreach (string text2 in this.ReadPolicyReferenceElements(item, xPath))
				{
					yield return text2;
				}
				IEnumerator<string> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x060075EE RID: 30190 RVA: 0x001BAB0A File Offset: 0x001B8D0A
			private IEnumerable<string> ReadPolicyReferenceElements(NamedItem item, string xPath)
			{
				List<XmlElement> list = new List<XmlElement>();
				list.AddRange(item.Extensions.FindAll("PolicyReference", "http://schemas.xmlsoap.org/ws/2004/09/policy"));
				list.AddRange(item.Extensions.FindAll("PolicyReference", "http://www.w3.org/ns/ws-policy"));
				foreach (XmlElement xmlElement in list)
				{
					string attribute = xmlElement.GetAttribute("URI");
					if (attribute == null)
					{
						string @string = SR.GetString("PolicyReferenceMissingURI", new object[]
						{
							"URI"
						});
						this.importer.LogImportWarning(@string);
					}
					else if (attribute == string.Empty)
					{
						string string2 = SR.GetString("PolicyReferenceInvalidId");
						this.importer.LogImportWarning(string2);
					}
					else
					{
						yield return attribute;
					}
				}
				List<XmlElement>.Enumerator enumerator = default(List<XmlElement>.Enumerator);
				yield break;
				yield break;
			}

			// Token: 0x060075EF RID: 30191 RVA: 0x001BAB24 File Offset: 0x001B8D24
			private static string[] ReadPolicyUrisAttribute(NamedItem item)
			{
				XmlAttribute[] extensibleAttributes = item.ExtensibleAttributes;
				if (extensibleAttributes != null && extensibleAttributes.Length != 0)
				{
					foreach (XmlAttribute xmlAttribute in extensibleAttributes)
					{
						if (MetadataImporter.PolicyHelper.IsPolicyURIs(xmlAttribute))
						{
							return xmlAttribute.Value.Split(null, StringSplitOptions.RemoveEmptyEntries);
						}
					}
				}
				return WsdlImporter.WsdlPolicyReader.EmptyStringArray;
			}

			// Token: 0x060075F0 RID: 30192 RVA: 0x001BAB70 File Offset: 0x001B8D70
			private static bool HasPolicyAttached(NamedItem item)
			{
				XmlAttribute[] extensibleAttributes = item.ExtensibleAttributes;
				return (extensibleAttributes != null && Array.Exists<XmlAttribute>(extensibleAttributes, new Predicate<XmlAttribute>(MetadataImporter.PolicyHelper.IsPolicyURIs))) || (item.Extensions.Find("PolicyReference", "http://schemas.xmlsoap.org/ws/2004/09/policy") != null || item.Extensions.Find("PolicyReference", "http://www.w3.org/ns/ws-policy") != null) || (item.Extensions.Find("Policy", "http://schemas.xmlsoap.org/ws/2004/09/policy") != null || item.Extensions.Find("Policy", "http://www.w3.org/ns/ws-policy") != null);
			}

			// Token: 0x060075F1 RID: 30193 RVA: 0x001BAC00 File Offset: 0x001B8E00
			internal MetadataImporter.PolicyAlternatives GetPolicyAlternatives(WsdlEndpointConversionContext endpointContext)
			{
				MetadataImporter.PolicyAlternatives policyAlternatives = new MetadataImporter.PolicyAlternatives();
				ServiceDescription serviceDescription = endpointContext.WsdlBinding.ServiceDescription;
				IEnumerable<IEnumerable<XmlElement>> policyAlternatives2 = this.GetPolicyAlternatives(endpointContext.WsdlBinding, serviceDescription);
				if (endpointContext.WsdlPort != null)
				{
					IEnumerable<IEnumerable<XmlElement>> policyAlternatives3 = this.GetPolicyAlternatives(endpointContext.WsdlPort, endpointContext.WsdlPort.Service.ServiceDescription);
					policyAlternatives.EndpointAlternatives = MetadataImporter.PolicyHelper.CrossProduct<XmlElement>(policyAlternatives2, policyAlternatives3, new MetadataImporter.YieldLimiter(this.importer.Quotas.MaxYields, this.importer));
				}
				else
				{
					policyAlternatives.EndpointAlternatives = policyAlternatives2;
				}
				policyAlternatives.OperationBindingAlternatives = new Dictionary<OperationDescription, IEnumerable<IEnumerable<XmlElement>>>(endpointContext.Endpoint.Contract.Operations.Count);
				policyAlternatives.MessageBindingAlternatives = new Dictionary<MessageDescription, IEnumerable<IEnumerable<XmlElement>>>();
				policyAlternatives.FaultBindingAlternatives = new Dictionary<FaultDescription, IEnumerable<IEnumerable<XmlElement>>>();
				foreach (OperationDescription operationDescription in endpointContext.Endpoint.Contract.Operations)
				{
					if (WsdlExporter.OperationIsExportable(operationDescription))
					{
						OperationBinding operationBinding = endpointContext.GetOperationBinding(operationDescription);
						try
						{
							IEnumerable<IEnumerable<XmlElement>> policyAlternatives4 = this.GetPolicyAlternatives(operationBinding, serviceDescription);
							policyAlternatives.OperationBindingAlternatives.Add(operationDescription, policyAlternatives4);
							foreach (MessageDescription message in operationDescription.Messages)
							{
								MessageBinding messageBinding = endpointContext.GetMessageBinding(message);
								this.CreateMessageBindingAlternatives(policyAlternatives, serviceDescription, message, messageBinding);
							}
							foreach (FaultDescription fault in operationDescription.Faults)
							{
								FaultBinding faultBinding = endpointContext.GetFaultBinding(fault);
								this.CreateFaultBindingAlternatives(policyAlternatives, serviceDescription, fault, faultBinding);
							}
						}
						catch (Exception ex)
						{
							if (Fx.IsFatal(ex))
							{
								throw;
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsdlImporter.WsdlImportException.Create(operationBinding, ex));
						}
					}
				}
				return policyAlternatives;
			}

			// Token: 0x060075F2 RID: 30194 RVA: 0x001BAE0C File Offset: 0x001B900C
			private void CreateMessageBindingAlternatives(MetadataImporter.PolicyAlternatives policyAlternatives, ServiceDescription bindingWsdl, MessageDescription message, MessageBinding wsdlMessageBinding)
			{
				try
				{
					IEnumerable<IEnumerable<XmlElement>> policyAlternatives2 = this.GetPolicyAlternatives(wsdlMessageBinding, bindingWsdl);
					policyAlternatives.MessageBindingAlternatives.Add(message, policyAlternatives2);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsdlImporter.WsdlImportException.Create(wsdlMessageBinding, ex));
				}
			}

			// Token: 0x060075F3 RID: 30195 RVA: 0x001BAE60 File Offset: 0x001B9060
			private void CreateFaultBindingAlternatives(MetadataImporter.PolicyAlternatives policyAlternatives, ServiceDescription bindingWsdl, FaultDescription fault, FaultBinding wsdlFaultBinding)
			{
				try
				{
					IEnumerable<IEnumerable<XmlElement>> policyAlternatives2 = this.GetPolicyAlternatives(wsdlFaultBinding, bindingWsdl);
					policyAlternatives.FaultBindingAlternatives.Add(fault, policyAlternatives2);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsdlImporter.WsdlImportException.Create(wsdlFaultBinding, ex));
				}
			}

			// Token: 0x060075F4 RID: 30196 RVA: 0x001BAEB4 File Offset: 0x001B90B4
			internal XmlElement ResolvePolicyReference(string policyReference, XmlElement contextPolicyAssertion)
			{
				return this.policyDictionary.ResolvePolicyReference(policyReference, contextPolicyAssertion);
			}

			// Token: 0x04004291 RID: 17041
			private WsdlImporter importer;

			// Token: 0x04004292 RID: 17042
			private WsdlImporter.WsdlPolicyReader.WsdlPolicyDictionary policyDictionary;

			// Token: 0x04004293 RID: 17043
			private static readonly string[] EmptyStringArray = new string[0];

			// Token: 0x02000F2A RID: 3882
			private class WsdlPolicyDictionary
			{
				// Token: 0x17001D7B RID: 7547
				// (get) Token: 0x06008659 RID: 34393 RVA: 0x001F1ED0 File Offset: 0x001F00D0
				internal Dictionary<XmlElement, ServiceDescription> PolicySourceTable
				{
					get
					{
						return this.policySourceTable;
					}
				}

				// Token: 0x0600865A RID: 34394 RVA: 0x001F1ED8 File Offset: 0x001F00D8
				internal WsdlPolicyDictionary(WsdlImporter importer)
				{
					this.importer = importer;
					foreach (object obj in importer.wsdlDocuments)
					{
						ServiceDescription serviceDescription = (ServiceDescription)obj;
						foreach (XmlElement element in WsdlImporter.WsdlPolicyReader.GetEmbeddedPolicy(serviceDescription))
						{
							this.AddEmbeddedPolicy(importer, serviceDescription, element);
						}
					}
					foreach (KeyValuePair<string, XmlElement> policyDocument in importer.policyDocuments)
					{
						this.AddExternalPolicy(importer, policyDocument);
					}
				}

				// Token: 0x0600865B RID: 34395 RVA: 0x001F1FE0 File Offset: 0x001F01E0
				private void AddEmbeddedPolicy(WsdlImporter importer, ServiceDescription wsdl, XmlElement element)
				{
					string fragmentIdentifier = WsdlImporter.WsdlPolicyReader.WsdlPolicyDictionary.GetFragmentIdentifier(element);
					if (string.IsNullOrEmpty(fragmentIdentifier))
					{
						string text = WsdlImporter.CreateXPathString(wsdl);
						string @string = SR.GetString("PolicyInWsdlMustHaveFragmentId", new object[]
						{
							text
						});
						importer.LogImportWarning(@string);
						return;
					}
					Dictionary<string, XmlElement> dictionary;
					if (!this.embeddedPolicyDictionary.TryGetValue(wsdl, out dictionary))
					{
						dictionary = new Dictionary<string, XmlElement>();
						this.embeddedPolicyDictionary.Add(wsdl, dictionary);
					}
					else if (dictionary.ContainsKey(fragmentIdentifier))
					{
						string text2 = WsdlImporter.WsdlPolicyReader.WsdlPolicyDictionary.CreateIdXPath(wsdl, element, fragmentIdentifier);
						string string2 = SR.GetString("DuplicatePolicyInWsdlSkipped", new object[]
						{
							text2
						});
						importer.LogImportWarning(string2);
						return;
					}
					dictionary.Add(fragmentIdentifier, element);
					this.policySourceTable.Add(element, wsdl);
				}

				// Token: 0x0600865C RID: 34396 RVA: 0x001F2090 File Offset: 0x001F0290
				private void AddExternalPolicy(WsdlImporter importer, KeyValuePair<string, XmlElement> policyDocument)
				{
					if (policyDocument.Value.NamespaceURI != "http://schemas.xmlsoap.org/ws/2004/09/policy" && policyDocument.Value.NamespaceURI != "http://www.w3.org/ns/ws-policy")
					{
						string @string = SR.GetString("UnrecognizedPolicyDocumentNamespace", new object[]
						{
							policyDocument.Value.NamespaceURI
						});
						importer.LogImportWarning(@string);
						return;
					}
					if (MetadataImporter.PolicyHelper.GetNodeType(policyDocument.Value) != MetadataImporter.PolicyHelper.NodeType.Policy)
					{
						string string2 = SR.GetString("UnsupportedPolicyDocumentRoot", new object[]
						{
							policyDocument.Value.Name
						});
						importer.LogImportWarning(string2);
						return;
					}
					string text = WsdlImporter.WsdlPolicyReader.WsdlPolicyDictionary.CreateKeyFromPolicy(policyDocument.Key, policyDocument.Value);
					if (this.externalPolicyDictionary.ContainsKey(text))
					{
						string string3 = SR.GetString("DuplicatePolicyDocumentSkipped", new object[]
						{
							text
						});
						importer.LogImportWarning(string3);
						return;
					}
					this.externalPolicyDictionary.Add(text, policyDocument.Value);
				}

				// Token: 0x0600865D RID: 34397 RVA: 0x001F2180 File Offset: 0x001F0380
				internal XmlElement ResolvePolicyReference(string policyReference, XmlElement contextPolicyAssertion)
				{
					if (policyReference[0] != '#')
					{
						XmlElement result;
						this.externalPolicyDictionary.TryGetValue(policyReference, out result);
						return result;
					}
					if (contextPolicyAssertion == null)
					{
						return null;
					}
					ServiceDescription wsdlDocument;
					if (!this.policySourceTable.TryGetValue(contextPolicyAssertion, out wsdlDocument))
					{
						return null;
					}
					return this.ResolvePolicyReference(policyReference, wsdlDocument);
				}

				// Token: 0x0600865E RID: 34398 RVA: 0x001F21C8 File Offset: 0x001F03C8
				internal XmlElement ResolvePolicyReference(string policyReference, ServiceDescription wsdlDocument)
				{
					XmlElement result;
					if (policyReference[0] != '#')
					{
						this.externalPolicyDictionary.TryGetValue(policyReference, out result);
						return result;
					}
					Dictionary<string, XmlElement> dictionary;
					if (!this.embeddedPolicyDictionary.TryGetValue(wsdlDocument, out dictionary))
					{
						return null;
					}
					dictionary.TryGetValue(policyReference, out result);
					return result;
				}

				// Token: 0x0600865F RID: 34399 RVA: 0x001F2210 File Offset: 0x001F0410
				private static string CreateKeyFromPolicy(string identifier, XmlElement policyElement)
				{
					string fragmentIdentifier = WsdlImporter.WsdlPolicyReader.WsdlPolicyDictionary.GetFragmentIdentifier(policyElement);
					return string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
					{
						identifier,
						fragmentIdentifier
					});
				}

				// Token: 0x06008660 RID: 34400 RVA: 0x001F2243 File Offset: 0x001F0443
				private static string GetFragmentIdentifier(XmlElement element)
				{
					return MetadataImporter.PolicyHelper.GetFragmentIdentifier(element);
				}

				// Token: 0x06008661 RID: 34401 RVA: 0x001F224C File Offset: 0x001F044C
				internal string CreateIdXPath(XmlElement policyAssertion)
				{
					ServiceDescription wsdl;
					if (!this.policySourceTable.TryGetValue(policyAssertion, out wsdl))
					{
						return null;
					}
					string fragmentIdentifier = WsdlImporter.WsdlPolicyReader.WsdlPolicyDictionary.GetFragmentIdentifier(policyAssertion);
					if (string.IsNullOrEmpty(fragmentIdentifier))
					{
						return null;
					}
					return WsdlImporter.WsdlPolicyReader.WsdlPolicyDictionary.CreateIdXPath(wsdl, policyAssertion, fragmentIdentifier);
				}

				// Token: 0x06008662 RID: 34402 RVA: 0x001F2284 File Offset: 0x001F0484
				internal static string CreateIdXPath(ServiceDescription wsdl, XmlElement element, string key)
				{
					string text = WsdlImporter.CreateXPathString(wsdl);
					string text2;
					if (element.HasAttribute("Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"))
					{
						text2 = WsdlImporter.WsdlPolicyReader.WsdlPolicyDictionary.wsuId;
					}
					else
					{
						if (!element.HasAttribute("id", "http://www.w3.org/XML/1998/namespace"))
						{
							return null;
						}
						text2 = WsdlImporter.WsdlPolicyReader.WsdlPolicyDictionary.xmlId;
					}
					return string.Format(CultureInfo.InvariantCulture, "{0}/{1}/[@{2}='{3}']", new object[]
					{
						text,
						WsdlImporter.WsdlPolicyReader.WsdlPolicyDictionary.wspPolicy,
						text2,
						key
					});
				}

				// Token: 0x04004DF5 RID: 19957
				private readonly MetadataImporter importer;

				// Token: 0x04004DF6 RID: 19958
				private readonly Dictionary<ServiceDescription, Dictionary<string, XmlElement>> embeddedPolicyDictionary = new Dictionary<ServiceDescription, Dictionary<string, XmlElement>>();

				// Token: 0x04004DF7 RID: 19959
				private readonly Dictionary<string, XmlElement> externalPolicyDictionary = new Dictionary<string, XmlElement>();

				// Token: 0x04004DF8 RID: 19960
				private readonly Dictionary<XmlElement, ServiceDescription> policySourceTable = new Dictionary<XmlElement, ServiceDescription>();

				// Token: 0x04004DF9 RID: 19961
				private static readonly string wspPolicy = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
				{
					"wsp",
					"Policy"
				});

				// Token: 0x04004DFA RID: 19962
				private static readonly string xmlId = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
				{
					"xml",
					"id"
				});

				// Token: 0x04004DFB RID: 19963
				private static readonly string wsuId = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
				{
					"wsu",
					"Id"
				});
			}
		}

		// Token: 0x02000BF9 RID: 3065
		private enum ErrorBehavior
		{
			// Token: 0x04004295 RID: 17045
			RethrowExceptions,
			// Token: 0x04004296 RID: 17046
			DoNotThrowExceptions
		}

		// Token: 0x02000BFA RID: 3066
		private enum WsdlPortTypeImportOptions
		{
			// Token: 0x04004298 RID: 17048
			ReuseExistingContracts,
			// Token: 0x04004299 RID: 17049
			IgnoreExistingContracts
		}
	}
}
