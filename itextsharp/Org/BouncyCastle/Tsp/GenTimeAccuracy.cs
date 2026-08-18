using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Tsp;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x02000297 RID: 663
	public class GenTimeAccuracy
	{
		// Token: 0x060018FE RID: 6398 RVA: 0x00092EFE File Offset: 0x00091EFE
		public GenTimeAccuracy(Accuracy accuracy)
		{
			this.accuracy = accuracy;
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x00092F0D File Offset: 0x00091F0D
		public int Seconds
		{
			get
			{
				return this.GetTimeComponent(this.accuracy.Seconds);
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x00092F20 File Offset: 0x00091F20
		public int Millis
		{
			get
			{
				return this.GetTimeComponent(this.accuracy.Millis);
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x00092F33 File Offset: 0x00091F33
		public int Micros
		{
			get
			{
				return this.GetTimeComponent(this.accuracy.Micros);
			}
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00092F46 File Offset: 0x00091F46
		private int GetTimeComponent(DerInteger time)
		{
			if (time != null)
			{
				return time.Value.IntValue;
			}
			return 0;
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x00092F58 File Offset: 0x00091F58
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Seconds,
				".",
				this.Millis.ToString("000"),
				this.Micros.ToString("000")
			});
		}

		// Token: 0x040010E5 RID: 4325
		private Accuracy accuracy;
	}
}
