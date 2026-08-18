using System;

namespace System.Xml
{
	// Token: 0x0200010B RID: 267
	internal class XmlElementListListener
	{
		// Token: 0x060012CB RID: 4811 RVA: 0x0004E084 File Offset: 0x0004C284
		internal XmlElementListListener(XmlDocument doc, XmlElementList elemList)
		{
			this.doc = doc;
			this.elemList = new WeakReference(elemList);
			this.nodeChangeHandler = new XmlNodeChangedEventHandler(this.OnListChanged);
			doc.NodeInserted += this.nodeChangeHandler;
			doc.NodeRemoved += this.nodeChangeHandler;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0004E0D4 File Offset: 0x0004C2D4
		private void OnListChanged(object sender, XmlNodeChangedEventArgs args)
		{
			lock (this)
			{
				if (this.elemList != null)
				{
					XmlElementList xmlElementList = (XmlElementList)this.elemList.Target;
					if (xmlElementList != null)
					{
						xmlElementList.ConcurrencyCheck(args);
					}
					else
					{
						this.doc.NodeInserted -= this.nodeChangeHandler;
						this.doc.NodeRemoved -= this.nodeChangeHandler;
						this.elemList = null;
					}
				}
			}
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0004E158 File Offset: 0x0004C358
		internal void Unregister()
		{
			lock (this)
			{
				if (this.elemList != null)
				{
					this.doc.NodeInserted -= this.nodeChangeHandler;
					this.doc.NodeRemoved -= this.nodeChangeHandler;
					this.elemList = null;
				}
			}
		}

		// Token: 0x04000538 RID: 1336
		private WeakReference elemList;

		// Token: 0x04000539 RID: 1337
		private XmlDocument doc;

		// Token: 0x0400053A RID: 1338
		private XmlNodeChangedEventHandler nodeChangeHandler;
	}
}
