using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.NotificationServices
{
	// Token: 0x0200018D RID: 397
	internal class Publisher
	{
		// Token: 0x06000F2F RID: 3887 RVA: 0x0009E1F0 File Offset: 0x0009C3F0
		public Publisher(ONS o, string c)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				this.oems = o;
				this.realStartup(c);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0009E274 File Offset: 0x0009C474
		private void realStartup(string c)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				this.lock_Renamed = new object();
				this.component = c;
				this.eventId = 1;
				this.oems.addPublisher(this);
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

		// Token: 0x06000F31 RID: 3889 RVA: 0x0009E308 File Offset: 0x0009C508
		public virtual void close()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			try
			{
				this.oems.removePublisher(this);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)270532608, ex, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
				}
			}
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0009E384 File Offset: 0x0009C584
		protected internal virtual void id(int i)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097408, new string[0]);
			}
			this.id_Renamed_Field = i;
			this.idString = this.oems.processId() + i.ToString();
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2097664, new string[0]);
			}
		}

		// Token: 0x040011C5 RID: 4549
		private string component;

		// Token: 0x040011C6 RID: 4550
		private ONS oems;

		// Token: 0x040011C7 RID: 4551
		private object lock_Renamed;

		// Token: 0x040011C8 RID: 4552
		private int eventId;

		// Token: 0x040011C9 RID: 4553
		private int id_Renamed_Field;

		// Token: 0x040011CA RID: 4554
		private string idString;
	}
}
