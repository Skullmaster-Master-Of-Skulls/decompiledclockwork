using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002BB RID: 699
	internal class ExtentKey : InternalBase
	{
		// Token: 0x0600299B RID: 10651 RVA: 0x000A1708 File Offset: 0x0009F908
		internal ExtentKey(IEnumerable<MemberPath> keyFields)
		{
			this.m_keyFields = new List<MemberPath>(keyFields);
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x000A171C File Offset: 0x0009F91C
		internal IEnumerable<MemberPath> KeyFields
		{
			get
			{
				return this.m_keyFields;
			}
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x000A1724 File Offset: 0x0009F924
		internal static List<ExtentKey> GetKeysForEntityType(MemberPath prefix, EntityType entityType)
		{
			ExtentKey primaryKeyForEntityType = ExtentKey.GetPrimaryKeyForEntityType(prefix, entityType);
			return new List<ExtentKey>
			{
				primaryKeyForEntityType
			};
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x000A1748 File Offset: 0x0009F948
		internal static ExtentKey GetPrimaryKeyForEntityType(MemberPath prefix, EntityType entityType)
		{
			List<MemberPath> list = new List<MemberPath>();
			foreach (EdmMember last in entityType.KeyMembers)
			{
				list.Add(new MemberPath(prefix, last));
			}
			return new ExtentKey(list);
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x000A17B0 File Offset: 0x0009F9B0
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

		// Token: 0x060029A0 RID: 10656 RVA: 0x000A1834 File Offset: 0x0009FA34
		internal string ToUserString()
		{
			return StringUtil.ToCommaSeparatedStringSorted(this.m_keyFields);
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x000A184E File Offset: 0x0009FA4E
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.ToCommaSeparatedStringSorted(builder, this.m_keyFields);
		}

		// Token: 0x04001286 RID: 4742
		private List<MemberPath> m_keyFields;
	}
}
