using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020004BC RID: 1212
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public abstract class GridColumn : IStateManager, IComparable
	{
		// Token: 0x06002B27 RID: 11047 RVA: 0x0008BDE5 File Offset: 0x00089FE5
		public GridColumn()
		{
			this.statebag = new StateBag();
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x0008BDF8 File Offset: 0x00089FF8
		public static GridColumn InheritanceSafeClone(GridColumn from)
		{
			GridColumn gridColumn = from.Clone();
			if (gridColumn.GetType() != from.GetType())
			{
				throw new InvalidOperationException("You must override Clone() for a derived grid column.");
			}
			return gridColumn;
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x0008BE2C File Offset: 0x0008A02C
		public virtual void Initialize()
		{
			if (this.owner != null && this.owner.OwnerGrid != null && this.owner.OwnerGrid.Site != null)
			{
				this.designMode = this.owner.OwnerGrid.Site.DesignMode;
			}
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x0008BE7C File Offset: 0x0008A07C
		private bool cellIsEmpty(TableCell cell)
		{
			string text = cell.Text ?? "";
			return (string.IsNullOrEmpty(text) || text == "&nbsp;") && cell.Controls.Count == 0;
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x0008BEC0 File Offset: 0x0008A0C0
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public virtual void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			if (this.Owner.OwnerGrid.IsExporting)
			{
				this.Owner.ClearTableViewScriptControls(cell);
			}
			Control control = null;
			Control control2 = null;
			int num = -1;
			bool flag = this.CurrentFilterFunction == GridKnownFunction.IsNull || this.CurrentFilterFunction == GridKnownFunction.IsEmpty || this.CurrentFilterFunction == GridKnownFunction.NotIsNull || this.CurrentFilterFunction == GridKnownFunction.NotIsEmpty;
			GridItemType itemType = inItem.ItemType;
			switch (itemType)
			{
			case GridItemType.Footer:
			{
				string text = this.FooterText;
				if (text.Length == 0)
				{
					text = "&nbsp;";
				}
				if (this.cellIsEmpty(cell))
				{
					cell.Text = text;
				}
				return;
			}
			case GridItemType.TFoot:
				break;
			case GridItemType.Header:
			{
				bool flag2 = this.ListOfFilterValues != null && this.ListOfFilterValues.Length > 0;
				bool flag3 = this.CurrentFilterFunction != GridKnownFunction.NoFilter && (!string.IsNullOrEmpty(this.CurrentFilterValue) || flag2);
				if (this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile && (flag3 || flag))
				{
					cell.CssClass = RadGrid.FilteredClassName;
				}
				Control control3 = null;
				bool flag4 = true;
				string text2 = null;
				if (!inItem.OwnerTableView.AllowSorting)
				{
					flag4 = false;
				}
				if (flag4)
				{
					if (!this.Sortable)
					{
						flag4 = false;
					}
					else
					{
						text2 = this.GetSortExpression();
						if (text2.Length == 0)
						{
							flag4 = false;
						}
					}
				}
				string headerImageUrl = this.HeaderImageUrl;
				if (headerImageUrl.Length == 0)
				{
					string text3 = this.HeaderText;
					if (flag4)
					{
						if (this.HeaderButtonType == GridHeaderButtonType.None)
						{
							cell.Attributes["onclick"] = string.Format("Telerik.Web.UI.Grid.Sort($find('{0}'), '{1}'); return false;", this.owner.ClientID, text2);
							cell.Style["cursor"] = "pointer";
							LiteralControl literalControl = new LiteralControl(text3);
							control3 = literalControl;
						}
						else if (this.HeaderButtonType == GridHeaderButtonType.LinkButton)
						{
							LinkButton linkButton = new GridLinkButton();
							linkButton.Text = text3;
							if (string.IsNullOrEmpty(this.HeaderTooltip))
							{
								linkButton.ToolTip = this.Owner.OwnerGrid.SortingSettings.SortToolTip;
							}
							else
							{
								linkButton.ToolTip = this.HeaderTooltip;
							}
							linkButton.CommandName = "Sort";
							linkButton.CommandArgument = text2;
							linkButton.CausesValidation = false;
							control3 = linkButton;
						}
						else if (this.HeaderButtonType == GridHeaderButtonType.PushButton)
						{
							Button button = new Button();
							button.Text = text3;
							if (string.IsNullOrEmpty(this.HeaderTooltip))
							{
								button.ToolTip = this.Owner.OwnerGrid.SortingSettings.SortToolTip;
							}
							else
							{
								button.ToolTip = this.HeaderTooltip;
							}
							button.CommandName = "Sort";
							button.CommandArgument = text2;
							button.CausesValidation = false;
							control3 = button;
						}
						else if (this.HeaderButtonType == GridHeaderButtonType.TextButton)
						{
							GridTextButton gridTextButton = new GridTextButton();
							gridTextButton.Text = text3;
							if (string.IsNullOrEmpty(this.HeaderTooltip))
							{
								gridTextButton.ToolTip = this.Owner.OwnerGrid.SortingSettings.SortToolTip;
							}
							else
							{
								gridTextButton.ToolTip = this.HeaderTooltip;
							}
							gridTextButton.CommandName = "Sort";
							gridTextButton.CommandArgument = text2;
							gridTextButton.CausesValidation = false;
							gridTextButton.Style.Add("cursor", "pointer");
							control3 = gridTextButton;
						}
					}
					else
					{
						if (text3.Length == 0)
						{
							text3 = "&nbsp;";
						}
						if (this.cellIsEmpty(cell))
						{
							cell.Text = text3;
							cell.Controls.Add(new LiteralControl(text3));
						}
					}
				}
				else if (flag4)
				{
					control3 = new GridImageButton(this)
					{
						ImageUrl = this.HeaderImageUrl,
						CommandName = "Sort",
						CommandArgument = text2,
						CausesValidation = false,
						AlternateText = this.HeaderText,
						ToolTip = this.HeaderText
					};
				}
				else
				{
					control3 = new System.Web.UI.WebControls.Image
					{
						ImageUrl = headerImageUrl,
						AlternateText = this.HeaderText,
						ToolTip = this.HeaderText
					};
				}
				if (control3 != null)
				{
					this.SetClientScript(control3, string.Format("Telerik.Web.UI.Grid.Sort($find('{0}'), '{1}'); return false;", inItem.OwnerTableView.ClientID, text2));
				}
				if (inItem.OwnerTableView.SortExpressions.ContainsExpression(text2))
				{
					if (this.owner.OwnerGrid.ShouldRenderImg(this.SortAscImageUrl))
					{
						control = this.CreateSortIcon();
						((GridImageButton)control).CommandName = "Sort";
						((GridImageButton)control).CommandArgument = this.GetSortExpression();
						((GridImageButton)control).CausesValidation = false;
						if (!this.owner.ShowHeader)
						{
							GridSortExpression expression = this.owner.SortExpressions.GetExpression(this.GetSortExpression());
							GridImageButton gridImageButton = (GridImageButton)control;
							switch (expression.SortOrder)
							{
							case GridSortOrder.None:
								gridImageButton.Visible = false;
								break;
							case GridSortOrder.Ascending:
								gridImageButton.AlternateText = this.Owner.OwnerGrid.SortingSettings.SortedAscToolTip;
								gridImageButton.ToolTip = this.Owner.OwnerGrid.SortingSettings.SortedAscToolTip;
								gridImageButton.ImageUrl = this.SortAscImageUrl;
								gridImageButton.BorderWidth = Unit.Empty;
								break;
							case GridSortOrder.Descending:
								gridImageButton.AlternateText = this.Owner.OwnerGrid.SortingSettings.SortedDescToolTip;
								gridImageButton.ToolTip = this.Owner.OwnerGrid.SortingSettings.SortedDescToolTip;
								gridImageButton.ImageUrl = this.SortDescImageUrl;
								break;
							}
						}
					}
					else
					{
						Button button2;
						if (this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
						{
							control = new ElasticButton();
							button2 = (control as ElasticButton);
							button2.UseSubmitBehavior = false;
						}
						else
						{
							control = new Button();
							button2 = (control as Button);
						}
						if (!this.ShowSortIcon)
						{
							button2.Style["display"] = "none";
						}
						button2.CommandName = "Sort";
						button2.CommandArgument = this.GetSortExpression();
						button2.CausesValidation = false;
						button2.Text = " ";
						if (this.owner.OwnerGrid.IsClientCommandAssigned)
						{
							button2.OnClientClick = string.Format("Telerik.Web.UI.Grid.Sort($find('{0}'), '{1}'); return false;", inItem.OwnerTableView.ClientID, ((Button)control).CommandArgument);
						}
					}
				}
				if (this.EnableHeaderContextMenu && this.Owner.OwnerGrid.FilterType == GridFilterType.HeaderContext && this.Owner.OwnerGrid.ResolvedRenderMode != RenderMode.Mobile)
				{
					Button button3 = null;
					if (this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Classic)
					{
						button3 = new Button();
						button3.CausesValidation = false;
						button3.CssClass = "rgOptions";
					}
					else if (this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
					{
						button3 = new ElasticButton
						{
							CausesValidation = false,
							CssClass = "t-button rgActionButton rgOptions",
							FirstSpanClass = "t-font-icon rgIcon rgOptionsIcon"
						};
					}
					if (button3 != null)
					{
						this.SetClientScript(button3, string.Format("Telerik.Web.UI.Grid.ShowContextMenu($find('{0}'), '{1}', event); return false;", inItem.OwnerTableView.ClientID, this.UniqueName));
						control2 = button3;
					}
				}
				if (control3 != null)
				{
					cell.Controls.Add(control3);
				}
				if (this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile && this.SupportsFiltering())
				{
					if (this.Owner.EnableHeaderContextMenu)
					{
						cell.Controls.Add(RadGrid.CreateButton("Options", true));
					}
					else if (this.Owner.AllowFilteringByColumn && this.ShowFilterIcon)
					{
						cell.Controls.Add(RadGrid.CreateButton("Filter", true));
					}
				}
				if (control != null)
				{
					if (this.ShowSortIcon)
					{
						cell.Controls.Add(new LiteralControl("&nbsp;"));
					}
					cell.Controls.Add(control);
					this.sortIcon = control;
					if (num > 0)
					{
						cell.Controls.Add(new LiteralControl(num.ToString()));
					}
				}
				if (control2 != null)
				{
					cell.Controls.Add(control2);
				}
				GridHeaderItem gridHeaderItem = inItem as GridHeaderItem;
				if (gridHeaderItem != null && this.Owner.OwnerGrid.IsClientCommandAssigned && this.ShowSortIcon)
				{
					Button button4;
					if (this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
					{
						button4 = new ElasticButton();
						button4.CssClass = "t-button rgActionButton ";
						((ElasticButton)button4).FirstSpanClass = "t-font-icon rgIcon rgSortAscIcon";
						((ElasticButton)button4).Text = this.Owner.OwnerGrid.SortingSettings.SortedAscToolTip;
					}
					else
					{
						button4 = new Button();
						button4.Text = " ";
					}
					button4.Attributes["id"] = string.Format("{0}__{1}__SortAsc", this.Owner.ClientID, this.UniqueName);
					button4.Style["display"] = "none";
					button4.ToolTip = this.Owner.OwnerGrid.SortingSettings.SortedAscToolTip;
					Button button5 = button4;
					button5.CssClass += "rgSortAsc";
					button4.OnClientClick = string.Format("Telerik.Web.UI.Grid.Sort($find('{0}'), '{1}'); return false;", inItem.OwnerTableView.ClientID, this.GetSortExpression());
					if (this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
					{
						cell.Controls.Add((ElasticButton)button4);
						if (this.Owner.OwnerGrid.EnableAriaSupport)
						{
							button4.Attributes.Add("aria-label", button4.ToolTip);
						}
					}
					else
					{
						cell.Controls.Add(button4);
					}
					Button button6;
					if (this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
					{
						button6 = new ElasticButton();
						button6.CssClass = "t-button rgActionButton ";
						((ElasticButton)button6).FirstSpanClass = "t-font-icon rgIcon rgSortDescIcon";
						((ElasticButton)button6).Text = this.Owner.OwnerGrid.SortingSettings.SortedDescToolTip;
						if (this.Owner.OwnerGrid.EnableAriaSupport)
						{
							button6.Attributes.Add("aria-label", this.Owner.OwnerGrid.SortingSettings.SortedDescToolTip);
						}
					}
					else
					{
						button6 = new Button();
						button6.Text = " ";
					}
					button6.Attributes["id"] = string.Format("{0}__{1}__SortDesc", this.Owner.ClientID, this.UniqueName);
					button6.Style["display"] = "none";
					button6.ToolTip = this.Owner.OwnerGrid.SortingSettings.SortedDescToolTip;
					Button button7 = button6;
					button7.CssClass += "rgSortDesc";
					button6.OnClientClick = string.Format("Telerik.Web.UI.Grid.Sort($find('{0}'), '{1}'); return false;", inItem.OwnerTableView.ClientID, this.GetSortExpression());
					if (this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
					{
						cell.Controls.Add((ElasticButton)button6);
					}
					else
					{
						cell.Controls.Add(button6);
					}
				}
				if (gridHeaderItem != null)
				{
					if (!string.IsNullOrEmpty(this.HeaderTooltip))
					{
						cell.ToolTip = this.HeaderTooltip;
					}
					if (!string.IsNullOrEmpty(this.HeaderAbbr))
					{
						cell.Attributes["abbr"] = this.HeaderAbbr;
					}
					if (!string.IsNullOrEmpty(this.HeaderAxis))
					{
						cell.Attributes["axis"] = this.HeaderAxis;
					}
				}
				return;
			}
			default:
				if (itemType != GridItemType.FilteringItem)
				{
					return;
				}
				if (inItem.OwnerTableView.AllowFilteringByColumn)
				{
					if (this.SupportsFiltering())
					{
						cell.Style["white-space"] = "nowrap";
						this.SetupFilterControls(cell);
						inItem.CellDataBound += this.inItem_CellDataBound;
						return;
					}
					if (this.cellIsEmpty(cell))
					{
						cell.Text = "&nbsp;";
					}
				}
				break;
			}
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x0008CACA File Offset: 0x0008ACCA
		private void inItem_CellDataBound(object sender, GridCellDataBoundEventArgs args)
		{
			if (args.Column == this && this.SupportsFiltering())
			{
				this.SetCurrentFilterValueToControl(args.Cell);
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06002B2D RID: 11053 RVA: 0x0008CAE9 File Offset: 0x0008ACE9
		// (set) Token: 0x06002B2E RID: 11054 RVA: 0x0008CB18 File Offset: 0x0008AD18
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string HeaderTooltip
		{
			get
			{
				if (this.ViewState["HeaderTooltip"] == null)
				{
					return "";
				}
				return (string)this.ViewState["HeaderTooltip"];
			}
			set
			{
				this.ViewState["HeaderTooltip"] = value;
				if (this.IsClone && this.owner != null)
				{
					this.owner.originalTableView.GetColumnSafe(this.UniqueName).HeaderTooltip = value;
				}
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x0008CB57 File Offset: 0x0008AD57
		// (set) Token: 0x06002B30 RID: 11056 RVA: 0x0008CB86 File Offset: 0x0008AD86
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public virtual string HeaderAbbr
		{
			get
			{
				if (this.ViewState["HeaderAbbr"] == null)
				{
					return "";
				}
				return (string)this.ViewState["HeaderAbbr"];
			}
			set
			{
				this.ViewState["HeaderAbbr"] = value;
				if (this.IsClone && this.owner != null)
				{
					this.owner.originalTableView.GetColumnSafe(this.UniqueName).HeaderAbbr = value;
				}
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06002B31 RID: 11057 RVA: 0x0008CBC5 File Offset: 0x0008ADC5
		// (set) Token: 0x06002B32 RID: 11058 RVA: 0x0008CBF4 File Offset: 0x0008ADF4
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string HeaderAxis
		{
			get
			{
				if (this.ViewState["HeaderAxis"] == null)
				{
					return "";
				}
				return (string)this.ViewState["HeaderAxis"];
			}
			set
			{
				this.ViewState["HeaderAxis"] = value;
				if (this.IsClone && this.owner != null)
				{
					this.owner.originalTableView.GetColumnSafe(this.UniqueName).HeaderAxis = value;
				}
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x0008CC34 File Offset: 0x0008AE34
		// (set) Token: 0x06002B34 RID: 11060 RVA: 0x0008CC66 File Offset: 0x0008AE66
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Color), "")]
		public virtual Color SortedBackColor
		{
			get
			{
				object obj = this.ViewState["_sbc"];
				if (obj == null)
				{
					obj = Color.Empty;
				}
				return (Color)obj;
			}
			set
			{
				this.ViewState["_sbc"] = value;
			}
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x0008CC7E File Offset: 0x0008AE7E
		protected virtual string GetFilterDataField()
		{
			return string.Empty;
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x0008CC85 File Offset: 0x0008AE85
		public virtual bool SupportsFiltering()
		{
			return false;
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x0008CC88 File Offset: 0x0008AE88
		internal virtual void SetCurrentFilterValueFromFilterCommand(string value)
		{
			this.CurrentFilterValue = value;
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06002B38 RID: 11064 RVA: 0x0008CC94 File Offset: 0x0008AE94
		// (set) Token: 0x06002B39 RID: 11065 RVA: 0x0008CD00 File Offset: 0x0008AF00
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string CurrentFilterValue
		{
			get
			{
				if (this.IsClone && this.owner != null && this.owner.FilterValues.ContainsKey(this.UniqueName))
				{
					return this.owner.FilterValues[this.UniqueName];
				}
				object obj = this.ViewState["_cfv"];
				if (obj == null)
				{
					obj = "";
				}
				return (string)obj;
			}
			set
			{
				if (!this.IsClone || this.owner == null)
				{
					this.ViewState["_cfv"] = value;
					return;
				}
				if (!this.owner.FilterValues.ContainsKey(this.UniqueName))
				{
					this.owner.FilterValues.Add(this.UniqueName, string.Empty);
				}
				if (string.IsNullOrEmpty(value))
				{
					this.owner.FilterValues.Remove(this.UniqueName);
					return;
				}
				this.owner.FilterValues[this.UniqueName] = value;
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x0008CD9C File Offset: 0x0008AF9C
		// (set) Token: 0x06002B3B RID: 11067 RVA: 0x0008CDC9 File Offset: 0x0008AFC9
		[DefaultValue("")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public virtual string AndCurrentFilterValue
		{
			get
			{
				object obj = this.ViewState["_acfv"];
				if (obj == null)
				{
					obj = "";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["_acfv"] = value;
				if (this.IsClone && this.owner != null)
				{
					this.owner.originalTableView.GetColumnSafe(this.UniqueName).CurrentFilterValue = value;
				}
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x0008CE08 File Offset: 0x0008B008
		// (set) Token: 0x06002B3D RID: 11069 RVA: 0x0008CE37 File Offset: 0x0008B037
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string ColumnGroupName
		{
			get
			{
				if (this.ViewState["_colGroupName"] == null)
				{
					return "";
				}
				return (string)this.ViewState["_colGroupName"];
			}
			set
			{
				this.ViewState["_colGroupName"] = value;
				if (this.IsClone && this.owner != null)
				{
					this.owner.originalTableView.GetColumnSafe(this.UniqueName).ColumnGroupName = value;
				}
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x0008CE78 File Offset: 0x0008B078
		// (set) Token: 0x06002B3F RID: 11071 RVA: 0x0008CF20 File Offset: 0x0008B120
		[NotifyParentProperty(true)]
		[Browsable(false)]
		[DefaultValue(false)]
		public bool Selected
		{
			get
			{
				bool result = false;
				if (this.Selectable && this.Owner != null && this.Owner.Items.Count > 0)
				{
					result = true;
					foreach (object obj in this.Owner.Items)
					{
						GridDataItem gridDataItem = (GridDataItem)obj;
						if (gridDataItem[this] is GridTableCell)
						{
							if (!((GridTableCell)gridDataItem[this]).Selected)
							{
								result = false;
							}
						}
						else
						{
							result = false;
						}
					}
				}
				return result;
			}
			set
			{
				if (this.Owner != null && this.Owner.OwnerGrid != null && this.Owner.OwnerGrid.Items.Count > 0)
				{
					foreach (object obj in this.Owner.OwnerGrid.Items)
					{
						GridDataItem gridDataItem = (GridDataItem)obj;
						if (gridDataItem[this] is GridTableCell)
						{
							((GridTableCell)gridDataItem[this]).Selected = value;
						}
					}
				}
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x0008CFCC File Offset: 0x0008B1CC
		internal int SelectedCellsCount
		{
			get
			{
				int num = 0;
				if (this.Owner != null && this.Owner.OwnerGrid != null)
				{
					GridIndexCollection selectedCellIndexes = this.Owner.OwnerGrid.SelectedCellIndexes;
					foreach (object obj in selectedCellIndexes)
					{
						if (obj.ToString().Split(new char[]
						{
							'&'
						})[1] == this.UniqueName)
						{
							num++;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06002B41 RID: 11073 RVA: 0x0008D070 File Offset: 0x0008B270
		public virtual bool Selectable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06002B42 RID: 11074 RVA: 0x0008D073 File Offset: 0x0008B273
		protected virtual bool Sortable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06002B43 RID: 11075 RVA: 0x0008D078 File Offset: 0x0008B278
		// (set) Token: 0x06002B44 RID: 11076 RVA: 0x0008D0E8 File Offset: 0x0008B2E8
		[NotifyParentProperty(true)]
		[DefaultValue(GridKnownFunction.NoFilter)]
		public virtual GridKnownFunction CurrentFilterFunction
		{
			get
			{
				if (this.IsClone && this.owner != null && this.owner.FilterFunctions.ContainsKey(this.UniqueName))
				{
					return this.owner.FilterFunctions[this.UniqueName];
				}
				object obj = this.ViewState["_cff"];
				if (obj == null)
				{
					obj = GridKnownFunction.NoFilter;
				}
				return (GridKnownFunction)obj;
			}
			set
			{
				if (this.IsClone && this.owner != null)
				{
					if (!this.owner.FilterFunctions.ContainsKey(this.UniqueName))
					{
						this.owner.FilterFunctions.Add(this.UniqueName, GridKnownFunction.NoFilter);
					}
					if (value == GridKnownFunction.NoFilter)
					{
						this.owner.FilterFunctions.Remove(this.UniqueName);
					}
					else
					{
						this.owner.FilterFunctions[this.UniqueName] = value;
					}
				}
				else
				{
					this.ViewState["_cff"] = value;
				}
				if (value != GridKnownFunction.NoFilter)
				{
					this.AutoPostBackFilterFunction = value;
				}
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06002B45 RID: 11077 RVA: 0x0008D18C File Offset: 0x0008B38C
		// (set) Token: 0x06002B46 RID: 11078 RVA: 0x0008D1D4 File Offset: 0x0008B3D4
		internal GridKnownFunction AutoPostBackFilterFunction
		{
			get
			{
				object obj = this.ViewState["_Acff"];
				if (obj == null)
				{
					obj = ((this.DataType == typeof(string)) ? GridKnownFunction.Contains : GridKnownFunction.EqualTo);
				}
				return (GridKnownFunction)obj;
			}
			set
			{
				this.ViewState["_Acff"] = value;
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06002B47 RID: 11079 RVA: 0x0008D1EC File Offset: 0x0008B3EC
		// (set) Token: 0x06002B48 RID: 11080 RVA: 0x0008D21C File Offset: 0x0008B41C
		[DefaultValue(GridKnownFunction.NoFilter)]
		[NotifyParentProperty(true)]
		public virtual GridKnownFunction AndCurrentFilterFunction
		{
			get
			{
				object obj = this.ViewState["_acff"];
				if (obj == null)
				{
					obj = GridKnownFunction.NoFilter;
				}
				return (GridKnownFunction)obj;
			}
			set
			{
				this.ViewState["_acff"] = value;
				if (this.IsClone && this.owner != null && this.owner.originalTableView != null)
				{
					this.owner.originalTableView.GetColumnSafe(this.UniqueName).CurrentFilterFunction = value;
					if (value == GridKnownFunction.NoFilter)
					{
						this.owner.originalTableView.GetColumnSafe(this.UniqueName).CurrentFilterValue = "";
					}
				}
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06002B49 RID: 11081 RVA: 0x0008D29C File Offset: 0x0008B49C
		// (set) Token: 0x06002B4A RID: 11082 RVA: 0x0008D2DA File Offset: 0x0008B4DA
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		public virtual int? FilterDelay
		{
			get
			{
				if (this.ViewState["FilterDelay"] == null)
				{
					return null;
				}
				return (int?)this.ViewState["FilterDelay"];
			}
			set
			{
				this.ViewState["FilterDelay"] = value;
			}
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06002B4B RID: 11083 RVA: 0x0008D2F2 File Offset: 0x0008B4F2
		// (set) Token: 0x06002B4C RID: 11084 RVA: 0x0008D31D File Offset: 0x0008B51D
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public virtual bool Exportable
		{
			get
			{
				return this.ViewState["Exportable"] == null || (bool)this.ViewState["Exportable"];
			}
			set
			{
				this.ViewState["Exportable"] = value;
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06002B4D RID: 11085 RVA: 0x0008D335 File Offset: 0x0008B535
		// (set) Token: 0x06002B4E RID: 11086 RVA: 0x0008D360 File Offset: 0x0008B560
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public virtual bool EnableHeaderContextMenu
		{
			get
			{
				return this.ViewState["EnableHeaderContextMenu"] == null || (bool)this.ViewState["EnableHeaderContextMenu"];
			}
			set
			{
				this.ViewState["EnableHeaderContextMenu"] = value;
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06002B4F RID: 11087 RVA: 0x0008D378 File Offset: 0x0008B578
		// (set) Token: 0x06002B50 RID: 11088 RVA: 0x0008D3A3 File Offset: 0x0008B5A3
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public virtual bool ShowFilterIcon
		{
			get
			{
				return this.ViewState["ShowFilterIcon"] == null || (bool)this.ViewState["ShowFilterIcon"];
			}
			set
			{
				this.ViewState["ShowFilterIcon"] = value;
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06002B51 RID: 11089 RVA: 0x0008D3BC File Offset: 0x0008B5BC
		// (set) Token: 0x06002B52 RID: 11090 RVA: 0x0008D3EA File Offset: 0x0008B5EA
		[DefaultValue(GridFilterListOptions.VaryByDataType)]
		[NotifyParentProperty(true)]
		public virtual GridFilterListOptions FilterListOptions
		{
			get
			{
				object obj = this.ViewState["_flo"];
				if (obj == null)
				{
					obj = GridFilterListOptions.VaryByDataType;
				}
				return (GridFilterListOptions)obj;
			}
			set
			{
				this.ViewState["_flo"] = value;
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06002B53 RID: 11091 RVA: 0x0008D404 File Offset: 0x0008B604
		// (set) Token: 0x06002B54 RID: 11092 RVA: 0x0008D432 File Offset: 0x0008B632
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public virtual bool AutoPostBackOnFilter
		{
			get
			{
				object obj = this.ViewState["_apbof"];
				if (obj == null)
				{
					obj = false;
				}
				return (bool)obj;
			}
			set
			{
				this.ViewState["_apbof"] = value;
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06002B55 RID: 11093 RVA: 0x0008D44C File Offset: 0x0008B64C
		// (set) Token: 0x06002B56 RID: 11094 RVA: 0x0008D4A6 File Offset: 0x0008B6A6
		[DefaultValue("Filter")]
		[NotifyParentProperty(true)]
		public virtual string FilterImageToolTip
		{
			get
			{
				object obj = this.ViewState["_fitt"];
				if (obj == null)
				{
					if (this.owner != null && this.owner.OwnerGrid != null)
					{
						obj = this.owner.OwnerGrid.Localization.FilterImageToolTip;
					}
					else
					{
						obj = "Filter";
					}
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["_fitt"] = value;
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06002B57 RID: 11095 RVA: 0x0008D4B9 File Offset: 0x0008B6B9
		// (set) Token: 0x06002B58 RID: 11096 RVA: 0x0008D4D9 File Offset: 0x0008B6D9
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string FilterControlToolTip
		{
			get
			{
				return (string)(this.ViewState["FilterControlToolTip"] ?? string.Empty);
			}
			set
			{
				this.ViewState["FilterControlToolTip"] = value;
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06002B59 RID: 11097 RVA: 0x0008D4EC File Offset: 0x0008B6EC
		// (set) Token: 0x06002B5A RID: 11098 RVA: 0x0008D547 File Offset: 0x0008B747
		[NotifyParentProperty(true)]
		[UrlProperty]
		[DefaultValue("")]
		public virtual string FilterImageUrl
		{
			get
			{
				object obj = this.ViewState["_fiurl"];
				if (obj != null)
				{
					return this.owner.OwnerGrid.ResolveUrl((string)obj);
				}
				if (this.Owner != null)
				{
					return this.Owner.OwnerGrid.ResolveGridImageUrl("Filter.gif");
				}
				return "";
			}
			set
			{
				this.ViewState["_fiurl"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x0008D560 File Offset: 0x0008B760
		protected virtual bool ShouldSerializeFilterImageUrl()
		{
			return this.Owner != null && this.Owner.OwnerGrid.ShouldSerializeImageUrl(this.FilterImageUrl);
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06002B5C RID: 11100 RVA: 0x0008D584 File Offset: 0x0008B784
		// (set) Token: 0x06002B5D RID: 11101 RVA: 0x0008D5DF File Offset: 0x0008B7DF
		[UrlProperty]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string SortAscImageUrl
		{
			get
			{
				object obj = this.ViewState["_saiurl"];
				if (obj != null)
				{
					return this.owner.OwnerGrid.ResolveUrl((string)obj);
				}
				if (this.Owner != null)
				{
					return this.Owner.OwnerGrid.ResolveGridImageUrl("SortAsc.gif");
				}
				return "";
			}
			set
			{
				this.ViewState["_saiurl"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x0008D5F8 File Offset: 0x0008B7F8
		protected virtual bool ShouldSerializeSortAscImageUrl()
		{
			return this.Owner != null && this.Owner.OwnerGrid.ShouldSerializeImageUrl(this.SortAscImageUrl);
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06002B5F RID: 11103 RVA: 0x0008D61C File Offset: 0x0008B81C
		// (set) Token: 0x06002B60 RID: 11104 RVA: 0x0008D677 File Offset: 0x0008B877
		[NotifyParentProperty(true)]
		[UrlProperty]
		[DefaultValue("")]
		public virtual string SortDescImageUrl
		{
			get
			{
				object obj = this.ViewState["_sdiurl"];
				if (obj != null)
				{
					return this.owner.OwnerGrid.ResolveUrl((string)obj);
				}
				if (this.Owner != null)
				{
					return this.Owner.OwnerGrid.ResolveGridImageUrl("SortDesc.gif");
				}
				return "";
			}
			set
			{
				this.ViewState["_sdiurl"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x0008D690 File Offset: 0x0008B890
		protected virtual bool ShouldSerializeSortDescImageUrl()
		{
			return this.Owner != null && this.Owner.OwnerGrid.ShouldSerializeImageUrl(this.SortDescImageUrl);
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06002B62 RID: 11106 RVA: 0x0008D6B2 File Offset: 0x0008B8B2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string DataTypeName
		{
			get
			{
				return this.DataType.ToString();
			}
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x0008D6C0 File Offset: 0x0008B8C0
		public virtual void RefreshCurrentFilterValue(GridFilteringItem filteringItem, string functionName)
		{
			TableCell cell = filteringItem[this.UniqueName];
			string currentFilterValueFromControl = this.GetCurrentFilterValueFromControl(cell);
			this.CurrentFilterValue = currentFilterValueFromControl;
			try
			{
				this.CurrentFilterFunction = (GridKnownFunction)Enum.Parse(typeof(GridKnownFunction), functionName);
			}
			catch (Exception)
			{
				throw new GridException(string.Format("{0} is not supported filter function for {1}. Custom filter functions must be handled in the ItemCommand event handler. Set e.Canceled=true to stop the built-in filtering.", functionName, this.ColumnType));
			}
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x0008D730 File Offset: 0x0008B930
		public virtual void RefreshCurrentFilterValue(GridFilteringItem filteringItem)
		{
			TableCell cell = filteringItem[this.UniqueName];
			string currentFilterValueFromControl = this.GetCurrentFilterValueFromControl(cell);
			this.CurrentFilterValue = currentFilterValueFromControl;
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x0008D75C File Offset: 0x0008B95C
		protected virtual void SetCurrentFilterValueToControl(TableCell cell)
		{
			if (this.DataType != typeof(bool))
			{
				TextBox textBox = null;
				foreach (object obj in cell.Controls)
				{
					Control control = (Control)obj;
					textBox = (control as TextBox);
					if (textBox != null)
					{
						break;
					}
				}
				if (textBox != null)
				{
					textBox.Text = this.CurrentFilterValue;
					return;
				}
			}
			else
			{
				CheckBox checkBox = null;
				foreach (object obj2 in cell.Controls)
				{
					Control control2 = (Control)obj2;
					checkBox = (control2 as CheckBox);
					if (checkBox != null)
					{
						break;
					}
				}
				if (checkBox != null && !string.IsNullOrEmpty(this.CurrentFilterValue))
				{
					checkBox.Checked = bool.Parse(this.CurrentFilterValue);
				}
			}
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x0008D860 File Offset: 0x0008BA60
		internal void SetCurrentFilterValueToControlInternal(TableCell cell)
		{
			this.SetCurrentFilterValueToControl(cell);
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06002B67 RID: 11111 RVA: 0x0008D869 File Offset: 0x0008BA69
		// (set) Token: 0x06002B68 RID: 11112 RVA: 0x0008D871 File Offset: 0x0008BA71
		public virtual string[] ListOfFilterValues { get; set; }

		// Token: 0x06002B69 RID: 11113 RVA: 0x0008D87C File Offset: 0x0008BA7C
		protected virtual string GetCurrentFilterValueFromControl(TableCell cell)
		{
			string text = this.GetFilterValue(cell);
			if (string.IsNullOrEmpty(text))
			{
				text = this.CurrentFilterValue;
			}
			return text;
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x0008D8A4 File Offset: 0x0008BAA4
		private string GetFilterValue(TableCell cell)
		{
			if (this.DataType != typeof(bool))
			{
				TextBox textBox = null;
				foreach (object obj in cell.Controls)
				{
					Control control = (Control)obj;
					textBox = (control as TextBox);
					if (textBox != null)
					{
						break;
					}
				}
				if (textBox == null)
				{
					return string.Empty;
				}
				return textBox.Text;
			}
			else
			{
				CheckBox checkBox = null;
				foreach (object obj2 in cell.Controls)
				{
					Control control2 = (Control)obj2;
					checkBox = (control2 as CheckBox);
					if (checkBox != null)
					{
						break;
					}
				}
				if (checkBox == null)
				{
					return string.Empty;
				}
				return checkBox.Checked.ToString();
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06002B6B RID: 11115 RVA: 0x0008D99C File Offset: 0x0008BB9C
		// (set) Token: 0x06002B6C RID: 11116 RVA: 0x0008D9A4 File Offset: 0x0008BBA4
		[TemplateContainer(typeof(GridItem), BindingDirection.TwoWay)]
		[Description("Gets or sets the Controls, which will be rendered in the filter item cell of the column.")]
		[DefaultValue(null)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate FilterTemplate
		{
			get
			{
				return this.filterTemplate;
			}
			set
			{
				this.filterTemplate = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x0008D9B4 File Offset: 0x0008BBB4
		public virtual string EvaluateFilterExpression(GridFilteringItem filteringItem)
		{
			if (!this.SupportsFiltering())
			{
				return string.Empty;
			}
			if (this.Owner.OwnerGrid.FilterType == GridFilterType.HeaderContext)
			{
				return this.EvaluateFilterExpression();
			}
			TableCell cell = filteringItem[this.UniqueName];
			string currentFilterValueFromControl = this.GetCurrentFilterValueFromControl(cell);
			string value = this.Owner.OwnerGrid.EnableLinqExpressions ? ")||(" : ") OR (";
			if (this.ListOfFilterValues != null && this.ListOfFilterValues.Length > 0)
			{
				if (this.CurrentFilterFunction == GridKnownFunction.EqualTo)
				{
					GridFilterFunction gridFilterFunction = new GridFilterFunction(this.CurrentFilterFunction);
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("(");
					stringBuilder.Append(gridFilterFunction.GetFunctionString(this.GetFilterDataField(), this.ListOfFilterValues[0], this.DataType, filteringItem.OwnerTableView));
					for (int i = 1; i < this.ListOfFilterValues.Length; i++)
					{
						string functionString = gridFilterFunction.GetFunctionString(this.GetFilterDataField(), this.ListOfFilterValues[i], this.DataType, filteringItem.OwnerTableView);
						if (!string.IsNullOrEmpty(functionString))
						{
							stringBuilder.Append(value);
							stringBuilder.Append(functionString);
						}
					}
					stringBuilder.Append(")");
					return stringBuilder.ToString();
				}
				if (this.CurrentFilterFunction == GridKnownFunction.NoFilter)
				{
					this.ListOfFilterValues = null;
				}
			}
			if (string.IsNullOrEmpty(currentFilterValueFromControl) && !this.FunctionTakesNoArguments(this.CurrentFilterFunction))
			{
				return "";
			}
			GridFilterFunction gridFilterFunction2 = new GridFilterFunction(this.CurrentFilterFunction);
			return gridFilterFunction2.GetFunctionString(this.GetFilterDataField(), currentFilterValueFromControl, this.DataType, filteringItem.OwnerTableView);
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x0008DB47 File Offset: 0x0008BD47
		// (set) Token: 0x06002B6F RID: 11119 RVA: 0x0008DB4F File Offset: 0x0008BD4F
		internal int RowSpan { get; set; }

		// Token: 0x06002B70 RID: 11120 RVA: 0x0008DB58 File Offset: 0x0008BD58
		private string ConvertToCultureSpecificString(string val)
		{
			string result = string.Empty;
			double num;
			if (double.TryParse(val, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out num))
			{
				result = num.ToString(Thread.CurrentThread.CurrentCulture.NumberFormat);
			}
			return result;
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x0008DB98 File Offset: 0x0008BD98
		private string ConvertToInvCultureString(string val)
		{
			string result = string.Empty;
			double num;
			if (double.TryParse(val, NumberStyles.Any, Thread.CurrentThread.CurrentCulture.NumberFormat, out num))
			{
				result = num.ToString(NumberFormatInfo.InvariantInfo);
			}
			return result;
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x0008DBD8 File Offset: 0x0008BDD8
		private string ConstructFilterExprOnNumericColumn(GridFilterFunction filterFunc, ref string filterValue)
		{
			string text = string.Empty;
			string result = string.Empty;
			if (this.TriggeredFilterCommand)
			{
				text = filterValue;
				filterValue = this.ConvertToCultureSpecificString(text);
			}
			else
			{
				text = this.ConvertToInvCultureString(filterValue);
			}
			if (this.Owner.OwnerGrid.EnableLinqExpressions)
			{
				result = filterFunc.GetFunctionString(this.GetFilterDataField(), filterValue, this.DataType, this.Owner);
			}
			else
			{
				result = filterFunc.GetFunctionString(this.GetFilterDataField(), text, this.DataType, this.Owner);
			}
			return result;
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x0008DC5C File Offset: 0x0008BE5C
		public virtual string EvaluateFilterExpression()
		{
			if (!this.SupportsFiltering())
			{
				return string.Empty;
			}
			string text = string.Empty;
			GridNumericColumn gridNumericColumn = this as GridNumericColumn;
			if (!string.IsNullOrEmpty(this.CurrentFilterValue) || this.FunctionTakesNoArguments(this.CurrentFilterFunction))
			{
				GridFilterFunction gridFilterFunction = new GridFilterFunction(this.CurrentFilterFunction);
				if (gridNumericColumn != null)
				{
					string currentFilterValue = this.CurrentFilterValue;
					text = this.ConstructFilterExprOnNumericColumn(gridFilterFunction, ref currentFilterValue);
					this.CurrentFilterValue = currentFilterValue;
				}
				else
				{
					text = gridFilterFunction.GetFunctionString(this.GetFilterDataField(), this.CurrentFilterValue, this.DataType, this.Owner);
				}
			}
			if ((!string.IsNullOrEmpty(this.AndCurrentFilterValue) || this.FunctionTakesNoArguments(this.AndCurrentFilterFunction)) && this.AndCurrentFilterFunction != GridKnownFunction.NoFilter)
			{
				GridFilterFunction gridFilterFunction = new GridFilterFunction(this.AndCurrentFilterFunction);
				if (!string.IsNullOrEmpty(text))
				{
					text += " AND ";
				}
				if (gridNumericColumn != null)
				{
					string andCurrentFilterValue = this.AndCurrentFilterValue;
					text += this.ConstructFilterExprOnNumericColumn(gridFilterFunction, ref andCurrentFilterValue);
					this.AndCurrentFilterValue = andCurrentFilterValue;
				}
				else
				{
					text += gridFilterFunction.GetFunctionString(this.GetFilterDataField(), this.AndCurrentFilterValue, this.DataType, this.Owner);
				}
			}
			if (this.owner.OwnerGrid.FilterType == GridFilterType.HeaderContext)
			{
				GridFilterFunction gridFilterFunction = new GridFilterFunction(GridKnownFunction.EqualTo);
				string value = this.Owner.OwnerGrid.EnableLinqExpressions ? ")||(" : ") OR (";
				if (this.ListOfFilterValues != null && this.ListOfFilterValues.Length > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("(");
					stringBuilder.Append(gridFilterFunction.GetFunctionString(this.GetFilterDataField(), this.ListOfFilterValues[0], this.DataType, this.Owner));
					for (int i = 1; i < this.ListOfFilterValues.Length; i++)
					{
						string functionString = gridFilterFunction.GetFunctionString(this.GetFilterDataField(), this.ListOfFilterValues[i], this.DataType, this.Owner);
						if (!string.IsNullOrEmpty(functionString))
						{
							stringBuilder.Append(value);
							stringBuilder.Append(functionString);
						}
					}
					stringBuilder.Append(")");
					string value2 = stringBuilder.ToString();
					if (!string.IsNullOrEmpty(value2))
					{
						bool flag = !string.IsNullOrEmpty(text);
						if (flag)
						{
							text += " AND (";
						}
						text += stringBuilder.ToString();
						if (flag)
						{
							text += ")";
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x0008DEB6 File Offset: 0x0008C0B6
		protected internal bool FunctionTakesNoArguments(GridKnownFunction filterFunction)
		{
			return filterFunction == GridKnownFunction.IsEmpty || filterFunction == GridKnownFunction.NotIsEmpty || filterFunction == GridKnownFunction.IsNull || filterFunction == GridKnownFunction.NotIsNull;
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x0008DED0 File Offset: 0x0008C0D0
		protected virtual void SetupFilterControls(TableCell cell)
		{
			if (this.filterTemplate != null)
			{
				this.filterTemplate.InstantiateIn(cell);
				return;
			}
			TextBox textBox = new TextBox();
			textBox.ID = string.Format("FilterTextBox_{0}", this.UniqueName);
			textBox.Columns = 10;
			textBox.Attributes["alt"] = this.FilterControlAltText;
			textBox.ToolTip = this.FilterControlToolTip;
			textBox.CssClass = "rgFilterBox";
			if (this.Owner.OwnerGrid.EnableAriaSupport)
			{
				textBox.Attributes.Add("aria-label", this.HeaderText);
			}
			if (!this.FilterControlWidth.IsEmpty)
			{
				textBox.Width = this.FilterControlWidth;
			}
			if (this.DataType != typeof(bool))
			{
				cell.Controls.Add(textBox);
				if (this.Owner.OwnerGrid.ResolvedRenderMode != RenderMode.Mobile)
				{
					int? filterDelay;
					if (this.FilterDelay > 0)
					{
						filterDelay = this.FilterDelay;
					}
					else
					{
						filterDelay = new int?(0);
					}
					string format = "$find(\"{0}\")._filterOnKey{1}WithDelay(event,\"{2}\",\"{3}\",\"{4}\")";
					string text = string.Format("$find(\"{0}\")._filterNoDelay(\"{1}\",\"{2}\")", this.Owner.ClientID, textBox.ClientID, this.UniqueName);
					string value = "if((event.keyCode == 13)) return false;";
					if (this.AutoPostBackOnFilter)
					{
						textBox.Attributes["onchange"] = text;
					}
					if (this.FilterDelay != null)
					{
						textBox.Attributes["onkeydown"] = string.Format("{0}", string.Format(format, new object[]
						{
							this.Owner.ClientID,
							"Down",
							textBox.ClientID,
							this.UniqueName,
							filterDelay
						}));
						textBox.Attributes["onkeypress"] = string.Format("{0}", string.Format(format, new object[]
						{
							this.Owner.ClientID,
							"Press",
							textBox.ClientID,
							this.UniqueName,
							filterDelay
						}));
					}
					else if (this.AutoPostBackOnFilter)
					{
						textBox.Attributes["onkeypress"] = string.Format("if(event.keyCode == 13){{ this.blur(); event.cancelBubble = true; event.returnValue = false; if (event.stopPropagation){{ event.stopPropagation(); event.preventDefault();}} {0} }}", text);
					}
					else
					{
						textBox.Attributes["onkeypress"] = value;
					}
				}
			}
			else
			{
				CheckBox checkBox = new CheckBox();
				checkBox.ID = string.Format("FilterCheckBox_{0}", this.UniqueName);
				AccessibilityHelper.AddToolTip(checkBox, this.FilterControlToolTip);
				checkBox.ToolTip = this.FilterControlToolTip;
				if (this.Owner.OwnerGrid.EnableAriaSupport)
				{
					checkBox.InputAttributes.Add("aria-label", this.HeaderText);
				}
				cell.Controls.Add(checkBox);
				if (this.AutoPostBackOnFilter)
				{
					string value2 = string.Format("setTimeout(function(){{$find(\"{0}\").filter(\"{1}\", {2});}}, {3});", new object[]
					{
						this.Owner.ClientID,
						this.UniqueName,
						string.Format("$get(\"{0}\").checked", checkBox.ClientID),
						0
					});
					checkBox.Attributes["onclick"] = value2;
				}
			}
			if (this.ShowFilterIcon)
			{
				if (this.Owner.OwnerGrid.ShouldRenderImg(this.FilterImageUrl))
				{
					System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image();
					image.ImageUrl = this.FilterImageUrl;
					image.AlternateText = this.FilterImageToolTip;
					image.ToolTip = this.FilterImageToolTip;
					image.BorderWidth = Unit.Pixel(0);
					image.ID = string.Format("Filter_{0}", this.UniqueName);
					cell.Controls.Add(image);
					return;
				}
				Button button;
				if (this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					button = new ElasticButton("t-font-icon rgIcon rgFilterIcon");
					button.CssClass = "t-button rgActionButton ";
					button.Text = this.FilterImageToolTip;
					if (this.Owner.OwnerGrid.EnableAriaSupport)
					{
						button.Attributes.Add("aria-label", this.FilterImageToolTip);
					}
				}
				else
				{
					button = new Button();
					button.Text = " ";
				}
				Button button2 = button;
				button2.CssClass += "rgFilter";
				RadGrid.ToggleColumnFilteredClass(button, this);
				button.ToolTip = this.FilterImageToolTip;
				button.ID = string.Format("Filter_{0}", this.UniqueName);
				cell.Controls.Add(button);
			}
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x0008E374 File Offset: 0x0008C574
		protected virtual ArrayList GetFilterFunctionsList(GridFilterListOptions options, ArrayList sourceList)
		{
			if (options == GridFilterListOptions.VaryByDataType)
			{
				sourceList.Remove(GridKnownFunction.Custom.ToString());
				if (this.DataType != typeof(string))
				{
					sourceList.Remove(GridKnownFunction.StartsWith.ToString());
					sourceList.Remove(GridKnownFunction.EndsWith.ToString());
					sourceList.Remove(GridKnownFunction.Contains.ToString());
					sourceList.Remove(GridKnownFunction.DoesNotContain.ToString());
					sourceList.Remove(GridKnownFunction.IsEmpty.ToString());
					sourceList.Remove(GridKnownFunction.NotIsEmpty.ToString());
				}
				return sourceList;
			}
			if (options == GridFilterListOptions.VaryByDataTypeAllowCustom && this.DataType != typeof(string))
			{
				sourceList.Remove(GridKnownFunction.StartsWith.ToString());
				sourceList.Remove(GridKnownFunction.EndsWith.ToString());
				sourceList.Remove(GridKnownFunction.Contains.ToString());
				sourceList.Remove(GridKnownFunction.DoesNotContain.ToString());
				sourceList.Remove(GridKnownFunction.IsEmpty.ToString());
				sourceList.Remove(GridKnownFunction.NotIsEmpty.ToString());
			}
			return sourceList;
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x0008E4A0 File Offset: 0x0008C6A0
		private GridImageButton CreateSortIcon()
		{
			return new GridImageButton(this);
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x0008E4B8 File Offset: 0x0008C6B8
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public virtual void PrepareCell(TableCell cell, GridItem item)
		{
			if (item is GridHeaderItem && !string.IsNullOrEmpty(this.GetSortExpression()) && item.OwnerTableView.SortExpressions.ContainsExpression(this.GetSortExpression()))
			{
				GridSortExpression expression = item.OwnerTableView.SortExpressions.GetExpression(this.GetSortExpression());
				if (this.Owner.OwnerGrid.ShouldRenderImg(this.SortAscImageUrl))
				{
					if (this.sortIcon == null)
					{
						this.sortIcon = this.CreateSortIcon();
					}
					GridImageButton gridImageButton = (GridImageButton)this.sortIcon;
					switch (expression.SortOrder)
					{
					case GridSortOrder.None:
						gridImageButton.Visible = false;
						break;
					case GridSortOrder.Ascending:
						gridImageButton.AlternateText = this.Owner.OwnerGrid.SortingSettings.SortedAscToolTip;
						gridImageButton.ToolTip = this.Owner.OwnerGrid.SortingSettings.SortedAscToolTip;
						gridImageButton.ImageUrl = this.SortAscImageUrl;
						gridImageButton.BorderWidth = Unit.Empty;
						break;
					case GridSortOrder.Descending:
						gridImageButton.AlternateText = this.Owner.OwnerGrid.SortingSettings.SortedDescToolTip;
						gridImageButton.ToolTip = this.Owner.OwnerGrid.SortingSettings.SortedDescToolTip;
						gridImageButton.ImageUrl = this.SortDescImageUrl;
						break;
					}
				}
				else
				{
					if (this.sortIcon == null)
					{
						this.sortIcon = new Button();
						if (this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
						{
							this.sortIcon = new ElasticButton();
						}
					}
					ElasticButton elasticButton = this.sortIcon as ElasticButton;
					Button button;
					if ((this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight) && elasticButton != null)
					{
						elasticButton.FirstSpanClass = "t-font-icon rgIcon ";
						button = elasticButton;
						button.CssClass = "t-button rgActionButton ";
					}
					else
					{
						button = (Button)this.sortIcon;
					}
					if (this.Owner.AutoGenerateColumns && cell.Controls.Count > 2)
					{
						Button button2 = cell.Controls[2] as Button;
						if (button2 != null)
						{
							button = button2;
						}
					}
					switch (expression.SortOrder)
					{
					case GridSortOrder.None:
						button.Visible = false;
						break;
					case GridSortOrder.Ascending:
					{
						button.ToolTip = this.Owner.OwnerGrid.SortingSettings.SortedAscToolTip;
						Button button3 = button;
						button3.CssClass += "rgSortAsc";
						if (this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
						{
							ElasticButton elasticButton2 = (ElasticButton)button;
							elasticButton2.FirstSpanClass += "rgSortAscIcon";
							((ElasticButton)button).Text = (this.Owner.OwnerGrid.IsExporting ? string.Empty : button.ToolTip);
							if (this.Owner.OwnerGrid.EnableAriaSupport && !this.Owner.OwnerGrid.IsExporting)
							{
								((ElasticButton)button).Attributes.Add("aria-label", button.ToolTip);
							}
						}
						break;
					}
					case GridSortOrder.Descending:
					{
						button.ToolTip = this.Owner.OwnerGrid.SortingSettings.SortedDescToolTip;
						Button button4 = button;
						button4.CssClass += "rgSortDesc";
						if (this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || this.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
						{
							ElasticButton elasticButton3 = (ElasticButton)button;
							elasticButton3.FirstSpanClass += "rgSortDescIcon";
							((ElasticButton)button).Text = (this.Owner.OwnerGrid.IsExporting ? string.Empty : button.ToolTip);
							if (this.Owner.OwnerGrid.EnableAriaSupport && !this.Owner.OwnerGrid.IsExporting)
							{
								((ElasticButton)button).Attributes.Add("aria-label", button.ToolTip);
							}
						}
						break;
					}
					}
				}
			}
			GridSortExpression gridSortExpression = null;
			if (item.OwnerTableView.SortExpressions.TryGetExpression(this.GetSortExpression(), out gridSortExpression) && gridSortExpression != null && gridSortExpression.SortOrder != GridSortOrder.None && this.sortIcon != null)
			{
				if (this.Owner.OwnerGrid.SortingSettings.EnableSkinSortStyles && !cell.CssClass.EndsWith(" rgSorted"))
				{
					if (item is GridHeaderItem)
					{
						cell.CssClass = cell.CssClass + " " + this.Owner.RenderHeaderStyle.CssClass + " rgSorted";
					}
					else if (item is GridDataItem)
					{
						cell.CssClass += " rgSorted";
					}
					cell.CssClass = cell.CssClass.Trim();
				}
				if (this.SortedBackColor != Color.Empty || this.Owner.OwnerGrid.SortingSettings.SortedBackColor != Color.Empty)
				{
					if (this.Owner.OwnerGrid.SortingSettings.SortedBackColor != Color.Empty)
					{
						cell.BackColor = this.Owner.OwnerGrid.SortingSettings.SortedBackColor;
					}
					if (this.SortedBackColor != Color.Empty)
					{
						cell.BackColor = this.SortedBackColor;
					}
				}
			}
			if (item is GridFilteringItem && this.Owner.OwnerGrid.ResolvedRenderMode != RenderMode.Mobile)
			{
				WebControl webControl = (WebControl)item.FindControl(string.Format("Filter_{0}", this.UniqueName));
				if (webControl != null)
				{
					webControl.Attributes["onclick"] = string.Format("$find(\"{0}\")._showFilterMenu(\"{1}\", \"{2}\", event); return false;", this.OwnerGridID, item.Parent.Parent.Parent.ClientID, this.UniqueName);
				}
			}
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x0008EAA8 File Offset: 0x0008CCA8
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				if (array[0] != null)
				{
					((IStateManager)this.ViewState).LoadViewState(array[0]);
				}
				if (array[1] != null)
				{
					((IStateManager)this.ItemStyle).LoadViewState(array[1]);
				}
				if (array[2] != null)
				{
					((IStateManager)this.HeaderStyle).LoadViewState(array[2]);
				}
				if (array[3] != null)
				{
					((IStateManager)this.FooterStyle).LoadViewState(array[3]);
				}
				if (array[4] != null)
				{
					this._uniqueName = (string)array[4];
				}
			}
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x0008EB1E File Offset: 0x0008CD1E
		protected virtual void OnColumnChanged()
		{
			if (this.owner != null && this.owner.OwnerGrid != null)
			{
				this.owner.OwnerGrid.OnColumnsChanged();
			}
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x0008EB48 File Offset: 0x0008CD48
		protected virtual object SaveViewState()
		{
			object obj = ((IStateManager)this.ViewState).SaveViewState();
			object obj2 = (this.itemStyle != null) ? ((IStateManager)this.itemStyle).SaveViewState() : null;
			object obj3 = (this.headerStyle != null) ? ((IStateManager)this.headerStyle).SaveViewState() : null;
			object obj4 = (this.footerStyle != null) ? ((IStateManager)this.footerStyle).SaveViewState() : null;
			object uniqueName = this._uniqueName;
			if (obj == null && obj2 == null && obj3 == null && obj4 == null && uniqueName == null)
			{
				return null;
			}
			return new object[]
			{
				obj,
				obj2,
				obj3,
				obj4,
				uniqueName
			};
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x0008EBE4 File Offset: 0x0008CDE4
		internal object SaveTableViewSpecificState()
		{
			return ((IStateManager)this.TableViewSpecificState).SaveViewState();
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x0008EBF1 File Offset: 0x0008CDF1
		internal void LoadTableViewSpecificState(object o)
		{
			((IStateManager)this.TableViewSpecificState).LoadViewState(o);
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x0008EBFF File Offset: 0x0008CDFF
		internal void TrackTableViewSpecificState()
		{
			((IStateManager)this.TableViewSpecificState).TrackViewState();
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06002B7F RID: 11135 RVA: 0x0008EC0C File Offset: 0x0008CE0C
		protected StateBag TableViewSpecificState
		{
			get
			{
				if (this._tableViewSpecificState == null)
				{
					this._tableViewSpecificState = new StateBag();
				}
				return this._tableViewSpecificState;
			}
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x0008EC27 File Offset: 0x0008CE27
		internal void SetOwner(GridTableView owner)
		{
			this.owner = owner;
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06002B81 RID: 11137 RVA: 0x0008EC30 File Offset: 0x0008CE30
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x0008EC38 File Offset: 0x0008CE38
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x0008EC41 File Offset: 0x0008CE41
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x0008EC49 File Offset: 0x0008CE49
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x0008EC51 File Offset: 0x0008CE51
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x0008EC58 File Offset: 0x0008CE58
		protected virtual void TrackViewState()
		{
			this.marked = true;
			((IStateManager)this.ViewState).TrackViewState();
			if (this.itemStyle != null)
			{
				((IStateManager)this.itemStyle).TrackViewState();
			}
			if (this.headerStyle != null)
			{
				((IStateManager)this.headerStyle).TrackViewState();
			}
			if (this.footerStyle != null)
			{
				((IStateManager)this.footerStyle).TrackViewState();
			}
			if (this.itemStyle != null)
			{
				((IStateManager)this.itemStyle).TrackViewState();
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06002B87 RID: 11143 RVA: 0x0008ECC3 File Offset: 0x0008CEC3
		protected bool DesignMode
		{
			get
			{
				return this.designMode;
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06002B88 RID: 11144 RVA: 0x0008ECCB File Offset: 0x0008CECB
		[NotifyParentProperty(true)]
		[Description("RadGridColumn_FooterStyle")]
		[Category("Style")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual TableItemStyle FooterStyle
		{
			get
			{
				if (this.footerStyle == null)
				{
					this.footerStyle = new TableItemStyle();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.footerStyle).TrackViewState();
					}
				}
				return this.footerStyle;
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06002B89 RID: 11145 RVA: 0x0008ECF9 File Offset: 0x0008CEF9
		// (set) Token: 0x06002B8A RID: 11146 RVA: 0x0008ED01 File Offset: 0x0008CF01
		internal TableItemStyle FooterStyleInternal
		{
			get
			{
				return this.footerStyle;
			}
			set
			{
				this.footerStyle = value;
			}
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x0008ED0A File Offset: 0x0008CF0A
		public void ResetCurrentFilterValue(GridFilteringItem item)
		{
			this.ViewState["_cfv"] = null;
			this.ViewState["_cff"] = null;
			this.owner.FilterValues.Remove(this.UniqueName);
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x0008ED48 File Offset: 0x0008CF48
		public void ResetCurrentFilterValue()
		{
			this.ViewState["_cfv"] = null;
			this.ViewState["_cff"] = null;
			this.ViewState["_acfv"] = null;
			this.ViewState["_acff"] = null;
			this.owner.FilterValues.Remove(this.UniqueName);
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06002B8D RID: 11149 RVA: 0x0008EDB0 File Offset: 0x0008CFB0
		// (set) Token: 0x06002B8E RID: 11150 RVA: 0x0008EDDD File Offset: 0x0008CFDD
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("RadGridColumn_FooterText")]
		[DefaultValue("")]
		[Category("Appearance")]
		public virtual string FooterText
		{
			get
			{
				object obj = this.ViewState["FooterText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["FooterText"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x0008EDF8 File Offset: 0x0008CFF8
		// (set) Token: 0x06002B90 RID: 11152 RVA: 0x0008EE25 File Offset: 0x0008D025
		[Localizable(true)]
		[Category("Appearance")]
		[Description("RadGridColumn_HeaderImageUrl")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string HeaderImageUrl
		{
			get
			{
				object obj = this.ViewState["HeaderImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["HeaderImageUrl"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06002B91 RID: 11153 RVA: 0x0008EE3E File Offset: 0x0008D03E
		[NotifyParentProperty(true)]
		[Description("RadGridColumn_HeaderStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(typeof(TableItemStyle))]
		public virtual TableItemStyle HeaderStyle
		{
			get
			{
				if (this.headerStyle == null)
				{
					this.headerStyle = new TableItemStyle();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.headerStyle).TrackViewState();
					}
				}
				return this.headerStyle;
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06002B92 RID: 11154 RVA: 0x0008EE6C File Offset: 0x0008D06C
		// (set) Token: 0x06002B93 RID: 11155 RVA: 0x0008EE74 File Offset: 0x0008D074
		internal TableItemStyle HeaderStyleInternal
		{
			get
			{
				return this.headerStyle;
			}
			set
			{
				this.headerStyle = value;
			}
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06002B94 RID: 11156 RVA: 0x0008EE80 File Offset: 0x0008D080
		// (set) Token: 0x06002B95 RID: 11157 RVA: 0x0008EEAD File Offset: 0x0008D0AD
		[Description("RadGridColumn_HeaderText")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Category("Appearance")]
		public virtual string HeaderText
		{
			get
			{
				object obj = this.ViewState["HeaderText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["HeaderText"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06002B96 RID: 11158 RVA: 0x0008EEC6 File Offset: 0x0008D0C6
		protected bool IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06002B97 RID: 11159 RVA: 0x0008EECE File Offset: 0x0008D0CE
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Style")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("RadGridColumn_ItemStyle")]
		public virtual TableItemStyle ItemStyle
		{
			get
			{
				if (this.itemStyle == null)
				{
					this.itemStyle = new TableItemStyle();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.itemStyle).TrackViewState();
					}
				}
				return this.itemStyle;
			}
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x0008EEFC File Offset: 0x0008D0FC
		// (set) Token: 0x06002B99 RID: 11161 RVA: 0x0008EF04 File Offset: 0x0008D104
		internal TableItemStyle ItemStyleInternal
		{
			get
			{
				return this.itemStyle;
			}
			set
			{
				this.itemStyle = value;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06002B9A RID: 11162 RVA: 0x0008EF0D File Offset: 0x0008D10D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public GridTableView Owner
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x0008EF15 File Offset: 0x0008D115
		internal virtual string GetSortExpression()
		{
			return this.SortExpression;
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06002B9C RID: 11164 RVA: 0x0008EF20 File Offset: 0x0008D120
		// (set) Token: 0x06002B9D RID: 11165 RVA: 0x0008EF4D File Offset: 0x0008D14D
		[Category("Behavior")]
		[Description("RadGridColumn_SortExpression")]
		[Localizable(true)]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string SortExpression
		{
			get
			{
				object obj = this.ViewState["_se"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["_se"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06002B9E RID: 11166 RVA: 0x0008EF68 File Offset: 0x0008D168
		// (set) Token: 0x06002B9F RID: 11167 RVA: 0x0008EF95 File Offset: 0x0008D195
		[Localizable(true)]
		[Category("Behavior")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("RadGridColumn_GroupByExpression")]
		public virtual string GroupByExpression
		{
			get
			{
				object obj = this.ViewState["_gbe"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["_gbe"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x0008EFAE File Offset: 0x0008D1AE
		public virtual string GetDefaultGroupByExpression()
		{
			return string.Empty;
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06002BA1 RID: 11169 RVA: 0x0008EFB5 File Offset: 0x0008D1B5
		protected StateBag ViewState
		{
			get
			{
				return this.statebag;
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06002BA2 RID: 11170 RVA: 0x0008EFC0 File Offset: 0x0008D1C0
		// (set) Token: 0x06002BA3 RID: 11171 RVA: 0x0008EFE9 File Offset: 0x0008D1E9
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("RadGridColumn_ShowSortIcon")]
		[DefaultValue(true)]
		public virtual bool ShowSortIcon
		{
			get
			{
				object obj = this.ViewState["ShowSortIcon"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowSortIcon"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06002BA4 RID: 11172 RVA: 0x0008F008 File Offset: 0x0008D208
		// (set) Token: 0x06002BA5 RID: 11173 RVA: 0x0008F031 File Offset: 0x0008D231
		[DefaultValue(true)]
		[Description("RadGridColumn_Visible")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public virtual bool Visible
		{
			get
			{
				object obj = this.ViewState["Visible"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["Visible"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06002BA6 RID: 11174 RVA: 0x0008F050 File Offset: 0x0008D250
		// (set) Token: 0x06002BA7 RID: 11175 RVA: 0x0008F079 File Offset: 0x0008D279
		[NotifyParentProperty(true)]
		[Description("RadGridColumn_Display")]
		[DefaultValue(true)]
		[Category("Behavior")]
		public bool Display
		{
			get
			{
				object obj = this.ViewState["Display"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["Display"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x0008F097 File Offset: 0x0008D297
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string OwnerID
		{
			get
			{
				return this.Owner.ClientID;
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06002BA9 RID: 11177 RVA: 0x0008F0A4 File Offset: 0x0008D2A4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string OwnerGridID
		{
			get
			{
				return this.Owner.OwnerGrid.ClientID;
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06002BAA RID: 11178 RVA: 0x0008F0B8 File Offset: 0x0008D2B8
		// (set) Token: 0x06002BAB RID: 11179 RVA: 0x0008F0E1 File Offset: 0x0008D2E1
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("RadGridColumn_Resizable")]
		public virtual bool Resizable
		{
			get
			{
				object obj = this.ViewState["Resizable"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["Resizable"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06002BAC RID: 11180 RVA: 0x0008F100 File Offset: 0x0008D300
		// (set) Token: 0x06002BAD RID: 11181 RVA: 0x0008F129 File Offset: 0x0008D329
		[NotifyParentProperty(true)]
		[Description("RadGridColumn_Reordarable")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public virtual bool Reorderable
		{
			get
			{
				object obj = this.ViewState["Reorderable"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["Reorderable"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06002BAE RID: 11182 RVA: 0x0008F148 File Offset: 0x0008D348
		// (set) Token: 0x06002BAF RID: 11183 RVA: 0x0008F171 File Offset: 0x0008D371
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("RadGridColumn_Groupable")]
		[DefaultValue(true)]
		public virtual bool Groupable
		{
			get
			{
				object obj = this.ViewState["Groupable"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["Groupable"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06002BB0 RID: 11184 RVA: 0x0008F18F File Offset: 0x0008D38F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string ColumnType
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06002BB1 RID: 11185 RVA: 0x0008F19C File Offset: 0x0008D39C
		// (set) Token: 0x06002BB2 RID: 11186 RVA: 0x0008F1C5 File Offset: 0x0008D3C5
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(GridHeaderButtonType), "LinkButton")]
		public virtual GridHeaderButtonType HeaderButtonType
		{
			get
			{
				object obj = this.ViewState["HeaderButtonType"];
				if (obj != null)
				{
					return (GridHeaderButtonType)obj;
				}
				return GridHeaderButtonType.LinkButton;
			}
			set
			{
				this.ViewState["HeaderButtonType"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06002BB3 RID: 11187 RVA: 0x0008F1E4 File Offset: 0x0008D3E4
		// (set) Token: 0x06002BB4 RID: 11188 RVA: 0x0008F24C File Offset: 0x0008D44C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[DefaultValue(-1)]
		public int OrderIndex
		{
			get
			{
				if (this.IsClone && this.owner != null && this.owner.OrderIndices.ContainsKey(this.UniqueName))
				{
					return this.owner.OrderIndices[this.UniqueName];
				}
				object obj = this.ViewState["oind"];
				if (obj != null)
				{
					return (int)obj;
				}
				return -1;
			}
			set
			{
				if (this.IsClone && this.owner != null)
				{
					if (!this.owner.OrderIndices.ContainsKey(this.UniqueName))
					{
						this.owner.OrderIndices.Add(this.UniqueName, value);
					}
					else
					{
						this.owner.OrderIndices[this.UniqueName] = value;
					}
				}
				else if (value != -1)
				{
					this.ViewState["oind"] = value;
				}
				else
				{
					this.ViewState["oind"] = null;
				}
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06002BB5 RID: 11189 RVA: 0x0008F2E6 File Offset: 0x0008D4E6
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool IsEditable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x0008F2E9 File Offset: 0x0008D4E9
		private void GetUniqueName()
		{
			this._uniqueName = this.GenerateUniqueName();
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x0008F2F8 File Offset: 0x0008D4F8
		protected bool IsUniqueName(string testName)
		{
			if (this.Owner != null)
			{
				foreach (object obj in this.Owner.Columns)
				{
					GridColumn gridColumn = (GridColumn)obj;
					if (this != gridColumn && gridColumn._uniqueName == testName)
					{
						return false;
					}
				}
				foreach (GridColumn gridColumn2 in this.Owner.AutoGeneratedColumns)
				{
					if (this != gridColumn2 && gridColumn2._uniqueName == testName)
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x0008F3B4 File Offset: 0x0008D5B4
		protected virtual bool IsDefaultUniqueName
		{
			get
			{
				return this._isDefaultUniqueName;
			}
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x0008F3BC File Offset: 0x0008D5BC
		protected string GenerateUniqueNameBase(string Base)
		{
			string text = (!string.IsNullOrEmpty(Base)) ? Base : "column";
			string text2 = text;
			if (this.Owner != null)
			{
				for (int i = 0; i < 500; i++)
				{
					text2 = text + ((i != 0) ? i.ToString() : string.Empty);
					if (this.IsUniqueName(text2))
					{
						break;
					}
				}
			}
			else
			{
				this._isDefaultUniqueName = true;
			}
			return text2;
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x0008F41F File Offset: 0x0008D61F
		protected virtual string GenerateUniqueName()
		{
			return this.GenerateUniqueNameBase("column");
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x0008F42C File Offset: 0x0008D62C
		protected virtual void TrySetOnClientClickScript(Control control, GridItem item, string functionName, params string[] functionParameters)
		{
			string clientScript = null;
			if (item.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
			{
				clientScript = this.GenerateClientScript(item.OwnerTableView.ClientID, functionName, functionParameters);
			}
			IButtonControl buttonControl = control as IButtonControl;
			if (item.OwnerTableView.EditMode == GridEditMode.Batch && buttonControl != null)
			{
				string text = null;
				string[] functionParameters2 = null;
				GridSelecting selecting = item.OwnerTableView.OwnerGrid.ClientSettings.Selecting;
				string commandName;
				switch (commandName = buttonControl.CommandName)
				{
				case "InitInsert":
					text = "addNewRecord";
					functionParameters2 = new string[]
					{
						item.OwnerTableView.ClientID
					};
					break;
				case "Edit":
					text = "openRowForEdit";
					functionParameters2 = new string[]
					{
						item.ClientID
					};
					break;
				case "Delete":
					text = "_deleteRecord";
					functionParameters2 = new string[]
					{
						item.OwnerTableView.ClientID,
						item.ClientID
					};
					break;
				case "Update":
				case "PerformInsert":
				case "UpdateEdited":
					if (item.OwnerTableView.BatchEditingSettings.SaveAllHierarchyLevels)
					{
						text = "saveAllChanges";
						functionParameters2 = new string[0];
					}
					else
					{
						text = "saveChanges";
						functionParameters2 = new string[]
						{
							item.OwnerTableView.ClientID
						};
					}
					break;
				case "CancelAll":
				case "Cancel":
					text = "cancelChanges";
					functionParameters2 = new string[]
					{
						item.OwnerTableView.ClientID
					};
					break;
				}
				if (!string.IsNullOrEmpty(text))
				{
					clientScript = GridBatchEditingHelper.GenerateClientScript(item.OwnerTableView, text, functionParameters2);
				}
			}
			this.SetClientScript(control, clientScript);
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x0008F654 File Offset: 0x0008D854
		private void SetClientScript(Control control, string clientScript)
		{
			if (!string.IsNullOrEmpty(clientScript))
			{
				Button button = control as Button;
				ImageButton imageButton = control as ImageButton;
				LinkButton linkButton = control as LinkButton;
				if (button != null)
				{
					button.OnClientClick = clientScript;
					return;
				}
				if (imageButton != null)
				{
					imageButton.OnClientClick = clientScript;
					return;
				}
				if (linkButton != null)
				{
					linkButton.OnClientClick = clientScript;
				}
			}
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x0008F6A0 File Offset: 0x0008D8A0
		private string GenerateClientScript(string controlClientId, string functionName, params string[] functionParameters)
		{
			StringBuilder stringBuilder = new StringBuilder(string.Format("if(!$find('{0}').{1}(", controlClientId, functionName));
			for (int i = 0; i < functionParameters.Length; i++)
			{
				stringBuilder.Append(string.Format("'{0}',", functionParameters[i]));
			}
			if (functionParameters.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			stringBuilder.Append(")) return false;");
			return stringBuilder.ToString();
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x0008F70C File Offset: 0x0008D90C
		// (set) Token: 0x06002BBF RID: 11199 RVA: 0x0008F73A File Offset: 0x0008D93A
		[NotifyParentProperty(true)]
		[DefaultValue(0)]
		public int EditFormColumnIndex
		{
			get
			{
				object obj = this.ViewState["_efci"];
				if (obj == null)
				{
					obj = 0;
				}
				return (int)obj;
			}
			set
			{
				this.ViewState["_efci"] = value;
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x0008F752 File Offset: 0x0008D952
		// (set) Token: 0x06002BC1 RID: 11201 RVA: 0x0008F76D File Offset: 0x0008D96D
		[DefaultValue("")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		public virtual string UniqueName
		{
			get
			{
				if (string.IsNullOrEmpty(this._uniqueName))
				{
					this.GetUniqueName();
				}
				return this._uniqueName;
			}
			set
			{
				if (Regex.IsMatch(value, "\\s"))
				{
					throw new ArgumentException("UniqueName cannot contain spaces");
				}
				this._uniqueName = value;
				this._isDefaultUniqueName = false;
			}
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x0008F795 File Offset: 0x0008D995
		protected void UpdateUniqueNameIfDefault(string value)
		{
			if (this.IsDefaultUniqueName)
			{
				this._uniqueName = this.GenerateUniqueNameBase(value);
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06002BC3 RID: 11203 RVA: 0x0008F7AC File Offset: 0x0008D9AC
		// (set) Token: 0x06002BC4 RID: 11204 RVA: 0x0008F7D9 File Offset: 0x0008D9D9
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("{0}:")]
		public string EditFormHeaderTextFormat
		{
			get
			{
				object obj = this.ViewState["_efht"];
				if (obj == null)
				{
					obj = "{0}:";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["_efht"] = value;
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x0008F7EC File Offset: 0x0008D9EC
		// (set) Token: 0x06002BC6 RID: 11206 RVA: 0x0008F81E File Offset: 0x0008DA1E
		[TypeConverter(typeof(GridDataTypeConverter))]
		[DefaultValue(typeof(string))]
		[NotifyParentProperty(true)]
		public Type DataType
		{
			get
			{
				object obj = this.ViewState["DataType"];
				if (obj == null)
				{
					obj = typeof(string);
				}
				return (Type)obj;
			}
			set
			{
				if (!GridDataTypeConverter.SupportedTypes.Contains(value) && !value.IsEnum)
				{
					throw new GridNotSupportedException("Specified column DataType is not supported " + value.ToString());
				}
				this.ViewState["DataType"] = value;
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x0008F85C File Offset: 0x0008DA5C
		internal bool DataTypeIsSet
		{
			get
			{
				return this.ViewState["DataType"] != null;
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x0008F874 File Offset: 0x0008DA74
		// (set) Token: 0x06002BC9 RID: 11209 RVA: 0x0008F87C File Offset: 0x0008DA7C
		internal GridColumn OriginalColumn
		{
			get
			{
				return this._originalColumn;
			}
			set
			{
				this._originalColumn = value;
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06002BCA RID: 11210 RVA: 0x0008F885 File Offset: 0x0008DA85
		internal bool IsClone
		{
			get
			{
				return this._originalColumn != null;
			}
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x0008F894 File Offset: 0x0008DA94
		protected virtual void CopyBaseProperties(GridColumn FromColumn)
		{
			this.OriginalColumn = FromColumn;
			this.SortExpression = FromColumn.SortExpression;
			this.ItemStyle.CopyFrom(FromColumn.ItemStyle);
			this.FooterStyle.CopyFrom(FromColumn.FooterStyle);
			this.FooterText = FromColumn.FooterText;
			this.HeaderText = FromColumn.HeaderText;
			this.HeaderStyle.CopyFrom(FromColumn.HeaderStyle);
			this.HeaderImageUrl = FromColumn.HeaderImageUrl;
			this.ShowSortIcon = FromColumn.ShowSortIcon;
			this.Visible = FromColumn.Visible;
			this.Display = FromColumn.Display;
			this.Resizable = FromColumn.Resizable;
			this.EnableHeaderContextMenu = FromColumn.EnableHeaderContextMenu;
			this.HeaderButtonType = FromColumn.HeaderButtonType;
			this.Reorderable = FromColumn.Reorderable;
			this.Groupable = FromColumn.Groupable;
			this.GroupByExpression = FromColumn.GroupByExpression;
			this.OrderIndex = FromColumn.OrderIndex;
			this.UniqueName = FromColumn.UniqueName;
			this.EditFormColumnIndex = FromColumn.EditFormColumnIndex;
			this.EditFormHeaderTextFormat = FromColumn.EditFormHeaderTextFormat;
			this.CurrentFilterValue = FromColumn.CurrentFilterValue;
			this.CurrentFilterFunction = FromColumn.CurrentFilterFunction;
			this.AutoPostBackOnFilter = FromColumn.AutoPostBackOnFilter;
			if (FromColumn.ViewState["_Acff"] != null)
			{
				this.AutoPostBackFilterFunction = FromColumn.AutoPostBackFilterFunction;
			}
			this.SortAscImageUrl = FromColumn.SortAscImageUrl;
			this.SortDescImageUrl = FromColumn.SortDescImageUrl;
			this.FilterImageUrl = FromColumn.FilterImageUrl;
			this.FilterImageToolTip = FromColumn.FilterImageToolTip;
			this.SortedBackColor = FromColumn.SortedBackColor;
			this.FilterControlWidth = FromColumn.FilterControlWidth;
			this.FilterControlToolTip = FromColumn.FilterControlToolTip;
			this.FilterTemplate = FromColumn.FilterTemplate;
			this.FilterDelay = FromColumn.FilterDelay;
			this.ShowFilterIcon = FromColumn.ShowFilterIcon;
			this.Exportable = FromColumn.Exportable;
			this.HeaderTooltip = FromColumn.HeaderTooltip;
			this.HeaderAbbr = FromColumn.HeaderAbbr;
			this.HeaderAxis = FromColumn.HeaderAxis;
			this.ColumnGroupName = FromColumn.ColumnGroupName;
			this.FilterCheckListWebServiceMethod = FromColumn.FilterCheckListWebServiceMethod;
			this.FilterCheckListEnableLoadOnDemand = FromColumn.FilterCheckListEnableLoadOnDemand;
			this.FilterControlAltText = FromColumn.FilterControlAltText;
			if (FromColumn.DataTypeIsSet)
			{
				this.DataType = FromColumn.DataType;
			}
		}

		// Token: 0x06002BCC RID: 11212
		public abstract GridColumn Clone();

		// Token: 0x06002BCD RID: 11213 RVA: 0x0008FAD5 File Offset: 0x0008DCD5
		public virtual bool IsBoundToFieldName(string name)
		{
			return false;
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x0008FAD8 File Offset: 0x0008DCD8
		public int CompareTo(object obj)
		{
			GridColumn gridColumn = obj as GridColumn;
			if (gridColumn != null)
			{
				int num = this.OrderIndex.CompareTo(gridColumn.OrderIndex);
				if (num == 0 && this.OrderIndex == -1)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>();
					dictionary.Add("GridGroupSplitterColumn", -3);
					dictionary.Add("GridExpandColumn", -2);
					dictionary.Add("GridRowIndicatorColumn", -1);
					num = dictionary[base.GetType().Name].CompareTo(dictionary[gridColumn.GetType().Name]);
					if (num == 0 && base.GetType().Name == "GridGroupSplitterColumn" && gridColumn.GetType().Name == "GridGroupSplitterColumn")
					{
						num = this.UniqueName.CompareTo(gridColumn.UniqueName);
					}
				}
				return num;
			}
			GridColumnGroup gridColumnGroup = obj as GridColumnGroup;
			if (gridColumnGroup != null)
			{
				return this.OrderIndex.CompareTo(gridColumnGroup.OrderIndex);
			}
			return 1;
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x0008FBD5 File Offset: 0x0008DDD5
		internal static GridItem GetBindingParentItem(Control control)
		{
			if (control.Parent != null && control.NamingContainer is GridItem)
			{
				return (GridItem)control.NamingContainer;
			}
			return GridColumn.GetBindingParentItem(control.NamingContainer);
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x0008FC03 File Offset: 0x0008DE03
		public virtual IDictionary GetCustomPropertyDataFields(object dataItemInstance)
		{
			return new Hashtable();
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x0008FC0C File Offset: 0x0008DE0C
		public static void AddSubPropertyFieldInfo(IDictionary fieldsInfo, string dataField, object dataItemInstance)
		{
			if (dataField.IndexOf('.') > 0)
			{
				PropertyDescriptor descriptor = GridPropertyEvaluator.GetDescriptor(dataItemInstance, dataField);
				if (descriptor == null)
				{
					fieldsInfo.Add(dataField, dataField);
					return;
				}
				fieldsInfo.Add(dataField, descriptor);
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06002BD2 RID: 11218 RVA: 0x0008FC40 File Offset: 0x0008DE40
		// (set) Token: 0x06002BD3 RID: 11219 RVA: 0x0008FC6D File Offset: 0x0008DE6D
		[DefaultValue(typeof(Unit), "")]
		public virtual Unit FilterControlWidth
		{
			get
			{
				object obj = this.ViewState["_fcw"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Empty;
			}
			set
			{
				this.ViewState["_fcw"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06002BD4 RID: 11220 RVA: 0x0008FC8C File Offset: 0x0008DE8C
		// (set) Token: 0x06002BD5 RID: 11221 RVA: 0x0008FCC9 File Offset: 0x0008DEC9
		[DefaultValue("Filter EditCommandColumn column")]
		public virtual string FilterControlAltText
		{
			get
			{
				object obj = this.ViewState["FilterControlAltText"];
				if (obj == null)
				{
					return "Filter " + this.UniqueName + " column";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["FilterControlAltText"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06002BD6 RID: 11222 RVA: 0x0008FCE4 File Offset: 0x0008DEE4
		// (set) Token: 0x06002BD7 RID: 11223 RVA: 0x0008FD11 File Offset: 0x0008DF11
		[DefaultValue("")]
		public virtual string FilterCheckListWebServiceMethod
		{
			get
			{
				object obj = this.ViewState["_fCLWSM"];
				if (obj == null)
				{
					return "";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["_fCLWSM"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06002BD8 RID: 11224 RVA: 0x0008FD2C File Offset: 0x0008DF2C
		// (set) Token: 0x06002BD9 RID: 11225 RVA: 0x0008FD55 File Offset: 0x0008DF55
		[DefaultValue(false)]
		public virtual bool FilterCheckListEnableLoadOnDemand
		{
			get
			{
				object obj = this.ViewState["_fCLELOD"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["_fCLELOD"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x04000B52 RID: 2898
		private bool designMode;

		// Token: 0x04000B53 RID: 2899
		private TableItemStyle footerStyle;

		// Token: 0x04000B54 RID: 2900
		private TableItemStyle headerStyle;

		// Token: 0x04000B55 RID: 2901
		private TableItemStyle itemStyle;

		// Token: 0x04000B56 RID: 2902
		private bool marked;

		// Token: 0x04000B57 RID: 2903
		private GridTableView owner;

		// Token: 0x04000B58 RID: 2904
		private StateBag statebag;

		// Token: 0x04000B59 RID: 2905
		internal Control sortIcon;

		// Token: 0x04000B5A RID: 2906
		private string _uniqueName;

		// Token: 0x04000B5B RID: 2907
		private bool _isDefaultUniqueName;

		// Token: 0x04000B5C RID: 2908
		private ITemplate filterTemplate;

		// Token: 0x04000B5D RID: 2909
		internal bool TriggeredFilterCommand;

		// Token: 0x04000B5E RID: 2910
		private StateBag _tableViewSpecificState;

		// Token: 0x04000B5F RID: 2911
		private GridColumn _originalColumn;
	}
}
