using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001E2 RID: 482
	public abstract class MetadataItem
	{
		// Token: 0x06002080 RID: 8320 RVA: 0x00071230 File Offset: 0x0006F430
		internal MetadataItem()
		{
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x00071243 File Offset: 0x0006F443
		internal MetadataItem(MetadataItem.MetadataFlags flags)
		{
			this._flags = flags;
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06002082 RID: 8322
		public abstract BuiltInTypeKind BuiltInTypeKind { get; }

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x00071260 File Offset: 0x0006F460
		[MetadataProperty(BuiltInTypeKind.MetadataProperty, true)]
		public ReadOnlyMetadataCollection<MetadataProperty> MetadataProperties
		{
			get
			{
				if (this._itemAttributes == null)
				{
					MetadataPropertyCollection metadataPropertyCollection = new MetadataPropertyCollection(this);
					if (this.IsReadOnly)
					{
						metadataPropertyCollection.SetReadOnly();
					}
					Interlocked.CompareExchange<MetadataCollection<MetadataProperty>>(ref this._itemAttributes, metadataPropertyCollection, null);
				}
				return this._itemAttributes.AsReadOnlyMetadataCollection();
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002084 RID: 8324 RVA: 0x000712A4 File Offset: 0x0006F4A4
		internal MetadataCollection<MetadataProperty> RawMetadataProperties
		{
			get
			{
				return this._itemAttributes;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06002085 RID: 8325 RVA: 0x000712AC File Offset: 0x0006F4AC
		// (set) Token: 0x06002086 RID: 8326 RVA: 0x000712B4 File Offset: 0x0006F4B4
		public Documentation Documentation
		{
			get
			{
				return this._documentation;
			}
			set
			{
				this._documentation = value;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06002087 RID: 8327
		internal abstract string Identity { get; }

		// Token: 0x06002088 RID: 8328 RVA: 0x000712BD File Offset: 0x0006F4BD
		internal virtual bool EdmEquals(MetadataItem item)
		{
			return item != null && (this == item || (this.BuiltInTypeKind == item.BuiltInTypeKind && this.Identity == item.Identity));
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x000712EB File Offset: 0x0006F4EB
		internal bool IsReadOnly
		{
			get
			{
				return this.GetFlag(MetadataItem.MetadataFlags.Readonly);
			}
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x000712F4 File Offset: 0x0006F4F4
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

		// Token: 0x0600208B RID: 8331 RVA: 0x0007131A File Offset: 0x0006F51A
		internal virtual void BuildIdentity(StringBuilder builder)
		{
			builder.Append(this.Identity);
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x00071329 File Offset: 0x0006F529
		internal void AddMetadataProperties(List<MetadataProperty> metadataProperties)
		{
			this.MetadataProperties.Source.AtomicAddRange(metadataProperties);
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x00071340 File Offset: 0x0006F540
		internal DataSpace GetDataSpace()
		{
			switch (this._flags & MetadataItem.MetadataFlags.DataSpace)
			{
			case MetadataItem.MetadataFlags.CSpace:
				return DataSpace.CSpace;
			case MetadataItem.MetadataFlags.OSpace:
				return DataSpace.OSpace;
			case MetadataItem.MetadataFlags.OCSpace:
				return DataSpace.OCSpace;
			case MetadataItem.MetadataFlags.SSpace:
				return DataSpace.SSpace;
			case MetadataItem.MetadataFlags.CSSpace:
				return DataSpace.CSSpace;
			default:
				return (DataSpace)(-1);
			}
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x0007137D File Offset: 0x0006F57D
		internal void SetDataSpace(DataSpace space)
		{
			this._flags = ((this._flags & ~(MetadataItem.MetadataFlags.CSpace | MetadataItem.MetadataFlags.OSpace | MetadataItem.MetadataFlags.SSpace)) | (MetadataItem.MetadataFlags.DataSpace & MetadataItem.Convert(space)));
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x00071397 File Offset: 0x0006F597
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

		// Token: 0x06002090 RID: 8336 RVA: 0x000713C0 File Offset: 0x0006F5C0
		internal ParameterMode GetParameterMode()
		{
			MetadataItem.MetadataFlags metadataFlags = this._flags & MetadataItem.MetadataFlags.ParameterMode;
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

		// Token: 0x06002091 RID: 8337 RVA: 0x0007140D File Offset: 0x0006F60D
		internal void SetParameterMode(ParameterMode mode)
		{
			this._flags = ((this._flags & ~(MetadataItem.MetadataFlags.In | MetadataItem.MetadataFlags.Out | MetadataItem.MetadataFlags.ReturnValue)) | (MetadataItem.MetadataFlags.ParameterMode & MetadataItem.Convert(mode)));
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x0007142E File Offset: 0x0006F62E
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

		// Token: 0x06002093 RID: 8339 RVA: 0x00071463 File Offset: 0x0006F663
		internal bool GetFlag(MetadataItem.MetadataFlags flag)
		{
			return flag == (this._flags & flag);
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x00071470 File Offset: 0x0006F670
		internal void SetFlag(MetadataItem.MetadataFlags flag, bool value)
		{
			MetadataItem.MetadataFlags metadataFlags = flag & MetadataItem.MetadataFlags.Readonly;
			object flagsLock = this._flagsLock;
			lock (flagsLock)
			{
				if (!this.IsReadOnly || (flag & MetadataItem.MetadataFlags.Readonly) != MetadataItem.MetadataFlags.Readonly)
				{
					Util.ThrowIfReadOnly(this);
					if (value)
					{
						this._flags |= flag;
					}
					else
					{
						this._flags &= ~flag;
					}
				}
			}
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000714E8 File Offset: 0x0006F6E8
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
				"Cascade",
				"Restrict"
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
			MetadataItem._generalFacetDescriptions = Array.AsReadOnly<FacetDescription>(array);
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

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06002096 RID: 8342 RVA: 0x0007229A File Offset: 0x0007049A
		internal static FacetDescription DefaultValueFacetDescription
		{
			get
			{
				return MetadataItem._defaultValueFacetDescription;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x000722A1 File Offset: 0x000704A1
		internal static FacetDescription CollectionKindFacetDescription
		{
			get
			{
				return MetadataItem._collectionKindFacetDescription;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06002098 RID: 8344 RVA: 0x000722A8 File Offset: 0x000704A8
		internal static FacetDescription NullableFacetDescription
		{
			get
			{
				return MetadataItem._nullableFacetDescription;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x000722AF File Offset: 0x000704AF
		internal static EdmProviderManifest EdmProviderManifest
		{
			get
			{
				return EdmProviderManifest.Instance;
			}
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000722B6 File Offset: 0x000704B6
		public static EdmType GetBuiltInType(BuiltInTypeKind builtInTypeKind)
		{
			return MetadataItem._builtInTypes[(int)builtInTypeKind];
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x000722BF File Offset: 0x000704BF
		public static ReadOnlyCollection<FacetDescription> GetGeneralFacetDescriptions()
		{
			return MetadataItem._generalFacetDescriptions;
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000722C6 File Offset: 0x000704C6
		private static void InitializeBuiltInTypes(ComplexType builtInType, string name, bool isAbstract, ComplexType baseType)
		{
			EdmType.Initialize(builtInType, name, "Edm", DataSpace.CSpace, isAbstract, baseType);
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000722D8 File Offset: 0x000704D8
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

		// Token: 0x0600209E RID: 8350 RVA: 0x0007230C File Offset: 0x0007050C
		private static void InitializeEnumType(BuiltInTypeKind builtInTypeKind, string name, string[] enumMemberNames)
		{
			EnumType enumType = (EnumType)MetadataItem.GetBuiltInType(builtInTypeKind);
			EdmType.Initialize(enumType, name, "Edm", DataSpace.CSpace, false, null);
			for (int i = 0; i < enumMemberNames.Length; i++)
			{
				enumType.AddMember(new EnumMember(enumMemberNames[i], i));
			}
		}

		// Token: 0x04000E47 RID: 3655
		private MetadataItem.MetadataFlags _flags;

		// Token: 0x04000E48 RID: 3656
		private object _flagsLock = new object();

		// Token: 0x04000E49 RID: 3657
		private MetadataCollection<MetadataProperty> _itemAttributes;

		// Token: 0x04000E4A RID: 3658
		private Documentation _documentation;

		// Token: 0x04000E4B RID: 3659
		private static EdmType[] _builtInTypes = new EdmType[40];

		// Token: 0x04000E4C RID: 3660
		private static readonly ReadOnlyCollection<FacetDescription> _generalFacetDescriptions;

		// Token: 0x04000E4D RID: 3661
		private static FacetDescription _nullableFacetDescription;

		// Token: 0x04000E4E RID: 3662
		private static FacetDescription _defaultValueFacetDescription;

		// Token: 0x04000E4F RID: 3663
		private static FacetDescription _collectionKindFacetDescription;

		// Token: 0x0200051C RID: 1308
		[Flags]
		internal enum MetadataFlags
		{
			// Token: 0x04001B32 RID: 6962
			None = 0,
			// Token: 0x04001B33 RID: 6963
			CSpace = 1,
			// Token: 0x04001B34 RID: 6964
			OSpace = 2,
			// Token: 0x04001B35 RID: 6965
			OCSpace = 3,
			// Token: 0x04001B36 RID: 6966
			SSpace = 4,
			// Token: 0x04001B37 RID: 6967
			CSSpace = 5,
			// Token: 0x04001B38 RID: 6968
			DataSpace = 7,
			// Token: 0x04001B39 RID: 6969
			Readonly = 8,
			// Token: 0x04001B3A RID: 6970
			IsAbstract = 16,
			// Token: 0x04001B3B RID: 6971
			In = 512,
			// Token: 0x04001B3C RID: 6972
			Out = 1024,
			// Token: 0x04001B3D RID: 6973
			InOut = 1536,
			// Token: 0x04001B3E RID: 6974
			ReturnValue = 2048,
			// Token: 0x04001B3F RID: 6975
			ParameterMode = 3584
		}
	}
}
