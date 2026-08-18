using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Permissions;
using System.Xml;
using System.Xml.XPath;

namespace System.Web.UI.Design
{
	// Token: 0x0200008C RID: 140
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public sealed class XmlDocumentSchema : IDataSourceSchema
	{
		// Token: 0x06000451 RID: 1105 RVA: 0x00013B3D File Offset: 0x00011D3D
		public XmlDocumentSchema(XmlDocument xmlDocument, string xPath) : this(xmlDocument, xPath, false)
		{
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00013B48 File Offset: 0x00011D48
		internal XmlDocumentSchema(XmlDocument xmlDocument, string xPath, bool includeSpecialSchema)
		{
			if (xmlDocument == null)
			{
				throw new ArgumentNullException("xmlDocument");
			}
			this._includeSpecialSchema = includeSpecialSchema;
			this._rootSchema = new OrderedDictionary();
			XPathNavigator xpathNavigator = xmlDocument.CreateNavigator();
			if (!string.IsNullOrEmpty(xPath))
			{
				XPathNodeIterator xpathNodeIterator = xpathNavigator.Select(xPath);
				while (xpathNodeIterator.MoveNext())
				{
					XPathNavigator xpathNavigator2 = xpathNodeIterator.Current;
					XPathNodeIterator xpathNodeIterator2 = xpathNavigator2.SelectDescendants(XPathNodeType.Element, true);
					while (xpathNodeIterator2.MoveNext())
					{
						XPathNavigator nav = xpathNodeIterator2.Current;
						this.AddSchemaElement(nav, xpathNodeIterator.Current);
					}
				}
				return;
			}
			XPathNodeIterator xpathNodeIterator3 = xpathNavigator.SelectDescendants(XPathNodeType.Element, true);
			while (xpathNodeIterator3.MoveNext())
			{
				XPathNavigator nav2 = xpathNodeIterator3.Current;
				this.AddSchemaElement(nav2, xpathNavigator);
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00013BE8 File Offset: 0x00011DE8
		private void AddSchemaElement(XPathNavigator nav, XPathNavigator rootNav)
		{
			List<string> list = new List<string>();
			XPathNodeIterator xpathNodeIterator = nav.SelectAncestors(XPathNodeType.Element, true);
			while (xpathNodeIterator.MoveNext())
			{
				XPathNavigator xpathNavigator = xpathNodeIterator.Current;
				list.Add(xpathNavigator.Name);
				if (xpathNodeIterator.Current.IsSamePosition(rootNav))
				{
					break;
				}
			}
			list.Reverse();
			OrderedDictionary orderedDictionary = this._rootSchema;
			Pair pair = null;
			foreach (string key in list)
			{
				pair = (orderedDictionary[key] as Pair);
				if (pair == null)
				{
					pair = new Pair(new OrderedDictionary(), new ArrayList());
					orderedDictionary.Add(key, pair);
				}
				orderedDictionary = (OrderedDictionary)pair.First;
			}
			this.AddAttributeList(nav, (ArrayList)pair.Second);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00013CC0 File Offset: 0x00011EC0
		private void AddAttributeList(XPathNavigator nav, ArrayList attrs)
		{
			if (!nav.HasAttributes)
			{
				return;
			}
			bool flag = nav.MoveToFirstAttribute();
			do
			{
				if (!attrs.Contains(nav.Name))
				{
					attrs.Add(nav.Name);
				}
			}
			while (nav.MoveToNextAttribute());
			flag = nav.MoveToParent();
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00013D08 File Offset: 0x00011F08
		public IDataSourceViewSchema[] GetViews()
		{
			if (this._viewSchemas == null)
			{
				this._viewSchemas = new IDataSourceViewSchema[this._rootSchema.Count];
				int num = 0;
				foreach (object obj in this._rootSchema)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					this._viewSchemas[num] = new XmlDocumentViewSchema((string)dictionaryEntry.Key, (Pair)dictionaryEntry.Value, this._includeSpecialSchema);
					num++;
				}
			}
			return this._viewSchemas;
		}

		// Token: 0x040001BC RID: 444
		private OrderedDictionary _rootSchema;

		// Token: 0x040001BD RID: 445
		private IDataSourceViewSchema[] _viewSchemas;

		// Token: 0x040001BE RID: 446
		private bool _includeSpecialSchema;
	}
}
