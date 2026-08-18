using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using NLog.Config;
using NLog.Internal;
using NLog.Internal.Fakeables;
using NLog.Targets;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000DA RID: 218
	[LayoutRenderer("log4jxmlevent")]
	public class Log4JXmlEventLayoutRenderer : LayoutRenderer, IUsesStackTrace
	{
		// Token: 0x0600064C RID: 1612 RVA: 0x0000E00D File Offset: 0x0000C20D
		public Log4JXmlEventLayoutRenderer() : this(AppDomainWrapper.CurrentDomain)
		{
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0000E01C File Offset: 0x0000C21C
		public Log4JXmlEventLayoutRenderer(IAppDomain appDomain)
		{
			this.IncludeNLogData = true;
			this.NdcItemSeparator = " ";
			this.AppInfo = string.Format(CultureInfo.InvariantCulture, "{0}({1})", new object[]
			{
				appDomain.FriendlyName,
				ThreadIDHelper.Instance.CurrentProcessID
			});
			this.Parameters = new List<NLogViewerParameterInfo>();
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0000E084 File Offset: 0x0000C284
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x0000E08C File Offset: 0x0000C28C
		[DefaultValue(true)]
		public bool IncludeNLogData { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0000E095 File Offset: 0x0000C295
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x0000E09D File Offset: 0x0000C29D
		public bool IndentXml { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0000E0A6 File Offset: 0x0000C2A6
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x0000E0AE File Offset: 0x0000C2AE
		public string AppInfo { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0000E0B7 File Offset: 0x0000C2B7
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x0000E0BF File Offset: 0x0000C2BF
		public bool IncludeCallSite { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0000E0C8 File Offset: 0x0000C2C8
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x0000E0D0 File Offset: 0x0000C2D0
		public bool IncludeSourceInfo { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0000E0D9 File Offset: 0x0000C2D9
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x0000E0E1 File Offset: 0x0000C2E1
		public bool IncludeMdc { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0000E0EA File Offset: 0x0000C2EA
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x0000E0F2 File Offset: 0x0000C2F2
		public bool IncludeNdc { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0000E0FB File Offset: 0x0000C2FB
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x0000E103 File Offset: 0x0000C303
		[DefaultValue(" ")]
		public string NdcItemSeparator { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0000E10C File Offset: 0x0000C30C
		StackTraceUsage IUsesStackTrace.StackTraceUsage
		{
			get
			{
				if (this.IncludeSourceInfo)
				{
					return StackTraceUsage.WithSource;
				}
				if (this.IncludeCallSite)
				{
					return StackTraceUsage.WithoutSource;
				}
				return StackTraceUsage.None;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0000E123 File Offset: 0x0000C323
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x0000E12B File Offset: 0x0000C32B
		internal IList<NLogViewerParameterInfo> Parameters { get; set; }

		// Token: 0x06000661 RID: 1633 RVA: 0x0000E134 File Offset: 0x0000C334
		internal void AppendToStringBuilder(StringBuilder sb, LogEventInfo logEvent)
		{
			this.Append(sb, logEvent);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0000E140 File Offset: 0x0000C340
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			XmlWriterSettings settings = new XmlWriterSettings
			{
				Indent = this.IndentXml,
				ConformanceLevel = ConformanceLevel.Fragment,
				IndentChars = "  "
			};
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, settings))
			{
				xmlWriter.WriteStartElement("log4j", "event", Log4JXmlEventLayoutRenderer.dummyNamespace);
				xmlWriter.WriteAttributeSafeString("xmlns", "nlog", null, Log4JXmlEventLayoutRenderer.dummyNLogNamespace);
				xmlWriter.WriteAttributeSafeString("logger", logEvent.LoggerName);
				xmlWriter.WriteAttributeSafeString("level", logEvent.Level.Name.ToUpper(CultureInfo.InvariantCulture));
				xmlWriter.WriteAttributeSafeString("timestamp", Convert.ToString((long)(logEvent.TimeStamp.ToUniversalTime() - Log4JXmlEventLayoutRenderer.log4jDateBase).TotalMilliseconds, CultureInfo.InvariantCulture));
				xmlWriter.WriteAttributeSafeString("thread", Thread.CurrentThread.ManagedThreadId.ToString(CultureInfo.InvariantCulture));
				xmlWriter.WriteElementSafeString("log4j", "message", Log4JXmlEventLayoutRenderer.dummyNamespace, logEvent.FormattedMessage);
				if (logEvent.Exception != null)
				{
					xmlWriter.WriteElementSafeString("log4j", "throwable", Log4JXmlEventLayoutRenderer.dummyNamespace, logEvent.Exception.ToString());
				}
				if (this.IncludeNdc)
				{
					xmlWriter.WriteElementSafeString("log4j", "NDC", Log4JXmlEventLayoutRenderer.dummyNamespace, string.Join(this.NdcItemSeparator, NestedDiagnosticsContext.GetAllMessages()));
				}
				if (logEvent.Exception != null)
				{
					xmlWriter.WriteStartElement("log4j", "throwable", Log4JXmlEventLayoutRenderer.dummyNamespace);
					xmlWriter.WriteSafeCData(logEvent.Exception.ToString());
					xmlWriter.WriteEndElement();
				}
				if (this.IncludeCallSite || this.IncludeSourceInfo)
				{
					StackFrame userStackFrame = logEvent.UserStackFrame;
					if (userStackFrame != null)
					{
						MethodBase method = userStackFrame.GetMethod();
						Type declaringType = method.DeclaringType;
						xmlWriter.WriteStartElement("log4j", "locationInfo", Log4JXmlEventLayoutRenderer.dummyNamespace);
						if (declaringType != null)
						{
							xmlWriter.WriteAttributeSafeString("class", declaringType.FullName);
						}
						xmlWriter.WriteAttributeSafeString("method", method.ToString());
						if (this.IncludeSourceInfo)
						{
							xmlWriter.WriteAttributeSafeString("file", userStackFrame.GetFileName());
							xmlWriter.WriteAttributeSafeString("line", userStackFrame.GetFileLineNumber().ToString(CultureInfo.InvariantCulture));
						}
						xmlWriter.WriteEndElement();
						if (this.IncludeNLogData)
						{
							xmlWriter.WriteElementSafeString("nlog", "eventSequenceNumber", Log4JXmlEventLayoutRenderer.dummyNLogNamespace, logEvent.SequenceID.ToString(CultureInfo.InvariantCulture));
							xmlWriter.WriteStartElement("nlog", "locationInfo", Log4JXmlEventLayoutRenderer.dummyNLogNamespace);
							if (declaringType != null)
							{
								xmlWriter.WriteAttributeSafeString("assembly", declaringType.Assembly.FullName);
							}
							xmlWriter.WriteEndElement();
							xmlWriter.WriteStartElement("nlog", "properties", Log4JXmlEventLayoutRenderer.dummyNLogNamespace);
							foreach (KeyValuePair<object, object> keyValuePair in logEvent.Properties)
							{
								xmlWriter.WriteStartElement("nlog", "data", Log4JXmlEventLayoutRenderer.dummyNLogNamespace);
								xmlWriter.WriteAttributeSafeString("name", Convert.ToString(keyValuePair.Key, CultureInfo.InvariantCulture));
								xmlWriter.WriteAttributeSafeString("value", Convert.ToString(keyValuePair.Value, CultureInfo.InvariantCulture));
								xmlWriter.WriteEndElement();
							}
							xmlWriter.WriteEndElement();
						}
					}
				}
				xmlWriter.WriteStartElement("log4j", "properties", Log4JXmlEventLayoutRenderer.dummyNamespace);
				if (this.IncludeMdc)
				{
					foreach (string text in MappedDiagnosticsContext.GetNames())
					{
						xmlWriter.WriteStartElement("log4j", "data", Log4JXmlEventLayoutRenderer.dummyNamespace);
						xmlWriter.WriteAttributeSafeString("name", text);
						xmlWriter.WriteAttributeSafeString("value", string.Format(logEvent.FormatProvider, "{0}", new object[]
						{
							MappedDiagnosticsContext.GetObject(text)
						}));
						xmlWriter.WriteEndElement();
					}
				}
				foreach (NLogViewerParameterInfo nlogViewerParameterInfo in this.Parameters)
				{
					xmlWriter.WriteStartElement("log4j", "data", Log4JXmlEventLayoutRenderer.dummyNamespace);
					xmlWriter.WriteAttributeSafeString("name", nlogViewerParameterInfo.Name);
					xmlWriter.WriteAttributeSafeString("value", nlogViewerParameterInfo.Layout.Render(logEvent));
					xmlWriter.WriteEndElement();
				}
				xmlWriter.WriteStartElement("log4j", "data", Log4JXmlEventLayoutRenderer.dummyNamespace);
				xmlWriter.WriteAttributeSafeString("name", "log4japp");
				xmlWriter.WriteAttributeSafeString("value", this.AppInfo);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("log4j", "data", Log4JXmlEventLayoutRenderer.dummyNamespace);
				xmlWriter.WriteAttributeSafeString("name", "log4jmachinename");
				xmlWriter.WriteAttributeSafeString("value", Environment.MachineName);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.Flush();
				stringBuilder.Replace(" xmlns:log4j=\"" + Log4JXmlEventLayoutRenderer.dummyNamespace + "\"", string.Empty);
				stringBuilder.Replace(" xmlns:nlog=\"" + Log4JXmlEventLayoutRenderer.dummyNLogNamespace + "\"", string.Empty);
				builder.Append(stringBuilder.ToString());
			}
		}

		// Token: 0x04000193 RID: 403
		private static readonly DateTime log4jDateBase = new DateTime(1970, 1, 1);

		// Token: 0x04000194 RID: 404
		private static readonly string dummyNamespace = "http://nlog-project.org/dummynamespace/" + Guid.NewGuid();

		// Token: 0x04000195 RID: 405
		private static readonly string dummyNLogNamespace = "http://nlog-project.org/dummynamespace/" + Guid.NewGuid();
	}
}
