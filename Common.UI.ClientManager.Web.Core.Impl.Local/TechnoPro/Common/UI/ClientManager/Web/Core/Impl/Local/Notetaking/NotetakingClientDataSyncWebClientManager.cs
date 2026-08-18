using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Notetaking;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.ClientManager.Core.DataSync;
using TechnoPro.Common.ClientManager.ICore.DataSync;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Notetaking;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;
using TechnoPro.Common.UI.Web.Entity.Notetaking;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Notetaking
{
	// Token: 0x02000017 RID: 23
	public class NotetakingClientDataSyncWebClientManager : INotetakingClientDataSyncWebClientManager
	{
		// Token: 0x06000082 RID: 130 RVA: 0x000049D8 File Offset: 0x00002BD8
		private NotetakerWithExternalCoursesDTO GetLegacyNotetakerWithExternalCoursesFromSession()
		{
			HttpSessionState session = HttpContext.Current.Session;
			object obj = session["notetaker"];
			bool flag = obj != null;
			if (flag)
			{
				try
				{
					Type type = Type.GetType("ClockWorkController.ServiceProvider, ClockWorkController");
					bool flag2 = obj.GetType() == type;
					if (flag2)
					{
						NotetakerWithExternalCoursesDTO notetakerWithExternalCoursesDTO = new NotetakerWithExternalCoursesDTO();
						notetakerWithExternalCoursesDTO.Notetaker = new SPProviderDTO();
						notetakerWithExternalCoursesDTO.ExternalCourses = new List<DataSyncExternalCourseDTO>();
						PropertyInfo[] properties = type.GetProperties();
						SPProviderDTO notetaker = notetakerWithExternalCoursesDTO.Notetaker;
						notetaker.Address1 = (string)properties.FirstOrDefault((PropertyInfo g) => g.Name == "Address").GetValue(obj, null);
						notetaker.Phone2 = (string)properties.FirstOrDefault((PropertyInfo g) => g.Name == "Phone2").GetValue(obj, null);
						notetaker.Email = (string)properties.FirstOrDefault((PropertyInfo g) => g.Name == "Email").GetValue(obj, null);
						notetaker.Address2 = (string)properties.FirstOrDefault((PropertyInfo g) => g.Name == "PermanentAddress").GetValue(obj, null);
						notetaker.Phone1 = (string)properties.FirstOrDefault((PropertyInfo g) => g.Name == "Phone1").GetValue(obj, null);
						notetaker.UserName = (string)properties.FirstOrDefault((PropertyInfo g) => g.Name == "AltId").GetValue(obj, null);
						SPProviderDTO spproviderDTO = notetaker;
						PersonBaseDTO personBaseDTO = new PersonBaseDTO();
						personBaseDTO.FirstName = (string)properties.FirstOrDefault((PropertyInfo g) => g.Name == "FirstName").GetValue(obj, null);
						personBaseDTO.Student_no = (string)properties.FirstOrDefault((PropertyInfo g) => g.Name == "Student_no").GetValue(obj, null);
						personBaseDTO.MiddleName = "";
						personBaseDTO.LastName = (string)properties.FirstOrDefault((PropertyInfo g) => g.Name == "LastName").GetValue(obj, null);
						spproviderDTO.Person = personBaseDTO;
						object obj2 = session["notetakercourses"];
						bool flag3 = obj2 != null && obj2 is IList;
						if (flag3)
						{
							IList<DataSyncExternalCourseDTO> externalCourses = notetakerWithExternalCoursesDTO.ExternalCourses;
							bool flag4 = typeof(IList).IsAssignableFrom(obj2.GetType());
							if (flag4)
							{
								IList list = (IList)obj2;
								foreach (object obj3 in list)
								{
									ICollection<DataSyncExternalCourseDTO> collection = externalCourses;
									DataSyncExternalCourseDTO dataSyncExternalCourseDTO = new DataSyncExternalCourseDTO();
									dataSyncExternalCourseDTO.AlternateContacts = new List<DataSyncExternalCourseAltContactDTO>();
									dataSyncExternalCourseDTO.Campus = (string)properties.FirstOrDefault((PropertyInfo g) => g.Name == "Campus").GetValue(obj3, null);
									dataSyncExternalCourseDTO.Term = (string)properties.FirstOrDefault((PropertyInfo g) => g.Name == "Term").GetValue(obj3, null);
									dataSyncExternalCourseDTO.Duration = (string)properties.FirstOrDefault((PropertyInfo g) => g.Name == "Duration").GetValue(obj3, null);
									dataSyncExternalCourseDTO.StartDate = (DateTime)properties.FirstOrDefault((PropertyInfo g) => g.Name == "StartDate").GetValue(obj3, null);
									dataSyncExternalCourseDTO.EndDate = (DateTime)properties.FirstOrDefault((PropertyInfo g) => g.Name == "EndDate").GetValue(obj3, null);
									dataSyncExternalCourseDTO.Course = (string)properties.FirstOrDefault((PropertyInfo g) => g.Name == "Course").GetValue(obj3, null);
									dataSyncExternalCourseDTO.Subject = (string)properties.FirstOrDefault((PropertyInfo g) => g.Name == "Subject").GetValue(obj3, null);
									dataSyncExternalCourseDTO.Department = (string)properties.FirstOrDefault((PropertyInfo g) => g.Name == "Department").GetValue(obj3, null);
									dataSyncExternalCourseDTO.TimeOfDay = (string)properties.FirstOrDefault((PropertyInfo g) => g.Name == "TimeOfDay").GetValue(obj3, null);
									dataSyncExternalCourseDTO.TimetableItems = new List<DataSyncExternalCourseTimetableItemDTO>();
									dataSyncExternalCourseDTO.Location = (string)properties.FirstOrDefault((PropertyInfo g) => g.Name == "Location").GetValue(obj3, null);
									collection.Add(dataSyncExternalCourseDTO);
								}
							}
						}
						return notetakerWithExternalCoursesDTO;
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("NotetakingClientDataSyncWebClientManager:GetNotetakerAndCourseInfo:exT={0}", ex.ToString());
				}
			}
			return null;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004FE4 File Offset: 0x000031E4
		public NotetakerWithExternalCoursesDTO GetNotetakerAndCourseInfo(bool ignoreCache, object currentPageObj, out GetNotetakerInfoAndCoursesInfo getNotetakerInfoAndCoursesInfo)
		{
			HttpSessionState session = HttpContext.Current.Session;
			NotetakerWithExternalCoursesDTO notetakerWithExternalCoursesDTO = ignoreCache ? null : ((NotetakerWithExternalCoursesDTO)session["notetakerwithextcourses"]);
			bool flag = notetakerWithExternalCoursesDTO != null;
			NotetakerWithExternalCoursesDTO result;
			if (flag)
			{
				getNotetakerInfoAndCoursesInfo = new GetNotetakerInfoAndCoursesInfo
				{
					Source = eGetNotetakerInfoAndCoursesSource.FromCache
				};
				result = notetakerWithExternalCoursesDTO;
			}
			else
			{
				NotetakerWithExternalCoursesDTO notetakerWithExternalCoursesDTO2 = this.GetLegacyNotetakerWithExternalCoursesFromSession();
				bool flag2 = notetakerWithExternalCoursesDTO2 != null;
				if (flag2)
				{
					getNotetakerInfoAndCoursesInfo = new GetNotetakerInfoAndCoursesInfo
					{
						Source = eGetNotetakerInfoAndCoursesSource.FromLegacySession
					};
					result = notetakerWithExternalCoursesDTO2;
				}
				else
				{
					ClockWorkIdentity currentClockWorkIdentity = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetCurrentClockWorkIdentity(currentPageObj);
					string text = (currentClockWorkIdentity != null) ? (currentClockWorkIdentity.StudentNumber ?? "") : "";
					string authenticatedUsername = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetAuthenticatedUsername(currentPageObj);
					string text2 = ((string)session["student_no"]) ?? "";
					bool flag3 = text2.Length > 0;
					if (flag3)
					{
						text = text2;
					}
					getNotetakerInfoAndCoursesInfo = new GetNotetakerInfoAndCoursesInfo
					{
						Username = authenticatedUsername
					};
					IDataSyncClientManager dataSyncClientManager = new DataSyncClientManager();
					bool flag4 = text.Trim().Length > 0;
					if (flag4)
					{
						getNotetakerInfoAndCoursesInfo.Source = eGetNotetakerInfoAndCoursesSource.FromSessionStudentNumber;
						getNotetakerInfoAndCoursesInfo.StudentNumber = text;
						notetakerWithExternalCoursesDTO2 = dataSyncClientManager.GetNotetakerPreviewDataByStudentNumber(authenticatedUsername, text.Trim());
					}
					else
					{
						getNotetakerInfoAndCoursesInfo.Source = eGetNotetakerInfoAndCoursesSource.FromUsername;
						notetakerWithExternalCoursesDTO2 = dataSyncClientManager.GetNotetakerPreviewData(authenticatedUsername);
					}
					bool flag5 = notetakerWithExternalCoursesDTO2 != null && notetakerWithExternalCoursesDTO2.Notetaker != null;
					if (flag5)
					{
						session.Add("notetakerwithextcourses", notetakerWithExternalCoursesDTO2);
					}
					else
					{
						notetakerWithExternalCoursesDTO2 = null;
					}
					result = notetakerWithExternalCoursesDTO2;
				}
			}
			return result;
		}
	}
}
