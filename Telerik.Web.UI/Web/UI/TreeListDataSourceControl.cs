using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001262 RID: 4706
	internal class TreeListDataSourceControl : IDataSource
	{
		// Token: 0x0600C3B3 RID: 50099 RVA: 0x002BE1D6 File Offset: 0x002BC3D6
		public TreeListDataSourceControl(IDataSource dataSourceControl)
		{
			this.DataSourceControl = dataSourceControl;
		}

		// Token: 0x1400019C RID: 412
		// (add) Token: 0x0600C3B4 RID: 50100 RVA: 0x002BE1E5 File Offset: 0x002BC3E5
		// (remove) Token: 0x0600C3B5 RID: 50101 RVA: 0x002BE1E7 File Offset: 0x002BC3E7
		event EventHandler IDataSource.DataSourceChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x0600C3B6 RID: 50102 RVA: 0x002BE1E9 File Offset: 0x002BC3E9
		public DataSourceView GetView(string viewName)
		{
			return new TreeListDataSourceControl.TreeListDataSourceView(this.DataSourceControl, viewName);
		}

		// Token: 0x0600C3B7 RID: 50103 RVA: 0x002BE1F7 File Offset: 0x002BC3F7
		ICollection IDataSource.GetViewNames()
		{
			return TreeListDataSourceControl.ViewNames;
		}

		// Token: 0x040033DD RID: 13277
		private IDataSource DataSourceControl;

		// Token: 0x040033DE RID: 13278
		private static string[] ViewNames = new string[0];

		// Token: 0x02001263 RID: 4707
		internal class TreeListDataSourceView : DataSourceView
		{
			// Token: 0x0600C3B9 RID: 50105 RVA: 0x002BE20B File Offset: 0x002BC40B
			public TreeListDataSourceView(IDataSource dataSource, string viewName) : base(dataSource, viewName)
			{
				this.DataSourceControl = dataSource;
			}

			// Token: 0x0600C3BA RID: 50106 RVA: 0x002BE21C File Offset: 0x002BC41C
			protected override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
			{
				return new object[0];
			}

			// Token: 0x17003EFD RID: 16125
			// (get) Token: 0x0600C3BB RID: 50107 RVA: 0x002BE224 File Offset: 0x002BC424
			public override bool CanDelete
			{
				get
				{
					return this.DataSourceControl.GetView(base.Name).CanDelete;
				}
			}

			// Token: 0x17003EFE RID: 16126
			// (get) Token: 0x0600C3BC RID: 50108 RVA: 0x002BE23C File Offset: 0x002BC43C
			public override bool CanInsert
			{
				get
				{
					return this.DataSourceControl.GetView(base.Name).CanInsert;
				}
			}

			// Token: 0x17003EFF RID: 16127
			// (get) Token: 0x0600C3BD RID: 50109 RVA: 0x002BE254 File Offset: 0x002BC454
			public override bool CanPage
			{
				get
				{
					return this.DataSourceControl.GetView(base.Name).CanPage;
				}
			}

			// Token: 0x17003F00 RID: 16128
			// (get) Token: 0x0600C3BE RID: 50110 RVA: 0x002BE26C File Offset: 0x002BC46C
			public override bool CanRetrieveTotalRowCount
			{
				get
				{
					return this.DataSourceControl.GetView(base.Name).CanRetrieveTotalRowCount;
				}
			}

			// Token: 0x17003F01 RID: 16129
			// (get) Token: 0x0600C3BF RID: 50111 RVA: 0x002BE284 File Offset: 0x002BC484
			public override bool CanSort
			{
				get
				{
					return this.DataSourceControl.GetView(base.Name).CanSort;
				}
			}

			// Token: 0x17003F02 RID: 16130
			// (get) Token: 0x0600C3C0 RID: 50112 RVA: 0x002BE29C File Offset: 0x002BC49C
			public override bool CanUpdate
			{
				get
				{
					return this.DataSourceControl.GetView(base.Name).CanUpdate;
				}
			}

			// Token: 0x0600C3C1 RID: 50113 RVA: 0x002BE2B4 File Offset: 0x002BC4B4
			public override void Delete(IDictionary keys, IDictionary oldValues, DataSourceViewOperationCallback callback)
			{
				this.DataSourceControl.GetView(base.Name).Delete(keys, oldValues, callback);
			}

			// Token: 0x0600C3C2 RID: 50114 RVA: 0x002BE2CF File Offset: 0x002BC4CF
			public override bool Equals(object obj)
			{
				return this.DataSourceControl.GetView(base.Name).Equals(obj);
			}

			// Token: 0x0600C3C3 RID: 50115 RVA: 0x002BE2E8 File Offset: 0x002BC4E8
			public override int GetHashCode()
			{
				return this.DataSourceControl.GetView(base.Name).GetHashCode();
			}

			// Token: 0x0600C3C4 RID: 50116 RVA: 0x002BE300 File Offset: 0x002BC500
			public override void Insert(IDictionary values, DataSourceViewOperationCallback callback)
			{
				this.DataSourceControl.GetView(base.Name).Insert(values, callback);
			}

			// Token: 0x0600C3C5 RID: 50117 RVA: 0x002BE31C File Offset: 0x002BC51C
			public override void Select(DataSourceSelectArguments arguments, DataSourceViewSelectCallback callback)
			{
				IHierarchicalDataSource hierarchicalDataSource = this.DataSourceControl as IHierarchicalDataSource;
				if (hierarchicalDataSource != null)
				{
					callback(hierarchicalDataSource.GetHierarchicalView(string.Empty).Select());
					return;
				}
				this.DataSourceControl.GetView(base.Name).Select(arguments, callback);
			}

			// Token: 0x0600C3C6 RID: 50118 RVA: 0x002BE367 File Offset: 0x002BC567
			public override void Update(IDictionary keys, IDictionary values, IDictionary oldValues, DataSourceViewOperationCallback callback)
			{
				this.DataSourceControl.GetView(base.Name).Update(keys, values, oldValues, callback);
			}

			// Token: 0x040033DF RID: 13279
			private IDataSource DataSourceControl;
		}
	}
}
