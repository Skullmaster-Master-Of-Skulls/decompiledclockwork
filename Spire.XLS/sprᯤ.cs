using System;
using System.IO;
using System.Text;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002DE RID: 734
internal class sprᯤ : spr\u20AE
{
	// Token: 0x06002CFE RID: 11518 RVA: 0x00194B28 File Offset: 0x00193B28
	internal override int ᜀ()
	{
		int num = 1;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					this.ᜄ = 93;
					num = 2;
					continue;
				case 2:
					goto IL_6C;
				}
				break;
			}
			IL_4A:
			if (this.ᜄ == 0)
			{
				num = 0;
				continue;
			}
			break;
			goto IL_4A;
		}
		IL_6C:
		return this.ᜄ;
	}

	// Token: 0x06002CFF RID: 11519 RVA: 0x00194BAC File Offset: 0x00193BAC
	internal string ᜂ()
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
		return this.ᜆ;
	}

	// Token: 0x06002D00 RID: 11520 RVA: 0x00194BF0 File Offset: 0x00193BF0
	internal new string ᜁ()
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
		return this.ᜈ;
	}

	// Token: 0x06002D01 RID: 11521 RVA: 0x00194C34 File Offset: 0x00193C34
	internal sprᯤ(spr\u1FDC A_0)
	{
		this.ᜆ = string.Empty;
		this.ᜇ = string.Empty;
		this.ᜈ = string.Empty;
		this.ᜉ = 1907505652U;
		this.ᜊ = string.Empty;
		this.ᜋ = string.Empty;
		this.ᜌ = string.Empty;
		base..ctor();
		byte[] array = new byte[A_0.Length];
		A_0.Read(array, 0, array.Length);
		this.ᜁ(array, 0);
	}

	// Token: 0x06002D02 RID: 11522 RVA: 0x00194CB8 File Offset: 0x00193CB8
	internal sprᯤ()
	{
		int a_ = 19;
		this.ᜆ = string.Empty;
		this.ᜇ = string.Empty;
		this.ᜈ = string.Empty;
		this.ᜉ = 1907505652U;
		this.ᜊ = string.Empty;
		this.ᜋ = string.Empty;
		this.ᜌ = string.Empty;
		base..ctor();
		this.ᜅ = new sprᯤ.ᜀ();
		this.ᜆ = RecordTableEnumerator.b("᥈⩊⹌⑎ぐ㑒ご坖", a_);
	}

	// Token: 0x06002D03 RID: 11523 RVA: 0x00194D40 File Offset: 0x00193D40
	internal override void ᜁ(byte[] A_0, int A_1)
	{
		int a_ = 19;
		switch (0)
		{
		default:
			for (;;)
			{
				this.ᜄ = A_0.Length;
				ASCIIEncoding asciiencoding = new ASCIIEncoding();
				UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
				this.ᜅ = new sprᯤ.ᜀ();
				this.ᜅ.ᜁ(A_0, A_1);
				A_1 += this.ᜅ.ᜀ();
				int num = spr\u20AE.ᜃ(A_0, ref A_1);
				int num2 = 9;
				for (;;)
				{
					uint num3;
					switch (num2)
					{
					case 0:
						if (num > 0)
						{
							num2 = 24;
							continue;
						}
						goto IL_45C;
					case 1:
						goto IL_2D8;
					case 2:
						goto IL_48B;
					case 3:
						if (num > 0)
						{
							num2 = 19;
							continue;
						}
						goto IL_222;
					case 4:
						if (num <= 40)
						{
							num2 = 7;
							continue;
						}
						goto IL_222;
					case 5:
						goto IL_222;
					case 6:
						goto IL_286;
					case 7:
					{
						byte[] bytes = spr\u20AE.ᜀ(A_0, num, ref A_1);
						this.ᜈ = asciiencoding.GetString(bytes);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_119;
						default:
							if (false)
							{
							}
							num2 = 5;
							continue;
						}
						break;
					}
					case 8:
						if (num > 0)
						{
							num2 = 30;
							continue;
						}
						goto IL_364;
					case 9:
						if (num > 0)
						{
							num2 = 22;
							continue;
						}
						goto IL_48B;
					case 10:
						if (this.ᜉ == 1907505652U)
						{
							num2 = 16;
							continue;
						}
						return;
					case 11:
						num2 = 17;
						continue;
					case 12:
					{
						byte[] bytes2 = spr\u20AE.ᜀ(A_0, num, ref A_1);
						this.ᜌ = unicodeEncoding.GetString(bytes2);
						num2 = 1;
						continue;
					}
					case 13:
						num2 = 27;
						continue;
					case 14:
						if (num3 != 4294967295U)
						{
							num2 = 15;
							continue;
						}
						goto IL_178;
					case 15:
						num2 = 23;
						continue;
					case 16:
						num = spr\u20AE.ᜃ(A_0, ref A_1);
						if (true)
						{
						}
						num2 = 0;
						continue;
					case 17:
						if (num <= 40)
						{
							num2 = 12;
							continue;
						}
						return;
					case 18:
						if (num3 == 4294967294U)
						{
							num2 = 21;
							continue;
						}
						num2 = 32;
						continue;
					case 19:
						num2 = 4;
						continue;
					case 20:
						if (num > 0)
						{
							num2 = 11;
							continue;
						}
						return;
					case 21:
						goto IL_3BA;
					case 22:
						goto IL_119;
					case 23:
						if (num3 == 4294967294U)
						{
							num2 = 31;
							continue;
						}
						num2 = 35;
						continue;
					case 24:
					{
						byte[] bytes3 = spr\u20AE.ᜀ(A_0, num, ref A_1);
						this.ᜊ = unicodeEncoding.GetString(bytes3);
						num2 = 26;
						continue;
					}
					case 25:
						if (num3 > 0U)
						{
							num2 = 13;
							continue;
						}
						goto IL_286;
					case 26:
						goto IL_45C;
					case 27:
						if (num3 != 4294967295U)
						{
							num2 = 33;
							continue;
						}
						goto IL_3BA;
					case 28:
						goto IL_3B5;
					case 29:
						goto IL_281;
					case 30:
						num2 = 14;
						continue;
					case 31:
						goto IL_178;
					case 32:
						if (num3 > 400U)
						{
							num2 = 28;
							continue;
						}
						goto IL_286;
					case 33:
						num2 = 18;
						continue;
					case 34:
						goto IL_364;
					case 35:
						if (num3 > 400U)
						{
							num2 = 29;
							continue;
						}
						goto IL_364;
					}
					break;
					IL_119:
					byte[] bytes4 = spr\u20AE.ᜀ(A_0, num, ref A_1);
					this.ᜆ = asciiencoding.GetString(bytes4);
					num2 = 2;
					continue;
					IL_178:
					byte[] bytes5 = spr\u20AE.ᜀ(A_0, 4, ref A_1);
					this.ᜆ = asciiencoding.GetString(bytes5);
					num2 = 34;
					continue;
					IL_222:
					this.ᜉ = spr\u20AE.ᜀ(A_0, ref A_1);
					num2 = 10;
					continue;
					IL_286:
					num = spr\u20AE.ᜃ(A_0, ref A_1);
					num2 = 20;
					continue;
					IL_364:
					num = spr\u20AE.ᜃ(A_0, ref A_1);
					num2 = 3;
					continue;
					IL_3BA:
					byte[] bytes6 = spr\u20AE.ᜀ(A_0, 4, ref A_1);
					this.ᜋ = unicodeEncoding.GetString(bytes6);
					num2 = 6;
					continue;
					IL_45C:
					num3 = spr\u20AE.ᜀ(A_0, ref A_1);
					num2 = 25;
					continue;
					IL_48B:
					num3 = spr\u20AE.ᜀ(A_0, ref A_1);
					num2 = 8;
				}
			}
			IL_281:
			throw new InvalidDataException(RecordTableEnumerator.b("و݊ࡌ潎≐❒❔㉖㡘㙚絜㙞འ䍢୤ࡦᵨ䭪᭬๮ᵰᩲᅴ", a_));
			IL_2D8:
			return;
			IL_3B5:
			throw new InvalidDataException(RecordTableEnumerator.b("و݊ࡌ潎≐❒❔㉖㡘㙚絜㙞འ䍢୤ࡦᵨ䭪᭬๮ᵰᩲᅴ", a_));
		}
	}

	// Token: 0x06002D04 RID: 11524 RVA: 0x00195208 File Offset: 0x00194208
	internal override int ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 11;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new NotImplementedException(RecordTableEnumerator.b("ཀⱂㅄ杆⁈♊㵌⍎㑐㹒ご㥖ⵘ㹚㥜", a_));
	}

	// Token: 0x06002D05 RID: 11525 RVA: 0x00195260 File Offset: 0x00194260
	internal new void ᜀ(spr\u2399 A_0)
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
		int a_ = 4;
		this.ᜅ.ᜀ(A_0);
		this.ᜀ(A_0, this.ᜆ);
		this.ᜀ(A_0, this.ᜇ);
		this.ᜀ(A_0, this.ᜈ);
		this.ᜀ(A_0, a_);
		this.ᜀ(A_0, a_);
		this.ᜀ(A_0, a_);
		this.ᜀ(A_0, a_);
	}

	// Token: 0x06002D06 RID: 11526 RVA: 0x001952F0 File Offset: 0x001942F0
	private new void ᜀ(spr\u2399 A_0, int A_1)
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
		byte[] buffer = new byte[A_1];
		A_0.Write(buffer, 0, A_1);
	}

	// Token: 0x06002D07 RID: 11527 RVA: 0x0019533C File Offset: 0x0019433C
	private new void ᜀ(spr\u2399 A_0, string A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
				{
					if (false)
					{
					}
					byte[] array = new byte[4];
					ASCIIEncoding asciiencoding = new ASCIIEncoding();
					int num = 0;
					byte[] bytes = asciiencoding.GetBytes(A_1);
					spr\u20AE.ᜀ(array, ref num, bytes.Length);
					A_0.Write(array, 0, array.Length);
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return;
						case 1:
							A_0.Write(bytes, 0, bytes.Length);
							num2 = 0;
							continue;
						case 2:
							if (true)
							{
							}
							if (bytes.Length > 0)
							{
								num2 = 1;
								continue;
							}
							return;
						}
						break;
					}
					break;
				}
				}
			}
			return;
		}
	}

	// Token: 0x040014B7 RID: 5303
	private new const int ᜀ = 93;

	// Token: 0x040014B8 RID: 5304
	private new const int ᜁ = 400;

	// Token: 0x040014B9 RID: 5305
	private new const int ᜂ = 40;

	// Token: 0x040014BA RID: 5306
	private new const uint ᜃ = 1907505652U;

	// Token: 0x040014BB RID: 5307
	private new int ᜄ;

	// Token: 0x040014BC RID: 5308
	private sprᯤ.ᜀ ᜅ;

	// Token: 0x040014BD RID: 5309
	private string ᜆ;

	// Token: 0x040014BE RID: 5310
	private string ᜇ;

	// Token: 0x040014BF RID: 5311
	private string ᜈ;

	// Token: 0x040014C0 RID: 5312
	private uint ᜉ;

	// Token: 0x040014C1 RID: 5313
	private string ᜊ;

	// Token: 0x040014C2 RID: 5314
	private string ᜋ;

	// Token: 0x040014C3 RID: 5315
	private string ᜌ;

	// Token: 0x020002DF RID: 735
	private new class ᜀ : spr\u20AE
	{
		// Token: 0x06002D08 RID: 11528 RVA: 0x001953FC File Offset: 0x001943FC
		internal override int ᜀ()
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
			return 28;
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x0019543C File Offset: 0x0019443C
		internal ᜀ()
		{
			this.ᜂ = -131071;
			this.ᜃ = 2563;
			this.ᜄ = new byte[20];
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x00195474 File Offset: 0x00194474
		internal override void ᜁ(byte[] A_0, int A_1)
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
			this.ᜂ = spr\u20AE.ᜃ(A_0, ref A_1);
			this.ᜃ = spr\u20AE.ᜃ(A_0, ref A_1);
			this.ᜄ = spr\u20AE.ᜀ(A_0, 20, ref A_1);
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x001954DC File Offset: 0x001944DC
		internal override int ᜀ(byte[] A_0, int A_1)
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
			spr\u20AE.ᜀ(A_0, ref A_1, -131071);
			spr\u20AE.ᜀ(A_0, ref A_1, 2563);
			this.ᜄ = new byte[]
			{
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				byte.MaxValue,
				101,
				202,
				1,
				184,
				252,
				161,
				208,
				17,
				133,
				173,
				68,
				69,
				83,
				84,
				0,
				0
			};
			spr\u20AE.ᜀ(A_0, ref A_1, this.ᜄ);
			return A_1;
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x00195558 File Offset: 0x00194558
		internal new void ᜀ(spr\u2399 A_0)
		{
			byte[] bytes = BitConverter.GetBytes(this.ᜂ);
			A_0.Write(bytes, 0, 4);
			bytes = BitConverter.GetBytes(this.ᜃ);
			A_0.Write(bytes, 0, 4);
			if (this.ᜄ != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					A_0.Write(this.ᜄ, 0, 20);
					return;
				}
			}
			A_0.Write(new byte[20], 0, 20);
		}

		// Token: 0x040014C4 RID: 5316
		internal new const int ᜀ = 28;

		// Token: 0x040014C5 RID: 5317
		internal new const int ᜁ = 20;

		// Token: 0x040014C6 RID: 5318
		internal new int ᜂ;

		// Token: 0x040014C7 RID: 5319
		internal new int ᜃ;

		// Token: 0x040014C8 RID: 5320
		internal new byte[] ᜄ;
	}
}
