using System;
using System.Collections;
using System.ComponentModel;
using Telerik.Web.UI.Calendar.Collections;

namespace Telerik.Web.UI.Calendar.TypeConverters
{
	// Token: 0x02001012 RID: 4114
	public class TemplateListTypeConverter : TypeConverter
	{
		// Token: 0x0600A1DD RID: 41437 RVA: 0x0023F858 File Offset: 0x0023DA58
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			ArrayList arrayList = new ArrayList();
			RadCalendarDay radCalendarDay = null;
			if (context != null && context.Instance != null && context.PropertyDescriptor != null)
			{
				if (context.Instance is RadCalendarDay)
				{
					radCalendarDay = (RadCalendarDay)context.Instance;
				}
				if (radCalendarDay.ParentCalendar != null)
				{
					CalendarDayTemplateCollection calendarDayTemplates = radCalendarDay.ParentCalendar.CalendarDayTemplates;
					arrayList.Add(string.Empty);
					for (int i = 0; i < calendarDayTemplates.Count; i++)
					{
						arrayList.Add(calendarDayTemplates[i].ID);
					}
					return new TypeConverter.StandardValuesCollection(arrayList);
				}
			}
			return null;
		}

		// Token: 0x0600A1DE RID: 41438 RVA: 0x0023F8EC File Offset: 0x0023DAEC
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x0600A1DF RID: 41439 RVA: 0x0023F8EF File Offset: 0x0023DAEF
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
