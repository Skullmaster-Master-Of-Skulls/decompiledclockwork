using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200030D RID: 781
	internal class WebServiceSettingsConverter : WebServiceSettingsConverter
	{
		// Token: 0x06001A72 RID: 6770 RVA: 0x0005666C File Offset: 0x0005486C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			WebServiceSettings webServiceSettings = obj as WebServiceSettings;
			if (webServiceSettings == null)
			{
				throw new InvalidOperationException("Can serialize only WebServiceSettings objects.");
			}
			IDictionary<string, object> dictionary = base.Serialize(obj, serializer);
			if (dictionary.ContainsKey("method") && dictionary["method"].Equals("GetTasks"))
			{
				dictionary.Remove("method");
			}
			ExplicitJavaScriptConverter.AddProperty(dictionary, "deleteTasksMethod", webServiceSettings.DeleteTasksMethod, "DeleteTasks");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "insertTasksMethod", webServiceSettings.InsertTasksMethod, "InsertTasks");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "updateTasksMethod", webServiceSettings.UpdateTasksMethod, "UpdateTasks");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "getDependenciesMethod", webServiceSettings.GetDependenciesMethod, "GetDependencies");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "deleteDependenciesMethod", webServiceSettings.DeleteDependenciesMethod, "DeleteDependencies");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "insertDependenciesMethod", webServiceSettings.InsertDependenciesMethod, "InsertDependencies");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "getResourcesMethod", webServiceSettings.GetResourcesMethod, "GetResources");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "getAssignmentsMethod", webServiceSettings.GetAssignmentsMethod, "GetAssignments");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "insertAssignmentsMethod", webServiceSettings.InsertAssignmentsMethod, "InsertAssignments");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "updateAssignmentsMethod", webServiceSettings.UpdateAssignmentsMethod, "UpdateAssignments");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "deleteAssignmentsMethod", webServiceSettings.DeleteAssignmentsMethod, "DeleteAssignments");
			return dictionary;
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x000567BC File Offset: 0x000549BC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(WebServiceSettings)
				};
			}
		}
	}
}
