using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

namespace iTextSharp.text
{
	// Token: 0x020000F4 RID: 244
	public class Chunk : IElement
	{
		// Token: 0x06000979 RID: 2425 RVA: 0x00031FF3 File Offset: 0x00030FF3
		static Chunk()
		{
			Chunk.NEXTPAGE.SetNewPage();
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0003201E File Offset: 0x0003101E
		public Chunk()
		{
			this.content = new StringBuilder();
			this.font = new Font();
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0003203C File Offset: 0x0003103C
		public Chunk(Chunk ck)
		{
			if (ck.content != null)
			{
				this.content = new StringBuilder(ck.content.ToString());
			}
			if (ck.font != null)
			{
				this.font = new Font(ck.font);
			}
			if (ck.attributes != null)
			{
				this.attributes = new Dictionary<string, object>(ck.attributes);
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0003209F File Offset: 0x0003109F
		public Chunk(string content, Font font)
		{
			this.content = new StringBuilder(content);
			this.font = font;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x000320BA File Offset: 0x000310BA
		public Chunk(string content) : this(content, new Font())
		{
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x000320C8 File Offset: 0x000310C8
		public Chunk(char c, Font font)
		{
			this.content = new StringBuilder();
			this.content.Append(c);
			this.font = font;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x000320EF File Offset: 0x000310EF
		public Chunk(char c) : this(c, new Font())
		{
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00032100 File Offset: 0x00031100
		public Chunk(Image image, float offsetX, float offsetY) : this("￼", new Font())
		{
			Image instance = Image.GetInstance(image);
			instance.SetAbsolutePosition(float.NaN, float.NaN);
			this.SetAttribute("IMAGE", new object[]
			{
				instance,
				offsetX,
				offsetY,
				false
			});
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00032167 File Offset: 0x00031167
		public Chunk(IDrawInterface separator) : this(separator, false)
		{
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00032174 File Offset: 0x00031174
		public Chunk(IDrawInterface separator, bool vertical) : this("￼", new Font())
		{
			this.SetAttribute("SEPARATOR", new object[]
			{
				separator,
				vertical
			});
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000321B2 File Offset: 0x000311B2
		public Chunk(IDrawInterface separator, float tabPosition) : this(separator, tabPosition, false)
		{
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x000321C0 File Offset: 0x000311C0
		public Chunk(IDrawInterface separator, float tabPosition, bool newline) : this("￼", new Font())
		{
			if (tabPosition < 0f)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("a.tab.position.may.not.be.lower.than.0.yours.is.1", tabPosition));
			}
			this.SetAttribute("TAB", new object[]
			{
				separator,
				tabPosition,
				newline,
				0
			});
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00032230 File Offset: 0x00031230
		public Chunk(Image image, float offsetX, float offsetY, bool changeLeading) : this("￼", new Font())
		{
			this.SetAttribute("IMAGE", new object[]
			{
				image,
				offsetX,
				offsetY,
				changeLeading
			});
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00032284 File Offset: 0x00031284
		public bool Process(IElementListener listener)
		{
			bool result;
			try
			{
				result = listener.Add(this);
			}
			catch (DocumentException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x000322B4 File Offset: 0x000312B4
		public int Type
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x000322B8 File Offset: 0x000312B8
		public List<Chunk> Chunks
		{
			get
			{
				return new List<Chunk>
				{
					this
				};
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x000322D3 File Offset: 0x000312D3
		public StringBuilder Append(string str)
		{
			return this.content.Append(str);
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x000322E1 File Offset: 0x000312E1
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x000322E9 File Offset: 0x000312E9
		public virtual Font Font
		{
			get
			{
				return this.font;
			}
			set
			{
				this.font = value;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x000322F2 File Offset: 0x000312F2
		public virtual string Content
		{
			get
			{
				return this.content.ToString();
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x000322FF File Offset: 0x000312FF
		public override string ToString()
		{
			return this.content.ToString();
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0003230C File Offset: 0x0003130C
		public virtual bool IsEmpty()
		{
			return this.content.ToString().Trim().Length == 0 && this.content.ToString().IndexOf("\n") == -1 && this.attributes == null;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00032348 File Offset: 0x00031348
		public float GetWidthPoint()
		{
			if (this.GetImage() != null)
			{
				return this.GetImage().ScaledWidth;
			}
			return this.font.GetCalculatedBaseFont(true).GetWidthPoint(this.Content, this.font.CalculatedSize) * this.HorizontalScaling;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00032387 File Offset: 0x00031387
		public bool HasAttributes()
		{
			return this.attributes != null;
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00032395 File Offset: 0x00031395
		// (set) Token: 0x06000992 RID: 2450 RVA: 0x0003239D File Offset: 0x0003139D
		public Dictionary<string, object> Attributes
		{
			get
			{
				return this.attributes;
			}
			set
			{
				this.attributes = value;
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x000323A6 File Offset: 0x000313A6
		private Chunk SetAttribute(string name, object obj)
		{
			if (this.attributes == null)
			{
				this.attributes = new Dictionary<string, object>();
			}
			this.attributes[name] = obj;
			return this;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x000323C9 File Offset: 0x000313C9
		public Chunk SetHorizontalScaling(float scale)
		{
			return this.SetAttribute("HSCALE", scale);
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x000323DC File Offset: 0x000313DC
		public float HorizontalScaling
		{
			get
			{
				if (this.attributes != null && this.attributes.ContainsKey("HSCALE"))
				{
					return (float)this.attributes["HSCALE"];
				}
				return 1f;
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00032413 File Offset: 0x00031413
		public Chunk SetUnderline(float thickness, float yPosition)
		{
			return this.SetUnderline(null, thickness, 0f, yPosition, 0f, 0);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0003242C File Offset: 0x0003142C
		public Chunk SetUnderline(BaseColor color, float thickness, float thicknessMul, float yPosition, float yPositionMul, int cap)
		{
			if (this.attributes == null)
			{
				this.attributes = new Dictionary<string, object>();
			}
			object[] item = new object[]
			{
				color,
				new float[]
				{
					thickness,
					thicknessMul,
					yPosition,
					yPositionMul,
					(float)cap
				}
			};
			object[][] original = null;
			if (this.attributes.ContainsKey("UNDERLINE"))
			{
				original = (object[][])this.attributes["UNDERLINE"];
			}
			object[][] obj = Utilities.AddToArray(original, item);
			return this.SetAttribute("UNDERLINE", obj);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x000324C1 File Offset: 0x000314C1
		public Chunk SetTextRise(float rise)
		{
			return this.SetAttribute("SUBSUPSCRIPT", rise);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000324D4 File Offset: 0x000314D4
		public float GetTextRise()
		{
			if (this.attributes != null && this.attributes.ContainsKey("SUBSUPSCRIPT"))
			{
				return (float)this.attributes["SUBSUPSCRIPT"];
			}
			return 0f;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0003250C File Offset: 0x0003150C
		public Chunk SetSkew(float alpha, float beta)
		{
			alpha = (float)Math.Tan((double)alpha * 3.141592653589793 / 180.0);
			beta = (float)Math.Tan((double)beta * 3.141592653589793 / 180.0);
			return this.SetAttribute("SKEW", new float[]
			{
				alpha,
				beta
			});
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00032570 File Offset: 0x00031570
		public Chunk SetBackground(BaseColor color)
		{
			return this.SetBackground(color, 0f, 0f, 0f, 0f);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00032590 File Offset: 0x00031590
		public Chunk SetBackground(BaseColor color, float extraLeft, float extraBottom, float extraRight, float extraTop)
		{
			return this.SetAttribute("BACKGROUND", new object[]
			{
				color,
				new float[]
				{
					extraLeft,
					extraBottom,
					extraRight,
					extraTop
				}
			});
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000325D4 File Offset: 0x000315D4
		public Chunk SetTextRenderMode(int mode, float strokeWidth, BaseColor strokeColor)
		{
			return this.SetAttribute("TEXTRENDERMODE", new object[]
			{
				mode,
				strokeWidth,
				strokeColor
			});
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0003260A File Offset: 0x0003160A
		public Chunk SetSplitCharacter(ISplitCharacter splitCharacter)
		{
			return this.SetAttribute("SPLITCHARACTER", splitCharacter);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00032618 File Offset: 0x00031618
		public Chunk SetHyphenation(IHyphenationEvent hyphenation)
		{
			return this.SetAttribute("HYPHENATION", hyphenation);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00032628 File Offset: 0x00031628
		public Chunk SetRemoteGoto(string filename, string name)
		{
			return this.SetAttribute("REMOTEGOTO", new object[]
			{
				filename,
				name
			});
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00032650 File Offset: 0x00031650
		public Chunk SetRemoteGoto(string filename, int page)
		{
			return this.SetAttribute("REMOTEGOTO", new object[]
			{
				filename,
				page
			});
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0003267D File Offset: 0x0003167D
		public Chunk SetLocalGoto(string name)
		{
			return this.SetAttribute("LOCALGOTO", name);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0003268B File Offset: 0x0003168B
		public Chunk SetLocalDestination(string name)
		{
			return this.SetAttribute("LOCALDESTINATION", name);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00032699 File Offset: 0x00031699
		public Chunk SetGenericTag(string text)
		{
			return this.SetAttribute("GENERICTAG", text);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x000326A7 File Offset: 0x000316A7
		public Image GetImage()
		{
			if (this.attributes != null && this.attributes.ContainsKey("IMAGE"))
			{
				return (Image)((object[])this.attributes["IMAGE"])[0];
			}
			return null;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000326E1 File Offset: 0x000316E1
		public static bool IsTag(string tag)
		{
			return "chunk".Equals(tag);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x000326EE File Offset: 0x000316EE
		public Chunk SetAction(PdfAction action)
		{
			return this.SetAttribute("ACTION", action);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000326FC File Offset: 0x000316FC
		public Chunk SetAnchor(Uri url)
		{
			return this.SetAttribute("ACTION", new PdfAction(url));
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0003270F File Offset: 0x0003170F
		public Chunk SetAnchor(string url)
		{
			return this.SetAttribute("ACTION", new PdfAction(url));
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00032722 File Offset: 0x00031722
		public Chunk SetNewPage()
		{
			return this.SetAttribute("NEWPAGE", null);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00032730 File Offset: 0x00031730
		public Chunk SetAnnotation(PdfAnnotation annotation)
		{
			return this.SetAttribute("PDFANNOTATION", annotation);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0003273E File Offset: 0x0003173E
		public bool IsContent()
		{
			return true;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00032741 File Offset: 0x00031741
		public bool IsNestable()
		{
			return true;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00032744 File Offset: 0x00031744
		public IHyphenationEvent GetHyphenation()
		{
			if (this.attributes != null && this.attributes.ContainsKey("HYPHENATION"))
			{
				return (IHyphenationEvent)this.attributes["HYPHENATION"];
			}
			return null;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00032777 File Offset: 0x00031777
		public Chunk SetCharacterSpacing(float charSpace)
		{
			return this.SetAttribute("CHAR_SPACING", charSpace);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0003278A File Offset: 0x0003178A
		public float GetCharacterSpacing()
		{
			if (this.attributes != null && this.attributes.ContainsKey("CHAR_SPACING"))
			{
				return (float)this.attributes["CHAR_SPACING"];
			}
			return 0f;
		}

		// Token: 0x040007E3 RID: 2019
		public const string OBJECT_REPLACEMENT_CHARACTER = "￼";

		// Token: 0x040007E4 RID: 2020
		public const string SEPARATOR = "SEPARATOR";

		// Token: 0x040007E5 RID: 2021
		public const string TAB = "TAB";

		// Token: 0x040007E6 RID: 2022
		public const string HSCALE = "HSCALE";

		// Token: 0x040007E7 RID: 2023
		public const string UNDERLINE = "UNDERLINE";

		// Token: 0x040007E8 RID: 2024
		public const string SUBSUPSCRIPT = "SUBSUPSCRIPT";

		// Token: 0x040007E9 RID: 2025
		public const string SKEW = "SKEW";

		// Token: 0x040007EA RID: 2026
		public const string BACKGROUND = "BACKGROUND";

		// Token: 0x040007EB RID: 2027
		public const string TEXTRENDERMODE = "TEXTRENDERMODE";

		// Token: 0x040007EC RID: 2028
		public const string SPLITCHARACTER = "SPLITCHARACTER";

		// Token: 0x040007ED RID: 2029
		public const string HYPHENATION = "HYPHENATION";

		// Token: 0x040007EE RID: 2030
		public const string REMOTEGOTO = "REMOTEGOTO";

		// Token: 0x040007EF RID: 2031
		public const string LOCALGOTO = "LOCALGOTO";

		// Token: 0x040007F0 RID: 2032
		public const string LOCALDESTINATION = "LOCALDESTINATION";

		// Token: 0x040007F1 RID: 2033
		public const string GENERICTAG = "GENERICTAG";

		// Token: 0x040007F2 RID: 2034
		public const string IMAGE = "IMAGE";

		// Token: 0x040007F3 RID: 2035
		public const string ACTION = "ACTION";

		// Token: 0x040007F4 RID: 2036
		public const string NEWPAGE = "NEWPAGE";

		// Token: 0x040007F5 RID: 2037
		public const string PDFANNOTATION = "PDFANNOTATION";

		// Token: 0x040007F6 RID: 2038
		public const string COLOR = "COLOR";

		// Token: 0x040007F7 RID: 2039
		public const string ENCODING = "ENCODING";

		// Token: 0x040007F8 RID: 2040
		public const string CHAR_SPACING = "CHAR_SPACING";

		// Token: 0x040007F9 RID: 2041
		public static readonly Chunk NEWLINE = new Chunk("\n");

		// Token: 0x040007FA RID: 2042
		public static readonly Chunk NEXTPAGE = new Chunk("");

		// Token: 0x040007FB RID: 2043
		protected StringBuilder content;

		// Token: 0x040007FC RID: 2044
		protected Font font;

		// Token: 0x040007FD RID: 2045
		protected Dictionary<string, object> attributes;
	}
}
