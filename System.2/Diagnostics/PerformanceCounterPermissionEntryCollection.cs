using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x020004ED RID: 1261
	[Serializable]
	public class PerformanceCounterPermissionEntryCollection : CollectionBase
	{
		// Token: 0x06002F9B RID: 12187 RVA: 0x000D72F0 File Offset: 0x000D54F0
		internal PerformanceCounterPermissionEntryCollection(PerformanceCounterPermission owner, ResourcePermissionBaseEntry[] entries)
		{
			this.owner = owner;
			for (int i = 0; i < entries.Length; i++)
			{
				base.InnerList.Add(new PerformanceCounterPermissionEntry(entries[i]));
			}
		}

		// Token: 0x17000B90 RID: 2960
		public PerformanceCounterPermissionEntry this[int index]
		{
			get
			{
				return (PerformanceCounterPermissionEntry)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x000D734E File Offset: 0x000D554E
		public int Add(PerformanceCounterPermissionEntry value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x000D735C File Offset: 0x000D555C
		public void AddRange(PerformanceCounterPermissionEntry[] value)
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

		// Token: 0x06002FA0 RID: 12192 RVA: 0x000D7390 File Offset: 0x000D5590
		public void AddRange(PerformanceCounterPermissionEntryCollection value)
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

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000D73CC File Offset: 0x000D55CC
		public bool Contains(PerformanceCounterPermissionEntry value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x000D73DA File Offset: 0x000D55DA
		public void CopyTo(PerformanceCounterPermissionEntry[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x000D73E9 File Offset: 0x000D55E9
		public int IndexOf(PerformanceCounterPermissionEntry value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000D73F7 File Offset: 0x000D55F7
		public void Insert(int index, PerformanceCounterPermissionEntry value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000D7406 File Offset: 0x000D5606
		public void Remove(PerformanceCounterPermissionEntry value)
		{
			base.List.Remove(value);
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x000D7414 File Offset: 0x000D5614
		protected override void OnClear()
		{
			this.owner.Clear();
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000D7421 File Offset: 0x000D5621
		protected override void OnInsert(int index, object value)
		{
			this.owner.AddPermissionAccess((PerformanceCounterPermissionEntry)value);
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000D7434 File Offset: 0x000D5634
		protected override void OnRemove(int index, object value)
		{
			this.owner.RemovePermissionAccess((PerformanceCounterPermissionEntry)value);
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000D7447 File Offset: 0x000D5647
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.owner.RemovePermissionAccess((PerformanceCounterPermissionEntry)oldValue);
			this.owner.AddPermissionAccess((PerformanceCounterPermissionEntry)newValue);
		}

		// Token: 0x04002815 RID: 10261
		private PerformanceCounterPermission owner;
	}
}
