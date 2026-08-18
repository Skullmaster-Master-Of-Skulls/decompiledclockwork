using System;

namespace System.Windows.Forms
{
	// Token: 0x02000397 RID: 919
	public class RowStyle : TableLayoutStyle
	{
		// Token: 0x06003C20 RID: 15392 RVA: 0x00106D44 File Offset: 0x00104F44
		public RowStyle()
		{
		}

		// Token: 0x06003C21 RID: 15393 RVA: 0x00106D4C File Offset: 0x00104F4C
		public RowStyle(SizeType sizeType)
		{
			base.SizeType = sizeType;
		}

		// Token: 0x06003C22 RID: 15394 RVA: 0x00106D82 File Offset: 0x00104F82
		public RowStyle(SizeType sizeType, float height)
		{
			base.SizeType = sizeType;
			this.Height = height;
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06003C23 RID: 15395 RVA: 0x00106D71 File Offset: 0x00104F71
		// (set) Token: 0x06003C24 RID: 15396 RVA: 0x00106D79 File Offset: 0x00104F79
		public float Height
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
