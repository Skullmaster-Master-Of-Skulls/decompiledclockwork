using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001A28 RID: 6696
	internal class SchedulerWebServiceSettingsConverter : WebServiceSettingsConverter
	{
		// Token: 0x06010424 RID: 66596 RVA: 0x003A2374 File Offset: 0x003A0574
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			SchedulerWebServiceSettings schedulerWebServiceSettings = obj as SchedulerWebServiceSettings;
			if (schedulerWebServiceSettings == null)
			{
				throw new InvalidOperationException("Can serialize only SchedulerWebServiceSettings objects.");
			}
			IDictionary<string, object> dictionary = base.Serialize(obj, serializer);
			if (dictionary["method"].Equals("GetAppointments"))
			{
				dictionary.Remove("method");
			}
			ExplicitJavaScriptConverter.AddProperty(dictionary, "updateMode", schedulerWebServiceSettings.UpdateMode, AppointmentUpdateMode.Batch);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "deleteAppointmentMethod", schedulerWebServiceSettings.DeleteAppointmentMethod, "DeleteAppointment");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "insertAppointmentMethod", schedulerWebServiceSettings.InsertAppointmentMethod, "InsertAppointment");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "updateAppointmentMethod", schedulerWebServiceSettings.UpdateAppointmentMethod, "UpdateAppointment");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "getResourcesMethod", schedulerWebServiceSettings.GetResourcesMethod, "GetResources");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "createRecurrenceExceptionMethod", schedulerWebServiceSettings.CreateRecurrenceExceptionMethod, "CreateRecurrenceException");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "removeRecurrenceExceptionsMethod", schedulerWebServiceSettings.RemoveRecurrenceExceptionsMethod, "RemoveRecurrenceExceptions");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "resourcesPopulated", schedulerWebServiceSettings.ResourcePopulationMode != SchedulerResourcePopulationMode.ClientSide, true);
			return dictionary;
		}

		// Token: 0x17004EC4 RID: 20164
		// (get) Token: 0x06010425 RID: 66597 RVA: 0x003A2488 File Offset: 0x003A0688
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(SchedulerWebServiceSettings)
				};
			}
		}
	}
}
