using System;
using System.Globalization;
using System.Text;
using Net.Sgoliver.NRtfTree.Core;

namespace Net.Sgoliver.NRtfTree.Util
{
	// Token: 0x02000006 RID: 6
	public class ObjectNode : RtfTreeNode
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00003B5C File Offset: 0x00001D5C
		public ObjectNode(RtfTreeNode node)
		{
			if (node != null)
			{
				base.NodeKey = node.NodeKey;
				base.HasParameter = node.HasParameter;
				base.Parameter = node.Parameter;
				base.ParentNode = node.ParentNode;
				base.RootNode = node.RootNode;
				base.NodeType = node.NodeType;
				base.ChildNodes = new RtfNodeCollection();
				base.ChildNodes.AddRange(node.ChildNodes);
				this.getObjectData();
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003BDC File Offset: 0x00001DDC
		public string ObjectType
		{
			get
			{
				if (base.SelectSingleChildNode("objemb") != null)
				{
					return "objemb";
				}
				if (base.SelectSingleChildNode("objlink") != null)
				{
					return "objlink";
				}
				if (base.SelectSingleChildNode("objautlink") != null)
				{
					return "objautlink";
				}
				if (base.SelectSingleChildNode("objsub") != null)
				{
					return "objsub";
				}
				if (base.SelectSingleChildNode("objpub") != null)
				{
					return "objpub";
				}
				if (base.SelectSingleChildNode("objicemb") != null)
				{
					return "objicemb";
				}
				if (base.SelectSingleChildNode("objhtml") != null)
				{
					return "objhtml";
				}
				if (base.SelectSingleChildNode("objocx") != null)
				{
					return "objocx";
				}
				return "";
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003C88 File Offset: 0x00001E88
		public string ObjectClass
		{
			get
			{
				RtfTreeNode rtfTreeNode = base.SelectSingleNode("objclass");
				if (rtfTreeNode != null)
				{
					return rtfTreeNode.NextSibling.NodeKey;
				}
				return "";
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003CB8 File Offset: 0x00001EB8
		public RtfTreeNode ResultNode
		{
			get
			{
				RtfTreeNode rtfTreeNode = base.SelectSingleNode("result");
				if (rtfTreeNode != null)
				{
					rtfTreeNode = rtfTreeNode.ParentNode;
				}
				return rtfTreeNode;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003CDC File Offset: 0x00001EDC
		public string HexData
		{
			get
			{
				string result = "";
				RtfTreeNode rtfTreeNode = base.SelectSingleNode("objdata");
				if (rtfTreeNode != null)
				{
					result = rtfTreeNode.ParentNode.LastChild.NodeKey;
				}
				return result;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003D10 File Offset: 0x00001F10
		public byte[] GetByteData()
		{
			return this.objdata;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003D18 File Offset: 0x00001F18
		private void getObjectData()
		{
			if (base.FirstChild.NodeKey == "object")
			{
				RtfTreeNode rtfTreeNode = base.SelectSingleNode("objdata");
				if (rtfTreeNode != null)
				{
					string nodeKey = rtfTreeNode.ParentNode.LastChild.NodeKey;
					int num = nodeKey.Length / 2;
					this.objdata = new byte[num];
					StringBuilder stringBuilder = new StringBuilder(2);
					for (int i = 0; i < nodeKey.Length; i++)
					{
						stringBuilder.Append(nodeKey[i]);
						if (stringBuilder.Length == 2)
						{
							this.objdata[i / 2] = byte.Parse(stringBuilder.ToString(), NumberStyles.HexNumber);
							stringBuilder.Remove(0, 2);
						}
					}
				}
			}
		}

		// Token: 0x04000025 RID: 37
		private byte[] objdata;
	}
}
