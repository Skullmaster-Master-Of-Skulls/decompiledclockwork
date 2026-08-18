using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020010ED RID: 4333
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	internal class GridDataTableFromEnumerable : GridResolveEnumerable
	{
		// Token: 0x0600B180 RID: 45440 RVA: 0x00266960 File Offset: 0x00264B60
		public GridDataTableFromEnumerable(GridTableView owner, IEnumerable rawEnumerable, bool generateColumns, bool generateDataTable, GridColumnCollection existingColumns, string[] additionalFields, bool retrieveAllFields, bool enableSplitHeaderText) : base(rawEnumerable, generateDataTable)
		{
			this.existingColumns = existingColumns;
			this.autoGenerateGridColumns = generateColumns;
			if (additionalFields != null)
			{
				this.additionalFields = new ArrayList(additionalFields);
			}
			this.retrieveAllFields = retrieveAllFields;
			this.enableSplitHeaderText = enableSplitHeaderText;
			this.owner = owner;
		}

		// Token: 0x0600B181 RID: 45441 RVA: 0x002669E0 File Offset: 0x00264BE0
		private static Type GetUnderlyingType(Type type)
		{
			if (type.IsGenericType && type.IsValueType && type.GetGenericArguments().Length == 1)
			{
				return type.GetGenericArguments()[0];
			}
			if (type.IsEnum)
			{
				return typeof(string);
			}
			return type;
		}

		// Token: 0x0600B182 RID: 45442 RVA: 0x00266A1A File Offset: 0x00264C1A
		public GridInsertionObject GetNewInsertionObject()
		{
			return new GridInsertionObject(this.properties);
		}

		// Token: 0x0600B183 RID: 45443 RVA: 0x00266A28 File Offset: 0x00264C28
		private void PrepareExistingColumn(string dataField, Type dataType)
		{
			if (this.existingColumns == null)
			{
				return;
			}
			foreach (GridColumn gridColumn in this.existingColumns.FindAllByDataField(dataField))
			{
				if (!gridColumn.DataTypeIsSet)
				{
					gridColumn.DataType = dataType;
					if (gridColumn.IsClone && !gridColumn.OriginalColumn.DataTypeIsSet)
					{
						gridColumn.OriginalColumn.DataType = gridColumn.DataType;
					}
				}
			}
		}

		// Token: 0x0600B184 RID: 45444 RVA: 0x00266A92 File Offset: 0x00264C92
		protected bool ShouldCreateDataColumn(Type dataFieldType, string dataFieldName)
		{
			return GridBaseDataList.IsBindableType(dataFieldType) && (this.retrieveAllFields || this.autoGenerateGridColumns || this.existingColumns.FindByDataFieldSafe(dataFieldName) != null || GridDataTableFromEnumerable.FindFieldByName(this.additionalFields, dataFieldName));
		}

		// Token: 0x0600B185 RID: 45445 RVA: 0x00266ACC File Offset: 0x00264CCC
		private static bool FindFieldByName(ArrayList list, string fieldName)
		{
			foreach (object obj in list)
			{
				string strA = (string)obj;
				if (string.Compare(strA, fieldName, true) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600B186 RID: 45446 RVA: 0x00266B2C File Offset: 0x00264D2C
		protected override void FinishedParsingProperties(object dataItemInstance)
		{
			base.FinishedParsingProperties(dataItemInstance);
			if (this.HasExistingColumns)
			{
				this.autoGenerateGridColumns = false;
				foreach (object obj in this.existingColumns)
				{
					GridColumn gridColumn = (GridColumn)obj;
					IDictionary customPropertyDataFields = gridColumn.GetCustomPropertyDataFields(dataItemInstance);
					if (customPropertyDataFields != null)
					{
						foreach (object obj2 in customPropertyDataFields)
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
							if (!this._table.Columns.Contains(dictionaryEntry.Key.ToString()))
							{
								if (dictionaryEntry.Value.GetType() == typeof(string))
								{
									this.CreateColumn(new DataColumn
									{
										Caption = gridColumn.HeaderText,
										DataType = gridColumn.DataType,
										ColumnName = (string)dictionaryEntry.Value
									});
								}
								else if (dictionaryEntry.Value is PropertyDescriptor)
								{
									this.CreateColumn((PropertyDescriptor)dictionaryEntry.Value, (string)dictionaryEntry.Key);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x1700397C RID: 14716
		// (get) Token: 0x0600B187 RID: 45447 RVA: 0x00266CB8 File Offset: 0x00264EB8
		private bool HasExistingColumns
		{
			get
			{
				return this.existingColumns != null && this.existingColumns.Count > 0;
			}
		}

		// Token: 0x0600B188 RID: 45448 RVA: 0x00266CD2 File Offset: 0x00264ED2
		protected override void OnNoBindableProperties()
		{
			if (!this.HasExistingColumns)
			{
				base.OnNoBindableProperties();
			}
		}

		// Token: 0x0600B189 RID: 45449 RVA: 0x00266CE2 File Offset: 0x00264EE2
		protected override void CreateColumn(PropertyDescriptor descriptor)
		{
			this.CreateColumn(descriptor, descriptor.Name);
		}

		// Token: 0x0600B18A RID: 45450 RVA: 0x00266CF4 File Offset: 0x00264EF4
		protected void CreateColumn(PropertyDescriptor descriptor, string fieldName)
		{
			if (this.ShouldCreateDataColumn(descriptor.PropertyType, fieldName))
			{
				GridDataColumn gridDataColumn = new GridDataColumn();
				gridDataColumn.ColumnName = fieldName;
				gridDataColumn.ReadOnly = descriptor.IsReadOnly;
				gridDataColumn.DataType = GridDataTableFromEnumerable.GetUnderlyingType(descriptor.PropertyType);
				this._table.Columns.Add(gridDataColumn);
				if (this.autoGenerateGridColumns)
				{
					this.CreateColumnInternal(fieldName, fieldName, new bool?(descriptor.IsReadOnly), gridDataColumn);
				}
				this.PrepareExistingColumn(fieldName, gridDataColumn.DataType);
				this.properties.Add(new GridPropertyDescriptor(fieldName, descriptor));
			}
		}

		// Token: 0x0600B18B RID: 45451 RVA: 0x00266D88 File Offset: 0x00264F88
		private bool IsNumericType(Type type)
		{
			return typeof(decimal) == type || typeof(double) == type || typeof(short) == type || typeof(int) == type || typeof(long) == type || typeof(float) == type;
		}

		// Token: 0x0600B18C RID: 45452 RVA: 0x00266E01 File Offset: 0x00265001
		public static string ToSeparatedWords(string value)
		{
			if (value == null)
			{
				return value;
			}
			return GridDataTableFromEnumerable.NameExpression.Replace(value, " $1").Trim();
		}

		// Token: 0x0600B18D RID: 45453 RVA: 0x00266E20 File Offset: 0x00265020
		private void CreateColumnInternal(string headerText, string fieldName, bool? isReadOnly, GridDataColumn column)
		{
			Regex regex = new Regex("^([A-Za-z]+)$");
			if (this.enableSplitHeaderText && regex.IsMatch(headerText))
			{
				headerText = GridDataTableFromEnumerable.ToSeparatedWords(headerText);
			}
			if (column.DataType == typeof(DateTime))
			{
				GridDateTimeColumn gridDateTimeColumn = new GridDateTimeColumn();
				((IStateManager)gridDateTimeColumn).TrackViewState();
				gridDateTimeColumn.HeaderText = headerText;
				gridDateTimeColumn.DataField = fieldName;
				gridDateTimeColumn.SortExpression = fieldName;
				if (isReadOnly != null)
				{
					gridDateTimeColumn.ReadOnly = isReadOnly.Value;
				}
				gridDateTimeColumn.DataType = column.DataType;
				this._list.Add(gridDateTimeColumn);
				return;
			}
			if (this.IsNumericType(column.DataType))
			{
				GridNumericColumn gridNumericColumn = new GridNumericColumn();
				((IStateManager)gridNumericColumn).TrackViewState();
				gridNumericColumn.HeaderText = headerText;
				gridNumericColumn.DataField = fieldName;
				gridNumericColumn.SortExpression = fieldName;
				if (isReadOnly != null)
				{
					gridNumericColumn.ReadOnly = isReadOnly.Value;
				}
				gridNumericColumn.DataType = column.DataType;
				this._list.Add(gridNumericColumn);
				return;
			}
			if (column.DataType == typeof(bool))
			{
				GridCheckBoxColumn gridCheckBoxColumn = new GridCheckBoxColumn();
				((IStateManager)gridCheckBoxColumn).TrackViewState();
				gridCheckBoxColumn.HeaderText = headerText;
				gridCheckBoxColumn.DataField = fieldName;
				gridCheckBoxColumn.SortExpression = fieldName;
				if (isReadOnly != null)
				{
					gridCheckBoxColumn.ReadOnly = isReadOnly.Value;
				}
				gridCheckBoxColumn.DataType = column.DataType;
				this._list.Add(gridCheckBoxColumn);
				return;
			}
			GridBoundColumn gridBoundColumn = new GridBoundColumn();
			((IStateManager)gridBoundColumn).TrackViewState();
			gridBoundColumn.HeaderText = headerText;
			gridBoundColumn.DataField = fieldName;
			gridBoundColumn.SortExpression = fieldName;
			if (isReadOnly != null)
			{
				gridBoundColumn.ReadOnly = isReadOnly.Value;
			}
			gridBoundColumn.DataType = column.DataType;
			this._list.Add(gridBoundColumn);
		}

		// Token: 0x0600B18E RID: 45454 RVA: 0x00266FE0 File Offset: 0x002651E0
		protected override void CreateColumn(DataColumn fromColumn)
		{
			if (this.ShouldCreateDataColumn(fromColumn.DataType, fromColumn.ColumnName))
			{
				GridDataColumn gridDataColumn = new GridDataColumn();
				gridDataColumn.ColumnName = fromColumn.ColumnName;
				gridDataColumn.DataType = fromColumn.DataType;
				gridDataColumn.ReadOnly = fromColumn.ReadOnly;
				gridDataColumn.Expression = fromColumn.Expression;
				this._table.Columns.Add(gridDataColumn);
				if (this.autoGenerateGridColumns)
				{
					this.CreateColumnInternal(fromColumn.Caption, fromColumn.ColumnName, new bool?(fromColumn.ReadOnly), gridDataColumn);
				}
				this.PrepareExistingColumn(fromColumn.ColumnName, fromColumn.DataType);
				this.properties.Add(new GridPropertyDescriptor(fromColumn.ColumnName, fromColumn.ReadOnly, fromColumn.DataType));
			}
		}

		// Token: 0x0600B18F RID: 45455 RVA: 0x002670A8 File Offset: 0x002652A8
		protected override void CreateColumn(Type type)
		{
			if (this.ShouldCreateDataColumn(type, "Item"))
			{
				GridDataColumn gridDataColumn = new GridDataColumn();
				gridDataColumn.ColumnName = "Item";
				gridDataColumn.DataType = GridDataTableFromEnumerable.GetUnderlyingType(type);
				gridDataColumn.IsPrimitive = true;
				this._table.Columns.Add(gridDataColumn);
				if (this.autoGenerateGridColumns)
				{
					this.CreateColumnInternal("Item", "Item", null, gridDataColumn);
				}
				this.PrepareExistingColumn("Item", gridDataColumn.DataType);
				this.properties.Add(new GridPropertyDescriptor("Item", gridDataColumn.ReadOnly, gridDataColumn.DataType));
			}
		}

		// Token: 0x1700397D RID: 14717
		// (get) Token: 0x0600B190 RID: 45456 RVA: 0x00267150 File Offset: 0x00265350
		protected override int ColumnsCount
		{
			get
			{
				return this._table.Columns.Count;
			}
		}

		// Token: 0x0600B191 RID: 45457 RVA: 0x00267164 File Offset: 0x00265364
		internal static IEnumerable ToGenericEnumerable(IEnumerable source, GridTableView tableView)
		{
			Type type = typeof(object);
			bool flag = false;
			bool flag2 = false;
			Type type2 = null;
			AdvancedEnumerable advancedEnumerable = source as AdvancedEnumerable;
			if (advancedEnumerable != null && advancedEnumerable.originalEnumerator.GetType().IsGenericType && !tableView.RetrieveDataTypeFromFirstItem)
			{
				Type[] genericArguments = advancedEnumerable.originalEnumerator.GetType().GetGenericArguments();
				if (genericArguments.Length == 1)
				{
					flag2 = true;
					type2 = genericArguments[0];
				}
			}
			IQueryable queryable = source as IQueryable;
			if (queryable != null)
			{
				type = queryable.ElementType;
			}
			else if (flag2 && type2 != null && type2.Name != "EntityDataSourceWrapper")
			{
				type = type2;
			}
			else
			{
				IEnumerator enumerator = source.GetEnumerator();
				if (enumerator.MoveNext() && enumerator.Current != null)
				{
					type = enumerator.Current.GetType();
					if (type != null && type.Name == "EntityDataSourceWrapper" && enumerator.Current is ICustomTypeDescriptor)
					{
						object propertyOwner = (enumerator.Current as ICustomTypeDescriptor).GetPropertyOwner(null);
						type = propertyOwner.GetType();
						flag = true;
					}
				}
				if (!(source is EnumerableRowCollection<DataRow>))
				{
					source.GetEnumerator().Reset();
				}
			}
			Type type3 = typeof(GridDataTableFromEnumerable.GridGenericEnumerable<>).MakeGenericType(new Type[]
			{
				type
			});
			if (flag)
			{
				type3 = typeof(GridDataTableFromEnumerable.GridEntityGenericEnumerable<>).MakeGenericType(new Type[]
				{
					type
				});
			}
			return (IEnumerable)Activator.CreateInstance(type3, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new object[]
			{
				source
			}, null);
		}

		// Token: 0x0600B192 RID: 45458 RVA: 0x002672F0 File Offset: 0x002654F0
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		internal static string TransformDataFieldName(string fieldName, Type type)
		{
			if (type == typeof(DataRowView) || type == typeof(DataRow) || type.GetInterface("IDataRecord") != null)
			{
				fieldName = string.Format("iif(it[\"{0}\"] == Convert.DBNull, null, it[\"{0}\"])", fieldName);
			}
			if (GridBaseDataList.IsBindableType(type))
			{
				fieldName = "it";
			}
			return fieldName;
		}

		// Token: 0x0600B193 RID: 45459 RVA: 0x00267354 File Offset: 0x00265554
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		internal void FillDataTableFromEnumerable(IQueryable enumerable)
		{
			GridPropertyEvaluator gridPropertyEvaluator = new GridPropertyEvaluator();
			this._table.MinimumCapacity = int.MaxValue;
			this._table.BeginLoadData();
			List<string> columnsToUse = this.GetColumnsToUse();
			this._table.Columns.Add(new GridDataColumn("OriginalDataItem", typeof(object)));
			foreach (object obj in enumerable)
			{
				DataRow dataRow = this._table.NewRow();
				dataRow["OriginalDataItem"] = obj;
				if (this.owner.CurrentDataSource is DataRow[] && obj is DataRow)
				{
					dataRow["OriginalDataItem"] = DBNull.Value;
				}
				foreach (string text in columnsToUse)
				{
					if (!(text == "OriginalDataItem"))
					{
						GridDataColumn gridDataColumn = (GridDataColumn)this._table.Columns[text];
						if (gridDataColumn.IsPrimitive)
						{
							dataRow[text] = obj;
						}
						else
						{
							object value = null;
							DataRow dataRow2 = obj as DataRow;
							if (dataRow2 != null)
							{
								try
								{
									value = DataBinder.Eval(obj, text);
									goto IL_152;
								}
								catch (Exception ex)
								{
									string message = ex.Message;
									value = dataRow2[text];
									goto IL_152;
								}
								goto IL_129;
							}
							goto IL_129;
							IL_152:
							value = this.ResolveDbNull(text, obj, value);
							dataRow[text] = value;
							continue;
							IL_129:
							if (obj is ICustomTypeDescriptor)
							{
								value = GridPropertyEvaluator.GetPropertyValue(obj, text, DBNull.Value);
								goto IL_152;
							}
							value = gridPropertyEvaluator.GetCachedPropertyValue(obj, text, DBNull.Value);
							goto IL_152;
						}
					}
				}
				this._table.LoadDataRow(dataRow.ItemArray, false);
			}
			this._table.EndLoadData();
		}

		// Token: 0x0600B194 RID: 45460 RVA: 0x00267574 File Offset: 0x00265774
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		[SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)")]
		protected void FillData35()
		{
			if (!this.owner.OwnerGrid.IsDesignMode)
			{
				IEnumerable enumerable = base.enumerable;
				IQueryable queryable = GridDataTableFromEnumerable.ToGenericEnumerable(enumerable, this.owner).AsQueryable();
				int num = (this.owner.CurrentDataSource is ICollection && string.IsNullOrEmpty(this.owner.FilterExpression)) ? ((ICollection)this.owner.CurrentDataSource).Count : queryable.Count();
				enumerable.GetEnumerator().Reset();
				if (num == 0)
				{
					return;
				}
				this.owner.originalQueryable = queryable;
				if (queryable.ElementType.BaseType != null && queryable.ElementType.BaseType.FullName != "Telerik.Web.UI.DynamicClass" && queryable.ElementType.BaseType.FullName != "System.Web.Query.Dynamic.DynamicClass")
				{
					this.owner.QueryableElementType = queryable.ElementType;
				}
				bool flag = false;
				List<string> list = new List<string>();
				foreach (object obj in this._table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					string item = string.Format("{0} as {1}", GridDataTableFromEnumerable.TransformDataFieldName(dataColumn.ColumnName, queryable.ElementType), dataColumn.ColumnName);
					list.Add(item);
				}
				foreach (GridColumn gridColumn in this.owner.RenderColumns)
				{
					GridCalculatedColumn gridCalculatedColumn = gridColumn as GridCalculatedColumn;
					if (gridCalculatedColumn != null)
					{
						flag = true;
						string[] dataFields = gridCalculatedColumn.DataFields;
						List<string> list2 = new List<string>();
						foreach (string text in dataFields)
						{
							if (!this._table.Columns.Contains(text))
							{
								throw new GridException(string.Format("DataField \"{0}\" for GridCalculatedColumn \"{1}\" does not exist in current DataSource.", text, gridColumn.UniqueName));
							}
							string arg = this._table.Columns[text].DataType.ToString().Split(new char[]
							{
								'.'
							})[1];
							if (this._table.Columns[text].DataType != typeof(string) && this._table.Columns[text].DataType != typeof(object))
							{
								list2.Add(string.Format("{0}?", arg));
							}
							else if (this._table.Columns[text].DataType != typeof(string))
							{
								list2.Add("object");
							}
							else
							{
								list2.Add("Convert.ToString");
							}
						}
						string arg2 = gridCalculatedColumn.FormatExpression(list2);
						list.Add(string.Format("{0} as {1}", arg2, string.Format("{0}Result", gridColumn.UniqueName)));
					}
					GridBinaryImageColumn gridBinaryImageColumn = gridColumn as GridBinaryImageColumn;
					if (gridBinaryImageColumn != null)
					{
						string item2 = string.Format("{0} as {1}", GridDataTableFromEnumerable.TransformDataFieldName(gridBinaryImageColumn.DataField, queryable.ElementType), gridBinaryImageColumn.DataField);
						list.Add(item2);
					}
				}
				if (flag)
				{
					string selector = string.Format("new ({0})", string.Join(",", list.ToArray()));
					queryable = queryable.Select(selector, new object[0]);
					queryable = GridDataTableFromEnumerable.ToGenericEnumerable(queryable, this.owner).AsQueryable();
				}
				List<string> list3 = new List<string>();
				for (int k = 0; k < this.owner.SortExpressions.Count; k++)
				{
					GridSortExpression gridSortExpression = this.owner.SortExpressions[k];
					string text2 = gridSortExpression.FieldName;
					if (text2.IndexOf(",") == -1)
					{
						text2 = GridDataTableFromEnumerable.TransformDataFieldName(gridSortExpression.FieldName, queryable.ElementType);
						list3.Add(string.Format("{0} {1}", text2, gridSortExpression.SortOrderAsString()).Trim());
					}
					else
					{
						string[] array2 = text2.Split(new char[]
						{
							','
						});
						foreach (string text3 in array2)
						{
							list3.Add(string.Format("{0} {1}", GridDataTableFromEnumerable.TransformDataFieldName(text3.Trim(), queryable.ElementType), gridSortExpression.SortOrderAsString()).Trim());
						}
					}
				}
				if (!string.IsNullOrEmpty(this.owner.FilterExpression) && !this.owner.isDataSourceViewFilter)
				{
					queryable = queryable.Where(this.owner.FilterExpression, new object[0]);
				}
				if (list3.Count > 0 && (!this.owner.AllowCustomSorting || this.owner.OverrideDataSourceControlSorting))
				{
					queryable = queryable.OrderBy(string.Join(",", list3.ToArray()), new object[0]);
				}
				this.owner.originalQueryable = queryable;
				this.owner.originalEnumerable = enumerable;
				if (this.owner.AllowPaging && (this.owner.GroupByExpressions.Count == 0 || !string.IsNullOrEmpty(this.owner.OwnerGrid.ClientDataSourceID)))
				{
					num = ((this.owner.CurrentDataSource is ICollection && string.IsNullOrEmpty(this.owner.FilterExpression)) ? ((ICollection)this.owner.CurrentDataSource).Count : queryable.Count());
					if (!this.owner.AllowCustomPaging)
					{
						this._table.ExtendedProperties["rowsCount"] = num;
					}
					if (!string.IsNullOrEmpty(this.owner.FilterExpression))
					{
						this._table.ExtendedProperties["rowsCount"] = num;
					}
					enumerable.GetEnumerator().Reset();
					int num2 = this.owner.PageSize * this.owner.CurrentPageIndex;
					if (this.owner.OwnerGrid.ClientSettings.Virtualization.EnableVirtualization)
					{
						num2 = this.owner.OwnerGrid.ClientSettings.Virtualization.FirstIndexInPage;
						if (this.owner.OwnerGrid.ClientSettings.Virtualization.StartIndex > 0)
						{
							num2 = this.owner.OwnerGrid.ClientSettings.Virtualization.StartIndex;
						}
					}
					if (num > num2)
					{
						queryable = queryable.Skip(num2);
					}
					else if (this.owner.CurrentPageIndex > 0 && !this.owner.AllowCustomPaging)
					{
						if (this.owner.CurrentPageIndex > 0 && num <= num2)
						{
							if (this.owner.PageSize == 0)
							{
								this.owner.CurrentPageIndex = 0;
								num2 = 0;
							}
							else
							{
								this.owner.CurrentPageIndex = num / this.owner.PageSize;
								num2 = this.owner.PageSize * this.owner.CurrentPageIndex;
								if (num != 0 && num2 == num)
								{
									this.owner.CurrentPageIndex--;
									num2 -= this.owner.PageSize;
								}
							}
						}
						if (num > num2)
						{
							queryable = queryable.Skip(num2);
						}
						else
						{
							this.owner.CurrentPageIndex = 0;
						}
					}
					queryable = queryable.Take(this.owner.PageSize);
					enumerable = queryable;
				}
				else
				{
					enumerable = queryable;
				}
				bool flag2 = false;
				if (queryable.ElementType.Name == "DbDataRecord")
				{
					flag2 = true;
					this.owner._shouldUseLinqGrouping = false;
				}
				if (this.owner.OwnerGrid.EnableLinqExpressions && this.owner.EnableLinqGrouping && this.owner.GroupByExpressions.Count > 0 && string.IsNullOrEmpty(this.owner.OwnerGrid.ClientDataSourceID) && !flag2)
				{
					this.owner.LinqGroupingHelper = new GridLinqGroupingHelper(this.owner);
					GridLinqGroupingHelper linqGroupingHelper = this.owner.LinqGroupingHelper;
					IEnumerable groupedItemsForCurrentPage = linqGroupingHelper.GetGroupedItemsForCurrentPage(queryable);
					this.FillDataTableFromEnumerable(groupedItemsForCurrentPage.AsQueryable());
					return;
				}
				this.FillDataTableFromEnumerable((IQueryable)enumerable);
			}
		}

		// Token: 0x0600B195 RID: 45461 RVA: 0x00267DBC File Offset: 0x00265FBC
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		internal List<string> GetColumnsToUse()
		{
			List<string> list = new List<string>();
			if (this.owner != null)
			{
				if (this.owner.CurrentDataSource is DataRow[] || (this.owner.CurrentDataSource != null && this.owner.CurrentDataSource.ToString() == "Microsoft.SharePoint.WebControls.SPDataSource") || this.owner.UseAllDataFields)
				{
					foreach (object obj in this._table.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj;
						if (!list.Contains(dataColumn.ColumnName))
						{
							list.Add(dataColumn.ColumnName);
						}
					}
					foreach (GridColumn gridColumn in this.owner.RenderColumns)
					{
						GridCalculatedColumn gridCalculatedColumn = gridColumn as GridCalculatedColumn;
						if (gridCalculatedColumn != null && !this.owner.OwnerGrid.IsDesignMode)
						{
							this.PopulateCalculatedColumn(list, gridCalculatedColumn);
						}
					}
					if (this.owner.GroupByExpressions.Count > 0)
					{
						this.isGrouping = true;
					}
					return list;
				}
				foreach (GridColumn gridColumn2 in this.owner.RenderColumns)
				{
					if (gridColumn2 is GridBoundColumn && ((GridBoundColumn)gridColumn2).Aggregate != GridAggregateFunction.None)
					{
						this.hasAggregates = true;
						string dataField = ((GridBoundColumn)gridColumn2).DataField;
						if (!list.Contains(dataField) && this._table.Columns.Contains(dataField))
						{
							list.Add(dataField);
						}
					}
					if (gridColumn2 is GridTemplateColumn && !string.IsNullOrEmpty(((GridTemplateColumn)gridColumn2).DataField) && ((GridTemplateColumn)gridColumn2).Aggregate != GridAggregateFunction.None)
					{
						this.hasAggregates = true;
						string dataField2 = ((GridTemplateColumn)gridColumn2).DataField;
						if (!list.Contains(dataField2) && this._table.Columns.Contains(dataField2))
						{
							list.Add(dataField2);
						}
					}
					GridCalculatedColumn gridCalculatedColumn2 = gridColumn2 as GridCalculatedColumn;
					if (gridCalculatedColumn2 != null && !this.owner.OwnerGrid.IsDesignMode)
					{
						this.PopulateCalculatedColumn(list, gridCalculatedColumn2);
					}
				}
				if (this.owner.ParentTableRelation != null)
				{
					foreach (GridRelationFields gridRelationFields in this.owner.ParentTableRelation)
					{
						if (!string.IsNullOrEmpty(gridRelationFields.DetailKeyField) && !list.Contains(gridRelationFields.DetailKeyField) && this._table.Columns.Contains(gridRelationFields.DetailKeyField))
						{
							list.Add(gridRelationFields.DetailKeyField);
						}
					}
				}
				if (!string.IsNullOrEmpty(this.owner.SelfHierarchySettings.KeyName) && !list.Contains(this.owner.SelfHierarchySettings.KeyName) && this._table.Columns.Contains(this.owner.SelfHierarchySettings.KeyName))
				{
					list.Add(this.owner.SelfHierarchySettings.KeyName);
				}
				if (!string.IsNullOrEmpty(this.owner.SelfHierarchySettings.ParentKeyName) && !list.Contains(this.owner.SelfHierarchySettings.ParentKeyName) && this._table.Columns.Contains(this.owner.SelfHierarchySettings.ParentKeyName))
				{
					list.Add(this.owner.SelfHierarchySettings.ParentKeyName);
				}
				if (!string.IsNullOrEmpty(this.owner.FilterExpression))
				{
					GridColumn[] renderColumns3 = this.owner.RenderColumns;
					int k = 0;
					while (k < renderColumns3.Length)
					{
						GridColumn gridColumn3 = renderColumns3[k];
						string text;
						if (gridColumn3 is GridBoundColumn)
						{
							text = ((GridBoundColumn)gridColumn3).DataField;
							goto IL_524;
						}
						if (gridColumn3 is GridTemplateColumn)
						{
							text = ((GridTemplateColumn)gridColumn3).DataField;
							goto IL_524;
						}
						if (gridColumn3 is GridCheckBoxColumn)
						{
							text = ((GridCheckBoxColumn)gridColumn3).DataField;
							goto IL_524;
						}
						if (gridColumn3 is GridDropDownColumn)
						{
							text = ((GridDropDownColumn)gridColumn3).DataField;
							goto IL_524;
						}
						if (gridColumn3 is GridHyperLinkColumn)
						{
							text = ((GridHyperLinkColumn)gridColumn3).DataTextField;
							goto IL_524;
						}
						if (gridColumn3 is GridImageColumn)
						{
							text = ((GridImageColumn)gridColumn3).DataAlternateTextField;
							goto IL_524;
						}
						if (gridColumn3 is GridBinaryImageColumn)
						{
							text = ((GridBinaryImageColumn)gridColumn3).DataAlternateTextField;
							goto IL_524;
						}
						if (gridColumn3 is GridAttachmentColumn)
						{
							GridAttachmentColumn gridAttachmentColumn = (GridAttachmentColumn)gridColumn3;
							text = (string.IsNullOrEmpty(gridAttachmentColumn.DataTextField) ? gridAttachmentColumn.FileNameTextField : gridAttachmentColumn.DataTextField);
							goto IL_524;
						}
						try
						{
							text = (string)GridPropertyEvaluator.GetPropertyValue(gridColumn3, "DataField");
							if (text == null)
							{
								goto IL_571;
							}
							if (!string.IsNullOrEmpty(text) && this.owner.FilterExpression.Contains(text) && !list.Contains(text) && this._table.Columns.Contains(text))
							{
								list.Add(text);
							}
							goto IL_571;
						}
						catch (Exception ex)
						{
							string message = ex.Message;
							goto IL_571;
						}
						goto IL_524;
						IL_571:
						k++;
						continue;
						IL_524:
						if (!string.IsNullOrEmpty(text) && this.owner.FilterExpression.ToUpperInvariant().Contains(text.ToUpperInvariant()) && !list.Contains(text) && this._table.Columns.Contains(text))
						{
							list.Add(text);
							goto IL_571;
						}
						goto IL_571;
					}
					this.isFiltering = true;
				}
				foreach (object obj2 in this.owner.SortExpressions)
				{
					GridSortExpression gridSortExpression = (GridSortExpression)obj2;
					if (!list.Contains(gridSortExpression.FieldName) && this._table.Columns.Contains(gridSortExpression.FieldName))
					{
						list.Add(gridSortExpression.FieldName);
					}
					this.isSorting = true;
				}
				foreach (GridGroupByExpression gridGroupByExpression in this.owner.GroupByExpressions)
				{
					foreach (object obj3 in gridGroupByExpression.SelectFields)
					{
						GridGroupByField gridGroupByField = (GridGroupByField)obj3;
						if (!list.Contains(gridGroupByField.FieldName) && this._table.Columns.Contains(gridGroupByField.FieldName))
						{
							list.Add(gridGroupByField.FieldName);
						}
					}
					foreach (object obj4 in gridGroupByExpression.GroupByFields)
					{
						GridGroupByField gridGroupByField2 = (GridGroupByField)obj4;
						if (!list.Contains(gridGroupByField2.FieldName) && this._table.Columns.Contains(gridGroupByField2.FieldName))
						{
							list.Add(gridGroupByField2.FieldName);
						}
					}
					this.isGrouping = true;
				}
				if (!this.owner.UseAllDataFields)
				{
					foreach (string text2 in this.owner.AdditionalDataFieldNames)
					{
						if (!list.Contains(text2) && this._table.Columns.Contains(text2))
						{
							list.Add(text2);
						}
					}
				}
				if (!(this.owner.CurrentDataSource is DataRow[]) && !this.owner.UseAllDataFields)
				{
					int num = this._table.Columns.Count - 1;
					while (this._table.Columns.Count != list.Count)
					{
						bool flag = false;
						foreach (string strA in list)
						{
							if (string.Compare(strA, this._table.Columns[num].ColumnName, true) == 0)
							{
								flag = true;
							}
						}
						if (!flag)
						{
							this._table.Columns.Remove(this._table.Columns[num]);
						}
						if (num == 0)
						{
							break;
						}
						num--;
					}
				}
			}
			return list;
		}

		// Token: 0x0600B196 RID: 45462 RVA: 0x002686FC File Offset: 0x002668FC
		private void PopulateCalculatedColumn(List<string> columnsToUse, GridCalculatedColumn column)
		{
			this.hasCalculatedColumns = true;
			if (!this._table.Columns.Contains(column.UniqueName))
			{
				GridDataColumn gridDataColumn = new GridDataColumn(column.UniqueName);
				if (!this.owner.OwnerGrid.EnableLinqExpressions || this.owner.originalEnumerable == null)
				{
					gridDataColumn.Expression = column.FormatExpression();
				}
				gridDataColumn.DataType = column.DataType;
				this._table.Columns.Add(gridDataColumn);
				gridDataColumn = new GridDataColumn(string.Format("{0}Result", column.UniqueName));
				gridDataColumn.DataType = column.DataType;
				this._table.Columns.Add(gridDataColumn);
			}
			foreach (string text in column.DataFields)
			{
				if (!columnsToUse.Contains(text) && this._table.Columns.Contains(text))
				{
					columnsToUse.Add(text);
				}
			}
			if (!columnsToUse.Contains(column.UniqueName) && this._table.Columns.Contains(column.UniqueName))
			{
				columnsToUse.Add(column.UniqueName);
			}
			if (!columnsToUse.Contains(string.Format("{0}Result", column.UniqueName)) && this._table.Columns.Contains(string.Format("{0}Result", column.UniqueName)))
			{
				columnsToUse.Add(string.Format("{0}Result", column.UniqueName));
			}
		}

		// Token: 0x0600B197 RID: 45463 RVA: 0x00268870 File Offset: 0x00266A70
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected override void FillData()
		{
			IEnumerable enumerable = base.enumerable;
			bool flag = this.owner.CurrentDataSource != null && this.owner.CurrentDataSource.GetType().GetInterface("IDataReader") != null;
			bool flag2 = false;
			AdvancedEnumerator advancedEnumerator = (AdvancedEnumerator)((AdvancedEnumerable)enumerable).GetEnumerator();
			advancedEnumerator.Reset();
			flag2 = advancedEnumerator.isForwardOnly;
			this.owner.IsBoundToForwardOnly = flag2;
			bool flag3 = false;
			if (this.owner.CurrentDataSource != null && this.owner.CurrentDataSource is IList && ((IList)this.owner.CurrentDataSource).Count > 1)
			{
				object obj = ((IList)this.owner.CurrentDataSource)[0];
				object obj2 = ((IList)this.owner.CurrentDataSource)[1];
				if (obj != null && obj2 != null)
				{
					flag3 = (obj.GetType() != obj.GetType());
				}
			}
			if (this.owner.OwnerGrid.EnableLinqExpressions && !flag && !flag2 && !flag3)
			{
				this.FillData35();
				return;
			}
			this.owner._shouldUseLinqGrouping = false;
			List<string> columnsToUse = this.GetColumnsToUse();
			this._table.Columns.Add(new GridDataColumn("OriginalDataItem", typeof(object)));
			if (this.owner.CurrentDataSource is DataRow[] || this.owner.UseAllDataFields)
			{
				foreach (object obj3 in this._table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj3;
					if (!columnsToUse.Contains(dataColumn.ColumnName))
					{
						columnsToUse.Add(dataColumn.ColumnName);
					}
				}
			}
			this._table.BeginLoadData();
			if (this.owner.CurrentDataSource is DataView)
			{
				DataTable table = ((DataView)this.owner.CurrentDataSource).Table;
				int count = table.Rows.Count;
				this._table.MinimumCapacity = count;
				if (!string.IsNullOrEmpty((this.owner.CurrentDataSource as DataView).Sort))
				{
					this.isSorting = false;
				}
			}
			if (this.owner.ParentTableRelation.Count > 0)
			{
				this.isHierarchy = true;
			}
			bool flag4 = true;
			if (this.isGrouping || this.isSorting || this.isFiltering || this.owner.AllowCustomPaging || !this.owner.AllowPaging || this.isHierarchy || this.hasAggregates || this.hasCalculatedColumns)
			{
				flag4 = false;
			}
			int pageSize = this.owner.PageSize;
			int num = 0;
			int num2 = this.owner.PageSize * ((this.owner.CurrentPageIndex != int.MaxValue) ? this.owner.CurrentPageIndex : (this.owner.PageCount - 1));
			int num3 = num2 + this.owner.PageSize - 1;
			GridPropertyEvaluator gridPropertyEvaluator = new GridPropertyEvaluator();
			OrderedDictionary orderedDictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
			foreach (object obj4 in this._table.Columns)
			{
				DataColumn dataColumn2 = (DataColumn)obj4;
				orderedDictionary[dataColumn2.ColumnName] = null;
			}
			object[] array = new object[orderedDictionary.Count];
			if (this.owner.CurrentDataSource is IList)
			{
				IList list = this.owner.CurrentDataSource as IList;
				if (!flag4)
				{
					num2 = 0;
					num3 = list.Count - 1;
				}
				int num4 = Math.Min(num3, list.Count - 1);
				if (list.Count != 0)
				{
					if (num2 > num4)
					{
						if (list.Count > this.owner.PageSize)
						{
							num2 = list.Count - this.owner.PageSize;
						}
						else
						{
							num2 = 0;
						}
					}
					if (this.owner.OwnerGrid.ClientSettings.Virtualization.EnableVirtualization && this.owner.OwnerGrid.ClientSettings.Virtualization.StartIndex > 0)
					{
						num2 = this.owner.OwnerGrid.ClientSettings.Virtualization.StartIndex;
						num3 = this.owner.OwnerGrid.ClientSettings.Virtualization.StartIndex + this.owner.OwnerGrid.ClientSettings.Virtualization.ItemsPerView;
					}
					for (int i = num2; i <= num4; i++)
					{
						object obj5 = list[i];
						orderedDictionary["OriginalDataItem"] = obj5;
						if (obj5 is DataRow)
						{
							orderedDictionary["OriginalDataItem"] = DBNull.Value;
						}
						if (columnsToUse.Count > 0)
						{
							foreach (string text in columnsToUse)
							{
								if (!(text == "OriginalDataItem") && !(text == "columnResult"))
								{
									GridDataColumn gridDataColumn = (GridDataColumn)this._table.Columns[text];
									if (!string.IsNullOrEmpty(gridDataColumn.Expression))
									{
										orderedDictionary[text] = null;
									}
									else if (gridDataColumn.IsPrimitive)
									{
										orderedDictionary[text] = obj5;
									}
									else
									{
										object value = null;
										if (obj5 is DataRow)
										{
											try
											{
												value = DataBinder.Eval(obj5, text);
												goto IL_5FE;
											}
											catch (Exception ex)
											{
												string message = ex.Message;
												value = ((DataRow)obj5)[text];
												goto IL_5FE;
											}
											goto IL_58E;
										}
										goto IL_58E;
										IL_5FE:
										value = this.ResolveDbNull(text, obj5, value);
										orderedDictionary[text] = value;
										continue;
										IL_58E:
										if (obj5.GetType().FullName == "Microsoft.SharePoint.WebControls.SPDataSourceViewResultItem" || obj5.GetType().FullName == "Microsoft.SharePoint.SPListItem")
										{
											value = this.owner.GetSPViewFieldValue<object>(obj5, text);
											goto IL_5FE;
										}
										if (obj5 is ICustomTypeDescriptor)
										{
											value = GridPropertyEvaluator.GetPropertyValue(obj5, text, DBNull.Value);
											goto IL_5FE;
										}
										value = gridPropertyEvaluator.GetCachedPropertyValue(obj5, text, DBNull.Value);
										goto IL_5FE;
									}
								}
							}
						}
						orderedDictionary.Values.CopyTo(array, 0);
						this._table.LoadDataRow(array, false);
						if (this.hasCalculatedColumns)
						{
							Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
							foreach (string text2 in columnsToUse)
							{
								GridDataColumn gridDataColumn2 = (GridDataColumn)this._table.Columns[text2];
								if (!string.IsNullOrEmpty(gridDataColumn2.Expression))
								{
									DataRow dataRow = this._table.Rows[this._table.Rows.Count - 1];
									dictionary[text2] = dataRow[text2];
									dataRow[string.Format("{0}Result", text2)] = dataRow[text2];
								}
								else
								{
									DataRow dataRow2 = this._table.Rows[this._table.Rows.Count - 1];
									dictionary[text2] = dataRow2[text2];
								}
							}
							if (dictionary.Count > 0)
							{
								if (obj5.GetType() == typeof(DataRow))
								{
									this.owner.relatedRows.Add(i, dictionary);
								}
								else
								{
									this.owner.relatedRows.Add(obj5, dictionary);
								}
							}
						}
					}
				}
				if (this.hasCalculatedColumns)
				{
					foreach (string name in columnsToUse)
					{
						GridDataColumn gridDataColumn3 = (GridDataColumn)this._table.Columns[name];
						if (!string.IsNullOrEmpty(gridDataColumn3.Expression))
						{
							gridDataColumn3.Expression = "";
						}
					}
				}
				if (flag4)
				{
					this._table.ExtendedProperties["rowsCount"] = list.Count;
				}
				return;
			}
			for (int j = 0; j <= 1; j++)
			{
				if (j == 1)
				{
					if (flag2)
					{
						break;
					}
					num2 -= this.owner.PageSize;
					num3 = num2 + this.owner.PageSize - 1;
					num = 0;
					advancedEnumerator.Reset();
				}
				foreach (object obj6 in enumerable)
				{
					if (flag4 && (num > num3 || num < num2))
					{
						num++;
					}
					else
					{
						j++;
						orderedDictionary["OriginalDataItem"] = obj6;
						if (this.owner.CurrentDataSource is DataRow[] && obj6 is DataRow)
						{
							orderedDictionary["OriginalDataItem"] = DBNull.Value;
						}
						int num5 = 0;
						foreach (string text3 in columnsToUse)
						{
							if (!(text3 == "OriginalDataItem"))
							{
								GridDataColumn gridDataColumn4 = (GridDataColumn)this._table.Columns[text3];
								if (!string.IsNullOrEmpty(gridDataColumn4.Expression))
								{
									orderedDictionary[text3] = null;
									num5++;
								}
								else
								{
									if (gridDataColumn4.IsPrimitive)
									{
										orderedDictionary[text3] = obj6;
									}
									else
									{
										object value2 = null;
										if (obj6 is DataRow)
										{
											try
											{
												value2 = DataBinder.Eval(obj6, text3);
												goto IL_A11;
											}
											catch (Exception ex2)
											{
												string message2 = ex2.Message;
												value2 = ((DataRow)obj6)[text3];
												goto IL_A11;
											}
											goto IL_9A1;
										}
										goto IL_9A1;
										IL_A11:
										value2 = this.ResolveDbNull(text3, obj6, value2);
										orderedDictionary[text3] = value2;
										goto IL_A2A;
										IL_9A1:
										if (obj6.GetType().FullName == "Microsoft.SharePoint.WebControls.SPDataSourceViewResultItem" || obj6.GetType().FullName == "Microsoft.SharePoint.SPListItem")
										{
											value2 = this.owner.GetSPViewFieldValue<object>(obj6, text3);
											goto IL_A11;
										}
										if (obj6 is ICustomTypeDescriptor)
										{
											value2 = GridPropertyEvaluator.GetPropertyValue(obj6, text3, DBNull.Value);
											goto IL_A11;
										}
										value2 = gridPropertyEvaluator.GetCachedPropertyValue(obj6, text3, DBNull.Value);
										goto IL_A11;
									}
									IL_A2A:
									num5++;
								}
							}
						}
						orderedDictionary.Values.CopyTo(array, 0);
						this._table.LoadDataRow(array, false);
						if (this.hasCalculatedColumns)
						{
							Dictionary<string, object> dictionary2 = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
							foreach (string text4 in columnsToUse)
							{
								GridDataColumn gridDataColumn5 = (GridDataColumn)this._table.Columns[text4];
								if (!string.IsNullOrEmpty(gridDataColumn5.Expression))
								{
									DataRow dataRow3 = this._table.Rows[this._table.Rows.Count - 1];
									dictionary2[text4] = dataRow3[text4];
									dataRow3[string.Format("{0}Result", text4)] = dataRow3[text4];
								}
								else
								{
									DataRow dataRow4 = this._table.Rows[this._table.Rows.Count - 1];
									dictionary2[text4] = dataRow4[text4];
								}
							}
							if (dictionary2.Count > 0 && !this.owner.relatedRows.ContainsKey(obj6))
							{
								this.owner.relatedRows.Add(obj6, dictionary2);
							}
						}
						num++;
					}
				}
			}
			if (this.hasCalculatedColumns)
			{
				foreach (string name2 in columnsToUse)
				{
					GridDataColumn gridDataColumn6 = (GridDataColumn)this._table.Columns[name2];
					if (!string.IsNullOrEmpty(gridDataColumn6.Expression))
					{
						gridDataColumn6.Expression = "";
					}
				}
			}
			if (flag4)
			{
				this._table.ExtendedProperties["rowsCount"] = num;
			}
			this._table.EndLoadData();
		}

		// Token: 0x0600B198 RID: 45464 RVA: 0x002695EC File Offset: 0x002677EC
		private object ResolveDbNull(string columnName, object dataItem, object value)
		{
			if (value == null)
			{
				if (!this.owner.RetrieveNullAsDBNull)
				{
					throw new GridBindingException(string.Format("Unable to find property {0} within a DataItem of type {1}", columnName, dataItem.GetType().FullName));
				}
				value = DBNull.Value;
			}
			string text = value as string;
			if (text != null)
			{
				value = text.TrimEnd(new char[0]);
			}
			return value;
		}

		// Token: 0x0600B199 RID: 45465 RVA: 0x00269647 File Offset: 0x00267847
		protected override DataTable GetDataTable()
		{
			return this._table;
		}

		// Token: 0x0600B19A RID: 45466 RVA: 0x0026964F File Offset: 0x0026784F
		protected override ArrayList GetColumns()
		{
			return this._list;
		}

		// Token: 0x0600B19B RID: 45467 RVA: 0x00269657 File Offset: 0x00267857
		protected override List<DataColumn> ParseSPListItemProperties<T>(T firstObject)
		{
			return this.owner.OwnerGrid.ParseSPViewFieldsIntoDataColumns<T>(firstObject);
		}

		// Token: 0x04002E8C RID: 11916
		private ArrayList _list = new ArrayList();

		// Token: 0x04002E8D RID: 11917
		private DataTable _table = new DataTable();

		// Token: 0x04002E8E RID: 11918
		protected bool autoGenerateGridColumns = true;

		// Token: 0x04002E8F RID: 11919
		private ArrayList properties = new ArrayList();

		// Token: 0x04002E90 RID: 11920
		private ArrayList additionalFields = new ArrayList();

		// Token: 0x04002E91 RID: 11921
		private bool retrieveAllFields;

		// Token: 0x04002E92 RID: 11922
		private bool enableSplitHeaderText;

		// Token: 0x04002E93 RID: 11923
		private GridColumnCollection existingColumns;

		// Token: 0x04002E94 RID: 11924
		private GridTableView owner;

		// Token: 0x04002E95 RID: 11925
		private static readonly Regex NameExpression = new Regex("([A-Z]+(?=$|[A-Z][a-z])|[A-Z]?[a-z]+)", RegexOptions.Compiled);

		// Token: 0x04002E96 RID: 11926
		internal bool isFiltering;

		// Token: 0x04002E97 RID: 11927
		internal bool isGrouping;

		// Token: 0x04002E98 RID: 11928
		internal bool isSorting;

		// Token: 0x04002E99 RID: 11929
		internal bool isHierarchy;

		// Token: 0x04002E9A RID: 11930
		internal bool hasAggregates;

		// Token: 0x04002E9B RID: 11931
		internal bool hasCalculatedColumns;

		// Token: 0x020010EE RID: 4334
		internal class GridGenericEnumerable<T> : IEnumerable<!0>, IEnumerable
		{
			// Token: 0x0600B19D RID: 45469 RVA: 0x0026967C File Offset: 0x0026787C
			internal GridGenericEnumerable(IEnumerable source)
			{
				this.source = source;
			}

			// Token: 0x0600B19E RID: 45470 RVA: 0x0026968B File Offset: 0x0026788B
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.source.GetEnumerator();
			}

			// Token: 0x0600B19F RID: 45471 RVA: 0x002697E8 File Offset: 0x002679E8
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				foreach (object obj in this.source)
				{
					T item = (T)((object)obj);
					yield return item;
				}
				yield break;
			}

			// Token: 0x04002E9C RID: 11932
			private IEnumerable source;
		}

		// Token: 0x020010EF RID: 4335
		internal class GridEntityGenericEnumerable<T> : IEnumerable<!0>, IEnumerable
		{
			// Token: 0x0600B1A0 RID: 45472 RVA: 0x00269804 File Offset: 0x00267A04
			internal GridEntityGenericEnumerable(IEnumerable source)
			{
				this.source = source;
			}

			// Token: 0x0600B1A1 RID: 45473 RVA: 0x00269813 File Offset: 0x00267A13
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.source.GetEnumerator();
			}

			// Token: 0x0600B1A2 RID: 45474 RVA: 0x00269978 File Offset: 0x00267B78
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				foreach (object item in this.source)
				{
					yield return (T)((object)(item as ICustomTypeDescriptor).GetPropertyOwner(null));
				}
				yield break;
			}

			// Token: 0x04002E9D RID: 11933
			private IEnumerable source;
		}
	}
}
