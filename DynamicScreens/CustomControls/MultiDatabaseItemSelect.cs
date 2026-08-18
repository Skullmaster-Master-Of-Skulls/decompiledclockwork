using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DynamicScreens.CustomControls
{
	// Token: 0x0200006A RID: 106
	public class MultiDatabaseItemSelect : UserControl
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x00042538 File Offset: 0x00041538
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0004256F File Offset: 0x0004156F
		private void InitializeComponent()
		{
			this.components = new Container();
			base.AutoScaleMode = AutoScaleMode.Font;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00042585 File Offset: 0x00041585
		public MultiDatabaseItemSelect()
		{
			this.InitializeComponent();
		}

		// Token: 0x0400038F RID: 911
		private IContainer components = null;
	}
}
