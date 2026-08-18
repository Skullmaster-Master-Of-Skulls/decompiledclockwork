using System;
using System.Collections.Generic;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace WCFExtras.Utils
{
	// Token: 0x02000010 RID: 16
	internal static class WsdlUtils
	{
		// Token: 0x0600004C RID: 76 RVA: 0x0000359C File Offset: 0x0000179C
		public static ServiceDescription FindRootDescription(ServiceDescriptionCollection wsdls)
		{
			ServiceDescription result = null;
			foreach (object obj in wsdls)
			{
				ServiceDescription serviceDescription = (ServiceDescription)obj;
				if (serviceDescription.Services.Count > 0)
				{
					result = serviceDescription.Services[0].ServiceDescription;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003630 File Offset: 0x00001830
		private static void EnumerateDocumentedItems(XmlSchemaObjectCollection schemaItems, Dictionary<string, string> documentedItems)
		{
			foreach (XmlSchemaObject schemaObj in schemaItems)
			{
				string documenation = WsdlUtils.GetDocumenation(schemaObj);
				if (documenation != null)
				{
					string uniqueName = WsdlUtils.GetUniqueName(schemaObj);
					documentedItems[uniqueName] = documenation;
				}
				WsdlUtils.EnumerateDocumentedItems(schemaObj, documentedItems);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000036B8 File Offset: 0x000018B8
		private static void EnumerateDocumentedItems(XmlSchemaObject schemaObj, Dictionary<string, string> documentedItems)
		{
			XmlSchemaComplexType xmlSchemaComplexType = schemaObj as XmlSchemaComplexType;
			if (xmlSchemaComplexType != null)
			{
				XmlSchemaSequence xmlSchemaSequence = xmlSchemaComplexType.ContentTypeParticle as XmlSchemaSequence;
				if (xmlSchemaSequence != null)
				{
					WsdlUtils.EnumerateDocumentedItems(xmlSchemaSequence.Items, documentedItems);
				}
			}
			else
			{
				XmlSchemaSimpleType xmlSchemaSimpleType = schemaObj as XmlSchemaSimpleType;
				if (xmlSchemaSimpleType != null && xmlSchemaSimpleType.Content != null)
				{
					XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = xmlSchemaSimpleType.Content as XmlSchemaSimpleTypeRestriction;
					if (xmlSchemaSimpleTypeRestriction != null)
					{
						WsdlUtils.EnumerateDocumentedItems(xmlSchemaSimpleTypeRestriction.Facets, documentedItems);
					}
				}
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003744 File Offset: 0x00001944
		private static string GetUniqueName(XmlSchemaObject schemaObj)
		{
			string result;
			if (schemaObj is XmlSchemaType)
			{
				result = XmlConvert.DecodeName(((XmlSchemaType)schemaObj).QualifiedName.ToString());
			}
			else if (schemaObj is XmlSchemaElement)
			{
				string uniqueName = WsdlUtils.GetUniqueName(schemaObj.Parent.Parent);
				result = uniqueName + "." + XmlConvert.DecodeName(((XmlSchemaElement)schemaObj).Name);
			}
			else
			{
				if (!(schemaObj is XmlSchemaEnumerationFacet))
				{
					throw new NotImplementedException();
				}
				string uniqueName = WsdlUtils.GetUniqueName(schemaObj.Parent.Parent);
				result = uniqueName + "." + XmlConvert.DecodeName(((XmlSchemaEnumerationFacet)schemaObj).Value);
			}
			return result;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003808 File Offset: 0x00001A08
		private static string GetDocumenation(XmlSchemaObject schemaObj)
		{
			XmlSchemaAnnotated xmlSchemaAnnotated = schemaObj as XmlSchemaAnnotated;
			string result;
			if (xmlSchemaAnnotated == null || xmlSchemaAnnotated.Annotation == null)
			{
				result = null;
			}
			else
			{
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchemaAnnotated.Annotation.Items)
				{
					XmlSchemaDocumentation xmlSchemaDocumentation = xmlSchemaObject as XmlSchemaDocumentation;
					if (xmlSchemaDocumentation != null && xmlSchemaDocumentation.Markup.Length > 0)
					{
						return xmlSchemaDocumentation.Markup[0].Value;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000038D0 File Offset: 0x00001AD0
		internal static void EnumerateDocumentedItems(XmlSchemaSet xmlSchemaSet, Dictionary<string, string> documentedItems)
		{
			foreach (object obj in xmlSchemaSet.Schemas())
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				WsdlUtils.EnumerateDocumentedItems(xmlSchema.Items, documentedItems);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000393C File Offset: 0x00001B3C
		internal static void EnumerateDocumentedItems(ServiceDescriptionCollection wsdls, Dictionary<string, string> documentedItems)
		{
			foreach (object obj in wsdls)
			{
				ServiceDescription serviceDescription = (ServiceDescription)obj;
				foreach (object obj2 in serviceDescription.Types.Schemas)
				{
					XmlSchema xmlSchema = (XmlSchema)obj2;
					WsdlUtils.EnumerateDocumentedItems(xmlSchema.Items, documentedItems);
				}
			}
		}
	}
}
