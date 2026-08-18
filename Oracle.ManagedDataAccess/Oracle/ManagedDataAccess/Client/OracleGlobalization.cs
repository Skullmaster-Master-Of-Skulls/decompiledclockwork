using System;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x0200006B RID: 107
	public sealed class OracleGlobalization : IDisposable, ICloneable
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x00030C88 File Offset: 0x0002EE88
		internal OracleGlobalization(OracleGlobalizationImpl oracleGlobImpl)
		{
			this.m_oracleGlobalizationImpl = oracleGlobImpl;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00030C98 File Offset: 0x0002EE98
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x00030CB8 File Offset: 0x0002EEB8
		public string Calendar
		{
			get
			{
				if (this.m_oracleGlobalizationImpl.m_calendar == null)
				{
					return string.Empty;
				}
				return this.m_oracleGlobalizationImpl.m_calendar;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.m_oracleGlobalizationImpl.m_calendar = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00030CE0 File Offset: 0x0002EEE0
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x00030D00 File Offset: 0x0002EF00
		public string Comparison
		{
			get
			{
				if (this.m_oracleGlobalizationImpl.m_comparison == null)
				{
					return string.Empty;
				}
				return this.m_oracleGlobalizationImpl.m_comparison;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.m_oracleGlobalizationImpl.m_comparison = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00030D28 File Offset: 0x0002EF28
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x00030D48 File Offset: 0x0002EF48
		public string Currency
		{
			get
			{
				if (this.m_oracleGlobalizationImpl.m_currency == null)
				{
					return string.Empty;
				}
				return this.m_oracleGlobalizationImpl.m_currency;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.m_oracleGlobalizationImpl.m_currency = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00030D70 File Offset: 0x0002EF70
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x00030D90 File Offset: 0x0002EF90
		public string DateFormat
		{
			get
			{
				if (this.m_oracleGlobalizationImpl.m_dateFormat == null)
				{
					return string.Empty;
				}
				return this.m_oracleGlobalizationImpl.m_dateFormat;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.m_oracleGlobalizationImpl.m_dateFormat = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00030DB8 File Offset: 0x0002EFB8
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x00030DD8 File Offset: 0x0002EFD8
		public string DateLanguage
		{
			get
			{
				if (this.m_oracleGlobalizationImpl.m_dateLanguage == null)
				{
					return string.Empty;
				}
				return this.m_oracleGlobalizationImpl.m_dateLanguage;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.m_oracleGlobalizationImpl.m_dateLanguage = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00030E00 File Offset: 0x0002F000
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x00030E20 File Offset: 0x0002F020
		public string DualCurrency
		{
			get
			{
				if (this.m_oracleGlobalizationImpl.m_dualCurrency == null)
				{
					return string.Empty;
				}
				return this.m_oracleGlobalizationImpl.m_dualCurrency;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.m_oracleGlobalizationImpl.m_dualCurrency = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00030E48 File Offset: 0x0002F048
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x00030E68 File Offset: 0x0002F068
		public string ISOCurrency
		{
			get
			{
				if (this.m_oracleGlobalizationImpl.m_isoCurrency == null)
				{
					return string.Empty;
				}
				return this.m_oracleGlobalizationImpl.m_isoCurrency;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.m_oracleGlobalizationImpl.m_isoCurrency = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00030E90 File Offset: 0x0002F090
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x00030EB0 File Offset: 0x0002F0B0
		public string Language
		{
			get
			{
				if (this.m_oracleGlobalizationImpl.m_language == null)
				{
					return string.Empty;
				}
				return this.m_oracleGlobalizationImpl.m_language;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.m_oracleGlobalizationImpl.m_language = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00030ED8 File Offset: 0x0002F0D8
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x00030EF8 File Offset: 0x0002F0F8
		public string LengthSemantics
		{
			get
			{
				if (this.m_oracleGlobalizationImpl.m_lengthSemantics == null)
				{
					return string.Empty;
				}
				return this.m_oracleGlobalizationImpl.m_lengthSemantics;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.m_oracleGlobalizationImpl.m_lengthSemantics = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00030F20 File Offset: 0x0002F120
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x00030F30 File Offset: 0x0002F130
		public bool NCharConversionException
		{
			get
			{
				return this.m_oracleGlobalizationImpl.m_nCharConvException;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.m_oracleGlobalizationImpl.m_nCharConvException = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00030F58 File Offset: 0x0002F158
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x00030F78 File Offset: 0x0002F178
		public string NumericCharacters
		{
			get
			{
				if (this.m_oracleGlobalizationImpl.m_numericCharacters == null)
				{
					return string.Empty;
				}
				return this.m_oracleGlobalizationImpl.m_numericCharacters;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.m_oracleGlobalizationImpl.m_numericCharacters = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00030FA0 File Offset: 0x0002F1A0
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x00030FC0 File Offset: 0x0002F1C0
		public string Sort
		{
			get
			{
				if (this.m_oracleGlobalizationImpl.m_sort == null)
				{
					return string.Empty;
				}
				return this.m_oracleGlobalizationImpl.m_sort;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.m_oracleGlobalizationImpl.m_sort = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00030FE8 File Offset: 0x0002F1E8
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x00031008 File Offset: 0x0002F208
		public string Territory
		{
			get
			{
				if (this.m_oracleGlobalizationImpl.m_territory == null)
				{
					return string.Empty;
				}
				return this.m_oracleGlobalizationImpl.m_territory;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.m_oracleGlobalizationImpl.m_territory = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00031030 File Offset: 0x0002F230
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x00031050 File Offset: 0x0002F250
		public string TimeStampFormat
		{
			get
			{
				if (this.m_oracleGlobalizationImpl.m_timeStampFormat == null)
				{
					return string.Empty;
				}
				return this.m_oracleGlobalizationImpl.m_timeStampFormat;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.m_oracleGlobalizationImpl.m_timeStampFormat = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00031078 File Offset: 0x0002F278
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x00031098 File Offset: 0x0002F298
		public string TimeStampTZFormat
		{
			get
			{
				if (this.m_oracleGlobalizationImpl.m_timeStampTZFormat == null)
				{
					return string.Empty;
				}
				return this.m_oracleGlobalizationImpl.m_timeStampTZFormat;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.m_oracleGlobalizationImpl.m_timeStampTZFormat = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x000310C0 File Offset: 0x0002F2C0
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x000310E0 File Offset: 0x0002F2E0
		public string TimeZone
		{
			get
			{
				if (this.m_oracleGlobalizationImpl.m_timeZone == null)
				{
					return string.Empty;
				}
				return this.m_oracleGlobalizationImpl.m_timeZone;
			}
			set
			{
				if (!this.m_disposed)
				{
					this.m_oracleGlobalizationImpl.m_timeZone = value;
					return;
				}
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00031108 File Offset: 0x0002F308
		public object Clone()
		{
			OracleGlobalizationImpl oracleGlobImpl = null;
			try
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
				}
				if (this.m_oracleGlobalizationImpl != null)
				{
					oracleGlobImpl = (OracleGlobalizationImpl)this.m_oracleGlobalizationImpl.Clone();
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return new OracleGlobalization(oracleGlobImpl);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00031198 File Offset: 0x0002F398
		public void Dispose()
		{
			try
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
				}
				if (!this.m_disposed)
				{
					this.m_disposed = true;
					if (this.m_oracleGlobalizationImpl != null)
					{
						this.m_oracleGlobalizationImpl.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x04000652 RID: 1618
		private bool m_disposed;

		// Token: 0x04000653 RID: 1619
		internal OracleGlobalizationImpl m_oracleGlobalizationImpl;
	}
}
