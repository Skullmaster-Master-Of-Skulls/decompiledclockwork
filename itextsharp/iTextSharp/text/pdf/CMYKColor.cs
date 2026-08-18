using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200041E RID: 1054
	public class CMYKColor : ExtendedColor
	{
		// Token: 0x060023D4 RID: 9172 RVA: 0x000DAEE9 File Offset: 0x000D9EE9
		public CMYKColor(int intCyan, int intMagenta, int intYellow, int intBlack) : this((float)intCyan / 255f, (float)intMagenta / 255f, (float)intYellow / 255f, (float)intBlack / 255f)
		{
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000DAF14 File Offset: 0x000D9F14
		public CMYKColor(float floatCyan, float floatMagenta, float floatYellow, float floatBlack) : base(2, 1f - floatCyan - floatBlack, 1f - floatMagenta - floatBlack, 1f - floatYellow - floatBlack)
		{
			this.ccyan = ExtendedColor.Normalize(floatCyan);
			this.cmagenta = ExtendedColor.Normalize(floatMagenta);
			this.cyellow = ExtendedColor.Normalize(floatYellow);
			this.cblack = ExtendedColor.Normalize(floatBlack);
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x000DAF77 File Offset: 0x000D9F77
		public float Cyan
		{
			get
			{
				return this.ccyan;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060023D7 RID: 9175 RVA: 0x000DAF7F File Offset: 0x000D9F7F
		public float Magenta
		{
			get
			{
				return this.cmagenta;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x000DAF87 File Offset: 0x000D9F87
		public float Yellow
		{
			get
			{
				return this.cyellow;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060023D9 RID: 9177 RVA: 0x000DAF8F File Offset: 0x000D9F8F
		public float Black
		{
			get
			{
				return this.cblack;
			}
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000DAF98 File Offset: 0x000D9F98
		public override bool Equals(object obj)
		{
			if (!(obj is CMYKColor))
			{
				return false;
			}
			CMYKColor cmykcolor = (CMYKColor)obj;
			return this.ccyan == cmykcolor.ccyan && this.cmagenta == cmykcolor.cmagenta && this.cyellow == cmykcolor.cyellow && this.cblack == cmykcolor.cblack;
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000DAFF0 File Offset: 0x000D9FF0
		public override int GetHashCode()
		{
			return this.ccyan.GetHashCode() ^ this.cmagenta.GetHashCode() ^ this.cyellow.GetHashCode() ^ this.cblack.GetHashCode();
		}

		// Token: 0x040018A7 RID: 6311
		private float ccyan;

		// Token: 0x040018A8 RID: 6312
		private float cmagenta;

		// Token: 0x040018A9 RID: 6313
		private float cyellow;

		// Token: 0x040018AA RID: 6314
		private float cblack;
	}
}
