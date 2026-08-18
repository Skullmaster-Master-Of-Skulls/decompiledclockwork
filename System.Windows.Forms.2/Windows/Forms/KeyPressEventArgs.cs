using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020002B5 RID: 693
	[ComVisible(true)]
	public class KeyPressEventArgs : EventArgs
	{
		// Token: 0x06002A8E RID: 10894 RVA: 0x000C0389 File Offset: 0x000BE589
		public KeyPressEventArgs(char keyChar)
		{
			this.keyChar = keyChar;
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06002A8F RID: 10895 RVA: 0x000C0398 File Offset: 0x000BE598
		// (set) Token: 0x06002A90 RID: 10896 RVA: 0x000C03A0 File Offset: 0x000BE5A0
		public char KeyChar
		{
			get
			{
				return this.keyChar;
			}
			set
			{
				this.keyChar = value;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06002A91 RID: 10897 RVA: 0x000C03A9 File Offset: 0x000BE5A9
		// (set) Token: 0x06002A92 RID: 10898 RVA: 0x000C03B1 File Offset: 0x000BE5B1
		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		// Token: 0x04001140 RID: 4416
		private char keyChar;

		// Token: 0x04001141 RID: 4417
		private bool handled;
	}
}
