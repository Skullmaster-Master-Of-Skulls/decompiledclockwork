using System;

namespace Telerik.Web.UI.Dictionaries
{
	// Token: 0x020011CC RID: 4556
	internal class DoubleMetaphone
	{
		// Token: 0x0600BC24 RID: 48164 RVA: 0x0029B198 File Offset: 0x00299398
		internal DoubleMetaphone()
		{
		}

		// Token: 0x0600BC25 RID: 48165 RVA: 0x0029B1A7 File Offset: 0x002993A7
		internal string Encode(string value)
		{
			return this.Encode(value, false);
		}

		// Token: 0x0600BC26 RID: 48166 RVA: 0x0029B1B4 File Offset: 0x002993B4
		internal string Encode(string value, bool alternate)
		{
			value = DoubleMetaphone.CleanInput(value);
			if (value == null)
			{
				return null;
			}
			bool slavoGermanic = DoubleMetaphone.IsSlavoGermanic(value);
			int num = DoubleMetaphone.IsSilentStart(value) ? 1 : 0;
			DoubleMetaphoneResult doubleMetaphoneResult = new DoubleMetaphoneResult(this.GetMaxCodeLen(), this);
			while (!doubleMetaphoneResult.IsComplete() && num <= value.Length - 1)
			{
				char c = value[num];
				switch (c)
				{
				case 'A':
				case 'E':
				case 'I':
				case 'O':
				case 'U':
				case 'Y':
					num = DoubleMetaphone.HandleAEIOUY(doubleMetaphoneResult, num);
					break;
				case 'B':
					doubleMetaphoneResult.Append('P');
					num = ((DoubleMetaphone.CharAt(value, num + 1) == 'B') ? (num + 2) : (num + 1));
					break;
				case 'C':
					num = DoubleMetaphone.HandleC(value, doubleMetaphoneResult, num);
					break;
				case 'D':
					num = DoubleMetaphone.HandleD(value, doubleMetaphoneResult, num);
					break;
				case 'F':
					doubleMetaphoneResult.Append('F');
					num = ((DoubleMetaphone.CharAt(value, num + 1) == 'F') ? (num + 2) : (num + 1));
					break;
				case 'G':
					num = DoubleMetaphone.HandleG(value, doubleMetaphoneResult, num, slavoGermanic);
					break;
				case 'H':
					num = DoubleMetaphone.HandleH(value, doubleMetaphoneResult, num);
					break;
				case 'J':
					num = DoubleMetaphone.HandleJ(value, doubleMetaphoneResult, num, slavoGermanic);
					break;
				case 'K':
					doubleMetaphoneResult.Append('K');
					num = ((DoubleMetaphone.CharAt(value, num + 1) == 'K') ? (num + 2) : (num + 1));
					break;
				case 'L':
					num = DoubleMetaphone.HandleL(value, doubleMetaphoneResult, num);
					break;
				case 'M':
					doubleMetaphoneResult.Append('M');
					num = (DoubleMetaphone.ConditionM0(value, num) ? (num + 2) : (num + 1));
					break;
				case 'N':
					doubleMetaphoneResult.Append('N');
					num = ((DoubleMetaphone.CharAt(value, num + 1) == 'N') ? (num + 2) : (num + 1));
					break;
				case 'P':
					num = DoubleMetaphone.HandleP(value, doubleMetaphoneResult, num);
					break;
				case 'Q':
					doubleMetaphoneResult.Append('K');
					num = ((DoubleMetaphone.CharAt(value, num + 1) == 'Q') ? (num + 2) : (num + 1));
					break;
				case 'R':
					num = DoubleMetaphone.HandleR(value, doubleMetaphoneResult, num, slavoGermanic);
					break;
				case 'S':
					num = DoubleMetaphone.HandleS(value, doubleMetaphoneResult, num, slavoGermanic);
					break;
				case 'T':
					num = DoubleMetaphone.HandleT(value, doubleMetaphoneResult, num);
					break;
				case 'V':
					doubleMetaphoneResult.Append('F');
					num = ((DoubleMetaphone.CharAt(value, num + 1) == 'V') ? (num + 2) : (num + 1));
					break;
				case 'W':
					num = DoubleMetaphone.HandleW(value, doubleMetaphoneResult, num);
					break;
				case 'X':
					num = DoubleMetaphone.HandleX(value, doubleMetaphoneResult, num);
					break;
				case 'Z':
					num = DoubleMetaphone.HandleZ(value, doubleMetaphoneResult, num, slavoGermanic);
					break;
				default:
					if (c != 'Ç')
					{
						if (c != 'Ñ')
						{
							num++;
						}
						else
						{
							doubleMetaphoneResult.Append('N');
							num++;
						}
					}
					else
					{
						doubleMetaphoneResult.Append('S');
						num++;
					}
					break;
				}
			}
			if (!alternate)
			{
				return doubleMetaphoneResult.GetPrimary();
			}
			return doubleMetaphoneResult.GetAlternate();
		}

