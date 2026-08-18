using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200026E RID: 622
	public class SpotColor : ExtendedColor
	{
		// Token: 0x06001754 RID: 5972 RVA: 0x000861FC File Offset: 0x000851FC
		public SpotColor(PdfSpotColor spot, float tint) : base(3, ((float)spot.AlternativeCS.R / 255f - 1f) * tint + 1f, ((float)spot.AlternativeCS.G / 255f - 1f) * tint + 1f, ((float)spot.AlternativeCS.B / 255f - 1f) * tint + 1f)
		{
			this.spot = spot;
			this.tint = tint;
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x0008627E File Offset: 0x0008527E
		public PdfSpotColor PdfSpotColor
		{
			get
			{
				return this.spot;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x00086286 File Offset: 0x00085286
		public float Tint
		{
			get
			{
				return this.tint;
			}
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0008628E File Offset: 0x0008528E
		public override bool Equals(object obj)
		{
			return this == obj;
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x00086294 File Offset: 0x00085294
		public override int GetHashCode()
		{
			return this.spot.GetHashCode() ^ this.tint.GetHashCode();
		}

		// Token: 0x04001000 RID: 4096
		private PdfSpotColor spot;

		// Token: 0x04001001 RID: 4097
		private float tint;
	}
}
