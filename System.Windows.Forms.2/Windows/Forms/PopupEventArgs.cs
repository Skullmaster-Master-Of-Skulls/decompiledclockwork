using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x0200031E RID: 798
	public class PopupEventArgs : CancelEventArgs
	{
		// Token: 0x060032F8 RID: 13048 RVA: 0x000E3AD2 File Offset: 0x000E1CD2
		public PopupEventArgs(IWin32Window associatedWindow, Control associatedControl, bool isBalloon, Size size)
		{
			this.associatedWindow = associatedWindow;
			this.size = size;
			this.associatedControl = associatedControl;
			this.isBalloon = isBalloon;
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x060032F9 RID: 13049 RVA: 0x000E3AF7 File Offset: 0x000E1CF7
		public IWin32Window AssociatedWindow
		{
			get
			{
				return this.associatedWindow;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x060032FA RID: 13050 RVA: 0x000E3AFF File Offset: 0x000E1CFF
		public Control AssociatedControl
		{
			get
			{
				return this.associatedControl;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x060032FB RID: 13051 RVA: 0x000E3B07 File Offset: 0x000E1D07
		public bool IsBalloon
		{
			get
			{
				return this.isBalloon;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x060032FC RID: 13052 RVA: 0x000E3B0F File Offset: 0x000E1D0F
		// (set) Token: 0x060032FD RID: 13053 RVA: 0x000E3B17 File Offset: 0x000E1D17
		public Size ToolTipSize
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x04001EAA RID: 7850
		private IWin32Window associatedWindow;

		// Token: 0x04001EAB RID: 7851
		private Size size;

		// Token: 0x04001EAC RID: 7852
		private Control associatedControl;

		// Token: 0x04001EAD RID: 7853
		private bool isBalloon;
	}
}
