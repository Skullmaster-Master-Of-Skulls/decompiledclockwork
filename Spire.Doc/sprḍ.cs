using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Spire.CompoundFile.Doc;

// Token: 0x0200017E RID: 382
[DefaultMember("Item")]
[CLSCompliant(false)]
internal class sprḍ : spr\u23F8, ICollection
{
	// Token: 0x06000D50 RID: 3408 RVA: 0x000DDB14 File Offset: 0x000DCB14
	internal sprḍ()
	{
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x000DDB34 File Offset: 0x000DCB34
	internal sprḍ(byte[] A_0) : base(A_0)
	{
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x000DDB54 File Offset: 0x000DCB54
	internal sprḍ(byte[] A_0, int A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06000D53 RID: 3411 RVA: 0x000DDB74 File Offset: 0x000DCB74
	internal sprḍ(byte[] A_0, int A_1, int A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x000DDB98 File Offset: 0x000DCB98
	internal sprḍ(Stream A_0, int A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x000DDBB8 File Offset: 0x000DCBB8
	internal void ᜆ(int A_0)
	{
		for (;;)
		{
			spr\u1CC1 spr_u1CC = this.ᜇ(A_0);
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					goto IL_32;
				case 1:
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_32;
					default:
						if (false)
						{
						}
						if (spr_u1CC == null)
						{
							num = 1;
							continue;
						}
						this.ᜀ.Remove(spr_u1CC);
						spr_u1CC = this.ᜇ(A_0);
						num = 3;
						continue;
					}
					break;
				case 3:
					goto IL_32;
				}
				break;
				IL_32:
				num = 2;
			}
		}
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x000DDC54 File Offset: 0x000DCC54
	internal override void ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 0;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				spr\u1CC1 spr_u1CC;
				this.ᜀ.Add(spr_u1CC);
				num = 4;
				continue;
			}
			case 1:
				return;
			case 3:
				goto IL_C2;
			case 4:
				goto IL_125;
			case 5:
				if (A_1 < 0)
				{
					num = 3;
					continue;
				}
				num = 7;
				continue;
			case 6:
				goto IL_98;
			case 7:
				if (A_2 < 0)
				{
					num = 8;
					continue;
				}
				num = 10;
				continue;
			case 8:
				goto IL_1BA;
			case 9:
				goto IL_66;
			case 10:
			{
				if (A_1 + A_2 > A_0.Length)
				{
					num = 6;
					continue;
				}
				this.ᜄ();
				int num2 = A_1;
				num = 12;
				continue;
			}
			case 11:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					int num2;
					if (A_2 - (A_1 - num2) > 1)
					{
						spr\u1CC1 spr_u1CC = new spr\u1CC1();
						A_1 = spr_u1CC.ᜁ(A_0, A_1);
						num = 13;
						continue;
					}
					break;
				}
				}
				num = 1;
				continue;
			case 12:
				goto IL_125;
			case 13:
			{
				spr\u1CC1 spr_u1CC;
				if (this.ᜅ(spr_u1CC))
				{
					num = 0;
					continue;
				}
				goto IL_125;
			}
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 5;
			continue;
			IL_125:
			num = 11;
		}
		IL_66:
		throw new ArgumentNullException(ClipboardData.b("ݥᩧᡩ⡫཭ѯ፱", a_));
		IL_98:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("ཥ❧౩੫ᵭᕯٱ味嵵塷፹㽻ᅽ", a_));
		IL_C2:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ཥ❧౩੫ᵭᕯٱ味䩵塷䩹", a_));
		IL_1BA:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ཥ⭧թᥫmѯ剱䡳噵䡷婹", a_));
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x000DDE20 File Offset: 0x000DCE20
	private bool ᜅ(spr\u1CC1 A_0)
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!this.ᜂ(A_0))
				{
					num = 3;
					continue;
				}
				return true;
			case 1:
				if (this.ᜀ(A_0))
				{
					num = 4;
					continue;
				}
				return false;
			case 2:
				num = 6;
				continue;
			case 3:
				num = 8;
				continue;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_C7;
				default:
					goto IL_80;
				}
				break;
			case 6:
				if (!this.ᜃ(A_0))
				{
					goto IL_C7;
				}
				return true;
			case 7:
				num = 1;
				continue;
			case 8:
				if (!this.ᜁ(A_0))
				{
					num = 7;
					continue;
				}
				return true;
			case 9:
				if (true)
				{
				}
				num = 0;
				continue;
			}
			if (!this.ᜄ(A_0))
			{
				num = 2;
				continue;
			}
			return true;
			IL_C7:
			num = 9;
		}
		IL_80:
		if (false)
		{
		}
		return true;
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x000DDF30 File Offset: 0x000DCF30
	private bool ᜄ(spr\u1CC1 A_0)
	{
		for (;;)
		{
			ushort num = A_0.ᜂ();
			int num2 = 46;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num <= 51810)
					{
						num2 = 120;
						continue;
					}
					num2 = 114;
					continue;
				case 1:
					if (num != 2178)
					{
						num2 = 42;
						continue;
					}
					return true;
				case 2:
					goto IL_5F4;
				case 3:
					num2 = 87;
					continue;
				case 4:
					goto IL_3C8;
				case 5:
					if (num != 18439)
					{
						num2 = 129;
						continue;
					}
					return true;
				case 6:
					switch (num)
					{
					case 2165:
					case 2166:
						return true;
					default:
						num2 = 166;
						continue;
					}
					break;
				case 7:
					goto IL_56E;
				case 8:
					num2 = 156;
					continue;
				case 9:
					if (num <= 19041)
					{
						num2 = 142;
						continue;
					}
					num2 = 41;
					continue;
				case 10:
					return false;
				case 11:
					goto IL_1037;
				case 12:
					num2 = 93;
					continue;
				case 13:
					if (num != 18514)
					{
						num2 = 14;
						continue;
					}
					return true;
				case 14:
					num2 = 11;
					continue;
				case 15:
					switch (num)
					{
					case 2132:
					case 2133:
					case 2134:
					case 2136:
					case 2138:
					case 2140:
					case 2141:
						return true;
					case 2135:
					case 2137:
					case 2139:
						return false;
					default:
						num2 = 60;
						continue;
					}
					break;
				case 16:
					num2 = 115;
					continue;
				case 17:
					if (num != 2072)
					{
						num2 = 52;
						continue;
					}
					return true;
				case 18:
					if (A_0.\u1714() != 0)
					{
						num2 = 35;
						continue;
					}
					return true;
				case 19:
					if (num <= 10351)
					{
						num2 = 45;
						continue;
					}
					num2 = 137;
					continue;
				case 20:
					num2 = 56;
					continue;
				case 21:
					num2 = 107;
					continue;
				case 22:
					if (num != 2152)
					{
						num2 = 78;
						continue;
					}
					return true;
				case 23:
					num2 = 117;
					continue;
				case 24:
					num2 = 164;
					continue;
				case 25:
					goto IL_B62;
				case 26:
					if (num <= 10886)
					{
						num2 = 55;
						continue;
					}
					num2 = 175;
					continue;
				case 27:
					num2 = 2;
					continue;
				case 28:
					num2 = 15;
					continue;
				case 29:
					num2 = 123;
					continue;
				case 30:
					num2 = 75;
					continue;
				case 31:
					num2 = 144;
					continue;
				case 32:
					num2 = 50;
					continue;
				case 33:
					switch (num)
					{
					case 19023:
					case 19024:
					case 19025:
						return true;
					default:
						num2 = 83;
						continue;
					}
					break;
				case 34:
					num2 = 101;
					continue;
				case 35:
					num2 = 84;
					continue;
				case 36:
					num2 = 82;
					continue;
				case 37:
					num2 = 65;
					continue;
				case 38:
					num2 = 169;
					continue;
				case 39:
					A_0.ᜀ(0);
					num2 = 151;
					continue;
				case 40:
					goto IL_7BD;
				case 41:
					if (num <= 26647)
					{
						num2 = 154;
						continue;
					}
					num2 = 160;
					continue;
				case 42:
					num2 = 71;
					continue;
				case 43:
					if (num != 51845)
					{
						num2 = 98;
						continue;
					}
					return true;
				case 44:
					goto IL_E10;
				case 45:
					num2 = 57;
					continue;
				case 46:
					if (num <= 18514)
					{
						num2 = 150;
						continue;
					}
					num2 = 127;
					continue;
				case 47:
					if (num != 18510)
					{
						num2 = 167;
						continue;
					}
					return true;
				case 48:
					if (num != 26743)
					{
						num2 = 8;
						continue;
					}
					return true;
				case 49:
					num2 = 139;
					continue;
				case 50:
					goto IL_767;
				case 51:
					if (num != 10883)
					{
						num2 = 177;
						continue;
					}
					return true;
				case 52:
					num2 = 109;
					continue;
				case 53:
					if (num != 18501)
					{
						num2 = 30;
						continue;
					}
					return true;
				case 54:
					switch (num)
					{
					case 2048:
						num2 = 18;
						continue;
					case 2049:
					case 2050:
					case 2054:
						return true;
					case 2051:
					case 2052:
					case 2053:
						return false;
					default:
						num2 = 89;
						continue;
					}
					break;
				case 55:
					num2 = 146;
					continue;
				case 56:
					if (num != 10818)
					{
						num2 = 49;
						continue;
					}
					return true;
				case 57:
					if (num != 10329)
					{
						num2 = 58;
						continue;
					}
					return true;
				case 58:
					num2 = 136;
					continue;
				case 59:
					num2 = 5;
					continue;
				case 60:
					num2 = 162;
					continue;
				case 61:
					if (num != 26629)
					{
						num2 = 179;
						continue;
					}
					return true;
				case 62:
					num2 = 112;
					continue;
				case 63:
					if (num != 34880)
					{
						num2 = 21;
						continue;
					}
					return true;
				case 64:
					if (num != 10764)
					{
						num2 = 62;
						continue;
					}
					return true;
				case 65:
					if (num <= 27139)
					{
						num2 = 124;
						continue;
					}
					num2 = 67;
					continue;
				case 66:
					num2 = 40;
					continue;
				case 67:
					if (num <= 34880)
					{
						num2 = 116;
						continue;
					}
					num2 = 119;
					continue;
				case 68:
					if (num != 10896)
					{
						num2 = 104;
						continue;
					}
					return true;
				case 69:
					num2 = 102;
					continue;
				case 70:
					num2 = 140;
					continue;
				case 71:
					goto IL_7DD;
				case 72:
					num2 = 141;
					continue;
				case 73:
					num2 = 80;
					continue;
				case 74:
					if (num != 18436)
					{
						num2 = 59;
						continue;
					}
					return true;
				case 75:
					if (num != 18507)
					{
						num2 = 69;
						continue;
					}
					return true;
				case 76:
					if (num <= 18507)
					{
						num2 = 92;
						continue;
					}
					num2 = 47;
					continue;
				case 77:
					goto IL_33D;
				case 78:
					num2 = 6;
					continue;
				case 79:
					if (A_0.\u1714() != 128)
					{
						num2 = 81;
						continue;
					}
					return true;
				case 80:
					goto IL_EA7;
				case 81:
					num2 = 96;
					continue;
				case 82:
					if (num != 26736)
					{
						num2 = 135;
						continue;
					}
					return true;
				case 83:
					num2 = 161;
					continue;
				case 84:
					if (true)
					{
					}
					if (A_0.\u1714() != 1)
					{
						num2 = 163;
						continue;
					}
					return true;
				case 85:
					if (num != 18992)
					{
						num2 = 159;
						continue;
					}
					return true;
				case 86:
					if (num <= 2065)
					{
						num2 = 165;
						continue;
					}
					num2 = 17;
					continue;
				case 87:
					if (num != 10835)
					{
						num2 = 16;
						continue;
					}
					return true;
				case 88:
					num2 = 99;
					continue;
				case 89:
					num2 = 125;
					continue;
				case 90:
					num2 = 155;
					continue;
				case 91:
					num2 = 143;
					continue;
				case 92:
					num2 = 53;
					continue;
				case 93:
					if (num != 51799)
					{
						num2 = 23;
						continue;
					}
					return true;
				case 94:
					if (num <= 51761)
					{
						num2 = 37;
						continue;
					}
					num2 = 0;
					continue;
				case 95:
					switch (num)
					{
					case 18547:
					case 18548:
						return true;
					default:
						num2 = 70;
						continue;
					}
					break;
				case 96:
					if (A_0.\u1714() != 129)
					{
						num2 = 39;
						continue;
					}
					return true;
				case 97:
					num2 = 68;
					continue;
				case 98:
					num2 = 103;
					continue;
				case 99:
					switch (num)
					{
					case 18531:
					case 18534:
					case 18535:
						return true;
					case 18532:
					case 18533:
						return false;
					default:
						num2 = 178;
						continue;
					}
					break;
				case 100:
					num2 = 44;
					continue;
				case 101:
					goto IL_1097;
				case 102:
					goto IL_B52;
				case 103:
					if (num != 51849)
					{
						num2 = 66;
						continue;
					}
					return true;
				case 104:
					num2 = 74;
					continue;
				case 105:
					if (num <= 10764)
					{
						num2 = 91;
						continue;
					}
					num2 = 26;
					continue;
				case 106:
					num2 = 132;
					continue;
				case 107:
					goto IL_F2C;
				case 108:
					if (num != 10814)
					{
						num2 = 20;
						continue;
					}
					return true;
				case 109:
					switch (num)
					{
					case 2101:
					case 2102:
					case 2103:
					case 2104:
					case 2105:
					case 2106:
					case 2107:
					case 2108:
						return true;
					default:
						num2 = 28;
						continue;
					}
					break;
				case 110:
					num2 = 173;
					continue;
				case 111:
					if (num != 2065)
					{
						num2 = 171;
						continue;
					}
					return true;
				case 112:
					goto IL_5DA;
				case 113:
					num2 = 63;
					continue;
				case 114:
					if (num <= 51832)
					{
						num2 = 31;
						continue;
					}
					num2 = 43;
					continue;
				case 115:
					goto IL_5CA;
				case 116:
					num2 = 122;
					continue;
				case 117:
					if (num != 51810)
					{
						num2 = 32;
						continue;
					}
					return true;
				case 118:
					if (num <= 2178)
					{
						num2 = 170;
						continue;
					}
					num2 = 19;
					continue;
				case 119:
					if (num != 51226)
					{
						num2 = 152;
						continue;
					}
					return true;
				case 120:
					num2 = 158;
					continue;
				case 121:
					if (num != 27139)
					{
						num2 = 168;
						continue;
					}
					return true;
				case 122:
					if (num != 27145)
					{
						num2 = 113;
						continue;
					}
					return true;
				case 123:
					goto IL_70B;
				case 124:
					num2 = 48;
					continue;
				case 125:
					if (num != 2058)
					{
						num2 = 138;
						continue;
					}
					return true;
				case 126:
					switch (num)
					{
					case 18541:
					case 18542:
						return true;
					default:
						num2 = 34;
						continue;
					}
					break;
				case 127:
					if (num <= 26736)
					{
						num2 = 174;
						continue;
					}
					num2 = 94;
					continue;
				case 128:
					goto IL_4D3;
				case 129:
					num2 = 7;
					continue;
				case 130:
					num2 = 33;
					continue;
				case 131:
					num2 = 64;
					continue;
				case 132:
					if (num <= 18542)
					{
						num2 = 38;
						continue;
					}
					num2 = 95;
					continue;
				case 133:
					num2 = 128;
					continue;
				case 134:
					num2 = 121;
					continue;
				case 135:
					num2 = 4;
					continue;
				case 136:
					if (num != 10351)
					{
						num2 = 133;
						continue;
					}
					return true;
				case 137:
					if (num != 10361)
					{
						num2 = 131;
						continue;
					}
					return true;
				case 138:
					num2 = 111;
					continue;
				case 139:
					goto IL_7CD;
				case 140:
					if (num != 18568)
					{
						num2 = 145;
						continue;
					}
					return true;
				case 141:
					switch (num)
					{
					case 10803:
					case 10804:
						return true;
					default:
						num2 = 149;
						continue;
					}
					break;
				case 142:
					num2 = 172;
					continue;
				case 143:
					if (num <= 2141)
					{
						num2 = 180;
						continue;
					}
					num2 = 118;
					continue;
				case 144:
					switch (num)
					{
					case 51825:
					case 51826:
						return true;
					default:
						num2 = 110;
						continue;
					}
					break;
				case 145:
					num2 = 85;
					continue;
				case 146:
					if (num <= 10818)
					{
						num2 = 72;
						continue;
					}
					num2 = 157;
					continue;
				case 147:
					if (num != 10886)
					{
						num2 = 29;
						continue;
					}
					return true;
				case 148:
					if (num != 51761)
					{
						num2 = 27;
						continue;
					}
					return true;
				case 149:
					num2 = 108;
					continue;
				case 150:
					num2 = 105;
					continue;
				case 151:
					goto IL_477;
				case 152:
					num2 = 148;
					continue;
				case 153:
					if (num <= 18992)
					{
						num2 = 106;
						continue;
					}
					num2 = 9;
					continue;
				case 154:
					num2 = 61;
					continue;
				case 155:
					goto IL_538;
				case 156:
					if (num != 26759)
					{
						num2 = 134;
						continue;
					}
					return true;
				case 157:
					if (num <= 10835)
					{
						num2 = 24;
						continue;
					}
					num2 = 51;
					continue;
				case 158:
					if (num != 51783)
					{
						num2 = 12;
						continue;
					}
					return true;
				case 159:
					num2 = 77;
					continue;
				case 160:
					switch (num)
					{
					case 26724:
					case 26725:
						return true;
					default:
						num2 = 36;
						continue;
					}
					break;
				case 161:
					switch (num)
					{
					case 19038:
					case 19040:
					case 19041:
						return true;
					case 19039:
						return false;
					default:
						num2 = 73;
						continue;
					}
					break;
				case 162:
					goto IL_C5B;
				case 163:
					num2 = 79;
					continue;
				case 164:
					if (num != 10824)
					{
						num2 = 3;
						continue;
					}
					return true;
				case 165:
					num2 = 54;
					continue;
				case 166:
					num2 = 1;
					continue;
				case 167:
					num2 = 13;
					continue;
				case 168:
					num2 = 176;
					continue;
				case 169:
					if (num != 18527)
					{
						num2 = 88;
						continue;
					}
					return true;
				case 170:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B62;
					default:
						if (false)
						{
						}
						num2 = 22;
						continue;
					}
					break;
				case 171:
					num2 = 10;
					continue;
				case 172:
					if (num != 19011)
					{
						num2 = 130;
						continue;
					}
					return true;
				case 173:
					switch (num)
					{
					case 51830:
					case 51832:
						return true;
					case 51831:
						return false;
					default:
						num2 = 90;
						continue;
					}
					break;
				case 174:
					num2 = 153;
					continue;
				case 175:
					if (num <= 18439)
					{
						num2 = 97;
						continue;
					}
					num2 = 76;
					continue;
				case 176:
					goto IL_8CE;
				case 177:
					num2 = 147;
					continue;
				case 178:
					num2 = 126;
					continue;
				case 179:
					num2 = 25;
					continue;
				case 180:
					num2 = 86;
					continue;
				}
				break;
				IL_B62:
				switch (num)
				{
				case 26645:
				case 26646:
				case 26647:
					return true;
				default:
					num2 = 100;
					break;
				}
			}
		}
		IL_33D:
		IL_3C8:
		return false;
		IL_477:
		return true;
		IL_4D3:
		IL_538:
		IL_56E:
		IL_5CA:
		IL_5DA:
		IL_5F4:
		IL_70B:
		IL_767:
		IL_7BD:
		IL_7CD:
		IL_7DD:
		IL_8CE:
		IL_B52:
		IL_C5B:
		IL_E10:
		IL_EA7:
		IL_F2C:
		IL_1037:
		return false;
		IL_1097:
		return false;
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x000DF064 File Offset: 0x000DE064
	private new bool ᜃ(spr\u1CC1 A_0)
	{
		for (;;)
		{
			ushort num = A_0.ᜂ();
			int num2 = 44;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num != 50701)
					{
						num2 = 37;
						continue;
					}
					return true;
				case 1:
					num2 = 14;
					continue;
				case 2:
					if (num <= 9228)
					{
						num2 = 8;
						continue;
					}
					num2 = 95;
					continue;
				case 3:
					num2 = 89;
					continue;
				case 4:
					num2 = 0;
					continue;
				case 5:
					num2 = 64;
					continue;
				case 6:
					goto IL_BD8;
				case 7:
					if (num != 9251)
					{
						num2 = 93;
						continue;
					}
					return true;
				case 8:
					num2 = 121;
					continue;
				case 9:
					num2 = 98;
					continue;
				case 10:
					if (num > 26186)
					{
						num2 = 86;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_67F;
					default:
						if (false)
						{
						}
						num2 = 97;
						continue;
					}
					break;
				case 11:
					num2 = 39;
					continue;
				case 12:
					num2 = 34;
					continue;
				case 13:
					num2 = 74;
					continue;
				case 14:
					goto IL_3F0;
				case 15:
					num2 = 49;
					continue;
				case 16:
					num2 = 31;
					continue;
				case 17:
					if (num != 17920)
					{
						num2 = 84;
						continue;
					}
					return true;
				case 18:
					num2 = 58;
					continue;
				case 19:
					goto IL_5B4;
				case 20:
					num2 = 94;
					continue;
				case 21:
					if (num != 25618)
					{
						num2 = 1;
						continue;
					}
					return true;
				case 22:
					if (num != 9828)
					{
						num2 = 38;
						continue;
					}
					return true;
				case 23:
					num2 = 70;
					continue;
				case 24:
					if (num != 9755)
					{
						num2 = 80;
						continue;
					}
					return true;
				case 25:
					num2 = 120;
					continue;
				case 26:
					num2 = 59;
					continue;
				case 27:
					num2 = 96;
					continue;
				case 28:
					num2 = 124;
					continue;
				case 29:
					goto IL_426;
				case 30:
					switch (num)
					{
					case 17465:
					case 17466:
						return true;
					default:
						num2 = 109;
						continue;
					}
					break;
				case 31:
					if (num <= 42004)
					{
						num2 = 20;
						continue;
					}
					num2 = 113;
					continue;
				case 32:
					goto IL_5C4;
				case 33:
					num2 = 87;
					continue;
				case 34:
					if (num <= 9755)
					{
						num2 = 11;
						continue;
					}
					num2 = 104;
					continue;
				case 35:
					if (num != 9730)
					{
						num2 = 71;
						continue;
					}
					return true;
				case 36:
					switch (num)
					{
					case 9306:
					case 9307:
					case 9308:
					case 9313:
					case 9314:
						return true;
					case 9309:
					case 9310:
					case 9311:
					case 9312:
						return false;
					default:
						num2 = 115;
						continue;
					}
					break;
				case 37:
					num2 = 42;
					continue;
				case 38:
					num2 = 92;
					continue;
				case 39:
					if (num != 9738)
					{
						num2 = 41;
						continue;
					}
					return true;
				case 40:
					if (num <= 17497)
					{
						num2 = 83;
						continue;
					}
					num2 = 17;
					continue;
				case 41:
					num2 = 24;
					continue;
				case 42:
					if (num != 50709)
					{
						num2 = 50;
						continue;
					}
					return true;
				case 43:
					goto IL_6E6;
				case 44:
					if (num <= 17936)
					{
						num2 = 60;
						continue;
					}
					num2 = 55;
					continue;
				case 45:
					if (num <= 9272)
					{
						num2 = 51;
						continue;
					}
					num2 = 110;
					continue;
				case 46:
					num2 = 66;
					continue;
				case 47:
					num2 = 21;
					continue;
				case 48:
					num2 = 103;
					continue;
				case 49:
					if (num <= 25707)
					{
						num2 = 23;
						continue;
					}
					num2 = 10;
					continue;
				case 50:
					num2 = 29;
					continue;
				case 51:
					num2 = 2;
					continue;
				case 52:
					num2 = 62;
					continue;
				case 53:
					num2 = 22;
					continue;
				case 54:
					if (num != 26153)
					{
						num2 = 52;
						continue;
					}
					return true;
				case 55:
					if (num <= 33839)
					{
						num2 = 15;
						continue;
					}
					num2 = 85;
					continue;
				case 56:
					num2 = 73;
					continue;
				case 57:
					switch (num)
					{
					case 17493:
					case 17494:
					case 17495:
					case 17496:
					case 17497:
						return true;
					default:
						num2 = 3;
						continue;
					}
					break;
				case 58:
					goto IL_729;
				case 59:
					switch (num)
					{
					case 42003:
					case 42004:
						return true;
					default:
						num2 = 33;
						continue;
					}
					break;
				case 60:
					num2 = 112;
					continue;
				case 61:
					num2 = 88;
					continue;
				case 62:
					switch (num)
					{
					case 26182:
					case 26185:
					case 26186:
						return true;
					case 26183:
					case 26184:
						return false;
					default:
						num2 = 107;
						continue;
					}
					break;
				case 63:
					if (num <= 17453)
					{
						num2 = 12;
						continue;
					}
					num2 = 40;
					continue;
				case 64:
					switch (num)
					{
					case 9287:
					case 9288:
					case 9291:
					case 9292:
						return true;
					case 9289:
					case 9290:
						return false;
					default:
						num2 = 105;
						continue;
					}
					break;
				case 65:
					goto IL_76F;
				case 66:
					if (num != 25707)
					{
						num2 = 72;
						continue;
					}
					return true;
				case 67:
					if (num == 50799)
					{
						num2 = 108;
						continue;
					}
					return false;
				case 68:
					if (num != 50793)
					{
						num2 = 48;
						continue;
					}
					return true;
				case 69:
					goto IL_838;
				case 70:
					if (num <= 25618)
					{
						num2 = 13;
						continue;
					}
					num2 = 75;
					continue;
				case 71:
					goto IL_67F;
				case 72:
					num2 = 19;
					continue;
				case 73:
					if (num != 9228)
					{
						num2 = 102;
						continue;
					}
					return true;
				case 74:
					if (num != 18015)
					{
						num2 = 47;
						continue;
					}
					return true;
				case 75:
					switch (num)
					{
					case 25636:
					case 25637:
					case 25638:
					case 25639:
					case 25640:
						return true;
					default:
						num2 = 81;
						continue;
					}
					break;
				case 76:
					switch (num)
					{
					case 9258:
					case 9264:
					case 9265:
					case 9267:
					case 9268:
					case 9269:
					case 9270:
					case 9271:
					case 9272:
						return true;
					case 9259:
					case 9260:
					case 9261:
					case 9262:
					case 9263:
					case 9266:
						return false;
					default:
						num2 = 123;
						continue;
					}
					break;
				case 77:
					switch (num)
					{
					case 9325:
					case 9328:
					case 9329:
						return true;
					case 9326:
					case 9327:
						return false;
					default:
						num2 = 82;
						continue;
					}
					break;
				case 78:
					num2 = 116;
					continue;
				case 79:
					num2 = 67;
					continue;
				case 80:
					num2 = 32;
					continue;
				case 81:
					num2 = 119;
					continue;
				case 82:
					num2 = 35;
					continue;
				case 83:
					num2 = 30;
					continue;
				case 84:
					num2 = 101;
					continue;
				case 85:
					if (num <= 50709)
					{
						num2 = 16;
						continue;
					}
					num2 = 99;
					continue;
				case 86:
					switch (num)
					{
					case 33806:
					case 33807:
					case 33809:
						return true;
					case 33808:
						return false;
					default:
						num2 = 78;
						continue;
					}
					break;
				case 87:
					goto IL_2DF;
				case 88:
					switch (num)
					{
					case 50757:
					case 50765:
					case 50766:
					case 50767:
					case 50768:
					case 50769:
					case 50770:
					case 50771:
						return true;
					case 50758:
					case 50759:
					case 50760:
					case 50761:
					case 50762:
					case 50763:
					case 50764:
						return false;
					default:
						num2 = 28;
						continue;
					}
					break;
				case 89:
					goto IL_739;
				case 90:
					goto IL_68A;
				case 91:
					num2 = 43;
					continue;
				case 92:
					switch (num)
					{
					case 17451:
					case 17452:
					case 17453:
						return true;
					default:
						num2 = 27;
						continue;
					}
					break;
				case 93:
					num2 = 76;
					continue;
				case 94:
					switch (num)
					{
					case 33885:
					case 33886:
					case 33888:
						return true;
					case 33887:
						return false;
					default:
						num2 = 26;
						continue;
					}
					break;
				case 95:
					switch (num)
					{
					case 9238:
					case 9239:
						return true;
					default:
						num2 = 111;
						continue;
					}
					break;
				case 96:
					goto IL_BC8;
				case 97:
					num2 = 54;
					continue;
				case 98:
					switch (num)
					{
					case 33838:
					case 33839:
						return true;
					default:
						num2 = 18;
						continue;
					}
					break;
				case 99:
					if (num <= 50790)
					{
						num2 = 61;
						continue;
					}
					num2 = 68;
					continue;
				case 100:
					if (num != 17936)
					{
						num2 = 91;
						continue;
					}
					return true;
				case 101:
					if (num != 17931)
					{
						num2 = 118;
						continue;
					}
					return true;
				case 102:
					num2 = 69;
					continue;
				case 103:
					if (num != 50796)
					{
						num2 = 79;
						continue;
					}
					return true;
				case 104:
					if (num != 9792)
					{
						num2 = 53;
						continue;
					}
					return true;
				case 105:
					num2 = 106;
					continue;
				case 106:
					goto IL_4BD;
				case 107:
					num2 = 6;
					continue;
				case 108:
					goto IL_44C;
				case 109:
					num2 = 57;
					continue;
				case 110:
					if (num <= 9292)
					{
						num2 = 25;
						continue;
					}
					num2 = 36;
					continue;
				case 111:
					num2 = 7;
					continue;
				case 112:
					if (num <= 9730)
					{
						num2 = 114;
						continue;
					}
					num2 = 63;
					continue;
				case 113:
					if (num != 50689)
					{
						num2 = 4;
						continue;
					}
					return true;
				case 114:
					num2 = 45;
					continue;
				case 115:
					num2 = 77;
					continue;
				case 116:
					switch (num)
					{
					case 33816:
					case 33817:
					case 33818:
						return true;
					default:
						num2 = 9;
						continue;
					}
					break;
				case 117:
					goto IL_7A5;
				case 118:
					num2 = 100;
					continue;
				case 119:
					switch (num)
					{
					case 25701:
					case 25703:
						return true;
					case 25702:
						return false;
					default:
						num2 = 46;
						continue;
					}
					break;
				case 120:
					switch (num)
					{
					case 9281:
					case 9283:
						return true;
					case 9282:
						return false;
					default:
						if (true)
						{
						}
						num2 = 5;
						continue;
					}
					break;
				case 121:
					switch (num)
					{
					case 9219:
					case 9221:
					case 9222:
					case 9223:
						return true;
					case 9220:
						return false;
					default:
						num2 = 56;
						continue;
					}
					break;
				case 122:
					num2 = 117;
					continue;
				case 123:
					num2 = 65;
					continue;
				case 124:
					if (num != 50790)
					{
						num2 = 122;
						continue;
					}
					return true;
				}
				break;
				IL_67F:
				num2 = 90;
			}
		}
		IL_2DF:
		IL_3F0:
		IL_426:
		return false;
		IL_44C:
		return true;
		IL_4BD:
		IL_5B4:
		IL_5C4:
		IL_68A:
		IL_6E6:
		IL_729:
		IL_739:
		IL_76F:
		IL_7A5:
		IL_838:
		IL_BC8:
		IL_BD8:
		return false;
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x000DFD54 File Offset: 0x000DED54
	private bool ᜂ(spr\u1CC1 A_0)
	{
		for (;;)
		{
			IL_1E7:
			int num;
			ushort num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_5C7:
				num = 38;
				break;
			default:
				if (false)
				{
				}
				num2 = A_0.ᜂ();
				num = 63;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (num2)
					{
					case 22050:
					case 22052:
					case 22053:
						return true;
					case 22051:
						return false;
					default:
						num = 107;
						continue;
					}
					break;
				case 1:
					if (num2 != 13928)
					{
						num = 114;
						continue;
					}
					return true;
				case 2:
					goto IL_7E5;
				case 3:
					if (num2 != 54850)
					{
						num = 112;
						continue;
					}
					return true;
				case 4:
					num = 12;
					continue;
				case 5:
					num = 43;
					continue;
				case 6:
					if (num2 != 29817)
					{
						num = 60;
						continue;
					}
					return true;
				case 7:
					num = 64;
					continue;
				case 8:
					switch (num2)
					{
					case 54896:
					case 54897:
					case 54898:
						return true;
					default:
						num = 45;
						continue;
					}
					break;
				case 9:
					num = 8;
					continue;
				case 10:
					if (num2 <= 29706)
					{
						num = 93;
						continue;
					}
					num = 97;
					continue;
				case 11:
					if (num2 != 54796)
					{
						num = 48;
						continue;
					}
					return true;
				case 12:
					if (num2 <= 54850)
					{
						num = 20;
						continue;
					}
					num = 103;
					continue;
				case 13:
					switch (num2)
					{
					case 54802:
					case 54803:
					case 54806:
					case 54810:
					case 54811:
					case 54812:
					case 54813:
					case 54816:
						return true;
					case 54804:
					case 54805:
					case 54807:
					case 54808:
					case 54809:
					case 54814:
					case 54815:
						return false;
					default:
						num = 117;
						continue;
					}
					break;
				case 14:
					if (num2 != 22074)
					{
						num = 77;
						continue;
					}
					return true;
				case 15:
					if (num2 != 21504)
					{
						num = 24;
						continue;
					}
					return true;
				case 16:
					num = 70;
					continue;
				case 17:
					num = 3;
					continue;
				case 18:
					goto IL_344;
				case 19:
					if (num2 != 63030)
					{
						num = 66;
						continue;
					}
					return true;
				case 20:
					num = 39;
					continue;
				case 21:
					switch (num2)
					{
					case 13448:
					case 13449:
						return true;
					default:
						num = 46;
						continue;
					}
					break;
				case 22:
					goto IL_2BD;
				case 23:
					goto IL_2AD;
				case 24:
					num = 83;
					continue;
				case 25:
					num = 28;
					continue;
				case 26:
					num = 68;
					continue;
				case 27:
					num = 98;
					continue;
				case 28:
					goto IL_752;
				case 29:
					num = 11;
					continue;
				case 30:
					num = 34;
					continue;
				case 31:
					switch (num2)
					{
					case 37902:
					case 37903:
					case 37904:
					case 37905:
						return true;
					default:
						num = 36;
						continue;
					}
					break;
				case 32:
					switch (num2)
					{
					case 38401:
					case 38402:
						return true;
					default:
						num = 67;
						continue;
					}
					break;
				case 33:
					if (num2 <= 13449)
					{
						num = 52;
						continue;
					}
					num = 82;
					continue;
				case 34:
					goto IL_2CD;
				case 35:
					if (num2 <= 21504)
					{
						num = 37;
						continue;
					}
					num = 96;
					continue;
				case 36:
					num = 118;
					continue;
				case 37:
					num = 33;
					continue;
				case 38:
					if (num2 != 21642)
					{
						num = 59;
						continue;
					}
					return true;
				case 39:
					switch (num2)
					{
					case 54827:
					case 54828:
					case 54829:
					case 54830:
					case 54831:
					case 54834:
					case 54835:
					case 54836:
					case 54837:
					case 54841:
					case 54846:
						return true;
					case 54832:
					case 54833:
					case 54838:
					case 54839:
					case 54840:
					case 54842:
					case 54843:
					case 54844:
					case 54845:
						return false;
					default:
						num = 17;
						continue;
					}
					break;
				case 40:
					num = 106;
					continue;
				case 41:
					if (num2 <= 54919)
					{
						num = 9;
						continue;
					}
					num = 102;
					continue;
				case 42:
					num = 84;
					continue;
				case 43:
					goto IL_37A;
				case 44:
					num = 75;
					continue;
				case 45:
					num = 73;
					continue;
				case 46:
					num = 22;
					continue;
				case 47:
					num = 62;
					continue;
				case 48:
					num = 13;
					continue;
				case 49:
					switch (num2)
					{
					case 30241:
					case 30243:
						return true;
					case 30242:
						return false;
					default:
						num = 91;
						continue;
					}
					break;
				case 50:
					num = 99;
					continue;
				case 51:
					goto IL_A65;
				case 52:
					num = 65;
					continue;
				case 53:
					goto IL_5C5;
				case 54:
					num = 21;
					continue;
				case 55:
					goto IL_893;
				case 56:
					if (num2 <= 54816)
					{
						num = 105;
						continue;
					}
					num = 113;
					continue;
				case 57:
					if (num2 != 54399)
					{
						num = 30;
						continue;
					}
					return true;
				case 58:
					num = 6;
					continue;
				case 59:
					num = 69;
					continue;
				case 60:
					num = 49;
					continue;
				case 61:
					num = 111;
					continue;
				case 62:
					switch (num2)
					{
					case 13413:
					case 13414:
						return true;
					default:
						num = 110;
						continue;
					}
					break;
				case 63:
					if (num2 <= 30243)
					{
						num = 100;
						continue;
					}
					num = 56;
					continue;
				case 64:
					if (num2 != 29706)
					{
						num = 5;
						continue;
					}
					return true;
				case 65:
					if (num2 <= 13414)
					{
						num = 50;
						continue;
					}
					num = 116;
					continue;
				case 66:
					num = 81;
					continue;
				case 67:
					num = 57;
					continue;
				case 68:
					if (num2 != 54890)
					{
						num = 95;
						continue;
					}
					return true;
				case 69:
					if (num2 != 22027)
					{
						num = 44;
						continue;
					}
					return true;
				case 70:
					goto IL_A55;
				case 71:
					switch (num2)
					{
					case 54789:
					case 54792:
					case 54793:
						return true;
					case 54790:
					case 54791:
						return false;
					default:
						num = 29;
						continue;
					}
					break;
				case 72:
					if (num2 <= 37919)
					{
						num = 61;
						continue;
					}
					num = 86;
					continue;
				case 73:
					switch (num2)
					{
					case 54912:
					case 54913:
					case 54914:
					case 54915:
					case 54916:
					case 54917:
					case 54918:
					case 54919:
						return true;
					default:
						num = 25;
						continue;
					}
					break;
				case 74:
					goto IL_667;
				case 75:
					goto IL_518;
				case 76:
					if (num2 != 22116)
					{
						num = 7;
						continue;
					}
					return true;
				case 77:
					num = 74;
					continue;
				case 78:
					if (num2 != 54887)
					{
						num = 26;
						continue;
					}
					return true;
				case 79:
					goto IL_3B0;
				case 80:
					num = 89;
					continue;
				case 81:
					if (num2 == 63073)
					{
						num = 101;
						continue;
					}
					return false;
				case 82:
					if (num2 <= 13845)
					{
						num = 88;
						continue;
					}
					num = 90;
					continue;
				case 83:
					goto IL_334;
				case 84:
					if (num2 != 37895)
					{
						num = 40;
						continue;
					}
					return true;
				case 85:
					num = 32;
					continue;
				case 86:
					if (num2 <= 54399)
					{
						num = 85;
						continue;
					}
					num = 71;
					continue;
				case 87:
					num = 108;
					continue;
				case 88:
					num = 115;
					continue;
				case 89:
					if (num2 <= 22027)
					{
						num = 53;
						continue;
					}
					num = 0;
					continue;
				case 90:
					if (num2 != 13849)
					{
						num = 109;
						continue;
					}
					return true;
				case 91:
					num = 51;
					continue;
				case 92:
					num = 78;
					continue;
				case 93:
					num = 76;
					continue;
				case 94:
					num = 19;
					continue;
				case 95:
					num = 18;
					continue;
				case 96:
					if (num2 <= 22074)
					{
						num = 80;
						continue;
					}
					num = 10;
					continue;
				case 97:
					if (num2 != 29801)
					{
						num = 58;
						continue;
					}
					return true;
				case 98:
					if (num2 != 13845)
					{
						num = 104;
						continue;
					}
					return true;
				case 99:
					switch (num2)
					{
					case 13315:
					case 13316:
						return true;
					default:
						num = 47;
						continue;
					}
					break;
				case 100:
					num = 35;
					continue;
				case 101:
					goto IL_36A;
				case 102:
					switch (num2)
					{
					case 62996:
					case 62999:
					case 63000:
						return true;
					case 62997:
					case 62998:
						return false;
					default:
						num = 94;
						continue;
					}
					break;
				case 103:
					switch (num2)
					{
					case 54880:
					case 54882:
						return true;
					case 54881:
						return false;
					default:
						num = 92;
						continue;
					}
					break;
				case 104:
					num = 79;
					continue;
				case 105:
					num = 72;
					continue;
				case 106:
					goto IL_324;
				case 107:
					num = 14;
					continue;
				case 108:
					if (num2 != 30249)
					{
						num = 42;
						continue;
					}
					return true;
				case 109:
					if (true)
					{
					}
					num = 1;
					continue;
				case 110:
					num = 2;
					continue;
				case 111:
					if (num2 <= 37895)
					{
						num = 87;
						continue;
					}
					num = 31;
					continue;
				case 112:
					num = 23;
					continue;
				case 113:
					if (num2 <= 54890)
					{
						num = 4;
						continue;
					}
					num = 41;
					continue;
				case 114:
					num = 15;
					continue;
				case 115:
					if (num2 != 13837)
					{
						num = 27;
						continue;
					}
					return true;
				case 116:
					switch (num2)
					{
					case 13436:
					case 13437:
						return true;
					default:
						num = 54;
						continue;
					}
					break;
				case 117:
					num = 55;
					continue;
				case 118:
					switch (num2)
					{
					case 37918:
					case 37919:
						return true;
					default:
						num = 16;
						continue;
					}
					break;
				}
				goto IL_1E7;
			}
			IL_5C5:
			goto IL_5C7;
		}
		IL_2AD:
		IL_2BD:
		IL_2CD:
		IL_324:
		IL_334:
		IL_344:
		return false;
		IL_36A:
		return true;
		IL_37A:
		IL_3B0:
		IL_518:
		IL_667:
		IL_752:
		IL_7E5:
		IL_893:
		IL_A55:
		IL_A65:
		return false;
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x000E0970 File Offset: 0x000DF970
	private bool ᜁ(spr\u1CC1 A_0)
	{
		for (;;)
		{
			ushort num = A_0.ᜂ();
			int num2 = 53;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch (num)
					{
					case 45087:
					case 45088:
					case 45089:
					case 45090:
					case 45093:
						return true;
					case 45091:
					case 45092:
						return false;
					default:
						num2 = 14;
						continue;
					}
					break;
				case 1:
					if (num != 20501)
					{
						num2 = 51;
						continue;
					}
					return true;
				case 2:
					return false;
				case 3:
					num2 = 36;
					continue;
				case 4:
					num2 = 12;
					continue;
				case 5:
					num2 = 22;
					continue;
				case 6:
					num2 = 69;
					continue;
				case 7:
					return false;
				case 8:
					goto IL_211;
				case 9:
					num2 = 52;
					continue;
				case 10:
					switch (num)
					{
					case 20530:
					case 20531:
						return true;
					default:
						num2 = 35;
						continue;
					}
					break;
				case 11:
					num2 = 65;
					continue;
				case 12:
					switch (num)
					{
					case 36899:
					case 36900:
						return true;
					default:
						num2 = 11;
						continue;
					}
					break;
				case 13:
					num2 = 7;
					continue;
				case 14:
					num2 = 58;
					continue;
				case 15:
					if (num != 28740)
					{
						num2 = 44;
						continue;
					}
					return true;
				case 16:
					switch (num)
					{
					case 45079:
					case 45080:
						return true;
					default:
						num2 = 13;
						continue;
					}
					break;
				case 17:
					switch (num)
					{
					case 12347:
					case 12348:
					case 12350:
						return true;
					case 12349:
						return false;
					default:
						num2 = 23;
						continue;
					}
					break;
				case 18:
					num2 = 43;
					continue;
				case 19:
					num2 = 26;
					continue;
				case 20:
					num2 = 46;
					continue;
				case 21:
					switch (num)
					{
					case 20487:
					case 20488:
					case 20491:
						return true;
					case 20489:
					case 20490:
						return false;
					default:
						num2 = 28;
						continue;
					}
					break;
				case 22:
					switch (num)
					{
					case 12288:
					case 12289:
					case 12293:
					case 12294:
					case 12297:
					case 12298:
						return true;
					case 12290:
					case 12291:
					case 12292:
					case 12295:
					case 12296:
						return false;
					default:
						num2 = 61;
						continue;
					}
					break;
				case 23:
					num2 = 29;
					continue;
				case 24:
					num2 = 0;
					continue;
				case 25:
					goto IL_201;
				case 26:
					if (num <= 12317)
					{
						num2 = 5;
						continue;
					}
					num2 = 17;
					continue;
				case 27:
					if (num <= 36886)
					{
						num2 = 60;
						continue;
					}
					num2 = 55;
					continue;
				case 28:
					num2 = 1;
					continue;
				case 29:
					switch (num)
					{
					case 12840:
					case 12842:
						return true;
					case 12841:
						return false;
					default:
						num2 = 73;
						continue;
					}
					break;
				case 30:
					if (num != 21039)
					{
						num2 = 20;
						continue;
					}
					return true;
				case 31:
					return false;
				case 32:
					num2 = 34;
					continue;
				case 33:
					switch (num)
					{
					case 20507:
					case 20508:
						return true;
					default:
						num2 = 6;
						continue;
					}
					break;
				case 34:
					switch (num)
					{
					case 61955:
					case 61956:
						return true;
					default:
						num2 = 45;
						continue;
					}
					break;
				case 35:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2B6;
					default:
						if (false)
						{
						}
						num2 = 39;
						continue;
					}
					break;
				case 36:
					return false;
				case 37:
					num2 = 50;
					continue;
				case 38:
					if (num != 53827)
					{
						num2 = 32;
						continue;
					}
					return true;
				case 39:
					switch (num)
					{
					case 20543:
					case 20544:
					case 20545:
					case 20546:
						return true;
					default:
						if (true)
						{
						}
						num2 = 3;
						continue;
					}
					break;
				case 40:
					if (num <= 28730)
					{
						num2 = 68;
						continue;
					}
					num2 = 15;
					continue;
				case 41:
					if (num != 36876)
					{
						num2 = 57;
						continue;
					}
					return true;
				case 42:
					if (num != 20518)
					{
						num2 = 62;
						continue;
					}
					return true;
				case 43:
					switch (num)
					{
					case 12313:
					case 12314:
					case 12317:
						return true;
					case 12315:
					case 12316:
						return false;
					default:
						num2 = 56;
						continue;
					}
					break;
				case 44:
					num2 = 41;
					continue;
				case 45:
					num2 = 31;
					continue;
				case 46:
					switch (num)
					{
					case 28715:
					case 28716:
					case 28717:
					case 28718:
					case 28720:
						return true;
					case 28719:
						return false;
					default:
						num2 = 9;
						continue;
					}
					break;
				case 47:
					num2 = 2;
					continue;
				case 48:
					return false;
				case 49:
					if (num <= 12857)
					{
						num2 = 19;
						continue;
					}
					num2 = 54;
					continue;
				case 50:
					return false;
				case 51:
					num2 = 33;
					continue;
				case 52:
					if (num != 28730)
					{
						num2 = 47;
						continue;
					}
					return true;
				case 53:
					if (num <= 20546)
					{
						num2 = 59;
						continue;
					}
					num2 = 27;
					continue;
				case 54:
					if (num <= 20508)
					{
						num2 = 67;
						continue;
					}
					num2 = 42;
					continue;
				case 55:
					if (num <= 45080)
					{
						num2 = 4;
						continue;
					}
					num2 = 71;
					continue;
				case 56:
					num2 = 25;
					continue;
				case 57:
					num2 = 63;
					continue;
				case 58:
					goto IL_2B6;
				case 59:
					num2 = 49;
					continue;
				case 60:
					num2 = 40;
					continue;
				case 61:
					num2 = 66;
					continue;
				case 62:
					num2 = 10;
					continue;
				case 63:
					if (num != 36886)
					{
						num2 = 70;
						continue;
					}
					return true;
				case 64:
					num2 = 48;
					continue;
				case 65:
					if (num != 36913)
					{
						num2 = 72;
						continue;
					}
					return true;
				case 66:
					switch (num)
					{
					case 12302:
					case 12305:
					case 12306:
					case 12307:
						return true;
					case 12303:
					case 12304:
						return false;
					default:
						num2 = 18;
						continue;
					}
					break;
				case 67:
					num2 = 21;
					continue;
				case 68:
					num2 = 30;
					continue;
				case 69:
					return false;
				case 70:
					num2 = 8;
					continue;
				case 71:
					if (num <= 53815)
					{
						num2 = 24;
						continue;
					}
					num2 = 38;
					continue;
				case 72:
					num2 = 16;
					continue;
				case 73:
					num2 = 74;
					continue;
				case 74:
					if (num != 12857)
					{
						num2 = 64;
						continue;
					}
					return true;
				}
				break;
				IL_2B6:
				switch (num)
				{
				case 53812:
				case 53813:
				case 53814:
				case 53815:
					return true;
				default:
					num2 = 37;
					break;
				}
			}
		}
		IL_201:
		IL_211:
		return false;
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x000E1160 File Offset: 0x000E0160
	private bool ᜀ(spr\u1CC1 A_0)
	{
		for (;;)
		{
			ushort num = A_0.ᜂ();
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return false;
				case 1:
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
						num2 = 2;
						continue;
					}
					break;
				case 2:
					switch (num)
					{
					case 52744:
					case 52745:
					case 52746:
					case 52747:
						return true;
					default:
						num2 = 4;
						continue;
					}
					break;
				case 3:
					switch (num)
					{
					case 27650:
					case 27651:
					case 27652:
					case 27653:
						return true;
					default:
						num2 = 1;
						continue;
					}
					break;
				case 4:
					num2 = 0;
					continue;
				}
				break;
			}
		}
		return true;
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x000E1230 File Offset: 0x000E0230
	internal override int ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 14;
		switch (0)
		{
		default:
			for (;;)
			{
				int num = this.ᜇ();
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						int num3;
						int num4;
						if (num3 >= num4)
						{
							num2 = 6;
							continue;
						}
						spr\u1CC1 spr_u1CC = this.ᜀ[num3];
						int num5 = spr_u1CC.ᜀ(A_0, A_1);
						int num6;
						num6 += num5;
						A_1 += num5;
						num3++;
						num2 = 2;
						continue;
					}
					case 1:
					{
						if (A_1 + num > A_0.Length)
						{
							num2 = 7;
							continue;
						}
						int num6 = 0;
						int num3 = 0;
						int num4 = this.ᜈ();
						if (true)
						{
						}
						num2 = 8;
						continue;
					}
					case 2:
						goto IL_146;
					case 3:
						return 0;
					case 4:
						if (A_0 == null)
						{
							num2 = 9;
							continue;
						}
						num2 = 10;
						continue;
					case 5:
						if (num == 0)
						{
							num2 = 3;
							continue;
						}
						num2 = 4;
						continue;
					case 6:
					{
						int num6;
						return num6;
					}
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_146;
						default:
							goto IL_D5;
						}
						break;
					case 8:
						goto IL_146;
					case 9:
						goto IL_F8;
					case 10:
						if (A_1 >= 0)
						{
							num2 = 11;
							continue;
						}
						goto IL_132;
					case 11:
						num2 = 1;
						continue;
					}
					break;
					IL_146:
					num2 = 0;
				}
			}
			return 0;
			IL_D5:
			if (false)
			{
			}
			goto IL_132;
			IL_F8:
			throw new ArgumentNullException(ClipboardData.b("ᕳѵ੷㹹ᵻ੽", a_));
			IL_132:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ᵳ㥵ṷᱹཻ᭽", a_));
		}
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x000E13D8 File Offset: 0x000E03D8
	internal int ᜀ(BinaryWriter A_0, Stream A_1, int A_2)
	{
		int a_ = 12;
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 2;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						goto IL_CA;
					case 1:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 10;
							continue;
						}
						spr\u1CC1 spr_u1CC = this.ᜀ[num2];
						goto IL_8F;
					}
					case 2:
						if (A_2 == 0)
						{
							num = 5;
							continue;
						}
						num = 6;
						continue;
					case 3:
					{
						spr\u1CC1 spr_u1CC;
						if (spr_u1CC.ᜎ() != null)
						{
							num = 7;
							continue;
						}
						goto IL_F7;
					}
					case 4:
						goto IL_CC;
					case 5:
						return 0;
					case 6:
					{
						if (A_0 == null)
						{
							num = 0;
							continue;
						}
						int num4 = 0;
						num2 = 0;
						int num3 = this.ᜈ();
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8F;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					}
					case 7:
					{
						spr\u1CC1 spr_u1CC;
						int num5 = spr_u1CC.ᜀ(A_0, A_1);
						int num4;
						num4 += num5;
						num = 9;
						continue;
					}
					case 8:
						goto IL_CC;
					case 9:
						goto IL_F7;
					case 10:
					{
						int num4;
						return num4;
					}
					}
					break;
					IL_8F:
					num = 3;
					continue;
					IL_CC:
					num = 1;
					continue;
					IL_F7:
					num2++;
					num = 4;
				}
			}
			return 0;
			IL_CA:
			throw new ArgumentNullException(ClipboardData.b("űsѵᵷ᭹ᅻ", a_));
		}
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x000E1544 File Offset: 0x000E0544
	internal int ᜀ(BinaryWriter A_0, Stream A_1)
	{
		int a_ = 8;
		long position;
		for (;;)
		{
			position = A_1.Position;
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (this.ᜂ().Count == 0)
					{
						num = 4;
						continue;
					}
					num = 2;
					continue;
				case 1:
					goto IL_D0;
				case 2:
				{
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					int num2 = 0;
					int num3 = this.ᜈ();
					num = 6;
					continue;
				}
				case 3:
					goto IL_B6;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B6;
					default:
						goto IL_73;
					}
					break;
				case 5:
					goto IL_EB;
				case 6:
					goto IL_B6;
				case 7:
				{
					int num2;
					int num3;
					if (num2 >= num3)
					{
						num = 1;
						continue;
					}
					this.ᜀ[num2].ᜀ(A_0, A_1);
					num2++;
					num = 3;
					continue;
				}
				}
				break;
				IL_B6:
				num = 7;
			}
		}
		IL_73:
		if (false)
		{
		}
		return 0;
		IL_D0:
		return (int)(A_1.Position - position);
		IL_EB:
		throw new ArgumentNullException(ClipboardData.b("ᵭѯqᅳ᝵ᕷ", a_));
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x000E1668 File Offset: 0x000E0668
	internal void ᜄ()
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
		this.ᜀ.Clear();
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x000E16B0 File Offset: 0x000E06B0
	internal void ᜆ(spr\u1CC1 A_0)
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
		this.ᜀ.Add(A_0);
	}

	// Token: 0x06000D62 RID: 3426 RVA: 0x000E16F8 File Offset: 0x000E06F8
	internal void ᜀ(spr\u1CC1 A_0, int A_1)
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
		this.ᜀ.Insert(A_1, A_0);
	}

	// Token: 0x06000D63 RID: 3427 RVA: 0x000E1740 File Offset: 0x000E0740
	internal void ᜀ(sprḍ A_0, int A_1)
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
		this.ᜀ.InsertRange(A_1, A_0.ᜂ());
	}

	// Token: 0x06000D64 RID: 3428 RVA: 0x000E1790 File Offset: 0x000E0790
	internal bool ᜀ(int A_0, bool A_1)
	{
		spr\u1CC1 spr_u1CC;
		for (;;)
		{
			if (true)
			{
			}
			spr_u1CC = this.ᜇ(A_0);
			if (spr_u1CC == null)
			{
				return A_1;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_35;
			}
		}
		IL_35:
		if (false)
		{
		}
		return spr_u1CC.ᜉ();
	}

	// Token: 0x06000D65 RID: 3429 RVA: 0x000E17E0 File Offset: 0x000E07E0
	internal byte ᜀ(int A_0, byte A_1)
	{
		spr\u1CC1 spr_u1CC;
		for (;;)
		{
			spr_u1CC = this.ᜃ(A_0);
			if (spr_u1CC != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				break;
			}
			return A_1;
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return spr_u1CC.\u1714();
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x000E1830 File Offset: 0x000E0830
	internal byte ᜀ(int A_0, byte A_1, ref bool A_2)
	{
		spr\u1CC1 spr_u1CC;
		for (;;)
		{
			spr_u1CC = this.ᜃ(A_0);
			if (spr_u1CC == null)
			{
				goto IL_45;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_35;
			}
		}
		IL_35:
		if (false)
		{
		}
		A_2 = false;
		return spr_u1CC.\u1714();
		IL_45:
		A_2 = true;
		return A_1;
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x000E1888 File Offset: 0x000E0888
	internal bool ᜄ(int A_0)
	{
		spr\u1CC1 spr_u1CC = this.ᜇ(A_0);
		if (spr_u1CC != null)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_23;
				}
			}
			IL_23:
			if (false)
			{
			}
			return true;
		}
		if (true)
		{
		}
		return false;
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x000E18D4 File Offset: 0x000E08D4
	internal ushort ᜁ(int A_0, ushort A_1)
	{
		spr\u1CC1 spr_u1CC = this.ᜃ(A_0);
		if (spr_u1CC != null)
		{
			for (;;)
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
					goto IL_2B;
				}
			}
			IL_2B:
			if (false)
			{
			}
			return spr_u1CC.\u1716();
		}
		return A_1;
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x000E1924 File Offset: 0x000E0924
	internal short ᜁ(int A_0, short A_1)
	{
		spr\u1CC1 spr_u1CC = this.ᜃ(A_0);
		if (spr_u1CC != null)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_23;
				}
			}
			IL_23:
			if (true)
			{
			}
			if (false)
			{
			}
			return spr_u1CC.ᜐ();
		}
		return A_1;
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x000E1974 File Offset: 0x000E0974
	internal int ᜀ(int A_0, int A_1)
	{
		spr\u1CC1 spr_u1CC = this.ᜃ(A_0);
		if (spr_u1CC != null)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_23;
				}
			}
			IL_23:
			if (false)
			{
			}
			if (true)
			{
			}
			return spr_u1CC.\u1712();
		}
		return A_1;
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x000E19C4 File Offset: 0x000E09C4
	internal uint ᜀ(int A_0, uint A_1)
	{
		if (true)
		{
		}
		spr\u1CC1 spr_u1CC = this.ᜃ(A_0);
		if (spr_u1CC != null)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_2B;
				}
			}
			IL_2B:
			if (false)
			{
			}
			return spr_u1CC.ᜋ();
		}
		return A_1;
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x000E1A14 File Offset: 0x000E0A14
	internal byte[] ᜅ(int A_0)
	{
		spr\u1CC1 spr_u1CC = this.ᜃ(A_0);
		if (spr_u1CC != null)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_23;
				}
			}
			IL_23:
			if (false)
			{
			}
			if (true)
			{
			}
			return spr_u1CC.ᜅ();
		}
		return null;
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x000E1A64 File Offset: 0x000E0A64
	internal void ᜁ(int A_0, bool A_1)
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
		this.ᜀ(A_0).ᜁ(A_1);
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x000E1AAC File Offset: 0x000E0AAC
	internal void ᜁ(int A_0, byte A_1)
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
		this.ᜀ(A_0).ᜀ(A_1);
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x000E1AF4 File Offset: 0x000E0AF4
	internal void ᜀ(int A_0, ushort A_1)
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
		this.ᜀ(A_0).ᜁ(A_1);
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x000E1B3C File Offset: 0x000E0B3C
	internal void ᜀ(int A_0, short A_1)
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
		this.ᜀ(A_0).ᜀ(A_1);
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x000E1B84 File Offset: 0x000E0B84
	internal void ᜁ(int A_0, int A_1)
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
		this.ᜀ(A_0).ᜁ(A_1);
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x000E1BCC File Offset: 0x000E0BCC
	internal void ᜁ(int A_0, uint A_1)
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
		this.ᜀ(A_0).ᜀ(A_1);
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x000E1C14 File Offset: 0x000E0C14
	internal void ᜀ(int A_0, byte[] A_1)
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
		this.ᜀ(A_0).ᜁ(A_1);
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x000E1C5C File Offset: 0x000E0C5C
	internal spr\u1739 ᜁ()
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
		return new spr\u1739(this);
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x000E1CA0 File Offset: 0x000E0CA0
	internal void ᜀ(spr\u1739 A_0)
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
		A_0.ᜉ(this);
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x000E1CE4 File Offset: 0x000E0CE4
	internal sprḍ ᜀ()
	{
		switch (0)
		{
		default:
		{
			sprḍ sprḍ;
			for (;;)
			{
				sprḍ = new sprḍ();
				int num = 0;
				int count = this.ᜀ.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_C8;
					case 1:
						goto IL_54;
					case 2:
						return sprḍ;
					case 3:
					{
						spr\u1CC1 spr_u1CC;
						sprḍ.ᜆ(spr_u1CC);
						goto IL_6B;
					}
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6B;
						default:
						{
							if (false)
							{
							}
							spr\u1CC1 spr_u1CC;
							if (spr_u1CC != null)
							{
								num2 = 3;
								continue;
							}
							goto IL_54;
						}
						}
						break;
					case 5:
					{
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						if (true)
						{
						}
						spr\u1CC1 spr_u1CC2 = this.ᜁ(num);
						spr\u1CC1 spr_u1CC = spr_u1CC2.ᜊ();
						num2 = 4;
						continue;
					}
					case 6:
						goto IL_C8;
					}
					break;
					IL_54:
					num++;
					num2 = 6;
					continue;
					IL_6B:
					num2 = 1;
					continue;
					IL_C8:
					num2 = 5;
				}
			}
			return sprḍ;
		}
		}
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x000E1DE4 File Offset: 0x000E0DE4
	internal List<spr\u1CC1> ᜂ()
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

	// Token: 0x06000D78 RID: 3448 RVA: 0x000E1E28 File Offset: 0x000E0E28
	internal spr\u1CC1 ᜇ(int A_0)
	{
		spr\u1CC1 spr_u1CC;
		for (;;)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					int num = 0;
					int count = this.ᜀ.Count;
					int num2 = 5;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (spr_u1CC.ᜈ() == A_0)
							{
								num2 = 4;
								continue;
							}
							num++;
							num2 = 2;
							continue;
						case 1:
							goto IL_B9;
						case 2:
							goto IL_9F;
						case 3:
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							spr_u1CC = this.ᜁ(num);
							num2 = 0;
							continue;
						case 4:
							return spr_u1CC;
						case 5:
							goto IL_9F;
						}
						break;
						IL_9F:
						num2 = 3;
					}
					break;
				}
				}
			}
		}
		return spr_u1CC;
		IL_B9:
		return null;
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x000E1EF4 File Offset: 0x000E0EF4
	public int ᜈ()
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
		return this.ᜀ.Count;
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x000E1F3C File Offset: 0x000E0F3C
	internal override int ᜇ()
	{
		int num;
		for (;;)
		{
			IL_00:
			if (true)
			{
			}
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_00;
				default:
				{
					if (false)
					{
					}
					num = 0;
					int num2 = 0;
					int num3 = this.ᜈ();
					int num4 = 3;
					for (;;)
					{
						switch (num4)
						{
						case 0:
							goto IL_5B;
						case 1:
							if (num2 >= num3)
							{
								num4 = 2;
								continue;
							}
							num += this.ᜁ(num2).ᜇ();
							num2++;
							num4 = 0;
							continue;
						case 2:
							return num;
						case 3:
							goto IL_5B;
						}
						break;
						IL_5B:
						num4 = 1;
					}
					break;
				}
				}
			}
		}
		return num;
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x000E1FD8 File Offset: 0x000E0FD8
	public bool ᜆ()
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
		return false;
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x000E2014 File Offset: 0x000E1014
	public void ᜀ(Array A_0, int A_1)
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
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x000E2050 File Offset: 0x000E1050
	public object ᜅ()
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
		return null;
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x000E208C File Offset: 0x000E108C
	public IEnumerator GetEnumerator()
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
		return new sprḍ.ᜀ(this);
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x000E20D0 File Offset: 0x000E10D0
	private spr\u1CC1 ᜀ(int A_0)
	{
		spr\u1CC1 spr_u1CC;
		for (;;)
		{
			for (;;)
			{
				spr_u1CC = this.ᜇ(A_0);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (spr_u1CC == null)
						{
							num = 1;
							continue;
						}
						return spr_u1CC;
					case 1:
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
							spr_u1CC = new spr\u1CC1(A_0);
							this.ᜆ(spr_u1CC);
							num = 2;
							continue;
						}
						break;
					case 2:
						return spr_u1CC;
					}
					break;
				}
			}
		}
		return spr_u1CC;
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x000E2158 File Offset: 0x000E1158
	internal spr\u1CC1 ᜁ(int A_0)
	{
		int a_ = 4;
		for (;;)
		{
			IL_09:
			int num = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_09;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						num = 2;
						continue;
					case 2:
						if (A_0 >= this.ᜀ.Count)
						{
							num = 3;
							continue;
						}
						goto IL_AA;
					case 3:
						goto IL_A8;
					}
					if (A_0 < 0)
					{
						goto IL_65;
					}
					num = 0;
					break;
				}
			}
		}
		IL_65:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ͩɫ੭ᕯੱ", a_), ClipboardData.b("㱩൫ɭկ᝱味ᕵ᥷ᑹ屻ၽꒃꪉ뒓ﮙ뺝邟芡얣좥첧誩쮫\udcad햯펱삳펵쪷骹좻횽ꆿ곁諅귇꓉ꯋ뫍룏", a_));
		IL_A8:
		goto IL_65;
		IL_AA:
		return this.ᜀ[A_0];
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x000E221C File Offset: 0x000E121C
	internal bool ᜂ(int A_0)
	{
		for (;;)
		{
			bool result;
			using (List<spr\u1CC1>.Enumerator enumerator = this.ᜀ.GetEnumerator())
			{
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (!enumerator.MoveNext())
						{
							num = 3;
							continue;
						}
						spr\u1CC1 spr_u1CC = enumerator.Current;
						num = 6;
						continue;
					}
					case 1:
						result = true;
						num = 4;
						continue;
					case 2:
						goto IL_B4;
					case 3:
						num = 2;
						continue;
					case 4:
						goto IL_AA;
					case 6:
					{
						spr\u1CC1 spr_u1CC;
						if ((int)spr_u1CC.ᜂ() == A_0)
						{
							num = 1;
							continue;
						}
						break;
					}
					}
					IL_62:
					num = 0;
					continue;
					goto IL_62;
				}
				IL_AA:
				return result;
				IL_B4:
				goto IL_0E;
			}
			return result;
			IL_0E:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_24;
			}
		}
		IL_24:
		if (true)
		{
		}
		if (false)
		{
		}
		return false;
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x000E230C File Offset: 0x000E130C
	internal new spr\u1CC1 ᜃ(int A_0)
	{
		switch (0)
		{
		default:
		{
			spr\u1CC1 result;
			int num2;
			int num3;
			int num4;
			int num5;
			for (;;)
			{
				result = this.ᜇ(A_0);
				int num = 26;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_30C;
					case 1:
						num = 30;
						continue;
					case 2:
						goto IL_26C;
					case 3:
						goto IL_131;
					case 4:
						goto IL_271;
					case 5:
						if ((int)this.ᜀ[num2].ᜂ() == A_0)
						{
							num = 25;
							continue;
						}
						num2--;
						num = 29;
						continue;
					case 6:
						goto IL_28E;
					case 7:
						goto IL_1A8;
					case 8:
						goto IL_449;
					case 9:
						if (num3 < 0)
						{
							num = 6;
							continue;
						}
						num = 18;
						continue;
					case 10:
						if ((int)this.ᜀ[num4].ᜂ() == A_0)
						{
							num = 8;
							continue;
						}
						num4--;
						num = 11;
						continue;
					case 11:
						goto IL_391;
					case 12:
						goto IL_465;
					case 13:
						goto IL_1A8;
					case 14:
						num = 37;
						continue;
					case 15:
						goto IL_391;
					case 16:
						num = 17;
						continue;
					case 17:
						if (this.ᜄ(51849))
						{
							num = 0;
							continue;
						}
						goto IL_1DA;
					case 18:
						if (true)
						{
						}
						if ((int)this.ᜀ[num3].ᜂ() == A_0)
						{
							num = 2;
							continue;
						}
						num3--;
						num = 4;
						continue;
					case 19:
						if ((int)this.ᜀ[num5].ᜂ() == A_0)
						{
							num = 24;
							continue;
						}
						num5--;
						num = 7;
						continue;
					case 20:
						goto IL_207;
					case 21:
						if (num4 < 0)
						{
							num = 35;
							continue;
						}
						num = 10;
						continue;
					case 22:
						goto IL_484;
					case 23:
						goto IL_271;
					case 24:
						goto IL_3E1;
					case 25:
						goto IL_415;
					case 26:
						if (!this.ᜄ(50751))
						{
							num = 1;
							continue;
						}
						goto IL_465;
					case 27:
						if (!this.ᜄ(29708))
						{
							num = 14;
							continue;
						}
						goto IL_17B;
					case 28:
						if (!this.ᜄ(21039))
						{
							num = 38;
							continue;
						}
						goto IL_484;
					case 29:
						goto IL_131;
					case 30:
						if (this.ᜄ(50799))
						{
							num = 12;
							continue;
						}
						goto IL_207;
					case 31:
						if (this.ᜄ(53799))
						{
							num = 22;
							continue;
						}
						goto IL_355;
					case 32:
						if (!this.ᜄ(51799))
						{
							num = 16;
							continue;
						}
						goto IL_30C;
					case 33:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1BB;
						}
						if (false)
						{
						}
						if (num2 < 0)
						{
							num = 20;
							continue;
						}
						num = 5;
						continue;
					case 34:
						goto IL_17B;
					case 35:
						goto IL_1DA;
					case 36:
						if (num5 < 0)
						{
							goto IL_1BB;
						}
						num = 19;
						continue;
					case 37:
						if (this.ᜄ(54887))
						{
							num = 34;
							continue;
						}
						return result;
					case 38:
						num = 31;
						continue;
					case 39:
						goto IL_355;
					}
					break;
					IL_131:
					num = 33;
					continue;
					IL_17B:
					num3 = this.ᜀ.Count - 1;
					num = 23;
					continue;
					IL_1A8:
					num = 36;
					continue;
					IL_1BB:
					num = 39;
					continue;
					IL_1DA:
					num = 28;
					continue;
					IL_207:
					num = 32;
					continue;
					IL_271:
					num = 9;
					continue;
					IL_30C:
					num4 = this.ᜀ.Count - 1;
					num = 15;
					continue;
					IL_355:
					num = 27;
					continue;
					IL_391:
					num = 21;
					continue;
					IL_465:
					num2 = this.ᜀ.Count - 1;
					num = 3;
					continue;
					IL_484:
					num5 = this.ᜀ.Count - 1;
					num = 13;
				}
			}
			IL_26C:
			return this.ᜀ[num3];
			IL_28E:
			return result;
			IL_3E1:
			return this.ᜀ[num5];
			IL_415:
			return this.ᜀ[num2];
			IL_449:
			return this.ᜀ[num4];
		}
		}
	}

	// Token: 0x04001543 RID: 5443
	private new List<spr\u1CC1> ᜀ = new List<spr\u1CC1>();

	// Token: 0x0200017F RID: 383
	private new class ᜀ : IEnumerator
	{
		// Token: 0x06000D83 RID: 3459 RVA: 0x000E27CC File Offset: 0x000E17CC
		internal ᜀ(sprḍ A_0)
		{
			int a_ = 15;
			this.ᜀ = -1;
			base..ctor();
			if (A_0 == null)
			{
				throw new ArgumentNullException(ClipboardData.b("մᙶ୸Ṻ፼୾", a_));
			}
			this.ᜁ = A_0;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x000E2810 File Offset: 0x000E1810
		public void ᜂ()
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
			this.ᜀ = -1;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x000E2854 File Offset: 0x000E1854
		public object ᜁ()
		{
			for (;;)
			{
				IL_00:
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_86;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 3:
						if (this.ᜀ >= this.ᜁ.ᜈ())
						{
							num = 1;
							continue;
						}
						goto IL_88;
					}
					if (this.ᜀ < 0)
					{
						goto IL_45;
					}
					if (true)
					{
					}
					num = 2;
				}
			}
			IL_45:
			return null;
			IL_86:
			goto IL_45;
			IL_88:
			return this.ᜁ.ᜁ(this.ᜀ);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x000E28FC File Offset: 0x000E18FC
		public bool ᜀ()
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
				for (;;)
				{
					this.ᜀ++;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 2;
							continue;
						case 1:
							if (this.ᜀ >= 0)
							{
								num = 0;
								continue;
							}
							return false;
						case 2:
							if (true)
							{
							}
							if (this.ᜀ >= this.ᜁ.ᜈ())
							{
								num = 3;
								continue;
							}
							return true;
						case 3:
							goto IL_94;
						}
						break;
					}
				}
				return true;
			}
			return false;
			IL_94:
			return false;
		}

		// Token: 0x04001544 RID: 5444
		private int ᜀ;

		// Token: 0x04001545 RID: 5445
		private sprḍ ᜁ;
	}
}
