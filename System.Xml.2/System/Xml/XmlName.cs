using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x02000112 RID: 274
	internal class XmlName : IXmlSchemaInfo
	{
		// Token: 0x0600131B RID: 4891 RVA: 0x0004FE35 File Offset: 0x0004E035
		public static XmlName Create(string prefix, string localName, string ns, int hashCode, XmlDocument ownerDoc, XmlName next, IXmlSchemaInfo schemaInfo)
		{
			if (schemaInfo == null)
			{
				return new XmlName(prefix, localName, ns, hashCode, ownerDoc, next);
			}
			return new XmlNameEx(prefix, localName, ns, hashCode, ownerDoc, next, schemaInfo);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0004FE58 File Offset: 0x0004E058
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

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x0004FE94 File Offset: 0x0004E094
		public string LocalName
		{
			get
			{
				return this.localName;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x0004FE9C File Offset: 0x0004E09C
		public string NamespaceURI
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x0004FEA4 File Offset: 0x0004E0A4
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x0004FEAC File Offset: 0x0004E0AC
		public int HashCode
		{
			get
			{
				return this.hashCode;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x0004FEB4 File Offset: 0x0004E0B4
		public XmlDocument OwnerDocument
		{
			get
			{
				return this.ownerDoc;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x0004FEBC File Offset: 0x0004E0BC
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
							XmlNameTable nameTable = this.ownerDoc.NameTable;
							lock (nameTable)
							{
								if (this.name == null)
								{
									this.name = this.ownerDoc.NameTable.Add(array);
								}
								goto IL_99;
							}
						}
						this.name = this.prefix;
					}
					else
					{
						this.name = this.localName;
					}
				}
				IL_99:
				return this.name;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x0004FF78 File Offset: 0x0004E178
		public virtual XmlSchemaValidity Validity
		{
			get
			{
				return XmlSchemaValidity.NotKnown;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x0004FF7B File Offset: 0x0004E17B
		public virtual bool IsDefault
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x0004FF7E File Offset: 0x0004E17E
		public virtual bool IsNil
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x0004FF81 File Offset: 0x0004E181
		public virtual XmlSchemaSimpleType MemberType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x0004FF84 File Offset: 0x0004E184
		public virtual XmlSchemaType SchemaType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x0004FF87 File Offset: 0x0004E187
		public virtual XmlSchemaElement SchemaElement
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x0004FF8A File Offset: 0x0004E18A
		public virtual XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0004FF8D File Offset: 0x0004E18D
		public virtual bool Equals(IXmlSchemaInfo schemaInfo)
		{
			return schemaInfo == null;
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0004FF94 File Offset: 0x0004E194
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

		// Token: 0x0400054E RID: 1358
		private string prefix;

		// Token: 0x0400054F RID: 1359
		private string localName;

		// Token: 0x04000550 RID: 1360
		private string ns;

		// Token: 0x04000551 RID: 1361
		private string name;

		// Token: 0x04000552 RID: 1362
		private int hashCode;

		// Token: 0x04000553 RID: 1363
		internal XmlDocument ownerDoc;

		// Token: 0x04000554 RID: 1364
		internal XmlName next;
	}
}
