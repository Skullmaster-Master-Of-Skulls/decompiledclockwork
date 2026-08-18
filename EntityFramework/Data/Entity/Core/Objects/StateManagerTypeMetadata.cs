using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005B1 RID: 1457
	internal class StateManagerTypeMetadata
	{
		// Token: 0x06003A60 RID: 14944 RVA: 0x00115C09 File Offset: 0x00113E09
		internal StateManagerTypeMetadata()
		{
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x00115C14 File Offset: 0x00113E14
		internal StateManagerTypeMetadata(EdmType edmType, ObjectTypeMapping mapping)
		{
			this._typeUsage = TypeUsage.Create(edmType);
			this._recordInfo = new DataRecordInfo(this._typeUsage);
			ReadOnlyMetadataCollection<EdmProperty> properties = TypeHelpers.GetProperties(edmType);
			this._members = new StateManagerMemberMetadata[properties.Count];
			this._objectNameToOrdinal = new Dictionary<string, int>(properties.Count);
			this._cLayerNameToOrdinal = new Dictionary<string, int>(properties.Count);
			ReadOnlyMetadataCollection<EdmMember> readOnlyMetadataCollection = null;
			if (Helper.IsEntityType(edmType))
			{
				readOnlyMetadataCollection = ((EntityType)edmType).KeyMembers;
			}
			for (int i = 0; i < this._members.Length; i++)
			{
				EdmProperty edmProperty = properties[i];
				ObjectPropertyMapping objectPropertyMapping = null;
				if (mapping != null)
				{
					objectPropertyMapping = mapping.GetPropertyMap(edmProperty.Name);
					if (objectPropertyMapping != null)
					{
						this._objectNameToOrdinal.Add(objectPropertyMapping.ClrProperty.Name, i);
					}
				}
				this._cLayerNameToOrdinal.Add(edmProperty.Name, i);
				this._members[i] = new StateManagerMemberMetadata(objectPropertyMapping, edmProperty, readOnlyMetadataCollection != null && readOnlyMetadataCollection.Contains(edmProperty));
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x00115D0E File Offset: 0x00113F0E
		internal TypeUsage CdmMetadata
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06003A63 RID: 14947 RVA: 0x00115D16 File Offset: 0x00113F16
		internal DataRecordInfo DataRecordInfo
		{
			get
			{
				return this._recordInfo;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06003A64 RID: 14948 RVA: 0x00115D1E File Offset: 0x00113F1E
		internal virtual int FieldCount
		{
			get
			{
				return this._members.Length;
			}
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x00115D28 File Offset: 0x00113F28
		internal Type GetFieldType(int ordinal)
		{
			return this.Member(ordinal).ClrType;
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x00115D36 File Offset: 0x00113F36
		internal virtual StateManagerMemberMetadata Member(int ordinal)
		{
			if (ordinal < this._members.Length)
			{
				return this._members[ordinal];
			}
			throw new ArgumentOutOfRangeException("ordinal");
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06003A67 RID: 14951 RVA: 0x00115D56 File Offset: 0x00113F56
		internal IEnumerable<StateManagerMemberMetadata> Members
		{
			get
			{
				return this._members;
			}
		}

		// Token: 0x06003A68 RID: 14952 RVA: 0x00115D5E File Offset: 0x00113F5E
		internal string CLayerMemberName(int ordinal)
		{
			return this.Member(ordinal).CLayerName;
		}

		// Token: 0x06003A69 RID: 14953 RVA: 0x00115D6C File Offset: 0x00113F6C
		internal int GetOrdinalforOLayerMemberName(string name)
		{
			int result;
			if (string.IsNullOrEmpty(name) || !this._objectNameToOrdinal.TryGetValue(name, out result))
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x06003A6A RID: 14954 RVA: 0x00115D94 File Offset: 0x00113F94
		internal int GetOrdinalforCLayerMemberName(string name)
		{
			int result;
			if (string.IsNullOrEmpty(name) || !this._cLayerNameToOrdinal.TryGetValue(name, out result))
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x04001619 RID: 5657
		private readonly TypeUsage _typeUsage;

		// Token: 0x0400161A RID: 5658
		private readonly StateManagerMemberMetadata[] _members;

		// Token: 0x0400161B RID: 5659
		private readonly Dictionary<string, int> _objectNameToOrdinal;

		// Token: 0x0400161C RID: 5660
		private readonly Dictionary<string, int> _cLayerNameToOrdinal;

		// Token: 0x0400161D RID: 5661
		private readonly DataRecordInfo _recordInfo;
	}
}
