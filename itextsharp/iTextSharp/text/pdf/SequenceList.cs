using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.util;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000322 RID: 802
	public class SequenceList
	{
		// Token: 0x06001D32 RID: 7474 RVA: 0x000AF204 File Offset: 0x000AE204
		protected SequenceList(string range)
		{
			this.ptr = 0;
			this.text = range.ToCharArray();
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x000AF220 File Offset: 0x000AE220
		protected char NextChar()
		{
			while (this.ptr < this.text.Length)
			{
				char c = this.text[this.ptr++];
				if (c > ' ')
				{
					return c;
				}
			}
			return char.MaxValue;
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x000AF262 File Offset: 0x000AE262
		protected void PutBack()
		{
			this.ptr--;
			if (this.ptr < 0)
			{
				this.ptr = 0;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x000AF284 File Offset: 0x000AE284
		protected int Type
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = 0;
				for (;;)
				{
					char c = this.NextChar();
					if (c == '￿')
					{
						break;
					}
					switch (num)
					{
					case 0:
					{
						char c2 = c;
						if (c2 == '!')
						{
							return 3;
						}
						switch (c2)
						{
						case ',':
							return 1;
						case '-':
							return 2;
						default:
							stringBuilder.Append(c);
							if (c >= '0' && c <= '9')
							{
								num = 1;
							}
							else
							{
								num = 2;
							}
							break;
						}
						break;
					}
					case 1:
						if (c < '0' || c > '9')
						{
							goto IL_C7;
						}
						stringBuilder.Append(c);
						break;
					case 2:
						if ("-,!0123456789".IndexOf(c) >= 0)
						{
							goto IL_106;
						}
						stringBuilder.Append(c);
						break;
					}
				}
				if (num == 1)
				{
					this.number = int.Parse(this.other = stringBuilder.ToString());
					return 5;
				}
				if (num == 2)
				{
					this.other = stringBuilder.ToString().ToLower(CultureInfo.InvariantCulture);
					return 4;
				}
				return 6;
				IL_C7:
				this.PutBack();
				this.number = int.Parse(this.other = stringBuilder.ToString());
				return 5;
				IL_106:
				this.PutBack();
				this.other = stringBuilder.ToString().ToLower(CultureInfo.InvariantCulture);
				return 4;
			}
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x000AF3B4 File Offset: 0x000AE3B4
		private void OtherProc()
		{
			if (this.other.Equals("odd") || this.other.Equals("o"))
			{
				this.odd = true;
				this.even = false;
				return;
			}
			if (this.other.Equals("even") || this.other.Equals("e"))
			{
				this.odd = false;
				this.even = true;
			}
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x000AF428 File Offset: 0x000AE428
		protected bool GetAttributes()
		{
			this.low = -1;
			this.high = -1;
			this.odd = (this.even = (this.inverse = false));
			int num = 2;
			int type;
			for (;;)
			{
				type = this.Type;
				if (type == 6 || type == 1)
				{
					break;
				}
				switch (num)
				{
				case 1:
					switch (type)
					{
					case 2:
						num = 3;
						break;
					case 3:
						this.inverse = true;
						num = 2;
						this.high = this.low;
						break;
					default:
						this.high = this.low;
						num = 2;
						this.OtherProc();
						break;
					}
					break;
				case 2:
					switch (type)
					{
					case 2:
						num = 3;
						break;
					case 3:
						this.inverse = true;
						break;
					default:
						if (type == 5)
						{
							this.low = this.number;
							num = 1;
						}
						else
						{
							this.OtherProc();
						}
						break;
					}
					break;
				case 3:
					switch (type)
					{
					case 2:
						continue;
					case 3:
						this.inverse = true;
						num = 2;
						continue;
					case 5:
						this.high = this.number;
						num = 2;
						continue;
					}
					num = 2;
					this.OtherProc();
					break;
				}
			}
			if (num == 1)
			{
				this.high = this.low;
			}
			return type == 6;
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x000AF578 File Offset: 0x000AE578
		public static ICollection<int> Expand(string ranges, int maxNumber)
		{
			SequenceList sequenceList = new SequenceList(ranges);
			List<int> list = new List<int>();
			bool flag = false;
			while (!flag)
			{
				flag = sequenceList.GetAttributes();
				if (sequenceList.low != -1 || sequenceList.high != -1 || sequenceList.even || sequenceList.odd)
				{
					if (sequenceList.low < 1)
					{
						sequenceList.low = 1;
					}
					if (sequenceList.high < 1 || sequenceList.high > maxNumber)
					{
						sequenceList.high = maxNumber;
					}
					if (sequenceList.low > maxNumber)
					{
						sequenceList.low = maxNumber;
					}
					int num = 1;
					if (sequenceList.inverse)
					{
						if (sequenceList.low > sequenceList.high)
						{
							int num2 = sequenceList.low;
							sequenceList.low = sequenceList.high;
							sequenceList.high = num2;
						}
						ListIterator<int> listIterator = new ListIterator<int>(list);
						while (listIterator.HasNext())
						{
							int num3 = listIterator.Next();
							if ((!sequenceList.even || (num3 & 1) != 1) && (!sequenceList.odd || (num3 & 1) != 0) && num3 >= sequenceList.low && num3 <= sequenceList.high)
							{
								listIterator.Remove();
							}
						}
					}
					else if (sequenceList.low > sequenceList.high)
					{
						num = -1;
						if (sequenceList.odd || sequenceList.even)
						{
							num--;
							if (sequenceList.even)
							{
								sequenceList.low &= -2;
							}
							else
							{
								sequenceList.low -= (((sequenceList.low & 1) == 1) ? 0 : 1);
							}
						}
						for (int i = sequenceList.low; i >= sequenceList.high; i += num)
						{
							list.Add(i);
						}
					}
					else
					{
						if (sequenceList.odd || sequenceList.even)
						{
							num++;
							if (sequenceList.odd)
							{
								sequenceList.low |= 1;
							}
							else
							{
								sequenceList.low += (((sequenceList.low & 1) == 1) ? 1 : 0);
							}
						}
						for (int j = sequenceList.low; j <= sequenceList.high; j += num)
						{
							list.Add(j);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x04001413 RID: 5139
		protected const int COMMA = 1;

		// Token: 0x04001414 RID: 5140
		protected const int MINUS = 2;

		// Token: 0x04001415 RID: 5141
		protected const int NOT = 3;

		// Token: 0x04001416 RID: 5142
		protected const int TEXT = 4;

		// Token: 0x04001417 RID: 5143
		protected const int NUMBER = 5;

		// Token: 0x04001418 RID: 5144
		protected const int END = 6;

		// Token: 0x04001419 RID: 5145
		protected const char EOT = '￿';

		// Token: 0x0400141A RID: 5146
		private const int FIRST = 0;

		// Token: 0x0400141B RID: 5147
		private const int DIGIT = 1;

		// Token: 0x0400141C RID: 5148
		private const int OTHER = 2;

		// Token: 0x0400141D RID: 5149
		private const int DIGIT2 = 3;

		// Token: 0x0400141E RID: 5150
		private const string NOT_OTHER = "-,!0123456789";

		// Token: 0x0400141F RID: 5151
		protected char[] text;

		// Token: 0x04001420 RID: 5152
		protected int ptr;

		// Token: 0x04001421 RID: 5153
		protected int number;

		// Token: 0x04001422 RID: 5154
		protected string other;

		// Token: 0x04001423 RID: 5155
		protected int low;

		// Token: 0x04001424 RID: 5156
		protected int high;

		// Token: 0x04001425 RID: 5157
		protected bool odd;

		// Token: 0x04001426 RID: 5158
		protected bool even;

		// Token: 0x04001427 RID: 5159
		protected bool inverse;
	}
}
