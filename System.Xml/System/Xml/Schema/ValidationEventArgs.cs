using System;

namespace System.Xml.Schema
{
	// Token: 0x0200021A RID: 538
	public class ValidationEventArgs : EventArgs
	{
		// Token: 0x060019BD RID: 6589 RVA: 0x0007BC4D File Offset: 0x0007AC4D
		internal ValidationEventArgs(XmlSchemaException ex)
		{
			this.ex = ex;
			this.severity = XmlSeverityType.Error;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0007BC63 File Offset: 0x0007AC63
		internal ValidationEventArgs(XmlSchemaException ex, XmlSeverityType severity)
		{
			this.ex = ex;
			this.severity = severity;
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x0007BC79 File Offset: 0x0007AC79
		public XmlSeverityType Severity
		{
			get
			{
				return this.severity;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060019C0 RID: 6592 RVA: 0x0007BC81 File Offset: 0x0007AC81
		public XmlSchemaException Exception
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x0007BC89 File Offset: 0x0007AC89
		public string Message
		{
			get
			{
				return this.ex.Message;
			}
		}

		// Token: 0x0400100F RID: 4111
		private XmlSchemaException ex;

		// Token: 0x04001010 RID: 4112
		private XmlSeverityType severity;
	}
}
