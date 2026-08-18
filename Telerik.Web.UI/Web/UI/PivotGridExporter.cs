using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Export;
using Telerik.Web.UI.ExportInfrastructure;

namespace Telerik.Web.UI
{
	// Token: 0x0200074A RID: 1866
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	internal class PivotGridExporter
	{
		// Token: 0x1700158F RID: 5519
		// (get) Token: 0x06004224 RID: 16932 RVA: 0x000CF597 File Offset: 0x000CD797
		// (set) Token: 0x06004225 RID: 16933 RVA: 0x000CF59F File Offset: 0x000CD79F
		private RadPivotGrid Owner { get; set; }

		// Token: 0x06004226 RID: 16934 RVA: 0x000CF5A8 File Offset: 0x000CD7A8
		public PivotGridExporter(RadPivotGrid owner)
		{
			this.Owner = owner;
			this.structure = new ExportStructure();
			this.structure.ColumnWidthUnit = ExportUnitType.Pixel;
			this.structure.RowHeightUnit = ExportUnitType.Pixel;
			this.table = new Telerik.Web.UI.ExportInfrastructure.Table();
			this.structure.ColumnWidthUnit = ExportUnitType.Pixel;
			this.structure.RowHeightUnit = ExportUnitType.Pixel;
		}

		// Token: 0x06004227 RID: 16935 RVA: 0x000CF608 File Offset: 0x000CD808
		internal static HtmlForm GetForm(Control control)
		{
			HtmlForm htmlForm = PivotGridExporter.SafeGetForm(control);
			if (htmlForm == null)
			{
				throw new Exception("Telerik RadGrid must be placed inside a <form> tag with runat='server'.");
			}
			return htmlForm;
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x000CF62C File Offset: 0x000CD82C
		private static HtmlForm SafeGetForm(Control control)
		{
			GridTableView gridTableView = control as GridTableView;
			HtmlForm form;
			if (gridTableView != null)
			{
				form = gridTableView.OwnerGrid.Page.Form;
			}
			else
			{
				form = control.Page.Form;
			}
			return form;
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x000CF668 File Offset: 0x000CD868
		internal void ExportRenderPage(HtmlTextWriter nullWriter, Control page)
		{
			HtmlForm form = PivotGridExporter.GetForm(this.Owner);
			form.SetRenderMethodDelegate(new RenderMethod(this.ExportRenderForm));
			HtmlTextWriter writer = new HtmlTextWriter(TextWriter.Null);
			form.RenderControl(writer);
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x000CF6A8 File Offset: 0x000CD8A8
		private void ExportRenderForm(HtmlTextWriter nullWriter, Control form)
		{
			Page page = this.Owner.Page;
			HttpResponse response = page.Response;
			this.structure.Tables.Add(this.table);
			this.PopulateExportStructure();
			if (this.Owner.CurrentExportFormat == PivotGridExportFormat.Biff)
			{
				PivotGridBiffExportingEventArgs e = new PivotGridBiffExportingEventArgs(this.structure);
				this.Owner.CallOnBiffExporting(e);
			}
			this.Owner.FirePivotGridInfrastructureExporting(new PivotGridInfrastructureExportingEventArgs(this.structure, this.Owner.CurrentExportFormat));
			byte[] array = null;
			switch (this.Owner.CurrentExportFormat)
			{
			case PivotGridExportFormat.Biff:
				this.ConfigureResponse("application/vnd.ms-excel", ".xls", false, response);
				array = new XlsBiffRenderer(this.structure).Render();
				break;
			case PivotGridExportFormat.Xlsx:
				this.ConfigureResponse("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ".xlsx", false, response);
				array = new XlsxRenderer(this.structure).Render(null);
				break;
			case PivotGridExportFormat.Docx:
				this.ConfigureResponse("application/vnd.openxmlformats-officedocument.wordprocessingml.document", ".docx", false, response);
				array = new DocxRenderer(this.structure).Render();
				break;
			}
			PivotGridExportingArgs e2 = new PivotGridExportingArgs(Encoding.GetEncoding(1252).GetString(array));
			this.Owner.FirePivotGridExporting(e2);
			response.BinaryWrite(array);
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x000CF7E8 File Offset: 0x000CD9E8
		private void ConfigureResponse(string contentType, string fileExtension, bool isMacOffice, HttpResponse response)
		{
			response.ClearHeaders();
			response.Clear();
			response.Buffer = true;
			response.ContentType = contentType;
			response.ContentEncoding = Encoding.UTF8;
			response.Charset = "";
			if (GridTableViewHelper.IsBrowser("IE", 8))
			{
				this.Owner.ExportSettings.FileName = HttpUtility.UrlEncode(this.Owner.ExportSettings.FileName, Encoding.UTF8);
			}
			string text = this.Owner.ExportSettings.FileName + fileExtension;
			text = text.Replace("\n", " ").Replace("\r", " ");
			if (!this.Owner.ExportSettings.OpenInNewWindow)
			{
				response.AddHeader("Content-Disposition", "inline;filename=\"" + text + "\"");
				return;
			}
			response.AddHeader("Content-Disposition", "attachment;filename=\"" + text + "\"");
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x000CF8E4 File Offset: 0x000CDAE4
		internal void PopulateExportStructure()
		{
			try
			{
				this.Owner.Page.UnregisterRequiresControlState(this.Owner);
				this.Owner.IsExporting = true;
				if (this.Owner.ExportSettings.IgnorePaging)
				{
					this.Owner.AllowPaging = false;
				}
				this.Owner.CallAutoDataBind(PivotGridRebindReason.ExplicitRebind);
			}
			finally
			{
				this.Owner.IsExporting = false;
			}
			if (this.Owner.ExportSettings.UseItemStyles)
			{
				this.Owner.PrepareTableItemsStyle(this.Owner.OuterTable.Rows);
				this.Owner.PrepareTableItemsStyle(this.Owner.RowHeaderTable.Rows);
				this.Owner.PrepareTableItemsStyle(this.Owner.ColumnHeaderTable.Rows);
				this.Owner.PrepareTableItemsStyle(this.Owner.DataTable.Rows);
			}
			int num = 1;
			List<PivotGridModelRow> rows = this.Owner.ColumnHeadersModel.Rows;
			for (int i = 0; i < rows.Count; i++)
			{
				foreach (PivotGridModelCellBase pivotGridModelCellBase in rows[i].Cells)
				{
					PivotGridModelCell pivotGridModelCell = (PivotGridModelCell)pivotGridModelCellBase;
					Cell cell = this.table.Cells[pivotGridModelCell.Slot + this.Owner.RowLayout.GroupLevels + 1, num];
					if (!string.IsNullOrEmpty(pivotGridModelCell.DataCell.Text))
					{
						cell.Value = pivotGridModelCell.DataCell.Text;
					}
					else
					{
						cell.Value = pivotGridModelCell.Name;
					}
					cell.Rowspan = ((pivotGridModelCell.RowSpan == 0) ? 1 : pivotGridModelCell.RowSpan);
					cell.Colspan = ((pivotGridModelCell.ColSpan == 0) ? 1 : pivotGridModelCell.ColSpan);
					PivotGridBaseModelCell pivotGridBaseModelCell = new PivotGridBaseModelCell();
					this.PopulatePivotGridBaseModelCell(pivotGridBaseModelCell, pivotGridModelCell, PivotGridTableCellType.ColumnHeaderCell);
					this.CopyExportRelatedStyles(cell, pivotGridModelCell.DataCell);
					PivotGridCellExportingArgs e = new PivotGridCellExportingArgs(cell, pivotGridBaseModelCell);
					this.Owner.FirePivotGridCellExporting(e);
				}
				num++;
			}
			int num2 = num;
			List<PivotGridModelRow> rows2 = this.Owner.RowHeaderModel.Rows;
			for (int j = 0; j < rows2.Count; j++)
			{
				foreach (PivotGridModelCellBase pivotGridModelCellBase2 in rows2[j].Cells)
				{
					PivotGridModelCell pivotGridModelCell2 = (PivotGridModelCell)pivotGridModelCellBase2;
					Cell cell2 = this.table.Cells[pivotGridModelCell2.GroupLevel + 1, num];
					if (!string.IsNullOrEmpty(pivotGridModelCell2.DataCell.Text))
					{
						cell2.Value = pivotGridModelCell2.DataCell.Text;
					}
					else
					{
						cell2.Value = pivotGridModelCell2.Name;
					}
					cell2.Rowspan = ((pivotGridModelCell2.RowSpan == 0) ? 1 : pivotGridModelCell2.RowSpan);
					cell2.Colspan = ((pivotGridModelCell2.ColSpan == 0) ? 1 : pivotGridModelCell2.ColSpan);
					PivotGridBaseModelCell pivotGridBaseModelCell2 = new PivotGridBaseModelCell();
					this.PopulatePivotGridBaseModelCell(pivotGridBaseModelCell2, pivotGridModelCell2, PivotGridTableCellType.RowHeaderCell);
					this.CopyExportRelatedStyles(cell2, pivotGridModelCell2.DataCell);
					PivotGridCellExportingArgs e2 = new PivotGridCellExportingArgs(cell2, pivotGridBaseModelCell2);
					this.Owner.FirePivotGridCellExporting(e2);
				}
				num++;
			}
			int num3 = this.Owner.RowLayout.GroupLevels;
			List<PivotGridModelDataRow> rows3 = this.Owner.DataModel.Rows;
			num = num2;
			for (int k = 0; k < rows3.Count; k++)
			{
				foreach (PivotGridModelCellBase pivotGridModelCellBase3 in rows3[k].Cells)
				{
					PivotGridModelDataCell pivotGridModelDataCell = (PivotGridModelDataCell)pivotGridModelCellBase3;
					Cell cell3 = this.table.Cells[num3 + 1, num];
					object value = pivotGridModelDataCell.Name;
					if (pivotGridModelDataCell.Name != null && pivotGridModelDataCell.Name.ToString() == "Telerik.Web.UI.PivotGrid.Core.Aggregates.AggregateError")
					{
						value = pivotGridModelDataCell.FormattedValue;
					}
					cell3.Value = value;
					PivotGridBaseModelCell pivotGridBaseModelCell3 = new PivotGridBaseModelCell();
					this.PopulatePivotGridBaseModelCell(pivotGridBaseModelCell3, pivotGridModelDataCell, PivotGridTableCellType.DataCell);
					this.CopyExportRelatedStyles(cell3, pivotGridBaseModelCell3.BaseCell.DataCell);
					PivotGridCellExportingArgs e3 = new PivotGridCellExportingArgs(cell3, pivotGridBaseModelCell3);
					this.Owner.FirePivotGridCellExporting(e3);
					num3++;
				}
				num3 = this.Owner.RowLayout.GroupLevels;
				num++;
			}
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x000CFDA0 File Offset: 0x000CDFA0
		private void CopyExportRelatedStyles(Cell eiCell, PivotGridCell pivotCell)
		{
			if (!this.Owner.ExportSettings.UseItemStyles)
			{
				return;
			}
			if (!pivotCell.BackColor.IsEmpty)
			{
				eiCell.Style.BackColor = pivotCell.BackColor;
			}
			if (!pivotCell.ForeColor.IsEmpty)
			{
				eiCell.Style.ForeColor = pivotCell.ForeColor;
			}
			if (!Utils.IsEmptyFontStyle(pivotCell.Font))
			{
				eiCell.Style.Font.Bold = pivotCell.Font.Bold;
				eiCell.Style.Font.Italic = pivotCell.Font.Italic;
				eiCell.Style.Font.Overline = pivotCell.Font.Overline;
				eiCell.Style.Font.Strikeout = pivotCell.Font.Strikeout;
				eiCell.Style.Font.Underline = pivotCell.Font.Underline;
				if (!string.IsNullOrEmpty(pivotCell.Font.Name))
				{
					eiCell.Style.Font.Name = pivotCell.Font.Name;
				}
				if (!pivotCell.Font.Size.IsEmpty)
				{
					eiCell.Style.Font.Size = pivotCell.Font.Size;
				}
			}
			if (pivotCell.HorizontalAlign != HorizontalAlign.NotSet)
			{
				eiCell.Style.HorizontalAlign = pivotCell.HorizontalAlign;
			}
			if (pivotCell.VerticalAlign != VerticalAlign.NotSet)
			{
				eiCell.Style.VerticalAlign = pivotCell.VerticalAlign;
			}
			if (!pivotCell.BorderColor.IsEmpty)
			{
				eiCell.Style.BorderTopColor = (eiCell.Style.BorderBottomColor = (eiCell.Style.BorderLeftColor = (eiCell.Style.BorderRightColor = pivotCell.BorderColor)));
			}
			if (pivotCell.BorderStyle != BorderStyle.NotSet)
			{
				eiCell.Style.BorderTopStyle = (eiCell.Style.BorderBottomStyle = (eiCell.Style.BorderLeftStyle = (eiCell.Style.BorderRightStyle = pivotCell.BorderStyle)));
			}
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x000CFFC4 File Offset: 0x000CE1C4
		private void PopulatePivotGridBaseModelCell(PivotGridBaseModelCell modelCell, PivotGridModelCellBase baseCell, PivotGridTableCellType TableCellType)
		{
			PivotGridModelDataCell pivotGridModelDataCell = baseCell as PivotGridModelDataCell;
			PivotGridModelCell pivotGridModelCell = baseCell as PivotGridModelCell;
			if (pivotGridModelDataCell != null)
			{
				modelCell.CellType = pivotGridModelDataCell.CellType;
			}
			else
			{
				modelCell.GroupLevel = pivotGridModelCell.GroupLevel;
				modelCell.IsCollapsed = pivotGridModelCell.IsCollapsed;
				modelCell.HasChildren = pivotGridModelCell.HasChildren;
				modelCell.IsTotalCell = pivotGridModelCell.IsTotalCell;
				modelCell.IsGrandTotalCell = pivotGridModelCell.IsGrandTotalCell;
			}
			modelCell.BaseCell = baseCell;
			modelCell.Data = baseCell.Name;
			modelCell.Field = baseCell.Field;
			modelCell.TableCellType = TableCellType;
		}

		// Token: 0x04001189 RID: 4489
		private ExportStructure structure;

		// Token: 0x0400118A RID: 4490
		private Telerik.Web.UI.ExportInfrastructure.Table table;
	}
}
