using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x0200040A RID: 1034
	internal class ToolStripLocationCancelEventArgs : CancelEventArgs
	{
		// Token: 0x0600476F RID: 18287 RVA: 0x0012C2ED File Offset: 0x0012A4ED
		public ToolStripLocationCancelEventArgs(Point newLocation, bool value) : base(value)
		{
			this.newLocation = newLocation;
		}

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x06004770 RID: 18288 RVA: 0x0012C2FD File Offset: 0x0012A4FD
		public Point NewLocation
		{
			get
			{
				return this.newLocation;
			}
		}

		// Token: 0x040026ED RID: 9965
		private Point newLocation;
	}
}
