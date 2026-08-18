using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x020002DF RID: 735
	internal class RecordState
	{
		// Token: 0x060019D6 RID: 6614 RVA: 0x0008090C File Offset: 0x0007EB0C
		internal RecordState(RecordStateFactory recordStateFactory, CoordinatorFactory coordinatorFactory)
		{
			this.RecordStateFactory = recordStateFactory;
			this.CoordinatorFactory = coordinatorFactory;
			this.CurrentColumnValues = new object[this.RecordStateFactory.ColumnCount];
			this.PendingColumnValues = new object[this.RecordStateFactory.ColumnCount];
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x0008095C File Offset: 0x0007EB5C
		internal void AcceptPendingValues()
		{
			object[] currentColumnValues = this.CurrentColumnValues;
			this.CurrentColumnValues = this.PendingColumnValues;
			this.PendingColumnValues = currentColumnValues;
			this._currentEntityRecordInfo = this._pendingEntityRecordInfo;
			this._pendingEntityRecordInfo = null;
			this._currentIsNull = this._pendingIsNull;
			if (this.RecordStateFactory.HasNestedColumns)
			{
				for (int i = 0; i < this.CurrentColumnValues.Length; i++)
				{
					if (this.RecordStateFactory.IsColumnNested[i])
					{
						RecordState recordState = this.CurrentColumnValues[i] as RecordState;
						if (recordState != null)
						{
							recordState.AcceptPendingValues();
						}
					}
				}
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x000809EC File Offset: 0x0007EBEC
		internal int ColumnCount
		{
			get
			{
				return this.RecordStateFactory.ColumnCount;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x000809FC File Offset: 0x0007EBFC
		internal DataRecordInfo DataRecordInfo
		{
			get
			{
				DataRecordInfo dataRecordInfo = this._currentEntityRecordInfo;
				if (dataRecordInfo == null)
				{
					dataRecordInfo = this.RecordStateFactory.DataRecordInfo;
				}
				return dataRecordInfo;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x00080A20 File Offset: 0x0007EC20
		internal bool IsNull
		{
			get
			{
				return this._currentIsNull;
			}
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00080A28 File Offset: 0x0007EC28
		internal long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			byte[] array = (byte[])this.CurrentColumnValues[ordinal];
			int num = array.Length;
			int num2 = (int)dataOffset;
			int num3 = num - num2;
			if (buffer != null)
			{
				num3 = Math.Min(num3, length);
				if (0 < num3)
				{
					Buffer.BlockCopy(array, num2, buffer, bufferOffset, num3);
				}
			}
			return (long)Math.Max(0, num3);
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00080A74 File Offset: 0x0007EC74
		internal long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			string text = this.CurrentColumnValues[ordinal] as string;
			char[] array;
			if (text != null)
			{
				array = text.ToCharArray();
			}
			else
			{
				array = (char[])this.CurrentColumnValues[ordinal];
			}
			int num = array.Length;
			int num2 = (int)dataOffset;
			int num3 = num - num2;
			if (buffer != null)
			{
				num3 = Math.Min(num3, length);
				if (0 < num3)
				{
					Buffer.BlockCopy(array, num2 * 2, buffer, bufferOffset * 2, num3 * 2);
				}
			}
			return (long)Math.Max(0, num3);
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00080AE3 File Offset: 0x0007ECE3
		internal string GetName(int ordinal)
		{
			if (ordinal < 0 || ordinal >= this.RecordStateFactory.ColumnCount)
			{
				throw new ArgumentOutOfRangeException("ordinal");
			}
			return this.RecordStateFactory.ColumnNames[ordinal];
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00080B13 File Offset: 0x0007ED13
		internal int GetOrdinal(string name)
		{
			return this.RecordStateFactory.FieldNameLookup.GetOrdinal(name);
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x00080B26 File Offset: 0x0007ED26
		internal TypeUsage GetTypeUsage(int ordinal)
		{
			return this.RecordStateFactory.TypeUsages[ordinal];
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00080B39 File Offset: 0x0007ED39
		internal bool IsNestedObject(int ordinal)
		{
			return this.RecordStateFactory.IsColumnNested[ordinal];
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00080B4C File Offset: 0x0007ED4C
		internal void ResetToDefaultState()
		{
			this._currentEntityRecordInfo = null;
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00080B55 File Offset: 0x0007ED55
		internal RecordState GatherData(Shaper shaper)
		{
			this.RecordStateFactory.GatherData(shaper);
			this._pendingIsNull = false;
			return this;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00080B71 File Offset: 0x0007ED71
		internal bool SetColumnValue(int ordinal, object value)
		{
			this.PendingColumnValues[ordinal] = value;
			return true;
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x00080B7D File Offset: 0x0007ED7D
		internal bool SetEntityRecordInfo(EntityKey entityKey, EntitySet entitySet)
		{
			this._pendingEntityRecordInfo = new EntityRecordInfo(this.RecordStateFactory.DataRecordInfo, entityKey, entitySet);
			return true;
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00080B98 File Offset: 0x0007ED98
		internal RecordState SetNullRecord()
		{
			for (int i = 0; i < this.PendingColumnValues.Length; i++)
			{
				this.PendingColumnValues[i] = DBNull.Value;
			}
			this._pendingEntityRecordInfo = null;
			this._pendingIsNull = true;
			return this;
		}

		// Token: 0x040008E3 RID: 2275
		private readonly RecordStateFactory RecordStateFactory;

		// Token: 0x040008E4 RID: 2276
		internal readonly CoordinatorFactory CoordinatorFactory;

		// Token: 0x040008E5 RID: 2277
		private bool _pendingIsNull;

		// Token: 0x040008E6 RID: 2278
		private bool _currentIsNull;

		// Token: 0x040008E7 RID: 2279
		private EntityRecordInfo _currentEntityRecordInfo;

		// Token: 0x040008E8 RID: 2280
		private EntityRecordInfo _pendingEntityRecordInfo;

		// Token: 0x040008E9 RID: 2281
		internal object[] CurrentColumnValues;

		// Token: 0x040008EA RID: 2282
		internal object[] PendingColumnValues;
	}
}
