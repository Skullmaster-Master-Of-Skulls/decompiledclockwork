using System;
using System.Collections;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x020001FA RID: 506
	internal sealed class SqlStatistics
	{
		// Token: 0x06001F5C RID: 8028 RVA: 0x000D8EA4 File Offset: 0x000D82A4
		internal static SqlStatistics StartTimer(SqlStatistics statistics)
		{
			if (statistics != null && !statistics.RequestExecutionTimer())
			{
				statistics = null;
			}
			return statistics;
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x000D8EC0 File Offset: 0x000D82C0
		internal static void StopTimer(SqlStatistics statistics)
		{
			if (statistics != null)
			{
				statistics.ReleaseAndUpdateExecutionTimer();
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001F5E RID: 8030 RVA: 0x000D8ED8 File Offset: 0x000D82D8
		// (set) Token: 0x06001F5F RID: 8031 RVA: 0x000D8EEC File Offset: 0x000D82EC
		internal bool WaitForDoneAfterRow
		{
			get
			{
				return this._waitForDoneAfterRow;
			}
			set
			{
				this._waitForDoneAfterRow = value;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001F60 RID: 8032 RVA: 0x000D8F00 File Offset: 0x000D8300
		internal bool WaitForReply
		{
			get
			{
				return this._waitForReply;
			}
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x000D8F14 File Offset: 0x000D8314
		internal SqlStatistics()
		{
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x000D8F28 File Offset: 0x000D8328
		internal void ContinueOnNewConnection()
		{
			this._startExecutionTimestamp = 0L;
			this._startFetchTimestamp = 0L;
			this._waitForDoneAfterRow = false;
			this._waitForReply = false;
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x000D8F54 File Offset: 0x000D8354
		internal IDictionary GetHashtable()
		{
			return new Hashtable
			{
				{
					"BuffersReceived",
					this._buffersReceived
				},
				{
					"BuffersSent",
					this._buffersSent
				},
				{
					"BytesReceived",
					this._bytesReceived
				},
				{
					"BytesSent",
					this._bytesSent
				},
				{
					"CursorOpens",
					this._cursorOpens
				},
				{
					"IduCount",
					this._iduCount
				},
				{
					"IduRows",
					this._iduRows
				},
				{
					"PreparedExecs",
					this._preparedExecs
				},
				{
					"Prepares",
					this._prepares
				},
				{
					"SelectCount",
					this._selectCount
				},
				{
					"SelectRows",
					this._selectRows
				},
				{
					"ServerRoundtrips",
					this._serverRoundtrips
				},
				{
					"SumResultSets",
					this._sumResultSets
				},
				{
					"Transactions",
					this._transactions
				},
				{
					"UnpreparedExecs",
					this._unpreparedExecs
				},
				{
					"ConnectionTime",
					ADP.TimerToMilliseconds(this._connectionTime)
				},
				{
					"ExecutionTime",
					ADP.TimerToMilliseconds(this._executionTime)
				},
				{
					"NetworkServerTime",
					ADP.TimerToMilliseconds(this._networkServerTime)
				}
			};
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x000D9104 File Offset: 0x000D8504
		internal bool RequestExecutionTimer()
		{
			if (this._startExecutionTimestamp == 0L)
			{
				ADP.TimerCurrent(out this._startExecutionTimestamp);
				return true;
			}
			return false;
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x000D9128 File Offset: 0x000D8528
		internal void RequestNetworkServerTimer()
		{
			if (this._startNetworkServerTimestamp == 0L)
			{
				ADP.TimerCurrent(out this._startNetworkServerTimestamp);
			}
			this._waitForReply = true;
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x000D9150 File Offset: 0x000D8550
		internal void ReleaseAndUpdateExecutionTimer()
		{
			if (this._startExecutionTimestamp > 0L)
			{
				this._executionTime += ADP.TimerCurrent() - this._startExecutionTimestamp;
				this._startExecutionTimestamp = 0L;
			}
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x000D9188 File Offset: 0x000D8588
		internal void ReleaseAndUpdateNetworkServerTimer()
		{
			if (this._waitForReply && this._startNetworkServerTimestamp > 0L)
			{
				this._networkServerTime += ADP.TimerCurrent() - this._startNetworkServerTimestamp;
				this._startNetworkServerTimestamp = 0L;
			}
			this._waitForReply = false;
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x000D91D0 File Offset: 0x000D85D0
		internal void Reset()
		{
			this._buffersReceived = 0L;
			this._buffersSent = 0L;
			this._bytesReceived = 0L;
			this._bytesSent = 0L;
			this._connectionTime = 0L;
			this._cursorOpens = 0L;
			this._executionTime = 0L;
			this._iduCount = 0L;
			this._iduRows = 0L;
			this._networkServerTime = 0L;
			this._preparedExecs = 0L;
			this._prepares = 0L;
			this._selectCount = 0L;
			this._selectRows = 0L;
			this._serverRoundtrips = 0L;
			this._sumResultSets = 0L;
			this._transactions = 0L;
			this._unpreparedExecs = 0L;
			this._waitForDoneAfterRow = false;
			this._waitForReply = false;
			this._startExecutionTimestamp = 0L;
			this._startNetworkServerTimestamp = 0L;
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x000D928C File Offset: 0x000D868C
		internal void SafeAdd(ref long value, long summand)
		{
			if (9223372036854775807L - value > summand)
			{
				value += summand;
				return;
			}
			value = long.MaxValue;
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x000D92BC File Offset: 0x000D86BC
		internal long SafeIncrement(ref long value)
		{
			if (value < 9223372036854775807L)
			{
				value += 1L;
			}
			return value;
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x000D92E0 File Offset: 0x000D86E0
		internal void UpdateStatistics()
		{
			if (this._closeTimestamp >= this._openTimestamp)
			{
				this.SafeAdd(ref this._connectionTime, this._closeTimestamp - this._openTimestamp);
				return;
			}
			this._connectionTime = long.MaxValue;
		}

		// Token: 0x040011B6 RID: 4534
		internal long _closeTimestamp;

		// Token: 0x040011B7 RID: 4535
		internal long _openTimestamp;

		// Token: 0x040011B8 RID: 4536
		internal long _startExecutionTimestamp;

		// Token: 0x040011B9 RID: 4537
		internal long _startFetchTimestamp;

		// Token: 0x040011BA RID: 4538
		internal long _startNetworkServerTimestamp;

		// Token: 0x040011BB RID: 4539
		internal long _buffersReceived;

		// Token: 0x040011BC RID: 4540
		internal long _buffersSent;

		// Token: 0x040011BD RID: 4541
		internal long _bytesReceived;

		// Token: 0x040011BE RID: 4542
		internal long _bytesSent;

		// Token: 0x040011BF RID: 4543
		internal long _connectionTime;

		// Token: 0x040011C0 RID: 4544
		internal long _cursorOpens;

		// Token: 0x040011C1 RID: 4545
		internal long _executionTime;

		// Token: 0x040011C2 RID: 4546
		internal long _iduCount;

		// Token: 0x040011C3 RID: 4547
		internal long _iduRows;

		// Token: 0x040011C4 RID: 4548
		internal long _networkServerTime;

		// Token: 0x040011C5 RID: 4549
		internal long _preparedExecs;

		// Token: 0x040011C6 RID: 4550
		internal long _prepares;

		// Token: 0x040011C7 RID: 4551
		internal long _selectCount;

		// Token: 0x040011C8 RID: 4552
		internal long _selectRows;

		// Token: 0x040011C9 RID: 4553
		internal long _serverRoundtrips;

		// Token: 0x040011CA RID: 4554
		internal long _sumResultSets;

		// Token: 0x040011CB RID: 4555
		internal long _transactions;

		// Token: 0x040011CC RID: 4556
		internal long _unpreparedExecs;

		// Token: 0x040011CD RID: 4557
		private bool _waitForDoneAfterRow;

		// Token: 0x040011CE RID: 4558
		private bool _waitForReply;
	}
}
