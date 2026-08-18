using System;
using System.Collections;
using System.IO;
using System.Text;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x02000014 RID: 20
	public class CsvStream
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00024128 File Offset: 0x00022328
		public CsvStream(TextReader s)
		{
			this.stream = s;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00024168 File Offset: 0x00022368
		public string[] GetNextRow()
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

		// Token: 0x06000169 RID: 361 RVA: 0x000241C0 File Offset: 0x000223C0
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
					bool flag4 = (flag3 || !flag) && nextChar == ',';
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
						bool flag7 = flag2 && nextChar == '"';
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

		// Token: 0x0600016A RID: 362 RVA: 0x00024330 File Offset: 0x00022530
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

		// Token: 0x04000055 RID: 85
		private TextReader stream;

		// Token: 0x04000056 RID: 86
		private bool EOS = false;

		// Token: 0x04000057 RID: 87
		private bool EOL = false;

		// Token: 0x04000058 RID: 88
		private char[] buffer = new char[4096];

		// Token: 0x04000059 RID: 89
		private int pos = 0;

		// Token: 0x0400005A RID: 90
		private int length = 0;
	}
}
