using System;
using System.Xml;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000525 RID: 1317
	public class XmlHierarchicalDataSourceView : HierarchicalDataSourceView
	{
		// Token: 0x060042BB RID: 17083 RVA: 0x000D9B86 File Offset: 0x000D7D86
		internal XmlHierarchicalDataSourceView(XmlDataSource owner, string viewPath)
		{
			this._owner = owner;
			this._viewPath = viewPath;
		}

		// Token: 0x060042BC RID: 17084 RVA: 0x000D9B9C File Offset: 0x000D7D9C
		public override IHierarchicalEnumerable Select()
		{
			XmlNode xmlDocument = this._owner.GetXmlDocument();
			XmlNodeList nodeList = null;
			if (!string.IsNullOrEmpty(this._viewPath))
			{
				XmlNode xmlNode = xmlDocument.SelectSingleNode(this._viewPath);
				if (xmlNode != null)
				{
					nodeList = xmlNode.ChildNodes;
				}
			}
			else if (this._owner.XPath.Length > 0)
			{
				nodeList = xmlDocument.SelectNodes(this._owner.XPath);
			}
			else
			{
				nodeList = xmlDocument.ChildNodes;
			}
			return new XmlHierarchicalEnumerable(nodeList);
		}

		// Token: 0x04002582 RID: 9602
		private XmlDataSource _owner;

		// Token: 0x04002583 RID: 9603
		private string _viewPath;
	}
}
