using System;
using System.Runtime.InteropServices;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x0200011F RID: 287
	internal class OpoDecCtx : IDisposable
	{
		// Token: 0x06000BE8 RID: 3048 RVA: 0x00078976 File Offset: 0x00077976
		internal OpoDecCtx(IntPtr numCtx)
		{
			this.m_pValCtx = numCtx;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00078988 File Offset: 0x00077988
		internal OpoDecCtx(IntPtr numCtx, out int numberType, out bool bPositive, out bool bZero)
		{
			int num = 0;
			int num2 = 0;
			numberType = 1;
			bPositive = false;
			bZero = false;
			try
			{
				this.m_error = OpsDec.GetInfo(numCtx, out numberType, out num, out num2, 1);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (this.m_error == 0)
			{
				this.m_pValCtx = numCtx;
				if (num == 1)
				{
					bPositive = true;
				}
				else
				{
					bPositive = false;
				}
				bZero = false;
				if (num2 == 1)
				{
					bZero = true;
					return;
				}
				bZero = false;
			}
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00078A0C File Offset: 0x00077A0C
		internal OpoDecCtx(string numStr, string numFmt, out int numberType, out bool bPositive, out bool bZero)
		{
			int num = 0;
			int num2 = 0;
			numberType = 1;
			bPositive = false;
			bZero = false;
			if (numStr == "~")
			{
				try
				{
					try
					{
						this.m_error = OpsDec.AllocValCtxForPosInf(out this.m_pValCtx);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						this.m_error = ErrRes.INT_ERR;
						throw;
					}
					return;
				}
				finally
				{
					if (this.m_error != 0)
					{
						OpoDecCtx.FreeCtx(ref this.m_pValCtx);
					}
					else
					{
						numberType = 3;
						bPositive = true;
						bZero = false;
					}
				}
			}
			if (numStr == "-~")
			{
				try
				{
					try
					{
						this.m_error = OpsDec.AllocValCtxForNegInf(out this.m_pValCtx);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
						this.m_error = ErrRes.INT_ERR;
						throw;
					}
					return;
				}
				finally
				{
					if (this.m_error != 0)
					{
						OpoDecCtx.FreeCtx(ref this.m_pValCtx);
					}
					else
					{
						numberType = 4;
						bPositive = false;
						bZero = false;
					}
				}
			}
			try
			{
				this.m_error = OpsDec.AllocValCtxWInfoFromStr(numStr, numFmt, out this.m_pValCtx, out numberType, out num, out num2);
			}
			catch (Exception ex3)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex3);
				}
				this.m_error = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (this.m_error != 0)
				{
					OpoDecCtx.FreeCtx(ref this.m_pValCtx);
				}
				else
				{
					if (num == 1)
					{
						bPositive = true;
					}
					else
					{
						bPositive = false;
					}
					if (num2 == 1)
					{
						bZero = true;
					}
					else
					{
						bZero = false;
					}
				}
			}
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00078BA8 File Offset: 0x00077BA8
		internal unsafe OpoDecCtx(int intX, out int numberType, out bool bPositive, out bool bZero)
		{
			numberType = 1;
			bPositive = false;
			bZero = false;
			try
			{
				this.m_error = OpsDec.AllocValCtxFromInteger((void*)(&intX), 4, ref this.m_pValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				this.m_error = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (this.m_error != 0)
				{
					OpoDecCtx.FreeCtx(ref this.m_pValCtx);
				}
				else
				{
					numberType = 1;
					if (intX > 0)
					{
						bPositive = true;
					}
					else
					{
						bPositive = false;
					}
					if (intX == 0)
					{
						bZero = true;
					}
					else
					{
						bZero = false;
					}
				}
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00078C48 File Offset: 0x00077C48
		internal unsafe OpoDecCtx(long longX, out int numberType, out bool bPositive, out bool bZero)
		{
			numberType = 1;
			bPositive = false;
			bZero = false;
			try
			{
				this.m_error = OpsDec.AllocValCtxFromInteger((void*)(&longX), 8, ref this.m_pValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				this.m_error = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (this.m_error != 0)
				{
					OpoDecCtx.FreeCtx(ref this.m_pValCtx);
				}
				else
				{
					numberType = 1;
					if (longX > 0L)
					{
						bPositive = true;
					}
					else
					{
						bPositive = false;
					}
					if (longX == 0L)
					{
						bZero = true;
					}
					else
					{
						bZero = false;
					}
				}
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00078CEC File Offset: 0x00077CEC
		internal unsafe OpoDecCtx(float floatX, out int numberType, out bool bPositive, out bool bZero)
		{
			int num = 0;
			int num2 = 0;
			bPositive = false;
			bZero = false;
			numberType = 0;
			try
			{
				this.m_error = OpsDec.AllocValCtxWInfoFromReal((void*)(&floatX), 4, out this.m_pValCtx, out numberType, out num, out num2);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				this.m_error = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (this.m_error != 0)
				{
					OpoDecCtx.FreeCtx(ref this.m_pValCtx);
				}
				else
				{
					if (num == 1)
					{
						bPositive = true;
					}
					else
					{
						bPositive = false;
					}
					if (num2 == 1)
					{
						bZero = true;
					}
					else
					{
						bZero = false;
					}
				}
			}
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00078D94 File Offset: 0x00077D94
		internal unsafe OpoDecCtx(double doubleX, out int numberType, out bool bPositive, out bool bZero)
		{
			int num = 0;
			int num2 = 0;
			bPositive = false;
			bZero = false;
			numberType = 0;
			try
			{
				this.m_error = OpsDec.AllocValCtxWInfoFromReal((void*)(&doubleX), 8, out this.m_pValCtx, out numberType, out num, out num2);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				this.m_error = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (this.m_error != 0)
				{
					OpoDecCtx.FreeCtx(ref this.m_pValCtx);
				}
				else
				{
					if (num == 1)
					{
						bPositive = true;
					}
					else
					{
						bPositive = false;
					}
					if (num2 == 1)
					{
						bZero = true;
					}
					else
					{
						bZero = false;
					}
				}
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00078E3C File Offset: 0x00077E3C
		internal OpoDecCtx(decimal decimalX, out int numberType, out bool bPositive, out bool bZero)
		{
			this.m_pValCtx = Marshal.AllocCoTaskMem(22);
			DecimalConv.GetBytes(decimalX, this.m_pValCtx);
			byte[] bytes = BitConverter.GetBytes(decimal.GetBits(decimalX)[3]);
			if (bytes[2] == 0)
			{
				numberType = 1;
			}
			else
			{
				numberType = 2;
			}
			if (decimalX > 0m)
			{
				bPositive = true;
			}
			else
			{
				bPositive = false;
			}
			if (decimalX == 0m)
			{
				bZero = true;
				return;
			}
			bZero = false;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00078EB0 File Offset: 0x00077EB0
		~OpoDecCtx()
		{
			this.Dispose();
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00078EDC File Offset: 0x00077EDC
		public void Dispose()
		{
			if (!this.m_DoNotFreeValCtx)
			{
				OpoDecCtx.FreeCtx(ref this.m_pValCtx);
			}
			try
			{
				GC.SuppressFinalize(this);
			}
			catch
			{
			}
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00078F18 File Offset: 0x00077F18
		internal static void FreeCtx(ref IntPtr numCtx)
		{
			if (numCtx != IntPtr.Zero)
			{
				try
				{
					OpsDec.FreeValCtx(numCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
				}
				numCtx = IntPtr.Zero;
			}
		}

		// Token: 0x0400096F RID: 2415
		internal IntPtr m_pValCtx;

		// Token: 0x04000970 RID: 2416
		internal int m_error;

		// Token: 0x04000971 RID: 2417
		internal bool m_DoNotFreeValCtx;
	}
}
