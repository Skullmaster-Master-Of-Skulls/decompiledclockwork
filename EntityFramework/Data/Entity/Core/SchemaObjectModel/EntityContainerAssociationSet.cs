using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200035E RID: 862
	internal sealed class EntityContainerAssociationSet : EntityContainerRelationshipSet
	{
		// Token: 0x06001EDB RID: 7899 RVA: 0x000938B1 File Offset: 0x00091AB1
		public EntityContainerAssociationSet(EntityContainer parentElement) : base(parentElement)
		{
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x00093B10 File Offset: 0x00091D10
		internal override IEnumerable<EntityContainerRelationshipSetEnd> Ends
		{
			get
			{
				foreach (EntityContainerAssociationSetEnd end in this._relationshipEnds.Values)
				{
					yield return end;
				}
				foreach (EntityContainerAssociationSetEnd end2 in this._rolelessEnds)
				{
					yield return end2;
				}
				yield break;
			}
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x00093B2D File Offset: 0x00091D2D
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

		// Token: 0x06001EDE RID: 7902 RVA: 0x00093B51 File Offset: 0x00091D51
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

		// Token: 0x06001EDF RID: 7903 RVA: 0x00093B78 File Offset: 0x00091D78
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

		// Token: 0x06001EE0 RID: 7904 RVA: 0x00093BDE File Offset: 0x00091DDE
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x00093BE8 File Offset: 0x00091DE8
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

		// Token: 0x06001EE2 RID: 7906 RVA: 0x00093C90 File Offset: 0x00091E90
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

		// Token: 0x06001EE3 RID: 7907 RVA: 0x00093CD8 File Offset: 0x00091ED8
		protected override bool HasEnd(string role)
		{
			return this._relationshipEnds.ContainsKey(role);
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x00093CE8 File Offset: 0x00091EE8
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

		// Token: 0x04000A83 RID: 2691
		private readonly Dictionary<string, EntityContainerAssociationSetEnd> _relationshipEnds = new Dictionary<string, EntityContainerAssociationSetEnd>();

		// Token: 0x04000A84 RID: 2692
		private readonly List<EntityContainerAssociationSetEnd> _rolelessEnds = new List<EntityContainerAssociationSetEnd>();
	}
}