		// Token: 0x0600BC27 RID: 48167 RVA: 0x0029B472 File Offset: 0x00299672
		internal bool IsDoubleMetaphoneEqual(string value1, string value2)
		{
			return this.IsDoubleMetaphoneEqual(value1, value2, false);
		}

		// Token: 0x0600BC28 RID: 48168 RVA: 0x0029B47D File Offset: 0x0029967D
		internal bool IsDoubleMetaphoneEqual(string value1, string value2, bool alternate)
		{
			return this.Encode(value1, alternate) == this.Encode(value2, alternate);
		}

		// Token: 0x0600BC29 RID: 48169 RVA: 0x0029B494 File Offset: 0x00299694
		internal int GetMaxCodeLen()
		{
			return this.maxCodeLen;
		}

		// Token: 0x0600BC2A RID: 48170 RVA: 0x0029B49C File Offset: 0x0029969C
		internal void SetMaxCodeLen(int maxCodeLen)
		{
			this.maxCodeLen = maxCodeLen;
		}

		// Token: 0x0600BC2B RID: 48171 RVA: 0x0029B4A5 File Offset: 0x002996A5
		private static int HandleAEIOUY(DoubleMetaphoneResult result, int index)
		{
			if (index == 0)
			{
				result.Append('A');
			}
			return checked(index + 1);
		}

		// Token: 0x0600BC2C RID: 48172 RVA: 0x0029B4B8 File Offset: 0x002996B8
		private static int HandleC(string value, DoubleMetaphoneResult result, int index)
		{
			checked
			{
				if (DoubleMetaphone.ConditionC0(value, index))
				{
					result.Append('K');
					index += 2;
				}
				else if (index == 0 && DoubleMetaphone.Contains(value, index, 6, "CAESAR"))
				{
					result.Append('S');
					index += 2;
				}
				else if (DoubleMetaphone.Contains(value, index, 2, "CH"))
				{
					index = DoubleMetaphone.HandleCH(value, result, index);
				}
				else if (DoubleMetaphone.Contains(value, index, 2, "CZ") && !DoubleMetaphone.Contains(value, index - 2, 4, "WICZ"))
				{
					result.Append('S', 'X');
					index += 2;
				}
				else if (DoubleMetaphone.Contains(value, index + 1, 3, "CIA"))
				{
					result.Append('X');
					index += 3;
				}
				else
				{
					if (DoubleMetaphone.Contains(value, index, 2, "CC") && (index != 1 || DoubleMetaphone.CharAt(value, 0) != 'M'))
					{
						return DoubleMetaphone.HandleCC(value, result, index);
					}
					if (DoubleMetaphone.Contains(value, index, 2, "CK", "CG", "CQ"))
					{
						result.Append('K');
						index += 2;
					}
					else if (DoubleMetaphone.Contains(value, index, 2, "CI", "CE", "CY"))
					{
						if (DoubleMetaphone.Contains(value, index, 3, "CIO", "CIE", "CIA"))
						{
							result.Append('S', 'X');
						}
						else
						{
							result.Append('S');
						}
						index += 2;
					}
					else
					{
						result.Append('K');
						if (DoubleMetaphone.Contains(value, index + 1, 2, " C", " Q", " G"))
						{
							index += 3;
						}
						else if (DoubleMetaphone.Contains(value, index + 1, 1, "C", "K", "Q") && !DoubleMetaphone.Contains(value, index + 1, 2, "CE", "CI"))
						{
							index += 2;
						}
						else
						{
							index++;
						}
					}
				}
				return index;
			}
		}

