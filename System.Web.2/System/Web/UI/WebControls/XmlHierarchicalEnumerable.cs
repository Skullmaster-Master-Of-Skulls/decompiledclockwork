using System;
using System.Collections;
using System.Xml;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000526 RID: 1318
	internal sealed class XmlHierarchicalEnumerable : IHierarchicalEnumerable, IEnumerable
	{
		// Token: 0x060042BD RID: 17085 RVA: 0x000D9C11 File Offset: 0x000D7E11
		internal XmlHierarchicalEnumerable(XmlNodeList nodeList)
		{
			this._nodeList = nodeList;
		}

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x060042BE RID: 17086 RVA: 0x000D9C20 File Offset: 0x000D7E20
		// (set) Token: 0x060042BF RID: 17087 RVA: 0x000D9C28 File Offset: 0x000D7E28
		internal string Path
		{
			get
			{
				return this._path;
			}
			set
			{
				this._path = value;
			}
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x000D9C31 File Offset: 0x000D7E31
		IEnumerator IEnumerable.GetEnumerator()
		{
			foreach (object obj in this._nodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					yield return new XmlHierarchyData(this, xmlNode);
				}
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060042C1 RID: 17089 RVA: 0x000D9C40 File Offset: 0x000D7E40
		IHierarchyData IHierarchicalEnumerable.GetHierarchyData(object enumeratedItem)
		{
			return (IHierarchyData)enumeratedItem;
		}

		// Token: 0x04002584 RID: 9604
		private string _path;

		// Token: 0x04002585 RID: 9605
		private XmlNodeList _nodeList;
	}
}
