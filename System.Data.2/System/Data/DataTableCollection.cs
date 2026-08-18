using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace System.Data
{
	// Token: 0x020000D1 RID: 209
	[ListBindable(false)]
	[DefaultEvent("CollectionChanged")]
	[Editor("Microsoft.VSDesigner.Data.Design.TablesCollectionEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class DataTableCollection : InternalDataCollectionBase
	{
		// Token: 0x06000D9B RID: 3483 RVA: 0x00072AE4 File Offset: 0x00071EE4
		internal DataTableCollection(DataSet dataSet)
		{
			Bid.Trace("<ds.DataTableCollection.DataTableCollection|INFO> %d#, dataSet=%d\n", this.ObjectID, (dataSet != null) ? dataSet.ObjectID : 0);
			this.dataSet = dataSet;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x00072B3C File Offset: 0x00071F3C
		protected override ArrayList List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00072B50 File Offset: 0x00071F50
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x170001F1 RID: 497
		public DataTable this[int index]
		{
			get
			{
				DataTable result;
				try
				{
					result = (DataTable)this._list[index];
				}
				catch (ArgumentOutOfRangeException)
				{
					throw ExceptionBuilder.TableOutOfRange(index);
				}
				return result;
			}
		}

		// Token: 0x170001F2 RID: 498
		public DataTable this[string name]
		{
			get
			{
				int num = this.InternalIndexOf(name);
				if (num == -2)
				{
					throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
				}
				if (num == -3)
				{
					throw ExceptionBuilder.NamespaceNameConflict(name);
				}
				if (num >= 0)
				{
					return (DataTable)this._list[num];
				}
				return null;
			}
		}

		// Token: 0x170001F3 RID: 499
		public DataTable this[string name, string tableNamespace]
		{
			get
			{
				if (tableNamespace == null)
				{
					throw ExceptionBuilder.ArgumentNull("tableNamespace");
				}
				int num = this.InternalIndexOf(name, tableNamespace);
				if (num == -2)
				{
					throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
				}
				if (num >= 0)
				{
					return (DataTable)this._list[num];
				}
				return null;
			}
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00072C38 File Offset: 0x00072038
		internal DataTable GetTable(string name, string ns)
		{
			for (int i = 0; i < this._list.Count; i++)
			{
				DataTable dataTable = (DataTable)this._list[i];
				if (dataTable.TableName == name && dataTable.Namespace == ns)
				{
					return dataTable;
				}
			}
			return null;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00072C8C File Offset: 0x0007208C
		internal DataTable GetTableSmart(string name, string ns)
		{
			int num = 0;
			DataTable result = null;
			for (int i = 0; i < this._list.Count; i++)
			{
				DataTable dataTable = (DataTable)this._list[i];
				if (dataTable.TableName == name)
				{
					if (dataTable.Namespace == ns)
					{
						return dataTable;
					}
					num++;
					result = dataTable;
				}
			}
			if (num != 1)
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00072CF0 File Offset: 0x000720F0
		public void Add(DataTable table)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTableCollection.Add|API> %d#, table=%d\n", this.ObjectID, (table != null) ? table.ObjectID : 0);
			try
			{
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Add, table));
				this.BaseAdd(table);
				this.ArrayAdd(table);
				if (table.SetLocaleValue(this.dataSet.Locale, false, false) || table.SetCaseSensitiveValue(this.dataSet.CaseSensitive, false, false))
				{
					table.ResetIndexes();
				}
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, table));
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00072D9C File Offset: 0x0007219C
		public void AddRange(DataTable[] tables)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTableCollection.AddRange|API> %d#\n", this.ObjectID);
			try
			{
				if (this.dataSet.fInitInProgress)
				{
					this.delayedAddRangeTables = tables;
				}
				else if (tables != null)
				{
					foreach (DataTable dataTable in tables)
					{
						if (dataTable != null)
						{
							this.Add(dataTable);
						}
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00072E18 File Offset: 0x00072218
		public DataTable Add(string name)
		{
			DataTable dataTable = new DataTable(name);
			this.Add(dataTable);
			return dataTable;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00072E34 File Offset: 0x00072234
		public DataTable Add(string name, string tableNamespace)
		{
			DataTable dataTable = new DataTable(name, tableNamespace);
			this.Add(dataTable);
			return dataTable;
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00072E54 File Offset: 0x00072254
		public DataTable Add()
		{
			DataTable dataTable = new DataTable();
			this.Add(dataTable);
			return dataTable;
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000DA8 RID: 3496 RVA: 0x00072E70 File Offset: 0x00072270
		// (remove) Token: 0x06000DA9 RID: 3497 RVA: 0x00072EA4 File Offset: 0x000722A4
		[ResDescription("collectionChangedEventDescr")]
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				Bid.Trace("<ds.DataTableCollection.add_CollectionChanged|API> %d#\n", this.ObjectID);
				this.onCollectionChangedDelegate = (CollectionChangeEventHandler)Delegate.Combine(this.onCollectionChangedDelegate, value);
			}
			remove
			{
				Bid.Trace("<ds.DataTableCollection.remove_CollectionChanged|API> %d#\n", this.ObjectID);
				this.onCollectionChangedDelegate = (CollectionChangeEventHandler)Delegate.Remove(this.onCollectionChangedDelegate, value);
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000DAA RID: 3498 RVA: 0x00072ED8 File Offset: 0x000722D8
		// (remove) Token: 0x06000DAB RID: 3499 RVA: 0x00072F0C File Offset: 0x0007230C
		public event CollectionChangeEventHandler CollectionChanging
		{
			add
			{
				Bid.Trace("<ds.DataTableCollection.add_CollectionChanging|API> %d#\n", this.ObjectID);
				this.onCollectionChangingDelegate = (CollectionChangeEventHandler)Delegate.Combine(this.onCollectionChangingDelegate, value);
			}
			remove
			{
				Bid.Trace("<ds.DataTableCollection.remove_CollectionChanging|API> %d#\n", this.ObjectID);
				this.onCollectionChangingDelegate = (CollectionChangeEventHandler)Delegate.Remove(this.onCollectionChangingDelegate, value);
			}
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00072F40 File Offset: 0x00072340
		private void ArrayAdd(DataTable table)
		{
			this._list.Add(table);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00072F5C File Offset: 0x0007235C
		internal string AssignName()
		{
			string result;
			while (this.Contains(result = this.MakeName(this.defaultNameIndex)))
			{
				this.defaultNameIndex++;
			}
			return result;
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00072F94 File Offset: 0x00072394
		private void BaseAdd(DataTable table)
		{
			if (table == null)
			{
				throw ExceptionBuilder.ArgumentNull("table");
			}
			if (table.DataSet == this.dataSet)
			{
				throw ExceptionBuilder.TableAlreadyInTheDataSet();
			}
			if (table.DataSet != null)
			{
				throw ExceptionBuilder.TableAlreadyInOtherDataSet();
			}
			if (table.TableName.Length == 0)
			{
				table.TableName = this.AssignName();
			}
			else
			{
				if (base.NamesEqual(table.TableName, this.dataSet.DataSetName, false, this.dataSet.Locale) != 0 && !table.fNestedInDataset)
				{
					throw ExceptionBuilder.DatasetConflictingName(this.dataSet.DataSetName);
				}
				this.RegisterName(table.TableName, table.Namespace);
			}
			table.SetDataSet(this.dataSet);
			foreach (object obj in table.Constraints)
			{
				Constraint constraint = (Constraint)obj;
				constraint.SetDataSet(this.dataSet);
			}
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x000730A8 File Offset: 0x000724A8
		private void BaseGroupSwitch(DataTable[] oldArray, int oldLength, DataTable[] newArray, int newLength)
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
				if (!flag && oldArray[i].DataSet == this.dataSet)
				{
					this.BaseRemove(oldArray[i]);
				}
			}
			for (int k = 0; k < newLength; k++)
			{
				if (newArray[k].DataSet != this.dataSet)
				{
					this.BaseAdd(newArray[k]);
					this._list.Add(newArray[k]);
				}
			}
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0007313C File Offset: 0x0007253C
		private void BaseRemove(DataTable table)
		{
			if (this.CanRemove(table, true))
			{
				this.UnregisterName(table.TableName);
				table.SetDataSet(null);
				foreach (object obj in table.Constraints)
				{
					Constraint constraint = (Constraint)obj;
					constraint.SetDataSet(null);
				}
			}
			this._list.Remove(table);
			this.dataSet.OnRemovedTable(table);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x000731D8 File Offset: 0x000725D8
		public bool CanRemove(DataTable table)
		{
			return this.CanRemove(table, false);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x000731F0 File Offset: 0x000725F0
		internal bool CanRemove(DataTable table, bool fThrowException)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTableCollection.CanRemove|INFO> %d#, table=%d, fThrowException=%d{bool}\n", this.ObjectID, (table != null) ? table.ObjectID : 0, fThrowException);
			bool result;
			try
			{
				if (table == null)
				{
					if (fThrowException)
					{
						throw ExceptionBuilder.ArgumentNull("table");
					}
					result = false;
				}
				else if (table.DataSet != this.dataSet)
				{
					if (fThrowException)
					{
						throw ExceptionBuilder.TableNotInTheDataSet(table.TableName);
					}
					result = false;
				}
				else
				{
					this.dataSet.OnRemoveTable(table);
					if (table.ChildRelations.Count != 0 || table.ParentRelations.Count != 0)
					{
						if (fThrowException)
						{
							throw ExceptionBuilder.TableInRelation();
						}
						result = false;
					}
					else
					{
						ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this.dataSet, table);
						while (parentForeignKeyConstraintEnumerator.GetNext())
						{
							ForeignKeyConstraint foreignKeyConstraint = parentForeignKeyConstraintEnumerator.GetForeignKeyConstraint();
							if (foreignKeyConstraint.Table != table || foreignKeyConstraint.RelatedTable != table)
							{
								if (!fThrowException)
								{
									return false;
								}
								throw ExceptionBuilder.TableInConstraint(table, foreignKeyConstraint);
							}
						}
						ChildForeignKeyConstraintEnumerator childForeignKeyConstraintEnumerator = new ChildForeignKeyConstraintEnumerator(this.dataSet, table);
						while (childForeignKeyConstraintEnumerator.GetNext())
						{
							ForeignKeyConstraint foreignKeyConstraint2 = childForeignKeyConstraintEnumerator.GetForeignKeyConstraint();
							if (foreignKeyConstraint2.Table != table || foreignKeyConstraint2.RelatedTable != table)
							{
								if (!fThrowException)
								{
									return false;
								}
								throw ExceptionBuilder.TableInConstraint(table, foreignKeyConstraint2);
							}
						}
						result = true;
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00073338 File Offset: 0x00072738
		public void Clear()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTableCollection.Clear|API> %d#\n", this.ObjectID);
			try
			{
				int count = this._list.Count;
				DataTable[] array = new DataTable[this._list.Count];
				this._list.CopyTo(array, 0);
				this.OnCollectionChanging(InternalDataCollectionBase.RefreshEventArgs);
				if (this.dataSet.fInitInProgress && this.delayedAddRangeTables != null)
				{
					this.delayedAddRangeTables = null;
				}
				this.BaseGroupSwitch(array, count, null, 0);
				this._list.Clear();
				this.OnCollectionChanged(InternalDataCollectionBase.RefreshEventArgs);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x000733F0 File Offset: 0x000727F0
		public bool Contains(string name)
		{
			return this.InternalIndexOf(name) >= 0;
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0007340C File Offset: 0x0007280C
		public bool Contains(string name, string tableNamespace)
		{
			if (name == null)
			{
				throw ExceptionBuilder.ArgumentNull("name");
			}
			if (tableNamespace == null)
			{
				throw ExceptionBuilder.ArgumentNull("tableNamespace");
			}
			return this.InternalIndexOf(name, tableNamespace) >= 0;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00073444 File Offset: 0x00072844
		internal bool Contains(string name, string tableNamespace, bool checkProperty, bool caseSensitive)
		{
			if (!caseSensitive)
			{
				return this.InternalIndexOf(name) >= 0;
			}
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				DataTable dataTable = (DataTable)this._list[i];
				string a = checkProperty ? dataTable.Namespace : dataTable.tableNamespace;
				if (base.NamesEqual(dataTable.TableName, name, true, this.dataSet.Locale) == 1 && a == tableNamespace)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x000734C8 File Offset: 0x000728C8
		internal bool Contains(string name, bool caseSensitive)
		{
			if (!caseSensitive)
			{
				return this.InternalIndexOf(name) >= 0;
			}
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				DataTable dataTable = (DataTable)this._list[i];
				if (base.NamesEqual(dataTable.TableName, name, true, this.dataSet.Locale) == 1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00073530 File Offset: 0x00072930
		public void CopyTo(DataTable[] array, int index)
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
				array[index + i] = (DataTable)this._list[i];
			}
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x000735A0 File Offset: 0x000729A0
		public int IndexOf(DataTable table)
		{
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				if (table == (DataTable)this._list[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x000735DC File Offset: 0x000729DC
		public int IndexOf(string tableName)
		{
			int num = this.InternalIndexOf(tableName);
			if (num >= 0)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x000735F8 File Offset: 0x000729F8
		public int IndexOf(string tableName, string tableNamespace)
		{
			return this.IndexOf(tableName, tableNamespace, true);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00073610 File Offset: 0x00072A10
		internal int IndexOf(string tableName, string tableNamespace, bool chekforNull)
		{
			if (chekforNull)
			{
				if (tableName == null)
				{
					throw ExceptionBuilder.ArgumentNull("tableName");
				}
				if (tableNamespace == null)
				{
					throw ExceptionBuilder.ArgumentNull("tableNamespace");
				}
			}
			int num = this.InternalIndexOf(tableName, tableNamespace);
			if (num >= 0)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0007364C File Offset: 0x00072A4C
		internal void ReplaceFromInference(List<DataTable> tableList)
		{
			this._list.Clear();
			this._list.AddRange(tableList);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00073670 File Offset: 0x00072A70
		internal int InternalIndexOf(string tableName)
		{
			int num = -1;
			if (tableName != null && 0 < tableName.Length)
			{
				int count = this._list.Count;
				for (int i = 0; i < count; i++)
				{
					DataTable dataTable = (DataTable)this._list[i];
					int num2 = base.NamesEqual(dataTable.TableName, tableName, false, this.dataSet.Locale);
					if (num2 == 1)
					{
						for (int j = i + 1; j < count; j++)
						{
							DataTable dataTable2 = (DataTable)this._list[j];
							if (base.NamesEqual(dataTable2.TableName, tableName, false, this.dataSet.Locale) == 1)
							{
								return -3;
							}
						}
						return i;
					}
					if (num2 == -1)
					{
						num = ((num == -1) ? i : -2);
					}
				}
			}
			return num;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00073738 File Offset: 0x00072B38
		internal int InternalIndexOf(string tableName, string tableNamespace)
		{
			int num = -1;
			if (tableName != null && 0 < tableName.Length)
			{
				int count = this._list.Count;
				for (int i = 0; i < count; i++)
				{
					DataTable dataTable = (DataTable)this._list[i];
					int num2 = base.NamesEqual(dataTable.TableName, tableName, false, this.dataSet.Locale);
					if (num2 == 1 && dataTable.Namespace == tableNamespace)
					{
						return i;
					}
					if (num2 == -1 && dataTable.Namespace == tableNamespace)
					{
						num = ((num == -1) ? i : -2);
					}
				}
			}
			return num;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x000737D4 File Offset: 0x00072BD4
		internal void FinishInitCollection()
		{
			if (this.delayedAddRangeTables != null)
			{
				foreach (DataTable dataTable in this.delayedAddRangeTables)
				{
					if (dataTable != null)
					{
						this.Add(dataTable);
					}
				}
				this.delayedAddRangeTables = null;
			}
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00073814 File Offset: 0x00072C14
		private string MakeName(int index)
		{
			if (1 == index)
			{
				return "Table1";
			}
			return "Table" + index.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00073844 File Offset: 0x00072C44
		private void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this.onCollectionChangedDelegate != null)
			{
				Bid.Trace("<ds.DataTableCollection.OnCollectionChanged|INFO> %d#\n", this.ObjectID);
				this.onCollectionChangedDelegate(this, ccevent);
			}
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00073878 File Offset: 0x00072C78
		private void OnCollectionChanging(CollectionChangeEventArgs ccevent)
		{
			if (this.onCollectionChangingDelegate != null)
			{
				Bid.Trace("<ds.DataTableCollection.OnCollectionChanging|INFO> %d#\n", this.ObjectID);
				this.onCollectionChangingDelegate(this, ccevent);
			}
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x000738AC File Offset: 0x00072CAC
		internal void RegisterName(string name, string tbNamespace)
		{
			Bid.Trace("<ds.DataTableCollection.RegisterName|INFO> %d#, name='%ls', tbNamespace='%ls'\n", this.ObjectID, name, tbNamespace);
			CultureInfo locale = this.dataSet.Locale;
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				DataTable dataTable = (DataTable)this._list[i];
				if (base.NamesEqual(name, dataTable.TableName, true, locale) != 0 && tbNamespace == dataTable.Namespace)
				{
					throw ExceptionBuilder.DuplicateTableName(((DataTable)this._list[i]).TableName);
				}
			}
			if (base.NamesEqual(name, this.MakeName(this.defaultNameIndex), true, locale) != 0)
			{
				this.defaultNameIndex++;
			}
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00073964 File Offset: 0x00072D64
		public void Remove(DataTable table)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTableCollection.Remove|API> %d#, table=%d\n", this.ObjectID, (table != null) ? table.ObjectID : 0);
			try
			{
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Remove, table));
				this.BaseRemove(table);
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, table));
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x000739D8 File Offset: 0x00072DD8
		public void RemoveAt(int index)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTableCollection.RemoveAt|API> %d#, index=%d\n", this.ObjectID, index);
			try
			{
				DataTable dataTable = this[index];
				if (dataTable == null)
				{
					throw ExceptionBuilder.TableOutOfRange(index);
				}
				this.Remove(dataTable);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00073A38 File Offset: 0x00072E38
		public void Remove(string name)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTableCollection.Remove|API> %d#, name='%ls'\n", this.ObjectID, name);
			try
			{
				DataTable dataTable = this[name];
				if (dataTable == null)
				{
					throw ExceptionBuilder.TableNotInTheDataSet(name);
				}
				this.Remove(dataTable);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00073A98 File Offset: 0x00072E98
		public void Remove(string name, string tableNamespace)
		{
			if (name == null)
			{
				throw ExceptionBuilder.ArgumentNull("name");
			}
			if (tableNamespace == null)
			{
				throw ExceptionBuilder.ArgumentNull("tableNamespace");
			}
			DataTable dataTable = this[name, tableNamespace];
			if (dataTable == null)
			{
				throw ExceptionBuilder.TableNotInTheDataSet(name);
			}
			this.Remove(dataTable);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00073ADC File Offset: 0x00072EDC
		internal void UnregisterName(string name)
		{
			Bid.Trace("<ds.DataTableCollection.UnregisterName|INFO> %d#, name='%ls'\n", this.ObjectID, name);
			if (base.NamesEqual(name, this.MakeName(this.defaultNameIndex - 1), true, this.dataSet.Locale) != 0)
			{
				do
				{
					this.defaultNameIndex--;
				}
				while (this.defaultNameIndex > 1 && !this.Contains(this.MakeName(this.defaultNameIndex - 1)));
			}
		}

		// Token: 0x040003FC RID: 1020
		private readonly DataSet dataSet;

		// Token: 0x040003FD RID: 1021
		private readonly ArrayList _list = new ArrayList();

		// Token: 0x040003FE RID: 1022
		private int defaultNameIndex = 1;

		// Token: 0x040003FF RID: 1023
		private DataTable[] delayedAddRangeTables;

		// Token: 0x04000400 RID: 1024
		private CollectionChangeEventHandler onCollectionChangedDelegate;

		// Token: 0x04000401 RID: 1025
		private CollectionChangeEventHandler onCollectionChangingDelegate;

		// Token: 0x04000402 RID: 1026
		private static int _objectTypeCount;

		// Token: 0x04000403 RID: 1027
		private readonly int _objectID = Interlocked.Increment(ref DataTableCollection._objectTypeCount);
	}
}
