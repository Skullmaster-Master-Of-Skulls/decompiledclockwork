using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x020011C4 RID: 4548
	internal class AppointmentConverter : EditorConverterBase
	{
		// Token: 0x17003CD6 RID: 15574
		// (get) Token: 0x0600BBF5 RID: 48117 RVA: 0x0029A40C File Offset: 0x0029860C
		// (set) Token: 0x0600BBF6 RID: 48118 RVA: 0x0029A414 File Offset: 0x00298614
		private IAppointmentFactory AppointmentFactory
		{
			get
			{
				return this._appointmentFactory;
			}
			set
			{
				this._appointmentFactory = value;
			}
		}

		// Token: 0x0600BBF7 RID: 48119 RVA: 0x0029A41D File Offset: 0x0029861D
		public AppointmentConverter(IAppointmentFactory appointmentFactory)
		{
			this.AppointmentFactory = appointmentFactory;
		}

		// Token: 0x0600BBF8 RID: 48120 RVA: 0x0029A42C File Offset: 0x0029862C
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			if (type != typeof(Appointment))
			{
				throw new ArgumentException("Can deserialize only Appointment objects.");
			}
			Appointment appointment = this.AppointmentFactory.CreateAppointment();
			appointment.Subject = dictionary["Subject"].ToString();
			if (dictionary.ContainsKey("TimeZoneID"))
			{
				appointment.TimeZoneID = dictionary["TimeZoneID"].ToString();
			}
			IDictionary<string, object> dictionary2 = serializer.ConvertToType<IDictionary<string, object>>(dictionary["Attributes"]);
			foreach (KeyValuePair<string, object> keyValuePair in dictionary2)
			{
				appointment.Attributes.Add(keyValuePair.Key, keyValuePair.Value.ToString());
			}
			IList<Resource> list = serializer.ConvertToType<IList<Resource>>(dictionary["Resources"]);
			foreach (Resource item in list)
			{
				appointment.Resources.Add(item);
			}
			return appointment;
		}

		// Token: 0x0600BBF9 RID: 48121 RVA: 0x0029A55C File Offset: 0x0029875C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Appointment appointment = obj as Appointment;
			if (appointment == null)
			{
				throw new ArgumentException("Can serialize only Appointment objects.");
			}
			IDictionary<string, object> dictionary = base.Serialize(obj, serializer);
			if (dictionary.ContainsKey("id"))
			{
				dictionary["internalID"] = LosSerializer.Serialize(dictionary["id"]);
			}
			if (appointment.Owner != null)
			{
				dictionary["start"] = appointment.Owner.UtcToDisplay(appointment.Start).ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
				dictionary["end"] = appointment.Owner.UtcToDisplay(appointment.End).ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
			}
			else
			{
				dictionary["start"] = appointment.Start.ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
				dictionary["end"] = appointment.End.ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
			}
			SchedulerAttributeCollectionConverter schedulerAttributeCollectionConverter = new SchedulerAttributeCollectionConverter();
			IDictionary<string, object> dictionary2 = schedulerAttributeCollectionConverter.Serialize(appointment.Attributes, serializer);
			if (dictionary2.Count > 0)
			{
				dictionary.Add("attributes", dictionary2);
			}
			ResourceConverter resourceConverter = new ResourceConverter();
			if (appointment.Resources.Count > 0)
			{
				List<IDictionary<string, object>> list = new List<IDictionary<string, object>>();
				foreach (object obj2 in appointment.Resources)
				{
					Resource resource = (Resource)obj2;
					if (!appointment.Owner.Resources.Contains(resource))
					{
						list.Add(resourceConverter.Serialize(resource, serializer));
					}
					else
					{
						list.Add(new Dictionary<string, object>
						{
							{
								"key",
								resource.Key
							},
							{
								"type",
								resource.Type
							}
						});
					}
				}
				dictionary.Add("resources", list);
			}
			ReminderConverter reminderConverter = new ReminderConverter();
			if (appointment.Reminders.Count > 0)
			{
				List<IDictionary<string, object>> list2 = new List<IDictionary<string, object>>();
				foreach (object obj3 in appointment.Reminders)
				{
					list2.Add(reminderConverter.Serialize(obj3, serializer));
				}
				dictionary.Add("reminders", list2);
			}
			if (appointment.Owner != null)
			{
				bool flag = (bool)dictionary["allowEdit"];
				if (flag == appointment.Owner.AllowEdit)
				{
					dictionary.Remove("allowEdit");
				}
				bool flag2 = (bool)dictionary["allowDelete"];
				if (flag2 == appointment.Owner.AllowDelete)
				{
					dictionary.Remove("allowDelete");
				}
			}
			return dictionary;
		}

		// Token: 0x17003CD7 RID: 15575
		// (get) Token: 0x0600BBFA RID: 48122 RVA: 0x0029A840 File Offset: 0x00298A40
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Appointment)
				};
			}
		}

		// Token: 0x04003163 RID: 12643
		public const string JavaScriptDateFormat = "yyyy/MM/dd HH:mm";

		// Token: 0x04003164 RID: 12644
		private IAppointmentFactory _appointmentFactory;
	}
}
