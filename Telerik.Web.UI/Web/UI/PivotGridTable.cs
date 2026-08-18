using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000E20 RID: 3616
	internal class PivotGridTable : Table
	{
		// Token: 0x06008903 RID: 35075 RVA: 0x001F34E7 File Offset: 0x001F16E7
		public PivotGridTable(RadPivotGrid owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17002B6A RID: 11114
		// (get) Token: 0x06008904 RID: 35076 RVA: 0x001F34F6 File Offset: 0x001F16F6
		// (set) Token: 0x06008905 RID: 35077 RVA: 0x001F34FE File Offset: 0x001F16FE
		public RadPivotGrid Owner { get; internal set; }

		// Token: 0x06008906 RID: 35078 RVA: 0x001F3508 File Offset: 0x001F1708
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this.CssClass;
			this.CssClass = string.Format(cssClass + " rpgTable", new object[0]).Trim();
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
			if (!this.Owner.IsDesignMode && this.CellSpacing == -1 && PivotGridTable.IsBrowser("IE") && !PivotGridTable.IsBrowserVersionNewer("IE", 7))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
			}
			if (this.CssClass == "rpgTableWrapper" && !string.IsNullOrEmpty(this.Owner.AccessibilitySettings.WrapperTableSummary))
			{
				writer.AddAttribute("summary", this.Owner.AccessibilitySettings.WrapperTableSummary);
			}
		}

		// Token: 0x06008907 RID: 35079 RVA: 0x001F35CC File Offset: 0x001F17CC
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (this.CssClass == "rpgTableWrapper")
			{
				AccessibilityHelper.RenderCaption(writer, this.Owner, this.Owner.AccessibilitySettings.WrapperTableCaption, "rpgWrapperTableCaption", false);
			}
			if (!(this is PivotGridColumnHeaderTable) && !(this is PivotGridRowHeaderTable))
			{
				this.RenderColGroup(writer);
			}
			base.RenderContents(writer);
		}

		// Token: 0x06008908 RID: 35080 RVA: 0x001F3668 File Offset: 0x001F1868
		protected virtual void RenderColGroup(HtmlTextWriter writer)
		{
			if (this.CssClass == "rpgTableWrapper")
			{
				return;
			}
			writer.Write("<colgroup>\r\n");
			IEnumerable<PivotGridField> source = from f in this.Owner.Fields
			where f is PivotGridRowField && !f.IsHidden
			select f;
			if (source.Count<PivotGridField>() > 0)
			{
				List<PivotGridField> list = (from f in source
				orderby f.ZoneIndex
				select f).ToList<PivotGridField>();
				if (this.Owner.AggregatesPosition == PivotGridAxis.Rows)
				{
					IEnumerable<PivotGridField> source2 = from f in this.Owner.Fields
					where f is PivotGridAggregateField && !f.IsHidden
					select f;
					if (source2.Count<PivotGridField>() > 1)
					{
						int num = this.Owner.AggregatesLevel;
						if (num < 0 || num > list.Count)
						{
							num = list.Count;
						}
						list.Insert(num, (from f in source2
						orderby f.ZoneIndex
						select f).Last<PivotGridField>());
					}
				}
				for (int i = 0; i < list.Count; i++)
				{
					PivotGridField field = list[i];
					this.RenderColWidth(writer, field, this.Owner.RowTableLayout == PivotGridLayout.Compact && i + 1 < list.Count);
				}
			}
			writer.Write("<col />\r\n");
			if (this.Owner.ClientSettings.Scrolling.AllowVerticalScroll)
			{
				writer.Write("<col />\r\n");
			}
			writer.Write("</colgroup>\r\n");
		}

		// Token: 0x06008909 RID: 35081 RVA: 0x001F380C File Offset: 0x001F1A0C
		private void RenderColWidth(HtmlTextWriter writer, PivotGridField field, bool compact)
		{
			if (!field.CellStyle.Width.IsEmpty)
			{
				writer.Write(string.Format("<col style=\"width:{0};\"/>\r\n", field.CellStyle.Width.ToString()));
				return;
			}
			if (!this.Owner.RowHeaderCellStyle.Width.IsEmpty)
			{
				writer.Write(string.Format("<col style=\"width:{0};\"/>\r\n", this.Owner.RowHeaderCellStyle.Width.ToString()));
				return;
			}
			if (compact)
			{
				writer.Write(string.Format("<col style=\"width:{0};\"/>\r\n", new Unit(23).ToString()));
				return;
			}
			writer.Write("<col />\r\n");
		}

		// Token: 0x0600890A RID: 35082 RVA: 0x001F38D6 File Offset: 0x001F1AD6
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
		}

		// Token: 0x0600890B RID: 35083 RVA: 0x001F38DF File Offset: 0x001F1ADF
		public static bool IsBrowser(string browser)
		{
			return HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.Browser.Browser.IndexOf(browser) > -1;
		}

		// Token: 0x0600890C RID: 35084 RVA: 0x001F3913 File Offset: 0x001F1B13
		public static bool IsBrowserVersionNewer(string browser, int version)
		{
			return PivotGridTable.IsBrowser(browser) && HttpContext.Current.Request.Browser.MajorVersion > version;
		}
	}
}
