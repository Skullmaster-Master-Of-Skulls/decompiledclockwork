using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Telerik.Web.UI.Scheduler.Views.Day.GroupedByDate;
using Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate;
using Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Week
{
	// Token: 0x02001AAB RID: 6827
	internal class WeekViewDataConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0601080B RID: 67595 RVA: 0x003AFD64 File Offset: 0x003ADF64
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			ModelBase modelBase = obj as ModelBase;
			if (modelBase == null)
			{
				throw new InvalidOperationException("Can serialize only Telerik.Web.UI.Scheduler.Views.Week.ModelBase objects.");
			}
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			if (!modelBase.Owner.ShowFullTime)
			{
				dictionary["aptIndicators"] = WeekViewDataConverter.GetIndicatorsByDay(modelBase);
			}
			return dictionary;
		}

		// Token: 0x0601080C RID: 67596 RVA: 0x003AFDAC File Offset: 0x003ADFAC
		private static int[] GetIndicatorsByDay(ModelBase model)
		{
			Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource.Model model2 = model as Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource.Model;
			if (model2 != null && model2.WeekModels != null && (model2.Owner.GroupingDirection == GroupingDirection.Horizontal || model2 is Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate.Model || model2 is Telerik.Web.UI.Scheduler.Views.Day.GroupedByDate.Model))
			{
				List<int[]> list = new List<int[]>();
				for (int i = 0; i < model2.WeekModels.Count; i++)
				{
					list.Add(WeekViewDataConverter.GetModelIndicatorsByDay(model2.WeekModels[i]));
				}
				return WeekViewDataConverter.MergeResourceIndicators(model2, list);
			}
			return WeekViewDataConverter.GetModelIndicatorsByDay(model);
		}

		// Token: 0x0601080D RID: 67597 RVA: 0x003AFE2C File Offset: 0x003AE02C
		private static int[] MergeResourceIndicators(Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource.Model model, List<int[]> modelsIndicators)
		{
			List<int> list = new List<int>();
			if (model.Owner.GroupingDirection == GroupingDirection.Horizontal)
			{
				if (model is Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate.Model)
				{
					for (int i = 0; i < model.NumberOfDays; i++)
					{
						for (int j = 0; j < modelsIndicators.Count; j++)
						{
							list.Add(modelsIndicators[j][i]);
						}
					}
				}
				else
				{
					for (int k = 0; k < modelsIndicators.Count; k++)
					{
						list.AddRange(modelsIndicators[k]);
					}
				}
			}
			else
			{
				for (int l = 0; l < modelsIndicators.Count; l++)
				{
					bool[] array = new bool[4];
					for (int m = 0; m < model.NumberOfDays; m++)
					{
						int num = modelsIndicators[l][m];
						array[num] = true;
					}
					if (array[3] || (array[2] && array[1]))
					{
						list.Add(3);
					}
					else if (array[2])
					{
						list.Add(2);
					}
					else if (array[1])
					{
						list.Add(1);
					}
					else
					{
						list.Add(0);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0601080E RID: 67598 RVA: 0x003AFF3C File Offset: 0x003AE13C
		private static int[] GetModelIndicatorsByDay(ModelBase model)
		{
			DateTime date = model.VisibleRangeStart;
			int[] array = new int[model.NumberOfDays];
			for (int i = 0; i < model.NumberOfDays; i++)
			{
				DateTime rangeStart = model.Owner.UtcDayStart(date);
				DateTime dateTime = rangeStart.Add(model.EffectiveDayStartTime);
				DateTime rangeEnd = model.Owner.UtcDayStart(date.AddDays(1.0));
				DateTime rangeStart2 = rangeStart.Add(model.EffectiveDayEndTime);
				bool flag = false;
				AppointmentCollection appointments = model.Appointments;
				foreach (Appointment appointment in appointments.GetAppointmentsInRange(rangeStart, dateTime))
				{
					if (appointment.End <= dateTime)
					{
						flag = true;
						break;
					}
				}
				bool flag2 = false;
				using (IEnumerator<Appointment> enumerator2 = appointments.GetAppointmentsStartingInRange(rangeStart2, rangeEnd).GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						Appointment appointment2 = enumerator2.Current;
						flag2 = true;
					}
				}
				HiddenAppointmentsIndicatorType hiddenAppointmentsIndicatorType = HiddenAppointmentsIndicatorType.None;
				if (flag && flag2)
				{
					hiddenAppointmentsIndicatorType = HiddenAppointmentsIndicatorType.Both;
				}
				else if (flag)
				{
					hiddenAppointmentsIndicatorType = HiddenAppointmentsIndicatorType.BeforeDayStart;
				}
				else if (flag2)
				{
					hiddenAppointmentsIndicatorType = HiddenAppointmentsIndicatorType.AfterDayEnd;
				}
				array[i] = (int)hiddenAppointmentsIndicatorType;
				date = date.AddDays(1.0);
			}
			return array;
		}

		// Token: 0x1700502B RID: 20523
		// (get) Token: 0x0601080F RID: 67599 RVA: 0x003B009C File Offset: 0x003AE29C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ModelBase)
				};
			}
		}
	}
}
