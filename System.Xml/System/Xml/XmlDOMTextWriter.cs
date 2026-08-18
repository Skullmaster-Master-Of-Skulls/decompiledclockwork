using System;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x020000D9 RID: 217
	internal class XmlDOMTextWriter : XmlTextWriter
	{
		// Token: 0x06000D44 RID: 3396 RVA: 0x0003B464 File Offset: 0x0003A464
		public XmlDOMTextWriter(Stream w, Encoding encoding) : base(w, encoding)
		{
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0003B46E File Offset: 0x0003A46E
		public XmlDOMTextWriter(string filename, Encoding encoding) : base(filename, encoding)
		{
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0003B478 File Offset: 0x0003A478
		public XmlDOMTextWriter(TextWriter w) : base(w)
		{
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0003B481 File Offset: 0x0003A481
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (ns.Length == 0 && prefix.Length != 0)
			{
				prefix = "";
			}
			base.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0003B4A3 File Offset: 0x0003A4A3
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
