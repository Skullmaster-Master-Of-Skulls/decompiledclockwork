using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.InteropServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web.Services.Description;
using System.Web.Services.Discovery;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000235 RID: 565
	internal class MexServiceChannelBuilder : IProxyCreator, IDisposable, IProvideChannelBuilderSettings
	{
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x0003D270 File Offset: 0x0003B470
		ServiceChannelFactory IProvideChannelBuilderSettings.ServiceChannelFactoryReadWrite
		{
			get
			{
				if (this.serviceChannel != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("TooLate"), HR.RPC_E_TOO_LATE));
				}
				return this.serviceChannelFactory;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x0003D2A1 File Offset: 0x0003B4A1
		ServiceChannel IProvideChannelBuilderSettings.ServiceChannel
		{
			get
			{
				return this.CreateChannel();
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x0003D2A9 File Offset: 0x0003B4A9
		ServiceChannelFactory IProvideChannelBuilderSettings.ServiceChannelFactoryReadOnly
		{
			get
			{
				return this.serviceChannelFactory;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x0003D2B1 File Offset: 0x0003B4B1
		KeyedByTypeCollection<IEndpointBehavior> IProvideChannelBuilderSettings.Behaviors
		{
			get
			{
				if (this.serviceChannel != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("TooLate"), HR.RPC_E_TOO_LATE));
				}
				return this.behaviors;
			}
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0003D2E2 File Offset: 0x0003B4E2
		void IDisposable.Dispose()
		{
			if (this.serviceChannel != null)
			{
				this.serviceChannel.Close();
			}
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0003D2FB File Offset: 0x0003B4FB
		internal MexServiceChannelBuilder(Dictionary<MonikerHelper.MonikerAttribute, string> propertyTable)
		{
			this.propertyTable = propertyTable;
			this.DoMex();
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0003D31C File Offset: 0x0003B51C
		private ServiceChannel CreateChannel()
		{
			if (this.serviceChannel == null)
			{
				lock (this)
				{
					if (this.serviceChannel == null)
					{
						try
						{
							if (this.serviceChannelFactory == null)
							{
								this.FaultInserviceChannelFactory();
							}
							if (this.serviceChannelFactory == null)
							{
								throw Fx.AssertAndThrow("ServiceChannelFactory cannot be null at this point");
							}
							this.serviceChannelFactory.Open();
							if (this.serviceEndpoint == null)
							{
								throw Fx.AssertAndThrow("ServiceEndpoint cannot be null");
							}
							ServiceChannel serviceChannel = this.serviceChannelFactory.CreateServiceChannel(new EndpointAddress(this.serviceEndpoint.Address.Uri, this.serviceEndpoint.Address.Identity, this.serviceEndpoint.Address.Headers), this.serviceEndpoint.Address.Uri);
							this.serviceChannel = serviceChannel;
							ComPlusChannelCreatedTrace.Trace(TraceEventType.Verbose, 327711, "TraceCodeComIntegrationChannelCreated", this.serviceEndpoint.Address.Uri, this.contractDescription.ContractType);
							if (this.serviceChannel == null)
							{
								throw Fx.AssertAndThrow("serviceProxy MUST derive from RealProxy");
							}
						}
						finally
						{
							if (this.serviceChannel == null && this.serviceChannelFactory != null)
							{
								this.serviceChannelFactory.Close();
							}
						}
					}
				}
			}
			return this.serviceChannel;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0003D494 File Offset: 0x0003B694
		private ServiceChannelFactory CreateServiceChannelFactory()
		{
			this.serviceChannelFactory = ServiceChannelFactory.BuildChannelFactory(this.serviceEndpoint);
			if (this.serviceChannelFactory == null)
			{
				throw Fx.AssertAndThrow("We should get a ServiceChannelFactory back");
			}
			this.FixupProxyBehavior();
			return this.serviceChannelFactory;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0003D4C8 File Offset: 0x0003B6C8
		private void FaultInserviceChannelFactory()
		{
			if (this.propertyTable == null)
			{
				throw Fx.AssertAndThrow("PropertyTable should not be null");
			}
			foreach (IEndpointBehavior item in this.behaviors)
			{
				this.serviceEndpoint.Behaviors.Add(item);
			}
			this.serviceChannelFactory = this.CreateServiceChannelFactory();
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0003D540 File Offset: 0x0003B740
		private void FixupProxyBehavior()
		{
			if (this.useXmlSerializer)
			{
				XmlSerializerOperationBehavior.AddBehaviors(this.contractDescription);
			}
			foreach (OperationDescription operationDescription in this.contractDescription.Operations)
			{
				ClientOperation clientOperation = this.serviceChannelFactory.ClientRuntime.Operations[operationDescription.Name];
				clientOperation.SerializeRequest = true;
				clientOperation.DeserializeReply = true;
				if (this.useXmlSerializer)
				{
					clientOperation.Formatter = XmlSerializerOperationBehavior.CreateOperationFormatter(operationDescription);
				}
				else
				{
					clientOperation.Formatter = new DataContractSerializerOperationFormatter(operationDescription, TypeLoader.DefaultDataContractFormatAttribute, null);
				}
			}
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0003D5F4 File Offset: 0x0003B7F4
		private void DoMex()
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			string text6 = null;
			string text7 = null;
			EndpointIdentity identity = null;
			EndpointIdentity endpointIdentity = null;
			string text8;
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.Contract, out text8);
			string ns;
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.ContractNamespace, out ns);
			string text9;
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.BindingNamespace, out text9);
			string text10;
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.Binding, out text10);
			string text11;
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.MexAddress, out text11);
			string text12;
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.MexBinding, out text12);
			string text13;
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.MexBindingConfiguration, out text13);
			string text14;
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.Address, out text14);
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.SpnIdentity, out text);
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.UpnIdentity, out text2);
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.DnsIdentity, out text3);
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.MexSpnIdentity, out text4);
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.MexUpnIdentity, out text5);
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.MexDnsIdentity, out text6);
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.Serializer, out text7);
			if (string.IsNullOrEmpty(text11))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerMexAddressNotSpecified")));
			}
			if (!string.IsNullOrEmpty(text4))
			{
				if (!string.IsNullOrEmpty(text5) || !string.IsNullOrEmpty(text6))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerIncorrectServerIdentityForMex")));
				}
				endpointIdentity = EndpointIdentity.CreateSpnIdentity(text4);
			}
			else if (!string.IsNullOrEmpty(text5))
			{
				if (!string.IsNullOrEmpty(text4) || !string.IsNullOrEmpty(text6))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerIncorrectServerIdentityForMex")));
				}
				endpointIdentity = EndpointIdentity.CreateUpnIdentity(text5);
			}
			else if (!string.IsNullOrEmpty(text6))
			{
				if (!string.IsNullOrEmpty(text4) || !string.IsNullOrEmpty(text5))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerIncorrectServerIdentityForMex")));
				}
				endpointIdentity = EndpointIdentity.CreateDnsIdentity(text6);
			}
			else
			{
				endpointIdentity = null;
			}
			if (string.IsNullOrEmpty(text14))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerAddressNotSpecified")));
			}
			if (string.IsNullOrEmpty(text8))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerContractNotSpecified")));
			}
			if (string.IsNullOrEmpty(text10))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerBindingNotSpecified")));
			}
			if (string.IsNullOrEmpty(text9))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerBindingNamespacetNotSpecified")));
			}
			if (!string.IsNullOrEmpty(text))
			{
				if (!string.IsNullOrEmpty(text2) || !string.IsNullOrEmpty(text3))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerIncorrectServerIdentity")));
				}
				identity = EndpointIdentity.CreateSpnIdentity(text);
			}
			else if (!string.IsNullOrEmpty(text2))
			{
				if (!string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(text3))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerIncorrectServerIdentity")));
				}
				identity = EndpointIdentity.CreateUpnIdentity(text2);
			}
			else if (!string.IsNullOrEmpty(text3))
			{
				if (!string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(text2))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerIncorrectServerIdentity")));
				}
				identity = EndpointIdentity.CreateDnsIdentity(text3);
			}
			else
			{
				identity = null;
			}
			EndpointAddress endpointAddress = new EndpointAddress(new Uri(text11), endpointIdentity, new AddressHeader[0]);
			MetadataExchangeClient metadataExchangeClient;
			if (!string.IsNullOrEmpty(text12))
			{
				System.ServiceModel.Channels.Binding binding = null;
				try
				{
					binding = ConfigLoader.LookupBinding(text12, text13);
				}
				catch (ConfigurationErrorsException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MexBindingNotFoundInConfig", new object[]
					{
						text12
					})));
				}
				if (binding == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MexBindingNotFoundInConfig", new object[]
					{
						text12
					})));
				}
				metadataExchangeClient = new MetadataExchangeClient(binding);
			}
			else
			{
				if (!string.IsNullOrEmpty(text13))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerMexBindingSectionNameNotSpecified")));
				}
				metadataExchangeClient = new MetadataExchangeClient(endpointAddress);
			}
			if (endpointIdentity != null)
			{
				metadataExchangeClient.SoapCredentials.Windows.AllowNtlm = false;
			}
			bool flag = false;
			if (!string.IsNullOrEmpty(text7))
			{
				if ("xml" != text7 && "datacontract" != text7)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerIncorectSerializer")));
				}
				if ("xml" == text7)
				{
					this.useXmlSerializer = true;
				}
				else
				{
					flag = true;
				}
			}
			ServiceEndpoint serviceEndpoint = null;
			ServiceEndpointCollection serviceEndpointCollection = null;
			try
			{
				MetadataSet metadata = metadataExchangeClient.GetMetadata(endpointAddress);
				WsdlImporter importer;
				if (this.useXmlSerializer)
				{
					importer = this.CreateXmlSerializerImporter(metadata);
				}
				else if (flag)
				{
					importer = this.CreateDataContractSerializerImporter(metadata);
				}
				else
				{
					importer = new WsdlImporter(metadata);
				}
				serviceEndpointCollection = this.ImportWsdlPortType(new XmlQualifiedName(text8, ns), importer);
				ComPlusMexChannelBuilderMexCompleteTrace.Trace(TraceEventType.Verbose, 327716, "TraceCodeComIntegrationMexMonikerMetadataExchangeComplete", serviceEndpointCollection);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (MexServiceChannelBuilder.UriSchemeSupportsDisco(endpointAddress.Uri))
				{
					try
					{
						DiscoveryClientProtocol discoveryClientProtocol = new DiscoveryClientProtocol();
						discoveryClientProtocol.UseDefaultCredentials = true;
						discoveryClientProtocol.AllowAutoRedirect = true;
						discoveryClientProtocol.DiscoverAny(endpointAddress.Uri.AbsoluteUri);
						discoveryClientProtocol.ResolveAll();
						MetadataSet metadataSet = new MetadataSet();
						foreach (object document in discoveryClientProtocol.Documents.Values)
						{
							this.AddDocumentToSet(metadataSet, document);
						}
						WsdlImporter importer;
						if (this.useXmlSerializer)
						{
							importer = this.CreateXmlSerializerImporter(metadataSet);
						}
						else if (flag)
						{
							importer = this.CreateDataContractSerializerImporter(metadataSet);
						}
						else
						{
							importer = new WsdlImporter(metadataSet);
						}
						serviceEndpointCollection = this.ImportWsdlPortType(new XmlQualifiedName(text8, ns), importer);
						ComPlusMexChannelBuilderMexCompleteTrace.Trace(TraceEventType.Verbose, 327716, "TraceCodeComIntegrationMexMonikerMetadataExchangeComplete", serviceEndpointCollection);
						goto IL_61E;
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerFailedToDoMexRetrieve", new object[]
						{
							ex2.Message
						})));
					}
					goto IL_5F4;
					IL_61E:
					goto IL_620;
				}
				IL_5F4:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerFailedToDoMexRetrieve", new object[]
				{
					ex.Message
				})));
			}
			IL_620:
			if (serviceEndpointCollection.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerContractNotFoundInRetreivedMex")));
			}
			foreach (ServiceEndpoint serviceEndpoint2 in serviceEndpointCollection)
			{
				System.ServiceModel.Channels.Binding binding2 = serviceEndpoint2.Binding;
				if (binding2.Name == text10 && binding2.Namespace == text9)
				{
					serviceEndpoint = serviceEndpoint2;
					break;
				}
			}
			if (serviceEndpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerNoneOfTheBindingMatchedTheSpecifiedBinding")));
			}
			this.contractDescription = serviceEndpoint.Contract;
			this.serviceEndpoint = new ServiceEndpoint(this.contractDescription, serviceEndpoint.Binding, new EndpointAddress(new Uri(text14), identity, null));
			ComPlusMexChannelBuilderTrace.Trace(TraceEventType.Verbose, 327717, "TraceCodeComIntegrationMexChannelBuilderLoaded", serviceEndpoint.Contract, serviceEndpoint.Binding, text14);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0003DD90 File Offset: 0x0003BF90
		private static bool UriSchemeSupportsDisco(Uri serviceUri)
		{
			return serviceUri.Scheme == Uri.UriSchemeHttp || serviceUri.Scheme == Uri.UriSchemeHttps;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0003DDB8 File Offset: 0x0003BFB8
		private void AddDocumentToSet(MetadataSet metadataSet, object document)
		{
			System.Web.Services.Description.ServiceDescription serviceDescription = document as System.Web.Services.Description.ServiceDescription;
			XmlSchema xmlSchema = document as XmlSchema;
			XmlElement xmlElement = document as XmlElement;
			if (serviceDescription != null)
			{
				metadataSet.MetadataSections.Add(MetadataSection.CreateFromServiceDescription(serviceDescription));
				return;
			}
			if (xmlSchema != null)
			{
				metadataSet.MetadataSections.Add(MetadataSection.CreateFromSchema(xmlSchema));
				return;
			}
			if (xmlElement != null && MetadataSection.IsPolicyElement(xmlElement))
			{
				metadataSet.MetadataSections.Add(MetadataSection.CreateFromPolicy(xmlElement, null));
				return;
			}
			MetadataSection metadataSection = new MetadataSection();
			metadataSection.Metadata = document;
			metadataSet.MetadataSections.Add(metadataSection);
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0003DE3C File Offset: 0x0003C03C
		public WsdlImporter CreateDataContractSerializerImporter(MetadataSet metaData)
		{
			Collection<IWsdlImportExtension> collection = ClientSection.GetSection().Metadata.LoadWsdlImportExtensions();
			for (int i = 0; i < collection.Count; i++)
			{
				if (collection[i].GetType() == typeof(XmlSerializerMessageContractImporter))
				{
					collection.RemoveAt(i);
				}
			}
			return new WsdlImporter(metaData, null, collection);
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0003DE98 File Offset: 0x0003C098
		public WsdlImporter CreateXmlSerializerImporter(MetadataSet metaData)
		{
			Collection<IWsdlImportExtension> collection = ClientSection.GetSection().Metadata.LoadWsdlImportExtensions();
			for (int i = 0; i < collection.Count; i++)
			{
				if (collection[i].GetType() == typeof(DataContractSerializerMessageContractImporter))
				{
					collection.RemoveAt(i);
				}
			}
			return new WsdlImporter(metaData, null, collection);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0003DEF4 File Offset: 0x0003C0F4
		private ServiceEndpointCollection ImportWsdlPortType(XmlQualifiedName portTypeQName, WsdlImporter importer)
		{
			foreach (object obj in importer.WsdlDocuments)
			{
				System.Web.Services.Description.ServiceDescription serviceDescription = (System.Web.Services.Description.ServiceDescription)obj;
				if (serviceDescription.TargetNamespace == portTypeQName.Namespace)
				{
					PortType portType = serviceDescription.PortTypes[portTypeQName.Name];
					if (portType != null)
					{
						return importer.ImportEndpoints(portType);
					}
				}
			}
			return new ServiceEndpointCollection();
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0003DF88 File Offset: 0x0003C188
		ComProxy IProxyCreator.CreateProxy(IntPtr outer, ref Guid riid)
		{
			IntPtr zero = IntPtr.Zero;
			if (riid != InterfaceID.idIDispatch)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidCastException(SR.GetString("NoInterface", new object[]
				{
					riid
				})));
			}
			if (this.contractDescription == null)
			{
				throw Fx.AssertAndThrow("ContractDescription should not be null at this point");
			}
			return DispatchProxy.Create(outer, this.contractDescription, this);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0003DFFB File Offset: 0x0003C1FB
		bool IProxyCreator.SupportsErrorInfo(ref Guid riid)
		{
			return !(riid != InterfaceID.idIDispatch);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0003E012 File Offset: 0x0003C212
		bool IProxyCreator.SupportsDispatch()
		{
			return true;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0003E015 File Offset: 0x0003C215
		bool IProxyCreator.SupportsIntrinsics()
		{
			return true;
		}

		// Token: 0x0400188B RID: 6283
		private ContractDescription contractDescription;

		// Token: 0x0400188C RID: 6284
		private ServiceChannelFactory serviceChannelFactory;

		// Token: 0x0400188D RID: 6285
		private Dictionary<MonikerHelper.MonikerAttribute, string> propertyTable;

		// Token: 0x0400188E RID: 6286
		private volatile ServiceChannel serviceChannel;

		// Token: 0x0400188F RID: 6287
		private ServiceEndpoint serviceEndpoint;

		// Token: 0x04001890 RID: 6288
		private KeyedByTypeCollection<IEndpointBehavior> behaviors = new KeyedByTypeCollection<IEndpointBehavior>();

		// Token: 0x04001891 RID: 6289
		private bool useXmlSerializer;
	}
}
