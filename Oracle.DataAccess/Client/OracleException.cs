using System;
using System.Data.Common;
using System.Runtime.Serialization;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	public sealed class OracleException : DbException
	{
		// Token: 0x060000DA RID: 218 RVA: 0x0000F694 File Offset: 0x0000E694
		static OracleException()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000F6C5 File Offset: 0x0000E6C5
		public OracleErrorCollection Errors
		{
			get
			{
				return this.m_errors;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000DC RID: 220 RVA: 0x0000F6CD File Offset: 0x0000E6CD
		public string DataSource
		{
			get
			{
				return this.m_errors[0].DataSource;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000F6E0 File Offset: 0x0000E6E0
		public override string Message
		{
			get
			{
				string text = string.Empty;
				if (this.m_errors != null)
				{
					for (int i = 0; i < this.m_errors.Count; i++)
					{
						text = text + this.m_errors[i].Message + "\n";
					}
					return text.TrimEnd(new char[]
					{
						'\n'
					});
				}
				return text;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000F743 File Offset: 0x0000E743
		public string Procedure
		{
			get
			{
				return this.m_errors[0].Procedure;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000F756 File Offset: 0x0000E756
		public override string Source
		{
			get
			{
				return this.m_errors[0].Source;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000F769 File Offset: 0x0000E769
		public int Number
		{
			get
			{
				return this.m_errors[0].Number;
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000F77C File Offset: 0x0000E77C
		internal OracleException(int errCode) : this(errCode, string.Empty, string.Empty, string.Empty)
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000F794 File Offset: 0x0000E794
		internal OracleException(int errCode, string dataSrc, string procedure, string errMsg)
		{
			this.m_errors = new OracleErrorCollection();
			this.m_errors.Add(new OracleError(errCode, dataSrc, procedure, errMsg));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000F7BD File Offset: 0x0000E7BD
		internal unsafe OracleException(IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, IntPtr opsConCtx, string dataSrc, string procedure, bool bCheck, Exception innerException) : base(null, innerException)
		{
			this.m_errors = this.GetOpoErrCtx(opsErrCtx, pOpoSqlValCtx, opsConCtx, dataSrc, procedure);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000F7DB File Offset: 0x0000E7DB
		internal unsafe static void HandleError(int errCode, OracleConnection conn, string procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, object src, bool bCheck, Exception innerException)
		{
			OracleException.HandleErrorHelper(errCode, conn, opsErrCtx, pOpoSqlValCtx, src, procedure, bCheck, innerException);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000F7F0 File Offset: 0x0000E7F0
		internal unsafe static void HandleErrorHelper(int errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, object src, string procedure, bool bCheck, Exception innerException)
		{
			bool flag = pOpoSqlValCtx != null && (pOpoSqlValCtx->mode & 128U) == 128U;
			string dataSrc;
			if (conn != null)
			{
				dataSrc = conn.DataSource;
			}
			else
			{
				dataSrc = string.Empty;
			}
			IntPtr intPtr;
			if (conn == null || conn.m_opoConCtx == null)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				intPtr = conn.m_opoConCtx.opsConCtx;
			}
			if (errCode == OracleException.OCIError || errCode == OracleException.OCINoData || (flag && errCode == OracleException.OCIWarning))
			{
				OracleException ex = new OracleException(opsErrCtx, pOpoSqlValCtx, intPtr, dataSrc, procedure, bCheck, innerException);
				if (bCheck && conn != null && conn.m_opoConCtx != null && conn.m_opoConCtx.pOpoConValCtx != null && conn.m_opoConCtx.m_bSelfTuning)
				{
					foreach (object obj in ex.Errors)
					{
						OracleError oracleError = (OracleError)obj;
						if (oracleError.Message.IndexOf("ORA-01000") != -1)
						{
							int num = (int)(0.9f * (float)conn.m_opoConCtx.pOpoConValCtx->StmtCacheSize);
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.Trace(64U, new string[]
								{
									string.Concat(new object[]
									{
										" (TUNING) OracleException::HandleErrorHelper(): Statement Cache Size change suggested from ",
										conn.m_opoConCtx.pOpoConValCtx->StmtCacheSize,
										" to ",
										num,
										" due to Exception ",
										oracleError.Message,
										"\n"
									})
								});
							}
							OraTrace.SetMaxStatementCacheSize(num);
						}
					}
				}
				throw ex;
			}
			if (errCode == OracleException.OCIWarning)
			{
				if (intPtr == IntPtr.Zero)
				{
					return;
				}
				OracleException ex2 = new OracleException(opsErrCtx, pOpoSqlValCtx, intPtr, dataSrc, procedure);
				OracleException.IssueWarning(conn, src, ex2.Errors);
				return;
			}
			else if (errCode <= OracleException.InternalError)
			{
				if (src == null)
				{
					throw new OracleException(errCode, dataSrc, procedure, string.Concat(new object[]
					{
						OpoErrResManager.GetErrorMesg(ErrRes.INT_ERR, new string[0]),
						"(",
						errCode,
						")"
					}));
				}
				throw new OracleException(errCode, dataSrc, procedure, string.Concat(new object[]
				{
					OpoErrResManager.GetErrorMesg(ErrRes.INT_ERR, new string[0]),
					"(",
					errCode,
					") [",
					src.GetType().FullName,
					"]"
				}));
			}
			else
			{
				if (errCode >= OracleException.CoreError)
				{
					string oraMesg = OracleException.GetOraMesg(errCode, new string[0]);
					throw new OracleException(errCode, dataSrc, procedure, oraMesg);
				}
				throw new OracleException(errCode, dataSrc, procedure, string.Empty);
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000FAE0 File Offset: 0x0000EAE0
		internal OracleException(OracleErrorCollection oec)
		{
			this.m_errors = oec;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000FAEF File Offset: 0x0000EAEF
		private OracleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.m_errors = (OracleErrorCollection)info.GetValue(base.GetType().FullName, typeof(OracleErrorCollection));
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000FB1F File Offset: 0x0000EB1F
		internal unsafe OracleException(IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, IntPtr opsConCtx, string dataSrc, string procedure)
		{
			this.m_errors = this.GetOpoErrCtx(opsErrCtx, pOpoSqlValCtx, opsConCtx, dataSrc, procedure);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000FB3C File Offset: 0x0000EB3C
		private unsafe OracleErrorCollection GetOpoErrCtx(IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, IntPtr opsConCtx, string dataSrc, string procedure)
		{
			if (opsErrCtx == IntPtr.Zero)
			{
				return null;
			}
			OracleErrorCollection oracleErrorCollection = new OracleErrorCollection();
			OpoErrCtx opoErr = this.GetOpoErr(opsErrCtx, 0, dataSrc, procedure);
			oracleErrorCollection.Add(new OracleError(opoErr, procedure, dataSrc));
			if (pOpoSqlValCtx != null && opsConCtx != IntPtr.Zero && pOpoSqlValCtx->ErrCnt != 0 && (pOpoSqlValCtx->mode & 128U) == 128U)
			{
				IntPtr[] array = new IntPtr[pOpoSqlValCtx->ErrCnt];
				int[] array2 = new int[pOpoSqlValCtx->ErrCnt];
				try
				{
					int batchErrCtx = OpsErr.GetBatchErrCtx(opsErrCtx, opsConCtx, pOpoSqlValCtx->ErrCnt, array, array2);
					if (batchErrCtx != 0)
					{
						throw new OracleException(ErrRes.INT_ERR_BATCHERRGET_FAIL, dataSrc, procedure, string.Empty);
					}
					for (int i = 0; i < pOpoSqlValCtx->ErrCnt; i++)
					{
						if (!(array[i] == IntPtr.Zero))
						{
							OpoErrCtx opoErr2 = this.GetOpoErr(array[i], array2[i], dataSrc, procedure);
							oracleErrorCollection.Add(new OracleError(opoErr2, procedure, dataSrc));
						}
					}
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
					if (array != null)
					{
						for (int j = 0; j < array.Length; j++)
						{
							if (array[j] != IntPtr.Zero)
							{
								try
								{
									OpsErr.FreeCtx(ref array[j]);
								}
								catch (Exception ex2)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex2);
									}
									array[j] = IntPtr.Zero;
								}
							}
						}
					}
					array = null;
				}
			}
			return oracleErrorCollection;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000FCFC File Offset: 0x0000ECFC
		private OpoErrCtx GetOpoErr(IntPtr opsErrCtx, int arrayBindIndex, string dataSrc, string procedure)
		{
			int num = 0;
			OpoErrCtx opoErrCtx = new OpoErrCtx();
			try
			{
				num = OpsErr.GetOpoCtx(opsErrCtx, ref opoErrCtx);
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
				throw new OracleException(ErrRes.INT_OCIERRORGET_FAIL, dataSrc, procedure, string.Empty);
			}
			opoErrCtx.m_arrayBindIndex = arrayBindIndex;
			return opoErrCtx;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000FD5C File Offset: 0x0000ED5C
		public static void IssueWarning(OracleConnection conn, object src, OracleErrorCollection c)
		{
			conn.OnInfoMessage(src, new OracleInfoMessageEventArgs(c));
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000FD6B File Offset: 0x0000ED6B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue(base.GetType().FullName, this.m_errors);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000FD8C File Offset: 0x0000ED8C
		public override string ToString()
		{
			string result;
			if (base.InnerException != null)
			{
				result = string.Concat(new string[]
				{
					base.GetType().FullName,
					" ",
					this.Message,
					" ",
					base.InnerException.Message,
					this.StackTrace
				});
			}
			else
			{
				result = string.Concat(new string[]
				{
					base.GetType().FullName,
					" ",
					this.Message,
					" ",
					this.StackTrace
				});
			}
			return result;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000FE2E File Offset: 0x0000EE2E
		internal static void HandleError(int errCode, OracleConnection conn, IntPtr opsErrCtx, object src)
		{
			OracleException.HandleErrorHelper(errCode, conn, opsErrCtx, null, src, string.Empty, false);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000FE41 File Offset: 0x0000EE41
		internal static void HandleError(int errCode, OracleConnection conn, IntPtr opsErrCtx, object src, bool bCheck)
		{
			OracleException.HandleErrorHelper(errCode, conn, opsErrCtx, null, src, string.Empty, bCheck);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000FE55 File Offset: 0x0000EE55
		internal unsafe static void HandleError(int errCode, OracleConnection conn, string procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, object src)
		{
			OracleException.HandleErrorHelper(errCode, conn, opsErrCtx, pOpoSqlValCtx, src, procedure, false);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000FE65 File Offset: 0x0000EE65
		internal unsafe static void HandleError(int errCode, OracleConnection conn, string procedure, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, object src, bool bCheck)
		{
			OracleException.HandleErrorHelper(errCode, conn, opsErrCtx, pOpoSqlValCtx, src, procedure, bCheck);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000FE78 File Offset: 0x0000EE78
		internal unsafe static void HandleErrorHelper(int errCode, OracleConnection conn, IntPtr opsErrCtx, OpoSqlValCtx* pOpoSqlValCtx, object src, string procedure, bool bCheck)
		{
			bool flag = pOpoSqlValCtx != null && (pOpoSqlValCtx->mode & 128U) == 128U;
			string dataSrc;
			if (conn != null)
			{
				dataSrc = conn.DataSource;
			}
			else
			{
				dataSrc = string.Empty;
			}
			IntPtr intPtr;
			if (conn == null || conn.m_opoConCtx == null)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				intPtr = conn.m_opoConCtx.opsConCtx;
			}
			if (errCode == OracleException.OCIError || errCode == OracleException.OCINoData || (flag && errCode == OracleException.OCIWarning))
			{
				OracleException ex = new OracleException(opsErrCtx, pOpoSqlValCtx, intPtr, dataSrc, procedure);
				if (bCheck && conn != null && conn.m_opoConCtx != null && conn.m_opoConCtx.pOpoConValCtx != null && conn.m_opoConCtx.m_bSelfTuning)
				{
					foreach (object obj in ex.Errors)
					{
						OracleError oracleError = (OracleError)obj;
						if (oracleError.Message.IndexOf("ORA-01000") != -1)
						{
							int num = (int)(0.9f * (float)conn.m_opoConCtx.pOpoConValCtx->StmtCacheSize);
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.Trace(64U, new string[]
								{
									string.Concat(new object[]
									{
										" (TUNING) OracleException::HandleErrorHelper(): Statement Cache Size change suggested from ",
										conn.m_opoConCtx.pOpoConValCtx->StmtCacheSize,
										" to ",
										num,
										" due to Exception ",
										oracleError.Message,
										"\n"
									})
								});
							}
							OraTrace.SetMaxStatementCacheSize(num);
						}
					}
				}
				throw ex;
			}
			if (errCode == OracleException.OCIWarning)
			{
				if (intPtr == IntPtr.Zero)
				{
					return;
				}
				OracleException ex2 = new OracleException(opsErrCtx, pOpoSqlValCtx, intPtr, dataSrc, procedure);
				OracleException.IssueWarning(conn, src, ex2.Errors);
				return;
			}
			else if (errCode <= OracleException.InternalError)
			{
				if (src == null)
				{
					throw new OracleException(errCode, dataSrc, procedure, string.Concat(new object[]
					{
						OpoErrResManager.GetErrorMesg(ErrRes.INT_ERR, new string[0]),
						"(",
						errCode,
						")"
					}));
				}
				throw new OracleException(errCode, dataSrc, procedure, string.Concat(new object[]
				{
					OpoErrResManager.GetErrorMesg(ErrRes.INT_ERR, new string[0]),
					"(",
					errCode,
					") [",
					src.GetType().FullName,
					"]"
				}));
			}
			else
			{
				if (errCode >= OracleException.CoreError)
				{
					string oraMesg = OracleException.GetOraMesg(errCode, new string[0]);
					throw new OracleException(errCode, dataSrc, procedure, oraMesg);
				}
				throw new OracleException(errCode, dataSrc, procedure, string.Empty);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00010164 File Offset: 0x0000F164
		internal static string GetOraMesg(int errNum, params string[] args)
		{
			string errMsg = "";
			int num = 0;
			try
			{
				num = OpsErr.GetOraMesg(errNum, out errMsg);
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
				errMsg = OpoErrResManager.GetErrorMesg(ErrRes.INT_ERR_CORE_MESG_GET, new string[0]);
			}
			return OracleException.AddOraMesgPrefix(errNum, errMsg);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000101C0 File Offset: 0x0000F1C0
		internal static string AddOraMesgPrefix(int errNum, string errMsg)
		{
			return string.Concat(new object[]
			{
				"ORA-",
				errNum,
				": ",
				errMsg
			});
		}

		// Token: 0x040000AC RID: 172
		private OracleErrorCollection m_errors;

		// Token: 0x040000AD RID: 173
		internal static int OCINoData = 100;

		// Token: 0x040000AE RID: 174
		internal static int CoreError = 2;

		// Token: 0x040000AF RID: 175
		private static int OCIWarning = 1;

		// Token: 0x040000B0 RID: 176
		private static int OCIError = -1;

		// Token: 0x040000B1 RID: 177
		private static int InternalError = -3000;
	}
}
