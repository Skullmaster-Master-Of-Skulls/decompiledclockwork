using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data.SqlClient
{
	// Token: 0x020001CE RID: 462
	[ListBindable(false)]
	[Serializable]
	public sealed class SqlErrorCollection : ICollection, IEnumerable
	{
		// Token: 0x06001D1B RID: 7451 RVA: 0x000CE6E0 File Offset: 0x000CDAE0
		internal SqlErrorCollection()
		{
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x000CE700 File Offset: 0x000CDB00
		public void CopyTo(Array array, int index)
		{
			this.errors.CopyTo(array, index);
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x000CE71C File Offset: 0x000CDB1C
		public void CopyTo(SqlError[] array, int index)
		{
			this.errors.CopyTo(array, index);
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x000CE738 File Offset: 0x000CDB38
		public int Count
		{
			get
			{
				return this.errors.Count;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x000CE750 File Offset: 0x000CDB50
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x000CE760 File Offset: 0x000CDB60
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700047A RID: 1146
		public SqlError this[int index]
		{
			get
			{
				return (SqlError)this.errors[index];
			}
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x000CE790 File Offset: 0x000CDB90
		public IEnumerator GetEnumerator()
		{
			return this.errors.GetEnumerator();
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x000CE7A8 File Offset: 0x000CDBA8
		internal void Add(SqlError error)
		{
			this.errors.Add(error);
		}

		// Token: 0x040010B5 RID: 4277
		private ArrayList errors = new ArrayList();
	}
}
