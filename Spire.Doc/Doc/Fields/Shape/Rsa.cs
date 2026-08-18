using System;

namespace Spire.Doc.Fields.Shape
{
	// Token: 0x0200006A RID: 106
	public class Rsa
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00007D94 File Offset: 0x00006D94
		public Rsa(byte[] modulusBytes, byte[] exponentBytes)
		{
			this.ᜀ = new spr\u17CD(modulusBytes);
			this.ᜁ = new spr\u17CD(exponentBytes);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00007DC0 File Offset: 0x00006DC0
		public byte[] Encrypt(byte[] inputBytes)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_78;
				case 1:
					num = 3;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						break;
					}
					break;
				case 3:
					if (inputBytes.Length < 1)
					{
						num = 0;
						continue;
					}
					goto IL_7A;
				}
				if (inputBytes == null)
				{
					break;
				}
				num = 1;
			}
			IL_5B:
			return new byte[0];
			IL_78:
			goto IL_5B;
			IL_7A:
			spr\u17CD spr_u17CD = new spr\u17CD(inputBytes);
			spr\u17CD spr_u17CD2 = spr_u17CD.ᜐ(this.ᜁ, this.ᜀ);
			byte[] result = spr_u17CD2.ᜂ();
			spr_u17CD.ᜅ();
			spr_u17CD2.ᜅ();
			return result;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00007E78 File Offset: 0x00006E78
		internal spr\u17CD Modulus
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00007EBC File Offset: 0x00006EBC
		internal spr\u17CD Exponent
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜁ;
			}
		}

		// Token: 0x040006A4 RID: 1700
		internal spr\u17CD ᜀ;

		// Token: 0x040006A5 RID: 1701
		private byte[] \u2609\u0084\u0082\u00A7;

		// Token: 0x040006A6 RID: 1702
		private float[] \u25D9\u0099\u0093\u00AE;

		// Token: 0x040006A7 RID: 1703
		private float[] \u2593\u00AD\u0098\u0096;

		// Token: 0x040006A8 RID: 1704
		internal spr\u17CD ᜁ;
	}
}
