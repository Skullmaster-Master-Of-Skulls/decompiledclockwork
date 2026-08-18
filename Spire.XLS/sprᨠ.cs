using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Spire.Xls;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004B2 RID: 1202
[DefaultMember("Item")]
internal class sprᨠ : XlsObject
{
	// Token: 0x06004A40 RID: 19008 RVA: 0x002CE0A4 File Offset: 0x002CD0A4
	private sprᨠ(spr\u2158 A_0, object A_1)
	{
		this.ᜊ = -1;
		this.ᜋ = -1;
		this.ᜌ = -1;
		this.ᜐ = -1;
		this.\u1715 = -1;
		this.\u1716 = -1;
		base..ctor(A_0, A_1);
	}

	// Token: 0x06004A41 RID: 19009 RVA: 0x002CE0E4 File Offset: 0x002CD0E4
	public sprᨠ(spr\u2158 A_0, object A_1, List<sprἏ> A_2)
	{
		int a_ = 11;
		this.ᜊ = -1;
		this.ᜋ = -1;
		this.ᜌ = -1;
		this.ᜐ = -1;
		this.\u1715 = -1;
		this.\u1716 = -1;
		base..ctor(A_0, A_1);
		if (A_2 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("㕀ⱂ⹄≆❈㡊", a_));
		}
		this.ᜈ = new List<sprἏ>(A_2);
		this.ᜊ();
	}

	// Token: 0x06004A42 RID: 19010 RVA: 0x002CE158 File Offset: 0x002CD158
	public void ᜊ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_F6:
			goto IL_325;
		default:
			if (false)
			{
			}
			num = 2;
			break;
		}
		for (;;)
		{
			IL_30:
			switch (num)
			{
			case 0:
			{
				sprᲸ sprᲸ;
				this.\u171C = sprᲸ.ᜁ();
				num = 19;
				continue;
			}
			case 1:
				this.\u171A = this.ᜈ();
				num = 20;
				continue;
			case 3:
				goto IL_21D;
			case 4:
			{
				sprᲸ sprᲸ = this.ᜈ[0] as sprᲸ;
				num = 16;
				continue;
			}
			case 5:
				if (this.ᜊ > 0)
				{
					num = 18;
					continue;
				}
				goto IL_1A7;
			case 6:
				this.ᜎ = this.ᜄ();
				this.ᜏ = this.ᜃ();
				this.ᜂ();
				num = 11;
				continue;
			case 7:
				goto IL_1A7;
			case 8:
				if (this.ᜈ.Count > 0)
				{
					num = 4;
					continue;
				}
				return;
			case 9:
				if (this.ᜑ() == CellFormatType.Number)
				{
					num = 1;
					continue;
				}
				goto IL_122;
			case 10:
				if (this.ᜑ() == CellFormatType.Number)
				{
					num = 6;
					continue;
				}
				num = 17;
				continue;
			case 11:
				goto IL_2C5;
			case 12:
				goto IL_2C5;
			case 13:
				goto IL_2C5;
			case 14:
				return;
			case 15:
				goto IL_F6;
			case 16:
			{
				sprᲸ sprᲸ;
				if (sprᲸ != null)
				{
					num = 0;
					continue;
				}
				return;
			}
			case 17:
				if (this.ᜑ() == CellFormatType.DateTime)
				{
					num = 21;
					continue;
				}
				this.ᜎ = -1;
				this.ᜏ = -1;
				num = 13;
				continue;
			case 18:
				this.\u1715 = this.ᜊ - 1;
				num = 7;
				continue;
			case 19:
				return;
			case 20:
				goto IL_122;
			case 21:
				this.ᜀ();
				this.ᜊ = -1;
				this.ᜑ = false;
				num = 12;
				continue;
			case 22:
				this.ᜂ();
				this.\u1715 = this.\u1712 - 1;
				num = 15;
				continue;
			case 23:
				if (this.ᜋ > 0)
				{
					num = 24;
					continue;
				}
				num = 25;
				continue;
			case 24:
				this.\u1715 = (this.\u1716 = this.ᜋ - 1);
				num = 3;
				continue;
			case 25:
				if (this.ᜑ)
				{
					num = 22;
					continue;
				}
				goto IL_325;
			}
			if (this.ᜉ)
			{
				num = 14;
				continue;
			}
			this.ᜇ();
			this.ᜌ = this.ᜅ();
			this.\u170D = this.ᜁ(this.ᜌ + 1);
			num = 10;
			continue;
			IL_122:
			this.ᜉ = true;
			if (true)
			{
			}
			num = 8;
			continue;
			IL_1A7:
			num = 9;
			continue;
			IL_2C5:
			int num2 = this.\u1712();
			this.\u1716 = num2 - 1;
			this.\u1715 = num2 - 1;
			num = 23;
		}
		return;
		IL_21D:
		IL_325:
		num = 5;
		goto IL_30;
	}

	// Token: 0x06004A43 RID: 19011 RVA: 0x002CE4B4 File Offset: 0x002CD4B4
	private bool ᜈ()
	{
		for (;;)
		{
			IL_30:
			int num = 0;
			int num2 = this.\u1715 - 1;
			for (;;)
			{
				int num3 = 4;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						if (this.ᜂ(num + 1) is spr\u20C6)
						{
							num3 = 8;
							continue;
						}
						goto IL_45;
					case 1:
						num3 = 6;
						continue;
					case 2:
						return false;
					case 3:
						goto IL_C1;
					case 4:
						goto IL_C1;
					case 5:
						if (num >= num2)
						{
							num3 = 2;
							continue;
						}
						num3 = 9;
						continue;
					case 6:
						if (this.ᜂ(num - 1) is spr\u20C6)
						{
							num3 = 7;
							continue;
						}
						goto IL_45;
					case 7:
						if (true)
						{
						}
						num3 = 0;
						continue;
					case 8:
						goto IL_7B;
					case 9:
						if (this.ᜂ(num).ᜀ() == TokenType.ThousandsSeparator)
						{
							num3 = 1;
							continue;
						}
						goto IL_45;
					}
					goto IL_30;
					IL_45:
					num++;
					num3 = 3;
					continue;
					IL_C1:
					num3 = 5;
				}
				IL_7B:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_91;
				}
			}
		}
		IL_91:
		if (false)
		{
		}
		return true;
	}

	// Token: 0x06004A44 RID: 19012 RVA: 0x002CE5DC File Offset: 0x002CD5DC
	private void ᜇ()
	{
		int a_ = 16;
		switch (0)
		{
		default:
			for (;;)
			{
				bool flag = false;
				this.\u171B = false;
				int num = 0;
				int num2 = this.\u1712();
				int num3 = 29;
				for (;;)
				{
					sprᩆ sprᩆ;
					switch (num3)
					{
					case 0:
						num3 = 15;
						continue;
					case 1:
						if (this.ᜊ < 0)
						{
							num3 = 16;
							continue;
						}
						this.\u171B = true;
						num3 = 24;
						continue;
					case 2:
					{
						spr\u20C6 spr_u20C;
						spr_u20C.ᜁ(true);
						flag = true;
						num3 = 25;
						continue;
					}
					case 3:
					{
						if (num >= num2)
						{
							num3 = 22;
							continue;
						}
						sprἏ sprἏ = this.ᜂ(num);
						TokenType tokenType = sprἏ.ᜀ();
						num3 = 23;
						continue;
					}
					case 4:
						goto IL_186;
					case 5:
						goto IL_280;
					case 6:
					{
						if (this.\u1717 != null)
						{
							num3 = 4;
							continue;
						}
						sprἏ sprἏ;
						this.\u1717 = (spr\u262F)sprἏ;
						num3 = 9;
						continue;
					}
					case 7:
						goto IL_18B;
					case 8:
						goto IL_18B;
					case 9:
						goto IL_18B;
					case 10:
						goto IL_2EC;
					case 11:
						goto IL_18B;
					case 12:
						num3 = 18;
						continue;
					case 13:
						goto IL_18B;
					case 14:
						goto IL_1E7;
					case 15:
					{
						TokenType tokenType;
						switch (tokenType)
						{
						case TokenType.AmPm:
							sprᩆ = this.ᜃ(num);
							num3 = 21;
							continue;
						case TokenType.Color:
						case TokenType.Text:
						case TokenType.Percent:
						case TokenType.General:
						case TokenType.ThousandsSeparator:
						case TokenType.Asterix:
						case TokenType.MilliSecond:
							goto IL_18B;
						case TokenType.Condition:
							num3 = 6;
							continue;
						case TokenType.SignificantDigit:
						case TokenType.InsignificantDigit:
						case TokenType.PlaceReservedDigit:
						{
							if (true)
							{
							}
							sprἏ sprἏ;
							spr\u20C6 spr_u20C = (spr\u20C6)sprἏ;
							num3 = 20;
							continue;
						}
						case TokenType.Scientific:
							this.ᜀ(ref this.ᜋ, num);
							num3 = 30;
							continue;
						case TokenType.DecimalPoint:
							num3 = 1;
							continue;
						case TokenType.Fraction:
							num3 = 26;
							continue;
						case TokenType.Culture:
							num3 = 27;
							continue;
						default:
							num3 = 12;
							continue;
						}
						break;
					}
					case 16:
						this.ᜀ(ref this.ᜊ, num);
						num3 = 17;
						continue;
					case 17:
						goto IL_18B;
					case 18:
						goto IL_18B;
					case 19:
						goto IL_18B;
					case 20:
						if (!flag)
						{
							num3 = 2;
							continue;
						}
						goto IL_18B;
					case 21:
						if (sprᩆ != null)
						{
							num3 = 5;
							continue;
						}
						goto IL_18B;
					case 22:
						goto IL_206;
					case 23:
					{
						TokenType tokenType;
						if (tokenType != TokenType.Minute)
						{
							num3 = 0;
							continue;
						}
						this.ᜀ(num);
						num3 = 7;
						continue;
					}
					case 24:
						goto IL_18B;
					case 25:
						goto IL_18B;
					case 26:
						if (this.ᜐ < 0)
						{
							num3 = 28;
							continue;
						}
						this.ᜑ = false;
						num3 = 13;
						continue;
					case 27:
					{
						if (this.\u1718 != null)
						{
							num3 = 10;
							continue;
						}
						sprἏ sprἏ;
						this.\u1718 = (sprᲸ)sprἏ;
						num3 = 19;
						continue;
					}
					case 28:
						this.ᜐ = num;
						this.ᜑ = true;
						num3 = 11;
						continue;
					case 29:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_280;
						}
						if (false)
						{
						}
						goto IL_1E7;
					case 30:
						goto IL_18B;
					}
					break;
					IL_18B:
					num++;
					num3 = 14;
					continue;
					IL_1E7:
					num3 = 3;
					continue;
					IL_280:
					sprᩆ.ᜀ(true);
					num3 = 8;
				}
			}
			IL_186:
			throw new FormatException(RecordTableEnumerator.b("Ʌ㵇㩉⁋❍㍏㍑⁓㍕㱗穙㽛ㅝ๟١ൣብŧթɫ乭ݯ፱ݳ噵ṷᕹॻၽꊁꢇﺉ낏躟", a_));
			IL_206:
			this.ᜆ();
			return;
			IL_2EC:
			throw new FormatException(RecordTableEnumerator.b("Ʌ㵇㩉⁋❍㍏㍑⁓㍕㱗穙㽛⭝౟ᙡᅣᑥ൧䩩իmᙯᵱٳ᭵᥷๹ᕻᅽꊁﮇꪉﲑ뚕벛좟잡蒣향춧즩\ud8ab잭\udfaf\udcb1骳", a_));
		}
	}

	// Token: 0x06004A45 RID: 19013 RVA: 0x002CE9EC File Offset: 0x002CD9EC
	private void ᜆ()
	{
		int num = 5;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				num2 = this.\u1712() - 1;
				num = 3;
				continue;
			case 1:
			{
				spr\u20C6 spr_u20C;
				if (spr_u20C != null)
				{
					if (true)
					{
					}
					num = 10;
					continue;
				}
				goto IL_52;
			}
			case 2:
			{
				sprẪ sprẪ;
				sprẪ.ᜀ(true);
				num = 8;
				continue;
			}
			case 3:
				goto IL_98;
			case 4:
				return;
			case 6:
				goto IL_98;
			case 7:
			{
				sprẪ sprẪ;
				if (sprẪ != null)
				{
					num = 2;
					continue;
				}
				return;
			}
			case 8:
				goto IL_52;
			case 9:
				if (num2 <= this.ᜊ)
				{
					num = 4;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_56;
				default:
				{
					if (false)
					{
					}
					spr\u20C6 spr_u20C = this.ᜈ[num2] as spr\u20C6;
					num = 1;
					continue;
				}
				}
				break;
			case 10:
			{
				spr\u20C6 spr_u20C;
				sprẪ sprẪ = spr_u20C as sprẪ;
				num = 7;
				continue;
			}
			}
			if (this.ᜊ >= 0)
			{
				num = 0;
				continue;
			}
			break;
			IL_56:
			num = 6;
			continue;
			IL_52:
			num2--;
			goto IL_56;
			IL_98:
			num = 9;
		}
	}

	// Token: 0x06004A46 RID: 19014 RVA: 0x002CEB2C File Offset: 0x002CDB2C
	public sprᩆ ᜃ(int A_0)
	{
		sprἏ sprἏ;
		for (;;)
		{
			int num = A_0;
			int num2 = 7;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_66;
				case 1:
					goto IL_C0;
				case 2:
					goto IL_45;
				case 3:
					num += this.\u1712();
					num2 = 2;
					continue;
				case 4:
					if (sprἏ.ᜀ() == TokenType.Hour)
					{
						num2 = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DB;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num2 = 6;
						continue;
					}
					break;
				case 5:
					if (num < 0)
					{
						num2 = 3;
						continue;
					}
					goto IL_45;
				case 6:
					if (num == A_0)
					{
						num2 = 1;
						continue;
					}
					goto IL_68;
				case 7:
					goto IL_68;
				}
				break;
				IL_45:
				sprἏ = this.ᜂ(num);
				num2 = 4;
				continue;
				IL_68:
				num--;
				num2 = 5;
			}
		}
		IL_66:
		return (sprᩆ)sprἏ;
		IL_C0:
		IL_DB:
		return null;
	}

	// Token: 0x06004A47 RID: 19015 RVA: 0x002CEC18 File Offset: 0x002CDC18
	public string ᜂ(double A_0)
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
		return this.ᜀ(A_0, false);
	}

	// Token: 0x06004A48 RID: 19016 RVA: 0x002CEC5C File Offset: 0x002CDC5C
	public string ᜀ(string A_0)
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
		return this.ᜀ(A_0, false);
	}

	// Token: 0x06004A49 RID: 19017 RVA: 0x002CECA0 File Offset: 0x002CDCA0
	public string ᜀ(double A_0, bool A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				this.ᜊ();
				this.ᜀ(ref A_0, A_1);
				int num = 20;
				for (;;)
				{
					double num5;
					bool flag;
					string text;
					int num7;
					switch (num)
					{
					case 0:
						if (this.ᜊ > 0)
						{
							num = 23;
							continue;
						}
						goto IL_2CC;
					case 1:
					{
						int num2 = this.ᜏ - 1;
						double num3;
						num3 *= Math.Pow(10.0, (double)num2);
						double num4;
						num4 -= (double)num2;
						A_0 = num3;
						num = 26;
						continue;
					}
					case 2:
						goto IL_2A3;
					case 3:
						num5 = A_0;
						num = 19;
						continue;
					case 4:
						goto IL_263;
					case 5:
						if (flag)
						{
							num = 18;
							continue;
						}
						A_0 += 1.0;
						num = 2;
						continue;
					case 6:
						goto IL_382;
					case 7:
						flag &= (num5 > 0.0);
						num = 4;
						continue;
					case 8:
						goto IL_2CC;
					case 9:
						return text;
					case 10:
					{
						A_0 = sprᨠ.ᜀ(A_0, out num5);
						double num6 = Math.Pow(10.0, (double)this.ᜎ);
						num5 *= num6;
						num5 = sprᨠ.ᜀ(num5);
						num = 13;
						continue;
					}
					case 11:
						return text;
					case 12:
						if (this.ᜋ > 0)
						{
							num = 28;
							continue;
						}
						num = 29;
						continue;
					case 13:
					{
						double num6;
						if (num5 >= num6)
						{
							num = 16;
							continue;
						}
						goto IL_2A3;
					}
					case 14:
						num5 -= ((A_0 > 0.0) ? Math.Floor(A_0) : Math.Ceiling(A_0));
						num5 = Math.Abs(num5);
						num = 6;
						continue;
					case 15:
						num = 22;
						continue;
					case 16:
					{
						double num6;
						num5 -= num6;
						num = 5;
						continue;
					}
					case 17:
						goto IL_2A3;
					case 18:
						A_0 -= 1.0;
						num = 17;
						continue;
					case 19:
						if (this.ᜀ(0, this.\u1715))
						{
							num = 21;
							continue;
						}
						goto IL_382;
					case 20:
					{
						if (this.\u171C)
						{
							num = 30;
							continue;
						}
						num7 = this.\u1712();
						num5 = 0.0;
						double num4 = Math.Floor(Math.Log10(Math.Abs(A_0)));
						double num3 = A_0 / Math.Pow(10.0, num4);
						num = 25;
						continue;
					}
					case 21:
						num = 14;
						continue;
					case 22:
						if (!this.ᜑ)
						{
							num = 10;
							continue;
						}
						goto IL_2A3;
					case 23:
						text += this.ᜀ(num5, A_1, this.ᜊ, this.\u1716, false);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_263;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					case 24:
						if (A_0 == 0.0)
						{
							num = 7;
							continue;
						}
						goto IL_263;
					case 25:
						if (this.ᜋ > 0)
						{
							num = 1;
							continue;
						}
						goto IL_41E;
					case 26:
						goto IL_41E;
					case 27:
						if (this.\u1719 == CellFormatType.Number)
						{
							num = 15;
							continue;
						}
						goto IL_2A3;
					case 28:
					{
						double num4;
						text += this.ᜀ(num4, A_1, this.\u1716 + 1, num7 - 1, false, false, num4 < 0.0);
						num = 11;
						continue;
					}
					case 29:
						if (this.ᜑ)
						{
							num = 3;
							continue;
						}
						return text;
					case 30:
						goto IL_C8;
					}
					break;
					IL_263:
					text = this.ᜀ(A_0, A_1, 0, this.\u1715, false, this.\u171A, flag);
					num = 0;
					continue;
					IL_2A3:
					num = 24;
					continue;
					IL_2CC:
					if (true)
					{
					}
					num = 12;
					continue;
					IL_382:
					spr\u228A spr_u228A = spr\u228A.ᜀ(num5, this.\u1714);
					long num8 = (long)spr_u228A.ᜂ();
					long num9 = (long)spr_u228A.ᜄ();
					text += this.ᜀ((double)num8, A_1, this.\u1715 + 1, this.ᜐ, false);
					text += this.ᜀ((double)num9, A_1, this.ᜐ + 1, this.\u1713, false);
					text += this.ᜀ(0.0, A_1, this.\u1713 + 1, num7 - 1, false);
					num = 9;
					continue;
					IL_41E:
					flag = (A_0 < 0.0);
					num = 27;
				}
			}
			IL_C8:
			return DateTime.FromOADate(A_0).ToLongDateString();
		}
	}

	// Token: 0x06004A4A RID: 19018 RVA: 0x002CF1BC File Offset: 0x002CE1BC
	public string ᜀ(string A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			string result;
			for (;;)
			{
				this.ᜊ();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1B2;
					case 1:
						if (this.\u1719 != CellFormatType.Text)
						{
							num = 6;
							continue;
						}
						goto IL_D3;
					case 2:
						goto IL_16B;
					case 3:
						goto IL_D3;
					case 4:
					{
						if (this.\u171C)
						{
							num = 15;
							continue;
						}
						int count = this.ᜈ.Count;
						result = string.Empty;
						num = 9;
						continue;
					}
					case 5:
						goto IL_8D;
					case 6:
						num = 12;
						continue;
					case 7:
					{
						int count;
						if (count != 1)
						{
							return result;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return A_0;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					}
					case 8:
					{
						sprἏ sprἏ = this.ᜈ[0];
						result = sprἏ.ᜀ(A_0, A_1);
						num = 0;
						continue;
					}
					case 9:
					{
						int count;
						if (count > 1)
						{
							num = 14;
							continue;
						}
						num = 7;
						continue;
					}
					case 10:
					{
						int num2;
						if (num2 < 0)
						{
							num = 13;
							continue;
						}
						if (true)
						{
						}
						sprἏ sprἏ = this.ᜈ[num2];
						string value = sprἏ.ᜀ(A_0, A_1);
						StringBuilder stringBuilder;
						stringBuilder.Insert(0, value);
						num2--;
						num = 2;
						continue;
					}
					case 11:
						goto IL_16B;
					case 12:
						if (this.\u1719 == CellFormatType.General)
						{
							num = 3;
							continue;
						}
						return A_0;
					case 13:
					{
						StringBuilder stringBuilder;
						result = stringBuilder.ToString();
						num = 5;
						continue;
					}
					case 14:
					{
						StringBuilder stringBuilder = new StringBuilder();
						int count;
						int num2 = count - 1;
						num = 11;
						continue;
					}
					case 15:
						goto IL_F6;
					}
					break;
					IL_D3:
					num = 4;
					continue;
					IL_16B:
					num = 10;
				}
			}
			IL_8D:
			return result;
			IL_F6:
			return A_0;
			IL_1B2:
			return result;
		}
		}
	}

	// Token: 0x06004A4B RID: 19019 RVA: 0x002CF3D0 File Offset: 0x002CE3D0
	private void ᜀ(ref int A_0, int A_1)
	{
		while (A_0 >= 0)
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
				if (true)
				{
				}
				throw new FormatException();
			}
		}
		A_0 = A_1;
	}

	// Token: 0x06004A4C RID: 19020 RVA: 0x002CF41B File Offset: 0x002CE41B
	private string ᜀ(double A_0, bool A_1, int A_2, int A_3, bool A_4)
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
		return this.ᜀ(A_0, A_1, A_2, A_3, A_4, false, false);
	}

	// Token: 0x06004A4D RID: 19021 RVA: 0x002CF45C File Offset: 0x002CE45C
	private string ᜀ(double A_0, bool A_1, int A_2, int A_3, bool A_4, bool A_5, bool A_6)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			StringBuilder stringBuilder;
			for (;;)
			{
				stringBuilder = new StringBuilder();
				int num = 27;
				for (;;)
				{
					int num2;
					int num3;
					int num4;
					int num5;
					double num7;
					CultureInfo a_3;
					int num8;
					int num9;
					int a_4;
					int a_5;
					switch (num)
					{
					case 0:
						num = 14;
						continue;
					case 1:
						num = 5;
						continue;
					case 2:
						num2 = A_2;
						goto IL_10B;
					case 3:
						num2 = A_3;
						goto IL_10B;
					case 4:
						num = 12;
						continue;
					case 5:
						if (num3 >= 0)
						{
							num = 4;
							continue;
						}
						goto IL_389;
					case 6:
						goto IL_15C;
					case 7:
						if (A_6)
						{
							num = 10;
							continue;
						}
						goto IL_15C;
					case 8:
						num4 = 1;
						goto IL_269;
					case 9:
						goto IL_15C;
					case 10:
						this.ᜀ(stringBuilder, A_4, RecordTableEnumerator.b("ᨶ", a_));
						A_6 = false;
						num = 9;
						continue;
					case 11:
						goto IL_178;
					case 12:
						if (A_6)
						{
							num = 19;
							continue;
						}
						goto IL_389;
					case 13:
						num5 = A_3;
						goto IL_30C;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_176;
						default:
							if (false)
							{
							}
							num5 = A_2;
							goto IL_30C;
						}
						break;
					case 15:
						goto IL_15C;
					case 16:
						if (!A_4)
						{
							num = 0;
							continue;
						}
						num = 13;
						continue;
					case 17:
						if (!A_4)
						{
							num = 25;
							continue;
						}
						num = 2;
						continue;
					case 18:
					{
						spr\u20C6 spr_u20C;
						if (spr_u20C != null)
						{
							num = 23;
							continue;
						}
						double num6 = num7;
						sprἏ sprἏ;
						string a_2 = sprἏ.ᜀ(ref num6, A_1, a_3, this);
						this.ᜀ(stringBuilder, A_4, a_2);
						num = 6;
						continue;
					}
					case 19:
						stringBuilder.Insert(stringBuilder.Length - num3, RecordTableEnumerator.b("ᨶ", a_));
						num = 20;
						continue;
					case 20:
						goto IL_375;
					case 21:
						if (A_4)
						{
							num = 28;
							continue;
						}
						num3 = stringBuilder.Length;
						num = 15;
						continue;
					case 22:
						num4 = -1;
						goto IL_269;
					case 23:
					{
						spr\u20C6 spr_u20C;
						spr_u20C.ᜀ(num7);
						a_4 = this.ᜀ(spr_u20C, num8, num9, ref A_0, a_4, stringBuilder, A_4, A_1, A_5);
						num = 21;
						continue;
					}
					case 24:
						num = 22;
						continue;
					case 25:
						num = 3;
						continue;
					case 26:
					{
						if (!this.ᜀ(a_5, A_4, num8))
						{
							num = 1;
							continue;
						}
						sprἏ sprἏ = this.ᜈ[num8];
						spr\u20C6 spr_u20C = sprἏ as spr\u20C6;
						num = 18;
						continue;
					}
					case 27:
						if (!A_4)
						{
							num = 24;
							continue;
						}
						num = 8;
						continue;
					case 28:
						num = 7;
						continue;
					case 29:
						goto IL_176;
					}
					break;
					IL_10B:
					num9 = num2;
					num = 16;
					continue;
					IL_15C:
					int num10;
					num8 += num10;
					if (true)
					{
					}
					num = 29;
					continue;
					IL_178:
					num = 26;
					continue;
					IL_176:
					goto IL_178;
					IL_269:
					num10 = num4;
					num = 17;
					continue;
					IL_30C:
					a_5 = num5;
					a_4 = 0;
					a_3 = this.\u170D();
					num3 = -1;
					num7 = A_0;
					num8 = num9;
					num = 11;
				}
			}
			IL_375:
			IL_389:
			return stringBuilder.ToString();
		}
		}
	}

	// Token: 0x06004A4E RID: 19022 RVA: 0x002CF7F8 File Offset: 0x002CE7F8
	private int ᜀ(spr\u20C6 A_0, int A_1, int A_2, ref double A_3, int A_4, StringBuilder A_5, bool A_6, bool A_7, bool A_8)
	{
		int a_ = 13;
		int num = 10;
		for (;;)
		{
			CultureInfo a_3;
			string a_2;
			switch (num)
			{
			case 0:
				return A_4;
			case 1:
				return A_4;
			case 2:
				num = 6;
				continue;
			case 3:
				goto IL_78;
			case 4:
				if (A_3 < 1.0)
				{
					num = 2;
					continue;
				}
				goto IL_78;
			case 5:
				goto IL_50;
			case 6:
			{
				bool flag;
				if (flag)
				{
					if (true)
					{
					}
					num = 8;
					continue;
				}
				return A_4;
			}
			case 7:
				if (A_0.ᜆ())
				{
					num = 9;
					continue;
				}
				a_2 = A_0.ᜀ(ref A_3, A_7, a_3, this);
				A_4 = this.ᜀ(A_1, A_2, A_4, a_2, A_5, A_6, A_8);
				num = 1;
				continue;
			case 8:
				A_3 = -A_3;
				num = 0;
				continue;
			case 9:
			{
				bool flag = A_3 < 0.0;
				A_3 = Math.Abs(A_3);
				num = 3;
				continue;
			}
			}
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_50;
			default:
				if (false)
				{
				}
				a_3 = this.\u170D();
				num = 7;
				continue;
			}
			IL_78:
			a_2 = A_0.ᜀ(ref A_3, A_7, a_3, this);
			A_4 = this.ᜀ(A_1, A_2, A_4, a_2, A_5, A_6, A_8);
			num = 4;
		}
		IL_50:
		throw new ArgumentNullException(RecordTableEnumerator.b("❂ⱄ⁆⁈㽊᥌⁎㩐㙒㭔", a_));
	}

	// Token: 0x06004A4F RID: 19023 RVA: 0x002CF994 File Offset: 0x002CE994
	private int ᜀ(int A_0, int A_1, int A_2, string A_3, StringBuilder A_4, bool A_5, bool A_6)
	{
		int a_ = 11;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (A_0 != A_1)
				{
					num = 6;
					continue;
				}
				goto IL_13F;
			case 2:
				num = 3;
				continue;
			case 3:
				if (A_3.Length <= 0)
				{
					goto IL_13F;
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
					num = 7;
					continue;
				}
				break;
			case 4:
				goto IL_54;
			case 5:
				if (true)
				{
				}
				this.ᜀ(A_4, A_5, base.ReservedHandle.ᜎ());
				A_2 = 1;
				num = 8;
				continue;
			case 6:
				num = 10;
				continue;
			case 7:
				num = 1;
				continue;
			case 8:
				goto IL_EC;
			case 9:
				if (this.\u171A)
				{
					num = 2;
					continue;
				}
				goto IL_13F;
			case 10:
				if (A_2 == 4)
				{
					num = 5;
					continue;
				}
				goto IL_13F;
			}
			IL_45:
			if (A_3 == null)
			{
				num = 4;
				continue;
			}
			A_2++;
			num = 9;
			continue;
			goto IL_45;
		}
		IL_54:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕀ⱂ⹄≆❈᥊⡌㱎⑐㽒⅔", a_));
		IL_EC:
		IL_13F:
		this.ᜀ(A_4, A_5, A_3);
		return A_2;
	}

	// Token: 0x06004A50 RID: 19024 RVA: 0x002CFAF0 File Offset: 0x002CEAF0
	private void ᜀ(StringBuilder A_0, bool A_1, string A_2)
	{
		int a_ = 1;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (A_1)
				{
					num = 4;
					continue;
				}
				goto IL_CD;
			case 2:
				if (A_2 == null)
				{
					num = 3;
					continue;
				}
				num = 1;
				continue;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_7B;
				}
				break;
			case 4:
				goto IL_5B;
			case 5:
				goto IL_46;
			}
			IL_3B:
			if (A_0 == null)
			{
				num = 5;
				continue;
			}
			num = 2;
			continue;
			goto IL_3B;
		}
		IL_46:
		throw new ArgumentNullException(RecordTableEnumerator.b("唶䰸刺儼嬾⑀ㅂ", a_));
		IL_5B:
		if (true)
		{
		}
		A_0.Append(A_2);
		return;
		IL_7B:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("愶堸场䠼娾", a_));
		IL_CD:
		A_0.Insert(0, A_2);
	}

	// Token: 0x06004A51 RID: 19025 RVA: 0x002CFBD4 File Offset: 0x002CEBD4
	private bool ᜀ(int A_0, bool A_1, int A_2)
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
			if (!A_1)
			{
				return A_2 >= A_0;
			}
			break;
		}
		return A_2 <= A_0;
	}

	// Token: 0x06004A52 RID: 19026 RVA: 0x002CFC24 File Offset: 0x002CEC24
	private int ᜅ()
	{
		switch (0)
		{
		default:
		{
			int result;
			for (;;)
			{
				result = -1;
				int num = this.\u1712();
				int num2 = 0;
				for (;;)
				{
					int num3;
					int num4;
					switch (num2)
					{
					case 0:
						if (this.ᜋ <= 0)
						{
							num2 = 8;
							continue;
						}
						num2 = 13;
						continue;
					case 1:
					{
						if (num3 < 0)
						{
							num2 = 2;
							continue;
						}
						sprἏ sprἏ = this.ᜂ(num3);
						spr\u20C6 spr_u20C = sprἏ as spr\u20C6;
						num2 = 18;
						continue;
					}
					case 2:
						goto IL_D0;
					case 3:
						num4 = num - 1;
						goto IL_1C9;
					case 4:
						if (this.ᜋ > 0)
						{
							num2 = 5;
							continue;
						}
						return result;
					case 5:
					{
						int num5 = this.ᜋ;
						num2 = 17;
						continue;
					}
					case 6:
						return result;
					case 7:
					{
						int num5;
						if (num5 >= num)
						{
							num2 = 19;
							continue;
						}
						spr\u20C6 spr_u20C2 = this.ᜂ(num5) as spr\u20C6;
						num2 = 16;
						continue;
					}
					case 8:
						num2 = 3;
						continue;
					case 9:
						goto IL_129;
					case 10:
						goto IL_191;
					case 11:
						goto IL_14A;
					case 12:
						goto IL_129;
					case 13:
						num4 = this.ᜋ - 1;
						goto IL_1C9;
					case 14:
					{
						spr\u20C6 spr_u20C2;
						spr_u20C2.ᜁ(true);
						num2 = 6;
						continue;
					}
					case 15:
						goto IL_D0;
					case 16:
					{
						spr\u20C6 spr_u20C2;
						if (spr_u20C2 != null)
						{
							num2 = 14;
							continue;
						}
						int num5;
						num5++;
						if (true)
						{
						}
						num2 = 10;
						continue;
					}
					case 17:
						goto IL_191;
					case 18:
					{
						spr\u20C6 spr_u20C;
						if (spr_u20C != null)
						{
							num2 = 11;
							continue;
						}
						num3--;
						num2 = 12;
						continue;
					}
					case 19:
						return result;
					}
					break;
					IL_D0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
					{
						IL_14A:
						int num6 = this.ᜊ;
						result = num3;
						num2 = 15;
						continue;
					}
					default:
						if (false)
						{
						}
						num2 = 4;
						continue;
					}
					IL_129:
					num2 = 1;
					continue;
					IL_191:
					num2 = 7;
					continue;
					IL_1C9:
					int num7 = num4;
					num3 = num7;
					num2 = 9;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06004A53 RID: 19027 RVA: 0x002CFE74 File Offset: 0x002CEE74
	private bool ᜁ(int A_0)
	{
		for (;;)
		{
			A_0 = Math.Max(this.ᜊ, A_0);
			A_0 = Math.Max(0, A_0);
			int num = this.\u1712();
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_81;
				case 1:
					return false;
				case 2:
				{
					if (A_0 >= num)
					{
						num2 = 1;
						continue;
					}
					spr\u2595 spr_u = this.ᜈ[A_0] as spr\u2595;
					bool result = spr_u != null;
					num2 = 6;
					continue;
				}
				case 3:
				{
					spr\u2595 spr_u = this.ᜈ[A_0] as spr\u2595;
					if (true)
					{
					}
					num2 = 0;
					continue;
				}
				case 4:
				{
					bool result;
					return result;
				}
				case 5:
				{
					if (A_0 < num)
					{
						num2 = 3;
						continue;
					}
					bool result;
					return result;
				}
				case 6:
					goto IL_B7;
				case 7:
				{
					spr\u2595 spr_u;
					if (spr_u != null)
					{
						spr_u.ᜀ(true);
						A_0++;
						num2 = 5;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_81;
					default:
						if (false)
						{
						}
						num2 = 4;
						continue;
					}
					break;
				}
				}
				break;
				IL_B7:
				num2 = 7;
				continue;
				IL_81:
				goto IL_B7;
			}
		}
		return false;
	}

	// Token: 0x06004A54 RID: 19028 RVA: 0x002CFF98 File Offset: 0x002CEF98
	private void ᜁ(ref double A_0, bool A_1)
	{
		int num = 2;
		for (;;)
		{
			int num2;
			int num3;
			spr\u2595 spr_u;
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5D;
				default:
					goto IL_C2;
				}
				break;
			case 1:
				if (num2 >= num3)
				{
					num = 0;
					continue;
				}
				goto IL_5D;
			case 3:
				return;
			case 4:
				return;
			case 5:
				goto IL_92;
			case 6:
				goto IL_92;
			case 7:
				if (spr_u == null)
				{
					num = 4;
					continue;
				}
				A_0 = spr_u.ᜀ(A_0);
				num2++;
				num = 6;
				continue;
			}
			if (!this.\u170D)
			{
				num = 3;
				continue;
			}
			num2 = this.ᜌ + 1;
			num3 = this.\u1712();
			num = 5;
			continue;
			IL_5D:
			spr_u = (this.ᜈ[num2] as spr\u2595);
			num = 7;
			continue;
			IL_92:
			num = 1;
		}
		return;
		IL_C2:
		if (false)
		{
		}
		if (true)
		{
		}
	}

	// Token: 0x06004A55 RID: 19029 RVA: 0x002D0098 File Offset: 0x002CF098
	private void ᜀ(ref double A_0, bool A_1)
	{
		for (;;)
		{
			if (true)
			{
			}
			this.ᜁ(ref A_0, A_1);
			int num = 0;
			int num2 = this.\u1712();
			int num3 = 0;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_BA;
				case 1:
				{
					if (num >= num2)
					{
						num3 = 3;
						continue;
					}
					sprἏ sprἏ = this.ᜂ(num);
					num3 = 4;
					continue;
				}
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						A_0 *= 100.0;
						num3 = 5;
						continue;
					}
					break;
				case 3:
					return;
				case 4:
				{
					sprἏ sprἏ;
					if (sprἏ.ᜀ() == TokenType.Percent)
					{
						num3 = 2;
						continue;
					}
					goto IL_47;
				}
				case 5:
					goto IL_47;
				case 6:
					goto IL_BA;
				}
				break;
				IL_47:
				num++;
				num3 = 6;
				continue;
				IL_BA:
				num3 = 1;
			}
		}
	}

	// Token: 0x06004A56 RID: 19030 RVA: 0x002D017C File Offset: 0x002CF17C
	private int ᜄ()
	{
		int result;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			result = 0;
			if (this.ᜊ > 0)
			{
				if (true)
				{
				}
				return this.ᜁ(this.ᜊ, this.ᜌ);
			}
			break;
		}
		return result;
	}

	// Token: 0x06004A57 RID: 19031 RVA: 0x002D01D8 File Offset: 0x002CF1D8
	private int ᜃ()
	{
		int a_;
		for (;;)
		{
			a_ = this.\u1712() - 1;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜋ > 0)
					{
						num = 4;
						continue;
					}
					goto IL_B8;
				case 1:
					if (true)
					{
					}
					a_ = this.ᜊ;
					num = 3;
					continue;
				case 2:
					if (this.ᜊ > 0)
					{
						num = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 3:
					goto IL_B6;
				case 4:
					a_ = this.ᜋ;
					num = 5;
					continue;
				case 5:
					goto IL_5D;
				}
				break;
			}
		}
		IL_5D:
		IL_B6:
		IL_B8:
		return this.ᜁ(0, a_);
	}

	// Token: 0x06004A58 RID: 19032 RVA: 0x002D02A8 File Offset: 0x002CF2A8
	private int ᜁ(int A_0, int A_1)
	{
		int a_ = 5;
		for (;;)
		{
			IL_4D:
			if (true)
			{
			}
			int num = this.\u1712();
			for (;;)
			{
				IL_5C:
				int num2 = 7;
				for (;;)
				{
					int num4;
					switch (num2)
					{
					case 0:
					{
						int num3;
						return num3;
					}
					case 1:
					{
						if (A_1 > num)
						{
							num2 = 6;
							continue;
						}
						int num3 = 0;
						num4 = A_0;
						num2 = 3;
						continue;
					}
					case 2:
					{
						int num3;
						num3++;
						num2 = 5;
						continue;
					}
					case 3:
						goto IL_130;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5C;
						default:
							if (false)
							{
							}
							if (A_0 > num)
							{
								num2 = 11;
								continue;
							}
							num2 = 14;
							continue;
						}
						break;
					case 5:
						goto IL_B6;
					case 6:
						goto IL_173;
					case 7:
						if (A_0 >= 0)
						{
							num2 = 9;
							continue;
						}
						goto IL_94;
					case 8:
						goto IL_130;
					case 9:
						num2 = 4;
						continue;
					case 10:
						if (this.ᜈ[num4] is spr\u20C6)
						{
							num2 = 2;
							continue;
						}
						goto IL_B6;
					case 11:
						goto IL_100;
					case 12:
						num2 = 1;
						continue;
					case 13:
						if (num4 > A_1)
						{
							num2 = 0;
							continue;
						}
						num2 = 10;
						continue;
					case 14:
						if (A_1 >= 0)
						{
							num2 = 12;
							continue;
						}
						goto IL_191;
					}
					goto IL_4D;
					IL_B6:
					num4++;
					num2 = 8;
					continue;
					IL_130:
					num2 = 13;
				}
			}
		}
		IL_94:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䠺䤼帾㍀㝂ౄ⥆ⵈ⹊㕌", a_), RecordTableEnumerator.b("䠺䤼帾㍀㝂ౄ⥆ⵈ⹊㕌潎㵐㙒♔⑖祘⽚㕜㹞འ䍢啤䝦ࡨժ६佮ᙰŲၴᙶ൸Ṻོ彾ꦈ搜ﾐ뮔", a_));
		IL_100:
		goto IL_94;
		IL_173:
		IL_191:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("帺匼嬾ࡀⵂ⅄≆ㅈ", a_), RecordTableEnumerator.b("帺匼嬾ࡀⵂ⅄≆ㅈ歊⅌⩎≐⁒畔⍖ㅘ㩚㍜罞兠䍢Ѥ०൨䭪੬ᵮᑰቲŴቶ୸孺ॼ᝾ꖄﺊﮎ뾐", a_));
	}

	// Token: 0x06004A59 RID: 19033 RVA: 0x002D046C File Offset: 0x002CF46C
	private void ᜂ()
	{
		int a_ = 12;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_DB;
				default:
					goto IL_104;
				}
				break;
			case 1:
				if (this.\u1712 >= 0)
				{
					num = 2;
					continue;
				}
				goto IL_119;
			case 2:
				goto IL_5F;
			case 3:
				if (this.ᜑ() != CellFormatType.Number)
				{
					goto IL_DB;
				}
				num = 1;
				continue;
			case 4:
				return;
			}
			if (!this.ᜑ)
			{
				num = 4;
				continue;
			}
			this.\u1712 = this.ᜀ(this.ᜐ, false);
			this.\u1713 = this.ᜀ(this.ᜐ, true);
			this.\u1714 = this.\u1713 - this.ᜀ(this.\u1713, false) + 1;
			num = 3;
			continue;
			IL_DB:
			if (true)
			{
			}
			num = 0;
		}
		return;
		IL_5F:
		spr\u20C6 spr_u20C = (spr\u20C6)this.ᜈ[this.\u1712];
		spr_u20C.ᜁ(true);
		return;
		IL_104:
		if (false)
		{
		}
		return;
		IL_119:
		throw new ArgumentException(RecordTableEnumerator.b("с㙃❅⭇㹉╋⅍㹏牑こ㽕㽗㍙⡛ⵝ䁟šգࡥ٧թᡫ乭ቯ᝱味ၵ᝷ཹቻ᩽깿", a_));
	}

	// Token: 0x06004A5A RID: 19034 RVA: 0x002D05A8 File Offset: 0x002CF5A8
	private int ᜀ(int A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			int num3;
			int num5;
			for (;;)
			{
				int num2;
				int num4;
				bool flag;
				switch (num)
				{
				case 0:
					goto IL_194;
				case 2:
					goto IL_201;
				case 3:
					goto IL_21E;
				case 4:
					num2 = -1;
					goto IL_1C4;
				case 5:
					if (num3 >= num4)
					{
						num = 2;
						continue;
					}
					num = 14;
					continue;
				case 6:
					return -1;
				case 7:
					if (num3 >= 0)
					{
						num = 12;
						continue;
					}
					goto IL_251;
				case 8:
					goto IL_194;
				case 9:
					goto IL_201;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_82;
					default:
						if (false)
						{
						}
						flag = true;
						num = 9;
						continue;
					}
					break;
				case 11:
					num = 4;
					continue;
				case 12:
					if (true)
					{
					}
					num = 16;
					continue;
				case 13:
					goto IL_15C;
				case 14:
					if (this.ᜈ[num3] is spr\u20C6)
					{
						num = 10;
						continue;
					}
					num3 += num5;
					num = 8;
					continue;
				case 15:
					num = 18;
					continue;
				case 16:
					if (num3 < num4)
					{
						num = 15;
						continue;
					}
					goto IL_251;
				case 17:
					num2 = 1;
					goto IL_1C4;
				case 18:
					if (!(this.ᜈ[num3] is spr\u20C6))
					{
						num = 13;
						continue;
					}
					num3 += num5;
					num = 20;
					continue;
				case 19:
					num = 5;
					continue;
				case 20:
					goto IL_21E;
				case 21:
					if (num3 >= 0)
					{
						num = 19;
						continue;
					}
					goto IL_201;
				case 22:
					if (!flag)
					{
						num = 6;
						continue;
					}
					num3 += num5;
					num = 3;
					continue;
				}
				goto IL_7C;
				IL_82:
				num = 11;
				continue;
				IL_7C:
				if (!A_1)
				{
					goto IL_82;
				}
				num = 17;
				continue;
				IL_194:
				num = 21;
				continue;
				IL_1C4:
				num5 = num2;
				num3 = A_0;
				num4 = this.\u1712();
				flag = false;
				num = 0;
				continue;
				IL_201:
				num = 22;
				continue;
				IL_21E:
				num = 7;
			}
			IL_15C:
			IL_251:
			return num3 - num5;
		}
		}
	}

	// Token: 0x06004A5B RID: 19035 RVA: 0x002D080C File Offset: 0x002CF80C
	private bool ᜀ(int A_0, int A_1)
	{
		for (;;)
		{
			int num = this.\u1712();
			A_0 = Math.Max(A_0, 0);
			A_1 = Math.Min(A_1, num - 1);
			int num2 = A_0;
			int num3 = 1;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					return true;
				case 1:
					goto IL_A5;
				case 2:
					goto IL_A5;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						if (this.ᜈ[num2] is spr\u20C6)
						{
							num3 = 0;
							continue;
						}
						num2++;
						num3 = 2;
						continue;
					}
					break;
				case 4:
					if (num2 >= A_1)
					{
						if (true)
						{
						}
						num3 = 5;
						continue;
					}
					num3 = 3;
					continue;
				case 5:
					return false;
				}
				break;
				IL_A5:
				num3 = 4;
			}
		}
		return true;
	}

	// Token: 0x06004A5C RID: 19036 RVA: 0x002D08E4 File Offset: 0x002CF8E4
	public bool ᜁ(double A_0)
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
			if (this.ᜌ())
			{
				return this.\u1717.ᜀ(A_0);
			}
			break;
		}
		return false;
	}

	// Token: 0x06004A5D RID: 19037 RVA: 0x002D0938 File Offset: 0x002CF938
	private void ᜁ()
	{
		switch (0)
		{
		default:
		{
			CellFormatType cellFormatType;
			for (;;)
			{
				for (;;)
				{
					this.\u1719 = CellFormatType.Unknown;
					int num = 0;
					int num2 = sprᨠ.ᜃ.Length;
					int num3 = 1;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_88;
						case 1:
							goto IL_E3;
						case 2:
						{
							TokenType[] a_;
							if (this.ᜀ(a_))
							{
								num3 = 0;
								continue;
							}
							goto IL_5E;
						}
						case 3:
							goto IL_E3;
						case 4:
							num3 = 6;
							continue;
						case 5:
						{
							if (true)
							{
							}
							if (num >= num2)
							{
								num3 = 8;
								continue;
							}
							TokenType[] a_ = (TokenType[])sprᨠ.ᜃ[num];
							cellFormatType = (CellFormatType)sprᨠ.ᜃ[num + 1];
							num3 = 9;
							continue;
						}
						case 6:
							if (!this.\u171B)
							{
								num3 = 7;
								continue;
							}
							goto IL_5E;
						case 7:
							goto IL_6D;
						case 8:
							return;
						case 9:
							if (cellFormatType == CellFormatType.Number)
							{
								num3 = 4;
								continue;
							}
							goto IL_6D;
						}
						break;
						IL_5E:
						num += 2;
						num3 = 3;
						continue;
						IL_6D:
						num3 = 2;
						continue;
						IL_E3:
						num3 = 5;
					}
				}
				IL_88:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_9E;
				}
			}
			IL_9E:
			if (false)
			{
			}
			this.\u1719 = cellFormatType;
			return;
		}
		}
	}

	// Token: 0x06004A5E RID: 19038 RVA: 0x002D0A88 File Offset: 0x002CFA88
	private bool ᜀ(TokenType[] A_0)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				int num2;
				int num3;
				int num4;
				switch (num)
				{
				case 0:
					return false;
				case 1:
					goto IL_65;
				case 2:
					if (num2 == 0)
					{
						num = 10;
						continue;
					}
					goto IL_69;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_15D;
					}
					break;
				case 4:
					goto IL_FF;
				case 5:
					goto IL_FF;
				case 6:
					return true;
				case 8:
				{
					sprἏ sprἏ;
					if (!sprᨠ.ᜀ(A_0, sprἏ.ᜀ()))
					{
						num = 0;
						continue;
					}
					num3++;
					num = 4;
					continue;
				}
				case 9:
				{
					if (num3 >= num4)
					{
						num = 6;
						continue;
					}
					sprἏ sprἏ = this.ᜂ(num3);
					num = 8;
					continue;
				}
				case 10:
					num = 11;
					continue;
				case 11:
					if (true)
					{
					}
					if (A_0.Length == 0)
					{
						num = 3;
						continue;
					}
					goto IL_69;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				num2 = this.\u1712();
				num = 2;
				continue;
				IL_69:
				num3 = 0;
				num4 = num2;
				num = 5;
				continue;
				IL_FF:
				num = 9;
			}
			IL_65:
			throw new ArgumentNullException(RecordTableEnumerator.b("伺刼吾⑀ⵂ㙄", a_));
			IL_15D:
			if (false)
			{
			}
			return true;
		}
		}
	}

	// Token: 0x06004A5F RID: 19039 RVA: 0x002D0BFC File Offset: 0x002CFBFC
	private void ᜀ(int A_0)
	{
		int a_ = 6;
		switch (0)
		{
		default:
			for (;;)
			{
				sprἏ sprἏ = this.ᜂ(A_0);
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					bool flag;
					bool flag2;
					switch (num)
					{
					case 0:
						if (sprἏ.ᜀ() != TokenType.Minute)
						{
							num = 3;
							continue;
						}
						num = 5;
						continue;
					case 1:
						if (!flag)
						{
							num = 6;
							continue;
						}
						return;
					case 2:
						num = 8;
						continue;
					case 3:
						goto IL_72;
					case 4:
						return;
					case 5:
						if (this.ᜀ(A_0 - 1, sprᨠ.ᜄ, false, new TokenType[]
						{
							TokenType.Hour,
							TokenType.Hour24
						}) == -1)
						{
							num = 2;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B0;
						default:
							if (false)
							{
							}
							num = 7;
							continue;
						}
						break;
					case 6:
					{
						sprᣌ sprᣌ = new sprᣌ();
						sprᣌ.ᜁ(sprἏ.ᜈ());
						this.ᜈ[A_0] = sprᣌ;
						num = 4;
						continue;
					}
					case 7:
						flag2 = true;
						goto IL_EF;
					case 8:
						goto IL_B0;
					}
					break;
					IL_EF:
					flag = flag2;
					num = 1;
					continue;
					IL_B0:
					flag2 = (this.ᜀ(A_0 + 1, sprᨠ.ᜅ, true, new TokenType[]
					{
						TokenType.Second,
						TokenType.SecondTotal
					}) != -1);
					goto IL_EF;
				}
			}
			IL_72:
			throw new ArgumentException(RecordTableEnumerator.b("焻圽㌿ㅁ⅃≅桇㹉⍋╍㕏㱑瑓≕⅗⩙㥛灝", a_));
		}
	}

	// Token: 0x06004A60 RID: 19040 RVA: 0x002D0D90 File Offset: 0x002CFD90
	private int ᜀ(int A_0, TokenType[] A_1, bool A_2, params TokenType[] A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = this.\u1712();
					break;
				}
				int num2 = 4;
				for (;;)
				{
					if (true)
					{
					}
					int num3;
					int num4;
					switch (num2)
					{
					case 0:
						goto IL_DB;
					case 1:
						num2 = 5;
						continue;
					case 2:
						num3 = 1;
						goto IL_161;
					case 3:
						return A_0;
					case 4:
						if (!A_2)
						{
							num2 = 1;
							continue;
						}
						num2 = 2;
						continue;
					case 5:
						num3 = -1;
						goto IL_161;
					case 6:
						num2 = 7;
						continue;
					case 7:
					{
						TokenType value;
						if (Array.IndexOf<TokenType>(A_3, value) != -1)
						{
							num2 = 3;
							continue;
						}
						A_0 += num4;
						num2 = 0;
						continue;
					}
					case 8:
					{
						if (A_0 >= num)
						{
							num2 = 9;
							continue;
						}
						sprἏ sprἏ = this.ᜂ(A_0);
						TokenType value = sprἏ.ᜀ();
						num2 = 13;
						continue;
					}
					case 9:
						return -1;
					case 10:
						if (A_0 >= 0)
						{
							num2 = 11;
							continue;
						}
						return -1;
					case 11:
						num2 = 8;
						continue;
					case 12:
						goto IL_DB;
					case 13:
					{
						TokenType value;
						if (Array.IndexOf<TokenType>(A_1, value) == -1)
						{
							num2 = 6;
							continue;
						}
						return -1;
					}
					}
					break;
					IL_DB:
					num2 = 10;
					continue;
					IL_161:
					num4 = num3;
					num2 = 12;
				}
			}
			return A_0;
		}
	}

	// Token: 0x06004A61 RID: 19041 RVA: 0x002D0F1C File Offset: 0x002CFF1C
	private void ᜀ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				bool flag = true;
				int num = this.\u1712();
				int num2 = 0;
				int num3 = 16;
				for (;;)
				{
					int num4;
					switch (num3)
					{
					case 0:
					{
						if (Array.IndexOf<TokenType>(sprᨠ.ᜆ, this.ᜂ(num2).ᜀ()) == -1)
						{
							num3 = 15;
							continue;
						}
						string text;
						text += this.ᜂ(num2).ᜈ();
						num2++;
						num3 = 19;
						continue;
					}
					case 1:
						goto IL_D5;
					case 2:
						goto IL_134;
					case 3:
						num3 = 8;
						continue;
					case 4:
						goto IL_195;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_257;
						default:
						{
							if (false)
							{
							}
							sprἏ sprἏ;
							if (sprἏ.ᜀ() == TokenType.DecimalPoint)
							{
								num3 = 22;
								continue;
							}
							goto IL_156;
						}
						}
						break;
					case 6:
						if (num2 < num)
						{
							num3 = 18;
							continue;
						}
						goto IL_1DA;
					case 7:
					{
						if (num2 >= num)
						{
							num3 = 3;
							continue;
						}
						sprἏ sprἏ = this.ᜂ(num2);
						num3 = 5;
						continue;
					}
					case 8:
						if (flag)
						{
							num3 = 11;
							continue;
						}
						num4 = 0;
						num3 = 2;
						continue;
					case 9:
					{
						sprᦚ sprᦚ = new sprᦚ();
						string text;
						sprᦚ.ᜁ(text);
						int num6;
						int num5 = num2 - num6;
						this.ᜈ.RemoveRange(num6, num5);
						this.ᜈ.Insert(num6, sprᦚ);
						num -= num5 - 1;
						flag = false;
						num3 = 13;
						continue;
					}
					case 10:
					{
						sprἏ sprἏ2;
						((spr\u173F)sprἏ2).ᜀ(false);
						num3 = 4;
						continue;
					}
					case 11:
						return;
					case 12:
						goto IL_134;
					case 13:
						goto IL_156;
					case 14:
					{
						if (num4 >= num)
						{
							num3 = 21;
							continue;
						}
						sprἏ sprἏ2 = this.ᜂ(num4);
						num3 = 20;
						continue;
					}
					case 15:
						goto IL_1DA;
					case 16:
						goto IL_B1;
					case 17:
					{
						int num6;
						if (num2 != num6 + 1)
						{
							num3 = 9;
							continue;
						}
						goto IL_156;
					}
					case 18:
						goto IL_257;
					case 19:
						goto IL_D5;
					case 20:
					{
						sprἏ sprἏ2;
						if (sprἏ2.ᜀ() == TokenType.Second)
						{
							num3 = 10;
							continue;
						}
						goto IL_195;
					}
					case 21:
						return;
					case 22:
					{
						int num6 = num2;
						string text = string.Empty;
						num2++;
						num3 = 1;
						continue;
					}
					case 23:
						goto IL_B1;
					}
					break;
					IL_B1:
					num3 = 7;
					continue;
					IL_D5:
					num3 = 6;
					continue;
					IL_134:
					num3 = 14;
					continue;
					IL_156:
					num2++;
					num3 = 23;
					continue;
					IL_195:
					num4++;
					num3 = 12;
					continue;
					IL_1DA:
					if (true)
					{
					}
					num3 = 17;
					continue;
					IL_257:
					num3 = 0;
				}
			}
			return;
		}
	}

	// Token: 0x06004A62 RID: 19042 RVA: 0x002D122C File Offset: 0x002D022C
	internal bool ᜋ()
	{
		int num = 0;
		for (;;)
		{
			bool result;
			List<sprἏ>.Enumerator enumerator;
			switch (num)
			{
			case 1:
				goto IL_3D;
				try
				{
					for (;;)
					{
						IL_3D:
						num = 6;
						for (;;)
						{
							switch (num)
							{
							case 0:
								result = false;
								num = 1;
								continue;
							case 1:
								goto IL_E1;
							case 2:
								if (enumerator.MoveNext())
								{
									sprἏ sprἏ = enumerator.Current;
									num = 3;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_3D;
								default:
									if (false)
									{
									}
									num = 4;
									continue;
								}
								break;
							case 3:
							{
								sprἏ sprἏ;
								if (Array.IndexOf<TokenType>(sprᨠ.ᜇ, sprἏ.ᜀ()) >= 0)
								{
									num = 0;
									continue;
								}
								break;
							}
							case 4:
								goto IL_E1;
							case 5:
								goto IL_EC;
							}
							IL_98:
							num = 2;
							continue;
							goto IL_98;
							IL_E1:
							num = 5;
						}
					}
					IL_EC:
					return result;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				goto IL_FC;
			case 2:
				return false;
			}
			if (true)
			{
			}
			if (this.ᜑ() != CellFormatType.DateTime)
			{
				num = 2;
				continue;
			}
			IL_FC:
			result = true;
			enumerator = this.ᜈ.GetEnumerator();
			num = 1;
		}
		return false;
	}

	// Token: 0x06004A63 RID: 19043 RVA: 0x002D1370 File Offset: 0x002D0370
	public sprἏ ᜂ(int A_0)
	{
		int a_ = 9;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 > this.\u1712() - 1)
				{
					num = 2;
					continue;
				}
				goto IL_A4;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_9C;
				}
				break;
			case 3:
				if (true)
				{
				}
				num = 0;
				continue;
			}
			if (A_0 < 0)
			{
				break;
			}
			num = 3;
		}
		IL_41:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嘾⽀❂⁄㽆", a_), RecordTableEnumerator.b("瘾⽀❂⁄㽆楈❊⡌㱎≐獒⅔㽖㡘㕚絜潞䅠ౢᝤ䝦๨ᥪ࡬๮հᙲݴ坶൸፺ᱼᅾꆀﾊꎌ", a_));
		IL_9C:
		if (false)
		{
		}
		goto IL_41;
		IL_A4:
		return this.ᜈ[A_0];
	}

	// Token: 0x06004A64 RID: 19044 RVA: 0x002D1430 File Offset: 0x002D0430
	public int \u1712()
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
		return this.ᜈ.Count;
	}

	// Token: 0x06004A65 RID: 19045 RVA: 0x002D1478 File Offset: 0x002D0478
	public bool ᜌ()
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
		return this.\u1717 != null;
	}

	// Token: 0x06004A66 RID: 19046 RVA: 0x002D14C0 File Offset: 0x002D04C0
	public CellFormatType ᜑ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6A;
			case 1:
				goto IL_5C;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5C;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			}
			if (true)
			{
			}
			if (this.\u1719 == CellFormatType.Unknown)
			{
				num = 1;
				continue;
			}
			break;
			IL_5C:
			this.ᜁ();
			num = 0;
		}
		IL_6A:
		return this.\u1719;
	}

	// Token: 0x06004A67 RID: 19047 RVA: 0x002D1540 File Offset: 0x002D0540
	public CultureInfo \u170D()
	{
		if (true)
		{
		}
		if (this.\u1718 == null)
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
				return CultureInfo.CurrentCulture;
			}
		}
		return this.\u1718.ᜂ();
	}

	// Token: 0x06004A68 RID: 19048 RVA: 0x002D1598 File Offset: 0x002D0598
	public bool ᜏ()
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
		return this.ᜑ;
	}

	// Token: 0x06004A69 RID: 19049 RVA: 0x002D15DC File Offset: 0x002D05DC
	public bool ᜎ()
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
		return this.ᜋ >= 0;
	}

	// Token: 0x06004A6A RID: 19050 RVA: 0x002D1624 File Offset: 0x002D0624
	public bool ᜉ()
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
		return this.\u171A;
	}

	// Token: 0x06004A6B RID: 19051 RVA: 0x002D1668 File Offset: 0x002D0668
	public int ᜐ()
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
		return this.ᜎ;
	}

	// Token: 0x06004A6C RID: 19052 RVA: 0x002D16AC File Offset: 0x002D06AC
	private static bool ᜀ(TokenType[] A_0, TokenType A_1)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 8;
			int num2;
			for (;;)
			{
				int num4;
				switch (num)
				{
				case 0:
					goto IL_F0;
				case 1:
				{
					TokenType tokenType;
					if (tokenType < A_1)
					{
						num = 5;
						continue;
					}
					goto IL_F0;
				}
				case 2:
				{
					int num3;
					if (num2 != num3)
					{
						num = 13;
						continue;
					}
					goto IL_B9;
				}
				case 3:
				{
					int num3;
					if (num4 != num3)
					{
						num = 14;
						continue;
					}
					goto IL_B9;
				}
				case 4:
					num = 2;
					continue;
				case 5:
					num = 3;
					continue;
				case 6:
					goto IL_79;
				case 7:
				{
					TokenType tokenType;
					if (tokenType >= A_1)
					{
						num = 4;
						continue;
					}
					num = 1;
					continue;
				}
				case 9:
					goto IL_F0;
				case 10:
					goto IL_B9;
				case 11:
					goto IL_F0;
				case 12:
					if (A_0[num4] != A_1)
					{
						num = 15;
						continue;
					}
					return true;
				case 13:
				{
					if (true)
					{
					}
					int num3;
					num2 = num3;
					num = 11;
					continue;
				}
				case 14:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_70;
					default:
					{
						if (false)
						{
						}
						int num3;
						num4 = num3;
						num = 9;
						continue;
					}
					}
					break;
				case 15:
					goto IL_DA;
				case 16:
				{
					if (num2 == num4)
					{
						num = 10;
						continue;
					}
					int num3 = (num2 + num4) / 2;
					TokenType tokenType = A_0[num3];
					num = 7;
					continue;
				}
				}
				goto IL_6D;
				IL_70:
				num = 6;
				continue;
				IL_6D:
				if (A_0 == null)
				{
					goto IL_70;
				}
				num4 = 0;
				num2 = A_0.Length - 1;
				num = 0;
				continue;
				IL_B9:
				num = 12;
				continue;
				IL_F0:
				num = 16;
			}
			IL_79:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴸吺嘼娾⽀あ", a_));
			IL_DA:
			return A_0[num2] == A_1;
		}
		}
	}

	// Token: 0x06004A6D RID: 19053 RVA: 0x002D1880 File Offset: 0x002D0880
	private static double ᜀ(double A_0, out double A_1)
	{
		switch (0)
		{
		default:
		{
			double num2;
			for (;;)
			{
				bool flag = A_0 > 0.0;
				int length = A_0.ToString().Length;
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!flag)
						{
							num = 4;
							continue;
						}
						return num2;
					case 1:
					{
						int num3;
						if (num3 < 15)
						{
							num = 5;
							continue;
						}
						goto IL_E3;
					}
					case 2:
						goto IL_CE;
					case 3:
					{
						int num3;
						if (num3 < length)
						{
							num = 6;
							continue;
						}
						goto IL_E3;
					}
					case 4:
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
							num = 2;
							continue;
						}
						break;
					case 5:
					{
						int num3;
						A_1 = Math.Round(A_1, num3);
						num = 8;
						continue;
					}
					case 6:
						num = 1;
						continue;
					case 7:
					{
						A_1 = Math.Abs(A_0 - (flag ? Math.Floor(A_0) : Math.Ceiling(A_0)));
						num2 = Math.Abs(A_0) - A_1;
						int num3 = length - num2.ToString().Length + 1;
						num = 3;
						continue;
					}
					case 8:
						goto IL_E3;
					}
					break;
					IL_E3:
					num = 0;
				}
			}
			IL_CE:
			return -num2;
		}
		}
	}

	// Token: 0x06004A6E RID: 19054 RVA: 0x002D19D4 File Offset: 0x002D09D4
	internal static double ᜀ(double A_0)
	{
		switch (0)
		{
		default:
		{
			double num3;
			for (;;)
			{
				for (;;)
				{
					bool flag = A_0 >= 0.0;
					int num = 6;
					for (;;)
					{
						double num2;
						int num4;
						double num5;
						double num6;
						switch (num)
						{
						case 0:
							num = 3;
							continue;
						case 1:
							if (num2 >= 0.49999999999995)
							{
								num = 2;
								continue;
							}
							return num3;
						case 2:
							num3 += (double)num4;
							if (true)
							{
							}
							num = 8;
							continue;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num5 = Math.Ceiling(A_0);
								goto IL_119;
							}
							break;
						case 4:
							num5 = Math.Floor(A_0);
							goto IL_119;
						case 5:
							num6 = num3 - A_0;
							goto IL_F2;
						case 6:
							if (!flag)
							{
								num = 0;
								continue;
							}
							num = 4;
							continue;
						case 7:
							if (!flag)
							{
								num = 9;
								continue;
							}
							num = 10;
							continue;
						case 8:
							return num3;
						case 9:
							num = 5;
							continue;
						case 10:
							num6 = A_0 - num3;
							goto IL_F2;
						}
						break;
						IL_F2:
						num2 = num6;
						num = 1;
						continue;
						IL_119:
						num3 = num5;
						num4 = Math.Sign(A_0);
						num = 7;
					}
				}
			}
			return num3;
		}
		}
	}

	// Token: 0x06004A6F RID: 19055 RVA: 0x002D1B28 File Offset: 0x002D0B28
	public object ᜀ(object A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_116:
			num = 6;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_5F;
			}
			break;
		}
		sprᨠ sprᨠ;
		int num2;
		int count;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
			{
				TokenType tokenType;
				if (tokenType != TokenType.Condition)
				{
					num = 5;
					continue;
				}
				sprἏ sprἏ;
				sprᨠ.\u1717 = (spr\u262F)sprἏ;
				if (true)
				{
				}
				num = 8;
				continue;
			}
			case 1:
				goto IL_17A;
			case 2:
				goto IL_106;
			case 3:
			{
				if (num2 >= count)
				{
					goto IL_116;
				}
				sprἏ sprἏ = this.ᜈ[num2];
				sprἏ = (sprἏ)sprἏ.ᜇ();
				sprᨠ.ᜈ.Add(sprἏ);
				TokenType tokenType = sprἏ.ᜀ();
				num = 0;
				continue;
			}
			case 4:
			{
				TokenType tokenType;
				if (tokenType != TokenType.Culture)
				{
					num = 7;
					continue;
				}
				sprἏ sprἏ;
				sprᨠ.\u1718 = (sprᲸ)sprἏ;
				num = 9;
				continue;
			}
			case 5:
				num = 4;
				continue;
			case 6:
				return sprᨠ;
			case 7:
				num = 1;
				continue;
			case 8:
				goto IL_17A;
			case 9:
				goto IL_17A;
			case 10:
				goto IL_106;
			}
			goto IL_5F;
			IL_106:
			num = 3;
			continue;
			IL_17A:
			num2++;
			num = 2;
		}
		return sprᨠ;
		IL_5F:
		sprᨠ = (sprᨠ)base.MemberwiseClone();
		sprᨠ.SetParent(A_0);
		sprᨠ.ᜈ = new List<sprἏ>(this.ᜈ.Count);
		num2 = 0;
		count = this.ᜈ.Count;
		num = 10;
		goto IL_2C;
	}

	// Token: 0x06004A70 RID: 19056 RVA: 0x002D1CE0 File Offset: 0x002D0CE0
	// Note: this type is marked as 'beforefieldinit'.
	static sprᨠ()
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
		sprᨠ.ᜃ = new object[]
		{
			new TokenType[]
			{
				TokenType.Unknown,
				TokenType.String,
				TokenType.ReservedPlace,
				TokenType.Character,
				TokenType.Color
			},
			CellFormatType.Unknown,
			new TokenType[]
			{
				TokenType.General,
				TokenType.Culture
			},
			CellFormatType.General,
			new TokenType[]
			{
				TokenType.Unknown,
				TokenType.String,
				TokenType.ReservedPlace,
				TokenType.Character,
				TokenType.Color,
				TokenType.Condition,
				TokenType.Text,
				TokenType.Asterix,
				TokenType.Culture
			},
			CellFormatType.Text,
			new TokenType[]
			{
				TokenType.Unknown,
				TokenType.String,
				TokenType.ReservedPlace,
				TokenType.Character,
				TokenType.Color,
				TokenType.Condition,
				TokenType.SignificantDigit,
				TokenType.InsignificantDigit,
				TokenType.PlaceReservedDigit,
				TokenType.Percent,
				TokenType.Scientific,
				TokenType.ThousandsSeparator,
				TokenType.DecimalPoint,
				TokenType.Asterix,
				TokenType.Fraction,
				TokenType.Culture
			},
			CellFormatType.Number,
			new TokenType[]
			{
				TokenType.Unknown,
				TokenType.Day,
				TokenType.String,
				TokenType.ReservedPlace,
				TokenType.Character,
				TokenType.Color,
				TokenType.Condition,
				TokenType.SignificantDigit,
				TokenType.InsignificantDigit,
				TokenType.PlaceReservedDigit,
				TokenType.Percent,
				TokenType.Scientific,
				TokenType.ThousandsSeparator,
				TokenType.DecimalPoint,
				TokenType.Asterix,
				TokenType.Fraction,
				TokenType.Culture
			},
			CellFormatType.Number,
			new TokenType[]
			{
				TokenType.Unknown,
				TokenType.Hour,
				TokenType.Hour24,
				TokenType.Minute,
				TokenType.MinuteTotal,
				TokenType.Second,
				TokenType.SecondTotal,
				TokenType.Year,
				TokenType.Month,
				TokenType.Day,
				TokenType.String,
				TokenType.ReservedPlace,
				TokenType.Character,
				TokenType.AmPm,
				TokenType.Color,
				TokenType.Condition,
				TokenType.SignificantDigit,
				TokenType.DecimalPoint,
				TokenType.Asterix,
				TokenType.Fraction,
				TokenType.Culture
			},
			CellFormatType.DateTime
		};
		sprᨠ.ᜄ = new TokenType[]
		{
			TokenType.Minute
		};
		sprᨠ.ᜅ = new TokenType[]
		{
			TokenType.Minute,
			TokenType.Hour,
			TokenType.Day,
			TokenType.Month,
			TokenType.Year
		};
		sprᨠ.ᜆ = new TokenType[]
		{
			TokenType.SignificantDigit
		};
		sprᨠ.ᜇ = new TokenType[]
		{
			TokenType.Day,
			TokenType.Month,
			TokenType.Year
		};
	}

	// Token: 0x040021A3 RID: 8611
	private const int ᜀ = -1;

	// Token: 0x040021A4 RID: 8612
	private const string ᜁ = ",";

	// Token: 0x040021A5 RID: 8613
	private const string ᜂ = "-";

	// Token: 0x040021A6 RID: 8614
	private static readonly object[] ᜃ;

	// Token: 0x040021A7 RID: 8615
	private static readonly TokenType[] ᜄ;

	// Token: 0x040021A8 RID: 8616
	private static readonly TokenType[] ᜅ;

	// Token: 0x040021A9 RID: 8617
	private static readonly TokenType[] ᜆ;

	// Token: 0x040021AA RID: 8618
	private static readonly TokenType[] ᜇ;

	// Token: 0x040021AB RID: 8619
	private List<sprἏ> ᜈ;

	// Token: 0x040021AC RID: 8620
	private bool ᜉ;

	// Token: 0x040021AD RID: 8621
	private int ᜊ;

	// Token: 0x040021AE RID: 8622
	private int ᜋ;

	// Token: 0x040021AF RID: 8623
	private int ᜌ;

	// Token: 0x040021B0 RID: 8624
	private bool \u170D;

	// Token: 0x040021B1 RID: 8625
	private int ᜎ;

	// Token: 0x040021B2 RID: 8626
	private int ᜏ;

	// Token: 0x040021B3 RID: 8627
	private int ᜐ;

	// Token: 0x040021B4 RID: 8628
	private bool ᜑ;

	// Token: 0x040021B5 RID: 8629
	private int \u1712;

	// Token: 0x040021B6 RID: 8630
	private int \u1713;

	// Token: 0x040021B7 RID: 8631
	private int \u1714;

	// Token: 0x040021B8 RID: 8632
	private int \u1715;

	// Token: 0x040021B9 RID: 8633
	private int \u1716;

	// Token: 0x040021BA RID: 8634
	private spr\u262F \u1717;

	// Token: 0x040021BB RID: 8635
	private sprᲸ \u1718;

	// Token: 0x040021BC RID: 8636
	private CellFormatType \u1719;

	// Token: 0x040021BD RID: 8637
	private bool \u171A;

	// Token: 0x040021BE RID: 8638
	private bool \u171B;

	// Token: 0x040021BF RID: 8639
	private bool \u171C;
}
