using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x0200025B RID: 603
	internal sealed class SchemaElementDecl : SchemaDeclBase, IDtdAttributeListInfo
	{
		// Token: 0x0600240D RID: 9229 RVA: 0x000C6140 File Offset: 0x000C4340
		internal SchemaElementDecl()
		{
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x000C615E File Offset: 0x000C435E
		internal SchemaElementDecl(XmlSchemaDatatype dtype)
		{
			base.Datatype = dtype;
			this.contentValidator = ContentValidator.TextOnly;
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000C618E File Offset: 0x000C438E
		internal SchemaElementDecl(XmlQualifiedName name, string prefix) : base(name, prefix)
		{
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000C61B0 File Offset: 0x000C43B0
		internal static SchemaElementDecl CreateAnyTypeElementDecl()
		{
			return new SchemaElementDecl
			{
				Datatype = DatatypeImplementation.AnySimpleType.Datatype
			};
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002411 RID: 9233 RVA: 0x000C61D4 File Offset: 0x000C43D4
		string IDtdAttributeListInfo.Prefix
		{
			get
			{
				return this.Prefix;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002412 RID: 9234 RVA: 0x000C61DC File Offset: 0x000C43DC
		string IDtdAttributeListInfo.LocalName
		{
			get
			{
				return this.Name.Name;
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06002413 RID: 9235 RVA: 0x000C61E9 File Offset: 0x000C43E9
		bool IDtdAttributeListInfo.HasNonCDataAttributes
		{
			get
			{
				return this.hasNonCDataAttribute;
			}
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000C61F4 File Offset: 0x000C43F4
		IDtdAttributeInfo IDtdAttributeListInfo.LookupAttribute(string prefix, string localName)
		{
			XmlQualifiedName key = new XmlQualifiedName(localName, prefix);
			SchemaAttDef result;
			if (this.attdefs.TryGetValue(key, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000C621C File Offset: 0x000C441C
		IEnumerable<IDtdDefaultAttributeInfo> IDtdAttributeListInfo.LookupDefaultAttributes()
		{
			return this.defaultAttdefs;
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000C6224 File Offset: 0x000C4424
		IDtdAttributeInfo IDtdAttributeListInfo.LookupIdAttribute()
		{
			foreach (SchemaAttDef schemaAttDef in this.attdefs.Values)
			{
				if (schemaAttDef.TokenizedType == XmlTokenizedType.ID)
				{
					return schemaAttDef;
				}
			}
			return null;
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002417 RID: 9239 RVA: 0x000C6288 File Offset: 0x000C4488
		// (set) Token: 0x06002418 RID: 9240 RVA: 0x000C6290 File Offset: 0x000C4490
		internal bool IsIdDeclared
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

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x000C6299 File Offset: 0x000C4499
		// (set) Token: 0x0600241A RID: 9242 RVA: 0x000C62A1 File Offset: 0x000C44A1
		internal bool HasNonCDataAttribute
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

		// Token: 0x0600241B RID: 9243 RVA: 0x000C62AA File Offset: 0x000C44AA
		internal SchemaElementDecl Clone()
		{
			return (SchemaElementDecl)base.MemberwiseClone();
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x0600241C RID: 9244 RVA: 0x000C62B7 File Offset: 0x000C44B7
		// (set) Token: 0x0600241D RID: 9245 RVA: 0x000C62BF File Offset: 0x000C44BF
		internal bool IsAbstract
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

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x000C62C8 File Offset: 0x000C44C8
		// (set) Token: 0x0600241F RID: 9247 RVA: 0x000C62D0 File Offset: 0x000C44D0
		internal bool IsNillable
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

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06002420 RID: 9248 RVA: 0x000C62D9 File Offset: 0x000C44D9
		// (set) Token: 0x06002421 RID: 9249 RVA: 0x000C62E1 File Offset: 0x000C44E1
		internal XmlSchemaDerivationMethod Block
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

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x000C62EA File Offset: 0x000C44EA
		// (set) Token: 0x06002423 RID: 9251 RVA: 0x000C62F2 File Offset: 0x000C44F2
		internal bool IsNotationDeclared
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

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06002424 RID: 9252 RVA: 0x000C62FB File Offset: 0x000C44FB
		internal bool HasDefaultAttribute
		{
			get
			{
				return this.defaultAttdefs != null;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x000C6306 File Offset: 0x000C4506
		// (set) Token: 0x06002426 RID: 9254 RVA: 0x000C630E File Offset: 0x000C450E
		internal bool HasRequiredAttribute
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

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06002427 RID: 9255 RVA: 0x000C6317 File Offset: 0x000C4517
		// (set) Token: 0x06002428 RID: 9256 RVA: 0x000C631F File Offset: 0x000C451F
		internal ContentValidator ContentValidator
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

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06002429 RID: 9257 RVA: 0x000C6328 File Offset: 0x000C4528
		// (set) Token: 0x0600242A RID: 9258 RVA: 0x000C6330 File Offset: 0x000C4530
		internal XmlSchemaAnyAttribute AnyAttribute
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

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x0600242B RID: 9259 RVA: 0x000C6339 File Offset: 0x000C4539
		// (set) Token: 0x0600242C RID: 9260 RVA: 0x000C6341 File Offset: 0x000C4541
		internal CompiledIdentityConstraint[] Constraints
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

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x0600242D RID: 9261 RVA: 0x000C634A File Offset: 0x000C454A
		// (set) Token: 0x0600242E RID: 9262 RVA: 0x000C6352 File Offset: 0x000C4552
		internal XmlSchemaElement SchemaElement
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

		// Token: 0x0600242F RID: 9263 RVA: 0x000C635C File Offset: 0x000C455C
		internal void AddAttDef(SchemaAttDef attdef)
		{
			this.attdefs.Add(attdef.Name, attdef);
			if (attdef.Presence == SchemaDeclBase.Use.Required || attdef.Presence == SchemaDeclBase.Use.RequiredFixed)
			{
				this.hasRequiredAttribute = true;
			}
			if (attdef.Presence == SchemaDeclBase.Use.Default || attdef.Presence == SchemaDeclBase.Use.Fixed)
			{
				if (this.defaultAttdefs == null)
				{
					this.defaultAttdefs = new List<IDtdDefaultAttributeInfo>();
				}
				this.defaultAttdefs.Add(attdef);
			}
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x000C63C4 File Offset: 0x000C45C4
		internal SchemaAttDef GetAttDef(XmlQualifiedName qname)
		{
			SchemaAttDef result;
			if (this.attdefs.TryGetValue(qname, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06002431 RID: 9265 RVA: 0x000C63E4 File Offset: 0x000C45E4
		internal IList<IDtdDefaultAttributeInfo> DefaultAttDefs
		{
			get
			{
				return this.defaultAttdefs;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002432 RID: 9266 RVA: 0x000C63EC File Offset: 0x000C45EC
		internal Dictionary<XmlQualifiedName, SchemaAttDef> AttDefs
		{
			get
			{
				return this.attdefs;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06002433 RID: 9267 RVA: 0x000C63F4 File Offset: 0x000C45F4
		internal Dictionary<XmlQualifiedName, XmlQualifiedName> ProhibitedAttributes
		{
			get
			{
				return this.prohibitedAttributes;
			}
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000C63FC File Offset: 0x000C45FC
		internal void CheckAttributes(Hashtable presence, bool standalone)
		{
			foreach (SchemaAttDef schemaAttDef in this.attdefs.Values)
			{
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

		// Token: 0x04000F15 RID: 3861
		private Dictionary<XmlQualifiedName, SchemaAttDef> attdefs = new Dictionary<XmlQualifiedName, SchemaAttDef>();

		// Token: 0x04000F16 RID: 3862
		private List<IDtdDefaultAttributeInfo> defaultAttdefs;

		// Token: 0x04000F17 RID: 3863
		private bool isIdDeclared;

		// Token: 0x04000F18 RID: 3864
		private bool hasNonCDataAttribute;

		// Token: 0x04000F19 RID: 3865
		private bool isAbstract;

		// Token: 0x04000F1A RID: 3866
		private bool isNillable;

		// Token: 0x04000F1B RID: 3867
		private bool hasRequiredAttribute;

		// Token: 0x04000F1C RID: 3868
		private bool isNotationDeclared;

		// Token: 0x04000F1D RID: 3869
		private Dictionary<XmlQualifiedName, XmlQualifiedName> prohibitedAttributes = new Dictionary<XmlQualifiedName, XmlQualifiedName>();

		// Token: 0x04000F1E RID: 3870
		private ContentValidator contentValidator;

		// Token: 0x04000F1F RID: 3871
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04000F20 RID: 3872
		private XmlSchemaDerivationMethod block;

		// Token: 0x04000F21 RID: 3873
		private CompiledIdentityConstraint[] constraints;

		// Token: 0x04000F22 RID: 3874
		private XmlSchemaElement schemaElement;

		// Token: 0x04000F23 RID: 3875
		internal static readonly SchemaElementDecl Empty = new SchemaElementDecl();
	}
}
