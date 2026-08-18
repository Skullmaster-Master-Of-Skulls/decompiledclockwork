using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x020004D4 RID: 1236
	[Serializable]
	public class EventLogPermissionEntryCollection : CollectionBase
	{
		// Token: 0x06002E8D RID: 11917 RVA: 0x000D1EF0 File Offset: 0x000D00F0
		internal EventLogPermissionEntryCollection(EventLogPermission owner, ResourcePermissionBaseEntry[] entries)
		{
			this.owner = owner;
			for (int i = 0; i < entries.Length; i++)
			{
				base.InnerList.Add(new EventLogPermissionEntry(entries[i]));
			}
		}

		// Token: 0x17000B44 RID: 2884
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

		// Token: 0x06002E90 RID: 11920 RVA: 0x000D1F4E File Offset: 0x000D014E
		public int Add(EventLogPermissionEntry value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x000D1F5C File Offset: 0x000D015C
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

		// Token: 0x06002E92 RID: 11922 RVA: 0x000D1F90 File Offset: 0x000D0190
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

		// Token: 0x06002E93 RID: 11923 RVA: 0x000D1FCC File Offset: 0x000D01CC
		public bool Contains(EventLogPermissionEntry value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x000D1FDA File Offset: 0x000D01DA
		public void CopyTo(EventLogPermissionEntry[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x000D1FE9 File Offset: 0x000D01E9
		public int IndexOf(EventLogPermissionEntry value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x000D1FF7 File Offset: 0x000D01F7
		public void Insert(int index, EventLogPermissionEntry value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x000D2006 File Offset: 0x000D0206
		public void Remove(EventLogPermissionEntry value)
		{
			base.List.Remove(value);
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000D2014 File Offset: 0x000D0214
		protected override void OnClear()
		{
			this.owner.Clear();
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x000D2021 File Offset: 0x000D0221
		protected override void OnInsert(int index, object value)
		{
			this.owner.AddPermissionAccess((EventLogPermissionEntry)value);
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000D2034 File Offset: 0x000D0234
		protected override void OnRemove(int index, object value)
		{
			this.owner.RemovePermissionAccess((EventLogPermissionEntry)value);
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x000D2047 File Offset: 0x000D0247
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.owner.RemovePermissionAccess((EventLogPermissionEntry)oldValue);
			this.owner.AddPermissionAccess((EventLogPermissionEntry)newValue);
		}

		// Token: 0x04002783 RID: 10115
		private EventLogPermission owner;
	}
}
