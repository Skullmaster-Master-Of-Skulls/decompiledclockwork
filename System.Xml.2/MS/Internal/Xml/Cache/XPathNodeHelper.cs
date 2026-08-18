using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000057 RID: 87
	internal abstract class XPathNodeHelper
	{
		// Token: 0x06000314 RID: 788 RVA: 0x0000C394 File Offset: 0x0000A594
		public static int GetLocalNamespaces(XPathNode[] pageElem, int idxElem, out XPathNode[] pageNmsp)
		{
			if (pageElem[idxElem].HasNamespaceDecls)
			{
				return pageElem[idxElem].Document.LookupNamespaces(pageElem, idxElem, out pageNmsp);
			}
			pageNmsp = null;
			return 0;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000C3C0 File Offset: 0x0000A5C0
		public static int GetInScopeNamespaces(XPathNode[] pageElem, int idxElem, out XPathNode[] pageNmsp)
		{
			if (pageElem[idxElem].NodeType == XPathNodeType.Element)
			{
				XPathDocument document = pageElem[idxElem].Document;
				while (!pageElem[idxElem].HasNamespaceDecls)
				{
					idxElem = pageElem[idxElem].GetParent(out pageElem);
					if (idxElem == 0)
					{
						return document.GetXmlNamespaceNode(out pageNmsp);
					}
				}
				return document.LookupNamespaces(pageElem, idxElem, out pageNmsp);
			}
			pageNmsp = null;
			return 0;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000C422 File Offset: 0x0000A622
		public static bool GetFirstAttribute(ref XPathNode[] pageNode, ref int idxNode)
		{
			if (pageNode[idxNode].HasAttribute)
			{
				XPathNodeHelper.GetChild(ref pageNode, ref idxNode);
				return true;
			}
			return false;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000C440 File Offset: 0x0000A640
		public static bool GetNextAttribute(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array;
			int sibling = pageNode[idxNode].GetSibling(out array);
			if (sibling != 0 && array[sibling].NodeType == XPathNodeType.Attribute)
			{
				pageNode = array;
				idxNode = sibling;
				return true;
			}
			return false;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000C47C File Offset: 0x0000A67C
		public static bool GetContentChild(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].HasContentChild)
			{
				XPathNodeHelper.GetChild(ref array, ref num);
				while (array[num].NodeType == XPathNodeType.Attribute)
				{
					num = array[num].GetSibling(out array);
				}
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
		public static bool GetContentSibling(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (!array[num].IsAttrNmsp)
			{
				num = array[num].GetSibling(out array);
				if (num != 0)
				{
					pageNode = array;
					idxNode = num;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000C50C File Offset: 0x0000A70C
		public static bool GetParent(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			num = array[num].GetParent(out array);
			if (num != 0)
			{
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000C53A File Offset: 0x0000A73A
		public static int GetLocation(XPathNode[] pageNode, int idxNode)
		{
			return pageNode[0].PageInfo.PageNumber << 16 | idxNode;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000C554 File Offset: 0x0000A754
		public static bool GetElementChild(ref XPathNode[] pageNode, ref int idxNode, string localName, string namespaceName)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].HasElementChild)
			{
				XPathNodeHelper.GetChild(ref array, ref num);
				while (!array[num].ElementMatch(localName, namespaceName))
				{
					num = array[num].GetSibling(out array);
					if (num == 0)
					{
						return false;
					}
				}
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000C5AC File Offset: 0x0000A7AC
		public static bool GetElementSibling(ref XPathNode[] pageNode, ref int idxNode, string localName, string namespaceName)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].NodeType != XPathNodeType.Attribute)
			{
				do
				{
					num = array[num].GetSibling(out array);
					if (num == 0)
					{
						return false;
					}
				}
				while (!array[num].ElementMatch(localName, namespaceName));
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000C5FC File Offset: 0x0000A7FC
		public static bool GetContentChild(ref XPathNode[] pageNode, ref int idxNode, XPathNodeType typ)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].HasContentChild)
			{
				int contentKindMask = XPathNavigator.GetContentKindMask(typ);
				XPathNodeHelper.GetChild(ref array, ref num);
				while ((1 << (int)array[num].NodeType & contentKindMask) == 0)
				{
					num = array[num].GetSibling(out array);
					if (num == 0)
					{
						return false;
					}
				}
				if (typ == XPathNodeType.Attribute)
				{
					return false;
				}
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000C664 File Offset: 0x0000A864
		public static bool GetContentSibling(ref XPathNode[] pageNode, ref int idxNode, XPathNodeType typ)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			int contentKindMask = XPathNavigator.GetContentKindMask(typ);
			if (array[num].NodeType != XPathNodeType.Attribute)
			{
				do
				{
					num = array[num].GetSibling(out array);
					if (num == 0)
					{
						return false;
					}
				}
				while ((1 << (int)array[num].NodeType & contentKindMask) == 0);
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000C6C0 File Offset: 0x0000A8C0
		public static bool GetPreviousContentSibling(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			num = array[num].GetParent(out array);
			if (num != 0)
			{
				int num2 = idxNode - 1;
				XPathNode[] array2;
				if (num2 == 0)
				{
					array2 = pageNode[0].PageInfo.PreviousPage;
					num2 = array2.Length - 1;
				}
				else
				{
					array2 = pageNode;
				}
				if (num == num2 && array == array2)
				{
					return false;
				}
				XPathNode[] array3 = array2;
				int num3 = num2;
				do
				{
					array2 = array3;
					num2 = num3;
					num3 = array3[num3].GetParent(out array3);
				}
				while (num3 != num || array3 != array);
				if (array2[num2].NodeType != XPathNodeType.Attribute)
				{
					pageNode = array2;
					idxNode = num2;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000C75C File Offset: 0x0000A95C
		public static bool GetPreviousElementSibling(ref XPathNode[] pageNode, ref int idxNode, string localName, string namespaceName)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].NodeType != XPathNodeType.Attribute)
			{
				while (XPathNodeHelper.GetPreviousContentSibling(ref array, ref num))
				{
					if (array[num].ElementMatch(localName, namespaceName))
					{
						pageNode = array;
						idxNode = num;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000C7A4 File Offset: 0x0000A9A4
		public static bool GetPreviousContentSibling(ref XPathNode[] pageNode, ref int idxNode, XPathNodeType typ)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			int contentKindMask = XPathNavigator.GetContentKindMask(typ);
			while (XPathNodeHelper.GetPreviousContentSibling(ref array, ref num))
			{
				if ((1 << (int)array[num].NodeType & contentKindMask) != 0)
				{
					pageNode = array;
					idxNode = num;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		public static bool GetAttribute(ref XPathNode[] pageNode, ref int idxNode, string localName, string namespaceName)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].HasAttribute)
			{
				XPathNodeHelper.GetChild(ref array, ref num);
				while (!array[num].NameMatch(localName, namespaceName))
				{
					num = array[num].GetSibling(out array);
					if (num == 0 || array[num].NodeType != XPathNodeType.Attribute)
					{
						return false;
					}
				}
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000C84C File Offset: 0x0000AA4C
		public static bool GetFollowing(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			while (++num >= array[0].PageInfo.NodeCount)
			{
				array = array[0].PageInfo.NextPage;
				num = 0;
				if (array == null)
				{
					return false;
				}
			}
			pageNode = array;
			idxNode = num;
			return true;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000C898 File Offset: 0x0000AA98
		public static bool GetElementFollowing(ref XPathNode[] pageCurrent, ref int idxCurrent, XPathNode[] pageEnd, int idxEnd, string localName, string namespaceName)
		{
			XPathNode[] array = pageCurrent;
			int i = idxCurrent;
			if (array[i].NodeType != XPathNodeType.Element || array[i].LocalName != localName)
			{
				i++;
				while (array != pageEnd || i > idxEnd)
				{
					while (i < array[0].PageInfo.NodeCount)
					{
						if (array[i].ElementMatch(localName, namespaceName))
						{
							goto IL_12C;
						}
						i++;
					}
					array = array[0].PageInfo.NextPage;
					i = 1;
					if (array == null)
					{
						return false;
					}
				}
				while (i != idxEnd)
				{
					if (array[i].ElementMatch(localName, namespaceName))
					{
						goto IL_12C;
					}
					i++;
				}
				return false;
			}
			int num = 0;
			if (pageEnd != null)
			{
				num = pageEnd[0].PageInfo.PageNumber;
				int pageNumber = array[0].PageInfo.PageNumber;
				if (pageNumber > num || (pageNumber == num && i >= idxEnd))
				{
					pageEnd = null;
				}
			}
			do
			{
				i = array[i].GetSimilarElement(out array);
				if (i == 0)
				{
					return false;
				}
				if (pageEnd != null)
				{
					int pageNumber = array[0].PageInfo.PageNumber;
					if (pageNumber > num || (pageNumber == num && i >= idxEnd))
					{
						return false;
					}
				}
			}
			while (array[i].LocalName != localName || !(array[i].NamespaceUri == namespaceName));
			IL_12C:
			pageCurrent = array;
			idxCurrent = i;
			return true;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000C9D8 File Offset: 0x0000ABD8
		public static bool GetContentFollowing(ref XPathNode[] pageCurrent, ref int idxCurrent, XPathNode[] pageEnd, int idxEnd, XPathNodeType typ)
		{
			XPathNode[] array = pageCurrent;
			int i = idxCurrent;
			int contentKindMask = XPathNavigator.GetContentKindMask(typ);
			i++;
			while (array != pageEnd || i > idxEnd)
			{
				while (i < array[0].PageInfo.NodeCount)
				{
					if ((1 << (int)array[i].NodeType & contentKindMask) != 0)
					{
						goto IL_81;
					}
					i++;
				}
				array = array[0].PageInfo.NextPage;
				i = 1;
				if (array == null)
				{
					return false;
				}
				continue;
				IL_81:
				pageCurrent = array;
				idxCurrent = i;
				return true;
			}
			while (i != idxEnd)
			{
				if ((1 << (int)array[i].NodeType & contentKindMask) != 0)
				{
					goto IL_81;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000CA70 File Offset: 0x0000AC70
		public static bool GetTextFollowing(ref XPathNode[] pageCurrent, ref int idxCurrent, XPathNode[] pageEnd, int idxEnd)
		{
			XPathNode[] array = pageCurrent;
			int i = idxCurrent;
			i++;
			while (array != pageEnd || i > idxEnd)
			{
				while (i < array[0].PageInfo.NodeCount)
				{
					if (array[i].IsText || (array[i].NodeType == XPathNodeType.Element && array[i].HasCollapsedText))
					{
						goto IL_AB;
					}
					i++;
				}
				array = array[0].PageInfo.NextPage;
				i = 1;
				if (array == null)
				{
					return false;
				}
				continue;
				IL_AB:
				pageCurrent = array;
				idxCurrent = i;
				return true;
			}
			while (i != idxEnd)
			{
				if (array[i].IsText || (array[i].NodeType == XPathNodeType.Element && array[i].HasCollapsedText))
				{
					goto IL_AB;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000CB30 File Offset: 0x0000AD30
		public static bool GetNonDescendant(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			while (!array[num].HasSibling)
			{
				num = array[num].GetParent(out array);
				if (num == 0)
				{
					return false;
				}
			}
			pageNode = array;
			idxNode = array[num].GetSibling(out pageNode);
			return true;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000CB78 File Offset: 0x0000AD78
		private static void GetChild(ref XPathNode[] pageNode, ref int idxNode)
		{
			int num = idxNode + 1;
			idxNode = num;
			if (num >= pageNode.Length)
			{
				pageNode = pageNode[0].PageInfo.NextPage;
				idxNode = 1;
			}
		}
	}
}
