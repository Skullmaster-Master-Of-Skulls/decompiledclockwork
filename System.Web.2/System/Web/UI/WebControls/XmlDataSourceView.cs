using System;
using System.Collections;
using System.Xml;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000524 RID: 1316
	public sealed class XmlDataSourceView : DataSourceView
	{
		// Token: 0x060042B8 RID: 17080 RVA: 0x000D9B1B File Offset: 0x000D7D1B
		public XmlDataSourceView(XmlDataSource owner, string name) : base(owner, name)
		{
			this._owner = owner;
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x000D9B2C File Offset: 0x000D7D2C
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			arguments.RaiseUnsupportedCapabilitiesError(this);
			XmlNode xmlDocument = this._owner.GetXmlDocument();
			XmlNodeList nodes;
			if (this._owner.XPath.Length != 0)
			{
				nodes = xmlDocument.SelectNodes(this._owner.XPath);
			}
			else
			{
				nodes = xmlDocument.SelectNodes("/node()/node()");
			}
			return new XmlDataSourceView.XmlDataSourceNodeDescriptorEnumeration(nodes);
		}

		// Token: 0x060042BA RID: 17082 RVA: 0x000B940C File Offset: 0x000B760C
		public IEnumerable Select(DataSourceSelectArguments arguments)
		{
			return this.ExecuteSelect(arguments);
		}

		// Token: 0x04002581 RID: 9601
		private XmlDataSource _owner;

		// Token: 0x020009E0 RID: 2528
		private class XmlDataSourceNodeDescriptorEnumeration : ICollection, IEnumerable
		{
			// Token: 0x06006CEE RID: 27886 RVA: 0x00186196 File Offset: 0x00184396
			public XmlDataSourceNodeDescriptorEnumeration(XmlNodeList nodes)
			{
				this._nodes = nodes;
			}

			// Token: 0x06006CEF RID: 27887 RVA: 0x001861AC File Offset: 0x001843AC
			IEnumerator IEnumerable.GetEnumerator()
			{
				foreach (object obj in this._nodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						yield return new XmlDataSourceNodeDescriptor(xmlNode);
					}
				}
				IEnumerator enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x17001DFC RID: 7676
			// (get) Token: 0x06006CF0 RID: 27888 RVA: 0x001861BC File Offset: 0x001843BC
			int ICollection.Count
			{
				get
				{
					if (this._count == -1)
					{
						this._count = 0;
						foreach (object obj in this._nodes)
						{
							XmlNode xmlNode = (XmlNode)obj;
							if (xmlNode.NodeType == XmlNodeType.Element)
							{
								this._count++;
							}
						}
					}
					return this._count;
				}
			}

			// Token: 0x17001DFD RID: 7677
			// (get) Token: 0x06006CF1 RID: 27889 RVA: 0x00007722 File Offset: 0x00005922
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001DFE RID: 7678
			// (get) Token: 0x06006CF2 RID: 27890 RVA: 0x0000298D File Offset: 0x00000B8D
			object ICollection.SyncRoot
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06006CF3 RID: 27891 RVA: 0x0018623C File Offset: 0x0018443C
			void ICollection.CopyTo(Array array, int index)
			{
				foreach (object value in ((IEnumerable)this))
				{
					array.SetValue(value, index++);
				}
			}

			// Token: 0x040039FA RID: 14842
			private XmlNodeList _nodes;

			// Token: 0x040039FB RID: 14843
			private int _count = -1;
		}
	}
}
