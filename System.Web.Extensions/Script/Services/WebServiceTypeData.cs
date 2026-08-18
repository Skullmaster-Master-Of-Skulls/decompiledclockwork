using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace System.Web.Script.Services
{
	// Token: 0x020000FC RID: 252
	internal class WebServiceTypeData
	{
		// Token: 0x06000D56 RID: 3414 RVA: 0x0002D058 File Offset: 0x0002B258
		static WebServiceTypeData()
		{
			WebServiceTypeData.Add(typeof(sbyte), "byte");
			WebServiceTypeData.Add(typeof(byte), "unsignedByte");
			WebServiceTypeData.Add(typeof(short), "short");
			WebServiceTypeData.Add(typeof(ushort), "unsignedShort");
			WebServiceTypeData.Add(typeof(int), "int");
			WebServiceTypeData.Add(typeof(uint), "unsignedInt");
			WebServiceTypeData.Add(typeof(long), "long");
			WebServiceTypeData.Add(typeof(ulong), "unsignedLong");
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0002D110 File Offset: 0x0002B310
		internal WebServiceTypeData(string name, string ns, Type type)
		{
			if (string.IsNullOrEmpty(ns))
			{
				this._typeName = name;
				if (type == null)
				{
					this._stringRepresentation = name;
				}
			}
			else
			{
				this._typeName = ns + "." + name;
				if (type == null)
				{
					this._stringRepresentation = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
					{
						name,
						ns
					});
				}
			}
			this._typeNamespace = ns;
			this._actualType = type;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0002D190 File Offset: 0x0002B390
		internal WebServiceTypeData(string name, string ns) : this(name, ns, null)
		{
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x0002D19B File Offset: 0x0002B39B
		private static XmlQualifiedName ActualTypeAnnotationName
		{
			get
			{
				if (WebServiceTypeData.actualTypeAnnotationName == null)
				{
					WebServiceTypeData.actualTypeAnnotationName = new XmlQualifiedName("ActualType", "http://schemas.microsoft.com/2003/10/Serialization/");
				}
				return WebServiceTypeData.actualTypeAnnotationName;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x0002D1C3 File Offset: 0x0002B3C3
		private static XmlQualifiedName EnumerationValueAnnotationName
		{
			get
			{
				if (WebServiceTypeData.enumerationValueAnnotationName == null)
				{
					WebServiceTypeData.enumerationValueAnnotationName = new XmlQualifiedName("EnumerationValue", "http://schemas.microsoft.com/2003/10/Serialization/");
				}
				return WebServiceTypeData.enumerationValueAnnotationName;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0002D1EB File Offset: 0x0002B3EB
		internal string StringRepresentation
		{
			get
			{
				return this._stringRepresentation;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0002D1F3 File Offset: 0x0002B3F3
		internal string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0002D1FB File Offset: 0x0002B3FB
		internal string TypeNamespace
		{
			get
			{
				return this._typeNamespace;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0002D203 File Offset: 0x0002B403
		internal Type Type
		{
			get
			{
				return this._actualType;
			}
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0002D20C File Offset: 0x0002B40C
		private static void Add(Type type, string localName)
		{
			XmlQualifiedName key = new XmlQualifiedName(localName, "http://www.w3.org/2001/XMLSchema");
			WebServiceTypeData._nameToType.Add(key, type);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0002D234 File Offset: 0x0002B434
		private static bool CheckIfCollection(XmlSchemaComplexType type)
		{
			if (type == null)
			{
				return false;
			}
			bool result = false;
			if (type.ContentModel == null)
			{
				result = WebServiceTypeData.CheckIfCollectionSequence(type.Particle as XmlSchemaSequence);
			}
			return result;
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0002D264 File Offset: 0x0002B464
		private static bool CheckIfCollectionSequence(XmlSchemaSequence rootSequence)
		{
			if (rootSequence.Items == null || rootSequence.Items.Count == 0)
			{
				return false;
			}
			if (rootSequence.Items.Count != 1)
			{
				return false;
			}
			XmlSchemaObject xmlSchemaObject = rootSequence.Items[0];
			if (!(xmlSchemaObject is XmlSchemaElement))
			{
				return false;
			}
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)xmlSchemaObject;
			return xmlSchemaElement.MaxOccursString == "unbounded" || xmlSchemaElement.MaxOccurs > 1m;
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0002D2DC File Offset: 0x0002B4DC
		private static bool CheckIfEnum(XmlSchemaSimpleType simpleType, out XmlSchemaSimpleTypeRestriction simpleTypeRestriction)
		{
			simpleTypeRestriction = null;
			if (simpleType == null)
			{
				return false;
			}
			XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = simpleType.Content as XmlSchemaSimpleTypeRestriction;
			if (xmlSchemaSimpleTypeRestriction != null)
			{
				simpleTypeRestriction = xmlSchemaSimpleTypeRestriction;
				return WebServiceTypeData.CheckIfEnumRestriction(xmlSchemaSimpleTypeRestriction);
			}
			XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = simpleType.Content as XmlSchemaSimpleTypeList;
			XmlSchemaSimpleType itemType = xmlSchemaSimpleTypeList.ItemType;
			if (itemType != null)
			{
				xmlSchemaSimpleTypeRestriction = (itemType.Content as XmlSchemaSimpleTypeRestriction);
				if (xmlSchemaSimpleTypeRestriction != null)
				{
					simpleTypeRestriction = xmlSchemaSimpleTypeRestriction;
					return WebServiceTypeData.CheckIfEnumRestriction(xmlSchemaSimpleTypeRestriction);
				}
			}
			return false;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0002D33C File Offset: 0x0002B53C
		private static bool CheckIfEnumRestriction(XmlSchemaSimpleTypeRestriction restriction)
		{
			foreach (XmlSchemaObject xmlSchemaObject in restriction.Facets)
			{
				XmlSchemaFacet xmlSchemaFacet = (XmlSchemaFacet)xmlSchemaObject;
				if (!(xmlSchemaFacet is XmlSchemaEnumerationFacet))
				{
					return false;
				}
			}
			return restriction.BaseTypeName != XmlQualifiedName.Empty && (restriction.BaseTypeName.Name == "string" && restriction.BaseTypeName.Namespace == "http://www.w3.org/2001/XMLSchema") && restriction.Facets.Count > 0;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0002D3F0 File Offset: 0x0002B5F0
		private static string GetInnerText(XmlQualifiedName typeName, XmlElement xmlElement)
		{
			if (xmlElement != null)
			{
				for (XmlNode xmlNode = xmlElement.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						throw new InvalidOperationException();
					}
				}
				return xmlElement.InnerText;
			}
			return null;
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0002D42C File Offset: 0x0002B62C
		internal static List<WebServiceTypeData> GetKnownTypes(Type type, WebServiceTypeData typeData)
		{
			List<WebServiceTypeData> list = new List<WebServiceTypeData>();
			XsdDataContractExporter xsdDataContractExporter = new XsdDataContractExporter();
			xsdDataContractExporter.Export(type);
			ICollection collection = xsdDataContractExporter.Schemas.Schemas();
			foreach (object obj in collection)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				if (!(xmlSchema.TargetNamespace == "http://schemas.microsoft.com/2003/10/Serialization/"))
				{
					foreach (XmlSchemaObject xmlSchemaObject in xmlSchema.Items)
					{
						XmlSchemaType xmlSchemaType = xmlSchemaObject as XmlSchemaType;
						string text = XmlConvert.DecodeName(xmlSchema.TargetNamespace);
						if (xmlSchemaType != null && (!(xmlSchemaType.Name == typeData.TypeName) || !(text == typeData.TypeNamespace)) && !string.IsNullOrEmpty(xmlSchemaType.Name))
						{
							WebServiceTypeData webServiceTypeData = null;
							XmlSchemaSimpleTypeRestriction restriction;
							if (WebServiceTypeData.CheckIfEnum(xmlSchemaType as XmlSchemaSimpleType, out restriction))
							{
								webServiceTypeData = WebServiceTypeData.ImportEnum(XmlConvert.DecodeName(xmlSchemaType.Name), text, xmlSchemaType.QualifiedName, restriction, xmlSchemaType.Annotation);
							}
							else
							{
								if (WebServiceTypeData.CheckIfCollection(xmlSchemaType as XmlSchemaComplexType))
								{
									continue;
								}
								if (!(xmlSchemaType is XmlSchemaSimpleType))
								{
									webServiceTypeData = new WebServiceTypeData(XmlConvert.DecodeName(xmlSchemaType.Name), text);
								}
							}
							if (webServiceTypeData != null)
							{
								list.Add(webServiceTypeData);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0002D5E0 File Offset: 0x0002B7E0
		internal static WebServiceTypeData GetWebServiceTypeData(Type type)
		{
			WebServiceTypeData result = null;
			XsdDataContractExporter xsdDataContractExporter = new XsdDataContractExporter();
			XmlQualifiedName schemaTypeName = xsdDataContractExporter.GetSchemaTypeName(type);
			if (!schemaTypeName.IsEmpty)
			{
				if (type.IsEnum)
				{
					bool isULong = Enum.GetUnderlyingType(type) == typeof(ulong);
					result = new WebServiceEnumData(XmlConvert.DecodeName(schemaTypeName.Name), XmlConvert.DecodeName(schemaTypeName.Namespace), Enum.GetNames(type), Enum.GetValues(type), isULong);
				}
				else
				{
					result = new WebServiceTypeData(XmlConvert.DecodeName(schemaTypeName.Name), XmlConvert.DecodeName(schemaTypeName.Namespace));
				}
			}
			return result;
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0002D66C File Offset: 0x0002B86C
		internal static XmlQualifiedName ImportActualType(XmlSchemaAnnotation annotation, XmlQualifiedName defaultTypeName, XmlQualifiedName typeName)
		{
			XmlElement xmlElement = WebServiceTypeData.ImportAnnotation(annotation, WebServiceTypeData.ActualTypeAnnotationName);
			if (xmlElement == null)
			{
				return defaultTypeName;
			}
			XmlNode namedItem = xmlElement.Attributes.GetNamedItem("Name");
			string value = namedItem.Value;
			XmlNode namedItem2 = xmlElement.Attributes.GetNamedItem("Namespace");
			string value2 = namedItem2.Value;
			return new XmlQualifiedName(value, value2);
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0002D6C4 File Offset: 0x0002B8C4
		private static XmlElement ImportAnnotation(XmlSchemaAnnotation annotation, XmlQualifiedName annotationQualifiedName)
		{
			if (annotation != null && annotation.Items != null && annotation.Items.Count > 0 && annotation.Items[0] is XmlSchemaAppInfo)
			{
				XmlSchemaAppInfo xmlSchemaAppInfo = (XmlSchemaAppInfo)annotation.Items[0];
				XmlNode[] markup = xmlSchemaAppInfo.Markup;
				if (markup != null)
				{
					for (int i = 0; i < markup.Length; i++)
					{
						XmlElement xmlElement = markup[i] as XmlElement;
						if (xmlElement != null && xmlElement.LocalName == annotationQualifiedName.Name && xmlElement.NamespaceURI == annotationQualifiedName.Namespace)
						{
							return xmlElement;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0002D760 File Offset: 0x0002B960
		private static WebServiceEnumData ImportEnum(string typeName, string typeNamespace, XmlQualifiedName typeQualifiedName, XmlSchemaSimpleTypeRestriction restriction, XmlSchemaAnnotation annotation)
		{
			XmlQualifiedName key = WebServiceTypeData.ImportActualType(annotation, new XmlQualifiedName("int", "http://www.w3.org/2001/XMLSchema"), typeQualifiedName);
			Type left = WebServiceTypeData._nameToType[key];
			bool flag = left == typeof(ulong);
			List<string> list = new List<string>();
			List<long> list2 = new List<long>();
			foreach (XmlSchemaObject xmlSchemaObject in restriction.Facets)
			{
				XmlSchemaFacet xmlSchemaFacet = (XmlSchemaFacet)xmlSchemaObject;
				XmlSchemaEnumerationFacet xmlSchemaEnumerationFacet = xmlSchemaFacet as XmlSchemaEnumerationFacet;
				string innerText = WebServiceTypeData.GetInnerText(typeQualifiedName, WebServiceTypeData.ImportAnnotation(xmlSchemaEnumerationFacet.Annotation, WebServiceTypeData.EnumerationValueAnnotationName));
				long item;
				if (innerText == null)
				{
					item = (long)list.Count;
				}
				else if (flag)
				{
					item = (long)ulong.Parse(innerText, NumberFormatInfo.InvariantInfo);
				}
				else
				{
					item = long.Parse(innerText, NumberFormatInfo.InvariantInfo);
				}
				list.Add(xmlSchemaEnumerationFacet.Value);
				list2.Add(item);
			}
			return new WebServiceEnumData(typeName, typeNamespace, list.ToArray(), list2.ToArray(), flag);
		}

		// Token: 0x040003B7 RID: 951
		private Type _actualType;

		// Token: 0x040003B8 RID: 952
		private string _stringRepresentation;

		// Token: 0x040003B9 RID: 953
		private string _typeName;

		// Token: 0x040003BA RID: 954
		private string _typeNamespace;

		// Token: 0x040003BB RID: 955
		private static Dictionary<XmlQualifiedName, Type> _nameToType = new Dictionary<XmlQualifiedName, Type>();

		// Token: 0x040003BC RID: 956
		private const string SerializationNamespace = "http://schemas.microsoft.com/2003/10/Serialization/";

		// Token: 0x040003BD RID: 957
		private const string StringLocalName = "string";

		// Token: 0x040003BE RID: 958
		private const string SchemaNamespace = "http://www.w3.org/2001/XMLSchema";

		// Token: 0x040003BF RID: 959
		private const string ActualTypeLocalName = "ActualType";

		// Token: 0x040003C0 RID: 960
		private const string ActualTypeNameAttribute = "Name";

		// Token: 0x040003C1 RID: 961
		private const string ActualTypeNamespaceAttribute = "Namespace";

		// Token: 0x040003C2 RID: 962
		private const string EnumerationValueLocalName = "EnumerationValue";

		// Token: 0x040003C3 RID: 963
		private const string OccursUnbounded = "unbounded";

		// Token: 0x040003C4 RID: 964
		private static XmlQualifiedName actualTypeAnnotationName;

		// Token: 0x040003C5 RID: 965
		private static XmlQualifiedName enumerationValueAnnotationName;
	}
}
