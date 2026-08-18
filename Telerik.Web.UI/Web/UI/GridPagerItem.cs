using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x0200114F RID: 4431
	public class GridPagerItem : GridItem
	{
		// Token: 0x17003A4A RID: 14922
		// (get) Token: 0x0600B465 RID: 46181 RVA: 0x00277BA4 File Offset: 0x00275DA4
		protected GridPagerStyle RenderPagerStyle
		{
			get
			{
				return base.OwnerTableView.RenderPagerStyle;
			}
		}

		// Token: 0x17003A4B RID: 14923
		// (get) Token: 0x0600B466 RID: 46182 RVA: 0x00277BB1 File Offset: 0x00275DB1
		internal GridPagerButtonBuilder Builder
		{
			get
			{
				if (this._builder == null)
				{
					this._builder = new GridPagerButtonBuilder(this);
				}
				return this._builder;
			}
		}

		// Token: 0x17003A4C RID: 14924
		// (get) Token: 0x0600B467 RID: 46183 RVA: 0x00277BCD File Offset: 0x00275DCD
		// (set) Token: 0x0600B468 RID: 46184 RVA: 0x00277BFC File Offset: 0x00275DFC
		[NotifyParentProperty(true)]
		[Description("Gets or sets the caption for the table pager.")]
		[DefaultValue("Data pager")]
		public virtual string Caption
		{
			get
			{
				if (this.ViewState["Caption"] == null)
				{
					return "Data pager";
				}
				return (string)this.ViewState["Caption"];
			}
			set
			{
				this.ViewState["Caption"] = value;
			}
		}

		// Token: 0x17003A4D RID: 14925
		// (get) Token: 0x0600B469 RID: 46185 RVA: 0x00277C0F File Offset: 0x00275E0F
		// (set) Token: 0x0600B46A RID: 46186 RVA: 0x00277C3E File Offset: 0x00275E3E
		[Description("Data pager which controls on which page is the RadGrid control.")]
		[DefaultValue("Data pager")]
		[NotifyParentProperty(true)]
		public virtual string Summary
		{
			get
			{
				if (this.ViewState["Summary"] == null)
				{
					return "Data pager which controls on which page is the RadGrid control.";
				}
				return (string)this.ViewState["Summary"];
			}
			set
			{
				this.ViewState["Summary"] = value;
			}
		}

		// Token: 0x0600B46B RID: 46187 RVA: 0x00277C51 File Offset: 0x00275E51
		public GridPagerItem(GridTableView ownerTableView, int itemIndex, int dataSetIndex, bool isTopPager) : base(ownerTableView, itemIndex, dataSetIndex, GridItemType.Pager)
		{
			this._isTopPager = isTopPager;
		}

		// Token: 0x17003A4E RID: 14926
		// (get) Token: 0x0600B46C RID: 46188 RVA: 0x00277C70 File Offset: 0x00275E70
		public bool IsTopPager
		{
			get
			{
				return this._isTopPager;
			}
		}

		// Token: 0x17003A4F RID: 14927
		// (get) Token: 0x0600B46D RID: 46189 RVA: 0x00277C78 File Offset: 0x00275E78
		public GridPagingManager Paging
		{
			get
			{
				return base.OwnerTableView.ResolvedDataSource.Paging;
			}
		}

		// Token: 0x0600B46E RID: 46190 RVA: 0x00277C8A File Offset: 0x00275E8A
		private string GetLocalizationString(TFunc<GridStrings, string> extractor, string defaultValue)
		{
			if (base.OwnerTableView != null && base.OwnerTableView.OwnerGrid != null)
			{
				return extractor(base.OwnerTableView.OwnerGrid.Localization);
			}
			return defaultValue;
		}

		// Token: 0x0600B46F RID: 46191 RVA: 0x00277CBC File Offset: 0x00275EBC
		public override void SetupItem(bool dataBind, object dataItem, GridColumn[] columns, ControlCollection rows)
		{
			rows.Add(this);
			GridItemEventArgs e = new GridItemEventArgs(this, new GridItemCreated());
			this._pagerCell = new TableCell();
			this.Cells.Add(this._pagerCell);
			this._columns = columns;
			if (base.OwnerTableView.PagerTemplate == null)
			{
				this.InitializePagerItem(columns);
			}
			else
			{
				base.OwnerTableView.PagerTemplate.InstantiateIn(this._pagerCell);
			}
			base.OwnerTableView.OwnerGrid.CallOnItemCreated(e);
			if (dataBind)
			{
				this.DataBind();
				e = new GridItemEventArgs(this, new GridItemDataBound());
				base.OwnerTableView.OwnerGrid.CallOnItemDataBound(e);
			}
		}

		// Token: 0x17003A50 RID: 14928
		// (get) Token: 0x0600B470 RID: 46192 RVA: 0x00277D64 File Offset: 0x00275F64
		public TableCell PagerContentCell
		{
			get
			{
				return this._pagerCell;
			}
		}

		// Token: 0x0600B471 RID: 46193 RVA: 0x00277D6C File Offset: 0x00275F6C
		public override void PrepareItemStyle()
		{
			if (this.PagerContentCell == null)
			{
				return;
			}
			this.PagerContentCell.ColumnSpan = base.CalcColSpan(base.OwnerTableView.RenderColumns, 0, -1);
			base.PrepareItemStyle();
		}

		// Token: 0x0600B472 RID: 46194 RVA: 0x00277E0C File Offset: 0x0027600C
		public void InitializePagerItem(GridColumn[] columns)
		{
			this.PagerContentCell.ColumnSpan = base.CalcColSpan(columns, 0, -1);
			GridPagerStyle style1 = base.OwnerTableView.RenderPagerStyle;
			GridPagingManager paging = base.OwnerTableView.ResolvedDataSource.Paging;
			GridItemEventArgs gridItemEventArgs = new GridItemEventArgs(this, new GridInitializePagerItem(paging));
			base.OwnerTableView.OwnerGrid.CallOnItemEvent(gridItemEventArgs);
			if (gridItemEventArgs.Canceled)
			{
				return;
			}
			bool flag = base.OwnerTableView.PagerStyle.EnableSEOPaging || base.OwnerTableView.OwnerGrid.PagerStyle.EnableSEOPaging;
			string text = string.Empty;
			if (style1.ShowPagerText)
			{
				try
				{
					if (!style1.IsPagerTextFormatChanged)
					{
						if (!base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
						{
							if (style1.Mode == GridPagerMode.Advanced || style1.Mode == GridPagerMode.NextPrevNumericAndAdvanced || style1.Mode == GridPagerMode.Slider)
							{
								text = string.Format("Page: {3} &nbsp;Item <strong>{0}</strong> to <strong>{1}</strong> of <strong>{2}</strong>", new object[]
								{
									(paging.DataSourceCount == 0) ? 0 : (paging.FirstIndexInPage + 1),
									paging.LastIndexInPage + 1,
									paging.DataSourceCount,
									"!"
								});
							}
							else if (style1.Mode == GridPagerMode.NumericPages || style1.Mode == GridPagerMode.NextPrev)
							{
								text = string.Format(style1.PagerTextFormat, new object[]
								{
									paging.CurrentPageIndex + 1,
									this.GetNormalizedPageCount(paging),
									(paging.DataSourceCount == 0) ? 0 : (paging.FirstIndexInPage + 1),
									paging.LastIndexInPage + 1,
									"!",
									paging.DataSourceCount
								});
							}
							else if (style1.Mode == GridPagerMode.NextPrevAndNumeric)
							{
								text = string.Format("Page: {2} &nbsp;<strong>{0}</strong> items in <strong>{1}</strong> pages", paging.DataSourceCount, this.GetNormalizedPageCount(paging), "!");
							}
						}
						else if (style1.Mode == GridPagerMode.Advanced || style1.Mode == GridPagerMode.NextPrevNumericAndAdvanced || style1.Mode == GridPagerMode.Slider)
						{
							if (this.IsTopPager)
							{
								text = string.Format("Page: {3} &nbsp;Item <strong>{0}</strong> to <strong>{1}</strong> of <strong>{2}</strong>", new object[]
								{
									string.Format("<span id='{1}'>{0}</span>", (paging.DataSourceCount == 0) ? 0 : (paging.FirstIndexInPage + 1), base.OwnerTableView.ClientID + "FIPTop"),
									string.Format("<span id='{1}'>{0}</span>", paging.LastIndexInPage + 1, base.OwnerTableView.ClientID + "LIPTop"),
									string.Format("<span id='{1}'>{0}</span>", paging.DataSourceCount, base.OwnerTableView.ClientID + "DSCTop"),
									"!"
								});
							}
							else
							{
								text = string.Format("Page: {3} &nbsp;Item <strong>{0}</strong> to <strong>{1}</strong> of <strong>{2}</strong>", new object[]
								{
									string.Format("<span id='{1}'>{0}</span>", (paging.DataSourceCount == 0) ? 0 : (paging.FirstIndexInPage + 1), base.OwnerTableView.ClientID + "FIP"),
									string.Format("<span id='{1}'>{0}</span>", paging.LastIndexInPage + 1, base.OwnerTableView.ClientID + "LIP"),
									string.Format("<span id='{1}'>{0}</span>", paging.DataSourceCount, base.OwnerTableView.ClientID + "DSC"),
									"!"
								});
							}
						}
						else if (style1.Mode == GridPagerMode.NumericPages || style1.Mode == GridPagerMode.NextPrev)
						{
							if (this.IsTopPager)
							{
								text = string.Format(style1.PagerTextFormat, new object[]
								{
									string.Format("<span id='{1}'>{0}</span>", paging.CurrentPageIndex + 1, base.OwnerTableView.ClientID + "CPITop"),
									string.Format("<span id='{1}'>{0}</span>", paging.PageCount, base.OwnerTableView.ClientID + "PCNTop"),
									string.Format("<span id='{1}'>{0}</span>", (paging.DataSourceCount == 0) ? 0 : (paging.FirstIndexInPage + 1), base.OwnerTableView.ClientID + "FIPTop"),
									string.Format("<span id='{1}'>{0}</span>", paging.LastIndexInPage + 1, base.OwnerTableView.ClientID + "LIPTop"),
									"!",
									string.Format("<span id='{1}'>{0}</span>", paging.DataSourceCount, base.OwnerTableView.ClientID + "DSCTop")
								});
							}
							else
							{
								text = string.Format(style1.PagerTextFormat, new object[]
								{
									string.Format("<span id='{1}'>{0}</span>", paging.CurrentPageIndex + 1, base.OwnerTableView.ClientID + "CPI"),
									string.Format("<span id='{1}'>{0}</span>", paging.PageCount, base.OwnerTableView.ClientID + "PCN"),
									string.Format("<span id='{1}'>{0}</span>", (paging.DataSourceCount == 0) ? 0 : (paging.FirstIndexInPage + 1), base.OwnerTableView.ClientID + "FIP"),
									string.Format("<span id='{1}'>{0}</span>", paging.LastIndexInPage + 1, base.OwnerTableView.ClientID + "LIP"),
									"!",
									string.Format("<span id='{1}'>{0}</span>", paging.DataSourceCount, base.OwnerTableView.ClientID + "DSC")
								});
							}
						}
						else if (style1.Mode == GridPagerMode.NextPrevAndNumeric)
						{
							if (this.IsTopPager)
							{
								text = string.Format("Page: {2} &nbsp;<strong>{0}</strong> items in <strong>{1}</strong> pages", string.Format("<span id='{1}'>{0}</span>", paging.DataSourceCount, base.OwnerTableView.ClientID + "DSCTop"), string.Format("<span id='{1}'>{0}</span>", paging.PageCount, base.OwnerTableView.ClientID + "PCNTop"), "!");
							}
							else
							{
								text = string.Format("Page: {2} &nbsp;<strong>{0}</strong> items in <strong>{1}</strong> pages", string.Format("<span id='{1}'>{0}</span>", paging.DataSourceCount, base.OwnerTableView.ClientID + "DSC"), string.Format("<span id='{1}'>{0}</span>", paging.PageCount, base.OwnerTableView.ClientID + "PCN"), "!");
							}
						}
					}
					else if (!base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
					{
						text = string.Format(style1.PagerTextFormat, new object[]
						{
							paging.CurrentPageIndex + 1,
							paging.PageCount,
							(paging.DataSourceCount == 0) ? 0 : (paging.FirstIndexInPage + 1),
							paging.LastIndexInPage + 1,
							"!",
							paging.DataSourceCount
						});
					}
					else if (this.IsTopPager)
					{
						text = string.Format(style1.PagerTextFormat, new object[]
						{
							string.Format("<span id='{1}'>{0}</span>", paging.CurrentPageIndex + 1, base.OwnerTableView.ClientID + "CPITop"),
							string.Format("<span id='{1}'>{0}</span>", paging.PageCount, base.OwnerTableView.ClientID + "PCNTop"),
							string.Format("<span id='{1}'>{0}</span>", (paging.DataSourceCount == 0) ? 0 : (paging.FirstIndexInPage + 1), base.OwnerTableView.ClientID + "FIPTop"),
							string.Format("<span id='{1}'>{0}</span>", paging.LastIndexInPage + 1, base.OwnerTableView.ClientID + "LIPTop"),
							"!",
							string.Format("<span id='{1}'>{0}</span>", paging.DataSourceCount, base.OwnerTableView.ClientID + "DSCTop")
						});
					}
					else
					{
						text = string.Format(style1.PagerTextFormat, new object[]
						{
							string.Format("<span id='{1}'>{0}</span>", paging.CurrentPageIndex + 1, base.OwnerTableView.ClientID + "CPI"),
							string.Format("<span id='{1}'>{0}</span>", paging.PageCount, base.OwnerTableView.ClientID + "PCN"),
							string.Format("<span id='{1}'>{0}</span>", (paging.DataSourceCount == 0) ? 0 : (paging.FirstIndexInPage + 1), base.OwnerTableView.ClientID + "FIP"),
							string.Format("<span id='{1}'>{0}</span>", paging.LastIndexInPage + 1, base.OwnerTableView.ClientID + "LIP"),
							"!",
							string.Format("<span id='{1}'>{0}</span>", paging.DataSourceCount, base.OwnerTableView.ClientID + "DSC")
						});
					}
				}
				catch (FormatException ex)
				{
					throw new GridException("Pager text format is not valid: " + ex.Message);
				}
			}
			WebControl webControl;
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode != RenderMode.Lightweight && base.OwnerTableView.OwnerGrid.ResolvedRenderMode != RenderMode.Mobile)
			{
				Table table = new Table();
				table.Style["width"] = "100%";
				this.SetCellSpacing(table);
				AccessibilityHelper.AddSummary(table, this.Summary);
				if (!string.IsNullOrEmpty(this.Caption))
				{
					table.Caption = string.Format("<span style='display: none'>{0}</span>", this.Caption);
				}
				TableHeaderRow tableHeaderRow = new TableHeaderRow();
				tableHeaderRow.TableSection = TableRowSection.TableHeader;
				table.Rows.Add(tableHeaderRow);
				TableHeaderCell tableHeaderCell = new TableHeaderCell();
				tableHeaderCell.Attributes["scope"] = "col";
				tableHeaderCell.Text = this.Caption;
				tableHeaderRow.Cells.Add(tableHeaderCell);
				TableRow tableRow = new TableRow();
				table.Rows.Add(tableRow);
				if (base.OwnerTableView.OwnerGrid.ShowStatusBar)
				{
					TableCell tableCell = new TableCell();
					tableCell.CssClass = "rgStatus";
					tableRow.Cells.Add(tableCell);
					Panel panel = new Panel();
					panel.ID = "statusPanel";
					panel.Style["visibility"] = "hidden";
					panel.Controls.Add(new LiteralControl("&nbsp;"));
					tableCell.Controls.Add(panel);
				}
				webControl = new TableCell();
				if (!base.OwnerTableView.OwnerGrid.EmptySkin())
				{
					webControl.CssClass = string.Format("rgPagerCell {0}{1}", style1.Mode, flag ? " rgSEO" : "");
				}
				tableRow.Cells.Add((TableCell)webControl);
				if (!this._generateSpanContainer)
				{
					this.PagerContentCell.Controls.Add(table);
				}
			}
			else
			{
				webControl = new Panel();
				if (!base.OwnerTableView.OwnerGrid.EmptySkin())
				{
					this.PagerContentCell.CssClass = "rgPagerCell";
					webControl.CssClass = string.Format("{0}{1}", style1.Mode, flag ? " rgSEO" : "");
				}
				if (base.OwnerTableView.OwnerGrid.ShowStatusBar)
				{
					Panel panel2 = new Panel();
					panel2.CssClass = "rgStatus";
					webControl.Controls.Add(panel2);
					Panel panel3 = new Panel();
					panel3.ID = "statusPanel";
					panel3.Style["visibility"] = "hidden";
					panel3.Controls.Add(new LiteralControl("&nbsp;"));
					panel2.Controls.Add(panel3);
				}
				if (!this._generateSpanContainer)
				{
					this.PagerContentCell.Controls.Add(webControl);
				}
			}
			this.spanContainer = new Panel();
			if (!base.OwnerTableView.OwnerGrid.EmptySkin())
			{
				if (style1.Mode == GridPagerMode.NumericPages)
				{
					this.spanContainer.CssClass = string.Format("rgWrap rgNumPart", new object[0]);
				}
				else if (style1.Mode == GridPagerMode.Slider)
				{
					this.spanContainer.CssClass = string.Format("rgWrap", new object[0]);
				}
				else
				{
					this.spanContainer.CssClass = string.Format("rgWrap rgArrPart1", new object[0]);
				}
			}
			if (!this._generateSpanContainer && style1.Mode != GridPagerMode.Advanced)
			{
				webControl.Controls.Add(this.spanContainer);
			}
			string[] array = text.Split(new char[]
			{
				'!'
			});
			if (array.Length > 1 && style1.Mode == GridPagerMode.NextPrev)
			{
				this.spanContainer.Controls.Add(new LiteralControl(array[0]));
			}
			if (style1.Mode == GridPagerMode.NextPrev || style1.Mode == GridPagerMode.NextPrevAndNumeric || style1.Mode == GridPagerMode.NextPrevNumericAndAdvanced)
			{
				if (!paging.IsFirstPage)
				{
					if (!flag || base.OwnerTableView.OwnerGrid.IsDesignMode)
					{
						this.CreatePagerNavButton(this.spanContainer, "Page", "First", style1.FirstPageText, style1.FirstPageImageUrl, style1.FirstPageToolTip, "rgPageFirst", false);
						this.spanContainer.Controls.Add(new LiteralControl(string.Empty));
						this.CreatePagerNavButton(this.spanContainer, "Page", "Prev", style1.PrevPageText, style1.PrevPageImageUrl, style1.PrevPageToolTip, "rgPagePrev", false);
					}
					else
					{
						this.CreateSEOPagerNavButton(this.spanContainer, 1, style1.FirstPageText, style1.FirstPageImageUrl, style1.FirstPageToolTip, false, () => style1.FirstPageImageUrl);
						this.spanContainer.Controls.Add(new LiteralControl(string.Empty));
						this.CreateSEOPagerNavButton(this.spanContainer, base.OwnerTableView.CurrentPageIndex, style1.PrevPageText, style1.PrevPageImageUrl, style1.PrevPageToolTip, false, () => style1.PrevPageImageUrl);
					}
				}
				else if (flag)
				{
					this.CreateSEODummyNavButton(this.spanContainer, style1.FirstPageText, style1.FirstPageImageUrl, style1.FirstPageToolTip, () => style1.FirstPageImageUrl);
					this.spanContainer.Controls.Add(new LiteralControl(string.Empty));
					this.CreateSEODummyNavButton(this.spanContainer, style1.PrevPageText, style1.PrevPageImageUrl, style1.PrevPageToolTip, () => style1.PrevPageImageUrl);
				}
				else
				{
					if (base.OwnerTableView.OwnerGrid.ShouldRenderImg(style1.FirstPageImageUrl))
					{
						Image image = new Image();
						if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
						{
							image.Attributes["onclick"] = string.Format(this.onClickFormat, base.OwnerTableView.ClientID, "First");
						}
						image.ImageUrl = style1.FirstPageImageUrl;
						image.ToolTip = style1.FirstPageToolTip;
						image.AlternateText = style1.FirstPageToolTip;
						this.spanContainer.Controls.Add(image);
						this.spanContainer.Controls.Add(new LiteralControl(string.Empty));
						if (!string.IsNullOrEmpty(style1.FirstPageText))
						{
							this.spanContainer.Controls.Add(new LiteralControl(string.Format("&nbsp;{0}", style1.FirstPageText)));
						}
					}
					else
					{
						Button button = new Button();
						if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
						{
							button = new ElasticButton("t-font-icon rgIcon");
							button.CssClass = "t-button rgActionButton rgPageFirst";
							if (base.OwnerTableView.OwnerGrid.EnableAriaSupport)
							{
								button.Attributes.Add("aria-label", style1.FirstPageToolTip);
							}
						}
						else
						{
							button.CssClass = "rgPageFirst";
						}
						if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
						{
							button.OnClientClick = string.Format(this.onClickFormat, base.OwnerTableView.ClientID, "First");
						}
						else
						{
							button.OnClientClick = "return false;";
						}
						button.Text = " ";
						button.ToolTip = style1.FirstPageToolTip;
						this.spanContainer.Controls.Add(button);
						this.spanContainer.Controls.Add(new LiteralControl(string.Empty));
						if (!string.IsNullOrEmpty(style1.FirstPageText))
						{
							this.spanContainer.Controls.Add(new LiteralControl(style1.FirstPageText));
						}
					}
					if (base.OwnerTableView.OwnerGrid.ShouldRenderImg(style1.PrevPageImageUrl))
					{
						Image image2 = new Image();
						if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
						{
							image2.Attributes["onclick"] = string.Format(this.onClickFormat, base.OwnerTableView.ClientID, "Prev");
						}
						image2.ImageUrl = style1.PrevPageImageUrl;
						image2.ToolTip = style1.PrevPageToolTip;
						image2.AlternateText = style1.PrevPageToolTip;
						this.spanContainer.Controls.Add(new LiteralControl(string.Empty));
						this.spanContainer.Controls.Add(image2);
						if (!string.IsNullOrEmpty(style1.PrevPageText))
						{
							this.spanContainer.Controls.Add(new LiteralControl(string.Format("&nbsp;{0}", style1.PrevPageText)));
						}
					}
					else
					{
						Button button2 = new Button();
						if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
						{
							button2 = new ElasticButton("t-font-icon rgIcon");
							button2.CssClass = "t-button rgActionButton rgPagePrev";
							if (base.OwnerTableView.OwnerGrid.EnableAriaSupport)
							{
								button2.Attributes.Add("aria-label", style1.FirstPageToolTip);
							}
						}
						else
						{
							button2.CssClass = "rgPagePrev";
						}
						if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
						{
							button2.OnClientClick = string.Format(this.onClickFormat, base.OwnerTableView.ClientID, "Prev");
						}
						else
						{
							button2.OnClientClick = "return false;";
						}
						button2.Text = " ";
						button2.ToolTip = style1.PrevPageToolTip;
						this.spanContainer.Controls.Add(button2);
						if (!string.IsNullOrEmpty(style1.PrevPageText))
						{
							this.spanContainer.Controls.Add(new LiteralControl(style1.PrevPageText));
						}
					}
				}
			}
			if (style1.Mode == GridPagerMode.NumericPages || style1.Mode == GridPagerMode.NextPrevAndNumeric || style1.Mode == GridPagerMode.NextPrevNumericAndAdvanced)
			{
				Control control = this.spanContainer;
				if (style1.Mode == GridPagerMode.NextPrevAndNumeric || style1.Mode == GridPagerMode.NextPrevNumericAndAdvanced)
				{
					control = new Panel();
					((Panel)control).CssClass = "rgWrap rgNumPart";
					webControl.Controls.Add(control);
				}
				if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
				{
					if (this.IsTopPager)
					{
						control.Controls.Add(new LiteralControl(string.Format("<div id=\"{0}NPPHTop\">", base.OwnerTableView.ClientID)));
					}
					else
					{
						control.Controls.Add(new LiteralControl(string.Format("<div id=\"{0}NPPH\">", base.OwnerTableView.ClientID)));
					}
				}
				int pageCount = paging.PageCount;
				int num = paging.CurrentPageIndex + 1;
				int pageButtonCount = style1.PageButtonCount;
				int num2 = pageButtonCount;
				if (pageCount < num2)
				{
					num2 = pageCount;
				}
				int num3 = 1;
				int num4 = num2;
				if (num > num4)
				{
					int num5 = paging.CurrentPageIndex / pageButtonCount;
					num3 = num5 * pageButtonCount + 1;
					num4 = num3 + pageButtonCount - 1;
					if (num4 > pageCount)
					{
						num4 = pageCount;
					}
					if (num4 - num3 + 1 < pageButtonCount)
					{
						num3 = Math.Max(1, num4 - pageButtonCount + 1);
					}
				}
				if (num3 != 1)
				{
					if (!flag || base.OwnerTableView.OwnerGrid.IsDesignMode)
					{
						LinkButton linkButton = new GridLinkButton();
						linkButton.Text = "...";
						linkButton.CommandName = "Page";
						linkButton.CommandArgument = (num3 - 1).ToString(NumberFormatInfo.InvariantInfo);
						linkButton.CausesValidation = false;
						linkButton.ToolTip = style1.PrevPagesToolTip;
						if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
						{
							linkButton.OnClientClick = string.Format(this.onClickFormat, base.OwnerTableView.ClientID, linkButton.CommandArgument);
						}
						Label label = new Label();
						label.Text = "...";
						linkButton.Controls.Add(label);
						control.Controls.Add(linkButton);
					}
					else
					{
						HyperLink hyperLink = new HyperLink();
						hyperLink.Text = "...";
						int num6 = num3 - 1;
						hyperLink.ToolTip = style1.PrevPagesToolTip;
						this.FixHyperLinkUrl(num6.ToString(NumberFormatInfo.InvariantInfo), hyperLink);
						Label label2 = new Label();
						label2.Text = "...";
						hyperLink.Controls.Add(label2);
						control.Controls.Add(hyperLink);
					}
				}
				WebControl webControl2 = null;
				int num7 = 0;
				for (int i = num3; i <= num4; i++)
				{
					string text2 = i.ToString(NumberFormatInfo.InvariantInfo);
					if (i == num)
					{
						if (i == num3)
						{
							num7 = 2;
						}
						else
						{
							webControl2.CssClass = string.Empty;
							num7 = 1;
						}
						LinkButton linkButton;
						if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
						{
							linkButton = new GridLinkButton();
							linkButton.CommandName = "Page";
							linkButton.CommandArgument = text2;
							linkButton.CausesValidation = false;
							linkButton.OnClientClick = string.Format(this.onClickFormat, base.OwnerTableView.ClientID, linkButton.CommandArgument);
						}
						else
						{
							linkButton = new GridLinkButton();
							linkButton.CausesValidation = false;
							linkButton.OnClientClick = "return false;";
						}
						linkButton.CssClass = "rgCurrentPage";
						webControl2 = linkButton;
					}
					else
					{
						if (!flag || base.OwnerTableView.OwnerGrid.IsDesignMode)
						{
							LinkButton linkButton = new GridLinkButton();
							linkButton.CommandName = "Page";
							linkButton.CommandArgument = text2;
							linkButton.CausesValidation = false;
							if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
							{
								linkButton.OnClientClick = string.Format(this.onClickFormat, base.OwnerTableView.ClientID, linkButton.CommandArgument);
							}
							webControl2 = linkButton;
						}
						else
						{
							HyperLink hyperLink2 = new HyperLink();
							hyperLink2.Text = text2;
							this.FixHyperLinkUrl(text2, hyperLink2);
							webControl2 = hyperLink2;
						}
						if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile && num7 <= 0)
						{
							webControl2.CssClass = "rgHiddenItem";
						}
						else
						{
							num7--;
						}
					}
					if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
					{
						if (webControl2 is HyperLink)
						{
							(webControl2 as HyperLink).Text = text2;
						}
						else
						{
							(webControl2 as GridLinkButton).Text = text2;
						}
					}
					else
					{
						Label label3 = new Label();
						label3.Text = text2;
						webControl2.Controls.Add(label3);
					}
					if (base.OwnerTableView.OwnerGrid.EnableAriaSupport)
					{
						webControl2.ToolTip = string.Format("Go to page {0}", text2);
					}
					control.Controls.Add(webControl2);
				}
				if (pageCount > num4)
				{
					if (!flag || base.OwnerTableView.OwnerGrid.IsDesignMode)
					{
						LinkButton linkButton = new GridLinkButton();
						linkButton.Text = "...";
						linkButton.CommandName = "Page";
						linkButton.CommandArgument = (num4 + 1).ToString(NumberFormatInfo.InvariantInfo);
						linkButton.CausesValidation = false;
						linkButton.ToolTip = style1.NextPagesToolTip;
						if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
						{
							linkButton.OnClientClick = string.Format(this.onClickFormat, base.OwnerTableView.ClientID, linkButton.CommandArgument);
						}
						Label label4 = new Label();
						label4.Text = "...";
						linkButton.Controls.Add(label4);
						control.Controls.Add(linkButton);
					}
					else
					{
						HyperLink hyperLink3 = new HyperLink();
						hyperLink3.Text = "...";
						int num6 = num4 + 1;
						hyperLink3.ToolTip = style1.NextPagesToolTip;
						this.FixHyperLinkUrl(num6.ToString(NumberFormatInfo.InvariantInfo), hyperLink3);
						Label label5 = new Label();
						label5.Text = "...";
						hyperLink3.Controls.Add(label5);
						control.Controls.Add(hyperLink3);
					}
				}
				if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
				{
					control.Controls.Add(new LiteralControl("</div>"));
				}
			}
			if (style1.Mode == GridPagerMode.NextPrev || style1.Mode == GridPagerMode.NextPrevAndNumeric || style1.Mode == GridPagerMode.NextPrevNumericAndAdvanced)
			{
				Panel panel4 = null;
				if (style1.Mode == GridPagerMode.NextPrevAndNumeric || style1.Mode == GridPagerMode.NextPrevNumericAndAdvanced)
				{
					panel4 = new Panel();
					panel4.CssClass = "rgWrap rgArrPart2";
					webControl.Controls.Add(panel4);
				}
				if (!paging.IsLastPage && base.OwnerTableView.PageSize < paging.DataSourceCount)
				{
					if (!flag || base.OwnerTableView.OwnerGrid.IsDesignMode)
					{
						Control control2 = this.spanContainer;
						if (panel4 != null)
						{
							control2 = panel4;
						}
						this.CreatePagerNavButton(control2, "Page", "Next", style1.NextPageText, style1.NextPageImageUrl, style1.NextPageToolTip, "rgPageNext", true);
						control2.Controls.Add(new LiteralControl(string.Empty));
						this.CreatePagerNavButton(control2, "Page", "Last", style1.LastPageText, style1.LastPageImageUrl, style1.LastPageToolTip, "rgPageLast", true);
						if (base.OwnerTableView.Dir == GridTableTextDirection.RTL)
						{
							Label label6 = new Label();
							label6.Style["visibility"] = "hidden";
							label6.Text = "rtl";
							control2.Controls.Add(label6);
						}
					}
					else
					{
						Control control3 = this.spanContainer;
						if (panel4 != null)
						{
							control3 = panel4;
						}
						this.CreateSEOPagerNavButton(control3, base.OwnerTableView.CurrentPageIndex + 2, style1.NextPageText, style1.NextPageImageUrl, style1.NextPageToolTip, true, () => style1.NextPageImageUrl);
						control3.Controls.Add(new LiteralControl(string.Empty));
						this.CreateSEOPagerNavButton(control3, base.OwnerTableView.PagingManager.PageCount, style1.LastPageText, style1.LastPageImageUrl, style1.LastPageToolTip, true, () => style1.LastPageImageUrl);
						if (base.OwnerTableView.Dir == GridTableTextDirection.RTL)
						{
							Label label7 = new Label();
							label7.Style["visibility"] = "hidden";
							label7.Text = "rtl";
							control3.Controls.Add(label7);
						}
					}
				}
				else
				{
					if (flag)
					{
						Panel panel5 = (panel4 != null) ? panel4 : this.spanContainer;
						this.CreateSEODummyNavButton(panel5, style1.NextPageText, style1.NextPageImageUrl, style1.NextPageToolTip, () => style1.NextPageImageUrl);
						panel5.Controls.Add(new LiteralControl(string.Empty));
						this.CreateSEODummyNavButton(panel5, style1.LastPageText, style1.LastPageImageUrl, style1.LastPageToolTip, () => style1.LastPageImageUrl);
					}
					else
					{
						if (base.OwnerTableView.OwnerGrid.ShouldRenderImg(style1.NextPageImageUrl))
						{
							if (!string.IsNullOrEmpty(style1.NextPageText))
							{
								if (panel4 != null)
								{
									panel4.Controls.Add(new LiteralControl(string.Format("{0}&nbsp;", style1.NextPageText)));
								}
								else
								{
									this.spanContainer.Controls.Add(new LiteralControl(string.Format("{0}&nbsp;", style1.NextPageText)));
								}
							}
							Image image3 = new Image();
							image3.ImageUrl = style1.NextPageImageUrl;
							image3.ToolTip = style1.NextPageToolTip;
							image3.AlternateText = style1.NextPageToolTip;
							if (panel4 != null)
							{
								panel4.Controls.Add(image3);
								panel4.Controls.Add(new LiteralControl(string.Empty));
							}
							else
							{
								this.spanContainer.Controls.Add(image3);
								this.spanContainer.Controls.Add(new LiteralControl(string.Empty));
							}
						}
						else
						{
							if (!string.IsNullOrEmpty(style1.NextPageText))
							{
								if (panel4 != null)
								{
									panel4.Controls.Add(new LiteralControl(style1.NextPageText));
								}
								else
								{
									this.spanContainer.Controls.Add(new LiteralControl(style1.NextPageText));
								}
							}
							Button button3 = new Button();
							if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
							{
								button3 = new ElasticButton("t-font-icon rgIcon");
								button3.CssClass = "t-button rgActionButton rgPageNext";
								if (base.OwnerTableView.OwnerGrid.EnableAriaSupport)
								{
									button3.Attributes.Add("aria-label", style1.NextPageToolTip);
								}
							}
							else
							{
								button3.CssClass = "rgPageNext";
							}
							if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
							{
								button3.OnClientClick = string.Format(this.onClickFormat, base.OwnerTableView.ClientID, "Next");
							}
							else
							{
								button3.OnClientClick = "return false;";
							}
							button3.ToolTip = style1.NextPageToolTip;
							button3.Text = " ";
							if (panel4 != null)
							{
								panel4.Controls.Add(button3);
								panel4.Controls.Add(new LiteralControl(string.Empty));
							}
							else
							{
								this.spanContainer.Controls.Add(button3);
								this.spanContainer.Controls.Add(new LiteralControl(string.Empty));
							}
							if (!string.IsNullOrEmpty(style1.LastPageText))
							{
								if (panel4 != null)
								{
									panel4.Controls.Add(new LiteralControl(style1.LastPageText));
								}
								else
								{
									this.spanContainer.Controls.Add(new LiteralControl(style1.LastPageText));
								}
							}
							button3 = new Button();
							if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
							{
								button3 = new ElasticButton("t-font-icon rgIcon");
								button3.CssClass = "t-button rgActionButton rgPageLast";
								if (base.OwnerTableView.OwnerGrid.EnableAriaSupport)
								{
									button3.Attributes.Add("aria-label", style1.LastPageToolTip);
								}
							}
							else
							{
								button3.CssClass = "rgPageLast";
							}
							if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
							{
								button3.OnClientClick = string.Format(this.onClickFormat, base.OwnerTableView.ClientID, "Last");
							}
							else
							{
								button3.OnClientClick = "return false;";
							}
							button3.ToolTip = style1.LastPageToolTip;
							button3.Text = " ";
							if (panel4 != null)
							{
								panel4.Controls.Add(new LiteralControl(string.Empty));
								panel4.Controls.Add(button3);
							}
							else
							{
								this.spanContainer.Controls.Add(new LiteralControl(string.Empty));
								this.spanContainer.Controls.Add(button3);
							}
						}
						if (base.OwnerTableView.OwnerGrid.ShouldRenderImg(style1.LastPageImageUrl))
						{
							if (!string.IsNullOrEmpty(style1.LastPageText))
							{
								if (panel4 != null)
								{
									panel4.Controls.Add(new LiteralControl(string.Format("{0}&nbsp;", style1.LastPageText)));
								}
								else
								{
									this.spanContainer.Controls.Add(new LiteralControl(string.Format("{0}&nbsp;", style1.LastPageText)));
								}
							}
							Image image4 = new Image();
							image4.ImageUrl = style1.LastPageImageUrl;
							image4.ToolTip = style1.LastPageToolTip;
							image4.AlternateText = style1.LastPageToolTip;
							if (panel4 != null)
							{
								panel4.Controls.Add(new LiteralControl(string.Empty));
								panel4.Controls.Add(image4);
							}
							else
							{
								this.spanContainer.Controls.Add(new LiteralControl(string.Empty));
								this.spanContainer.Controls.Add(image4);
							}
						}
					}
					if (base.OwnerTableView.Dir == GridTableTextDirection.RTL)
					{
						Label label8 = new Label();
						label8.Style["visibility"] = "hidden";
						label8.Text = "rtl";
						if (panel4 != null)
						{
							panel4.Controls.Add(label8);
						}
						else
						{
							this.spanContainer.Controls.Add(label8);
						}
					}
				}
			}
			if (style1.Mode == GridPagerMode.Advanced || style1.Mode == GridPagerMode.NextPrevAndNumeric)
			{
				Panel panel6;
				if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight || base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile)
				{
					panel6 = new Panel();
					webControl.Controls.Add(panel6);
					if (!base.OwnerTableView.OwnerGrid.EmptySkin())
					{
						panel6.CssClass = "rgWrap rgAdvPart";
					}
				}
				else
				{
					panel6 = this.CreateAdvancedPanel((TableCell)webControl);
				}
				if (style1.Mode == GridPagerMode.NextPrevAndNumeric)
				{
					if (base.OwnerTableView.PagerStyle.PageSizeControlType != PagerDropDownControlType.None)
					{
						this.Builder.CreatePageSize(panel6);
					}
				}
				else
				{
					this.CreateAdvancedNavigationControls(paging, flag, panel6);
				}
			}
			Panel panel7 = new Panel();
			if (!base.OwnerTableView.OwnerGrid.EmptySkin())
			{
				panel7.CssClass = "rgWrap rgInfoPart";
			}
			if (array.Length > 1)
			{
				if (!this._generateSpanContainer)
				{
					webControl.Controls.Add(panel7);
				}
				panel7.Controls.Add(new LiteralControl(array[1]));
			}
			if (style1.Mode == GridPagerMode.NextPrevNumericAndAdvanced)
			{
				Panel panel8;
				if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight || base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile)
				{
					panel8 = new Panel();
					webControl.Controls.Add(panel8);
					if (!base.OwnerTableView.OwnerGrid.EmptySkin())
					{
						panel8.CssClass = "rgWrap rgAdvPart";
					}
				}
				else
				{
					panel8 = this.CreateAdvancedPanel((TableCell)webControl);
				}
				this.CreateAdvancedNavigationControls(paging, flag, panel8);
			}
			if (style1.Mode == GridPagerMode.Slider)
			{
				this.CreateSliderNavigationControls(this.spanContainer, flag);
			}
		}

		// Token: 0x0600B473 RID: 46195 RVA: 0x0027A498 File Offset: 0x00278698
		private int GetNormalizedPageCount(GridPagingManager pagingManager)
		{
			if (pagingManager.PageCount != 0)
			{
				return pagingManager.PageCount;
			}
			if (pagingManager.DataSourceCount <= 0)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x0600B474 RID: 46196 RVA: 0x0027A4B8 File Offset: 0x002786B8
		private string GetSeoPageUrlParameter(string url, int? pageSize)
		{
			if (!(pageSize == base.OwnerTableView.OwnerGrid._defaultPageSize) && pageSize != null)
			{
				return string.Format("{0}_{1}", url, pageSize);
			}
			return url;
		}

		// Token: 0x0600B475 RID: 46197 RVA: 0x0027A50C File Offset: 0x0027870C
		internal string GeneratePageSizeAttributeLink(int pageSize, bool replacePageIndexWithInt32MinValue)
		{
			if (base.DesignMode)
			{
				return "";
			}
			string url = this.Page.Response.ApplyAppPathModifier(this.Page.Request.RawUrl);
			int num = replacePageIndexWithInt32MinValue ? int.MinValue : (base.OwnerTableView.CurrentPageIndex + 1);
			if (this.GetUseRouting() && !string.IsNullOrEmpty(this.GetSEOPageIndexRouteParameterName()))
			{
				return this.FixRoutedStringUrl(num.ToString(), new int?(pageSize));
			}
			return this.AppendKeyValuePairToQueryString(url, this.SEOPagingQueryStringKey(), this.GetSeoPageUrlParameter(num.ToString(), new int?(pageSize)));
		}

		// Token: 0x0600B476 RID: 46198 RVA: 0x0027A5D8 File Offset: 0x002787D8
		private void CreateSEODummyNavButton(Control container, string text, string imageUrl, string toolTip, TFunc<string> imageUrlFunction)
		{
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				GridPagerStyle renderPagerStyle = base.OwnerTableView.RenderPagerStyle;
				HyperLink hyperLink = new HyperLink();
				hyperLink.ToolTip = toolTip;
				string text2 = string.Empty;
				if (toolTip == renderPagerStyle.PrevPageToolTip)
				{
					text2 += "t-button rgActionButton rgPagePrev";
				}
				else if (toolTip == renderPagerStyle.FirstPageToolTip)
				{
					text2 += "t-button rgActionButton rgPageFirst";
				}
				else if (toolTip == renderPagerStyle.NextPageToolTip)
				{
					text2 += "t-button rgActionButton rgPageNext";
				}
				else if (toolTip == renderPagerStyle.LastPageToolTip)
				{
					text2 += "t-button rgActionButton rgPageLast";
				}
				hyperLink.CssClass = text2;
				if (!string.IsNullOrEmpty(text2))
				{
					HtmlGenericControl htmlGenericControl = new HtmlGenericControl("span");
					htmlGenericControl.Attributes.Add("class", "t-font-icon rgIcon");
					hyperLink.Controls.Add(htmlGenericControl);
				}
				if (!string.IsNullOrEmpty(text))
				{
					hyperLink.Controls.Add(new LiteralControl(string.Format("{0}&nbsp;", text)));
				}
				container.Controls.Add(hyperLink);
				return;
			}
			if (!string.IsNullOrEmpty(imageUrl))
			{
				HyperLink hyperLink2 = new HyperLink();
				hyperLink2.ToolTip = toolTip;
				if (!string.IsNullOrEmpty(imageUrl))
				{
					Image image = new Image();
					image.PreRender += delegate(object s, EventArgs e)
					{
						image.ImageUrl = imageUrlFunction();
					};
					image.AlternateText = toolTip;
					hyperLink2.Controls.Add(image);
				}
				if (!string.IsNullOrEmpty(text))
				{
					hyperLink2.Controls.Add(new LiteralControl(string.Format("{0}&nbsp;", text)));
				}
				container.Controls.Add(hyperLink2);
			}
		}

		// Token: 0x0600B477 RID: 46199 RVA: 0x0027A7E0 File Offset: 0x002789E0
		private void CreateSEOPagerNavButton(Control container, int goToIndex, string text, string imageUrl, string toolTipText, bool isReversed, TFunc<string> imageUrlFunction)
		{
			HyperLink hyperLink = new HyperLink();
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				container.Controls.Add(hyperLink);
				GridPagerStyle renderPagerStyle = base.OwnerTableView.RenderPagerStyle;
				hyperLink.ToolTip = toolTipText;
				string text2 = string.Empty;
				if (toolTipText == renderPagerStyle.PrevPageToolTip)
				{
					text2 += "t-button rgActionButton rgPagePrev";
				}
				else if (toolTipText == renderPagerStyle.FirstPageToolTip)
				{
					text2 += "t-button rgActionButton rgPageFirst";
				}
				else if (toolTipText == renderPagerStyle.NextPageToolTip)
				{
					text2 += "t-button rgActionButton rgPageNext";
				}
				else if (toolTipText == renderPagerStyle.LastPageToolTip)
				{
					text2 += "t-button rgActionButton rgPageLast";
				}
				hyperLink.CssClass = text2;
				if (!string.IsNullOrEmpty(text2))
				{
					HtmlGenericControl htmlGenericControl = new HtmlGenericControl("span");
					htmlGenericControl.Attributes.Add("class", "t-font-icon rgIcon");
					hyperLink.Controls.Add(htmlGenericControl);
				}
				this.FixHyperLinkUrl(goToIndex.ToString(NumberFormatInfo.InvariantInfo), hyperLink);
				if (!string.IsNullOrEmpty(text) && !isReversed)
				{
					hyperLink.Controls.Add(new LiteralControl(string.Format("&nbsp;{0}", text)));
				}
				return;
			}
			container.Controls.Add(hyperLink);
			hyperLink.ToolTip = toolTipText;
			if (!string.IsNullOrEmpty(imageUrl))
			{
				Image image = new Image();
				image.PreRender += delegate(object s, EventArgs e)
				{
					image.ImageUrl = imageUrlFunction();
				};
				image.AlternateText = toolTipText;
				if (isReversed)
				{
					if (!string.IsNullOrEmpty(text))
					{
						hyperLink.Controls.Add(new LiteralControl(string.Format("{0}&nbsp;", text)));
					}
					hyperLink.Controls.Add(image);
				}
				else
				{
					hyperLink.Controls.Add(image);
				}
			}
			this.FixHyperLinkUrl(goToIndex.ToString(NumberFormatInfo.InvariantInfo), hyperLink);
			if (!string.IsNullOrEmpty(text) && !isReversed)
			{
				hyperLink.Controls.Add(new LiteralControl(string.Format("&nbsp;{0}", text)));
			}
		}

		// Token: 0x0600B478 RID: 46200 RVA: 0x0027AA14 File Offset: 0x00278C14
		private void CreatePagerNavButton(Control container, string commandName, string commandArgument, string buttonText, string imageUrl, string toolTipText, string imageCssClassName, bool isReversed)
		{
			LinkButton linkButton = new GridLinkButton();
			if (base.OwnerTableView.OwnerGrid.ShouldRenderImg(imageUrl))
			{
				container.Controls.Add(linkButton);
			}
			linkButton.CommandName = commandName;
			linkButton.CommandArgument = commandArgument;
			linkButton.CausesValidation = false;
			if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
			{
				linkButton.OnClientClick = string.Format(this.onClickFormat, base.OwnerTableView.ClientID, linkButton.CommandArgument);
			}
			linkButton.ToolTip = toolTipText;
			if (base.OwnerTableView.OwnerGrid.ShouldRenderImg(imageUrl) && (string.IsNullOrEmpty(buttonText) || !isReversed))
			{
				Image image = new Image();
				image.ImageUrl = imageUrl;
				image.AlternateText = toolTipText;
				image.ToolTip = toolTipText;
				linkButton.Controls.Add(image);
			}
			else if (!base.OwnerTableView.OwnerGrid.ShouldRenderImg(imageUrl))
			{
				Button button = new Button();
				if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					button = new ElasticButton("t-font-icon rgIcon");
					button.UseSubmitBehavior = false;
					button.CssClass = string.Format("{0} {1}", "t-button rgActionButton", imageCssClassName);
					if (base.OwnerTableView.OwnerGrid.EnableAriaSupport)
					{
						button.Attributes.Add("aria-label", toolTipText);
					}
				}
				else
				{
					button.CssClass = imageCssClassName;
				}
				button.Text = " ";
				button.CommandName = commandName;
				button.CommandArgument = commandArgument;
				button.CausesValidation = false;
				button.ToolTip = toolTipText;
				if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
				{
					button.OnClientClick = string.Format(this.onClickFormat, base.OwnerTableView.ClientID, button.CommandArgument);
				}
				if (isReversed)
				{
					if (!string.IsNullOrEmpty(buttonText) && !base.OwnerTableView.OwnerGrid.ShouldRenderImg(imageUrl))
					{
						LinkButton linkButton2 = new GridLinkButton();
						linkButton2.CommandName = commandName;
						linkButton2.CommandArgument = commandArgument;
						linkButton2.CausesValidation = false;
						linkButton2.ToolTip = toolTipText;
						linkButton2.Text = buttonText;
						if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
						{
							linkButton2.OnClientClick = string.Format(this.onClickFormat, base.OwnerTableView.ClientID, linkButton2.CommandArgument);
						}
						container.Controls.Add(linkButton2);
					}
					container.Controls.Add(button);
				}
				else
				{
					container.Controls.Add(button);
				}
			}
			if (!string.IsNullOrEmpty(buttonText))
			{
				if (base.OwnerTableView.OwnerGrid.ShouldRenderImg(imageUrl))
				{
					if (isReversed)
					{
						linkButton.Controls.Add(new LiteralControl(string.Format("{0}&nbsp;", buttonText)));
						Image image2 = new Image();
						image2.ImageUrl = imageUrl;
						image2.AlternateText = toolTipText;
						linkButton.Controls.Add(image2);
						return;
					}
					linkButton.Controls.Add(new LiteralControl(string.Format("&nbsp;{0}", buttonText)));
					return;
				}
				else if (!isReversed)
				{
					LinkButton linkButton3 = new GridLinkButton();
					linkButton3.CommandName = commandName;
					linkButton3.CommandArgument = commandArgument;
					linkButton3.CausesValidation = false;
					linkButton3.ToolTip = toolTipText;
					linkButton3.Text = buttonText;
					if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
					{
						linkButton3.OnClientClick = string.Format(this.onClickFormat, base.OwnerTableView.ClientID, linkButton3.CommandArgument);
					}
					container.Controls.Add(linkButton3);
				}
			}
		}

		// Token: 0x0600B479 RID: 46201 RVA: 0x0027ADB8 File Offset: 0x00278FB8
		private void CreateSliderNavigationControls(Control container, bool isSeoPaging)
		{
			RadSlider radSlider = new RadSlider();
			radSlider.RenderMode = base.OwnerTableView.OwnerGrid.RenderMode;
			if (!base.OwnerTableView.OwnerGrid.IsClientCommandAssigned || isSeoPaging)
			{
				radSlider.ValueChanged += this.slider_ValueChanged;
			}
			radSlider.ID = "GridSliderPager";
			radSlider.RenderMode = base.OwnerTableView.OwnerGrid.RenderMode;
			radSlider.IncreaseText = this.GetLocalizationString((GridStrings loc) => loc.SliderIncreaseText, radSlider.IncreaseText);
			radSlider.DecreaseText = this.GetLocalizationString((GridStrings loc) => loc.SliderDecreaseText, radSlider.DecreaseText);
			radSlider.DragText = this.GetLocalizationString((GridStrings loc) => loc.SliderDragText, radSlider.DragText);
			radSlider.EnableEmbeddedSkins = base.OwnerTableView.OwnerGrid.EnableEmbeddedSkins;
			radSlider.PreRender += delegate(object sender, EventArgs e)
			{
				((RadSlider)sender).Skin = base.OwnerTableView.OwnerGrid.RuntimeSkin;
			};
			radSlider.AutoPostBack = (!base.OwnerTableView.OwnerGrid.IsClientCommandAssigned && !isSeoPaging);
			radSlider.Value = this.Paging.CurrentPageIndex;
			radSlider.Width = Unit.Pixel(200);
			radSlider.MinimumValue = 0m;
			radSlider.MaximumValue = this.Paging.PageCount - 1;
			if (!this._generateSpanContainer)
			{
				container.Controls.Add(radSlider);
			}
			Panel panel = new Panel();
			panel.ID = this.OwnerGridID + "_SliderPagerLabel";
			if ((base.OwnerTableView.OwnerGrid.IsClientCommandAssigned || isSeoPaging) && this.IsTopPager)
			{
				panel.ID += "Top";
			}
			panel.CssClass = "sliderPagerLabel_" + base.OwnerTableView.OwnerGrid.RuntimeSkin;
			panel.Controls.Add(new LiteralControl(string.Format(base.OwnerTableView.OwnerGrid.ClientSettings.ClientMessages.PagerTooltipFormatString, this.Paging.CurrentPageIndex + 1, this.Paging.PageCount)));
			if (!this._generateSpanContainer)
			{
				container.Controls.Add(panel);
			}
			if (!base.OwnerTableView.OwnerGrid.EmptySkin())
			{
				panel.CssClass = "rgSliderLabel";
			}
			if ((base.OwnerTableView.OwnerGrid.IsClientCommandAssigned || isSeoPaging) && this.IsTopPager)
			{
				base.OwnerTableView.sliderTopClientID = radSlider.ClientID;
				base.OwnerTableView.sliderTopLabelClientID = panel.ClientID;
			}
			else
			{
				base.OwnerTableView.sliderClientID = radSlider.ClientID;
				base.OwnerTableView.sliderLabelClientID = panel.ClientID;
			}
			if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned || isSeoPaging)
			{
				string text = this.GeneratePageSizeAttributeLink(base.OwnerTableView.PageSize, true);
				radSlider.LiveDrag = false;
				radSlider.OnClientValueChanged = string.Format("function(sender, args){{var v = sender.get_value();if(!$find(\"{0}\").page(v + 1))sender.set_value(v); $find(\"{0}\")._sliderClientValueChanged(\"{1}\", \"{2}\", \"{3}\");}}", new object[]
				{
					base.OwnerTableView.ClientID,
					panel.ClientID,
					radSlider.ClientID,
					isSeoPaging ? text : ""
				});
				return;
			}
			radSlider.OnClientValueChanged = string.Format("function(){{$find(\"{0}\")._sliderClientValueChanged(\"{1}\", \"{2}\", \"{3}\");}}", new object[]
			{
				this.OwnerID,
				panel.ClientID,
				radSlider.ClientID,
				""
			});
		}

		// Token: 0x0600B47A RID: 46202 RVA: 0x0027B168 File Offset: 0x00279368
		protected virtual Panel CreateAdvancedPanel(TableCell pagerCell)
		{
			Panel panel = new Panel();
			pagerCell.Controls.Add(panel);
			if (!base.OwnerTableView.OwnerGrid.EmptySkin())
			{
				panel.CssClass = "rgWrap rgAdvPart";
			}
			return panel;
		}

		// Token: 0x0600B47B RID: 46203 RVA: 0x0027B208 File Offset: 0x00279408
		private void CreateAdvancedNavigationControls(GridPagingManager pagingManager, bool isSEOPaging, Panel advancedPanel)
		{
			Label label = new Label();
			label.Text = HttpUtility.HtmlEncode(this.GetLocalizationString((GridStrings loc) => loc.GoToPageLabelText, "Page:"));
			label.ID = "GoToPageLabel";
			if (!base.OwnerTableView.OwnerGrid.EmptySkin())
			{
				label.CssClass = "rgPagerLabel";
			}
			advancedPanel.Controls.Add(label);
			RadNumericTextBox radNumericTextBox = new RadNumericTextBox();
			radNumericTextBox.RenderMode = base.OwnerTableView.OwnerGrid.RenderMode;
			radNumericTextBox.ID = "GoToPageTextBox";
			AccessibilityHelper.AddToolTip(radNumericTextBox, this.RenderPagerStyle.GoToPageTextBoxToolTip);
			radNumericTextBox.EnableAriaSupport = base.OwnerTableView.OwnerGrid.EnableAriaSupport;
			radNumericTextBox.PreRender += delegate(object sender, EventArgs e)
			{
				((RadNumericTextBox)sender).Skin = base.OwnerTableView.OwnerGrid.RuntimeSkin;
			};
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Classic)
			{
				if (radNumericTextBox.EnableSingleInputRendering)
				{
					radNumericTextBox.Width = Unit.Pixel(10 + pagingManager.PageCount.ToString().Length * 10);
				}
				else
				{
					radNumericTextBox.Width = Unit.Pixel(pagingManager.PageCount.ToString().Length * 10);
				}
				if ((base.OwnerTableView.OwnerGrid.RuntimeSkin == "MetroTouch" || base.OwnerTableView.OwnerGrid.RuntimeSkin == "Glow" || base.OwnerTableView.OwnerGrid.RuntimeSkin == "Silk" || base.OwnerTableView.OwnerGrid.RuntimeSkin == "BlackMetroTouch" || base.OwnerTableView.OwnerGrid.RuntimeSkin == "Bootstrap") && radNumericTextBox.Width.Value < 50.0)
				{
					radNumericTextBox.Width = Unit.Pixel(50);
				}
			}
			else
			{
				radNumericTextBox.Width = Unit.Parse(2.2857000827789307 + (double)(pagingManager.PageCount.ToString().Length - 1) * 0.6 + "em");
				if (radNumericTextBox.EnableAriaSupport && (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight))
				{
					radNumericTextBox.Attributes.Add("aria-label", string.IsNullOrEmpty(this.RenderPagerStyle.GoToPageTextBoxToolTip) ? "Page" : this.RenderPagerStyle.GoToPageTextBoxToolTip);
				}
			}
			radNumericTextBox.Text = HttpUtility.HtmlEncode((pagingManager.CurrentPageIndex + 1).ToString());
			if (pagingManager.PageCount > 0)
			{
				radNumericTextBox.MinValue = 1.0;
				radNumericTextBox.MaxValue = (double)pagingManager.PageCount;
				radNumericTextBox.NumberFormat.DecimalDigits = 0;
			}
			if (!base.OwnerTableView.OwnerGrid.EmptySkin())
			{
				radNumericTextBox.CssClass = "rgPagerTextBox";
			}
			radNumericTextBox.EnableEmbeddedSkins = base.OwnerTableView.OwnerGrid.EnableEmbeddedSkins;
			advancedPanel.Controls.Add(radNumericTextBox);
			Label label2 = new Label();
			label2.Text = HttpUtility.HtmlEncode(string.Format(this.GetLocalizationString((GridStrings loc) => loc.PageOfLabelText, "of {0}"), pagingManager.PageCount));
			label2.ID = "PageOfLabel";
			if (!base.OwnerTableView.OwnerGrid.EmptySkin())
			{
				label2.CssClass = "rgPagerLabel";
			}
			advancedPanel.Controls.Add(label2);
			if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
			{
				if (this.IsTopPager)
				{
					base.OwnerTableView.pageOfLabelTopClientID = label2.ClientID;
				}
				else
				{
					base.OwnerTableView.pageOfLabelClientID = label2.ClientID;
				}
			}
			WebControl webControl;
			if (!isSEOPaging)
			{
				Button button;
				if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					button = new ElasticButton(string.Empty, "t-text rgButtonText");
					button.CssClass = "t-button ";
					if (base.OwnerTableView.OwnerGrid.EnableAriaSupport)
					{
						button.Attributes.Add("aria-label", this.RenderPagerStyle.GoToPageButtonToolTip);
					}
				}
				else
				{
					button = new Button();
					button.CssClass = string.Empty;
				}
				webControl = button;
				webControl.ID = "GoToPageLinkButton";
				button.Text = this.GetLocalizationString((GridStrings loc) => loc.GoToPageLinkButtonText, "Go");
				button.ToolTip = this.RenderPagerStyle.GoToPageButtonToolTip;
				button.CommandName = "Page";
				button.CommandArgument = "GoToPage";
				button.CausesValidation = false;
				if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
				{
					if (this.IsTopPager)
					{
						base.OwnerTableView.goToPageTextBoxTopClientID = radNumericTextBox.ClientID;
					}
					else
					{
						base.OwnerTableView.goToPageTextBoxClientID = radNumericTextBox.ClientID;
					}
					button.OnClientClick = string.Format("Telerik.Web.UI.Grid.NavigateToPage('{0}', $find('{1}').get_value()); return false;", base.OwnerTableView.ClientID, radNumericTextBox.ClientID);
				}
			}
			else
			{
				HyperLink hyperLink = new HyperLink();
				webControl = hyperLink;
				webControl.ID = "GoToPageLinkButton";
				hyperLink.Text = HttpUtility.HtmlEncode(this.GetLocalizationString((GridStrings loc) => loc.GoToPageLinkButtonText, "Go"));
				this.FixHyperLinkUrl(radNumericTextBox.Text, hyperLink);
			}
			if (!base.OwnerTableView.OwnerGrid.EmptySkin())
			{
				WebControl webControl2 = webControl;
				webControl2.CssClass += "rgPagerButton";
			}
			advancedPanel.Controls.Add(new LiteralControl("&nbsp;"));
			advancedPanel.Controls.Add(webControl);
			if (isSEOPaging)
			{
				webControl.Attributes["onclick"] = string.Format("Telerik.Web.UI.Grid.ChangePageIndexButtonClickHandler('{0}', '{1}', '{2}', '{3}'); return false;", new object[]
				{
					this.SEOPagingQueryStringKey(),
					base.OwnerTableView.OwnerGrid._defaultPageSize,
					radNumericTextBox.ClientID,
					base.OwnerTableView.ClientID
				});
			}
			else
			{
				webControl.Attributes["onclick"] = string.Format("var gtpi = $get('{0}'); if(gtpi.value == {1} || gtpi.value == '')return false;", radNumericTextBox.ClientID, pagingManager.CurrentPageIndex + 1);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("function(sender, args){");
			stringBuilder.Append("if(args.get_keyCode() == 13){sender.set_value(sender.get_textBoxValue()); args.get_domEvent().stopPropagation();args.get_domEvent().preventDefault();");
			stringBuilder.AppendFormat("var button = $get('{0}');", webControl.ClientID);
			stringBuilder.Append("if (button.click){ button.click(); }else{ eval(button.href); }");
			stringBuilder.Append("}");
			stringBuilder.Append("}");
			radNumericTextBox.ClientEvents.OnKeyPress = stringBuilder.ToString();
			Label label3 = new Label();
			label3.Text = HttpUtility.HtmlEncode(base.OwnerTableView.RenderPagerStyle.PageSizeLabelText);
			label3.ID = "ChangePageSizeLabel";
			if (!base.OwnerTableView.OwnerGrid.EmptySkin())
			{
				label3.CssClass = "rgPagerLabel";
			}
			advancedPanel.Controls.Add(label3);
			Button button2;
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				button2 = new ElasticButton(string.Empty, "t-text rgButtonText");
				button2.CssClass = "t-button ";
				if (base.OwnerTableView.OwnerGrid.EnableAriaSupport)
				{
					button2.Attributes.Add("aria-label", this.RenderPagerStyle.ChangePageSizeButtonToolTip);
				}
			}
			else
			{
				button2 = new Button();
				button2.CssClass = string.Empty;
			}
			button2.ID = "ChangePageSizeLinkButton";
			AccessibilityHelper.AddToolTip(button2, this.RenderPagerStyle.ChangePageSizeButtonToolTip);
			if (!base.OwnerTableView.OwnerGrid.EmptySkin())
			{
				Button button3 = button2;
				button3.CssClass += "rgPagerButton";
			}
			RadNumericTextBox radNumericTextBox2 = new RadNumericTextBox();
			radNumericTextBox2.RenderMode = base.OwnerTableView.OwnerGrid.RenderMode;
			radNumericTextBox2.ClientEvents.OnValueChanging = "function(sender, args){if(args.get_newValue() == '')args.set_cancel(true);}";
			radNumericTextBox2.PreRender += delegate(object sender, EventArgs e)
			{
				((RadNumericTextBox)sender).Skin = base.OwnerTableView.OwnerGrid.RuntimeSkin;
			};
			advancedPanel.Controls.Add(radNumericTextBox2);
			advancedPanel.Controls.Add(button2);
			radNumericTextBox2.ID = "ChangePageSizeTextBox";
			AccessibilityHelper.AddToolTip(radNumericTextBox2, this.RenderPagerStyle.ChangePageSizeTextBoxToolTip);
			radNumericTextBox2.EnableAriaSupport = base.OwnerTableView.OwnerGrid.EnableAriaSupport;
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Classic)
			{
				int num = (pagingManager.DataSourceCount == 0) ? 2 : pagingManager.DataSourceCount.ToString().Length;
				if (radNumericTextBox2.EnableSingleInputRendering)
				{
					radNumericTextBox2.Width = Unit.Pixel(10 + num * 10);
				}
				else
				{
					radNumericTextBox2.Width = Unit.Pixel(num * 10);
				}
				if ((base.OwnerTableView.OwnerGrid.RuntimeSkin == "MetroTouch" || base.OwnerTableView.OwnerGrid.RuntimeSkin == "Glow" || base.OwnerTableView.OwnerGrid.RuntimeSkin == "Silk" || base.OwnerTableView.OwnerGrid.RuntimeSkin == "BlackMetroTouch" || base.OwnerTableView.OwnerGrid.RuntimeSkin == "Bootstrap") && radNumericTextBox2.Width.Value < 40.0)
				{
					radNumericTextBox2.Width = Unit.Pixel(40);
				}
			}
			else
			{
				radNumericTextBox2.Width = Unit.Parse(2.2857000827789307 + (double)(pagingManager.DataSourceCount.ToString().Length - 1) * 0.6 + "em");
				if (radNumericTextBox2.EnableAriaSupport && (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight))
				{
					radNumericTextBox2.Attributes.Add("aria-label", string.IsNullOrEmpty(this.RenderPagerStyle.ChangePageSizeTextBoxToolTip) ? "Page size" : this.RenderPagerStyle.ChangePageSizeTextBoxToolTip);
				}
			}
			radNumericTextBox2.Text = pagingManager.PageSize.ToString();
			radNumericTextBox2.NumberFormat.DecimalDigits = 0;
			stringBuilder = new StringBuilder();
			stringBuilder.Append("function(sender, args){");
			stringBuilder.Append("if(args.get_keyCode() == 13){sender.set_value(sender.get_textBoxValue()); args.get_domEvent().stopPropagation();args.get_domEvent().preventDefault();");
			stringBuilder.Append("if(sender.get_value() == ''){args.set_cancel(true);return false;}");
			stringBuilder.AppendFormat("var button = $get('{0}');", button2.ClientID);
			stringBuilder.Append("if (button.click){ button.click(); }else{ eval(button.href); }");
			stringBuilder.Append("}");
			stringBuilder.Append("}");
			radNumericTextBox2.ClientEvents.OnKeyPress = stringBuilder.ToString();
			if (pagingManager.DataSourceCount > 0)
			{
				radNumericTextBox2.MinValue = 1.0;
				radNumericTextBox2.MaxValue = (double)pagingManager.DataSourceCount;
				if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
				{
					if (this.IsTopPager)
					{
						base.OwnerTableView.changePageSizeTextBoxTopClientID = radNumericTextBox2.ClientID;
					}
					else
					{
						base.OwnerTableView.changePageSizeTextBoxClientID = radNumericTextBox2.ClientID;
					}
				}
			}
			else
			{
				radNumericTextBox2.MinValue = 1.0;
				radNumericTextBox2.MaxValue = 2147483647.0;
			}
			if (!base.OwnerTableView.OwnerGrid.EmptySkin())
			{
				radNumericTextBox2.CssClass = "rgPagerTextBox";
			}
			radNumericTextBox2.EnableEmbeddedSkins = base.OwnerTableView.OwnerGrid.EnableEmbeddedSkins;
			button2.Text = this.GetLocalizationString((GridStrings loc) => loc.ChangePageSizeLinkButtonText, "Change");
			button2.CommandName = "Page";
			button2.CommandArgument = "ChangePageSize";
			button2.CausesValidation = false;
			if (isSEOPaging)
			{
				button2.OnClientClick = string.Format("Telerik.Web.UI.Grid.ChangePageSizeButtonClickHandler('{0}', '{1}', '{2}', '{3}'); return false;", new object[]
				{
					this.SEOPagingQueryStringKey(),
					base.OwnerTableView.OwnerGrid._defaultPageSize,
					radNumericTextBox2.ClientID,
					base.OwnerTableView.ClientID
				});
				return;
			}
			if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
			{
				button2.OnClientClick = string.Format("Telerik.Web.UI.Grid.ChangePageSize($find('{0}'), $find('{1}').get_value()); return false;", base.OwnerTableView.ClientID, radNumericTextBox2.ClientID);
				return;
			}
			button2.Attributes["onclick"] = string.Format("var cpsi = document.getElementById(\"{0}\"); if ( cpsi.value > {1} || cpsi.value == {2}){{cpsi.value=Math.min({1},{2});return false;}}", radNumericTextBox2.ClientID, pagingManager.DataSourceCount, pagingManager.PageSize);
		}

		// Token: 0x0600B47C RID: 46204 RVA: 0x0027BED0 File Offset: 0x0027A0D0
		private void slider_ValueChanged(object sender, EventArgs e)
		{
			base.OwnerTableView.OwnerGrid.EditIndexes.Clear();
			RadSlider radSlider = sender as RadSlider;
			GridPagerItem gridPagerItem = radSlider.NamingContainer as GridPagerItem;
			if (gridPagerItem.IsTopPager)
			{
				base.OwnerTableView.topSliderChanged = true;
			}
			GridPageChangedEventArgs gridPageChangedEventArgs;
			if (!base.OwnerTableView.topSliderChanged || gridPagerItem.IsTopPager)
			{
				base.OwnerTableView.CurrentPageIndex = Convert.ToInt32(radSlider.Value, NumberFormatInfo.InvariantInfo);
				gridPageChangedEventArgs = new GridPageChangedEventArgs(this, radSlider, (++radSlider.Value).ToString());
			}
			else
			{
				RadSlider radSlider2 = base.OwnerTableView.GetItems(new GridItemType[]
				{
					GridItemType.Pager
				})[0].FindControl("GridSliderPager") as RadSlider;
				gridPageChangedEventArgs = new GridPageChangedEventArgs(this, radSlider2, (++radSlider2.Value).ToString());
			}
			gridPageChangedEventArgs.ExecuteCommand(radSlider);
			if (!gridPagerItem.IsTopPager)
			{
				base.OwnerTableView.topSliderChanged = false;
			}
		}

		// Token: 0x0600B47D RID: 46205 RVA: 0x0027BFD0 File Offset: 0x0027A1D0
		private string SEOPagingQueryStringKey()
		{
			string text = (!string.IsNullOrEmpty(base.OwnerTableView.PagerStyle.SEOPagingQueryStringKey)) ? base.OwnerTableView.PagerStyle.SEOPagingQueryStringKey : base.OwnerTableView.OwnerGrid.PagerStyle.SEOPagingQueryStringKey;
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			return string.Format("{0}ChangePage", base.OwnerTableView.OwnerGrid.ClientID);
		}

		// Token: 0x0600B47E RID: 46206 RVA: 0x0027C040 File Offset: 0x0027A240
		private bool GetUseRouting()
		{
			return base.OwnerTableView.PagerStyle.UseRouting || base.OwnerTableView.OwnerGrid.PagerStyle.UseRouting;
		}

		// Token: 0x0600B47F RID: 46207 RVA: 0x0027C06C File Offset: 0x0027A26C
		private string GetSEOPageIndexRouteParameterName()
		{
			string seopageIndexRouteParameterName = base.OwnerTableView.PagerStyle.SEOPageIndexRouteParameterName;
			if (string.IsNullOrEmpty(seopageIndexRouteParameterName))
			{
				seopageIndexRouteParameterName = base.OwnerTableView.OwnerGrid.PagerStyle.SEOPageIndexRouteParameterName;
			}
			return seopageIndexRouteParameterName;
		}

		// Token: 0x0600B480 RID: 46208 RVA: 0x0027C0AC File Offset: 0x0027A2AC
		private string GetSEORouteName()
		{
			string seorouteName = base.OwnerTableView.PagerStyle.SEORouteName;
			if (string.IsNullOrEmpty(seorouteName))
			{
				seorouteName = base.OwnerTableView.OwnerGrid.PagerStyle.SEORouteName;
			}
			return seorouteName;
		}

		// Token: 0x0600B481 RID: 46209 RVA: 0x0027C0EC File Offset: 0x0027A2EC
		internal void FixHyperLinkUrl(string text1, HyperLink link)
		{
			if (this.GetUseRouting() && !string.IsNullOrEmpty(this.GetSEOPageIndexRouteParameterName()))
			{
				link.NavigateUrl = this.FixRoutedStringUrl(text1, new int?(base.OwnerTableView.PageSize));
				return;
			}
			if (this.Context != null)
			{
				string url = this.Page.Response.ApplyAppPathModifier(this.Page.Request.RawUrl);
				link.NavigateUrl = this.AppendKeyValuePairToQueryString(url, this.SEOPagingQueryStringKey(), this.GetSeoPageUrlParameter(text1, new int?(base.OwnerTableView.PageSize)));
			}
		}

		// Token: 0x0600B482 RID: 46210 RVA: 0x0027C180 File Offset: 0x0027A380
		[SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)")]
		[SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.EndsWith(System.String)")]
		internal string FixRoutedStringUrl(string pageIdx, int? pageSize)
		{
			string text = string.Empty;
			string text2 = this.Page.Response.ApplyAppPathModifier(this.Page.Request.RawUrl);
			string seopageIndexRouteParameterName = this.GetSEOPageIndexRouteParameterName();
			text = this.BuildRouteUrl(this.GetSEORouteName(), seopageIndexRouteParameterName, this.GetSeoPageUrlParameter(pageIdx, pageSize));
			if (text2.IndexOf("?") > -1 && !text2.EndsWith("?"))
			{
				string text3 = text2.Substring(text2.IndexOf("?"));
				if (text.IndexOf("?") > -1)
				{
					text = text.Remove(text.IndexOf("?"));
					text += this.AppendKeyValuePairToQueryString(text3, seopageIndexRouteParameterName, pageIdx);
				}
				else
				{
					text += text3;
				}
			}
			return text;
		}

		// Token: 0x0600B483 RID: 46211 RVA: 0x0027C23C File Offset: 0x0027A43C
		private string BuildRouteUrl(string routeName, string pageIndexParameterName, string urlParam)
		{
			RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
			foreach (KeyValuePair<string, object> keyValuePair in this.Page.RouteData.Values)
			{
				routeValueDictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
			routeValueDictionary[pageIndexParameterName] = urlParam;
			VirtualPathData virtualPath;
			if (!string.IsNullOrEmpty(routeName))
			{
				virtualPath = RouteTable.Routes.GetVirtualPath(null, routeName, routeValueDictionary);
			}
			else
			{
				virtualPath = RouteTable.Routes.GetVirtualPath(null, routeValueDictionary);
			}
			if (virtualPath == null)
			{
				return string.Empty;
			}
			return virtualPath.VirtualPath;
		}

		// Token: 0x0600B484 RID: 46212 RVA: 0x0027C2EC File Offset: 0x0027A4EC
		private string RemoveKeyValuePairFromQueryString(string url, string keyName)
		{
			int num = url.IndexOf(keyName + "=");
			if (num > -1)
			{
				int num2 = url.IndexOf("&", num);
				if (num2 > -1)
				{
					url = url.Remove(num, num2 - num + 1);
				}
				else
				{
					url = url.Remove(num, url.Length - num);
				}
				if (url.EndsWith("&") || url.EndsWith("?"))
				{
					url = url.Remove(url.Length - 1, 1);
				}
			}
			return url;
		}

		// Token: 0x0600B485 RID: 46213 RVA: 0x0027C370 File Offset: 0x0027A570
		private string AppendKeyValuePairToQueryString(string url, string key, string value)
		{
			string arg = string.Empty;
			url = this.RemoveKeyValuePairFromQueryString(url, key);
			if (url.IndexOf("?") > -1)
			{
				if (!url.EndsWith("?") && !url.EndsWith("&"))
				{
					arg = "&";
				}
			}
			else
			{
				arg = "?";
			}
			return url + string.Format("{0}{1}={2}", arg, key, value);
		}

		// Token: 0x0600B486 RID: 46214 RVA: 0x0027C3D8 File Offset: 0x0027A5D8
		public Control GetNumericPager()
		{
			if (base.OwnerTableView.RenderPagerStyle.Mode != GridPagerMode.NumericPages)
			{
				throw new GridNotSupportedException("This method is supported only for pager mode NumericPages");
			}
			if (base.OwnerTableView.PagerTemplate != null)
			{
				this._generateSpanContainer = true;
				this.InitializePagerItem(this._columns);
			}
			return this.spanContainer;
		}

		// Token: 0x0600B487 RID: 46215 RVA: 0x0027C42C File Offset: 0x0027A62C
		private void SetCellSpacing(Table table)
		{
			int num = (table.CellSpacing == -1) ? 0 : table.CellSpacing;
			if (GridTableViewHelper.IsBrowser("IE") && !GridTableViewHelper.IsBrowserVersionNewer("IE", 7))
			{
				table.Attributes.Add("cellspacing", num.ToString());
				return;
			}
			table.Style.Add("border-spacing", num.ToString());
		}

		// Token: 0x04002F8A RID: 12170
		private bool _isTopPager;

		// Token: 0x04002F8B RID: 12171
		private TableCell _pagerCell;

		// Token: 0x04002F8C RID: 12172
		private Panel spanContainer;

		// Token: 0x04002F8D RID: 12173
		private bool _generateSpanContainer;

		// Token: 0x04002F8E RID: 12174
		private GridColumn[] _columns;

		// Token: 0x04002F8F RID: 12175
		private string onClickFormat = "Telerik.Web.UI.Grid.NavigateToPage('{0}', '{1}'); return false;";

		// Token: 0x04002F90 RID: 12176
		private GridPagerButtonBuilder _builder;
	}
}
