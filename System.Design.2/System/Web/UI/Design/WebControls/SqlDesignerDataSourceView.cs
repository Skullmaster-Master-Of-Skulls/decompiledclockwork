using System;
using System.Collections;
using System.Data;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000120 RID: 288
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class SqlDesignerDataSourceView : DesignerDataSourceView
	{
		// Token: 0x06000A89 RID: 2697 RVA: 0x0004322F File Offset: 0x0004142F
		public SqlDesignerDataSourceView(SqlDataSourceDesigner owner, string viewName) : base(owner, viewName)
		{
			this._owner = owner;
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x00043240 File Offset: 0x00041440
		public override bool CanDelete
		{
			get
			{
				return this._owner.SqlDataSource.DeleteCommand.Length > 0;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0004325A File Offset: 0x0004145A
		public override bool CanInsert
		{
			get
			{
				return this._owner.SqlDataSource.InsertCommand.Length > 0;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool CanPage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool CanRetrieveTotalRowCount
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00043274 File Offset: 0x00041474
		public override bool CanSort
		{
			get
			{
				return this._owner.SqlDataSource.DataSourceMode == SqlDataSourceMode.DataSet || this._owner.SqlDataSource.SortParameterName.Length > 0;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x000432A3 File Offset: 0x000414A3
		public override bool CanUpdate
		{
			get
			{
				return this._owner.SqlDataSource.UpdateCommand.Length > 0;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x000432C0 File Offset: 0x000414C0
		public override IDataSourceViewSchema Schema
		{
			get
			{
				DataTable dataTable = this._owner.LoadSchema();
				if (dataTable == null)
				{
					return null;
				}
				return new DataSetViewSchema(dataTable);
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x000432E4 File Offset: 0x000414E4
		public override IEnumerable GetDesignTimeData(int minimumRows, out bool isSampleData)
		{
			DataTable dataTable = this._owner.LoadSchema();
			if (dataTable != null)
			{
				isSampleData = true;
				return DesignTimeData.GetDesignTimeDataSource(DesignTimeData.CreateSampleDataTable(new DataView(dataTable), true), minimumRows);
			}
			return base.GetDesignTimeData(minimumRows, out isSampleData);
		}

		// Token: 0x0400065B RID: 1627
		private SqlDataSourceDesigner _owner;
	}
}
