using System;
using System.Data.Common;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Linq;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002CA RID: 714
	internal abstract class PropagatorResult
	{
		// Token: 0x06002A03 RID: 10755 RVA: 0x00002050 File Offset: 0x00000250
		private PropagatorResult()
		{
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06002A04 RID: 10756
		internal abstract bool IsNull { get; }

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06002A05 RID: 10757
		internal abstract bool IsSimple { get; }

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002A06 RID: 10758 RVA: 0x000173E2 File Offset: 0x000155E2
		internal virtual PropagatorFlags PropagatorFlags
		{
			get
			{
				return PropagatorFlags.NoFlags;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06002A07 RID: 10759 RVA: 0x00006174 File Offset: 0x00004374
		internal virtual IEntityStateEntry StateEntry
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06002A08 RID: 10760 RVA: 0x00006174 File Offset: 0x00004374
		internal virtual CurrentValueRecord Record
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06002A09 RID: 10761 RVA: 0x00006174 File Offset: 0x00004374
		internal virtual StructuralType StructuralType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06002A0A RID: 10762 RVA: 0x0003BCE8 File Offset: 0x00039EE8
		internal virtual int RecordOrdinal
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06002A0B RID: 10763 RVA: 0x0003BCE8 File Offset: 0x00039EE8
		internal virtual int Identifier
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002A0C RID: 10764 RVA: 0x00006174 File Offset: 0x00004374
		internal virtual PropagatorResult Next
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x000A473A File Offset: 0x000A293A
		internal virtual object GetSimpleValue()
		{
			throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "PropagatorResult.GetSimpleValue");
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x000A474C File Offset: 0x000A294C
		internal virtual PropagatorResult GetMemberValue(int ordinal)
		{
			throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "PropagatorResult.GetMemberValue");
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x000A4760 File Offset: 0x000A2960
		internal PropagatorResult GetMemberValue(EdmMember member)
		{
			int ordinal = TypeHelpers.GetAllStructuralMembers(this.StructuralType).IndexOf(member);
			return this.GetMemberValue(ordinal);
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x000A4786 File Offset: 0x000A2986
		internal virtual PropagatorResult[] GetMemberValues()
		{
			throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "PropagatorResult.GetMembersValues");
		}

		// Token: 0x06002A11 RID: 10769
		internal abstract PropagatorResult ReplicateResultWithNewFlags(PropagatorFlags flags);

		// Token: 0x06002A12 RID: 10770 RVA: 0x000A4798 File Offset: 0x000A2998
		internal virtual PropagatorResult ReplicateResultWithNewValue(object value)
		{
			throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "PropagatorResult.ReplicateResultWithNewValue");
		}

		// Token: 0x06002A13 RID: 10771
		internal abstract PropagatorResult Replace(Func<PropagatorResult, PropagatorResult> map);

		// Token: 0x06002A14 RID: 10772 RVA: 0x000A47AA File Offset: 0x000A29AA
		internal virtual PropagatorResult Merge(KeyManager keyManager, PropagatorResult other)
		{
			throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "PropagatorResult.Merge");
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x000A47BC File Offset: 0x000A29BC
		internal static PropagatorResult CreateSimpleValue(PropagatorFlags flags, object value)
		{
			return new PropagatorResult.SimpleValue(flags, value);
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x000A47C5 File Offset: 0x000A29C5
		internal static PropagatorResult CreateServerGenSimpleValue(PropagatorFlags flags, object value, CurrentValueRecord record, int recordOrdinal)
		{
			return new PropagatorResult.ServerGenSimpleValue(flags, value, record, recordOrdinal);
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x000A47D0 File Offset: 0x000A29D0
		internal static PropagatorResult CreateKeyValue(PropagatorFlags flags, object value, IEntityStateEntry stateEntry, int identifier)
		{
			return new PropagatorResult.KeyValue(flags, value, stateEntry, identifier, null);
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x000A47DC File Offset: 0x000A29DC
		internal static PropagatorResult CreateServerGenKeyValue(PropagatorFlags flags, object value, IEntityStateEntry stateEntry, int identifier, int recordOrdinal)
		{
			return new PropagatorResult.ServerGenKeyValue(flags, value, stateEntry, identifier, recordOrdinal, null);
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x000A47EA File Offset: 0x000A29EA
		internal static PropagatorResult CreateStructuralValue(PropagatorResult[] values, StructuralType structuralType, bool isModified)
		{
			if (isModified)
			{
				return new PropagatorResult.StructuralValue(values, structuralType);
			}
			return new PropagatorResult.UnmodifiedStructuralValue(values, structuralType);
		}

		// Token: 0x040012C0 RID: 4800
		internal const int NullIdentifier = -1;

		// Token: 0x040012C1 RID: 4801
		internal const int NullOrdinal = -1;

		// Token: 0x02000620 RID: 1568
		private class SimpleValue : PropagatorResult
		{
			// Token: 0x060042F3 RID: 17139 RVA: 0x000F3D63 File Offset: 0x000F1F63
			internal SimpleValue(PropagatorFlags flags, object value)
			{
				this.m_flags = flags;
				this.m_value = (value ?? DBNull.Value);
			}

			// Token: 0x17000B7C RID: 2940
			// (get) Token: 0x060042F4 RID: 17140 RVA: 0x000F3D82 File Offset: 0x000F1F82
			internal override PropagatorFlags PropagatorFlags
			{
				get
				{
					return this.m_flags;
				}
			}

			// Token: 0x17000B7D RID: 2941
			// (get) Token: 0x060042F5 RID: 17141 RVA: 0x00017938 File Offset: 0x00015B38
			internal override bool IsSimple
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000B7E RID: 2942
			// (get) Token: 0x060042F6 RID: 17142 RVA: 0x000F3D8A File Offset: 0x000F1F8A
			internal override bool IsNull
			{
				get
				{
					return -1 == this.Identifier && DBNull.Value == this.m_value;
				}
			}

			// Token: 0x060042F7 RID: 17143 RVA: 0x000F3DA4 File Offset: 0x000F1FA4
			internal override object GetSimpleValue()
			{
				return this.m_value;
			}

			// Token: 0x060042F8 RID: 17144 RVA: 0x000F3DAC File Offset: 0x000F1FAC
			internal override PropagatorResult ReplicateResultWithNewFlags(PropagatorFlags flags)
			{
				return new PropagatorResult.SimpleValue(flags, this.m_value);
			}

			// Token: 0x060042F9 RID: 17145 RVA: 0x000F3DBA File Offset: 0x000F1FBA
			internal override PropagatorResult ReplicateResultWithNewValue(object value)
			{
				return new PropagatorResult.SimpleValue(this.PropagatorFlags, value);
			}

			// Token: 0x060042FA RID: 17146 RVA: 0x000F3DC8 File Offset: 0x000F1FC8
			internal override PropagatorResult Replace(Func<PropagatorResult, PropagatorResult> map)
			{
				return map(this);
			}

			// Token: 0x04001E65 RID: 7781
			private readonly PropagatorFlags m_flags;

			// Token: 0x04001E66 RID: 7782
			protected readonly object m_value;
		}

		// Token: 0x02000621 RID: 1569
		private class ServerGenSimpleValue : PropagatorResult.SimpleValue
		{
			// Token: 0x060042FB RID: 17147 RVA: 0x000F3DD1 File Offset: 0x000F1FD1
			internal ServerGenSimpleValue(PropagatorFlags flags, object value, CurrentValueRecord record, int recordOrdinal) : base(flags, value)
			{
				this.m_record = record;
				this.m_recordOrdinal = recordOrdinal;
			}

			// Token: 0x17000B7F RID: 2943
			// (get) Token: 0x060042FC RID: 17148 RVA: 0x000F3DEA File Offset: 0x000F1FEA
			internal override CurrentValueRecord Record
			{
				get
				{
					return this.m_record;
				}
			}

			// Token: 0x17000B80 RID: 2944
			// (get) Token: 0x060042FD RID: 17149 RVA: 0x000F3DF2 File Offset: 0x000F1FF2
			internal override int RecordOrdinal
			{
				get
				{
					return this.m_recordOrdinal;
				}
			}

			// Token: 0x060042FE RID: 17150 RVA: 0x000F3DFA File Offset: 0x000F1FFA
			internal override PropagatorResult ReplicateResultWithNewFlags(PropagatorFlags flags)
			{
				return new PropagatorResult.ServerGenSimpleValue(flags, this.m_value, this.Record, this.RecordOrdinal);
			}

			// Token: 0x060042FF RID: 17151 RVA: 0x000F3E14 File Offset: 0x000F2014
			internal override PropagatorResult ReplicateResultWithNewValue(object value)
			{
				return new PropagatorResult.ServerGenSimpleValue(this.PropagatorFlags, value, this.Record, this.RecordOrdinal);
			}

			// Token: 0x04001E67 RID: 7783
			private readonly CurrentValueRecord m_record;

			// Token: 0x04001E68 RID: 7784
			private readonly int m_recordOrdinal;
		}

		// Token: 0x02000622 RID: 1570
		private class KeyValue : PropagatorResult.SimpleValue
		{
			// Token: 0x06004300 RID: 17152 RVA: 0x000F3E2E File Offset: 0x000F202E
			internal KeyValue(PropagatorFlags flags, object value, IEntityStateEntry stateEntry, int identifier, PropagatorResult.KeyValue next) : base(flags, value)
			{
				this.m_stateEntry = stateEntry;
				this.m_identifier = identifier;
				this.m_next = next;
			}

			// Token: 0x17000B81 RID: 2945
			// (get) Token: 0x06004301 RID: 17153 RVA: 0x000F3E4F File Offset: 0x000F204F
			internal override IEntityStateEntry StateEntry
			{
				get
				{
					return this.m_stateEntry;
				}
			}

			// Token: 0x17000B82 RID: 2946
			// (get) Token: 0x06004302 RID: 17154 RVA: 0x000F3E57 File Offset: 0x000F2057
			internal override int Identifier
			{
				get
				{
					return this.m_identifier;
				}
			}

			// Token: 0x17000B83 RID: 2947
			// (get) Token: 0x06004303 RID: 17155 RVA: 0x000F3E5F File Offset: 0x000F205F
			internal override CurrentValueRecord Record
			{
				get
				{
					return this.m_stateEntry.CurrentValues;
				}
			}

			// Token: 0x17000B84 RID: 2948
			// (get) Token: 0x06004304 RID: 17156 RVA: 0x000F3E6C File Offset: 0x000F206C
			internal override PropagatorResult Next
			{
				get
				{
					return this.m_next;
				}
			}

			// Token: 0x06004305 RID: 17157 RVA: 0x000F3E74 File Offset: 0x000F2074
			internal override PropagatorResult ReplicateResultWithNewFlags(PropagatorFlags flags)
			{
				return new PropagatorResult.KeyValue(flags, this.m_value, this.StateEntry, this.Identifier, this.m_next);
			}

			// Token: 0x06004306 RID: 17158 RVA: 0x000F3E94 File Offset: 0x000F2094
			internal override PropagatorResult ReplicateResultWithNewValue(object value)
			{
				return new PropagatorResult.KeyValue(this.PropagatorFlags, value, this.StateEntry, this.Identifier, this.m_next);
			}

			// Token: 0x06004307 RID: 17159 RVA: 0x000F3EB4 File Offset: 0x000F20B4
			internal virtual PropagatorResult.KeyValue ReplicateResultWithNewNext(PropagatorResult.KeyValue next)
			{
				if (this.m_next != null)
				{
					next = this.m_next.ReplicateResultWithNewNext(next);
				}
				return new PropagatorResult.KeyValue(this.PropagatorFlags, this.m_value, this.m_stateEntry, this.m_identifier, next);
			}

			// Token: 0x06004308 RID: 17160 RVA: 0x000F3EEC File Offset: 0x000F20EC
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

			// Token: 0x04001E69 RID: 7785
			private readonly IEntityStateEntry m_stateEntry;

			// Token: 0x04001E6A RID: 7786
			private readonly int m_identifier;

			// Token: 0x04001E6B RID: 7787
			protected readonly PropagatorResult.KeyValue m_next;
		}

		// Token: 0x02000623 RID: 1571
		private class ServerGenKeyValue : PropagatorResult.KeyValue
		{
			// Token: 0x06004309 RID: 17161 RVA: 0x000F3F6F File Offset: 0x000F216F
			internal ServerGenKeyValue(PropagatorFlags flags, object value, IEntityStateEntry stateEntry, int identifier, int recordOrdinal, PropagatorResult.KeyValue next) : base(flags, value, stateEntry, identifier, next)
			{
				this.m_recordOrdinal = recordOrdinal;
			}

			// Token: 0x17000B85 RID: 2949
			// (get) Token: 0x0600430A RID: 17162 RVA: 0x000F3F86 File Offset: 0x000F2186
			internal override int RecordOrdinal
			{
				get
				{
					return this.m_recordOrdinal;
				}
			}

			// Token: 0x0600430B RID: 17163 RVA: 0x000F3F8E File Offset: 0x000F218E
			internal override PropagatorResult ReplicateResultWithNewFlags(PropagatorFlags flags)
			{
				return new PropagatorResult.ServerGenKeyValue(flags, this.m_value, this.StateEntry, this.Identifier, this.RecordOrdinal, this.m_next);
			}

			// Token: 0x0600430C RID: 17164 RVA: 0x000F3FB4 File Offset: 0x000F21B4
			internal override PropagatorResult ReplicateResultWithNewValue(object value)
			{
				return new PropagatorResult.ServerGenKeyValue(this.PropagatorFlags, value, this.StateEntry, this.Identifier, this.RecordOrdinal, this.m_next);
			}

			// Token: 0x0600430D RID: 17165 RVA: 0x000F3FDA File Offset: 0x000F21DA
			internal override PropagatorResult.KeyValue ReplicateResultWithNewNext(PropagatorResult.KeyValue next)
			{
				if (this.m_next != null)
				{
					next = this.m_next.ReplicateResultWithNewNext(next);
				}
				return new PropagatorResult.ServerGenKeyValue(this.PropagatorFlags, this.m_value, this.StateEntry, this.Identifier, this.RecordOrdinal, next);
			}

			// Token: 0x04001E6C RID: 7788
			private readonly int m_recordOrdinal;
		}

		// Token: 0x02000624 RID: 1572
		private class StructuralValue : PropagatorResult
		{
			// Token: 0x0600430E RID: 17166 RVA: 0x000F4016 File Offset: 0x000F2216
			internal StructuralValue(PropagatorResult[] values, StructuralType structuralType)
			{
				this.m_values = values;
				this.m_structuralType = structuralType;
			}

			// Token: 0x17000B86 RID: 2950
			// (get) Token: 0x0600430F RID: 17167 RVA: 0x000173E2 File Offset: 0x000155E2
			internal override bool IsSimple
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000B87 RID: 2951
			// (get) Token: 0x06004310 RID: 17168 RVA: 0x000173E2 File Offset: 0x000155E2
			internal override bool IsNull
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000B88 RID: 2952
			// (get) Token: 0x06004311 RID: 17169 RVA: 0x000F402C File Offset: 0x000F222C
			internal override StructuralType StructuralType
			{
				get
				{
					return this.m_structuralType;
				}
			}

			// Token: 0x06004312 RID: 17170 RVA: 0x000F4034 File Offset: 0x000F2234
			internal override PropagatorResult GetMemberValue(int ordinal)
			{
				return this.m_values[ordinal];
			}

			// Token: 0x06004313 RID: 17171 RVA: 0x000F403E File Offset: 0x000F223E
			internal override PropagatorResult[] GetMemberValues()
			{
				return this.m_values;
			}

			// Token: 0x06004314 RID: 17172 RVA: 0x000F4046 File Offset: 0x000F2246
			internal override PropagatorResult ReplicateResultWithNewFlags(PropagatorFlags flags)
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "StructuralValue.ReplicateResultWithNewFlags");
			}

			// Token: 0x06004315 RID: 17173 RVA: 0x000F4058 File Offset: 0x000F2258
			internal override PropagatorResult Replace(Func<PropagatorResult, PropagatorResult> map)
			{
				PropagatorResult[] array = this.ReplaceValues(map);
				if (array != null)
				{
					return new PropagatorResult.StructuralValue(array, this.m_structuralType);
				}
				return this;
			}

			// Token: 0x06004316 RID: 17174 RVA: 0x000F4080 File Offset: 0x000F2280
			protected PropagatorResult[] ReplaceValues(Func<PropagatorResult, PropagatorResult> map)
			{
				PropagatorResult[] array = new PropagatorResult[this.m_values.Length];
				bool flag = false;
				for (int i = 0; i < array.Length; i++)
				{
					PropagatorResult propagatorResult = this.m_values[i].Replace(map);
					if (propagatorResult != this.m_values[i])
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

			// Token: 0x04001E6D RID: 7789
			private readonly PropagatorResult[] m_values;

			// Token: 0x04001E6E RID: 7790
			protected readonly StructuralType m_structuralType;
		}

		// Token: 0x02000625 RID: 1573
		private class UnmodifiedStructuralValue : PropagatorResult.StructuralValue
		{
			// Token: 0x06004317 RID: 17175 RVA: 0x000F40D1 File Offset: 0x000F22D1
			internal UnmodifiedStructuralValue(PropagatorResult[] values, StructuralType structuralType) : base(values, structuralType)
			{
			}

			// Token: 0x17000B89 RID: 2953
			// (get) Token: 0x06004318 RID: 17176 RVA: 0x00017938 File Offset: 0x00015B38
			internal override PropagatorFlags PropagatorFlags
			{
				get
				{
					return PropagatorFlags.Preserve;
				}
			}

			// Token: 0x06004319 RID: 17177 RVA: 0x000F40DC File Offset: 0x000F22DC
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
