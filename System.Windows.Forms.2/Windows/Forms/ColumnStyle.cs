using System;

namespace System.Windows.Forms
{
	// Token: 0x02000396 RID: 918
	public class ColumnStyle : TableLayoutStyle
	{
		// Token: 0x06003C1B RID: 15387 RVA: 0x00106D44 File Offset: 0x00104F44
		public ColumnStyle()
		{
		}

		// Token: 0x06003C1C RID: 15388 RVA: 0x00106D4C File Offset: 0x00104F4C
		public ColumnStyle(SizeType sizeType)
		{
			base.SizeType = sizeType;
		}

		// Token: 0x06003C1D RID: 15389 RVA: 0x00106D5B File Offset: 0x00104F5B
		public ColumnStyle(SizeType sizeType, float width)
		{
			base.SizeType = sizeType;
			this.Width = width;
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x00106D71 File Offset: 0x00104F71
		// (set) Token: 0x06003C1F RID: 15391 RVA: 0x00106D79 File Offset: 0x00104F79
		public float Width
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}
	}
}
