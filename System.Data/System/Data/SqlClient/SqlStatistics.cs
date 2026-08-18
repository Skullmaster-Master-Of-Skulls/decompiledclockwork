using System;
using System.Collections;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x0200030D RID: 781
	internal sealed class SqlStatistics
	{
		// Token: 0x060028C0 RID: 10432 RVA: 0x002B1D28 File Offset: 0x002B1128
		internal static SqlStatistics StartTimer(SqlStatistics statistics)
		{
			if (statistics != null && !statistics.RequestExecutionTimer())
			{
				statistics = null;
			}
			return statistics;
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x002B1D48 File Offset: 0x002B1148
		internal static void StopTimer(SqlStatistics statistics)
		{
			if (statistics != null)
			{
				statistics.ReleaseAndUpdateExecutionTimer();
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060028C2 RID: 10434 RVA: 0x002B1D68 File Offset: 0x002B1168
		// (set) Token: 0x060028C3 RID: 10435 RVA: 0x002B1D88 File Offset: 0x002B1188
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

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060028C4 RID: 10436 RVA: 0x002B1DA8 File Offset: 0x002B11A8
		internal bool WaitForReply
		{
			get
			{
				return this._waitForReply;
			}
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x002B1DC8 File Offset: 0x002B11C8
		internal SqlStatistics()
		{
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x002B1DE8 File Offset: 0x002B11E8
		internal void ContinueOnNewConnection()
		{
			this._startExecutionTimestamp = 0L;
			this._startFetchTimestamp = 0L;
			this._waitForDoneAfterRow = false;
			this._waitForReply = false;
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x002B1E18 File Offset: 0x002B1218
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

		// Token: 0x060028C8 RID: 10440 RVA: 0x002B1FC8 File Offset: 0x002B13C8
		internal bool RequestExecutionTimer()
		{
			if (this._startExecutionTimestamp == 0L)
			{
				ADP.TimerCurrent(out this._startExecutionTimestamp);
				return true;
			}
			return false;
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x002B1FF8 File Offset: 0x002B13F8
		internal void RequestNetworkServerTimer()
		{
			if (this._startNetworkServerTimestamp == 0L)
			{
				ADP.TimerCurrent(out this._startNetworkServerTimestamp);
			}
			this._waitForReply = true;
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x002B2028 File Offset: 0x002B1428
		internal void ReleaseAndUpdateExecutionTimer()
		{
			if (this._startExecutionTimestamp > 0L)
			{
				long num;
				ADP.TimerCurrent(out num);
				this._executionTime += num - this._startExecutionTimestamp;
				this._startExecutionTimestamp = 0L;
			}
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x002B2068 File Offset: 0x002B1468
		internal void ReleaseAndUpdateNetworkServerTimer()
		{
			if (this._waitForReply && this._startNetworkServerTimestamp > 0L)
			{
				long num;
				ADP.TimerCurrent(out num);
				this._networkServerTime += num - this._startNetworkServerTimestamp;
				this._startNetworkServerTimestamp = 0L;
			}
			this._waitForReply = false;
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x002B20B8 File Offset: 0x002B14B8
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

		// Token: 0x060028CD RID: 10445 RVA: 0x002B2178 File Offset: 0x002B1578
		internal void SafeAdd(ref long value, long summand)
		{
			if (9223372036854775807L - value > summand)
			{
				value += summand;
				return;
			}
			value = long.MaxValue;
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x002B21A8 File Offset: 0x002B15A8
		internal long SafeIncrement(ref long value)
		{
			if (value < 9223372036854775807L)
			{
				value += 1L;
			}
			return value;
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x002B21D8 File Offset: 0x002B15D8
		internal void UpdateStatistics()
		{
			if (this._closeTimestamp >= this._openTimestamp)
			{
				this.SafeAdd(ref this._connectionTime, this._closeTimestamp - this._openTimestamp);
				return;
			}
			this._connectionTime = long.MaxValue;
		}

		// Token: 0x0400197F RID: 6527
		internal long _closeTimestamp;

		// Token: 0x04001980 RID: 6528
		internal long _openTimestamp;

		// Token: 0x04001981 RID: 6529
		internal long _startExecutionTimestamp;

		// Token: 0x04001982 RID: 6530
		internal long _startFetchTimestamp;

		// Token: 0x04001983 RID: 6531
		internal long _startNetworkServerTimestamp;

		// Token: 0x04001984 RID: 6532
		internal long _buffersReceived;

		// Token: 0x04001985 RID: 6533
		internal long _buffersSent;

		// Token: 0x04001986 RID: 6534
		internal long _bytesReceived;

		// Token: 0x04001987 RID: 6535
		internal long _bytesSent;

		// Token: 0x04001988 RID: 6536
		internal long _connectionTime;

		// Token: 0x04001989 RID: 6537
		internal long _cursorOpens;

		// Token: 0x0400198A RID: 6538
		internal long _executionTime;

		// Token: 0x0400198B RID: 6539
		internal long _iduCount;

		// Token: 0x0400198C RID: 6540
		internal long _iduRows;

		// Token: 0x0400198D RID: 6541
		internal long _networkServerTime;

		// Token: 0x0400198E RID: 6542
		internal long _preparedExecs;

		// Token: 0x0400198F RID: 6543
		internal long _prepares;

		// Token: 0x04001990 RID: 6544
		internal long _selectCount;

		// Token: 0x04001991 RID: 6545
		internal long _selectRows;

		// Token: 0x04001992 RID: 6546
		internal long _serverRoundtrips;

		// Token: 0x04001993 RID: 6547
		internal long _sumResultSets;

		// Token: 0x04001994 RID: 6548
		internal long _transactions;

		// Token: 0x04001995 RID: 6549
		internal long _unpreparedExecs;

		// Token: 0x04001996 RID: 6550
		private bool _waitForDoneAfterRow;

		// Token: 0x04001997 RID: 6551
		private bool _waitForReply;
	}
}
