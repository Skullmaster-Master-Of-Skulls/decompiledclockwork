using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001A0D RID: 6669
	public class RadSchedulerContextMenuCollection : StateManagedCollection
	{
		// Token: 0x0601023F RID: 66111 RVA: 0x0039F7C3 File Offset: 0x0039D9C3
		public RadSchedulerContextMenuCollection(RadScheduler scheduler)
		{
			this._scheduler = scheduler;
		}

		// Token: 0x17004DEE RID: 19950
		public RadSchedulerContextMenu this[int index]
		{
			get
			{
				return (RadSchedulerContextMenu)this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		// Token: 0x17004DEF RID: 19951
		// (get) Token: 0x06010242 RID: 66114 RVA: 0x0039F7F4 File Offset: 0x0039D9F4
		private IList List
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06010243 RID: 66115 RVA: 0x0039F7F7 File Offset: 0x0039D9F7
		public void Add(RadSchedulerContextMenu target)
		{
			this.List.Add(target);
		}

		// Token: 0x06010244 RID: 66116 RVA: 0x0039F808 File Offset: 0x0039DA08
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete(index, value);
			RadSchedulerContextMenu radSchedulerContextMenu = (RadSchedulerContextMenu)value;
			if (string.IsNullOrEmpty(radSchedulerContextMenu.ID))
			{
				radSchedulerContextMenu.ID = radSchedulerContextMenu.GetType().Name + this._scheduler.AppointmentContextMenus.Count;
			}
		}

		// Token: 0x06010245 RID: 66117 RVA: 0x0039F85C File Offset: 0x0039DA5C
		public bool Contains(RadSchedulerContextMenu target)
		{
			return this.List.Contains(target);
		}

		// Token: 0x06010246 RID: 66118 RVA: 0x0039F86C File Offset: 0x0039DA6C
		internal bool ContainsID(string contextMenuID)
		{
			RadSchedulerContextMenu radSchedulerContextMenu = this.FindById(contextMenuID);
			return radSchedulerContextMenu != null;
		}

		// Token: 0x06010247 RID: 66119 RVA: 0x0039F888 File Offset: 0x0039DA88
		internal RadSchedulerContextMenu FindById(string contextMenuID)
		{
			foreach (object obj in this)
			{
				RadSchedulerContextMenu radSchedulerContextMenu = (RadSchedulerContextMenu)obj;
				if (radSchedulerContextMenu.ID == contextMenuID)
				{
					return radSchedulerContextMenu;
				}
			}
			return null;
		}

		// Token: 0x06010248 RID: 66120 RVA: 0x0039F8EC File Offset: 0x0039DAEC
		internal RadSchedulerContextMenu FindByClientId(string contextMenuClientID)
		{
			foreach (object obj in this)
			{
				RadSchedulerContextMenu radSchedulerContextMenu = (RadSchedulerContextMenu)obj;
				if (radSchedulerContextMenu.ClientID == contextMenuClientID)
				{
					return radSchedulerContextMenu;
				}
			}
			return null;
		}

		// Token: 0x06010249 RID: 66121 RVA: 0x0039F950 File Offset: 0x0039DB50
		public void CopyTo(RadSchedulerContextMenu[] array, int index)
		{
			this.List.CopyTo(array, index);
		}

		// Token: 0x0601024A RID: 66122 RVA: 0x0039F960 File Offset: 0x0039DB60
		public void AddRange(IEnumerable<RadSchedulerContextMenu> contextMenus)
		{
			foreach (RadSchedulerContextMenu target in contextMenus)
			{
				this.Add(target);
			}
		}

		// Token: 0x0601024B RID: 66123 RVA: 0x0039F9A8 File Offset: 0x0039DBA8
		public int IndexOf(RadSchedulerContextMenu target)
		{
			return this.List.IndexOf(target);
		}

		// Token: 0x0601024C RID: 66124 RVA: 0x0039F9B6 File Offset: 0x0039DBB6
		public void Insert(int index, RadSchedulerContextMenu target)
		{
			this.List.Insert(index, target);
		}

		// Token: 0x0601024D RID: 66125 RVA: 0x0039F9C5 File Offset: 0x0039DBC5
		public void Remove(RadSchedulerContextMenu target)
		{
			this.List.Remove(target);
		}

		// Token: 0x0601024E RID: 66126 RVA: 0x0039F9D3 File Offset: 0x0039DBD3
		public void RemoveAt(int index)
		{
			this.List.RemoveAt(index);
		}

		// Token: 0x0601024F RID: 66127 RVA: 0x0039F9E1 File Offset: 0x0039DBE1
		protected override void SetDirtyObject(object o)
		{
			((IMarkableStateManager)o).SetDirty();
		}

		// Token: 0x04004911 RID: 18705
		private readonly RadScheduler _scheduler;
	}
}
