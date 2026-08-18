using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02000F7A RID: 3962
	public class PageHeaderElement : ElementBase
	{
		// Token: 0x060097C6 RID: 38854 RVA: 0x002202EE File Offset: 0x0021E4EE
		public PageHeaderElement()
		{
			this.Data = string.Empty;
		}

		// Token: 0x17002FF1 RID: 12273
		// (get) Token: 0x060097C7 RID: 38855 RVA: 0x00220310 File Offset: 0x0021E510
		// (set) Token: 0x060097C8 RID: 38856 RVA: 0x00220318 File Offset: 0x0021E518
		public string Data { get; set; }

		// Token: 0x17002FF2 RID: 12274
		// (get) Token: 0x060097C9 RID: 38857 RVA: 0x00220321 File Offset: 0x0021E521
		// (set) Token: 0x060097CA RID: 38858 RVA: 0x00220329 File Offset: 0x0021E529
		public double Margin
		{
			get
			{
				return this._margin;
			}
			set
			{
				if (value < 0.0)
				{
					throw new ArgumentOutOfRangeException("Value", "Margin cannot be less then 0");
				}
				this._margin = value;
			}
		}

		// Token: 0x17002FF3 RID: 12275
		// (get) Token: 0x060097CB RID: 38859 RVA: 0x0022034E File Offset: 0x0021E54E
		protected override string EndTag
		{
			get
			{
				return "</Header>";
			}
		}

		// Token: 0x17002FF4 RID: 12276
		// (get) Token: 0x060097CC RID: 38860 RVA: 0x00220355 File Offset: 0x0021E555
		protected override string StartTag
		{
			get
			{
				return "<Header{0}>";
			}
		}

		// Token: 0x060097CD RID: 38861 RVA: 0x0022035C File Offset: 0x0021E55C
		protected override void AppendAttributes(StringBuilder sb)
		{
			if (this.Margin != 0.5)
			{
				base.Attributes.Add("x:Margin", this.Margin.ToString());
			}
			if (!string.IsNullOrEmpty(this.Data))
			{
				base.Attributes.Add("x:Data", this.Data);
			}
			base.AppendAttributes(sb);
		}

		// Token: 0x04002B5A RID: 11098
		private double _margin = 0.5;
	}
}
