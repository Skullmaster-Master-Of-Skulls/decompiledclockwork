using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200117B RID: 4475
	internal class GridTable : Table
	{
		// Token: 0x0600B671 RID: 46705 RVA: 0x0028234E File Offset: 0x0028054E
		public GridTable(GridTableView ownerTableView)
		{
			this._ownerTableView = ownerTableView;
		}

		// Token: 0x0600B672 RID: 46706 RVA: 0x00282370 File Offset: 0x00280570
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.Style["table-layout"] = ((this._ownerTableView.TableLayout == GridTableLayout.Fixed || (this._ownerTableView.EditMode == GridEditMode.Batch && !this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.UseStaticHeaders)) ? "fixed" : "auto");
			GridColumn[] renderColumns = this._ownerTableView.RenderColumns;
			if (this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.AllowScroll && this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.UseStaticHeaders)
			{
				int i = 0;
				int num = renderColumns.Length;
				while (i < num)
				{
					if (renderColumns[i].Visible && renderColumns[i].Display && renderColumns[i].ColumnType != "GridExpandColumn" && renderColumns[i].ColumnType != "GridRowIndicatorColumn" && renderColumns[i].ColumnType != "GridGroupSplitterColumn")
					{
						Unit columnWidth = this.GetColumnWidth(i);
						if (columnWidth != Unit.Empty)
						{
							base.Style["table-layout"] = "fixed";
							break;
						}
					}
					i++;
				}
			}
			if ((this._ownerTableView.OwnerGrid.ClientSettings.Resizing.ClipCellContentOnResize && (this._ownerTableView.OwnerGrid.ClientSettings.Resizing.AllowColumnResize || this._ownerTableView.OwnerGrid.ClientSettings.Resizing.AllowRowResize)) || (this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.AllowScroll && this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.UseStaticHeaders))
			{
				if (this._ownerTableView.OwnerGrid.ClientSettings.Resizing.ClipCellContentOnResize && (this._ownerTableView.OwnerGrid.ClientSettings.Resizing.AllowColumnResize || this._ownerTableView.OwnerGrid.ClientSettings.Resizing.AllowRowResize))
				{
					base.Style["table-layout"] = "fixed";
				}
				if (this._ownerTableView.OwnerGrid.ResolvedRenderMode != Telerik.Web.UI.RenderMode.Mobile)
				{
					base.Style["overflow"] = "hidden";
				}
				if (this.Context != null && GridTableViewHelper.IsBrowser("IE"))
				{
					base.Style["text-overflow"] = "ellipsis";
				}
			}
			if ((base.Style["table-layout"] == "fixed" || (this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.AllowScroll && this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.UseStaticHeaders)) && this.RenderMode < 2)
			{
				this.CssClass += " rgClipCells";
			}
			base.Style["empty-cells"] = "show";
			string id = this.ID;
			this.ID = null;
			base.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.Parent.ClientID + this.IDSuffix);
			this.ID = id;
			this.IDSuffix = string.Empty;
			if (this._ownerTableView.Dir != GridTableTextDirection.LTR)
			{
				writer.AddAttribute("dir", this._ownerTableView.Dir.ToString().ToLower());
			}
			if (this._ownerTableView.Frame != GridTableFrame.Border)
			{
				writer.AddAttribute("frame", this._ownerTableView.Frame.ToString().ToLower());
			}
		}

		// Token: 0x0600B673 RID: 46707 RVA: 0x00282730 File Offset: 0x00280930
		protected override void Render(HtmlTextWriter writer)
		{
			if (!this._ownerTableView.OwnerGrid.EmptySkin())
			{
				if (!this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.UseStaticHeaders && !this._ownerTableView.IsClone && this._ownerTableView.Width == Unit.Empty)
				{
					this.Width = Unit.Percentage(100.0);
				}
				if (!this._ownerTableView.OwnerGrid.IsDesignMode && this.Width == Unit.Empty && this.Page != null && (!GridTableViewHelper.IsBrowser("IE") || GridTableViewHelper.IsBrowserVersionNewer("IE", 6)))
				{
					if (GridTableViewHelper.IsBrowser("IE", 7))
					{
						if (this._ownerTableView.HasDetailTables || this._ownerTableView.NamingContainer is GridNestedViewItem)
						{
							this.Width = Unit.Percentage(100.0);
						}
					}
					else
					{
						this.Width = Unit.Percentage(100.0);
					}
				}
			}
			else if (this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.AllowScroll && this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.UseStaticHeaders && !this._ownerTableView.IsClone && this._ownerTableView.Width == Unit.Empty)
			{
				this.Width = Unit.Percentage(100.0);
			}
			if (this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.AllowScroll && !this._ownerTableView.IsClone)
			{
				if (this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.UseStaticHeaders)
				{
					if ((this._ownerTableView.AllowPaging && this._ownerTableView.RenderPagerStyle.IsPagerOnTop) || this._ownerTableView.CommandItemDisplay == GridCommandItemDisplay.Top || this._ownerTableView.CommandItemDisplay == GridCommandItemDisplay.TopAndBottom)
					{
						Unit width = this.Width;
						this.IDSuffix = "_TopPager";
						this.Width = Unit.Percentage(100.0);
						this.RenderMode = 1;
						this.RenderCellSpacing(writer);
						base.Render(writer);
						this.Width = width;
					}
					this.RenderMode = 1;
					this.IDSuffix = "_Header";
					string arg = "";
					if (!this._ownerTableView.OwnerGrid.EmptySkin())
					{
						arg = "class=\"rgHeaderDiv\"";
					}
					writer.WriteLine("<div class=\"rgHeaderWrapper\"><div id=\"{0}_GridHeader\" {1} style=\"overflow:hidden;{2}\">\r\n", this._ownerTableView.OwnerGrid.ClientID, arg, this.GetHeaderDivWidth());
					bool flag = false;
					if (this.Width == Unit.Empty)
					{
						flag = true;
					}
					this.RenderCellSpacing(writer);
					base.Render(writer);
					if (flag)
					{
						this.Width = Unit.Empty;
					}
					writer.WriteLine("\r\n </div></div>");
					this.RenderMode = 2;
				}
				string text = (!this._ownerTableView.OwnerGrid.EmptySkin()) ? "rgDataDiv" : "";
				if (this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.EnableVirtualScrollPaging && !this._ownerTableView.OwnerGrid.EmptySkin())
				{
					text = string.Format("{0} rgVScroll", text);
				}
				if (!string.IsNullOrEmpty(text))
				{
					text = string.Format("class=\"{0}\"", text);
				}
				string text2 = "auto";
				string text3 = "auto";
				string text4 = "100%";
				if (this._ownerTableView.OwnerGrid.ClientSettings.Virtualization.ShouldCreateCustomScrollbar)
				{
					text2 = "auto";
					text3 = "scroll";
					if (this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.UseStaticHeaders)
					{
						writer.WriteLine("<div class=\"rgDataWrap\" style=\"position: relative;\">");
					}
				}
				if (this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.EnableNextPrevFrozenColumns)
				{
					text2 = "hidden";
				}
				if (this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.ScrollHeight.Type != UnitType.Percentage)
				{
					writer.WriteLine("\t<div id=\"{0}_GridData\" {2} style=\"{4}overflow-x:{3};overflow-y:{6};width:{5};height:{1};\">\r\n", new object[]
					{
						this._ownerTableView.OwnerGrid.ClientID,
						this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.ScrollHeight,
						text,
						text2,
						this.GetPosition(),
						text4,
						text3
					});
				}
				else
				{
					writer.WriteLine("\t<div id=\"{0}_GridData\" {2} style=\"{4}overflow-x:{3};overflow-y:{6};width:{5};height:{1};\">\r\n", new object[]
					{
						this._ownerTableView.OwnerGrid.ClientID,
						"300px",
						text,
						text2,
						this.GetPosition(),
						text4,
						text3
					});
				}
			}
			this.IDSuffix = string.Empty;
			this.RenderCellSpacing(writer);
			base.Render(writer);
			if (this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.AllowScroll && !this._ownerTableView.IsClone)
			{
				if (this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.UseStaticHeaders)
				{
					writer.WriteLine("\t</div>");
					if (this._ownerTableView.OwnerGrid.ClientSettings.Virtualization.ShouldCreateCustomScrollbar)
					{
						string arg2 = this._ownerTableView.ClientID + "_VirtualScroll";
						if (this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.ScrollHeight.Type != UnitType.Percentage)
						{
							writer.WriteLine(string.Format("<div id='{0}' style='overflow-y:scroll;position:absolute;right: 0;top:0;width:18px;height:{1};'><div style='height:250000px;width:1px;' ></div></div>", arg2, this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.ScrollHeight));
						}
						else
						{
							writer.WriteLine(string.Format("<div id='{0}' style='overflow-y:scroll;position:absolute;right:0;top:0;width:18px;height:300px;'><div style='height:250000px;width:1px;' ></div></div>", arg2));
						}
						writer.WriteLine("</div>");
					}
					if ((this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.FrozenColumnsCount > 0 || this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.EnableColumnClientFreeze) && !this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.EnableNextPrevFrozenColumns)
					{
						writer.WriteLine(string.Format("<div id=\"{0}_Frozen\" style=\"width:100%;overflow:auto;\"><div id=\"{0}_FrozenScroll\" style=\"height:100%;\"></div></div>", this._ownerTableView.OwnerGrid.ClientID));
					}
					if (this._ownerTableView.ShowFooter || this._ownerTableView.CommandItemDisplay == GridCommandItemDisplay.Bottom || this._ownerTableView.CommandItemDisplay == GridCommandItemDisplay.TopAndBottom || this._ownerTableView.OwnerGrid.ShowStatusBar || (this._ownerTableView.AllowPaging && this._ownerTableView.PagerStyle.IsPagerOnBottom) || (this._ownerTableView.IsItemInserted && this._ownerTableView.InsertItemDisplay == GridInsertItemDisplay.Bottom))
					{
						if (this._ownerTableView.ShowFooter)
						{
							string text5 = "";
							if (!this._ownerTableView.OwnerGrid.EmptySkin())
							{
								text5 = "class=\"rgFooterDiv\"";
							}
							writer.WriteLine("<div class=\"rgFooterWrapper\"><div id=\"{0}_GridFooter\" {1} style=\"{2}overflow:hidden;{3}\">\r\n", new object[]
							{
								this._ownerTableView.OwnerGrid.ClientID,
								text5,
								"padding-right:16px;",
								this.GetHeaderDivWidth()
							});
							this.IDSuffix = "_Footer";
							this.RenderMode = 3;
							this.RenderCellSpacing(writer);
							base.Render(writer);
							writer.WriteLine("</div></div>");
						}
						if (((this._ownerTableView.AllowPaging && this._ownerTableView.PagerStyle.IsPagerOnBottom) || this._ownerTableView.CommandItemDisplay == GridCommandItemDisplay.Bottom || this._ownerTableView.CommandItemDisplay == GridCommandItemDisplay.TopAndBottom || this._ownerTableView.OwnerGrid.ShowStatusBar || (this._ownerTableView.IsItemInserted && this._ownerTableView.InsertItemDisplay == GridInsertItemDisplay.Bottom)) && this._ownerTableView.PagerStyle.Visible && this._ownerTableView.OwnerGrid.PagerStyle.Visible)
						{
							this.IDSuffix = "_Pager";
							this.Width = Unit.Percentage(100.0);
							this.RenderMode = 4;
							this.RenderCellSpacing(writer);
							base.Render(writer);
						}
					}
				}
				else
				{
					writer.WriteLine("\t</div>");
				}
				if (this._ownerTableView.OwnerGrid.ClientSettings.Virtualization.ShouldCreateCustomScrollbar && !this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.UseStaticHeaders)
				{
					string arg3 = this._ownerTableView.ClientID + "_VirtualScroll";
					if (this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.ScrollHeight.Type != UnitType.Percentage)
					{
						writer.WriteLine(string.Format("<div id='{0}' style='overflow-y:scroll;position:absolute;right:0;top:0;width:18px;height:{1};'><div style='height:250000px;width:1px;' ></div></div>", arg3, this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.ScrollHeight));
						return;
					}
					writer.WriteLine(string.Format("<div id='{0}' style='overflow-y:scroll;position:absolute;right:0;top:0;width:18px;height:300px;'><div style='height:250000px;width:1px;' ></div></div>", arg3));
				}
			}
		}

		// Token: 0x0600B674 RID: 46708 RVA: 0x00283040 File Offset: 0x00281240
		private void RenderCellSpacing(HtmlTextWriter writer)
		{
			if (this.CellSpacing == -1 && GridTableViewHelper.IsBrowser("IE") && !GridTableViewHelper.IsBrowserVersionNewer("IE", 7))
			{
				writer.AddAttribute("cellspacing", "0");
			}
		}

		// Token: 0x0600B675 RID: 46709 RVA: 0x00283074 File Offset: 0x00281274
		private string GetHeaderDivWidth()
		{
			return "";
		}

		// Token: 0x0600B676 RID: 46710 RVA: 0x0028307B File Offset: 0x0028127B
		private bool RenderHeaders()
		{
			return this.RenderMode == 0 || this.RenderMode == 1;
		}

		// Token: 0x0600B677 RID: 46711 RVA: 0x00283090 File Offset: 0x00281290
		private bool RenderFooters()
		{
			return this.RenderMode == 0 || this.RenderMode == 3 || this.RenderMode == 4;
		}

		// Token: 0x0600B678 RID: 46712 RVA: 0x002830AE File Offset: 0x002812AE
		private bool RenderRows()
		{
			return this.RenderMode == 0 || this.RenderMode == 2;
		}

		// Token: 0x0600B679 RID: 46713 RVA: 0x002830C3 File Offset: 0x002812C3
		private bool RenderPager()
		{
			return this.RenderMode == 0 || this.RenderMode == 4;
		}

		// Token: 0x0600B67A RID: 46714 RVA: 0x002830D8 File Offset: 0x002812D8
		private bool RenderCollGroups()
		{
			return this.RenderMode != 4;
		}

		// Token: 0x0600B67B RID: 46715 RVA: 0x002830E8 File Offset: 0x002812E8
		internal Unit GetColumnWidth(int index)
		{
			Unit result = Unit.Empty;
			if (this._ownerTableView.RenderColumns[index].HeaderStyle.Width != Unit.Empty)
			{
				result = this._ownerTableView.RenderColumns[index].HeaderStyle.Width;
			}
			else if (this._ownerTableView.HeaderStyle.Width != Unit.Empty)
			{
				result = this._ownerTableView.HeaderStyle.Width;
			}
			else if (this._ownerTableView.OwnerGrid.HeaderStyle.Width != Unit.Empty)
			{
				result = this._ownerTableView.OwnerGrid.HeaderStyle.Width;
			}
			return result;
		}

		// Token: 0x0600B67C RID: 46716 RVA: 0x002831A0 File Offset: 0x002813A0
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected override void RenderContents(HtmlTextWriter writer)
		{
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				int num = 0;
				if (control is GridTFoot)
				{
					if (this.Exporting)
					{
						if (this.RenderRows())
						{
							writer.WriteLine("<tbody>");
						}
					}
					else
					{
						if (this.RenderMode != 0)
						{
							if (this.RenderFooters())
							{
								writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
								foreach (object obj2 in control.Controls)
								{
									TableRow tableRow = (TableRow)obj2;
									if (control.Visible)
									{
										if (this.RenderPager() && (tableRow is GridPagerItem || tableRow is GridCommandItem || tableRow is IGridInsertItem))
										{
											tableRow.RenderControl(writer);
										}
										if (!this.RenderPager() && (!(tableRow is GridPagerItem) || !(tableRow is GridCommandItem) || !(tableRow is IGridInsertItem)))
										{
											tableRow.RenderControl(writer);
											break;
										}
									}
								}
								writer.RenderEndTag();
								writer.WriteLine();
							}
						}
						else
						{
							control.RenderControl(writer);
						}
						if (this.RenderRows())
						{
							writer.WriteLine("<tbody>");
						}
					}
				}
				else if (control is GridTHead)
				{
					if ((this._ownerTableView.RenderPagerStyle.Position == GridPagerPosition.Bottom && (this._ownerTableView.CommandItemDisplay == GridCommandItemDisplay.Bottom || this._ownerTableView.CommandItemDisplay == GridCommandItemDisplay.None)) || (!this._ownerTableView.AllowPaging && this._ownerTableView.RenderPagerStyle.IsPagerOnTop && this._ownerTableView.CommandItemDisplay != GridCommandItemDisplay.Top && this._ownerTableView.CommandItemDisplay != GridCommandItemDisplay.TopAndBottom))
					{
						(control as GridTHead).isStatic = false;
					}
					if (!string.IsNullOrEmpty(this._ownerTableView.Caption))
					{
						string text = this.captionRendered ? "style='display: none;'" : string.Empty;
						if (this._ownerTableView.OwnerGrid.EmptySkin())
						{
							writer.Write(string.Format("\t\t<caption {1}>{0}</caption>\r\n", this._ownerTableView.Caption, text));
						}
						else
						{
							writer.Write(string.Format("\t\t<caption class=\"{1}\" {2}>{0}</caption>\r\n", this._ownerTableView.Caption, "rgCaption", text));
						}
						this.captionRendered = true;
					}
					if (!this.Exporting || this.ShouldRenderColgroup)
					{
						if ((this.RenderCollGroups() || (this._ownerTableView.IsItemInserted && this._ownerTableView.InsertItemDisplay == GridInsertItemDisplay.Bottom)) && !(control as GridTHead).isStatic)
						{
							int num2 = this._ownerTableView.RenderColumns.Length;
							int num3 = 0;
							if (this.Exporting && this._ownerTableView.OwnerGrid.ExportSettings.HideStructureColumns)
							{
								num3++;
							}
							writer.Write("<colgroup>\r\n");
							for (int i = num3; i < num2; i++)
							{
								if (this._ownerTableView.RenderColumns[i].Visible)
								{
									Unit left = this.GetColumnWidth(i);
									if (!this.RenderHeaders() && this._ownerTableView.EnableNoRecordsTemplate && this._ownerTableView.NoRecordsTemplate != null && this._ownerTableView.OwnerGrid.ClientSettings.Scrolling.UseStaticHeaders && this._ownerTableView.DetailTables.Count > 0 && this._ownerTableView.Items.Count == 0 && i == 2)
									{
										left = Unit.Empty;
									}
									if (left != Unit.Empty)
									{
										writer.Write(string.Format("\t\t<col style=\"width:{0}{1}\" />\r\n", left.ToString(CultureInfo.InvariantCulture), this.GetColumnDisplay(this._ownerTableView.RenderColumns[i], false)));
									}
									else
									{
										writer.Write(string.Format("\t\t<col {0} />\r\n", this.GetColumnDisplay(this._ownerTableView.RenderColumns[i], true)));
									}
									num++;
								}
							}
							writer.Write("\t</colgroup>\r\n");
						}
						if (!this.RenderHeaders())
						{
							AccessibilityHelper.RenderAccessibilityRow(writer, this._ownerTableView.Caption);
						}
					}
					if (this.RenderHeaders())
					{
						control.RenderControl(writer);
						if (!this.RenderRows())
						{
							writer.Write("<tbody style=\"display:none;\"><tr>");
							if (num > 0)
							{
								writer.Write("<td colspan=\"{0}\"></td>", num);
							}
							else
							{
								writer.Write("<td></td>");
							}
							writer.Write("</tr></tbody>");
						}
					}
				}
				else if (this.RenderRows())
				{
					control.RenderControl(writer);
				}
			}
			if (this.RenderRows())
			{
				writer.WriteLine();
				writer.WriteLine("</tbody>");
				if (this.Exporting)
				{
					foreach (object obj3 in this.Controls)
					{
						Control control2 = (Control)obj3;
						if (control2 is GridTFoot)
						{
							foreach (object obj4 in control2.Controls)
							{
								Control control3 = (Control)obj4;
								if (control3 is GridFooterItem)
								{
									control2.RenderControl(writer);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600B67D RID: 46717 RVA: 0x00283744 File Offset: 0x00281944
		private string GetColumnDisplay(GridColumn gridColumn, bool returnStyleAttribute)
		{
			string result = "";
			if (!this._ownerTableView.OwnerGrid.IsDesignMode && !gridColumn.Display)
			{
				if (returnStyleAttribute)
				{
					result = "style=\"display:none;\"";
				}
				else
				{
					result = ";display:none;";
				}
			}
			return result;
		}

		// Token: 0x0600B67E RID: 46718 RVA: 0x00283784 File Offset: 0x00281984
		internal bool IsGrouped(GridTableView view)
		{
			if (!string.IsNullOrEmpty(view.OwnerGrid.ClientDataSourceID))
			{
				return false;
			}
			if (view.GroupByExpressions.Count > 0)
			{
				return true;
			}
			if (view.HasDetailTables)
			{
				foreach (GridTableView view2 in view.DetailTables)
				{
					if (this.IsGrouped(view2))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600B67F RID: 46719 RVA: 0x00283810 File Offset: 0x00281A10
		private string GetPosition()
		{
			string result = "";
			if (this.IsGrouped(this._ownerTableView) && !this._ownerTableView.IsClone)
			{
				result = "position:relative;";
			}
			return result;
		}

		// Token: 0x04003013 RID: 12307
		private GridTableView _ownerTableView;

		// Token: 0x04003014 RID: 12308
		private string IDSuffix = string.Empty;

		// Token: 0x04003015 RID: 12309
		internal bool Exporting;

		// Token: 0x04003016 RID: 12310
		internal bool ShouldRenderColgroup = true;

		// Token: 0x04003017 RID: 12311
		internal int RenderMode;

		// Token: 0x04003018 RID: 12312
		internal bool captionRendered;
	}
}
