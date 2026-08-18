using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x02000CA7 RID: 3239
	internal class ContainerNodeCollectionHelper
	{
		// Token: 0x06007981 RID: 31105 RVA: 0x001BE9AC File Offset: 0x001BCBAC
		public static ContainerNode GetOrCreateFolderNodes(ContainerNode node, IList<string> folderNames)
		{
			LinkedList<string> linkedList = new LinkedList<string>(folderNames);
			return ContainerNodeCollectionHelper.GetOrCreateFolderNodes(node, linkedList.First);
		}

		// Token: 0x06007982 RID: 31106 RVA: 0x001BE9CC File Offset: 0x001BCBCC
		private static ContainerNode GetOrCreateFolderNodes(ContainerNode node, LinkedListNode<string> currentFolderName)
		{
			if (currentFolderName == null)
			{
				return node;
			}
			string value = currentFolderName.Value;
			ContainerNode containerNode = ContainerNodeCollectionHelper.FindChildNodeByUniqueName(node, value);
			if (containerNode == null)
			{
				containerNode = new ContainerNode(value, ContainerNodeRole.Folder);
				node.Children.Add(containerNode);
			}
			return ContainerNodeCollectionHelper.GetOrCreateFolderNodes(containerNode, currentFolderName.Next);
		}

		// Token: 0x06007983 RID: 31107 RVA: 0x001BEA10 File Offset: 0x001BCC10
		private static ContainerNode FindChildNodeByUniqueName(ContainerNode node, string folderName)
		{
			foreach (ContainerNode containerNode in node.Children)
			{
				if (containerNode.Name == folderName)
				{
					return containerNode;
				}
			}
			return null;
		}
	}
}
