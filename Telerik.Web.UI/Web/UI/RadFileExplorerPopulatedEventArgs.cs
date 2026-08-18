using System;
using System.Collections.Generic;
using Telerik.Web.UI.Widgets;

namespace Telerik.Web.UI
{
	// Token: 0x020002E1 RID: 737
	public class RadFileExplorerPopulatedEventArgs : EventArgs
	{
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001995 RID: 6549 RVA: 0x00054740 File Offset: 0x00052940
		// (set) Token: 0x06001996 RID: 6550 RVA: 0x00054748 File Offset: 0x00052948
		public List<FileBrowserItem> List
		{
			get
			{
				return this._list;
			}
			set
			{
				this._list = value;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06001997 RID: 6551 RVA: 0x00054751 File Offset: 0x00052951
		// (set) Token: 0x06001998 RID: 6552 RVA: 0x00054759 File Offset: 0x00052959
		public string SortColumnName { get; set; }

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001999 RID: 6553 RVA: 0x00054762 File Offset: 0x00052962
		// (set) Token: 0x0600199A RID: 6554 RVA: 0x0005476A File Offset: 0x0005296A
		public string SortDirection { get; set; }

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x00054773 File Offset: 0x00052973
		// (set) Token: 0x0600199C RID: 6556 RVA: 0x0005477B File Offset: 0x0005297B
		public string FilterKeyWord { get; set; }

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x00054784 File Offset: 0x00052984
		public string ControlName
		{
			get
			{
				return this.controlName;
			}
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0005478C File Offset: 0x0005298C
		public RadFileExplorerPopulatedEventArgs(List<FileBrowserItem> pList, string _sortColumnName, string _sortDirection, string controlName, string filterKeyWord)
		{
			this._list = pList;
			this.SortColumnName = _sortColumnName;
			this.SortDirection = _sortDirection;
			this.controlName = controlName;
			this.FilterKeyWord = filterKeyWord;
		}

		// Token: 0x0400069C RID: 1692
		private readonly string controlName;

		// Token: 0x0400069D RID: 1693
		private List<FileBrowserItem> _list;
	}
}
