using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020001F4 RID: 500
	public abstract class MetadataItem
	{
		// Token: 0x0600116E RID: 4462 RVA: 0x00049BB1 File Offset: 0x00047DB1
		internal MetadataItem()
		{
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00049BB9 File Offset: 0x00047DB9
		internal MetadataItem(MetadataItem.MetadataFlags flags)
		{
			this._flags = (int)flags;
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x00049BD0 File Offset: 0x00047DD0
		internal virtual IEnumerable<MetadataProperty> Annotations
		{
			get
			{
				return from p in this.GetMetadataProperties()
				where p.IsAnnotation
				select p;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06001171 RID: 4465
		public abstract BuiltInTypeKind BuiltInTypeKind { get; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x00049BFA File Offset: 0x00047DFA
		[MetadataProperty(BuiltInTypeKind.MetadataProperty, true)]
		public virtual ReadOnlyMetadataCollection<MetadataProperty> MetadataProperties
		{
			get
			{
				return this.GetMetadataProperties().AsReadOnlyMetadataCollection();
			}
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00049C08 File Offset: 0x00047E08
		internal MetadataPropertyCollection GetMetadataProperties()
		{
			if (this._itemAttributes == null)
			{
				MetadataPropertyCollection metadataPropertyCollection = new MetadataPropertyCollection(this);
				if (this.IsReadOnly)
				{
					metadataPropertyCollection.SetReadOnly();
				}
				Interlocked.CompareExchange<MetadataPropertyCollection>(ref this._itemAttributes, metadataPropertyCollection, null);
			}
			return this._itemAttributes;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00049C64 File Offset: 0x00047E64
		public void AddAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			MetadataProperty metadataProperty = this.Annotations.FirstOrDefault((MetadataProperty a) => a.Name == name);
			if (metadataProperty == null)
			{
				if (value != null)
				{
					this.GetMetadataProperties().Add(MetadataProperty.CreateAnnotation(name, value));
				}
				return;
			}
			if (value == null)
			{
				this.RemoveAnnotation(name);
				return;
			}
			metadataProperty.Value = value;
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00049CE0 File Offset: 0x00047EE0
		public bool RemoveAnnotation(string name)
		{
			Check.NotEmpty(name, "name");
			MetadataPropertyCollection metadataProperties = this.GetMetadataProperties();
			MetadataProperty item;
			return metadataProperties.TryGetValue(name, false, out item) && metadataProperties.Remove(item);
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x00049D15 File Offset: 0x00047F15
		internal MetadataCollection<MetadataProperty> RawMetadataProperties
		{
			get
			{
				return this._itemAttributes;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x00049D1D File Offset: 0x00047F1D
		// (set) Token: 0x06001178 RID: 4472 RVA: 0x00049D25 File Offset: 0x00047F25
		public Documentation Documentation { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06001179 RID: 4473
		internal abstract string Identity { get; }

		// Token: 0x0600117A RID: 4474 RVA: 0x00049D2E File Offset: 0x00047F2E
		internal virtual bool EdmEquals(MetadataItem item)
		{
			return item != null && (this == item || (this.BuiltInTypeKind == item.BuiltInTypeKind && this.Identity == item.Identity));
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x00049D5C File Offset: 0x00047F5C
		internal bool IsReadOnly
		{
			get
			{
				return this.GetFlag(MetadataItem.MetadataFlags.Readonly);
			}
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00049D65 File Offset: 0x00047F65
		internal virtual void SetReadOnly()
		{
			if (!this.IsReadOnly)
			{
				if (this._itemAttributes != null)
				{
					this._itemAttributes.SetReadOnly();
				}
				this.SetFlag(MetadataItem.MetadataFlags.Readonly, true);
			}
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00049D8B File Offset: 0x00047F8B
		internal virtual void BuildIdentity(StringBuilder builder)
		{
			builder.Append(this.Identity);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00049D9A File Offset: 0x00047F9A
		internal void AddMetadataProperties(List<MetadataProperty> metadataProperties)
		{
			this.GetMetadataProperties().AddRange(metadataProperties);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00049DA8 File Offset: 0x00047FA8
		internal DataSpace GetDataSpace()
		{
			switch (this._flags & 7)
			{
			case 1:
				return DataSpace.CSpace;
			case 2:
				return DataSpace.OSpace;
			case 3:
				return DataSpace.OCSpace;
			case 4:
				return DataSpace.SSpace;
			case 5:
				return DataSpace.CSSpace;
			default:
				return (DataSpace)(-1);
			}
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00049DE5 File Offset: 0x00047FE5
		internal void SetDataSpace(DataSpace space)
		{
			this._flags = ((this._flags & -8) | (int)(MetadataItem.MetadataFlags.DataSpace & MetadataItem.Convert(space)));
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00049E00 File Offset: 0x00048000
		private static MetadataItem.MetadataFlags Convert(DataSpace space)
		{
			switch (space)
			{
			case DataSpace.OSpace:
				return MetadataItem.MetadataFlags.OSpace;
			case DataSpace.CSpace:
				return MetadataItem.MetadataFlags.CSpace;
			case DataSpace.SSpace:
				return MetadataItem.MetadataFlags.SSpace;
			case DataSpace.OCSpace:
				return MetadataItem.MetadataFlags.OCSpace;
			case DataSpace.CSSpace:
				return MetadataItem.MetadataFlags.CSSpace;
			default:
				return MetadataItem.MetadataFlags.None;
			}
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00049E34 File Offset: 0x00048034
		internal ParameterMode GetParameterMode()
		{
			MetadataItem.MetadataFlags metadataFlags = (MetadataItem.MetadataFlags)(this._flags & 3584);
			if (metadataFlags <= MetadataItem.MetadataFlags.Out)
			{
				if (metadataFlags == MetadataItem.MetadataFlags.In)
				{
					return ParameterMode.In;
				}
				if (metadataFlags == MetadataItem.MetadataFlags.Out)
				{
					return ParameterMode.Out;
				}
			}
			else
			{
				if (metadataFlags == MetadataItem.MetadataFlags.InOut)
				{
					return ParameterMode.InOut;
				}
				if (metadataFlags == MetadataItem.MetadataFlags.ReturnValue)
				{
					return ParameterMode.ReturnValue;
				}
			}
			return (ParameterMode)(-1);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00049E81 File Offset: 0x00048081
		internal void SetParameterMode(ParameterMode mode)
		{
			this._flags = ((this._flags & -3585) | (int)(MetadataItem.MetadataFlags.ParameterMode & MetadataItem.Convert(mode)));
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00049EA4 File Offset: 0x000480A4
		private static MetadataItem.MetadataFlags Convert(ParameterMode mode)
		{
			switch (mode)
			{
			case ParameterMode.In:
				return MetadataItem.MetadataFlags.In;
			case ParameterMode.Out:
				return MetadataItem.MetadataFlags.Out;
			case ParameterMode.InOut:
				return MetadataItem.MetadataFlags.InOut;
			case ParameterMode.ReturnValue:
				return MetadataItem.MetadataFlags.ReturnValue;
			default:
				return MetadataItem.MetadataFlags.ParameterMode;
			}
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00049EE6 File Offset: 0x000480E6
		internal bool GetFlag(MetadataItem.MetadataFlags flag)
		{
			return flag == (MetadataItem.MetadataFlags)(this._flags & (int)flag);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00049EF4 File Offset: 0x000480F4
		internal void SetFlag(MetadataItem.MetadataFlags flag, bool value)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int flags = this._flags;
				int value2 = value ? (flags | (int)flag) : (flags & (int)(~(int)flag));
				if ((flags & 8) == 8)
				{
					break;
				}
				if (flags == Interlocked.CompareExchange(ref this._flags, value2, flags))
				{
					return;
				}
				spinWait.SpinOnce();
			}
			if ((flag & MetadataItem.MetadataFlags.Readonly) == MetadataItem.MetadataFlags.Readonly)
			{
				return;
			}
			throw new InvalidOperationException(Strings.OperationOnReadOnlyItem);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00049F50 File Offset: 0x00048150
		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
		static MetadataItem()
		{
			MetadataItem._builtInTypes[0] = new ComplexType();
			MetadataItem._builtInTypes[2] = new ComplexType();
			MetadataItem._builtInTypes[1] = new ComplexType();
			MetadataItem._builtInTypes[3] = new ComplexType();
			MetadataItem._builtInTypes[3] = new ComplexType();
			MetadataItem._builtInTypes[7] = new EnumType();
			MetadataItem._builtInTypes[6] = new ComplexType();
			MetadataItem._builtInTypes[8] = new ComplexType();
			MetadataItem._builtInTypes[9] = new ComplexType();
			MetadataItem._builtInTypes[10] = new EnumType();
			MetadataItem._builtInTypes[11] = new ComplexType();
			MetadataItem._builtInTypes[12] = new ComplexType();
			MetadataItem._builtInTypes[13] = new ComplexType();
			MetadataItem._builtInTypes[14] = new ComplexType();
			MetadataItem._builtInTypes[4] = new ComplexType();
			MetadataItem._builtInTypes[5] = new ComplexType();
			MetadataItem._builtInTypes[15] = new ComplexType();
			MetadataItem._builtInTypes[16] = new ComplexType();
			MetadataItem._builtInTypes[17] = new ComplexType();
			MetadataItem._builtInTypes[18] = new ComplexType();
			MetadataItem._builtInTypes[19] = new ComplexType();
			MetadataItem._builtInTypes[20] = new ComplexType();
			MetadataItem._builtInTypes[21] = new ComplexType();
			MetadataItem._builtInTypes[22] = new ComplexType();
			MetadataItem._builtInTypes[23] = new ComplexType();
			MetadataItem._builtInTypes[24] = new ComplexType();
			MetadataItem._builtInTypes[25] = new EnumType();
			MetadataItem._builtInTypes[26] = new ComplexType();
			MetadataItem._builtInTypes[27] = new EnumType();
			MetadataItem._builtInTypes[28] = new ComplexType();
			MetadataItem._builtInTypes[29] = new ComplexType();
			MetadataItem._builtInTypes[30] = new ComplexType();
			MetadataItem._builtInTypes[31] = new ComplexType();
			MetadataItem._builtInTypes[32] = new ComplexType();
			MetadataItem._builtInTypes[33] = new EnumType();
			MetadataItem._builtInTypes[34] = new ComplexType();
			MetadataItem._builtInTypes[35] = new ComplexType();
			MetadataItem._builtInTypes[36] = new ComplexType();
			MetadataItem._builtInTypes[37] = new ComplexType();
			MetadataItem._builtInTypes[38] = new ComplexType();
			MetadataItem._builtInTypes[39] = new ComplexType();
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem), "ItemType", false, null);
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataProperty), "MetadataProperty", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.GlobalItem), "GlobalItem", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.TypeUsage), "TypeUsage", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType), "EdmType", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.GlobalItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.SimpleType), "SimpleType", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EnumType), "EnumType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.SimpleType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.PrimitiveType), "PrimitiveType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.SimpleType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.CollectionType), "CollectionType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.RefType), "RefType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmMember), "EdmMember", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmProperty), "EdmProperty", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmMember));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.NavigationProperty), "NavigationProperty", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmMember));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.ProviderManifest), "ProviderManifest", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.RelationshipEndMember), "RelationshipEnd", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmMember));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.AssociationEndMember), "AssociationEnd", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.RelationshipEndMember));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EnumMember), "EnumMember", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.ReferentialConstraint), "ReferentialConstraint", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.StructuralType), "StructuralType", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.RowType), "RowType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.StructuralType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.ComplexType), "ComplexType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.StructuralType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EntityTypeBase), "ElementType", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.StructuralType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EntityType), "EntityType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EntityTypeBase));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.RelationshipType), "RelationshipType", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EntityTypeBase));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.AssociationType), "AssociationType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.RelationshipType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.Facet), "Facet", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EntityContainer), "EntityContainerType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.GlobalItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EntitySetBase), "BaseEntitySetType", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EntitySet), "EntitySetType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EntitySetBase));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.RelationshipSet), "RelationshipSet", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EntitySetBase));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.AssociationSet), "AssocationSetType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.RelationshipSet));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.AssociationSetEnd), "AssociationSetEndType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.FunctionParameter), "FunctionParameter", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmFunction), "EdmFunction", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.Documentation), "Documentation", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeEnumType(BuiltInTypeKind.OperationAction, "DeleteAction", new string[]
			{
				"None",
				"Cascade"
			});
			MetadataItem.InitializeEnumType(BuiltInTypeKind.RelationshipMultiplicity, "RelationshipMultiplicity", new string[]
			{
				"One",
				"ZeroToOne",
				"Many"
			});
			MetadataItem.InitializeEnumType(BuiltInTypeKind.ParameterMode, "ParameterMode", new string[]
			{
				"In",
				"Out",
				"InOut"
			});
			MetadataItem.InitializeEnumType(BuiltInTypeKind.CollectionKind, "CollectionKind", new string[]
			{
				"None",
				"List",
				"Bag"
			});
			MetadataItem.InitializeEnumType(BuiltInTypeKind.PrimitiveTypeKind, "PrimitiveTypeKind", Enum.GetNames(typeof(PrimitiveTypeKind)));
			FacetDescription[] array = new FacetDescription[2];
			MetadataItem._nullableFacetDescription = new FacetDescription("Nullable", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean), null, null, true);
			array[0] = MetadataItem._nullableFacetDescription;
			MetadataItem._defaultValueFacetDescription = new FacetDescription("DefaultValue", MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType), null, null, null);
			array[1] = MetadataItem._defaultValueFacetDescription;
			MetadataItem._generalFacetDescriptions = new ReadOnlyCollection<FacetDescription>(array);
			MetadataItem._collectionKindFacetDescription = new FacetDescription("CollectionKind", MetadataItem.GetBuiltInType(BuiltInTypeKind.EnumType), null, null, null);
			TypeUsage typeUsage = TypeUsage.Create(MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.String));
			TypeUsage typeUsage2 = TypeUsage.Create(MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean));
			TypeUsage typeUsage3 = TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType));
			TypeUsage typeUsage4 = TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.TypeUsage));
			TypeUsage typeUsage5 = TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.ComplexType));
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.MetadataProperty, new EdmProperty[]
			{
				new EdmProperty("Name", typeUsage),
				new EdmProperty("TypeUsage", typeUsage4),
				new EdmProperty("Value", typeUsage5)
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.MetadataItem, new EdmProperty[]
			{
				new EdmProperty("MetadataProperties", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataProperty).GetCollectionType())),
				new EdmProperty("Documentation", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.Documentation)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.TypeUsage, new EdmProperty[]
			{
				new EdmProperty("EdmType", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType))),
				new EdmProperty("Facets", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.Facet)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.EdmType, new EdmProperty[]
			{
				new EdmProperty("Name", typeUsage),
				new EdmProperty("Namespace", typeUsage),
				new EdmProperty("Abstract", typeUsage2),
				new EdmProperty("Sealed", typeUsage2),
				new EdmProperty("BaseType", typeUsage5)
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.EnumType, new EdmProperty[]
			{
				new EdmProperty("EnumMembers", typeUsage)
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.CollectionType, new EdmProperty[]
			{
				new EdmProperty("TypeUsage", typeUsage4)
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.RefType, new EdmProperty[]
			{
				new EdmProperty("EntityType", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EntityType)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.EdmMember, new EdmProperty[]
			{
				new EdmProperty("Name", typeUsage),
				new EdmProperty("TypeUsage", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.TypeUsage)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.EdmProperty, new EdmProperty[]
			{
				new EdmProperty("Nullable", typeUsage),
				new EdmProperty("DefaultValue", typeUsage5)
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.NavigationProperty, new EdmProperty[]
			{
				new EdmProperty("RelationshipTypeName", typeUsage),
				new EdmProperty("ToEndMemberName", typeUsage)
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.RelationshipEndMember, new EdmProperty[]
			{
				new EdmProperty("OperationBehaviors", typeUsage5),
				new EdmProperty("RelationshipMultiplicity", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EnumType)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.EnumMember, new EdmProperty[]
			{
				new EdmProperty("Name", typeUsage)
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.ReferentialConstraint, new EdmProperty[]
			{
				new EdmProperty("ToRole", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.RelationshipEndMember))),
				new EdmProperty("FromRole", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.RelationshipEndMember))),
				new EdmProperty("ToProperties", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmProperty).GetCollectionType())),
				new EdmProperty("FromProperties", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmProperty).GetCollectionType()))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.StructuralType, new EdmProperty[]
			{
				new EdmProperty("Members", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmMember)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.EntityTypeBase, new EdmProperty[]
			{
				new EdmProperty("KeyMembers", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmMember)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.Facet, new EdmProperty[]
			{
				new EdmProperty("Name", typeUsage),
				new EdmProperty("EdmType", typeUsage3),
				new EdmProperty("Value", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.EntityContainer, new EdmProperty[]
			{
				new EdmProperty("Name", typeUsage),
				new EdmProperty("EntitySets", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EntitySet)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.EntitySetBase, new EdmProperty[]
			{
				new EdmProperty("Name", typeUsage),
				new EdmProperty("EntityType", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EntityType))),
				new EdmProperty("Schema", typeUsage),
				new EdmProperty("Table", typeUsage)
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.AssociationSet, new EdmProperty[]
			{
				new EdmProperty("AssociationSetEnds", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.AssociationSetEnd).GetCollectionType()))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.AssociationSetEnd, new EdmProperty[]
			{
				new EdmProperty("Role", typeUsage),
				new EdmProperty("EntitySetType", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EntitySet)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.FunctionParameter, new EdmProperty[]
			{
				new EdmProperty("Name", typeUsage),
				new EdmProperty("Mode", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EnumType))),
				new EdmProperty("TypeUsage", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.TypeUsage)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.EdmFunction, new EdmProperty[]
			{
				new EdmProperty("Name", typeUsage),
				new EdmProperty("Namespace", typeUsage),
				new EdmProperty("ReturnParameter", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.FunctionParameter))),
				new EdmProperty("Parameters", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.FunctionParameter).GetCollectionType()))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.Documentation, new EdmProperty[]
			{
				new EdmProperty("Summary", typeUsage),
				new EdmProperty("LongDescription", typeUsage)
			});
			for (int i = 0; i < MetadataItem._builtInTypes.Length; i++)
			{
				MetadataItem._builtInTypes[i].SetReadOnly();
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x0004ADA4 File Offset: 0x00048FA4
		internal static FacetDescription DefaultValueFacetDescription
		{
			get
			{
				return MetadataItem._defaultValueFacetDescription;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x0004ADAB File Offset: 0x00048FAB
		internal static FacetDescription CollectionKindFacetDescription
		{
			get
			{
				return MetadataItem._collectionKindFacetDescription;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x0004ADB2 File Offset: 0x00048FB2
		internal static FacetDescription NullableFacetDescription
		{
			get
			{
				return MetadataItem._nullableFacetDescription;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x0004ADB9 File Offset: 0x00048FB9
		internal static EdmProviderManifest EdmProviderManifest
		{
			get
			{
				return EdmProviderManifest.Instance;
			}
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0004ADC0 File Offset: 0x00048FC0
		public static EdmType GetBuiltInType(BuiltInTypeKind builtInTypeKind)
		{
			return MetadataItem._builtInTypes[(int)builtInTypeKind];
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0004ADC9 File Offset: 0x00048FC9
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public static ReadOnlyCollection<FacetDescription> GetGeneralFacetDescriptions()
		{
			return MetadataItem._generalFacetDescriptions;
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0004ADD0 File Offset: 0x00048FD0
		private static void InitializeBuiltInTypes(ComplexType builtInType, string name, bool isAbstract, ComplexType baseType)
		{
			EdmType.Initialize(builtInType, name, "Edm", DataSpace.CSpace, isAbstract, baseType);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0004ADE4 File Offset: 0x00048FE4
		private static void AddBuiltInTypeProperties(BuiltInTypeKind builtInTypeKind, EdmProperty[] properties)
		{
			ComplexType complexType = (ComplexType)MetadataItem.GetBuiltInType(builtInTypeKind);
			if (properties != null)
			{
				for (int i = 0; i < properties.Length; i++)
				{
					complexType.AddMember(properties[i]);
				}
			}
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0004AE18 File Offset: 0x00049018
		private static void InitializeEnumType(BuiltInTypeKind builtInTypeKind, string name, string[] enumMemberNames)
		{
			EnumType enumType = (EnumType)MetadataItem.GetBuiltInType(builtInTypeKind);
			EdmType.Initialize(enumType, name, "Edm", DataSpace.CSpace, false, null);
			for (int i = 0; i < enumMemberNames.Length; i++)
			{
				enumType.AddMember(new EnumMember(enumMemberNames[i], i));
			}
		}

		// Token: 0x0400052E RID: 1326
		private int _flags;

		// Token: 0x0400052F RID: 1327
		private MetadataPropertyCollection _itemAttributes;

		// Token: 0x04000530 RID: 1328
		private static readonly EdmType[] _builtInTypes = new EdmType[40];

		// Token: 0x04000531 RID: 1329
		private static readonly ReadOnlyCollection<FacetDescription> _generalFacetDescriptions;

		// Token: 0x04000532 RID: 1330
		private static readonly FacetDescription _nullableFacetDescription;

		// Token: 0x04000533 RID: 1331
		private static readonly FacetDescription _defaultValueFacetDescription;

		// Token: 0x04000534 RID: 1332
		private static readonly FacetDescription _collectionKindFacetDescription;

		// Token: 0x020001F5 RID: 501
		[Flags]
		internal enum MetadataFlags
		{
			// Token: 0x04000538 RID: 1336
			None = 0,
			// Token: 0x04000539 RID: 1337
			CSpace = 1,
			// Token: 0x0400053A RID: 1338
			OSpace = 2,
			// Token: 0x0400053B RID: 1339
			OCSpace = 3,
			// Token: 0x0400053C RID: 1340
			SSpace = 4,
			// Token: 0x0400053D RID: 1341
			CSSpace = 5,
			// Token: 0x0400053E RID: 1342
			DataSpace = 7,
			// Token: 0x0400053F RID: 1343
			Readonly = 8,
			// Token: 0x04000540 RID: 1344
			IsAbstract = 16,
			// Token: 0x04000541 RID: 1345
			In = 512,
			// Token: 0x04000542 RID: 1346
			Out = 1024,
			// Token: 0x04000543 RID: 1347
			InOut = 1536,
			// Token: 0x04000544 RID: 1348
			ReturnValue = 2048,
			// Token: 0x04000545 RID: 1349
			ParameterMode = 3584
		}
	}
}