		// Token: 0x0600BC2D RID: 48173 RVA: 0x0029B680 File Offset: 0x00299880
		private static int HandleCC(string value, DoubleMetaphoneResult result, int index)
		{
			checked
			{
				if (DoubleMetaphone.Contains(value, index + 2, 1, "I", "E", "H") && !DoubleMetaphone.Contains(value, index + 2, 2, "HU"))
				{
					if ((index == 1 && DoubleMetaphone.CharAt(value, index - 1) == 'A') || DoubleMetaphone.Contains(value, index - 1, 5, "UCCEE", "UCCES"))
					{
						result.Append("KS");
					}
					else
					{
						result.Append('X');
					}
					index += 3;
				}
				else
				{
					result.Append('K');
					index += 2;
				}
				return index;
			}
		}

		// Token: 0x0600BC2E RID: 48174 RVA: 0x0029B70C File Offset: 0x0029990C
		private static int HandleCH(string value, DoubleMetaphoneResult result, int index)
		{
			if (index > 0 && DoubleMetaphone.Contains(value, index, 4, "CHAE"))
			{
				result.Append('K', 'X');
				return index + 2;
			}
			if (DoubleMetaphone.ConditionCH0(value, index))
			{
				result.Append('K');
				return index + 2;
			}
			if (DoubleMetaphone.ConditionCH1(value, index))
			{
				result.Append('K');
				return index + 2;
			}
			if (index > 0)
			{
				if (DoubleMetaphone.Contains(value, 0, 2, "MC"))
				{
					result.Append('K');
				}
				else
				{
					result.Append('X', 'K');
				}
			}
			else
			{
				result.Append('X');
			}
			return index + 2;
		}

		// Token: 0x0600BC2F RID: 48175 RVA: 0x0029B798 File Offset: 0x00299998
		private static int HandleD(string value, DoubleMetaphoneResult result, int index)
		{
			checked
			{
				if (DoubleMetaphone.Contains(value, index, 2, "DG"))
				{
					if (DoubleMetaphone.Contains(value, index + 2, 1, "I", "E", "Y"))
					{
						result.Append('J');
						index += 3;
					}
					else
					{
						result.Append("TK");
						index += 2;
					}
				}
				else if (DoubleMetaphone.Contains(value, index, 2, "DT", "DD"))
				{
					result.Append('T');
					index += 2;
				}
				else
				{
					result.Append('T');
					index++;
				}
				return index;
			}
		}

		// Token: 0x0600BC30 RID: 48176 RVA: 0x0029B824 File Offset: 0x00299A24
		private static int HandleG(string value, DoubleMetaphoneResult result, int index, bool slavoGermanic)
		{
			checked
			{
				if (DoubleMetaphone.CharAt(value, index + 1) == 'H')
				{
					index = DoubleMetaphone.HandleGH(value, result, index);
				}
				else if (DoubleMetaphone.CharAt(value, index + 1) == 'N')
				{
					if (index == 1 && DoubleMetaphone.IsVowel(DoubleMetaphone.CharAt(value, 0)) && !slavoGermanic)
					{
						result.Append("KN", "N");
					}
					else if (!DoubleMetaphone.Contains(value, index + 2, 2, "EY") && DoubleMetaphone.CharAt(value, index + 1) != 'Y' && !slavoGermanic)
					{
						result.Append("N", "KN");
					}
					else
					{
						result.Append("KN");
					}
					index += 2;
				}
				else if (DoubleMetaphone.Contains(value, index + 1, 2, "LI") && !slavoGermanic)
				{
					result.Append("KL", "L");
					index += 2;
				}
				else if (index == 0 && (DoubleMetaphone.CharAt(value, index + 1) == 'Y' || DoubleMetaphone.Contains(value, index + 1, 2, DoubleMetaphone.ES_EP_EB_EL_EY_IB_IL_IN_IE_EI_ER)))
				{
					result.Append('K', 'J');
					index += 2;
				}
				else if ((DoubleMetaphone.Contains(value, index + 1, 2, "ER") || DoubleMetaphone.CharAt(value, index + 1) == 'Y') && !DoubleMetaphone.Contains(value, 0, 6, "DANGER", "RANGER", "MANGER") && !DoubleMetaphone.Contains(value, index - 1, 1, "E", "I") && !DoubleMetaphone.Contains(value, index - 1, 3, "RGY", "OGY"))
				{
					result.Append('K', 'J');
					index += 2;
				}
				else if (DoubleMetaphone.Contains(value, index + 1, 1, "E", "I", "Y") || DoubleMetaphone.Contains(value, index - 1, 4, "AGGI", "OGGI"))
				{
					if (DoubleMetaphone.Contains(value, 0, 4, "VAN ", "VON ") || DoubleMetaphone.Contains(value, 0, 3, "SCH") || DoubleMetaphone.Contains(value, index + 1, 2, "ET"))
					{
						result.Append('K');
					}
					else if (DoubleMetaphone.Contains(value, index + 1, 4, "IER"))
					{
						result.Append('J');
					}
					else
					{
						result.Append('J', 'K');
					}
					index += 2;
				}
				else if (DoubleMetaphone.CharAt(value, index + 1) == 'G')
				{
					index += 2;
					result.Append('K');
				}
				else
				{
					index++;
					result.Append('K');
				}
				return index;
			}
		}

