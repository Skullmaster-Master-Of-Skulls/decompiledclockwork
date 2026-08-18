using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x02000407 RID: 1031
	internal abstract class PropagatorResult
	{
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060025FD RID: 9725
		internal abstract bool IsNull { get; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060025FE RID: 9726
		internal abstract bool IsSimple { get; }

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060025FF RID: 9727 RVA: 0x000B550D File Offset: 0x000B370D
		internal virtual PropagatorFlags PropagatorFlags
		{
			get
			{
				return PropagatorFlags.NoFlags;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x000B5510 File Offset: 0x000B3710
		internal virtual IEntityStateEntry StateEntry
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06002601 RID: 9729 RVA: 0x000B5513 File Offset: 0x000B3713
		internal virtual CurrentValueRecord Record
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x000B5516 File Offset: 0x000B3716
		internal virtual StructuralType StructuralType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06002603 RID: 9731 RVA: 0x000B5519 File Offset: 0x000B3719
		internal virtual int RecordOrdinal
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06002604 RID: 9732 RVA: 0x000B551C File Offset: 0x000B371C
		internal virtual int Identifier
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06002605 RID: 9733 RVA: 0x000B551F File Offset: 0x000B371F
		internal virtual PropagatorResult Next
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x000B5522 File Offset: 0x000B3722
		internal virtual object GetSimpleValue()
		{
			throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "PropagatorResult.GetSimpleValue");
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x000B5534 File Offset: 0x000B3734
		internal virtual PropagatorResult GetMemberValue(int ordinal)
		{
			throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "PropagatorResult.GetMemberValue");
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x000B5548 File Offset: 0x000B3748
		internal PropagatorResult GetMemberValue(EdmMember member)
		{
			int ordinal = TypeHelpers.GetAllStructuralMembers(this.StructuralType).IndexOf(member);
			return this.GetMemberValue(ordinal);
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x000B556E File Offset: 0x000B376E
		internal virtual PropagatorResult[] GetMemberValues()
		{
			throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "PropagatorResult.GetMembersValues");
		}

		// Token: 0x0600260A RID: 9738
		internal abstract PropagatorResult ReplicateResultWithNewFlags(PropagatorFlags flags);

		// Token: 0x0600260B RID: 9739 RVA: 0x000B5580 File Offset: 0x000B3780
		internal virtual PropagatorResult ReplicateResultWithNewValue(object value)
		{
			throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "PropagatorResult.ReplicateResultWithNewValue");
		}

		// Token: 0x0600260C RID: 9740
		internal abstract PropagatorResult Replace(Func<PropagatorResult, PropagatorResult> map);

		// Token: 0x0600260D RID: 9741 RVA: 0x000B5592 File Offset: 0x000B3792
		internal virtual PropagatorResult Merge(KeyManager keyManager, PropagatorResult other)
		{
			throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "PropagatorResult.Merge");
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x000B55A4 File Offset: 0x000B37A4
		internal virtual void SetServerGenValue(object value)
		{
			if (this.RecordOrdinal != -1)
			{
				CurrentValueRecord record = this.Record;
				IExtendedDataRecord extendedDataRecord = record;
				EdmMember fieldType = extendedDataRecord.DataRecordInfo.FieldMetadata[this.RecordOrdinal].FieldType;
				value = (value ?? DBNull.Value);
				value = this.AlignReturnValue(value, fieldType);
				record.SetValue(this.RecordOrdinal, value);
			}
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x000B5608 File Offset: 0x000B3808
		internal object AlignReturnValue(object value, EdmMember member)
		{
			if (DBNull.Value.Equals(value))
			{
				if (BuiltInTypeKind.EdmProperty == member.BuiltInTypeKind && !((EdmProperty)member).Nullable)
				{
					throw EntityUtil.Update(Strings.Update_NullReturnValueForNonNullableMember(member.Name, member.DeclaringType.FullName), null, new IEntityStateEntry[0]);
				}
			}
			else if (!Helper.IsSpatialType(member.TypeUsage))
			{
				Type type = null;
				Type clrEquivalentType;
				if (Helper.IsEnumType(member.TypeUsage.EdmType))
				{
					PrimitiveType primitiveType = Helper.AsPrimitive(member.TypeUsage.EdmType);
					type = this.Record.GetFieldType(this.RecordOrdinal);
					clrEquivalentType = primitiveType.ClrEquivalentType;
				}
				else
				{
					PrimitiveType primitiveType2 = (PrimitiveType)member.TypeUsage.EdmType;
					clrEquivalentType = primitiveType2.ClrEquivalentType;
				}
				try
				{
					value = Convert.ChangeType(value, clrEquivalentType, CultureInfo.InvariantCulture);
					if (type != null)
					{
						value = Enum.ToObject(type, value);
					}
				}
				catch (Exception ex)
				{
					if (ex.RequiresContext())
					{
						Type type2 = type ?? clrEquivalentType;
						throw EntityUtil.Update(Strings.Update_ReturnValueHasUnexpectedType(value.GetType().FullName, type2.FullName, member.Name, member.DeclaringType.FullName), ex, new IEntityStateEntry[0]);
					}
					throw;
				}
			}
			return value;
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x000B5748 File Offset: 0x000B3948
		internal static PropagatorResult CreateSimpleValue(PropagatorFlags flags, object value)
		{
			return new PropagatorResult.SimpleValue(flags, value);
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x000B5751 File Offset: 0x000B3951
		internal static PropagatorResult CreateServerGenSimpleValue(PropagatorFlags flags, object value, CurrentValueRecord record, int recordOrdinal)
		{
			return new PropagatorResult.ServerGenSimpleValue(flags, value, record, recordOrdinal);
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x000B575C File Offset: 0x000B395C
		internal static PropagatorResult CreateKeyValue(PropagatorFlags flags, object value, IEntityStateEntry stateEntry, int identifier)
		{
			return new PropagatorResult.KeyValue(flags, value, stateEntry, identifier, null);
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x000B5768 File Offset: 0x000B3968
		internal static PropagatorResult CreateServerGenKeyValue(PropagatorFlags flags, object value, IEntityStateEntry stateEntry, int identifier, int recordOrdinal)
		{
			return new PropagatorResult.ServerGenKeyValue(flags, value, stateEntry, identifier, recordOrdinal, null);
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x000B5776 File Offset: 0x000B3976
		internal static PropagatorResult CreateStructuralValue(PropagatorResult[] values, StructuralType structuralType, bool isModified)
		{
			if (isModified)
			{
				return new PropagatorResult.StructuralValue(values, structuralType);
			}
			return new PropagatorResult.UnmodifiedStructuralValue(values, structuralType);
		}

		// Token: 0x04000E43 RID: 3651
		internal const int NullIdentifier = -1;

		// Token: 0x04000E44 RID: 3652
		internal const int NullOrdinal = -1;

		// Token: 0x02000408 RID: 1032
		private class SimpleValue : PropagatorResult
		{
			// Token: 0x06002616 RID: 9750 RVA: 0x000B5792 File Offset: 0x000B3992
			internal SimpleValue(PropagatorFlags flags, object value)
			{
				this.m_flags = flags;
				this.m_value = (value ?? DBNull.Value);
			}

			// Token: 0x17000535 RID: 1333
			// (get) Token: 0x06002617 RID: 9751 RVA: 0x000B57B1 File Offset: 0x000B39B1
			internal override PropagatorFlags PropagatorFlags
			{
				get
				{
					return this.m_flags;
				}
			}

			// Token: 0x17000536 RID: 1334
			// (get) Token: 0x06002618 RID: 9752 RVA: 0x000B57B9 File Offset: 0x000B39B9
			internal override bool IsSimple
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000537 RID: 1335
			// (get) Token: 0x06002619 RID: 9753 RVA: 0x000B57BC File Offset: 0x000B39BC
			internal override bool IsNull
			{
				get
				{
					return -1 == this.Identifier && DBNull.Value == this.m_value;
				}
			}

			// Token: 0x0600261A RID: 9754 RVA: 0x000B57D6 File Offset: 0x000B39D6
			internal override object GetSimpleValue()
			{
				return this.m_value;
			}

			// Token: 0x0600261B RID: 9755 RVA: 0x000B57DE File Offset: 0x000B39DE
			internal override PropagatorResult ReplicateResultWithNewFlags(PropagatorFlags flags)
			{
				return new PropagatorResult.SimpleValue(flags, this.m_value);
			}

			// Token: 0x0600261C RID: 9756 RVA: 0x000B57EC File Offset: 0x000B39EC
			internal override PropagatorResult ReplicateResultWithNewValue(object value)
			{
				return new PropagatorResult.SimpleValue(this.PropagatorFlags, value);
			}

			// Token: 0x0600261D RID: 9757 RVA: 0x000B57FA File Offset: 0x000B39FA
			internal override PropagatorResult Replace(Func<PropagatorResult, PropagatorResult> map)
			{
				return map(this);
			}

			// Token: 0x04000E45 RID: 3653
			private readonly PropagatorFlags m_flags;

			// Token: 0x04000E46 RID: 3654
			protected readonly object m_value;
		}

		// Token: 0x02000409 RID: 1033
		private class ServerGenSimpleValue : PropagatorResult.SimpleValue
		{
			// Token: 0x0600261E RID: 9758 RVA: 0x000B5803 File Offset: 0x000B3A03
			internal ServerGenSimpleValue(PropagatorFlags flags, object value, CurrentValueRecord record, int recordOrdinal) : base(flags, value)
			{
				this.m_record = record;
				this.m_recordOrdinal = recordOrdinal;
			}

			// Token: 0x17000538 RID: 1336
			// (get) Token: 0x0600261F RID: 9759 RVA: 0x000B581C File Offset: 0x000B3A1C
			internal override CurrentValueRecord Record
			{
				get
				{
					return this.m_record;
				}
			}

			// Token: 0x17000539 RID: 1337
			// (get) Token: 0x06002620 RID: 9760 RVA: 0x000B5824 File Offset: 0x000B3A24
			internal override int RecordOrdinal
			{
				get
				{
					return this.m_recordOrdinal;
				}
			}

			// Token: 0x06002621 RID: 9761 RVA: 0x000B582C File Offset: 0x000B3A2C
			internal override PropagatorResult ReplicateResultWithNewFlags(PropagatorFlags flags)
			{
				return new PropagatorResult.ServerGenSimpleValue(flags, this.m_value, this.Record, this.RecordOrdinal);
			}

			// Token: 0x06002622 RID: 9762 RVA: 0x000B5846 File Offset: 0x000B3A46
			internal override PropagatorResult ReplicateResultWithNewValue(object value)
			{
				return new PropagatorResult.ServerGenSimpleValue(this.PropagatorFlags, value, this.Record, this.RecordOrdinal);
			}

			// Token: 0x04000E47 RID: 3655
			private readonly CurrentValueRecord m_record;

			// Token: 0x04000E48 RID: 3656
			private readonly int m_recordOrdinal;
		}

		// Token: 0x0200040A RID: 1034
		private class KeyValue : PropagatorResult.SimpleValue
		{
			// Token: 0x06002623 RID: 9763 RVA: 0x000B5860 File Offset: 0x000B3A60
			internal KeyValue(PropagatorFlags flags, object value, IEntityStateEntry stateEntry, int identifier, PropagatorResult.KeyValue next) : base(flags, value)
			{
				this.m_stateEntry = stateEntry;
				this.m_identifier = identifier;
				this.m_next = next;
			}

			// Token: 0x1700053A RID: 1338
			// (get) Token: 0x06002624 RID: 9764 RVA: 0x000B5881 File Offset: 0x000B3A81
			internal override IEntityStateEntry StateEntry
			{
				get
				{
					return this.m_stateEntry;
				}
			}

			// Token: 0x1700053B RID: 1339
			// (get) Token: 0x06002625 RID: 9765 RVA: 0x000B5889 File Offset: 0x000B3A89
			internal override int Identifier
			{
				get
				{
					return this.m_identifier;
				}
			}

			// Token: 0x1700053C RID: 1340
			// (get) Token: 0x06002626 RID: 9766 RVA: 0x000B5891 File Offset: 0x000B3A91
			internal override CurrentValueRecord Record
			{
				get
				{
					return this.m_stateEntry.CurrentValues;
				}
			}

			// Token: 0x1700053D RID: 1341
			// (get) Token: 0x06002627 RID: 9767 RVA: 0x000B589E File Offset: 0x000B3A9E
			internal override PropagatorResult Next
			{
				get
				{
					return this.m_next;
				}
			}

			// Token: 0x06002628 RID: 9768 RVA: 0x000B58A6 File Offset: 0x000B3AA6
			internal override PropagatorResult ReplicateResultWithNewFlags(PropagatorFlags flags)
			{
				return new PropagatorResult.KeyValue(flags, this.m_value, this.StateEntry, this.Identifier, this.m_next);
			}

			// Token: 0x06002629 RID: 9769 RVA: 0x000B58C6 File Offset: 0x000B3AC6
			internal override PropagatorResult ReplicateResultWithNewValue(object value)
			{
				return new PropagatorResult.KeyValue(this.PropagatorFlags, value, this.StateEntry, this.Identifier, this.m_next);
			}

			// Token: 0x0600262A RID: 9770 RVA: 0x000B58E6 File Offset: 0x000B3AE6
			internal virtual PropagatorResult.KeyValue ReplicateResultWithNewNext(PropagatorResult.KeyValue next)
			{
				if (this.m_next != null)
				{
					next = this.m_next.ReplicateResultWithNewNext(next);
				}
				return new PropagatorResult.KeyValue(this.PropagatorFlags, this.m_value, this.m_stateEntry, this.m_identifier, next);
			}

			// Token: 0x0600262B RID: 9771 RVA: 0x000B591C File Offset: 0x000B3B1C
			internal override PropagatorResult Merge(KeyManager keyManager, PropagatorResult other)
			{
				PropagatorResult.KeyValue keyValue = other as PropagatorResult.KeyValue;
				if (keyValue == null)
				{
					EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "KeyValue.Merge");
				}
				if (this.Identifier != keyValue.Identifier)
				{
					if (keyManager.GetPrincipals(keyValue.Identifier).Contains(this.Identifier))
					{
						return this.ReplicateResultWithNewNext(keyValue);
					}
					return keyValue.ReplicateResultWithNewNext(this);
				}
				else
				{
					if (this.m_stateEntry == null || this.m_stateEntry.IsRelationship)
					{
						return keyValue.ReplicateResultWithNewNext(this);
					}
					return this.ReplicateResultWithNewNext(keyValue);
				}
			}

			// Token: 0x04000E49 RID: 3657
			private readonly IEntityStateEntry m_stateEntry;

			// Token: 0x04000E4A RID: 3658
			private readonly int m_identifier;

			// Token: 0x04000E4B RID: 3659
			protected readonly PropagatorResult.KeyValue m_next;
		}

		// Token: 0x0200040B RID: 1035
		private class ServerGenKeyValue : PropagatorResult.KeyValue
		{
			// Token: 0x0600262C RID: 9772 RVA: 0x000B599F File Offset: 0x000B3B9F
			internal ServerGenKeyValue(PropagatorFlags flags, object value, IEntityStateEntry stateEntry, int identifier, int recordOrdinal, PropagatorResult.KeyValue next) : base(flags, value, stateEntry, identifier, next)
			{
				this.m_recordOrdinal = recordOrdinal;
			}

			// Token: 0x1700053E RID: 1342
			// (get) Token: 0x0600262D RID: 9773 RVA: 0x000B59B6 File Offset: 0x000B3BB6
			internal override int RecordOrdinal
			{
				get
				{
					return this.m_recordOrdinal;
				}
			}

			// Token: 0x0600262E RID: 9774 RVA: 0x000B59BE File Offset: 0x000B3BBE
			internal override PropagatorResult ReplicateResultWithNewFlags(PropagatorFlags flags)
			{
				return new PropagatorResult.ServerGenKeyValue(flags, this.m_value, this.StateEntry, this.Identifier, this.RecordOrdinal, this.m_next);
			}

			// Token: 0x0600262F RID: 9775 RVA: 0x000B59E4 File Offset: 0x000B3BE4
			internal override PropagatorResult ReplicateResultWithNewValue(object value)
			{
				return new PropagatorResult.ServerGenKeyValue(this.PropagatorFlags, value, this.StateEntry, this.Identifier, this.RecordOrdinal, this.m_next);
			}

			// Token: 0x06002630 RID: 9776 RVA: 0x000B5A0A File Offset: 0x000B3C0A
			internal override PropagatorResult.KeyValue ReplicateResultWithNewNext(PropagatorResult.KeyValue next)
			{
				if (this.m_next != null)
				{
					next = this.m_next.ReplicateResultWithNewNext(next);
				}
				return new PropagatorResult.ServerGenKeyValue(this.PropagatorFlags, this.m_value, this.StateEntry, this.Identifier, this.RecordOrdinal, next);
			}

			// Token: 0x04000E4C RID: 3660
			private readonly int m_recordOrdinal;
		}

		// Token: 0x0200040C RID: 1036
		private class StructuralValue : PropagatorResult
		{
			// Token: 0x06002631 RID: 9777 RVA: 0x000B5A46 File Offset: 0x000B3C46
			internal StructuralValue(PropagatorResult[] values, StructuralType structuralType)
			{
				this.m_values = values;
				this.m_structuralType = structuralType;
			}

			// Token: 0x1700053F RID: 1343
			// (get) Token: 0x06002632 RID: 9778 RVA: 0x000B5A5C File Offset: 0x000B3C5C
			internal override bool IsSimple
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000540 RID: 1344
			// (get) Token: 0x06002633 RID: 9779 RVA: 0x000B5A5F File Offset: 0x000B3C5F
			internal override bool IsNull
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000541 RID: 1345
			// (get) Token: 0x06002634 RID: 9780 RVA: 0x000B5A62 File Offset: 0x000B3C62
			internal override StructuralType StructuralType
			{
				get
				{
					return this.m_structuralType;
				}
			}

			// Token: 0x06002635 RID: 9781 RVA: 0x000B5A6A File Offset: 0x000B3C6A
			internal override PropagatorResult GetMemberValue(int ordinal)
			{
				return this.m_values[ordinal];
			}

			// Token: 0x06002636 RID: 9782 RVA: 0x000B5A74 File Offset: 0x000B3C74
			internal override PropagatorResult[] GetMemberValues()
			{
				return this.m_values;
			}

			// Token: 0x06002637 RID: 9783 RVA: 0x000B5A7C File Offset: 0x000B3C7C
			internal override PropagatorResult ReplicateResultWithNewFlags(PropagatorFlags flags)
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "StructuralValue.ReplicateResultWithNewFlags");
			}

			// Token: 0x06002638 RID: 9784 RVA: 0x000B5A90 File Offset: 0x000B3C90
			internal override PropagatorResult Replace(Func<PropagatorResult, PropagatorResult> map)
			{
				PropagatorResult[] array = this.ReplaceValues(map);
				if (array != null)
				{
					return new PropagatorResult.StructuralValue(array, this.m_structuralType);
				}
				return this;
			}

			// Token: 0x06002639 RID: 9785 RVA: 0x000B5AB8 File Offset: 0x000B3CB8
			protected PropagatorResult[] ReplaceValues(Func<PropagatorResult, PropagatorResult> map)
			{
				PropagatorResult[] array = new PropagatorResult[this.m_values.Length];
				bool flag = false;
				for (int i = 0; i < array.Length; i++)
				{
					PropagatorResult propagatorResult = this.m_values[i].Replace(map);
					if (!object.ReferenceEquals(propagatorResult, this.m_values[i]))
					{
						flag = true;
					}
					array[i] = propagatorResult;
				}
				if (!flag)
				{
					return null;
				}
				return array;
			}

			// Token: 0x04000E4D RID: 3661
			private readonly PropagatorResult[] m_values;

			// Token: 0x04000E4E RID: 3662
			protected readonly StructuralType m_structuralType;
		}

		// Token: 0x0200040D RID: 1037
		private class UnmodifiedStructuralValue : PropagatorResult.StructuralValue
		{
			// Token: 0x0600263A RID: 9786 RVA: 0x000B5B0E File Offset: 0x000B3D0E
			internal UnmodifiedStructuralValue(PropagatorResult[] values, StructuralType structuralType) : base(values, structuralType)
			{
			}

			// Token: 0x17000542 RID: 1346
			// (get) Token: 0x0600263B RID: 9787 RVA: 0x000B5B18 File Offset: 0x000B3D18
			internal override PropagatorFlags PropagatorFlags
			{
				get
				{
					return PropagatorFlags.Preserve;
				}
			}

			// Token: 0x0600263C RID: 9788 RVA: 0x000B5B1C File Offset: 0x000B3D1C
			internal override PropagatorResult Replace(Func<PropagatorResult, PropagatorResult> map)
			{
				PropagatorResult[] array = base.ReplaceValues(map);
				if (array != null)
				{
					return new PropagatorResult.UnmodifiedStructuralValue(array, this.m_structuralType);
				}
				return this;
			}
		}
	}
}
