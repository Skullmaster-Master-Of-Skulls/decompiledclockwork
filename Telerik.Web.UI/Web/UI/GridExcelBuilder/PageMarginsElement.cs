using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02000F7D RID: 3965
	public class PageMarginsElement : ElementBase
	{
		// Token: 0x17002FFA RID: 12282
		// (get) Token: 0x060097D8 RID: 38872 RVA: 0x0022048B File Offset: 0x0021E68B
		// (set) Token: 0x060097D9 RID: 38873 RVA: 0x00220493 File Offset: 0x0021E693
		public double Right
		{
			get
			{
				return this._right;
			}
			set
			{
				if (value < 0.0)
				{
					throw new ArgumentOutOfRangeException("Value", "Right margin cannot be less then 0");
				}
				this._right = value;
			}
		}

		// Token: 0x17002FFB RID: 12283
		// (get) Token: 0x060097DA RID: 38874 RVA: 0x002204B8 File Offset: 0x0021E6B8
		// (set) Token: 0x060097DB RID: 38875 RVA: 0x002204C0 File Offset: 0x0021E6C0
		public double Left
		{
			get
			{
				return this._left;
			}
			set
			{
				if (value < 0.0)
				{
					throw new ArgumentOutOfRangeException("Value", "Left margin cannot be less then 0");
				}
				this._left = value;
			}
		}

		// Token: 0x17002FFC RID: 12284
		// (get) Token: 0x060097DC RID: 38876 RVA: 0x002204E5 File Offset: 0x0021E6E5
		// (set) Token: 0x060097DD RID: 38877 RVA: 0x002204ED File Offset: 0x0021E6ED
		public double Top
		{
			get
			{
				return this._top;
			}
			set
			{
				if (value < 0.0)
				{
					throw new ArgumentOutOfRangeException("Value", "Top margin cannot be less then 0");
				}
				this._top = value;
			}
		}

		// Token: 0x17002FFD RID: 12285
		// (get) Token: 0x060097DE RID: 38878 RVA: 0x00220512 File Offset: 0x0021E712
		// (set) Token: 0x060097DF RID: 38879 RVA: 0x0022051A File Offset: 0x0021E71A
		public double Bottom
		{
			get
			{
				return this._bottom;
			}
			set
			{
				if (value < 0.0)
				{
					throw new ArgumentOutOfRangeException("Value", "Bottom margin cannot be less then 0");
				}
				this._bottom = value;
			}
		}

		// Token: 0x17002FFE RID: 12286
		// (get) Token: 0x060097E0 RID: 38880 RVA: 0x0022053F File Offset: 0x0021E73F
		protected override string EndTag
		{
			get
			{
				return "</x:PageMargins>";
			}
		}

		// Token: 0x17002FFF RID: 12287
		// (get) Token: 0x060097E1 RID: 38881 RVA: 0x00220546 File Offset: 0x0021E746
		protected override string StartTag
		{
			get
			{
				return "<x:PageMargins{0}>";
			}
		}

		// Token: 0x060097E2 RID: 38882 RVA: 0x00220550 File Offset: 0x0021E750
		protected override void AppendAttributes(StringBuilder sb)
		{
			if (this.Left != 1.0)
			{
				base.Attributes.Add("x:Left", this.Left.ToString());
			}
			if (this.Right != 1.0)
			{
				base.Attributes.Add("x:Right", this.Right.ToString());
			}
			if (this.Top != 1.0)
			{
				base.Attributes.Add("x:Top", this.Top.ToString());
			}
			if (this.Bottom != 1.0)
			{
				base.Attributes.Add("x:Bottom", this.Bottom.ToString());
			}
			base.AppendAttributes(sb);
		}

		// Token: 0x04002B62 RID: 11106
		private double _bottom = 1.0;

		// Token: 0x04002B63 RID: 11107
		private double _left = 1.0;

		// Token: 0x04002B64 RID: 11108
		private double _right = 1.0;

		// Token: 0x04002B65 RID: 11109
		private double _top = 1.0;
	}
}
