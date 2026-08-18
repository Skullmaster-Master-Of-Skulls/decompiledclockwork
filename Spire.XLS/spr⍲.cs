using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000404 RID: 1028
[spr\u2400(FormulaToken.tArray2)]
[spr\u2400(FormulaToken.tArray1)]
[spr\u2400(FormulaToken.tArray3)]
internal class spr\u2372 : Ptg, sprḝ, ICloneable
{
	// Token: 0x06003DCE RID: 15822 RVA: 0x002266F8 File Offset: 0x002256F8
	public spr\u2372()
	{
	}

	// Token: 0x06003DCF RID: 15823 RVA: 0x0022670C File Offset: 0x0022570C
	public spr\u2372(string A_0, FormulaUtil A_1)
	{
		int a_ = 13;
		base..ctor();
		A_0 = A_0.Substring(1, A_0.Length - 2);
		List<string> list = A_1.SplitArray(A_0, A_1.ArrayRowSeparator);
		List<string>[] array = new List<string>[list.Count];
		int i = 0;
		int count = list.Count;
		while (i < count)
		{
			string strFormula = list[i];
			array[i] = A_1.SplitArray(strFormula, A_1.OperandsSeparator);
			if (i > 0 && array[i].Count != array[i - 1].Count)
			{
				throw new ArgumentException(RecordTableEnumerator.b("ق⑄⑆ⅈ歊㽌⁎♐獒㱔㥖祘⽚㕜㩞䅠ᝢ⑤ᕦ᭨੪ᑬ佮ᱰٲٴͶ奸፺ᱼॾꎂꮊﺌﲐ떔쾠莢쮤튦쒨즪좬\uddae龰", a_));
			}
			i++;
		}
		this.ᜀ(array, A_1);
		this.ᜁ(2);
	}

