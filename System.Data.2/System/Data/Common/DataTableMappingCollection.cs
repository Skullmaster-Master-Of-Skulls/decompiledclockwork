using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x020002DC RID: 732
	[Editor("Microsoft.VSDesigner.Data.Design.DataTableMappingCollectionEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ListBindable(false)]
	public sealed class DataTableMappingCollection : MarshalByRefObject, ITableMappingCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002D9F RID: 11679 RVA: 0x00124998 File Offset: 0x00123D98
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06002DA0 RID: 11680 RVA: 0x001249A8 File Offset: 0x00123DA8
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06002DA1 RID: 11681 RVA: 0x001249B8 File Offset: 0x00123DB8
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002DA2 RID: 11682 RVA: 0x001249C8 File Offset: 0x00123DC8
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700074C RID: 1868
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

		// Token: 0x1700074D RID: 1869
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

		// Token: 0x06002DA7 RID: 11687 RVA: 0x00124A48 File Offset: 0x00123E48
		ITableMapping ITableMappingCollection.Add(string sourceTableName, string dataSetTableName)
		{
			return this.Add(sourceTableName, dataSetTableName);
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x00124A60 File Offset: 0x00123E60
		ITableMapping ITableMappingCollection.GetByDataSetTable(string dataSetTableName)
		{
			return this.GetByDataSetTable(dataSetTableName);
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x00124A74 File Offset: 0x00123E74
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

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002DAA RID: 11690 RVA: 0x00124A98 File Offset: 0x00123E98
		private Type ItemType
		{
			get
			{
				return typeof(DataTableMapping);
			}
		}

		// Token: 0x17000750 RID: 1872
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[ResDescription("DataTableMappings_Item")]
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

		// Token: 0x17000751 RID: 1873
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

		// Token: 0x06002DAF RID: 11695 RVA: 0x00124B30 File Offset: 0x00123F30
		public int Add(object value)
		{
			this.ValidateType(value);
			this.Add((DataTableMapping)value);
			return this.Count - 1;
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x00124B5C File Offset: 0x00123F5C
		private DataTableMapping Add(DataTableMapping value)
		{
			this.AddWithoutEvents(value);
			return value;
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x00124B74 File Offset: 0x00123F74
		public void AddRange(DataTableMapping[] values)
		{
			this.AddEnumerableRange(values, false);
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x00124B8C File Offset: 0x00123F8C
		public void AddRange(Array values)
		{
			this.AddEnumerableRange(values, false);
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x00124BA4 File Offset: 0x00123FA4
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

		// Token: 0x06002DB4 RID: 11700 RVA: 0x00124CC0 File Offset: 0x001240C0
		public DataTableMapping Add(string sourceTable, string dataSetTable)
		{
			return this.Add(new DataTableMapping(sourceTable, dataSetTable));
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x00124CDC File Offset: 0x001240DC
		private void AddWithoutEvents(DataTableMapping value)
		{
			this.Validate(-1, value);
			value.Parent = this;
			this.ArrayList().Add(value);
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x00124D04 File Offset: 0x00124104
		private List<DataTableMapping> ArrayList()
		{
			if (this.items == null)
			{
				this.items = new List<DataTableMapping>();
			}
			return this.items;
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x00124D2C File Offset: 0x0012412C
		public void Clear()
		{
			if (0 < this.Count)
			{
				this.ClearWithoutEvents();
			}
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x00124D48 File Offset: 0x00124148
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

		// Token: 0x06002DB9 RID: 11705 RVA: 0x00124DBC File Offset: 0x001241BC
		public bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x00124DD8 File Offset: 0x001241D8
		public bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x00124DF4 File Offset: 0x001241F4
		public void CopyTo(Array array, int index)
		{
			((ICollection)this.ArrayList()).CopyTo(array, index);
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x00124E10 File Offset: 0x00124210
		public void CopyTo(DataTableMapping[] array, int index)
		{
			this.ArrayList().CopyTo(array, index);
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x00124E2C File Offset: 0x0012422C
		public DataTableMapping GetByDataSetTable(string dataSetTable)
		{
			int num = this.IndexOfDataSetTable(dataSetTable);
			if (0 > num)
			{
				throw ADP.TablesDataSetTable(dataSetTable);
			}
			return this.items[num];
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x00124E58 File Offset: 0x00124258
		public IEnumerator GetEnumerator()
		{
			return this.ArrayList().GetEnumerator();
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x00124E78 File Offset: 0x00124278
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

		// Token: 0x06002DC0 RID: 11712 RVA: 0x00124EB4 File Offset: 0x001242B4
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

		// Token: 0x06002DC1 RID: 11713 RVA: 0x00124EFC File Offset: 0x001242FC
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

		// Token: 0x06002DC2 RID: 11714 RVA: 0x00124F44 File Offset: 0x00124344
		public void Insert(int index, object value)
		{
			this.ValidateType(value);
			this.Insert(index, (DataTableMapping)value);
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x00124F68 File Offset: 0x00124368
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

		// Token: 0x06002DC4 RID: 11716 RVA: 0x00124FA0 File Offset: 0x001243A0
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw ADP.TablesIndexInt32(index, this);
			}
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x00124FC4 File Offset: 0x001243C4
		private int RangeCheck(string sourceTable)
		{
			int num = this.IndexOf(sourceTable);
			if (num < 0)
			{
				throw ADP.TablesSourceIndex(sourceTable);
			}
			return num;
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x00124FE8 File Offset: 0x001243E8
		public void RemoveAt(int index)
		{
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x00125004 File Offset: 0x00124404
		public void RemoveAt(string sourceTable)
		{
			int index = this.RangeCheck(sourceTable);
			this.RemoveIndex(index);
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x00125020 File Offset: 0x00124420
		private void RemoveIndex(int index)
		{
			this.items[index].Parent = null;
			this.items.RemoveAt(index);
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x0012504C File Offset: 0x0012444C
		public void Remove(object value)
		{
			this.ValidateType(value);
			this.Remove((DataTableMapping)value);
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x0012506C File Offset: 0x0012446C
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

		// Token: 0x06002DCB RID: 11723 RVA: 0x001250A8 File Offset: 0x001244A8
		private void Replace(int index, DataTableMapping newValue)
		{
			this.Validate(index, newValue);
			this.items[index].Parent = null;
			newValue.Parent = this;
			this.items[index] = newValue;
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x001250E4 File Offset: 0x001244E4
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

		// Token: 0x06002DCD RID: 11725 RVA: 0x00125114 File Offset: 0x00124514
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

		// Token: 0x06002DCE RID: 11726 RVA: 0x001251A0 File Offset: 0x001245A0
		internal void ValidateSourceTable(int index, string value)
		{
			int num = this.IndexOf(value);
			if (-1 != num && index != num)
			{
				throw ADP.TablesUniqueSourceTable(value);
			}
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x001251C4 File Offset: 0x001245C4
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

		// Token: 0x04001C7D RID: 7293
		private List<DataTableMapping> items;
	}
}
