using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200037F RID: 895
	internal sealed class RelationshipEnd : SchemaElement, IRelationshipEnd
	{
		// Token: 0x06002048 RID: 8264 RVA: 0x00098F29 File Offset: 0x00097129
		public RelationshipEnd(Relationship relationship) : base(relationship, null)
		{
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06002049 RID: 8265 RVA: 0x00098F33 File Offset: 0x00097133
		// (set) Token: 0x0600204A RID: 8266 RVA: 0x00098F3B File Offset: 0x0009713B
		public SchemaEntityType Type { get; private set; }

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600204B RID: 8267 RVA: 0x00098F44 File Offset: 0x00097144
		// (set) Token: 0x0600204C RID: 8268 RVA: 0x00098F4C File Offset: 0x0009714C
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

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600204D RID: 8269 RVA: 0x00098F55 File Offset: 0x00097155
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

		// Token: 0x0600204E RID: 8270 RVA: 0x00098F70 File Offset: 0x00097170
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

		// Token: 0x0600204F RID: 8271 RVA: 0x00098FE0 File Offset: 0x000971E0
		internal override void Validate()
		{
			base.Validate();
			if (this.Multiplicity == RelationshipMultiplicity.Many && this.Operations.Count != 0)
			{
				base.AddError(ErrorCode.EndWithManyMultiplicityCannotHaveOperationsSpecified, EdmSchemaErrorSeverity.Error, Strings.EndWithManyMultiplicityCannotHaveOperationsSpecified(this.Name, this.ParentElement.FQName));
			}
			if (this.ParentElement.Constraints.Count == 0 && this.Multiplicity == null)
			{
				base.AddError(ErrorCode.EndWithoutMultiplicity, EdmSchemaErrorSeverity.Error, Strings.EndWithoutMultiplicity(this.Name, this.ParentElement.FQName));
			}
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00099083 File Offset: 0x00097283
		protected override void HandleAttributesComplete()
		{
			if (this.Name == null && this._unresolvedType != null)
			{
				this.Name = Utils.ExtractTypeName(this._unresolvedType);
			}
			base.HandleAttributesComplete();
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x000990AC File Offset: 0x000972AC
		protected override bool ProhibitAttribute(string namespaceUri, string localName)
		{
			return base.ProhibitAttribute(namespaceUri, localName) || (namespaceUri == null && localName == "Name" && false);
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x000990D0 File Offset: 0x000972D0
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

		// Token: 0x06002053 RID: 8275 RVA: 0x0009912B File Offset: 0x0009732B
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

		// Token: 0x06002054 RID: 8276 RVA: 0x00099150 File Offset: 0x00097350
		private void HandleTypeAttribute(XmlReader reader)
		{
			string unresolvedType;
			if (!Utils.GetDottedName(base.Schema, reader, out unresolvedType))
			{
				return;
			}
			this._unresolvedType = unresolvedType;
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x00099178 File Offset: 0x00097378
		private void HandleMultiplicityAttribute(XmlReader reader)
		{
			RelationshipMultiplicity value;
			if (!RelationshipMultiplicityConverter.TryParseMultiplicity(reader.Value, out value))
			{
				base.AddError(ErrorCode.InvalidMultiplicity, EdmSchemaErrorSeverity.Error, reader, Strings.InvalidRelationshipEndMultiplicity(this.ParentElement.Name, reader.Value));
			}
			this._multiplicity = new RelationshipMultiplicity?(value);
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x000991C0 File Offset: 0x000973C0
		private void HandleOnDeleteElement(XmlReader reader)
		{
			this.HandleOnOperationElement(reader, Operation.Delete);
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x000991CC File Offset: 0x000973CC
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

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06002058 RID: 8280 RVA: 0x0009924C File Offset: 0x0009744C
		internal new IRelationship ParentElement
		{
			get
			{
				return (IRelationship)base.ParentElement;
			}
		}

		// Token: 0x04000B7A RID: 2938
		private string _unresolvedType;

		// Token: 0x04000B7B RID: 2939
		private RelationshipMultiplicity? _multiplicity;

		// Token: 0x04000B7C RID: 2940
		private List<OnOperation> _operations;
	}
}
