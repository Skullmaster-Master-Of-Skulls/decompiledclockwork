using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000350 RID: 848
	internal class CallbackCommandConverter : JavaScriptConverter
	{
		// Token: 0x06001D5B RID: 7515 RVA: 0x0005C915 File Offset: 0x0005AB15
		public CallbackCommandConverter(ITaskFactory taskFactory)
		{
			this.taskFactory = taskFactory;
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x0005C924 File Offset: 0x0005AB24
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			CallbackCommand callbackCommand = new CallbackCommand();
			callbackCommand.Command = (CommandType)Enum.Parse(typeof(CommandType), (string)dictionary["commandName"]);
			if (dictionary.ContainsKey("tasks"))
			{
				ArrayList arrayList = (ArrayList)dictionary["tasks"];
				foreach (object obj in arrayList)
				{
					Task task = this.taskFactory.CreateTask();
					task.LoadFromDictionary((Dictionary<string, object>)obj);
					callbackCommand.Tasks.Add(task);
				}
			}
			if (dictionary.ContainsKey("dependencies"))
			{
				ArrayList arrayList2 = (ArrayList)dictionary["dependencies"];
				foreach (object obj2 in arrayList2)
				{
					Dependency dependency = new Dependency();
					dependency.LoadFromDictionary((Dictionary<string, object>)obj2);
					callbackCommand.Dependencies.Add(dependency);
				}
			}
			if (dictionary.ContainsKey("assignments"))
			{
				ArrayList arrayList3 = (ArrayList)dictionary["assignments"];
				foreach (object obj3 in arrayList3)
				{
					Assignment assignment = new Assignment();
					assignment.LoadFromDictionary((Dictionary<string, object>)obj3);
					callbackCommand.Assignments.Add(assignment);
				}
			}
			return callbackCommand;
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0005CAE8 File Offset: 0x0005ACE8
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x0005CAF0 File Offset: 0x0005ACF0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(CallbackCommand)
				};
			}
		}

		// Token: 0x04000758 RID: 1880
		private readonly ITaskFactory taskFactory;
	}
}
