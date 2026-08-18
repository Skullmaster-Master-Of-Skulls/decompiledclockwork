using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003CB RID: 971
	public sealed class DataKeyCollection : ICollection, IEnumerable
	{
		// Token: 0x06002EB5 RID: 11957 RVA: 0x00098D42 File Offset: 0x00096F42
		public DataKeyCollection(ArrayList keys)
		{
			this.keys = keys;
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x00098D51 File Offset: 0x00096F51
		public int Count
		{
			get
			{
				return this.keys.Count;
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06002EB7 RID: 11959 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06002EB8 RID: 11960 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06002EB9 RID: 11961 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000D61 RID: 3425
		public object this[int index]
		{
			get
			{
				return this.keys[index];
			}
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x00098D6C File Offset: 0x00096F6C
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x00098D9C File Offset: 0x00096F9C
		public IEnumerator GetEnumerator()
		{
			return this.keys.GetEnumerator();
		}

		// Token: 0x04002003 RID: 8195
		private ArrayList keys;
	}
}
