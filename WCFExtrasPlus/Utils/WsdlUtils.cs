using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;

namespace WCFExtrasPlus.Utils
{
	// Token: 0x02000021 RID: 33
	internal static class WsdlUtils
	{
		// Token: 0x060000BC RID: 188 RVA: 0x000053C4 File Offset: 0x000035C4
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

		// Token: 0x060000BD RID: 189 RVA: 0x00005438 File Offset: 0x00003638
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

		// Token: 0x060000BE RID: 190 RVA: 0x000054A4 File Offset: 0x000036A4
		private static void EnumerateDocumentedItems(XmlSchemaObject schemaObj, Dictionary<string, string> documentedItems)
		{
			XmlSchemaComplexType xmlSchemaComplexType = schemaObj as XmlSchemaComplexType;
			if (xmlSchemaComplexType != null)
			{
				XmlSchemaSequence xmlSchemaSequence = xmlSchemaComplexType.ContentTypeParticle as XmlSchemaSequence;
				if (xmlSchemaSequence != null)
				{
					WsdlUtils.EnumerateDocumentedItems(xmlSchemaSequence.Items, documentedItems);
					return;
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

		// Token: 0x060000BF RID: 191 RVA: 0x00005504 File Offset: 0x00003704
		private static string GetUniqueName(XmlSchemaObject schemaObj)
		{
			if (schemaObj is XmlSchemaType)
			{
				return XmlConvert.DecodeName(((XmlSchemaType)schemaObj).QualifiedName.ToString());
			}
			if (schemaObj is XmlSchemaElement)
			{
				string uniqueName;
				if (schemaObj.Parent.Parent is XmlSchemaComplexContentExtension)
				{
					uniqueName = WsdlUtils.GetUniqueName(schemaObj.Parent.Parent.Parent.Parent);
				}
				else
				{
					uniqueName = WsdlUtils.GetUniqueName(schemaObj.Parent.Parent);
				}
				return uniqueName + "." + XmlConvert.DecodeName(((XmlSchemaElement)schemaObj).Name);
			}
			if (schemaObj is XmlSchemaEnumerationFacet)
			{
				string uniqueName2 = WsdlUtils.GetUniqueName(schemaObj.Parent.Parent);
				return uniqueName2 + "." + XmlConvert.DecodeName(((XmlSchemaEnumerationFacet)schemaObj).Value);
			}
			throw new NotImplementedException(string.Format("Unknown schema object detected: {0}, at line number {1}, position {2}", schemaObj.GetType().FullName, schemaObj.LineNumber, schemaObj.LinePosition));
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000055F8 File Offset: 0x000037F8
		private static string GetDocumenation(XmlSchemaObject schemaObj)
		{
			XmlSchemaAnnotated xmlSchemaAnnotated = schemaObj as XmlSchemaAnnotated;
			if (xmlSchemaAnnotated == null || xmlSchemaAnnotated.Annotation == null)
			{
				return null;
			}
			foreach (XmlSchemaObject xmlSchemaObject in xmlSchemaAnnotated.Annotation.Items)
			{
				XmlSchemaDocumentation xmlSchemaDocumentation = xmlSchemaObject as XmlSchemaDocumentation;
				if (xmlSchemaDocumentation != null && xmlSchemaDocumentation.Markup.Length > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (XmlNode xmlNode in xmlSchemaDocumentation.Markup)
					{
						stringBuilder.Append(xmlNode.Value);
					}
					return stringBuilder.ToString();
				}
			}
			return null;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000056C0 File Offset: 0x000038C0
		internal static void EnumerateDocumentedItems(XmlSchemaSet xmlSchemaSet, Dictionary<string, string> documentedItems)
		{
			foreach (object obj in xmlSchemaSet.Schemas())
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				WsdlUtils.EnumerateDocumentedItems(xmlSchema.Items, documentedItems);
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005720 File Offset: 0x00003920
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
