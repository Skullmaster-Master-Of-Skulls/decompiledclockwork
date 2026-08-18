using System;
using System.Collections.Generic;
using System.util;
using iTextSharp.text.error_messages;

namespace iTextSharp.text
{
	// Token: 0x020005FA RID: 1530
	public sealed class FontFactory
	{
		// Token: 0x0600341F RID: 13343 RVA: 0x0014463B File Offset: 0x0014363B
		private FontFactory()
		{
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x00144643 File Offset: 0x00143643
		public static Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color)
		{
			return FontFactory.fontImp.GetFont(fontname, encoding, embedded, size, style, color);
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x00144657 File Offset: 0x00143657
		public static Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color, bool cached)
		{
			return FontFactory.fontImp.GetFont(fontname, encoding, embedded, size, style, color, cached);
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x0014466D File Offset: 0x0014366D
		public static Font GetFont(Properties attributes)
		{
			FontFactory.fontImp.DefaultEmbedding = FontFactory.defaultEmbedding;
			FontFactory.fontImp.DefaultEncoding = FontFactory.defaultEncoding;
			return FontFactory.fontImp.GetFont(attributes);
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x00144698 File Offset: 0x00143698
		public static Font GetFont(string fontname, string encoding, bool embedded, float size, int style)
		{
			return FontFactory.GetFont(fontname, encoding, embedded, size, style, null);
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x001446A6 File Offset: 0x001436A6
		public static Font GetFont(string fontname, string encoding, bool embedded, float size)
		{
			return FontFactory.GetFont(fontname, encoding, embedded, size, -1, null);
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x001446B3 File Offset: 0x001436B3
		public static Font GetFont(string fontname, string encoding, bool embedded)
		{
			return FontFactory.GetFont(fontname, encoding, embedded, -1f, -1, null);
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x001446C4 File Offset: 0x001436C4
		public static Font GetFont(string fontname, string encoding, float size, int style, BaseColor color)
		{
			return FontFactory.GetFont(fontname, encoding, FontFactory.defaultEmbedding, size, style, color);
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x001446D6 File Offset: 0x001436D6
		public static Font GetFont(string fontname, string encoding, float size, int style)
		{
			return FontFactory.GetFont(fontname, encoding, FontFactory.defaultEmbedding, size, style, null);
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x001446E7 File Offset: 0x001436E7
		public static Font GetFont(string fontname, string encoding, float size)
		{
			return FontFactory.GetFont(fontname, encoding, FontFactory.defaultEmbedding, size, -1, null);
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x001446F8 File Offset: 0x001436F8
		public static Font GetFont(string fontname, string encoding)
		{
			return FontFactory.GetFont(fontname, encoding, FontFactory.defaultEmbedding, -1f, -1, null);
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x0014470D File Offset: 0x0014370D
		public static Font GetFont(string fontname, float size, int style, BaseColor color)
		{
			return FontFactory.GetFont(fontname, FontFactory.defaultEncoding, FontFactory.defaultEmbedding, size, style, color);
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x00144722 File Offset: 0x00143722
		public static Font GetFont(string fontname, float size, BaseColor color)
		{
			return FontFactory.GetFont(fontname, FontFactory.defaultEncoding, FontFactory.defaultEmbedding, size, -1, color);
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x00144737 File Offset: 0x00143737
		public static Font GetFont(string fontname, float size, int style)
		{
			return FontFactory.GetFont(fontname, FontFactory.defaultEncoding, FontFactory.defaultEmbedding, size, style, null);
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x0014474C File Offset: 0x0014374C
		public static Font GetFont(string fontname, float size)
		{
			return FontFactory.GetFont(fontname, FontFactory.defaultEncoding, FontFactory.defaultEmbedding, size, -1, null);
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x00144761 File Offset: 0x00143761
		public static Font GetFont(string fontname)
		{
			return FontFactory.GetFont(fontname, FontFactory.defaultEncoding, FontFactory.defaultEmbedding, -1f, -1, null);
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x0014477A File Offset: 0x0014377A
		public void RegisterFamily(string familyName, string fullName, string path)
		{
			FontFactory.fontImp.RegisterFamily(familyName, fullName, path);
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x0014478C File Offset: 0x0014378C
		public static void Register(Properties attributes)
		{
			string path = attributes.Remove("path");
			string alias = attributes.Remove("alias");
			FontFactory.fontImp.Register(path, alias);
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x001447BF File Offset: 0x001437BF
		public static void Register(string path)
		{
			FontFactory.Register(path, null);
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x001447C8 File Offset: 0x001437C8
		public static void Register(string path, string alias)
		{
			FontFactory.fontImp.Register(path, alias);
		}

		// Token: 0x06003433 RID: 13363 RVA: 0x001447D6 File Offset: 0x001437D6
		public static int RegisterDirectory(string dir)
		{
			return FontFactory.fontImp.RegisterDirectory(dir);
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x001447E3 File Offset: 0x001437E3
		public static int RegisterDirectory(string dir, bool scanSubdirectories)
		{
			return FontFactory.fontImp.RegisterDirectory(dir, scanSubdirectories);
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x001447F1 File Offset: 0x001437F1
		public static int RegisterDirectories()
		{
			return FontFactory.fontImp.RegisterDirectories();
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06003436 RID: 13366 RVA: 0x001447FD File Offset: 0x001437FD
		public static ICollection<string> RegisteredFonts
		{
			get
			{
				return FontFactory.fontImp.RegisteredFonts;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06003437 RID: 13367 RVA: 0x00144809 File Offset: 0x00143809
		public static ICollection<string> RegisteredFamilies
		{
			get
			{
				return FontFactory.fontImp.RegisteredFamilies;
			}
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x00144815 File Offset: 0x00143815
		public static bool Contains(string fontname)
		{
			return FontFactory.fontImp.IsRegistered(fontname);
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x00144822 File Offset: 0x00143822
		public static bool IsRegistered(string fontname)
		{
			return FontFactory.fontImp.IsRegistered(fontname);
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x0600343A RID: 13370 RVA: 0x0014482F File Offset: 0x0014382F
		public static string DefaultEncoding
		{
			get
			{
				return FontFactory.defaultEncoding;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x0600343B RID: 13371 RVA: 0x00144836 File Offset: 0x00143836
		public static bool DefaultEmbedding
		{
			get
			{
				return FontFactory.defaultEmbedding;
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x0600343C RID: 13372 RVA: 0x0014483D File Offset: 0x0014383D
		// (set) Token: 0x0600343D RID: 13373 RVA: 0x00144844 File Offset: 0x00143844
		public static FontFactoryImp FontImp
		{
			get
			{
				return FontFactory.fontImp;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(MessageLocalization.GetComposedMessage("fontfactoryimp.cannot.be.null"));
				}
				FontFactory.fontImp = value;
			}
		}

		// Token: 0x0400231D RID: 8989
		public const string COURIER = "Courier";

		// Token: 0x0400231E RID: 8990
		public const string COURIER_BOLD = "Courier-Bold";

		// Token: 0x0400231F RID: 8991
		public const string COURIER_OBLIQUE = "Courier-Oblique";

		// Token: 0x04002320 RID: 8992
		public const string COURIER_BOLDOBLIQUE = "Courier-BoldOblique";

		// Token: 0x04002321 RID: 8993
		public const string HELVETICA = "Helvetica";

		// Token: 0x04002322 RID: 8994
		public const string HELVETICA_BOLD = "Helvetica-Bold";

		// Token: 0x04002323 RID: 8995
		public const string HELVETICA_OBLIQUE = "Helvetica-Oblique";

		// Token: 0x04002324 RID: 8996
		public const string HELVETICA_BOLDOBLIQUE = "Helvetica-BoldOblique";

		// Token: 0x04002325 RID: 8997
		public const string SYMBOL = "Symbol";

		// Token: 0x04002326 RID: 8998
		public const string TIMES = "Times";

		// Token: 0x04002327 RID: 8999
		public const string TIMES_ROMAN = "Times-Roman";

		// Token: 0x04002328 RID: 9000
		public const string TIMES_BOLD = "Times-Bold";

		// Token: 0x04002329 RID: 9001
		public const string TIMES_ITALIC = "Times-Italic";

		// Token: 0x0400232A RID: 9002
		public const string TIMES_BOLDITALIC = "Times-BoldItalic";

		// Token: 0x0400232B RID: 9003
		public const string ZAPFDINGBATS = "ZapfDingbats";

		// Token: 0x0400232C RID: 9004
		private static FontFactoryImp fontImp = new FontFactoryImp();

		// Token: 0x0400232D RID: 9005
		private static string defaultEncoding = "Cp1252";

		// Token: 0x0400232E RID: 9006
		private static bool defaultEmbedding = false;
	}
}
