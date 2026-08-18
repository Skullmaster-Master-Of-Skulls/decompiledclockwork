using System;
using System.ComponentModel;
using DevComponents.DotNetBar;

namespace AutoComboBox.AccessibilityWrappers.AccessibleDevComponentsExplorerBar
{
	// Token: 0x0200003D RID: 61
	public class AccDevComponentsExplorerBar : ExplorerBar
	{
		// Token: 0x0600020A RID: 522 RVA: 0x000127B2 File Offset: 0x000117B2
		public AccDevComponentsExplorerBar()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000127CC File Offset: 0x000117CC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00012803 File Offset: 0x00011803
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		// Token: 0x040001DE RID: 478
		private IContainer components = null;
	}
}
