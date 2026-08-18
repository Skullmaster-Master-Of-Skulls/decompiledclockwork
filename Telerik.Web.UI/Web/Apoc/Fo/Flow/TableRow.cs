using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x02001413 RID: 5139
	internal class TableRow : FObj
	{
		// Token: 0x0600D2AD RID: 53933 RVA: 0x002EBE0B File Offset: 0x002EA00B
		public new static FObj.Maker GetMaker()
		{
			return new TableRow.Maker();
		}

		// Token: 0x0600D2AE RID: 53934 RVA: 0x002EBE12 File Offset: 0x002EA012
		public TableRow(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			if (!(parent is AbstractTableBody))
			{
				throw new ApocException("A table row must be child of fo:table-body, fo:table-header or fo:table-footer, not " + parent.GetName());
			}
			this.name = "fo:table-row";
		}

		// Token: 0x0600D2AF RID: 53935 RVA: 0x002EBE45 File Offset: 0x002EA045
		public void SetColumns(ArrayList columns)
		{
			this.columns = columns;
		}

		// Token: 0x0600D2B0 RID: 53936 RVA: 0x002EBE4E File Offset: 0x002EA04E
		public KeepValue GetKeepWithPrevious()
		{
			return this.keepWithPrevious;
		}

		// Token: 0x0600D2B1 RID: 53937 RVA: 0x002EBE56 File Offset: 0x002EA056
		public KeepValue GetKeepWithNext()
		{
			return this.keepWithNext;
		}

		// Token: 0x0600D2B2 RID: 53938 RVA: 0x002EBE5E File Offset: 0x002EA05E
		public KeepValue GetKeepTogether()
		{
			return this.keepTogether;
		}

		// Token: 0x0600D2B3 RID: 53939 RVA: 0x002EBE68 File Offset: 0x002EA068
		public void DoSetup(Area area)
		{
			this.propMgr.GetAccessibilityProps();
			this.propMgr.GetAuralProps();
			this.propMgr.GetBorderAndPadding();
			this.propMgr.GetBackgroundProps();
			this.propMgr.GetRelativePositionProps();
			this.breakAfter = this.properties.GetProperty("break-after").GetEnum();
			this.keepTogether = this.getKeepValue("keep-together.within-column");
			this.keepWithNext = this.getKeepValue("keep-with-next.within-column");
			this.keepWithPrevious = this.getKeepValue("keep-with-previous.within-column");
			this.id = this.properties.GetProperty("id").GetString();
			this.minHeight = this.properties.GetProperty("height").GetLength().MValue();
			this.setup = true;
		}

		// Token: 0x0600D2B4 RID: 53940 RVA: 0x002EBF44 File Offset: 0x002EA144
		private KeepValue getKeepValue(string sPropName)
		{
			Property property = this.properties.GetProperty(sPropName);
			Number number = property.GetNumber();
			if (number != null)
			{
				return new KeepValue("KEEP_WITH_VALUE", number.IntValue());
			}
			switch (property.GetEnum())
			{
			case 5:
				return new KeepValue("KEEP_WITH_ALWAYS", 0);
			}
			return new KeepValue("KEEP_WITH_AUTO", 0);
		}

		// Token: 0x0600D2B5 RID: 53941 RVA: 0x002EBFB0 File Offset: 0x002EA1B0
		public override Status Layout(Area area)
		{
			if (this.marker == -1001)
			{
				return new Status(1);
			}
			if (this.marker == -1000)
			{
				if (!this.setup)
				{
					this.DoSetup(area);
				}
				if (this.cellArray == null)
				{
					this.InitCellArray();
					area.getIDReferences().CreateID(this.id);
				}
				this.marker = 0;
				int num = this.propMgr.CheckBreakBefore(area);
				if (num != 1)
				{
					return new Status(num);
				}
			}
			if (this.marker == 0)
			{
				area.getIDReferences().ConfigureID(this.id, area);
			}
			int num2 = area.spaceLeft();
			this.areaContainer = new AreaContainer(this.propMgr.GetFontState(area.getFontInfo()), 0, 0, area.getContentWidth(), num2, 61);
			this.areaContainer.foCreator = this;
			this.areaContainer.setPage(area.getPage());
			this.areaContainer.setParent(area);
			this.areaContainer.setBackground(this.propMgr.GetBackgroundProps());
			this.areaContainer.start();
			this.areaContainer.setAbsoluteHeight(area.getAbsoluteHeight());
			this.areaContainer.setIDReferences(area.getIDReferences());
			this.largestCellHeight = this.minHeight;
			bool flag = false;
			int num3 = 0;
			int num4 = 0;
			foreach (object obj in this.columns)
			{
				TableColumn tableColumn = (TableColumn)obj;
				num4++;
				int columnWidth = tableColumn.GetColumnWidth();
				if (this.cellArray.GetCellType(num4) == 1)
				{
					TableCell cell = this.cellArray.GetCell(num4);
					cell.SetStartOffset(num3);
					num3 += columnWidth;
					int numRowsSpanned = cell.GetNumRowsSpanned();
					Status status2;
					Status status = status2 = cell.Layout(this.areaContainer);
					if (status2.isIncomplete())
					{
						if (this.keepTogether.GetKeepType() == "KEEP_WITH_ALWAYS" || status.getCode() == 2 || numRowsSpanned > 1)
						{
							this.ResetMarker();
							this.RemoveID(area.getIDReferences());
							return new Status(2);
						}
						if (status.getCode() == 3)
						{
							flag = true;
						}
					}
					int height = cell.GetHeight();
					if (numRowsSpanned > 1)
					{
						this.rowSpanMgr.AddRowSpan(cell, num4, cell.GetNumColumnsSpanned(), height, numRowsSpanned);
					}
					else if (height > this.largestCellHeight)
					{
						this.largestCellHeight = height;
					}
				}
				else
				{
					if (this.rowSpanMgr.IsInLastRow(num4))
					{
						int remainingHeight = this.rowSpanMgr.GetRemainingHeight(num4);
						if (remainingHeight > this.largestCellHeight)
						{
							this.largestCellHeight = remainingHeight;
						}
					}
					num3 += columnWidth;
				}
			}
			area.setMaxHeight(area.getMaxHeight() - num2 + this.areaContainer.getMaxHeight());
			for (int i = 1; i <= this.columns.Count; i++)
			{
				if (this.cellArray.GetCellType(i) == 1 && !this.rowSpanMgr.IsSpanned(i))
				{
					this.cellArray.GetCell(i).SetRowHeight(this.largestCellHeight);
				}
			}
			this.rowSpanMgr.FinishRow(this.largestCellHeight);
			area.addChild(this.areaContainer);
			this.areaContainer.SetHeight(this.largestCellHeight);
			this.areaAdded = true;
			this.areaContainer.end();
			area.addDisplaySpace(this.largestCellHeight + this.areaContainer.getPaddingTop() + this.areaContainer.getBorderTopWidth() + this.areaContainer.getPaddingBottom() + this.areaContainer.getBorderBottomWidth());
			if (flag)
			{
				return new Status(3);
			}
			if (this.rowSpanMgr.HasUnfinishedSpans())
			{
				return new Status(8);
			}
			if (this.breakAfter == 58)
			{
				this.marker = -1001;
				return new Status(4);
			}
			if (this.breakAfter == 55)
			{
				this.marker = -1001;
				return new Status(6);
			}
			if (this.breakAfter == 26)
			{
				this.marker = -1001;
				return new Status(5);
			}
			if (this.breakAfter == 15)
			{
				this.marker = -1001;
				return new Status(7);
			}
			if (this.keepWithNext.GetKeepType() != "KEEP_WITH_AUTO")
			{
				return new Status(8);
			}
			return new Status(1);
		}

		// Token: 0x0600D2B6 RID: 53942 RVA: 0x002EC418 File Offset: 0x002EA618
		public int GetAreaHeight()
		{
			return this.areaContainer.GetHeight();
		}

		// Token: 0x0600D2B7 RID: 53943 RVA: 0x002EC425 File Offset: 0x002EA625
		public void RemoveLayout(Area area)
		{
			if (this.areaAdded)
			{
				area.removeChild(this.areaContainer);
			}
			this.areaAdded = false;
			this.ResetMarker();
			this.RemoveID(area.getIDReferences());
		}

		// Token: 0x0600D2B8 RID: 53944 RVA: 0x002EC454 File Offset: 0x002EA654
		public new void ResetMarker()
		{
			base.ResetMarker();
		}

		// Token: 0x0600D2B9 RID: 53945 RVA: 0x002EC45C File Offset: 0x002EA65C
		public void SetRowSpanMgr(RowSpanMgr rowSpanMgr)
		{
			this.rowSpanMgr = rowSpanMgr;
		}

		// Token: 0x0600D2BA RID: 53946 RVA: 0x002EC468 File Offset: 0x002EA668
		private void InitCellArray()
		{
			this.cellArray = new TableRow.CellArray(this.rowSpanMgr, this.columns.Count);
			int num = 1;
			foreach (object obj in this.children)
			{
				TableCell tableCell = (TableCell)obj;
				num = this.cellArray.GetNextFreeCell(num);
				int num2 = tableCell.GetNumColumnsSpanned();
				tableCell.GetNumRowsSpanned();
				int num3 = tableCell.GetColumnNumber();
				if (num3 == 0)
				{
					if (num < 1)
					{
						continue;
					}
					num3 = num;
				}
				else if (num3 > this.columns.Count)
				{
					continue;
				}
				if (num3 + num2 - 1 > this.columns.Count)
				{
					num2 = this.columns.Count - num3 + 1;
				}
				this.cellArray.StoreCell(tableCell, num3, num2);
				if (num3 > num)
				{
					num = num3;
				}
				else if (num3 < num)
				{
					num = num3;
				}
				int cellWidth = this.GetCellWidth(num3, num2);
				tableCell.SetWidth(cellWidth);
				num += num2;
			}
		}

		// Token: 0x0600D2BB RID: 53947 RVA: 0x002EC578 File Offset: 0x002EA778
		private int GetCellWidth(int startCol, int numCols)
		{
			int num = 0;
			for (int i = 0; i < numCols; i++)
			{
				num += ((TableColumn)this.columns[startCol + i - 1]).GetColumnWidth();
			}
			return num;
		}

		// Token: 0x0600D2BC RID: 53948 RVA: 0x002EC5B1 File Offset: 0x002EA7B1
		internal void setIgnoreKeepTogether(bool bIgnoreKeepTogether)
		{
			this.bIgnoreKeepTogether = bIgnoreKeepTogether;
		}

		// Token: 0x040038F5 RID: 14581
		private bool setup;

		// Token: 0x040038F6 RID: 14582
		private int breakAfter;

		// Token: 0x040038F7 RID: 14583
		private string id;

		// Token: 0x040038F8 RID: 14584
		private KeepValue keepWithNext;

		// Token: 0x040038F9 RID: 14585
		private KeepValue keepWithPrevious;

		// Token: 0x040038FA RID: 14586
		private KeepValue keepTogether;

		// Token: 0x040038FB RID: 14587
		private int largestCellHeight;

		// Token: 0x040038FC RID: 14588
		private int minHeight;

		// Token: 0x040038FD RID: 14589
		private ArrayList columns;

		// Token: 0x040038FE RID: 14590
		private AreaContainer areaContainer;

		// Token: 0x040038FF RID: 14591
		private bool areaAdded;

		// Token: 0x04003900 RID: 14592
		private bool bIgnoreKeepTogether;

		// Token: 0x04003901 RID: 14593
		private RowSpanMgr rowSpanMgr;

		// Token: 0x04003902 RID: 14594
		private TableRow.CellArray cellArray;

		// Token: 0x02001414 RID: 5140
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D2BD RID: 53949 RVA: 0x002EC5BA File Offset: 0x002EA7BA
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new TableRow(parent, propertyList);
			}
		}

		// Token: 0x02001415 RID: 5141
		private class CellArray
		{
			// Token: 0x0600D2BF RID: 53951 RVA: 0x002EC5CC File Offset: 0x002EA7CC
			internal CellArray(RowSpanMgr rsi, int numColumns)
			{
				this.cells = new TableCell[numColumns];
				this.states = new byte[numColumns];
				for (int i = 0; i < numColumns; i++)
				{
					if (rsi.IsSpanned(i + 1))
					{
						this.cells[i] = rsi.GetSpanningCell(i + 1);
						this.states[i] = 2;
					}
					else
					{
						this.states[i] = 0;
					}
				}
			}

			// Token: 0x0600D2C0 RID: 53952 RVA: 0x002EC634 File Offset: 0x002EA834
			[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
			internal int GetNextFreeCell(int colNum)
			{
				for (int i = colNum - 1; i < this.states.Length; i++)
				{
					if (this.states[i] == 0)
					{
						return i + 1;
					}
				}
				return -1;
			}

			// Token: 0x0600D2C1 RID: 53953 RVA: 0x002EC665 File Offset: 0x002EA865
			internal int GetCellType(int colNum)
			{
				if (colNum > 0 && colNum <= this.cells.Length)
				{
					return (int)this.states[colNum - 1];
				}
				return -1;
			}

			// Token: 0x0600D2C2 RID: 53954 RVA: 0x002EC682 File Offset: 0x002EA882
			internal TableCell GetCell(int colNum)
			{
				if (colNum > 0 && colNum <= this.cells.Length)
				{
					return this.cells[colNum - 1];
				}
				return null;
			}

			// Token: 0x0600D2C3 RID: 53955 RVA: 0x002EC6A0 File Offset: 0x002EA8A0
			[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
			internal bool StoreCell(TableCell cell, int colNum, int numCols)
			{
				bool result = true;
				int num = colNum - 1;
				int num2 = 0;
				while (num < this.cells.Length && num2 < numCols)
				{
					if (this.cells[num] == null)
					{
						this.cells[num] = cell;
						this.states[num] = ((num2 == 0) ? 1 : 2);
					}
					else
					{
						result = false;
					}
					num2++;
					num++;
				}
				return result;
			}

			// Token: 0x04003903 RID: 14595
			public const byte EMPTY = 0;

			// Token: 0x04003904 RID: 14596
			public const byte CELLSTART = 1;

			// Token: 0x04003905 RID: 14597
			public const byte CELLSPAN = 2;

			// Token: 0x04003906 RID: 14598
			private TableCell[] cells;

			// Token: 0x04003907 RID: 14599
			private byte[] states;
		}
	}
}
