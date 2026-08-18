using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x0200011B RID: 283
	public sealed class DataColumnMappingCollection : MarshalByRefObject, IColumnMappingCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00235C28 File Offset: 0x00235028
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x00235C38 File Offset: 0x00235038
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00235C48 File Offset: 0x00235048
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x00235C58 File Offset: 0x00235058
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000257 RID: 599
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

		// Token: 0x17000258 RID: 600
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

		// Token: 0x060011F5 RID: 4597 RVA: 0x00235D08 File Offset: 0x00235108
		IColumnMapping IColumnMappingCollection.Add(string sourceColumnName, string dataSetColumnName)
		{
			return this.Add(sourceColumnName, dataSetColumnName);
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00235D28 File Offset: 0x00235128
		IColumnMapping IColumnMappingCollection.GetByDataSetColumn(string dataSetColumnName)
		{
			return this.GetByDataSetColumn(dataSetColumnName);
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00235D48 File Offset: 0x00235148
		[ResDescription("DataColumnMappings_Count")]
		[Browsable(false)]
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

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x00235D78 File Offset: 0x00235178
		private Type ItemType
		{
			get
			{
				return typeof(DataColumnMapping);
			}
		}

		// Token: 0x1700025B RID: 603
		[ResDescription("DataColumnMappings_Item")]
		[Browsable(false)]
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

		// Token: 0x1700025C RID: 604
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

		// Token: 0x060011FD RID: 4605 RVA: 0x00235E28 File Offset: 0x00235228
		public int Add(object value)
		{
			this.ValidateType(value);
			this.Add((DataColumnMapping)value);
			return this.Count - 1;
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00235E58 File Offset: 0x00235258
		private DataColumnMapping Add(DataColumnMapping value)
		{
			this.AddWithoutEvents(value);
			return value;
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00235E78 File Offset: 0x00235278
		public DataColumnMapping Add(string sourceColumn, string dataSetColumn)
		{
			return this.Add(new DataColumnMapping(sourceColumn, dataSetColumn));
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00235E98 File Offset: 0x00235298
		public void AddRange(DataColumnMapping[] values)
		{
			this.AddEnumerableRange(values, false);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00235EB8 File Offset: 0x002352B8
		public void AddRange(Array values)
		{
			this.AddEnumerableRange(values, false);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00235ED8 File Offset: 0x002352D8
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

		// Token: 0x06001203 RID: 4611 RVA: 0x00235FF8 File Offset: 0x002353F8
		private void AddWithoutEvents(DataColumnMapping value)
		{
			this.Validate(-1, value);
			value.Parent = this;
			this.ArrayList().Add(value);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00236028 File Offset: 0x00235428
		private List<DataColumnMapping> ArrayList()
		{
			if (this.items == null)
			{
				this.items = new List<DataColumnMapping>();
			}
			return this.items;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00236058 File Offset: 0x00235458
		public void Clear()
		{
			if (0 < this.Count)
			{
				this.ClearWithoutEvents();
			}
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00236078 File Offset: 0x00235478
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

		// Token: 0x06001207 RID: 4615 RVA: 0x002360F8 File Offset: 0x002354F8
		public bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00236118 File Offset: 0x00235518
		public bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00236138 File Offset: 0x00235538
		public void CopyTo(Array array, int index)
		{
			((ICollection)this.ArrayList()).CopyTo(array, index);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00236158 File Offset: 0x00235558
		public void CopyTo(DataColumnMapping[] array, int index)
		{
			this.ArrayList().CopyTo(array, index);
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00236178 File Offset: 0x00235578
		public DataColumnMapping GetByDataSetColumn(string value)
		{
			int num = this.IndexOfDataSetColumn(value);
			if (0 > num)
			{
				throw ADP.ColumnsDataSetColumn(value);
			}
			return this.items[num];
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x002361A8 File Offset: 0x002355A8
		public IEnumerator GetEnumerator()
		{
			return this.ArrayList().GetEnumerator();
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x002361C8 File Offset: 0x002355C8
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

		// Token: 0x0600120E RID: 4622 RVA: 0x00236208 File Offset: 0x00235608
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

		// Token: 0x0600120F RID: 4623 RVA: 0x00236258 File Offset: 0x00235658
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

		// Token: 0x06001210 RID: 4624 RVA: 0x002362A8 File Offset: 0x002356A8
		public void Insert(int index, object value)
		{
			this.ValidateType(value);
			this.Insert(index, (DataColumnMapping)value);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x002362D8 File Offset: 0x002356D8
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

		// Token: 0x06001212 RID: 4626 RVA: 0x00236318 File Offset: 0x00235718
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw ADP.ColumnsIndexInt32(index, this);
			}
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00236348 File Offset: 0x00235748
		private int RangeCheck(string sourceColumn)
		{
			int num = this.IndexOf(sourceColumn);
			if (num < 0)
			{
				throw ADP.ColumnsIndexSource(sourceColumn);
			}
			return num;
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00236378 File Offset: 0x00235778
		public void RemoveAt(int index)
		{
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00236398 File Offset: 0x00235798
		public void RemoveAt(string sourceColumn)
		{
			int index = this.RangeCheck(sourceColumn);
			this.RemoveIndex(index);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x002363B8 File Offset: 0x002357B8
		private void RemoveIndex(int index)
		{
			this.items[index].Parent = null;
			this.items.RemoveAt(index);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x002363E8 File Offset: 0x002357E8
		public void Remove(object value)
		{
			this.ValidateType(value);
			this.Remove((DataColumnMapping)value);
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00236408 File Offset: 0x00235808
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

		// Token: 0x06001219 RID: 4633 RVA: 0x00236448 File Offset: 0x00235848
		private void Replace(int index, DataColumnMapping newValue)
		{
			this.Validate(index, newValue);
			this.items[index].Parent = null;
			newValue.Parent = this;
			this.items[index] = newValue;
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00236488 File Offset: 0x00235888
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

		// Token: 0x0600121B RID: 4635 RVA: 0x002364B8 File Offset: 0x002358B8
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

		// Token: 0x0600121C RID: 4636 RVA: 0x00236548 File Offset: 0x00235948
		internal void ValidateSourceColumn(int index, string value)
		{
			int num = this.IndexOf(value);
			if (-1 != num && index != num)
			{
				throw ADP.ColumnsUniqueSourceColumn(value);
			}
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00236578 File Offset: 0x00235978
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

		// Token: 0x0600121E RID: 4638 RVA: 0x002365F8 File Offset: 0x002359F8
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

		// Token: 0x04000B8C RID: 2956
		private List<DataColumnMapping> items;
	}
}
