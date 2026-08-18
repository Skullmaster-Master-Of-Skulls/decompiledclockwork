using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200034C RID: 844
	public class WebServiceController
	{
		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x0005AD88 File Offset: 0x00058F88
		public virtual GanttProviderBase Provider
		{
			get
			{
				return this._provider;
			}
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0005AD90 File Offset: 0x00058F90
		public WebServiceController(string providerName)
		{
			this.LoadProvider(providerName);
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0005AD9F File Offset: 0x00058F9F
		public WebServiceController(GanttProviderBase provider)
		{
			this._provider = provider;
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x0005ADAE File Offset: 0x00058FAE
		public virtual IEnumerable<TaskData> GetTasks()
		{
			return this.GetTasks<TaskData>();
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x0005ADF5 File Offset: 0x00058FF5
		public virtual IEnumerable<T> GetTasks<T>() where T : ITaskData, new()
		{
			return this.Provider.GetTasks().Select(delegate(ITask task)
			{
				T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
				result.CopyFrom(task);
				return result;
			}).ToList<T>();
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x0005AE18 File Offset: 0x00059018
		public virtual IEnumerable<TaskData> InsertTasks(IEnumerable<TaskData> models)
		{
			return this.InsertTasks<TaskData>(models);
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0005B050 File Offset: 0x00059250
		public virtual IEnumerable<T> InsertTasks<T>(IEnumerable<T> models) where T : ITaskData, new()
		{
			foreach (T taskData in models)
			{
				ITask task = this.Provider.TaskFactory.CreateTask();
				T t = taskData;
				t.CopyTo(task);
				T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
				result.CopyFrom(this.Provider.InsertTask(task));
				yield return result;
			}
			yield break;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0005B074 File Offset: 0x00059274
		public virtual IEnumerable<TaskData> UpdateTasks(IEnumerable<TaskData> models)
		{
			return this.UpdateTasks<TaskData>(models);
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0005B2AC File Offset: 0x000594AC
		public virtual IEnumerable<T> UpdateTasks<T>(IEnumerable<T> models) where T : ITaskData, new()
		{
			foreach (T taskData in models)
			{
				ITask task = this.Provider.TaskFactory.CreateTask();
				T t = taskData;
				t.CopyTo(task);
				T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
				result.CopyFrom(this.Provider.UpdateTask(task));
				yield return result;
			}
			yield break;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x0005B2D0 File Offset: 0x000594D0
		public virtual IEnumerable<TaskData> DeleteTasks(IEnumerable<TaskData> models)
		{
			return this.DeleteTasks<TaskData>(models);
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x0005B508 File Offset: 0x00059708
		public virtual IEnumerable<T> DeleteTasks<T>(IEnumerable<T> models) where T : ITaskData, new()
		{
			foreach (T taskData in models)
			{
				ITask task = this.Provider.TaskFactory.CreateTask();
				T t = taskData;
				t.CopyTo(task);
				T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
				result.CopyFrom(this.Provider.DeleteTask(task));
				yield return result;
			}
			yield break;
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x0005B52C File Offset: 0x0005972C
		public virtual IEnumerable<DependencyData> GetDependencies()
		{
			return this.GetDependencies<DependencyData>();
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0005B571 File Offset: 0x00059771
		public virtual IEnumerable<T> GetDependencies<T>() where T : IDependencyData, new()
		{
			return this.Provider.GetDependencies().Select(delegate(IDependency dependency)
			{
				T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
				result.CopyFrom(dependency);
				return result;
			}).ToList<T>();
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0005B594 File Offset: 0x00059794
		public virtual IEnumerable<DependencyData> InsertDependencies(IEnumerable<DependencyData> models)
		{
			return this.InsertDependencies<DependencyData>(models);
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x0005B7CC File Offset: 0x000599CC
		public virtual IEnumerable<T> InsertDependencies<T>(IEnumerable<T> models) where T : IDependencyData, new()
		{
			foreach (T dependencyData in models)
			{
				IDependency dependency = this.Provider.DependencyFactory.CreateDependency();
				T t = dependencyData;
				t.CopyTo(dependency);
				T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
				result.CopyFrom(this.Provider.InsertDependency(dependency));
				yield return result;
			}
			yield break;
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0005B7F0 File Offset: 0x000599F0
		public virtual IEnumerable<DependencyData> DeleteDependencies(IEnumerable<DependencyData> models)
		{
			return this.DeleteDependencies<DependencyData>(models);
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x0005BA28 File Offset: 0x00059C28
		public virtual IEnumerable<T> DeleteDependencies<T>(IEnumerable<T> models) where T : IDependencyData, new()
		{
			foreach (T dependencyData in models)
			{
				IDependency dependency = this.Provider.DependencyFactory.CreateDependency();
				T t = dependencyData;
				t.CopyTo(dependency);
				T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
				result.CopyFrom(this.Provider.DeleteDependency(dependency));
				yield return result;
			}
			yield break;
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x0005BA4C File Offset: 0x00059C4C
		public virtual IEnumerable<ResourceData> GetResources()
		{
			return this.GetResources<ResourceData>();
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0005BA91 File Offset: 0x00059C91
		public virtual IEnumerable<T> GetResources<T>() where T : IResourceData, new()
		{
			return this.Provider.GetResources().Select(delegate(IResource resource)
			{
				T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
				result.CopyFrom(resource);
				return result;
			}).ToList<T>();
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0005BAB4 File Offset: 0x00059CB4
		public virtual IEnumerable<AssignmentData> GetAssignments()
		{
			return this.GetAssignments<AssignmentData>();
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0005BAF9 File Offset: 0x00059CF9
		public virtual IEnumerable<T> GetAssignments<T>() where T : IAssignmentData, new()
		{
			return this.Provider.GetAssignments().Select(delegate(IAssignment assignment)
			{
				T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
				result.CopyFrom(assignment);
				return result;
			}).ToList<T>();
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x0005BB1C File Offset: 0x00059D1C
		public virtual IEnumerable<AssignmentData> InsertAssignments(IEnumerable<AssignmentData> models)
		{
			return this.InsertAssignments<AssignmentData>(models);
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x0005BD54 File Offset: 0x00059F54
		public virtual IEnumerable<T> InsertAssignments<T>(IEnumerable<T> models) where T : IAssignmentData, new()
		{
			foreach (T assignmentData in models)
			{
				IAssignment assignment = this.Provider.AssignmentFactory.CreateAssignment();
				T t = assignmentData;
				t.CopyTo(assignment);
				T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
				result.CopyFrom(this.Provider.InsertAssignment(assignment));
				yield return result;
			}
			yield break;
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x0005BD78 File Offset: 0x00059F78
		public virtual IEnumerable<AssignmentData> UpdateAssignments(IEnumerable<AssignmentData> models)
		{
			return this.UpdateAssignments<AssignmentData>(models);
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0005BFB0 File Offset: 0x0005A1B0
		public virtual IEnumerable<T> UpdateAssignments<T>(IEnumerable<T> models) where T : IAssignmentData, new()
		{
			foreach (T assignmentData in models)
			{
				IAssignment assignment = this.Provider.AssignmentFactory.CreateAssignment();
				T t = assignmentData;
				t.CopyTo(assignment);
				T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
				result.CopyFrom(this.Provider.UpdateAssignment(assignment));
				yield return result;
			}
			yield break;
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x0005BFD4 File Offset: 0x0005A1D4
		public virtual IEnumerable<AssignmentData> DeleteAssignments(IEnumerable<AssignmentData> models)
		{
			return this.DeleteAssignments<AssignmentData>(models);
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x0005C20C File Offset: 0x0005A40C
		public virtual IEnumerable<T> DeleteAssignments<T>(IEnumerable<T> models) where T : IAssignmentData, new()
		{
			foreach (T assignmentData in models)
			{
				IAssignment assignment = this.Provider.AssignmentFactory.CreateAssignment();
				T t = assignmentData;
				t.CopyTo(assignment);
				T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
				result.CopyFrom(this.Provider.DeleteAssignment(assignment));
				yield return result;
			}
			yield break;
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x0005C230 File Offset: 0x0005A430
		protected void LoadProvider(string providerName)
		{
			if (providerName == "Integrated")
			{
				throw new ConfigurationErrorsException("The Integrated provider is not supported when binding to a Web Service.");
			}
			this._provider = GanttProviderFactory.GetProvider(providerName);
		}

		// Token: 0x04000757 RID: 1879
		private GanttProviderBase _provider;
	}
}
