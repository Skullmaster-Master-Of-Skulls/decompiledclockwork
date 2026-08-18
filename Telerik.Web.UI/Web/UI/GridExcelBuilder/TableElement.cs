using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B2C RID: 6956
	public class TableElement : ElementBase
	{
		// Token: 0x17005204 RID: 20996
		// (get) Token: 0x06010D4C RID: 68940 RVA: 0x003BBF98 File Offset: 0x003BA198
		public IColumnsCollection Columns
		{
			get
			{
				if (this._columns == null)
				{
					this._columns = new ColumnsCollection();
				}
				return this._columns;
			}
		}

		// Token: 0x17005205 RID: 20997
		// (get) Token: 0x06010D4D RID: 68941 RVA: 0x003BBFB3 File Offset: 0x003BA1B3
		public IRowsCollection Rows
		{
			get
			{
				if (this._rows == null)
				{
					this._rows = new RowsCollection();
				}
				return this._rows;
			}
		}

		// Token: 0x17005206 RID: 20998
		// (get) Token: 0x06010D4E RID: 68942 RVA: 0x003BBFCE File Offset: 0x003BA1CE
		protected override string StartTag
		{
			get
			{
				return "<Table{0}>";
			}
		}

		// Token: 0x17005207 RID: 20999
		// (get) Token: 0x06010D4F RID: 68943 RVA: 0x003BBFD5 File Offset: 0x003BA1D5
		protected override string EndTag
		{
			get
			{
				return "</Table>";
			}
		}

		// Token: 0x06010D50 RID: 68944 RVA: 0x003BBFDC File Offset: 0x003BA1DC
		protected override void RenderChildElements(StringBuilder sb)
		{
			if (this.Columns.Count > 0)
			{
				foreach (object obj in this.Columns)
				{
					ColumnElement columnElement = (ColumnElement)obj;
					((IElement)columnElement).Render(sb);
				}
			}
			if (this.Rows.Count > 0)
			{
				foreach (object obj2 in this.Rows)
				{
					RowElement rowElement = (RowElement)obj2;
					((IElement)rowElement).Render(sb);
				}
			}
			base.RenderChildElements(sb);
		}

		// Token: 0x04004B43 RID: 19267
		private IRowsCollection _rows;

		// Token: 0x04004B44 RID: 19268
		private IColumnsCollection _columns;
	}
}
