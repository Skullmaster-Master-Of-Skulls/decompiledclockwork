using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;
using WCFExtras.Utils;

namespace WCFExtras.Wsdl.Documentation
{
	// Token: 0x0200001F RID: 31
	internal class XmlCommentsExporter
	{
		// Token: 0x060000BF RID: 191 RVA: 0x000052E0 File Offset: 0x000034E0
		private static void InitXsdDataContractExporter(WsdlExporter exporter, XmlCommentFormat format)
		{
			object obj;
			XsdDataContractExporter xsdDataContractExporter;
			if (!exporter.State.TryGetValue(typeof(XsdDataContractExporter), out obj))
			{
				xsdDataContractExporter = new XsdDataContractExporter(exporter.GeneratedXmlSchemas);
				exporter.State.Add(typeof(XsdDataContractExporter), xsdDataContractExporter);
			}
			else
			{
				xsdDataContractExporter = (XsdDataContractExporter)obj;
			}
			if (xsdDataContractExporter.Options == null)
			{
				xsdDataContractExporter.Options = new ExportOptions();
			}
			if (!(xsdDataContractExporter.Options.DataContractSurrogate is XmlCommentsDataSurrogate))
			{
				xsdDataContractExporter.Options.DataContractSurrogate = new XmlCommentsDataSurrogate(xsdDataContractExporter.Options.DataContractSurrogate, format);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000538C File Offset: 0x0000358C
		private static void ConvertObjectAnnotation(XmlSchemaObject schemaObj)
		{
			XmlSchemaAnnotated xmlSchemaAnnotated = schemaObj as XmlSchemaAnnotated;
			if (xmlSchemaAnnotated != null && xmlSchemaAnnotated.Annotation != null)
			{
				XmlSchemaDocumentation xmlSchemaDocumentation = null;
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchemaAnnotated.Annotation.Items)
				{
					XmlSchemaAppInfo xmlSchemaAppInfo = xmlSchemaObject as XmlSchemaAppInfo;
					if (xmlSchemaAppInfo != null)
					{
						for (int i = 0; i < xmlSchemaAppInfo.Markup.Length; i++)
						{
							XmlNode xmlNode = xmlSchemaAppInfo.Markup[i];
							if (xmlNode != null)
							{
								XmlAttribute xmlAttribute = xmlNode.Attributes["type", "http://www.w3.org/2001/XMLSchema-instance"];
								if (xmlAttribute != null)
								{
									if (xmlAttribute.Value.Contains(":Annotation"))
									{
										string localName = xmlAttribute.Value.Split(new char[]
										{
											':'
										})[0];
										xmlAttribute = xmlNode.Attributes[localName, "http://www.w3.org/2000/xmlns/"];
										if (xmlAttribute != null && xmlAttribute.Value == "XmlCommentsExporter.Annotation")
										{
											xmlSchemaDocumentation = XmlCommentsExporter.CreateDocumentationItem(xmlNode.InnerText);
											xmlSchemaAppInfo.Markup[i] = null;
											break;
										}
									}
									else if (xmlAttribute.Value.Contains(":EnumAnnotation"))
									{
										string localName = xmlAttribute.Value.Split(new char[]
										{
											':'
										})[0];
										xmlAttribute = xmlNode.Attributes[localName, "http://www.w3.org/2000/xmlns/"];
										if (xmlAttribute != null && xmlAttribute.Value == "XmlCommentsExporter.Annotation")
										{
											DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(EnumAnnotation));
											using (XmlReader xmlReader = new XmlNodeReader(xmlNode))
											{
												EnumAnnotation enumAnnotation = (EnumAnnotation)dataContractSerializer.ReadObject(xmlReader, false);
												if (enumAnnotation.EnumText != null)
												{
													xmlSchemaDocumentation = XmlCommentsExporter.CreateDocumentationItem(enumAnnotation.EnumText);
												}
												if (enumAnnotation.Members.Count > 0)
												{
													foreach (XmlSchemaObject xmlSchemaObject2 in XmlCommentsExporter.GetEnumItems(schemaObj))
													{
														XmlSchemaEnumerationFacet xmlSchemaEnumerationFacet = (XmlSchemaEnumerationFacet)xmlSchemaObject2;
														string text;
														if (enumAnnotation.Members.TryGetValue(xmlSchemaEnumerationFacet.Value, out text))
														{
															if (xmlSchemaEnumerationFacet.Annotation == null)
															{
																xmlSchemaEnumerationFacet.Annotation = new XmlSchemaAnnotation();
															}
															xmlSchemaEnumerationFacet.Annotation.Items.Add(XmlCommentsExporter.CreateDocumentationItem(text));
														}
													}
												}
												xmlSchemaAppInfo.Markup[i] = null;
												break;
											}
										}
									}
								}
							}
						}
					}
				}
				if (xmlSchemaDocumentation != null)
				{
					xmlSchemaAnnotated.Annotation.Items.Add(xmlSchemaDocumentation);
				}
			}
			foreach (XmlSchemaObject schemaObj2 in XmlCommentsExporter.GetSubItems(schemaObj))
			{
				XmlCommentsExporter.ConvertObjectAnnotation(schemaObj2);
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005788 File Offset: 0x00003988
		private static XmlSchemaDocumentation CreateDocumentationItem(string text)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlSchemaDocumentation xmlSchemaDocumentation = new XmlSchemaDocumentation();
			XmlNode xmlNode = xmlDocument.CreateTextNode(text);
			xmlSchemaDocumentation.Markup = new XmlNode[]
			{
				xmlNode
			};
			return xmlSchemaDocumentation;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005A20 File Offset: 0x00003C20
		private static IEnumerable<XmlSchemaObject> GetEnumItems(XmlSchemaObject schemaObj)
		{
			XmlSchemaSimpleType simpleType = schemaObj as XmlSchemaSimpleType;
			if (simpleType != null)
			{
				XmlSchemaSimpleTypeRestriction restriction = simpleType.Content as XmlSchemaSimpleTypeRestriction;
				if (restriction != null)
				{
					foreach (XmlSchemaObject obj in restriction.Facets)
					{
						yield return obj;
					}
				}
			}
			yield break;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005CA0 File Offset: 0x00003EA0
		private static IEnumerable<XmlSchemaObject> GetSubItems(XmlSchemaObject schemaObj)
		{
			XmlSchemaComplexType complexType = schemaObj as XmlSchemaComplexType;
			if (complexType != null)
			{
				XmlSchemaSequence seq = complexType.ContentTypeParticle as XmlSchemaSequence;
				if (seq != null)
				{
					foreach (XmlSchemaObject subObj in seq.Items)
					{
						yield return subObj;
					}
				}
			}
			yield break;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005CC4 File Offset: 0x00003EC4
		internal static void ExportEndpoint(WsdlExporter exporter, XmlCommentFormat format)
		{
			foreach (object obj in exporter.GeneratedXmlSchemas.Schemas())
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				foreach (XmlSchemaObject schemaObj in xmlSchema.Items)
				{
					XmlCommentsExporter.ConvertObjectAnnotation(schemaObj);
				}
			}
			XmlCommentsUtils.ClearCache();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005D8C File Offset: 0x00003F8C
		internal static void ExportContract(WsdlExporter exporter, WsdlContractConversionContext context, XmlCommentFormat format)
		{
			XmlCommentsExporter.InitXsdDataContractExporter(exporter, format);
			XmlDocument xmlDocument = XmlCommentsUtils.LoadXmlComments(context.Contract.ContractType, true);
			if (xmlDocument != null)
			{
				string formattedComment = XmlCommentsUtils.GetFormattedComment(xmlDocument, context.Contract.ContractType, format);
				if (formattedComment != null)
				{
					context.WsdlPortType.Documentation = formattedComment;
				}
				foreach (object obj in context.WsdlPortType.Operations)
				{
					Operation operation = (Operation)obj;
					OperationDescription operationDescription = context.GetOperationDescription(operation);
					MemberInfo memberInfo = operationDescription.SyncMethod;
					if (memberInfo == null)
					{
						memberInfo = operationDescription.BeginMethod;
					}
					formattedComment = XmlCommentsUtils.GetFormattedComment(xmlDocument, memberInfo, format);
					if (formattedComment != null)
					{
						operation.Documentation = formattedComment;
					}
				}
			}
		}
	}
}
