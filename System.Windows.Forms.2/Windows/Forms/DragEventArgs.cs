using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000238 RID: 568
	[ComVisible(true)]
	public class DragEventArgs : EventArgs
	{
		// Token: 0x060024A4 RID: 9380 RVA: 0x000ACB5C File Offset: 0x000AAD5C
		public DragEventArgs(IDataObject data, int keyState, int x, int y, DragDropEffects allowedEffect, DragDropEffects effect)
		{
			this.data = data;
			this.keyState = keyState;
			this.x = x;
			this.y = y;
			this.allowedEffect = allowedEffect;
			this.effect = effect;
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060024A5 RID: 9381 RVA: 0x000ACB91 File Offset: 0x000AAD91
		public IDataObject Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060024A6 RID: 9382 RVA: 0x000ACB99 File Offset: 0x000AAD99
		public int KeyState
		{
			get
			{
				return this.keyState;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x060024A7 RID: 9383 RVA: 0x000ACBA1 File Offset: 0x000AADA1
		public int X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060024A8 RID: 9384 RVA: 0x000ACBA9 File Offset: 0x000AADA9
		public int Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060024A9 RID: 9385 RVA: 0x000ACBB1 File Offset: 0x000AADB1
		public DragDropEffects AllowedEffect
		{
			get
			{
				return this.allowedEffect;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060024AA RID: 9386 RVA: 0x000ACBB9 File Offset: 0x000AADB9
		// (set) Token: 0x060024AB RID: 9387 RVA: 0x000ACBC1 File Offset: 0x000AADC1
		public DragDropEffects Effect
		{
			get
			{
				return this.effect;
			}
			set
			{
				this.effect = value;
			}
		}

		// Token: 0x04000F23 RID: 3875
		private readonly IDataObject data;

		// Token: 0x04000F24 RID: 3876
		private readonly int keyState;

		// Token: 0x04000F25 RID: 3877
		private readonly int x;

		// Token: 0x04000F26 RID: 3878
		private readonly int y;

		// Token: 0x04000F27 RID: 3879
		private readonly DragDropEffects allowedEffect;

		// Token: 0x04000F28 RID: 3880
		private DragDropEffects effect;
	}
}
