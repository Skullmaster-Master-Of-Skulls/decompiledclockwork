using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Data
{
	// Token: 0x02000090 RID: 144
	public class InternalDataCollectionBase : ICollection, IEnumerable
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x000513BC File Offset: 0x000507BC
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				return this.List.Count;
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x000513D4 File Offset: 0x000507D4
		public virtual void CopyTo(Array ar, int index)
		{
			this.List.CopyTo(ar, index);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x000513F0 File Offset: 0x000507F0
		public virtual IEnumerator GetEnumerator()
		{
			return this.List.GetEnumerator();
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x00051408 File Offset: 0x00050808
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x00051418 File Offset: 0x00050818
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00051428 File Offset: 0x00050828
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

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x0005146C File Offset: 0x0005086C
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0005147C File Offset: 0x0005087C
		protected virtual ArrayList List
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040002B5 RID: 693
		internal static CollectionChangeEventArgs RefreshEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null);
	}
}
