using System;

namespace System.Windows.Forms
{
	// Token: 0x02000270 RID: 624
	public class HandledMouseEventArgs : MouseEventArgs
	{
		// Token: 0x060027FF RID: 10239 RVA: 0x000BA33E File Offset: 0x000B853E
		public HandledMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta) : this(button, clicks, x, y, delta, false)
		{
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x000BA34E File Offset: 0x000B854E
		public HandledMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta, bool defaultHandledValue) : base(button, clicks, x, y, delta)
		{
			this.handled = defaultHandledValue;
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06002801 RID: 10241 RVA: 0x000BA365 File Offset: 0x000B8565
		// (set) Token: 0x06002802 RID: 10242 RVA: 0x000BA36D File Offset: 0x000B856D
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

		// Token: 0x0400106E RID: 4206
		private bool handled;
	}
}
