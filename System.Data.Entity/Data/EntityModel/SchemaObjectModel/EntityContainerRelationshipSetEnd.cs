using System;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002E9 RID: 745
	internal class EntityContainerRelationshipSetEnd : SchemaElement
	{
		// Token: 0x06002CAA RID: 11434 RVA: 0x000A9632 File Offset: 0x000A7832
		public EntityContainerRelationshipSetEnd(EntityContainerRelationshipSet parentElement) : base(parentElement)
		{
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002CAB RID: 11435 RVA: 0x000A9C71 File Offset: 0x000A7E71
		// (set) Token: 0x06002CAC RID: 11436 RVA: 0x000A9C79 File Offset: 0x000A7E79
		public IRelationshipEnd RelationshipEnd
		{
			get
			{
				return this._relationshipEnd;
			}
			internal set
			{
				this._relationshipEnd = value;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x000A9C82 File Offset: 0x000A7E82
		// (set) Token: 0x06002CAE RID: 11438 RVA: 0x000A9C8A File Offset: 0x000A7E8A
		public EntityContainerEntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
			internal set
			{
				this._entitySet = value;
			}
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x000A9C93 File Offset: 0x000A7E93
		protected override bool ProhibitAttribute(string namespaceUri, string localName)
		{
			if (base.ProhibitAttribute(namespaceUri, localName))
			{
				return true;
			}
			if (namespaceUri == null)
			{
				localName == "Name";
				return false;
			}
			return false;
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x000A9CB3 File Offset: 0x000A7EB3
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "EntitySet"))
			{
				this.HandleEntitySetAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x000A9CD7 File Offset: 0x000A7ED7
		private void HandleEntitySetAttribute(XmlReader reader)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
			{
				this._unresolvedEntitySetName = reader.Value;
				return;
			}
			this._unresolvedEntitySetName = base.HandleUndottedNameAttribute(reader, this._unresolvedEntitySetName);
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000A9D08 File Offset: 0x000A7F08
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (this._entitySet == null)
			{
				this._entitySet = this.ParentElement.ParentElement.FindEntitySet(this._unresolvedEntitySetName);
				if (this._entitySet == null)
				{
					base.AddError(ErrorCode.InvalidEndEntitySet, EdmSchemaErrorSeverity.Error, Strings.InvalidEntitySetNameReference(this._unresolvedEntitySetName, this.Name));
				}
			}
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000A9D64 File Offset: 0x000A7F64
		internal override void Validate()
		{
			base.Validate();
			if (this._relationshipEnd == null || this._entitySet == null)
			{
				return;
			}
			if (!this._relationshipEnd.Type.IsOfType(this._entitySet.EntityType) && !this._entitySet.EntityType.IsOfType(this._relationshipEnd.Type))
			{
				base.AddError(ErrorCode.InvalidEndEntitySet, EdmSchemaErrorSeverity.Error, Strings.InvalidEndEntitySetTypeMismatch(this._relationshipEnd.Name));
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x000A9DDB File Offset: 0x000A7FDB
		internal new EntityContainerRelationshipSet ParentElement
		{
			get
			{
				return (EntityContainerRelationshipSet)base.ParentElement;
			}
		}

		// Token: 0x04001315 RID: 4885
		private IRelationshipEnd _relationshipEnd;

		// Token: 0x04001316 RID: 4886
		private string _unresolvedEntitySetName;

		// Token: 0x04001317 RID: 4887
		private EntityContainerEntitySet _entitySet;
	}
}
