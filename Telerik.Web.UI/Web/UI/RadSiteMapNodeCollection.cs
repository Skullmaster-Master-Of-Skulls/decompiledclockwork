using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001ABA RID: 6842
	public class RadSiteMapNodeCollection : ControlItemCollection, IList<RadSiteMapNode>, ICollection<RadSiteMapNode>, IEnumerable<RadSiteMapNode>, IEnumerable
	{
		// Token: 0x0601089D RID: 67741 RVA: 0x003B11E4 File Offset: 0x003AF3E4
		public RadSiteMapNodeCollection(Control parent) : base(parent)
		{
		}

		// Token: 0x17005063 RID: 20579
		public RadSiteMapNode this[int index]
		{
			get
			{
				return (RadSiteMapNode)base[index];
			}
			set
			{
				base[index] = value;
			}
		}

		// Token: 0x060108A0 RID: 67744 RVA: 0x003B1205 File Offset: 0x003AF405
		public void Add(RadSiteMapNode node)
		{
			base.Add(node);
		}

		// Token: 0x060108A1 RID: 67745 RVA: 0x003B1210 File Offset: 0x003AF410
		public virtual void AddRange(IEnumerable<RadSiteMapNode> nodes)
		{
			IList<ControlItem> list = new List<ControlItem>();
			foreach (RadSiteMapNode item in nodes)
			{
				list.Add(item);
			}
			base.AddRange(list);
		}

		// Token: 0x060108A2 RID: 67746 RVA: 0x003B1268 File Offset: 0x003AF468
		public void Insert(int index, RadSiteMapNode node)
		{
			base.Insert(index, node);
		}

		// Token: 0x060108A3 RID: 67747 RVA: 0x003B1272 File Offset: 0x003AF472
		public void Remove(RadSiteMapNode node)
		{
			base.Remove(node);
		}

		// Token: 0x060108A4 RID: 67748 RVA: 0x003B127B File Offset: 0x003AF47B
		public RadSiteMapNode FindNodeByText(string text)
		{
			return base.FindChildByText<RadSiteMapNode>(text);
		}

		// Token: 0x060108A5 RID: 67749 RVA: 0x003B1284 File Offset: 0x003AF484
		public RadSiteMapNode FindNodeByText(string text, bool ignoreCase)
		{
			return base.FindChildByText<RadSiteMapNode>(text, ignoreCase);
		}

		// Token: 0x060108A6 RID: 67750 RVA: 0x003B128E File Offset: 0x003AF48E
		public RadSiteMapNode FindNode(Predicate<RadSiteMapNode> match)
		{
			return base.FindChild<RadSiteMapNode>(match);
		}

		// Token: 0x060108A7 RID: 67751 RVA: 0x003B1298 File Offset: 0x003AF498
		protected override void SetOwner(ControlItem node)
		{
			RadSiteMapNode radSiteMapNode = node as RadSiteMapNode;
			IRadSiteMapNodeContainer owner = radSiteMapNode.Owner;
			if (owner != null && owner.Nodes.Contains(node) && owner != base.Parent)
			{
				owner.Nodes.Remove(node);
			}
			radSiteMapNode.Owner = (IRadSiteMapNodeContainer)base.Parent;
		}

		// Token: 0x060108A8 RID: 67752 RVA: 0x003B142C File Offset: 0x003AF62C
		IEnumerator<RadSiteMapNode> IEnumerable<RadSiteMapNode>.GetEnumerator()
		{
			foreach (object obj in this)
			{
				RadSiteMapNode node = (RadSiteMapNode)obj;
				yield return node;
			}
			yield break;
		}

		// Token: 0x060108A9 RID: 67753 RVA: 0x003B1448 File Offset: 0x003AF648
		bool ICollection<RadSiteMapNode>.Contains(RadSiteMapNode node)
		{
			return this.Contains(node);
		}

		// Token: 0x060108AA RID: 67754 RVA: 0x003B1451 File Offset: 0x003AF651
		void ICollection<RadSiteMapNode>.CopyTo(RadSiteMapNode[] array, int arrayIndex)
		{
			this.CopyTo(array, arrayIndex);
		}

		// Token: 0x060108AB RID: 67755 RVA: 0x003B145B File Offset: 0x003AF65B
		bool ICollection<RadSiteMapNode>.Remove(RadSiteMapNode node)
		{
			this.Remove(node);
			return true;
		}

		// Token: 0x17005064 RID: 20580
		// (get) Token: 0x060108AC RID: 67756 RVA: 0x003B1465 File Offset: 0x003AF665
		bool ICollection<RadSiteMapNode>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060108AD RID: 67757 RVA: 0x003B1468 File Offset: 0x003AF668
		int IList<RadSiteMapNode>.IndexOf(RadSiteMapNode node)
		{
			return this.IndexOf(node);
		}

		// Token: 0x060108AE RID: 67758 RVA: 0x003B1471 File Offset: 0x003AF671
		void IList<RadSiteMapNode>.Insert(int index, RadSiteMapNode node)
		{
			this.Insert(index, node);
		}

		// Token: 0x060108AF RID: 67759 RVA: 0x003B147B File Offset: 0x003AF67B
		void IList<RadSiteMapNode>.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x060108B0 RID: 67760 RVA: 0x003B1484 File Offset: 0x003AF684
		void ICollection<RadSiteMapNode>.Clear()
		{
			base.Clear();
		}

		// Token: 0x060108B1 RID: 67761 RVA: 0x003B148C File Offset: 0x003AF68C
		int ICollection<RadSiteMapNode>.get_Count()
		{
			return base.Count;
		}
	}
}
