using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace System.Xml.XPath
{
	// Token: 0x0200000B RID: 11
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class XDocumentExtensions
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00003C1F File Offset: 0x00001E1F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IXPathNavigable ToXPathNavigable(this XNode node)
		{
			return new XDocumentExtensions.XDocumentNavigable(node);
		}

		// Token: 0x02000041 RID: 65
		private class XDocumentNavigable : IXPathNavigable
		{
			// Token: 0x060002EA RID: 746 RVA: 0x0000C797 File Offset: 0x0000A997
			public XDocumentNavigable(XNode n)
			{
				this._node = n;
			}

			// Token: 0x060002EB RID: 747 RVA: 0x0000C7A6 File Offset: 0x0000A9A6
			public XPathNavigator CreateNavigator()
			{
				return this._node.CreateNavigator();
			}

			// Token: 0x04000104 RID: 260
			private XNode _node;
		}
	}
}
