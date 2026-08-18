using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace iTextSharp.text.pdf.hyphenation
{
	// Token: 0x020004EA RID: 1258
	public class HyphenationTree : TernaryTree, IPatternConsumer
	{
		// Token: 0x06002B0A RID: 11018 RVA: 0x00105264 File Offset: 0x00104264
		public HyphenationTree()
		{
			this.stoplist = new Dictionary<string, List<object>>(23);
			this.classmap = new TernaryTree();
			this.vspace = new ByteVector();
			this.vspace.Alloc(1);
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x0010529C File Offset: 0x0010429C
		protected int PackValues(string values)
		{
			int length = values.Length;
			int num = ((length & 1) == 1) ? ((length >> 1) + 2) : ((length >> 1) + 1);
			int num2 = this.vspace.Alloc(num);
			byte[] arr = this.vspace.Arr;
			for (int i = 0; i < length; i++)
			{
				int num3 = i >> 1;
				byte b = (byte)(values[i] - '0' + '\u0001' & '\u000f');
				if ((i & 1) == 1)
				{
					arr[num3 + num2] = (arr[num3 + num2] | b);
				}
				else
				{
					arr[num3 + num2] = (byte)(b << 4);
				}
			}
			arr[num - 1 + num2] = 0;
			return num2;
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x00105330 File Offset: 0x00104330
		protected string UnpackValues(int k)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (byte b = this.vspace[k++]; b != 0; b = this.vspace[k++])
			{
				char c = (char)((b >> 4) - 1 + 48);
				stringBuilder.Append(c);
				c = (char)(b & 15);
				if (c == '\0')
				{
					break;
				}
				c = c - '\u0001' + '0';
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x001053A0 File Offset: 0x001043A0
		public void LoadSimplePatterns(Stream stream)
		{
			SimplePatternParser simplePatternParser = new SimplePatternParser();
			this.ivalues = new TernaryTree();
			simplePatternParser.Parse(stream, this);
			base.TrimToSize();
			this.vspace.TrimToSize();
			this.classmap.TrimToSize();
			this.ivalues = null;
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x001053EC File Offset: 0x001043EC
		public string FindPattern(string pat)
		{
			int num = base.Find(pat);
			if (num >= 0)
			{
				return this.UnpackValues(num);
			}
			return "";
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x00105412 File Offset: 0x00104412
		protected int Hstrcmp(char[] s, int si, char[] t, int ti)
		{
			while (s[si] == t[ti])
			{
				if (s[si] == '\0')
				{
					return 0;
				}
				si++;
				ti++;
			}
			if (t[ti] == '\0')
			{
				return 0;
			}
			return (int)(s[si] - t[ti]);
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x00105444 File Offset: 0x00104444
		protected byte[] GetValues(int k)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (byte b = this.vspace[k++]; b != 0; b = this.vspace[k++])
			{
				char c = (char)((b >> 4) - 1);
				stringBuilder.Append(c);
				c = (char)(b & 15);
				if (c == '\0')
				{
					break;
				}
				c -= '\u0001';
				stringBuilder.Append(c);
			}
			byte[] array = new byte[stringBuilder.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)stringBuilder[i];
			}
			return array;
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x001054D4 File Offset: 0x001044D4
		protected void SearchPatterns(char[] word, int index, byte[] il)
		{
			int num = index;
			char c = word[num];
			char c2 = this.root;
			while (c2 > '\0' && (int)c2 < this.sc.Length)
			{
				if (this.sc[(int)c2] == '￿')
				{
					if (this.Hstrcmp(word, num, this.kv.Arr, (int)this.lo[(int)c2]) == 0)
					{
						byte[] values = this.GetValues((int)this.eq[(int)c2]);
						int num2 = index;
						for (int i = 0; i < values.Length; i++)
						{
							if (num2 < il.Length && values[i] > il[num2])
							{
								il[num2] = values[i];
							}
							num2++;
						}
					}
					return;
				}
				int num3 = (int)(c - this.sc[(int)c2]);
				if (num3 == 0)
				{
					if (c == '\0')
					{
						return;
					}
					c = word[++num];
					c2 = this.eq[(int)c2];
					for (char c3 = c2; c3 > '\0'; c3 = this.lo[(int)c3])
					{
						if ((int)c3 >= this.sc.Length)
						{
							break;
						}
						if (this.sc[(int)c3] == '￿')
						{
							break;
						}
						if (this.sc[(int)c3] == '\0')
						{
							byte[] values = this.GetValues((int)this.eq[(int)c3]);
							int num4 = index;
							for (int j = 0; j < values.Length; j++)
							{
								if (num4 < il.Length && values[j] > il[num4])
								{
									il[num4] = values[j];
								}
								num4++;
							}
							break;
						}
					}
				}
				else
				{
					c2 = ((num3 < 0) ? this.lo[(int)c2] : this.hi[(int)c2]);
				}
			}
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x00105638 File Offset: 0x00104638
		public Hyphenation Hyphenate(string word, int remainCharCount, int pushCharCount)
		{
			char[] array = word.ToCharArray();
			return this.Hyphenate(array, 0, array.Length, remainCharCount, pushCharCount);
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x0010565C File Offset: 0x0010465C
		public Hyphenation Hyphenate(char[] w, int offset, int len, int remainCharCount, int pushCharCount)
		{
			char[] array = new char[len + 3];
			char[] array2 = new char[2];
			int num = 0;
			int num2 = len;
			bool flag = false;
			for (int i = 1; i <= len; i++)
			{
				array2[0] = w[offset + i - 1];
				int num3 = this.classmap.Find(array2, 0);
				if (num3 < 0)
				{
					if (i == 1 + num)
					{
						num++;
					}
					else
					{
						flag = true;
					}
					num2--;
				}
				else
				{
					if (flag)
					{
						return null;
					}
					array[i - num] = (char)num3;
				}
			}
			len = num2;
			if (len < remainCharCount + pushCharCount)
			{
				return null;
			}
			int[] array3 = new int[len + 1];
			int num4 = 0;
			string key = new string(array, 1, len);
			if (this.stoplist.ContainsKey(key))
			{
				List<object> list = this.stoplist[key];
				int num5 = 0;
				for (int i = 0; i < list.Count; i++)
				{
					object obj = list[i];
					if (obj is string)
					{
						num5 += ((string)obj).Length;
						if (num5 >= remainCharCount && num5 < len - pushCharCount)
						{
							array3[num4++] = num5 + num;
						}
					}
				}
			}
			else
			{
				array[0] = '.';
				array[len + 1] = '.';
				array[len + 2] = '\0';
				byte[] array4 = new byte[len + 3];
				for (int i = 0; i < len + 1; i++)
				{
					this.SearchPatterns(array, i, array4);
				}
				for (int i = 0; i < len; i++)
				{
					if ((array4[i + 1] & 1) == 1 && i >= remainCharCount && i <= len - pushCharCount)
					{
						array3[num4++] = i + num;
					}
				}
			}
			if (num4 > 0)
			{
				int[] array5 = new int[num4];
				Array.Copy(array3, 0, array5, 0, num4);
				return new Hyphenation(new string(w, offset, len), array5);
			}
			return null;
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x001057FC File Offset: 0x001047FC
		public void AddClass(string chargroup)
		{
			if (chargroup.Length > 0)
			{
				char val = chargroup[0];
				char[] array = new char[]
				{
					'\0',
					'\0'
				};
				for (int i = 0; i < chargroup.Length; i++)
				{
					array[0] = chargroup[i];
					this.classmap.Insert(array, 0, val);
				}
			}
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x0010584E File Offset: 0x0010484E
		public void AddException(string word, List<object> hyphenatedword)
		{
			this.stoplist[word] = hyphenatedword;
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x00105860 File Offset: 0x00104860
		public void AddPattern(string pattern, string ivalue)
		{
			int num = this.ivalues.Find(ivalue);
			if (num <= 0)
			{
				num = this.PackValues(ivalue);
				this.ivalues.Insert(ivalue, (char)num);
			}
			base.Insert(pattern, (char)num);
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x0010589D File Offset: 0x0010489D
		public override void PrintStats()
		{
			Console.WriteLine("Value space size = " + this.vspace.Length);
			base.PrintStats();
		}

		// Token: 0x04001DC4 RID: 7620
		protected ByteVector vspace;

		// Token: 0x04001DC5 RID: 7621
		protected Dictionary<string, List<object>> stoplist;

		// Token: 0x04001DC6 RID: 7622
		protected TernaryTree classmap;

		// Token: 0x04001DC7 RID: 7623
		private TernaryTree ivalues;
	}
}
