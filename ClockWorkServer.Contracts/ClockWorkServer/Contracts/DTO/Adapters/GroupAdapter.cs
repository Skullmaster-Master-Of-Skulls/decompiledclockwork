using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.DataStructure.Tree;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C83 RID: 3203
	public static class GroupAdapter
	{
		// Token: 0x060042C6 RID: 17094 RVA: 0x00021BE8 File Offset: 0x0001FDE8
		public static Forest<GroupOrGroupContainerForEditDTO> ConvertToForest(IList<GroupForEditDTO> groupsForEdit)
		{
			List<Pair<string, string>> list = new List<Pair<string, string>>();
			using (IEnumerator<GroupForEditDTO> enumerator = groupsForEdit.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					GroupForEditDTO group = enumerator.Current;
					string text = (group.FullDescription ?? "").Trim();
					bool flag = text.Length < 1;
					if (!flag)
					{
						bool flag2 = !list.Any((Pair<string, string> g) => g.Item1.Equals(group.FullDescription, StringComparison.OrdinalIgnoreCase));
						if (flag2)
						{
							list.Add(new Pair<string, string>(group.FullDescription, group.FullDescription));
						}
					}
				}
			}
			list.Sort((Pair<string, string> g1, Pair<string, string> g2) => g1.Item1.CompareTo(g2.Item1));
			list.Insert(0, new Pair<string, string>("", "Main"));
			Forest<GroupOrGroupContainerForEditDTO> forest = new Forest<GroupOrGroupContainerForEditDTO>();
			using (List<Pair<string, string>>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					Pair<string, string> uniqueGroupContainer = enumerator2.Current;
					TreeNode<GroupOrGroupContainerForEditDTO> parentNode = forest.AppendNode(null, new GroupOrGroupContainerForEditDTO
					{
						GroupContainer = new GroupContainerForEditDTO
						{
							FullDescription = uniqueGroupContainer.Item2
						}
					});
					List<GroupForEditDTO> list2 = (from g in groupsForEdit
					where (g.FullDescription ?? "").Trim().Equals(uniqueGroupContainer.Item2, StringComparison.OrdinalIgnoreCase)
					select g).ToList<GroupForEditDTO>();
					foreach (GroupForEditDTO group2 in list2)
					{
						forest.AppendNode(parentNode, new GroupOrGroupContainerForEditDTO
						{
							Group = group2
						});
					}
				}
			}
			return forest;
		}
	}
}
