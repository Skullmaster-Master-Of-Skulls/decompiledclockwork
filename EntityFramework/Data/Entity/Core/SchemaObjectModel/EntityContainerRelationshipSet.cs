using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200035D RID: 861
	internal abstract class EntityContainerRelationshipSet : SchemaElement
	{
		// Token: 0x06001ECD RID: 7885 RVA: 0x000935CB File Offset: 0x000917CB
		public EntityContainerRelationshipSet(EntityContainer parentElement) : base(parentElement, null)
		{
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001ECE RID: 7886 RVA: 0x000935D5 File Offset: 0x000917D5
		public override string FQName
		{
			get
			{
				return this.ParentElement.Name + "." + this.Name;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001ECF RID: 7887 RVA: 0x000935F2 File Offset: 0x000917F2
		// (set) Token: 0x06001ED0 RID: 7888 RVA: 0x000935FA File Offset: 0x000917FA
		internal IRelationship Relationship
		{
			get
			{
				return this._relationship;
			}
			set
			{
				this._relationship = value;
			}
		}

		// Token: 0x06001ED1 RID: 7889
		protected abstract bool HasEnd(string role);

		// Token: 0x06001ED2 RID: 7890
		protected abstract void AddEnd(IRelationshipEnd relationshipEnd, EntityContainerEntitySet entitySet);

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001ED3 RID: 7891
		internal abstract IEnumerable<EntityContainerRelationshipSetEnd> Ends { get; }

		// Token: 0x06001ED4 RID: 7892 RVA: 0x00093604 File Offset: 0x00091804
		protected void HandleRelationshipTypeNameAttribute(XmlReader reader)
		{
			ReturnValue<string> returnValue = base.HandleDottedNameAttribute(reader, this._unresolvedRelationshipTypeName);
			if (returnValue.Succeeded)
			{
				this._unresolvedRelationshipTypeName = returnValue.Value;
			}
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x00093634 File Offset: 0x00091834
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (this._relationship == null)
			{
				SchemaType schemaType;
				if (!base.Schema.ResolveTypeName(this, this._unresolvedRelationshipTypeName, out schemaType))
				{
					return;
				}
				this._relationship = (schemaType as IRelationship);
				if (this._relationship == null)
				{
					base.AddError(ErrorCode.InvalidPropertyType, EdmSchemaErrorSeverity.Error, Strings.InvalidRelationshipSetType(schemaType.Name));
					return;
				}
			}
			foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd in this.Ends)
			{
				entityContainerRelationshipSetEnd.ResolveTopLevelNames();
			}
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x000936D0 File Offset: 0x000918D0
		internal override void ResolveSecondLevelNames()
		{
			base.ResolveSecondLevelNames();
			foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd in this.Ends)
			{
				entityContainerRelationshipSetEnd.ResolveSecondLevelNames();
			}
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x00093724 File Offset: 0x00091924
		internal override void Validate()
		{
			base.Validate();
			this.InferEnds();
			foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd in this.Ends)
			{
				entityContainerRelationshipSetEnd.Validate();
			}
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x0009377C File Offset: 0x0009197C
		private void InferEnds()
		{
			foreach (IRelationshipEnd relationshipEnd in this.Relationship.Ends)
			{
				if (!this.HasEnd(relationshipEnd.Name))
				{
					EntityContainerEntitySet entityContainerEntitySet = this.InferEntitySet(relationshipEnd);
					if (entityContainerEntitySet != null)
					{
						this.AddEnd(relationshipEnd, entityContainerEntitySet);
					}
				}
			}
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x000937E8 File Offset: 0x000919E8
		private EntityContainerEntitySet InferEntitySet(IRelationshipEnd relationshipEnd)
		{
			List<EntityContainerEntitySet> list = new List<EntityContainerEntitySet>();
			foreach (EntityContainerEntitySet entityContainerEntitySet in this.ParentElement.EntitySets)
			{
				if (relationshipEnd.Type.IsOfType(entityContainerEntitySet.EntityType))
				{
					list.Add(entityContainerEntitySet);
				}
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			if (list.Count == 0)
			{
				base.AddError(ErrorCode.MissingExtentEntityContainerEnd, EdmSchemaErrorSeverity.Error, Strings.MissingEntityContainerEnd(relationshipEnd.Name, this.FQName));
			}
			else
			{
				base.AddError(ErrorCode.AmbiguousEntityContainerEnd, EdmSchemaErrorSeverity.Error, Strings.AmbiguousEntityContainerEnd(relationshipEnd.Name, this.FQName));
			}
			return null;
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001EDA RID: 7898 RVA: 0x000938A4 File Offset: 0x00091AA4
		internal new EntityContainer ParentElement
		{
			get
			{
				return (EntityContainer)base.ParentElement;
			}
		}

		// Token: 0x04000A81 RID: 2689
		private IRelationship _relationship;

		// Token: 0x04000A82 RID: 2690
		private string _unresolvedRelationshipTypeName;
	}
}
