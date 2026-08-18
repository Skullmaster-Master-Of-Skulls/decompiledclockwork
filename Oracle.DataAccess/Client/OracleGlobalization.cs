using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000050 RID: 80
	[StructLayout(LayoutKind.Sequential)]
	public sealed class OracleGlobalization : ICloneable, IDisposable
	{
		// Token: 0x0600037B RID: 891 RVA: 0x00029107 File Offset: 0x00028107
		static OracleGlobalization()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0002911F File Offset: 0x0002811F
		// (set) Token: 0x0600037D RID: 893 RVA: 0x0002913F File Offset: 0x0002813F
		public string Calendar
		{
			get
			{
				if (this.m_oraGlob.m_calendar == null)
				{
					return string.Empty;
				}
				return this.m_oraGlob.m_calendar;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.ValidateSetting(0, value);
					this.m_oraGlob.m_calendar = value;
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0002915D File Offset: 0x0002815D
		public string ClientCharacterSet
		{
			get
			{
				if (this.m_oraGlob.m_clientCharacterSet == null)
				{
					return string.Empty;
				}
				return this.m_oraGlob.m_clientCharacterSet;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0002917D File Offset: 0x0002817D
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0002919D File Offset: 0x0002819D
		public string Comparison
		{
			get
			{
				if (this.m_oraGlob.m_comparison == null)
				{
					return string.Empty;
				}
				return this.m_oraGlob.m_comparison;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.ValidateSetting(2, value);
					this.m_oraGlob.m_comparison = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000381 RID: 897 RVA: 0x000291CC File Offset: 0x000281CC
		// (set) Token: 0x06000382 RID: 898 RVA: 0x000291EC File Offset: 0x000281EC
		public string Currency
		{
			get
			{
				if (this.m_oraGlob.m_currency == null)
				{
					return string.Empty;
				}
				return this.m_oraGlob.m_currency;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.ValidateSetting(3, value);
					this.m_oraGlob.m_currency = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0002921B File Offset: 0x0002821B
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0002923B File Offset: 0x0002823B
		public string DateFormat
		{
			get
			{
				if (this.m_oraGlob.m_dateFormat == null)
				{
					return string.Empty;
				}
				return this.m_oraGlob.m_dateFormat;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.ValidateSetting(4, value);
					this.m_oraGlob.m_dateFormat = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0002926A File Offset: 0x0002826A
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0002928A File Offset: 0x0002828A
		public string DateLanguage
		{
			get
			{
				if (this.m_oraGlob.m_dateLanguage == null)
				{
					return string.Empty;
				}
				return this.m_oraGlob.m_dateLanguage;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.ValidateSetting(5, value);
					this.m_oraGlob.m_dateLanguage = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000387 RID: 903 RVA: 0x000292B9 File Offset: 0x000282B9
		// (set) Token: 0x06000388 RID: 904 RVA: 0x000292D9 File Offset: 0x000282D9
		public string DualCurrency
		{
			get
			{
				if (this.m_oraGlob.m_dualCurrency == null)
				{
					return string.Empty;
				}
				return this.m_oraGlob.m_dualCurrency;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.ValidateSetting(6, value);
					this.m_oraGlob.m_dualCurrency = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00029308 File Offset: 0x00028308
		// (set) Token: 0x0600038A RID: 906 RVA: 0x00029328 File Offset: 0x00028328
		public string ISOCurrency
		{
			get
			{
				if (this.m_oraGlob.m_isoCurrency == null)
				{
					return string.Empty;
				}
				return this.m_oraGlob.m_isoCurrency;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.ValidateSetting(7, value);
					this.m_oraGlob.m_isoCurrency = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00029357 File Offset: 0x00028357
		// (set) Token: 0x0600038C RID: 908 RVA: 0x00029378 File Offset: 0x00028378
		public string Language
		{
			get
			{
				if (this.m_oraGlob.m_language == null)
				{
					return string.Empty;
				}
				return this.m_oraGlob.m_language;
			}
			set
			{
				if (!this.m_disposed)
				{
					IntPtr zero = IntPtr.Zero;
					string timeZone = this.m_oraGlob.m_timeZone;
					this.ValidateSetting(8, value);
					try
					{
						OpsCom.RefreshGlobInfo(this.m_nlsCtx, out zero, 0);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						throw;
					}
					Marshal.PtrToStructure(zero, this.m_oraGlob);
					this.m_oraGlob.m_timeZone = timeZone;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00029404 File Offset: 0x00028404
		// (set) Token: 0x0600038E RID: 910 RVA: 0x00029424 File Offset: 0x00028424
		public string LengthSemantics
		{
			get
			{
				if (this.m_oraGlob.m_lengthSemantics == null)
				{
					return string.Empty;
				}
				return this.m_oraGlob.m_lengthSemantics;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.ValidateSetting(9, value);
					this.m_oraGlob.m_lengthSemantics = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00029454 File Offset: 0x00028454
		// (set) Token: 0x06000390 RID: 912 RVA: 0x00029470 File Offset: 0x00028470
		public bool NCharConversionException
		{
			get
			{
				return this.m_oraGlob.m_nCharConvExcp.ToLower() == "true";
			}
			set
			{
				if (!this.m_disposed)
				{
					this.ValidateSetting(10, value ? "true" : "false");
					this.m_oraGlob.m_nCharConvExcp = value.ToString().ToLower();
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000391 RID: 913 RVA: 0x000294C4 File Offset: 0x000284C4
		// (set) Token: 0x06000392 RID: 914 RVA: 0x000294E4 File Offset: 0x000284E4
		public string NumericCharacters
		{
			get
			{
				if (this.m_oraGlob.m_numericCharacters == null)
				{
					return string.Empty;
				}
				return this.m_oraGlob.m_numericCharacters;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.ValidateSetting(11, value);
					this.m_oraGlob.m_numericCharacters = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00029514 File Offset: 0x00028514
		// (set) Token: 0x06000394 RID: 916 RVA: 0x00029534 File Offset: 0x00028534
		public string Sort
		{
			get
			{
				if (this.m_oraGlob.m_sort == null)
				{
					return string.Empty;
				}
				return this.m_oraGlob.m_sort;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.ValidateSetting(13, value);
					this.m_oraGlob.m_sort = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000395 RID: 917 RVA: 0x00029564 File Offset: 0x00028564
		// (set) Token: 0x06000396 RID: 918 RVA: 0x00029584 File Offset: 0x00028584
		public string Territory
		{
			get
			{
				if (this.m_oraGlob.m_territory == null)
				{
					return string.Empty;
				}
				return this.m_oraGlob.m_territory;
			}
			set
			{
				if (!this.m_disposed)
				{
					IntPtr zero = IntPtr.Zero;
					string timeZone = this.m_oraGlob.m_timeZone;
					this.ValidateSetting(14, value);
					try
					{
						OpsCom.RefreshGlobInfo(this.m_nlsCtx, out zero, 0);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						throw;
					}
					Marshal.PtrToStructure(zero, this.m_oraGlob);
					this.m_oraGlob.m_timeZone = timeZone;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000397 RID: 919 RVA: 0x00029610 File Offset: 0x00028610
		// (set) Token: 0x06000398 RID: 920 RVA: 0x00029630 File Offset: 0x00028630
		public string TimeStampFormat
		{
			get
			{
				if (this.m_oraGlob.m_timeStampFormat == null)
				{
					return string.Empty;
				}
				return this.m_oraGlob.m_timeStampFormat;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.ValidateSetting(15, value);
					this.m_oraGlob.m_timeStampFormat = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00029660 File Offset: 0x00028660
		// (set) Token: 0x0600039A RID: 922 RVA: 0x00029680 File Offset: 0x00028680
		public string TimeStampTZFormat
		{
			get
			{
				if (this.m_oraGlob.m_timeStampTZFormat == null)
				{
					return string.Empty;
				}
				return this.m_oraGlob.m_timeStampTZFormat;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.ValidateSetting(16, value);
					this.m_oraGlob.m_timeStampTZFormat = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600039B RID: 923 RVA: 0x000296B0 File Offset: 0x000286B0
		// (set) Token: 0x0600039C RID: 924 RVA: 0x000296D0 File Offset: 0x000286D0
		public string TimeZone
		{
			get
			{
				if (this.m_oraGlob.m_timeZone != null)
				{
					return this.m_oraGlob.m_timeZone;
				}
				return string.Empty;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.m_oraGlob.m_timeZone = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000296F8 File Offset: 0x000286F8
		internal OracleGlobalization()
		{
			int num = 0;
			this.m_oraGlob = new OraGlobStruct();
			try
			{
				num = OpsCom.AllocNlsCtx(out this.m_nlsCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				OracleException.HandleError(num, null, IntPtr.Zero, null);
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00029764 File Offset: 0x00028764
		public object Clone()
		{
			return new OracleGlobalization
			{
				m_oraGlob = 
				{
					m_calendar = this.m_oraGlob.m_calendar,
					m_clientCharacterSet = this.m_oraGlob.m_clientCharacterSet,
					m_comparison = this.m_oraGlob.m_comparison,
					m_currency = this.m_oraGlob.m_currency,
					m_dateFormat = this.m_oraGlob.m_dateFormat,
					m_dateLanguage = this.m_oraGlob.m_dateLanguage,
					m_dualCurrency = this.m_oraGlob.m_dualCurrency,
					m_isoCurrency = this.m_oraGlob.m_isoCurrency,
					m_language = this.m_oraGlob.m_language,
					m_lengthSemantics = this.m_oraGlob.m_lengthSemantics,
					m_nCharConvExcp = this.m_oraGlob.m_nCharConvExcp,
					m_numericCharacters = this.m_oraGlob.m_numericCharacters,
					m_sort = this.m_oraGlob.m_sort,
					m_territory = this.m_oraGlob.m_territory,
					m_timeStampFormat = this.m_oraGlob.m_timeStampFormat,
					m_timeStampTZFormat = this.m_oraGlob.m_timeStampTZFormat,
					m_timeZone = this.m_oraGlob.m_timeZone
				}
			};
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000298F0 File Offset: 0x000288F0
		public static OracleGlobalization GetClientInfo()
		{
			int num = 0;
			OracleGlobalization result;
			lock (OracleGlobalization.s_lockObj)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleGlobalization::GetClientInfo(1)\n"
					});
				}
				OracleGlobalization oracleGlobalization = new OracleGlobalization();
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsCom.GetClientInfo(ref zero);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, null, IntPtr.Zero, null);
					}
				}
				Marshal.PtrToStructure(zero, oracleGlobalization.m_oraGlob);
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleGlobalization::GetClientInfo(1)\n"
					});
				}
				result = oracleGlobalization;
			}
			return result;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000299D4 File Offset: 0x000289D4
		public static void GetClientInfo(OracleGlobalization oraGlob)
		{
			int num = 0;
			lock (OracleGlobalization.s_lockObj)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleGlobalization::GetClientInfo(2)\n"
					});
				}
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsCom.GetClientInfo(ref zero);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, null, IntPtr.Zero, null);
					}
				}
				Marshal.PtrToStructure(zero, oraGlob.m_oraGlob);
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleGlobalization::GetClientInfo(2)\n"
					});
				}
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00029AA8 File Offset: 0x00028AA8
		public static OracleGlobalization GetThreadInfo()
		{
			int num = 0;
			OracleGlobalization result;
			lock (OracleGlobalization.s_lockObj)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleGlobalization::GetThreadInfo(1)\n"
					});
				}
				OracleGlobalization oracleGlobalization = new OracleGlobalization();
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsCom.GetThreadInfo(ref zero);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, null, IntPtr.Zero, null);
					}
				}
				Marshal.PtrToStructure(zero, oracleGlobalization.m_oraGlob);
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleGlobalization::GetThreadInfo(1)\n"
					});
				}
				result = oracleGlobalization;
			}
			return result;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00029B8C File Offset: 0x00028B8C
		public static void GetThreadInfo(OracleGlobalization oraGlob)
		{
			int num = 0;
			lock (OracleGlobalization.s_lockObj)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleGlobalization::GetThreadInfo(2)\n"
					});
				}
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsCom.GetThreadInfo(ref zero);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, null, IntPtr.Zero, null);
					}
				}
				Marshal.PtrToStructure(zero, oraGlob.m_oraGlob);
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleGlobalization::GetThreadInfo(2)\n"
					});
				}
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00029C60 File Offset: 0x00028C60
		public static void SetThreadInfo(OracleGlobalization oraGlob)
		{
			int num = 0;
			lock (OracleGlobalization.s_lockObj)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleGlobalization::SetThreadInfo()\n"
					});
				}
				try
				{
					num = OpsCom.SetThreadInfo(oraGlob.m_oraGlob);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(12705, null, IntPtr.Zero, null);
					}
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleGlobalization::SetThreadInfo()\n"
					});
				}
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00029D28 File Offset: 0x00028D28
		public void Dispose()
		{
			lock (this.m_lockObj)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleGlobalization::Dispose()\n"
					});
				}
				if (!this.m_disposed)
				{
					this.m_disposed = true;
					if (this.m_nlsCtx != IntPtr.Zero)
					{
						try
						{
							OpsCom.FreeNlsCtx(this.m_nlsCtx);
							this.m_nlsCtx = IntPtr.Zero;
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
						}
					}
				}
				GC.SuppressFinalize(this);
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleGlobalization::Dispose()\n"
					});
				}
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00029DFC File Offset: 0x00028DFC
		internal void ValidateSetting(int paramKey, string paramVal)
		{
			if ((paramVal == null || paramVal.Length == 0) && paramKey != 17)
			{
				OracleException.HandleError(1741, null, IntPtr.Zero, null);
			}
			int num = 0;
			try
			{
				num = OpsCom.ValidateGlobInfo(this.m_nlsCtx, paramKey, paramVal);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(12705, null, IntPtr.Zero, null);
				}
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00029E80 File Offset: 0x00028E80
		~OracleGlobalization()
		{
			this.Dispose();
		}

		// Token: 0x04000277 RID: 631
		internal OraGlobStruct m_oraGlob;

		// Token: 0x04000278 RID: 632
		internal IntPtr m_nlsCtx;

		// Token: 0x04000279 RID: 633
		internal bool m_disposed;

		// Token: 0x0400027A RID: 634
		private object m_lockObj = new object();

		// Token: 0x0400027B RID: 635
		private static object s_lockObj = new object();
	}
}
