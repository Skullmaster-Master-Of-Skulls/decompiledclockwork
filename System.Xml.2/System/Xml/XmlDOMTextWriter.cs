using System;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000106 RID: 262
	internal class XmlDOMTextWriter : XmlTextWriter
	{
		// Token: 0x06001275 RID: 4725 RVA: 0x0004D224 File Offset: 0x0004B424
		public XmlDOMTextWriter(Stream w, Encoding encoding) : base(w, encoding)
		{
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0004D22E File Offset: 0x0004B42E
		public XmlDOMTextWriter(string filename, Encoding encoding) : base(filename, encoding)
		{
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0004D238 File Offset: 0x0004B438
		public XmlDOMTextWriter(TextWriter w) : base(w)
		{
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0004D241 File Offset: 0x0004B441
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (ns.Length == 0 && prefix.Length != 0)
			{
				prefix = "";
			}
			base.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0004D263 File Offset: 0x0004B463
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (ns.Length == 0 && prefix.Length != 0)
			{
				prefix = "";
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}
	}
}
