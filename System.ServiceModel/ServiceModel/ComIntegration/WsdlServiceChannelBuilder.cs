using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime;
using System.Runtime.InteropServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Threading;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000278 RID: 632
	internal class WsdlServiceChannelBuilder : IProxyCreator, IDisposable, IProvideChannelBuilderSettings
	{
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x000422FC File Offset: 0x000404FC
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

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x0004232B File Offset: 0x0004052B
		ServiceChannel IProvideChannelBuilderSettings.ServiceChannel
		{
			get
			{
				return this.CreateChannel();
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x00042333 File Offset: 0x00040533
		ServiceChannelFactory IProvideChannelBuilderSettings.ServiceChannelFactoryReadOnly
		{
			get
			{
				return this.serviceChannelFactory;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x0004233B File Offset: 0x0004053B
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

		// Token: 0x06001204 RID: 4612 RVA: 0x0004236A File Offset: 0x0004056A
		void IDisposable.Dispose()
		{
			if (this.serviceChannel != null)
			{
				this.serviceChannel.Close();
			}
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0004237F File Offset: 0x0004057F
		internal WsdlServiceChannelBuilder(Dictionary<MonikerHelper.MonikerAttribute, string> propertyTable)
		{
			this.propertyTable = propertyTable;
			this.ProcessWsdl();
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x000423A0 File Offset: 0x000405A0
		private ServiceChannel CreateChannel()
		{
			Thread.MemoryBarrier();
			if (this.serviceChannel == null)
			{
				lock (this)
				{
					Thread.MemoryBarrier();
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
							this.serviceChannel = this.serviceChannelFactory.CreateServiceChannel(new EndpointAddress(this.serviceEndpoint.Address.Uri, this.serviceEndpoint.Address.Identity, this.serviceEndpoint.Address.Headers), this.serviceEndpoint.Address.Uri);
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

		// Token: 0x06001207 RID: 4615 RVA: 0x00042514 File Offset: 0x00040714
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

		// Token: 0x06001208 RID: 4616 RVA: 0x00042548 File Offset: 0x00040748
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

		// Token: 0x06001209 RID: 4617 RVA: 0x000425C0 File Offset: 0x000407C0
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

		// Token: 0x0600120A RID: 4618 RVA: 0x00042674 File Offset: 0x00040874
		private void ProcessWsdl()
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			EndpointIdentity identity = null;
			string text4 = null;
			string text5 = null;
			string text6 = null;
			string text7;
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.Wsdl, out text7);
			string text8;
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.Contract, out text8);
			string text9;
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.Binding, out text9);
			string text10;
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.Address, out text10);
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.SpnIdentity, out text);
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.UpnIdentity, out text2);
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.DnsIdentity, out text3);
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.Serializer, out text4);
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.BindingNamespace, out text6);
			this.propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.ContractNamespace, out text5);
			if (string.IsNullOrEmpty(text7))
			{
				throw Fx.AssertAndThrow("Wsdl should not be null at this point");
			}
			if (string.IsNullOrEmpty(text8) || string.IsNullOrEmpty(text9) || string.IsNullOrEmpty(text10))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("ContractBindingAddressCannotBeNull")));
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
			bool flag = false;
			if (!string.IsNullOrEmpty(text4))
			{
				if ("xml" != text4 && "datacontract" != text4)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerIncorectSerializer")));
				}
				if ("xml" == text4)
				{
					this.useXmlSerializer = true;
				}
				else
				{
					flag = true;
				}
			}
			TextReader textReader = new StringReader(text7);
			try
			{
				System.Web.Services.Description.ServiceDescription serviceDescription = System.Web.Services.Description.ServiceDescription.Read(textReader);
				if (string.IsNullOrEmpty(text5))
				{
					text5 = serviceDescription.TargetNamespace;
				}
				if (string.IsNullOrEmpty(text6))
				{
					text6 = serviceDescription.TargetNamespace;
				}
				ServiceDescriptionCollection serviceDescriptionCollection = new ServiceDescriptionCollection();
				serviceDescriptionCollection.Add(serviceDescription);
				XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
				foreach (object obj in serviceDescription.Types.Schemas)
				{
					XmlSchema schema = (XmlSchema)obj;
					xmlSchemaSet.Add(schema);
				}
				MetadataSet metadataSet = new MetadataSet(WsdlImporter.CreateMetadataDocuments(serviceDescriptionCollection, xmlSchemaSet, null));
				WsdlImporter wsdlImporter;
				if (this.useXmlSerializer)
				{
					wsdlImporter = this.CreateXmlSerializerImporter(metadataSet);
				}
				else if (flag)
				{
					wsdlImporter = this.CreateDataContractSerializerImporter(metadataSet);
				}
				else
				{
					wsdlImporter = new WsdlImporter(metadataSet);
				}
				XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(text8, text5);
				XmlQualifiedName xmlQualifiedName2 = new XmlQualifiedName(text9, text6);
				PortType portType = serviceDescriptionCollection.GetPortType(xmlQualifiedName);
				this.contractDescription = wsdlImporter.ImportContract(portType);
				System.Web.Services.Description.Binding binding = serviceDescriptionCollection.GetBinding(xmlQualifiedName2);
				System.ServiceModel.Channels.Binding binding2 = wsdlImporter.ImportBinding(binding);
				EndpointAddress address = new EndpointAddress(new Uri(text10), identity, null);
				this.serviceEndpoint = new ServiceEndpoint(this.contractDescription, binding2, address);
				ComPlusWsdlChannelBuilderTrace.Trace(TraceEventType.Verbose, 327709, "TraceCodeComIntegrationWsdlChannelBuilderLoaded", xmlQualifiedName2, xmlQualifiedName, serviceDescription, this.contractDescription, binding2, serviceDescription.Types.Schemas);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("FailedImportOfWsdl", new object[]
				{
					ex.Message
				})));
			}
			finally
			{
				IDisposable disposable2 = textReader;
				disposable2.Dispose();
			}
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00042A94 File Offset: 0x00040C94
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

		// Token: 0x0600120C RID: 4620 RVA: 0x00042AF0 File Offset: 0x00040CF0
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

		// Token: 0x0600120D RID: 4621 RVA: 0x00042B4C File Offset: 0x00040D4C
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

		// Token: 0x0600120E RID: 4622 RVA: 0x00042BBF File Offset: 0x00040DBF
		bool IProxyCreator.SupportsErrorInfo(ref Guid riid)
		{
			return !(riid != InterfaceID.idIDispatch);
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00042BD6 File Offset: 0x00040DD6
		bool IProxyCreator.SupportsDispatch()
		{
			return true;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00042BD9 File Offset: 0x00040DD9
		bool IProxyCreator.SupportsIntrinsics()
		{
			return true;
		}

		// Token: 0x040019C6 RID: 6598
		private ContractDescription contractDescription;

		// Token: 0x040019C7 RID: 6599
		private ServiceChannelFactory serviceChannelFactory;

		// Token: 0x040019C8 RID: 6600
		private Dictionary<MonikerHelper.MonikerAttribute, string> propertyTable;

		// Token: 0x040019C9 RID: 6601
		private ServiceChannel serviceChannel;

		// Token: 0x040019CA RID: 6602
		private ServiceEndpoint serviceEndpoint;

		// Token: 0x040019CB RID: 6603
		private KeyedByTypeCollection<IEndpointBehavior> behaviors = new KeyedByTypeCollection<IEndpointBehavior>();

		// Token: 0x040019CC RID: 6604
		private bool useXmlSerializer;
	}
}
