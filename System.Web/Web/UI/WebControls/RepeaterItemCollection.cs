using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200062F RID: 1583
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class RepeaterItemCollection : ICollection, IEnumerable
	{
		// Token: 0x06004E6B RID: 20075 RVA: 0x0013D49E File Offset: 0x0013C49E
		public RepeaterItemCollection(ArrayList items)
		{
			this.items = items;
		}

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x06004E6C RID: 20076 RVA: 0x0013D4AD File Offset: 0x0013C4AD
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x06004E6D RID: 20077 RVA: 0x0013D4BA File Offset: 0x0013C4BA
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x06004E6E RID: 20078 RVA: 0x0013D4BD File Offset: 0x0013C4BD
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x06004E6F RID: 20079 RVA: 0x0013D4C0 File Offset: 0x0013C4C0
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170013D5 RID: 5077
		public RepeaterItem this[int index]
		{
			get
			{
				return (RepeaterItem)this.items[index];
			}
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x0013D4D8 File Offset: 0x0013C4D8
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x0013D508 File Offset: 0x0013C508
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x04002C98 RID: 11416
		private ArrayList items;
	}
}
