using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000660 RID: 1632
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntryEnumerator : IEnumerator
	{
		// Token: 0x06003AE4 RID: 15076 RVA: 0x000C6CB2 File Offset: 0x000C5CB2
		private KeyContainerPermissionAccessEntryEnumerator()
		{
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x000C6CBA File Offset: 0x000C5CBA
		internal KeyContainerPermissionAccessEntryEnumerator(KeyContainerPermissionAccessEntryCollection entries)
		{
			this.m_entries = entries;
			this.m_current = -1;
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06003AE6 RID: 15078 RVA: 0x000C6CD0 File Offset: 0x000C5CD0
		public KeyContainerPermissionAccessEntry Current
		{
			get
			{
				return this.m_entries[this.m_current];
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06003AE7 RID: 15079 RVA: 0x000C6CE3 File Offset: 0x000C5CE3
		object IEnumerator.Current
		{
			get
			{
				return this.m_entries[this.m_current];
			}
		}

		// Token: 0x06003AE8 RID: 15080 RVA: 0x000C6CF6 File Offset: 0x000C5CF6
		public bool MoveNext()
		{
			if (this.m_current == this.m_entries.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x000C6D1E File Offset: 0x000C5D1E
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x04001E82 RID: 7810
		private KeyContainerPermissionAccessEntryCollection m_entries;

		// Token: 0x04001E83 RID: 7811
		private int m_current;
	}
}
