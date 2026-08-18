using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Mapping;
using System.Data.Metadata.Edm;

namespace System.Data.Objects
{
	// Token: 0x0200014C RID: 332
	internal sealed class StateManagerTypeMetadata
	{
		// Token: 0x06001850 RID: 6224 RVA: 0x00053634 File Offset: 0x00051834
		internal StateManagerTypeMetadata(EdmType edmType, ObjectTypeMapping mapping)
		{
			this._typeUsage = TypeUsage.Create(edmType);
			this._recordInfo = new DataRecordInfo(this._typeUsage);
			this._ocObjectMap = mapping;
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

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x00053735 File Offset: 0x00051935
		internal TypeUsage CdmMetadata
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x0005373D File Offset: 0x0005193D
		internal DataRecordInfo DataRecordInfo
		{
			get
			{
				return this._recordInfo;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x00053745 File Offset: 0x00051945
		internal int FieldCount
		{
			get
			{
				return this._members.Length;
			}
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0005374F File Offset: 0x0005194F
		internal Type GetFieldType(int ordinal)
		{
			return this.Member(ordinal).ClrType;
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0005375D File Offset: 0x0005195D
		internal StateManagerMemberMetadata Member(int ordinal)
		{
			if (ordinal < this._members.Length)
			{
				return this._members[ordinal];
			}
			throw EntityUtil.ArgumentOutOfRange("ordinal");
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x0005377D File Offset: 0x0005197D
		internal IEnumerable<StateManagerMemberMetadata> Members
		{
			get
			{
				return this._members;
			}
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00053785 File Offset: 0x00051985
		internal string CLayerMemberName(int ordinal)
		{
			return this.Member(ordinal).CLayerName;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00053794 File Offset: 0x00051994
		internal int GetOrdinalforOLayerMemberName(string name)
		{
			int result;
			if (string.IsNullOrEmpty(name) || !this._objectNameToOrdinal.TryGetValue(name, out result))
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x000537BC File Offset: 0x000519BC
		internal int GetOrdinalforCLayerMemberName(string name)
		{
			int result;
			if (string.IsNullOrEmpty(name) || !this._cLayerNameToOrdinal.TryGetValue(name, out result))
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x000537E4 File Offset: 0x000519E4
		internal bool IsMemberPartofShadowState(int ordinal)
		{
			return this.Member(ordinal).ClrMetadata == null;
		}

		// Token: 0x04000ABD RID: 2749
		private readonly TypeUsage _typeUsage;

		// Token: 0x04000ABE RID: 2750
		private readonly ObjectTypeMapping _ocObjectMap;

		// Token: 0x04000ABF RID: 2751
		private readonly StateManagerMemberMetadata[] _members;

		// Token: 0x04000AC0 RID: 2752
		private readonly Dictionary<string, int> _objectNameToOrdinal;

		// Token: 0x04000AC1 RID: 2753
		private readonly Dictionary<string, int> _cLayerNameToOrdinal;

		// Token: 0x04000AC2 RID: 2754
		private readonly DataRecordInfo _recordInfo;
	}
}
