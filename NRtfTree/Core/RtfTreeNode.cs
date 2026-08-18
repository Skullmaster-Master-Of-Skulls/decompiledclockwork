using System;
using System.Text;

namespace Net.Sgoliver.NRtfTree.Core
{
	// Token: 0x02000003 RID: 3
	public class RtfTreeNode
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public RtfTreeNode()
		{
			this.type = RtfNodeType.None;
			this.key = "";
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000206A File Offset: 0x0000026A
		public RtfTreeNode(RtfNodeType nodeType)
		{
			this.type = nodeType;
			this.key = "";
			if (nodeType == RtfNodeType.Group || nodeType == RtfNodeType.Root)
			{
				this.children = new RtfNodeCollection();
			}
			if (nodeType == RtfNodeType.Root)
			{
				this.root = this;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020A0 File Offset: 0x000002A0
		public RtfTreeNode(RtfNodeType type, string key, bool hasParameter, int parameter)
		{
			this.type = type;
			this.key = key;
			this.hasParam = hasParameter;
			this.param = parameter;
			if (type == RtfNodeType.Group || type == RtfNodeType.Root)
			{
				this.children = new RtfNodeCollection();
			}
			if (type == RtfNodeType.Root)
			{
				this.root = this;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020EC File Offset: 0x000002EC
		internal RtfTreeNode(RtfToken token)
		{
			this.type = (RtfNodeType)token.Type;
			this.key = token.Key;
			this.hasParam = token.HasParameter;
			this.param = token.Parameter;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002124 File Offset: 0x00000324
		public void AppendChild(RtfTreeNode newNode)
		{
			if (newNode != null)
			{
				if (this.children == null)
				{
					this.children = new RtfNodeCollection();
				}
				newNode.parent = this;
				this.updateNodeRoot(newNode);
				this.children.Add(newNode);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002158 File Offset: 0x00000358
		public void InsertChild(int index, RtfTreeNode newNode)
		{
			if (newNode != null)
			{
				if (this.children == null)
				{
					this.children = new RtfNodeCollection();
				}
				if (index >= 0 && index <= this.children.Count)
				{
					newNode.parent = this;
					this.updateNodeRoot(newNode);
					this.children.Insert(index, newNode);
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021A8 File Offset: 0x000003A8
		public void RemoveChild(int index)
		{
			if (this.children != null && index >= 0 && index < this.children.Count)
			{
				this.children.RemoveAt(index);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021D0 File Offset: 0x000003D0
		public void RemoveChild(RtfTreeNode node)
		{
			if (this.children != null)
			{
				int num = this.children.IndexOf(node);
				if (num != -1)
				{
					this.children.RemoveAt(num);
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002204 File Offset: 0x00000404
		public RtfTreeNode CloneNode(bool cloneChildren)
		{
			RtfTreeNode rtfTreeNode = new RtfTreeNode();
			rtfTreeNode.key = this.key;
			rtfTreeNode.hasParam = this.hasParam;
			rtfTreeNode.param = this.param;
			rtfTreeNode.parent = this.parent;
			rtfTreeNode.root = this.root;
			rtfTreeNode.tree = this.tree;
			rtfTreeNode.type = this.type;
			if (!cloneChildren)
			{
				rtfTreeNode.children = this.children;
			}
			else
			{
				rtfTreeNode.children = null;
				if (this.children != null)
				{
					rtfTreeNode.children = new RtfNodeCollection();
					foreach (object obj in this.children)
					{
						RtfTreeNode rtfTreeNode2 = (RtfTreeNode)obj;
						rtfTreeNode.children.Add(rtfTreeNode2.CloneNode(true));
					}
				}
			}
			return rtfTreeNode;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022F0 File Offset: 0x000004F0
		public bool HasChildNodes()
		{
			bool result = false;
			if (this.children != null && this.children.Count > 0)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002318 File Offset: 0x00000518
		public RtfTreeNode SelectSingleChildNode(string keyword)
		{
			int num = 0;
			bool flag = false;
			RtfTreeNode result = null;
			if (this.children != null)
			{
				while (num < this.children.Count && !flag)
				{
					if (this.children[num].key == keyword)
					{
						result = this.children[num];
						flag = true;
					}
					num++;
				}
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002374 File Offset: 0x00000574
		public RtfTreeNode SelectSingleChildNode(RtfNodeType nodeType)
		{
			int num = 0;
			bool flag = false;
			RtfTreeNode result = null;
			if (this.children != null)
			{
				while (num < this.children.Count && !flag)
				{
					if (this.children[num].type == nodeType)
					{
						result = this.children[num];
						flag = true;
					}
					num++;
				}
			}
			return result;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023CC File Offset: 0x000005CC
		public RtfTreeNode SelectSingleChildNode(string keyword, int param)
		{
			int num = 0;
			bool flag = false;
			RtfTreeNode result = null;
			if (this.children != null)
			{
				while (num < this.children.Count && !flag)
				{
					if (this.children[num].key == keyword && this.children[num].param == param)
					{
						result = this.children[num];
						flag = true;
					}
					num++;
				}
			}
			return result;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000243C File Offset: 0x0000063C
		public RtfTreeNode SelectSingleChildGroup(string keyword)
		{
			int num = 0;
			bool flag = false;
			RtfTreeNode result = null;
			if (this.children != null)
			{
				while (num < this.children.Count && !flag)
				{
					if (this.children[num].NodeType == RtfNodeType.Group && this.children[num].HasChildNodes() && this.children[num].FirstChild.NodeKey == keyword)
					{
						result = this.children[num];
						flag = true;
					}
					num++;
				}
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000024C4 File Offset: 0x000006C4
		public RtfTreeNode SelectSingleNode(RtfNodeType nodeType)
		{
			int num = 0;
			bool flag = false;
			RtfTreeNode rtfTreeNode = null;
			if (this.children != null)
			{
				while (num < this.children.Count && !flag)
				{
					if (this.children[num].type == nodeType)
					{
						rtfTreeNode = this.children[num];
						flag = true;
					}
					else
					{
						rtfTreeNode = this.children[num].SelectSingleNode(nodeType);
						if (rtfTreeNode != null)
						{
							flag = true;
						}
					}
					num++;
				}
			}
			return rtfTreeNode;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002534 File Offset: 0x00000734
		public RtfTreeNode SelectSingleNode(string keyword)
		{
			int num = 0;
			bool flag = false;
			RtfTreeNode rtfTreeNode = null;
			if (this.children != null)
			{
				while (num < this.children.Count && !flag)
				{
					if (this.children[num].key == keyword)
					{
						rtfTreeNode = this.children[num];
						flag = true;
					}
					else
					{
						rtfTreeNode = this.children[num].SelectSingleNode(keyword);
						if (rtfTreeNode != null)
						{
							flag = true;
						}
					}
					num++;
				}
			}
			return rtfTreeNode;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000025AC File Offset: 0x000007AC
		public RtfTreeNode SelectSingleGroup(string keyword)
		{
			int num = 0;
			bool flag = false;
			RtfTreeNode rtfTreeNode = null;
			if (this.children != null)
			{
				while (num < this.children.Count && !flag)
				{
					if (this.children[num].NodeType == RtfNodeType.Group && this.children[num].HasChildNodes() && this.children[num].FirstChild.NodeKey == keyword)
					{
						rtfTreeNode = this.children[num];
						flag = true;
					}
					else
					{
						rtfTreeNode = this.children[num].SelectSingleGroup(keyword);
						if (rtfTreeNode != null)
						{
							flag = true;
						}
					}
					num++;
				}
			}
			return rtfTreeNode;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002654 File Offset: 0x00000854
		public RtfTreeNode SelectSingleNode(string keyword, int param)
		{
			int num = 0;
			bool flag = false;
			RtfTreeNode rtfTreeNode = null;
			if (this.children != null)
			{
				while (num < this.children.Count && !flag)
				{
					if (this.children[num].key == keyword && this.children[num].param == param)
					{
						rtfTreeNode = this.children[num];
						flag = true;
					}
					else
					{
						rtfTreeNode = this.children[num].SelectSingleNode(keyword, param);
						if (rtfTreeNode != null)
						{
							flag = true;
						}
					}
					num++;
				}
			}
			return rtfTreeNode;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000026E0 File Offset: 0x000008E0
		public RtfNodeCollection SelectNodes(string keyword)
		{
			RtfNodeCollection rtfNodeCollection = new RtfNodeCollection();
			if (this.children != null)
			{
				foreach (object obj in this.children)
				{
					RtfTreeNode rtfTreeNode = (RtfTreeNode)obj;
					if (rtfTreeNode.key == keyword)
					{
						rtfNodeCollection.Add(rtfTreeNode);
					}
					rtfNodeCollection.AddRange(rtfTreeNode.SelectNodes(keyword));
				}
			}
			return rtfNodeCollection;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002764 File Offset: 0x00000964
		public RtfNodeCollection SelectGroups(string keyword)
		{
			RtfNodeCollection rtfNodeCollection = new RtfNodeCollection();
			if (this.children != null)
			{
				foreach (object obj in this.children)
				{
					RtfTreeNode rtfTreeNode = (RtfTreeNode)obj;
					if (rtfTreeNode.NodeType == RtfNodeType.Group && rtfTreeNode.HasChildNodes() && rtfTreeNode.FirstChild.NodeKey == keyword)
					{
						rtfNodeCollection.Add(rtfTreeNode);
					}
					rtfNodeCollection.AddRange(rtfTreeNode.SelectGroups(keyword));
				}
			}
			return rtfNodeCollection;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002800 File Offset: 0x00000A00
		public RtfNodeCollection SelectNodes(RtfNodeType nodeType)
		{
			RtfNodeCollection rtfNodeCollection = new RtfNodeCollection();
			if (this.children != null)
			{
				foreach (object obj in this.children)
				{
					RtfTreeNode rtfTreeNode = (RtfTreeNode)obj;
					if (rtfTreeNode.type == nodeType)
					{
						rtfNodeCollection.Add(rtfTreeNode);
					}
					rtfNodeCollection.AddRange(rtfTreeNode.SelectNodes(nodeType));
				}
			}
			return rtfNodeCollection;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002880 File Offset: 0x00000A80
		public RtfNodeCollection SelectNodes(string keyword, int param)
		{
			RtfNodeCollection rtfNodeCollection = new RtfNodeCollection();
			if (this.children != null)
			{
				foreach (object obj in this.children)
				{
					RtfTreeNode rtfTreeNode = (RtfTreeNode)obj;
					if (rtfTreeNode.key == keyword && rtfTreeNode.param == param)
					{
						rtfNodeCollection.Add(rtfTreeNode);
					}
					rtfNodeCollection.AddRange(rtfTreeNode.SelectNodes(keyword, param));
				}
			}
			return rtfNodeCollection;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002910 File Offset: 0x00000B10
		public RtfNodeCollection SelectChildNodes(string keyword)
		{
			RtfNodeCollection rtfNodeCollection = new RtfNodeCollection();
			if (this.children != null)
			{
				foreach (object obj in this.children)
				{
					RtfTreeNode rtfTreeNode = (RtfTreeNode)obj;
					if (rtfTreeNode.key == keyword)
					{
						rtfNodeCollection.Add(rtfTreeNode);
					}
				}
			}
			return rtfNodeCollection;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002988 File Offset: 0x00000B88
		public RtfNodeCollection SelectChildGroups(string keyword)
		{
			RtfNodeCollection rtfNodeCollection = new RtfNodeCollection();
			if (this.children != null)
			{
				foreach (object obj in this.children)
				{
					RtfTreeNode rtfTreeNode = (RtfTreeNode)obj;
					if (rtfTreeNode.NodeType == RtfNodeType.Group && rtfTreeNode.HasChildNodes() && rtfTreeNode.FirstChild.NodeKey == keyword)
					{
						rtfNodeCollection.Add(rtfTreeNode);
					}
				}
			}
			return rtfNodeCollection;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002A18 File Offset: 0x00000C18
		public RtfNodeCollection SelectChildNodes(RtfNodeType nodeType)
		{
			RtfNodeCollection rtfNodeCollection = new RtfNodeCollection();
			if (this.children != null)
			{
				foreach (object obj in this.children)
				{
					RtfTreeNode rtfTreeNode = (RtfTreeNode)obj;
					if (rtfTreeNode.type == nodeType)
					{
						rtfNodeCollection.Add(rtfTreeNode);
					}
				}
			}
			return rtfNodeCollection;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002A8C File Offset: 0x00000C8C
		public RtfNodeCollection SelectChildNodes(string keyword, int param)
		{
			RtfNodeCollection rtfNodeCollection = new RtfNodeCollection();
			if (this.children != null)
			{
				foreach (object obj in this.children)
				{
					RtfTreeNode rtfTreeNode = (RtfTreeNode)obj;
					if (rtfTreeNode.key == keyword && rtfTreeNode.param == param)
					{
						rtfNodeCollection.Add(rtfTreeNode);
					}
				}
			}
			return rtfNodeCollection;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002B0C File Offset: 0x00000D0C
		public RtfTreeNode SelectSibling(string keyword)
		{
			RtfTreeNode result = null;
			RtfTreeNode rtfTreeNode = this.parent;
			if (rtfTreeNode != null)
			{
				int num = rtfTreeNode.ChildNodes.IndexOf(this);
				int num2 = num + 1;
				bool flag = false;
				while (num2 < rtfTreeNode.children.Count && !flag)
				{
					if (rtfTreeNode.children[num2].key == keyword)
					{
						result = rtfTreeNode.children[num2];
						flag = true;
					}
					num2++;
				}
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002B7C File Offset: 0x00000D7C
		public RtfTreeNode SelectSibling(RtfNodeType nodeType)
		{
			RtfTreeNode result = null;
			RtfTreeNode rtfTreeNode = this.parent;
			if (rtfTreeNode != null)
			{
				int num = rtfTreeNode.ChildNodes.IndexOf(this);
				int num2 = num + 1;
				bool flag = false;
				while (num2 < rtfTreeNode.children.Count && !flag)
				{
					if (rtfTreeNode.children[num2].type == nodeType)
					{
						result = rtfTreeNode.children[num2];
						flag = true;
					}
					num2++;
				}
			}
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public RtfTreeNode SelectSibling(string keyword, int param)
		{
			RtfTreeNode result = null;
			RtfTreeNode rtfTreeNode = this.parent;
			if (rtfTreeNode != null)
			{
				int num = rtfTreeNode.ChildNodes.IndexOf(this);
				int num2 = num + 1;
				bool flag = false;
				while (num2 < rtfTreeNode.children.Count && !flag)
				{
					if (rtfTreeNode.children[num2].key == keyword && rtfTreeNode.children[num2].param == param)
					{
						result = rtfTreeNode.children[num2];
						flag = true;
					}
					num2++;
				}
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002C6C File Offset: 0x00000E6C
		public RtfNodeCollection FindText(string text)
		{
			RtfNodeCollection rtfNodeCollection = new RtfNodeCollection();
			if (this.children != null)
			{
				foreach (object obj in this.children)
				{
					RtfTreeNode rtfTreeNode = (RtfTreeNode)obj;
					if (rtfTreeNode.NodeType == RtfNodeType.Text && rtfTreeNode.NodeKey.IndexOf(text) != -1)
					{
						rtfNodeCollection.Add(rtfTreeNode);
					}
					else if (rtfTreeNode.NodeType == RtfNodeType.Group)
					{
						rtfNodeCollection.AddRange(rtfTreeNode.FindText(text));
					}
				}
			}
			return rtfNodeCollection;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002D08 File Offset: 0x00000F08
		public void ReplaceText(string oldValue, string newValue)
		{
			if (this.children != null)
			{
				foreach (object obj in this.children)
				{
					RtfTreeNode rtfTreeNode = (RtfTreeNode)obj;
					if (rtfTreeNode.NodeType == RtfNodeType.Text)
					{
						rtfTreeNode.NodeKey = rtfTreeNode.NodeKey.Replace(oldValue, newValue);
					}
					else if (rtfTreeNode.NodeType == RtfNodeType.Group)
					{
						rtfTreeNode.ReplaceText(oldValue, newValue);
					}
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002D94 File Offset: 0x00000F94
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"[",
				this.type,
				", ",
				this.key,
				", ",
				this.hasParam,
				", ",
				this.param,
				"]"
			});
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002E0C File Offset: 0x0000100C
		private string getRtf()
		{
			Encoding encoding = this.tree.GetEncoding();
			return this.getRtfInm(this, null, encoding);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002E38 File Offset: 0x00001038
		private string getRtfInm(RtfTreeNode curNode, RtfTreeNode prevNode, Encoding enc)
		{
			StringBuilder stringBuilder = new StringBuilder("");
			if (curNode.NodeType == RtfNodeType.Root)
			{
				stringBuilder.Append("");
			}
			else if (curNode.NodeType == RtfNodeType.Group)
			{
				stringBuilder.Append("{");
			}
			else
			{
				if (curNode.NodeType == RtfNodeType.Control || curNode.NodeType == RtfNodeType.Keyword)
				{
					stringBuilder.Append("\\");
				}
				else if (prevNode != null && prevNode.NodeType == RtfNodeType.Keyword)
				{
					int num = char.ConvertToUtf32(curNode.NodeKey, 0);
					if (num >= 32 && num < 128)
					{
						stringBuilder.Append(" ");
					}
				}
				this.AppendEncoded(stringBuilder, curNode.NodeKey, enc);
				if (curNode.HasParameter)
				{
					if (curNode.NodeType == RtfNodeType.Keyword)
					{
						stringBuilder.Append(Convert.ToString(curNode.Parameter));
					}
					else if (curNode.NodeType == RtfNodeType.Control && curNode.NodeKey == "'")
					{
						stringBuilder.Append(this.GetHexa(curNode.Parameter));
					}
				}
			}
			RtfNodeCollection childNodes = curNode.ChildNodes;
			if (childNodes != null)
			{
				for (int i = 0; i < childNodes.Count; i++)
				{
					RtfTreeNode curNode2 = childNodes[i];
					if (i > 0)
					{
						stringBuilder.Append(this.getRtfInm(curNode2, childNodes[i - 1], enc));
					}
					else
					{
						stringBuilder.Append(this.getRtfInm(curNode2, null, enc));
					}
				}
			}
			if (curNode.NodeType == RtfNodeType.Group)
			{
				stringBuilder.Append("}");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002FA8 File Offset: 0x000011A8
		private void AppendEncoded(StringBuilder res, string s, Encoding enc)
		{
			for (int i = 0; i < s.Length; i++)
			{
				int num = char.ConvertToUtf32(s, i);
				if (num >= 128 || num < 32)
				{
					res.Append("\\'");
					byte[] bytes = enc.GetBytes(new char[]
					{
						s[i]
					});
					res.Append(this.GetHexa((int)bytes[0]));
				}
				else
				{
					if (s[i] == '{' || s[i] == '}' || s[i] == '\\')
					{
						res.Append("\\");
					}
					res.Append(s[i]);
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003054 File Offset: 0x00001254
		private string GetHexa(int code)
		{
			string text = Convert.ToString(code, 16);
			if (text.Length == 1)
			{
				text = "0" + text;
			}
			return text;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003080 File Offset: 0x00001280
		private void updateNodeRoot(RtfTreeNode node)
		{
			node.root = this.root;
			node.tree = this.tree;
			if (node.children != null)
			{
				foreach (object obj in node.children)
				{
					RtfTreeNode node2 = (RtfTreeNode)obj;
					this.updateNodeRoot(node2);
				}
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000030FC File Offset: 0x000012FC
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00003104 File Offset: 0x00001304
		public RtfTreeNode RootNode
		{
			get
			{
				return this.root;
			}
			set
			{
				this.root = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000310D File Offset: 0x0000130D
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00003115 File Offset: 0x00001315
		public RtfTreeNode ParentNode
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000311E File Offset: 0x0000131E
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00003126 File Offset: 0x00001326
		public RtfTree Tree
		{
			get
			{
				return this.tree;
			}
			set
			{
				this.tree = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000312F File Offset: 0x0000132F
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00003137 File Offset: 0x00001337
		public RtfNodeType NodeType
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00003140 File Offset: 0x00001340
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00003148 File Offset: 0x00001348
		public string NodeKey
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00003151 File Offset: 0x00001351
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00003159 File Offset: 0x00001359
		public bool HasParameter
		{
			get
			{
				return this.hasParam;
			}
			set
			{
				this.hasParam = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00003162 File Offset: 0x00001362
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000316A File Offset: 0x0000136A
		public int Parameter
		{
			get
			{
				return this.param;
			}
			set
			{
				this.param = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003173 File Offset: 0x00001373
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000317C File Offset: 0x0000137C
		public RtfNodeCollection ChildNodes
		{
			get
			{
				return this.children;
			}
			set
			{
				this.children = value;
				foreach (object obj in this.children)
				{
					RtfTreeNode rtfTreeNode = (RtfTreeNode)obj;
					rtfTreeNode.parent = this;
					this.updateNodeRoot(rtfTreeNode);
				}
			}
		}

		// Token: 0x17000009 RID: 9
		public RtfTreeNode this[string keyword]
		{
			get
			{
				return this.SelectSingleChildNode(keyword);
			}
		}

		// Token: 0x1700000A RID: 10
		public RtfTreeNode this[int childIndex]
		{
			get
			{
				RtfTreeNode result = null;
				if (this.children != null && childIndex >= 0 && childIndex < this.children.Count)
				{
					result = this.children[childIndex];
				}
				return result;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003228 File Offset: 0x00001428
		public RtfTreeNode FirstChild
		{
			get
			{
				RtfTreeNode result = null;
				if (this.children != null && this.children.Count > 0)
				{
					result = this.children[0];
				}
				return result;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000325C File Offset: 0x0000145C
		public RtfTreeNode LastChild
		{
			get
			{
				RtfTreeNode result = null;
				if (this.children != null && this.children.Count > 0)
				{
					return this.children[this.children.Count - 1];
				}
				return result;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000329C File Offset: 0x0000149C
		public RtfTreeNode NextSibling
		{
			get
			{
				RtfTreeNode result = null;
				if (this.parent != null && this.parent.children != null)
				{
					int num = this.parent.children.IndexOf(this);
					if (this.parent.children.Count > num + 1)
					{
						result = this.parent.children[num + 1];
					}
				}
				return result;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000032FC File Offset: 0x000014FC
		public RtfTreeNode PreviousSibling
		{
			get
			{
				RtfTreeNode result = null;
				if (this.parent != null && this.parent.children != null)
				{
					int num = this.parent.children.IndexOf(this);
					if (num > 0)
					{
						result = this.parent.children[num - 1];
					}
				}
				return result;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000334B File Offset: 0x0000154B
		public string Rtf
		{
			get
			{
				return this.getRtf();
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003354 File Offset: 0x00001554
		public int Index
		{
			get
			{
				int result = -1;
				if (this.parent != null)
				{
					result = this.parent.children.IndexOf(this);
				}
				return result;
			}
		}

		// Token: 0x04000006 RID: 6
		private RtfNodeType type;

		// Token: 0x04000007 RID: 7
		private string key;

		// Token: 0x04000008 RID: 8
		private bool hasParam;

		// Token: 0x04000009 RID: 9
		private int param;

		// Token: 0x0400000A RID: 10
		private RtfNodeCollection children;

		// Token: 0x0400000B RID: 11
		private RtfTreeNode parent;

		// Token: 0x0400000C RID: 12
		private RtfTreeNode root;

		// Token: 0x0400000D RID: 13
		private RtfTree tree;
	}
}
