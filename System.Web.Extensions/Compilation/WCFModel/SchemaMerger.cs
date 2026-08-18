using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Web.Resources;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000022 RID: 34
	internal class SchemaMerger
	{
		// Token: 0x06000149 RID: 329 RVA: 0x00004C28 File Offset: 0x00002E28
		internal static void MergeSchemas(IEnumerable<XmlSchema> schemaList, IList<ProxyGenerationError> importErrors, out IEnumerable<XmlSchema> duplicatedSchemas)
		{
			if (schemaList == null)
			{
				throw new ArgumentNullException("schemaList");
			}
			if (importErrors == null)
			{
				throw new ArgumentNullException("importErrors");
			}
			List<XmlSchema> list = new List<XmlSchema>();
			duplicatedSchemas = list;
			Dictionary<XmlQualifiedName, XmlSchemaObject>[] array = new Dictionary<XmlQualifiedName, XmlSchemaObject>[SchemaMerger.schemaTopLevelItemTypes.Length];
			for (int i = 0; i < SchemaMerger.schemaTopLevelItemTypes.Length; i++)
			{
				array[i] = new Dictionary<XmlQualifiedName, XmlSchemaObject>();
			}
			foreach (XmlSchema xmlSchema in schemaList)
			{
				bool flag = false;
				List<XmlSchemaObject> list2 = new List<XmlSchemaObject>();
				for (int j = 0; j < SchemaMerger.schemaTopLevelItemTypes.Length; j++)
				{
					Dictionary<XmlQualifiedName, XmlSchemaObject> dictionary = array[j];
					int count = dictionary.Count;
					SchemaMerger.FindDuplicatedItems(xmlSchema, SchemaMerger.schemaTopLevelItemTypes[j].ItemType, SchemaMerger.schemaTopLevelItemTypes[j].Name, dictionary, list2, importErrors);
					if (dictionary.Count > count)
					{
						flag = true;
					}
				}
				if (list2.Count > 0)
				{
					if (!flag)
					{
						list.Add(xmlSchema);
					}
					else
					{
						foreach (XmlSchemaObject item in list2)
						{
							xmlSchema.Items.Remove(item);
						}
					}
				}
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004D88 File Offset: 0x00002F88
		private static void FindDuplicatedItems(XmlSchema schema, Type itemType, string itemTypeName, Dictionary<XmlQualifiedName, XmlSchemaObject> knownItemTable, List<XmlSchemaObject> duplicatedItems, IList<ProxyGenerationError> importErrors)
		{
			string text = schema.TargetNamespace;
			if (string.IsNullOrEmpty(text))
			{
				text = string.Empty;
			}
			foreach (XmlSchemaObject xmlSchemaObject in schema.Items)
			{
				if (itemType.IsInstanceOfType(xmlSchemaObject))
				{
					XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(SchemaMerger.GetSchemaItemName(xmlSchemaObject), text);
					XmlSchemaObject xmlSchemaObject2 = null;
					if (knownItemTable.TryGetValue(xmlQualifiedName, out xmlSchemaObject2))
					{
						string text2;
						if (!SchemaMerger.AreSchemaObjectsEquivalent(xmlSchemaObject2, xmlSchemaObject, out text2))
						{
							text2 = SchemaMerger.CombinePath(".", text2);
							importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.MergeMetadata, string.Empty, new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, WCFModelStrings.ReferenceGroup_DuplicatedSchemaItems, new object[]
							{
								itemTypeName,
								xmlQualifiedName.ToString(),
								schema.SourceUri,
								xmlSchemaObject2.SourceUri,
								text2
							}))));
						}
						else if (!string.IsNullOrEmpty(text2))
						{
							text2 = SchemaMerger.CombinePath(".", text2);
							importErrors.Add(new ProxyGenerationError(ProxyGenerationError.GeneratorState.MergeMetadata, string.Empty, new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, WCFModelStrings.ReferenceGroup_DuplicatedSchemaItemsIgnored, new object[]
							{
								itemTypeName,
								xmlQualifiedName.ToString(),
								schema.SourceUri,
								xmlSchemaObject2.SourceUri,
								text2
							})), true));
						}
						duplicatedItems.Add(xmlSchemaObject);
					}
					else
					{
						xmlSchemaObject.SourceUri = schema.SourceUri;
						knownItemTable.Add(xmlQualifiedName, xmlSchemaObject);
					}
				}
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004F20 File Offset: 0x00003120
		private static bool AreSchemaObjectsEquivalent(XmlSchemaObject originalItem, XmlSchemaObject item, out string differentLocation)
		{
			differentLocation = string.Empty;
			Type type = originalItem.GetType();
			if (type != item.GetType())
			{
				return false;
			}
			string text = string.Empty;
			PropertyInfo[] properties = type.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (SchemaMerger.IsPersistedProperty(propertyInfo))
				{
					bool flag = SchemaMerger.ShouldIgnoreSchemaProperty(propertyInfo);
					object value = propertyInfo.GetValue(originalItem, new object[0]);
					object value2 = propertyInfo.GetValue(item, new object[0]);
					if (!SchemaMerger.CompareSchemaPropertyValues(propertyInfo, value, value2, out differentLocation) && !flag)
					{
						return false;
					}
					if (string.IsNullOrEmpty(text))
					{
						text = differentLocation;
					}
				}
			}
			differentLocation = text;
			return true;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004FC8 File Offset: 0x000031C8
		private static bool CompareSchemaPropertyValues(PropertyInfo propertyInfo, object originalValue, object newValue, out string differentLocation)
		{
			differentLocation = string.Empty;
			if (originalValue == null && newValue == null)
			{
				return true;
			}
			if (typeof(XmlAttribute[]) == propertyInfo.PropertyType)
			{
				if (originalValue == null)
				{
					originalValue = SchemaMerger.emptyXmlAttributeCollection;
				}
				if (newValue == null)
				{
					newValue = SchemaMerger.emptyXmlAttributeCollection;
				}
				XmlAttribute value;
				XmlAttribute value2;
				if (!SchemaMerger.CompareXmlAttributeCollections((XmlAttribute[])originalValue, (XmlAttribute[])newValue, out value, out value2))
				{
					differentLocation = SchemaMerger.GetSchemaPropertyNameInXml(propertyInfo, value, value2);
					return false;
				}
				return true;
			}
			else if (typeof(ICollection).IsAssignableFrom(propertyInfo.PropertyType))
			{
				if (originalValue == null)
				{
					originalValue = SchemaMerger.emptyCollection;
				}
				if (newValue == null)
				{
					newValue = SchemaMerger.emptyCollection;
				}
				object value3;
				object value4;
				if (!SchemaMerger.CompareSchemaCollections((ICollection)originalValue, (ICollection)newValue, out value3, out value4, out differentLocation))
				{
					differentLocation = SchemaMerger.CombinePath(SchemaMerger.GetSchemaPropertyNameInXml(propertyInfo, value3, value4), differentLocation);
					return false;
				}
				if (!string.IsNullOrEmpty(differentLocation))
				{
					differentLocation = SchemaMerger.CombinePath(SchemaMerger.GetSchemaPropertyNameInXml(propertyInfo, value3, value4), differentLocation);
				}
				return true;
			}
			else
			{
				if (originalValue == null || newValue == null)
				{
					differentLocation = SchemaMerger.CombinePath(SchemaMerger.GetSchemaPropertyNameInXml(propertyInfo, originalValue, newValue), differentLocation);
					return false;
				}
				if (originalValue.GetType() != newValue.GetType())
				{
					differentLocation = SchemaMerger.CombinePath(SchemaMerger.GetSchemaPropertyNameInXml(propertyInfo, originalValue, newValue), differentLocation);
					return false;
				}
				if (!SchemaMerger.CompareSchemaValues(originalValue, newValue, out differentLocation))
				{
					differentLocation = SchemaMerger.CombinePath(SchemaMerger.GetSchemaPropertyNameInXml(propertyInfo, originalValue, newValue), differentLocation);
					return false;
				}
				if (!string.IsNullOrEmpty(differentLocation))
				{
					differentLocation = SchemaMerger.CombinePath(SchemaMerger.GetSchemaPropertyNameInXml(propertyInfo, originalValue, newValue), differentLocation);
				}
				return true;
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005120 File Offset: 0x00003320
		private static bool CompareSchemaValues(object originalValue, object newValue, out string differentLocation)
		{
			differentLocation = string.Empty;
			if (originalValue == null || newValue == null)
			{
				return originalValue == null && newValue == null;
			}
			if (originalValue.GetType() != newValue.GetType())
			{
				return false;
			}
			if (originalValue is XmlSchemaObject)
			{
				return SchemaMerger.AreSchemaObjectsEquivalent((XmlSchemaObject)originalValue, (XmlSchemaObject)newValue, out differentLocation);
			}
			if (originalValue is XmlAttribute)
			{
				return SchemaMerger.CompareXmlAttributes((XmlAttribute)originalValue, (XmlAttribute)newValue);
			}
			if (originalValue is XmlElement)
			{
				return SchemaMerger.CompareXmlElements((XmlElement)originalValue, (XmlElement)newValue, out differentLocation);
			}
			if (originalValue is XmlText)
			{
				return SchemaMerger.CompareXmlTexts((XmlText)originalValue, (XmlText)newValue);
			}
			return originalValue.Equals(newValue);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000051CC File Offset: 0x000033CC
		private static bool CompareSchemaCollections(IEnumerable originalCollection, IEnumerable newCollection, out object differentItem1, out object differentItem2, out string differentLocation)
		{
			differentLocation = string.Empty;
			IEnumerator enumerator = originalCollection.GetEnumerator();
			IEnumerator enumerator2 = newCollection.GetEnumerator();
			string text = string.Empty;
			object obj = null;
			object obj2 = null;
			for (;;)
			{
				differentItem1 = (enumerator.MoveNext() ? enumerator.Current : null);
				differentItem2 = (enumerator2.MoveNext() ? enumerator2.Current : null);
				if (!SchemaMerger.CompareSchemaValues(differentItem1, differentItem2, out differentLocation))
				{
					break;
				}
				if (string.IsNullOrEmpty(text))
				{
					obj = differentItem1;
					obj2 = differentItem2;
					text = differentLocation;
				}
				if (differentItem1 == null || differentItem2 == null)
				{
					goto IL_71;
				}
			}
			return false;
			IL_71:
			differentLocation = text;
			differentItem1 = obj;
			differentItem2 = obj2;
			return true;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005256 File Offset: 0x00003456
		private static bool CompareXmlAttributes(XmlAttribute attribute1, XmlAttribute attribute2)
		{
			return string.Equals(attribute1.LocalName, attribute2.LocalName, StringComparison.Ordinal) && string.Equals(attribute1.NamespaceURI, attribute2.NamespaceURI, StringComparison.Ordinal) && string.Equals(attribute1.Value, attribute2.Value, StringComparison.Ordinal);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005294 File Offset: 0x00003494
		private static bool CompareXmlAttributeCollections(ICollection attributeCollection1, ICollection attributeCollection2, out XmlAttribute differentAttribute1, out XmlAttribute differentAttribute2)
		{
			differentAttribute1 = null;
			differentAttribute2 = null;
			XmlAttribute[] sortedAttributeArray = SchemaMerger.GetSortedAttributeArray(attributeCollection1);
			XmlAttribute[] sortedAttributeArray2 = SchemaMerger.GetSortedAttributeArray(attributeCollection2);
			object obj;
			object obj2;
			string text;
			if (!SchemaMerger.CompareSchemaCollections(sortedAttributeArray, sortedAttributeArray2, out obj, out obj2, out text))
			{
				differentAttribute1 = (XmlAttribute)obj;
				differentAttribute2 = (XmlAttribute)obj2;
				return false;
			}
			return true;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000052D8 File Offset: 0x000034D8
		private static XmlAttribute[] GetSortedAttributeArray(ICollection attributeCollection)
		{
			XmlAttribute[] array = new XmlAttribute[attributeCollection.Count];
			int num = 0;
			foreach (object obj in attributeCollection)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				array[num++] = xmlAttribute;
			}
			Array.Sort<XmlAttribute>(array, new SchemaMerger.AttributeComparer());
			return array;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000534C File Offset: 0x0000354C
		private static bool CompareXmlElements(XmlElement element1, XmlElement element2, out string differentLocation)
		{
			differentLocation = string.Empty;
			if (!string.Equals(element1.LocalName, element2.LocalName, StringComparison.Ordinal) || !string.Equals(element1.NamespaceURI, element2.NamespaceURI, StringComparison.Ordinal))
			{
				return false;
			}
			XmlAttribute xmlAttribute;
			XmlAttribute xmlAttribute2;
			if (!SchemaMerger.CompareXmlAttributeCollections(element1.Attributes, element2.Attributes, out xmlAttribute, out xmlAttribute2))
			{
				string name = (xmlAttribute != null) ? ("@" + xmlAttribute.LocalName) : string.Empty;
				string name2 = (xmlAttribute2 != null) ? ("@" + xmlAttribute2.LocalName) : string.Empty;
				differentLocation = SchemaMerger.CombineTwoNames(name, name2);
				return false;
			}
			object obj;
			object obj2;
			if (!SchemaMerger.CompareSchemaCollections(element1.ChildNodes, element2.ChildNodes, out obj, out obj2, out differentLocation))
			{
				string name3 = (obj != null) ? ((XmlNode)obj).LocalName : string.Empty;
				string name4 = (obj2 != null) ? ((XmlNode)obj2).LocalName : string.Empty;
				differentLocation = SchemaMerger.CombinePath(SchemaMerger.CombineTwoNames(name3, name4), differentLocation);
				return false;
			}
			return true;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005441 File Offset: 0x00003641
		private static bool CompareXmlTexts(XmlText text1, XmlText text2)
		{
			return string.Equals(text1.Value, text2.Value, StringComparison.Ordinal);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005455 File Offset: 0x00003655
		private static string CombinePath(string path1, string path2)
		{
			if (string.IsNullOrEmpty(path1))
			{
				return path2;
			}
			if (string.IsNullOrEmpty(path2))
			{
				return path1;
			}
			return path1 + "/" + path2;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005478 File Offset: 0x00003678
		private static string GetSchemaItemName(XmlSchemaObject item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			Type type = item.GetType();
			PropertyInfo property = type.GetProperty("Name");
			if (!(property != null))
			{
				return string.Empty;
			}
			object value = property.GetValue(item, new object[0]);
			if (value is string)
			{
				return (string)value;
			}
			return string.Empty;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000054D8 File Offset: 0x000036D8
		private static string GetSchemaPropertyNameInXml(PropertyInfo property, object value1, object value2)
		{
			object[] customAttributes = property.GetCustomAttributes(true);
			string text = string.Empty;
			if (customAttributes != null)
			{
				string schemaPropertyNameInXmlHelper = SchemaMerger.GetSchemaPropertyNameInXmlHelper(customAttributes, value1);
				string schemaPropertyNameInXmlHelper2 = SchemaMerger.GetSchemaPropertyNameInXmlHelper(customAttributes, value2);
				text = SchemaMerger.CombineTwoNames(schemaPropertyNameInXmlHelper, schemaPropertyNameInXmlHelper2);
			}
			if (string.IsNullOrEmpty(text))
			{
				text = property.Name;
			}
			return text;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005520 File Offset: 0x00003720
		private static string CombineTwoNames(string name1, string name2)
		{
			string result = string.Empty;
			if (name1.Length > 0)
			{
				if (name2.Length > 0)
				{
					if (string.Equals(name1, name2, StringComparison.Ordinal))
					{
						result = name1;
					}
					else
					{
						result = name1 + "|" + name2;
					}
				}
				else
				{
					result = name1;
				}
			}
			else if (name2.Length > 0)
			{
				result = name2;
			}
			return result;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005574 File Offset: 0x00003774
		private static string GetSchemaPropertyNameInXmlHelper(object[] propertyAttributes, object value)
		{
			if (value != null)
			{
				foreach (object obj in propertyAttributes)
				{
					if (obj is XmlAttributeAttribute)
					{
						return "@" + ((XmlAttributeAttribute)obj).AttributeName;
					}
					if (obj is XmlElementAttribute)
					{
						XmlElementAttribute xmlElementAttribute = (XmlElementAttribute)obj;
						Type type = xmlElementAttribute.Type;
						if (type == null || type.IsInstanceOfType(value))
						{
							if (value is XmlSchemaObject)
							{
								string schemaItemName = SchemaMerger.GetSchemaItemName((XmlSchemaObject)value);
								if (schemaItemName.Length > 0)
								{
									return string.Format(CultureInfo.InvariantCulture, "{0}[@name='{1}']", new object[]
									{
										xmlElementAttribute.ElementName,
										schemaItemName
									});
								}
							}
							return xmlElementAttribute.ElementName;
						}
					}
					if (obj is XmlAnyAttributeAttribute && value is XmlAttribute)
					{
						return "@" + ((XmlAttribute)value).LocalName;
					}
					if (obj is XmlAnyElementAttribute && value is XmlElement)
					{
						return ((XmlElement)value).LocalName;
					}
					if (obj is XmlTextAttribute && value is XmlText)
					{
						return ((XmlText)value).Name;
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005698 File Offset: 0x00003898
		private static bool IsPersistedProperty(PropertyInfo property)
		{
			object[] customAttributes = property.GetCustomAttributes(true);
			if (customAttributes != null)
			{
				foreach (object o in customAttributes)
				{
					foreach (Type type in SchemaMerger.xmlSerializationAttributes)
					{
						if (type.IsInstanceOfType(o))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000056F4 File Offset: 0x000038F4
		private static bool ShouldIgnoreSchemaProperty(PropertyInfo property)
		{
			Type propertyType = property.PropertyType;
			foreach (Type type in SchemaMerger.ignorablePropertyTypes)
			{
				if (propertyType == type || propertyType.IsSubclassOf(type))
				{
					return true;
				}
			}
			return string.Equals(property.Name, "Constraints", StringComparison.Ordinal);
		}

		// Token: 0x04000066 RID: 102
		private static Type[] xmlSerializationAttributes = new Type[]
		{
			typeof(XmlElementAttribute),
			typeof(XmlAttributeAttribute),
			typeof(XmlAnyAttributeAttribute),
			typeof(XmlAnyElementAttribute),
			typeof(XmlTextAttribute)
		};

		// Token: 0x04000067 RID: 103
		private static SchemaMerger.SchemaTopLevelItemType[] schemaTopLevelItemTypes = new SchemaMerger.SchemaTopLevelItemType[]
		{
			new SchemaMerger.SchemaTopLevelItemType(typeof(XmlSchemaType), "type"),
			new SchemaMerger.SchemaTopLevelItemType(typeof(XmlSchemaElement), "element"),
			new SchemaMerger.SchemaTopLevelItemType(typeof(XmlSchemaAttribute), "attribute"),
			new SchemaMerger.SchemaTopLevelItemType(typeof(XmlSchemaGroup), "group"),
			new SchemaMerger.SchemaTopLevelItemType(typeof(XmlSchemaAttributeGroup), "attributeGroup")
		};

		// Token: 0x04000068 RID: 104
		private static Type[] ignorablePropertyTypes = new Type[]
		{
			typeof(XmlAttribute[]),
			typeof(XmlElement[]),
			typeof(XmlNode[]),
			typeof(XmlSchemaAnnotation)
		};

		// Token: 0x04000069 RID: 105
		private static readonly XmlAttribute[] emptyXmlAttributeCollection = new XmlAttribute[0];

		// Token: 0x0400006A RID: 106
		private static readonly object[] emptyCollection = new object[0];

		// Token: 0x02000130 RID: 304
		private struct SchemaTopLevelItemType
		{
			// Token: 0x06000F5A RID: 3930 RVA: 0x00037142 File Offset: 0x00035342
			public SchemaTopLevelItemType(Type itemType, string name)
			{
				this.ItemType = itemType;
				this.Name = name;
			}

			// Token: 0x04000475 RID: 1141
			public Type ItemType;

			// Token: 0x04000476 RID: 1142
			public string Name;
		}

		// Token: 0x02000131 RID: 305
		private class AttributeComparer : IComparer<XmlAttribute>
		{
			// Token: 0x06000F5B RID: 3931 RVA: 0x00037154 File Offset: 0x00035354
			public int Compare(XmlAttribute x, XmlAttribute y)
			{
				int num = string.Compare(x.NamespaceURI, y.NamespaceURI, StringComparison.Ordinal);
				if (num != 0)
				{
					return num;
				}
				return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
			}
		}
	}
}
