using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Spire.CompoundFile.Doc;

// Token: 0x0200027D RID: 637
[CLSCompliant(false)]
internal sealed class spr\u1AED
{
	// Token: 0x06002200 RID: 8704 RVA: 0x002341B4 File Offset: 0x002331B4
	internal spr\u1AED.EncrytionType ᜀ(spr\u2547 A_0)
	{
		int a_ = 1;
		spr\u1AED.EncrytionType result;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_6D:
			if (!A_0.ᜃ(ClipboardData.b("≦ݨࡪὬ᙮Űݲᱴᡶ᝸㉺፼᥾", a_)))
			{
				return result;
			}
			num = 1;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_5A;
			}
			break;
		}
		for (;;)
		{
			IL_35:
			Stream stream;
			switch (num)
			{
			case 0:
				goto IL_1A2;
			case 1:
				num = 3;
				continue;
			case 2:
				goto IL_6D;
			case 3:
				if (A_0.ᜇ(ClipboardData.b("慦⵨੪ᥬ๮≰Ͳᑴᑶᱸࡺ", a_)))
				{
					num = 0;
					continue;
				}
				return result;
			case 4:
				try
				{
					for (;;)
					{
						byte[] a_2 = new byte[4];
						int num2 = this.ᜀ(stream, a_2);
						stream.Position = 0L;
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (num2 != 131075)
								{
									num = 3;
									continue;
								}
								goto IL_143;
							case 1:
								if (num2 == 262148)
								{
									num = 4;
									continue;
								}
								goto IL_153;
							case 2:
								goto IL_143;
							case 3:
								num = 5;
								continue;
							case 4:
								result = spr\u1AED.EncrytionType.Agile;
								num = 8;
								continue;
							case 5:
								if (num2 == 131076)
								{
									num = 2;
									continue;
								}
								num = 1;
								continue;
							case 6:
								goto IL_15F;
							case 7:
								goto IL_153;
							case 8:
								goto IL_153;
							}
							break;
							IL_143:
							result = spr\u1AED.EncrytionType.Standard;
							num = 7;
							continue;
							IL_153:
							num = 6;
						}
					}
					IL_15F:
					return result;
				}
				finally
				{
					num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_19F;
						case 2:
							((IDisposable)stream).Dispose();
							num = 1;
							continue;
						}
						if (stream == null)
						{
							break;
						}
						num = 2;
					}
					IL_19F:;
				}
				goto IL_1A2;
			}
			break;
			IL_1A2:
			stream = A_0.ᜁ(ClipboardData.b("≦ݨࡪὬ᙮Űݲᱴᡶ᝸㉺፼᥾", a_));
			num = 4;
		}
		IL_5A:
		if (true)
		{
		}
		result = spr\u1AED.EncrytionType.None;
		num = 2;
		goto IL_35;
	}

	// Token: 0x06002201 RID: 8705 RVA: 0x002343CC File Offset: 0x002333CC
	internal int ᜀ(Stream A_0, byte[] A_1)
	{
		int a_ = 13;
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
			if (A_0.Read(A_1, 0, 4) == 4)
			{
				return BitConverter.ToInt32(A_1, 0);
			}
			break;
		}
		throw new Exception(ClipboardData.b("㩲᭴Ŷᡸ᝺ᑼ᭾ꆀ", a_));
	}

	// Token: 0x06002202 RID: 8706 RVA: 0x0023443C File Offset: 0x0023343C
	internal string ᜁ(Stream A_0)
	{
		int a_ = 9;
		for (;;)
		{
			IL_09:
			switch (0)
			{
			default:
				for (;;)
				{
					byte[] array = new byte[4];
					int num = this.ᜀ(A_0, array);
					array = new byte[num];
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
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
							{
								int num3;
								A_0.Position += (long)(4 - num3);
								num2 = 2;
								continue;
							}
							case 1:
							{
								if (A_0.Read(array, 0, num) != num)
								{
									num2 = 4;
									continue;
								}
								string @string = Encoding.Unicode.GetString(array, 0, array.Length);
								int num3 = num % 4;
								num2 = 3;
								continue;
							}
							case 2:
							{
								string @string;
								return @string;
							}
							case 3:
							{
								int num3;
								if (num3 != 0)
								{
									if (true)
									{
									}
									num2 = 0;
									continue;
								}
								string @string;
								return @string;
							}
							case 4:
								goto IL_85;
							}
							break;
						}
						break;
					}
					}
				}
				break;
			}
		}
		IL_85:
		throw new Exception(ClipboardData.b("♮ὰղᑴ᭶ၸὺ嵼᭾", a_));
	}

	// Token: 0x06002203 RID: 8707 RVA: 0x00234548 File Offset: 0x00233548
	internal string ᜀ(Stream A_0)
	{
		StringBuilder stringBuilder;
		for (;;)
		{
			for (;;)
			{
				stringBuilder = new StringBuilder();
				byte[] array = new byte[2];
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (true)
						{
						}
						string @string;
						if (@string[0] != '\0')
						{
							num = 3;
							continue;
						}
						goto IL_C6;
					}
					case 1:
						goto IL_53;
					case 2:
					{
						if (A_0.Read(array, 0, 2) <= 0)
						{
							num = 5;
							continue;
						}
						string @string = Encoding.Unicode.GetString(array, 0, array.Length);
						num = 0;
						continue;
					}
					case 3:
					{
						string @string;
						stringBuilder.Append(@string);
						num = 1;
						continue;
					}
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							goto IL_53;
						}
						break;
					case 5:
						goto IL_6F;
					}
					break;
					IL_53:
					num = 2;
				}
			}
		}
		IL_6F:
		IL_C6:
		return stringBuilder.ToString();
	}

	// Token: 0x06002204 RID: 8708 RVA: 0x00234624 File Offset: 0x00233624
	internal void ᜀ(Stream A_0, int A_1)
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
		byte[] bytes = BitConverter.GetBytes(A_1);
		A_0.Write(bytes, 0, 4);
	}

	// Token: 0x06002205 RID: 8709 RVA: 0x00234670 File Offset: 0x00233670
	internal void ᜀ(Stream A_0, string A_1)
	{
		for (;;)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					byte[] bytes = Encoding.Unicode.GetBytes(A_1);
					int num = bytes.Length;
					this.ᜀ(A_0, num);
					A_0.Write(bytes, 0, num);
					int num2 = num % 4;
					int num3 = 1;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_73;
						case 1:
							if (num2 != 0)
							{
								if (true)
								{
								}
								num3 = 2;
								continue;
							}
							goto IL_C4;
						case 2:
						{
							int num4 = 0;
							int num5 = 4 - num2;
							num3 = 0;
							continue;
						}
						case 3:
							goto IL_73;
						case 4:
							goto IL_8A;
						case 5:
						{
							int num4;
							int num5;
							if (num4 >= num5)
							{
								num3 = 4;
								continue;
							}
							A_0.WriteByte(0);
							num4++;
							num3 = 3;
							continue;
						}
						}
						break;
						IL_73:
						num3 = 5;
					}
				}
				IL_C4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_DA;
				}
				IL_8A:
				goto IL_C4;
			}
		}
		IL_DA:
		if (false)
		{
		}
	}

	// Token: 0x06002206 RID: 8710 RVA: 0x00234760 File Offset: 0x00233760
	internal void ᜁ(Stream A_0, string A_1)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_81:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			goto IL_38;
		}
		int length;
		for (;;)
		{
			IL_1E:
			switch (num)
			{
			case 0:
				goto IL_A9;
			case 1:
				if (A_1[length - 1] != '\0')
				{
					num = 0;
					continue;
				}
				return;
			case 2:
				goto IL_89;
			case 3:
				if (length != 0)
				{
					num = 4;
					continue;
				}
				goto IL_73;
			case 4:
				num = 1;
				continue;
			}
			goto IL_38;
		}
		IL_73:
		A_0.WriteByte(0);
		A_0.WriteByte(0);
		goto IL_81;
		IL_89:
		return;
		IL_A9:
		goto IL_73;
		IL_38:
		if (true)
		{
		}
		length = A_1.Length;
		byte[] bytes = Encoding.Unicode.GetBytes(A_1);
		A_0.Write(bytes, 0, bytes.Length);
		num = 3;
		goto IL_1E;
	}

	// Token: 0x06002207 RID: 8711 RVA: 0x00234824 File Offset: 0x00233824
	internal byte[] ᜀ(string A_0, byte[] A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			byte[] array9;
			for (;;)
			{
				SHA1 sha = new SHA1Managed();
				byte[] bytes = Encoding.Unicode.GetBytes(A_0);
				byte[] array = new byte[A_1.Length + bytes.Length];
				Buffer.BlockCopy(A_1, 0, array, 0, A_1.Length);
				Buffer.BlockCopy(bytes, 0, array, A_1.Length, bytes.Length);
				byte[] array2 = sha.ComputeHash(array);
				byte[] array3 = new byte[array2.Length + 4];
				byte[] array4 = array2;
				int num = 0;
				int num2 = 12;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_16F;
					case 1:
					{
						int num3 = 0;
						byte[] array5;
						int num4 = array5.Length;
						num2 = 19;
						continue;
					}
					case 2:
						goto IL_1C8;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_146;
						default:
						{
							if (false)
							{
							}
							int num5;
							if (num5 >= 64)
							{
								num2 = 1;
								continue;
							}
							byte[] array6;
							array6[num5] = 92;
							num5++;
							num2 = 0;
							continue;
						}
						}
						break;
					case 4:
					{
						int num6 = 0;
						byte[] array5;
						int num7 = array5.Length;
						num2 = 2;
						continue;
					}
					case 5:
					{
						int num6;
						int num7;
						if (num6 >= num7)
						{
							num2 = 13;
							continue;
						}
						byte[] array6;
						byte[] array7 = array6;
						int num8 = num6;
						byte[] array5;
						array7[num8] ^= array5[num6];
						num6++;
						num2 = 10;
						continue;
					}
					case 6:
					{
						if (true)
						{
						}
						byte[] array6;
						sha.ComputeHash(array6);
						num2 = 18;
						continue;
					}
					case 7:
						goto IL_16F;
					case 8:
					{
						int num3;
						int num4;
						if (num3 >= num4)
						{
							num2 = 6;
							continue;
						}
						byte[] array6;
						byte[] array8 = array6;
						int num9 = num3;
						byte[] array5;
						array8[num9] ^= array5[num3];
						num3++;
						num2 = 20;
						continue;
					}
					case 9:
						goto IL_271;
					case 10:
						goto IL_1C8;
					case 11:
						goto IL_317;
					case 12:
						goto IL_146;
					case 13:
					{
						byte[] array6;
						array9 = sha.ComputeHash(array6);
						int num5 = 0;
						num2 = 7;
						continue;
					}
					case 14:
					{
						byte[] bytes2 = BitConverter.GetBytes(0);
						Buffer.BlockCopy(array4, 0, array3, 0, array4.Length);
						Buffer.BlockCopy(bytes2, 0, array3, array4.Length, bytes2.Length);
						byte[] array5 = sha.ComputeHash(array3);
						byte[] array6 = new byte[64];
						int num10 = 0;
						num2 = 16;
						continue;
					}
					case 15:
						goto IL_146;
					case 16:
						goto IL_317;
					case 17:
					{
						int num10;
						if (num10 >= 64)
						{
							num2 = 4;
							continue;
						}
						byte[] array6;
						array6[num10] = 54;
						num10++;
						num2 = 11;
						continue;
					}
					case 18:
						if (A_2 <= array9.Length)
						{
							num2 = 9;
							continue;
						}
						goto IL_38D;
					case 19:
						goto IL_120;
					case 20:
						goto IL_120;
					case 21:
					{
						if (num >= 50000)
						{
							num2 = 14;
							continue;
						}
						byte[] bytes2 = BitConverter.GetBytes(num);
						Buffer.BlockCopy(bytes2, 0, array3, 0, bytes2.Length);
						Buffer.BlockCopy(array4, 0, array3, bytes2.Length, array4.Length);
						array4 = sha.ComputeHash(array3);
						num++;
						num2 = 15;
						continue;
					}
					}
					break;
					IL_120:
					num2 = 8;
					continue;
					IL_146:
					num2 = 21;
					continue;
					IL_16F:
					num2 = 3;
					continue;
					IL_1C8:
					num2 = 5;
					continue;
					IL_317:
					num2 = 17;
				}
			}
			IL_271:
			byte[] array10 = new byte[A_2];
			Buffer.BlockCopy(array9, 0, array10, 0, A_2);
			return array10;
			IL_38D:
			throw new NotImplementedException();
		}
		}
	}

	// Token: 0x06002208 RID: 8712 RVA: 0x00234BC4 File Offset: 0x00233BC4
	internal byte[] ᜀ(string A_0, byte[] A_1, byte[] A_2, int A_3, int A_4)
	{
		int num2;
		SHA1 sha;
		byte[] array;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_6E:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_83;
				case 1:
					goto IL_9A;
				case 2:
					goto IL_83;
				case 3:
					if (num2 >= A_4)
					{
						num = 1;
						continue;
					}
					array = sha.ComputeHash(this.ᜁ(BitConverter.GetBytes(num2), array));
					num2++;
					num = 2;
					continue;
				}
				goto IL_4B;
				IL_83:
				num = 3;
			}
			IL_9A:
			array = sha.ComputeHash(this.ᜁ(array, A_2));
			array = this.ᜀ(array, A_3, 54);
			return array;
		}
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				if (true)
				{
				}
				break;
			}
			break;
		}
		IL_4B:
		sha = new SHA1Managed();
		byte[] bytes = Encoding.Unicode.GetBytes(A_0);
		array = sha.ComputeHash(this.ᜁ(A_1, bytes));
		num2 = 0;
		goto IL_6E;
	}

	// Token: 0x06002209 RID: 8713 RVA: 0x00234CB0 File Offset: 0x00233CB0
	internal byte[] ᜀ(byte[] A_0, spr\u1AED.ᜀ A_1, int A_2)
	{
		int num2;
		int num3;
		byte[] array;
		byte[] array2;
		byte[] array3;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_5F:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_74;
				case 1:
				{
					if (num2 >= num3)
					{
						num = 3;
						continue;
					}
					if (true)
					{
					}
					int val = num3 - num2;
					int count = Math.Min(val, A_2);
					Buffer.BlockCopy(A_0, num2, array, 0, count);
					A_1(array, array2);
					Buffer.BlockCopy(array2, 0, array3, num2, count);
					num2 += A_2;
					num = 2;
					continue;
				}
				case 2:
					goto IL_74;
				case 3:
					return array3;
				}
				goto IL_43;
				IL_74:
				num = 1;
			}
			return array3;
		}
		default:
			if (false)
			{
			}
			switch (0)
			{
			}
			break;
		}
		IL_43:
		num3 = A_0.Length;
		array3 = new byte[num3];
		array = new byte[A_2];
		array2 = new byte[A_2];
		num2 = 0;
		goto IL_5F;
	}

	// Token: 0x0600220A RID: 8714 RVA: 0x00234D98 File Offset: 0x00233D98
	internal byte[] ᜁ(byte[] A_0, byte[] A_1)
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
		int num = A_0.Length;
		int num2 = A_1.Length;
		int num3 = num + num2;
		byte[] array = new byte[num3];
		Buffer.BlockCopy(A_0, 0, array, 0, num);
		Buffer.BlockCopy(A_1, 0, array, num, num2);
		return array;
	}

	// Token: 0x0600220B RID: 8715 RVA: 0x00234DFC File Offset: 0x00233DFC
	internal byte[] ᜀ(byte[] A_0, int A_1, byte A_2)
	{
		byte[] array;
		for (;;)
		{
			array = new byte[A_1];
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_C8;
				case 1:
					return array;
				case 2:
					goto IL_91;
				case 3:
					return array;
				case 4:
					num = 3;
					continue;
				case 5:
					Buffer.BlockCopy(A_0, 0, array, 0, A_1);
					num = 1;
					continue;
				case 6:
				{
					int num2;
					if (num2 >= A_1)
					{
						num = 4;
						continue;
					}
					array[num2] = A_2;
					num2++;
					num = 2;
					continue;
				}
				case 7:
					if (A_0.Length >= A_1)
					{
						num = 5;
						continue;
					}
					return array;
				case 8:
					if (A_0.Length >= A_1)
					{
						num = 7;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C8;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					break;
				case 9:
				{
					if (true)
					{
					}
					Buffer.BlockCopy(A_0, 0, array, 0, A_0.Length);
					int num2 = A_0.Length;
					num = 0;
					continue;
				}
				}
				break;
				IL_91:
				num = 6;
				continue;
				IL_C8:
				goto IL_91;
			}
		}
		return array;
	}

	// Token: 0x0600220C RID: 8716 RVA: 0x00234F18 File Offset: 0x00233F18
	internal byte[] ᜂ(byte[] A_0, byte[] A_1)
	{
		byte[] array;
		for (;;)
		{
			array = new byte[A_0.Length];
			int num = 0;
			if (true)
			{
			}
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_73:
				array[num] = (A_0[num] ^ A_1[num]);
				num++;
				num2 = 2;
				break;
			default:
				if (false)
				{
				}
				num2 = 3;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= array.Length)
					{
						num2 = 1;
						continue;
					}
					goto IL_73;
				case 1:
					return array;
				case 2:
					goto IL_5B;
				case 3:
					goto IL_5B;
				}
				break;
				IL_5B:
				num2 = 0;
			}
		}
		return array;
	}

	// Token: 0x0600220D RID: 8717 RVA: 0x00234FB8 File Offset: 0x00233FB8
	internal bool ᜀ(byte[] A_0, byte[] A_1)
	{
		bool result;
		for (;;)
		{
			result = true;
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return result;
				case 1:
					goto IL_97;
				case 2:
					if (num >= A_0.Length)
					{
						num2 = 4;
						continue;
					}
					num2 = 6;
					continue;
				case 3:
					goto IL_97;
				case 4:
					return result;
				case 5:
					result = false;
					num2 = 0;
					continue;
				case 6:
					if (A_0[num] == A_1[num])
					{
						num++;
						if (true)
						{
						}
						num2 = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return result;
					default:
						if (false)
						{
						}
						num2 = 5;
						continue;
					}
					break;
				}
				break;
				IL_97:
				num2 = 2;
			}
		}
		return result;
	}

	// Token: 0x040020C1 RID: 8385
	private const int ᜀ = 50000;

	// Token: 0x040020C2 RID: 8386
	internal const string ᜁ = "EncryptionInfo";

	// Token: 0x040020C3 RID: 8387
	internal const string ᜂ = "\u0006DataSpaces";

	// Token: 0x040020C4 RID: 8388
	internal const string ᜃ = "DataSpaceMap";

	// Token: 0x040020C5 RID: 8389
	internal const string ᜄ = "\u0006Primary";

	// Token: 0x040020C6 RID: 8390
	internal const string ᜅ = "DataSpaceInfo";

	// Token: 0x040020C7 RID: 8391
	internal const string ᜆ = "TransformInfo";

	// Token: 0x040020C8 RID: 8392
	internal const string ᜇ = "EncryptedPackage";

	// Token: 0x040020C9 RID: 8393
	internal const string ᜈ = "StrongEncryptionDataSpace";

	// Token: 0x040020CA RID: 8394
	internal const string ᜉ = "StrongEncryptionTransform";

	// Token: 0x040020CB RID: 8395
	internal const string ᜊ = "Version";

	// Token: 0x0200027E RID: 638
	internal enum EncrytionType
	{
		// Token: 0x040020CD RID: 8397
		Standard,
		// Token: 0x040020CE RID: 8398
		Agile,
		// Token: 0x040020CF RID: 8399
		None
	}

	// Token: 0x0200027F RID: 639
	// (Invoke) Token: 0x06002210 RID: 8720
	internal delegate void ᜀ(byte[] A_0, byte[] A_1);
}
