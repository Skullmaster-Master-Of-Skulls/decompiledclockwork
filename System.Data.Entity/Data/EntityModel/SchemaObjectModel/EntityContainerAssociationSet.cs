using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002E4 RID: 740
	internal sealed class EntityContainerAssociationSet : EntityContainerRelationshipSet
	{
		// Token: 0x06002C74 RID: 11380 RVA: 0x000A912E File Offset: 0x000A732E
		public EntityContainerAssociationSet(EntityContainer parentElement) : base(parentElement)
		{
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002C75 RID: 11381 RVA: 0x000A9150 File Offset: 0x000A7350
		internal override IEnumerable<EntityContainerRelationshipSetEnd> Ends
		{
			get
			{
				foreach (EntityContainerAssociationSetEnd entityContainerAssociationSetEnd in this._relationshipEnds.Values)
				{
					yield return entityContainerAssociationSetEnd;
				}
				Dictionary<string, EntityContainerAssociationSetEnd>.ValueCollection.Enumerator enumerator = default(Dictionary<string, EntityContainerAssociationSetEnd>.ValueCollection.Enumerator);
				foreach (EntityContainerAssociationSetEnd entityContainerAssociationSetEnd2 in this._rolelessEnds)
				{
					yield return entityContainerAssociationSetEnd2;
				}
				List<EntityContainerAssociationSetEnd>.Enumerator enumerator2 = default(List<EntityContainerAssociationSetEnd>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x000A916D File Offset: 0x000A736D
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Association"))
			{
				base.HandleRelationshipTypeNameAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x000A9191 File Offset: 0x000A7391
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "End"))
			{
				this.HandleEndElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x000A91B8 File Offset: 0x000A73B8
		private void HandleEndElement(XmlReader reader)
		{
			EntityContainerAssociationSetEnd entityContainerAssociationSetEnd = new EntityContainerAssociationSetEnd(this);
			entityContainerAssociationSetEnd.Parse(reader);
			if (entityContainerAssociationSetEnd.Role == null)
			{
				this._rolelessEnds.Add(entityContainerAssociationSetEnd);
				return;
			}
			if (this.HasEnd(entityContainerAssociationSetEnd.Role))
			{
				entityContainerAssociationSetEnd.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, reader, Strings.DuplicateEndName(entityContainerAssociationSetEnd.Name));
				return;
			}
			this._relationshipEnds.Add(entityContainerAssociationSetEnd.Role, entityContainerAssociationSetEnd);
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x000A921E File Offset: 0x000A741E
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x000A9228 File Offset: 0x000A7428
		internal override void ResolveSecondLevelNames()
		{
			base.ResolveSecondLevelNames();
			foreach (EntityContainerAssociationSetEnd entityContainerAssociationSetEnd in this._rolelessEnds)
			{
				if (entityContainerAssociationSetEnd.Role != null)
				{
					if (this.HasEnd(entityContainerAssociationSetEnd.Role))
					{
						entityContainerAssociationSetEnd.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, Strings.InferRelationshipEndGivesAlreadyDefinedEnd(entityContainerAssociationSetEnd.EntitySet.FQName, this.Name));
					}
					else
					{
						this._relationshipEnds.Add(entityContainerAssociationSetEnd.Role, entityContainerAssociationSetEnd);
					}
				}
			}
			this._rolelessEnds.Clear();
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x000A92D0 File Offset: 0x000A74D0
		protected override void AddEnd(IRelationshipEnd relationshipEnd, EntityContainerEntitySet entitySet)
		{
			EntityContainerAssociationSetEnd entityContainerAssociationSetEnd = new EntityContainerAssociationSetEnd(this);
			entityContainerAssociationSetEnd.Role = relationshipEnd.Name;
			entityContainerAssociationSetEnd.RelationshipEnd = relationshipEnd;
			entityContainerAssociationSetEnd.EntitySet = entitySet;
			if (entityContainerAssociationSetEnd.EntitySet != null)
			{
				this._relationshipEnds.Add(entityContainerAssociationSetEnd.Role, entityContainerAssociationSetEnd);
			}
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x000A9318 File Offset: 0x000A7518
		protected override bool HasEnd(string role)
		{
			return this._relationshipEnds.ContainsKey(role);
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x000A9328 File Offset: 0x000A7528
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			EntityContainerAssociationSet entityContainerAssociationSet = new EntityContainerAssociationSet((EntityContainer)parentElement);
			entityContainerAssociationSet.Name = this.Name;
			entityContainerAssociationSet.Relationship = base.Relationship;
			foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd in this.Ends)
			{
				EntityContainerAssociationSetEnd entityContainerAssociationSetEnd = (EntityContainerAssociationSetEnd)entityContainerRelationshipSetEnd;
				EntityContainerAssociationSetEnd entityContainerAssociationSetEnd2 = (EntityContainerAssociationSetEnd)entityContainerAssociationSetEnd.Clone(entityContainerAssociationSet);
				entityContainerAssociationSet._relationshipEnds.Add(entityContainerAssociationSetEnd2.Role, entityContainerAssociationSetEnd2);
			}
			return entityContainerAssociationSet;
		}

		// Token: 0x0400130A RID: 4874
		private Dictionary<string, EntityContainerAssociationSetEnd> _relationshipEnds = new Dictionary<string, EntityContainerAssociationSetEnd>();

		// Token: 0x0400130B RID: 4875
		private List<EntityContainerAssociationSetEnd> _rolelessEnds = new List<EntityContainerAssociationSetEnd>();
	}
}
