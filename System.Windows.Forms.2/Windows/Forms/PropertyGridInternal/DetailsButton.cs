using System;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000508 RID: 1288
	internal class DetailsButton : Button
	{
		// Token: 0x06005499 RID: 21657 RVA: 0x00162A4C File Offset: 0x00160C4C
		public DetailsButton(GridErrorDlg form)
		{
			this.parent = form;
		}

		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x0600549A RID: 21658 RVA: 0x00162A5B File Offset: 0x00160C5B
		public bool Expanded
		{
			get
			{
				return this.parent.DetailsButtonExpanded;
			}
		}

		// Token: 0x0600549B RID: 21659 RVA: 0x00162A68 File Offset: 0x00160C68
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level1)
			{
				return new DetailsButtonAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x0400371B RID: 14107
		private GridErrorDlg parent;
	}
}
