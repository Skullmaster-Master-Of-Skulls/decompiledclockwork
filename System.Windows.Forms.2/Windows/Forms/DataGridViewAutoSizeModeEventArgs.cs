using System;

namespace System.Windows.Forms
{
	// Token: 0x0200019E RID: 414
	public class DataGridViewAutoSizeModeEventArgs : EventArgs
	{
		// Token: 0x06001CBE RID: 7358 RVA: 0x00086B31 File Offset: 0x00084D31
		public DataGridViewAutoSizeModeEventArgs(bool previousModeAutoSized)
		{
			this.previousModeAutoSized = previousModeAutoSized;
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x00086B40 File Offset: 0x00084D40
		public bool PreviousModeAutoSized
		{
			get
			{
				return this.previousModeAutoSized;
			}
		}

		// Token: 0x04000C75 RID: 3189
		private bool previousModeAutoSized;
	}
}
