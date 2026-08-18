using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000DA8 RID: 3496
	public class PivotGridFieldsWindow : PivotGridWindowBase
	{
		// Token: 0x060082B4 RID: 33460 RVA: 0x001DCB3C File Offset: 0x001DAD3C
		public PivotGridFieldsWindow(RadPivotGrid pivotGrid) : base(pivotGrid)
		{
		}

		// Token: 0x060082B5 RID: 33461 RVA: 0x001DCB48 File Offset: 0x001DAD48
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Panel panel = base.ContentContainer.FindControl("FieldsWindowWrapperPanel") as Panel;
			if (panel != null)
			{
				panel.CssClass = string.Format("RadPivotGrid RadPivotGrid_{0} rpgFieldsWindow", this.ownerPivotGrid.RuntimeSkin);
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x060082B6 RID: 33462 RVA: 0x001DCB98 File Offset: 0x001DAD98
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			base.CreateChildControls();
			Panel panel = new Panel();
			base.ContentContainer.Controls.Add(panel);
			panel.ID = "FieldsWindowWrapperPanel";
			if (!this.ownerPivotGrid.EnableConfigurationPanel || this.ownerPivotGrid.ConfigurationPanelSettings.Position != PivotGridConfigurationPanelPosition.FieldsWindow)
			{
				IEnumerable<PivotGridField> enumerable = from f in this.ownerPivotGrid.Fields
				where f.IsHidden
				select f;
				foreach (PivotGridField pivotGridField in enumerable)
				{
					pivotGridField.RenderingControl.CssClass = ((pivotGridField.RenderingControl.ChildIndex == -1) ? "rpgFieldItem" : "rpgFieldItem rpgSubFieldItem");
					if (this.ownerPivotGrid.ClientSettings.EnableFieldsDragDrop)
					{
						pivotGridField.RenderingControl.ToolTip = "Drag to move";
					}
					panel.Controls.Add(pivotGridField.RenderingControl);
				}
			}
		}

		// Token: 0x17002950 RID: 10576
		// (get) Token: 0x060082B7 RID: 33463 RVA: 0x001DCCB8 File Offset: 0x001DAEB8
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x060082B8 RID: 33464 RVA: 0x001DCCC8 File Offset: 0x001DAEC8
		internal void Initialize(PivotGridFieldsWindow original)
		{
			this.ID = "FieldsWindow";
			base.Title = this.ownerPivotGrid.Localization.FieldsWindowTitle;
			base.OffsetElementID = this.ownerPivotGrid.ClientID;
			base.VisibleStatusbar = false;
			if (this.ownerPivotGrid.EnableConfigurationPanel && this.ownerPivotGrid.ConfigurationPanelSettings.Position == PivotGridConfigurationPanelPosition.FieldsWindow)
			{
				this.Width = 450;
				this.Height = 600;
				base.Behaviors = (WindowBehaviors.Close | WindowBehaviors.Move);
			}
			else
			{
				this.Width = 240;
				base.MinWidth = 160;
				this.Height = 250;
				base.MinHeight = 200;
				base.Behaviors = (WindowBehaviors.Resize | WindowBehaviors.Close | WindowBehaviors.Move);
			}
			base.KeepInScreenBounds = true;
			if (original == null)
			{
				return;
			}
			base.Top = original.Top;
			base.Left = original.Left;
			if (this.ownerPivotGrid.ConfigurationPanelSettings.Position != PivotGridConfigurationPanelPosition.FieldsWindow)
			{
				this.Width = original.Width;
				this.Height = original.Height;
			}
			base.VisibleOnPageLoad = original.VisibleOnPageLoad;
			this.EnsureChildControls();
		}
	}
}
