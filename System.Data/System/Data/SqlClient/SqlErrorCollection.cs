using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data.SqlClient
{
	// Token: 0x020002F3 RID: 755
	[ListBindable(false)]
	[Serializable]
	public sealed class SqlErrorCollection : ICollection, IEnumerable
	{
		// Token: 0x06002721 RID: 10017 RVA: 0x002AA0B8 File Offset: 0x002A94B8
		internal SqlErrorCollection()
		{
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x002AA0D8 File Offset: 0x002A94D8
		public void CopyTo(Array array, int index)
		{
			this.errors.CopyTo(array, index);
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x002AA0F8 File Offset: 0x002A94F8
		public void CopyTo(SqlError[] array, int index)
		{
			this.errors.CopyTo(array, index);
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x002AA118 File Offset: 0x002A9518
		public int Count
		{
			get
			{
				return this.errors.Count;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06002725 RID: 10021 RVA: 0x002AA138 File Offset: 0x002A9538
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06002726 RID: 10022 RVA: 0x002AA148 File Offset: 0x002A9548
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700063B RID: 1595
		public SqlError this[int index]
		{
			get
			{
				return (SqlError)this.errors[index];
			}
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x002AA178 File Offset: 0x002A9578
		public IEnumerator GetEnumerator()
		{
			return this.errors.GetEnumerator();
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x002AA198 File Offset: 0x002A9598
		internal void Add(SqlError error)
		{
			this.errors.Add(error);
		}

		// Token: 0x040018F1 RID: 6385
		private ArrayList errors = new ArrayList();
	}
}
