using System;
using System.Collections.Generic;
using System.Globalization;
using iTextSharp.text.pdf;

namespace iTextSharp.text.html.simpleparser
{
	// Token: 0x02000290 RID: 656
	public class IncTable : IElement
	{
		// Token: 0x060018DB RID: 6363 RVA: 0x00092960 File Offset: 0x00091960
		public IncTable(Dictionary<string, string> props)
		{
			foreach (KeyValuePair<string, string> keyValuePair in props)
			{
				this.props[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x000929DC File Offset: 0x000919DC
		public void AddCol(PdfPCell cell)
		{
			if (this.cols == null)
			{
				this.cols = new List<PdfPCell>();
			}
			this.cols.Add(cell);
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x000929FD File Offset: 0x000919FD
		public void AddCols(List<PdfPCell> ncols)
		{
			if (this.cols == null)
			{
				this.cols = new List<PdfPCell>(ncols);
				return;
			}
			this.cols.AddRange(ncols);
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x00092A20 File Offset: 0x00091A20
		public void EndRow()
		{
			if (this.cols != null)
			{
				this.cols.Reverse();
				this.rows.Add(this.cols);
				this.cols = null;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x00092A4D File Offset: 0x00091A4D
		public List<List<PdfPCell>> Rows
		{
			get
			{
				return this.rows;
			}
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x00092A58 File Offset: 0x00091A58
		public PdfPTable BuildTable()
		{
			if (this.rows.Count == 0)
			{
				return new PdfPTable(1);
			}
			int num = 0;
			foreach (PdfPCell pdfPCell in this.rows[0])
			{
				num += pdfPCell.Colspan;
			}
			PdfPTable pdfPTable = new PdfPTable(num);
			string text;
			if (!this.props.TryGetValue("width", out text))
			{
				pdfPTable.WidthPercentage = 100f;
			}
			else if (text.EndsWith("%"))
			{
				pdfPTable.WidthPercentage = float.Parse(text.Substring(0, text.Length - 1), NumberFormatInfo.InvariantInfo);
			}
			else
			{
				pdfPTable.TotalWidth = float.Parse(text, NumberFormatInfo.InvariantInfo);
				pdfPTable.LockedWidth = true;
			}
			foreach (List<PdfPCell> list in this.rows)
			{
				foreach (PdfPCell cell in list)
				{
					pdfPTable.AddCell(cell);
				}
			}
			return pdfPTable;
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x00092BB8 File Offset: 0x00091BB8
		public bool Process(IElementListener listener)
		{
			return false;
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x00092BBB File Offset: 0x00091BBB
		public int Type
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x00092BBE File Offset: 0x00091BBE
		public bool IsContent()
		{
			return false;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x00092BC1 File Offset: 0x00091BC1
		public bool IsNestable()
		{
			return false;
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x00092BC4 File Offset: 0x00091BC4
		public List<Chunk> Chunks
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040010C9 RID: 4297
		private Dictionary<string, string> props = new Dictionary<string, string>();

		// Token: 0x040010CA RID: 4298
		private List<List<PdfPCell>> rows = new List<List<PdfPCell>>();

		// Token: 0x040010CB RID: 4299
		private List<PdfPCell> cols;
	}
}
