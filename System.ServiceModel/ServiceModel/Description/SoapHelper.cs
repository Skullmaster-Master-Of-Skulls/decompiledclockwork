using System;
using System.Collections.Generic;
using System.ServiceModel.Dispatcher;
using System.Web.Services.Description;
using System.Xml;

namespace System.ServiceModel.Description
{
	// Token: 0x02000424 RID: 1060
	internal static class SoapHelper
	{
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060028C2 RID: 10434 RVA: 0x00099110 File Offset: 0x00097310
		private static XmlDocument Document
		{
			get
			{
				if (SoapHelper.xmlDocument == null)
				{
					SoapHelper.xmlDocument = new XmlDocument();
				}
				return SoapHelper.xmlDocument;
			}
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x00099128 File Offset: 0x00097328
		private static XmlAttribute CreateLocalAttribute(string name, string value)
		{
			XmlAttribute xmlAttribute = SoapHelper.Document.CreateAttribute(name);
			xmlAttribute.Value = value;
			return xmlAttribute;
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x0009914C File Offset: 0x0009734C
		internal static SoapAddressBinding GetOrCreateSoapAddressBinding(Binding wsdlBinding, Port wsdlPort, WsdlExporter exporter)
		{
			if (SoapHelper.GetSoapVersionState(wsdlBinding, exporter) == EnvelopeVersion.None)
			{
				return null;
			}
			SoapAddressBinding soapAddressBinding = SoapHelper.GetSoapAddressBinding(wsdlPort);
			EnvelopeVersion soapVersion = SoapHelper.GetSoapVersion(wsdlBinding);
			if (soapAddressBinding != null)
			{
				return soapAddressBinding;
			}
			return SoapHelper.CreateSoapAddressBinding(soapVersion, wsdlPort);
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x00099188 File Offset: 0x00097388
		internal static SoapBinding GetOrCreateSoapBinding(WsdlEndpointConversionContext endpointContext, WsdlExporter exporter)
		{
			if (SoapHelper.GetSoapVersionState(endpointContext.WsdlBinding, exporter) == EnvelopeVersion.None)
			{
				return null;
			}
			SoapBinding soapBinding = SoapHelper.GetSoapBinding(endpointContext);
			if (soapBinding != null)
			{
				return soapBinding;
			}
			EnvelopeVersion soapVersion = SoapHelper.GetSoapVersion(endpointContext.WsdlBinding);
			return SoapHelper.CreateSoapBinding(soapVersion, endpointContext.WsdlBinding);
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000991D0 File Offset: 0x000973D0
		internal static SoapOperationBinding GetOrCreateSoapOperationBinding(WsdlEndpointConversionContext endpointContext, OperationDescription operation, WsdlExporter exporter)
		{
			if (SoapHelper.GetSoapVersionState(endpointContext.WsdlBinding, exporter) == EnvelopeVersion.None)
			{
				return null;
			}
			SoapOperationBinding soapOperationBinding = SoapHelper.GetSoapOperationBinding(endpointContext, operation);
			OperationBinding operationBinding = endpointContext.GetOperationBinding(operation);
			EnvelopeVersion soapVersion = SoapHelper.GetSoapVersion(endpointContext.WsdlBinding);
			if (soapOperationBinding != null)
			{
				return soapOperationBinding;
			}
			return SoapHelper.CreateSoapOperationBinding(soapVersion, operationBinding);
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x0009921C File Offset: 0x0009741C
		internal static SoapBodyBinding GetOrCreateSoapBodyBinding(WsdlEndpointConversionContext endpointContext, MessageBinding wsdlMessageBinding, WsdlExporter exporter)
		{
			if (SoapHelper.GetSoapVersionState(endpointContext.WsdlBinding, exporter) == EnvelopeVersion.None)
			{
				return null;
			}
			SoapBodyBinding soapBodyBinding = SoapHelper.GetSoapBodyBinding(endpointContext, wsdlMessageBinding);
			EnvelopeVersion soapVersion = SoapHelper.GetSoapVersion(endpointContext.WsdlBinding);
			if (soapBodyBinding != null)
			{
				return soapBodyBinding;
			}
			return SoapHelper.CreateSoapBodyBinding(soapVersion, wsdlMessageBinding);
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x00099260 File Offset: 0x00097460
		internal static SoapHeaderBinding CreateSoapHeaderBinding(WsdlEndpointConversionContext endpointContext, MessageBinding wsdlMessageBinding)
		{
			EnvelopeVersion soapVersion = SoapHelper.GetSoapVersion(endpointContext.WsdlBinding);
			return SoapHelper.CreateSoapHeaderBinding(soapVersion, wsdlMessageBinding);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x00099284 File Offset: 0x00097484
		internal static void CreateSoapFaultBinding(string name, WsdlEndpointConversionContext endpointContext, FaultBinding wsdlFaultBinding, bool isEncoded)
		{
			EnvelopeVersion soapVersion = SoapHelper.GetSoapVersion(endpointContext.WsdlBinding);
			XmlElement xmlElement = SoapHelper.CreateSoapFaultBinding(soapVersion);
			xmlElement.Attributes.Append(SoapHelper.CreateLocalAttribute("name", name));
			xmlElement.Attributes.Append(SoapHelper.CreateLocalAttribute("use", isEncoded ? "encoded" : "literal"));
			wsdlFaultBinding.Extensions.Add(xmlElement);
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000992F0 File Offset: 0x000974F0
		internal static void SetSoapVersion(WsdlEndpointConversionContext endpointContext, WsdlExporter exporter, EnvelopeVersion version)
		{
			SoapHelper.SetSoapVersionState(endpointContext.WsdlBinding, exporter, version);
			if (endpointContext.WsdlPort != null)
			{
				SoapHelper.SoapConverter.ConvertExtensions(endpointContext.WsdlPort.Extensions, version, new SoapHelper.SoapConverter.ConvertExtension(SoapHelper.SoapConverter.ConvertSoapAddressBinding));
			}
			SoapHelper.SoapConverter.ConvertExtensions(endpointContext.WsdlBinding.Extensions, version, new SoapHelper.SoapConverter.ConvertExtension(SoapHelper.SoapConverter.ConvertSoapBinding));
			foreach (object obj in endpointContext.WsdlBinding.Operations)
			{
				OperationBinding operationBinding = (OperationBinding)obj;
				SoapHelper.SoapConverter.ConvertExtensions(operationBinding.Extensions, version, new SoapHelper.SoapConverter.ConvertExtension(SoapHelper.SoapConverter.ConvertSoapOperationBinding));
				if (operationBinding.Input != null)
				{
					SoapHelper.SoapConverter.ConvertExtensions(operationBinding.Input.Extensions, version, new SoapHelper.SoapConverter.ConvertExtension(SoapHelper.SoapConverter.ConvertSoapMessageBinding));
				}
				if (operationBinding.Output != null)
				{
					SoapHelper.SoapConverter.ConvertExtensions(operationBinding.Output.Extensions, version, new SoapHelper.SoapConverter.ConvertExtension(SoapHelper.SoapConverter.ConvertSoapMessageBinding));
				}
				foreach (object obj2 in operationBinding.Faults)
				{
					MessageBinding messageBinding = (MessageBinding)obj2;
					SoapHelper.SoapConverter.ConvertExtensions(messageBinding.Extensions, version, new SoapHelper.SoapConverter.ConvertExtension(SoapHelper.SoapConverter.ConvertSoapMessageBinding));
				}
			}
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x00099460 File Offset: 0x00097660
		internal static EnvelopeVersion GetSoapVersion(Binding wsdlBinding)
		{
			foreach (object obj in wsdlBinding.Extensions)
			{
				if (obj is SoapBinding)
				{
					return (obj is Soap12Binding) ? EnvelopeVersion.Soap12 : EnvelopeVersion.Soap11;
				}
			}
			return EnvelopeVersion.Soap12;
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x000994D4 File Offset: 0x000976D4
		private static void SetSoapVersionState(Binding wsdlBinding, WsdlExporter exporter, EnvelopeVersion version)
		{
			object obj = null;
			if (!exporter.State.TryGetValue(SoapHelper.SoapVersionStateKey, out obj))
			{
				obj = new Dictionary<Binding, EnvelopeVersion>();
				exporter.State[SoapHelper.SoapVersionStateKey] = obj;
			}
			((Dictionary<Binding, EnvelopeVersion>)obj)[wsdlBinding] = version;
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x0009951C File Offset: 0x0009771C
		private static EnvelopeVersion GetSoapVersionState(Binding wsdlBinding, WsdlExporter exporter)
		{
			object obj = null;
			if (exporter.State.TryGetValue(SoapHelper.SoapVersionStateKey, out obj) && obj != null && ((Dictionary<Binding, EnvelopeVersion>)obj).ContainsKey(wsdlBinding))
			{
				return ((Dictionary<Binding, EnvelopeVersion>)obj)[wsdlBinding];
			}
			return null;
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x00099560 File Offset: 0x00097760
		private static SoapAddressBinding CreateSoapAddressBinding(EnvelopeVersion version, Port wsdlPort)
		{
			SoapAddressBinding soapAddressBinding = null;
			if (version == EnvelopeVersion.Soap12)
			{
				soapAddressBinding = new Soap12AddressBinding();
			}
			else if (version == EnvelopeVersion.Soap11)
			{
				soapAddressBinding = new SoapAddressBinding();
			}
			wsdlPort.Extensions.Add(soapAddressBinding);
			return soapAddressBinding;
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x0009959C File Offset: 0x0009779C
		private static SoapBinding CreateSoapBinding(EnvelopeVersion version, Binding wsdlBinding)
		{
			SoapBinding soapBinding = null;
			if (version == EnvelopeVersion.Soap12)
			{
				soapBinding = new Soap12Binding();
			}
			else if (version == EnvelopeVersion.Soap11)
			{
				soapBinding = new SoapBinding();
			}
			wsdlBinding.Extensions.Add(soapBinding);
			return soapBinding;
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000995D8 File Offset: 0x000977D8
		private static SoapOperationBinding CreateSoapOperationBinding(EnvelopeVersion version, OperationBinding wsdlOperationBinding)
		{
			SoapOperationBinding soapOperationBinding = null;
			if (version == EnvelopeVersion.Soap12)
			{
				soapOperationBinding = new Soap12OperationBinding();
			}
			else if (version == EnvelopeVersion.Soap11)
			{
				soapOperationBinding = new SoapOperationBinding();
			}
			wsdlOperationBinding.Extensions.Add(soapOperationBinding);
			return soapOperationBinding;
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x00099614 File Offset: 0x00097814
		private static SoapBodyBinding CreateSoapBodyBinding(EnvelopeVersion version, MessageBinding wsdlMessageBinding)
		{
			SoapBodyBinding soapBodyBinding = null;
			if (version == EnvelopeVersion.Soap12)
			{
				soapBodyBinding = new Soap12BodyBinding();
			}
			else if (version == EnvelopeVersion.Soap11)
			{
				soapBodyBinding = new SoapBodyBinding();
			}
			wsdlMessageBinding.Extensions.Add(soapBodyBinding);
			return soapBodyBinding;
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x00099650 File Offset: 0x00097850
		private static SoapHeaderBinding CreateSoapHeaderBinding(EnvelopeVersion version, MessageBinding wsdlMessageBinding)
		{
			SoapHeaderBinding soapHeaderBinding = null;
			if (version == EnvelopeVersion.Soap12)
			{
				soapHeaderBinding = new Soap12HeaderBinding();
			}
			else if (version == EnvelopeVersion.Soap11)
			{
				soapHeaderBinding = new SoapHeaderBinding();
			}
			wsdlMessageBinding.Extensions.Add(soapHeaderBinding);
			return soapHeaderBinding;
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x0009968C File Offset: 0x0009788C
		private static XmlElement CreateSoapFaultBinding(EnvelopeVersion version)
		{
			string prefix = null;
			string namespaceURI = null;
			if (version == EnvelopeVersion.Soap12)
			{
				namespaceURI = "http://schemas.xmlsoap.org/wsdl/soap12/";
				prefix = "soap12";
			}
			else if (version == EnvelopeVersion.Soap11)
			{
				namespaceURI = "http://schemas.xmlsoap.org/wsdl/soap/";
				prefix = "soap";
			}
			return SoapHelper.Document.CreateElement(prefix, "fault", namespaceURI);
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000996D8 File Offset: 0x000978D8
		internal static SoapAddressBinding GetSoapAddressBinding(Port wsdlPort)
		{
			foreach (object obj in wsdlPort.Extensions)
			{
				if (obj is SoapAddressBinding)
				{
					return (SoapAddressBinding)obj;
				}
			}
			return null;
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x0009973C File Offset: 0x0009793C
		private static SoapBinding GetSoapBinding(WsdlEndpointConversionContext endpointContext)
		{
			foreach (object obj in endpointContext.WsdlBinding.Extensions)
			{
				if (obj is SoapBinding)
				{
					return (SoapBinding)obj;
				}
			}
			return null;
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x000997A4 File Offset: 0x000979A4
		private static SoapOperationBinding GetSoapOperationBinding(WsdlEndpointConversionContext endpointContext, OperationDescription operation)
		{
			OperationBinding operationBinding = endpointContext.GetOperationBinding(operation);
			foreach (object obj in operationBinding.Extensions)
			{
				if (obj is SoapOperationBinding)
				{
					return (SoapOperationBinding)obj;
				}
			}
			return null;
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x00099810 File Offset: 0x00097A10
		private static SoapBodyBinding GetSoapBodyBinding(WsdlEndpointConversionContext endpointContext, MessageBinding wsdlMessageBinding)
		{
			foreach (object obj in wsdlMessageBinding.Extensions)
			{
				if (obj is SoapBodyBinding)
				{
					return (SoapBodyBinding)obj;
				}
			}
			return null;
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x00099874 File Offset: 0x00097A74
		internal static string ReadSoapAction(OperationBinding wsdlOperationBinding)
		{
			SoapOperationBinding soapOperationBinding = (SoapOperationBinding)wsdlOperationBinding.Extensions.Find(typeof(SoapOperationBinding));
			if (soapOperationBinding == null)
			{
				return null;
			}
			return soapOperationBinding.SoapAction;
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000998A8 File Offset: 0x00097AA8
		internal static SoapBindingStyle GetStyle(Binding binding)
		{
			SoapBindingStyle result = SoapBindingStyle.Default;
			if (binding != null)
			{
				SoapBinding soapBinding = binding.Extensions.Find(typeof(SoapBinding)) as SoapBinding;
				if (soapBinding != null)
				{
					result = soapBinding.Style;
				}
			}
			return result;
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000998E0 File Offset: 0x00097AE0
		internal static SoapBindingStyle GetStyle(OperationBinding operationBinding, SoapBindingStyle defaultBindingStyle)
		{
			SoapBindingStyle result = defaultBindingStyle;
			if (operationBinding != null)
			{
				SoapOperationBinding soapOperationBinding = operationBinding.Extensions.Find(typeof(SoapOperationBinding)) as SoapOperationBinding;
				if (soapOperationBinding != null && soapOperationBinding.Style != SoapBindingStyle.Default)
				{
					result = soapOperationBinding.Style;
				}
			}
			return result;
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x00099920 File Offset: 0x00097B20
		internal static bool IsSoapFaultBinding(XmlElement element)
		{
			return element != null && element.LocalName == "fault" && (element.NamespaceURI == "http://schemas.xmlsoap.org/wsdl/soap12/" || element.NamespaceURI == "http://schemas.xmlsoap.org/wsdl/soap/");
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x00099960 File Offset: 0x00097B60
		internal static bool IsEncoded(XmlElement element)
		{
			XmlAttribute attributeNode = element.GetAttributeNode("use");
			return attributeNode != null && attributeNode.Value == "encoded";
		}

		// Token: 0x04002259 RID: 8793
		private static object SoapVersionStateKey = new object();

		// Token: 0x0400225A RID: 8794
		private static XmlDocument xmlDocument;

		// Token: 0x02000BDD RID: 3037
		private static class SoapConverter
		{
			// Token: 0x06007547 RID: 30023 RVA: 0x001B6B40 File Offset: 0x001B4D40
			internal static void ConvertExtensions(ServiceDescriptionFormatExtensionCollection extensions, EnvelopeVersion version, SoapHelper.SoapConverter.ConvertExtension conversionMethod)
			{
				bool flag = false;
				for (int i = extensions.Count - 1; i >= 0; i--)
				{
					object obj = extensions[i];
					if (conversionMethod(ref obj, version))
					{
						if (obj == null)
						{
							extensions.Remove(extensions[i]);
						}
						else
						{
							extensions[i] = obj;
						}
						flag = true;
					}
				}
				if (!flag)
				{
					object obj2 = null;
					conversionMethod(ref obj2, version);
					if (obj2 != null)
					{
						extensions.Add(obj2);
					}
				}
			}

			// Token: 0x06007548 RID: 30024 RVA: 0x001B6BAC File Offset: 0x001B4DAC
			internal static bool ConvertSoapBinding(ref object src, EnvelopeVersion version)
			{
				SoapBinding soapBinding = src as SoapBinding;
				if (src != null)
				{
					if (soapBinding == null)
					{
						return false;
					}
					if (SoapHelper.SoapConverter.GetBindingVersion<Soap12Binding>(src) == version)
					{
						return true;
					}
				}
				if (version == EnvelopeVersion.None)
				{
					src = null;
					return true;
				}
				SoapBinding soapBinding2 = (version == EnvelopeVersion.Soap12) ? new Soap12Binding() : new SoapBinding();
				if (soapBinding != null)
				{
					soapBinding2.Required = soapBinding.Required;
					soapBinding2.Style = soapBinding.Style;
					soapBinding2.Transport = soapBinding.Transport;
				}
				src = soapBinding2;
				return true;
			}

			// Token: 0x06007549 RID: 30025 RVA: 0x001B6C24 File Offset: 0x001B4E24
			internal static bool ConvertSoapAddressBinding(ref object src, EnvelopeVersion version)
			{
				SoapAddressBinding soapAddressBinding = src as SoapAddressBinding;
				if (src != null)
				{
					if (soapAddressBinding == null)
					{
						return false;
					}
					if (SoapHelper.SoapConverter.GetBindingVersion<Soap12AddressBinding>(src) == version)
					{
						return true;
					}
				}
				if (version == EnvelopeVersion.None)
				{
					src = null;
					return true;
				}
				SoapAddressBinding soapAddressBinding2 = (version == EnvelopeVersion.Soap12) ? new Soap12AddressBinding() : new SoapAddressBinding();
				if (soapAddressBinding != null)
				{
					soapAddressBinding2.Required = soapAddressBinding.Required;
					soapAddressBinding2.Location = soapAddressBinding.Location;
				}
				src = soapAddressBinding2;
				return true;
			}

			// Token: 0x0600754A RID: 30026 RVA: 0x001B6C90 File Offset: 0x001B4E90
			internal static bool ConvertSoapOperationBinding(ref object src, EnvelopeVersion version)
			{
				SoapOperationBinding soapOperationBinding = src as SoapOperationBinding;
				if (src != null)
				{
					if (soapOperationBinding == null)
					{
						return false;
					}
					if (SoapHelper.SoapConverter.GetBindingVersion<Soap12OperationBinding>(src) == version)
					{
						return true;
					}
				}
				if (version == EnvelopeVersion.None)
				{
					src = null;
					return true;
				}
				SoapOperationBinding soapOperationBinding2 = (version == EnvelopeVersion.Soap12) ? new Soap12OperationBinding() : new SoapOperationBinding();
				if (src != null)
				{
					soapOperationBinding2.Required = soapOperationBinding.Required;
					soapOperationBinding2.Style = soapOperationBinding.Style;
					soapOperationBinding2.SoapAction = soapOperationBinding.SoapAction;
				}
				src = soapOperationBinding2;
				return true;
			}

			// Token: 0x0600754B RID: 30027 RVA: 0x001B6D08 File Offset: 0x001B4F08
			internal static bool ConvertSoapMessageBinding(ref object src, EnvelopeVersion version)
			{
				SoapBodyBinding soapBodyBinding = src as SoapBodyBinding;
				if (soapBodyBinding != null)
				{
					src = SoapHelper.SoapConverter.ConvertSoapBodyBinding(soapBodyBinding, version);
					return true;
				}
				SoapHeaderBinding soapHeaderBinding = src as SoapHeaderBinding;
				if (soapHeaderBinding != null)
				{
					src = SoapHelper.SoapConverter.ConvertSoapHeaderBinding(soapHeaderBinding, version);
					return true;
				}
				SoapFaultBinding soapFaultBinding = src as SoapFaultBinding;
				if (soapFaultBinding != null)
				{
					src = SoapHelper.SoapConverter.ConvertSoapFaultBinding(soapFaultBinding, version);
					return true;
				}
				XmlElement xmlElement = src as XmlElement;
				if (xmlElement != null && SoapHelper.IsSoapFaultBinding(xmlElement))
				{
					src = SoapHelper.SoapConverter.ConvertSoapFaultBinding(xmlElement, version);
					return true;
				}
				return src == null;
			}

			// Token: 0x0600754C RID: 30028 RVA: 0x001B6D7C File Offset: 0x001B4F7C
			private static SoapBodyBinding ConvertSoapBodyBinding(SoapBodyBinding src, EnvelopeVersion version)
			{
				if (version == EnvelopeVersion.None)
				{
					return null;
				}
				EnvelopeVersion bindingVersion = SoapHelper.SoapConverter.GetBindingVersion<Soap12BodyBinding>(src);
				if (bindingVersion == version)
				{
					return src;
				}
				SoapBodyBinding soapBodyBinding = (version == EnvelopeVersion.Soap12) ? new Soap12BodyBinding() : new SoapBodyBinding();
				if (src != null)
				{
					if (XmlSerializerOperationFormatter.GetEncoding(bindingVersion) == src.Encoding)
					{
						soapBodyBinding.Encoding = XmlSerializerOperationFormatter.GetEncoding(version);
					}
					soapBodyBinding.Encoding = XmlSerializerOperationFormatter.GetEncoding(version);
					soapBodyBinding.Namespace = src.Namespace;
					soapBodyBinding.Parts = src.Parts;
					soapBodyBinding.PartsString = src.PartsString;
					soapBodyBinding.Use = src.Use;
					soapBodyBinding.Required = src.Required;
				}
				return soapBodyBinding;
			}

			// Token: 0x0600754D RID: 30029 RVA: 0x001B6E20 File Offset: 0x001B5020
			private static XmlElement ConvertSoapFaultBinding(XmlElement src, EnvelopeVersion version)
			{
				if (src == null)
				{
					return null;
				}
				if (version == EnvelopeVersion.Soap12)
				{
					if (src.NamespaceURI == "http://schemas.xmlsoap.org/wsdl/soap12/")
					{
						return src;
					}
				}
				else
				{
					if (version != EnvelopeVersion.Soap11)
					{
						return null;
					}
					if (src.NamespaceURI == "http://schemas.xmlsoap.org/wsdl/soap/")
					{
						return src;
					}
				}
				XmlElement xmlElement = SoapHelper.CreateSoapFaultBinding(version);
				if (src.HasAttributes)
				{
					foreach (object obj in src.Attributes)
					{
						XmlAttribute xmlAttribute = (XmlAttribute)obj;
						xmlElement.SetAttribute(xmlAttribute.Name, xmlAttribute.Value);
					}
				}
				return xmlElement;
			}

			// Token: 0x0600754E RID: 30030 RVA: 0x001B6ED4 File Offset: 0x001B50D4
			private static SoapFaultBinding ConvertSoapFaultBinding(SoapFaultBinding src, EnvelopeVersion version)
			{
				if (version == EnvelopeVersion.None)
				{
					return null;
				}
				if (SoapHelper.SoapConverter.GetBindingVersion<Soap12FaultBinding>(src) == version)
				{
					return src;
				}
				SoapFaultBinding soapFaultBinding = (version == EnvelopeVersion.Soap12) ? new Soap12FaultBinding() : new SoapFaultBinding();
				if (src != null)
				{
					soapFaultBinding.Encoding = src.Encoding;
					soapFaultBinding.Name = src.Name;
					soapFaultBinding.Namespace = src.Namespace;
					soapFaultBinding.Use = src.Use;
					soapFaultBinding.Required = src.Required;
				}
				return soapFaultBinding;
			}

			// Token: 0x0600754F RID: 30031 RVA: 0x001B6F4C File Offset: 0x001B514C
			private static SoapHeaderBinding ConvertSoapHeaderBinding(SoapHeaderBinding src, EnvelopeVersion version)
			{
				if (version == EnvelopeVersion.None)
				{
					return null;
				}
				if (SoapHelper.SoapConverter.GetBindingVersion<Soap12HeaderBinding>(src) == version)
				{
					return src;
				}
				SoapHeaderBinding soapHeaderBinding = (version == EnvelopeVersion.Soap12) ? new Soap12HeaderBinding() : new SoapHeaderBinding();
				if (src != null)
				{
					soapHeaderBinding.Fault = src.Fault;
					soapHeaderBinding.MapToProperty = src.MapToProperty;
					soapHeaderBinding.Message = src.Message;
					soapHeaderBinding.Part = src.Part;
					soapHeaderBinding.Encoding = src.Encoding;
					soapHeaderBinding.Namespace = src.Namespace;
					soapHeaderBinding.Use = src.Use;
					soapHeaderBinding.Required = src.Required;
				}
				return soapHeaderBinding;
			}

			// Token: 0x06007550 RID: 30032 RVA: 0x001B6FE7 File Offset: 0x001B51E7
			internal static EnvelopeVersion GetBindingVersion<T12>(object src)
			{
				if (!(src is T12))
				{
					return EnvelopeVersion.Soap11;
				}
				return EnvelopeVersion.Soap12;
			}

			// Token: 0x02000F22 RID: 3874
			// (Invoke) Token: 0x06008649 RID: 34377
			internal delegate bool ConvertExtension(ref object src, EnvelopeVersion version);
		}
	}
}
