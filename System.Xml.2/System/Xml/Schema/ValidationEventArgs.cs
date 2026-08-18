using System;

namespace System.Xml.Schema
{
	// Token: 0x02000263 RID: 611
	public class ValidationEventArgs : EventArgs
	{
		// Token: 0x0600248F RID: 9359 RVA: 0x000C8429 File Offset: 0x000C6629
		internal ValidationEventArgs(XmlSchemaException ex)
		{
			this.ex = ex;
			this.severity = XmlSeverityType.Error;
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000C843F File Offset: 0x000C663F
		internal ValidationEventArgs(XmlSchemaException ex, XmlSeverityType severity)
		{
			this.ex = ex;
			this.severity = severity;
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06002491 RID: 9361 RVA: 0x000C8455 File Offset: 0x000C6655
		public XmlSeverityType Severity
		{
			get
			{
				return this.severity;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06002492 RID: 9362 RVA: 0x000C845D File Offset: 0x000C665D
		public XmlSchemaException Exception
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06002493 RID: 9363 RVA: 0x000C8465 File Offset: 0x000C6665
		public string Message
		{
			get
			{
				return this.ex.Message;
			}
		}

		// Token: 0x04000FE2 RID: 4066
		private XmlSchemaException ex;

		// Token: 0x04000FE3 RID: 4067
		private XmlSeverityType severity;
	}
}
