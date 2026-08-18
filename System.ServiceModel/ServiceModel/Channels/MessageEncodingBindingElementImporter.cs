using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009EA RID: 2538
	public class MessageEncodingBindingElementImporter : IWsdlImportExtension, IPolicyImportExtension
	{
		// Token: 0x06006483 RID: 25731 RVA: 0x00176ED5 File Offset: 0x001750D5
		void IWsdlImportExtension.BeforeImport(ServiceDescriptionCollection wsdlDocuments, XmlSchemaSet xmlSchemas, ICollection<XmlElement> policy)
		{
		}

		// Token: 0x06006484 RID: 25732 RVA: 0x00176ED7 File Offset: 0x001750D7
		void IWsdlImportExtension.ImportContract(WsdlImporter importer, WsdlContractConversionContext context)
		{
		}

		// Token: 0x06006485 RID: 25733 RVA: 0x00176EDC File Offset: 0x001750DC
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
			BindingElementCollection bindingElements = MessageEncodingBindingElementImporter.GetBindingElements(context);
			MessageEncodingBindingElement messageEncodingBindingElement = bindingElements.Find<MessageEncodingBindingElement>();
			TextMessageEncodingBindingElement textMessageEncodingBindingElement = messageEncodingBindingElement as TextMessageEncodingBindingElement;
			if (messageEncodingBindingElement != null)
			{
				Type type = messageEncodingBindingElement.GetType();
				if (type != typeof(TextMessageEncodingBindingElement) && type != typeof(BinaryMessageEncodingBindingElement) && type != typeof(MtomMessageEncodingBindingElement))
				{
					return;
				}
			}
			MessageEncodingBindingElementImporter.EnsureMessageEncoding(context, messageEncodingBindingElement);
			foreach (object obj in context.WsdlBinding.Operations)
			{
				OperationBinding operationBinding = (OperationBinding)obj;
				OperationDescription operationDescription = context.GetOperationDescription(operationBinding);
				for (int i = 0; i < operationDescription.Messages.Count; i++)
				{
					MessageDescription message = operationDescription.Messages[i];
					MessageBinding messageBinding = context.GetMessageBinding(message);
					MessageEncodingBindingElementImporter.ImportMessageSoapAction(context.ContractConversionContext, message, messageBinding, i != 0);
				}
				foreach (FaultDescription fault in operationDescription.Faults)
				{
					FaultBinding faultBinding = context.GetFaultBinding(fault);
					if (faultBinding != null)
					{
						MessageEncodingBindingElementImporter.ImportFaultSoapAction(context.ContractConversionContext, fault, faultBinding);
					}
				}
			}
		}

		// Token: 0x06006486 RID: 25734 RVA: 0x0017707C File Offset: 0x0017527C
		private static void ImportFaultSoapAction(WsdlContractConversionContext contractContext, FaultDescription fault, FaultBinding wsdlFaultBinding)
		{
			string text = SoapHelper.ReadSoapAction(wsdlFaultBinding.OperationBinding);
			if (contractContext != null)
			{
				OperationFault operationFault = contractContext.GetOperationFault(fault);
				if (WsdlImporter.WSAddressingHelper.FindWsaActionAttribute(operationFault) == null && text != null)
				{
					fault.Action = text;
				}
			}
		}

		// Token: 0x06006487 RID: 25735 RVA: 0x001770B4 File Offset: 0x001752B4
		private static void ImportMessageSoapAction(WsdlContractConversionContext contractContext, MessageDescription message, MessageBinding wsdlMessageBinding, bool isResponse)
		{
			string text = SoapHelper.ReadSoapAction(wsdlMessageBinding.OperationBinding);
			if (contractContext != null)
			{
				OperationMessage operationMessage = contractContext.GetOperationMessage(message);
				if (WsdlImporter.WSAddressingHelper.FindWsaActionAttribute(operationMessage) == null && text != null)
				{
					if (isResponse)
					{
						message.Action = "*";
						return;
					}
					message.Action = text;
				}
			}
		}

		// Token: 0x06006488 RID: 25736 RVA: 0x001770FC File Offset: 0x001752FC
		private static void EnsureMessageEncoding(WsdlEndpointConversionContext context, MessageEncodingBindingElement encodingBindingElement)
		{
			EnvelopeVersion soapVersion = SoapHelper.GetSoapVersion(context.WsdlBinding);
			AddressingVersion addressingVersion;
			if (encodingBindingElement == null)
			{
				encodingBindingElement = new TextMessageEncodingBindingElement();
				MessageEncodingBindingElementImporter.ConvertToCustomBinding(context).Elements.Add(encodingBindingElement);
				addressingVersion = AddressingVersion.None;
			}
			else if (soapVersion == EnvelopeVersion.None)
			{
				addressingVersion = AddressingVersion.None;
			}
			else
			{
				addressingVersion = encodingBindingElement.MessageVersion.Addressing;
			}
			MessageVersion messageVersion = MessageVersion.CreateVersion(soapVersion, addressingVersion);
			if (!encodingBindingElement.MessageVersion.IsMatch(messageVersion))
			{
				MessageEncodingBindingElementImporter.ConvertToCustomBinding(context).Elements.Find<MessageEncodingBindingElement>().MessageVersion = MessageVersion.CreateVersion(soapVersion, addressingVersion);
			}
		}

		// Token: 0x06006489 RID: 25737 RVA: 0x00177188 File Offset: 0x00175388
		private static BindingElementCollection GetBindingElements(WsdlEndpointConversionContext context)
		{
			Binding binding = context.Endpoint.Binding;
			return (binding is CustomBinding) ? ((CustomBinding)binding).Elements : binding.CreateBindingElements();
		}

		// Token: 0x0600648A RID: 25738 RVA: 0x001771C0 File Offset: 0x001753C0
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

		// Token: 0x0600648B RID: 25739 RVA: 0x001771FF File Offset: 0x001753FF
		void IPolicyImportExtension.ImportPolicy(MetadataImporter importer, PolicyConversionContext context)
		{
			if (importer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("importer");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			this.ImportPolicyInternal(context);
		}

		// Token: 0x0600648C RID: 25740 RVA: 0x00177230 File Offset: 0x00175430
		private void ImportPolicyInternal(PolicyConversionContext context)
		{
			ICollection<XmlElement> bindingAssertions = context.GetBindingAssertions();
			XmlElement xmlElement;
			MessageEncodingBindingElement messageEncodingBindingElement = this.CreateEncodingBindingElement(context.GetBindingAssertions(), out xmlElement);
			AddressingVersion addressingVersion = WsdlImporter.WSAddressingHelper.FindAddressingVersion(context);
			MessageEncodingBindingElementImporter.ApplyAddressingVersion(messageEncodingBindingElement, addressingVersion);
			context.BindingElements.Add(messageEncodingBindingElement);
		}

		// Token: 0x0600648D RID: 25741 RVA: 0x00177270 File Offset: 0x00175470
		private static void ApplyAddressingVersion(MessageEncodingBindingElement encodingBindingElement, AddressingVersion addressingVersion)
		{
			EnvelopeVersion envelope = encodingBindingElement.MessageVersion.Envelope;
			if (envelope == EnvelopeVersion.None && addressingVersion != AddressingVersion.None)
			{
				encodingBindingElement.MessageVersion = MessageVersion.CreateVersion(EnvelopeVersion.Soap12, addressingVersion);
				return;
			}
			encodingBindingElement.MessageVersion = MessageVersion.CreateVersion(envelope, addressingVersion);
		}

		// Token: 0x0600648E RID: 25742 RVA: 0x001772B8 File Offset: 0x001754B8
		private MessageEncodingBindingElement CreateEncodingBindingElement(ICollection<XmlElement> assertions, out XmlElement encodingAssertion)
		{
			encodingAssertion = null;
			foreach (XmlElement xmlElement in assertions)
			{
				string namespaceURI = xmlElement.NamespaceURI;
				if (!(namespaceURI == "http://schemas.microsoft.com/ws/06/2004/mspolicy/netbinary1"))
				{
					if (namespaceURI == "http://schemas.xmlsoap.org/ws/2004/09/policy/optimizedmimeserialization")
					{
						if (xmlElement.LocalName == "OptimizedMimeSerialization")
						{
							encodingAssertion = xmlElement;
							assertions.Remove(encodingAssertion);
							return new MtomMessageEncodingBindingElement();
						}
					}
				}
				else if (xmlElement.LocalName == "BinaryEncoding")
				{
					encodingAssertion = xmlElement;
					assertions.Remove(encodingAssertion);
					return new BinaryMessageEncodingBindingElement();
				}
			}
			return new TextMessageEncodingBindingElement();
		}
	}
}
