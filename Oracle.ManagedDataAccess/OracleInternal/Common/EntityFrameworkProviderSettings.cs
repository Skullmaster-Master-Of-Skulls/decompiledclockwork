using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.EntityFramework;

namespace OracleInternal.Common
{
	// Token: 0x02000087 RID: 135
	internal class EntityFrameworkProviderSettings : EFProviderSettings.IEFProviderSettings
	{
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0003A538 File Offset: 0x00038738
		EFProviderSettings.EFOracleProviderType EFProviderSettings.IEFProviderSettings.ThickOrThin
		{
			get
			{
				return EFProviderSettings.EFOracleProviderType.Thin;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0003A53C File Offset: 0x0003873C
		int EFProviderSettings.IEFProviderSettings.InitialLONGFetchSize
		{
			get
			{
				return ConfigBaseClass.m_InitialLONGFetchSize;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0003A544 File Offset: 0x00038744
		int EFProviderSettings.IEFProviderSettings.InitialLOBFetchSize
		{
			get
			{
				return ConfigBaseClass.m_InitialLOBFetchSize;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0003A54C File Offset: 0x0003874C
		bool EFProviderSettings.IEFProviderSettings.TracingEnabled
		{
			get
			{
				return ConfigBaseClass.m_TraceLevel > 0;
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0003A558 File Offset: 0x00038758
		void EFProviderSettings.IEFProviderSettings.Trace(EFProviderSettings.EFTraceLevel level, string message)
		{
			OracleTraceTag traceTag = OracleTraceTag.Entry;
			if (level == EFProviderSettings.EFTraceLevel.Entry)
			{
				traceTag = OracleTraceTag.Exit;
			}
			Trace.Write(OracleTraceLevel.Public, traceTag, new string[]
			{
				message
			});
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0003A588 File Offset: 0x00038788
		int EFProviderSettings.IEFProviderSettings.GetMaxPrecision(string typeName)
		{
			return ConfigBaseClass.GetMaxPrecision(typeName, false);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0003A594 File Offset: 0x00038794
		string EFProviderSettings.IEFProviderSettings.GetErrorMessage(int errorCode, params string[] args)
		{
			return OracleStringResourceManager.GetErrorMesg(errorCode, args);
		}
	}
}
