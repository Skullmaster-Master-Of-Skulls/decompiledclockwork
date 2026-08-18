using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.Web.UI.Calendar.Collections
{
	// Token: 0x02000FE5 RID: 4069
	[ToolboxItem(false)]
	public class CalendarDayTemplateCollection : CollectionBase
	{
		// Token: 0x1700320A RID: 12810
		public DayTemplate this[object obj]
		{
			get
			{
				int count = this.Count;
				return (DayTemplate)base.List[this.IndexOf(obj)];
			}
			set
			{
				base.List[this.IndexOf(obj)] = value;
			}
		}

		// Token: 0x06009E59 RID: 40537 RVA: 0x00234A6A File Offset: 0x00232C6A
		public void Add(DayTemplate DayTemplate)
		{
			base.List.Insert(base.List.Count, DayTemplate);
		}

		// Token: 0x06009E5A RID: 40538 RVA: 0x00234A84 File Offset: 0x00232C84
		public void AddRange(params DayTemplate[] DayTemplates)
		{
			foreach (DayTemplate value in DayTemplates)
			{
				base.List.Add(value);
			}
		}

		// Token: 0x06009E5B RID: 40539 RVA: 0x00234AB2 File Offset: 0x00232CB2
		public new void Clear()
		{
			base.List.Clear();
		}

		// Token: 0x06009E5C RID: 40540 RVA: 0x00234ABF File Offset: 0x00232CBF
		public bool Contains(DayTemplate DayTemplate)
		{
			return base.List.IndexOf(DayTemplate) == -1;
		}

		// Token: 0x1700320B RID: 12811
		// (get) Token: 0x06009E5D RID: 40541 RVA: 0x00234AD3 File Offset: 0x00232CD3
		public new int Count
		{
			get
			{
				return base.List.Count;
			}
		}

		// Token: 0x06009E5E RID: 40542 RVA: 0x00234AE0 File Offset: 0x00232CE0
		public int IndexOf(object obj)
		{
			if (obj is int)
			{
				return (int)obj;
			}
			if (obj is string)
			{
				for (int i = 0; i < base.List.Count; i++)
				{
					if (((DayTemplate)base.List[i]).ID == obj.ToString())
					{
						return i;
					}
				}
				return -1;
			}
			throw new ArgumentException("You may use only a string or an integer as index in the DayTemplates collection.");
		}

		// Token: 0x06009E5F RID: 40543 RVA: 0x00234B4B File Offset: 0x00232D4B
		internal void Insert(int index, DayTemplate DayTemplate)
		{
			base.List.Insert(index, DayTemplate);
		}

		// Token: 0x06009E60 RID: 40544 RVA: 0x00234B5A File Offset: 0x00232D5A
		public void Remove(DayTemplate DayTemplate)
		{
			base.List.Remove(DayTemplate);
		}

		// Token: 0x06009E61 RID: 40545 RVA: 0x00234B68 File Offset: 0x00232D68
		public new void RemoveAt(int index)
		{
			base.List.RemoveAt(index);
		}
	}
}
