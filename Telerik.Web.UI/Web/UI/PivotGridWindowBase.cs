using System;
using System.Globalization;

namespace Telerik.Web.UI
{
	// Token: 0x02000DA6 RID: 3494
	public abstract class PivotGridWindowBase : RadWindow
	{
		// Token: 0x060082AB RID: 33451 RVA: 0x001DC7A8 File Offset: 0x001DA9A8
		public PivotGridWindowBase(RadPivotGrid ownerPivotGrid)
		{
			this.ownerPivotGrid = ownerPivotGrid;
			this.EnableEmbeddedScripts = ownerPivotGrid.EnableEmbeddedScripts;
			this.EnableEmbeddedSkins = ownerPivotGrid.EnableEmbeddedSkins;
			this.EnableEmbeddedBaseStylesheet = ownerPivotGrid.EnableEmbeddedBaseStylesheet;
			base.EnableAriaSupport = ownerPivotGrid.EnableAriaSupport;
			this.RenderMode = ownerPivotGrid.ResolvedRenderMode;
			this.OnClientShow = "Telerik.Web.UI.PivotGrid.OnWindowShow";
			base.PreRender += this.PivotGridWindow_PreRender;
			if (!base.DesignMode)
			{
				this.EnableTheming = ownerPivotGrid.EnableTheming;
			}
			else
			{
				ownerPivotGrid.Controls.Add(this);
			}
			this.Visible = !base.DesignMode;
		}

		// Token: 0x060082AC RID: 33452 RVA: 0x001DC84C File Offset: 0x001DAA4C
		private void PivotGridWindow_PreRender(object sender, EventArgs e)
		{
			this.Skin = this.ownerPivotGrid.RuntimeSkin;
			this.CssClass = string.Format(CultureInfo.InvariantCulture, "PivotGridWindow PivotGridWindow_{0}", new object[]
			{
				this.ownerPivotGrid.RuntimeSkin
			});
		}

		// Token: 0x040023FD RID: 9213
		protected readonly RadPivotGrid ownerPivotGrid;
	}
}
