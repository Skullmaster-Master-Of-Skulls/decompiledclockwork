using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Data
{
	// Token: 0x0200005A RID: 90
	public class InternalDataCollectionBase : ICollection, IEnumerable
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x001E7558 File Offset: 0x001E6958
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				return this.List.Count;
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x001E7578 File Offset: 0x001E6978
		public virtual void CopyTo(Array ar, int index)
		{
			this.List.CopyTo(ar, index);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x001E7598 File Offset: 0x001E6998
		public virtual IEnumerator GetEnumerator()
		{
			return this.List.GetEnumerator();
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x001E75B8 File Offset: 0x001E69B8
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x001E75C8 File Offset: 0x001E69C8
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x001E75D8 File Offset: 0x001E69D8
		internal int NamesEqual(string s1, string s2, bool fCaseSensitive, CultureInfo locale)
		{
			if (fCaseSensitive)
			{
				if (string.Compare(s1, s2, false, locale) == 0)
				{
					return 1;
				}
				return 0;
			}
			else
			{
				if (locale.CompareInfo.Compare(s1, s2, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) != 0)
				{
					return 0;
				}
				if (string.Compare(s1, s2, false, locale) == 0)
				{
					return 1;
				}
				return -1;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x001E7628 File Offset: 0x001E6A28
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x001E7638 File Offset: 0x001E6A38
		protected virtual ArrayList List
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040006B6 RID: 1718
		internal static CollectionChangeEventArgs RefreshEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null);
	}
}
