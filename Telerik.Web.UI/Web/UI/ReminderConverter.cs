using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI
{
	// Token: 0x02000F9C RID: 3996
	internal class ReminderConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06009905 RID: 39173 RVA: 0x00222220 File Offset: 0x00220420
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Reminder reminder = obj as Reminder;
			if (reminder == null)
			{
				throw new ArgumentException("Can serialize only Reminder objects.");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("id", reminder.ID);
			dictionary.Add("trigger", (int)reminder.Trigger.TotalMinutes);
			SchedulerAttributeCollectionConverter schedulerAttributeCollectionConverter = new SchedulerAttributeCollectionConverter();
			IDictionary<string, object> dictionary2 = schedulerAttributeCollectionConverter.Serialize(reminder.Attributes, serializer);
			if (dictionary2.Count > 0)
			{
				dictionary.Add("attributes", dictionary2);
			}
			return dictionary;
		}

		// Token: 0x17003070 RID: 12400
		// (get) Token: 0x06009906 RID: 39174 RVA: 0x002222A4 File Offset: 0x002204A4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Reminder)
				};
			}
		}
	}
}
