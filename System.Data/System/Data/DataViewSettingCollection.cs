using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000AF RID: 175
	[Editor("Microsoft.VSDesigner.Data.Design.DataViewSettingsCollectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class DataViewSettingCollection : ICollection, IEnumerable
	{
		// Token: 0x06000BF2 RID: 3058 RVA: 0x0020F338 File Offset: 0x0020E738
		internal DataViewSettingCollection(DataViewManager dataViewManager)
		{
			if (dataViewManager == null)
			{
				throw ExceptionBuilder.ArgumentNull("dataViewManager");
			}
			this.dataViewManager = dataViewManager;
		}

		// Token: 0x170001AA RID: 426
		public virtual DataViewSetting this[DataTable table]
		{
			get
			{
				if (table == null)
				{
					throw ExceptionBuilder.ArgumentNull("table");
				}
				DataViewSetting dataViewSetting = (DataViewSetting)this.list[table];
				if (dataViewSetting == null)
				{
					dataViewSetting = new DataViewSetting();
					this[table] = dataViewSetting;
				}
				return dataViewSetting;
			}
			set
			{
				if (table == null)
				{
					throw ExceptionBuilder.ArgumentNull("table");
				}
				value.SetDataViewManager(this.dataViewManager);
				value.SetDataTable(table);
				this.list[table] = value;
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0020F3F8 File Offset: 0x0020E7F8
		private DataTable GetTable(string tableName)
		{
			DataTable result = null;
			DataSet dataSet = this.dataViewManager.DataSet;
			if (dataSet != null)
			{
				result = dataSet.Tables[tableName];
			}
			return result;
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0020F428 File Offset: 0x0020E828
		private DataTable GetTable(int index)
		{
			DataTable result = null;
			DataSet dataSet = this.dataViewManager.DataSet;
			if (dataSet != null)
			{
				result = dataSet.Tables[index];
			}
			return result;
		}

		// Token: 0x170001AB RID: 427
		public virtual DataViewSetting this[string tableName]
		{
			get
			{
				DataTable table = this.GetTable(tableName);
				if (table != null)
				{
					return this[table];
				}
				return null;
			}
		}

		// Token: 0x170001AC RID: 428
		public virtual DataViewSetting this[int index]
		{
			get
			{
				DataTable table = this.GetTable(index);
				if (table != null)
				{
					return this[table];
				}
				return null;
			}
			set
			{
				DataTable table = this.GetTable(index);
				if (table != null)
				{
					this[table] = value;
				}
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0020F4D8 File Offset: 0x0020E8D8
		public void CopyTo(Array ar, int index)
		{
			foreach (object value in this)
			{
				ar.SetValue(value, index++);
			}
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0020F508 File Offset: 0x0020E908
		public void CopyTo(DataViewSetting[] ar, int index)
		{
			foreach (object value in this)
			{
				ar.SetValue(value, index++);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x0020F538 File Offset: 0x0020E938
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				DataSet dataSet = this.dataViewManager.DataSet;
				if (dataSet != null)
				{
					return dataSet.Tables.Count;
				}
				return 0;
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0020F568 File Offset: 0x0020E968
		public IEnumerator GetEnumerator()
		{
			return new DataViewSettingCollection.DataViewSettingsEnumerator(this.dataViewManager);
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0020F588 File Offset: 0x0020E988
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0020F598 File Offset: 0x0020E998
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0020F5A8 File Offset: 0x0020E9A8
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0020F5B8 File Offset: 0x0020E9B8
		internal void Remove(DataTable table)
		{
			this.list.Remove(table);
		}

		// Token: 0x04000877 RID: 2167
		private readonly DataViewManager dataViewManager;

		// Token: 0x04000878 RID: 2168
		private readonly Hashtable list = new Hashtable();

		// Token: 0x020000B0 RID: 176
		private sealed class DataViewSettingsEnumerator : IEnumerator
		{
			// Token: 0x06000C02 RID: 3074 RVA: 0x0020F5D8 File Offset: 0x0020E9D8
			public DataViewSettingsEnumerator(DataViewManager dvm)
			{
				DataSet dataSet = dvm.DataSet;
				if (dataSet != null)
				{
					this.dataViewSettings = dvm.DataViewSettings;
					this.tableEnumerator = dvm.DataSet.Tables.GetEnumerator();
					return;
				}
				this.dataViewSettings = null;
				this.tableEnumerator = DataSet.zeroTables.GetEnumerator();
			}

			// Token: 0x06000C03 RID: 3075 RVA: 0x0020F638 File Offset: 0x0020EA38
			public bool MoveNext()
			{
				return this.tableEnumerator.MoveNext();
			}

			// Token: 0x06000C04 RID: 3076 RVA: 0x0020F658 File Offset: 0x0020EA58
			public void Reset()
			{
				this.tableEnumerator.Reset();
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0020F678 File Offset: 0x0020EA78
			public object Current
			{
				get
				{
					return this.dataViewSettings[(DataTable)this.tableEnumerator.Current];
				}
			}

			// Token: 0x04000879 RID: 2169
			private DataViewSettingCollection dataViewSettings;

			// Token: 0x0400087A RID: 2170
			private IEnumerator tableEnumerator;
		}
	}
}
