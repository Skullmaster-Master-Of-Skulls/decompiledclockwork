using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003CD RID: 973
	internal class RecordState
	{
		// Token: 0x0600347E RID: 13438 RVA: 0x000CAB14 File Offset: 0x000C8D14
		internal RecordState(RecordStateFactory recordStateFactory, CoordinatorFactory coordinatorFactory)
		{
			this.RecordStateFactory = recordStateFactory;
			this.CoordinatorFactory = coordinatorFactory;
			this.CurrentColumnValues = new object[this.RecordStateFactory.ColumnCount];
			this.PendingColumnValues = new object[this.RecordStateFactory.ColumnCount];
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x000CAB64 File Offset: 0x000C8D64
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

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06003480 RID: 13440 RVA: 0x000CABF4 File Offset: 0x000C8DF4
		internal int ColumnCount
		{
			get
			{
				return this.RecordStateFactory.ColumnCount;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06003481 RID: 13441 RVA: 0x000CAC04 File Offset: 0x000C8E04
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

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06003482 RID: 13442 RVA: 0x000CAC28 File Offset: 0x000C8E28
		internal bool IsNull
		{
			get
			{
				return this._currentIsNull;
			}
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x000CAC30 File Offset: 0x000C8E30
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

		// Token: 0x06003484 RID: 13444 RVA: 0x000CAC7C File Offset: 0x000C8E7C
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

		// Token: 0x06003485 RID: 13445 RVA: 0x000CACEB File Offset: 0x000C8EEB
		internal string GetName(int ordinal)
		{
			if (ordinal < 0 || ordinal >= this.RecordStateFactory.ColumnCount)
			{
				throw EntityUtil.ArgumentOutOfRange("ordinal");
			}
			return this.RecordStateFactory.ColumnNames[ordinal];
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x000CAD1B File Offset: 0x000C8F1B
		internal int GetOrdinal(string name)
		{
			return this.RecordStateFactory.FieldNameLookup.GetOrdinal(name);
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x000CAD2E File Offset: 0x000C8F2E
		internal TypeUsage GetTypeUsage(int ordinal)
		{
			return this.RecordStateFactory.TypeUsages[ordinal];
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x000CAD41 File Offset: 0x000C8F41
		internal bool IsNestedObject(int ordinal)
		{
			return this.RecordStateFactory.IsColumnNested[ordinal];
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x000CAD54 File Offset: 0x000C8F54
		internal void ResetToDefaultState()
		{
			this._currentEntityRecordInfo = null;
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000CAD5D File Offset: 0x000C8F5D
		internal RecordState GatherData(Shaper shaper)
		{
			this.RecordStateFactory.GatherData(shaper);
			this._pendingIsNull = false;
			return this;
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000CAD79 File Offset: 0x000C8F79
		internal bool SetColumnValue(int ordinal, object value)
		{
			this.PendingColumnValues[ordinal] = value;
			return true;
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x000CAD85 File Offset: 0x000C8F85
		internal bool SetEntityRecordInfo(EntityKey entityKey, EntitySet entitySet)
		{
			this._pendingEntityRecordInfo = new EntityRecordInfo(this.RecordStateFactory.DataRecordInfo, entityKey, entitySet);
			return true;
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x000CADA0 File Offset: 0x000C8FA0
		internal RecordState SetNullRecord(Shaper shaper)
		{
			for (int i = 0; i < this.PendingColumnValues.Length; i++)
			{
				this.PendingColumnValues[i] = DBNull.Value;
			}
			this._pendingEntityRecordInfo = null;
			this._pendingIsNull = true;
			return this;
		}

		// Token: 0x040016F6 RID: 5878
		private readonly RecordStateFactory RecordStateFactory;

		// Token: 0x040016F7 RID: 5879
		internal readonly CoordinatorFactory CoordinatorFactory;

		// Token: 0x040016F8 RID: 5880
		private bool _pendingIsNull;

		// Token: 0x040016F9 RID: 5881
		private bool _currentIsNull;

		// Token: 0x040016FA RID: 5882
		private EntityRecordInfo _currentEntityRecordInfo;

		// Token: 0x040016FB RID: 5883
		private EntityRecordInfo _pendingEntityRecordInfo;

		// Token: 0x040016FC RID: 5884
		internal object[] CurrentColumnValues;

		// Token: 0x040016FD RID: 5885
		internal object[] PendingColumnValues;
	}
}
