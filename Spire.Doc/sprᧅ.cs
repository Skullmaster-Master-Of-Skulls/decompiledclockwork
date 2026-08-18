using System;
using System.IO;
using System.Text;
using Spire.CompoundFile.Doc;

// Token: 0x020001F5 RID: 501
internal class sprᧅ : TextReader
{
	// Token: 0x060015F6 RID: 5622 RVA: 0x00162FCC File Offset: 0x00161FCC
	public sprᧅ(Stream A_0, Encoding A_1)
	{
		if (A_1 == null)
		{
			A_1 = Encoding.UTF8;
		}
		if (!A_0.CanSeek)
		{
			A_0 = sprᧅ.ᜀ(A_0);
		}
		this.ᜂ = A_0;
		this.ᜃ = new byte[16384];
		this.ᜅ = A_0.Read(this.ᜃ, 0, 4);
		this.ᜈ = new char[16384];
		this.ᜇ = sprᧅ.ᜀ(this.ᜃ, ref this.ᜄ, this.ᜅ);
		int num = this.ᜄ;
		if (this.ᜇ == null)
		{
			this.ᜇ = A_1.GetDecoder();
			this.ᜅ += A_0.Read(this.ᜃ, 4, 16380);
			this.ᜆ();
			Decoder decoder = this.ᜊ();
			if (decoder != null)
			{
				this.ᜇ = decoder;
			}
		}
		this.ᜂ.Seek(0L, SeekOrigin.Begin);
		this.ᜊ = (this.ᜉ = 0);
		if (num > 0)
		{
			A_0.Read(this.ᜃ, 0, num);
		}
		this.ᜄ = (this.ᜅ = 0);
	}

	// Token: 0x060015F7 RID: 5623 RVA: 0x00163104 File Offset: 0x00162104
	public Encoding ᜇ()
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

