using System;

namespace System.Xml.Serialization
{
	// Token: 0x020001BD RID: 445
	[AttributeUsage(AttributeTargets.Assembly)]
	public sealed class XmlSerializerVersionAttribute : Attribute
	{
		// Token: 0x06001ED7 RID: 7895 RVA: 0x000A8EBC File Offset: 0x000A70BC
		public XmlSerializerVersionAttribute()
		{
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x000A8EC4 File Offset: 0x000A70C4
		public XmlSerializerVersionAttribute(Type type)
		{
			this.type = type;
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x000A8ED3 File Offset: 0x000A70D3
		// (set) Token: 0x06001EDA RID: 7898 RVA: 0x000A8EDB File Offset: 0x000A70DB
		public string ParentAssemblyId
		{
			get
			{
				return this.mvid;
			}
			set
			{
				this.mvid = value;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x000A8EE4 File Offset: 0x000A70E4
		// (set) Token: 0x06001EDC RID: 7900 RVA: 0x000A8EEC File Offset: 0x000A70EC
		public string Version
		{
			get
			{
				return this.serializerVersion;
			}
			set
			{
				this.serializerVersion = value;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x000A8EF5 File Offset: 0x000A70F5
		// (set) Token: 0x06001EDE RID: 7902 RVA: 0x000A8EFD File Offset: 0x000A70FD
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001EDF RID: 7903 RVA: 0x000A8F06 File Offset: 0x000A7106
		// (set) Token: 0x06001EE0 RID: 7904 RVA: 0x000A8F0E File Offset: 0x000A710E
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x04000CEA RID: 3306
		private string mvid;

		// Token: 0x04000CEB RID: 3307
		private string serializerVersion;

		// Token: 0x04000CEC RID: 3308
		private string ns;

		// Token: 0x04000CED RID: 3309
		private Type type;
	}
}
