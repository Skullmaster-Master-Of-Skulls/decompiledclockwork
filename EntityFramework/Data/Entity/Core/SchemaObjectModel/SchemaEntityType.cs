using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200036F RID: 879
	[DebuggerDisplay("Name={Name}, BaseType={BaseType.FQName}, HasKeys={HasKeys}")]
	internal sealed class SchemaEntityType : StructuredType
	{
		// Token: 0x06001F87 RID: 8071 RVA: 0x00095E64 File Offset: 0x00094064
		public SchemaEntityType(Schema parentElement) : base(parentElement)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x00095E90 File Offset: 0x00094090
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (base.BaseType != null)
			{
				if (!(base.BaseType is SchemaEntityType))
				{
					base.AddError(ErrorCode.InvalidBaseType, EdmSchemaErrorSeverity.Error, Strings.InvalidBaseTypeForItemType(base.BaseType.FQName, this.FQName));
					return;
				}
				if (this._keyElement != null && base.BaseType != null)
				{
					base.AddError(ErrorCode.InvalidKey, EdmSchemaErrorSeverity.Error, Strings.InvalidKeyKeyDefinedInBaseClass(this.FQName, base.BaseType.FQName));
					return;
				}
			}
			else
			{
				if (this._keyElement == null)
				{
					base.AddError(ErrorCode.KeyMissingOnEntityType, EdmSchemaErrorSeverity.Error, Strings.KeyMissingOnEntityType(this.FQName));
					return;
				}
				if (base.BaseType == null && base.UnresolvedBaseType != null)
				{
					return;
				}
				this._keyElement.ResolveTopLevelNames();
			}
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x00095F44 File Offset: 0x00094144
		protected override bool HandleAttribute(XmlReader reader)
		{
			return base.HandleAttribute(reader) || (SchemaElement.CanHandleAttribute(reader, "OpenType") && base.Schema.DataModel == SchemaDataModelOption.EntityDataModel);
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x00095F6E File Offset: 0x0009416E
		public EntityKeyElement KeyElement
		{
			get
			{
				return this._keyElement;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001F8B RID: 8075 RVA: 0x00095F76 File Offset: 0x00094176
		public IList<PropertyRefElement> DeclaredKeyProperties
		{
			get
			{
				if (this.KeyElement == null)
				{
					return SchemaEntityType._emptyKeyProperties;
				}
				return this.KeyElement.KeyProperties;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001F8C RID: 8076 RVA: 0x00095F91 File Offset: 0x00094191
		public IList<PropertyRefElement> KeyProperties
		{
			get
			{
				if (this.KeyElement != null)
				{
					return this.KeyElement.KeyProperties;
				}
				if (base.BaseType != null)
				{
					return (base.BaseType as SchemaEntityType).KeyProperties;
				}
				return SchemaEntityType._emptyKeyProperties;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001F8D RID: 8077 RVA: 0x00095FC5 File Offset: 0x000941C5
		public ISchemaElementLookUpTable<NavigationProperty> NavigationProperties
		{
			get
			{
				if (this._navigationProperties == null)
				{
					this._navigationProperties = new FilteredSchemaElementLookUpTable<NavigationProperty, SchemaElement>(base.NamedMembers);
				}
				return this._navigationProperties;
			}
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x00095FE6 File Offset: 0x000941E6
		internal override void Validate()
		{
			base.Validate();
			if (this.KeyElement != null)
			{
				this.KeyElement.Validate();
			}
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x00096004 File Offset: 0x00094204
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "Key"))
			{
				this.HandleKeyElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "NavigationProperty"))
			{
				this.HandleNavigationPropertyElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "ValueAnnotation") && base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				this.SkipElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "TypeAnnotation") && base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				this.SkipElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x00096094 File Offset: 0x00094294
		private void HandleNavigationPropertyElement(XmlReader reader)
		{
			NavigationProperty navigationProperty = new NavigationProperty(this);
			navigationProperty.Parse(reader);
			base.AddMember(navigationProperty);
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x000960B6 File Offset: 0x000942B6
		private void HandleKeyElement(XmlReader reader)
		{
			this._keyElement = new EntityKeyElement(this);
			this._keyElement.Parse(reader);
		}

		// Token: 0x04000B49 RID: 2889
		private const char KEY_DELIMITER = ' ';

		// Token: 0x04000B4A RID: 2890
		private ISchemaElementLookUpTable<NavigationProperty> _navigationProperties;

		// Token: 0x04000B4B RID: 2891
		private EntityKeyElement _keyElement;

		// Token: 0x04000B4C RID: 2892
		private static readonly List<PropertyRefElement> _emptyKeyProperties = new List<PropertyRefElement>(0);
	}
}
