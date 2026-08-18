using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Common.Utils
{
	// Token: 0x02000393 RID: 915
	internal static class Helpers
	{
		// Token: 0x06003297 RID: 12951 RVA: 0x000C5B18 File Offset: 0x000C3D18
		internal static void FormatTraceLine(string format, params object[] args)
		{
			Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x000C5B2B File Offset: 0x000C3D2B
		internal static void StringTrace(string arg)
		{
			Trace.Write(arg);
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x000C5B33 File Offset: 0x000C3D33
		internal static void StringTraceLine(string arg)
		{
			Trace.WriteLine(arg);
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x000C5B3C File Offset: 0x000C3D3C
		internal static bool IsSetEqual<Type>(IEnumerable<Type> list1, IEnumerable<Type> list2, IEqualityComparer<Type> comparer)
		{
			Set<Type> set = new Set<Type>(list1, comparer);
			Set<Type> equals = new Set<Type>(list2, comparer);
			return set.SetEquals(equals);
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x000C5B60 File Offset: 0x000C3D60
		internal static IEnumerable<SuperType> AsSuperTypeList<SubType, SuperType>(IEnumerable<SubType> values) where SubType : SuperType
		{
			foreach (SubType subType in values)
			{
				yield return (SuperType)((object)subType);
			}
			IEnumerator<SubType> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x000C5B70 File Offset: 0x000C3D70
		internal static TElement[] Prepend<TElement>(TElement[] args, TElement arg)
		{
			TElement[] array = new TElement[args.Length + 1];
			array[0] = arg;
			for (int i = 0; i < args.Length; i++)
			{
				array[i + 1] = args[i];
			}
			return array;
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x000C5BB0 File Offset: 0x000C3DB0
		internal static TNode BuildBalancedTreeInPlace<TNode>(IList<TNode> nodes, Func<TNode, TNode, TNode> combinator)
		{
			EntityUtil.CheckArgumentNull<IList<TNode>>(nodes, "nodes");
			EntityUtil.CheckArgumentNull<Func<TNode, TNode, TNode>>(combinator, "combinator");
			if (nodes.Count == 1)
			{
				return nodes[0];
			}
			if (nodes.Count == 2)
			{
				return combinator(nodes[0], nodes[1]);
			}
			for (int num = nodes.Count; num != 1; num /= 2)
			{
				bool flag = (num & 1) == 1;
				if (flag)
				{
					num--;
				}
				int num2 = 0;
				for (int i = 0; i < num; i += 2)
				{
					nodes[num2++] = combinator(nodes[i], nodes[i + 1]);
				}
				if (flag)
				{
					int index = num2 - 1;
					nodes[index] = combinator(nodes[index], nodes[num]);
				}
			}
			return nodes[0];
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x000C5C7E File Offset: 0x000C3E7E
		internal static IEnumerable<TNode> GetLeafNodes<TNode>(TNode root, Func<TNode, bool> isLeaf, Func<TNode, IEnumerable<TNode>> getImmediateSubNodes)
		{
			EntityUtil.CheckArgumentNull<Func<TNode, bool>>(isLeaf, "isLeaf");
			EntityUtil.CheckArgumentNull<Func<TNode, IEnumerable<TNode>>>(getImmediateSubNodes, "getImmediateSubNodes");
			Stack<TNode> nodes = new Stack<TNode>();
			nodes.Push(root);
			while (nodes.Count > 0)
			{
				TNode tnode = nodes.Pop();
				if (isLeaf(tnode))
				{
					yield return tnode;
				}
				else
				{
					List<TNode> list = new List<TNode>(getImmediateSubNodes(tnode));
					for (int i = list.Count - 1; i > -1; i--)
					{
						nodes.Push(list[i]);
					}
				}
			}
			yield break;
		}
	}
}
