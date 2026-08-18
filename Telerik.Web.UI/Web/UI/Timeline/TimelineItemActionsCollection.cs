using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI.Timeline
{
	// Token: 0x02000924 RID: 2340
	public class TimelineItemActionsCollection : BaseCollection<TimelineItemAction, TimelineItem>, IList<TimelineItemAction>, ICollection<TimelineItemAction>, IEnumerable<TimelineItemAction>, IEnumerable
	{
		// Token: 0x060058E0 RID: 22752 RVA: 0x0010F1C8 File Offset: 0x0010D3C8
		public TimelineItemActionsCollection()
		{
		}

		// Token: 0x060058E1 RID: 22753 RVA: 0x0010F1D0 File Offset: 0x0010D3D0
		public TimelineItemActionsCollection(TimelineItem owner) : base(owner)
		{
		}

		// Token: 0x060058E2 RID: 22754 RVA: 0x0010F1DC File Offset: 0x0010D3DC
		protected void SetOwner(TimelineItemAction item)
		{
			TimelineItem owner = item.Owner;
			if (owner != null && owner.Actions.Contains(item) && owner != base.Owner)
			{
				owner.Actions.Remove(item);
			}
			item.Owner = base.Owner;
		}

		// Token: 0x060058E3 RID: 22755 RVA: 0x0010F224 File Offset: 0x0010D424
		public override void AddRange(IEnumerable<TimelineItemAction> items)
		{
			foreach (TimelineItemAction entity in items)
			{
				this.Add(entity);
			}
		}

		// Token: 0x060058E4 RID: 22756 RVA: 0x0010F26C File Offset: 0x0010D46C
		public override void Add(TimelineItemAction item)
		{
			base.Add(item);
			this.SetOwner(item);
		}

		// Token: 0x060058E5 RID: 22757 RVA: 0x0010F27C File Offset: 0x0010D47C
		protected internal virtual IList<TimelineItemAction> ToList()
		{
			List<TimelineItemAction> list = new List<TimelineItemAction>();
			foreach (TimelineItemAction item in this)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060058E6 RID: 22758 RVA: 0x0010F2CC File Offset: 0x0010D4CC
		public void RemoveAt(int index)
		{
			this.Remove(base[index]);
		}

		// Token: 0x060058E7 RID: 22759 RVA: 0x0010F2DB File Offset: 0x0010D4DB
		bool ICollection<TimelineItemAction>.Contains(TimelineItemAction item)
		{
			return base.Contains(item);
		}

		// Token: 0x060058E8 RID: 22760 RVA: 0x0010F2E4 File Offset: 0x0010D4E4
		void ICollection<TimelineItemAction>.CopyTo(TimelineItemAction[] array, int arrayIndex)
		{
			base.CopyTo(array, arrayIndex);
		}

		// Token: 0x060058E9 RID: 22761 RVA: 0x0010F2EE File Offset: 0x0010D4EE
		bool ICollection<TimelineItemAction>.Remove(TimelineItemAction item)
		{
			this.Remove(item);
			return true;
		}

		// Token: 0x17001D58 RID: 7512
		// (get) Token: 0x060058EA RID: 22762 RVA: 0x0010F2F8 File Offset: 0x0010D4F8
		bool ICollection<TimelineItemAction>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060058EB RID: 22763 RVA: 0x0010F2FB File Offset: 0x0010D4FB
		int IList<TimelineItemAction>.IndexOf(TimelineItemAction item)
		{
			return base.IndexOf(item);
		}

		// Token: 0x060058EC RID: 22764 RVA: 0x0010F304 File Offset: 0x0010D504
		void IList<TimelineItemAction>.Insert(int index, TimelineItemAction item)
		{
			this.Insert(index, item);
		}

		// Token: 0x060058ED RID: 22765 RVA: 0x0010F30E File Offset: 0x0010D50E
		void IList<TimelineItemAction>.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x060058EE RID: 22766 RVA: 0x0010F444 File Offset: 0x0010D644
		IEnumerator<TimelineItemAction> IEnumerable<TimelineItemAction>.GetEnumerator()
		{
			foreach (TimelineItemAction item in this)
			{
				yield return item;
			}
			yield break;
		}

		// Token: 0x060058EF RID: 22767 RVA: 0x0010F460 File Offset: 0x0010D660
		public TimelineItemAction FindChild(Predicate<TimelineItemAction> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			foreach (TimelineItemAction timelineItemAction in this)
			{
				if (match(timelineItemAction))
				{
					return timelineItemAction;
				}
			}
			return null;
		}

		// Token: 0x060058F0 RID: 22768 RVA: 0x0010F4C0 File Offset: 0x0010D6C0
		void ICollection<TimelineItemAction>.Clear()
		{
			base.Clear();
		}

		// Token: 0x060058F1 RID: 22769 RVA: 0x0010F4C8 File Offset: 0x0010D6C8
		int ICollection<TimelineItemAction>.get_Count()
		{
			return base.Count;
		}
	}
}
