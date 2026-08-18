using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000317 RID: 791
	[TypeConverter(typeof(PaddingConverter))]
	[Serializable]
	public struct Padding
	{
		// Token: 0x06003236 RID: 12854 RVA: 0x000E1D1C File Offset: 0x000DFF1C
		public Padding(int all)
		{
			this._all = true;
			this._bottom = all;
			this._right = all;
			this._left = all;
			this._top = all;
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x000E1D54 File Offset: 0x000DFF54
		public Padding(int left, int top, int right, int bottom)
		{
			this._top = top;
			this._left = left;
			this._right = right;
			this._bottom = bottom;
			this._all = (this._top == this._left && this._top == this._right && this._top == this._bottom);
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06003238 RID: 12856 RVA: 0x000E1DB1 File Offset: 0x000DFFB1
		// (set) Token: 0x06003239 RID: 12857 RVA: 0x000E1DC4 File Offset: 0x000DFFC4
		[RefreshProperties(RefreshProperties.All)]
		public int All
		{
			get
			{
				if (!this._all)
				{
					return -1;
				}
				return this._top;
			}
			set
			{
				if (!this._all || this._top != value)
				{
					this._all = true;
					this._bottom = value;
					this._right = value;
					this._left = value;
					this._top = value;
				}
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x0600323A RID: 12858 RVA: 0x000E1E0B File Offset: 0x000E000B
		// (set) Token: 0x0600323B RID: 12859 RVA: 0x000E1E22 File Offset: 0x000E0022
		[RefreshProperties(RefreshProperties.All)]
		public int Bottom
		{
			get
			{
				if (this._all)
				{
					return this._top;
				}
				return this._bottom;
			}
			set
			{
				if (this._all || this._bottom != value)
				{
					this._all = false;
					this._bottom = value;
				}
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x0600323C RID: 12860 RVA: 0x000E1E43 File Offset: 0x000E0043
		// (set) Token: 0x0600323D RID: 12861 RVA: 0x000E1E5A File Offset: 0x000E005A
		[RefreshProperties(RefreshProperties.All)]
		public int Left
		{
			get
			{
				if (this._all)
				{
					return this._top;
				}
				return this._left;
			}
			set
			{
				if (this._all || this._left != value)
				{
					this._all = false;
					this._left = value;
				}
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x0600323E RID: 12862 RVA: 0x000E1E7B File Offset: 0x000E007B
		// (set) Token: 0x0600323F RID: 12863 RVA: 0x000E1E92 File Offset: 0x000E0092
		[RefreshProperties(RefreshProperties.All)]
		public int Right
		{
			get
			{
				if (this._all)
				{
					return this._top;
				}
				return this._right;
			}
			set
			{
				if (this._all || this._right != value)
				{
					this._all = false;
					this._right = value;
				}
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06003240 RID: 12864 RVA: 0x000E1EB3 File Offset: 0x000E00B3
		// (set) Token: 0x06003241 RID: 12865 RVA: 0x000E1EBB File Offset: 0x000E00BB
		[RefreshProperties(RefreshProperties.All)]
		public int Top
		{
			get
			{
				return this._top;
			}
			set
			{
				if (this._all || this._top != value)
				{
					this._all = false;
					this._top = value;
				}
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06003242 RID: 12866 RVA: 0x000E1EDC File Offset: 0x000E00DC
		[Browsable(false)]
		public int Horizontal
		{
			get
			{
				return this.Left + this.Right;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06003243 RID: 12867 RVA: 0x000E1EEB File Offset: 0x000E00EB
		[Browsable(false)]
		public int Vertical
		{
			get
			{
				return this.Top + this.Bottom;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06003244 RID: 12868 RVA: 0x000E1EFA File Offset: 0x000E00FA
		[Browsable(false)]
		public Size Size
		{
			get
			{
				return new Size(this.Horizontal, this.Vertical);
			}
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x000E1F0D File Offset: 0x000E010D
		public static Padding Add(Padding p1, Padding p2)
		{
			return p1 + p2;
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000E1F16 File Offset: 0x000E0116
		public static Padding Subtract(Padding p1, Padding p2)
		{
			return p1 - p2;
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000E1F1F File Offset: 0x000E011F
		public override bool Equals(object other)
		{
			return other is Padding && (Padding)other == this;
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000E1F3C File Offset: 0x000E013C
		public static Padding operator +(Padding p1, Padding p2)
		{
			return new Padding(p1.Left + p2.Left, p1.Top + p2.Top, p1.Right + p2.Right, p1.Bottom + p2.Bottom);
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x000E1F8C File Offset: 0x000E018C
		public static Padding operator -(Padding p1, Padding p2)
		{
			return new Padding(p1.Left - p2.Left, p1.Top - p2.Top, p1.Right - p2.Right, p1.Bottom - p2.Bottom);
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x000E1FDC File Offset: 0x000E01DC
		public static bool operator ==(Padding p1, Padding p2)
		{
			return p1.Left == p2.Left && p1.Top == p2.Top && p1.Right == p2.Right && p1.Bottom == p2.Bottom;
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000E202B File Offset: 0x000E022B
		public static bool operator !=(Padding p1, Padding p2)
		{
			return !(p1 == p2);
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x000E2037 File Offset: 0x000E0237
		public override int GetHashCode()
		{
			return this.Left ^ WindowsFormsUtils.RotateLeft(this.Top, 8) ^ WindowsFormsUtils.RotateLeft(this.Right, 16) ^ WindowsFormsUtils.RotateLeft(this.Bottom, 24);
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x000E2068 File Offset: 0x000E0268
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{Left=",
				this.Left.ToString(CultureInfo.CurrentCulture),
				",Top=",
				this.Top.ToString(CultureInfo.CurrentCulture),
				",Right=",
				this.Right.ToString(CultureInfo.CurrentCulture),
				",Bottom=",
				this.Bottom.ToString(CultureInfo.CurrentCulture),
				"}"
			});
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x000E2101 File Offset: 0x000E0301
		private void ResetAll()
		{
			this.All = 0;
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x000E210A File Offset: 0x000E030A
		private void ResetBottom()
		{
			this.Bottom = 0;
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x000E2113 File Offset: 0x000E0313
		private void ResetLeft()
		{
			this.Left = 0;
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x000E211C File Offset: 0x000E031C
		private void ResetRight()
		{
			this.Right = 0;
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x000E2125 File Offset: 0x000E0325
		private void ResetTop()
		{
			this.Top = 0;
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x000E2130 File Offset: 0x000E0330
		internal void Scale(float dx, float dy)
		{
			this._top = (int)((float)this._top * dy);
			this._left = (int)((float)this._left * dx);
			this._right = (int)((float)this._right * dx);
			this._bottom = (int)((float)this._bottom * dy);
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x000E217D File Offset: 0x000E037D
		internal bool ShouldSerializeAll()
		{
			return this._all;
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x000E2185 File Offset: 0x000E0385
		[Conditional("DEBUG")]
		private void Debug_SanityCheck()
		{
			bool all = this._all;
		}

		// Token: 0x04001E72 RID: 7794
		private bool _all;

		// Token: 0x04001E73 RID: 7795
		private int _top;

		// Token: 0x04001E74 RID: 7796
		private int _left;

		// Token: 0x04001E75 RID: 7797
		private int _right;

		// Token: 0x04001E76 RID: 7798
		private int _bottom;

		// Token: 0x04001E77 RID: 7799
		public static readonly Padding Empty = new Padding(0);
	}
}
