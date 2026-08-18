using System;

namespace System.Windows.Forms
{
	// Token: 0x020001B4 RID: 436
	internal class DataGridViewCellStyleChangedEventArgs : EventArgs
	{
		// Token: 0x06001EAC RID: 7852 RVA: 0x00090A2B File Offset: 0x0008EC2B
		internal DataGridViewCellStyleChangedEventArgs()
		{
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x00090A33 File Offset: 0x0008EC33
		// (set) Token: 0x06001EAE RID: 7854 RVA: 0x00090A3B File Offset: 0x0008EC3B
		internal bool ChangeAffectsPreferredSize
		{
			get
			{
				return this.changeAffectsPreferredSize;
			}
			set
			{
				this.changeAffectsPreferredSize = value;
			}
		}

		// Token: 0x04000CFB RID: 3323
		private bool changeAffectsPreferredSize;
	}
}
