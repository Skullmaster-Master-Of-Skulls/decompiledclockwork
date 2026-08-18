using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Transactions;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.ConnectionPool;
using OracleInternal.MTS;
using OracleInternal.ServiceObjects;

namespace OracleInternal.Common
{
	// Token: 0x020000C5 RID: 197
	internal static class Trace
	{
		// Token: 0x0600078A RID: 1930 RVA: 0x00045BB0 File Offset: 0x00043DB0
		static Trace()
		{
			Trace.Init();
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00045C14 File Offset: 0x00043E14
		private static void Init()
		{
			if (ConfigBaseClass.m_TraceLevel > 0)
			{
				ConfigBaseClass.m_TraceLevel |= 268435456;
				ProviderConfig.m_bTraceLevelPublic = ((ConfigBaseClass.m_TraceLevel & 1) != 0);
				ProviderConfig.m_bTraceLevelPrivate = ((ConfigBaseClass.m_TraceLevel & 2) != 0);
				ProviderConfig.m_bTraceLevelNetwork = ((ConfigBaseClass.m_TraceLevel & 4) != 0);
				ProviderConfig.m_bTraceLevelConfig = ((ConfigBaseClass.m_TraceLevel & 268435456) != 0);
				Trace.CreateEventLogSource();
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00045C8C File Offset: 0x00043E8C
		internal static void ReInit(bool doFinalize = true)
		{
			try
			{
				if (doFinalize)
				{
					if (Trace.s_singleTextListener != null)
					{
						lock (Trace.s_syncObj)
						{
							if (Trace.s_singleTextListener != null)
							{
								Trace.FinalizeSingleTraceListener();
							}
						}
					}
					ThreadData.RegenerateTimeStampHash();
				}
				Trace.Init();
			}
			catch
			{
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00045CF8 File Offset: 0x00043EF8
		private static void FinalizeMultiTraceListener()
		{
			try
			{
				ThreadData threadData = (ThreadData)Thread.GetData(Thread.GetNamedDataSlot(Trace.s_namedSlot));
				if (threadData != null)
				{
					threadData.Dispose();
				}
			}
			catch
			{
			}
			finally
			{
				Thread.SetData(Thread.GetNamedDataSlot(Trace.s_namedSlot), null);
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00045D58 File Offset: 0x00043F58
		private static ThreadData InitializeMultiTraceListener(bool reInit = false)
		{
			if (reInit)
			{
				Trace.FinalizeMultiTraceListener();
			}
			ThreadData threadData = null;
			Stream fileName = Trace.GetFileName(ConfigBaseClass.m_traceFileLocation, Thread.CurrentThread.ManagedThreadId);
			if (fileName != null)
			{
				TextWriterTraceListener textListener = new TextWriterTraceListener(fileName);
				threadData = new ThreadData
				{
					traceFile = fileName,
					textListener = textListener
				};
				Thread.SetData(Thread.GetNamedDataSlot(Trace.s_namedSlot), threadData);
			}
			return threadData;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00045DB8 File Offset: 0x00043FB8
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		private static void FinalizeSingleTraceListener()
		{
			try
			{
				if (Trace.s_singleTextListener != null)
				{
					Trace.Listeners.Remove(Trace.s_singleTextListener);
					Trace.s_singleTextListener.Dispose();
				}
			}
			catch
			{
			}
			finally
			{
				Trace.s_singleTextListener = null;
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00045E10 File Offset: 0x00044010
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		private static void InitializeSingleTraceListener()
		{
			Stream fileName = Trace.GetFileName(ConfigBaseClass.m_traceFileLocation, -1);
			if (fileName != null)
			{
				Trace.s_singleTextListener = new TextWriterTraceListener(fileName);
				Trace.Listeners.Add(Trace.s_singleTextListener);
			}
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00045E48 File Offset: 0x00044048
		internal static void Write(OracleTraceLevel traceLevel, OracleTraceTag traceTag, params string[] args)
		{
			try
			{
				bool flag = false;
				if (ConfigBaseClass.m_TraceLevel > 0 && (traceLevel & (OracleTraceLevel)ConfigBaseClass.m_TraceLevel) == traceLevel)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(Trace.GetTimeInfo());
					string str = string.Empty;
					string str2 = string.Empty;
					string text = string.Empty;
					switch (traceLevel)
					{
					case OracleTraceLevel.Public:
						str = "(PUB)";
						break;
					case OracleTraceLevel.Private:
						str = "(PRI)";
						break;
					case (OracleTraceLevel)3:
						break;
					case OracleTraceLevel.Network:
						str = "(NET)";
						break;
					default:
						if (traceLevel == OracleTraceLevel.Config)
						{
							str = "(CFG)";
						}
						break;
					}
					if (OracleTraceTag.Error == (traceTag & OracleTraceTag.Error))
					{
						traceTag &= (OracleTraceTag)(-268435457);
						text = " (ERR)";
						flag = true;
					}
					else if (OracleTraceTag.Entry == (traceTag & OracleTraceTag.Entry))
					{
						traceTag &= (OracleTraceTag)(-257);
						text = " (ENT)";
						flag = true;
					}
					else if (OracleTraceTag.Exit == (traceTag & OracleTraceTag.Exit))
					{
						traceTag &= (OracleTraceTag)(-513);
						text = " (EXT)";
						flag = true;
					}
					if (OracleTraceTag.MTS == (traceTag & OracleTraceTag.MTS))
					{
						traceTag &= (OracleTraceTag)(-4097);
						text += " (MTS)";
						flag = true;
					}
					if (OracleTraceTag.CP == (traceTag & OracleTraceTag.CP))
					{
						traceTag &= (OracleTraceTag)(-2049);
						text += " (CP)";
						flag = true;
					}
					OracleTraceTag oracleTraceTag = traceTag;
					if (oracleTraceTag <= OracleTraceTag.TTC)
					{
						if (oracleTraceTag <= OracleTraceTag.SQL)
						{
							if (oracleTraceTag <= OracleTraceTag.Sqlnet)
							{
								switch (oracleTraceTag)
								{
								case OracleTraceTag.Environment:
									str2 = " (ENV)";
									break;
								case OracleTraceTag.Version:
									str2 = " (VER)";
									break;
								case (OracleTraceTag)3:
									break;
								case OracleTraceTag.Config:
									str2 = " (.NET)";
									break;
								default:
									if (oracleTraceTag == OracleTraceTag.Sqlnet)
									{
										str2 = " (SQLNET)";
									}
									break;
								}
							}
							else if (oracleTraceTag != OracleTraceTag.Tnsnames)
							{
								if (oracleTraceTag == OracleTraceTag.SQL)
								{
									str2 = " (SQL)";
								}
							}
							else
							{
								str2 = " (TNSNAMES)";
							}
						}
						else if (oracleTraceTag <= OracleTraceTag.REFCursor)
						{
							if (oracleTraceTag != OracleTraceTag.EDM)
							{
								if (oracleTraceTag == OracleTraceTag.REFCursor)
								{
									str2 = " (REF)";
								}
							}
							else
							{
								str2 = " (EDM)";
							}
						}
						else if (oracleTraceTag != OracleTraceTag.SelfTuning)
						{
							if (oracleTraceTag == OracleTraceTag.TTC)
							{
								str2 = " (TTC)";
							}
						}
						else
						{
							str2 = " (TUN)";
						}
					}
					else if (oracleTraceTag <= OracleTraceTag.ONS)
					{
						if (oracleTraceTag <= OracleTraceTag.RLB)
						{
							if (oracleTraceTag != OracleTraceTag.SvcObj)
							{
								if (oracleTraceTag == OracleTraceTag.RLB)
								{
									str2 = " (RLB)";
								}
							}
							else
							{
								str2 = " (SVC)";
							}
						}
						else if (oracleTraceTag != OracleTraceTag.HA)
						{
							if (oracleTraceTag == OracleTraceTag.ONS)
							{
								str2 = " (ONS)";
							}
						}
						else
						{
							str2 = " (HA)";
						}
					}
					else if (oracleTraceTag <= OracleTraceTag.BinXML)
					{
						if (oracleTraceTag != OracleTraceTag.BUF)
						{
							if (oracleTraceTag == OracleTraceTag.BinXML)
							{
								str2 = " (BINXML)";
							}
						}
						else
						{
							str2 = " (BUF)";
						}
					}
					else if (oracleTraceTag != OracleTraceTag.Send)
					{
						if (oracleTraceTag != OracleTraceTag.Receive)
						{
							if (oracleTraceTag == OracleTraceTag.Prm)
							{
								str2 = " (PRM)";
							}
						}
						else
						{
							str2 = " (REC)";
						}
					}
					else
					{
						str2 = " (SND)";
					}
					if (traceLevel == OracleTraceLevel.Config)
					{
						stringBuilder.AppendFormat("{0,-17}", str + str2 + text);
					}
					else
					{
						stringBuilder.AppendFormat("{0,-10} ", str + str2 + text);
					}
					if (flag)
					{
						StackTrace stackTrace = new StackTrace();
						int index = 1;
						string name = stackTrace.GetFrame(index).GetMethod().Name;
						if (name == "HandleError")
						{
							index = 2;
							name = stackTrace.GetFrame(index).GetMethod().Name;
						}
						string name2 = stackTrace.GetFrame(index).GetMethod().ReflectedType.Name;
						stringBuilder.Append(name2);
						if (name == ".ctor")
						{
							stringBuilder.Append(name);
						}
						else
						{
							stringBuilder.Append(".");
							stringBuilder.Append(name);
						}
						stringBuilder.Append("()");
					}
					if (args.Length > 0)
					{
						if (flag)
						{
							stringBuilder.Append(" ");
						}
						string[] array = new string[args.Length];
						Array.Copy(args, 1, array, 0, args.Length - 1);
						if (array != null)
						{
							stringBuilder.AppendFormat(args[0], array);
						}
					}
					stringBuilder.Append("\r\n");
					Trace.WriteTrace(stringBuilder.ToString());
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000462A8 File Offset: 0x000444A8
		internal static void Write(OracleTraceLevel traceLevel, OracleTraceTag tracetag, byte[] dataBuffer, int offset, int size)
		{
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				string asciiCharacters = Trace.GetAsciiCharacters(dataBuffer, offset, size);
				int num = 0;
				int num2 = 8;
				DateTime now = DateTime.Now;
				while (size > 0)
				{
					int length = Math.Min(size, num2);
					string text = BitConverter.ToString(dataBuffer, offset + num2 * num, length);
					string arg = asciiCharacters.Substring(num2 * num, length);
					num++;
					size -= num2;
					stringBuilder.AppendFormat(Trace.GetTimeInfo(), new object[0]);
					if (tracetag != OracleTraceTag.Send)
					{
						if (tracetag == OracleTraceTag.Receive)
						{
							stringBuilder.AppendFormat("{0,-12}", "(NET) (REC)");
						}
					}
					else
					{
						stringBuilder.AppendFormat("{0,-12}", "(NET) (SND)");
					}
					stringBuilder.AppendFormat("{0,-26}|{1,-8}|", text.Replace("-", " "), arg);
					stringBuilder.Append("\r\n");
				}
				Trace.WriteTrace(stringBuilder.ToString());
			}
			catch
			{
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000463A8 File Offset: 0x000445A8
		private static string GetTimeInfo()
		{
			return string.Format("{0} TID:{1,-3} ", DateTime.Now.ToString(Trace.s_dateTimeFormat), Thread.CurrentThread.ManagedThreadId);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x000463E0 File Offset: 0x000445E0
		private static void WriteTrace(string message)
		{
			if (ConfigBaseClass.m_TraceOption == 0)
			{
				lock (Trace.s_syncObj)
				{
					if (Trace.s_singleTextListener == null)
					{
						Trace.InitializeSingleTraceListener();
					}
					Trace.s_singleTextListener.Write(message);
					Trace.s_singleTextListener.Flush();
					return;
				}
			}
			ThreadData threadData = (ThreadData)Thread.GetData(Thread.GetNamedDataSlot(Trace.s_namedSlot));
			if (threadData == null)
			{
				threadData = Trace.InitializeMultiTraceListener(false);
			}
			if (threadData != null)
			{
				if (threadData.IsOutdated)
				{
					threadData = Trace.InitializeMultiTraceListener(true);
				}
				threadData.textListener.Write(message);
				threadData.textListener.Flush();
			}
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0004648C File Offset: 0x0004468C
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		[EventLogPermission(SecurityAction.Assert, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		private static Stream OpenFile(string singleTraceFileLocation, string traceFileName, int threadId)
		{
			Stream result = null;
			try
			{
				string text = null;
				singleTraceFileLocation = (ConfigBaseClass.m_singleTraceFileLocation = ConfigBaseClass.GetResolvedFileLocation(singleTraceFileLocation));
				if (!Directory.Exists(singleTraceFileLocation))
				{
					text = ConfigBaseClass.m_singleTraceFileLocation;
					try
					{
						singleTraceFileLocation = Path.GetDirectoryName(singleTraceFileLocation);
					}
					catch
					{
					}
				}
				string text2 = singleTraceFileLocation + Path.DirectorySeparatorChar + traceFileName;
				bool flag = false;
				try
				{
					result = File.Open(text2, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
					flag = true;
				}
				catch
				{
					flag = false;
					result = null;
				}
				try
				{
					if (text != null)
					{
						EventLog.WriteEntry(Trace.s_eventLogSource, Trace.s_eventLogSource + " has detected that " + text + " is not a valid/write-able directory.");
					}
					if (flag)
					{
						if (Trace.m_bEventLogEntryAdded)
						{
							goto IL_175;
						}
						lock (Trace.s_syncObj)
						{
							if (!Trace.m_bEventLogEntryAdded)
							{
								if (ConfigBaseClass.m_TraceOption == 0)
								{
									EventLog.WriteEntry(Trace.s_eventLogSource, Trace.s_eventLogSource + " has opened trace file " + text2 + ".");
								}
								else
								{
									EventLog.WriteEntry(Trace.s_eventLogSource, string.Concat(new object[]
									{
										Trace.s_eventLogSource,
										" has opened trace file ",
										singleTraceFileLocation,
										Path.DirectorySeparatorChar,
										ConfigBaseClass.CurrentProcess.ProcessName.ToUpperInvariant(),
										".EXE_PID_",
										ConfigBaseClass.CurrentProcess.Id,
										"_<TID>_<DATE>_<TIME>.trc."
									}));
								}
								Trace.m_bEventLogEntryAdded = true;
							}
							goto IL_175;
						}
					}
					EventLog.WriteEntry(Trace.s_eventLogSource, Trace.s_eventLogSource + " could not open trace file(s) at " + singleTraceFileLocation + ".");
					IL_175:;
				}
				catch
				{
				}
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00046698 File Offset: 0x00044898
		[EventLogPermission(SecurityAction.Assert, Unrestricted = true)]
		private static void CreateEventLogSource()
		{
			try
			{
				if (!EventLog.SourceExists(Trace.s_eventLogSource))
				{
					EventLog.CreateEventSource(Trace.s_eventLogSource, Trace.s_eventLogName);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x000466D8 File Offset: 0x000448D8
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		[EventLogPermission(SecurityAction.Assert, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		private static Stream GetFileName(string singleTraceFileLocation, int threadId)
		{
			DateTime now = DateTime.Now;
			string traceFileName = null;
			string empty = string.Empty;
			Stream stream = null;
			string text = "";
			if (threadId != -1)
			{
				text = "_TID_" + threadId.ToString();
			}
			traceFileName = string.Concat(new object[]
			{
				ConfigBaseClass.CurrentProcess.ProcessName.ToUpperInvariant(),
				".EXE_PID_",
				ConfigBaseClass.CurrentProcess.Id,
				text,
				"_DATE_",
				now.ToString(Trace.s_dateFormat),
				"_TIME_",
				now.ToString(Trace.s_timeFormat),
				".trc"
			});
			if (singleTraceFileLocation != null && singleTraceFileLocation != string.Empty)
			{
				stream = Trace.OpenFile(singleTraceFileLocation, traceFileName, threadId);
			}
			if (stream == null)
			{
				try
				{
					DirectoryInfo directoryInfo = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), "ODP.NET\\managed\\trace"));
					singleTraceFileLocation = directoryInfo.FullName;
				}
				catch
				{
				}
				stream = Trace.OpenFile(singleTraceFileLocation, traceFileName, threadId);
			}
			return stream;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x000467EC File Offset: 0x000449EC
		private static string GetAsciiCharacters(byte[] dataBuffer, int offset, int size)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = offset; i < offset + size; i++)
			{
				byte b = dataBuffer[i];
				if (b > 31 && b < 127)
				{
					stringBuilder.Append((char)b);
				}
				else
				{
					stringBuilder.Append(".");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00046838 File Offset: 0x00044A38
		public static string GetCPInfo(OracleConnectionImpl con, object transaction, string affinityInstance, string oper, bool bResourcePoolInfo = false, bool bDistributionInfo = false)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			StringBuilder stringBuilder = new StringBuilder();
			string text4 = null;
			TransactionContext<OraclePoolManager, OraclePool, OracleConnectionImpl> transactionContext = null;
			try
			{
				if (con != null && con.m_localTxnId != null && con.m_localTxnId != string.Empty)
				{
					text2 = con.m_localTxnId;
				}
				else if (transaction != null)
				{
					if (transaction is Transaction)
					{
						text2 = ((Transaction)transaction).TransactionInformation.LocalIdentifier;
					}
					else if (transaction is string)
					{
						text2 = (string)transaction;
					}
				}
				else if (Transaction.Current != null)
				{
					text2 = Transaction.Current.TransactionInformation.LocalIdentifier;
				}
				if (con == null)
				{
					stringBuilder.Append("(txnid=");
					if (text2 != null && text2 != string.Empty)
					{
						stringBuilder.Append(text2);
					}
					else
					{
						stringBuilder.Append("n/a");
					}
					stringBuilder.Append(") ");
					if (text2 != null)
					{
						stringBuilder.Append(OracleConnection.Dump(text2));
					}
					return stringBuilder.ToString();
				}
				OraclePoolManager pm = con.m_pm;
				if (con.m_txnCtx != null)
				{
					try
					{
						transactionContext = con.m_txnCtx;
					}
					catch
					{
					}
					try
					{
						text = con.m_txnCtx.m_localTxnId;
					}
					catch
					{
					}
					try
					{
						text3 = con.m_txnCtx.m_affinityInstanceName;
					}
					catch
					{
					}
					try
					{
						if (con.m_txnCtx.m_mtsTxnRM != null)
						{
							text4 = con.m_txnCtx.m_mtsTxnRM.m_RMGuid.ToString();
						}
					}
					catch
					{
					}
				}
				if (affinityInstance == null || affinityInstance == string.Empty)
				{
					affinityInstance = text3;
				}
				if (oper != null && oper != string.Empty)
				{
					stringBuilder.Append("(oper=");
					stringBuilder.Append(oper);
					stringBuilder.Append(") ");
				}
				stringBuilder.Append("(aff=");
				stringBuilder.Append((affinityInstance == null || affinityInstance == string.Empty) ? "n/a" : affinityInstance);
				stringBuilder.Append(") (inst=");
				stringBuilder.Append(con.m_instanceName);
				stringBuilder.Append(") (affmatch=");
				stringBuilder.Append((affinityInstance == null || affinityInstance == string.Empty) ? "n/a" : (affinityInstance == con.m_instanceName).ToString().Substring(0, 1));
				stringBuilder.Append(") (pr.service=");
				stringBuilder.Append(con.ServiceName);
				stringBuilder.Append(") (pr.pdb=");
				stringBuilder.Append(con.PdbName);
				stringBuilder.Append(") (pr.edition=");
				stringBuilder.Append(con.EditionName);
				stringBuilder.Append(") (sessid=");
				stringBuilder.Append(con.m_endUserSessionId);
				stringBuilder.Append(":");
				stringBuilder.Append(con.m_endUserSerialNum);
				stringBuilder.Append(") ");
				if (con.m_pxyUserSessionId != -1)
				{
					stringBuilder.Append(") (psessid=");
					stringBuilder.Append(con.m_pxyUserSessionId);
					stringBuilder.Append(":");
					stringBuilder.Append(con.m_pxyUserSerialNum);
					stringBuilder.Append(") ");
				}
				if (con != null)
				{
					stringBuilder.Append(string.Format("({0};{1};{2};{3};{4}) ", new object[]
					{
						con.m_bCheckedOutByApp.ToString().Substring(0, 1),
						con.m_bCheckedOutByDTC.ToString().Substring(0, 1),
						con.m_bPutCompleted.ToString().Substring(0, 1),
						con.m_instanceName,
						(con.m_mtsTxnCtx != null) ? con.m_mtsTxnCtx.m_txnType.ToString().Substring(0, 1) : "N"
					}));
				}
				if (pm != null)
				{
					stringBuilder.Append("(pmid=");
					stringBuilder.Append(pm.m_id);
					stringBuilder.Append(") ");
				}
				if ((Transaction.Current != null && con.m_cs != null && con.m_cs.m_enlist == Enlist.True) || text2 != null)
				{
					MTSTxnCtx mtsTxnCtx = con.m_mtsTxnCtx;
					MTSTxnBranch mtstxnBranch = null;
					if (mtsTxnCtx != null)
					{
						try
						{
							mtstxnBranch = con.m_mtsTxnCtx.m_mtsTxnBranch;
						}
						catch
						{
						}
						stringBuilder.Append("(txntype=");
						stringBuilder.Append(mtsTxnCtx.m_txnType.ToString().Substring(0, 1));
						stringBuilder.Append(") ");
						if (mtstxnBranch != null)
						{
							stringBuilder.Append("(br=");
							stringBuilder.Append(mtstxnBranch.BranchNumber);
							stringBuilder.Append(") ");
						}
						else
						{
							stringBuilder.Append("(br=n/a) ");
						}
					}
					if (text4 != null)
					{
						stringBuilder.Append("(rmid=");
						stringBuilder.Append(text4);
						stringBuilder.Append(") ");
					}
					stringBuilder.Append("(txnid=");
					if (text2 != null && text2 != string.Empty)
					{
						stringBuilder.Append(text2);
					}
					else
					{
						stringBuilder.Append("n/a");
					}
					stringBuilder.Append(") ");
					if (mtsTxnCtx != null && text != null && text != string.Empty)
					{
						if (text2 != mtsTxnCtx.m_txnLocalID || text2 != text)
						{
							stringBuilder.Append("(txnidmatch=F) ");
							if (text2 != mtsTxnCtx.m_txnLocalID)
							{
								stringBuilder.Append("(mtstxnid=");
								stringBuilder.Append(mtsTxnCtx.m_txnLocalID);
								stringBuilder.Append(") ");
							}
							else if (text2 != text)
							{
								stringBuilder.Append("(mtstxnid=");
								stringBuilder.Append(mtsTxnCtx.m_txnLocalID);
								stringBuilder.Append(") ");
							}
						}
						else
						{
							stringBuilder.Append("(txnidmatch=T) ");
						}
					}
				}
				if (bResourcePoolInfo && transactionContext != null)
				{
					lock (transactionContext)
					{
						for (int i = 0; i <= transactionContext.m_maxBranchIndex; i++)
						{
							OracleConnectionImpl oracleConnectionImpl = transactionContext.m_enlistedPRList[i];
							if (oracleConnectionImpl != null)
							{
								if (oracleConnectionImpl.m_sessionType != SessionType.Two_Session_Proxy)
								{
									stringBuilder.Append(string.Format("[{0}:({1}:{2});{3};{4};{5};{6}]", new object[]
									{
										i,
										oracleConnectionImpl.m_endUserSessionId,
										oracleConnectionImpl.m_endUserSerialNum,
										oracleConnectionImpl.m_bCheckedOutByApp.ToString().Substring(0, 1),
										oracleConnectionImpl.m_bCheckedOutByDTC.ToString().Substring(0, 1),
										oracleConnectionImpl.m_bPutCompleted.ToString().Substring(0, 1),
										oracleConnectionImpl.m_instanceName
									}));
								}
								else
								{
									stringBuilder.Append(string.Format("[{0}:({1}:{2})({3}:{4});{5};{6};{7};{8}]", new object[]
									{
										i,
										oracleConnectionImpl.m_endUserSessionId,
										oracleConnectionImpl.m_endUserSerialNum,
										oracleConnectionImpl.m_pxyUserSessionId,
										oracleConnectionImpl.m_pxyUserSerialNum,
										oracleConnectionImpl.m_bCheckedOutByApp.ToString().Substring(0, 1),
										oracleConnectionImpl.m_bCheckedOutByDTC.ToString().Substring(0, 1),
										oracleConnectionImpl.m_bPutCompleted.ToString().Substring(0, 1),
										oracleConnectionImpl.m_instanceName
									}));
								}
							}
							else
							{
								stringBuilder.Append(string.Format("[{0}:null]", i));
							}
						}
					}
					stringBuilder.Append(" ");
				}
				if (bDistributionInfo && pm != null)
				{
					RLB rlb = null;
					if (con != null && !string.IsNullOrEmpty(con.ServiceName))
					{
						string id = (con.m_databaseName + "|" + con.ServiceName).ToLowerInvariant();
						rlb = RLBManager.Get(id);
					}
					if (rlb == null)
					{
						List<OraclePool> list = pm.m_pmListCP.GetList();
						stringBuilder.Append("(DISP) ");
						using (List<OraclePool>.Enumerator enumerator = list.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								OraclePool oraclePool = enumerator.Current;
								int count = oraclePool.m_cpQueuePR.Count;
								int count2 = oraclePool.m_cpListPR.Count;
								stringBuilder.Append("(service=");
								stringBuilder.Append(oraclePool.m_serviceName);
								stringBuilder.Append(";inst=");
								stringBuilder.Append(oraclePool.m_instanceName);
								if (oraclePool != null)
								{
									stringBuilder.Append(";used=");
									stringBuilder.Append(count2 - count);
									stringBuilder.Append(";idle=");
									stringBuilder.Append(count);
									stringBuilder.Append(";tot=");
									stringBuilder.Append(count2);
									stringBuilder.Append(") ");
								}
							}
							goto IL_B1E;
						}
					}
					if (rlb != null)
					{
						List<OraclePool> list2 = pm.m_pmListCP.GetList();
						int num = 0;
						stringBuilder.Append("(DISP) (RLB) ");
						if (rlb != null)
						{
							for (int j = 0; j < rlb.m_dispenseCounter.Length; j++)
							{
								if (con.m_instanceName.ToLowerInvariant() == rlb.m_instances[j].ToLowerInvariant())
								{
									Interlocked.Increment(ref rlb.m_dispenseCounter[j]);
								}
								num += rlb.m_dispenseCounter[j];
							}
							for (int k = 0; k < rlb.m_instances.Length; k++)
							{
								OraclePool oraclePool2 = null;
								int num2 = 0;
								int num3 = 0;
								for (int l = 0; l < list2.Count; l++)
								{
									if (list2[l].m_instanceName.ToLowerInvariant() == rlb.m_instances[k].ToLowerInvariant())
									{
										oraclePool2 = list2[l];
										num2 = oraclePool2.m_cpQueuePR.Count;
										num3 = oraclePool2.m_cpListPR.Count;
										break;
									}
								}
								stringBuilder.Append("(service=");
								stringBuilder.Append(rlb.m_service);
								stringBuilder.Append(";inst=");
								stringBuilder.Append(rlb.m_instances[k]);
								if (oraclePool2 != null)
								{
									stringBuilder.Append("; used=");
									stringBuilder.Append(num3 - num2);
									stringBuilder.Append("; idle=");
									stringBuilder.Append(num2);
									stringBuilder.Append("; tot=");
									stringBuilder.Append(num3);
								}
								else
								{
									stringBuilder.Append("; used=0; idle=0; tot=0");
								}
								int num4 = rlb.m_dispenseCounter[k];
								stringBuilder.Append("; rlb pct=");
								stringBuilder.Append(rlb.m_rlbPercentages[k].ToString());
								stringBuilder.Append("%");
								stringBuilder.Append("; act pct=");
								stringBuilder.Append(num4);
								stringBuilder.Append("/");
								stringBuilder.Append(num);
								stringBuilder.Append(" (");
								if (num != 0)
								{
									stringBuilder.Append((int)((double)num4 / (double)num * 100.0));
									stringBuilder.Append("%)) ");
								}
								else
								{
									stringBuilder.Append("0%)) ");
								}
							}
							stringBuilder.Append("(miss=");
							stringBuilder.Append(pm.m_rlbMissCount);
							stringBuilder.Append(") ");
						}
					}
				}
				IL_B1E:
				if (text2 != null)
				{
					stringBuilder.Append(OracleConnection.Dump(text2));
				}
			}
			catch
			{
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00047448 File Offset: 0x00045648
		internal static string GetMTSTraceOutput(OraclePoolManager pm, OracleConnectionImpl conImpl, string txnid, string operation, string txnctxid = null, string respool = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (operation != null)
			{
				stringBuilder.Append("(oper=");
				stringBuilder.Append(operation);
				stringBuilder.Append(") ");
			}
			if (pm != null)
			{
				stringBuilder.Append("(pmid=");
				stringBuilder.Append(pm.m_id);
				stringBuilder.Append(") ");
			}
			else if (conImpl != null && conImpl.m_pm != null)
			{
				stringBuilder.Append("(pmid=");
				stringBuilder.Append(conImpl.m_pm.m_id);
				stringBuilder.Append(") ");
			}
			if (conImpl != null)
			{
				stringBuilder.Append("(sessid=");
				stringBuilder.Append(conImpl.m_endUserSessionId.ToString());
				stringBuilder.Append(") ");
				stringBuilder.Append("(implId=");
				stringBuilder.Append(conImpl.m_id);
				stringBuilder.Append(") ");
				stringBuilder.Append("(app=");
				stringBuilder.Append(conImpl.m_bCheckedOutByApp.ToString());
				stringBuilder.Append(") ");
				stringBuilder.Append("(dtc=");
				stringBuilder.Append(conImpl.m_bCheckedOutByDTC.ToString());
				stringBuilder.Append(") ");
				stringBuilder.Append("(put=");
				stringBuilder.Append(conImpl.m_bPutCompleted.ToString());
				stringBuilder.Append(") ");
			}
			if (conImpl != null && conImpl.m_cs != null)
			{
				stringBuilder.Append("(pooling=");
				stringBuilder.Append(conImpl.m_cs.m_pooling);
				stringBuilder.Append(") ");
			}
			if (txnid != null)
			{
				stringBuilder.Append("(MTS) (txnid=");
				stringBuilder.Append(txnid);
				stringBuilder.Append(") ");
			}
			else if (conImpl != null && conImpl.m_txnCtx != null)
			{
				stringBuilder.Append("(MTS) (txnid=");
				stringBuilder.Append(conImpl.m_txnCtx.m_localTxnId);
				stringBuilder.Append(") ");
			}
			if (txnctxid != null)
			{
				stringBuilder.Append("(txnctxid=");
				stringBuilder.Append(txnctxid);
				stringBuilder.Append(") ");
			}
			else if (conImpl != null && conImpl.m_txnCtx != null)
			{
				stringBuilder.Append("(txnctxid=");
				stringBuilder.Append(conImpl.m_txnCtx.m_id);
				stringBuilder.Append(") ");
			}
			if (conImpl != null && conImpl.m_mtsTxnCtx != null)
			{
				stringBuilder.Append("(txntype=");
				stringBuilder.Append(conImpl.m_mtsTxnCtx.m_txnType);
				stringBuilder.Append(") ");
			}
			if (conImpl != null && conImpl.m_txnCtx != null && conImpl.m_txnCtx.m_mtsTxnRM != null)
			{
				stringBuilder.Append("(rmid=");
				stringBuilder.Append(conImpl.m_txnCtx.m_mtsTxnRM.m_RMGuid.ToString());
				stringBuilder.Append(") ");
			}
			if (conImpl != null && conImpl.m_mtsTxnCtx != null && conImpl.m_mtsTxnCtx.m_mtsTxnBranch != null)
			{
				stringBuilder.Append("(br=");
				stringBuilder.Append(conImpl.m_mtsTxnCtx.m_mtsTxnBranch.BranchNumber);
				stringBuilder.Append(") ");
			}
			if (respool != null)
			{
				stringBuilder.Append("(respool=");
				stringBuilder.Append(respool);
				stringBuilder.Append(") ");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000A48 RID: 2632
		private static object s_syncObj = new object();

		// Token: 0x04000A49 RID: 2633
		internal static TextWriterTraceListener s_singleTextListener = null;

		// Token: 0x04000A4A RID: 2634
		internal static string s_namedSlot = "OracleInternalThreadData";

		// Token: 0x04000A4B RID: 2635
		internal static string s_dateTimeFormat = "yyyy-MM-dd HH:mm:ss.ffffff";

		// Token: 0x04000A4C RID: 2636
		internal static string s_dateFormat = "yyyy_MM_dd";

		// Token: 0x04000A4D RID: 2637
		internal static string s_timeFormat = "HH_mm_ss";

		// Token: 0x04000A4E RID: 2638
		internal static string s_eventLogSource = "Oracle Data Provider for .NET, Managed Driver";

		// Token: 0x04000A4F RID: 2639
		internal static string s_eventLogName = "Application";

		// Token: 0x04000A50 RID: 2640
		internal static bool m_bEventLogEntryAdded = false;
	}
}
