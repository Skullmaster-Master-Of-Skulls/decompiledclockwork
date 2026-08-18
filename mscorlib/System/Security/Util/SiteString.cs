using System;
using System.Collections;
using System.Globalization;

namespace System.Security.Util
{
	// Token: 0x02000486 RID: 1158
	[Serializable]
	internal class SiteString
	{
		// Token: 0x06002DDB RID: 11739 RVA: 0x00099394 File Offset: 0x00098394
		protected internal SiteString()
		{
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x0009939C File Offset: 0x0009839C
		public SiteString(string site)
		{
			this.m_separatedSite = SiteString.CreateSeparatedSite(site);
			this.m_site = site;
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x000993B7 File Offset: 0x000983B7
		private SiteString(string site, ArrayList separatedSite)
		{
			this.m_separatedSite = separatedSite;
			this.m_site = site;
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x000993D0 File Offset: 0x000983D0
		private static ArrayList CreateSeparatedSite(string site)
		{
			ArrayList arrayList = new ArrayList();
			if (site == null || site.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
			}
			int num = -1;
			int num2 = site.IndexOf('[');
			if (num2 == 0)
			{
				num = site.IndexOf(']', num2 + 1);
			}
			if (num != -1)
			{
				string value = site.Substring(num2 + 1, num - num2 - 1);
				arrayList.Add(value);
				return arrayList;
			}
			string[] array = site.Split(SiteString.m_separators);
			for (int i = array.Length - 1; i > -1; i--)
			{
				if (array[i] == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
				}
				if (array[i].Equals(""))
				{
					if (i != array.Length - 1)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
					}
				}
				else if (array[i].Equals("*"))
				{
					if (i != 0)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
					}
					arrayList.Add(array[i]);
				}
				else
				{
					if (!SiteString.AllLegalCharacters(array[i]))
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
					}
					arrayList.Add(array[i]);
				}
			}
			return arrayList;
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x00099500 File Offset: 0x00098500
		private static bool AllLegalCharacters(string str)
		{
			foreach (char c in str)
			{
				if (!SiteString.IsLegalDNSChar(c) && !SiteString.IsNetbiosSplChar(c))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x00099539 File Offset: 0x00098539
		private static bool IsLegalDNSChar(char c)
		{
			return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '-';
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x00099564 File Offset: 0x00098564
		private static bool IsNetbiosSplChar(char c)
		{
			if (c <= '@')
			{
				switch (c)
				{
				case '!':
				case '#':
				case '$':
				case '%':
				case '&':
				case '\'':
				case '(':
				case ')':
				case '-':
				case '.':
					break;
				case '"':
				case '*':
				case '+':
				case ',':
					return false;
				default:
					if (c != '@')
					{
						return false;
					}
					break;
				}
			}
			else
			{
				switch (c)
				{
				case '^':
				case '_':
					break;
				default:
					switch (c)
					{
					case '{':
					case '}':
					case '~':
						break;
					case '|':
						return false;
					default:
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000995EF File Offset: 0x000985EF
		public override string ToString()
		{
			return this.m_site;
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x000995F7 File Offset: 0x000985F7
		public override bool Equals(object o)
		{
			return o != null && o is SiteString && this.Equals((SiteString)o, true);
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x00099614 File Offset: 0x00098614
		public override int GetHashCode()
		{
			TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
			return textInfo.GetCaseInsensitiveHashCode(this.m_site);
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x00099638 File Offset: 0x00098638
		internal bool Equals(SiteString ss, bool ignoreCase)
		{
			if (this.m_site == null)
			{
				return ss.m_site == null;
			}
			return ss.m_site != null && this.IsSubsetOf(ss, ignoreCase) && ss.IsSubsetOf(this, ignoreCase);
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x0009966A File Offset: 0x0009866A
		public virtual SiteString Copy()
		{
			return new SiteString(this.m_site, this.m_separatedSite);
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x0009967D File Offset: 0x0009867D
		public virtual bool IsSubsetOf(SiteString operand)
		{
			return this.IsSubsetOf(operand, true);
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x00099688 File Offset: 0x00098688
		public virtual bool IsSubsetOf(SiteString operand, bool ignoreCase)
		{
			StringComparison comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			if (operand == null)
			{
				return false;
			}
			if (this.m_separatedSite.Count == operand.m_separatedSite.Count && this.m_separatedSite.Count == 0)
			{
				return true;
			}
			if (this.m_separatedSite.Count < operand.m_separatedSite.Count - 1)
			{
				return false;
			}
			if (this.m_separatedSite.Count > operand.m_separatedSite.Count && operand.m_separatedSite.Count > 0 && !operand.m_separatedSite[operand.m_separatedSite.Count - 1].Equals("*"))
			{
				return false;
			}
			if (string.Compare(this.m_site, operand.m_site, comparisonType) == 0)
			{
				return true;
			}
			for (int i = 0; i < operand.m_separatedSite.Count - 1; i++)
			{
				if (string.Compare((string)this.m_separatedSite[i], (string)operand.m_separatedSite[i], comparisonType) != 0)
				{
					return false;
				}
			}
			if (this.m_separatedSite.Count < operand.m_separatedSite.Count)
			{
				return operand.m_separatedSite[operand.m_separatedSite.Count - 1].Equals("*");
			}
			return this.m_separatedSite.Count != operand.m_separatedSite.Count || string.Compare((string)this.m_separatedSite[this.m_separatedSite.Count - 1], (string)operand.m_separatedSite[this.m_separatedSite.Count - 1], comparisonType) == 0 || operand.m_separatedSite[operand.m_separatedSite.Count - 1].Equals("*");
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x00099846 File Offset: 0x00098846
		public virtual SiteString Intersect(SiteString operand)
		{
			if (operand == null)
			{
				return null;
			}
			if (this.IsSubsetOf(operand))
			{
				return this.Copy();
			}
			if (operand.IsSubsetOf(this))
			{
				return operand.Copy();
			}
			return null;
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x0009986E File Offset: 0x0009886E
		public virtual SiteString Union(SiteString operand)
		{
			if (operand == null)
			{
				return this;
			}
			if (this.IsSubsetOf(operand))
			{
				return operand.Copy();
			}
			if (operand.IsSubsetOf(this))
			{
				return this.Copy();
			}
			return null;
		}

		// Token: 0x040017A4 RID: 6052
		protected string m_site;

		// Token: 0x040017A5 RID: 6053
		protected ArrayList m_separatedSite;

		// Token: 0x040017A6 RID: 6054
		protected static char[] m_separators = new char[]
		{
			'.'
		};
	}
}
