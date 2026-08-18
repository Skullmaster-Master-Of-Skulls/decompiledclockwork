using System;

namespace System.Data.Design
{
	// Token: 0x02000235 RID: 565
	internal class DesignColumnCollection : DataSourceCollectionBase
	{
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x000782F6 File Offset: 0x000764F6
		protected override Type ItemType
		{
			get
			{
				return typeof(DesignColumn);
			}
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00078304 File Offset: 0x00076504
		public DesignColumnCollection(DesignTable designTable) : base(designTable)
		{
			this.designTable = designTable;
			if (designTable != null && designTable.DataTable != null)
			{
				foreach (object obj in designTable.DataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					this.Add(new DesignColumn(dataColumn));
				}
			}
			this.table = designTable;
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x00078388 File Offset: 0x00076588
		protected override INameService NameService
		{
			get
			{
				return DataSetNameService.DefaultInstance;
			}
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00078390 File Offset: 0x00076590
		public void Add(DesignColumn designColumn)
		{
			if (designColumn.DesignTable != null && designColumn.DesignTable != this.designTable)
			{
				throw new InternalException("Cannot insert a DesignColumn object in two collections.");
			}
			designColumn.DesignTable = this.designTable;
			base.List.Add(designColumn);
			if (designColumn.DataColumn != null && this.designTable != null && this.designTable.DataTable != null && !this.designTable.DataTable.Columns.Contains(designColumn.Name))
			{
				this.designTable.DataTable.Columns.Add(designColumn.DataColumn);
			}
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x00057A47 File Offset: 0x00055C47
		public void Remove(DesignColumn column)
		{
			base.List.Remove(column);
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x00057A2B File Offset: 0x00055C2B
		public int IndexOf(DesignColumn column)
		{
			return base.List.IndexOf(column);
		}

		// Token: 0x170004AD RID: 1197
		public DesignColumn this[string columnName]
		{
			get
			{
				return (DesignColumn)this.FindObject(columnName);
			}
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0007843C File Offset: 0x0007663C
		protected override void OnInsert(int index, object value)
		{
			base.OnInsert(index, value);
			base.ValidateType(value);
			DesignColumn designColumn = (DesignColumn)value;
			if (designColumn.DataColumn != null && this.table != null && !this.table.DataTable.Columns.Contains(designColumn.DataColumn.ColumnName))
			{
				this.table.DataTable.Columns.Add(designColumn.DataColumn);
			}
			designColumn.DesignTable = this.designTable;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x000784B8 File Offset: 0x000766B8
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			base.OnSet(index, oldValue, newValue);
			base.ValidateType(newValue);
			base.ValidateType(oldValue);
			DesignColumn designColumn = (DesignColumn)oldValue;
			DesignColumn designColumn2 = (DesignColumn)newValue;
			if (this.table != null && oldValue != newValue)
			{
				if (designColumn.DataColumn != null)
				{
					this.table.DataTable.Columns.Remove(designColumn.DataColumn);
					designColumn.DesignTable = null;
				}
				if (designColumn2.DataColumn != null && !this.table.DataTable.Columns.Contains(designColumn2.DataColumn.ColumnName))
				{
					this.table.DataTable.Columns.Add(designColumn2.DataColumn);
					designColumn2.DesignTable = this.designTable;
				}
			}
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00078574 File Offset: 0x00076774
		protected override void OnRemove(int index, object value)
		{
			base.OnRemove(index, value);
			base.ValidateType(value);
			DesignColumn designColumn = (DesignColumn)value;
			if (this.table != null && designColumn.DataColumn != null)
			{
				this.table.DataTable.Columns.Remove(designColumn.DataColumn);
			}
			designColumn.DesignTable = null;
		}

		// Token: 0x170004AE RID: 1198
		public DesignColumn this[int index]
		{
			get
			{
				int num = 0;
				foreach (object obj in base.InnerList)
				{
					DesignColumn result = (DesignColumn)obj;
					if (index == num)
					{
						return result;
					}
					num++;
				}
				throw new InternalException("Index out of range in getting DesignColumn", 20011);
			}
		}

		// Token: 0x04000B20 RID: 2848
		private DesignTable table;

		// Token: 0x04000B21 RID: 2849
		private DesignTable designTable;
	}
}
