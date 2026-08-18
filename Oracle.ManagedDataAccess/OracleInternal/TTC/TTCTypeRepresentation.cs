using System;

namespace OracleInternal.TTC
{
	// Token: 0x0200023A RID: 570
	internal class TTCTypeRepresentation
	{
		// Token: 0x060014C5 RID: 5317 RVA: 0x000DF314 File Offset: 0x000DD514
		internal TTCTypeRepresentation()
		{
			this.m_representationArray = new byte[5];
			this.m_representationArray[0] = 0;
			this.m_representationArray[1] = 1;
			this.m_representationArray[2] = 1;
			this.m_representationArray[3] = 1;
			this.m_representationArray[4] = 1;
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x000DF360 File Offset: 0x000DD560
		// (set) Token: 0x060014C7 RID: 5319 RVA: 0x000DF368 File Offset: 0x000DD568
		internal byte ServerConversionFlags
		{
			get
			{
				return this.m_serverConvFlags;
			}
			set
			{
				this.m_serverConvFlags = value;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x000DF374 File Offset: 0x000DD574
		internal bool ConversionRequired
		{
			get
			{
				return (this.m_serverConvFlags & 2) > 0;
			}
		}

		// Token: 0x0400191F RID: 6431
		internal const byte NATIVE = 0;

		// Token: 0x04001920 RID: 6432
		internal const byte UNIVERSAL = 1;

		// Token: 0x04001921 RID: 6433
		internal const byte LSB = 2;

		// Token: 0x04001922 RID: 6434
		internal const byte MAXREP = 3;

		// Token: 0x04001923 RID: 6435
		internal const byte B1 = 0;

		// Token: 0x04001924 RID: 6436
		internal const byte B2 = 1;

		// Token: 0x04001925 RID: 6437
		internal const byte B4 = 2;

		// Token: 0x04001926 RID: 6438
		internal const byte B8 = 3;

		// Token: 0x04001927 RID: 6439
		internal const byte PTR = 4;

		// Token: 0x04001928 RID: 6440
		internal const byte MAXTYPE = 4;

		// Token: 0x04001929 RID: 6441
		internal const byte REPUNV = 1;

		// Token: 0x0400192A RID: 6442
		internal const byte REPBUNV = 1;

		// Token: 0x0400192B RID: 6443
		internal const byte REPCUNV = 1;

		// Token: 0x0400192C RID: 6444
		internal const byte REPIUNV = 1;

		// Token: 0x0400192D RID: 6445
		internal const byte REPNV51 = 10;

		// Token: 0x0400192E RID: 6446
		internal const byte REPDV51 = 10;

		// Token: 0x0400192F RID: 6447
		internal const byte REPAUNV = 1;

		// Token: 0x04001930 RID: 6448
		internal const byte REPRUNV = 1;

		// Token: 0x04001931 RID: 6449
		internal const byte NUMREPS = 5;

		// Token: 0x04001932 RID: 6450
		internal byte[] m_representationArray;

		// Token: 0x04001933 RID: 6451
		internal byte m_serverConvFlags;
	}
}
