using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200030A RID: 778
	internal class PostbackCommandConverter : JavaScriptConverter
	{
		// Token: 0x06001A66 RID: 6758 RVA: 0x0005612E File Offset: 0x0005432E
		public PostbackCommandConverter(ITaskFactory taskFactory)
		{
			this.taskFactory = taskFactory;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00056380 File Offset: 0x00054580
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			PostbackCommand command = new PostbackCommand();
			if (dictionary.ContainsKey("commandName"))
			{
				command.Command = (CommandType)Enum.Parse(typeof(CommandType), (string)dictionary["commandName"]);
			}
			Action<string, Action<ITask>> action4 = delegate(string key, Action<ITask> action)
			{
				if (dictionary.ContainsKey(key))
				{
					ArrayList arrayList = (ArrayList)dictionary[key];
					foreach (object obj in arrayList)
					{
						Task task = this.taskFactory.CreateTask();
						task.LoadFromDictionary((Dictionary<string, object>)obj);
						action(task);
					}
				}
			};
			action4("InsertTask", delegate(ITask task)
			{
				command.InsertedTasks.Add(task);
			});
			action4("UpdateTask", delegate(ITask task)
			{
				command.UpdatedTasks.Add(task);
			});
			action4("DeleteTask", delegate(ITask task)
			{
				command.DeletedTasks.Add(task);
			});
			Action<string, Action<IDependency>> action2 = delegate(string key, Action<IDependency> action)
			{
				if (dictionary.ContainsKey(key))
				{
					ArrayList arrayList = (ArrayList)dictionary[key];
					foreach (object obj in arrayList)
					{
						Dependency dependency = new Dependency();
						dependency.LoadFromDictionary((Dictionary<string, object>)obj);
						action(dependency);
					}
				}
			};
			action2("InsertDependency", delegate(IDependency dependency)
			{
				command.InsertedDependencies.Add(dependency);
			});
			action2("DeleteDependency", delegate(IDependency dependency)
			{
				command.DeletedDependencies.Add(dependency);
			});
			Action<string, Action<IAssignment>> action3 = delegate(string key, Action<IAssignment> action)
			{
				if (dictionary.ContainsKey(key))
				{
					ArrayList arrayList = (ArrayList)dictionary[key];
					foreach (object obj in arrayList)
					{
						Assignment assignment = new Assignment();
						assignment.LoadFromDictionary((Dictionary<string, object>)obj);
						action(assignment);
					}
				}
			};
			action3("InsertAssignment", delegate(IAssignment assignment)
			{
				command.InsertedAssignments.Add(assignment);
			});
			action3("UpdateAssignment", delegate(IAssignment assignment)
			{
				command.UpdatedAssignments.Add(assignment);
			});
			action3("DeleteAssignment", delegate(IAssignment assignment)
			{
				command.DeletedAssignments.Add(assignment);
			});
			return command;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x000564D7 File Offset: 0x000546D7
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x000564E0 File Offset: 0x000546E0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(PostbackCommand)
				};
			}
		}

		// Token: 0x040006BD RID: 1725
		private readonly ITaskFactory taskFactory;
	}
}
