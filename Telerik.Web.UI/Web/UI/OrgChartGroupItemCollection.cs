using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Telerik.Web.UI.OrgChart.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000BDC RID: 3036
	[XmlRoot("Items")]
	public class OrgChartGroupItemCollection : List<OrgChartGroupItem>, IOrgChartRendererContainer, IXmlSerializable, IDisposable
	{
		// Token: 0x060073BB RID: 29627 RVA: 0x001B0526 File Offset: 0x001AE726
		public OrgChartGroupItemCollection()
		{
		}

		// Token: 0x060073BC RID: 29628 RVA: 0x001B052E File Offset: 0x001AE72E
		internal OrgChartGroupItemCollection(RadOrgChart orgChart)
		{
			this.OrgChart = orgChart;
		}

		// Token: 0x060073BD RID: 29629 RVA: 0x001B053D File Offset: 0x001AE73D
		public new void Add(OrgChartGroupItem item)
		{
			this.OnItemAdding(item);
			base.Add(item);
		}

		// Token: 0x060073BE RID: 29630 RVA: 0x001B054D File Offset: 0x001AE74D
		public new void Insert(int index, OrgChartGroupItem item)
		{
			if ((base.Count > 0 && index < base.Count && index >= 0) || (base.Count == 0 && index == 0))
			{
				this.OnItemAdding(item);
			}
			base.Insert(index, item);
		}

		// Token: 0x060073BF RID: 29631 RVA: 0x001B0580 File Offset: 0x001AE780
		public new void AddRange(IEnumerable<OrgChartGroupItem> collection)
		{
			foreach (OrgChartGroupItem item in collection)
			{
				this.OnItemAdding(item);
			}
			base.AddRange(collection);
		}

		// Token: 0x060073C0 RID: 29632 RVA: 0x001B05D0 File Offset: 0x001AE7D0
		public new void InsertRange(int index, IEnumerable<OrgChartGroupItem> collection)
		{
			if ((base.Count > 0 && index < base.Count && index >= 0) || (base.Count == 0 && index == 0))
			{
				foreach (OrgChartGroupItem item in collection)
				{
					this.OnItemAdding(item);
				}
			}
			base.InsertRange(index, collection);
		}

		// Token: 0x060073C1 RID: 29633 RVA: 0x001B0644 File Offset: 0x001AE844
		public new void Remove(OrgChartGroupItem item)
		{
			if (base.Contains(item))
			{
				this.OnItemRemoving(item);
			}
			base.Remove(item);
		}

		// Token: 0x060073C2 RID: 29634 RVA: 0x001B0660 File Offset: 0x001AE860
		public new void RemoveAll(Predicate<OrgChartGroupItem> match)
		{
			foreach (OrgChartGroupItem item in base.FindAll(match))
			{
				this.OnItemRemoving(item);
			}
			base.RemoveAll(match);
		}

		// Token: 0x060073C3 RID: 29635 RVA: 0x001B06BC File Offset: 0x001AE8BC
		public new void RemoveAt(int index)
		{
			if (base.Count > 0 && index < base.Count && index >= 0)
			{
				this.OnItemRemoving(base[index]);
			}
			base.RemoveAt(index);
		}

		// Token: 0x060073C4 RID: 29636 RVA: 0x001B06E8 File Offset: 0x001AE8E8
		public new void RemoveRange(int index, int count)
		{
			if (base.Count > 0 && index + count < base.Count && index >= 0)
			{
				foreach (OrgChartGroupItem item in base.GetRange(index, count))
				{
					this.OnItemRemoving(item);
				}
			}
			base.RemoveAt(index);
		}

		// Token: 0x060073C5 RID: 29637 RVA: 0x001B075C File Offset: 0x001AE95C
		public new void Clear()
		{
			foreach (OrgChartGroupItem item in this)
			{
				this.OnItemRemoving(item);
			}
			base.Clear();
		}

		// Token: 0x170025B0 RID: 9648
		// (get) Token: 0x060073C6 RID: 29638 RVA: 0x001B07B0 File Offset: 0x001AE9B0
		// (set) Token: 0x060073C7 RID: 29639 RVA: 0x001B07B8 File Offset: 0x001AE9B8
		public OrgChartNode Node { get; set; }

		// Token: 0x170025B1 RID: 9649
		// (get) Token: 0x060073C8 RID: 29640 RVA: 0x001B07C1 File Offset: 0x001AE9C1
		public OrgChartGroupItemCollectionRendererBase Renderer
		{
			get
			{
				if (this._renderer == null)
				{
					this._renderer = RendererFactory.CreateOrgChartGroupItemCollectionRenderer(this.OrgChart);
				}
				return this._renderer;
			}
		}

		// Token: 0x060073C9 RID: 29641 RVA: 0x001B07E4 File Offset: 0x001AE9E4
		public void SyncRenderedProperties()
		{
			this.Renderer.GroupItemsCount = base.Count;
			this.Renderer.IsSimpleBinding = this.IsSimpleBinding;
			this.Renderer.EnableCollapsing = this.EnableCollapsing;
			this.Renderer.EnableGroupCollapsing = this.EnableGroupCollapsing;
			this.Renderer.Collapsed = this.Collapsed;
			this.Renderer.GroupCollapsed = this.GroupCollapsed;
			this.Renderer.HasNodes = (this.Node != null && this.Node.HasNodes);
			this.Renderer.HasNodesForLoad = (this.Node != null && this.Node.HasNodesForLoad);
			this.Renderer.IsGroup = this.IsGroup;
			foreach (OrgChartRenderedField item in this.Node.RenderedFields)
			{
				if (!this.Renderer.RenderedFields.Contains(item))
				{
					this.Renderer.RenderedFields.Add(item);
				}
			}
		}

		// Token: 0x170025B2 RID: 9650
		// (get) Token: 0x060073CA RID: 29642 RVA: 0x001B0914 File Offset: 0x001AEB14
		// (set) Token: 0x060073CB RID: 29643 RVA: 0x001B091C File Offset: 0x001AEB1C
		internal RadOrgChart OrgChart { get; set; }

		// Token: 0x170025B3 RID: 9651
		// (get) Token: 0x060073CC RID: 29644 RVA: 0x001B0925 File Offset: 0x001AEB25
		internal bool IsGroup
		{
			get
			{
				return base.Count > 1 || this.Node.RenderedFields.Count > 0;
			}
		}

		// Token: 0x170025B4 RID: 9652
		// (get) Token: 0x060073CD RID: 29645 RVA: 0x001B0945 File Offset: 0x001AEB45
		internal bool IsSimpleBinding
		{
			get
			{
				return this.OrgChart != null && this.OrgChart.IsSimpleBinding;
			}
		}

		// Token: 0x170025B5 RID: 9653
		// (get) Token: 0x060073CE RID: 29646 RVA: 0x001B095C File Offset: 0x001AEB5C
		internal bool EnableCollapsing
		{
			get
			{
				return this.Node.OrgChart != null && this.Node.OrgChart.EnableCollapsing;
			}
		}

		// Token: 0x170025B6 RID: 9654
		// (get) Token: 0x060073CF RID: 29647 RVA: 0x001B097D File Offset: 0x001AEB7D
		internal bool EnableGroupCollapsing
		{
			get
			{
				return this.Node.OrgChart != null && this.Node.OrgChart.EnableGroupCollapsing;
			}
		}

		// Token: 0x170025B7 RID: 9655
		// (get) Token: 0x060073D0 RID: 29648 RVA: 0x001B099E File Offset: 0x001AEB9E
		internal bool Collapsed
		{
			get
			{
				return this.Node != null && this.Node.Collapsed;
			}
		}

		// Token: 0x170025B8 RID: 9656
		// (get) Token: 0x060073D1 RID: 29649 RVA: 0x001B09B5 File Offset: 0x001AEBB5
		internal bool GroupCollapsed
		{
			get
			{
				return this.Node != null && this.Node.GroupCollapsed;
			}
		}

		// Token: 0x060073D2 RID: 29650 RVA: 0x001B09CC File Offset: 0x001AEBCC
		protected void OnItemAdding(OrgChartGroupItem item)
		{
			if (!base.Contains(item))
			{
				item.OrgChart = this.OrgChart;
				item.Node = this.Node;
			}
		}

		// Token: 0x060073D3 RID: 29651 RVA: 0x001B09EF File Offset: 0x001AEBEF
		protected void OnItemRemoving(OrgChartGroupItem item)
		{
			if (this.Renderer.Controls.Contains(item.Renderer))
			{
				this.Renderer.Controls.Remove(item.Renderer);
			}
			item.OrgChart = null;
			item.Node = null;
		}

		// Token: 0x060073D4 RID: 29652 RVA: 0x001B0A2D File Offset: 0x001AEC2D
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060073D5 RID: 29653 RVA: 0x001B0A34 File Offset: 0x001AEC34
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					using (XmlReader xmlReader = reader.ReadSubtree())
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrgChartGroupItem));
						OrgChartGroupItem item = (OrgChartGroupItem)xmlSerializer.Deserialize(xmlReader);
						this.Add(item);
					}
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x060073D6 RID: 29654 RVA: 0x001B0AAC File Offset: 0x001AECAC
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (base.Count > 0)
			{
				foreach (OrgChartGroupItem o in this)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrgChartGroupItem));
					xmlSerializer.Serialize(writer, o);
				}
			}
		}

		// Token: 0x060073D7 RID: 29655 RVA: 0x001B0B14 File Offset: 0x001AED14
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060073D8 RID: 29656 RVA: 0x001B0B23 File Offset: 0x001AED23
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._renderer != null)
			{
				this._renderer.Dispose();
				this._renderer = null;
			}
		}

		// Token: 0x04001F77 RID: 8055
		private OrgChartGroupItemCollectionRendererBase _renderer;
	}
}
