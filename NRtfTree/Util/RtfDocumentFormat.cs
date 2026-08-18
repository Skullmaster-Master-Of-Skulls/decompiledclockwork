using System;

namespace Net.Sgoliver.NRtfTree.Util
{
	// Token: 0x0200000A RID: 10
	public class RtfDocumentFormat
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004CA1 File Offset: 0x00002EA1
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00004CA9 File Offset: 0x00002EA9
		public float MarginL
		{
			get
			{
				return this.marginl;
			}
			set
			{
				this.marginl = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004CB2 File Offset: 0x00002EB2
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00004CBA File Offset: 0x00002EBA
		public float MarginR
		{
			get
			{
				return this.marginr;
			}
			set
			{
				this.marginr = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004CC3 File Offset: 0x00002EC3
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00004CCB File Offset: 0x00002ECB
		public float MarginT
		{
			get
			{
				return this.margint;
			}
			set
			{
				this.margint = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004CD4 File Offset: 0x00002ED4
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00004CDC File Offset: 0x00002EDC
		public float MarginB
		{
			get
			{
				return this.marginb;
			}
			set
			{
				this.marginb = value;
			}
		}

		// Token: 0x04000035 RID: 53
		private float marginl = 2f;

		// Token: 0x04000036 RID: 54
		private float marginr = 2f;

		// Token: 0x04000037 RID: 55
		private float margint = 2f;

		// Token: 0x04000038 RID: 56
		private float marginb = 2f;
	}
}
