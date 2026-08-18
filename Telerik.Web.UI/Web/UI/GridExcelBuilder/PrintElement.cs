using System;
using System.Drawing.Printing;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02000B79 RID: 2937
	public class PrintElement : ElementBase
	{
		// Token: 0x17002464 RID: 9316
		// (get) Token: 0x06006EEA RID: 28394 RVA: 0x0019B969 File Offset: 0x00199B69
		// (set) Token: 0x06006EEB RID: 28395 RVA: 0x0019B974 File Offset: 0x00199B74
		public int? FitHeight
		{
			get
			{
				return this._fitHeight;
			}
			set
			{
				if (value != null && value < 0)
				{
					throw new ArgumentOutOfRangeException("FitHeight cannot be less then 0");
				}
				this._fitHeight = value;
			}
		}

		// Token: 0x17002465 RID: 9317
		// (get) Token: 0x06006EEC RID: 28396 RVA: 0x0019B9B4 File Offset: 0x00199BB4
		// (set) Token: 0x06006EED RID: 28397 RVA: 0x0019B9DC File Offset: 0x00199BDC
		public PaperKind PaperSize
		{
			get
			{
				int? paperSizeIndex = this._paperSizeIndex;
				if (paperSizeIndex == null)
				{
					return PaperKind.Letter;
				}
				return (PaperKind)paperSizeIndex.GetValueOrDefault();
			}
			set
			{
				Array values = Enum.GetValues(typeof(PaperKind));
				if (value < PaperKind.Custom || value >= (PaperKind)values.Length)
				{
					throw new ArgumentOutOfRangeException("Invalid PaperKind value");
				}
				this._paperSizeIndex = new int?((int)value);
			}
		}

		// Token: 0x17002466 RID: 9318
		// (get) Token: 0x06006EEE RID: 28398 RVA: 0x0019BA1D File Offset: 0x00199C1D
		protected override string EndTag
		{
			get
			{
				return "</Print>";
			}
		}

		// Token: 0x17002467 RID: 9319
		// (get) Token: 0x06006EEF RID: 28399 RVA: 0x0019BA24 File Offset: 0x00199C24
		protected override string StartTag
		{
			get
			{
				return "<Print {0}>";
			}
		}

		// Token: 0x06006EF0 RID: 28400 RVA: 0x0019BA2C File Offset: 0x00199C2C
		protected override void RenderChildElements(StringBuilder sb)
		{
			sb.Append("<ValidPrinterInfo/>");
			if (this.FitHeight != null && this.FitHeight >= 0)
			{
				sb.Append(string.Format("<FitHeight>{0}</FitHeight>", this.FitHeight));
			}
			if (this._paperSizeIndex != null)
			{
				sb.Append(string.Format("<PaperSizeIndex>{0}</PaperSizeIndex>", this._paperSizeIndex));
			}
			base.RenderChildElements(sb);
		}

		// Token: 0x04001DEC RID: 7660
		private int? _fitHeight;

		// Token: 0x04001DED RID: 7661
		private int? _paperSizeIndex;
	}
}
