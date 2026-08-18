using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x02000773 RID: 1907
	[Serializable]
	public class PerformanceCounterPermissionEntryCollection : CollectionBase
	{
		// Token: 0x06003AB8 RID: 15032 RVA: 0x000F9AB4 File Offset: 0x000F8AB4
		internal PerformanceCounterPermissionEntryCollection(PerformanceCounterPermission owner, ResourcePermissionBaseEntry[] entries)
		{
			this.owner = owner;
			for (int i = 0; i < entries.Length; i++)
			{
				base.InnerList.Add(new PerformanceCounterPermissionEntry(entries[i]));
			}
		}

		// Token: 0x17000DB1 RID: 3505
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

		// Token: 0x06003ABB RID: 15035 RVA: 0x000F9B12 File Offset: 0x000F8B12
		public int Add(PerformanceCounterPermissionEntry value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x000F9B20 File Offset: 0x000F8B20
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

		// Token: 0x06003ABD RID: 15037 RVA: 0x000F9B54 File Offset: 0x000F8B54
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

		// Token: 0x06003ABE RID: 15038 RVA: 0x000F9B90 File Offset: 0x000F8B90
		public bool Contains(PerformanceCounterPermissionEntry value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06003ABF RID: 15039 RVA: 0x000F9B9E File Offset: 0x000F8B9E
		public void CopyTo(PerformanceCounterPermissionEntry[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06003AC0 RID: 15040 RVA: 0x000F9BAD File Offset: 0x000F8BAD
		public int IndexOf(PerformanceCounterPermissionEntry value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x000F9BBB File Offset: 0x000F8BBB
		public void Insert(int index, PerformanceCounterPermissionEntry value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x000F9BCA File Offset: 0x000F8BCA
		public void Remove(PerformanceCounterPermissionEntry value)
		{
			base.List.Remove(value);
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x000F9BD8 File Offset: 0x000F8BD8
		protected override void OnClear()
		{
			this.owner.Clear();
		}

		// Token: 0x06003AC4 RID: 15044 RVA: 0x000F9BE5 File Offset: 0x000F8BE5
		protected override void OnInsert(int index, object value)
		{
			this.owner.AddPermissionAccess((PerformanceCounterPermissionEntry)value);
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x000F9BF8 File Offset: 0x000F8BF8
		protected override void OnRemove(int index, object value)
		{
			this.owner.RemovePermissionAccess((PerformanceCounterPermissionEntry)value);
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x000F9C0B File Offset: 0x000F8C0B
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.owner.RemovePermissionAccess((PerformanceCounterPermissionEntry)oldValue);
			this.owner.AddPermissionAccess((PerformanceCounterPermissionEntry)newValue);
		}

		// Token: 0x04003369 RID: 13161
		private PerformanceCounterPermission owner;
	}
}
