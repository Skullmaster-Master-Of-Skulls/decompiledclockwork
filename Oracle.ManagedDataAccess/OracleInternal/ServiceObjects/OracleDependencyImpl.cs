using System;
using System.Collections;
using System.Net;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001A7 RID: 423
	internal class OracleDependencyImpl
	{
		// Token: 0x06000FDA RID: 4058 RVA: 0x000A3BDC File Offset: 0x000A1DDC
		internal OracleDependencyImpl(bool isNotifiedOnce, long timeout, bool isPersistent)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			this.m_bIsRegistered = false;
			this.m_bIsNotifiedOnce = isNotifiedOnce;
			this.m_bIsPersistent = isPersistent;
			this.m_timeout = timeout;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
			}
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x000A3C68 File Offset: 0x000A1E68
		internal static string GetMachineAddress()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			string result;
			try
			{
				IPHostEntry iphostEntry = Dns.Resolve(Dns.GetHostName());
				IPAddress ipaddress = iphostEntry.AddressList[0];
				result = ipaddress.ToString();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x000A3CF0 File Offset: 0x000A1EF0
		internal void SetRegisterInfo(bool isNotifiedOnce, bool isPersistent, long timeout)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			this.m_bIsNotifiedOnce = isNotifiedOnce;
			this.m_bIsPersistent = isPersistent;
			this.m_timeout = timeout;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
			}
		}

		// Token: 0x04001278 RID: 4728
		internal static int m_portForlistening = -1;

		// Token: 0x04001279 RID: 4729
		internal static string s_machineAddress = OracleDependencyImpl.GetMachineAddress();

		// Token: 0x0400127A RID: 4730
		internal bool m_bIsRegistered;

		// Token: 0x0400127B RID: 4731
		internal long m_clientRegistrationId;

		// Token: 0x0400127C RID: 4732
		internal int m_RegIdFromServer;

		// Token: 0x0400127D RID: 4733
		internal bool m_bIsEnabled;

		// Token: 0x0400127E RID: 4734
		internal object m_syncList = new object();

		// Token: 0x0400127F RID: 4735
		internal ArrayList m_queryIDList = new ArrayList();

		// Token: 0x04001280 RID: 4736
		internal bool m_bIsNotifiedOnce;

		// Token: 0x04001281 RID: 4737
		internal bool m_bIsPersistent;

		// Token: 0x04001282 RID: 4738
		internal long m_timeout;

		// Token: 0x04001283 RID: 4739
		internal bool m_bExcludeRowId;

		// Token: 0x04001284 RID: 4740
		internal bool m_bIncludeRowId;

		// Token: 0x04001285 RID: 4741
		internal bool m_bQueryBasedNTFN = true;

		// Token: 0x04001286 RID: 4742
		internal ArrayList m_regList = ArrayList.Synchronized(new ArrayList());
	}
}
