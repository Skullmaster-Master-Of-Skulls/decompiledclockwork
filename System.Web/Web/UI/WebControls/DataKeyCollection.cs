using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200054A RID: 1354
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataKeyCollection : ICollection, IEnumerable
	{
		// Token: 0x060042AB RID: 17067 RVA: 0x00113F76 File Offset: 0x00112F76
		public DataKeyCollection(ArrayList keys)
		{
			this.keys = keys;
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x060042AC RID: 17068 RVA: 0x00113F85 File Offset: 0x00112F85
		public int Count
		{
			get
			{
				return this.keys.Count;
			}
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x060042AD RID: 17069 RVA: 0x00113F92 File Offset: 0x00112F92
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x060042AE RID: 17070 RVA: 0x00113F95 File Offset: 0x00112F95
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x060042AF RID: 17071 RVA: 0x00113F98 File Offset: 0x00112F98
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700102B RID: 4139
		public object this[int index]
		{
			get
			{
				return this.keys[index];
			}
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x00113FAC File Offset: 0x00112FAC
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x060042B2 RID: 17074 RVA: 0x00113FDC File Offset: 0x00112FDC
		public IEnumerator GetEnumerator()
		{
			return this.keys.GetEnumerator();
		}

		// Token: 0x04002922 RID: 10530
		private ArrayList keys;
	}
}
