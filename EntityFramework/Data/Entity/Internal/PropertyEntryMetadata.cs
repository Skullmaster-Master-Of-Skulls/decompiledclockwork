using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000781 RID: 1921
	internal class PropertyEntryMetadata : MemberEntryMetadata
	{
		// Token: 0x060056FC RID: 22268 RVA: 0x00177FC6 File Offset: 0x001761C6
		public PropertyEntryMetadata(Type declaringType, Type propertyType, string propertyName, bool isMapped, bool isComplex) : base(declaringType, propertyType, propertyName)
		{
			this._isMapped = isMapped;
			this._isComplex = isComplex;
		}

		// Token: 0x060056FD RID: 22269 RVA: 0x00177FE4 File Offset: 0x001761E4
		public static PropertyEntryMetadata ValidateNameAndGetMetadata(InternalContext internalContext, Type declaringType, Type requestedType, string propertyName)
		{
			Type type;
			DbHelpers.GetPropertyTypes(declaringType).TryGetValue(propertyName, out type);
			MetadataWorkspace metadataWorkspace = internalContext.ObjectContext.MetadataWorkspace;
			StructuralType item = metadataWorkspace.GetItem<StructuralType>(declaringType.FullNameWithNesting(), DataSpace.OSpace);
			bool isMapped = false;
			bool isComplex = false;
			EdmMember edmMember;
			item.Members.TryGetValue(propertyName, false, out edmMember);
			if (edmMember != null)
			{
				EdmProperty edmProperty = edmMember as EdmProperty;
				if (edmProperty == null)
				{
					return null;
				}
				if (type == null)
				{
					PrimitiveType primitiveType = edmProperty.TypeUsage.EdmType as PrimitiveType;
					if (primitiveType != null)
					{
						type = primitiveType.ClrEquivalentType;
					}
					else
					{
						ObjectItemCollection objectItemCollection = (ObjectItemCollection)metadataWorkspace.GetItemCollection(DataSpace.OSpace);
						type = objectItemCollection.GetClrType((StructuralType)edmProperty.TypeUsage.EdmType);
					}
				}
				isMapped = true;
				isComplex = (edmProperty.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.ComplexType);
			}
			else
			{
				IDictionary<string, Func<object, object>> propertyGetters = DbHelpers.GetPropertyGetters(declaringType);
				IDictionary<string, Action<object, object>> propertySetters = DbHelpers.GetPropertySetters(declaringType);
				if (!propertyGetters.ContainsKey(propertyName) && !propertySetters.ContainsKey(propertyName))
				{
					return null;
				}
			}
			if (!requestedType.IsAssignableFrom(type))
			{
				throw Error.DbEntityEntry_WrongGenericForProp(propertyName, declaringType.Name, requestedType.Name, type.Name);
			}
			return new PropertyEntryMetadata(declaringType, type, propertyName, isMapped, isComplex);
		}

		// Token: 0x060056FE RID: 22270 RVA: 0x00178100 File Offset: 0x00176300
		public override InternalMemberEntry CreateMemberEntry(InternalEntityEntry internalEntityEntry, InternalPropertyEntry parentPropertyEntry)
		{
			if (parentPropertyEntry != null)
			{
				return new InternalNestedPropertyEntry(parentPropertyEntry, this);
			}
			return new InternalEntityPropertyEntry(internalEntityEntry, this);
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x060056FF RID: 22271 RVA: 0x00178114 File Offset: 0x00176314
		public bool IsComplex
		{
			get
			{
				return this._isComplex;
			}
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06005700 RID: 22272 RVA: 0x0017811C File Offset: 0x0017631C
		public override MemberEntryType MemberEntryType
		{
			get
			{
				if (!this._isComplex)
				{
					return MemberEntryType.ScalarProperty;
				}
				return MemberEntryType.ComplexProperty;
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06005701 RID: 22273 RVA: 0x00178129 File Offset: 0x00176329
		public bool IsMapped
		{
			get
			{
				return this._isMapped;
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06005702 RID: 22274 RVA: 0x00178131 File Offset: 0x00176331
		public override Type MemberType
		{
			get
			{
				return base.ElementType;
			}
		}

		// Token: 0x0400231E RID: 8990
		private readonly bool _isMapped;

		// Token: 0x0400231F RID: 8991
		private readonly bool _isComplex;
	}
}
