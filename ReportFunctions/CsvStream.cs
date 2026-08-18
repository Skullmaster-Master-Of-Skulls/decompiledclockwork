using System;
using System.Collections;
using System.IO;
using System.Text;

// Token: 0x0200001E RID: 30
public class CsvStream
{
	// Token: 0x06000252 RID: 594 RVA: 0x00038BAA File Offset: 0x00037BAA
	public CsvStream(TextReader s)
	{
		this.stream = s;
	}

	// Token: 0x06000253 RID: 595 RVA: 0x00038BE8 File Offset: 0x00037BE8
	public string[] GetNextRow()
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
		return (arrayList.Count == 0) ? null : ((string[])arrayList.ToArray(typeof(string)));
	}

	// Token: 0x06000254 RID: 596 RVA: 0x00038C44 File Offset: 0x00037C44
	private string GetNextItem()
	{
		string result;
		if (this.EOL)
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
				if (this.EOS)
				{
					break;
				}
				if ((flag3 || !flag) && nextChar == ',')
				{
					goto Block_5;
				}
				if ((flag2 || flag3 || !flag) && (nextChar == '\n' || nextChar == '\r'))
				{
					goto Block_9;
				}
				if (!flag2 || nextChar != ' ')
				{
					if (flag2 && nextChar == '"')
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
			return (stringBuilder.Length > 0) ? stringBuilder.ToString() : null;
			Block_5:
			return stringBuilder.ToString();
			Block_9:
			this.EOL = true;
			if (nextChar == '\r' && this.GetNextChar(false) == '\n')
			{
				this.GetNextChar(true);
			}
			result = stringBuilder.ToString();
		}
		return result;
	}

	// Token: 0x06000255 RID: 597 RVA: 0x00038DD4 File Offset: 0x00037DD4
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
		char result;
		if (eat)
		{
			result = this.buffer[this.pos++];
		}
		else
		{
			result = this.buffer[this.pos];
		}
		return result;
	}

	// Token: 0x04000116 RID: 278
	private TextReader stream;

	// Token: 0x04000117 RID: 279
	private bool EOS = false;

	// Token: 0x04000118 RID: 280
	private bool EOL = false;

	// Token: 0x04000119 RID: 281
	private char[] buffer = new char[4096];

	// Token: 0x0400011A RID: 282
	private int pos = 0;

	// Token: 0x0400011B RID: 283
	private int length = 0;
}
