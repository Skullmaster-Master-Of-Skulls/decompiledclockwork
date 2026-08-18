using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000572 RID: 1394
	public class RadListBoxCheckAllCheckEventArgs : EventArgs
	{
		// Token: 0x06003291 RID: 12945 RVA: 0x000A61BD File Offset: 0x000A43BD
		public RadListBoxCheckAllCheckEventArgs(bool checkAllChecked)
		{
			this._checkAllChecked = checkAllChecked;
		}

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x06003292 RID: 12946 RVA: 0x000A61CC File Offset: 0x000A43CC
		public bool CheckAllChecked
		{
			get
			{
				return this._checkAllChecked;
			}
		}

		// Token: 0x04000DD5 RID: 3541
		private readonly bool _checkAllChecked;
	}
}
