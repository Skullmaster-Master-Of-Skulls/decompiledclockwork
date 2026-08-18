using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000E26 RID: 3622
	internal class PivotGridRowHeaderTable : PivotGridTable
	{
		// Token: 0x17002B77 RID: 11127
		// (get) Token: 0x06008954 RID: 35156 RVA: 0x001F5857 File Offset: 0x001F3A57
		internal Unit HeaderWidth
		{
			get
			{
				return base.Owner.RowHeaderCellStyle.Width;
			}
		}

		// Token: 0x06008955 RID: 35157 RVA: 0x001F586C File Offset: 0x001F3A6C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (!this.HeaderWidth.IsEmpty || base.Owner.RowHeaderTableLayout == PivotGridTableLayout.Fixed)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Style, "table-layout: fixed;");
			}
			if (!string.IsNullOrEmpty(base.Owner.AccessibilitySettings.RowHeaderTableSummary))
			{
				writer.AddAttribute("summary", base.Owner.AccessibilitySettings.RowHeaderTableSummary);
			}
		}

		// Token: 0x06008956 RID: 35158 RVA: 0x001F58DD File Offset: 0x001F3ADD
		public PivotGridRowHeaderTable(RadPivotGrid owner) : base(owner)
		{
			base.Owner = owner;
			this.ID = "RHT";
		}

		// Token: 0x06008957 RID: 35159 RVA: 0x001F58F8 File Offset: 0x001F3AF8
		protected override void RenderContents(HtmlTextWriter writer)
		{
			AccessibilityHelper.RenderCaption(writer, base.Owner, base.Owner.AccessibilitySettings.RowHeaderTableCaption, "rpgRowHeaderTableCaption", false);
			this.RenderColGroup(writer);
			base.RenderContents(writer);
		}

		// Token: 0x06008958 RID: 35160 RVA: 0x001F5964 File Offset: 0x001F3B64
		protected override void RenderColGroup(HtmlTextWriter writer)
		{
			writer.Write("<colgroup>\r\n");
			IEnumerable<PivotGridField> source = from f in base.Owner.Fields
			where f is PivotGridRowField && !f.IsHidden
			select f;
			if (source.Count<PivotGridField>() > 0)
			{
				List<PivotGridField> list = (from f in source
				orderby f.ZoneIndex
				select f).ToList<PivotGridField>();
				if (base.Owner.AggregatesPosition == PivotGridAxis.Rows)
				{
					IEnumerable<PivotGridField> source2 = from f in base.Owner.Fields
					where f is PivotGridAggregateField && !f.IsHidden
					select f;
					if (source2.Count<PivotGridField>() > 1)
					{
						int num = base.Owner.AggregatesLevel;
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
					this.RenderColWidth(writer, list[i], base.Owner.RowTableLayout == PivotGridLayout.Compact && i + 1 < list.Count);
				}
				writer.Write("</colgroup>\r\n");
			}
		}

		// Token: 0x06008959 RID: 35161 RVA: 0x001F5AC4 File Offset: 0x001F3CC4
		private void RenderColWidth(HtmlTextWriter writer, PivotGridField field, bool compact)
		{
			if (!field.CellStyle.Width.IsEmpty)
			{
				writer.Write(string.Format("<col style=\"width:{0};\"/>\r\n", field.CellStyle.Width.ToString()));
				return;
			}
			if (!this.HeaderWidth.IsEmpty)
			{
				writer.Write(string.Format("<col style=\"width:{0};\"/>\r\n", this.HeaderWidth.ToString()));
				return;
			}
			if (compact)
			{
				writer.Write(string.Format("<col style=\"width:{0};\"/>\r\n", new Unit(23).ToString()));
				return;
			}
			writer.Write("<col />\r\n");
		}

		// Token: 0x0600895A RID: 35162 RVA: 0x001F5B7A File Offset: 0x001F3D7A
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
		}
	}
}
