using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Design;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000037 RID: 55
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public sealed class DesignTimeData
	{
		// Token: 0x060001EA RID: 490 RVA: 0x0000362F File Offset: 0x0000182F
		private DesignTimeData()
		{
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000D120 File Offset: 0x0000B320
		public static DataTable CreateDummyDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.Locale = CultureInfo.InvariantCulture;
			DataColumnCollection columns = dataTable.Columns;
			columns.Add(SR.GetString("Sample_Column", new object[]
			{
				0
			}), typeof(string));
			columns.Add(SR.GetString("Sample_Column", new object[]
			{
				1
			}), typeof(string));
			columns.Add(SR.GetString("Sample_Column", new object[]
			{
				2
			}), typeof(string));
			return dataTable;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000D1C4 File Offset: 0x0000B3C4
		public static DataTable CreateDummyDataBoundDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.Locale = CultureInfo.InvariantCulture;
			DataColumnCollection columns = dataTable.Columns;
			columns.Add(SR.GetString("Sample_Databound_Column", new object[]
			{
				0
			}), typeof(string));
			columns.Add(SR.GetString("Sample_Databound_Column", new object[]
			{
				1
			}), typeof(int));
			columns.Add(SR.GetString("Sample_Databound_Column", new object[]
			{
				2
			}), typeof(string));
			return dataTable;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000D268 File Offset: 0x0000B468
		public static DataTable CreateSampleDataTable(IEnumerable referenceData)
		{
			return DesignTimeData.CreateSampleDataTableInternal(referenceData, false);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000D271 File Offset: 0x0000B471
		public static DataTable CreateSampleDataTable(IEnumerable referenceData, bool useDataBoundData)
		{
			return DesignTimeData.CreateSampleDataTableInternal(referenceData, useDataBoundData);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000D27C File Offset: 0x0000B47C
		private static DataTable CreateSampleDataTableInternal(IEnumerable referenceData, bool useDataBoundData)
		{
			DataTable dataTable = new DataTable();
			dataTable.Locale = CultureInfo.InvariantCulture;
			DataColumnCollection columns = dataTable.Columns;
			PropertyDescriptorCollection dataFields = DesignTimeData.GetDataFields(referenceData);
			if (dataFields != null)
			{
				foreach (object obj in dataFields)
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
					Type type = propertyDescriptor.PropertyType;
					if (!type.IsPrimitive && type != typeof(DateTime) && type != typeof(decimal) && type != typeof(DateTimeOffset) && type != typeof(TimeSpan))
					{
						type = typeof(string);
					}
					columns.Add(propertyDescriptor.Name, type);
				}
			}
			if (columns.Count != 0)
			{
				return dataTable;
			}
			if (useDataBoundData)
			{
				return DesignTimeData.CreateDummyDataBoundDataTable();
			}
			return DesignTimeData.CreateDummyDataTable();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000D38C File Offset: 0x0000B58C
		public static PropertyDescriptorCollection GetDataFields(IEnumerable dataSource)
		{
			if (dataSource is ITypedList)
			{
				return ((ITypedList)dataSource).GetItemProperties(new PropertyDescriptor[0]);
			}
			Type type = dataSource.GetType();
			PropertyInfo property = type.GetProperty("Item", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, null, new Type[]
			{
				typeof(int)
			}, null);
			if (property != null && property.PropertyType != typeof(object))
			{
				return TypeDescriptor.GetProperties(property.PropertyType);
			}
			return null;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000D40C File Offset: 0x0000B60C
		public static string[] GetDataMembers(object dataSource)
		{
			IListSource listSource = dataSource as IListSource;
			if (listSource != null && listSource.ContainsListCollection)
			{
				IList list = ((IListSource)dataSource).GetList();
				ITypedList typedList = list as ITypedList;
				if (typedList != null)
				{
					PropertyDescriptorCollection itemProperties = typedList.GetItemProperties(new PropertyDescriptor[0]);
					if (itemProperties != null)
					{
						ArrayList arrayList = new ArrayList(itemProperties.Count);
						foreach (object obj in itemProperties)
						{
							PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
							arrayList.Add(propertyDescriptor.Name);
						}
						return (string[])arrayList.ToArray(typeof(string));
					}
				}
			}
			return null;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000D4D4 File Offset: 0x0000B6D4
		public static IEnumerable GetDataMember(IListSource dataSource, string dataMember)
		{
			IEnumerable result = null;
			IList list = dataSource.GetList();
			if (list != null && list is ITypedList)
			{
				if (!dataSource.ContainsListCollection)
				{
					if (dataMember != null && dataMember.Length != 0)
					{
						throw new ArgumentException(SR.GetString("DesignTimeData_BadDataMember"));
					}
					result = list;
				}
				else
				{
					ITypedList typedList = (ITypedList)list;
					PropertyDescriptorCollection itemProperties = typedList.GetItemProperties(new PropertyDescriptor[0]);
					if (itemProperties != null && itemProperties.Count != 0)
					{
						PropertyDescriptor propertyDescriptor;
						if (dataMember == null || dataMember.Length == 0)
						{
							propertyDescriptor = itemProperties[0];
						}
						else
						{
							propertyDescriptor = itemProperties.Find(dataMember, true);
						}
						if (propertyDescriptor != null)
						{
							object component = list[0];
							object value = propertyDescriptor.GetValue(component);
							if (value != null && value is IEnumerable)
							{
								result = (IEnumerable)value;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000D594 File Offset: 0x0000B794
		public static IEnumerable GetDesignTimeDataSource(DataTable dataTable, int minimumRows)
		{
			int count = dataTable.Rows.Count;
			if (count < minimumRows)
			{
				int num = minimumRows - count;
				DataRowCollection rows = dataTable.Rows;
				DataColumnCollection columns = dataTable.Columns;
				int count2 = columns.Count;
				DataRow[] array = new DataRow[num];
				for (int i = 0; i < num; i++)
				{
					DataRow dataRow = dataTable.NewRow();
					int num2 = count + i;
					for (int j = 0; j < count2; j++)
					{
						Type dataType = columns[j].DataType;
						object value;
						if (dataType == typeof(string))
						{
							value = SR.GetString("Sample_Databound_Text_Alt");
						}
						else if (dataType == typeof(int) || dataType == typeof(short) || dataType == typeof(long) || dataType == typeof(uint) || dataType == typeof(ushort) || dataType == typeof(ulong))
						{
							value = num2;
						}
						else if (dataType == typeof(byte) || dataType == typeof(sbyte))
						{
							value = ((num2 % 2 != 0) ? 1 : 0);
						}
						else if (dataType == typeof(bool))
						{
							value = (num2 % 2 != 0);
						}
						else if (dataType == typeof(DateTime))
						{
							value = DateTime.Today;
						}
						else if (dataType == typeof(double) || dataType == typeof(float) || dataType == typeof(decimal))
						{
							value = (double)i / 10.0;
						}
						else if (dataType == typeof(char))
						{
							value = 'x';
						}
						else if (dataType == typeof(TimeSpan))
						{
							value = TimeSpan.Zero;
						}
						else if (dataType == typeof(DateTimeOffset))
						{
							value = DateTimeOffset.Now;
						}
						else
						{
							value = DBNull.Value;
						}
						dataRow[j] = value;
					}
					rows.Add(dataRow);
				}
			}
			return new DataView(dataTable);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000D828 File Offset: 0x0000BA28
		public static object GetSelectedDataSource(IComponent component, string dataSource)
		{
			object result = null;
			ISite site = component.Site;
			if (site != null)
			{
				IContainer container = (IContainer)site.GetService(typeof(IContainer));
				if (container != null)
				{
					IComponent component2 = container.Components[dataSource];
					if (component2 is IEnumerable || component2 is IListSource)
					{
						result = component2;
					}
				}
			}
			return result;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000D87C File Offset: 0x0000BA7C
		public static IEnumerable GetSelectedDataSource(IComponent component, string dataSource, string dataMember)
		{
			IEnumerable result = null;
			object selectedDataSource = DesignTimeData.GetSelectedDataSource(component, dataSource);
			if (selectedDataSource != null)
			{
				IListSource listSource = selectedDataSource as IListSource;
				if (listSource != null)
				{
					if (!listSource.ContainsListCollection)
					{
						result = listSource.GetList();
					}
					else
					{
						result = DesignTimeData.GetDataMember(listSource, dataMember);
					}
				}
				else
				{
					result = (IEnumerable)selectedDataSource;
				}
			}
			return result;
		}

		// Token: 0x0400012F RID: 303
		public static readonly EventHandler DataBindingHandler = new EventHandler(GlobalDataBindingHandler.OnDataBind);
	}
}
