using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002EA RID: 746
	internal sealed class EntityKeyElement : SchemaElement
	{
		// Token: 0x06002CB5 RID: 11445 RVA: 0x000A9632 File Offset: 0x000A7832
		public EntityKeyElement(SchemaEntityType parentElement) : base(parentElement)
		{
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x000A9DE8 File Offset: 0x000A7FE8
		public IList<PropertyRefElement> KeyProperties
		{
			get
			{
				if (this._keyProperties == null)
				{
					this._keyProperties = new List<PropertyRefElement>();
				}
				return this._keyProperties;
			}
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000173E2 File Offset: 0x000155E2
		protected override bool HandleAttribute(XmlReader reader)
		{
			return false;
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000A9E03 File Offset: 0x000A8003
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "PropertyRef"))
			{
				this.HandlePropertyRefElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x000A9E28 File Offset: 0x000A8028
		private void HandlePropertyRefElement(XmlReader reader)
		{
			PropertyRefElement propertyRefElement = new PropertyRefElement((SchemaEntityType)base.ParentElement);
			propertyRefElement.Parse(reader);
			this.KeyProperties.Add(propertyRefElement);
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x000A9E5C File Offset: 0x000A805C
		internal override void ResolveTopLevelNames()
		{
			foreach (PropertyRefElement propertyRefElement in this._keyProperties)
			{
				if (!propertyRefElement.ResolveNames((SchemaEntityType)base.ParentElement))
				{
					base.AddError(ErrorCode.InvalidKey, EdmSchemaErrorSeverity.Error, Strings.InvalidKeyNoProperty(base.ParentElement.FQName, propertyRefElement.Name));
				}
			}
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x000A9EDC File Offset: 0x000A80DC
		internal override void Validate()
		{
			Dictionary<string, PropertyRefElement> dictionary = new Dictionary<string, PropertyRefElement>(StringComparer.Ordinal);
			foreach (PropertyRefElement propertyRefElement in this._keyProperties)
			{
				StructuredProperty property = propertyRefElement.Property;
				if (dictionary.ContainsKey(property.Name))
				{
					base.AddError(ErrorCode.DuplicatePropertySpecifiedInEntityKey, EdmSchemaErrorSeverity.Error, Strings.DuplicatePropertyNameSpecifiedInEntityKey(base.ParentElement.FQName, property.Name));
				}
				else
				{
					dictionary.Add(property.Name, propertyRefElement);
					if (property.Nullable)
					{
						base.AddError(ErrorCode.InvalidKey, EdmSchemaErrorSeverity.Error, Strings.InvalidKeyNullablePart(property.Name, base.ParentElement.Name));
					}
					if ((!(property.Type is ScalarType) && !(property.Type is SchemaEnumType)) || property.CollectionKind != CollectionKind.None)
					{
						base.AddError(ErrorCode.EntityKeyMustBeScalar, EdmSchemaErrorSeverity.Error, Strings.EntityKeyMustBeScalar(property.Name, base.ParentElement.Name));
					}
					else if (!(property.Type is SchemaEnumType))
					{
						PrimitiveType primitiveType = (PrimitiveType)property.TypeUsage.EdmType;
						if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
						{
							if ((primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Binary && base.Schema.SchemaVersion < 2.0) || Helper.IsSpatialType(primitiveType))
							{
								base.AddError(ErrorCode.EntityKeyTypeCurrentlyNotSupported, EdmSchemaErrorSeverity.Error, Strings.EntityKeyTypeCurrentlyNotSupported(property.Name, base.ParentElement.FQName, primitiveType.PrimitiveTypeKind));
							}
						}
						else if ((primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Binary && base.Schema.SchemaVersion < 2.0) || Helper.IsSpatialType(primitiveType))
						{
							base.AddError(ErrorCode.EntityKeyTypeCurrentlyNotSupported, EdmSchemaErrorSeverity.Error, Strings.EntityKeyTypeCurrentlyNotSupportedInSSDL(property.Name, base.ParentElement.FQName, property.TypeUsage.EdmType.Name, property.TypeUsage.EdmType.BaseType.FullName, primitiveType.PrimitiveTypeKind));
						}
					}
				}
			}
		}

		// Token: 0x04001318 RID: 4888
		private List<PropertyRefElement> _keyProperties;
	}
}
