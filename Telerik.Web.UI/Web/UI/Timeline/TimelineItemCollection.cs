using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI.Timeline
{
	// Token: 0x02000925 RID: 2341
	public class TimelineItemCollection : BaseCollection<TimelineItem, RadTimeline>, IList<TimelineItem>, ICollection<TimelineItem>, IEnumerable<TimelineItem>, IEnumerable
	{
		// Token: 0x060058F2 RID: 22770 RVA: 0x0010F4D0 File Offset: 0x0010D6D0
		public TimelineItemCollection()
		{
		}

		// Token: 0x060058F3 RID: 22771 RVA: 0x0010F4D8 File Offset: 0x0010D6D8
		public TimelineItemCollection(RadTimeline owner) : base(owner)
		{
		}

		// Token: 0x060058F4 RID: 22772 RVA: 0x0010F4E4 File Offset: 0x0010D6E4
		protected void SetOwner(TimelineItem item)
		{
			RadTimeline owner = item.Owner;
			if (owner != null && owner.Items.Contains(item) && owner != base.Owner)
			{
				owner.Items.Remove(item);
			}
			item.Owner = base.Owner;
		}

		// Token: 0x060058F5 RID: 22773 RVA: 0x0010F52C File Offset: 0x0010D72C
		public override void AddRange(IEnumerable<TimelineItem> items)
		{
			base.AddRange(items);
			foreach (TimelineItem timelineItem in items)
			{
				if (base.Owner != null)
				{
					timelineItem.Owner = base.Owner;
				}
			}
		}

		// Token: 0x060058F6 RID: 22774 RVA: 0x0010F588 File Offset: 0x0010D788
		public override void Add(TimelineItem item)
		{
			base.Add(item);
			if (base.Owner != null)
			{
				item.Owner = base.Owner;
			}
		}

		// Token: 0x060058F7 RID: 22775 RVA: 0x0010F5A8 File Offset: 0x0010D7A8
		protected internal virtual IList<TimelineItem> ToList()
		{
			List<TimelineItem> list = new List<TimelineItem>();
			foreach (TimelineItem item in this)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060058F8 RID: 22776 RVA: 0x0010F5F8 File Offset: 0x0010D7F8
		public void RemoveAt(int index)
		{
			this.Remove(base[index]);
		}

		// Token: 0x060058F9 RID: 22777 RVA: 0x0010F607 File Offset: 0x0010D807
		bool ICollection<TimelineItem>.Contains(TimelineItem item)
		{
			return base.Contains(item);
		}

		// Token: 0x060058FA RID: 22778 RVA: 0x0010F610 File Offset: 0x0010D810
		void ICollection<TimelineItem>.CopyTo(TimelineItem[] array, int arrayIndex)
		{
			base.CopyTo(array, arrayIndex);
		}

		// Token: 0x060058FB RID: 22779 RVA: 0x0010F61A File Offset: 0x0010D81A
		bool ICollection<TimelineItem>.Remove(TimelineItem item)
		{
			this.Remove(item);
			return true;
		}

		// Token: 0x17001D59 RID: 7513
		// (get) Token: 0x060058FC RID: 22780 RVA: 0x0010F624 File Offset: 0x0010D824
		bool ICollection<TimelineItem>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060058FD RID: 22781 RVA: 0x0010F627 File Offset: 0x0010D827
		int IList<TimelineItem>.IndexOf(TimelineItem item)
		{
			return base.IndexOf(item);
		}

		// Token: 0x060058FE RID: 22782 RVA: 0x0010F630 File Offset: 0x0010D830
		void IList<TimelineItem>.Insert(int index, TimelineItem item)
		{
			this.Insert(index, item);
		}

		// Token: 0x060058FF RID: 22783 RVA: 0x0010F63A File Offset: 0x0010D83A
		void IList<TimelineItem>.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x06005900 RID: 22784 RVA: 0x0010F770 File Offset: 0x0010D970
		IEnumerator<TimelineItem> IEnumerable<TimelineItem>.GetEnumerator()
		{
			foreach (TimelineItem item in this)
			{
				yield return item;
			}
			yield break;
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x0010F78C File Offset: 0x0010D98C
		public TimelineItem FindChild(Predicate<TimelineItem> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			foreach (TimelineItem timelineItem in this)
			{
				if (match(timelineItem))
				{
					return timelineItem;
				}
			}
			return null;
		}

		// Token: 0x06005902 RID: 22786 RVA: 0x0010F7EC File Offset: 0x0010D9EC
		public TimelineItem FindChildByText(string text)
		{
			return this.FindChildByText(text, false);
		}

		// Token: 0x06005903 RID: 22787 RVA: 0x0010F7F6 File Offset: 0x0010D9F6
		public TimelineItem FindChildByValue(string value)
		{
			return this.FindChildByValue(value, false);
		}

		// Token: 0x06005904 RID: 22788 RVA: 0x0010F800 File Offset: 0x0010DA00
		public TimelineItem FindChildByValue(string value, bool ignoreCase)
		{
			foreach (TimelineItem timelineItem in this)
			{
			}
			return null;
		}

		// Token: 0x06005905 RID: 22789 RVA: 0x0010F844 File Offset: 0x0010DA44
		public TimelineItem FindChildByText(string text, bool ignoreCase)
		{
			foreach (TimelineItem timelineItem in this)
			{
			}
			return null;
		}

		// Token: 0x06005906 RID: 22790 RVA: 0x0010F888 File Offset: 0x0010DA88
		void ICollection<TimelineItem>.Clear()
		{
			base.Clear();
		}

		// Token: 0x06005907 RID: 22791 RVA: 0x0010F890 File Offset: 0x0010DA90
		int ICollection<TimelineItem>.get_Count()
		{
			return base.Count;
		}
	}
}
