using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.Description
{
	// Token: 0x0200041C RID: 1052
	internal static class SchemaHelper
	{
		// Token: 0x0600283F RID: 10303 RVA: 0x000972AE File Offset: 0x000954AE
		internal static void AddElementForm(XmlSchemaElement element, XmlSchema schema)
		{
			if (schema.ElementFormDefault != XmlSchemaForm.Qualified)
			{
				element.Form = XmlSchemaForm.Qualified;
			}
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x000972C0 File Offset: 0x000954C0
		internal static void AddElementToSchema(XmlSchemaElement element, XmlSchema schema, XmlSchemaSet schemaSet)
		{
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)schema.Elements[new XmlQualifiedName(element.Name, schema.TargetNamespace)];
			if (xmlSchemaElement == null)
			{
				schema.Items.Add(element);
				if (!element.SchemaTypeName.IsEmpty)
				{
					SchemaHelper.AddImportToSchema(element.SchemaTypeName.Namespace, schema);
				}
				schemaSet.Reprocess(schema);
				return;
			}
			if (element.SchemaType == xmlSchemaElement.SchemaType && element.SchemaTypeName == xmlSchemaElement.SchemaTypeName)
			{
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxConflictingGlobalElement", new object[]
			{
				element.Name,
				schema.TargetNamespace,
				SchemaHelper.GetTypeName(element),
				SchemaHelper.GetTypeName(xmlSchemaElement)
			})));
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x0009738C File Offset: 0x0009558C
		internal static void AddImportToSchema(string ns, XmlSchema schema)
		{
			if (SchemaHelper.NamespacesEqual(ns, schema.TargetNamespace) || SchemaHelper.NamespacesEqual(ns, "http://www.w3.org/2001/XMLSchema") || SchemaHelper.NamespacesEqual(ns, "http://www.w3.org/2001/XMLSchema-instance"))
			{
				return;
			}
			foreach (object obj in schema.Includes)
			{
				if (obj is XmlSchemaImport && SchemaHelper.NamespacesEqual(ns, ((XmlSchemaImport)obj).Namespace))
				{
					return;
				}
			}
			XmlSchemaImport xmlSchemaImport = new XmlSchemaImport();
			if (ns != null && ns.Length > 0)
			{
				xmlSchemaImport.Namespace = ns;
			}
			schema.Includes.Add(xmlSchemaImport);
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x00097448 File Offset: 0x00095648
		internal static void AddTypeToSchema(XmlSchemaType type, XmlSchema schema, XmlSchemaSet schemaSet)
		{
			XmlSchemaType xmlSchemaType = (XmlSchemaType)schema.SchemaTypes[new XmlQualifiedName(type.Name, schema.TargetNamespace)];
			if (xmlSchemaType == null)
			{
				schema.Items.Add(type);
				schemaSet.Reprocess(schema);
				return;
			}
			if (xmlSchemaType == type)
			{
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxConflictingGlobalType", new object[]
			{
				type.Name,
				schema.TargetNamespace
			})));
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x000974C8 File Offset: 0x000956C8
		internal static XmlSchema GetSchema(string ns, XmlSchemaSet schemaSet)
		{
			if (ns == null)
			{
				ns = string.Empty;
			}
			ICollection collection = schemaSet.Schemas();
			foreach (object obj in collection)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				if ((xmlSchema.TargetNamespace == null && ns.Length == 0) || ns.Equals(xmlSchema.TargetNamespace))
				{
					return xmlSchema;
				}
			}
			XmlSchema xmlSchema2 = new XmlSchema();
			xmlSchema2.ElementFormDefault = XmlSchemaForm.Qualified;
			if (ns.Length > 0)
			{
				xmlSchema2.TargetNamespace = ns;
			}
			schemaSet.Add(xmlSchema2);
			return xmlSchema2;
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x00097578 File Offset: 0x00095778
		private static string GetTypeName(XmlSchemaElement element)
		{
			if (element.SchemaType != null)
			{
				return "anonymous";
			}
			if (!element.SchemaTypeName.IsEmpty)
			{
				return element.SchemaTypeName.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000975A8 File Offset: 0x000957A8
		internal static bool IsMatch(XmlSchemaElement e1, XmlSchemaElement e2)
		{
			return e1.SchemaType == null && e2.SchemaType == null && !(e1.SchemaTypeName != e2.SchemaTypeName) && e1.Form == e2.Form && e1.IsNillable == e2.IsNillable;
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x000975FD File Offset: 0x000957FD
		internal static bool NamespacesEqual(string ns1, string ns2)
		{
			if (ns1 == null || ns1.Length == 0)
			{
				return ns2 == null || ns2.Length == 0;
			}
			return ns1 == ns2;
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x00097620 File Offset: 0x00095820
		internal static void Compile(XmlSchemaSet schemaSet, Collection<MetadataConversionError> errors)
		{
			ValidationEventHandler value = delegate(object sender, ValidationEventArgs args)
			{
				SchemaHelper.HandleSchemaValidationError(sender, args, errors);
			};
			schemaSet.ValidationEventHandler += value;
			schemaSet.Compile();
			schemaSet.ValidationEventHandler -= value;
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x0009765C File Offset: 0x0009585C
		internal static void HandleSchemaValidationError(object sender, ValidationEventArgs args, Collection<MetadataConversionError> errors)
		{
			MetadataConversionError item;
			if (args.Exception != null && args.Exception.SourceUri != null)
			{
				XmlSchemaException exception = args.Exception;
				item = new MetadataConversionError(SR.GetString("SchemaValidationError", new object[]
				{
					exception.SourceUri,
					exception.LineNumber,
					exception.LinePosition,
					exception.Message
				}));
			}
			else
			{
				item = new MetadataConversionError(SR.GetString("GeneralSchemaValidationError", new object[]
				{
					args.Message
				}));
			}
			if (!errors.Contains(item))
			{
				errors.Add(item);
			}
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x000976FC File Offset: 0x000958FC
		internal static bool IsElementValueType(XmlSchemaElement element)
		{
			XmlQualifiedName schemaTypeName = element.SchemaTypeName;
			if (schemaTypeName == null || schemaTypeName.IsEmpty)
			{
				return false;
			}
			if (schemaTypeName.Namespace == "http://www.w3.org/2001/XMLSchema")
			{
				return SchemaHelper.xsdValueTypePrimitives.Contains(schemaTypeName.Name);
			}
			if (schemaTypeName.Namespace == SchemaHelper.dataContractSerializerNamespace)
			{
				return SchemaHelper.dataContractPrimitives.Contains(schemaTypeName.Name);
			}
			return schemaTypeName.Namespace == SchemaHelper.xmlSerializerNamespace && SchemaHelper.dataContractPrimitives.Contains(schemaTypeName.Name);
		}

		// Token: 0x04002221 RID: 8737
		private static IList<string> xsdValueTypePrimitives = new string[]
		{
			"boolean",
			"float",
			"double",
			"decimal",
			"long",
			"unsignedLong",
			"int",
			"unsignedInt",
			"short",
			"unsignedShort",
			"byte",
			"unsignedByte",
			"duration",
			"dateTime",
			"integer",
			"positiveInteger",
			"negativeInteger",
			"nonPositiveInteger"
		};

		// Token: 0x04002222 RID: 8738
		private static IList<string> dataContractPrimitives = new string[]
		{
			"char",
			"guid"
		};

		// Token: 0x04002223 RID: 8739
		private static IList<string> xmlSerializerPrimitives = new string[]
		{
			"char",
			"guid"
		};

		// Token: 0x04002224 RID: 8740
		private static string dataContractSerializerNamespace = "http://schemas.microsoft.com/2003/10/Serialization/";

		// Token: 0x04002225 RID: 8741
		private static string xmlSerializerNamespace = "http://microsoft.com/wsdl/types/";
	}
}
