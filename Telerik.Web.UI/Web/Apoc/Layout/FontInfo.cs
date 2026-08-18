using System;
using System.Collections;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015E8 RID: 5608
	internal class FontInfo
	{
		// Token: 0x0600DA7C RID: 55932 RVA: 0x002FDA21 File Offset: 0x002FBC21
		public FontInfo()
		{
			this.triplets = new Hashtable();
			this.fonts = new Hashtable();
			this.usedFonts = new Hashtable();
		}

		// Token: 0x0600DA7D RID: 55933 RVA: 0x002FDA4C File Offset: 0x002FBC4C
		public void AddFontProperties(string name, string family, string style, string weight)
		{
			string key = FontInfo.CreateFontKey(family, style, weight);
			this.triplets.Add(key, name);
		}

		// Token: 0x0600DA7E RID: 55934 RVA: 0x002FDA70 File Offset: 0x002FBC70
		public void AddMetrics(string name, IFontMetric metrics)
		{
			this.fonts.Add(name, metrics);
		}

		// Token: 0x0600DA7F RID: 55935 RVA: 0x002FDA7F File Offset: 0x002FBC7F
		public string FontLookup(string family, string style, string weight)
		{
			return this.FontLookup(FontInfo.CreateFontKey(family, style, weight));
		}

		// Token: 0x0600DA80 RID: 55936 RVA: 0x002FDA90 File Offset: 0x002FBC90
		private string FontLookup(string key)
		{
			string text = (string)this.triplets[key];
			if (text == null)
			{
				int startIndex = key.IndexOf(',');
				string key2 = "any" + key.Substring(startIndex);
				text = (string)this.triplets[key2];
				if (text == null)
				{
					text = (string)this.triplets["any,normal,normal"];
					if (text == null)
					{
						throw new ApocException("no default font defined by OutputConverter");
					}
					ApocDriver.ActiveDriver.FireApocInfo("Defaulted font to any,normal,normal");
				}
				ApocDriver.ActiveDriver.FireApocWarning("Unknown font " + key + " so defaulted font to any");
			}
			this.usedFonts[text] = this.fonts[text];
			return text;
		}

		// Token: 0x0600DA81 RID: 55937 RVA: 0x002FDB48 File Offset: 0x002FBD48
		private bool HasFont(string family, string style, string weight)
		{
			string key = FontInfo.CreateFontKey(family, style, weight);
			return this.triplets.ContainsKey(key);
		}

		// Token: 0x0600DA82 RID: 55938 RVA: 0x002FDB6C File Offset: 0x002FBD6C
		public static string CreateFontKey(string family, string style, string weight)
		{
			int num;
			try
			{
				if (weight != null && weight.Length > 0 && char.IsNumber(weight, 0))
				{
					num = int.Parse(weight);
				}
				else
				{
					num = 0;
				}
			}
			catch (Exception)
			{
				num = 0;
			}
			if (num > 600)
			{
				weight = "bold";
			}
			else if (num > 0)
			{
				weight = "normal";
			}
			return string.Format("{0},{1},{2}", family, style, weight);
		}

		// Token: 0x0600DA83 RID: 55939 RVA: 0x002FDBDC File Offset: 0x002FBDDC
		public IDictionary GetFonts()
		{
			return this.fonts;
		}

		// Token: 0x0600DA84 RID: 55940 RVA: 0x002FDBE4 File Offset: 0x002FBDE4
		public IFontMetric GetFontByName(string fontName)
		{
			return (IFontMetric)this.fonts[fontName];
		}

		// Token: 0x0600DA85 RID: 55941 RVA: 0x002FDBF7 File Offset: 0x002FBDF7
		public Hashtable GetUsedFonts()
		{
			return this.usedFonts;
		}

		// Token: 0x0600DA86 RID: 55942 RVA: 0x002FDBFF File Offset: 0x002FBDFF
		public IFontMetric GetMetricsFor(string fontName)
		{
			this.usedFonts[fontName] = this.fonts[fontName];
			return (IFontMetric)this.fonts[fontName];
		}

		// Token: 0x04003CD0 RID: 15568
		private Hashtable usedFonts;

		// Token: 0x04003CD1 RID: 15569
		private Hashtable triplets;

		// Token: 0x04003CD2 RID: 15570
		private Hashtable fonts;
	}
}
