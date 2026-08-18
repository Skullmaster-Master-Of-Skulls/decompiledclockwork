using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x02000122 RID: 290
	[Editor("Microsoft.VSDesigner.Data.Design.DataTableMappingCollectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ListBindable(false)]
	public sealed class DataTableMappingCollection : MarshalByRefObject, ITableMappingCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x00237318 File Offset: 0x00236718
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x00237328 File Offset: 0x00236728
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x00237338 File Offset: 0x00236738
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00237348 File Offset: 0x00236748
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700026C RID: 620
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.ValidateType(value);
				this[index] = (DataTableMapping)value;
			}
		}

		// Token: 0x1700026D RID: 621
		object ITableMappingCollection.this[string index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.ValidateType(value);
				this[index] = (DataTableMapping)value;
			}
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x002373F8 File Offset: 0x002367F8
		ITableMapping ITableMappingCollection.Add(string sourceTableName, string dataSetTableName)
		{
			return this.Add(sourceTableName, dataSetTableName);
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00237418 File Offset: 0x00236818
		ITableMapping ITableMappingCollection.GetByDataSetTable(string dataSetTableName)
		{
			return this.GetByDataSetTable(dataSetTableName);
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x00237438 File Offset: 0x00236838
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("DataTableMappings_Count")]
		public int Count
		{
			get
			{
				if (this.items == null)
				{
					return 0;
				}
				return this.items.Count;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x00237468 File Offset: 0x00236868
		private Type ItemType
		{
			get
			{
				return typeof(DataTableMapping);
			}
		}

		// Token: 0x17000270 RID: 624
		[Browsable(false)]
		[ResDescription("DataTableMappings_Item")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataTableMapping this[int index]
		{
			get
			{
				this.RangeCheck(index);
				return this.items[index];
			}
			set
			{
				this.RangeCheck(index);
				this.Replace(index, value);
			}
		}

		// Token: 0x17000271 RID: 625
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("DataTableMappings_Item")]
		public DataTableMapping this[string sourceTable]
		{
			get
			{
				int index = this.RangeCheck(sourceTable);
				return this.items[index];
			}
			set
			{
				int index = this.RangeCheck(sourceTable);
				this.Replace(index, value);
			}
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00237518 File Offset: 0x00236918
		public int Add(object value)
		{
			this.ValidateType(value);
			this.Add((DataTableMapping)value);
			return this.Count - 1;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00237548 File Offset: 0x00236948
		private DataTableMapping Add(DataTableMapping value)
		{
			this.AddWithoutEvents(value);
			return value;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00237568 File Offset: 0x00236968
		public void AddRange(DataTableMapping[] values)
		{
			this.AddEnumerableRange(values, false);
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00237588 File Offset: 0x00236988
		public void AddRange(Array values)
		{
			this.AddEnumerableRange(values, false);
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x002375A8 File Offset: 0x002369A8
		private void AddEnumerableRange(IEnumerable values, bool doClone)
		{
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			foreach (object value in values)
			{
				this.ValidateType(value);
			}
			if (doClone)
			{
				using (IEnumerator enumerator2 = values.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						object obj = enumerator2.Current;
						ICloneable cloneable = (ICloneable)obj;
						this.AddWithoutEvents(cloneable.Clone() as DataTableMapping);
					}
					return;
				}
			}
			foreach (object obj2 in values)
			{
				DataTableMapping value2 = (DataTableMapping)obj2;
				this.AddWithoutEvents(value2);
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x002376C8 File Offset: 0x00236AC8
		public DataTableMapping Add(string sourceTable, string dataSetTable)
		{
			return this.Add(new DataTableMapping(sourceTable, dataSetTable));
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x002376E8 File Offset: 0x00236AE8
		private void AddWithoutEvents(DataTableMapping value)
		{
			this.Validate(-1, value);
			value.Parent = this;
			this.ArrayList().Add(value);
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00237718 File Offset: 0x00236B18
		private List<DataTableMapping> ArrayList()
		{
			if (this.items == null)
			{
				this.items = new List<DataTableMapping>();
			}
			return this.items;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00237748 File Offset: 0x00236B48
		public void Clear()
		{
			if (0 < this.Count)
			{
				this.ClearWithoutEvents();
			}
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00237768 File Offset: 0x00236B68
		private void ClearWithoutEvents()
		{
			if (this.items != null)
			{
				foreach (DataTableMapping dataTableMapping in this.items)
				{
					dataTableMapping.Parent = null;
				}
				this.items.Clear();
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x002377E8 File Offset: 0x00236BE8
		public bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00237808 File Offset: 0x00236C08
		public bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00237828 File Offset: 0x00236C28
		public void CopyTo(Array array, int index)
		{
			((ICollection)this.ArrayList()).CopyTo(array, index);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00237848 File Offset: 0x00236C48
		public void CopyTo(DataTableMapping[] array, int index)
		{
			this.ArrayList().CopyTo(array, index);
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00237868 File Offset: 0x00236C68
		public DataTableMapping GetByDataSetTable(string dataSetTable)
		{
			int num = this.IndexOfDataSetTable(dataSetTable);
			if (0 > num)
			{
				throw ADP.TablesDataSetTable(dataSetTable);
			}
			return this.items[num];
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00237898 File Offset: 0x00236C98
		public IEnumerator GetEnumerator()
		{
			return this.ArrayList().GetEnumerator();
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x002378B8 File Offset: 0x00236CB8
		public int IndexOf(object value)
		{
			if (value != null)
			{
				this.ValidateType(value);
				for (int i = 0; i < this.Count; i++)
				{
					if (this.items[i] == value)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x002378F8 File Offset: 0x00236CF8
		public int IndexOf(string sourceTable)
		{
			if (!ADP.IsEmpty(sourceTable))
			{
				for (int i = 0; i < this.Count; i++)
				{
					string sourceTable2 = this.items[i].SourceTable;
					if (sourceTable2 != null && ADP.SrcCompare(sourceTable, sourceTable2) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00237948 File Offset: 0x00236D48
		public int IndexOfDataSetTable(string dataSetTable)
		{
			if (!ADP.IsEmpty(dataSetTable))
			{
				for (int i = 0; i < this.Count; i++)
				{
					string dataSetTable2 = this.items[i].DataSetTable;
					if (dataSetTable2 != null && ADP.DstCompare(dataSetTable, dataSetTable2) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00237998 File Offset: 0x00236D98
		public void Insert(int index, object value)
		{
			this.ValidateType(value);
			this.Insert(index, (DataTableMapping)value);
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x002379C8 File Offset: 0x00236DC8
		public void Insert(int index, DataTableMapping value)
		{
			if (value == null)
			{
				throw ADP.TablesAddNullAttempt("value");
			}
			this.Validate(-1, value);
			value.Parent = this;
			this.ArrayList().Insert(index, value);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00237A08 File Offset: 0x00236E08
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw ADP.TablesIndexInt32(index, this);
			}
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00237A38 File Offset: 0x00236E38
		private int RangeCheck(string sourceTable)
		{
			int num = this.IndexOf(sourceTable);
			if (num < 0)
			{
				throw ADP.TablesSourceIndex(sourceTable);
			}
			return num;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00237A68 File Offset: 0x00236E68
		public void RemoveAt(int index)
		{
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00237A88 File Offset: 0x00236E88
		public void RemoveAt(string sourceTable)
		{
			int index = this.RangeCheck(sourceTable);
			this.RemoveIndex(index);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00237AA8 File Offset: 0x00236EA8
		private void RemoveIndex(int index)
		{
			this.items[index].Parent = null;
			this.items.RemoveAt(index);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00237AD8 File Offset: 0x00236ED8
		public void Remove(object value)
		{
			this.ValidateType(value);
			this.Remove((DataTableMapping)value);
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00237AF8 File Offset: 0x00236EF8
		public void Remove(DataTableMapping value)
		{
			if (value == null)
			{
				throw ADP.TablesAddNullAttempt("value");
			}
			int num = this.IndexOf(value);
			if (-1 != num)
			{
				this.RemoveIndex(num);
				return;
			}
			throw ADP.CollectionRemoveInvalidObject(this.ItemType, this);
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00237B38 File Offset: 0x00236F38
		private void Replace(int index, DataTableMapping newValue)
		{
			this.Validate(index, newValue);
			this.items[index].Parent = null;
			newValue.Parent = this;
			this.items[index] = newValue;
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00237B78 File Offset: 0x00236F78
		private void ValidateType(object value)
		{
			if (value == null)
			{
				throw ADP.TablesAddNullAttempt("value");
			}
			if (!this.ItemType.IsInstanceOfType(value))
			{
				throw ADP.NotADataTableMapping(value);
			}
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00237BA8 File Offset: 0x00236FA8
		private void Validate(int index, DataTableMapping value)
		{
			if (value == null)
			{
				throw ADP.TablesAddNullAttempt("value");
			}
			if (value.Parent != null)
			{
				if (this != value.Parent)
				{
					throw ADP.TablesIsNotParent(this);
				}
				if (index != this.IndexOf(value))
				{
					throw ADP.TablesIsParent(this);
				}
			}
			string text = value.SourceTable;
			if (ADP.IsEmpty(text))
			{
				index = 1;
				do
				{
					text = "SourceTable" + index.ToString(CultureInfo.InvariantCulture);
					index++;
				}
				while (-1 != this.IndexOf(text));
				value.SourceTable = text;
				return;
			}
			this.ValidateSourceTable(index, text);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00237C38 File Offset: 0x00237038
		internal void ValidateSourceTable(int index, string value)
		{
			int num = this.IndexOf(value);
			if (-1 != num && index != num)
			{
				throw ADP.TablesUniqueSourceTable(value);
			}
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00237C68 File Offset: 0x00237068
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static DataTableMapping GetTableMappingBySchemaAction(DataTableMappingCollection tableMappings, string sourceTable, string dataSetTable, MissingMappingAction mappingAction)
		{
			if (tableMappings != null)
			{
				int num = tableMappings.IndexOf(sourceTable);
				if (-1 != num)
				{
					return tableMappings.items[num];
				}
			}
			if (ADP.IsEmpty(sourceTable))
			{
				throw ADP.InvalidSourceTable("sourceTable");
			}
			switch (mappingAction)
			{
			case MissingMappingAction.Passthrough:
				return new DataTableMapping(sourceTable, dataSetTable);
			case MissingMappingAction.Ignore:
				return null;
			case MissingMappingAction.Error:
				throw ADP.MissingTableMapping(sourceTable);
			default:
				throw ADP.InvalidMissingMappingAction(mappingAction);
			}
		}

		// Token: 0x04000BC1 RID: 3009
		private List<DataTableMapping> items;
	}
}
