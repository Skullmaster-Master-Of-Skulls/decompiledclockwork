using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.xml.simpleparser;

namespace iTextSharp.text.xml.xmp
{
	// Token: 0x020000B9 RID: 185
	public class XmpWriter
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x0001D894 File Offset: 0x0001C894
		public XmpWriter(Stream os, string utfEncoding, int extraSpace)
		{
			this.extraSpace = extraSpace;
			this.writer = new StreamWriter(os, new EncodingNoPreamble(IanaEncodings.GetEncodingEncoding(utfEncoding)));
			this.writer.Write("<?xpacket begin=\"﻿\" id=\"W5M0MpCehiHzreSzNTczkc9d\"?>\n");
			this.writer.Write("<x:xmpmeta xmlns:x=\"adobe:ns:meta/\">\n");
			this.writer.Write("<rdf:RDF xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\">\n");
			this.about = "";
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001D908 File Offset: 0x0001C908
		public XmpWriter(Stream os) : this(os, "UTF-8", 20)
		{
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001D918 File Offset: 0x0001C918
		public void SetReadOnly()
		{
			this.end = 'r';
		}

		// Token: 0x1700010B RID: 267
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x0001D922 File Offset: 0x0001C922
		public string About
		{
			set
			{
				this.about = value;
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001D92C File Offset: 0x0001C92C
		public void AddRdfDescription(string xmlns, string content)
		{
			this.writer.Write("<rdf:Description rdf:about=\"");
			this.writer.Write(this.about);
			this.writer.Write("\" ");
			this.writer.Write(xmlns);
			this.writer.Write(">");
			this.writer.Write(content);
			this.writer.Write("</rdf:Description>\n");
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001D9A4 File Offset: 0x0001C9A4
		public void AddRdfDescription(XmpSchema s)
		{
			this.writer.Write("<rdf:Description rdf:about=\"");
			this.writer.Write(this.about);
			this.writer.Write("\" ");
			this.writer.Write(s.Xmlns);
			this.writer.Write(">");
			this.writer.Write(s.ToString());
			this.writer.Write("</rdf:Description>\n");
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001DA24 File Offset: 0x0001CA24
		public void Close()
		{
			this.writer.Write("</rdf:RDF>");
			this.writer.Write("</x:xmpmeta>\n");
			for (int i = 0; i < this.extraSpace; i++)
			{
				this.writer.Write("                                                                                                   \n");
			}
			this.writer.Write((this.end == 'r') ? "<?xpacket end=\"r\"?>" : "<?xpacket end=\"w\"?>");
			this.writer.Flush();
			this.writer.Close();
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001DAAC File Offset: 0x0001CAAC
		public XmpWriter(Stream os, PdfDictionary info, int PdfXConformance) : this(os)
		{
			if (info != null)
			{
				DublinCoreSchema dublinCoreSchema = new DublinCoreSchema();
				PdfSchema pdfSchema = new PdfSchema();
				XmpBasicSchema xmpBasicSchema = new XmpBasicSchema();
				foreach (PdfName pdfName in info.Keys)
				{
					PdfObject pdfObject = info.Get(pdfName);
					if (pdfObject != null)
					{
						if (PdfName.TITLE.Equals(pdfName))
						{
							dublinCoreSchema.AddTitle(((PdfString)pdfObject).ToUnicodeString());
						}
						if (PdfName.AUTHOR.Equals(pdfName))
						{
							dublinCoreSchema.AddAuthor(((PdfString)pdfObject).ToUnicodeString());
						}
						if (PdfName.SUBJECT.Equals(pdfName))
						{
							dublinCoreSchema.AddSubject(((PdfString)pdfObject).ToUnicodeString());
							dublinCoreSchema.AddDescription(((PdfString)pdfObject).ToUnicodeString());
						}
						if (PdfName.KEYWORDS.Equals(pdfName))
						{
							pdfSchema.AddKeywords(((PdfString)pdfObject).ToUnicodeString());
						}
						if (PdfName.CREATOR.Equals(pdfName))
						{
							xmpBasicSchema.AddCreatorTool(((PdfString)pdfObject).ToUnicodeString());
						}
						if (PdfName.PRODUCER.Equals(pdfName))
						{
							pdfSchema.AddProducer(((PdfString)pdfObject).ToUnicodeString());
						}
						if (PdfName.CREATIONDATE.Equals(pdfName))
						{
							xmpBasicSchema.AddCreateDate(((PdfDate)pdfObject).GetW3CDate());
						}
						if (PdfName.MODDATE.Equals(pdfName))
						{
							xmpBasicSchema.AddModDate(((PdfDate)pdfObject).GetW3CDate());
						}
					}
				}
				if (dublinCoreSchema.Count > 0)
				{
					this.AddRdfDescription(dublinCoreSchema);
				}
				if (pdfSchema.Count > 0)
				{
					this.AddRdfDescription(pdfSchema);
				}
				if (xmpBasicSchema.Count > 0)
				{
					this.AddRdfDescription(xmpBasicSchema);
				}
				if (PdfXConformance == 3 || PdfXConformance == 4)
				{
					PdfA1Schema pdfA1Schema = new PdfA1Schema();
					if (PdfXConformance == 3)
					{
						pdfA1Schema.AddConformance("A");
					}
					else
					{
						pdfA1Schema.AddConformance("B");
					}
					this.AddRdfDescription(pdfA1Schema);
				}
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001DCA8 File Offset: 0x0001CCA8
		public XmpWriter(Stream os, IDictionary<string, string> info) : this(os)
		{
			if (info != null)
			{
				DublinCoreSchema dublinCoreSchema = new DublinCoreSchema();
				PdfSchema pdfSchema = new PdfSchema();
				XmpBasicSchema xmpBasicSchema = new XmpBasicSchema();
				foreach (KeyValuePair<string, string> keyValuePair in info)
				{
					string key = keyValuePair.Key;
					string value = keyValuePair.Value;
					if (value != null)
					{
						if ("Title".Equals(key))
						{
							dublinCoreSchema.AddTitle(value);
						}
						if ("Author".Equals(key))
						{
							dublinCoreSchema.AddAuthor(value);
						}
						if ("Subject".Equals(key))
						{
							dublinCoreSchema.AddSubject(value);
							dublinCoreSchema.AddDescription(value);
						}
						if ("Keywords".Equals(key))
						{
							pdfSchema.AddKeywords(value);
						}
						if ("Creator".Equals(key))
						{
							xmpBasicSchema.AddCreatorTool(value);
						}
						if ("Producer".Equals(key))
						{
							pdfSchema.AddProducer(value);
						}
						if ("CreationDate".Equals(key))
						{
							xmpBasicSchema.AddCreateDate(PdfDate.GetW3CDate(value));
						}
						if ("ModDate".Equals(key))
						{
							xmpBasicSchema.AddModDate(PdfDate.GetW3CDate(value));
						}
					}
				}
				if (dublinCoreSchema.Count > 0)
				{
					this.AddRdfDescription(dublinCoreSchema);
				}
				if (pdfSchema.Count > 0)
				{
					this.AddRdfDescription(pdfSchema);
				}
				if (xmpBasicSchema.Count > 0)
				{
					this.AddRdfDescription(xmpBasicSchema);
				}
			}
		}

		// Token: 0x040002C0 RID: 704
		public const string UTF8 = "UTF-8";

		// Token: 0x040002C1 RID: 705
		public const string UTF16 = "UTF-16";

		// Token: 0x040002C2 RID: 706
		public const string UTF16BE = "UTF-16BE";

		// Token: 0x040002C3 RID: 707
		public const string UTF16LE = "UTF-16LE";

		// Token: 0x040002C4 RID: 708
		public const string EXTRASPACE = "                                                                                                   \n";

		// Token: 0x040002C5 RID: 709
		public const string XPACKET_PI_BEGIN = "<?xpacket begin=\"﻿\" id=\"W5M0MpCehiHzreSzNTczkc9d\"?>\n";

		// Token: 0x040002C6 RID: 710
		public const string XPACKET_PI_END_W = "<?xpacket end=\"w\"?>";

		// Token: 0x040002C7 RID: 711
		public const string XPACKET_PI_END_R = "<?xpacket end=\"r\"?>";

		// Token: 0x040002C8 RID: 712
		protected int extraSpace;

		// Token: 0x040002C9 RID: 713
		protected StreamWriter writer;

		// Token: 0x040002CA RID: 714
		protected string about;

		// Token: 0x040002CB RID: 715
		protected char end = 'w';
	}
}
