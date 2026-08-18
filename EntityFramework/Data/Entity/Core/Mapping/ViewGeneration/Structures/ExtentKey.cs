using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000497 RID: 1175
	internal class ExtentKey : InternalBase
	{
		// Token: 0x06002B54 RID: 11092 RVA: 0x000D205E File Offset: 0x000D025E
		internal ExtentKey(IEnumerable<MemberPath> keyFields)
		{
			this.m_keyFields = new List<MemberPath>(keyFields);
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06002B55 RID: 11093 RVA: 0x000D2072 File Offset: 0x000D0272
		internal IEnumerable<MemberPath> KeyFields
		{
			get
			{
				return this.m_keyFields;
			}
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000D207C File Offset: 0x000D027C
		internal static List<ExtentKey> GetKeysForEntityType(MemberPath prefix, EntityType entityType)
		{
			ExtentKey primaryKeyForEntityType = ExtentKey.GetPrimaryKeyForEntityType(prefix, entityType);
			return new List<ExtentKey>
			{
				primaryKeyForEntityType
			};
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x000D20A0 File Offset: 0x000D02A0
		internal static ExtentKey GetPrimaryKeyForEntityType(MemberPath prefix, EntityType entityType)
		{
			List<MemberPath> list = new List<MemberPath>();
			foreach (EdmMember last in entityType.KeyMembers)
			{
				list.Add(new MemberPath(prefix, last));
			}
			return new ExtentKey(list);
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x000D2108 File Offset: 0x000D0308
		internal static ExtentKey GetKeyForRelationType(MemberPath prefix, AssociationType relationType)
		{
			List<MemberPath> list = new List<MemberPath>();
			foreach (AssociationEndMember associationEndMember in relationType.AssociationEndMembers)
			{
				MemberPath prefix2 = new MemberPath(prefix, associationEndMember);
				EntityType entityTypeForEnd = MetadataHelper.GetEntityTypeForEnd(associationEndMember);
				ExtentKey primaryKeyForEntityType = ExtentKey.GetPrimaryKeyForEntityType(prefix2, entityTypeForEnd);
				list.AddRange(primaryKeyForEntityType.KeyFields);
			}
			return new ExtentKey(list);
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x000D218C File Offset: 0x000D038C
		internal string ToUserString()
		{
			return StringUtil.ToCommaSeparatedStringSorted(this.m_keyFields);
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x000D21A6 File Offset: 0x000D03A6
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.ToCommaSeparatedStringSorted(builder, this.m_keyFields);
		}

		// Token: 0x04001003 RID: 4099
		private readonly List<MemberPath> m_keyFields;
	}
}
