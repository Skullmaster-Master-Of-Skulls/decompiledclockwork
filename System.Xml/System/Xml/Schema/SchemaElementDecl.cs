using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000211 RID: 529
	internal sealed class SchemaElementDecl : SchemaDeclBase
	{
		// Token: 0x0600194F RID: 6479 RVA: 0x00079A78 File Offset: 0x00078A78
		public SchemaElementDecl()
		{
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x00079A96 File Offset: 0x00078A96
		public SchemaElementDecl(XmlSchemaDatatype dtype)
		{
			base.Datatype = dtype;
			this.contentValidator = ContentValidator.TextOnly;
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x00079AC6 File Offset: 0x00078AC6
		public SchemaElementDecl(XmlQualifiedName name, string prefix, SchemaType schemaType) : base(name, prefix)
		{
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00079AE8 File Offset: 0x00078AE8
		public static SchemaElementDecl CreateAnyTypeElementDecl()
		{
			return new SchemaElementDecl
			{
				Datatype = DatatypeImplementation.AnySimpleType.Datatype
			};
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00079B0C File Offset: 0x00078B0C
		public SchemaElementDecl Clone()
		{
			return (SchemaElementDecl)base.MemberwiseClone();
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x00079B19 File Offset: 0x00078B19
		// (set) Token: 0x06001955 RID: 6485 RVA: 0x00079B21 File Offset: 0x00078B21
		public bool IsAbstract
		{
			get
			{
				return this.isAbstract;
			}
			set
			{
				this.isAbstract = value;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x00079B2A File Offset: 0x00078B2A
		// (set) Token: 0x06001957 RID: 6487 RVA: 0x00079B32 File Offset: 0x00078B32
		public bool IsNillable
		{
			get
			{
				return this.isNillable;
			}
			set
			{
				this.isNillable = value;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001958 RID: 6488 RVA: 0x00079B3B File Offset: 0x00078B3B
		// (set) Token: 0x06001959 RID: 6489 RVA: 0x00079B43 File Offset: 0x00078B43
		public XmlSchemaDerivationMethod Block
		{
			get
			{
				return this.block;
			}
			set
			{
				this.block = value;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x00079B4C File Offset: 0x00078B4C
		// (set) Token: 0x0600195B RID: 6491 RVA: 0x00079B54 File Offset: 0x00078B54
		public bool IsIdDeclared
		{
			get
			{
				return this.isIdDeclared;
			}
			set
			{
				this.isIdDeclared = value;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x00079B5D File Offset: 0x00078B5D
		// (set) Token: 0x0600195D RID: 6493 RVA: 0x00079B65 File Offset: 0x00078B65
		public bool IsNotationDeclared
		{
			get
			{
				return this.isNotationDeclared;
			}
			set
			{
				this.isNotationDeclared = value;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x00079B6E File Offset: 0x00078B6E
		public bool HasDefaultAttribute
		{
			get
			{
				return this.defaultAttdefs != null;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x00079B7C File Offset: 0x00078B7C
		// (set) Token: 0x06001960 RID: 6496 RVA: 0x00079B84 File Offset: 0x00078B84
		public bool HasRequiredAttribute
		{
			get
			{
				return this.hasRequiredAttribute;
			}
			set
			{
				this.hasRequiredAttribute = value;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x00079B8D File Offset: 0x00078B8D
		// (set) Token: 0x06001962 RID: 6498 RVA: 0x00079B95 File Offset: 0x00078B95
		public bool HasNonCDataAttribute
		{
			get
			{
				return this.hasNonCDataAttribute;
			}
			set
			{
				this.hasNonCDataAttribute = value;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x00079B9E File Offset: 0x00078B9E
		// (set) Token: 0x06001964 RID: 6500 RVA: 0x00079BA6 File Offset: 0x00078BA6
		public ContentValidator ContentValidator
		{
			get
			{
				return this.contentValidator;
			}
			set
			{
				this.contentValidator = value;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001965 RID: 6501 RVA: 0x00079BAF File Offset: 0x00078BAF
		// (set) Token: 0x06001966 RID: 6502 RVA: 0x00079BB7 File Offset: 0x00078BB7
		public XmlSchemaAnyAttribute AnyAttribute
		{
			get
			{
				return this.anyAttribute;
			}
			set
			{
				this.anyAttribute = value;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x00079BC0 File Offset: 0x00078BC0
		// (set) Token: 0x06001968 RID: 6504 RVA: 0x00079BC8 File Offset: 0x00078BC8
		public CompiledIdentityConstraint[] Constraints
		{
			get
			{
				return this.constraints;
			}
			set
			{
				this.constraints = value;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x00079BD1 File Offset: 0x00078BD1
		// (set) Token: 0x0600196A RID: 6506 RVA: 0x00079BD9 File Offset: 0x00078BD9
		public XmlSchemaElement SchemaElement
		{
			get
			{
				return this.schemaElement;
			}
			set
			{
				this.schemaElement = value;
			}
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x00079BE4 File Offset: 0x00078BE4
		public void AddAttDef(SchemaAttDef attdef)
		{
			this.attdefs.Add(attdef.Name, attdef);
			if (attdef.Presence == SchemaDeclBase.Use.Required || attdef.Presence == SchemaDeclBase.Use.RequiredFixed)
			{
				this.hasRequiredAttribute = true;
			}
			if (attdef.Presence == SchemaDeclBase.Use.Default || attdef.Presence == SchemaDeclBase.Use.Fixed)
			{
				if (this.tmpDefaultAttdefs == null)
				{
					this.tmpDefaultAttdefs = new ArrayList();
				}
				this.tmpDefaultAttdefs.Add(attdef);
			}
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x00079C4D File Offset: 0x00078C4D
		public void EndAddAttDef()
		{
			if (this.tmpDefaultAttdefs != null)
			{
				this.defaultAttdefs = (SchemaAttDef[])this.tmpDefaultAttdefs.ToArray(typeof(SchemaAttDef));
				this.tmpDefaultAttdefs = null;
			}
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00079C7E File Offset: 0x00078C7E
		public SchemaAttDef GetAttDef(XmlQualifiedName qname)
		{
			return (SchemaAttDef)this.attdefs[qname];
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x00079C91 File Offset: 0x00078C91
		public Hashtable AttDefs
		{
			get
			{
				return this.attdefs;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x00079C99 File Offset: 0x00078C99
		public SchemaAttDef[] DefaultAttDefs
		{
			get
			{
				return this.defaultAttdefs;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x00079CA1 File Offset: 0x00078CA1
		public Hashtable ProhibitedAttributes
		{
			get
			{
				return this.prohibitedAttributes;
			}
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x00079CAC File Offset: 0x00078CAC
		public void CheckAttributes(Hashtable presence, bool standalone)
		{
			foreach (object obj in this.attdefs.Values)
			{
				SchemaAttDef schemaAttDef = (SchemaAttDef)obj;
				if (presence[schemaAttDef.Name] == null)
				{
					if (schemaAttDef.Presence == SchemaDeclBase.Use.Required)
					{
						throw new XmlSchemaException("Sch_MissRequiredAttribute", schemaAttDef.Name.ToString());
					}
					if (standalone && schemaAttDef.IsDeclaredInExternal && (schemaAttDef.Presence == SchemaDeclBase.Use.Default || schemaAttDef.Presence == SchemaDeclBase.Use.Fixed))
					{
						throw new XmlSchemaException("Sch_StandAlone", string.Empty);
					}
				}
			}
		}

		// Token: 0x04000EC6 RID: 3782
		private ContentValidator contentValidator;

		// Token: 0x04000EC7 RID: 3783
		private Hashtable attdefs = new Hashtable();

		// Token: 0x04000EC8 RID: 3784
		private Hashtable prohibitedAttributes = new Hashtable();

		// Token: 0x04000EC9 RID: 3785
		private ArrayList tmpDefaultAttdefs;

		// Token: 0x04000ECA RID: 3786
		private SchemaAttDef[] defaultAttdefs;

		// Token: 0x04000ECB RID: 3787
		private bool isAbstract;

		// Token: 0x04000ECC RID: 3788
		private bool isNillable;

		// Token: 0x04000ECD RID: 3789
		private XmlSchemaDerivationMethod block;

		// Token: 0x04000ECE RID: 3790
		private bool isIdDeclared;

		// Token: 0x04000ECF RID: 3791
		private bool isNotationDeclared;

		// Token: 0x04000ED0 RID: 3792
		private bool hasRequiredAttribute;

		// Token: 0x04000ED1 RID: 3793
		private bool hasNonCDataAttribute;

		// Token: 0x04000ED2 RID: 3794
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04000ED3 RID: 3795
		private CompiledIdentityConstraint[] constraints;

		// Token: 0x04000ED4 RID: 3796
		private XmlSchemaElement schemaElement;

		// Token: 0x04000ED5 RID: 3797
		public static readonly SchemaElementDecl Empty = new SchemaElementDecl();
	}
}
