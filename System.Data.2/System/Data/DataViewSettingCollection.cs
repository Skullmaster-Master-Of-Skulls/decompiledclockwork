using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000DE RID: 222
	[Editor("Microsoft.VSDesigner.Data.Design.DataViewSettingsCollectionEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class DataViewSettingCollection : ICollection, IEnumerable
	{
		// Token: 0x06000EF9 RID: 3833 RVA: 0x00078B7C File Offset: 0x00077F7C
		internal DataViewSettingCollection(DataViewManager dataViewManager)
		{
			if (dataViewManager == null)
			{
				throw ExceptionBuilder.ArgumentNull("dataViewManager");
			}
			this.dataViewManager = dataViewManager;
		}

		// Token: 0x1700023E RID: 574
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

		// Token: 0x06000EFC RID: 3836 RVA: 0x00078C2C File Offset: 0x0007802C
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

		// Token: 0x06000EFD RID: 3837 RVA: 0x00078C58 File Offset: 0x00078058
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

		// Token: 0x1700023F RID: 575
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

		// Token: 0x17000240 RID: 576
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

		// Token: 0x06000F01 RID: 3841 RVA: 0x00078CEC File Offset: 0x000780EC
		public void CopyTo(Array ar, int index)
		{
			foreach (object value in this)
			{
				ar.SetValue(value, index++);
			}
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00078D1C File Offset: 0x0007811C
		public void CopyTo(DataViewSetting[] ar, int index)
		{
			foreach (object value in this)
			{
				ar.SetValue(value, index++);
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00078D4C File Offset: 0x0007814C
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

		// Token: 0x06000F04 RID: 3844 RVA: 0x00078D78 File Offset: 0x00078178
		public IEnumerator GetEnumerator()
		{
			return new DataViewSettingCollection.DataViewSettingsEnumerator(this.dataViewManager);
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x00078D90 File Offset: 0x00078190
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00078DA0 File Offset: 0x000781A0
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x00078DB0 File Offset: 0x000781B0
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00078DC0 File Offset: 0x000781C0
		internal void Remove(DataTable table)
		{
			this.list.Remove(table);
		}

		// Token: 0x04000453 RID: 1107
		private readonly DataViewManager dataViewManager;

		// Token: 0x04000454 RID: 1108
		private readonly Hashtable list = new Hashtable();

		// Token: 0x0200034F RID: 847
		private sealed class DataViewSettingsEnumerator : IEnumerator
		{
			// Token: 0x0600340A RID: 13322 RVA: 0x0013FF48 File Offset: 0x0013F348
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

			// Token: 0x0600340B RID: 13323 RVA: 0x0013FFA0 File Offset: 0x0013F3A0
			public bool MoveNext()
			{
				return this.tableEnumerator.MoveNext();
			}

			// Token: 0x0600340C RID: 13324 RVA: 0x0013FFB8 File Offset: 0x0013F3B8
			public void Reset()
			{
				this.tableEnumerator.Reset();
			}

			// Token: 0x17000841 RID: 2113
			// (get) Token: 0x0600340D RID: 13325 RVA: 0x0013FFD0 File Offset: 0x0013F3D0
			public object Current
			{
				get
				{
					return this.dataViewSettings[(DataTable)this.tableEnumerator.Current];
				}
			}

			// Token: 0x04001EBD RID: 7869
			private DataViewSettingCollection dataViewSettings;

			// Token: 0x04001EBE RID: 7870
			private IEnumerator tableEnumerator;
		}
	}
}
