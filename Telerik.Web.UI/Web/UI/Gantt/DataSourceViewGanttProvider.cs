using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Web.UI;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020004A8 RID: 1192
	internal class DataSourceViewGanttProvider : GanttProviderBase, IDisposable
	{
		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06002A1E RID: 10782 RVA: 0x00087757 File Offset: 0x00085957
		protected internal virtual DataSourceView TasksView
		{
			get
			{
				return this._gantt.TasksView;
			}
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06002A1F RID: 10783 RVA: 0x00087764 File Offset: 0x00085964
		protected internal virtual DataSourceView DependenciesView
		{
			get
			{
				return this._gantt.DependenciesView;
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06002A20 RID: 10784 RVA: 0x00087771 File Offset: 0x00085971
		protected internal virtual DataSourceView ResourcesView
		{
			get
			{
				return this._gantt.ResourcesView;
			}
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06002A21 RID: 10785 RVA: 0x0008777E File Offset: 0x0008597E
		protected internal virtual DataSourceView AssignmentsView
		{
			get
			{
				return this._gantt.AssignmentsView;
			}
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06002A22 RID: 10786 RVA: 0x0008778B File Offset: 0x0008598B
		public override string Name
		{
			get
			{
				return "Integrated";
			}
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x00087794 File Offset: 0x00085994
		public DataSourceViewGanttProvider(IGantt gantt)
		{
			this._bindings = gantt.DataBindings;
			this._gantt = gantt;
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x0008786C File Offset: 0x00085A6C
		public override List<ITask> GetTasks()
		{
			this.EnsureTaskDataFields();
			this.TasksView.Select(DataSourceSelectArguments.Empty, delegate(IEnumerable data)
			{
				this._selectedTasksData = data;
				this._selectedTasksCompleted.Set();
			});
			this._selectedTasksCompleted.WaitOne();
			if (this._selectedTasksData == null)
			{
				return null;
			}
			List<ITask> list = new List<ITask>();
			foreach (object dataItem in this._selectedTasksData)
			{
				list.Add(TasksBinder.BindTask(dataItem, this._bindings.TasksDataBindings));
			}
			return list;
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x00087948 File Offset: 0x00085B48
		public override ITask UpdateTask(ITask task)
		{
			if (this.TasksView.CanUpdate)
			{
				this.EnsureTaskDataFields();
				OrderedDictionary orderedDictionary = new OrderedDictionary();
				orderedDictionary.Add(this._bindings.TasksDataBindings.IdField, task.ID);
				IOrderedDictionary values = this.TranslateTaskKeys(task.GetData());
				ITask task2 = this.GetTasks().Find((ITask x) => object.Equals(x.ID, task.ID));
				IOrderedDictionary oldValues = this.TranslateTaskKeys(task2.GetData());
				this.TasksView.Update(orderedDictionary, values, oldValues, delegate(int count, Exception ex)
				{
					this._updatedTaskCompleted.Set();
					return DataSourceViewGanttProvider.OnDataSourceOperationCompleted(count, ex);
				});
				this._updatedTaskCompleted.WaitOne();
			}
			return task;
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x00087A3C File Offset: 0x00085C3C
		public override ITask DeleteTask(ITask task)
		{
			if (this.TasksView.CanDelete)
			{
				this.EnsureTaskDataFields();
				OrderedDictionary orderedDictionary = new OrderedDictionary();
				orderedDictionary.Add(this._bindings.TasksDataBindings.IdField, task.ID);
				IOrderedDictionary oldValues = this.TranslateTaskKeys(task.GetData());
				this.TasksView.Delete(orderedDictionary, oldValues, delegate(int count, Exception ex)
				{
					this._deletedTaskCompleted.Set();
					return DataSourceViewGanttProvider.OnDataSourceOperationCompleted(count, ex);
				});
				this._deletedTaskCompleted.WaitOne();
			}
			return task;
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x00087AC8 File Offset: 0x00085CC8
		public override ITask InsertTask(ITask task)
		{
			if (this.TasksView.CanInsert)
			{
				this.EnsureTaskDataFields();
				HashSet<object> hashSet = new HashSet<object>();
				foreach (ITask task2 in this.GetTasks())
				{
					hashSet.Add(task2.ID);
				}
				IOrderedDictionary orderedDictionary = this.TranslateTaskKeys(task.GetData());
				orderedDictionary.Remove("ID");
				this.TasksView.Insert(orderedDictionary, delegate(int count, Exception ex)
				{
					this._insertedTaskCompleted.Set();
					return true;
				});
				this._insertedTaskCompleted.WaitOne();
				ITask result = null;
				foreach (ITask task3 in this.GetTasks())
				{
					if (!hashSet.Contains(task3.ID))
					{
						result = task3;
						break;
					}
				}
				return result;
			}
			return task;
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x00087BF4 File Offset: 0x00085DF4
		public override List<IDependency> GetDependencies()
		{
			List<IDependency> list = new List<IDependency>();
			if (this.DependenciesView == null)
			{
				return list;
			}
			this.EnsureDependencyDataFields();
			this.DependenciesView.Select(DataSourceSelectArguments.Empty, delegate(IEnumerable data)
			{
				this._selectedDependenciesData = data;
				this._selectDependenciesCompleted.Set();
			});
			this._selectDependenciesCompleted.WaitOne();
			if (this._selectedDependenciesData == null)
			{
				return null;
			}
			foreach (object dataItem in this._selectedDependenciesData)
			{
				list.Add(DependenciesBinder.BindDependency(dataItem, this._bindings.DependenciesDataBindings));
			}
			return list;
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x00087CDC File Offset: 0x00085EDC
		public override IDependency UpdateDependency(IDependency dependency)
		{
			if (this.DependenciesView != null && this.DependenciesView.CanUpdate)
			{
				this.EnsureDependencyDataFields();
				OrderedDictionary orderedDictionary = new OrderedDictionary();
				orderedDictionary.Add(this._bindings.DependenciesDataBindings.IdField, dependency.ID);
				IOrderedDictionary values = this.TranslateDependencyKeys(dependency.GetData());
				IDependency dependency2 = this.GetDependencies().Find((IDependency x) => object.Equals(x.ID, dependency.ID));
				IOrderedDictionary oldValues = this.TranslateDependencyKeys(dependency2.GetData());
				this.DependenciesView.Update(orderedDictionary, values, oldValues, delegate(int count, Exception ex)
				{
					this._updatedDependencyCompleted.Set();
					return DataSourceViewGanttProvider.OnDataSourceOperationCompleted(count, ex);
				});
				this._updatedDependencyCompleted.WaitOne();
			}
			return dependency;
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x00087DDC File Offset: 0x00085FDC
		public override IDependency DeleteDependency(IDependency dependency)
		{
			if (this.DependenciesView != null && this.DependenciesView.CanDelete)
			{
				this.EnsureDependencyDataFields();
				OrderedDictionary orderedDictionary = new OrderedDictionary();
				orderedDictionary.Add(this._bindings.DependenciesDataBindings.IdField, dependency.ID);
				IOrderedDictionary oldValues = this.TranslateDependencyKeys(dependency.GetData());
				this.DependenciesView.Delete(orderedDictionary, oldValues, delegate(int count, Exception ex)
				{
					this._deletedDependencyCompleted.Set();
					return DataSourceViewGanttProvider.OnDataSourceOperationCompleted(count, ex);
				});
				this._deletedDependencyCompleted.WaitOne();
			}
			return dependency;
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x00087E78 File Offset: 0x00086078
		public override IDependency InsertDependency(IDependency dependency)
		{
			if (this.DependenciesView != null && this.DependenciesView.CanInsert)
			{
				this.EnsureDependencyDataFields();
				HashSet<object> hashSet = new HashSet<object>();
				foreach (IDependency dependency2 in this.GetDependencies())
				{
					hashSet.Add(dependency2.ID);
				}
				IOrderedDictionary orderedDictionary = this.TranslateDependencyKeys(dependency.GetData());
				orderedDictionary.Remove("ID");
				this.DependenciesView.Insert(orderedDictionary, delegate(int count, Exception ex)
				{
					this._insertedDependencyCompleted.Set();
					return DataSourceViewGanttProvider.OnDataSourceOperationCompleted(count, ex);
				});
				this._insertedDependencyCompleted.WaitOne();
				IDependency result = null;
				foreach (IDependency dependency3 in this.GetDependencies())
				{
					if (!hashSet.Contains(dependency3.ID))
					{
						result = dependency3;
						break;
					}
				}
				return result;
			}
			return dependency;
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x00087FB0 File Offset: 0x000861B0
		public override List<IResource> GetResources()
		{
			List<IResource> list = new List<IResource>();
			if (this.ResourcesView == null)
			{
				return list;
			}
			this.EnsureResourcesDataFields();
			this.ResourcesView.Select(DataSourceSelectArguments.Empty, delegate(IEnumerable data)
			{
				this._selectedResourcesData = data;
				this._selectedResourcesCompleted.Set();
			});
			this._selectedResourcesCompleted.WaitOne();
			if (this._selectedResourcesData == null)
			{
				return null;
			}
			foreach (object dataItem in this._selectedResourcesData)
			{
				list.Add(ResourcesBinder.BindResource(dataItem, this._bindings.ResourcesDataBindings));
			}
			return list;
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x00088078 File Offset: 0x00086278
		public override List<IAssignment> GetAssignments()
		{
			List<IAssignment> list = new List<IAssignment>();
			if (this.AssignmentsView == null)
			{
				return list;
			}
			this.EnsureAssignmentsDataFields();
			this.AssignmentsView.Select(DataSourceSelectArguments.Empty, delegate(IEnumerable data)
			{
				this._selectedAssignmentsData = data;
				this._selectedAssignmentsCompleted.Set();
			});
			this._selectedAssignmentsCompleted.WaitOne();
			if (this._selectedAssignmentsData == null)
			{
				return null;
			}
			foreach (object dataItem in this._selectedAssignmentsData)
			{
				list.Add(AssignmentsBinder.BindAssignments(dataItem, this._bindings.AssignmentsDataBindings));
			}
			return list;
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x00088160 File Offset: 0x00086360
		public override IAssignment UpdateAssignment(IAssignment assignment)
		{
			if (this.AssignmentsView != null && this.AssignmentsView.CanUpdate)
			{
				this.EnsureAssignmentsDataFields();
				OrderedDictionary keys = new OrderedDictionary
				{
					{
						this._bindings.AssignmentsDataBindings.IdField,
						assignment.ID
					}
				};
				IOrderedDictionary values = this.TranslateAssignmentKeys(assignment.GetData());
				IAssignment assignment2 = this.GetAssignments().Find((IAssignment x) => object.Equals(x.ID, assignment.ID));
				IOrderedDictionary oldValues = this.TranslateAssignmentKeys(assignment2.GetData());
				this.AssignmentsView.Update(keys, values, oldValues, delegate(int count, Exception ex)
				{
					this._updatedAssignmentCompleted.Set();
					return DataSourceViewGanttProvider.OnDataSourceOperationCompleted(count, ex);
				});
				this._updatedAssignmentCompleted.WaitOne();
			}
			return assignment;
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x00088264 File Offset: 0x00086464
		public override IAssignment DeleteAssignment(IAssignment assignment)
		{
			if (this.AssignmentsView != null && this.AssignmentsView.CanDelete)
			{
				this.EnsureAssignmentsDataFields();
				OrderedDictionary keys = new OrderedDictionary
				{
					{
						this._bindings.AssignmentsDataBindings.IdField,
						assignment.ID
					}
				};
				IOrderedDictionary oldValues = this.TranslateAssignmentKeys(assignment.GetData());
				this.AssignmentsView.Delete(keys, oldValues, delegate(int count, Exception ex)
				{
					this._deletedAssignmentCompleted.Set();
					return DataSourceViewGanttProvider.OnDataSourceOperationCompleted(count, ex);
				});
				this._deletedAssignmentCompleted.WaitOne();
			}
			return assignment;
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x00088300 File Offset: 0x00086500
		public override IAssignment InsertAssignment(IAssignment assignment)
		{
			if (this.AssignmentsView != null && this.AssignmentsView.CanInsert)
			{
				this.EnsureAssignmentsDataFields();
				HashSet<object> hashSet = new HashSet<object>();
				foreach (IAssignment assignment2 in this.GetAssignments())
				{
					hashSet.Add(assignment2.ID);
				}
				IOrderedDictionary orderedDictionary = this.TranslateAssignmentKeys(assignment.GetData());
				orderedDictionary.Remove("ID");
				this.AssignmentsView.Insert(orderedDictionary, delegate(int count, Exception ex)
				{
					this._insertedAssignmentCompleted.Set();
					return DataSourceViewGanttProvider.OnDataSourceOperationCompleted(count, ex);
				});
				this._insertedAssignmentCompleted.WaitOne();
				IAssignment result = null;
				foreach (IAssignment assignment3 in this.GetAssignments())
				{
					if (!hashSet.Contains(assignment3.ID))
					{
						result = assignment3;
						break;
					}
				}
				return result;
			}
			return assignment;
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x00088420 File Offset: 0x00086620
		protected internal virtual void EnsureTaskDataFields()
		{
			this._bindings.TasksDataBindings.EnsureDataFields();
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x00088432 File Offset: 0x00086632
		protected internal virtual void EnsureDependencyDataFields()
		{
			this._bindings.DependenciesDataBindings.EnsureDataFields();
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x00088444 File Offset: 0x00086644
		protected internal virtual void EnsureResourcesDataFields()
		{
			this._bindings.ResourcesDataBindings.EnsureDataFields();
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x00088456 File Offset: 0x00086656
		protected internal virtual void EnsureAssignmentsDataFields()
		{
			this._bindings.AssignmentsDataBindings.EnsureDataFields();
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x00088468 File Offset: 0x00086668
		protected internal virtual IOrderedDictionary TranslateTaskKeys(IOrderedDictionary data)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			TasksDataBindings tasksDataBindings = this._bindings.TasksDataBindings;
			foreach (object obj in data.Keys)
			{
				string text = (string)obj;
				if (!orderedDictionary.Contains(text))
				{
					string key;
					switch (key = text)
					{
					case "ID":
						orderedDictionary.Add(tasksDataBindings.IdField, data[text]);
						continue;
					case "ParentID":
						if (!string.IsNullOrEmpty(tasksDataBindings.ParentIdField))
						{
							orderedDictionary.Add(tasksDataBindings.ParentIdField, data[text]);
							continue;
						}
						continue;
					case "OrderID":
						if (!string.IsNullOrEmpty(tasksDataBindings.OrderIdField))
						{
							orderedDictionary.Add(tasksDataBindings.OrderIdField, data[text]);
							continue;
						}
						continue;
					case "Summary":
						if (!string.IsNullOrEmpty(tasksDataBindings.SummaryField))
						{
							orderedDictionary.Add(tasksDataBindings.SummaryField, data[text]);
							continue;
						}
						continue;
					case "Start":
						orderedDictionary.Add(tasksDataBindings.StartField, data[text]);
						continue;
					case "PlannedStart":
						if (!string.IsNullOrEmpty(tasksDataBindings.PlannedStartField))
						{
							orderedDictionary.Add(tasksDataBindings.PlannedStartField, data[text]);
							continue;
						}
						continue;
					case "End":
						orderedDictionary.Add(tasksDataBindings.EndField, data[text]);
						continue;
					case "PlannedEnd":
						if (!string.IsNullOrEmpty(tasksDataBindings.PlannedEndField))
						{
							orderedDictionary.Add(tasksDataBindings.PlannedEndField, data[text]);
							continue;
						}
						continue;
					case "PercentComplete":
						if (!string.IsNullOrEmpty(tasksDataBindings.PercentCompleteField))
						{
							orderedDictionary.Add(tasksDataBindings.PercentCompleteField, data[text]);
							continue;
						}
						continue;
					case "Title":
						orderedDictionary.Add(tasksDataBindings.TitleField, data[text]);
						continue;
					case "Expanded":
						if (!string.IsNullOrEmpty(tasksDataBindings.ExpandedField))
						{
							orderedDictionary.Add(tasksDataBindings.ExpandedField, data[text]);
							continue;
						}
						continue;
					}
					orderedDictionary.Add(text, orderedDictionary[text]);
				}
			}
			return orderedDictionary;
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x00088758 File Offset: 0x00086958
		protected internal virtual IOrderedDictionary TranslateDependencyKeys(IOrderedDictionary data)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			DependenciesDataBindings dependenciesDataBindings = this._bindings.DependenciesDataBindings;
			foreach (object obj in data.Keys)
			{
				string text = (string)obj;
				if (!orderedDictionary.Contains(text))
				{
					string a;
					if ((a = text) != null)
					{
						if (a == "ID")
						{
							orderedDictionary.Add(dependenciesDataBindings.IdField, data[text]);
							continue;
						}
						if (a == "PredecessorID")
						{
							orderedDictionary.Add(dependenciesDataBindings.PredecessorIdField, data[text]);
							continue;
						}
						if (a == "SuccessorID")
						{
							orderedDictionary.Add(dependenciesDataBindings.SuccessorIdField, data[text]);
							continue;
						}
						if (a == "Type")
						{
							orderedDictionary.Add(dependenciesDataBindings.TypeField, data[text]);
							continue;
						}
					}
					orderedDictionary.Add(text, orderedDictionary[text]);
				}
			}
			return orderedDictionary;
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x00088878 File Offset: 0x00086A78
		protected internal virtual IOrderedDictionary TranslateAssignmentKeys(IOrderedDictionary data)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			AssignmentsDataBindings assignmentsDataBindings = this._bindings.AssignmentsDataBindings;
			foreach (object obj in data.Keys)
			{
				string text = (string)obj;
				string a;
				if (!orderedDictionary.Contains(text) && (a = text) != null)
				{
					if (!(a == "ID"))
					{
						if (!(a == "TaskID"))
						{
							if (!(a == "ResourceID"))
							{
								if (a == "Units")
								{
									orderedDictionary.Add(assignmentsDataBindings.UnitsField, data[text]);
								}
							}
							else
							{
								orderedDictionary.Add(assignmentsDataBindings.ResourceIdField, data[text]);
							}
						}
						else
						{
							orderedDictionary.Add(assignmentsDataBindings.TaskIdField, data[text]);
						}
					}
					else
					{
						orderedDictionary.Add(assignmentsDataBindings.IdField, data[text]);
					}
				}
			}
			return orderedDictionary;
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x00088988 File Offset: 0x00086B88
		protected static bool OnDataSourceOperationCompleted(int count, Exception ex)
		{
			if (ex != null)
			{
				throw ex;
			}
			return true;
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x00088990 File Offset: 0x00086B90
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._selectedTasksCompleted.Close();
				this._insertedTaskCompleted.Close();
				this._updatedTaskCompleted.Close();
				this._deletedTaskCompleted.Close();
				this._selectDependenciesCompleted.Close();
				this._insertedDependencyCompleted.Close();
				this._updatedDependencyCompleted.Close();
				this._deletedDependencyCompleted.Close();
				this._selectedResourcesCompleted.Close();
				this._selectedAssignmentsCompleted.Close();
				this._insertedAssignmentCompleted.Close();
				this._updatedAssignmentCompleted.Close();
				this._deletedAssignmentCompleted.Close();
			}
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x00088A32 File Offset: 0x00086C32
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x04000AE3 RID: 2787
		private readonly IGantt _gantt;

		// Token: 0x04000AE4 RID: 2788
		private readonly GanttDataBindings _bindings;

		// Token: 0x04000AE5 RID: 2789
		private readonly AutoResetEvent _selectedTasksCompleted = new AutoResetEvent(false);

		// Token: 0x04000AE6 RID: 2790
		private readonly AutoResetEvent _insertedTaskCompleted = new AutoResetEvent(false);

		// Token: 0x04000AE7 RID: 2791
		private readonly AutoResetEvent _updatedTaskCompleted = new AutoResetEvent(false);

		// Token: 0x04000AE8 RID: 2792
		private readonly AutoResetEvent _deletedTaskCompleted = new AutoResetEvent(false);

		// Token: 0x04000AE9 RID: 2793
		private readonly AutoResetEvent _selectDependenciesCompleted = new AutoResetEvent(false);

		// Token: 0x04000AEA RID: 2794
		private readonly AutoResetEvent _insertedDependencyCompleted = new AutoResetEvent(false);

		// Token: 0x04000AEB RID: 2795
		private readonly AutoResetEvent _updatedDependencyCompleted = new AutoResetEvent(false);

		// Token: 0x04000AEC RID: 2796
		private readonly AutoResetEvent _deletedDependencyCompleted = new AutoResetEvent(false);

		// Token: 0x04000AED RID: 2797
		private readonly AutoResetEvent _selectedResourcesCompleted = new AutoResetEvent(false);

		// Token: 0x04000AEE RID: 2798
		private readonly AutoResetEvent _selectedAssignmentsCompleted = new AutoResetEvent(false);

		// Token: 0x04000AEF RID: 2799
		private readonly AutoResetEvent _insertedAssignmentCompleted = new AutoResetEvent(false);

		// Token: 0x04000AF0 RID: 2800
		private readonly AutoResetEvent _updatedAssignmentCompleted = new AutoResetEvent(false);

		// Token: 0x04000AF1 RID: 2801
		private readonly AutoResetEvent _deletedAssignmentCompleted = new AutoResetEvent(false);

		// Token: 0x04000AF2 RID: 2802
		private IEnumerable _selectedTasksData;

		// Token: 0x04000AF3 RID: 2803
		private IEnumerable _selectedDependenciesData;

		// Token: 0x04000AF4 RID: 2804
		private IEnumerable _selectedResourcesData;

		// Token: 0x04000AF5 RID: 2805
		private IEnumerable _selectedAssignmentsData;
	}
}
