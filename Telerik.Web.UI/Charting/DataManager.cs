using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Charting
{
	// Token: 0x020016EE RID: 5870
	public sealed class DataManager
	{
		// Token: 0x0600E3F6 RID: 58358 RVA: 0x00329414 File Offset: 0x00327614
		private int GetColumnIndex(string column, DataManager.ColumnType columnType)
		{
			if (this.CurrentDataHelper != null && !string.IsNullOrEmpty(column))
			{
				int columnIndex = this.CurrentDataHelper.GetColumnIndex(column);
				bool flag = false;
				if (columnIndex < 0)
				{
					flag = int.TryParse(column, out columnIndex);
				}
				if (columnIndex >= this.CurrentDataHelper.ColumnsCount)
				{
					this.dataManagerDataBindCalled = false;
					throw new ChartException(string.Format("The column for {0} {1} {2} does not exist.", columnType.ToString(), flag ? " source with index " : "with name ", column));
				}
				if (columnIndex >= 0 && !this.CurrentDataHelper.IsColumnNumeric(columnIndex) && columnType != DataManager.ColumnType.Labels && columnType != DataManager.ColumnType.AxisLabels && columnType != DataManager.ColumnType.Groups)
				{
					this.dataManagerDataBindCalled = false;
					throw new ChartException(string.Format("The type of column {0} {1} is not numeric", flag ? "with index " : "with name ", column));
				}
				if (columnIndex >= 0)
				{
					return columnIndex;
				}
			}
			return -1;
		}

		// Token: 0x0600E3F7 RID: 58359 RVA: 0x003294E0 File Offset: 0x003276E0
		private int FindPossibleColumnIndex(int groupColumn, DataManager.ColumnType type)
		{
			switch (type)
			{
			case DataManager.ColumnType.AxisLabels:
			case DataManager.ColumnType.Labels:
				return this.CurrentDataHelper.GetLabelsColumnIndex(groupColumn);
			case DataManager.ColumnType.Groups:
				return this.CurrentDataHelper.GetGroupsColumnIndex();
			case DataManager.ColumnType.YValues:
				if (!this.IsSeriesSupportsX2Y2Values)
				{
					return this.CurrentDataHelper.GetValuesYColumnIndex();
				}
				return this.CurrentDataHelper.GetGanttValuesColumns()[1];
			case DataManager.ColumnType.Y2Values:
				if (!this.IsSeriesSupportsX2Y2Values)
				{
					return -1;
				}
				return this.CurrentDataHelper.GetGanttValuesColumns()[3];
			case DataManager.ColumnType.XValues:
				if (!this.IsSeriesSupportsX2Y2Values)
				{
					return this.CurrentDataHelper.GetValuesXColumnIndex();
				}
				return this.CurrentDataHelper.GetGanttValuesColumns()[0];
			case DataManager.ColumnType.X2Values:
				if (!this.IsSeriesSupportsX2Y2Values)
				{
					return -1;
				}
				return this.CurrentDataHelper.GetGanttValuesColumns()[2];
			}
			return -1;
		}

		// Token: 0x0600E3F8 RID: 58360 RVA: 0x003295AC File Offset: 0x003277AC
		private int GetGroupsColumn(string groupsColumn)
		{
			if (this.dataManagerUseAutoSeriesGrouping)
			{
				return this.GetColumnIndex(groupsColumn, DataManager.ColumnType.Groups);
			}
			return -1;
		}

		// Token: 0x0600E3F9 RID: 58361 RVA: 0x003295C0 File Offset: 0x003277C0
		private int GetLabelsColumn(string labelsColumn)
		{
			return this.GetColumnIndex(labelsColumn, DataManager.ColumnType.Labels);
		}

		// Token: 0x0600E3FA RID: 58362 RVA: 0x003295CC File Offset: 0x003277CC
		private int GetValuesColumn(int groupsColumn, string column, DataManager.ColumnType columnType, bool auto)
		{
			if (this.CurrentDataHelper != null)
			{
				int num = this.GetColumnIndex(column, columnType);
				if (num != groupsColumn || groupsColumn < 0)
				{
					if (num < 0 && auto)
					{
						num = this.FindPossibleColumnIndex(groupsColumn, columnType);
					}
					return num;
				}
			}
			return -1;
		}

		// Token: 0x0600E3FB RID: 58363 RVA: 0x00329608 File Offset: 0x00327808
		private int[] GetValuesYColumns(string[] valuesYColumns, bool auto)
		{
			ArrayList arrayList = new ArrayList();
			if (this.CurrentDataHelper != null)
			{
				if (valuesYColumns != null && valuesYColumns.Length > 0)
				{
					if (valuesYColumns.Length > this.CurrentDataHelper.ColumnsCount)
					{
						this.dataManagerDataBindCalled = false;
						throw new ChartException("The Y values columns count is greater than the data source columns count");
					}
					foreach (string column in valuesYColumns)
					{
						arrayList.Add(this.GetValuesColumn(-1, column, DataManager.ColumnType.YValues, auto));
					}
				}
				else
				{
					arrayList.AddRange(this.CurrentDataHelper.GetValuesYColumns());
				}
			}
			return (int[])arrayList.ToArray(typeof(int));
		}

		// Token: 0x0600E3FC RID: 58364 RVA: 0x003296A0 File Offset: 0x003278A0
		private int GetAxisLabelsColumn(string axisLabelsColumn)
		{
			return this.GetColumnIndex(axisLabelsColumn, DataManager.ColumnType.AxisLabels);
		}

		// Token: 0x0600E3FD RID: 58365 RVA: 0x003296AC File Offset: 0x003278AC
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private string GetItemName(int groupColumn, bool isGroupColumnNumeric, int labelsColumn, int[] valuesYColumns, int row, int column, DataManager.ItemType itemType)
		{
			string text = string.Empty;
			if (this.CurrentDataHelper != null)
			{
				switch (itemType)
				{
				case DataManager.ItemType.Series:
				{
					if (groupColumn >= 0)
					{
						return this.CurrentDataHelper.GetStringValue(row, groupColumn);
					}
					int num = -1;
					if (valuesYColumns != null && column < valuesYColumns.Length)
					{
						num = valuesYColumns[column];
					}
					if (num >= 0)
					{
						text = this.CurrentDataHelper.GetColumnName(num);
						goto IL_71;
					}
					goto IL_71;
				}
				}
				if (labelsColumn >= 0)
				{
					return this.CurrentDataHelper.GetStringValue(row, labelsColumn);
				}
				IL_71:
				if (string.IsNullOrEmpty(text))
				{
					switch (itemType)
					{
					case DataManager.ItemType.Series:
						if (groupColumn >= 0)
						{
							text = itemType + " " + (row + 1);
						}
						else
						{
							text = itemType + " " + (column + 1);
						}
						break;
					case DataManager.ItemType.Item:
						text = itemType + " " + (row + 1);
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x17004593 RID: 17811
		// (get) Token: 0x0600E3FE RID: 58366 RVA: 0x003297A0 File Offset: 0x003279A0
		// (set) Token: 0x0600E3FF RID: 58367 RVA: 0x00329810 File Offset: 0x00327A10
		internal bool UseAutoBind
		{
			get
			{
				if (this.ParentChart.DesignTime)
				{
					return this.dataManagerUseAutoBind;
				}
				foreach (ChartSeries chartSeries in this.ParentChart.Series)
				{
					if (chartSeries.IsDataBound)
					{
						return false;
					}
				}
				return true;
			}
			set
			{
				this.dataManagerUseAutoBind = value;
			}
		}

		// Token: 0x17004594 RID: 17812
		// (get) Token: 0x0600E400 RID: 58368 RVA: 0x0032981C File Offset: 0x00327A1C
		private bool IsChartSupportsXAxisDataBinding
		{
			get
			{
				if (this.ParentChart == null)
				{
					return false;
				}
				switch (this.ParentChart.DefaultType)
				{
				case ChartSeriesType.Bar:
				case ChartSeriesType.StackedBar:
				case ChartSeriesType.StackedBar100:
				case ChartSeriesType.Line:
				case ChartSeriesType.Area:
				case ChartSeriesType.StackedArea:
				case ChartSeriesType.StackedArea100:
				case ChartSeriesType.Gantt:
				case ChartSeriesType.Bezier:
				case ChartSeriesType.Spline:
				case ChartSeriesType.Bubble:
				case ChartSeriesType.Point:
				case ChartSeriesType.SplineArea:
				case ChartSeriesType.StackedSplineArea:
				case ChartSeriesType.StackedSplineArea100:
				case ChartSeriesType.CandleStick:
				case ChartSeriesType.StackedLine:
				case ChartSeriesType.StackedSpline:
					return true;
				}
				return false;
			}
		}

		// Token: 0x17004595 RID: 17813
		// (get) Token: 0x0600E401 RID: 58369 RVA: 0x0032989C File Offset: 0x00327A9C
		private bool IsSeriesSupportsXValues
		{
			get
			{
				switch (this.CurrentSeriesType)
				{
				case ChartSeriesType.Bar:
				case ChartSeriesType.StackedBar:
				case ChartSeriesType.StackedBar100:
				case ChartSeriesType.Line:
				case ChartSeriesType.Area:
				case ChartSeriesType.StackedArea:
				case ChartSeriesType.StackedArea100:
				case ChartSeriesType.Gantt:
				case ChartSeriesType.Bezier:
				case ChartSeriesType.Spline:
				case ChartSeriesType.Bubble:
				case ChartSeriesType.Point:
				case ChartSeriesType.SplineArea:
				case ChartSeriesType.StackedSplineArea:
				case ChartSeriesType.StackedSplineArea100:
				case ChartSeriesType.CandleStick:
				case ChartSeriesType.StackedLine:
				case ChartSeriesType.StackedSpline:
					return true;
				}
				return false;
			}
		}

		// Token: 0x17004596 RID: 17814
		// (get) Token: 0x0600E402 RID: 58370 RVA: 0x00329910 File Offset: 0x00327B10
		private bool IsSeriesSupportsY2Values
		{
			get
			{
				ChartSeriesType currentSeriesType = this.CurrentSeriesType;
				return currentSeriesType == ChartSeriesType.Gantt || currentSeriesType == ChartSeriesType.Bubble || currentSeriesType == ChartSeriesType.CandleStick;
			}
		}

		// Token: 0x17004597 RID: 17815
		// (get) Token: 0x0600E403 RID: 58371 RVA: 0x00329938 File Offset: 0x00327B38
		private bool IsSeriesSupportsX2Values
		{
			get
			{
				ChartSeriesType currentSeriesType = this.CurrentSeriesType;
				return currentSeriesType == ChartSeriesType.Gantt || currentSeriesType == ChartSeriesType.Bubble || currentSeriesType == ChartSeriesType.CandleStick;
			}
		}

		// Token: 0x17004598 RID: 17816
		// (get) Token: 0x0600E404 RID: 58372 RVA: 0x0032995E File Offset: 0x00327B5E
		private bool IsSeriesSupportsX2Y2Values
		{
			get
			{
				return this.IsSeriesSupportsX2Values && this.IsSeriesSupportsY2Values;
			}
		}

		// Token: 0x17004599 RID: 17817
		// (get) Token: 0x0600E405 RID: 58373 RVA: 0x00329970 File Offset: 0x00327B70
		private bool IsSeriesSupportsY3Values
		{
			get
			{
				ChartSeriesType currentSeriesType = this.CurrentSeriesType;
				return currentSeriesType == ChartSeriesType.CandleStick;
			}
		}

		// Token: 0x1700459A RID: 17818
		// (get) Token: 0x0600E406 RID: 58374 RVA: 0x0032998C File Offset: 0x00327B8C
		private bool IsSeriesSupportsY4Values
		{
			get
			{
				ChartSeriesType currentSeriesType = this.CurrentSeriesType;
				return currentSeriesType == ChartSeriesType.CandleStick;
			}
		}

		// Token: 0x0600E407 RID: 58375 RVA: 0x003299A8 File Offset: 0x00327BA8
		private void DataBindXAxes(int groupColumn)
		{
			if (this.CurrentDataHelper != null && this.dataManagerChart.PlotArea.XAxis.IsDataBound)
			{
				this.dataManagerChart.PlotArea.XAxis.Clear();
				int axisLabelsColumn = this.GetAxisLabelsColumn(this.dataManagerChart.PlotArea.XAxis.DataLabelsColumn);
				bool flag = this.CurrentDataHelper.IsColumnNumeric(axisLabelsColumn);
				bool flag2 = this.CurrentDataHelper.IsColumnString(axisLabelsColumn);
				if (axisLabelsColumn >= 0)
				{
					this.dataManagerChart.PlotArea.XAxis.AutoScale = false;
					int num = this.ParentChart.DesignTime ? Math.Min(7, this.CurrentDataHelper.RowsCount) : this.CurrentDataHelper.RowsCount;
					if (groupColumn >= 0)
					{
						object[] filteredColumn = this.CurrentDataHelper.GetFilteredColumn(axisLabelsColumn);
						for (int i = 0; i < filteredColumn.Length; i++)
						{
							if (flag)
							{
								double num2 = Convert.ToDouble(filteredColumn[i]);
								this.dataManagerChart.PlotArea.XAxis.AddItem(this.dataManagerChart.PlotArea.XAxis.FormatLabel(num2), num2);
							}
							else if (flag2)
							{
								this.dataManagerChart.PlotArea.XAxis.AddItem(filteredColumn[i].ToString(), (double)(i + 1));
							}
							if (i == num)
							{
								return;
							}
						}
						return;
					}
					for (int j = 0; j < num; j++)
					{
						if (flag)
						{
							double doubleValue = this.CurrentDataHelper.GetDoubleValue(j, axisLabelsColumn);
							this.dataManagerChart.PlotArea.XAxis.AddItem(this.dataManagerChart.PlotArea.XAxis.FormatLabel(doubleValue), doubleValue);
						}
						else if (flag2)
						{
							this.dataManagerChart.PlotArea.XAxis.AddItem(this.CurrentDataHelper.GetStringValue(j, axisLabelsColumn), (double)(j + 1));
						}
					}
				}
			}
		}

		// Token: 0x0600E408 RID: 58376 RVA: 0x00329B88 File Offset: 0x00327D88
		private bool ItemsEqual(ChartSeriesItem item1, ChartSeriesItem item2)
		{
			return item1.XValue.Equals(item2.XValue) && item1.XValue2.Equals(item2.XValue2) && item1.YValue.Equals(item2.YValue) && item1.YValue2.Equals(item2.YValue2) && item1.YValue3.Equals(item2.YValue3) && item1.YValue4.Equals(item2.YValue4) && item1.Empty == item2.Empty && string.Compare(item1.Name, item2.Name, true) == 0;
		}

		// Token: 0x0600E409 RID: 58377 RVA: 0x00329C44 File Offset: 0x00327E44
		private int DataBindSeries(int rows)
		{
			int groupsColumn = this.GetGroupsColumn(this.dataManagerGroupColumn);
			bool flag = groupsColumn >= 0;
			int num = 0;
			foreach (ChartSeries chartSeries in this.ParentChart.Series)
			{
				if (chartSeries.IsDataBound)
				{
					int labelsColumn = this.GetLabelsColumn(chartSeries.DataLabelsColumn);
					if (labelsColumn < 0)
					{
						labelsColumn = this.GetLabelsColumn(this.dataManagerLabelsColumn);
					}
					this.CurrentSeriesType = chartSeries.Type;
					DataManager.ValuesColumns valuesColumns = new DataManager.ValuesColumns();
					if (!this.IsSeriesSupportsY2Values)
					{
						valuesColumns.Y = this.GetValuesYColumns(new string[]
						{
							chartSeries.DataYColumn
						}, flag);
						valuesColumns.X = (this.IsSeriesSupportsXValues ? this.GetValuesColumn(groupsColumn, chartSeries.DataXColumn, DataManager.ColumnType.XValues, false) : -1);
					}
					else
					{
						valuesColumns.X = this.GetValuesColumn(groupsColumn, chartSeries.DataXColumn, DataManager.ColumnType.XValues, false);
						valuesColumns.Y = new int[]
						{
							this.GetValuesColumn(groupsColumn, chartSeries.DataYColumn, DataManager.ColumnType.YValues, flag)
						};
						if (this.IsSeriesSupportsY2Values)
						{
							valuesColumns.Y2 = this.GetValuesColumn(groupsColumn, chartSeries.DataYColumn2, DataManager.ColumnType.Y2Values, flag);
						}
						if (this.IsSeriesSupportsX2Values)
						{
							valuesColumns.X2 = this.GetValuesColumn(groupsColumn, chartSeries.DataXColumn2, DataManager.ColumnType.X2Values, flag);
						}
						if (this.IsSeriesSupportsY3Values)
						{
							valuesColumns.Y3 = this.GetValuesColumn(groupsColumn, chartSeries.DataYColumn3, DataManager.ColumnType.Y3Values, false);
						}
						if (this.IsSeriesSupportsY4Values)
						{
							valuesColumns.Y4 = this.GetValuesColumn(groupsColumn, chartSeries.DataYColumn4, DataManager.ColumnType.Y4Values, false);
						}
					}
					if (rows > 0)
					{
						if (flag)
						{
							object[] filteredColumn = this.CurrentDataHelper.GetFilteredColumn(groupsColumn);
							object obj = filteredColumn[num];
							int num2 = 0;
							int count = chartSeries.Items.Count;
							for (int i = 0; i < rows; i++)
							{
								object objectValue = this.CurrentDataHelper.GetObjectValue(i, groupsColumn);
								if (objectValue != null && objectValue.Equals(obj))
								{
									int column = valuesColumns.Y.Length - 1;
									ChartSeriesItem chartSeriesItem = this.CreateSeriesItem(i, column, groupsColumn, labelsColumn, valuesColumns, true);
									if (num2 <= count - 1)
									{
										if (!this.ItemsEqual(chartSeriesItem, chartSeries.Items[num2]))
										{
											chartSeries.Items[num2] = chartSeriesItem;
										}
									}
									else
									{
										chartSeries.Items.Add(chartSeriesItem);
									}
									this.OnItemDataBound(chartSeries, chartSeriesItem, this.GetDataItem(i));
									num2++;
								}
							}
							num++;
							if (num >= filteredColumn.Length)
							{
								break;
							}
						}
						else
						{
							for (int j = 0; j < valuesColumns.Y.Length; j++)
							{
								bool flag2 = this.CurrentDataHelper.IsColumnNumeric(valuesColumns.Y[j]);
								if (valuesColumns.Y[j] >= 0 && flag2)
								{
									for (int k = 0; k < rows; k++)
									{
										ChartSeriesItem chartSeriesItem2 = this.CreateSeriesItem(k, j, groupsColumn, labelsColumn, valuesColumns, true);
										if (k <= chartSeries.Items.Count - 1)
										{
											if (!this.ItemsEqual(chartSeriesItem2, chartSeries.Items[k]))
											{
												chartSeries.Items[k] = chartSeriesItem2;
											}
										}
										else
										{
											chartSeries.Items.Add(chartSeriesItem2);
										}
										this.OnItemDataBound(chartSeries, chartSeriesItem2, this.GetDataItem(k));
									}
								}
							}
						}
					}
				}
			}
			return groupsColumn;
		}

		// Token: 0x0600E40A RID: 58378 RVA: 0x00329FAC File Offset: 0x003281AC
		private int DataBindAuto(int rows)
		{
			int groupsColumn = this.GetGroupsColumn(this.dataManagerGroupColumn);
			int labelsColumn = this.GetLabelsColumn(this.dataManagerLabelsColumn);
			this.ParentChart.Series.Clear();
			DataManager.ValuesColumns valuesColumns = new DataManager.ValuesColumns();
			if (!this.IsSeriesSupportsX2Y2Values)
			{
				valuesColumns.Y = this.GetValuesYColumns(this.dataManagerValuesYColumns, true);
				valuesColumns.X = (this.IsSeriesSupportsXValues ? this.GetValuesColumn(groupsColumn, this.dataManagerValuesXColumn, DataManager.ColumnType.XValues, false) : -1);
			}
			else
			{
				string column = string.Empty;
				if (this.dataManagerValuesYColumns != null && this.dataManagerValuesYColumns.Length > 0)
				{
					column = this.dataManagerValuesYColumns[0];
				}
				valuesColumns.X = this.GetValuesColumn(groupsColumn, column, DataManager.ColumnType.XValues, true);
				if (this.dataManagerValuesYColumns != null && this.dataManagerValuesYColumns.Length > 1)
				{
					column = this.dataManagerValuesYColumns[1];
				}
				valuesColumns.Y = new int[]
				{
					this.GetValuesColumn(groupsColumn, column, DataManager.ColumnType.YValues, true)
				};
				if (this.dataManagerValuesYColumns != null && this.dataManagerValuesYColumns.Length > 2)
				{
					column = this.dataManagerValuesYColumns[2];
				}
				valuesColumns.X2 = this.GetValuesColumn(groupsColumn, column, DataManager.ColumnType.X2Values, true);
				if (this.dataManagerValuesYColumns != null && this.dataManagerValuesYColumns.Length > 3)
				{
					column = this.dataManagerValuesYColumns[3];
				}
				valuesColumns.Y2 = this.GetValuesColumn(groupsColumn, column, DataManager.ColumnType.Y2Values, true);
			}
			if (groupsColumn >= 0)
			{
				bool isGroupColumnNumeric = this.CurrentDataHelper.IsColumnNumeric(groupsColumn);
				object[] filteredColumn = this.CurrentDataHelper.GetFilteredColumn(groupsColumn);
				foreach (object obj in filteredColumn)
				{
					ChartSeries chartSeries = null;
					for (int j = 0; j < this.CurrentDataHelper.RowsCount; j++)
					{
						object objectValue = this.CurrentDataHelper.GetObjectValue(j, groupsColumn);
						if (objectValue != null && objectValue.Equals(obj))
						{
							int column2 = valuesColumns.Y.Length - 1;
							if (chartSeries == null)
							{
								chartSeries = this.CreateSeries(groupsColumn, isGroupColumnNumeric, j, column2, valuesColumns);
							}
							if (chartSeries != null)
							{
								ChartSeriesItem chartSeriesItem = this.CreateSeriesItem(j, column2, groupsColumn, labelsColumn, valuesColumns, true);
								chartSeries.Items.Add(chartSeriesItem);
								this.OnItemDataBound(chartSeries, chartSeriesItem, this.GetDataItem(j));
							}
						}
					}
				}
			}
			else
			{
				for (int k = 0; k < valuesColumns.Y.Length; k++)
				{
					ChartSeries chartSeries = this.CreateSeries(-1, false, 0, k, valuesColumns);
					if (this.CurrentDataHelper.IsColumnNumeric(valuesColumns.Y[k]))
					{
						for (int l = 0; l < rows; l++)
						{
							if (chartSeries != null && valuesColumns.Y[k] >= 0)
							{
								ChartSeriesItem chartSeriesItem2 = this.CreateSeriesItem(l, k, -1, labelsColumn, valuesColumns, true);
								chartSeries.Items.Add(chartSeriesItem2);
								this.OnItemDataBound(chartSeries, chartSeriesItem2, this.GetDataItem(l));
							}
						}
					}
				}
			}
			return groupsColumn;
		}

		// Token: 0x0600E40B RID: 58379 RVA: 0x0032A25C File Offset: 0x0032845C
		private ChartSeries CreateSeries(int groupColumn, bool isGroupColumnNumeric, int row, int column, DataManager.ValuesColumns vColumns)
		{
			ChartSeries chartSeries = new ChartSeries();
			chartSeries.DataYColumn = ((column >= 0) ? this.CurrentDataHelper.GetColumnName(vColumns.Y[column]) : string.Empty);
			chartSeries.DataXColumn = ((vColumns.X >= 0) ? this.CurrentDataHelper.GetColumnName(vColumns.X) : string.Empty);
			if (this.IsSeriesSupportsX2Y2Values)
			{
				chartSeries.DataXColumn2 = ((vColumns.X2 >= 0) ? this.CurrentDataHelper.GetColumnName(vColumns.X2) : string.Empty);
				chartSeries.DataYColumn2 = ((vColumns.Y2 >= 0) ? this.CurrentDataHelper.GetColumnName(vColumns.Y2) : string.Empty);
			}
			chartSeries.Name = this.GetItemName(groupColumn, isGroupColumnNumeric, -1, vColumns.Y, row, column, DataManager.ItemType.Series);
			chartSeries.Type = this.ParentChart.DefaultType;
			chartSeries.Appearance.LabelAppearance.Chart = (chartSeries.Appearance.PointMark.Chart = this.ParentChart);
			this.ParentChart.Series.Add(chartSeries);
			return chartSeries;
		}

		// Token: 0x0600E40C RID: 58380 RVA: 0x0032A380 File Offset: 0x00328580
		private ChartSeriesItem CreateSeriesItem(int row, int column, int groupColumn, int labelsColumn, DataManager.ValuesColumns vColumns, bool useLabels)
		{
			ChartSeriesItem chartSeriesItem = new ChartSeriesItem();
			if (this.CurrentDataHelper != null)
			{
				double doubleValue = this.CurrentDataHelper.GetDoubleValue(row, vColumns.Y[column]);
				chartSeriesItem.Empty = doubleValue.Equals(double.NaN);
				chartSeriesItem.YValue = (doubleValue.Equals(double.NaN) ? 0.0 : doubleValue);
				if (this.IsSeriesSupportsXValues && vColumns.X >= 0)
				{
					chartSeriesItem.XValue = this.CurrentDataHelper.GetDoubleValue(row, vColumns.X);
				}
				if (this.IsSeriesSupportsX2Values && vColumns.X2 >= 0)
				{
					chartSeriesItem.XValue2 = this.CurrentDataHelper.GetDoubleValue(row, vColumns.X2);
				}
				if (this.IsSeriesSupportsY2Values && vColumns.Y2 >= 0)
				{
					chartSeriesItem.YValue2 = this.CurrentDataHelper.GetDoubleValue(row, vColumns.Y2);
				}
				if (this.IsSeriesSupportsY3Values && vColumns.Y3 >= 0)
				{
					chartSeriesItem.YValue3 = this.CurrentDataHelper.GetDoubleValue(row, vColumns.Y3);
				}
				if (this.IsSeriesSupportsY4Values && vColumns.Y4 >= 0)
				{
					chartSeriesItem.YValue4 = this.CurrentDataHelper.GetDoubleValue(row, vColumns.Y4);
				}
				if (useLabels)
				{
					chartSeriesItem.Name = this.GetItemName(groupColumn, false, labelsColumn, vColumns.Y, row, column, DataManager.ItemType.Item);
					if (labelsColumn >= 0)
					{
						chartSeriesItem.Label.TextBlock.Text = chartSeriesItem.Name;
					}
				}
				chartSeriesItem.Label.Appearance.Chart = (chartSeriesItem.PointAppearance.Chart = this.ParentChart);
			}
			return chartSeriesItem;
		}

		// Token: 0x0600E40D RID: 58381 RVA: 0x0032A524 File Offset: 0x00328724
		private object GetDataItem(int row)
		{
			if (this.CurrentDataHelper == null)
			{
				return null;
			}
			if (this.CurrentDataHelper is DataTableDataHelper)
			{
				DataTableDataHelper dataTableDataHelper = (DataTableDataHelper)this.CurrentDataHelper;
				return dataTableDataHelper.DataTable.DefaultView[row];
			}
			if (this.CurrentDataHelper is ListDataHelper)
			{
				ListDataHelper listDataHelper = (ListDataHelper)this.CurrentDataHelper;
				return listDataHelper.data[row];
			}
			if (!(this.CurrentDataHelper is ArrayDataHelper))
			{
				return null;
			}
			ArrayDataHelper arrayDataHelper = (ArrayDataHelper)this.CurrentDataHelper;
			if (arrayDataHelper.data.Rank == 2)
			{
				int length = arrayDataHelper.data.GetLength(1);
				object[] array = new object[length];
				for (int i = 0; i < length; i++)
				{
					array[i] = arrayDataHelper.data.GetValue(row, i);
				}
				return array;
			}
			if (arrayDataHelper.data.Rank == 1)
			{
				return arrayDataHelper.data.GetValue(row);
			}
			this.dataManagerDataBindCalled = false;
			throw new ChartException("Data binding to arrays with Rank of 3 or more is not supported.");
		}

		// Token: 0x0600E40E RID: 58382 RVA: 0x0032A61F File Offset: 0x0032881F
		internal void ValidateDataSource(object dataSource)
		{
			if (dataSource != null && !(dataSource is IEnumerable) && !(dataSource is IListSource))
			{
				throw new Exception("Given data source is not supported");
			}
		}

		// Token: 0x1700459B RID: 17819
		// (get) Token: 0x0600E40F RID: 58383 RVA: 0x0032A63F File Offset: 0x0032883F
		// (set) Token: 0x0600E410 RID: 58384 RVA: 0x0032A647 File Offset: 0x00328847
		private ChartSeriesType CurrentSeriesType
		{
			get
			{
				return this.dataManagerCurrentSeriesType;
			}
			set
			{
				this.dataManagerCurrentSeriesType = value;
			}
		}

		// Token: 0x1700459C RID: 17820
		// (get) Token: 0x0600E411 RID: 58385 RVA: 0x0032A650 File Offset: 0x00328850
		// (set) Token: 0x0600E412 RID: 58386 RVA: 0x0032A658 File Offset: 0x00328858
		internal ICommonDataHelper CurrentDataHelper
		{
			get
			{
				return this.dataManagerDataHelper;
			}
			set
			{
				this.dataManagerDataHelper = value;
			}
		}

		// Token: 0x1700459D RID: 17821
		// (get) Token: 0x0600E413 RID: 58387 RVA: 0x0032A661 File Offset: 0x00328861
		// (set) Token: 0x0600E414 RID: 58388 RVA: 0x0032A669 File Offset: 0x00328869
		internal Chart ParentChart
		{
			get
			{
				return this.dataManagerChart;
			}
			set
			{
				this.dataManagerChart = value;
			}
		}

		// Token: 0x140001C2 RID: 450
		// (add) Token: 0x0600E415 RID: 58389 RVA: 0x0032A674 File Offset: 0x00328874
		// (remove) Token: 0x0600E416 RID: 58390 RVA: 0x0032A6AC File Offset: 0x003288AC
		public event EventHandler<ChartItemDataBoundEventArgs> ItemDataBound;

		// Token: 0x0600E417 RID: 58391 RVA: 0x0032A6E4 File Offset: 0x003288E4
		private void OnItemDataBound(ChartSeries chartSeries, ChartSeriesItem chartSeriesItem, object dataItem)
		{
			ChartItemDataBoundEventArgs e = new ChartItemDataBoundEventArgs(chartSeriesItem, chartSeries, dataItem);
			if (this.ItemDataBound != null)
			{
				this.ItemDataBound(this, e);
			}
		}

		// Token: 0x1700459E RID: 17822
		// (get) Token: 0x0600E418 RID: 58392 RVA: 0x0032A70F File Offset: 0x0032890F
		// (set) Token: 0x0600E419 RID: 58393 RVA: 0x0032A717 File Offset: 0x00328917
		[NotifyParentProperty(true)]
		public object DataSource
		{
			get
			{
				return this.dataManagerDataSource;
			}
			set
			{
				if (value != null)
				{
					this.ValidateDataSource(value);
					if (!this.dataManagerChart.DesignTime)
					{
						this.dataManagerChart.Series.ClearItems();
					}
				}
				this.dataManagerDataBindCalled = false;
				this.dataManagerDataSource = value;
			}
		}

		// Token: 0x1700459F RID: 17823
		// (get) Token: 0x0600E41A RID: 58394 RVA: 0x0032A74E File Offset: 0x0032894E
		// (set) Token: 0x0600E41B RID: 58395 RVA: 0x0032A758 File Offset: 0x00328958
		[NotifyParentProperty(true)]
		public string DataMember
		{
			get
			{
				return this.dataManagerDataMember;
			}
			set
			{
				if (value != null)
				{
					Array array = value.ToString().Split(new char[]
					{
						'.'
					});
					this.dataManagerDataMember = array.GetValue(0).ToString();
					return;
				}
				this.dataManagerDataMember = string.Empty;
			}
		}

		// Token: 0x0600E41C RID: 58396 RVA: 0x0032A7A0 File Offset: 0x003289A0
		public void DataBind()
		{
			this.dataManagerGroupColumn = this.ParentChart.DataGroupColumn;
			if (this.dataManagerDataSource != null)
			{
				this.CurrentDataHelper = DataHelper.CreateDataHelper(this.dataManagerDataSource, this.dataManagerDataMember, this.ParentChart.DesignTime);
				if (this.CurrentDataHelper != null && this.CurrentDataHelper.ColumnsCount > 0)
				{
					this.CurrentSeriesType = this.ParentChart.DefaultType;
					this.dataManagerDataBindCalled = true;
					int rows = this.ParentChart.DesignTime ? Math.Min(7, this.CurrentDataHelper.RowsCount) : this.CurrentDataHelper.RowsCount;
					int num;
					if (this.UseAutoBind)
					{
						num = this.DataBindAuto(rows);
					}
					else
					{
						num = this.DataBindSeries(rows);
					}
					foreach (ChartSeries chartSeries in this.ParentChart.Series)
					{
						chartSeries.SetFormattedLegendItemText();
					}
					string columnName = this.CurrentDataHelper.GetColumnName(num);
					if (!string.IsNullOrEmpty(columnName))
					{
						this.ParentChart.DataGroupColumn = columnName;
					}
					if (this.IsChartSupportsXAxisDataBinding)
					{
						this.DataBindXAxes(num);
					}
				}
			}
		}

		// Token: 0x0600E41D RID: 58397 RVA: 0x0032A8E0 File Offset: 0x00328AE0
		public void ClearDataSource()
		{
			this.dataManagerDataSource = null;
			this.dataManagerDataHelper = null;
			this.dataManagerDataBindCalled = false;
		}

		// Token: 0x170045A0 RID: 17824
		// (get) Token: 0x0600E41E RID: 58398 RVA: 0x0032A8F7 File Offset: 0x00328AF7
		// (set) Token: 0x0600E41F RID: 58399 RVA: 0x0032A8FF File Offset: 0x00328AFF
		public bool IsDataBindCalled
		{
			get
			{
				return this.dataManagerDataBindCalled;
			}
			set
			{
				this.dataManagerDataBindCalled = value;
			}
		}

		// Token: 0x170045A1 RID: 17825
		// (get) Token: 0x0600E420 RID: 58400 RVA: 0x0032A908 File Offset: 0x00328B08
		// (set) Token: 0x0600E421 RID: 58401 RVA: 0x0032A910 File Offset: 0x00328B10
		public string LabelsColumn
		{
			get
			{
				return this.dataManagerLabelsColumn;
			}
			set
			{
				this.dataManagerLabelsColumn = value;
			}
		}

		// Token: 0x170045A2 RID: 17826
		// (get) Token: 0x0600E422 RID: 58402 RVA: 0x0032A919 File Offset: 0x00328B19
		// (set) Token: 0x0600E423 RID: 58403 RVA: 0x0032A921 File Offset: 0x00328B21
		public string ValuesXColumn
		{
			get
			{
				return this.dataManagerValuesXColumn;
			}
			set
			{
				this.dataManagerValuesXColumn = value;
			}
		}

		// Token: 0x170045A3 RID: 17827
		// (get) Token: 0x0600E424 RID: 58404 RVA: 0x0032A92A File Offset: 0x00328B2A
		// (set) Token: 0x0600E425 RID: 58405 RVA: 0x0032A932 File Offset: 0x00328B32
		public string[] ValuesYColumns
		{
			get
			{
				return this.dataManagerValuesYColumns;
			}
			set
			{
				this.dataManagerValuesYColumns = value;
			}
		}

		// Token: 0x170045A4 RID: 17828
		// (get) Token: 0x0600E426 RID: 58406 RVA: 0x0032A93B File Offset: 0x00328B3B
		// (set) Token: 0x0600E427 RID: 58407 RVA: 0x0032A943 File Offset: 0x00328B43
		public bool UseSeriesGrouping
		{
			get
			{
				return this.dataManagerUseAutoSeriesGrouping;
			}
			set
			{
				this.dataManagerUseAutoSeriesGrouping = value;
			}
		}

		// Token: 0x0600E428 RID: 58408 RVA: 0x0032A94C File Offset: 0x00328B4C
		public void CopyFrom(DataManager manager)
		{
			this.DataMember = manager.DataMember;
			this.DataSource = manager.DataSource;
			this.LabelsColumn = manager.LabelsColumn;
			this.ValuesXColumn = manager.ValuesXColumn;
			this.ValuesYColumns = manager.ValuesYColumns;
		}

		// Token: 0x0600E429 RID: 58409 RVA: 0x0032A98C File Offset: 0x00328B8C
		internal DataManager(Chart chart)
		{
			this.dataManagerUseAutoSeriesGrouping = true;
			this.dataManagerDataMember = string.Empty;
			this.dataManagerGroupColumn = string.Empty;
			this.dataManagerLabelsColumn = string.Empty;
			this.dataManagerValuesXColumn = string.Empty;
			this.dataManagerChart = chart;
			this.CurrentSeriesType = chart.DefaultType;
		}

		// Token: 0x040041BF RID: 16831
		private const int DESIGN_ROWS_AFFECTED = 7;

		// Token: 0x040041C0 RID: 16832
		private Chart dataManagerChart;

		// Token: 0x040041C1 RID: 16833
		private object dataManagerDataSource;

		// Token: 0x040041C2 RID: 16834
		private string dataManagerDataMember;

		// Token: 0x040041C3 RID: 16835
		private bool dataManagerDataBindCalled;

		// Token: 0x040041C4 RID: 16836
		private bool dataManagerUseAutoBind;

		// Token: 0x040041C5 RID: 16837
		private bool dataManagerUseAutoSeriesGrouping;

		// Token: 0x040041C6 RID: 16838
		private string dataManagerGroupColumn;

		// Token: 0x040041C7 RID: 16839
		private string dataManagerLabelsColumn;

		// Token: 0x040041C8 RID: 16840
		private string dataManagerValuesXColumn;

		// Token: 0x040041C9 RID: 16841
		private string[] dataManagerValuesYColumns;

		// Token: 0x040041CA RID: 16842
		private ChartSeriesType dataManagerCurrentSeriesType;

		// Token: 0x040041CB RID: 16843
		private ICommonDataHelper dataManagerDataHelper;

		// Token: 0x020016EF RID: 5871
		private class ValuesColumns
		{
			// Token: 0x040041CD RID: 16845
			internal int[] Y;

			// Token: 0x040041CE RID: 16846
			internal int X;

			// Token: 0x040041CF RID: 16847
			internal int X2 = -1;

			// Token: 0x040041D0 RID: 16848
			internal int Y2 = -1;

			// Token: 0x040041D1 RID: 16849
			internal int Y3 = -1;

			// Token: 0x040041D2 RID: 16850
			internal int Y4 = -1;
		}

		// Token: 0x020016F0 RID: 5872
		private enum ColumnType
		{
			// Token: 0x040041D4 RID: 16852
			AxisLabels,
			// Token: 0x040041D5 RID: 16853
			Groups,
			// Token: 0x040041D6 RID: 16854
			Labels,
			// Token: 0x040041D7 RID: 16855
			YValues,
			// Token: 0x040041D8 RID: 16856
			Y2Values,
			// Token: 0x040041D9 RID: 16857
			Y3Values,
			// Token: 0x040041DA RID: 16858
			Y4Values,
			// Token: 0x040041DB RID: 16859
			XValues,
			// Token: 0x040041DC RID: 16860
			X2Values
		}

		// Token: 0x020016F1 RID: 5873
		private enum ItemType
		{
			// Token: 0x040041DE RID: 16862
			Series,
			// Token: 0x040041DF RID: 16863
			Item
		}
	}
}
