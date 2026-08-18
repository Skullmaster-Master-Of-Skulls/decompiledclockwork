using System;
using System.IO;
using System.Text;

namespace Telerik.Pdf
{
	// Token: 0x02001663 RID: 5731
	public sealed class PdfName : PdfObject
	{
		// Token: 0x0600DDF6 RID: 56822 RVA: 0x00307D1E File Offset: 0x00305F1E
		public PdfName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
		}

		// Token: 0x0600DDF7 RID: 56823 RVA: 0x00307D3B File Offset: 0x00305F3B
		public PdfName(string name, PdfObjectId objectId) : base(objectId)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
		}

		// Token: 0x170043ED RID: 17389
		// (get) Token: 0x0600DDF8 RID: 56824 RVA: 0x00307D59 File Offset: 0x00305F59
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0600DDF9 RID: 56825 RVA: 0x00307D61 File Offset: 0x00305F61
		protected internal override void Write(PdfWriter writer)
		{
			writer.Write(this.NameBytes);
		}

		// Token: 0x170043EE RID: 17390
		// (get) Token: 0x0600DDFA RID: 56826 RVA: 0x00307D70 File Offset: 0x00305F70
		private byte[] NameBytes
		{
			get
			{
				if (this.bytes == null)
				{
					MemoryStream memoryStream = new MemoryStream(this.name.Length + 1);
					memoryStream.WriteByte(47);
					foreach (byte b in Encoding.UTF8.GetBytes(this.name))
					{
						if (b < 34 || b > 125 || b == 35)
						{
							memoryStream.WriteByte(35);
							memoryStream.WriteByte(PdfName.HexDigits[b >> 4]);
							memoryStream.WriteByte(PdfName.HexDigits[(int)(b & 15)]);
						}
						else
						{
							memoryStream.WriteByte(b);
						}
					}
					memoryStream.Close();
					this.bytes = memoryStream.ToArray();
				}
				return this.bytes;
			}
		}

		// Token: 0x0600DDFB RID: 56827 RVA: 0x00307E1D File Offset: 0x0030601D
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x0600DDFC RID: 56828 RVA: 0x00307E2C File Offset: 0x0030602C
		public override bool Equals(object obj)
		{
			PdfName pdfName = obj as PdfName;
			return pdfName != null && this.name.Equals(pdfName.Name);
		}

		// Token: 0x04003F3F RID: 16191
		private string name;

		// Token: 0x04003F40 RID: 16192
		private byte[] bytes;

		// Token: 0x04003F41 RID: 16193
		private static readonly byte[] HexDigits = new byte[]
		{
			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57,
			97,
			98,
			99,
			100,
			101,
			102
		};

		// Token: 0x02001664 RID: 5732
		public class Names
		{
			// Token: 0x04003F42 RID: 16194
			public static readonly PdfName Catalog = new PdfName("Catalog");

			// Token: 0x04003F43 RID: 16195
			public static readonly PdfName Type = new PdfName("Type");

			// Token: 0x04003F44 RID: 16196
			public static readonly PdfName Subtype = new PdfName("Subtype");

			// Token: 0x04003F45 RID: 16197
			public static readonly PdfName Pages = new PdfName("Pages");

			// Token: 0x04003F46 RID: 16198
			public static readonly PdfName Outlines = new PdfName("Outlines");

			// Token: 0x04003F47 RID: 16199
			public static readonly PdfName Kids = new PdfName("Kids");

			// Token: 0x04003F48 RID: 16200
			public static readonly PdfName Count = new PdfName("Count");

			// Token: 0x04003F49 RID: 16201
			public static readonly PdfName Title = new PdfName("Title");

			// Token: 0x04003F4A RID: 16202
			public static readonly PdfName Author = new PdfName("Author");

			// Token: 0x04003F4B RID: 16203
			public static readonly PdfName Subject = new PdfName("Subject");

			// Token: 0x04003F4C RID: 16204
			public static readonly PdfName Keywords = new PdfName("Keywords");

			// Token: 0x04003F4D RID: 16205
			public static readonly PdfName Creator = new PdfName("Creator");

			// Token: 0x04003F4E RID: 16206
			public static readonly PdfName Producer = new PdfName("Producer");

			// Token: 0x04003F4F RID: 16207
			public static readonly PdfName CreationDate = new PdfName("CreationDate");

			// Token: 0x04003F50 RID: 16208
			public static readonly PdfName ModDate = new PdfName("ModDate");

			// Token: 0x04003F51 RID: 16209
			public static readonly PdfName Size = new PdfName("Size");

			// Token: 0x04003F52 RID: 16210
			public static readonly PdfName Prev = new PdfName("Prev");

			// Token: 0x04003F53 RID: 16211
			public static readonly PdfName Root = new PdfName("Root");

			// Token: 0x04003F54 RID: 16212
			public static readonly PdfName Encrypt = new PdfName("Encrypt");

			// Token: 0x04003F55 RID: 16213
			public static readonly PdfName Info = new PdfName("Info");

			// Token: 0x04003F56 RID: 16214
			public static readonly PdfName Id = new PdfName("ID");

			// Token: 0x04003F57 RID: 16215
			public static readonly PdfName Encoding = new PdfName("Encoding");

			// Token: 0x04003F58 RID: 16216
			public static readonly PdfName BaseEncoding = new PdfName("BaseEncoding");

			// Token: 0x04003F59 RID: 16217
			public static readonly PdfName MacRomanEncoding = new PdfName("MacRomanEncoding");

			// Token: 0x04003F5A RID: 16218
			public static readonly PdfName MacExpertEncoding = new PdfName("MacExpertEncoding");

			// Token: 0x04003F5B RID: 16219
			public static readonly PdfName WinAnsiEncoding = new PdfName("WinAnsiEncoding");

			// Token: 0x04003F5C RID: 16220
			public static readonly PdfName FileSpec = new PdfName("FileSpec");

			// Token: 0x04003F5D RID: 16221
			public static readonly PdfName F = new PdfName("F");

			// Token: 0x04003F5E RID: 16222
			public static readonly PdfName Annot = new PdfName("Annot");

			// Token: 0x04003F5F RID: 16223
			public static readonly PdfName Action = new PdfName("Action");

			// Token: 0x04003F60 RID: 16224
			public static readonly PdfName Link = new PdfName("Link");

			// Token: 0x04003F61 RID: 16225
			public static readonly PdfName H = new PdfName("H");

			// Token: 0x04003F62 RID: 16226
			public static readonly PdfName I = new PdfName("I");

			// Token: 0x04003F63 RID: 16227
			public static readonly PdfName A = new PdfName("A");

			// Token: 0x04003F64 RID: 16228
			public static readonly PdfName Border = new PdfName("Border");

			// Token: 0x04003F65 RID: 16229
			public static readonly PdfName Rect = new PdfName("Rect");

			// Token: 0x04003F66 RID: 16230
			public static readonly PdfName C = new PdfName("C");

			// Token: 0x04003F67 RID: 16231
			public static readonly PdfName S = new PdfName("S");

			// Token: 0x04003F68 RID: 16232
			public static readonly PdfName GoTo = new PdfName("GoTo");

			// Token: 0x04003F69 RID: 16233
			public static readonly PdfName GoToR = new PdfName("GoToR");

			// Token: 0x04003F6A RID: 16234
			public static readonly PdfName D = new PdfName("D");

			// Token: 0x04003F6B RID: 16235
			public static readonly PdfName XYZ = new PdfName("XYZ");

			// Token: 0x04003F6C RID: 16236
			public static readonly PdfName URI = new PdfName("URI");

			// Token: 0x04003F6D RID: 16237
			public static readonly PdfName Font = new PdfName("Font");

			// Token: 0x04003F6E RID: 16238
			public static readonly PdfName FontName = new PdfName("FontName");

			// Token: 0x04003F6F RID: 16239
			public static readonly PdfName FontDescriptor = new PdfName("FontDescriptor");

			// Token: 0x04003F70 RID: 16240
			public static readonly PdfName Flags = new PdfName("Flags");

			// Token: 0x04003F71 RID: 16241
			public static readonly PdfName FontBBox = new PdfName("FontBBox");

			// Token: 0x04003F72 RID: 16242
			public static readonly PdfName ItalicAngle = new PdfName("ItalicAngle");

			// Token: 0x04003F73 RID: 16243
			public static readonly PdfName Ascent = new PdfName("Ascent");

			// Token: 0x04003F74 RID: 16244
			public static readonly PdfName Descent = new PdfName("Descent");

			// Token: 0x04003F75 RID: 16245
			public static readonly PdfName Leading = new PdfName("Leading");

			// Token: 0x04003F76 RID: 16246
			public static readonly PdfName CapHeight = new PdfName("CapHeight");

			// Token: 0x04003F77 RID: 16247
			public static readonly PdfName XHeight = new PdfName("XHeight");

			// Token: 0x04003F78 RID: 16248
			public static readonly PdfName StemV = new PdfName("StemV");

			// Token: 0x04003F79 RID: 16249
			public static readonly PdfName StemH = new PdfName("StemH");

			// Token: 0x04003F7A RID: 16250
			public static readonly PdfName AvgWidth = new PdfName("AvgWidth");

			// Token: 0x04003F7B RID: 16251
			public static readonly PdfName MaxWidth = new PdfName("MaxWidth");

			// Token: 0x04003F7C RID: 16252
			public static readonly PdfName MissingWidth = new PdfName("MissingWidth");

			// Token: 0x04003F7D RID: 16253
			public static readonly PdfName FontFile = new PdfName("FontFile");

			// Token: 0x04003F7E RID: 16254
			public static readonly PdfName FontFile2 = new PdfName("FontFile2");

			// Token: 0x04003F7F RID: 16255
			public static readonly PdfName FontFile3 = new PdfName("FontFile3");

			// Token: 0x04003F80 RID: 16256
			public static readonly PdfName CharSet = new PdfName("CharSet");

			// Token: 0x04003F81 RID: 16257
			public static readonly PdfName CIDToGIDMap = new PdfName("CIDToGIDMap");

			// Token: 0x04003F82 RID: 16258
			public static readonly PdfName Identity = new PdfName("Identity");

			// Token: 0x04003F83 RID: 16259
			public static readonly PdfName Length1 = new PdfName("Length1");

			// Token: 0x04003F84 RID: 16260
			public static readonly PdfName Length2 = new PdfName("Length2");

			// Token: 0x04003F85 RID: 16261
			public static readonly PdfName Length3 = new PdfName("Length3");

			// Token: 0x04003F86 RID: 16262
			public static readonly PdfName ToUnicode = new PdfName("ToUnicode");

			// Token: 0x04003F87 RID: 16263
			public static readonly PdfName CMap = new PdfName("CMap");

			// Token: 0x04003F88 RID: 16264
			public static readonly PdfName CMapName = new PdfName("CMapName");

			// Token: 0x04003F89 RID: 16265
			public static readonly PdfName WMode = new PdfName("WMode");

			// Token: 0x04003F8A RID: 16266
			public static readonly PdfName Type0 = new PdfName("Type0");

			// Token: 0x04003F8B RID: 16267
			public static readonly PdfName Type1 = new PdfName("Type1");

			// Token: 0x04003F8C RID: 16268
			public static readonly PdfName TrueType = new PdfName("TrueType");

			// Token: 0x04003F8D RID: 16269
			public static readonly PdfName Name = new PdfName("Name");

			// Token: 0x04003F8E RID: 16270
			public static readonly PdfName BaseFont = new PdfName("BaseFont");

			// Token: 0x04003F8F RID: 16271
			public static readonly PdfName XObject = new PdfName("XObject");

			// Token: 0x04003F90 RID: 16272
			public static readonly PdfName CIDFontType0 = new PdfName("CIDFontType0");

			// Token: 0x04003F91 RID: 16273
			public static readonly PdfName CIDFontType2 = new PdfName("CIDFontType2");

			// Token: 0x04003F92 RID: 16274
			public static readonly PdfName CIDSystemInfo = new PdfName("CIDSystemInfo");

			// Token: 0x04003F93 RID: 16275
			public static readonly PdfName DescendantFonts = new PdfName("DescendantFonts");

			// Token: 0x04003F94 RID: 16276
			public static readonly PdfName Registry = new PdfName("Registry");

			// Token: 0x04003F95 RID: 16277
			public static readonly PdfName Ordering = new PdfName("Ordering");

			// Token: 0x04003F96 RID: 16278
			public static readonly PdfName Supplement = new PdfName("Supplement");

			// Token: 0x04003F97 RID: 16279
			public static readonly PdfName DW = new PdfName("DW");

			// Token: 0x04003F98 RID: 16280
			public static readonly PdfName W = new PdfName("W");

			// Token: 0x04003F99 RID: 16281
			public static readonly PdfName Page = new PdfName("Page");

			// Token: 0x04003F9A RID: 16282
			public static readonly PdfName PageMode = new PdfName("PageMode");

			// Token: 0x04003F9B RID: 16283
			public static readonly PdfName UseOutlines = new PdfName("UseOutlines");

			// Token: 0x04003F9C RID: 16284
			public static readonly PdfName Resources = new PdfName("Resources");

			// Token: 0x04003F9D RID: 16285
			public static readonly PdfName Contents = new PdfName("Contents");

			// Token: 0x04003F9E RID: 16286
			public static readonly PdfName MediaBox = new PdfName("MediaBox");

			// Token: 0x04003F9F RID: 16287
			public static readonly PdfName Parent = new PdfName("Parent");

			// Token: 0x04003FA0 RID: 16288
			public static readonly PdfName Annots = new PdfName("Annots");

			// Token: 0x04003FA1 RID: 16289
			public static readonly PdfName Image = new PdfName("Image");

			// Token: 0x04003FA2 RID: 16290
			public static readonly PdfName Width = new PdfName("Width");

			// Token: 0x04003FA3 RID: 16291
			public static readonly PdfName Height = new PdfName("Height");

			// Token: 0x04003FA4 RID: 16292
			public static readonly PdfName BitsPerComponent = new PdfName("BitsPerComponent");

			// Token: 0x04003FA5 RID: 16293
			public static readonly PdfName ColorSpace = new PdfName("ColorSpace");

			// Token: 0x04003FA6 RID: 16294
			public static readonly PdfName ProcSet = new PdfName("ProcSet");

			// Token: 0x04003FA7 RID: 16295
			public static readonly PdfName PDF = new PdfName("PDF");

			// Token: 0x04003FA8 RID: 16296
			public static readonly PdfName Text = new PdfName("Text");

			// Token: 0x04003FA9 RID: 16297
			public static readonly PdfName ImageB = new PdfName("ImageB");

			// Token: 0x04003FAA RID: 16298
			public static readonly PdfName ImageC = new PdfName("ImageC");

			// Token: 0x04003FAB RID: 16299
			public static readonly PdfName ImageI = new PdfName("ImageI");

			// Token: 0x04003FAC RID: 16300
			public static readonly PdfName Length = new PdfName("Length");

			// Token: 0x04003FAD RID: 16301
			public static readonly PdfName Filter = new PdfName("Filter");

			// Token: 0x04003FAE RID: 16302
			public static readonly PdfName DecodeParams = new PdfName("DecodeParams");

			// Token: 0x04003FAF RID: 16303
			public static readonly PdfName ASCII85Decode = new PdfName("ASCII85Decode");

			// Token: 0x04003FB0 RID: 16304
			public static readonly PdfName ASCIIHexDecode = new PdfName("ASCIIHexDecode");

			// Token: 0x04003FB1 RID: 16305
			public static readonly PdfName CCITTFaxDecode = new PdfName("CCITTFaxDecode");

			// Token: 0x04003FB2 RID: 16306
			public static readonly PdfName DCTDecode = new PdfName("DCTDecode");

			// Token: 0x04003FB3 RID: 16307
			public static readonly PdfName FlateDecode = new PdfName("FlateDecode");

			// Token: 0x04003FB4 RID: 16308
			public static readonly PdfName JBIG2Decode = new PdfName("JBIG2Decode");

			// Token: 0x04003FB5 RID: 16309
			public static readonly PdfName LZWDecode = new PdfName("LZWDecode");

			// Token: 0x04003FB6 RID: 16310
			public static readonly PdfName RunLengthDecode = new PdfName("RunLengthDecode");

			// Token: 0x04003FB7 RID: 16311
			public static readonly PdfName Standard = new PdfName("Standard");

			// Token: 0x04003FB8 RID: 16312
			public static readonly PdfName V = new PdfName("V");

			// Token: 0x04003FB9 RID: 16313
			public static readonly PdfName R = new PdfName("R");

			// Token: 0x04003FBA RID: 16314
			public static readonly PdfName O = new PdfName("O");

			// Token: 0x04003FBB RID: 16315
			public static readonly PdfName U = new PdfName("U");

			// Token: 0x04003FBC RID: 16316
			public static readonly PdfName P = new PdfName("P");

			// Token: 0x04003FBD RID: 16317
			public static readonly PdfName FirstChar = new PdfName("FirstChar");

			// Token: 0x04003FBE RID: 16318
			public static readonly PdfName LastChar = new PdfName("LastChar");

			// Token: 0x04003FBF RID: 16319
			public static readonly PdfName Widths = new PdfName("Widths");

			// Token: 0x04003FC0 RID: 16320
			public static readonly PdfName First = new PdfName("First");

			// Token: 0x04003FC1 RID: 16321
			public static readonly PdfName Last = new PdfName("Last");

			// Token: 0x04003FC2 RID: 16322
			public static readonly PdfName Next = new PdfName("Next");

			// Token: 0x04003FC3 RID: 16323
			public static readonly PdfName Alternate = new PdfName("Alternate");

			// Token: 0x04003FC4 RID: 16324
			public static readonly PdfName ICCBased = new PdfName("ICCBased");

			// Token: 0x04003FC5 RID: 16325
			public static readonly PdfName N = new PdfName("N");
		}
	}
}
