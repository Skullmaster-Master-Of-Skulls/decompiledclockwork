using System;
using System.Collections;
using System.Globalization;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x020000EE RID: 238
	public class DN
	{
		// Token: 0x060005CD RID: 1485 RVA: 0x0001B758 File Offset: 0x0001A758
		private void InitBlock()
		{
			this.rdnList = new ArrayList();
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0001B770 File Offset: 0x0001A770
		public virtual ArrayList RDNs
		{
			get
			{
				int count = this.rdnList.Count;
				ArrayList arrayList = new ArrayList(count);
				for (int i = 0; i < count; i++)
				{
					arrayList.Add(this.rdnList[i]);
				}
				return arrayList;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001B7B4 File Offset: 0x0001A7B4
		public virtual DN Parent
		{
			get
			{
				DN dn = new DN();
				dn.rdnList = (ArrayList)this.rdnList.Clone();
				if (dn.rdnList.Count >= 1)
				{
					dn.rdnList.Remove(this.rdnList[0]);
				}
				return dn;
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001B808 File Offset: 0x0001A808
		public DN()
		{
			this.InitBlock();
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001B824 File Offset: 0x0001A824
		public DN(string dnString)
		{
			this.InitBlock();
			if (dnString.Length != 0)
			{
				char[] array = new char[dnString.Length];
				int num = 0;
				string attrType = "";
				int num2 = 0;
				RDN rdn = new RDN();
				int num3 = 0;
				int i = 0;
				int num4 = 0;
				int num5 = 1;
				int num6 = dnString.Length - 1;
				while (i <= num6)
				{
					char c = dnString[i];
					switch (num5)
					{
					case 1:
						while (c == ' ' && i < num6)
						{
							c = dnString[++i];
						}
						if (this.isAlpha(c))
						{
							if (dnString.Substring(i).StartsWith("oid.") || dnString.Substring(i).StartsWith("OID."))
							{
								i += 4;
								if (i > num6)
								{
									throw new ArgumentException(dnString);
								}
								c = dnString[i];
								if (!this.isDigit(c))
								{
									throw new ArgumentException(dnString);
								}
								array[num3++] = c;
								num5 = 3;
							}
							else
							{
								array[num3++] = c;
								num5 = 2;
							}
						}
						else if (this.isDigit(c))
						{
							i--;
							num5 = 3;
						}
						else if (char.GetUnicodeCategory(c) != UnicodeCategory.SpaceSeparator)
						{
							throw new ArgumentException(dnString);
						}
						break;
					case 2:
						if (this.isAlpha(c) || this.isDigit(c) || c == '-')
						{
							array[num3++] = c;
						}
						else
						{
							while (c == ' ' && i < num6)
							{
								c = dnString[++i];
							}
							if (c != '=')
							{
								throw new ArgumentException(dnString);
							}
							attrType = new string(array, 0, num3);
							num3 = 0;
							num5 = 4;
						}
						break;
					case 3:
					{
						if (!this.isDigit(c))
						{
							throw new ArgumentException(dnString);
						}
						bool flag = c == '0';
						array[num3++] = c;
						c = dnString[++i];
						if ((this.isDigit(c) && flag) || (c == '.' && flag))
						{
							throw new ArgumentException(dnString);
						}
						while (this.isDigit(c) && i < num6)
						{
							array[num3++] = c;
							c = dnString[++i];
						}
						if (c == '.')
						{
							array[num3++] = c;
						}
						else
						{
							while (c == ' ' && i < num6)
							{
								c = dnString[++i];
							}
							if (c != '=')
							{
								throw new ArgumentException(dnString);
							}
							attrType = new string(array, 0, num3);
							num3 = 0;
							num5 = 4;
						}
						break;
					}
					case 4:
						while (c == ' ')
						{
							if (i >= num6)
							{
								throw new ArgumentException(dnString);
							}
							c = dnString[++i];
						}
						if (c == '"')
						{
							num5 = 5;
							num4 = i;
						}
						else if (c == '#')
						{
							num2 = 0;
							array[num3++] = c;
							num4 = i;
							num5 = 6;
						}
						else
						{
							num4 = i;
							i--;
							num5 = 7;
						}
						break;
					case 5:
						if (c == '"')
						{
							string rawValue = dnString.Substring(num4, i + 1 - num4);
							if (i < num6)
							{
								c = dnString[++i];
							}
							while (c == ' ' && i < num6)
							{
								c = dnString[++i];
							}
							if (c != ',' && c != ';' && c != '+' && i != num6)
							{
								throw new ArgumentException(dnString);
							}
							string attrValue = new string(array, 0, num3);
							rdn.add(attrType, attrValue, rawValue);
							if (c != '+')
							{
								this.rdnList.Add(rdn);
								rdn = new RDN();
							}
							num = 0;
							num3 = 0;
							num5 = 1;
						}
						else if (c == '\\')
						{
							c = dnString[++i];
							if (DN.isHexDigit(c))
							{
								char c2 = dnString[++i];
								if (!DN.isHexDigit(c2))
								{
									throw new ArgumentException(dnString);
								}
								array[num3++] = DN.hexToChar(c, c2);
								num = 0;
							}
							else
							{
								if (!this.needsEscape(c) && c != '#' && c != '=' && c != ' ')
								{
									throw new ArgumentException(dnString);
								}
								array[num3++] = c;
								num = 0;
							}
						}
						else
						{
							array[num3++] = c;
						}
						break;
					case 6:
						if (!DN.isHexDigit(c) || i > num6)
						{
							if (num2 % 2 != 0 || num2 == 0)
							{
								throw new ArgumentException(dnString);
							}
							string rawValue = dnString.Substring(num4, i - num4);
							while (c == ' ' && i < num6)
							{
								c = dnString[++i];
							}
							if (c != ',' && c != ';' && c != '+' && i != num6)
							{
								throw new ArgumentException(dnString);
							}
							string attrValue = new string(array, 0, num3);
							rdn.add(attrType, attrValue, rawValue);
							if (c != '+')
							{
								this.rdnList.Add(rdn);
								rdn = new RDN();
							}
							num3 = 0;
							num5 = 1;
						}
						else
						{
							array[num3++] = c;
							num2++;
						}
						break;
					case 7:
						if (c == '\\')
						{
							if (i >= num6)
							{
								throw new ArgumentException(dnString);
							}
							c = dnString[++i];
							if (DN.isHexDigit(c))
							{
								if (i >= num6)
								{
									throw new ArgumentException(dnString);
								}
								char c2 = dnString[++i];
								if (!DN.isHexDigit(c2))
								{
									throw new ArgumentException(dnString);
								}
								array[num3++] = DN.hexToChar(c, c2);
								num = 0;
							}
							else
							{
								if (!this.needsEscape(c) && c != '#' && c != '=' && c != ' ')
								{
									throw new ArgumentException(dnString);
								}
								array[num3++] = c;
								num = 0;
							}
						}
						else if (c == ' ')
						{
							num++;
							array[num3++] = c;
						}
						else if (c == ',' || c == ';' || c == '+')
						{
							string attrValue = new string(array, 0, num3 - num);
							string rawValue = dnString.Substring(num4, i - num - num4);
							rdn.add(attrType, attrValue, rawValue);
							if (c != '+')
							{
								this.rdnList.Add(rdn);
								rdn = new RDN();
							}
							num = 0;
							num3 = 0;
							num5 = 1;
						}
						else
						{
							if (this.needsEscape(c))
							{
								throw new ArgumentException(dnString);
							}
							num = 0;
							array[num3++] = c;
						}
						break;
					}
					i++;
				}
				if (num5 == 7 || (num5 == 6 && num2 % 2 == 0 && num2 != 0))
				{
					string attrValue = new string(array, 0, num3 - num);
					string rawValue = dnString.Substring(num4, i - num - num4);
					rdn.add(attrType, attrValue, rawValue);
					this.rdnList.Add(rdn);
				}
				else
				{
					if (num5 != 4)
					{
						throw new ArgumentException(dnString);
					}
					string attrValue = "";
					string rawValue = dnString.Substring(num4);
					rdn.add(attrType, attrValue, rawValue);
					this.rdnList.Add(rdn);
				}
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001BE9C File Offset: 0x0001AE9C
		private bool isAlpha(char ch)
		{
			return (ch < '[' && ch > '@') || (ch < '{' && ch > '`');
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001BEC8 File Offset: 0x0001AEC8
		private bool isDigit(char ch)
		{
			return ch < ':' && ch > '/';
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001BEE8 File Offset: 0x0001AEE8
		private static bool isHexDigit(char ch)
		{
			return (ch < ':' && ch > '/') || (ch < 'G' && ch > '@') || (ch < 'g' && ch > '`');
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001BF1C File Offset: 0x0001AF1C
		private bool needsEscape(char ch)
		{
			return ch == ',' || ch == '+' || ch == '"' || ch == ';' || ch == '<' || ch == '>' || ch == '\\';
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001BF58 File Offset: 0x0001AF58
		private static char hexToChar(char hex1, char hex0)
		{
			int num;
			if (hex1 < ':' && hex1 > '/')
			{
				num = (int)((hex1 - '0') * '\u0010');
			}
			else if (hex1 < 'G' && hex1 > '@')
			{
				num = (int)((hex1 - '7') * '\u0010');
			}
			else
			{
				if (hex1 >= 'g' || hex1 <= '`')
				{
					throw new ArgumentException("Not hex digit");
				}
				num = (int)((hex1 - 'W') * '\u0010');
			}
			if (hex0 < ':' && hex0 > '/')
			{
				num += (int)(hex0 - '0');
			}
			else if (hex0 < 'G' && hex0 > '@')
			{
				num += (int)(hex0 - '7');
			}
			else
			{
				if (hex0 >= 'g' || hex0 <= '`')
				{
					throw new ArgumentException("Not hex digit");
				}
				num += (int)(hex0 - 'W');
			}
			return (char)num;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001BFF8 File Offset: 0x0001AFF8
		public override string ToString()
		{
			int count = this.rdnList.Count;
			string result;
			if (count < 1)
			{
				result = null;
			}
			else
			{
				string text = this.rdnList[0].ToString();
				for (int i = 1; i < count; i++)
				{
					text = text + "," + this.rdnList[i].ToString();
				}
				result = text;
			}
			return result;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001C060 File Offset: 0x0001B060
		public ArrayList getrdnList()
		{
			return this.rdnList;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001C078 File Offset: 0x0001B078
		public override bool Equals(object toDN)
		{
			return this.Equals((DN)toDN);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001C098 File Offset: 0x0001B098
		public bool Equals(DN toDN)
		{
			ArrayList arrayList = toDN.getrdnList();
			int count = arrayList.Count;
			bool result;
			if (this.rdnList.Count != count)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					if (!((RDN)this.rdnList[i]).equals((RDN)toDN.getrdnList()[i]))
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001C104 File Offset: 0x0001B104
		public virtual string[] explodeDN(bool noTypes)
		{
			int count = this.rdnList.Count;
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = ((RDN)this.rdnList[i]).toString(noTypes);
			}
			return array;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001C150 File Offset: 0x0001B150
		public virtual int countRDNs()
		{
			return this.rdnList.Count;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001C16C File Offset: 0x0001B16C
		public virtual bool isDescendantOf(DN containerDN)
		{
			int num = containerDN.rdnList.Count - 1;
			int num2 = this.rdnList.Count - 1;
			while (!((RDN)this.rdnList[num2]).equals((RDN)containerDN.rdnList[num]))
			{
				num2--;
				if (num2 <= 0)
				{
					return false;
				}
			}
			num--;
			num2--;
			while (num >= 0 && num2 >= 0)
			{
				if (!((RDN)this.rdnList[num2]).equals((RDN)containerDN.rdnList[num]))
				{
					return false;
				}
				num--;
				num2--;
			}
			return num2 != 0 || num != 0;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001C224 File Offset: 0x0001B224
		public virtual void addRDN(RDN rdn)
		{
			this.rdnList.Insert(0, rdn);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001C240 File Offset: 0x0001B240
		public virtual void addRDNToFront(RDN rdn)
		{
			this.rdnList.Insert(0, rdn);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001C25C File Offset: 0x0001B25C
		public virtual void addRDNToBack(RDN rdn)
		{
			this.rdnList.Add(rdn);
		}

		// Token: 0x0400043C RID: 1084
		private const int LOOK_FOR_RDN_ATTR_TYPE = 1;

		// Token: 0x0400043D RID: 1085
		private const int ALPHA_ATTR_TYPE = 2;

		// Token: 0x0400043E RID: 1086
		private const int OID_ATTR_TYPE = 3;

		// Token: 0x0400043F RID: 1087
		private const int LOOK_FOR_RDN_VALUE = 4;

		// Token: 0x04000440 RID: 1088
		private const int QUOTED_RDN_VALUE = 5;

		// Token: 0x04000441 RID: 1089
		private const int HEX_RDN_VALUE = 6;

		// Token: 0x04000442 RID: 1090
		private const int UNQUOTED_RDN_VALUE = 7;

		// Token: 0x04000443 RID: 1091
		private ArrayList rdnList;
	}
}
