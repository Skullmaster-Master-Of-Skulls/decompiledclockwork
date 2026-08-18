using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Threading;

namespace System.Data
{
	// Token: 0x020000BB RID: 187
	[DefaultProperty("Table")]
	[DefaultEvent("CollectionChanged")]
	[Editor("Microsoft.VSDesigner.Data.Design.DataRelationCollectionEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class DataRelationCollection : InternalDataCollectionBase
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x000608C0 File Offset: 0x0005FCC0
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x17000186 RID: 390
		public abstract DataRelation this[int index]
		{
			get;
		}

		// Token: 0x17000187 RID: 391
		public abstract DataRelation this[string name]
		{
			get;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x000608D4 File Offset: 0x0005FCD4
		public void Add(DataRelation relation)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataRelationCollection.Add|API> %d#, relation=%d\n", this.ObjectID, (relation != null) ? relation.ObjectID : 0);
			try
			{
				if (this.inTransition != relation)
				{
					this.inTransition = relation;
					try
					{
						this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Add, relation));
						this.AddCore(relation);
						this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, relation));
					}
					finally
					{
						this.inTransition = null;
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0006097C File Offset: 0x0005FD7C
		public virtual void AddRange(DataRelation[] relations)
		{
			if (relations != null)
			{
				foreach (DataRelation dataRelation in relations)
				{
					if (dataRelation != null)
					{
						this.Add(dataRelation);
					}
				}
			}
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x000609AC File Offset: 0x0005FDAC
		public virtual DataRelation Add(string name, DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			DataRelation dataRelation = new DataRelation(name, parentColumns, childColumns);
			this.Add(dataRelation);
			return dataRelation;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x000609CC File Offset: 0x0005FDCC
		public virtual DataRelation Add(string name, DataColumn[] parentColumns, DataColumn[] childColumns, bool createConstraints)
		{
			DataRelation dataRelation = new DataRelation(name, parentColumns, childColumns, createConstraints);
			this.Add(dataRelation);
			return dataRelation;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x000609EC File Offset: 0x0005FDEC
		public virtual DataRelation Add(DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			DataRelation dataRelation = new DataRelation(null, parentColumns, childColumns);
			this.Add(dataRelation);
			return dataRelation;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00060A0C File Offset: 0x0005FE0C
		public virtual DataRelation Add(string name, DataColumn parentColumn, DataColumn childColumn)
		{
			DataRelation dataRelation = new DataRelation(name, parentColumn, childColumn);
			this.Add(dataRelation);
			return dataRelation;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00060A2C File Offset: 0x0005FE2C
		public virtual DataRelation Add(string name, DataColumn parentColumn, DataColumn childColumn, bool createConstraints)
		{
			DataRelation dataRelation = new DataRelation(name, parentColumn, childColumn, createConstraints);
			this.Add(dataRelation);
			return dataRelation;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00060A4C File Offset: 0x0005FE4C
		public virtual DataRelation Add(DataColumn parentColumn, DataColumn childColumn)
		{
			DataRelation dataRelation = new DataRelation(null, parentColumn, childColumn);
			this.Add(dataRelation);
			return dataRelation;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00060A6C File Offset: 0x0005FE6C
		protected virtual void AddCore(DataRelation relation)
		{
			Bid.Trace("<ds.DataRelationCollection.AddCore|INFO> %d#, relation=%d\n", this.ObjectID, (relation != null) ? relation.ObjectID : 0);
			if (relation == null)
			{
				throw ExceptionBuilder.ArgumentNull("relation");
			}
			relation.CheckState();
			DataSet dataSet = this.GetDataSet();
			if (relation.DataSet == dataSet)
			{
				throw ExceptionBuilder.RelationAlreadyInTheDataSet();
			}
			if (relation.DataSet != null)
			{
				throw ExceptionBuilder.RelationAlreadyInOtherDataSet();
			}
			if (relation.ChildTable.Locale.LCID != relation.ParentTable.Locale.LCID || relation.ChildTable.CaseSensitive != relation.ParentTable.CaseSensitive)
			{
				throw ExceptionBuilder.CaseLocaleMismatch();
			}
			if (relation.Nested)
			{
				relation.CheckNamespaceValidityForNestedRelations(relation.ParentTable.Namespace);
				relation.ValidateMultipleNestedRelations();
				DataTable parentTable = relation.ParentTable;
				int elementColumnCount = parentTable.ElementColumnCount;
				parentTable.ElementColumnCount = elementColumnCount + 1;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000ACE RID: 2766 RVA: 0x00060B44 File Offset: 0x0005FF44
		// (remove) Token: 0x06000ACF RID: 2767 RVA: 0x00060B78 File Offset: 0x0005FF78
		[ResDescription("collectionChangedEventDescr")]
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				Bid.Trace("<ds.DataRelationCollection.add_CollectionChanged|API> %d#\n", this.ObjectID);
				this.onCollectionChangedDelegate = (CollectionChangeEventHandler)Delegate.Combine(this.onCollectionChangedDelegate, value);
			}
			remove
			{
				Bid.Trace("<ds.DataRelationCollection.remove_CollectionChanged|API> %d#\n", this.ObjectID);
				this.onCollectionChangedDelegate = (CollectionChangeEventHandler)Delegate.Remove(this.onCollectionChangedDelegate, value);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000AD0 RID: 2768 RVA: 0x00060BAC File Offset: 0x0005FFAC
		// (remove) Token: 0x06000AD1 RID: 2769 RVA: 0x00060BE0 File Offset: 0x0005FFE0
		internal event CollectionChangeEventHandler CollectionChanging
		{
			add
			{
				Bid.Trace("<ds.DataRelationCollection.add_CollectionChanging|INFO> %d#\n", this.ObjectID);
				this.onCollectionChangingDelegate = (CollectionChangeEventHandler)Delegate.Combine(this.onCollectionChangingDelegate, value);
			}
			remove
			{
				Bid.Trace("<ds.DataRelationCollection.remove_CollectionChanging|INFO> %d#\n", this.ObjectID);
				this.onCollectionChangingDelegate = (CollectionChangeEventHandler)Delegate.Remove(this.onCollectionChangingDelegate, value);
			}
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00060C14 File Offset: 0x00060014
		internal string AssignName()
		{
			string result = this.MakeName(this.defaultNameIndex);
			this.defaultNameIndex++;
			return result;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00060C40 File Offset: 0x00060040
		public virtual void Clear()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataRelationCollection.Clear|API> %d#\n", this.ObjectID);
			try
			{
				int count = this.Count;
				this.OnCollectionChanging(InternalDataCollectionBase.RefreshEventArgs);
				for (int i = count - 1; i >= 0; i--)
				{
					this.inTransition = this[i];
					this.RemoveCore(this.inTransition);
				}
				this.OnCollectionChanged(InternalDataCollectionBase.RefreshEventArgs);
				this.inTransition = null;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00060CD0 File Offset: 0x000600D0
		public virtual bool Contains(string name)
		{
			return this.InternalIndexOf(name) >= 0;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00060CEC File Offset: 0x000600EC
		public void CopyTo(DataRelation[] array, int index)
		{
			if (array == null)
			{
				throw ExceptionBuilder.ArgumentNull("array");
			}
			if (index < 0)
			{
				throw ExceptionBuilder.ArgumentOutOfRange("index");
			}
			ArrayList list = this.List;
			if (array.Length - index < list.Count)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
			for (int i = 0; i < list.Count; i++)
			{
				array[index + i] = (DataRelation)list[i];
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00060D54 File Offset: 0x00060154
		public virtual int IndexOf(DataRelation relation)
		{
			int count = this.List.Count;
			for (int i = 0; i < count; i++)
			{
				if (relation == (DataRelation)this.List[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00060D90 File Offset: 0x00060190
		public virtual int IndexOf(string relationName)
		{
			int num = this.InternalIndexOf(relationName);
			if (num >= 0)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00060DAC File Offset: 0x000601AC
		internal int InternalIndexOf(string name)
		{
			int num = -1;
			if (name != null && 0 < name.Length)
			{
				int count = this.List.Count;
				for (int i = 0; i < count; i++)
				{
					DataRelation dataRelation = (DataRelation)this.List[i];
					int num2 = base.NamesEqual(dataRelation.RelationName, name, false, this.GetDataSet().Locale);
					if (num2 == 1)
					{
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

		// Token: 0x06000AD9 RID: 2777
		protected abstract DataSet GetDataSet();

		// Token: 0x06000ADA RID: 2778 RVA: 0x00060E24 File Offset: 0x00060224
		private string MakeName(int index)
		{
			if (1 == index)
			{
				return "Relation1";
			}
			return "Relation" + index.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00060E54 File Offset: 0x00060254
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this.onCollectionChangedDelegate != null)
			{
				Bid.Trace("<ds.DataRelationCollection.OnCollectionChanged|INFO> %d#\n", this.ObjectID);
				this.onCollectionChangedDelegate(this, ccevent);
			}
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00060E88 File Offset: 0x00060288
		protected virtual void OnCollectionChanging(CollectionChangeEventArgs ccevent)
		{
			if (this.onCollectionChangingDelegate != null)
			{
				Bid.Trace("<ds.DataRelationCollection.OnCollectionChanging|INFO> %d#\n", this.ObjectID);
				this.onCollectionChangingDelegate(this, ccevent);
			}
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00060EBC File Offset: 0x000602BC
		internal void RegisterName(string name)
		{
			Bid.Trace("<ds.DataRelationCollection.RegisterName|INFO> %d#, name='%ls'\n", this.ObjectID, name);
			CultureInfo locale = this.GetDataSet().Locale;
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				if (base.NamesEqual(name, this[i].RelationName, true, locale) != 0)
				{
					throw ExceptionBuilder.DuplicateRelation(this[i].RelationName);
				}
			}
			if (base.NamesEqual(name, this.MakeName(this.defaultNameIndex), true, locale) != 0)
			{
				this.defaultNameIndex++;
			}
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00060F48 File Offset: 0x00060348
		public virtual bool CanRemove(DataRelation relation)
		{
			return relation != null && relation.DataSet == this.GetDataSet();
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00060F6C File Offset: 0x0006036C
		public void Remove(DataRelation relation)
		{
			Bid.Trace("<ds.DataRelationCollection.Remove|API> %d#, relation=%d\n", this.ObjectID, (relation != null) ? relation.ObjectID : 0);
			if (this.inTransition == relation)
			{
				return;
			}
			this.inTransition = relation;
			try
			{
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Remove, relation));
				this.RemoveCore(relation);
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, relation));
			}
			finally
			{
				this.inTransition = null;
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00060FF0 File Offset: 0x000603F0
		public void RemoveAt(int index)
		{
			DataRelation dataRelation = this[index];
			if (dataRelation == null)
			{
				throw ExceptionBuilder.RelationOutOfRange(index);
			}
			this.Remove(dataRelation);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0006101C File Offset: 0x0006041C
		public void Remove(string name)
		{
			DataRelation dataRelation = this[name];
			if (dataRelation == null)
			{
				throw ExceptionBuilder.RelationNotInTheDataSet(name);
			}
			this.Remove(dataRelation);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00061044 File Offset: 0x00060444
		protected virtual void RemoveCore(DataRelation relation)
		{
			Bid.Trace("<ds.DataRelationCollection.RemoveCore|INFO> %d#, relation=%d\n", this.ObjectID, (relation != null) ? relation.ObjectID : 0);
			if (relation == null)
			{
				throw ExceptionBuilder.ArgumentNull("relation");
			}
			DataSet dataSet = this.GetDataSet();
			if (relation.DataSet != dataSet)
			{
				throw ExceptionBuilder.RelationNotInTheDataSet(relation.RelationName);
			}
			if (relation.Nested)
			{
				DataTable parentTable = relation.ParentTable;
				int elementColumnCount = parentTable.ElementColumnCount;
				parentTable.ElementColumnCount = elementColumnCount - 1;
				relation.ParentTable.Columns.UnregisterName(relation.ChildTable.TableName);
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x000610D0 File Offset: 0x000604D0
		internal void UnregisterName(string name)
		{
			Bid.Trace("<ds.DataRelationCollection.UnregisterName|INFO> %d#, name='%ls'\n", this.ObjectID, name);
			if (base.NamesEqual(name, this.MakeName(this.defaultNameIndex - 1), true, this.GetDataSet().Locale) != 0)
			{
				do
				{
					this.defaultNameIndex--;
				}
				while (this.defaultNameIndex > 1 && !this.Contains(this.MakeName(this.defaultNameIndex - 1)));
			}
		}

		// Token: 0x04000340 RID: 832
		private DataRelation inTransition;

		// Token: 0x04000341 RID: 833
		private int defaultNameIndex = 1;

		// Token: 0x04000342 RID: 834
		private CollectionChangeEventHandler onCollectionChangedDelegate;

		// Token: 0x04000343 RID: 835
		private CollectionChangeEventHandler onCollectionChangingDelegate;

		// Token: 0x04000344 RID: 836
		private static int _objectTypeCount;

		// Token: 0x04000345 RID: 837
		private readonly int _objectID = Interlocked.Increment(ref DataRelationCollection._objectTypeCount);

		// Token: 0x02000347 RID: 839
		internal sealed class DataTableRelationCollection : DataRelationCollection
		{
			// Token: 0x060033DF RID: 13279 RVA: 0x0013F478 File Offset: 0x0013E878
			internal DataTableRelationCollection(DataTable table, bool fParentCollection)
			{
				if (table == null)
				{
					throw ExceptionBuilder.RelationTableNull();
				}
				this.table = table;
				this.fParentCollection = fParentCollection;
				this.relations = new ArrayList();
			}

			// Token: 0x17000839 RID: 2105
			// (get) Token: 0x060033E0 RID: 13280 RVA: 0x0013F4B0 File Offset: 0x0013E8B0
			protected override ArrayList List
			{
				get
				{
					return this.relations;
				}
			}

			// Token: 0x060033E1 RID: 13281 RVA: 0x0013F4C4 File Offset: 0x0013E8C4
			private void EnsureDataSet()
			{
				if (this.table.DataSet == null)
				{
					throw ExceptionBuilder.RelationTableWasRemoved();
				}
			}

			// Token: 0x060033E2 RID: 13282 RVA: 0x0013F4E4 File Offset: 0x0013E8E4
			protected override DataSet GetDataSet()
			{
				this.EnsureDataSet();
				return this.table.DataSet;
			}

			// Token: 0x1700083A RID: 2106
			public override DataRelation this[int index]
			{
				get
				{
					if (index >= 0 && index < this.relations.Count)
					{
						return (DataRelation)this.relations[index];
					}
					throw ExceptionBuilder.RelationOutOfRange(index);
				}
			}

			// Token: 0x1700083B RID: 2107
			public override DataRelation this[string name]
			{
				get
				{
					int num = base.InternalIndexOf(name);
					if (num == -2)
					{
						throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
					}
					if (num >= 0)
					{
						return (DataRelation)this.List[num];
					}
					return null;
				}
			}

			// Token: 0x1400002E RID: 46
			// (add) Token: 0x060033E5 RID: 13285 RVA: 0x0013F578 File Offset: 0x0013E978
			// (remove) Token: 0x060033E6 RID: 13286 RVA: 0x0013F59C File Offset: 0x0013E99C
			internal event CollectionChangeEventHandler RelationPropertyChanged
			{
				add
				{
					this.onRelationPropertyChangedDelegate = (CollectionChangeEventHandler)Delegate.Combine(this.onRelationPropertyChangedDelegate, value);
				}
				remove
				{
					this.onRelationPropertyChangedDelegate = (CollectionChangeEventHandler)Delegate.Remove(this.onRelationPropertyChangedDelegate, value);
				}
			}

			// Token: 0x060033E7 RID: 13287 RVA: 0x0013F5C0 File Offset: 0x0013E9C0
			internal void OnRelationPropertyChanged(CollectionChangeEventArgs ccevent)
			{
				if (!this.fParentCollection)
				{
					this.table.UpdatePropertyDescriptorCollectionCache();
				}
				if (this.onRelationPropertyChangedDelegate != null)
				{
					this.onRelationPropertyChangedDelegate(this, ccevent);
				}
			}

			// Token: 0x060033E8 RID: 13288 RVA: 0x0013F5F8 File Offset: 0x0013E9F8
			private void AddCache(DataRelation relation)
			{
				this.relations.Add(relation);
				if (!this.fParentCollection)
				{
					this.table.UpdatePropertyDescriptorCollectionCache();
				}
			}

			// Token: 0x060033E9 RID: 13289 RVA: 0x0013F628 File Offset: 0x0013EA28
			protected override void AddCore(DataRelation relation)
			{
				if (this.fParentCollection)
				{
					if (relation.ChildTable != this.table)
					{
						throw ExceptionBuilder.ChildTableMismatch();
					}
				}
				else if (relation.ParentTable != this.table)
				{
					throw ExceptionBuilder.ParentTableMismatch();
				}
				this.GetDataSet().Relations.Add(relation);
				this.AddCache(relation);
			}

			// Token: 0x060033EA RID: 13290 RVA: 0x0013F680 File Offset: 0x0013EA80
			public override bool CanRemove(DataRelation relation)
			{
				if (!base.CanRemove(relation))
				{
					return false;
				}
				if (this.fParentCollection)
				{
					if (relation.ChildTable != this.table)
					{
						return false;
					}
				}
				else if (relation.ParentTable != this.table)
				{
					return false;
				}
				return true;
			}

			// Token: 0x060033EB RID: 13291 RVA: 0x0013F6C4 File Offset: 0x0013EAC4
			private void RemoveCache(DataRelation relation)
			{
				for (int i = 0; i < this.relations.Count; i++)
				{
					if (relation == this.relations[i])
					{
						this.relations.RemoveAt(i);
						if (!this.fParentCollection)
						{
							this.table.UpdatePropertyDescriptorCollectionCache();
						}
						return;
					}
				}
				throw ExceptionBuilder.RelationDoesNotExist();
			}

			// Token: 0x060033EC RID: 13292 RVA: 0x0013F71C File Offset: 0x0013EB1C
			protected override void RemoveCore(DataRelation relation)
			{
				if (this.fParentCollection)
				{
					if (relation.ChildTable != this.table)
					{
						throw ExceptionBuilder.ChildTableMismatch();
					}
				}
				else if (relation.ParentTable != this.table)
				{
					throw ExceptionBuilder.ParentTableMismatch();
				}
				this.GetDataSet().Relations.Remove(relation);
				this.RemoveCache(relation);
			}

			// Token: 0x04001EB0 RID: 7856
			private readonly DataTable table;

			// Token: 0x04001EB1 RID: 7857
			private readonly ArrayList relations;

			// Token: 0x04001EB2 RID: 7858
			private readonly bool fParentCollection;

			// Token: 0x04001EB3 RID: 7859
			private CollectionChangeEventHandler onRelationPropertyChangedDelegate;
		}

		// Token: 0x02000348 RID: 840
		internal sealed class DataSetRelationCollection : DataRelationCollection
		{
			// Token: 0x060033ED RID: 13293 RVA: 0x0013F774 File Offset: 0x0013EB74
			internal DataSetRelationCollection(DataSet dataSet)
			{
				if (dataSet == null)
				{
					throw ExceptionBuilder.RelationDataSetNull();
				}
				this.dataSet = dataSet;
				this.relations = new ArrayList();
			}

			// Token: 0x1700083C RID: 2108
			// (get) Token: 0x060033EE RID: 13294 RVA: 0x0013F7A4 File Offset: 0x0013EBA4
			protected override ArrayList List
			{
				get
				{
					return this.relations;
				}
			}

			// Token: 0x060033EF RID: 13295 RVA: 0x0013F7B8 File Offset: 0x0013EBB8
			public override void AddRange(DataRelation[] relations)
			{
				if (this.dataSet.fInitInProgress)
				{
					this.delayLoadingRelations = relations;
					return;
				}
				if (relations != null)
				{
					foreach (DataRelation dataRelation in relations)
					{
						if (dataRelation != null)
						{
							base.Add(dataRelation);
						}
					}
				}
			}

			// Token: 0x060033F0 RID: 13296 RVA: 0x0013F7FC File Offset: 0x0013EBFC
			public override void Clear()
			{
				base.Clear();
				if (this.dataSet.fInitInProgress && this.delayLoadingRelations != null)
				{
					this.delayLoadingRelations = null;
				}
			}

			// Token: 0x060033F1 RID: 13297 RVA: 0x0013F82C File Offset: 0x0013EC2C
			protected override DataSet GetDataSet()
			{
				return this.dataSet;
			}

			// Token: 0x1700083D RID: 2109
			public override DataRelation this[int index]
			{
				get
				{
					if (index >= 0 && index < this.relations.Count)
					{
						return (DataRelation)this.relations[index];
					}
					throw ExceptionBuilder.RelationOutOfRange(index);
				}
			}

			// Token: 0x1700083E RID: 2110
			public override DataRelation this[string name]
			{
				get
				{
					int num = base.InternalIndexOf(name);
					if (num == -2)
					{
						throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
					}
					if (num >= 0)
					{
						return (DataRelation)this.List[num];
					}
					return null;
				}
			}

			// Token: 0x060033F4 RID: 13300 RVA: 0x0013F8B4 File Offset: 0x0013ECB4
			protected override void AddCore(DataRelation relation)
			{
				base.AddCore(relation);
				if (relation.ChildTable.DataSet != this.dataSet || relation.ParentTable.DataSet != this.dataSet)
				{
					throw ExceptionBuilder.ForeignRelation();
				}
				relation.CheckState();
				if (relation.Nested)
				{
					relation.CheckNestedRelations();
				}
				if (relation.relationName.Length == 0)
				{
					relation.relationName = base.AssignName();
				}
				else
				{
					base.RegisterName(relation.relationName);
				}
				DataKey childKey = relation.ChildKey;
				for (int i = 0; i < this.relations.Count; i++)
				{
					if (childKey.ColumnsEqual(((DataRelation)this.relations[i]).ChildKey) && relation.ParentKey.ColumnsEqual(((DataRelation)this.relations[i]).ParentKey))
					{
						throw ExceptionBuilder.RelationAlreadyExists();
					}
				}
				this.relations.Add(relation);
				((DataRelationCollection.DataTableRelationCollection)relation.ParentTable.ChildRelations).Add(relation);
				((DataRelationCollection.DataTableRelationCollection)relation.ChildTable.ParentRelations).Add(relation);
				relation.SetDataSet(this.dataSet);
				relation.ChildKey.GetSortIndex().AddRef();
				if (relation.Nested)
				{
					relation.ChildTable.CacheNestedParent();
				}
				ForeignKeyConstraint foreignKeyConstraint = relation.ChildTable.Constraints.FindForeignKeyConstraint(relation.ParentColumnsReference, relation.ChildColumnsReference);
				if (relation.createConstraints && foreignKeyConstraint == null)
				{
					relation.ChildTable.Constraints.Add(foreignKeyConstraint = new ForeignKeyConstraint(relation.ParentColumnsReference, relation.ChildColumnsReference));
					try
					{
						foreignKeyConstraint.ConstraintName = relation.RelationName;
					}
					catch (Exception e)
					{
						if (!ADP.IsCatchableExceptionType(e))
						{
							throw;
						}
						ExceptionBuilder.TraceExceptionWithoutRethrow(e);
					}
				}
				UniqueConstraint parentKeyConstraint = relation.ParentTable.Constraints.FindKeyConstraint(relation.ParentColumnsReference);
				relation.SetParentKeyConstraint(parentKeyConstraint);
				relation.SetChildKeyConstraint(foreignKeyConstraint);
			}

			// Token: 0x060033F5 RID: 13301 RVA: 0x0013FAB4 File Offset: 0x0013EEB4
			protected override void RemoveCore(DataRelation relation)
			{
				base.RemoveCore(relation);
				this.dataSet.OnRemoveRelationHack(relation);
				relation.SetDataSet(null);
				relation.ChildKey.GetSortIndex().RemoveRef();
				if (relation.Nested)
				{
					relation.ChildTable.CacheNestedParent();
				}
				for (int i = 0; i < this.relations.Count; i++)
				{
					if (relation == this.relations[i])
					{
						this.relations.RemoveAt(i);
						((DataRelationCollection.DataTableRelationCollection)relation.ParentTable.ChildRelations).Remove(relation);
						((DataRelationCollection.DataTableRelationCollection)relation.ChildTable.ParentRelations).Remove(relation);
						if (relation.Nested)
						{
							relation.ChildTable.CacheNestedParent();
						}
						base.UnregisterName(relation.RelationName);
						relation.SetParentKeyConstraint(null);
						relation.SetChildKeyConstraint(null);
						return;
					}
				}
				throw ExceptionBuilder.RelationDoesNotExist();
			}

			// Token: 0x060033F6 RID: 13302 RVA: 0x0013FB98 File Offset: 0x0013EF98
			internal void FinishInitRelations()
			{
				if (this.delayLoadingRelations == null)
				{
					return;
				}
				for (int i = 0; i < this.delayLoadingRelations.Length; i++)
				{
					DataRelation dataRelation = this.delayLoadingRelations[i];
					if (dataRelation.parentColumnNames == null || dataRelation.childColumnNames == null)
					{
						base.Add(dataRelation);
					}
					else
					{
						int num = dataRelation.parentColumnNames.Length;
						DataColumn[] array = new DataColumn[num];
						DataColumn[] array2 = new DataColumn[num];
						for (int j = 0; j < num; j++)
						{
							if (dataRelation.parentTableNamespace == null)
							{
								array[j] = this.dataSet.Tables[dataRelation.parentTableName].Columns[dataRelation.parentColumnNames[j]];
							}
							else
							{
								array[j] = this.dataSet.Tables[dataRelation.parentTableName, dataRelation.parentTableNamespace].Columns[dataRelation.parentColumnNames[j]];
							}
							if (dataRelation.childTableNamespace == null)
							{
								array2[j] = this.dataSet.Tables[dataRelation.childTableName].Columns[dataRelation.childColumnNames[j]];
							}
							else
							{
								array2[j] = this.dataSet.Tables[dataRelation.childTableName, dataRelation.childTableNamespace].Columns[dataRelation.childColumnNames[j]];
							}
						}
						base.Add(new DataRelation(dataRelation.relationName, array, array2, false)
						{
							Nested = dataRelation.nested
						});
					}
				}
				this.delayLoadingRelations = null;
			}

			// Token: 0x04001EB4 RID: 7860
			private readonly DataSet dataSet;

			// Token: 0x04001EB5 RID: 7861
			private readonly ArrayList relations;

			// Token: 0x04001EB6 RID: 7862
			private DataRelation[] delayLoadingRelations;
		}
	}
}
