using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020004AD RID: 1197
	public class TaskCollection : BaseCollection<ITask>
	{
		// Token: 0x06002AA4 RID: 10916 RVA: 0x00089EA6 File Offset: 0x000880A6
		public TaskCollection()
		{
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x00089EAE File Offset: 0x000880AE
		public TaskCollection(IGantt owner) : base(owner)
		{
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x00089EC4 File Offset: 0x000880C4
		public override void AddRange(IEnumerable<ITask> tasks)
		{
			foreach (ITask task in from p in tasks
			where p.ParentID == null
			select p)
			{
				this.Add(task);
				this.BuildTree(tasks, task);
			}
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x00089F38 File Offset: 0x00088138
		public override void Add(ITask task)
		{
			base.Add(task);
			if (base.Owner != null)
			{
				task.Owner = base.Owner;
			}
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x00089F78 File Offset: 0x00088178
		protected internal virtual void BuildTree(IEnumerable<ITask> tasks, ITask root)
		{
			IEnumerable<ITask> enumerable = from p in tasks
			where object.Equals(p.ParentID, root.ID)
			select p;
			foreach (ITask task in enumerable)
			{
				if (root != null)
				{
					root.Tasks.Add(task);
					this.BuildTree(tasks, task);
				}
			}
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x00089FFC File Offset: 0x000881FC
		protected internal virtual void FillTree(IList<ITask> flastList, ITask root)
		{
			flastList.Add(root);
			foreach (ITask root2 in root.Tasks)
			{
				this.FillTree(flastList, root2);
			}
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x0008A054 File Offset: 0x00088254
		protected internal virtual IList<ITask> ToFlatList()
		{
			List<ITask> list = new List<ITask>();
			foreach (ITask root in this)
			{
				this.FillTree(list, root);
			}
			return list;
		}
	}
}
