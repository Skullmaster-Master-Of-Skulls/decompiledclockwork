using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020002E9 RID: 745
	public class MaskInputRejectedEventArgs : EventArgs
	{
		// Token: 0x06002F63 RID: 12131 RVA: 0x000D5B76 File Offset: 0x000D3D76
		public MaskInputRejectedEventArgs(int position, MaskedTextResultHint rejectionHint)
		{
			this.position = position;
			this.hint = rejectionHint;
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06002F64 RID: 12132 RVA: 0x000D5B8C File Offset: 0x000D3D8C
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06002F65 RID: 12133 RVA: 0x000D5B94 File Offset: 0x000D3D94
		public MaskedTextResultHint RejectionHint
		{
			get
			{
				return this.hint;
			}
		}

		// Token: 0x04001397 RID: 5015
		private int position;

		// Token: 0x04001398 RID: 5016
		private MaskedTextResultHint hint;
	}
}
