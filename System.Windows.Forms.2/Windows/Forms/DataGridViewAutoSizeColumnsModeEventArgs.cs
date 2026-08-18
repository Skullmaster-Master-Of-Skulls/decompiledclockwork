using System;

namespace System.Windows.Forms
{
	// Token: 0x02000194 RID: 404
	public class DataGridViewAutoSizeColumnsModeEventArgs : EventArgs
	{
		// Token: 0x06001CBC RID: 7356 RVA: 0x00086B1A File Offset: 0x00084D1A
		public DataGridViewAutoSizeColumnsModeEventArgs(DataGridViewAutoSizeColumnMode[] previousModes)
		{
			this.previousModes = previousModes;
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001CBD RID: 7357 RVA: 0x00086B29 File Offset: 0x00084D29
		public DataGridViewAutoSizeColumnMode[] PreviousModes
		{
			get
			{
				return this.previousModes;
			}
		}

		// Token: 0x04000C3D RID: 3133
		private DataGridViewAutoSizeColumnMode[] previousModes;
	}
}
