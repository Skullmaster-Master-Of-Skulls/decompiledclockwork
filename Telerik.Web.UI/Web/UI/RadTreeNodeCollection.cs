using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200129E RID: 4766
	public class RadTreeNodeCollection : ControlItemCollection
	{
		// Token: 0x0600C7C0 RID: 51136 RVA: 0x002C7994 File Offset: 0x002C5B94
		public RadTreeNodeCollection(Control parent) : base(parent)
		{
		}

		// Token: 0x1700408F RID: 16527
		public RadTreeNode this[int index]
		{
			get
			{
				return (RadTreeNode)base[index];
			}
			set
			{
				base[index] = value;
			}
		}

		// Token: 0x0600C7C3 RID: 51139 RVA: 0x002C79B5 File Offset: 0x002C5BB5
		public void Add(RadTreeNode node)
		{
			if (!this.Contains(node))
			{
				base.Add(node);
			}
		}

		// Token: 0x0600C7C4 RID: 51140 RVA: 0x002C79C7 File Offset: 0x002C5BC7
		public void Remove(RadTreeNode node)
		{
			base.Remove(node);
			node.Owner = null;
		}

		// Token: 0x0600C7C5 RID: 51141 RVA: 0x002C79D7 File Offset: 0x002C5BD7
		public new void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x0600C7C6 RID: 51142 RVA: 0x002C79E6 File Offset: 0x002C5BE6
		public virtual bool Contains(RadTreeNode node)
		{
			return base.Contains(node);
		}

		// Token: 0x0600C7C7 RID: 51143 RVA: 0x002C79EF File Offset: 0x002C5BEF
		public virtual void CopyTo(RadTreeNode[] array, int index)
		{
			base.CopyTo(array, index);
		}

		// Token: 0x0600C7C8 RID: 51144 RVA: 0x002C79FC File Offset: 0x002C5BFC
		public virtual void AddRange(IEnumerable<RadTreeNode> nodes)
		{
			IList<ControlItem> list = new List<ControlItem>();
			foreach (RadTreeNode radTreeNode in nodes)
			{
				if (!this.Contains(radTreeNode))
				{
					list.Add(radTreeNode);
				}
			}
			base.AddRange(list);
		}

		// Token: 0x0600C7C9 RID: 51145 RVA: 0x002C7A5C File Offset: 0x002C5C5C
		public virtual int IndexOf(RadTreeNode node)
		{
			return base.IndexOf(node);
		}

		// Token: 0x0600C7CA RID: 51146 RVA: 0x002C7A65 File Offset: 0x002C5C65
		public virtual void Insert(int index, RadTreeNode node)
		{
			if (!this.Contains(node))
			{
				base.Insert(index, node);
			}
		}

		// Token: 0x0600C7CB RID: 51147 RVA: 0x002C7A78 File Offset: 0x002C5C78
		public RadTreeNode FindNodeByText(string text)
		{
			return base.FindChildByText<RadTreeNode>(text);
		}

		// Token: 0x0600C7CC RID: 51148 RVA: 0x002C7A81 File Offset: 0x002C5C81
		public RadTreeNode FindNodeByText(string text, bool ignoreCase)
		{
			return base.FindChildByText<RadTreeNode>(text, ignoreCase);
		}

		// Token: 0x0600C7CD RID: 51149 RVA: 0x002C7A8B File Offset: 0x002C5C8B
		public RadTreeNode FindNodeByValue(string value)
		{
			return base.FindChildByValue<RadTreeNode>(value);
		}

		// Token: 0x0600C7CE RID: 51150 RVA: 0x002C7A94 File Offset: 0x002C5C94
		public RadTreeNode FindNodeByValue(string value, bool ignoreCase)
		{
			return base.FindChildByValue<RadTreeNode>(value, ignoreCase);
		}

		// Token: 0x0600C7CF RID: 51151 RVA: 0x002C7A9E File Offset: 0x002C5C9E
		public RadTreeNode FindNodeByAttribute(string attributeName, string attributeValue)
		{
			return base.FindChildByAttribute<RadTreeNode>(attributeName, attributeValue);
		}

		// Token: 0x0600C7D0 RID: 51152 RVA: 0x002C7AA8 File Offset: 0x002C5CA8
		public RadTreeNode FindNode(Predicate<RadTreeNode> match)
		{
			return base.FindChild<RadTreeNode>(match);
		}

		// Token: 0x0600C7D1 RID: 51153 RVA: 0x002C7AB4 File Offset: 0x002C5CB4
		protected override void SetOwner(ControlItem item)
		{
			RadTreeNode radTreeNode = item as RadTreeNode;
			IRadTreeNodeContainer owner = radTreeNode.Owner;
			if (owner != null && owner.Nodes.Contains(item) && owner != base.Parent)
			{
				owner.Nodes.Remove(item);
			}
			radTreeNode.Owner = (IRadTreeNodeContainer)base.Parent;
		}

		// Token: 0x0600C7D2 RID: 51154 RVA: 0x002C7B06 File Offset: 0x002C5D06
		protected override void AddItemToParentControls(int index, ControlItem item)
		{
			index = this.AdjustControlIndexDependingOnContextMenusCount(index);
			base.AddItemToParentControls(index, item);
		}

		// Token: 0x0600C7D3 RID: 51155 RVA: 0x002C7B1C File Offset: 0x002C5D1C
		private int AdjustControlIndexDependingOnContextMenusCount(int index)
		{
			RadTreeView radTreeView = base.Parent as RadTreeView;
			if (radTreeView != null && index == -1)
			{
				index = radTreeView.Nodes.Count - 1;
			}
			return index;
		}
	}
}
