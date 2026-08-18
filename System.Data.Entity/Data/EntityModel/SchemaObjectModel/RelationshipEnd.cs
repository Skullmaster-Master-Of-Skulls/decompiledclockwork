using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000303 RID: 771
	internal sealed class RelationshipEnd : SchemaElement, IRelationshipEnd
	{
		// Token: 0x06002D9F RID: 11679 RVA: 0x000A9632 File Offset: 0x000A7832
		public RelationshipEnd(Relationship relationship) : base(relationship)
		{
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002DA0 RID: 11680 RVA: 0x000ACF2D File Offset: 0x000AB12D
		// (set) Token: 0x06002DA1 RID: 11681 RVA: 0x000ACF35 File Offset: 0x000AB135
		public SchemaEntityType Type
		{
			get
			{
				return this._type;
			}
			private set
			{
				this._type = value;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002DA2 RID: 11682 RVA: 0x000ACF3E File Offset: 0x000AB13E
		// (set) Token: 0x06002DA3 RID: 11683 RVA: 0x000ACF46 File Offset: 0x000AB146
		public RelationshipMultiplicity? Multiplicity
		{
			get
			{
				return this._multiplicity;
			}
			set
			{
				this._multiplicity = value;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002DA4 RID: 11684 RVA: 0x000ACF4F File Offset: 0x000AB14F
		public ICollection<OnOperation> Operations
		{
			get
			{
				if (this._operations == null)
				{
					this._operations = new List<OnOperation>();
				}
				return this._operations;
			}
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000ACF6C File Offset: 0x000AB16C
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (this.Type == null && this._unresolvedType != null)
			{
				SchemaType schemaType;
				if (!base.Schema.ResolveTypeName(this, this._unresolvedType, out schemaType))
				{
					return;
				}
				this.Type = (schemaType as SchemaEntityType);
				if (this.Type == null)
				{
					base.AddError(ErrorCode.InvalidRelationshipEndType, EdmSchemaErrorSeverity.Error, Strings.InvalidRelationshipEndType(this.ParentElement.Name, schemaType.FQName));
				}
			}
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x000ACFDC File Offset: 0x000AB1DC
		internal override void Validate()
		{
			base.Validate();
			RelationshipMultiplicity? multiplicity = this.Multiplicity;
			RelationshipMultiplicity relationshipMultiplicity = RelationshipMultiplicity.Many;
			if ((multiplicity.GetValueOrDefault() == relationshipMultiplicity & multiplicity != null) && this.Operations.Count != 0)
			{
				base.AddError(ErrorCode.EndWithManyMultiplicityCannotHaveOperationsSpecified, EdmSchemaErrorSeverity.Error, Strings.EndWithManyMultiplicityCannotHaveOperationsSpecified(this.Name, this.ParentElement.FQName));
			}
			if (this.ParentElement.Constraints.Count == 0 && this.Multiplicity == null)
			{
				base.AddError(ErrorCode.EndWithoutMultiplicity, EdmSchemaErrorSeverity.Error, Strings.EndWithoutMultiplicity(this.Name, this.ParentElement.FQName));
			}
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x000AD07F File Offset: 0x000AB27F
		protected override void HandleAttributesComplete()
		{
			if (this.Name == null && this._unresolvedType != null)
			{
				this.Name = Utils.ExtractTypeName(base.Schema.DataModel, this._unresolvedType);
			}
			base.HandleAttributesComplete();
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000A9C93 File Offset: 0x000A7E93
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

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000AD0B4 File Offset: 0x000AB2B4
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Multiplicity"))
			{
				this.HandleMultiplicityAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Role"))
			{
				this.HandleNameAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Type"))
			{
				this.HandleTypeAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000AD10F File Offset: 0x000AB30F
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "OnDelete"))
			{
				this.HandleOnDeleteElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000AD134 File Offset: 0x000AB334
		private void HandleTypeAttribute(XmlReader reader)
		{
			string unresolvedType;
			if (!Utils.GetDottedName(base.Schema, reader, out unresolvedType))
			{
				return;
			}
			this._unresolvedType = unresolvedType;
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x000AD15C File Offset: 0x000AB35C
		private void HandleMultiplicityAttribute(XmlReader reader)
		{
			RelationshipMultiplicity value;
			if (!RelationshipEnd.TryParseMultiplicity(reader.Value, out value))
			{
				base.AddError(ErrorCode.InvalidMultiplicity, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidRelationshipEndMultiplicity(this.ParentElement.Name, reader.Value));
			}
			this._multiplicity = new RelationshipMultiplicity?(value);
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x000AD1A4 File Offset: 0x000AB3A4
		private void HandleOnDeleteElement(XmlReader reader)
		{
			this.HandleOnOperationElement(reader, Operation.Delete);
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x000AD1B0 File Offset: 0x000AB3B0
		private void HandleOnOperationElement(XmlReader reader, Operation operation)
		{
			foreach (OnOperation onOperation in this.Operations)
			{
				if (onOperation.Operation == operation)
				{
					base.AddError(ErrorCode.InvalidOperation, EdmSchemaErrorSeverity.Error, reader, Strings.DuplicationOperation(reader.Name));
				}
			}
			OnOperation onOperation2 = new OnOperation(this, operation);
			onOperation2.Parse(reader);
			this._operations.Add(onOperation2);
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002DAF RID: 11695 RVA: 0x000ACA69 File Offset: 0x000AAC69
		internal new IRelationship ParentElement
		{
			get
			{
				return (IRelationship)base.ParentElement;
			}
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x000AD230 File Offset: 0x000AB430
		private static bool TryParseMultiplicity(string value, out RelationshipMultiplicity multiplicity)
		{
			if (value == "0..1")
			{
				multiplicity = RelationshipMultiplicity.ZeroOrOne;
				return true;
			}
			if (value == "1")
			{
				multiplicity = RelationshipMultiplicity.One;
				return true;
			}
			if (!(value == "*"))
			{
				multiplicity = (RelationshipMultiplicity)(-1);
				return false;
			}
			multiplicity = RelationshipMultiplicity.Many;
			return true;
		}

		// Token: 0x040013EB RID: 5099
		private string _unresolvedType;

		// Token: 0x040013EC RID: 5100
		private RelationshipMultiplicity? _multiplicity;

		// Token: 0x040013ED RID: 5101
		private SchemaEntityType _type;

		// Token: 0x040013EE RID: 5102
		private List<OnOperation> _operations;
	}
}
