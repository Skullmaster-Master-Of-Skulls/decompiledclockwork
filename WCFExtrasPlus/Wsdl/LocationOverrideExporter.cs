using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Web.Services.Description;
using System.Xml.Schema;
using WCFExtrasPlus.Utils;

namespace WCFExtrasPlus.Wsdl
{
	// Token: 0x0200001F RID: 31
	internal class LocationOverrideExporter
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00004E1A File Offset: 0x0000301A
		private LocationOverrideExporter(Uri location)
		{
			this.location = location;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004E34 File Offset: 0x00003034
		public void ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
		{
			foreach (object obj in context.WsdlPort.Extensions)
			{
				SoapAddressBinding soapAddressBinding = obj as SoapAddressBinding;
				if (soapAddressBinding != null)
				{
					soapAddressBinding.Location = this.location.ToString();
				}
			}
			this.EnumerateWsdlsAndSchemas(exporter.GeneratedWsdlDocuments, exporter.GeneratedXmlSchemas);
			context.Endpoint.Address = new EndpointAddress(this.location, new AddressHeader[0]);
			foreach (object obj2 in exporter.GeneratedWsdlDocuments)
			{
				System.Web.Services.Description.ServiceDescription wsdlDoc = (System.Web.Services.Description.ServiceDescription)obj2;
				this.FixImportAddresses(exporter.GeneratedWsdlDocuments, wsdlDoc, exporter.GeneratedXmlSchemas);
			}
			foreach (object obj3 in exporter.GeneratedXmlSchemas.Schemas())
			{
				XmlSchema xsdDoc = (XmlSchema)obj3;
				this.FixImportAddresses(exporter.GeneratedXmlSchemas, xsdDoc);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004F8C File Offset: 0x0000318C
		private void EnumerateWsdlsAndSchemas(ServiceDescriptionCollection wsdls, XmlSchemaSet xsds)
		{
			System.Web.Services.Description.ServiceDescription serviceDescription = WsdlUtils.FindRootDescription(wsdls);
			int num = 0;
			foreach (object obj in wsdls)
			{
				System.Web.Services.Description.ServiceDescription serviceDescription2 = (System.Web.Services.Description.ServiceDescription)obj;
				string text = "wsdl";
				if (serviceDescription2 != serviceDescription)
				{
					text = text + "=wsdl" + num++;
				}
				this.queryFromDoc.Add(serviceDescription2, text);
			}
			int num2 = 0;
			foreach (object obj2 in xsds.Schemas())
			{
				XmlSchema key = (XmlSchema)obj2;
				string value = "xsd=xsd" + num2++;
				this.queryFromDoc.Add(key, value);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000508C File Offset: 0x0000328C
		private void FixImportAddresses(ServiceDescriptionCollection wsdls, System.Web.Services.Description.ServiceDescription wsdlDoc, XmlSchemaSet schemas)
		{
			foreach (object obj in wsdlDoc.Imports)
			{
				Import import = (Import)obj;
				if (string.IsNullOrEmpty(import.Location))
				{
					System.Web.Services.Description.ServiceDescription serviceDescription = wsdls[import.Namespace ?? string.Empty];
					if (serviceDescription != null)
					{
						string arg = this.queryFromDoc[serviceDescription];
						import.Location = this.location + "?" + arg;
					}
				}
			}
			if (wsdlDoc.Types != null)
			{
				foreach (object obj2 in wsdlDoc.Types.Schemas)
				{
					XmlSchema xsdDoc = (XmlSchema)obj2;
					this.FixImportAddresses(schemas, xsdDoc);
				}
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000518C File Offset: 0x0000338C
		private void FixImportAddresses(XmlSchemaSet xmlSchemaSet, XmlSchema xsdDoc)
		{
			foreach (XmlSchemaObject xmlSchemaObject in xsdDoc.Includes)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)xmlSchemaObject;
				if (xmlSchemaExternal != null && string.IsNullOrEmpty(xmlSchemaExternal.SchemaLocation))
				{
					string text = (xmlSchemaExternal is XmlSchemaImport) ? ((XmlSchemaImport)xmlSchemaExternal).Namespace : xsdDoc.TargetNamespace;
					foreach (object obj in xmlSchemaSet.Schemas(text ?? string.Empty))
					{
						XmlSchema xmlSchema = (XmlSchema)obj;
						if (xmlSchema != xsdDoc)
						{
							string arg = this.queryFromDoc[xmlSchema];
							xmlSchemaExternal.SchemaLocation = this.location + "?" + arg;
							break;
						}
					}
				}
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000529C File Offset: 0x0000349C
		internal static void ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context, Uri uri)
		{
			LocationOverrideExporter locationOverrideExporter = new LocationOverrideExporter(uri);
			locationOverrideExporter.ExportEndpoint(exporter, context);
		}

		// Token: 0x0400002F RID: 47
		private Uri location;

		// Token: 0x04000030 RID: 48
		private Dictionary<object, string> queryFromDoc = new Dictionary<object, string>();
	}
}
