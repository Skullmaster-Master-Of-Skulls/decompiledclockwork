using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200002F RID: 47
	internal class NamespaceFrame
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00006323 File Offset: 0x00004523
		internal NamespaceFrame()
		{
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006341 File Offset: 0x00004541
		internal void AddRendered(XmlAttribute attr)
		{
			this.m_rendered.Add(Utils.GetNamespacePrefix(attr), attr);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006355 File Offset: 0x00004555
		internal XmlAttribute GetRendered(string nsPrefix)
		{
			return (XmlAttribute)this.m_rendered[nsPrefix];
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006368 File Offset: 0x00004568
		internal void AddUnrendered(XmlAttribute attr)
		{
			this.m_unrendered.Add(Utils.GetNamespacePrefix(attr), attr);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000637C File Offset: 0x0000457C
		internal XmlAttribute GetUnrendered(string nsPrefix)
		{
			return (XmlAttribute)this.m_unrendered[nsPrefix];
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000638F File Offset: 0x0000458F
		internal Hashtable GetUnrendered()
		{
			return this.m_unrendered;
		}

		// Token: 0x040003A1 RID: 929
		private Hashtable m_rendered = new Hashtable();

		// Token: 0x040003A2 RID: 930
		private Hashtable m_unrendered = new Hashtable();
	}
}
