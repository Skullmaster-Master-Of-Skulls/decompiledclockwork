using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008AB RID: 2219
	public class TransportBindingElementImporter : IWsdlImportExtension, IPolicyImportExtension
	{
		// Token: 0x060054A6 RID: 21670 RVA: 0x0013782B File Offset: 0x00135A2B
		void IWsdlImportExtension.BeforeImport(ServiceDescriptionCollection wsdlDocuments, XmlSchemaSet xmlSchemas, ICollection<XmlElement> policy)
		{
			WsdlImporter.SoapInPolicyWorkaroundHelper.InsertAdHocTransportPolicy(wsdlDocuments);
		}

		// Token: 0x060054A7 RID: 21671 RVA: 0x00137833 File Offset: 0x00135A33
		void IWsdlImportExtension.ImportContract(WsdlImporter importer, WsdlContractConversionContext context)
		{
		}

		// Token: 0x060054A8 RID: 21672 RVA: 0x00137838 File Offset: 0x00135A38
		void IWsdlImportExtension.ImportEndpoint(WsdlImporter importer, WsdlEndpointConversionContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (context.Endpoint.Binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context.Endpoint.Binding");
			}
			TransportBindingElement transportBindingElement = TransportBindingElementImporter.GetBindingElements(context).Find<TransportBindingElement>();
			bool flag = transportBindingElement != null && !StateHelper.IsRegisteredTransportBindingElement(importer, context);
			if (flag)
			{
				return;
			}
			SoapBinding soapBinding = (SoapBinding)context.WsdlBinding.Extensions.Find(typeof(SoapBinding));
			if (soapBinding != null && transportBindingElement == null)
			{
				TransportBindingElementImporter.CreateLegacyTransportBindingElement(importer, soapBinding, context);
			}
			if (context.WsdlPort != null)
			{
				TransportBindingElementImporter.ImportAddress(context, transportBindingElement);
			}
		}

		// Token: 0x060054A9 RID: 21673 RVA: 0x001378D4 File Offset: 0x00135AD4
		private static BindingElementCollection GetBindingElements(WsdlEndpointConversionContext context)
		{
			Binding binding = context.Endpoint.Binding;
			return (binding is CustomBinding) ? ((CustomBinding)binding).Elements : binding.CreateBindingElements();
		}

		// Token: 0x060054AA RID: 21674 RVA: 0x0013790C File Offset: 0x00135B0C
		private static CustomBinding ConvertToCustomBinding(WsdlEndpointConversionContext context)
		{
			CustomBinding customBinding = context.Endpoint.Binding as CustomBinding;
			if (customBinding == null)
			{
				customBinding = new CustomBinding(context.Endpoint.Binding);
				context.Endpoint.Binding = customBinding;
			}
			return customBinding;
		}

		// Token: 0x060054AB RID: 21675 RVA: 0x0013794C File Offset: 0x00135B4C
		private static void ImportAddress(WsdlEndpointConversionContext context, TransportBindingElement transportBindingElement)
		{
			EndpointAddress endpointAddress = context.Endpoint.Address = WsdlImporter.WSAddressingHelper.ImportAddress(context.WsdlPort);
			if (endpointAddress != null)
			{
				context.Endpoint.Address = endpointAddress;
				if (endpointAddress.Uri.Scheme == Uri.UriSchemeHttps && transportBindingElement is HttpTransportBindingElement && !(transportBindingElement is HttpsTransportBindingElement))
				{
					BindingElementCollection elements = TransportBindingElementImporter.ConvertToCustomBinding(context).Elements;
					elements.Remove(transportBindingElement);
					elements.Add(TransportBindingElementImporter.CreateHttpsFromHttp(transportBindingElement as HttpTransportBindingElement));
				}
			}
		}

		// Token: 0x060054AC RID: 21676 RVA: 0x001379D4 File Offset: 0x00135BD4
		private static void CreateLegacyTransportBindingElement(WsdlImporter importer, SoapBinding soapBinding, WsdlEndpointConversionContext context)
		{
			TransportBindingElement transportBindingElement = TransportBindingElementImporter.CreateTransportBindingElements(soapBinding.Transport, null);
			if (transportBindingElement != null)
			{
				TransportBindingElementImporter.ConvertToCustomBinding(context).Elements.Add(transportBindingElement);
				StateHelper.RegisterTransportBindingElement(importer, context);
			}
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x00137A0C File Offset: 0x00135C0C
		private static HttpsTransportBindingElement CreateHttpsFromHttp(HttpTransportBindingElement http)
		{
			if (http == null)
			{
				return new HttpsTransportBindingElement();
			}
			return HttpsTransportBindingElement.CreateFromHttpBindingElement(http);
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x00137A2C File Offset: 0x00135C2C
		void IPolicyImportExtension.ImportPolicy(MetadataImporter importer, PolicyConversionContext policyContext)
		{
			XmlQualifiedName wsdlBindingQName;
			string text = WsdlImporter.SoapInPolicyWorkaroundHelper.FindAdHocTransportPolicy(policyContext, out wsdlBindingQName);
			if (text != null && !policyContext.BindingElements.Contains(typeof(TransportBindingElement)))
			{
				TransportBindingElement transportBindingElement = TransportBindingElementImporter.CreateTransportBindingElements(text, policyContext);
				if (transportBindingElement != null)
				{
					ITransportPolicyImport transportPolicyImport = transportBindingElement as ITransportPolicyImport;
					if (transportPolicyImport != null)
					{
						transportPolicyImport.ImportPolicy(importer, policyContext);
					}
					policyContext.BindingElements.Add(transportBindingElement);
					StateHelper.RegisterTransportBindingElement(importer, wsdlBindingQName);
				}
			}
		}

		// Token: 0x060054AF RID: 21679 RVA: 0x00137A8C File Offset: 0x00135C8C
		private static TransportBindingElement CreateTransportBindingElements(string transportUri, PolicyConversionContext policyContext)
		{
			TransportBindingElement result = null;
			if (!(transportUri == "http://schemas.xmlsoap.org/soap/http"))
			{
				if (!(transportUri == "http://schemas.microsoft.com/soap/tcp"))
				{
					if (!(transportUri == "http://schemas.microsoft.com/soap/named-pipe"))
					{
						if (!(transportUri == "http://schemas.microsoft.com/soap/msmq"))
						{
							if (!(transportUri == "http://schemas.microsoft.com/soap/peer"))
							{
								if (transportUri == "http://schemas.microsoft.com/soap/websocket")
								{
									HttpTransportBindingElement httpTransportBindingElement = TransportBindingElementImporter.GetHttpTransportBindingElement(policyContext);
									httpTransportBindingElement.WebSocketSettings.TransportUsage = WebSocketTransportUsage.Always;
									httpTransportBindingElement.WebSocketSettings.SubProtocol = "soap";
									result = httpTransportBindingElement;
								}
							}
							else
							{
								result = new PeerTransportBindingElement();
							}
						}
						else
						{
							result = new MsmqTransportBindingElement();
						}
					}
					else
					{
						result = new NamedPipeTransportBindingElement();
					}
				}
				else
				{
					result = new TcpTransportBindingElement();
				}
			}
			else
			{
				result = TransportBindingElementImporter.GetHttpTransportBindingElement(policyContext);
			}
			return result;
		}

		// Token: 0x060054B0 RID: 21680 RVA: 0x00137B3C File Offset: 0x00135D3C
		private static HttpTransportBindingElement GetHttpTransportBindingElement(PolicyConversionContext policyContext)
		{
			if (policyContext != null)
			{
				WSSecurityPolicy wssecurityPolicy = null;
				ICollection<XmlElement> bindingAssertions = policyContext.GetBindingAssertions();
				if (WSSecurityPolicy.TryGetSecurityPolicyDriver(bindingAssertions, out wssecurityPolicy) && wssecurityPolicy.ContainsWsspHttpsTokenAssertion(bindingAssertions))
				{
					return new HttpsTransportBindingElement
					{
						MessageSecurityVersion = wssecurityPolicy.GetSupportedMessageSecurityVersion(SecurityVersion.WSSecurity11)
					};
				}
			}
			return new HttpTransportBindingElement();
		}
	}
}
