using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000234 RID: 564
	public sealed class DpiChangedEventArgs : CancelEventArgs
	{
		// Token: 0x06002498 RID: 9368 RVA: 0x000ACA9C File Offset: 0x000AAC9C
		internal DpiChangedEventArgs(int old, Message m)
		{
			this.DeviceDpiOld = old;
			this.DeviceDpiNew = NativeMethods.Util.SignedLOWORD(m.WParam);
			NativeMethods.RECT rect = (NativeMethods.RECT)UnsafeNativeMethods.PtrToStructure(m.LParam, typeof(NativeMethods.RECT));
			this.SuggestedRectangle = Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002499 RID: 9369 RVA: 0x000ACB07 File Offset: 0x000AAD07
		// (set) Token: 0x0600249A RID: 9370 RVA: 0x000ACB0F File Offset: 0x000AAD0F
		public int DeviceDpiOld { get; private set; }

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x0600249B RID: 9371 RVA: 0x000ACB18 File Offset: 0x000AAD18
		// (set) Token: 0x0600249C RID: 9372 RVA: 0x000ACB20 File Offset: 0x000AAD20
		public int DeviceDpiNew { get; private set; }

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x0600249D RID: 9373 RVA: 0x000ACB29 File Offset: 0x000AAD29
		// (set) Token: 0x0600249E RID: 9374 RVA: 0x000ACB31 File Offset: 0x000AAD31
		public Rectangle SuggestedRectangle { get; private set; }

		// Token: 0x0600249F RID: 9375 RVA: 0x000ACB3A File Offset: 0x000AAD3A
		public override string ToString()
		{
			return string.Format("was: {0}, now: {1}", this.DeviceDpiOld, this.DeviceDpiNew);
		}
	}
}
