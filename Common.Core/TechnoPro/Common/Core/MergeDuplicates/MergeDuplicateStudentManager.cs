using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.AppointmentsCalendar;
using TechnoPro.Common.Core.Cases;
using TechnoPro.Common.Core.CourseRegistrations;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.ServiceProvider;
using TechnoPro.Common.DAO.Impl.MergeDuplicates;
using TechnoPro.Common.DAO.MergeDuplicates;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.CourseRegistrations;
using TechnoPro.Common.ICore.MergeDuplicates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.MergeDuplicates.Students;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.TextFormat.Adapters;

namespace TechnoPro.Common.Core.MergeDuplicates
{
	// Token: 0x020000B5 RID: 181
	public class MergeDuplicateStudentManager : IMergeDuplicateStudentManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x00027328 File Offset: 0x00025528
		private DynamicDataManager dynamicDataManager
		{
			get
			{
				DynamicDataManager result;
				if ((result = this._dynamicDataManager) == null)
				{
					result = (this._dynamicDataManager = new DynamicDataManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00027353 File Offset: 0x00025553
		public MergeDuplicateStudentManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new MergeDuplicateStudentDAO(opContext);
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x00027371 File Offset: 0x00025571
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x00027379 File Offset: 0x00025579
		public OperationContext OpContext { get; set; }

		// Token: 0x060006CD RID: 1741 RVA: 0x00027384 File Offset: 0x00025584
		private void GetDuplicateStudentPreviewInfo(ref DuplicateStudent student)
		{
			DateTime startDate = DateTime.Now.AddYears(-75);
			DateTime endDate = DateTime.Now.AddYears(75);
			int personId = student.Student.PersonId;
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(this.OpContext);
			student.Appointments = baseAppointmentManager.LoadBaseBasicAppointmentsByPersonAndDateRange(personId, false, startDate, endDate);
			student.Courses = courseRegistrationManager.LoadStudentsCourses(startDate, endDate, personId, false);
			DynamicFormManager dynamicFormManager = new DynamicFormManager(this.OpContext);
			IList<DynamicForm> screensAStudentHasDataOn = dynamicFormManager.GetScreensAStudentHasDataOn(personId);
			List<DynamicData> list = new List<DynamicData>();
			foreach (DynamicForm dynamicForm in screensAStudentHasDataOn)
			{
				bool flag = dynamicForm.FormType == eDynamicFormType.PerStudent;
				if (flag)
				{
					List<DynamicData> list2 = this.dynamicDataManager.LoadData(new DynamicDataContext
					{
						PrimaryId = personId
					}, dynamicForm);
					bool flag2 = list2 != null;
					if (flag2)
					{
						using (List<DynamicData>.Enumerator enumerator2 = list2.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								DynamicData dataItem = enumerator2.Current;
								bool flag3 = list.Find((DynamicData f) => f.Field.ControlId == dataItem.Field.ControlId) == null;
								if (flag3)
								{
									list.Add(dataItem);
								}
							}
						}
					}
				}
			}
			student.PerStudentDataItems = list;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00027524 File Offset: 0x00025724
		public IList<PotentialDuplicateStudentSet> FindPotentialDuplicateStudents(int GroupId)
		{
			PeopleManager peopleManager = new PeopleManager(this.OpContext);
			List<PersonBase> list = peopleManager.LoadStudents();
			List<PersonBase> list2 = list.ConvertAll<PersonBase>((PersonBase p) => p);
			List<PotentialDuplicateStudentSet> list3 = new List<PotentialDuplicateStudentSet>();
			list2.Sort((PersonBase p1, PersonBase p2) => p1.Student_no.CompareTo(p2.Student_no));
			int num = list2.Count - 1;
			for (int i = 0; i < num; i++)
			{
				PersonBase personBase = list2[i];
				PersonBase personBase2 = list2[i + 1];
				bool flag = personBase.PersonId != personBase2.PersonId && personBase.Student_no.Equals(personBase2.Student_no, StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					list3.Add(new PotentialDuplicateStudentSet
					{
						Student1 = personBase,
						Student2 = personBase2,
						EditDistance = 0
					});
				}
			}
			list2.Sort((PersonBase p1, PersonBase p2) => string.Concat(new string[]
			{
				p1.LastName,
				", ",
				p1.FirstName,
				" ",
				p1.MiddleName,
				" ",
				p1.Student_no
			}).CompareTo(string.Concat(new string[]
			{
				p2.LastName,
				", ",
				p2.FirstName,
				" ",
				p2.MiddleName,
				" ",
				p2.Student_no
			})));
			for (int i = 0; i < num; i++)
			{
				PersonBase p1 = list2[i];
				PersonBase p2 = list2[i + 1];
				bool flag2 = p1.PersonId != p2.PersonId;
				if (flag2)
				{
					int distanceSlowerButMoreAccurate = (p1.FirstName + " " + p1.LastName).GetDistanceSlowerButMoreAccurate(p2.FirstName + " " + p2.LastName);
					bool flag3 = distanceSlowerButMoreAccurate < 3;
					if (flag3)
					{
						bool flag4 = list3.Find((PotentialDuplicateStudentSet pm) => (pm.Student1.PersonId == p1.PersonId && pm.Student2.PersonId == p2.PersonId) || (pm.Student2.PersonId == p1.PersonId && pm.Student1.PersonId == p2.PersonId)) == null;
						if (flag4)
						{
							list3.Add(new PotentialDuplicateStudentSet
							{
								EditDistance = distanceSlowerButMoreAccurate,
								Student1 = p1,
								Student2 = p2
							});
						}
					}
				}
			}
			list3.Sort(delegate(PotentialDuplicateStudentSet p1, PotentialDuplicateStudentSet p2)
			{
				bool flag5 = p1.EditDistance == p2.EditDistance;
				int result;
				if (flag5)
				{
					result = p1.Student1.GetStudentName().CompareTo(p2.Student1.GetStudentName());
				}
				else
				{
					result = p1.EditDistance.CompareTo(p2.EditDistance);
				}
				return result;
			});
			return list3;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00027790 File Offset: 0x00025990
		public void MergeDuplicateStudents(DuplicateStudentSet DuplicateStudentSet)
		{
			PeopleManager peopleManager = new PeopleManager(this.OpContext);
			CourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(this.OpContext);
			AppointmentManager appointmentManager = new AppointmentManager(this.OpContext);
			ServiceRequestManager serviceRequestManager = new ServiceRequestManager(this.OpContext);
			CaseManager caseManager = new CaseManager(this.OpContext);
			StudentActivationManager studentActivationManager = new StudentActivationManager(this.OpContext);
			bool flag = DuplicateStudentSet.StudentToKeep == eDuplicateItemToUse.Use2;
			DuplicateStudent duplicateStudent;
			DuplicateStudent duplicateStudent2;
			if (flag)
			{
				duplicateStudent = DuplicateStudentSet.Student2;
				duplicateStudent2 = DuplicateStudentSet.Student1;
			}
			else
			{
				duplicateStudent = DuplicateStudentSet.Student1;
				duplicateStudent2 = DuplicateStudentSet.Student2;
				DuplicateStudentSet.StudentToKeep = eDuplicateItemToUse.Use1;
			}
			PersonBase student = duplicateStudent.Student;
			PersonBase student2 = duplicateStudent2.Student;
			bool flag2 = !string.IsNullOrEmpty(DuplicateStudentSet.CorrectStudentNumber) && !student.Student_no.Equals(DuplicateStudentSet.CorrectStudentNumber, StringComparison.OrdinalIgnoreCase);
			if (flag2)
			{
				student.Student_no = DuplicateStudentSet.CorrectStudentNumber;
				peopleManager.UpdateUser(student, false);
			}
			List<DuplicateDynamicDataItem> list = DuplicateStudentSet.DuplicateDataItems.ToList<DuplicateDynamicDataItem>();
			List<int> list2 = new List<int>();
			List<DynamicData> list3 = new List<DynamicData>();
			foreach (DuplicateDynamicDataItem duplicateDynamicDataItem in list)
			{
				bool flag3 = !list2.Contains(duplicateDynamicDataItem.DataItem1.Field.ControlId);
				if (flag3)
				{
					list2.Add(duplicateDynamicDataItem.DataItem1.Field.ControlId);
				}
				bool flag4 = !list2.Contains(duplicateDynamicDataItem.DataItem2.Field.ControlId);
				if (flag4)
				{
					list2.Add(duplicateDynamicDataItem.DataItem2.Field.ControlId);
				}
				bool flag5 = duplicateDynamicDataItem.DataItemToUse == eDuplicateItemToUse.Use2;
				if (flag5)
				{
					bool flag6 = DuplicateStudentSet.StudentToKeep == eDuplicateItemToUse.Use1;
					if (flag6)
					{
						duplicateDynamicDataItem.DataItem1.Value = duplicateDynamicDataItem.DataItem2.Value;
						duplicateDynamicDataItem.DataItem1.ValueId = duplicateDynamicDataItem.DataItem2.ValueId;
						list3.Add(duplicateDynamicDataItem.DataItem1);
					}
				}
				else
				{
					bool flag7 = DuplicateStudentSet.StudentToKeep == eDuplicateItemToUse.Use2;
					if (flag7)
					{
						duplicateDynamicDataItem.DataItem2.Value = duplicateDynamicDataItem.DataItem1.Value;
						duplicateDynamicDataItem.DataItem2.ValueId = duplicateDynamicDataItem.DataItem1.ValueId;
						list3.Add(duplicateDynamicDataItem.DataItem2);
					}
				}
			}
			this.dynamicDataManager.SaveData(new DynamicDataContext
			{
				PrimaryId = student.PersonId
			}, list3, eDynamicFormType.PerStudent);
			this.dynamicDataManager.MergeAllData(student.PersonId, student2.PersonId);
			courseRegistrationManager.MergeCourseRegistrations(student.PersonId, student2.PersonId);
			appointmentManager.MergeAllAppointments(false, student.PersonId, student2.PersonId);
			serviceRequestManager.MergeDuplicateRequestsForTwoStudents(student.PersonId, student2.PersonId);
			caseManager.MergeCasesForTwoStudents(student.PersonId, student2.PersonId);
			IList<Group> source = peopleManager.LoadUserGroupMemberships(student2.PersonId);
			peopleManager.AddUserToGroups(student.PersonId, source.ToList<Group>().ConvertAll<int>((Group f) => f.GroupId));
			studentActivationManager.MergeActivations(student.PersonId, student2.PersonId);
			peopleManager.DeleteUser(student2.PersonId, true);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00027B18 File Offset: 0x00025D18
		public DuplicateStudentSet LoadDuplicateStudentPreviewInfo(DuplicateStudentSet DuplicateSet)
		{
			DuplicateStudent student = DuplicateSet.Student1;
			DuplicateStudent student2 = DuplicateSet.Student2;
			this.GetDuplicateStudentPreviewInfo(ref student);
			this.GetDuplicateStudentPreviewInfo(ref student2);
			List<DuplicateDynamicDataItem> list = new List<DuplicateDynamicDataItem>();
			List<DynamicData> list4 = student.PerStudentDataItems.ToList<DynamicData>();
			List<DynamicData> list2 = student2.PerStudentDataItems.ToList<DynamicData>();
			List<DynamicData> list3 = list4.FindAll((DynamicData f) => list2.Find((DynamicData g) => g.Field.ControlId == f.Field.ControlId) != null);
			foreach (DynamicData item in list3)
			{
				DynamicData item1 = item;
				DynamicData item2 = list2.Find((DynamicData f) => f.Field.ControlId == item1.Field.ControlId);
				bool flag = list.Find((DuplicateDynamicDataItem f) => (f.DataItem1.Field.ControlId == item1.Field.ControlId && f.DataItem2.Field.ControlId == item2.Field.ControlId) || (f.DataItem1.Field.ControlId == item2.Field.ControlId && f.DataItem2.Field.ControlId == item1.Field.ControlId)) == null;
				if (flag)
				{
					string text = item1.GetString() ?? "";
					string value = item2.GetString() ?? "";
					bool flag2 = !text.Equals(value, StringComparison.OrdinalIgnoreCase);
					if (flag2)
					{
						list.Add(new DuplicateDynamicDataItem
						{
							DataItem1 = item1,
							DataItem2 = item2,
							DataItemToUse = eDuplicateItemToUse.Use1
						});
					}
				}
			}
			DuplicateSet.DuplicateDataItems = list;
			return DuplicateSet;
		}

		// Token: 0x04000149 RID: 329
		private IMergeDuplicateStudentDAO dao;

		// Token: 0x0400014A RID: 330
		private DynamicDataManager _dynamicDataManager;
	}
}
