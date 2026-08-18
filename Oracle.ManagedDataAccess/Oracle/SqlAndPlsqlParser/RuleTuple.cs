using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x0200027F RID: 639
	[Serializable]
	internal class RuleTuple : RuleTupleBase<string>, IStreamable
	{
		// Token: 0x06001911 RID: 6417 RVA: 0x00107E8C File Offset: 0x0010608C
		static RuleTuple()
		{
			RuleTupleBase<string>.s_vNullHeadHashCode = string.Empty.GetHashCode();
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00107EA0 File Offset: 0x001060A0
		public RuleTuple(string h, List<string> r) : base(h, r)
		{
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00107EAC File Offset: 0x001060AC
		public RuleTuple(string h, string[] r) : base(h, r)
		{
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x00107EB8 File Offset: 0x001060B8
		public RuleTuple()
		{
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00107EC0 File Offset: 0x001060C0
		public override int CompareTo(RuleTupleBase<string> src)
		{
			RuleTuple ruleTuple = src as RuleTuple;
			if (ruleTuple == null)
			{
				return 1;
			}
			int num = string.Compare(this.m_vHead, ruleTuple.m_vHead);
			if (num == 0)
			{
				string[] vRhs = ruleTuple.m_vRhs;
				if (this.m_vRhs == null)
				{
					if (vRhs != null)
					{
						num = -1;
					}
				}
				else if (vRhs == null)
				{
					num = 1;
				}
				else
				{
					int num2 = this.m_vRhs.Length;
					num = num2 - vRhs.Length;
					int num3 = 0;
					while (num == 0 && num3 < num2)
					{
						num = string.Compare(this.m_vRhs[num3], vRhs[num3]);
						num3++;
					}
				}
			}
			return num;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00107F44 File Offset: 0x00106144
		public bool IgnoreRule(string keyword, string bra, string ket)
		{
			bool flag = false;
			string[] vRhs = this.m_vRhs;
			int i = 0;
			while (i < vRhs.Length)
			{
				string a = vRhs[i];
				if (flag)
				{
					if (a == keyword)
					{
						return true;
					}
				}
				else
				{
					flag = (a == bra);
				}
				if (!(a == ket))
				{
					i++;
					continue;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00107F9C File Offset: 0x0010619C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			if (this.m_vHead != null)
			{
				stringBuilder.Append(this.m_vHead);
				stringBuilder.Append(':');
			}
			foreach (string value in this.m_vRhs)
			{
				stringBuilder.Append(' ');
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00108004 File Offset: 0x00106204
		public virtual string ToHTML(RuleTuple predecessor)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			if (predecessor == null || predecessor.m_vHead != this.m_vHead)
			{
				stringBuilder.Append(this.m_vHead);
				stringBuilder.Append(':');
				Service.IndentLine(12 - this.m_vHead.Length - 2, " ", ref stringBuilder);
			}
			else
			{
				Service.IndentLine(10, "| ", ref stringBuilder);
			}
			foreach (string text in this.m_vRhs)
			{
				stringBuilder.Append(' ');
				if (text.StartsWith("'"))
				{
					stringBuilder.Append('"');
					stringBuilder.Append(text.Substring(1, text.Length - 2));
					stringBuilder.Append('"');
				}
				else
				{
					stringBuilder.Append(text);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x001080DC File Offset: 0x001062DC
		public virtual int WriteToStream(OutputStream ostrm)
		{
			ostrm.WriteLine(this.ToString());
			return 0;
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x001080EC File Offset: 0x001062EC
		public virtual int ReadFromStream(InputStream istrm)
		{
			return this.ReadFromString(istrm.ReadLine().Trim());
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x00108100 File Offset: 0x00106300
		public virtual int ReadFromString(string str)
		{
			string[] array = str.Split(RuleTupleBase<string>.s_vColonDelimiter, 2);
			this.m_vHead = array[0];
			if (array.Length > 1)
			{
				this.m_vRhs = array[1].Split(RuleTupleBase<string>.s_vSpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);
			}
			else
			{
				this.m_vRhs = RuleTupleBase<string>.s_vEmptyStringArray;
			}
			this.m_vHashValue = null;
			return 0;
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x00108158 File Offset: 0x00106358
		public static void WriteRules(StreamableSet<RuleTuple> rules, OutputStream ostr)
		{
			ostr.WriteObjectData(rules);
			ostr.Close();
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x00108168 File Offset: 0x00106368
		public static StreamableSet<RuleTuple> ReadRules(InputStream istr)
		{
			StreamableSet<RuleTuple> result = istr.ReadObjectData<StreamableSet<RuleTuple>>();
			istr.Close();
			return result;
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x00108184 File Offset: 0x00106384
		public static void PrintRules(StreamableSet<RuleTuple> rules)
		{
			RuleTuple predecessor = null;
			foreach (RuleTuple ruleTuple in rules)
			{
				Console.WriteLine(ruleTuple.ToHTML(predecessor));
				predecessor = ruleTuple;
			}
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x001081DC File Offset: 0x001063DC
		public static Set<RuleTuple> ReadUnifiedRules(InputStream istrm)
		{
			Set<RuleTuple> set = new Set<RuleTuple>();
			while (!istrm.EndOfStream)
			{
				string text = istrm.ReadLine().Trim();
				if (!text.StartsWith("#") && text.Length != 0)
				{
					RuleTuple ruleTuple = new RuleTuple();
					ruleTuple.ReadFromString(text);
					set.Add(ruleTuple);
				}
			}
			istrm.Close();
			return set;
		}

		// Token: 0x02000280 RID: 640
		public class RuleTupleHeadComparer : IComparer<RuleTuple>
		{
			// Token: 0x06001920 RID: 6432 RVA: 0x00108238 File Offset: 0x00106438
			private RuleTupleHeadComparer()
			{
			}

			// Token: 0x06001921 RID: 6433 RVA: 0x00108240 File Offset: 0x00106440
			public int Compare(RuleTuple x, RuleTuple y)
			{
				return string.Compare(x.m_vHead, y.m_vHead);
			}

			// Token: 0x04001B75 RID: 7029
			public static RuleTuple.RuleTupleHeadComparer s_vInstance = new RuleTuple.RuleTupleHeadComparer();
		}
	}
}
