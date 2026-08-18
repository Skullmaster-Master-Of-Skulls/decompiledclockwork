using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using System.Web.UI;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x020012DB RID: 4827
	internal class DataSourceViewSchedulerProvider : SchedulerProviderBase, IDisposable
	{
		// Token: 0x1700417A RID: 16762
		// (get) Token: 0x0600CAA4 RID: 51876 RVA: 0x002D390D File Offset: 0x002D1B0D
		private DataSourceView _view
		{
			get
			{
				return this._scheduler.DataSourceView;
			}
		}

		// Token: 0x0600CAA5 RID: 51877 RVA: 0x002D391A File Offset: 0x002D1B1A
		public DataSourceViewSchedulerProvider(RadScheduler scheduler)
		{
			this._scheduler = scheduler;
			this._selectCompleted = new AutoResetEvent(false);
		}

		// Token: 0x1700417B RID: 16763
		// (get) Token: 0x0600CAA6 RID: 51878 RVA: 0x002D3935 File Offset: 0x002D1B35
		public override string Name
		{
			get
			{
				return "Integrated";
			}
		}

		// Token: 0x0600CAA7 RID: 51879 RVA: 0x002D393C File Offset: 0x002D1B3C
		public override IEnumerable<Appointment> GetAppointments(RadScheduler owner)
		{
			this.EnsureDataFieldsAreSet();
			this._view.Select(DataSourceSelectArguments.Empty, new DataSourceViewSelectCallback(this.OnDataSourceViewSelectCallback));
			this._selectCompleted.WaitOne();
			if (this._selectedData == null)
			{
				return null;
			}
			List<Appointment> list = new List<Appointment>();
			foreach (object obj in this._selectedData)
			{
				object obj2 = DataBinder.Eval(obj, this._scheduler.DataKeyField);
				if (obj2 != null && obj2 != DBNull.Value)
				{
					object id = obj2;
					obj2 = DataBinder.Eval(obj, this._scheduler.DataStartField);
					if (obj2 != null && obj2 != DBNull.Value)
					{
						DateTime start = DateHelper.AssumeUtc(Convert.ToDateTime(obj2));
						obj2 = DataBinder.Eval(obj, this._scheduler.DataEndField);
						if (obj2 != null && obj2 != DBNull.Value)
						{
							DateTime end = DateHelper.AssumeUtc(Convert.ToDateTime(obj2));
							string timeZoneID = string.Empty;
							if (!string.IsNullOrEmpty(this._scheduler.DataTimeZoneIdField))
							{
								obj2 = DataBinder.Eval(obj, this._scheduler.DataTimeZoneIdField);
								if (obj2 != null && obj2 != DBNull.Value)
								{
									timeZoneID = Convert.ToString(obj2);
								}
							}
							obj2 = DataBinder.Eval(obj, this._scheduler.DataSubjectField);
							if (obj2 != null && obj2 != DBNull.Value)
							{
								string subject = Convert.ToString(obj2);
								string description = string.Empty;
								if (!string.IsNullOrEmpty(this._scheduler.DataDescriptionField))
								{
									obj2 = DataBinder.Eval(obj, this._scheduler.DataDescriptionField);
									if (obj2 != null && obj2 != DBNull.Value)
									{
										description = Convert.ToString(obj2);
									}
								}
								IList<Reminder> list2 = null;
								if (!string.IsNullOrEmpty(this._scheduler.DataReminderField))
								{
									obj2 = DataBinder.Eval(obj, this._scheduler.DataReminderField);
									if (obj2 != null && obj2 != DBNull.Value)
									{
										list2 = Reminder.TryParse(Convert.ToString(obj2));
									}
								}
								string text = string.Empty;
								if (!string.IsNullOrEmpty(this._scheduler.DataRecurrenceField))
								{
									obj2 = DataBinder.Eval(obj, this._scheduler.DataRecurrenceField);
									if (obj2 != null && obj2 != DBNull.Value)
									{
										text = Convert.ToString(obj2);
									}
								}
								object obj3 = null;
								if (!string.IsNullOrEmpty(this._scheduler.DataRecurrenceParentKeyField))
								{
									obj2 = DataBinder.Eval(obj, this._scheduler.DataRecurrenceParentKeyField);
									if (obj2 != null && obj2 != DBNull.Value)
									{
										obj3 = obj2;
									}
								}
								RecurrenceState recurrenceState = RecurrenceState.NotRecurring;
								if (obj3 != null)
								{
									recurrenceState = RecurrenceState.Exception;
								}
								else if (!string.IsNullOrEmpty(text))
								{
									recurrenceState = RecurrenceState.Master;
								}
								Appointment appointment = owner.CreateAppointment();
								appointment.ID = id;
								appointment.Start = start;
								appointment.End = end;
								appointment.Subject = subject;
								appointment.Description = description;
								appointment.RecurrenceRule = text;
								appointment.RecurrenceParentID = obj3;
								appointment.RecurrenceState = recurrenceState;
								appointment.DataItem = obj;
								appointment.TimeZoneID = timeZoneID;
								if (list2 != null)
								{
									appointment.Reminders.AddRange(list2);
								}
								foreach (object obj4 in this._scheduler.ResourceTypes)
								{
									ResourceType resourceType = (ResourceType)obj4;
									obj2 = DataBinder.Eval(obj, resourceType.ForeignKeyField);
									if (obj2 != null && obj2 != DBNull.Value)
									{
										object key = obj2;
										Resource resource = this.GetResource(resourceType.Name, key);
										if (resource != null)
										{
											appointment.Resources.Add(resource);
										}
									}
								}
								PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
								foreach (object obj5 in properties)
								{
									PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj5;
									if (this.IsAttribute(propertyDescriptor.Name))
									{
										object value = propertyDescriptor.GetValue(obj);
										if (value == null)
										{
											appointment.Attributes[propertyDescriptor.Name] = null;
										}
										else
										{
											appointment.Attributes[propertyDescriptor.Name] = value.ToString();
										}
									}
								}
								list.Add(appointment);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600CAA8 RID: 51880 RVA: 0x002D3D98 File Offset: 0x002D1F98
		public override void Insert(RadScheduler owner, Appointment appointmentToInsert)
		{
			if (this._view.CanInsert)
			{
				this.EnsureDataFieldsAreSet();
				appointmentToInsert.Owner = owner;
				IOrderedDictionary orderedDictionary = this.TranslateKeys(appointmentToInsert.GetData());
				DataSourceViewSchedulerProvider.SetMissingAttributesToNull(owner, orderedDictionary);
				this._view.Insert(orderedDictionary, new DataSourceViewOperationCallback(DataSourceViewSchedulerProvider.OnDataSourceOperationComplete));
			}
		}

		// Token: 0x0600CAA9 RID: 51881 RVA: 0x002D3DEC File Offset: 0x002D1FEC
		public override void Update(RadScheduler owner, Appointment appointmentToUpdate)
		{
			if (this._view.CanUpdate)
			{
				this.EnsureDataFieldsAreSet();
				OrderedDictionary orderedDictionary = new OrderedDictionary();
				orderedDictionary.Add(this._scheduler.DataKeyField, appointmentToUpdate.ID);
				IOrderedDictionary orderedDictionary2 = appointmentToUpdate.GetData();
				orderedDictionary2 = this.TranslateKeys(orderedDictionary2);
				IOrderedDictionary orderedDictionary3 = null;
				UpdateAppointmentContext updateAppointmentContext = owner.ProviderContext as UpdateAppointmentContext;
				CreateRecurrenceExceptionContext createRecurrenceExceptionContext = owner.ProviderContext as CreateRecurrenceExceptionContext;
				if (updateAppointmentContext != null)
				{
					orderedDictionary3 = updateAppointmentContext.OriginalAppointment.GetData();
				}
				else if (createRecurrenceExceptionContext != null)
				{
					orderedDictionary3 = createRecurrenceExceptionContext.ParentAppointment.GetData();
				}
				if (orderedDictionary3 != null)
				{
					orderedDictionary3 = this.TranslateKeys(orderedDictionary3);
					DataSourceViewSchedulerProvider.SetMissingAttributesToNull(owner, orderedDictionary3);
				}
				DataSourceViewSchedulerProvider.SetMissingAttributesToNull(owner, orderedDictionary2);
				this._view.Update(orderedDictionary, orderedDictionary2, orderedDictionary3, new DataSourceViewOperationCallback(DataSourceViewSchedulerProvider.OnDataSourceOperationComplete));
			}
		}

		// Token: 0x0600CAAA RID: 51882 RVA: 0x002D3EAC File Offset: 0x002D20AC
		public override void Delete(RadScheduler owner, Appointment appointmentToDelete)
		{
			if (this._view.CanDelete)
			{
				this.EnsureDataFieldsAreSet();
				OrderedDictionary orderedDictionary = new OrderedDictionary();
				orderedDictionary.Add(this._scheduler.DataKeyField, appointmentToDelete.ID);
				IOrderedDictionary orderedDictionary2 = appointmentToDelete.GetData();
				orderedDictionary2 = this.TranslateKeys(orderedDictionary2);
				this._view.Delete(orderedDictionary, orderedDictionary2, new DataSourceViewOperationCallback(DataSourceViewSchedulerProvider.OnDataSourceOperationComplete));
			}
		}

		// Token: 0x0600CAAB RID: 51883 RVA: 0x002D3F14 File Offset: 0x002D2114
		public override IEnumerable<ResourceType> GetResourceTypes(RadScheduler owner)
		{
			ResourceType[] array = new ResourceType[this._scheduler.ResourceTypes.Count];
			this._scheduler.ResourceTypes.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600CAAC RID: 51884 RVA: 0x002D3F4A File Offset: 0x002D214A
		public override IEnumerable<Resource> GetResourcesByType(RadScheduler owner, string resourceType)
		{
			return this._scheduler.Resources.GetResourcesByType(resourceType);
		}

		// Token: 0x0600CAAD RID: 51885 RVA: 0x002D3F5D File Offset: 0x002D215D
		private void OnDataSourceViewSelectCallback(IEnumerable data)
		{
			this._selectedData = data;
			this._selectCompleted.Set();
		}

		// Token: 0x0600CAAE RID: 51886 RVA: 0x002D3F72 File Offset: 0x002D2172
		private static bool OnDataSourceOperationComplete(int count, Exception e)
		{
			if (e != null)
			{
				throw e;
			}
			return true;
		}

		// Token: 0x0600CAAF RID: 51887 RVA: 0x002D3F7C File Offset: 0x002D217C
		private void EnsureDataFieldsAreSet()
		{
			if (string.IsNullOrEmpty(this._scheduler.DataKeyField) || string.IsNullOrEmpty(this._scheduler.DataStartField) || string.IsNullOrEmpty(this._scheduler.DataEndField) || string.IsNullOrEmpty(this._scheduler.DataSubjectField))
			{
				throw new ArgumentException("DataKeyField, DataSubjectField, DataStartField and DataEndField are required for databinding");
			}
			if ((!string.IsNullOrEmpty(this._scheduler.DataRecurrenceField) && string.IsNullOrEmpty(this._scheduler.DataRecurrenceParentKeyField)) || (string.IsNullOrEmpty(this._scheduler.DataRecurrenceField) && !string.IsNullOrEmpty(this._scheduler.DataRecurrenceParentKeyField)))
			{
				throw new ArgumentException("DataRecurrenceField and DataRecurrenceParentKeyField must be set simultaneously.");
			}
		}

		// Token: 0x0600CAB0 RID: 51888 RVA: 0x002D4054 File Offset: 0x002D2254
		private Resource GetResource(string type, object key)
		{
			List<Resource> list = (List<Resource>)this.GetResourcesByType(this._scheduler, type);
			return list.Find((Resource res) => key != null && res.Key.Equals(key));
		}

		// Token: 0x0600CAB1 RID: 51889 RVA: 0x002D4094 File Offset: 0x002D2294
		private IOrderedDictionary TranslateKeys(IDictionary data)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			foreach (object obj in data.Keys)
			{
				string text = (string)obj;
				if (!orderedDictionary.Contains(text))
				{
					string key;
					switch (key = text)
					{
					case "Subject":
						orderedDictionary.Add(this._scheduler.DataSubjectField, data[text]);
						continue;
					case "$$Description$$":
						orderedDictionary.Add(this._scheduler.DataDescriptionField, data[text]);
						continue;
					case "Start":
						orderedDictionary.Add(this._scheduler.DataStartField, data[text]);
						continue;
					case "End":
						orderedDictionary.Add(this._scheduler.DataEndField, data[text]);
						continue;
					case "RecurrenceRule":
						if (this._scheduler.RecurrenceSupport)
						{
							orderedDictionary.Add(this._scheduler.DataRecurrenceField, data[text]);
							continue;
						}
						continue;
					case "RecurrenceParentID":
						if (this._scheduler.RecurrenceSupport)
						{
							orderedDictionary.Add(this._scheduler.DataRecurrenceParentKeyField, data[text]);
							continue;
						}
						continue;
					case "$$Reminders$$":
						if (this._scheduler.RemindersSupport)
						{
							orderedDictionary.Add(this._scheduler.DataReminderField, data[text]);
							continue;
						}
						continue;
					case "TimeZoneID":
						if (!string.IsNullOrEmpty(this._scheduler.DataTimeZoneIdField) && !string.IsNullOrEmpty((string)data[text]))
						{
							orderedDictionary.Add(this._scheduler.DataTimeZoneIdField, data[text]);
							continue;
						}
						continue;
					}
					string a = data[text] as string;
					bool flag = a == string.Empty;
					ResourceType resourceType = this._scheduler.ResourceTypes.FindByName(text);
					if (resourceType != null)
					{
						if (!orderedDictionary.Contains(resourceType.ForeignKeyField))
						{
							orderedDictionary.Add(resourceType.ForeignKeyField, flag ? null : data[text]);
						}
					}
					else
					{
						orderedDictionary.Add(text, data[text]);
					}
				}
			}
			return orderedDictionary;
		}

		// Token: 0x0600CAB2 RID: 51890 RVA: 0x002D4384 File Offset: 0x002D2584
		private static void SetMissingAttributesToNull(RadScheduler owner, IDictionary data)
		{
			foreach (string key in owner.CustomAttributeNames)
			{
				if (!data.Contains(key))
				{
					data[key] = null;
				}
			}
		}

		// Token: 0x0600CAB3 RID: 51891 RVA: 0x002D43D4 File Offset: 0x002D25D4
		private bool IsAttribute(string propertyName)
		{
			return Array.Exists<string>(this._scheduler.CustomAttributeNames, (string attribName) => attribName.Equals(propertyName));
		}

		// Token: 0x0600CAB4 RID: 51892 RVA: 0x002D440A File Offset: 0x002D260A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600CAB5 RID: 51893 RVA: 0x002D4419 File Offset: 0x002D2619
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._selectCompleted != null)
			{
				((IDisposable)this._selectCompleted).Dispose();
			}
		}

		// Token: 0x04003534 RID: 13620
		private readonly RadScheduler _scheduler;

		// Token: 0x04003535 RID: 13621
		private readonly AutoResetEvent _selectCompleted;

		// Token: 0x04003536 RID: 13622
		private IEnumerable _selectedData;
	}
}