	// Token: 0x06003DD0 RID: 15824 RVA: 0x002267C4 File Offset: 0x002257C4
	public spr\u2372(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06003DD1 RID: 15825 RVA: 0x002267DC File Offset: 0x002257DC
	public int ᜄ()
	{
		int a_ = 12;
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int num2 = 15;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						goto IL_184;
					case 1:
					{
						int upperBound;
						if (num3 > upperBound)
						{
							num2 = 4;
							continue;
						}
						object[,] array;
						int num4;
						object obj = array[num4, num3];
						if (true)
						{
						}
						num2 = 14;
						continue;
					}
					case 2:
						goto IL_1A6;
					case 3:
					{
						object obj;
						num += Encoding.Unicode.GetByteCount((string)obj) + 4;
						num2 = 18;
						continue;
					}
					case 4:
					{
						int num4;
						num4++;
						num2 = 11;
						continue;
					}
					case 5:
						goto IL_136;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return num;
						default:
						{
							if (false)
							{
							}
							num += 3;
							object[,] array = this.ᜉ;
							int upperBound2 = array.GetUpperBound(0);
							int upperBound = array.GetUpperBound(1);
							int num4 = array.GetLowerBound(0);
							num2 = 0;
							continue;
						}
						}
						break;
					case 7:
						goto IL_170;
					case 8:
						num2 = 12;
						continue;
					case 9:
					{
						object obj;
						if (!(obj is byte))
						{
							num2 = 17;
							continue;
						}
						goto IL_1A6;
					}
					case 10:
						return num;
					case 11:
						goto IL_184;
					case 12:
					{
						object obj;
						if (!(obj is bool))
						{
							num2 = 13;
							continue;
						}
						goto IL_1A6;
					}
					case 13:
						num2 = 9;
						continue;
					case 14:
					{
						object obj;
						if (!(obj is double))
						{
							num2 = 8;
							continue;
						}
						goto IL_1A6;
					}
					case 15:
						if (this.ᜉ != null)
						{
							num2 = 6;
							continue;
						}
						return num;
					case 16:
					{
						int num4;
						int upperBound2;
						if (num4 > upperBound2)
						{
							num2 = 10;
							continue;
						}
						object[,] array;
						num3 = array.GetLowerBound(1);
						num2 = 19;
						continue;
					}
					case 17:
						num2 = 20;
						continue;
					case 18:
						goto IL_170;
					case 19:
						goto IL_136;
					case 20:
					{
						object obj;
						if (obj == null)
						{
							num2 = 2;
							continue;
						}
						num2 = 21;
						continue;
					}
					case 21:
					{
						object obj;
						if (obj is string)
						{
							num2 = 3;
							continue;
						}
						goto IL_B8;
					}
					}
					break;
					IL_136:
					num2 = 1;
					continue;
					IL_170:
					num3++;
					num2 = 5;
					continue;
					IL_184:
					num2 = 16;
					continue;
					IL_1A6:
					num += 9;
					num2 = 7;
				}
			}
			IL_B8:
			throw new ArrayTypeMismatchException(RecordTableEnumerator.b("ᝁ⩃⍅ぇ㩉⥋ⵍ⑏㝑こ癕ⱗ⍙ⱛ㭝䁟ୡ੣䙥ᱧ⭩ṫᱭᅯୱ婳", a_));
		}
	}

	// Token: 0x06003DD2 RID: 15826 RVA: 0x00226A80 File Offset: 0x00225A80
	public int ᜀ(DataProvider A_0, int A_1)
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
		this.ᜇ = A_0.ReadByte(A_1);
		this.ᜈ = (ushort)A_0.ReadInt16(A_1 + 1);
		return this.ᜀ(A_0, A_1 + 3, (int)(this.ᜇ + 1), (int)(this.ᜈ + 1));
	}

	// Token: 0x06003DD3 RID: 15827 RVA: 0x00226AF4 File Offset: 0x00225AF4
	private int ᜀ(DataProvider A_0, int A_1, int A_2, int A_3)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			byte b2;
			for (;;)
			{
				this.ᜉ = new object[A_3, A_2];
				int num = 0;
				int num2 = 6;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						goto IL_188;
					case 1:
					{
						byte b;
						if (b != 16)
						{
							num2 = 7;
							continue;
						}
						this.ᜉ[num, num3] = A_0.ReadByte(A_1);
						A_1 += 8;
						num2 = 14;
						continue;
					}
					case 2:
						goto IL_171;
					case 3:
					{
						byte b;
						switch (b)
						{
						case 0:
							this.ᜉ[num, num3] = null;
							A_1 += 8;
							num2 = 13;
							continue;
						case 1:
						{
							double num4 = A_0.ReadDouble(A_1);
							this.ᜉ[num, num3] = num4;
							A_1 += 8;
							num2 = 17;
							continue;
						}
						case 2:
						{
							int num5;
							string text = A_0.ReadString16Bit(A_1, out num5);
							this.ᜉ[num, num3] = text;
							A_1 += num5;
							num2 = 2;
							continue;
						}
						case 3:
							goto IL_87;
						case 4:
						{
							bool flag = A_0.ReadBoolean(A_1);
							this.ᜉ[num, num3] = flag;
							A_1 += 8;
							num2 = 11;
							continue;
						}
						default:
							num2 = 12;
							continue;
						}
						break;
					}
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_139;
						default:
							if (false)
							{
							}
							goto IL_104;
						}
						break;
					case 5:
					{
						if (num3 >= A_2)
						{
							num2 = 9;
							continue;
						}
						b2 = A_0.ReadByte(A_1++);
						byte b = b2;
						goto IL_139;
					}
					case 6:
						goto IL_188;
					case 7:
						num2 = 10;
						continue;
					case 8:
						return A_1;
					case 9:
						num++;
						num2 = 0;
						continue;
					case 10:
						goto IL_1EA;
					case 11:
						goto IL_171;
					case 12:
						num2 = 1;
						continue;
					case 13:
						goto IL_171;
					case 14:
						goto IL_171;
					case 15:
						goto IL_104;
					case 16:
						if (num >= A_3)
						{
							num2 = 8;
							continue;
						}
						if (true)
						{
						}
						num3 = 0;
						num2 = 4;
						continue;
					case 17:
						goto IL_171;
					}
					break;
					IL_104:
					num2 = 5;
					continue;
					IL_139:
					num2 = 3;
					continue;
					IL_171:
					num3++;
					num2 = 15;
					continue;
					IL_188:
					num2 = 16;
				}
			}
			IL_87:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("永唺嘼儾⹀㑂⭄杆㵈㉊㵌⩎煐㩒㭔睖ⵘᩚ⽜ⵞ`ᩢ彤䝦", a_) + b2);
			IL_1EA:
			goto IL_87;
		}
		}
	}

	// Token: 0x06003DD4 RID: 15828 RVA: 0x00226DC8 File Offset: 0x00225DC8
	private int ᜀ(byte[] A_0, int A_1, int A_2, int A_3)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			byte b2;
			for (;;)
			{
				this.ᜉ = new object[A_3, A_2];
				int num = 0;
				int num2 = 3;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2E0;
						default:
							if (false)
							{
							}
							if (A_1 + 8 > A_0.Length)
							{
								num2 = 6;
								continue;
							}
							this.ᜉ[num, num3] = A_0[A_1];
							A_1 += 8;
							num2 = 19;
							continue;
						}
						break;
					case 1:
					{
						if (A_1 + 8 > A_0.Length)
						{
							num2 = 5;
							continue;
						}
						bool flag = BitConverter.ToBoolean(A_0, A_1);
						this.ᜉ[num, num3] = flag;
						A_1 += 8;
						num2 = 12;
						continue;
					}
					case 2:
						if (num >= A_3)
						{
							num2 = 15;
							continue;
						}
						num3 = 0;
						num2 = 9;
						continue;
					case 3:
						goto IL_1CD;
					case 4:
					{
						byte b;
						if (b != 16)
						{
							num2 = 16;
							continue;
						}
						goto IL_2E0;
					}
					case 5:
						goto IL_34F;
					case 6:
						goto IL_327;
					case 7:
						goto IL_101;
					case 8:
					{
						if (num3 >= A_2)
						{
							num2 = 13;
							continue;
						}
						b2 = A_0[A_1++];
						byte b = b2;
						num2 = 17;
						continue;
					}
					case 9:
						goto IL_1AB;
					case 10:
					{
						if (A_1 + 8 > A_0.Length)
						{
							num2 = 21;
							continue;
						}
						double num4 = BitConverter.ToDouble(A_0, A_1);
						this.ᜉ[num, num3] = num4;
						A_1 += 8;
						num2 = 14;
						continue;
					}
					case 11:
						goto IL_1AB;
					case 12:
						goto IL_106;
					case 13:
						num++;
						num2 = 22;
						continue;
					case 14:
						goto IL_106;
					case 15:
						return A_1;
					case 16:
						num2 = 7;
						continue;
					case 17:
					{
						byte b;
						switch (b)
						{
						case 1:
							num2 = 10;
							continue;
						case 2:
						{
							int num5;
							string string16Bit = Ptg.GetString16Bit(A_0, A_1, out num5);
							this.ᜉ[num, num3] = string16Bit;
							A_1 += num5;
							num2 = 18;
							continue;
						}
						case 3:
							goto IL_2C1;
						case 4:
							num2 = 1;
							continue;
						default:
							num2 = 20;
							continue;
						}
						break;
					}
					case 18:
						goto IL_106;
					case 19:
						goto IL_106;
					case 20:
						num2 = 4;
						continue;
					case 21:
						goto IL_1A6;
					case 22:
						goto IL_1CD;
					}
					break;
					IL_106:
					num3++;
					num2 = 11;
					continue;
					IL_1AB:
					num2 = 8;
					continue;
					IL_1CD:
					num2 = 2;
					continue;
					IL_2E0:
					num2 = 0;
				}
			}
			IL_101:
			goto IL_2C1;
			IL_1A6:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("཈≊⅌⍎ᵐ㩒♔⍖捘筚㥜㹞ᕠɢ䕤٦᭨ᥪ౬᙮兰ݲᩴᡶ奸ࡺၼṾꮄ", a_));
			IL_2C1:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("᱈╊♌ⅎ㹐⑒㭔睖ⵘ≚ⵜ㩞䅠੢୤䝦ᵨ⩪Ὤᵮၰੲ佴坶", a_) + b2);
			IL_327:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("཈≊⅌⍎ᵐ㩒♔⍖捘筚㥜㹞ᕠɢ䕤٦᭨ᥪ౬᙮兰ݲᩴᡶ奸ࡺၼṾꮄ", a_));
			IL_34F:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("཈≊⅌⍎ᵐ㩒♔⍖捘筚㥜㹞ᕠɢ䕤٦᭨ᥪ౬᙮兰ݲᩴᡶ奸ࡺၼṾꮄ", a_));
		}
		}
	}

	// Token: 0x06003DD5 RID: 15829 RVA: 0x0022712C File Offset: 0x0022612C
	private void ᜀ(List<string>[] A_0, FormulaUtil A_1)
	{
		int a_ = 18;
		switch (0)
		{
		default:
			for (;;)
			{
				int num = A_0.Length;
				int count = A_0[0].Count;
				this.ᜇ = (byte)(count - 1);
				this.ᜈ = (ushort)(num - 1);
				this.ᜉ = new object[num, count];
				int num2 = 0;
				int num3 = 3;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						if (num2 < num)
						{
							if (true)
							{
							}
							num3 = 8;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_15C;
						default:
							if (false)
							{
							}
							num3 = 9;
							continue;
						}
						break;
					case 1:
						goto IL_83;
					case 2:
						goto IL_83;
					case 3:
						goto IL_E8;
					case 4:
						goto IL_15C;
					case 5:
						goto IL_E8;
					case 6:
						goto IL_157;
					case 7:
					{
						int num4;
						if (num4 >= count)
						{
							num3 = 4;
							continue;
						}
						this.ᜉ[num2, num4] = this.ᜀ(A_0[num2][num4], A_1);
						num4++;
						num3 = 1;
						continue;
					}
					case 8:
					{
						if (A_0[num2].Count != count)
						{
							num3 = 6;
							continue;
						}
						int num4 = 0;
						num3 = 2;
						continue;
					}
					case 9:
						return;
					}
					break;
					IL_83:
					num3 = 7;
					continue;
					IL_E8:
					num3 = 0;
					continue;
					IL_15C:
					num2++;
					num3 = 5;
				}
			}
			return;
			IL_157:
			this.ᜉ = null;
			throw new ArgumentException(RecordTableEnumerator.b("േ⭉⽋♍灏⁑㭓⅕硗㍙㉛繝ᑟ੡ţ䙥ᱧ⭩ṫᱭᅯୱ味᭵൷ॹࡻ幽ꢇ黎늑望鍊肟춡슣蚥쮧얩삫\udbad\uddaf\udcb1잳颵", a_));
		}
	}

	// Token: 0x06003DD6 RID: 15830 RVA: 0x002272C0 File Offset: 0x002262C0
	private object ᜀ(string A_0, FormulaUtil A_1)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 3;
			sprἪ sprἪ;
			double num2;
			for (;;)
			{
				Ptg[] array;
				switch (num)
				{
				case 0:
					if ('"' == A_0[A_0.Length - 1])
					{
						if (true)
						{
						}
						num = 7;
						continue;
					}
					goto IL_12F;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9A;
					default:
						goto IL_E7;
					}
					break;
				case 2:
					if (array.Length == 1)
					{
						num = 11;
						continue;
					}
					goto IL_161;
				case 4:
					num = 2;
					continue;
				case 5:
					if (array != null)
					{
						num = 4;
						continue;
					}
					goto IL_161;
				case 6:
					if (A_0[0] == '"')
					{
						num = 9;
						continue;
					}
					goto IL_12F;
				case 7:
					goto IL_CC;
				case 8:
					if (sprἪ != null)
					{
						num = 13;
						continue;
					}
					goto IL_161;
				case 9:
					goto IL_9A;
				case 10:
					if (double.TryParse(A_0, NumberStyles.Any, A_1.NumberFormat, out num2))
					{
						num = 1;
						continue;
					}
					try
					{
						bool flag = bool.Parse(A_0.ToLower());
						return flag;
					}
					catch (FormatException)
					{
						goto IL_163;
					}
					goto IL_1A4;
					IL_163:
					num = 6;
					continue;
				case 11:
					goto IL_1A4;
				case 12:
					goto IL_75;
				case 13:
					goto IL_1C8;
				}
				if (A_0.Length == 0)
				{
					num = 12;
					continue;
				}
				num = 10;
				continue;
				IL_9A:
				num = 0;
				continue;
				IL_12F:
				array = A_1.ᜃ(A_0);
				num = 5;
				continue;
				IL_1A4:
				sprἪ = (array[0] as sprἪ);
				num = 8;
			}
			IL_75:
			throw new ArgumentException(RecordTableEnumerator.b("̿ⵁ⩃㕅㱇⭉≋㩍灏⅑⁓⑕ㅗ㑙㭛繝͟͡੣䅥ᱧ䩩๫୭偯᝱ᥳٵ౷͹剻", a_));
			IL_CC:
			return A_0.Substring(1, A_0.Length - 2);
			IL_E7:
			if (false)
			{
			}
			return num2;
			IL_161:
			return null;
			IL_1C8:
			return sprἪ.ᜀ();
		}
		}
	}

	// Token: 0x06003DD7 RID: 15831 RVA: 0x002274D0 File Offset: 0x002264D0
	public spr\u177A ᜂ()
	{
		int a_ = 17;
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u177A spr_u177A = new spr\u177A(false);
				spr_u177A.ᜀ(this.ᜇ);
				spr_u177A.ᜀ(BitConverter.GetBytes(this.ᜈ));
				object[,] array = this.ᜉ;
				int upperBound = array.GetUpperBound(0);
				int upperBound2 = array.GetUpperBound(1);
				int num = array.GetLowerBound(0);
				int num2 = 22;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						goto IL_1E1;
					case 1:
						goto IL_26F;
					case 2:
					{
						object obj;
						if (obj is string)
						{
							num2 = 18;
							continue;
						}
						num2 = 6;
						continue;
					}
					case 3:
					{
						if (num3 > upperBound2)
						{
							num2 = 8;
							continue;
						}
						if (true)
						{
						}
						object obj = array[num, num3];
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_29D;
						default:
							if (false)
							{
							}
							num2 = 13;
							continue;
						}
						break;
					}
					case 4:
						goto IL_139;
					case 5:
						goto IL_26F;
					case 6:
					{
						object obj;
						if (obj is bool)
						{
							goto IL_29D;
						}
						num2 = 21;
						continue;
					}
					case 7:
						goto IL_26F;
					case 8:
						num++;
						num2 = 0;
						continue;
					case 9:
						spr_u177A.ᜀ(0);
						spr_u177A.ᜀ(this.ᜀ());
						num2 = 1;
						continue;
					case 10:
					{
						object obj;
						if (obj == null)
						{
							num2 = 9;
							continue;
						}
						goto IL_D2;
					}
					case 11:
						if (num > upperBound)
						{
							num2 = 17;
							continue;
						}
						num3 = array.GetLowerBound(1);
						num2 = 4;
						continue;
					case 12:
					{
						spr_u177A.ᜀ(1);
						object obj;
						spr_u177A.ᜀ(this.ᜀ((double)obj));
						num2 = 14;
						continue;
					}
					case 13:
					{
						object obj;
						if (obj is double)
						{
							num2 = 12;
							continue;
						}
						num2 = 2;
						continue;
					}
					case 14:
						goto IL_26F;
					case 15:
					{
						spr_u177A.ᜀ(4);
						object obj;
						spr_u177A.ᜀ(this.ᜀ((bool)obj));
						num2 = 16;
						continue;
					}
					case 16:
						goto IL_26F;
					case 17:
						return spr_u177A;
					case 18:
					{
						spr_u177A.ᜀ(2);
						object obj;
						spr_u177A.ᜀ(this.ᜀ((string)obj));
						num2 = 7;
						continue;
					}
					case 19:
						goto IL_139;
					case 20:
					{
						spr_u177A.ᜀ(16);
						object obj;
						spr_u177A.ᜀ(this.ᜀ((byte)obj));
						num2 = 5;
						continue;
					}
					case 21:
					{
						object obj;
						if (obj is byte)
						{
							num2 = 20;
							continue;
						}
						num2 = 10;
						continue;
					}
					case 22:
						goto IL_1E1;
					}
					break;
					IL_139:
					num2 = 3;
					continue;
					IL_1E1:
					num2 = 11;
					continue;
					IL_26F:
					num3++;
					num2 = 19;
					continue;
					IL_29D:
					num2 = 15;
				}
			}
			IL_D2:
			throw new ArrayTypeMismatchException(RecordTableEnumerator.b("ቆ❈⹊㕌㽎㑐げ⅔㉖㵘筚⥜♞ᅠ٢䕤๦ݨ䭪ᥬ⹮ͰŲᑴ๶坸", a_));
		}
	}

	// Token: 0x06003DD8 RID: 15832 RVA: 0x00227818 File Offset: 0x00226818
	private byte[] ᜀ(bool A_0)
	{
		byte[] array2;
		for (;;)
		{
			byte[] array = new byte[8];
			array2 = array;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					for (;;)
					{
						array2[0] = 1;
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_54;
						}
					}
					IL_54:
					if (false)
					{
					}
					num = 2;
					continue;
				case 1:
					if (A_0)
					{
						num = 0;
						continue;
					}
					return array2;
				case 2:
					return array2;
				}
				break;
			}
		}
		return array2;
	}

	// Token: 0x06003DD9 RID: 15833 RVA: 0x00227894 File Offset: 0x00226894
	private byte[] ᜀ(byte A_0)
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
		byte[] array = new byte[8];
		array[0] = A_0;
		return array;
	}

	// Token: 0x06003DDA RID: 15834 RVA: 0x002278E0 File Offset: 0x002268E0
	private byte[] ᜀ()
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
		return new byte[8];
	}

	// Token: 0x06003DDB RID: 15835 RVA: 0x00227928 File Offset: 0x00226928
	private byte[] ᜀ(double A_0)
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
		return BitConverter.GetBytes(A_0);
	}

	// Token: 0x06003DDC RID: 15836 RVA: 0x0022796C File Offset: 0x0022696C
	private byte[] ᜀ(string A_0)
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
		byte[] bytes = Encoding.Unicode.GetBytes(A_0);
		byte[] array = new byte[bytes.Length + 3];
		bytes.CopyTo(array, 3);
		BitConverter.GetBytes((ushort)A_0.Length).CopyTo(array, 0);
		array[2] = 1;
		return array;
	}

	// Token: 0x06003DDD RID: 15837 RVA: 0x002279E0 File Offset: 0x002269E0
	private void ᜁ(int A_0)
	{
		int a_ = 16;
		for (;;)
		{
			IL_1D:
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_71;
				case 1:
					num = 0;
					continue;
				case 2:
					switch (A_0)
					{
					case 1:
						goto IL_60;
					case 2:
						goto IL_57;
					case 3:
						goto IL_8F;
					default:
						num = 1;
						continue;
					}
					break;
				}
				goto IL_1D;
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
		}
		IL_57:
		this.TokenCode = FormulaToken.tArray2;
		return;
		IL_60:
		this.TokenCode = FormulaToken.tArray1;
		return;
		IL_87:
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⽅♇⹉⥋㙍", a_));
		IL_8F:
		this.TokenCode = FormulaToken.tArray3;
	}

	// Token: 0x06003DDE RID: 15838 RVA: 0x00227A98 File Offset: 0x00226A98
	public virtual int ᜁ(ExcelVersion A_0)
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
		return 8;
	}

	// Token: 0x06003DDF RID: 15839 RVA: 0x00227AD4 File Offset: 0x00226AD4
	public virtual string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			string text;
			for (;;)
			{
				text = string.Empty;
				text += RecordTableEnumerator.b("䐾", a_);
				string operandsSeparator = spr\u2372.ᜆ;
				string arrayRowSeparator = spr\u2372.ᜅ;
				int num = 1;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						operandsSeparator = A_0.OperandsSeparator;
						arrayRowSeparator = A_0.ArrayRowSeparator;
						goto IL_AF;
					case 1:
						if (A_0 != null)
						{
							num = 0;
							continue;
						}
						goto IL_C7;
					case 2:
						goto IL_C7;
					case 3:
						if (num2 != (int)this.ᜈ)
						{
							num = 10;
							continue;
						}
						goto IL_239;
					case 4:
						goto IL_239;
					case 5:
						if (num2 <= (int)this.ᜈ)
						{
							int num3 = 0;
							num = 8;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AF;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						break;
					case 6:
						goto IL_28E;
					case 7:
						if (true)
						{
						}
						goto IL_172;
					case 8:
						goto IL_172;
					case 9:
						goto IL_24D;
					case 10:
						text += arrayRowSeparator;
						num = 4;
						continue;
					case 11:
						num = 13;
						continue;
					case 12:
					{
						int num3;
						if (num3 >= (int)this.ᜇ)
						{
							num = 11;
							continue;
						}
						num = 14;
						continue;
					}
					case 13:
					{
						int num3;
						text += ((this.ᜉ[num2, num3] is string) ? ('"' + this.ᜉ[num2, num3].ToString() + '"') : this.ᜉ[num2, (int)this.ᜇ]);
						num = 3;
						continue;
					}
					case 14:
					{
						int num3;
						text += ((this.ᜉ[num2, num3] is string) ? ('"' + this.ᜉ[num2, num3].ToString() + '"') : this.ᜉ[num2, num3]);
						text += operandsSeparator;
						num3++;
						num = 7;
						continue;
					}
					case 15:
						goto IL_24D;
					}
					break;
					IL_AF:
					num = 2;
					continue;
					IL_C7:
					num2 = 0;
					num = 15;
					continue;
					IL_172:
					num = 12;
					continue;
					IL_239:
					num2++;
					num = 9;
					continue;
					IL_24D:
					num = 5;
				}
			}
			IL_28E:
			return text + RecordTableEnumerator.b("䈾", a_);
		}
		}
	}

	// Token: 0x06003DE0 RID: 15840 RVA: 0x00227D88 File Offset: 0x00226D88
	public virtual byte[] ᜀ(ExcelVersion A_0)
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
		byte[] array = base.ToByteArray(A_0);
		array[1] = 0;
		return array;
	}

	// Token: 0x06003DE1 RID: 15841 RVA: 0x00227DD0 File Offset: 0x00226DD0
	public static FormulaToken ᜀ(int A_0)
	{
		int a_ = 12;
		for (;;)
		{
			IL_1D:
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					goto IL_5D;
				case 2:
					switch (A_0)
					{
					case 1:
						return FormulaToken.tArray1;
					case 2:
						return FormulaToken.tArray2;
					case 3:
						return FormulaToken.tArray3;
					default:
						num = 0;
						continue;
					}
					break;
				}
				goto IL_1D;
			}
			IL_5D:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_73;
			}
		}
		return FormulaToken.tArray2;
		IL_73:
		if (true)
		{
		}
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭁⩃≅ⵇ㉉", a_));
	}

	// Token: 0x06003DE2 RID: 15842 RVA: 0x00227E78 File Offset: 0x00226E78
	public object ᜃ()
	{
		switch (0)
		{
		default:
		{
			object result;
			for (;;)
			{
				result = base.Clone();
				object[,] array = this.ᜉ;
				int num = 5;
				for (;;)
				{
					int num2;
					int num3;
					int length2;
					switch (num)
					{
					case 0:
						goto IL_129;
					case 1:
						num2++;
						num = 3;
						continue;
					case 2:
					{
						int length;
						if (num2 >= length)
						{
							num = 9;
							continue;
						}
						num3 = 0;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_135;
						default:
							if (false)
							{
							}
							num = 7;
							continue;
						}
						break;
					}
					case 3:
						if (true)
						{
						}
						goto IL_D3;
					case 4:
					{
						int length = array.GetLength(0);
						length2 = array.GetLength(1);
						num2 = 0;
						num = 8;
						continue;
					}
					case 5:
						if (this.ᜉ != null)
						{
							num = 4;
							continue;
						}
						return result;
					case 6:
						goto IL_135;
					case 7:
						goto IL_129;
					case 8:
						goto IL_D3;
					case 9:
						return result;
					}
					break;
					IL_135:
					if (num3 >= length2)
					{
						num = 1;
						continue;
					}
					this.ᜉ[num2, num3] = array[num2, num3];
					num3++;
					num = 0;
					continue;
					IL_D3:
					num = 2;
					continue;
					IL_129:
					num = 6;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06003DE3 RID: 15843 RVA: 0x00227FD4 File Offset: 0x00226FD4
	public virtual void ᜀ(DataProvider A_0, ref int A_1, ExcelVersion A_2)
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
		A_1 += this.GetSize(A_2) - 1;
	}

	// Token: 0x06003DE4 RID: 15844 RVA: 0x00228020 File Offset: 0x00227020
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u2372()
	{
		int a_ = 15;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		spr\u2372.ᜅ = RecordTableEnumerator.b("繄", a_);
		spr\u2372.ᜆ = RecordTableEnumerator.b("楄", a_);
	}

	// Token: 0x04001A95 RID: 6805
	public const byte ᜀ = 1;

	// Token: 0x04001A96 RID: 6806
	public const byte ᜁ = 2;

	// Token: 0x04001A97 RID: 6807
	public const byte ᜂ = 4;

	// Token: 0x04001A98 RID: 6808
	public const byte ᜃ = 16;

	// Token: 0x04001A99 RID: 6809
	public const byte ᜄ = 0;

	// Token: 0x04001A9A RID: 6810
	public static readonly string ᜅ;

	// Token: 0x04001A9B RID: 6811
	public static readonly string ᜆ;

	// Token: 0x04001A9C RID: 6812
	private byte ᜇ;

	// Token: 0x04001A9D RID: 6813
	private ushort ᜈ;

	// Token: 0x04001A9E RID: 6814
	private object[,] ᜉ;
}
