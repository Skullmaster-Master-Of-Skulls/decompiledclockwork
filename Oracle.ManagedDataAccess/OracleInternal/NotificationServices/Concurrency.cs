using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x02000182 RID: 386
	internal class Concurrency
	{
		// Token: 0x170002BD RID: 701
		// (set) Token: 0x06000ED2 RID: 3794 RVA: 0x00099544 File Offset: 0x00097744
		protected internal virtual long ScanTime
		{
			set
			{
				this.scanTime = value;
			}
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00099550 File Offset: 0x00097750
		protected internal Concurrency(int myIndex)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.index = myIndex;
			this.scanIndex = 0;
			this.clear();
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x000995A8 File Offset: 0x000977A8
		protected internal virtual void assign(Connection connection)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				this.assignedIndex = connection.ListIndex;
				this.active = true;
				connection.ConcurrencyIndex = this.index;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00099638 File Offset: 0x00097838
		protected internal virtual void connected()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.listFailed = false;
			this.scanIndex = this.assignedIndex;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00099688 File Offset: 0x00097888
		protected internal virtual void clear()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.assignedIndex = -1;
			this.active = false;
			this.listFailed = false;
			this.scanTime = 0L;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x000996E4 File Offset: 0x000978E4
		protected internal virtual void setListFailed()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.listFailed = true;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x04001121 RID: 4385
		protected internal long scanTime;

		// Token: 0x04001122 RID: 4386
		protected internal int index;

		// Token: 0x04001123 RID: 4387
		protected internal int assignedIndex;

		// Token: 0x04001124 RID: 4388
		protected internal int scanIndex;

		// Token: 0x04001125 RID: 4389
		protected internal bool active;

		// Token: 0x04001126 RID: 4390
		protected internal bool listFailed;
	}
}
