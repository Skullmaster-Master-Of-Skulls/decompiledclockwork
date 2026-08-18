using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001AC6 RID: 6854
	public class RadSliderItemCollection : ControlItemCollection
	{
		// Token: 0x0601096F RID: 67951 RVA: 0x003B356C File Offset: 0x003B176C
		public RadSliderItemCollection(Control parent) : base(parent)
		{
		}

		// Token: 0x06010970 RID: 67952 RVA: 0x003B357C File Offset: 0x003B177C
		internal RadSliderItemCollection(Control parent, bool synchronizeItemsWithControls) : base(parent)
		{
			this.synchronizeItemsWithControls = synchronizeItemsWithControls;
		}

		// Token: 0x06010971 RID: 67953 RVA: 0x003B3593 File Offset: 0x003B1793
		protected override void AddItemToParentControls(int index, ControlItem item)
		{
			if (index >= 0)
			{
				index += 2;
			}
			base.AddItemToParentControls(index, item);
		}

		// Token: 0x06010972 RID: 67954 RVA: 0x003B35A6 File Offset: 0x003B17A6
		protected override void OnInsertComplete(int index, object value)
		{
			if (this.synchronizeItemsWithControls)
			{
				base.OnInsertComplete(index, value);
			}
		}

		// Token: 0x06010973 RID: 67955 RVA: 0x003B35B8 File Offset: 0x003B17B8
		protected override void OnRemoveComplete(int index, object value)
		{
			if (this.synchronizeItemsWithControls)
			{
				base.OnRemoveComplete(index, value);
			}
		}

		// Token: 0x06010974 RID: 67956 RVA: 0x003B35CA File Offset: 0x003B17CA
		protected override void OnClear()
		{
			if (this.synchronizeItemsWithControls)
			{
				base.OnClear();
			}
		}

		// Token: 0x170050AC RID: 20652
		public RadSliderItem this[int index]
		{
			get
			{
				return (RadSliderItem)base[index];
			}
			set
			{
				base[index] = value;
			}
		}

		// Token: 0x06010977 RID: 67959 RVA: 0x003B35F2 File Offset: 0x003B17F2
		public virtual void Add(RadSliderItem item)
		{
			base.Add(item);
		}

		// Token: 0x06010978 RID: 67960 RVA: 0x003B35FB File Offset: 0x003B17FB
		public RadSliderItem FindItemByText(string text)
		{
			return base.FindChildByText<RadSliderItem>(text);
		}

		// Token: 0x06010979 RID: 67961 RVA: 0x003B3604 File Offset: 0x003B1804
		public RadSliderItem FindItemByValue(string value)
		{
			return base.FindChildByValue<RadSliderItem>(value);
		}

		// Token: 0x0601097A RID: 67962 RVA: 0x003B360D File Offset: 0x003B180D
		public RadSliderItem FindItemByAttribute(string attributeName, string attributeValue)
		{
			return base.FindChildByAttribute<RadSliderItem>(attributeName, attributeValue);
		}

		// Token: 0x0601097B RID: 67963 RVA: 0x003B3617 File Offset: 0x003B1817
		public virtual bool Contains(RadSliderItem item)
		{
			return base.Contains(item);
		}

		// Token: 0x0601097C RID: 67964 RVA: 0x003B3620 File Offset: 0x003B1820
		public virtual void AddRange(IEnumerable<RadSliderItem> items)
		{
			IList<ControlItem> list = new List<ControlItem>();
			foreach (RadSliderItem item in items)
			{
				list.Add(item);
			}
			base.AddRange(list);
		}

		// Token: 0x0601097D RID: 67965 RVA: 0x003B3678 File Offset: 0x003B1878
		public virtual int IndexOf(RadSliderItem item)
		{
			return base.IndexOf(item);
		}

		// Token: 0x0601097E RID: 67966 RVA: 0x003B3681 File Offset: 0x003B1881
		public virtual void Insert(int index, RadSliderItem item)
		{
			base.Insert(index, item);
		}

		// Token: 0x0601097F RID: 67967 RVA: 0x003B368B File Offset: 0x003B188B
		public virtual void Remove(RadSliderItem item)
		{
			base.Remove(item);
			item.Owner = null;
		}

		// Token: 0x06010980 RID: 67968 RVA: 0x003B369B File Offset: 0x003B189B
		public virtual void Remove(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x06010981 RID: 67969 RVA: 0x003B36AC File Offset: 0x003B18AC
		protected override void SetOwner(ControlItem item)
		{
			RadSliderItem radSliderItem = item as RadSliderItem;
			RadSlider owner = radSliderItem.Owner;
			if (owner != null && owner.Items.Contains(item) && owner != base.Parent)
			{
				owner.Items.Remove(item);
			}
			radSliderItem.Owner = (RadSlider)base.Parent;
		}

		// Token: 0x04004A24 RID: 18980
		private readonly bool synchronizeItemsWithControls = true;
	}
}
