using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000373 RID: 883
	[DebuggerDisplay("Name={Name}, Relationship={_unresolvedRelationshipName}, FromRole={_unresolvedFromEndRole}, ToRole={_unresolvedToEndRole}")]
	internal sealed class NavigationProperty : Property
	{
		// Token: 0x06001FA5 RID: 8101 RVA: 0x00096364 File Offset: 0x00094564
		public NavigationProperty(SchemaEntityType parent) : base(parent)
		{
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001FA6 RID: 8102 RVA: 0x0009636D File Offset: 0x0009456D
		public new SchemaEntityType ParentElement
		{
			get
			{
				return base.ParentElement as SchemaEntityType;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001FA7 RID: 8103 RVA: 0x0009637A File Offset: 0x0009457A
		internal IRelationship Relationship
		{
			get
			{
				return this._relationship;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001FA8 RID: 8104 RVA: 0x00096382 File Offset: 0x00094582
		internal IRelationshipEnd ToEnd
		{
			get
			{
				return this._toEnd;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x0009638A File Offset: 0x0009458A
		internal IRelationshipEnd FromEnd
		{
			get
			{
				return this._fromEnd;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001FAA RID: 8106 RVA: 0x00096392 File Offset: 0x00094592
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

		// Token: 0x06001FAB RID: 8107 RVA: 0x000963B8 File Offset: 0x000945B8
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

		// Token: 0x06001FAC RID: 8108 RVA: 0x00096424 File Offset: 0x00094624
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

		// Token: 0x06001FAD RID: 8109 RVA: 0x0009650C File Offset: 0x0009470C
		internal override void Validate()
		{
			base.Validate();
			if (this._fromEnd.Type != this.ParentElement)
			{
				base.AddError(ErrorCode.BadNavigationProperty, EdmSchemaErrorSeverity.Error, Strings.BadNavigationPropertyBadFromRoleType(this.Name, this._fromEnd.Type.FQName, this._fromEnd.Name, this._relationship.FQName, this.ParentElement.FQName));
			}
			SchemaEntityType type = this._toEnd.Type;
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x00096583 File Offset: 0x00094783
		private void HandleToRoleAttribute(XmlReader reader)
		{
			this._unresolvedToEndRole = base.HandleUndottedNameAttribute(reader, this._unresolvedToEndRole);
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x00096598 File Offset: 0x00094798
		private void HandleFromRoleAttribute(XmlReader reader)
		{
			this._unresolvedFromEndRole = base.HandleUndottedNameAttribute(reader, this._unresolvedFromEndRole);
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x000965B0 File Offset: 0x000947B0
		private void HandleAssociationAttribute(XmlReader reader)
		{
			string unresolvedRelationshipName;
			if (!Utils.GetDottedName(base.Schema, reader, out unresolvedRelationshipName))
			{
				return;
			}
			this._unresolvedRelationshipName = unresolvedRelationshipName;
		}

		// Token: 0x04000B4F RID: 2895
		private string _unresolvedFromEndRole;

		// Token: 0x04000B50 RID: 2896
		private string _unresolvedToEndRole;

		// Token: 0x04000B51 RID: 2897
		private string _unresolvedRelationshipName;

		// Token: 0x04000B52 RID: 2898
		private IRelationshipEnd _fromEnd;

		// Token: 0x04000B53 RID: 2899
		private IRelationshipEnd _toEnd;

		// Token: 0x04000B54 RID: 2900
		private IRelationship _relationship;
	}
}
