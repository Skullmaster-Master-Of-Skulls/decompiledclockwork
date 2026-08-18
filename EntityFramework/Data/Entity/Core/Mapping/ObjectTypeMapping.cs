using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003D0 RID: 976
	internal class ObjectTypeMapping : MappingBase
	{
		// Token: 0x06002379 RID: 9081 RVA: 0x000A5270 File Offset: 0x000A3470
		internal ObjectTypeMapping(EdmType clrType, EdmType cdmType)
		{
			this.m_clrType = clrType;
			this.m_cdmType = cdmType;
			this.identity = clrType.Identity + ':' + cdmType.Identity;
			if (Helper.IsStructuralType(cdmType))
			{
				this.m_memberMapping = new Dictionary<string, ObjectMemberMapping>(((StructuralType)cdmType).Members.Count);
				return;
			}
			this.m_memberMapping = ObjectTypeMapping.EmptyMemberMapping;
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600237A RID: 9082 RVA: 0x000A52DE File Offset: 0x000A34DE
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.MetadataItem;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x000A52E2 File Offset: 0x000A34E2
		internal EdmType ClrType
		{
			get
			{
				return this.m_clrType;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x000A52EA File Offset: 0x000A34EA
		internal override MetadataItem EdmItem
		{
			get
			{
				return this.EdmType;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x000A52F2 File Offset: 0x000A34F2
		internal EdmType EdmType
		{
			get
			{
				return this.m_cdmType;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x000A52FA File Offset: 0x000A34FA
		internal override string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x000A5304 File Offset: 0x000A3504
		internal ObjectPropertyMapping GetPropertyMap(string propertyName)
		{
			ObjectMemberMapping memberMap = this.GetMemberMap(propertyName, false);
			if ((memberMap != null && memberMap.MemberMappingKind == MemberMappingKind.ScalarPropertyMapping) || memberMap.MemberMappingKind == MemberMappingKind.ComplexPropertyMapping)
			{
				return (ObjectPropertyMapping)memberMap;
			}
			return null;
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x000A5336 File Offset: 0x000A3536
		internal void AddMemberMap(ObjectMemberMapping memberMapping)
		{
			this.m_memberMapping.Add(memberMapping.EdmMember.Name, memberMapping);
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x000A534F File Offset: 0x000A354F
		internal ObjectMemberMapping GetMemberMapForClrMember(string clrMemberName, bool ignoreCase)
		{
			return this.GetMemberMap(clrMemberName, ignoreCase);
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x000A535C File Offset: 0x000A355C
		private ObjectMemberMapping GetMemberMap(string propertyName, bool ignoreCase)
		{
			Check.NotEmpty(propertyName, "propertyName");
			ObjectMemberMapping objectMemberMapping = null;
			if (!ignoreCase)
			{
				this.m_memberMapping.TryGetValue(propertyName, out objectMemberMapping);
			}
			else
			{
				foreach (KeyValuePair<string, ObjectMemberMapping> keyValuePair in this.m_memberMapping)
				{
					if (keyValuePair.Key.Equals(propertyName, StringComparison.OrdinalIgnoreCase))
					{
						if (objectMemberMapping != null)
						{
							throw new MappingException(Strings.Mapping_Duplicate_PropertyMap_CaseInsensitive(propertyName));
						}
						objectMemberMapping = keyValuePair.Value;
					}
				}
			}
			return objectMemberMapping;
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x000A53F4 File Offset: 0x000A35F4
		public override string ToString()
		{
			return this.Identity;
		}

		// Token: 0x04000C7C RID: 3196
		private readonly EdmType m_clrType;

		// Token: 0x04000C7D RID: 3197
		private readonly EdmType m_cdmType;

		// Token: 0x04000C7E RID: 3198
		private readonly string identity;

		// Token: 0x04000C7F RID: 3199
		private readonly Dictionary<string, ObjectMemberMapping> m_memberMapping;

		// Token: 0x04000C80 RID: 3200
		private static readonly Dictionary<string, ObjectMemberMapping> EmptyMemberMapping = new Dictionary<string, ObjectMemberMapping>(0);
	}
}
