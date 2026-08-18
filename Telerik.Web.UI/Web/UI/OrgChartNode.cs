using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Telerik.Web.UI.OrgChart.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000BEB RID: 3051
	[XmlRoot("Node")]
	public class OrgChartNode : IOrgChartNodeContainer, IItem, IOrgChartRendererContainer, IXmlSerializable, IDisposable
	{
		// Token: 0x06007445 RID: 29765 RVA: 0x001B2037 File Offset: 0x001B0237
		public OrgChartNode()
		{
		}

		// Token: 0x06007446 RID: 29766 RVA: 0x001B2051 File Offset: 0x001B0251
		internal OrgChartNode(RadOrgChart orgChart)
		{
			this.OrgChart = orgChart;
		}

		// Token: 0x06007447 RID: 29767 RVA: 0x001B2074 File Offset: 0x001B0274
		public string GetHierarchicalIndex()
		{
			OrgChartNode orgChartNode = this.Parent as OrgChartNode;
			RadOrgChart radOrgChart = null;
			OrgChartNode item = this;
			List<string> list = new List<string>();
			while (orgChartNode != null)
			{
				list.Add(orgChartNode.Nodes.IndexOf(item).ToString());
				item = orgChartNode;
				IOrgChartNodeContainer parent = orgChartNode.Parent;
				orgChartNode = (parent as OrgChartNode);
				if (orgChartNode == null)
				{
					radOrgChart = (parent as RadOrgChart);
				}
			}
			if (this.Parent is RadOrgChart)
			{
				radOrgChart = (this.Parent as RadOrgChart);
			}
			if (radOrgChart != null)
			{
				list.Add(radOrgChart.Nodes.IndexOf(item).ToString());
			}
			list.Reverse();
			string[] value = list.ToArray();
			return string.Join(":", value);
		}

		// Token: 0x170025DD RID: 9693
		// (get) Token: 0x06007448 RID: 29768 RVA: 0x001B2125 File Offset: 0x001B0325
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public OrgChartGroupItemCollection GroupItems
		{
			get
			{
				if (this._group == null)
				{
					this._group = new OrgChartGroupItemCollection(this.OrgChart);
					this._group.Node = this;
				}
				return this._group;
			}
		}

		// Token: 0x170025DE RID: 9694
		// (get) Token: 0x06007449 RID: 29769 RVA: 0x001B2152 File Offset: 0x001B0352
		public OrgChartNodeRendererBase Renderer
		{
			get
			{
				if (this._renderer == null)
				{
					this._renderer = RendererFactory.CreateOrgChartNodeRenderer(this.OrgChart);
				}
				return this._renderer;
			}
		}

		// Token: 0x170025DF RID: 9695
		// (get) Token: 0x0600744A RID: 29770 RVA: 0x001B2173 File Offset: 0x001B0373
		// (set) Token: 0x0600744B RID: 29771 RVA: 0x001B217B File Offset: 0x001B037B
		[Browsable(false)]
		public OrgChartNodeCollection Container { get; internal set; }

		// Token: 0x170025E0 RID: 9696
		// (get) Token: 0x0600744C RID: 29772 RVA: 0x001B2184 File Offset: 0x001B0384
		// (set) Token: 0x0600744D RID: 29773 RVA: 0x001B218C File Offset: 0x001B038C
		[Browsable(false)]
		public IOrgChartNodeContainer Parent
		{
			get
			{
				return this._parentNodesContainer;
			}
			internal set
			{
				this._parentNodesContainer = value;
				foreach (OrgChartNode orgChartNode in this.Nodes)
				{
					orgChartNode.Parent = this;
				}
			}
		}

		// Token: 0x170025E1 RID: 9697
		// (get) Token: 0x0600744E RID: 29774 RVA: 0x001B21E8 File Offset: 0x001B03E8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public OrgChartRenderedFieldCollection RenderedFields
		{
			get
			{
				if (this._renderedFields == null)
				{
					this._renderedFields = new OrgChartRenderedFieldCollection();
				}
				return this._renderedFields;
			}
		}

		// Token: 0x170025E2 RID: 9698
		// (get) Token: 0x0600744F RID: 29775 RVA: 0x001B2203 File Offset: 0x001B0403
		// (set) Token: 0x06007450 RID: 29776 RVA: 0x001B220B File Offset: 0x001B040B
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[TemplateContainer(typeof(OrgChartGroupItemRendererBase))]
		public ITemplate ItemTemplate { get; set; }

		// Token: 0x170025E3 RID: 9699
		// (get) Token: 0x06007451 RID: 29777 RVA: 0x001B2214 File Offset: 0x001B0414
		public int Level
		{
			get
			{
				int num = 0;
				if (this.OrgChart != null && this.Parent != null)
				{
					IOrgChartNodeContainer parent = this.Parent;
					while (parent != this.OrgChart)
					{
						if (parent == null)
						{
							num = -1;
							break;
						}
						parent = ((OrgChartNode)parent).Parent;
						num++;
					}
				}
				else
				{
					num = -1;
				}
				return num;
			}
		}

		// Token: 0x170025E4 RID: 9700
		// (get) Token: 0x06007452 RID: 29778 RVA: 0x001B2261 File Offset: 0x001B0461
		// (set) Token: 0x06007453 RID: 29779 RVA: 0x001B2274 File Offset: 0x001B0474
		[DefaultValue(-1)]
		public int ColumnCount
		{
			get
			{
				if (this._columnCount >= 0)
				{
					return this._columnCount;
				}
				return -1;
			}
			set
			{
				this._columnCount = value;
			}
		}

		// Token: 0x170025E5 RID: 9701
		// (get) Token: 0x06007454 RID: 29780 RVA: 0x001B227D File Offset: 0x001B047D
		public string ID
		{
			get
			{
				return this._nodeID;
			}
		}

		// Token: 0x170025E6 RID: 9702
		// (get) Token: 0x06007455 RID: 29781 RVA: 0x001B2285 File Offset: 0x001B0485
		// (set) Token: 0x06007456 RID: 29782 RVA: 0x001B228D File Offset: 0x001B048D
		public string CssClass { get; set; }

		// Token: 0x170025E7 RID: 9703
		// (get) Token: 0x06007457 RID: 29783 RVA: 0x001B2296 File Offset: 0x001B0496
		// (set) Token: 0x06007458 RID: 29784 RVA: 0x001B229E File Offset: 0x001B049E
		public bool Collapsed
		{
			get
			{
				return this._collapsed;
			}
			set
			{
				this._collapsed = value;
			}
		}

		// Token: 0x170025E8 RID: 9704
		// (get) Token: 0x06007459 RID: 29785 RVA: 0x001B22A7 File Offset: 0x001B04A7
		// (set) Token: 0x0600745A RID: 29786 RVA: 0x001B22AF File Offset: 0x001B04AF
		public bool GroupCollapsed
		{
			get
			{
				return this._groupCollapsed;
			}
			set
			{
				this._groupCollapsed = value;
			}
		}

		// Token: 0x170025E9 RID: 9705
		// (get) Token: 0x0600745B RID: 29787 RVA: 0x001B22B8 File Offset: 0x001B04B8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public OrgChartNodeCollection Nodes
		{
			get
			{
				if (this._nodes == null)
				{
					this._nodes = new OrgChartNodeCollection(this.OrgChart, this);
				}
				return this._nodes;
			}
		}

		// Token: 0x0600745C RID: 29788 RVA: 0x001B22DC File Offset: 0x001B04DC
		public void SyncRenderedProperties()
		{
			this.Renderer.IsFirst = this.IsFirst;
			this.Renderer.IsLast = this.IsLast;
			this.Renderer.IsRoot = this.IsRoot;
			this.Renderer.HasNodes = this.HasNodes;
			this.Renderer.EnableCollapsing = (this.OrgChart != null && this.OrgChart.EnableCollapsing);
			this.Renderer.Collapsed = this.Collapsed;
			this.Renderer.HasNodesForLoad = this.HasNodesForLoad;
			this.Renderer.CssClass = this.CssClass;
			this.Renderer.IsDrilled = this.IsDrilled;
		}

		// Token: 0x170025EA RID: 9706
		// (get) Token: 0x0600745D RID: 29789 RVA: 0x001B2394 File Offset: 0x001B0594
		internal int AppliedColumnCount
		{
			get
			{
				int result = -1;
				if (this.ColumnCount > 0 && this.GroupItems.Count > 1)
				{
					result = this.ColumnCount;
				}
				else if (this.ColumnCount == -1)
				{
					result = this.OrgChart.GroupColumnCount;
				}
				return result;
			}
		}

		// Token: 0x170025EB RID: 9707
		// (get) Token: 0x0600745E RID: 29790 RVA: 0x001B23D9 File Offset: 0x001B05D9
		private bool IsRoot
		{
			get
			{
				return this.Parent is RadOrgChart;
			}
		}

		// Token: 0x170025EC RID: 9708
		// (get) Token: 0x0600745F RID: 29791 RVA: 0x001B23E9 File Offset: 0x001B05E9
		private bool IsFirst
		{
			get
			{
				return this.Parent != null && this.Parent.Nodes.IndexOf(this) == 0;
			}
		}

		// Token: 0x170025ED RID: 9709
		// (get) Token: 0x06007460 RID: 29792 RVA: 0x001B2409 File Offset: 0x001B0609
		private bool IsLast
		{
			get
			{
				return this.Parent != null && this.Parent.Nodes.IndexOf(this) == this.Parent.Nodes.Count - 1;
			}
		}

		// Token: 0x170025EE RID: 9710
		// (get) Token: 0x06007461 RID: 29793 RVA: 0x001B243A File Offset: 0x001B063A
		private bool IsDrilled
		{
			get
			{
				return this.IsRoot && this.OrgChart._drilledNodeIndexes != null && this.OrgChart._drilledNodeIndexes.Length > 1;
			}
		}

		// Token: 0x170025EF RID: 9711
		// (get) Token: 0x06007462 RID: 29794 RVA: 0x001B2466 File Offset: 0x001B0666
		internal bool HasNodes
		{
			get
			{
				return this.Nodes.Count > 0;
			}
		}

		// Token: 0x170025F0 RID: 9712
		// (get) Token: 0x06007463 RID: 29795 RVA: 0x001B2476 File Offset: 0x001B0676
		internal bool HasNodesForLoad
		{
			get
			{
				return this.OrgChart._webServiceBindings != null;
			}
		}

		// Token: 0x170025F1 RID: 9713
		// (get) Token: 0x06007464 RID: 29796 RVA: 0x001B2489 File Offset: 0x001B0689
		// (set) Token: 0x06007465 RID: 29797 RVA: 0x001B2491 File Offset: 0x001B0691
		internal RadOrgChart OrgChart { get; set; }

		// Token: 0x170025F2 RID: 9714
		// (get) Token: 0x06007466 RID: 29798 RVA: 0x001B249A File Offset: 0x001B069A
		// (set) Token: 0x06007467 RID: 29799 RVA: 0x001B24A2 File Offset: 0x001B06A2
		[Browsable(false)]
		public object DataItem { get; set; }

		// Token: 0x06007468 RID: 29800 RVA: 0x001B24AB File Offset: 0x001B06AB
		public void DataBind()
		{
			if (this.OrgChart != null && !this.OrgChart.IsGroupEnabledBinding && this.GroupItems.Count == 1)
			{
				this.GroupItems[0].DataItem = this.DataItem;
			}
		}

		// Token: 0x06007469 RID: 29801 RVA: 0x001B24E8 File Offset: 0x001B06E8
		void IItem.PopulateFromDataItem(PropertyDescriptorCache properties, object dataItem, string dataMember, int depth)
		{
			if (this.OrgChart.IsGroupEnabledBinding)
			{
				object propertyValue = properties.GetPropertyValue(dataItem, this.OrgChart.GroupEnabledBinding.NodeBindingSettings.DataFieldID);
				this._nodeID = propertyValue.ToString();
				this.ParseCollapsedField(this.OrgChart.GroupEnabledBinding.NodeBindingSettings.DataCollapsedField, properties, dataItem, false);
				this.ParseCollapsedField(this.OrgChart.GroupEnabledBinding.NodeBindingSettings.DataGroupCollapsedField, properties, dataItem, true);
				if (this.OrgChart.ItemsHash.ContainsKey(propertyValue))
				{
					List<OrgChartGroupItem> list = this.OrgChart.ItemsHash[propertyValue];
					if (list != null)
					{
						this.GroupItems.AddRange(list);
					}
				}
				OrgChartRenderedFieldCollection nodeFields = this.OrgChart.RenderedFields.NodeFields;
				using (List<OrgChartRenderedField>.Enumerator enumerator = nodeFields.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						OrgChartRenderedField orgChartRenderedField = enumerator.Current;
						string text = properties.GetPropertyValue(dataItem, orgChartRenderedField.DataField) as string;
						this.RenderedFields.Add(new OrgChartRenderedField
						{
							Text = text,
							MasterField = orgChartRenderedField
						});
					}
					return;
				}
			}
			OrgChartGroupItem orgChartGroupItem = new OrgChartGroupItem(this.OrgChart);
			if (this.OrgChart.DataFieldID != null)
			{
				this._nodeID = properties.GetPropertyValue(dataItem, this.OrgChart.DataFieldID).ToString();
			}
			this.ParseCollapsedField(this.OrgChart.DataCollapsedField, properties, dataItem, false);
			((IItem)orgChartGroupItem).PopulateFromDataItem(properties, dataItem, dataMember, depth);
			this.GroupItems.Add(orgChartGroupItem);
		}

		// Token: 0x0600746A RID: 29802 RVA: 0x001B2688 File Offset: 0x001B0888
		private void ParseCollapsedField(string collapseField, PropertyDescriptorCache properties, object dataItem, bool isGroupCollapsing)
		{
			if (!string.IsNullOrEmpty(collapseField))
			{
				object obj = properties.GetPropertyValue(dataItem, collapseField);
				bool flag = false;
				try
				{
					int num;
					if (int.TryParse(obj as string, out num))
					{
						obj = num;
					}
					flag = Convert.ToBoolean(obj);
				}
				catch (FormatException)
				{
					flag = false;
				}
				catch (InvalidCastException)
				{
					flag = false;
				}
				finally
				{
					if (isGroupCollapsing)
					{
						this._groupCollapsed = flag;
					}
					else
					{
						this._collapsed = flag;
					}
				}
			}
		}

		// Token: 0x170025F3 RID: 9715
		// (get) Token: 0x0600746B RID: 29803 RVA: 0x001B2710 File Offset: 0x001B0910
		IList IItem.Children
		{
			get
			{
				return this.Nodes;
			}
		}

		// Token: 0x0600746C RID: 29804 RVA: 0x001B2718 File Offset: 0x001B0918
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600746D RID: 29805 RVA: 0x001B2720 File Offset: 0x001B0920
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader.HasAttributes)
			{
				string attribute = reader.GetAttribute("ColumnCount");
				if (attribute != null)
				{
					this.ColumnCount = int.Parse(attribute);
				}
				string attribute2 = reader.GetAttribute("Collapsed");
				if (attribute2 != null)
				{
					this.Collapsed = bool.Parse(attribute2);
				}
				this._nodeID = reader.GetAttribute("ID");
			}
			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					Type typeFromHandle = typeof(OrgChartRenderedFieldCollection);
					Type typeFromHandle2 = typeof(OrgChartGroupItemCollection);
					Type typeFromHandle3 = typeof(OrgChartNodeCollection);
					XmlRootAttribute xmlRootAttribute = typeFromHandle.GetCustomAttributes(typeof(XmlRootAttribute), false)[0] as XmlRootAttribute;
					XmlRootAttribute xmlRootAttribute2 = typeFromHandle2.GetCustomAttributes(typeof(XmlRootAttribute), false)[0] as XmlRootAttribute;
					XmlRootAttribute xmlRootAttribute3 = typeFromHandle3.GetCustomAttributes(typeof(XmlRootAttribute), false)[0] as XmlRootAttribute;
					if (xmlRootAttribute.ElementName == reader.Name)
					{
						this.DeserializeRenderedFieldCollection(reader);
					}
					else if (xmlRootAttribute2.ElementName == reader.Name)
					{
						this.DeserializeGroupItemCollection(reader);
					}
					else if (xmlRootAttribute3.ElementName == reader.Name)
					{
						this.DeserializeNodeCollection(reader);
					}
				}
			}
		}

		// Token: 0x0600746E RID: 29806 RVA: 0x001B2870 File Offset: 0x001B0A70
		private void DeserializeRenderedFieldCollection(XmlReader reader)
		{
			using (XmlReader xmlReader = reader.ReadSubtree())
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrgChartRenderedFieldCollection));
				OrgChartRenderedFieldCollection orgChartRenderedFieldCollection = (OrgChartRenderedFieldCollection)xmlSerializer.Deserialize(xmlReader);
				foreach (OrgChartRenderedField item in orgChartRenderedFieldCollection)
				{
					this.RenderedFields.Add(item);
				}
				reader.MoveToContent();
			}
		}

		// Token: 0x0600746F RID: 29807 RVA: 0x001B2908 File Offset: 0x001B0B08
		private void DeserializeGroupItemCollection(XmlReader reader)
		{
			using (XmlReader xmlReader = reader.ReadSubtree())
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrgChartGroupItemCollection));
				OrgChartGroupItemCollection orgChartGroupItemCollection = (OrgChartGroupItemCollection)xmlSerializer.Deserialize(xmlReader);
				foreach (OrgChartGroupItem item in orgChartGroupItemCollection)
				{
					this.GroupItems.Add(item);
				}
				reader.MoveToContent();
			}
		}

		// Token: 0x06007470 RID: 29808 RVA: 0x001B29A0 File Offset: 0x001B0BA0
		private void DeserializeNodeCollection(XmlReader reader)
		{
			using (XmlReader xmlReader = reader.ReadSubtree())
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrgChartNodeCollection));
				OrgChartNodeCollection orgChartNodeCollection = (OrgChartNodeCollection)xmlSerializer.Deserialize(xmlReader);
				foreach (OrgChartNode node in orgChartNodeCollection)
				{
					this.Nodes.Add(node);
				}
				reader.MoveToContent();
			}
		}

		// Token: 0x06007471 RID: 29809 RVA: 0x001B2A38 File Offset: 0x001B0C38
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXmlForOwnedAttributes(writer);
			this.WriteXmlForInnerContent(writer);
		}

		// Token: 0x06007472 RID: 29810 RVA: 0x001B2A48 File Offset: 0x001B0C48
		private void WriteXmlForOwnedAttributes(XmlWriter writer)
		{
			if (this.ColumnCount > 0)
			{
				writer.WriteAttributeString("ColumnCount", this.ColumnCount.ToString());
			}
			if (this.Collapsed)
			{
				writer.WriteAttributeString("Collapsed", this.Collapsed.ToString());
			}
			if (!string.IsNullOrEmpty(this.ID))
			{
				writer.WriteAttributeString("ID", this.ID);
			}
		}

		// Token: 0x06007473 RID: 29811 RVA: 0x001B2AB8 File Offset: 0x001B0CB8
		private void WriteXmlForInnerContent(XmlWriter writer)
		{
			if (this.RenderedFields.Count > 0)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrgChartRenderedFieldCollection));
				xmlSerializer.Serialize(writer, this.RenderedFields);
			}
			if (this.GroupItems.Count > 0)
			{
				XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(OrgChartGroupItemCollection));
				xmlSerializer2.Serialize(writer, this.GroupItems);
			}
			if (this.Nodes.Count > 0)
			{
				XmlSerializer xmlSerializer3 = new XmlSerializer(typeof(OrgChartNodeCollection));
				xmlSerializer3.Serialize(writer, this.Nodes);
			}
		}

		// Token: 0x06007474 RID: 29812 RVA: 0x001B2B46 File Offset: 0x001B0D46
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06007475 RID: 29813 RVA: 0x001B2B58 File Offset: 0x001B0D58
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._renderer != null)
				{
					this._renderer.Dispose();
					this._renderer = null;
				}
				if (this._group != null)
				{
					this._group.Dispose();
				}
				if (this._nodes != null)
				{
					this._nodes.Dispose();
				}
			}
		}

		// Token: 0x04001F95 RID: 8085
		private OrgChartNodeCollection _nodes;

		// Token: 0x04001F96 RID: 8086
		private OrgChartGroupItemCollection _group;

		// Token: 0x04001F97 RID: 8087
		private OrgChartNodeRendererBase _renderer;

		// Token: 0x04001F98 RID: 8088
		private IOrgChartNodeContainer _parentNodesContainer;

		// Token: 0x04001F99 RID: 8089
		private OrgChartRenderedFieldCollection _renderedFields;

		// Token: 0x04001F9A RID: 8090
		private int _columnCount = -1;

		// Token: 0x04001F9B RID: 8091
		private string _nodeID = string.Empty;

		// Token: 0x04001F9C RID: 8092
		private bool _collapsed;

		// Token: 0x04001F9D RID: 8093
		private bool _groupCollapsed;
	}
}
