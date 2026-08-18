using System;

namespace System.Web.UI.Design
{
	// Token: 0x02000032 RID: 50
	public abstract class DesignerHierarchicalDataSourceView
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x0000CC03 File Offset: 0x0000AE03
		protected DesignerHierarchicalDataSourceView(IHierarchicalDataSourceDesigner owner, string viewPath)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			if (viewPath == null)
			{
				throw new ArgumentNullException("viewPath");
			}
			this._owner = owner;
			this._path = viewPath;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000CC35 File Offset: 0x0000AE35
		public IHierarchicalDataSourceDesigner DataSourceDesigner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000CC3D File Offset: 0x0000AE3D
		public string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00003598 File Offset: 0x00001798
		public virtual IDataSourceSchema Schema
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000CC45 File Offset: 0x0000AE45
		public virtual IHierarchicalEnumerable GetDesignTimeData(out bool isSampleData)
		{
			isSampleData = true;
			return null;
		}

		// Token: 0x0400011E RID: 286
		private string _path;

		// Token: 0x0400011F RID: 287
		private IHierarchicalDataSourceDesigner _owner;
	}
}
