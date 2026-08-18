using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Spire.License;
using Spire.Pdf;
using Spire.Pdf.Annotations;
using Spire.Pdf.General;
using Spire.Pdf.Graphics;
using Spire.Xls.Collections;
using Spire.Xls.Conversion.Element.HeaderFooter;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Converter
{
	// Token: 0x02000020 RID: 32
	public class PdfConverter : IDisposable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000089 RID: 137 RVA: 0x00007A88 File Offset: 0x00005C88
		// (remove) Token: 0x0600008A RID: 138 RVA: 0x00007B20 File Offset: 0x00005D20
		public event ProgressEventHandler CurrentProgressChanged
		{
			add
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (true)
						{
						}
						if (false)
						{
						}
						ProgressEventHandler progressEventHandler = this.\u171D;
						int num = 0;
						for (;;)
						{
							ProgressEventHandler progressEventHandler2;
							switch (num)
							{
							case 0:
								goto IL_53;
							case 1:
								if (progressEventHandler == progressEventHandler2)
								{
									num = 2;
									continue;
								}
								goto IL_53;
							case 2:
								return;
							}
							break;
							IL_53:
							progressEventHandler2 = progressEventHandler;
							ProgressEventHandler value2 = (ProgressEventHandler)Delegate.Combine(progressEventHandler2, value);
							progressEventHandler = Interlocked.CompareExchange<ProgressEventHandler>(ref this.\u171D, value2, progressEventHandler2);
							num = 1;
						}
						break;
					}
					}
				}
			}
			remove
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (true)
						{
						}
						if (false)
						{
						}
						ProgressEventHandler progressEventHandler = this.\u171D;
						int num = 2;
						for (;;)
						{
							ProgressEventHandler progressEventHandler2;
							switch (num)
							{
							case 0:
								if (progressEventHandler == progressEventHandler2)
								{
									num = 1;
									continue;
								}
								goto IL_53;
							case 1:
								return;
							case 2:
								goto IL_53;
							}
							break;
							IL_53:
							progressEventHandler2 = progressEventHandler;
							ProgressEventHandler value2 = (ProgressEventHandler)Delegate.Remove(progressEventHandler2, value);
							progressEventHandler = Interlocked.CompareExchange<ProgressEventHandler>(ref this.\u171D, value2, progressEventHandler2);
							num = 0;
						}
						break;
					}
					}
				}
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600008B RID: 139 RVA: 0x00007BB8 File Offset: 0x00005DB8
		// (remove) Token: 0x0600008C RID: 140 RVA: 0x00007C50 File Offset: 0x00005E50
		public event SheetFinishedEventHandler SheetAfterDrawn
		{
			add
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (false)
						{
						}
						SheetFinishedEventHandler sheetFinishedEventHandler = this.\u171E;
						int num = 0;
						for (;;)
						{
							SheetFinishedEventHandler sheetFinishedEventHandler2;
							switch (num)
							{
							case 0:
								goto IL_4B;
							case 1:
								return;
							case 2:
								if (sheetFinishedEventHandler == sheetFinishedEventHandler2)
								{
									if (true)
									{
									}
									num = 1;
									continue;
								}
								goto IL_4B;
							}
							break;
							IL_4B:
							sheetFinishedEventHandler2 = sheetFinishedEventHandler;
							SheetFinishedEventHandler value2 = (SheetFinishedEventHandler)Delegate.Combine(sheetFinishedEventHandler2, value);
							sheetFinishedEventHandler = Interlocked.CompareExchange<SheetFinishedEventHandler>(ref this.\u171E, value2, sheetFinishedEventHandler2);
							num = 2;
						}
						break;
					}
					}
				}
			}
			remove
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (false)
						{
						}
						SheetFinishedEventHandler sheetFinishedEventHandler = this.\u171E;
						int num = 1;
						for (;;)
						{
							SheetFinishedEventHandler sheetFinishedEventHandler2;
							switch (num)
							{
							case 0:
								if (sheetFinishedEventHandler == sheetFinishedEventHandler2)
								{
									if (true)
									{
									}
									num = 2;
									continue;
								}
								goto IL_4B;
							case 1:
								goto IL_4B;
							case 2:
								return;
							}
							break;
							IL_4B:
							sheetFinishedEventHandler2 = sheetFinishedEventHandler;
							SheetFinishedEventHandler value2 = (SheetFinishedEventHandler)Delegate.Remove(sheetFinishedEventHandler2, value);
							sheetFinishedEventHandler = Interlocked.CompareExchange<SheetFinishedEventHandler>(ref this.\u171E, value2, sheetFinishedEventHandler2);
							num = 0;
						}
						break;
					}
					}
				}
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600008D RID: 141 RVA: 0x00007CE8 File Offset: 0x00005EE8
		// (remove) Token: 0x0600008E RID: 142 RVA: 0x00007D80 File Offset: 0x00005F80
		public event SheetStartEventHandler SheetBeforeDrawn
		{
			add
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (false)
						{
						}
						SheetStartEventHandler sheetStartEventHandler = this.\u171F;
						int num = 1;
						for (;;)
						{
							SheetStartEventHandler sheetStartEventHandler2;
							switch (num)
							{
							case 0:
								return;
							case 1:
								goto IL_4B;
							case 2:
								if (sheetStartEventHandler == sheetStartEventHandler2)
								{
									num = 0;
									continue;
								}
								goto IL_4B;
							}
							break;
							IL_4B:
							sheetStartEventHandler2 = sheetStartEventHandler;
							SheetStartEventHandler value2 = (SheetStartEventHandler)Delegate.Combine(sheetStartEventHandler2, value);
							sheetStartEventHandler = Interlocked.CompareExchange<SheetStartEventHandler>(ref this.\u171F, value2, sheetStartEventHandler2);
							if (true)
							{
							}
							num = 2;
						}
						break;
					}
					}
				}
			}
			remove
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (true)
						{
						}
						if (false)
						{
						}
						SheetStartEventHandler sheetStartEventHandler = this.\u171F;
						int num = 2;
						for (;;)
						{
							SheetStartEventHandler sheetStartEventHandler2;
							switch (num)
							{
							case 0:
								return;
							case 1:
								if (sheetStartEventHandler == sheetStartEventHandler2)
								{
									num = 0;
									continue;
								}
								goto IL_53;
							case 2:
								goto IL_53;
							}
							break;
							IL_53:
							sheetStartEventHandler2 = sheetStartEventHandler;
							SheetStartEventHandler value2 = (SheetStartEventHandler)Delegate.Remove(sheetStartEventHandler2, value);
							sheetStartEventHandler = Interlocked.CompareExchange<SheetStartEventHandler>(ref this.\u171F, value2, sheetStartEventHandler2);
							num = 1;
						}
						break;
					}
					}
				}
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00007E18 File Offset: 0x00006018
		public PdfConverter()
		{
			this.\u170D = new PdfUnitConvertor();
			this.ᜎ = new sprᯏ();
			this.ᜑ = SizeF.Empty;
			this.\u1712 = -1f;
			this.\u1719 = 1f;
			this.\u171A = true;
			base..ctor();
			this.ᜃ = new spr\u2310();
			this.ᜉ = new PdfDocument();
			this.ᜌ = new PdfConverterSettings();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00007E8C File Offset: 0x0000608C
		public PdfConverter(Workbook workBook) : this()
		{
			this.ᜆ = workBook;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00007EA8 File Offset: 0x000060A8
		public PdfConverter(Worksheet workSheet) : this()
		{
			this.ᜏ = workSheet;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00007EC4 File Offset: 0x000060C4
		public PdfConverter(Stream stream)
		{
			int a_ = 14;
			this..ctor();
			if (stream.CanSeek)
			{
				if (stream.CanRead)
				{
					this.ᜆ = new Workbook();
					this.ᜆ.LoadFromStream(stream);
					this.\u171B = true;
					return;
				}
			}
			throw new Exception(SheetFinishedEventHandler.b("뛄돆믈껊곌ꋎ", a_));
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00007F28 File Offset: 0x00006128
		public PdfConverter(string filePath)
		{
			int a_ = 18;
			this..ctor();
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException(SheetFinishedEventHandler.b("꿈ꋊꇌ꫎臐닒ꇔ뿖", a_));
			}
			this.ᜆ = new Workbook();
			if (filePath.ToString().EndsWith(SheetFinishedEventHandler.b("돊ꇌ볎꧐", a_), StringComparison.InvariantCultureIgnoreCase))
			{
				this.ᜆ.LoadFromFile(filePath, ExcelVersion.Version2007);
				this.\u171B = true;
				return;
			}
			if (filePath.ToString().EndsWith(SheetFinishedEventHandler.b("돊ꇌ볎", a_), StringComparison.InvariantCultureIgnoreCase))
			{
				this.ᜆ.LoadFromFile(filePath, ExcelVersion.Version97to2003);
				this.\u171B = true;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00007FD8 File Offset: 0x000061D8
		private FitToPageType FitToPage
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_75;
					case 1:
						if (this.ᜌ.EnableExcelPageBreak)
						{
							num = 0;
							continue;
						}
						goto IL_81;
					case 3:
						num = 1;
						continue;
					}
					if (true)
					{
					}
					if (this.ᜌ == null)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 3;
						break;
					}
				}
				return FitToPageType.None;
				IL_75:
				return FitToPageType.None;
				IL_81:
				return this.ᜌ.FitSheetToOnePage;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00008074 File Offset: 0x00006274
		private bool EnableExcelPageBreak
		{
			get
			{
				while (this.ᜌ != null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						return this.ᜌ.EnableExcelPageBreak;
					}
				}
				return false;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000080C8 File Offset: 0x000062C8
		private XlsFont DefaultXlsFont
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_AC;
					case 2:
						if (true)
						{
						}
						num = 7;
						continue;
					case 3:
						goto IL_CE;
					case 4:
						goto IL_EF;
					case 5:
						this.\u1714 = this.ᜆ.DefaultFont;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CE;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 6:
						if (this.ᜏ != null)
						{
							num = 3;
							continue;
						}
						goto IL_F1;
					case 7:
						if (this.ᜆ != null)
						{
							num = 5;
							continue;
						}
						num = 6;
						continue;
					}
					if (this.\u1714 == null)
					{
						num = 2;
						continue;
					}
					break;
					IL_CE:
					this.\u1714 = this.ᜏ.Workbook.DefaultFont;
					num = 4;
				}
				IL_AC:
				IL_EF:
				IL_F1:
				return this.\u1714;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000081CC File Offset: 0x000063CC
		private Font DefaultFont
		{
			get
			{
				int num = 1;
				for (;;)
				{
					XlsFont xlsFont;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							goto IL_7F;
						case 2:
							xlsFont = this.DefaultXlsFont;
							if (true)
							{
							}
							num = 3;
							continue;
						case 3:
							if (xlsFont != null)
							{
								num = 0;
								continue;
							}
							this.\u1713 = new Font(FontFamily.GenericSansSerif, 11f);
							num = 4;
							continue;
						case 4:
							goto IL_7D;
						case 5:
							goto IL_96;
						}
						if (this.\u1713 == null)
						{
							num = 2;
							continue;
						}
						goto IL_C2;
					}
					IL_7F:
					this.\u1713 = xlsFont.GenerateNativeFont();
					num = 5;
				}
				IL_7D:
				IL_96:
				IL_C2:
				return this.\u1713;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000082A4 File Offset: 0x000064A4
		private PdfTrueTypeFont DefaultPdfFont
		{
			get
			{
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_75;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						this.\u1715 = new PdfTrueTypeFont(this.DefaultFont);
						num = 2;
						continue;
					case 2:
						goto IL_75;
					}
					if (this.\u1715 != null)
					{
						break;
					}
					num = 1;
				}
				IL_75:
				return this.\u1715;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00008330 File Offset: 0x00006530
		private void ᜃ()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜉ.Bookmarks.Add(this.ᜏ.Name).Destination = new PdfDestination(this.ᜅ, new PointF(0f, 0f));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000083A8 File Offset: 0x000065A8
		private PointF ᜀ(PointF[] A_0, RectangleF A_1, PdfTextAlignment A_2, PdfVerticalAlignment A_3)
		{
			switch (0)
			{
			default:
			{
				float x;
				float y;
				for (;;)
				{
					x = 0f;
					y = 0f;
					float num = float.MaxValue;
					float num2 = float.MaxValue;
					float num3 = float.MinValue;
					float num4 = float.MinValue;
					int num5 = 0;
					int num6 = A_0.Length;
					int num7 = 8;
					for (;;)
					{
						switch (num7)
						{
						case 0:
							goto IL_2E7;
						case 1:
							goto IL_23B;
						case 2:
						{
							PointF pointF;
							if (num > pointF.X)
							{
								num7 = 15;
								continue;
							}
							goto IL_16E;
						}
						case 3:
							goto IL_1FD;
						case 4:
						{
							PointF pointF;
							if (num4 < pointF.Y)
							{
								num7 = 27;
								continue;
							}
							goto IL_2D0;
						}
						case 5:
							goto IL_161;
						case 6:
							switch (A_3)
							{
							case PdfVerticalAlignment.Top:
								x = A_1.Height - num4;
								num7 = 1;
								continue;
							case PdfVerticalAlignment.Middle:
								y = (A_1.Height - num4) / 2f;
								num7 = 13;
								continue;
							case PdfVerticalAlignment.Bottom:
								y = -num2;
								num7 = 26;
								continue;
							}
							goto IL_30A;
						case 7:
							num7 = 14;
							continue;
						case 8:
							goto IL_1D7;
						case 9:
							goto IL_2D0;
						case 10:
							goto IL_2E7;
						case 11:
						{
							PointF pointF;
							num2 = pointF.Y;
							num7 = 23;
							continue;
						}
						case 12:
						{
							if (num5 >= num6)
							{
								num7 = 17;
								continue;
							}
							PointF pointF = A_0[num5];
							num7 = 2;
							continue;
						}
						case 13:
							goto IL_1D2;
						case 14:
							goto IL_2E7;
						case 15:
						{
							PointF pointF;
							num = pointF.X;
							num7 = 21;
							continue;
						}
						case 16:
						{
							PointF pointF;
							num3 = pointF.X;
							num7 = 3;
							continue;
						}
						case 17:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_30A;
							default:
								if (false)
								{
								}
								num7 = 20;
								continue;
							}
							break;
						case 18:
						{
							PointF pointF;
							if (num2 > pointF.Y)
							{
								num7 = 11;
								continue;
							}
							goto IL_32C;
						}
						case 19:
							goto IL_2E7;
						case 20:
							switch (A_2)
							{
							case PdfTextAlignment.Left:
								x = -num;
								num7 = 19;
								continue;
							case PdfTextAlignment.Center:
								x = (A_1.Width - num3) / 2f;
								num7 = 10;
								continue;
							case PdfTextAlignment.Right:
								x = A_1.Width - num3;
								num7 = 0;
								continue;
							default:
								num7 = 7;
								continue;
							}
							break;
						case 21:
							goto IL_16E;
						case 22:
							num7 = 5;
							continue;
						case 23:
							goto IL_32C;
						case 24:
						{
							PointF pointF;
							if (num3 < pointF.X)
							{
								num7 = 16;
								continue;
							}
							goto IL_1FD;
						}
						case 25:
							goto IL_1D7;
						case 26:
							goto IL_24F;
						case 27:
						{
							PointF pointF;
							num4 = pointF.Y;
							num7 = 9;
							continue;
						}
						}
						break;
						IL_16E:
						num7 = 24;
						continue;
						IL_1D7:
						num7 = 12;
						continue;
						IL_1FD:
						num7 = 18;
						continue;
						IL_2D0:
						num5++;
						num7 = 25;
						continue;
						IL_2E7:
						num7 = 6;
						continue;
						IL_30A:
						num7 = 22;
						continue;
						IL_32C:
						num7 = 4;
					}
				}
				IL_161:
				if (true)
				{
				}
				IL_1D2:
				IL_23B:
				IL_24F:
				return new PointF(x, y);
			}
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00008720 File Offset: 0x00006920
		private Image ᜀ(Image A_0, RectangleF A_1, int A_2, int A_3)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			Bitmap bitmap = new Bitmap(A_0, new Size(A_2, A_3));
			return bitmap.Clone(A_1, bitmap.PixelFormat);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00008778 File Offset: 0x00006978
		private void ᜂ()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.\u1716 = new List<Worksheet>();
			this.ᜋ = new Dictionary<Worksheet, PdfPageSettings>();
			this.\u1717 = new Dictionary<Worksheet, spr\u1719[]>();
			this.\u1712 = -1f;
			this.\u1713 = null;
			this.\u1714 = null;
			this.\u1715 = null;
			this.\u171C = new Dictionary<long, Metafile>();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00008800 File Offset: 0x00006A00
		private void ᜀ(bool A_0)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u171C != null)
					{
						num = 4;
						continue;
					}
					return;
				case 1:
					num = 2;
					continue;
				case 2:
					if (A_0)
					{
						num = 3;
						continue;
					}
					return;
				case 3:
					goto IL_C1;
				case 4:
				{
					Dictionary<long, Metafile>.ValueCollection.Enumerator enumerator = this.\u171C.Values.GetEnumerator();
					goto IL_5E;
				}
				case 5:
					if (true)
					{
					}
					break;
				case 6:
					return;
				case 7:
					try
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_18E;
							case 1:
								num = 0;
								continue;
							case 4:
							{
								Dictionary<long, Metafile>.ValueCollection.Enumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 1;
									continue;
								}
								Metafile metafile = enumerator.Current;
								metafile.Dispose();
								num = 3;
								continue;
							}
							}
							IL_153:
							num = 4;
							continue;
							goto IL_153;
						}
						IL_18E:
						goto IL_83;
					}
					finally
					{
						Dictionary<long, Metafile>.ValueCollection.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					return;
					IL_83:
					this.\u171C.Clear();
					this.\u171C = null;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5E;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				}
				if (!this.\u171A)
				{
					num = 1;
					continue;
				}
				goto IL_C1;
				IL_5E:
				num = 7;
				continue;
				IL_C1:
				this.\u1716 = null;
				this.ᜋ = null;
				this.\u1717 = null;
				this.\u1712 = -1f;
				this.\u1713 = null;
				this.\u1714 = null;
				this.\u1715 = null;
				this.\u171A = true;
				num = 0;
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000089C0 File Offset: 0x00006BC0
		[Obsolete("Use Convert(PdfConverterSettings converterSettings) instead")]
		public PdfDocument Convert()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜂ();
					int num = 35;
					for (;;)
					{
						Worksheet worksheet;
						int num3;
						int count;
						switch (num)
						{
						case 0:
							if (worksheet.HasPictures)
							{
								num = 31;
								continue;
							}
							goto IL_108;
						case 1:
							goto IL_130;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1E7;
							default:
								if (false)
								{
								}
								this.\u1719 = this.ᜌ.ScalePage.Value;
								num = 1;
								continue;
							}
							break;
						case 3:
							this.ᜀ(this.ᜉ);
							num = 20;
							continue;
						case 4:
							if (!worksheet.IsEmpty)
							{
								num = 26;
								continue;
							}
							goto IL_49C;
						case 5:
							goto IL_49C;
						case 6:
							goto IL_413;
						case 7:
							goto IL_2AA;
						case 8:
							this.ᜀ(this.ᜏ.Workbook, this.ᜉ);
							num = 11;
							continue;
						case 9:
							if (this.ᜏ != null)
							{
								num = 8;
								continue;
							}
							goto IL_55E;
						case 10:
							if (this.ᜆ != null)
							{
								num = 36;
								continue;
							}
							num = 34;
							continue;
						case 11:
							goto IL_2A5;
						case 12:
							if (!this.ᜀ(this.ᜏ.Index))
							{
								num = 21;
								continue;
							}
							goto IL_1AA;
						case 13:
							num = 14;
							continue;
						case 14:
							if (this.ᜌ.ExportDocumentProperties)
							{
								num = 3;
								continue;
							}
							goto IL_158;
						case 15:
							if (!this.ᜌ.ExportBookmarks)
							{
								num = 30;
								continue;
							}
							goto IL_2AA;
						case 16:
							this.\u1718 = spr\u21BD.ᜁ(this.ᜏ);
							this.ᜉ = this.ᜀ(this.ᜏ, this.ᜏ.AllocatedRange);
							this.ᜂ(this.ᜏ.Index);
							num = 19;
							continue;
						case 17:
							if (worksheet.IsEmpty)
							{
								num = 27;
								continue;
							}
							goto IL_108;
						case 18:
						{
							float? num2 = this.ᜌ.ScalePage;
							num = 24;
							continue;
						}
						case 19:
							goto IL_1BF;
						case 20:
							goto IL_52B;
						case 21:
							num = 15;
							continue;
						case 22:
							if (this.ᜁ)
							{
								num = 7;
								continue;
							}
							goto IL_413;
						case 23:
							if (this.ᜌ != null)
							{
								num = 18;
								continue;
							}
							goto IL_130;
						case 24:
						{
							float? num2;
							if (num2 != null)
							{
								num = 2;
								continue;
							}
							goto IL_130;
						}
						case 25:
							goto IL_E4;
						case 26:
							this.ᜉ = this.ᜀ(this.ᜏ, this.ᜏ.AllocatedRange);
							num = 5;
							continue;
						case 27:
							num = 0;
							continue;
						case 28:
							goto IL_49C;
						case 29:
							goto IL_4EB;
						case 30:
							num = 22;
							continue;
						case 31:
							if (true)
							{
							}
							goto IL_1E7;
						case 32:
							if (num3 >= count)
							{
								num = 13;
								continue;
							}
							this.ᜏ = this.ᜆ.Worksheets[num3];
							num = 12;
							continue;
						case 33:
							this.ᜑ = new SizeF(this.ᜉ.PageSettings.Width, this.ᜉ.PageSettings.Height);
							num = 29;
							continue;
						case 34:
							if (!this.ᜀ(this.ᜏ.Index))
							{
								num = 16;
								continue;
							}
							goto IL_1BF;
						case 35:
							if (this.ᜉ != null)
							{
								num = 33;
								continue;
							}
							goto IL_4EB;
						case 36:
							count = this.ᜆ.Worksheets.Count;
							num3 = 0;
							num = 25;
							continue;
						case 37:
							goto IL_E4;
						case 38:
							goto IL_1AA;
						}
						break;
						IL_E4:
						num = 32;
						continue;
						IL_108:
						num = 4;
						continue;
						IL_130:
						num = 10;
						continue;
						IL_1AA:
						num3++;
						num = 37;
						continue;
						IL_1BF:
						num = 9;
						continue;
						IL_1E7:
						this.ᜉ = this.ᜁ(worksheet);
						num = 28;
						continue;
						IL_2AA:
						this.ᜌ.ExportBookmarks = true;
						num = 6;
						continue;
						IL_413:
						worksheet = this.ᜏ;
						this.\u1718 = spr\u21BD.ᜁ(this.ᜏ);
						this.ᜀ(count, this.ᜏ.Index);
						num = 17;
						continue;
						IL_49C:
						this.ᜂ(this.ᜏ.Index);
						num = 38;
						continue;
						IL_4EB:
						num = 23;
					}
				}
				IL_158:
				this.ᜀ(this.ᜆ, this.ᜉ);
				this.ᜀ(false);
				return this.ᜉ;
				IL_2A5:
				goto IL_55E;
				IL_52B:
				goto IL_158;
				IL_55E:
				this.ᜀ(false);
				return this.ᜉ;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00008F38 File Offset: 0x00007138
		public PdfDocument Convert(PdfConverterSettings converterSettings)
		{
			switch (0)
			{
			default:
			{
				PdfDocument templateDocument;
				for (;;)
				{
					this.\u171A = false;
					int num = 0;
					for (;;)
					{
						PdfDocument pdfDocument;
						float num2;
						float num3;
						float left;
						float top;
						float num8;
						float num7;
						float num10;
						float num9;
						PdfMargins margins;
						switch (num)
						{
						case 0:
							if (converterSettings == null)
							{
								num = 1;
								continue;
							}
							goto IL_93E;
						case 1:
							goto IL_911;
						case 2:
							goto IL_939;
						case 3:
							try
							{
								pdfDocument.PageSettings.Margins.ᜀ(0f);
								pdfDocument.PageSettings.Height = num2;
								pdfDocument.PageSettings.Width = num3;
								converterSettings.TemplateDocument = pdfDocument;
								this.ᜉ = pdfDocument;
								this.ᜌ = converterSettings;
								this.Convert();
								MemoryStream memoryStream = new MemoryStream();
								try
								{
									for (;;)
									{
										pdfDocument.SaveToStream(memoryStream);
										PdfDocument pdfDocument2 = new PdfDocument(memoryStream.ToArray());
										float num4 = 1f;
										num = 2;
										for (;;)
										{
											IEnumerator enumerator;
											switch (num)
											{
											case 0:
												try
												{
													num = 11;
													for (;;)
													{
														switch (num)
														{
														case 0:
															goto IL_239;
														case 1:
															goto IL_1B4;
														case 2:
															num = 7;
															continue;
														case 3:
														{
															SizeF size;
															float num5 = num3 / size.Width;
															num = 1;
															continue;
														}
														case 4:
														{
															if (!enumerator.MoveNext())
															{
																num = 2;
																continue;
															}
															PdfPageBase pdfPageBase = (PdfPageBase)enumerator.Current;
															SizeF size = pdfPageBase.Size;
															float num5 = 1f;
															float num6 = 1f;
															num = 10;
															continue;
														}
														case 5:
														{
															float num5;
															num4 = num5;
															num = 6;
															continue;
														}
														case 6:
															goto IL_1FC;
														case 7:
															goto IL_2DC;
														case 9:
														{
															float num5;
															if (num4 > num5)
															{
																num = 5;
																continue;
															}
															goto IL_1FC;
														}
														case 10:
														{
															SizeF size;
															if (size.Width > num3)
															{
																num = 3;
																continue;
															}
															goto IL_1B4;
														}
														case 12:
														{
															float num6;
															num4 = num6;
															num = 8;
															continue;
														}
														case 13:
														{
															SizeF size;
															if (size.Height > num2)
															{
																num = 15;
																continue;
															}
															goto IL_239;
														}
														case 14:
														{
															float num6;
															if (num4 > num6)
															{
																num = 12;
																continue;
															}
															break;
														}
														case 15:
														{
															SizeF size;
															float num6 = num2 / size.Height;
															num = 0;
															continue;
														}
														}
														goto IL_19B;
														IL_1B4:
														num = 13;
														continue;
														IL_1D3:
														num = 4;
														continue;
														IL_19B:
														goto IL_1D3;
														IL_1FC:
														num = 14;
														continue;
														IL_239:
														num = 9;
													}
													IL_2DC:
													goto IL_84E;
												}
												finally
												{
													for (;;)
													{
														IDisposable disposable = enumerator as IDisposable;
														num = 0;
														for (;;)
														{
															switch (num)
															{
															case 0:
																if (disposable != null)
																{
																	num = 2;
																	continue;
																}
																goto IL_329;
															case 1:
																goto IL_327;
															case 2:
																disposable.Dispose();
																num = 1;
																continue;
															}
															break;
														}
													}
													IL_327:
													IL_329:;
												}
												goto Block_7;
											case 1:
												goto IL_88A;
											case 2:
												if (this.FitToPage == FitToPageType.ScaleWithSameFactor)
												{
													num = 3;
													continue;
												}
												goto IL_84E;
											case 3:
												goto IL_82F;
											case 4:
												goto IL_32A;
											}
											break;
											IL_82F:
											enumerator = pdfDocument2.Pages.GetEnumerator();
											num = 0;
											continue;
											Block_7:
											IEnumerator enumerator2;
											int num11;
											int count;
											try
											{
												IL_32A:
												num = 2;
												for (;;)
												{
													PdfNewPage pdfNewPage;
													PdfGraphicsState state;
													float val;
													PdfPageBase pdfPageBase2;
													PdfSection pdfSection4;
													float val2;
													switch (num)
													{
													case 0:
														goto IL_6DC;
													case 1:
														goto IL_645;
													case 4:
													{
														PdfSection pdfSection = templateDocument.Sections.Add();
														pdfSection.PageSettings.Margins.ᜀ(0f);
														pdfNewPage = pdfSection.Pages.Add();
														state = pdfNewPage.Canvas.Save();
														pdfNewPage.Canvas.TranslateTransform(left, top);
														num = 1;
														continue;
													}
													case 5:
													{
														SizeF size2;
														if (size2.Width > num3)
														{
															num = 20;
															continue;
														}
														goto IL_6DC;
													}
													case 6:
														goto IL_5AE;
													case 7:
														num = 18;
														continue;
													case 8:
													{
														SizeF size2;
														if (size2.Height > num2)
														{
															num = 14;
															continue;
														}
														goto IL_5AE;
													}
													case 9:
														pdfNewPage.Canvas.ScaleTransform(num4, num4);
														goto IL_7C4;
													case 10:
														goto IL_645;
													case 11:
														goto IL_645;
													case 12:
														if (num4 != 1f)
														{
															num = 9;
															continue;
														}
														goto IL_645;
													case 13:
														goto IL_645;
													case 14:
													{
														SizeF size2;
														val = num2 / size2.Height;
														num = 6;
														continue;
													}
													case 15:
														if (num4 != 1f)
														{
															num = 19;
															continue;
														}
														goto IL_645;
													case 16:
													{
														FitToPageType fitToPageType;
														switch (fitToPageType)
														{
														case FitToPageType.NoScale:
														{
															SizeF size3 = pdfPageBase2.Size;
															PdfSection pdfSection2 = templateDocument.Sections.Add();
															PdfPageSettings pageSettings = pdfSection2.PageSettings;
															num7 = size3.Width * num8 + pageSettings.Margins.Left + pageSettings.Margins.Right;
															num9 = size3.Height * num10 + pageSettings.Margins.Top + pageSettings.Margins.Bottom;
															pageSettings.Width = num7;
															pageSettings.Height = num9;
															pageSettings.Margins.ᜀ(0f);
															pdfNewPage = pdfSection2.Pages.Add();
															state = pdfNewPage.Canvas.Save();
															pdfNewPage.Canvas.TranslateTransform(left, top);
															num = 10;
															continue;
														}
														case FitToPageType.ScaleWithSameFactor:
														{
															PdfSection pdfSection3 = templateDocument.Sections.Add();
															pdfSection3.PageSettings.Margins.ᜀ(0f);
															pdfNewPage = pdfSection3.Pages.Add();
															state = pdfNewPage.Canvas.Save();
															pdfNewPage.Canvas.TranslateTransform(left, top);
															num = 12;
															continue;
														}
														case FitToPageType.ScaleWidthDifferentFactor:
														{
															SizeF size2 = pdfPageBase2.Size;
															pdfSection4 = templateDocument.Sections.Add();
															pdfSection4.PageSettings.Margins.ᜀ(0f);
															val2 = 1f;
															val = 1f;
															num = 5;
															continue;
														}
														default:
															num = 21;
															continue;
														}
														break;
													}
													case 17:
													{
														if (!enumerator2.MoveNext())
														{
															num = 7;
															continue;
														}
														pdfPageBase2 = (PdfPageBase)enumerator2.Current;
														pdfNewPage = null;
														state = null;
														FitToPageType fitToPageType = this.FitToPage;
														num = 16;
														continue;
													}
													case 18:
														goto IL_7E1;
													case 19:
														switch ((1 == 1) ? 1 : 0)
														{
														case 0:
														case 2:
															goto IL_7C4;
														default:
															if (false)
															{
															}
															pdfNewPage.Canvas.ScaleTransform(num4, num4);
															num = 11;
															continue;
														}
														break;
													case 20:
													{
														SizeF size2;
														val2 = num3 / size2.Width;
														num = 0;
														continue;
													}
													case 21:
														num = 4;
														continue;
													}
													goto IL_394;
													IL_5AE:
													num4 = Math.Min(val2, val);
													pdfNewPage = pdfSection4.Pages.Add();
													state = pdfNewPage.Canvas.Save();
													pdfNewPage.Canvas.TranslateTransform(left, top);
													num = 15;
													continue;
													IL_645:
													pdfNewPage.Canvas.Save();
													pdfNewPage.Canvas.ScaleTransform(num8, num10);
													RectangleF layoutRectangle = new RectangleF(PointF.Empty, pdfPageBase2.Size);
													pdfPageBase2.CreateTemplate().Draw(pdfNewPage, layoutRectangle, converterSettings.EmbedFonts);
													pdfNewPage.Canvas.Restore();
													pdfNewPage.Canvas.Restore(state);
													Worksheet a_ = this.\u1716[num11];
													this.ᜀ(pdfNewPage, a_, margins, num4, num11 + 1, count);
													num11++;
													num = 3;
													continue;
													IL_6DC:
													num = 8;
													continue;
													IL_707:
													num = 17;
													continue;
													IL_394:
													goto IL_707;
													IL_7C4:
													num = 13;
												}
												IL_7E1:
												goto IL_87E;
											}
											finally
											{
												for (;;)
												{
													IDisposable disposable2 = enumerator2 as IDisposable;
													num = 2;
													for (;;)
													{
														switch (num)
														{
														case 0:
															disposable2.Dispose();
															num = 1;
															continue;
														case 1:
															goto IL_82C;
														case 2:
															if (disposable2 != null)
															{
																num = 0;
																continue;
															}
															goto IL_82E;
														}
														break;
													}
												}
												IL_82C:
												IL_82E:;
											}
											goto IL_82F;
											IL_87E:
											num = 1;
											continue;
											IL_84E:
											num11 = 0;
											count = pdfDocument2.Pages.Count;
											enumerator2 = pdfDocument2.Pages.GetEnumerator();
											num = 4;
										}
									}
									IL_88A:;
								}
								finally
								{
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 1:
											goto IL_8C9;
										case 2:
											((IDisposable)memoryStream).Dispose();
											num = 1;
											continue;
										}
										if (memoryStream == null)
										{
											break;
										}
										num = 2;
									}
									IL_8C9:;
								}
								goto IL_61;
							}
							finally
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										((IDisposable)pdfDocument).Dispose();
										num = 1;
										continue;
									case 1:
										goto IL_90E;
									}
									if (pdfDocument == null)
									{
										break;
									}
									num = 0;
								}
								IL_90E:;
							}
							goto IL_911;
							IL_61:
							if (true)
							{
							}
							converterSettings.TemplateDocument = templateDocument;
							num = 4;
							continue;
						case 4:
							if (converterSettings.ExportDocumentProperties)
							{
								num = 5;
								continue;
							}
							goto IL_9F7;
						case 5:
							this.ᜀ(templateDocument);
							num = 2;
							continue;
						case 6:
							goto IL_93E;
						}
						break;
						IL_911:
						converterSettings = PdfConverterSettings.Default;
						num = 6;
						continue;
						IL_93E:
						templateDocument = converterSettings.TemplateDocument;
						margins = templateDocument.PageSettings.Margins;
						left = margins.Left;
						top = margins.Top;
						float right = margins.Right;
						float bottom = margins.Bottom;
						num9 = templateDocument.PageSettings.Height - top - bottom;
						num2 = this.\u170D.ConvertToPixels(num9, this.ᜉ.PageSettings.Unit);
						num10 = num9 / num2;
						num7 = templateDocument.PageSettings.Width - left - right;
						num3 = this.\u170D.ConvertToPixels(num7, this.ᜉ.PageSettings.Unit);
						num8 = num7 / num3;
						pdfDocument = new PdfDocument();
						num = 3;
					}
				}
				IL_939:
				IL_9F7:
				this.ᜀ(this.ᜆ, templateDocument);
				this.ᜀ(true);
				return templateDocument;
			}
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000099B8 File Offset: 0x00007BB8
		private spr\u1719[] ᜂ(Worksheet A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (this.\u1717.ContainsKey(A_0))
				{
					return this.\u1717[A_0];
				}
				break;
			}
			if (true)
			{
			}
			spr\u1719[] array = spr\u1719.ᜀ(A_0);
			this.\u1717[A_0] = array;
			return array;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00009A28 File Offset: 0x00007C28
		private void ᜀ(PdfPageBase A_0, Worksheet A_1, PdfMargins A_2, float A_3, int A_4, int A_5)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					for (;;)
					{
						if (true)
						{
						}
						spr\u1719[] array = this.ᜂ(A_1);
						float num = this.\u170D.ConvertUnits((float)A_1.PageSetup.HeaderMarginInch, PdfGraphicsUnit.Inch, PdfGraphicsUnit.Point);
						float num2 = this.\u170D.ConvertUnits((float)A_1.PageSetup.FooterMarginInch, PdfGraphicsUnit.Inch, PdfGraphicsUnit.Point);
						int num3 = 6;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								if (array[1].ᜂ() != null)
								{
									num3 = 17;
									continue;
								}
								goto IL_E6;
							case 1:
								if (array[0].ᜀ() != null)
								{
									num3 = 3;
									continue;
								}
								goto IL_468;
							case 2:
							{
								float x = A_0.Size.Width - A_2.Right;
								float y = A_0.Size.Height - num2;
								PointF a_ = new PointF(x, y);
								PdfStringFormat a_2 = new PdfStringFormat
								{
									Alignment = PdfTextAlignment.Right,
									LineAlignment = PdfVerticalAlignment.Bottom
								};
								this.ᜀ(A_0.Canvas, a_, array[1].ᜁ(), A_3, A_4, A_5, a_2);
								num3 = 4;
								continue;
							}
							case 3:
							{
								float x2 = (A_0.Size.Width - A_2.Left - A_2.Right) / 2f + A_2.Left;
								float y2 = num;
								PointF a_3 = new PointF(x2, y2);
								PdfStringFormat a_4 = new PdfStringFormat
								{
									Alignment = PdfTextAlignment.Center,
									LineAlignment = PdfVerticalAlignment.Top
								};
								this.ᜀ(A_0.Canvas, a_3, array[0].ᜀ(), A_3, A_4, A_5, a_4);
								num3 = 16;
								continue;
							}
							case 4:
								goto IL_302;
							case 5:
								if (array[1].ᜀ() != null)
								{
									num3 = 9;
									continue;
								}
								goto IL_16B;
							case 6:
								if (array[0].ᜂ() != null)
								{
									num3 = 12;
									continue;
								}
								goto IL_141;
							case 7:
								if (array[1].ᜁ() != null)
								{
									num3 = 2;
									continue;
								}
								return;
							case 8:
								goto IL_141;
							case 9:
							{
								float x3 = (A_0.Size.Width - A_2.Left - A_2.Right) / 2f + A_2.Left;
								float y3 = A_0.Size.Height - num2;
								PointF a_5 = new PointF(x3, y3);
								PdfStringFormat a_6 = new PdfStringFormat
								{
									Alignment = PdfTextAlignment.Center,
									LineAlignment = PdfVerticalAlignment.Bottom
								};
								this.ᜀ(A_0.Canvas, a_5, array[1].ᜀ(), A_3, A_4, A_5, a_6);
								num3 = 11;
								continue;
							}
							case 10:
							{
								float x4 = A_0.Size.Width - A_2.Right;
								float y4 = num;
								PointF a_7 = new PointF(x4, y4);
								PdfStringFormat a_8 = new PdfStringFormat
								{
									Alignment = PdfTextAlignment.Right,
									LineAlignment = PdfVerticalAlignment.Top
								};
								this.ᜀ(A_0.Canvas, a_7, array[0].ᜁ(), A_3, A_4, A_5, a_8);
								num3 = 14;
								continue;
							}
							case 11:
								goto IL_16B;
							case 12:
							{
								float left = A_2.Left;
								float y5 = num;
								PointF a_9 = new PointF(left, y5);
								PdfStringFormat a_10 = new PdfStringFormat
								{
									Alignment = PdfTextAlignment.Left,
									LineAlignment = PdfVerticalAlignment.Top
								};
								this.ᜀ(A_0.Canvas, a_9, array[0].ᜂ(), A_3, A_4, A_5, a_10);
								num3 = 8;
								continue;
							}
							case 13:
								goto IL_E6;
							case 14:
								goto IL_11A;
							case 15:
								if (array[0].ᜁ() != null)
								{
									num3 = 10;
									continue;
								}
								goto IL_11A;
							case 16:
								goto IL_468;
							case 17:
							{
								float left2 = A_2.Left;
								float y6 = A_0.Size.Height - num2;
								PointF a_11 = new PointF(left2, y6);
								PdfStringFormat a_12 = new PdfStringFormat
								{
									Alignment = PdfTextAlignment.Left,
									LineAlignment = PdfVerticalAlignment.Bottom
								};
								this.ᜀ(A_0.Canvas, a_11, array[1].ᜂ(), A_3, A_4, A_5, a_12);
								num3 = 13;
								continue;
							}
							}
							break;
							IL_E6:
							num3 = 5;
							continue;
							IL_11A:
							num3 = 0;
							continue;
							IL_141:
							num3 = 1;
							continue;
							IL_16B:
							num3 = 7;
							continue;
							IL_468:
							num3 = 15;
						}
					}
					IL_302:
					break;
				}
				break;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00009EC8 File Offset: 0x000080C8
		private void ᜀ(PdfCanvas A_0, PointF A_1, sprṶ A_2, float A_3, int A_4, int A_5, PdfStringFormat A_6)
		{
			switch (0)
			{
			default:
			{
				StringBuilder stringBuilder;
				for (;;)
				{
					stringBuilder = new StringBuilder();
					List<sprḧ>.Enumerator enumerator = A_2.ᜀ().GetEnumerator();
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							try
							{
								num = 0;
								for (;;)
								{
									sprᴘ sprᴘ;
									switch (num)
									{
									case 1:
									{
										if (!enumerator.MoveNext())
										{
											num = 2;
											continue;
										}
										sprḧ sprḧ = enumerator.Current;
										num = 11;
										continue;
									}
									case 2:
										num = 9;
										continue;
									case 3:
										goto IL_F9;
									case 4:
										goto IL_F9;
									case 5:
										goto IL_F9;
									case 6:
									{
										sprḧ sprḧ;
										sprᴘ = (sprḧ as sprᴘ);
										HFFieldType hffieldType = sprᴘ.ᜂ();
										num = 8;
										continue;
									}
									case 8:
									{
										HFFieldType hffieldType;
										switch (hffieldType)
										{
										case HFFieldType.PageNumber:
											sprᴘ.ᜀ(A_4.ToString());
											num = 5;
											continue;
										case HFFieldType.NumberOfPages:
											sprᴘ.ᜀ(A_5.ToString());
											num = 3;
											continue;
										default:
											num = 10;
											continue;
										}
										break;
									}
									case 9:
										goto IL_1C4;
									case 10:
										num = 4;
										continue;
									case 11:
									{
										sprḧ sprḧ;
										if (sprḧ is sprᴘ)
										{
											num = 6;
											continue;
										}
										break;
									}
									}
									goto IL_D3;
									IL_F9:
									stringBuilder.Append(sprᴘ.ᜁ());
									num = 7;
									continue;
									IL_111:
									num = 1;
									continue;
									IL_D3:
									goto IL_111;
								}
								IL_1C4:
								goto IL_40;
							}
							finally
							{
								((IDisposable)enumerator).Dispose();
							}
							goto IL_1D7;
							IL_40:
							num = 2;
							continue;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_49;
							}
							goto Block_2;
						case 2:
							goto IL_49;
						}
						break;
						IL_49:
						if (A_3 == 1f)
						{
							goto IL_221;
						}
						num = 1;
					}
				}
				Block_2:
				if (true)
				{
				}
				if (false)
				{
				}
				IL_1D7:
				A_0.Save();
				A_0.TranslateTransform(A_1.X, A_1.Y);
				A_0.ScaleTransform(A_3, A_3);
				A_0.DrawString(stringBuilder.ToString(), this.DefaultPdfFont, PdfBrushes.Black, PointF.Empty, A_6);
				A_0.Restore();
				return;
				IL_221:
				A_0.DrawString(stringBuilder.ToString(), this.DefaultPdfFont, PdfBrushes.Black, A_1, A_6);
				return;
			}
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000A12C File Offset: 0x0000832C
		private PdfPen ᜁ(IBorder A_0)
		{
			switch (0)
			{
			default:
			{
				PdfPen pdfPen;
				for (;;)
				{
					pdfPen = new PdfPen(this.ᜀ(A_0.Color), this.ᜀ(A_0));
					LineStyleType lineStyle = A_0.LineStyle;
					int num = 10;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return pdfPen;
						case 1:
							num = 8;
							continue;
						case 2:
							goto IL_1A9;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_202;
							default:
								goto IL_224;
							}
							break;
						case 4:
							goto IL_1D8;
						case 5:
							goto IL_13A;
						case 6:
							goto IL_169;
						case 7:
							goto IL_122;
						case 8:
							goto IL_17A;
						case 9:
							return pdfPen;
						case 10:
							switch (lineStyle)
							{
							case LineStyleType.Thin:
							case LineStyleType.Medium:
							case LineStyleType.Thick:
							case LineStyleType.Double:
								pdfPen.DashStyle = PdfDashStyle.Solid;
								num = 5;
								continue;
							case LineStyleType.Dashed:
								pdfPen.DashStyle = PdfDashStyle.Custom;
								pdfPen.DashPattern = new float[]
								{
									4f,
									4f
								};
								num = 9;
								continue;
							case LineStyleType.Dotted:
								pdfPen.DashStyle = PdfDashStyle.Custom;
								pdfPen.DashPattern = new float[]
								{
									2f,
									4f
								};
								goto IL_202;
							case LineStyleType.Hair:
								pdfPen.DashStyle = PdfDashStyle.Custom;
								pdfPen.DashPattern = new float[]
								{
									1f,
									4f
								};
								num = 0;
								continue;
							case LineStyleType.MediumDashed:
								pdfPen.DashStyle = PdfDashStyle.Custom;
								pdfPen.DashPattern = new float[]
								{
									6f,
									3f
								};
								num = 7;
								continue;
							case LineStyleType.DashDot:
								pdfPen.DashStyle = PdfDashStyle.Custom;
								pdfPen.DashPattern = new float[]
								{
									6f,
									4f,
									2f,
									4f
								};
								num = 2;
								continue;
							case LineStyleType.MediumDashDot:
								pdfPen.DashStyle = PdfDashStyle.Custom;
								pdfPen.DashPattern = new float[]
								{
									4f,
									2f,
									1f,
									2f
								};
								num = 11;
								continue;
							case LineStyleType.DashDotDot:
								pdfPen.DashStyle = PdfDashStyle.Custom;
								pdfPen.DashPattern = new float[]
								{
									6f,
									4f,
									2f,
									4f,
									2f,
									4f
								};
								num = 6;
								continue;
							case LineStyleType.MediumDashDotDot:
								pdfPen.DashStyle = PdfDashStyle.Custom;
								pdfPen.DashPattern = new float[]
								{
									4f,
									2f,
									1f,
									2f,
									1f,
									2f
								};
								num = 4;
								continue;
							default:
								num = 1;
								continue;
							}
							break;
						case 11:
							goto IL_E8;
						}
						break;
						IL_202:
						num = 3;
					}
				}
				IL_E8:
				IL_122:
				IL_13A:
				IL_169:
				IL_17A:
				IL_1A9:
				IL_1D8:
				return pdfPen;
				IL_224:
				if (false)
				{
				}
				if (true)
				{
				}
				return pdfPen;
			}
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000A3EC File Offset: 0x000085EC
		public void Dispose()
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜆ.Dispose();
					num = 1;
					continue;
				case 1:
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_89;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 3:
					if (true)
					{
					}
					break;
				case 4:
					if (this.ᜆ != null)
					{
						goto IL_89;
					}
					return;
				}
				if (this.\u171B)
				{
					num = 2;
					continue;
				}
				break;
				IL_89:
				num = 0;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000A48C File Offset: 0x0000868C
		private void ᜀ(spr\u192F A_0, RectangleF A_1, PdfCanvas A_2)
		{
			int num = 11;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					PdfBrush brush = new PdfSolidBrush(A_0.ᜰ());
					A_2.DrawRectangle(brush, A_1);
					if (true)
					{
					}
					num = 3;
					continue;
				}
				case 1:
				{
					ExcelPatternType excelPatternType;
					if (excelPatternType != ExcelPatternType.Solid)
					{
						num = 12;
						continue;
					}
					PdfBrush brush = new PdfSolidBrush(this.ᜀ(A_0.ᜰ()));
					A_2.DrawRectangle(brush, A_1);
					num = 7;
					continue;
				}
				case 2:
					goto IL_5D;
				case 3:
					goto IL_EF;
				case 4:
				{
					ExcelPatternType excelPatternType;
					if (excelPatternType != ExcelPatternType.Gradient)
					{
						num = 10;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5D;
					default:
						if (false)
						{
						}
						this.ᜎ.ᜀ(A_0, A_1, A_2);
						num = 6;
						continue;
					}
					break;
				}
				case 5:
					if (A_1.Width > 0f)
					{
						num = 8;
						continue;
					}
					return;
				case 6:
					goto IL_EF;
				case 7:
					goto IL_EF;
				case 8:
				{
					A_0.ᜤ();
					ExcelPatternType excelPatternType = A_0.ᜤ();
					num = 1;
					continue;
				}
				case 9:
					return;
				case 10:
					num = 0;
					continue;
				case 12:
					num = 4;
					continue;
				}
				if (A_1.Height > 0f)
				{
					num = 2;
					continue;
				}
				break;
				IL_5D:
				num = 5;
				continue;
				IL_EF:
				num = 9;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000A628 File Offset: 0x00008828
		private void ᜀ(XlsRange A_0, RectangleF A_1, PdfCanvas A_2)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			spr\u192F spr_u192F = ((A_0.Style as CellStyle).Wrapped as spr\u21A0).Wrapped;
			spr_u192F = this.ᜃ.ᜀ(A_0, spr_u192F);
			spr_u192F = this.\u1718.ᜀ(A_0, spr_u192F);
			this.ᜀ(spr_u192F, A_1, A_2);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000A6A4 File Offset: 0x000088A4
		private void ᜀ(Worksheet A_0, int A_1, int A_2, int A_3, int A_4, PdfCanvas A_5, sprᱥ A_6, sprᱥ A_7, PdfConverter.ᜀ A_8)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					spr\u24F1 spr_u24F = new spr\u24F1(A_0.AppImplementation, A_0);
					int num = A_1;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							int num3;
							if (num3 > 0)
							{
								goto IL_F7;
							}
							goto IL_66;
						}
						case 1:
							goto IL_105;
						case 2:
							goto IL_151;
						case 3:
						{
							if (num > A_3)
							{
								num2 = 9;
								continue;
							}
							int y = A_6.ᜀ(A_1, num - 1);
							int num3 = A_6.ᜀ(num);
							num2 = 0;
							continue;
						}
						case 4:
						{
							int num4 = A_2;
							num2 = 2;
							continue;
						}
						case 5:
							goto IL_105;
						case 6:
							goto IL_151;
						case 7:
							goto IL_66;
						case 8:
						{
							int num4;
							if (num4 > A_4)
							{
								num2 = 7;
								continue;
							}
							int x = A_7.ᜀ(A_2, num4 - 1);
							spr_u24F.ᜀ(num, num4);
							int width = A_7.ᜀ(num4);
							int num3;
							int y;
							RectangleF a_ = new Rectangle(x, y, width, num3);
							A_8(spr_u24F, a_, A_5);
							num4++;
							num2 = 6;
							continue;
						}
						case 9:
							return;
						}
						break;
						IL_66:
						num++;
						num2 = 5;
						continue;
						IL_F7:
						num2 = 4;
						continue;
						IL_105:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F7;
						default:
							if (false)
							{
							}
							num2 = 3;
							continue;
						}
						IL_151:
						if (true)
						{
						}
						num2 = 8;
					}
				}
				return;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000A830 File Offset: 0x00008A30
		private void ᜁ(IBorders A_0, IBorder A_1, float A_2, float A_3, float A_4, float A_5, PdfCanvas A_6, XlsRange A_7)
		{
			for (;;)
			{
				LineStyleType lineStyle = A_1.LineStyle;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_68;
					case 1:
						if (lineStyle != LineStyleType.SlantedDashDot)
						{
							num = 5;
							continue;
						}
						goto IL_80;
					case 2:
						if (lineStyle != LineStyleType.None)
						{
							num = 3;
							continue;
						}
						return;
					case 3:
						if (true)
						{
						}
						num = 6;
						continue;
					case 4:
						num = 1;
						continue;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4C;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 6:
						if (lineStyle != LineStyleType.Double)
						{
							num = 4;
							continue;
						}
						goto IL_4C;
					}
					break;
				}
			}
			IL_4C:
			this.ᜀ(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7);
			return;
			IL_68:
			this.ᜀ(A_1, A_2, A_3, A_4, A_5, A_6);
			return;
			IL_80:
			this.ᜁ(A_1, A_2, A_3, A_4, A_5, A_6);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000A924 File Offset: 0x00008B24
		private void ᜀ(IBorders A_0, RectangleF A_1, PdfCanvas A_2, XlsRange A_3)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IBorder border = A_0[BordersLineType.EdgeLeft];
					this.ᜁ(A_0, border, A_1.Left, A_1.Top, A_1.Left, A_1.Bottom, A_2, A_3);
					border = A_0[BordersLineType.EdgeRight];
					int num = 21;
					for (;;)
					{
						bool flag;
						int lastRow;
						int num2;
						IXLSRange mergeArea;
						switch (num)
						{
						case 0:
						{
							IBorder border2;
							if (border2.LineStyle == LineStyleType.Double)
							{
								num = 3;
								continue;
							}
							goto IL_2DD;
						}
						case 1:
							num = 23;
							continue;
						case 2:
							goto IL_2B0;
						case 3:
							flag = false;
							num = 11;
							continue;
						case 4:
						{
							IBorder border3;
							border = border3;
							goto IL_21B;
						}
						case 5:
							goto IL_13C;
						case 6:
							if (border.ShowDiagonalLine)
							{
								num = 25;
								continue;
							}
							goto IL_2B0;
						case 7:
							lastRow = A_3.LastRow;
							goto IL_38B;
						case 8:
						{
							XlsRange xlsRange = this.ᜏ.Range[num2 + 1, A_3.Column];
							int extendedFormatIndex = (int)xlsRange.ExtendedFormatIndex;
							spr\u192F spr_u192F = xlsRange.Workbook.InnerExtFormats.ᜁ(extendedFormatIndex);
							spr_u192F = this.\u1718.ᜀ(xlsRange, spr_u192F);
							IBorder border4 = spr_u192F.ᜪ()[BordersLineType.EdgeTop];
							num = 24;
							continue;
						}
						case 9:
							if (mergeArea != null)
							{
								num = 13;
								continue;
							}
							if (true)
							{
							}
							num = 7;
							continue;
						case 10:
							if (border.ShowDiagonalLine)
							{
								num = 18;
								continue;
							}
							return;
						case 11:
							goto IL_2DD;
						case 12:
							return;
						case 13:
							num = 30;
							continue;
						case 14:
							num = 22;
							continue;
						case 15:
						{
							IBorder border3;
							if (border3.LineStyle != LineStyleType.None)
							{
								num = 4;
								continue;
							}
							goto IL_13C;
						}
						case 16:
						{
							XlsRange xlsRange2 = this.ᜏ.Range[num2 + 1, A_3.Column];
							int extendedFormatIndex2 = (int)xlsRange2.ExtendedFormatIndex;
							spr\u192F spr_u192F2 = xlsRange2.Workbook.InnerExtFormats.ᜁ(extendedFormatIndex2);
							IBorder border2 = spr_u192F2.ᜪ()[BordersLineType.EdgeTop];
							num = 0;
							continue;
						}
						case 17:
						{
							IBorder border4;
							border = border4;
							num = 19;
							continue;
						}
						case 18:
							this.ᜁ(A_0, border, A_1.Left, A_1.Bottom, A_1.Right, A_1.Top, A_2, A_3);
							num = 12;
							continue;
						case 19:
							goto IL_4FA;
						case 20:
						{
							XlsRange xlsRange3 = this.ᜏ.Range[A_3.Row, A_3.Column + 1];
							int extendedFormatIndex3 = (int)xlsRange3.ExtendedFormatIndex;
							spr\u192F spr_u192F3 = xlsRange3.Workbook.InnerExtFormats.ᜁ(extendedFormatIndex3);
							spr_u192F3 = this.\u1718.ᜀ(xlsRange3, spr_u192F3);
							IBorder border3 = spr_u192F3.ᜪ()[BordersLineType.EdgeLeft];
							num = 15;
							continue;
						}
						case 21:
							if (border.LineStyle == LineStyleType.None)
							{
								num = 14;
								continue;
							}
							goto IL_13C;
						case 22:
							if (A_3.Column == this.ᜐ.LastColumn)
							{
								num = 20;
								continue;
							}
							goto IL_13C;
						case 23:
							if (A_3.Row == this.ᜐ.LastRow)
							{
								num = 8;
								continue;
							}
							goto IL_4FA;
						case 24:
						{
							IBorder border4;
							if (border4.LineStyle != LineStyleType.None)
							{
								num = 17;
								continue;
							}
							goto IL_4FA;
						}
						case 25:
							this.ᜁ(A_0, border, A_1.Left, A_1.Top, A_1.Right, A_1.Bottom, A_2, A_3);
							num = 2;
							continue;
						case 26:
							goto IL_1C2;
						case 27:
							if (flag)
							{
								num = 29;
								continue;
							}
							goto IL_1C2;
						case 28:
							if (border.LineStyle == LineStyleType.None)
							{
								num = 1;
								continue;
							}
							goto IL_4FA;
						case 29:
							border = A_0[BordersLineType.EdgeBottom];
							num = 28;
							continue;
						case 30:
							lastRow = A_3.MergeArea.LastRow;
							goto IL_38B;
						case 31:
							if (num2 < this.ᜐ.LastRow)
							{
								num = 16;
								continue;
							}
							goto IL_2DD;
						}
						break;
						IL_13C:
						this.ᜁ(A_0, border, A_1.Right, A_1.Top, A_1.Right, A_1.Bottom, A_2, A_3);
						border = A_0[BordersLineType.EdgeTop];
						this.ᜁ(A_0, border, A_1.Left, A_1.Top, A_1.Right, A_1.Top, A_2, A_3);
						flag = true;
						mergeArea = A_3.MergeArea;
						num = 9;
						continue;
						IL_1C2:
						border = A_0[BordersLineType.DiagonalDown];
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_21B:
							num = 5;
							continue;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						IL_2B0:
						border = A_0[BordersLineType.DiagonalUp];
						num = 10;
						continue;
						IL_2DD:
						num = 27;
						continue;
						IL_38B:
						num2 = lastRow;
						num = 31;
						continue;
						IL_4FA:
						this.ᜁ(A_0, border, A_1.Left, A_1.Bottom, A_1.Right, A_1.Bottom, A_2, A_3);
						num = 26;
					}
				}
				return;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000AEA8 File Offset: 0x000090A8
		private void ᜀ(spr\u24F1 A_0, RectangleF A_1, RectangleF A_2, PdfCanvas A_3)
		{
			int a_ = 3;
			for (;;)
			{
				IL_09:
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_3 != null)
						{
							goto IL_A2;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_09;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 1:
						goto IL_34;
					case 2:
						goto IL_84;
					}
					if (A_0 == null)
					{
						num = 1;
					}
					else
					{
						num = 0;
					}
				}
			}
			IL_34:
			throw new ArgumentNullException(SheetFinishedEventHandler.b("\ud9b9\ud9bb튽겿", a_));
			IL_84:
			if (true)
			{
			}
			throw new ArgumentNullException(SheetFinishedEventHandler.b("\uddb9캻\udfbd낿꫁귃ꗅ믇", a_));
			IL_A2:
			int extendedFormatIndex = (int)A_0.ExtendedFormatIndex;
			spr\u192F a_2 = A_0.Workbook.InnerExtFormats.ᜁ(extendedFormatIndex);
			this.ᜀ(a_2, A_0, A_1, A_2, A_3);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000AF7C File Offset: 0x0000917C
		private void ᜂ(spr\u192F A_0, XlsRange A_1, string A_2, PdfTrueTypeFont A_3, PdfBrush A_4, RectangleF A_5, RectangleF A_6, PdfStringFormat A_7, PdfCanvas A_8)
		{
			switch (0)
			{
			default:
			{
				int num = 5;
				PdfMetafile pdfMetafile;
				float num2;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_29B;
					default:
					{
						if (false)
						{
						}
						PdfVerticalAlignment lineAlignment;
						switch (num)
						{
						case 0:
						{
							long key;
							pdfMetafile = (PdfMetafile)PdfImage.FromImage(this.\u171C[key]);
							num = 2;
							continue;
						}
						case 1:
							switch (lineAlignment)
							{
							case PdfVerticalAlignment.Middle:
								num2 = (A_6.Height - (float)pdfMetafile.Height) / 2f;
								num = 6;
								continue;
							case PdfVerticalAlignment.Bottom:
								num2 = A_6.Height - (float)pdfMetafile.Height;
								num = 3;
								continue;
							default:
								num = 14;
								continue;
							}
							break;
						case 2:
							goto IL_1D5;
						case 3:
							goto IL_133;
						case 4:
						{
							long key;
							if (this.\u171C.ContainsKey(key))
							{
								num = 0;
								continue;
							}
							pdfMetafile = (PdfMetafile)PdfImage.FromRtf(A_1.RichText.RtfText, A_6.Width * 72f / 96f, PdfImageType.Metafile, A_7);
							num = 12;
							continue;
						}
						case 6:
							goto IL_159;
						case 7:
							goto IL_A2;
						case 8:
							num = 9;
							continue;
						case 9:
							if (A_2.Trim().Length > 0)
							{
								num = 10;
								continue;
							}
							goto IL_29B;
						case 10:
						{
							pdfMetafile = null;
							long key = (long)(A_1.Row | A_1.Column);
							num = 4;
							continue;
						}
						case 11:
							if (A_1.HasRichText)
							{
								num = 8;
								continue;
							}
							goto IL_29B;
						case 12:
							goto IL_1D5;
						case 13:
							return;
						case 14:
							num = 7;
							continue;
						}
						if (true)
						{
						}
						if (string.IsNullOrEmpty(A_2))
						{
							num = 13;
							break;
						}
						num = 11;
						break;
						IL_1D5:
						PdfMetafileLayoutFormat pdfMetafileLayoutFormat = new PdfMetafileLayoutFormat();
						pdfMetafileLayoutFormat.SplitTextLines = true;
						A_8.Save();
						A_8.SetClip(A_6);
						A_8.TranslateTransform(A_6.X, A_6.Y);
						float num3 = 1.3333334f;
						A_8.ScaleTransform(num3, num3);
						num2 = 0f;
						A_0.ᝏ();
						lineAlignment = A_7.LineAlignment;
						num = 1;
						break;
					}
					}
				}
				return;
				IL_A2:
				IL_133:
				IL_159:
				num2 = num2 * 72f / 96f;
				pdfMetafile.Draw(A_8, 0f, num2);
				A_8.Restore();
				return;
				IL_29B:
				A_3 = this.ᜀ(A_3);
				this.ᜁ(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8);
				return;
			}
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000B244 File Offset: 0x00009444
		private void ᜁ(spr\u192F A_0, XlsRange A_1, string A_2, PdfTrueTypeFont A_3, PdfBrush A_4, RectangleF A_5, RectangleF A_6, PdfStringFormat A_7, PdfCanvas A_8)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					A_8.Save();
					A_8.SetClip(A_6);
					int num = 8;
					for (;;)
					{
						float num2;
						RectangleF layoutRectangle;
						float num5;
						SizeF sizeF;
						float num6;
						float num7;
						PdfVerticalAlignment lineAlignment2;
						float num8;
						float num9;
						PdfVerticalAlignment lineAlignment3;
						switch (num)
						{
						case 0:
							goto IL_1CE;
						case 1:
							goto IL_53F;
						case 2:
						{
							SizeF size = A_3.MeasureString(A_2, A_7);
							num = 26;
							continue;
						}
						case 3:
							goto IL_5D6;
						case 4:
							goto IL_195;
						case 5:
							num = 24;
							continue;
						case 6:
						{
							SizeF size;
							float num3;
							num2 = num3 / size.Width;
							float num4 = size.Height * num2;
							layoutRectangle = new RectangleF(PointF.Empty, size);
							num5 = A_6.Height - num4;
							PdfVerticalAlignment lineAlignment = A_7.LineAlignment;
							num = 9;
							continue;
						}
						case 7:
							goto IL_5D6;
						case 8:
						{
							if (true)
							{
							}
							if (A_7.WordWrap == PdfWordWrapType.None)
							{
								num = 10;
								continue;
							}
							sizeF = A_3.MeasureString(A_2, A_6.Width, A_7);
							num6 = 0f;
							num7 = 0f;
							PdfTextAlignment alignment = A_7.Alignment;
							num = 37;
							continue;
						}
						case 9:
						{
							PdfVerticalAlignment lineAlignment;
							switch (lineAlignment)
							{
							case PdfVerticalAlignment.Top:
								num5 = 0f;
								num = 11;
								continue;
							case PdfVerticalAlignment.Middle:
								num5 /= 2f;
								num = 36;
								continue;
							default:
								num = 40;
								continue;
							}
							break;
						}
						case 10:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_3E4;
							default:
							{
								if (false)
								{
								}
								bool flag = false;
								SizeF size = SizeF.Empty;
								float num3 = A_5.Width - this.ᜀ(A_0);
								num = 18;
								continue;
							}
							}
							break;
						case 11:
							goto IL_434;
						case 12:
							switch (lineAlignment2)
							{
							case PdfVerticalAlignment.Top:
								num8 = 0f;
								num = 1;
								continue;
							case PdfVerticalAlignment.Middle:
								num8 = A_6.Height / 2f;
								num = 21;
								continue;
							case PdfVerticalAlignment.Bottom:
								num8 = A_6.Height;
								num = 35;
								continue;
							default:
								num = 5;
								continue;
							}
							break;
						case 13:
						{
							bool flag;
							if (flag)
							{
								num = 6;
								continue;
							}
							num9 = 0f;
							num8 = 0f;
							PdfTextAlignment alignment2 = A_7.Alignment;
							num = 17;
							continue;
						}
						case 14:
							num = 41;
							continue;
						case 15:
							goto IL_434;
						case 16:
							num = 7;
							continue;
						case 17:
						{
							PdfTextAlignment alignment2;
							switch (alignment2)
							{
							case PdfTextAlignment.Left:
							case PdfTextAlignment.Justify:
								num9 = 0f;
								num = 3;
								continue;
							case PdfTextAlignment.Center:
								num9 = A_6.Width / 2f;
								num = 43;
								continue;
							case PdfTextAlignment.Right:
								num9 = A_6.Width;
								num = 28;
								continue;
							default:
								num = 16;
								continue;
							}
							break;
						}
						case 18:
							if (A_0.ᝏ())
							{
								num = 2;
								continue;
							}
							goto IL_36D;
						case 19:
							goto IL_195;
						case 20:
							goto IL_2B2;
						case 21:
							goto IL_53F;
						case 22:
							goto IL_2B2;
						case 23:
							goto IL_195;
						case 24:
							goto IL_53F;
						case 25:
						{
							bool flag = true;
							num = 32;
							continue;
						}
						case 26:
							if (A_5.Left < A_6.Left)
							{
								num = 29;
								continue;
							}
							goto IL_1CE;
						case 27:
							goto IL_2B2;
						case 28:
							goto IL_5D6;
						case 29:
						{
							float num3;
							num3 -= A_6.Left - A_5.Left;
							num = 0;
							continue;
						}
						case 30:
						{
							SizeF size;
							float num3;
							if (size.Width > num3)
							{
								num = 25;
								continue;
							}
							goto IL_36D;
						}
						case 31:
							goto IL_576;
						case 32:
							goto IL_36D;
						case 33:
							goto IL_473;
						case 34:
							switch (lineAlignment3)
							{
							case PdfVerticalAlignment.Top:
								num7 = 0f;
								num = 20;
								continue;
							case PdfVerticalAlignment.Middle:
								num7 = (A_6.Height - sizeF.Height) / 2f;
								num = 22;
								continue;
							case PdfVerticalAlignment.Bottom:
								num7 = A_6.Height - sizeF.Height;
								num = 27;
								continue;
							default:
								num = 14;
								continue;
							}
							break;
						case 35:
							goto IL_53F;
						case 36:
							goto IL_434;
						case 37:
						{
							PdfTextAlignment alignment;
							switch (alignment)
							{
							case PdfTextAlignment.Left:
							case PdfTextAlignment.Justify:
								num6 = 0f;
								num = 38;
								continue;
							case PdfTextAlignment.Center:
								num6 = (A_6.Width - sizeF.Width) / 2f;
								num = 23;
								continue;
							case PdfTextAlignment.Right:
								num6 = A_6.Width - sizeF.Width;
								num = 19;
								continue;
							default:
								num = 42;
								continue;
							}
							break;
						}
						case 38:
							goto IL_3E4;
						case 39:
							goto IL_2F7;
						case 40:
							num = 15;
							continue;
						case 41:
							goto IL_2B2;
						case 42:
							num = 4;
							continue;
						case 43:
							goto IL_5D6;
						}
						break;
						IL_195:
						lineAlignment3 = A_7.LineAlignment;
						num = 34;
						continue;
						IL_3E4:
						goto IL_195;
						IL_1CE:
						num = 30;
						continue;
						IL_2B2:
						RectangleF layoutRectangle2 = new RectangleF(num6 + A_6.X, num7 + A_6.Y, sizeF.Width, sizeF.Height);
						A_8.DrawString(A_2, A_3, A_4, layoutRectangle2, A_7);
						num = 39;
						continue;
						IL_36D:
						num = 13;
						continue;
						IL_434:
						num5 = A_6.Top + num5;
						A_8.TranslateTransform(A_6.Left, num5);
						A_8.ScaleTransform(num2, num2);
						A_8.DrawString(A_2, A_3, A_4, layoutRectangle);
						num = 33;
						continue;
						IL_53F:
						PointF point = new PointF(A_6.X + num9, A_6.Y + num8);
						A_8.DrawString(A_2, A_3, A_4, point, A_7);
						num = 31;
						continue;
						IL_5D6:
						lineAlignment2 = A_7.LineAlignment;
						num = 12;
					}
				}
				IL_2F7:
				IL_473:
				IL_576:
				A_8.Restore();
				return;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000B8B4 File Offset: 0x00009AB4
		private void ᜀ(spr\u192F A_0, XlsRange A_1, string A_2, PdfTrueTypeFont A_3, PdfBrush A_4, RectangleF A_5, RectangleF A_6, PdfStringFormat A_7, PdfCanvas A_8)
		{
			switch (0)
			{
			default:
			{
				int num = 11;
				float x;
				PdfTemplate pdfTemplate;
				float y;
				for (;;)
				{
					PdfTextAlignment alignment;
					float width;
					PdfMetafile pdfMetafile;
					float num3;
					RectangleF rectangleF;
					PdfVerticalAlignment lineAlignment;
					float height;
					switch (num)
					{
					case 0:
						goto IL_42D;
					case 1:
						if (A_0.\u171B() > 90)
						{
							num = 6;
							continue;
						}
						num = 12;
						continue;
					case 2:
						switch (alignment)
						{
						case PdfTextAlignment.Left:
						case PdfTextAlignment.Justify:
							x = 0f;
							num = 18;
							continue;
						case PdfTextAlignment.Center:
							x = (A_6.Width - width) / 2f;
							num = 24;
							continue;
						case PdfTextAlignment.Right:
							x = A_6.Width - width;
							num = 23;
							continue;
						default:
							num = 10;
							continue;
						}
						break;
					case 3:
					{
						float num2 = 1.3333334f;
						pdfTemplate.Graphics.ScaleTransform(num2, num2);
						pdfMetafile.Draw(pdfTemplate.Graphics);
						num = 8;
						continue;
					}
					case 4:
						goto IL_349;
					case 5:
						goto IL_C4;
					case 6:
						num = 7;
						continue;
					case 7:
						num3 = (float)(A_0.\u171B() - 90);
						goto IL_234;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_289;
						default:
							if (false)
							{
							}
							goto IL_10B;
						}
						break;
					case 9:
						num = 14;
						continue;
					case 10:
						num = 5;
						continue;
					case 12:
						num3 = (float)(-(float)A_0.\u171B());
						goto IL_234;
					case 13:
						goto IL_344;
					case 14:
						if (A_2.Trim().Length > 0)
						{
							num = 20;
							continue;
						}
						goto IL_3AD;
					case 15:
						goto IL_349;
					case 16:
						goto IL_1A9;
					case 17:
						if (pdfMetafile != null)
						{
							num = 3;
							continue;
						}
						if (true)
						{
						}
						pdfTemplate.Graphics.DrawString(A_2, A_3, A_4, rectangleF, A_7);
						num = 19;
						continue;
					case 18:
						goto IL_C4;
					case 19:
						goto IL_10B;
					case 20:
						goto IL_289;
					case 21:
						switch (lineAlignment)
						{
						case PdfVerticalAlignment.Top:
							y = 0f;
							num = 26;
							continue;
						case PdfVerticalAlignment.Middle:
							y = (A_6.Height - height) / 2f;
							num = 13;
							continue;
						case PdfVerticalAlignment.Bottom:
							y = A_6.Height - height;
							num = 0;
							continue;
						default:
							num = 25;
							continue;
						}
						break;
					case 22:
						return;
					case 23:
						goto IL_C4;
					case 24:
						goto IL_C4;
					case 25:
						num = 16;
						continue;
					case 26:
						goto IL_3F6;
					case 27:
						if (A_1.HasRichText)
						{
							num = 9;
							continue;
						}
						goto IL_3AD;
					}
					if (string.IsNullOrEmpty(A_2))
					{
						num = 22;
						continue;
					}
					A_8.Save();
					A_8.SetClip(A_6);
					A_8.TranslateTransform(A_6.X, A_6.Y);
					pdfMetafile = null;
					rectangleF = default(RectangleF);
					num = 1;
					continue;
					IL_C4:
					lineAlignment = A_7.LineAlignment;
					num = 21;
					continue;
					IL_10B:
					width = pdfTemplate.Width;
					height = pdfTemplate.Height;
					x = 0f;
					y = 0f;
					alignment = A_7.Alignment;
					num = 2;
					continue;
					IL_234:
					float num4 = num3;
					float width2 = (float)((double)A_6.Height / Math.Sin((double)Math.Abs(num4) * 3.141592653589793 / 180.0));
					num = 27;
					continue;
					IL_289:
					string rtfText = A_1.RichText.RtfText;
					float num5 = 1.3333334f;
					width2 = A_6.Width / num5;
					pdfMetafile = (PdfMetafile)PdfImage.FromRtf(rtfText, width2, PdfImageType.Metafile, A_7);
					rectangleF = this.ᜀ(pdfMetafile.InternalImage, A_7);
					num = 15;
					continue;
					IL_349:
					RectangleF rectangleF2 = this.ᜀ(rectangleF, num4);
					pdfTemplate = new PdfTemplate(rectangleF2.Size);
					pdfTemplate.Graphics.TranslateTransform(-rectangleF2.X, -rectangleF2.Y);
					pdfTemplate.Graphics.RotateTransform(num4);
					num = 17;
					continue;
					IL_3AD:
					A_3 = this.ᜀ(A_3);
					SizeF size = A_3.MeasureString(A_2, width2, A_7);
					rectangleF = new RectangleF(PointF.Empty, size);
					num = 4;
				}
				return;
				IL_1A9:
				IL_344:
				IL_3F6:
				IL_42D:
				PointF location = new PointF(x, y);
				pdfTemplate.Draw(A_8, location);
				A_8.Restore();
				return;
			}
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000BD68 File Offset: 0x00009F68
		private void ᜀ(spr\u192F A_0, XlsRange A_1, RectangleF A_2, RectangleF A_3, PdfCanvas A_4)
		{
			int a_ = 7;
			switch (0)
			{
			default:
			{
				PdfBrush a_3;
				for (;;)
				{
					IL_17:
					for (;;)
					{
						A_0 = this.ᜃ.ᜀ(A_1, A_0);
						A_0 = this.\u1718.ᜀ(A_1, A_0);
						int num = 15;
						for (;;)
						{
							string text;
							PdfTrueTypeFont a_2;
							PdfStringFormat pdfStringFormat;
							IFont font;
							switch (num)
							{
							case 0:
								num = 18;
								continue;
							case 1:
								A_3.Width -= this.ᜂ;
								A_3.Height -= this.ᜂ;
								num = 25;
								continue;
							case 2:
								this.ᜀ(A_1, A_3);
								this.ᜂ(A_0, A_1, text, a_2, a_3, A_2, A_3, pdfStringFormat, A_4);
								num = 5;
								continue;
							case 3:
								num = 6;
								continue;
							case 4:
								if (pdfStringFormat.Alignment == PdfTextAlignment.Right)
								{
									num = 23;
									continue;
								}
								A_3.X += this.ᜂ;
								A_3.Height -= this.ᜂ;
								this.ᜂ(A_0, A_1, text, a_2, a_3, A_2, A_3, pdfStringFormat, A_4);
								num = 17;
								continue;
							case 5:
								goto IL_2D3;
							case 6:
								if (pdfStringFormat.Alignment == PdfTextAlignment.Right)
								{
									num = 1;
									continue;
								}
								A_3.X += this.ᜂ;
								A_3.Height -= this.ᜂ;
								num = 24;
								continue;
							case 7:
								a_2 = new PdfTrueTypeFont(font.GenerateNativeFont(), this.ᜌ.EmbedFonts);
								num = 21;
								continue;
							case 8:
								if (!string.IsNullOrEmpty(text))
								{
									num = 7;
									continue;
								}
								goto IL_4D2;
							case 9:
								if (A_0.\u171B() != 255)
								{
									num = 12;
									continue;
								}
								goto IL_185;
							case 10:
							{
								HorizontalAlignType horizontalAlignType;
								switch (horizontalAlignType)
								{
								case HorizontalAlignType.Left:
								{
									float num2;
									A_3 = new RectangleF(A_3.Left + num2, A_3.Top, A_3.Width - num2, A_3.Height);
									num = 22;
									continue;
								}
								case HorizontalAlignType.Center:
									goto IL_DA;
								case HorizontalAlignType.Right:
								{
									float num2;
									A_3 = new RectangleF(A_3.Left, A_3.Top, A_3.Width - num2, A_3.Height);
									num = 13;
									continue;
								}
								default:
									num = 0;
									continue;
								}
								break;
							}
							case 11:
								if (A_0.\u171B() != 0)
								{
									num = 3;
									continue;
								}
								goto IL_185;
							case 12:
								num = 11;
								continue;
							case 13:
								goto IL_DA;
							case 14:
								goto IL_270;
							case 15:
								if (A_0.\u171A() > 0)
								{
									num = 16;
									continue;
								}
								goto IL_DA;
							case 16:
							{
								float num2 = this.ᜀ(A_1, A_0.\u171A());
								HorizontalAlignType horizontalAlignType = A_0.ᜋ();
								num = 10;
								continue;
							}
							case 17:
								goto IL_488;
							case 18:
								goto IL_DA;
							case 19:
							{
								PdfStringFormat pdfStringFormat2 = pdfStringFormat;
								if (A_1.Style == null)
								{
									goto IL_3CF;
								}
								if (A_1.Style.WrapText)
								{
									goto IL_3CF;
								}
								PdfWordWrapType wordWrap = PdfWordWrapType.None;
								IL_3D2:
								pdfStringFormat2.WordWrap = wordWrap;
								text = A_1.NumberText;
								text = this.ᜁ(text, A_0.\u171B());
								num = 8;
								continue;
								IL_3CF:
								wordWrap = PdfWordWrapType.Word;
								goto IL_3D2;
							}
							case 20:
								goto IL_224;
							case 21:
								if (A_1.CellStyleName == SheetFinishedEventHandler.b("릿닁ꇃ듅꓇ꏉꋋꗍ", a_))
								{
									num = 2;
									continue;
								}
								num = 9;
								continue;
							case 22:
								goto IL_DA;
							case 23:
								A_3.Width -= this.ᜂ;
								A_3.Height -= this.ᜂ;
								this.ᜂ(A_0, A_1, text, a_2, a_3, A_2, A_3, pdfStringFormat, A_4);
								num = 14;
								continue;
							case 24:
								goto IL_1E9;
							case 25:
								goto IL_1E9;
							}
							break;
							IL_DA:
							font = A_0.ᜀ();
							a_3 = new PdfSolidBrush(this.ᜀ(font.Color));
							pdfStringFormat = new PdfStringFormat();
							pdfStringFormat.Alignment = this.ᜀ(A_0, A_1);
							pdfStringFormat.LineAlignment = this.ᜀ(A_0);
							pdfStringFormat.LineLimit = false;
							pdfStringFormat.NoClip = true;
							num = 19;
							continue;
							IL_185:
							if (true)
							{
							}
							num = 4;
							continue;
							IL_1E9:
							this.ᜀ(A_0, A_1, text, a_2, a_3, A_2, A_3, pdfStringFormat, A_4);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_17;
							default:
								if (false)
								{
								}
								num = 20;
								break;
							}
						}
					}
				}
				IL_224:
				IL_270:
				IL_2D3:
				IL_488:
				IL_4D2:
				a_3 = null;
				this.ᜀ(A_0.ᜪ(), A_2, A_4, A_1);
				return;
			}
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000C25C File Offset: 0x0000A45C
		private void ᜀ(Worksheet A_0, int A_1, int A_2, int A_3, int A_4, PdfCanvas A_5, sprᱥ A_6, sprᱥ A_7)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_53:
					spr\u24F1 spr_u24F = new spr\u24F1(A_0.AppImplementation, A_0);
					spr\u24F1 a_ = new spr\u24F1(A_0.AppImplementation, A_0);
					int num = A_1;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_EE:
						num2 = 3;
						break;
					default:
						if (false)
						{
						}
						num2 = 13;
						break;
					}
					int x;
					int num4;
					int num5;
					int y;
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
							if (num3 > A_4)
							{
								num2 = 2;
								continue;
							}
							x = A_7.ᜀ(A_2, num3 - 1);
							spr_u24F.ᜀ(num, num3);
							num2 = 9;
							continue;
						case 1:
							num3 = A_2;
							num2 = 7;
							continue;
						case 2:
							goto IL_117;
						case 3:
							goto IL_166;
						case 4:
							goto IL_17D;
						case 5:
							return;
						case 6:
							if (num4 > 0)
							{
								num2 = 8;
								continue;
							}
							goto IL_166;
						case 7:
							goto IL_9C;
						case 8:
							goto IL_1C7;
						case 9:
							if (!spr_u24F.HasMerged)
							{
								num2 = 14;
								continue;
							}
							goto IL_166;
						case 10:
							if (num5 > 0)
							{
								num2 = 1;
								continue;
							}
							goto IL_117;
						case 11:
							if (num > A_3)
							{
								num2 = 5;
								continue;
							}
							if (true)
							{
							}
							y = A_6.ᜀ(A_1, num - 1);
							num5 = A_6.ᜀ(num);
							num2 = 10;
							continue;
						case 12:
							goto IL_9C;
						case 13:
							goto IL_17D;
						case 14:
							num4 = A_7.ᜀ(num3);
							num2 = 6;
							continue;
						}
						goto IL_53;
						IL_9C:
						num2 = 0;
						continue;
						IL_117:
						num++;
						num2 = 4;
						continue;
						IL_166:
						num3++;
						num2 = 12;
						continue;
						IL_17D:
						num2 = 11;
					}
					IL_1C7:
					RectangleF a_2 = new Rectangle(x, y, num4, num5);
					RectangleF a_3 = this.ᜀ(spr_u24F, a_2, A_7, A_2, a_);
					this.ᜀ(spr_u24F, a_2, a_3, A_5);
					goto IL_EE;
				}
				return;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000C474 File Offset: 0x0000A674
		private void ᜀ(IBorders A_0, IBorder A_1, float A_2, float A_3, float A_4, float A_5, PdfCanvas A_6, XlsRange A_7)
		{
			PdfPen a_;
			BordersLineType bordersLineType;
			int a_2;
			int a_3;
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
					for (;;)
					{
						a_ = this.ᜁ(A_1);
						bordersLineType = (A_1 as XlsBorder).BorderIndex;
						BordersLineType bordersLineType2 = bordersLineType;
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								switch (bordersLineType2)
								{
								case BordersLineType.EdgeLeft:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_00;
									default:
										if (false)
										{
										}
										a_2 = -1;
										a_3 = 0;
										num = 3;
										continue;
									}
									break;
								case BordersLineType.EdgeTop:
									a_2 = 0;
									a_3 = -1;
									num = 6;
									continue;
								case BordersLineType.EdgeBottom:
									a_2 = 0;
									a_3 = 1;
									num = 4;
									continue;
								case BordersLineType.EdgeRight:
									a_2 = 1;
									a_3 = 0;
									num = 1;
									continue;
								default:
									num = 5;
									continue;
								}
								break;
							case 1:
								goto IL_88;
							case 2:
								goto IL_CF;
							case 3:
								goto IL_FD;
							case 4:
								goto IL_10F;
							case 5:
								num = 7;
								continue;
							case 6:
								goto IL_9D;
							case 7:
								if (true)
								{
								}
								a_2 = 1;
								a_3 = 1;
								num = 2;
								continue;
							}
							break;
						}
					}
					break;
				}
			}
			IL_88:
			IL_9D:
			IL_CF:
			IL_FD:
			IL_10F:
			this.ᜁ(A_6, a_, A_0, bordersLineType, A_2, A_3, A_4, A_5, a_2, a_3, A_7);
			this.ᜀ(A_6, a_, A_0, bordersLineType, A_2, A_3, A_4, A_5, a_2, a_3, A_7);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
		private void ᜀ(PdfDocument A_0, Worksheet A_1, float A_2, float A_3)
		{
			int a_ = 2;
			for (;;)
			{
				RectangleF bounds = default(RectangleF);
				int num = 48;
				for (;;)
				{
					Dictionary<string, string> dictionary;
					switch (num)
					{
					case 0:
						if (!(A_1.PageSetup.LeftHeader != ""))
						{
							num = 14;
							continue;
						}
						goto IL_6D0;
					case 1:
						return;
					case 2:
						goto IL_530;
					case 3:
						goto IL_764;
					case 4:
						if (A_1.PageSetup.RightFooter != "")
						{
							num = 43;
							continue;
						}
						return;
					case 5:
						num = 42;
						continue;
					case 6:
						if (A_1.PageSetup.CenterFooter != "")
						{
							num = 27;
							continue;
						}
						goto IL_1F7;
					case 7:
						goto IL_4C9;
					case 8:
						goto IL_12F;
					case 9:
						if (A_1.PageSetup.CenterHeaderImage == null)
						{
							num = 21;
							continue;
						}
						goto IL_42F;
					case 10:
						dictionary.Add(SheetFinishedEventHandler.b("視", a_), A_1.PageSetup.CenterHeader);
						num = 24;
						continue;
					case 11:
						goto IL_465;
					case 12:
						if (A_1.PageSetup.RightHeaderImage != null)
						{
							num = 50;
							continue;
						}
						goto IL_55B;
					case 13:
						if (A_1.PageSetup.RightHeader != "")
						{
							num = 49;
							continue;
						}
						goto IL_530;
					case 14:
						num = 44;
						continue;
					case 15:
						num = 9;
						continue;
					case 16:
						if (A_1.PageSetup.LeftHeaderImage == null)
						{
							num = 29;
							continue;
						}
						goto IL_3F1;
					case 17:
						num = 13;
						continue;
					case 18:
						dictionary.Add(SheetFinishedEventHandler.b("", a_), A_1.PageSetup.LeftHeader);
						num = 7;
						continue;
					case 19:
						if (A_1.PageSetup.RightFooter == null)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2B1;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					case 20:
						if (A_1.PageSetup.LeftFooterImage == null)
						{
							num = 51;
							continue;
						}
						goto IL_3F1;
					case 21:
						num = 12;
						continue;
					case 22:
						if (A_1.PageSetup.LeftFooter == null)
						{
							num = 35;
							continue;
						}
						goto IL_12F;
					case 23:
						num = 30;
						continue;
					case 24:
						goto IL_4FB;
					case 25:
						if (A_1.PageSetup.RightFooter != "")
						{
							num = 32;
							continue;
						}
						goto IL_465;
					case 26:
						if (A_1.PageSetup.LeftHeaderImage == null)
						{
							num = 15;
							continue;
						}
						goto IL_42F;
					case 27:
						dictionary.Add(SheetFinishedEventHandler.b("視", a_), A_1.PageSetup.CenterFooter);
						num = 55;
						continue;
					case 28:
						dictionary.Add(SheetFinishedEventHandler.b("", a_), A_1.PageSetup.RightHeader);
						num = 41;
						continue;
					case 29:
						num = 20;
						continue;
					case 30:
						if (A_1.PageSetup.RightFooterImage != null)
						{
							num = 53;
							continue;
						}
						goto IL_1CC;
					case 31:
						dictionary.Add(SheetFinishedEventHandler.b("", a_), A_1.PageSetup.LeftFooter);
						num = 3;
						continue;
					case 32:
						dictionary.Add(SheetFinishedEventHandler.b("", a_), A_1.PageSetup.RightFooter);
						num = 11;
						continue;
					case 33:
						if (A_1.PageSetup.CenterFooterImage == null)
						{
							num = 23;
							continue;
						}
						goto IL_3F1;
					case 34:
						num = 45;
						continue;
					case 35:
						num = 47;
						continue;
					case 36:
						num = 16;
						continue;
					case 37:
						goto IL_1CC;
					case 38:
						if (A_1.PageSetup.RightHeader != "")
						{
							num = 28;
							continue;
						}
						goto IL_700;
					case 39:
						num = 19;
						continue;
					case 40:
						if (A_1.PageSetup.LeftHeader != "")
						{
							num = 18;
							continue;
						}
						goto IL_4C9;
					case 41:
						goto IL_700;
					case 42:
						goto IL_2B1;
					case 43:
						goto IL_36A;
					case 44:
						if (!(A_1.PageSetup.CenterHeader != ""))
						{
							num = 17;
							continue;
						}
						goto IL_6D0;
					case 45:
						if (A_1.PageSetup.CenterHeaderImage == null)
						{
							num = 36;
							continue;
						}
						goto IL_3F1;
					case 46:
						if (A_1.PageSetup.CenterHeader != "")
						{
							num = 10;
							continue;
						}
						goto IL_4FB;
					case 47:
						if (A_1.PageSetup.CenterFooter == null)
						{
							num = 39;
							continue;
						}
						goto IL_12F;
					case 48:
						if (A_1.PageSetup.RightHeaderImage == null)
						{
							num = 34;
							continue;
						}
						goto IL_3F1;
					case 49:
						goto IL_6D0;
					case 50:
						goto IL_42F;
					case 51:
						if (true)
						{
						}
						num = 33;
						continue;
					case 52:
						num = 4;
						continue;
					case 53:
						goto IL_3F1;
					case 54:
						if (A_1.PageSetup.LeftFooter != "")
						{
							num = 31;
							continue;
						}
						goto IL_764;
					case 55:
						goto IL_1F7;
					case 56:
						goto IL_55B;
					case 57:
						if (!(A_1.PageSetup.LeftFooter != ""))
						{
							num = 5;
							continue;
						}
						goto IL_36A;
					}
					break;
					IL_12F:
					bounds = new RectangleF(0f, 0f, this.ᜉ.PageSettings.Width, 50f);
					PdfPageTemplateElement a_2 = new PdfPageTemplateElement(bounds);
					dictionary = new Dictionary<string, string>();
					num = 54;
					continue;
					IL_1CC:
					num = 26;
					continue;
					IL_1F7:
					num = 25;
					continue;
					IL_2B1:
					if (!(A_1.PageSetup.CenterFooter != ""))
					{
						num = 52;
						continue;
					}
					IL_36A:
					this.ᜀ(dictionary, this.ᜉ, A_1, A_2, a_2, SheetFinishedEventHandler.b("ﾸ풺튼쮾꓀뇂", a_), A_3);
					num = 1;
					continue;
					IL_3F1:
					this.ᜀ();
					num = 37;
					continue;
					IL_42F:
					bounds = new RectangleF(0f, 0f, this.ᜉ.PageSettings.Width, 50f);
					num = 56;
					continue;
					IL_465:
					num = 57;
					continue;
					IL_4C9:
					num = 46;
					continue;
					IL_4FB:
					num = 38;
					continue;
					IL_530:
					num = 22;
					continue;
					IL_55B:
					a_2 = new PdfPageTemplateElement(bounds);
					dictionary = new Dictionary<string, string>();
					num = 40;
					continue;
					IL_6D0:
					this.ᜀ(dictionary, this.ᜉ, A_1, A_2, a_2, SheetFinishedEventHandler.b("\udeba\udcbc\udbbe꓀뇂", a_), A_3);
					num = 2;
					continue;
					IL_700:
					num = 0;
					continue;
					IL_764:
					num = 6;
				}
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000CD70 File Offset: 0x0000AF70
		private PdfDocument ᜁ(Worksheet A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					sprᱥ a_ = new sprᱥ(new sprᱥ.ᜀ(A_0.GetRowHeightPixels));
					sprᱥ a_2 = new sprᱥ(new sprᱥ.ᜀ(A_0.GetColumnWidthPixels));
					PdfPageSettings pageSettings = this.ᜉ.PageSettings;
					CellRange a_3 = A_0.AllocatedRange;
					int num = 2;
					for (;;)
					{
						int num2;
						int num3;
						IPictures pictures;
						int num4;
						int count;
						int[] array;
						float num5;
						float num6;
						int num7;
						float x;
						float y;
						int num9;
						switch (num)
						{
						case 0:
							goto IL_33F;
						case 1:
							goto IL_29A;
						case 2:
							if (this.FitToPage != FitToPageType.None)
							{
								num = 20;
								continue;
							}
							goto IL_474;
						case 3:
							goto IL_26C;
						case 4:
							num2 = 1;
							num = 21;
							continue;
						case 5:
							if (num3 >= pictures.Count)
							{
								num = 4;
								continue;
							}
							num = 15;
							continue;
						case 6:
						{
							if (num4 >= count)
							{
								num = 18;
								continue;
							}
							IPictureShape pictureShape = pictures[num4];
							array[num4] = (this.ᜀ(pictureShape).Y / (int)num5 + 1) * (pictureShape.Width / (int)num6 + 1);
							num4++;
							num = 23;
							continue;
						}
						case 7:
							goto IL_1F1;
						case 8:
							num = 17;
							continue;
						case 9:
							if (num7 >= pictures.Count)
							{
								num = 22;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
							{
								if (false)
								{
								}
								IPictureShape pictureShape2 = pictures[num7];
								Point point = this.ᜀ(pictureShape2);
								x = (float)(point.X - point.X / (int)num6 * (int)num6);
								y = (float)(point.Y - point.Y / (int)num5 * (int)num5);
								PdfImage image = new PdfBitmap(pictureShape2.Picture);
								num = 11;
								continue;
							}
							}
							break;
						case 10:
						{
							PdfPageBase pdfPageBase;
							PdfCanvas canvas = pdfPageBase.Canvas;
							IPictureShape pictureShape2;
							PdfImage image;
							canvas.DrawImage(image, new RectangleF(x, y, (float)pictureShape2.Width, (float)pictureShape2.Height));
							num = 24;
							continue;
						}
						case 11:
							if (array[num7] == num2)
							{
								num = 8;
								continue;
							}
							goto IL_374;
						case 12:
						{
							int num8 = array[num3];
							num = 31;
							continue;
						}
						case 13:
							goto IL_374;
						case 14:
						{
							PdfPageBase pdfPageBase = this.ᜁ();
							num = 7;
							continue;
						}
						case 15:
						{
							int num8;
							if (num8 < array[num3])
							{
								num = 12;
								continue;
							}
							goto IL_328;
						}
						case 16:
							goto IL_474;
						case 17:
						{
							if (num9 == 0)
							{
								num = 10;
								continue;
							}
							IPictureShape pictureShape2;
							PdfImage image;
							PdfCanvas canvas;
							canvas.DrawImage(image, new RectangleF(x, y, (float)pictureShape2.Width, (float)pictureShape2.Height));
							num = 26;
							continue;
						}
						case 18:
						{
							int num8 = array[0];
							num3 = 1;
							num = 28;
							continue;
						}
						case 19:
							goto IL_2ED;
						case 20:
							a_3 = this.ᜀ(A_0, a_3, a_, a_2);
							pageSettings = this.ᜄ.PageSettings;
							num = 16;
							continue;
						case 21:
							goto IL_26C;
						case 22:
							num = 29;
							continue;
						case 23:
							goto IL_2ED;
						case 24:
							goto IL_F9;
						case 25:
							goto IL_33F;
						case 26:
							goto IL_F9;
						case 27:
						{
							if (true)
							{
							}
							int num8;
							if (num2 > num8)
							{
								num = 30;
								continue;
							}
							PdfPageBase pdfPageBase = this.ᜁ();
							PdfCanvas canvas = pdfPageBase.Canvas;
							num9 = 0;
							num7 = 0;
							num = 25;
							continue;
						}
						case 28:
							goto IL_29A;
						case 29:
							if (num9 == 0)
							{
								num = 14;
								continue;
							}
							goto IL_1F1;
						case 30:
							goto IL_295;
						case 31:
							goto IL_328;
						}
						break;
						IL_F9:
						num9++;
						num = 13;
						continue;
						IL_1F1:
						num2++;
						num = 3;
						continue;
						IL_26C:
						num = 27;
						continue;
						IL_29A:
						num = 5;
						continue;
						IL_2ED:
						num = 6;
						continue;
						IL_328:
						num3++;
						num = 1;
						continue;
						IL_33F:
						num = 9;
						continue;
						IL_374:
						num7++;
						num = 0;
						continue;
						IL_474:
						float num10 = pageSettings.Height - (pageSettings.Margins.Top + pageSettings.Margins.Bottom);
						float num11 = pageSettings.Width - (pageSettings.Margins.Left + pageSettings.Margins.Right);
						pictures = A_0.Pictures;
						num5 = num10;
						num6 = num11;
						x = 0f;
						y = 0f;
						array = new int[100];
						num4 = 0;
						count = pictures.Count;
						num = 19;
					}
				}
				IL_295:
				return this.ᜉ;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000D27C File Offset: 0x0000B47C
		private float ᜀ(Worksheet A_0, float A_1, float A_2, PdfPageTemplateElement A_3, string A_4, string A_5)
		{
			int a_ = 7;
			switch (0)
			{
			default:
			{
				float num;
				for (;;)
				{
					num = A_1 / 3f;
					int num2 = 52;
					for (;;)
					{
						if (true)
						{
						}
						int width;
						int width2;
						int width4;
						int num3;
						switch (num2)
						{
						case 0:
							if (A_5 == SheetFinishedEventHandler.b("꾿귁냃ꏅ뫇", a_))
							{
								num2 = 48;
								continue;
							}
							return num;
						case 1:
							if (A_4 == SheetFinishedEventHandler.b("", a_))
							{
								num2 = 22;
								continue;
							}
							return num;
						case 2:
							goto IL_55A;
						case 3:
							goto IL_606;
						case 4:
							width = A_0.PageSetup.CenterHeaderImage.Width;
							num2 = 54;
							continue;
						case 5:
							num2 = 57;
							continue;
						case 6:
							if (A_4 == SheetFinishedEventHandler.b("ﶽ", a_))
							{
								num2 = 35;
								continue;
							}
							goto IL_492;
						case 7:
							if (width2 <= 590)
							{
								goto IL_843;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2F7;
							default:
								if (false)
								{
								}
								num2 = 40;
								continue;
							}
							break;
						case 8:
							num2 = 31;
							continue;
						case 9:
						{
							int width3;
							if ((float)width3 > num)
							{
								num2 = 38;
								continue;
							}
							num *= 2f;
							num2 = 51;
							continue;
						}
						case 10:
							if ((float)width4 > A_1)
							{
								num2 = 41;
								continue;
							}
							goto IL_859;
						case 11:
							if (A_5 == SheetFinishedEventHandler.b("ꖿꏁꃃꏅ뫇", a_))
							{
								num2 = 37;
								continue;
							}
							goto IL_7A6;
						case 12:
							goto IL_5D2;
						case 13:
							this.ᜇ.DrawImage(new PdfBitmap(A_0.PageSetup.LeftHeaderImage), new PointF(0f, 0f));
							num = (float)A_0.PageSetup.LeftHeaderImage.Width;
							num2 = 34;
							continue;
						case 14:
							goto IL_75B;
						case 15:
							num2 = 30;
							continue;
						case 16:
							if (A_0.PageSetup.CenterFooterImage != null)
							{
								num2 = 53;
								continue;
							}
							goto IL_215;
						case 17:
							if (A_0.PageSetup.CenterHeaderImage != null)
							{
								num2 = 61;
								continue;
							}
							goto IL_492;
						case 18:
							num2 = 10;
							continue;
						case 19:
							num2 = 16;
							continue;
						case 20:
							goto IL_492;
						case 21:
							if (A_0.PageSetup.RightFooterImage != null)
							{
								num2 = 60;
								continue;
							}
							return num;
						case 22:
							num2 = 21;
							continue;
						case 23:
							if (A_5 == SheetFinishedEventHandler.b("꾿귁냃ꏅ뫇", a_))
							{
								num2 = 55;
								continue;
							}
							goto IL_215;
						case 24:
							goto IL_606;
						case 25:
							if ((float)width4 > num)
							{
								num2 = 18;
								continue;
							}
							goto IL_859;
						case 26:
							if (A_5 == SheetFinishedEventHandler.b("ꖿꏁꃃꏅ뫇", a_))
							{
								num2 = 13;
								continue;
							}
							goto IL_674;
						case 27:
							num2 = 11;
							continue;
						case 28:
							if (A_5 == SheetFinishedEventHandler.b("꾿귁냃ꏅ뫇", a_))
							{
								num2 = 43;
								continue;
							}
							goto IL_55A;
						case 29:
							if (A_4 == SheetFinishedEventHandler.b("ﶽ", a_))
							{
								num2 = 19;
								continue;
							}
							goto IL_215;
						case 30:
							if (width > 590)
							{
								num2 = 36;
								continue;
							}
							goto IL_329;
						case 31:
							if (A_0.PageSetup.LeftFooterImage != null)
							{
								num2 = 42;
								continue;
							}
							goto IL_55A;
						case 32:
							goto IL_5D2;
						case 33:
							num2 = 26;
							continue;
						case 34:
							goto IL_674;
						case 35:
							num2 = 17;
							continue;
						case 36:
							num = 0f;
							num2 = 24;
							continue;
						case 37:
							width2 = A_0.PageSetup.RightHeaderImage.Width;
							num2 = 46;
							continue;
						case 38:
							goto IL_2F7;
						case 39:
							return num;
						case 40:
							num = 0f;
							num2 = 14;
							continue;
						case 41:
							num = 0f;
							num2 = 32;
							continue;
						case 42:
							num2 = 28;
							continue;
						case 43:
							num3 = (int)A_2 - A_0.PageSetup.LeftFooterImage.Height;
							this.ᜇ.DrawImage(new PdfBitmap(A_0.PageSetup.LeftFooterImage), new PointF(0f, (float)num3));
							num2 = 2;
							continue;
						case 44:
							if (A_4 == SheetFinishedEventHandler.b("", a_))
							{
								num2 = 5;
								continue;
							}
							goto IL_7A6;
						case 45:
							goto IL_215;
						case 46:
							if ((float)width2 > num)
							{
								num2 = 58;
								continue;
							}
							goto IL_843;
						case 47:
							goto IL_386;
						case 48:
						{
							int width3 = A_0.PageSetup.RightFooterImage.Width;
							num3 = (int)A_2 - A_0.PageSetup.RightFooterImage.Height;
							num2 = 9;
							continue;
						}
						case 49:
							goto IL_75B;
						case 50:
							num2 = 56;
							continue;
						case 51:
							goto IL_386;
						case 52:
							if (A_4 == SheetFinishedEventHandler.b("", a_))
							{
								num2 = 50;
								continue;
							}
							goto IL_674;
						case 53:
							num2 = 23;
							continue;
						case 54:
							if ((float)width > num)
							{
								num2 = 15;
								continue;
							}
							goto IL_329;
						case 55:
							num3 = (int)A_2 - A_0.PageSetup.CenterFooterImage.Height;
							width4 = A_0.PageSetup.CenterFooterImage.Width;
							num2 = 25;
							continue;
						case 56:
							if (A_0.PageSetup.LeftHeaderImage != null)
							{
								num2 = 33;
								continue;
							}
							goto IL_674;
						case 57:
							if (A_0.PageSetup.RightHeaderImage != null)
							{
								num2 = 27;
								continue;
							}
							goto IL_7A6;
						case 58:
							num2 = 7;
							continue;
						case 59:
							if (A_4 == SheetFinishedEventHandler.b("", a_))
							{
								num2 = 8;
								continue;
							}
							goto IL_55A;
						case 60:
							num2 = 0;
							continue;
						case 61:
							num2 = 62;
							continue;
						case 62:
							if (A_5 == SheetFinishedEventHandler.b("ꖿꏁꃃꏅ뫇", a_))
							{
								num2 = 4;
								continue;
							}
							goto IL_492;
						case 63:
							goto IL_7A6;
						}
						break;
						IL_215:
						num2 = 1;
						continue;
						IL_2F7:
						num = 0f;
						num2 = 47;
						continue;
						IL_329:
						num = A_1 / 2f - (float)width;
						num2 = 3;
						continue;
						IL_386:
						this.ᜇ.DrawImage(new PdfBitmap(A_0.PageSetup.RightFooterImage), new PointF(num, (float)num3));
						num2 = 39;
						continue;
						IL_492:
						num2 = 44;
						continue;
						IL_55A:
						num2 = 29;
						continue;
						IL_5D2:
						this.ᜇ.DrawImage(new PdfBitmap(A_0.PageSetup.CenterFooterImage), new PointF(num, (float)num3));
						num2 = 45;
						continue;
						IL_606:
						this.ᜇ.DrawImage(new PdfBitmap(A_0.PageSetup.CenterHeaderImage), new PointF(num, 0f));
						num2 = 20;
						continue;
						IL_674:
						num2 = 6;
						continue;
						IL_75B:
						this.ᜇ.DrawImage(new PdfBitmap(A_0.PageSetup.RightHeaderImage), new PointF(num, 0f));
						num2 = 63;
						continue;
						IL_7A6:
						num2 = 59;
						continue;
						IL_843:
						num = A_1 - (float)width2;
						num2 = 49;
						continue;
						IL_859:
						num = A_1 / 2f - (float)width4;
						num2 = 12;
					}
				}
				return num;
			}
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000DB24 File Offset: 0x0000BD24
		private void ᜀ(Worksheet A_0, PdfCanvas A_1, int A_2, int A_3, int A_4, int A_5, sprᱥ A_6, sprᱥ A_7)
		{
			switch (0)
			{
			default:
			{
				int num = 3;
				for (;;)
				{
					int num6;
					switch (num)
					{
					case 0:
					{
						IPictureShape pictureShape;
						PdfImage image = new PdfBitmap(pictureShape.Picture);
						Point point;
						RectangleF rectangle = new RectangleF((float)point.X, (float)point.Y, (float)pictureShape.Width, (float)pictureShape.Height);
						int num2;
						int num3;
						rectangle.Offset((float)(-(float)num2), (float)(-(float)num3));
						A_1.DrawImage(image, rectangle);
						num = 11;
						continue;
					}
					case 1:
					{
						IPictures pictures = A_0.Pictures;
						int num2 = A_7.ᜁ(A_3 - 1);
						int num3 = A_6.ᜁ(A_2 - 1);
						int num4 = A_7.ᜁ(A_5);
						int num5 = A_6.ᜁ(A_4);
						num6 = 0;
						int count = pictures.Count;
						num = 5;
						continue;
					}
					case 2:
						num = 8;
						continue;
					case 4:
						num = 14;
						continue;
					case 5:
						goto IL_167;
					case 6:
					{
						int count;
						if (num6 >= count)
						{
							num = 9;
							continue;
						}
						IPictures pictures;
						IPictureShape pictureShape = pictures[num6];
						Point point = this.ᜀ(pictureShape);
						num = 7;
						continue;
					}
					case 7:
					{
						Point point;
						int num5;
						if (point.Y <= num5)
						{
							num = 4;
							continue;
						}
						goto IL_1DC;
					}
					case 8:
					{
						Point point;
						int num4;
						if (point.X <= num4)
						{
							num = 13;
							continue;
						}
						goto IL_1DC;
					}
					case 9:
						return;
					case 10:
						goto IL_167;
					case 11:
						goto IL_1DC;
					case 12:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
						{
							if (true)
							{
							}
							if (false)
							{
							}
							IPictureShape pictureShape;
							Point point;
							int num2;
							if (point.X + pictureShape.Width >= num2)
							{
								num = 0;
								continue;
							}
							goto IL_1DC;
						}
						}
						break;
					case 13:
						num = 12;
						continue;
					case 14:
					{
						IPictureShape pictureShape;
						Point point;
						int num3;
						if (point.Y + pictureShape.Height >= num3)
						{
							num = 2;
							continue;
						}
						goto IL_1DC;
					}
					}
					if (A_0.HasPictures)
					{
						num = 1;
						continue;
					}
					break;
					IL_167:
					num = 6;
					continue;
					IL_1DC:
					num6++;
					num = 10;
				}
				return;
			}
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000DD84 File Offset: 0x0000BF84
		private void ᜁ(PdfCanvas A_0, PdfPen A_1, IBorders A_2, BordersLineType A_3, float A_4, float A_5, float A_6, float A_7, int A_8, int A_9, XlsRange A_10)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			BordersLineType bordersLineType;
			BordersLineType bordersLineType2;
			this.ᜀ(A_3, out bordersLineType, out bordersLineType2);
			int num = A_8;
			int num2 = A_8;
			int num3 = A_9;
			int num4 = A_9;
			int row = A_10.Row;
			int column = A_10.Column;
			IWorksheet worksheet = A_10.Worksheet;
			this.ᜀ(worksheet, row, column, -A_8, -A_9, ref num, ref num3, false, A_2, bordersLineType2, bordersLineType, true);
			this.ᜀ(worksheet, row, column, -A_8, -A_9, ref num2, ref num4, false, A_2, bordersLineType, bordersLineType2, false);
			A_0.DrawLine(A_1, A_4 - (float)num, A_5 - (float)num3, A_6 - (float)num2, A_7 - (float)num4);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000DE48 File Offset: 0x0000C048
		private void ᜀ(Worksheet A_0, int A_1, int A_2, int A_3, int A_4, PdfCanvas A_5, sprᱥ A_6, sprᱥ A_7, float A_8, float A_9)
		{
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					PdfPen pdfPen;
					PdfPen pen;
					int num4;
					int num5;
					switch (num)
					{
					case 0:
						pdfPen = new PdfPen(A_0.Workbook.GetPaletteColor(A_0.GridLineColor));
						goto IL_1C6;
					case 1:
						num = 8;
						continue;
					case 2:
					{
						int num2;
						if (num2 > A_4)
						{
							num = 7;
							continue;
						}
						int num3;
						num3 += A_7.ᜀ(num2);
						A_5.DrawLine(pen, (float)num3, 0f, (float)num3, A_9);
						num2++;
						num = 3;
						continue;
					}
					case 3:
						goto IL_1A4;
					case 4:
						pdfPen = PdfPens.LightGray;
						goto IL_1C6;
					case 6:
						goto IL_17F;
					case 7:
						return;
					case 8:
						if (!A_0.DefaultGridlineColor)
						{
							num = 9;
							continue;
						}
						num = 4;
						continue;
					case 9:
						num = 0;
						continue;
					case 10:
						goto IL_17F;
					case 11:
						if (num4 > A_3)
						{
							num = 12;
							continue;
						}
						num5 += A_6.ᜀ(num4);
						A_5.DrawLine(pen, 0f, (float)num5, A_8, (float)num5);
						num4++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						break;
					case 12:
					{
						A_5.DrawLine(pen, 0f, 0f, 0f, A_9);
						int num2 = A_2;
						int num3 = 0;
						num = 13;
						continue;
					}
					case 13:
						goto IL_1A4;
					}
					if (A_0.GridLinesVisible)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					break;
					IL_17F:
					num = 11;
					continue;
					IL_1A4:
					num = 2;
					continue;
					IL_1C6:
					pen = pdfPen;
					A_5.DrawLine(pen, 0f, 0f, A_8, 0f);
					num4 = A_1;
					num5 = 0;
					num = 10;
				}
				return;
			}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000E064 File Offset: 0x0000C264
		private void ᜁ(Worksheet A_0, spr\u25A6.ᜀ A_1, int A_2, int A_3, PdfCanvas A_4, sprᱥ A_5, sprᱥ A_6)
		{
			for (;;)
			{
				spr\u192F a_ = A_0.MergeCells.ᜀ(A_1);
				Rectangle r = this.ᜀ(A_0, A_1, A_2, A_3, A_5, A_6);
				CellRange a_2 = A_0[A_1.ᜂ() + 1, A_1.ᜅ() + 1];
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_74:
					if (r.Height <= 0)
					{
						return;
					}
					num = 2;
					break;
				default:
					if (false)
					{
					}
					num = 3;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜀ(a_, a_2, r, r, A_4);
						num = 1;
						continue;
					case 1:
						return;
					case 2:
						if (true)
						{
						}
						num = 4;
						continue;
					case 3:
						goto IL_74;
					case 4:
						if (r.Width > 0)
						{
							num = 0;
							continue;
						}
						return;
					}
					break;
				}
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000E154 File Offset: 0x0000C354
		private void ᜀ(Worksheet A_0, spr\u25A6.ᜀ A_1, int A_2, int A_3, PdfCanvas A_4, sprᱥ A_5, sprᱥ A_6)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					spr\u192F a_ = A_0.MergeCells.ᜀ(A_1);
					int num = A_5.ᜁ(A_1.ᜂ());
					int num2 = A_6.ᜁ(A_1.ᜅ());
					num -= A_5.ᜁ(A_2 - 1);
					num2 -= A_6.ᜁ(A_3 - 1);
					int num3 = A_5.ᜀ(A_1.ᜂ() + 1, A_1.ᜇ() + 1);
					int num4 = A_6.ᜀ(A_1.ᜅ() + 1, A_1.ᜃ() + 1);
					Rectangle r = new Rectangle(num2, num, num4, num3);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (false)
						{
						}
						int num5 = 1;
						for (;;)
						{
							switch (num5)
							{
							case 0:
								if (true)
								{
								}
								num5 = 3;
								continue;
							case 1:
								if (num3 > 0)
								{
									num5 = 0;
									continue;
								}
								return;
							case 2:
								this.ᜀ(a_, r, A_4);
								num5 = 4;
								continue;
							case 3:
								if (num4 > 0)
								{
									num5 = 2;
									continue;
								}
								return;
							case 4:
								return;
							}
							break;
						}
						break;
					}
					}
				}
				return;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000E298 File Offset: 0x0000C498
		private void ᜁ(IBorder A_0, float A_1, float A_2, float A_3, float A_4, PdfCanvas A_5)
		{
			switch (0)
			{
			default:
			{
				PdfTilingBrush pdfTilingBrush;
				for (;;)
				{
					float val = Math.Abs(A_3 - A_1);
					float num = Math.Abs(A_4 - A_2);
					float num2 = Math.Max(val, num);
					num = (float)((double)num2 * Math.Sqrt(2.0));
					pdfTilingBrush = new PdfTilingBrush(new RectangleF(0f, 0f, num2, num2));
					PdfBrush brush = new PdfSolidBrush(this.ᜀ(A_0.Color));
					pdfTilingBrush.Graphics.TranslateTransform(num2 / 2f, -num2 / 2f);
					pdfTilingBrush.Graphics.RotateTransform(45f);
					float num3 = 0f;
					int num4 = 1;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_111;
						}
						if (false)
						{
						}
						switch (num4)
						{
						case 0:
							goto IL_EA;
						case 1:
							goto IL_EA;
						case 2:
							goto IL_10F;
						case 3:
							if (num3 >= num)
							{
								num4 = 2;
								continue;
							}
							goto IL_111;
						}
						break;
						IL_EA:
						if (true)
						{
						}
						num4 = 3;
						continue;
						IL_111:
						pdfTilingBrush.Graphics.DrawRectangle(brush, num3, 0f, 6f, num);
						pdfTilingBrush.Graphics.DrawRectangle(brush, num3 + 8f, 0f, 2f, num);
						num3 += 12f;
						num4 = 0;
					}
				}
				IL_10F:
				PdfPen pen = new PdfPen(pdfTilingBrush, this.ᜀ(A_0));
				A_5.DrawLine(pen, A_1, A_2, A_3, A_4);
				return;
			}
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000E428 File Offset: 0x0000C628
		private void ᜀ(IBorder A_0, float A_1, float A_2, float A_3, float A_4, PdfCanvas A_5)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (false)
						{
						}
						PdfPen pen = this.ᜁ(A_0);
						A_5.DrawLine(pen, A_1, A_2, A_3, A_4);
						num = 0;
						continue;
					}
					}
					break;
				}
				if (A_0.LineStyle == LineStyleType.None)
				{
					break;
				}
				num = 1;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
		private void ᜀ(PdfCanvas A_0, PdfPen A_1, IBorders A_2, BordersLineType A_3, float A_4, float A_5, float A_6, float A_7, int A_8, int A_9, XlsRange A_10)
		{
			switch (0)
			{
			default:
			{
				BordersLineType bordersLineType;
				BordersLineType bordersLineType2;
				int num;
				int num2;
				int num3;
				int num4;
				int num5;
				int num6;
				for (;;)
				{
					IL_2F:
					this.ᜀ(A_3, out bordersLineType, out bordersLineType2);
					num = A_8;
					num2 = A_8;
					num3 = A_9;
					num4 = A_9;
					num5 = A_10.Row + A_9;
					num6 = A_10.Column + A_8;
					for (;;)
					{
						IL_61:
						int num7 = 5;
						for (;;)
						{
							switch (num7)
							{
							case 0:
								if (num5 == 0)
								{
									num7 = 2;
									continue;
								}
								goto IL_E7;
							case 1:
								goto IL_B6;
							case 2:
								num5 = 1;
								num7 = 1;
								continue;
							case 3:
								goto IL_B8;
							case 4:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_61;
								default:
									if (true)
									{
									}
									if (false)
									{
									}
									num6 = 1;
									num7 = 3;
									continue;
								}
								break;
							case 5:
								if (num6 == 0)
								{
									num7 = 4;
									continue;
								}
								goto IL_B8;
							}
							goto IL_2F;
							IL_B8:
							num7 = 0;
						}
					}
				}
				IL_B6:
				IL_E7:
				IBorders borders = A_10.Worksheet[num5, num6].Borders;
				this.ᜀ(A_10.Worksheet, num5, num6, A_8, A_9, ref num, ref num3, true, borders, bordersLineType2, bordersLineType, true);
				this.ᜀ(A_10.Worksheet, num5, num6, A_8, A_9, ref num2, ref num4, true, borders, bordersLineType, bordersLineType2, false);
				A_0.DrawLine(A_1, A_4 + (float)num, A_5 + (float)num3, A_6 + (float)num2, A_7 + (float)num4);
				return;
			}
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000E61C File Offset: 0x0000C81C
		private PdfDocument ᜀ(Worksheet A_0, int A_1, int A_2, int A_3, int A_4, CellRange A_5, float A_6, float A_7, sprᱥ A_8, sprᱥ A_9, bool A_10)
		{
			switch (0)
			{
			default:
			{
				float num;
				float num2;
				for (;;)
				{
					IL_2F:
					num = (float)A_8.ᜀ(A_3, A_4);
					num2 = (float)A_9.ᜀ(A_1, A_2);
					for (;;)
					{
						IL_49:
						int num3 = 5;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								this.ᜀ(A_0, A_3, A_1, A_4, A_2, this.ᜇ, A_8, A_9, num2, num);
								num3 = 3;
								continue;
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_49;
								default:
									if (true)
									{
									}
									if (false)
									{
									}
									this.ᜀ();
									num3 = 4;
									continue;
								}
								break;
							case 2:
								if (this.ᜌ.DisplayGridLines == PdfConverterSettings.GridLinesDisplayStyle.Auto | this.ᜌ.DisplayGridLines == PdfConverterSettings.GridLinesDisplayStyle.Visible)
								{
									num3 = 0;
									continue;
								}
								goto IL_103;
							case 3:
								goto IL_B4;
							case 4:
								goto IL_B6;
							case 5:
								if (A_10)
								{
									num3 = 1;
									continue;
								}
								goto IL_B6;
							}
							goto IL_2F;
							IL_B6:
							num3 = 2;
						}
					}
				}
				IL_B4:
				IL_103:
				this.ᜀ(A_0, A_3, A_1, A_4, A_2, this.ᜇ, A_8, A_9, new PdfConverter.ᜀ(this.ᜀ));
				PdfGraphicsState state = this.ᜇ.Save();
				this.ᜇ.SetClip(new RectangleF(0f, 0f, num2, num));
				this.ᜀ(A_0, A_3, A_1, A_4, A_2, this.ᜇ, A_8, A_9, new PdfConverter.ᜁ(this.ᜀ));
				this.ᜀ(A_0, A_3, A_1, A_4, A_2, this.ᜇ, A_8, A_9, new PdfConverter.ᜁ(this.ᜁ));
				this.ᜇ.Restore(state);
				this.ᜀ(A_0, A_3, A_1, A_4, A_2, this.ᜇ, A_8, A_9);
				this.ᜀ(A_0, this.ᜇ, A_3, A_1, A_4, A_2, A_8, A_9);
				this.ᜀ(A_0, this.ᜇ, A_6, A_7);
				return this.ᜉ;
			}
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000E810 File Offset: 0x0000CA10
		private void ᜀ(Worksheet A_0, PdfCanvas A_1, float A_2, float A_3)
		{
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					int num6;
					switch (num)
					{
					case 0:
					{
						ITextBoxes textBoxes = A_0.TextBoxes;
						float num2 = this.ᜉ.PageSettings.Height - (this.ᜉ.PageSettings.Margins.Top + this.ᜉ.PageSettings.Margins.Bottom);
						float num3 = this.ᜉ.PageSettings.Width - (this.ᜉ.PageSettings.Margins.Left + this.ᜉ.PageSettings.Margins.Right);
						float num4 = A_2 + num3;
						float num5 = A_3 + num2;
						num6 = 0;
						num = 7;
						continue;
					}
					case 2:
						goto IL_1CD;
					case 3:
					{
						ITextBoxShape textBoxShape;
						ExcelFont excelFont = textBoxShape.RichText.GetFont(1) as ExcelFont;
						PdfStringFormat pdfStringFormat = new PdfStringFormat();
						pdfStringFormat.Alignment = this.ᜁ(textBoxShape);
						pdfStringFormat.LineLimit = false;
						pdfStringFormat.NoClip = true;
						PdfTrueTypeFont pdfTrueTypeFont = new PdfTrueTypeFont(this.ᜀ(excelFont, excelFont.FontName, (int)excelFont.Size), this.ᜌ.EmbedFonts);
						pdfTrueTypeFont = this.ᜀ(pdfTrueTypeFont);
						pdfStringFormat.LineAlignment = this.ᜀ(textBoxShape);
						Rectangle r;
						A_1.DrawString(textBoxShape.Text, pdfTrueTypeFont, new PdfSolidBrush(this.ᜀ(excelFont.Color)), r, pdfStringFormat);
						num = 2;
						continue;
					}
					case 4:
						return;
					case 5:
					{
						ITextBoxShape textBoxShape;
						if ((float)textBoxShape.Top >= A_3)
						{
							num = 16;
							continue;
						}
						goto IL_1CD;
					}
					case 6:
					{
						ITextBoxShape textBoxShape;
						if (textBoxShape.Visible)
						{
							goto IL_1BC;
						}
						goto IL_1CD;
					}
					case 7:
						goto IL_2E9;
					case 8:
						goto IL_2E9;
					case 9:
					{
						ITextBoxShape textBoxShape;
						Rectangle r;
						A_1.DrawRectangle(PdfPens.Black, new PdfSolidBrush(this.ᜀ(textBoxShape.Fill.ForeColor)), r);
						num = 14;
						continue;
					}
					case 10:
					{
						float num4;
						ITextBoxShape textBoxShape;
						if ((float)textBoxShape.Left <= num4)
						{
							num = 17;
							continue;
						}
						goto IL_1CD;
					}
					case 11:
					{
						float num5;
						ITextBoxShape textBoxShape;
						if ((float)textBoxShape.Top <= num5)
						{
							num = 18;
							continue;
						}
						goto IL_1CD;
					}
					case 12:
					{
						ITextBoxes textBoxes;
						if (num6 >= textBoxes.Count)
						{
							num = 4;
							continue;
						}
						ITextBoxShape textBoxShape = textBoxes[num6];
						num = 6;
						continue;
					}
					case 13:
					{
						ITextBoxShape textBoxShape;
						if ((float)textBoxShape.Left >= A_2)
						{
							num = 9;
							continue;
						}
						goto IL_1CD;
					}
					case 14:
					{
						ITextBoxShape textBoxShape;
						if (textBoxShape.RichText.Text != "")
						{
							num = 3;
							continue;
						}
						goto IL_1CD;
					}
					case 15:
					{
						float num3;
						ITextBoxShape textBoxShape;
						int x = textBoxShape.Left - (int)((float)(textBoxShape.Left / (int)num3) * num3);
						float num2;
						int y = textBoxShape.Top - (int)((float)(textBoxShape.Top / (int)num2) * num2);
						int height = textBoxShape.Height;
						int width = textBoxShape.Width;
						Rectangle r = new Rectangle(x, y, width, height);
						num = 11;
						continue;
					}
					case 16:
						num = 10;
						continue;
					case 17:
						num = 13;
						continue;
					case 18:
						num = 5;
						continue;
					}
					if (A_0.TextBoxes != null)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1BC;
						}
						if (false)
						{
						}
						if (true)
						{
						}
						num = 0;
						continue;
					}
					break;
					IL_1BC:
					num = 15;
					continue;
					IL_1CD:
					num6++;
					num = 8;
					continue;
					IL_2E9:
					num = 12;
				}
				return;
			}
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000EC04 File Offset: 0x0000CE04
		private CellRange ᜀ(Worksheet A_0, CellRange A_1, sprᱥ A_2, sprᱥ A_3)
		{
			switch (0)
			{
			default:
			{
				int num;
				int num2;
				for (;;)
				{
					PdfSection pdfSection = this.ᜉ.Sections.Add();
					PdfPageSettings pageSettings = pdfSection.PageSettings;
					this.ᜄ = pdfSection;
					pageSettings.Width = this.ᜑ.Width;
					pageSettings.Height = this.ᜑ.Height;
					num = A_1.LastRow;
					num2 = A_1.LastColumn;
					int num3 = 0;
					int num4 = 0;
					int num5 = 0;
					int num6 = 0;
					int num7 = 48;
					for (;;)
					{
						int num12;
						int num13;
						int num14;
						int num15;
						float num16;
						switch (num7)
						{
						case 0:
							pageSettings.Height = (float)num6;
							num7 = 45;
							continue;
						case 1:
							num7 = 20;
							continue;
						case 2:
						{
							int num8;
							CellRange[] mergedCells;
							if (num8 >= mergedCells.Length)
							{
								num7 = 6;
								continue;
							}
							CellRange cellRange = mergedCells[num8];
							num4 = Math.Max(num4, cellRange.LastRow);
							num3 = Math.Max(num3, cellRange.LastColumn);
							num8++;
							num7 = 47;
							continue;
						}
						case 3:
						{
							int num9;
							if (num9 > num6)
							{
								num7 = 67;
								continue;
							}
							goto IL_268;
						}
						case 4:
							goto IL_768;
						case 5:
							goto IL_933;
						case 6:
							goto IL_3A5;
						case 7:
							goto IL_3EA;
						case 8:
							try
							{
								num7 = 1;
								for (;;)
								{
									IPictureShape pictureShape;
									Point point;
									int num11;
									switch (num7)
									{
									case 0:
									{
										int num10;
										num5 = num10;
										num7 = 2;
										continue;
									}
									case 2:
										goto IL_6CE;
									case 3:
										goto IL_676;
									case 4:
										goto IL_65B;
									case 5:
										goto IL_723;
									case 6:
									{
										IEnumerator<IPictureShape> enumerator;
										if (!enumerator.MoveNext())
										{
											num7 = 3;
											continue;
										}
										pictureShape = enumerator.Current;
										point = this.ᜀ(pictureShape);
										int num10 = point.X + pictureShape.Width;
										num7 = 9;
										continue;
									}
									case 7:
										if (num11 > num6)
										{
											num7 = 8;
											continue;
										}
										goto IL_65B;
									case 8:
										num6 = num11;
										num7 = 4;
										continue;
									case 9:
									{
										int num10;
										if (num10 > num5)
										{
											num7 = 0;
											continue;
										}
										goto IL_6CE;
									}
									}
									goto IL_63D;
									IL_65B:
									num7 = 6;
									continue;
									IL_63D:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										IL_676:
										num7 = 5;
										continue;
									default:
										if (false)
										{
										}
										goto IL_65B;
									}
									IL_6CE:
									num11 = point.Y + pictureShape.Height;
									num7 = 7;
								}
								IL_723:
								goto IL_8DB;
							}
							finally
							{
								num7 = 1;
								for (;;)
								{
									IEnumerator<IPictureShape> enumerator;
									switch (num7)
									{
									case 0:
										goto IL_765;
									case 2:
										enumerator.Dispose();
										num7 = 0;
										continue;
									}
									if (enumerator == null)
									{
										break;
									}
									num7 = 2;
								}
								IL_765:;
							}
							goto IL_768;
						case 9:
							goto IL_8B6;
						case 10:
							goto IL_268;
						case 11:
							goto IL_7BF;
						case 12:
							if (A_2.ᜀ(1, num) > num6)
							{
								num7 = 34;
								continue;
							}
							goto IL_7BF;
						case 13:
							goto IL_3EA;
						case 14:
							num7 = 44;
							continue;
						case 15:
						{
							SizeF sizeF = SizeF.Empty;
							num7 = 43;
							continue;
						}
						case 16:
							num7 = 64;
							continue;
						case 17:
							num7 = 60;
							continue;
						case 18:
							num7 = 33;
							continue;
						case 19:
							if (true)
							{
							}
							if ((float)num6 > pageSettings.Height)
							{
								num7 = 0;
								continue;
							}
							goto IL_82A;
						case 20:
						{
							CellRange cellRange2;
							if (!this.ᜁ(cellRange2))
							{
								num7 = 24;
								continue;
							}
							goto IL_35B;
						}
						case 21:
						{
							CellRange cellRange2;
							if (cellRange2.IsBlank)
							{
								num7 = 1;
								continue;
							}
							goto IL_35B;
						}
						case 22:
						{
							if (num12 <= 0)
							{
								num7 = 23;
								continue;
							}
							CellRange cellRange3 = A_0.Range[num13, num12];
							num7 = 61;
							continue;
						}
						case 23:
							goto IL_91F;
						case 24:
							num2--;
							num7 = 7;
							continue;
						case 25:
							if (A_0.MergedCells != null)
							{
								num7 = 14;
								continue;
							}
							goto IL_3A5;
						case 26:
						{
							CellRange cellRange4;
							if (cellRange4.IsBlank)
							{
								num7 = 59;
								continue;
							}
							goto IL_7BF;
						}
						case 27:
							goto IL_3C5;
						case 28:
							goto IL_1AD;
						case 29:
						{
							CellRange cellRange3;
							if (!cellRange3.Style.WrapText)
							{
								num7 = 15;
								continue;
							}
							goto IL_91F;
						}
						case 30:
							if (num14 > num5)
							{
								num7 = 31;
								continue;
							}
							goto IL_421;
						case 31:
							num5 = num14;
							num7 = 53;
							continue;
						case 32:
							goto IL_903;
						case 33:
							goto IL_35B;
						case 34:
							num7 = 50;
							continue;
						case 35:
							if ((float)num5 > pageSettings.Width)
							{
								num7 = 62;
								continue;
							}
							goto IL_7D6;
						case 36:
							goto IL_903;
						case 37:
							if (num == A_1.LastRow)
							{
								num7 = 57;
								continue;
							}
							goto IL_95B;
						case 38:
						{
							num2 = num15;
							int num9 = A_2.ᜀ(1, num);
							num7 = 3;
							continue;
						}
						case 39:
							goto IL_1AD;
						case 40:
							num--;
							num7 = 66;
							continue;
						case 41:
							goto IL_933;
						case 42:
							if (num2 != A_1.LastColumn)
							{
								num7 = 52;
								continue;
							}
							return A_1;
						case 43:
						{
							CellRange cellRange3;
							if (cellRange3.HasRichText)
							{
								num7 = 46;
								continue;
							}
							SizeF sizeF = A_0.ᜀ(cellRange3, false, false);
							num7 = 36;
							continue;
						}
						case 44:
							if (A_0.MergedCells.Length > 0)
							{
								num7 = 49;
								continue;
							}
							goto IL_3A5;
						case 45:
							goto IL_82A;
						case 46:
						{
							CellRange cellRange3;
							Metafile metafile = sprᯏ.ᜀ(cellRange3.RichText.RtfText);
							GraphicsUnit graphicsUnit = GraphicsUnit.Pixel;
							SizeF sizeF = metafile.GetBounds(ref graphicsUnit).Size;
							long key = (long)(cellRange3.Row | cellRange3.Column);
							this.\u171C[key] = metafile;
							num7 = 32;
							continue;
						}
						case 47:
							goto IL_3C5;
						case 48:
							if (A_0.HasPictures)
							{
								num7 = 63;
								continue;
							}
							goto IL_8DB;
						case 49:
						{
							CellRange[] mergedCells = A_0.MergedCells;
							int num8 = 0;
							num7 = 27;
							continue;
						}
						case 50:
						{
							if (num <= num4)
							{
								num7 = 11;
								continue;
							}
							CellRange cellRange4 = A_0.Range[num, 1, num, num2];
							num7 = 26;
							continue;
						}
						case 51:
							goto IL_7D6;
						case 52:
							goto IL_356;
						case 53:
							goto IL_421;
						case 54:
						{
							SizeF sizeF;
							if (num16 >= sizeF.Width)
							{
								num7 = 16;
								continue;
							}
							num15++;
							num16 = (float)A_3.ᜀ(num12, num15);
							num7 = 41;
							continue;
						}
						case 55:
							if (num13 > num)
							{
								num7 = 38;
								continue;
							}
							num12 = num2;
							num7 = 39;
							continue;
						case 56:
						{
							CellRange cellRange4;
							if (!this.ᜀ(cellRange4))
							{
								num7 = 40;
								continue;
							}
							goto IL_7BF;
						}
						case 57:
							num7 = 42;
							continue;
						case 58:
							if (A_3.ᜀ(1, num2) > num5)
							{
								num7 = 17;
								continue;
							}
							goto IL_35B;
						case 59:
							num7 = 56;
							continue;
						case 60:
						{
							if (num2 <= num3)
							{
								num7 = 18;
								continue;
							}
							CellRange cellRange2 = A_0.Range[1, num2, num, num2];
							num7 = 21;
							continue;
						}
						case 61:
						{
							CellRange cellRange3;
							if (!cellRange3.IsBlank)
							{
								num7 = 4;
								continue;
							}
							num12--;
							num7 = 28;
							continue;
						}
						case 62:
							pageSettings.Width = (float)num5;
							num7 = 51;
							continue;
						case 63:
						{
							IEnumerator<IPictureShape> enumerator = A_0.Pictures.GetEnumerator();
							num7 = 8;
							continue;
						}
						case 64:
							goto IL_91F;
						case 65:
							goto IL_8B6;
						case 66:
							goto IL_35B;
						case 67:
						{
							int num9;
							num6 = num9;
							num7 = 10;
							continue;
						}
						}
						break;
						IL_1AD:
						num7 = 22;
						continue;
						IL_268:
						num14 = A_3.ᜀ(1, num2);
						num7 = 30;
						continue;
						IL_35B:
						num7 = 12;
						continue;
						IL_3A5:
						num2 = Math.Max(num2, num3);
						num = Math.Max(num, num4);
						num7 = 13;
						continue;
						IL_3C5:
						num7 = 2;
						continue;
						IL_3EA:
						num7 = 58;
						continue;
						IL_421:
						num7 = 19;
						continue;
						IL_768:
						num7 = 29;
						continue;
						IL_7BF:
						num15 = num2;
						num13 = 1;
						num7 = 9;
						continue;
						IL_7D6:
						num7 = 37;
						continue;
						IL_82A:
						num7 = 35;
						continue;
						IL_8B6:
						num7 = 55;
						continue;
						IL_8DB:
						num7 = 25;
						continue;
						IL_903:
						num16 = (float)A_3.ᜀ(num12, num15);
						num7 = 5;
						continue;
						IL_91F:
						num13++;
						num7 = 65;
						continue;
						IL_933:
						num7 = 54;
					}
				}
				IL_356:
				IL_95B:
				return A_0.Range[1, 1, num, num2];
			}
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000F5A8 File Offset: 0x0000D7A8
		private bool ᜁ(CellRange A_0)
		{
			switch (0)
			{
			default:
			{
				IEnumerator<IXLSRange> enumerator = A_0.GetEnumerator();
				bool result;
				try
				{
					int num = 10;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_152;
						case 1:
						{
							BordersCollection borders;
							if (borders[BordersLineType.EdgeTop].LineStyle == LineStyleType.None)
							{
								num = 3;
								continue;
							}
							goto IL_DE;
						}
						case 2:
							goto IL_DE;
						case 3:
							num = 5;
							continue;
						case 4:
							goto IL_99;
						case 5:
						{
							BordersCollection borders;
							if (borders[BordersLineType.EdgeRight].LineStyle == LineStyleType.None)
							{
								num = 6;
								continue;
							}
							goto IL_DE;
						}
						case 6:
							num = 7;
							continue;
						case 7:
						{
							BordersCollection borders;
							if (borders[BordersLineType.EdgeBottom].LineStyle != LineStyleType.None)
							{
								num = 2;
								continue;
							}
							goto IL_7F;
						}
						case 8:
						{
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							CellRange cellRange = (CellRange)enumerator.Current;
							BordersCollection borders = cellRange.Style.Borders;
							num = 1;
							continue;
						}
						case 9:
							goto IL_EC;
						}
						goto IL_61;
						IL_7F:
						num = 8;
						continue;
						IL_61:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_99:
							num = 0;
							continue;
						default:
							if (false)
							{
							}
							goto IL_7F;
						}
						IL_DE:
						result = true;
						num = 9;
					}
					IL_EC:
					return result;
					IL_152:
					return false;
				}
				finally
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							enumerator.Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_192;
						}
						if (enumerator == null)
						{
							goto IL_19C;
						}
						num = 0;
					}
					IL_192:
					if (true)
					{
					}
					IL_19C:;
				}
				return result;
			}
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000F770 File Offset: 0x0000D970
		private bool ᜀ(CellRange A_0)
		{
			switch (0)
			{
			default:
			{
				IEnumerator<IXLSRange> enumerator = A_0.GetEnumerator();
				bool result;
				try
				{
					int num = 7;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							if (!enumerator.MoveNext())
							{
								num = 6;
								continue;
							}
							CellRange cellRange = (CellRange)enumerator.Current;
							BordersCollection borders = cellRange.Style.Borders;
							num = 1;
							continue;
						}
						case 1:
						{
							BordersCollection borders;
							if (borders[BordersLineType.EdgeLeft].LineStyle == LineStyleType.None)
							{
								num = 5;
								continue;
							}
							goto IL_E6;
						}
						case 2:
							goto IL_F4;
						case 3:
						{
							BordersCollection borders;
							if (borders[BordersLineType.EdgeRight].LineStyle == LineStyleType.None)
							{
								num = 10;
								continue;
							}
							goto IL_E6;
						}
						case 4:
						{
							BordersCollection borders;
							if (borders[BordersLineType.EdgeBottom].LineStyle != LineStyleType.None)
							{
								num = 9;
								continue;
							}
							goto IL_87;
						}
						case 5:
							num = 3;
							continue;
						case 6:
							goto IL_A1;
						case 8:
							goto IL_15A;
						case 9:
							goto IL_E6;
						case 10:
							num = 4;
							continue;
						}
						goto IL_61;
						IL_87:
						num = 0;
						continue;
						IL_61:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_A1:
							num = 8;
							continue;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							goto IL_87;
						}
						IL_E6:
						result = true;
						num = 2;
					}
					IL_F4:
					return result;
					IL_15A:
					return false;
				}
				finally
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_19A;
						case 1:
							enumerator.Dispose();
							num = 0;
							continue;
						}
						if (enumerator == null)
						{
							break;
						}
						num = 1;
					}
					IL_19A:;
				}
				return result;
			}
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000F938 File Offset: 0x0000DB38
		private int[] ᜀ(Worksheet A_0)
		{
			int num = 3;
			Match match;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7B;
				case 1:
					num = 7;
					continue;
				case 2:
					if (A_0.PageSetup.PrintTitleRows != null)
					{
						num = 4;
						continue;
					}
					goto IL_119;
				case 4:
					match = PdfConverter.ᜀ.Match(A_0.PageSetup.PrintTitleRows);
					num = 5;
					continue;
				case 5:
					if (match.Success)
					{
						num = 0;
						continue;
					}
					goto IL_119;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DE;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 7:
					if (A_0.PageSetup != null)
					{
						num = 6;
						continue;
					}
					goto IL_119;
				}
				if (A_0 == null)
				{
					goto IL_119;
				}
				num = 1;
			}
			IL_7B:
			IL_DE:
			return new int[]
			{
				int.Parse(match.Groups[1].Value),
				int.Parse(match.Groups[2].Value)
			};
			IL_119:
			if (true)
			{
			}
			return null;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000FA68 File Offset: 0x0000DC68
		private PdfDocument ᜀ(Worksheet A_0, CellRange A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					sprᱥ sprᱥ = new sprᱥ(new sprᱥ.ᜀ(A_0.GetRowHeightPixels));
					sprᱥ sprᱥ2 = new sprᱥ(new sprᱥ.ᜀ(A_0.GetColumnWidthPixels));
					PdfPageSettings pageSettings = this.ᜉ.PageSettings;
					int num = 1;
					for (;;)
					{
						List<int> list;
						int num2;
						List<int> list2;
						int num3;
						List<int> list3;
						float num4;
						float num5;
						int[] array;
						PdfGraphicsState pdfGraphicsState;
						int num6;
						int num8;
						int num7;
						int num9;
						int lastColumn;
						int num10;
						List<int> list4;
						int num12;
						List<int> list5;
						int num13;
						int num14;
						float num15;
						float num16;
						bool a_;
						int num18;
						int num19;
						int lastRow;
						switch (num)
						{
						case 0:
							list = new List<int>();
							num2 = 0;
							list2 = new List<int>();
							list2.Add(1);
							num3 = 1;
							num = 7;
							continue;
						case 1:
							if (this.FitToPage != FitToPageType.None)
							{
								num = 47;
								continue;
							}
							goto IL_612;
						case 2:
							num = 73;
							continue;
						case 3:
							num = 48;
							continue;
						case 4:
							this.ᜃ();
							this.ᜌ.ExportBookmarks = false;
							this.ᜁ = true;
							num = 26;
							continue;
						case 5:
							try
							{
								num = 4;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_802;
									case 2:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											break;
										default:
											if (false)
											{
											}
											break;
										}
										num = 0;
										continue;
									case 3:
									{
										IEnumerator<IVPageBreak> enumerator;
										if (!enumerator.MoveNext())
										{
											num = 2;
											continue;
										}
										VPageBreak vpageBreak = (VPageBreak)enumerator.Current;
										list3.Add(vpageBreak.Column);
										num = 1;
										continue;
									}
									}
									IL_796:
									num = 3;
									continue;
									goto IL_796;
								}
								IL_802:
								goto IL_C51;
							}
							finally
							{
								num = 0;
								for (;;)
								{
									IEnumerator<IVPageBreak> enumerator;
									switch (num)
									{
									case 1:
										enumerator.Dispose();
										num = 2;
										continue;
									case 2:
										goto IL_844;
									}
									if (enumerator == null)
									{
										break;
									}
									num = 1;
								}
								IL_844:;
							}
							goto IL_847;
						case 6:
						{
							RectangleF rectangle = new RectangleF((float)(num2 + 1), 0f, num4 - (float)num2, num5);
							this.ᜅ.Canvas.DrawRectangle(PdfBrushes.White, rectangle);
							num = 83;
							continue;
						}
						case 7:
							goto IL_CF0;
						case 8:
							if (array != null)
							{
								num = 44;
								continue;
							}
							goto IL_B5E;
						case 9:
							goto IL_5D7;
						case 10:
							goto IL_612;
						case 11:
							num = 65;
							continue;
						case 12:
							if (this.ᜌ.ExportBookmarks)
							{
								num = 4;
								continue;
							}
							goto IL_417;
						case 13:
							this.ᜅ.Canvas.Restore(pdfGraphicsState);
							num = 84;
							continue;
						case 14:
							goto IL_6BF;
						case 15:
							num6 = 1;
							num = 58;
							continue;
						case 16:
							goto IL_448;
						case 17:
							num7 = num8;
							num = 42;
							continue;
						case 18:
							if ((float)num2 > num4)
							{
								num = 22;
								continue;
							}
							num = 20;
							continue;
						case 19:
							num9--;
							num = 30;
							continue;
						case 20:
							if (num3 != lastColumn)
							{
								num = 2;
								continue;
							}
							goto IL_4ED;
						case 21:
							if (num10 >= list4.Count)
							{
								num = 77;
								continue;
							}
							num = 52;
							continue;
						case 22:
							goto IL_847;
						case 23:
							num = 71;
							continue;
						case 24:
							goto IL_C4C;
						case 25:
						{
							int num11;
							if (num2 == num11)
							{
								num = 64;
								continue;
							}
							num2 -= num11;
							num3--;
							num = 41;
							continue;
						}
						case 26:
							goto IL_417;
						case 27:
							num12 = list4[num10 - 1] + 1;
							goto IL_CCA;
						case 28:
							num12 = list4[0];
							goto IL_CCA;
						case 29:
							goto IL_3D5;
						case 30:
							goto IL_49A;
						case 31:
							if (true)
							{
							}
							num = 27;
							continue;
						case 32:
							goto IL_CF0;
						case 33:
							num8 = sprᱥ.ᜀ(array[0], array[1]);
							num = 49;
							continue;
						case 34:
							num7 = num8;
							num = 36;
							continue;
						case 35:
							num = 66;
							continue;
						case 36:
							goto IL_C0F;
						case 37:
							if (array != null)
							{
								num = 33;
								continue;
							}
							goto IL_92A;
						case 38:
							if (array != null)
							{
								num = 3;
								continue;
							}
							goto IL_A44;
						case 39:
							if (num6 == 0)
							{
								num = 15;
								continue;
							}
							goto IL_96B;
						case 40:
							num = 60;
							continue;
						case 41:
							goto IL_727;
						case 42:
							goto IL_C0F;
						case 43:
						{
							try
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_257;
									case 2:
									{
										IEnumerator<IHPageBreak> enumerator2;
										if (!enumerator2.MoveNext())
										{
											num = 3;
											continue;
										}
										HPageBreak hpageBreak = (HPageBreak)enumerator2.Current;
										list5.Add(hpageBreak.Row);
										num = 4;
										continue;
									}
									case 3:
										num = 0;
										continue;
									}
									IL_231:
									num = 2;
									continue;
									goto IL_231;
								}
								IL_257:
								goto IL_A58;
							}
							finally
							{
								num = 1;
								for (;;)
								{
									IEnumerator<IHPageBreak> enumerator2;
									switch (num)
									{
									case 0:
										enumerator2.Dispose();
										num = 2;
										continue;
									case 2:
										goto IL_299;
									}
									if (enumerator2 == null)
									{
										break;
									}
									num = 0;
								}
								IL_299:;
							}
							goto IL_29C;
							IL_A58:
							IEnumerator<IVPageBreak> enumerator = A_0.VPageBreaks.GetEnumerator();
							num = 5;
							continue;
						}
						case 44:
							num = 90;
							continue;
						case 45:
							goto IL_B72;
						case 46:
							goto IL_4ED;
						case 47:
							A_1 = this.ᜀ(A_0, A_1, sprᱥ, sprᱥ2);
							pageSettings = this.ᜄ.PageSettings;
							num = 10;
							continue;
						case 48:
							if (num9 >= array[1])
							{
								num = 17;
								continue;
							}
							goto IL_A44;
						case 49:
							goto IL_92A;
						case 50:
							this.ᜉ = this.ᜀ(A_0, num13, num14, array[0], array[1], A_1, num15, num16, sprᱥ, sprᱥ2, true);
							a_ = false;
							num16 += (float)num8;
							pdfGraphicsState = this.ᜅ.Canvas.Save();
							this.ᜅ.Canvas.TranslateTransform(0f, (float)num8);
							num = 79;
							continue;
						case 51:
							if (array != null)
							{
								num = 23;
								continue;
							}
							goto IL_309;
						case 52:
							if (num10 != 1)
							{
								num = 31;
								continue;
							}
							num = 28;
							continue;
						case 53:
							if (num13 == 0)
							{
								num = 63;
								continue;
							}
							goto IL_9BC;
						case 54:
							if (this.ᜐ.IsBlank)
							{
								num = 82;
								continue;
							}
							goto IL_448;
						case 55:
							if ((float)num7 > num5)
							{
								num = 40;
								continue;
							}
							num = 68;
							continue;
						case 56:
							if (this.ᜐ.HasPictures)
							{
								num = 16;
								continue;
							}
							goto IL_BBD;
						case 57:
							list.Add((num3 == lastColumn) ? ((int)Math.Ceiling((double)num4)) : num2);
							list2.Add(num3);
							num2 = 0;
							num = 29;
							continue;
						case 58:
							goto IL_96B;
						case 59:
						{
							if (num3 > lastColumn)
							{
								num = 74;
								continue;
							}
							int num11 = sprᱥ2.ᜀ(num3);
							num2 += num11;
							num = 18;
							continue;
						}
						case 60:
						{
							int num17;
							if (num7 >= num17)
							{
								num = 19;
								continue;
							}
							goto IL_49A;
						}
						case 61:
							if (num18 >= list2.Count)
							{
								num = 24;
								continue;
							}
							num = 88;
							continue;
						case 62:
							goto IL_C0F;
						case 63:
							num13 = 1;
							num = 86;
							continue;
						case 64:
							num2 = (int)Math.Ceiling((double)num4);
							num = 75;
							continue;
						case 65:
							num19 = list2[num18 - 1] + 1;
							goto IL_A08;
						case 66:
							if (list5.Contains(num9))
							{
								num = 9;
								continue;
							}
							goto IL_C0F;
						case 67:
							num19 = list2[0];
							goto IL_A08;
						case 68:
							if (num9 != lastRow)
							{
								num = 35;
								continue;
							}
							goto IL_5D7;
						case 69:
							goto IL_3D5;
						case 70:
						{
							if (num9 > lastRow)
							{
								num = 0;
								continue;
							}
							int num17 = sprᱥ.ᜀ(num9);
							num7 += num17;
							num = 55;
							continue;
						}
						case 71:
							if (num6 > array[1])
							{
								num = 50;
								continue;
							}
							goto IL_309;
						case 72:
							if (this.EnableExcelPageBreak)
							{
								num = 81;
								continue;
							}
							goto IL_C51;
						case 73:
							if (list3.Contains(num3))
							{
								num = 46;
								continue;
							}
							goto IL_3D5;
						case 74:
							num15 = 0f;
							num18 = 1;
							num = 78;
							continue;
						case 75:
							goto IL_727;
						case 76:
							if (num2 != 0)
							{
								num = 6;
								continue;
							}
							goto IL_BBD;
						case 77:
							num15 += num4;
							num18++;
							num = 87;
							continue;
						case 78:
							goto IL_C26;
						case 79:
							goto IL_309;
						case 80:
							goto IL_6BF;
						case 81:
						{
							IEnumerator<IHPageBreak> enumerator2 = A_0.HPageBreaks.GetEnumerator();
							num = 43;
							continue;
						}
						case 82:
							goto IL_29C;
						case 83:
							goto IL_BBD;
						case 84:
							goto IL_3AB;
						case 85:
							goto IL_C0F;
						case 86:
							goto IL_9BC;
						case 87:
							goto IL_C26;
						case 88:
							if (num18 != 1)
							{
								num = 11;
								continue;
							}
							num = 67;
							continue;
						case 89:
							goto IL_B72;
						case 90:
							if (num9 >= array[1])
							{
								num = 34;
								continue;
							}
							goto IL_B5E;
						case 91:
							if (pdfGraphicsState != null)
							{
								num = 13;
								continue;
							}
							goto IL_3AB;
						}
						break;
						IL_29C:
						num = 56;
						continue;
						IL_309:
						int num20;
						this.ᜉ = this.ᜀ(A_0, num13, num14, num6, num20, A_1, num15, num16, sprᱥ, sprᱥ2, a_);
						num = 91;
						continue;
						IL_3AB:
						num = 12;
						continue;
						IL_3D5:
						num3++;
						num = 32;
						continue;
						IL_417:
						num2 = list[num18 - 1];
						num = 76;
						continue;
						IL_448:
						pdfGraphicsState = null;
						a_ = true;
						num = 51;
						continue;
						IL_49A:
						list4.Add(num9);
						num = 38;
						continue;
						IL_4ED:
						num = 57;
						continue;
						IL_5D7:
						list4.Add(num9);
						num = 8;
						continue;
						IL_612:
						num5 = pageSettings.Height - (pageSettings.Margins.Top + pageSettings.Margins.Bottom);
						num4 = pageSettings.Width - (pageSettings.Margins.Left + pageSettings.Margins.Right);
						lastColumn = A_1.LastColumn;
						lastRow = A_1.LastRow;
						sprᱥ.ᜀ(1, lastRow);
						sprᱥ2.ᜀ(1, lastColumn);
						list5 = new List<int>();
						list3 = new List<int>();
						num = 72;
						continue;
						IL_6BF:
						num = 21;
						continue;
						IL_727:
						list.Add(num2);
						list2.Add(num3);
						num2 = 0;
						num = 69;
						continue;
						IL_847:
						num = 25;
						continue;
						IL_92A:
						num9 = 1;
						num = 45;
						continue;
						IL_96B:
						num20 = list4[num10];
						this.ᜐ = A_0.Range[num6, num13, num20, num14];
						num = 54;
						continue;
						IL_9BC:
						num14 = list2[num18];
						num16 = 0f;
						num10 = 1;
						num = 14;
						continue;
						IL_A08:
						num13 = num19;
						num = 53;
						continue;
						IL_A44:
						num7 = 0;
						num = 62;
						continue;
						IL_B5E:
						num7 = 0;
						num = 85;
						continue;
						IL_B72:
						num = 70;
						continue;
						IL_BBD:
						num16 += num5;
						num10++;
						num = 80;
						continue;
						IL_C0F:
						num9++;
						num = 89;
						continue;
						IL_C26:
						num = 61;
						continue;
						IL_C51:
						list4 = new List<int>();
						list4.Add(1);
						num7 = 0;
						array = this.ᜀ(A_0);
						num8 = 0;
						num = 37;
						continue;
						IL_CCA:
						num6 = num12;
						num = 39;
						continue;
						IL_CF0:
						num = 59;
					}
				}
				IL_C4C:
				return this.ᜉ;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000107B0 File Offset: 0x0000E9B0
		private RectangleF ᜀ(XlsRange A_0, RectangleF A_1, sprᱥ A_2, int A_3, spr\u24F1 A_4)
		{
			switch (0)
			{
			default:
			{
				RectangleF result;
				int num2;
				int num5;
				for (;;)
				{
					result = A_1;
					int num = 21;
					for (;;)
					{
						int num3;
						int num4;
						long key;
						SizeF sizeF;
						Worksheet worksheet;
						int num6;
						int num7;
						int num8;
						int maxColumnCount;
						switch (num)
						{
						case 0:
							return result;
						case 1:
							num2 = 1;
							num = 25;
							continue;
						case 2:
							if (!A_0.IsWrapText)
							{
								num = 32;
								continue;
							}
							return result;
						case 3:
							num = 19;
							continue;
						case 4:
							goto IL_41A;
						case 5:
							if (!A_0.HasString)
							{
								num = 9;
								continue;
							}
							goto IL_117;
						case 6:
							num3 = 1;
							num = 11;
							continue;
						case 7:
							goto IL_D4;
						case 8:
							num4 = -1;
							num = 18;
							continue;
						case 9:
							num = 34;
							continue;
						case 10:
						{
							GraphicsUnit graphicsUnit = GraphicsUnit.Pixel;
							sizeF = this.\u171C[key].GetBounds(ref graphicsUnit).Size;
							num = 17;
							continue;
						}
						case 11:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_261;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								goto IL_2BE;
							}
							break;
						case 12:
							goto IL_189;
						case 13:
							num = 2;
							continue;
						case 14:
							goto IL_41A;
						case 15:
							if (this.\u171C.ContainsKey(key))
							{
								num = 10;
								continue;
							}
							sizeF = worksheet.ᜀ(A_0, false, false);
							num = 20;
							continue;
						case 16:
							if (num3 == 0)
							{
								num = 6;
								continue;
							}
							goto IL_2BE;
						case 17:
							goto IL_3A0;
						case 18:
							goto IL_3D8;
						case 19:
							if (num5 > 0)
							{
								num = 24;
								continue;
							}
							goto IL_1A2;
						case 20:
							goto IL_3A0;
						case 21:
							if (!A_0.IsBlank)
							{
								num = 13;
								continue;
							}
							return result;
						case 22:
							num = 31;
							continue;
						case 23:
						{
							HorizontalAlignType horizontalAlignment;
							if (horizontalAlignment == HorizontalAlignType.Right)
							{
								num = 8;
								continue;
							}
							goto IL_3D8;
						}
						case 24:
							num = 35;
							continue;
						case 25:
							goto IL_275;
						case 26:
							goto IL_261;
						case 27:
							if (A_4.IsBlank)
							{
								num = 22;
								continue;
							}
							goto IL_1A2;
						case 28:
							num6 = 1;
							num = 7;
							continue;
						case 29:
							if (num2 == 0)
							{
								num = 1;
								continue;
							}
							goto IL_44D;
						case 30:
						{
							if (num7 <= num8)
							{
								num = 0;
								continue;
							}
							HorizontalAlignType horizontalAlignment = A_0.Style.HorizontalAlignment;
							num4 = 1;
							num = 23;
							continue;
						}
						case 31:
							if (num5 < maxColumnCount)
							{
								num = 3;
								continue;
							}
							goto IL_1A2;
						case 32:
							num = 5;
							continue;
						case 33:
							if (num6 == 0)
							{
								num = 28;
								continue;
							}
							goto IL_D4;
						case 34:
							if (A_0.FormulaStringValue == null)
							{
								num = 12;
								continue;
							}
							goto IL_117;
						case 35:
							if (num7 <= num8)
							{
								num = 26;
								continue;
							}
							num6 = A_4.Column;
							num = 33;
							continue;
						}
						break;
						IL_D4:
						num8 += A_2.ᜀ(num6);
						num5 += num4;
						int row;
						A_4.ᜀ(row, num5 + num4);
						num = 4;
						continue;
						IL_117:
						row = A_0.Row;
						num3 = A_0.Column;
						num = 16;
						continue;
						IL_1A2:
						num2 = Math.Min(num5, A_0.Column);
						num = 29;
						continue;
						IL_261:
						goto IL_1A2;
						IL_2BE:
						num5 = num3;
						worksheet = (A_0.Worksheet as Worksheet);
						maxColumnCount = worksheet.ParentWorkbook.MaxColumnCount;
						sizeF = SizeF.Empty;
						key = (long)(A_0.Row | A_0.Column);
						num = 15;
						continue;
						IL_3A0:
						num8 = A_2.ᜀ(num3);
						num7 = (int)sizeF.Width;
						num = 30;
						continue;
						IL_3D8:
						A_4.ᜀ(row, num5 + num4);
						num = 14;
						continue;
						IL_41A:
						num = 27;
					}
				}
				IL_189:
				return result;
				IL_275:
				IL_44D:
				int a_ = Math.Max(num5, A_0.Column);
				int num9 = A_2.ᜀ(A_3, num2 - 1);
				int num10 = A_2.ᜀ(num2, a_);
				return new RectangleF((float)num9, A_1.Y, (float)num10, A_1.Height);
			}
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00010C4C File Offset: 0x0000EE4C
		private float ᜀ(IBorder A_0)
		{
			for (;;)
			{
				LineStyleType lineStyle = A_0.LineStyle;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_11C;
					case 1:
						switch (lineStyle)
						{
						case LineStyleType.Thin:
						case LineStyleType.Dashed:
						case LineStyleType.Dotted:
						case LineStyleType.Hair:
						case LineStyleType.DashDot:
						case LineStyleType.DashDotDot:
							this.ᜂ = 0.75f;
							num = 0;
							continue;
						case LineStyleType.Medium:
						case LineStyleType.MediumDashed:
						case LineStyleType.MediumDashDot:
						case LineStyleType.MediumDashDotDot:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_EC;
							default:
								if (false)
								{
								}
								this.ᜂ = 1.5f;
								num = 7;
								continue;
							}
							break;
						case LineStyleType.Thick:
							this.ᜂ = 2f;
							num = 2;
							continue;
						case LineStyleType.Double:
							this.ᜂ = 0.5f;
							num = 6;
							continue;
						case LineStyleType.SlantedDashDot:
							this.ᜂ = 1.5f;
							num = 5;
							continue;
						default:
							num = 3;
							continue;
						}
						break;
					case 2:
						goto IL_A8;
					case 3:
						if (true)
						{
						}
						num = 4;
						continue;
					case 4:
						goto IL_EC;
					case 5:
						goto IL_134;
					case 6:
						goto IL_104;
					case 7:
						goto IL_DF;
					}
					break;
				}
			}
			IL_A8:
			IL_DF:
			IL_EC:
			IL_104:
			IL_11C:
			IL_134:
			return this.ᜂ;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00010D98 File Offset: 0x0000EF98
		private string ᜁ(string A_0, int A_1)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						IL_96:
						StringBuilder stringBuilder;
						A_0 = stringBuilder.ToString();
						num = 4;
						continue;
					}
					case 1:
						goto IL_5A;
					case 2:
						goto IL_5A;
					case 3:
					{
						StringBuilder stringBuilder = new StringBuilder(A_0);
						int num2 = 0;
						int num3 = 1;
						int length = A_0.Length;
						num = 2;
						continue;
					}
					case 4:
						return A_0;
					case 5:
					{
						int num2;
						int length;
						if (num2 >= length)
						{
							num = 0;
							continue;
						}
						StringBuilder stringBuilder;
						int num3;
						stringBuilder.Insert(num3, '\n');
						num2++;
						num3 += 2;
						num = 1;
						continue;
					}
					}
					if (A_1 == 255)
					{
						num = 3;
						continue;
					}
					break;
					IL_5A:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_96;
					default:
						if (false)
						{
						}
						num = 5;
						break;
					}
				}
				return A_0;
			}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00010E94 File Offset: 0x0000F094
		private int ᜁ(int A_0)
		{
			if (A_0 > 90)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					A_0 -= 90;
					return A_0;
				}
			}
			if (true)
			{
			}
			A_0 = -A_0;
			return A_0;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00010EE4 File Offset: 0x0000F0E4
		private Font ᜀ(string A_0, int A_1)
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				FontStyle fontStyle;
				FontStyle fontStyle2;
				string familyName;
				for (;;)
				{
					fontStyle = FontStyle.Regular;
					fontStyle2 = FontStyle.Regular;
					new Font(SheetFinishedEventHandler.b("튺킼\udabe닀评ꋆ뻈鿌ꃎ볐닒믔", a_), 12f);
					familyName = SheetFinishedEventHandler.b("튺킼\udabe닀评ꋆ뻈鿌ꃎ볐닒믔", a_);
					string[] array = A_0.Split(new char[]
					{
						'"'
					})[1].Split(new char[]
					{
						','
					});
					int num = 11;
					for (;;)
					{
						IL_19:
						int num2;
						switch (num)
						{
						case 0:
							goto IL_ED;
						case 1:
						{
							string[] array2;
							while (array2[num2] == SheetFinishedEventHandler.b("﮸풺톼\udbbe", a_))
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									num = 3;
									goto IL_19;
								}
							}
							goto IL_ED;
						}
						case 2:
						{
							familyName = array[0];
							string[] array2 = array[1].Split(new char[]
							{
								' '
							});
							num2 = 0;
							num = 7;
							continue;
						}
						case 3:
							fontStyle = FontStyle.Bold;
							num = 0;
							continue;
						case 4:
						{
							string[] array2;
							if (array2[num2] == SheetFinishedEventHandler.b("쾺\udcbc펾ꣀꃂ", a_))
							{
								num = 6;
								continue;
							}
							goto IL_1C5;
						}
						case 5:
							goto IL_1C5;
						case 6:
							fontStyle2 = FontStyle.Italic;
							num = 5;
							continue;
						case 7:
							goto IL_174;
						case 8:
							goto IL_194;
						case 9:
							goto IL_174;
						case 10:
						{
							string[] array2;
							if (num2 >= array2.Length)
							{
								num = 8;
								continue;
							}
							num = 1;
							continue;
						}
						case 11:
							if (array.Length > 1)
							{
								num = 2;
								continue;
							}
							goto IL_1F4;
						}
						break;
						IL_ED:
						num = 4;
						continue;
						IL_174:
						num = 10;
						continue;
						IL_1C5:
						if (true)
						{
						}
						num2++;
						num = 9;
					}
				}
				IL_194:
				IL_1F4:
				return new Font(familyName, (float)A_1, fontStyle | fontStyle2);
			}
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000110F0 File Offset: 0x0000F2F0
		private Font ᜀ(ExcelFont A_0, string A_1, int A_2)
		{
			int a_ = 5;
			FontStyle fontStyle;
			FontStyle fontStyle2;
			FontStyle fontStyle3;
			for (;;)
			{
				new Font(A_1, (float)A_2);
				fontStyle = FontStyle.Regular;
				int num = 3;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						fontStyle = FontStyle.Bold;
						num = 1;
						continue;
					case 1:
						goto IL_B2;
					case 2:
						fontStyle2 = FontStyle.Underline;
						num = 9;
						continue;
					case 3:
						if (A_0.IsBold)
						{
							num = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_12A;
						default:
							if (false)
							{
							}
							fontStyle = FontStyle.Regular;
							num = 7;
							continue;
						}
						break;
					case 4:
						goto IL_12A;
					case 5:
						fontStyle3 = FontStyle.Italic;
						num = 4;
						continue;
					case 6:
						if (A_0.Underline.ToString() == SheetFinishedEventHandler.b("ힽ꺿ꗁꣃꏅ", a_))
						{
							num = 2;
							continue;
						}
						goto IL_12F;
					case 7:
						goto IL_B2;
					case 8:
						if (A_0.IsItalic)
						{
							num = 5;
							continue;
						}
						goto IL_73;
					case 9:
						goto IL_F0;
					}
					break;
					IL_73:
					fontStyle2 = FontStyle.Regular;
					num = 6;
					continue;
					IL_12A:
					goto IL_73;
					IL_B2:
					fontStyle3 = FontStyle.Regular;
					num = 8;
				}
			}
			IL_F0:
			IL_12F:
			return new Font(A_1, (float)A_2, fontStyle | fontStyle3 | fontStyle2);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0001123C File Offset: 0x0000F43C
		private Color ᜀ(string A_0)
		{
			switch (0)
			{
			default:
			{
				Color result;
				for (;;)
				{
					result = default(Color);
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_B8;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								if (A_0.Length > 1)
								{
									num = 1;
									continue;
								}
								return result;
							}
							break;
						case 1:
						{
							int red = int.Parse(A_0.Substring(1, 2), NumberStyles.HexNumber);
							int green = int.Parse(A_0.Substring(3, 2), NumberStyles.HexNumber);
							int blue = int.Parse(A_0.Substring(5, 2), NumberStyles.HexNumber);
							result = Color.FromArgb(red, green, blue);
							goto IL_B8;
						}
						case 2:
							return result;
						}
						break;
						IL_B8:
						num = 2;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00011310 File Offset: 0x0000F510
		private float ᜀ(PdfDocument A_0, Worksheet A_1, string A_2, PdfPageTemplateElement A_3, float A_4, Dictionary<int, Dictionary<Color, Font>> A_5, float A_6)
		{
			int a_ = 4;
			switch (0)
			{
			default:
			{
				Font font = new Font(SheetFinishedEventHandler.b("풼튾꓀냂视곈볊鷎뻐뻒듔맖", a_), 12f);
				Color color = default(Color);
				Dictionary<int, Dictionary<Color, Font>>.Enumerator enumerator = A_5.GetEnumerator();
				try
				{
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (!enumerator.MoveNext())
							{
								num = 1;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
							{
								if (false)
								{
								}
								KeyValuePair<int, Dictionary<Color, Font>> keyValuePair = enumerator.Current;
								Dictionary<Color, Font>.Enumerator enumerator2 = keyValuePair.Value.GetEnumerator();
								num = 4;
								continue;
							}
							}
							break;
						case 1:
							goto IL_156;
						case 2:
							goto IL_162;
						case 4:
							try
							{
								num = 4;
								for (;;)
								{
									switch (num)
									{
									case 0:
										num = 3;
										continue;
									case 1:
									{
										Dictionary<Color, Font>.Enumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num = 0;
											continue;
										}
										KeyValuePair<Color, Font> keyValuePair2 = enumerator2.Current;
										color = keyValuePair2.Key;
										font = keyValuePair2.Value;
										num = 2;
										continue;
									}
									case 3:
										goto IL_143;
									}
									IL_FA:
									num = 1;
									continue;
									goto IL_FA;
								}
								IL_143:
								break;
							}
							finally
							{
								Dictionary<Color, Font>.Enumerator enumerator2;
								((IDisposable)enumerator2).Dispose();
							}
							goto IL_156;
						}
						IL_B2:
						num = 0;
						continue;
						goto IL_B2;
						IL_156:
						num = 2;
					}
					IL_162:;
				}
				finally
				{
					if (true)
					{
					}
					((IDisposable)enumerator).Dispose();
				}
				A_3.Graphics.DrawString(A_2, new PdfTrueTypeFont(font, this.ᜌ.EmbedFonts), new PdfSolidBrush(color), new PointF(A_4, A_6));
				return A_6 + font.Size;
			}
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00011508 File Offset: 0x0000F708
		private void ᜀ(Dictionary<string, string> A_0, PdfDocument A_1, Worksheet A_2, float A_3, PdfPageTemplateElement A_4, string A_5, float A_6)
		{
			int a_ = 7;
			switch (0)
			{
			default:
			{
				string[] array = new string[10];
				DateTime now = DateTime.Now;
				float a_2 = 0f;
				Dictionary<string, string>.Enumerator enumerator = A_0.GetEnumerator();
				try
				{
					int num = 25;
					for (;;)
					{
						string[] array2;
						int num2;
						int num3;
						Dictionary<int, Dictionary<Color, Font>> a_3;
						StringBuilder stringBuilder;
						KeyValuePair<string, string> keyValuePair;
						float a_4;
						switch (num)
						{
						case 0:
							goto IL_885;
						case 1:
							num = 19;
							continue;
						case 2:
						{
							string text;
							int startIndex = text.LastIndexOf('"');
							text = text.Substring(startIndex, text.Length);
							num = 24;
							continue;
						}
						case 3:
						{
							string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), SheetFinishedEventHandler.b("钽닁ꃃꃅ", a_));
							Array.Copy(array2, num2, array, 0, num3 - num2);
							a_3 = this.ᜀ(array, num3 - num2);
							stringBuilder.Append(files[0]);
							num2 = num3 + 1;
							num = 13;
							continue;
						}
						case 4:
						{
							string key;
							if ((key = array2[num3].Trim()) != null)
							{
								num = 8;
								continue;
							}
							goto IL_1C7;
						}
						case 5:
							goto IL_3CB;
						case 6:
							goto IL_1C7;
						case 7:
							goto IL_370;
						case 8:
							num = 39;
							continue;
						case 9:
							if (num3 >= array2.Length)
							{
								num = 1;
								continue;
							}
							num = 4;
							continue;
						case 10:
							num = 12;
							continue;
						case 11:
							num = 16;
							continue;
						case 12:
							goto IL_8D0;
						case 13:
							goto IL_1C7;
						case 14:
							goto IL_1C7;
						case 15:
							if (array2[array2.Length - 1].Length > 1)
							{
								num = 28;
								continue;
							}
							break;
						case 16:
							goto IL_1C7;
						case 17:
							goto IL_1C7;
						case 18:
							goto IL_89C;
						case 19:
							if (stringBuilder.Length != 0)
							{
								num = 30;
								continue;
							}
							goto IL_15A;
						case 20:
						{
							FileInfo[] files2;
							if (files2.Length != 0)
							{
								num = 21;
								continue;
							}
							goto IL_885;
						}
						case 21:
						{
							FileInfo[] files2;
							stringBuilder.Append(files2[0].ToString());
							num = 0;
							continue;
						}
						case 22:
							goto IL_1C7;
						case 23:
							a_2 = 0f;
							num = 7;
							continue;
						case 24:
							goto IL_7E7;
						case 26:
						{
							string text = text.Substring(7, text.Length - 7);
							num = 36;
							continue;
						}
						case 27:
							goto IL_1C7;
						case 28:
						{
							string text = array2[array2.Length - 1];
							num = 33;
							continue;
						}
						case 29:
							if (keyValuePair.Key == SheetFinishedEventHandler.b("", a_))
							{
								num = 41;
								continue;
							}
							goto IL_370;
						case 30:
							a_4 = this.ᜀ(A_1, A_2, stringBuilder.ToString(), A_4, a_2, a_3, a_4);
							num = 40;
							continue;
						case 31:
							if (keyValuePair.Key == SheetFinishedEventHandler.b("", a_))
							{
								num = 23;
								continue;
							}
							num = 38;
							continue;
						case 32:
							goto IL_1C7;
						case 33:
						{
							string text;
							if (text[0] == 'K')
							{
								num = 26;
								continue;
							}
							num = 35;
							continue;
						}
						case 34:
							if (!enumerator.MoveNext())
							{
								num = 10;
								continue;
							}
							keyValuePair = enumerator.Current;
							num = 31;
							continue;
						case 35:
						{
							string text;
							if (text[0] == '"')
							{
								num = 2;
								continue;
							}
							goto IL_7E7;
						}
						case 36:
							goto IL_7E7;
						case 37:
						{
							if (array2[num3 - 1] == SheetFinishedEventHandler.b("", a_))
							{
								num = 3;
								continue;
							}
							FileInfo[] files2 = new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles(SheetFinishedEventHandler.b("钽닁ꃃꃅ", a_));
							Array.Copy(array2, num2, array, 0, num3 - num2);
							a_3 = this.ᜀ(array, num3 - num2);
							num = 20;
							continue;
						}
						case 38:
							if (keyValuePair.Key == SheetFinishedEventHandler.b("ﶽ", a_))
							{
								num = 44;
								continue;
							}
							num = 29;
							continue;
						case 39:
							if (spr\u224A.ᜄ == null)
							{
								num = 49;
								continue;
							}
							goto IL_3CB;
						case 40:
							goto IL_15A;
						case 41:
							a_2 = A_3;
							num = 47;
							continue;
						case 42:
						{
							int num4;
							switch (num4)
							{
							case 0:
								Array.Copy(array2, num2, array, 0, num3 - num2);
								a_3 = this.ᜀ(array, num3 - num2);
								stringBuilder.Append(A_2.Name);
								num2 = num3 + 1;
								num = 48;
								continue;
							case 1:
								Array.Copy(array2, num2, array, 0, num3 - num2);
								a_3 = this.ᜀ(array, num3 - num2);
								stringBuilder.Append(this.ᜈ++);
								num2 = num3 + 1;
								num = 22;
								continue;
							case 2:
								Array.Copy(array2, num2, array, 0, num3 - num2);
								a_3 = this.ᜀ(array, num3 - num2);
								stringBuilder.Append(this.ᜈ);
								num2 = num3 + 1;
								num = 17;
								continue;
							case 3:
								Array.Copy(array2, num2, array, 0, num3 - num2);
								a_3 = this.ᜀ(array, num3 - num2);
								stringBuilder.Append(now.ToShortDateString());
								num2 = num3 + 1;
								num = 6;
								continue;
							case 4:
								Array.Copy(array2, num2, array, 0, num3 - num2);
								a_3 = this.ᜀ(array, num3 - num2);
								stringBuilder.Append(now.ToShortTimeString());
								num2 = num3 + 1;
								num = 14;
								continue;
							case 5:
								num = 37;
								continue;
							case 6:
								a_2 = this.ᜀ(A_2, this.ᜉ.PageSettings.Width, A_6, A_4, keyValuePair.Key, A_5);
								num = 27;
								continue;
							default:
								num = 11;
								continue;
							}
							break;
						}
						case 44:
							a_2 = A_3 / 2f;
							num = 51;
							continue;
						case 45:
							num = 42;
							continue;
						case 46:
							goto IL_89C;
						case 47:
							IL_3C9:
							goto IL_370;
						case 48:
							goto IL_1C7;
						case 49:
							spr\u224A.ᜄ = new Dictionary<string, int>(7)
							{
								{
									SheetFinishedEventHandler.b("ﾽ", a_),
									0
								},
								{
									SheetFinishedEventHandler.b("", a_),
									1
								},
								{
									SheetFinishedEventHandler.b("", a_),
									2
								},
								{
									SheetFinishedEventHandler.b("諾", a_),
									3
								},
								{
									SheetFinishedEventHandler.b("", a_),
									4
								},
								{
									SheetFinishedEventHandler.b("", a_),
									5
								},
								{
									SheetFinishedEventHandler.b("尿", a_),
									6
								}
							};
							num = 5;
							continue;
						case 50:
						{
							string key;
							int num4;
							if (spr\u224A.ᜄ.TryGetValue(key, out num4))
							{
								num = 45;
								continue;
							}
							goto IL_1C7;
						}
						case 51:
							goto IL_370;
						}
						goto IL_126;
						IL_15A:
						num = 15;
						continue;
						IL_1C7:
						num3++;
						num = 46;
						continue;
						IL_2B2:
						num = 34;
						continue;
						IL_126:
						goto IL_2B2;
						IL_370:
						array2 = keyValuePair.Value.Split(new char[]
						{
							'&'
						});
						a_3 = new Dictionary<int, Dictionary<Color, Font>>();
						num2 = 1;
						a_4 = 0f;
						stringBuilder = new StringBuilder();
						num3 = 0;
						num = 18;
						continue;
						IL_7E7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3C9;
						default:
						{
							if (false)
							{
							}
							Array.Copy(array2, num2, array, 0, array2.Length - num2);
							a_3 = this.ᜀ(array, array2.Length - num2);
							string text;
							a_4 = this.ᜀ(A_1, A_2, text, A_4, a_2, a_3, a_4);
							num = 43;
							continue;
						}
						}
						IL_3CB:
						num = 50;
						continue;
						IL_885:
						num2 = num3 + 1;
						num = 32;
						continue;
						IL_89C:
						num = 9;
					}
					IL_8D0:;
				}
				finally
				{
					if (true)
					{
					}
					((IDisposable)enumerator).Dispose();
				}
				return;
			}
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00011E1C File Offset: 0x0001001C
		private Dictionary<int, Dictionary<Color, Font>> ᜀ(string[] A_0, int A_1)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				Dictionary<int, Dictionary<Color, Font>> dictionary;
				Dictionary<Color, Font> dictionary2;
				Font value;
				Color key;
				for (;;)
				{
					dictionary = new Dictionary<int, Dictionary<Color, Font>>();
					dictionary2 = new Dictionary<Color, Font>();
					int num = 8;
					value = new Font(SheetFinishedEventHandler.b("鷈ꋊꃌ꫎ꋐ鯔닖께ﯚ远냞賠苢诤", a_), (float)num);
					string text = null;
					key = Color.FromArgb(255, 0, 0, 0);
					int num2 = 0;
					int num3 = 1;
					for (;;)
					{
						if (true)
						{
						}
						switch (num3)
						{
						case 0:
							goto IL_17B;
						case 1:
							goto IL_AF;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_18C;
							default:
								if (false)
								{
								}
								if (A_0[num2].Substring(0, 1) == SheetFinishedEventHandler.b("苈", a_))
								{
									num3 = 3;
									continue;
								}
								goto IL_1F0;
							}
							break;
						case 3:
							key = this.ᜀ(A_0[num2]);
							goto IL_18C;
						case 4:
							goto IL_AF;
						case 5:
							num3 = 10;
							continue;
						case 6:
							if (A_0[num2].Length == 2)
							{
								num3 = 8;
								continue;
							}
							goto IL_19A;
						case 7:
							goto IL_19A;
						case 8:
							num = int.Parse(A_0[num2].Trim(), NumberStyles.Any);
							num3 = 7;
							continue;
						case 9:
							value = this.ᜀ(text, num);
							num3 = 0;
							continue;
						case 10:
							if (text != null)
							{
								num3 = 9;
								continue;
							}
							goto IL_21D;
						case 11:
							goto IL_14E;
						case 12:
							goto IL_1F0;
						case 13:
							text = A_0[num2];
							num3 = 11;
							continue;
						case 14:
							if (num2 >= A_1)
							{
								num3 = 5;
								continue;
							}
							num3 = 6;
							continue;
						case 15:
							if (A_0[num2].IndexOf('"') == 0)
							{
								num3 = 13;
								continue;
							}
							goto IL_14E;
						}
						break;
						IL_AF:
						num3 = 14;
						continue;
						IL_14E:
						num2++;
						num3 = 4;
						continue;
						IL_18C:
						num3 = 12;
						continue;
						IL_19A:
						num3 = 2;
						continue;
						IL_1F0:
						num3 = 15;
					}
				}
				IL_17B:
				IL_21D:
				dictionary2.Add(key, value);
				dictionary.Add(1, dictionary2);
				return dictionary;
			}
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00012064 File Offset: 0x00010264
		private PdfTextAlignment ᜀ(IExtendedFormat A_0, XlsRange A_1)
		{
			for (;;)
			{
				int horizontalAlignment = (int)A_0.HorizontalAlignment;
				int num = 8;
				for (;;)
				{
					XlsWorkbook xlsWorkbook;
					switch (num)
					{
					case 0:
						return PdfTextAlignment.Left;
					case 1:
						if (A_1.FormulaStringValue == null)
						{
							num = 4;
							continue;
						}
						return PdfTextAlignment.Left;
					case 2:
						num = 12;
						continue;
					case 3:
						if (A_1.HasFormula)
						{
							num = 16;
							continue;
						}
						return PdfTextAlignment.Left;
					case 4:
						num = 13;
						continue;
					case 5:
						num = 0;
						continue;
					case 6:
						num = 9;
						continue;
					case 7:
						if (A_0.Rotation != 255)
						{
							num = 2;
							continue;
						}
						return PdfTextAlignment.Center;
					case 8:
						switch (horizontalAlignment)
						{
						case 0:
							num = 7;
							continue;
						case 1:
						case 4:
						case 5:
							return PdfTextAlignment.Left;
						case 2:
						case 6:
							return PdfTextAlignment.Center;
						case 3:
							return PdfTextAlignment.Right;
						default:
							num = 5;
							continue;
						}
						break;
					case 9:
						if (!A_1.HasFormulaBoolValue)
						{
							num = 15;
							continue;
						}
						return PdfTextAlignment.Left;
					case 10:
						if (xlsWorkbook.InnerFormats.ᜁ(A_0.NumberFormatIndex).ᜀ(A_1.NumberValue) != CellFormatType.Text)
						{
							num = 14;
							continue;
						}
						return PdfTextAlignment.Left;
					case 11:
						num = 3;
						continue;
					case 12:
						if (!A_1.HasNumber)
						{
							num = 11;
							continue;
						}
						goto IL_8F;
					case 13:
						if (!A_1.HasFormulaErrorValue)
						{
							num = 6;
							continue;
						}
						return PdfTextAlignment.Left;
					case 14:
						goto IL_D5;
					case 15:
						goto IL_8F;
					case 16:
						if (true)
						{
						}
						num = 1;
						continue;
					}
					break;
					IL_8F:
					xlsWorkbook = (A_1.Worksheet.Workbook as XlsWorkbook);
					num = 10;
				}
			}
			return PdfTextAlignment.Left;
			IL_D5:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return PdfTextAlignment.Left;
			default:
				if (false)
				{
				}
				return PdfTextAlignment.Right;
			}
			return PdfTextAlignment.Center;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00012260 File Offset: 0x00010460
		private Rectangle ᜀ(Worksheet A_0, spr\u25A6.ᜀ A_1, int A_2, int A_3, sprᱥ A_4, sprᱥ A_5)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			int num = A_4.ᜁ(A_1.ᜂ());
			int num2 = A_5.ᜁ(A_1.ᜅ());
			num -= A_4.ᜁ(A_2 - 1);
			num2 -= A_5.ᜁ(A_3 - 1);
			int height = A_4.ᜀ(A_1.ᜂ() + 1, A_1.ᜇ() + 1);
			int width = A_5.ᜀ(A_1.ᜅ() + 1, A_1.ᜃ() + 1);
			return new Rectangle(num2, num, width, height);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0001230C File Offset: 0x0001050C
		private Point ᜀ(IShape A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			int left = A_0.Left;
			int top = A_0.Top;
			return new Point(left, top);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0001235C File Offset: 0x0001055C
		private float ᜀ(spr\u192F A_0)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				Bitmap bitmap = new Bitmap(1, 1);
				float result;
				try
				{
					if (true)
					{
					}
					Graphics graphics = Graphics.FromImage(bitmap);
					Font font = A_0.ᜀ().GenerateNativeFont();
					string text = SheetFinishedEventHandler.b("ힹ", a_);
					string text2 = SheetFinishedEventHandler.b("ힹ톻", a_);
					SizeF sizeF = graphics.MeasureString(text, font);
					float num = graphics.MeasureString(text2, font).Width - sizeF.Width;
					result = (sizeF.Width - num) * 0.8f;
				}
				finally
				{
					for (;;)
					{
						IL_95:
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 1:
								goto IL_EC;
							case 2:
								((IDisposable)bitmap).Dispose();
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_95;
								default:
									if (false)
									{
									}
									num2 = 1;
									continue;
								}
								break;
							}
							if (bitmap == null)
							{
								goto IL_EE;
							}
							num2 = 2;
						}
					}
					IL_EC:
					IL_EE:;
				}
				return result;
			}
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00012474 File Offset: 0x00010674
		private float ᜀ(XlsRange A_0, int A_1)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					Bitmap bitmap;
					IFont font2;
					switch (num)
					{
					case 0:
						try
						{
							for (;;)
							{
								Graphics graphics = Graphics.FromImage(bitmap);
								Font font = font2.GenerateNativeFont();
								font = new Font(font.FontFamily, font.Size, font.Style);
								float num2 = 0f;
								int num3 = 26;
								int num4 = 0;
								num = 5;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										int num5;
										if (num5 >= num3)
										{
											num = 6;
											continue;
										}
										string text = new string((char)(num5 + 65), 1);
										num2 += graphics.MeasureString(text, font).Width;
										num5++;
										num = 7;
										continue;
									}
									case 1:
										goto IL_CB;
									case 2:
									{
										int num5 = 0;
										num = 1;
										continue;
									}
									case 3:
									{
										if (num4 >= num3)
										{
											num = 2;
											continue;
										}
										string text2 = new string((char)(num4 + 97), 1);
										num2 += graphics.MeasureString(text2, font).Width;
										num4++;
										num = 8;
										continue;
									}
									case 4:
										goto IL_1C5;
									case 5:
										goto IL_174;
									case 6:
										this.\u1712 = num2 / (float)(2 * num3);
										this.\u1712 = (float)Math.Round((double)this.\u1712, MidpointRounding.AwayFromZero);
										num = 4;
										continue;
									case 7:
										goto IL_CB;
									case 8:
										goto IL_174;
									}
									break;
									IL_CB:
									num = 0;
									continue;
									IL_174:
									num = 3;
								}
							}
							IL_1C5:
							goto IL_25E;
						}
						finally
						{
							for (;;)
							{
								IL_1CA:
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_221;
									case 2:
										((IDisposable)bitmap).Dispose();
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_1CA;
										default:
											if (false)
											{
											}
											num = 0;
											continue;
										}
										break;
									}
									if (bitmap == null)
									{
										goto IL_223;
									}
									num = 2;
								}
							}
							IL_221:
							IL_223:;
						}
						goto IL_224;
					case 1:
						goto IL_224;
					}
					if (this.\u1712 <= 0f)
					{
						num = 1;
						continue;
					}
					break;
					IL_224:
					font2 = A_0.Workbook.InnerExtFormats.ᜁ(A_0.Workbook.DefaultXFIndex).ᜀ();
					bitmap = new Bitmap(1, 1);
					num = 0;
				}
				IL_25E:
				return this.\u1712 * (float)A_1;
			}
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00012704 File Offset: 0x00010904
		private void ᜀ(BordersLineType A_0, out BordersLineType A_1, out BordersLineType A_2)
		{
			if (true)
			{
			}
			A_1 = (BordersLineType)(-1);
			A_2 = (BordersLineType)(-1);
			switch (A_0)
			{
			case BordersLineType.EdgeLeft:
			case BordersLineType.EdgeRight:
				A_1 = BordersLineType.EdgeTop;
				A_2 = BordersLineType.EdgeBottom;
				return;
			case BordersLineType.EdgeTop:
			case BordersLineType.EdgeBottom:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					A_1 = BordersLineType.EdgeLeft;
					A_2 = BordersLineType.EdgeRight;
					return;
				}
				break;
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00012774 File Offset: 0x00010974
		private PdfTextAlignment ᜁ(ITextBoxShape A_0)
		{
			for (;;)
			{
				CommentHAlignType halignment = A_0.HAlignment;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4F;
					case 1:
						switch (halignment)
						{
						case CommentHAlignType.Left:
							return PdfTextAlignment.Left;
						case CommentHAlignType.Center:
							return PdfTextAlignment.Center;
						case CommentHAlignType.Right:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_4F;
							default:
								goto IL_6F;
							}
							break;
						default:
							num = 0;
							continue;
						}
						break;
					case 2:
						goto IL_57;
					}
					break;
					IL_4F:
					num = 2;
				}
			}
			return PdfTextAlignment.Center;
			IL_57:
			if (true)
			{
			}
			return PdfTextAlignment.Justify;
			IL_6F:
			if (false)
			{
			}
			return PdfTextAlignment.Right;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00012804 File Offset: 0x00010A04
		private PdfVerticalAlignment ᜀ(IExtendedFormat A_0)
		{
			for (;;)
			{
				IL_00:
				for (;;)
				{
					VerticalAlignType verticalAlignment = A_0.VerticalAlignment;
					int num = 1;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							num = 2;
							continue;
						case 1:
							switch (verticalAlignment)
							{
							case VerticalAlignType.Top:
								return PdfVerticalAlignment.Top;
							case VerticalAlignType.Center:
								return PdfVerticalAlignment.Middle;
							default:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_00;
								}
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						case 2:
							return PdfVerticalAlignment.Bottom;
						}
						break;
					}
				}
			}
			return PdfVerticalAlignment.Middle;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0001288C File Offset: 0x00010A8C
		private PdfVerticalAlignment ᜀ(ITextBoxShape A_0)
		{
			for (;;)
			{
				IL_00:
				for (;;)
				{
					if (true)
					{
					}
					CommentVAlignType valignment = A_0.VAlignment;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return PdfVerticalAlignment.Top;
						case 1:
							switch (valignment)
							{
							case CommentVAlignType.Center:
								return PdfVerticalAlignment.Middle;
							case CommentVAlignType.Bottom:
								return PdfVerticalAlignment.Bottom;
							default:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_00;
								}
								if (false)
								{
								}
								num = 2;
								continue;
							}
							break;
						case 2:
							num = 0;
							continue;
						}
						break;
					}
				}
			}
			return PdfVerticalAlignment.Bottom;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00012914 File Offset: 0x00010B14
		private PdfPageBase ᜁ()
		{
			switch (0)
			{
			default:
			{
				PdfPageBase pdfPageBase;
				for (;;)
				{
					pdfPageBase = null;
					int num = 9;
					for (;;)
					{
						PdfPageSettings pdfPageSettings;
						switch (num)
						{
						case 0:
							pdfPageSettings = this.ᜋ[this.ᜏ];
							num = 12;
							continue;
						case 1:
							pdfPageBase = this.ᜄ.Pages.Add();
							num = 10;
							continue;
						case 2:
							num = 4;
							continue;
						case 3:
						{
							if (this.ᜋ.ContainsKey(this.ᜏ))
							{
								num = 0;
								continue;
							}
							pdfPageSettings = new PdfPageSettings();
							PageSetup pageSetup = this.ᜏ.PageSetup;
							num = 7;
							continue;
						}
						case 4:
							if (this.ᜌ.IsUsingExcelPageSetup)
							{
								num = 14;
								continue;
							}
							goto IL_2B6;
						case 5:
							goto IL_F1;
						case 6:
							goto IL_1CE;
						case 7:
						{
							PageSetup pageSetup;
							if (pageSetup == null)
							{
								num = 15;
								continue;
							}
							pdfPageSettings.Width = (float)pageSetup.PageWidth;
							pdfPageSettings.Height = (float)pageSetup.PageHeight;
							float left = this.\u170D.ConvertUnits((float)pageSetup.LeftMargin, PdfGraphicsUnit.Inch, PdfGraphicsUnit.Point);
							float top = this.\u170D.ConvertUnits((float)pageSetup.TopMargin, PdfGraphicsUnit.Inch, PdfGraphicsUnit.Point);
							float right = this.\u170D.ConvertUnits((float)pageSetup.RightMargin, PdfGraphicsUnit.Inch, PdfGraphicsUnit.Point);
							float bottom = this.\u170D.ConvertUnits((float)pageSetup.BottomMargin, PdfGraphicsUnit.Inch, PdfGraphicsUnit.Point);
							pdfPageSettings.SetMargins(left, top, right, bottom);
							num = 20;
							continue;
						}
						case 8:
							if (this.ᜄ != null)
							{
								num = 1;
								continue;
							}
							goto IL_294;
						case 9:
							if (this.ᜌ != null)
							{
								num = 2;
								continue;
							}
							goto IL_2B6;
						case 10:
							goto IL_F1;
						case 11:
							goto IL_F1;
						case 12:
							goto IL_F1;
						case 13:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_F1;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								num = 8;
								continue;
							}
							break;
						case 14:
							pdfPageSettings = null;
							num = 3;
							continue;
						case 15:
							pdfPageSettings.Size = PdfPageSize.A4;
							pdfPageSettings.SetMargins(51f, 54f, 51f, 54f);
							num = 18;
							continue;
						case 16:
							pdfPageBase.Canvas.Save();
							pdfPageBase.Canvas.ScaleTransform(this.\u1719, this.\u1719);
							num = 6;
							continue;
						case 17:
							if (this.FitToPage != FitToPageType.None)
							{
								num = 13;
								continue;
							}
							goto IL_294;
						case 18:
							goto IL_AE;
						case 19:
							if (this.\u1719 != 1f)
							{
								num = 16;
								continue;
							}
							goto IL_348;
						case 20:
							goto IL_AE;
						}
						break;
						IL_AE:
						this.ᜋ[this.ᜏ] = pdfPageSettings;
						num = 11;
						continue;
						IL_F1:
						num = 19;
						continue;
						IL_294:
						pdfPageBase = this.ᜉ.Pages.Add();
						num = 5;
						continue;
						IL_2B6:
						num = 17;
					}
				}
				IL_1CE:
				IL_348:
				this.\u1716.Add(this.ᜏ);
				return pdfPageBase;
			}
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00012C7C File Offset: 0x00010E7C
		private void ᜀ()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜅ = this.ᜁ();
			this.ᜇ = this.ᜅ.Canvas;
			this.ᜈ++;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00012CE4 File Offset: 0x00010EE4
		private void ᜀ(Worksheet A_0, int A_1, int A_2, int A_3, int A_4, PdfCanvas A_5, sprᱥ A_6, sprᱥ A_7, PdfConverter.ᜁ A_8)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
					{
						if (false)
						{
						}
						List<spr\u25A6.ᜀ> list = new List<spr\u25A6.ᜀ>();
						A_0.MergeCells.ᜀ(A_0[A_1, A_2, A_3, A_4], list);
						int num2 = 0;
						int count = list.Count;
						break;
					}
					}
					num = 5;
					continue;
				case 1:
					goto IL_63;
				case 2:
					return;
				case 3:
				{
					if (true)
					{
					}
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					List<spr\u25A6.ᜀ> list;
					A_8(A_0, list[num2], A_1, A_2, A_5, A_6, A_7);
					num2++;
					num = 1;
					continue;
				}
				case 5:
					goto IL_63;
				}
				if (A_0.HasMergedCells)
				{
					num = 0;
					continue;
				}
				break;
				IL_63:
				num = 3;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00012DD0 File Offset: 0x00010FD0
		private Color ᜀ(Color A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return Color.FromArgb(255, (int)A_0.R, (int)A_0.G, (int)A_0.B);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00012E2C File Offset: 0x0001102C
		internal void ᜀ(int A_0, int A_1)
		{
			for (;;)
			{
				IL_00:
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
					{
						ProgressEventArgs args = new ProgressEventArgs(A_0, A_1, this);
						this.\u171D(this, args);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					case 2:
						if (true)
						{
						}
						break;
					}
					if (this.\u171D == null)
					{
						return;
					}
					num = 1;
				}
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00012EB8 File Offset: 0x000110B8
		internal void ᜂ(int A_0)
		{
			for (;;)
			{
				IL_00:
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 2:
					{
						SheetFinishedEventArgs args = new SheetFinishedEventArgs(A_0, this);
						this.\u171E(this, args);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					}
					if (true)
					{
					}
					if (this.\u171E == null)
					{
						return;
					}
					num = 2;
				}
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00012F40 File Offset: 0x00011140
		internal void ᜀ(SheetStartEventArgs A_0)
		{
			for (;;)
			{
				IL_00:
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u171F(this, A_0);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					}
					if (this.\u171F == null)
					{
						return;
					}
					num = 0;
				}
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00012FC0 File Offset: 0x000111C0
		private bool ᜀ(int A_0)
		{
			bool result;
			for (;;)
			{
				for (;;)
				{
					result = false;
					if (true)
					{
					}
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							SheetStartEventArgs sheetStartEventArgs = new SheetStartEventArgs(A_0, this);
							this.ᜀ(sheetStartEventArgs);
							result = sheetStartEventArgs.Skip;
							num = 1;
							continue;
						}
						case 1:
							return result;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								if (this.\u171F != null)
								{
									num = 0;
									continue;
								}
								return result;
							}
							break;
						}
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0001304C File Offset: 0x0001124C
		private PointF[] ᜀ(SizeF A_0, int A_1)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			Matrix matrix = new Matrix();
			PointF[] array = new PointF[]
			{
				new PointF(0f, 0f),
				new PointF(0f, A_0.Height),
				new PointF(A_0.Width, A_0.Height),
				new PointF(A_0.Width, 0f)
			};
			matrix.Rotate((float)A_1);
			matrix.TransformPoints(array);
			return array;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0001311C File Offset: 0x0001131C
		private void ᜀ(PdfDocument A_0)
		{
			for (;;)
			{
				Workbook workbook = this.ᜆ;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_17A;
					case 1:
						A_0.DocumentInformation.Author = workbook.DocumentProperties.Author;
						A_0.DocumentInformation.Creator = workbook.DocumentProperties.ApplicationName;
						A_0.DocumentInformation.Producer = workbook.DocumentProperties.Company;
						A_0.DocumentInformation.Title = workbook.DocumentProperties.Title;
						A_0.DocumentInformation.Subject = workbook.DocumentProperties.Subject;
						A_0.DocumentInformation.Keywords = workbook.DocumentProperties.Keywords;
						A_0.DocumentInformation.CreationDate = workbook.DocumentProperties.CreatedTime;
						num = 0;
						continue;
					case 2:
						if (workbook == null)
						{
							num = 6;
							continue;
						}
						goto IL_73;
					case 3:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17C;
						default:
							if (false)
							{
							}
							if (workbook != null)
							{
								num = 9;
								continue;
							}
							goto IL_198;
						}
						break;
					case 4:
						if (this.ᜏ != null)
						{
							num = 7;
							continue;
						}
						goto IL_73;
					case 5:
						if (workbook.DocumentProperties != null)
						{
							num = 1;
							continue;
						}
						goto IL_198;
					case 6:
						num = 4;
						continue;
					case 7:
						goto IL_17C;
					case 8:
						goto IL_73;
					case 9:
						num = 5;
						continue;
					}
					break;
					IL_73:
					num = 3;
					continue;
					IL_17C:
					workbook = this.ᜏ.Workbook;
					num = 8;
				}
			}
			IL_17A:
			IL_198:
			A_0.DocumentInformation.ModificationDate = DateTime.Now;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000132D4 File Offset: 0x000114D4
		private void ᜀ(XlsRange A_0, RectangleF A_1)
		{
			XlsHyperLinksCollection hyperLinks;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_5E:
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
					{
						PdfFileLinkAnnotation pdfFileLinkAnnotation = new PdfFileLinkAnnotation(A_1, hyperLinks.InnerList[0].Address);
						pdfFileLinkAnnotation.Border.Width = 0f;
						this.ᜅ.AnnotationsWidget.Add(pdfFileLinkAnnotation);
						num = 0;
						continue;
					}
					case 2:
						goto IL_112;
					case 3:
						if (hyperLinks.InnerList.Count > 0)
						{
							num = 5;
							continue;
						}
						return;
					case 4:
						if (hyperLinks.InnerList[0].Type == HyperLinkType.Url)
						{
							num = 2;
							continue;
						}
						num = 6;
						continue;
					case 5:
						num = 4;
						continue;
					case 6:
						if (hyperLinks.InnerList[0].Type == HyperLinkType.File)
						{
							num = 1;
							continue;
						}
						return;
					}
					goto IL_4A;
				}
				IL_112:
				PdfUriAnnotation pdfUriAnnotation = new PdfUriAnnotation(A_1, hyperLinks.InnerList[0].Address);
				pdfUriAnnotation.Border.Width = 0f;
				this.ᜅ.AnnotationsWidget.Add(pdfUriAnnotation);
				return;
			}
			default:
				if (false)
				{
				}
				break;
			}
			IL_4A:
			if (true)
			{
			}
			hyperLinks = this.ᜏ.HyperLinks;
			goto IL_5E;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0001343C File Offset: 0x0001163C
		private void ᜀ(IWorksheet A_0, int A_1, int A_2, int A_3, int A_4, ref int A_5, ref int A_6, bool A_7, IBorders A_8, BordersLineType A_9, BordersLineType A_10, bool A_11)
		{
			switch (0)
			{
			default:
			{
				int num6;
				for (;;)
				{
					int num = A_1 + A_3;
					int num2 = A_2 + A_4;
					IWorkbook workbook = A_0.Workbook;
					int maxRowCount = workbook.MaxRowCount;
					int maxColumnCount = workbook.MaxColumnCount;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_177;
					default:
					{
						if (false)
						{
						}
						int num3 = 32;
						for (;;)
						{
							int num4;
							bool flag;
							int num5;
							bool flag2;
							switch (num3)
							{
							case 0:
								if (num2 <= maxColumnCount)
								{
									num3 = 10;
									continue;
								}
								return;
							case 1:
								return;
							case 2:
								num4 = -1;
								goto IL_35F;
							case 3:
								flag = !flag;
								num3 = 6;
								continue;
							case 4:
								num3 = 22;
								continue;
							case 5:
								num3 = 14;
								continue;
							case 6:
								goto IL_238;
							case 7:
								num3 = 20;
								continue;
							case 8:
								if (A_10 != (BordersLineType)(-1))
								{
									num3 = 21;
									continue;
								}
								goto IL_1A1;
							case 9:
								num3 = 13;
								continue;
							case 10:
							{
								IBorders borders = A_0[A_1 + A_3, A_2 + A_4].Borders;
								num3 = 17;
								continue;
							}
							case 11:
								if (true)
								{
								}
								num3 = 31;
								continue;
							case 12:
								if (!A_11)
								{
									num3 = 5;
									continue;
								}
								num3 = 18;
								continue;
							case 13:
								if (num <= maxRowCount)
								{
									num3 = 24;
									continue;
								}
								return;
							case 14:
								num5 = -1;
								goto IL_302;
							case 15:
								goto IL_177;
							case 16:
								if (A_3 != 0)
								{
									num3 = 29;
									continue;
								}
								return;
							case 17:
								if (A_8[A_10].LineStyle != LineStyleType.Double)
								{
									num3 = 7;
									continue;
								}
								num3 = 19;
								continue;
							case 18:
								num5 = 1;
								goto IL_302;
							case 19:
								flag2 = true;
								goto IL_2C7;
							case 20:
							{
								IBorders borders;
								flag2 = (borders[A_9].LineStyle == LineStyleType.Double);
								goto IL_2C7;
							}
							case 21:
								num3 = 30;
								continue;
							case 22:
								num4 = 1;
								goto IL_35F;
							case 23:
								if (A_7)
								{
									num3 = 3;
									continue;
								}
								goto IL_238;
							case 24:
								num3 = 26;
								continue;
							case 25:
								num3 = 0;
								continue;
							case 26:
								if (num2 > 0)
								{
									num3 = 25;
									continue;
								}
								return;
							case 27:
								goto IL_15B;
							case 28:
								if (A_4 != 0)
								{
									num3 = 15;
									continue;
								}
								num3 = 16;
								continue;
							case 29:
								A_6 = num6;
								num3 = 1;
								continue;
							case 30:
								if (flag)
								{
									num3 = 11;
									continue;
								}
								goto IL_1A1;
							case 31:
								if (!A_11)
								{
									num3 = 4;
									continue;
								}
								num3 = 2;
								continue;
							case 32:
								if (num > 0)
								{
									num3 = 9;
									continue;
								}
								return;
							case 33:
								goto IL_15B;
							}
							break;
							IL_15B:
							num3 = 28;
							continue;
							IL_1A1:
							num3 = 12;
							continue;
							IL_238:
							num3 = 8;
							continue;
							IL_2C7:
							flag = flag2;
							num3 = 23;
							continue;
							IL_302:
							num6 = num5;
							num3 = 27;
							continue;
							IL_35F:
							num6 = num4;
							num3 = 33;
						}
						break;
					}
					}
				}
				IL_177:
				A_5 = num6;
				return;
			}
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000137DC File Offset: 0x000119DC
		private RectangleF ᜀ(RectangleF A_0, IBorders A_1)
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					A_0.Height += 1f;
					num = 11;
					continue;
				case 1:
					A_0.Offset(0f, -1f);
					A_0.Height += 1f;
					num = 4;
					continue;
				case 2:
					if (A_1[BordersLineType.EdgeBottom].LineStyle != LineStyleType.None)
					{
						num = 0;
						continue;
					}
					goto IL_153;
				case 3:
					return A_0;
				case 4:
					goto IL_83;
				case 5:
					if (A_1[BordersLineType.EdgeRight].LineStyle != LineStyleType.None)
					{
						num = 7;
						continue;
					}
					return A_0;
				case 7:
					A_0.Width += 1f;
					num = 3;
					continue;
				case 8:
					if (A_1[BordersLineType.EdgeTop].LineStyle != LineStyleType.None)
					{
						num = 1;
						continue;
					}
					goto IL_83;
				case 9:
					A_0.Offset(-1f, 0f);
					A_0.Width += 1f;
					num = 10;
					continue;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						goto IL_AD;
					}
					break;
				case 11:
					goto IL_153;
				}
				IL_40:
				if (true)
				{
				}
				if (A_1[BordersLineType.EdgeLeft].LineStyle != LineStyleType.None)
				{
					num = 9;
					continue;
				}
				goto IL_AD;
				goto IL_40;
				IL_83:
				num = 2;
				continue;
				IL_AD:
				num = 8;
				continue;
				IL_153:
				num = 5;
			}
			return A_0;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0001399C File Offset: 0x00011B9C
		private PdfTrueTypeFont ᜀ(PdfTrueTypeFont A_0)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			float size = A_0.Size * 96f / 72f;
			return new PdfTrueTypeFont(A_0, size);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000139F4 File Offset: 0x00011BF4
		private RectangleF ᜀ(Image A_0, PdfStringFormat A_1)
		{
			switch (0)
			{
			default:
			{
				RectangleF rectangleF = default(RectangleF);
				Bitmap bitmap = new Bitmap(A_0);
				try
				{
					for (;;)
					{
						int num = bitmap.Width;
						int num2 = -1;
						PdfTextAlignment alignment = A_1.Alignment;
						int num3 = 45;
						for (;;)
						{
							int num4;
							int num5;
							int num6;
							int num7;
							int num8;
							int num9;
							int num10;
							int num11;
							switch (num3)
							{
							case 0:
								if (num4 > num2)
								{
									num3 = 24;
									continue;
								}
								goto IL_943;
							case 1:
							{
								if (num4 >= bitmap.Width)
								{
									num3 = 42;
									continue;
								}
								Color pixel = bitmap.GetPixel(num4, num5);
								num3 = 71;
								continue;
							}
							case 2:
								if (true)
								{
								}
								num6 = 0;
								num3 = 4;
								continue;
							case 3:
								if (num7 < num)
								{
									num3 = 19;
									continue;
								}
								goto IL_852;
							case 4:
								goto IL_454;
							case 5:
								goto IL_38A;
							case 6:
								if (num8 > num2)
								{
									num3 = 8;
									continue;
								}
								goto IL_5CD;
							case 7:
								goto IL_254;
							case 8:
								num2 = num8;
								num3 = 50;
								continue;
							case 9:
								goto IL_6A1;
							case 10:
							{
								Color pixel2;
								if (pixel2.R == 255)
								{
									num3 = 31;
									continue;
								}
								goto IL_3EA;
							}
							case 11:
								goto IL_321;
							case 12:
								goto IL_5F8;
							case 13:
							{
								Color pixel3;
								if (pixel3.G == 255)
								{
									num3 = 93;
									continue;
								}
								goto IL_8C5;
							}
							case 14:
							{
								Color pixel;
								if (pixel.R == 255)
								{
									num3 = 29;
									continue;
								}
								goto IL_A17;
							}
							case 15:
								num2 = num9;
								num3 = 80;
								continue;
							case 16:
							{
								Color pixel4;
								if (pixel4.R == 255)
								{
									num3 = 94;
									continue;
								}
								goto IL_66B;
							}
							case 17:
								if (num2 >= num)
								{
									num3 = 2;
									continue;
								}
								num4 = 0;
								num3 = 55;
								continue;
							case 18:
							{
								if (num8 <= num2)
								{
									num3 = 76;
									continue;
								}
								Color pixel3 = bitmap.GetPixel(num8, num5);
								num3 = 22;
								continue;
							}
							case 19:
								num = num7;
								num3 = 26;
								continue;
							case 20:
								goto IL_A3C;
							case 21:
								rectangleF = new RectangleF(0f, 0f, (float)(num2 + 1), (float)bitmap.Height);
								num3 = 27;
								continue;
							case 22:
							{
								Color pixel3;
								if (pixel3.A == 255)
								{
									num3 = 90;
									continue;
								}
								goto IL_8C5;
							}
							case 23:
								goto IL_58D;
							case 24:
								num2 = num4;
								num3 = 53;
								continue;
							case 25:
								goto IL_58D;
							case 26:
								goto IL_A3C;
							case 27:
								goto IL_A8D;
							case 28:
								goto IL_439;
							case 29:
								num3 = 52;
								continue;
							case 30:
							{
								if (num6 >= num)
								{
									num3 = 83;
									continue;
								}
								Color pixel5 = bitmap.GetPixel(num6, num5);
								num3 = 79;
								continue;
							}
							case 31:
								num3 = 56;
								continue;
							case 32:
								goto IL_8C5;
							case 33:
								num3 = 54;
								continue;
							case 34:
							{
								Color pixel5;
								if (pixel5.R == 255)
								{
									num3 = 95;
									continue;
								}
								goto IL_6C6;
							}
							case 35:
								rectangleF = new RectangleF((float)num, 0f, (float)(bitmap.Width - num), (float)bitmap.Height);
								num3 = 77;
								continue;
							case 36:
								num3 = 10;
								continue;
							case 37:
								if (num10 >= bitmap.Height)
								{
									num3 = 21;
									continue;
								}
								num9 = bitmap.Width - 1;
								num3 = 59;
								continue;
							case 38:
								rectangleF = new RectangleF((float)num, 0f, (float)(num2 - num + 1), (float)bitmap.Height);
								num3 = 67;
								continue;
							case 39:
								num3 = 14;
								continue;
							case 40:
								goto IL_38A;
							case 41:
							{
								Color pixel;
								if (pixel.B != 255)
								{
									num3 = 69;
									continue;
								}
								goto IL_943;
							}
							case 42:
								goto IL_5F8;
							case 43:
								goto IL_321;
							case 44:
								if (num6 < num)
								{
									num3 = 87;
									continue;
								}
								goto IL_5E1;
							case 45:
								switch (alignment)
								{
								case PdfTextAlignment.Left:
									num10 = 0;
									num3 = 40;
									continue;
								case PdfTextAlignment.Center:
								case PdfTextAlignment.Justify:
									num5 = 0;
									num3 = 64;
									continue;
								case PdfTextAlignment.Right:
									num11 = 0;
									num3 = 11;
									continue;
								default:
									num3 = 33;
									continue;
								}
								break;
							case 46:
								num3 = 13;
								continue;
							case 47:
							{
								Color pixel2;
								if (pixel2.B != 255)
								{
									num3 = 57;
									continue;
								}
								goto IL_852;
							}
							case 48:
								num3 = 16;
								continue;
							case 49:
								num3 = 34;
								continue;
							case 50:
								goto IL_5F8;
							case 51:
							{
								Color pixel2;
								if (pixel2.A == 255)
								{
									num3 = 36;
									continue;
								}
								goto IL_3EA;
							}
							case 52:
							{
								Color pixel;
								if (pixel.G == 255)
								{
									num3 = 97;
									continue;
								}
								goto IL_A17;
							}
							case 53:
								goto IL_943;
							case 54:
								goto IL_A8D;
							case 55:
								goto IL_7EE;
							case 56:
							{
								Color pixel2;
								if (pixel2.G == 255)
								{
									num3 = 84;
									continue;
								}
								goto IL_3EA;
							}
							case 57:
								goto IL_3EA;
							case 58:
							{
								if (num9 <= num2)
								{
									num3 = 98;
									continue;
								}
								Color pixel4 = bitmap.GetPixel(num9, num10);
								num3 = 63;
								continue;
							}
							case 59:
								goto IL_3B4;
							case 60:
								goto IL_A99;
							case 61:
							{
								Color pixel3;
								if (pixel3.R == 255)
								{
									num3 = 46;
									continue;
								}
								goto IL_8C5;
							}
							case 62:
								if (num11 >= bitmap.Height)
								{
									num3 = 35;
									continue;
								}
								num7 = 0;
								num3 = 9;
								continue;
							case 63:
							{
								Color pixel4;
								if (pixel4.A == 255)
								{
									num3 = 48;
									continue;
								}
								goto IL_66B;
							}
							case 64:
								goto IL_40F;
							case 65:
								goto IL_454;
							case 66:
							{
								if (num7 >= num)
								{
									num3 = 20;
									continue;
								}
								Color pixel2 = bitmap.GetPixel(num7, num11);
								num3 = 51;
								continue;
							}
							case 67:
								goto IL_A8D;
							case 68:
							{
								Color pixel4;
								if (pixel4.G == 255)
								{
									num3 = 73;
									continue;
								}
								goto IL_66B;
							}
							case 69:
								goto IL_A17;
							case 70:
							{
								Color pixel5;
								if (pixel5.B != 255)
								{
									num3 = 75;
									continue;
								}
								goto IL_5E1;
							}
							case 71:
							{
								Color pixel;
								if (pixel.A == 255)
								{
									num3 = 39;
									continue;
								}
								goto IL_A17;
							}
							case 72:
								goto IL_66B;
							case 73:
								num3 = 86;
								continue;
							case 74:
								goto IL_6A1;
							case 75:
								goto IL_6C6;
							case 76:
								num3 = 12;
								continue;
							case 77:
								goto IL_A8D;
							case 78:
								num = num4;
								num3 = 7;
								continue;
							case 79:
							{
								Color pixel5;
								if (pixel5.A == 255)
								{
									num3 = 49;
									continue;
								}
								goto IL_6C6;
							}
							case 80:
								goto IL_75B;
							case 81:
							{
								Color pixel3;
								if (pixel3.B != 255)
								{
									num3 = 32;
									continue;
								}
								goto IL_5CD;
							}
							case 82:
								goto IL_7EE;
							case 83:
								goto IL_439;
							case 84:
								num3 = 47;
								continue;
							case 85:
								if (num4 < num)
								{
									num3 = 78;
									continue;
								}
								goto IL_254;
							case 86:
							{
								Color pixel4;
								if (pixel4.B != 255)
								{
									num3 = 72;
									continue;
								}
								goto IL_6EB;
							}
							case 87:
								num = num6;
								num3 = 28;
								continue;
							case 88:
								goto IL_3B4;
							case 89:
							{
								Color pixel5;
								if (pixel5.G == 255)
								{
									num3 = 96;
									continue;
								}
								goto IL_6C6;
							}
							case 90:
								num3 = 61;
								continue;
							case 91:
								if (num9 > num2)
								{
									num3 = 15;
									continue;
								}
								goto IL_6EB;
							case 92:
								if (num5 >= bitmap.Height)
								{
									num3 = 38;
									continue;
								}
								num3 = 17;
								continue;
							case 93:
								num3 = 81;
								continue;
							case 94:
								num3 = 68;
								continue;
							case 95:
								num3 = 89;
								continue;
							case 96:
								num3 = 70;
								continue;
							case 97:
								num3 = 41;
								continue;
							case 98:
								goto IL_75B;
							case 99:
								goto IL_40F;
							}
							break;
							IL_254:
							num3 = 0;
							continue;
							IL_321:
							num3 = 62;
							continue;
							IL_38A:
							num3 = 37;
							continue;
							IL_3B4:
							num3 = 58;
							continue;
							IL_3EA:
							num3 = 3;
							continue;
							IL_40F:
							num3 = 92;
							continue;
							IL_439:
							num8 = bitmap.Width - 1;
							num3 = 25;
							continue;
							IL_454:
							num3 = 30;
							continue;
							IL_58D:
							num3 = 18;
							continue;
							IL_5CD:
							num8--;
							num3 = 23;
							continue;
							IL_5E1:
							num6++;
							num3 = 65;
							continue;
							IL_5F8:
							num5++;
							num3 = 99;
							continue;
							IL_66B:
							num3 = 91;
							continue;
							IL_6A1:
							num3 = 66;
							continue;
							IL_6C6:
							num3 = 44;
							continue;
							IL_6EB:
							num9--;
							num3 = 88;
							continue;
							IL_75B:
							num10++;
							num3 = 5;
							continue;
							IL_7EE:
							num3 = 1;
							continue;
							IL_852:
							num7++;
							num3 = 74;
							continue;
							IL_8C5:
							num3 = 6;
							continue;
							IL_943:
							num4++;
							num3 = 82;
							continue;
							IL_A17:
							num3 = 85;
							continue;
							IL_A3C:
							num11++;
							num3 = 43;
							continue;
							IL_A8D:
							num3 = 60;
						}
					}
					IL_A99:;
				}
				finally
				{
					int num3;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_AD8:
						num3 = 0;
						break;
					default:
						if (false)
						{
						}
						num3 = 1;
						break;
					}
					for (;;)
					{
						switch (num3)
						{
						case 0:
							((IDisposable)bitmap).Dispose();
							num3 = 2;
							continue;
						case 2:
							goto IL_AF2;
						}
						break;
					}
					if (bitmap != null)
					{
						goto IL_AD8;
					}
					IL_AF2:;
				}
				float num12 = 1f;
				return new RectangleF(rectangleF.X / num12, rectangleF.Y / num12, rectangleF.Width / num12, rectangleF.Height / num12);
			}
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00014550 File Offset: 0x00012750
		private RectangleF ᜀ(RectangleF A_0, float A_1)
		{
			switch (0)
			{
			default:
			{
				PointF[] array;
				for (;;)
				{
					double num = (double)(A_1 / 180f) * 3.141592653589793;
					double num2 = Math.Sin(num);
					double num3 = Math.Cos(num);
					array = new PointF[]
					{
						new PointF(A_0.Left, A_0.Top),
						new PointF(A_0.Right, A_0.Top),
						new PointF(A_0.Right, A_0.Bottom),
						new PointF(A_0.Left, A_0.Bottom)
					};
					int num4 = 0;
					int num5 = 4;
					for (;;)
					{
						switch (num5)
						{
						case 0:
						{
							if (true)
							{
							}
							if (num4 >= array.Length)
							{
								num5 = 3;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							}
							if (false)
							{
							}
							float x = (float)((double)array[num4].X * num3 - (double)array[num4].Y * num2);
							float y = (float)((double)array[num4].X * num2 + (double)array[num4].Y * num3);
							array[num4] = new PointF(x, y);
							num4++;
							num5 = 1;
							continue;
						}
						case 1:
							goto IL_114;
						case 2:
							if (A_1 > 0f)
							{
								num5 = 5;
								continue;
							}
							goto IL_245;
						case 3:
							num5 = 2;
							continue;
						case 4:
							goto IL_114;
						case 5:
							goto IL_10F;
						}
						break;
						IL_114:
						num5 = 0;
					}
				}
				IL_10F:
				PointF pointF = new PointF(array[3].X, array[0].Y);
				float width = array[1].X - array[3].X;
				float height = array[2].Y - array[0].Y;
				return new RectangleF(pointF.X, pointF.Y, width, height);
				IL_245:
				PointF pointF2 = new PointF(array[0].X, array[1].Y);
				float width2 = array[2].X - array[0].X;
				float height2 = array[3].Y - array[1].Y;
				return new RectangleF(pointF2.X, pointF2.Y, width2, height2);
			}
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00014810 File Offset: 0x00012A10
		private void ᜀ(Workbook A_0, PdfDocument A_1)
		{
			int a_ = 1;
			switch (0)
			{
			default:
				if (A_0 != null)
				{
					goto IL_1F;
					try
					{
						for (;;)
						{
							IL_1F:
							for (;;)
							{
								License license = null;
								LicenseManager.IsValid(typeof(Workbook), A_0, out license);
								LicenseType licenseType = spr\u2067.ᜀ(license);
								int num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_FA;
									case 1:
										goto IL_EE;
									case 2:
										if ((licenseType & LicenseType.Runtime) == LicenseType.Runtime)
										{
											num = 3;
											continue;
										}
										goto IL_EE;
									case 3:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											break;
										default:
											if (false)
											{
											}
											A_1.InternalLicense = new InternalLicense
											{
												License = (LicenseInfo)license,
												LicenseType = licenseType,
												ProductName = SheetFinishedEventHandler.b("쪹햻첽ꖿ鳃諅鯇", a_),
												AssemblyList = new string[]
												{
													SheetFinishedEventHandler.b("쪹햻첽ꖿ鳃諅鯇", a_)
												}
											};
											num = 1;
											continue;
										}
										break;
									}
									break;
									IL_EE:
									num = 0;
								}
							}
						}
						IL_FA:
						return;
					}
					catch (Exception)
					{
						return;
					}
				}
				if (true)
				{
				}
				return;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00014940 File Offset: 0x00012B40
		// Note: this type is marked as 'beforefieldinit'.
		static PdfConverter()
		{
			int a_ = 17;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			PdfConverter.ᜀ = new Regex(SheetFinishedEventHandler.b("铇鋍꟏六﷓蓗ﻙ苝韟짡췣", a_));
		}

		// Token: 0x04000057 RID: 87
		private static Regex ᜀ;

		// Token: 0x04000058 RID: 88
		private bool ᜁ;

		// Token: 0x04000059 RID: 89
		private float ᜂ;

		// Token: 0x0400005A RID: 90
		private spr\u2310 ᜃ;

		// Token: 0x0400005B RID: 91
		private PdfSection ᜄ;

		// Token: 0x0400005C RID: 92
		private PdfPageBase ᜅ;

		// Token: 0x0400005D RID: 93
		private Workbook ᜆ;

		// Token: 0x0400005E RID: 94
		private PdfCanvas ᜇ;

		// Token: 0x0400005F RID: 95
		private int ᜈ;

		// Token: 0x04000060 RID: 96
		private PdfDocument ᜉ;

		// Token: 0x04000061 RID: 97
		private bool ᜊ;

		// Token: 0x04000062 RID: 98
		private Dictionary<Worksheet, PdfPageSettings> ᜋ;

		// Token: 0x04000063 RID: 99
		private PdfConverterSettings ᜌ;

		// Token: 0x04000064 RID: 100
		private PdfUnitConvertor \u170D;

		// Token: 0x04000065 RID: 101
		private sprᯏ ᜎ;

		// Token: 0x04000066 RID: 102
		private Worksheet ᜏ;

		// Token: 0x04000067 RID: 103
		private CellRange ᜐ;

		// Token: 0x04000068 RID: 104
		private SizeF ᜑ;

		// Token: 0x04000069 RID: 105
		private float \u1712;

		// Token: 0x0400006A RID: 106
		private Font \u1713;

		// Token: 0x0400006B RID: 107
		private XlsFont \u1714;

		// Token: 0x0400006C RID: 108
		private PdfTrueTypeFont \u1715;

		// Token: 0x0400006D RID: 109
		private List<Worksheet> \u1716;

		// Token: 0x0400006E RID: 110
		private Dictionary<Worksheet, spr\u1719[]> \u1717;

		// Token: 0x0400006F RID: 111
		private spr\u21BD \u1718;

		// Token: 0x04000070 RID: 112
		private float \u1719;

		// Token: 0x04000071 RID: 113
		private bool \u171A;

		// Token: 0x04000072 RID: 114
		private bool \u171B;

		// Token: 0x04000073 RID: 115
		private Dictionary<long, Metafile> \u171C;

		// Token: 0x04000074 RID: 116
		private ProgressEventHandler \u171D;

		// Token: 0x04000075 RID: 117
		private SheetFinishedEventHandler \u171E;

		// Token: 0x04000076 RID: 118
		private SheetStartEventHandler \u171F;

		// Token: 0x02000021 RID: 33
		// (Invoke) Token: 0x060000E9 RID: 233
		private delegate void ᜀ(XlsRange A_0, RectangleF A_1, PdfCanvas A_2);

		// Token: 0x02000022 RID: 34
		// (Invoke) Token: 0x060000ED RID: 237
		private delegate void ᜁ(Worksheet A_0, spr\u25A6.ᜀ A_1, int A_2, int A_3, PdfCanvas A_4, sprᱥ A_5, sprᱥ A_6);
	}
}
