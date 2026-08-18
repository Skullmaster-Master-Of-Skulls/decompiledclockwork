using System;

namespace iTextSharp.text.xml.xmp
{
	// Token: 0x02000633 RID: 1587
	public class DublinCoreSchema : XmpSchema
	{
		// Token: 0x060035AA RID: 13738 RVA: 0x0014C4F4 File Offset: 0x0014B4F4
		public DublinCoreSchema() : base("xmlns:dc=\"http://purl.org/dc/elements/1.1/\"")
		{
			this["dc:format"] = "application/pdf";
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x0014C514 File Offset: 0x0014B514
		public void AddTitle(string title)
		{
			base.SetProperty("dc:title", new XmpArray("rdf:Alt")
			{
				title
			});
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x0014C540 File Offset: 0x0014B540
		public void AddDescription(string desc)
		{
			base.SetProperty("dc:description", new XmpArray("rdf:Alt")
			{
				desc
			});
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x0014C56C File Offset: 0x0014B56C
		public void AddSubject(string subject)
		{
			base.SetProperty("dc:subject", new XmpArray("rdf:Bag")
			{
				subject
			});
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x0014C598 File Offset: 0x0014B598
		public void addSubject(string[] subject)
		{
			XmpArray xmpArray = new XmpArray("rdf:Bag");
			for (int i = 0; i < subject.Length; i++)
			{
				xmpArray.Add(subject[i]);
			}
			base.SetProperty("dc:subject", xmpArray);
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x0014C5D4 File Offset: 0x0014B5D4
		public void AddAuthor(string author)
		{
			base.SetProperty("dc:creator", new XmpArray("rdf:Seq")
			{
				author
			});
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x0014C600 File Offset: 0x0014B600
		public void AddAuthor(string[] author)
		{
			XmpArray xmpArray = new XmpArray("rdf:Seq");
			for (int i = 0; i < author.Length; i++)
			{
				xmpArray.Add(author[i]);
			}
			base.SetProperty("dc:creator", xmpArray);
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x0014C63C File Offset: 0x0014B63C
		public void AddPublisher(string publisher)
		{
			base.SetProperty("dc:publisher", new XmpArray("rdf:Seq")
			{
				publisher
			});
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x0014C668 File Offset: 0x0014B668
		public void AddPublisher(string[] publisher)
		{
			XmpArray xmpArray = new XmpArray("rdf:Seq");
			for (int i = 0; i < publisher.Length; i++)
			{
				xmpArray.Add(publisher[i]);
			}
			base.SetProperty("dc:publisher", xmpArray);
		}

		// Token: 0x040023FD RID: 9213
		public const string DEFAULT_XPATH_ID = "dc";

		// Token: 0x040023FE RID: 9214
		public const string DEFAULT_XPATH_URI = "http://purl.org/dc/elements/1.1/";

		// Token: 0x040023FF RID: 9215
		public const string CONTRIBUTOR = "dc:contributor";

		// Token: 0x04002400 RID: 9216
		public const string COVERAGE = "dc:coverage";

		// Token: 0x04002401 RID: 9217
		public const string CREATOR = "dc:creator";

		// Token: 0x04002402 RID: 9218
		public const string DATE = "dc:date";

		// Token: 0x04002403 RID: 9219
		public const string DESCRIPTION = "dc:description";

		// Token: 0x04002404 RID: 9220
		public const string FORMAT = "dc:format";

		// Token: 0x04002405 RID: 9221
		public const string IDENTIFIER = "dc:identifier";

		// Token: 0x04002406 RID: 9222
		public const string LANGUAGE = "dc:language";

		// Token: 0x04002407 RID: 9223
		public const string PUBLISHER = "dc:publisher";

		// Token: 0x04002408 RID: 9224
		public const string RELATION = "dc:relation";

		// Token: 0x04002409 RID: 9225
		public const string RIGHTS = "dc:rights";

		// Token: 0x0400240A RID: 9226
		public const string SOURCE = "dc:source";

		// Token: 0x0400240B RID: 9227
		public const string SUBJECT = "dc:subject";

		// Token: 0x0400240C RID: 9228
		public const string TITLE = "dc:title";

		// Token: 0x0400240D RID: 9229
		public const string TYPE = "dc:type";
	}
}
