using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000137 RID: 311
	public class RadComboBoxCheckAllCheckEventArgs : EventArgs
	{
		// Token: 0x06000CD4 RID: 3284 RVA: 0x0002DD9C File Offset: 0x0002BF9C
		public RadComboBoxCheckAllCheckEventArgs(bool checkAllChecked)
		{
			this._checkAllChecked = checkAllChecked;
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0002DDAB File Offset: 0x0002BFAB
		public bool CheckAllChecked
		{
			get
			{
				return this._checkAllChecked;
			}
		}

		// Token: 0x0400031D RID: 797
		private readonly bool _checkAllChecked;
	}
}
