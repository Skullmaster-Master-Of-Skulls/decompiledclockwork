using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x0200008B RID: 139
	[Obsolete("XmlDataDocument class will be removed in a future release.")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class XmlDataDocument : XmlDocument
	{
		// Token: 0x060006A5 RID: 1701 RVA: 0x0004B8F4 File Offset: 0x0004ACF4
		internal void AddPointer(IXmlDataVirtualNode pointer)
		{
			Hashtable obj = this.pointers;
			lock (obj)
			{
				this.countAddPointer++;
				if (this.countAddPointer >= 5)
				{
					ArrayList arrayList = new ArrayList();
					foreach (object obj2 in this.pointers)
					{
						IXmlDataVirtualNode xmlDataVirtualNode = (IXmlDataVirtualNode)((DictionaryEntry)obj2).Value;
						if (!xmlDataVirtualNode.IsInUse())
						{
							arrayList.Add(xmlDataVirtualNode);
						}
					}
					for (int i = 0; i < arrayList.Count; i++)
					{
						this.pointers.Remove(arrayList[i]);
					}
					this.countAddPointer = 0;
				}
				this.pointers[pointer] = pointer;
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0004BA04 File Offset: 0x0004AE04
		[Conditional("DEBUG")]
		internal void AssertPointerPresent(IXmlDataVirtualNode pointer)
		{
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0004BA14 File Offset: 0x0004AE14
		private void AttachDataSet(DataSet ds)
		{
			if (ds.FBoundToDocument)
			{
				throw new ArgumentException(Res.GetString("DataDom_MultipleDataSet"));
			}
			ds.FBoundToDocument = true;
			this.dataSet = ds;
			this.BindSpecialListeners();
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0004BA50 File Offset: 0x0004AE50
		internal void SyncRows(DataRow parentRow, XmlNode node, bool fAddRowsToTable)
		{
			XmlBoundElement xmlBoundElement = node as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				DataRow row = xmlBoundElement.Row;
				if (row != null && xmlBoundElement.ElementState == ElementState.Defoliated)
				{
					return;
				}
				if (row != null)
				{
					this.SynchronizeRowFromRowElement(xmlBoundElement);
					xmlBoundElement.ElementState = ElementState.WeakFoliation;
					this.DefoliateRegion(xmlBoundElement);
					if (parentRow != null)
					{
						XmlDataDocument.SetNestedParentRow(row, parentRow);
					}
					if (fAddRowsToTable && row.RowState == DataRowState.Detached)
					{
						row.Table.Rows.Add(row);
					}
					parentRow = row;
				}
			}
			for (XmlNode xmlNode = node.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				this.SyncRows(parentRow, xmlNode, fAddRowsToTable);
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0004BADC File Offset: 0x0004AEDC
		internal void SyncTree(XmlNode node)
		{
			XmlBoundElement xmlBoundElement = null;
			this.mapper.GetRegion(node, out xmlBoundElement);
			DataRow parentRow = null;
			bool flag = this.IsConnected(node);
			if (xmlBoundElement != null)
			{
				DataRow row = xmlBoundElement.Row;
				if (row != null && xmlBoundElement.ElementState == ElementState.Defoliated)
				{
					return;
				}
				if (row != null)
				{
					this.SynchronizeRowFromRowElement(xmlBoundElement);
					if (node == xmlBoundElement)
					{
						xmlBoundElement.ElementState = ElementState.WeakFoliation;
						this.DefoliateRegion(xmlBoundElement);
					}
					if (flag && row.RowState == DataRowState.Detached)
					{
						row.Table.Rows.Add(row);
					}
					parentRow = row;
				}
			}
			for (XmlNode xmlNode = node.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				this.SyncRows(parentRow, xmlNode, flag);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0004BB78 File Offset: 0x0004AF78
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x0004BB8C File Offset: 0x0004AF8C
		internal ElementState AutoFoliationState
		{
			get
			{
				return this.autoFoliationState;
			}
			set
			{
				this.autoFoliationState = value;
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0004BBA0 File Offset: 0x0004AFA0
		private void BindForLoad()
		{
			this.ignoreDataSetEvents = true;
			this.mapper.SetupMapping(this, this.dataSet);
			if (this.dataSet.Tables.Count > 0)
			{
				this.LoadDataSetFromTree();
			}
			this.BindListeners();
			this.ignoreDataSetEvents = false;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0004BBEC File Offset: 0x0004AFEC
		private void Bind(bool fLoadFromDataSet)
		{
			this.ignoreDataSetEvents = true;
			this.ignoreXmlEvents = true;
			this.mapper.SetupMapping(this, this.dataSet);
			if (base.DocumentElement != null)
			{
				this.LoadDataSetFromTree();
				this.BindListeners();
			}
			else if (fLoadFromDataSet)
			{
				this.bLoadFromDataSet = true;
				this.LoadTreeFromDataSet(this.DataSet);
				this.BindListeners();
			}
			this.ignoreDataSetEvents = false;
			this.ignoreXmlEvents = false;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0004BC5C File Offset: 0x0004B05C
		internal void Bind(DataRow r, XmlBoundElement e)
		{
			r.Element = e;
			e.Row = r;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0004BC78 File Offset: 0x0004B078
		private void BindSpecialListeners()
		{
			this.dataSet.DataRowCreated += this.OnDataRowCreatedSpecial;
			this.fDataRowCreatedSpecial = true;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0004BCA4 File Offset: 0x0004B0A4
		private void UnBindSpecialListeners()
		{
			this.dataSet.DataRowCreated -= this.OnDataRowCreatedSpecial;
			this.fDataRowCreatedSpecial = false;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0004BCD0 File Offset: 0x0004B0D0
		private void BindListeners()
		{
			this.BindToDocument();
			this.BindToDataSet();
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0004BCEC File Offset: 0x0004B0EC
		private void BindToDataSet()
		{
			if (this.fBoundToDataSet)
			{
				return;
			}
			if (this.fDataRowCreatedSpecial)
			{
				this.UnBindSpecialListeners();
			}
			this.dataSet.Tables.CollectionChanging += this.OnDataSetTablesChanging;
			this.dataSet.Relations.CollectionChanging += this.OnDataSetRelationsChanging;
			this.dataSet.DataRowCreated += this.OnDataRowCreated;
			this.dataSet.PropertyChanging += this.OnDataSetPropertyChanging;
			this.dataSet.ClearFunctionCalled += this.OnClearCalled;
			if (this.dataSet.Tables.Count > 0)
			{
				foreach (object obj in this.dataSet.Tables)
				{
					DataTable t = (DataTable)obj;
					this.BindToTable(t);
				}
			}
			foreach (object obj2 in this.dataSet.Relations)
			{
				DataRelation dataRelation = (DataRelation)obj2;
				dataRelation.PropertyChanging += this.OnRelationPropertyChanging;
			}
			this.fBoundToDataSet = true;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0004BE6C File Offset: 0x0004B26C
		private void BindToDocument()
		{
			if (!this.fBoundToDocument)
			{
				base.NodeInserting += this.OnNodeInserting;
				base.NodeInserted += this.OnNodeInserted;
				base.NodeRemoving += this.OnNodeRemoving;
				base.NodeRemoved += this.OnNodeRemoved;
				base.NodeChanging += this.OnNodeChanging;
				base.NodeChanged += this.OnNodeChanged;
				this.fBoundToDocument = true;
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0004BEF4 File Offset: 0x0004B2F4
		private void BindToTable(DataTable t)
		{
			t.ColumnChanged += this.OnColumnChanged;
			t.RowChanging += this.OnRowChanging;
			t.RowChanged += this.OnRowChanged;
			t.RowDeleting += this.OnRowChanging;
			t.RowDeleted += this.OnRowChanged;
			t.PropertyChanging += this.OnTablePropertyChanging;
			t.Columns.CollectionChanging += this.OnTableColumnsChanging;
			foreach (object obj in t.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				dataColumn.PropertyChanging += this.OnColumnPropertyChanging;
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0004BFE8 File Offset: 0x0004B3E8
		public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
		{
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			if (namespaceURI == null)
			{
				namespaceURI = string.Empty;
			}
			if (!this.fAssociateDataRow)
			{
				return new XmlBoundElement(prefix, localName, namespaceURI, this);
			}
			this.EnsurePopulatedMode();
			DataTable dataTable = this.mapper.SearchMatchingTableSchema(localName, namespaceURI);
			if (dataTable != null)
			{
				DataRow dataRow = dataTable.CreateEmptyRow();
				foreach (object obj in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					if (dataColumn.ColumnMapping != MappingType.Hidden)
					{
						XmlDataDocument.SetRowValueToNull(dataRow, dataColumn);
					}
				}
				XmlBoundElement element = dataRow.Element;
				element.Prefix = prefix;
				return element;
			}
			return new XmlBoundElement(prefix, localName, namespaceURI, this);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0004C0BC File Offset: 0x0004B4BC
		public override XmlEntityReference CreateEntityReference(string name)
		{
			throw new NotSupportedException(Res.GetString("DataDom_NotSupport_EntRef"));
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0004C0D8 File Offset: 0x0004B4D8
		public DataSet DataSet
		{
			get
			{
				return this.dataSet;
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0004C0EC File Offset: 0x0004B4EC
		private void DefoliateRegion(XmlBoundElement rowElem)
		{
			if (!this.optimizeStorage)
			{
				return;
			}
			if (rowElem.ElementState != ElementState.WeakFoliation)
			{
				return;
			}
			if (!this.mapper.IsRegionRadical(rowElem))
			{
				return;
			}
			bool flag = this.IgnoreXmlEvents;
			this.IgnoreXmlEvents = true;
			rowElem.ElementState = ElementState.Defoliating;
			try
			{
				rowElem.RemoveAllAttributes();
				XmlNode nextSibling;
				for (XmlNode xmlNode = rowElem.FirstChild; xmlNode != null; xmlNode = nextSibling)
				{
					nextSibling = xmlNode.NextSibling;
					XmlBoundElement xmlBoundElement = xmlNode as XmlBoundElement;
					if (xmlBoundElement != null && xmlBoundElement.Row != null)
					{
						break;
					}
					rowElem.RemoveChild(xmlNode);
				}
				rowElem.ElementState = ElementState.Defoliated;
			}
			finally
			{
				this.IgnoreXmlEvents = flag;
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0004C194 File Offset: 0x0004B594
		private XmlElement EnsureDocumentElement()
		{
			XmlElement xmlElement = base.DocumentElement;
			if (xmlElement == null)
			{
				string text = XmlConvert.EncodeLocalName(this.DataSet.DataSetName);
				if (text == null || text.Length == 0)
				{
					text = "Xml";
				}
				string text2 = this.DataSet.Namespace;
				if (text2 == null)
				{
					text2 = string.Empty;
				}
				xmlElement = new XmlBoundElement(string.Empty, text, text2, this);
				this.AppendChild(xmlElement);
			}
			return xmlElement;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0004C1FC File Offset: 0x0004B5FC
		private XmlElement EnsureNonRowDocumentElement()
		{
			XmlElement documentElement = base.DocumentElement;
			if (documentElement == null)
			{
				return this.EnsureDocumentElement();
			}
			if (this.GetRowFromElement(documentElement) == null)
			{
				return documentElement;
			}
			return this.DemoteDocumentElement();
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0004C230 File Offset: 0x0004B630
		private XmlElement DemoteDocumentElement()
		{
			XmlElement documentElement = base.DocumentElement;
			this.RemoveChild(documentElement);
			XmlElement xmlElement = this.EnsureDocumentElement();
			xmlElement.AppendChild(documentElement);
			return xmlElement;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0004C25C File Offset: 0x0004B65C
		private void EnsurePopulatedMode()
		{
			if (this.fDataRowCreatedSpecial)
			{
				this.UnBindSpecialListeners();
				this.mapper.SetupMapping(this, this.dataSet);
				this.BindListeners();
				this.fAssociateDataRow = true;
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0004C298 File Offset: 0x0004B698
		private void FixNestedChildren(DataRow row, XmlElement rowElement)
		{
			foreach (object obj in this.GetNestedChildRelations(row))
			{
				DataRelation relation = (DataRelation)obj;
				foreach (DataRow dataRow in row.GetChildRows(relation))
				{
					XmlElement element = dataRow.Element;
					if (element != null && element.ParentNode != rowElement)
					{
						element.ParentNode.RemoveChild(element);
						rowElement.AppendChild(element);
					}
				}
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0004C340 File Offset: 0x0004B740
		internal void Foliate(XmlBoundElement node, ElementState newState)
		{
			if (this.IsFoliationEnabled)
			{
				if (node.ElementState == ElementState.Defoliated)
				{
					this.ForceFoliation(node, newState);
					return;
				}
				if (node.ElementState == ElementState.WeakFoliation && newState == ElementState.StrongFoliation)
				{
					node.ElementState = newState;
				}
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0004C37C File Offset: 0x0004B77C
		private void Foliate(XmlElement element)
		{
			if (element is XmlBoundElement)
			{
				((XmlBoundElement)element).Foliate(ElementState.WeakFoliation);
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0004C3A0 File Offset: 0x0004B7A0
		private void FoliateIfDataPointers(DataRow row, XmlElement rowElement)
		{
			if (!this.IsFoliated(rowElement) && this.HasPointers(rowElement))
			{
				bool flag = this.IsFoliationEnabled;
				this.IsFoliationEnabled = true;
				try
				{
					this.Foliate(rowElement);
				}
				finally
				{
					this.IsFoliationEnabled = flag;
				}
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0004C3FC File Offset: 0x0004B7FC
		private void EnsureFoliation(XmlBoundElement rowElem, ElementState foliation)
		{
			if (rowElem.IsFoliated)
			{
				return;
			}
			this.ForceFoliation(rowElem, foliation);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0004C41C File Offset: 0x0004B81C
		private void ForceFoliation(XmlBoundElement node, ElementState newState)
		{
			object obj = this.foliationLock;
			lock (obj)
			{
				if (node.ElementState == ElementState.Defoliated)
				{
					node.ElementState = ElementState.Foliating;
					bool flag2 = this.IgnoreXmlEvents;
					this.IgnoreXmlEvents = true;
					try
					{
						XmlNode xmlNode = null;
						DataRow row = node.Row;
						DataRowVersion version = (row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current;
						foreach (object obj2 in row.Table.Columns)
						{
							DataColumn dataColumn = (DataColumn)obj2;
							if (!this.IsNotMapped(dataColumn))
							{
								object value = row[dataColumn, version];
								if (!Convert.IsDBNull(value))
								{
									if (dataColumn.ColumnMapping == MappingType.Attribute)
									{
										node.SetAttribute(dataColumn.EncodedColumnName, dataColumn.Namespace, dataColumn.ConvertObjectToXml(value));
									}
									else if (dataColumn.ColumnMapping == MappingType.Element)
									{
										XmlNode xmlNode2 = new XmlBoundElement(string.Empty, dataColumn.EncodedColumnName, dataColumn.Namespace, this);
										xmlNode2.AppendChild(this.CreateTextNode(dataColumn.ConvertObjectToXml(value)));
										if (xmlNode != null)
										{
											node.InsertAfter(xmlNode2, xmlNode);
										}
										else if (node.FirstChild != null)
										{
											node.InsertBefore(xmlNode2, node.FirstChild);
										}
										else
										{
											node.AppendChild(xmlNode2);
										}
										xmlNode = xmlNode2;
									}
									else
									{
										XmlNode xmlNode2 = this.CreateTextNode(dataColumn.ConvertObjectToXml(value));
										if (node.FirstChild != null)
										{
											node.InsertBefore(xmlNode2, node.FirstChild);
										}
										else
										{
											node.AppendChild(xmlNode2);
										}
										if (xmlNode == null)
										{
											xmlNode = xmlNode2;
										}
									}
								}
								else if (dataColumn.ColumnMapping == MappingType.SimpleContent)
								{
									XmlAttribute xmlAttribute = this.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
									xmlAttribute.Value = "true";
									node.SetAttributeNode(xmlAttribute);
									this.bHasXSINIL = true;
								}
							}
						}
					}
					finally
					{
						this.IgnoreXmlEvents = flag2;
						node.ElementState = newState;
					}
					this.OnFoliated(node);
				}
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0004C664 File Offset: 0x0004BA64
		private XmlNode GetColumnInsertAfterLocation(DataRow row, DataColumn col, XmlBoundElement rowElement)
		{
			XmlNode result = null;
			if (this.IsTextOnly(col))
			{
				return null;
			}
			for (XmlNode xmlNode = rowElement.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (!XmlDataDocument.IsTextLikeNode(xmlNode))
				{
					IL_81:
					while (xmlNode != null && xmlNode.NodeType == XmlNodeType.Element)
					{
						XmlElement e = xmlNode as XmlElement;
						if (this.mapper.GetRowFromElement(e) != null)
						{
							break;
						}
						object columnSchemaForNode = this.mapper.GetColumnSchemaForNode(rowElement, xmlNode);
						if (columnSchemaForNode == null || !(columnSchemaForNode is DataColumn) || ((DataColumn)columnSchemaForNode).Ordinal > col.Ordinal)
						{
							break;
						}
						result = xmlNode;
						xmlNode = xmlNode.NextSibling;
					}
					return result;
				}
				result = xmlNode;
			}
			goto IL_81;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0004C6F8 File Offset: 0x0004BAF8
		private ArrayList GetNestedChildRelations(DataRow row)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in row.Table.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (dataRelation.Nested)
				{
					arrayList.Add(dataRelation);
				}
			}
			return arrayList;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0004C774 File Offset: 0x0004BB74
		private DataRow GetNestedParent(DataRow row)
		{
			DataRelation nestedParentRelation = XmlDataDocument.GetNestedParentRelation(row);
			if (nestedParentRelation != null)
			{
				return row.GetParentRow(nestedParentRelation);
			}
			return null;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0004C794 File Offset: 0x0004BB94
		private static DataRelation GetNestedParentRelation(DataRow row)
		{
			DataRelation[] nestedParentRelations = row.Table.NestedParentRelations;
			if (nestedParentRelations.Length == 0)
			{
				return null;
			}
			return nestedParentRelations[0];
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0004C7B8 File Offset: 0x0004BBB8
		private DataColumn GetTextOnlyColumn(DataRow row)
		{
			return row.Table.XmlText;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0004C7D0 File Offset: 0x0004BBD0
		public DataRow GetRowFromElement(XmlElement e)
		{
			return this.mapper.GetRowFromElement(e);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0004C7EC File Offset: 0x0004BBEC
		private XmlNode GetRowInsertBeforeLocation(DataRow row, XmlElement rowElement, XmlNode parentElement)
		{
			DataRow dataRow = row;
			int i = 0;
			while (i < row.Table.Rows.Count && row != row.Table.Rows[i])
			{
				i++;
			}
			int num = i;
			DataRow nestedParent = this.GetNestedParent(row);
			for (i = num + 1; i < row.Table.Rows.Count; i++)
			{
				dataRow = row.Table.Rows[i];
				if (this.GetNestedParent(dataRow) == nestedParent && this.GetElementFromRow(dataRow).ParentNode == parentElement)
				{
					break;
				}
			}
			if (i < row.Table.Rows.Count)
			{
				return this.GetElementFromRow(dataRow);
			}
			return null;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0004C89C File Offset: 0x0004BC9C
		public XmlElement GetElementFromRow(DataRow r)
		{
			return r.Element;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0004C8B4 File Offset: 0x0004BCB4
		internal bool HasPointers(XmlNode node)
		{
			bool result;
			for (;;)
			{
				try
				{
					if (this.pointers.Count > 0)
					{
						foreach (object obj in this.pointers)
						{
							object value = ((DictionaryEntry)obj).Value;
							if (((IXmlDataVirtualNode)value).IsOnNode(node))
							{
								return true;
							}
						}
					}
					result = false;
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableExceptionType(e))
					{
						throw;
					}
					continue;
				}
				break;
			}
			return result;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0004C96C File Offset: 0x0004BD6C
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x0004C980 File Offset: 0x0004BD80
		internal bool IgnoreXmlEvents
		{
			get
			{
				return this.ignoreXmlEvents;
			}
			set
			{
				this.ignoreXmlEvents = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0004C994 File Offset: 0x0004BD94
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x0004C9A8 File Offset: 0x0004BDA8
		internal bool IgnoreDataSetEvents
		{
			get
			{
				return this.ignoreDataSetEvents;
			}
			set
			{
				this.ignoreDataSetEvents = value;
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0004C9BC File Offset: 0x0004BDBC
		private bool IsFoliated(XmlElement element)
		{
			return !(element is XmlBoundElement) || ((XmlBoundElement)element).IsFoliated;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0004C9E0 File Offset: 0x0004BDE0
		private bool IsFoliated(XmlBoundElement be)
		{
			return be.IsFoliated;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0004C9F4 File Offset: 0x0004BDF4
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x0004CA08 File Offset: 0x0004BE08
		internal bool IsFoliationEnabled
		{
			get
			{
				return this.isFoliationEnabled;
			}
			set
			{
				this.isFoliationEnabled = value;
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0004CA1C File Offset: 0x0004BE1C
		internal XmlNode CloneTree(DataPointer other)
		{
			this.EnsurePopulatedMode();
			bool flag = this.ignoreDataSetEvents;
			bool flag2 = this.ignoreXmlEvents;
			bool flag3 = this.IsFoliationEnabled;
			bool flag4 = this.fAssociateDataRow;
			XmlNode xmlNode;
			try
			{
				this.ignoreDataSetEvents = true;
				this.ignoreXmlEvents = true;
				this.IsFoliationEnabled = false;
				this.fAssociateDataRow = false;
				xmlNode = this.CloneTreeInternal(other);
				this.LoadRows(null, xmlNode);
				this.SyncRows(null, xmlNode, false);
			}
			finally
			{
				this.ignoreDataSetEvents = flag;
				this.ignoreXmlEvents = flag2;
				this.IsFoliationEnabled = flag3;
				this.fAssociateDataRow = flag4;
			}
			return xmlNode;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0004CAC0 File Offset: 0x0004BEC0
		private XmlNode CloneTreeInternal(DataPointer other)
		{
			XmlNode xmlNode = this.CloneNode(other);
			DataPointer dataPointer = new DataPointer(other);
			try
			{
				dataPointer.AddPointer();
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					int attributeCount = dataPointer.AttributeCount;
					for (int i = 0; i < attributeCount; i++)
					{
						dataPointer.MoveToOwnerElement();
						if (dataPointer.MoveToAttribute(i))
						{
							xmlNode.Attributes.Append((XmlAttribute)this.CloneTreeInternal(dataPointer));
						}
					}
					dataPointer.MoveTo(other);
				}
				bool flag = dataPointer.MoveToFirstChild();
				while (flag)
				{
					xmlNode.AppendChild(this.CloneTreeInternal(dataPointer));
					flag = dataPointer.MoveToNextSibling();
				}
			}
			finally
			{
				dataPointer.SetNoLongerUse();
			}
			return xmlNode;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0004CB78 File Offset: 0x0004BF78
		public override XmlNode CloneNode(bool deep)
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)base.CloneNode(false);
			xmlDataDocument.Init(this.DataSet.Clone());
			xmlDataDocument.dataSet.EnforceConstraints = this.dataSet.EnforceConstraints;
			if (deep)
			{
				DataPointer dataPointer = new DataPointer(this, this);
				try
				{
					dataPointer.AddPointer();
					bool flag = dataPointer.MoveToFirstChild();
					while (flag)
					{
						XmlNode newChild;
						if (dataPointer.NodeType == XmlNodeType.Element)
						{
							newChild = xmlDataDocument.CloneTree(dataPointer);
						}
						else
						{
							newChild = xmlDataDocument.CloneNode(dataPointer);
						}
						xmlDataDocument.AppendChild(newChild);
						flag = dataPointer.MoveToNextSibling();
					}
				}
				finally
				{
					dataPointer.SetNoLongerUse();
				}
			}
			return xmlDataDocument;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0004CC28 File Offset: 0x0004C028
		private XmlNode CloneNode(DataPointer dp)
		{
			switch (dp.NodeType)
			{
			case XmlNodeType.Element:
				return this.CreateElement(dp.Prefix, dp.LocalName, dp.NamespaceURI);
			case XmlNodeType.Attribute:
				return this.CreateAttribute(dp.Prefix, dp.LocalName, dp.NamespaceURI);
			case XmlNodeType.Text:
				return this.CreateTextNode(dp.Value);
			case XmlNodeType.CDATA:
				return this.CreateCDataSection(dp.Value);
			case XmlNodeType.EntityReference:
				return this.CreateEntityReference(dp.Name);
			case XmlNodeType.ProcessingInstruction:
				return this.CreateProcessingInstruction(dp.Name, dp.Value);
			case XmlNodeType.Comment:
				return this.CreateComment(dp.Value);
			case XmlNodeType.DocumentType:
				return this.CreateDocumentType(dp.Name, dp.PublicId, dp.SystemId, dp.InternalSubset);
			case XmlNodeType.DocumentFragment:
				return this.CreateDocumentFragment();
			case XmlNodeType.Whitespace:
				return this.CreateWhitespace(dp.Value);
			case XmlNodeType.SignificantWhitespace:
				return this.CreateSignificantWhitespace(dp.Value);
			case XmlNodeType.XmlDeclaration:
				return this.CreateXmlDeclaration(dp.Version, dp.Encoding, dp.Standalone);
			}
			throw new InvalidOperationException(Res.GetString("DataDom_CloneNode", new object[]
			{
				dp.NodeType.ToString()
			}));
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0004CD8C File Offset: 0x0004C18C
		internal static bool IsTextLikeNode(XmlNode n)
		{
			XmlNodeType nodeType = n.NodeType;
			if (nodeType - XmlNodeType.Text > 1)
			{
				if (nodeType == XmlNodeType.EntityReference)
				{
					return false;
				}
				if (nodeType - XmlNodeType.Whitespace > 1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0004CDB8 File Offset: 0x0004C1B8
		internal bool IsNotMapped(DataColumn c)
		{
			return DataSetMapper.IsNotMapped(c);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0004CDCC File Offset: 0x0004C1CC
		private bool IsSame(DataColumn c, int recNo1, int recNo2)
		{
			return c.Compare(recNo1, recNo2) == 0;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0004CDE8 File Offset: 0x0004C1E8
		internal bool IsTextOnly(DataColumn c)
		{
			return c.ColumnMapping == MappingType.SimpleContent;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0004CE00 File Offset: 0x0004C200
		public override void Load(string filename)
		{
			this.bForceExpandEntity = true;
			base.Load(filename);
			this.bForceExpandEntity = false;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0004CE24 File Offset: 0x0004C224
		public override void Load(Stream inStream)
		{
			this.bForceExpandEntity = true;
			base.Load(inStream);
			this.bForceExpandEntity = false;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0004CE48 File Offset: 0x0004C248
		public override void Load(TextReader txtReader)
		{
			this.bForceExpandEntity = true;
			base.Load(txtReader);
			this.bForceExpandEntity = false;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0004CE6C File Offset: 0x0004C26C
		public override void Load(XmlReader reader)
		{
			if (this.FirstChild != null)
			{
				throw new InvalidOperationException(Res.GetString("DataDom_MultipleLoad"));
			}
			try
			{
				this.ignoreXmlEvents = true;
				if (this.fDataRowCreatedSpecial)
				{
					this.UnBindSpecialListeners();
				}
				this.fAssociateDataRow = false;
				this.isFoliationEnabled = false;
				if (this.bForceExpandEntity)
				{
					((XmlTextReader)reader).EntityHandling = EntityHandling.ExpandEntities;
				}
				base.Load(reader);
				this.BindForLoad();
			}
			finally
			{
				this.ignoreXmlEvents = false;
				this.isFoliationEnabled = true;
				this.autoFoliationState = ElementState.StrongFoliation;
				this.fAssociateDataRow = true;
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0004CF14 File Offset: 0x0004C314
		private void LoadDataSetFromTree()
		{
			this.ignoreDataSetEvents = true;
			this.ignoreXmlEvents = true;
			bool flag = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			bool enforceConstraints = this.dataSet.EnforceConstraints;
			this.dataSet.EnforceConstraints = false;
			try
			{
				this.LoadRows(null, base.DocumentElement);
				this.SyncRows(null, base.DocumentElement, true);
				this.dataSet.EnforceConstraints = enforceConstraints;
			}
			finally
			{
				this.ignoreDataSetEvents = false;
				this.ignoreXmlEvents = false;
				this.IsFoliationEnabled = flag;
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0004CFB0 File Offset: 0x0004C3B0
		private void LoadTreeFromDataSet(DataSet ds)
		{
			this.ignoreDataSetEvents = true;
			this.ignoreXmlEvents = true;
			bool flag = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			this.fAssociateDataRow = false;
			DataTable[] array = this.OrderTables(ds);
			try
			{
				foreach (DataTable dataTable in array)
				{
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						XmlBoundElement xmlBoundElement = this.AttachBoundElementToDataRow(dataRow);
						DataRowState rowState = dataRow.RowState;
						switch (rowState)
						{
						case DataRowState.Detached:
						case DataRowState.Detached | DataRowState.Unchanged:
							continue;
						case DataRowState.Unchanged:
						case DataRowState.Added:
							break;
						default:
							if (rowState == DataRowState.Deleted || rowState != DataRowState.Modified)
							{
								continue;
							}
							break;
						}
						this.OnAddRow(dataRow);
					}
				}
			}
			finally
			{
				this.ignoreDataSetEvents = false;
				this.ignoreXmlEvents = false;
				this.IsFoliationEnabled = flag;
				this.fAssociateDataRow = true;
			}
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0004D0CC File Offset: 0x0004C4CC
		private void LoadRows(XmlBoundElement rowElem, XmlNode node)
		{
			XmlBoundElement xmlBoundElement = node as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				DataTable dataTable = this.mapper.SearchMatchingTableSchema(rowElem, xmlBoundElement);
				if (dataTable != null)
				{
					DataRow r = this.GetRowFromElement(xmlBoundElement);
					if (xmlBoundElement.ElementState == ElementState.None)
					{
						xmlBoundElement.ElementState = ElementState.WeakFoliation;
					}
					r = dataTable.CreateEmptyRow();
					this.Bind(r, xmlBoundElement);
					rowElem = xmlBoundElement;
				}
			}
			for (XmlNode xmlNode = node.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				this.LoadRows(rowElem, xmlNode);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0004D138 File Offset: 0x0004C538
		internal DataSetMapper Mapper
		{
			get
			{
				return this.mapper;
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0004D14C File Offset: 0x0004C54C
		internal void OnDataRowCreated(object oDataSet, DataRow row)
		{
			this.OnNewRow(row);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0004D160 File Offset: 0x0004C560
		internal void OnClearCalled(object oDataSet, DataTable table)
		{
			throw new NotSupportedException(Res.GetString("DataDom_NotSupport_Clear"));
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0004D17C File Offset: 0x0004C57C
		internal void OnDataRowCreatedSpecial(object oDataSet, DataRow row)
		{
			this.Bind(true);
			this.OnNewRow(row);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0004D198 File Offset: 0x0004C598
		internal void OnNewRow(DataRow row)
		{
			this.AttachBoundElementToDataRow(row);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0004D1B0 File Offset: 0x0004C5B0
		private XmlBoundElement AttachBoundElementToDataRow(DataRow row)
		{
			DataTable table = row.Table;
			XmlBoundElement xmlBoundElement = new XmlBoundElement(string.Empty, table.EncodedTableName, table.Namespace, this);
			xmlBoundElement.IsEmpty = false;
			this.Bind(row, xmlBoundElement);
			xmlBoundElement.ElementState = ElementState.Defoliated;
			return xmlBoundElement;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0004D1F4 File Offset: 0x0004C5F4
		private bool NeedXSI_NilAttr(DataRow row)
		{
			DataTable table = row.Table;
			if (table.xmlText == null)
			{
				return false;
			}
			object value = row[table.xmlText];
			return Convert.IsDBNull(value);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0004D228 File Offset: 0x0004C628
		private void OnAddRow(DataRow row)
		{
			XmlBoundElement xmlBoundElement = (XmlBoundElement)this.GetElementFromRow(row);
			if (this.NeedXSI_NilAttr(row) && !xmlBoundElement.IsFoliated)
			{
				this.ForceFoliation(xmlBoundElement, this.AutoFoliationState);
			}
			DataRow rowFromElement = this.GetRowFromElement(base.DocumentElement);
			if (rowFromElement != null && this.GetNestedParent(row) == null)
			{
				this.DemoteDocumentElement();
			}
			this.EnsureDocumentElement().AppendChild(xmlBoundElement);
			this.FixNestedChildren(row, xmlBoundElement);
			this.OnNestedParentChange(row, xmlBoundElement, null);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0004D2A0 File Offset: 0x0004C6A0
		private void OnColumnValueChanged(DataRow row, DataColumn col, XmlBoundElement rowElement)
		{
			if (!this.IsNotMapped(col))
			{
				object value = row[col];
				if (col.ColumnMapping == MappingType.SimpleContent && Convert.IsDBNull(value) && !rowElement.IsFoliated)
				{
					this.ForceFoliation(rowElement, ElementState.WeakFoliation);
				}
				else if (!this.IsFoliated(rowElement))
				{
					goto IL_310;
				}
				if (this.IsTextOnly(col))
				{
					if (Convert.IsDBNull(value))
					{
						value = string.Empty;
						XmlAttribute xmlAttribute = rowElement.GetAttributeNode("xsi:nil");
						if (xmlAttribute == null)
						{
							xmlAttribute = this.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
							xmlAttribute.Value = "true";
							rowElement.SetAttributeNode(xmlAttribute);
							this.bHasXSINIL = true;
						}
						else
						{
							xmlAttribute.Value = "true";
						}
					}
					else
					{
						XmlAttribute attributeNode = rowElement.GetAttributeNode("xsi:nil");
						if (attributeNode != null)
						{
							attributeNode.Value = "false";
						}
					}
					this.ReplaceInitialChildText(rowElement, col.ConvertObjectToXml(value));
				}
				else
				{
					bool flag = false;
					if (col.ColumnMapping == MappingType.Attribute)
					{
						foreach (object obj in rowElement.Attributes)
						{
							XmlAttribute xmlAttribute2 = (XmlAttribute)obj;
							if (xmlAttribute2.LocalName == col.EncodedColumnName && xmlAttribute2.NamespaceURI == col.Namespace)
							{
								if (Convert.IsDBNull(value))
								{
									xmlAttribute2.OwnerElement.Attributes.Remove(xmlAttribute2);
								}
								else
								{
									xmlAttribute2.Value = col.ConvertObjectToXml(value);
								}
								flag = true;
								break;
							}
						}
						if (!flag && !Convert.IsDBNull(value))
						{
							rowElement.SetAttribute(col.EncodedColumnName, col.Namespace, col.ConvertObjectToXml(value));
						}
					}
					else
					{
						RegionIterator regionIterator = new RegionIterator(rowElement);
						bool flag2 = regionIterator.Next();
						while (flag2)
						{
							if (regionIterator.CurrentNode.NodeType == XmlNodeType.Element)
							{
								XmlElement xmlElement = (XmlElement)regionIterator.CurrentNode;
								XmlBoundElement xmlBoundElement = xmlElement as XmlBoundElement;
								if (xmlBoundElement != null && xmlBoundElement.Row != null)
								{
									flag2 = regionIterator.NextRight();
									continue;
								}
								if (xmlElement.LocalName == col.EncodedColumnName && xmlElement.NamespaceURI == col.Namespace)
								{
									flag = true;
									if (Convert.IsDBNull(value))
									{
										this.PromoteNonValueChildren(xmlElement);
										flag2 = regionIterator.NextRight();
										xmlElement.ParentNode.RemoveChild(xmlElement);
										continue;
									}
									this.ReplaceInitialChildText(xmlElement, col.ConvertObjectToXml(value));
									XmlAttribute attributeNode2 = xmlElement.GetAttributeNode("xsi:nil");
									if (attributeNode2 != null)
									{
										attributeNode2.Value = "false";
										goto IL_310;
									}
									goto IL_310;
								}
							}
							flag2 = regionIterator.Next();
						}
						if (!flag && !Convert.IsDBNull(value))
						{
							XmlElement xmlElement2 = new XmlBoundElement(string.Empty, col.EncodedColumnName, col.Namespace, this);
							xmlElement2.AppendChild(this.CreateTextNode(col.ConvertObjectToXml(value)));
							XmlNode columnInsertAfterLocation = this.GetColumnInsertAfterLocation(row, col, rowElement);
							if (columnInsertAfterLocation != null)
							{
								rowElement.InsertAfter(xmlElement2, columnInsertAfterLocation);
							}
							else if (rowElement.FirstChild != null)
							{
								rowElement.InsertBefore(xmlElement2, rowElement.FirstChild);
							}
							else
							{
								rowElement.AppendChild(xmlElement2);
							}
						}
					}
				}
			}
			IL_310:
			DataRelation nestedParentRelation = XmlDataDocument.GetNestedParentRelation(row);
			if (nestedParentRelation != null && nestedParentRelation.ChildKey.ContainsColumn(col))
			{
				this.OnNestedParentChange(row, rowElement, col);
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0004D604 File Offset: 0x0004CA04
		private void OnColumnChanged(object sender, DataColumnChangeEventArgs args)
		{
			if (this.ignoreDataSetEvents)
			{
				return;
			}
			bool flag = this.ignoreXmlEvents;
			this.ignoreXmlEvents = true;
			bool flag2 = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			try
			{
				DataRow row = args.Row;
				DataColumn column = args.Column;
				object proposedValue = args.ProposedValue;
				if (row.RowState == DataRowState.Detached)
				{
					XmlBoundElement element = row.Element;
					if (element.IsFoliated)
					{
						this.OnColumnValueChanged(row, column, element);
					}
				}
			}
			finally
			{
				this.IsFoliationEnabled = flag2;
				this.ignoreXmlEvents = flag;
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0004D69C File Offset: 0x0004CA9C
		private void OnColumnValuesChanged(DataRow row, XmlBoundElement rowElement)
		{
			if (this.columnChangeList.Count > 0)
			{
				if (((DataColumn)this.columnChangeList[0]).Table == row.Table)
				{
					using (IEnumerator enumerator = this.columnChangeList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							DataColumn col = (DataColumn)obj;
							this.OnColumnValueChanged(row, col, rowElement);
						}
						goto IL_FC;
					}
				}
				using (IEnumerator enumerator2 = row.Table.Columns.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						object obj2 = enumerator2.Current;
						DataColumn col2 = (DataColumn)obj2;
						this.OnColumnValueChanged(row, col2, rowElement);
					}
					goto IL_FC;
				}
			}
			foreach (object obj3 in row.Table.Columns)
			{
				DataColumn col3 = (DataColumn)obj3;
				this.OnColumnValueChanged(row, col3, rowElement);
			}
			IL_FC:
			this.columnChangeList.Clear();
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0004D7FC File Offset: 0x0004CBFC
		private void OnDeleteRow(DataRow row, XmlBoundElement rowElement)
		{
			if (rowElement == base.DocumentElement)
			{
				this.DemoteDocumentElement();
			}
			this.PromoteInnerRegions(rowElement);
			rowElement.ParentNode.RemoveChild(rowElement);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0004D830 File Offset: 0x0004CC30
		private void OnDeletingRow(DataRow row, XmlBoundElement rowElement)
		{
			if (this.IsFoliated(rowElement))
			{
				return;
			}
			bool flag = this.IgnoreXmlEvents;
			this.IgnoreXmlEvents = true;
			bool flag2 = this.IsFoliationEnabled;
			this.IsFoliationEnabled = true;
			try
			{
				this.Foliate(rowElement);
			}
			finally
			{
				this.IsFoliationEnabled = flag2;
				this.IgnoreXmlEvents = flag;
			}
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0004D898 File Offset: 0x0004CC98
		private void OnFoliated(XmlNode node)
		{
			for (;;)
			{
				try
				{
					if (this.pointers.Count > 0)
					{
						foreach (object obj in this.pointers)
						{
							object value = ((DictionaryEntry)obj).Value;
							((IXmlDataVirtualNode)value).OnFoliated(node);
						}
					}
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableExceptionType(e))
					{
						throw;
					}
					continue;
				}
				break;
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0004D944 File Offset: 0x0004CD44
		private DataColumn FindAssociatedParentColumn(DataRelation relation, DataColumn childCol)
		{
			DataColumn[] columnsReference = relation.ChildKey.ColumnsReference;
			for (int i = 0; i < columnsReference.Length; i++)
			{
				if (childCol == columnsReference[i])
				{
					return relation.ParentKey.ColumnsReference[i];
				}
			}
			return null;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0004D988 File Offset: 0x0004CD88
		private void OnNestedParentChange(DataRow child, XmlBoundElement childElement, DataColumn childCol)
		{
			DataRow dataRow;
			if (childElement == base.DocumentElement || childElement.ParentNode == null)
			{
				dataRow = null;
			}
			else
			{
				dataRow = this.GetRowFromElement((XmlElement)childElement.ParentNode);
			}
			DataRow nestedParent = this.GetNestedParent(child);
			if (dataRow != nestedParent)
			{
				if (nestedParent != null)
				{
					XmlElement elementFromRow = this.GetElementFromRow(nestedParent);
					elementFromRow.AppendChild(childElement);
					return;
				}
				DataRelation nestedParentRelation = XmlDataDocument.GetNestedParentRelation(child);
				if (childCol == null || nestedParentRelation == null || Convert.IsDBNull(child[childCol]))
				{
					this.EnsureNonRowDocumentElement().AppendChild(childElement);
					return;
				}
				DataColumn dataColumn = this.FindAssociatedParentColumn(nestedParentRelation, childCol);
				object value = dataColumn.ConvertValue(child[childCol]);
				if (dataRow.tempRecord != -1 && dataColumn.CompareValueTo(dataRow.tempRecord, value) != 0)
				{
					this.EnsureNonRowDocumentElement().AppendChild(childElement);
				}
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0004DA48 File Offset: 0x0004CE48
		private void OnNodeChanged(object sender, XmlNodeChangedEventArgs args)
		{
			if (this.ignoreXmlEvents)
			{
				return;
			}
			bool flag = this.ignoreDataSetEvents;
			bool flag2 = this.ignoreXmlEvents;
			bool flag3 = this.IsFoliationEnabled;
			this.ignoreDataSetEvents = true;
			this.ignoreXmlEvents = true;
			this.IsFoliationEnabled = false;
			bool fEnableCascading = this.DataSet.fEnableCascading;
			this.DataSet.fEnableCascading = false;
			try
			{
				XmlBoundElement rowElement = null;
				if (this.mapper.GetRegion(args.Node, out rowElement))
				{
					this.SynchronizeRowFromRowElement(rowElement);
				}
			}
			finally
			{
				this.ignoreDataSetEvents = flag;
				this.ignoreXmlEvents = flag2;
				this.IsFoliationEnabled = flag3;
				this.DataSet.fEnableCascading = fEnableCascading;
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0004DB00 File Offset: 0x0004CF00
		private void OnNodeChanging(object sender, XmlNodeChangedEventArgs args)
		{
			if (this.ignoreXmlEvents)
			{
				return;
			}
			if (this.DataSet.EnforceConstraints)
			{
				throw new InvalidOperationException(Res.GetString("DataDom_EnforceConstraintsShouldBeOff"));
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0004DB34 File Offset: 0x0004CF34
		private void OnNodeInserted(object sender, XmlNodeChangedEventArgs args)
		{
			if (this.ignoreXmlEvents)
			{
				return;
			}
			bool flag = this.ignoreDataSetEvents;
			bool flag2 = this.ignoreXmlEvents;
			bool flag3 = this.IsFoliationEnabled;
			this.ignoreDataSetEvents = true;
			this.ignoreXmlEvents = true;
			this.IsFoliationEnabled = false;
			bool fEnableCascading = this.DataSet.fEnableCascading;
			this.DataSet.fEnableCascading = false;
			try
			{
				XmlNode node = args.Node;
				XmlNode oldParent = args.OldParent;
				XmlNode newParent = args.NewParent;
				if (this.IsConnected(newParent))
				{
					this.OnNodeInsertedInTree(node);
				}
				else
				{
					this.OnNodeInsertedInFragment(node);
				}
			}
			finally
			{
				this.ignoreDataSetEvents = flag;
				this.ignoreXmlEvents = flag2;
				this.IsFoliationEnabled = flag3;
				this.DataSet.fEnableCascading = fEnableCascading;
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0004DC00 File Offset: 0x0004D000
		private void OnNodeInserting(object sender, XmlNodeChangedEventArgs args)
		{
			if (this.ignoreXmlEvents)
			{
				return;
			}
			if (this.DataSet.EnforceConstraints)
			{
				throw new InvalidOperationException(Res.GetString("DataDom_EnforceConstraintsShouldBeOff"));
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0004DC34 File Offset: 0x0004D034
		private void OnNodeRemoved(object sender, XmlNodeChangedEventArgs args)
		{
			if (this.ignoreXmlEvents)
			{
				return;
			}
			bool flag = this.ignoreDataSetEvents;
			bool flag2 = this.ignoreXmlEvents;
			bool flag3 = this.IsFoliationEnabled;
			this.ignoreDataSetEvents = true;
			this.ignoreXmlEvents = true;
			this.IsFoliationEnabled = false;
			bool fEnableCascading = this.DataSet.fEnableCascading;
			this.DataSet.fEnableCascading = false;
			try
			{
				XmlNode node = args.Node;
				XmlNode oldParent = args.OldParent;
				if (this.IsConnected(oldParent))
				{
					this.OnNodeRemovedFromTree(node, oldParent);
				}
				else
				{
					this.OnNodeRemovedFromFragment(node, oldParent);
				}
			}
			finally
			{
				this.ignoreDataSetEvents = flag;
				this.ignoreXmlEvents = flag2;
				this.IsFoliationEnabled = flag3;
				this.DataSet.fEnableCascading = fEnableCascading;
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0004DCFC File Offset: 0x0004D0FC
		private void OnNodeRemoving(object sender, XmlNodeChangedEventArgs args)
		{
			if (this.ignoreXmlEvents)
			{
				return;
			}
			if (this.DataSet.EnforceConstraints)
			{
				throw new InvalidOperationException(Res.GetString("DataDom_EnforceConstraintsShouldBeOff"));
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0004DD30 File Offset: 0x0004D130
		private void OnNodeRemovedFromTree(XmlNode node, XmlNode oldParent)
		{
			XmlBoundElement rowElement;
			if (this.mapper.GetRegion(oldParent, out rowElement))
			{
				this.SynchronizeRowFromRowElement(rowElement);
			}
			XmlBoundElement xmlBoundElement = node as XmlBoundElement;
			if (xmlBoundElement != null && xmlBoundElement.Row != null)
			{
				this.EnsureDisconnectedDataRow(xmlBoundElement);
			}
			TreeIterator treeIterator = new TreeIterator(node);
			bool flag = treeIterator.NextRowElement();
			while (flag)
			{
				xmlBoundElement = (XmlBoundElement)treeIterator.CurrentNode;
				this.EnsureDisconnectedDataRow(xmlBoundElement);
				flag = treeIterator.NextRowElement();
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0004DD9C File Offset: 0x0004D19C
		private void OnNodeRemovedFromFragment(XmlNode node, XmlNode oldParent)
		{
			XmlBoundElement xmlBoundElement;
			if (this.mapper.GetRegion(oldParent, out xmlBoundElement))
			{
				DataRow row = xmlBoundElement.Row;
				if (xmlBoundElement.Row.RowState == DataRowState.Detached)
				{
					this.SynchronizeRowFromRowElement(xmlBoundElement);
				}
			}
			XmlBoundElement xmlBoundElement2 = node as XmlBoundElement;
			if (xmlBoundElement2 != null && xmlBoundElement2.Row != null)
			{
				this.SetNestedParentRegion(xmlBoundElement2, null);
				return;
			}
			TreeIterator treeIterator = new TreeIterator(node);
			bool flag = treeIterator.NextRowElement();
			while (flag)
			{
				XmlBoundElement childRowElem = (XmlBoundElement)treeIterator.CurrentNode;
				this.SetNestedParentRegion(childRowElem, null);
				flag = treeIterator.NextRightRowElement();
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0004DE24 File Offset: 0x0004D224
		private void OnRowChanged(object sender, DataRowChangeEventArgs args)
		{
			if (this.ignoreDataSetEvents)
			{
				return;
			}
			this.ignoreXmlEvents = true;
			bool flag = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			try
			{
				DataRow row = args.Row;
				XmlBoundElement element = row.Element;
				DataRowAction action = args.Action;
				switch (action)
				{
				case DataRowAction.Delete:
					this.OnDeleteRow(row, element);
					break;
				case DataRowAction.Change:
					this.OnColumnValuesChanged(row, element);
					break;
				case DataRowAction.Delete | DataRowAction.Change:
					break;
				case DataRowAction.Rollback:
				{
					DataRowState dataRowState = this.rollbackState;
					if (dataRowState != DataRowState.Added)
					{
						if (dataRowState != DataRowState.Deleted)
						{
							if (dataRowState == DataRowState.Modified)
							{
								this.OnColumnValuesChanged(row, element);
							}
						}
						else
						{
							this.OnUndeleteRow(row, element);
							this.UpdateAllColumns(row, element);
						}
					}
					else
					{
						element.ParentNode.RemoveChild(element);
					}
					break;
				}
				default:
					if (action != DataRowAction.Commit)
					{
						if (action == DataRowAction.Add)
						{
							this.OnAddRow(row);
						}
					}
					else if (row.RowState == DataRowState.Detached)
					{
						element.RemoveAll();
					}
					break;
				}
			}
			finally
			{
				this.IsFoliationEnabled = flag;
				this.ignoreXmlEvents = false;
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0004DF24 File Offset: 0x0004D324
		private void OnRowChanging(object sender, DataRowChangeEventArgs args)
		{
			DataRow row = args.Row;
			if (args.Action == DataRowAction.Delete && row.Element != null)
			{
				this.OnDeletingRow(row, row.Element);
				return;
			}
			if (this.ignoreDataSetEvents)
			{
				return;
			}
			bool flag = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			try
			{
				this.ignoreXmlEvents = true;
				XmlElement elementFromRow = this.GetElementFromRow(row);
				if (elementFromRow != null)
				{
					DataRowAction action = args.Action;
					int recordFromVersion;
					int recordFromVersion2;
					switch (action)
					{
					case DataRowAction.Delete:
					case DataRowAction.Delete | DataRowAction.Change:
						goto IL_20D;
					case DataRowAction.Change:
						break;
					case DataRowAction.Rollback:
					{
						this.rollbackState = row.RowState;
						DataRowState dataRowState = this.rollbackState;
						if (dataRowState <= DataRowState.Added)
						{
							if (dataRowState != DataRowState.Detached && dataRowState != DataRowState.Added)
							{
								return;
							}
							goto IL_20D;
						}
						else
						{
							if (dataRowState == DataRowState.Deleted)
							{
								goto IL_20D;
							}
							if (dataRowState != DataRowState.Modified)
							{
								return;
							}
							this.columnChangeList.Clear();
							recordFromVersion = row.GetRecordFromVersion(DataRowVersion.Original);
							recordFromVersion2 = row.GetRecordFromVersion(DataRowVersion.Current);
							using (IEnumerator enumerator = row.Table.Columns.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									DataColumn dataColumn = (DataColumn)obj;
									if (!this.IsSame(dataColumn, recordFromVersion, recordFromVersion2))
									{
										this.columnChangeList.Add(dataColumn);
									}
								}
								return;
							}
						}
						break;
					}
					default:
						if (action != DataRowAction.Commit && action != DataRowAction.Add)
						{
							goto IL_20D;
						}
						goto IL_20D;
					}
					this.columnChangeList.Clear();
					recordFromVersion = row.GetRecordFromVersion(DataRowVersion.Proposed);
					recordFromVersion2 = row.GetRecordFromVersion(DataRowVersion.Current);
					foreach (object obj2 in row.Table.Columns)
					{
						DataColumn dataColumn2 = (DataColumn)obj2;
						object value = row[dataColumn2, DataRowVersion.Proposed];
						object value2 = row[dataColumn2, DataRowVersion.Current];
						if (Convert.IsDBNull(value) && !Convert.IsDBNull(value2) && dataColumn2.ColumnMapping != MappingType.Hidden)
						{
							this.FoliateIfDataPointers(row, elementFromRow);
						}
						if (!this.IsSame(dataColumn2, recordFromVersion, recordFromVersion2))
						{
							this.columnChangeList.Add(dataColumn2);
						}
					}
				}
				IL_20D:;
			}
			finally
			{
				this.ignoreXmlEvents = false;
				this.IsFoliationEnabled = flag;
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0004E19C File Offset: 0x0004D59C
		private void OnDataSetPropertyChanging(object oDataSet, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "DataSetName")
			{
				throw new InvalidOperationException(Res.GetString("DataDom_DataSetNameChange"));
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0004E1CC File Offset: 0x0004D5CC
		private void OnColumnPropertyChanging(object oColumn, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "ColumnName")
			{
				throw new InvalidOperationException(Res.GetString("DataDom_ColumnNameChange"));
			}
			if (args.PropertyName == "Namespace")
			{
				throw new InvalidOperationException(Res.GetString("DataDom_ColumnNamespaceChange"));
			}
			if (args.PropertyName == "ColumnMapping")
			{
				throw new InvalidOperationException(Res.GetString("DataDom_ColumnMappingChange"));
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0004E240 File Offset: 0x0004D640
		private void OnTablePropertyChanging(object oTable, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "TableName")
			{
				throw new InvalidOperationException(Res.GetString("DataDom_TableNameChange"));
			}
			if (args.PropertyName == "Namespace")
			{
				throw new InvalidOperationException(Res.GetString("DataDom_TableNamespaceChange"));
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0004E294 File Offset: 0x0004D694
		private void OnTableColumnsChanging(object oColumnsCollection, CollectionChangeEventArgs args)
		{
			throw new InvalidOperationException(Res.GetString("DataDom_TableColumnsChange"));
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0004E2B0 File Offset: 0x0004D6B0
		private void OnDataSetTablesChanging(object oTablesCollection, CollectionChangeEventArgs args)
		{
			throw new InvalidOperationException(Res.GetString("DataDom_DataSetTablesChange"));
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0004E2CC File Offset: 0x0004D6CC
		private void OnDataSetRelationsChanging(object oRelationsCollection, CollectionChangeEventArgs args)
		{
			DataRelation dataRelation = (DataRelation)args.Element;
			if (dataRelation != null && dataRelation.Nested)
			{
				throw new InvalidOperationException(Res.GetString("DataDom_DataSetNestedRelationsChange"));
			}
			if (args.Action == CollectionChangeAction.Refresh)
			{
				foreach (object obj in ((DataRelationCollection)oRelationsCollection))
				{
					DataRelation dataRelation2 = (DataRelation)obj;
					if (dataRelation2.Nested)
					{
						throw new InvalidOperationException(Res.GetString("DataDom_DataSetNestedRelationsChange"));
					}
				}
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0004E374 File Offset: 0x0004D774
		private void OnRelationPropertyChanging(object oRelationsCollection, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "Nested")
			{
				throw new InvalidOperationException(Res.GetString("DataDom_DataSetNestedRelationsChange"));
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0004E3A4 File Offset: 0x0004D7A4
		private void OnUndeleteRow(DataRow row, XmlElement rowElement)
		{
			if (rowElement.ParentNode != null)
			{
				rowElement.ParentNode.RemoveChild(rowElement);
			}
			DataRow nestedParent = this.GetNestedParent(row);
			XmlElement xmlElement;
			if (nestedParent == null)
			{
				xmlElement = this.EnsureNonRowDocumentElement();
			}
			else
			{
				xmlElement = this.GetElementFromRow(nestedParent);
			}
			XmlNode rowInsertBeforeLocation;
			if ((rowInsertBeforeLocation = this.GetRowInsertBeforeLocation(row, rowElement, xmlElement)) != null)
			{
				xmlElement.InsertBefore(rowElement, rowInsertBeforeLocation);
			}
			else
			{
				xmlElement.AppendChild(rowElement);
			}
			this.FixNestedChildren(row, rowElement);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0004E40C File Offset: 0x0004D80C
		private void PromoteChild(XmlNode child, XmlNode prevSibling)
		{
			if (child.ParentNode != null)
			{
				child.ParentNode.RemoveChild(child);
			}
			prevSibling.ParentNode.InsertAfter(child, prevSibling);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0004E43C File Offset: 0x0004D83C
		private void PromoteInnerRegions(XmlNode parent)
		{
			XmlBoundElement parentRowElem;
			this.mapper.GetRegion(parent.ParentNode, out parentRowElem);
			TreeIterator treeIterator = new TreeIterator(parent);
			bool flag = treeIterator.NextRowElement();
			while (flag)
			{
				XmlBoundElement xmlBoundElement = (XmlBoundElement)treeIterator.CurrentNode;
				flag = treeIterator.NextRightRowElement();
				this.PromoteChild(xmlBoundElement, parent);
				this.SetNestedParentRegion(xmlBoundElement, parentRowElem);
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0004E498 File Offset: 0x0004D898
		private void PromoteNonValueChildren(XmlNode parent)
		{
			XmlNode prevSibling = parent;
			XmlNode xmlNode = parent.FirstChild;
			bool flag = true;
			while (xmlNode != null)
			{
				XmlNode nextSibling = xmlNode.NextSibling;
				if (!flag || !XmlDataDocument.IsTextLikeNode(xmlNode))
				{
					flag = false;
					nextSibling = xmlNode.NextSibling;
					this.PromoteChild(xmlNode, prevSibling);
					prevSibling = xmlNode;
				}
				xmlNode = nextSibling;
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0004E4E0 File Offset: 0x0004D8E0
		private void RemoveInitialTextNodes(XmlNode node)
		{
			while (node != null && XmlDataDocument.IsTextLikeNode(node))
			{
				XmlNode nextSibling = node.NextSibling;
				node.ParentNode.RemoveChild(node);
				node = nextSibling;
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0004E514 File Offset: 0x0004D914
		private void ReplaceInitialChildText(XmlNode parent, string value)
		{
			XmlNode xmlNode = parent.FirstChild;
			while (xmlNode != null && xmlNode.NodeType == XmlNodeType.Whitespace)
			{
				xmlNode = xmlNode.NextSibling;
			}
			if (xmlNode != null)
			{
				if (xmlNode.NodeType == XmlNodeType.Text)
				{
					xmlNode.Value = value;
				}
				else
				{
					xmlNode = parent.InsertBefore(this.CreateTextNode(value), xmlNode);
				}
				this.RemoveInitialTextNodes(xmlNode.NextSibling);
				return;
			}
			parent.AppendChild(this.CreateTextNode(value));
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0004E580 File Offset: 0x0004D980
		internal XmlNode SafeFirstChild(XmlNode n)
		{
			XmlBoundElement xmlBoundElement = n as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				return xmlBoundElement.SafeFirstChild;
			}
			return n.FirstChild;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0004E5A4 File Offset: 0x0004D9A4
		internal XmlNode SafeNextSibling(XmlNode n)
		{
			XmlBoundElement xmlBoundElement = n as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				return xmlBoundElement.SafeNextSibling;
			}
			return n.NextSibling;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0004E5C8 File Offset: 0x0004D9C8
		internal XmlNode SafePreviousSibling(XmlNode n)
		{
			XmlBoundElement xmlBoundElement = n as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				return xmlBoundElement.SafePreviousSibling;
			}
			return n.PreviousSibling;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0004E5EC File Offset: 0x0004D9EC
		internal static void SetRowValueToNull(DataRow row, DataColumn col)
		{
			if (!row.IsNull(col))
			{
				row[col] = Convert.DBNull;
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0004E610 File Offset: 0x0004DA10
		internal static void SetRowValueFromXmlText(DataRow row, DataColumn col, string xmlText)
		{
			object obj;
			try
			{
				obj = col.ConvertXmlToObject(xmlText);
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				XmlDataDocument.SetRowValueToNull(row, col);
				return;
			}
			if (!obj.Equals(row[col]))
			{
				row[col] = obj;
			}
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0004E670 File Offset: 0x0004DA70
		private void SynchronizeRowFromRowElement(XmlBoundElement rowElement)
		{
			this.SynchronizeRowFromRowElement(rowElement, null);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0004E688 File Offset: 0x0004DA88
		private void SynchronizeRowFromRowElement(XmlBoundElement rowElement, ArrayList rowElemList)
		{
			DataRow row = rowElement.Row;
			if (row.RowState == DataRowState.Deleted)
			{
				return;
			}
			row.BeginEdit();
			this.SynchronizeRowFromRowElementEx(rowElement, rowElemList);
			row.EndEdit();
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0004E6BC File Offset: 0x0004DABC
		private void SynchronizeRowFromRowElementEx(XmlBoundElement rowElement, ArrayList rowElemList)
		{
			DataRow row = rowElement.Row;
			DataTable table = row.Table;
			Hashtable hashtable = new Hashtable();
			string a = string.Empty;
			RegionIterator regionIterator = new RegionIterator(rowElement);
			DataColumn textOnlyColumn = this.GetTextOnlyColumn(row);
			bool flag;
			if (textOnlyColumn != null)
			{
				hashtable[textOnlyColumn] = textOnlyColumn;
				string text;
				flag = regionIterator.NextInitialTextLikeNodes(out text);
				if (text.Length == 0 && ((a = rowElement.GetAttribute("xsi:nil")) == "1" || a == "true"))
				{
					row[textOnlyColumn] = Convert.DBNull;
				}
				else
				{
					XmlDataDocument.SetRowValueFromXmlText(row, textOnlyColumn, text);
				}
			}
			else
			{
				flag = regionIterator.Next();
			}
			while (flag)
			{
				XmlElement xmlElement = regionIterator.CurrentNode as XmlElement;
				if (xmlElement == null)
				{
					flag = regionIterator.Next();
				}
				else
				{
					XmlBoundElement xmlBoundElement = xmlElement as XmlBoundElement;
					if (xmlBoundElement != null && xmlBoundElement.Row != null)
					{
						if (rowElemList != null)
						{
							rowElemList.Add(xmlElement);
						}
						flag = regionIterator.NextRight();
					}
					else
					{
						DataColumn columnSchemaForNode = this.mapper.GetColumnSchemaForNode(rowElement, xmlElement);
						if (columnSchemaForNode != null && hashtable[columnSchemaForNode] == null)
						{
							hashtable[columnSchemaForNode] = columnSchemaForNode;
							string text2;
							flag = regionIterator.NextInitialTextLikeNodes(out text2);
							if (text2.Length == 0 && ((a = xmlElement.GetAttribute("xsi:nil")) == "1" || a == "true"))
							{
								row[columnSchemaForNode] = Convert.DBNull;
							}
							else
							{
								XmlDataDocument.SetRowValueFromXmlText(row, columnSchemaForNode, text2);
							}
						}
						else
						{
							flag = regionIterator.Next();
						}
					}
				}
			}
			foreach (object obj in rowElement.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				DataColumn columnSchemaForNode2 = this.mapper.GetColumnSchemaForNode(rowElement, xmlAttribute);
				if (columnSchemaForNode2 != null && hashtable[columnSchemaForNode2] == null)
				{
					hashtable[columnSchemaForNode2] = columnSchemaForNode2;
					XmlDataDocument.SetRowValueFromXmlText(row, columnSchemaForNode2, xmlAttribute.Value);
				}
			}
			foreach (object obj2 in row.Table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj2;
				if (hashtable[dataColumn] == null && !this.IsNotMapped(dataColumn))
				{
					if (!dataColumn.AutoIncrement)
					{
						XmlDataDocument.SetRowValueToNull(row, dataColumn);
					}
					else
					{
						dataColumn.Init(row.tempRecord);
					}
				}
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0004E960 File Offset: 0x0004DD60
		private void UpdateAllColumns(DataRow row, XmlBoundElement rowElement)
		{
			foreach (object obj in row.Table.Columns)
			{
				DataColumn col = (DataColumn)obj;
				this.OnColumnValueChanged(row, col, rowElement);
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0004E9CC File Offset: 0x0004DDCC
		public XmlDataDocument() : base(new XmlDataImplementation())
		{
			this.Init();
			this.AttachDataSet(new DataSet());
			this.dataSet.EnforceConstraints = false;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0004EA04 File Offset: 0x0004DE04
		public XmlDataDocument(DataSet dataset) : base(new XmlDataImplementation())
		{
			this.Init(dataset);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0004EA24 File Offset: 0x0004DE24
		internal XmlDataDocument(XmlImplementation imp) : base(imp)
		{
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0004EA38 File Offset: 0x0004DE38
		private void Init()
		{
			this.pointers = new Hashtable();
			this.countAddPointer = 0;
			this.columnChangeList = new ArrayList();
			this.ignoreDataSetEvents = false;
			this.isFoliationEnabled = true;
			this.optimizeStorage = true;
			this.fDataRowCreatedSpecial = false;
			this.autoFoliationState = ElementState.StrongFoliation;
			this.fAssociateDataRow = true;
			this.mapper = new DataSetMapper();
			this.foliationLock = new object();
			this.ignoreXmlEvents = true;
			this.attrXml = this.CreateAttribute("xmlns", "xml", XPathNodePointer.s_strReservedXmlns);
			this.attrXml.Value = XPathNodePointer.s_strReservedXml;
			this.ignoreXmlEvents = false;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0004EADC File Offset: 0x0004DEDC
		private void Init(DataSet ds)
		{
			if (ds == null)
			{
				throw new ArgumentException(Res.GetString("DataDom_DataSetNull"));
			}
			this.Init();
			if (ds.FBoundToDocument)
			{
				throw new ArgumentException(Res.GetString("DataDom_MultipleDataSet"));
			}
			ds.FBoundToDocument = true;
			this.dataSet = ds;
			this.Bind(true);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0004EB30 File Offset: 0x0004DF30
		private bool IsConnected(XmlNode node)
		{
			while (node != null)
			{
				if (node == this)
				{
					return true;
				}
				XmlAttribute xmlAttribute = node as XmlAttribute;
				if (xmlAttribute != null)
				{
					node = xmlAttribute.OwnerElement;
				}
				else
				{
					node = node.ParentNode;
				}
			}
			return false;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0004EB68 File Offset: 0x0004DF68
		private bool IsRowLive(DataRow row)
		{
			return (row.RowState & (DataRowState.Unchanged | DataRowState.Added | DataRowState.Modified)) > (DataRowState)0;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0004EB84 File Offset: 0x0004DF84
		private static void SetNestedParentRow(DataRow childRow, DataRow parentRow)
		{
			DataRelation nestedParentRelation = XmlDataDocument.GetNestedParentRelation(childRow);
			if (nestedParentRelation != null)
			{
				if (parentRow == null || nestedParentRelation.ParentKey.Table != parentRow.Table)
				{
					childRow.SetParentRow(null, nestedParentRelation);
					return;
				}
				childRow.SetParentRow(parentRow, nestedParentRelation);
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0004EBC8 File Offset: 0x0004DFC8
		private void OnNodeInsertedInTree(XmlNode node)
		{
			ArrayList arrayList = new ArrayList();
			XmlBoundElement xmlBoundElement;
			if (this.mapper.GetRegion(node, out xmlBoundElement))
			{
				if (xmlBoundElement == node)
				{
					this.OnRowElementInsertedInTree(xmlBoundElement, arrayList);
				}
				else
				{
					this.OnNonRowElementInsertedInTree(node, xmlBoundElement, arrayList);
				}
			}
			else
			{
				TreeIterator treeIterator = new TreeIterator(node);
				bool flag = treeIterator.NextRowElement();
				while (flag)
				{
					arrayList.Add(treeIterator.CurrentNode);
					flag = treeIterator.NextRightRowElement();
				}
			}
			while (arrayList.Count > 0)
			{
				XmlBoundElement rowElem = (XmlBoundElement)arrayList[0];
				arrayList.RemoveAt(0);
				this.OnRowElementInsertedInTree(rowElem, arrayList);
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0004EC54 File Offset: 0x0004E054
		private void OnNodeInsertedInFragment(XmlNode node)
		{
			XmlBoundElement xmlBoundElement;
			if (!this.mapper.GetRegion(node, out xmlBoundElement))
			{
				return;
			}
			if (xmlBoundElement == node)
			{
				this.SetNestedParentRegion(xmlBoundElement);
				return;
			}
			ArrayList arrayList = new ArrayList();
			this.OnNonRowElementInsertedInFragment(node, xmlBoundElement, arrayList);
			while (arrayList.Count > 0)
			{
				XmlBoundElement childRowElem = (XmlBoundElement)arrayList[0];
				arrayList.RemoveAt(0);
				this.SetNestedParentRegion(childRowElem, xmlBoundElement);
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0004ECB4 File Offset: 0x0004E0B4
		private void OnRowElementInsertedInTree(XmlBoundElement rowElem, ArrayList rowElemList)
		{
			DataRow row = rowElem.Row;
			DataRowState rowState = row.RowState;
			if (rowState != DataRowState.Detached)
			{
				if (rowState != DataRowState.Deleted)
				{
					return;
				}
				row.RejectChanges();
				this.SynchronizeRowFromRowElement(rowElem, rowElemList);
				this.SetNestedParentRegion(rowElem);
			}
			else
			{
				row.Table.Rows.Add(row);
				this.SetNestedParentRegion(rowElem);
				if (rowElemList != null)
				{
					RegionIterator regionIterator = new RegionIterator(rowElem);
					bool flag = regionIterator.NextRowElement();
					while (flag)
					{
						rowElemList.Add(regionIterator.CurrentNode);
						flag = regionIterator.NextRightRowElement();
					}
					return;
				}
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0004ED30 File Offset: 0x0004E130
		private void EnsureDisconnectedDataRow(XmlBoundElement rowElem)
		{
			DataRow row = rowElem.Row;
			DataRowState rowState = row.RowState;
			switch (rowState)
			{
			case DataRowState.Detached:
				this.SetNestedParentRegion(rowElem);
				return;
			case DataRowState.Unchanged:
				break;
			case DataRowState.Detached | DataRowState.Unchanged:
				return;
			case DataRowState.Added:
				this.EnsureFoliation(rowElem, ElementState.WeakFoliation);
				row.Delete();
				this.SetNestedParentRegion(rowElem);
				return;
			default:
				if (rowState == DataRowState.Deleted)
				{
					return;
				}
				if (rowState != DataRowState.Modified)
				{
					return;
				}
				break;
			}
			this.EnsureFoliation(rowElem, ElementState.WeakFoliation);
			row.Delete();
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0004ED9C File Offset: 0x0004E19C
		private void OnNonRowElementInsertedInTree(XmlNode node, XmlBoundElement rowElement, ArrayList rowElemList)
		{
			DataRow row = rowElement.Row;
			this.SynchronizeRowFromRowElement(rowElement);
			if (rowElemList != null)
			{
				TreeIterator treeIterator = new TreeIterator(node);
				bool flag = treeIterator.NextRowElement();
				while (flag)
				{
					rowElemList.Add(treeIterator.CurrentNode);
					flag = treeIterator.NextRightRowElement();
				}
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0004EDE4 File Offset: 0x0004E1E4
		private void OnNonRowElementInsertedInFragment(XmlNode node, XmlBoundElement rowElement, ArrayList rowElemList)
		{
			DataRow row = rowElement.Row;
			if (row.RowState == DataRowState.Detached)
			{
				this.SynchronizeRowFromRowElementEx(rowElement, rowElemList);
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0004EE0C File Offset: 0x0004E20C
		private void SetNestedParentRegion(XmlBoundElement childRowElem)
		{
			XmlBoundElement parentRowElem;
			this.mapper.GetRegion(childRowElem.ParentNode, out parentRowElem);
			this.SetNestedParentRegion(childRowElem, parentRowElem);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0004EE38 File Offset: 0x0004E238
		private void SetNestedParentRegion(XmlBoundElement childRowElem, XmlBoundElement parentRowElem)
		{
			DataRow row = childRowElem.Row;
			if (parentRowElem == null)
			{
				XmlDataDocument.SetNestedParentRow(row, null);
				return;
			}
			DataRow row2 = parentRowElem.Row;
			DataRelation[] nestedParentRelations = row.Table.NestedParentRelations;
			if (nestedParentRelations.Length != 0 && nestedParentRelations[0].ParentTable == row2.Table)
			{
				XmlDataDocument.SetNestedParentRow(row, row2);
				return;
			}
			XmlDataDocument.SetNestedParentRow(row, null);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0004EE90 File Offset: 0x0004E290
		internal static bool IsTextNode(XmlNodeType nt)
		{
			return nt - XmlNodeType.Text <= 1 || nt - XmlNodeType.Whitespace <= 1;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0004EEB0 File Offset: 0x0004E2B0
		protected override XPathNavigator CreateNavigator(XmlNode node)
		{
			if (XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[(int)node.NodeType] == -1)
			{
				return null;
			}
			if (XmlDataDocument.IsTextNode(node.NodeType))
			{
				XmlNode parentNode = node.ParentNode;
				if (parentNode != null && parentNode.NodeType == XmlNodeType.Attribute)
				{
					return null;
				}
				XmlNode xmlNode = node.PreviousSibling;
				while (xmlNode != null && XmlDataDocument.IsTextNode(xmlNode.NodeType))
				{
					node = xmlNode;
					xmlNode = this.SafePreviousSibling(node);
				}
			}
			return new DataDocumentXPathNavigator(this, node);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0004EF1C File Offset: 0x0004E31C
		[Conditional("DEBUG")]
		private void AssertLiveRows(XmlNode node)
		{
			bool flag = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			try
			{
				XmlBoundElement xmlBoundElement = node as XmlBoundElement;
				if (xmlBoundElement != null)
				{
					DataRow row = xmlBoundElement.Row;
				}
				TreeIterator treeIterator = new TreeIterator(node);
				bool flag2 = treeIterator.NextRowElement();
				while (flag2)
				{
					xmlBoundElement = (treeIterator.CurrentNode as XmlBoundElement);
					flag2 = treeIterator.NextRowElement();
				}
			}
			finally
			{
				this.IsFoliationEnabled = flag;
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0004EF94 File Offset: 0x0004E394
		[Conditional("DEBUG")]
		private void AssertNonLiveRows(XmlNode node)
		{
			bool flag = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			try
			{
				XmlBoundElement xmlBoundElement = node as XmlBoundElement;
				if (xmlBoundElement != null)
				{
					DataRow row = xmlBoundElement.Row;
				}
				TreeIterator treeIterator = new TreeIterator(node);
				bool flag2 = treeIterator.NextRowElement();
				while (flag2)
				{
					xmlBoundElement = (treeIterator.CurrentNode as XmlBoundElement);
					flag2 = treeIterator.NextRowElement();
				}
			}
			finally
			{
				this.IsFoliationEnabled = flag;
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0004F00C File Offset: 0x0004E40C
		public override XmlElement GetElementById(string elemId)
		{
			throw new NotSupportedException(Res.GetString("DataDom_NotSupport_GetElementById"));
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0004F028 File Offset: 0x0004E428
		public override XmlNodeList GetElementsByTagName(string name)
		{
			XmlNodeList elementsByTagName = base.GetElementsByTagName(name);
			int count = elementsByTagName.Count;
			return elementsByTagName;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0004F048 File Offset: 0x0004E448
		private DataTable[] OrderTables(DataSet ds)
		{
			DataTable[] array = null;
			if (ds == null || ds.Tables.Count == 0)
			{
				array = new DataTable[0];
			}
			else if (this.TablesAreOrdered(ds))
			{
				array = new DataTable[ds.Tables.Count];
				ds.Tables.CopyTo(array, 0);
			}
			if (array == null)
			{
				array = new DataTable[ds.Tables.Count];
				List<DataTable> list = new List<DataTable>();
				foreach (object obj in ds.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					if (dataTable.ParentRelations.Count == 0)
					{
						list.Add(dataTable);
					}
				}
				if (list.Count > 0)
				{
					foreach (object obj2 in ds.Tables)
					{
						DataTable dataTable2 = (DataTable)obj2;
						if (this.IsSelfRelatedDataTable(dataTable2))
						{
							list.Add(dataTable2);
						}
					}
					for (int i = 0; i < list.Count; i++)
					{
						foreach (object obj3 in list[i].ChildRelations)
						{
							DataRelation dataRelation = (DataRelation)obj3;
							DataTable childTable = dataRelation.ChildTable;
							if (!list.Contains(childTable))
							{
								list.Add(childTable);
							}
						}
					}
					list.CopyTo(array);
				}
				else
				{
					ds.Tables.CopyTo(array, 0);
				}
			}
			return array;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0004F22C File Offset: 0x0004E62C
		private bool IsSelfRelatedDataTable(DataTable rootTable)
		{
			List<DataTable> list = new List<DataTable>();
			bool flag = false;
			foreach (object obj in rootTable.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				DataTable childTable = dataRelation.ChildTable;
				if (childTable == rootTable)
				{
					flag = true;
					break;
				}
				if (!list.Contains(childTable))
				{
					list.Add(childTable);
				}
			}
			if (!flag)
			{
				for (int i = 0; i < list.Count; i++)
				{
					foreach (object obj2 in list[i].ChildRelations)
					{
						DataRelation dataRelation2 = (DataRelation)obj2;
						DataTable childTable2 = dataRelation2.ChildTable;
						if (childTable2 == rootTable)
						{
							flag = true;
							break;
						}
						if (!list.Contains(childTable2))
						{
							list.Add(childTable2);
						}
					}
					if (flag)
					{
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0004F354 File Offset: 0x0004E754
		private bool TablesAreOrdered(DataSet ds)
		{
			foreach (object obj in ds.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				if (dataTable.Namespace != ds.Namespace)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000287 RID: 647
		private DataSet dataSet;

		// Token: 0x04000288 RID: 648
		private DataSetMapper mapper;

		// Token: 0x04000289 RID: 649
		internal Hashtable pointers;

		// Token: 0x0400028A RID: 650
		private int countAddPointer;

		// Token: 0x0400028B RID: 651
		private ArrayList columnChangeList;

		// Token: 0x0400028C RID: 652
		private DataRowState rollbackState;

		// Token: 0x0400028D RID: 653
		private bool fBoundToDataSet;

		// Token: 0x0400028E RID: 654
		private bool fBoundToDocument;

		// Token: 0x0400028F RID: 655
		private bool fDataRowCreatedSpecial;

		// Token: 0x04000290 RID: 656
		private bool ignoreXmlEvents;

		// Token: 0x04000291 RID: 657
		private bool ignoreDataSetEvents;

		// Token: 0x04000292 RID: 658
		private bool isFoliationEnabled;

		// Token: 0x04000293 RID: 659
		private bool optimizeStorage;

		// Token: 0x04000294 RID: 660
		private ElementState autoFoliationState;

		// Token: 0x04000295 RID: 661
		private bool fAssociateDataRow;

		// Token: 0x04000296 RID: 662
		private object foliationLock;

		// Token: 0x04000297 RID: 663
		internal const string XSI_NIL = "xsi:nil";

		// Token: 0x04000298 RID: 664
		internal const string XSI = "xsi";

		// Token: 0x04000299 RID: 665
		private bool bForceExpandEntity;

		// Token: 0x0400029A RID: 666
		internal XmlAttribute attrXml;

		// Token: 0x0400029B RID: 667
		internal bool bLoadFromDataSet;

		// Token: 0x0400029C RID: 668
		internal bool bHasXSINIL;
	}
}
