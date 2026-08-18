using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace System.Diagnostics
{
	// Token: 0x020004BB RID: 1211
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class XmlWriterTraceListener : TextWriterTraceListener
	{
		// Token: 0x06002D40 RID: 11584 RVA: 0x000CBB8C File Offset: 0x000C9D8C
		public XmlWriterTraceListener(Stream stream) : base(stream)
		{
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x000CBBA0 File Offset: 0x000C9DA0
		public XmlWriterTraceListener(Stream stream, string name) : base(stream, name)
		{
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x000CBBB5 File Offset: 0x000C9DB5
		public XmlWriterTraceListener(TextWriter writer) : base(writer)
		{
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x000CBBC9 File Offset: 0x000C9DC9
		public XmlWriterTraceListener(TextWriter writer, string name) : base(writer, name)
		{
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000CBBDE File Offset: 0x000C9DDE
		public XmlWriterTraceListener(string filename) : base(filename)
		{
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x000CBBF2 File Offset: 0x000C9DF2
		public XmlWriterTraceListener(string filename, string name) : base(filename, name)
		{
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x000CBC07 File Offset: 0x000C9E07
		public override void Write(string message)
		{
			this.WriteLine(message);
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x000CBC10 File Offset: 0x000C9E10
		public override void WriteLine(string message)
		{
			this.TraceEvent(null, SR.GetString("TraceAsTraceSource"), TraceEventType.Information, 0, message);
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x000CBC28 File Offset: 0x000C9E28
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

		// Token: 0x06002D49 RID: 11593 RVA: 0x000CBC6C File Offset: 0x000C9E6C
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, format, args))
			{
				return;
			}
			this.WriteHeader(source, eventType, id, eventCache);
			string str;
			if (args != null)
			{
				str = string.Format(CultureInfo.InvariantCulture, format, args);
			}
			else
			{
				str = format;
			}
			this.WriteEscaped(str);
			this.WriteFooter(eventCache);
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x000CBCC9 File Offset: 0x000C9EC9
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, message))
			{
				return;
			}
			this.WriteHeader(source, eventType, id, eventCache);
			this.WriteEscaped(message);
			this.WriteFooter(eventCache);
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x000CBD04 File Offset: 0x000C9F04
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id, eventCache);
			this.InternalWrite("<TraceData>");
			if (data != null)
			{
				this.InternalWrite("<DataItem>");
				this.WriteData(data);
				this.InternalWrite("</DataItem>");
			}
			this.InternalWrite("</TraceData>");
			this.WriteFooter(eventCache);
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000CBD7C File Offset: 0x000C9F7C
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id, eventCache);
			this.InternalWrite("<TraceData>");
			if (data != null)
			{
				for (int i = 0; i < data.Length; i++)
				{
					this.InternalWrite("<DataItem>");
					if (data[i] != null)
					{
						this.WriteData(data[i]);
					}
					this.InternalWrite("</DataItem>");
				}
			}
			this.InternalWrite("</TraceData>");
			this.WriteFooter(eventCache);
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000CBE0C File Offset: 0x000CA00C
		private void WriteData(object data)
		{
			XPathNavigator xpathNavigator = data as XPathNavigator;
			if (xpathNavigator == null)
			{
				this.WriteEscaped(data.ToString());
				return;
			}
			if (this.strBldr == null)
			{
				this.strBldr = new StringBuilder();
				this.xmlBlobWriter = new XmlTextWriter(new StringWriter(this.strBldr, CultureInfo.CurrentCulture));
			}
			else
			{
				this.strBldr.Length = 0;
			}
			try
			{
				xpathNavigator.MoveToRoot();
				this.xmlBlobWriter.WriteNode(xpathNavigator, false);
				this.InternalWrite(this.strBldr.ToString());
			}
			catch (Exception)
			{
				this.InternalWrite(data.ToString());
			}
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000CBEB4 File Offset: 0x000CA0B4
		public override void Close()
		{
			base.Close();
			if (this.xmlBlobWriter != null)
			{
				this.xmlBlobWriter.Close();
			}
			this.xmlBlobWriter = null;
			this.strBldr = null;
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000CBEE0 File Offset: 0x000CA0E0
		public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
		{
			if (this.shouldRespectFilterOnTraceTransfer && base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, TraceEventType.Transfer, id, message))
			{
				return;
			}
			this.WriteHeader(source, TraceEventType.Transfer, id, eventCache, relatedActivityId);
			this.WriteEscaped(message);
			this.WriteFooter(eventCache);
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000CBF34 File Offset: 0x000CA134
		private void WriteHeader(string source, TraceEventType eventType, int id, TraceEventCache eventCache, Guid relatedActivityId)
		{
			this.WriteStartHeader(source, eventType, id, eventCache);
			this.InternalWrite("\" RelatedActivityID=\"");
			this.InternalWrite(relatedActivityId.ToString("B"));
			this.WriteEndHeader(eventCache);
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x000CBF66 File Offset: 0x000CA166
		private void WriteHeader(string source, TraceEventType eventType, int id, TraceEventCache eventCache)
		{
			this.WriteStartHeader(source, eventType, id, eventCache);
			this.WriteEndHeader(eventCache);
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x000CBF7C File Offset: 0x000CA17C
		private void WriteStartHeader(string source, TraceEventType eventType, int id, TraceEventCache eventCache)
		{
			this.InternalWrite("<E2ETraceEvent xmlns=\"http://schemas.microsoft.com/2004/06/E2ETraceEvent\"><System xmlns=\"http://schemas.microsoft.com/2004/06/windows/eventlog/system\">");
			this.InternalWrite("<EventID>");
			uint num = (uint)id;
			this.InternalWrite(num.ToString(CultureInfo.InvariantCulture));
			this.InternalWrite("</EventID>");
			this.InternalWrite("<Type>3</Type>");
			this.InternalWrite("<SubType Name=\"");
			this.InternalWrite(eventType.ToString());
			this.InternalWrite("\">0</SubType>");
			this.InternalWrite("<Level>");
			int num2 = (int)eventType;
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			this.InternalWrite(num2.ToString(CultureInfo.InvariantCulture));
			this.InternalWrite("</Level>");
			this.InternalWrite("<TimeCreated SystemTime=\"");
			if (eventCache != null)
			{
				this.InternalWrite(eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture));
			}
			else
			{
				this.InternalWrite(DateTime.Now.ToString("o", CultureInfo.InvariantCulture));
			}
			this.InternalWrite("\" />");
			this.InternalWrite("<Source Name=\"");
			this.WriteEscaped(source);
			this.InternalWrite("\" />");
			this.InternalWrite("<Correlation ActivityID=\"");
			if (eventCache != null)
			{
				this.InternalWrite(eventCache.ActivityId.ToString("B"));
				return;
			}
			this.InternalWrite(Guid.Empty.ToString("B"));
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x000CC0E8 File Offset: 0x000CA2E8
		private void WriteEndHeader(TraceEventCache eventCache)
		{
			this.InternalWrite("\" />");
			this.InternalWrite("<Execution ProcessName=\"");
			this.InternalWrite(TraceEventCache.GetProcessName());
			this.InternalWrite("\" ProcessID=\"");
			this.InternalWrite(((uint)TraceEventCache.GetProcessId()).ToString(CultureInfo.InvariantCulture));
			this.InternalWrite("\" ThreadID=\"");
			if (eventCache != null)
			{
				this.WriteEscaped(eventCache.ThreadId.ToString(CultureInfo.InvariantCulture));
			}
			else
			{
				this.WriteEscaped(TraceEventCache.GetThreadId().ToString(CultureInfo.InvariantCulture));
			}
			this.InternalWrite("\" />");
			this.InternalWrite("<Channel/>");
			this.InternalWrite("<Computer>");
			this.InternalWrite(this.machineName);
			this.InternalWrite("</Computer>");
			this.InternalWrite("</System>");
			this.InternalWrite("<ApplicationData>");
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000CC1C8 File Offset: 0x000CA3C8
		private void WriteFooter(TraceEventCache eventCache)
		{
			bool flag = base.IsEnabled(TraceOptions.LogicalOperationStack);
			bool flag2 = base.IsEnabled(TraceOptions.Callstack);
			if (eventCache != null && (flag || flag2))
			{
				this.InternalWrite("<System.Diagnostics xmlns=\"http://schemas.microsoft.com/2004/08/System.Diagnostics\">");
				if (flag)
				{
					this.InternalWrite("<LogicalOperationStack>");
					Stack logicalOperationStack = eventCache.LogicalOperationStack;
					if (logicalOperationStack != null)
					{
						foreach (object obj in logicalOperationStack)
						{
							this.InternalWrite("<LogicalOperation>");
							this.WriteEscaped(obj.ToString());
							this.InternalWrite("</LogicalOperation>");
						}
					}
					this.InternalWrite("</LogicalOperationStack>");
				}
				this.InternalWrite("<Timestamp>");
				this.InternalWrite(eventCache.Timestamp.ToString(CultureInfo.InvariantCulture));
				this.InternalWrite("</Timestamp>");
				if (flag2)
				{
					this.InternalWrite("<Callstack>");
					this.WriteEscaped(eventCache.Callstack);
					this.InternalWrite("</Callstack>");
				}
				this.InternalWrite("</System.Diagnostics>");
			}
			this.InternalWrite("</ApplicationData></E2ETraceEvent>");
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000CC2F0 File Offset: 0x000CA4F0
		private void WriteEscaped(string str)
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
								this.InternalWrite(str.Substring(num, i - num));
								this.InternalWrite("&quot;");
								num = i + 1;
							}
						}
						else
						{
							this.InternalWrite(str.Substring(num, i - num));
							this.InternalWrite("&#xD;");
							num = i + 1;
						}
					}
					else
					{
						this.InternalWrite(str.Substring(num, i - num));
						this.InternalWrite("&#xA;");
						num = i + 1;
					}
				}
				else if (c <= '\'')
				{
					if (c != '&')
					{
						if (c == '\'')
						{
							this.InternalWrite(str.Substring(num, i - num));
							this.InternalWrite("&apos;");
							num = i + 1;
						}
					}
					else
					{
						this.InternalWrite(str.Substring(num, i - num));
						this.InternalWrite("&amp;");
						num = i + 1;
					}
				}
				else if (c != '<')
				{
					if (c == '>')
					{
						this.InternalWrite(str.Substring(num, i - num));
						this.InternalWrite("&gt;");
						num = i + 1;
					}
				}
				else
				{
					this.InternalWrite(str.Substring(num, i - num));
					this.InternalWrite("&lt;");
					num = i + 1;
				}
			}
			this.InternalWrite(str.Substring(num, str.Length - num));
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000CC46D File Offset: 0x000CA66D
		private void InternalWrite(string message)
		{
			if (!base.EnsureWriter())
			{
				return;
			}
			this.writer.Write(message);
		}

		// Token: 0x04002714 RID: 10004
		private const string fixedHeader = "<E2ETraceEvent xmlns=\"http://schemas.microsoft.com/2004/06/E2ETraceEvent\"><System xmlns=\"http://schemas.microsoft.com/2004/06/windows/eventlog/system\">";

		// Token: 0x04002715 RID: 10005
		private readonly string machineName = Environment.MachineName;

		// Token: 0x04002716 RID: 10006
		private StringBuilder strBldr;

		// Token: 0x04002717 RID: 10007
		private XmlTextWriter xmlBlobWriter;

		// Token: 0x04002718 RID: 10008
		internal bool shouldRespectFilterOnTraceTransfer;
	}
}
