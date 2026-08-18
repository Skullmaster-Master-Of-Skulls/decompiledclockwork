using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x020004B0 RID: 1200
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HtmlTableRowCollection : ICollection, IEnumerable
	{
		// Token: 0x06003833 RID: 14387 RVA: 0x000EFEDD File Offset: 0x000EEEDD
		internal HtmlTableRowCollection(HtmlTable owner)
		{
			this.owner = owner;
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06003834 RID: 14388 RVA: 0x000EFEEC File Offset: 0x000EEEEC
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

		// Token: 0x17000C9A RID: 3226
		public HtmlTableRow this[int index]
		{
			get
			{
				return (HtmlTableRow)this.owner.Controls[index];
			}
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x000EFF25 File Offset: 0x000EEF25
		public void Add(HtmlTableRow row)
		{
			this.Insert(-1, row);
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x000EFF2F File Offset: 0x000EEF2F
		public void Insert(int index, HtmlTableRow row)
		{
			this.owner.Controls.AddAt(index, row);
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000EFF43 File Offset: 0x000EEF43
		public void Clear()
		{
			if (this.owner.HasControls())
			{
				this.owner.Controls.Clear();
			}
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000EFF64 File Offset: 0x000EEF64
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x0600383A RID: 14394 RVA: 0x000EFF94 File Offset: 0x000EEF94
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x0600383B RID: 14395 RVA: 0x000EFF97 File Offset: 0x000EEF97
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x0600383C RID: 14396 RVA: 0x000EFF9A File Offset: 0x000EEF9A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x000EFF9D File Offset: 0x000EEF9D
		public IEnumerator GetEnumerator()
		{
			return this.owner.Controls.GetEnumerator();
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x000EFFAF File Offset: 0x000EEFAF
		public void Remove(HtmlTableRow row)
		{
			this.owner.Controls.Remove(row);
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x000EFFC2 File Offset: 0x000EEFC2
		public void RemoveAt(int index)
		{
			this.owner.Controls.RemoveAt(index);
		}

		// Token: 0x040025DB RID: 9691
		private HtmlTable owner;
	}
}
