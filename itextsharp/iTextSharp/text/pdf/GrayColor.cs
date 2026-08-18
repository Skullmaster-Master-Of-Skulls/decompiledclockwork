using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x020001CD RID: 461
	public class GrayColor : ExtendedColor
	{
		// Token: 0x06001207 RID: 4615 RVA: 0x00067C1F File Offset: 0x00066C1F
		public GrayColor(int intGray) : this((float)intGray / 255f)
		{
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00067C2F File Offset: 0x00066C2F
		public GrayColor(float floatGray) : base(1, floatGray, floatGray, floatGray)
		{
			this.cgray = ExtendedColor.Normalize(floatGray);
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x00067C47 File Offset: 0x00066C47
		public float Gray
		{
			get
			{
				return this.cgray;
			}
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00067C4F File Offset: 0x00066C4F
		public override bool Equals(object obj)
		{
			return obj is GrayColor && ((GrayColor)obj).cgray == this.cgray;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00067C6E File Offset: 0x00066C6E
		public override int GetHashCode()
		{
			return this.cgray.GetHashCode();
		}

		// Token: 0x04000CAF RID: 3247
		private float cgray;

		// Token: 0x04000CB0 RID: 3248
		public static readonly GrayColor GRAYBLACK = new GrayColor(0f);

		// Token: 0x04000CB1 RID: 3249
		public static readonly GrayColor GRAYWHITE = new GrayColor(1f);
	}
}
