using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020010AA RID: 4266
	public class GridClientDeleteColumn : GridButtonColumn
	{
		// Token: 0x0600ADBC RID: 44476 RVA: 0x0025771F File Offset: 0x0025591F
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public GridClientDeleteColumn()
		{
		}

		// Token: 0x17003826 RID: 14374
		// (get) Token: 0x0600ADBD RID: 44477 RVA: 0x00257727 File Offset: 0x00255927
		public override bool Selectable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600ADBE RID: 44478 RVA: 0x0025772A File Offset: 0x0025592A
		public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			if (string.IsNullOrEmpty(this.CommandName))
			{
				this.CommandName = "Delete";
			}
			base.InitializeCell(cell, columnIndex, inItem);
		}

		// Token: 0x0600ADBF RID: 44479 RVA: 0x00257750 File Offset: 0x00255950
		protected override void TrySetOnClientClickScript(Control control, GridItem item, string functionName, params string[] functionParameters)
		{
			string onClientClick;
			if (item.OwnerTableView.EditMode == GridEditMode.Batch)
			{
				onClientClick = GridBatchEditingHelper.GenerateClientScript(item.OwnerTableView, "_deleteRecord", new string[]
				{
					item.OwnerTableView.ClientID,
					item.ClientID
				});
			}
			else
			{
				onClientClick = string.Format("$find('{0}')._clientDelete(event); return false;", item.OwnerTableView.ClientID);
			}
			Button button = control as Button;
			ImageButton imageButton = control as ImageButton;
			LinkButton linkButton = control as LinkButton;
			if (button != null)
			{
				button.OnClientClick = onClientClick;
				return;
			}
			if (imageButton != null)
			{
				imageButton.OnClientClick = onClientClick;
				return;
			}
			if (linkButton != null)
			{
				linkButton.OnClientClick = onClientClick;
			}
		}

		// Token: 0x0600ADC0 RID: 44480 RVA: 0x002577F0 File Offset: 0x002559F0
		public override GridColumn Clone()
		{
			GridClientDeleteColumn gridClientDeleteColumn = new GridClientDeleteColumn();
			gridClientDeleteColumn.CopyBaseProperties(this);
			return gridClientDeleteColumn;
		}

		// Token: 0x0600ADC1 RID: 44481 RVA: 0x0025780B File Offset: 0x00255A0B
		protected override void CopyBaseProperties(GridColumn FromColumn)
		{
			base.CopyBaseProperties(FromColumn);
		}

		// Token: 0x17003827 RID: 14375
		// (get) Token: 0x0600ADC2 RID: 44482 RVA: 0x00257814 File Offset: 0x00255A14
		// (set) Token: 0x0600ADC3 RID: 44483 RVA: 0x00257865 File Offset: 0x00255A65
		[DefaultValue("Delete")]
		[Category("Appearance")]
		[Localizable(true)]
		[Description("The text used for the delete button.")]
		[NotifyParentProperty(true)]
		public override string Text
		{
			get
			{
				string text = base.Text;
				string result = (base.Owner != null && base.Owner.OwnerGrid != null) ? base.Owner.OwnerGrid.Localization.DeleteText : "Delete";
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
				return result;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x17003828 RID: 14376
		// (get) Token: 0x0600ADC4 RID: 44484 RVA: 0x00257870 File Offset: 0x00255A70
		// (set) Token: 0x0600ADC5 RID: 44485 RVA: 0x0025790E File Offset: 0x00255B0E
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[UrlProperty]
		public override string ImageUrl
		{
			get
			{
				object obj = base.ViewState["_iurl"];
				if (obj != null)
				{
					if (base.DesignMode)
					{
						return obj as string;
					}
					return base.Owner.OwnerGrid.ResolveUrl((string)obj);
				}
				else
				{
					if (base.Owner != null && !base.Owner.IsDesignMode && base.Owner.OwnerGrid.ResolvedRenderMode != RenderMode.Lightweight && base.Owner.OwnerGrid.ResolvedRenderMode != RenderMode.Mobile)
					{
						return base.Owner.OwnerGrid.ResolveGridImageUrl("Delete.gif", false);
					}
					return "";
				}
			}
			set
			{
				base.ViewState["_iurl"] = value;
				this.OnColumnChanged();
			}
		}
	}
}