		// Token: 0x0600BC31 RID: 48177 RVA: 0x0029BA68 File Offset: 0x00299C68
		private static int HandleGH(string value, DoubleMetaphoneResult result, int index)
		{
			if (index > 0 && !DoubleMetaphone.IsVowel(DoubleMetaphone.CharAt(value, index - 1)))
			{
				result.Append('K');
				index += 2;
			}
			else if (index == 0)
			{
				if (DoubleMetaphone.CharAt(value, index + 2) == 'I')
				{
					result.Append('J');
				}
				else
				{
					result.Append('K');
				}
				index += 2;
			}
			else if ((index > 1 && DoubleMetaphone.Contains(value, index - 2, 1, "B", "H", "D")) || (index > 2 && DoubleMetaphone.Contains(value, index - 3, 1, "B", "H", "D")) || (index > 3 && DoubleMetaphone.Contains(value, index - 4, 1, "B", "H")))
			{
				index += 2;
			}
			else
			{
				if (index > 2 && DoubleMetaphone.CharAt(value, index - 1) == 'U' && DoubleMetaphone.Contains(value, index - 3, 1, "C", "G", "L", "R", "T"))
				{
					result.Append('F');
				}
				else if (index > 0 && DoubleMetaphone.CharAt(value, index - 1) != 'I')
				{
					result.Append('K');
				}
				index += 2;
			}
			return index;
		}

		// Token: 0x0600BC32 RID: 48178 RVA: 0x0029BB85 File Offset: 0x00299D85
		private static int HandleH(string value, DoubleMetaphoneResult result, int index)
		{
			checked
			{
				if ((index == 0 || DoubleMetaphone.IsVowel(DoubleMetaphone.CharAt(value, index - 1))) && DoubleMetaphone.IsVowel(DoubleMetaphone.CharAt(value, index + 1)))
				{
					result.Append('H');
					index += 2;
				}
				else
				{
					index++;
				}
				return index;
			}
		}

		// Token: 0x0600BC33 RID: 48179 RVA: 0x0029BBC0 File Offset: 0x00299DC0
		private static int HandleJ(string value, DoubleMetaphoneResult result, int index, bool slavoGermanic)
		{
			checked
			{
				if (DoubleMetaphone.Contains(value, index, 4, "JOSE") || DoubleMetaphone.Contains(value, 0, 4, "SAN "))
				{
					if ((index == 0 && DoubleMetaphone.CharAt(value, index + 4) == ' ') || value.Length == 4 || DoubleMetaphone.Contains(value, 0, 4, "SAN "))
					{
						result.Append('H');
					}
					else
					{
						result.Append('J', 'H');
					}
					index++;
				}
				else
				{
					if (index == 0 && !DoubleMetaphone.Contains(value, index, 4, "JOSE"))
					{
						result.Append('J', 'A');
					}
					else if (DoubleMetaphone.IsVowel(DoubleMetaphone.CharAt(value, index - 1)) && !slavoGermanic && (DoubleMetaphone.CharAt(value, index + 1) == 'A' || DoubleMetaphone.CharAt(value, index + 1) == 'O'))
					{
						result.Append('J', 'H');
					}
					else if (index == value.Length - 1)
					{
						result.Append('J', ' ');
					}
					else if (!DoubleMetaphone.Contains(value, index + 1, 1, DoubleMetaphone.L_T_K_S_N_M_B_Z) && !DoubleMetaphone.Contains(value, index - 1, 1, "S", "K", "L"))
					{
						result.Append('J');
					}
					if (DoubleMetaphone.CharAt(value, index + 1) == 'J')
					{
						index += 2;
					}
					else
					{
						index++;
					}
				}
				return index;
			}
		}

