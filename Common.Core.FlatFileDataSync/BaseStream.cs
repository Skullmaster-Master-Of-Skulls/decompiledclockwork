using System;
using System.Collections;
using System.IO;
using System.Text;

namespace TechnoPro.Common.Core.FlatFileDataSync
{
	// Token: 0x02000004 RID: 4
	public class BaseStream
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002E0E File Offset: 0x0000100E
		public BaseStream(TextReader s, char colDelimiter, bool ignoreQuotes)
		{
			this.ignoreQuotes = ignoreQuotes;
			this.colDelimiter = colDelimiter;
			this.stream = s;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002E3C File Offset: 0x0000103C
		public virtual string[] GetNextRow()
		{
			ArrayList arrayList = new ArrayList();
			for (;;)
			{
				string nextItem = this.GetNextItem();
				if (nextItem == null)
				{
					break;
				}
				arrayList.Add(nextItem);
			}
			if (arrayList.Count != 0)
			{
				return (string[])arrayList.ToArray(typeof(string));
			}
			return null;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002E84 File Offset: 0x00001084
		private string GetNextItem()
		{
			if (this.EOL)
			{
				this.EOL = false;
				return null;
			}
			bool flag = false;
			bool flag2 = true;
			bool flag3 = false;
			StringBuilder stringBuilder = new StringBuilder();
			char nextChar;
			for (;;)
			{
				nextChar = this.GetNextChar(true);
				if (this.EOS)
				{
					break;
				}
				if ((flag3 || !flag) && nextChar == this.colDelimiter)
				{
					goto Block_5;
				}
				if ((flag2 || flag3 || !flag) && (nextChar == '\n' || nextChar == '\r'))
				{
					goto IL_6B;
				}
				if (!flag2 || nextChar != ' ')
				{
					if (!this.ignoreQuotes && flag2 && nextChar == '"')
					{
						flag = true;
						flag2 = false;
					}
					else if (flag2)
					{
						flag2 = false;
						stringBuilder.Append(nextChar);
					}
					else if (nextChar == '"' && flag)
					{
						if (this.GetNextChar(false) == '"')
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
			if (stringBuilder.Length <= 0)
			{
				return null;
			}
			return stringBuilder.ToString();
			Block_5:
			return stringBuilder.ToString();
			IL_6B:
			this.EOL = true;
			if (nextChar == '\r' && this.GetNextChar(false) == '\n')
			{
				this.GetNextChar(true);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002F98 File Offset: 0x00001198
		private char GetNextChar(bool eat)
		{
			if (this.pos >= this.length)
			{
				this.length = this.stream.ReadBlock(this.buffer, 0, this.buffer.Length);
				if (this.length == 0)
				{
					this.EOS = true;
					return '\0';
				}
				this.pos = 0;
			}
			if (eat)
			{
				char[] array = this.buffer;
				int num = this.pos;
				this.pos = num + 1;
				return array[num];
			}
			return this.buffer[this.pos];
		}

		// Token: 0x04000004 RID: 4
		private TextReader stream;

		// Token: 0x04000005 RID: 5
		private char colDelimiter;

		// Token: 0x04000006 RID: 6
		private bool ignoreQuotes;

		// Token: 0x04000007 RID: 7
		private bool EOS;

		// Token: 0x04000008 RID: 8
		private bool EOL;

		// Token: 0x04000009 RID: 9
		private char[] buffer = new char[4096];

		// Token: 0x0400000A RID: 10
		private int pos;

		// Token: 0x0400000B RID: 11
		private int length;
	}
}
