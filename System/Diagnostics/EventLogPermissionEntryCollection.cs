using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x0200075A RID: 1882
	[Serializable]
	public class EventLogPermissionEntryCollection : CollectionBase
	{
		// Token: 0x060039AD RID: 14765 RVA: 0x000F4A34 File Offset: 0x000F3A34
		internal EventLogPermissionEntryCollection(EventLogPermission owner, ResourcePermissionBaseEntry[] entries)
		{
			this.owner = owner;
			for (int i = 0; i < entries.Length; i++)
			{
				base.InnerList.Add(new EventLogPermissionEntry(entries[i]));
			}
		}

		// Token: 0x17000D66 RID: 3430
		public EventLogPermissionEntry this[int index]
		{
			get
			{
				return (EventLogPermissionEntry)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x000F4A92 File Offset: 0x000F3A92
		public int Add(EventLogPermissionEntry value)
		{
			return base.List.Add(value);
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x000F4AA0 File Offset: 0x000F3AA0
		public void AddRange(EventLogPermissionEntry[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x000F4AD4 File Offset: 0x000F3AD4
		public void AddRange(EventLogPermissionEntryCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x000F4B10 File Offset: 0x000F3B10
		public bool Contains(EventLogPermissionEntry value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x000F4B1E File Offset: 0x000F3B1E
		public void CopyTo(EventLogPermissionEntry[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x000F4B2D File Offset: 0x000F3B2D
		public int IndexOf(EventLogPermissionEntry value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x000F4B3B File Offset: 0x000F3B3B
		public void Insert(int index, EventLogPermissionEntry value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x000F4B4A File Offset: 0x000F3B4A
		public void Remove(EventLogPermissionEntry value)
		{
			base.List.Remove(value);
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x000F4B58 File Offset: 0x000F3B58
		protected override void OnClear()
		{
			this.owner.Clear();
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x000F4B65 File Offset: 0x000F3B65
		protected override void OnInsert(int index, object value)
		{
			this.owner.AddPermissionAccess((EventLogPermissionEntry)value);
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x000F4B78 File Offset: 0x000F3B78
		protected override void OnRemove(int index, object value)
		{
			this.owner.RemovePermissionAccess((EventLogPermissionEntry)value);
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x000F4B8B File Offset: 0x000F3B8B
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.owner.RemovePermissionAccess((EventLogPermissionEntry)oldValue);
			this.owner.AddPermissionAccess((EventLogPermissionEntry)newValue);
		}

		// Token: 0x040032DB RID: 13019
		private EventLogPermission owner;
	}
}
