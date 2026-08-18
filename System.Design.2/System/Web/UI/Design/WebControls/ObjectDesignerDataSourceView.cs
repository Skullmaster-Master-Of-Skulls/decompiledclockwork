using System;
using System.Collections;
using System.Data;
using System.Security.Permissions;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000F2 RID: 242
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ObjectDesignerDataSourceView : DesignerDataSourceView
	{
		// Token: 0x06000870 RID: 2160 RVA: 0x0002FD97 File Offset: 0x0002DF97
		public ObjectDesignerDataSourceView(ObjectDataSourceDesigner owner, string viewName) : base(owner, viewName)
		{
			this._owner = owner;
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x0002FDA8 File Offset: 0x0002DFA8
		public override bool CanDelete
		{
			get
			{
				return this._owner.ObjectDataSource.DeleteMethod.Length > 0;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0002FDC2 File Offset: 0x0002DFC2
		public override bool CanInsert
		{
			get
			{
				return this._owner.ObjectDataSource.InsertMethod.Length > 0;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0002FDDC File Offset: 0x0002DFDC
		public override bool CanPage
		{
			get
			{
				return this._owner.ObjectDataSource.EnablePaging;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0002FDEE File Offset: 0x0002DFEE
		public override bool CanRetrieveTotalRowCount
		{
			get
			{
				return this._owner.ObjectDataSource.SelectCountMethod.Length > 0;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0002FE08 File Offset: 0x0002E008
		public override bool CanSort
		{
			get
			{
				if (this._owner.ObjectDataSource.SortParameterName.Length > 0)
				{
					return true;
				}
				Type selectMethodReturnType = this._owner.SelectMethodReturnType;
				return selectMethodReturnType != null && (typeof(DataSet).IsAssignableFrom(selectMethodReturnType) || typeof(DataTable).IsAssignableFrom(selectMethodReturnType) || typeof(DataView).IsAssignableFrom(selectMethodReturnType));
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0002FE7C File Offset: 0x0002E07C
		public override bool CanUpdate
		{
			get
			{
				return this._owner.ObjectDataSource.UpdateMethod.Length > 0;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0002FE98 File Offset: 0x0002E098
		public override IDataSourceViewSchema Schema
		{
			get
			{
				DataTable[] array = this._owner.LoadSchema();
				if (array != null && array.Length != 0)
				{
					if (base.Name.Length == 0)
					{
						return new DataSetViewSchema(array[0]);
					}
					foreach (DataTable dataTable in array)
					{
						if (string.Equals(dataTable.TableName, base.Name, StringComparison.OrdinalIgnoreCase))
						{
							return new DataSetViewSchema(dataTable);
						}
					}
				}
				return null;
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0002FF00 File Offset: 0x0002E100
		public override IEnumerable GetDesignTimeData(int minimumRows, out bool isSampleData)
		{
			isSampleData = true;
			DataTable[] array = this._owner.LoadSchema();
			if (array != null && array.Length != 0)
			{
				if (base.Name.Length == 0)
				{
					return DesignTimeData.GetDesignTimeDataSource(DesignTimeData.CreateSampleDataTable(new DataView(array[0]), true), minimumRows);
				}
				foreach (DataTable dataTable in array)
				{
					if (string.Equals(dataTable.TableName, base.Name, StringComparison.OrdinalIgnoreCase))
					{
						return DesignTimeData.GetDesignTimeDataSource(DesignTimeData.CreateSampleDataTable(new DataView(dataTable), true), minimumRows);
					}
				}
			}
			return base.GetDesignTimeData(minimumRows, out isSampleData);
		}

		// Token: 0x040004F0 RID: 1264
		private ObjectDataSourceDesigner _owner;
	}
}
