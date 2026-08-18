using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C8B RID: 3211
	public static class ReportAdapter
	{
		// Token: 0x060042E6 RID: 17126 RVA: 0x00022CBC File Offset: 0x00020EBC
		private static Forest<ReportOrGroupDTO> MoveReportNodeUpDown(this Forest<ReportOrGroupDTO> forest, TreeNode<ReportOrGroupDTO> node, int direction)
		{
			TreeNode<ReportOrGroupDTO> treeNode = (node.Value.Report.GroupId < 1) ? null : forest.Find((ReportOrGroupDTO g) => g.Group != null && g.Group.GroupId == node.Value.Report.GroupId);
			int num = 10;
			TreeNodeCollection<ReportOrGroupDTO> source = (treeNode == null) ? forest.Nodes : treeNode.Nodes;
			List<TreeNode<ReportOrGroupDTO>> list = (from g in source
			where g.Value.Report != null
			select g).ToList<TreeNode<ReportOrGroupDTO>>();
			list.Sort(delegate(TreeNode<ReportOrGroupDTO> g1, TreeNode<ReportOrGroupDTO> g2)
			{
				ReportGroupDTO group = g1.Value.Group;
				ReportGroupDTO group2 = g2.Value.Group;
				ReportDTO report = g1.Value.Report;
				ReportDTO report2 = g2.Value.Report;
				bool flag4 = group != null && group2 != null;
				int result;
				if (flag4)
				{
					result = group.OrderNum.CompareTo(group2.OrderNum);
				}
				else
				{
					bool flag5 = report != null && report2 != null;
					if (flag5)
					{
						result = report.OrderNum.CompareTo(report2.OrderNum);
					}
					else
					{
						bool flag6 = group != null;
						if (flag6)
						{
							result = -1;
						}
						else
						{
							result = 1;
						}
					}
				}
				return result;
			});
			TreeNode<ReportOrGroupDTO> treeNode2 = null;
			foreach (TreeNode<ReportOrGroupDTO> treeNode3 in list)
			{
				treeNode3.Value.Report.OrderNum = num;
				num += 10;
				bool flag = treeNode3.Value.Report.ReportId == node.Value.Report.ReportId;
				if (flag)
				{
					bool flag2 = direction < 0;
					if (flag2)
					{
						bool flag3 = treeNode2 != null;
						if (flag3)
						{
							int orderNum = treeNode2.Value.Report.OrderNum;
							treeNode2.Value.Report.OrderNum = treeNode3.Value.Report.OrderNum;
							treeNode3.Value.Report.OrderNum = orderNum;
						}
					}
					else
					{
						treeNode3.Value.Report.OrderNum = num + 1;
					}
				}
				treeNode2 = treeNode3;
			}
			return forest.ReBuildReportForest();
		}

		// Token: 0x060042E7 RID: 17127 RVA: 0x00022E8C File Offset: 0x0002108C
		private static Forest<ReportOrGroupDTO> MoveGroupNodeUpDown(this Forest<ReportOrGroupDTO> forest, TreeNode<ReportOrGroupDTO> node, int direction)
		{
			TreeNode<ReportOrGroupDTO> treeNode = (node.Value.Group.ParentGroupId < 1) ? null : forest.Find((ReportOrGroupDTO g) => g.Group != null && g.Group.GroupId == node.Value.Group.ParentGroupId);
			int num = 10;
			TreeNodeCollection<ReportOrGroupDTO> source = (treeNode == null) ? forest.Nodes : treeNode.Nodes;
			List<TreeNode<ReportOrGroupDTO>> list = (from g in source
			where g.Value.Group != null
			select g).ToList<TreeNode<ReportOrGroupDTO>>();
			list.Sort(delegate(TreeNode<ReportOrGroupDTO> g1, TreeNode<ReportOrGroupDTO> g2)
			{
				ReportGroupDTO group = g1.Value.Group;
				ReportGroupDTO group2 = g2.Value.Group;
				ReportDTO report = g1.Value.Report;
				ReportDTO report2 = g2.Value.Report;
				bool flag4 = group != null && group2 != null;
				int result;
				if (flag4)
				{
					result = group.OrderNum.CompareTo(group2.OrderNum);
				}
				else
				{
					bool flag5 = report != null && report2 != null;
					if (flag5)
					{
						result = report.OrderNum.CompareTo(report2.OrderNum);
					}
					else
					{
						bool flag6 = group != null;
						if (flag6)
						{
							result = -1;
						}
						else
						{
							result = 1;
						}
					}
				}
				return result;
			});
			TreeNode<ReportOrGroupDTO> treeNode2 = null;
			foreach (TreeNode<ReportOrGroupDTO> treeNode3 in list)
			{
				treeNode3.Value.Group.OrderNum = num;
				num += 10;
				bool flag = treeNode3.Value.Group.GroupId == node.Value.Group.GroupId;
				if (flag)
				{
					bool flag2 = direction < 0;
					if (flag2)
					{
						bool flag3 = treeNode2 != null;
						if (flag3)
						{
							int orderNum = treeNode2.Value.Group.OrderNum;
							treeNode2.Value.Group.OrderNum = treeNode3.Value.Group.OrderNum;
							treeNode3.Value.Group.OrderNum = orderNum;
						}
					}
					else
					{
						treeNode3.Value.Group.OrderNum = num + 1;
					}
				}
				treeNode2 = treeNode3;
			}
			return forest.ReBuildReportForest();
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x0002305C File Offset: 0x0002125C
		private static void AddGroupsAndReportsToForest(List<ReportAdapter.Node> groupNodes, TreeNode<ReportOrGroupDTO> currentParentNode, ref Forest<ReportOrGroupDTO> forest, ReportCollectionDTO reportCollection)
		{
			using (List<ReportAdapter.Node>.Enumerator enumerator = groupNodes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ReportAdapter.Node groupNode = enumerator.Current;
					ReportGroupDTO group = reportCollection.ReportGroups.FirstOrDefault((ReportGroupDTO g) => g.GroupId == groupNode.Id);
					TreeNode<ReportOrGroupDTO> treeNode = forest.AppendNode(currentParentNode, new ReportOrGroupDTO
					{
						Group = group
					});
					bool flag = groupNode.Children.Count > 0;
					if (flag)
					{
						ReportAdapter.AddGroupsAndReportsToForest(groupNode.Children, treeNode, ref forest, reportCollection);
					}
					List<ReportDTO> list = (from r in reportCollection.Reports
					where r.GroupId == groupNode.Id
					select r).ToList<ReportDTO>();
					foreach (ReportDTO report in list)
					{
						forest.AppendNode(treeNode, new ReportOrGroupDTO
						{
							Report = report
						});
					}
				}
			}
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x00023184 File Offset: 0x00021384
		private static List<ReportAdapter.Node> MakeTreeFromFlatList(IList<ReportGroupDTO> reportGroups)
		{
			List<ReportAdapter.Node> list = new List<ReportAdapter.Node>();
			foreach (ReportGroupDTO reportGroupDTO in reportGroups)
			{
				list.Add(new ReportAdapter.Node(reportGroupDTO.GroupId, reportGroupDTO.ParentGroupId));
			}
			return ReportAdapter.MakeTreeFromFlatList(list);
		}

		// Token: 0x060042EA RID: 17130 RVA: 0x000231F0 File Offset: 0x000213F0
		private static List<ReportAdapter.Node> MakeTreeFromFlatList(IEnumerable<ReportAdapter.Node> flatList)
		{
			Dictionary<int, ReportAdapter.Node> dictionary = flatList.ToDictionary((ReportAdapter.Node n) => n.Id, (ReportAdapter.Node n) => n);
			List<ReportAdapter.Node> list = new List<ReportAdapter.Node>();
			foreach (ReportAdapter.Node node in flatList)
			{
				bool flag = node.ParentId != null && dictionary.ContainsKey(node.ParentId.Value);
				if (flag)
				{
					ReportAdapter.Node node2 = dictionary[node.ParentId.Value];
					node.Parent = node2;
					node2.Children.Add(node);
				}
				else
				{
					list.Add(node);
				}
			}
			return list;
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x000232F4 File Offset: 0x000214F4
		public static Forest<ReportOrGroupDTO> MoveNodeUpDown(this Forest<ReportOrGroupDTO> forest, TreeNode<ReportOrGroupDTO> node, int direction)
		{
			bool flag = node == null;
			Forest<ReportOrGroupDTO> result;
			if (flag)
			{
				result = forest;
			}
			else
			{
				bool flag2 = node.Value.Report != null;
				if (flag2)
				{
					result = forest.MoveReportNodeUpDown(node, direction);
				}
				else
				{
					bool flag3 = node.Value.Group != null;
					if (flag3)
					{
						result = forest.MoveGroupNodeUpDown(node, direction);
					}
					else
					{
						result = forest;
					}
				}
			}
			return result;
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x00023350 File Offset: 0x00021550
		public static Forest<ReportOrGroupDTO> BuildReportForest(this ReportCollectionDTO reportCollection)
		{
			List<ReportDTO> list = (from g in reportCollection.Reports
			where g.GroupId < 1 || reportCollection.ReportGroups.FirstOrDefault((ReportGroupDTO h) => h.GroupId == g.GroupId) == null
			select g).ToList<ReportDTO>();
			List<ReportAdapter.Node> groupNodes = ReportAdapter.MakeTreeFromFlatList(reportCollection.ReportGroups);
			Forest<ReportOrGroupDTO> forest = new Forest<ReportOrGroupDTO>();
			ReportAdapter.AddGroupsAndReportsToForest(groupNodes, null, ref forest, reportCollection);
			foreach (ReportDTO report in list)
			{
				forest.AppendNode(null, new ReportOrGroupDTO
				{
					Report = report
				});
			}
			return forest;
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x00023410 File Offset: 0x00021610
		public static ReportCollectionDTO ConvertToReportCollection(this Forest<ReportOrGroupDTO> reportForest)
		{
			ReportCollectionDTO reportCollectionDTO = new ReportCollectionDTO();
			IList<TreeNode<ReportOrGroupDTO>> allNodesList = reportForest.AllNodesList;
			List<TreeNode<ReportOrGroupDTO>> list = (from g in allNodesList
			where g.Value.Group != null
			select g).ToList<TreeNode<ReportOrGroupDTO>>();
			List<TreeNode<ReportOrGroupDTO>> list2 = (from g in allNodesList
			where g.Value.Report != null
			select g).ToList<TreeNode<ReportOrGroupDTO>>();
			reportCollectionDTO.Reports = list2.ConvertAll<ReportDTO>((TreeNode<ReportOrGroupDTO> g) => g.Value.Report);
			reportCollectionDTO.ReportGroups = list.ConvertAll<ReportGroupDTO>((TreeNode<ReportOrGroupDTO> g) => g.Value.Group);
			return reportCollectionDTO;
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x000234E0 File Offset: 0x000216E0
		public static Forest<ReportOrGroupDTO> ReBuildReportForest(this Forest<ReportOrGroupDTO> reportForest)
		{
			ReportCollectionDTO reportCollection = reportForest.ConvertToReportCollection();
			return reportCollection.BuildReportForest();
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x00023500 File Offset: 0x00021700
		public static string GetFunctionTypeDescription(this eFunctionType functionType)
		{
			return functionType.ToString().Replace("_", " ");
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x00023530 File Offset: 0x00021730
		public static string GetDefaultFunctionParameter(this ReportFunctionDTO Function)
		{
			bool flag = Function == null || Function.FunctionParameters == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				ReportParameterDTO reportParameterDTO = Function.FunctionParameters.FirstOrDefault((ReportParameterDTO f) => f.Name.Equals("default", StringComparison.OrdinalIgnoreCase));
				result = ((reportParameterDTO == null) ? "" : reportParameterDTO.Value.ToString());
			}
			return result;
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x000235A0 File Offset: 0x000217A0
		public static ReportFunctionTypeAttribute GetFunctionTypeAttribute(this eFunctionType FunctionType)
		{
			return ReportFunctionTypeAttribute.GetAttribute<ReportFunctionTypeAttribute>(FunctionType);
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x000235C0 File Offset: 0x000217C0
		public static string GetTitle(this ReportOrGroupDTO ReportOrGroup)
		{
			bool flag = ReportOrGroup == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = ReportOrGroup.Report != null;
				if (flag2)
				{
					result = (ReportOrGroup.Report.Title ?? "");
				}
				else
				{
					bool flag3 = ReportOrGroup.Group != null;
					if (flag3)
					{
						result = (ReportOrGroup.Group.Title ?? "");
					}
					else
					{
						result = "";
					}
				}
			}
			return result;
		}

		// Token: 0x02000CBC RID: 3260
		internal class Node
		{
			// Token: 0x060043CB RID: 17355 RVA: 0x00024CFF File Offset: 0x00022EFF
			public Node()
			{
				this.Children = new List<ReportAdapter.Node>();
			}

			// Token: 0x060043CC RID: 17356 RVA: 0x00024D18 File Offset: 0x00022F18
			public Node(int id, int parentId)
			{
				this.Id = id;
				bool flag = parentId > 0;
				if (flag)
				{
					this.ParentId = new int?(parentId);
				}
				this.Children = new List<ReportAdapter.Node>();
			}

			// Token: 0x170018C5 RID: 6341
			// (get) Token: 0x060043CD RID: 17357 RVA: 0x00024D56 File Offset: 0x00022F56
			// (set) Token: 0x060043CE RID: 17358 RVA: 0x00024D5E File Offset: 0x00022F5E
			public int Id { get; set; }

			// Token: 0x170018C6 RID: 6342
			// (get) Token: 0x060043CF RID: 17359 RVA: 0x00024D67 File Offset: 0x00022F67
			// (set) Token: 0x060043D0 RID: 17360 RVA: 0x00024D6F File Offset: 0x00022F6F
			public int? ParentId { get; set; }

			// Token: 0x170018C7 RID: 6343
			// (get) Token: 0x060043D1 RID: 17361 RVA: 0x00024D78 File Offset: 0x00022F78
			// (set) Token: 0x060043D2 RID: 17362 RVA: 0x00024D80 File Offset: 0x00022F80
			public List<ReportAdapter.Node> Children { get; set; }

			// Token: 0x170018C8 RID: 6344
			// (get) Token: 0x060043D3 RID: 17363 RVA: 0x00024D89 File Offset: 0x00022F89
			// (set) Token: 0x060043D4 RID: 17364 RVA: 0x00024D91 File Offset: 0x00022F91
			public ReportAdapter.Node Parent { get; set; }
		}
	}
}
