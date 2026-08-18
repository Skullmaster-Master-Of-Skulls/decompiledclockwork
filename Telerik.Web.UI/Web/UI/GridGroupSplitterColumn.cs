using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020010BB RID: 4283
	public class GridGroupSplitterColumn : GridColumn
	{
		// Token: 0x1700387C RID: 14460
		// (get) Token: 0x0600AED6 RID: 44758 RVA: 0x0025D155 File Offset: 0x0025B355
		public override bool Selectable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700387D RID: 14461
		// (get) Token: 0x0600AED7 RID: 44759 RVA: 0x0025D158 File Offset: 0x0025B358
		// (set) Token: 0x0600AED8 RID: 44760 RVA: 0x0025D18B File Offset: 0x0025B38B
		public override bool Visible
		{
			get
			{
				return base.Visible && (base.Owner == null || base.Owner.OwnerGrid == null || base.Owner.OwnerGrid.GroupingEnabled);
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x0600AED9 RID: 44761 RVA: 0x0025D194 File Offset: 0x0025B394
		private void SetVisibilityOnHeaderButton(WebControl expandHeaderButton, GridHeaderItem headerItem)
		{
			if (this._correspondingExpression.Index > 0)
			{
				int index = this._correspondingExpression.Index - 1;
				ControlCollection controls = headerItem.Cells[index].Controls;
				if (controls.Count > 0)
				{
					WebControl webControl = controls[0] as WebControl;
					if (base.Owner.GroupLoadMode == GridGroupLoadMode.Client)
					{
						if (!(webControl.Style["display"] != "none") || !webControl.CssClass.Contains("rgCollapse"))
						{
							expandHeaderButton.Style["display"] = "none";
							return;
						}
						if (!string.IsNullOrEmpty(webControl.Style["display"]))
						{
							expandHeaderButton.Style["display"] = "";
							return;
						}
					}
					else
					{
						if (webControl.Visible && webControl.CssClass.Contains("rgCollapse"))
						{
							expandHeaderButton.Visible = true;
							return;
						}
						expandHeaderButton.Visible = false;
					}
				}
			}
		}

		// Token: 0x0600AEDA RID: 44762 RVA: 0x0025D294 File Offset: 0x0025B494
		public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			GridHeaderItem gridHeaderItem = inItem as GridHeaderItem;
			if (inItem.OwnerTableView.EnableGroupsExpandAll && gridHeaderItem != null)
			{
				Button button;
				if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					button = new ElasticButton();
					button.CssClass = "t-button rgActionButton ";
					button.UseSubmitBehavior = false;
					((ElasticButton)button).FirstSpanClass = "t-font-icon rgIcon " + (base.Owner.GroupsDefaultExpanded ? "rgCollapseIcon" : "rgExpandIcon");
					if (base.Owner.OwnerGrid.EnableAriaSupport)
					{
						button.Attributes.Add("aria-label", base.Owner.GroupsDefaultExpanded ? base.Owner.OwnerGrid.GroupingSettings.CollapseTooltip : base.Owner.OwnerGrid.GroupingSettings.ExpandTooltip);
					}
				}
				else
				{
					button = new Button();
					button.Text = " ";
				}
				if (inItem.OwnerTableView.GroupLoadMode == GridGroupLoadMode.Client)
				{
					string onClientClick = string.Format("$find(\"{0}\")._expandAllGroups(event, {1}); return false;", inItem.Parent.Parent.Parent.ClientID, this._correspondingExpression.Index);
					Button button2 = button;
					button2.CssClass += (base.Owner.GroupsDefaultExpanded ? "rgCollapse" : "rgExpand");
					button.OnClientClick = onClientClick;
					button.CausesValidation = false;
					this.SetVisibilityOnHeaderButton(button, gridHeaderItem);
					if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
					{
						cell.Controls.Add((ElasticButton)button);
					}
					else
					{
						cell.Controls.Add(button);
					}
				}
				else
				{
					Button button3 = button;
					button3.CssClass += (base.Owner.GroupsDefaultExpanded ? "rgCollapse" : "rgExpand");
					button.CommandName = "GroupsExpandAll";
					button.CommandArgument = this._correspondingExpression.Index.ToString();
					if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
					{
						cell.Controls.Add((ElasticButton)button);
					}
					else
					{
						cell.Controls.Add(button);
					}
					this.SetVisibilityOnHeaderButton(button, gridHeaderItem);
				}
			}
			else if (inItem is GridGroupHeaderItem && this.CorrespondingExpression.Index == inItem.GroupLevel)
			{
				WebControl webControl;
				if (base.Owner.OwnerGrid.ShouldRenderImg(this.CollapseImageUrl))
				{
					if (inItem.OwnerTableView.GroupLoadMode == GridGroupLoadMode.Client)
					{
						Image image = new Image();
						image.BorderWidth = Unit.Pixel(0);
						image.ImageUrl = "";
						image.PreRender += this.button1_PreRender;
						webControl = image;
					}
					else
					{
						webControl = new GridImageButton(this)
						{
							ImageUrl = "",
							CommandName = "ExpandCollapse",
							CausesValidation = false
						};
					}
				}
				else
				{
					Button button4 = (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight) ? new ElasticButton() : new Button();
					button4.CommandName = "ExpandCollapse";
					button4.Text = " ";
					button4.CausesValidation = false;
					button4.PreRender += this.button1_PreRender;
					webControl = button4;
				}
				if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					((ElasticButton)webControl).UseSubmitBehavior = false;
					cell.Controls.Add((ElasticButton)webControl);
				}
				else
				{
					cell.Controls.Add(webControl);
				}
			}
			if (gridHeaderItem != null && base.Owner.OwnerGrid.EnableAriaSupport && (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight))
			{
				Label label = new Label();
				label.Style.Add("display", "none");
				label.Text = "GroupSplitterColumn";
				cell.Controls.Add(label);
			}
			if (cell.Controls.Count == 0 && string.IsNullOrEmpty(cell.Text) && base.Owner.OwnerGrid.ResolvedRenderMode != RenderMode.Lightweight)
			{
				cell.Text = "&nbsp;";
			}
		}

		// Token: 0x0600AEDB RID: 44763 RVA: 0x0025D724 File Offset: 0x0025B924
		private void button1_PreRender(object sender, EventArgs e)
		{
			WebControl webControl = (WebControl)sender;
			GridGroupHeaderItem gridGroupHeaderItem = (GridGroupHeaderItem)webControl.Parent.Parent;
			if (gridGroupHeaderItem.OwnerTableView.GroupLoadMode == GridGroupLoadMode.Client)
			{
				webControl.Attributes["onclick"] = string.Format("$find(\"{0}\")._toggleGroupsExpand(this, event); return false;", gridGroupHeaderItem.Parent.Parent.ClientID);
			}
			webControl.Attributes["id"] = string.Format("{0}__{1}__{2}", gridGroupHeaderItem.Parent.Parent.ClientID, gridGroupHeaderItem.RowIndex, gridGroupHeaderItem.GroupLevel);
		}

		// Token: 0x0600AEDC RID: 44764 RVA: 0x0025D7C4 File Offset: 0x0025B9C4
		public override void PrepareCell(TableCell cell, GridItem item)
		{
			if (!this.Visible || !cell.Visible)
			{
				return;
			}
			if (!base.Display)
			{
				cell.Style.Add(HtmlTextWriterStyle.Display, "none");
			}
			if (cell.CssClass.IndexOf("rgGroupHeader") > -1)
			{
				cell.CssClass = string.Empty;
			}
			GridHeaderItem gridHeaderItem = item as GridHeaderItem;
			GridFooterItem gridFooterItem = item as GridFooterItem;
			if (string.IsNullOrEmpty(cell.CssClass) && gridHeaderItem == null && !(item is GridFilteringItem) && gridFooterItem == null)
			{
				cell.CssClass = "rgGroupCol";
			}
			if (gridHeaderItem != null)
			{
				cell.CssClass = base.Owner.RenderHeaderStyle.CssClass + " rgGroupCol";
				if (gridHeaderItem.OwnerTableView.EnableGroupsExpandAll && cell.Controls.Count > 0)
				{
					Button button = cell.Controls[0] as Button;
					if (button != null)
					{
						button.ToolTip = (button.CssClass.Contains("rgExpand") ? base.Owner.OwnerGrid.GroupingSettings.ExpandAllTooltip : base.Owner.OwnerGrid.GroupingSettings.CollapseAllTooltip);
						if (base.Owner.OwnerGrid.EnableAriaSupport && (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight))
						{
							button.Attributes.Add("aria-label", button.ToolTip);
						}
					}
				}
			}
			if (gridFooterItem != null)
			{
				cell.CssClass = "rgGroupCol";
			}
			if (item is GridGroupHeaderItem)
			{
				if (cell.Controls.Count == 0)
				{
					return;
				}
				if (base.Owner.OwnerGrid.ShouldRenderImg(this.CollapseImageUrl))
				{
					if (item.Expanded)
					{
						(cell.Controls[0] as Image).ImageUrl = this.CollapseImageUrl;
						(cell.Controls[0] as Image).ToolTip = base.Owner.OwnerGrid.GroupingSettings.CollapseTooltip;
						(cell.Controls[0] as Image).AlternateText = base.Owner.OwnerGrid.GroupingSettings.CollapseTooltip;
						return;
					}
					(cell.Controls[0] as Image).ImageUrl = this.ExpandImageUrl;
					(cell.Controls[0] as Image).ToolTip = base.Owner.OwnerGrid.GroupingSettings.ExpandTooltip;
					(cell.Controls[0] as Image).AlternateText = base.Owner.OwnerGrid.GroupingSettings.ExpandTooltip;
					return;
				}
				else if (item.Expanded)
				{
					(cell.Controls[0] as Button).CssClass = ((base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight) ? "t-button rgActionButton rgCollapse" : "rgCollapse");
					(cell.Controls[0] as Button).ToolTip = base.Owner.OwnerGrid.GroupingSettings.CollapseTooltip;
					if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
					{
						(cell.Controls[0] as ElasticButton).FirstSpanClass = "t-font-icon rgIcon rgCollapseIcon";
						(cell.Controls[0] as ElasticButton).Text = base.Owner.OwnerGrid.GroupingSettings.CollapseTooltip;
						if (base.Owner.OwnerGrid.EnableAriaSupport)
						{
							(cell.Controls[0] as ElasticButton).Attributes.Add("aria-label", base.Owner.OwnerGrid.GroupingSettings.CollapseTooltip);
							return;
						}
					}
				}
				else
				{
					(cell.Controls[0] as Button).CssClass = ((base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight) ? "t-button rgActionButton rgExpand" : "rgExpand");
					(cell.Controls[0] as Button).ToolTip = base.Owner.OwnerGrid.GroupingSettings.ExpandTooltip;
					if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
					{
						(cell.Controls[0] as ElasticButton).FirstSpanClass = "t-font-icon rgIcon rgExpandIcon";
						(cell.Controls[0] as ElasticButton).Text = base.Owner.OwnerGrid.GroupingSettings.ExpandTooltip;
						if (base.Owner.OwnerGrid.EnableAriaSupport)
						{
							(cell.Controls[0] as ElasticButton).Attributes.Add("aria-label", base.Owner.OwnerGrid.GroupingSettings.ExpandTooltip);
						}
					}
				}
			}
		}

		// Token: 0x0600AEDD RID: 44765 RVA: 0x0025DCD0 File Offset: 0x0025BED0
		private void AdjustVisibilityOnHeaderButtons()
		{
			GridHeaderItem gridHeaderItem = base.Owner.GetItems(new GridItemType[]
			{
				GridItemType.Header
			})[0] as GridHeaderItem;
			WebControl webControl = null;
			for (int i = this._correspondingExpression.Index; i < base.Owner.GroupByExpressions.Count; i++)
			{
				WebControl webControl2 = gridHeaderItem.Cells[i].Controls[0] as WebControl;
				if (webControl != null)
				{
					if (webControl.CssClass == "rgCollapse" && webControl.Visible && webControl.Style["display"] != "none")
					{
						webControl2.Style.Add("display", "");
					}
					else
					{
						webControl2.Style.Add("display", "none");
					}
				}
				webControl = webControl2;
			}
		}

		// Token: 0x1700387E RID: 14462
		// (get) Token: 0x0600AEDE RID: 44766 RVA: 0x0025DDB0 File Offset: 0x0025BFB0
		// (set) Token: 0x0600AEDF RID: 44767 RVA: 0x0025DE0B File Offset: 0x0025C00B
		[UrlProperty]
		[DefaultValue("")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public virtual string ExpandImageUrl
		{
			get
			{
				object obj = base.ViewState["_eiurl"];
				if (obj != null)
				{
					return base.Owner.OwnerGrid.ResolveUrl((string)obj);
				}
				if (base.Owner != null)
				{
					return base.Owner.OwnerGrid.ResolveGridImageUrl("SinglePlus.gif");
				}
				return "";
			}
			set
			{
				base.ViewState["_eiurl"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x0600AEE0 RID: 44768 RVA: 0x0025DE24 File Offset: 0x0025C024
		protected virtual bool ShouldSerializeExpandImageUrl()
		{
			return base.Owner != null && base.Owner.OwnerGrid.ShouldSerializeImageUrl(this.ExpandImageUrl);
		}

		// Token: 0x1700387F RID: 14463
		// (get) Token: 0x0600AEE1 RID: 44769 RVA: 0x0025DE48 File Offset: 0x0025C048
		// (set) Token: 0x0600AEE2 RID: 44770 RVA: 0x0025DEA3 File Offset: 0x0025C0A3
		[Localizable(true)]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[UrlProperty]
		public virtual string CollapseImageUrl
		{
			get
			{
				object obj = base.ViewState["_ciurl"];
				if (obj != null)
				{
					return base.Owner.OwnerGrid.ResolveUrl((string)obj);
				}
				if (base.Owner != null)
				{
					return base.Owner.OwnerGrid.ResolveGridImageUrl("SingleMinus.gif");
				}
				return "";
			}
			set
			{
				base.ViewState["_ciurl"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x0600AEE3 RID: 44771 RVA: 0x0025DEBC File Offset: 0x0025C0BC
		protected virtual bool ShouldSerializeCollapseImageUrl()
		{
			return base.Owner != null && base.Owner.OwnerGrid.ShouldSerializeImageUrl(this.CollapseImageUrl);
		}

		// Token: 0x17003880 RID: 14464
		// (get) Token: 0x0600AEE4 RID: 44772 RVA: 0x0025DEDE File Offset: 0x0025C0DE
		// (set) Token: 0x0600AEE5 RID: 44773 RVA: 0x0025DEE1 File Offset: 0x0025C0E1
		[DefaultValue(false)]
		public override bool Groupable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x0600AEE6 RID: 44774 RVA: 0x0025DEE4 File Offset: 0x0025C0E4
		public override GridColumn Clone()
		{
			GridGroupSplitterColumn gridGroupSplitterColumn = new GridGroupSplitterColumn();
			gridGroupSplitterColumn.CopyBaseProperties(this);
			return gridGroupSplitterColumn;
		}

		// Token: 0x0600AEE7 RID: 44775 RVA: 0x0025DF00 File Offset: 0x0025C100
		protected override void CopyBaseProperties(GridColumn fromColumn)
		{
			base.CopyBaseProperties(fromColumn);
			GridGroupSplitterColumn gridGroupSplitterColumn = (GridGroupSplitterColumn)fromColumn;
			this.ExpandImageUrl = gridGroupSplitterColumn.ExpandImageUrl;
			this.CollapseImageUrl = gridGroupSplitterColumn.CollapseImageUrl;
		}

		// Token: 0x17003881 RID: 14465
		// (get) Token: 0x0600AEE8 RID: 44776 RVA: 0x0025DF33 File Offset: 0x0025C133
		public GridGroupByExpression CorrespondingExpression
		{
			get
			{
				return this._correspondingExpression;
			}
		}

		// Token: 0x0600AEE9 RID: 44777 RVA: 0x0025DF3B File Offset: 0x0025C13B
		internal void SetCorrespondingExpression(GridGroupByExpression expr)
		{
			this._correspondingExpression = expr;
		}

		// Token: 0x04002E1D RID: 11805
		private GridGroupByExpression _correspondingExpression;
	}
}
