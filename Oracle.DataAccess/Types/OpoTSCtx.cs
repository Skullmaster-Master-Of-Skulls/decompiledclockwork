using System;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000014 RID: 20
	internal class OpoTSCtx : IDisposable
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x0000E938 File Offset: 0x0000D938
		public OpoTSCtx(int year, int month, int day, int hour, int minute, int second, int fSecond, int tzHours, int tzMinutes, TimeStampType tsType)
		{
			int num = 0;
			try
			{
				switch (tsType)
				{
				case TimeStampType.TSType_TS:
					try
					{
						num = OpsTS.AllocValCtxFromData(year, month, day, hour, minute, second, fSecond, out this.m_pValCtx);
						goto IL_C1;
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						num = ErrRes.INT_ERR;
						throw;
					}
					break;
				case (TimeStampType)4:
				case TimeStampType.TSType_TSZ:
				case (TimeStampType)6:
					goto IL_8D;
				case TimeStampType.TSType_TSL:
					break;
				default:
					goto IL_8D;
				}
				try
				{
					num = OpsTSL.AllocValCtxFromData(year, month, day, hour, minute, second, fSecond, tzHours, tzMinutes, out this.m_pValCtx);
					goto IL_C1;
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					num = ErrRes.INT_ERR;
					throw;
				}
				try
				{
					IL_8D:
					num = OpsTSZ.AllocValCtxFromData(year, month, day, hour, minute, second, fSecond, tzHours, tzMinutes, null, out this.m_pValCtx);
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
					num = ErrRes.INT_ERR;
					throw;
				}
				IL_C1:;
			}
			finally
			{
				if (num != 0 && this.m_pValCtx != null)
				{
					try
					{
						OpsTS.FreeValCtx(this.m_pValCtx);
					}
					catch (Exception ex4)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex4);
						}
					}
					this.m_pValCtx = null;
				}
			}
			this.m_error = num;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000EA88 File Offset: 0x0000DA88
		public OpoTSCtx(int year, int month, int day, int hour, int minute, int second, int fSecond, string regionName)
		{
			int num = 0;
			try
			{
				num = OpsTSZ.AllocValCtxFromData(year, month, day, hour, minute, second, fSecond, 0, 0, regionName, out this.m_pValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0)
				{
					try
					{
						OpsTS.FreeValCtx(this.m_pValCtx);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
					}
					this.m_pValCtx = null;
				}
			}
			this.m_error = num;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000EB2C File Offset: 0x0000DB2C
		public OpoTSCtx(int year, int month, int day, int hour, int minute, int second, double milliSecond, int tzHours, int tzMinutes, TimeStampType tsType) : this(year, month, day, hour, minute, second, (int)(milliSecond * 1000000.0), tzHours, tzMinutes, tsType)
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000EB5C File Offset: 0x0000DB5C
		internal OpoTSCtx(byte[] binData, TimeStampType tsType)
		{
			int num = 0;
			try
			{
				switch (tsType)
				{
				case TimeStampType.TSType_TS:
					num = OpsTS.AllocValCtxFromBytes(binData, out this.m_pValCtx, 9);
					goto IL_6A;
				case TimeStampType.TSType_TSZ:
					num = OpsTSZ.AllocValCtxFromBytes(binData, out this.m_pValCtx, 9);
					goto IL_6A;
				case TimeStampType.TSType_TSL:
					num = OpsTSL.AllocValCtxFromBytes(binData, out this.m_pValCtx, 9);
					goto IL_6A;
				}
				num = OpsTSZ.AllocValCtxFromBytes(binData, out this.m_pValCtx, 9);
				IL_6A:;
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0 && this.m_pValCtx != null)
				{
					try
					{
						OpsTS.FreeValCtx(this.m_pValCtx);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
					}
					this.m_pValCtx = null;
				}
			}
			this.m_error = num;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000EC50 File Offset: 0x0000DC50
		public unsafe OpoTSCtx(DateTime dt, int tzHours, int tzMinutes, TimeStampType tsType)
		{
			OpoTSValCtx opoTSValCtx;
			TimeStamp.FillValCtxFromDateTime(&opoTSValCtx, dt);
			int num = 0;
			try
			{
				switch (tsType)
				{
				case TimeStampType.TSType_TS:
					try
					{
						num = OpsTS.AllocValCtxFromData((int)opoTSValCtx.m_year, (int)opoTSValCtx.m_month, (int)opoTSValCtx.m_day, (int)opoTSValCtx.m_hour, (int)opoTSValCtx.m_minute, (int)opoTSValCtx.m_second, opoTSValCtx.m_fSecond, out this.m_pValCtx);
						goto IL_152;
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						num = ErrRes.INT_ERR;
						throw;
					}
					break;
				case (TimeStampType)4:
				case TimeStampType.TSType_TSZ:
				case (TimeStampType)6:
					goto IL_F8;
				case TimeStampType.TSType_TSL:
					break;
				default:
					goto IL_F8;
				}
				try
				{
					num = OpsTSL.AllocValCtxFromData((int)opoTSValCtx.m_year, (int)opoTSValCtx.m_month, (int)opoTSValCtx.m_day, (int)opoTSValCtx.m_hour, (int)opoTSValCtx.m_minute, (int)opoTSValCtx.m_second, opoTSValCtx.m_fSecond, TimeStamp.LocalTZOffset.m_tzHours, TimeStamp.LocalTZOffset.m_tzMinutes, out this.m_pValCtx);
					goto IL_152;
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					num = ErrRes.INT_ERR;
					throw;
				}
				try
				{
					IL_F8:
					num = OpsTSZ.AllocValCtxFromData((int)opoTSValCtx.m_year, (int)opoTSValCtx.m_month, (int)opoTSValCtx.m_day, (int)opoTSValCtx.m_hour, (int)opoTSValCtx.m_minute, (int)opoTSValCtx.m_second, opoTSValCtx.m_fSecond, tzHours, tzMinutes, null, out this.m_pValCtx);
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
					num = ErrRes.INT_ERR;
					throw;
				}
				IL_152:;
			}
			finally
			{
				if (num != 0 && this.m_pValCtx != null)
				{
					try
					{
						OpsTS.FreeValCtx(this.m_pValCtx);
					}
					catch (Exception ex4)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex4);
						}
					}
					this.m_pValCtx = null;
				}
			}
			this.m_error = num;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000EE6C File Offset: 0x0000DE6C
		public unsafe OpoTSCtx(DateTime dt, string regionName)
		{
			OpoTSValCtx opoTSValCtx;
			TimeStamp.FillValCtxFromDateTime(&opoTSValCtx, dt);
			int num = 0;
			try
			{
				num = OpsTSZ.AllocValCtxFromData((int)opoTSValCtx.m_year, (int)opoTSValCtx.m_month, (int)opoTSValCtx.m_day, (int)opoTSValCtx.m_hour, (int)opoTSValCtx.m_minute, (int)opoTSValCtx.m_second, opoTSValCtx.m_fSecond, 0, 0, regionName, out this.m_pValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0 && this.m_pValCtx != null)
				{
					try
					{
						OpsTS.FreeValCtx(this.m_pValCtx);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
					}
					this.m_pValCtx = null;
				}
			}
			this.m_error = num;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000EF48 File Offset: 0x0000DF48
		public OpoTSCtx(string tsStr, TimeStampType tsType)
		{
			int num = 0;
			try
			{
				switch (tsType)
				{
				case TimeStampType.TSType_TS:
					try
					{
						num = OpsTS.AllocValCtxFromStr(tsStr, out this.m_pValCtx);
						goto IL_E8;
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						num = ErrRes.INT_ERR;
						throw;
					}
					break;
				case (TimeStampType)4:
				case TimeStampType.TSType_TSZ:
				case (TimeStampType)6:
					goto IL_9C;
				case TimeStampType.TSType_TSL:
					break;
				default:
					goto IL_9C;
				}
				OracleIntervalDS oracleIntervalDS = new OracleIntervalDS(0, TimeStamp.LocalTZOffset.m_tzHours, TimeStamp.LocalTZOffset.m_tzMinutes, 0, 0);
				try
				{
					num = OpsTSL.AllocValCtxFromStr(tsStr, oracleIntervalDS.GetValCtx(), out this.m_pValCtx);
					goto IL_E8;
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					num = ErrRes.INT_ERR;
					throw;
				}
				IL_9C:
				OracleIntervalDS oracleIntervalDS2 = new OracleIntervalDS(0, TimeStamp.LocalTZOffset.m_tzHours, TimeStamp.LocalTZOffset.m_tzMinutes, 0, 0);
				try
				{
					num = OpsTSZ.AllocValCtxFromStr(tsStr, oracleIntervalDS2.GetValCtx(), out this.m_pValCtx);
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
					num = ErrRes.INT_ERR;
					throw;
				}
				IL_E8:;
			}
			finally
			{
				if (num != 0 && this.m_pValCtx != null)
				{
					try
					{
						OpsTS.FreeValCtx(this.m_pValCtx);
					}
					catch (Exception ex4)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex4);
						}
					}
					this.m_pValCtx = null;
				}
			}
			this.m_error = num;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000F0BC File Offset: 0x0000E0BC
		internal OpoTSCtx(TimeStampType tsType)
		{
			int error;
			if ((error = OpoTSCtx.AllocValCtx(ref this.m_pValCtx, tsType)) != 0)
			{
				this.m_pValCtx = null;
			}
			this.m_error = error;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000F0F0 File Offset: 0x0000E0F0
		internal unsafe OpoTSCtx(OpoTSValCtx* pCtx)
		{
			this.m_pValCtx = pCtx;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000F100 File Offset: 0x0000E100
		~OpoTSCtx()
		{
			this.Dispose();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000F12C File Offset: 0x0000E12C
		public unsafe void Dispose()
		{
			if (this.m_pValCtx != null)
			{
				switch (this.m_pValCtx->m_type)
				{
				case 3:
					try
					{
						OpsTS.FreeValCtx(this.m_pValCtx);
						goto IL_B1;
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						goto IL_B1;
					}
					break;
				case 4:
				case 6:
					goto IL_93;
				case 5:
					goto IL_75;
				case 7:
					break;
				default:
					goto IL_93;
				}
				try
				{
					OpsTSL.FreeValCtx(this.m_pValCtx);
					goto IL_B1;
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					goto IL_B1;
				}
				try
				{
					IL_75:
					OpsTSZ.FreeValCtx(this.m_pValCtx);
					goto IL_B1;
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
					goto IL_B1;
				}
				try
				{
					IL_93:
					OpsTSZ.FreeValCtx(this.m_pValCtx);
				}
				catch (Exception ex4)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex4);
					}
				}
				IL_B1:
				this.m_pValCtx = null;
			}
			try
			{
				GC.SuppressFinalize(this);
			}
			catch
			{
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000F240 File Offset: 0x0000E240
		internal unsafe static int AllocValCtx(ref OpoTSValCtx* pValCtx, TimeStampType tsType)
		{
			int result = 0;
			try
			{
				switch (tsType)
				{
				case TimeStampType.TSType_TS:
					result = OpsTS.AllocValCtx(ref pValCtx);
					goto IL_44;
				case TimeStampType.TSType_TSZ:
					result = OpsTSZ.AllocValCtx(ref pValCtx);
					goto IL_44;
				case TimeStampType.TSType_TSL:
					result = OpsTSL.AllocValCtx(ref pValCtx);
					goto IL_44;
				}
				result = OpsTSZ.AllocValCtx(ref pValCtx);
				IL_44:;
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

		// Token: 0x0400009B RID: 155
		internal unsafe OpoTSValCtx* m_pValCtx;

		// Token: 0x0400009C RID: 156
		internal int m_error;
	}
}
