using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;

namespace a.b
{
	// Token: 0x02000311 RID: 785
	[DefaultMember("Item")]
	internal class f0
	{
		// Token: 0x06001C00 RID: 7168 RVA: 0x0007AE9D File Offset: 0x00079E9D
		public f0()
		{
			this.a = new Hashtable();
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x0007AEB0 File Offset: 0x00079EB0
		public string d(string A_0)
		{
			string result = (string)this.a[A_0];
			this.a.Remove(A_0);
			return result;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x0007AECF File Offset: 0x00079ECF
		public IEnumerator c()
		{
			return this.a.GetEnumerator();
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x0007AEDC File Offset: 0x00079EDC
		public bool c(string A_0)
		{
			return this.a.ContainsKey(A_0);
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x0007AEEA File Offset: 0x00079EEA
		public virtual void a(string A_0, string A_1)
		{
			this.a[A_0] = A_1;
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x0007AEFC File Offset: 0x00079EFC
		public void a(f0 A_0)
		{
			foreach (object obj in A_0.a())
			{
				string text = (string)obj;
				this.a[text] = A_0.e(text);
			}
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x0007AF64 File Offset: 0x00079F64
		public int b()
		{
			return this.a.Count;
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x0007AF71 File Offset: 0x00079F71
		public virtual string e(string A_0)
		{
			return (string)this.a[A_0];
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x0007AF84 File Offset: 0x00079F84
		public virtual void b(string A_0, string A_1)
		{
			this.a[A_0] = A_1;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x0007AF93 File Offset: 0x00079F93
		public ICollection a()
		{
			return this.a.Keys;
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x0007AFA0 File Offset: 0x00079FA0
		public void d()
		{
			this.a.Clear();
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x0007AFB0 File Offset: 0x00079FB0
		public void a(Stream A_0)
		{
			StreamReader streamReader = new StreamReader(A_0, Encoding.GetEncoding(1252));
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
							while (this.a(text))
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
							string a_ = text.Substring(num, i - num);
							string text3 = (i < length) ? text.Substring(num3, length - num3) : "";
							a_ = this.b(a_);
							text3 = this.b(text3);
							this.a(a_, text3);
						}
					}
				}
			}
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x0007B188 File Offset: 0x0007A188
		private string b(string A_0)
		{
			int length = A_0.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			int i = 0;
			while (i < length)
			{
				char c = A_0[i++];
				if (c == '\\')
				{
					c = A_0[i++];
					if (c == 'u')
					{
						int num = 0;
						int j = 0;
						while (j < 4)
						{
							c = A_0[i++];
							switch (c)
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
								goto IL_109;
							case 'A':
							case 'B':
							case 'C':
							case 'D':
							case 'E':
							case 'F':
								num = (num << 4) + 10 + (int)c - 65;
								break;
							default:
								switch (c)
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
									goto IL_109;
								}
								break;
							}
							j++;
							continue;
							IL_109:
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

		// Token: 0x06001C0D RID: 7181 RVA: 0x0007B308 File Offset: 0x0007A308
		private bool a(string A_0)
		{
			int num = 0;
			int num2 = A_0.Length - 1;
			while (num2 >= 0 && A_0[num2--] == '\\')
			{
				num++;
			}
			return num % 2 == 1;
		}

		// Token: 0x04001348 RID: 4936
		private Hashtable a;

		// Token: 0x04001349 RID: 4937
		private const string b = " \t\r\n\f";

		// Token: 0x0400134A RID: 4938
		private const string c = "=: \t\r\n\f";

		// Token: 0x0400134B RID: 4939
		private const string d = "=:";
	}
}
