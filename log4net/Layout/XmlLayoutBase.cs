using System;
using System.IO;
using System.Xml;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout
{
	// Token: 0x020000B3 RID: 179
	public abstract class XmlLayoutBase : LayoutSkeleton
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x0000FFFD File Offset: 0x0000E1FD
		protected XmlLayoutBase() : this(false)
		{
			this.IgnoresException = false;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001000D File Offset: 0x0000E20D
		protected XmlLayoutBase(bool locationInfo)
		{
			this.IgnoresException = false;
			this.m_locationInfo = locationInfo;
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0001002E File Offset: 0x0000E22E
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x00010036 File Offset: 0x0000E236
		public bool LocationInfo
		{
			get
			{
				return this.m_locationInfo;
			}
			set
			{
				this.m_locationInfo = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0001003F File Offset: 0x0000E23F
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x00010047 File Offset: 0x0000E247
		public string InvalidCharReplacement
		{
			get
			{
				return this.m_invalidCharReplacement;
			}
			set
			{
				this.m_invalidCharReplacement = value;
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00010050 File Offset: 0x0000E250
		public override void ActivateOptions()
		{
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00010052 File Offset: 0x0000E252
		public override string ContentType
		{
			get
			{
				return "text/xml";
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001005C File Offset: 0x0000E25C
		public override void Format(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			XmlTextWriter xmlTextWriter = new XmlTextWriter(new ProtectCloseTextWriter(writer));
			xmlTextWriter.Formatting = Formatting.None;
			xmlTextWriter.Namespaces = false;
			this.FormatXml(xmlTextWriter, loggingEvent);
			xmlTextWriter.WriteWhitespace(SystemInfo.NewLine);
			xmlTextWriter.Close();
		}

		// Token: 0x0600051E RID: 1310
		protected abstract void FormatXml(XmlWriter writer, LoggingEvent loggingEvent);

		// Token: 0x04000214 RID: 532
		private bool m_locationInfo;

		// Token: 0x04000215 RID: 533
		private string m_invalidCharReplacement = "?";
	}
}
