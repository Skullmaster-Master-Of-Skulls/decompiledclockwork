using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000480 RID: 1152
	internal sealed class MemberProjectionIndex : InternalBase
	{
		// Token: 0x06002A8C RID: 10892 RVA: 0x000CDA18 File Offset: 0x000CBC18
		internal static MemberProjectionIndex Create(EntitySetBase extent, EdmItemCollection edmItemCollection)
		{
			MemberProjectionIndex memberProjectionIndex = new MemberProjectionIndex();
			MemberProjectionIndex.GatherPartialSignature(memberProjectionIndex, edmItemCollection, new MemberPath(extent), false);
			return memberProjectionIndex;
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x000CDA3A File Offset: 0x000CBC3A
		private MemberProjectionIndex()
		{
			this.m_indexMap = new Dictionary<MemberPath, int>(MemberPath.EqualityComparer);
			this.m_members = new List<MemberPath>();
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06002A8E RID: 10894 RVA: 0x000CDA5D File Offset: 0x000CBC5D
		internal int Count
		{
			get
			{
				return this.m_members.Count;
			}
		}

		// Token: 0x170005DD RID: 1501
		internal MemberPath this[int index]
		{
			get
			{
				return this.m_members[index];
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06002A90 RID: 10896 RVA: 0x000CDA78 File Offset: 0x000CBC78
		internal IEnumerable<int> KeySlots
		{
			get
			{
				List<int> list = new List<int>();
				for (int i = 0; i < this.Count; i++)
				{
					if (this.IsKeySlot(i, 0))
					{
						list.Add(i);
					}
				}
				return list;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06002A91 RID: 10897 RVA: 0x000CDAAE File Offset: 0x000CBCAE
		internal IEnumerable<MemberPath> Members
		{
			get
			{
				return this.m_members;
			}
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x000CDAB8 File Offset: 0x000CBCB8
		internal int IndexOf(MemberPath member)
		{
			int result;
			if (this.m_indexMap.TryGetValue(member, out result))
			{
				return result;
			}
			return -1;
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x000CDAD8 File Offset: 0x000CBCD8
		internal int CreateIndex(MemberPath member)
		{
			int count;
			if (!this.m_indexMap.TryGetValue(member, out count))
			{
				count = this.m_indexMap.Count;
				this.m_indexMap[member] = count;
				this.m_members.Add(member);
			}
			return count;
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x000CDB1C File Offset: 0x000CBD1C
		internal MemberPath GetMemberPath(int slotNum, int numBoolSlots)
		{
			return this.IsBoolSlot(slotNum, numBoolSlots) ? null : this[slotNum];
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x000CDB3F File Offset: 0x000CBD3F
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "numBoolSlots")]
		internal int BoolIndexToSlot(int boolIndex, int numBoolSlots)
		{
			return this.Count + boolIndex;
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x000CDB49 File Offset: 0x000CBD49
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "numBoolSlots")]
		internal int SlotToBoolIndex(int slotNum, int numBoolSlots)
		{
			return slotNum - this.Count;
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x000CDB53 File Offset: 0x000CBD53
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "numBoolSlots")]
		internal bool IsKeySlot(int slotNum, int numBoolSlots)
		{
			return slotNum < this.Count && this[slotNum].IsPartOfKey;
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x000CDB6C File Offset: 0x000CBD6C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "numBoolSlots")]
		internal bool IsBoolSlot(int slotNum, int numBoolSlots)
		{
			return slotNum >= this.Count;
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x000CDB7A File Offset: 0x000CBD7A
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append('<');
			StringUtil.ToCommaSeparatedString(builder, this.m_members);
			builder.Append('>');
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x000CDB9C File Offset: 0x000CBD9C
		private static void GatherPartialSignature(MemberProjectionIndex index, EdmItemCollection edmItemCollection, MemberPath member, bool needKeysOnly)
		{
			EdmType edmType = member.EdmType;
			if (edmType is ComplexType && needKeysOnly)
			{
				return;
			}
			index.CreateIndex(member);
			foreach (EdmType edmType2 in MetadataHelper.GetTypeAndSubtypesOf(edmType, edmItemCollection, false))
			{
				StructuralType possibleType = edmType2 as StructuralType;
				MemberProjectionIndex.GatherSignatureFromTypeStructuralMembers(index, edmItemCollection, member, possibleType, needKeysOnly);
			}
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x000CDC10 File Offset: 0x000CBE10
		private static void GatherSignatureFromTypeStructuralMembers(MemberProjectionIndex index, EdmItemCollection edmItemCollection, MemberPath member, StructuralType possibleType, bool needKeysOnly)
		{
			foreach (object obj in Helper.GetAllStructuralMembers(possibleType))
			{
				EdmMember edmMember = (EdmMember)obj;
				if (MetadataHelper.IsNonRefSimpleMember(edmMember))
				{
					if (!needKeysOnly || MetadataHelper.IsPartOfEntityTypeKey(edmMember))
					{
						MemberPath member2 = new MemberPath(member, edmMember);
						index.CreateIndex(member2);
					}
				}
				else
				{
					MemberPath member3 = new MemberPath(member, edmMember);
					MemberProjectionIndex.GatherPartialSignature(index, edmItemCollection, member3, needKeysOnly || Helper.IsAssociationEndMember(edmMember));
				}
			}
		}

		// Token: 0x04000FAF RID: 4015
		private readonly Dictionary<MemberPath, int> m_indexMap;

		// Token: 0x04000FB0 RID: 4016
		private readonly List<MemberPath> m_members;
	}
}
