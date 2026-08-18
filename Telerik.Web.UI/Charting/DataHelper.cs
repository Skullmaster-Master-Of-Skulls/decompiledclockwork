using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Telerik.Charting
{
	// Token: 0x020016EC RID: 5868
	internal abstract class DataHelper : ICommonDataHelper
	{
		// Token: 0x1700458D RID: 17805
		// (get) Token: 0x0600E3D2 RID: 58322
		public abstract int RowsCount { get; }

		// Token: 0x1700458E RID: 17806
		// (get) Token: 0x0600E3D3 RID: 58323
		public abstract int ColumnsCount { get; }

		// Token: 0x0600E3D4 RID: 58324 RVA: 0x00328554 File Offset: 0x00326754
		public double GetDoubleValue(int rowIndex, int columnIndex)
		{
			double result = double.NaN;
			if (rowIndex >= 0 && rowIndex < this.RowsCount && columnIndex >= 0 && columnIndex < this.ColumnsCount)
			{
				object objectValue = this.GetObjectValue(rowIndex, columnIndex);
				if (objectValue != null)
				{
					if (this.IsItemNumeric(rowIndex, columnIndex))
					{
						NumberFormatInfo numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat.Clone() as NumberFormatInfo;
						numberFormatInfo.NumberGroupSeparator = "";
						try
						{
							return double.Parse(objectValue.ToString(), numberFormatInfo);
						}
						catch
						{
							numberFormatInfo.NumberDecimalSeparator = ((numberFormatInfo.NumberDecimalSeparator == ".") ? "," : ".");
							return double.Parse(objectValue.ToString(), numberFormatInfo);
						}
					}
					if (!double.TryParse(objectValue.ToString(), NumberStyles.Any, null, out result))
					{
						result = 0.0;
					}
				}
			}
			return result;
		}

		// Token: 0x0600E3D5 RID: 58325
		public abstract object GetObjectValue(int rowIndex, int columnIndex);

		// Token: 0x0600E3D6 RID: 58326 RVA: 0x00328640 File Offset: 0x00326840
		public string GetStringValue(int rowIndex, int columnIndex)
		{
			object objectValue = this.GetObjectValue(rowIndex, columnIndex);
			if (objectValue == null || objectValue == DBNull.Value)
			{
				return "Null";
			}
			if (this.IsColumnString(columnIndex))
			{
				return (string)objectValue;
			}
			return objectValue.ToString();
		}

		// Token: 0x0600E3D7 RID: 58327
		public abstract bool IsColumnNumeric(int columnIndex);

		// Token: 0x0600E3D8 RID: 58328
		public abstract bool IsColumnString(int columnIndex);

		// Token: 0x0600E3D9 RID: 58329
		public abstract int GetColumnIndex(string columnName);

		// Token: 0x0600E3DA RID: 58330
		public abstract string GetColumnName(int columnIndex);

		// Token: 0x1700458F RID: 17807
		// (get) Token: 0x0600E3DB RID: 58331
		public abstract bool ColumnNameSupported { get; }

		// Token: 0x0600E3DC RID: 58332 RVA: 0x0032867D File Offset: 0x0032687D
		public bool IsItemNumeric(int rowIndex, int columnIndex)
		{
			return rowIndex >= 0 && rowIndex < this.RowsCount && columnIndex >= 0 && columnIndex < this.ColumnsCount && DataHelper.IsValueNumeric(this.GetObjectValue(rowIndex, columnIndex));
		}

		// Token: 0x0600E3DD RID: 58333 RVA: 0x003286A8 File Offset: 0x003268A8
		public int GetLabelsColumnIndex(int groupColumn)
		{
			for (int i = this.ColumnsCount - 1; i > -1; i--)
			{
				if (i != groupColumn && !this.IsColumnNumeric(i) && this.IsColumnString(i))
				{
					this.dataHelperLabelsColumnIndex = i;
					return i;
				}
			}
			this.dataHelperLabelsColumnIndex = -1;
			return -1;
		}

		// Token: 0x0600E3DE RID: 58334 RVA: 0x003286F0 File Offset: 0x003268F0
		public int GetGroupsColumnIndex()
		{
			this.dataHelperGroupsColumnIndex = -1;
			if (this.ColumnsCount < 2)
			{
				return -1;
			}
			object[] sortedAndFilteredColumn = this.GetSortedAndFilteredColumn(0);
			if (sortedAndFilteredColumn.Length < this.RowsCount)
			{
				this.dataHelperGroupsColumnIndex = 0;
				return 0;
			}
			return -1;
		}

		// Token: 0x0600E3DF RID: 58335 RVA: 0x0032872C File Offset: 0x0032692C
		public object[] GetFilteredColumn(int columnIndex)
		{
			ArrayList arrayList = new ArrayList();
			if (columnIndex >= 0 && columnIndex < this.ColumnsCount)
			{
				for (int i = 0; i < this.RowsCount; i++)
				{
					object objectValue = this.GetObjectValue(i, columnIndex);
					if (objectValue != null && DBNull.Value != objectValue && !arrayList.Contains(objectValue))
					{
						arrayList.Add(objectValue);
					}
				}
			}
			return (object[])arrayList.ToArray(typeof(object));
		}

		// Token: 0x0600E3E0 RID: 58336 RVA: 0x00328798 File Offset: 0x00326998
		public object[] GetSortedAndFilteredColumn(int columnIndex)
		{
			ArrayList arrayList = new ArrayList();
			if (columnIndex >= 0 && columnIndex < this.ColumnsCount)
			{
				arrayList.AddRange(this.GetFilteredColumn(columnIndex));
				try
				{
					arrayList.Sort();
				}
				catch
				{
					return (object[])arrayList.ToArray(typeof(object));
				}
			}
			return (object[])arrayList.ToArray(typeof(object));
		}

		// Token: 0x0600E3E1 RID: 58337 RVA: 0x00328810 File Offset: 0x00326A10
		public int GetValuesXColumnIndex()
		{
			int num = this.dataHelperGroupsColumnIndex;
			int num2 = (this.dataHelperValuesYColumnIndex == -2) ? this.GetValuesYColumnIndex() : this.dataHelperValuesYColumnIndex;
			int num3 = (this.dataHelperLabelsColumnIndex == -2) ? this.GetLabelsColumnIndex(num) : this.dataHelperLabelsColumnIndex;
			for (int i = 0; i < this.ColumnsCount; i++)
			{
				if (this.IsColumnNumeric(i) && i != num && i != num2 && i != num3)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600E3E2 RID: 58338 RVA: 0x00328880 File Offset: 0x00326A80
		public int GetValuesYColumnIndex()
		{
			int num = this.dataHelperGroupsColumnIndex;
			for (int i = this.ColumnsCount - 1; i > -1; i--)
			{
				if (this.IsColumnNumeric(i) && i != num)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600E3E3 RID: 58339 RVA: 0x003288B8 File Offset: 0x00326AB8
		public int[] GetValuesYColumns()
		{
			int num = this.dataHelperGroupsColumnIndex;
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < this.ColumnsCount; i++)
			{
				if (this.IsColumnNumeric(i) && i != num)
				{
					arrayList.Add(i);
				}
			}
			return (int[])arrayList.ToArray(typeof(int));
		}

		// Token: 0x0600E3E4 RID: 58340 RVA: 0x00328914 File Offset: 0x00326B14
		public int[] GetGanttValuesColumns()
		{
			int num = this.dataHelperGroupsColumnIndex;
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			for (int i = 0; i < this.ColumnsCount; i++)
			{
				if (this.IsColumnNumeric(i) && i != num)
				{
					arrayList2.Add(i);
				}
			}
			switch (arrayList2.Count)
			{
			case 0:
				break;
			case 1:
				arrayList.Add(arrayList2[0]);
				arrayList.Add(arrayList2[0]);
				arrayList.Add(arrayList2[0]);
				arrayList.Add(arrayList2[0]);
				break;
			case 2:
				arrayList.Add(arrayList2[0]);
				arrayList.Add(arrayList2[1]);
				arrayList.Add(arrayList2[0]);
				arrayList.Add(arrayList2[1]);
				break;
			case 3:
				arrayList.Add(arrayList2[0]);
				arrayList.Add(arrayList2[1]);
				arrayList.Add(arrayList2[2]);
				arrayList.Add(arrayList2[0]);
				break;
			default:
				arrayList.Add(arrayList2[0]);
				arrayList.Add(arrayList2[1]);
				arrayList.Add(arrayList2[2]);
				arrayList.Add(arrayList2[3]);
				break;
			}
			return (int[])arrayList.ToArray(typeof(int));
		}

		// Token: 0x0600E3E5 RID: 58341 RVA: 0x00328A81 File Offset: 0x00326C81
		protected static bool IsNullableType(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x0600E3E6 RID: 58342 RVA: 0x00328AA4 File Offset: 0x00326CA4
		protected static bool IsTypeNumeric(Type type)
		{
			return type == typeof(byte) || type == typeof(int) || type == typeof(short) || type == typeof(long) || type == typeof(float) || type == typeof(double) || type == typeof(decimal) || type == typeof(SqlDecimal) || type == typeof(SqlDouble) || type == typeof(SqlInt16) || type == typeof(SqlInt32) || type == typeof(SqlInt64) || type == typeof(SqlMoney) || type == typeof(SqlSingle) || type == typeof(SqlByte);
		}

		// Token: 0x0600E3E7 RID: 58343 RVA: 0x00328BD8 File Offset: 0x00326DD8
		internal static bool IsValueNumeric(object obj)
		{
			if (obj != null)
			{
				NumberFormatInfo numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat.Clone() as NumberFormatInfo;
				numberFormatInfo.NumberGroupSeparator = "";
				if (DataHelper.IsTypeNumeric(obj.GetType()))
				{
					return true;
				}
				double num;
				if (double.TryParse(obj.ToString(), NumberStyles.Number, numberFormatInfo, out num))
				{
					return true;
				}
				numberFormatInfo.NumberDecimalSeparator = ((numberFormatInfo.NumberDecimalSeparator == ".") ? "," : ".");
				if (double.TryParse(obj.ToString(), NumberStyles.Number, numberFormatInfo, out num))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600E3E8 RID: 58344 RVA: 0x00328C64 File Offset: 0x00326E64
		protected static bool IsTypeString(Type type)
		{
			return type == typeof(string) || type == typeof(DateTime);
		}

		// Token: 0x0600E3E9 RID: 58345 RVA: 0x00328C8D File Offset: 0x00326E8D
		protected static bool IsValueString(object obj)
		{
			return DataHelper.IsTypeString(obj.GetType());
		}

		// Token: 0x0600E3EA RID: 58346 RVA: 0x00328CA0 File Offset: 0x00326EA0
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		internal static ICommonDataHelper CreateDataHelper(object dataSource, string dataMember, bool isDesign)
		{
			ICommonDataHelper result = null;
			object obj = dataSource;
			BindingSource bindingSource = dataSource as BindingSource;
			if (bindingSource != null)
			{
				if (bindingSource.Count <= 0 && !isDesign)
				{
					return null;
				}
				obj = bindingSource.List;
			}
			if (obj is DataSet)
			{
				DataSet dataSet = (DataSet)obj;
				DataTable data = null;
				if (dataSet.Tables.Count > 0)
				{
					if (!string.IsNullOrEmpty(dataMember))
					{
						if (dataSet.Tables.Contains(dataMember))
						{
							data = dataSet.Tables[dataMember];
						}
					}
					else
					{
						data = dataSet.Tables[0];
					}
				}
				result = new DataTableDataHelper(data);
			}
			else if (obj is DataViewManager)
			{
				DataSet dataSet2 = ((DataViewManager)obj).DataSet;
				DataTable data2 = null;
				if (dataSet2.Tables.Count > 0)
				{
					if (!string.IsNullOrEmpty(dataMember) && dataSet2.Tables.Contains(dataMember))
					{
						data2 = dataSet2.Tables[dataMember];
					}
					else
					{
						data2 = dataSet2.Tables[0];
					}
				}
				result = new DataTableDataHelper(data2);
			}
			else if (obj is DataView)
			{
				DataTable data3 = ((DataView)obj).ToTable();
				result = new DataTableDataHelper(data3);
			}
			else if (obj is DataTable)
			{
				result = new DataTableDataHelper((DataTable)obj);
			}
			else if (obj is IDataReader)
			{
				DataTable dataTable = new DataTable();
				dataTable.Load((IDataReader)obj);
				result = new DataTableDataHelper(dataTable);
			}
			else if (obj is Array)
			{
				result = new ArrayDataHelper((Array)obj);
			}
			else if (obj is string)
			{
				string text = (string)obj;
				if (File.Exists(text))
				{
					if (text.EndsWith(".xml"))
					{
						DataSet dataSet3 = new DataSet();
						dataSet3.ReadXml(text);
						result = new DataTableDataHelper(dataSet3.Tables[0]);
					}
					else
					{
						DataTable dataTable2 = new DataTable();
						ArrayList arrayList = new ArrayList();
						StreamReader streamReader = new StreamReader(text);
						string text2;
						while (!string.IsNullOrEmpty(text2 = streamReader.ReadLine()))
						{
							arrayList.Add(text2.Split(new char[]
							{
								',',
								'|',
								';',
								'\t'
							}));
						}
						streamReader.Close();
						if (arrayList.Count > 0)
						{
							int num = ((string[])arrayList[0]).Length;
							foreach (string text3 in (string[])arrayList[0])
							{
								dataTable2.Columns.Add(text3.ToString());
							}
							if (dataTable2.Columns.Count > 0)
							{
								for (int j = 1; j < arrayList.Count; j++)
								{
									ArrayList arrayList2 = new ArrayList();
									string[] array2 = (string[])arrayList[j];
									for (int k = 0; k < num; k++)
									{
										try
										{
											arrayList2.Add(array2[k]);
										}
										catch
										{
											arrayList2.Add("");
										}
									}
									dataTable2.Rows.Add((string[])arrayList2.ToArray(typeof(string)));
								}
							}
							result = new DataTableDataHelper(dataTable2);
						}
					}
				}
			}
			else if (obj is IEnumerable)
			{
				DataTable dataTable3 = new DataTable();
				IEnumerable enumerable = (IEnumerable)obj;
				IEnumerator enumerator = enumerable.GetEnumerator();
				PropertyDescriptorCollection propertyDescriptorCollection = null;
				if (!isDesign)
				{
					try
					{
						enumerator.Reset();
					}
					catch
					{
						enumerator = enumerable.GetEnumerator();
					}
					try
					{
						enumerator.MoveNext();
						propertyDescriptorCollection = TypeDescriptor.GetProperties(enumerator.Current);
						goto IL_3CA;
					}
					catch
					{
						propertyDescriptorCollection = null;
						goto IL_3CA;
					}
				}
				try
				{
					try
					{
						enumerator.Reset();
					}
					catch
					{
						enumerator = enumerable.GetEnumerator();
					}
					enumerator.MoveNext();
					propertyDescriptorCollection = TypeDescriptor.GetProperties(enumerator.Current);
				}
				catch
				{
					CurrencyManager currencyManager = (CurrencyManager)new BindingContext()[dataSource, dataMember];
					if (currencyManager != null)
					{
						propertyDescriptorCollection = currencyManager.GetItemProperties();
					}
				}
				IL_3CA:
				if (propertyDescriptorCollection != null)
				{
					if (propertyDescriptorCollection.Count > 0)
					{
						for (int l = 0; l < propertyDescriptorCollection.Count; l++)
						{
							Type propertyType = propertyDescriptorCollection[l].PropertyType;
							if (DataHelper.IsNullableType(propertyType))
							{
								NullableConverter nullableConverter = new NullableConverter(propertyType);
								dataTable3.Columns.Add(propertyDescriptorCollection[l].Name, nullableConverter.UnderlyingType);
							}
							else
							{
								dataTable3.Columns.Add(propertyDescriptorCollection[l].Name, propertyType);
							}
						}
						do
						{
							propertyDescriptorCollection = TypeDescriptor.GetProperties(enumerator.Current);
							DataRow dataRow = dataTable3.NewRow();
							for (int m = 0; m < propertyDescriptorCollection.Count; m++)
							{
								object value = propertyDescriptorCollection[m].GetValue(enumerator.Current);
								if (DataHelper.IsNullableType(propertyDescriptorCollection[m].PropertyType) && value == null)
								{
									dataRow[propertyDescriptorCollection[m].Name] = DBNull.Value;
								}
								else
								{
									dataRow[propertyDescriptorCollection[m].Name] = value;
								}
							}
							dataTable3.Rows.Add(dataRow);
						}
						while (enumerator.MoveNext());
						result = new DataTableDataHelper(dataTable3);
					}
					else if (!isDesign && enumerator.Current is ValueType)
					{
						result = new ListDataHelper((IList)obj);
					}
				}
			}
			return result;
		}

		// Token: 0x040041B9 RID: 16825
		private int dataHelperGroupsColumnIndex = -2;

		// Token: 0x040041BA RID: 16826
		private int dataHelperLabelsColumnIndex = -2;

		// Token: 0x040041BB RID: 16827
		private int dataHelperValuesYColumnIndex = -2;
	}
}
