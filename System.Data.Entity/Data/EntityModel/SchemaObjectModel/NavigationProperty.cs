using System;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002F8 RID: 760
	[DebuggerDisplay("Name={Name}, Relationship={_unresolvedRelationshipName}, FromRole={_unresolvedFromEndRole}, ToRole={_unresolvedToEndRole}")]
	internal sealed class NavigationProperty : Property
	{
		// Token: 0x06002D40 RID: 11584 RVA: 0x000AB81D File Offset: 0x000A9A1D
		public NavigationProperty(SchemaEntityType parent) : base(parent)
		{
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06002D41 RID: 11585 RVA: 0x000AB826 File Offset: 0x000A9A26
		public new SchemaEntityType ParentElement
		{
			get
			{
				return base.ParentElement as SchemaEntityType;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06002D42 RID: 11586 RVA: 0x000AB833 File Offset: 0x000A9A33
		internal IRelationship Relationship
		{
			get
			{
				return this._relationship;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002D43 RID: 11587 RVA: 0x000AB83B File Offset: 0x000A9A3B
		internal IRelationshipEnd ToEnd
		{
			get
			{
				return this._toEnd;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002D44 RID: 11588 RVA: 0x000AB843 File Offset: 0x000A9A43
		internal IRelationshipEnd FromEnd
		{
			get
			{
				return this._fromEnd;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002D45 RID: 11589 RVA: 0x000AB84B File Offset: 0x000A9A4B
		public override SchemaType Type
		{
			get
			{
				if (this._toEnd == null || this._toEnd.Type == null)
				{
					return null;
				}
				return this._toEnd.Type;
			}
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x000AB870 File Offset: 0x000A9A70
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Relationship"))
			{
				this.HandleAssociationAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "FromRole"))
			{
				this.HandleFromRoleAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "ToRole"))
			{
				this.HandleToRoleAttribute(reader);
				return true;
			}
			return SchemaElement.CanHandleAttribute(reader, "ContainsTarget");
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x000AB8DC File Offset: 0x000A9ADC
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			SchemaType schemaType;
			if (!base.Schema.ResolveTypeName(this, this._unresolvedRelationshipName, out schemaType))
			{
				return;
			}
			this._relationship = (schemaType as IRelationship);
			if (this._relationship == null)
			{
				base.AddError(ErrorCode.BadNavigationProperty, EdmSchemaErrorSeverity.Error, Strings.BadNavigationPropertyRelationshipNotRelationship(this._unresolvedRelationshipName));
				return;
			}
			bool flag = true;
			if (!this._relationship.TryGetEnd(this._unresolvedFromEndRole, out this._fromEnd))
			{
				base.AddError(ErrorCode.BadNavigationProperty, EdmSchemaErrorSeverity.Error, Strings.BadNavigationPropertyUndefinedRole(this._unresolvedFromEndRole, this._relationship.FQName));
				flag = false;
			}
			if (!this._relationship.TryGetEnd(this._unresolvedToEndRole, out this._toEnd))
			{
				base.AddError(ErrorCode.BadNavigationProperty, EdmSchemaErrorSeverity.Error, Strings.BadNavigationPropertyUndefinedRole(this._unresolvedToEndRole, this._relationship.FQName));
				flag = false;
			}
			if (flag && this._fromEnd == this._toEnd)
			{
				base.AddError(ErrorCode.BadNavigationProperty, EdmSchemaErrorSeverity.Error, Strings.BadNavigationPropertyRolesCannotBeTheSame);
			}
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x000AB9C4 File Offset: 0x000A9BC4
		internal override void Validate()
		{
			base.Validate();
			if (this._fromEnd.Type != this.ParentElement)
			{
				base.AddError(ErrorCode.BadNavigationProperty, EdmSchemaErrorSeverity.Error, Strings.BadNavigationPropertyBadFromRoleType(this.Name, this._fromEnd.Type.FQName, this._fromEnd.Name, this._relationship.FQName, this.ParentElement.FQName));
			}
			StructuredType type = this._toEnd.Type;
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x000ABA3B File Offset: 0x000A9C3B
		private void HandleToRoleAttribute(XmlReader reader)
		{
			this._unresolvedToEndRole = base.HandleUndottedNameAttribute(reader, this._unresolvedToEndRole);
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x000ABA50 File Offset: 0x000A9C50
		private void HandleFromRoleAttribute(XmlReader reader)
		{
			this._unresolvedFromEndRole = base.HandleUndottedNameAttribute(reader, this._unresolvedFromEndRole);
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x000ABA68 File Offset: 0x000A9C68
		private void HandleAssociationAttribute(XmlReader reader)
		{
			string unresolvedRelationshipName;
			if (!Utils.GetDottedName(base.Schema, reader, out unresolvedRelationshipName))
			{
				return;
			}
			this._unresolvedRelationshipName = unresolvedRelationshipName;
		}

		// Token: 0x040013D2 RID: 5074
		private string _unresolvedFromEndRole;

		// Token: 0x040013D3 RID: 5075
		private string _unresolvedToEndRole;

		// Token: 0x040013D4 RID: 5076
		private string _unresolvedRelationshipName;

		// Token: 0x040013D5 RID: 5077
		private IRelationshipEnd _fromEnd;

		// Token: 0x040013D6 RID: 5078
		private IRelationshipEnd _toEnd;

		// Token: 0x040013D7 RID: 5079
		private IRelationship _relationship;
	}
}
