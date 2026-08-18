using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.Web.UI.ExportInfrastructure
{
	// Token: 0x02000A48 RID: 2632
	public class Cell
	{
		// Token: 0x1700217B RID: 8571
		// (get) Token: 0x060065B2 RID: 26034 RVA: 0x0017CF21 File Offset: 0x0017B121
		// (set) Token: 0x060065B3 RID: 26035 RVA: 0x0017CF29 File Offset: 0x0017B129
		public Table Table
		{
			get
			{
				return this._table;
			}
			internal set
			{
				this._table = value;
			}
		}

		// Token: 0x060065B4 RID: 26036 RVA: 0x0017CF32 File Offset: 0x0017B132
		public Cell(Table tbl)
		{
			this.Table = tbl;
		}

		// Token: 0x1700217C RID: 8572
		// (get) Token: 0x060065B5 RID: 26037 RVA: 0x0017CF50 File Offset: 0x0017B150
		// (set) Token: 0x060065B6 RID: 26038 RVA: 0x0017CF6C File Offset: 0x0017B16C
		public int RowIndex
		{
			get
			{
				return this.Index.Y;
			}
			internal set
			{
				this.Index = new Point(this.Index.Y, value);
			}
		}

		// Token: 0x1700217D RID: 8573
		// (get) Token: 0x060065B7 RID: 26039 RVA: 0x0017CF94 File Offset: 0x0017B194
		// (set) Token: 0x060065B8 RID: 26040 RVA: 0x0017CFB0 File Offset: 0x0017B1B0
		public int ColIndex
		{
			get
			{
				return this.Index.X;
			}
			internal set
			{
				this.Index = new Point(value, this.Index.X);
			}
		}

		// Token: 0x1700217E RID: 8574
		// (get) Token: 0x060065B9 RID: 26041 RVA: 0x0017CFD7 File Offset: 0x0017B1D7
		// (set) Token: 0x060065BA RID: 26042 RVA: 0x0017CFDF File Offset: 0x0017B1DF
		public Point Index
		{
			get
			{
				return this._index;
			}
			internal set
			{
				this._index = value;
			}
		}

		// Token: 0x1700217F RID: 8575
		// (get) Token: 0x060065BB RID: 26043 RVA: 0x0017CFE8 File Offset: 0x0017B1E8
		// (set) Token: 0x060065BC RID: 26044 RVA: 0x0017CFF0 File Offset: 0x0017B1F0
		public string Hyperlink
		{
			get
			{
				return this._hyperlink;
			}
			set
			{
				this._hyperlink = value;
			}
		}

		// Token: 0x17002180 RID: 8576
		// (get) Token: 0x060065BD RID: 26045 RVA: 0x0017CFF9 File Offset: 0x0017B1F9
		// (set) Token: 0x060065BE RID: 26046 RVA: 0x0017D014 File Offset: 0x0017B214
		public ExportStyle Style
		{
			get
			{
				if (this._style == null)
				{
					this._style = new ExportStyle();
				}
				return this._style;
			}
			set
			{
				this._style = value;
			}
		}

		// Token: 0x17002181 RID: 8577
		// (get) Token: 0x060065BF RID: 26047 RVA: 0x0017D01D File Offset: 0x0017B21D
		// (set) Token: 0x060065C0 RID: 26048 RVA: 0x0017D025 File Offset: 0x0017B225
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x17002182 RID: 8578
		// (get) Token: 0x060065C1 RID: 26049 RVA: 0x0017D02E File Offset: 0x0017B22E
		// (set) Token: 0x060065C2 RID: 26050 RVA: 0x0017D036 File Offset: 0x0017B236
		[DefaultValue(1)]
		public int Colspan
		{
			get
			{
				return this._colspan;
			}
			set
			{
				this._colspan = value;
			}
		}

		// Token: 0x17002183 RID: 8579
		// (get) Token: 0x060065C3 RID: 26051 RVA: 0x0017D03F File Offset: 0x0017B23F
		// (set) Token: 0x060065C4 RID: 26052 RVA: 0x0017D047 File Offset: 0x0017B247
		[DefaultValue(1)]
		public int Rowspan
		{
			get
			{
				return this._rowspan;
			}
			set
			{
				this._rowspan = value;
			}
		}

		// Token: 0x17002184 RID: 8580
		// (get) Token: 0x060065C5 RID: 26053 RVA: 0x0017D050 File Offset: 0x0017B250
		public string Text
		{
			get
			{
				if (this.Value != null)
				{
					return this.Value.ToString();
				}
				return "";
			}
		}

		// Token: 0x17002185 RID: 8581
		// (get) Token: 0x060065C6 RID: 26054 RVA: 0x0017D06B File Offset: 0x0017B26B
		// (set) Token: 0x060065C7 RID: 26055 RVA: 0x0017D073 File Offset: 0x0017B273
		public string Format
		{
			get
			{
				return this._format;
			}
			set
			{
				this._format = value;
			}
		}

		// Token: 0x17002186 RID: 8582
		// (get) Token: 0x060065C8 RID: 26056 RVA: 0x0017D07C File Offset: 0x0017B27C
		// (set) Token: 0x060065C9 RID: 26057 RVA: 0x0017D084 File Offset: 0x0017B284
		public double RotationAngle
		{
			get
			{
				return this._rotationAngle;
			}
			set
			{
				this._rotationAngle = value;
			}
		}

		// Token: 0x17002187 RID: 8583
		// (get) Token: 0x060065CA RID: 26058 RVA: 0x0017D08D File Offset: 0x0017B28D
		// (set) Token: 0x060065CB RID: 26059 RVA: 0x0017D095 File Offset: 0x0017B295
		public bool RTL
		{
			get
			{
				return this._rtl;
			}
			set
			{
				this._rtl = value;
			}
		}

		// Token: 0x17002188 RID: 8584
		// (get) Token: 0x060065CC RID: 26060 RVA: 0x0017D09E File Offset: 0x0017B29E
		// (set) Token: 0x060065CD RID: 26061 RVA: 0x0017D0A6 File Offset: 0x0017B2A6
		public bool TextWrap
		{
			get
			{
				return this._textWrap;
			}
			set
			{
				this._textWrap = value;
			}
		}

		// Token: 0x04001891 RID: 6289
		private object _value;

		// Token: 0x04001892 RID: 6290
		private int _rowspan = 1;

		// Token: 0x04001893 RID: 6291
		private int _colspan = 1;

		// Token: 0x04001894 RID: 6292
		private ExportStyle _style;

		// Token: 0x04001895 RID: 6293
		private Point _index;

		// Token: 0x04001896 RID: 6294
		private string _hyperlink;

		// Token: 0x04001897 RID: 6295
		private Table _table;

		// Token: 0x04001898 RID: 6296
		private string _format;

		// Token: 0x04001899 RID: 6297
		private double _rotationAngle;

		// Token: 0x0400189A RID: 6298
		private bool _rtl;

		// Token: 0x0400189B RID: 6299
		private bool _textWrap;
	}
}
