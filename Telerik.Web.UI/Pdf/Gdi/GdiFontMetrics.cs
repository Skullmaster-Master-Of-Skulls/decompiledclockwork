using System;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Telerik.Pdf.Gdi.Font;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x0200162F RID: 5679
	public class GdiFontMetrics : IDisposable
	{
		// Token: 0x0600DCC3 RID: 56515 RVA: 0x00303ED8 File Offset: 0x003020D8
		internal GdiFontMetrics(GdiDeviceContent dc, GdiFont currentFont)
		{
			if (dc.Handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("dc", "Handle to device context cannot be null");
			}
			if (dc.GetCurrentObject(GdiDcObject.Font) == IntPtr.Zero)
			{
				throw new ArgumentException("dc", "No font selected into supplied device context");
			}
			this.dc = dc;
			this.currentFont = currentFont;
			StringBuilder stringBuilder = new StringBuilder(255);
			NativeMethods.GetTextFace(dc.Handle, stringBuilder.Capacity, stringBuilder);
			this.faceName = stringBuilder.ToString();
			this.ranges = new GdiUnicodeRanges(dc);
			this.reader = new FontFileReader(new MemoryStream(this.GetFontData()), this.faceName);
			this.converter = new PdfUnitConverter(this.EmSquare);
			currentFont.Dispose();
		}

		// Token: 0x17004397 RID: 17303
		// (get) Token: 0x0600DCC4 RID: 56516 RVA: 0x00303FA7 File Offset: 0x003021A7
		public string FaceName
		{
			get
			{
				return this.faceName;
			}
		}

		// Token: 0x17004398 RID: 17304
		// (get) Token: 0x0600DCC5 RID: 56517 RVA: 0x00303FAF File Offset: 0x003021AF
		public int EmSquare
		{
			get
			{
				this.EnsureHeadTable();
				return this.head.unitsPermEm;
			}
		}

		// Token: 0x17004399 RID: 17305
		// (get) Token: 0x0600DCC6 RID: 56518 RVA: 0x00303FC2 File Offset: 0x003021C2
		public int ItalicAngle
		{
			get
			{
				this.EnsurePostTable();
				return this.converter.ToPdfUnits((int)this.post.ItalicAngle);
			}
		}

		// Token: 0x1700439A RID: 17306
		// (get) Token: 0x0600DCC7 RID: 56519 RVA: 0x00303FE1 File Offset: 0x003021E1
		public int Ascent
		{
			get
			{
				this.EnsureHheaTable();
				return this.converter.ToPdfUnits((int)this.hhea.ascender);
			}
		}

		// Token: 0x1700439B RID: 17307
		// (get) Token: 0x0600DCC8 RID: 56520 RVA: 0x00303FFF File Offset: 0x003021FF
		public int Descent
		{
			get
			{
				this.EnsureHheaTable();
				return this.converter.ToPdfUnits((int)this.hhea.decender);
			}
		}

		// Token: 0x1700439C RID: 17308
		// (get) Token: 0x0600DCC9 RID: 56521 RVA: 0x0030401D File Offset: 0x0030221D
		public int CapHeight
		{
			get
			{
				this.EnsureOS2Table();
				return this.converter.ToPdfUnits(this.os2.CapHeight);
			}
		}

		// Token: 0x1700439D RID: 17309
		// (get) Token: 0x0600DCCA RID: 56522 RVA: 0x0030403B File Offset: 0x0030223B
		public int XHeight
		{
			get
			{
				this.EnsureOS2Table();
				return this.converter.ToPdfUnits(this.os2.XHeight);
			}
		}

		// Token: 0x1700439E RID: 17310
		// (get) Token: 0x0600DCCB RID: 56523 RVA: 0x00304059 File Offset: 0x00302259
		public int StemV
		{
			get
			{
				return this.converter.ToPdfUnits(0);
			}
		}

		// Token: 0x1700439F RID: 17311
		// (get) Token: 0x0600DCCC RID: 56524 RVA: 0x00304067 File Offset: 0x00302267
		public int FirstChar
		{
			get
			{
				this.EnsureOS2Table();
				return this.os2.FirstChar;
			}
		}

		// Token: 0x170043A0 RID: 17312
		// (get) Token: 0x0600DCCD RID: 56525 RVA: 0x0030407A File Offset: 0x0030227A
		public int LastChar
		{
			get
			{
				this.EnsureOS2Table();
				return this.os2.LastChar;
			}
		}

		// Token: 0x170043A1 RID: 17313
		// (get) Token: 0x0600DCCE RID: 56526 RVA: 0x0030408D File Offset: 0x0030228D
		public int AverageWidth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170043A2 RID: 17314
		// (get) Token: 0x0600DCCF RID: 56527 RVA: 0x00304090 File Offset: 0x00302290
		public int MaxWidth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170043A3 RID: 17315
		// (get) Token: 0x0600DCD0 RID: 56528 RVA: 0x00304093 File Offset: 0x00302293
		public bool IsEmbeddable
		{
			get
			{
				this.EnsureOS2Table();
				return this.os2.IsEmbeddable;
			}
		}

		// Token: 0x170043A4 RID: 17316
		// (get) Token: 0x0600DCD1 RID: 56529 RVA: 0x003040A6 File Offset: 0x003022A6
		public bool IsSubsettable
		{
			get
			{
				this.EnsureOS2Table();
				return this.os2.IsSubsettable;
			}
		}

		// Token: 0x170043A5 RID: 17317
		// (get) Token: 0x0600DCD2 RID: 56530 RVA: 0x003040BC File Offset: 0x003022BC
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public int[] BoundingBox
		{
			get
			{
				this.EnsureHeadTable();
				return new int[]
				{
					this.converter.ToPdfUnits((int)this.head.xMin),
					this.converter.ToPdfUnits((int)this.head.yMin),
					this.converter.ToPdfUnits((int)this.head.xMax),
					this.converter.ToPdfUnits((int)this.head.yMax)
				};
			}
		}

		// Token: 0x170043A6 RID: 17318
		// (get) Token: 0x0600DCD3 RID: 56531 RVA: 0x0030413C File Offset: 0x0030233C
		public int Flags
		{
			get
			{
				this.EnsureOS2Table();
				BitVector32 bitVector = new BitVector32(0);
				bitVector[1] = this.os2.IsMonospaced;
				bitVector[2] = this.os2.IsSerif;
				bitVector[8] = this.os2.IsScript;
				bitVector[64] = this.os2.IsItalic;
				if (this.os2.IsSymbolic)
				{
					bitVector[4] = true;
				}
				else
				{
					bitVector[32] = true;
				}
				if (bitVector.Data == 0)
				{
					return 32;
				}
				return bitVector.Data;
			}
		}

		// Token: 0x0600DCD4 RID: 56532 RVA: 0x003041DC File Offset: 0x003023DC
		public byte[] GetFontData()
		{
			if (this.data == null)
			{
				try
				{
					int dwTable = TableNames.ToUint("ttcf");
					long num = (long)NativeMethods.GetFontData(this.dc.Handle, dwTable, 0, null, 0);
					if (num != 0L && num != (long)((ulong)-1))
					{
						this.data = this.ReadFontFromCollection();
					}
					else
					{
						this.data = this.ReadFont();
					}
				}
				catch (Exception innerException)
				{
					throw new Exception(string.Format("Failed to load data for font {0}", this.FaceName), innerException);
				}
			}
			return this.data;
		}

		// Token: 0x0600DCD5 RID: 56533 RVA: 0x00304268 File Offset: 0x00302468
		private byte[] ReadFontFromCollection()
		{
			GdiFontCreator gdiFontCreator = new GdiFontCreator(this.dc);
			return gdiFontCreator.Build();
		}

		// Token: 0x0600DCD6 RID: 56534 RVA: 0x00304288 File Offset: 0x00302488
		private byte[] ReadFont()
		{
			int fontData = NativeMethods.GetFontData(this.dc.Handle, 0, 0, null, 0);
			if (fontData == 2147483647)
			{
				throw new InvalidOperationException("No font selected into device context");
			}
			byte[] array = new byte[fontData];
			long num = (long)NativeMethods.GetFontData(this.dc.Handle, 0, 0, array, fontData);
			if (num == (long)((ulong)-1))
			{
				throw new Exception("Failed to retrieve table data for font " + this.FaceName);
			}
			return array;
		}

		// Token: 0x170043A7 RID: 17319
		// (get) Token: 0x0600DCD7 RID: 56535 RVA: 0x003042F8 File Offset: 0x003024F8
		public GdiKerningPairs KerningPairs
		{
			get
			{
				if (this.reader.ContainsTable("kern"))
				{
					this.kern = (KerningTable)this.reader.GetTable("kern");
					return new GdiKerningPairs(this.kern.KerningPairs, this.converter);
				}
				return GdiKerningPairs.Empty;
			}
		}

		// Token: 0x170043A8 RID: 17320
		// (get) Token: 0x0600DCD8 RID: 56536 RVA: 0x00304350 File Offset: 0x00302550
		public GdiKerningPairs AnsiKerningPairs
		{
			get
			{
				if (this.reader.ContainsTable("kern"))
				{
					this.kern = (KerningTable)this.reader.GetTable("kern");
					KerningPairs kerningPairs = this.kern.KerningPairs;
					KerningPairs kerningPairs2 = new KerningPairs();
					WinAnsiMapping mapping = WinAnsiMapping.Mapping;
					for (int i = 0; i < 256; i++)
					{
						int left = this.ranges.MapCharacter((char)i);
						for (int j = 0; j < 256; j++)
						{
							int right = this.ranges.MapCharacter((char)j);
							if (kerningPairs.HasKerning(left, right))
							{
								kerningPairs2.Add(mapping.MapCharacter((char)i), mapping.MapCharacter((char)j), kerningPairs[left, right]);
							}
						}
					}
					return new GdiKerningPairs(kerningPairs2, this.converter);
				}
				return GdiKerningPairs.Empty;
			}
		}

		// Token: 0x0600DCD9 RID: 56537 RVA: 0x00304428 File Offset: 0x00302628
		public int[] GetWidths()
		{
			this.EnsureHmtxTable();
			int[] array = new int[this.hmtx.Count];
			for (int i = 0; i < this.hmtx.Count; i++)
			{
				array[i] = this.converter.ToPdfUnits(this.hmtx[i].AdvanceWidth);
			}
			return array;
		}

		// Token: 0x0600DCDA RID: 56538 RVA: 0x00304484 File Offset: 0x00302684
		public int[] GetAnsiWidths()
		{
			this.EnsureHmtxTable();
			int[] array = new int[256];
			int num = this.converter.ToPdfUnits(this.hmtx[0].AdvanceWidth);
			for (int i = 0; i < 256; i++)
			{
				array[i] = num;
			}
			WinAnsiMapping mapping = WinAnsiMapping.Mapping;
			for (int j = 0; j < 256; j++)
			{
				int index = this.MapCharacter((char)j);
				int num2 = mapping.MapCharacter((char)j);
				array[num2] = this.converter.ToPdfUnits(this.hmtx[index].AdvanceWidth);
			}
			return array;
		}

		// Token: 0x0600DCDB RID: 56539 RVA: 0x00304525 File Offset: 0x00302725
		public int MapCharacter(char c)
		{
			return this.ranges.MapCharacter(c);
		}

		// Token: 0x0600DCDC RID: 56540 RVA: 0x00304533 File Offset: 0x00302733
		private void EnsureHmtxTable()
		{
			if (this.hmtx == null)
			{
				this.hmtx = (HorizontalMetricsTable)this.GetTable("hmtx");
			}
		}

		// Token: 0x0600DCDD RID: 56541 RVA: 0x00304553 File Offset: 0x00302753
		private void EnsureHheaTable()
		{
			if (this.hhea == null)
			{
				this.hhea = (HorizontalHeaderTable)this.GetTable("hhea");
			}
		}

		// Token: 0x0600DCDE RID: 56542 RVA: 0x00304573 File Offset: 0x00302773
		private void EnsurePostTable()
		{
			if (this.post == null)
			{
				this.post = (PostTable)this.GetTable("post");
			}
		}

		// Token: 0x0600DCDF RID: 56543 RVA: 0x00304593 File Offset: 0x00302793
		private void EnsureHeadTable()
		{
			if (this.head == null)
			{
				this.head = (HeaderTable)this.GetTable("head");
			}
		}

		// Token: 0x0600DCE0 RID: 56544 RVA: 0x003045B3 File Offset: 0x003027B3
		private void EnsureOS2Table()
		{
			if (this.os2 == null)
			{
				this.os2 = (OS2Table)this.GetTable("OS/2");
			}
		}

		// Token: 0x0600DCE1 RID: 56545 RVA: 0x003045D4 File Offset: 0x003027D4
		private FontTable GetTable(string name)
		{
			FontTable table;
			try
			{
				table = this.reader.GetTable(name);
			}
			catch
			{
				throw new Exception(string.Format("Unable to retrieve table {0} from font {1}", name, this.FaceName));
			}
			return table;
		}

		// Token: 0x0600DCE2 RID: 56546 RVA: 0x0030461C File Offset: 0x0030281C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.reader != null)
			{
				this.reader.Dispose();
			}
		}

		// Token: 0x0600DCE3 RID: 56547 RVA: 0x00304634 File Offset: 0x00302834
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04003E55 RID: 15957
		public const long GDI_ERROR = 4294967295L;

		// Token: 0x04003E56 RID: 15958
		private FontFileReader reader;

		// Token: 0x04003E57 RID: 15959
		private GdiDeviceContent dc;

		// Token: 0x04003E58 RID: 15960
		private GdiFont currentFont;

		// Token: 0x04003E59 RID: 15961
		private PdfUnitConverter converter;

		// Token: 0x04003E5A RID: 15962
		private GdiUnicodeRanges ranges;

		// Token: 0x04003E5B RID: 15963
		private string faceName;

		// Token: 0x04003E5C RID: 15964
		private HeaderTable head;

		// Token: 0x04003E5D RID: 15965
		private PostTable post;

		// Token: 0x04003E5E RID: 15966
		private HorizontalHeaderTable hhea;

		// Token: 0x04003E5F RID: 15967
		private HorizontalMetricsTable hmtx;

		// Token: 0x04003E60 RID: 15968
		private OS2Table os2;

		// Token: 0x04003E61 RID: 15969
		private KerningTable kern;

		// Token: 0x04003E62 RID: 15970
		private byte[] data;
	}
}
