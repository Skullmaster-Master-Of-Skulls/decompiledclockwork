using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.LookupCourses;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.LookupCourses
{
	// Token: 0x020000D7 RID: 215
	public class AcademicTermManager : IAcademicTermManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x00038171 File Offset: 0x00036371
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x00038179 File Offset: 0x00036379
		public OperationContext OpContext { get; set; }

		// Token: 0x06000843 RID: 2115 RVA: 0x00038182 File Offset: 0x00036382
		public AcademicTermManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00038194 File Offset: 0x00036394
		public AcademicTerm GetCurrentAcademicTerm()
		{
			return this.GetAcademicTerm(DateTime.Now);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000381B4 File Offset: 0x000363B4
		public IList<AcademicTerm> LoadAcademicTerms(bool ignoreCache = false)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<AcademicTerm> list = ignoreCache ? null : ((IList<AcademicTerm>)cacheStorageManager["academicTerms"]);
			bool flag = list == null;
			if (flag)
			{
				IAcademicTermDAO academicTermDAO = new AcademicTermDAO(this.OpContext);
				IList<AcademicTerm> list2 = academicTermDAO.LoadAcademicTerms();
				List<AcademicTerm> list3 = (list2 != null) ? list2.ToList<AcademicTerm>() : null;
				bool flag2 = list3 != null;
				if (flag2)
				{
					if (list3 != null)
					{
						list3.Sort(delegate(AcademicTerm g1, AcademicTerm g2)
						{
							int num = this.GetDayOfYearNumber(g1.StartMonthDay).CompareTo(this.GetDayOfYearNumber(g2.StartMonthDay));
							bool flag3 = num != 0;
							int result;
							if (flag3)
							{
								result = num;
							}
							else
							{
								result = this.GetDayOfYearNumber(g1.EndMonthDay).CompareTo(this.GetDayOfYearNumber(g2.EndMonthDay));
							}
							return result;
						});
					}
					list = list3;
					cacheStorageManager["academicTerms"] = list;
				}
			}
			return list;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00038248 File Offset: 0x00036448
		private int GetDayOfYearNumber(DateTime dt)
		{
			DateTime dateTime = new DateTime(2015, dt.Month, dt.Day);
			return dateTime.DayOfYear;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0003827C File Offset: 0x0003647C
		public AcademicTerm GetAcademicTerm(DateTime date)
		{
			IList<AcademicTerm> list = this.LoadAcademicTerms(false);
			int year = date.Year;
			int i = 0;
			while (i < list.Count)
			{
				AcademicTerm academicTerm = list[i];
				TimeSpan value = academicTerm.EndMonthDay - academicTerm.StartMonthDay;
				DateTime t = new DateTime(year, academicTerm.StartMonthDay.Month, academicTerm.StartMonthDay.Day);
				DateTime t2 = t.Add(value);
				bool flag = date >= t && date <= t2;
				AcademicTerm result;
				if (flag)
				{
					result = academicTerm;
				}
				else
				{
					bool flag2 = date < t;
					if (!flag2)
					{
						i++;
						continue;
					}
					bool flag3 = i > 0;
					if (flag3)
					{
						result = list[i - 1];
					}
					else
					{
						result = academicTerm;
					}
				}
				return result;
			}
			return null;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00038358 File Offset: 0x00036558
		public eSessionListValidationResult ValidateAcademicTermList(IList<AcademicTerm> list)
		{
			bool flag = list == null || list.Count < 1;
			eSessionListValidationResult result;
			if (flag)
			{
				result = eSessionListValidationResult.Empty;
			}
			else
			{
				int[] array = new int[365];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = 0;
				}
				foreach (AcademicTerm academicTerm in list)
				{
					try
					{
						DayInYear dayInYear = new DayInYear(academicTerm.StartMonthDay.Month, academicTerm.StartMonthDay.Day);
						DayInYear dayInYear2 = new DayInYear(academicTerm.EndMonthDay.Month, academicTerm.EndMonthDay.Day);
						bool flag2 = !dayInYear.IsValid || !dayInYear2.IsValid;
						if (flag2)
						{
							return eSessionListValidationResult.InvalidDate;
						}
						for (int j = dayInYear.DayOfYear; j <= dayInYear2.DayOfYear; j++)
						{
							int num = j - 1;
							bool flag3 = array[num] > 0;
							if (flag3)
							{
								return eSessionListValidationResult.Overlap;
							}
							array[num] = 1;
						}
					}
					catch (Exception ex)
					{
						CWLogger.Logger.Error("ValidateSessionList:Failed:Title={0}:err={1}", ((academicTerm != null) ? academicTerm.Title : null) ?? "", ex.ToString());
						return eSessionListValidationResult.InvalidDate;
					}
				}
				result = (array.Any((int g) => g < 1) ? eSessionListValidationResult.Gap : eSessionListValidationResult.Succeeded);
			}
			return result;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0003852C File Offset: 0x0003672C
		public void ChangeCurrentAcademicTerms(IList<AcademicTerm> newAcademicTermList)
		{
			eSessionListValidationResult eSessionListValidationResult = this.ValidateAcademicTermList(newAcademicTermList);
			bool flag = eSessionListValidationResult != eSessionListValidationResult.Succeeded;
			if (flag)
			{
				string message = string.Format("ChangeCurrentAcademicTerms:Failed:reason={0}", eSessionListValidationResult.ToString());
				CWLogger.Logger.Error(message);
				throw new Exception(message);
			}
			IAcademicTermDAO academicTermDAO = new AcademicTermDAO(this.OpContext);
			academicTermDAO.ChangeCurrentAcademicTerms(newAcademicTermList);
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			cacheStorageManager.Remove("academicTerms");
		}
	}
}
