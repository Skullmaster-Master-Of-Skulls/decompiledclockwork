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
	// Token: 0x0200038C RID: 908
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class XmlDataDocument : XmlDocument
	{
		// Token: 0x06003005 RID: 12293 RVA: 0x002D6F68 File Offset: 0x002D6368
		internal void AddPointer(IXmlDataVirtualNode pointer)
		{
			lock (this.pointers)
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

		// Token: 0x06003006 RID: 12294 RVA: 0x002D7078 File Offset: 0x002D6478
		[Conditional("DEBUG")]
		internal void AssertPointerPresent(IXmlDataVirtualNode pointer)
		{
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x002D7088 File Offset: 0x002D6488
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

		// Token: 0x06003008 RID: 12296 RVA: 0x002D70C8 File Offset: 0x002D64C8
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

		// Token: 0x06003009 RID: 12297 RVA: 0x002D7158 File Offset: 0x002D6558
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

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x0600300A RID: 12298 RVA: 0x002D71F8 File Offset: 0x002D65F8
		// (set) Token: 0x0600300B RID: 12299 RVA: 0x002D7218 File Offset: 0x002D6618
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

		// Token: 0x0600300C RID: 12300 RVA: 0x002D7238 File Offset: 0x002D6638
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

		// Token: 0x0600300D RID: 12301 RVA: 0x002D7288 File Offset: 0x002D6688
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

		// Token: 0x0600300E RID: 12302 RVA: 0x002D72F8 File Offset: 0x002D66F8
		internal void Bind(DataRow r, XmlBoundElement e)
		{
			r.Element = e;
			e.Row = r;
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x002D7318 File Offset: 0x002D6718
		private void BindSpecialListeners()
		{
			this.dataSet.DataRowCreated += this.OnDataRowCreatedSpecial;
			this.fDataRowCreatedSpecial = true;
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x002D7348 File Offset: 0x002D6748
		private void UnBindSpecialListeners()
		{
			this.dataSet.DataRowCreated -= this.OnDataRowCreatedSpecial;
			this.fDataRowCreatedSpecial = false;
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x002D7378 File Offset: 0x002D6778
		private void BindListeners()
		{
			this.BindToDocument();
			this.BindToDataSet();
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x002D7398 File Offset: 0x002D6798
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

		// Token: 0x06003013 RID: 12307 RVA: 0x002D7518 File Offset: 0x002D6918
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

		// Token: 0x06003014 RID: 12308 RVA: 0x002D75A8 File Offset: 0x002D69A8
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

		// Token: 0x06003015 RID: 12309 RVA: 0x002D76A8 File Offset: 0x002D6AA8
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

		// Token: 0x06003016 RID: 12310 RVA: 0x002D7788 File Offset: 0x002D6B88
		public override XmlEntityReference CreateEntityReference(string name)
		{
			throw new NotSupportedException(Res.GetString("DataDom_NotSupport_EntRef"));
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06003017 RID: 12311 RVA: 0x002D77A8 File Offset: 0x002D6BA8
		public DataSet DataSet
		{
			get
			{
				return this.dataSet;
			}
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x002D77C8 File Offset: 0x002D6BC8
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

		// Token: 0x06003019 RID: 12313 RVA: 0x002D7878 File Offset: 0x002D6C78
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

		// Token: 0x0600301A RID: 12314 RVA: 0x002D78E8 File Offset: 0x002D6CE8
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

		// Token: 0x0600301B RID: 12315 RVA: 0x002D7928 File Offset: 0x002D6D28
		private XmlElement DemoteDocumentElement()
		{
			XmlElement documentElement = base.DocumentElement;
			this.RemoveChild(documentElement);
			XmlElement xmlElement = this.EnsureDocumentElement();
			xmlElement.AppendChild(documentElement);
			return xmlElement;
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x002D7958 File Offset: 0x002D6D58
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

		// Token: 0x0600301D RID: 12317 RVA: 0x002D7998 File Offset: 0x002D6D98
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

		// Token: 0x0600301E RID: 12318 RVA: 0x002D7A48 File Offset: 0x002D6E48
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

		// Token: 0x0600301F RID: 12319 RVA: 0x002D7A88 File Offset: 0x002D6E88
		private void Foliate(XmlElement element)
		{
			if (element is XmlBoundElement)
			{
				((XmlBoundElement)element).Foliate(ElementState.WeakFoliation);
			}
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x002D7AB8 File Offset: 0x002D6EB8
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

		// Token: 0x06003021 RID: 12321 RVA: 0x002D7B18 File Offset: 0x002D6F18
		private void EnsureFoliation(XmlBoundElement rowElem, ElementState foliation)
		{
			if (rowElem.IsFoliated)
			{
				return;
			}
			this.ForceFoliation(rowElem, foliation);
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x002D7B38 File Offset: 0x002D6F38
		private void ForceFoliation(XmlBoundElement node, ElementState newState)
		{
			lock (this.foliationLock)
			{
				if (node.ElementState == ElementState.Defoliated)
				{
					node.ElementState = ElementState.Foliating;
					bool flag = this.IgnoreXmlEvents;
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
						this.IgnoreXmlEvents = flag;
						node.ElementState = newState;
					}
					this.OnFoliated(node);
				}
			}
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x002D7D78 File Offset: 0x002D7178
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

		// Token: 0x06003024 RID: 12324 RVA: 0x002D7E18 File Offset: 0x002D7218
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

		// Token: 0x06003025 RID: 12325 RVA: 0x002D7E98 File Offset: 0x002D7298
		private DataRow GetNestedParent(DataRow row)
		{
			DataRelation nestedParentRelation = XmlDataDocument.GetNestedParentRelation(row);
			if (nestedParentRelation != null)
			{
				return row.GetParentRow(nestedParentRelation);
			}
			return null;
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x002D7EB8 File Offset: 0x002D72B8
		private static DataRelation GetNestedParentRelation(DataRow row)
		{
			DataRelation[] nestedParentRelations = row.Table.NestedParentRelations;
			if (nestedParentRelations.Length == 0)
			{
				return null;
			}
			return nestedParentRelations[0];
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x002D7EE8 File Offset: 0x002D72E8
		private DataColumn GetTextOnlyColumn(DataRow row)
		{
			return row.Table.XmlText;
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x002D7F08 File Offset: 0x002D7308
		public DataRow GetRowFromElement(XmlElement e)
		{
			return this.mapper.GetRowFromElement(e);
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x002D7F28 File Offset: 0x002D7328
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

		// Token: 0x0600302A RID: 12330 RVA: 0x002D7FD8 File Offset: 0x002D73D8
		public XmlElement GetElementFromRow(DataRow r)
		{
			return r.Element;
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x002D7FF8 File Offset: 0x002D73F8
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

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x0600302C RID: 12332 RVA: 0x002D80B8 File Offset: 0x002D74B8
		// (set) Token: 0x0600302D RID: 12333 RVA: 0x002D80D8 File Offset: 0x002D74D8
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

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x0600302E RID: 12334 RVA: 0x002D80F8 File Offset: 0x002D74F8
		// (set) Token: 0x0600302F RID: 12335 RVA: 0x002D8118 File Offset: 0x002D7518
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

		// Token: 0x06003030 RID: 12336 RVA: 0x002D8138 File Offset: 0x002D7538
		private bool IsFoliated(XmlElement element)
		{
			return !(element is XmlBoundElement) || ((XmlBoundElement)element).IsFoliated;
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x002D8168 File Offset: 0x002D7568
		private bool IsFoliated(XmlBoundElement be)
		{
			return be.IsFoliated;
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06003032 RID: 12338 RVA: 0x002D8188 File Offset: 0x002D7588
		// (set) Token: 0x06003033 RID: 12339 RVA: 0x002D81A8 File Offset: 0x002D75A8
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

		// Token: 0x06003034 RID: 12340 RVA: 0x002D81C8 File Offset: 0x002D75C8
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

		// Token: 0x06003035 RID: 12341 RVA: 0x002D8278 File Offset: 0x002D7678
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

		// Token: 0x06003036 RID: 12342 RVA: 0x002D8338 File Offset: 0x002D7738
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

		// Token: 0x06003037 RID: 12343 RVA: 0x002D83E8 File Offset: 0x002D77E8
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

		// Token: 0x06003038 RID: 12344 RVA: 0x002D8558 File Offset: 0x002D7958
		internal static bool IsTextLikeNode(XmlNode n)
		{
			XmlNodeType nodeType = n.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				break;
			case XmlNodeType.EntityReference:
				return false;
			default:
				switch (nodeType)
				{
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					break;
				default:
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x002D8598 File Offset: 0x002D7998
		internal bool IsNotMapped(DataColumn c)
		{
			return DataSetMapper.IsNotMapped(c);
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x002D85B8 File Offset: 0x002D79B8
		private bool IsSame(DataColumn c, int recNo1, int recNo2)
		{
			return c.Compare(recNo1, recNo2) == 0;
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x002D85D8 File Offset: 0x002D79D8
		internal bool IsTextOnly(DataColumn c)
		{
			return c.ColumnMapping == MappingType.SimpleContent;
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x002D85F8 File Offset: 0x002D79F8
		public override void Load(string filename)
		{
			this.bForceExpandEntity = true;
			base.Load(filename);
			this.bForceExpandEntity = false;
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x002D8628 File Offset: 0x002D7A28
		public override void Load(Stream inStream)
		{
			this.bForceExpandEntity = true;
			base.Load(inStream);
			this.bForceExpandEntity = false;
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x002D8658 File Offset: 0x002D7A58
		public override void Load(TextReader txtReader)
		{
			this.bForceExpandEntity = true;
			base.Load(txtReader);
			this.bForceExpandEntity = false;
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x002D8688 File Offset: 0x002D7A88
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

		// Token: 0x06003040 RID: 12352 RVA: 0x002D8738 File Offset: 0x002D7B38
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

		// Token: 0x06003041 RID: 12353 RVA: 0x002D87D8 File Offset: 0x002D7BD8
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
						this.AttachBoundElementToDataRow(dataRow);
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

		// Token: 0x06003042 RID: 12354 RVA: 0x002D88F8 File Offset: 0x002D7CF8
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

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06003043 RID: 12355 RVA: 0x002D8968 File Offset: 0x002D7D68
		internal DataSetMapper Mapper
		{
			get
			{
				return this.mapper;
			}
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x002D8988 File Offset: 0x002D7D88
		internal void OnDataRowCreated(object oDataSet, DataRow row)
		{
			this.OnNewRow(row);
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x002D89A8 File Offset: 0x002D7DA8
		internal void OnClearCalled(object oDataSet, DataTable table)
		{
			throw new NotSupportedException(Res.GetString("DataDom_NotSupport_Clear"));
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x002D89C8 File Offset: 0x002D7DC8
		internal void OnDataRowCreatedSpecial(object oDataSet, DataRow row)
		{
			this.Bind(true);
			this.OnNewRow(row);
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x002D89E8 File Offset: 0x002D7DE8
		internal void OnNewRow(DataRow row)
		{
			this.AttachBoundElementToDataRow(row);
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x002D8A08 File Offset: 0x002D7E08
		private XmlBoundElement AttachBoundElementToDataRow(DataRow row)
		{
			DataTable table = row.Table;
			XmlBoundElement xmlBoundElement = new XmlBoundElement(string.Empty, table.EncodedTableName, table.Namespace, this);
			xmlBoundElement.IsEmpty = false;
			this.Bind(row, xmlBoundElement);
			xmlBoundElement.ElementState = ElementState.Defoliated;
			return xmlBoundElement;
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x002D8A58 File Offset: 0x002D7E58
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

		// Token: 0x0600304A RID: 12362 RVA: 0x002D8A98 File Offset: 0x002D7E98
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

		// Token: 0x0600304B RID: 12363 RVA: 0x002D8B18 File Offset: 0x002D7F18
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

		// Token: 0x0600304C RID: 12364 RVA: 0x002D8E88 File Offset: 0x002D8288
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

		// Token: 0x0600304D RID: 12365 RVA: 0x002D8F28 File Offset: 0x002D8328
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
						goto IL_102;
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
					goto IL_102;
				}
			}
			foreach (object obj3 in row.Table.Columns)
			{
				DataColumn col3 = (DataColumn)obj3;
				this.OnColumnValueChanged(row, col3, rowElement);
			}
			IL_102:
			this.columnChangeList.Clear();
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x002D9098 File Offset: 0x002D8498
		private void OnDeleteRow(DataRow row, XmlBoundElement rowElement)
		{
			if (rowElement == base.DocumentElement)
			{
				this.DemoteDocumentElement();
			}
			this.PromoteInnerRegions(rowElement);
			rowElement.ParentNode.RemoveChild(rowElement);
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x002D90D8 File Offset: 0x002D84D8
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

		// Token: 0x06003050 RID: 12368 RVA: 0x002D9148 File Offset: 0x002D8548
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

		// Token: 0x06003051 RID: 12369 RVA: 0x002D91F8 File Offset: 0x002D85F8
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

		// Token: 0x06003052 RID: 12370 RVA: 0x002D9248 File Offset: 0x002D8648
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

		// Token: 0x06003053 RID: 12371 RVA: 0x002D9308 File Offset: 0x002D8708
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

		// Token: 0x06003054 RID: 12372 RVA: 0x002D93C8 File Offset: 0x002D87C8
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

		// Token: 0x06003055 RID: 12373 RVA: 0x002D9408 File Offset: 0x002D8808
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

		// Token: 0x06003056 RID: 12374 RVA: 0x002D94D8 File Offset: 0x002D88D8
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

		// Token: 0x06003057 RID: 12375 RVA: 0x002D9518 File Offset: 0x002D8918
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

		// Token: 0x06003058 RID: 12376 RVA: 0x002D95E8 File Offset: 0x002D89E8
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

		// Token: 0x06003059 RID: 12377 RVA: 0x002D9628 File Offset: 0x002D8A28
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

		// Token: 0x0600305A RID: 12378 RVA: 0x002D9698 File Offset: 0x002D8A98
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

		// Token: 0x0600305B RID: 12379 RVA: 0x002D9728 File Offset: 0x002D8B28
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

		// Token: 0x0600305C RID: 12380 RVA: 0x002D9828 File Offset: 0x002D8C28
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
						goto IL_20C;
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
								goto IL_20C;
							}
							goto IL_20C;
						}
						else
						{
							if (dataRowState == DataRowState.Deleted || dataRowState != DataRowState.Modified)
							{
								goto IL_20C;
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
								goto IL_20C;
							}
						}
						break;
					}
					default:
						if (action != DataRowAction.Commit && action != DataRowAction.Add)
						{
							goto IL_20C;
						}
						goto IL_20C;
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
				IL_20C:;
			}
			finally
			{
				this.ignoreXmlEvents = false;
				this.IsFoliationEnabled = flag;
			}
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x002D9AA8 File Offset: 0x002D8EA8
		private void OnDataSetPropertyChanging(object oDataSet, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "DataSetName")
			{
				throw new InvalidOperationException(Res.GetString("DataDom_DataSetNameChange"));
			}
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x002D9AD8 File Offset: 0x002D8ED8
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

		// Token: 0x0600305F RID: 12383 RVA: 0x002D9B58 File Offset: 0x002D8F58
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

		// Token: 0x06003060 RID: 12384 RVA: 0x002D9BB8 File Offset: 0x002D8FB8
		private void OnTableColumnsChanging(object oColumnsCollection, CollectionChangeEventArgs args)
		{
			throw new InvalidOperationException(Res.GetString("DataDom_TableColumnsChange"));
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x002D9BD8 File Offset: 0x002D8FD8
		private void OnDataSetTablesChanging(object oTablesCollection, CollectionChangeEventArgs args)
		{
			throw new InvalidOperationException(Res.GetString("DataDom_DataSetTablesChange"));
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x002D9BF8 File Offset: 0x002D8FF8
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

		// Token: 0x06003063 RID: 12387 RVA: 0x002D9CA8 File Offset: 0x002D90A8
		private void OnRelationPropertyChanging(object oRelationsCollection, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "Nested")
			{
				throw new InvalidOperationException(Res.GetString("DataDom_DataSetNestedRelationsChange"));
			}
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x002D9CD8 File Offset: 0x002D90D8
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

		// Token: 0x06003065 RID: 12389 RVA: 0x002D9D48 File Offset: 0x002D9148
		private void PromoteChild(XmlNode child, XmlNode prevSibling)
		{
			if (child.ParentNode != null)
			{
				child.ParentNode.RemoveChild(child);
			}
			prevSibling.ParentNode.InsertAfter(child, prevSibling);
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x002D9D78 File Offset: 0x002D9178
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

		// Token: 0x06003067 RID: 12391 RVA: 0x002D9DD8 File Offset: 0x002D91D8
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

		// Token: 0x06003068 RID: 12392 RVA: 0x002D9E28 File Offset: 0x002D9228
		private void RemoveInitialTextNodes(XmlNode node)
		{
			while (node != null && XmlDataDocument.IsTextLikeNode(node))
			{
				XmlNode nextSibling = node.NextSibling;
				node.ParentNode.RemoveChild(node);
				node = nextSibling;
			}
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x002D9E68 File Offset: 0x002D9268
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

		// Token: 0x0600306A RID: 12394 RVA: 0x002D9ED8 File Offset: 0x002D92D8
		internal XmlNode SafeFirstChild(XmlNode n)
		{
			XmlBoundElement xmlBoundElement = n as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				return xmlBoundElement.SafeFirstChild;
			}
			return n.FirstChild;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x002D9F08 File Offset: 0x002D9308
		internal XmlNode SafeNextSibling(XmlNode n)
		{
			XmlBoundElement xmlBoundElement = n as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				return xmlBoundElement.SafeNextSibling;
			}
			return n.NextSibling;
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x002D9F38 File Offset: 0x002D9338
		internal XmlNode SafePreviousSibling(XmlNode n)
		{
			XmlBoundElement xmlBoundElement = n as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				return xmlBoundElement.SafePreviousSibling;
			}
			return n.PreviousSibling;
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x002D9F68 File Offset: 0x002D9368
		internal static void SetRowValueToNull(DataRow row, DataColumn col)
		{
			if (!row.IsNull(col))
			{
				row[col] = Convert.DBNull;
			}
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x002D9F98 File Offset: 0x002D9398
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

		// Token: 0x0600306F RID: 12399 RVA: 0x002D9FF8 File Offset: 0x002D93F8
		private void SynchronizeRowFromRowElement(XmlBoundElement rowElement)
		{
			this.SynchronizeRowFromRowElement(rowElement, null);
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x002DA018 File Offset: 0x002D9418
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

		// Token: 0x06003071 RID: 12401 RVA: 0x002DA058 File Offset: 0x002D9458
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

		// Token: 0x06003072 RID: 12402 RVA: 0x002DA308 File Offset: 0x002D9708
		private void UpdateAllColumns(DataRow row, XmlBoundElement rowElement)
		{
			foreach (object obj in row.Table.Columns)
			{
				DataColumn col = (DataColumn)obj;
				this.OnColumnValueChanged(row, col, rowElement);
			}
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x002DA378 File Offset: 0x002D9778
		public XmlDataDocument() : base(new XmlDataImplementation())
		{
			this.Init();
			this.AttachDataSet(new DataSet());
			this.dataSet.EnforceConstraints = false;
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x002DA3B8 File Offset: 0x002D97B8
		public XmlDataDocument(DataSet dataset) : base(new XmlDataImplementation())
		{
			this.Init(dataset);
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x002DA3D8 File Offset: 0x002D97D8
		internal XmlDataDocument(XmlImplementation imp) : base(imp)
		{
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x002DA3F8 File Offset: 0x002D97F8
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

		// Token: 0x06003077 RID: 12407 RVA: 0x002DA4A8 File Offset: 0x002D98A8
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

		// Token: 0x06003078 RID: 12408 RVA: 0x002DA508 File Offset: 0x002D9908
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

		// Token: 0x06003079 RID: 12409 RVA: 0x002DA548 File Offset: 0x002D9948
		private bool IsRowLive(DataRow row)
		{
			return (row.RowState & (DataRowState.Unchanged | DataRowState.Added | DataRowState.Modified)) != (DataRowState)0;
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x002DA568 File Offset: 0x002D9968
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

		// Token: 0x0600307B RID: 12411 RVA: 0x002DA5B8 File Offset: 0x002D99B8
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

		// Token: 0x0600307C RID: 12412 RVA: 0x002DA648 File Offset: 0x002D9A48
		private void OnNodeInsertedInFragment(XmlNode node)
		{
			XmlBoundElement xmlBoundElement;
			if (this.mapper.GetRegion(node, out xmlBoundElement))
			{
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
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x002DA6A8 File Offset: 0x002D9AA8
		private void OnRowElementInsertedInTree(XmlBoundElement rowElem, ArrayList rowElemList)
		{
			DataRow row = rowElem.Row;
			DataRowState rowState = row.RowState;
			DataRowState dataRowState = rowState;
			if (dataRowState != DataRowState.Detached)
			{
				if (dataRowState != DataRowState.Deleted)
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

		// Token: 0x0600307E RID: 12414 RVA: 0x002DA728 File Offset: 0x002D9B28
		private void EnsureDisconnectedDataRow(XmlBoundElement rowElem)
		{
			DataRow row = rowElem.Row;
			DataRowState rowState = row.RowState;
			DataRowState dataRowState = rowState;
			switch (dataRowState)
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
				if (dataRowState == DataRowState.Deleted)
				{
					return;
				}
				if (dataRowState != DataRowState.Modified)
				{
					return;
				}
				break;
			}
			this.EnsureFoliation(rowElem, ElementState.WeakFoliation);
			row.Delete();
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x002DA798 File Offset: 0x002D9B98
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

		// Token: 0x06003080 RID: 12416 RVA: 0x002DA7E8 File Offset: 0x002D9BE8
		private void OnNonRowElementInsertedInFragment(XmlNode node, XmlBoundElement rowElement, ArrayList rowElemList)
		{
			DataRow row = rowElement.Row;
			if (row.RowState == DataRowState.Detached)
			{
				this.SynchronizeRowFromRowElementEx(rowElement, rowElemList);
			}
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x002DA818 File Offset: 0x002D9C18
		private void SetNestedParentRegion(XmlBoundElement childRowElem)
		{
			XmlBoundElement parentRowElem;
			this.mapper.GetRegion(childRowElem.ParentNode, out parentRowElem);
			this.SetNestedParentRegion(childRowElem, parentRowElem);
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x002DA848 File Offset: 0x002D9C48
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

		// Token: 0x06003083 RID: 12419 RVA: 0x002DA8A8 File Offset: 0x002D9CA8
		internal static bool IsTextNode(XmlNodeType nt)
		{
			switch (nt)
			{
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				break;
			default:
				switch (nt)
				{
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					break;
				default:
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x002DA8E8 File Offset: 0x002D9CE8
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

		// Token: 0x06003085 RID: 12421 RVA: 0x002DA958 File Offset: 0x002D9D58
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

		// Token: 0x06003086 RID: 12422 RVA: 0x002DA9D8 File Offset: 0x002D9DD8
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

		// Token: 0x06003087 RID: 12423 RVA: 0x002DAA58 File Offset: 0x002D9E58
		public override XmlElement GetElementById(string elemId)
		{
			throw new NotSupportedException(Res.GetString("DataDom_NotSupport_GetElementById"));
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x002DAA78 File Offset: 0x002D9E78
		public override XmlNodeList GetElementsByTagName(string name)
		{
			XmlNodeList elementsByTagName = base.GetElementsByTagName(name);
			int count = elementsByTagName.Count;
			return elementsByTagName;
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x002DAA98 File Offset: 0x002D9E98
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

		// Token: 0x0600308A RID: 12426 RVA: 0x002DAC88 File Offset: 0x002DA088
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

		// Token: 0x0600308B RID: 12427 RVA: 0x002DADB8 File Offset: 0x002DA1B8
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

		// Token: 0x04001DB4 RID: 7604
		internal const string XSI_NIL = "xsi:nil";

		// Token: 0x04001DB5 RID: 7605
		internal const string XSI = "xsi";

		// Token: 0x04001DB6 RID: 7606
		private DataSet dataSet;

		// Token: 0x04001DB7 RID: 7607
		private DataSetMapper mapper;

		// Token: 0x04001DB8 RID: 7608
		internal Hashtable pointers;

		// Token: 0x04001DB9 RID: 7609
		private int countAddPointer;

		// Token: 0x04001DBA RID: 7610
		private ArrayList columnChangeList;

		// Token: 0x04001DBB RID: 7611
		private DataRowState rollbackState;

		// Token: 0x04001DBC RID: 7612
		private bool fBoundToDataSet;

		// Token: 0x04001DBD RID: 7613
		private bool fBoundToDocument;

		// Token: 0x04001DBE RID: 7614
		private bool fDataRowCreatedSpecial;

		// Token: 0x04001DBF RID: 7615
		private bool ignoreXmlEvents;

		// Token: 0x04001DC0 RID: 7616
		private bool ignoreDataSetEvents;

		// Token: 0x04001DC1 RID: 7617
		private bool isFoliationEnabled;

		// Token: 0x04001DC2 RID: 7618
		private bool optimizeStorage;

		// Token: 0x04001DC3 RID: 7619
		private ElementState autoFoliationState;

		// Token: 0x04001DC4 RID: 7620
		private bool fAssociateDataRow;

		// Token: 0x04001DC5 RID: 7621
		private object foliationLock;

		// Token: 0x04001DC6 RID: 7622
		private bool bForceExpandEntity;

		// Token: 0x04001DC7 RID: 7623
		internal XmlAttribute attrXml;

		// Token: 0x04001DC8 RID: 7624
		internal bool bLoadFromDataSet;

		// Token: 0x04001DC9 RID: 7625
		internal bool bHasXSINIL;
	}
}
