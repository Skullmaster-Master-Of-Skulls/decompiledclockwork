using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public.Entities.Templates;

namespace TechnoPro.Common.Core.Adapters
{
	// Token: 0x02000178 RID: 376
	public static class TemplateAdapters
	{
		// Token: 0x0600104F RID: 4175 RVA: 0x000783C0 File Offset: 0x000765C0
		public static Forest<TemplateOrGroup> ConvertTemplateCollectionToForest(this TemplateCollection TemplateCollection)
		{
			Forest<TemplateOrGroup> forest = new Forest<TemplateOrGroup>();
			bool flag = TemplateCollection.Groups == null;
			if (flag)
			{
				TemplateCollection.Groups = new List<TemplateGroup>();
			}
			List<TemplateGroup> groups = TemplateCollection.Groups.ToList<TemplateGroup>();
			groups.Sort((TemplateGroup g1, TemplateGroup g2) => (g1.Title ?? "").CompareTo(g2.Title ?? ""));
			using (List<TemplateGroup>.Enumerator enumerator = groups.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TemplateGroup group = enumerator.Current;
					TreeNode<TemplateOrGroup> parentNode = forest.AppendNode(null, new TemplateOrGroup
					{
						Group = group
					});
					IEnumerable<Template> enumerable = from g in TemplateCollection.Templates
					where g.IsInGroup(@group)
					select g;
					foreach (Template item in enumerable)
					{
						forest.AppendNode(parentNode, new TemplateOrGroup
						{
							Item = item
						});
					}
				}
			}
			List<Template> list = (from g in TemplateCollection.Templates
			where groups.FirstOrDefault(new Func<TemplateGroup, bool>(g.IsInGroup)) == null
			select g).ToList<Template>();
			list.Sort((Template h1, Template h2) => (h1.TemplateTitle ?? "").CompareTo(h2.TemplateTitle ?? ""));
			foreach (Template item2 in list)
			{
				forest.AppendNode(null, new TemplateOrGroup
				{
					Item = item2
				});
			}
			return forest;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000785A8 File Offset: 0x000767A8
		public static TemplateCollection ConvertTemplateForestToCollection(this Forest<TemplateOrGroup> Forest)
		{
			List<Template> templates = new List<Template>();
			List<TemplateGroup> groups = new List<TemplateGroup>();
			TemplateAdapters.CollectGroupsAndItems(Forest.Nodes, ref groups, ref templates);
			return new TemplateCollection
			{
				Templates = templates,
				Groups = groups
			};
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x000785EC File Offset: 0x000767EC
		private static void CollectGroupsAndItems(TreeNodeCollection<TemplateOrGroup> nodes, ref List<TemplateGroup> groups, ref List<Template> items)
		{
			using (IEnumerator<TreeNode<TemplateOrGroup>> enumerator = nodes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TreeNode<TemplateOrGroup> x = enumerator.Current;
					bool flag = x.Value.Item != null && items.FirstOrDefault((Template h) => h.TemplateId == x.Value.Item.TemplateId) == null;
					if (flag)
					{
						items.Add(x.Value.Item);
					}
					else
					{
						bool flag2 = x.Value.Group != null && groups.FirstOrDefault((TemplateGroup h) => h.TemplateGroupId == x.Value.Group.TemplateGroupId) == null;
						if (flag2)
						{
							groups.Add(x.Value.Group);
						}
					}
					bool flag3 = x.Nodes.Count > 0;
					if (flag3)
					{
						TemplateAdapters.CollectGroupsAndItems(x.Nodes, ref groups, ref items);
					}
				}
			}
		}
	}
}
