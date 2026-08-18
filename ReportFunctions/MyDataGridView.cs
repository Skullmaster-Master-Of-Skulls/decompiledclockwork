using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReportFunctions
{
	// Token: 0x0200002F RID: 47
	public class MyDataGridView : DataGridView
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0003CFA4 File Offset: 0x0003BFA4
		public MyDataGridView()
		{
			Bitmap bitmap = new Bitmap(16, 16);
			this.img = bitmap;
			Graphics graphics = Graphics.FromImage(this.img);
			using (Brush brush = new SolidBrush(SystemColors.ControlText))
			{
				graphics.DrawString("•", this.Font, brush, 2f, 2f);
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0003D024 File Offset: 0x0003C024
		protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
		{
			if (e.DesiredType == typeof(Image))
			{
				e.Value = this.img;
				e.FormattingApplied = true;
			}
			else
			{
				base.OnCellFormatting(e);
			}
		}

		// Token: 0x04000170 RID: 368
		private Image img;

		// Token: 0x02000030 RID: 48
		private class VirtualDataGridViewColumn : DataGridViewColumn
		{
			// Token: 0x06000303 RID: 771 RVA: 0x0003D06C File Offset: 0x0003C06C
			public VirtualDataGridViewColumn(DataGridViewColumn originalColumn)
			{
				this.originalColumn = originalColumn;
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x06000304 RID: 772 RVA: 0x0003D080 File Offset: 0x0003C080
			public DataGridViewColumn OriginalColumn
			{
				get
				{
					return this.originalColumn;
				}
			}

			// Token: 0x04000171 RID: 369
			private DataGridViewColumn originalColumn;
		}
	}
}
