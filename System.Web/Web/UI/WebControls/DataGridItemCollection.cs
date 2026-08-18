using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200053D RID: 1341
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DataGridItemCollection : ICollection, IEnumerable
	{
		// Token: 0x06004223 RID: 16931 RVA: 0x001121DF File Offset: 0x001111DF
		public DataGridItemCollection(ArrayList items)
		{
			this.items = items;
		}

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06004224 RID: 16932 RVA: 0x001121EE File Offset: 0x001111EE
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06004225 RID: 16933 RVA: 0x001121FB File Offset: 0x001111FB
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06004226 RID: 16934 RVA: 0x001121FE File Offset: 0x001111FE
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06004227 RID: 16935 RVA: 0x00112201 File Offset: 0x00111201
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000FFB RID: 4091
		public DataGridItem this[int index]
		{
			get
			{
				return (DataGridItem)this.items[index];
			}
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x00112218 File Offset: 0x00111218
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x00112248 File Offset: 0x00111248
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x040028F4 RID: 10484
		private ArrayList items;
	}
}
