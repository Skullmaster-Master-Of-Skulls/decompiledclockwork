using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000E0E RID: 3598
	public class PivotGridToolTipManager : RadToolTipManager
	{
		// Token: 0x060085B3 RID: 34227 RVA: 0x001E794C File Offset: 0x001E5B4C
		public PivotGridToolTipManager(RadPivotGrid ownerPivotGrid)
		{
			this.ownerPivotGrid = ownerPivotGrid;
			this.EnableEmbeddedScripts = ownerPivotGrid.EnableEmbeddedScripts;
			this.EnableEmbeddedSkins = ownerPivotGrid.EnableEmbeddedSkins;
			this.EnableEmbeddedBaseStylesheet = ownerPivotGrid.EnableEmbeddedBaseStylesheet;
			base.EnableAriaSupport = ownerPivotGrid.EnableAriaSupport;
			base.PreRender += this.RadToolTipManager_PreRender;
		}

		// Token: 0x060085B4 RID: 34228 RVA: 0x001E79A8 File Offset: 0x001E5BA8
		private void RadToolTipManager_PreRender(object sender, EventArgs e)
		{
			this.Skin = this.ownerPivotGrid.RuntimeSkin;
		}

		// Token: 0x04002545 RID: 9541
		protected readonly RadPivotGrid ownerPivotGrid;
	}
}
