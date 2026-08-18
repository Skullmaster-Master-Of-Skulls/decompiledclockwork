using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200088D RID: 2189
	internal static class AccessibilityHelper
	{
		// Token: 0x060050F5 RID: 20725 RVA: 0x000FC240 File Offset: 0x000FA440
		public static void AddAccessibilityRow(Table table, string text)
		{
			TableHeaderRow tableHeaderRow = new TableHeaderRow();
			table.Rows.Add(tableHeaderRow);
			tableHeaderRow.TableSection = TableRowSection.TableHeader;
			tableHeaderRow.Style.Add(HtmlTextWriterStyle.Display, "none");
			TableHeaderCell tableHeaderCell = new TableHeaderCell();
			tableHeaderCell.Text = text;
			tableHeaderRow.Cells.Add(tableHeaderCell);
			tableHeaderCell.Attributes.Add("scope", "col");
		}

		// Token: 0x060050F6 RID: 20726 RVA: 0x000FC2A8 File Offset: 0x000FA4A8
		public static void RenderAccessibilityRow(HtmlTextWriter writer, string text)
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Thead);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col");
			writer.RenderBeginTag(HtmlTextWriterTag.Th);
			writer.Write(text);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060050F7 RID: 20727 RVA: 0x000FC300 File Offset: 0x000FA500
		public static void AddSummary(Table table, string summary)
		{
			if (!string.IsNullOrEmpty(summary))
			{
				table.Attributes.Add("summary", summary);
			}
		}

		// Token: 0x060050F8 RID: 20728 RVA: 0x000FC31B File Offset: 0x000FA51B
		public static void AddCaption(Table table, string caption)
		{
			if (!string.IsNullOrEmpty(caption))
			{
				table.Caption = caption;
			}
		}

		// Token: 0x060050F9 RID: 20729 RVA: 0x000FC32C File Offset: 0x000FA52C
		public static void AddToolTip(WebControl control, string toolTip)
		{
			if (!string.IsNullOrEmpty(toolTip))
			{
				CheckBox checkBox = control as CheckBox;
				if (checkBox == null)
				{
					control.ToolTip = toolTip;
					return;
				}
				checkBox.InputAttributes.Add("title", toolTip);
			}
		}

		// Token: 0x060050FA RID: 20730 RVA: 0x000FC364 File Offset: 0x000FA564
		public static void AddTitle(WebControl control, string title)
		{
			if (!string.IsNullOrEmpty(title))
			{
				control.Attributes.Add("title", title);
			}
		}

		// Token: 0x060050FB RID: 20731 RVA: 0x000FC380 File Offset: 0x000FA580
		public static void RenderCaption(HtmlTextWriter writer, RadCompositeDataBoundControl owner, string caption, string className, bool displayed)
		{
			if (!string.IsNullOrEmpty(caption))
			{
				if (string.IsNullOrEmpty(owner.RuntimeSkin))
				{
					if (displayed)
					{
						writer.Write(string.Format("\t\t<caption>{0}</caption>\r\n", caption));
						return;
					}
					writer.Write(string.Format("\t\t<caption style='display:none;'>{0}</caption>\r\n", caption));
					return;
				}
				else
				{
					if (displayed)
					{
						writer.Write(string.Format("\t\t<caption class=\"{1}\">{0}</caption>\r\n", caption, className));
						return;
					}
					writer.Write(string.Format("\t\t<caption style='display:none;' class=\"{1}\">{0}</caption>\r\n", caption, className));
				}
			}
		}
	}
}
