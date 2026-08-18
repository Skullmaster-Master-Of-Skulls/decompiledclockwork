using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ServiceModel.Description;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007A8 RID: 1960
	[TypeForwardedFrom("System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class ContextBindingElementImporter : IPolicyImportExtension, IWsdlImportExtension
	{
		// Token: 0x06004A34 RID: 18996 RVA: 0x00110CF8 File Offset: 0x0010EEF8
		public void BeforeImport(ServiceDescriptionCollection wsdlDocuments, XmlSchemaSet xmlSchemas, ICollection<XmlElement> policy)
		{
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x00110CFA File Offset: 0x0010EEFA
		public void ImportContract(WsdlImporter importer, WsdlContractConversionContext context)
		{
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x00110CFC File Offset: 0x0010EEFC
		public void ImportEndpoint(WsdlImporter importer, WsdlEndpointConversionContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (context.Endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context.Endpoint");
			}
			if (context.Endpoint.Binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context.Endpoint.Binding");
			}
			CustomBinding customBinding = context.Endpoint.Binding as CustomBinding;
			if (customBinding != null)
			{
				UnrecognizedAssertionsBindingElement unrecognizedAssertionsBindingElement = customBinding.Elements.Find<UnrecognizedAssertionsBindingElement>();
				HttpTransportBindingElement httpTransportBindingElement = null;
				if (unrecognizedAssertionsBindingElement != null)
				{
					XmlElement item = null;
					if (ContextBindingElementPolicy.TryGetHttpUseCookieAssertion(unrecognizedAssertionsBindingElement.BindingAsserions, out item))
					{
						foreach (BindingElement bindingElement in customBinding.Elements)
						{
							httpTransportBindingElement = (bindingElement as HttpTransportBindingElement);
							if (httpTransportBindingElement != null)
							{
								httpTransportBindingElement.AllowCookies = true;
								unrecognizedAssertionsBindingElement.BindingAsserions.Remove(item);
								if (unrecognizedAssertionsBindingElement.BindingAsserions.Count == 0)
								{
									customBinding.Elements.Remove(unrecognizedAssertionsBindingElement);
									break;
								}
								break;
							}
						}
					}
				}
				BindingElementCollection bindingElementCollection = customBinding.CreateBindingElements();
				Binding binding;
				if (!WSHttpContextBinding.TryCreate(bindingElementCollection, out binding) && !NetTcpContextBinding.TryCreate(bindingElementCollection, out binding))
				{
					if (httpTransportBindingElement == null)
					{
						foreach (BindingElement bindingElement2 in bindingElementCollection)
						{
							httpTransportBindingElement = (bindingElement2 as HttpTransportBindingElement);
							if (httpTransportBindingElement != null)
							{
								break;
							}
						}
					}
					if (httpTransportBindingElement != null && httpTransportBindingElement.AllowCookies)
					{
						httpTransportBindingElement.AllowCookies = false;
						if (BasicHttpBinding.TryCreate(bindingElementCollection, out binding))
						{
							((BasicHttpBinding)binding).AllowCookies = true;
						}
					}
				}
				if (binding != null)
				{
					binding.Name = context.Endpoint.Binding.Name;
					binding.Namespace = context.Endpoint.Binding.Namespace;
					context.Endpoint.Binding = binding;
				}
			}
		}

		// Token: 0x06004A37 RID: 18999 RVA: 0x00110ED4 File Offset: 0x0010F0D4
		public virtual void ImportPolicy(MetadataImporter importer, PolicyConversionContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (context.BindingElements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PolicyImportContextBindingElementCollectionIsNull")));
			}
			XmlElement item = null;
			ContextBindingElement item2;
			if (ContextBindingElementPolicy.TryImportRequireContextAssertion(context.GetBindingAssertions(), out item2))
			{
				context.BindingElements.Insert(0, item2);
				return;
			}
			if (ContextBindingElementPolicy.TryGetHttpUseCookieAssertion(context.GetBindingAssertions(), out item))
			{
				foreach (BindingElement bindingElement in context.BindingElements)
				{
					HttpTransportBindingElement httpTransportBindingElement = bindingElement as HttpTransportBindingElement;
					if (httpTransportBindingElement != null)
					{
						httpTransportBindingElement.AllowCookies = true;
						context.GetBindingAssertions().Remove(item);
						break;
					}
				}
			}
		}
	}
}
