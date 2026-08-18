using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005DF RID: 1503
	public static class TestBookingAdapter
	{
		// Token: 0x06003065 RID: 12389 RVA: 0x000400B4 File Offset: 0x0003E2B4
		public static PersonBase GetFirstStudent(this Test Test)
		{
			foreach (Attendee attendee in Test.Attendees)
			{
				bool flag = attendee.Person.CoreGroup == eCoreGroup.Students;
				if (flag)
				{
					return attendee.Person;
				}
			}
			return null;
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x00040124 File Offset: 0x0003E324
		public static int GetDefaultColourArgb(this Test Test)
		{
			return Test.GetDefaultColourArgb();
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x0004013C File Offset: 0x0003E33C
		public static LookupCourseBase GetCourse(this Test Test)
		{
			bool flag = Test.ClassTestInfo != null;
			if (flag)
			{
				bool flag2 = Test.ClassTestInfo.Course != null;
				if (flag2)
				{
					return Test.ClassTestInfo.Course;
				}
				bool flag3 = Test.StudentClassTestInfo != null;
				if (flag3)
				{
					return Test.StudentClassTestInfo.Course;
				}
			}
			return null;
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x0004019C File Offset: 0x0003E39C
		public static eClassTestType GetClassTestTypeFromString(this string s)
		{
			string text = s.ToUpper().Trim();
			bool flag = text.Length == 1;
			if (flag)
			{
				char value = text[0];
				int num = Convert.ToInt32(value);
				bool flag2 = Enum.IsDefined(typeof(eClassTestType), num);
				if (flag2)
				{
					return (eClassTestType)num;
				}
			}
			return eClassTestType.Unknown;
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000401FC File Offset: 0x0003E3FC
		public static string GetStringFromClassTestType(this eClassTestType ClassTestType)
		{
			return Convert.ToChar((int)ClassTestType).ToString();
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x00040220 File Offset: 0x0003E420
		public static Forest<TreeNodeDataItemOrGroup<ExamManagementView, string>> ConvertExamManagementViewsToForest(IList<ExamManagementView> items)
		{
			Forest<TreeNodeDataItemOrGroup<ExamManagementView, string>> forest = new Forest<TreeNodeDataItemOrGroup<ExamManagementView, string>>();
			bool flag = items == null;
			Forest<TreeNodeDataItemOrGroup<ExamManagementView, string>> result;
			if (flag)
			{
				result = forest;
			}
			else
			{
				eExamManagementViewGroup[] array = (eExamManagementViewGroup[])Enum.GetValues(typeof(eExamManagementViewGroup));
				eExamManagementViewGroup[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					eExamManagementViewGroup group = array2[i];
					List<ExamManagementView> list = (from g in items
					where g.Group == @group
					select g).ToList<ExamManagementView>();
					bool flag2 = list.Count < 1;
					if (!flag2)
					{
						TreeNode<TreeNodeDataItemOrGroup<ExamManagementView, string>> parentNode = forest.AppendNode(null, new TreeNodeDataItemOrGroup<ExamManagementView, string>
						{
							Group = group.GetAttribute<ExamManagementViewGroupAttribute>().Title
						});
						foreach (ExamManagementView item in list)
						{
							forest.AppendNode(parentNode, new TreeNodeDataItemOrGroup<ExamManagementView, string>
							{
								Item = item
							});
						}
					}
				}
				result = forest;
			}
			return result;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x0004033C File Offset: 0x0003E53C
		public static IList<ExamManagementView> GetExamManagementListFromXml(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			IList<ExamManagementView> result;
			if (flag)
			{
				result = new List<ExamManagementView>();
			}
			else
			{
				xml = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\" ?>{0}", xml);
				XDocument xdocument = XDocument.Parse(xml);
				IEnumerable<XElement> source = xdocument.Descendants("view");
				result = (from item in source
				let attrTitle = item.Attribute("title")
				let attrDescription = item.Attribute("description")
				let attrGroup = item.Attribute("group")
				let attrViewType = item.Attribute("viewtype")
				let attrQueryType = item.Attribute("querytype")
				let attrDefaultStart = item.Attribute("defaultstart")
				let attrDefaultNumDays = item.Attribute("defaultnumdays")
				let attrOrderNum = item.Attribute("ordernum")
				let attrRid = item.Attribute("reportid")
				select new ExamManagementView
				{
					Title = ((attrTitle == null) ? "" : (attrTitle.Value ?? "")),
					Description = ((attrDescription == null) ? "" : (attrDescription.Value ?? "")),
					Group = attrGroup.GetEnumFromAttributeInt(eExamManagementViewGroup.Lists),
					ViewType = attrViewType.GetEnumFromAttributeInt(eExamManagementViewType.GridWithDateNavigator),
					QueryType = attrQueryType.GetEnumFromAttributeInt(eExamManagementQueryType.Bookings),
					StartDaysFromToday = attrDefaultStart.GetIntFromAttribute(),
					EndNumDays = attrDefaultNumDays.GetIntFromAttribute(),
					OrderNum = attrOrderNum.GetIntFromAttribute(0),
					ReportId = attrRid.GetIntFromAttribute(0),
					IsDisabled = false
				}).ToList<ExamManagementView>();
			}
			return result;
		}
	}
}
