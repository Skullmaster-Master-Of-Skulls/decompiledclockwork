using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000E21 RID: 3617
	internal class PivotGridColumnHeaderTable : PivotGridTable
	{
		// Token: 0x17002B6B RID: 11115
		// (get) Token: 0x06008911 RID: 35089 RVA: 0x001F3938 File Offset: 0x001F1B38
		internal Unit HeaderWidth
		{
			get
			{
				this.headerWidth = base.Owner.DataCellStyle.Width;
				if (base.Owner.ColumnHeaderCellStyle.Width.Value > base.Owner.DataCellStyle.Width.Value)
				{
					this.headerWidth = base.Owner.ColumnHeaderCellStyle.Width;
				}
				return this.headerWidth;
			}
		}

		// Token: 0x06008912 RID: 35090 RVA: 0x001F39A9 File Offset: 0x001F1BA9
		public PivotGridColumnHeaderTable(RadPivotGrid owner) : base(owner)
		{
			base.Owner = owner;
			this.ID = "CHT";
		}

		// Token: 0x06008913 RID: 35091 RVA: 0x001F3A00 File Offset: 0x001F1C00
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.Owner.AggregatesPosition == PivotGridAxis.Columns)
			{
				IEnumerable<PivotGridField> enumerable = from f in base.Owner.Fields
				where f is PivotGridAggregateField && !f.IsHidden
				select f;
				if (enumerable.Count<PivotGridField>() > 0)
				{
					enumerable = (from f in enumerable
					orderby f.ZoneIndex
					select f).ToList<PivotGridField>();
				}
				using (IEnumerator<PivotGridField> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PivotGridField pivotGridField = enumerator.Current;
						if (!pivotGridField.CellStyle.Width.IsEmpty)
						{
							base.Owner.ColumnHeaderTableLayout = PivotGridTableLayout.Fixed;
						}
					}
					goto IL_14D;
				}
			}
			IEnumerable<PivotGridField> source = from f in base.Owner.Fields
			where f is PivotGridColumnField && !f.IsHidden
			select f;
			PivotGridField pivotGridField2 = null;
			if (source.Count<PivotGridField>() > 0)
			{
				pivotGridField2 = (from f in source
				orderby f.ZoneIndex
				select f).LastOrDefault<PivotGridField>();
			}
			if (pivotGridField2 != null && !pivotGridField2.CellStyle.Width.IsEmpty)
			{
				base.Owner.ColumnHeaderTableLayout = PivotGridTableLayout.Fixed;
			}
			IL_14D:
			if (!this.HeaderWidth.IsEmpty || base.Owner.ColumnHeaderTableLayout == PivotGridTableLayout.Fixed)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Style, "table-layout: fixed;");
			}
			if (this.HeaderWidth.IsEmpty && base.Owner.ColumnHeaderTableLayout == PivotGridTableLayout.Fixed)
			{
				base.Owner.HideHorizontalScroll = true;
			}
			if (!string.IsNullOrEmpty(base.Owner.AccessibilitySettings.ColumnHeaderTableSummary))
			{
				writer.AddAttribute("summary", base.Owner.AccessibilitySettings.ColumnHeaderTableSummary);
			}
		}

		// Token: 0x06008914 RID: 35092 RVA: 0x001F3BF4 File Offset: 0x001F1DF4
		protected override void RenderContents(HtmlTextWriter writer)
		{
			AccessibilityHelper.RenderCaption(writer, base.Owner, base.Owner.AccessibilitySettings.ColumnHeaderTableCaption, "rpgColumnHeaderTableCaption", false);
			this.RenderColGroup(writer);
			if (base.Owner.ColumnHeaderTableLayout == PivotGridTableLayout.Auto)
			{
				this.RenderFakeRow(writer);
			}
			else
			{
				base.Owner.ShouldAdjustColumnsLayout = false;
			}
			base.RenderContents(writer);
		}

		// Token: 0x06008915 RID: 35093 RVA: 0x001F3C54 File Offset: 0x001F1E54
		private void RenderFakeRow(HtmlTextWriter writer)
		{
			if (this.HeaderWidth.IsEmpty)
			{
				TableRow tableRow = new TableRow();
				tableRow.ID = "FakeRow";
				tableRow.CssClass = "rpgDimensionRow";
				base.Owner.ShouldAdjustColumnsLayout = true;
				for (int i = 0; i < base.Owner.ColumnGroupsCount; i++)
				{
					tableRow.Cells.Add(new TableCell());
				}
				this.Rows.Add(tableRow);
				return;
			}
			base.Owner.ShouldAdjustColumnsLayout = false;
		}

		// Token: 0x06008916 RID: 35094 RVA: 0x001F3CDC File Offset: 0x001F1EDC
		private void RenderColWidth(HtmlTextWriter writer, PivotGridField field, PivotGridColumnHeaderCell headerCell)
		{
			if (field != null && !field.CellStyle.Width.IsEmpty)
			{
				if (base.Owner.ClientSettings.Resizing.AllowColumnResize && headerCell != null && base.Owner.ResizedColumnsWidth.ContainsKey("col" + headerCell.ID))
				{
					string[] array = headerCell.ID.Split(new char[]
					{
						'_'
					}, StringSplitOptions.RemoveEmptyEntries);
					string key = "col" + array[array.Length - 1];
					writer.Write(string.Format("<col style=\"width:{0};\"/>\r\n", base.Owner.ResizedColumnsWidth[key].ToString()));
					return;
				}
				if (headerCell.IsGrandTotalCell && !base.Owner.ColumnGrandTotalCellStyle.Width.IsEmpty)
				{
					writer.Write(string.Format("<col style=\"width:{0};\"/>\r\n", base.Owner.ColumnGrandTotalCellStyle.Width.ToString()));
					return;
				}
				writer.Write(string.Format("<col style=\"width:{0};\"/>\r\n", field.CellStyle.Width.ToString()));
				return;
			}
			else
			{
				if (base.Owner.ClientSettings.Resizing.AllowColumnResize && headerCell != null && base.Owner.ResizedColumnsWidth.ContainsKey("col" + headerCell.ID))
				{
					string[] array2 = headerCell.ID.Split(new char[]
					{
						'_'
					}, StringSplitOptions.RemoveEmptyEntries);
					string key2 = "col" + array2[array2.Length - 1];
					writer.Write(string.Format("<col style=\"width:{0};\"/>\r\n", base.Owner.ResizedColumnsWidth[key2].ToString()));
				}
				if (headerCell.IsGrandTotalCell && !base.Owner.ColumnGrandTotalCellStyle.Width.IsEmpty)
				{
					writer.Write(string.Format("<col style=\"width:{0};\"/>\r\n", base.Owner.ColumnGrandTotalCellStyle.Width.ToString()));
					return;
				}
				if (this.HeaderWidth.IsEmpty)
				{
					writer.Write("<col />\r\n");
					return;
				}
				writer.Write(string.Format("<col style=\"width:{0};\"/>\r\n", this.headerWidth.ToString()));
				return;
			}
		}

		// Token: 0x06008917 RID: 35095 RVA: 0x001F3F94 File Offset: 0x001F2194
		protected override void RenderColGroup(HtmlTextWriter writer)
		{
			writer.Write("<colgroup>\r\n");
			PivotGridColumnHeaderCell headerCell = null;
			for (int i = 0; i < base.Owner.ColumnGroupsCount; i++)
			{
				if (i < base.Owner.resizeableHeaderCellsList.Count)
				{
					headerCell = base.Owner.resizeableHeaderCellsList[i];
				}
				if (base.Owner.AggregatesPosition == PivotGridAxis.Columns)
				{
					IEnumerable<PivotGridField> source = from f in base.Owner.Fields
					where f is PivotGridAggregateField && !f.IsHidden
					select f;
					PivotGridField field = null;
					if (source.Count<PivotGridField>() > 0)
					{
						List<PivotGridField> list = (from f in source
						orderby f.ZoneIndex
						select f).ToList<PivotGridField>();
						if (list.Count > 0)
						{
							int index = i % list.Count;
							field = list[index];
						}
					}
					this.RenderColWidth(writer, field, headerCell);
				}
				else
				{
					IEnumerable<PivotGridField> source2 = from f in base.Owner.Fields
					where f is PivotGridColumnField && !f.IsHidden
					select f;
					if (source2.Count<PivotGridField>() > 0)
					{
						PivotGridField field2 = (from f in source2
						orderby f.ZoneIndex
						select f).LastOrDefault<PivotGridField>();
						this.RenderColWidth(writer, field2, headerCell);
					}
				}
			}
			writer.Write("</colgroup>\r\n");
		}

		// Token: 0x06008918 RID: 35096 RVA: 0x001F4109 File Offset: 0x001F2309
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
		}

		// Token: 0x04002647 RID: 9799
		private Unit headerWidth;
	}
}
