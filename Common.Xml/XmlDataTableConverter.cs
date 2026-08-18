using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Xml.Entity;

namespace TechnoPro.Common.Xml
{
	// Token: 0x02000002 RID: 2
	public static class XmlDataTableConverter
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static void ConvertForestToString(StringBuilder sb, int indent, TreeNodeCollection<XmlEntry> currentNodes)
		{
			foreach (TreeNode<XmlEntry> treeNode in currentNodes)
			{
				sb.AppendLine(((indent > 0) ? new string(' ', indent) : "") + treeNode.Value.Name + "=" + treeNode.Value.Value);
				if (treeNode.Nodes.Count > 0)
				{
					XmlDataTableConverter.ConvertForestToString(sb, indent + 5, treeNode.Nodes);
				}
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E8 File Offset: 0x000002E8
		private static void ExtractForestFromXml(Forest<XmlEntry> forest, TreeNode<XmlEntry> currentParentNode, IList<XElement> elements)
		{
			foreach (XElement xelement in elements)
			{
				TreeNode<XmlEntry> currentParentNode2 = forest.AppendNode(currentParentNode, new XmlEntry
				{
					Name = xelement.Name.LocalName,
					Value = xelement.Value
				});
				bool hasAttributes = xelement.HasAttributes;
				if (xelement.HasElements)
				{
					XmlDataTableConverter.ExtractForestFromXml(forest, currentParentNode2, xelement.Elements().ToList<XElement>());
				}
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002174 File Offset: 0x00000374
		private static string GetUniqueColName(DataTable t, string colName)
		{
			if (!t.Columns.Contains(colName))
			{
				return colName;
			}
			int num = 1;
			string text = colName + num;
			while (t.Columns.Contains(text) && num < 10000)
			{
				text = colName + ++num;
			}
			return text;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021CC File Offset: 0x000003CC
		public static DataTable ConvertForestToDataTable(Forest<XmlEntry> forest, string rootNodeName)
		{
			IList<TreeNode<XmlEntry>> list;
			if (string.IsNullOrEmpty(rootNodeName))
			{
				Forest<XmlEntry> forest2 = new Forest<XmlEntry>();
				TreeNode<XmlEntry> parentNode = forest2.AppendNode(null, new XmlEntry
				{
					Name = "item",
					Value = ""
				});
				foreach (TreeNode<XmlEntry> treeNode in forest.Nodes)
				{
					forest2.AppendNode(parentNode, treeNode.Value);
				}
				list = forest2.Nodes.ToList<TreeNode<XmlEntry>>();
			}
			else
			{
				list = forest.FindAll((XmlEntry g) => (g.Name ?? "").Equals(rootNodeName, StringComparison.OrdinalIgnoreCase)).ToList<TreeNode<XmlEntry>>();
			}
			List<string> list2 = new List<string>();
			foreach (TreeNode<XmlEntry> treeNode2 in list)
			{
				foreach (TreeNode<XmlEntry> treeNode3 in (from g in treeNode2.Nodes
				where g.Value.Name.Trim().Length > 0
				select g).ToList<TreeNode<XmlEntry>>())
				{
					string name = treeNode3.Value.Name.Trim();
					if (list2.FirstOrDefault((string g) => g.Equals(name, StringComparison.OrdinalIgnoreCase)) == null)
					{
						list2.Add(name);
						List<TreeNode<XmlEntry>> list3 = (from g in treeNode3.Nodes
						where g.Value.Name.Trim().Length > 0
						select g).ToList<TreeNode<XmlEntry>>();
						if (list3.Count >= 1)
						{
							foreach (TreeNode<XmlEntry> treeNode4 in list3)
							{
								string name2 = name + "." + treeNode4.Value.Name.Trim();
								if (list2.FirstOrDefault((string g) => g.Equals(name2, StringComparison.OrdinalIgnoreCase)) == null)
								{
									list2.Add(name2);
								}
							}
						}
					}
				}
			}
			DataTable t = new DataTable("t");
			Dictionary<string, string> dictionary = list2.ToDictionary((string g) => g, delegate(string g)
			{
				string uniqueColName = XmlDataTableConverter.GetUniqueColName(t, g);
				t.Columns.Add(uniqueColName);
				return uniqueColName;
			});
			foreach (TreeNode<XmlEntry> treeNode5 in list)
			{
				DataRow dataRow = t.NewRow();
				foreach (TreeNode<XmlEntry> treeNode6 in treeNode5.Nodes)
				{
					string text = treeNode6.Value.Name.Trim();
					dataRow[dictionary[text]] = (treeNode6.Value.Value ?? "");
					foreach (TreeNode<XmlEntry> treeNode7 in treeNode6.Nodes)
					{
						string text2 = text + "." + treeNode7.Value.Name.Trim();
						if (dictionary.ContainsKey(text2) && t.Columns.Contains(text2))
						{
							dataRow[dictionary[text2]] = (treeNode7.Value.Value ?? "");
						}
					}
				}
				t.Rows.Add(dataRow);
			}
			return t;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002658 File Offset: 0x00000858
		public static string ConvertForestToString(Forest<XmlEntry> forest)
		{
			StringBuilder stringBuilder = new StringBuilder();
			XmlDataTableConverter.ConvertForestToString(stringBuilder, 0, forest.Nodes);
			return stringBuilder.ToString();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002671 File Offset: 0x00000871
		public static Forest<XmlEntry> ExtractForestFromXml(string xml)
		{
			return XmlDataTableConverter.ExtractForestFromXml(XDocument.Parse(xml));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002680 File Offset: 0x00000880
		public static Forest<XmlEntry> ExtractForestFromXml(XDocument doc)
		{
			Forest<XmlEntry> forest = new Forest<XmlEntry>();
			if (doc.Root.Elements().ToList<XElement>().Count < 1)
			{
				new List<XElement>().Add(new XElement(doc.Root.Name, doc.Root.Value));
			}
			XmlDataTableConverter.ExtractForestFromXml(forest, null, doc.Root.Elements().ToList<XElement>());
			return forest;
		}
	}
}
