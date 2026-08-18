using System;
using System.Collections;
using System.IO;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x02001631 RID: 5681
	public class GdiPrivateFontCollection
	{
		// Token: 0x0600DCE9 RID: 56553 RVA: 0x003046B1 File Offset: 0x003028B1
		public void AddFontFile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename", "Parameter cannot be null");
			}
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentException("filename", "Parameter cannot be empty string");
			}
			this.AddFontFile(new FileInfo(filename));
		}

		// Token: 0x0600DCEA RID: 56554 RVA: 0x003046EC File Offset: 0x003028EC
		public void AddFontFile(FileInfo fontFile)
		{
			if (fontFile == null)
			{
				throw new ArgumentNullException("fontFile", "Parameter cannot be null");
			}
			if (!fontFile.Exists)
			{
				throw new FileNotFoundException("Font file does not exist", fontFile.FullName);
			}
			if (this.fonts.Contains(fontFile.FullName))
			{
				throw new ArgumentException("Font file already exists", "fontFile");
			}
			string fullName = fontFile.FullName;
			this.fonts.Add(fullName, string.Empty);
			if (NativeMethods.AddFontResourceEx(fullName, 16, 0) == 0)
			{
				throw new ArgumentException("Unable to add font file: " + fullName, "fontFile");
			}
		}

		// Token: 0x04003E66 RID: 15974
		private const int FR_PRIVATE = 16;

		// Token: 0x04003E67 RID: 15975
		private const int FR_NOT_ENUM = 32;

		// Token: 0x04003E68 RID: 15976
		private IDictionary fonts = new Hashtable();
	}
}
