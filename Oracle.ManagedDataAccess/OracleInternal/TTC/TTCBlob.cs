using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.TTC
{
	// Token: 0x0200021C RID: 540
	internal class TTCBlob : TTCLob
	{
		// Token: 0x06001428 RID: 5160 RVA: 0x000D5CE4 File Offset: 0x000D3EE4
		internal TTCBlob(MarshallingEngine mEngine) : base(mEngine)
		{
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x000D5CF0 File Offset: 0x000D3EF0
		internal override byte[] CreateTemporaryLob(bool bCache, bool bNClob, int duration)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			byte[] sourceLobLocator;
			try
			{
				if (12 == duration)
				{
					throw new Exception("Invalid duration in CreateTemporaryLob");
				}
				base.Initialize();
				this.m_lobOperation = 272L;
				this.m_sourceLobLocator = new byte[40];
				this.m_sourceLobLocator[1] = 84;
				this.m_characterSet = 1;
				this.m_lobAmount = (long)duration;
				this.m_bSendLobAmount = true;
				this.m_destinationOffset = 113L;
				this.m_destinationLength = duration;
				this.m_bNullO2U = true;
				this.m_lobSCN = new int[1];
				this.m_lobSCN[0] = (bCache ? 1 : 0);
				this.m_lobSCNLength = 1;
				base.WriteFunctionHeader();
				base.WriteLobOperation();
				this.m_marshallingEngine.m_oraBufWriter.FlushData();
				base.ReceiveResponse(null);
				sourceLobLocator = this.m_sourceLobLocator;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
			return sourceLobLocator;
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x000D5E10 File Offset: 0x000D4010
		internal bool Open(byte[] lobLocator, int mode)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			bool result;
			try
			{
				int mode2 = 2;
				if (mode == 0)
				{
					mode2 = 1;
				}
				result = base.OpenLob(lobLocator, mode2, 32768);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x000D5E94 File Offset: 0x000D4094
		internal bool Close(byte[] lobLocator)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			bool result;
			try
			{
				result = base.CloseLob(lobLocator, 65536);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
			return result;
		}
	}
}
