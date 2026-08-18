using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02000F79 RID: 3961
	public class PageFooterElement : ElementBase
	{
		// Token: 0x060097BE RID: 38846 RVA: 0x00220217 File Offset: 0x0021E417
		public PageFooterElement()
		{
			this.Data = string.Empty;
		}

		// Token: 0x17002FED RID: 12269
		// (get) Token: 0x060097BF RID: 38847 RVA: 0x00220239 File Offset: 0x0021E439
		// (set) Token: 0x060097C0 RID: 38848 RVA: 0x00220241 File Offset: 0x0021E441
		public string Data { get; set; }

		// Token: 0x17002FEE RID: 12270
		// (get) Token: 0x060097C1 RID: 38849 RVA: 0x0022024A File Offset: 0x0021E44A
		// (set) Token: 0x060097C2 RID: 38850 RVA: 0x00220252 File Offset: 0x0021E452
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

		// Token: 0x17002FEF RID: 12271
		// (get) Token: 0x060097C3 RID: 38851 RVA: 0x00220277 File Offset: 0x0021E477
		protected override string EndTag
		{
			get
			{
				return "</Footer>";
			}
		}

		// Token: 0x17002FF0 RID: 12272
		// (get) Token: 0x060097C4 RID: 38852 RVA: 0x0022027E File Offset: 0x0021E47E
		protected override string StartTag
		{
			get
			{
				return "<Footer{0}>";
			}
		}

		// Token: 0x060097C5 RID: 38853 RVA: 0x00220288 File Offset: 0x0021E488
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

		// Token: 0x04002B58 RID: 11096
		private double _margin = 0.5;
	}
}
