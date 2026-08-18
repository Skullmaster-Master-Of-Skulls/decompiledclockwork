using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020010BA RID: 4282
	public class GridExpandColumn : GridColumn
	{
		// Token: 0x1700386F RID: 14447
		// (get) Token: 0x0600AEB4 RID: 44724 RVA: 0x0025BCEF File Offset: 0x00259EEF
		public override bool Selectable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17003870 RID: 14448
		// (get) Token: 0x0600AEB5 RID: 44725 RVA: 0x0025BCF2 File Offset: 0x00259EF2
		protected bool IsSkinEmpty
		{
			get
			{
				if (this._isSkinEmpty == null)
				{
					this._isSkinEmpty = new bool?(base.Owner.OwnerGrid.EmptySkin());
				}
				return this._isSkinEmpty.Value;
			}
		}

		// Token: 0x17003871 RID: 14449
		// (get) Token: 0x0600AEB6 RID: 44726 RVA: 0x0025BD27 File Offset: 0x00259F27
		// (set) Token: 0x0600AEB7 RID: 44727 RVA: 0x0025BD2F File Offset: 0x00259F2F
		[DefaultValue("Filter ExpandColumn column")]
		public override string FilterControlAltText
		{
			get
			{
				return base.FilterControlAltText;
			}
			set
			{
				base.FilterControlAltText = value;
			}
		}

		// Token: 0x0600AEB8 RID: 44728 RVA: 0x0025BD38 File Offset: 0x00259F38
		public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			base.InitializeCell(cell, columnIndex, inItem);
			cell.Controls.Clear();
			if (inItem.IsDataBound)
			{
				if ((this.IsSkinEmpty || !string.IsNullOrEmpty(base.Owner.OwnerGrid.ImagesPath.Trim())) && this.ButtonType == GridExpandColumnType.SpriteButton)
				{
					this.ButtonType = GridExpandColumnType.ImageButton;
				}
				Control control = null;
				if (inItem.OwnerTableView.HierarchyLoadMode != GridChildLoadMode.Client)
				{
					if (this.ButtonType == GridExpandColumnType.LinkButton)
					{
						LinkButton linkButton = new GridLinkButton();
						linkButton.Text = "+";
						linkButton.CommandName = this.CommandName;
						linkButton.Style.Add("text-decoration", "none");
						linkButton.CausesValidation = false;
						control = linkButton;
					}
					else if (this.ButtonType == GridExpandColumnType.PushButton)
					{
						control = new Button
						{
							Text = "+",
							CommandName = this.CommandName,
							CausesValidation = false,
							Width = Unit.Pixel(22),
							Height = Unit.Pixel(22)
						};
					}
					else if (this.ButtonType == GridExpandColumnType.ImageButton)
					{
						control = new GridImageButton(this)
						{
							ID = "GECBtn" + this.UniqueName,
							ImageUrl = "",
							CommandName = this.CommandName,
							CausesValidation = false
						};
					}
					else if (this.ButtonType == GridExpandColumnType.SpriteButton)
					{
						Button button = new Button();
						if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
						{
							button = new ElasticButton();
							button.CssClass = "t-button rgActionButton ";
							button.UseSubmitBehavior = false;
							((ElasticButton)button).FirstSpanClass = "t-font-icon rgIcon " + (base.Owner.HierarchyDefaultExpanded ? "rgCollapseIcon" : "rgExpandIcon");
							if (base.Owner.OwnerGrid.EnableAriaSupport)
							{
								button.Attributes.Add("aria-label", base.Owner.HierarchyDefaultExpanded ? base.Owner.OwnerGrid.HierarchySettings.CollapseTooltip : base.Owner.OwnerGrid.HierarchySettings.ExpandTooltip);
							}
						}
						button.ID = "GECBtn" + this.UniqueName;
						button.CommandName = this.CommandName;
						button.CausesValidation = false;
						button.Text = " ";
						control = button;
					}
					this.TrySetOnClientClickScript(control, inItem, "fireCommand", new string[]
					{
						this.CommandName,
						inItem.ItemIndexHierarchical
					});
				}
				else
				{
					string value = string.Format("$find(\"{0}\")._toggleExpand(this, event); return false;", inItem.Parent.Parent.ClientID);
					if (this.ButtonType == GridExpandColumnType.LinkButton)
					{
						HtmlAnchor htmlAnchor = new HtmlAnchor();
						htmlAnchor.InnerHtml = "+";
						htmlAnchor.Style.Add("text-decoration", "none");
						htmlAnchor.Attributes["onclick"] = value;
						control = htmlAnchor;
					}
					else if (this.ButtonType == GridExpandColumnType.PushButton)
					{
						HtmlButton htmlButton = new HtmlButton();
						htmlButton.InnerHtml = "+";
						htmlButton.Style["width"] = Unit.Pixel(22).ToString();
						htmlButton.Style["height"] = Unit.Pixel(22).ToString();
						htmlButton.Attributes["onclick"] = value;
						control = htmlButton;
					}
					else if (this.ButtonType == GridExpandColumnType.ImageButton)
					{
						Image image = new Image();
						image.BorderWidth = Unit.Pixel(0);
						image.ImageUrl = "";
						image.Attributes["onclick"] = value;
						control = image;
					}
					else if (this.ButtonType == GridExpandColumnType.SpriteButton)
					{
						Button button2 = new Button();
						if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
						{
							button2 = new ElasticButton();
							button2.CssClass = "t-button rgActionButton ";
							Button button3 = button2;
							button3.CssClass += (base.Owner.HierarchyDefaultExpanded ? "rgCollapse" : "rgExpand");
							((ElasticButton)button2).FirstSpanClass = "t-font-icon rgIcon " + (base.Owner.HierarchyDefaultExpanded ? "rgCollapseIcon" : "rgExpandIcon");
							if (base.Owner.OwnerGrid.EnableAriaSupport)
							{
								button2.Attributes.Add("aria-label", base.Owner.HierarchyDefaultExpanded ? base.Owner.OwnerGrid.HierarchySettings.CollapseTooltip : base.Owner.OwnerGrid.HierarchySettings.ExpandTooltip);
							}
						}
						button2.Attributes["onclick"] = value;
						button2.Text = " ";
						control = button2;
					}
				}
				if ((base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight) && this.ButtonType == GridExpandColumnType.SpriteButton)
				{
					cell.Controls.Add((ElasticButton)control);
				}
				else
				{
					cell.Controls.Add(control);
				}
			}
			else if (inItem.OwnerTableView.EnableHierarchyExpandAll && inItem is GridHeaderItem)
			{
				Control child = null;
				if (inItem.OwnerTableView.HierarchyLoadMode == GridChildLoadMode.Client)
				{
					string value2 = string.Format("$find(\"{0}\")._expandAll(event); return false;", inItem.Parent.Parent.Parent.ClientID);
					if (this.ButtonType == GridExpandColumnType.LinkButton)
					{
						HtmlAnchor htmlAnchor2 = new HtmlAnchor();
						htmlAnchor2.InnerHtml = "+";
						htmlAnchor2.Style.Add("text-decoration", "none");
						htmlAnchor2.Attributes["onclick"] = value2;
						child = htmlAnchor2;
					}
					else if (this.ButtonType == GridExpandColumnType.PushButton)
					{
						HtmlButton htmlButton2 = new HtmlButton();
						htmlButton2.InnerHtml = "+";
						htmlButton2.Style["width"] = Unit.Pixel(22).ToString();
						htmlButton2.Style["height"] = Unit.Pixel(22).ToString();
						htmlButton2.Attributes["onclick"] = value2;
						child = htmlButton2;
					}
					else if (this.ButtonType == GridExpandColumnType.ImageButton)
					{
						Image image2 = new Image();
						image2.BorderWidth = Unit.Pixel(0);
						image2.ImageUrl = "";
						image2.Attributes["onclick"] = value2;
						child = image2;
					}
					else if (this.ButtonType == GridExpandColumnType.SpriteButton)
					{
						Button button4 = new Button();
						if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
						{
							button4 = new ElasticButton();
							button4.CssClass = "t-button rgActionButton ";
							((ElasticButton)button4).FirstSpanClass = "t-font-icon rgIcon " + (base.Owner.HierarchyDefaultExpanded ? "rgCollapseIcon" : "rgExpandIcon");
							if (base.Owner.OwnerGrid.EnableAriaSupport)
							{
								button4.Attributes.Add("aria-label", base.Owner.HierarchyDefaultExpanded ? base.Owner.OwnerGrid.HierarchySettings.CollapseAllTooltip : base.Owner.OwnerGrid.HierarchySettings.ExpandAllTooltip);
							}
						}
						button4.Attributes["onclick"] = value2;
						Button button5 = button4;
						button5.CssClass += (inItem.OwnerTableView.HierarchyDefaultExpanded ? "rgCollapse" : "rgExpand");
						button4.Text = " ";
						child = button4;
					}
					cell.Controls.Add(child);
				}
				else
				{
					Control control2 = null;
					if (this.ButtonType == GridExpandColumnType.LinkButton)
					{
						LinkButton linkButton2 = new GridLinkButton();
						linkButton2.ID = "GECAllBtn" + this.UniqueName;
						linkButton2.Text = "+";
						linkButton2.CommandName = "ExpandCollapseAll";
						linkButton2.Style.Add("text-decoration", "none");
						linkButton2.CausesValidation = false;
						control2 = linkButton2;
					}
					else if (this.ButtonType == GridExpandColumnType.PushButton)
					{
						control2 = new Button
						{
							ID = "GECAllBtn" + this.UniqueName,
							Text = "+",
							CommandName = "ExpandCollapseAll",
							CausesValidation = false,
							Width = Unit.Pixel(22),
							Height = Unit.Pixel(22)
						};
					}
					else if (this.ButtonType == GridExpandColumnType.ImageButton)
					{
						control2 = new GridImageButton(this)
						{
							ID = "GECAllBtn" + this.UniqueName,
							ImageUrl = "",
							CommandName = "ExpandCollapseAll",
							CausesValidation = false
						};
					}
					else if (this.ButtonType == GridExpandColumnType.SpriteButton)
					{
						Button button6 = new Button();
						if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
						{
							button6 = new ElasticButton();
							button6.UseSubmitBehavior = false;
							button6.CssClass = "t-button rgActionButton ";
							((ElasticButton)button6).FirstSpanClass = "t-font-icon rgIcon " + (base.Owner.HierarchyDefaultExpanded ? "rgCollapseIcon" : "rgExpandIcon");
							if (base.Owner.OwnerGrid.EnableAriaSupport)
							{
								button6.Attributes.Add("aria-label", base.Owner.HierarchyDefaultExpanded ? base.Owner.OwnerGrid.HierarchySettings.CollapseAllTooltip : base.Owner.OwnerGrid.HierarchySettings.ExpandAllTooltip);
							}
						}
						button6.ID = "GECAllBtn" + this.UniqueName;
						button6.CommandName = "ExpandCollapseAll";
						button6.CausesValidation = false;
						Button button7 = button6;
						button7.CssClass += (base.Owner.HierarchyDefaultExpanded ? "rgCollapse" : "rgExpand");
						button6.Text = " ";
						control2 = button6;
					}
					cell.Controls.Add(control2);
					this.TrySetOnClientClickScript(control2, inItem, "fireCommand", new string[]
					{
						"ExpandCollapseAll",
						this.UniqueName
					});
				}
			}
			if (inItem is GridHeaderItem && base.Owner.OwnerGrid.EnableAriaSupport && (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight))
			{
				Label label = new Label();
				label.Style.Add("display", "none");
				label.Text = this.UniqueName;
				cell.Controls.Add(label);
			}
		}

		// Token: 0x0600AEB9 RID: 44729 RVA: 0x0025C848 File Offset: 0x0025AA48
		public override void PrepareCell(TableCell cell, GridItem item)
		{
			if (!this.Visible || !cell.Visible)
			{
				return;
			}
			GridHeaderItem gridHeaderItem = item as GridHeaderItem;
			if (gridHeaderItem == null && !(item is GridFilteringItem))
			{
				cell.CssClass = ((!string.IsNullOrEmpty(cell.CssClass)) ? string.Format("{0} {1}", "rgExpandCol", cell.CssClass) : "rgExpandCol");
			}
			if (gridHeaderItem != null)
			{
				cell.CssClass = ((!string.IsNullOrEmpty(cell.CssClass)) ? string.Format("{0} {1} {2}", base.Owner.RenderHeaderStyle.CssClass, "rgExpandCol", cell.CssClass) : string.Format("{0} {1}", base.Owner.RenderHeaderStyle.CssClass, "rgExpandCol"));
				if (item.OwnerTableView.EnableHierarchyExpandAll)
				{
					WebControl webControl = cell.Controls[0] as WebControl;
					Image image = webControl as Image;
					bool flag = true;
					GridDataItemCollection items = item.OwnerTableView.Items;
					if (item.OwnerTableView.HierarchyLoadMode == GridChildLoadMode.Conditional)
					{
						foreach (object obj in items)
						{
							GridDataItem gridDataItem = (GridDataItem)obj;
							flag = (flag && (gridDataItem.ConditionalExpanded || gridDataItem.Expanded));
							if (!flag)
							{
								break;
							}
						}
						if (flag)
						{
							string value = string.Format("$find(\"{0}\")._expandAll(event); return false;", item.Parent.Parent.Parent.ClientID);
							webControl.Attributes["onclick"] = value;
						}
					}
					if (image != null)
					{
						if (image.CssClass.Contains("rgExpand"))
						{
							image.ImageUrl = this.ExpandImageUrl;
						}
						else
						{
							image.ImageUrl = this.CollapseImageUrl;
						}
						image.CssClass = "";
					}
					string value2 = "";
					if (item.OwnerTableView.Items.Count == 0)
					{
						value2 = "none";
					}
					else if (item.OwnerTableView.EnableGroupsExpandAll && item.OwnerTableView.GroupByExpressions.Count > 0)
					{
						value2 = (this.IsLastGroupAllHeaderButtonOpen(gridHeaderItem) ? "" : "none");
					}
					webControl.Style.Add("display", value2);
					if (webControl.CssClass.Contains("rgExpand"))
					{
						webControl.ToolTip = base.Owner.OwnerGrid.HierarchySettings.ExpandAllTooltip;
					}
					else
					{
						webControl.ToolTip = base.Owner.OwnerGrid.HierarchySettings.CollapseAllTooltip;
					}
				}
			}
			if (item.IsDataBound)
			{
				if (cell.Controls.Count == 0)
				{
					return;
				}
				Control control = cell.Controls[0];
				GridHierarchySettings hierarchySettings = base.Owner.OwnerGrid.HierarchySettings;
				string text = item.Expanded ? hierarchySettings.CollapseTooltip : hierarchySettings.ExpandTooltip;
				string text2 = item.Expanded ? "-" : "+";
				GridChildLoadMode hierarchyLoadMode = item.OwnerTableView.HierarchyLoadMode;
				if (this.ButtonType == GridExpandColumnType.LinkButton)
				{
					if (hierarchyLoadMode != GridChildLoadMode.Client)
					{
						LinkButton linkButton = control as LinkButton;
						linkButton.Text = text2;
						if (string.IsNullOrEmpty(linkButton.ToolTip))
						{
							linkButton.ToolTip = text;
						}
					}
					else
					{
						HtmlAnchor htmlAnchor = control as HtmlAnchor;
						htmlAnchor.InnerHtml = text2;
						if (string.IsNullOrEmpty(htmlAnchor.Title))
						{
							htmlAnchor.Title = text;
						}
					}
				}
				else if (this.ButtonType == GridExpandColumnType.PushButton)
				{
					if (hierarchyLoadMode != GridChildLoadMode.Client)
					{
						Button button = control as Button;
						button.Text = text2;
						if (string.IsNullOrEmpty(button.ToolTip))
						{
							button.ToolTip = text;
						}
					}
					else
					{
						HtmlButton htmlButton = control as HtmlButton;
						htmlButton.InnerHtml = text2;
						if (string.IsNullOrEmpty(htmlButton.Attributes["title"]))
						{
							htmlButton.Attributes["title"] = text;
						}
					}
				}
				else if (this.ButtonType == GridExpandColumnType.ImageButton)
				{
					Image image2 = control as Image;
					image2.ImageUrl = (item.Expanded ? this.CollapseImageUrl : this.ExpandImageUrl);
					if (string.IsNullOrEmpty(image2.ToolTip))
					{
						image2.ToolTip = text;
					}
					image2.AlternateText = text;
				}
				else if (this.ButtonType == GridExpandColumnType.SpriteButton)
				{
					Button button2 = control as Button;
					if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
					{
						ElasticButton elasticButton = control as ElasticButton;
						elasticButton.CssClass = "t-button rgActionButton ";
						elasticButton.FirstSpanClass = "t-font-icon rgIcon " + (item.Expanded ? "rgCollapseIcon" : "rgExpandIcon");
						elasticButton.Text = text;
						button2 = elasticButton;
						if (base.Owner.OwnerGrid.EnableAriaSupport)
						{
							elasticButton.Attributes.Add("aria-label", text);
						}
					}
					Button button3 = button2;
					button3.CssClass += (item.Expanded ? "rgCollapse" : "rgExpand");
					if (string.IsNullOrEmpty(button2.ToolTip))
					{
						button2.ToolTip = text;
					}
				}
				WebControl webControl2 = control as WebControl;
				if (webControl2 != null)
				{
					webControl2.Enabled = item.CanExpand;
					if ((item.Expanded || item.ConditionalExpanded) && item.OwnerTableView.HierarchyLoadMode == GridChildLoadMode.Conditional)
					{
						webControl2.Attributes["onclick"] = string.Format("$find(\"{0}\")._toggleExpand(this, event); return false;", item.OwnerTableView.ClientID);
					}
				}
			}
		}

		// Token: 0x0600AEBA RID: 44730 RVA: 0x0025CDDC File Offset: 0x0025AFDC
		private bool IsLastGroupAllHeaderButtonOpen(GridHeaderItem headerItem)
		{
			bool result = false;
			if (base.Owner.GroupByExpressions.Count > 0)
			{
				WebControl webControl = headerItem.Cells[base.Owner.GroupByExpressions.Count - 1].Controls[0] as WebControl;
				if (webControl.CssClass.Contains("rgCollapse") && webControl.Visible && webControl.Style["display"] != "none")
				{
					result = true;
				}
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x17003872 RID: 14450
		// (get) Token: 0x0600AEBB RID: 44731 RVA: 0x0025CE6C File Offset: 0x0025B06C
		// (set) Token: 0x0600AEBC RID: 44732 RVA: 0x0025CEC7 File Offset: 0x0025B0C7
		[DefaultValue("")]
		[UrlProperty]
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

		// Token: 0x0600AEBD RID: 44733 RVA: 0x0025CEE0 File Offset: 0x0025B0E0
		protected virtual bool ShouldSerializeExpandImageUrl()
		{
			return base.Owner != null && base.Owner.OwnerGrid.ShouldSerializeImageUrl(this.ExpandImageUrl);
		}

		// Token: 0x17003873 RID: 14451
		// (get) Token: 0x0600AEBE RID: 44734 RVA: 0x0025CF04 File Offset: 0x0025B104
		// (set) Token: 0x0600AEBF RID: 44735 RVA: 0x0025CF5F File Offset: 0x0025B15F
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("")]
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

		// Token: 0x0600AEC0 RID: 44736 RVA: 0x0025CF78 File Offset: 0x0025B178
		protected virtual bool ShouldSerializeCollapseImageUrl()
		{
			return base.Owner != null && base.Owner.OwnerGrid.ShouldSerializeImageUrl(this.CollapseImageUrl);
		}

		// Token: 0x17003874 RID: 14452
		// (get) Token: 0x0600AEC1 RID: 44737 RVA: 0x0025CF9C File Offset: 0x0025B19C
		// (set) Token: 0x0600AEC2 RID: 44738 RVA: 0x0025CFC5 File Offset: 0x0025B1C5
		[Description("The type of button contained within the column.")]
		[Category("Appearance")]
		[DefaultValue(typeof(GridExpandColumnType), "SpriteButton")]
		public virtual GridExpandColumnType ButtonType
		{
			get
			{
				object obj = base.ViewState["ButtonType"];
				if (obj != null)
				{
					return (GridExpandColumnType)obj;
				}
				return GridExpandColumnType.SpriteButton;
			}
			set
			{
				if (value < GridExpandColumnType.LinkButton || value > GridExpandColumnType.SpriteButton)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["ButtonType"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003875 RID: 14453
		// (get) Token: 0x0600AEC3 RID: 44739 RVA: 0x0025CFF6 File Offset: 0x0025B1F6
		// (set) Token: 0x0600AEC4 RID: 44740 RVA: 0x0025CFFE File Offset: 0x0025B1FE
		[Browsable(true)]
		[DefaultValue("ExpandColumn")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17003876 RID: 14454
		// (get) Token: 0x0600AEC5 RID: 44741 RVA: 0x0025D007 File Offset: 0x0025B207
		// (set) Token: 0x0600AEC6 RID: 44742 RVA: 0x0025D00A File Offset: 0x0025B20A
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

		// Token: 0x17003877 RID: 14455
		// (get) Token: 0x0600AEC7 RID: 44743 RVA: 0x0025D00C File Offset: 0x0025B20C
		// (set) Token: 0x0600AEC8 RID: 44744 RVA: 0x0025D00F File Offset: 0x0025B20F
		[DefaultValue(false)]
		public override bool Reorderable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17003878 RID: 14456
		// (get) Token: 0x0600AEC9 RID: 44745 RVA: 0x0025D011 File Offset: 0x0025B211
		// (set) Token: 0x0600AECA RID: 44746 RVA: 0x0025D014 File Offset: 0x0025B214
		[DefaultValue(false)]
		public override bool Resizable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17003879 RID: 14457
		// (get) Token: 0x0600AECB RID: 44747 RVA: 0x0025D016 File Offset: 0x0025B216
		// (set) Token: 0x0600AECC RID: 44748 RVA: 0x0025D01E File Offset: 0x0025B21E
		[DefaultValue(true)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x1700387A RID: 14458
		// (get) Token: 0x0600AECD RID: 44749 RVA: 0x0025D028 File Offset: 0x0025B228
		// (set) Token: 0x0600AECE RID: 44750 RVA: 0x0025D055 File Offset: 0x0025B255
		[DefaultValue("ExpandCollapse")]
		public virtual string CommandName
		{
			get
			{
				object obj = base.ViewState["CommandName"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "ExpandCollapse";
			}
			set
			{
				if (value != "CommandName")
				{
					base.ViewState["CommandName"] = value;
				}
				else
				{
					base.ViewState["CommandName"] = null;
				}
				this.OnColumnChanged();
			}
		}

		// Token: 0x1700387B RID: 14459
		// (get) Token: 0x0600AECF RID: 44751 RVA: 0x0025D08E File Offset: 0x0025B28E
		// (set) Token: 0x0600AED0 RID: 44752 RVA: 0x0025D096 File Offset: 0x0025B296
		[DefaultValue(false)]
		public bool Created { get; set; }

		// Token: 0x0600AED1 RID: 44753 RVA: 0x0025D09F File Offset: 0x0025B29F
		protected override string GenerateUniqueName()
		{
			return base.GenerateUniqueNameBase("ExpandColumn");
		}

		// Token: 0x0600AED2 RID: 44754 RVA: 0x0025D0AC File Offset: 0x0025B2AC
		public override GridColumn Clone()
		{
			GridExpandColumn gridExpandColumn = new GridExpandColumn();
			gridExpandColumn.CopyBaseProperties(this);
			return gridExpandColumn;
		}

		// Token: 0x0600AED3 RID: 44755 RVA: 0x0025D0C8 File Offset: 0x0025B2C8
		protected override void CopyBaseProperties(GridColumn fromColumn)
		{
			base.CopyBaseProperties(fromColumn);
			GridExpandColumn gridExpandColumn = (GridExpandColumn)fromColumn;
			this.CommandName = gridExpandColumn.CommandName;
			this.ButtonType = gridExpandColumn.ButtonType;
			this.ExpandImageUrl = gridExpandColumn.ExpandImageUrl;
			this.CollapseImageUrl = gridExpandColumn.CollapseImageUrl;
		}

		// Token: 0x0600AED4 RID: 44756 RVA: 0x0025D113 File Offset: 0x0025B313
		public override void Initialize()
		{
			base.Initialize();
			if (!base.Owner.IsDesignMode && base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile)
			{
				this.HeaderStyle.Width = Unit.Pixel(41);
			}
		}

		// Token: 0x04002E1B RID: 11803
		private bool? _isSkinEmpty;
	}
}
