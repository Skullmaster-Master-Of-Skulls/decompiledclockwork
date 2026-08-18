using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace System.util
{
	// Token: 0x020000BB RID: 187
	public class Properties
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x0001E0B9 File Offset: 0x0001D0B9
		public Properties()
		{
			this._col = new Dictionary<string, string>();
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001E0CC File Offset: 0x0001D0CC
		public string Remove(string key)
		{
			string result;
			this._col.TryGetValue(key, out result);
			this._col.Remove(key);
			return result;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001E0F6 File Offset: 0x0001D0F6
		public Dictionary<string, string>.Enumerator GetEnumerator()
		{
			return this._col.GetEnumerator();
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001E103 File Offset: 0x0001D103
		public bool ContainsKey(string key)
		{
			return this._col.ContainsKey(key);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001E111 File Offset: 0x0001D111
		public virtual void Add(string key, string value)
		{
			this._col[key] = value;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001E120 File Offset: 0x0001D120
		public void AddAll(Properties col)
		{
			foreach (string key in col.Keys)
			{
				this._col[key] = col[key];
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0001E180 File Offset: 0x0001D180
		public int Count
		{
			get
			{
				return this._col.Count;
			}
		}

		// Token: 0x1700010D RID: 269
		public virtual string this[string key]
		{
			get
			{
				string result;
				this._col.TryGetValue(key, out result);
				return result;
			}
			set
			{
				this._col[key] = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0001E1BC File Offset: 0x0001D1BC
		public Dictionary<string, string>.KeyCollection Keys
		{
			get
			{
				return this._col.Keys;
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001E1C9 File Offset: 0x0001D1C9
		public void Clear()
		{
			this._col.Clear();
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001E1D8 File Offset: 0x0001D1D8
		public void Load(Stream inStream)
		{
			StreamReader streamReader = new StreamReader(inStream, Encoding.GetEncoding(1252));
			for (;;)
			{
				string text = streamReader.ReadLine();
				if (text == null)
				{
					break;
				}
				if (text.Length > 0)
				{
					int length = text.Length;
					int num = 0;
					while (num < length && " \t\r\n\f".IndexOf(text[num]) != -1)
					{
						num++;
					}
					if (num != length)
					{
						char c = text[num];
						if (c != '#' && c != '!')
						{
							while (this.ContinueLine(text))
							{
								string text2 = streamReader.ReadLine();
								if (text2 == null)
								{
									text2 = "";
								}
								string str = text.Substring(0, length - 1);
								int num2 = 0;
								while (num2 < text2.Length && " \t\r\n\f".IndexOf(text2[num2]) != -1)
								{
									num2++;
								}
								text2 = text2.Substring(num2, text2.Length - num2);
								text = str + text2;
								length = text.Length;
							}
							int i;
							for (i = num; i < length; i++)
							{
								char c2 = text[i];
								if (c2 == '\\')
								{
									i++;
								}
								else if ("=: \t\r\n\f".IndexOf(c2) != -1)
								{
									break;
								}
							}
							int num3 = i;
							while (num3 < length && " \t\r\n\f".IndexOf(text[num3]) != -1)
							{
								num3++;
							}
							if (num3 < length && "=:".IndexOf(text[num3]) != -1)
							{
								num3++;
							}
							while (num3 < length && " \t\r\n\f".IndexOf(text[num3]) != -1)
							{
								num3++;
							}
							string text3 = text.Substring(num, i - num);
							string text4 = (i < length) ? text.Substring(num3, length - num3) : "";
							text3 = this.LoadConvert(text3);
							text4 = this.LoadConvert(text4);
							this.Add(text3, text4);
						}
					}
				}
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001E3B0 File Offset: 0x0001D3B0
		private string LoadConvert(string theString)
		{
			int length = theString.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			int i = 0;
			while (i < length)
			{
				char c = theString[i++];
				if (c == '\\')
				{
					c = theString[i++];
					if (c == 'u')
					{
						int num = 0;
						int j = 0;
						while (j < 4)
						{
							c = theString[i++];
							char c2 = c;
							switch (c2)
							{
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								num = (num << 4) + (int)c - 48;
								break;
							case ':':
							case ';':
							case '<':
							case '=':
							case '>':
							case '?':
							case '@':
								goto IL_10E;
							case 'A':
							case 'B':
							case 'C':
							case 'D':
							case 'E':
							case 'F':
								num = (num << 4) + 10 + (int)c - 65;
								break;
							default:
								switch (c2)
								{
								case 'a':
								case 'b':
								case 'c':
								case 'd':
								case 'e':
								case 'f':
									num = (num << 4) + 10 + (int)c - 97;
									break;
								default:
									goto IL_10E;
								}
								break;
							}
							j++;
							continue;
							IL_10E:
							throw new ArgumentException("Malformed \\uxxxx encoding.");
						}
						stringBuilder.Append((char)num);
					}
					else
					{
						if (c == 't')
						{
							c = '\t';
						}
						else if (c == 'r')
						{
							c = '\r';
						}
						else if (c == 'n')
						{
							c = '\n';
						}
						else if (c == 'f')
						{
							c = '\f';
						}
						stringBuilder.Append(c);
					}
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001E538 File Offset: 0x0001D538
		private bool ContinueLine(string line)
		{
			int num = 0;
			int num2 = line.Length - 1;
			while (num2 >= 0 && line[num2--] == '\\')
			{
				num++;
			}
			return num % 2 == 1;
		}

		// Token: 0x040002CD RID: 717
		private const string whiteSpaceChars = " \t\r\n\f";

		// Token: 0x040002CE RID: 718
		private const string keyValueSeparators = "=: \t\r\n\f";

		// Token: 0x040002CF RID: 719
		private const string strictKeyValueSeparators = "=:";

		// Token: 0x040002D0 RID: 720
		private Dictionary<string, string> _col;
	}
}
