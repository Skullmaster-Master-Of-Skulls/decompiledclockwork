using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200035F RID: 863
	internal class EntityContainerRelationshipSetEnd : SchemaElement
	{
		// Token: 0x06001EE5 RID: 7909 RVA: 0x00093D78 File Offset: 0x00091F78
		public EntityContainerRelationshipSetEnd(EntityContainerRelationshipSet parentElement) : base(parentElement, null)
		{
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x00093D82 File Offset: 0x00091F82
		// (set) Token: 0x06001EE7 RID: 7911 RVA: 0x00093D8A File Offset: 0x00091F8A
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

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x00093D93 File Offset: 0x00091F93
		// (set) Token: 0x06001EE9 RID: 7913 RVA: 0x00093D9B File Offset: 0x00091F9B
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

		// Token: 0x06001EEA RID: 7914 RVA: 0x00093DA4 File Offset: 0x00091FA4
		protected override bool ProhibitAttribute(string namespaceUri, string localName)
		{
			return base.ProhibitAttribute(namespaceUri, localName) || (namespaceUri == null && localName == "Name" && false);
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x00093DC5 File Offset: 0x00091FC5
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

		// Token: 0x06001EEC RID: 7916 RVA: 0x00093DE9 File Offset: 0x00091FE9
		private void HandleEntitySetAttribute(XmlReader reader)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
			{
				this._unresolvedEntitySetName = reader.Value;
				return;
			}
			this._unresolvedEntitySetName = base.HandleUndottedNameAttribute(reader, this._unresolvedEntitySetName);
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x00093E1C File Offset: 0x0009201C
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

		// Token: 0x06001EEE RID: 7918 RVA: 0x00093E78 File Offset: 0x00092078
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

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x00093EEF File Offset: 0x000920EF
		internal new EntityContainerRelationshipSet ParentElement
		{
			get
			{
				return (EntityContainerRelationshipSet)base.ParentElement;
			}
		}

		// Token: 0x04000A85 RID: 2693
		private IRelationshipEnd _relationshipEnd;

		// Token: 0x04000A86 RID: 2694
		private string _unresolvedEntitySetName;

		// Token: 0x04000A87 RID: 2695
		private EntityContainerEntitySet _entitySet;
	}
}