	// Token: 0x060015F8 RID: 5624 RVA: 0x00163148 File Offset: 0x00162148
	private static Stream ᜀ(Stream A_0)
	{
		switch (0)
		{
		default:
		{
			MemoryStream memoryStream;
			for (;;)
			{
				IL_31:
				int num = 100000;
				byte[] buffer = new byte[num];
				memoryStream = new MemoryStream();
				for (;;)
				{
					IL_44:
					if (true)
					{
					}
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							int count;
							if ((count = A_0.Read(buffer, 0, num)) <= 0)
							{
								num2 = 2;
								continue;
							}
							memoryStream.Write(buffer, 0, count);
							num2 = 1;
							continue;
						}
						case 1:
							goto IL_57;
						case 2:
							goto IL_96;
						case 3:
							goto IL_57;
						}
						goto IL_31;
						IL_57:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_44;
						default:
							if (false)
							{
							}
							num2 = 0;
							break;
						}
					}
				}
			}
			IL_96:
			memoryStream.Seek(0L, SeekOrigin.Begin);
			A_0.Close();
			return memoryStream;
		}
		}
	}

	// Token: 0x060015F9 RID: 5625 RVA: 0x00163218 File Offset: 0x00162218
	internal void ᜆ()
	{
		int num = 3;
		for (;;)
		{
			int charCount;
			int num2;
			switch (num)
			{
			case 0:
			{
				char[] destinationArray = new char[this.ᜈ.Length + charCount];
				Array.Copy(this.ᜈ, this.ᜊ, destinationArray, 0, this.ᜉ - this.ᜊ);
				this.ᜈ = destinationArray;
				num = 5;
				continue;
			}
			case 1:
				if (num2 < charCount)
				{
					num = 0;
					continue;
				}
				goto IL_176;
			case 2:
				goto IL_4A;
			case 4:
				goto IL_9B;
			case 5:
				goto IL_176;
			case 6:
				Array.Copy(this.ᜈ, this.ᜊ, this.ᜈ, 0, this.ᜉ - this.ᜊ);
				if (true)
				{
				}
				num = 4;
				continue;
			case 7:
				num = 8;
				continue;
			case 8:
				if (this.ᜊ < this.ᜉ)
				{
					num = 6;
					continue;
				}
				goto IL_9B;
			}
			if (this.ᜊ > 0)
			{
				num = 7;
				continue;
			}
			IL_4A:
			charCount = this.ᜇ.GetCharCount(this.ᜃ, this.ᜄ, this.ᜅ - this.ᜄ);
			num2 = this.ᜈ.Length - this.ᜉ;
			num = 1;
			continue;
			IL_176:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_4A;
			default:
				goto IL_18C;
			}
			IL_9B:
			this.ᜉ -= this.ᜊ;
			this.ᜊ = 0;
			num = 2;
		}
		IL_18C:
		if (false)
		{
		}
		this.ᜉ = this.ᜊ + this.ᜇ.GetChars(this.ᜃ, this.ᜄ, this.ᜅ - this.ᜄ, this.ᜈ, this.ᜊ);
		this.ᜄ = this.ᜅ;
	}

	// Token: 0x060015FA RID: 5626 RVA: 0x00163400 File Offset: 0x00162400
	internal static Decoder ᜀ(byte[] A_0, ref int A_1, int A_2)
	{
		int num = 22;
		for (;;)
		{
			uint num4;
			switch (num)
			{
			case 0:
				num = 13;
				continue;
			case 1:
				num = 23;
				continue;
			case 2:
			{
				uint num2;
				if (num2 != 60U)
				{
					num = 14;
					continue;
				}
				goto IL_26E;
			}
			case 3:
				goto IL_19D;
			case 4:
			{
				uint num3;
				if (num3 <= 15360U)
				{
					num = 25;
					continue;
				}
				num = 24;
				continue;
			}
			case 5:
			{
				uint num2;
				if (num2 != 4278189823U)
				{
					num = 12;
					continue;
				}
				goto IL_1B5;
			}
			case 6:
				num = 3;
				continue;
			case 7:
				num = 17;
				continue;
			case 8:
			{
				uint num2;
				if (num2 != 1006632960U)
				{
					goto IL_104;
				}
				goto IL_1B5;
			}
			case 9:
			{
				uint num2;
				if (num2 > 1006632960U)
				{
					num = 5;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_104;
				default:
					if (false)
					{
					}
					num = 21;
					continue;
				}
				break;
			}
			case 10:
			{
				if (true)
				{
				}
				uint num3;
				if (num3 != 60U)
				{
					num = 1;
					continue;
				}
				goto IL_1A2;
			}
			case 11:
				num = 18;
				continue;
			case 12:
				num = 26;
				continue;
			case 13:
			{
				uint num3;
				if (num3 != 65534U)
				{
					num = 6;
					continue;
				}
				goto IL_1A2;
			}
			case 14:
				num = 8;
				continue;
			case 15:
				num = 19;
				continue;
			case 16:
				goto IL_16A;
			case 17:
				goto IL_A5;
			case 18:
				goto IL_145;
			case 19:
				goto IL_145;
			case 20:
			{
				if (num4 == 15711167U)
				{
					num = 16;
					continue;
				}
				num4 >>= 8;
				uint num3 = num4;
				num = 4;
				continue;
			}
			case 21:
				num = 2;
				continue;
			case 23:
			{
				uint num3;
				if (num3 != 15360U)
				{
					num = 7;
					continue;
				}
				goto IL_134;
			}
			case 24:
			{
				uint num3;
				if (num3 != 65279U)
				{
					num = 0;
					continue;
				}
				goto IL_134;
			}
			case 25:
				num = 10;
				continue;
			case 26:
			{
				uint num2;
				if (num2 != 4294901758U)
				{
					num = 11;
					continue;
				}
				goto IL_26E;
			}
			case 27:
			{
				num4 = (uint)((int)A_0[A_1] << 24 | (int)A_0[A_1 + 1] << 16 | (int)A_0[A_1 + 2] << 8 | (int)A_0[A_1 + 3]);
				uint num2 = num4;
				num = 9;
				continue;
			}
			}
			if (4 <= A_2 - A_1)
			{
				num = 27;
				continue;
			}
			break;
			IL_104:
			num = 15;
			continue;
			IL_145:
			num4 >>= 8;
			num = 20;
		}
		IL_A5:
		goto IL_301;
		IL_134:
		A_1 += 2;
		return Encoding.BigEndianUnicode.GetDecoder();
		IL_16A:
		A_1 += 3;
		return Encoding.UTF8.GetDecoder();
		IL_19D:
		goto IL_301;
		IL_1A2:
		A_1 += 2;
		return new UnicodeEncoding(false, false).GetDecoder();
		IL_1B5:
		A_1 += 4;
		return new spr\u241B();
		IL_26E:
		A_1 += 4;
		return new sprḦ();
		IL_301:
		return null;
	}

	// Token: 0x060015FB RID: 5627 RVA: 0x00163710 File Offset: 0x00162710
	private int ᜃ()
	{
		if (this.ᜊ < this.ᜉ)
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
				return (int)this.ᜈ[this.ᜊ++];
			}
		}
		return -1;
	}

	// Token: 0x060015FC RID: 5628 RVA: 0x00163778 File Offset: 0x00162778
	private int ᜂ()
	{
		int num;
		int num2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_47:
			if (num == -1)
			{
				return num;
			}
			num2 = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_38;
		}
		for (;;)
		{
			IL_1E:
			if (true)
			{
			}
			switch (num2)
			{
			case 0:
				goto IL_75;
			case 1:
				this.ᜊ--;
				num2 = 0;
				continue;
			case 2:
				goto IL_47;
			}
			goto IL_38;
		}
		IL_75:
		return num;
		IL_38:
		num = this.ᜃ();
		num2 = 2;
		goto IL_1E;
	}

	// Token: 0x060015FD RID: 5629 RVA: 0x00163800 File Offset: 0x00162800
	private bool ᜂ(string A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_5B:
				int num = this.ᜂ();
				int num2 = 0;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						switch (num2)
						{
						case 0:
						{
							if (num != (int)A_0[0])
							{
								if (true)
								{
								}
								num2 = 7;
								continue;
							}
							int num3 = 0;
							int length = A_0.Length;
							num2 = 2;
							continue;
						}
						case 1:
						{
							int num3;
							int length;
							if (num3 >= length)
							{
								num2 = 4;
								continue;
							}
							num = this.ᜃ();
							char c = A_0[num3];
							num2 = 6;
							continue;
						}
						case 2:
							goto IL_DD;
						case 3:
							return false;
						case 4:
							goto IL_D9;
						case 5:
							num2 = 1;
							continue;
						case 6:
						{
							char c;
							if (num != (int)c)
							{
								num2 = 3;
								continue;
							}
							int num3;
							num3++;
							num2 = 9;
							continue;
						}
						case 7:
							return false;
						case 8:
							goto IL_E9;
						case 9:
							goto IL_DD;
						}
						goto IL_5B;
						IL_DD:
						num2 = 8;
						continue;
					}
					IL_E9:
					if (num == -1)
					{
						return true;
					}
					num2 = 5;
				}
			}
			return false;
			IL_D9:
			return true;
		}
	}

	// Token: 0x060015FE RID: 5630 RVA: 0x0016393C File Offset: 0x0016293C
	private void ᜁ()
	{
		for (;;)
		{
			char c = (char)this.ᜂ();
			int num = 3;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (c != '\t')
					{
						num = 17;
						continue;
					}
					goto IL_F3;
				case 2:
					if (c != '\n')
					{
						num = 4;
						continue;
					}
					goto IL_F3;
				case 3:
					goto IL_127;
				case 4:
					return;
				case 5:
					if (true)
					{
					}
					num = 14;
					continue;
				case 6:
					if (c != ' ')
					{
						num = 5;
						continue;
					}
					goto IL_127;
				case 7:
					num = 15;
					continue;
				case 8:
					num = 11;
					continue;
				case 9:
					if (c != '\r')
					{
						num = 16;
						continue;
					}
					goto IL_F3;
				case 10:
					if (c != ' ')
					{
						num = 0;
						continue;
					}
					goto IL_F3;
				case 11:
					if (c != '\n')
					{
						num = 12;
						continue;
					}
					goto IL_127;
				case 12:
					this.ᜊ = num2;
					num = 13;
					continue;
				case 13:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						goto IL_127;
					}
					break;
				case 14:
					if (c != '\t')
					{
						num = 7;
						continue;
					}
					goto IL_127;
				case 15:
					if (c != '\r')
					{
						num = 8;
						continue;
					}
					goto IL_127;
				case 16:
					num = 2;
					continue;
				case 17:
					num = 9;
					continue;
				}
				break;
				IL_F3:
				num2 = this.ᜊ;
				c = (char)this.ᜃ();
				num = 6;
				continue;
				IL_127:
				num = 10;
			}
		}
	}

	// Token: 0x060015FF RID: 5631 RVA: 0x00163AEC File Offset: 0x00162AEC
	private string ᜀ()
	{
		int num3;
		for (;;)
		{
			int num = this.ᜂ();
			int num2 = 6;
			for (;;)
			{
				int num4;
				switch (num2)
				{
				case 0:
					goto IL_7A;
				case 1:
					if (true)
					{
					}
					if (this.ᜊ <= num3)
					{
						num2 = 0;
						continue;
					}
					goto IL_CA;
				case 2:
					goto IL_104;
				case 3:
					goto IL_104;
				case 4:
					if (num4 != -1)
					{
						num2 = 11;
						continue;
					}
					goto IL_59;
				case 5:
					goto IL_E1;
				case 6:
					if (num != 39)
					{
						num2 = 5;
						continue;
					}
					goto IL_9E;
				case 7:
					goto IL_59;
				case 8:
					if (num4 == num)
					{
						num2 = 7;
						continue;
					}
					num4 = this.ᜃ();
					num2 = 2;
					continue;
				case 9:
					if (num == 34)
					{
						num2 = 10;
						continue;
					}
					goto IL_156;
				case 10:
					goto IL_9E;
				case 11:
					num2 = 8;
					continue;
				}
				break;
				IL_59:
				num2 = 1;
				continue;
				IL_9E:
				this.ᜃ();
				num3 = this.ᜊ;
				num4 = this.ᜃ();
				num2 = 3;
				continue;
				IL_E1:
				num2 = 9;
				continue;
				IL_104:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E1;
				default:
					if (false)
					{
					}
					num2 = 4;
					break;
				}
			}
		}
		IL_7A:
		return "";
		IL_CA:
		return new string(this.ᜈ, num3, this.ᜊ - num3 - 1);
		IL_156:
		return null;
	}

	// Token: 0x06001600 RID: 5632 RVA: 0x00163C50 File Offset: 0x00162C50
	private string ᜁ(string A_0)
	{
		int a_ = 19;
		for (;;)
		{
			this.ᜁ();
			string b = this.ᜉ();
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜁ();
					num = 2;
					continue;
				case 1:
					goto IL_B4;
				case 2:
					if (!this.ᜂ(ClipboardData.b("䑸", a_)))
					{
						goto IL_B6;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4A;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 3:
					if (string.Equals(A_0, b, StringComparison.OrdinalIgnoreCase))
					{
						num = 0;
						continue;
					}
					goto IL_B6;
				}
				break;
			}
		}
		IL_4A:
		this.ᜁ();
		return this.ᜀ();
		IL_B4:
		goto IL_4A;
		IL_B6:
		return null;
	}

	// Token: 0x06001601 RID: 5633 RVA: 0x00163D14 File Offset: 0x00162D14
	private string ᜀ(out string A_0)
	{
		int a_ = 16;
		for (;;)
		{
			this.ᜁ();
			A_0 = this.ᜉ();
			int num = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					this.ᜁ();
					num = 1;
					continue;
				case 1:
					if (!this.ᜂ(ClipboardData.b("䭵", a_)))
					{
						goto IL_B1;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_57;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 2:
					goto IL_AF;
				case 3:
					if (A_0 != null)
					{
						num = 0;
						continue;
					}
					goto IL_B1;
				}
				break;
			}
		}
		IL_57:
		this.ᜁ();
		return this.ᜀ();
		IL_AF:
		goto IL_57;
		IL_B1:
		return null;
	}

	// Token: 0x06001602 RID: 5634 RVA: 0x00163DD4 File Offset: 0x00162DD4
	private void ᜀ(string A_0)
	{
		for (;;)
		{
			int num;
			int num2;
			int num3;
			int length;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_E6:
				if ((int)A_0[num] == num2)
				{
					num3 = 1;
				}
				else
				{
					num = 0;
					num3 = 10;
				}
				break;
			default:
				if (false)
				{
				}
				num2 = this.ᜃ();
				num = 0;
				length = A_0.Length;
				num3 = 9;
				break;
			}
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (true)
					{
					}
					goto IL_FD;
				case 1:
					num++;
					num3 = 4;
					continue;
				case 2:
					return;
				case 3:
					goto IL_E6;
				case 4:
					if (num == length)
					{
						num3 = 2;
						continue;
					}
					goto IL_6D;
				case 5:
					return;
				case 6:
					if (num2 == -1)
					{
						num3 = 5;
						continue;
					}
					num3 = 3;
					continue;
				case 7:
					if (num < length)
					{
						num3 = 8;
						continue;
					}
					return;
				case 8:
					num3 = 6;
					continue;
				case 9:
					goto IL_FD;
				case 10:
					goto IL_6D;
				}
				break;
				IL_6D:
				num2 = this.ᜃ();
				num3 = 0;
				continue;
				IL_FD:
				num3 = 7;
			}
		}
	}

	// Token: 0x06001603 RID: 5635 RVA: 0x00163EFC File Offset: 0x00162EFC
	internal Decoder ᜊ()
	{
		int a_ = 5;
		switch (0)
		{
		default:
			for (;;)
			{
				Decoder decoder = null;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						string text;
						if (text != null)
						{
							num = 8;
							continue;
						}
						goto IL_92;
					}
					case 1:
					{
						string text = this.ᜁ(ClipboardData.b("๪ͬ౮Ṱᝲᱴ᥶Ṹ", a_));
						num = 0;
						continue;
					}
					case 2:
					{
						string text2;
						if (text2 != null)
						{
							num = 1;
							continue;
						}
						goto IL_78;
					}
					case 3:
						goto IL_8D;
					case 4:
						goto IL_78;
					case 5:
						if (this.ᜂ(ClipboardData.b("坪剬ᝮᱰὲ", a_)))
						{
							num = 6;
							continue;
						}
						goto IL_78;
					case 6:
					{
						if (true)
						{
						}
						string text2 = this.ᜁ(ClipboardData.b("ᵪ࡬ᵮɰᩲᩴ᥶", a_));
						num = 2;
						continue;
					}
					case 7:
						if (decoder == null)
						{
							num = 3;
							continue;
						}
						goto IL_E6;
					case 8:
					{
						Decoder decoder2;
						try
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								IL_176:
								num = 2;
								break;
							default:
								if (false)
								{
								}
								goto IL_163;
							}
							Encoding encoding;
							for (;;)
							{
								IL_14C:
								switch (num)
								{
								case 0:
									goto IL_199;
								case 1:
									goto IL_1A4;
								case 2:
									this.ᜆ = encoding;
									decoder2 = encoding.GetDecoder();
									num = 0;
									continue;
								case 3:
									if (encoding != null)
									{
										goto IL_176;
									}
									num = 1;
									continue;
								}
								goto IL_163;
							}
							IL_199:
							return decoder2;
							IL_1A4:
							goto IL_92;
							IL_163:
							string text;
							encoding = Encoding.GetEncoding(text);
							num = 3;
							goto IL_14C;
						}
						catch (ArgumentException)
						{
							goto IL_92;
						}
						return decoder2;
					}
					}
					break;
					IL_78:
					num = 7;
					continue;
					IL_92:
					this.ᜀ(ClipboardData.b("啪", a_));
					num = 4;
				}
			}
			IL_8D:
			return this.ᜈ();
			IL_E6:
			return null;
		}
	}

	// Token: 0x06001604 RID: 5636 RVA: 0x001640CC File Offset: 0x001630CC
	internal Decoder ᜈ()
	{
		int a_ = 3;
		switch (0)
		{
		default:
			for (;;)
			{
				int num = this.ᜃ();
				int num2 = 29;
				for (;;)
				{
					string text;
					int num3;
					string text2;
					string name;
					int num4;
					string text3;
					switch (num2)
					{
					case 0:
						num3 = text.IndexOf(ClipboardData.b("੨ͪ౬ᵮɰᙲŴ", a_));
						goto IL_F0;
					case 1:
						if (sprἿ.ᜀ(text2, ClipboardData.b("ŨὪᥬὮ屰ᙲѴɶၸൺ", a_)))
						{
							num2 = 18;
							continue;
						}
						num2 = 22;
						continue;
					case 2:
						num3 = text.IndexOf(ClipboardData.b("周", a_), num3);
						num2 = 6;
						continue;
					case 3:
						num2 = 15;
						continue;
					case 4:
						try
						{
							Encoding encoding = Encoding.GetEncoding(name);
							this.ᜆ = encoding;
							return encoding.GetDecoder();
						}
						catch (ArgumentException)
						{
							goto IL_25B;
						}
						goto IL_337;
					case 5:
						goto IL_11F;
					case 6:
						if (num3 >= 0)
						{
							num2 = 9;
							continue;
						}
						goto IL_25B;
					case 7:
						text2 = this.ᜉ();
						num2 = 20;
						continue;
					case 8:
						goto IL_1C6;
					case 9:
						num3++;
						num4 = text.IndexOf(ClipboardData.b("剨", a_), num3);
						num2 = 27;
						continue;
					case 10:
						goto IL_337;
					case 11:
						text = text3;
						num2 = 5;
						continue;
					case 12:
						if (sprἿ.ᜀ(text2, ClipboardData.b("Ѩ๪ᥬ๮", a_)))
						{
							num2 = 21;
							continue;
						}
						goto IL_25B;
					case 13:
						num2 = 12;
						continue;
					case 14:
					{
						string text4;
						if (text4 != null)
						{
							num2 = 16;
							continue;
						}
						goto IL_25B;
					}
					case 15:
						if (text != null)
						{
							num2 = 0;
							continue;
						}
						goto IL_25B;
					case 16:
						num2 = 28;
						continue;
					case 17:
					{
						if (num == -1)
						{
							num2 = 23;
							continue;
						}
						char c = (char)num;
						num2 = 30;
						continue;
					}
					case 18:
					{
						string text4 = text3;
						num2 = 24;
						continue;
					}
					case 19:
						if (num3 >= 0)
						{
							num2 = 2;
							continue;
						}
						goto IL_25B;
					case 20:
						if (text2 != null)
						{
							num2 = 13;
							continue;
						}
						goto IL_25B;
					case 21:
					{
						string text4 = null;
						text = null;
						num2 = 25;
						continue;
					}
					case 22:
						if (sprἿ.ᜀ(text2, ClipboardData.b("੨Ѫͬ᭮ᑰᵲŴ", a_)))
						{
							num2 = 11;
							continue;
						}
						goto IL_11F;
					case 23:
						goto IL_1ED;
					case 24:
						goto IL_11F;
					case 25:
						goto IL_11F;
					case 26:
						if (text2 != null)
						{
							num2 = 32;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F0;
						default:
							if (false)
							{
							}
							num2 = 14;
							continue;
						}
						break;
					case 27:
						if (num4 < 0)
						{
							num2 = 31;
							continue;
						}
						goto IL_337;
					case 28:
					{
						string text4;
						if (sprἿ.ᜀ(text4, ClipboardData.b("੨Ѫͬ᭮ᑰᵲŴ婶൸ɺർ᩾", a_)))
						{
							num2 = 3;
							continue;
						}
						goto IL_25B;
					}
					case 29:
						goto IL_1C6;
					case 30:
					{
						char c;
						if (c == '<')
						{
							num2 = 7;
							continue;
						}
						goto IL_25B;
					}
					case 31:
						num4 = text.Length;
						num2 = 10;
						continue;
					case 32:
						num2 = 1;
						continue;
					}
					break;
					IL_F0:
					num2 = 19;
					continue;
					IL_11F:
					text3 = this.ᜀ(out text2);
					num2 = 26;
					continue;
					IL_1C6:
					if (true)
					{
					}
					num2 = 17;
					continue;
					IL_25B:
					num = this.ᜃ();
					num2 = 8;
					continue;
					IL_337:
					name = text.Substring(num3, num4 - num3).Trim();
					num2 = 4;
				}
			}
			IL_1ED:
			return null;
		}
	}

	// Token: 0x06001605 RID: 5637 RVA: 0x00164510 File Offset: 0x00163510
	internal string ᜉ()
	{
		switch (0)
		{
		default:
		{
			int num3;
			for (;;)
			{
				int num = this.ᜂ();
				int num2 = 2;
				for (;;)
				{
					char c;
					switch (num2)
					{
					case 0:
						goto IL_14C;
					case 1:
						if (this.ᜊ < this.ᜉ - 1)
						{
							num2 = 4;
							continue;
						}
						goto IL_DC;
					case 2:
						if (num == -1)
						{
							num2 = 5;
							continue;
						}
						c = (char)num;
						num3 = this.ᜊ;
						num2 = 0;
						continue;
					case 3:
						if (!char.IsLetterOrDigit(c))
						{
							num2 = 6;
							continue;
						}
						goto IL_179;
					case 4:
						num2 = 3;
						continue;
					case 5:
						goto IL_7E;
					case 6:
						num2 = 12;
						continue;
					case 7:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_94;
						default:
							if (false)
							{
							}
							if (num3 == this.ᜊ)
							{
								num2 = 9;
								continue;
							}
							goto IL_1C5;
						}
						break;
					case 8:
						if (c != ':')
						{
							goto IL_94;
						}
						goto IL_179;
					case 9:
						goto IL_124;
					case 10:
						if (c != '_')
						{
							num2 = 14;
							continue;
						}
						goto IL_179;
					case 11:
						goto IL_DC;
					case 12:
						if (c != '-')
						{
							num2 = 15;
							continue;
						}
						goto IL_179;
					case 13:
						goto IL_14C;
					case 14:
						num2 = 8;
						continue;
					case 15:
						num2 = 10;
						continue;
					}
					break;
					IL_94:
					num2 = 11;
					continue;
					IL_DC:
					num2 = 7;
					continue;
					IL_14C:
					num2 = 1;
					continue;
					IL_179:
					c = this.ᜈ[++this.ᜊ];
					num2 = 13;
				}
			}
			IL_7E:
			return null;
			IL_124:
			return null;
			IL_1C5:
			return new string(this.ᜈ, num3, this.ᜊ - num3);
		}
		}
	}

	// Token: 0x06001606 RID: 5638 RVA: 0x001646F8 File Offset: 0x001636F8
	internal void ᜋ()
	{
		for (;;)
		{
			char c = (char)this.ᜂ();
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (this.ᜊ < this.ᜉ - 1)
					{
						num = 4;
						continue;
					}
					return;
				case 1:
					return;
				case 2:
					goto IL_E0;
				case 3:
					goto IL_E0;
				case 4:
					num = 5;
					continue;
				case 5:
					if (c != ' ')
					{
						num = 8;
						continue;
					}
					goto IL_9D;
				case 6:
					num = 7;
					continue;
				case 7:
					if (c != '\n')
					{
						num = 1;
						continue;
					}
					goto IL_9D;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					break;
				case 9:
					if (c != '\r')
					{
						num = 6;
						continue;
					}
					goto IL_9D;
				}
				break;
				IL_9D:
				c = this.ᜈ[++this.ᜊ];
				num = 3;
				continue;
				IL_E0:
				num = 0;
			}
		}
	}

	// Token: 0x06001607 RID: 5639 RVA: 0x0016481C File Offset: 0x0016381C
	internal void ᜀ(char A_0)
	{
		for (;;)
		{
			char c = (char)this.ᜂ();
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (c == A_0)
					{
						num = 3;
						continue;
					}
					c = this.ᜈ[++this.ᜊ];
					num = 4;
					continue;
				case 2:
					goto IL_52;
				case 3:
					return;
				case 4:
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
						goto IL_52;
					}
					break;
				case 5:
					if (this.ᜊ < this.ᜉ - 1)
					{
						num = 0;
						continue;
					}
					return;
				}
				break;
				IL_52:
				num = 5;
			}
		}
	}

	// Token: 0x06001608 RID: 5640 RVA: 0x001648E8 File Offset: 0x001638E8
	internal string ᜅ()
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_79;
		}
		if (false)
		{
		}
		int num2;
		for (;;)
		{
			this.ᜀ('=');
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜊ++;
					this.ᜋ();
					num = 5;
					continue;
				case 1:
					goto IL_EC;
				case 2:
				{
					char a_ = this.ᜈ[this.ᜊ];
					this.ᜊ++;
					num2 = this.ᜊ;
					this.ᜀ(a_);
					num = 4;
					continue;
				}
				case 3:
					if (true)
					{
					}
					if (this.ᜊ < this.ᜉ)
					{
						num = 0;
						continue;
					}
					goto IL_12B;
				case 4:
					if (this.ᜊ < this.ᜉ)
					{
						num = 1;
						continue;
					}
					goto IL_12B;
				case 5:
					if (this.ᜊ < this.ᜉ)
					{
						num = 2;
						continue;
					}
					goto IL_12B;
				}
				break;
			}
		}
		IL_EC:
		goto IL_79;
		IL_12B:
		return null;
		IL_79:
		string result = new string(this.ᜈ, num2, this.ᜊ - num2);
		this.ᜊ++;
		return result;
	}

	// Token: 0x06001609 RID: 5641 RVA: 0x00164A24 File Offset: 0x00163A24
	public virtual int ᜎ()
	{
		int num;
		for (;;)
		{
			IL_14:
			num = this.Read();
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_47;
				case 1:
					this.ᜊ--;
					num2 = 0;
					continue;
				case 2:
					if (num != -1)
					{
						num2 = 1;
						continue;
					}
					goto IL_6F;
				}
				goto IL_14;
			}
			IL_47:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_5D;
			}
		}
		IL_5D:
		if (false)
		{
		}
		IL_6F:
		if (true)
		{
		}
		return num;
	}

	// Token: 0x0600160A RID: 5642 RVA: 0x00164AAC File Offset: 0x00163AAC
	public virtual int ᜌ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_70:
			if (this.ᜊ >= this.ᜉ)
			{
				return -1;
			}
			num = 3;
			break;
		default:
			if (false)
			{
			}
			if (true)
			{
			}
			num = 1;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜅ = this.ᜂ.Read(this.ᜃ, 0, this.ᜃ.Length);
				this.ᜄ = 0;
				num = 4;
				continue;
			case 2:
				return -1;
			case 3:
				goto IL_90;
			case 4:
				if (this.ᜅ == 0)
				{
					num = 2;
					continue;
				}
				this.ᜆ();
				num = 6;
				continue;
			case 5:
				goto IL_70;
			case 6:
				goto IL_68;
			}
			if (this.ᜊ == this.ᜉ)
			{
				num = 0;
				continue;
			}
			IL_68:
			num = 5;
		}
		IL_90:
		return (int)this.ᜈ[this.ᜊ++];
	}

	// Token: 0x0600160B RID: 5643 RVA: 0x00164BC0 File Offset: 0x00163BC0
	public virtual int ᜂ(char[] A_0, int A_1, int A_2)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_68:
			if (this.ᜊ >= this.ᜉ)
			{
				return 0;
			}
			num = 3;
			break;
		default:
			if (false)
			{
			}
			num = 1;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜅ = this.ᜂ.Read(this.ᜃ, 0, this.ᜃ.Length);
				this.ᜄ = 0;
				num = 2;
				continue;
			case 2:
				if (this.ᜅ == 0)
				{
					num = 6;
					continue;
				}
				this.ᜆ();
				num = 4;
				continue;
			case 3:
				goto IL_8B;
			case 4:
				if (true)
				{
				}
				goto IL_60;
			case 5:
				goto IL_68;
			case 6:
				return -1;
			}
			if (this.ᜊ == this.ᜉ)
			{
				num = 0;
				continue;
			}
			IL_60:
			num = 5;
		}
		IL_8B:
		A_2 = Math.Min(this.ᜉ - this.ᜊ, A_2);
		Array.Copy(this.ᜈ, this.ᜊ, A_0, A_1, A_2);
		this.ᜊ += A_2;
		return A_2;
	}

	// Token: 0x0600160C RID: 5644 RVA: 0x00164CF8 File Offset: 0x00163CF8
	public virtual int ᜀ(char[] A_0, int A_1, int A_2)
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
		return this.Read(A_0, A_1, A_2);
	}

	// Token: 0x0600160D RID: 5645 RVA: 0x00164D3C File Offset: 0x00163D3C
	public int ᜁ(char[] A_0, int A_1, int A_2)
	{
		int num;
		for (;;)
		{
			if (true)
			{
			}
			num = 0;
			int num2 = this.ᜃ();
			int num3 = 5;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (num2 != 10)
					{
						num3 = 11;
						continue;
					}
					return num;
				case 1:
					if (this.ᜂ() == 10)
					{
						num3 = 10;
						continue;
					}
					return num;
				case 2:
					if (num2 == -1)
					{
						num3 = 9;
						continue;
					}
					A_0[num + A_1] = (char)num2;
					num++;
					num3 = 6;
					continue;
				case 3:
					num3 = 4;
					continue;
				case 4:
					if (num2 == 13)
					{
						num3 = 8;
						continue;
					}
					num3 = 0;
					continue;
				case 5:
					goto IL_DC;
				case 6:
					if (num + A_1 != A_2)
					{
						num3 = 3;
						continue;
					}
					return num;
				case 7:
					return num;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num3 = 1;
						continue;
					}
					break;
				case 9:
					return num;
				case 10:
					num2 = this.ᜃ();
					A_0[num + A_1] = (char)num2;
					num++;
					num3 = 7;
					continue;
				case 11:
					num2 = this.ᜃ();
					num3 = 12;
					continue;
				case 12:
					goto IL_DC;
				}
				break;
				IL_DC:
				num3 = 2;
			}
		}
		return num;
	}

	// Token: 0x0600160E RID: 5646 RVA: 0x00164EA4 File Offset: 0x00163EA4
	public virtual string \u170D()
	{
		StringBuilder stringBuilder;
		for (;;)
		{
			char[] array = new char[100000];
			stringBuilder = new StringBuilder();
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int charCount;
					if ((charCount = this.Read(array, 0, array.Length)) <= 0)
					{
						num = 1;
						continue;
					}
					stringBuilder.Append(array, 0, charCount);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_90;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				case 1:
					goto IL_55;
				case 2:
					goto IL_35;
				case 3:
					goto IL_90;
				}
				break;
				IL_35:
				num = 0;
				continue;
				IL_90:
				goto IL_35;
			}
		}
		IL_55:
		return stringBuilder.ToString();
	}

	// Token: 0x0600160F RID: 5647 RVA: 0x00164F54 File Offset: 0x00163F54
	public virtual void ᜄ()
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
		this.ᜂ.Close();
	}

	// Token: 0x040019FA RID: 6650
	private const int ᜀ = 16384;

	// Token: 0x040019FB RID: 6651
	private const int ᜁ = -1;

	// Token: 0x040019FC RID: 6652
	private Stream ᜂ;

	// Token: 0x040019FD RID: 6653
	private byte[] ᜃ;

	// Token: 0x040019FE RID: 6654
	private int ᜄ;

	// Token: 0x040019FF RID: 6655
	private int ᜅ;

	// Token: 0x04001A00 RID: 6656
	private Encoding ᜆ;

	// Token: 0x04001A01 RID: 6657
	private Decoder ᜇ;

	// Token: 0x04001A02 RID: 6658
	private char[] ᜈ;

	// Token: 0x04001A03 RID: 6659
	private int ᜉ;

	// Token: 0x04001A04 RID: 6660
	private int ᜊ;
}
