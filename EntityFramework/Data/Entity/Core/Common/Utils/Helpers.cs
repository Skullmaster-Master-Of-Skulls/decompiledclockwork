using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x02000327 RID: 807
	internal static class Helpers
	{
		// Token: 0x06001BCC RID: 7116 RVA: 0x00088A32 File Offset: 0x00086C32
		internal static void FormatTraceLine(string format, params object[] args)
		{
			Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x00088A45 File Offset: 0x00086C45
		internal static void StringTrace(string arg)
		{
			Trace.Write(arg);
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x00088A4D File Offset: 0x00086C4D
		internal static void StringTraceLine(string arg)
		{
			Trace.WriteLine(arg);
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x00088A58 File Offset: 0x00086C58
		internal static bool IsSetEqual<Type>(IEnumerable<Type> list1, IEnumerable<Type> list2, IEqualityComparer<Type> comparer)
		{
			Set<Type> set = new Set<Type>(list1, comparer);
			Set<Type> equals = new Set<Type>(list2, comparer);
			return set.SetEquals(equals);
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x00088C10 File Offset: 0x00086E10
		internal static IEnumerable<SuperType> AsSuperTypeList<SubType, SuperType>(IEnumerable<SubType> values) where SubType : SuperType
		{
			foreach (SubType value in values)
			{
				yield return (SuperType)((object)value);
			}
			yield break;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x00088C30 File Offset: 0x00086E30
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

		// Token: 0x06001BD2 RID: 7122 RVA: 0x00088C70 File Offset: 0x00086E70
		internal static TNode BuildBalancedTreeInPlace<TNode>(IList<TNode> nodes, Func<TNode, TNode, TNode> combinator)
		{
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

		// Token: 0x06001BD3 RID: 7123 RVA: 0x00088EAC File Offset: 0x000870AC
		internal static IEnumerable<TNode> GetLeafNodes<TNode>(TNode root, Func<TNode, bool> isLeaf, Func<TNode, IEnumerable<TNode>> getImmediateSubNodes)
		{
			Stack<TNode> nodes = new Stack<TNode>();
			nodes.Push(root);
			while (nodes.Count > 0)
			{
				TNode current = nodes.Pop();
				if (isLeaf(current))
				{
					yield return current;
				}
				else
				{
					List<TNode> list = new List<TNode>(getImmediateSubNodes(current));
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
