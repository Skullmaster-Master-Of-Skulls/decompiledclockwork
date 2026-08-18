using System;
using System.Collections;
using System.IO;

namespace iTextSharp.text.pdf.hyphenation
{
	// Token: 0x020004E9 RID: 1257
	public class Hyphenator
	{
		// Token: 0x06002AFF RID: 11007 RVA: 0x0010506D File Offset: 0x0010406D
		public Hyphenator(string lang, string country, int leftMin, int rightMin)
		{
			this.hyphenTree = Hyphenator.GetHyphenationTree(lang, country);
			this.remainCharCount = leftMin;
			this.pushCharCount = rightMin;
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x001050A0 File Offset: 0x001040A0
		public static HyphenationTree GetHyphenationTree(string lang, string country)
		{
			string text = lang;
			if (country != null && !country.Equals("none"))
			{
				text = text + "_" + country;
			}
			if (Hyphenator.hyphenTrees.ContainsKey(text))
			{
				return (HyphenationTree)Hyphenator.hyphenTrees[text];
			}
			if (Hyphenator.hyphenTrees.ContainsKey(lang))
			{
				return (HyphenationTree)Hyphenator.hyphenTrees[lang];
			}
			HyphenationTree resourceHyphenationTree = Hyphenator.GetResourceHyphenationTree(text);
			if (resourceHyphenationTree != null)
			{
				Hyphenator.hyphenTrees[text] = resourceHyphenationTree;
			}
			return resourceHyphenationTree;
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x00105120 File Offset: 0x00104120
		public static HyphenationTree GetResourceHyphenationTree(string key)
		{
			HyphenationTree result;
			try
			{
				Stream resourceStream = BaseFont.GetResourceStream("iTextSharp.text.pdf.hyphenation.hyph." + key + ".xml");
				if (resourceStream == null && key.Length > 2)
				{
					resourceStream = BaseFont.GetResourceStream("iTextSharp.text.pdf.hyphenation.hyph." + key.Substring(0, 2) + ".xml");
				}
				if (resourceStream == null)
				{
					result = null;
				}
				else
				{
					HyphenationTree hyphenationTree = new HyphenationTree();
					hyphenationTree.LoadSimplePatterns(resourceStream);
					result = hyphenationTree;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x0010519C File Offset: 0x0010419C
		public static Hyphenation Hyphenate(string lang, string country, string word, int leftMin, int rightMin)
		{
			HyphenationTree hyphenationTree = Hyphenator.GetHyphenationTree(lang, country);
			if (hyphenationTree == null)
			{
				return null;
			}
			return hyphenationTree.Hyphenate(word, leftMin, rightMin);
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x001051C0 File Offset: 0x001041C0
		public static Hyphenation Hyphenate(string lang, string country, char[] word, int offset, int len, int leftMin, int rightMin)
		{
			HyphenationTree hyphenationTree = Hyphenator.GetHyphenationTree(lang, country);
			if (hyphenationTree == null)
			{
				return null;
			}
			return hyphenationTree.Hyphenate(word, offset, len, leftMin, rightMin);
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x001051E8 File Offset: 0x001041E8
		public void SetMinRemainCharCount(int min)
		{
			this.remainCharCount = min;
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x001051F1 File Offset: 0x001041F1
		public void SetMinPushCharCount(int min)
		{
			this.pushCharCount = min;
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x001051FA File Offset: 0x001041FA
		public void SetLanguage(string lang, string country)
		{
			this.hyphenTree = Hyphenator.GetHyphenationTree(lang, country);
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x00105209 File Offset: 0x00104209
		public Hyphenation Hyphenate(char[] word, int offset, int len)
		{
			if (this.hyphenTree == null)
			{
				return null;
			}
			return this.hyphenTree.Hyphenate(word, offset, len, this.remainCharCount, this.pushCharCount);
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x0010522F File Offset: 0x0010422F
		public Hyphenation Hyphenate(string word)
		{
			if (this.hyphenTree == null)
			{
				return null;
			}
			return this.hyphenTree.Hyphenate(word, this.remainCharCount, this.pushCharCount);
		}

		// Token: 0x04001DBF RID: 7615
		private const string defaultHyphLocation = "iTextSharp.text.pdf.hyphenation.hyph.";

		// Token: 0x04001DC0 RID: 7616
		private static Hashtable hyphenTrees = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04001DC1 RID: 7617
		private HyphenationTree hyphenTree;

		// Token: 0x04001DC2 RID: 7618
		private int remainCharCount = 2;

		// Token: 0x04001DC3 RID: 7619
		private int pushCharCount = 2;
	}
}
