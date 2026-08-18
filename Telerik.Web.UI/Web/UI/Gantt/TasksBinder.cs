using System;
using System.Web.UI;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200049C RID: 1180
	public class TasksBinder
	{
		// Token: 0x060029E9 RID: 10729 RVA: 0x000870B0 File Offset: 0x000852B0
		public static Task BindTask(object dataItem, ITasksDataBindings bindings)
		{
			Task task = new Task();
			TasksBinder.InitField(delegate(object value)
			{
				task.ID = value;
			}, DataBinder.Eval(dataItem, bindings.IdField));
			TasksBinder.InitField(delegate(object value)
			{
				task.Start = DateHelper.AssumeUtc((DateTime)value);
			}, DataBinder.Eval(dataItem, bindings.StartField));
			if (!string.IsNullOrEmpty(bindings.PlannedStartField))
			{
				TasksBinder.InitField(delegate(object value)
				{
					task.PlannedStart = ((value == null) ? ((DateTime?)value) : new DateTime?(DateHelper.AssumeUtc(((DateTime?)value).Value)));
				}, DataBinder.Eval(dataItem, bindings.PlannedStartField));
			}
			TasksBinder.InitField(delegate(object value)
			{
				task.End = DateHelper.AssumeUtc((DateTime)value);
			}, DataBinder.Eval(dataItem, bindings.EndField));
			if (!string.IsNullOrEmpty(bindings.PlannedEndField))
			{
				TasksBinder.InitField(delegate(object value)
				{
					task.PlannedEnd = ((value == null) ? ((DateTime?)value) : new DateTime?(DateHelper.AssumeUtc(((DateTime?)value).Value)));
				}, DataBinder.Eval(dataItem, bindings.PlannedEndField));
			}
			TasksBinder.InitField(delegate(object value)
			{
				task.Title = (string)value;
			}, DataBinder.Eval(dataItem, bindings.TitleField));
			if (!string.IsNullOrEmpty(bindings.OrderIdField))
			{
				TasksBinder.InitField(delegate(object value)
				{
					task.OrderID = value;
				}, DataBinder.Eval(dataItem, bindings.OrderIdField));
			}
			if (!string.IsNullOrEmpty(bindings.ParentIdField))
			{
				TasksBinder.InitField(delegate(object value)
				{
					task.ParentID = value;
				}, DataBinder.Eval(dataItem, bindings.ParentIdField));
			}
			if (!string.IsNullOrEmpty(bindings.SummaryField))
			{
				TasksBinder.InitField(delegate(object value)
				{
					task.Summary = (bool)value;
				}, DataBinder.Eval(dataItem, bindings.SummaryField));
			}
			if (!string.IsNullOrEmpty(bindings.PercentCompleteField))
			{
				TasksBinder.InitField(delegate(object value)
				{
					task.PercentComplete = (decimal)value;
				}, DataBinder.Eval(dataItem, bindings.PercentCompleteField));
			}
			if (!string.IsNullOrEmpty(bindings.ExpandedField))
			{
				TasksBinder.InitField(delegate(object value)
				{
					task.Expanded = (bool)value;
				}, DataBinder.Eval(dataItem, bindings.ExpandedField));
			}
			return task;
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000872B9 File Offset: 0x000854B9
		private static void InitField(Action<object> action, object data)
		{
			action(TasksBinder.DbNullOrNull(data) ? null : data);
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000872CD File Offset: 0x000854CD
		private static bool DbNullOrNull(object value)
		{
			return value == null || value == DBNull.Value;
		}
	}
}
