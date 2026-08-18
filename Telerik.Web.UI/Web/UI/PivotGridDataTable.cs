using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000E22 RID: 3618
	internal class PivotGridDataTable : PivotGridTable
	{
		// Token: 0x17002B6C RID: 11116
		// (get) Token: 0x06008921 RID: 35105 RVA: 0x001F4114 File Offset: 0x001F2314
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

		// Token: 0x06008922 RID: 35106 RVA: 0x001F4185 File Offset: 0x001F2385
		public PivotGridDataTable(RadPivotGrid owner) : base(owner)
		{
			base.Owner = owner;
			this.ID = "DT";
		}

		// Token: 0x06008923 RID: 35107 RVA: 0x001F41DC File Offset: 0x001F23DC
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
					goto IL_14B;
				}
			}
			IEnumerable<PivotGridField> source = from f in base.Owner.Fields
			where f is PivotGridColumnField && !f.IsHidden
			select f;
			if (source.Count<PivotGridField>() > 0)
			{
				PivotGridField pivotGridField2 = (from f in source
				orderby f.ZoneIndex
				select f).LastOrDefault<PivotGridField>();
				if (pivotGridField2 != null && !pivotGridField2.CellStyle.Width.IsEmpty)
				{
					base.Owner.ColumnHeaderTableLayout = PivotGridTableLayout.Fixed;
				}
			}
			IL_14B:
			if (!this.HeaderWidth.IsEmpty || base.Owner.ColumnHeaderTableLayout == PivotGridTableLayout.Fixed)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Style, "table-layout: fixed;");
			}
			if (!string.IsNullOrEmpty(base.Owner.AccessibilitySettings.DataTableSummary))
			{
				writer.AddAttribute("summary", base.Owner.AccessibilitySettings.DataTableSummary);
			}
		}

		// Token: 0x06008924 RID: 35108 RVA: 0x001F43A4 File Offset: 0x001F25A4
		protected override void RenderContents(HtmlTextWriter writer)
		{
			AccessibilityHelper.RenderCaption(writer, base.Owner, base.Owner.AccessibilitySettings.DataTableCaption, "rpgDataTableCaption", false);
			base.RenderContents(writer);
		}

		// Token: 0x06008925 RID: 35109 RVA: 0x001F440C File Offset: 0x001F260C
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

		// Token: 0x06008926 RID: 35110 RVA: 0x001F4584 File Offset: 0x001F2784
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

		// Token: 0x06008927 RID: 35111 RVA: 0x001F4802 File Offset: 0x001F2A02
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
		}

		// Token: 0x04002650 RID: 9808
		private Unit headerWidth;
	}
}
