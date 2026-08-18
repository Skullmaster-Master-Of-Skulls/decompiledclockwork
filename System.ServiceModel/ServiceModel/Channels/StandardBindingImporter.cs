using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009A1 RID: 2465
	public class StandardBindingImporter : IWsdlImportExtension
	{
		// Token: 0x060060C8 RID: 24776 RVA: 0x00169CDC File Offset: 0x00167EDC
		void IWsdlImportExtension.BeforeImport(ServiceDescriptionCollection wsdlDocuments, XmlSchemaSet xmlSchemas, ICollection<XmlElement> policy)
		{
		}

		// Token: 0x060060C9 RID: 24777 RVA: 0x00169CDE File Offset: 0x00167EDE
		void IWsdlImportExtension.ImportContract(WsdlImporter importer, WsdlContractConversionContext context)
		{
		}

		// Token: 0x060060CA RID: 24778 RVA: 0x00169CE0 File Offset: 0x00167EE0
		void IWsdlImportExtension.ImportEndpoint(WsdlImporter importer, WsdlEndpointConversionContext endpointContext)
		{
			if (endpointContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointContext");
			}
			if (endpointContext.Endpoint.Binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointContext.Binding");
			}
			if (endpointContext.Endpoint.Binding is CustomBinding)
			{
				BindingElementCollection elements = ((CustomBinding)endpointContext.Endpoint.Binding).Elements;
				TransportBindingElement transportBindingElement = elements.Find<TransportBindingElement>();
				if (transportBindingElement is HttpTransportBindingElement)
				{
					Binding binding;
					if (WSHttpBindingBase.TryCreate(elements, out binding))
					{
						this.SetBinding(endpointContext.Endpoint, binding);
						return;
					}
					if (WSDualHttpBinding.TryCreate(elements, out binding))
					{
						this.SetBinding(endpointContext.Endpoint, binding);
						return;
					}
					if (BasicHttpBinding.TryCreate(elements, out binding))
					{
						this.SetBinding(endpointContext.Endpoint, binding);
						return;
					}
					if (NetHttpBinding.TryCreate(elements, out binding))
					{
						this.SetBinding(endpointContext.Endpoint, binding);
						return;
					}
				}
				else
				{
					Binding binding;
					if (transportBindingElement is MsmqTransportBindingElement && NetMsmqBinding.TryCreate(elements, out binding))
					{
						this.SetBinding(endpointContext.Endpoint, binding);
						return;
					}
					if (transportBindingElement is NamedPipeTransportBindingElement && NetNamedPipeBinding.TryCreate(elements, out binding))
					{
						this.SetBinding(endpointContext.Endpoint, binding);
						return;
					}
					if (transportBindingElement is PeerTransportBindingElement && NetPeerTcpBinding.TryCreate(elements, out binding))
					{
						this.SetBinding(endpointContext.Endpoint, binding);
						return;
					}
					if (transportBindingElement is TcpTransportBindingElement && NetTcpBinding.TryCreate(elements, out binding))
					{
						this.SetBinding(endpointContext.Endpoint, binding);
					}
				}
			}
		}

		// Token: 0x060060CB RID: 24779 RVA: 0x00169E39 File Offset: 0x00168039
		private void SetBinding(ServiceEndpoint endpoint, Binding binding)
		{
			binding.Name = endpoint.Binding.Name;
			binding.Namespace = endpoint.Binding.Namespace;
			endpoint.Binding = binding;
		}
	}
}
