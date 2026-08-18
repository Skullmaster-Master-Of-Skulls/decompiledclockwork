using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x020004AD RID: 1197
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HtmlTableCellCollection : ICollection, IEnumerable
	{
		// Token: 0x06003810 RID: 14352 RVA: 0x000EFAB7 File Offset: 0x000EEAB7
		internal HtmlTableCellCollection(HtmlTableRow owner)
		{
			this.owner = owner;
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06003811 RID: 14353 RVA: 0x000EFAC6 File Offset: 0x000EEAC6
		public int Count
		{
			get
			{
				if (this.owner.HasControls())
				{
					return this.owner.Controls.Count;
				}
				return 0;
			}
		}

		// Token: 0x17000C8D RID: 3213
		public HtmlTableCell this[int index]
		{
			get
			{
				return (HtmlTableCell)this.owner.Controls[index];
			}
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x000EFAFF File Offset: 0x000EEAFF
		public void Add(HtmlTableCell cell)
		{
			this.Insert(-1, cell);
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x000EFB09 File Offset: 0x000EEB09
		public void Insert(int index, HtmlTableCell cell)
		{
			this.owner.Controls.AddAt(index, cell);
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000EFB1D File Offset: 0x000EEB1D
		public void Clear()
		{
			if (this.owner.HasControls())
			{
				this.owner.Controls.Clear();
			}
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x000EFB3C File Offset: 0x000EEB3C
		public IEnumerator GetEnumerator()
		{
			return this.owner.Controls.GetEnumerator();
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x000EFB50 File Offset: 0x000EEB50
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06003818 RID: 14360 RVA: 0x000EFB80 File Offset: 0x000EEB80
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06003819 RID: 14361 RVA: 0x000EFB83 File Offset: 0x000EEB83
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x0600381A RID: 14362 RVA: 0x000EFB86 File Offset: 0x000EEB86
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x000EFB89 File Offset: 0x000EEB89
		public void Remove(HtmlTableCell cell)
		{
			this.owner.Controls.Remove(cell);
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x000EFB9C File Offset: 0x000EEB9C
		public void RemoveAt(int index)
		{
			this.owner.Controls.RemoveAt(index);
		}

		// Token: 0x040025D9 RID: 9689
		private HtmlTableRow owner;
	}
}
