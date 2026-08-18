using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x0200162D RID: 5677
	public class GdiFontEnumerator
	{
		// Token: 0x0600DCB7 RID: 56503 RVA: 0x00303CE9 File Offset: 0x00301EE9
		public GdiFontEnumerator(GdiDeviceContent dc)
		{
			this.dc = dc;
		}

		// Token: 0x17004392 RID: 17298
		// (get) Token: 0x0600DCB8 RID: 56504 RVA: 0x00303D10 File Offset: 0x00301F10
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public string[] FamilyNames
		{
			get
			{
				LogFont logFont = new LogFont();
				logFont.lfCharSet = 1;
				FontEnumDelegate lpEnumFontFamProc = new FontEnumDelegate(this.EnumFontMethod);
				NativeMethods.EnumFontFamiliesEx(this.dc.Handle, logFont, lpEnumFontFamProc, 1, 0);
				return (string[])new ArrayList(this.families.Keys).ToArray(typeof(string));
			}
		}

		// Token: 0x0600DCB9 RID: 56505 RVA: 0x00303D70 File Offset: 0x00301F70
		public FontStyles GetStyles(string familyName)
		{
			this.styles.Clear();
			FontEnumDelegate lpEnumFontFamProc = new FontEnumDelegate(this.EnumFontMethod);
			NativeMethods.EnumFontFamilies(this.dc.Handle, familyName, lpEnumFontFamProc, 2);
			return this.styles;
		}

		// Token: 0x0600DCBA RID: 56506 RVA: 0x00303DB0 File Offset: 0x00301FB0
		private int EnumFontMethod(ref EnumLogFont logFont, ref NewTextMetric textMetric, int fontType, int lParam)
		{
			if ((fontType & 4) > 0)
			{
				if (lParam == 1)
				{
					string lfFaceName = logFont.elfLogFont.lfFaceName;
					if (!this.families.ContainsKey(lfFaceName))
					{
						this.families.Add(lfFaceName, string.Empty);
					}
				}
				else
				{
					if (lParam != 2)
					{
						throw new InvalidOperationException("Unknown EnumFontMethod parameter.");
					}
					string text = new string(logFont.elfStyle);
					char[] trimChars = new char[1];
					string styleName = text.Trim(trimChars);
					if (!this.styles.Contains(styleName))
					{
						this.styles.AddStyle(styleName);
					}
				}
			}
			return 1;
		}

		// Token: 0x04003E49 RID: 15945
		private const int RasterFont = 1;

		// Token: 0x04003E4A RID: 15946
		private const int DeviceFont = 2;

		// Token: 0x04003E4B RID: 15947
		private const int TrueTypeFont = 4;

		// Token: 0x04003E4C RID: 15948
		private const int ExtractFamilies = 1;

		// Token: 0x04003E4D RID: 15949
		private const int ExtractStyles = 2;

		// Token: 0x04003E4E RID: 15950
		private const byte AnsiCharset = 0;

		// Token: 0x04003E4F RID: 15951
		private const byte DefaultCharset = 1;

		// Token: 0x04003E50 RID: 15952
		private const byte SymbolCharset = 2;

		// Token: 0x04003E51 RID: 15953
		private SortedList families = new SortedList();

		// Token: 0x04003E52 RID: 15954
		private FontStyles styles = new FontStyles();

		// Token: 0x04003E53 RID: 15955
		private GdiDeviceContent dc;
	}
}
