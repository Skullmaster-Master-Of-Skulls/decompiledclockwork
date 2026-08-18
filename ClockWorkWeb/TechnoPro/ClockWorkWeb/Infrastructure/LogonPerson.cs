using System;
using System.Security.Claims;
using System.Web;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.People;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;

namespace TechnoPro.ClockWorkWeb.Infrastructure
{
	// Token: 0x02000113 RID: 275
	public class LogonPerson
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0003A9A0 File Offset: 0x00038BA0
		public static LogonPerson Instance
		{
			get
			{
				LogonPerson result;
				if ((result = LogonPerson._instance) == null)
				{
					result = (LogonPerson._instance = new LogonPerson());
				}
				return result;
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0000AF9E File Offset: 0x0000919E
		protected LogonPerson()
		{
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0003A9C8 File Offset: 0x00038BC8
		public int GetLogonStudentPersonId(HttpSessionStateBase session = null)
		{
			HttpSessionStateBase httpSessionStateBase = session ?? new HttpSessionStateWrapper(HttpContext.Current.Session);
			object obj = httpSessionStateBase["StudentPersonId"];
			bool flag = obj != null;
			int result;
			if (flag)
			{
				result = (int)obj;
			}
			else
			{
				int num = 0;
				try
				{
					PersonBaseDTO personBaseDTO = (PersonBaseDTO)httpSessionStateBase["Student"];
					bool flag2 = personBaseDTO != null;
					if (flag2)
					{
						num = (result = personBaseDTO.PersonId);
					}
					else
					{
						Claim claim = ClaimsPrincipal.Current.FindFirst("pid");
						bool flag3 = claim != null && !string.IsNullOrEmpty(claim.Value) && int.TryParse(claim.Value, out num);
						if (flag3)
						{
							result = num;
						}
						else
						{
							IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
							ClockWorkIdentity currentClockWorkIdentity = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity(null);
							bool flag4 = currentClockWorkIdentity != null && currentClockWorkIdentity.PersonId > 0;
							if (flag4)
							{
								num = (result = currentClockWorkIdentity.PersonId);
							}
							else
							{
								result = num;
							}
						}
					}
				}
				finally
				{
					bool flag5 = num > 0;
					if (flag5)
					{
						httpSessionStateBase["StudentPersonId"] = num;
					}
				}
			}
			return result;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0003AAE8 File Offset: 0x00038CE8
		public PersonBaseDTO GetLogonStudent(HttpSessionStateBase session = null)
		{
			HttpSessionStateBase httpSessionStateBase = session ?? new HttpSessionStateWrapper(HttpContext.Current.Session);
			PersonBaseDTO personBaseDTO = (PersonBaseDTO)httpSessionStateBase["Student"];
			bool flag = personBaseDTO != null;
			PersonBaseDTO result;
			if (flag)
			{
				result = personBaseDTO;
			}
			else
			{
				IPeopleClientManager peopleClientManager = new PeopleClientManager();
				bool flag2 = personBaseDTO == null;
				if (flag2)
				{
					Claim claim = ClaimsPrincipal.Current.FindFirst("pid");
					int personId;
					bool flag3 = claim != null && !string.IsNullOrEmpty(claim.Value) && int.TryParse(claim.Value, out personId);
					if (flag3)
					{
						personBaseDTO = peopleClientManager.LoadPersonById(personId);
					}
					bool flag4 = personBaseDTO == null;
					if (flag4)
					{
						Claim claim2 = ClaimsPrincipal.Current.FindFirst("TechnoPro/ClockWorks/claims/studentnumber");
						bool flag5 = claim2 != null && !string.IsNullOrEmpty(claim2.Value);
						if (flag5)
						{
							personBaseDTO = peopleClientManager.LoadPersonByStudentNumber(claim2.Value, false);
						}
					}
				}
				bool flag6 = personBaseDTO == null;
				if (flag6)
				{
					IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
					ClockWorkIdentity currentClockWorkIdentity = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity(null);
					bool flag7 = currentClockWorkIdentity != null;
					if (flag7)
					{
						bool flag8 = currentClockWorkIdentity.PersonId > 0;
						if (flag8)
						{
							personBaseDTO = peopleClientManager.LoadPersonById(currentClockWorkIdentity.PersonId);
						}
						else
						{
							bool flag9 = !string.IsNullOrEmpty(currentClockWorkIdentity.StudentNumber);
							if (flag9)
							{
								personBaseDTO = peopleClientManager.LoadPersonByStudentNumber(currentClockWorkIdentity.StudentNumber, false);
							}
						}
					}
				}
				bool flag10 = personBaseDTO != null;
				if (flag10)
				{
					httpSessionStateBase["Student"] = personBaseDTO;
				}
				result = personBaseDTO;
			}
			return result;
		}

		// Token: 0x04000626 RID: 1574
		protected static LogonPerson _instance;

		// Token: 0x04000627 RID: 1575
		private const string LogonStudentSessionKey = "Student";

		// Token: 0x04000628 RID: 1576
		private const string LogonStudentPersonIdSessionKey = "StudentPersonId";
	}
}
