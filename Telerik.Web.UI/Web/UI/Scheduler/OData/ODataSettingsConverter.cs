using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Scheduler.OData
{
	// Token: 0x02000E64 RID: 3684
	internal class ODataSettingsConverter : WebServiceSettingsConverter
	{
		// Token: 0x06008BC3 RID: 35779 RVA: 0x001FC344 File Offset: 0x001FA544
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			SchedulerWebServiceSettings schedulerWebServiceSettings = obj as SchedulerWebServiceSettings;
			if (schedulerWebServiceSettings == null)
			{
				throw new InvalidOperationException("Can serialize only WebServiceSettings objects.");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "path", WebServiceSettingsConverter.ResolveUrl(schedulerWebServiceSettings.Path), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "resourcesPopulated", schedulerWebServiceSettings.ResourcePopulationMode != SchedulerResourcePopulationMode.ClientSide, true);
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary2, "DataDescriptionField", schedulerWebServiceSettings.ODataSettings.DataDescriptionField, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary2, "DataEndField", schedulerWebServiceSettings.ODataSettings.DataEndField, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary2, "DataStartField", schedulerWebServiceSettings.ODataSettings.DataStartField, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary2, "DataKeyField", schedulerWebServiceSettings.ODataSettings.DataKeyField, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary2, "DataSubjectField", schedulerWebServiceSettings.ODataSettings.DataSubjectField, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary2, "DataRecurrenceParentKeyField", schedulerWebServiceSettings.ODataSettings.DataRecurrenceParentKeyField, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary2, "DataRecurrenceField", schedulerWebServiceSettings.ODataSettings.DataRecurrenceField, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary2, "DataModelID", schedulerWebServiceSettings.ODataSettings.DataModelID, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary2, "ODataSourceID", schedulerWebServiceSettings.ODataSettings.ODataDataSourceID, string.Empty);
			string value = serializer.Serialize(schedulerWebServiceSettings.ODataSettings.ResourceTypes);
			ExplicitJavaScriptConverter.AddProperty(dictionary2, "resourceTypes", value, null);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "odataSettings", dictionary2, null);
			return dictionary;
		}

		// Token: 0x17002C2F RID: 11311
		// (get) Token: 0x06008BC4 RID: 35780 RVA: 0x001FC59C File Offset: 0x001FA79C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(SchedulerWebServiceSettings);
				yield break;
			}
		}
	}
}
