using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000260 RID: 608
	public class XmlSchemaIdentityConstraint : XmlSchemaAnnotated
	{
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x00083255 File Offset: 0x00082255
		// (set) Token: 0x06001C72 RID: 7282 RVA: 0x0008325D File Offset: 0x0008225D
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x00083266 File Offset: 0x00082266
		// (set) Token: 0x06001C74 RID: 7284 RVA: 0x0008326E File Offset: 0x0008226E
		[XmlElement("selector", typeof(XmlSchemaXPath))]
		public XmlSchemaXPath Selector
		{
			get
			{
				return this.selector;
			}
			set
			{
				this.selector = value;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x00083277 File Offset: 0x00082277
		[XmlElement("field", typeof(XmlSchemaXPath))]
		public XmlSchemaObjectCollection Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x0008327F File Offset: 0x0008227F
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x00083287 File Offset: 0x00082287
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qualifiedName = value;
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x00083290 File Offset: 0x00082290
		// (set) Token: 0x06001C79 RID: 7289 RVA: 0x00083298 File Offset: 0x00082298
		[XmlIgnore]
		internal CompiledIdentityConstraint CompiledConstraint
		{
			get
			{
				return this.compiledConstraint;
			}
			set
			{
				this.compiledConstraint = value;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x000832A1 File Offset: 0x000822A1
		// (set) Token: 0x06001C7B RID: 7291 RVA: 0x000832A9 File Offset: 0x000822A9
		[XmlIgnore]
		internal override string NameAttribute
		{
			get
			{
				return this.Name;
			}
			set
			{
				this.Name = value;
			}
		}

		// Token: 0x0400118C RID: 4492
		private string name;

		// Token: 0x0400118D RID: 4493
		private XmlSchemaXPath selector;

		// Token: 0x0400118E RID: 4494
		private XmlSchemaObjectCollection fields = new XmlSchemaObjectCollection();

		// Token: 0x0400118F RID: 4495
		private XmlQualifiedName qualifiedName = XmlQualifiedName.Empty;

		// Token: 0x04001190 RID: 4496
		private CompiledIdentityConstraint compiledConstraint;
	}
}
