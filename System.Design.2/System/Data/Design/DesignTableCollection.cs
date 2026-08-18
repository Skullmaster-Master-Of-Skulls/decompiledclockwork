using System;

namespace System.Data.Design
{
	// Token: 0x0200023F RID: 575
	internal class DesignTableCollection : DataSourceCollectionBase
	{
		// Token: 0x0600167A RID: 5754 RVA: 0x0007BDA3 File Offset: 0x00079FA3
		public DesignTableCollection(DesignDataSource dataSource) : base(dataSource)
		{
			this.dataSource = dataSource;
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x0007BDB3 File Offset: 0x00079FB3
		private DataSet DataSet
		{
			get
			{
				if (this.dataSource != null)
				{
					return this.dataSource.DataSet;
				}
				return null;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x0007BDCA File Offset: 0x00079FCA
		protected override Type ItemType
		{
			get
			{
				return typeof(DesignTable);
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x00078388 File Offset: 0x00076588
		protected override INameService NameService
		{
			get
			{
				return DataSetNameService.DefaultInstance;
			}
		}

		// Token: 0x1700052D RID: 1325
		internal DesignTable this[string name]
		{
			get
			{
				return (DesignTable)this.FindObject(name);
			}
		}

		// Token: 0x1700052E RID: 1326
		internal DesignTable this[DataTable dataTable]
		{
			get
			{
				foreach (object obj in this)
				{
					DesignTable designTable = (DesignTable)obj;
					if (designTable.DataTable == dataTable)
					{
						return designTable;
					}
				}
				return null;
			}
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0007BE44 File Offset: 0x0007A044
		public void Add(DesignTable designTable)
		{
			base.List.Add(designTable);
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00057A39 File Offset: 0x00055C39
		public bool Contains(DesignTable table)
		{
			return base.List.Contains(table);
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00057A2B File Offset: 0x00055C2B
		public int IndexOf(DesignTable table)
		{
			return base.List.IndexOf(table);
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00057A47 File Offset: 0x00055C47
		public void Remove(DesignTable table)
		{
			base.List.Remove(table);
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x0007BE54 File Offset: 0x0007A054
		protected override void OnInsert(int index, object value)
		{
			base.OnInsert(index, value);
			DesignTable designTable = (DesignTable)value;
			if (designTable.Name == null || designTable.Name.Length == 0)
			{
				designTable.Name = this.CreateUniqueName(designTable);
			}
			this.NameService.ValidateUniqueName(this, designTable.Name);
			if (this.dataSource != null && designTable.Owner == this.dataSource)
			{
				return;
			}
			if (this.dataSource != null && designTable.Owner != null)
			{
				throw new InternalException("This table belongs to another DataSource already", 20002);
			}
			DataSet dataSet = this.DataSet;
			if (dataSet != null && !dataSet.Tables.Contains(designTable.DataTable.TableName))
			{
				dataSet.Tables.Add(designTable.DataTable);
			}
			designTable.Owner = this.dataSource;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0007BF1C File Offset: 0x0007A11C
		protected override void OnRemove(int index, object value)
		{
			base.OnRemove(index, value);
			DesignTable designTable = (DesignTable)value;
			DataSet dataSet = this.DataSet;
			if (dataSet != null && designTable.DataTable != null && dataSet.Tables.Contains(designTable.DataTable.TableName))
			{
				dataSet.Tables.Remove(designTable.DataTable);
			}
			designTable.Owner = null;
		}

		// Token: 0x04000B88 RID: 2952
		private DesignDataSource dataSource;
	}
}
