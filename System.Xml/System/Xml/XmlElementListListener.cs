using System;

namespace System.Xml
{
	// Token: 0x020000DE RID: 222
	internal class XmlElementListListener
	{
		// Token: 0x06000D97 RID: 3479 RVA: 0x0003C270 File Offset: 0x0003B270
		internal XmlElementListListener(XmlDocument doc, XmlElementList elemList)
		{
			this.doc = doc;
			this.elemList = new WeakReference(elemList);
			this.nodeChangeHandler = new XmlNodeChangedEventHandler(this.OnListChanged);
			doc.NodeInserted += this.nodeChangeHandler;
			doc.NodeRemoved += this.nodeChangeHandler;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0003C2C0 File Offset: 0x0003B2C0
		private void OnListChanged(object sender, XmlNodeChangedEventArgs args)
		{
			XmlElementList xmlElementList = (XmlElementList)this.elemList.Target;
			if (xmlElementList != null)
			{
				xmlElementList.ConcurrencyCheck(args);
				return;
			}
			this.doc.NodeInserted -= this.nodeChangeHandler;
			this.doc.NodeRemoved -= this.nodeChangeHandler;
		}

		// Token: 0x04000958 RID: 2392
		private WeakReference elemList;

		// Token: 0x04000959 RID: 2393
		private XmlDocument doc;

		// Token: 0x0400095A RID: 2394
		private XmlNodeChangedEventHandler nodeChangeHandler;
	}
}
