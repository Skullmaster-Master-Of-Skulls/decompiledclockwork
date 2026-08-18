using System;
using System.Collections;

namespace System.Web.UI.Design
{
	// Token: 0x02000031 RID: 49
	public abstract class DesignerDataSourceView
	{
		// Token: 0x0600019B RID: 411 RVA: 0x0000CBB1 File Offset: 0x0000ADB1
		protected DesignerDataSourceView(IDataSourceDesigner owner, string viewName)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			if (viewName == null)
			{
				throw new ArgumentNullException("viewName");
			}
			this._owner = owner;
			this._name = viewName;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual bool CanDelete
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual bool CanInsert
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual bool CanPage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual bool CanRetrieveTotalRowCount
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual bool CanSort
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000445B File Offset: 0x0000265B
		public virtual bool CanUpdate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000CBE3 File Offset: 0x0000ADE3
		public IDataSourceDesigner DataSourceDesigner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000CBEB File Offset: 0x0000ADEB
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00003598 File Offset: 0x00001798
		public virtual IDataSourceViewSchema Schema
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000CBF3 File Offset: 0x0000ADF3
		public virtual IEnumerable GetDesignTimeData(int minimumRows, out bool isSampleData)
		{
			isSampleData = true;
			return DesignTimeData.GetDesignTimeDataSource(DesignTimeData.CreateDummyDataBoundDataTable(), minimumRows);
		}

		// Token: 0x0400011C RID: 284
		private string _name;

		// Token: 0x0400011D RID: 285
		private IDataSourceDesigner _owner;
	}
}
