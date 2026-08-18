using System;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Linq;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002C2 RID: 706
	internal class ExtractorMetadata
	{
		// Token: 0x060029D0 RID: 10704 RVA: 0x000A3298 File Offset: 0x000A1498
		internal ExtractorMetadata(EntitySetBase entitySetBase, StructuralType type, UpdateTranslator translator)
		{
			EntityUtil.CheckArgumentNull<EntitySetBase>(entitySetBase, "entitySetBase");
			this.m_type = EntityUtil.CheckArgumentNull<StructuralType>(type, "type");
			this.m_translator = EntityUtil.CheckArgumentNull<UpdateTranslator>(translator, "translator");
			EntityType entityType = null;
			BuiltInTypeKind builtInTypeKind = type.BuiltInTypeKind;
			Set<EdmMember> set;
			Set<EdmMember> set2;
			if (builtInTypeKind != BuiltInTypeKind.EntityType)
			{
				if (builtInTypeKind == BuiltInTypeKind.RowType)
				{
					set = new Set<EdmMember>(((RowType)type).Properties).MakeReadOnly();
					set2 = Set<EdmMember>.Empty;
				}
				else
				{
					set = Set<EdmMember>.Empty;
					set2 = Set<EdmMember>.Empty;
				}
			}
			else
			{
				entityType = (EntityType)type;
				set = new Set<EdmMember>(entityType.KeyMembers).MakeReadOnly();
				set2 = new Set<EdmMember>(((EntitySet)entitySetBase).ForeignKeyDependents.SelectMany((Tuple<AssociationSet, ReferentialConstraint> fk) => fk.Item2.ToProperties)).MakeReadOnly();
			}
			IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(type);
			this.m_memberMap = new ExtractorMetadata.MemberInformation[allStructuralMembers.Count];
			for (int i = 0; i < allStructuralMembers.Count; i++)
			{
				EdmMember edmMember = allStructuralMembers[i];
				PropagatorFlags propagatorFlags = PropagatorFlags.NoFlags;
				int? entityKeyOrdinal = null;
				if (set.Contains(edmMember))
				{
					propagatorFlags |= PropagatorFlags.Key;
					if (entityType != null)
					{
						entityKeyOrdinal = new int?(entityType.KeyMembers.IndexOf(edmMember));
					}
				}
				if (set2.Contains(edmMember))
				{
					propagatorFlags |= PropagatorFlags.ForeignKey;
				}
				if (MetadataHelper.GetConcurrencyMode(edmMember) == ConcurrencyMode.Fixed)
				{
					propagatorFlags |= PropagatorFlags.ConcurrencyValue;
				}
				bool isServerGenerated = this.m_translator.ViewLoader.IsServerGen(entitySetBase, this.m_translator.MetadataWorkspace, edmMember);
				bool isNullConditionMember = this.m_translator.ViewLoader.IsNullConditionMember(entitySetBase, this.m_translator.MetadataWorkspace, edmMember);
				this.m_memberMap[i] = new ExtractorMetadata.MemberInformation(i, entityKeyOrdinal, propagatorFlags, edmMember, isServerGenerated, isNullConditionMember);
			}
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x000A3458 File Offset: 0x000A1658
		internal PropagatorResult RetrieveMember(IEntityStateEntry stateEntry, IExtendedDataRecord record, bool useCurrentValues, EntityKey key, int ordinal, ModifiedPropertiesBehavior modifiedPropertiesBehavior)
		{
			ExtractorMetadata.MemberInformation memberInformation = this.m_memberMap[ordinal];
			int identifier;
			if (memberInformation.IsKeyMember)
			{
				int value = memberInformation.EntityKeyOrdinal.Value;
				identifier = this.m_translator.KeyManager.GetKeyIdentifierForMemberOffset(key, value, ((EntityType)this.m_type).KeyMembers.Count);
			}
			else if (memberInformation.IsForeignKeyMember)
			{
				identifier = this.m_translator.KeyManager.GetKeyIdentifierForMember(key, record.GetName(ordinal), useCurrentValues);
			}
			else
			{
				identifier = -1;
			}
			bool flag = modifiedPropertiesBehavior == ModifiedPropertiesBehavior.AllModified || (modifiedPropertiesBehavior == ModifiedPropertiesBehavior.SomeModified && stateEntry.ModifiedProperties != null && stateEntry.ModifiedProperties[memberInformation.Ordinal]);
			if (memberInformation.CheckIsNotNull && record.IsDBNull(ordinal))
			{
				throw EntityUtil.Update(Strings.Update_NullValue(record.GetName(ordinal)), null, new IEntityStateEntry[]
				{
					stateEntry
				});
			}
			object value2 = record.GetValue(ordinal);
			EntityKey entityKey = value2 as EntityKey;
			if (entityKey != null)
			{
				return this.CreateEntityKeyResult(stateEntry, entityKey);
			}
			IExtendedDataRecord extendedDataRecord = value2 as IExtendedDataRecord;
			if (extendedDataRecord != null)
			{
				ModifiedPropertiesBehavior modifiedPropertiesBehavior2 = flag ? ModifiedPropertiesBehavior.AllModified : ModifiedPropertiesBehavior.NoneModified;
				UpdateTranslator translator = this.m_translator;
				return ExtractorMetadata.ExtractResultFromRecord(stateEntry, flag, extendedDataRecord, useCurrentValues, translator, modifiedPropertiesBehavior2);
			}
			return this.CreateSimpleResult(stateEntry, record, memberInformation, identifier, flag, ordinal, value2);
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x000A3590 File Offset: 0x000A1790
		private PropagatorResult CreateEntityKeyResult(IEntityStateEntry stateEntry, EntityKey entityKey)
		{
			EntityType elementType = entityKey.GetEntitySet(this.m_translator.MetadataWorkspace).ElementType;
			RowType keyRowType = elementType.GetKeyRowType(this.m_translator.MetadataWorkspace);
			ExtractorMetadata extractorMetadata = this.m_translator.GetExtractorMetadata(stateEntry.EntitySet, keyRowType);
			int count = keyRowType.Properties.Count;
			PropagatorResult[] array = new PropagatorResult[count];
			for (int i = 0; i < keyRowType.Properties.Count; i++)
			{
				EdmMember edmMember = keyRowType.Properties[i];
				ExtractorMetadata.MemberInformation memberInformation = extractorMetadata.m_memberMap[i];
				int keyIdentifierForMemberOffset = this.m_translator.KeyManager.GetKeyIdentifierForMemberOffset(entityKey, i, keyRowType.Properties.Count);
				object value;
				if (entityKey.IsTemporary)
				{
					IEntityStateEntry entityStateEntry = stateEntry.StateManager.GetEntityStateEntry(entityKey);
					value = entityStateEntry.CurrentValues[edmMember.Name];
				}
				else
				{
					value = entityKey.FindValueByName(edmMember.Name);
				}
				array[i] = PropagatorResult.CreateKeyValue(memberInformation.Flags, value, stateEntry, keyIdentifierForMemberOffset);
			}
			return PropagatorResult.CreateStructuralValue(array, extractorMetadata.m_type, false);
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x000A36AC File Offset: 0x000A18AC
		private PropagatorResult CreateSimpleResult(IEntityStateEntry stateEntry, IExtendedDataRecord record, ExtractorMetadata.MemberInformation memberInformation, int identifier, bool isModified, int recordOrdinal, object value)
		{
			CurrentValueRecord currentValueRecord = record as CurrentValueRecord;
			PropagatorFlags propagatorFlags = memberInformation.Flags;
			if (!isModified)
			{
				propagatorFlags |= PropagatorFlags.Preserve;
			}
			if (-1 != identifier)
			{
				PropagatorResult propagatorResult;
				if ((memberInformation.IsServerGenerated || memberInformation.IsForeignKeyMember) && currentValueRecord != null)
				{
					propagatorResult = PropagatorResult.CreateServerGenKeyValue(propagatorFlags, value, stateEntry, identifier, recordOrdinal);
				}
				else
				{
					propagatorResult = PropagatorResult.CreateKeyValue(propagatorFlags, value, stateEntry, identifier);
				}
				this.m_translator.KeyManager.RegisterIdentifierOwner(propagatorResult);
				return propagatorResult;
			}
			if ((memberInformation.IsServerGenerated || memberInformation.IsForeignKeyMember) && currentValueRecord != null)
			{
				return PropagatorResult.CreateServerGenSimpleValue(propagatorFlags, value, currentValueRecord, recordOrdinal);
			}
			return PropagatorResult.CreateSimpleValue(propagatorFlags, value);
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x000A3740 File Offset: 0x000A1940
		internal static PropagatorResult ExtractResultFromRecord(IEntityStateEntry stateEntry, bool isModified, IExtendedDataRecord record, bool useCurrentValues, UpdateTranslator translator, ModifiedPropertiesBehavior modifiedPropertiesBehavior)
		{
			StructuralType structuralType = (StructuralType)record.DataRecordInfo.RecordType.EdmType;
			ExtractorMetadata extractorMetadata = translator.GetExtractorMetadata(stateEntry.EntitySet, structuralType);
			EntityKey entityKey = stateEntry.EntityKey;
			PropagatorResult[] array = new PropagatorResult[record.FieldCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = extractorMetadata.RetrieveMember(stateEntry, record, useCurrentValues, entityKey, i, modifiedPropertiesBehavior);
			}
			return PropagatorResult.CreateStructuralValue(array, structuralType, isModified);
		}

		// Token: 0x040012A4 RID: 4772
		private readonly ExtractorMetadata.MemberInformation[] m_memberMap;

		// Token: 0x040012A5 RID: 4773
		private readonly StructuralType m_type;

		// Token: 0x040012A6 RID: 4774
		private readonly UpdateTranslator m_translator;

		// Token: 0x02000614 RID: 1556
		private class MemberInformation
		{
			// Token: 0x17000B75 RID: 2933
			// (get) Token: 0x0600429D RID: 17053 RVA: 0x000F1C57 File Offset: 0x000EFE57
			internal bool IsKeyMember
			{
				get
				{
					return PropagatorFlags.Key == (this.Flags & PropagatorFlags.Key);
				}
			}

			// Token: 0x17000B76 RID: 2934
			// (get) Token: 0x0600429E RID: 17054 RVA: 0x000F1C66 File Offset: 0x000EFE66
			internal bool IsForeignKeyMember
			{
				get
				{
					return PropagatorFlags.ForeignKey == (this.Flags & PropagatorFlags.ForeignKey);
				}
			}

			// Token: 0x0600429F RID: 17055 RVA: 0x000F1C78 File Offset: 0x000EFE78
			internal MemberInformation(int ordinal, int? entityKeyOrdinal, PropagatorFlags flags, EdmMember member, bool isServerGenerated, bool isNullConditionMember)
			{
				this.Ordinal = ordinal;
				this.EntityKeyOrdinal = entityKeyOrdinal;
				this.Flags = flags;
				this.Member = member;
				this.IsServerGenerated = isServerGenerated;
				this.CheckIsNotNull = (!TypeSemantics.IsNullable(member) && (isNullConditionMember || member.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.ComplexType));
			}

			// Token: 0x04001E35 RID: 7733
			internal readonly int Ordinal;

			// Token: 0x04001E36 RID: 7734
			internal readonly int? EntityKeyOrdinal;

			// Token: 0x04001E37 RID: 7735
			internal readonly PropagatorFlags Flags;

			// Token: 0x04001E38 RID: 7736
			internal readonly bool IsServerGenerated;

			// Token: 0x04001E39 RID: 7737
			internal readonly bool CheckIsNotNull;

			// Token: 0x04001E3A RID: 7738
			internal readonly EdmMember Member;
		}
	}
}
