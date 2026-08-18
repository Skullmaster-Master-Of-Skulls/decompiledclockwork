using System;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

// Token: 0x020004BD RID: 1213
[CLSCompliant(false)]
internal class spr\u22F6 : IDecryptor, IEncryptor
{
	// Token: 0x06004ABE RID: 19134 RVA: 0x002D59BC File Offset: 0x002D49BC
	public bool ᜁ(string A_0)
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
		this.ᜀ(A_0);
		return this.ᜂ();
	}

	// Token: 0x06004ABF RID: 19135 RVA: 0x002D5A08 File Offset: 0x002D4A08
	public MemoryStream ᜀ(Stream A_0)
	{
		MemoryStream memoryStream;
		for (;;)
		{
			switch (0)
			{
			default:
			{
				int num = 11;
				for (;;)
				{
					long length;
					byte[] array;
					spr\u24E5 a_;
					switch (num)
					{
					case 0:
						goto IL_185;
					case 1:
					{
						if (length == 0L)
						{
							num = 2;
							continue;
						}
						long num2 = A_0.Position;
						uint num3 = 0U;
						WordKey wordKey = new WordKey();
						this.ᜀ(wordKey, num3, this.ᜉ);
						num = 0;
						continue;
					}
					case 2:
						return memoryStream;
					case 3:
					{
						int num4;
						if (num4 >= 16)
						{
							num = 10;
							continue;
						}
						array[num4] = 1;
						num4++;
						num = 13;
						continue;
					}
					case 4:
						goto IL_185;
					case 5:
						goto IL_1A2;
					case 6:
						goto IL_71;
					case 7:
					{
						uint num3;
						num3 += 1U;
						WordKey wordKey;
						this.ᜀ(wordKey, num3, this.ᜉ);
						num = 4;
						continue;
					}
					case 8:
						goto IL_15F;
					case 9:
					{
						long num2;
						if (num2 % 1024L == 0L)
						{
							if (true)
							{
							}
							num = 7;
							continue;
						}
						goto IL_185;
					}
					case 10:
					{
						WordKey wordKey;
						this.ᜀ(a_, 0, 16, wordKey);
						memoryStream.Write(array, 0, 16);
						long num2;
						num2 += 16L;
						num = 9;
						continue;
					}
					case 12:
					{
						long num2;
						if (num2 >= length)
						{
							num = 5;
							continue;
						}
						int num5 = A_0.Read(array, 0, 16);
						int num4 = num5;
						num = 8;
						continue;
					}
					case 13:
						goto IL_15F;
					}
					if (A_0 == null)
					{
						num = 6;
						continue;
					}
					memoryStream = new MemoryStream();
					array = new byte[16];
					a_ = new spr\u24E5(array);
					length = A_0.Length;
					num = 1;
					continue;
					IL_15F:
					num = 3;
					continue;
					IL_185:
					num = 12;
				}
				IL_71:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_87;
				}
				break;
			}
			}
		}
		IL_87:
		if (false)
		{
		}
		return null;
		IL_1A2:
		memoryStream.Position = 0L;
		return memoryStream;
	}

	// Token: 0x06004AC0 RID: 19136 RVA: 0x002D5C18 File Offset: 0x002D4C18
	public void ᜁ(DataProvider A_0, int A_1, int A_2, long A_3)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (A_2 < 0)
					{
						num = 4;
						continue;
					}
					this.ᜀ();
					long num2 = A_3 / 1024L;
					long num3 = num2 * 1024L;
					int val = (int)(1024L - A_3 + num3);
					int num4 = A_1;
					int num5 = Math.Min(val, A_2);
					num = 5;
					continue;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						goto IL_E0;
					}
					break;
				case 2:
					return;
				case 3:
				{
					int num5;
					if (num5 <= 0)
					{
						num = 8;
						continue;
					}
					WordKey a_2 = this.ᜀ(A_3);
					int num4;
					this.ᜀ(A_0, num4, num5, a_2);
					num4 += num5;
					A_2 -= num5;
					A_3 += (long)num5;
					num5 = Math.Min(1024, A_2);
					num = 1;
					continue;
				}
				case 4:
					goto IL_191;
				case 5:
					goto IL_E0;
				case 7:
					goto IL_DB;
				case 8:
					return;
				case 9:
					if (A_1 < 0)
					{
						num = 7;
						continue;
					}
					if (true)
					{
					}
					num = 0;
					continue;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 9;
				continue;
				IL_E0:
				num = 3;
			}
			return;
			IL_DB:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嘸崺嬼䰾⑀㝂", a_));
			IL_191:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("唸帺匼堾㕀⭂", a_));
		}
		}
	}

	// Token: 0x06004AC1 RID: 19137 RVA: 0x002D5DD0 File Offset: 0x002D4DD0
	public void ᜀ(byte[] A_0, int A_1, int A_2)
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
		throw new NotImplementedException();
	}

	// Token: 0x06004AC2 RID: 19138 RVA: 0x002D5E10 File Offset: 0x002D4E10
	private void ᜀ(string A_0)
	{
		int num;
		for (;;)
		{
			num = 0;
			int length = A_0.Length;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_D2;
			default:
			{
				if (false)
				{
				}
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						num2 = 4;
						continue;
					case 1:
						goto IL_6F;
					case 2:
						if (true)
						{
						}
						if (num < 16)
						{
							num2 = 0;
							continue;
						}
						goto IL_D2;
					case 3:
						goto IL_6D;
					case 4:
					{
						if (num >= length)
						{
							num2 = 3;
							continue;
						}
						ushort num3 = (ushort)A_0[num];
						this.ᜈ[2 * num] = (byte)(num3 & 255);
						this.ᜈ[2 * num + 1] = (byte)(num3 >> 8 & 255);
						num++;
						num2 = 5;
						continue;
					}
					case 5:
						goto IL_6F;
					}
					break;
					IL_6F:
					num2 = 2;
				}
				break;
			}
			}
		}
		IL_6D:
		IL_D2:
		this.ᜈ[2 * num] = 128;
		this.ᜈ[56] = (byte)(num << 4);
	}

	// Token: 0x06004AC3 RID: 19139 RVA: 0x002D5F0C File Offset: 0x002D4F0C
	private void ᜀ(ref byte A_0, ref byte A_1)
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
		byte b = A_0;
		A_0 = A_1;
		A_1 = b;
	}

	// Token: 0x06004AC4 RID: 19140 RVA: 0x002D5F54 File Offset: 0x002D4F54
	private void ᜀ(WordKey A_0, byte[] A_1, byte A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				byte b = 0;
				byte b2 = 0;
				byte[] status = A_0.Status;
				int num = 0;
				int num2 = 3;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						goto IL_110;
					case 1:
						goto IL_9C;
					case 2:
						if (num >= 256)
						{
							num2 = 1;
							continue;
						}
						status[num] = (byte)num;
						num++;
						num2 = 0;
						continue;
					case 3:
						goto IL_110;
					case 4:
						goto IL_CE;
					case 5:
						goto IL_CE;
					case 6:
						return;
					case 7:
						if (num3 < 256)
						{
							b2 = (byte)((int)(A_1[(int)b] + status[num3] + b2) % 256);
							this.ᜀ(ref status[num3], ref status[(int)b2]);
							b = (b + 1) % A_2;
							num3++;
							if (true)
							{
							}
							num2 = 5;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9C;
						default:
							if (false)
							{
							}
							num2 = 6;
							continue;
						}
						break;
					}
					break;
					IL_9C:
					num3 = 0;
					num2 = 4;
					continue;
					IL_CE:
					num2 = 7;
					continue;
					IL_110:
					num2 = 2;
				}
			}
			return;
		}
	}

	// Token: 0x06004AC5 RID: 19141 RVA: 0x002D6098 File Offset: 0x002D5098
	private void ᜀ(WordKey A_0, uint A_1, sprឃ A_2)
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
		sprឃ sprឃ = new sprឃ();
		byte[] array = new byte[64];
		Buffer.BlockCopy(A_2.ᜂ(), 0, array, 0, 5);
		array[5] = (byte)(A_1 & 255U);
		array[6] = (byte)(A_1 >> 8 & 255U);
		array[7] = (byte)(A_1 >> 16 & 255U);
		array[8] = (byte)(A_1 >> 24 & 255U);
		array[9] = 128;
		array[56] = 72;
		sprឃ.ᜀ(array, 64U);
		sprឃ.ᜅ();
		this.ᜀ(A_0, sprឃ.ᜂ(), 16);
	}

	// Token: 0x06004AC6 RID: 19142 RVA: 0x002D6154 File Offset: 0x002D5154
	private bool ᜀ(byte[] A_0, byte[] A_1, int A_2)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_7A;
				case 1:
					return false;
				case 2:
					return true;
				case 3:
					if (num >= A_2)
					{
						num2 = 2;
						continue;
					}
					num2 = 5;
					continue;
				case 4:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return true;
					default:
						if (false)
						{
						}
						goto IL_7A;
					}
					break;
				case 5:
					if (A_0[num] != A_1[num])
					{
						num2 = 1;
						continue;
					}
					num++;
					num2 = 0;
					continue;
				}
				break;
				IL_7A:
				num2 = 3;
			}
		}
		return false;
	}

	// Token: 0x06004AC7 RID: 19143 RVA: 0x002D6200 File Offset: 0x002D5200
	private bool ᜂ()
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
		this.ᜁ();
		WordKey wordKey = new WordKey();
		this.ᜀ(wordKey, 0U, this.ᜉ);
		spr\u24E5 spr_u24E = new spr\u24E5(this.ᜆ);
		this.ᜀ(spr_u24E, 0, 16, wordKey);
		spr_u24E.ᜀ(this.ᜇ);
		this.ᜀ(spr_u24E, 0, 16, wordKey);
		this.ᜆ[16] = 128;
		spr\u22F6.ᜀ(this.ᜆ, 17, 47, 0);
		this.ᜆ[56] = 128;
		sprឃ sprឃ = new sprឃ();
		sprឃ.ᜀ(this.ᜆ, 64U);
		sprឃ.ᜅ();
		return this.ᜀ(sprឃ.ᜂ(), this.ᜇ, 16);
	}

	// Token: 0x06004AC8 RID: 19144 RVA: 0x002D62E0 File Offset: 0x002D52E0
	private void ᜁ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				sprឃ sprឃ = new sprឃ();
				sprឃ.ᜀ(this.ᜈ, 64U);
				sprឃ.ᜅ();
				this.ᜉ = new sprឃ();
				int num = 0;
				int srcOffset = 0;
				int num2 = 5;
				int num3 = 6;
				for (;;)
				{
					if (true)
					{
					}
					switch (num3)
					{
					case 0:
						goto IL_12D;
					case 1:
						if (num == 64)
						{
							num3 = 4;
							continue;
						}
						srcOffset = 0;
						num2 = 5;
						Buffer.BlockCopy(this.ᜅ, 0, this.ᜈ, num, 16);
						num += 16;
						goto IL_9E;
					case 2:
						if (num == 16)
						{
							num3 = 8;
							continue;
						}
						num3 = 5;
						continue;
					case 3:
						goto IL_12D;
					case 4:
						this.ᜉ.ᜀ(this.ᜈ, 64U);
						srcOffset = num2;
						num2 = 5 - num2;
						num = 0;
						num3 = 0;
						continue;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9E;
						default:
							if (false)
							{
							}
							if (64 - num < 5)
							{
								num3 = 9;
								continue;
							}
							goto IL_AF;
						}
						break;
					case 6:
						goto IL_12D;
					case 7:
						goto IL_AF;
					case 8:
						goto IL_14A;
					case 9:
						num2 = 64 - num;
						num3 = 7;
						continue;
					}
					break;
					IL_9E:
					num3 = 3;
					continue;
					IL_AF:
					Buffer.BlockCopy(sprឃ.ᜂ(), srcOffset, this.ᜈ, num, num2);
					num += num2;
					num3 = 1;
					continue;
					IL_12D:
					num3 = 2;
				}
			}
			IL_14A:
			this.ᜈ[16] = 128;
			Array.Clear(this.ᜈ, 17, 47);
			this.ᜈ[56] = 128;
			this.ᜈ[57] = 10;
			this.ᜉ.ᜀ(this.ᜈ, 64U);
			this.ᜉ.ᜅ();
			return;
		}
	}

	// Token: 0x06004AC9 RID: 19145 RVA: 0x002D64CC File Offset: 0x002D54CC
	private static void ᜀ(byte[] A_0, int A_1, int A_2, byte A_3)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_7D;
			case 2:
				goto IL_57;
			case 4:
			{
				int num2;
				if (num2 >= A_2)
				{
					num = 0;
					continue;
				}
				A_0[num2] = A_3;
				num2++;
				num = 5;
				continue;
			}
			case 5:
				goto IL_7D;
			}
			if (A_0 != null)
			{
				int num2 = A_1;
				num = 1;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 2;
				continue;
			}
			IL_7D:
			num = 4;
		}
		IL_57:
		throw new ArgumentNullException();
	}

	// Token: 0x06004ACA RID: 19146 RVA: 0x002D657C File Offset: 0x002D557C
	private void ᜀ(DataProvider A_0, int A_1, int A_2, WordKey A_3)
	{
		switch (0)
		{
		default:
		{
			byte b;
			byte b2;
			byte[] status;
			for (;;)
			{
				b = A_3.X;
				b2 = A_3.Y;
				status = A_3.Status;
				int num = 0;
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						if (num >= A_2)
						{
							num2 = 4;
							continue;
						}
						b = (byte)((int)(b + 1) % 256);
						b2 = (byte)((int)(status[(int)b] + b2) % 256);
						this.ᜀ(ref status[(int)b], ref status[(int)b2]);
						byte b3 = (byte)((int)(status[(int)b] + status[(int)b2]) % 256);
						num2 = 3;
						continue;
					}
					case 1:
					{
						byte b4 = A_0.ReadByte(A_1);
						byte b3;
						b4 ^= status[(int)b3];
						A_0.WriteByte(A_1, b4);
						num2 = 2;
						continue;
					}
					case 2:
						goto IL_5C;
					case 3:
						if (A_0 != null)
						{
							num2 = 1;
							continue;
						}
						goto IL_5C;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_78;
						default:
							goto IL_141;
						}
						break;
					case 5:
						goto IL_104;
					case 6:
						goto IL_78;
					}
					break;
					IL_5C:
					if (true)
					{
					}
					num++;
					A_1++;
					num2 = 6;
					continue;
					IL_104:
					num2 = 0;
					continue;
					IL_78:
					goto IL_104;
				}
			}
			IL_141:
			if (false)
			{
			}
			A_3.Status = status;
			A_3.X = b;
			A_3.Y = b2;
			return;
		}
		}
	}

	// Token: 0x06004ACB RID: 19147 RVA: 0x002D66EC File Offset: 0x002D56EC
	private void ᜀ()
	{
		int a_ = 2;
		while (this.ᜅ == null)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				throw new ApplicationException(RecordTableEnumerator.b("簷弹弻䰽㤿㉁ぃ⽅❇⑉汋㥍ㅏ⅑㩓煕ⱗ穙ⱛⱝ՟ቡգᑥ൧๩䉫", a_));
			}
		}
	}

	// Token: 0x06004ACC RID: 19148 RVA: 0x002D6750 File Offset: 0x002D5750
	private WordKey ᜀ(long A_0)
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
		WordKey wordKey = new WordKey();
		int a_ = (int)(A_0 - this.ᜊ);
		int a_2 = (int)(A_0 / 1024L);
		this.ᜀ(wordKey, (uint)a_2, this.ᜉ);
		a_ = (int)(A_0 % 1024L);
		this.ᜀ(null, 0, a_, wordKey);
		this.ᜊ = A_0;
		return wordKey;
	}

	// Token: 0x06004ACD RID: 19149 RVA: 0x002D67D0 File Offset: 0x002D57D0
	public bool ᜀ(byte[] A_0, byte[] A_1, byte[] A_2, string A_3)
	{
		int a_ = 2;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_F3;
			case 1:
				goto IL_152;
			case 2:
				goto IL_170;
			case 3:
				if (16 != A_2.Length)
				{
					num = 0;
					continue;
				}
				goto IL_1D3;
			case 4:
				goto IL_1A6;
			case 5:
				goto IL_A6;
			case 6:
				if (16 != A_1.Length)
				{
					num = 5;
					continue;
				}
				num = 3;
				continue;
			case 7:
				if (16 != A_0.Length)
				{
					num = 4;
					continue;
				}
				num = 6;
				continue;
			case 8:
				if (A_2 == null)
				{
					num = 2;
					continue;
				}
				num = 11;
				continue;
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1AB;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					break;
				}
				break;
			case 10:
				if (A_1 == null)
				{
					num = 13;
					continue;
				}
				num = 8;
				continue;
			case 11:
				if (A_3 == null)
				{
					num = 1;
					continue;
				}
				num = 7;
				continue;
			case 12:
				goto IL_8A;
			case 13:
				goto IL_10E;
			}
			if (A_0 == null)
			{
				num = 12;
			}
			else
			{
				num = 10;
			}
		}
		IL_8A:
		goto IL_1AB;
		IL_A6:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("崷吹弻䰽㤿㉁ぃ⍅ⱇ้⍋ⵍ᥏㙑", a_));
		IL_F3:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("尷匹嬻嬽㌿㙁", a_));
		IL_10E:
		throw new ArgumentNullException(RecordTableEnumerator.b("崷吹弻䰽㤿㉁ぃ⍅ⱇ้⍋ⵍ᥏㙑", a_));
		IL_152:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠷嬹伻䴽㜿ⵁ㙃≅", a_));
		IL_170:
		throw new ArgumentNullException(RecordTableEnumerator.b("尷匹嬻嬽㌿㙁", a_));
		IL_1A6:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("尷唹弻眽␿", a_));
		IL_1AB:
		throw new ArgumentNullException(RecordTableEnumerator.b("尷唹弻眽␿", a_));
		IL_1D3:
		this.ᜉ = new sprឃ();
		this.ᜅ = new byte[16];
		this.ᜆ = new byte[64];
		this.ᜇ = new byte[16];
		Buffer.BlockCopy(A_0, 0, this.ᜅ, 0, 16);
		Buffer.BlockCopy(A_1, 0, this.ᜆ, 0, 16);
		Buffer.BlockCopy(A_2, 0, this.ᜇ, 0, 16);
		return this.ᜁ(A_3);
	}

	// Token: 0x06004ACE RID: 19150 RVA: 0x002D6A1C File Offset: 0x002D5A1C
	public void ᜀ(byte[] A_0, string A_1)
	{
		int a_ = 18;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_7B;
			case 1:
				if (A_0 != null)
				{
					num = 5;
					continue;
				}
				goto IL_AE;
			case 2:
				if (A_0.Length != 16)
				{
					num = 0;
					continue;
				}
				goto IL_C2;
			case 3:
				goto IL_62;
			case 5:
				if (true)
				{
				}
				num = 2;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_C2;
			default:
				if (false)
				{
				}
				if (A_1 == null)
				{
					num = 3;
				}
				else
				{
					num = 1;
				}
				break;
			}
		}
		IL_62:
		throw new ArgumentNullException(RecordTableEnumerator.b("㡇⭉㽋㵍❏㵑♓㉕", a_));
		IL_7B:
		IL_AE:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⱇ╉⽋ݍ㑏", a_));
		IL_C2:
		this.ᜉ = new sprឃ();
		this.ᜅ = A_0;
		this.ᜀ(A_1);
		this.ᜁ();
		this.ᜆ = new byte[64];
		Buffer.BlockCopy(A_0, 0, this.ᜆ, 0, 16);
		this.ᜆ[16] = 128;
		spr\u22F6.ᜀ(this.ᜆ, 17, 47, 0);
		this.ᜆ[56] = 128;
		sprឃ sprឃ = new sprឃ();
		sprឃ.ᜀ(this.ᜆ, 64U);
		sprឃ.ᜅ();
		this.ᜇ = new byte[16];
		Buffer.BlockCopy(sprឃ.ᜂ(), 0, this.ᜇ, 0, 16);
		WordKey wordKey = new WordKey();
		this.ᜀ(wordKey, 0U, this.ᜉ);
		spr\u24E5 spr_u24E = new spr\u24E5(this.ᜆ);
		this.ᜀ(spr_u24E, 0, 16, wordKey);
		spr_u24E.ᜀ(this.ᜇ);
		this.ᜀ(spr_u24E, 0, 16, wordKey);
	}

	// Token: 0x06004ACF RID: 19151 RVA: 0x002D6BD4 File Offset: 0x002D5BD4
	public void ᜀ(DataProvider A_0, int A_1, int A_2, long A_3)
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
		this.ᜁ(A_0, A_1, A_2, A_3);
	}

	// Token: 0x06004AD0 RID: 19152 RVA: 0x002D6C1C File Offset: 0x002D5C1C
	public void ᜀ(byte[] A_0, int A_1, int A_2, long A_3)
	{
		for (;;)
		{
			IL_00:
			int num = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_00;
				}
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_82;
				case 1:
					this.ᜋ = new spr\u24E5(A_0);
					num = 0;
					continue;
				case 2:
					goto IL_6C;
				}
				if (this.ᜋ == null)
				{
					num = 1;
				}
				else
				{
					this.ᜋ.ᜀ(A_0);
					num = 2;
				}
			}
		}
		IL_6C:
		IL_82:
		if (true)
		{
		}
		this.ᜀ(this.ᜋ, A_1, A_2, A_3);
	}

	// Token: 0x06004AD1 RID: 19153 RVA: 0x002D6CC8 File Offset: 0x002D5CC8
	public FilePassRecord ᜃ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		FilePassRecord filePassRecord = (FilePassRecord)spr\u175E.ᜀ(TBIFFRecord.FilePass);
		filePassRecord.IsWeakEncryption = false;
		filePassRecord.Key = (filePassRecord.Hash = 1);
		filePassRecord.CreateStandardBlock();
		sprṺ sprṺ = filePassRecord.StandardBlock;
		Buffer.BlockCopy(this.ᜅ, 0, sprṺ.ᜂ(), 0, 16);
		Buffer.BlockCopy(this.ᜆ, 0, sprṺ.ᜁ(), 0, 16);
		Buffer.BlockCopy(this.ᜇ, 0, sprṺ.ᜀ(), 0, 16);
		return filePassRecord;
	}

	// Token: 0x040021EE RID: 8686
	private const int ᜀ = 16;

	// Token: 0x040021EF RID: 8687
	private const int ᜁ = 64;

	// Token: 0x040021F0 RID: 8688
	private const int ᜂ = 1024;

	// Token: 0x040021F1 RID: 8689
	private const int ᜃ = 0;

	// Token: 0x040021F2 RID: 8690
	private const int ᜄ = 256;

	// Token: 0x040021F3 RID: 8691
	private byte[] ᜅ;

	// Token: 0x040021F4 RID: 8692
	private byte[] ᜆ;

	// Token: 0x040021F5 RID: 8693
	private byte[] ᜇ;

	// Token: 0x040021F6 RID: 8694
	private byte[] ᜈ = new byte[64];

	// Token: 0x040021F7 RID: 8695
	private sprឃ ᜉ;

	// Token: 0x040021F8 RID: 8696
	private long ᜊ;

	// Token: 0x040021F9 RID: 8697
	private spr\u24E5 ᜋ;
}
