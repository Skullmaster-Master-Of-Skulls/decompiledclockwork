using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Telerik.Web.UI
{
	// Token: 0x020010EC RID: 4332
	internal abstract class GridResolveEnumerable
	{
		// Token: 0x17003978 RID: 14712
		// (get) Token: 0x0600B16B RID: 45419 RVA: 0x002663B0 File Offset: 0x002645B0
		protected IEnumerable enumerable
		{
			get
			{
				if (this.rawEnumerator == null)
				{
					this.rawEnumerator = this.rawEnumerable.GetEnumerator();
				}
				return new AdvancedEnumerable(this.rawEnumerator, this.firstDataItem);
			}
		}

		// Token: 0x0600B16C RID: 45420 RVA: 0x002663DC File Offset: 0x002645DC
		public GridResolveEnumerable(IEnumerable rawEnumerable)
		{
			this.rawEnumerable = rawEnumerable;
		}

		// Token: 0x0600B16D RID: 45421 RVA: 0x002663F9 File Offset: 0x002645F9
		public GridResolveEnumerable(IEnumerable rawEnumerable, bool generateDataTable)
		{
			this.rawEnumerable = rawEnumerable;
			this.generateDataTable = generateDataTable;
		}

		// Token: 0x17003979 RID: 14713
		// (get) Token: 0x0600B16E RID: 45422 RVA: 0x0026641D File Offset: 0x0026461D
		public DataTable DataTable
		{
			get
			{
				this.EnsureInitialized();
				return this.GetDataTable();
			}
		}

		// Token: 0x1700397A RID: 14714
		// (get) Token: 0x0600B16F RID: 45423 RVA: 0x0026642B File Offset: 0x0026462B
		public ArrayList Columns
		{
			get
			{
				this.EnsureInitialized();
				return this.GetColumns();
			}
		}

		// Token: 0x0600B170 RID: 45424
		protected abstract DataTable GetDataTable();

		// Token: 0x0600B171 RID: 45425
		protected abstract ArrayList GetColumns();

		// Token: 0x0600B172 RID: 45426
		protected abstract void CreateColumn(PropertyDescriptor descriptor);

		// Token: 0x0600B173 RID: 45427
		protected abstract void CreateColumn(Type type);

		// Token: 0x0600B174 RID: 45428
		protected abstract void CreateColumn(DataColumn column);

		// Token: 0x0600B175 RID: 45429
		protected abstract void FillData();

		// Token: 0x1700397B RID: 14715
		// (get) Token: 0x0600B176 RID: 45430
		protected abstract int ColumnsCount { get; }

		// Token: 0x0600B177 RID: 45431 RVA: 0x00266439 File Offset: 0x00264639
		private void EnsureInitialized()
		{
			if (this.isInitialized)
			{
				return;
			}
			this.Initialize();
			this.isInitialized = true;
		}

		// Token: 0x0600B178 RID: 45432 RVA: 0x00266451 File Offset: 0x00264651
		protected virtual void Initialize()
		{
			this.ParseProperties();
			if (this.generateDataTable && this.HasData)
			{
				this.FillData();
			}
		}

		// Token: 0x0600B179 RID: 45433 RVA: 0x00266470 File Offset: 0x00264670
		protected PropertyDescriptorCollection GetItemProperties(object source)
		{
			ITypedList typedList = source as ITypedList;
			if (source != null && typedList != null)
			{
				return typedList.GetItemProperties(new PropertyDescriptor[0]);
			}
			return null;
		}

		// Token: 0x0600B17A RID: 45434 RVA: 0x00266498 File Offset: 0x00264698
		protected void ParseProperties()
		{
			bool flag = true;
			PropertyDescriptorCollection propertyDescriptorCollection = this.GetItemProperties(this.rawEnumerable);
			object dataItemInstance = null;
			if (propertyDescriptorCollection == null)
			{
				Type type;
				object obj;
				flag = this.GetCollectionItemType(flag, out type, out obj);
				if (obj != null && type.IsAssignableFrom(typeof(DataRow)))
				{
					DataRow dataRow = obj as DataRow;
					foreach (object obj2 in dataRow.Table.Columns)
					{
						DataColumn column = (DataColumn)obj2;
						this.CreateColumn(column);
					}
					return;
				}
				if (obj != null && (type.FullName == "Microsoft.SharePoint.WebControls.SPDataSourceViewResultItem" || type.FullName == "Microsoft.SharePoint.SPListItem"))
				{
					List<DataColumn> list = this.ParseSPListItemProperties<object>(obj);
					foreach (DataColumn column2 in list)
					{
						this.CreateColumn(column2);
					}
					return;
				}
				if (obj != null && obj is ICustomTypeDescriptor)
				{
					propertyDescriptorCollection = TypeDescriptor.GetProperties(obj);
				}
				else if (type != null && type != typeof(object))
				{
					if (GridBaseDataList.IsBindableType(type))
					{
						this.CreateColumn(type);
					}
					else
					{
						propertyDescriptorCollection = TypeDescriptor.GetProperties(type);
					}
				}
				else if (this.rawEnumerable is IDataReader)
				{
					propertyDescriptorCollection = this.GetPropertiesForDataReader();
				}
				else if (GridDataSourceHelper.IsOpenAccess(this.rawEnumerable.GetType()))
				{
					Type[] genericArguments = this.rawEnumerable.GetType().GetGenericArguments();
					if (genericArguments.Length > 0)
					{
						propertyDescriptorCollection = TypeDescriptor.GetProperties(genericArguments[0]);
					}
				}
				else if (GridDataSourceHelper.IsEntity(this.rawEnumerable.GetType()))
				{
					MethodBase method = this.rawEnumerable.GetType().GetMethod("GetItemProperties");
					object obj3 = this.rawEnumerable;
					object[] parameters = new object[1];
					propertyDescriptorCollection = (method.Invoke(obj3, parameters) as PropertyDescriptorCollection);
				}
				dataItemInstance = obj;
			}
			if (propertyDescriptorCollection != null && propertyDescriptorCollection.Count != 0)
			{
				foreach (object obj4 in propertyDescriptorCollection)
				{
					PropertyDescriptor descriptor = (PropertyDescriptor)obj4;
					this.CreateColumn(descriptor);
				}
			}
			this.FinishedParsingProperties(dataItemInstance);
			if (this.ColumnsCount == 0 && flag)
			{
				this.OnNoBindableProperties();
				return;
			}
			if (!flag)
			{
				this.HasData = false;
			}
		}

		// Token: 0x0600B17B RID: 45435 RVA: 0x0026671C File Offset: 0x0026491C
		internal PropertyDescriptorCollection GetPropertiesForDataReader()
		{
			ArrayList arrayList = new ArrayList();
			PropertyDescriptor[] array = new PropertyDescriptor[0];
			try
			{
				if (this.rawEnumerable is DbDataReader)
				{
					DbDataReader dbDataReader = (DbDataReader)this.rawEnumerable;
					for (int i = 0; i < dbDataReader.FieldCount; i++)
					{
						Type fieldType = dbDataReader.GetFieldType(i);
						string name = dbDataReader.GetName(i);
						GridPropertyDescriptor value = new GridPropertyDescriptor(name, false, fieldType);
						arrayList.Add(value);
					}
					array = new PropertyDescriptor[arrayList.Count];
					arrayList.CopyTo(array);
				}
			}
			catch (InvalidOperationException)
			{
			}
			return new PropertyDescriptorCollection(array);
		}

		// Token: 0x0600B17C RID: 45436 RVA: 0x002667B8 File Offset: 0x002649B8
		[SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate")]
		private bool GetCollectionItemType(bool noItemsInEnumerator, out Type collectionItemType, out object collectionFirstObject)
		{
			Type type = null;
			object obj = null;
			Type type2 = this.rawEnumerable.GetType();
			if (type2.HasElementType)
			{
				type = type2.GetElementType();
			}
			Type[] types = new Type[]
			{
				typeof(int)
			};
			PropertyInfo property = type2.GetProperty("Item", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, null, types, null);
			if (type == null && property != null)
			{
				type = property.PropertyType;
			}
			if (type == null || type == typeof(object) || (type == typeof(DataRow) && this.rawEnumerable is DataRow[]) || (type == typeof(DataRow) && this.rawEnumerable is IEnumerable<DataRow>) || (type.FullName == "Microsoft.SharePoint.WebControls.SPDataSourceViewResultItem" && this.rawEnumerable.GetType().FullName == "Microsoft.SharePoint.WebControls.SPDataSourceViewResultItem[]") || type.FullName == "Microsoft.SharePoint.SPListItem")
			{
				IEnumerator enumerator = this.rawEnumerable.GetEnumerator();
				if (enumerator.MoveNext())
				{
					obj = enumerator.Current;
				}
				else
				{
					noItemsInEnumerator = false;
				}
				if (obj != null)
				{
					type = obj.GetType();
				}
				this.rawEnumerator = enumerator;
				this.firstDataItem = obj;
			}
			collectionItemType = type;
			collectionFirstObject = obj;
			if (type == null && obj == null)
			{
				try
				{
					Type[] genericArguments = type2.GetGenericArguments();
					if (genericArguments != null && genericArguments.Length > 0)
					{
						type = genericArguments[0];
						collectionItemType = type;
					}
				}
				catch (NotSupportedException)
				{
				}
			}
			return noItemsInEnumerator;
		}

		// Token: 0x0600B17D RID: 45437 RVA: 0x00266948 File Offset: 0x00264B48
		protected virtual List<DataColumn> ParseSPListItemProperties<T>(T firstObject)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600B17E RID: 45438 RVA: 0x0026694F File Offset: 0x00264B4F
		protected virtual void OnNoBindableProperties()
		{
			throw new GridNotSupportedException("Cannot find any bindable properties in an item from the datasource");
		}

		// Token: 0x0600B17F RID: 45439 RVA: 0x0026695B File Offset: 0x00264B5B
		protected virtual void FinishedParsingProperties(object dataItemInstance)
		{
		}

		// Token: 0x04002E86 RID: 11910
		private IEnumerable rawEnumerable;

		// Token: 0x04002E87 RID: 11911
		private object firstDataItem;

		// Token: 0x04002E88 RID: 11912
		private IEnumerator rawEnumerator;

		// Token: 0x04002E89 RID: 11913
		private bool isInitialized;

		// Token: 0x04002E8A RID: 11914
		private bool generateDataTable = true;

		// Token: 0x04002E8B RID: 11915
		protected bool HasData = true;
	}
}
