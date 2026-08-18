using System;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000175 RID: 373
	public class CreateParams
	{
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x0004155D File Offset: 0x0003F75D
		// (set) Token: 0x0600138C RID: 5004 RVA: 0x00041565 File Offset: 0x0003F765
		public string ClassName
		{
			get
			{
				return this.className;
			}
			set
			{
				this.className = value;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x0004156E File Offset: 0x0003F76E
		// (set) Token: 0x0600138E RID: 5006 RVA: 0x00041576 File Offset: 0x0003F776
		public string Caption
		{
			get
			{
				return this.caption;
			}
			set
			{
				this.caption = value;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x0004157F File Offset: 0x0003F77F
		// (set) Token: 0x06001390 RID: 5008 RVA: 0x00041587 File Offset: 0x0003F787
		public int Style
		{
			get
			{
				return this.style;
			}
			set
			{
				this.style = value;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x00041590 File Offset: 0x0003F790
		// (set) Token: 0x06001392 RID: 5010 RVA: 0x00041598 File Offset: 0x0003F798
		public int ExStyle
		{
			get
			{
				return this.exStyle;
			}
			set
			{
				this.exStyle = value;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x000415A1 File Offset: 0x0003F7A1
		// (set) Token: 0x06001394 RID: 5012 RVA: 0x000415A9 File Offset: 0x0003F7A9
		public int ClassStyle
		{
			get
			{
				return this.classStyle;
			}
			set
			{
				this.classStyle = value;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x000415B2 File Offset: 0x0003F7B2
		// (set) Token: 0x06001396 RID: 5014 RVA: 0x000415BA File Offset: 0x0003F7BA
		public int X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x000415C3 File Offset: 0x0003F7C3
		// (set) Token: 0x06001398 RID: 5016 RVA: 0x000415CB File Offset: 0x0003F7CB
		public int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x000415D4 File Offset: 0x0003F7D4
		// (set) Token: 0x0600139A RID: 5018 RVA: 0x000415DC File Offset: 0x0003F7DC
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x000415E5 File Offset: 0x0003F7E5
		// (set) Token: 0x0600139C RID: 5020 RVA: 0x000415ED File Offset: 0x0003F7ED
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x000415F6 File Offset: 0x0003F7F6
		// (set) Token: 0x0600139E RID: 5022 RVA: 0x000415FE File Offset: 0x0003F7FE
		public IntPtr Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x00041607 File Offset: 0x0003F807
		// (set) Token: 0x060013A0 RID: 5024 RVA: 0x0004160F File Offset: 0x0003F80F
		public object Param
		{
			get
			{
				return this.param;
			}
			set
			{
				this.param = value;
			}
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00041618 File Offset: 0x0003F818
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("CreateParams {'");
			stringBuilder.Append(this.className);
			stringBuilder.Append("', '");
			stringBuilder.Append(this.caption);
			stringBuilder.Append("', 0x");
			stringBuilder.Append(Convert.ToString(this.style, 16));
			stringBuilder.Append(", 0x");
			stringBuilder.Append(Convert.ToString(this.exStyle, 16));
			stringBuilder.Append(", {");
			stringBuilder.Append(this.x);
			stringBuilder.Append(", ");
			stringBuilder.Append(this.y);
			stringBuilder.Append(", ");
			stringBuilder.Append(this.width);
			stringBuilder.Append(", ");
			stringBuilder.Append(this.height);
			stringBuilder.Append("}");
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x0400093D RID: 2365
		private string className;

		// Token: 0x0400093E RID: 2366
		private string caption;

		// Token: 0x0400093F RID: 2367
		private int style;

		// Token: 0x04000940 RID: 2368
		private int exStyle;

		// Token: 0x04000941 RID: 2369
		private int classStyle;

		// Token: 0x04000942 RID: 2370
		private int x;

		// Token: 0x04000943 RID: 2371
		private int y;

		// Token: 0x04000944 RID: 2372
		private int width;

		// Token: 0x04000945 RID: 2373
		private int height;

		// Token: 0x04000946 RID: 2374
		private IntPtr parent;

		// Token: 0x04000947 RID: 2375
		private object param;
	}
}
