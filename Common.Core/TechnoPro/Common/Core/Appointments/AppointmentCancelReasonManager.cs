using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Appointments
{
	// Token: 0x0200012B RID: 299
	public class AppointmentCancelReasonManager : IAppointmentCancelReasonManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x00058BF8 File Offset: 0x00056DF8
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x00058C00 File Offset: 0x00056E00
		public IAppointmentCancelReasonDAO dao { get; set; }

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00058C09 File Offset: 0x00056E09
		public AppointmentCancelReasonManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new AppointmentCancelReasonDAO(this.OpContext);
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x00058C2D File Offset: 0x00056E2D
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x00058C35 File Offset: 0x00056E35
		public OperationContext OpContext { get; set; }

		// Token: 0x06000CAC RID: 3244 RVA: 0x00058C40 File Offset: 0x00056E40
		public Forest<AppCancelReasonOrGroup> LoadCancelReasons()
		{
			IList<AppCancelReason> list = this.LoadAllCancelReasons();
			List<AppCancelReason> list2;
			if (list == null)
			{
				list2 = null;
			}
			else
			{
				list2 = (from g in list
				where g.IsActive
				select g).ToList<AppCancelReason>();
			}
			List<AppCancelReason> list3 = list2;
			Forest<AppCancelReasonOrGroup> forest = new Forest<AppCancelReasonOrGroup>();
			Stack<TreeNode<AppCancelReasonOrGroup>> stack = new Stack<TreeNode<AppCancelReasonOrGroup>>();
			stack.Push(null);
			foreach (AppCancelReason appCancelReason in list3)
			{
				TreeNode<AppCancelReasonOrGroup> treeNode = stack.Peek();
				string text = (appCancelReason.CancelReasonGroup == null) ? "" : (appCancelReason.CancelReasonGroup.CancelReasonGroupName ?? "");
				string value = (treeNode != null) ? (treeNode.Value.AppCancelReasonGroup.CancelReasonGroupName ?? "") : "";
				bool flag = !text.Equals(value, StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					bool flag2 = treeNode != null;
					if (flag2)
					{
						stack.Pop();
						treeNode = stack.Peek();
					}
					AppCancelReasonGroup appCancelReasonGroup;
					if (appCancelReason.CancelReasonGroup == null || string.IsNullOrEmpty(appCancelReason.CancelReasonGroup.CancelReasonGroupName))
					{
						(appCancelReasonGroup = new AppCancelReasonGroup()).CancelReasonGroupName = ((text.Length > 0) ? text : "Cancel reasons");
					}
					else
					{
						appCancelReasonGroup = appCancelReason.CancelReasonGroup;
					}
					AppCancelReasonGroup appCancelReasonGroup2 = appCancelReasonGroup;
					treeNode = forest.AppendNode(treeNode, new AppCancelReasonOrGroup
					{
						AppCancelReasonGroup = appCancelReasonGroup2
					});
					stack.Push(treeNode);
				}
				forest.AppendNode(treeNode, new AppCancelReasonOrGroup
				{
					AppCancelReason = appCancelReason
				});
			}
			return forest;
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00058DFC File Offset: 0x00056FFC
		public IList<AppCancelReason> LoadAllCancelReasons()
		{
			return this.dao.LoadAllCancelReasons();
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00058E1C File Offset: 0x0005701C
		public AppCancelReason LoadCancelReasonById(int CancelReasonId)
		{
			return this.dao.LoadCancelReasonById(CancelReasonId);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00058E3A File Offset: 0x0005703A
		public void DeleteCancelReason(int CancelReasonId)
		{
			this.dao.DeleteCancelReason(CancelReasonId);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00058E4A File Offset: 0x0005704A
		public void UpdateCancelReason(AppCancelReason CancelReason)
		{
			this.dao.UpdateCancelReason(CancelReason);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00058E5C File Offset: 0x0005705C
		public int CreateCancelReason(AppCancelReason CancelReason)
		{
			return this.dao.CreateCancelReason(CancelReason);
		}
	}
}