		// Token: 0x0600BC34 RID: 48180 RVA: 0x0029BCF0 File Offset: 0x00299EF0
		private static int HandleL(string value, DoubleMetaphoneResult result, int index)
		{
			result.Append('L');
			checked
			{
				if (DoubleMetaphone.CharAt(value, index + 1) == 'L')
				{
					if (DoubleMetaphone.ConditionL0(value, index))
					{
						result.AppendAlternate(' ');
					}
					index += 2;
				}
				else
				{
					index++;
				}
				return index;
			}
		}

		// Token: 0x0600BC35 RID: 48181 RVA: 0x0029BD28 File Offset: 0x00299F28
		private static int HandleP(string value, DoubleMetaphoneResult result, int index)
		{
			checked
			{
				if (DoubleMetaphone.CharAt(value, index + 1) == 'H')
				{
					result.Append('F');
					index += 2;
				}
				else
				{
					result.Append('P');
					index = (DoubleMetaphone.Contains(value, index + 1, 1, "P", "B") ? (index + 2) : (index + 1));
				}
				return index;
			}
		}

		// Token: 0x0600BC36 RID: 48182 RVA: 0x0029BD7C File Offset: 0x00299F7C
		private static int HandleR(string value, DoubleMetaphoneResult result, int index, bool slavoGermanic)
		{
			checked
			{
				if (index == value.Length - 1 && !slavoGermanic && DoubleMetaphone.Contains(value, index - 2, 2, "IE") && !DoubleMetaphone.Contains(value, index - 4, 2, "ME", "MA"))
				{
					result.AppendAlternate('R');
				}
				else
				{
					result.Append('R');
				}
				if (DoubleMetaphone.CharAt(value, index + 1) != 'R')
				{
					return index + 1;
				}
				return index + 2;
			}
		}

		// Token: 0x0600BC37 RID: 48183 RVA: 0x0029BDE4 File Offset: 0x00299FE4
		private static int HandleS(string value, DoubleMetaphoneResult result, int index, bool slavoGermanic)
		{
			checked
			{
				if (DoubleMetaphone.Contains(value, index - 1, 3, "ISL", "YSL"))
				{
					index++;
				}
				else if (index == 0 && DoubleMetaphone.Contains(value, index, 5, "SUGAR"))
				{
					result.Append('X', 'S');
					index++;
				}
				else if (DoubleMetaphone.Contains(value, index, 2, "SH"))
				{
					if (DoubleMetaphone.Contains(value, index + 1, 4, "HEIM", "HOEK", "HOLM", "HOLZ"))
					{
						result.Append('S');
					}
					else
					{
						result.Append('X');
					}
					index += 2;
				}
				else if (DoubleMetaphone.Contains(value, index, 3, "SIO", "SIA") || DoubleMetaphone.Contains(value, index, 4, "SIAN"))
				{
					if (slavoGermanic)
					{
						result.Append('S');
					}
					else
					{
						result.Append('S', 'X');
					}
					index += 3;
				}
				else if ((index == 0 && DoubleMetaphone.Contains(value, index + 1, 1, "M", "N", "L", "W")) || DoubleMetaphone.Contains(value, index + 1, 1, "Z"))
				{
					result.Append('S', 'X');
					index = (DoubleMetaphone.Contains(value, index + 1, 1, "Z") ? (index + 2) : (index + 1));
				}
				else if (DoubleMetaphone.Contains(value, index, 2, "SC"))
				{
					index = DoubleMetaphone.HandleSC(value, result, index);
				}
				else
				{
					if (index == value.Length - 1 && DoubleMetaphone.Contains(value, index - 2, 2, "AI", "OI"))
					{
						result.AppendAlternate('S');
					}
					else
					{
						result.Append('S');
					}
					index = (DoubleMetaphone.Contains(value, index + 1, 1, "S", "Z") ? (index + 2) : (index + 1));
				}
				return index;
			}
		}

		// Token: 0x0600BC38 RID: 48184 RVA: 0x0029BF90 File Offset: 0x0029A190
		private static int HandleSC(string value, DoubleMetaphoneResult result, int index)
		{
			checked
			{
				if (DoubleMetaphone.CharAt(value, index + 2) == 'H')
				{
					if (DoubleMetaphone.Contains(value, index + 3, 2, "OO", "ER", "EN", "UY", "ED", "EM"))
					{
						if (DoubleMetaphone.Contains(value, index + 3, 2, "ER", "EN"))
						{
							result.Append("X", "SK");
						}
						else
						{
							result.Append("SK");
						}
					}
					else if (index == 0 && !DoubleMetaphone.IsVowel(DoubleMetaphone.CharAt(value, 3)) && DoubleMetaphone.CharAt(value, 3) != 'W')
					{
						result.Append('X', 'S');
					}
					else
					{
						result.Append('X');
					}
				}
				else if (DoubleMetaphone.Contains(value, index + 2, 1, "I", "E", "Y"))
				{
					result.Append('S');
				}
				else
				{
					result.Append("SK");
				}
				return index + 3;
			}
		}

