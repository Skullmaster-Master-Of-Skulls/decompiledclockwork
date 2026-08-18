using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003C4 RID: 964
	public class DataGridPageChangedEventArgs : EventArgs
	{
		// Token: 0x06002E79 RID: 11897 RVA: 0x0009833D File Offset: 0x0009653D
		public DataGridPageChangedEventArgs(object commandSource, int newPageIndex)
		{
			this.commandSource = commandSource;
			this.newPageIndex = newPageIndex;
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06002E7A RID: 11898 RVA: 0x00098353 File Offset: 0x00096553
		public object CommandSource
		{
			get
			{
				return this.commandSource;
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06002E7B RID: 11899 RVA: 0x0009835B File Offset: 0x0009655B
		public int NewPageIndex
		{
			get
			{
				return this.newPageIndex;
			}
		}

		// Token: 0x04001FF3 RID: 8179
		private object commandSource;

		// Token: 0x04001FF4 RID: 8180
		private int newPageIndex;
	}
}
