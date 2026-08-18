using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AF2 RID: 2802
	internal class Worksheet : IDisposable
	{
		// Token: 0x1700226B RID: 8811
		// (get) Token: 0x06006926 RID: 26918 RVA: 0x0018B6CB File Offset: 0x001898CB
		// (set) Token: 0x06006927 RID: 26919 RVA: 0x0018B6D3 File Offset: 0x001898D3
		public bool ShowGridlines { get; set; }

		// Token: 0x1700226C RID: 8812
		// (get) Token: 0x06006928 RID: 26920 RVA: 0x0018B6DC File Offset: 0x001898DC
		// (set) Token: 0x06006929 RID: 26921 RVA: 0x0018B6E4 File Offset: 0x001898E4
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700226D RID: 8813
		// (get) Token: 0x0600692A RID: 26922 RVA: 0x0018B6ED File Offset: 0x001898ED
		public Collection<Column> Columns
		{
			get
			{
				if (this.columns == null)
				{
					this.columns = new Collection<Column>();
				}
				return this.columns;
			}
		}

		// Token: 0x0600692B RID: 26923 RVA: 0x0018B708 File Offset: 0x00189908
		public Column AddColumn()
		{
			Column column = new Column();
			this.Columns.Add(column);
			return column;
		}

		// Token: 0x0600692C RID: 26924 RVA: 0x0018B728 File Offset: 0x00189928
		public Column AddColumn(double width)
		{
			Column column = new Column(width);
			this.Columns.Add(column);
			return column;
		}

		// Token: 0x1700226E RID: 8814
		// (get) Token: 0x0600692D RID: 26925 RVA: 0x0018B749 File Offset: 0x00189949
		public Collection<Row> Rows
		{
			get
			{
				if (this.rows == null)
				{
					this.rows = new Collection<Row>();
				}
				return this.rows;
			}
		}

		// Token: 0x0600692E RID: 26926 RVA: 0x0018B764 File Offset: 0x00189964
		public Row AddRow()
		{
			Row row = new Row();
			this.Rows.Add(row);
			return row;
		}

		// Token: 0x0600692F RID: 26927 RVA: 0x0018B784 File Offset: 0x00189984
		public Row AddRow(double height)
		{
			Row row = new Row(height);
			this.Rows.Add(row);
			return row;
		}

		// Token: 0x06006930 RID: 26928 RVA: 0x0018B7A8 File Offset: 0x001899A8
		public Range GetRange(int col, int row, int colSpan, int rowSpan)
		{
			Range range = new Range(this, col, row, colSpan, rowSpan);
			if (!range.CanBeMerged())
			{
				string message = string.Format("Attempting to get range {0}, that intersects with an already merged range.", range);
				throw new InvalidOperationException(message);
			}
			return range;
		}

		// Token: 0x1700226F RID: 8815
		// (get) Token: 0x06006931 RID: 26929 RVA: 0x0018B7DD File Offset: 0x001899DD
		public Margins Margins
		{
			get
			{
				if (this.margins == null)
				{
					this.margins = new Margins();
				}
				return this.margins;
			}
		}

		// Token: 0x17002270 RID: 8816
		// (get) Token: 0x06006932 RID: 26930 RVA: 0x0018B7F8 File Offset: 0x001899F8
		// (set) Token: 0x06006933 RID: 26931 RVA: 0x0018B800 File Offset: 0x00189A00
		public bool Landscape
		{
			get
			{
				return this.landscape;
			}
			set
			{
				this.landscape = value;
			}
		}

		// Token: 0x17002271 RID: 8817
		// (get) Token: 0x06006934 RID: 26932 RVA: 0x0018B809 File Offset: 0x00189A09
		// (set) Token: 0x06006935 RID: 26933 RVA: 0x0018B811 File Offset: 0x00189A11
		public SizeF PageSize
		{
			get
			{
				return this.pageSize;
			}
			set
			{
				this.pageSize = value;
			}
		}

		// Token: 0x17002272 RID: 8818
		// (get) Token: 0x06006936 RID: 26934 RVA: 0x0018B81A File Offset: 0x00189A1A
		// (set) Token: 0x06006937 RID: 26935 RVA: 0x0018B822 File Offset: 0x00189A22
		public int FrozenRows
		{
			get
			{
				return this.frozenRows;
			}
			set
			{
				this.frozenRows = value;
			}
		}

		// Token: 0x17002273 RID: 8819
		// (get) Token: 0x06006938 RID: 26936 RVA: 0x0018B82B File Offset: 0x00189A2B
		// (set) Token: 0x06006939 RID: 26937 RVA: 0x0018B833 File Offset: 0x00189A33
		public string PageHeader
		{
			get
			{
				return this.pageHeader;
			}
			set
			{
				this.pageHeader = value;
			}
		}

		// Token: 0x17002274 RID: 8820
		// (get) Token: 0x0600693A RID: 26938 RVA: 0x0018B83C File Offset: 0x00189A3C
		// (set) Token: 0x0600693B RID: 26939 RVA: 0x0018B844 File Offset: 0x00189A44
		public string PageFooter
		{
			get
			{
				return this.pageFooter;
			}
			set
			{
				this.pageFooter = value;
			}
		}

		// Token: 0x17002275 RID: 8821
		// (get) Token: 0x0600693C RID: 26940 RVA: 0x0018B84D File Offset: 0x00189A4D
		public Workbook Workbook
		{
			get
			{
				return this.workbook;
			}
		}

		// Token: 0x17002276 RID: 8822
		// (get) Token: 0x0600693D RID: 26941 RVA: 0x0018B855 File Offset: 0x00189A55
		public List<Range> MergedRanges
		{
			get
			{
				if (this.mergedRanges == null)
				{
					this.mergedRanges = new List<Range>();
				}
				return this.mergedRanges;
			}
		}

		// Token: 0x17002277 RID: 8823
		// (get) Token: 0x0600693E RID: 26942 RVA: 0x0018B870 File Offset: 0x00189A70
		public CellGrid CellGrid
		{
			get
			{
				if (this.cellGrid == null)
				{
					this.cellGrid = new CellGrid(this);
				}
				return this.cellGrid;
			}
		}

		// Token: 0x17002278 RID: 8824
		// (get) Token: 0x0600693F RID: 26943 RVA: 0x0018B88C File Offset: 0x00189A8C
		public Stream Stream
		{
			get
			{
				if (this.stream == null)
				{
					this.stream = new MemoryStream();
				}
				return this.stream;
			}
		}

		// Token: 0x17002279 RID: 8825
		// (get) Token: 0x06006940 RID: 26944 RVA: 0x0018B8A7 File Offset: 0x00189AA7
		public Dictionary<Range, PageBreak> PageBreakInfos
		{
			get
			{
				if (this.pageBreakInfos == null)
				{
					this.pageBreakInfos = new Dictionary<Range, PageBreak>();
				}
				return this.pageBreakInfos;
			}
		}

		// Token: 0x1700227A RID: 8826
		// (get) Token: 0x06006941 RID: 26945 RVA: 0x0018B8C2 File Offset: 0x00189AC2
		private int SheetIndex
		{
			get
			{
				if (this.workbook != null)
				{
					return this.workbook.Worksheets.IndexOf(this);
				}
				return -1;
			}
		}

		// Token: 0x06006942 RID: 26946 RVA: 0x0018B8E0 File Offset: 0x00189AE0
		internal uint GetNumberOfRowBlocks()
		{
			uint result = 0U;
			if (this.CellGrid.LastRow > -1)
			{
				result = (uint)(this.CellGrid.LastRow / 32 + 1);
			}
			return result;
		}

		// Token: 0x06006943 RID: 26947 RVA: 0x0018B910 File Offset: 0x00189B10
		internal int AddImage(byte[] imageData, Escher.RecordType recordType, Escher.ClientAnchor.SPRC clientSPRC, string imageName)
		{
			int num;
			int num2;
			uint referenceIndex = (uint)this.workbook.AddImage(imageData, imageName, recordType, this.SheetIndex, out num, out num2);
			if (this.drawingContainer == null)
			{
				this.drawingContainer = new Escher.DrawingContainer((ushort)num2);
				this.currentSPID = num;
			}
			this.currentSPID++;
			return this.drawingContainer.AddShape((uint)this.currentSPID, imageName, clientSPRC, referenceIndex);
		}

		// Token: 0x06006944 RID: 26948 RVA: 0x0018B978 File Offset: 0x00189B78
		public void Write(Stream stream)
		{
			int num = 0;
			int firstRow = 0;
			int lastColumn = this.CellGrid.LastColumn;
			int lastRow = this.CellGrid.LastRow;
			byte[] data = new BOF(true).GetData();
			stream.Write(data, 0, data.Length);
			uint indexRecOffsetsStart = 0U;
			if (lastRow > -1 && lastColumn > -1)
			{
				indexRecOffsetsStart = this.WriteIndexRecord(firstRow, lastRow, stream);
			}
			data = new CalcMode().GetData();
			stream.Write(data, 0, data.Length);
			data = new CalcCount().GetData();
			stream.Write(data, 0, data.Length);
			data = new RefMode().GetData();
			stream.Write(data, 0, data.Length);
			data = new Iteration().GetData();
			stream.Write(data, 0, data.Length);
			data = new Delta().GetData();
			stream.Write(data, 0, data.Length);
			data = new SaveRecalc().GetData();
			stream.Write(data, 0, data.Length);
			data = new PrintHeaders().GetData();
			stream.Write(data, 0, data.Length);
			data = new PrintGridLines().GetData();
			stream.Write(data, 0, data.Length);
			data = new GridSet().GetData();
			stream.Write(data, 0, data.Length);
			data = new Guts(0, 0).GetData();
			stream.Write(data, 0, data.Length);
			data = new DefaultRowHeight().GetData();
			stream.Write(data, 0, data.Length);
			this.WriteWSBool(stream);
			this.WritePageBreaks(stream);
			if (!string.IsNullOrEmpty(this.PageHeader))
			{
				data = new HeaderFooter(this.PageHeader, 20).GetData();
				stream.Write(data, 0, data.Length);
			}
			if (!string.IsNullOrEmpty(this.PageFooter))
			{
				data = new HeaderFooter(this.PageFooter, 21).GetData();
				stream.Write(data, 0, data.Length);
			}
			data = new HCenter().GetData();
			stream.Write(data, 0, data.Length);
			data = new VCenter().GetData();
			stream.Write(data, 0, data.Length);
			data = new Margin(this.Margins.Left, 38).GetData();
			stream.Write(data, 0, data.Length);
			data = new Margin(this.Margins.Right, 39).GetData();
			stream.Write(data, 0, data.Length);
			data = new Margin(this.Margins.Top, 40).GetData();
			stream.Write(data, 0, data.Length);
			data = new Margin(this.Margins.Bottom, 41).GetData();
			stream.Write(data, 0, data.Length);
			int paperSizeIndex = 0;
			if (this.PageSize != SizeF.Empty)
			{
				paperSizeIndex = PaperSizeIndex.GetPaperSizeIndex(this.PageSize);
			}
			data = new Setup(paperSizeIndex, !this.Landscape, 0.0, 0.0).GetData();
			stream.Write(data, 0, data.Length);
			data = new DefColWidth().GetData();
			stream.Write(data, 0, data.Length);
			this.WriteColInfoRecords(stream);
			Dimensions dimensions = new Dimensions((uint)firstRow, (uint)(lastRow + 1), (ushort)num, (ushort)(lastColumn + 1));
			data = dimensions.GetData();
			stream.Write(data, 0, data.Length);
			if (lastColumn >= 0)
			{
				this.WriteRowBlocksAndCells(stream, lastRow, indexRecOffsetsStart, num, lastColumn);
			}
			this.WriteEscher(stream);
			Window2 window = new Window2();
			if (this.ShowGridlines)
			{
				window.TurnOnGridLines();
			}
			else
			{
				window.TurnOffGridLines();
			}
			if (this.SheetIndex == 0)
			{
				window.DisplaySelectedSheet();
			}
			ushort num2 = (ushort)this.FrozenRows;
			if (num2 > 0)
			{
				window.FreezePane();
			}
			data = window.GetData();
			stream.Write(data, 0, data.Length);
			if (num2 > 0)
			{
				data = new Pane(0, num2, num2, 0, 2).GetData();
				stream.Write(data, 0, data.Length);
			}
			data = new Selection().GetData();
			stream.Write(data, 0, data.Length);
			if (this.MergedRanges.Count > 0)
			{
				this.WriteMergedCellsRecords(stream);
			}
			data = new EOF().GetData();
			stream.Write(data, 0, data.Length);
		}

		// Token: 0x06006945 RID: 26949 RVA: 0x0018BD98 File Offset: 0x00189F98
		private void WriteMergedCellsRecords(Stream stream)
		{
			List<Ref> list = new List<Ref>();
			foreach (Range range in this.MergedRanges)
			{
				list.Add(new Ref
				{
					colFirst = (ushort)range.FirstCol,
					colLast = (ushort)range.LastCol,
					rwFirst = (ushort)range.FirstRow,
					rwLast = (ushort)range.LastRow
				});
			}
			int num = list.Count / 1027;
			Ref[] array = null;
			for (int i = 0; i < num; i++)
			{
				if (i == 0)
				{
					array = new Ref[1027];
				}
				list.CopyTo(i * 1027, array, 0, 1027);
				byte[] data = new MergeCells(array).GetData();
				stream.Write(data, 0, data.Length);
			}
			int num2 = list.Count % 1027;
			if (num2 > 0)
			{
				array = new Ref[num2];
				list.CopyTo(num * 1027, array, 0, num2);
				byte[] data2 = new MergeCells(array).GetData();
				stream.Write(data2, 0, data2.Length);
			}
		}

		// Token: 0x06006946 RID: 26950 RVA: 0x0018BEDC File Offset: 0x0018A0DC
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private uint WriteIndexRecord(int firstRow, int lastRow, Stream stream)
		{
			uint numberOfRowBlocks = this.GetNumberOfRowBlocks();
			uint[] dbCellOffsets = new uint[numberOfRowBlocks];
			uint result = (uint)(stream.Position + 4L + 16L);
			Index index = new Index((uint)firstRow, (uint)(lastRow + 1), dbCellOffsets);
			byte[] data = index.GetData();
			stream.Write(data, 0, data.Length);
			return result;
		}

		// Token: 0x06006947 RID: 26951 RVA: 0x0018BF27 File Offset: 0x0018A127
		private void WriteFirstRow(Stream stream, IRecord rowRecord, out uint startStreamPosOfFirstRow, out uint endStreamPosOfFirstRow)
		{
			startStreamPosOfFirstRow = (uint)stream.Position;
			this.WriteRow(stream, rowRecord);
			endStreamPosOfFirstRow = (uint)stream.Position;
		}

		// Token: 0x06006948 RID: 26952 RVA: 0x0018BF44 File Offset: 0x0018A144
		private void WriteRow(Stream stream, IRecord rowRecordRecord)
		{
			byte[] data = rowRecordRecord.GetData();
			stream.Write(data, 0, data.Length);
		}

		// Token: 0x06006949 RID: 26953 RVA: 0x0018BF64 File Offset: 0x0018A164
		private void WriteCell(Stream stream, BiffCell biffCell, ICollection<ushort> cellAddresses, int row, int col, uint firstRowEndStreamPos, ref MulBlankCell multipleBlankCells)
		{
			if (biffCell == null)
			{
				throw new ArgumentException("Biff Cell is null", "biffCell");
			}
			BlankCell blankCell = biffCell as BlankCell;
			if (blankCell == null)
			{
				if (multipleBlankCells != null)
				{
					multipleBlankCells.Write(stream);
					multipleBlankCells = null;
				}
				this.WriteCell(stream, biffCell, cellAddresses, row, col, firstRowEndStreamPos);
				return;
			}
			if (multipleBlankCells == null)
			{
				multipleBlankCells = new MulBlankCell((ushort)row, (ushort)col, blankCell);
				return;
			}
			multipleBlankCells.Add(blankCell);
		}

		// Token: 0x0600694A RID: 26954 RVA: 0x0018BFD0 File Offset: 0x0018A1D0
		private void WriteCell(Stream stream, BiffCell biffBiffCell, ICollection<ushort> cellAddresses, int row, int col, uint firstRowEndStreamPos)
		{
			if (biffBiffCell != null)
			{
				IRecord record = biffBiffCell.GetRecord(row, col);
				if (record.RecordType == 516 || record.RecordType == 253 || record.RecordType == 515)
				{
					ushort item = (ushort)(stream.Position - (long)((ulong)firstRowEndStreamPos));
					cellAddresses.Add(item);
				}
				byte[] data = record.GetData();
				stream.Write(data, 0, data.Length);
			}
		}

		// Token: 0x0600694B RID: 26955 RVA: 0x0018C038 File Offset: 0x0018A238
		private void WriteDBCell(Stream stream, int currentDBCellRecordNumber, List<ushort> cellAddresses, uint indexBeginOffsetPosition, uint firstRowBeginStreamPos)
		{
			uint num = (uint)stream.Position;
			int num2 = 4;
			stream.Position = (long)((ulong)indexBeginOffsetPosition + (ulong)((long)(currentDBCellRecordNumber * num2)));
			byte[] bytes = BitConverter.GetBytes(num);
			stream.Write(bytes, 0, bytes.Length);
			stream.Position = (long)((ulong)num);
			uint dbRtrw = num - firstRowBeginStreamPos;
			DBCell dbcell = new DBCell(dbRtrw, cellAddresses);
			dbcell.WriteToStream(stream);
		}

		// Token: 0x0600694C RID: 26956 RVA: 0x0018C090 File Offset: 0x0018A290
		private void WriteRowBlocksAndCells(Stream stream, int lastRow, uint indexRecOffsetsStart, int firstCol, int lastCol)
		{
			int num = 0;
			uint numberOfRowBlocks = this.GetNumberOfRowBlocks();
			int num2 = 0;
			while ((long)num2 < (long)((ulong)numberOfRowBlocks))
			{
				List<ushort> cellAddresses = new List<ushort>();
				uint firstRowBeginStreamPos = 0U;
				uint firstRowEndStreamPos = 0U;
				for (int i = 0; i < 32; i++)
				{
					int num3 = num2 * 32 + i;
					if (num3 > lastRow)
					{
						break;
					}
					bool autoSize = false;
					if (num3 < this.Rows.Count)
					{
						autoSize = this.Rows[num3].AutoSize;
					}
					RowRecord rowRecord = new RowRecord((ushort)num3, (ushort)firstCol, (ushort)(lastCol + 1), 0, false, false, autoSize);
					if (num3 < this.Rows.Count)
					{
						rowRecord.RowHeight = (ushort)this.Rows[num3].Height;
					}
					if (i == 0)
					{
						this.WriteFirstRow(stream, rowRecord, out firstRowBeginStreamPos, out firstRowEndStreamPos);
					}
					else
					{
						this.WriteRow(stream, rowRecord);
					}
				}
				MulBlankCell mulBlankCell = null;
				for (int j = 0; j < 32; j++)
				{
					int num4 = num2 * 32 + j;
					if (num4 > lastRow)
					{
						break;
					}
					for (int k = 0; k <= lastCol; k++)
					{
						Cell cell = this.CellGrid[k, num4];
						BiffCell biffCell;
						if (cell != null)
						{
							biffCell = cell.CreateBiffCell();
						}
						else
						{
							biffCell = new BlankCell();
						}
						this.WriteCell(stream, biffCell, cellAddresses, num4, k, firstRowEndStreamPos, ref mulBlankCell);
					}
					if (mulBlankCell != null)
					{
						mulBlankCell.Write(stream);
						mulBlankCell = null;
					}
				}
				this.WriteDBCell(stream, num, cellAddresses, indexRecOffsetsStart, firstRowBeginStreamPos);
				num++;
				num2++;
			}
		}

		// Token: 0x0600694D RID: 26957 RVA: 0x0018C1FC File Offset: 0x0018A3FC
		private void WriteWSBool(Stream stream)
		{
			byte[] data = new WSBool(0).GetData();
			stream.Write(data, 0, data.Length);
		}

		// Token: 0x0600694E RID: 26958 RVA: 0x0018C220 File Offset: 0x0018A420
		private List<BRK> RemoveDuplicateRowBreaks(IEnumerable<BRK> originalList)
		{
			Dictionary<int, BRK> dictionary = new Dictionary<int, BRK>();
			foreach (BRK value in originalList)
			{
				if (!dictionary.ContainsKey(value.Row))
				{
					dictionary[value.Row] = value;
				}
			}
			List<BRK> list = new List<BRK>();
			foreach (int key in dictionary.Keys)
			{
				list.Add(dictionary[key]);
			}
			return list;
		}

		// Token: 0x0600694F RID: 26959 RVA: 0x0018C2DC File Offset: 0x0018A4DC
		private void WritePageBreaks(Stream stream)
		{
			List<BRK> list = new List<BRK>();
			foreach (Range range in this.PageBreakInfos.Keys)
			{
				PageBreak pageBreak = this.PageBreakInfos[range];
				if ((pageBreak & PageBreak.Before) != PageBreak.None)
				{
					list.Add(new BRK((ushort)range.FirstRow, 0, ushort.MaxValue));
				}
				if ((pageBreak & PageBreak.After) != PageBreak.None)
				{
					list.Add(new BRK((ushort)(range.LastRow + 1), 0, ushort.MaxValue));
				}
			}
			list = this.RemoveDuplicateRowBreaks(list);
			list.Sort();
			BRK[] array = new BRK[list.Count];
			list.CopyTo(array);
			HorizontalPageBreaks horizontalPageBreaks = new HorizontalPageBreaks(array);
			byte[] data = horizontalPageBreaks.GetData();
			stream.Write(data, 0, data.Length);
		}

		// Token: 0x06006950 RID: 26960 RVA: 0x0018C3BC File Offset: 0x0018A5BC
		private void WriteColInfoRecords(Stream stream)
		{
			for (int i = 0; i < this.Columns.Count; i++)
			{
				Column column = this.Columns[i];
				ColInfo colInfo = new ColInfo((ushort)i, (ushort)i, column.Width, 0, false, false);
				byte[] data = colInfo.GetData();
				stream.Write(data, 0, data.Length);
			}
		}

		// Token: 0x06006951 RID: 26961 RVA: 0x0018C411 File Offset: 0x0018A611
		private void WriteEscher(Stream stream)
		{
			if (this.drawingContainer != null)
			{
				this.drawingContainer.WriteToStream(stream);
			}
		}

		// Token: 0x06006952 RID: 26962 RVA: 0x0018C427 File Offset: 0x0018A627
		public override string ToString()
		{
			return string.Format("{0}[{1}]", this.Name, this.SheetIndex);
		}

		// Token: 0x06006953 RID: 26963 RVA: 0x0018C444 File Offset: 0x0018A644
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06006954 RID: 26964 RVA: 0x0018C44D File Offset: 0x0018A64D
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.stream != null)
			{
				this.stream.Dispose();
			}
		}

		// Token: 0x04001C59 RID: 7257
		private string name;

		// Token: 0x04001C5A RID: 7258
		private Collection<Column> columns;

		// Token: 0x04001C5B RID: 7259
		private Collection<Row> rows;

		// Token: 0x04001C5C RID: 7260
		internal Workbook workbook;

		// Token: 0x04001C5D RID: 7261
		private Stream stream;

		// Token: 0x04001C5E RID: 7262
		private CellGrid cellGrid;

		// Token: 0x04001C5F RID: 7263
		private List<Range> mergedRanges;

		// Token: 0x04001C60 RID: 7264
		private bool landscape;

		// Token: 0x04001C61 RID: 7265
		private Margins margins;

		// Token: 0x04001C62 RID: 7266
		private Escher.DrawingContainer drawingContainer;

		// Token: 0x04001C63 RID: 7267
		private int currentSPID;

		// Token: 0x04001C64 RID: 7268
		private Dictionary<Range, PageBreak> pageBreakInfos;

		// Token: 0x04001C65 RID: 7269
		private SizeF pageSize;

		// Token: 0x04001C66 RID: 7270
		private int frozenRows;

		// Token: 0x04001C67 RID: 7271
		private string pageHeader;

		// Token: 0x04001C68 RID: 7272
		private string pageFooter;
	}
}
