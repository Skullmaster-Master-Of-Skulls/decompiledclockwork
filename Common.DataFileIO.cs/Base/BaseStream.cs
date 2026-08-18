using System;
using System.Collections;
using System.IO;
using System.Text;

namespace TechnoPro.Common.DataFileIO.cs.Base
{
	// Token: 0x0200000D RID: 13
	public class BaseStream
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00004608 File Offset: 0x00002808
		public BaseStream(TextReader s, char colDelimiter, bool ignoreQuotes)
		{
			this.ignoreQuotes = ignoreQuotes;
			this.colDelimiter = colDelimiter;
			this.stream = s;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00004660 File Offset: 0x00002860
		public virtual string[] GetNextRow()
		{
			ArrayList arrayList = new ArrayList();
			for (;;)
			{
				string nextItem = this.GetNextItem();
				bool flag = nextItem == null;
				if (flag)
				{
					break;
				}
				arrayList.Add(nextItem);
			}
			return (arrayList.Count == 0) ? null : ((string[])arrayList.ToArray(typeof(string)));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000046B8 File Offset: 0x000028B8
		private string GetNextItem()
		{
			bool eol = this.EOL;
			string result;
			if (eol)
			{
				this.EOL = false;
				result = null;
			}
			else
			{
				bool flag = false;
				bool flag2 = true;
				bool flag3 = false;
				StringBuilder stringBuilder = new StringBuilder();
				char nextChar;
				for (;;)
				{
					nextChar = this.GetNextChar(true);
					bool eos = this.EOS;
					if (eos)
					{
						break;
					}
					bool flag4 = (flag3 || !flag) && nextChar == this.colDelimiter;
					if (flag4)
					{
						goto Block_5;
					}
					bool flag5 = (flag2 || flag3 || !flag) && (nextChar == '\n' || nextChar == '\r');
					if (flag5)
					{
						goto Block_8;
					}
					bool flag6 = flag2 && nextChar == ' ';
					if (!flag6)
					{
						bool flag7 = !this.ignoreQuotes && flag2 && nextChar == '"';
						if (flag7)
						{
							flag = true;
							flag2 = false;
						}
						else
						{
							bool flag8 = flag2;
							if (flag8)
							{
								flag2 = false;
								stringBuilder.Append(nextChar);
							}
							else
							{
								bool flag9 = nextChar == '"' && flag;
								if (flag9)
								{
									bool flag10 = this.GetNextChar(false) == '"';
									if (flag10)
									{
										stringBuilder.Append(this.GetNextChar(true));
									}
									else
									{
										flag3 = true;
									}
								}
								else
								{
									stringBuilder.Append(nextChar);
								}
							}
						}
					}
				}
				return (stringBuilder.Length > 0) ? stringBuilder.ToString() : null;
				Block_5:
				return stringBuilder.ToString();
				Block_8:
				this.EOL = true;
				bool flag11 = nextChar == '\r' && this.GetNextChar(false) == '\n';
				if (flag11)
				{
					this.GetNextChar(true);
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00004834 File Offset: 0x00002A34
		private char GetNextChar(bool eat)
		{
			bool flag = this.pos >= this.length;
			if (flag)
			{
				this.length = this.stream.ReadBlock(this.buffer, 0, this.buffer.Length);
				bool flag2 = this.length == 0;
				if (flag2)
				{
					this.EOS = true;
					return '\0';
				}
				this.pos = 0;
			}
			char result;
			if (eat)
			{
				char[] array = this.buffer;
				int num = this.pos;
				this.pos = num + 1;
				result = array[num];
			}
			else
			{
				result = this.buffer[this.pos];
			}
			return result;
		}

		// Token: 0x04000007 RID: 7
		private TextReader stream;

		// Token: 0x04000008 RID: 8
		private char colDelimiter;

		// Token: 0x04000009 RID: 9
		private bool ignoreQuotes;

		// Token: 0x0400000A RID: 10
		private bool EOS = false;

		// Token: 0x0400000B RID: 11
		private bool EOL = false;

		// Token: 0x0400000C RID: 12
		private char[] buffer = new char[4096];

		// Token: 0x0400000D RID: 13
		private int pos = 0;

		// Token: 0x0400000E RID: 14
		private int length = 0;
	}
}
