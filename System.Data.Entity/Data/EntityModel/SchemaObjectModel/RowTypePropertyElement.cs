using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Text;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000324 RID: 804
	internal class RowTypePropertyElement : ModelFunctionTypeElement
	{
		// Token: 0x06002F5E RID: 12126 RVA: 0x000AD434 File Offset: 0x000AB634
		internal RowTypePropertyElement(SchemaElement parentElement) : base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x000B3314 File Offset: 0x000B1514
		internal override void ResolveTopLevelNames()
		{
			if (this._unresolvedType != null)
			{
				base.ResolveTopLevelNames();
			}
			if (this._typeSubElement != null)
			{
				this._typeSubElement.ResolveTopLevelNames();
			}
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x000B3337 File Offset: 0x000B1537
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Type"))
			{
				this.HandleTypeAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x000B335C File Offset: 0x000B155C
		protected void HandleTypeAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetString(base.Schema, reader, out text))
			{
				return;
			}
			TypeModifier typeModifier;
			Function.RemoveTypeModifier(ref text, out typeModifier, out this._isRefType);
			if (typeModifier == TypeModifier.Array)
			{
				this._collectionKind = CollectionKind.Bag;
			}
			if (!Utils.ValidateDottedName(base.Schema, reader, text))
			{
				return;
			}
			this._unresolvedType = text;
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x000B33AC File Offset: 0x000B15AC
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.CanHandleElement(reader, "CollectionType"))
			{
				this.HandleCollectionTypeElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "ReferenceType"))
			{
				this.HandleReferenceTypeElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "TypeRef"))
			{
				this.HandleTypeRefElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "RowType"))
			{
				this.HandleRowTypeElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x000B3418 File Offset: 0x000B1618
		protected void HandleCollectionTypeElement(XmlReader reader)
		{
			CollectionTypeElement collectionTypeElement = new CollectionTypeElement(this);
			collectionTypeElement.Parse(reader);
			this._typeSubElement = collectionTypeElement;
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x000B343C File Offset: 0x000B163C
		protected void HandleReferenceTypeElement(XmlReader reader)
		{
			ReferenceTypeElement referenceTypeElement = new ReferenceTypeElement(this);
			referenceTypeElement.Parse(reader);
			this._typeSubElement = referenceTypeElement;
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x000B3460 File Offset: 0x000B1660
		protected void HandleTypeRefElement(XmlReader reader)
		{
			TypeRefElement typeRefElement = new TypeRefElement(this);
			typeRefElement.Parse(reader);
			this._typeSubElement = typeRefElement;
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x000B3484 File Offset: 0x000B1684
		protected void HandleRowTypeElement(XmlReader reader)
		{
			RowTypeElement rowTypeElement = new RowTypeElement(this);
			rowTypeElement.Parse(reader);
			this._typeSubElement = rowTypeElement;
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x000B34A8 File Offset: 0x000B16A8
		internal override void WriteIdentity(StringBuilder builder)
		{
			builder.Append("Property(");
			if (base.UnresolvedType != null && !base.UnresolvedType.Trim().Equals(string.Empty))
			{
				if (this._collectionKind != CollectionKind.None)
				{
					builder.Append("Collection(" + base.UnresolvedType + ")");
				}
				else if (this._isRefType)
				{
					builder.Append("Ref(" + base.UnresolvedType + ")");
				}
				else
				{
					builder.Append(base.UnresolvedType);
				}
			}
			else
			{
				this._typeSubElement.WriteIdentity(builder);
			}
			builder.Append(")");
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x000B3553 File Offset: 0x000B1753
		internal override TypeUsage GetTypeUsage()
		{
			if (this._typeUsage != null)
			{
				return this._typeUsage;
			}
			if (this._typeSubElement != null)
			{
				this._typeUsage = this._typeSubElement.GetTypeUsage();
			}
			return this._typeUsage;
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x000B3584 File Offset: 0x000B1784
		internal override bool ResolveNameAndSetTypeUsage(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			if (this._typeUsage != null)
			{
				return true;
			}
			if (this._typeSubElement != null)
			{
				return this._typeSubElement.ResolveNameAndSetTypeUsage(convertedItemCache, newGlobalItems);
			}
			if (this._type is ScalarType)
			{
				this._typeUsageBuilder.ValidateAndSetTypeUsage(this._type as ScalarType, false);
				this._typeUsage = this._typeUsageBuilder.TypeUsage;
			}
			else
			{
				EdmType edmType = (EdmType)Converter.LoadSchemaElement(this._type, this._type.Schema.ProviderManifest, convertedItemCache, newGlobalItems);
				if (edmType != null)
				{
					if (this._isRefType)
					{
						EntityType entityType = edmType as EntityType;
						this._typeUsage = TypeUsage.Create(new RefType(entityType));
					}
					else
					{
						this._typeUsageBuilder.ValidateAndSetTypeUsage(edmType, false);
						this._typeUsage = this._typeUsageBuilder.TypeUsage;
					}
				}
			}
			if (this._collectionKind != CollectionKind.None)
			{
				this._typeUsage = TypeUsage.Create(new CollectionType(this._typeUsage));
			}
			return this._typeUsage != null;
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x000B3678 File Offset: 0x000B1878
		internal bool ValidateIsScalar()
		{
			if (this._type != null)
			{
				if (!(this._type is ScalarType) || this._isRefType || this._collectionKind != CollectionKind.None)
				{
					return false;
				}
			}
			else if (this._typeSubElement != null && !(this._typeSubElement.Type is ScalarType))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x000B36CC File Offset: 0x000B18CC
		internal override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateFacets(this, this._type, this._typeUsageBuilder);
			ValidationHelper.ValidateTypeDeclaration(this, this._type, this._typeSubElement);
			if (this._isRefType)
			{
				ValidationHelper.ValidateRefType(this, this._type);
			}
			if (this._typeSubElement != null)
			{
				this._typeSubElement.Validate();
			}
		}

		// Token: 0x0400145E RID: 5214
		private ModelFunctionTypeElement _typeSubElement;

		// Token: 0x0400145F RID: 5215
		private bool _isRefType;

		// Token: 0x04001460 RID: 5216
		private CollectionKind _collectionKind;
	}
}
