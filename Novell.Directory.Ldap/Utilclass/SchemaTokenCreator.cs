using System;
using System.IO;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x020000FB RID: 251
	public class SchemaTokenCreator
	{
		// Token: 0x06000630 RID: 1584 RVA: 0x0001E930 File Offset: 0x0001D930
		private void Initialise()
		{
			this.ctype = new sbyte[256];
			this.buf = new char[20];
			this.peekchar = int.MaxValue;
			this.WordCharacters(97, 122);
			this.WordCharacters(65, 90);
			this.WordCharacters(160, 255);
			this.WhitespaceCharacters(0, 32);
			this.CommentCharacter(47);
			this.QuoteCharacter(34);
			this.QuoteCharacter(39);
			this.parseNumbers();
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001E9B0 File Offset: 0x0001D9B0
		public SchemaTokenCreator(Stream instream)
		{
			this.Initialise();
			if (instream == null)
			{
				throw new NullReferenceException();
			}
			this.input = instream;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001EA14 File Offset: 0x0001DA14
		public SchemaTokenCreator(StreamReader r)
		{
			this.Initialise();
			if (r == null)
			{
				throw new NullReferenceException();
			}
			this.reader = r;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001EA78 File Offset: 0x0001DA78
		public SchemaTokenCreator(StringReader r)
		{
			this.Initialise();
			if (r == null)
			{
				throw new NullReferenceException();
			}
			this.sreader = r;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001EADC File Offset: 0x0001DADC
		public void pushBack()
		{
			this.pushedback = true;
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001EAF0 File Offset: 0x0001DAF0
		public int CurrentLine
		{
			get
			{
				return this.linenumber;
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001EB08 File Offset: 0x0001DB08
		public string ToStringValue()
		{
			int num = this.lastttype;
			string result;
			switch (num)
			{
			case -5:
				result = this.StringValue;
				break;
			case -4:
			case -2:
				result = "n=" + this.NumberValue;
				break;
			case -3:
				result = this.StringValue;
				break;
			case -1:
				result = "EOF";
				break;
			default:
				if (num != 10)
				{
					if (this.lastttype < 256 && (this.ctype[this.lastttype] & 8) != 0)
					{
						result = this.StringValue;
					}
					else
					{
						char[] array = new char[3];
						array[0] = (array[2] = '\'');
						array[1] = (char)this.lastttype;
						result = new string(array);
					}
				}
				else
				{
					result = "EOL";
				}
				break;
			}
			return result;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001EBD0 File Offset: 0x0001DBD0
		public void WordCharacters(int min, int max)
		{
			if (min < 0)
			{
				min = 0;
			}
			if (max >= this.ctype.Length)
			{
				max = this.ctype.Length - 1;
			}
			while (min <= max)
			{
				sbyte[] array;
				IntPtr intPtr;
				(array = this.ctype)[(int)(intPtr = (IntPtr)(min++))] = (array[(int)intPtr] | 4);
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001EC18 File Offset: 0x0001DC18
		public void WhitespaceCharacters(int min, int max)
		{
			if (min < 0)
			{
				min = 0;
			}
			if (max >= this.ctype.Length)
			{
				max = this.ctype.Length - 1;
			}
			while (min <= max)
			{
				this.ctype[min++] = 1;
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001EC58 File Offset: 0x0001DC58
		public void OrdinaryCharacters(int min, int max)
		{
			if (min < 0)
			{
				min = 0;
			}
			if (max >= this.ctype.Length)
			{
				max = this.ctype.Length - 1;
			}
			while (min <= max)
			{
				this.ctype[min++] = 0;
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001EC98 File Offset: 0x0001DC98
		public void OrdinaryCharacter(int ch)
		{
			if (ch >= 0 && ch < this.ctype.Length)
			{
				this.ctype[ch] = 0;
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001ECC0 File Offset: 0x0001DCC0
		public void CommentCharacter(int ch)
		{
			if (ch >= 0 && ch < this.ctype.Length)
			{
				this.ctype[ch] = 16;
			}
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001ECE8 File Offset: 0x0001DCE8
		public void InitTable()
		{
			int num = this.ctype.Length;
			while (--num >= 0)
			{
				this.ctype[num] = 0;
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001ED14 File Offset: 0x0001DD14
		public void QuoteCharacter(int ch)
		{
			if (ch >= 0 && ch < this.ctype.Length)
			{
				this.ctype[ch] = 8;
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001ED3C File Offset: 0x0001DD3C
		public void parseNumbers()
		{
			sbyte[] array;
			for (int i = 48; i <= 57; i++)
			{
				IntPtr intPtr;
				(array = this.ctype)[(int)(intPtr = (IntPtr)i)] = (array[(int)intPtr] | 2);
			}
			(array = this.ctype)[46] = (array[46] | 2);
			(array = this.ctype)[45] = (array[45] | 2);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001ED90 File Offset: 0x0001DD90
		private int read()
		{
			int result;
			if (this.sreader != null)
			{
				result = this.sreader.Read();
			}
			else if (this.reader != null)
			{
				result = this.reader.Read();
			}
			else
			{
				if (this.input == null)
				{
					throw new SystemException();
				}
				result = this.input.ReadByte();
			}
			return result;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001EDE8 File Offset: 0x0001DDE8
		public int nextToken()
		{
			int result;
			if (this.pushedback)
			{
				this.pushedback = false;
				result = this.lastttype;
			}
			else
			{
				this.StringValue = null;
				int num = this.peekchar;
				if (num < 0)
				{
					num = int.MaxValue;
				}
				if (num == 2147483646)
				{
					num = this.read();
					if (num < 0)
					{
						return this.lastttype = -1;
					}
					if (num == 10)
					{
						num = int.MaxValue;
					}
				}
				if (num == 2147483647)
				{
					num = this.read();
					if (num < 0)
					{
						return this.lastttype = -1;
					}
				}
				this.lastttype = num;
				this.peekchar = int.MaxValue;
				int num2 = (int)((num < 256) ? this.ctype[num] : 4);
				while ((num2 & 1) != 0)
				{
					if (num == 13)
					{
						this.linenumber++;
						if (this.iseolsig)
						{
							this.peekchar = 2147483646;
							return this.lastttype = 10;
						}
						num = this.read();
						if (num == 10)
						{
							num = this.read();
						}
					}
					else
					{
						if (num == 10)
						{
							this.linenumber++;
							if (this.iseolsig)
							{
								return this.lastttype = 10;
							}
						}
						num = this.read();
					}
					if (num < 0)
					{
						return this.lastttype = -1;
					}
					num2 = (int)((num < 256) ? this.ctype[num] : 4);
				}
				if ((num2 & 2) != 0)
				{
					bool flag = false;
					if (num == 45)
					{
						num = this.read();
						if (num != 46 && (num < 48 || num > 57))
						{
							this.peekchar = num;
							return this.lastttype = 45;
						}
						flag = true;
					}
					double num3 = 0.0;
					int i = 0;
					int num4 = 0;
					for (;;)
					{
						if (num == 46 && num4 == 0)
						{
							num4 = 1;
						}
						else
						{
							if (48 > num || num > 57)
							{
								break;
							}
							num3 = num3 * 10.0 + (double)(num - 48);
							i += num4;
						}
						num = this.read();
					}
					this.peekchar = num;
					if (i != 0)
					{
						double num5 = 10.0;
						for (i--; i > 0; i--)
						{
							num5 *= 10.0;
						}
						num3 /= num5;
					}
					this.NumberValue = (flag ? (-num3) : num3);
					result = (this.lastttype = -2);
				}
				else if ((num2 & 4) != 0)
				{
					int num6 = 0;
					do
					{
						if (num6 >= this.buf.Length)
						{
							char[] destinationArray = new char[this.buf.Length * 2];
							Array.Copy(this.buf, 0, destinationArray, 0, this.buf.Length);
							this.buf = destinationArray;
						}
						this.buf[num6++] = (char)num;
						num = this.read();
						num2 = (int)((num < 0) ? 1 : ((num < 256) ? this.ctype[num] : 4));
					}
					while ((num2 & 6) != 0);
					this.peekchar = num;
					this.StringValue = new string(this.buf, 0, num6);
					if (this.cidtolower)
					{
						this.StringValue = this.StringValue.ToLower();
					}
					result = (this.lastttype = -3);
				}
				else if ((num2 & 8) != 0)
				{
					this.lastttype = num;
					int num7 = 0;
					int num8 = this.read();
					while (num8 >= 0 && num8 != this.lastttype && num8 != 10 && num8 != 13)
					{
						if (num8 == 92)
						{
							num = this.read();
							int num9 = num;
							if (num < 48 || num > 55)
							{
								int num10 = num;
								if (num10 <= 102)
								{
									switch (num10)
									{
									case 97:
										num = 7;
										break;
									case 98:
										num = 8;
										break;
									default:
										if (num10 == 102)
										{
											num = 12;
										}
										break;
									}
								}
								else if (num10 != 110)
								{
									switch (num10)
									{
									case 114:
										num = 13;
										break;
									case 116:
										num = 9;
										break;
									case 118:
										num = 11;
										break;
									}
								}
								else
								{
									num = 10;
								}
								IL_429:
								num8 = this.read();
								goto IL_431;
								goto IL_429;
							}
							num -= 48;
							int num11 = this.read();
							if (48 <= num11 && num11 <= 55)
							{
								num = (num << 3) + (num11 - 48);
								num11 = this.read();
								if (48 <= num11 && num11 <= 55 && num9 <= 51)
								{
									num = (num << 3) + (num11 - 48);
									num8 = this.read();
								}
								else
								{
									num8 = num11;
								}
							}
							else
							{
								num8 = num11;
							}
							IL_431:;
						}
						else
						{
							num = num8;
							num8 = this.read();
						}
						if (num7 >= this.buf.Length)
						{
							char[] destinationArray2 = new char[this.buf.Length * 2];
							Array.Copy(this.buf, 0, destinationArray2, 0, this.buf.Length);
							this.buf = destinationArray2;
						}
						this.buf[num7++] = (char)num;
					}
					this.peekchar = ((num8 == this.lastttype) ? int.MaxValue : num8);
					this.StringValue = new string(this.buf, 0, num7);
					result = this.lastttype;
				}
				else if (num == 47 && (this.cppcomments || this.ccomments))
				{
					num = this.read();
					if (num == 42 && this.ccomments)
					{
						int num12 = 0;
						while ((num = this.read()) != 47 || num12 != 42)
						{
							if (num == 13)
							{
								this.linenumber++;
								num = this.read();
								if (num == 10)
								{
									num = this.read();
								}
							}
							else if (num == 10)
							{
								this.linenumber++;
								num = this.read();
							}
							if (num < 0)
							{
								return this.lastttype = -1;
							}
							num12 = num;
						}
						result = this.nextToken();
					}
					else if (num == 47 && this.cppcomments)
					{
						while ((num = this.read()) != 10 && num != 13 && num >= 0)
						{
						}
						this.peekchar = num;
						result = this.nextToken();
					}
					else if ((this.ctype[47] & 16) != 0)
					{
						while ((num = this.read()) != 10 && num != 13 && num >= 0)
						{
						}
						this.peekchar = num;
						result = this.nextToken();
					}
					else
					{
						this.peekchar = num;
						result = (this.lastttype = 47);
					}
				}
				else if ((num2 & 16) != 0)
				{
					while ((num = this.read()) != 10 && num != 13 && num >= 0)
					{
					}
					this.peekchar = num;
					result = this.nextToken();
				}
				else
				{
					result = (this.lastttype = num);
				}
			}
			return result;
		}

		// Token: 0x040004AE RID: 1198
		private string basestring;

		// Token: 0x040004AF RID: 1199
		private bool cppcomments = false;

		// Token: 0x040004B0 RID: 1200
		private bool ccomments = false;

		// Token: 0x040004B1 RID: 1201
		private bool iseolsig = false;

		// Token: 0x040004B2 RID: 1202
		private bool cidtolower;

		// Token: 0x040004B3 RID: 1203
		private bool pushedback;

		// Token: 0x040004B4 RID: 1204
		private int peekchar;

		// Token: 0x040004B5 RID: 1205
		private sbyte[] ctype;

		// Token: 0x040004B6 RID: 1206
		private int linenumber = 1;

		// Token: 0x040004B7 RID: 1207
		private int ichar = 1;

		// Token: 0x040004B8 RID: 1208
		private char[] buf;

		// Token: 0x040004B9 RID: 1209
		private StreamReader reader = null;

		// Token: 0x040004BA RID: 1210
		private StringReader sreader = null;

		// Token: 0x040004BB RID: 1211
		private Stream input = null;

		// Token: 0x040004BC RID: 1212
		public string StringValue;

		// Token: 0x040004BD RID: 1213
		public double NumberValue;

		// Token: 0x040004BE RID: 1214
		public int lastttype;
	}
}
