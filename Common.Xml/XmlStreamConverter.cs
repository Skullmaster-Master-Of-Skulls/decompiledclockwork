using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using TechnoPro.Common.DataStructure.Tree;

namespace TechnoPro.Common.Xml
{
	// Token: 0x02000003 RID: 3
	public static class XmlStreamConverter
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000026E8 File Offset: 0x000008E8
		public static void ConvertXmlData<T>(StreamReader streamReader, Func<T, bool> action)
		{
			string name = typeof(T).Name;
			XmlReaderSettings settings = new XmlReaderSettings
			{
				ConformanceLevel = ConformanceLevel.Document
			};
			using (XmlReader xmlReader = XmlReader.Create(streamReader, settings))
			{
				for (;;)
				{
					Forest<XmlStreamConverter.XmlLine> xmlLinesForNextObject = XmlStreamConverter.GetXmlLinesForNextObject(xmlReader, name);
					if (xmlLinesForNextObject == null)
					{
						break;
					}
					if (xmlLinesForNextObject.Nodes.Count < 1)
					{
						break;
					}
					T arg = XmlStreamConverter.ParseXmlData<T>(xmlLinesForNextObject);
					action(arg);
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002764 File Offset: 0x00000964
		public static void ConvertXmlDataFromXmlString<T>(string xml, Func<T, bool> action)
		{
			string name = typeof(T).Name;
			using (StringReader stringReader = new StringReader(xml))
			{
				XmlReaderSettings settings = new XmlReaderSettings
				{
					ConformanceLevel = ConformanceLevel.Document
				};
				using (XmlReader xmlReader = XmlReader.Create(stringReader, settings))
				{
					for (;;)
					{
						Forest<XmlStreamConverter.XmlLine> xmlLinesForNextObject = XmlStreamConverter.GetXmlLinesForNextObject(xmlReader, name);
						if (xmlLinesForNextObject == null)
						{
							break;
						}
						if (xmlLinesForNextObject.Nodes.Count < 1)
						{
							break;
						}
						T arg = XmlStreamConverter.ParseXmlData<T>(xmlLinesForNextObject);
						action(arg);
					}
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002804 File Offset: 0x00000A04
		public static void ConvertXmlData<T>(string fileName, Func<T, bool> action)
		{
			string name = typeof(T).Name;
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				XmlReaderSettings settings = new XmlReaderSettings
				{
					ConformanceLevel = ConformanceLevel.Document
				};
				using (XmlReader xmlReader = XmlReader.Create(fileStream, settings))
				{
					for (;;)
					{
						Forest<XmlStreamConverter.XmlLine> xmlLinesForNextObject = XmlStreamConverter.GetXmlLinesForNextObject(xmlReader, name);
						if (xmlLinesForNextObject == null)
						{
							break;
						}
						if (xmlLinesForNextObject.Nodes.Count < 1)
						{
							break;
						}
						T arg = XmlStreamConverter.ParseXmlData<T>(xmlLinesForNextObject);
						action(arg);
					}
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000028A4 File Offset: 0x00000AA4
		private static T ParseXmlData<T>(Forest<XmlStreamConverter.XmlLine> forestWithSingleObject)
		{
			T t = Activator.CreateInstance<T>();
			XmlStreamConverter.SetItemProperties(t, typeof(T), forestWithSingleObject.Nodes);
			return t;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000028C8 File Offset: 0x00000AC8
		private static void SetItemProperties(object item, Type type, TreeNodeCollection<XmlStreamConverter.XmlLine> parentNodes)
		{
			PropertyInfo[] properties = type.GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				string propName = propertyInfo.Name;
				Type propertyType = propertyInfo.PropertyType;
				TreeNode<XmlStreamConverter.XmlLine> treeNode = parentNodes.FirstOrDefault((TreeNode<XmlStreamConverter.XmlLine> g) => g.Value.Name == propName);
				if (propertyType == typeof(string))
				{
					if (treeNode != null)
					{
						string value = treeNode.Value.Value;
						propertyInfo.SetValue(item, value, null);
					}
				}
				else
				{
					if (propertyType.IsGenericType)
					{
						TreeNodeCollection<XmlStreamConverter.XmlLine> treeNodeCollection = (treeNode != null) ? treeNode.Nodes : parentNodes;
						Type type2 = propertyType.GetGenericArguments()[0];
						IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[]
						{
							type2
						}));
						propertyInfo.SetValue(item, list, null);
						object obj = Activator.CreateInstance(type2);
						using (IEnumerator<TreeNode<XmlStreamConverter.XmlLine>> enumerator = treeNodeCollection.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								TreeNode<XmlStreamConverter.XmlLine> treeNode2 = enumerator.Current;
								XmlStreamConverter.SetItemProperties(obj, type2, treeNode2.Nodes);
								list.Add(obj);
								obj = Activator.CreateInstance(type2);
							}
							goto IL_141;
						}
					}
					XmlStreamConverter.SetItemProperties(Activator.CreateInstance(propertyType), propertyType, (treeNode == null) ? parentNodes : treeNode.Nodes);
				}
				IL_141:;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002A34 File Offset: 0x00000C34
		private static Forest<XmlStreamConverter.XmlLine> GetXmlLinesForNextObject(XmlReader reader, string parentObjectName)
		{
			bool flag = false;
			Forest<XmlStreamConverter.XmlLine> forest = new Forest<XmlStreamConverter.XmlLine>();
			Dictionary<int, TreeNode<XmlStreamConverter.XmlLine>> parentNodes = new Dictionary<int, TreeNode<XmlStreamConverter.XmlLine>>();
			if (reader.Read())
			{
				for (;;)
				{
					string name = reader.Name;
					if (name == parentObjectName)
					{
						if (!reader.IsStartElement())
						{
							break;
						}
						flag = true;
						if (!reader.Read())
						{
							break;
						}
					}
					else
					{
						if (flag && reader.NodeType.Equals(XmlNodeType.Element) && !reader.IsEmptyElement)
						{
							int depth = reader.Depth;
							Dictionary<string, string> dictionary = new Dictionary<string, string>();
							if (reader.HasAttributes)
							{
								int num = 0;
								while (reader.MoveToNextAttribute())
								{
									string name2 = reader.Name;
									string value = reader[num++];
									if (!dictionary.ContainsKey(name2))
									{
										dictionary.Add(name2, value);
									}
								}
							}
							string readerValue = reader.ReadString().Trim();
							XmlStreamConverter.AddItemToForest(forest, parentNodes, depth, name, readerValue);
							using (Dictionary<string, string>.Enumerator enumerator = dictionary.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									KeyValuePair<string, string> keyValuePair = enumerator.Current;
									XmlStreamConverter.AddItemToForest(forest, parentNodes, depth + 1, keyValuePair.Key, keyValuePair.Value ?? "");
								}
								continue;
							}
						}
						if (!reader.Read())
						{
							break;
						}
					}
				}
			}
			return forest;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002B90 File Offset: 0x00000D90
		private static void AddItemToForest(Forest<XmlStreamConverter.XmlLine> forest, Dictionary<int, TreeNode<XmlStreamConverter.XmlLine>> parentNodes, int thisItemsDepth, string readerName, string readerValue)
		{
			TreeNode<XmlStreamConverter.XmlLine> parentNode = parentNodes.ContainsKey(thisItemsDepth) ? parentNodes[thisItemsDepth] : null;
			TreeNode<XmlStreamConverter.XmlLine> value = forest.AppendNode(parentNode, new XmlStreamConverter.XmlLine(readerName, readerValue ?? ""));
			int key = thisItemsDepth + 1;
			if (parentNodes.ContainsKey(key))
			{
				parentNodes.Remove(key);
			}
			parentNodes.Add(key, value);
		}

		// Token: 0x02000009 RID: 9
		internal class XmlLine
		{
			// Token: 0x06000021 RID: 33 RVA: 0x00002C33 File Offset: 0x00000E33
			public XmlLine()
			{
			}

			// Token: 0x06000022 RID: 34 RVA: 0x00002CCC File Offset: 0x00000ECC
			public XmlLine(string name, string val)
			{
				this.Name = name;
				this.Value = val;
			}

			// Token: 0x06000023 RID: 35 RVA: 0x00002CE2 File Offset: 0x00000EE2
			public override string ToString()
			{
				return (this.Name ?? "NULL") + "=" + (this.Value ?? "NULL");
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000024 RID: 36 RVA: 0x00002D0C File Offset: 0x00000F0C
			// (set) Token: 0x06000025 RID: 37 RVA: 0x00002D14 File Offset: 0x00000F14
			public string Name { get; set; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000026 RID: 38 RVA: 0x00002D1D File Offset: 0x00000F1D
			// (set) Token: 0x06000027 RID: 39 RVA: 0x00002D25 File Offset: 0x00000F25
			public string Value { get; set; }
		}
	}
}
