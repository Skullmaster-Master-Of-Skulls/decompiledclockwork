using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002E5 RID: 741
	internal sealed class EntityContainerAssociationSetEnd : EntityContainerRelationshipSetEnd
	{
		// Token: 0x06002C7E RID: 11390 RVA: 0x000A93B8 File Offset: 0x000A75B8
		public EntityContainerAssociationSetEnd(EntityContainerAssociationSet parentElement) : base(parentElement)
		{
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002C7F RID: 11391 RVA: 0x000A93C1 File Offset: 0x000A75C1
		// (set) Token: 0x06002C80 RID: 11392 RVA: 0x000A93C9 File Offset: 0x000A75C9
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

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002C81 RID: 11393 RVA: 0x000A93D2 File Offset: 0x000A75D2
		public override string Name
		{
			get
			{
				return this.Role;
			}
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x000A93DA File Offset: 0x000A75DA
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

		// Token: 0x06002C83 RID: 11395 RVA: 0x000A93FE File Offset: 0x000A75FE
		private void HandleRoleAttribute(XmlReader reader)
		{
			this._unresolvedRelationshipEndRole = base.HandleUndottedNameAttribute(reader, this._unresolvedRelationshipEndRole);
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x000A9414 File Offset: 0x000A7614
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			IRelationship relationship = base.ParentElement.Relationship;
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000A9438 File Offset: 0x000A7638
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

		// Token: 0x06002C86 RID: 11398 RVA: 0x000A94D0 File Offset: 0x000A76D0
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

		// Token: 0x06002C87 RID: 11399 RVA: 0x000A9600 File Offset: 0x000A7800
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			return new EntityContainerAssociationSetEnd((EntityContainerAssociationSet)parentElement)
			{
				_unresolvedRelationshipEndRole = this._unresolvedRelationshipEndRole,
				EntitySet = base.EntitySet
			};
		}

		// Token: 0x0400130C RID: 4876
		private string _unresolvedRelationshipEndRole;
	}
}
