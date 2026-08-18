using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000385 RID: 901
	internal class RowTypePropertyElement : ModelFunctionTypeElement
	{
		// Token: 0x06002099 RID: 8345 RVA: 0x00099D84 File Offset: 0x00097F84
		internal RowTypePropertyElement(SchemaElement parentElement) : base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x00099D99 File Offset: 0x00097F99
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

		// Token: 0x0600209B RID: 8347 RVA: 0x00099DBC File Offset: 0x00097FBC
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

		// Token: 0x0600209C RID: 8348 RVA: 0x00099DE0 File Offset: 0x00097FE0
		protected void HandleTypeAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetString(base.Schema, reader, out text))
			{
				return;
			}
			TypeModifier typeModifier;
			Function.RemoveTypeModifier(ref text, out typeModifier, out this._isRefType);
			TypeModifier typeModifier2 = typeModifier;
			if (typeModifier2 == TypeModifier.Array)
			{
				this._collectionKind = CollectionKind.Bag;
			}
			if (!Utils.ValidateDottedName(base.Schema, reader, text))
			{
				return;
			}
			this._unresolvedType = text;
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x00099E34 File Offset: 0x00098034
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

		// Token: 0x0600209E RID: 8350 RVA: 0x00099EA0 File Offset: 0x000980A0
		protected void HandleCollectionTypeElement(XmlReader reader)
		{
			CollectionTypeElement collectionTypeElement = new CollectionTypeElement(this);
			collectionTypeElement.Parse(reader);
			this._typeSubElement = collectionTypeElement;
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x00099EC4 File Offset: 0x000980C4
		protected void HandleReferenceTypeElement(XmlReader reader)
		{
			ReferenceTypeElement referenceTypeElement = new ReferenceTypeElement(this);
			referenceTypeElement.Parse(reader);
			this._typeSubElement = referenceTypeElement;
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x00099EE8 File Offset: 0x000980E8
		protected void HandleTypeRefElement(XmlReader reader)
		{
			TypeRefElement typeRefElement = new TypeRefElement(this);
			typeRefElement.Parse(reader);
			this._typeSubElement = typeRefElement;
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x00099F0C File Offset: 0x0009810C
		protected void HandleRowTypeElement(XmlReader reader)
		{
			RowTypeElement rowTypeElement = new RowTypeElement(this);
			rowTypeElement.Parse(reader);
			this._typeSubElement = rowTypeElement;
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x00099F30 File Offset: 0x00098130
		internal override void WriteIdentity(StringBuilder builder)
		{
			builder.Append("Property(");
			if (!string.IsNullOrWhiteSpace(base.UnresolvedType))
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

		// Token: 0x060020A3 RID: 8355 RVA: 0x00099FC9 File Offset: 0x000981C9
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

		// Token: 0x060020A4 RID: 8356 RVA: 0x00099FFC File Offset: 0x000981FC
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

		// Token: 0x060020A5 RID: 8357 RVA: 0x0009A0F4 File Offset: 0x000982F4
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

		// Token: 0x060020A6 RID: 8358 RVA: 0x0009A148 File Offset: 0x00098348
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

		// Token: 0x04000B8C RID: 2956
		private ModelFunctionTypeElement _typeSubElement;

		// Token: 0x04000B8D RID: 2957
		private bool _isRefType;

		// Token: 0x04000B8E RID: 2958
		private CollectionKind _collectionKind;
	}
}
