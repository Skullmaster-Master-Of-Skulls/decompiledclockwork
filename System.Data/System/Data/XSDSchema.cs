using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;

namespace System.Data
{
	// Token: 0x020000FB RID: 251
	internal sealed class XSDSchema : XMLSchema
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x002221D8 File Offset: 0x002215D8
		// (set) Token: 0x06000E95 RID: 3733 RVA: 0x002221F8 File Offset: 0x002215F8
		internal bool FromInference
		{
			get
			{
				return this.fromInference;
			}
			set
			{
				this.fromInference = value;
			}
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00222218 File Offset: 0x00221618
		private void CollectElementsAnnotations(XmlSchema schema)
		{
			ArrayList arrayList = new ArrayList();
			this.CollectElementsAnnotations(schema, arrayList);
			arrayList.Clear();
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00222248 File Offset: 0x00221648
		private void CollectElementsAnnotations(XmlSchema schema, ArrayList schemaList)
		{
			if (schemaList.Contains(schema))
			{
				return;
			}
			schemaList.Add(schema);
			foreach (object obj in schema.Items)
			{
				if (obj is XmlSchemaAnnotation)
				{
					this.annotations.Add((XmlSchemaAnnotation)obj);
				}
				if (obj is XmlSchemaElement)
				{
					XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
					this.elements.Add(xmlSchemaElement);
					this.elementsTable[xmlSchemaElement.QualifiedName] = xmlSchemaElement;
				}
				if (obj is XmlSchemaAttribute)
				{
					XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj;
					this.attributes[xmlSchemaAttribute.QualifiedName] = xmlSchemaAttribute;
				}
				if (obj is XmlSchemaAttributeGroup)
				{
					XmlSchemaAttributeGroup xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)obj;
					this.attributeGroups[xmlSchemaAttributeGroup.QualifiedName] = xmlSchemaAttributeGroup;
				}
				if (obj is XmlSchemaType)
				{
					if (obj is XmlSchemaSimpleType)
					{
						XSDSchema.GetMsdataAttribute((XmlSchemaType)obj, "targetNamespace");
					}
					XmlSchemaType xmlSchemaType = (XmlSchemaType)obj;
					this.schemaTypes[xmlSchemaType.QualifiedName] = xmlSchemaType;
					XmlSchemaSimpleType xmlSchemaSimpleType = obj as XmlSchemaSimpleType;
					if (xmlSchemaSimpleType != null)
					{
						if (this.udSimpleTypes == null)
						{
							this.udSimpleTypes = new Hashtable();
						}
						this.udSimpleTypes[xmlSchemaType.QualifiedName.ToString()] = xmlSchemaSimpleType;
						DataColumn dataColumn = (DataColumn)this.existingSimpleTypeMap[xmlSchemaType.QualifiedName.ToString()];
						SimpleType simpleType = (dataColumn != null) ? dataColumn.SimpleType : null;
						if (simpleType != null)
						{
							SimpleType simpleType2 = new SimpleType(xmlSchemaSimpleType);
							string text = simpleType.HasConflictingDefinition(simpleType2);
							if (text.Length != 0)
							{
								throw ExceptionBuilder.InvalidDuplicateNamedSimpleTypeDelaration(simpleType2.SimpleTypeQualifiedName, text);
							}
						}
					}
				}
			}
			foreach (XmlSchemaObject xmlSchemaObject in schema.Includes)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)xmlSchemaObject;
				if (!(xmlSchemaExternal is XmlSchemaImport) && xmlSchemaExternal.Schema != null)
				{
					this.CollectElementsAnnotations(xmlSchemaExternal.Schema, schemaList);
				}
			}
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00222498 File Offset: 0x00221898
		internal static string QualifiedName(string name)
		{
			int num = name.IndexOf(':');
			if (num == -1)
			{
				return "xs:" + name;
			}
			return name;
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x002224C8 File Offset: 0x002218C8
		internal static void SetProperties(object instance, XmlAttribute[] attrs)
		{
			if (attrs == null)
			{
				return;
			}
			for (int i = 0; i < attrs.Length; i++)
			{
				if (attrs[i].NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
				{
					string localName = attrs[i].LocalName;
					string value = attrs[i].Value;
					if (!(localName == "DefaultValue") && !(localName == "Ordinal") && !(localName == "Locale") && !(localName == "RemotingFormat") && (!(localName == "Expression") || !(instance is DataColumn)))
					{
						if (localName == "DataType")
						{
							DataColumn dataColumn = instance as DataColumn;
							if (dataColumn != null)
							{
								Type type = Type.GetType(value);
								dataColumn.DataType = type;
							}
						}
						else
						{
							PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(instance)[localName];
							if (propertyDescriptor != null)
							{
								Type propertyType = propertyDescriptor.PropertyType;
								TypeConverter converter = XMLSchema.GetConverter(propertyType);
								object value2;
								if (converter.CanConvertFrom(typeof(string)))
								{
									value2 = converter.ConvertFromString(value);
								}
								else if (propertyType == typeof(Type))
								{
									value2 = Type.GetType(value);
								}
								else
								{
									if (propertyType != typeof(CultureInfo))
									{
										throw ExceptionBuilder.CannotConvert(value, propertyType.FullName);
									}
									value2 = new CultureInfo(value);
								}
								propertyDescriptor.SetValue(instance, value2);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00222638 File Offset: 0x00221A38
		private static void SetExtProperties(object instance, XmlAttribute[] attrs)
		{
			PropertyCollection propertyCollection = null;
			if (attrs == null)
			{
				return;
			}
			for (int i = 0; i < attrs.Length; i++)
			{
				if (attrs[i].NamespaceURI == "urn:schemas-microsoft-com:xml-msprop")
				{
					if (propertyCollection == null)
					{
						object value = TypeDescriptor.GetProperties(instance)["ExtendedProperties"].GetValue(instance);
						propertyCollection = (PropertyCollection)value;
					}
					string text = XmlConvert.DecodeName(attrs[i].LocalName);
					if (instance is ForeignKeyConstraint)
					{
						if (!text.StartsWith("fk_", StringComparison.Ordinal))
						{
							goto IL_B6;
						}
						text = text.Substring(3);
					}
					if (instance is DataRelation && text.StartsWith("rel_", StringComparison.Ordinal))
					{
						text = text.Substring(4);
					}
					else if (instance is DataRelation && text.StartsWith("fk_", StringComparison.Ordinal))
					{
						goto IL_B6;
					}
					propertyCollection.Add(text, attrs[i].Value);
				}
				IL_B6:;
			}
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x00222708 File Offset: 0x00221B08
		private void HandleColumnExpression(object instance, XmlAttribute[] attrs)
		{
			if (attrs == null)
			{
				return;
			}
			DataColumn dataColumn = instance as DataColumn;
			if (dataColumn != null)
			{
				for (int i = 0; i < attrs.Length; i++)
				{
					if (attrs[i].NamespaceURI == "urn:schemas-microsoft-com:xml-msdata" && attrs[i].LocalName == "Expression")
					{
						if (this.expressions == null)
						{
							this.expressions = new Hashtable();
						}
						this.expressions[dataColumn] = attrs[i].Value;
						this.ColumnExpressions.Add(dataColumn);
						return;
					}
				}
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x00222798 File Offset: 0x00221B98
		internal static string GetMsdataAttribute(XmlSchemaAnnotated node, string ln)
		{
			XmlAttribute[] unhandledAttributes = node.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				for (int i = 0; i < unhandledAttributes.Length; i++)
				{
					if (unhandledAttributes[i].LocalName == ln && unhandledAttributes[i].NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
					{
						return unhandledAttributes[i].Value;
					}
				}
			}
			return null;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x002227F8 File Offset: 0x00221BF8
		private static void SetExtProperties(object instance, XmlAttributeCollection attrs)
		{
			PropertyCollection propertyCollection = null;
			for (int i = 0; i < attrs.Count; i++)
			{
				if (attrs[i].NamespaceURI == "urn:schemas-microsoft-com:xml-msprop")
				{
					if (propertyCollection == null)
					{
						object value = TypeDescriptor.GetProperties(instance)["ExtendedProperties"].GetValue(instance);
						propertyCollection = (PropertyCollection)value;
					}
					string key = XmlConvert.DecodeName(attrs[i].LocalName);
					propertyCollection.Add(key, attrs[i].Value);
				}
			}
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00222878 File Offset: 0x00221C78
		internal void HandleRefTableProperties(ArrayList RefTables, XmlSchemaElement element)
		{
			string instanceName = this.GetInstanceName(element);
			DataTable table = this._ds.Tables.GetTable(XmlConvert.DecodeName(instanceName), element.QualifiedName.Namespace);
			XSDSchema.SetProperties(table, element.UnhandledAttributes);
			XSDSchema.SetExtProperties(table, element.UnhandledAttributes);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x002228C8 File Offset: 0x00221CC8
		internal void HandleRelation(XmlElement node, bool fNested)
		{
			bool createConstraints = false;
			DataRelationCollection relations = this._ds.Relations;
			string text = XmlConvert.DecodeName(node.GetAttribute("name"));
			for (int i = 0; i < relations.Count; i++)
			{
				if (string.Compare(relations[i].RelationName, text, StringComparison.Ordinal) == 0)
				{
					return;
				}
			}
			string text2 = node.GetAttribute("parent", "urn:schemas-microsoft-com:xml-msdata");
			if (text2 == null || text2.Length == 0)
			{
				throw ExceptionBuilder.RelationParentNameMissing(text);
			}
			text2 = XmlConvert.DecodeName(text2);
			string text3 = node.GetAttribute("child", "urn:schemas-microsoft-com:xml-msdata");
			if (text3 == null || text3.Length == 0)
			{
				throw ExceptionBuilder.RelationChildNameMissing(text);
			}
			text3 = XmlConvert.DecodeName(text3);
			string attribute = node.GetAttribute("parentkey", "urn:schemas-microsoft-com:xml-msdata");
			if (attribute == null || attribute.Length == 0)
			{
				throw ExceptionBuilder.RelationTableKeyMissing(text);
			}
			string[] array = attribute.TrimEnd(null).Split(new char[]
			{
				' ',
				'+'
			});
			attribute = node.GetAttribute("childkey", "urn:schemas-microsoft-com:xml-msdata");
			if (attribute == null || attribute.Length == 0)
			{
				throw ExceptionBuilder.RelationChildKeyMissing(text);
			}
			string[] array2 = attribute.TrimEnd(null).Split(new char[]
			{
				' ',
				'+'
			});
			int num = array.Length;
			if (num != array2.Length)
			{
				throw ExceptionBuilder.MismatchKeyLength();
			}
			DataColumn[] array3 = new DataColumn[num];
			DataColumn[] array4 = new DataColumn[num];
			string attribute2 = node.GetAttribute("ParentTableNamespace", "urn:schemas-microsoft-com:xml-msdata");
			string attribute3 = node.GetAttribute("ChildTableNamespace", "urn:schemas-microsoft-com:xml-msdata");
			DataTable tableSmart = this._ds.Tables.GetTableSmart(text2, attribute2);
			if (tableSmart == null)
			{
				throw ExceptionBuilder.ElementTypeNotFound(text2);
			}
			DataTable tableSmart2 = this._ds.Tables.GetTableSmart(text3, attribute3);
			if (tableSmart2 == null)
			{
				throw ExceptionBuilder.ElementTypeNotFound(text3);
			}
			for (int j = 0; j < num; j++)
			{
				array3[j] = tableSmart.Columns[XmlConvert.DecodeName(array[j])];
				if (array3[j] == null)
				{
					throw ExceptionBuilder.ElementTypeNotFound(array[j]);
				}
				array4[j] = tableSmart2.Columns[XmlConvert.DecodeName(array2[j])];
				if (array4[j] == null)
				{
					throw ExceptionBuilder.ElementTypeNotFound(array2[j]);
				}
			}
			DataRelation dataRelation = new DataRelation(text, array3, array4, createConstraints);
			dataRelation.Nested = fNested;
			XSDSchema.SetExtProperties(dataRelation, node.Attributes);
			this._ds.Relations.Add(dataRelation);
			if (this.FromInference && dataRelation.Nested)
			{
				this.tableDictionary[dataRelation.ParentTable].Add(dataRelation.ChildTable);
			}
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00222B68 File Offset: 0x00221F68
		private bool HasAttributes(XmlSchemaObjectCollection attributes)
		{
			foreach (XmlSchemaObject xmlSchemaObject in attributes)
			{
				if (xmlSchemaObject is XmlSchemaAttribute)
				{
					return true;
				}
				if (xmlSchemaObject is XmlSchemaAttributeGroup)
				{
					return true;
				}
				if (xmlSchemaObject is XmlSchemaAttributeGroupRef)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00222BE8 File Offset: 0x00221FE8
		private bool IsDatasetParticle(XmlSchemaParticle pt)
		{
			XmlSchemaObjectCollection particleItems = this.GetParticleItems(pt);
			if (particleItems == null)
			{
				return false;
			}
			bool flag = this.FromInference && pt is XmlSchemaChoice;
			foreach (XmlSchemaObject xmlSchemaObject in particleItems)
			{
				XmlSchemaAnnotated xmlSchemaAnnotated = (XmlSchemaAnnotated)xmlSchemaObject;
				if (xmlSchemaAnnotated is XmlSchemaElement)
				{
					if (flag && pt.MaxOccurs > 1m && ((XmlSchemaElement)xmlSchemaAnnotated).SchemaType is XmlSchemaComplexType)
					{
						((XmlSchemaElement)xmlSchemaAnnotated).MaxOccurs = pt.MaxOccurs;
					}
					if ((((XmlSchemaElement)xmlSchemaAnnotated).RefName.Name.Length == 0 || (this.FromInference && (!(((XmlSchemaElement)xmlSchemaAnnotated).MaxOccurs != 1m) || ((XmlSchemaElement)xmlSchemaAnnotated).SchemaType is XmlSchemaComplexType))) && !this.IsTable((XmlSchemaElement)xmlSchemaAnnotated))
					{
						return false;
					}
				}
				else if (xmlSchemaAnnotated is XmlSchemaParticle && !this.IsDatasetParticle((XmlSchemaParticle)xmlSchemaAnnotated))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00222D38 File Offset: 0x00222138
		private int DatasetElementCount(XmlSchemaObjectCollection elements)
		{
			int num = 0;
			foreach (XmlSchemaObject xmlSchemaObject in elements)
			{
				XmlSchemaElement element = (XmlSchemaElement)xmlSchemaObject;
				if (this.GetBooleanAttribute(element, "IsDataSet", false))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00222DA8 File Offset: 0x002221A8
		private XmlSchemaElement FindDatasetElement(XmlSchemaObjectCollection elements)
		{
			foreach (XmlSchemaObject xmlSchemaObject in elements)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)xmlSchemaObject;
				if (this.GetBooleanAttribute(xmlSchemaElement, "IsDataSet", false))
				{
					return xmlSchemaElement;
				}
			}
			if (elements.Count != 1 && (!this.FromInference || elements.Count <= 0))
			{
				return null;
			}
			XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)elements[0];
			if (!this.GetBooleanAttribute(xmlSchemaElement2, "IsDataSet", true))
			{
				return null;
			}
			XmlSchemaComplexType xmlSchemaComplexType = xmlSchemaElement2.SchemaType as XmlSchemaComplexType;
			if (xmlSchemaComplexType == null)
			{
				return null;
			}
			while (xmlSchemaComplexType != null)
			{
				if (this.HasAttributes(xmlSchemaComplexType.Attributes))
				{
					return null;
				}
				if (xmlSchemaComplexType.ContentModel is XmlSchemaSimpleContent)
				{
					XmlSchemaAnnotated content = ((XmlSchemaSimpleContent)xmlSchemaComplexType.ContentModel).Content;
					if (content is XmlSchemaSimpleContentExtension)
					{
						XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = (XmlSchemaSimpleContentExtension)content;
						if (this.HasAttributes(xmlSchemaSimpleContentExtension.Attributes))
						{
							return null;
						}
					}
					else
					{
						XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = (XmlSchemaSimpleContentRestriction)content;
						if (this.HasAttributes(xmlSchemaSimpleContentRestriction.Attributes))
						{
							return null;
						}
					}
				}
				XmlSchemaParticle particle = this.GetParticle(xmlSchemaComplexType);
				if (particle != null && !this.IsDatasetParticle(particle))
				{
					return null;
				}
				if (!(xmlSchemaComplexType.BaseXmlSchemaType is XmlSchemaComplexType))
				{
					break;
				}
				xmlSchemaComplexType = (XmlSchemaComplexType)xmlSchemaComplexType.BaseXmlSchemaType;
			}
			return xmlSchemaElement2;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00222F18 File Offset: 0x00222318
		public void LoadSchema(XmlSchemaSet schemaSet, DataTable dt)
		{
			if (dt.DataSet != null)
			{
				this.LoadSchema(schemaSet, dt.DataSet);
			}
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00222F48 File Offset: 0x00222348
		public void LoadSchema(XmlSchemaSet schemaSet, DataSet ds)
		{
			this.ConstraintNodes = new Hashtable();
			this.RefTables = new ArrayList();
			this.ColumnExpressions = new ArrayList();
			this.complexTypes = new ArrayList();
			bool flag = false;
			bool isNewDataSet = ds.Tables.Count == 0;
			if (schemaSet == null)
			{
				return;
			}
			this._schemaSet = schemaSet;
			this._ds = ds;
			ds.fIsSchemaLoading = true;
			using (IEnumerator enumerator = schemaSet.Schemas().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					XmlSchema xmlSchema = (XmlSchema)enumerator.Current;
					this._schemaName = xmlSchema.Id;
					if (this._schemaName == null || this._schemaName.Length == 0)
					{
						this._schemaName = "NewDataSet";
					}
					ds.DataSetName = XmlConvert.DecodeName(this._schemaName);
					string targetNamespace = xmlSchema.TargetNamespace;
					if (ds.namespaceURI == null || ds.namespaceURI.Length == 0)
					{
						ds.namespaceURI = ((targetNamespace == null) ? string.Empty : targetNamespace);
					}
				}
			}
			this.annotations = new XmlSchemaObjectCollection();
			this.elements = new XmlSchemaObjectCollection();
			this.elementsTable = new Hashtable();
			this.attributes = new Hashtable();
			this.attributeGroups = new Hashtable();
			this.schemaTypes = new Hashtable();
			this.tableDictionary = new Dictionary<DataTable, List<DataTable>>();
			this.existingSimpleTypeMap = new Hashtable();
			foreach (object obj in ds.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				foreach (object obj2 in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj2;
					if (dataColumn.SimpleType != null && dataColumn.SimpleType.Name != null && dataColumn.SimpleType.Name.Length != 0)
					{
						this.existingSimpleTypeMap[dataColumn.SimpleType.SimpleTypeQualifiedName] = dataColumn;
					}
				}
			}
			foreach (object obj3 in schemaSet.Schemas())
			{
				XmlSchema schema = (XmlSchema)obj3;
				this.CollectElementsAnnotations(schema);
			}
			this.dsElement = this.FindDatasetElement(this.elements);
			if (this.dsElement != null)
			{
				string stringAttribute = this.GetStringAttribute(this.dsElement, "MainDataTable", "");
				if (stringAttribute != null)
				{
					ds.MainTableName = XmlConvert.DecodeName(stringAttribute);
				}
			}
			else
			{
				if (this.FromInference)
				{
					ds.fTopLevelTable = true;
				}
				flag = true;
			}
			List<XmlQualifiedName> list = new List<XmlQualifiedName>();
			if (ds != null && ds.UseDataSetSchemaOnly)
			{
				int num = this.DatasetElementCount(this.elements);
				if (num == 0)
				{
					throw ExceptionBuilder.IsDataSetAttributeMissingInSchema();
				}
				if (num > 1)
				{
					throw ExceptionBuilder.TooManyIsDataSetAtributeInSchema();
				}
				XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)this.FindTypeNode(this.dsElement);
				if (xmlSchemaComplexType.Particle != null)
				{
					XmlSchemaObjectCollection particleItems = this.GetParticleItems(xmlSchemaComplexType.Particle);
					if (particleItems != null)
					{
						foreach (XmlSchemaObject xmlSchemaObject in particleItems)
						{
							XmlSchemaAnnotated xmlSchemaAnnotated = (XmlSchemaAnnotated)xmlSchemaObject;
							XmlSchemaElement xmlSchemaElement = xmlSchemaAnnotated as XmlSchemaElement;
							if (xmlSchemaElement != null && xmlSchemaElement.RefName.Name.Length != 0)
							{
								list.Add(xmlSchemaElement.QualifiedName);
							}
						}
					}
				}
			}
			foreach (XmlSchemaObject xmlSchemaObject2 in this.elements)
			{
				XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)xmlSchemaObject2;
				if (xmlSchemaElement2 != this.dsElement && (ds == null || !ds.UseDataSetSchemaOnly || this.dsElement == null || this.dsElement.Parent == xmlSchemaElement2.Parent || list.Contains(xmlSchemaElement2.QualifiedName)))
				{
					string instanceName = this.GetInstanceName(xmlSchemaElement2);
					if (this.RefTables.Contains(xmlSchemaElement2.QualifiedName.Namespace + ":" + instanceName))
					{
						this.HandleRefTableProperties(this.RefTables, xmlSchemaElement2);
					}
					else
					{
						this.HandleTable(xmlSchemaElement2);
					}
				}
			}
			if (this.dsElement != null)
			{
				this.HandleDataSet(this.dsElement, isNewDataSet);
			}
			foreach (XmlSchemaObject xmlSchemaObject3 in this.annotations)
			{
				XmlSchemaAnnotation ann = (XmlSchemaAnnotation)xmlSchemaObject3;
				this.HandleRelations(ann, false);
			}
			for (int i = 0; i < this.ColumnExpressions.Count; i++)
			{
				DataColumn dataColumn2 = (DataColumn)this.ColumnExpressions[i];
				dataColumn2.Expression = (string)this.expressions[dataColumn2];
			}
			foreach (object obj4 in ds.Tables)
			{
				DataTable dataTable2 = (DataTable)obj4;
				if (dataTable2.NestedParentRelations.Length == 0 && dataTable2.Namespace == ds.Namespace)
				{
					DataRelationCollection childRelations = dataTable2.ChildRelations;
					for (int j = 0; j < childRelations.Count; j++)
					{
						if (childRelations[j].Nested && dataTable2.Namespace == childRelations[j].ChildTable.Namespace)
						{
							childRelations[j].ChildTable.tableNamespace = null;
						}
					}
					dataTable2.tableNamespace = null;
				}
			}
			DataTable dataTable3 = ds.Tables[ds.DataSetName, ds.Namespace];
			if (dataTable3 != null)
			{
				dataTable3.fNestedInDataset = true;
			}
			if (this.FromInference && ds.Tables.Count == 0 && string.Compare(ds.DataSetName, "NewDataSet", StringComparison.Ordinal) == 0)
			{
				ds.DataSetName = XmlConvert.DecodeName(((XmlSchemaElement)this.elements[0]).Name);
			}
			ds.fIsSchemaLoading = false;
			if (flag)
			{
				if (ds.Tables.Count > 0)
				{
					ds.Namespace = ds.Tables[0].Namespace;
					ds.Prefix = ds.Tables[0].Prefix;
					return;
				}
				foreach (object obj5 in schemaSet.Schemas())
				{
					XmlSchema xmlSchema2 = (XmlSchema)obj5;
					ds.Namespace = xmlSchema2.TargetNamespace;
				}
			}
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x002236F8 File Offset: 0x00222AF8
		private void HandleRelations(XmlSchemaAnnotation ann, bool fNested)
		{
			foreach (object obj in ann.Items)
			{
				if (obj is XmlSchemaAppInfo)
				{
					XmlNode[] markup = ((XmlSchemaAppInfo)obj).Markup;
					for (int i = 0; i < markup.Length; i++)
					{
						if (XMLSchema.FEqualIdentity(markup[i], "Relationship", "urn:schemas-microsoft-com:xml-msdata"))
						{
							this.HandleRelation((XmlElement)markup[i], fNested);
						}
					}
				}
			}
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x002237A8 File Offset: 0x00222BA8
		internal XmlSchemaObjectCollection GetParticleItems(XmlSchemaParticle pt)
		{
			if (pt is XmlSchemaSequence)
			{
				return ((XmlSchemaSequence)pt).Items;
			}
			if (pt is XmlSchemaAll)
			{
				return ((XmlSchemaAll)pt).Items;
			}
			if (pt is XmlSchemaChoice)
			{
				return ((XmlSchemaChoice)pt).Items;
			}
			if (pt is XmlSchemaAny)
			{
				return null;
			}
			if (pt is XmlSchemaElement)
			{
				return new XmlSchemaObjectCollection
				{
					pt
				};
			}
			if (pt is XmlSchemaGroupRef)
			{
				return this.GetParticleItems(((XmlSchemaGroupRef)pt).Particle);
			}
			return null;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00223838 File Offset: 0x00222C38
		internal void HandleParticle(XmlSchemaParticle pt, DataTable table, ArrayList tableChildren, bool isBase)
		{
			XmlSchemaObjectCollection particleItems = this.GetParticleItems(pt);
			if (particleItems == null)
			{
				return;
			}
			foreach (XmlSchemaObject xmlSchemaObject in particleItems)
			{
				XmlSchemaAnnotated xmlSchemaAnnotated = (XmlSchemaAnnotated)xmlSchemaObject;
				XmlSchemaElement xmlSchemaElement = xmlSchemaAnnotated as XmlSchemaElement;
				if (xmlSchemaElement != null)
				{
					if (this.FromInference && pt is XmlSchemaChoice && pt.MaxOccurs > 1m && xmlSchemaElement.SchemaType is XmlSchemaComplexType)
					{
						xmlSchemaElement.MaxOccurs = pt.MaxOccurs;
					}
					DataTable dataTable;
					if ((xmlSchemaElement.Name == null && xmlSchemaElement.RefName.Name == table.EncodedTableName && xmlSchemaElement.RefName.Namespace == table.Namespace) || (this.IsTable(xmlSchemaElement) && xmlSchemaElement.Name == table.TableName))
					{
						if (this.FromInference)
						{
							dataTable = this.HandleTable(xmlSchemaElement);
						}
						else
						{
							dataTable = table;
						}
					}
					else
					{
						dataTable = this.HandleTable(xmlSchemaElement);
						if (dataTable == null && this.FromInference && xmlSchemaElement.Name == table.TableName)
						{
							dataTable = table;
						}
					}
					if (dataTable == null)
					{
						if (!this.FromInference || xmlSchemaElement.Name != table.TableName)
						{
							this.HandleElementColumn(xmlSchemaElement, table, isBase);
						}
					}
					else
					{
						DataRelation dataRelation = null;
						if (xmlSchemaElement.Annotation != null)
						{
							this.HandleRelations(xmlSchemaElement.Annotation, true);
						}
						DataRelationCollection childRelations = table.ChildRelations;
						for (int i = 0; i < childRelations.Count; i++)
						{
							if (childRelations[i].Nested && dataTable == childRelations[i].ChildTable)
							{
								dataRelation = childRelations[i];
							}
						}
						if (dataRelation == null)
						{
							tableChildren.Add(dataTable);
							if (this.FromInference && table.UKColumnPositionForInference == -1)
							{
								int num = -1;
								foreach (object obj in table.Columns)
								{
									DataColumn dataColumn = (DataColumn)obj;
									if (dataColumn.ColumnMapping == MappingType.Element)
									{
										num++;
									}
								}
								table.UKColumnPositionForInference = num + 1;
							}
						}
					}
				}
				else
				{
					this.HandleParticle((XmlSchemaParticle)xmlSchemaAnnotated, table, tableChildren, isBase);
				}
			}
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00223AC8 File Offset: 0x00222EC8
		internal void HandleAttributes(XmlSchemaObjectCollection attributes, DataTable table, bool isBase)
		{
			foreach (XmlSchemaObject xmlSchemaObject in attributes)
			{
				if (xmlSchemaObject is XmlSchemaAttribute)
				{
					this.HandleAttributeColumn((XmlSchemaAttribute)xmlSchemaObject, table, isBase);
				}
				else
				{
					XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = xmlSchemaObject as XmlSchemaAttributeGroupRef;
					XmlSchemaAttributeGroup xmlSchemaAttributeGroup = this.attributeGroups[xmlSchemaAttributeGroupRef.RefName] as XmlSchemaAttributeGroup;
					if (xmlSchemaAttributeGroup != null)
					{
						this.HandleAttributeGroup(xmlSchemaAttributeGroup, table, isBase);
					}
				}
			}
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00223B68 File Offset: 0x00222F68
		private void HandleAttributeGroup(XmlSchemaAttributeGroup attributeGroup, DataTable table, bool isBase)
		{
			foreach (XmlSchemaObject xmlSchemaObject in attributeGroup.Attributes)
			{
				if (xmlSchemaObject is XmlSchemaAttribute)
				{
					this.HandleAttributeColumn((XmlSchemaAttribute)xmlSchemaObject, table, isBase);
				}
				else
				{
					XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = (XmlSchemaAttributeGroupRef)xmlSchemaObject;
					XmlSchemaAttributeGroup xmlSchemaAttributeGroup;
					if (attributeGroup.RedefinedAttributeGroup != null && xmlSchemaAttributeGroupRef.RefName == new XmlQualifiedName(attributeGroup.Name, xmlSchemaAttributeGroupRef.RefName.Namespace))
					{
						xmlSchemaAttributeGroup = attributeGroup.RedefinedAttributeGroup;
					}
					else
					{
						xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)this.attributeGroups[xmlSchemaAttributeGroupRef.RefName];
					}
					if (xmlSchemaAttributeGroup != null)
					{
						this.HandleAttributeGroup(xmlSchemaAttributeGroup, table, isBase);
					}
				}
			}
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00223C48 File Offset: 0x00223048
		internal void HandleComplexType(XmlSchemaComplexType ct, DataTable table, ArrayList tableChildren, bool isNillable)
		{
			if (this.complexTypes.Contains(ct))
			{
				throw ExceptionBuilder.CircularComplexType(ct.Name);
			}
			bool isBase = false;
			this.complexTypes.Add(ct);
			if (ct.ContentModel != null)
			{
				if (ct.ContentModel is XmlSchemaComplexContent)
				{
					XmlSchemaAnnotated content = ((XmlSchemaComplexContent)ct.ContentModel).Content;
					if (content is XmlSchemaComplexContentExtension)
					{
						XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = (XmlSchemaComplexContentExtension)content;
						if (!(ct.BaseXmlSchemaType is XmlSchemaComplexType) || !this.FromInference)
						{
							this.HandleAttributes(xmlSchemaComplexContentExtension.Attributes, table, isBase);
						}
						if (ct.BaseXmlSchemaType is XmlSchemaComplexType)
						{
							this.HandleComplexType((XmlSchemaComplexType)ct.BaseXmlSchemaType, table, tableChildren, isNillable);
						}
						else if (xmlSchemaComplexContentExtension.BaseTypeName.Namespace != "http://www.w3.org/2001/XMLSchema")
						{
							this.HandleSimpleContentColumn(xmlSchemaComplexContentExtension.BaseTypeName.ToString(), table, isBase, ct.ContentModel.UnhandledAttributes, isNillable);
						}
						else
						{
							this.HandleSimpleContentColumn(xmlSchemaComplexContentExtension.BaseTypeName.Name, table, isBase, ct.ContentModel.UnhandledAttributes, isNillable);
						}
						if (xmlSchemaComplexContentExtension.Particle != null)
						{
							this.HandleParticle(xmlSchemaComplexContentExtension.Particle, table, tableChildren, isBase);
						}
						if (ct.BaseXmlSchemaType is XmlSchemaComplexType && this.FromInference)
						{
							this.HandleAttributes(xmlSchemaComplexContentExtension.Attributes, table, isBase);
						}
					}
					else
					{
						XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = (XmlSchemaComplexContentRestriction)content;
						if (!this.FromInference)
						{
							this.HandleAttributes(xmlSchemaComplexContentRestriction.Attributes, table, isBase);
						}
						if (xmlSchemaComplexContentRestriction.Particle != null)
						{
							this.HandleParticle(xmlSchemaComplexContentRestriction.Particle, table, tableChildren, isBase);
						}
						if (this.FromInference)
						{
							this.HandleAttributes(xmlSchemaComplexContentRestriction.Attributes, table, isBase);
						}
					}
				}
				else
				{
					XmlSchemaAnnotated content2 = ((XmlSchemaSimpleContent)ct.ContentModel).Content;
					if (content2 is XmlSchemaSimpleContentExtension)
					{
						XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = (XmlSchemaSimpleContentExtension)content2;
						this.HandleAttributes(xmlSchemaSimpleContentExtension.Attributes, table, isBase);
						if (ct.BaseXmlSchemaType is XmlSchemaComplexType)
						{
							this.HandleComplexType((XmlSchemaComplexType)ct.BaseXmlSchemaType, table, tableChildren, isNillable);
						}
						else
						{
							this.HandleSimpleTypeSimpleContentColumn((XmlSchemaSimpleType)ct.BaseXmlSchemaType, xmlSchemaSimpleContentExtension.BaseTypeName.Name, table, isBase, ct.ContentModel.UnhandledAttributes, isNillable);
						}
					}
					else
					{
						XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = (XmlSchemaSimpleContentRestriction)content2;
						this.HandleAttributes(xmlSchemaSimpleContentRestriction.Attributes, table, isBase);
					}
				}
			}
			else
			{
				isBase = true;
				if (!this.FromInference)
				{
					this.HandleAttributes(ct.Attributes, table, isBase);
				}
				if (ct.Particle != null)
				{
					this.HandleParticle(ct.Particle, table, tableChildren, isBase);
				}
				if (this.FromInference)
				{
					this.HandleAttributes(ct.Attributes, table, isBase);
					if (isNillable)
					{
						this.HandleSimpleContentColumn("string", table, isBase, null, isNillable);
					}
				}
			}
			this.complexTypes.Remove(ct);
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00223F08 File Offset: 0x00223308
		internal XmlSchemaParticle GetParticle(XmlSchemaComplexType ct)
		{
			if (ct.ContentModel == null)
			{
				return ct.Particle;
			}
			if (!(ct.ContentModel is XmlSchemaComplexContent))
			{
				return null;
			}
			XmlSchemaAnnotated content = ((XmlSchemaComplexContent)ct.ContentModel).Content;
			if (content is XmlSchemaComplexContentExtension)
			{
				return ((XmlSchemaComplexContentExtension)content).Particle;
			}
			return ((XmlSchemaComplexContentRestriction)content).Particle;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00223F68 File Offset: 0x00223368
		internal DataColumn FindField(DataTable table, string field)
		{
			bool flag = false;
			string text = field;
			if (field.StartsWith("@", StringComparison.Ordinal))
			{
				flag = true;
				text = field.Substring(1);
			}
			string[] array = text.Split(new char[]
			{
				':'
			});
			text = array[array.Length - 1];
			text = XmlConvert.DecodeName(text);
			DataColumn dataColumn = table.Columns[text];
			if (dataColumn == null)
			{
				throw ExceptionBuilder.InvalidField(field);
			}
			bool flag2 = dataColumn.ColumnMapping == MappingType.Attribute || dataColumn.ColumnMapping == MappingType.Hidden;
			if (flag2 != flag)
			{
				throw ExceptionBuilder.InvalidField(field);
			}
			return dataColumn;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00223FF8 File Offset: 0x002233F8
		internal DataColumn[] BuildKey(XmlSchemaIdentityConstraint keyNode, DataTable table)
		{
			ArrayList arrayList = new ArrayList();
			foreach (XmlSchemaObject xmlSchemaObject in keyNode.Fields)
			{
				XmlSchemaXPath xmlSchemaXPath = (XmlSchemaXPath)xmlSchemaObject;
				arrayList.Add(this.FindField(table, xmlSchemaXPath.XPath));
			}
			DataColumn[] array = new DataColumn[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00224088 File Offset: 0x00223488
		internal bool GetBooleanAttribute(XmlSchemaAnnotated element, string attrName, bool defVal)
		{
			string msdataAttribute = XSDSchema.GetMsdataAttribute(element, attrName);
			if (msdataAttribute == null || msdataAttribute.Length == 0)
			{
				return defVal;
			}
			if (msdataAttribute == "true" || msdataAttribute == "1")
			{
				return true;
			}
			if (msdataAttribute == "false" || msdataAttribute == "0")
			{
				return false;
			}
			throw ExceptionBuilder.InvalidAttributeValue(attrName, msdataAttribute);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x002240F8 File Offset: 0x002234F8
		internal string GetStringAttribute(XmlSchemaAnnotated element, string attrName, string defVal)
		{
			string msdataAttribute = XSDSchema.GetMsdataAttribute(element, attrName);
			if (msdataAttribute == null || msdataAttribute.Length == 0)
			{
				return defVal;
			}
			return msdataAttribute;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00224128 File Offset: 0x00223528
		internal int GetIntegerAttribute(XmlSchemaAnnotated element, string attrName, int defVal)
		{
			string msdataAttribute = XSDSchema.GetMsdataAttribute(element, attrName);
			if (msdataAttribute == null || msdataAttribute.Length == 0)
			{
				return defVal;
			}
			return Convert.ToInt32(msdataAttribute);
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00224158 File Offset: 0x00223558
		internal static AcceptRejectRule TranslateAcceptRejectRule(string strRule)
		{
			if (strRule == "Cascade")
			{
				return AcceptRejectRule.Cascade;
			}
			if (strRule == "None")
			{
				return AcceptRejectRule.None;
			}
			return AcceptRejectRule.None;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00224188 File Offset: 0x00223588
		internal static Rule TranslateRule(string strRule)
		{
			if (strRule == "Cascade")
			{
				return Rule.Cascade;
			}
			if (strRule == "None")
			{
				return Rule.None;
			}
			if (strRule == "SetDefault")
			{
				return Rule.SetDefault;
			}
			if (strRule == "SetNull")
			{
				return Rule.SetNull;
			}
			return Rule.Cascade;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x002241D8 File Offset: 0x002235D8
		internal void HandleKeyref(XmlSchemaKeyref keyref)
		{
			string text = XmlConvert.DecodeName(keyref.Refer.Name);
			string text2 = XmlConvert.DecodeName(keyref.Name);
			text2 = this.GetStringAttribute(keyref, "ConstraintName", text2);
			string tableName = this.GetTableName(keyref);
			string msdataAttribute = XSDSchema.GetMsdataAttribute(keyref, "TableNamespace");
			DataTable tableSmart = this._ds.Tables.GetTableSmart(tableName, msdataAttribute);
			if (tableSmart == null)
			{
				return;
			}
			if (text == null || text.Length == 0)
			{
				throw ExceptionBuilder.MissingRefer(text2);
			}
			ConstraintTable constraintTable = (ConstraintTable)this.ConstraintNodes[text];
			if (constraintTable == null)
			{
				throw ExceptionBuilder.InvalidKey(text2);
			}
			DataColumn[] array = this.BuildKey(constraintTable.constraint, constraintTable.table);
			DataColumn[] array2 = this.BuildKey(keyref, tableSmart);
			ForeignKeyConstraint foreignKeyConstraint = null;
			if (this.GetBooleanAttribute(keyref, "ConstraintOnly", false))
			{
				int num = array2[0].Table.Constraints.InternalIndexOf(text2);
				if (num > -1 && array2[0].Table.Constraints[num].ConstraintName != text2)
				{
					num = -1;
				}
				if (num < 0)
				{
					foreignKeyConstraint = new ForeignKeyConstraint(text2, array, array2);
					array2[0].Table.Constraints.Add(foreignKeyConstraint);
				}
			}
			else
			{
				string text3 = XmlConvert.DecodeName(this.GetStringAttribute(keyref, "RelationName", keyref.Name));
				if (text3 == null || text3.Length == 0)
				{
					text3 = text2;
				}
				int num2 = array2[0].Table.DataSet.Relations.InternalIndexOf(text3);
				if (num2 > -1 && array2[0].Table.DataSet.Relations[num2].RelationName != text3)
				{
					num2 = -1;
				}
				DataRelation dataRelation;
				if (num2 < 0)
				{
					dataRelation = new DataRelation(text3, array, array2);
					XSDSchema.SetExtProperties(dataRelation, keyref.UnhandledAttributes);
					array[0].Table.DataSet.Relations.Add(dataRelation);
					if (this.FromInference && dataRelation.Nested && this.tableDictionary.ContainsKey(dataRelation.ParentTable))
					{
						this.tableDictionary[dataRelation.ParentTable].Add(dataRelation.ChildTable);
					}
					foreignKeyConstraint = dataRelation.ChildKeyConstraint;
					foreignKeyConstraint.ConstraintName = text2;
				}
				else
				{
					dataRelation = array2[0].Table.DataSet.Relations[num2];
				}
				if (this.GetBooleanAttribute(keyref, "IsNested", false))
				{
					dataRelation.Nested = true;
				}
			}
			string msdataAttribute2 = XSDSchema.GetMsdataAttribute(keyref, "AcceptRejectRule");
			string msdataAttribute3 = XSDSchema.GetMsdataAttribute(keyref, "UpdateRule");
			string msdataAttribute4 = XSDSchema.GetMsdataAttribute(keyref, "DeleteRule");
			if (foreignKeyConstraint != null)
			{
				if (msdataAttribute2 != null)
				{
					foreignKeyConstraint.AcceptRejectRule = XSDSchema.TranslateAcceptRejectRule(msdataAttribute2);
				}
				if (msdataAttribute3 != null)
				{
					foreignKeyConstraint.UpdateRule = XSDSchema.TranslateRule(msdataAttribute3);
				}
				if (msdataAttribute4 != null)
				{
					foreignKeyConstraint.DeleteRule = XSDSchema.TranslateRule(msdataAttribute4);
				}
				XSDSchema.SetExtProperties(foreignKeyConstraint, keyref.UnhandledAttributes);
			}
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x002244A8 File Offset: 0x002238A8
		internal void HandleConstraint(XmlSchemaIdentityConstraint keyNode)
		{
			string text = XmlConvert.DecodeName(keyNode.Name);
			if (text == null || text.Length == 0)
			{
				throw ExceptionBuilder.MissingAttribute("name");
			}
			if (this.ConstraintNodes.ContainsKey(text))
			{
				throw ExceptionBuilder.DuplicateConstraintRead(text);
			}
			string tableName = this.GetTableName(keyNode);
			string msdataAttribute = XSDSchema.GetMsdataAttribute(keyNode, "TableNamespace");
			DataTable tableSmart = this._ds.Tables.GetTableSmart(tableName, msdataAttribute);
			if (tableSmart == null)
			{
				return;
			}
			this.ConstraintNodes.Add(text, new ConstraintTable(tableSmart, keyNode));
			bool booleanAttribute = this.GetBooleanAttribute(keyNode, "PrimaryKey", false);
			text = this.GetStringAttribute(keyNode, "ConstraintName", text);
			DataColumn[] array = this.BuildKey(keyNode, tableSmart);
			if (0 < array.Length)
			{
				UniqueConstraint uniqueConstraint = (UniqueConstraint)array[0].Table.Constraints.FindConstraint(new UniqueConstraint(text, array));
				if (uniqueConstraint == null)
				{
					array[0].Table.Constraints.Add(text, array, booleanAttribute);
					XSDSchema.SetExtProperties(array[0].Table.Constraints[text], keyNode.UnhandledAttributes);
				}
				else
				{
					array = uniqueConstraint.ColumnsReference;
					XSDSchema.SetExtProperties(uniqueConstraint, keyNode.UnhandledAttributes);
					if (booleanAttribute)
					{
						array[0].Table.PrimaryKey = array;
					}
				}
				if (keyNode is XmlSchemaKey)
				{
					for (int i = 0; i < array.Length; i++)
					{
						array[i].AllowDBNull = false;
					}
				}
			}
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00224608 File Offset: 0x00223A08
		internal DataTable InstantiateSimpleTable(XmlSchemaElement node)
		{
			string text = XmlConvert.DecodeName(this.GetInstanceName(node));
			string @namespace = node.QualifiedName.Namespace;
			DataTable dataTable = this._ds.Tables.GetTable(text, @namespace);
			if (!this.FromInference && dataTable != null)
			{
				throw ExceptionBuilder.DuplicateDeclaration(text);
			}
			if (dataTable == null)
			{
				dataTable = new DataTable(text);
				dataTable.Namespace = @namespace;
				dataTable.Namespace = this.GetStringAttribute(node, "targetNamespace", @namespace);
				if (!this.FromInference)
				{
					dataTable.MinOccurs = node.MinOccurs;
					dataTable.MaxOccurs = node.MaxOccurs;
				}
				else
				{
					string prefix = this.GetPrefix(@namespace);
					if (prefix != null)
					{
						dataTable.Prefix = prefix;
					}
				}
				XSDSchema.SetProperties(dataTable, node.UnhandledAttributes);
				XSDSchema.SetExtProperties(dataTable, node.UnhandledAttributes);
			}
			XmlSchemaComplexType xmlSchemaComplexType = node.SchemaType as XmlSchemaComplexType;
			bool flag = node.ElementSchemaType.BaseXmlSchemaType != null || (xmlSchemaComplexType != null && xmlSchemaComplexType.ContentModel is XmlSchemaSimpleContent);
			if (!this.FromInference || (flag && dataTable.Columns.Count == 0))
			{
				this.HandleElementColumn(node, dataTable, false);
				string text2;
				if (this.FromInference)
				{
					int num = 0;
					text2 = text + "_Text";
					while (dataTable.Columns[text2] != null)
					{
						text2 += num++;
					}
				}
				else
				{
					text2 = text + "_Column";
				}
				dataTable.Columns[0].ColumnName = text2;
				dataTable.Columns[0].ColumnMapping = MappingType.SimpleContent;
			}
			if (!this.FromInference || this._ds.Tables.GetTable(text, @namespace) == null)
			{
				this._ds.Tables.Add(dataTable);
				if (this.FromInference)
				{
					this.tableDictionary.Add(dataTable, new List<DataTable>());
				}
			}
			if (this.dsElement != null && this.dsElement.Constraints != null)
			{
				foreach (XmlSchemaObject xmlSchemaObject in this.dsElement.Constraints)
				{
					XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = (XmlSchemaIdentityConstraint)xmlSchemaObject;
					if (!(xmlSchemaIdentityConstraint is XmlSchemaKeyref) && this.GetTableName(xmlSchemaIdentityConstraint) == dataTable.TableName)
					{
						this.HandleConstraint(xmlSchemaIdentityConstraint);
					}
				}
			}
			dataTable.fNestedInDataset = false;
			return dataTable;
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00224888 File Offset: 0x00223C88
		internal string GetInstanceName(XmlSchemaAnnotated node)
		{
			string result = null;
			if (node is XmlSchemaElement)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)node;
				result = ((xmlSchemaElement.Name != null) ? xmlSchemaElement.Name : xmlSchemaElement.RefName.Name);
			}
			else if (node is XmlSchemaAttribute)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)node;
				result = ((xmlSchemaAttribute.Name != null) ? xmlSchemaAttribute.Name : xmlSchemaAttribute.RefName.Name);
			}
			return result;
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x002248F8 File Offset: 0x00223CF8
		internal DataTable InstantiateTable(XmlSchemaElement node, XmlSchemaComplexType typeNode, bool isRef)
		{
			string instanceName = this.GetInstanceName(node);
			ArrayList arrayList = new ArrayList();
			string @namespace = node.QualifiedName.Namespace;
			DataTable dataTable = this._ds.Tables.GetTable(XmlConvert.DecodeName(instanceName), @namespace);
			if (!this.FromInference || (this.FromInference && dataTable == null))
			{
				if (dataTable != null)
				{
					if (isRef)
					{
						return dataTable;
					}
					throw ExceptionBuilder.DuplicateDeclaration(instanceName);
				}
				else
				{
					if (isRef)
					{
						this.RefTables.Add(@namespace + ":" + instanceName);
					}
					dataTable = new DataTable(XmlConvert.DecodeName(instanceName));
					dataTable.TypeName = node.SchemaTypeName;
					dataTable.Namespace = @namespace;
					dataTable.Namespace = this.GetStringAttribute(node, "targetNamespace", @namespace);
					string text = this.GetStringAttribute(typeNode, "CaseSensitive", "");
					if (text.Length == 0)
					{
						text = this.GetStringAttribute(node, "CaseSensitive", "");
					}
					if (0 < text.Length)
					{
						if (text == "true" || text == "True")
						{
							dataTable.CaseSensitive = true;
						}
						if (text == "false" || text == "False")
						{
							dataTable.CaseSensitive = false;
						}
					}
					text = XSDSchema.GetMsdataAttribute(node, "Locale");
					if (text != null)
					{
						if (0 < text.Length)
						{
							dataTable.Locale = new CultureInfo(text);
						}
						else
						{
							dataTable.Locale = CultureInfo.InvariantCulture;
						}
					}
					if (!this.FromInference)
					{
						dataTable.MinOccurs = node.MinOccurs;
						dataTable.MaxOccurs = node.MaxOccurs;
					}
					else
					{
						string prefix = this.GetPrefix(@namespace);
						if (prefix != null)
						{
							dataTable.Prefix = prefix;
						}
					}
					this._ds.Tables.Add(dataTable);
					if (this.FromInference)
					{
						this.tableDictionary.Add(dataTable, new List<DataTable>());
					}
				}
			}
			this.HandleComplexType(typeNode, dataTable, arrayList, node.IsNillable);
			for (int i = 0; i < dataTable.Columns.Count; i++)
			{
				dataTable.Columns[i].SetOrdinalInternal(i);
			}
			XSDSchema.SetProperties(dataTable, node.UnhandledAttributes);
			XSDSchema.SetExtProperties(dataTable, node.UnhandledAttributes);
			if (this.dsElement != null && this.dsElement.Constraints != null)
			{
				foreach (XmlSchemaObject xmlSchemaObject in this.dsElement.Constraints)
				{
					XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = (XmlSchemaIdentityConstraint)xmlSchemaObject;
					if (!(xmlSchemaIdentityConstraint is XmlSchemaKeyref) && this.GetTableName(xmlSchemaIdentityConstraint) == dataTable.TableName && (this.GetTableNamespace(xmlSchemaIdentityConstraint) == dataTable.Namespace || this.GetTableNamespace(xmlSchemaIdentityConstraint) == null))
					{
						this.HandleConstraint(xmlSchemaIdentityConstraint);
					}
				}
			}
			foreach (object obj in arrayList)
			{
				DataTable dataTable2 = (DataTable)obj;
				if (dataTable2 != dataTable && dataTable.Namespace == dataTable2.Namespace)
				{
					dataTable2.tableNamespace = null;
				}
				if (this.dsElement != null && this.dsElement.Constraints != null)
				{
					foreach (XmlSchemaObject xmlSchemaObject2 in this.dsElement.Constraints)
					{
						XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint2 = (XmlSchemaIdentityConstraint)xmlSchemaObject2;
						XmlSchemaKeyref xmlSchemaKeyref = xmlSchemaIdentityConstraint2 as XmlSchemaKeyref;
						if (xmlSchemaKeyref != null)
						{
							bool booleanAttribute = this.GetBooleanAttribute(xmlSchemaKeyref, "IsNested", false);
							if (booleanAttribute && this.GetTableName(xmlSchemaKeyref) == dataTable2.TableName)
							{
								if (dataTable2.DataSet.Tables.InternalIndexOf(dataTable2.TableName) < -1)
								{
									if (this.GetTableNamespace(xmlSchemaKeyref) == dataTable2.Namespace)
									{
										this.HandleKeyref(xmlSchemaKeyref);
									}
								}
								else
								{
									this.HandleKeyref(xmlSchemaKeyref);
								}
							}
						}
					}
				}
				DataRelation dataRelation = null;
				DataRelationCollection childRelations = dataTable.ChildRelations;
				for (int j = 0; j < childRelations.Count; j++)
				{
					if (childRelations[j].Nested && dataTable2 == childRelations[j].ChildTable)
					{
						dataRelation = childRelations[j];
					}
				}
				if (dataRelation == null)
				{
					DataColumn dataColumn2;
					if (this.FromInference)
					{
						int num = dataTable.UKColumnPositionForInference;
						if (num == -1)
						{
							foreach (object obj2 in dataTable.Columns)
							{
								DataColumn dataColumn = (DataColumn)obj2;
								if (dataColumn.ColumnMapping == MappingType.Attribute)
								{
									num = dataColumn.Ordinal;
									break;
								}
							}
						}
						dataColumn2 = dataTable.AddUniqueKey(num);
					}
					else
					{
						dataColumn2 = dataTable.AddUniqueKey();
					}
					DataColumn dataColumn3 = dataTable2.AddForeignKey(dataColumn2);
					if (this.FromInference)
					{
						dataColumn3.Prefix = dataTable2.Prefix;
					}
					dataRelation = new DataRelation(dataTable.TableName + "_" + dataTable2.TableName, dataColumn2, dataColumn3, true);
					dataRelation.Nested = true;
					dataTable2.DataSet.Relations.Add(dataRelation);
					if (this.FromInference && dataRelation.Nested && this.tableDictionary.ContainsKey(dataRelation.ParentTable))
					{
						this.tableDictionary[dataRelation.ParentTable].Add(dataRelation.ChildTable);
					}
				}
			}
			return dataTable;
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00224EB8 File Offset: 0x002242B8
		public static Type XsdtoClr(string xsdTypeName)
		{
			int num = Array.BinarySearch(XSDSchema.mapNameTypeXsd, xsdTypeName);
			if (num < 0)
			{
				throw ExceptionBuilder.UndefinedDatatype(xsdTypeName);
			}
			return XSDSchema.mapNameTypeXsd[num].type;
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00224EE8 File Offset: 0x002242E8
		private static XSDSchema.NameType FindNameType(string name)
		{
			int num = Array.BinarySearch(XSDSchema.mapNameTypeXsd, name);
			if (num < 0)
			{
				throw ExceptionBuilder.UndefinedDatatype(name);
			}
			return XSDSchema.mapNameTypeXsd[num];
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00224F18 File Offset: 0x00224318
		private Type ParseDataType(string dt)
		{
			if (XSDSchema.IsXsdType(dt) || this.udSimpleTypes == null)
			{
				XSDSchema.NameType nameType = XSDSchema.FindNameType(dt);
				return nameType.type;
			}
			XmlSchemaSimpleType xmlSchemaSimpleType = (XmlSchemaSimpleType)this.udSimpleTypes[dt];
			if (xmlSchemaSimpleType == null)
			{
				throw ExceptionBuilder.UndefinedDatatype(dt);
			}
			SimpleType simpleType = new SimpleType(xmlSchemaSimpleType);
			while (simpleType.BaseSimpleType != null)
			{
				simpleType = simpleType.BaseSimpleType;
			}
			return this.ParseDataType(simpleType.BaseType);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00224F88 File Offset: 0x00224388
		internal static bool IsXsdType(string name)
		{
			int num = Array.BinarySearch(XSDSchema.mapNameTypeXsd, name);
			return num >= 0;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00224FA8 File Offset: 0x002243A8
		internal XmlSchemaAnnotated FindTypeNode(XmlSchemaAnnotated node)
		{
			XmlSchemaAttribute xmlSchemaAttribute = node as XmlSchemaAttribute;
			XmlSchemaElement xmlSchemaElement = node as XmlSchemaElement;
			bool flag = false;
			if (xmlSchemaAttribute != null)
			{
				flag = true;
			}
			string text = flag ? xmlSchemaAttribute.SchemaTypeName.Name : xmlSchemaElement.SchemaTypeName.Name;
			string a = flag ? xmlSchemaAttribute.SchemaTypeName.Namespace : xmlSchemaElement.SchemaTypeName.Namespace;
			if (a == "http://www.w3.org/2001/XMLSchema")
			{
				return null;
			}
			XmlSchemaAnnotated result;
			if (text == null || text.Length == 0)
			{
				text = (flag ? xmlSchemaAttribute.RefName.Name : xmlSchemaElement.RefName.Name);
				if (text == null || text.Length == 0)
				{
					result = (flag ? xmlSchemaAttribute.SchemaType : xmlSchemaElement.SchemaType);
				}
				else
				{
					result = (flag ? this.FindTypeNode((XmlSchemaAnnotated)this.attributes[xmlSchemaAttribute.RefName]) : this.FindTypeNode((XmlSchemaAnnotated)this.elementsTable[xmlSchemaElement.RefName]));
				}
			}
			else
			{
				result = (XmlSchemaAnnotated)this.schemaTypes[flag ? ((XmlSchemaAttribute)node).SchemaTypeName : ((XmlSchemaElement)node).SchemaTypeName];
			}
			return result;
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x002250D8 File Offset: 0x002244D8
		internal void HandleSimpleTypeSimpleContentColumn(XmlSchemaSimpleType typeNode, string strType, DataTable table, bool isBase, XmlAttribute[] attrs, bool isNillable)
		{
			if (this.FromInference && table.XmlText != null)
			{
				return;
			}
			Type type = null;
			SimpleType simpleType = null;
			if (typeNode.QualifiedName.Name != null && typeNode.QualifiedName.Name.Length != 0 && typeNode.QualifiedName.Namespace != "http://www.w3.org/2001/XMLSchema")
			{
				simpleType = new SimpleType(typeNode);
				strType = typeNode.QualifiedName.ToString();
				type = this.ParseDataType(typeNode.QualifiedName.ToString());
			}
			else
			{
				XmlSchemaSimpleType xmlSchemaSimpleType = typeNode.BaseXmlSchemaType as XmlSchemaSimpleType;
				if (xmlSchemaSimpleType != null && xmlSchemaSimpleType.QualifiedName.Namespace != "http://www.w3.org/2001/XMLSchema")
				{
					simpleType = new SimpleType(typeNode);
					SimpleType simpleType2 = simpleType;
					while (simpleType2.BaseSimpleType != null)
					{
						simpleType2 = simpleType2.BaseSimpleType;
					}
					type = this.ParseDataType(simpleType2.BaseType);
					strType = simpleType.Name;
				}
				else
				{
					type = this.ParseDataType(strType);
				}
			}
			string text;
			if (this.FromInference)
			{
				int num = 0;
				text = table.TableName + "_Text";
				while (table.Columns[text] != null)
				{
					text += num++;
				}
			}
			else
			{
				text = table.TableName + "_text";
			}
			string text2 = text;
			bool flag = true;
			DataColumn dataColumn;
			if (!isBase && table.Columns.Contains(text2, true))
			{
				dataColumn = table.Columns[text2];
				flag = false;
			}
			else
			{
				dataColumn = new DataColumn(text2, type, null, MappingType.SimpleContent);
			}
			XSDSchema.SetProperties(dataColumn, attrs);
			this.HandleColumnExpression(dataColumn, attrs);
			XSDSchema.SetExtProperties(dataColumn, attrs);
			int num2 = -1;
			string text3 = null;
			dataColumn.AllowDBNull = isNillable;
			if (attrs != null)
			{
				for (int i = 0; i < attrs.Length; i++)
				{
					string localName;
					if (attrs[i].NamespaceURI == "urn:schemas-microsoft-com:xml-msdata" && (localName = attrs[i].LocalName) != null)
					{
						if (!(localName == "AllowDBNull"))
						{
							if (!(localName == "Ordinal"))
							{
								if (localName == "DefaultValue")
								{
									text3 = attrs[i].Value;
								}
							}
							else
							{
								num2 = Convert.ToInt32(attrs[i].Value);
							}
						}
						else if (attrs[i].Value == "false")
						{
							dataColumn.AllowDBNull = false;
						}
					}
				}
			}
			if (dataColumn.Expression != null && dataColumn.Expression.Length != 0)
			{
				this.ColumnExpressions.Add(dataColumn);
			}
			if (simpleType != null && simpleType.Name != null && simpleType.Name.Length > 0)
			{
				if (XSDSchema.GetMsdataAttribute(typeNode, "targetNamespace") != null)
				{
					dataColumn.XmlDataType = simpleType.SimpleTypeQualifiedName;
				}
			}
			else
			{
				dataColumn.XmlDataType = strType;
			}
			dataColumn.SimpleType = simpleType;
			if (flag)
			{
				if (this.FromInference)
				{
					dataColumn.Prefix = this.GetPrefix(table.Namespace);
					dataColumn.AllowDBNull = true;
				}
				if (num2 > -1 && num2 < table.Columns.Count)
				{
					table.Columns.AddAt(num2, dataColumn);
				}
				else
				{
					table.Columns.Add(dataColumn);
				}
			}
			if (text3 != null)
			{
				try
				{
					dataColumn.DefaultValue = dataColumn.ConvertXmlToObject(text3);
				}
				catch (FormatException)
				{
					throw ExceptionBuilder.CannotConvert(text3, type.FullName);
				}
			}
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x00225428 File Offset: 0x00224828
		internal void HandleSimpleContentColumn(string strType, DataTable table, bool isBase, XmlAttribute[] attrs, bool isNillable)
		{
			if (this.FromInference && table.XmlText != null)
			{
				return;
			}
			Type type = null;
			if (strType == null)
			{
				return;
			}
			type = this.ParseDataType(strType);
			string text;
			if (this.FromInference)
			{
				int num = 0;
				text = table.TableName + "_Text";
				while (table.Columns[text] != null)
				{
					text += num++;
				}
			}
			else
			{
				text = table.TableName + "_text";
			}
			string text2 = text;
			bool flag = true;
			DataColumn dataColumn;
			if (!isBase && table.Columns.Contains(text2, true))
			{
				dataColumn = table.Columns[text2];
				flag = false;
			}
			else
			{
				dataColumn = new DataColumn(text2, type, null, MappingType.SimpleContent);
			}
			XSDSchema.SetProperties(dataColumn, attrs);
			this.HandleColumnExpression(dataColumn, attrs);
			XSDSchema.SetExtProperties(dataColumn, attrs);
			int num2 = -1;
			string text3 = null;
			dataColumn.AllowDBNull = isNillable;
			if (attrs != null)
			{
				for (int i = 0; i < attrs.Length; i++)
				{
					string localName;
					if (attrs[i].NamespaceURI == "urn:schemas-microsoft-com:xml-msdata" && (localName = attrs[i].LocalName) != null)
					{
						if (!(localName == "AllowDBNull"))
						{
							if (!(localName == "Ordinal"))
							{
								if (localName == "DefaultValue")
								{
									text3 = attrs[i].Value;
								}
							}
							else
							{
								num2 = Convert.ToInt32(attrs[i].Value);
							}
						}
						else if (attrs[i].Value == "false")
						{
							dataColumn.AllowDBNull = false;
						}
					}
				}
			}
			if (dataColumn.Expression != null && dataColumn.Expression.Length != 0)
			{
				this.ColumnExpressions.Add(dataColumn);
			}
			dataColumn.XmlDataType = strType;
			dataColumn.SimpleType = null;
			if (this.FromInference)
			{
				dataColumn.Prefix = this.GetPrefix(dataColumn.Namespace);
			}
			if (flag)
			{
				if (this.FromInference)
				{
					dataColumn.AllowDBNull = true;
				}
				if (num2 > -1 && num2 < table.Columns.Count)
				{
					table.Columns.AddAt(num2, dataColumn);
				}
				else
				{
					table.Columns.Add(dataColumn);
				}
			}
			if (text3 != null)
			{
				try
				{
					dataColumn.DefaultValue = dataColumn.ConvertXmlToObject(text3);
				}
				catch (FormatException)
				{
					throw ExceptionBuilder.CannotConvert(text3, type.FullName);
				}
			}
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00225688 File Offset: 0x00224A88
		internal void HandleAttributeColumn(XmlSchemaAttribute attrib, DataTable table, bool isBase)
		{
			Type type = null;
			XmlSchemaAttribute xmlSchemaAttribute = (attrib.Name != null) ? attrib : ((XmlSchemaAttribute)this.attributes[attrib.RefName]);
			XmlSchemaAnnotated xmlSchemaAnnotated = this.FindTypeNode(xmlSchemaAttribute);
			SimpleType simpleType = null;
			string text;
			if (xmlSchemaAnnotated == null)
			{
				text = xmlSchemaAttribute.SchemaTypeName.Name;
				if (ADP.IsEmpty(text))
				{
					text = "";
					type = typeof(string);
				}
				else if (xmlSchemaAttribute.SchemaTypeName.Namespace != "http://www.w3.org/2001/XMLSchema")
				{
					type = this.ParseDataType(xmlSchemaAttribute.SchemaTypeName.ToString());
				}
				else
				{
					type = this.ParseDataType(xmlSchemaAttribute.SchemaTypeName.Name);
				}
			}
			else if (xmlSchemaAnnotated is XmlSchemaSimpleType)
			{
				XmlSchemaSimpleType xmlSchemaSimpleType = xmlSchemaAnnotated as XmlSchemaSimpleType;
				simpleType = new SimpleType(xmlSchemaSimpleType);
				if (xmlSchemaSimpleType.QualifiedName.Name != null && xmlSchemaSimpleType.QualifiedName.Name.Length != 0 && xmlSchemaSimpleType.QualifiedName.Namespace != "http://www.w3.org/2001/XMLSchema")
				{
					text = xmlSchemaSimpleType.QualifiedName.ToString();
					type = this.ParseDataType(xmlSchemaSimpleType.QualifiedName.ToString());
				}
				else
				{
					type = this.ParseDataType(simpleType.BaseType);
					text = simpleType.Name;
					if (simpleType.Length == 1 && type == typeof(string))
					{
						type = typeof(char);
					}
				}
			}
			else if (xmlSchemaAnnotated is XmlSchemaElement)
			{
				text = ((XmlSchemaElement)xmlSchemaAnnotated).SchemaTypeName.Name;
				type = this.ParseDataType(text);
			}
			else
			{
				if (xmlSchemaAnnotated.Id == null)
				{
					throw ExceptionBuilder.DatatypeNotDefined();
				}
				throw ExceptionBuilder.UndefinedDatatype(xmlSchemaAnnotated.Id);
			}
			string text2 = XmlConvert.DecodeName(this.GetInstanceName(xmlSchemaAttribute));
			bool flag = true;
			DataColumn dataColumn;
			if ((!isBase || this.FromInference) && table.Columns.Contains(text2, true))
			{
				dataColumn = table.Columns[text2];
				flag = false;
				if (this.FromInference)
				{
					if (dataColumn.ColumnMapping != MappingType.Attribute)
					{
						throw ExceptionBuilder.ColumnTypeConflict(dataColumn.ColumnName);
					}
					if ((ADP.IsEmpty(attrib.QualifiedName.Namespace) && ADP.IsEmpty(dataColumn._columnUri)) || string.Compare(attrib.QualifiedName.Namespace, dataColumn.Namespace, StringComparison.Ordinal) == 0)
					{
						return;
					}
					dataColumn = new DataColumn(text2, type, null, MappingType.Attribute);
					flag = true;
				}
			}
			else
			{
				dataColumn = new DataColumn(text2, type, null, MappingType.Attribute);
			}
			XSDSchema.SetProperties(dataColumn, xmlSchemaAttribute.UnhandledAttributes);
			this.HandleColumnExpression(dataColumn, xmlSchemaAttribute.UnhandledAttributes);
			XSDSchema.SetExtProperties(dataColumn, xmlSchemaAttribute.UnhandledAttributes);
			if (dataColumn.Expression != null && dataColumn.Expression.Length != 0)
			{
				this.ColumnExpressions.Add(dataColumn);
			}
			if (simpleType != null && simpleType.Name != null && simpleType.Name.Length > 0)
			{
				if (XSDSchema.GetMsdataAttribute(xmlSchemaAnnotated, "targetNamespace") != null)
				{
					dataColumn.XmlDataType = simpleType.SimpleTypeQualifiedName;
				}
			}
			else
			{
				dataColumn.XmlDataType = text;
			}
			dataColumn.SimpleType = simpleType;
			dataColumn.AllowDBNull = (attrib.Use != XmlSchemaUse.Required);
			dataColumn.Namespace = attrib.QualifiedName.Namespace;
			dataColumn.Namespace = this.GetStringAttribute(attrib, "targetNamespace", dataColumn.Namespace);
			if (flag)
			{
				if (this.FromInference)
				{
					dataColumn.AllowDBNull = true;
					dataColumn.Prefix = this.GetPrefix(dataColumn.Namespace);
				}
				table.Columns.Add(dataColumn);
			}
			if (attrib.Use == XmlSchemaUse.Prohibited)
			{
				dataColumn.ColumnMapping = MappingType.Hidden;
				dataColumn.AllowDBNull = this.GetBooleanAttribute(xmlSchemaAttribute, "AllowDBNull", true);
				string msdataAttribute = XSDSchema.GetMsdataAttribute(xmlSchemaAttribute, "DefaultValue");
				if (msdataAttribute != null)
				{
					try
					{
						dataColumn.DefaultValue = dataColumn.ConvertXmlToObject(msdataAttribute);
					}
					catch (FormatException)
					{
						throw ExceptionBuilder.CannotConvert(msdataAttribute, type.FullName);
					}
				}
			}
			string text3 = (attrib.Use == XmlSchemaUse.Required) ? XSDSchema.GetMsdataAttribute(xmlSchemaAttribute, "DefaultValue") : xmlSchemaAttribute.DefaultValue;
			if (xmlSchemaAttribute.Use == XmlSchemaUse.Optional && text3 == null)
			{
				text3 = xmlSchemaAttribute.FixedValue;
			}
			if (text3 != null)
			{
				try
				{
					dataColumn.DefaultValue = dataColumn.ConvertXmlToObject(text3);
				}
				catch (FormatException)
				{
					throw ExceptionBuilder.CannotConvert(text3, type.FullName);
				}
			}
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00225AC8 File Offset: 0x00224EC8
		internal void HandleElementColumn(XmlSchemaElement elem, DataTable table, bool isBase)
		{
			Type type = null;
			XmlSchemaElement xmlSchemaElement = (elem.Name != null) ? elem : ((XmlSchemaElement)this.elementsTable[elem.RefName]);
			if (xmlSchemaElement == null)
			{
				return;
			}
			XmlSchemaAnnotated xmlSchemaAnnotated = this.FindTypeNode(xmlSchemaElement);
			string text = null;
			SimpleType simpleType = null;
			if (xmlSchemaAnnotated == null)
			{
				text = xmlSchemaElement.SchemaTypeName.Name;
				if (ADP.IsEmpty(text))
				{
					text = "";
					type = typeof(string);
				}
				else
				{
					type = this.ParseDataType(xmlSchemaElement.SchemaTypeName.Name);
				}
			}
			else if (xmlSchemaAnnotated is XmlSchemaSimpleType)
			{
				XmlSchemaSimpleType node = xmlSchemaAnnotated as XmlSchemaSimpleType;
				simpleType = new SimpleType(node);
				if (((XmlSchemaSimpleType)xmlSchemaAnnotated).Name != null && ((XmlSchemaSimpleType)xmlSchemaAnnotated).Name.Length != 0 && ((XmlSchemaSimpleType)xmlSchemaAnnotated).QualifiedName.Namespace != "http://www.w3.org/2001/XMLSchema")
				{
					XSDSchema.GetMsdataAttribute(xmlSchemaAnnotated, "targetNamespace");
					text = ((XmlSchemaSimpleType)xmlSchemaAnnotated).QualifiedName.ToString();
					type = this.ParseDataType(text);
				}
				else
				{
					for (node = ((simpleType.XmlBaseType != null && simpleType.XmlBaseType.Namespace != "http://www.w3.org/2001/XMLSchema") ? (this.schemaTypes[simpleType.XmlBaseType] as XmlSchemaSimpleType) : null); node != null; node = ((simpleType.XmlBaseType != null && simpleType.XmlBaseType.Namespace != "http://www.w3.org/2001/XMLSchema") ? (this.schemaTypes[simpleType.XmlBaseType] as XmlSchemaSimpleType) : null))
					{
						simpleType.LoadTypeValues(node);
					}
					type = this.ParseDataType(simpleType.BaseType);
					text = simpleType.Name;
					if (simpleType.Length == 1 && type == typeof(string))
					{
						type = typeof(char);
					}
				}
			}
			else if (xmlSchemaAnnotated is XmlSchemaElement)
			{
				text = ((XmlSchemaElement)xmlSchemaAnnotated).SchemaTypeName.Name;
				type = this.ParseDataType(text);
			}
			else if (xmlSchemaAnnotated is XmlSchemaComplexType)
			{
				if (ADP.IsEmpty(XSDSchema.GetMsdataAttribute(elem, "DataType")))
				{
					throw ExceptionBuilder.DatatypeNotDefined();
				}
				type = typeof(object);
			}
			else
			{
				if (xmlSchemaAnnotated.Id == null)
				{
					throw ExceptionBuilder.DatatypeNotDefined();
				}
				throw ExceptionBuilder.UndefinedDatatype(xmlSchemaAnnotated.Id);
			}
			string text2 = XmlConvert.DecodeName(this.GetInstanceName(xmlSchemaElement));
			bool flag = true;
			DataColumn dataColumn;
			if ((!isBase || this.FromInference) && table.Columns.Contains(text2, true))
			{
				dataColumn = table.Columns[text2];
				flag = false;
				if (this.FromInference)
				{
					if (dataColumn.ColumnMapping != MappingType.Element)
					{
						throw ExceptionBuilder.ColumnTypeConflict(dataColumn.ColumnName);
					}
					if ((ADP.IsEmpty(elem.QualifiedName.Namespace) && ADP.IsEmpty(dataColumn._columnUri)) || string.Compare(elem.QualifiedName.Namespace, dataColumn.Namespace, StringComparison.Ordinal) == 0)
					{
						return;
					}
					dataColumn = new DataColumn(text2, type, null, MappingType.Element);
					flag = true;
				}
			}
			else
			{
				dataColumn = new DataColumn(text2, type, null, MappingType.Element);
			}
			XSDSchema.SetProperties(dataColumn, xmlSchemaElement.UnhandledAttributes);
			this.HandleColumnExpression(dataColumn, xmlSchemaElement.UnhandledAttributes);
			XSDSchema.SetExtProperties(dataColumn, xmlSchemaElement.UnhandledAttributes);
			if (!ADP.IsEmpty(dataColumn.Expression))
			{
				this.ColumnExpressions.Add(dataColumn);
			}
			if (simpleType != null && simpleType.Name != null && simpleType.Name.Length > 0)
			{
				if (XSDSchema.GetMsdataAttribute(xmlSchemaAnnotated, "targetNamespace") != null)
				{
					dataColumn.XmlDataType = simpleType.SimpleTypeQualifiedName;
				}
			}
			else
			{
				dataColumn.XmlDataType = text;
			}
			dataColumn.SimpleType = simpleType;
			dataColumn.AllowDBNull = (this.FromInference || elem.MinOccurs == 0m || elem.IsNillable);
			if (!elem.RefName.IsEmpty || elem.QualifiedName.Namespace != table.Namespace)
			{
				dataColumn.Namespace = elem.QualifiedName.Namespace;
				dataColumn.Namespace = this.GetStringAttribute(xmlSchemaElement, "targetNamespace", dataColumn.Namespace);
			}
			else if (elem.Form == XmlSchemaForm.Unqualified)
			{
				dataColumn.Namespace = string.Empty;
			}
			else if (elem.Form == XmlSchemaForm.None)
			{
				XmlSchemaObject parent = elem.Parent;
				while (parent.Parent != null)
				{
					parent = parent.Parent;
				}
				if (((XmlSchema)parent).ElementFormDefault == XmlSchemaForm.Unqualified)
				{
					dataColumn.Namespace = string.Empty;
				}
			}
			else
			{
				dataColumn.Namespace = elem.QualifiedName.Namespace;
				dataColumn.Namespace = this.GetStringAttribute(xmlSchemaElement, "targetNamespace", dataColumn.Namespace);
			}
			if (flag)
			{
				int integerAttribute = this.GetIntegerAttribute(elem, "Ordinal", -1);
				if (integerAttribute > -1 && integerAttribute < table.Columns.Count)
				{
					table.Columns.AddAt(integerAttribute, dataColumn);
				}
				else
				{
					table.Columns.Add(dataColumn);
				}
			}
			if (dataColumn.Namespace == table.Namespace)
			{
				dataColumn._columnUri = null;
			}
			if (this.FromInference)
			{
				dataColumn.Prefix = this.GetPrefix(dataColumn.Namespace);
			}
			string defaultValue = xmlSchemaElement.DefaultValue;
			if (defaultValue != null)
			{
				try
				{
					dataColumn.DefaultValue = dataColumn.ConvertXmlToObject(defaultValue);
				}
				catch (FormatException)
				{
					throw ExceptionBuilder.CannotConvert(defaultValue, type.FullName);
				}
			}
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00226018 File Offset: 0x00225418
		internal void HandleDataSet(XmlSchemaElement node, bool isNewDataSet)
		{
			string text = node.Name;
			string @namespace = node.QualifiedName.Namespace;
			int count = this._ds.Tables.Count;
			List<DataTable> list = new List<DataTable>();
			string msdataAttribute = XSDSchema.GetMsdataAttribute(node, "Locale");
			if (msdataAttribute != null)
			{
				if (msdataAttribute.Length != 0)
				{
					this._ds.Locale = new CultureInfo(msdataAttribute);
				}
				else
				{
					this._ds.Locale = CultureInfo.InvariantCulture;
				}
			}
			else if (this.GetBooleanAttribute(node, "UseCurrentLocale", false))
			{
				this._ds.SetLocaleValue(CultureInfo.CurrentCulture, false);
			}
			else
			{
				this._ds.SetLocaleValue(new CultureInfo(1033), false);
			}
			msdataAttribute = XSDSchema.GetMsdataAttribute(node, "DataSetName");
			if (msdataAttribute != null && msdataAttribute.Length != 0)
			{
				text = msdataAttribute;
			}
			msdataAttribute = XSDSchema.GetMsdataAttribute(node, "DataSetNamespace");
			if (msdataAttribute != null && msdataAttribute.Length != 0)
			{
				@namespace = msdataAttribute;
			}
			XSDSchema.SetProperties(this._ds, node.UnhandledAttributes);
			XSDSchema.SetExtProperties(this._ds, node.UnhandledAttributes);
			if (text != null && text.Length != 0)
			{
				this._ds.DataSetName = XmlConvert.DecodeName(text);
			}
			this._ds.Namespace = @namespace;
			if (this.FromInference)
			{
				this._ds.Prefix = this.GetPrefix(this._ds.Namespace);
			}
			XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)this.FindTypeNode(node);
			if (xmlSchemaComplexType.Particle != null)
			{
				XmlSchemaObjectCollection particleItems = this.GetParticleItems(xmlSchemaComplexType.Particle);
				if (particleItems == null)
				{
					return;
				}
				foreach (XmlSchemaObject xmlSchemaObject in particleItems)
				{
					XmlSchemaAnnotated xmlSchemaAnnotated = (XmlSchemaAnnotated)xmlSchemaObject;
					if (xmlSchemaAnnotated is XmlSchemaElement)
					{
						if (((XmlSchemaElement)xmlSchemaAnnotated).RefName.Name.Length != 0)
						{
							if (!this.FromInference)
							{
								continue;
							}
							DataTable table = this._ds.Tables.GetTable(XmlConvert.DecodeName(this.GetInstanceName((XmlSchemaElement)xmlSchemaAnnotated)), node.QualifiedName.Namespace);
							if (table != null)
							{
								list.Add(table);
							}
							bool flag = false;
							if (node.ElementSchemaType != null || !(((XmlSchemaElement)xmlSchemaAnnotated).SchemaType is XmlSchemaComplexType))
							{
								flag = true;
							}
							if (((XmlSchemaElement)xmlSchemaAnnotated).MaxOccurs != 1m && !flag)
							{
								continue;
							}
						}
						DataTable dataTable = this.HandleTable((XmlSchemaElement)xmlSchemaAnnotated);
						if (dataTable != null)
						{
							dataTable.fNestedInDataset = true;
						}
						if (this.FromInference)
						{
							list.Add(dataTable);
						}
					}
					else if (xmlSchemaAnnotated is XmlSchemaChoice)
					{
						XmlSchemaObjectCollection items = ((XmlSchemaChoice)xmlSchemaAnnotated).Items;
						if (items != null)
						{
							foreach (XmlSchemaObject xmlSchemaObject2 in items)
							{
								XmlSchemaAnnotated xmlSchemaAnnotated2 = (XmlSchemaAnnotated)xmlSchemaObject2;
								if (xmlSchemaAnnotated2 is XmlSchemaElement)
								{
									if (((XmlSchemaParticle)xmlSchemaAnnotated).MaxOccurs > 1m && ((XmlSchemaElement)xmlSchemaAnnotated2).SchemaType is XmlSchemaComplexType)
									{
										((XmlSchemaElement)xmlSchemaAnnotated2).MaxOccurs = ((XmlSchemaParticle)xmlSchemaAnnotated).MaxOccurs;
									}
									if (((XmlSchemaElement)xmlSchemaAnnotated2).RefName.Name.Length == 0 || this.FromInference || !(((XmlSchemaElement)xmlSchemaAnnotated2).MaxOccurs != 1m) || ((XmlSchemaElement)xmlSchemaAnnotated2).SchemaType is XmlSchemaComplexType)
									{
										DataTable dataTable2 = this.HandleTable((XmlSchemaElement)xmlSchemaAnnotated2);
										if (this.FromInference)
										{
											list.Add(dataTable2);
										}
										if (dataTable2 != null)
										{
											dataTable2.fNestedInDataset = true;
										}
									}
								}
							}
						}
					}
				}
			}
			if (node.Constraints != null)
			{
				foreach (XmlSchemaObject xmlSchemaObject3 in node.Constraints)
				{
					XmlSchemaIdentityConstraint xmlSchemaIdentityConstraint = (XmlSchemaIdentityConstraint)xmlSchemaObject3;
					XmlSchemaKeyref xmlSchemaKeyref = xmlSchemaIdentityConstraint as XmlSchemaKeyref;
					if (xmlSchemaKeyref != null && !this.GetBooleanAttribute(xmlSchemaKeyref, "IsNested", false))
					{
						this.HandleKeyref(xmlSchemaKeyref);
					}
				}
			}
			if (this.FromInference && isNewDataSet)
			{
				List<DataTable> tableList = new List<DataTable>(this._ds.Tables.Count);
				foreach (DataTable dt in list)
				{
					this.AddTablesToList(tableList, dt);
				}
				this._ds.Tables.ReplaceFromInference(tableList);
			}
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00226518 File Offset: 0x00225918
		private void AddTablesToList(List<DataTable> tableList, DataTable dt)
		{
			if (!tableList.Contains(dt))
			{
				tableList.Add(dt);
				foreach (DataTable dt2 in this.tableDictionary[dt])
				{
					this.AddTablesToList(tableList, dt2);
				}
			}
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00226598 File Offset: 0x00225998
		private string GetPrefix(string ns)
		{
			if (ns == null)
			{
				return null;
			}
			foreach (object obj in this._schemaSet.Schemas())
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				XmlQualifiedName[] array = xmlSchema.Namespaces.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].Namespace == ns)
					{
						return array[i].Name;
					}
				}
			}
			return null;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00226648 File Offset: 0x00225A48
		private string GetNamespaceFromPrefix(string prefix)
		{
			if (prefix == null || prefix.Length == 0)
			{
				return null;
			}
			foreach (object obj in this._schemaSet.Schemas())
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				XmlQualifiedName[] array = xmlSchema.Namespaces.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].Name == prefix)
					{
						return array[i].Namespace;
					}
				}
			}
			return null;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x002266F8 File Offset: 0x00225AF8
		private string GetTableNamespace(XmlSchemaIdentityConstraint key)
		{
			string xpath = key.Selector.XPath;
			string[] array = xpath.Split(new char[]
			{
				'/'
			});
			string text = string.Empty;
			string text2 = array[array.Length - 1];
			if (text2 == null || text2.Length == 0)
			{
				throw ExceptionBuilder.InvalidSelector(xpath);
			}
			if (text2.IndexOf(':') != -1)
			{
				text = text2.Substring(0, text2.IndexOf(':'));
				text = XmlConvert.DecodeName(text);
				return this.GetNamespaceFromPrefix(text);
			}
			return XSDSchema.GetMsdataAttribute(key, "TableNamespace");
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00226788 File Offset: 0x00225B88
		private string GetTableName(XmlSchemaIdentityConstraint key)
		{
			string xpath = key.Selector.XPath;
			string[] array = xpath.Split(new char[]
			{
				'/',
				':'
			});
			string text = array[array.Length - 1];
			if (text == null || text.Length == 0)
			{
				throw ExceptionBuilder.InvalidSelector(xpath);
			}
			return XmlConvert.DecodeName(text);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x002267E8 File Offset: 0x00225BE8
		internal bool IsTable(XmlSchemaElement node)
		{
			if (node.MaxOccurs == 0m)
			{
				return false;
			}
			XmlAttribute[] unhandledAttributes = node.UnhandledAttributes;
			if (unhandledAttributes != null)
			{
				foreach (XmlAttribute xmlAttribute in unhandledAttributes)
				{
					if (xmlAttribute.LocalName == "DataType" && xmlAttribute.Prefix == "msdata" && xmlAttribute.NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
					{
						return false;
					}
				}
			}
			object obj = this.FindTypeNode(node);
			if (node.MaxOccurs > 1m && obj == null)
			{
				return true;
			}
			if (obj == null || !(obj is XmlSchemaComplexType))
			{
				return false;
			}
			XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)obj;
			if (xmlSchemaComplexType.IsAbstract)
			{
				throw ExceptionBuilder.CannotInstantiateAbstract(node.Name);
			}
			return true;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x002268B8 File Offset: 0x00225CB8
		internal DataTable HandleTable(XmlSchemaElement node)
		{
			if (!this.IsTable(node))
			{
				return null;
			}
			object obj = this.FindTypeNode(node);
			if (node.MaxOccurs > 1m && obj == null)
			{
				return this.InstantiateSimpleTable(node);
			}
			DataTable dataTable = this.InstantiateTable(node, (XmlSchemaComplexType)obj, node.RefName != null);
			dataTable.fNestedInDataset = false;
			return dataTable;
		}

		// Token: 0x04000A93 RID: 2707
		private XmlSchemaSet _schemaSet;

		// Token: 0x04000A94 RID: 2708
		private XmlSchemaElement dsElement;

		// Token: 0x04000A95 RID: 2709
		private DataSet _ds;

		// Token: 0x04000A96 RID: 2710
		private string _schemaName;

		// Token: 0x04000A97 RID: 2711
		private ArrayList ColumnExpressions;

		// Token: 0x04000A98 RID: 2712
		private Hashtable ConstraintNodes;

		// Token: 0x04000A99 RID: 2713
		private ArrayList RefTables;

		// Token: 0x04000A9A RID: 2714
		private ArrayList complexTypes;

		// Token: 0x04000A9B RID: 2715
		private XmlSchemaObjectCollection annotations;

		// Token: 0x04000A9C RID: 2716
		private XmlSchemaObjectCollection elements;

		// Token: 0x04000A9D RID: 2717
		private Hashtable attributes;

		// Token: 0x04000A9E RID: 2718
		private Hashtable elementsTable;

		// Token: 0x04000A9F RID: 2719
		private Hashtable attributeGroups;

		// Token: 0x04000AA0 RID: 2720
		private Hashtable schemaTypes;

		// Token: 0x04000AA1 RID: 2721
		private Hashtable expressions;

		// Token: 0x04000AA2 RID: 2722
		private Dictionary<DataTable, List<DataTable>> tableDictionary;

		// Token: 0x04000AA3 RID: 2723
		private Hashtable udSimpleTypes;

		// Token: 0x04000AA4 RID: 2724
		private Hashtable existingSimpleTypeMap;

		// Token: 0x04000AA5 RID: 2725
		private bool fromInference;

		// Token: 0x04000AA6 RID: 2726
		private static readonly XSDSchema.NameType[] mapNameTypeXsd = new XSDSchema.NameType[]
		{
			new XSDSchema.NameType("ENTITIES", typeof(string)),
			new XSDSchema.NameType("ENTITY", typeof(string)),
			new XSDSchema.NameType("ID", typeof(string)),
			new XSDSchema.NameType("IDREF", typeof(string)),
			new XSDSchema.NameType("IDREFS", typeof(string)),
			new XSDSchema.NameType("NCName", typeof(string)),
			new XSDSchema.NameType("NMTOKEN", typeof(string)),
			new XSDSchema.NameType("NMTOKENS", typeof(string)),
			new XSDSchema.NameType("NOTATION", typeof(string)),
			new XSDSchema.NameType("Name", typeof(string)),
			new XSDSchema.NameType("QName", typeof(string)),
			new XSDSchema.NameType("anyType", typeof(object)),
			new XSDSchema.NameType("anyURI", typeof(Uri)),
			new XSDSchema.NameType("base64Binary", typeof(byte[])),
			new XSDSchema.NameType("boolean", typeof(bool)),
			new XSDSchema.NameType("byte", typeof(sbyte)),
			new XSDSchema.NameType("date", typeof(DateTime)),
			new XSDSchema.NameType("dateTime", typeof(DateTime)),
			new XSDSchema.NameType("decimal", typeof(decimal)),
			new XSDSchema.NameType("double", typeof(double)),
			new XSDSchema.NameType("duration", typeof(TimeSpan)),
			new XSDSchema.NameType("float", typeof(float)),
			new XSDSchema.NameType("gDay", typeof(DateTime)),
			new XSDSchema.NameType("gMonth", typeof(DateTime)),
			new XSDSchema.NameType("gMonthDay", typeof(DateTime)),
			new XSDSchema.NameType("gYear", typeof(DateTime)),
			new XSDSchema.NameType("gYearMonth", typeof(DateTime)),
			new XSDSchema.NameType("hexBinary", typeof(byte[])),
			new XSDSchema.NameType("int", typeof(int)),
			new XSDSchema.NameType("integer", typeof(long)),
			new XSDSchema.NameType("language", typeof(string)),
			new XSDSchema.NameType("long", typeof(long)),
			new XSDSchema.NameType("negativeInteger", typeof(long)),
			new XSDSchema.NameType("nonNegativeInteger", typeof(ulong)),
			new XSDSchema.NameType("nonPositiveInteger", typeof(long)),
			new XSDSchema.NameType("normalizedString", typeof(string)),
			new XSDSchema.NameType("positiveInteger", typeof(ulong)),
			new XSDSchema.NameType("short", typeof(short)),
			new XSDSchema.NameType("string", typeof(string)),
			new XSDSchema.NameType("time", typeof(DateTime)),
			new XSDSchema.NameType("unsignedByte", typeof(byte)),
			new XSDSchema.NameType("unsignedInt", typeof(uint)),
			new XSDSchema.NameType("unsignedLong", typeof(ulong)),
			new XSDSchema.NameType("unsignedShort", typeof(ushort))
		};

		// Token: 0x020000FC RID: 252
		private sealed class NameType : IComparable
		{
			// Token: 0x06000ECC RID: 3788 RVA: 0x00226D78 File Offset: 0x00226178
			public NameType(string n, Type t)
			{
				this.name = n;
				this.type = t;
			}

			// Token: 0x06000ECD RID: 3789 RVA: 0x00226DA8 File Offset: 0x002261A8
			public int CompareTo(object obj)
			{
				return string.Compare(this.name, (string)obj, StringComparison.Ordinal);
			}

			// Token: 0x04000AA7 RID: 2727
			public readonly string name;

			// Token: 0x04000AA8 RID: 2728
			public readonly Type type;
		}
	}
}
