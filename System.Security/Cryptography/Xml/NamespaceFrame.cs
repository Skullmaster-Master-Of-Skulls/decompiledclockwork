using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000094 RID: 148
	internal class NamespaceFrame
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0000ECA4 File Offset: 0x0000DCA4
		internal NamespaceFrame()
		{
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000ECC2 File Offset: 0x0000DCC2
		internal void AddRendered(XmlAttribute attr)
		{
			this.m_rendered.Add(Utils.GetNamespacePrefix(attr), attr);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000ECD6 File Offset: 0x0000DCD6
		internal XmlAttribute GetRendered(string nsPrefix)
		{
			return (XmlAttribute)this.m_rendered[nsPrefix];
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000ECE9 File Offset: 0x0000DCE9
		internal void AddUnrendered(XmlAttribute attr)
		{
			this.m_unrendered.Add(Utils.GetNamespacePrefix(attr), attr);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000ECFD File Offset: 0x0000DCFD
		internal XmlAttribute GetUnrendered(string nsPrefix)
		{
			return (XmlAttribute)this.m_unrendered[nsPrefix];
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000ED10 File Offset: 0x0000DD10
		internal Hashtable GetUnrendered()
		{
			return this.m_unrendered;
		}

		// Token: 0x040004F2 RID: 1266
		private Hashtable m_rendered = new Hashtable();

		// Token: 0x040004F3 RID: 1267
		private Hashtable m_unrendered = new Hashtable();
	}
}
