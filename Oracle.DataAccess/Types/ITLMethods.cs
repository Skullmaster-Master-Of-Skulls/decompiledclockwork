using System;
using System.Runtime.InteropServices;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x0200008C RID: 140
	internal class ITLMethods
	{
		// Token: 0x060006AB RID: 1707 RVA: 0x00044750 File Offset: 0x00043750
		private ITLMethods()
		{
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00044758 File Offset: 0x00043758
		public unsafe static string ToString(OpoITLValCtx* pValCtx1, int leadPrec, int trailPrec)
		{
			int num = 0;
			IntPtr ptr;
			try
			{
				num = OpsITL.ToString(pValCtx1, leadPrec, trailPrec, out ptr);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			string result = Marshal.PtrToStringUni(ptr);
			Marshal.FreeCoTaskMem(ptr);
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			return result;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000447B4 File Offset: 0x000437B4
		internal unsafe static int Compare(OpoITLValCtx* pITLValCtx1, OpoITLValCtx* pITLValCtx2)
		{
			int result = 0;
			try
			{
				OpsITL.Compare(pITLValCtx1, pITLValCtx2, ref result);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000447F0 File Offset: 0x000437F0
		internal unsafe static void FreeCtx(ref OpoITLValCtx* valCtx)
		{
			if (valCtx != (IntPtr)((UIntPtr)0))
			{
				try
				{
					OpsIDS.FreeValCtx(valCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
				}
				valCtx = (IntPtr)((UIntPtr)0);
			}
		}
	}
}
