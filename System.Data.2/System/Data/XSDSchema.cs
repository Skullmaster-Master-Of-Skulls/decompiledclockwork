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
	// Token: 0x02000143 RID: 323
	internal sealed class XSDSchema : XMLSchema
	{
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x00094974 File Offset: 0x00093D74
		// (set) Token: 0x06001302 RID: 4866 RVA: 0x00094988 File Offset: 0x00093D88
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

		// Token: 0x06001303 RID: 4867 RVA: 0x0009499C File Offset: 0x00093D9C
		private void CollectElementsAnnotations(XmlSchema schema)
		{
			ArrayList arrayList = new ArrayList();
			this.CollectElementsAnnotations(schema, arrayList);
			arrayList.Clear();
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x000949C0 File Offset: 0x00093DC0
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
						string msdataAttribute = XSDSchema.GetMsdataAttribute((XmlSchemaType)obj, "targetNamespace");
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

		// Token: 0x06001305 RID: 4869 RVA: 0x00094C10 File Offset: 0x00094010
		internal static string QualifiedName(string name)
		{
			int num = name.IndexOf(':');
			if (num == -1)
			{
				return "xs:" + name;
			}
			return name;
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00094C38 File Offset: 0x00094038
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
								dataColumn.DataType = DataStorage.GetType(value);
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
									if (!(propertyType == typeof(CultureInfo)))
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

		// Token: 0x06001307 RID: 4871 RVA: 0x00094DA0 File Offset: 0x000941A0
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

		// Token: 0x06001308 RID: 4872 RVA: 0x00094E70 File Offset: 0x00094270
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

		// Token: 0x06001309 RID: 4873 RVA: 0x00094EF8 File Offset: 0x000942F8
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

		// Token: 0x0600130A RID: 4874 RVA: 0x00094F4C File Offset: 0x0009434C
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

		// Token: 0x0600130B RID: 4875 RVA: 0x00094FCC File Offset: 0x000943CC
		internal void HandleRefTableProperties(ArrayList RefTables, XmlSchemaElement element)
		{
			string instanceName = this.GetInstanceName(element);
			DataTable table = this._ds.Tables.GetTable(XmlConvert.DecodeName(instanceName), element.QualifiedName.Namespace);
			XSDSchema.SetProperties(table, element.UnhandledAttributes);
			XSDSchema.SetExtProperties(table, element.UnhandledAttributes);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0009501C File Offset: 0x0009441C
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

		// Token: 0x0600130D RID: 4877 RVA: 0x000952A4 File Offset: 0x000946A4
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

		// Token: 0x0600130E RID: 4878 RVA: 0x00095320 File Offset: 0x00094720
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

		// Token: 0x0600130F RID: 4879 RVA: 0x00095460 File Offset: 0x00094860
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

		// Token: 0x06001310 RID: 4880 RVA: 0x000954D0 File Offset: 0x000948D0
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

		// Token: 0x06001311 RID: 4881 RVA: 0x00095640 File Offset: 0x00094A40
		public void LoadSchema(XmlSchemaSet schemaSet, DataTable dt)
		{
			if (dt.DataSet != null)
			{
				this.LoadSchema(schemaSet, dt.DataSet);
			}
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00095664 File Offset: 0x00094A64
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
						DataTable dataTable2 = this.HandleTable(xmlSchemaElement2);
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
				DataTable dataTable3 = (DataTable)obj4;
				if (dataTable3.NestedParentRelations.Length == 0 && dataTable3.Namespace == ds.Namespace)
				{
					DataRelationCollection childRelations = dataTable3.ChildRelations;
					for (int j = 0; j < childRelations.Count; j++)
					{
						if (childRelations[j].Nested && dataTable3.Namespace == childRelations[j].ChildTable.Namespace)
						{
							childRelations[j].ChildTable.tableNamespace = null;
						}
					}
					dataTable3.tableNamespace = null;
				}
			}
			DataTable dataTable4 = ds.Tables[ds.DataSetName, ds.Namespace];
			if (dataTable4 != null)
			{
				dataTable4.fNestedInDataset = true;
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

		// Token: 0x06001313 RID: 4883 RVA: 0x00095DFC File Offset: 0x000951FC
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

		// Token: 0x06001314 RID: 4884 RVA: 0x00095EA0 File Offset: 0x000952A0
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

		// Token: 0x06001315 RID: 4885 RVA: 0x00095F28 File Offset: 0x00095328
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

		// Token: 0x06001316 RID: 4886 RVA: 0x000961A8 File Offset: 0x000955A8
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

		// Token: 0x06001317 RID: 4887 RVA: 0x00096240 File Offset: 0x00095640
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

		// Token: 0x06001318 RID: 4888 RVA: 0x00096318 File Offset: 0x00095718
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

		// Token: 0x06001319 RID: 4889 RVA: 0x000965CC File Offset: 0x000959CC
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

		// Token: 0x0600131A RID: 4890 RVA: 0x00096628 File Offset: 0x00095A28
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

		// Token: 0x0600131B RID: 4891 RVA: 0x000966B0 File Offset: 0x00095AB0
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

		// Token: 0x0600131C RID: 4892 RVA: 0x00096740 File Offset: 0x00095B40
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

		// Token: 0x0600131D RID: 4893 RVA: 0x000967A4 File Offset: 0x00095BA4
		internal string GetStringAttribute(XmlSchemaAnnotated element, string attrName, string defVal)
		{
			string msdataAttribute = XSDSchema.GetMsdataAttribute(element, attrName);
			if (msdataAttribute == null || msdataAttribute.Length == 0)
			{
				return defVal;
			}
			return msdataAttribute;
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x000967C8 File Offset: 0x00095BC8
		internal static AcceptRejectRule TranslateAcceptRejectRule(string strRule)
		{
			if (strRule == "Cascade")
			{
				return AcceptRejectRule.Cascade;
			}
			strRule == "None";
			return AcceptRejectRule.None;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x000967F4 File Offset: 0x00095BF4
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

		// Token: 0x06001320 RID: 4896 RVA: 0x00096840 File Offset: 0x00095C40
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

		// Token: 0x06001321 RID: 4897 RVA: 0x00096B0C File Offset: 0x00095F0C
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
			if (array.Length != 0)
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

		// Token: 0x06001322 RID: 4898 RVA: 0x00096C64 File Offset: 0x00096064
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
						text2 += num++.ToString();
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

		// Token: 0x06001323 RID: 4899 RVA: 0x00096EDC File Offset: 0x000962DC
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

		// Token: 0x06001324 RID: 4900 RVA: 0x00096F44 File Offset: 0x00096344
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

		// Token: 0x06001325 RID: 4901 RVA: 0x00097500 File Offset: 0x00096900
		public static Type XsdtoClr(string xsdTypeName)
		{
			int num = Array.BinarySearch(XSDSchema.mapNameTypeXsd, xsdTypeName);
			if (num < 0)
			{
				throw ExceptionBuilder.UndefinedDatatype(xsdTypeName);
			}
			return XSDSchema.mapNameTypeXsd[num].type;
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00097530 File Offset: 0x00096930
		private static XSDSchema.NameType FindNameType(string name)
		{
			int num = Array.BinarySearch(XSDSchema.mapNameTypeXsd, name);
			if (num < 0)
			{
				throw ExceptionBuilder.UndefinedDatatype(name);
			}
			return XSDSchema.mapNameTypeXsd[num];
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0009755C File Offset: 0x0009695C
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

		// Token: 0x06001328 RID: 4904 RVA: 0x000975C8 File Offset: 0x000969C8
		internal static bool IsXsdType(string name)
		{
			int num = Array.BinarySearch(XSDSchema.mapNameTypeXsd, name);
			return num >= 0;
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x000975E8 File Offset: 0x000969E8
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

		// Token: 0x0600132A RID: 4906 RVA: 0x00097710 File Offset: 0x00096B10
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
					text += num++.ToString();
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
			string value = -1.ToString(CultureInfo.CurrentCulture);
			string text3 = null;
			dataColumn.AllowDBNull = isNillable;
			if (attrs != null)
			{
				for (int i = 0; i < attrs.Length; i++)
				{
					if (attrs[i].LocalName == "AllowDBNull" && attrs[i].NamespaceURI == "urn:schemas-microsoft-com:xml-msdata" && attrs[i].Value == "false")
					{
						dataColumn.AllowDBNull = false;
					}
					if (attrs[i].LocalName == "Ordinal" && attrs[i].NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
					{
						value = attrs[i].Value;
					}
					if (attrs[i].LocalName == "DefaultValue" && attrs[i].NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
					{
						text3 = attrs[i].Value;
					}
				}
			}
			int num2 = (int)Convert.ChangeType(value, typeof(int), null);
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

		// Token: 0x0600132B RID: 4907 RVA: 0x00097AA0 File Offset: 0x00096EA0
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
					text += num++.ToString();
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
			string value = -1.ToString(CultureInfo.CurrentCulture);
			string text3 = null;
			dataColumn.AllowDBNull = isNillable;
			if (attrs != null)
			{
				for (int i = 0; i < attrs.Length; i++)
				{
					if (attrs[i].LocalName == "AllowDBNull" && attrs[i].NamespaceURI == "urn:schemas-microsoft-com:xml-msdata" && attrs[i].Value == "false")
					{
						dataColumn.AllowDBNull = false;
					}
					if (attrs[i].LocalName == "Ordinal" && attrs[i].NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
					{
						value = attrs[i].Value;
					}
					if (attrs[i].LocalName == "DefaultValue" && attrs[i].NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
					{
						text3 = attrs[i].Value;
					}
				}
			}
			int num2 = (int)Convert.ChangeType(value, typeof(int), null);
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

		// Token: 0x0600132C RID: 4908 RVA: 0x00097D40 File Offset: 0x00097140
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

		// Token: 0x0600132D RID: 4909 RVA: 0x00098180 File Offset: 0x00097580
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
					string msdataAttribute = XSDSchema.GetMsdataAttribute(xmlSchemaAnnotated, "targetNamespace");
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
			string stringAttribute = this.GetStringAttribute(elem, "Ordinal", -1.ToString(CultureInfo.CurrentCulture));
			int num = (int)Convert.ChangeType(stringAttribute, typeof(int), null);
			if (flag)
			{
				if (num > -1 && num < table.Columns.Count)
				{
					table.Columns.AddAt(num, dataColumn);
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

		// Token: 0x0600132E RID: 4910 RVA: 0x000986F0 File Offset: 0x00097AF0
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

		// Token: 0x0600132F RID: 4911 RVA: 0x00098BDC File Offset: 0x00097FDC
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

		// Token: 0x06001330 RID: 4912 RVA: 0x00098C54 File Offset: 0x00098054
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

		// Token: 0x06001331 RID: 4913 RVA: 0x00098CF8 File Offset: 0x000980F8
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

		// Token: 0x06001332 RID: 4914 RVA: 0x00098DA4 File Offset: 0x000981A4
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

		// Token: 0x06001333 RID: 4915 RVA: 0x00098E28 File Offset: 0x00098228
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

		// Token: 0x06001334 RID: 4916 RVA: 0x00098E7C File Offset: 0x0009827C
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

		// Token: 0x06001335 RID: 4917 RVA: 0x00098F3C File Offset: 0x0009833C
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

		// Token: 0x0400076E RID: 1902
		private XmlSchemaSet _schemaSet;

		// Token: 0x0400076F RID: 1903
		private XmlSchemaElement dsElement;

		// Token: 0x04000770 RID: 1904
		private DataSet _ds;

		// Token: 0x04000771 RID: 1905
		private string _schemaName;

		// Token: 0x04000772 RID: 1906
		private ArrayList ColumnExpressions;

		// Token: 0x04000773 RID: 1907
		private Hashtable ConstraintNodes;

		// Token: 0x04000774 RID: 1908
		private ArrayList RefTables;

		// Token: 0x04000775 RID: 1909
		private ArrayList complexTypes;

		// Token: 0x04000776 RID: 1910
		private XmlSchemaObjectCollection annotations;

		// Token: 0x04000777 RID: 1911
		private XmlSchemaObjectCollection elements;

		// Token: 0x04000778 RID: 1912
		private Hashtable attributes;

		// Token: 0x04000779 RID: 1913
		private Hashtable elementsTable;

		// Token: 0x0400077A RID: 1914
		private Hashtable attributeGroups;

		// Token: 0x0400077B RID: 1915
		private Hashtable schemaTypes;

		// Token: 0x0400077C RID: 1916
		private Hashtable expressions;

		// Token: 0x0400077D RID: 1917
		private Dictionary<DataTable, List<DataTable>> tableDictionary;

		// Token: 0x0400077E RID: 1918
		private Hashtable udSimpleTypes;

		// Token: 0x0400077F RID: 1919
		private Hashtable existingSimpleTypeMap;

		// Token: 0x04000780 RID: 1920
		private bool fromInference;

		// Token: 0x04000781 RID: 1921
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

		// Token: 0x02000365 RID: 869
		private sealed class NameType : IComparable
		{
			// Token: 0x06003449 RID: 13385 RVA: 0x00140A48 File Offset: 0x0013FE48
			public NameType(string n, Type t)
			{
				this.name = n;
				this.type = t;
			}

			// Token: 0x0600344A RID: 13386 RVA: 0x00140A6C File Offset: 0x0013FE6C
			public int CompareTo(object obj)
			{
				return string.Compare(this.name, (string)obj, StringComparison.Ordinal);
			}

			// Token: 0x04001F10 RID: 7952
			public readonly string name;

			// Token: 0x04001F11 RID: 7953
			public readonly Type type;
		}
	}
}
