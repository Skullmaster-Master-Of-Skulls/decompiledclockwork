using System;

namespace iTextSharp.text.xml.xmp
{
	// Token: 0x02000158 RID: 344
	public class XmpBasicSchema : XmpSchema
	{
		// Token: 0x06000C53 RID: 3155 RVA: 0x00043ABD File Offset: 0x00042ABD
		public XmpBasicSchema() : base("xmlns:xmp=\"http://ns.adobe.com/xap/1.0/\"")
		{
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00043ACA File Offset: 0x00042ACA
		public void AddCreatorTool(string creator)
		{
			this["xmp:CreatorTool"] = creator;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00043AD8 File Offset: 0x00042AD8
		public void AddCreateDate(string date)
		{
			this["xmp:CreateDate"] = date;
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00043AE6 File Offset: 0x00042AE6
		public void AddModDate(string date)
		{
			this["xmp:ModifyDate"] = date;
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00043AF4 File Offset: 0x00042AF4
		public void AddMetaDataDate(string date)
		{
			this["xmp:MetadataDate"] = date;
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00043B04 File Offset: 0x00042B04
		public void AddIdentifiers(string[] id)
		{
			XmpArray xmpArray = new XmpArray("rdf:Bag");
			for (int i = 0; i < id.Length; i++)
			{
				xmpArray.Add(id[i]);
			}
			base.SetProperty("xmp:Identifier", xmpArray);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00043B3F File Offset: 0x00042B3F
		public void AddNickname(string name)
		{
			this["xmp:Nickname"] = name;
		}

		// Token: 0x04000997 RID: 2455
		public const string DEFAULT_XPATH_ID = "xmp";

		// Token: 0x04000998 RID: 2456
		public const string DEFAULT_XPATH_URI = "http://ns.adobe.com/xap/1.0/";

		// Token: 0x04000999 RID: 2457
		public const string ADVISORY = "xmp:Advisory";

		// Token: 0x0400099A RID: 2458
		public const string BASEURL = "xmp:BaseURL";

		// Token: 0x0400099B RID: 2459
		public const string CREATEDATE = "xmp:CreateDate";

		// Token: 0x0400099C RID: 2460
		public const string CREATORTOOL = "xmp:CreatorTool";

		// Token: 0x0400099D RID: 2461
		public const string IDENTIFIER = "xmp:Identifier";

		// Token: 0x0400099E RID: 2462
		public const string METADATADATE = "xmp:MetadataDate";

		// Token: 0x0400099F RID: 2463
		public const string MODIFYDATE = "xmp:ModifyDate";

		// Token: 0x040009A0 RID: 2464
		public const string NICKNAME = "xmp:Nickname";

		// Token: 0x040009A1 RID: 2465
		public const string THUMBNAILS = "xmp:Thumbnails";
	}
}
