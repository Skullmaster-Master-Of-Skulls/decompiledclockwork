using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000621 RID: 1569
	public class NavigationNodeCollection : List<NavigationNode>
	{
		// Token: 0x060038FF RID: 14591 RVA: 0x000BB6A4 File Offset: 0x000B98A4
		protected void OnNodeAdding(NavigationNode node)
		{
			if (!base.Contains(node))
			{
				node.Owner = (this._nav ?? ((NavigationNode)this.NodesContainer).Owner);
			}
			if (!((Control)this.NodesContainer).Controls.Contains(node))
			{
				((Control)this.NodesContainer).Controls.Add(node);
			}
			if (node.TemplateToApply != null)
			{
				node.ApplyTemplate(node.TemplateToApply);
				node.ApplyContentTemplate();
			}
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x000BB724 File Offset: 0x000B9924
		protected void OnNodeAdded(NavigationNode node)
		{
			if (!base.Contains(node))
			{
				node.Owner = (this._nav ?? ((NavigationNode)this.NodesContainer).Owner);
			}
			if (this._nav != null)
			{
				this._nav.AssignReferencesToInnerTree(this._nav.Nodes, this._nav);
			}
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x000BB77E File Offset: 0x000B997E
		protected void OnNodeRemoving(NavigationNode node)
		{
			node.Owner = null;
			((Control)this.NodesContainer).Controls.Remove(node);
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x000BB79D File Offset: 0x000B999D
		public NavigationNodeCollection()
		{
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x000BB7A5 File Offset: 0x000B99A5
		public NavigationNodeCollection(RadNavigation control, INavigationNodeContainer nodesContainer)
		{
			this._nav = control;
			this.NodesContainer = nodesContainer;
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x000BB7BB File Offset: 0x000B99BB
		public new void Add(NavigationNode node)
		{
			if (!base.Contains(node))
			{
				this.OnNodeAdding(node);
				base.Add(node);
				this.OnNodeAdded(node);
			}
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x000BB7DB File Offset: 0x000B99DB
		public new void Insert(int index, NavigationNode node)
		{
			if ((base.Count > 0 && index < base.Count && index >= 0) || (base.Count == 0 && index == 0))
			{
				this.OnNodeAdding(node);
			}
			base.Insert(index, node);
			this.OnNodeAdded(node);
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x000BB814 File Offset: 0x000B9A14
		public new void AddRange(IEnumerable<NavigationNode> collection)
		{
			foreach (NavigationNode node in collection)
			{
				this.OnNodeAdding(node);
			}
			base.AddRange(collection);
			foreach (NavigationNode node2 in collection)
			{
				this.OnNodeAdded(node2);
			}
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x000BB89C File Offset: 0x000B9A9C
		public new void InsertRange(int index, IEnumerable<NavigationNode> collection)
		{
			if ((base.Count > 0 && index < base.Count && index >= 0) || (base.Count == 0 && index == 0))
			{
				foreach (NavigationNode node in collection)
				{
					this.OnNodeAdding(node);
				}
			}
			base.InsertRange(index, collection);
			foreach (NavigationNode node2 in collection)
			{
				this.OnNodeAdded(node2);
			}
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x000BB944 File Offset: 0x000B9B44
		public new void Remove(NavigationNode node)
		{
			if (base.Contains(node))
			{
				this.OnNodeRemoving(node);
			}
			base.Remove(node);
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x000BB960 File Offset: 0x000B9B60
		public new void RemoveAll(Predicate<NavigationNode> match)
		{
			foreach (NavigationNode node in base.FindAll(match))
			{
				this.OnNodeRemoving(node);
			}
			base.RemoveAll(match);
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x000BB9BC File Offset: 0x000B9BBC
		public new void RemoveAt(int index)
		{
			if (base.Count > 0 && index < base.Count && index >= 0)
			{
				this.OnNodeRemoving(base[index]);
			}
			base.RemoveAt(index);
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x000BB9E8 File Offset: 0x000B9BE8
		public new void RemoveRange(int index, int count)
		{
			if (base.Count > 0 && index + count < base.Count && index >= 0)
			{
				foreach (NavigationNode node in base.GetRange(index, count))
				{
					this.OnNodeRemoving(node);
				}
			}
			base.RemoveAt(index);
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x000BBA5C File Offset: 0x000B9C5C
		public new void Clear()
		{
			foreach (NavigationNode node in this)
			{
				this.OnNodeRemoving(node);
			}
			base.Clear();
		}

		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x0600390D RID: 14605 RVA: 0x000BBAB0 File Offset: 0x000B9CB0
		// (set) Token: 0x0600390E RID: 14606 RVA: 0x000BBAB8 File Offset: 0x000B9CB8
		internal RadNavigation Nav { get; set; }

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x0600390F RID: 14607 RVA: 0x000BBAC1 File Offset: 0x000B9CC1
		// (set) Token: 0x06003910 RID: 14608 RVA: 0x000BBAC9 File Offset: 0x000B9CC9
		public INavigationNodeContainer NodesContainer { get; set; }

		// Token: 0x04000F39 RID: 3897
		private RadNavigation _nav;
	}
}
