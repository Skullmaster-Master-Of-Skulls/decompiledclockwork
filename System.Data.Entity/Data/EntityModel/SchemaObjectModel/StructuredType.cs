using System;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000317 RID: 791
	internal abstract class StructuredType : SchemaType
	{
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002EC1 RID: 11969 RVA: 0x000B0A9B File Offset: 0x000AEC9B
		// (set) Token: 0x06002EC2 RID: 11970 RVA: 0x000B0AA3 File Offset: 0x000AECA3
		public StructuredType BaseType
		{
			get
			{
				return this._baseType;
			}
			private set
			{
				this._baseType = value;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06002EC3 RID: 11971 RVA: 0x000B0AAC File Offset: 0x000AECAC
		public ISchemaElementLookUpTable<StructuredProperty> Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = new FilteredSchemaElementLookUpTable<StructuredProperty, SchemaElement>(this.NamedMembers);
				}
				return this._properties;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06002EC4 RID: 11972 RVA: 0x000B0ACD File Offset: 0x000AECCD
		protected SchemaElementLookUpTable<SchemaElement> NamedMembers
		{
			get
			{
				if (this._namedMembers == null)
				{
					this._namedMembers = new SchemaElementLookUpTable<SchemaElement>();
				}
				return this._namedMembers;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06002EC5 RID: 11973 RVA: 0x000B0AE8 File Offset: 0x000AECE8
		public virtual bool IsTypeHierarchyRoot
		{
			get
			{
				return this.BaseType == null;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06002EC6 RID: 11974 RVA: 0x000B0AF3 File Offset: 0x000AECF3
		public bool IsAbstract
		{
			get
			{
				return this._isAbstract;
			}
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000B0AFC File Offset: 0x000AECFC
		public StructuredProperty FindProperty(string name)
		{
			StructuredProperty structuredProperty = this.Properties.LookUpEquivalentKey(name);
			if (structuredProperty != null)
			{
				return structuredProperty;
			}
			if (this.IsTypeHierarchyRoot)
			{
				return null;
			}
			return this.BaseType.FindProperty(name);
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x000B0B34 File Offset: 0x000AED34
		public bool IsOfType(StructuredType baseType)
		{
			StructuredType structuredType = this;
			while (structuredType != null && structuredType != baseType)
			{
				structuredType = structuredType.BaseType;
			}
			return structuredType == baseType;
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000B0B58 File Offset: 0x000AED58
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			this.TryResolveBaseType();
			foreach (SchemaElement schemaElement in this.NamedMembers)
			{
				schemaElement.ResolveTopLevelNames();
			}
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x000B0BB4 File Offset: 0x000AEDB4
		internal override void Validate()
		{
			base.Validate();
			foreach (SchemaElement schemaElement in this.NamedMembers)
			{
				if (this.BaseType != null)
				{
					string text = null;
					StructuredType structuredType;
					SchemaElement schemaElement2;
					if (StructuredType.HowDefined.AsMember == this.BaseType.DefinesMemberName(schemaElement.Name, out structuredType, out schemaElement2))
					{
						text = Strings.DuplicateMemberName(schemaElement.Name, this.FQName, structuredType.FQName);
					}
					if (text != null)
					{
						schemaElement.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, text);
					}
				}
				schemaElement.Validate();
			}
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x000B0C54 File Offset: 0x000AEE54
		protected StructuredType(Schema parentElement) : base(parentElement)
		{
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x000B0C60 File Offset: 0x000AEE60
		protected void AddMember(SchemaElement newMember)
		{
			if (string.IsNullOrEmpty(newMember.Name))
			{
				return;
			}
			if (base.Schema.DataModel != SchemaDataModelOption.ProviderDataModel && Utils.CompareNames(newMember.Name, this.Name) == 0)
			{
				newMember.AddError(ErrorCode.BadProperty, EdmSchemaErrorSeverity.Error, Strings.InvalidMemberNameMatchesTypeName(newMember.Name, this.FQName));
			}
			this.NamedMembers.Add(newMember, true, new Func<object, string>(Strings.PropertyNameAlreadyDefinedDuplicate));
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x000B0CD0 File Offset: 0x000AEED0
		private StructuredType.HowDefined DefinesMemberName(string name, out StructuredType definingType, out SchemaElement definingMember)
		{
			if (this.NamedMembers.ContainsKey(name))
			{
				definingType = this;
				definingMember = this.NamedMembers[name];
				return StructuredType.HowDefined.AsMember;
			}
			definingMember = this.NamedMembers.LookUpEquivalentKey(name);
			if (this.IsTypeHierarchyRoot)
			{
				definingType = null;
				definingMember = null;
				return StructuredType.HowDefined.NotDefined;
			}
			return this.BaseType.DefinesMemberName(name, out definingType, out definingMember);
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06002ECE RID: 11982 RVA: 0x000B0D2A File Offset: 0x000AEF2A
		// (set) Token: 0x06002ECF RID: 11983 RVA: 0x000B0D32 File Offset: 0x000AEF32
		protected string UnresolvedBaseType
		{
			get
			{
				return this._unresolvedBaseType;
			}
			set
			{
				this._unresolvedBaseType = value;
			}
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x000B0D3B File Offset: 0x000AEF3B
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "Property"))
			{
				this.HandlePropertyElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x000B0D60 File Offset: 0x000AEF60
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "BaseType"))
			{
				this.HandleBaseTypeAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Abstract"))
			{
				this.HandleAbstractAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x000B0D9C File Offset: 0x000AEF9C
		private bool TryResolveBaseType()
		{
			if (this._baseTypeResolveResult != null)
			{
				return this._baseTypeResolveResult.Value;
			}
			if (this.BaseType != null)
			{
				this._baseTypeResolveResult = new bool?(true);
				return this._baseTypeResolveResult.Value;
			}
			if (this.UnresolvedBaseType == null)
			{
				this._baseTypeResolveResult = new bool?(true);
				return this._baseTypeResolveResult.Value;
			}
			SchemaType schemaType;
			if (!base.Schema.ResolveTypeName(this, this.UnresolvedBaseType, out schemaType))
			{
				this._baseTypeResolveResult = new bool?(false);
				return this._baseTypeResolveResult.Value;
			}
			this.BaseType = (schemaType as StructuredType);
			if (this.BaseType == null)
			{
				base.AddError(ErrorCode.InvalidBaseType, EdmSchemaErrorSeverity.Error, Strings.InvalidBaseTypeForStructuredType(this.UnresolvedBaseType, this.FQName));
				this._baseTypeResolveResult = new bool?(false);
				return this._baseTypeResolveResult.Value;
			}
			if (this.CheckForInheritanceCycle())
			{
				this.BaseType = null;
				base.AddError(ErrorCode.CycleInTypeHierarchy, EdmSchemaErrorSeverity.Error, Strings.CycleInTypeHierarchy(this.FQName));
				this._baseTypeResolveResult = new bool?(false);
				return this._baseTypeResolveResult.Value;
			}
			this._baseTypeResolveResult = new bool?(true);
			return true;
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x000B0EC0 File Offset: 0x000AF0C0
		private void HandleBaseTypeAttribute(XmlReader reader)
		{
			string unresolvedBaseType;
			if (!Utils.GetDottedName(base.Schema, reader, out unresolvedBaseType))
			{
				return;
			}
			this.UnresolvedBaseType = unresolvedBaseType;
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x000B0EE5 File Offset: 0x000AF0E5
		private void HandleAbstractAttribute(XmlReader reader)
		{
			base.HandleBoolAttribute(reader, ref this._isAbstract);
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000B0EF8 File Offset: 0x000AF0F8
		private void HandlePropertyElement(XmlReader reader)
		{
			StructuredProperty structuredProperty = new StructuredProperty(this);
			structuredProperty.Parse(reader);
			this.AddMember(structuredProperty);
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000B0F1C File Offset: 0x000AF11C
		private bool CheckForInheritanceCycle()
		{
			StructuredType baseType = this.BaseType;
			StructuredType structuredType = baseType;
			StructuredType structuredType2 = baseType;
			for (;;)
			{
				structuredType2 = structuredType2.BaseType;
				if (structuredType == structuredType2)
				{
					break;
				}
				if (structuredType == null)
				{
					return false;
				}
				structuredType = structuredType.BaseType;
				if (structuredType2 != null)
				{
					structuredType2 = structuredType2.BaseType;
				}
				if (structuredType2 == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400143C RID: 5180
		private bool? _baseTypeResolveResult;

		// Token: 0x0400143D RID: 5181
		private string _unresolvedBaseType;

		// Token: 0x0400143E RID: 5182
		private StructuredType _baseType;

		// Token: 0x0400143F RID: 5183
		private bool _isAbstract;

		// Token: 0x04001440 RID: 5184
		private SchemaElementLookUpTable<SchemaElement> _namedMembers;

		// Token: 0x04001441 RID: 5185
		private ISchemaElementLookUpTable<StructuredProperty> _properties;

		// Token: 0x04001442 RID: 5186
		private static readonly char[] NameSeparators = new char[]
		{
			'.'
		};

		// Token: 0x02000642 RID: 1602
		private enum HowDefined
		{
			// Token: 0x04001ED5 RID: 7893
			NotDefined,
			// Token: 0x04001ED6 RID: 7894
			AsMember
		}
	}
}
