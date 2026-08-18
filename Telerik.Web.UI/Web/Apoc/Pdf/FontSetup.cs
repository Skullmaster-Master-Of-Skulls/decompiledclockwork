using System;
using System.Collections;
using Telerik.Pdf;
using Telerik.Pdf.Gdi;
using Telerik.Web.Apoc.Layout;
using Telerik.Web.Apoc.Render.Pdf;
using Telerik.Web.Apoc.Render.Pdf.Fonts;

namespace Telerik.Web.Apoc.Pdf
{
	// Token: 0x02001682 RID: 5762
	internal class FontSetup
	{
		// Token: 0x0600DEAE RID: 57006 RVA: 0x0030D53F File Offset: 0x0030B73F
		public FontSetup(FontInfo fontInfo, FontType proxyFontType)
		{
			this.fontInfo = fontInfo;
			this.AddBase14Fonts();
			this.AddSystemFonts(proxyFontType);
		}

		// Token: 0x0600DEAF RID: 57007 RVA: 0x0030D564 File Offset: 0x0030B764
		private void AddSystemFonts(FontType fontType)
		{
			GdiFontEnumerator gdiFontEnumerator = new GdiFontEnumerator(new GdiDeviceContent());
			foreach (string text in gdiFontEnumerator.FamilyNames)
			{
				if (this.IsBase14FontName(text))
				{
					ApocDriver.ActiveDriver.FireApocWarning("Will ignore TrueType font '" + text + "' because a base 14 font with the same name already exists.");
				}
				else
				{
					gdiFontEnumerator.GetStyles(text);
					string nextAvailableName = this.GetNextAvailableName();
					this.fontInfo.AddMetrics(nextAvailableName, new ProxyFont(new FontProperties(text, false, false), fontType));
					this.fontInfo.AddFontProperties(nextAvailableName, text, "normal", "normal");
					nextAvailableName = this.GetNextAvailableName();
					this.fontInfo.AddMetrics(nextAvailableName, new ProxyFont(new FontProperties(text, true, false), fontType));
					this.fontInfo.AddFontProperties(nextAvailableName, text, "normal", "bold");
					nextAvailableName = this.GetNextAvailableName();
					this.fontInfo.AddMetrics(nextAvailableName, new ProxyFont(new FontProperties(text, false, true), fontType));
					this.fontInfo.AddFontProperties(nextAvailableName, text, "italic", "normal");
					nextAvailableName = this.GetNextAvailableName();
					this.fontInfo.AddMetrics(nextAvailableName, new ProxyFont(new FontProperties(text, true, true), fontType));
					this.fontInfo.AddFontProperties(nextAvailableName, text, "italic", "bold");
				}
			}
			this.fontInfo.AddMetrics("F15", new ProxyFont(new FontProperties("Monotype Corsiva", false, false), fontType));
			this.fontInfo.AddFontProperties("F15", "cursive", "normal", "normal");
			this.fontInfo.AddMetrics("F16", Base14Font.ZapfDingbats);
			this.fontInfo.AddFontProperties("F16", "fantasy", "normal", "normal");
		}

		// Token: 0x0600DEB0 RID: 57008 RVA: 0x0030D728 File Offset: 0x0030B928
		private bool IsBase14FontName(string familyName)
		{
			switch (familyName)
			{
			case "any":
			case "sans-serif":
			case "serif":
			case "monospace":
			case "Helvetica":
			case "Times":
			case "Courier":
			case "Symbol":
			case "ZapfDingbats":
				return true;
			}
			return false;
		}

		// Token: 0x0600DEB1 RID: 57009 RVA: 0x0030D800 File Offset: 0x0030BA00
		private string GetNextAvailableName()
		{
			return string.Format("F{0}", this.startIndex++);
		}

