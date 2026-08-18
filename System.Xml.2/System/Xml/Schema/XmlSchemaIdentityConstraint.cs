using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000299 RID: 665
	public class XmlSchemaIdentityConstraint : XmlSchemaAnnotated
	{
		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x060026FC RID: 9980 RVA: 0x000CF38F File Offset: 0x000CD58F
		// (set) Token: 0x060026FD RID: 9981 RVA: 0x000CF397 File Offset: 0x000CD597
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

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x060026FE RID: 9982 RVA: 0x000CF3A0 File Offset: 0x000CD5A0
		// (set) Token: 0x060026FF RID: 9983 RVA: 0x000CF3A8 File Offset: 0x000CD5A8
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

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002700 RID: 9984 RVA: 0x000CF3B1 File Offset: 0x000CD5B1
		[XmlElement("field", typeof(XmlSchemaXPath))]
		public XmlSchemaObjectCollection Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002701 RID: 9985 RVA: 0x000CF3B9 File Offset: 0x000CD5B9
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x000CF3C1 File Offset: 0x000CD5C1
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qualifiedName = value;
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002703 RID: 9987 RVA: 0x000CF3CA File Offset: 0x000CD5CA
		// (set) Token: 0x06002704 RID: 9988 RVA: 0x000CF3D2 File Offset: 0x000CD5D2
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

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002705 RID: 9989 RVA: 0x000CF3DB File Offset: 0x000CD5DB
		// (set) Token: 0x06002706 RID: 9990 RVA: 0x000CF3E3 File Offset: 0x000CD5E3
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

		// Token: 0x04001108 RID: 4360
		private string name;

		// Token: 0x04001109 RID: 4361
		private XmlSchemaXPath selector;

		// Token: 0x0400110A RID: 4362
		private XmlSchemaObjectCollection fields = new XmlSchemaObjectCollection();

		// Token: 0x0400110B RID: 4363
		private XmlQualifiedName qualifiedName = XmlQualifiedName.Empty;

		// Token: 0x0400110C RID: 4364
		private CompiledIdentityConstraint compiledConstraint;
	}
}
