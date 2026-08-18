using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002F6 RID: 758
	[DebuggerDisplay("Name={Name}, BaseType={BaseType.FQName}, HasKeys={HasKeys}")]
	internal sealed class SchemaEntityType : StructuredType
	{
		// Token: 0x06002D30 RID: 11568 RVA: 0x000AB56E File Offset: 0x000A976E
		public SchemaEntityType(Schema parentElement) : base(parentElement)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x000AB59C File Offset: 0x000A979C
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

		// Token: 0x06002D32 RID: 11570 RVA: 0x000AB650 File Offset: 0x000A9850
		protected override bool HandleAttribute(XmlReader reader)
		{
			return base.HandleAttribute(reader) || (SchemaElement.CanHandleAttribute(reader, "OpenType") && base.Schema.DataModel == SchemaDataModelOption.EntityDataModel);
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06002D33 RID: 11571 RVA: 0x000AB67A File Offset: 0x000A987A
		public EntityKeyElement KeyElement
		{
			get
			{
				return this._keyElement;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002D34 RID: 11572 RVA: 0x000AB682 File Offset: 0x000A9882
		public IList<PropertyRefElement> DeclaredKeyProperties
		{
			get
			{
				if (this.KeyElement == null)
				{
					return SchemaEntityType.EmptyKeyProperties;
				}
				return this.KeyElement.KeyProperties;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002D35 RID: 11573 RVA: 0x000AB69D File Offset: 0x000A989D
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
				return SchemaEntityType.EmptyKeyProperties;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002D36 RID: 11574 RVA: 0x000AB6D1 File Offset: 0x000A98D1
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

		// Token: 0x06002D37 RID: 11575 RVA: 0x000AB6F2 File Offset: 0x000A98F2
		internal override void Validate()
		{
			base.Validate();
			if (this.KeyElement != null)
			{
				this.KeyElement.Validate();
			}
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x000AB710 File Offset: 0x000A9910
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
				base.SkipElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "TypeAnnotation") && base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.SkipElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x000AB7A0 File Offset: 0x000A99A0
		private void HandleNavigationPropertyElement(XmlReader reader)
		{
			NavigationProperty navigationProperty = new NavigationProperty(this);
			navigationProperty.Parse(reader);
			base.AddMember(navigationProperty);
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x000AB7C2 File Offset: 0x000A99C2
		private void HandleKeyElement(XmlReader reader)
		{
			this._keyElement = new EntityKeyElement(this);
			this._keyElement.Parse(reader);
		}

		// Token: 0x040013CD RID: 5069
		private const char KEY_DELIMITER = ' ';

		// Token: 0x040013CE RID: 5070
		private ISchemaElementLookUpTable<NavigationProperty> _navigationProperties;

		// Token: 0x040013CF RID: 5071
		private EntityKeyElement _keyElement;

		// Token: 0x040013D0 RID: 5072
		private static List<PropertyRefElement> EmptyKeyProperties = new List<PropertyRefElement>(0);
	}
}