		// Token: 0x0600DEB2 RID: 57010 RVA: 0x0030D830 File Offset: 0x0030BA30
		private void AddBase14Fonts()
		{
			this.fontInfo.AddMetrics("F1", Base14Font.Helvetica);
			this.fontInfo.AddMetrics("F2", Base14Font.HelveticaItalic);
			this.fontInfo.AddMetrics("F3", Base14Font.HelveticaBold);
			this.fontInfo.AddMetrics("F4", Base14Font.HelveticaBoldItalic);
			this.fontInfo.AddMetrics("F5", Base14Font.Times);
			this.fontInfo.AddMetrics("F6", Base14Font.TimesItalic);
			this.fontInfo.AddMetrics("F7", Base14Font.TimesBold);
			this.fontInfo.AddMetrics("F8", Base14Font.TimesBoldItalic);
			this.fontInfo.AddMetrics("F9", Base14Font.Courier);
			this.fontInfo.AddMetrics("F10", Base14Font.CourierItalic);
			this.fontInfo.AddMetrics("F11", Base14Font.CourierBold);
			this.fontInfo.AddMetrics("F12", Base14Font.CourierBoldItalic);
			this.fontInfo.AddMetrics("F13", Base14Font.Symbol);
			this.fontInfo.AddMetrics("F14", Base14Font.ZapfDingbats);
			this.fontInfo.AddFontProperties("F5", "any", "normal", "normal");
			this.fontInfo.AddFontProperties("F6", "any", "italic", "normal");
			this.fontInfo.AddFontProperties("F6", "any", "oblique", "normal");
			this.fontInfo.AddFontProperties("F7", "any", "normal", "bold");
			this.fontInfo.AddFontProperties("F8", "any", "italic", "bold");
			this.fontInfo.AddFontProperties("F8", "any", "oblique", "bold");
			this.fontInfo.AddFontProperties("F1", "sans-serif", "normal", "normal");
			this.fontInfo.AddFontProperties("F2", "sans-serif", "oblique", "normal");
			this.fontInfo.AddFontProperties("F2", "sans-serif", "italic", "normal");
			this.fontInfo.AddFontProperties("F3", "sans-serif", "normal", "bold");
			this.fontInfo.AddFontProperties("F4", "sans-serif", "oblique", "bold");
			this.fontInfo.AddFontProperties("F4", "sans-serif", "italic", "bold");
			this.fontInfo.AddFontProperties("F5", "serif", "normal", "normal");
			this.fontInfo.AddFontProperties("F6", "serif", "oblique", "normal");
			this.fontInfo.AddFontProperties("F6", "serif", "italic", "normal");
			this.fontInfo.AddFontProperties("F7", "serif", "normal", "bold");
			this.fontInfo.AddFontProperties("F8", "serif", "oblique", "bold");
			this.fontInfo.AddFontProperties("F8", "serif", "italic", "bold");
			this.fontInfo.AddFontProperties("F9", "monospace", "normal", "normal");
			this.fontInfo.AddFontProperties("F10", "monospace", "oblique", "normal");
			this.fontInfo.AddFontProperties("F10", "monospace", "italic", "normal");
			this.fontInfo.AddFontProperties("F11", "monospace", "normal", "bold");
			this.fontInfo.AddFontProperties("F12", "monospace", "oblique", "bold");
			this.fontInfo.AddFontProperties("F12", "monospace", "italic", "bold");
			this.fontInfo.AddFontProperties("F1", "Helvetica", "normal", "normal");
			this.fontInfo.AddFontProperties("F2", "Helvetica", "oblique", "normal");
			this.fontInfo.AddFontProperties("F2", "Helvetica", "italic", "normal");
			this.fontInfo.AddFontProperties("F3", "Helvetica", "normal", "bold");
			this.fontInfo.AddFontProperties("F4", "Helvetica", "oblique", "bold");
			this.fontInfo.AddFontProperties("F4", "Helvetica", "italic", "bold");
			this.fontInfo.AddFontProperties("F5", "Times", "normal", "normal");
			this.fontInfo.AddFontProperties("F6", "Times", "oblique", "normal");
			this.fontInfo.AddFontProperties("F6", "Times", "italic", "normal");
			this.fontInfo.AddFontProperties("F7", "Times", "normal", "bold");
			this.fontInfo.AddFontProperties("F8", "Times", "oblique", "bold");
			this.fontInfo.AddFontProperties("F8", "Times", "italic", "bold");
			this.fontInfo.AddFontProperties("F9", "Courier", "normal", "normal");
			this.fontInfo.AddFontProperties("F10", "Courier", "oblique", "normal");
			this.fontInfo.AddFontProperties("F10", "Courier", "italic", "normal");
			this.fontInfo.AddFontProperties("F11", "Courier", "normal", "bold");
			this.fontInfo.AddFontProperties("F12", "Courier", "oblique", "bold");
			this.fontInfo.AddFontProperties("F12", "Courier", "italic", "bold");
			this.fontInfo.AddFontProperties("F13", "Symbol", "normal", "normal");
			this.fontInfo.AddFontProperties("F14", "ZapfDingbats", "normal", "normal");
		}

		// Token: 0x0600DEB3 RID: 57011 RVA: 0x0030DEB8 File Offset: 0x0030C0B8
		internal void AddToResources(PdfFontCreator fontCreator, PdfResources resources)
		{
			Hashtable usedFonts = this.fontInfo.GetUsedFonts();
			foreach (object obj in usedFonts.Keys)
			{
				string text = (string)obj;
				Font font = (Font)usedFonts[text];
				resources.AddFont(fontCreator.MakeFont(text, font));
			}
		}

		// Token: 0x04004013 RID: 16403
		private int startIndex = 17;

		// Token: 0x04004014 RID: 16404
		private FontInfo fontInfo;
	}
}
