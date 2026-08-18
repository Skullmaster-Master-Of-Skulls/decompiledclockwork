using System;

namespace iTextSharp.text.xml.xmp
{
	// Token: 0x02000631 RID: 1585
	public class XmpMMSchema : XmpSchema
	{
		// Token: 0x060035A7 RID: 13735 RVA: 0x0014C41F File Offset: 0x0014B41F
		public XmpMMSchema() : base("xmlns:xmpMM=\"http://ns.adobe.com/xap/1.0/mm/\"")
		{
		}

		// Token: 0x040023EB RID: 9195
		public const string DEFAULT_XPATH_ID = "xmpMM";

		// Token: 0x040023EC RID: 9196
		public const string DEFAULT_XPATH_URI = "http://ns.adobe.com/xap/1.0/mm/";

		// Token: 0x040023ED RID: 9197
		public const string DERIVEDFROM = "xmpMM:DerivedFrom";

		// Token: 0x040023EE RID: 9198
		public const string DOCUMENTID = "xmpMM:DocumentID";

		// Token: 0x040023EF RID: 9199
		public const string HISTORY = "xmpMM:History";

		// Token: 0x040023F0 RID: 9200
		public const string MANAGEDFROM = "xmpMM:ManagedFrom";

		// Token: 0x040023F1 RID: 9201
		public const string MANAGER = "xmpMM:Manager";

		// Token: 0x040023F2 RID: 9202
		public const string MANAGETO = "xmpMM:ManageTo";

		// Token: 0x040023F3 RID: 9203
		public const string MANAGEUI = "xmpMM:ManageUI";

		// Token: 0x040023F4 RID: 9204
		public const string MANAGERVARIANT = "xmpMM:ManagerVariant";

		// Token: 0x040023F5 RID: 9205
		public const string RENDITIONCLASS = "xmpMM:RenditionClass";

		// Token: 0x040023F6 RID: 9206
		public const string RENDITIONPARAMS = "xmpMM:RenditionParams";

		// Token: 0x040023F7 RID: 9207
		public const string VERSIONID = "xmpMM:VersionID";

		// Token: 0x040023F8 RID: 9208
		public const string VERSIONS = "xmpMM:Versions";
	}
}
