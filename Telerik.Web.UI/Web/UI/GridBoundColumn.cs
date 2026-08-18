using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020010A4 RID: 4260
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public class GridBoundColumn : GridEditableColumn
	{
		// Token: 0x0600AD18 RID: 44312 RVA: 0x00253231 File Offset: 0x00251431
		[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
		public GridBoundColumn()
		{
		}

		// Token: 0x0600AD19 RID: 44313 RVA: 0x00253239 File Offset: 0x00251439
		protected virtual string FormatDataValue(object dataValue, GridItem item)
		{
			return this.FormatDataValue(dataValue, item, false);
		}

		// Token: 0x0600AD1A RID: 44314 RVA: 0x00253244 File Offset: 0x00251444
		protected virtual string FormatDataValue(object dataValue, GridItem item, bool formatEvenIfReadOnly)
		{
			string empty = string.Empty;
			if (dataValue == null || dataValue == DBNull.Value)
			{
				return empty;
			}
			bool flag = this.formatting.Length == 0 || (base.Owner != null && base.Owner.OwnerGrid.IsExporting && base.Owner.OwnerGrid.ExportSettings.SuppressColumnDataFormatStrings);
			bool flag2 = base.Owner.TimeZoneID != string.Empty && base.DataType == typeof(DateTime);
			if (flag2)
			{
				dataValue = base.Owner.TimeZoneProvider.UtcToLocal((DateTime)dataValue);
			}
			if (dataValue != null && !string.IsNullOrEmpty(dataValue.ToString()) && this.HtmlEncode && (base.IsReadOnly(item) || !item.IsInEditMode))
			{
				if (flag)
				{
					return HttpUtility.HtmlEncode(dataValue.ToString());
				}
				return HttpUtility.HtmlEncode(string.Format(this.formatting, dataValue));
			}
			else
			{
				if (flag)
				{
					return dataValue.ToString();
				}
				if (!item.IsInEditMode || base.IsReadOnly(item))
				{
					return string.Format(this.formatting, dataValue);
				}
				return dataValue.ToString();
			}
		}

		// Token: 0x0600AD1B RID: 44315 RVA: 0x0025336F File Offset: 0x0025156F
		public override void Initialize()
		{
			base.Initialize();
			this.boundFieldDesc = null;
			this.boundField = this.DataField;
			this.formatting = this.DataFormatString;
		}

		// Token: 0x0600AD1C RID: 44316 RVA: 0x00253398 File Offset: 0x00251598
		public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			GridGroupFooterItem gridGroupFooterItem = inItem as GridGroupFooterItem;
			if (gridGroupFooterItem == null || gridGroupFooterItem.OwnerTableView.GroupFooterTemplate == null)
			{
				base.InitializeCell(cell, columnIndex, inItem);
				if ((inItem is GridFooterItem || gridGroupFooterItem != null) && this.Aggregate != GridAggregateFunction.None)
				{
					cell.DataBinding += this.cell_DataBinding;
				}
				if (inItem.IsDataBound)
				{
					if (inItem.IsInEditMode)
					{
						if (this.DataField.Length > 0)
						{
							this.CurrentColumnEditor.InitializeInControl(cell);
							if (this.CurrentColumnEditor is GridTextBoxColumnEditor)
							{
								if (this.MaxLength > 0)
								{
									((GridTextBoxColumnEditor)this.CurrentColumnEditor).TextBoxControl.MaxLength = this.MaxLength;
								}
								((GridTextBoxColumnEditor)this.CurrentColumnEditor).TextBoxControl.Visible = !base.IsReadOnly(inItem);
								if (this.ColumnValidationSettings.EnableRequiredFieldValidation)
								{
									((GridTextBoxColumnEditor)this.CurrentColumnEditor).GetRequiredFieldValidator().Visible = !base.IsReadOnly(inItem);
								}
								if (this.ColumnValidationSettings.EnableModelErrorMessageValidation)
								{
									((GridTextBoxColumnEditor)this.CurrentColumnEditor).GetModelErrorMessageValidator().Visible = !this.ReadOnly;
								}
							}
							if (this.CurrentColumnEditor is GridNumericColumnEditor)
							{
								if (this.MaxLength > 0)
								{
									((GridNumericColumnEditor)this.CurrentColumnEditor).NumericTextBox.MaxLength = this.MaxLength;
								}
								((GridNumericColumnEditor)this.CurrentColumnEditor).NumericTextBox.Visible = !base.IsReadOnly(inItem);
								if (this.ColumnValidationSettings.EnableRequiredFieldValidation)
								{
									((GridNumericColumnEditor)this.CurrentColumnEditor).GetRequiredFieldValidator().Visible = !base.IsReadOnly(inItem);
								}
								if (this.ColumnValidationSettings.EnableModelErrorMessageValidation)
								{
									((GridNumericColumnEditor)this.CurrentColumnEditor).GetModelErrorMessageValidator().Visible = !this.ReadOnly;
								}
							}
							if (this.CurrentColumnEditor is GridHTMLEditorColumnEditor && this.MaxLength > 0)
							{
								((GridHTMLEditorColumnEditor)this.CurrentColumnEditor).Editor.MaxTextLength = this.MaxLength;
							}
						}
						if (base.IsReadOnly(inItem) && (Literal)inItem.FindControl(string.Format("ROLC_{0}", this.UniqueName)) == null)
						{
							Literal literal = new Literal();
							literal.ID = string.Format("ROLC_{0}", this.UniqueName);
							cell.Controls.Add(literal);
						}
					}
					cell.DataBinding += this.OnDataBindColumn;
				}
				return;
			}
			if (base.Owner._resolvedDataSource is GridEnumerableFromViewState)
			{
				return;
			}
			if (gridGroupFooterItem != null && this.Aggregate != GridAggregateFunction.None)
			{
				if (base.Owner.OwnerGrid.IsDesignMode)
				{
					return;
				}
				if (this.Aggregate == GridAggregateFunction.Custom)
				{
					GridCustomAggregateEventArgs gridCustomAggregateEventArgs = new GridCustomAggregateEventArgs((GridItem)cell.Parent, this, "");
					base.Owner.OwnerGrid.CallOnCustomAggregate(gridCustomAggregateEventArgs);
					this.PopulateAggragateInGroupFooter(cell, gridCustomAggregateEventArgs.Result);
				}
				if (base.Owner.OwnerGrid.EnableLinqExpressions)
				{
					this.ApplyAggregates35(cell, string.Empty);
					return;
				}
				this.ApplyAggregates(cell, string.Empty);
			}
		}

		// Token: 0x0600AD1D RID: 44317 RVA: 0x002536AC File Offset: 0x002518AC
		private void cell_DataBinding(object sender, EventArgs e)
		{
			if (base.Owner.OwnerGrid.IsDesignMode)
			{
				return;
			}
			if (base.Owner.ShowFooter || base.Owner.OwnerGrid.ShowFooter || base.Owner.ShowGroupFooter)
			{
				TableCell tableCell = (TableCell)sender;
				string @string = base.Owner.OwnerGrid.Localization.GetString("AggregateFunction" + this.Aggregate.ToString());
				string footerText = string.IsNullOrEmpty(this.FooterText) ? string.Format("{0} : ", @string) : this.FooterText;
				if (!string.IsNullOrEmpty(this.FooterAggregateFormatString) || !string.IsNullOrEmpty(this.DataFormatString))
				{
					footerText = "";
				}
				if (this.Aggregate == GridAggregateFunction.Custom)
				{
					GridCustomAggregateEventArgs gridCustomAggregateEventArgs = new GridCustomAggregateEventArgs((GridItem)tableCell.Parent, this, "");
					base.Owner.OwnerGrid.CallOnCustomAggregate(gridCustomAggregateEventArgs);
					tableCell.Text = this.FormatCellText(footerText, gridCustomAggregateEventArgs.Result);
					return;
				}
				if (base.Owner.OwnerGrid.EnableLinqExpressions)
				{
					this.ApplyAggregates35(tableCell, footerText);
					return;
				}
				this.ApplyAggregates(tableCell, footerText);
			}
		}

		// Token: 0x0600AD1E RID: 44318 RVA: 0x002537DC File Offset: 0x002519DC
		internal string FormatCellText(string footerText, object aggregateResult)
		{
			if (!string.IsNullOrEmpty(this.FooterAggregateFormatString))
			{
				try
				{
					return string.Format(this.FooterAggregateFormatString, aggregateResult);
				}
				catch
				{
					throw new FormatException(string.Format("Invalid FooterAggregateFormatString for column with UniqueName \"{0}\"", this.UniqueName));
				}
			}
			if (!string.IsNullOrEmpty(this.DataFormatString))
			{
				try
				{
					return string.Format(this.DataFormatString, aggregateResult);
				}
				catch
				{
					throw new FormatException(string.Format("Invalid FooterAggregateFormatString for column with UniqueName \"{0}\"", this.UniqueName));
				}
			}
			return string.Format("{0}{1}", footerText, aggregateResult);
		}

		// Token: 0x0600AD1F RID: 44319 RVA: 0x0025387C File Offset: 0x00251A7C
		private void ApplyAggregates(TableCell cell, string footerText)
		{
			object obj = null;
			if (base.Owner._resolvedDataSource == null)
			{
				return;
			}
			string key = string.Format("GroupedResult{0}", ((GridItem)cell.Parent).GroupLevel);
			DataTable dataTable = (DataTable)((GridEnumerableFromDataView)base.Owner._resolvedDataSource).GroupingDataSet.ExtendedProperties[key];
			if (dataTable == null)
			{
				dataTable = ((GridEnumerableFromDataView)base.Owner._resolvedDataSource)._dataView.Table;
			}
			string filterExpression = base.Owner.FilterExpression;
			string arg = (this.DataField.IndexOf("[") == -1) ? string.Format("[{0}]", this.DataField) : this.DataField;
			DataTable dataTable2 = new DataTable();
			if (dataTable.Columns.Count > 0 && dataTable.Columns.Contains("OriginalDataItem") && !dataTable.Columns.Contains(this.DataField) && dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.Rows[0];
				if (dataRow.ItemArray.Count<object>() <= 0 || !(dataRow["OriginalDataItem"] is DbDataRecord))
				{
					goto IL_1C1;
				}
				dataTable2.Columns.Add(this.DataField, base.DataType);
				using (IEnumerator enumerator = dataTable.Rows.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						DataRow dataRow2 = (DataRow)obj2;
						dataTable2.Rows.Add(new object[]
						{
							DataBinder.GetPropertyValue(dataRow["OriginalDataItem"], this.DataField)
						});
					}
					goto IL_1C1;
				}
			}
			dataTable2 = dataTable;
			IL_1C1:
			if (this.Aggregate == GridAggregateFunction.Count)
			{
				obj = dataTable2.DefaultView.Count;
				cell.Text = this.FormatCellText(footerText, obj);
			}
			if (this.Aggregate == GridAggregateFunction.First && dataTable2.DefaultView.Count > 0)
			{
				obj = dataTable2.DefaultView[0][this.DataField];
				cell.Text = this.FormatCellText(footerText, obj);
			}
			if (this.Aggregate == GridAggregateFunction.Last && dataTable2.DefaultView.Count > 0)
			{
				obj = dataTable2.DefaultView[dataTable2.DefaultView.Count - 1][this.DataField];
				cell.Text = this.FormatCellText(footerText, obj);
			}
			if (this.Aggregate == GridAggregateFunction.Max && dataTable2.DefaultView.Count > 0)
			{
				obj = dataTable2.Compute(string.Format("Max({0})", arg), filterExpression);
				cell.Text = this.FormatCellText(footerText, obj);
			}
			if (this.Aggregate == GridAggregateFunction.Min && dataTable2.DefaultView.Count > 0)
			{
				obj = dataTable2.Compute(string.Format("Min({0})", arg), filterExpression);
				cell.Text = this.FormatCellText(footerText, obj);
			}
			if (this.Aggregate == GridAggregateFunction.Sum && dataTable2.DefaultView.Count > 0)
			{
				obj = dataTable2.Compute(string.Format("Sum({0})", arg), filterExpression);
				cell.Text = this.FormatCellText(footerText, obj);
			}
			if (this.Aggregate == GridAggregateFunction.Avg && dataTable2.DefaultView.Count > 0)
			{
				obj = dataTable2.Compute(string.Format("Avg({0})", arg), filterExpression);
				cell.Text = this.FormatCellText(footerText, obj);
			}
			if (this.Aggregate == GridAggregateFunction.CountDistinct && dataTable2.DefaultView.Count > 0)
			{
				obj = GridBoundColumn.GetDistinctCount(dataTable2, this.DataField);
				cell.Text = this.FormatCellText(footerText, obj);
			}
			this.PopulateAggragateInGroupFooter(cell, obj);
		}

		// Token: 0x0600AD20 RID: 44320 RVA: 0x00253C3C File Offset: 0x00251E3C
		internal static int GetDistinctCount(DataTable table, string dataField)
		{
			List<object> list = new List<object>();
			foreach (object obj in table.DefaultView)
			{
				DataRowView dataRowView = (DataRowView)obj;
				if (!list.Contains(dataRowView[dataField]))
				{
					list.Add(dataRowView[dataField]);
				}
			}
			return list.Count;
		}

		// Token: 0x0600AD21 RID: 44321 RVA: 0x00253CB8 File Offset: 0x00251EB8
		private bool PopulateAggragateInGroupFooter(TableCell cell, object result)
		{
			GridGroupFooterItem gridGroupFooterItem = cell.Parent as GridGroupFooterItem;
			if (gridGroupFooterItem != null && gridGroupFooterItem.OwnerTableView.GroupFooterTemplate != null)
			{
				if (!gridGroupFooterItem.AggregatesValues.Contains(this.DataField))
				{
					gridGroupFooterItem.AggregatesValues.Add(this.DataField, result);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600AD22 RID: 44322 RVA: 0x00253D0C File Offset: 0x00251F0C
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		private void ApplyAggregates35(TableCell cell, string footerText)
		{
			object obj = null;
			string key = string.Format("GroupedResult{0}", ((GridItem)cell.Parent).GroupLevel);
			GridGroupFooterItem gridGroupFooterItem = cell.Parent as GridGroupFooterItem;
			if (gridGroupFooterItem != null && base.Owner.OwnerGrid.GroupingSettings.IgnorePagingForGroupAggregates)
			{
				GridGroupHeaderItem groupHeaderItem = gridGroupFooterItem.GroupHeaderItem;
				base.Owner.LinqGroupingHelper.EnsureOriginalGroup(groupHeaderItem);
				IEnumerable items = groupHeaderItem.OriginalGroup.Items;
				obj = base.Owner.LinqGroupingHelper.GetAggregate(items, items.AsQueryable(), this.DataField, base.DataType, this.Aggregate);
			}
			else
			{
				DataTable dataTable = (DataTable)((GridEnumerableFromDataView)base.Owner._resolvedDataSource).GroupingDataSet.ExtendedProperties[key];
				if (dataTable != null)
				{
					IEnumerable<DataRow> enumerable = dataTable.AsEnumerable();
					IQueryable<DataRow> queryable = enumerable.AsQueryable<DataRow>();
					obj = GridBoundColumn.GetAggregate(enumerable, queryable, this.DataField, base.DataType, this.Aggregate);
				}
				else if (base.Owner.PagingManager.DataSourceCount > 0)
				{
					if (base.Owner.originalEnumerable == null)
					{
						this.ApplyAggregates(cell, footerText);
						return;
					}
					obj = GridBoundColumn.GetAggregate(base.Owner.originalEnumerable, base.Owner.originalQueryable, this.DataField, base.DataType, this.Aggregate);
				}
			}
			if (this.PopulateAggragateInGroupFooter(cell, obj))
			{
				return;
			}
			cell.Text = this.FormatCellText(footerText, obj);
		}

		// Token: 0x0600AD23 RID: 44323 RVA: 0x00253E84 File Offset: 0x00252084
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		internal static string PrepareFieldName(IEnumerable enumerable, IQueryable queryable, string fieldName, Type dataType)
		{
			if (enumerable == null)
			{
				return "";
			}
			if (!(enumerable is EnumerableRowCollection<DataRow>))
			{
				IEnumerator enumerator = enumerable.GetEnumerator();
				if (enumerator != null)
				{
					enumerator.Reset();
				}
			}
			fieldName = GridDataTableFromEnumerable.TransformDataFieldName(fieldName, queryable.ElementType);
			string arg = dataType.ToString().Split(new char[]
			{
				'.'
			})[1];
			if (!(dataType != typeof(string)) || !(dataType != typeof(object)))
			{
				return string.Format("{0}({1})", "object", fieldName);
			}
			arg = string.Format("{0}?", arg);
			if (queryable.ElementType == typeof(DataRowView) || queryable.ElementType == typeof(DataRow) || queryable.ElementType.GetInterface("IDataRecord") != null)
			{
				return string.Format("iif({1} == Convert.DBNull, null, {0}({1}))", arg, fieldName);
			}
			return string.Format("{0}({1})", arg, fieldName);
		}

		// Token: 0x0600AD24 RID: 44324 RVA: 0x00253F80 File Offset: 0x00252180
		internal static object GetAggregate(IEnumerable enumerable, IQueryable queryable, string fieldName, Type dataType, GridAggregateFunction func)
		{
			if (enumerable == null)
			{
				return null;
			}
			fieldName = GridBoundColumn.PrepareFieldName(enumerable, queryable, fieldName, dataType);
			MethodInfo method = typeof(GridBoundColumn).GetMethod("GetAggregateByType", BindingFlags.Static | BindingFlags.NonPublic);
			if (dataType != typeof(string) && dataType != typeof(object))
			{
				dataType = typeof(Nullable<>).MakeGenericType(new Type[]
				{
					dataType
				});
			}
			else
			{
				dataType = typeof(object);
			}
			MethodInfo methodInfo = method.MakeGenericMethod(new Type[]
			{
				dataType
			});
			return methodInfo.Invoke(null, new object[]
			{
				queryable,
				fieldName,
				func
			});
		}

		// Token: 0x0600AD25 RID: 44325 RVA: 0x0025403C File Offset: 0x0025223C
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		internal static object GetAggregateByType<T>(IQueryable queryable, string fieldName, GridAggregateFunction func)
		{
			if (func == GridAggregateFunction.First)
			{
				return ((IQueryable<T>)queryable.Take(1).Select(fieldName, new object[0])).First<T>();
			}
			IQueryable<T> source = (IQueryable<T>)queryable.Select(fieldName, new object[0]);
			if (func == GridAggregateFunction.Last)
			{
				return source.Last<T>();
			}
			if (func == GridAggregateFunction.Avg)
			{
				if (typeof(T) == typeof(short))
				{
					return source.Cast<short>().Average((short n) => (int)((short)n));
				}
				if (typeof(T) == typeof(short?))
				{
					return source.Cast<short?>().Average((short? n) => (int?)((short?)n));
				}
				if (typeof(T) == typeof(int))
				{
					return source.Cast<int>().Average();
				}
				if (typeof(T) == typeof(int?))
				{
					return source.Cast<int?>().Average();
				}
				if (typeof(T) == typeof(long))
				{
					return source.Cast<long>().Average((long n) => (long)n);
				}
				if (typeof(T) == typeof(long?))
				{
					return source.Cast<long?>().Average((long? n) => (long?)n);
				}
				if (typeof(T) == typeof(long))
				{
					return source.Cast<long>().Average((long n) => (long)n);
				}
				if (typeof(T) == typeof(long?))
				{
					return source.Cast<long?>().Average((long? n) => (long?)n);
				}
				if (typeof(T) == typeof(decimal))
				{
					return source.Cast<decimal>().Average();
				}
				if (typeof(T) == typeof(decimal?))
				{
					return source.Cast<decimal?>().Average();
				}
				if (typeof(T) == typeof(float))
				{
					return source.Cast<float>().Average((float n) => (float)n);
				}
				if (typeof(T) == typeof(float?))
				{
					return source.Cast<float?>().Average((float? n) => (float?)n);
				}
				if (typeof(T) == typeof(double))
				{
					return source.Cast<double>().Average();
				}
				if (typeof(T) == typeof(double?))
				{
					return source.Cast<double?>().Average();
				}
				if (typeof(T) == typeof(uint))
				{
					return source.Cast<uint>().Average((uint n) => (long)((uint)n));
				}
				if (typeof(T) == typeof(uint?))
				{
					return source.Cast<uint?>().Average((uint? n) => (long?)((uint?)n));
				}
				if (typeof(T) == typeof(short))
				{
					return source.Cast<short>().Average((short n) => (int)((short)n));
				}
				if (typeof(T) == typeof(short?))
				{
					return source.Cast<short?>().Average((short? n) => (int?)((short?)n));
				}
				if (typeof(T) == typeof(ushort))
				{
					return source.Cast<ushort>().Average((ushort n) => (int)((ushort)n));
				}
				if (typeof(T) == typeof(ushort?))
				{
					return source.Cast<ushort?>().Average((ushort? n) => (int?)((ushort?)n));
				}
				throw new NotSupportedException(string.Format("Average is not supported for type \"{0}\"", typeof(T)));
			}
			else if (func == GridAggregateFunction.Sum)
			{
				if (typeof(T) == typeof(short))
				{
					return source.Cast<short>().Sum((short n) => (int)((short)n));
				}
				if (typeof(T) == typeof(short?))
				{
					return source.Cast<short?>().Sum((short? n) => (int?)((short?)n));
				}
				if (typeof(T) == typeof(int))
				{
					return source.Cast<int>().Sum();
				}
				if (typeof(T) == typeof(int?))
				{
					return source.Cast<int?>().Sum();
				}
				if (typeof(T) == typeof(long))
				{
					return source.Cast<long>().Sum((long n) => (long)n);
				}
				if (typeof(T) == typeof(long?))
				{
					return source.Cast<long?>().Sum((long? n) => (long?)n);
				}
				if (typeof(T) == typeof(long))
				{
					return source.Cast<long>().Sum((long n) => (long)n);
				}
				if (typeof(T) == typeof(long?))
				{
					return source.Cast<long?>().Sum((long? n) => (long?)n);
				}
				if (typeof(T) == typeof(decimal))
				{
					return source.Cast<decimal>().Sum();
				}
				if (typeof(T) == typeof(decimal?))
				{
					return source.Cast<decimal?>().Sum();
				}
				if (typeof(T) == typeof(float))
				{
					return source.Cast<float>().Sum((float n) => (float)n);
				}
				if (typeof(T) == typeof(float?))
				{
					return source.Cast<float?>().Sum((float? n) => (float?)n);
				}
				if (typeof(T) == typeof(double))
				{
					return source.Cast<double>().Sum();
				}
				if (typeof(T) == typeof(double?))
				{
					return source.Cast<double?>().Sum();
				}
				if (typeof(T) == typeof(uint))
				{
					return source.Cast<uint>().Sum((uint n) => (long)((uint)n));
				}
				if (typeof(T) == typeof(uint?))
				{
					return source.Cast<uint?>().Sum((uint? n) => (long?)((uint?)n));
				}
				if (typeof(T) == typeof(short))
				{
					return source.Cast<short>().Sum((short n) => (int)((short)n));
				}
				if (typeof(T) == typeof(short?))
				{
					return source.Cast<short?>().Sum((short? n) => (int?)((short?)n));
				}
				if (typeof(T) == typeof(ushort))
				{
					return source.Cast<ushort>().Sum((ushort n) => (int)((ushort)n));
				}
				if (typeof(T) == typeof(ushort?))
				{
					return source.Cast<ushort?>().Sum((ushort? n) => (int?)((ushort?)n));
				}
				throw new NotSupportedException(string.Format("Sum is not supported for type \"{0}\"", typeof(T)));
			}
			else
			{
				if (func == GridAggregateFunction.CountDistinct)
				{
					return source.Distinct<T>().Count<T>();
				}
				if (func == GridAggregateFunction.Count)
				{
					return source.Count<T>();
				}
				if (func == GridAggregateFunction.Max)
				{
					return source.Max<T>();
				}
				if (func == GridAggregateFunction.Min)
				{
					return source.Min<T>();
				}
				return null;
			}
		}

		// Token: 0x0600AD26 RID: 44326 RVA: 0x00254F74 File Offset: 0x00253174
		private void OnDataBindColumn(object sender, EventArgs e)
		{
			bool visible = this.Visible;
			bool flag = base.Owner.OwnerGrid.ShouldBindInvisibleColumns;
			if (base.Owner.OwnerGrid.IsExporting)
			{
				flag = true;
			}
			Control control = (Control)sender;
			GridItem bindingParentItem = GridColumn.GetBindingParentItem(control);
			if (bindingParentItem.IsInEditMode && this.DataField.Length > 0)
			{
				this.CurrentColumnEditor.InitializeFromControl(control);
			}
			object obj = bindingParentItem.DataItem;
			if (bindingParentItem.DataItem != null && (bindingParentItem.DataItem.GetType().FullName == "Microsoft.SharePoint.WebControls.SPDataSourceViewResultItem" || bindingParentItem.DataItem.GetType().FullName == "Microsoft.SharePoint.SPListItem"))
			{
				obj = base.Owner.GetSPViewFieldValue<object>(obj, this.boundField);
			}
			else if ((this.boundFieldDesc == null || this.boundFieldDesc.ComponentType != obj.GetType()) && !this.boundField.Equals(GridBoundColumn.thisExpr))
			{
				this.boundFieldDesc = TypeDescriptor.GetProperties(obj).Find(this.boundField, true);
				if (this.boundFieldDesc == null && !base.DesignMode && !string.IsNullOrEmpty(this.boundField) && (visible || flag || bindingParentItem.IsInEditMode))
				{
					if (this.boundField.IndexOf(".") > -1)
					{
						try
						{
							obj = DataBinder.Eval(obj, this.boundField);
							goto IL_1FE;
						}
						catch
						{
							if (!GridBaseDataList.IsBindableType(obj.GetType()))
							{
								obj = null;
							}
							goto IL_1FE;
						}
					}
					DataRow dataRow = obj as DataRow;
					if (dataRow != null)
					{
						if (dataRow.Table.Columns.Contains(this.boundField))
						{
							obj = dataRow[this.boundField];
						}
						else
						{
							obj = null;
						}
					}
					else
					{
						try
						{
							obj = DataBinder.GetPropertyValue(obj, this.boundField);
						}
						catch
						{
							try
							{
								obj = DataBinder.Eval(obj, this.boundField);
							}
							catch
							{
								if (!GridBaseDataList.IsBindableType(obj.GetType()))
								{
									obj = null;
								}
							}
						}
					}
				}
			}
			IL_1FE:
			object obj2 = obj;
			string text;
			if (this.boundFieldDesc == null && base.DesignMode)
			{
				text = "GridBoundColumn";
			}
			else
			{
				if (this.boundFieldDesc != null)
				{
					obj2 = this.boundFieldDesc.GetValue(obj);
					if ((base.Owner.IsUsingModelBinding || !string.IsNullOrWhiteSpace(base.Owner.ItemType)) && obj2 != null && bindingParentItem.IsInEditMode && base.Owner.IsItemInserted && this.DataField.Length > 0 && this.TextEditor.IsInitialized && string.IsNullOrEmpty(base.DefaultInsertValue))
					{
						try
						{
							if (this.boundFieldDesc.PropertyType.FullName == "System.String")
							{
								if (string.IsNullOrWhiteSpace(obj2.ToString()))
								{
									obj2 = null;
								}
							}
							else if (Activator.CreateInstance(Type.GetType(this.boundFieldDesc.PropertyType.FullName)) != null && Activator.CreateInstance(Type.GetType(this.boundFieldDesc.PropertyType.FullName)).ToString() == obj2.ToString())
							{
								obj2 = null;
							}
						}
						catch (Exception)
						{
						}
					}
				}
				text = this.FormatDataValue(obj2, bindingParentItem);
			}
			if (bindingParentItem.IsInEditMode && this.DataField.Length > 0 && this.TextEditor.IsInitialized)
			{
				this.TextEditor.Text = text;
				if (base.IsReadOnly(bindingParentItem))
				{
					bool flag2 = true;
					if (this.TextEditor is GridTextBoxColumnEditor)
					{
						((GridTextBoxColumnEditor)this.TextEditor).TextBoxControl.Visible = false;
						if (this.ColumnValidationSettings.EnableRequiredFieldValidation)
						{
							((GridTextBoxColumnEditor)this.TextEditor).GetRequiredFieldValidator().Visible = false;
						}
						if (this.ColumnValidationSettings.EnableModelErrorMessageValidation)
						{
							((GridTextBoxColumnEditor)this.TextEditor).GetModelErrorMessageValidator().Visible = false;
						}
					}
					else if (this.TextEditor is GridMaskedColumnEditor)
					{
						((GridMaskedColumnEditor)this.TextEditor).MaskedTextBox.Visible = false;
						if (this.ColumnValidationSettings.EnableRequiredFieldValidation)
						{
							((GridMaskedColumnEditor)this.TextEditor).GetRequiredFieldValidator().Visible = false;
						}
						if (this.ColumnValidationSettings.EnableModelErrorMessageValidation)
						{
							((GridMaskedColumnEditor)this.TextEditor).GetModelErrorMessageValidator().Visible = false;
						}
					}
					else if (this.TextEditor is GridDateTimeColumnEditor)
					{
						if (((GridDateTimeColumnEditor)this.TextEditor).PickerControl != null)
						{
							((GridDateTimeColumnEditor)this.TextEditor).PickerControl.Visible = false;
						}
						else
						{
							((GridDateTimeColumnEditor)this.TextEditor).TextBoxControl.Visible = false;
						}
						if (this.ColumnValidationSettings.EnableRequiredFieldValidation)
						{
							((GridDateTimeColumnEditor)this.TextEditor).GetRequiredFieldValidator().Visible = false;
						}
						if (this.ColumnValidationSettings.EnableModelErrorMessageValidation)
						{
							((GridDateTimeColumnEditor)this.TextEditor).GetModelErrorMessageValidator().Visible = false;
						}
					}
					else if (this.TextEditor is GridNumericColumnEditor)
					{
						((GridNumericColumnEditor)this.TextEditor).NumericTextBox.Visible = false;
						if (this.ColumnValidationSettings.EnableRequiredFieldValidation)
						{
							((GridNumericColumnEditor)this.TextEditor).GetRequiredFieldValidator().Visible = false;
						}
						if (this.ColumnValidationSettings.EnableModelErrorMessageValidation)
						{
							((GridNumericColumnEditor)this.TextEditor).GetModelErrorMessageValidator().Visible = false;
						}
					}
					else if (this.TextEditor is GridHTMLEditorColumnEditor)
					{
						((GridHTMLEditorColumnEditor)this.TextEditor).Editor.Visible = false;
						if (this.ColumnValidationSettings.EnableRequiredFieldValidation)
						{
							((GridHTMLEditorColumnEditor)this.TextEditor).GetRequiredFieldValidator().Visible = false;
						}
						if (this.ColumnValidationSettings.EnableModelErrorMessageValidation)
						{
							((GridHTMLEditorColumnEditor)this.TextEditor).GetModelErrorMessageValidator().Visible = false;
						}
					}
					else
					{
						flag2 = false;
					}
					if (flag2)
					{
						Literal literal = (Literal)bindingParentItem.FindControl(string.Format("ROLC_{0}", this.UniqueName));
						if (literal == null && base.Owner.EditMode != GridEditMode.PopUp)
						{
							literal = new Literal();
							literal.ID = string.Format("ROLC_{0}", this.UniqueName);
							control.Controls.Add(literal);
						}
						if (literal != null)
						{
							literal.Text = this.FormatDataValue(obj2, bindingParentItem, true);
							return;
						}
					}
				}
			}
			else
			{
				if (((text.Length == 0 || !visible) && !flag) || (text.Length == 0 && visible))
				{
					text = this.EmptyDataText;
				}
				((TableCell)control).Text = text;
			}
		}

		// Token: 0x0600AD27 RID: 44327 RVA: 0x00255638 File Offset: 0x00253838
		protected override IGridColumnEditor CreateDefaultColumnEditor()
		{
			return new GridTextBoxColumnEditor(this);
		}

		// Token: 0x170037ED RID: 14317
		// (get) Token: 0x0600AD28 RID: 44328 RVA: 0x00255640 File Offset: 0x00253840
		private GridTextColumnEditor TextEditor
		{
			get
			{
				return this.CurrentColumnEditor as GridTextColumnEditor;
			}
		}

		// Token: 0x0600AD29 RID: 44329 RVA: 0x0025564D File Offset: 0x0025384D
		protected override void ColumnEditorChange(IGridColumnEditor newValue)
		{
			if (!(newValue is GridTextColumnEditor))
			{
				throw new GridColumnEditorException(this.ToString() + " accepts only editor of type: " + typeof(GridTextColumnEditor).ToString());
			}
			base.ColumnEditorChange(newValue);
		}

		// Token: 0x170037EE RID: 14318
		// (get) Token: 0x0600AD2A RID: 44330 RVA: 0x00255684 File Offset: 0x00253884
		// (set) Token: 0x0600AD2B RID: 44331 RVA: 0x002556B1 File Offset: 0x002538B1
		[Description("GridBoundColumn_DataField")]
		[Category("Data")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string DataField
		{
			get
			{
				object obj = base.ViewState["DataField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataField"] = value;
				base.UpdateUniqueNameIfDefault(value);
				this.OnColumnChanged();
			}
		}

		// Token: 0x170037EF RID: 14319
		// (get) Token: 0x0600AD2C RID: 44332 RVA: 0x002556D1 File Offset: 0x002538D1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
		public GridColumnValidationSettings ColumnValidationSettings
		{
			get
			{
				if (this._columnValidationSettings == null)
				{
					this._columnValidationSettings = new GridColumnValidationSettings(base.ViewState, this);
				}
				return this._columnValidationSettings;
			}
		}

		// Token: 0x170037F0 RID: 14320
		// (get) Token: 0x0600AD2D RID: 44333 RVA: 0x002556F4 File Offset: 0x002538F4
		// (set) Token: 0x0600AD2E RID: 44334 RVA: 0x0025571D File Offset: 0x0025391D
		[Category("Data")]
		[Description("GridBoundColumn aggregate function")]
		[DefaultValue(typeof(GridAggregateFunction), "None")]
		[NotifyParentProperty(true)]
		public virtual GridAggregateFunction Aggregate
		{
			get
			{
				object obj = base.ViewState["Aggregate"];
				if (obj != null)
				{
					return (GridAggregateFunction)obj;
				}
				return GridAggregateFunction.None;
			}
			set
			{
				base.ViewState["Aggregate"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x170037F1 RID: 14321
		// (get) Token: 0x0600AD2F RID: 44335 RVA: 0x0025573C File Offset: 0x0025393C
		// (set) Token: 0x0600AD30 RID: 44336 RVA: 0x00255765 File Offset: 0x00253965
		[DefaultValue(false)]
		[Localizable(true)]
		[Description("Sets or gets whether cell content must be encoded.")]
		[NotifyParentProperty(true)]
		public virtual bool HtmlEncode
		{
			get
			{
				object obj = base.ViewState["HtmlEncode"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["HtmlEncode"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x170037F2 RID: 14322
		// (get) Token: 0x0600AD31 RID: 44337 RVA: 0x00255784 File Offset: 0x00253984
		// (set) Token: 0x0600AD32 RID: 44338 RVA: 0x002557B1 File Offset: 0x002539B1
		[Localizable(true)]
		[DefaultValue("&nbsp;")]
		[Description("Sets or gets default text when column is empty")]
		[NotifyParentProperty(true)]
		public virtual string EmptyDataText
		{
			get
			{
				object obj = base.ViewState["EmptyDataText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&nbsp;";
			}
			set
			{
				base.ViewState["EmptyDataText"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x170037F3 RID: 14323
		// (get) Token: 0x0600AD33 RID: 44339 RVA: 0x002557CC File Offset: 0x002539CC
		// (set) Token: 0x0600AD34 RID: 44340 RVA: 0x00255830 File Offset: 0x00253A30
		[DefaultValue("")]
		[Category("Behavior")]
		[Localizable(true)]
		[Description("Sets or gets format string for the footer/group footer aggregate.")]
		[NotifyParentProperty(true)]
		public virtual string FooterAggregateFormatString
		{
			get
			{
				object obj = base.ViewState["FooterAggregateFormatString"];
				if (obj == null)
				{
					return string.Empty;
				}
				if (base.Owner != null && base.Owner.OwnerGrid.ExportSettings.SuppressColumnDataFormatStrings && base.Owner.OwnerGrid.IsExporting)
				{
					return "{0}";
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["FooterAggregateFormatString"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x170037F4 RID: 14324
		// (get) Token: 0x0600AD35 RID: 44341 RVA: 0x0025584C File Offset: 0x00253A4C
		// (set) Token: 0x0600AD36 RID: 44342 RVA: 0x00255875 File Offset: 0x00253A75
		[Category("Data")]
		[Description("")]
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		public virtual int MaxLength
		{
			get
			{
				object obj = base.ViewState["MaxLength"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["MaxLength"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x170037F5 RID: 14325
		// (get) Token: 0x0600AD37 RID: 44343 RVA: 0x002558A4 File Offset: 0x00253AA4
		// (set) Token: 0x0600AD38 RID: 44344 RVA: 0x00255908 File Offset: 0x00253B08
		[Localizable(true)]
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("GridBoundColumn_DataFormatString")]
		[NotifyParentProperty(true)]
		public virtual string DataFormatString
		{
			get
			{
				object obj = base.ViewState["DataFormatString"];
				if (obj == null)
				{
					return string.Empty;
				}
				if (base.Owner != null && base.Owner.OwnerGrid.ExportSettings.SuppressColumnDataFormatStrings && base.Owner.OwnerGrid.IsExporting)
				{
					return "{0}";
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["DataFormatString"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x0600AD39 RID: 44345 RVA: 0x00255921 File Offset: 0x00253B21
		public override bool SupportsFiltering()
		{
			return this.AllowFiltering;
		}

		// Token: 0x0600AD3A RID: 44346 RVA: 0x00255929 File Offset: 0x00253B29
		protected override string GetFilterDataField()
		{
			return this.DataField;
		}

		// Token: 0x0600AD3B RID: 44347 RVA: 0x00255931 File Offset: 0x00253B31
		internal override string GetSortExpression()
		{
			if (string.IsNullOrEmpty(this.SortExpression) && !string.IsNullOrEmpty(this.DataField) && this.AllowSorting)
			{
				return this.DataField;
			}
			return base.GetSortExpression();
		}

		// Token: 0x0600AD3C RID: 44348 RVA: 0x00255962 File Offset: 0x00253B62
		public override string GetDefaultGroupByExpression()
		{
			if (string.IsNullOrEmpty(this.DataField))
			{
				return base.GetDefaultGroupByExpression();
			}
			return this.DataField + " Group By " + this.DataField;
		}

		// Token: 0x0600AD3D RID: 44349 RVA: 0x0025598E File Offset: 0x00253B8E
		public override bool IsBoundToFieldName(string name)
		{
			return string.Compare(this.DataField, name, true) == 0;
		}

		// Token: 0x170037F6 RID: 14326
		// (get) Token: 0x0600AD3E RID: 44350 RVA: 0x002559A0 File Offset: 0x00253BA0
		public override bool IsEditable
		{
			get
			{
				return !this.ReadOnly;
			}
		}

		// Token: 0x0600AD3F RID: 44351 RVA: 0x002559AB File Offset: 0x00253BAB
		protected override string GenerateUniqueName()
		{
			return base.GenerateUniqueNameBase(this.DataField);
		}

		// Token: 0x0600AD40 RID: 44352 RVA: 0x002559BC File Offset: 0x00253BBC
		public override void FillValues(IDictionary newValues, GridEditableItem editableItem)
		{
			if (!editableItem.IsInEditMode)
			{
				if (string.Equals(editableItem[this].Text.Trim(), this.EmptyDataText, StringComparison.InvariantCultureIgnoreCase))
				{
					editableItem[this].Text = string.Empty;
				}
				newValues[this.DataField] = base.ConvertValueIfEmpty(editableItem[this].Text);
				return;
			}
			GridTextColumnEditor gridTextColumnEditor = (GridTextColumnEditor)editableItem.EditManager.GetColumnEditor(this);
			if (base.IsReadOnly(editableItem) && this.HtmlEncode)
			{
				newValues[this.DataField] = HttpUtility.HtmlDecode(gridTextColumnEditor.Text);
				return;
			}
			newValues[this.DataField] = base.ConvertValueIfEmpty(gridTextColumnEditor.Text);
		}

		// Token: 0x0600AD41 RID: 44353 RVA: 0x00255A74 File Offset: 0x00253C74
		public override IDictionary GetCustomPropertyDataFields(object dataItemInstance)
		{
			Hashtable hashtable = new Hashtable();
			GridColumn.AddSubPropertyFieldInfo(hashtable, this.DataField, dataItemInstance);
			return hashtable;
		}

		// Token: 0x0600AD42 RID: 44354 RVA: 0x00255A98 File Offset: 0x00253C98
		public override GridColumn Clone()
		{
			GridBoundColumn gridBoundColumn = new GridBoundColumn();
			gridBoundColumn.CopyBaseProperties(this);
			return gridBoundColumn;
		}

		// Token: 0x0600AD43 RID: 44355 RVA: 0x00255AB4 File Offset: 0x00253CB4
		protected override void CopyBaseProperties(GridColumn fromColumn)
		{
			base.CopyBaseProperties(fromColumn);
			GridBoundColumn gridBoundColumn = (GridBoundColumn)fromColumn;
			this.DataField = gridBoundColumn.DataField;
			this.DataFormatString = gridBoundColumn.DataFormatString;
			this.AutoPostBackOnFilter = gridBoundColumn.AutoPostBackOnFilter;
			this.Aggregate = gridBoundColumn.Aggregate;
			this.FooterAggregateFormatString = gridBoundColumn.FooterAggregateFormatString;
			this.HtmlEncode = gridBoundColumn.HtmlEncode;
			this.EmptyDataText = gridBoundColumn.EmptyDataText;
			this.MaxLength = gridBoundColumn.MaxLength;
			this.ColumnValidationSettings.CopyBaseProperties(gridBoundColumn.ColumnValidationSettings);
		}

		// Token: 0x04002DE2 RID: 11746
		private GridColumnValidationSettings _columnValidationSettings;

		// Token: 0x04002DE3 RID: 11747
		private string boundField;

		// Token: 0x04002DE4 RID: 11748
		private PropertyDescriptor boundFieldDesc;

		// Token: 0x04002DE5 RID: 11749
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		protected string formatting;

		// Token: 0x04002DE6 RID: 11750
		public static readonly string thisExpr = "!";
	}
}
