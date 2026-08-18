using System;
using System.Data.Common;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x0200024E RID: 590
	[Serializable]
	public sealed class OleDbError
	{
		// Token: 0x06002595 RID: 9621 RVA: 0x001005E0 File Offset: 0x000FF9E0
		internal OleDbError(UnsafeNativeMethods.IErrorRecords errorRecords, int index)
		{
			int num = CultureInfo.CurrentCulture.LCID;
			Bid.Trace("<oledb.IErrorRecords.GetErrorInfo|API|OS>\n");
			UnsafeNativeMethods.IErrorInfo errorInfo = errorRecords.GetErrorInfo(index, num);
			OleDbHResult oleDbHResult;
			if (errorInfo != null)
			{
				Bid.Trace("<oledb.IErrorInfo.GetDescription|API|OS>\n");
				oleDbHResult = errorInfo.GetDescription(out this.message);
				Bid.Trace("<oledb.IErrorInfo.GetDescription|API|OS|RET> Message='%ls'\n", this.message);
				if (OleDbHResult.DB_E_NOLOCALE == oleDbHResult)
				{
					Bid.Trace("<oledb.ReleaseComObject|API|OS> ErrorInfo\n");
					Marshal.ReleaseComObject(errorInfo);
					Bid.Trace("<oledb.Kernel32.GetUserDefaultLCID|API|OS>\n");
					num = SafeNativeMethods.GetUserDefaultLCID();
					Bid.Trace("<oledb.IErrorRecords.GetErrorInfo|API|OS> LCID=%d\n", num);
					errorInfo = errorRecords.GetErrorInfo(index, num);
					if (errorInfo != null)
					{
						Bid.Trace("<oledb.IErrorInfo.GetDescription|API|OS>\n");
						oleDbHResult = errorInfo.GetDescription(out this.message);
						Bid.Trace("<oledb.IErrorInfo.GetDescription|API|OS|RET> Message='%ls'\n", this.message);
					}
				}
				if (oleDbHResult < OleDbHResult.S_OK && ADP.IsEmpty(this.message))
				{
					this.message = ODB.FailedGetDescription(oleDbHResult);
				}
				if (errorInfo != null)
				{
					Bid.Trace("<oledb.IErrorInfo.GetSource|API|OS>\n");
					oleDbHResult = errorInfo.GetSource(out this.source);
					Bid.Trace("<oledb.IErrorInfo.GetSource|API|OS|RET> Source='%ls'\n", this.source);
					if (OleDbHResult.DB_E_NOLOCALE == oleDbHResult)
					{
						Marshal.ReleaseComObject(errorInfo);
						Bid.Trace("<oledb.Kernel32.GetUserDefaultLCID|API|OS>\n");
						num = SafeNativeMethods.GetUserDefaultLCID();
						Bid.Trace("<oledb.IErrorRecords.GetErrorInfo|API|OS> LCID=%d\n", num);
						errorInfo = errorRecords.GetErrorInfo(index, num);
						if (errorInfo != null)
						{
							Bid.Trace("<oledb.IErrorInfo.GetSource|API|OS>\n");
							oleDbHResult = errorInfo.GetSource(out this.source);
							Bid.Trace("<oledb.IErrorInfo.GetSource|API|OS|RET> Source='%ls'\n", this.source);
						}
					}
					if (oleDbHResult < OleDbHResult.S_OK && ADP.IsEmpty(this.source))
					{
						this.source = ODB.FailedGetSource(oleDbHResult);
					}
					Bid.Trace("<oledb.Marshal.ReleaseComObject|API|OS> ErrorInfo\n");
					Marshal.ReleaseComObject(errorInfo);
				}
			}
			Bid.Trace("<oledb.IErrorRecords.GetCustomErrorObject|API|OS> IID_ISQLErrorInfo\n");
			UnsafeNativeMethods.ISQLErrorInfo isqlerrorInfo;
			oleDbHResult = errorRecords.GetCustomErrorObject(index, ref ODB.IID_ISQLErrorInfo, out isqlerrorInfo);
			if (isqlerrorInfo != null)
			{
				Bid.Trace("<oledb.ISQLErrorInfo.GetSQLInfo|API|OS>\n");
				this.nativeError = isqlerrorInfo.GetSQLInfo(out this.sqlState);
				Bid.Trace("<oledb.ReleaseComObject|API|OS> SQLErrorInfo\n");
				Marshal.ReleaseComObject(isqlerrorInfo);
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x001007C4 File Offset: 0x000FFBC4
		public string Message
		{
			get
			{
				string text = this.message;
				if (text == null)
				{
					return ADP.StrEmpty;
				}
				return text;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06002597 RID: 9623 RVA: 0x001007E4 File Offset: 0x000FFBE4
		public int NativeError
		{
			get
			{
				return this.nativeError;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06002598 RID: 9624 RVA: 0x001007F8 File Offset: 0x000FFBF8
		public string Source
		{
			get
			{
				string text = this.source;
				if (text == null)
				{
					return ADP.StrEmpty;
				}
				return text;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06002599 RID: 9625 RVA: 0x00100818 File Offset: 0x000FFC18
		public string SQLState
		{
			get
			{
				string text = this.sqlState;
				if (text == null)
				{
					return ADP.StrEmpty;
				}
				return text;
			}
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x00100838 File Offset: 0x000FFC38
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x04001606 RID: 5638
		private readonly string message;

		// Token: 0x04001607 RID: 5639
		private readonly string source;

		// Token: 0x04001608 RID: 5640
		private readonly string sqlState;

		// Token: 0x04001609 RID: 5641
		private readonly int nativeError;
	}
}
