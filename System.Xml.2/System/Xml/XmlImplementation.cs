using System;

namespace System.Xml
{
	// Token: 0x0200010F RID: 271
	public class XmlImplementation
	{
		// Token: 0x060012F7 RID: 4855 RVA: 0x0004E573 File Offset: 0x0004C773
		public XmlImplementation() : this(new NameTable())
		{
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0004E580 File Offset: 0x0004C780
		public XmlImplementation(XmlNameTable nt)
		{
			this.nameTable = nt;
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0004E58F File Offset: 0x0004C78F
		public bool HasFeature(string strFeature, string strVersion)
		{
			return string.Compare("XML", strFeature, StringComparison.OrdinalIgnoreCase) == 0 && (strVersion == null || strVersion == "1.0" || strVersion == "2.0");
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0004E5BF File Offset: 0x0004C7BF
		public virtual XmlDocument CreateDocument()
		{
			return new XmlDocument(this);
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x0004E5C7 File Offset: 0x0004C7C7
		internal XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x04000549 RID: 1353
		private XmlNameTable nameTable;
	}
}
