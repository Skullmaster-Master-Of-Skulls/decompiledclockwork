using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200036D RID: 877
	internal abstract class StructuredType : SchemaType
	{
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001F71 RID: 8049 RVA: 0x0009599E File Offset: 0x00093B9E
		// (set) Token: 0x06001F72 RID: 8050 RVA: 0x000959A6 File Offset: 0x00093BA6
		public StructuredType BaseType { get; private set; }

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001F73 RID: 8051 RVA: 0x000959AF File Offset: 0x00093BAF
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

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x000959D0 File Offset: 0x00093BD0
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

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001F75 RID: 8053 RVA: 0x000959EB File Offset: 0x00093BEB
		public virtual bool IsTypeHierarchyRoot
		{
			get
			{
				return this.BaseType == null;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001F76 RID: 8054 RVA: 0x000959F6 File Offset: 0x00093BF6
		public bool IsAbstract
		{
			get
			{
				return this._isAbstract;
			}
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x00095A00 File Offset: 0x00093C00
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

		// Token: 0x06001F78 RID: 8056 RVA: 0x00095A38 File Offset: 0x00093C38
		public bool IsOfType(StructuredType baseType)
		{
			StructuredType structuredType = this;
			while (structuredType != null && structuredType != baseType)
			{
				structuredType = structuredType.BaseType;
			}
			return structuredType == baseType;
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x00095A5C File Offset: 0x00093C5C
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			this.TryResolveBaseType();
			foreach (SchemaElement schemaElement in this.NamedMembers)
			{
				schemaElement.ResolveTopLevelNames();
			}
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x00095AB8 File Offset: 0x00093CB8
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

		// Token: 0x06001F7B RID: 8059 RVA: 0x00095B58 File Offset: 0x00093D58
		protected StructuredType(Schema parentElement) : base(parentElement)
		{
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x00095B64 File Offset: 0x00093D64
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

		// Token: 0x06001F7D RID: 8061 RVA: 0x00095BD4 File Offset: 0x00093DD4
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

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x00095C2E File Offset: 0x00093E2E
		// (set) Token: 0x06001F7F RID: 8063 RVA: 0x00095C36 File Offset: 0x00093E36
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

		// Token: 0x06001F80 RID: 8064 RVA: 0x00095C3F File Offset: 0x00093E3F
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

		// Token: 0x06001F81 RID: 8065 RVA: 0x00095C64 File Offset: 0x00093E64
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

		// Token: 0x06001F82 RID: 8066 RVA: 0x00095CA0 File Offset: 0x00093EA0
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

		// Token: 0x06001F83 RID: 8067 RVA: 0x00095DC4 File Offset: 0x00093FC4
		private void HandleBaseTypeAttribute(XmlReader reader)
		{
			string unresolvedBaseType;
			if (!Utils.GetDottedName(base.Schema, reader, out unresolvedBaseType))
			{
				return;
			}
			this.UnresolvedBaseType = unresolvedBaseType;
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x00095DE9 File Offset: 0x00093FE9
		private void HandleAbstractAttribute(XmlReader reader)
		{
			base.HandleBoolAttribute(reader, ref this._isAbstract);
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x00095DFC File Offset: 0x00093FFC
		private void HandlePropertyElement(XmlReader reader)
		{
			StructuredProperty structuredProperty = new StructuredProperty(this);
			structuredProperty.Parse(reader);
			this.AddMember(structuredProperty);
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x00095E20 File Offset: 0x00094020
		private bool CheckForInheritanceCycle()
		{
			StructuredType baseType = this.BaseType;
			StructuredType structuredType = baseType;
			StructuredType structuredType2 = baseType;
			for (;;)
			{
				structuredType2 = structuredType2.BaseType;
				if (object.ReferenceEquals(structuredType, structuredType2))
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

		// Token: 0x04000B40 RID: 2880
		private bool? _baseTypeResolveResult;

		// Token: 0x04000B41 RID: 2881
		private string _unresolvedBaseType;

		// Token: 0x04000B42 RID: 2882
		private bool _isAbstract;

		// Token: 0x04000B43 RID: 2883
		private SchemaElementLookUpTable<SchemaElement> _namedMembers;

		// Token: 0x04000B44 RID: 2884
		private ISchemaElementLookUpTable<StructuredProperty> _properties;

		// Token: 0x0200036E RID: 878
		private enum HowDefined
		{
			// Token: 0x04000B47 RID: 2887
			NotDefined,
			// Token: 0x04000B48 RID: 2888
			AsMember
		}
	}
}
