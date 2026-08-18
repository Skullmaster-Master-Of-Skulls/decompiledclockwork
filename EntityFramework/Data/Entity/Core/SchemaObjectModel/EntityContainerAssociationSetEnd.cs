using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000360 RID: 864
	internal sealed class EntityContainerAssociationSetEnd : EntityContainerRelationshipSetEnd
	{
		// Token: 0x06001EF0 RID: 7920 RVA: 0x00093EFC File Offset: 0x000920FC
		public EntityContainerAssociationSetEnd(EntityContainerAssociationSet parentElement) : base(parentElement)
		{
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x00093F05 File Offset: 0x00092105
		// (set) Token: 0x06001EF2 RID: 7922 RVA: 0x00093F0D File Offset: 0x0009210D
		public string Role
		{
			get
			{
				return this._unresolvedRelationshipEndRole;
			}
			set
			{
				this._unresolvedRelationshipEndRole = value;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001EF3 RID: 7923 RVA: 0x00093F16 File Offset: 0x00092116
		public override string Name
		{
			get
			{
				return this.Role;
			}
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x00093F1E File Offset: 0x0009211E
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Role"))
			{
				this.HandleRoleAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x00093F42 File Offset: 0x00092142
		private void HandleRoleAttribute(XmlReader reader)
		{
			this._unresolvedRelationshipEndRole = base.HandleUndottedNameAttribute(reader, this._unresolvedRelationshipEndRole);
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x00093F58 File Offset: 0x00092158
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			IRelationship relationship = base.ParentElement.Relationship;
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x00093F7C File Offset: 0x0009217C
		internal override void ResolveSecondLevelNames()
		{
			base.ResolveSecondLevelNames();
			if (this._unresolvedRelationshipEndRole == null && base.EntitySet != null)
			{
				base.RelationshipEnd = this.InferRelationshipEnd(base.EntitySet);
				if (base.RelationshipEnd != null)
				{
					this._unresolvedRelationshipEndRole = base.RelationshipEnd.Name;
					return;
				}
			}
			else if (this._unresolvedRelationshipEndRole != null)
			{
				IRelationship relationship = base.ParentElement.Relationship;
				IRelationshipEnd relationshipEnd;
				if (relationship.TryGetEnd(this._unresolvedRelationshipEndRole, out relationshipEnd))
				{
					base.RelationshipEnd = relationshipEnd;
					return;
				}
				base.AddError(ErrorCode.InvalidContainerTypeForEnd, EdmSchemaErrorSeverity.Error, Strings.InvalidEntityEndName(this.Role, relationship.FQName));
			}
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x00094014 File Offset: 0x00092214
		private IRelationshipEnd InferRelationshipEnd(EntityContainerEntitySet set)
		{
			if (base.ParentElement.Relationship == null)
			{
				return null;
			}
			List<IRelationshipEnd> list = new List<IRelationshipEnd>();
			foreach (IRelationshipEnd relationshipEnd in base.ParentElement.Relationship.Ends)
			{
				if (set.EntityType.IsOfType(relationshipEnd.Type))
				{
					list.Add(relationshipEnd);
				}
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			if (list.Count == 0)
			{
				base.AddError(ErrorCode.FailedInference, EdmSchemaErrorSeverity.Error, Strings.InferRelationshipEndFailedNoEntitySetMatch(set.Name, base.ParentElement.Name, base.ParentElement.Relationship.FQName, set.EntityType.FQName, base.ParentElement.ParentElement.FQName));
			}
			else
			{
				base.AddError(ErrorCode.FailedInference, EdmSchemaErrorSeverity.Error, Strings.InferRelationshipEndAmbiguous(set.Name, base.ParentElement.Name, base.ParentElement.Relationship.FQName, set.EntityType.FQName, base.ParentElement.ParentElement.FQName));
			}
			return null;
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x00094144 File Offset: 0x00092344
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			return new EntityContainerAssociationSetEnd((EntityContainerAssociationSet)parentElement)
			{
				_unresolvedRelationshipEndRole = this._unresolvedRelationshipEndRole,
				EntitySet = base.EntitySet
			};
		}

		// Token: 0x04000A88 RID: 2696
		private string _unresolvedRelationshipEndRole;
	}
}
