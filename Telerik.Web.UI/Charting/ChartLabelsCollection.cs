using System;
using System.ComponentModel;

namespace Telerik.Charting
{
	// Token: 0x02001709 RID: 5897
	public class ChartLabelsCollection : ChartingStateManagedCollection<LabelItem>
	{
		// Token: 0x170045DF RID: 17887
		// (get) Token: 0x0600E53A RID: 58682 RVA: 0x0032EC6B File Offset: 0x0032CE6B
		// (set) Token: 0x0600E53B RID: 58683 RVA: 0x0032EC73 File Offset: 0x0032CE73
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object Parent
		{
			get
			{
				return this.labelsCollectionParent;
			}
			set
			{
				this.labelsCollectionParent = value;
			}
		}

		// Token: 0x170045E0 RID: 17888
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[NotifyParentProperty(false)]
		public override LabelItem this[int index]
		{
			get
			{
				return base.List[index];
			}
			set
			{
				base.List[index] = value;
				base.List[index].Parent = this.Parent;
				((LabelItem)this.Parent).Add(base.List[index]);
			}
		}

		// Token: 0x0600E53F RID: 58687 RVA: 0x0032ED08 File Offset: 0x0032CF08
		internal void ClearBindableItems()
		{
			for (int i = 0; i < base.Count; i++)
			{
				LabelItem labelItem = this[i];
				if (labelItem.IsBound)
				{
					this.Remove(labelItem);
					i--;
				}
			}
		}

		// Token: 0x0600E540 RID: 58688 RVA: 0x0032ED44 File Offset: 0x0032CF44
		internal void CopyBindableItemsTo(ChartLabelsCollection items)
		{
			int num = 0;
			foreach (LabelItem labelItem in this)
			{
				if (labelItem.IsBound)
				{
					LabelItem labelItem2 = new LabelItem();
					labelItem2.Name = labelItem.Name;
					labelItem2.IsBound = labelItem.IsBound;
					labelItem2.Parent = labelItem.Parent;
					labelItem2.Container = labelItem.Container;
					labelItem2.ActiveRegion.Region = labelItem.ActiveRegion.Region;
					items.Insert(num++, labelItem2);
				}
			}
		}

		// Token: 0x0600E541 RID: 58689 RVA: 0x0032EDE8 File Offset: 0x0032CFE8
		internal bool IsVisible()
		{
			bool result = false;
			foreach (LabelItem labelItem in this)
			{
				if (labelItem.IsVisible())
				{
					return true;
				}
			}
			return result;
		}

		// Token: 0x0600E542 RID: 58690 RVA: 0x0032EE3C File Offset: 0x0032D03C
		public override void Add(LabelItem item)
		{
			item.Parent = this;
			((IContainer)this.Parent).Add(item);
			base.Add(item);
		}

		// Token: 0x0600E543 RID: 58691 RVA: 0x0032EE60 File Offset: 0x0032D060
		public new void Clear()
		{
			foreach (LabelItem element in base.List)
			{
				((IContainer)this.Parent).Remove(element);
			}
			base.List.Clear();
		}

		// Token: 0x0600E544 RID: 58692 RVA: 0x0032EEC4 File Offset: 0x0032D0C4
		public override void Insert(int index, LabelItem item)
		{
			item.Parent = this;
			base.Insert(index, item);
		}

		// Token: 0x0600E545 RID: 58693 RVA: 0x0032EED8 File Offset: 0x0032D0D8
		public override bool Remove(LabelItem item)
		{
			bool result = false;
			((IContainer)this.Parent).Remove(item);
			while (base.List.IndexOf(item) > -1)
			{
				result = base.List.Remove(item);
			}
			return result;
		}

		// Token: 0x0600E546 RID: 58694 RVA: 0x0032EF17 File Offset: 0x0032D117
		public override void RemoveAt(int index)
		{
			((IContainer)this.Parent).Remove(this[index]);
			base.RemoveAt(index);
		}

		// Token: 0x0600E547 RID: 58695 RVA: 0x0032EF37 File Offset: 0x0032D137
		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete(index, value);
			this.CollectionChange();
		}

		// Token: 0x0600E548 RID: 58696 RVA: 0x0032EF4C File Offset: 0x0032D14C
		protected override void OnClearComplete()
		{
			base.OnClearComplete();
			this.CollectionChange();
		}

		// Token: 0x0600E549 RID: 58697 RVA: 0x0032EF5F File Offset: 0x0032D15F
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete(index, value);
			this.CollectionChange();
		}

		// Token: 0x0600E54A RID: 58698 RVA: 0x0032EF74 File Offset: 0x0032D174
		protected override object SaveViewState()
		{
			this.ClearBindableItems();
			return base.SaveViewState();
		}

		// Token: 0x0600E54B RID: 58699 RVA: 0x0032EF84 File Offset: 0x0032D184
		protected override void LoadViewState(object state)
		{
			base.LoadViewState(state);
			foreach (LabelItem labelItem in this)
			{
				labelItem.Parent = this;
				labelItem.Container = (IContainer)this.Parent;
			}
		}

		// Token: 0x04004201 RID: 16897
		private object labelsCollectionParent;

		// Token: 0x04004202 RID: 16898
		internal CollectionChange CollectionChange = delegate()
		{
		};
	}
}
