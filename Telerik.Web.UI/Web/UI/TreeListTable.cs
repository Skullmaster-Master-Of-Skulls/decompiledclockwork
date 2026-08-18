using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001283 RID: 4739
	internal class TreeListTable : Table
	{
		// Token: 0x17003FC9 RID: 16329
		// (get) Token: 0x0600C59B RID: 50587 RVA: 0x002C1BB6 File Offset: 0x002BFDB6
		// (set) Token: 0x0600C59C RID: 50588 RVA: 0x002C1BBE File Offset: 0x002BFDBE
		public RadTreeList Owner { get; internal set; }

		// Token: 0x0600C59D RID: 50589 RVA: 0x002C1BC7 File Offset: 0x002BFDC7
		public TreeListTable(RadTreeList owner)
		{
			this.Owner = owner;
			this.headerRows = new List<TableRow>();
			this.bodyRows = new List<TableRow>();
			this.footerRows = new List<TableRow>();
		}

		// Token: 0x17003FCA RID: 16330
		// (get) Token: 0x0600C59E RID: 50590 RVA: 0x002C1BF7 File Offset: 0x002BFDF7
		// (set) Token: 0x0600C59F RID: 50591 RVA: 0x002C1BFF File Offset: 0x002BFDFF
		internal bool RenderBodyWithStaticHeaders { get; set; }

		// Token: 0x17003FCB RID: 16331
		// (get) Token: 0x0600C5A0 RID: 50592 RVA: 0x002C1C08 File Offset: 0x002BFE08
		// (set) Token: 0x0600C5A1 RID: 50593 RVA: 0x002C1C10 File Offset: 0x002BFE10
		internal bool RenderTfootWithStaticHeaders { get; set; }

		// Token: 0x17003FCC RID: 16332
		// (get) Token: 0x0600C5A2 RID: 50594 RVA: 0x002C1C19 File Offset: 0x002BFE19
		// (set) Token: 0x0600C5A3 RID: 50595 RVA: 0x002C1C21 File Offset: 0x002BFE21
		internal bool RenderStaticHeadersOnly { get; set; }

		// Token: 0x17003FCD RID: 16333
		// (get) Token: 0x0600C5A4 RID: 50596 RVA: 0x002C1C2A File Offset: 0x002BFE2A
		// (set) Token: 0x0600C5A5 RID: 50597 RVA: 0x002C1C32 File Offset: 0x002BFE32
		private bool HasRowsInBody { get; set; }

		// Token: 0x0600C5A6 RID: 50598 RVA: 0x002C1C3C File Offset: 0x002BFE3C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
			StringBuilder stringBuilder = new StringBuilder("rtlTable");
			if (this.Owner.ShowTreeLines)
			{
				stringBuilder.Append(" rtlLines");
			}
			if ((this.Owner.GridLines & TreeListGridLines.Horizontal) == TreeListGridLines.Horizontal)
			{
				stringBuilder.Append(" rtlHBorders");
			}
			if ((this.Owner.GridLines & TreeListGridLines.Vertical) == TreeListGridLines.Vertical)
			{
				stringBuilder.Append(" rtlVBorders");
			}
			if ((this.Owner.GridLines & TreeListGridLines.Both) == TreeListGridLines.Both)
			{
				stringBuilder.Append(" rtlHVBorders");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, stringBuilder.ToString());
			if (!string.IsNullOrEmpty(this.Owner.Summary))
			{
				writer.AddAttribute("summary", this.Owner.Summary);
			}
		}

		// Token: 0x0600C5A7 RID: 50599 RVA: 0x002C1D0C File Offset: 0x002BFF0C
		protected virtual bool HasRowSections()
		{
			bool result = false;
			foreach (object obj in this.Rows)
			{
				TableRow tableRow = (TableRow)obj;
				if (tableRow.TableSection != TableRowSection.TableBody)
				{
					result = true;
				}
				if (tableRow.TableSection == TableRowSection.TableBody)
				{
					this.HasRowsInBody = true;
					this.bodyRows.Add(tableRow);
				}
				if (tableRow.TableSection == TableRowSection.TableHeader)
				{
					this.headerRows.Add(tableRow);
				}
				if (tableRow.TableSection == TableRowSection.TableFooter)
				{
					this.footerRows.Add(tableRow);
				}
			}
			return result;
		}

		// Token: 0x0600C5A8 RID: 50600 RVA: 0x002C1DB4 File Offset: 0x002BFFB4
		protected void RenderColGroup(HtmlTextWriter writer)
		{
			writer.Write("<colgroup>\r\n");
			if ((this.RenderStaticHeadersOnly || this.RenderTfootWithStaticHeaders) && this.Owner.RenderMode == RenderMode.Lightweight)
			{
				writer.Write("<col />\r\n");
			}
			else
			{
				for (int i = 0; i < this.Owner.MostNestedIndex + 1; i++)
				{
					if (this.RenderStaticHeadersOnly || this.RenderTfootWithStaticHeaders)
					{
						writer.Write("<col />\r\n");
					}
					else if (!this.Owner.ExpandCollapseColumnWidth.IsEmpty)
					{
						writer.Write(string.Format("<col style=\"width:{0};\"/>\r\n", this.Owner.ExpandCollapseColumnWidth));
					}
					else if (this.Owner.RuntimeSkin == "MetroTouch" || this.Owner.RuntimeSkin == "Glow" || this.Owner.RuntimeSkin == "Silk" || this.Owner.RuntimeSkin == "BlackMetroTouch" || this.Owner.RuntimeSkin == "Bootstrap")
					{
						writer.Write("<col style=\"width:41px;\"/>\r\n");
					}
					else
					{
						writer.Write("<col style=\"width:23px;\"/>\r\n");
					}
				}
				foreach (TreeListColumn treeListColumn in this.Owner.RenderColumns)
				{
					if (treeListColumn.Visible && (treeListColumn.Display || this.Owner.IsDesignMode || !HttpContext.Current.Request.Browser.IsBrowser("IE") || HttpContext.Current.Request.Browser.MajorVersion > 7))
					{
						this.visibleColumnsCount++;
						Unit columnWidth = this.GetColumnWidth(treeListColumn);
						if (columnWidth != Unit.Empty && !this.RenderStaticHeadersOnly && !this.RenderTfootWithStaticHeaders)
						{
							string value = string.Format("<col style=\"width:{0};{1}\"/>\r\n", columnWidth, this.GetColumnDisplay(treeListColumn, false));
							writer.Write(value);
						}
						else
						{
							writer.Write(string.Format("<col {0} />\r\n", this.GetColumnDisplay(treeListColumn, true)));
						}
					}
				}
			}
			writer.Write("</colgroup>\r\n");
		}

		// Token: 0x0600C5A9 RID: 50601 RVA: 0x002C1FF4 File Offset: 0x002C01F4
		private string GetColumnDisplay(TreeListColumn column, bool returnStyleAttribute)
		{
			string result = "";
			if (!this.Owner.IsDesignMode && (!HttpContext.Current.Request.Browser.IsBrowser("IE") || HttpContext.Current.Request.Browser.MajorVersion > 7) && !column.Display)
			{
				if (returnStyleAttribute)
				{
					result = "style=\"display:none;\"";
				}
				else
				{
					result = "display:none;";
				}
			}
			return result;
		}

		// Token: 0x0600C5AA RID: 50602 RVA: 0x002C2060 File Offset: 0x002C0260
		internal Unit GetColumnWidth(TreeListColumn column)
		{
			Unit result = Unit.Empty;
			if (column.HeaderStyle.Width != Unit.Empty)
			{
				result = column.HeaderStyle.Width;
			}
			else if (this.Owner.HeaderStyle.Width != Unit.Empty)
			{
				result = this.Owner.HeaderStyle.Width;
			}
			return result;
		}

		// Token: 0x0600C5AB RID: 50603 RVA: 0x002C20C6 File Offset: 0x002C02C6
		protected void RenderCaption(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.Owner.Caption))
			{
				writer.Write(string.Format("<caption class=\"{0}\">{1}</caption>\r\n", "rtlCaption", this.Owner.Caption));
			}
		}

		// Token: 0x0600C5AC RID: 50604 RVA: 0x002C20FA File Offset: 0x002C02FA
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (!this.Owner.HasStaticHeaders)
			{
				this.RenderOrdinaryTable(writer);
				return;
			}
			if (this.RenderStaticHeadersOnly)
			{
				this.RenderStaticHeaderTable(writer);
				return;
			}
			this.RenderTableWithStaticHeaders(writer);
		}

		// Token: 0x0600C5AD RID: 50605 RVA: 0x002C2128 File Offset: 0x002C0328
		protected override void Render(HtmlTextWriter writer)
		{
			if (!this.Owner.HasStaticHeaders)
			{
				base.Render(writer);
				return;
			}
			if (this.RenderStaticHeadersOnly)
			{
				string arg = string.Empty;
				if (!this.Owner.Width.IsEmpty)
				{
					arg = string.Format("style='width:{0};'", this.Owner.Width);
				}
				writer.WriteLine(" <div id='{0}_rtlStaticHeader' {1}>", this.Owner.ClientID, arg);
				base.Render(writer);
				writer.WriteLine("</div>");
				return;
			}
			if (this.RenderBodyWithStaticHeaders)
			{
				double num = this.Owner.ClientSettings.Scrolling.ScrollHeight.Value;
				if (!this.Height.IsEmpty)
				{
					TreeListHeaderItem treeListHeaderItem = this.Owner.GetItems(new TreeListItemType[]
					{
						TreeListItemType.HeaderItem
					})[0] as TreeListHeaderItem;
					num -= (treeListHeaderItem.Height.IsEmpty ? 0.0 : treeListHeaderItem.Height.Value);
					TreeListItem[] items = this.Owner.GetItems(new TreeListItemType[]
					{
						TreeListItemType.PagerItem
					});
					new List<TreeListPagerItem>();
					foreach (TreeListPagerItem treeListPagerItem in items)
					{
						num -= (treeListPagerItem.Height.IsEmpty ? 0.0 : treeListPagerItem.Height.Value);
					}
				}
				string arg2 = string.Empty;
				if (!this.Owner.Width.IsEmpty)
				{
					arg2 = string.Format(" width:{0};", this.Owner.Width);
				}
				writer.WriteLine("\t<div id='{0}_rtlData' class='rtlDataDiv' style='overflow:auto; height:{1};{2}'>\r\n", this.Owner.ClientID, (num == 0.0) ? "auto" : new Unit(num).ToString(), arg2);
				base.Render(writer);
				writer.WriteLine("</div>");
				return;
			}
			if (this.RenderTfootWithStaticHeaders)
			{
				string arg3 = string.Empty;
				if (!this.Owner.Width.IsEmpty)
				{
					arg3 = string.Format("style='width:{0};'", this.Owner.Width);
				}
				writer.WriteLine(" <div id='{0}_rtlFooter' class='rtlFooter' {1}>", this.Owner.ClientID, arg3);
				base.Render(writer);
				writer.WriteLine("</div>");
				return;
			}
			string empty = string.Empty;
			if (!this.Owner.Width.IsEmpty)
			{
				string.Format("width:{0};", this.Owner.Width);
			}
			writer.WriteLine(" <div id='{0}_rtlHeader' class='rtlHeader'><div class='rtlScroller'>", this.Owner.ClientID);
			base.Render(writer);
			writer.WriteLine("</div></div>");
		}

		// Token: 0x0600C5AE RID: 50606 RVA: 0x002C2410 File Offset: 0x002C0610
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private void RenderStaticHeaderTable(HtmlTextWriter writer)
		{
			TableRowCollection rows = this.Owner.GetStaticTreeListTable().Rows;
			this.RenderCaption(writer);
			this.RenderColGroup(writer);
			if (rows.Count > 0)
			{
				this.RenderEmptyTHead(writer);
				foreach (object obj in rows)
				{
					TableRow tableRow = (TableRow)obj;
					tableRow.RenderControl(writer);
				}
			}
		}

		// Token: 0x0600C5AF RID: 50607 RVA: 0x002C2494 File Offset: 0x002C0694
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private void RenderTableWithStaticHeaders(HtmlTextWriter writer)
		{
			this.RenderCaption(writer);
			this.RenderColGroup(writer);
			if (this.RenderBodyWithStaticHeaders || this.RenderTfootWithStaticHeaders)
			{
				AccessibilityHelper.RenderAccessibilityRow(writer, this.Owner.Caption);
			}
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(new StringWriter());
			HtmlTextWriter htmlTextWriter2 = writer;
			TableRowCollection rows = this.Owner.GetTreeListTable().Rows;
			bool flag = false;
			bool flag2 = false;
			TableRow tableRow = null;
			if (rows.Count > 0)
			{
				if (this.HasRowSections())
				{
					TableRowSection tableRowSection = TableRowSection.TableHeader;
					bool flag3 = true;
					foreach (object obj in rows)
					{
						TableRow tableRow2 = (TableRow)obj;
						if (tableRow2.TableSection < tableRowSection)
						{
							throw new HttpException(string.Format("The table {0} must contain row sections in order of header, body, then footer.", new object[]
							{
								this.ID
							}));
						}
						if (tableRowSection != tableRow2.TableSection)
						{
							htmlTextWriter2.RenderEndTag();
							tableRowSection = tableRow2.TableSection;
							flag3 = true;
							if (tableRowSection == TableRowSection.TableBody)
							{
								htmlTextWriter2 = htmlTextWriter;
							}
							else
							{
								htmlTextWriter2 = writer;
							}
						}
						if (flag3)
						{
							flag3 = false;
							switch (tableRowSection)
							{
							case TableRowSection.TableHeader:
								htmlTextWriter2.RenderBeginTag(HtmlTextWriterTag.Thead);
								break;
							case TableRowSection.TableBody:
								htmlTextWriter2.RenderBeginTag(HtmlTextWriterTag.Tbody);
								break;
							case TableRowSection.TableFooter:
								htmlTextWriter2.RenderBeginTag(HtmlTextWriterTag.Tfoot);
								break;
							}
						}
						if (tableRow2 is TreeListCommandItem && (this.Owner.CommandItemDisplay == TreeListCommandItemDisplay.TopAndBottom || this.Owner.CommandItemDisplay == TreeListCommandItemDisplay.Top))
						{
							if (tableRow == null)
							{
								tableRow = tableRow2;
								tableRow.RenderControl(writer);
							}
						}
						else
						{
							if ((!this.RenderBodyWithStaticHeaders && tableRow2 is TreeListDataItem) || (!this.RenderBodyWithStaticHeaders && tableRow2 is TreeListEditFormItem) || (!this.RenderBodyWithStaticHeaders && tableRow2 is TreeListDataInsertItem) || (this.RenderBodyWithStaticHeaders && tableRow2 is TreeListHeaderItem))
							{
								return;
							}
							if (!this.RenderBodyWithStaticHeaders)
							{
								if (tableRow2 is TreeListPagerItem && this.Owner.PagerStyle.Position != TreeListPagerPosition.Bottom)
								{
									if (!flag)
									{
										tableRow2.RenderControl(htmlTextWriter2);
										foreach (object obj2 in this.Rows)
										{
											TableRow tableRow3 = (TableRow)obj2;
											if (tableRow3 is TreeListHeaderItem)
											{
												tableRow3.RenderControl(htmlTextWriter2);
											}
										}
									}
									flag = true;
								}
								else if (!(tableRow2 is TreeListNoRecordsItem) && !(tableRow2 is TreeListCommandItem) && (!(tableRow2 is TreeListPagerItem) || this.Owner.PagerStyle.Position != TreeListPagerPosition.Bottom))
								{
									tableRow2.RenderControl(htmlTextWriter2);
								}
							}
							else
							{
								tableRow2.RenderControl(htmlTextWriter2);
							}
							if (!this.RenderBodyWithStaticHeaders && !this.RenderTfootWithStaticHeaders && !flag2)
							{
								htmlTextWriter2.RenderEndTag();
								htmlTextWriter2.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
								htmlTextWriter2.RenderBeginTag(HtmlTextWriterTag.Tbody);
								htmlTextWriter2.RenderBeginTag(HtmlTextWriterTag.Tr);
								string value;
								if (!base.DesignMode && HttpContext.Current.Request.Browser.IsBrowser("IE") && HttpContext.Current.Request.Browser.MajorVersion <= 7)
								{
									value = this.visibleColumnsCount.ToString();
								}
								else
								{
									value = rows[0].Cells.Count.ToString();
								}
								htmlTextWriter2.AddAttribute(HtmlTextWriterAttribute.Colspan, value);
								htmlTextWriter2.RenderBeginTag(HtmlTextWriterTag.Td);
								htmlTextWriter2.Write("&nbsp;");
								htmlTextWriter2.RenderEndTag();
								htmlTextWriter2.RenderEndTag();
								flag2 = true;
							}
						}
					}
					htmlTextWriter2.RenderEndTag();
					htmlTextWriter2 = writer;
					htmlTextWriter2.Write(this.PrepareTableBody(htmlTextWriter.InnerWriter.ToString()));
					return;
				}
				if (this.RenderBodyWithStaticHeaders || this.RenderTfootWithStaticHeaders)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
				}
				bool flag4 = false;
				foreach (object obj3 in rows)
				{
					TableRow tableRow4 = (TableRow)obj3;
					if ((!this.RenderBodyWithStaticHeaders || !(tableRow4 is TreeListHeaderItem)) && (!this.RenderBodyWithStaticHeaders || !(tableRow4 is TreeListPagerItem)) && (!this.RenderTfootWithStaticHeaders || !(tableRow4 is TreeListNoRecordsItem)) && (!this.RenderTfootWithStaticHeaders || !(tableRow4 is TreeListHeaderItem)) && (!this.RenderTfootWithStaticHeaders || !(tableRow4 is TreeListDataItem)) && (!this.RenderTfootWithStaticHeaders || !(tableRow4 is TreeListEditFormItem)) && (!this.RenderTfootWithStaticHeaders || !(tableRow4 is TreeListFooterItem)) && (!this.RenderTfootWithStaticHeaders || !(tableRow4 is TreeListDataInsertItem)) && (!this.RenderTfootWithStaticHeaders || !(tableRow4 is TreeListDetailTemplateItem)))
					{
						if (tableRow4 is TreeListCommandItem && tableRow == null)
						{
							tableRow = tableRow4;
						}
						if ((!flag4 || !(tableRow4 is TreeListPagerItem)) && (!(tableRow4 is TreeListPagerItem) || this.Owner.PagerStyle.Position != TreeListPagerPosition.Top) && (!(tableRow4 is TreeListCommandItem) || (tableRow4 is TreeListCommandItem && !this.RenderBodyWithStaticHeaders)))
						{
							tableRow4.RenderControl(writer);
						}
						if (tableRow4 is TreeListPagerItem)
						{
							flag4 = true;
						}
					}
				}
				if (this.RenderTfootWithStaticHeaders && (this.Owner.CommandItemDisplay == TreeListCommandItemDisplay.TopAndBottom || this.Owner.CommandItemDisplay == TreeListCommandItemDisplay.Bottom) && !flag4 && tableRow == null)
				{
					tableRow.RenderControl(writer);
				}
				if (this.RenderBodyWithStaticHeaders || this.RenderTfootWithStaticHeaders)
				{
					writer.RenderEndTag();
				}
			}
		}

		// Token: 0x0600C5B0 RID: 50608 RVA: 0x002C2A24 File Offset: 0x002C0C24
		private void RenderOrdinaryTable(HtmlTextWriter writer)
		{
			this.RenderCaption(writer);
			this.RenderColGroup(writer);
			TableRowCollection rows = this.Rows;
			if (rows.Count > 0)
			{
				if (this.HasRowSections())
				{
					List<TableRow> list = new List<TableRow>(this.Rows.Count);
					list.AddRange(this.headerRows);
					list.AddRange(this.footerRows);
					list.AddRange(this.bodyRows);
					TableRowSection tableRowSection = TableRowSection.TableHeader;
					bool flag = true;
					foreach (TableRow tableRow in list)
					{
						if (tableRowSection != tableRow.TableSection)
						{
							writer.RenderEndTag();
							tableRowSection = tableRow.TableSection;
							flag = true;
						}
						if (flag)
						{
							flag = false;
							switch (tableRowSection)
							{
							case TableRowSection.TableHeader:
								writer.RenderBeginTag(HtmlTextWriterTag.Thead);
								break;
							case TableRowSection.TableBody:
								writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
								break;
							case TableRowSection.TableFooter:
								writer.RenderBeginTag(HtmlTextWriterTag.Tfoot);
								break;
							}
						}
						tableRow.RenderControl(writer);
					}
					writer.RenderEndTag();
					this.RenderEmptyTBody(writer);
					return;
				}
				foreach (object obj in rows)
				{
					TableRow tableRow2 = (TableRow)obj;
					tableRow2.RenderControl(writer);
				}
			}
		}

		// Token: 0x0600C5B1 RID: 50609 RVA: 0x002C2B90 File Offset: 0x002C0D90
		private void RenderEmptyTHead(HtmlTextWriter writer)
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Thead);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.RenderBeginTag(HtmlTextWriterTag.Th);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600C5B2 RID: 50610 RVA: 0x002C2BCC File Offset: 0x002C0DCC
		private void RenderEmptyTBody(HtmlTextWriter writer)
		{
			if (!this.HasRowsInBody)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600C5B3 RID: 50611 RVA: 0x002C2C18 File Offset: 0x002C0E18
		protected string PrepareTableBody(string markup)
		{
			if (string.IsNullOrEmpty(markup))
			{
				HtmlTextWriter htmlTextWriter = new HtmlTextWriter(new StringWriter());
				htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Tbody);
				htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
				htmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
				htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Td);
				htmlTextWriter.RenderEndTag();
				htmlTextWriter.RenderEndTag();
				htmlTextWriter.RenderEndTag();
				markup = htmlTextWriter.InnerWriter.ToString();
			}
			return markup;
		}

		// Token: 0x04003438 RID: 13368
		private List<TableRow> headerRows;

		// Token: 0x04003439 RID: 13369
		private List<TableRow> bodyRows;

		// Token: 0x0400343A RID: 13370
		private List<TableRow> footerRows;

		// Token: 0x0400343B RID: 13371
		private int visibleColumnsCount;
	}
}
