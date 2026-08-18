using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;
using WCFExtrasPlus.Utils;

namespace WCFExtrasPlus.Wsdl.Documentation
{
	// Token: 0x02000015 RID: 21
	internal class XmlCommentsExporter
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003A98 File Offset: 0x00001C98
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

		// Token: 0x06000070 RID: 112 RVA: 0x00003B2C File Offset: 0x00001D2C
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
										string localName2 = xmlAttribute.Value.Split(new char[]
										{
											':'
										})[0];
										xmlAttribute = xmlNode.Attributes[localName2, "http://www.w3.org/2000/xmlns/"];
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

		// Token: 0x06000071 RID: 113 RVA: 0x00003E6C File Offset: 0x0000206C
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

		// Token: 0x06000072 RID: 114 RVA: 0x00004074 File Offset: 0x00002274
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

		// Token: 0x06000073 RID: 115 RVA: 0x00004268 File Offset: 0x00002468
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

		// Token: 0x06000074 RID: 116 RVA: 0x00004288 File Offset: 0x00002488
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

		// Token: 0x06000075 RID: 117 RVA: 0x0000432C File Offset: 0x0000252C
		internal static void ExportContract(WsdlExporter exporter, WsdlContractConversionContext context, XmlCommentFormat format)
		{
			XmlCommentsExporter.InitXsdDataContractExporter(exporter, format);
			XmlDocument xmlDocument = XmlCommentsUtils.LoadXmlComments(context.Contract.ContractType);
			if (xmlDocument == null)
			{
				return;
			}
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
				if (memberInfo == null)
				{
					memberInfo = operationDescription.TaskMethod;
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