		// Token: 0x0600BC39 RID: 48185 RVA: 0x0029C074 File Offset: 0x0029A274
		private static int HandleT(string value, DoubleMetaphoneResult result, int index)
		{
			checked
			{
				if (DoubleMetaphone.Contains(value, index, 4, "TION"))
				{
					result.Append('X');
					index += 3;
				}
				else if (DoubleMetaphone.Contains(value, index, 3, "TIA", "TCH"))
				{
					result.Append('X');
					index += 3;
				}
				else if (DoubleMetaphone.Contains(value, index, 2, "TH") || DoubleMetaphone.Contains(value, index, 3, "TTH"))
				{
					if (DoubleMetaphone.Contains(value, index + 2, 2, "OM", "AM") || DoubleMetaphone.Contains(value, 0, 4, "VAN ", "VON ") || DoubleMetaphone.Contains(value, 0, 3, "SCH"))
					{
						result.Append('T');
					}
					else
					{
						result.Append('0', 'T');
					}
					index += 2;
				}
				else
				{
					result.Append('T');
					index = (DoubleMetaphone.Contains(value, index + 1, 1, "T", "D") ? (index + 2) : (index + 1));
				}
				return index;
			}
		}

		// Token: 0x0600BC3A RID: 48186 RVA: 0x0029C164 File Offset: 0x0029A364
		private static int HandleW(string value, DoubleMetaphoneResult result, int index)
		{
			checked
			{
				if (DoubleMetaphone.Contains(value, index, 2, "WR"))
				{
					result.Append('R');
					index += 2;
				}
				else if (index == 0 && (DoubleMetaphone.IsVowel(DoubleMetaphone.CharAt(value, index + 1)) || DoubleMetaphone.Contains(value, index, 2, "WH")))
				{
					if (DoubleMetaphone.IsVowel(DoubleMetaphone.CharAt(value, index + 1)))
					{
						result.Append('A', 'F');
					}
					else
					{
						result.Append('A');
					}
					index++;
				}
				else if ((index == value.Length - 1 && DoubleMetaphone.IsVowel(DoubleMetaphone.CharAt(value, index - 1))) || DoubleMetaphone.Contains(value, index - 1, 5, "EWSKI", "EWSKY", "OWSKI", "OWSKY") || DoubleMetaphone.Contains(value, 0, 3, "SCH"))
				{
					result.AppendAlternate('F');
					index++;
				}
				else if (DoubleMetaphone.Contains(value, index, 4, "WICZ", "WITZ"))
				{
					result.Append("TS", "FX");
					index += 4;
				}
				else
				{
					index++;
				}
				return index;
			}
		}

		// Token: 0x0600BC3B RID: 48187 RVA: 0x0029C26C File Offset: 0x0029A46C
		private static int HandleX(string value, DoubleMetaphoneResult result, int index)
		{
			checked
			{
				if (index == 0)
				{
					result.Append('S');
					index++;
				}
				else
				{
					if (index != value.Length - 1 || (!DoubleMetaphone.Contains(value, index - 3, 3, "IAU", "EAU") && !DoubleMetaphone.Contains(value, index - 2, 2, "AU", "OU")))
					{
						result.Append("KS");
					}
					index = (DoubleMetaphone.Contains(value, index + 1, 1, "C", "X") ? (index + 2) : (index + 1));
				}
				return index;
			}
		}

		// Token: 0x0600BC3C RID: 48188 RVA: 0x0029C2F0 File Offset: 0x0029A4F0
		private static int HandleZ(string value, DoubleMetaphoneResult result, int index, bool slavoGermanic)
		{
			if (DoubleMetaphone.CharAt(value, index + 1) == 'H')
			{
				result.Append('J');
				index += 2;
			}
			else
			{
				if (DoubleMetaphone.Contains(value, index + 1, 2, "ZO", "ZI", "ZA") || (slavoGermanic && index > 0 && DoubleMetaphone.CharAt(value, index - 1) != 'T'))
				{
					result.Append("S", "TS");
				}
				else
				{
					result.Append('S');
				}
				index = ((DoubleMetaphone.CharAt(value, index + 1) == 'Z') ? (index + 2) : (index + 1));
			}
			return index;
		}

