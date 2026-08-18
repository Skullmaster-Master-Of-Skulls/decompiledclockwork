using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200010C RID: 268
	internal abstract class XPathNodeHelper
	{
		// Token: 0x06001064 RID: 4196 RVA: 0x0004AB94 File Offset: 0x00049B94
		public static int GetLocalNamespaces(XPathNode[] pageElem, int idxElem, out XPathNode[] pageNmsp)
		{
			if (pageElem[idxElem].HasNamespaceDecls)
			{
				return pageElem[idxElem].Document.LookupNamespaces(pageElem, idxElem, out pageNmsp);
			}
			pageNmsp = null;
			return 0;
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0004ABC0 File Offset: 0x00049BC0
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

		// Token: 0x06001066 RID: 4198 RVA: 0x0004AC22 File Offset: 0x00049C22
		public static bool GetFirstAttribute(ref XPathNode[] pageNode, ref int idxNode)
		{
			if (pageNode[idxNode].HasAttribute)
			{
				XPathNodeHelper.GetChild(ref pageNode, ref idxNode);
				return true;
			}
			return false;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0004AC40 File Offset: 0x00049C40
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

		// Token: 0x06001068 RID: 4200 RVA: 0x0004AC7C File Offset: 0x00049C7C
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

		// Token: 0x06001069 RID: 4201 RVA: 0x0004ACD0 File Offset: 0x00049CD0
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

		// Token: 0x0600106A RID: 4202 RVA: 0x0004AD0C File Offset: 0x00049D0C
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

		// Token: 0x0600106B RID: 4203 RVA: 0x0004AD3A File Offset: 0x00049D3A
		public static int GetLocation(XPathNode[] pageNode, int idxNode)
		{
			return pageNode[0].PageInfo.PageNumber << 16 | idxNode;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0004AD54 File Offset: 0x00049D54
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

		// Token: 0x0600106D RID: 4205 RVA: 0x0004ADAC File Offset: 0x00049DAC
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

		// Token: 0x0600106E RID: 4206 RVA: 0x0004ADFC File Offset: 0x00049DFC
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

		// Token: 0x0600106F RID: 4207 RVA: 0x0004AE64 File Offset: 0x00049E64
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

		// Token: 0x06001070 RID: 4208 RVA: 0x0004AEC0 File Offset: 0x00049EC0
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

		// Token: 0x06001071 RID: 4209 RVA: 0x0004AF5C File Offset: 0x00049F5C
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

		// Token: 0x06001072 RID: 4210 RVA: 0x0004AFA4 File Offset: 0x00049FA4
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

		// Token: 0x06001073 RID: 4211 RVA: 0x0004AFE8 File Offset: 0x00049FE8
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

		// Token: 0x06001074 RID: 4212 RVA: 0x0004B04C File Offset: 0x0004A04C
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

		// Token: 0x06001075 RID: 4213 RVA: 0x0004B098 File Offset: 0x0004A098
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

		// Token: 0x06001076 RID: 4214 RVA: 0x0004B1D8 File Offset: 0x0004A1D8
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

		// Token: 0x06001077 RID: 4215 RVA: 0x0004B270 File Offset: 0x0004A270
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

		// Token: 0x06001078 RID: 4216 RVA: 0x0004B330 File Offset: 0x0004A330
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

		// Token: 0x06001079 RID: 4217 RVA: 0x0004B378 File Offset: 0x0004A378
		private static void GetChild(ref XPathNode[] pageNode, ref int idxNode)
		{
			if (++idxNode >= pageNode.Length)
			{
				pageNode = pageNode[0].PageInfo.NextPage;
				idxNode = 1;
			}
		}
	}
}
