using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.Web.Services.Description;
using System.Xml.Schema;
using WCFExtras.Utils;

namespace WCFExtras.Wsdl
{
	// Token: 0x02000005 RID: 5
	internal class LocationOverrideExporter
	{
		// Token: 0x0600001C RID: 28 RVA: 0x0000249D File Offset: 0x0000069D
		private LocationOverrideExporter(Uri location)
		{
			this.location = location;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024BC File Offset: 0x000006BC
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

		// Token: 0x0600001E RID: 30 RVA: 0x00002638 File Offset: 0x00000838
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

		// Token: 0x0600001F RID: 31 RVA: 0x00002760 File Offset: 0x00000960
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

		// Token: 0x06000020 RID: 32 RVA: 0x000028A0 File Offset: 0x00000AA0
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

		// Token: 0x06000021 RID: 33 RVA: 0x000029E0 File Offset: 0x00000BE0
		internal static void ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context, Uri uri)
		{
			LocationOverrideExporter locationOverrideExporter = new LocationOverrideExporter(uri);
			locationOverrideExporter.ExportEndpoint(exporter, context);
		}

		// Token: 0x04000004 RID: 4
		private Uri location;

		// Token: 0x04000005 RID: 5
		private Dictionary<object, string> queryFromDoc = new Dictionary<object, string>();
	}
}