		// Token: 0x0600BC3D RID: 48189 RVA: 0x0029C37C File Offset: 0x0029A57C
		private static bool ConditionC0(string value, int index)
		{
			if (DoubleMetaphone.Contains(value, index, 4, "CHIA"))
			{
				return true;
			}
			if (index <= 1)
			{
				return false;
			}
			if (DoubleMetaphone.IsVowel(DoubleMetaphone.CharAt(value, index - 2)))
			{
				return false;
			}
			if (!DoubleMetaphone.Contains(value, index - 1, 3, "ACH"))
			{
				return false;
			}
			char c = DoubleMetaphone.CharAt(value, index + 2);
			return (c != 'I' && c != 'E') || DoubleMetaphone.Contains(value, index - 2, 6, "BACHER", "MACHER");
		}

		// Token: 0x0600BC3E RID: 48190 RVA: 0x0029C3F0 File Offset: 0x0029A5F0
		private static bool ConditionCH0(string value, int index)
		{
			return index == 0 && (DoubleMetaphone.Contains(value, 1, 5, "HARAC", "HARIS") || DoubleMetaphone.Contains(value, 1, 3, "HOR", "HYM", "HIA", "HEM")) && !DoubleMetaphone.Contains(value, 0, 5, "CHORE");
		}

		// Token: 0x0600BC3F RID: 48191 RVA: 0x0029C448 File Offset: 0x0029A648
		private static bool ConditionCH1(string value, int index)
		{
			return checked(DoubleMetaphone.Contains(value, 0, 4, "VAN ", "VON ") || DoubleMetaphone.Contains(value, 0, 3, "SCH") || DoubleMetaphone.Contains(value, index - 2, 6, "ORCHES", "ARCHIT", "ORCHID") || DoubleMetaphone.Contains(value, index + 2, 1, "T", "S") || ((DoubleMetaphone.Contains(value, index - 1, 1, "A", "O", "U", "E") || index == 0) && (DoubleMetaphone.Contains(value, index + 2, 1, DoubleMetaphone.L_R_N_M_B_H_F_V_W_SPACE) || index + 1 == value.Length - 1)));
		}

		// Token: 0x0600BC40 RID: 48192 RVA: 0x0029C4F4 File Offset: 0x0029A6F4
		private static bool ConditionL0(string value, int index)
		{
			return checked((index == value.Length - 3 && DoubleMetaphone.Contains(value, index - 1, 4, "ILLO", "ILLA", "ALLE")) || ((DoubleMetaphone.Contains(value, index - 1, 2, "AS", "OS") || DoubleMetaphone.Contains(value, value.Length - 1, 1, "A", "O")) && DoubleMetaphone.Contains(value, index - 1, 4, "ALLE")));
		}

		// Token: 0x0600BC41 RID: 48193 RVA: 0x0029C570 File Offset: 0x0029A770
		private static bool ConditionM0(string value, int index)
		{
			return checked(DoubleMetaphone.CharAt(value, index + 1) == 'M' || (DoubleMetaphone.Contains(value, index - 1, 3, "UMB") && (index + 1 == value.Length - 1 || DoubleMetaphone.Contains(value, index + 2, 2, "ER"))));
		}

		// Token: 0x0600BC42 RID: 48194 RVA: 0x0029C5BD File Offset: 0x0029A7BD
		private static bool IsSlavoGermanic(string value)
		{
			return value.IndexOf('W') > -1 || value.IndexOf('K') > -1 || value.IndexOf("CZ") > -1 || value.IndexOf("WITZ") > -1;
		}

		// Token: 0x0600BC43 RID: 48195 RVA: 0x0029C5F3 File Offset: 0x0029A7F3
		private static bool IsVowel(char ch)
		{
			return "AEIOUY".IndexOf(ch) != -1;
		}

