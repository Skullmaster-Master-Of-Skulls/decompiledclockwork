using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020003F0 RID: 1008
	internal class ExtractorMetadata
	{
		// Token: 0x06002539 RID: 9529 RVA: 0x000B0390 File Offset: 0x000AE590
		internal ExtractorMetadata(EntitySetBase entitySetBase, StructuralType type, UpdateTranslator translator)
		{
			this.m_type = type;
			this.m_translator = translator;
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

		// Token: 0x0600253A RID: 9530 RVA: 0x000B0534 File Offset: 0x000AE734
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

		// Token: 0x0600253B RID: 9531 RVA: 0x000B0674 File Offset: 0x000AE874
		private PropagatorResult CreateEntityKeyResult(IEntityStateEntry stateEntry, EntityKey entityKey)
		{
			EntityType elementType = entityKey.GetEntitySet(this.m_translator.MetadataWorkspace).ElementType;
			RowType keyRowType = elementType.GetKeyRowType();
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

		// Token: 0x0600253C RID: 9532 RVA: 0x000B0784 File Offset: 0x000AE984
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

		// Token: 0x0600253D RID: 9533 RVA: 0x000B0818 File Offset: 0x000AEA18
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

		// Token: 0x04000DD0 RID: 3536
		private readonly ExtractorMetadata.MemberInformation[] m_memberMap;

		// Token: 0x04000DD1 RID: 3537
		private readonly StructuralType m_type;

		// Token: 0x04000DD2 RID: 3538
		private readonly UpdateTranslator m_translator;

		// Token: 0x020003F1 RID: 1009
		private class MemberInformation
		{
			// Token: 0x1700051F RID: 1311
			// (get) Token: 0x0600253F RID: 9535 RVA: 0x000B0889 File Offset: 0x000AEA89
			internal bool IsKeyMember
			{
				get
				{
					return 16 == (byte)(this.Flags & PropagatorFlags.Key);
				}
			}

			// Token: 0x17000520 RID: 1312
			// (get) Token: 0x06002540 RID: 9536 RVA: 0x000B0899 File Offset: 0x000AEA99
			internal bool IsForeignKeyMember
			{
				get
				{
					return 32 == (byte)(this.Flags & PropagatorFlags.ForeignKey);
				}
			}

			// Token: 0x06002541 RID: 9537 RVA: 0x000B08AC File Offset: 0x000AEAAC
			internal MemberInformation(int ordinal, int? entityKeyOrdinal, PropagatorFlags flags, EdmMember member, bool isServerGenerated, bool isNullConditionMember)
			{
				this.Ordinal = ordinal;
				this.EntityKeyOrdinal = entityKeyOrdinal;
				this.Flags = flags;
				this.Member = member;
				this.IsServerGenerated = isServerGenerated;
				this.CheckIsNotNull = (!TypeSemantics.IsNullable(member) && (isNullConditionMember || member.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.ComplexType));
			}

			// Token: 0x04000DD4 RID: 3540
			internal readonly int Ordinal;

			// Token: 0x04000DD5 RID: 3541
			internal readonly int? EntityKeyOrdinal;

			// Token: 0x04000DD6 RID: 3542
			internal readonly PropagatorFlags Flags;

			// Token: 0x04000DD7 RID: 3543
			internal readonly bool IsServerGenerated;

			// Token: 0x04000DD8 RID: 3544
			internal readonly bool CheckIsNotNull;

			// Token: 0x04000DD9 RID: 3545
			[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
			internal readonly EdmMember Member;
		}
	}
}
