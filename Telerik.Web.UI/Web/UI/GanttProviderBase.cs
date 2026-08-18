using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using Telerik.Web.UI.Gantt;

namespace Telerik.Web.UI
{
	// Token: 0x020004A7 RID: 1191
	public abstract class GanttProviderBase : ProviderBase
	{
		// Token: 0x06002A0C RID: 10764 RVA: 0x000876C4 File Offset: 0x000858C4
		public GanttProviderBase()
		{
			this._taskFactory = new TaskFactory();
			this._dependencyFactory = new DependencyFactory();
			this._resourceFactory = new ResourceFactory();
			this._assignmentFactory = new AssignmentFactory();
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06002A0D RID: 10765 RVA: 0x000876F8 File Offset: 0x000858F8
		public virtual ITaskFactory TaskFactory
		{
			get
			{
				return this._taskFactory;
			}
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06002A0E RID: 10766 RVA: 0x00087700 File Offset: 0x00085900
		public virtual IDependencyFactory DependencyFactory
		{
			get
			{
				return this._dependencyFactory;
			}
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06002A0F RID: 10767 RVA: 0x00087708 File Offset: 0x00085908
		public virtual IResourceFactory ResourceFactory
		{
			get
			{
				return this._resourceFactory;
			}
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06002A10 RID: 10768 RVA: 0x00087710 File Offset: 0x00085910
		public virtual IAssignmentFactory AssignmentFactory
		{
			get
			{
				return this._assignmentFactory;
			}
		}

		// Token: 0x06002A11 RID: 10769
		public abstract List<ITask> GetTasks();

		// Token: 0x06002A12 RID: 10770
		public abstract ITask UpdateTask(ITask task);

		// Token: 0x06002A13 RID: 10771
		public abstract ITask DeleteTask(ITask task);

		// Token: 0x06002A14 RID: 10772
		public abstract ITask InsertTask(ITask task);

		// Token: 0x06002A15 RID: 10773 RVA: 0x00087718 File Offset: 0x00085918
		public virtual List<IDependency> GetDependencies()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x0008771F File Offset: 0x0008591F
		public virtual IDependency UpdateDependency(IDependency dependency)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x00087726 File Offset: 0x00085926
		public virtual IDependency DeleteDependency(IDependency dependency)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x0008772D File Offset: 0x0008592D
		public virtual IDependency InsertDependency(IDependency dependency)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x00087734 File Offset: 0x00085934
		public virtual List<IResource> GetResources()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x0008773B File Offset: 0x0008593B
		public virtual List<IAssignment> GetAssignments()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x00087742 File Offset: 0x00085942
		public virtual IAssignment UpdateAssignment(IAssignment assignment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x00087749 File Offset: 0x00085949
		public virtual IAssignment DeleteAssignment(IAssignment assignment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x00087750 File Offset: 0x00085950
		public virtual IAssignment InsertAssignment(IAssignment assignment)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000ADF RID: 2783
		private readonly ITaskFactory _taskFactory;

		// Token: 0x04000AE0 RID: 2784
		private readonly IDependencyFactory _dependencyFactory;

		// Token: 0x04000AE1 RID: 2785
		private readonly IResourceFactory _resourceFactory;

		// Token: 0x04000AE2 RID: 2786
		private readonly IAssignmentFactory _assignmentFactory;
	}
}