		// Token: 0x0600BC44 RID: 48196 RVA: 0x0029C608 File Offset: 0x0029A808
		private static bool IsSilentStart(string value)
		{
			bool result = false;
			for (int i = 0; i < DoubleMetaphone.SILENT_START.Length; i++)
			{
				if (value.StartsWith(DoubleMetaphone.SILENT_START[i]))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600BC45 RID: 48197 RVA: 0x0029C63D File Offset: 0x0029A83D
		private static string CleanInput(string input)
		{
			if (input == null)
			{
				return null;
			}
			input = input.Trim();
			if (input.Length == 0)
			{
				return null;
			}
			return input.ToUpper();
		}

		// Token: 0x0600BC46 RID: 48198 RVA: 0x0029C65C File Offset: 0x0029A85C
		protected static char CharAt(string value, int index)
		{
			if (index < 0 || index >= value.Length)
			{
				return '\0';
			}
			return value[index];
		}

		// Token: 0x0600BC47 RID: 48199 RVA: 0x0029C674 File Offset: 0x0029A874
		private static bool Contains(string value, int start, int length, string criteria)
		{
			return DoubleMetaphone.Contains(value, start, length, new string[]
			{
				criteria
			});
		}

		// Token: 0x0600BC48 RID: 48200 RVA: 0x0029C698 File Offset: 0x0029A898
		private static bool Contains(string value, int start, int length, string criteria1, string criteria2)
		{
			return DoubleMetaphone.Contains(value, start, length, new string[]
			{
				criteria1,
				criteria2
			});
		}

		// Token: 0x0600BC49 RID: 48201 RVA: 0x0029C6C0 File Offset: 0x0029A8C0
		private static bool Contains(string value, int start, int length, string criteria1, string criteria2, string criteria3)
		{
			return DoubleMetaphone.Contains(value, start, length, new string[]
			{
				criteria1,
				criteria2,
				criteria3
			});
		}

		// Token: 0x0600BC4A RID: 48202 RVA: 0x0029C6EC File Offset: 0x0029A8EC
		private static bool Contains(string value, int start, int length, string criteria1, string criteria2, string criteria3, string criteria4)
		{
			return DoubleMetaphone.Contains(value, start, length, new string[]
			{
				criteria1,
				criteria2,
				criteria3,
				criteria4
			});
		}

		// Token: 0x0600BC4B RID: 48203 RVA: 0x0029C71C File Offset: 0x0029A91C
		private static bool Contains(string value, int start, int length, string criteria1, string criteria2, string criteria3, string criteria4, string criteria5)
		{
			return DoubleMetaphone.Contains(value, start, length, new string[]
			{
				criteria1,
				criteria2,
				criteria3,
				criteria4,
				criteria5
			});
		}

		// Token: 0x0600BC4C RID: 48204 RVA: 0x0029C754 File Offset: 0x0029A954
		private static bool Contains(string value, int start, int length, string criteria1, string criteria2, string criteria3, string criteria4, string criteria5, string criteria6)
		{
			return DoubleMetaphone.Contains(value, start, length, new string[]
			{
				criteria1,
				criteria2,
				criteria3,
				criteria4,
				criteria5,
				criteria6
			});
		}

		// Token: 0x0600BC4D RID: 48205 RVA: 0x0029C790 File Offset: 0x0029A990
		protected static bool Contains(string value, int start, int length, string[] criteria)
		{
			bool result = false;
			if (start >= 0 && start + length <= value.Length)
			{
				string a = value.Substring(start, length);
				for (int i = 0; i < criteria.Length; i++)
				{
					if (a == criteria[i])
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x04003170 RID: 12656
		private const string VOWELS = "AEIOUY";

		// Token: 0x04003171 RID: 12657
		private static readonly string[] SILENT_START = new string[]
		{
			"GN",
			"KN",
			"PN",
			"WR",
			"PS"
		};

		// Token: 0x04003172 RID: 12658
		private static readonly string[] L_R_N_M_B_H_F_V_W_SPACE = new string[]
		{
			"L",
			"R",
			"N",
			"M",
			"B",
			"H",
			"F",
			"V",
			"W",
			" "
		};

		// Token: 0x04003173 RID: 12659
		private static readonly string[] ES_EP_EB_EL_EY_IB_IL_IN_IE_EI_ER = new string[]
		{
			"ES",
			"EP",
			"EB",
			"EL",
			"EY",
			"IB",
			"IL",
			"IN",
			"IE",
			"EI",
			"ER"
		};

		// Token: 0x04003174 RID: 12660
		private static readonly string[] L_T_K_S_N_M_B_Z = new string[]
		{
			"L",
			"T",
			"K",
			"S",
			"N",
			"M",
			"B",
			"Z"
		};

		// Token: 0x04003175 RID: 12661
		private int maxCodeLen = 4;
	}
}
