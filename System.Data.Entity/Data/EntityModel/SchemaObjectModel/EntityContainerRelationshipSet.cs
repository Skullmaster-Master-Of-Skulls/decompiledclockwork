using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002E8 RID: 744
	internal abstract class EntityContainerRelationshipSet : SchemaElement
	{
		// Token: 0x06002C9C RID: 11420 RVA: 0x000A9632 File Offset: 0x000A7832
		public EntityContainerRelationshipSet(EntityContainer parentElement) : base(parentElement)
		{
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06002C9D RID: 11421 RVA: 0x000A9987 File Offset: 0x000A7B87
		public override string FQName
		{
			get
			{
				return this.ParentElement.Name + "." + this.Name;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002C9E RID: 11422 RVA: 0x000A99A4 File Offset: 0x000A7BA4
		// (set) Token: 0x06002C9F RID: 11423 RVA: 0x000A99AC File Offset: 0x000A7BAC
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

		// Token: 0x06002CA0 RID: 11424
		protected abstract bool HasEnd(string role);

		// Token: 0x06002CA1 RID: 11425
		protected abstract void AddEnd(IRelationshipEnd relationshipEnd, EntityContainerEntitySet entitySet);

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06002CA2 RID: 11426
		internal abstract IEnumerable<EntityContainerRelationshipSetEnd> Ends { get; }

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000A99B8 File Offset: 0x000A7BB8
		protected void HandleRelationshipTypeNameAttribute(XmlReader reader)
		{
			ReturnValue<string> returnValue = base.HandleDottedNameAttribute(reader, this._unresolvedRelationshipTypeName, new Func<object, string>(Strings.PropertyTypeAlreadyDefined));
			if (returnValue.Succeeded)
			{
				this._unresolvedRelationshipTypeName = returnValue.Value;
			}
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000A99F4 File Offset: 0x000A7BF4
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

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000A9A90 File Offset: 0x000A7C90
		internal override void ResolveSecondLevelNames()
		{
			base.ResolveSecondLevelNames();
			foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd in this.Ends)
			{
				entityContainerRelationshipSetEnd.ResolveSecondLevelNames();
			}
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000A9AE4 File Offset: 0x000A7CE4
		internal override void Validate()
		{
			base.Validate();
			this.InferEnds();
			foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd in this.Ends)
			{
				entityContainerRelationshipSetEnd.Validate();
			}
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000A9B3C File Offset: 0x000A7D3C
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

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000A9BA8 File Offset: 0x000A7DA8
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

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06002CA9 RID: 11433 RVA: 0x000A9C64 File Offset: 0x000A7E64
		internal new EntityContainer ParentElement
		{
			get
			{
				return (EntityContainer)base.ParentElement;
			}
		}

		// Token: 0x04001313 RID: 4883
		private IRelationship _relationship;

		// Token: 0x04001314 RID: 4884
		private string _unresolvedRelationshipTypeName;
	}
}
