using System;
using System.Drawing;

namespace Telerik.Web.UI.ExportInfrastructure
{
	// Token: 0x02000A56 RID: 2646
	public class ExportStructure : IDisposable
	{
		// Token: 0x06006699 RID: 26265 RVA: 0x00180743 File Offset: 0x0017E943
		public ExportStructure()
		{
			this._tables = new TableCollection();
		}

		// Token: 0x0600669A RID: 26266 RVA: 0x00180756 File Offset: 0x0017E956
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600669B RID: 26267 RVA: 0x00180765 File Offset: 0x0017E965
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._defaultFont != null)
			{
				this._defaultFont.Dispose();
			}
		}

		// Token: 0x170021C4 RID: 8644
		// (get) Token: 0x0600669C RID: 26268 RVA: 0x0018077D File Offset: 0x0017E97D
		// (set) Token: 0x0600669D RID: 26269 RVA: 0x00180785 File Offset: 0x0017E985
		public TableCollection Tables
		{
			get
			{
				return this._tables;
			}
			set
			{
				this._tables = value;
			}
		}

		// Token: 0x170021C5 RID: 8645
		// (get) Token: 0x0600669E RID: 26270 RVA: 0x00180790 File Offset: 0x0017E990
		internal Font DefaultFont
		{
			get
			{
				if (this._defaultFont == null)
				{
					this._defaultFont = new Font("Arial", 10f);
				}
				if (this._defaultFont.Size <= 0f)
				{
					this._defaultFont = new Font(this._defaultFont.FontFamily.Name, 10f);
				}
				return this._defaultFont;
			}
		}

		// Token: 0x170021C6 RID: 8646
		// (get) Token: 0x0600669F RID: 26271 RVA: 0x001807F2 File Offset: 0x0017E9F2
		// (set) Token: 0x060066A0 RID: 26272 RVA: 0x001807FA File Offset: 0x0017E9FA
		public ExportUnitType ColumnWidthUnit
		{
			get
			{
				return this.columnWidthUnit;
			}
			set
			{
				this.columnWidthUnit = value;
			}
		}

		// Token: 0x170021C7 RID: 8647
		// (get) Token: 0x060066A1 RID: 26273 RVA: 0x00180803 File Offset: 0x0017EA03
		// (set) Token: 0x060066A2 RID: 26274 RVA: 0x0018080B File Offset: 0x0017EA0B
		public ExportUnitType RowHeightUnit
		{
			get
			{
				return this.rowHeightUnit;
			}
			set
			{
				this.rowHeightUnit = value;
			}
		}

		// Token: 0x040018E7 RID: 6375
		private const double pointsPerCm = 28.346456693;

		// Token: 0x040018E8 RID: 6376
		internal const float defaultCharWidth = 6f;

		// Token: 0x040018E9 RID: 6377
		internal const float defaultCharHeight = 10f;

		// Token: 0x040018EA RID: 6378
		private TableCollection _tables;

		// Token: 0x040018EB RID: 6379
		private ExportUnitType columnWidthUnit;

		// Token: 0x040018EC RID: 6380
		private ExportUnitType rowHeightUnit;

		// Token: 0x040018ED RID: 6381
		private Font _defaultFont;
	}
}
