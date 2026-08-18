using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x020000E4 RID: 228
	internal class XmlName : IXmlSchemaInfo
	{
		// Token: 0x06000DE2 RID: 3554 RVA: 0x0003DEBC File Offset: 0x0003CEBC
		public static XmlName Create(string prefix, string localName, string ns, int hashCode, XmlDocument ownerDoc, XmlName next, IXmlSchemaInfo schemaInfo)
		{
			if (schemaInfo == null)
			{
				return new XmlName(prefix, localName, ns, hashCode, ownerDoc, next);
			}
			return new XmlNameEx(prefix, localName, ns, hashCode, ownerDoc, next, schemaInfo);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0003DEDF File Offset: 0x0003CEDF
		internal XmlName(string prefix, string localName, string ns, int hashCode, XmlDocument ownerDoc, XmlName next)
		{
			this.prefix = prefix;
			this.localName = localName;
			this.ns = ns;
			this.name = null;
			this.hashCode = hashCode;
			this.ownerDoc = ownerDoc;
			this.next = next;
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0003DF1B File Offset: 0x0003CF1B
		public string LocalName
		{
			get
			{
				return this.localName;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x0003DF23 File Offset: 0x0003CF23
		public string NamespaceURI
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0003DF2B File Offset: 0x0003CF2B
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0003DF33 File Offset: 0x0003CF33
		public int HashCode
		{
			get
			{
				return this.hashCode;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x0003DF3B File Offset: 0x0003CF3B
		public XmlDocument OwnerDocument
		{
			get
			{
				return this.ownerDoc;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0003DF44 File Offset: 0x0003CF44
		public string Name
		{
			get
			{
				if (this.name == null)
				{
					if (this.prefix.Length > 0)
					{
						if (this.localName.Length > 0)
						{
							string array = this.prefix + ":" + this.localName;
							lock (this.ownerDoc.NameTable)
							{
								if (this.name == null)
								{
									this.name = this.ownerDoc.NameTable.Add(array);
								}
								goto IL_92;
							}
						}
						this.name = this.prefix;
					}
					else
					{
						this.name = this.localName;
					}
				}
				IL_92:
				return this.name;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0003DFFC File Offset: 0x0003CFFC
		public virtual XmlSchemaValidity Validity
		{
			get
			{
				return XmlSchemaValidity.NotKnown;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0003DFFF File Offset: 0x0003CFFF
		public virtual bool IsDefault
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x0003E002 File Offset: 0x0003D002
		public virtual bool IsNil
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x0003E005 File Offset: 0x0003D005
		public virtual XmlSchemaSimpleType MemberType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x0003E008 File Offset: 0x0003D008
		public virtual XmlSchemaType SchemaType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x0003E00B File Offset: 0x0003D00B
		public virtual XmlSchemaElement SchemaElement
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0003E00E File Offset: 0x0003D00E
		public virtual XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0003E011 File Offset: 0x0003D011
		public virtual bool Equals(IXmlSchemaInfo schemaInfo)
		{
			return schemaInfo == null;
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0003E018 File Offset: 0x0003D018
		public static int GetHashCode(string name)
		{
			int num = 0;
			if (name != null)
			{
				for (int i = name.Length - 1; i >= 0; i--)
				{
					char c = name[i];
					if (c == ':')
					{
						break;
					}
					num += (num << 7 ^ (int)c);
				}
				num -= num >> 17;
				num -= num >> 11;
				num -= num >> 5;
			}
			return num;
		}

		// Token: 0x0400096D RID: 2413
		private string prefix;

		// Token: 0x0400096E RID: 2414
		private string localName;

		// Token: 0x0400096F RID: 2415
		private string ns;

		// Token: 0x04000970 RID: 2416
		private string name;

		// Token: 0x04000971 RID: 2417
		private int hashCode;

		// Token: 0x04000972 RID: 2418
		internal XmlDocument ownerDoc;

		// Token: 0x04000973 RID: 2419
		internal XmlName next;
	}
}
