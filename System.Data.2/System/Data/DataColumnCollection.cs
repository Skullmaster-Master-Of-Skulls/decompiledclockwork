using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000AA RID: 170
	[DefaultEvent("CollectionChanged")]
	[Editor("Microsoft.VSDesigner.Data.Design.ColumnsCollectionEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class DataColumnCollection : InternalDataCollectionBase
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x0005AB18 File Offset: 0x00059F18
		internal DataColumnCollection(DataTable table)
		{
			this.table = table;
			this.columnFromName = new Dictionary<string, DataColumn>();
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x0005AB5C File Offset: 0x00059F5C
		protected override ArrayList List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0005AB70 File Offset: 0x00059F70
		internal DataColumn[] ColumnsImplementingIChangeTracking
		{
			get
			{
				return this.columnsImplementingIChangeTracking;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x0005AB84 File Offset: 0x00059F84
		internal int ColumnsImplementingIChangeTrackingCount
		{
			get
			{
				return this.nColumnsImplementingIChangeTracking;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0005AB98 File Offset: 0x00059F98
		internal int ColumnsImplementingIRevertibleChangeTrackingCount
		{
			get
			{
				return this.nColumnsImplementingIRevertibleChangeTracking;
			}
		}

		// Token: 0x17000165 RID: 357
		public DataColumn this[int index]
		{
			get
			{
				DataColumn result;
				try
				{
					result = (DataColumn)this._list[index];
				}
				catch (ArgumentOutOfRangeException)
				{
					throw ExceptionBuilder.ColumnOutOfRange(index);
				}
				return result;
			}
		}

		// Token: 0x17000166 RID: 358
		public DataColumn this[string name]
		{
			get
			{
				if (name == null)
				{
					throw ExceptionBuilder.ArgumentNull("name");
				}
				DataColumn dataColumn;
				if (!this.columnFromName.TryGetValue(name, out dataColumn) || dataColumn == null)
				{
					int num = this.IndexOfCaseInsensitive(name);
					if (0 <= num)
					{
						dataColumn = (DataColumn)this._list[num];
					}
					else if (-2 == num)
					{
						throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
					}
				}
				return dataColumn;
			}
		}

		// Token: 0x17000167 RID: 359
		internal DataColumn this[string name, string ns]
		{
			get
			{
				DataColumn dataColumn;
				if (this.columnFromName.TryGetValue(name, out dataColumn) && dataColumn != null && dataColumn.Namespace == ns)
				{
					return dataColumn;
				}
				return null;
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0005AC84 File Offset: 0x0005A084
		internal void EnsureAdditionalCapacity(int capacity)
		{
			if (this._list.Capacity < capacity + this._list.Count)
			{
				this._list.Capacity = capacity + this._list.Count;
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0005ACC4 File Offset: 0x0005A0C4
		public void Add(DataColumn column)
		{
			this.AddAt(-1, column);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0005ACDC File Offset: 0x0005A0DC
		internal void AddAt(int index, DataColumn column)
		{
			if (column != null && column.ColumnMapping == MappingType.SimpleContent)
			{
				if (this.table.XmlText != null && this.table.XmlText != column)
				{
					throw ExceptionBuilder.CannotAddColumn3();
				}
				if (this.table.ElementColumnCount > 0)
				{
					throw ExceptionBuilder.CannotAddColumn4(column.ColumnName);
				}
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
				this.BaseAdd(column);
				if (index != -1)
				{
					this.ArrayAdd(index, column);
				}
				else
				{
					this.ArrayAdd(column);
				}
				this.table.XmlText = column;
			}
			else
			{
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
				this.BaseAdd(column);
				if (index != -1)
				{
					this.ArrayAdd(index, column);
				}
				else
				{
					this.ArrayAdd(column);
				}
				if (column.ColumnMapping == MappingType.Element)
				{
					DataTable dataTable = this.table;
					int elementColumnCount = dataTable.ElementColumnCount;
					dataTable.ElementColumnCount = elementColumnCount + 1;
				}
			}
			if (!this.table.fInitInProgress && column != null && column.Computed)
			{
				column.Expression = column.Expression;
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0005ADE4 File Offset: 0x0005A1E4
		public void AddRange(DataColumn[] columns)
		{
			if (this.table.fInitInProgress)
			{
				this.delayedAddRangeColumns = columns;
				return;
			}
			if (columns != null)
			{
				foreach (DataColumn dataColumn in columns)
				{
					if (dataColumn != null)
					{
						this.Add(dataColumn);
					}
				}
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0005AE28 File Offset: 0x0005A228
		public DataColumn Add(string columnName, Type type, string expression)
		{
			DataColumn dataColumn = new DataColumn(columnName, type, expression);
			this.Add(dataColumn);
			return dataColumn;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0005AE48 File Offset: 0x0005A248
		public DataColumn Add(string columnName, Type type)
		{
			DataColumn dataColumn = new DataColumn(columnName, type);
			this.Add(dataColumn);
			return dataColumn;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0005AE68 File Offset: 0x0005A268
		public DataColumn Add(string columnName)
		{
			DataColumn dataColumn = new DataColumn(columnName);
			this.Add(dataColumn);
			return dataColumn;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0005AE84 File Offset: 0x0005A284
		public DataColumn Add()
		{
			DataColumn dataColumn = new DataColumn();
			this.Add(dataColumn);
			return dataColumn;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060008F1 RID: 2289 RVA: 0x0005AEA0 File Offset: 0x0005A2A0
		// (remove) Token: 0x060008F2 RID: 2290 RVA: 0x0005AEC4 File Offset: 0x0005A2C4
		[ResDescription("collectionChangedEventDescr")]
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				this.onCollectionChangedDelegate = (CollectionChangeEventHandler)Delegate.Combine(this.onCollectionChangedDelegate, value);
			}
			remove
			{
				this.onCollectionChangedDelegate = (CollectionChangeEventHandler)Delegate.Remove(this.onCollectionChangedDelegate, value);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060008F3 RID: 2291 RVA: 0x0005AEE8 File Offset: 0x0005A2E8
		// (remove) Token: 0x060008F4 RID: 2292 RVA: 0x0005AF0C File Offset: 0x0005A30C
		internal event CollectionChangeEventHandler CollectionChanging
		{
			add
			{
				this.onCollectionChangingDelegate = (CollectionChangeEventHandler)Delegate.Combine(this.onCollectionChangingDelegate, value);
			}
			remove
			{
				this.onCollectionChangingDelegate = (CollectionChangeEventHandler)Delegate.Remove(this.onCollectionChangingDelegate, value);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060008F5 RID: 2293 RVA: 0x0005AF30 File Offset: 0x0005A330
		// (remove) Token: 0x060008F6 RID: 2294 RVA: 0x0005AF54 File Offset: 0x0005A354
		internal event CollectionChangeEventHandler ColumnPropertyChanged
		{
			add
			{
				this.onColumnPropertyChangedDelegate = (CollectionChangeEventHandler)Delegate.Combine(this.onColumnPropertyChangedDelegate, value);
			}
			remove
			{
				this.onColumnPropertyChangedDelegate = (CollectionChangeEventHandler)Delegate.Remove(this.onColumnPropertyChangedDelegate, value);
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0005AF78 File Offset: 0x0005A378
		private void ArrayAdd(DataColumn column)
		{
			this._list.Add(column);
			column.SetOrdinalInternal(this._list.Count - 1);
			this.CheckIChangeTracking(column);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0005AFAC File Offset: 0x0005A3AC
		private void ArrayAdd(int index, DataColumn column)
		{
			this._list.Insert(index, column);
			this.CheckIChangeTracking(column);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0005AFD0 File Offset: 0x0005A3D0
		private void ArrayRemove(DataColumn column)
		{
			column.SetOrdinalInternal(-1);
			this._list.Remove(column);
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				((DataColumn)this._list[i]).SetOrdinalInternal(i);
			}
			if (column.ImplementsIChangeTracking)
			{
				this.RemoveColumnsImplementingIChangeTrackingList(column);
			}
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0005B030 File Offset: 0x0005A430
		internal string AssignName()
		{
			int num = this.defaultNameIndex;
			this.defaultNameIndex = num + 1;
			string text = this.MakeName(num);
			while (this.columnFromName.ContainsKey(text))
			{
				num = this.defaultNameIndex;
				this.defaultNameIndex = num + 1;
				text = this.MakeName(num);
			}
			return text;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0005B080 File Offset: 0x0005A480
		private void BaseAdd(DataColumn column)
		{
			if (column == null)
			{
				throw ExceptionBuilder.ArgumentNull("column");
			}
			if (column.table == this.table)
			{
				throw ExceptionBuilder.CannotAddColumn1(column.ColumnName);
			}
			if (column.table != null)
			{
				throw ExceptionBuilder.CannotAddColumn2(column.ColumnName);
			}
			if (column.ColumnName.Length == 0)
			{
				column.ColumnName = this.AssignName();
			}
			this.RegisterColumnName(column.ColumnName, column);
			try
			{
				column.SetTable(this.table);
				if (!this.table.fInitInProgress && column.Computed && column.DataExpression.DependsOn(column))
				{
					throw ExceptionBuilder.ExpressionCircular();
				}
				if (0 < this.table.RecordCapacity)
				{
					column.SetCapacity(this.table.RecordCapacity);
				}
				for (int i = 0; i < this.table.RecordCapacity; i++)
				{
					column.InitializeRecord(i);
				}
				if (this.table.DataSet != null)
				{
					column.OnSetDataSet();
				}
			}
			catch (Exception e)
			{
				if (ADP.IsCatchableOrSecurityExceptionType(e))
				{
					this.UnregisterName(column.ColumnName);
				}
				throw;
			}
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0005B1AC File Offset: 0x0005A5AC
		private void BaseGroupSwitch(DataColumn[] oldArray, int oldLength, DataColumn[] newArray, int newLength)
		{
			int num = 0;
			for (int i = 0; i < oldLength; i++)
			{
				bool flag = false;
				for (int j = num; j < newLength; j++)
				{
					if (oldArray[i] == newArray[j])
					{
						if (num == j)
						{
							num++;
						}
						flag = true;
						break;
					}
				}
				if (!flag && oldArray[i].Table == this.table)
				{
					this.BaseRemove(oldArray[i]);
					this._list.Remove(oldArray[i]);
					oldArray[i].SetOrdinalInternal(-1);
				}
			}
			for (int k = 0; k < newLength; k++)
			{
				if (newArray[k].Table != this.table)
				{
					this.BaseAdd(newArray[k]);
					this._list.Add(newArray[k]);
				}
				newArray[k].SetOrdinalInternal(k);
			}
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0005B260 File Offset: 0x0005A660
		private void BaseRemove(DataColumn column)
		{
			if (this.CanRemove(column, true))
			{
				if (column.errors > 0)
				{
					for (int i = 0; i < this.table.Rows.Count; i++)
					{
						this.table.Rows[i].ClearError(column);
					}
				}
				this.UnregisterName(column.ColumnName);
				column.SetTable(null);
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0005B2C8 File Offset: 0x0005A6C8
		public bool CanRemove(DataColumn column)
		{
			return this.CanRemove(column, false);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0005B2E0 File Offset: 0x0005A6E0
		internal bool CanRemove(DataColumn column, bool fThrowException)
		{
			if (column == null)
			{
				if (!fThrowException)
				{
					return false;
				}
				throw ExceptionBuilder.ArgumentNull("column");
			}
			else if (column.table != this.table)
			{
				if (!fThrowException)
				{
					return false;
				}
				throw ExceptionBuilder.CannotRemoveColumn();
			}
			else
			{
				this.table.OnRemoveColumnInternal(column);
				if (this.table.primaryKey == null || !this.table.primaryKey.Key.ContainsColumn(column))
				{
					int i = 0;
					while (i < this.table.ParentRelations.Count)
					{
						if (this.table.ParentRelations[i].ChildKey.ContainsColumn(column))
						{
							if (!fThrowException)
							{
								return false;
							}
							throw ExceptionBuilder.CannotRemoveChildKey(this.table.ParentRelations[i].RelationName);
						}
						else
						{
							i++;
						}
					}
					int j = 0;
					while (j < this.table.ChildRelations.Count)
					{
						if (this.table.ChildRelations[j].ParentKey.ContainsColumn(column))
						{
							if (!fThrowException)
							{
								return false;
							}
							throw ExceptionBuilder.CannotRemoveChildKey(this.table.ChildRelations[j].RelationName);
						}
						else
						{
							j++;
						}
					}
					int k = 0;
					while (k < this.table.Constraints.Count)
					{
						if (this.table.Constraints[k].ContainsColumn(column))
						{
							if (!fThrowException)
							{
								return false;
							}
							throw ExceptionBuilder.CannotRemoveConstraint(this.table.Constraints[k].ConstraintName, this.table.Constraints[k].Table.TableName);
						}
						else
						{
							k++;
						}
					}
					if (this.table.DataSet != null)
					{
						ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this.table.DataSet, this.table);
						while (parentForeignKeyConstraintEnumerator.GetNext())
						{
							Constraint constraint = parentForeignKeyConstraintEnumerator.GetConstraint();
							if (((ForeignKeyConstraint)constraint).ParentKey.ContainsColumn(column))
							{
								if (!fThrowException)
								{
									return false;
								}
								throw ExceptionBuilder.CannotRemoveConstraint(constraint.ConstraintName, constraint.Table.TableName);
							}
						}
					}
					if (column.dependentColumns != null)
					{
						for (int l = 0; l < column.dependentColumns.Count; l++)
						{
							DataColumn dataColumn = column.dependentColumns[l];
							if ((!this.fInClear || (dataColumn.Table != this.table && dataColumn.Table != null)) && dataColumn.Table != null)
							{
								DataExpression dataExpression = dataColumn.DataExpression;
								if (dataExpression != null && dataExpression.DependsOn(column))
								{
									if (!fThrowException)
									{
										return false;
									}
									throw ExceptionBuilder.CannotRemoveExpression(dataColumn.ColumnName, dataColumn.Expression);
								}
							}
						}
					}
					foreach (Index index in this.table.LiveIndexes)
					{
					}
					return true;
				}
				if (!fThrowException)
				{
					return false;
				}
				throw ExceptionBuilder.CannotRemovePrimaryKey();
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0005B5D0 File Offset: 0x0005A9D0
		private void CheckIChangeTracking(DataColumn column)
		{
			if (column.ImplementsIRevertibleChangeTracking)
			{
				this.nColumnsImplementingIRevertibleChangeTracking++;
				this.nColumnsImplementingIChangeTracking++;
				this.AddColumnsImplementingIChangeTrackingList(column);
				return;
			}
			if (column.ImplementsIChangeTracking)
			{
				this.nColumnsImplementingIChangeTracking++;
				this.AddColumnsImplementingIChangeTrackingList(column);
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0005B628 File Offset: 0x0005AA28
		public void Clear()
		{
			int count = this._list.Count;
			DataColumn[] array = new DataColumn[this._list.Count];
			this._list.CopyTo(array, 0);
			this.OnCollectionChanging(InternalDataCollectionBase.RefreshEventArgs);
			if (this.table.fInitInProgress && this.delayedAddRangeColumns != null)
			{
				this.delayedAddRangeColumns = null;
			}
			try
			{
				this.fInClear = true;
				this.BaseGroupSwitch(array, count, null, 0);
				this.fInClear = false;
			}
			catch (Exception e)
			{
				if (ADP.IsCatchableOrSecurityExceptionType(e))
				{
					this.fInClear = false;
					this.BaseGroupSwitch(null, 0, array, count);
					this._list.Clear();
					for (int i = 0; i < count; i++)
					{
						this._list.Add(array[i]);
					}
				}
				throw;
			}
			this._list.Clear();
			this.table.ElementColumnCount = 0;
			this.OnCollectionChanged(InternalDataCollectionBase.RefreshEventArgs);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0005B724 File Offset: 0x0005AB24
		public bool Contains(string name)
		{
			DataColumn dataColumn;
			return (this.columnFromName.TryGetValue(name, out dataColumn) && dataColumn != null) || this.IndexOfCaseInsensitive(name) >= 0;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0005B754 File Offset: 0x0005AB54
		internal bool Contains(string name, bool caseSensitive)
		{
			DataColumn dataColumn;
			return (this.columnFromName.TryGetValue(name, out dataColumn) && dataColumn != null) || (!caseSensitive && this.IndexOfCaseInsensitive(name) >= 0);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0005B788 File Offset: 0x0005AB88
		public void CopyTo(DataColumn[] array, int index)
		{
			if (array == null)
			{
				throw ExceptionBuilder.ArgumentNull("array");
			}
			if (index < 0)
			{
				throw ExceptionBuilder.ArgumentOutOfRange("index");
			}
			if (array.Length - index < this._list.Count)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
			for (int i = 0; i < this._list.Count; i++)
			{
				array[index + i] = (DataColumn)this._list[i];
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0005B7F8 File Offset: 0x0005ABF8
		public int IndexOf(DataColumn column)
		{
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				if (column == (DataColumn)this._list[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0005B834 File Offset: 0x0005AC34
		public int IndexOf(string columnName)
		{
			if (columnName != null && 0 < columnName.Length)
			{
				int count = this.Count;
				DataColumn dataColumn;
				if (this.columnFromName.TryGetValue(columnName, out dataColumn) && dataColumn != null)
				{
					for (int i = 0; i < count; i++)
					{
						if (dataColumn == this._list[i])
						{
							return i;
						}
					}
				}
				else
				{
					int num = this.IndexOfCaseInsensitive(columnName);
					if (num >= 0)
					{
						return num;
					}
					return -1;
				}
			}
			return -1;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0005B898 File Offset: 0x0005AC98
		internal int IndexOfCaseInsensitive(string name)
		{
			int specialHashCode = this.table.GetSpecialHashCode(name);
			int num = -1;
			for (int i = 0; i < this.Count; i++)
			{
				DataColumn dataColumn = (DataColumn)this._list[i];
				if ((specialHashCode == 0 || dataColumn._hashCode == 0 || dataColumn._hashCode == specialHashCode) && base.NamesEqual(dataColumn.ColumnName, name, false, this.table.Locale) != 0)
				{
					if (num != -1)
					{
						return -2;
					}
					num = i;
				}
			}
			return num;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0005B914 File Offset: 0x0005AD14
		internal void FinishInitCollection()
		{
			if (this.delayedAddRangeColumns != null)
			{
				foreach (DataColumn dataColumn in this.delayedAddRangeColumns)
				{
					if (dataColumn != null)
					{
						this.Add(dataColumn);
					}
				}
				foreach (DataColumn dataColumn2 in this.delayedAddRangeColumns)
				{
					if (dataColumn2 != null)
					{
						dataColumn2.FinishInitInProgress();
					}
				}
				this.delayedAddRangeColumns = null;
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0005B97C File Offset: 0x0005AD7C
		private string MakeName(int index)
		{
			if (1 == index)
			{
				return "Column1";
			}
			return "Column" + index.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0005B9AC File Offset: 0x0005ADAC
		internal void MoveTo(DataColumn column, int newPosition)
		{
			if (0 > newPosition || newPosition > this.Count - 1)
			{
				throw ExceptionBuilder.InvalidOrdinal("ordinal", newPosition);
			}
			if (column.ImplementsIChangeTracking)
			{
				this.RemoveColumnsImplementingIChangeTrackingList(column);
			}
			this._list.Remove(column);
			this._list.Insert(newPosition, column);
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				((DataColumn)this._list[i]).SetOrdinalInternal(i);
			}
			this.CheckIChangeTracking(column);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, column));
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0005BA40 File Offset: 0x0005AE40
		private void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			this.table.UpdatePropertyDescriptorCollectionCache();
			if (ccevent != null && !this.table.SchemaLoading && !this.table.fInitInProgress)
			{
				DataColumn dataColumn = (DataColumn)ccevent.Element;
			}
			if (this.onCollectionChangedDelegate != null)
			{
				this.onCollectionChangedDelegate(this, ccevent);
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0005BA98 File Offset: 0x0005AE98
		private void OnCollectionChanging(CollectionChangeEventArgs ccevent)
		{
			if (this.onCollectionChangingDelegate != null)
			{
				this.onCollectionChangingDelegate(this, ccevent);
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0005BABC File Offset: 0x0005AEBC
		internal void OnColumnPropertyChanged(CollectionChangeEventArgs ccevent)
		{
			this.table.UpdatePropertyDescriptorCollectionCache();
			if (this.onColumnPropertyChangedDelegate != null)
			{
				this.onColumnPropertyChangedDelegate(this, ccevent);
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0005BAEC File Offset: 0x0005AEEC
		internal void RegisterColumnName(string name, DataColumn column)
		{
			try
			{
				this.columnFromName.Add(name, column);
				if (column != null)
				{
					column._hashCode = this.table.GetSpecialHashCode(name);
				}
			}
			catch (ArgumentException)
			{
				if (this.columnFromName[name] == null)
				{
					throw ExceptionBuilder.CannotAddDuplicate2(name);
				}
				if (column != null)
				{
					throw ExceptionBuilder.CannotAddDuplicate(name);
				}
				throw ExceptionBuilder.CannotAddDuplicate3(name);
			}
			if (column == null && base.NamesEqual(name, this.MakeName(this.defaultNameIndex), true, this.table.Locale) != 0)
			{
				do
				{
					this.defaultNameIndex++;
				}
				while (this.Contains(this.MakeName(this.defaultNameIndex)));
			}
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0005BBA8 File Offset: 0x0005AFA8
		internal bool CanRegisterName(string name)
		{
			return !this.columnFromName.ContainsKey(name);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0005BBC4 File Offset: 0x0005AFC4
		public void Remove(DataColumn column)
		{
			this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Remove, column));
			this.BaseRemove(column);
			this.ArrayRemove(column);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, column));
			if (column.ColumnMapping == MappingType.Element)
			{
				DataTable dataTable = this.table;
				int elementColumnCount = dataTable.ElementColumnCount;
				dataTable.ElementColumnCount = elementColumnCount - 1;
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0005BC18 File Offset: 0x0005B018
		public void RemoveAt(int index)
		{
			DataColumn dataColumn = this[index];
			if (dataColumn == null)
			{
				throw ExceptionBuilder.ColumnOutOfRange(index);
			}
			this.Remove(dataColumn);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0005BC40 File Offset: 0x0005B040
		public void Remove(string name)
		{
			DataColumn dataColumn = this[name];
			if (dataColumn == null)
			{
				throw ExceptionBuilder.ColumnNotInTheTable(name, this.table.TableName);
			}
			this.Remove(dataColumn);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0005BC74 File Offset: 0x0005B074
		internal void UnregisterName(string name)
		{
			this.columnFromName.Remove(name);
			if (base.NamesEqual(name, this.MakeName(this.defaultNameIndex - 1), true, this.table.Locale) != 0)
			{
				do
				{
					this.defaultNameIndex--;
				}
				while (this.defaultNameIndex > 1 && !this.Contains(this.MakeName(this.defaultNameIndex - 1)));
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0005BCE0 File Offset: 0x0005B0E0
		private void AddColumnsImplementingIChangeTrackingList(DataColumn dataColumn)
		{
			DataColumn[] array = this.columnsImplementingIChangeTracking;
			DataColumn[] array2 = new DataColumn[array.Length + 1];
			array.CopyTo(array2, 0);
			array2[array.Length] = dataColumn;
			this.columnsImplementingIChangeTracking = array2;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0005BD14 File Offset: 0x0005B114
		private void RemoveColumnsImplementingIChangeTrackingList(DataColumn dataColumn)
		{
			DataColumn[] array = this.columnsImplementingIChangeTracking;
			DataColumn[] array2 = new DataColumn[array.Length - 1];
			int i = 0;
			int num = 0;
			while (i < array.Length)
			{
				if (array[i] != dataColumn)
				{
					array2[num++] = array[i];
				}
				i++;
			}
			this.columnsImplementingIChangeTracking = array2;
		}

		// Token: 0x0400031A RID: 794
		private readonly DataTable table;

		// Token: 0x0400031B RID: 795
		private readonly ArrayList _list = new ArrayList();

		// Token: 0x0400031C RID: 796
		private int defaultNameIndex = 1;

		// Token: 0x0400031D RID: 797
		private DataColumn[] delayedAddRangeColumns;

		// Token: 0x0400031E RID: 798
		private readonly Dictionary<string, DataColumn> columnFromName;

		// Token: 0x0400031F RID: 799
		private CollectionChangeEventHandler onCollectionChangedDelegate;

		// Token: 0x04000320 RID: 800
		private CollectionChangeEventHandler onCollectionChangingDelegate;

		// Token: 0x04000321 RID: 801
		private CollectionChangeEventHandler onColumnPropertyChangedDelegate;

		// Token: 0x04000322 RID: 802
		private bool fInClear;

		// Token: 0x04000323 RID: 803
		private DataColumn[] columnsImplementingIChangeTracking = DataTable.zeroColumns;

		// Token: 0x04000324 RID: 804
		private int nColumnsImplementingIChangeTracking;

		// Token: 0x04000325 RID: 805
		private int nColumnsImplementingIRevertibleChangeTracking;
	}
}
