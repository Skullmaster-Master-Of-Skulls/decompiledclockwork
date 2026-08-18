using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using System.Xml;

namespace System.Data
{
	// Token: 0x020000EE RID: 238
	internal sealed class XDRSchema : XMLSchema
	{
		// Token: 0x06000DC4 RID: 3524 RVA: 0x00217FD8 File Offset: 0x002173D8
		internal XDRSchema(DataSet ds, bool fInline)
		{
			this._schemaUri = string.Empty;
			this._schemaName = string.Empty;
			this._schemaRoot = null;
			this._ds = ds;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00218018 File Offset: 0x00217418
		internal void LoadSchema(XmlElement schemaRoot, DataSet ds)
		{
			if (schemaRoot == null)
			{
				return;
			}
			this._schemaRoot = schemaRoot;
			this._ds = ds;
			this._schemaName = schemaRoot.GetAttribute("name");
			this._schemaUri = "";
			if (this._schemaName == null || this._schemaName.Length == 0)
			{
				this._schemaName = "NewDataSet";
			}
			ds.Namespace = this._schemaUri;
			for (XmlNode xmlNode = schemaRoot.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode is XmlElement)
				{
					XmlElement node = (XmlElement)xmlNode;
					if (XMLSchema.FEqualIdentity(node, "ElementType", "urn:schemas-microsoft-com:xml-data"))
					{
						this.HandleTable(node);
					}
				}
			}
			this._schemaName = XmlConvert.DecodeName(this._schemaName);
			if (ds.Tables[this._schemaName] == null)
			{
				ds.DataSetName = this._schemaName;
			}
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x002180F8 File Offset: 0x002174F8
		internal XmlElement FindTypeNode(XmlElement node)
		{
			if (XMLSchema.FEqualIdentity(node, "ElementType", "urn:schemas-microsoft-com:xml-data"))
			{
				return node;
			}
			string attribute = node.GetAttribute("type");
			if (!XMLSchema.FEqualIdentity(node, "element", "urn:schemas-microsoft-com:xml-data") && !XMLSchema.FEqualIdentity(node, "attribute", "urn:schemas-microsoft-com:xml-data"))
			{
				return null;
			}
			if (attribute == null || attribute.Length == 0)
			{
				return null;
			}
			XmlNode xmlNode = node.OwnerDocument.FirstChild;
			XmlNode ownerDocument = node.OwnerDocument;
			while (xmlNode != ownerDocument)
			{
				if (((XMLSchema.FEqualIdentity(xmlNode, "ElementType", "urn:schemas-microsoft-com:xml-data") && XMLSchema.FEqualIdentity(node, "element", "urn:schemas-microsoft-com:xml-data")) || (XMLSchema.FEqualIdentity(xmlNode, "AttributeType", "urn:schemas-microsoft-com:xml-data") && XMLSchema.FEqualIdentity(node, "attribute", "urn:schemas-microsoft-com:xml-data"))) && xmlNode is XmlElement && ((XmlElement)xmlNode).GetAttribute("name") == attribute)
				{
					return (XmlElement)xmlNode;
				}
				if (xmlNode.FirstChild != null)
				{
					xmlNode = xmlNode.FirstChild;
				}
				else if (xmlNode.NextSibling != null)
				{
					xmlNode = xmlNode.NextSibling;
				}
				else
				{
					while (xmlNode != ownerDocument)
					{
						xmlNode = xmlNode.ParentNode;
						if (xmlNode.NextSibling != null)
						{
							xmlNode = xmlNode.NextSibling;
							break;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00218228 File Offset: 0x00217628
		internal bool IsTextOnlyContent(XmlElement node)
		{
			string attribute = node.GetAttribute("content");
			if (attribute == null || attribute.Length == 0)
			{
				string attribute2 = node.GetAttribute("type", "urn:schemas-microsoft-com:datatypes");
				return attribute2 != null && attribute2.Length > 0;
			}
			if (attribute == "empty" || attribute == "eltOnly" || attribute == "elementOnly" || attribute == "mixed")
			{
				return false;
			}
			if (attribute == "textOnly")
			{
				return true;
			}
			throw ExceptionBuilder.InvalidAttributeValue("content", attribute);
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x002182C8 File Offset: 0x002176C8
		internal bool IsXDRField(XmlElement node, XmlElement typeNode)
		{
			int num = 1;
			int num2 = 1;
			if (!this.IsTextOnlyContent(typeNode))
			{
				return false;
			}
			for (XmlNode xmlNode = typeNode.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (XMLSchema.FEqualIdentity(xmlNode, "element", "urn:schemas-microsoft-com:xml-data") || XMLSchema.FEqualIdentity(xmlNode, "attribute", "urn:schemas-microsoft-com:xml-data"))
				{
					return false;
				}
			}
			if (XMLSchema.FEqualIdentity(node, "element", "urn:schemas-microsoft-com:xml-data"))
			{
				this.GetMinMax(node, ref num, ref num2);
				if (num2 == -1 || num2 > 1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00218348 File Offset: 0x00217748
		internal DataTable HandleTable(XmlElement node)
		{
			XmlElement xmlElement = this.FindTypeNode(node);
			string attribute = node.GetAttribute("minOccurs");
			if (attribute != null && attribute.Length > 0 && Convert.ToInt32(attribute, CultureInfo.InvariantCulture) > 1 && xmlElement == null)
			{
				return this.InstantiateSimpleTable(this._ds, node);
			}
			attribute = node.GetAttribute("maxOccurs");
			if (attribute != null && attribute.Length > 0 && string.Compare(attribute, "1", StringComparison.Ordinal) != 0 && xmlElement == null)
			{
				return this.InstantiateSimpleTable(this._ds, node);
			}
			if (xmlElement == null)
			{
				return null;
			}
			if (this.IsXDRField(node, xmlElement))
			{
				return null;
			}
			return this.InstantiateTable(this._ds, node, xmlElement);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x002183F8 File Offset: 0x002177F8
		private static XDRSchema.NameType FindNameType(string name)
		{
			int num = Array.BinarySearch(XDRSchema.mapNameTypeXdr, name);
			if (num < 0)
			{
				throw ExceptionBuilder.UndefinedDatatype(name);
			}
			return XDRSchema.mapNameTypeXdr[num];
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00218428 File Offset: 0x00217828
		private Type ParseDataType(string dt, string dtValues)
		{
			string name = dt;
			string[] array = dt.Split(XDRSchema.colonArray);
			if (array.Length > 2)
			{
				throw ExceptionBuilder.InvalidAttributeValue("type", dt);
			}
			if (array.Length == 2)
			{
				name = array[1];
			}
			XDRSchema.NameType nameType = XDRSchema.FindNameType(name);
			if (nameType == XDRSchema.enumerationNameType && (dtValues == null || dtValues.Length == 0))
			{
				throw ExceptionBuilder.MissingAttribute("type", "values");
			}
			return nameType.type;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00218498 File Offset: 0x00217898
		internal string GetInstanceName(XmlElement node)
		{
			string attribute;
			if (XMLSchema.FEqualIdentity(node, "ElementType", "urn:schemas-microsoft-com:xml-data") || XMLSchema.FEqualIdentity(node, "AttributeType", "urn:schemas-microsoft-com:xml-data"))
			{
				attribute = node.GetAttribute("name");
				if (attribute == null || attribute.Length == 0)
				{
					throw ExceptionBuilder.MissingAttribute("Element", "name");
				}
			}
			else
			{
				attribute = node.GetAttribute("type");
				if (attribute == null || attribute.Length == 0)
				{
					throw ExceptionBuilder.MissingAttribute("Element", "type");
				}
			}
			return attribute;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00218518 File Offset: 0x00217918
		internal void HandleColumn(XmlElement node, DataTable table)
		{
			int num = 0;
			int num2 = 1;
			node.GetAttribute("use");
			string name;
			DataColumn dataColumn;
			if (node.Attributes.Count > 0)
			{
				string attribute = node.GetAttribute("ref");
				if (attribute != null && attribute.Length > 0)
				{
					return;
				}
				string instanceName;
				name = (instanceName = this.GetInstanceName(node));
				dataColumn = table.Columns[name, this._schemaUri];
				if (dataColumn != null)
				{
					if (dataColumn.ColumnMapping == MappingType.Attribute)
					{
						if (XMLSchema.FEqualIdentity(node, "attribute", "urn:schemas-microsoft-com:xml-data"))
						{
							throw ExceptionBuilder.DuplicateDeclaration(instanceName);
						}
					}
					else if (XMLSchema.FEqualIdentity(node, "element", "urn:schemas-microsoft-com:xml-data"))
					{
						throw ExceptionBuilder.DuplicateDeclaration(instanceName);
					}
					name = XMLSchema.GenUniqueColumnName(instanceName, table);
				}
			}
			else
			{
				name = "";
			}
			XmlElement xmlElement = this.FindTypeNode(node);
			SimpleType simpleType = null;
			string text;
			if (xmlElement == null)
			{
				text = node.GetAttribute("type");
				throw ExceptionBuilder.UndefinedDatatype(text);
			}
			text = xmlElement.GetAttribute("type", "urn:schemas-microsoft-com:datatypes");
			string attribute2 = xmlElement.GetAttribute("values", "urn:schemas-microsoft-com:datatypes");
			Type type;
			if (text == null || text.Length == 0)
			{
				text = "";
				type = typeof(string);
			}
			else
			{
				type = this.ParseDataType(text, attribute2);
				if (text == "float")
				{
					text = "";
				}
				if (text == "char")
				{
					text = "";
					simpleType = SimpleType.CreateSimpleType(type);
				}
				if (text == "enumeration")
				{
					text = "";
					simpleType = SimpleType.CreateEnumeratedType(attribute2);
				}
				if (text == "bin.base64")
				{
					text = "";
					simpleType = SimpleType.CreateByteArrayType("base64");
				}
				if (text == "bin.hex")
				{
					text = "";
					simpleType = SimpleType.CreateByteArrayType("hex");
				}
			}
			bool flag = XMLSchema.FEqualIdentity(node, "attribute", "urn:schemas-microsoft-com:xml-data");
			this.GetMinMax(node, flag, ref num, ref num2);
			string text2 = null;
			text2 = node.GetAttribute("default");
			bool flag2 = false;
			dataColumn = new DataColumn(XmlConvert.DecodeName(name), type, null, flag ? MappingType.Attribute : MappingType.Element);
			XMLSchema.SetProperties(dataColumn, node.Attributes);
			dataColumn.XmlDataType = text;
			dataColumn.SimpleType = simpleType;
			dataColumn.AllowDBNull = (num == 0 || flag2);
			dataColumn.Namespace = (flag ? string.Empty : this._schemaUri);
			if (node.Attributes != null)
			{
				for (int i = 0; i < node.Attributes.Count; i++)
				{
					if (node.Attributes[i].NamespaceURI == "urn:schemas-microsoft-com:xml-msdata" && node.Attributes[i].LocalName == "Expression")
					{
						dataColumn.Expression = node.Attributes[i].Value;
						break;
					}
				}
			}
			string attribute3 = node.GetAttribute("targetNamespace");
			if (attribute3 != null && attribute3.Length > 0)
			{
				dataColumn.Namespace = attribute3;
			}
			table.Columns.Add(dataColumn);
			if (text2 != null && text2.Length != 0)
			{
				try
				{
					dataColumn.DefaultValue = SqlConvert.ChangeTypeForXML(text2, type);
				}
				catch (FormatException)
				{
					throw ExceptionBuilder.CannotConvert(text2, type.FullName);
				}
			}
			for (XmlNode xmlNode = node.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (XMLSchema.FEqualIdentity(xmlNode, "description", "urn:schemas-microsoft-com:xml-data"))
				{
					dataColumn.Description(((XmlElement)xmlNode).InnerText);
				}
			}
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00218888 File Offset: 0x00217C88
		internal void GetMinMax(XmlElement elNode, ref int minOccurs, ref int maxOccurs)
		{
			this.GetMinMax(elNode, false, ref minOccurs, ref maxOccurs);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x002188A8 File Offset: 0x00217CA8
		internal void GetMinMax(XmlElement elNode, bool isAttribute, ref int minOccurs, ref int maxOccurs)
		{
			string attribute = elNode.GetAttribute("minOccurs");
			if (attribute != null && attribute.Length > 0)
			{
				try
				{
					minOccurs = int.Parse(attribute, CultureInfo.InvariantCulture);
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableExceptionType(e))
					{
						throw;
					}
					throw ExceptionBuilder.AttributeValues("minOccurs", "0", "1");
				}
			}
			attribute = elNode.GetAttribute("maxOccurs");
			if (attribute != null && attribute.Length > 0)
			{
				if (string.Compare(attribute, "*", StringComparison.Ordinal) == 0)
				{
					maxOccurs = -1;
					return;
				}
				try
				{
					maxOccurs = int.Parse(attribute, CultureInfo.InvariantCulture);
				}
				catch (Exception e2)
				{
					if (!ADP.IsCatchableExceptionType(e2))
					{
						throw;
					}
					throw ExceptionBuilder.AttributeValues("maxOccurs", "1", "*");
				}
				if (maxOccurs != 1)
				{
					throw ExceptionBuilder.AttributeValues("maxOccurs", "1", "*");
				}
			}
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x002189B8 File Offset: 0x00217DB8
		internal void HandleTypeNode(XmlElement typeNode, DataTable table, ArrayList tableChildren)
		{
			for (XmlNode xmlNode = typeNode.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode is XmlElement)
				{
					if (XMLSchema.FEqualIdentity(xmlNode, "element", "urn:schemas-microsoft-com:xml-data"))
					{
						DataTable dataTable = this.HandleTable((XmlElement)xmlNode);
						if (dataTable != null)
						{
							tableChildren.Add(dataTable);
							goto IL_6E;
						}
					}
					if (XMLSchema.FEqualIdentity(xmlNode, "attribute", "urn:schemas-microsoft-com:xml-data") || XMLSchema.FEqualIdentity(xmlNode, "element", "urn:schemas-microsoft-com:xml-data"))
					{
						this.HandleColumn((XmlElement)xmlNode, table);
					}
				}
				IL_6E:;
			}
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00218A48 File Offset: 0x00217E48
		internal DataTable InstantiateTable(DataSet dataSet, XmlElement node, XmlElement typeNode)
		{
			string name = "";
			XmlAttributeCollection attributes = node.Attributes;
			int value = 1;
			int value2 = 1;
			string text = null;
			ArrayList arrayList = new ArrayList();
			DataTable dataTable;
			if (attributes.Count > 0)
			{
				name = this.GetInstanceName(node);
				dataTable = dataSet.Tables.GetTable(name, this._schemaUri);
				if (dataTable != null)
				{
					return dataTable;
				}
			}
			dataTable = new DataTable(XmlConvert.DecodeName(name));
			dataTable.Namespace = this._schemaUri;
			this.GetMinMax(node, ref value, ref value2);
			dataTable.MinOccurs = value;
			dataTable.MaxOccurs = value2;
			this._ds.Tables.Add(dataTable);
			this.HandleTypeNode(typeNode, dataTable, arrayList);
			XMLSchema.SetProperties(dataTable, attributes);
			if (text != null)
			{
				string[] array = text.TrimEnd(null).Split(null);
				int num = array.Length;
				DataColumn[] array2 = new DataColumn[num];
				for (int i = 0; i < num; i++)
				{
					DataColumn dataColumn = dataTable.Columns[array[i], this._schemaUri];
					if (dataColumn == null)
					{
						throw ExceptionBuilder.ElementTypeNotFound(array[i]);
					}
					array2[i] = dataColumn;
				}
				dataTable.PrimaryKey = array2;
			}
			foreach (object obj in arrayList)
			{
				DataTable dataTable2 = (DataTable)obj;
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
					DataColumn dataColumn2 = dataTable.AddUniqueKey();
					DataColumn childColumn = dataTable2.AddForeignKey(dataColumn2);
					dataRelation = new DataRelation(dataTable.TableName + "_" + dataTable2.TableName, dataColumn2, childColumn, true);
					dataRelation.CheckMultipleNested = false;
					dataRelation.Nested = true;
					dataTable2.DataSet.Relations.Add(dataRelation);
					dataRelation.CheckMultipleNested = true;
				}
			}
			return dataTable;
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00218C68 File Offset: 0x00218068
		internal DataTable InstantiateSimpleTable(DataSet dataSet, XmlElement node)
		{
			XmlAttributeCollection attributes = node.Attributes;
			int value = 1;
			int value2 = 1;
			string instanceName = this.GetInstanceName(node);
			DataTable dataTable = dataSet.Tables.GetTable(instanceName, this._schemaUri);
			if (dataTable != null)
			{
				throw ExceptionBuilder.DuplicateDeclaration(instanceName);
			}
			string text = XmlConvert.DecodeName(instanceName);
			dataTable = new DataTable(text);
			dataTable.Namespace = this._schemaUri;
			this.GetMinMax(node, ref value, ref value2);
			dataTable.MinOccurs = value;
			dataTable.MaxOccurs = value2;
			XMLSchema.SetProperties(dataTable, attributes);
			dataTable.repeatableElement = true;
			this.HandleColumn(node, dataTable);
			dataTable.Columns[0].ColumnName = text + "_Column";
			this._ds.Tables.Add(dataTable);
			return dataTable;
		}

		// Token: 0x0400097C RID: 2428
		internal string _schemaName;

		// Token: 0x0400097D RID: 2429
		internal string _schemaUri;

		// Token: 0x0400097E RID: 2430
		internal XmlElement _schemaRoot;

		// Token: 0x0400097F RID: 2431
		internal DataSet _ds;

		// Token: 0x04000980 RID: 2432
		private static char[] colonArray = new char[]
		{
			':'
		};

		// Token: 0x04000981 RID: 2433
		private static XDRSchema.NameType[] mapNameTypeXdr = new XDRSchema.NameType[]
		{
			new XDRSchema.NameType("bin.base64", typeof(byte[])),
			new XDRSchema.NameType("bin.hex", typeof(byte[])),
			new XDRSchema.NameType("boolean", typeof(bool)),
			new XDRSchema.NameType("byte", typeof(sbyte)),
			new XDRSchema.NameType("char", typeof(char)),
			new XDRSchema.NameType("date", typeof(DateTime)),
			new XDRSchema.NameType("dateTime", typeof(DateTime)),
			new XDRSchema.NameType("dateTime.tz", typeof(DateTime)),
			new XDRSchema.NameType("entities", typeof(string)),
			new XDRSchema.NameType("entity", typeof(string)),
			new XDRSchema.NameType("enumeration", typeof(string)),
			new XDRSchema.NameType("fixed.14.4", typeof(decimal)),
			new XDRSchema.NameType("float", typeof(double)),
			new XDRSchema.NameType("i1", typeof(sbyte)),
			new XDRSchema.NameType("i2", typeof(short)),
			new XDRSchema.NameType("i4", typeof(int)),
			new XDRSchema.NameType("i8", typeof(long)),
			new XDRSchema.NameType("id", typeof(string)),
			new XDRSchema.NameType("idref", typeof(string)),
			new XDRSchema.NameType("idrefs", typeof(string)),
			new XDRSchema.NameType("int", typeof(int)),
			new XDRSchema.NameType("nmtoken", typeof(string)),
			new XDRSchema.NameType("nmtokens", typeof(string)),
			new XDRSchema.NameType("notation", typeof(string)),
			new XDRSchema.NameType("number", typeof(decimal)),
			new XDRSchema.NameType("r4", typeof(float)),
			new XDRSchema.NameType("r8", typeof(double)),
			new XDRSchema.NameType("string", typeof(string)),
			new XDRSchema.NameType("time", typeof(DateTime)),
			new XDRSchema.NameType("time.tz", typeof(DateTime)),
			new XDRSchema.NameType("ui1", typeof(byte)),
			new XDRSchema.NameType("ui2", typeof(ushort)),
			new XDRSchema.NameType("ui4", typeof(uint)),
			new XDRSchema.NameType("ui8", typeof(ulong)),
			new XDRSchema.NameType("uri", typeof(string)),
			new XDRSchema.NameType("uuid", typeof(Guid))
		};

		// Token: 0x04000982 RID: 2434
		private static XDRSchema.NameType enumerationNameType = XDRSchema.FindNameType("enumeration");

		// Token: 0x020000EF RID: 239
		private sealed class NameType : IComparable
		{
			// Token: 0x06000DD4 RID: 3540 RVA: 0x002190D8 File Offset: 0x002184D8
			public NameType(string n, Type t)
			{
				this.name = n;
				this.type = t;
			}

			// Token: 0x06000DD5 RID: 3541 RVA: 0x00219108 File Offset: 0x00218508
			public int CompareTo(object obj)
			{
				return string.Compare(this.name, (string)obj, StringComparison.Ordinal);
			}

			// Token: 0x04000983 RID: 2435
			public string name;

			// Token: 0x04000984 RID: 2436
			public Type type;
		}
	}
}
