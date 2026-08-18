using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using ClockWorkLogger;
using TechnoPro.Common.Core.Authentication;
using TechnoPro.Common.Core.AuthenticationADFS;
using TechnoPro.Common.Core.AuthenticationCAS;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.Ldap;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.Notetaking;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.Reports;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.ICore.Authentication;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.ICore.Membership;
using TechnoPro.Common.ICore.Notetaking;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.ADFS;
using TechnoPro.Common.Public.Entities.Authentication.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authentication.AuthenticationParameter;
using TechnoPro.Common.Public.Entities.Authentication.Authorization;
using TechnoPro.Common.Public.Entities.Caching;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.Membership;
using TechnoPro.Common.Public.Entities.Notetaking;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Security.Hashing;
using TechnoPro.Common.Security.Saml;
using TechnoPro.Common.Security.Saml.Adapters;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.AuthenticationAuthorization
{
	// Token: 0x02000002 RID: 2
	public class ClockWorkAuthenticationManager : IClockWorkAuthenticationManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public ClockWorkAuthenticationManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002062 File Offset: 0x00000262
		// (set) Token: 0x06000003 RID: 3 RVA: 0x0000206A File Offset: 0x0000026A
		public OperationContext OpContext { get; set; }

		// Token: 0x06000004 RID: 4 RVA: 0x00002074 File Offset: 0x00000274
		private string UserToString(ClockWorkUser user)
		{
			bool flag = user == null;
			string result;
			if (flag)
			{
				result = "NULL";
			}
			else
			{
				result = string.Format("Username={0}:pid={1}:nid={2}:iid={3}:alt={4}:snum={5}", new object[]
				{
					user.Username ?? "NULL",
					user.ClockWorkPid.ToString(),
					user.ClockWorkNid.ToString(),
					user.ClockWorkIid.ToString(),
					user.ClockWorkAltContactId.ToString(),
					user.StudentNumber ?? "NULL"
				});
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002110 File Offset: 0x00000310
		private AlternateContact LookupAuthenticatedAltContactInClockWork(AuthorizationContextItem AltContactContext, ExternalUserInfo externalUserInfo, bool VerboseLogging, string usernameToCheckWith)
		{
			bool flag = AltContactContext.IsDisabled || externalUserInfo.IsExternalUserInfoEmpty();
			AlternateContact result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = (externalUserInfo.UserName ?? "").Trim();
				string text2 = (externalUserInfo.Email ?? "").Trim();
				string text3 = (externalUserInfo.StudentNumber ?? "").Trim();
				IAlternateContactManager alternateContactManager = new AlternateContactManager(this.OpContext);
				eLookupMethod lookupMethod = AltContactContext.LookupMethod;
				eLookupMethod eLookupMethod = lookupMethod;
				if (eLookupMethod != eLookupMethod.ByStudentNumberOrEmployeeId)
				{
					if (eLookupMethod != eLookupMethod.ByEmail)
					{
						if (eLookupMethod != eLookupMethod.ByCustomField)
						{
							bool flag2 = text.Length > 0;
							if (flag2)
							{
								return alternateContactManager.LoadAlternateContactByUsername(text);
							}
						}
						else
						{
							CWLogger.Logger.Error("ClockWorkAuthenticationManager:LookupAuthenticatedAltContactInClockWork:LookupMethod=ByCustomField is not supported for alt contact");
						}
					}
					else
					{
						AlternateContact alternateContact = (text2.Length > 0) ? alternateContactManager.LoadAlternateContactByEmail(text2) : null;
						bool flag3 = alternateContact != null;
						if (flag3)
						{
							return alternateContact;
						}
						bool flag4 = text.Length > 0;
						if (flag4)
						{
							return alternateContactManager.LoadAlternateContactByEmail(text);
						}
					}
				}
				else
				{
					AlternateContact alternateContact2 = (text3.Length > 0) ? alternateContactManager.LoadAlternateContactByEmployeeId(text3) : null;
					bool flag5 = alternateContact2 != null;
					if (flag5)
					{
						return alternateContact2;
					}
					bool flag6 = text.Length > 0;
					if (flag6)
					{
						return alternateContactManager.LoadAlternateContactByEmployeeId(text);
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002268 File Offset: 0x00000468
		private LookupInstructor LookupAuthenticatedInstructorInClockWork(AuthorizationContextItem InstructorContext, ExternalUserInfo externalUserInfo, bool VerboseLogging, string usernameToCheckWith)
		{
			bool flag = InstructorContext.IsDisabled || externalUserInfo.IsExternalUserInfoEmpty();
			LookupInstructor result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = (externalUserInfo.Email ?? "").Trim();
				string text2 = (externalUserInfo.StudentNumber ?? "").Trim();
				ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(this.OpContext);
				eLookupMethod lookupMethod = InstructorContext.LookupMethod;
				eLookupMethod eLookupMethod = lookupMethod;
				if (eLookupMethod != eLookupMethod.ByStudentNumberOrEmployeeId)
				{
					if (eLookupMethod != eLookupMethod.ByEmail)
					{
						if (eLookupMethod != eLookupMethod.ByCustomField)
						{
							bool flag2 = usernameToCheckWith.Length > 0;
							if (flag2)
							{
								return lookupInstructorManager.LoadInstructorByUsername(usernameToCheckWith);
							}
						}
						else
						{
							CWLogger.Logger.Error("ClockWorkAuthenticationManager:LookupAuthenticatedInstructorInClockWork:LookupMethod=ByCustomField is not supported for instructors");
						}
					}
					else
					{
						LookupInstructor lookupInstructor = (text.Length > 0) ? lookupInstructorManager.LoadInstructorByEmail(text) : null;
						bool flag3 = lookupInstructor != null;
						if (flag3)
						{
							return lookupInstructor;
						}
						bool flag4 = usernameToCheckWith.Length > 0;
						if (flag4)
						{
							return lookupInstructorManager.LoadInstructorByEmail(usernameToCheckWith);
						}
					}
				}
				else
				{
					LookupInstructor lookupInstructor2 = (text2.Length > 0) ? lookupInstructorManager.LoadInstructorByEmployeeId(text2) : null;
					bool flag5 = lookupInstructor2 != null;
					if (flag5)
					{
						return lookupInstructor2;
					}
					bool flag6 = usernameToCheckWith.Length > 0;
					if (flag6)
					{
						return lookupInstructorManager.LoadInstructorByEmployeeId(usernameToCheckWith);
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000023B4 File Offset: 0x000005B4
		private NotetakerBase LookupAuthenticatedNotetakerInClockWork(AuthorizationContextItem NotetakerContext, ExternalUserInfo externalUserInfo, bool VerboseLogging, string usernameToCheckWith)
		{
			bool flag = NotetakerContext.IsDisabled || externalUserInfo.IsExternalUserInfoEmpty();
			NotetakerBase result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = (externalUserInfo.Email ?? "").Trim();
				string text2 = (externalUserInfo.StudentNumber ?? "").Trim();
				if (VerboseLogging)
				{
					CWLogger.Logger.Trace("Common.Core.AuthenticationAuthorization.ClockWorkAuthenticationManager.LookupAuthenticatedNotetakerInClockWork:Start:username={0}:snum={1}:email={2}:lookupmethod={3}", new object[]
					{
						text2,
						usernameToCheckWith,
						text,
						NotetakerContext.LookupMethod.ToString()
					});
				}
				INotetakingManager notetakingManager = new NotetakingManager(this.OpContext);
				eLookupMethod lookupMethod = NotetakerContext.LookupMethod;
				eLookupMethod eLookupMethod = lookupMethod;
				if (eLookupMethod != eLookupMethod.ByStudentNumberOrEmployeeId)
				{
					if (eLookupMethod != eLookupMethod.ByEmail)
					{
						if (eLookupMethod != eLookupMethod.ByCustomField)
						{
							NotetakerBase notetakerBase = (usernameToCheckWith.Length > 0) ? notetakingManager.LoadNotetakerBaseByUsername(usernameToCheckWith) : null;
							if (VerboseLogging)
							{
								CWLogger.Logger.Trace("Common.Core.AuthenticationAuthorization.ClockWorkAuthenticationManager.LookupAuthenticatedNotetakerInClockWork:ByUsername:username={0}:res={1}", usernameToCheckWith, (notetakerBase == null) ? "NULL" : notetakerBase.ServiceProviderId.ToString());
							}
							bool flag2 = notetakerBase != null;
							if (flag2)
							{
								return notetakerBase;
							}
						}
						else
						{
							CWLogger.Logger.Error("ClockWorkAuthenticationManager:LoadAuthenticatedNotetakerInClockWork:LookupMethod=ByCustomField is not supported for notetakers");
						}
					}
					else
					{
						NotetakerBase notetakerBase2 = (text.Length > 0) ? notetakingManager.LoadNotetakerBaseByEmail(text) : null;
						bool flag3 = notetakerBase2 != null;
						if (flag3)
						{
							return notetakerBase2;
						}
						bool flag4 = usernameToCheckWith.Length > 0;
						if (flag4)
						{
							return notetakingManager.LoadNotetakerBaseByEmail(usernameToCheckWith);
						}
					}
				}
				else
				{
					NotetakerBase notetakerBase3 = (text2.Length > 0) ? notetakingManager.LoadNotetakerBaseByStudentNumber(text2) : null;
					if (VerboseLogging)
					{
						CWLogger.Logger.Trace("Common.Core.AuthenticationAuthorization.ClockWorkAuthenticationManager.LookupAuthenticatedNotetakerInClockWork:BySnum:snum={0}:res={1}", text2, ((notetakerBase3 != null) ? notetakerBase3.ServiceProviderId.ToString() : null) ?? "NULL");
					}
					bool flag5 = notetakerBase3 != null;
					if (flag5)
					{
						return notetakerBase3;
					}
					bool flag6 = usernameToCheckWith.Length > 0;
					if (flag6)
					{
						return notetakingManager.LoadNotetakerBaseByStudentNumber(usernameToCheckWith);
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000025C8 File Offset: 0x000007C8
		private PersonBase LookupAuthenticatedStudentInClockWork(AuthorizationContextItem StudentContext, ExternalUserInfo externalUserInfo, bool VerboseLogging, string usernameToCheckWith)
		{
			bool flag = StudentContext.IsDisabled || externalUserInfo.IsExternalUserInfoEmpty();
			PersonBase result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = (externalUserInfo.Email ?? "").Trim();
				string text2 = (externalUserInfo.StudentNumber ?? "").Trim();
				eLookupMethod lookupMethod = StudentContext.LookupMethod;
				eLookupMethod eLookupMethod = lookupMethod;
				if (eLookupMethod != eLookupMethod.ByUsername)
				{
					if (eLookupMethod != eLookupMethod.ByEmail)
					{
						if (eLookupMethod != eLookupMethod.ByCustomField)
						{
							IPeopleManager peopleManager = new PeopleManager(this.OpContext);
							PersonBase personBase = (text2.Length < 1) ? null : peopleManager.LoadPersonByStudentNumber(text2);
							CWLogger.Logger.Debug("ClockWorkAuthenticationManager:LookupAuthenticatedStudentInClockWork:Snum={0}:usernameToCheckWith={1}:resPid={2}", text2, usernameToCheckWith, ((personBase != null) ? personBase.PersonId.ToString() : null) ?? "NULL");
							bool flag2 = personBase != null;
							if (flag2)
							{
								return personBase;
							}
							bool flag3 = usernameToCheckWith.Length > 0;
							if (flag3)
							{
								return peopleManager.LoadPersonByStudentNumber(usernameToCheckWith);
							}
							goto IL_281;
						}
					}
					else
					{
						IStudentCommonInfoManager studentCommonInfoManager = new StudentCommonInfoManager(this.OpContext);
						PersonBase personBase2 = (text.Length < 1) ? null : studentCommonInfoManager.LoadStudentByEmailAddress(text);
						bool flag4 = personBase2 != null;
						if (flag4)
						{
							return personBase2;
						}
						bool flag5 = usernameToCheckWith.Length > 0;
						if (flag5)
						{
							return studentCommonInfoManager.LoadStudentByEmailAddress(usernameToCheckWith);
						}
						goto IL_281;
					}
				}
				bool flag6 = usernameToCheckWith.Length < 1;
				if (flag6)
				{
					CWLogger.Logger.Error("ClockWorkAuthenticationManager:LookupAuthenticatedStudentInClockWork:LookupMethod={0}:Missing username", StudentContext.LookupMethod.ToString());
				}
				else
				{
					bool flag7 = StudentContext.LookupMethodCid > 0;
					if (flag7)
					{
						ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
						DynamicField dynamicField = (DynamicField)cacheStorageManager[eServerCacheItemType.uWebAuthenticationCustomFieldStudent];
						bool flag8 = dynamicField == null;
						if (flag8)
						{
							IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
							dynamicField = dynamicFieldManager.LoadFieldByControlId(StudentContext.LookupMethodCid);
							cacheStorageManager.Insert(eServerCacheItemType.uWebAuthenticationCustomFieldStudent, dynamicField, TimeSpan.FromMinutes(60.0));
						}
						bool flag9 = dynamicField != null;
						if (flag9)
						{
							IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
							IList<PersonBase> list = dynamicDataManager.LoadStudentByDataItem(eDynamicFormType.PerStudent, dynamicField, usernameToCheckWith);
							bool flag10 = list == null || list.Count < 1;
							if (flag10)
							{
								return null;
							}
							return list[0];
						}
					}
					else
					{
						CWLogger.Logger.Error("ClockWorkAuthenticationManager:LookupAuthenticatedStudentInClockWork:LookupMethod={0}:MissingLookupMethodCid", StudentContext.LookupMethod.ToString());
					}
				}
				IL_281:
				result = null;
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000285C File Offset: 0x00000A5C
		private PersonBase LookupAuthenticatedStaffInClockWork(AuthorizationContextItem StaffContext, ExternalUserInfo externalUserInfo, bool VerboseLogging, string usernameToCheckWith)
		{
			bool flag = StaffContext.IsDisabled || externalUserInfo.IsExternalUserInfoEmpty();
			PersonBase result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = (externalUserInfo.Email ?? "").Trim();
				string text2 = (externalUserInfo.StudentNumber ?? "").Trim();
				eLookupMethod lookupMethod = StaffContext.LookupMethod;
				eLookupMethod eLookupMethod = lookupMethod;
				if (eLookupMethod != eLookupMethod.ByUsername)
				{
					if (eLookupMethod != eLookupMethod.ByEmail)
					{
						if (eLookupMethod != eLookupMethod.ByCustomField)
						{
							IPeopleManager peopleManager = new PeopleManager(this.OpContext);
							PersonBase personBase = (text2.Length > 0) ? peopleManager.LoadPersonByStudentNumber(text2) : null;
							bool flag2 = personBase != null;
							if (flag2)
							{
								return personBase;
							}
							bool flag3 = usernameToCheckWith.Length > 0;
							if (flag3)
							{
								personBase = peopleManager.LoadPersonByStudentNumber(usernameToCheckWith);
							}
							bool flag4 = personBase != null;
							if (flag4)
							{
								return personBase;
							}
							CWLogger.Logger.Trace("ClockWorkAuthenticationManager:LookupAuthenticatedStaffInClockWork:FailedToLookupStaffByEmployeeId:LookupMethod={0}:username={1}", StaffContext.LookupMethod.ToString(), usernameToCheckWith ?? "NULL");
							goto IL_2EC;
						}
					}
					else
					{
						IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(this.OpContext);
						PersonBase personBase2 = (text.Length > 0) ? staffCommonInfoManager.LoadStaffByEmail(text) : null;
						bool flag5 = personBase2 != null;
						if (flag5)
						{
							return personBase2;
						}
						bool flag6 = usernameToCheckWith.Length > 0;
						if (flag6)
						{
							return staffCommonInfoManager.LoadStaffByEmail(usernameToCheckWith);
						}
						goto IL_2EC;
					}
				}
				bool flag7 = usernameToCheckWith.Length < 1;
				if (flag7)
				{
					CWLogger.Logger.Error("ClockWorkAuthenticationManager:LookupAuthenticatedStaffInClockWork:LookupMethod={0}:Missing username", StaffContext.LookupMethod.ToString());
				}
				else
				{
					bool flag8 = StaffContext.LookupMethodCid > 0;
					if (flag8)
					{
						ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
						DynamicField dynamicField = (DynamicField)cacheStorageManager[eServerCacheItemType.uWebAuthenticationCustomFieldStaff];
						bool flag9 = dynamicField == null;
						if (flag9)
						{
							IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
							dynamicField = dynamicFieldManager.LoadFieldByControlId(StaffContext.LookupMethodCid);
							cacheStorageManager.Insert(eServerCacheItemType.uWebAuthenticationCustomFieldStaff, dynamicField, TimeSpan.FromMinutes(60.0));
						}
						bool flag10 = dynamicField != null;
						if (flag10)
						{
							IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
							IList<PersonBase> list = dynamicDataManager.LoadStudentByDataItem(eDynamicFormType.PerStudent, dynamicField, usernameToCheckWith);
							bool flag11 = list != null && list.Count >= 1;
							if (flag11)
							{
								return list[0];
							}
							CWLogger.Logger.Trace("Common.Core.AuthenticationAuthorization.ClockWorkAuthenticationManager.LookupAuthenticatedStaffInClockWork:Failed to locate username for staff:cid1={0}:cid2={1}:username={2}", StaffContext.LookupMethodCid.ToString(), dynamicField.ControlId.ToString(), usernameToCheckWith ?? "NULL");
							return null;
						}
						else
						{
							CWLogger.Logger.Warn("Common.Core.AuthenticationAuthorization.ClockWorkAuthenticationManager.LookupAuthenticatedStaffInClockWork:Failed to load username field:cid={0}", StaffContext.LookupMethodCid.ToString());
						}
					}
					else
					{
						CWLogger.Logger.Error("ClockWorkAuthenticationManager:LookupAuthenticatedStudentInClockWork:LookupMethod={0}:MissingLookupMethodCid", StaffContext.LookupMethod.ToString());
					}
				}
				IL_2EC:
				result = null;
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002B5C File Offset: 0x00000D5C
		private string ContextToString(AuthorizationContext context)
		{
			bool flag = context == null;
			string result;
			if (flag)
			{
				result = "NULL";
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("AuthorizationContext");
				foreach (AuthorizationContextItem item in context.ContextItems)
				{
					stringBuilder.AppendLine(this.AuthContextItemToString(item));
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002BE4 File Offset: 0x00000DE4
		private string AuthContextItemToString(AuthorizationContextItem item)
		{
			bool flag = item == null;
			string result;
			if (flag)
			{
				result = "NULL";
			}
			else
			{
				result = string.Concat(new string[]
				{
					"Item=",
					item.GetAuthorizationContextDisplayString(),
					";LookupMethod=",
					item.LookupMethod.ToString(),
					";LookupMethodCid=",
					item.LookupMethodCid.ToString(),
					";OrderNum=",
					item.OrderId.ToString(),
					";isDisabled=",
					item.IsDisabled.ToString()
				});
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002C94 File Offset: 0x00000E94
		public PersonBase FindStudentByUserName(int Cid, string UserName)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
			IList<PersonBase> list = dynamicDataManager.LoadStudentByDataItem(eDynamicFormType.PerStudent, dynamicFieldManager.LoadFieldByControlId(Cid), UserName);
			return (list == null || list.Count < 1) ? null : list[0];
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public PersonBase LoadStudentByStudentNumber(string StudentNumber)
		{
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			return peopleManager.LoadPersonByStudentNumber(StudentNumber);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002D0C File Offset: 0x00000F0C
		public LookupInstructor LoadInstructorByUsername(string username)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(this.OpContext);
			return lookupInstructorManager.LoadInstructorByUsername(username);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002D34 File Offset: 0x00000F34
		public LookupInstructor LoadInstructorByEmail(string email)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(this.OpContext);
			return lookupInstructorManager.LoadInstructorByEmail(email);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002D5C File Offset: 0x00000F5C
		public LookupInstructor LoadInstructorByEmployeeId(string employeeId)
		{
			ILookupInstructorManager lookupInstructorManager = new LookupInstructorManager(this.OpContext);
			return lookupInstructorManager.LoadInstructorByEmployeeId(employeeId);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002D84 File Offset: 0x00000F84
		public AlternateContact LoadAlternateContactById(int AlternateContactId)
		{
			IAlternateContactManager alternateContactManager = new AlternateContactManager(this.OpContext);
			return alternateContactManager.LoadAlternateContactById(AlternateContactId);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002DAC File Offset: 0x00000FAC
		public AlternateContact LoadAlternateContactByEmployeeId(string EmployeeId)
		{
			IAlternateContactManager alternateContactManager = new AlternateContactManager(this.OpContext);
			return alternateContactManager.LoadAlternateContactByEmployeeId(EmployeeId);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002DD4 File Offset: 0x00000FD4
		public AlternateContact LoadAlternateContactByUsername(string Username)
		{
			IAlternateContactManager alternateContactManager = new AlternateContactManager(this.OpContext);
			return alternateContactManager.LoadAlternateContactByUsername(Username);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002DFC File Offset: 0x00000FFC
		public ClockWorkUser LookupAuthenticatedUserInClockWork(AuthorizationContext Context, ExternalUserInfo externalUserInfo, bool verboseLogging)
		{
			if (verboseLogging)
			{
				CWLogger.Logger.Debug("LookupAuthenticatedUserInClockWork:BeginAuthorization:Context={0}:ExternalUserInfo={1}", this.ContextToString(Context), externalUserInfo.GetExternalUserInfoDisplayString());
			}
			bool flag = Context == null || externalUserInfo.IsExternalUserInfoEmpty();
			ClockWorkUser result;
			if (flag)
			{
				CWLogger.Logger.Warn("LookupAuthenticatedUserInClockWork:Context is null or external user info is empty");
				result = null;
			}
			else
			{
				bool flag2 = Context.ContextItems == null;
				if (flag2)
				{
					Context.ContextItems = new List<AuthorizationContextItem>();
				}
				ClockWorkUser clockWorkUser = null;
				string text = (externalUserInfo.UserName ?? "").Trim();
				foreach (AuthorizationContextItem authorizationContextItem in Context.ContextItems)
				{
					bool flag3 = !authorizationContextItem.IsDisabled;
					if (flag3)
					{
						string text2 = string.IsNullOrEmpty(authorizationContextItem.UsernamePostfix) ? text : (text + authorizationContextItem.UsernamePostfix.Trim());
						eAuthorizationContextItemType contextItemType = authorizationContextItem.ContextItemType;
						eAuthorizationContextItemType eAuthorizationContextItemType = contextItemType;
						switch (eAuthorizationContextItemType)
						{
						case eAuthorizationContextItemType.Staff:
						{
							PersonBase personBase = this.LookupAuthenticatedStaffInClockWork(authorizationContextItem, externalUserInfo, verboseLogging, text2);
							bool flag4 = personBase != null;
							if (flag4)
							{
								personBase.Student_no = "";
								bool flag5 = clockWorkUser == null;
								if (flag5)
								{
									clockWorkUser = new ClockWorkUser
									{
										Username = externalUserInfo.UserName,
										StudentNumber = externalUserInfo.StudentNumber
									};
								}
								bool flag6 = clockWorkUser.ClockWorkPid < 1;
								if (flag6)
								{
									clockWorkUser.ClockWorkPid = personBase.PersonId;
									clockWorkUser.StudentNumber = externalUserInfo.StudentNumber;
								}
								else
								{
									CWLogger.Logger.Warn("LookupAuthenticatedUserInClockWork:LookupStaff:ClockWorkPid={0}:StaffPid={1}:Can'tUseBoth_ChoseStudentPid", clockWorkUser.ClockWorkPid.ToString(), personBase.PersonId.ToString());
								}
								if (verboseLogging)
								{
									CWLogger.Logger.Debug("LookupAuthenticatedUserInClockWork:staff:Found:Pid={0}", personBase.PersonId.ToString());
								}
							}
							else if (verboseLogging)
							{
								CWLogger.Logger.Debug("LookupAuthenticatedUserInClockWork:staff:FailedToLocate");
							}
							break;
						}
						case eAuthorizationContextItemType.Student:
						{
							PersonBase personBase2 = this.LookupAuthenticatedStudentInClockWork(authorizationContextItem, externalUserInfo, verboseLogging, text2);
							bool flag7 = personBase2 != null;
							if (flag7)
							{
								bool flag8 = clockWorkUser == null;
								if (flag8)
								{
									clockWorkUser = new ClockWorkUser
									{
										Username = externalUserInfo.UserName,
										StudentNumber = externalUserInfo.StudentNumber
									};
								}
								clockWorkUser.ClockWorkPid = personBase2.PersonId;
								clockWorkUser.StudentNumber = personBase2.Student_no;
								if (verboseLogging)
								{
									CWLogger.Logger.Debug("LookupAuthenticatedUserInClockWork:Student:Found:Pid={0}", personBase2.PersonId.ToString());
								}
							}
							else if (verboseLogging)
							{
								CWLogger.Logger.Debug("LookupAuthenticatedUserInClockWork:Student:FailedToLocate");
							}
							break;
						}
						case (eAuthorizationContextItemType)3:
							goto IL_507;
						case eAuthorizationContextItemType.Notetaking:
						{
							NotetakerBase notetakerBase = this.LookupAuthenticatedNotetakerInClockWork(authorizationContextItem, externalUserInfo, verboseLogging, text2);
							bool flag9 = notetakerBase != null;
							if (flag9)
							{
								bool flag10 = clockWorkUser == null;
								if (flag10)
								{
									clockWorkUser = new ClockWorkUser
									{
										Username = externalUserInfo.UserName,
										StudentNumber = externalUserInfo.StudentNumber
									};
								}
								clockWorkUser.ClockWorkNid = notetakerBase.ServiceProviderId;
								clockWorkUser.StudentNumber = ((!string.IsNullOrEmpty(notetakerBase.Student_no)) ? notetakerBase.Student_no : externalUserInfo.StudentNumber);
								if (verboseLogging)
								{
									CWLogger.Logger.Debug("LookupAuthenticatedUserInClockWork:notetaker:Found:Nid={0}", notetakerBase.ServiceProviderId.ToString());
								}
							}
							else if (verboseLogging)
							{
								CWLogger.Logger.Debug("LookupAuthenticatedUserInClockWork:notetaker:FailedToLocate");
							}
							break;
						}
						default:
							if (eAuthorizationContextItemType != eAuthorizationContextItemType.Instructor)
							{
								if (eAuthorizationContextItemType != eAuthorizationContextItemType.AlternateContact)
								{
									goto IL_507;
								}
								AlternateContact alternateContact = this.LookupAuthenticatedAltContactInClockWork(authorizationContextItem, externalUserInfo, verboseLogging, text2);
								bool flag11 = alternateContact != null;
								if (flag11)
								{
									bool flag12 = clockWorkUser == null;
									if (flag12)
									{
										clockWorkUser = new ClockWorkUser
										{
											Username = externalUserInfo.UserName,
											StudentNumber = externalUserInfo.StudentNumber
										};
									}
									clockWorkUser.ClockWorkAltContactId = alternateContact.AlternateContactId;
									clockWorkUser.StudentNumber = ((!string.IsNullOrEmpty(alternateContact.EmployeeId)) ? alternateContact.EmployeeId : externalUserInfo.StudentNumber);
									if (verboseLogging)
									{
										CWLogger.Logger.Debug("LookupAuthenticatedUserInClockWork:AltContact:Found:Pid={0}", alternateContact.AlternateContactId.ToString());
									}
								}
								else if (verboseLogging)
								{
									CWLogger.Logger.Debug("LookupAuthenticatedUserInClockWork:AltContact:FailedToLocate");
								}
							}
							else
							{
								LookupInstructor lookupInstructor = this.LookupAuthenticatedInstructorInClockWork(authorizationContextItem, externalUserInfo, verboseLogging, text2);
								bool flag13 = lookupInstructor != null;
								if (flag13)
								{
									bool flag14 = clockWorkUser == null;
									if (flag14)
									{
										clockWorkUser = new ClockWorkUser
										{
											Username = externalUserInfo.UserName,
											StudentNumber = externalUserInfo.StudentNumber
										};
									}
									clockWorkUser.ClockWorkIid = lookupInstructor.InstructorId;
									clockWorkUser.StudentNumber = ((!string.IsNullOrEmpty(lookupInstructor.EmployeeId)) ? lookupInstructor.EmployeeId : externalUserInfo.StudentNumber);
									if (verboseLogging)
									{
										CWLogger.Logger.Debug("LookupAuthenticatedUserInClockWork:instructor:Found:Iid={0}", lookupInstructor.InstructorId.ToString());
									}
								}
								else if (verboseLogging)
								{
									CWLogger.Logger.Debug("LookupAuthenticatedUserInClockWork:instructor:FailedToLocate:usernameToCheckWith={0}", "\"" + (text2 ?? "NULL") + "\"");
								}
							}
							break;
						}
						continue;
						IL_507:
						CWLogger.Logger.Error("ClockWorkAuthenticationManager:Unrecognized context item type: {0}", authorizationContextItem.ContextItemType.ToString());
					}
					else if (verboseLogging)
					{
						CWLogger.Logger.Debug("Skipped {0} because it is marked as disabled.", authorizationContextItem.GetAuthorizationContextDisplayString());
					}
				}
				if (verboseLogging)
				{
					bool flag15 = clockWorkUser == null;
					if (flag15)
					{
						CWLogger.Logger.Trace("LookupAuthenticatedUserInClockWork:LoginFail:externalUserInfo={0}", externalUserInfo.GetExternalUserInfoDisplayString());
					}
					else
					{
						CWLogger.Logger.Trace("LookupAuthenticatedUserInClockWork:LoginSuccess:externalUserInfo={0}:User={1}", externalUserInfo.GetExternalUserInfoDisplayString(), this.UserToString(clockWorkUser));
					}
				}
				result = clockWorkUser;
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000033E0 File Offset: 0x000015E0
		private List<ClockWorkAuthenticationManager.AuthenticationToDoItem> SetupAuthenticationToDoList(IEnumerable<AuthenticationContextItem> contextItems, string binPath, bool verboseLoggingEnabled)
		{
			List<ClockWorkAuthenticationManager.AuthenticationToDoItem> list = new List<ClockWorkAuthenticationManager.AuthenticationToDoItem>();
			IOrderedEnumerable<AuthenticationContextItem> orderedEnumerable = from g in contextItems
			where !g.IsDisabled
			orderby g.OrderId
			select g;
			foreach (AuthenticationContextItem authenticationContextItem in orderedEnumerable)
			{
				switch (authenticationContextItem.ContextItemType)
				{
				case eAuthenticationContextItemType.ClockWork:
				{
					bool flag = authenticationContextItem.Args == null;
					if (flag)
					{
						authenticationContextItem.Args = new Dictionary<string, string>();
					}
					IDictionary<string, string> args = authenticationContextItem.Args;
					bool flag2 = !args.ContainsKey("binPath");
					if (flag2)
					{
						args.Add("binPath", binPath);
					}
					list.Add(new ClockWorkAuthenticationManager.AuthenticationToDoItem(new Func<AuthenticationRequestParameters, AuthenticationResultParameters>(this.TryToAuthenticateClockWork), authenticationContextItem));
					break;
				}
				case eAuthenticationContextItemType.Ldap:
					list.Add(new ClockWorkAuthenticationManager.AuthenticationToDoItem(new Func<AuthenticationRequestParameters, AuthenticationResultParameters>(this.TryToAuthenticateLdap), authenticationContextItem));
					break;
				case eAuthenticationContextItemType.ActiveDirectory:
					list.Add(new ClockWorkAuthenticationManager.AuthenticationToDoItem(new Func<AuthenticationRequestParameters, AuthenticationResultParameters>(this.TryToAuthenticateActiveDirectory), authenticationContextItem));
					break;
				case eAuthenticationContextItemType.CAS:
					list.Add(new ClockWorkAuthenticationManager.AuthenticationToDoItem(new Func<AuthenticationRequestParameters, AuthenticationResultParameters>(this.TryToAuthenticationCAS), authenticationContextItem));
					break;
				case eAuthenticationContextItemType.Shibboleth:
					list.Add(new ClockWorkAuthenticationManager.AuthenticationToDoItem(new Func<AuthenticationRequestParameters, AuthenticationResultParameters>(this.TryToAuthenticateShibboleth), authenticationContextItem));
					break;
				case eAuthenticationContextItemType.Portal:
					list.Add(new ClockWorkAuthenticationManager.AuthenticationToDoItem(new Func<AuthenticationRequestParameters, AuthenticationResultParameters>(this.TryToAuthenticationPortalHashing), authenticationContextItem));
					break;
				case eAuthenticationContextItemType.Custom:
					goto IL_1CE;
				case eAuthenticationContextItemType.PortalGuard:
					list.Add(new ClockWorkAuthenticationManager.AuthenticationToDoItem(new Func<AuthenticationRequestParameters, AuthenticationResultParameters>(this.TryToAuthenticatePortalGuard), authenticationContextItem));
					break;
				case eAuthenticationContextItemType.Adfs:
					list.Add(new ClockWorkAuthenticationManager.AuthenticationToDoItem(new Func<AuthenticationRequestParameters, AuthenticationResultParameters>(this.TryToAuthenticateAdfs), authenticationContextItem));
					break;
				default:
					goto IL_1CE;
				}
				continue;
				IL_1CE:
				CWLogger.Logger.Warn("Common.Core.Authentication.ClockWorkAuthenticationManager:Uknown ContextitemType={0}", authenticationContextItem.ContextItemType.ToString());
			}
			return list;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00003620 File Offset: 0x00001820
		private ClockWorkAuthenticationManager.AuthenticationTodoListResult ExecuteAuthenticationTodoList(List<ClockWorkAuthenticationManager.AuthenticationToDoItem> authenticationToDoList, string username, string password, AuthenticationArgs AuthenticationArgs, bool verboseLoggingEnabled)
		{
			AuthenticationRequestParameters authenticationRequestParameters = new AuthenticationRequestParameters(username, password, AuthenticationArgs, verboseLoggingEnabled);
			foreach (ClockWorkAuthenticationManager.AuthenticationToDoItem authenticationToDoItem in authenticationToDoList)
			{
				CWLogger logger = CWLogger.Logger;
				string message = "Common.Core.AuthenticationAuthorization.ClockWorkAuthenticationManager:AuthenticateAndAuthorizeUser:Starting attemp:Username={0}:AttemptFor={1}";
				object arg = username ?? "NULL";
				object obj;
				if (authenticationToDoItem == null)
				{
					obj = null;
				}
				else
				{
					AuthenticationContextItem contextItem = authenticationToDoItem.ContextItem;
					obj = ((contextItem != null) ? contextItem.ContextItemType.ToString() : null);
				}
				logger.Trace(message, arg, obj ?? ((authenticationToDoItem == null) ? "F is null - attempt is aborted" : "f.ContextItem is null - attempt is aborted"));
				bool flag = ((authenticationToDoItem != null) ? authenticationToDoItem.ContextItem : null) == null;
				if (!flag)
				{
					authenticationRequestParameters.ContextItem = authenticationToDoItem.ContextItem;
					AuthenticationResultParameters authenticationResultParameters = authenticationToDoItem.Func(authenticationRequestParameters);
					bool isSuccess = authenticationResultParameters.IsSuccess;
					if (isSuccess)
					{
						CWLogger logger2 = CWLogger.Logger;
						string message2 = "Common.Core.AuthenticationAuthorization.ClockWorkAuthenticationManager:AuthenticateAndAuthorizeUser:SuccessfulAttempt:Username={0}:ExternalUserInfo={1}:AttemptFor={2}";
						object arg2 = username ?? "NULL";
						object externalUserInfoDisplayString = authenticationResultParameters.ExternalUserInfo.GetExternalUserInfoDisplayString();
						AuthenticationContextItem contextItem2 = authenticationToDoItem.ContextItem;
						logger2.Info(message2, arg2, externalUserInfoDisplayString, ((contextItem2 != null) ? contextItem2.ContextItemType.ToString() : null) ?? "NULL");
						return new ClockWorkAuthenticationManager.AuthenticationTodoListResult
						{
							Res = authenticationResultParameters,
							Req = authenticationRequestParameters
						};
					}
					CWLogger.Logger.Warn("Common.Core.AuthenticationAuthorization.ClockWorkAuthenticationManager:AuthenticateAndAuthorizeUser:FailedAttempt:Username={0}:AttemptFor={1}:Message={2}", username ?? "NULL", (authenticationToDoItem.ContextItem == null) ? "NULL" : authenticationToDoItem.ContextItem.ContextItemType.ToString(), authenticationResultParameters.LoggingMessage ?? "NULL");
				}
			}
			return new ClockWorkAuthenticationManager.AuthenticationTodoListResult
			{
				Req = authenticationRequestParameters
			};
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000037F0 File Offset: 0x000019F0
		private ClockWorkAuthenticationManager.PostAuthenticationReportResult TryToRunPostAuthenticationReport(AuthenticationRequestParameters req, AuthenticationResultParameters res, string binPath)
		{
			try
			{
				int settingValue = SettingManager.CurrentInstance.GetSettingValue<int>(Setting.LOGIN_PostAuthenticationReport);
				bool flag = settingValue <= 0;
				if (flag)
				{
					return null;
				}
				List<ReportParameter> reportParameters = this.GetReportParameters(req);
				this.AddOrUpdateParameterValue(ref reportParameters, "contextitemtype", (req != null && req.ContextItem != null) ? ((int)req.ContextItem.ContextItemType).ToString() : "");
				this.AddOrUpdateParameterValue(ref reportParameters, "authenticated", (res != null && res.IsSuccess) ? "1" : "0");
				ExternalUserInfo externalUserInfo = (res != null) ? res.ExternalUserInfo : null;
				bool flag2 = externalUserInfo != null;
				if (flag2)
				{
					this.AddOrUpdateParameterValue(ref reportParameters, "resusername", externalUserInfo.UserName ?? "");
					this.AddOrUpdateParameterValue(ref reportParameters, "resstudent_no", externalUserInfo.StudentNumber ?? "");
					this.AddOrUpdateParameterValue(ref reportParameters, "resemail", externalUserInfo.Email ?? "");
				}
				RunReportResult runReportResult = this.RunReport(settingValue, reportParameters, binPath);
				bool flag3 = runReportResult == null || runReportResult.ReportStatus == null || runReportResult.ReportStatus.LastStatusStep != eRunStatusStep.CompletedSuccessfully || runReportResult.PrimaryData == null || runReportResult.PrimaryData.Table == null || runReportResult.PrimaryData.Table.Rows.Count <= 0;
				if (flag3)
				{
					CWLogger.Logger.Error("Common.Core.AuthenticationAuthorization.ClockWorkAuthenticationManager:TryToRunPostAuthenticationReport:ReportFailed:errmsg={0}", (runReportResult == null || runReportResult.ReportStatus == null || runReportResult.ReportStatus.ErrorMessage == null) ? "NULL" : runReportResult.ReportStatus.ErrorMessage);
					return null;
				}
				DataTable table = runReportResult.PrimaryData.Table;
				bool flag4 = table == null || table.Rows.Count < 1;
				if (flag4)
				{
					return null;
				}
				DataRow dataRow = table.Rows[0];
				return new ClockWorkAuthenticationManager.PostAuthenticationReportResult
				{
					OverrideIsAuthenticated = ((table.Columns.Contains("authenticated") && !(dataRow["authenticated"] is DBNull)) ? new bool?((bool)dataRow["authenticated"]) : null),
					OverrideStudentNumber = (table.Columns.Contains("student_no") ? dataRow["student_no"].ToString().Trim() : null),
					OverrideUsername = (table.Columns.Contains("username") ? dataRow["username"].ToString().Trim() : null),
					OverrideEmail = (table.Columns.Contains("email") ? dataRow["email"].ToString().Trim() : null)
				};
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.Core.AuthenticationAuthorization.ClockWorkAuthenticationManager:TryToRunPostAuthenticationReport:Failed:ex={0}", ex.ToString());
			}
			return null;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003B10 File Offset: 0x00001D10
		public AuthenticationAndAuthorizationResult AuthenticateAndAuthorizeUser(AuthenticationContext AuthenticationContext, AuthorizationContext AuthorizationContext, string UserName, string Password, AuthenticationArgs AuthenticationArgs, string BinPath, bool VerboseLogging = false)
		{
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			bool flag = AuthenticationContext == null;
			if (flag)
			{
				AuthenticationContext = webSettingManager.GetSettingValue<string>(Setting.LOGIN_AuthenticationContext).GetAuthenticationContextFromXml();
			}
			bool flag2 = AuthenticationContext == null;
			if (flag2)
			{
				AuthenticationContext = new AuthenticationContext();
			}
			bool flag3 = AuthenticationContext.ContextItems == null;
			if (flag3)
			{
				AuthenticationContext.ContextItems = new List<AuthenticationContextItem>();
			}
			bool flag4 = AuthenticationContext.ContextItems.Count < 1;
			if (flag4)
			{
				AuthenticationContext.ContextItems.Add(new AuthenticationContextItem
				{
					ContextItemType = eAuthenticationContextItemType.ClockWork,
					Args = new Dictionary<string, string>
					{
						{
							"all",
							"1"
						}
					}
				});
			}
			bool flag5 = AuthorizationContext == null;
			if (flag5)
			{
				AuthorizationContext = webSettingManager.GetSettingValue<string>(Setting.LOGIN_AuthorizationContext).GetAuthorizationContextFromXml();
			}
			List<ClockWorkAuthenticationManager.AuthenticationToDoItem> authenticationToDoList = this.SetupAuthenticationToDoList(AuthenticationContext.ContextItems, BinPath, VerboseLogging);
			ClockWorkAuthenticationManager.AuthenticationTodoListResult authenticationTodoListResult = this.ExecuteAuthenticationTodoList(authenticationToDoList, UserName, Password, AuthenticationArgs, VerboseLogging);
			AuthenticationResultParameters authenticationResultParameters = authenticationTodoListResult.Res;
			ClockWorkAuthenticationManager.PostAuthenticationReportResult postAuthenticationReportResult = this.TryToRunPostAuthenticationReport(authenticationTodoListResult.Req, authenticationResultParameters, BinPath);
			CWLogger.Logger.Debug("ClockWorkAuthenticationManager:AuthenticateAndAuthorizeUser:postAuthReportRes={0}", ((postAuthenticationReportResult != null) ? postAuthenticationReportResult.ToString() : null) ?? "NULL");
			string text = (authenticationResultParameters != null && authenticationResultParameters.ExternalUserInfo != null && authenticationResultParameters.ExternalUserInfo.UserName != null) ? authenticationResultParameters.ExternalUserInfo.UserName.Trim() : "";
			string text2 = (authenticationResultParameters != null && authenticationResultParameters.ExternalUserInfo != null && authenticationResultParameters.ExternalUserInfo.StudentNumber != null) ? authenticationResultParameters.ExternalUserInfo.StudentNumber.Trim() : "";
			bool flag6 = postAuthenticationReportResult != null;
			if (flag6)
			{
				bool flag7 = postAuthenticationReportResult.OverrideIsAuthenticated != null;
				if (flag7)
				{
					bool flag8 = authenticationResultParameters == null;
					if (flag8)
					{
						authenticationResultParameters = new AuthenticationResultParameters();
					}
					authenticationResultParameters.IsSuccess = postAuthenticationReportResult.OverrideIsAuthenticated.Value;
				}
				bool flag9 = !string.IsNullOrEmpty(postAuthenticationReportResult.OverrideStudentNumber);
				if (flag9)
				{
					text2 = postAuthenticationReportResult.OverrideStudentNumber;
					bool flag10 = text2 != null && authenticationResultParameters != null;
					if (flag10)
					{
						bool flag11 = authenticationResultParameters.ExternalUserInfo == null;
						if (flag11)
						{
							authenticationResultParameters.ExternalUserInfo = new ExternalUserInfo();
						}
						authenticationResultParameters.ExternalUserInfo.StudentNumber = text2;
					}
				}
				bool flag12 = !string.IsNullOrEmpty(postAuthenticationReportResult.OverrideUsername);
				if (flag12)
				{
					text = postAuthenticationReportResult.OverrideUsername;
					bool flag13 = text != null && authenticationResultParameters != null;
					if (flag13)
					{
						bool flag14 = authenticationResultParameters.ExternalUserInfo == null;
						if (flag14)
						{
							authenticationResultParameters.ExternalUserInfo = new ExternalUserInfo();
						}
						authenticationResultParameters.ExternalUserInfo.UserName = text;
					}
				}
			}
			bool flag15 = authenticationResultParameters == null || !authenticationResultParameters.IsSuccess;
			AuthenticationAndAuthorizationResult result;
			if (flag15)
			{
				result = new AuthenticationAndAuthorizationResult
				{
					PassedAuthentication = false
				};
			}
			else
			{
				AuthenticationAndAuthorizationResult authenticationAndAuthorizationResult = new AuthenticationAndAuthorizationResult();
				authenticationAndAuthorizationResult.PassedAuthentication = true;
				ClockWorkUser clockWorkUser;
				if ((clockWorkUser = this.LookupAuthenticatedUserInClockWork(AuthorizationContext, authenticationResultParameters.ExternalUserInfo, VerboseLogging)) == null)
				{
					ClockWorkUser clockWorkUser2 = new ClockWorkUser();
					clockWorkUser2.Username = text;
					clockWorkUser = clockWorkUser2;
					clockWorkUser2.StudentNumber = text2;
				}
				authenticationAndAuthorizationResult.ClockWorkUser = clockWorkUser;
				result = authenticationAndAuthorizationResult;
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003E14 File Offset: 0x00002014
		private string GetAssemblyLocation(Assembly assembly)
		{
			string result;
			try
			{
				string codeBase = assembly.CodeBase;
				UriBuilder uriBuilder = new UriBuilder(codeBase);
				string text = Uri.UnescapeDataString(uriBuilder.Path);
				text = Path.GetDirectoryName(text);
				text = text + "\\" + Path.GetFileName(assembly.Location);
				result = text;
			}
			catch
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003E7C File Offset: 0x0000207C
		private string GetSecureValueFromArgs(AuthenticationArgs args, string keyNotCaseSensitive)
		{
			return this.GetValueFromArgs(args.SecureArgs, keyNotCaseSensitive);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003E9C File Offset: 0x0000209C
		private string GetInSecureValueFromArgs(AuthenticationArgs args, string keyNotCaseSensitive)
		{
			return this.GetValueFromArgs(args.InsecureArgs, keyNotCaseSensitive);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003EBC File Offset: 0x000020BC
		private string GetInSecureOrSecureValueFromArgs(AuthenticationArgs args, string keyNotCaseSensitive)
		{
			return this.GetSecureValueFromArgs(args, keyNotCaseSensitive) ?? this.GetInSecureValueFromArgs(args, keyNotCaseSensitive);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003EE4 File Offset: 0x000020E4
		private string GetValueFromArgs(IDictionary<string, string> args, string keyNotCaseSensitive)
		{
			bool flag = args.ContainsKey(keyNotCaseSensitive);
			string result;
			if (flag)
			{
				result = args[keyNotCaseSensitive];
			}
			else
			{
				string text = args.Keys.FirstOrDefault((string g) => g.Equals(keyNotCaseSensitive, StringComparison.OrdinalIgnoreCase));
				result = ((text == null) ? null : args[text]);
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003F48 File Offset: 0x00002148
		private AuthenticationResultParameters TryToAuthenticationPortalHashing(AuthenticationRequestParameters Request)
		{
			string fieldName = Request.ContextItem.Args.ContainsKey("username_field") ? (Request.ContextItem.Args["username_field"] ?? "username") : "username";
			string fieldName2 = Request.ContextItem.Args.ContainsKey("date_field") ? (Request.ContextItem.Args["date_field"] ?? "date") : "date";
			string fieldName3 = Request.ContextItem.Args.ContainsKey("token_field") ? (Request.ContextItem.Args["token_field"] ?? "token") : "token";
			string fieldName4 = Request.ContextItem.Args.ContainsKey("extra_field") ? (Request.ContextItem.Args["extra_field"] ?? "extra") : "extra";
			string text = Request.ContextItem.Args.ContainsKey("student_no_field") ? (Request.ContextItem.Args["student_no_field"] ?? "").Trim() : "";
			string text2 = Request.ContextItem.Args.ContainsKey("email_field") ? (Request.ContextItem.Args["email_field"] ?? "").Trim() : "";
			string text3 = Request.ContextItem.Args.ContainsKey("hash_type") ? (Request.ContextItem.Args["hash_type"] ?? "").Trim() : "";
			eHashingType hashingType = (text3.Length > 0 && Enum.IsDefined(typeof(eHashingType), text3)) ? ((eHashingType)Enum.Parse(typeof(eHashingType), text3)) : eHashingType.ClockWorkDefault;
			bool flag = Request.ContextItem.Args.ContainsKey("hashing_uses_hex_encoding") && (Request.ContextItem.Args["hashing_uses_hex_encoding"] ?? "").Trim() == "1";
			bool flag2 = Request.ContextItem.Args.ContainsKey("whole_token_is_base64_encoded") && (Request.ContextItem.Args["whole_token_is_base64_encoded"] ?? "").Trim() == "1";
			string text4 = Request.ContextItem.Args.ContainsKey("override_token_timeout") ? (Request.ContextItem.Args["override_token_timeout"] ?? "") : "";
			int tokenLifetimeInMinutes;
			bool flag3 = text4.Length < 1 || !int.TryParse(text4, out tokenLifetimeInMinutes);
			if (flag3)
			{
				tokenLifetimeInMinutes = 0;
			}
			string text5;
			AuthenticationResultParameters inSecureOrSecureContextValue = this.GetInSecureOrSecureContextValue(Request.AuthenticationArgs, fieldName, out text5);
			bool flag4 = inSecureOrSecureContextValue != null;
			AuthenticationResultParameters result;
			if (flag4)
			{
				result = inSecureOrSecureContextValue;
			}
			else
			{
				string text6;
				inSecureOrSecureContextValue = this.GetInSecureOrSecureContextValue(Request.AuthenticationArgs, fieldName2, out text6);
				bool flag5 = inSecureOrSecureContextValue != null;
				if (flag5)
				{
					result = inSecureOrSecureContextValue;
				}
				else
				{
					string text7;
					inSecureOrSecureContextValue = this.GetInSecureOrSecureContextValue(Request.AuthenticationArgs, fieldName3, out text7);
					text7 = (text7 ?? "").Trim().Replace(" ", "+");
					bool flag6 = inSecureOrSecureContextValue != null;
					if (flag6)
					{
						result = inSecureOrSecureContextValue;
					}
					else
					{
						string text8;
						this.GetInSecureOrSecureContextValue(Request.AuthenticationArgs, fieldName4, out text8);
						text8 = (text8 ?? "").Trim();
						bool flag7 = text8.Length > 0;
						if (flag7)
						{
							text8 = string.Join(",", (from g in text8.Split(new char[]
							{
								','
							})
							select g.Trim() into h
							where h.Length > 0
							select h).Select(delegate(string m)
							{
								string text12;
								this.GetInSecureOrSecureContextValue(Request.AuthenticationArgs, m, out text12);
								return (text12 ?? "").Trim();
							}).ToArray<string>());
						}
						string settingValue = SettingManager.CurrentInstance.GetSettingValue<string>(Setting.LOGIN_Hashing_Authentication_key);
						bool flag8 = string.IsNullOrEmpty(settingValue);
						if (flag8)
						{
							result = new AuthenticationResultParameters
							{
								IsSuccess = false,
								LoggingMessage = "Login hashing authentication key is empty. Check settings."
							};
						}
						else
						{
							string text9 = SettingManager.CurrentInstance.GetSettingValue<string>(Setting.LOGIN_Hashing_Authentication_salt);
							bool flag9 = string.IsNullOrEmpty(text9);
							if (flag9)
							{
								text9 = settingValue;
							}
							bool flag10 = text.Length > 0;
							string text10;
							if (flag10)
							{
								this.GetInSecureOrSecureContextValue(Request.AuthenticationArgs, text, out text10);
							}
							else
							{
								text10 = "";
							}
							bool flag11 = text2.Length > 0;
							string text11;
							if (flag11)
							{
								this.GetInSecureOrSecureContextValue(Request.AuthenticationArgs, text2, out text11);
							}
							else
							{
								text11 = "";
							}
							bool flag12 = flag2 && !string.IsNullOrEmpty(text7);
							if (flag12)
							{
								try
								{
									text7 = Encoding.UTF8.GetString(Convert.FromBase64String(text7));
								}
								catch (Exception ex)
								{
									CWLogger.Logger.Error("ClockWorkAuthenticationManager:TryToAuthenticationPortalHashing:entireTokenIsBase64Encoded:{0}", ex.ToString());
								}
							}
							bool flag13 = flag && !string.IsNullOrEmpty(text7);
							if (flag13)
							{
								try
								{
									string[] array = text7.Split(new char[]
									{
										':'
									});
									bool flag14 = array.Length > 1;
									if (flag14)
									{
										string[] array2 = new string[array.Length];
										array2[0] = array[0];
										for (int i = 1; i < array.Length; i++)
										{
											array2[i] = this.ConvertHexStringToBase64String(array[i]);
										}
										text7 = string.Join(":", array2);
									}
								}
								catch (Exception ex2)
								{
									CWLogger.Logger.Error("ClockWorkAuthenticationManager:TryToAuthenticationPortalHashing:hashingUsesHexEncodingEx:{0}", ex2.ToString());
								}
							}
							IHashingAuthenticationManager hashingAuthenticationManager = new HashingAuthenticationManager(new HashingOperationContext
							{
								HashingKey = settingValue,
								TokenLifetimeInMinutes = tokenLifetimeInMinutes,
								WhoAmI = this.OpContext.WhoAmI
							});
							CWLogger.Logger.Trace("TryToAuthenticationPortalHashing:StampTime={0}:Seed={1}:SecretKey={2}:overrideTokenTimeoutMinutes={3}", new object[]
							{
								text6 ?? "NULL",
								text8 ?? "NULL",
								text9 ?? "NULL",
								tokenLifetimeInMinutes.ToString()
							});
							HashAuthentication hashAuth = new HashAuthentication
							{
								Username = text5,
								HashValue = text7,
								StampTime = text6,
								Seed = text8,
								SecretKey = text9
							};
							bool flag15 = hashingAuthenticationManager.ValidateHash(hashingType, hashAuth);
							bool flag16 = !flag15;
							if (flag16)
							{
								result = new AuthenticationResultParameters
								{
									IsSuccess = false,
									LoggingMessage = "Failed ValidateClockWorkHash"
								};
							}
							else
							{
								result = new AuthenticationResultParameters
								{
									IsSuccess = true,
									ExternalUserInfo = new ExternalUserInfo
									{
										UserName = text5,
										StudentNumber = (text10 ?? ""),
										Email = (text11 ?? "")
									},
									Args = new Dictionary<string, string>()
								};
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00004718 File Offset: 0x00002918
		private string ConvertHexStringToBase64String(string hexString)
		{
			bool flag = hexString.Length % 2 > 0;
			if (flag)
			{
				throw new FormatException("Input string was not in a correct format.");
			}
			bool success = Regex.Match(hexString, "[^a-fA-F0-9]").Success;
			if (success)
			{
				throw new FormatException("Input string was not in a correct format.");
			}
			byte[] array = new byte[hexString.Length / 2];
			for (int i = 0; i < hexString.Length; i += 2)
			{
				array[i / 2] = byte.Parse(hexString.Substring(i, 2), NumberStyles.HexNumber);
			}
			return Convert.ToBase64String(array);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000047AC File Offset: 0x000029AC
		private AuthenticationResultParameters GetSecureContextValue(AuthenticationArgs args, string fieldName, out string val)
		{
			return this.GetContextValue(args.SecureArgs, fieldName, out val);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000047CC File Offset: 0x000029CC
		private AuthenticationResultParameters GetInSecureContextValue(AuthenticationArgs args, string fieldName, out string val)
		{
			return this.GetContextValue(args.InsecureArgs, fieldName, out val);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000047EC File Offset: 0x000029EC
		private AuthenticationResultParameters GetInSecureOrSecureContextValue(AuthenticationArgs args, string fieldName, out string val)
		{
			AuthenticationResultParameters secureContextValue = this.GetSecureContextValue(args, fieldName, out val);
			bool flag = secureContextValue == null || secureContextValue.IsSuccess;
			AuthenticationResultParameters result;
			if (flag)
			{
				result = secureContextValue;
			}
			else
			{
				result = this.GetInSecureContextValue(args, fieldName, out val);
			}
			return result;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00004828 File Offset: 0x00002A28
		private AuthenticationResultParameters GetContextValue(IDictionary<string, string> args, string fieldName, out string val)
		{
			bool flag = !args.ContainsKey(fieldName);
			AuthenticationResultParameters result;
			if (flag)
			{
				val = null;
				result = new AuthenticationResultParameters
				{
					IsSuccess = false,
					LoggingMessage = "Failed to find field: " + fieldName
				};
			}
			else
			{
				val = (args[fieldName] ?? "");
				result = null;
			}
			return result;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00004884 File Offset: 0x00002A84
		private void AddOrUpdateParameterValue(ref List<ReportParameter> parameters, string name, string val)
		{
			string value = val ?? "";
			ReportParameter reportParameter = parameters.FirstOrDefault((ReportParameter g) => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
			bool flag = reportParameter != null;
			if (flag)
			{
				reportParameter.Value = value;
			}
			else
			{
				parameters.Add(new ReportParameter
				{
					Name = name,
					Value = value
				});
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000048F4 File Offset: 0x00002AF4
		private List<ReportParameter> GetReportParameters(AuthenticationRequestParameters Request)
		{
			string val = (Request.UserName ?? "").ToUpper().Trim();
			IDictionary<string, string> args = Request.ContextItem.Args;
			string text = args.ContainsKey("reportArgs") ? args["reportArgs"] : null;
			List<string> list;
			if (!string.IsNullOrEmpty(text))
			{
				list = (from g in text.Split(new char[]
				{
					','
				})
				select g.Trim() into h
				where h.Length > 0
				select h).Distinct<string>().ToList<string>();
			}
			else
			{
				list = new List<string>();
			}
			List<string> source = list;
			string text2 = args.ContainsKey("reportArgsInsecure") ? args["reportArgsInsecure"] : null;
			List<string> list2;
			if (!string.IsNullOrEmpty(text2))
			{
				list2 = (from g in text2.Split(new char[]
				{
					','
				})
				select g.Trim() into h
				where h.Length > 0
				select h).Distinct<string>().ToList<string>();
			}
			else
			{
				list2 = new List<string>();
			}
			List<string> list3 = list2;
			List<ReportParameter> reportParameters = (from q in source
			select new ReportParameter
			{
				Name = q,
				Value = ((Request.AuthenticationArgs != null && Request.AuthenticationArgs != null) ? (this.GetSecureValueFromArgs(Request.AuthenticationArgs, q) ?? "") : "")
			}).ToList<ReportParameter>();
			bool flag = list3.Count > 0;
			if (flag)
			{
				list3 = (from g in list3
				where reportParameters.FirstOrDefault((ReportParameter h) => h.Name.Equals(g, StringComparison.OrdinalIgnoreCase)) == null
				select g).ToList<string>();
				bool flag2 = list3.Count > 0;
				if (flag2)
				{
					reportParameters.AddRange(from q in list3
					select new ReportParameter
					{
						Name = q,
						Value = ((Request.AuthenticationArgs != null && Request.AuthenticationArgs != null) ? (this.GetInSecureOrSecureValueFromArgs(Request.AuthenticationArgs, q) ?? "") : "")
					});
				}
			}
			this.AddOrUpdateParameterValue(ref reportParameters, "username", val);
			this.AddOrUpdateParameterValue(ref reportParameters, "password", Request.Password ?? "");
			return reportParameters;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00004B28 File Offset: 0x00002D28
		private RunReportResult RunReport(int rid, IList<ReportParameter> reportParameters, string binPath)
		{
			IReportManager reportManager = new ReportManager(this.OpContext);
			return reportManager.ExecuteReport2(rid, reportParameters.ToArray<ReportParameter>());
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00004B54 File Offset: 0x00002D54
		private AuthenticationResultParameters TryToAuthenticateClockWorkReport(AuthenticationRequestParameters Request)
		{
			string text = (Request.UserName ?? "").ToUpper().Trim();
			IDictionary<string, string> args = Request.ContextItem.Args;
			ExternalUserInfo externalUserInfo = new ExternalUserInfo
			{
				UserName = text
			};
			string text2 = string.Empty;
			try
			{
				string s = args["ReportId"];
				int rid;
				int.TryParse(s, out rid);
				string text3 = args.ContainsKey("binPath") ? args["binPath"] : null;
				bool flag = string.IsNullOrEmpty(text3);
				if (flag)
				{
					text3 = this.GetAssemblyLocation(Assembly.GetExecutingAssembly());
				}
				string text4 = this.GetValueFromArgs(Request.ContextItem.Args, "student_no_field") ?? "";
				string text5 = this.GetValueFromArgs(Request.ContextItem.Args, "username_field") ?? "";
				string text6 = this.GetValueFromArgs(Request.ContextItem.Args, "email_field") ?? "";
				bool flag2 = text5.Length < 1;
				if (flag2)
				{
					text5 = "username";
				}
				bool flag3 = text4.Length < 1;
				if (flag3)
				{
					text4 = "student_no";
				}
				List<ReportParameter> reportParameters = this.GetReportParameters(Request);
				RunReportResult repRes = this.RunReport(rid, reportParameters, text3);
				RunReportResult repRes5 = repRes;
				bool flag4;
				if (((repRes5 != null) ? repRes5.ReportStatus : null) != null && repRes.ReportStatus.LastStatusStep == eRunStatusStep.CompletedSuccessfully)
				{
					RunFunctionData primaryData = repRes.PrimaryData;
					if (((primaryData != null) ? primaryData.Table : null) != null && repRes.PrimaryData.Table.Columns.Contains("authenticated"))
					{
						flag4 = (repRes.PrimaryData.Table.Rows.Count > 0);
						goto IL_1C4;
					}
				}
				flag4 = false;
				IL_1C4:
				bool flag5 = flag4;
				if (flag5)
				{
					DataRow row = repRes.PrimaryData.Table.Rows[0];
					bool flag6 = !(row["authenticated"] is DBNull) && Convert.ToBoolean(row["authenticated"]);
					bool flag7 = flag6;
					if (flag7)
					{
						Dictionary<string, string> args2 = (from DataColumn dc in repRes.PrimaryData.Table.Columns
						select dc.ColumnName).ToDictionary((string g) => g, (string g) => row[g].ToString());
						string text7 = (text4.Length > 0) ? (this.GetValueFromArgs(args2, text4) ?? "").Trim() : "";
						string text8 = (text5.Length > 0) ? (this.GetValueFromArgs(args2, text5) ?? "").Trim() : "";
						string text9 = (text6.Length > 0) ? (this.GetValueFromArgs(args2, text6) ?? "").Trim() : "";
						bool flag8 = text7.Length > 0;
						if (flag8)
						{
							externalUserInfo.StudentNumber = text7;
						}
						bool flag9 = text8.Length > 0;
						if (flag9)
						{
							externalUserInfo.UserName = text8;
						}
						bool flag10 = text9.Length > 0;
						if (flag10)
						{
							externalUserInfo.Email = text9;
						}
						return new AuthenticationResultParameters
						{
							ExternalUserInfo = externalUserInfo,
							IsSuccess = true,
							Args = args2
						};
					}
				}
				else
				{
					string[] array = new string[6];
					array[0] = "ClockWorkAuthenticationManager:TryToAuthenticateClockWorkReport:ReportStatus=";
					int num = 1;
					RunReportResult repRes2 = repRes;
					string text10;
					if (repRes2 == null)
					{
						text10 = null;
					}
					else
					{
						RunStatus reportStatus = repRes2.ReportStatus;
						text10 = ((reportStatus != null) ? reportStatus.LastStatusStep.ToString() : null);
					}
					array[num] = (text10 ?? "NULL");
					array[2] = ":cols=";
					int num2 = 3;
					RunReportResult repRes3 = repRes;
					bool flag11;
					if (repRes3 == null)
					{
						flag11 = (null != null);
					}
					else
					{
						RunFunctionData primaryData2 = repRes3.PrimaryData;
						flag11 = (((primaryData2 != null) ? primaryData2.Table : null) != null);
					}
					string text11;
					if (flag11)
					{
						text11 = string.Join(",", (from DataColumn dc in repRes.PrimaryData.Table.Columns
						select dc.ColumnName).ToArray<string>());
					}
					else
					{
						text11 = "NULL";
					}
					array[num2] = text11;
					array[4] = ":rows=";
					int num3 = 5;
					RunReportResult repRes4 = repRes;
					bool flag12;
					if (repRes4 == null)
					{
						flag12 = (null != null);
					}
					else
					{
						RunFunctionData primaryData3 = repRes4.PrimaryData;
						flag12 = (((primaryData3 != null) ? primaryData3.Table : null) != null);
					}
					array[num3] = ((!flag12 || repRes.PrimaryData.Table.Rows.Count < 1) ? "NULL" : string.Join(",", (from DataColumn dc in repRes.PrimaryData.Table.Columns
					select repRes.PrimaryData.Table.Rows[0][dc.ColumnName].ToString()).ToArray<string>()));
					text2 = string.Concat(array);
				}
			}
			catch (Exception ex)
			{
				text2 = "Common.Core.Authentication.ClockWorkAuthenticationManager.TryToAuthenticateClockWorkReport:Step Error:username=" + text + ":ex=" + ex.ToString();
				CWLogger.Logger.Error(text2);
			}
			return new AuthenticationResultParameters
			{
				ExternalUserInfo = externalUserInfo,
				IsSuccess = false,
				LoggingMessage = text2
			};
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000050CC File Offset: 0x000032CC
		private AuthenticationResultParameters TryToAuthenticateClockWorkBuiltInAuthentication(AuthenticationRequestParameters Request)
		{
			string text = (Request.UserName ?? "").ToUpper().Trim();
			ExternalUserInfo externalUserInfo = new ExternalUserInfo
			{
				UserName = text
			};
			string text2 = string.Empty;
			IDictionary<string, string> args = Request.ContextItem.Args;
			try
			{
				IUserManager userManager = ObjectFactory.Resolve<IUserManager>();
				userManager.OpContext = this.OpContext;
				bool flag = userManager.ValidateUserPassword(text, Request.Password);
				if (flag)
				{
					bool flag2 = args == null || !args.ContainsKey("all");
					if (!flag2)
					{
						return new AuthenticationResultParameters
						{
							ExternalUserInfo = externalUserInfo,
							IsSuccess = true,
							Args = new Dictionary<string, string>(),
							LoggingMessage = text2
						};
					}
					User user = userManager.GetUser(text);
					IPeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
					IList<int> groupIdsByPersonId = peopleGroupManager.GetGroupIdsByPersonId((user != null) ? user.UserId : 0);
					bool flag3 = groupIdsByPersonId.Contains(2);
					if (flag3)
					{
						return new AuthenticationResultParameters
						{
							ExternalUserInfo = externalUserInfo,
							IsSuccess = true,
							Args = new Dictionary<string, string>()
						};
					}
				}
			}
			catch (Exception ex)
			{
				text2 = "Common.Core.Authentication.TryToAuthenticateClockWorkBuiltInAuthentication.TryToAuthenticateClockWork:Step Error:username=" + text + ":ex=" + ex.ToString();
				CWLogger.Logger.Error(text2);
			}
			return new AuthenticationResultParameters
			{
				ExternalUserInfo = externalUserInfo,
				IsSuccess = false,
				LoggingMessage = text2
			};
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00005250 File Offset: 0x00003450
		private AuthenticationResultParameters TryToAuthenticateClockWork(AuthenticationRequestParameters Request)
		{
			IDictionary<string, string> args = Request.ContextItem.Args;
			return (args != null && args.ContainsKey("ReportId")) ? this.TryToAuthenticateClockWorkReport(Request) : this.TryToAuthenticateClockWorkBuiltInAuthentication(Request);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00005290 File Offset: 0x00003490
		private AuthenticationResultParameters TryToAuthenticateActiveDirectory(AuthenticationRequestParameters Request)
		{
			IDictionary<string, string> args = Request.ContextItem.Args;
			bool flag = !args.ContainsKey("activedirectory");
			if (flag)
			{
				args.Add("activedirectory", "1");
			}
			string text = (args.GetArgSafe("ldaplookupattribute") ?? "").Trim();
			bool flag2 = text.Length > 0;
			bool flag3 = flag2 && !args.ContainsKey("activedirectoryuselookupattribute");
			if (flag3)
			{
				args.Add("activedirectoryuselookupattribute", "1");
			}
			return this.TryToAuthenticateLdap(Request);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000532C File Offset: 0x0000352C
		private AuthenticationResultParameters TryToAuthenticateLdap(AuthenticationRequestParameters Request)
		{
			string loggingMessage = string.Empty;
			try
			{
				LdapConnectionInfo connectionInfoFromArgs = Request.ContextItem.Args.GetConnectionInfoFromArgs();
				ILdapManager ldapManager = new LdapManager(this.OpContext);
				LdapAuthenticationResult ldapAuthenticationResult = ldapManager.LdapLogin(connectionInfoFromArgs, Request.UserName, Request.Password);
				bool isAuthenticated = ldapAuthenticationResult.IsAuthenticated;
				if (isAuthenticated)
				{
					bool flag = Request.ContextItem.Args == null;
					if (flag)
					{
						Request.ContextItem.Args = new Dictionary<string, string>();
					}
					string snumField = Request.ContextItem.Args.ContainsKey("student_no_field") ? (Request.ContextItem.Args["student_no_field"] ?? "") : "";
					string text = Request.ContextItem.Args.ContainsKey("email_field") ? (Request.ContextItem.Args["email_field"] ?? "") : "";
					Dictionary<string, string> dictionary = ldapAuthenticationResult.ReturnAttributes ?? new Dictionary<string, string>();
					string text2 = (snumField.Length > 0 && dictionary.ContainsKey(snumField)) ? dictionary[snumField] : "";
					string email = (text.Length > 0 && dictionary.ContainsKey(text)) ? dictionary[text] : "";
					bool flag2 = snumField.Length > 0 && text2.Length < 1;
					if (flag2)
					{
						string text3 = dictionary.Keys.FirstOrDefault((string g) => g != null && g.Equals(snumField, StringComparison.OrdinalIgnoreCase));
						bool flag3 = !string.IsNullOrWhiteSpace(text3);
						if (flag3)
						{
							text2 = (dictionary.ContainsKey(text3) ? dictionary[text3] : string.Empty);
						}
						bool flag4 = snumField.Length > 0 && text2.Length < 1;
						if (flag4)
						{
							CWLogger logger = CWLogger.Logger;
							string message = "Common.Core.Authentication.ClockWorkAuthenticationManager.TryToAuthenticationLdap:Can't find snum:SnumField={0}:returnAttributes={1}";
							object snumField2 = snumField;
							object arg;
							if (dictionary != null)
							{
								arg = string.Join(", ", (from g in dictionary
								select g.Key + "=" + (g.Value ?? "NULL")).ToArray<string>());
							}
							else
							{
								arg = "NULL";
							}
							logger.Debug(message, snumField2, arg);
						}
					}
					return new AuthenticationResultParameters
					{
						ExternalUserInfo = new ExternalUserInfo
						{
							UserName = Request.UserName,
							StudentNumber = text2,
							Email = email
						},
						IsSuccess = true,
						Args = ldapAuthenticationResult.ReturnAttributes
					};
				}
				loggingMessage = (ldapAuthenticationResult.ErrorMessage ?? "");
			}
			catch (Exception ex)
			{
				loggingMessage = "Common.Core.Authentication.ClockWorkAuthenticationManager.TryToAuthenticationLdap:Step Error:ex=" + ex.ToString();
			}
			return new AuthenticationResultParameters
			{
				IsSuccess = false,
				LoggingMessage = loggingMessage
			};
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00005634 File Offset: 0x00003834
		private AuthenticationResultParameters TryToAuthenticateShibboleth(AuthenticationRequestParameters Request)
		{
			bool flag = Request.ContextItem.Args == null;
			if (flag)
			{
				Request.ContextItem.Args = new Dictionary<string, string>();
			}
			bool flag2 = Request.AuthenticationArgs == null;
			if (flag2)
			{
				Request.AuthenticationArgs = new AuthenticationArgs();
			}
			string text = Request.ContextItem.Args.ContainsKey("student_no_field") ? (Request.ContextItem.Args["student_no_field"] ?? "") : "";
			string text2 = Request.ContextItem.Args.ContainsKey("username_field") ? (Request.ContextItem.Args["username_field"] ?? "") : "";
			string text3 = Request.ContextItem.Args.ContainsKey("email_field") ? (Request.ContextItem.Args["email_field"] ?? "") : "";
			string loggingMessage = string.Empty;
			Exception ex = null;
			IDictionary<string, string> dictionary;
			if (Request.AuthenticationArgs != null && Request.AuthenticationArgs.SecureArgs != null)
			{
				dictionary = Request.AuthenticationArgs.SecureArgs;
			}
			else
			{
				IDictionary<string, string> dictionary2 = new Dictionary<string, string>();
				dictionary = dictionary2;
			}
			IDictionary<string, string> dictionary3 = dictionary;
			try
			{
				string text4 = (text.Length > 0 && dictionary3.ContainsKey(text)) ? (dictionary3[text] ?? "").Trim() : "";
				string text5 = (text2.Length > 0 && dictionary3.ContainsKey(text2)) ? (dictionary3[text2] ?? "").Trim() : "";
				string text6 = (text3.Length > 0 && dictionary3.ContainsKey(text3)) ? (dictionary3[text3] ?? "").Trim() : "";
				bool flag3 = text.Length > 0 && text4.Length < 1;
				if (flag3)
				{
					CWLogger.Logger.Warn("Common.Core.AuthenticationAuthorization.ClockWorkAuthenticationManager.TryToAuthenticateShibboleth:Can't find snum:snumField={0}:Request.AuthenticationArgs={1}", text, string.Join(", ", (from g in dictionary3
					select g.Key + "=" + (g.Value ?? "NULL")).ToArray<string>()));
				}
				bool flag4 = text4.Length > 0 || text5.Length > 0;
				if (flag4)
				{
					return new AuthenticationResultParameters
					{
						ExternalUserInfo = new ExternalUserInfo
						{
							StudentNumber = text4,
							UserName = ((text5.Length > 0) ? text5 : text4),
							Email = text6
						},
						IsSuccess = true,
						Args = new Dictionary<string, string>
						{
							{
								"email",
								text6
							}
						}
					};
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			string[] array = new string[6];
			array[0] = "Common.Core.Authentication.ClockWorkAuthenticationManager.TryToAuthenticationShibboleth:Step Error:authenticationArgs=";
			array[1] = string.Join(", ", (from g in dictionary3
			select g.Key + "=" + (g.Value ?? "NULL")).ToArray<string>());
			array[2] = ":contextArgs=";
			array[3] = string.Join(", ", (from g in Request.ContextItem.Args
			select g.Key + "=" + (g.Value ?? "NULL")).ToArray<string>());
			array[4] = ":ex=";
			array[5] = (((ex != null) ? ex.ToString() : null) ?? "NULL");
			loggingMessage = string.Concat(array);
			return new AuthenticationResultParameters
			{
				IsSuccess = false,
				LoggingMessage = loggingMessage
			};
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000059E8 File Offset: 0x00003BE8
		private static string Base64Decode(string base64EncodedData)
		{
			byte[] bytes = Convert.FromBase64String(base64EncodedData);
			return Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00005A0C File Offset: 0x00003C0C
		private static T ParseEnum<T>(string s, T defaultEnum) where T : struct
		{
			bool flag = string.IsNullOrEmpty(s);
			T result;
			if (flag)
			{
				result = defaultEnum;
			}
			else
			{
				T t;
				result = ((!Enum.TryParse<T>(s, out t)) ? defaultEnum : t);
			}
			return result;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00005A3C File Offset: 0x00003C3C
		private AuthenticationResultParameters TryToAuthenticatePortalGuard(AuthenticationRequestParameters Request)
		{
			string loggingMessage = string.Empty;
			try
			{
				string inSecureOrSecureValueFromArgs = this.GetInSecureOrSecureValueFromArgs(Request.AuthenticationArgs, "samlresponse");
				string text = ClockWorkAuthenticationManager.Base64Decode(inSecureOrSecureValueFromArgs);
				bool verboseLoggingEnabled = Request.VerboseLoggingEnabled;
				if (verboseLoggingEnabled)
				{
					CWLogger.Logger.Trace("TryToAuthenticatePortalGuard:samlResponseXml={0}", text ?? "NULL");
				}
				string xml = Request.ContextItem.Args.ContainsKey("token_issuer") ? (Request.ContextItem.Args["token_issuer"] ?? "") : "";
				TokenIssuerAuthParameter tokenIssuerFromXml = xml.GetTokenIssuerFromXml();
				SecurityTokenElement securityTokenElement2;
				if (tokenIssuerFromXml != null)
				{
					SecurityTokenElement securityTokenElement = new SecurityTokenElement();
					securityTokenElement.Name = (tokenIssuerFromXml.Name ?? "");
					securityTokenElement.UriToken = new Uri(tokenIssuerFromXml.UriToken ?? "");
					securityTokenElement.StoreLocation = ClockWorkAuthenticationManager.ParseEnum<StoreLocation>(tokenIssuerFromXml.StoreLocation ?? "", StoreLocation.LocalMachine);
					securityTokenElement.StoreName = ClockWorkAuthenticationManager.ParseEnum<StoreName>(tokenIssuerFromXml.StoreName ?? "", StoreName.TrustedPeople);
					securityTokenElement.FindType = ClockWorkAuthenticationManager.ParseEnum<X509FindType>(tokenIssuerFromXml.FindType ?? "", X509FindType.FindByThumbprint);
					securityTokenElement2 = securityTokenElement;
					securityTokenElement.FindValue = (tokenIssuerFromXml.FindValue ?? "");
				}
				else
				{
					securityTokenElement2 = new SecurityTokenElement();
				}
				SecurityTokenElement tokenIssuer = securityTokenElement2;
				Saml2Response saml2Response = new Saml2Response();
				saml2Response.ReadXml(text, tokenIssuer);
				SamlResponseStatusCode? statusCode = saml2Response.StatusCode;
				bool flag;
				if (statusCode != null)
				{
					statusCode = saml2Response.StatusCode;
					flag = (statusCode.Value > SamlResponseStatusCode.Success);
				}
				else
				{
					flag = true;
				}
				bool flag2 = flag;
				if (flag2)
				{
					string str = "SamlResponseParse failed - Status code=";
					statusCode = saml2Response.StatusCode;
					throw new Exception(str + ((statusCode != null) ? statusCode.GetValueOrDefault().ToString() : null));
				}
				IDictionary<string, string> claims = saml2Response.GetClaims();
				bool flag3 = Request.ContextItem.Args == null;
				if (flag3)
				{
					Request.ContextItem.Args = new Dictionary<string, string>();
				}
				string text2 = Request.ContextItem.Args.ContainsKey("student_no_field") ? (Request.ContextItem.Args["student_no_field"] ?? "") : "";
				string text3 = Request.ContextItem.Args.ContainsKey("username_field") ? (Request.ContextItem.Args["username_field"] ?? "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname") : "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname";
				string text4 = Request.ContextItem.Args.ContainsKey("email_field") ? (Request.ContextItem.Args["email_field"] ?? "") : "";
				string text5 = (text2.Length > 0 && claims.ContainsKey(text2)) ? (claims[text2] ?? "").Trim() : "";
				string text6 = (text3.Length > 0 && claims.ContainsKey(text3)) ? (claims[text3] ?? "").Trim() : "";
				string text7 = (text4.Length > 0 && claims.ContainsKey(text4)) ? (claims[text4] ?? "").Trim() : "";
				bool flag4 = text2.Length > 0 && text5.Length < 1;
				if (flag4)
				{
					CWLogger.Logger.Warn("Common.Core.AuthenticationAuthorization.ClockWorkAuthenticationManager.TryToAuthenticatePortalGuard:Can't find snum:snumField={0}:Request.AuthenticationArgs={1}", text2, string.Join(", ", (from g in claims
					select g.Key + "=" + (g.Value ?? "NULL")).ToArray<string>()));
				}
				bool flag5 = text5.Length > 0 || text6.Length > 0;
				if (flag5)
				{
					return new AuthenticationResultParameters
					{
						ExternalUserInfo = new ExternalUserInfo
						{
							StudentNumber = text5,
							UserName = ((text6.Length > 0) ? text6 : text5),
							Email = text7
						},
						IsSuccess = true,
						Args = new Dictionary<string, string>
						{
							{
								"email",
								text7
							}
						}
					};
				}
			}
			catch (Exception ex)
			{
				loggingMessage = "Common.Core.Authentication.ClockWorkAuthenticationManager.TryToAuthenticatPortalGuard:Step Error:ex=" + ex.ToString();
			}
			return new AuthenticationResultParameters
			{
				IsSuccess = false,
				LoggingMessage = loggingMessage
			};
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00005EC4 File Offset: 0x000040C4
		private AuthenticationResultParameters TryToAuthenticateAdfs(AuthenticationRequestParameters Request)
		{
			string loggingMessage = string.Empty;
			try
			{
				foreach (KeyValuePair<string, string> keyValuePair in Request.AuthenticationArgs.SecureArgs)
				{
					CWLogger.Logger.Trace("TryToAuthenticateAdfs:secureauthargs:{0}={1}", keyValuePair.Key, keyValuePair.Value ?? "NULL");
				}
				foreach (KeyValuePair<string, string> keyValuePair2 in Request.AuthenticationArgs.InsecureArgs)
				{
					CWLogger.Logger.Trace("TryToAuthenticateAdfs:insecureauthargs:{0}={1}", keyValuePair2.Key, keyValuePair2.Value ?? "NULL");
				}
				string text = this.GetInSecureOrSecureValueFromArgs(Request.AuthenticationArgs, "samlresponse");
				string inSecureOrSecureValueFromArgs = this.GetInSecureOrSecureValueFromArgs(Request.AuthenticationArgs, "samlart");
				CWLogger.Logger.Trace("TryToAuthenticateAdfs:token={0}:samlArt={1}", text ?? "NULL", inSecureOrSecureValueFromArgs ?? "NULL");
				string xml = Request.ContextItem.Args.ContainsKey("token_issuer") ? (Request.ContextItem.Args["token_issuer"] ?? "") : "";
				TokenIssuerAuthParameter tokenIssuerFromXml = xml.GetTokenIssuerFromXml();
				SecurityTokenElement securityTokenElement2;
				if (tokenIssuerFromXml != null)
				{
					SecurityTokenElement securityTokenElement = new SecurityTokenElement();
					securityTokenElement.Name = (tokenIssuerFromXml.Name ?? "");
					securityTokenElement.UriToken = new Uri(tokenIssuerFromXml.UriToken ?? "");
					securityTokenElement.StoreLocation = ClockWorkAuthenticationManager.ParseEnum<StoreLocation>(tokenIssuerFromXml.StoreLocation ?? "", StoreLocation.LocalMachine);
					securityTokenElement.StoreName = ClockWorkAuthenticationManager.ParseEnum<StoreName>(tokenIssuerFromXml.StoreName ?? "", StoreName.TrustedPeople);
					securityTokenElement.FindType = ClockWorkAuthenticationManager.ParseEnum<X509FindType>(tokenIssuerFromXml.FindType ?? "", X509FindType.FindByThumbprint);
					securityTokenElement2 = securityTokenElement;
					securityTokenElement.FindValue = (tokenIssuerFromXml.FindValue ?? "");
				}
				else
				{
					securityTokenElement2 = new SecurityTokenElement();
				}
				SecurityTokenElement securityTokenElement3 = securityTokenElement2;
				CWLogger.Logger.Trace("TryToAuthenticateAdfs:issuername={0}:storeLocation={1}:storename={2}:uritoken={3}:CertificateThumbprint={4}", new object[]
				{
					securityTokenElement3.Name ?? "NULL",
					securityTokenElement3.StoreLocation.ToString() ?? "NULL",
					securityTokenElement3.StoreName.ToString() ?? "NULL",
					((tokenIssuerFromXml != null) ? tokenIssuerFromXml.UriToken : null) ?? "",
					securityTokenElement3.FindValue ?? "NULL"
				});
				IADFSAuthManager iadfsauthManager = new ADFS2AuthManager();
				string text2 = (((tokenIssuerFromXml != null) ? tokenIssuerFromXml.UriToken : null) ?? "").Trim();
				bool flag = text2.Length > 1 && text2[text2.Length - 1] == '/';
				if (flag)
				{
					text2 = text2.Substring(0, text2.Length - 1);
				}
				iadfsauthManager.Parameters = new AdfsParameters
				{
					IssuerName = securityTokenElement3.Name,
					StoreLocation = securityTokenElement3.StoreLocation,
					StoreName = securityTokenElement3.StoreName,
					UriToken = text2,
					CertificateThumbprint = securityTokenElement3.FindValue
				};
				bool flag2 = string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(inSecureOrSecureValueFromArgs);
				if (flag2)
				{
					CWLogger.Logger.Debug("ClockWorkAuthenticationManager:TryToAuthenticateAdfs:Detected Saml Artifact:samlArt={0}", inSecureOrSecureValueFromArgs);
					string text3 = Request.ContextItem.Args.ContainsKey("request_signing_key") ? (Request.ContextItem.Args["request_signing_key"] ?? "") : "";
					string text4 = text2.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ? ("https://" + text2.Substring(7) + "/artifactresolution") : (text2 + "/artifactresolution");
					CWLogger.Logger.Debug("ArtifactResolveServiceUri=" + text4);
					CertificateLocation certificateLocation = text3.CertificateLocationFromXml();
					bool flag3 = certificateLocation == null;
					if (flag3)
					{
						throw new Exception("Saml artifact is present but no signing key is available.  request_signing_key='" + (text3 ?? "NULL") + "'");
					}
					text = iadfsauthManager.GetSamlResponseFromSamlArtifact(inSecureOrSecureValueFromArgs, "", certificateLocation, text4);
					bool flag4 = string.IsNullOrEmpty(text);
					if (flag4)
					{
						CWLogger.Logger.Warn("Saml artifact is present but unable to get request.  Artifact={0}", inSecureOrSecureValueFromArgs);
					}
				}
				ClaimsPrincipal claimsPrincipal;
				bool flag5 = iadfsauthManager.ValidateToken(text, out claimsPrincipal);
				bool flag6 = !flag5;
				if (flag6)
				{
					throw new Exception("SamlResponseParse failed.");
				}
				CWLogger.Logger.Trace("TryToAuthenticateAdfs:authenticationSucceeded={0}", flag5);
				Dictionary<string, string> dictionary = claimsPrincipal.Claims.ToDictionary((Claim g) => g.Type, (Claim g) => g.Value);
				CWLogger.Logger.Trace("TryToAuthenticateAdfs:claims={0}", dictionary.Count.ToString());
				foreach (KeyValuePair<string, string> keyValuePair3 in dictionary)
				{
					CWLogger.Logger.Trace("TryToAuthenticateAdfs:claim:{0}={1}", keyValuePair3.Key.ToString(), (keyValuePair3.Value == null) ? "NULL" : keyValuePair3.Value.ToString());
				}
				bool flag7 = Request.ContextItem.Args == null;
				if (flag7)
				{
					Request.ContextItem.Args = new Dictionary<string, string>();
				}
				string text5 = Request.ContextItem.Args.ContainsKey("student_no_field") ? (Request.ContextItem.Args["student_no_field"] ?? "") : "";
				string text6 = Request.ContextItem.Args.ContainsKey("username_field") ? (Request.ContextItem.Args["username_field"] ?? "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname") : "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname";
				string text7 = Request.ContextItem.Args.ContainsKey("email_field") ? (Request.ContextItem.Args["email_field"] ?? "") : "";
				string text8 = (text5.Length > 0 && dictionary.ContainsKey(text5)) ? (dictionary[text5] ?? "").Trim() : "";
				string text9 = (text6.Length > 0 && dictionary.ContainsKey(text6)) ? (dictionary[text6] ?? "").Trim() : "";
				string text10 = (text7.Length > 0 && dictionary.ContainsKey(text7)) ? (dictionary[text7] ?? "").Trim() : "";
				bool flag8 = text5.Length > 0 && text8.Length < 1;
				if (flag8)
				{
					CWLogger.Logger.Warn("Common.Core.AuthenticationAuthorization.ClockWorkAuthenticationManager.TryToAuthenticateAdfs:Can't find snum:snumField={0}:Request.AuthenticationArgs={1}", text5, string.Join(", ", (from g in dictionary
					select g.Key + "=" + (g.Value ?? "NULL")).ToArray<string>()));
				}
				bool flag9 = text8.Length > 0 || text9.Length > 0;
				if (flag9)
				{
					return new AuthenticationResultParameters
					{
						ExternalUserInfo = new ExternalUserInfo
						{
							StudentNumber = text8,
							UserName = ((text9.Length > 0) ? text9 : text8),
							Email = text10
						},
						IsSuccess = true,
						Args = new Dictionary<string, string>
						{
							{
								"email",
								text10
							}
						}
					};
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("TryToAuthenticateAdfs:errorex={0}", ex.ToString());
				loggingMessage = "Common.Core.Authentication.ClockWorkAuthenticationManager.TryToAuthenticatAdfs:Step Error:ex=" + ex.ToString();
			}
			return new AuthenticationResultParameters
			{
				IsSuccess = false,
				LoggingMessage = loggingMessage
			};
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000675C File Offset: 0x0000495C
		private AuthenticationResultParameters TryToAuthenticationCAS(AuthenticationRequestParameters Request)
		{
			string loggingMessage = string.Empty;
			try
			{
				string ticket = this.GetInSecureOrSecureValueFromArgs(Request.AuthenticationArgs, "ticket") ?? "";
				ICASAuthManager icasauthManager = new CASAuthManager(this.OpContext);
				CASAuthenticationResult casauthenticationResult = icasauthManager.AuthenticateCAS(ticket);
				bool isAuthenticated = casauthenticationResult.IsAuthenticated;
				if (isAuthenticated)
				{
					return new AuthenticationResultParameters
					{
						ExternalUserInfo = new ExternalUserInfo
						{
							UserName = casauthenticationResult.UserName
						},
						IsSuccess = true,
						Args = casauthenticationResult.ReturnAttributes
					};
				}
				loggingMessage = "Failed CAS ticket check";
			}
			catch (Exception ex)
			{
				loggingMessage = "Common.Core.Authentication.ClockWorkAuthenticationManager.TryToAuthenticationCAS:Step Error:ex=" + ex.ToString();
			}
			return new AuthenticationResultParameters
			{
				IsSuccess = false,
				LoggingMessage = loggingMessage
			};
		}

		// Token: 0x02000003 RID: 3
		internal class AuthenticationToDoItem
		{
			// Token: 0x06000032 RID: 50 RVA: 0x00006830 File Offset: 0x00004A30
			public AuthenticationToDoItem()
			{
			}

			// Token: 0x06000033 RID: 51 RVA: 0x0000683A File Offset: 0x00004A3A
			public AuthenticationToDoItem(Func<AuthenticationRequestParameters, AuthenticationResultParameters> func, AuthenticationContextItem contextItem)
			{
				this.Func = func;
				this.ContextItem = contextItem;
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000034 RID: 52 RVA: 0x00006854 File Offset: 0x00004A54
			// (set) Token: 0x06000035 RID: 53 RVA: 0x0000685C File Offset: 0x00004A5C
			public AuthenticationContextItem ContextItem { get; set; }

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000036 RID: 54 RVA: 0x00006865 File Offset: 0x00004A65
			// (set) Token: 0x06000037 RID: 55 RVA: 0x0000686D File Offset: 0x00004A6D
			public Func<AuthenticationRequestParameters, AuthenticationResultParameters> Func { get; set; }
		}

		// Token: 0x02000004 RID: 4
		internal class AuthenticationTodoListResult
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000038 RID: 56 RVA: 0x00006876 File Offset: 0x00004A76
			// (set) Token: 0x06000039 RID: 57 RVA: 0x0000687E File Offset: 0x00004A7E
			public AuthenticationResultParameters Res { get; set; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600003A RID: 58 RVA: 0x00006887 File Offset: 0x00004A87
			// (set) Token: 0x0600003B RID: 59 RVA: 0x0000688F File Offset: 0x00004A8F
			public AuthenticationRequestParameters Req { get; set; }
		}

		// Token: 0x02000005 RID: 5
		internal class PostAuthenticationReportResult
		{
			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600003D RID: 61 RVA: 0x000068A1 File Offset: 0x00004AA1
			// (set) Token: 0x0600003E RID: 62 RVA: 0x000068A9 File Offset: 0x00004AA9
			public string OverrideStudentNumber { get; set; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600003F RID: 63 RVA: 0x000068B2 File Offset: 0x00004AB2
			// (set) Token: 0x06000040 RID: 64 RVA: 0x000068BA File Offset: 0x00004ABA
			public string OverrideUsername { get; set; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000041 RID: 65 RVA: 0x000068C3 File Offset: 0x00004AC3
			// (set) Token: 0x06000042 RID: 66 RVA: 0x000068CB File Offset: 0x00004ACB
			public string OverrideEmail { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000043 RID: 67 RVA: 0x000068D4 File Offset: 0x00004AD4
			// (set) Token: 0x06000044 RID: 68 RVA: 0x000068DC File Offset: 0x00004ADC
			public bool? OverrideIsAuthenticated { get; set; }
		}
	}
}
