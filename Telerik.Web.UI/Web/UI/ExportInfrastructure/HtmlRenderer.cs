using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Export;

namespace Telerik.Web.UI.ExportInfrastructure
{
	// Token: 0x02000A52 RID: 2642
	internal class HtmlRenderer
	{
		// Token: 0x06006663 RID: 26211 RVA: 0x0017F673 File Offset: 0x0017D873
		public HtmlRenderer(ExportStructure structure)
		{
			this._structure = structure;
		}

		// Token: 0x06006664 RID: 26212 RVA: 0x0017F684 File Offset: 0x0017D884
		public StringBuilder Render()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Table tbl in this._structure.Tables)
			{
				stringBuilder.Append(this.RenderTable(tbl));
			}
			return stringBuilder;
		}

		// Token: 0x06006665 RID: 26213 RVA: 0x0017F6E4 File Offset: 0x0017D8E4
		private StringBuilder RenderTable(Table tbl)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<Point> list = new List<Point>();
			if (tbl.Cells.Count == 0)
			{
				return new StringBuilder("");
			}
			int num = 0;
			int num2 = 0;
			foreach (Cell cell in tbl.Cells)
			{
				int num3 = cell.Rowspan - 1;
				int num4 = cell.Colspan - 1;
				if (cell.RowIndex + num3 > num2)
				{
					num2 = cell.RowIndex + num3;
				}
				if (cell.ColIndex + num4 > num)
				{
					num = cell.ColIndex + num4;
				}
				list.AddRange(Utils.GetCellRange(cell.Index, new Point(cell.Index.X + num4, cell.Index.Y + num3)));
			}
			stringBuilder.Append("<table cellspacing=5 class='testClass'>");
			for (int i = 1; i <= num2; i++)
			{
				stringBuilder.Append("<tr>");
				for (int j = 1; j <= num; j++)
				{
					Cell cell2 = tbl.Cells.GetCell(j, i);
					if (!list.Contains(new Point(j, i)))
					{
						if (cell2 != null)
						{
							stringBuilder.Append("<td");
							if (cell2.Colspan > 1)
							{
								stringBuilder.AppendFormat(" colspan={0}", cell2.Colspan);
							}
							if (cell2.Rowspan > 1)
							{
								stringBuilder.AppendFormat(" rowspan={0}", cell2.Rowspan);
							}
							if (!cell2.Style.IsEmpty)
							{
								StringBuilder stringBuilder2 = new StringBuilder(" style='");
								if (!cell2.Style.ForeColor.IsEmpty)
								{
									stringBuilder2.AppendFormat("color: {0};", ColorTranslator.ToHtml(cell2.Style.ForeColor));
								}
								if (!cell2.Style.BackColor.IsEmpty)
								{
									stringBuilder2.AppendFormat("background-color: {0};", ColorTranslator.ToHtml(cell2.Style.BackColor));
								}
								if (cell2.Style.HorizontalAlign != HorizontalAlign.NotSet)
								{
									stringBuilder2.AppendFormat("text-align: {0};", cell2.Style.HorizontalAlign.ToString());
								}
								if (cell2.Style.VerticalAlign != VerticalAlign.NotSet)
								{
									stringBuilder2.AppendFormat("vertical-align: {0};", cell2.Style.VerticalAlign.ToString());
								}
								if (cell2.Style.Font.Italic)
								{
									stringBuilder2.AppendFormat("font-style: italic;", new object[0]);
								}
								if (cell2.Style.Font.Bold)
								{
									stringBuilder2.AppendFormat("font-weight: bold;", new object[0]);
								}
								if (cell2.Style.Font.Underline)
								{
									stringBuilder2.AppendFormat("text-decoration: underline;", new object[0]);
								}
								else if (cell2.Style.Font.Strikeout)
								{
									stringBuilder2.AppendFormat("text-decoration: strikeout;", new object[0]);
								}
								stringBuilder2.Append("'");
								stringBuilder.Append(stringBuilder2);
							}
							stringBuilder.Append(">");
							if (!string.IsNullOrEmpty(cell2.Hyperlink))
							{
								stringBuilder.AppendFormat("<a href='{0}'>{1}</a>", cell2.Hyperlink, cell2.Text);
							}
							else
							{
								stringBuilder.Append(cell2.Text);
							}
						}
						else
						{
							stringBuilder.Append("<td>");
						}
						stringBuilder.Append("</td>");
					}
				}
				stringBuilder.Append("</tr>");
			}
			stringBuilder.Append("</table>");
			return stringBuilder;
		}

		// Token: 0x040018D7 RID: 6359
		private ExportStructure _structure;
	}
}
