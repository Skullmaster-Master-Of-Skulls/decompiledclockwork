using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020002B3 RID: 691
	[ComVisible(true)]
	public class KeyEventArgs : EventArgs
	{
		// Token: 0x06002A7E RID: 10878 RVA: 0x000C02B7 File Offset: 0x000BE4B7
		public KeyEventArgs(Keys keyData)
		{
			this.keyData = keyData;
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06002A7F RID: 10879 RVA: 0x000C02C6 File Offset: 0x000BE4C6
		public virtual bool Alt
		{
			get
			{
				return (this.keyData & Keys.Alt) == Keys.Alt;
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002A80 RID: 10880 RVA: 0x000C02DB File Offset: 0x000BE4DB
		public bool Control
		{
			get
			{
				return (this.keyData & Keys.Control) == Keys.Control;
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002A81 RID: 10881 RVA: 0x000C02F0 File Offset: 0x000BE4F0
		// (set) Token: 0x06002A82 RID: 10882 RVA: 0x000C02F8 File Offset: 0x000BE4F8
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

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002A83 RID: 10883 RVA: 0x000C0304 File Offset: 0x000BE504
		public Keys KeyCode
		{
			get
			{
				Keys keys = this.keyData & Keys.KeyCode;
				if (!Enum.IsDefined(typeof(Keys), (int)keys))
				{
					return Keys.None;
				}
				return keys;
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06002A84 RID: 10884 RVA: 0x000C0338 File Offset: 0x000BE538
		public int KeyValue
		{
			get
			{
				return (int)(this.keyData & Keys.KeyCode);
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06002A85 RID: 10885 RVA: 0x000C0346 File Offset: 0x000BE546
		public Keys KeyData
		{
			get
			{
				return this.keyData;
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06002A86 RID: 10886 RVA: 0x000C034E File Offset: 0x000BE54E
		public Keys Modifiers
		{
			get
			{
				return this.keyData & Keys.Modifiers;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06002A87 RID: 10887 RVA: 0x000C035C File Offset: 0x000BE55C
		public virtual bool Shift
		{
			get
			{
				return (this.keyData & Keys.Shift) == Keys.Shift;
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06002A88 RID: 10888 RVA: 0x000C0371 File Offset: 0x000BE571
		// (set) Token: 0x06002A89 RID: 10889 RVA: 0x000C0379 File Offset: 0x000BE579
		public bool SuppressKeyPress
		{
			get
			{
				return this.suppressKeyPress;
			}
			set
			{
				this.suppressKeyPress = value;
				this.handled = value;
			}
		}

		// Token: 0x0400113D RID: 4413
		private readonly Keys keyData;

		// Token: 0x0400113E RID: 4414
		private bool handled;

		// Token: 0x0400113F RID: 4415
		private bool suppressKeyPress;
	}
}
