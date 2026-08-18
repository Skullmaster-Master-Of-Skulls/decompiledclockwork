using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x020002D8 RID: 728
	public sealed class DataColumnMappingCollection : MarshalByRefObject, IColumnMappingCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06002D2C RID: 11564 RVA: 0x00123304 File Offset: 0x00122704
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06002D2D RID: 11565 RVA: 0x00123314 File Offset: 0x00122714
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06002D2E RID: 11566 RVA: 0x00123324 File Offset: 0x00122724
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06002D2F RID: 11567 RVA: 0x00123334 File Offset: 0x00122734
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700073B RID: 1851
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.ValidateType(value);
				this[index] = (DataColumnMapping)value;
			}
		}

		// Token: 0x1700073C RID: 1852
		object IColumnMappingCollection.this[string index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.ValidateType(value);
				this[index] = (DataColumnMapping)value;
			}
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x001233B4 File Offset: 0x001227B4
		IColumnMapping IColumnMappingCollection.Add(string sourceColumnName, string dataSetColumnName)
		{
			return this.Add(sourceColumnName, dataSetColumnName);
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x001233CC File Offset: 0x001227CC
		IColumnMapping IColumnMappingCollection.GetByDataSetColumn(string dataSetColumnName)
		{
			return this.GetByDataSetColumn(dataSetColumnName);
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002D36 RID: 11574 RVA: 0x001233E0 File Offset: 0x001227E0
		[Browsable(false)]
		[ResDescription("DataColumnMappings_Count")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002D37 RID: 11575 RVA: 0x00123404 File Offset: 0x00122804
		private Type ItemType
		{
			get
			{
				return typeof(DataColumnMapping);
			}
		}

		// Token: 0x1700073F RID: 1855
		[Browsable(false)]
		[ResDescription("DataColumnMappings_Item")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataColumnMapping this[int index]
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

		// Token: 0x17000740 RID: 1856
		[ResDescription("DataColumnMappings_Item")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataColumnMapping this[string sourceColumn]
		{
			get
			{
				int index = this.RangeCheck(sourceColumn);
				return this.items[index];
			}
			set
			{
				int index = this.RangeCheck(sourceColumn);
				this.Replace(index, value);
			}
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x0012349C File Offset: 0x0012289C
		public int Add(object value)
		{
			this.ValidateType(value);
			this.Add((DataColumnMapping)value);
			return this.Count - 1;
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x001234C8 File Offset: 0x001228C8
		private DataColumnMapping Add(DataColumnMapping value)
		{
			this.AddWithoutEvents(value);
			return value;
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x001234E0 File Offset: 0x001228E0
		public DataColumnMapping Add(string sourceColumn, string dataSetColumn)
		{
			return this.Add(new DataColumnMapping(sourceColumn, dataSetColumn));
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x001234FC File Offset: 0x001228FC
		public void AddRange(DataColumnMapping[] values)
		{
			this.AddEnumerableRange(values, false);
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x00123514 File Offset: 0x00122914
		public void AddRange(Array values)
		{
			this.AddEnumerableRange(values, false);
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x0012352C File Offset: 0x0012292C
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
						this.AddWithoutEvents(cloneable.Clone() as DataColumnMapping);
					}
					return;
				}
			}
			foreach (object obj2 in values)
			{
				DataColumnMapping value2 = (DataColumnMapping)obj2;
				this.AddWithoutEvents(value2);
			}
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x00123648 File Offset: 0x00122A48
		private void AddWithoutEvents(DataColumnMapping value)
		{
			this.Validate(-1, value);
			value.Parent = this;
			this.ArrayList().Add(value);
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x00123670 File Offset: 0x00122A70
		private List<DataColumnMapping> ArrayList()
		{
			if (this.items == null)
			{
				this.items = new List<DataColumnMapping>();
			}
			return this.items;
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x00123698 File Offset: 0x00122A98
		public void Clear()
		{
			if (0 < this.Count)
			{
				this.ClearWithoutEvents();
			}
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x001236B4 File Offset: 0x00122AB4
		private void ClearWithoutEvents()
		{
			if (this.items != null)
			{
				foreach (DataColumnMapping dataColumnMapping in this.items)
				{
					dataColumnMapping.Parent = null;
				}
				this.items.Clear();
			}
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x00123728 File Offset: 0x00122B28
		public bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x00123744 File Offset: 0x00122B44
		public bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x00123760 File Offset: 0x00122B60
		public void CopyTo(Array array, int index)
		{
			((ICollection)this.ArrayList()).CopyTo(array, index);
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x0012377C File Offset: 0x00122B7C
		public void CopyTo(DataColumnMapping[] array, int index)
		{
			this.ArrayList().CopyTo(array, index);
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x00123798 File Offset: 0x00122B98
		public DataColumnMapping GetByDataSetColumn(string value)
		{
			int num = this.IndexOfDataSetColumn(value);
			if (0 > num)
			{
				throw ADP.ColumnsDataSetColumn(value);
			}
			return this.items[num];
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x001237C4 File Offset: 0x00122BC4
		public IEnumerator GetEnumerator()
		{
			return this.ArrayList().GetEnumerator();
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x001237E4 File Offset: 0x00122BE4
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

		// Token: 0x06002D4D RID: 11597 RVA: 0x00123820 File Offset: 0x00122C20
		public int IndexOf(string sourceColumn)
		{
			if (!ADP.IsEmpty(sourceColumn))
			{
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					if (ADP.SrcCompare(sourceColumn, this.items[i].SourceColumn) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x00123864 File Offset: 0x00122C64
		public int IndexOfDataSetColumn(string dataSetColumn)
		{
			if (!ADP.IsEmpty(dataSetColumn))
			{
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					if (ADP.DstCompare(dataSetColumn, this.items[i].DataSetColumn) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x001238A8 File Offset: 0x00122CA8
		public void Insert(int index, object value)
		{
			this.ValidateType(value);
			this.Insert(index, (DataColumnMapping)value);
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x001238CC File Offset: 0x00122CCC
		public void Insert(int index, DataColumnMapping value)
		{
			if (value == null)
			{
				throw ADP.ColumnsAddNullAttempt("value");
			}
			this.Validate(-1, value);
			value.Parent = this;
			this.ArrayList().Insert(index, value);
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x00123904 File Offset: 0x00122D04
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw ADP.ColumnsIndexInt32(index, this);
			}
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x00123928 File Offset: 0x00122D28
		private int RangeCheck(string sourceColumn)
		{
			int num = this.IndexOf(sourceColumn);
			if (num < 0)
			{
				throw ADP.ColumnsIndexSource(sourceColumn);
			}
			return num;
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x0012394C File Offset: 0x00122D4C
		public void RemoveAt(int index)
		{
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x00123968 File Offset: 0x00122D68
		public void RemoveAt(string sourceColumn)
		{
			int index = this.RangeCheck(sourceColumn);
			this.RemoveIndex(index);
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x00123984 File Offset: 0x00122D84
		private void RemoveIndex(int index)
		{
			this.items[index].Parent = null;
			this.items.RemoveAt(index);
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x001239B0 File Offset: 0x00122DB0
		public void Remove(object value)
		{
			this.ValidateType(value);
			this.Remove((DataColumnMapping)value);
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x001239D0 File Offset: 0x00122DD0
		public void Remove(DataColumnMapping value)
		{
			if (value == null)
			{
				throw ADP.ColumnsAddNullAttempt("value");
			}
			int num = this.IndexOf(value);
			if (-1 != num)
			{
				this.RemoveIndex(num);
				return;
			}
			throw ADP.CollectionRemoveInvalidObject(this.ItemType, this);
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x00123A0C File Offset: 0x00122E0C
		private void Replace(int index, DataColumnMapping newValue)
		{
			this.Validate(index, newValue);
			this.items[index].Parent = null;
			newValue.Parent = this;
			this.items[index] = newValue;
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x00123A48 File Offset: 0x00122E48
		private void ValidateType(object value)
		{
			if (value == null)
			{
				throw ADP.ColumnsAddNullAttempt("value");
			}
			if (!this.ItemType.IsInstanceOfType(value))
			{
				throw ADP.NotADataColumnMapping(value);
			}
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x00123A78 File Offset: 0x00122E78
		private void Validate(int index, DataColumnMapping value)
		{
			if (value == null)
			{
				throw ADP.ColumnsAddNullAttempt("value");
			}
			if (value.Parent != null)
			{
				if (this != value.Parent)
				{
					throw ADP.ColumnsIsNotParent(this);
				}
				if (index != this.IndexOf(value))
				{
					throw ADP.ColumnsIsParent(this);
				}
			}
			string text = value.SourceColumn;
			if (ADP.IsEmpty(text))
			{
				index = 1;
				do
				{
					text = "SourceColumn" + index.ToString(CultureInfo.InvariantCulture);
					index++;
				}
				while (-1 != this.IndexOf(text));
				value.SourceColumn = text;
				return;
			}
			this.ValidateSourceColumn(index, text);
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x00123B04 File Offset: 0x00122F04
		internal void ValidateSourceColumn(int index, string value)
		{
			int num = this.IndexOf(value);
			if (-1 != num && index != num)
			{
				throw ADP.ColumnsUniqueSourceColumn(value);
			}
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x00123B28 File Offset: 0x00122F28
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static DataColumn GetDataColumn(DataColumnMappingCollection columnMappings, string sourceColumn, Type dataType, DataTable dataTable, MissingMappingAction mappingAction, MissingSchemaAction schemaAction)
		{
			if (columnMappings != null)
			{
				int num = columnMappings.IndexOf(sourceColumn);
				if (-1 != num)
				{
					return columnMappings.items[num].GetDataColumnBySchemaAction(dataTable, dataType, schemaAction);
				}
			}
			if (ADP.IsEmpty(sourceColumn))
			{
				throw ADP.InvalidSourceColumn("sourceColumn");
			}
			switch (mappingAction)
			{
			case MissingMappingAction.Passthrough:
				return DataColumnMapping.GetDataColumnBySchemaAction(sourceColumn, sourceColumn, dataTable, dataType, schemaAction);
			case MissingMappingAction.Ignore:
				return null;
			case MissingMappingAction.Error:
				throw ADP.MissingColumnMapping(sourceColumn);
			default:
				throw ADP.InvalidMissingMappingAction(mappingAction);
			}
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x00123BA0 File Offset: 0x00122FA0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static DataColumnMapping GetColumnMappingBySchemaAction(DataColumnMappingCollection columnMappings, string sourceColumn, MissingMappingAction mappingAction)
		{
			if (columnMappings != null)
			{
				int num = columnMappings.IndexOf(sourceColumn);
				if (-1 != num)
				{
					return columnMappings.items[num];
				}
			}
			if (ADP.IsEmpty(sourceColumn))
			{
				throw ADP.InvalidSourceColumn("sourceColumn");
			}
			switch (mappingAction)
			{
			case MissingMappingAction.Passthrough:
				return new DataColumnMapping(sourceColumn, sourceColumn);
			case MissingMappingAction.Ignore:
				return null;
			case MissingMappingAction.Error:
				throw ADP.MissingColumnMapping(sourceColumn);
			default:
				throw ADP.InvalidMissingMappingAction(mappingAction);
			}
		}

		// Token: 0x04001C40 RID: 7232
		private List<DataColumnMapping> items;
	}
}
