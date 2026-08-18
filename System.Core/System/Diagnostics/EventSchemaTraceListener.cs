using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics
{
	// Token: 0x0200029B RID: 667
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventSchemaTraceListener : TextWriterTraceListener
	{
		// Token: 0x06001837 RID: 6199 RVA: 0x00057447 File Offset: 0x00055647
		public EventSchemaTraceListener(string fileName) : this(fileName, string.Empty)
		{
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00057455 File Offset: 0x00055655
		public EventSchemaTraceListener(string fileName, string name) : this(fileName, name, 32768)
		{
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x00057464 File Offset: 0x00055664
		public EventSchemaTraceListener(string fileName, string name, int bufferSize) : this(fileName, name, bufferSize, TraceLogRetentionOption.SingleFileUnboundedSize)
		{
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00057470 File Offset: 0x00055670
		public EventSchemaTraceListener(string fileName, string name, int bufferSize, TraceLogRetentionOption logRetentionOption) : this(fileName, name, bufferSize, logRetentionOption, 10240000L)
		{
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00057483 File Offset: 0x00055683
		public EventSchemaTraceListener(string fileName, string name, int bufferSize, TraceLogRetentionOption logRetentionOption, long maximumFileSize) : this(fileName, name, bufferSize, logRetentionOption, maximumFileSize, 2)
		{
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00057494 File Offset: 0x00055694
		public EventSchemaTraceListener(string fileName, string name, int bufferSize, TraceLogRetentionOption logRetentionOption, long maximumFileSize, int maximumNumberOfFiles)
		{
			if (bufferSize < 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (logRetentionOption < TraceLogRetentionOption.UnlimitedSequentialFiles || logRetentionOption > TraceLogRetentionOption.SingleFileBoundedSize)
			{
				throw new ArgumentOutOfRangeException("logRetentionOption", SR.GetString("ArgumentOutOfRange_NeedValidLogRetention"));
			}
			base.Name = name;
			this.fileName = fileName;
			if (!string.IsNullOrEmpty(this.fileName) && fileName[0] != Path.DirectorySeparatorChar && fileName[0] != Path.AltDirectorySeparatorChar && !Path.IsPathRooted(fileName))
			{
				this.fileName = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile), this.fileName);
			}
			this._retention = logRetentionOption;
			this._bufferSize = bufferSize;
			this._SetMaxFileSize(maximumFileSize, false);
			this._SetMaxNumberOfFiles(maximumNumberOfFiles, false);
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x00057594 File Offset: 0x00055794
		// (set) Token: 0x0600183E RID: 6206 RVA: 0x000575A3 File Offset: 0x000557A3
		public new TextWriter Writer
		{
			[SecurityCritical]
			get
			{
				this.EnsureWriter();
				return this.traceWriter;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("NotSupported_SetTextWriter"));
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x000575B4 File Offset: 0x000557B4
		public override bool IsThreadSafe
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x000575B7 File Offset: 0x000557B7
		public int BufferSize
		{
			get
			{
				this.Init();
				return this._bufferSize;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x000575C5 File Offset: 0x000557C5
		public TraceLogRetentionOption TraceLogRetentionOption
		{
			get
			{
				this.Init();
				return this._retention;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x000575D3 File Offset: 0x000557D3
		public long MaximumFileSize
		{
			get
			{
				this.Init();
				return this._maxFileSize;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x000575E1 File Offset: 0x000557E1
		public int MaximumNumberOfFiles
		{
			get
			{
				this.Init();
				return this._maxNumberOfFiles;
			}
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x000575F0 File Offset: 0x000557F0
		public override void Close()
		{
			try
			{
				if (this.traceWriter != null)
				{
					this.traceWriter.Flush();
					this.traceWriter.Close();
				}
			}
			finally
			{
				this.traceWriter = null;
				base.Close();
			}
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0005763C File Offset: 0x0005583C
		[SecurityCritical]
		public override void Flush()
		{
			if (!this.EnsureWriter())
			{
				return;
			}
			this.traceWriter.Flush();
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00057652 File Offset: 0x00055852
		public override void Write(string message)
		{
			this.WriteLine(message);
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0005765B File Offset: 0x0005585B
		public override void WriteLine(string message)
		{
			this.TraceEvent(null, SR.GetString("TraceAsTraceSource"), TraceEventType.Information, 0, message);
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x00057674 File Offset: 0x00055874
		public override void Fail(string message, string detailMessage)
		{
			StringBuilder stringBuilder = new StringBuilder(message);
			if (detailMessage != null)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(detailMessage);
			}
			this.TraceEvent(null, SR.GetString("TraceAsTraceSource"), TraceEventType.Error, 0, stringBuilder.ToString());
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x000576B8 File Offset: 0x000558B8
		[SecurityCritical]
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null))
			{
				return;
			}
			StringBuilder writer = new StringBuilder(512);
			EventSchemaTraceListener.BuildHeader(writer, source, eventType, id, eventCache, null, false, base.TraceOutputOptions);
			string message;
			if (args != null)
			{
				message = string.Format(CultureInfo.InvariantCulture, format, args);
			}
			else
			{
				message = format;
			}
			EventSchemaTraceListener.BuildMessage(writer, message);
			EventSchemaTraceListener.BuildFooter(writer, eventType, eventCache, false, base.TraceOutputOptions);
			this._InternalWriteRaw(writer);
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0005773C File Offset: 0x0005593C
		[SecurityCritical]
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
			{
				return;
			}
			StringBuilder writer = new StringBuilder(512);
			EventSchemaTraceListener.BuildHeader(writer, source, eventType, id, eventCache, null, false, base.TraceOutputOptions);
			EventSchemaTraceListener.BuildMessage(writer, message);
			EventSchemaTraceListener.BuildFooter(writer, eventType, eventCache, false, base.TraceOutputOptions);
			this._InternalWriteRaw(writer);
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x000577A8 File Offset: 0x000559A8
		[SecurityCritical]
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
			{
				return;
			}
			StringBuilder writer = new StringBuilder(512);
			EventSchemaTraceListener.BuildHeader(writer, source, eventType, id, eventCache, null, true, base.TraceOutputOptions);
			if (data != null)
			{
				EventSchemaTraceListener._InternalBuildRaw(writer, "<System.Diagnostics.UserData xmlns=\"http://schemas.microsoft.com/win/2006/09/System.Diagnostics/UserData/\">");
				EventSchemaTraceListener.BuildUserData(writer, data);
				EventSchemaTraceListener._InternalBuildRaw(writer, "</System.Diagnostics.UserData>");
			}
			EventSchemaTraceListener.BuildFooter(writer, eventType, eventCache, true, base.TraceOutputOptions);
			this._InternalWriteRaw(writer);
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0005782C File Offset: 0x00055A2C
		[SecurityCritical]
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
			{
				return;
			}
			StringBuilder writer = new StringBuilder(512);
			EventSchemaTraceListener.BuildHeader(writer, source, eventType, id, eventCache, null, true, base.TraceOutputOptions);
			if (data != null && data.Length != 0)
			{
				EventSchemaTraceListener._InternalBuildRaw(writer, "<System.Diagnostics.UserData xmlns=\"http://schemas.microsoft.com/win/2006/09/System.Diagnostics/UserData/\">");
				for (int i = 0; i < data.Length; i++)
				{
					if (data[i] != null)
					{
						EventSchemaTraceListener.BuildUserData(writer, data[i]);
					}
				}
				EventSchemaTraceListener._InternalBuildRaw(writer, "</System.Diagnostics.UserData>");
			}
			EventSchemaTraceListener.BuildFooter(writer, eventType, eventCache, true, base.TraceOutputOptions);
			this._InternalWriteRaw(writer);
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x000578CC File Offset: 0x00055ACC
		[SecurityCritical]
		public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
		{
			StringBuilder writer = new StringBuilder(512);
			EventSchemaTraceListener.BuildHeader(writer, source, TraceEventType.Transfer, id, eventCache, relatedActivityId.ToString("B"), false, base.TraceOutputOptions);
			EventSchemaTraceListener.BuildMessage(writer, message);
			EventSchemaTraceListener.BuildFooter(writer, TraceEventType.Transfer, eventCache, false, base.TraceOutputOptions);
			this._InternalWriteRaw(writer);
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00057927 File Offset: 0x00055B27
		private static void BuildMessage(StringBuilder writer, string message)
		{
			EventSchemaTraceListener._InternalBuildRaw(writer, "<Data>");
			EventSchemaTraceListener.BuildEscaped(writer, message);
			EventSchemaTraceListener._InternalBuildRaw(writer, "</Data>");
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00057948 File Offset: 0x00055B48
		[SecurityCritical]
		private static void BuildHeader(StringBuilder writer, string source, TraceEventType eventType, int id, TraceEventCache eventCache, string relatedActivityId, bool isUserData, TraceOptions opts)
		{
			EventSchemaTraceListener._InternalBuildRaw(writer, "<Event xmlns=\"http://schemas.microsoft.com/win/2004/08/events/event\"><System><Provider Guid=\"");
			EventSchemaTraceListener._InternalBuildRaw(writer, "{00000000-0000-0000-0000-000000000000}");
			EventSchemaTraceListener._InternalBuildRaw(writer, "\"/><EventID>");
			EventSchemaTraceListener._InternalBuildRaw(writer, ((uint)((id < 0) ? 0 : id)).ToString(CultureInfo.InvariantCulture));
			EventSchemaTraceListener._InternalBuildRaw(writer, "</EventID>");
			EventSchemaTraceListener._InternalBuildRaw(writer, "<Level>");
			int num = (int)eventType;
			int num2 = num;
			if (num > 255 || num < 0)
			{
				num = 8;
			}
			EventSchemaTraceListener._InternalBuildRaw(writer, num.ToString(CultureInfo.InvariantCulture));
			EventSchemaTraceListener._InternalBuildRaw(writer, "</Level>");
			if (num2 > 255)
			{
				num2 /= 256;
				EventSchemaTraceListener._InternalBuildRaw(writer, "<Opcode>");
				EventSchemaTraceListener._InternalBuildRaw(writer, num2.ToString(CultureInfo.InvariantCulture));
				EventSchemaTraceListener._InternalBuildRaw(writer, "</Opcode>");
			}
			if ((TraceOptions.DateTime & opts) != TraceOptions.None)
			{
				EventSchemaTraceListener._InternalBuildRaw(writer, "<TimeCreated SystemTime=\"");
				if (eventCache != null)
				{
					EventSchemaTraceListener._InternalBuildRaw(writer, eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture));
				}
				else
				{
					EventSchemaTraceListener._InternalBuildRaw(writer, DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture));
				}
				EventSchemaTraceListener._InternalBuildRaw(writer, "\"/>");
			}
			EventSchemaTraceListener._InternalBuildRaw(writer, "<Correlation ActivityID=\"");
			EventSchemaTraceListener._InternalBuildRaw(writer, Trace.CorrelationManager.ActivityId.ToString("B"));
			if (relatedActivityId != null)
			{
				EventSchemaTraceListener._InternalBuildRaw(writer, "\" RelatedActivityID=\"");
				EventSchemaTraceListener._InternalBuildRaw(writer, relatedActivityId);
			}
			EventSchemaTraceListener._InternalBuildRaw(writer, "\"/>");
			if (eventCache != null && ((TraceOptions.ProcessId | TraceOptions.ThreadId) & opts) != TraceOptions.None)
			{
				EventSchemaTraceListener._InternalBuildRaw(writer, "<Execution ");
				EventSchemaTraceListener._InternalBuildRaw(writer, "ProcessID=\"");
				EventSchemaTraceListener._InternalBuildRaw(writer, ((uint)eventCache.ProcessId).ToString(CultureInfo.InvariantCulture));
				EventSchemaTraceListener._InternalBuildRaw(writer, "\" ");
				EventSchemaTraceListener._InternalBuildRaw(writer, "ThreadID=\"");
				EventSchemaTraceListener._InternalBuildRaw(writer, eventCache.ThreadId);
				EventSchemaTraceListener._InternalBuildRaw(writer, "\"");
				EventSchemaTraceListener._InternalBuildRaw(writer, "/>");
			}
			EventSchemaTraceListener._InternalBuildRaw(writer, "<Computer>");
			EventSchemaTraceListener._InternalBuildRaw(writer, EventSchemaTraceListener.machineName);
			EventSchemaTraceListener._InternalBuildRaw(writer, "</Computer>");
			EventSchemaTraceListener._InternalBuildRaw(writer, "</System>");
			if (!isUserData)
			{
				EventSchemaTraceListener._InternalBuildRaw(writer, "<EventData>");
				return;
			}
			EventSchemaTraceListener._InternalBuildRaw(writer, "<UserData>");
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x00057B74 File Offset: 0x00055D74
		private static void BuildFooter(StringBuilder writer, TraceEventType eventType, TraceEventCache eventCache, bool isUserData, TraceOptions opts)
		{
			if (!isUserData)
			{
				EventSchemaTraceListener._InternalBuildRaw(writer, "</EventData>");
			}
			else
			{
				EventSchemaTraceListener._InternalBuildRaw(writer, "</UserData>");
			}
			EventSchemaTraceListener._InternalBuildRaw(writer, "<RenderingInfo Culture=\"en-EN\">");
			if (eventType <= TraceEventType.Start)
			{
				if (eventType <= TraceEventType.Information)
				{
					switch (eventType)
					{
					case TraceEventType.Critical:
						EventSchemaTraceListener._InternalBuildRaw(writer, "<Level>Critical</Level>");
						break;
					case TraceEventType.Error:
						EventSchemaTraceListener._InternalBuildRaw(writer, "<Level>Error</Level>");
						break;
					case (TraceEventType)3:
						break;
					case TraceEventType.Warning:
						EventSchemaTraceListener._InternalBuildRaw(writer, "<Level>Warning</Level>");
						break;
					default:
						if (eventType == TraceEventType.Information)
						{
							EventSchemaTraceListener._InternalBuildRaw(writer, "<Level>Information</Level>");
						}
						break;
					}
				}
				else if (eventType != TraceEventType.Verbose)
				{
					if (eventType == TraceEventType.Start)
					{
						EventSchemaTraceListener._InternalBuildRaw(writer, "<Level>Information</Level><Opcode>Start</Opcode>");
					}
				}
				else
				{
					EventSchemaTraceListener._InternalBuildRaw(writer, "<Level>Verbose</Level>");
				}
			}
			else if (eventType <= TraceEventType.Suspend)
			{
				if (eventType != TraceEventType.Stop)
				{
					if (eventType == TraceEventType.Suspend)
					{
						EventSchemaTraceListener._InternalBuildRaw(writer, "<Level>Information</Level><Opcode>Suspend</Opcode>");
					}
				}
				else
				{
					EventSchemaTraceListener._InternalBuildRaw(writer, "<Level>Information</Level><Opcode>Stop</Opcode>");
				}
			}
			else if (eventType != TraceEventType.Resume)
			{
				if (eventType == TraceEventType.Transfer)
				{
					EventSchemaTraceListener._InternalBuildRaw(writer, "<Level>Information</Level><Opcode>Transfer</Opcode>");
				}
			}
			else
			{
				EventSchemaTraceListener._InternalBuildRaw(writer, "<Level>Information</Level><Opcode>Resume</Opcode>");
			}
			EventSchemaTraceListener._InternalBuildRaw(writer, "</RenderingInfo>");
			if (eventCache != null && ((TraceOptions.LogicalOperationStack | TraceOptions.Timestamp | TraceOptions.Callstack) & opts) != TraceOptions.None)
			{
				EventSchemaTraceListener._InternalBuildRaw(writer, "<System.Diagnostics.ExtendedData xmlns=\"http://schemas.microsoft.com/2006/09/System.Diagnostics/ExtendedData\">");
				if ((TraceOptions.Timestamp & opts) != TraceOptions.None)
				{
					EventSchemaTraceListener._InternalBuildRaw(writer, "<Timestamp>");
					EventSchemaTraceListener._InternalBuildRaw(writer, eventCache.Timestamp.ToString(CultureInfo.InvariantCulture));
					EventSchemaTraceListener._InternalBuildRaw(writer, "</Timestamp>");
				}
				if ((TraceOptions.LogicalOperationStack & opts) != TraceOptions.None)
				{
					Stack logicalOperationStack = eventCache.LogicalOperationStack;
					EventSchemaTraceListener._InternalBuildRaw(writer, "<LogicalOperationStack>");
					if (logicalOperationStack != null && logicalOperationStack.Count > 0)
					{
						foreach (object obj in logicalOperationStack)
						{
							EventSchemaTraceListener._InternalBuildRaw(writer, "<LogicalOperation>");
							EventSchemaTraceListener.BuildEscaped(writer, obj.ToString());
							EventSchemaTraceListener._InternalBuildRaw(writer, "</LogicalOperation>");
						}
					}
					EventSchemaTraceListener._InternalBuildRaw(writer, "</LogicalOperationStack>");
				}
				if ((TraceOptions.Callstack & opts) != TraceOptions.None)
				{
					EventSchemaTraceListener._InternalBuildRaw(writer, "<Callstack>");
					EventSchemaTraceListener.BuildEscaped(writer, eventCache.Callstack);
					EventSchemaTraceListener._InternalBuildRaw(writer, "</Callstack>");
				}
				EventSchemaTraceListener._InternalBuildRaw(writer, "</System.Diagnostics.ExtendedData>");
			}
			EventSchemaTraceListener._InternalBuildRaw(writer, "</Event>");
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00057DC4 File Offset: 0x00055FC4
		private static void BuildEscaped(StringBuilder writer, string str)
		{
			if (str == null)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];
				if (c <= '"')
				{
					if (c != '\n')
					{
						if (c != '\r')
						{
							if (c == '"')
							{
								EventSchemaTraceListener._InternalBuildRaw(writer, str.Substring(num, i - num));
								EventSchemaTraceListener._InternalBuildRaw(writer, "&quot;");
								num = i + 1;
							}
						}
						else
						{
							EventSchemaTraceListener._InternalBuildRaw(writer, str.Substring(num, i - num));
							EventSchemaTraceListener._InternalBuildRaw(writer, "&#xD;");
							num = i + 1;
						}
					}
					else
					{
						EventSchemaTraceListener._InternalBuildRaw(writer, str.Substring(num, i - num));
						EventSchemaTraceListener._InternalBuildRaw(writer, "&#xA;");
						num = i + 1;
					}
				}
				else if (c <= '\'')
				{
					if (c != '&')
					{
						if (c == '\'')
						{
							EventSchemaTraceListener._InternalBuildRaw(writer, str.Substring(num, i - num));
							EventSchemaTraceListener._InternalBuildRaw(writer, "&apos;");
							num = i + 1;
						}
					}
					else
					{
						EventSchemaTraceListener._InternalBuildRaw(writer, str.Substring(num, i - num));
						EventSchemaTraceListener._InternalBuildRaw(writer, "&amp;");
						num = i + 1;
					}
				}
				else if (c != '<')
				{
					if (c == '>')
					{
						EventSchemaTraceListener._InternalBuildRaw(writer, str.Substring(num, i - num));
						EventSchemaTraceListener._InternalBuildRaw(writer, "&gt;");
						num = i + 1;
					}
				}
				else
				{
					EventSchemaTraceListener._InternalBuildRaw(writer, str.Substring(num, i - num));
					EventSchemaTraceListener._InternalBuildRaw(writer, "&lt;");
					num = i + 1;
				}
			}
			EventSchemaTraceListener._InternalBuildRaw(writer, str.Substring(num, str.Length - num));
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x00057F44 File Offset: 0x00056144
		private static void BuildUserData(StringBuilder writer, object data)
		{
			UnescapedXmlDiagnosticData unescapedXmlDiagnosticData = data as UnescapedXmlDiagnosticData;
			if (unescapedXmlDiagnosticData == null)
			{
				EventSchemaTraceListener.BuildMessage(writer, data.ToString());
				return;
			}
			EventSchemaTraceListener._InternalBuildRaw(writer, unescapedXmlDiagnosticData.ToString());
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00057F74 File Offset: 0x00056174
		private static void _InternalBuildRaw(StringBuilder writer, string message)
		{
			writer.Append(message);
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x00057F7E File Offset: 0x0005617E
		[SecurityCritical]
		private void _InternalWriteRaw(StringBuilder writer)
		{
			if (!this.EnsureWriter())
			{
				return;
			}
			this.traceWriter.Write(writer.ToString());
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x00057F9A File Offset: 0x0005619A
		protected override string[] GetSupportedAttributes()
		{
			return new string[]
			{
				"bufferSize",
				"logRetentionOption",
				"maximumFileSize",
				"maximumNumberOfFiles"
			};
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x00057FC4 File Offset: 0x000561C4
		private void Init()
		{
			if (!this._initialized)
			{
				object lockObject = this.m_lockObject;
				lock (lockObject)
				{
					if (!this._initialized)
					{
						try
						{
							if (base.Attributes.ContainsKey("bufferSize"))
							{
								int num = int.Parse(base.Attributes["bufferSize"], CultureInfo.InvariantCulture);
								if (num > 0)
								{
									this._bufferSize = num;
								}
							}
							if (base.Attributes.ContainsKey("logRetentionOption"))
							{
								string strA = base.Attributes["logRetentionOption"];
								if (string.Compare(strA, "SingleFileUnboundedSize", StringComparison.OrdinalIgnoreCase) == 0)
								{
									this._retention = TraceLogRetentionOption.SingleFileUnboundedSize;
								}
								else if (string.Compare(strA, "LimitedCircularFiles", StringComparison.OrdinalIgnoreCase) == 0)
								{
									this._retention = TraceLogRetentionOption.LimitedCircularFiles;
								}
								else if (string.Compare(strA, "UnlimitedSequentialFiles", StringComparison.OrdinalIgnoreCase) == 0)
								{
									this._retention = TraceLogRetentionOption.UnlimitedSequentialFiles;
								}
								else if (string.Compare(strA, "SingleFileBoundedSize", StringComparison.OrdinalIgnoreCase) == 0)
								{
									this._retention = TraceLogRetentionOption.SingleFileBoundedSize;
								}
								else if (string.Compare(strA, "LimitedSequentialFiles", StringComparison.OrdinalIgnoreCase) == 0)
								{
									this._retention = TraceLogRetentionOption.LimitedSequentialFiles;
								}
								else
								{
									this._retention = TraceLogRetentionOption.SingleFileUnboundedSize;
								}
							}
							if (base.Attributes.ContainsKey("maximumFileSize"))
							{
								long maximumFileSize = long.Parse(base.Attributes["maximumFileSize"], CultureInfo.InvariantCulture);
								this._SetMaxFileSize(maximumFileSize, false);
							}
							if (base.Attributes.ContainsKey("maximumNumberOfFiles"))
							{
								int maximumNumberOfFiles = int.Parse(base.Attributes["maximumNumberOfFiles"], CultureInfo.InvariantCulture);
								this._SetMaxNumberOfFiles(maximumNumberOfFiles, false);
							}
						}
						catch (Exception)
						{
						}
						finally
						{
							this._initialized = true;
						}
					}
				}
			}
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x000581A4 File Offset: 0x000563A4
		private void _SetMaxFileSize(long maximumFileSize, bool throwOnError)
		{
			switch (this._retention)
			{
			case TraceLogRetentionOption.UnlimitedSequentialFiles:
			case TraceLogRetentionOption.LimitedCircularFiles:
			case TraceLogRetentionOption.LimitedSequentialFiles:
			case TraceLogRetentionOption.SingleFileBoundedSize:
				if (maximumFileSize < 0L && throwOnError)
				{
					throw new ArgumentOutOfRangeException("maximumFileSize", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (maximumFileSize >= (long)this._bufferSize)
				{
					this._maxFileSize = maximumFileSize;
					return;
				}
				if (throwOnError)
				{
					throw new ArgumentOutOfRangeException("maximumFileSize", SR.GetString("ArgumentOutOfRange_NeedMaxFileSizeGEBufferSize"));
				}
				this._maxFileSize = (long)this._bufferSize;
				return;
			case TraceLogRetentionOption.SingleFileUnboundedSize:
				this._maxFileSize = -1L;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00058234 File Offset: 0x00056434
		private void _SetMaxNumberOfFiles(int maximumNumberOfFiles, bool throwOnError)
		{
			switch (this._retention)
			{
			case TraceLogRetentionOption.UnlimitedSequentialFiles:
				this._maxNumberOfFiles = -1;
				return;
			case TraceLogRetentionOption.LimitedCircularFiles:
				if (maximumNumberOfFiles >= 2)
				{
					this._maxNumberOfFiles = maximumNumberOfFiles;
					return;
				}
				if (throwOnError)
				{
					throw new ArgumentOutOfRangeException("maximumNumberOfFiles", SR.GetString("ArgumentOutOfRange_NeedValidMaxNumFiles", new object[]
					{
						2
					}));
				}
				this._maxNumberOfFiles = 2;
				return;
			case TraceLogRetentionOption.SingleFileUnboundedSize:
			case TraceLogRetentionOption.SingleFileBoundedSize:
				this._maxNumberOfFiles = 1;
				return;
			case TraceLogRetentionOption.LimitedSequentialFiles:
				if (maximumNumberOfFiles >= 1)
				{
					this._maxNumberOfFiles = maximumNumberOfFiles;
					return;
				}
				if (throwOnError)
				{
					throw new ArgumentOutOfRangeException("maximumNumberOfFiles", SR.GetString("ArgumentOutOfRange_NeedValidMaxNumFiles", new object[]
					{
						1
					}));
				}
				this._maxNumberOfFiles = 1;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x000582E8 File Offset: 0x000564E8
		[SecurityCritical]
		private bool EnsureWriter()
		{
			if (this.traceWriter == null)
			{
				if (string.IsNullOrEmpty(this.fileName))
				{
					return false;
				}
				object lockObject = this.m_lockObject;
				lock (lockObject)
				{
					if (this.traceWriter != null)
					{
						return true;
					}
					string text = this.fileName;
					for (int i = 0; i < 2; i++)
					{
						try
						{
							this.Init();
							this.traceWriter = new EventSchemaTraceListener.TraceWriter(text, this._bufferSize, this._retention, this._maxFileSize, this._maxNumberOfFiles);
							break;
						}
						catch (IOException)
						{
							string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.fileName);
							string extension = Path.GetExtension(this.fileName);
							text = fileNameWithoutExtension + Guid.NewGuid().ToString() + extension;
						}
						catch (UnauthorizedAccessException)
						{
							break;
						}
						catch (Exception)
						{
							break;
						}
					}
					if (this.traceWriter == null)
					{
						this.fileName = null;
					}
				}
			}
			return this.traceWriter != null;
		}

		// Token: 0x04000B95 RID: 2965
		private const string s_optionBufferSize = "bufferSize";

		// Token: 0x04000B96 RID: 2966
		private const string s_optionLogRetention = "logRetentionOption";

		// Token: 0x04000B97 RID: 2967
		private const string s_optionMaximumFileSize = "maximumFileSize";

		// Token: 0x04000B98 RID: 2968
		private const string s_optionMaximumNumberOfFiles = "maximumNumberOfFiles";

		// Token: 0x04000B99 RID: 2969
		private const string s_userDataHeader = "<System.Diagnostics.UserData xmlns=\"http://schemas.microsoft.com/win/2006/09/System.Diagnostics/UserData/\">";

		// Token: 0x04000B9A RID: 2970
		private const string s_eventHeader = "<Event xmlns=\"http://schemas.microsoft.com/win/2004/08/events/event\"><System><Provider Guid=\"";

		// Token: 0x04000B9B RID: 2971
		private const int s_defaultPayloadSize = 512;

		// Token: 0x04000B9C RID: 2972
		private const int _retryThreshold = 2;

		// Token: 0x04000B9D RID: 2973
		private static readonly string machineName = Environment.MachineName;

		// Token: 0x04000B9E RID: 2974
		private EventSchemaTraceListener.TraceWriter traceWriter;

		// Token: 0x04000B9F RID: 2975
		private string fileName;

		// Token: 0x04000BA0 RID: 2976
		private bool _initialized;

		// Token: 0x04000BA1 RID: 2977
		private int _bufferSize = 32768;

		// Token: 0x04000BA2 RID: 2978
		private TraceLogRetentionOption _retention = TraceLogRetentionOption.SingleFileUnboundedSize;

		// Token: 0x04000BA3 RID: 2979
		private long _maxFileSize = 10240000L;

		// Token: 0x04000BA4 RID: 2980
		private int _maxNumberOfFiles = 2;

		// Token: 0x04000BA5 RID: 2981
		private readonly object m_lockObject = new object();

		// Token: 0x02000465 RID: 1125
		private sealed class TraceWriter : TextWriter
		{
			// Token: 0x0600200D RID: 8205 RVA: 0x00070045 File Offset: 0x0006E245
			[SecurityCritical]
			internal TraceWriter(string _fileName, int bufferSize, TraceLogRetentionOption retention, long maxFileSize, int maxNumberOfFiles) : base(CultureInfo.InvariantCulture)
			{
				this.stream = new LogStream(_fileName, bufferSize, (LogRetentionOption)retention, maxFileSize, maxNumberOfFiles);
			}

			// Token: 0x0600200E RID: 8206 RVA: 0x00070070 File Offset: 0x0006E270
			private static Encoding GetEncodingWithFallback(Encoding encoding)
			{
				Encoding encoding2 = (Encoding)encoding.Clone();
				encoding2.EncoderFallback = EncoderFallback.ReplacementFallback;
				encoding2.DecoderFallback = DecoderFallback.ReplacementFallback;
				return encoding2;
			}

			// Token: 0x17000641 RID: 1601
			// (get) Token: 0x0600200F RID: 8207 RVA: 0x000700A0 File Offset: 0x0006E2A0
			public override Encoding Encoding
			{
				get
				{
					if (this.encNoBOMwithFallback == null)
					{
						object lockObject = this.m_lockObject;
						lock (lockObject)
						{
							if (this.encNoBOMwithFallback == null)
							{
								this.encNoBOMwithFallback = EventSchemaTraceListener.TraceWriter.GetEncodingWithFallback(new UTF8Encoding(false));
							}
						}
					}
					return this.encNoBOMwithFallback;
				}
			}

			// Token: 0x06002010 RID: 8208 RVA: 0x00070104 File Offset: 0x0006E304
			public override void Write(string value)
			{
				try
				{
					byte[] bytes = this.Encoding.GetBytes(value);
					this.stream.Write(bytes, 0, bytes.Length);
				}
				catch (Exception)
				{
					if (this.stream is BufferedStream2)
					{
						((BufferedStream2)this.stream).DiscardBuffer();
					}
				}
			}

			// Token: 0x06002011 RID: 8209 RVA: 0x00070160 File Offset: 0x0006E360
			public override void Flush()
			{
				this.stream.Flush();
			}

			// Token: 0x06002012 RID: 8210 RVA: 0x00070170 File Offset: 0x0006E370
			protected override void Dispose(bool disposing)
			{
				try
				{
					if (disposing)
					{
						this.stream.Close();
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}

			// Token: 0x04001325 RID: 4901
			private Encoding encNoBOMwithFallback;

			// Token: 0x04001326 RID: 4902
			private Stream stream;

			// Token: 0x04001327 RID: 4903
			private object m_lockObject = new object();
		}
	}
}
