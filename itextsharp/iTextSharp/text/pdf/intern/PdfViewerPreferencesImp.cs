using System;
using iTextSharp.text.pdf.interfaces;

namespace iTextSharp.text.pdf.intern
{
	// Token: 0x02000284 RID: 644
	public class PdfViewerPreferencesImp : IPdfViewerPreferences
	{
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x0008CAB7 File Offset: 0x0008BAB7
		public int PageLayoutAndMode
		{
			get
			{
				return this.pageLayoutAndMode;
			}
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0008CABF File Offset: 0x0008BABF
		public PdfDictionary GetViewerPreferences()
		{
			return this.viewerPreferences;
		}

		// Token: 0x17000471 RID: 1137
		// (set) Token: 0x06001854 RID: 6228 RVA: 0x0008CAC8 File Offset: 0x0008BAC8
		public int ViewerPreferences
		{
			set
			{
				this.pageLayoutAndMode |= value;
				if ((value & 16773120) != 0)
				{
					this.pageLayoutAndMode = (-16773121 & this.pageLayoutAndMode);
					if ((value & 4096) != 0)
					{
						this.viewerPreferences.Put(PdfName.HIDETOOLBAR, PdfBoolean.PDFTRUE);
					}
					if ((value & 8192) != 0)
					{
						this.viewerPreferences.Put(PdfName.HIDEMENUBAR, PdfBoolean.PDFTRUE);
					}
					if ((value & 16384) != 0)
					{
						this.viewerPreferences.Put(PdfName.HIDEWINDOWUI, PdfBoolean.PDFTRUE);
					}
					if ((value & 32768) != 0)
					{
						this.viewerPreferences.Put(PdfName.FITWINDOW, PdfBoolean.PDFTRUE);
					}
					if ((value & 65536) != 0)
					{
						this.viewerPreferences.Put(PdfName.CENTERWINDOW, PdfBoolean.PDFTRUE);
					}
					if ((value & 131072) != 0)
					{
						this.viewerPreferences.Put(PdfName.DISPLAYDOCTITLE, PdfBoolean.PDFTRUE);
					}
					if ((value & 262144) != 0)
					{
						this.viewerPreferences.Put(PdfName.NONFULLSCREENPAGEMODE, PdfName.USENONE);
					}
					else if ((value & 524288) != 0)
					{
						this.viewerPreferences.Put(PdfName.NONFULLSCREENPAGEMODE, PdfName.USEOUTLINES);
					}
					else if ((value & 1048576) != 0)
					{
						this.viewerPreferences.Put(PdfName.NONFULLSCREENPAGEMODE, PdfName.USETHUMBS);
					}
					else if ((value & 2097152) != 0)
					{
						this.viewerPreferences.Put(PdfName.NONFULLSCREENPAGEMODE, PdfName.USEOC);
					}
					if ((value & 4194304) != 0)
					{
						this.viewerPreferences.Put(PdfName.DIRECTION, PdfName.L2R);
					}
					else if ((value & 8388608) != 0)
					{
						this.viewerPreferences.Put(PdfName.DIRECTION, PdfName.R2L);
					}
					if ((value & 16777216) != 0)
					{
						this.viewerPreferences.Put(PdfName.PRINTSCALING, PdfName.NONE);
					}
				}
			}
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0008CC94 File Offset: 0x0008BC94
		private int GetIndex(PdfName key)
		{
			for (int i = 0; i < PdfViewerPreferencesImp.VIEWER_PREFERENCES.Length; i++)
			{
				if (PdfViewerPreferencesImp.VIEWER_PREFERENCES[i].Equals(key))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0008CCC8 File Offset: 0x0008BCC8
		private bool IsPossibleValue(PdfName value, PdfName[] accepted)
		{
			for (int i = 0; i < accepted.Length; i++)
			{
				if (accepted[i].Equals(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0008CCF4 File Offset: 0x0008BCF4
		public virtual void AddViewerPreference(PdfName key, PdfObject value)
		{
			switch (this.GetIndex(key))
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 14:
				if (value is PdfBoolean)
				{
					this.viewerPreferences.Put(key, value);
					return;
				}
				break;
			case 6:
				if (value is PdfName && this.IsPossibleValue((PdfName)value, PdfViewerPreferencesImp.NONFULLSCREENPAGEMODE_PREFERENCES))
				{
					this.viewerPreferences.Put(key, value);
					return;
				}
				break;
			case 7:
				if (value is PdfName && this.IsPossibleValue((PdfName)value, PdfViewerPreferencesImp.DIRECTION_PREFERENCES))
				{
					this.viewerPreferences.Put(key, value);
					return;
				}
				break;
			case 8:
			case 9:
			case 10:
			case 11:
				if (value is PdfName && this.IsPossibleValue((PdfName)value, PdfViewerPreferencesImp.PAGE_BOUNDARIES))
				{
					this.viewerPreferences.Put(key, value);
					return;
				}
				break;
			case 12:
				if (value is PdfName && this.IsPossibleValue((PdfName)value, PdfViewerPreferencesImp.PRINTSCALING_PREFERENCES))
				{
					this.viewerPreferences.Put(key, value);
					return;
				}
				break;
			case 13:
				if (value is PdfName && this.IsPossibleValue((PdfName)value, PdfViewerPreferencesImp.DUPLEX_PREFERENCES))
				{
					this.viewerPreferences.Put(key, value);
					return;
				}
				break;
			case 15:
				if (value is PdfArray)
				{
					this.viewerPreferences.Put(key, value);
					return;
				}
				break;
			case 16:
				if (value is PdfNumber)
				{
					this.viewerPreferences.Put(key, value);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0008CE78 File Offset: 0x0008BE78
		public void AddToCatalog(PdfDictionary catalog)
		{
			catalog.Remove(PdfName.PAGELAYOUT);
			if ((this.pageLayoutAndMode & 1) != 0)
			{
				catalog.Put(PdfName.PAGELAYOUT, PdfName.SINGLEPAGE);
			}
			else if ((this.pageLayoutAndMode & 2) != 0)
			{
				catalog.Put(PdfName.PAGELAYOUT, PdfName.ONECOLUMN);
			}
			else if ((this.pageLayoutAndMode & 4) != 0)
			{
				catalog.Put(PdfName.PAGELAYOUT, PdfName.TWOCOLUMNLEFT);
			}
			else if ((this.pageLayoutAndMode & 8) != 0)
			{
				catalog.Put(PdfName.PAGELAYOUT, PdfName.TWOCOLUMNRIGHT);
			}
			else if ((this.pageLayoutAndMode & 16) != 0)
			{
				catalog.Put(PdfName.PAGELAYOUT, PdfName.TWOPAGELEFT);
			}
			else if ((this.pageLayoutAndMode & 32) != 0)
			{
				catalog.Put(PdfName.PAGELAYOUT, PdfName.TWOPAGERIGHT);
			}
			catalog.Remove(PdfName.PAGEMODE);
			if ((this.pageLayoutAndMode & 64) != 0)
			{
				catalog.Put(PdfName.PAGEMODE, PdfName.USENONE);
			}
			else if ((this.pageLayoutAndMode & 128) != 0)
			{
				catalog.Put(PdfName.PAGEMODE, PdfName.USEOUTLINES);
			}
			else if ((this.pageLayoutAndMode & 256) != 0)
			{
				catalog.Put(PdfName.PAGEMODE, PdfName.USETHUMBS);
			}
			else if ((this.pageLayoutAndMode & 512) != 0)
			{
				catalog.Put(PdfName.PAGEMODE, PdfName.FULLSCREEN);
			}
			else if ((this.pageLayoutAndMode & 1024) != 0)
			{
				catalog.Put(PdfName.PAGEMODE, PdfName.USEOC);
			}
			else if ((this.pageLayoutAndMode & 2048) != 0)
			{
				catalog.Put(PdfName.PAGEMODE, PdfName.USEATTACHMENTS);
			}
			catalog.Remove(PdfName.VIEWERPREFERENCES);
			if (this.viewerPreferences.Size > 0)
			{
				catalog.Put(PdfName.VIEWERPREFERENCES, this.viewerPreferences);
			}
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0008D030 File Offset: 0x0008C030
		public static PdfViewerPreferencesImp GetViewerPreferences(PdfDictionary catalog)
		{
			PdfViewerPreferencesImp pdfViewerPreferencesImp = new PdfViewerPreferencesImp();
			int num = 0;
			PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(catalog.Get(PdfName.PAGELAYOUT));
			if (pdfObjectRelease != null && pdfObjectRelease.IsName())
			{
				PdfName pdfName = (PdfName)pdfObjectRelease;
				if (pdfName.Equals(PdfName.SINGLEPAGE))
				{
					num |= 1;
				}
				else if (pdfName.Equals(PdfName.ONECOLUMN))
				{
					num |= 2;
				}
				else if (pdfName.Equals(PdfName.TWOCOLUMNLEFT))
				{
					num |= 4;
				}
				else if (pdfName.Equals(PdfName.TWOCOLUMNRIGHT))
				{
					num |= 8;
				}
				else if (pdfName.Equals(PdfName.TWOPAGELEFT))
				{
					num |= 16;
				}
				else if (pdfName.Equals(PdfName.TWOPAGERIGHT))
				{
					num |= 32;
				}
			}
			pdfObjectRelease = PdfReader.GetPdfObjectRelease(catalog.Get(PdfName.PAGEMODE));
			if (pdfObjectRelease != null && pdfObjectRelease.IsName())
			{
				PdfName pdfName = (PdfName)pdfObjectRelease;
				if (pdfName.Equals(PdfName.USENONE))
				{
					num |= 64;
				}
				else if (pdfName.Equals(PdfName.USEOUTLINES))
				{
					num |= 128;
				}
				else if (pdfName.Equals(PdfName.USETHUMBS))
				{
					num |= 256;
				}
				else if (pdfName.Equals(PdfName.FULLSCREEN))
				{
					num |= 512;
				}
				else if (pdfName.Equals(PdfName.USEOC))
				{
					num |= 1024;
				}
				else if (pdfName.Equals(PdfName.USEATTACHMENTS))
				{
					num |= 2048;
				}
			}
			pdfViewerPreferencesImp.ViewerPreferences = num;
			pdfObjectRelease = PdfReader.GetPdfObjectRelease(catalog.Get(PdfName.VIEWERPREFERENCES));
			if (pdfObjectRelease != null && pdfObjectRelease.IsDictionary())
			{
				PdfDictionary pdfDictionary = (PdfDictionary)pdfObjectRelease;
				for (int i = 0; i < PdfViewerPreferencesImp.VIEWER_PREFERENCES.Length; i++)
				{
					pdfObjectRelease = PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfViewerPreferencesImp.VIEWER_PREFERENCES[i]));
					pdfViewerPreferencesImp.AddViewerPreference(PdfViewerPreferencesImp.VIEWER_PREFERENCES[i], pdfObjectRelease);
				}
			}
			return pdfViewerPreferencesImp;
		}

		// Token: 0x04001059 RID: 4185
		private const int viewerPreferencesMask = 16773120;

		// Token: 0x0400105A RID: 4186
		public static readonly PdfName[] VIEWER_PREFERENCES = new PdfName[]
		{
			PdfName.HIDETOOLBAR,
			PdfName.HIDEMENUBAR,
			PdfName.HIDEWINDOWUI,
			PdfName.FITWINDOW,
			PdfName.CENTERWINDOW,
			PdfName.DISPLAYDOCTITLE,
			PdfName.NONFULLSCREENPAGEMODE,
			PdfName.DIRECTION,
			PdfName.VIEWAREA,
			PdfName.VIEWCLIP,
			PdfName.PRINTAREA,
			PdfName.PRINTCLIP,
			PdfName.PRINTSCALING,
			PdfName.DUPLEX,
			PdfName.PICKTRAYBYPDFSIZE,
			PdfName.PRINTPAGERANGE,
			PdfName.NUMCOPIES
		};

		// Token: 0x0400105B RID: 4187
		public static readonly PdfName[] NONFULLSCREENPAGEMODE_PREFERENCES = new PdfName[]
		{
			PdfName.USENONE,
			PdfName.USEOUTLINES,
			PdfName.USETHUMBS,
			PdfName.USEOC
		};

		// Token: 0x0400105C RID: 4188
		public static readonly PdfName[] DIRECTION_PREFERENCES = new PdfName[]
		{
			PdfName.L2R,
			PdfName.R2L
		};

		// Token: 0x0400105D RID: 4189
		public static readonly PdfName[] PAGE_BOUNDARIES = new PdfName[]
		{
			PdfName.MEDIABOX,
			PdfName.CROPBOX,
			PdfName.BLEEDBOX,
			PdfName.TRIMBOX,
			PdfName.ARTBOX
		};

		// Token: 0x0400105E RID: 4190
		public static readonly PdfName[] PRINTSCALING_PREFERENCES = new PdfName[]
		{
			PdfName.APPDEFAULT,
			PdfName.NONE
		};

		// Token: 0x0400105F RID: 4191
		public static readonly PdfName[] DUPLEX_PREFERENCES = new PdfName[]
		{
			PdfName.SIMPLEX,
			PdfName.DUPLEXFLIPSHORTEDGE,
			PdfName.DUPLEXFLIPLONGEDGE
		};

		// Token: 0x04001060 RID: 4192
		private int pageLayoutAndMode;

		// Token: 0x04001061 RID: 4193
		private PdfDictionary viewerPreferences = new PdfDictionary();
	}
}
