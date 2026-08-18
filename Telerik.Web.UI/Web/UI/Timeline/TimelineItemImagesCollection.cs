using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI.Timeline
{
	// Token: 0x02000923 RID: 2339
	public class TimelineItemImagesCollection : BaseCollection<TimelineItemImage, TimelineItem>, IList<TimelineItemImage>, ICollection<TimelineItemImage>, IEnumerable<TimelineItemImage>, IEnumerable
	{
		// Token: 0x060058CE RID: 22734 RVA: 0x0010EEB1 File Offset: 0x0010D0B1
		public TimelineItemImagesCollection()
		{
		}

		// Token: 0x060058CF RID: 22735 RVA: 0x0010EEB9 File Offset: 0x0010D0B9
		public TimelineItemImagesCollection(TimelineItem owner) : base(owner)
		{
		}

		// Token: 0x060058D0 RID: 22736 RVA: 0x0010EEC4 File Offset: 0x0010D0C4
		protected void SetOwner(TimelineItemImage item)
		{
			TimelineItem owner = item.Owner;
			if (owner != null && owner.Images.Contains(item) && owner != base.Owner)
			{
				owner.Images.Remove(item);
			}
			item.Owner = base.Owner;
		}

		// Token: 0x060058D1 RID: 22737 RVA: 0x0010EF0C File Offset: 0x0010D10C
		public override void AddRange(IEnumerable<TimelineItemImage> items)
		{
			foreach (TimelineItemImage entity in items)
			{
				this.Add(entity);
			}
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x0010EF54 File Offset: 0x0010D154
		public override void Add(TimelineItemImage item)
		{
			base.Add(item);
			if (base.Owner != null)
			{
				item.Owner = base.Owner;
			}
		}

		// Token: 0x060058D3 RID: 22739 RVA: 0x0010EF74 File Offset: 0x0010D174
		protected internal virtual IList<TimelineItemImage> ToList()
		{
			List<TimelineItemImage> list = new List<TimelineItemImage>();
			foreach (TimelineItemImage item in this)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060058D4 RID: 22740 RVA: 0x0010EFC4 File Offset: 0x0010D1C4
		public void RemoveAt(int index)
		{
			this.Remove(base[index]);
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x0010EFD3 File Offset: 0x0010D1D3
		bool ICollection<TimelineItemImage>.Contains(TimelineItemImage item)
		{
			return base.Contains(item);
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x0010EFDC File Offset: 0x0010D1DC
		void ICollection<TimelineItemImage>.CopyTo(TimelineItemImage[] array, int arrayIndex)
		{
			base.CopyTo(array, arrayIndex);
		}

		// Token: 0x060058D7 RID: 22743 RVA: 0x0010EFE6 File Offset: 0x0010D1E6
		bool ICollection<TimelineItemImage>.Remove(TimelineItemImage item)
		{
			this.Remove(item);
			return true;
		}

		// Token: 0x17001D57 RID: 7511
		// (get) Token: 0x060058D8 RID: 22744 RVA: 0x0010EFF0 File Offset: 0x0010D1F0
		bool ICollection<TimelineItemImage>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060058D9 RID: 22745 RVA: 0x0010EFF3 File Offset: 0x0010D1F3
		int IList<TimelineItemImage>.IndexOf(TimelineItemImage item)
		{
			return base.IndexOf(item);
		}

		// Token: 0x060058DA RID: 22746 RVA: 0x0010EFFC File Offset: 0x0010D1FC
		void IList<TimelineItemImage>.Insert(int index, TimelineItemImage item)
		{
			this.Insert(index, item);
		}

		// Token: 0x060058DB RID: 22747 RVA: 0x0010F006 File Offset: 0x0010D206
		void IList<TimelineItemImage>.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x060058DC RID: 22748 RVA: 0x0010F13C File Offset: 0x0010D33C
		IEnumerator<TimelineItemImage> IEnumerable<TimelineItemImage>.GetEnumerator()
		{
			foreach (TimelineItemImage item in this)
			{
				yield return item;
			}
			yield break;
		}

		// Token: 0x060058DD RID: 22749 RVA: 0x0010F158 File Offset: 0x0010D358
		public TimelineItemImage FindChild(Predicate<TimelineItemImage> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			foreach (TimelineItemImage timelineItemImage in this)
			{
				if (match(timelineItemImage))
				{
					return timelineItemImage;
				}
			}
			return null;
		}

		// Token: 0x060058DE RID: 22750 RVA: 0x0010F1B8 File Offset: 0x0010D3B8
		void ICollection<TimelineItemImage>.Clear()
		{
			base.Clear();
		}

		// Token: 0x060058DF RID: 22751 RVA: 0x0010F1C0 File Offset: 0x0010D3C0
		int ICollection<TimelineItemImage>.get_Count()
		{
			return base.Count;
		}
	}
}
