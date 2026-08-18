using System;

namespace System.Xml
{
	// Token: 0x020000E2 RID: 226
	public class XmlImplementation
	{
		// Token: 0x06000DC2 RID: 3522 RVA: 0x0003C6C3 File Offset: 0x0003B6C3
		public XmlImplementation() : this(new NameTable())
		{
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0003C6D0 File Offset: 0x0003B6D0
		public XmlImplementation(XmlNameTable nt)
		{
			this.nameTable = nt;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0003C6DF File Offset: 0x0003B6DF
		public bool HasFeature(string strFeature, string strVersion)
		{
			return string.Compare("XML", strFeature, StringComparison.OrdinalIgnoreCase) == 0 && (strVersion == null || strVersion == "1.0" || strVersion == "2.0");
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0003C70F File Offset: 0x0003B70F
		public virtual XmlDocument CreateDocument()
		{
			return new XmlDocument(this);
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x0003C717 File Offset: 0x0003B717
		internal XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x04000969 RID: 2409
		private XmlNameTable nameTable;
	}
}
