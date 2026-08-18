using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000DA7 RID: 3495
	public class PivotGridFieldSettingsWindow : PivotGridWindowBase
	{
		// Token: 0x060082AD RID: 33453 RVA: 0x001DC895 File Offset: 0x001DAA95
		public PivotGridFieldSettingsWindow(RadPivotGrid owner) : base(owner)
		{
		}

		// Token: 0x060082AE RID: 33454 RVA: 0x001DC89E File Offset: 0x001DAA9E
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			base.CreateChildControls();
			base.ContentContainer.Controls.Add(this.ContainerPanel);
			this.CreateFunctionsBox();
			this.CreateOKButton();
			this.CreateCancelButton();
		}

		// Token: 0x060082AF RID: 33455 RVA: 0x001DC8DC File Offset: 0x001DAADC
		private void CreateOKButton()
		{
			if (this.ownerPivotGrid.ResolvedRenderMode == RenderMode.Classic)
			{
				this.btnOK = new Button();
			}
			else
			{
				this.btnOK = new ElasticButton(string.Empty, "rpgButtonText");
				this.btnOK.CssClass = "rpgActionButton rpgButtonOk";
			}
			this.btnOK.ID = "UpdateFieldButton";
			this.btnOK.Text = this.ownerPivotGrid.Localization.FieldSettingsWindowOKButton;
			this.btnOK.ToolTip = this.ownerPivotGrid.Localization.FieldSettingsWindowOKButton;
			this.btnOK.OnClientClick = "return false";
			this.ContainerPanel.Controls.Add(this.btnOK);
		}

		// Token: 0x060082B0 RID: 33456 RVA: 0x001DC998 File Offset: 0x001DAB98
		private void CreateCancelButton()
		{
			if (this.ownerPivotGrid.ResolvedRenderMode == RenderMode.Classic)
			{
				this.btnCancel = new Button();
			}
			else
			{
				this.btnCancel = new ElasticButton(string.Empty, "rpgButtonText");
				this.btnCancel.CssClass = "rpgActionButton rpgButtonCancel";
			}
			this.btnCancel.ID = "CancelButton";
			this.btnCancel.Text = this.ownerPivotGrid.Localization.FieldSettingsWindowCancelButton;
			this.btnCancel.ToolTip = this.ownerPivotGrid.Localization.FieldSettingsWindowCancelButton;
			this.btnCancel.OnClientClick = "return false";
			this.ContainerPanel.Controls.Add(this.btnCancel);
		}

		// Token: 0x060082B1 RID: 33457 RVA: 0x001DCA54 File Offset: 0x001DAC54
		private void CreateFunctionsBox()
		{
			this.rlbFunctionsBox = new RadListBox();
			this.rlbFunctionsBox.ID = "FunctionsBox";
			this.rlbFunctionsBox.RenderMode = this.ownerPivotGrid.ResolvedRenderMode;
			int num = 0;
			foreach (string text in Enum.GetNames(typeof(PivotGridAggregate)))
			{
				RadListBoxItem radListBoxItem = new RadListBoxItem();
				radListBoxItem.Text = text;
				radListBoxItem.Value = num++.ToString();
				this.rlbFunctionsBox.Items.Add(radListBoxItem);
			}
			this.ContainerPanel.Controls.Add(this.rlbFunctionsBox);
		}

		// Token: 0x1700294E RID: 10574
		// (get) Token: 0x060082B2 RID: 33458 RVA: 0x001DCB03 File Offset: 0x001DAD03
		public RadListBox AggregateFunctionsBox
		{
			get
			{
				this.EnsureChildControls();
				return this.rlbFunctionsBox;
			}
		}

		// Token: 0x1700294F RID: 10575
		// (get) Token: 0x060082B3 RID: 33459 RVA: 0x001DCB11 File Offset: 0x001DAD11
		public Panel ContainerPanel
		{
			get
			{
				if (this.pnlContainer == null)
				{
					this.pnlContainer = new Panel();
					this.pnlContainer.ID = "WrapperPanel";
				}
				return this.pnlContainer;
			}
		}

		// Token: 0x040023FE RID: 9214
		private RadListBox rlbFunctionsBox;

		// Token: 0x040023FF RID: 9215
		private Button btnOK;

		// Token: 0x04002400 RID: 9216
		private Button btnCancel;

		// Token: 0x04002401 RID: 9217
		private Panel pnlContainer;
	}
}
