using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms.Layout
{
	// Token: 0x020004D2 RID: 1234
	internal class TableLayout : LayoutEngine
	{
		// Token: 0x060050E1 RID: 20705 RVA: 0x00150971 File Offset: 0x0014EB71
		private static int GetMedian(int low, int hi)
		{
			return low + (hi - low >> 1);
		}

		// Token: 0x060050E2 RID: 20706 RVA: 0x0015097C File Offset: 0x0014EB7C
		private static void Sort(object[] array, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length > 1)
			{
				TableLayout.SorterObjectArray sorterObjectArray = new TableLayout.SorterObjectArray(array, comparer);
				sorterObjectArray.QuickSort(0, array.Length - 1);
			}
		}

		// Token: 0x060050E3 RID: 20707 RVA: 0x001509B3 File Offset: 0x0014EBB3
		internal static TableLayoutSettings CreateSettings(IArrangedElement owner)
		{
			return new TableLayoutSettings(owner);
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x001509BC File Offset: 0x0014EBBC
		internal override void ProcessSuspendedLayoutEventArgs(IArrangedElement container, LayoutEventArgs args)
		{
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(container);
			foreach (string text in TableLayout._propertiesWhichInvalidateCache)
			{
				if (args.AffectedProperty == text)
				{
					TableLayout.ClearCachedAssignments(containerInfo);
					return;
				}
			}
		}

		// Token: 0x060050E5 RID: 20709 RVA: 0x001509F8 File Offset: 0x0014EBF8
		internal override bool LayoutCore(IArrangedElement container, LayoutEventArgs args)
		{
			this.ProcessSuspendedLayoutEventArgs(container, args);
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(container);
			this.EnsureRowAndColumnAssignments(container, containerInfo, false);
			int cellBorderWidth = containerInfo.CellBorderWidth;
			Size size = container.DisplayRectangle.Size - new Size(cellBorderWidth, cellBorderWidth);
			size.Width = Math.Max(size.Width, 1);
			size.Height = Math.Max(size.Height, 1);
			Size usedSpace = this.ApplyStyles(containerInfo, size, false);
			this.ExpandLastElement(containerInfo, usedSpace, size);
			RectangleF displayRectF = container.DisplayRectangle;
			displayRectF.Inflate(-((float)cellBorderWidth / 2f), (float)(-(float)cellBorderWidth) / 2f);
			this.SetElementBounds(containerInfo, displayRectF);
			CommonProperties.SetLayoutBounds(containerInfo.Container, new Size(this.SumStrips(containerInfo.Columns, 0, containerInfo.Columns.Length), this.SumStrips(containerInfo.Rows, 0, containerInfo.Rows.Length)));
			return CommonProperties.GetAutoSize(container);
		}

		// Token: 0x060050E6 RID: 20710 RVA: 0x00150AEC File Offset: 0x0014ECEC
		internal override Size GetPreferredSize(IArrangedElement container, Size proposedConstraints)
		{
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(container);
			bool flag = false;
			float num = -1f;
			Size size = containerInfo.GetCachedPreferredSize(proposedConstraints, out flag);
			if (flag)
			{
				return size;
			}
			TableLayout.ContainerInfo containerInfo2 = new TableLayout.ContainerInfo(containerInfo);
			int cellBorderWidth = containerInfo.CellBorderWidth;
			if (containerInfo.MaxColumns == 1 && containerInfo.ColumnStyles.Count > 0 && containerInfo.ColumnStyles[0].SizeType == SizeType.Absolute)
			{
				Size size2 = container.DisplayRectangle.Size - new Size(cellBorderWidth * 2, cellBorderWidth * 2);
				size2.Width = Math.Max(size2.Width, 1);
				size2.Height = Math.Max(size2.Height, 1);
				num = containerInfo.ColumnStyles[0].Size;
				containerInfo.ColumnStyles[0].SetSize(Math.Max(num, (float)Math.Min(proposedConstraints.Width, size2.Width)));
			}
			this.EnsureRowAndColumnAssignments(container, containerInfo2, true);
			Size sz = new Size(cellBorderWidth, cellBorderWidth);
			proposedConstraints -= sz;
			proposedConstraints.Width = Math.Max(proposedConstraints.Width, 1);
			proposedConstraints.Height = Math.Max(proposedConstraints.Height, 1);
			if (containerInfo2.Columns != null && containerInfo.Columns != null && containerInfo2.Columns.Length != containerInfo.Columns.Length)
			{
				TableLayout.ClearCachedAssignments(containerInfo);
			}
			if (containerInfo2.Rows != null && containerInfo.Rows != null && containerInfo2.Rows.Length != containerInfo.Rows.Length)
			{
				TableLayout.ClearCachedAssignments(containerInfo);
			}
			size = this.ApplyStyles(containerInfo2, proposedConstraints, true);
			if (num >= 0f)
			{
				containerInfo.ColumnStyles[0].SetSize(num);
			}
			return size + sz;
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x00150CAB File Offset: 0x0014EEAB
		private void EnsureRowAndColumnAssignments(IArrangedElement container, TableLayout.ContainerInfo containerInfo, bool doNotCache)
		{
			if (!TableLayout.HasCachedAssignments(containerInfo) || doNotCache)
			{
				this.AssignRowsAndColumns(containerInfo);
			}
		}

		// Token: 0x060050E8 RID: 20712 RVA: 0x00150CC4 File Offset: 0x0014EEC4
		private void ExpandLastElement(TableLayout.ContainerInfo containerInfo, Size usedSpace, Size totalSpace)
		{
			TableLayout.Strip[] rows = containerInfo.Rows;
			TableLayout.Strip[] columns = containerInfo.Columns;
			if (columns.Length != 0 && totalSpace.Width > usedSpace.Width)
			{
				TableLayout.Strip[] array = columns;
				int num = columns.Length - 1;
				array[num].MinSize = array[num].MinSize + (totalSpace.Width - usedSpace.Width);
			}
			if (rows.Length != 0 && totalSpace.Height > usedSpace.Height)
			{
				TableLayout.Strip[] array2 = rows;
				int num2 = rows.Length - 1;
				array2[num2].MinSize = array2[num2].MinSize + (totalSpace.Height - usedSpace.Height);
			}
		}

		// Token: 0x060050E9 RID: 20713 RVA: 0x00150D54 File Offset: 0x0014EF54
		private void AssignRowsAndColumns(TableLayout.ContainerInfo containerInfo)
		{
			int num = containerInfo.MaxColumns;
			int num2 = containerInfo.MaxRows;
			TableLayout.LayoutInfo[] childrenInfo = containerInfo.ChildrenInfo;
			int minRowsAndColumns = containerInfo.MinRowsAndColumns;
			int minColumns = containerInfo.MinColumns;
			int minRows = containerInfo.MinRows;
			TableLayoutPanelGrowStyle growStyle = containerInfo.GrowStyle;
			if (growStyle == TableLayoutPanelGrowStyle.FixedSize)
			{
				if (containerInfo.MinRowsAndColumns > num * num2)
				{
					throw new ArgumentException(SR.GetString("TableLayoutPanelFullDesc"));
				}
				if (minColumns > num || minRows > num2)
				{
					throw new ArgumentException(SR.GetString("TableLayoutPanelSpanDesc"));
				}
				num2 = Math.Max(1, num2);
				num = Math.Max(1, num);
			}
			else if (growStyle == TableLayoutPanelGrowStyle.AddRows)
			{
				num2 = 0;
			}
			else
			{
				num = 0;
			}
			if (num > 0)
			{
				this.xAssignRowsAndColumns(containerInfo, childrenInfo, num, (num2 == 0) ? int.MaxValue : num2, growStyle);
				return;
			}
			if (num2 > 0)
			{
				int num3 = Math.Max((int)Math.Ceiling((double)((float)minRowsAndColumns / (float)num2)), minColumns);
				num3 = Math.Max(num3, 1);
				while (!this.xAssignRowsAndColumns(containerInfo, childrenInfo, num3, num2, growStyle))
				{
					num3++;
				}
				return;
			}
			this.xAssignRowsAndColumns(containerInfo, childrenInfo, Math.Max(minColumns, 1), int.MaxValue, growStyle);
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x00150E5C File Offset: 0x0014F05C
		private bool xAssignRowsAndColumns(TableLayout.ContainerInfo containerInfo, TableLayout.LayoutInfo[] childrenInfo, int maxColumns, int maxRows, TableLayoutPanelGrowStyle growStyle)
		{
			int num = 0;
			int num2 = 0;
			TableLayout.ReservationGrid reservationGrid = new TableLayout.ReservationGrid();
			int num3 = 0;
			int num4 = 0;
			int num5 = -1;
			int num6 = -1;
			TableLayout.LayoutInfo[] fixedChildrenInfo = containerInfo.FixedChildrenInfo;
			TableLayout.LayoutInfo nextLayoutInfo = TableLayout.GetNextLayoutInfo(fixedChildrenInfo, ref num5, true);
			TableLayout.LayoutInfo nextLayoutInfo2 = TableLayout.GetNextLayoutInfo(childrenInfo, ref num6, false);
			while (nextLayoutInfo != null || nextLayoutInfo2 != null)
			{
				int num7 = num4;
				if (nextLayoutInfo2 != null)
				{
					nextLayoutInfo2.RowStart = num3;
					nextLayoutInfo2.ColumnStart = num4;
					this.AdvanceUntilFits(maxColumns, reservationGrid, nextLayoutInfo2, out num7);
					if (nextLayoutInfo2.RowStart >= maxRows)
					{
						return false;
					}
				}
				int num8;
				if (nextLayoutInfo2 != null && (nextLayoutInfo == null || (!this.IsCursorPastInsertionPoint(nextLayoutInfo, nextLayoutInfo2.RowStart, num7) && !this.IsOverlappingWithReservationGrid(nextLayoutInfo, reservationGrid, num3))))
				{
					for (int i = 0; i < nextLayoutInfo2.RowStart - num3; i++)
					{
						reservationGrid.AdvanceRow();
					}
					num3 = nextLayoutInfo2.RowStart;
					num8 = Math.Min(num3 + nextLayoutInfo2.RowSpan, maxRows);
					reservationGrid.ReserveAll(nextLayoutInfo2, num8, num7);
					nextLayoutInfo2 = TableLayout.GetNextLayoutInfo(childrenInfo, ref num6, false);
				}
				else
				{
					if (num4 >= maxColumns)
					{
						num4 = 0;
						num3++;
						reservationGrid.AdvanceRow();
					}
					nextLayoutInfo.RowStart = Math.Min(nextLayoutInfo.RowPosition, maxRows - 1);
					nextLayoutInfo.ColumnStart = Math.Min(nextLayoutInfo.ColumnPosition, maxColumns - 1);
					if (num3 > nextLayoutInfo.RowStart)
					{
						nextLayoutInfo.ColumnStart = num4;
					}
					else if (num3 == nextLayoutInfo.RowStart)
					{
						nextLayoutInfo.ColumnStart = Math.Max(nextLayoutInfo.ColumnStart, num4);
					}
					nextLayoutInfo.RowStart = Math.Max(nextLayoutInfo.RowStart, num3);
					int j;
					for (j = 0; j < nextLayoutInfo.RowStart - num3; j++)
					{
						reservationGrid.AdvanceRow();
					}
					this.AdvanceUntilFits(maxColumns, reservationGrid, nextLayoutInfo, out num7);
					if (nextLayoutInfo.RowStart >= maxRows)
					{
						return false;
					}
					while (j < nextLayoutInfo.RowStart - num3)
					{
						reservationGrid.AdvanceRow();
						j++;
					}
					num3 = nextLayoutInfo.RowStart;
					num7 = Math.Min(nextLayoutInfo.ColumnStart + nextLayoutInfo.ColumnSpan, maxColumns);
					num8 = Math.Min(nextLayoutInfo.RowStart + nextLayoutInfo.RowSpan, maxRows);
					reservationGrid.ReserveAll(nextLayoutInfo, num8, num7);
					nextLayoutInfo = TableLayout.GetNextLayoutInfo(fixedChildrenInfo, ref num5, true);
				}
				num4 = num7;
				num2 = ((num2 == int.MaxValue) ? num8 : Math.Max(num2, num8));
				num = ((num == int.MaxValue) ? num7 : Math.Max(num, num7));
			}
			if (growStyle == TableLayoutPanelGrowStyle.FixedSize)
			{
				num = maxColumns;
				num2 = maxRows;
			}
			else if (growStyle == TableLayoutPanelGrowStyle.AddRows)
			{
				num = maxColumns;
				num2 = Math.Max(containerInfo.MaxRows, num2);
			}
			else
			{
				num2 = ((maxRows == int.MaxValue) ? num2 : maxRows);
				num = Math.Max(containerInfo.MaxColumns, num);
			}
			if (containerInfo.Rows == null || containerInfo.Rows.Length != num2)
			{
				containerInfo.Rows = new TableLayout.Strip[num2];
			}
			if (containerInfo.Columns == null || containerInfo.Columns.Length != num)
			{
				containerInfo.Columns = new TableLayout.Strip[num];
			}
			containerInfo.Valid = true;
			return true;
		}

		// Token: 0x060050EB RID: 20715 RVA: 0x00151134 File Offset: 0x0014F334
		private static TableLayout.LayoutInfo GetNextLayoutInfo(TableLayout.LayoutInfo[] layoutInfo, ref int index, bool absolutelyPositioned)
		{
			int num = index + 1;
			index = num;
			for (int i = num; i < layoutInfo.Length; i++)
			{
				if (absolutelyPositioned == layoutInfo[i].IsAbsolutelyPositioned)
				{
					index = i;
					return layoutInfo[i];
				}
			}
			index = layoutInfo.Length;
			return null;
		}

		// Token: 0x060050EC RID: 20716 RVA: 0x0015116F File Offset: 0x0014F36F
		private bool IsCursorPastInsertionPoint(TableLayout.LayoutInfo fixedLayoutInfo, int insertionRow, int insertionCol)
		{
			return fixedLayoutInfo.RowPosition < insertionRow || (fixedLayoutInfo.RowPosition == insertionRow && fixedLayoutInfo.ColumnPosition < insertionCol);
		}

		// Token: 0x060050ED RID: 20717 RVA: 0x00151194 File Offset: 0x0014F394
		private bool IsOverlappingWithReservationGrid(TableLayout.LayoutInfo fixedLayoutInfo, TableLayout.ReservationGrid reservationGrid, int currentRow)
		{
			if (fixedLayoutInfo.RowPosition < currentRow)
			{
				return true;
			}
			for (int i = fixedLayoutInfo.RowPosition - currentRow; i < fixedLayoutInfo.RowPosition - currentRow + fixedLayoutInfo.RowSpan; i++)
			{
				for (int j = fixedLayoutInfo.ColumnPosition; j < fixedLayoutInfo.ColumnPosition + fixedLayoutInfo.ColumnSpan; j++)
				{
					if (reservationGrid.IsReserved(j, i))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060050EE RID: 20718 RVA: 0x001511F8 File Offset: 0x0014F3F8
		private void AdvanceUntilFits(int maxColumns, TableLayout.ReservationGrid reservationGrid, TableLayout.LayoutInfo layoutInfo, out int colStop)
		{
			int rowStart = layoutInfo.RowStart;
			do
			{
				this.GetColStartAndStop(maxColumns, reservationGrid, layoutInfo, out colStop);
			}
			while (this.ScanRowForOverlap(maxColumns, reservationGrid, layoutInfo, colStop, layoutInfo.RowStart - rowStart));
		}

		// Token: 0x060050EF RID: 20719 RVA: 0x00151230 File Offset: 0x0014F430
		private void GetColStartAndStop(int maxColumns, TableLayout.ReservationGrid reservationGrid, TableLayout.LayoutInfo layoutInfo, out int colStop)
		{
			colStop = layoutInfo.ColumnStart + layoutInfo.ColumnSpan;
			if (colStop > maxColumns)
			{
				if (layoutInfo.ColumnStart != 0)
				{
					layoutInfo.ColumnStart = 0;
					int rowStart = layoutInfo.RowStart;
					layoutInfo.RowStart = rowStart + 1;
				}
				colStop = Math.Min(layoutInfo.ColumnSpan, maxColumns);
			}
		}

		// Token: 0x060050F0 RID: 20720 RVA: 0x00151284 File Offset: 0x0014F484
		private bool ScanRowForOverlap(int maxColumns, TableLayout.ReservationGrid reservationGrid, TableLayout.LayoutInfo layoutInfo, int stopCol, int rowOffset)
		{
			for (int i = layoutInfo.ColumnStart; i < stopCol; i++)
			{
				if (reservationGrid.IsReserved(i, rowOffset))
				{
					layoutInfo.ColumnStart = i + 1;
					while (layoutInfo.ColumnStart < maxColumns && reservationGrid.IsReserved(layoutInfo.ColumnStart, rowOffset))
					{
						int columnStart = layoutInfo.ColumnStart;
						layoutInfo.ColumnStart = columnStart + 1;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060050F1 RID: 20721 RVA: 0x001512E8 File Offset: 0x0014F4E8
		private Size ApplyStyles(TableLayout.ContainerInfo containerInfo, Size proposedConstraints, bool measureOnly)
		{
			Size empty = Size.Empty;
			this.InitializeStrips(containerInfo.Columns, containerInfo.ColumnStyles);
			this.InitializeStrips(containerInfo.Rows, containerInfo.RowStyles);
			containerInfo.ChildHasColumnSpan = false;
			containerInfo.ChildHasRowSpan = false;
			foreach (TableLayout.LayoutInfo layoutInfo in containerInfo.ChildrenInfo)
			{
				containerInfo.Columns[layoutInfo.ColumnStart].IsStart = true;
				containerInfo.Rows[layoutInfo.RowStart].IsStart = true;
				if (layoutInfo.ColumnSpan > 1)
				{
					containerInfo.ChildHasColumnSpan = true;
				}
				if (layoutInfo.RowSpan > 1)
				{
					containerInfo.ChildHasRowSpan = true;
				}
			}
			empty.Width = this.InflateColumns(containerInfo, proposedConstraints, measureOnly);
			int expandLastElementWidth = Math.Max(0, proposedConstraints.Width - empty.Width);
			empty.Height = this.InflateRows(containerInfo, proposedConstraints, expandLastElementWidth, measureOnly);
			return empty;
		}

		// Token: 0x060050F2 RID: 20722 RVA: 0x001513D4 File Offset: 0x0014F5D4
		private void InitializeStrips(TableLayout.Strip[] strips, IList styles)
		{
			for (int i = 0; i < strips.Length; i++)
			{
				TableLayoutStyle tableLayoutStyle = (i < styles.Count) ? ((TableLayoutStyle)styles[i]) : null;
				TableLayout.Strip strip = strips[i];
				if (tableLayoutStyle != null && tableLayoutStyle.SizeType == SizeType.Absolute)
				{
					strip.MinSize = (int)Math.Round((double)((TableLayoutStyle)styles[i]).Size);
					strip.MaxSize = strip.MinSize;
				}
				else
				{
					strip.MinSize = 0;
					strip.MaxSize = 0;
				}
				strip.IsStart = false;
				strips[i] = strip;
			}
		}

		// Token: 0x060050F3 RID: 20723 RVA: 0x00151474 File Offset: 0x0014F674
		private int InflateColumns(TableLayout.ContainerInfo containerInfo, Size proposedConstraints, bool measureOnly)
		{
			bool flag = measureOnly;
			TableLayout.LayoutInfo[] childrenInfo = containerInfo.ChildrenInfo;
			if (containerInfo.ChildHasColumnSpan)
			{
				object[] array = childrenInfo;
				TableLayout.Sort(array, TableLayout.ColumnSpanComparer.GetInstance);
			}
			if (flag && proposedConstraints.Width < 32767)
			{
				TableLayoutPanel tableLayoutPanel = containerInfo.Container as TableLayoutPanel;
				if (tableLayoutPanel != null && tableLayoutPanel.ParentInternal != null && tableLayoutPanel.ParentInternal.LayoutEngine == DefaultLayout.Instance)
				{
					if (tableLayoutPanel.Dock == DockStyle.Top || tableLayoutPanel.Dock == DockStyle.Bottom || tableLayoutPanel.Dock == DockStyle.Fill)
					{
						flag = false;
					}
					if ((tableLayoutPanel.Anchor & (AnchorStyles.Left | AnchorStyles.Right)) == (AnchorStyles.Left | AnchorStyles.Right))
					{
						flag = false;
					}
				}
			}
			foreach (TableLayout.LayoutInfo layoutInfo in childrenInfo)
			{
				IArrangedElement element = layoutInfo.Element;
				int columnSpan = layoutInfo.ColumnSpan;
				if (columnSpan > 1 || !this.IsAbsolutelySized(layoutInfo.ColumnStart, containerInfo.ColumnStyles))
				{
					int num;
					int num2;
					if (columnSpan == 1 && layoutInfo.RowSpan == 1 && this.IsAbsolutelySized(layoutInfo.RowStart, containerInfo.RowStyles))
					{
						int height = (int)containerInfo.RowStyles[layoutInfo.RowStart].Size;
						num = this.GetElementSize(element, new Size(0, height)).Width;
						num2 = num;
					}
					else
					{
						num = this.GetElementSize(element, new Size(1, 0)).Width;
						num2 = this.GetElementSize(element, Size.Empty).Width;
					}
					Padding margin = CommonProperties.GetMargin(element);
					num += margin.Horizontal;
					num2 += margin.Horizontal;
					int stop = Math.Min(layoutInfo.ColumnStart + layoutInfo.ColumnSpan, containerInfo.Columns.Length);
					this.DistributeSize(containerInfo.ColumnStyles, containerInfo.Columns, layoutInfo.ColumnStart, stop, num, num2, containerInfo.CellBorderWidth);
				}
			}
			int num3 = this.DistributeStyles(containerInfo.CellBorderWidth, containerInfo.ColumnStyles, containerInfo.Columns, proposedConstraints.Width, flag);
			if (flag && num3 > proposedConstraints.Width && proposedConstraints.Width > 1)
			{
				TableLayout.Strip[] columns = containerInfo.Columns;
				float num4 = 0f;
				int num5 = 0;
				TableLayoutStyleCollection columnStyles = containerInfo.ColumnStyles;
				for (int j = 0; j < columns.Length; j++)
				{
					TableLayout.Strip strip = columns[j];
					if (j < columnStyles.Count)
					{
						TableLayoutStyle tableLayoutStyle = columnStyles[j];
						if (tableLayoutStyle.SizeType == SizeType.Percent)
						{
							num4 += tableLayoutStyle.Size;
							num5 += strip.MinSize;
						}
					}
				}
				int val = num3 - proposedConstraints.Width;
				int num6 = Math.Min(val, num5);
				for (int k = 0; k < columns.Length; k++)
				{
					if (k < columnStyles.Count)
					{
						TableLayoutStyle tableLayoutStyle2 = columnStyles[k];
						if (tableLayoutStyle2.SizeType == SizeType.Percent)
						{
							float num7 = tableLayoutStyle2.Size / num4;
							TableLayout.Strip[] array3 = columns;
							int num8 = k;
							array3[num8].MinSize = array3[num8].MinSize - (int)(num7 * (float)num6);
						}
					}
				}
				return num3 - num6;
			}
			return num3;
		}

		// Token: 0x060050F4 RID: 20724 RVA: 0x00151784 File Offset: 0x0014F984
		private int InflateRows(TableLayout.ContainerInfo containerInfo, Size proposedConstraints, int expandLastElementWidth, bool measureOnly)
		{
			bool flag = measureOnly;
			TableLayout.LayoutInfo[] childrenInfo = containerInfo.ChildrenInfo;
			if (containerInfo.ChildHasRowSpan)
			{
				object[] array = childrenInfo;
				TableLayout.Sort(array, TableLayout.RowSpanComparer.GetInstance);
			}
			bool hasMultiplePercentColumns = containerInfo.HasMultiplePercentColumns;
			if (flag && proposedConstraints.Height < 32767)
			{
				TableLayoutPanel tableLayoutPanel = containerInfo.Container as TableLayoutPanel;
				if (tableLayoutPanel != null && tableLayoutPanel.ParentInternal != null && tableLayoutPanel.ParentInternal.LayoutEngine == DefaultLayout.Instance)
				{
					if (tableLayoutPanel.Dock == DockStyle.Left || tableLayoutPanel.Dock == DockStyle.Right || tableLayoutPanel.Dock == DockStyle.Fill)
					{
						flag = false;
					}
					if ((tableLayoutPanel.Anchor & (AnchorStyles.Top | AnchorStyles.Bottom)) == (AnchorStyles.Top | AnchorStyles.Bottom))
					{
						flag = false;
					}
				}
			}
			foreach (TableLayout.LayoutInfo layoutInfo in childrenInfo)
			{
				IArrangedElement element = layoutInfo.Element;
				int rowSpan = layoutInfo.RowSpan;
				if (rowSpan > 1 || !this.IsAbsolutelySized(layoutInfo.RowStart, containerInfo.RowStyles))
				{
					int num = this.SumStrips(containerInfo.Columns, layoutInfo.ColumnStart, layoutInfo.ColumnSpan);
					if (!flag && layoutInfo.ColumnStart + layoutInfo.ColumnSpan >= containerInfo.MaxColumns && !hasMultiplePercentColumns)
					{
						num += expandLastElementWidth;
					}
					Padding margin = CommonProperties.GetMargin(element);
					int num2 = this.GetElementSize(element, new Size(num - margin.Horizontal, 0)).Height + margin.Vertical;
					int max = num2;
					int stop = Math.Min(layoutInfo.RowStart + layoutInfo.RowSpan, containerInfo.Rows.Length);
					this.DistributeSize(containerInfo.RowStyles, containerInfo.Rows, layoutInfo.RowStart, stop, num2, max, containerInfo.CellBorderWidth);
				}
			}
			return this.DistributeStyles(containerInfo.CellBorderWidth, containerInfo.RowStyles, containerInfo.Rows, proposedConstraints.Height, flag);
		}

		// Token: 0x060050F5 RID: 20725 RVA: 0x00151950 File Offset: 0x0014FB50
		private Size GetElementSize(IArrangedElement element, Size proposedConstraints)
		{
			if (CommonProperties.GetAutoSize(element))
			{
				return element.GetPreferredSize(proposedConstraints);
			}
			return CommonProperties.GetSpecifiedBounds(element).Size;
		}

		// Token: 0x060050F6 RID: 20726 RVA: 0x0015197C File Offset: 0x0014FB7C
		internal int SumStrips(TableLayout.Strip[] strips, int start, int span)
		{
			int num = 0;
			for (int i = start; i < Math.Min(start + span, strips.Length); i++)
			{
				TableLayout.Strip strip = strips[i];
				num += strip.MinSize;
			}
			return num;
		}

		// Token: 0x060050F7 RID: 20727 RVA: 0x001519B4 File Offset: 0x0014FBB4
		private void DistributeSize(IList styles, TableLayout.Strip[] strips, int start, int stop, int min, int max, int cellBorderWidth)
		{
			this.xDistributeSize(styles, strips, start, stop, min, TableLayout.MinSizeProxy.GetInstance, cellBorderWidth);
			this.xDistributeSize(styles, strips, start, stop, max, TableLayout.MaxSizeProxy.GetInstance, cellBorderWidth);
		}

		// Token: 0x060050F8 RID: 20728 RVA: 0x001519E0 File Offset: 0x0014FBE0
		private void xDistributeSize(IList styles, TableLayout.Strip[] strips, int start, int stop, int desiredLength, TableLayout.SizeProxy sizeProxy, int cellBorderWidth)
		{
			int num = 0;
			int num2 = 0;
			desiredLength -= cellBorderWidth * (stop - start - 1);
			desiredLength = Math.Max(0, desiredLength);
			for (int i = start; i < stop; i++)
			{
				sizeProxy.Strip = strips[i];
				if (!this.IsAbsolutelySized(i, styles) && sizeProxy.Size == 0)
				{
					num2++;
				}
				num += sizeProxy.Size;
			}
			int num3 = desiredLength - num;
			if (num3 <= 0)
			{
				return;
			}
			if (num2 == 0)
			{
				int num4 = stop - 1;
				while (num4 >= start && (num4 >= styles.Count || ((TableLayoutStyle)styles[num4]).SizeType != SizeType.Percent))
				{
					num4--;
				}
				if (num4 != start - 1)
				{
					stop = num4 + 1;
				}
				for (int j = stop - 1; j >= start; j--)
				{
					if (!this.IsAbsolutelySized(j, styles))
					{
						sizeProxy.Strip = strips[j];
						if (j != strips.Length - 1 && !strips[j + 1].IsStart && !this.IsAbsolutelySized(j + 1, styles))
						{
							sizeProxy.Strip = strips[j + 1];
							int num5 = Math.Min(sizeProxy.Size, num3);
							sizeProxy.Size -= num5;
							strips[j + 1] = sizeProxy.Strip;
							sizeProxy.Strip = strips[j];
						}
						sizeProxy.Size += num3;
						strips[j] = sizeProxy.Strip;
						return;
					}
				}
				return;
			}
			int num6 = num3 / num2;
			int num7 = 0;
			for (int k = start; k < stop; k++)
			{
				sizeProxy.Strip = strips[k];
				if (!this.IsAbsolutelySized(k, styles) && sizeProxy.Size == 0)
				{
					num7++;
					if (num7 == num2)
					{
						num6 = num3 - num6 * (num2 - 1);
					}
					sizeProxy.Size += num6;
					strips[k] = sizeProxy.Strip;
				}
			}
		}

		// Token: 0x060050F9 RID: 20729 RVA: 0x00151BD1 File Offset: 0x0014FDD1
		private bool IsAbsolutelySized(int index, IList styles)
		{
			return index < styles.Count && ((TableLayoutStyle)styles[index]).SizeType == SizeType.Absolute;
		}

		// Token: 0x060050FA RID: 20730 RVA: 0x00151BF4 File Offset: 0x0014FDF4
		private int DistributeStyles(int cellBorderWidth, IList styles, TableLayout.Strip[] strips, int maxSize, bool dontHonorConstraint)
		{
			int num = 0;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			bool flag = false;
			for (int i = 0; i < strips.Length; i++)
			{
				TableLayout.Strip strip = strips[i];
				if (i < styles.Count)
				{
					TableLayoutStyle tableLayoutStyle = (TableLayoutStyle)styles[i];
					SizeType sizeType = tableLayoutStyle.SizeType;
					if (sizeType != SizeType.Absolute)
					{
						if (sizeType != SizeType.Percent)
						{
							num5 += (float)strip.MinSize;
							flag = true;
						}
						else
						{
							num3 += tableLayoutStyle.Size;
							num4 += (float)strip.MinSize;
						}
					}
					else
					{
						num5 += (float)strip.MinSize;
					}
				}
				else
				{
					flag = true;
				}
				strip.MaxSize += cellBorderWidth;
				strip.MinSize += cellBorderWidth;
				strips[i] = strip;
				num += strip.MinSize;
			}
			int num6 = maxSize - num;
			if (num3 > 0f)
			{
				if (!dontHonorConstraint)
				{
					if (num4 > (float)maxSize - num5)
					{
						num4 = Math.Max(0f, (float)maxSize - num5);
					}
					if (num6 > 0)
					{
						num4 += (float)num6;
					}
					else if (num6 < 0)
					{
						num4 = (float)maxSize - num5 - (float)(strips.Length * cellBorderWidth);
					}
					for (int j = 0; j < strips.Length; j++)
					{
						TableLayout.Strip strip2 = strips[j];
						SizeType sizeType2 = (j < styles.Count) ? ((TableLayoutStyle)styles[j]).SizeType : SizeType.AutoSize;
						if (sizeType2 == SizeType.Percent)
						{
							TableLayoutStyle tableLayoutStyle2 = (TableLayoutStyle)styles[j];
							int num7 = (int)(tableLayoutStyle2.Size * num4 / num3);
							num -= strip2.MinSize;
							num += num7 + cellBorderWidth;
							strip2.MinSize = num7 + cellBorderWidth;
							strips[j] = strip2;
						}
					}
				}
				else
				{
					int num8 = 0;
					for (int k = 0; k < strips.Length; k++)
					{
						TableLayout.Strip strip3 = strips[k];
						SizeType sizeType3 = (k < styles.Count) ? ((TableLayoutStyle)styles[k]).SizeType : SizeType.AutoSize;
						if (sizeType3 == SizeType.Percent)
						{
							TableLayoutStyle tableLayoutStyle3 = (TableLayoutStyle)styles[k];
							int val = (int)Math.Round((double)((float)strip3.MinSize * num3 / tableLayoutStyle3.Size));
							num8 = Math.Max(num8, val);
							num -= strip3.MinSize;
						}
					}
					num += num8;
				}
			}
			num6 = maxSize - num;
			if (flag && num6 > 0)
			{
				if ((float)num6 < num2)
				{
					float num9 = (float)num6 / num2;
				}
				num6 -= (int)Math.Ceiling((double)num2);
				for (int l = 0; l < strips.Length; l++)
				{
					TableLayout.Strip strip4 = strips[l];
					if (l >= styles.Count || ((TableLayoutStyle)styles[l]).SizeType == SizeType.AutoSize)
					{
						int num10 = Math.Min(strip4.MaxSize - strip4.MinSize, num6);
						if (num10 > 0)
						{
							num += num10;
							num6 -= num10;
							strip4.MinSize += num10;
							strips[l] = strip4;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060050FB RID: 20731 RVA: 0x00151F00 File Offset: 0x00150100
		private void SetElementBounds(TableLayout.ContainerInfo containerInfo, RectangleF displayRectF)
		{
			int cellBorderWidth = containerInfo.CellBorderWidth;
			float num = displayRectF.Y;
			int i = 0;
			int j = 0;
			bool flag = false;
			Rectangle rectangle = Rectangle.Truncate(displayRectF);
			if (containerInfo.Container is Control)
			{
				Control control = containerInfo.Container as Control;
				flag = (control.RightToLeft == RightToLeft.Yes);
			}
			TableLayout.LayoutInfo[] childrenInfo = containerInfo.ChildrenInfo;
			float num2 = flag ? displayRectF.Right : displayRectF.X;
			object[] array = childrenInfo;
			TableLayout.Sort(array, TableLayout.PostAssignedPositionComparer.GetInstance);
			foreach (TableLayout.LayoutInfo layoutInfo in childrenInfo)
			{
				IArrangedElement element = layoutInfo.Element;
				if (j != layoutInfo.RowStart)
				{
					while (j < layoutInfo.RowStart)
					{
						num += (float)containerInfo.Rows[j].MinSize;
						j++;
					}
					num2 = (flag ? displayRectF.Right : displayRectF.X);
					i = 0;
				}
				while (i < layoutInfo.ColumnStart)
				{
					if (flag)
					{
						num2 -= (float)containerInfo.Columns[i].MinSize;
					}
					else
					{
						num2 += (float)containerInfo.Columns[i].MinSize;
					}
					i++;
				}
				int num3 = i + layoutInfo.ColumnSpan;
				int num4 = 0;
				while (i < num3 && i < containerInfo.Columns.Length)
				{
					num4 += containerInfo.Columns[i].MinSize;
					i++;
				}
				if (flag)
				{
					num2 -= (float)num4;
				}
				int num5 = j + layoutInfo.RowSpan;
				int num6 = 0;
				int num7 = j;
				while (num7 < num5 && num7 < containerInfo.Rows.Length)
				{
					num6 += containerInfo.Rows[num7].MinSize;
					num7++;
				}
				Rectangle rectangle2 = new Rectangle((int)(num2 + (float)cellBorderWidth / 2f), (int)(num + (float)cellBorderWidth / 2f), num4 - cellBorderWidth, num6 - cellBorderWidth);
				Padding margin = CommonProperties.GetMargin(element);
				if (flag)
				{
					int right = margin.Right;
					margin.Right = margin.Left;
					margin.Left = right;
				}
				rectangle2 = LayoutUtils.DeflateRect(rectangle2, margin);
				rectangle2.Width = Math.Max(rectangle2.Width, 1);
				rectangle2.Height = Math.Max(rectangle2.Height, 1);
				AnchorStyles unifiedAnchor = LayoutUtils.GetUnifiedAnchor(element);
				Rectangle bounds = LayoutUtils.AlignAndStretch(this.GetElementSize(element, rectangle2.Size), rectangle2, unifiedAnchor);
				bounds.Width = Math.Min(rectangle2.Width, bounds.Width);
				bounds.Height = Math.Min(rectangle2.Height, bounds.Height);
				if (flag)
				{
					bounds.X = rectangle2.X + (rectangle2.Right - bounds.Right);
				}
				element.SetBounds(bounds, BoundsSpecified.None);
				if (!flag)
				{
					num2 += (float)num4;
				}
			}
		}

		// Token: 0x060050FC RID: 20732 RVA: 0x001521D0 File Offset: 0x001503D0
		internal IArrangedElement GetControlFromPosition(IArrangedElement container, int column, int row)
		{
			if (row < 0)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"RowPosition",
					row.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (column < 0)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"ColumnPosition",
					column.ToString(CultureInfo.CurrentCulture)
				}));
			}
			ArrangedElementCollection children = container.Children;
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(container);
			if (children == null || children.Count == 0)
			{
				return null;
			}
			if (!containerInfo.Valid)
			{
				this.EnsureRowAndColumnAssignments(container, containerInfo, true);
			}
			for (int i = 0; i < children.Count; i++)
			{
				TableLayout.LayoutInfo layoutInfo = TableLayout.GetLayoutInfo(children[i]);
				if (layoutInfo.ColumnStart <= column && layoutInfo.ColumnStart + layoutInfo.ColumnSpan - 1 >= column && layoutInfo.RowStart <= row && layoutInfo.RowStart + layoutInfo.RowSpan - 1 >= row)
				{
					return layoutInfo.Element;
				}
			}
			return null;
		}

		// Token: 0x060050FD RID: 20733 RVA: 0x001522C8 File Offset: 0x001504C8
		internal TableLayoutPanelCellPosition GetPositionFromControl(IArrangedElement container, IArrangedElement child)
		{
			if (container == null || child == null)
			{
				return new TableLayoutPanelCellPosition(-1, -1);
			}
			ArrangedElementCollection children = container.Children;
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(container);
			if (children == null || children.Count == 0)
			{
				return new TableLayoutPanelCellPosition(-1, -1);
			}
			if (!containerInfo.Valid)
			{
				this.EnsureRowAndColumnAssignments(container, containerInfo, true);
			}
			TableLayout.LayoutInfo layoutInfo = TableLayout.GetLayoutInfo(child);
			return new TableLayoutPanelCellPosition(layoutInfo.ColumnStart, layoutInfo.RowStart);
		}

		// Token: 0x060050FE RID: 20734 RVA: 0x00152330 File Offset: 0x00150530
		internal static TableLayout.LayoutInfo GetLayoutInfo(IArrangedElement element)
		{
			TableLayout.LayoutInfo layoutInfo = (TableLayout.LayoutInfo)element.Properties.GetObject(TableLayout._layoutInfoProperty);
			if (layoutInfo == null)
			{
				layoutInfo = new TableLayout.LayoutInfo(element);
				TableLayout.SetLayoutInfo(element, layoutInfo);
			}
			return layoutInfo;
		}

		// Token: 0x060050FF RID: 20735 RVA: 0x00152365 File Offset: 0x00150565
		internal static void SetLayoutInfo(IArrangedElement element, TableLayout.LayoutInfo value)
		{
			element.Properties.SetObject(TableLayout._layoutInfoProperty, value);
		}

		// Token: 0x06005100 RID: 20736 RVA: 0x00152378 File Offset: 0x00150578
		internal static bool HasCachedAssignments(TableLayout.ContainerInfo containerInfo)
		{
			return containerInfo.Valid;
		}

		// Token: 0x06005101 RID: 20737 RVA: 0x00152380 File Offset: 0x00150580
		internal static void ClearCachedAssignments(TableLayout.ContainerInfo containerInfo)
		{
			containerInfo.Valid = false;
		}

		// Token: 0x06005102 RID: 20738 RVA: 0x0015238C File Offset: 0x0015058C
		internal static TableLayout.ContainerInfo GetContainerInfo(IArrangedElement container)
		{
			TableLayout.ContainerInfo containerInfo = (TableLayout.ContainerInfo)container.Properties.GetObject(TableLayout._containerInfoProperty);
			if (containerInfo == null)
			{
				containerInfo = new TableLayout.ContainerInfo(container);
				container.Properties.SetObject(TableLayout._containerInfoProperty, containerInfo);
			}
			return containerInfo;
		}

		// Token: 0x06005103 RID: 20739 RVA: 0x000072B6 File Offset: 0x000054B6
		[Conditional("DEBUG_LAYOUT")]
		private void Debug_VerifyAssignmentsAreCurrent(IArrangedElement container, TableLayout.ContainerInfo containerInfo)
		{
		}

		// Token: 0x06005104 RID: 20740 RVA: 0x001523CC File Offset: 0x001505CC
		[Conditional("DEBUG_LAYOUT")]
		private void Debug_VerifyNoOverlapping(IArrangedElement container)
		{
			ArrayList arrayList = new ArrayList(container.Children.Count);
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(container);
			TableLayout.Strip[] rows = containerInfo.Rows;
			TableLayout.Strip[] columns = containerInfo.Columns;
			foreach (object obj in container.Children)
			{
				IArrangedElement arrangedElement = (IArrangedElement)obj;
				if (arrangedElement.ParticipatesInLayout)
				{
					arrayList.Add(TableLayout.GetLayoutInfo(arrangedElement));
				}
			}
			for (int i = 0; i < arrayList.Count; i++)
			{
				TableLayout.LayoutInfo layoutInfo = (TableLayout.LayoutInfo)arrayList[i];
				Rectangle bounds = layoutInfo.Element.Bounds;
				Rectangle rectangle = new Rectangle(layoutInfo.ColumnStart, layoutInfo.RowStart, layoutInfo.ColumnSpan, layoutInfo.RowSpan);
				for (int j = i + 1; j < arrayList.Count; j++)
				{
					TableLayout.LayoutInfo layoutInfo2 = (TableLayout.LayoutInfo)arrayList[j];
					Rectangle bounds2 = layoutInfo2.Element.Bounds;
					Rectangle rectangle2 = new Rectangle(layoutInfo2.ColumnStart, layoutInfo2.RowStart, layoutInfo2.ColumnSpan, layoutInfo2.RowSpan);
					if (LayoutUtils.IsIntersectHorizontally(bounds, bounds2))
					{
						for (int k = layoutInfo.ColumnStart; k < layoutInfo.ColumnStart + layoutInfo.ColumnSpan; k++)
						{
						}
						for (int k = layoutInfo2.ColumnStart; k < layoutInfo2.ColumnStart + layoutInfo2.ColumnSpan; k++)
						{
						}
					}
					if (LayoutUtils.IsIntersectVertically(bounds, bounds2))
					{
						for (int l = layoutInfo.RowStart; l < layoutInfo.RowStart + layoutInfo.RowSpan; l++)
						{
						}
						for (int l = layoutInfo2.RowStart; l < layoutInfo2.RowStart + layoutInfo2.RowSpan; l++)
						{
						}
					}
				}
			}
		}

		// Token: 0x040034F4 RID: 13556
		internal static readonly TableLayout Instance = new TableLayout();

		// Token: 0x040034F5 RID: 13557
		private static readonly int _containerInfoProperty = PropertyStore.CreateKey();

		// Token: 0x040034F6 RID: 13558
		private static readonly int _layoutInfoProperty = PropertyStore.CreateKey();

		// Token: 0x040034F7 RID: 13559
		private static string[] _propertiesWhichInvalidateCache = new string[]
		{
			null,
			PropertyNames.ChildIndex,
			PropertyNames.Parent,
			PropertyNames.Visible,
			PropertyNames.Items,
			PropertyNames.Rows,
			PropertyNames.Columns,
			PropertyNames.RowStyles,
			PropertyNames.ColumnStyles
		};

		// Token: 0x02000867 RID: 2151
		private struct SorterObjectArray
		{
			// Token: 0x06007110 RID: 28944 RVA: 0x0019ED6A File Offset: 0x0019CF6A
			internal SorterObjectArray(object[] keys, IComparer comparer)
			{
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				this.keys = keys;
				this.comparer = comparer;
			}

			// Token: 0x06007111 RID: 28945 RVA: 0x0019ED84 File Offset: 0x0019CF84
			internal void SwapIfGreaterWithItems(int a, int b)
			{
				if (a != b)
				{
					try
					{
						if (this.comparer.Compare(this.keys[a], this.keys[b]) > 0)
						{
							object obj = this.keys[a];
							this.keys[a] = this.keys[b];
							this.keys[b] = obj;
						}
					}
					catch (IndexOutOfRangeException)
					{
						throw new ArgumentException();
					}
					catch (Exception)
					{
						throw new InvalidOperationException();
					}
				}
			}

			// Token: 0x06007112 RID: 28946 RVA: 0x0019EE04 File Offset: 0x0019D004
			internal void QuickSort(int left, int right)
			{
				do
				{
					int num = left;
					int num2 = right;
					int median = TableLayout.GetMedian(num, num2);
					this.SwapIfGreaterWithItems(num, median);
					this.SwapIfGreaterWithItems(num, num2);
					this.SwapIfGreaterWithItems(median, num2);
					object obj = this.keys[median];
					do
					{
						try
						{
							while (this.comparer.Compare(this.keys[num], obj) < 0)
							{
								num++;
							}
							while (this.comparer.Compare(obj, this.keys[num2]) < 0)
							{
								num2--;
							}
						}
						catch (IndexOutOfRangeException)
						{
							throw new ArgumentException();
						}
						catch (Exception)
						{
							throw new InvalidOperationException();
						}
						if (num > num2)
						{
							break;
						}
						if (num < num2)
						{
							object obj2 = this.keys[num];
							this.keys[num] = this.keys[num2];
							this.keys[num2] = obj2;
						}
						num++;
						num2--;
					}
					while (num <= num2);
					if (num2 - left <= right - num)
					{
						if (left < num2)
						{
							this.QuickSort(left, num2);
						}
						left = num;
					}
					else
					{
						if (num < right)
						{
							this.QuickSort(num, right);
						}
						right = num2;
					}
				}
				while (left < right);
			}

			// Token: 0x040043FF RID: 17407
			private object[] keys;

			// Token: 0x04004400 RID: 17408
			private IComparer comparer;
		}

		// Token: 0x02000868 RID: 2152
		internal sealed class LayoutInfo
		{
			// Token: 0x06007113 RID: 28947 RVA: 0x0019EF10 File Offset: 0x0019D110
			public LayoutInfo(IArrangedElement element)
			{
				this._element = element;
			}

			// Token: 0x170018B2 RID: 6322
			// (get) Token: 0x06007114 RID: 28948 RVA: 0x0019EF49 File Offset: 0x0019D149
			internal bool IsAbsolutelyPositioned
			{
				get
				{
					return this._rowPos >= 0 && this._colPos >= 0;
				}
			}

			// Token: 0x170018B3 RID: 6323
			// (get) Token: 0x06007115 RID: 28949 RVA: 0x0019EF62 File Offset: 0x0019D162
			internal IArrangedElement Element
			{
				get
				{
					return this._element;
				}
			}

			// Token: 0x170018B4 RID: 6324
			// (get) Token: 0x06007116 RID: 28950 RVA: 0x0019EF6A File Offset: 0x0019D16A
			// (set) Token: 0x06007117 RID: 28951 RVA: 0x0019EF72 File Offset: 0x0019D172
			internal int RowPosition
			{
				get
				{
					return this._rowPos;
				}
				set
				{
					this._rowPos = value;
				}
			}

			// Token: 0x170018B5 RID: 6325
			// (get) Token: 0x06007118 RID: 28952 RVA: 0x0019EF7B File Offset: 0x0019D17B
			// (set) Token: 0x06007119 RID: 28953 RVA: 0x0019EF83 File Offset: 0x0019D183
			internal int ColumnPosition
			{
				get
				{
					return this._colPos;
				}
				set
				{
					this._colPos = value;
				}
			}

			// Token: 0x170018B6 RID: 6326
			// (get) Token: 0x0600711A RID: 28954 RVA: 0x0019EF8C File Offset: 0x0019D18C
			// (set) Token: 0x0600711B RID: 28955 RVA: 0x0019EF94 File Offset: 0x0019D194
			internal int RowStart
			{
				get
				{
					return this._rowStart;
				}
				set
				{
					this._rowStart = value;
				}
			}

			// Token: 0x170018B7 RID: 6327
			// (get) Token: 0x0600711C RID: 28956 RVA: 0x0019EF9D File Offset: 0x0019D19D
			// (set) Token: 0x0600711D RID: 28957 RVA: 0x0019EFA5 File Offset: 0x0019D1A5
			internal int ColumnStart
			{
				get
				{
					return this._columnStart;
				}
				set
				{
					this._columnStart = value;
				}
			}

			// Token: 0x170018B8 RID: 6328
			// (get) Token: 0x0600711E RID: 28958 RVA: 0x0019EFAE File Offset: 0x0019D1AE
			// (set) Token: 0x0600711F RID: 28959 RVA: 0x0019EFB6 File Offset: 0x0019D1B6
			internal int ColumnSpan
			{
				get
				{
					return this._columnSpan;
				}
				set
				{
					this._columnSpan = value;
				}
			}

			// Token: 0x170018B9 RID: 6329
			// (get) Token: 0x06007120 RID: 28960 RVA: 0x0019EFBF File Offset: 0x0019D1BF
			// (set) Token: 0x06007121 RID: 28961 RVA: 0x0019EFC7 File Offset: 0x0019D1C7
			internal int RowSpan
			{
				get
				{
					return this._rowSpan;
				}
				set
				{
					this._rowSpan = value;
				}
			}

			// Token: 0x04004401 RID: 17409
			private int _rowStart = -1;

			// Token: 0x04004402 RID: 17410
			private int _columnStart = -1;

			// Token: 0x04004403 RID: 17411
			private int _columnSpan = 1;

			// Token: 0x04004404 RID: 17412
			private int _rowSpan = 1;

			// Token: 0x04004405 RID: 17413
			private int _rowPos = -1;

			// Token: 0x04004406 RID: 17414
			private int _colPos = -1;

			// Token: 0x04004407 RID: 17415
			private IArrangedElement _element;
		}

		// Token: 0x02000869 RID: 2153
		internal sealed class ContainerInfo
		{
			// Token: 0x06007122 RID: 28962 RVA: 0x0019EFD0 File Offset: 0x0019D1D0
			public ContainerInfo(IArrangedElement container)
			{
				this._container = container;
				this._growStyle = TableLayoutPanelGrowStyle.AddRows;
			}

			// Token: 0x06007123 RID: 28963 RVA: 0x0019EFFC File Offset: 0x0019D1FC
			public ContainerInfo(TableLayout.ContainerInfo containerInfo)
			{
				this._cellBorderWidth = containerInfo.CellBorderWidth;
				this._maxRows = containerInfo.MaxRows;
				this._maxColumns = containerInfo.MaxColumns;
				this._growStyle = containerInfo.GrowStyle;
				this._container = containerInfo.Container;
				this._rowStyles = containerInfo.RowStyles;
				this._colStyles = containerInfo.ColumnStyles;
			}

			// Token: 0x170018BA RID: 6330
			// (get) Token: 0x06007124 RID: 28964 RVA: 0x0019F079 File Offset: 0x0019D279
			public IArrangedElement Container
			{
				get
				{
					return this._container;
				}
			}

			// Token: 0x170018BB RID: 6331
			// (get) Token: 0x06007125 RID: 28965 RVA: 0x0019F081 File Offset: 0x0019D281
			// (set) Token: 0x06007126 RID: 28966 RVA: 0x0019F089 File Offset: 0x0019D289
			public int CellBorderWidth
			{
				get
				{
					return this._cellBorderWidth;
				}
				set
				{
					this._cellBorderWidth = value;
				}
			}

			// Token: 0x170018BC RID: 6332
			// (get) Token: 0x06007127 RID: 28967 RVA: 0x0019F092 File Offset: 0x0019D292
			// (set) Token: 0x06007128 RID: 28968 RVA: 0x0019F09A File Offset: 0x0019D29A
			public TableLayout.Strip[] Columns
			{
				get
				{
					return this._cols;
				}
				set
				{
					this._cols = value;
				}
			}

			// Token: 0x170018BD RID: 6333
			// (get) Token: 0x06007129 RID: 28969 RVA: 0x0019F0A3 File Offset: 0x0019D2A3
			// (set) Token: 0x0600712A RID: 28970 RVA: 0x0019F0AB File Offset: 0x0019D2AB
			public TableLayout.Strip[] Rows
			{
				get
				{
					return this._rows;
				}
				set
				{
					this._rows = value;
				}
			}

			// Token: 0x170018BE RID: 6334
			// (get) Token: 0x0600712B RID: 28971 RVA: 0x0019F0B4 File Offset: 0x0019D2B4
			// (set) Token: 0x0600712C RID: 28972 RVA: 0x0019F0BC File Offset: 0x0019D2BC
			public int MaxRows
			{
				get
				{
					return this._maxRows;
				}
				set
				{
					if (this._maxRows != value)
					{
						this._maxRows = value;
						this.Valid = false;
					}
				}
			}

			// Token: 0x170018BF RID: 6335
			// (get) Token: 0x0600712D RID: 28973 RVA: 0x0019F0D5 File Offset: 0x0019D2D5
			// (set) Token: 0x0600712E RID: 28974 RVA: 0x0019F0DD File Offset: 0x0019D2DD
			public int MaxColumns
			{
				get
				{
					return this._maxColumns;
				}
				set
				{
					if (this._maxColumns != value)
					{
						this._maxColumns = value;
						this.Valid = false;
					}
				}
			}

			// Token: 0x170018C0 RID: 6336
			// (get) Token: 0x0600712F RID: 28975 RVA: 0x0019F0F6 File Offset: 0x0019D2F6
			public int MinRowsAndColumns
			{
				get
				{
					return this._minRowsAndColumns;
				}
			}

			// Token: 0x170018C1 RID: 6337
			// (get) Token: 0x06007130 RID: 28976 RVA: 0x0019F0FE File Offset: 0x0019D2FE
			public int MinColumns
			{
				get
				{
					return this._minColumns;
				}
			}

			// Token: 0x170018C2 RID: 6338
			// (get) Token: 0x06007131 RID: 28977 RVA: 0x0019F106 File Offset: 0x0019D306
			public int MinRows
			{
				get
				{
					return this._minRows;
				}
			}

			// Token: 0x170018C3 RID: 6339
			// (get) Token: 0x06007132 RID: 28978 RVA: 0x0019F10E File Offset: 0x0019D30E
			// (set) Token: 0x06007133 RID: 28979 RVA: 0x0019F116 File Offset: 0x0019D316
			public TableLayoutPanelGrowStyle GrowStyle
			{
				get
				{
					return this._growStyle;
				}
				set
				{
					if (this._growStyle != value)
					{
						this._growStyle = value;
						this.Valid = false;
					}
				}
			}

			// Token: 0x170018C4 RID: 6340
			// (get) Token: 0x06007134 RID: 28980 RVA: 0x0019F12F File Offset: 0x0019D32F
			// (set) Token: 0x06007135 RID: 28981 RVA: 0x0019F150 File Offset: 0x0019D350
			public TableLayoutRowStyleCollection RowStyles
			{
				get
				{
					if (this._rowStyles == null)
					{
						this._rowStyles = new TableLayoutRowStyleCollection(this._container);
					}
					return this._rowStyles;
				}
				set
				{
					this._rowStyles = value;
					if (this._rowStyles != null)
					{
						this._rowStyles.EnsureOwnership(this._container);
					}
				}
			}

			// Token: 0x170018C5 RID: 6341
			// (get) Token: 0x06007136 RID: 28982 RVA: 0x0019F172 File Offset: 0x0019D372
			// (set) Token: 0x06007137 RID: 28983 RVA: 0x0019F193 File Offset: 0x0019D393
			public TableLayoutColumnStyleCollection ColumnStyles
			{
				get
				{
					if (this._colStyles == null)
					{
						this._colStyles = new TableLayoutColumnStyleCollection(this._container);
					}
					return this._colStyles;
				}
				set
				{
					this._colStyles = value;
					if (this._colStyles != null)
					{
						this._colStyles.EnsureOwnership(this._container);
					}
				}
			}

			// Token: 0x170018C6 RID: 6342
			// (get) Token: 0x06007138 RID: 28984 RVA: 0x0019F1B8 File Offset: 0x0019D3B8
			public TableLayout.LayoutInfo[] ChildrenInfo
			{
				get
				{
					if (!this._state[TableLayout.ContainerInfo.stateChildInfoValid])
					{
						this._countFixedChildren = 0;
						this._minRowsAndColumns = 0;
						this._minColumns = 0;
						this._minRows = 0;
						ArrangedElementCollection children = this.Container.Children;
						TableLayout.LayoutInfo[] array = new TableLayout.LayoutInfo[children.Count];
						int num = 0;
						int num2 = 0;
						for (int i = 0; i < children.Count; i++)
						{
							IArrangedElement arrangedElement = children[i];
							if (!arrangedElement.ParticipatesInLayout)
							{
								num++;
							}
							else
							{
								TableLayout.LayoutInfo layoutInfo = TableLayout.GetLayoutInfo(arrangedElement);
								if (layoutInfo.IsAbsolutelyPositioned)
								{
									this._countFixedChildren++;
								}
								array[num2++] = layoutInfo;
								this._minRowsAndColumns += layoutInfo.RowSpan * layoutInfo.ColumnSpan;
								if (layoutInfo.IsAbsolutelyPositioned)
								{
									this._minColumns = Math.Max(this._minColumns, layoutInfo.ColumnPosition + layoutInfo.ColumnSpan);
									this._minRows = Math.Max(this._minRows, layoutInfo.RowPosition + layoutInfo.RowSpan);
								}
							}
						}
						if (num > 0)
						{
							TableLayout.LayoutInfo[] array2 = new TableLayout.LayoutInfo[array.Length - num];
							Array.Copy(array, array2, array2.Length);
							this._childInfo = array2;
						}
						else
						{
							this._childInfo = array;
						}
						this._state[TableLayout.ContainerInfo.stateChildInfoValid] = true;
					}
					if (this._childInfo != null)
					{
						return this._childInfo;
					}
					return new TableLayout.LayoutInfo[0];
				}
			}

			// Token: 0x170018C7 RID: 6343
			// (get) Token: 0x06007139 RID: 28985 RVA: 0x0019F32A File Offset: 0x0019D52A
			public bool ChildInfoValid
			{
				get
				{
					return this._state[TableLayout.ContainerInfo.stateChildInfoValid];
				}
			}

			// Token: 0x170018C8 RID: 6344
			// (get) Token: 0x0600713A RID: 28986 RVA: 0x0019F33C File Offset: 0x0019D53C
			public TableLayout.LayoutInfo[] FixedChildrenInfo
			{
				get
				{
					TableLayout.LayoutInfo[] array = new TableLayout.LayoutInfo[this._countFixedChildren];
					if (this.HasChildWithAbsolutePositioning)
					{
						int num = 0;
						for (int i = 0; i < this._childInfo.Length; i++)
						{
							if (this._childInfo[i].IsAbsolutelyPositioned)
							{
								array[num++] = this._childInfo[i];
							}
						}
						object[] array2 = array;
						TableLayout.Sort(array2, TableLayout.PreAssignedPositionComparer.GetInstance);
					}
					return array;
				}
			}

			// Token: 0x170018C9 RID: 6345
			// (get) Token: 0x0600713B RID: 28987 RVA: 0x0019F39E File Offset: 0x0019D59E
			// (set) Token: 0x0600713C RID: 28988 RVA: 0x0019F3B0 File Offset: 0x0019D5B0
			public bool Valid
			{
				get
				{
					return this._state[TableLayout.ContainerInfo.stateValid];
				}
				set
				{
					this._state[TableLayout.ContainerInfo.stateValid] = value;
					if (!this._state[TableLayout.ContainerInfo.stateValid])
					{
						this._state[TableLayout.ContainerInfo.stateChildInfoValid] = false;
					}
				}
			}

			// Token: 0x170018CA RID: 6346
			// (get) Token: 0x0600713D RID: 28989 RVA: 0x0019F3E6 File Offset: 0x0019D5E6
			public bool HasChildWithAbsolutePositioning
			{
				get
				{
					return this._countFixedChildren > 0;
				}
			}

			// Token: 0x170018CB RID: 6347
			// (get) Token: 0x0600713E RID: 28990 RVA: 0x0019F3F4 File Offset: 0x0019D5F4
			public bool HasMultiplePercentColumns
			{
				get
				{
					if (this._colStyles != null)
					{
						bool flag = false;
						foreach (object obj in ((IEnumerable)this._colStyles))
						{
							ColumnStyle columnStyle = (ColumnStyle)obj;
							if (columnStyle.SizeType == SizeType.Percent)
							{
								if (flag)
								{
									return true;
								}
								flag = true;
							}
						}
						return false;
					}
					return false;
				}
			}

			// Token: 0x170018CC RID: 6348
			// (get) Token: 0x0600713F RID: 28991 RVA: 0x0019F468 File Offset: 0x0019D668
			// (set) Token: 0x06007140 RID: 28992 RVA: 0x0019F47A File Offset: 0x0019D67A
			public bool ChildHasColumnSpan
			{
				get
				{
					return this._state[TableLayout.ContainerInfo.stateChildHasColumnSpan];
				}
				set
				{
					this._state[TableLayout.ContainerInfo.stateChildHasColumnSpan] = value;
				}
			}

			// Token: 0x170018CD RID: 6349
			// (get) Token: 0x06007141 RID: 28993 RVA: 0x0019F48D File Offset: 0x0019D68D
			// (set) Token: 0x06007142 RID: 28994 RVA: 0x0019F49F File Offset: 0x0019D69F
			public bool ChildHasRowSpan
			{
				get
				{
					return this._state[TableLayout.ContainerInfo.stateChildHasRowSpan];
				}
				set
				{
					this._state[TableLayout.ContainerInfo.stateChildHasRowSpan] = value;
				}
			}

			// Token: 0x06007143 RID: 28995 RVA: 0x0019F4B4 File Offset: 0x0019D6B4
			public Size GetCachedPreferredSize(Size proposedContstraints, out bool isValid)
			{
				isValid = false;
				if (proposedContstraints.Height == 0 || proposedContstraints.Width == 0)
				{
					Size result = CommonProperties.xGetPreferredSizeCache(this.Container);
					if (!result.IsEmpty)
					{
						isValid = true;
						return result;
					}
				}
				return Size.Empty;
			}

			// Token: 0x04004408 RID: 17416
			private static TableLayout.Strip[] emptyStrip = new TableLayout.Strip[0];

			// Token: 0x04004409 RID: 17417
			private static readonly int stateValid = BitVector32.CreateMask();

			// Token: 0x0400440A RID: 17418
			private static readonly int stateChildInfoValid = BitVector32.CreateMask(TableLayout.ContainerInfo.stateValid);

			// Token: 0x0400440B RID: 17419
			private static readonly int stateChildHasColumnSpan = BitVector32.CreateMask(TableLayout.ContainerInfo.stateChildInfoValid);

			// Token: 0x0400440C RID: 17420
			private static readonly int stateChildHasRowSpan = BitVector32.CreateMask(TableLayout.ContainerInfo.stateChildHasColumnSpan);

			// Token: 0x0400440D RID: 17421
			private int _cellBorderWidth;

			// Token: 0x0400440E RID: 17422
			private TableLayout.Strip[] _cols = TableLayout.ContainerInfo.emptyStrip;

			// Token: 0x0400440F RID: 17423
			private TableLayout.Strip[] _rows = TableLayout.ContainerInfo.emptyStrip;

			// Token: 0x04004410 RID: 17424
			private int _maxRows;

			// Token: 0x04004411 RID: 17425
			private int _maxColumns;

			// Token: 0x04004412 RID: 17426
			private TableLayoutRowStyleCollection _rowStyles;

			// Token: 0x04004413 RID: 17427
			private TableLayoutColumnStyleCollection _colStyles;

			// Token: 0x04004414 RID: 17428
			private TableLayoutPanelGrowStyle _growStyle;

			// Token: 0x04004415 RID: 17429
			private IArrangedElement _container;

			// Token: 0x04004416 RID: 17430
			private TableLayout.LayoutInfo[] _childInfo;

			// Token: 0x04004417 RID: 17431
			private int _countFixedChildren;

			// Token: 0x04004418 RID: 17432
			private int _minRowsAndColumns;

			// Token: 0x04004419 RID: 17433
			private int _minColumns;

			// Token: 0x0400441A RID: 17434
			private int _minRows;

			// Token: 0x0400441B RID: 17435
			private BitVector32 _state;
		}

		// Token: 0x0200086A RID: 2154
		private abstract class SizeProxy
		{
			// Token: 0x170018CE RID: 6350
			// (get) Token: 0x06007145 RID: 28997 RVA: 0x0019F547 File Offset: 0x0019D747
			// (set) Token: 0x06007146 RID: 28998 RVA: 0x0019F54F File Offset: 0x0019D74F
			public TableLayout.Strip Strip
			{
				get
				{
					return this.strip;
				}
				set
				{
					this.strip = value;
				}
			}

			// Token: 0x170018CF RID: 6351
			// (get) Token: 0x06007147 RID: 28999
			// (set) Token: 0x06007148 RID: 29000
			public abstract int Size { get; set; }

			// Token: 0x0400441C RID: 17436
			protected TableLayout.Strip strip;
		}

		// Token: 0x0200086B RID: 2155
		private class MinSizeProxy : TableLayout.SizeProxy
		{
			// Token: 0x170018D0 RID: 6352
			// (get) Token: 0x0600714A RID: 29002 RVA: 0x0019F558 File Offset: 0x0019D758
			// (set) Token: 0x0600714B RID: 29003 RVA: 0x0019F565 File Offset: 0x0019D765
			public override int Size
			{
				get
				{
					return this.strip.MinSize;
				}
				set
				{
					this.strip.MinSize = value;
				}
			}

			// Token: 0x170018D1 RID: 6353
			// (get) Token: 0x0600714C RID: 29004 RVA: 0x0019F573 File Offset: 0x0019D773
			public static TableLayout.MinSizeProxy GetInstance
			{
				get
				{
					return TableLayout.MinSizeProxy.instance;
				}
			}

			// Token: 0x0400441D RID: 17437
			private static readonly TableLayout.MinSizeProxy instance = new TableLayout.MinSizeProxy();
		}

		// Token: 0x0200086C RID: 2156
		private class MaxSizeProxy : TableLayout.SizeProxy
		{
			// Token: 0x170018D2 RID: 6354
			// (get) Token: 0x0600714F RID: 29007 RVA: 0x0019F58E File Offset: 0x0019D78E
			// (set) Token: 0x06007150 RID: 29008 RVA: 0x0019F59B File Offset: 0x0019D79B
			public override int Size
			{
				get
				{
					return this.strip.MaxSize;
				}
				set
				{
					this.strip.MaxSize = value;
				}
			}

			// Token: 0x170018D3 RID: 6355
			// (get) Token: 0x06007151 RID: 29009 RVA: 0x0019F5A9 File Offset: 0x0019D7A9
			public static TableLayout.MaxSizeProxy GetInstance
			{
				get
				{
					return TableLayout.MaxSizeProxy.instance;
				}
			}

			// Token: 0x0400441E RID: 17438
			private static readonly TableLayout.MaxSizeProxy instance = new TableLayout.MaxSizeProxy();
		}

		// Token: 0x0200086D RID: 2157
		private abstract class SpanComparer : IComparer
		{
			// Token: 0x06007154 RID: 29012
			public abstract int GetSpan(TableLayout.LayoutInfo layoutInfo);

			// Token: 0x06007155 RID: 29013 RVA: 0x0019F5BC File Offset: 0x0019D7BC
			public int Compare(object x, object y)
			{
				TableLayout.LayoutInfo layoutInfo = (TableLayout.LayoutInfo)x;
				TableLayout.LayoutInfo layoutInfo2 = (TableLayout.LayoutInfo)y;
				return this.GetSpan(layoutInfo) - this.GetSpan(layoutInfo2);
			}
		}

		// Token: 0x0200086E RID: 2158
		private class RowSpanComparer : TableLayout.SpanComparer
		{
			// Token: 0x06007157 RID: 29015 RVA: 0x0019F5E6 File Offset: 0x0019D7E6
			public override int GetSpan(TableLayout.LayoutInfo layoutInfo)
			{
				return layoutInfo.RowSpan;
			}

			// Token: 0x170018D4 RID: 6356
			// (get) Token: 0x06007158 RID: 29016 RVA: 0x0019F5EE File Offset: 0x0019D7EE
			public static TableLayout.RowSpanComparer GetInstance
			{
				get
				{
					return TableLayout.RowSpanComparer.instance;
				}
			}

			// Token: 0x0400441F RID: 17439
			private static readonly TableLayout.RowSpanComparer instance = new TableLayout.RowSpanComparer();
		}

		// Token: 0x0200086F RID: 2159
		private class ColumnSpanComparer : TableLayout.SpanComparer
		{
			// Token: 0x0600715B RID: 29019 RVA: 0x0019F609 File Offset: 0x0019D809
			public override int GetSpan(TableLayout.LayoutInfo layoutInfo)
			{
				return layoutInfo.ColumnSpan;
			}

			// Token: 0x170018D5 RID: 6357
			// (get) Token: 0x0600715C RID: 29020 RVA: 0x0019F611 File Offset: 0x0019D811
			public static TableLayout.ColumnSpanComparer GetInstance
			{
				get
				{
					return TableLayout.ColumnSpanComparer.instance;
				}
			}

			// Token: 0x04004420 RID: 17440
			private static readonly TableLayout.ColumnSpanComparer instance = new TableLayout.ColumnSpanComparer();
		}

		// Token: 0x02000870 RID: 2160
		private class PostAssignedPositionComparer : IComparer
		{
			// Token: 0x170018D6 RID: 6358
			// (get) Token: 0x0600715F RID: 29023 RVA: 0x0019F624 File Offset: 0x0019D824
			public static TableLayout.PostAssignedPositionComparer GetInstance
			{
				get
				{
					return TableLayout.PostAssignedPositionComparer.instance;
				}
			}

			// Token: 0x06007160 RID: 29024 RVA: 0x0019F62C File Offset: 0x0019D82C
			public int Compare(object x, object y)
			{
				TableLayout.LayoutInfo layoutInfo = (TableLayout.LayoutInfo)x;
				TableLayout.LayoutInfo layoutInfo2 = (TableLayout.LayoutInfo)y;
				if (layoutInfo.RowStart < layoutInfo2.RowStart)
				{
					return -1;
				}
				if (layoutInfo.RowStart > layoutInfo2.RowStart)
				{
					return 1;
				}
				if (layoutInfo.ColumnStart < layoutInfo2.ColumnStart)
				{
					return -1;
				}
				if (layoutInfo.ColumnStart > layoutInfo2.ColumnStart)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x04004421 RID: 17441
			private static readonly TableLayout.PostAssignedPositionComparer instance = new TableLayout.PostAssignedPositionComparer();
		}

		// Token: 0x02000871 RID: 2161
		private class PreAssignedPositionComparer : IComparer
		{
			// Token: 0x170018D7 RID: 6359
			// (get) Token: 0x06007163 RID: 29027 RVA: 0x0019F694 File Offset: 0x0019D894
			public static TableLayout.PreAssignedPositionComparer GetInstance
			{
				get
				{
					return TableLayout.PreAssignedPositionComparer.instance;
				}
			}

			// Token: 0x06007164 RID: 29028 RVA: 0x0019F69C File Offset: 0x0019D89C
			public int Compare(object x, object y)
			{
				TableLayout.LayoutInfo layoutInfo = (TableLayout.LayoutInfo)x;
				TableLayout.LayoutInfo layoutInfo2 = (TableLayout.LayoutInfo)y;
				if (layoutInfo.RowPosition < layoutInfo2.RowPosition)
				{
					return -1;
				}
				if (layoutInfo.RowPosition > layoutInfo2.RowPosition)
				{
					return 1;
				}
				if (layoutInfo.ColumnPosition < layoutInfo2.ColumnPosition)
				{
					return -1;
				}
				if (layoutInfo.ColumnPosition > layoutInfo2.ColumnPosition)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x04004422 RID: 17442
			private static readonly TableLayout.PreAssignedPositionComparer instance = new TableLayout.PreAssignedPositionComparer();
		}

		// Token: 0x02000872 RID: 2162
		private sealed class ReservationGrid
		{
			// Token: 0x06007167 RID: 29031 RVA: 0x0019F704 File Offset: 0x0019D904
			public bool IsReserved(int column, int rowOffset)
			{
				return rowOffset < this._rows.Count && column < ((BitArray)this._rows[rowOffset]).Length && ((BitArray)this._rows[rowOffset])[column];
			}

			// Token: 0x06007168 RID: 29032 RVA: 0x0019F754 File Offset: 0x0019D954
			public void Reserve(int column, int rowOffset)
			{
				while (rowOffset >= this._rows.Count)
				{
					this._rows.Add(new BitArray(this._numColumns));
				}
				if (column >= ((BitArray)this._rows[rowOffset]).Length)
				{
					((BitArray)this._rows[rowOffset]).Length = column + 1;
					if (column >= this._numColumns)
					{
						this._numColumns = column + 1;
					}
				}
				((BitArray)this._rows[rowOffset])[column] = true;
			}

			// Token: 0x06007169 RID: 29033 RVA: 0x0019F7E4 File Offset: 0x0019D9E4
			public void ReserveAll(TableLayout.LayoutInfo layoutInfo, int rowStop, int colStop)
			{
				for (int i = 1; i < rowStop - layoutInfo.RowStart; i++)
				{
					for (int j = layoutInfo.ColumnStart; j < colStop; j++)
					{
						this.Reserve(j, i);
					}
				}
			}

			// Token: 0x0600716A RID: 29034 RVA: 0x0019F81D File Offset: 0x0019DA1D
			public void AdvanceRow()
			{
				if (this._rows.Count > 0)
				{
					this._rows.RemoveAt(0);
				}
			}

			// Token: 0x04004423 RID: 17443
			private int _numColumns = 1;

			// Token: 0x04004424 RID: 17444
			private ArrayList _rows = new ArrayList();
		}

		// Token: 0x02000873 RID: 2163
		internal struct Strip
		{
			// Token: 0x170018D8 RID: 6360
			// (get) Token: 0x0600716C RID: 29036 RVA: 0x0019F853 File Offset: 0x0019DA53
			// (set) Token: 0x0600716D RID: 29037 RVA: 0x0019F85B File Offset: 0x0019DA5B
			public int MinSize
			{
				get
				{
					return this._minSize;
				}
				set
				{
					this._minSize = value;
				}
			}

			// Token: 0x170018D9 RID: 6361
			// (get) Token: 0x0600716E RID: 29038 RVA: 0x0019F864 File Offset: 0x0019DA64
			// (set) Token: 0x0600716F RID: 29039 RVA: 0x0019F86C File Offset: 0x0019DA6C
			public int MaxSize
			{
				get
				{
					return this._maxSize;
				}
				set
				{
					this._maxSize = value;
				}
			}

			// Token: 0x170018DA RID: 6362
			// (get) Token: 0x06007170 RID: 29040 RVA: 0x0019F875 File Offset: 0x0019DA75
			// (set) Token: 0x06007171 RID: 29041 RVA: 0x0019F87D File Offset: 0x0019DA7D
			public bool IsStart
			{
				get
				{
					return this._isStart;
				}
				set
				{
					this._isStart = value;
				}
			}

			// Token: 0x04004425 RID: 17445
			private int _maxSize;

			// Token: 0x04004426 RID: 17446
			private int _minSize;

			// Token: 0x04004427 RID: 17447
			private bool _isStart;
		}
	}
}
