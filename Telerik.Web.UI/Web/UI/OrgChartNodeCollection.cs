using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000BDD RID: 3037
	[XmlRoot("Nodes")]
	public class OrgChartNodeCollection : List<OrgChartNode>, IOrgChartRendererContainer, IXmlSerializable, IDisposable
	{
		// Token: 0x060073D9 RID: 29657 RVA: 0x001B0B42 File Offset: 0x001AED42
		public OrgChartNodeCollection()
		{
		}

		// Token: 0x060073DA RID: 29658 RVA: 0x001B0B4A File Offset: 0x001AED4A
		internal OrgChartNodeCollection(RadOrgChart orgChart, IOrgChartNodeContainer nodesContainer)
		{
			this.OrgChart = orgChart;
			this.NodesContainer = nodesContainer;
		}

		// Token: 0x060073DB RID: 29659 RVA: 0x001B0B60 File Offset: 0x001AED60
		public new void Add(OrgChartNode node)
		{
			if (!base.Contains(node))
			{
				this.OnNodeAdding(node);
				base.Add(node);
				this.OnNodeAdded(node);
			}
		}

		// Token: 0x060073DC RID: 29660 RVA: 0x001B0B80 File Offset: 0x001AED80
		public new void Insert(int index, OrgChartNode node)
		{
			if ((base.Count > 0 && index < base.Count && index >= 0) || (base.Count == 0 && index == 0))
			{
				this.OnNodeAdding(node);
				base.Insert(index, node);
				this.OnNodeAdded(node);
				return;
			}
			base.Insert(index, node);
		}

		// Token: 0x060073DD RID: 29661 RVA: 0x001B0BD0 File Offset: 0x001AEDD0
		public new void AddRange(IEnumerable<OrgChartNode> collection)
		{
			foreach (OrgChartNode node in collection)
			{
				this.OnNodeAdding(node);
			}
			base.AddRange(collection);
			foreach (OrgChartNode node2 in collection)
			{
				this.OnNodeAdded(node2);
			}
		}

		// Token: 0x060073DE RID: 29662 RVA: 0x001B0C58 File Offset: 0x001AEE58
		public new void InsertRange(int index, IEnumerable<OrgChartNode> collection)
		{
			if ((base.Count > 0 && index < base.Count && index >= 0) || (base.Count == 0 && index == 0))
			{
				foreach (OrgChartNode node in collection)
				{
					this.OnNodeAdding(node);
				}
			}
			base.InsertRange(index, collection);
		}

		// Token: 0x060073DF RID: 29663 RVA: 0x001B0CCC File Offset: 0x001AEECC
		public new void Remove(OrgChartNode node)
		{
			this.OnNodeRemoving(node);
			base.Remove(node);
		}

		// Token: 0x060073E0 RID: 29664 RVA: 0x001B0CE0 File Offset: 0x001AEEE0
		public new void RemoveAll(Predicate<OrgChartNode> match)
		{
			foreach (OrgChartNode node in base.FindAll(match))
			{
				this.OnNodeRemoving(node);
			}
			base.RemoveAll(match);
		}

		// Token: 0x060073E1 RID: 29665 RVA: 0x001B0D3C File Offset: 0x001AEF3C
		public new void RemoveAt(int index)
		{
			if (base.Count > 0 && index < base.Count && index >= 0)
			{
				this.OnNodeRemoving(base[index]);
			}
			base.RemoveAt(index);
		}

		// Token: 0x060073E2 RID: 29666 RVA: 0x001B0D68 File Offset: 0x001AEF68
		public new void RemoveRange(int index, int count)
		{
			if (base.Count > 0 && index + count < base.Count && index >= 0)
			{
				foreach (OrgChartNode node in base.GetRange(index, count))
				{
					this.OnNodeRemoving(node);
				}
			}
			base.RemoveAt(index);
		}

		// Token: 0x060073E3 RID: 29667 RVA: 0x001B0DDC File Offset: 0x001AEFDC
		public new void Clear()
		{
			foreach (OrgChartNode node in this)
			{
				this.OnNodeRemoving(node);
			}
			base.Clear();
		}

		// Token: 0x170025B9 RID: 9657
		// (get) Token: 0x060073E4 RID: 29668 RVA: 0x001B0E30 File Offset: 0x001AF030
		// (set) Token: 0x060073E5 RID: 29669 RVA: 0x001B0E38 File Offset: 0x001AF038
		public IOrgChartNodeContainer NodesContainer { get; set; }

		// Token: 0x170025BA RID: 9658
		// (get) Token: 0x060073E6 RID: 29670 RVA: 0x001B0E41 File Offset: 0x001AF041
		public bool IsRootNodeCollection
		{
			get
			{
				return this.OrgChart.Nodes == this;
			}
		}

		// Token: 0x170025BB RID: 9659
		// (get) Token: 0x060073E7 RID: 29671 RVA: 0x001B0E51 File Offset: 0x001AF051
		public OrgChartNodeCollectionRenderer Renderer
		{
			get
			{
				if (this._renderer == null)
				{
					this._renderer = new OrgChartNodeCollectionRenderer();
				}
				return this._renderer;
			}
		}

		// Token: 0x060073E8 RID: 29672 RVA: 0x001B0E6C File Offset: 0x001AF06C
		public void SyncRenderedProperties()
		{
			this.Renderer.Level = this.Level;
		}

		// Token: 0x170025BC RID: 9660
		// (get) Token: 0x060073E9 RID: 29673 RVA: 0x001B0E7F File Offset: 0x001AF07F
		// (set) Token: 0x060073EA RID: 29674 RVA: 0x001B0E87 File Offset: 0x001AF087
		internal RadOrgChart OrgChart { get; set; }

		// Token: 0x170025BD RID: 9661
		// (get) Token: 0x060073EB RID: 29675 RVA: 0x001B0E90 File Offset: 0x001AF090
		private int Level
		{
			get
			{
				int result = -1;
				if (base.Count != 0)
				{
					result = base[0].Level;
				}
				return result;
			}
		}

		// Token: 0x060073EC RID: 29676 RVA: 0x001B0EB8 File Offset: 0x001AF0B8
		internal void AssignReferencesToInnerTree(OrgChartNodeCollection nodes, RadOrgChart orgChart)
		{
			foreach (OrgChartNode orgChartNode in nodes)
			{
				orgChartNode.Parent = nodes.NodesContainer;
				orgChartNode.OrgChart = orgChart;
				orgChartNode.GroupItems.Node = orgChartNode;
				orgChartNode.GroupItems.OrgChart = orgChart;
				foreach (OrgChartGroupItem orgChartGroupItem in orgChartNode.GroupItems)
				{
					orgChartGroupItem.OrgChart = orgChart;
					orgChartGroupItem.Node = orgChartNode;
				}
				this.AssignReferencesToInnerTree(orgChartNode.Nodes, orgChart);
			}
		}

		// Token: 0x060073ED RID: 29677 RVA: 0x001B0F84 File Offset: 0x001AF184
		protected void OnNodeAdding(OrgChartNode node)
		{
		}

		// Token: 0x060073EE RID: 29678 RVA: 0x001B0F86 File Offset: 0x001AF186
		protected void OnNodeAdded(OrgChartNode node)
		{
			if (this.OrgChart != null)
			{
				this.AssignReferencesToInnerTree(this, this.OrgChart);
			}
		}

		// Token: 0x060073EF RID: 29679 RVA: 0x001B0FA0 File Offset: 0x001AF1A0
		protected void OnNodeRemoving(OrgChartNode node)
		{
			if (this.Renderer.Controls.Contains(node.Renderer))
			{
				this.Renderer.Controls.Remove(node.Renderer);
			}
			node.Parent = null;
			node.OrgChart = null;
			this.AssignReferencesToInnerTree(node.Nodes, null);
		}

		// Token: 0x060073F0 RID: 29680 RVA: 0x001B0FF6 File Offset: 0x001AF1F6
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060073F1 RID: 29681 RVA: 0x001B1000 File Offset: 0x001AF200
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					using (XmlReader xmlReader = reader.ReadSubtree())
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrgChartNode));
						OrgChartNode node = (OrgChartNode)xmlSerializer.Deserialize(xmlReader);
						this.Add(node);
					}
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x060073F2 RID: 29682 RVA: 0x001B1078 File Offset: 0x001AF278
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteNodeXml(writer);
		}

		// Token: 0x060073F3 RID: 29683 RVA: 0x001B1084 File Offset: 0x001AF284
		private void WriteNodeXml(XmlWriter writer)
		{
			if (base.Count > 0)
			{
				foreach (OrgChartNode o in this)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrgChartNode));
					xmlSerializer.Serialize(writer, o);
				}
			}
		}

		// Token: 0x060073F4 RID: 29684 RVA: 0x001B10EC File Offset: 0x001AF2EC
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060073F5 RID: 29685 RVA: 0x001B10F5 File Offset: 0x001AF2F5
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._renderer != null)
			{
				this._renderer.Dispose();
				this._renderer = null;
			}
		}

		// Token: 0x04001F7A RID: 8058
		private OrgChartNodeCollectionRenderer _renderer;
	}
}
