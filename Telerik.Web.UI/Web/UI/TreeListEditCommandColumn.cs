using System;
using System.ComponentModel;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001205 RID: 4613
	public class TreeListEditCommandColumn : TreeListColumn
	{
		// Token: 0x0600BE96 RID: 48790 RVA: 0x002A37E8 File Offset: 0x002A19E8
		public override void InitializeCell(TableCell cell, int columnIndex, TreeListItem inItem)
		{
			TreeListHeaderItem treeListHeaderItem = inItem as TreeListHeaderItem;
			if (treeListHeaderItem != null)
			{
				this.InitializeHeaderCells(cell, columnIndex, treeListHeaderItem);
			}
			TreeListEditableItem treeListEditableItem = inItem as TreeListEditableItem;
			TreeListDataItem treeListDataItem = inItem as TreeListDataItem;
			bool flag = treeListDataItem != null && base.Owner.EditMode == TreeListEditMode.InPlace;
			if (treeListEditableItem != null && treeListEditableItem.IsInEditMode && (treeListDataItem == null || flag))
			{
				this.InitializeEditItemCell(cell, treeListEditableItem);
				return;
			}
			if (treeListDataItem != null)
			{
				this.InitializeDataCells(cell, columnIndex, treeListDataItem);
			}
		}

		// Token: 0x0600BE97 RID: 48791 RVA: 0x002A3854 File Offset: 0x002A1A54
		protected override void InitializeDataCells(TableCell cell, int columnIndex, TreeListDataItem inItem)
		{
			if (this.ShowAddButton)
			{
				cell.Controls.Add(this.CreateButton(this.InsertButtonID, this.AddRecordText, "InitInsert", inItem.DisplayIndex.ToString(CultureInfo.InvariantCulture), this.AddRecordImageUrl, "rtlAdd", this.ToolTip, false));
			}
			if (this.ShowAddButton && this.ShowEditButton)
			{
				cell.Controls.Add(new LiteralControl("&nbsp;"));
			}
			if (this.ShowEditButton)
			{
				cell.Controls.Add(this.CreateButton(this.EditButtonID, this.EditText, "Edit", inItem.DisplayIndex.ToString(CultureInfo.InvariantCulture), this.EditImageUrl, "rtlEdit", this.ToolTip, false));
			}
		}

		// Token: 0x0600BE98 RID: 48792 RVA: 0x002A3924 File Offset: 0x002A1B24
		public virtual void InitializeEditItemCell(TableCell cell, TreeListEditableItem inItem)
		{
			if (inItem is ITreeListInsertItem)
			{
				cell.Controls.Add(this.CreateButton(this.InsertButtonID, this.InsertText, "PerformInsert", string.Empty, this.InsertImageUrl, "rtlUpdate", this.ToolTip, base.Owner.ValidationSettings.EnableValidation));
			}
			else
			{
				cell.Controls.Add(this.CreateButton(this.UpdateButtonID, this.UpdateText, "Update", string.Empty, this.UpdateImageUrl, "rtlUpdate", this.ToolTip, base.Owner.ValidationSettings.EnableValidation));
			}
			cell.Controls.Add(new LiteralControl("&nbsp;"));
			cell.Controls.Add(this.CreateButton(this.CancelButtonID, this.CancelText, "Cancel", string.Empty, this.CancelImageUrl, "rtlCancel", this.ToolTip, false));
		}

		// Token: 0x0600BE99 RID: 48793 RVA: 0x002A3A1C File Offset: 0x002A1C1C
		protected override void InitializeHeaderCells(TableCell cell, int columnIndex, TreeListHeaderItem inItem)
		{
			if (this.ShowAddButton)
			{
				cell.Controls.Add(this.CreateButton(this.InsertButtonID, this.AddRecordText, "InitInsert", null, this.AddRecordImageUrl, "rtlAdd", this.HeaderTooltip, false));
				return;
			}
			base.InitializeHeaderCells(cell, columnIndex, inItem);
		}

		// Token: 0x0600BE9A RID: 48794 RVA: 0x002A3A70 File Offset: 0x002A1C70
		protected virtual WebControl CreateButton(string buttonId, string buttonText, string commandName, string commandArgument, string imageUrl, string buttonCssClass, string toolTip, bool causesValidation)
		{
			switch (this.ButtonType)
			{
			case TreeListButtonColumnType.LinkButton:
				return new LinkButton
				{
					ID = buttonId,
					Text = HttpUtility.HtmlEncode(buttonText),
					ToolTip = toolTip,
					CommandName = commandName,
					CommandArgument = commandArgument,
					CausesValidation = causesValidation,
					ValidationGroup = (causesValidation ? base.Owner.ValidationSettings.ValidationGroup : string.Empty)
				};
			case TreeListButtonColumnType.PushButton:
				return new Button
				{
					ID = buttonId,
					Text = HttpUtility.HtmlEncode(buttonText),
					ToolTip = toolTip,
					CommandName = commandName,
					CommandArgument = commandArgument,
					CausesValidation = causesValidation,
					ValidationGroup = (causesValidation ? base.Owner.ValidationSettings.ValidationGroup : string.Empty)
				};
			case TreeListButtonColumnType.ImageButton:
				if (string.IsNullOrEmpty(imageUrl))
				{
					return new Button
					{
						ID = buttonId,
						CssClass = buttonCssClass,
						Text = " ",
						ToolTip = (string.IsNullOrEmpty(toolTip) ? buttonText : toolTip),
						CommandName = commandName,
						CommandArgument = commandArgument,
						CausesValidation = causesValidation,
						ValidationGroup = (causesValidation ? base.Owner.ValidationSettings.ValidationGroup : string.Empty)
					};
				}
				return new ImageButton
				{
					ID = buttonId,
					AlternateText = HttpUtility.HtmlEncode(buttonText),
					CommandName = commandName,
					CommandArgument = commandArgument,
					ImageUrl = imageUrl,
					ToolTip = (string.IsNullOrEmpty(toolTip) ? HttpUtility.HtmlEncode(buttonText) : toolTip),
					BorderWidth = Unit.Pixel(0),
					CausesValidation = causesValidation,
					ValidationGroup = (causesValidation ? base.Owner.ValidationSettings.ValidationGroup : string.Empty)
				};
			case TreeListButtonColumnType.FontIconButton:
				if (base.Owner.ResolvedRenderMode == RenderMode.Lightweight || base.Owner.ResolvedRenderMode == RenderMode.Mobile)
				{
					return new ElasticButton
					{
						ID = buttonId,
						CssClass = "t-button rtlActionButton " + buttonCssClass,
						FirstSpanClass = "t-font-icon rtlIcon " + buttonCssClass + "Icon",
						Text = (string.IsNullOrEmpty(buttonText) ? (buttonCssClass.Substring(3) + " Button") : buttonText),
						ToolTip = (string.IsNullOrEmpty(toolTip) ? buttonText : toolTip),
						CommandName = commandName,
						CommandArgument = commandArgument,
						CausesValidation = causesValidation,
						ValidationGroup = (causesValidation ? base.Owner.ValidationSettings.ValidationGroup : string.Empty),
						UseSubmitBehavior = false
					};
				}
				return new LinkButton
				{
					ID = buttonId,
					Text = HttpUtility.HtmlEncode(buttonText),
					ToolTip = toolTip,
					CommandName = commandName,
					CommandArgument = commandArgument,
					CausesValidation = causesValidation,
					ValidationGroup = (causesValidation ? base.Owner.ValidationSettings.ValidationGroup : string.Empty)
				};
			default:
				return null;
			}
		}

		// Token: 0x17003D7A RID: 15738
		// (get) Token: 0x0600BE9B RID: 48795 RVA: 0x002A3D8B File Offset: 0x002A1F8B
		protected virtual string EditButtonID
		{
			get
			{
				return "EditButton_" + this.UniqueName;
			}
		}

		// Token: 0x17003D7B RID: 15739
		// (get) Token: 0x0600BE9C RID: 48796 RVA: 0x002A3D9D File Offset: 0x002A1F9D
		protected virtual string AddButtonID
		{
			get
			{
				return "AddButton_" + this.UniqueName;
			}
		}

		// Token: 0x17003D7C RID: 15740
		// (get) Token: 0x0600BE9D RID: 48797 RVA: 0x002A3DAF File Offset: 0x002A1FAF
		protected virtual string InsertButtonID
		{
			get
			{
				return "InsertButton_" + this.UniqueName;
			}
		}

		// Token: 0x17003D7D RID: 15741
		// (get) Token: 0x0600BE9E RID: 48798 RVA: 0x002A3DC1 File Offset: 0x002A1FC1
		protected virtual string UpdateButtonID
		{
			get
			{
				return "UpdateButton_" + this.UniqueName;
			}
		}

		// Token: 0x17003D7E RID: 15742
		// (get) Token: 0x0600BE9F RID: 48799 RVA: 0x002A3DD3 File Offset: 0x002A1FD3
		protected virtual string CancelButtonID
		{
			get
			{
				return "CancelButton_" + this.UniqueName;
			}
		}

		// Token: 0x17003D7F RID: 15743
		// (get) Token: 0x0600BEA0 RID: 48800 RVA: 0x002A3DE8 File Offset: 0x002A1FE8
		// (set) Token: 0x0600BEA1 RID: 48801 RVA: 0x002A3E11 File Offset: 0x002A2011
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue(true)]
		public virtual bool ShowAddButton
		{
			get
			{
				object obj = base.ViewState["ShowAddButton"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ShowAddButton"] = value;
			}
		}

		// Token: 0x17003D80 RID: 15744
		// (get) Token: 0x0600BEA2 RID: 48802 RVA: 0x002A3E2C File Offset: 0x002A202C
		// (set) Token: 0x0600BEA3 RID: 48803 RVA: 0x002A3E55 File Offset: 0x002A2055
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public virtual bool ShowEditButton
		{
			get
			{
				object obj = base.ViewState["ShowEditButton"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ShowEditButton"] = value;
			}
		}

		// Token: 0x17003D81 RID: 15745
		// (get) Token: 0x0600BEA4 RID: 48804 RVA: 0x002A3E70 File Offset: 0x002A2070
		// (set) Token: 0x0600BEA5 RID: 48805 RVA: 0x002A3EBF File Offset: 0x002A20BF
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[DefaultValue(typeof(TreeListButtonColumnType), "LinkButton")]
		[Description("The type of button contained within the column.")]
		public virtual TreeListButtonColumnType ButtonType
		{
			get
			{
				object obj = base.ViewState["ButtonType"];
				if (obj != null)
				{
					return (TreeListButtonColumnType)obj;
				}
				if (base.Owner == null || (base.Owner.ResolvedRenderMode != RenderMode.Lightweight && base.Owner.ResolvedRenderMode != RenderMode.Mobile))
				{
					return TreeListButtonColumnType.LinkButton;
				}
				return TreeListButtonColumnType.FontIconButton;
			}
			set
			{
				if (value < TreeListButtonColumnType.LinkButton || value > TreeListButtonColumnType.FontIconButton)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["ButtonType"] = value;
			}
		}

		// Token: 0x0600BEA6 RID: 48806 RVA: 0x002A3EEA File Offset: 0x002A20EA
		protected override string GenerateUniqueName()
		{
			return base.GenerateUniqueNameBase("EditCommandColumn");
		}

		// Token: 0x17003D82 RID: 15746
		// (get) Token: 0x0600BEA7 RID: 48807 RVA: 0x002A3EF7 File Offset: 0x002A20F7
		// (set) Token: 0x0600BEA8 RID: 48808 RVA: 0x002A3EFF File Offset: 0x002A20FF
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[DefaultValue("EditCommandColumn")]
		public override string UniqueName
		{
			get
			{
				return base.UniqueName;
			}
			set
			{
				base.UniqueName = value;
			}
		}

		// Token: 0x17003D83 RID: 15747
		// (get) Token: 0x0600BEA9 RID: 48809 RVA: 0x002A3F08 File Offset: 0x002A2108
		// (set) Token: 0x0600BEAA RID: 48810 RVA: 0x002A3F41 File Offset: 0x002A2141
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Cancel")]
		public virtual string CancelText
		{
			get
			{
				string result;
				if ((result = (base.ViewState["CancelText"] as string)) == null)
				{
					if (base.Owner == null)
					{
						return "Cancel";
					}
					result = base.Owner.Localization.CancelText;
				}
				return result;
			}
			set
			{
				base.ViewState["CancelText"] = value;
			}
		}

		// Token: 0x17003D84 RID: 15748
		// (get) Token: 0x0600BEAB RID: 48811 RVA: 0x002A3F54 File Offset: 0x002A2154
		// (set) Token: 0x0600BEAC RID: 48812 RVA: 0x002A3F8D File Offset: 0x002A218D
		[NotifyParentProperty(true)]
		[DefaultValue("Edit")]
		[Localizable(true)]
		public virtual string EditText
		{
			get
			{
				string result;
				if ((result = (base.ViewState["EditText"] as string)) == null)
				{
					if (base.Owner == null)
					{
						return "Edit";
					}
					result = base.Owner.Localization.EditText;
				}
				return result;
			}
			set
			{
				base.ViewState["EditText"] = value;
			}
		}

		// Token: 0x17003D85 RID: 15749
		// (get) Token: 0x0600BEAD RID: 48813 RVA: 0x002A3FA0 File Offset: 0x002A21A0
		// (set) Token: 0x0600BEAE RID: 48814 RVA: 0x002A3FD9 File Offset: 0x002A21D9
		[Localizable(true)]
		[DefaultValue("Update")]
		[NotifyParentProperty(true)]
		public virtual string UpdateText
		{
			get
			{
				string result;
				if ((result = (base.ViewState["UpdateText"] as string)) == null)
				{
					if (base.Owner == null)
					{
						return "Update";
					}
					result = base.Owner.Localization.UpdateText;
				}
				return result;
			}
			set
			{
				base.ViewState["UpdateText"] = value;
			}
		}

		// Token: 0x17003D86 RID: 15750
		// (get) Token: 0x0600BEAF RID: 48815 RVA: 0x002A3FEC File Offset: 0x002A21EC
		// (set) Token: 0x0600BEB0 RID: 48816 RVA: 0x002A4025 File Offset: 0x002A2225
		[DefaultValue("Add record")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public virtual string AddRecordText
		{
			get
			{
				string result;
				if ((result = (base.ViewState["AddRecordText"] as string)) == null)
				{
					if (base.Owner == null)
					{
						return "Add record";
					}
					result = base.Owner.Localization.AddRecordText;
				}
				return result;
			}
			set
			{
				base.ViewState["AddRecordText"] = value;
			}
		}

		// Token: 0x17003D87 RID: 15751
		// (get) Token: 0x0600BEB1 RID: 48817 RVA: 0x002A4038 File Offset: 0x002A2238
		// (set) Token: 0x0600BEB2 RID: 48818 RVA: 0x002A4071 File Offset: 0x002A2271
		[DefaultValue("Insert")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public virtual string InsertText
		{
			get
			{
				string result;
				if ((result = (base.ViewState["InsertText"] as string)) == null)
				{
					if (base.Owner == null)
					{
						return "Insert";
					}
					result = base.Owner.Localization.InsertText;
				}
				return result;
			}
			set
			{
				base.ViewState["InsertText"] = value;
			}
		}

		// Token: 0x17003D88 RID: 15752
		// (get) Token: 0x0600BEB3 RID: 48819 RVA: 0x002A4084 File Offset: 0x002A2284
		// (set) Token: 0x0600BEB4 RID: 48820 RVA: 0x002A40A4 File Offset: 0x002A22A4
		[NotifyParentProperty(true)]
		[UrlProperty]
		[DefaultValue("")]
		public virtual string AddRecordImageUrl
		{
			get
			{
				return (base.ViewState["AddRecordImageUrl"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["AddRecordImageUrl"] = value;
			}
		}

		// Token: 0x17003D89 RID: 15753
		// (get) Token: 0x0600BEB5 RID: 48821 RVA: 0x002A40B7 File Offset: 0x002A22B7
		// (set) Token: 0x0600BEB6 RID: 48822 RVA: 0x002A40D7 File Offset: 0x002A22D7
		[UrlProperty]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string InsertImageUrl
		{
			get
			{
				return (base.ViewState["InsertImageUrl"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["InsertImageUrl"] = value;
			}
		}

		// Token: 0x17003D8A RID: 15754
		// (get) Token: 0x0600BEB7 RID: 48823 RVA: 0x002A40EA File Offset: 0x002A22EA
		// (set) Token: 0x0600BEB8 RID: 48824 RVA: 0x002A410A File Offset: 0x002A230A
		[NotifyParentProperty(true)]
		[UrlProperty]
		[DefaultValue("")]
		public virtual string UpdateImageUrl
		{
			get
			{
				return (base.ViewState["UpdateImageUrl"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["UpdateImageUrl"] = value;
			}
		}

		// Token: 0x17003D8B RID: 15755
		// (get) Token: 0x0600BEB9 RID: 48825 RVA: 0x002A411D File Offset: 0x002A231D
		// (set) Token: 0x0600BEBA RID: 48826 RVA: 0x002A413D File Offset: 0x002A233D
		[DefaultValue("")]
		[UrlProperty]
		[NotifyParentProperty(true)]
		public virtual string EditImageUrl
		{
			get
			{
				return (base.ViewState["EditImageUrl"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["EditImageUrl"] = value;
			}
		}

		// Token: 0x17003D8C RID: 15756
		// (get) Token: 0x0600BEBB RID: 48827 RVA: 0x002A4150 File Offset: 0x002A2350
		// (set) Token: 0x0600BEBC RID: 48828 RVA: 0x002A4170 File Offset: 0x002A2370
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[UrlProperty]
		public virtual string CancelImageUrl
		{
			get
			{
				return (base.ViewState["CancelImageUrl"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["CancelImageUrl"] = value;
			}
		}

		// Token: 0x17003D8D RID: 15757
		// (get) Token: 0x0600BEBD RID: 48829 RVA: 0x002A4183 File Offset: 0x002A2383
		// (set) Token: 0x0600BEBE RID: 48830 RVA: 0x002A41A3 File Offset: 0x002A23A3
		[Description("Gets or sets the title attribute that will be applied to the buttons")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string ToolTip
		{
			get
			{
				return (base.ViewState["ToolTip"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["ToolTip"] = value;
			}
		}
	}
}
