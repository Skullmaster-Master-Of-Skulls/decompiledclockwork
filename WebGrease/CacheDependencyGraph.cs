using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace WebGrease
{
	// Token: 0x020000E1 RID: 225
	internal class CacheDependencyGraph
	{
		// Token: 0x06000EBA RID: 3770 RVA: 0x000450F8 File Offset: 0x000432F8
		internal void AddDependencyLink(string label1, string label2)
		{
			Guid key = this.AddDependencyNode(label1);
			Guid value = this.AddDependencyNode(label2);
			KeyValuePair<Guid, Guid> item = new KeyValuePair<Guid, Guid>(key, value);
			if (!this.links.Contains(item))
			{
				this.links.Add(item);
			}
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0004520C File Offset: 0x0004340C
		internal void Save(string path)
		{
			XNamespace xmlns = XNamespace.Get("http://schemas.microsoft.com/vs/2009/dgml");
			XDocument xdocument = new XDocument(new XDeclaration("1.0", "utf-8", "no"), new object[]
			{
				new XElement(xmlns + "DirectedGraph", new object[]
				{
					new XAttribute("GraphDirection", "TopToBottom"),
					new XAttribute("Layout", "Sugiyama"),
					new XElement(xmlns + "Nodes", from kvp in this.nodes
					select new XElement(xmlns + "Node", new object[]
					{
						new XAttribute("Id", kvp.Value),
						new XAttribute("Label", kvp.Key)
					})),
					new XElement(xmlns + "Links", from kvp in this.links
					select new XElement(xmlns + "Link", new object[]
					{
						new XAttribute("Source", kvp.Key),
						new XAttribute("Target", kvp.Value)
					})),
					new XElement(xmlns + "Properties", new object[]
					{
						new XElement(xmlns + "Property", new object[]
						{
							new XAttribute("Id", "GraphDirection"),
							new XAttribute("DataType", "Microsoft.VisualStudio.Diagrams.Layout.LayoutOrientation")
						}),
						new XElement(xmlns + "Property", new object[]
						{
							new XAttribute("Id", "Layout"),
							new XAttribute("DataType", "System.String")
						}),
						new XElement(xmlns + "Property", new object[]
						{
							new XAttribute("Id", "Bounds"),
							new XAttribute("DataType", "System.String")
						}),
						new XElement(xmlns + "Property", new object[]
						{
							new XAttribute("Id", "Label"),
							new XAttribute("Label", "Label"),
							new XAttribute("Description", "Displayable label of an Annotatable object"),
							new XAttribute("DataType", "System.String")
						})
					})
				})
			});
			xdocument.Save(path);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x000454A4 File Offset: 0x000436A4
		private Guid AddDependencyNode(string label)
		{
			string key = label.ToLowerInvariant();
			if (!this.nodes.ContainsKey(key))
			{
				this.nodes.Add(key, Guid.NewGuid());
			}
			return this.nodes[key];
		}

		// Token: 0x040005B5 RID: 1461
		private readonly List<KeyValuePair<Guid, Guid>> links = new List<KeyValuePair<Guid, Guid>>();

		// Token: 0x040005B6 RID: 1462
		private readonly IDictionary<string, Guid> nodes = new Dictionary<string, Guid>();
	}
}
