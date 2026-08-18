using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002B1 RID: 689
	internal sealed class MemberProjectionIndex : InternalBase
	{
		// Token: 0x0600290B RID: 10507 RVA: 0x0009F248 File Offset: 0x0009D448
		internal static MemberProjectionIndex Create(EntitySetBase extent, EdmItemCollection edmItemCollection)
		{
			MemberProjectionIndex memberProjectionIndex = new MemberProjectionIndex();
			MemberProjectionIndex.GatherPartialSignature(memberProjectionIndex, edmItemCollection, new MemberPath(extent), false);
			return memberProjectionIndex;
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x0009F26A File Offset: 0x0009D46A
		private MemberProjectionIndex()
		{
			this.m_indexMap = new Dictionary<MemberPath, int>(MemberPath.EqualityComparer);
			this.m_members = new List<MemberPath>();
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x0600290D RID: 10509 RVA: 0x0009F28D File Offset: 0x0009D48D
		internal int Count
		{
			get
			{
				return this.m_members.Count;
			}
		}

		// Token: 0x17000814 RID: 2068
		internal MemberPath this[int index]
		{
			get
			{
				return this.m_members[index];
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x0600290F RID: 10511 RVA: 0x0009F2A8 File Offset: 0x0009D4A8
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

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06002910 RID: 10512 RVA: 0x0009F2DE File Offset: 0x0009D4DE
		internal IEnumerable<MemberPath> Members
		{
			get
			{
				return this.m_members;
			}
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x0009F2E8 File Offset: 0x0009D4E8
		internal int IndexOf(MemberPath member)
		{
			int result;
			if (this.m_indexMap.TryGetValue(member, out result))
			{
				return result;
			}
			return -1;
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x0009F308 File Offset: 0x0009D508
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

		// Token: 0x06002913 RID: 10515 RVA: 0x0009F34C File Offset: 0x0009D54C
		internal MemberPath GetMemberPath(int slotNum, int numBoolSlots)
		{
			return this.IsBoolSlot(slotNum, numBoolSlots) ? null : this[slotNum];
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x0009F36F File Offset: 0x0009D56F
		internal int BoolIndexToSlot(int boolIndex, int numBoolSlots)
		{
			return this.Count + boolIndex;
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x0009F379 File Offset: 0x0009D579
		internal int SlotToBoolIndex(int slotNum, int numBoolSlots)
		{
			return slotNum - this.Count;
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x0009F383 File Offset: 0x0009D583
		internal bool IsKeySlot(int slotNum, int numBoolSlots)
		{
			return slotNum < this.Count && this[slotNum].IsPartOfKey;
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x0009F39C File Offset: 0x0009D59C
		internal bool IsBoolSlot(int slotNum, int numBoolSlots)
		{
			return slotNum >= this.Count;
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x0009F3AA File Offset: 0x0009D5AA
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append('<');
			StringUtil.ToCommaSeparatedString(builder, this.m_members);
			builder.Append('>');
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x0009F3CC File Offset: 0x0009D5CC
		private static void GatherPartialSignature(MemberProjectionIndex index, EdmItemCollection edmItemCollection, MemberPath member, bool needKeysOnly)
		{
			EdmType edmType = member.EdmType;
			ComplexType complexType = edmType as ComplexType;
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

		// Token: 0x0600291A RID: 10522 RVA: 0x0009F44C File Offset: 0x0009D64C
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

		// Token: 0x04001277 RID: 4727
		private readonly Dictionary<MemberPath, int> m_indexMap;

		// Token: 0x04001278 RID: 4728
		private readonly List<MemberPath> m_members;
	}
}
