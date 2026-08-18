using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000363 RID: 867
	internal sealed class EntityKeyElement : SchemaElement
	{
		// Token: 0x06001F0E RID: 7950 RVA: 0x000944C5 File Offset: 0x000926C5
		public EntityKeyElement(SchemaEntityType parentElement) : base(parentElement, null)
		{
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x000944CF File Offset: 0x000926CF
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

		// Token: 0x06001F10 RID: 7952 RVA: 0x000944EA File Offset: 0x000926EA
		protected override bool HandleAttribute(XmlReader reader)
		{
			return false;
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x000944ED File Offset: 0x000926ED
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

		// Token: 0x06001F12 RID: 7954 RVA: 0x00094514 File Offset: 0x00092714
		private void HandlePropertyRefElement(XmlReader reader)
		{
			PropertyRefElement propertyRefElement = new PropertyRefElement(base.ParentElement);
			propertyRefElement.Parse(reader);
			this.KeyProperties.Add(propertyRefElement);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x00094540 File Offset: 0x00092740
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

		// Token: 0x06001F14 RID: 7956 RVA: 0x000945C0 File Offset: 0x000927C0
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

		// Token: 0x04000A8F RID: 2703
		private List<PropertyRefElement> _keyProperties;
	}
}
