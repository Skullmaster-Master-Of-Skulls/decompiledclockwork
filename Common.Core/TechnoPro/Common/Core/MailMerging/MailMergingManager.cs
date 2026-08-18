using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using Newtonsoft.Json;
using TechnoPro.Common.Barcode;
using TechnoPro.Common.Core.AlternativeFormat;
using TechnoPro.Common.Core.AppointmentsCalendar;
using TechnoPro.Common.Core.AppointmentsTestBooking;
using TechnoPro.Common.Core.CourseRegistrations;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.Inventory;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.MailMerging.Output;
using TechnoPro.Common.Core.OnlineForms;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.Reports;
using TechnoPro.Common.Core.ServiceProvidersOriginal;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.MailMerging;
using TechnoPro.Common.DAO.MailMerging;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.ICore.AlternativeFormat;
using TechnoPro.Common.ICore.AppointmentsCalendar;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.ICore.CourseRegistrations;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.ICore.MailMerging;
using TechnoPro.Common.ICore.MailMerging.Output;
using TechnoPro.Common.ICore.OnlineForms;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem;
using TechnoPro.Common.Public.Entities.Inventory;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeCodes;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Public.Exceptions.PermissionDenied;
using TechnoPro.Common.TextFormat.Adapters;

namespace TechnoPro.Common.Core.MailMerging
{
	// Token: 0x020000CA RID: 202
	public class MailMergingManager : IMailMergingManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0002AF50 File Offset: 0x00029150
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x0002AF58 File Offset: 0x00029158
		public IMailMergingDAO dao { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0002AF64 File Offset: 0x00029164
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

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0002AF90 File Offset: 0x00029190
		private DynamicFieldManager dynamicFieldManager
		{
			get
			{
				DynamicFieldManager result;
				if ((result = this._dynamicFieldManager) == null)
				{
					result = (this._dynamicFieldManager = new DynamicFieldManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0002AFBC File Offset: 0x000291BC
		private IAppointmentManager appointmentManager
		{
			get
			{
				IAppointmentManager result;
				if ((result = this.apm) == null)
				{
					result = (this.apm = new AppointmentManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0002AFE8 File Offset: 0x000291E8
		private ClassTestDefinitionManager classTestManager
		{
			get
			{
				ClassTestDefinitionManager result;
				if ((result = this._classTestManager) == null)
				{
					result = (this._classTestManager = new ClassTestDefinitionManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0002B014 File Offset: 0x00029214
		private TestBookingManager testBookingManager
		{
			get
			{
				TestBookingManager result;
				if ((result = this.tbm) == null)
				{
					result = (this.tbm = new TestBookingManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0002B040 File Offset: 0x00029240
		private IReportManager reportManager
		{
			get
			{
				IReportManager result;
				if ((result = this.rm) == null)
				{
					result = (this.rm = new ReportManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0002B06C File Offset: 0x0002926C
		private ILookupCourseManager lookupCourseManager
		{
			get
			{
				ILookupCourseManager result;
				if ((result = this.lcm) == null)
				{
					result = (this.lcm = new LookupCourseManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x0002B098 File Offset: 0x00029298
		private IStudentCommonInfoManager studentCommonInfoManager
		{
			get
			{
				IStudentCommonInfoManager result;
				if ((result = this.scm) == null)
				{
					result = (this.scm = new StudentCommonInfoManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0002B0C4 File Offset: 0x000292C4
		private ISessionManager sessionManager
		{
			get
			{
				ISessionManager result;
				if ((result = this.snm) == null)
				{
					result = (this.snm = new SessionManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0002B0F0 File Offset: 0x000292F0
		private IOldUserSettingManager oldUserSettingsManager
		{
			get
			{
				IOldUserSettingManager result;
				if ((result = this.ousm) == null)
				{
					result = (this.ousm = new OldUserSettingManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0002B11C File Offset: 0x0002931C
		private PeopleManager peopleManager
		{
			get
			{
				PeopleManager result;
				if ((result = this.pm) == null)
				{
					result = (this.pm = new PeopleManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0002B148 File Offset: 0x00029348
		private OldUserSettingManager oldUserSettingManager
		{
			get
			{
				OldUserSettingManager result;
				if ((result = this.om) == null)
				{
					result = (this.om = new OldUserSettingManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0002B174 File Offset: 0x00029374
		private IStaffCommonInfoManager staffCommonInfoManager
		{
			get
			{
				IStaffCommonInfoManager result;
				if ((result = this.scim) == null)
				{
					result = (this.scim = new StaffCommonInfoManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0002B19F File Offset: 0x0002939F
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x0002B1A7 File Offset: 0x000293A7
		public OperationContext OpContext { get; set; }

		// Token: 0x0600075D RID: 1885 RVA: 0x0002B1B0 File Offset: 0x000293B0
		public MailMergingManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new MailMergingDAO(opContext);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0002B1D8 File Offset: 0x000293D8
		private void SetMailMergeValueForWebSettingValueAsCheckboxValue(ref MailMergingManager.MailMergeCodeWrapper code, object codeValue)
		{
			bool flag = codeValue == null;
			bool isChecked;
			if (flag)
			{
				isChecked = false;
			}
			else
			{
				bool flag2 = codeValue is int;
				if (flag2)
				{
					isChecked = ((int)codeValue != 0);
				}
				else
				{
					bool flag3 = codeValue is double;
					if (flag3)
					{
						isChecked = ((double)codeValue != 0.0);
					}
					else
					{
						bool flag4 = codeValue is bool;
						if (flag4)
						{
							isChecked = (bool)codeValue;
						}
						else
						{
							string value = codeValue.ToString().Trim().ToLower();
							isChecked = ("1yestrue".IndexOf(value) >= 0);
						}
					}
				}
			}
			code.Item.SetMailMergeValue(new MailMergeCheckedItem
			{
				Title = code.Name,
				IsChecked = isChecked,
				HideCheckboxTitle = code.Item.Args.ContainsKey("hidetitle")
			});
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0002B2B4 File Offset: 0x000294B4
		private void SetMailMergeValueForWebSettingValue(ref MailMergingManager.MailMergeCodeWrapper code, object codeValue)
		{
			bool flag = code.Item.Args.ContainsKey("checkbox");
			bool flag2 = flag;
			if (flag2)
			{
				this.SetMailMergeValueForWebSettingValueAsCheckboxValue(ref code, codeValue);
			}
			else
			{
				bool flag3 = codeValue is string;
				if (flag3)
				{
					code.Item.SetMailMergeValue((string)codeValue);
				}
				else
				{
					bool flag4 = codeValue is int;
					if (flag4)
					{
						code.Item.SetMailMergeValue((int)codeValue);
					}
					else
					{
						bool flag5 = codeValue is DateTime;
						if (flag5)
						{
							code.Item.SetMailMergeValue((DateTime)codeValue);
						}
						else
						{
							bool flag6 = codeValue is bool;
							if (flag6)
							{
								code.Item.SetMailMergeValue((bool)codeValue);
							}
							else
							{
								bool flag7 = codeValue is byte[];
								if (flag7)
								{
									code.Item.SetMailMergeValue((byte[])codeValue);
								}
								else
								{
									bool flag8 = codeValue is IList<string>;
									if (flag8)
									{
										code.Item.SetMailMergeValue((IList<string>)codeValue);
									}
									else
									{
										bool flag9 = codeValue is IList<int>;
										if (flag9)
										{
											code.Item.SetMailMergeValue((IList<int>)codeValue);
										}
										else
										{
											bool flag10 = codeValue is double;
											if (flag10)
											{
												code.Item.SetMailMergeValue((double)codeValue);
											}
											else
											{
												bool flag11 = codeValue is float;
												if (flag11)
												{
													code.Item.SetMailMergeValue(Convert.ToDouble((float)codeValue));
												}
												else
												{
													code.Item.SetMailMergeValue(codeValue.ToString());
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0002B458 File Offset: 0x00029658
		private bool MailMergeWebSettingInfo(ref MailMergingManager.MailMergeCodeWrapper code, MailMergeContextWithCustomDictionary ContextWithCustomDictionary, Dictionary<string, string> args, ref Dictionary<string, object> tempCache)
		{
			string text = args["websettingid"];
			string text2 = args.ContainsKey("websettingcontext") ? args["websettingcontext"] : "ClockWork";
			bool flag = string.IsNullOrEmpty(text2);
			if (flag)
			{
				text2 = "ClockWork";
			}
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext, text2));
			bool flag2 = text.Contains("|");
			bool result;
			if (flag2)
			{
				string[] array = text.Split(new char[]
				{
					'|'
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text3 in array)
				{
					int num;
					bool flag3 = !int.TryParse(text3.Trim(), out num) || num <= 0;
					if (!flag3)
					{
						object settingValue = webSettingManager.GetSettingValue<object>(num);
						bool flag4 = settingValue == null || (settingValue is string && string.IsNullOrEmpty((string)settingValue));
						if (!flag4)
						{
							this.SetMailMergeValueForWebSettingValue(ref code, settingValue);
							return true;
						}
					}
				}
				result = false;
			}
			else
			{
				bool flag5 = text.Contains(",");
				if (flag5)
				{
					string[] array3 = text.Split(new char[]
					{
						','
					});
					bool flag6 = array3.Length == 0;
					if (flag6)
					{
						result = false;
					}
					else
					{
						List<object> list = new List<object>();
						foreach (string text4 in array3)
						{
							int num2;
							bool flag7 = !int.TryParse(text4.Trim(), out num2) || num2 <= 0;
							if (!flag7)
							{
								object settingValue2 = webSettingManager.GetSettingValue<object>(num2);
								list.Add(settingValue2);
							}
						}
						this.SetMailMergeValueForWebSettingValue(ref code, list);
						result = true;
					}
				}
				else
				{
					int num3;
					bool flag8 = !int.TryParse(text.Trim(), out num3) || num3 <= 0;
					if (flag8)
					{
						result = false;
					}
					else
					{
						object settingValue3 = webSettingManager.GetSettingValue<object>(num3);
						this.SetMailMergeValueForWebSettingValue(ref code, settingValue3);
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0002B65C File Offset: 0x0002985C
		private bool MailMergeReportInfo(ref MailMergingManager.MailMergeCodeWrapper code, MailMergeContextWithCustomDictionary ContextWithCustomDictionary, Dictionary<string, string> args, ref Dictionary<string, object> tempCache)
		{
			MailMergeContext context = ContextWithCustomDictionary.Context;
			int reportId;
			bool flag = int.TryParse(args["rid"], out reportId);
			if (flag)
			{
				string key = "rid" + reportId.ToString();
				bool flag2 = !tempCache.ContainsKey(key);
				if (flag2)
				{
					IReportManager reportManager = this.reportManager;
					ReportParameter[] array = new ReportParameter[13];
					array[0] = new ReportParameter
					{
						Name = "pid",
						Value = context.PersonId
					};
					array[1] = new ReportParameter
					{
						Name = "altpid",
						Value = context.AltPersonId
					};
					array[2] = new ReportParameter
					{
						Name = "appid",
						Value = context.AppointmentId
					};
					array[3] = new ReportParameter
					{
						Name = "lucid",
						Value = context.LuCourseId
					};
					array[4] = new ReportParameter
					{
						Name = "examid",
						Value = context.ExamId
					};
					array[5] = new ReportParameter
					{
						Name = "serviceproviderid",
						Value = context.ServiceProviderId
					};
					array[6] = new ReportParameter
					{
						Name = "whoami",
						Value = context.WhoAmId
					};
					array[7] = new ReportParameter
					{
						Name = "instructorid",
						Value = context.InstructorId
					};
					array[8] = new ReportParameter
					{
						Name = "caseid",
						Value = context.CaseId
					};
					array[9] = new ReportParameter
					{
						Name = "courseid",
						Value = context.CourseId
					};
					array[10] = new ReportParameter
					{
						Name = "perdateid",
						Value = context.PerDateId
					};
					array[11] = new ReportParameter
					{
						Name = "originalcode",
						Value = code.Item.OriginalCode
					};
					int num = 12;
					ReportParameter reportParameter = new ReportParameter();
					reportParameter.Name = "lucids";
					object value;
					if (context.LuCourseIds != null)
					{
						value = string.Join(",", (from g in context.LuCourseIds
						select g.ToString()).ToArray<string>());
					}
					else
					{
						value = "";
					}
					reportParameter.Value = value;
					array[num] = reportParameter;
					ReportParameter[] parameters = array;
					RunReportResult value2 = reportManager.ExecuteReport2(reportId, parameters);
					tempCache.Add(key, value2);
				}
				RunReportResult runReportResult = tempCache.ContainsKey(key) ? ((RunReportResult)tempCache[key]) : null;
				bool flag3;
				if (runReportResult != null)
				{
					RunFunctionData primaryData = runReportResult.PrimaryData;
					if (((primaryData != null) ? primaryData.Table : null) != null && runReportResult.PrimaryData.Table.Rows.Count > 0)
					{
						flag3 = runReportResult.PrimaryData.Table.Columns.Contains(code.Name);
						goto IL_314;
					}
				}
				flag3 = false;
				IL_314:
				bool flag4 = flag3;
				if (flag4)
				{
					object obj = runReportResult.PrimaryData.Table.Rows[0][code.Name];
					bool flag5 = obj == DBNull.Value || obj == null;
					if (flag5)
					{
						code.Item.SetMailMergeValueDirectly(null);
					}
					else
					{
						this.SetMailMergeValueForWebSettingValue(ref code, obj);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0002B9E8 File Offset: 0x00029BE8
		private bool MailMergeCustomDictionary(ref MailMergingManager.MailMergeCodeWrapper code, MailMergeContextWithCustomDictionary ContextWithCustomDictionary)
		{
			MailMergeCustomDictionary customDictionary = ContextWithCustomDictionary.CustomDictionary;
			Dictionary<string, string> dictionary = (customDictionary != null) ? customDictionary.Args : null;
			bool flag = dictionary != null;
			if (flag)
			{
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					bool flag2 = !keyValuePair.Key.Equals(code.Name, StringComparison.OrdinalIgnoreCase);
					if (!flag2)
					{
						code.Item.SetMailMergeValue(keyValuePair.Value);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0002BA90 File Offset: 0x00029C90
		private bool TryToMailMergeAlternateFormatPublisher(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper Code)
		{
			eMailMergeCode? eMailMergeCode = Code.Name.FindMailMergeCode();
			bool flag = eMailMergeCode == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				switch (eMailMergeCode.Value)
				{
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatPublisherId:
					Code.Item.SetMailMergeValue(Context.AlternateFormatPublisherId);
					result = true;
					break;
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatPublisherName:
				{
					MediaPublisher alternateFormatPublisher = this.GetAlternateFormatPublisher(Context, tempCache);
					bool flag2 = alternateFormatPublisher != null;
					if (flag2)
					{
						Code.Item.SetMailMergeValue(alternateFormatPublisher.Name);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatPublisherEmail:
				{
					MediaPublisher alternateFormatPublisher = this.GetAlternateFormatPublisher(Context, tempCache);
					bool flag3 = alternateFormatPublisher != null;
					if (flag3)
					{
						Code.Item.SetMailMergeValue(alternateFormatPublisher.Email);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatPublisherPhone:
				{
					MediaPublisher alternateFormatPublisher = this.GetAlternateFormatPublisher(Context, tempCache);
					bool flag4 = alternateFormatPublisher != null;
					if (flag4)
					{
						Code.Item.SetMailMergeValue(alternateFormatPublisher.Phone);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatPublisherFax:
				{
					MediaPublisher alternateFormatPublisher = this.GetAlternateFormatPublisher(Context, tempCache);
					bool flag5 = alternateFormatPublisher != null;
					if (flag5)
					{
						Code.Item.SetMailMergeValue(alternateFormatPublisher.Fax);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatPublisherWebsite:
				{
					MediaPublisher alternateFormatPublisher = this.GetAlternateFormatPublisher(Context, tempCache);
					bool flag6 = alternateFormatPublisher != null;
					if (flag6)
					{
						Code.Item.SetMailMergeValue(alternateFormatPublisher.Website);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatPublisherAddress:
				{
					MediaPublisher alternateFormatPublisher = this.GetAlternateFormatPublisher(Context, tempCache);
					bool flag7 = alternateFormatPublisher != null;
					if (flag7)
					{
						Code.Item.SetMailMergeValue(alternateFormatPublisher.Address);
					}
					result = true;
					break;
				}
				default:
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0002BC18 File Offset: 0x00029E18
		private bool TryToMailMergeAlternateFormatVendor(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper Code)
		{
			eMailMergeCode? eMailMergeCode = Code.Name.FindMailMergeCode();
			bool flag = eMailMergeCode == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				switch (eMailMergeCode.Value)
				{
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatVendorId:
					Code.Item.SetMailMergeValue(Context.AlternateFormatVendorId);
					result = true;
					break;
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatVendorName:
				{
					MediaVendor alternateFormatVendor = this.GetAlternateFormatVendor(Context, tempCache);
					bool flag2 = alternateFormatVendor != null;
					if (flag2)
					{
						Code.Item.SetMailMergeValue(alternateFormatVendor.Name);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatVendorEmail:
				{
					MediaVendor alternateFormatVendor = this.GetAlternateFormatVendor(Context, tempCache);
					bool flag3 = alternateFormatVendor != null;
					if (flag3)
					{
						Code.Item.SetMailMergeValue(alternateFormatVendor.Email);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatVendorPhone:
				{
					MediaVendor alternateFormatVendor = this.GetAlternateFormatVendor(Context, tempCache);
					bool flag4 = alternateFormatVendor != null;
					if (flag4)
					{
						Code.Item.SetMailMergeValue(alternateFormatVendor.Phone);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatVendorFax:
				{
					MediaVendor alternateFormatVendor = this.GetAlternateFormatVendor(Context, tempCache);
					bool flag5 = alternateFormatVendor != null;
					if (flag5)
					{
						Code.Item.SetMailMergeValue(alternateFormatVendor.Fax);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatVendorWebsite:
				{
					MediaVendor alternateFormatVendor = this.GetAlternateFormatVendor(Context, tempCache);
					bool flag6 = alternateFormatVendor != null;
					if (flag6)
					{
						Code.Item.SetMailMergeValue(alternateFormatVendor.Website);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatVendorAddress:
				{
					MediaVendor alternateFormatVendor = this.GetAlternateFormatVendor(Context, tempCache);
					bool flag7 = alternateFormatVendor != null;
					if (flag7)
					{
						Code.Item.SetMailMergeValue(alternateFormatVendor.Address);
					}
					result = true;
					break;
				}
				default:
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0002BDA0 File Offset: 0x00029FA0
		private bool TryToMailMergeAlternateFormatMediaContent(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper Code)
		{
			eMailMergeCode? eMailMergeCode = Code.Name.FindMailMergeCode();
			bool flag = eMailMergeCode == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				switch (eMailMergeCode.Value)
				{
				case eMailMergeCode.AlternateFormatMediaContent_MediaContentId:
					Code.Item.SetMailMergeValue(Context.AlternateFormatMediaContentId.ToString());
					result = true;
					break;
				case eMailMergeCode.AlternateFormatMediaContent_MediaContentTitle:
				{
					MediaContent alternateFormatMediaContent = this.GetAlternateFormatMediaContent(Context, tempCache);
					bool flag2 = alternateFormatMediaContent != null;
					if (flag2)
					{
						Code.Item.SetMailMergeValue(alternateFormatMediaContent.ShortTitle ?? string.Empty);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatMediaContent_MediaContentISBN:
				{
					MediaContent alternateFormatMediaContent = this.GetAlternateFormatMediaContent(Context, tempCache);
					bool flag3 = alternateFormatMediaContent != null;
					if (flag3)
					{
						Code.Item.SetMailMergeValue(alternateFormatMediaContent.ISBN ?? string.Empty);
					}
					result = true;
					break;
				}
				default:
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0002BE8C File Offset: 0x0002A08C
		private bool TryToMailMergeAlternateFormatRequest(MailMergeContext context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper Code)
		{
			eMailMergeCode? eMailMergeCode = Code.Name.FindMailMergeCode();
			bool flag = eMailMergeCode == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				switch (eMailMergeCode.Value)
				{
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatRequestId:
					Code.Item.SetMailMergeValue(context.AlternateFormatRequestId);
					result = true;
					break;
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatRequestStatus:
				{
					MediaContentRequestedInfo alternateFormatRequest = this.GetAlternateFormatRequest(context, tempCache);
					bool flag2 = alternateFormatRequest != null;
					if (flag2)
					{
						Code.Item.SetMailMergeValue(alternateFormatRequest.RequestStatus.ToString());
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatRequestAvailableStartTime:
				{
					MediaContentRequestedInfo alternateFormatRequest = this.GetAlternateFormatRequest(context, tempCache);
					bool flag3 = alternateFormatRequest != null;
					if (flag3)
					{
						Code.Item.SetMailMergeValue(alternateFormatRequest.AvailableStartTime);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatRequestAvailableEndTime:
				{
					MediaContentRequestedInfo alternateFormatRequest = this.GetAlternateFormatRequest(context, tempCache);
					bool flag4 = alternateFormatRequest != null;
					if (flag4)
					{
						Code.Item.SetMailMergeValue(alternateFormatRequest.AvailableEndTime);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatRequestMediaContentTitle:
				{
					MediaContentRequestedInfo alternateFormatRequest = this.GetAlternateFormatRequest(context, tempCache);
					bool flag5 = ((alternateFormatRequest != null) ? alternateFormatRequest.ContentDetailRequested : null) != null;
					if (flag5)
					{
						Code.Item.SetMailMergeValue(alternateFormatRequest.ContentDetailRequested.MediaContent.ShortTitle);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatRequestMediaContentFormat:
				{
					MediaContentRequestedInfo alternateFormatRequest = this.GetAlternateFormatRequest(context, tempCache);
					bool flag6 = ((alternateFormatRequest != null) ? alternateFormatRequest.ContentDetailRequested : null) != null;
					if (flag6)
					{
						Code.Item.SetMailMergeValue(alternateFormatRequest.ContentDetailRequested.MediaContentFormat.ToString());
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatRequestMediaContentISBN:
				{
					MediaContentRequestedInfo alternateFormatRequest = this.GetAlternateFormatRequest(context, tempCache);
					bool flag7 = ((alternateFormatRequest != null) ? alternateFormatRequest.ContentDetailRequested : null) != null;
					if (flag7)
					{
						Code.Item.SetMailMergeValue(alternateFormatRequest.ContentDetailRequested.MediaContent.ISBN);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatRequestMediaContentAuthors:
				{
					MediaContentRequestedInfo alternateFormatRequest = this.GetAlternateFormatRequest(context, tempCache);
					bool flag8 = alternateFormatRequest != null;
					if (flag8)
					{
						Code.Item.SetMailMergeValue(alternateFormatRequest.ContentDetailRequested.MediaContent.Authors);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatRequestMediaContentEdition:
				{
					MediaContentRequestedInfo alternateFormatRequest = this.GetAlternateFormatRequest(context, tempCache);
					bool flag9 = ((alternateFormatRequest != null) ? alternateFormatRequest.ContentDetailRequested : null) != null;
					if (flag9)
					{
						Code.Item.SetMailMergeValue(alternateFormatRequest.ContentDetailRequested.MediaContent.Edition);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatRequestMediaContentCampus:
				{
					MediaContentRequestedInfo alternateFormatRequest = this.GetAlternateFormatRequest(context, tempCache);
					bool flag10 = alternateFormatRequest != null;
					if (flag10)
					{
						Code.Item.SetMailMergeValue((alternateFormatRequest.Campus != null) ? alternateFormatRequest.Campus.CampusName : string.Empty);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatRequestCreatedDatetime:
				{
					MediaContentRequestedInfo alternateFormatRequest = this.GetAlternateFormatRequest(context, tempCache);
					bool flag11 = alternateFormatRequest != null;
					if (flag11)
					{
						Code.Item.SetMailMergeValue(alternateFormatRequest.CreatedDatetime);
					}
					result = true;
					break;
				}
				case eMailMergeCode.AlternateFormatRequest_AlternateFormatRequestCompletedDatetime:
				{
					MediaContentRequestedInfo alternateFormatRequest = this.GetAlternateFormatRequest(context, tempCache);
					bool flag12 = alternateFormatRequest != null;
					if (flag12)
					{
						Code.Item.SetMailMergeValue(alternateFormatRequest.CompletedDateTime);
					}
					result = true;
					break;
				}
				default:
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0002C190 File Offset: 0x0002A390
		private bool TryToMailMergeProductLoan(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper Code)
		{
			eMailMergeCode? eMailMergeCode = Code.Name.FindMailMergeCode();
			bool flag = eMailMergeCode == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				switch (eMailMergeCode.Value)
				{
				case eMailMergeCode.InventoryProductLoan_LoanId:
					Code.Item.SetMailMergeValue(Context.LoanId);
					result = true;
					break;
				case eMailMergeCode.InventoryProductLoan_LoanedDate:
				{
					InventoryLoan productLoan = this.GetProductLoan(Context, tempCache);
					bool flag2 = productLoan != null;
					if (flag2)
					{
						Code.Item.SetMailMergeValue(productLoan.Group.LoanedDate);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProductLoan_DueDate:
				{
					InventoryLoan productLoan = this.GetProductLoan(Context, tempCache);
					bool flag3 = productLoan != null;
					if (flag3)
					{
						Code.Item.SetMailMergeValue(productLoan.Group.DueDate);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProductLoan_LoanNotes:
				{
					InventoryLoan productLoan = this.GetProductLoan(Context, tempCache);
					bool flag4 = productLoan != null;
					if (flag4)
					{
						Code.Item.SetMailMergeValue(productLoan.Group.LoanNotes ?? string.Empty);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProductLoan_WhoLoanedPersonId:
				{
					InventoryLoan productLoan = this.GetProductLoan(Context, tempCache);
					bool flag5 = productLoan != null;
					if (flag5)
					{
						Code.Item.SetMailMergeValue(productLoan.Group.WhoLoaned.PersonId);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProductLoan_WhoLoanedFirstname:
				{
					InventoryLoan productLoan = this.GetProductLoan(Context, tempCache);
					bool flag6 = productLoan != null;
					if (flag6)
					{
						Code.Item.SetMailMergeValue(productLoan.Group.WhoLoaned.FirstName ?? string.Empty);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProductLoan_WhoLoanedLastname:
				{
					InventoryLoan productLoan = this.GetProductLoan(Context, tempCache);
					bool flag7 = productLoan != null;
					if (flag7)
					{
						Code.Item.SetMailMergeValue(productLoan.Group.WhoLoaned.LastName ?? string.Empty);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProductLoan_Location:
				{
					InventoryLoan productLoan = this.GetProductLoan(Context, tempCache);
					bool flag8 = productLoan != null;
					if (flag8)
					{
						Code.Item.SetMailMergeValue((productLoan.Group.Location != null) ? productLoan.Group.Location.ToString() : string.Empty);
					}
					result = true;
					break;
				}
				default:
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0002C3B4 File Offset: 0x0002A5B4
		private bool TryToMailMergeProduct(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper Code)
		{
			eMailMergeCode? eMailMergeCode = Code.Name.FindMailMergeCode();
			bool flag = eMailMergeCode == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				switch (eMailMergeCode.Value)
				{
				case eMailMergeCode.InventoryProducts_ProductUniqueId:
					Code.Item.SetMailMergeValue(Context.ProductUniqueId);
					result = true;
					break;
				case eMailMergeCode.InventoryProducts_ProductId:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag2 = product != null;
					if (flag2)
					{
						Code.Item.SetMailMergeValue(product.ProductDynamicDataId);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductName:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag3 = product != null;
					if (flag3)
					{
						Code.Item.SetMailMergeValue(product.Name);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductCategory:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag4 = product != null;
					if (flag4)
					{
						Code.Item.SetMailMergeValue(product.CategoryName);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductSerialNumber:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag5 = product != null;
					if (flag5)
					{
						Code.Item.SetMailMergeValue(product.SerialNumber);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductBarcode:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag6 = product != null;
					if (flag6)
					{
						Code.Item.SetMailMergeValue(product.BarCode);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductBarcodeImage:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag7 = product != null;
					if (flag7)
					{
						Dictionary<string, string> args = Code.Item.Args;
						int imgWidth = (args != null && args.Count > 0 && args.ContainsKey("width")) ? int.Parse(args["width"]) : 200;
						int imgHeight = (args != null && args.Count > 0 && args.ContainsKey("height")) ? int.Parse(args["height"]) : 100;
						Image image = product.BarCode.Encode(imgWidth, imgHeight);
						byte[] array = (image != null) ? image.Serialize() : null;
						bool flag8 = array != null;
						if (flag8)
						{
							Code.Item.SetMailMergeValue(array);
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductStatus:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag9 = product != null;
					if (flag9)
					{
						MailMergeCode item = Code.Item;
						InventoryProductStatus status = product.Status;
						item.SetMailMergeValue((status != null) ? status.Name : null);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductOwner:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag10 = product != null;
					if (flag10)
					{
						MailMergeCode item2 = Code.Item;
						PersonBase inChargePerson = product.InChargePerson;
						item2.SetMailMergeValue((inChargePerson != null) ? inChargePerson.GetName() : null);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductDescription:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag11 = product != null;
					if (flag11)
					{
						Code.Item.SetMailMergeValue(product.Description);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductNotes:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag12 = product != null;
					if (flag12)
					{
						Code.Item.SetMailMergeValue(product.Notes);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductGroup:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag13 = product != null;
					if (flag13)
					{
						Code.Item.SetMailMergeValue((product.Group != null) ? product.Group.Name : null);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductIsLoaned:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag14 = product != null;
					if (flag14)
					{
						Code.Item.SetMailMergeValue(product.IsLoaned);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductLocationCampus:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag15 = product != null;
					if (flag15)
					{
						MailMergeCode item3 = Code.Item;
						InventoryLocation location = product.Location;
						item3.SetMailMergeValue((location != null) ? location.Campus : null);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductLocationBuilding:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag16 = product != null;
					if (flag16)
					{
						MailMergeCode item4 = Code.Item;
						InventoryLocation location2 = product.Location;
						item4.SetMailMergeValue((location2 != null) ? location2.Building : null);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductLocationRoom:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag17 = product != null;
					if (flag17)
					{
						MailMergeCode item5 = Code.Item;
						InventoryLocation location3 = product.Location;
						item5.SetMailMergeValue((location3 != null) ? location3.RoomNumber : null);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductLocationSeat:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag18 = product != null;
					if (flag18)
					{
						MailMergeCode item6 = Code.Item;
						InventoryLocation location4 = product.Location;
						item6.SetMailMergeValue((location4 != null) ? location4.Seat : null);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductLocationNotes:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag19 = product != null;
					if (flag19)
					{
						MailMergeCode item7 = Code.Item;
						InventoryLocation location5 = product.Location;
						item7.SetMailMergeValue((location5 != null) ? location5.Notes : null);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductLocation:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag20 = product != null;
					if (flag20)
					{
						MailMergeCode item8 = Code.Item;
						InventoryLocation location6 = product.Location;
						item8.SetMailMergeValue((location6 != null) ? location6.ToString() : null);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductVendorName:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag21 = product != null;
					if (flag21)
					{
						MailMergeCode item9 = Code.Item;
						InventoryVendorInfo vendor = product.Vendor;
						item9.SetMailMergeValue((vendor != null) ? vendor.VendorName : null);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductVendorPurchaseDate:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag22 = product != null;
					if (flag22)
					{
						MailMergeCode item10 = Code.Item;
						InventoryVendorInfo vendor2 = product.Vendor;
						item10.SetMailMergeValue((vendor2 != null) ? vendor2.PurchaseDate : null);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductVendorPurchaseAmount:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag23 = product != null;
					if (flag23)
					{
						MailMergeCode item11 = Code.Item;
						InventoryVendorInfo vendor3 = product.Vendor;
						item11.SetMailMergeValue((vendor3 != null) ? vendor3.PurchaseAmount : 0.0);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductVendorWarrantyExpiration:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag24 = product != null;
					if (flag24)
					{
						MailMergeCode item12 = Code.Item;
						InventoryVendorInfo vendor4 = product.Vendor;
						item12.SetMailMergeValue((vendor4 != null) ? vendor4.WarrantyExpDate : null);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductVendorPurchaseInfo:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag25 = product != null;
					if (flag25)
					{
						MailMergeCode item13 = Code.Item;
						InventoryVendorInfo vendor5 = product.Vendor;
						item13.SetMailMergeValue((vendor5 != null) ? vendor5.PurchaseInfo : null);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductThumbnail:
				{
					InventoryProduct product = this.GetProduct(Context, tempCache);
					bool flag26 = product != null;
					if (flag26)
					{
						Image thumbnail = product.Thumbnail;
						Code.Item.SetMailMergeValue((thumbnail != null) ? product.Thumbnail.Serialize() : null);
					}
					result = true;
					break;
				}
				case eMailMergeCode.InventoryProducts_ProductImage:
				{
					IInventoryAttachmentManager inventoryAttachmentManager = new InventoryAttachmentManager(this.OpContext);
					Image productPicture = inventoryAttachmentManager.GetProductPicture(new Guid(Context.ProductUniqueId));
					Code.Item.SetMailMergeValue((productPicture != null) ? productPicture.Serialize() : null);
					result = true;
					break;
				}
				default:
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0002CA68 File Offset: 0x0002AC68
		private StaffWithCommonInfo GetStaffCommonInfoByCustomAssignedAdvisorDropList(Dictionary<string, object> tempCache, int studentPid, int cid)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
			IList<IDynamicDataSerializableItem> list = dynamicDataManager.LoadDynamicDataItemsByControlIds(new DynamicDataContext
			{
				PrimaryId = studentPid
			}, new List<int>
			{
				cid
			}, eDynamicFormType.PerStudent);
			bool flag = list == null || list.Count < 1;
			StaffWithCommonInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DynamicDataStorageItem dynamicDataStorageItem = list[0].WriteToStorage();
				int valueOrDefault = dynamicDataStorageItem.IntValue.GetValueOrDefault();
				bool flag2 = valueOrDefault < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					string key = "staffCommonInfoByCidAndPid_" + valueOrDefault.ToString() + "_" + cid.ToString();
					StaffWithCommonInfo staffWithCommonInfo = (StaffWithCommonInfo)tempCache[key];
					bool flag3 = staffWithCommonInfo == null;
					if (flag3)
					{
						StaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(this.OpContext);
						staffWithCommonInfo = staffCommonInfoManager.LoadStaffWithCommonInfoById(valueOrDefault);
						tempCache.Add(key, staffWithCommonInfo);
					}
					result = staffWithCommonInfo;
				}
			}
			return result;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0002CB54 File Offset: 0x0002AD54
		private string GetFirstLetter(string s)
		{
			string text = (s ?? "").Trim();
			return (text.Length > 0) ? text.Substring(0, 1) : string.Empty;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0002CB90 File Offset: 0x0002AD90
		private bool TryToMailMergeBaseBuiltInCodesForStudent(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper Code)
		{
			eMailMergeCode? eMailMergeCode = Code.Name.FindMailMergeCode();
			bool flag = eMailMergeCode == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				int personId = Code.GetPersonId(Context);
				MailMergeCode item = Code.Item;
				Dictionary<string, string> dictionary = ((item != null) ? item.Args : null) ?? new Dictionary<string, string>();
				bool flag2 = dictionary.ContainsKey("cid");
				int num;
				if (flag2)
				{
					int.TryParse(dictionary["cid"], out num);
				}
				else
				{
					num = 0;
				}
				StudentCommonInfo studentCommonInfo = null;
				switch (eMailMergeCode.Value)
				{
				case eMailMergeCode.STUDENT_PersonId:
					Code.Item.SetMailMergeValue(personId);
					result = true;
					break;
				case eMailMergeCode.STUDENT_FirstName:
				{
					PersonBase person = this.GetPerson(Context, tempCache, Code);
					bool flag3 = person != null;
					if (flag3)
					{
						Code.Item.SetMailMergeValue(person.FirstName);
					}
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_PreferredFirstName:
				{
					bool flag4 = !tempCache.ContainsKey("preferredname");
					string text;
					if (flag4)
					{
						int num2;
						if (num <= 0)
						{
							DynamicField dynamicField = this.dynamicFieldManager.LoadFieldByName("preferredname");
							num2 = ((dynamicField != null) ? dynamicField.ControlId : 0);
						}
						else
						{
							num2 = num;
						}
						int num3 = num2;
						bool flag5 = num3 > 0;
						if (flag5)
						{
							List<DynamicData> list = this.dynamicDataManager.LoadDataByFields(new DynamicDataContext
							{
								PrimaryId = personId
							}, new List<int>
							{
								num3
							}, eDynamicFormType.PerStudent);
							text = ((list.Count > 0) ? list[0].GetString() : null);
							bool flag6 = !string.IsNullOrWhiteSpace(text);
							if (flag6)
							{
								tempCache.Add("preferredname", text);
							}
						}
						else
						{
							text = null;
						}
					}
					else
					{
						text = (string)tempCache["preferredname"];
					}
					bool flag7 = string.IsNullOrWhiteSpace(text);
					if (flag7)
					{
						PersonBase person = this.GetPerson(Context, tempCache, Code);
						bool flag8 = person != null;
						if (flag8)
						{
							Code.Item.SetMailMergeValue(person.FirstName);
							result = true;
							break;
						}
					}
					Code.Item.SetMailMergeValue(text);
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_MiddleName:
				{
					PersonBase person = this.GetPerson(Context, tempCache, Code);
					bool flag9 = person != null;
					if (flag9)
					{
						Code.Item.SetMailMergeValue(person.MiddleName);
					}
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_LastName:
				{
					PersonBase person = this.GetPerson(Context, tempCache, Code);
					bool flag10 = person != null;
					if (flag10)
					{
						Code.Item.SetMailMergeValue(person.LastName);
					}
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_Initials:
				{
					PersonBase person = this.GetPerson(Context, tempCache, Code);
					bool flag11 = person != null;
					if (flag11)
					{
						string text2 = string.Join(".", (from h in new string[]
						{
							(person != null) ? person.FirstName : null,
							(person != null) ? person.MiddleName : null,
							(person != null) ? person.LastName : null
						}.Select(new Func<string, string>(this.GetFirstLetter))
						where h.Length > 0
						select h).ToArray<string>());
						Code.Item.SetMailMergeValue((text2.Length > 0) ? (text2 + ".") : "");
					}
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_Student_no:
				{
					PersonBase person = this.GetPerson(Context, tempCache, Code);
					bool flag12 = person != null;
					if (flag12)
					{
						Code.Item.SetMailMergeValue(person.Student_no);
					}
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_Age:
				{
					OldUserSettingManager oldUserSettingManager = this.oldUserSettingManager;
					int dobCid = oldUserSettingManager.GetSettingValue_Int(Context.WhoAmId, eSettingCode.SETTING_MedicalScheduler_BirthDateControlId);
					List<DynamicData> perStudentData = this.GetPerStudentData(Context, tempCache, Code);
					DynamicData dynamicData = perStudentData.Find((DynamicData f) => f.Field.ControlId == dobCid);
					bool flag13 = dynamicData != null;
					if (flag13)
					{
						bool flag14 = dynamicData.Value is DateTime;
						if (flag14)
						{
							DateTime birthDate = (DateTime)dynamicData.Value;
							int num4 = this.CalculateAge(birthDate);
							bool flag15 = num4 > 0;
							if (flag15)
							{
								Code.Item.SetMailMergeValue(num4);
								result = true;
								break;
							}
						}
						else
						{
							bool flag16 = dynamicData.Value is DynamicData;
							if (flag16)
							{
								DynamicData dynamicData2 = (DynamicData)dynamicData.Value;
								bool flag17 = dynamicData2.Value is DateTime;
								if (flag17)
								{
									DateTime birthDate = (DateTime)dynamicData2.Value;
									int num5 = this.CalculateAge(birthDate);
									bool flag18 = num5 > 0;
									if (flag18)
									{
										Code.Item.SetMailMergeValue(num5);
										result = true;
										break;
									}
								}
							}
						}
					}
					Code.Item.SetMailMergeValue("");
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_StudentEmail:
				{
					bool flag19 = studentCommonInfo == null;
					if (flag19)
					{
						studentCommonInfo = this.GetPersonCommonInfo(Context, ref tempCache, Code);
					}
					bool flag20 = studentCommonInfo != null;
					if (flag20)
					{
						Code.Item.SetMailMergeValue(studentCommonInfo.Email ?? "");
					}
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_StudentPhone:
				{
					bool flag21 = studentCommonInfo == null;
					if (flag21)
					{
						studentCommonInfo = this.GetPersonCommonInfo(Context, ref tempCache, Code);
					}
					bool flag22 = studentCommonInfo != null;
					if (flag22)
					{
						Code.Item.SetMailMergeValue(studentCommonInfo.Phone ?? "");
					}
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_AccommodationsExpiry:
				{
					DateTime? studentAccommodationsExpiryDate = this.accommodationsManager.GetStudentAccommodationsExpiryDate(personId);
					bool flag23 = studentAccommodationsExpiryDate != null;
					if (flag23)
					{
						Code.Item.SetMailMergeValue(studentAccommodationsExpiryDate.Value);
					}
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_HeShe:
				{
					bool flag24 = studentCommonInfo == null;
					if (flag24)
					{
						studentCommonInfo = this.GetPersonCommonInfo(Context, ref tempCache, Code);
					}
					bool flag25 = studentCommonInfo != null;
					if (flag25)
					{
						eGender gender = studentCommonInfo.Gender;
						eGender eGender = gender;
						if (eGender != eGender.Female)
						{
							if (eGender != eGender.Male)
							{
								result = true;
							}
							else
							{
								Code.Item.SetMailMergeValue("He");
								result = true;
							}
						}
						else
						{
							Code.Item.SetMailMergeValue("She");
							result = true;
						}
					}
					else
					{
						Code.Item.SetMailMergeValue("He/she");
						result = true;
					}
					break;
				}
				case eMailMergeCode.STUDENT_HeSheLower:
				{
					bool flag26 = studentCommonInfo == null;
					if (flag26)
					{
						studentCommonInfo = this.GetPersonCommonInfo(Context, ref tempCache, Code);
					}
					bool flag27 = studentCommonInfo != null;
					if (flag27)
					{
						eGender gender2 = studentCommonInfo.Gender;
						eGender eGender2 = gender2;
						if (eGender2 != eGender.Female)
						{
							if (eGender2 != eGender.Male)
							{
								result = true;
							}
							else
							{
								Code.Item.SetMailMergeValue("he");
								result = true;
							}
						}
						else
						{
							Code.Item.SetMailMergeValue("she");
							result = true;
						}
					}
					else
					{
						Code.Item.SetMailMergeValue("he/she");
						result = true;
					}
					break;
				}
				case eMailMergeCode.STUDENT_Counsellor:
				{
					bool flag28 = num < 1;
					if (flag28)
					{
						bool flag29 = studentCommonInfo == null;
						if (flag29)
						{
							studentCommonInfo = this.GetPersonCommonInfo(Context, ref tempCache, Code);
						}
						bool flag30 = studentCommonInfo != null && studentCommonInfo.AssignedCounsellor != null;
						if (flag30)
						{
							Code.Item.SetMailMergeValue(string.Format("{0} {1}", studentCommonInfo.AssignedCounsellor.FirstName ?? "", studentCommonInfo.AssignedCounsellor.LastName ?? ""));
						}
					}
					else
					{
						StaffWithCommonInfo staffCommonInfoByCustomAssignedAdvisorDropList = this.GetStaffCommonInfoByCustomAssignedAdvisorDropList(tempCache, Code.GetPersonId(Context), num);
						bool flag31 = staffCommonInfoByCustomAssignedAdvisorDropList != null && staffCommonInfoByCustomAssignedAdvisorDropList.Staff != null;
						if (flag31)
						{
							Code.Item.SetMailMergeValue(string.Format("{0} {1}", staffCommonInfoByCustomAssignedAdvisorDropList.Staff.FirstName ?? "", staffCommonInfoByCustomAssignedAdvisorDropList.Staff.LastName ?? ""));
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_CounsellorFirstName:
				{
					bool flag32 = num < 1;
					if (flag32)
					{
						bool flag33 = studentCommonInfo == null;
						if (flag33)
						{
							studentCommonInfo = this.GetPersonCommonInfo(Context, ref tempCache, Code);
						}
						bool flag34 = studentCommonInfo != null && studentCommonInfo.AssignedCounsellor != null;
						if (flag34)
						{
							Code.Item.SetMailMergeValue(studentCommonInfo.AssignedCounsellor.FirstName ?? "");
						}
					}
					else
					{
						StaffWithCommonInfo staffCommonInfoByCustomAssignedAdvisorDropList2 = this.GetStaffCommonInfoByCustomAssignedAdvisorDropList(tempCache, Code.GetPersonId(Context), num);
						bool flag35 = staffCommonInfoByCustomAssignedAdvisorDropList2 != null && staffCommonInfoByCustomAssignedAdvisorDropList2.Staff != null;
						if (flag35)
						{
							Code.Item.SetMailMergeValue(staffCommonInfoByCustomAssignedAdvisorDropList2.Staff.FirstName ?? "");
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_CounsellorLastName:
				{
					bool flag36 = num < 1;
					if (flag36)
					{
						bool flag37 = studentCommonInfo == null;
						if (flag37)
						{
							studentCommonInfo = this.GetPersonCommonInfo(Context, ref tempCache, Code);
						}
						bool flag38 = studentCommonInfo != null && studentCommonInfo.AssignedCounsellor != null;
						if (flag38)
						{
							Code.Item.SetMailMergeValue(studentCommonInfo.AssignedCounsellor.LastName ?? "");
						}
					}
					else
					{
						StaffWithCommonInfo staffCommonInfoByCustomAssignedAdvisorDropList3 = this.GetStaffCommonInfoByCustomAssignedAdvisorDropList(tempCache, Code.GetPersonId(Context), num);
						bool flag39 = ((staffCommonInfoByCustomAssignedAdvisorDropList3 != null) ? staffCommonInfoByCustomAssignedAdvisorDropList3.Staff : null) != null;
						if (flag39)
						{
							Code.Item.SetMailMergeValue(staffCommonInfoByCustomAssignedAdvisorDropList3.Staff.LastName ?? "");
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_CounsellorEmail:
				{
					bool flag40 = num < 1;
					if (flag40)
					{
						bool flag41 = studentCommonInfo == null;
						if (flag41)
						{
							studentCommonInfo = this.GetPersonCommonInfo(Context, ref tempCache, Code);
						}
						bool flag42 = ((studentCommonInfo != null) ? studentCommonInfo.AssignedCounsellor : null) != null;
						if (flag42)
						{
							Code.Item.SetMailMergeValue(studentCommonInfo.AssignedCounsellorEmail ?? "");
						}
					}
					else
					{
						StaffWithCommonInfo staffCommonInfoByCustomAssignedAdvisorDropList4 = this.GetStaffCommonInfoByCustomAssignedAdvisorDropList(tempCache, Code.GetPersonId(Context), num);
						bool flag43 = ((staffCommonInfoByCustomAssignedAdvisorDropList4 != null) ? staffCommonInfoByCustomAssignedAdvisorDropList4.StaffCommonInfo : null) != null;
						if (flag43)
						{
							Code.Item.SetMailMergeValue(staffCommonInfoByCustomAssignedAdvisorDropList4.StaffCommonInfo.Email ?? "");
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_CounsellorPhone:
				{
					bool flag44 = num < 1;
					if (flag44)
					{
						bool flag45 = studentCommonInfo == null;
						if (flag45)
						{
							studentCommonInfo = this.GetPersonCommonInfo(Context, ref tempCache, Code);
						}
						bool flag46 = ((studentCommonInfo != null) ? studentCommonInfo.AssignedCounsellor : null) != null;
						if (flag46)
						{
							Code.Item.SetMailMergeValue(studentCommonInfo.AssignedCounsellorPhone ?? "");
						}
					}
					else
					{
						StaffWithCommonInfo staffCommonInfoByCustomAssignedAdvisorDropList5 = this.GetStaffCommonInfoByCustomAssignedAdvisorDropList(tempCache, Code.GetPersonId(Context), num);
						bool flag47 = staffCommonInfoByCustomAssignedAdvisorDropList5 != null && staffCommonInfoByCustomAssignedAdvisorDropList5.StaffCommonInfo != null;
						if (flag47)
						{
							Code.Item.SetMailMergeValue(staffCommonInfoByCustomAssignedAdvisorDropList5.StaffCommonInfo.Phone ?? "");
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_CounsellorTitle:
				{
					bool flag48 = num < 1;
					if (flag48)
					{
						bool flag49 = studentCommonInfo == null;
						if (flag49)
						{
							studentCommonInfo = this.GetPersonCommonInfo(Context, ref tempCache, Code);
						}
						bool flag50 = ((studentCommonInfo != null) ? studentCommonInfo.AssignedCounsellorTitle : null) != null;
						if (flag50)
						{
							Code.Item.SetMailMergeValue(studentCommonInfo.AssignedCounsellorTitle ?? "");
						}
					}
					else
					{
						StaffWithCommonInfo staffCommonInfoByCustomAssignedAdvisorDropList6 = this.GetStaffCommonInfoByCustomAssignedAdvisorDropList(tempCache, Code.GetPersonId(Context), num);
						bool flag51 = ((staffCommonInfoByCustomAssignedAdvisorDropList6 != null) ? staffCommonInfoByCustomAssignedAdvisorDropList6.StaffCommonInfo : null) != null;
						if (flag51)
						{
							Code.Item.SetMailMergeValue(staffCommonInfoByCustomAssignedAdvisorDropList6.StaffCommonInfo.Title ?? "");
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_CounsellorContact:
				{
					bool flag52 = num < 1;
					if (flag52)
					{
						bool flag53 = studentCommonInfo == null;
						if (flag53)
						{
							studentCommonInfo = this.GetPersonCommonInfo(Context, ref tempCache, Code);
						}
						bool flag54 = ((studentCommonInfo != null) ? studentCommonInfo.AssignedCounsellor : null) != null;
						if (flag54)
						{
							bool flag55 = !string.IsNullOrEmpty(studentCommonInfo.AssignedCounsellorEmail);
							bool flag56 = !string.IsNullOrEmpty(studentCommonInfo.AssignedCounsellorPhone);
							bool flag57 = flag55 && flag56;
							if (flag57)
							{
								Code.Item.SetMailMergeValue(studentCommonInfo.AssignedCounsellorPhone + " (phone) or " + studentCommonInfo.AssignedCounsellorEmail + " (email)");
							}
							else
							{
								bool flag58 = flag55;
								if (flag58)
								{
									Code.Item.SetMailMergeValue(studentCommonInfo.AssignedCounsellorEmail + " (email)");
								}
								else
								{
									bool flag59 = flag56;
									if (flag59)
									{
										Code.Item.SetMailMergeValue(studentCommonInfo.AssignedCounsellorPhone + " (email)");
									}
								}
							}
						}
					}
					else
					{
						StaffWithCommonInfo staffCommonInfoByCustomAssignedAdvisorDropList7 = this.GetStaffCommonInfoByCustomAssignedAdvisorDropList(tempCache, Code.GetPersonId(Context), num);
						bool flag60 = ((staffCommonInfoByCustomAssignedAdvisorDropList7 != null) ? staffCommonInfoByCustomAssignedAdvisorDropList7.StaffCommonInfo : null) != null;
						if (flag60)
						{
							bool flag61 = !string.IsNullOrEmpty(staffCommonInfoByCustomAssignedAdvisorDropList7.StaffCommonInfo.Email);
							bool flag62 = !string.IsNullOrEmpty(staffCommonInfoByCustomAssignedAdvisorDropList7.StaffCommonInfo.Phone);
							bool flag63 = flag61 && flag62;
							if (flag63)
							{
								Code.Item.SetMailMergeValue(staffCommonInfoByCustomAssignedAdvisorDropList7.StaffCommonInfo.Phone + " (phone) or " + staffCommonInfoByCustomAssignedAdvisorDropList7.StaffCommonInfo.Email + " (email)");
							}
							else
							{
								bool flag64 = flag61;
								if (flag64)
								{
									Code.Item.SetMailMergeValue(staffCommonInfoByCustomAssignedAdvisorDropList7.StaffCommonInfo.Email + " (email)");
								}
								else
								{
									bool flag65 = flag62;
									if (flag65)
									{
										Code.Item.SetMailMergeValue(staffCommonInfoByCustomAssignedAdvisorDropList7.StaffCommonInfo.Phone + " (email)");
									}
								}
							}
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_CounsellorSignature:
				{
					int num6 = 0;
					bool flag66 = num < 1;
					if (flag66)
					{
						bool flag67 = studentCommonInfo == null;
						if (flag67)
						{
							studentCommonInfo = this.GetPersonCommonInfo(Context, ref tempCache, Code);
						}
						bool flag68 = ((studentCommonInfo != null) ? studentCommonInfo.AssignedCounsellor : null) != null && studentCommonInfo.AssignedCounsellor.PersonId > 0;
						if (flag68)
						{
							num6 = studentCommonInfo.AssignedCounsellor.PersonId;
						}
					}
					else
					{
						StaffWithCommonInfo staffCommonInfoByCustomAssignedAdvisorDropList8 = this.GetStaffCommonInfoByCustomAssignedAdvisorDropList(tempCache, Code.GetPersonId(Context), num);
						num6 = ((((staffCommonInfoByCustomAssignedAdvisorDropList8 != null) ? staffCommonInfoByCustomAssignedAdvisorDropList8.Staff : null) == null) ? 0 : staffCommonInfoByCustomAssignedAdvisorDropList8.Staff.PersonId);
					}
					bool flag69 = num6 > 0;
					if (flag69)
					{
						DynamicData dynamicData3 = this.staffCommonInfoManager.LoadStaffStoredSignatureData(num6);
						bool flag70 = !(((dynamicData3 != null) ? dynamicData3.Value : null) is byte[]);
						if (flag70)
						{
							result = true;
							break;
						}
						byte[] array = (byte[])dynamicData3.Value;
						bool flag71 = array == null;
						if (flag71)
						{
							result = true;
							break;
						}
						eControlCode controlCode = dynamicData3.Field.ControlCode;
						eControlCode eControlCode = controlCode;
						if (eControlCode != eControlCode.Picture)
						{
							if (eControlCode == eControlCode.File)
							{
								int num7 = 6;
								byte[] array2 = new byte[num7];
								for (int i = 0; i < num7; i++)
								{
									array2[i] = array[i];
								}
								UTF8Encoding utf8Encoding = new UTF8Encoding();
								string @string = utf8Encoding.GetString(array2);
								int num8 = int.Parse(@string);
								byte[] array3 = new byte[num8];
								for (int j = 0; j < num8; j++)
								{
									array3[j] = array[j + num7];
								}
								string args = (array3 == null) ? "" : utf8Encoding.GetString(array3);
								StringDictionary stringDictionary = MailMergingManager.ParseArgs(args, new char[]
								{
									';'
								});
								int num9 = array.Length - num7 - num8;
								byte[] array4 = new byte[num9];
								for (int k = 0; k < array4.Length; k++)
								{
									array4[k] = array[k + num8 + num7];
								}
								Code.Item.SetMailMergeValue(array4);
							}
						}
						else
						{
							Code.Item.SetMailMergeValue(array);
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_DateAdded:
				{
					bool flag72 = personId > 0;
					if (flag72)
					{
						DateTime personDateAdded = this.peopleManager.GetPersonDateAdded(personId);
						bool flag73 = personDateAdded != DateTime.MinValue;
						if (flag73)
						{
							Code.Item.SetMailMergeValue(personDateAdded);
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_InstructorUrl:
				{
					string text3 = this.oldUserSettingsManager.GetSettingValue_String(Context.WhoAmId, eSettingCode.SETTING_WebBaseUrl, false);
					bool flag74 = text3 == null;
					if (flag74)
					{
						text3 = "";
					}
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
					OperationContext opContext = this.OpContext;
					IEncryption encryption = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption;
					Code.Item.SetMailMergeValue(string.Concat(new string[]
					{
						text3,
						"/user/instructor/iletter.aspx?lucid=",
						((course != null) ? course.LuCourseId : 0).EncodeUrlVariable(encryption),
						"&pid=",
						personId.EncodeUrlVariable(encryption)
					}));
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_StudentUrl:
				{
					string str = this.oldUserSettingsManager.GetSettingValue_String(Context.WhoAmId, eSettingCode.SETTING_WebBaseUrl, false) ?? "";
					LookupCourse course2 = this.GetCourse(Context, tempCache, Code);
					eDatabaseConnectionStringName csName2 = eDatabaseConnectionStringName.ClockWork;
					OperationContext opContext2 = this.OpContext;
					IEncryption encryption2 = DatabaseLayerFactory.GetDatabaseLayer(csName2, (opContext2 != null) ? opContext2.TenantId : null).Encryption;
					Code.Item.SetMailMergeValue(str + "/user/test/AccommodationsLetter.aspx?lucid=" + ((course2 != null) ? course2.LuCourseId : 0).EncodeUrlVariable(encryption2));
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_StudentSignature:
				{
					DynamicField dynamicField2 = this.dynamicFieldManager.LoadFieldByName("Student Accommodation Signature");
					bool flag75 = dynamicField2 != null;
					if (flag75)
					{
						Code.Item.SetMailMergeValue(this.dynamicDataManager.LoadDataByFields(new DynamicDataContext
						{
							PrimaryId = personId
						}, new List<int>
						{
							dynamicField2.ControlId
						}, eDynamicFormType.PerStudent));
					}
					result = true;
					break;
				}
				case eMailMergeCode.STUDENT_StaffSignature:
				{
					DynamicField dynamicField3 = this.dynamicFieldManager.LoadFieldByName("Staff Accommodation Signature");
					bool flag76 = dynamicField3 != null;
					if (flag76)
					{
						List<DynamicData> list2 = this.dynamicDataManager.LoadDataByFields(new DynamicDataContext
						{
							PrimaryId = personId
						}, new List<int>
						{
							dynamicField3.ControlId
						}, eDynamicFormType.PerStudent);
						bool flag77 = list2 == null || list2.Count < 1;
						if (flag77)
						{
							bool flag78 = studentCommonInfo == null;
							if (flag78)
							{
								studentCommonInfo = this.GetPersonCommonInfo(Context, ref tempCache, Code);
							}
							bool flag79 = ((studentCommonInfo != null) ? studentCommonInfo.AssignedCounsellor : null) != null && studentCommonInfo.AssignedCounsellor.PersonId > 0;
							if (flag79)
							{
								DynamicData dynamicData4 = this.staffCommonInfoManager.LoadStaffStoredSignatureData(studentCommonInfo.AssignedCounsellor.PersonId);
								bool flag80 = dynamicData4 != null;
								if (flag80)
								{
									bool flag81 = list2 == null;
									if (flag81)
									{
										list2 = new List<DynamicData>();
									}
									list2.Add(dynamicData4);
								}
							}
						}
						Code.Item.SetMailMergeValue(list2);
					}
					result = true;
					break;
				}
				default:
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0002DDAC File Offset: 0x0002BFAC
		private static StringDictionary ParseArgs(string args, char[] delimiter)
		{
			string[] array = args.Split(delimiter);
			StringDictionary stringDictionary = new StringDictionary();
			foreach (string text in array)
			{
				bool flag = text.Trim().Length <= 0;
				if (!flag)
				{
					int num = text.IndexOf('=');
					bool flag2 = num > 0;
					if (flag2)
					{
						stringDictionary.Add(text.Substring(0, num), text.Substring(num + 1));
					}
					else
					{
						stringDictionary.Add(text, "");
					}
				}
			}
			return stringDictionary;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0002DE44 File Offset: 0x0002C044
		private PersonBase GetWhoAmI(ref Dictionary<string, object> tempCache)
		{
			string key = "whoami";
			bool flag = tempCache.ContainsKey(key);
			PersonBase result;
			if (flag)
			{
				result = (PersonBase)tempCache[key];
			}
			else
			{
				IPeopleManager peopleManager = new PeopleManager(this.OpContext);
				PersonBase personBase = peopleManager.LoadPerson(this.OpContext.WhoAmI);
				tempCache.Add(key, personBase);
				result = personBase;
			}
			return result;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0002DEA4 File Offset: 0x0002C0A4
		private Session GetCurrentSession(MailMergeContext Context, ref Dictionary<string, object> tempCache)
		{
			bool flag = tempCache.ContainsKey("currentsession");
			Session result;
			if (flag)
			{
				result = (Session)tempCache["currentsession"];
			}
			else
			{
				Session currentSession = this.sessionManager.GetCurrentSession();
				tempCache.Add("currentsession", currentSession);
				result = currentSession;
			}
			return result;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0002DEF8 File Offset: 0x0002C0F8
		private string GetCourseDescription(LookupCourse course)
		{
			bool flag = course == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = string.Concat(new string[]
				{
					(course.Subject == null) ? "" : (course.Subject.SubjectDescription ?? ""),
					" ",
					course.Course,
					" ",
					course.Section,
					" ",
					course.TimeOfDay
				});
			}
			return result;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0002DF80 File Offset: 0x0002C180
		private bool TryToMailMergeAppointment(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper Code)
		{
			eMailMergeCode? eMailMergeCode = Code.Name.FindMailMergeCode();
			bool flag = eMailMergeCode == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				eMailMergeCode value = eMailMergeCode.Value;
				eMailMergeCode eMailMergeCode2 = value;
				if (eMailMergeCode2 != eMailMergeCode.APPOINTMENTS_SUBTITLE)
				{
					switch (eMailMergeCode2)
					{
					case eMailMergeCode.APPOINTMENTS_AppointmentId:
						Code.Item.SetMailMergeValue(Context.AppointmentId);
						result = true;
						break;
					case eMailMergeCode.APPOINTMENTS_AppDate:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag2 = appointment != null;
						if (flag2)
						{
							Code.Item.SetMailMergeValue(appointment.StartDateTime);
							Code.Item.SetValueFormatIfNotOverridenByUser(new MailMergeValueFormat
							{
								ValueFormatType = eValueFormatType.CustomFormat,
								CustomFormat = (Context.DefaultDateFormat ?? "dddd MMMM d, yyyy")
							});
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_ScheduledDate2:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag3 = appointment != null;
						if (flag3)
						{
							Code.Item.SetMailMergeValue(appointment.StartDateTime);
							Code.Item.SetValueFormatIfNotOverridenByUser(new MailMergeValueFormat
							{
								ValueFormatType = eValueFormatType.CustomFormat,
								CustomFormat = (Context.DefaultDateFormat ?? "MM/dd/yy")
							});
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_ScheduledDate3:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag4 = appointment != null;
						if (flag4)
						{
							Code.Item.SetMailMergeValue(appointment.StartDateTime);
							Code.Item.SetValueFormatIfNotOverridenByUser(new MailMergeValueFormat
							{
								ValueFormatType = eValueFormatType.CustomFormat,
								CustomFormat = (Context.DefaultDateFormat ?? "yyyy-MM-dd")
							});
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AppStartTime:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag5 = appointment != null;
						if (flag5)
						{
							Code.Item.SetMailMergeValue(appointment.StartDateTime);
							Code.Item.SetValueFormatIfNotOverridenByUser(new MailMergeValueFormat
							{
								ValueFormatType = eValueFormatType.CustomFormat,
								CustomFormat = (Context.DefaultTimeFormat ?? "h:mm tt")
							});
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AppEndTime:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag6 = appointment != null;
						if (flag6)
						{
							Code.Item.SetMailMergeValue(appointment.EndDateTime);
							Code.Item.SetValueFormatIfNotOverridenByUser(new MailMergeValueFormat
							{
								ValueFormatType = eValueFormatType.CustomFormat,
								CustomFormat = (Context.DefaultTimeFormat ?? "h:mm tt")
							});
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_ScheduledStartDateTime:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag7 = appointment != null;
						if (flag7)
						{
							Code.Item.SetMailMergeValue(appointment.StartDateTime);
							Code.Item.SetValueFormatIfNotOverridenByUser(new MailMergeValueFormat
							{
								ValueFormatType = eValueFormatType.CustomFormat,
								CustomFormat = (Context.DefaultDateFormat ?? "MMMM d, yyyy") + "  " + (Context.DefaultTimeFormat ?? "h:mm tt")
							});
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_ScheduledEndDateTime:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag8 = appointment != null;
						if (flag8)
						{
							Code.Item.SetMailMergeValue(appointment.EndDateTime);
							Code.Item.SetValueFormatIfNotOverridenByUser(new MailMergeValueFormat
							{
								ValueFormatType = eValueFormatType.CustomFormat,
								CustomFormat = (Context.DefaultDateFormat ?? "MMMM d, yyyy") + "  " + (Context.DefaultTimeFormat ?? "h:mm tt")
							});
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AppStartDateDayOfWeek:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag9 = appointment != null;
						if (flag9)
						{
							Code.Item.SetMailMergeValue(appointment.StartDateTime);
							Code.Item.SetValueFormatIfNotOverridenByUser(new MailMergeValueFormat
							{
								ValueFormatType = eValueFormatType.CustomFormat,
								CustomFormat = "dddd"
							});
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AppDurationMinutes:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag10 = appointment != null;
						if (flag10)
						{
							TimeSpan timeSpan = appointment.EndDateTime - appointment.StartDateTime;
							Code.Item.SetMailMergeValue(Convert.ToInt32(timeSpan.TotalMinutes));
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AppDuration:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag11 = appointment != null;
						if (flag11)
						{
							int durationMinutes = Convert.ToInt32((appointment.EndDateTime - appointment.StartDateTime).TotalMinutes);
							Code.Item.SetMailMergeValue(this.GetDurationDescription(durationMinutes));
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AppTime:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag12 = appointment != null;
						if (flag12)
						{
							MailMergeCode item = Code.Item;
							string format = "{0} to {1}";
							DateTime dateTime = appointment.StartDateTime;
							object arg = dateTime.ToString(Context.DefaultTimeFormat ?? "h:mm tt");
							dateTime = appointment.EndDateTime;
							item.SetMailMergeValue(string.Format(format, arg, dateTime.ToString(Context.DefaultTimeFormat ?? "h:mm tt")));
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_Memo:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag13 = appointment != null;
						if (flag13)
						{
							string text = appointment.Memo ?? "";
							bool flag14 = text.StartsWith("{\\rtf1", StringComparison.OrdinalIgnoreCase);
							if (flag14)
							{
								text = text.ConvertRtfToPlainText();
							}
							Code.Item.SetMailMergeValue(text);
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AppDescription:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag15 = ((appointment != null) ? appointment.AppType : null) != null;
						if (flag15)
						{
							Code.Item.SetMailMergeValue(appointment.AppType.Description ?? "");
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AppTypeId:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag16 = ((appointment != null) ? appointment.AppType : null) != null;
						if (flag16)
						{
							Code.Item.SetMailMergeValue(appointment.AppType.AppTypeId);
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AppCode:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag17 = ((appointment != null) ? appointment.ShowTimeAs : null) != null;
						if (flag17)
						{
							Code.Item.SetMailMergeValue(appointment.ShowTimeAs.Title ?? "");
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AppCodeId:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag18 = ((appointment != null) ? appointment.ShowTimeAs : null) != null;
						if (flag18)
						{
							Code.Item.SetMailMergeValue(appointment.ShowTimeAs.AppointmentShowTimeAsId.ToString());
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_RoomPid:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag19 = ((appointment != null) ? appointment.Room : null) != null;
						if (flag19)
						{
							AppointmentRoom room = appointment.Room;
							bool flag20 = room != null;
							if (flag20)
							{
								Code.Item.SetMailMergeValue(room.RoomId);
							}
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_Room:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag21 = ((appointment != null) ? appointment.Room : null) != null;
						if (flag21)
						{
							AppointmentRoom room2 = appointment.Room;
							bool flag22 = room2 != null;
							if (flag22)
							{
								Code.Item.SetMailMergeValue(room2.RoomTitle ?? "");
							}
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_RoomDescriptionFirstWord:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag23 = ((appointment != null) ? appointment.Room : null) != null;
						if (flag23)
						{
							AppointmentRoom room3 = appointment.Room;
							bool flag24 = room3 != null;
							if (flag24)
							{
								string text2 = room3.RoomTitle ?? "";
								int num = text2.Trim().IndexOf(" ");
								Code.Item.SetMailMergeValue((num > 0) ? text2.Substring(0, num) : text2);
							}
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_RoomDescriptionLastWord:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag25 = ((appointment != null) ? appointment.Room : null) != null;
						if (flag25)
						{
							AppointmentRoom room4 = appointment.Room;
							bool flag26 = room4 != null;
							if (flag26)
							{
								string text3 = room4.RoomTitle ?? "";
								int num2 = text3.Trim().LastIndexOf(" ");
								Code.Item.SetMailMergeValue((num2 > 0) ? text3.Substring(num2 + 1) : text3);
							}
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_Location:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag27 = appointment != null;
						if (flag27)
						{
							Code.Item.SetMailMergeValue(appointment.Location ?? "");
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_RoomAndLocation:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag28 = appointment != null;
						if (flag28)
						{
							string location = appointment.Location;
							string text4 = ((location != null) ? location.Trim() : null) ?? "";
							AppointmentRoom room5 = appointment.Room;
							string text5 = (room5 == null) ? "" : (room5.RoomTitle ?? "");
							bool flag29 = string.IsNullOrEmpty(text4);
							if (flag29)
							{
								Code.Item.SetMailMergeValue(text5);
							}
							else
							{
								bool flag30 = string.IsNullOrEmpty(text5);
								if (flag30)
								{
									Code.Item.SetMailMergeValue(text4);
								}
								else
								{
									Code.Item.SetMailMergeValue(text5 + " " + text4);
								}
							}
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_Cancelled:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag31 = appointment != null;
						if (flag31)
						{
							Code.Item.SetMailMergeValue(appointment.IsCancelled);
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AttendeesEmailsStudents:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag32 = appointment != null;
						if (flag32)
						{
							List<string> list = new List<string>();
							foreach (Attendee attendee in appointment.GetAttendees())
							{
								StudentCommonInfo studentCommonInfo = this.studentCommonInfoManager.LoadStudentCommonInfo(attendee.Person.PersonId);
								bool flag33 = !string.IsNullOrEmpty((studentCommonInfo != null) ? studentCommonInfo.Email : null) && !list.Contains(studentCommonInfo.Email);
								if (flag33)
								{
									list.Add(studentCommonInfo.Email);
								}
							}
							Code.Item.SetMailMergeValue(list);
							Code.Item.ValueFormat = new MailMergeValueFormat
							{
								ValueFormatType = eValueFormatType.CommaSeparatedList
							};
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AttendeesEmailsAll:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag34 = appointment != null;
						if (flag34)
						{
							List<string> list2 = new List<string>();
							IList<Attendee> filteredAttendeesList = this.GetFilteredAttendeesList(appointment, Code, Context, tempCache);
							foreach (Attendee attendee2 in filteredAttendeesList)
							{
								StudentCommonInfo studentCommonInfo2 = this.studentCommonInfoManager.LoadStudentCommonInfo(attendee2.Person.PersonId);
								bool flag35 = !string.IsNullOrEmpty((studentCommonInfo2 != null) ? studentCommonInfo2.Email : null) && !list2.Contains(studentCommonInfo2.Email);
								if (flag35)
								{
									list2.Add(studentCommonInfo2.Email);
								}
								else
								{
									string text6 = this.staffCommonInfoManager.LoadStaffEmail(attendee2.Person.PersonId);
									bool flag36 = !string.IsNullOrEmpty(text6);
									if (flag36)
									{
										list2.Add(text6);
									}
								}
							}
							Code.Item.SetMailMergeValue(list2);
							Code.Item.ValueFormat = new MailMergeValueFormat
							{
								ValueFormatType = eValueFormatType.CommaSeparatedList
							};
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AttendeesEmailsStaff:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag37 = appointment != null;
						if (flag37)
						{
							List<string> mailMergeValue = (from att in appointment.GetAttendees()
							select this.staffCommonInfoManager.LoadStaffEmail(att.Person.PersonId) into staffEmail
							where !string.IsNullOrEmpty(staffEmail)
							select staffEmail).ToList<string>();
							Code.Item.SetMailMergeValue(mailMergeValue);
							Code.Item.ValueFormat = new MailMergeValueFormat
							{
								ValueFormatType = eValueFormatType.CommaSeparatedList
							};
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_Attendees:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag38 = appointment != null;
						if (flag38)
						{
							IList<Attendee> filteredAttendeesList2 = this.GetFilteredAttendeesList(appointment, Code, Context, tempCache);
							eValueFormatType valueFormatType = eValueFormatType.CommaSeparatedList;
							Code.Item.SetMailMergeValue((from f in filteredAttendeesList2
							select f.Person.GetName()).ToList<string>());
							Code.Item.ValueFormat = new MailMergeValueFormat
							{
								ValueFormatType = valueFormatType
							};
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AttendeesCount:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag39 = appointment != null;
						if (flag39)
						{
							IList<Attendee> filteredAttendeesList3 = this.GetFilteredAttendeesList(appointment, Code, Context, tempCache);
							Code.Item.SetMailMergeValue(filteredAttendeesList3.Count((Attendee a) => a.Person.CoreGroup != eCoreGroup.Rooms));
							Code.Item.ValueFormat = new MailMergeValueFormat
							{
								ValueFormatType = eValueFormatType.DefaultToStringFormat
							};
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AttendeesNonStudents:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag40 = appointment != null;
						if (flag40)
						{
							Code.Item.SetMailMergeValue(appointment.GetNonStudentAttendees().ConvertAll<string>((Attendee f) => f.Person.GetName()));
							Code.Item.ValueFormat = new MailMergeValueFormat
							{
								ValueFormatType = eValueFormatType.CommaSeparatedList
							};
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AttendeesStudents:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag41 = appointment != null;
						if (flag41)
						{
							Code.Item.SetMailMergeValue(appointment.GetStudentAttendees().ConvertAll<string>((Attendee f) => f.Person.GetStudentName()));
							Code.Item.ValueFormat = new MailMergeValueFormat
							{
								ValueFormatType = eValueFormatType.CommaSeparatedList
							};
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AttendeesMarkNoShows:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag42 = appointment != null;
						if (flag42)
						{
							eValueFormatType valueFormatType2 = eValueFormatType.CommaSeparatedList;
							Code.Item.SetMailMergeValue(appointment.GetAttendees().ConvertAll<string>((Attendee f) => f.Person.GetName() + (f.IsNoShow ? " [No-show]" : "")));
							Code.Item.ValueFormat = new MailMergeValueFormat
							{
								ValueFormatType = valueFormatType2
							};
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AttendeesOnlyStaff:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag43 = appointment != null;
						if (flag43)
						{
							Code.Item.SetMailMergeValue(appointment.GetStaffAttendees().ConvertAll<string>((Attendee f) => f.Person.GetName()));
							Code.Item.ValueFormat = new MailMergeValueFormat
							{
								ValueFormatType = eValueFormatType.CommaSeparatedList
							};
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_AttendeesNoFacilitatorsStaff:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag44 = appointment != null;
						if (flag44)
						{
							Code.Item.SetMailMergeValue((from g in appointment.GetAttendees()
							where g.MiscCode != 1 && g.Person.CoreGroup != eCoreGroup.Staff
							select g).ToList<Attendee>().ConvertAll<string>((Attendee f) => f.Person.GetName()));
							Code.Item.ValueFormat = new MailMergeValueFormat
							{
								ValueFormatType = eValueFormatType.CommaSeparatedList
							};
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_Workshop:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag45 = appointment != null && appointment.WorkshopInfo != null && appointment.WorkshopInfo.WorkshopId > 0;
						if (flag45)
						{
							Code.Item.SetMailMergeValue(appointment.WorkshopInfo.WorkshopTitle ?? "");
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_DateBooked:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag46 = appointment != null;
						if (flag46)
						{
							Code.Item.SetMailMergeValue(appointment.DateBooked);
							Code.Item.ValueFormat = new MailMergeValueFormat
							{
								ValueFormatType = eValueFormatType.CustomFormat,
								CustomFormat = "yyyy-MM-dd"
							};
						}
						result = true;
						break;
					}
					case eMailMergeCode.APPOINTMENTS_WhoBooked:
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag47 = appointment != null && appointment.WhoBooked != null;
						if (flag47)
						{
							Code.Item.SetMailMergeValue(appointment.WhoBooked.GetName());
						}
						result = true;
						break;
					}
					default:
						result = false;
						break;
					}
				}
				else
				{
					Appointment appointment = this.GetAppointment(Context, tempCache);
					bool flag48 = appointment != null;
					if (flag48)
					{
						Code.Item.SetMailMergeValue(appointment.SubTitle ?? "");
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0002EF9C File Offset: 0x0002D19C
		private IList<Attendee> GetFilteredAttendeesList(Appointment app, MailMergingManager.MailMergeCodeWrapper code, MailMergeContext Context, Dictionary<string, object> tempCache)
		{
			MailMergeCode item = code.Item;
			bool? flag;
			if (item == null)
			{
				flag = null;
			}
			else
			{
				Dictionary<string, string> args = item.Args;
				flag = ((args != null) ? new bool?(args.ContainsKey("gid")) : null);
			}
			bool? flag2 = flag;
			string text = flag2.GetValueOrDefault() ? code.Item.Args["gid"] : string.Empty;
			int overrideGid;
			bool flag3 = text.Length < 1 || !int.TryParse(text, out overrideGid);
			if (flag3)
			{
				overrideGid = 0;
			}
			Func<TechnoPro.Common.Public.Entities.People.Group, bool> <>9__1;
			return (overrideGid < 1) ? app.GetAttendees() : app.GetAttendees().Where(delegate(Attendee g)
			{
				bool result;
				if (g.Person.CoreGroup != (eCoreGroup)overrideGid)
				{
					if (g.Person.Groups != null)
					{
						IEnumerable<TechnoPro.Common.Public.Entities.People.Group> groups = g.Person.Groups;
						Func<TechnoPro.Common.Public.Entities.People.Group, bool> predicate;
						if ((predicate = <>9__1) == null)
						{
							predicate = (<>9__1 = ((TechnoPro.Common.Public.Entities.People.Group h) => h.GroupId == overrideGid));
						}
						result = groups.Any(predicate);
					}
					else
					{
						result = false;
					}
				}
				else
				{
					result = true;
				}
				return result;
			}).ToList<Attendee>();
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0002F068 File Offset: 0x0002D268
		private bool TryToMailMergeTest(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper Code)
		{
			eMailMergeCode? eMailMergeCode = Code.Name.FindMailMergeCode();
			bool flag = eMailMergeCode == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				switch (eMailMergeCode.Value)
				{
				case eMailMergeCode.TESTS_ScheduledEndTimeWithoutBreaks:
				{
					Test test = this.GetTest(Context, tempCache);
					bool flag2 = test != null;
					if (flag2)
					{
						MailMergeCode item = Code.Item;
						DateTime dateTime = test.EndDateTime;
						dateTime = dateTime.AddMinutes((double)(-(double)test.BreakTimeMinutes));
						item.SetMailMergeValue(dateTime.ToString(Context.DefaultTimeFormat ?? "h:mm tt"));
					}
					result = true;
					break;
				}
				case eMailMergeCode.TESTS_ScheduledDurationWithoutBreaks:
				{
					Test test = this.GetTest(Context, tempCache);
					bool flag3 = test != null;
					if (flag3)
					{
						int num = Convert.ToInt32((test.EndDateTime - test.StartDateTime).TotalMinutes);
						Code.Item.SetMailMergeValue(this.GetDurationDescription(num - test.BreakTimeMinutes));
					}
					result = true;
					break;
				}
				case eMailMergeCode.TESTS_BreakDuration:
				{
					Test test = this.GetTest(Context, tempCache);
					bool flag4 = test != null && test.BreakTimeMinutes > 0;
					if (flag4)
					{
						Code.Item.SetMailMergeValue(this.GetDurationDescription(test.BreakTimeMinutes));
					}
					result = true;
					break;
				}
				case eMailMergeCode.TESTS_ActualDate:
				{
					Test test = this.GetTest(Context, tempCache);
					bool flag5 = test != null && test.ActualStartDateTime != null;
					if (flag5)
					{
						Code.Item.SetMailMergeValue(test.ActualStartDateTime.Value);
					}
					result = true;
					break;
				}
				case eMailMergeCode.TESTS_ActualStartTime:
				{
					Test test = this.GetTest(Context, tempCache);
					bool flag6 = test != null && test.ActualStartDateTime != null;
					if (flag6)
					{
						Code.Item.SetMailMergeValue(test.ActualStartDateTime.Value);
						Code.Item.ValueFormat = new MailMergeValueFormat
						{
							ValueFormatType = eValueFormatType.CustomFormat,
							CustomFormat = (Context.DefaultTimeFormat ?? "h:mm tt")
						};
					}
					result = true;
					break;
				}
				case eMailMergeCode.TESTS_ActualEndTime:
				{
					Test test = this.GetTest(Context, tempCache);
					bool flag7 = test != null && test.ActualEndDateTime != null;
					if (flag7)
					{
						Code.Item.SetMailMergeValue(test.ActualEndDateTime.Value);
						Code.Item.ValueFormat = new MailMergeValueFormat
						{
							ValueFormatType = eValueFormatType.CustomFormat,
							CustomFormat = (Context.DefaultTimeFormat ?? "h:mm tt")
						};
					}
					result = true;
					break;
				}
				case eMailMergeCode.TESTS_ActualDuration:
				{
					Test test = this.GetTest(Context, tempCache);
					bool flag8 = test != null && test.ActualEndDateTime != null && test.ActualStartDateTime != null;
					if (flag8)
					{
						int durationMinutes = Convert.ToInt32((test.ActualEndDateTime.Value - test.ActualStartDateTime.Value).TotalMinutes);
						Code.Item.SetMailMergeValue(this.GetDurationDescription(durationMinutes));
					}
					result = true;
					break;
				}
				case eMailMergeCode.TESTS_ActualDurationMinutes:
				{
					Test test = this.GetTest(Context, tempCache);
					bool flag9 = test != null && test.ActualEndDateTime != null && test.ActualStartDateTime != null;
					if (flag9)
					{
						TimeSpan timeSpan = test.ActualEndDateTime.Value - test.ActualStartDateTime.Value;
						Code.Item.SetMailMergeValue(Convert.ToInt32(timeSpan.TotalMinutes));
					}
					result = true;
					break;
				}
				case eMailMergeCode.TESTS_ExamAccommodations:
				{
					IList<AccommodationData> examAccommodations = this.GetExamAccommodations(Context, tempCache, Code);
					bool flag10 = examAccommodations != null;
					if (flag10)
					{
						Code.Item.SetMailMergeValue(examAccommodations);
						Code.Item.ValueFormat = new MailMergeValueFormat
						{
							ValueFormatType = eValueFormatType.BulletedList
						};
					}
					result = true;
					break;
				}
				case eMailMergeCode.TESTS_BookingNotes:
				{
					bool flag11 = Context.AppointmentId > 0;
					if (flag11)
					{
						StudentClassTest studentClassTest = this.studentClassTestInfoManager.LoadClassTestByAppointmentId(Context.AppointmentId);
						bool flag12 = studentClassTest != null;
						if (flag12)
						{
							string mailMergeValue = studentClassTest.BookingNote ?? "";
							Code.Item.SetMailMergeValue(mailMergeValue);
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.TESTS_PrivateNotes:
				{
					bool flag13 = Context.AppointmentId > 0;
					if (flag13)
					{
						StudentClassTest studentClassTest2 = this.studentClassTestInfoManager.LoadClassTestByAppointmentId(Context.AppointmentId);
						bool flag14 = studentClassTest2 != null;
						if (flag14)
						{
							Code.Item.SetMailMergeValue(studentClassTest2.PrivateNote ?? "");
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.TESTS_ExamAccommodationsShort:
				{
					IList<AccommodationData> list = this.GetExamAccommodations(Context, tempCache, Code);
					bool flag15 = list != null && list.Count > 0;
					if (flag15)
					{
						list = list.ToArray<AccommodationData>().ToList<AccommodationData>();
						IDictionary<int, ExtendedAccommodationInfo> dictionary = this.dynamicFieldManager.LoadAccommodationShortCodes((from g in list.ToList<AccommodationData>()
						select g.Data.Field.ControlId).ToArray<int>());
						foreach (AccommodationData accommodationData in list)
						{
							int controlId = accommodationData.Data.Field.ControlId;
							bool flag16 = !dictionary.ContainsKey(controlId);
							if (!flag16)
							{
								ExtendedAccommodationInfo extendedAccommodationInfo = dictionary[controlId];
								bool flag17 = !string.IsNullOrEmpty(extendedAccommodationInfo.ShortCode);
								if (flag17)
								{
									accommodationData.Data.Field.ControlCaption = extendedAccommodationInfo.ShortCode;
								}
							}
						}
						Code.Item.SetMailMergeValue(list);
						Code.Item.ValueFormat = new MailMergeValueFormat
						{
							ValueFormatType = eValueFormatType.BulletedList
						};
					}
					result = true;
					break;
				}
				case eMailMergeCode.TESTS_ExamStatus:
				{
					bool flag18 = Context.AppointmentId > 0;
					if (flag18)
					{
						ExamStatus examStatus = this.studentClassTestInfoManager.LoadExamStatusByAppointmentId(Context.AppointmentId);
						bool flag19 = examStatus != null;
						if (flag19)
						{
							Code.Item.SetMailMergeValue(examStatus.Title ?? "");
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.TESTS_ProctorName:
				{
					Test test = this.GetTest(Context, tempCache);
					bool flag20 = test != null;
					if (flag20)
					{
						List<int> list2 = test.GetNonStudentAttendees().ToList<Attendee>().ConvertAll<int>((Attendee g) => g.Person.PersonId);
						bool flag21 = list2.Count > 0;
						if (flag21)
						{
							IList<PersonBase> list3 = this.LoadProctors(list2);
							bool flag22 = list3.Count > 0;
							if (flag22)
							{
								Code.Item.SetMailMergeValue(list3[0].GetName());
							}
						}
					}
					result = true;
					break;
				}
				default:
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0002F720 File Offset: 0x0002D920
		private IList<PersonBase> LoadProctors(IList<int> personIds)
		{
			IPeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
			IList<PersonBase> list = peopleGroupManager.LoadusersByGroupTitleAndPersonIdList(personIds, "Invigilators", "Proctors");
			bool flag = list.Count < 1;
			if (flag)
			{
				list = peopleGroupManager.LoadusersByGroupTitleAndPersonIdList(personIds, "Invigilator", "Proctor");
			}
			return list;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0002F770 File Offset: 0x0002D970
		private IStudentClassTestInfoManager studentClassTestInfoManager
		{
			get
			{
				bool flag = this._studentClassTestInfoManager == null;
				if (flag)
				{
					this._studentClassTestInfoManager = new StudentClassTestInfoManager(this.OpContext);
				}
				return this._studentClassTestInfoManager;
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0002F7A8 File Offset: 0x0002D9A8
		private bool TryToMailMergeExamInfo(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper Code)
		{
			eMailMergeCode? eMailMergeCode = Code.Name.FindMailMergeCode();
			bool flag = eMailMergeCode == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				switch (eMailMergeCode.Value)
				{
				case eMailMergeCode.EXAM_ClassDateTime:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag2 = classTest != null;
					if (flag2)
					{
						MailMergeCode item = Code.Item;
						string format = "{0}  {1} to {2}";
						DateTime dateTime = classTest.StartDateTime;
						object arg = dateTime.ToString(Context.DefaultDateFormat ?? "MMMM d, yyyy");
						dateTime = classTest.StartDateTime;
						object arg2 = dateTime.ToString(Context.DefaultTimeFormat ?? "h:mm tt");
						dateTime = classTest.EndDateTime;
						item.SetMailMergeValue(string.Format(format, arg, arg2, dateTime.ToString(Context.DefaultTimeFormat ?? "h:mm tt")));
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_ClassStartDateTime:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag3 = classTest != null;
					if (flag3)
					{
						MailMergeCode item2 = Code.Item;
						string format2 = "{0}  {1}";
						DateTime dateTime = classTest.StartDateTime;
						object arg3 = dateTime.ToString(Context.DefaultDateFormat ?? "MMMM d, yyyy");
						dateTime = classTest.StartDateTime;
						item2.SetMailMergeValue(string.Format(format2, arg3, dateTime.ToString(Context.DefaultTimeFormat ?? "h:mm tt")));
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_ClassEndDateTime:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag4 = classTest != null;
					if (flag4)
					{
						MailMergeCode item3 = Code.Item;
						string format3 = "{0}  {1}";
						DateTime dateTime = classTest.StartDateTime;
						object arg4 = dateTime.ToString(Context.DefaultDateFormat ?? "MMMM d, yyyy");
						dateTime = classTest.EndDateTime;
						item3.SetMailMergeValue(string.Format(format3, arg4, dateTime.ToString(Context.DefaultTimeFormat ?? "h:mm tt")));
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_ExamId:
					Code.Item.SetMailMergeValue(Context.ExamId);
					result = true;
					break;
				case eMailMergeCode.EXAM_ClassDate:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag5 = classTest != null;
					if (flag5)
					{
						Code.Item.SetMailMergeValue(classTest.StartDateTime);
						Code.Item.ValueFormat = new MailMergeValueFormat
						{
							ValueFormatType = eValueFormatType.CustomFormat,
							CustomFormat = "dddd MMMM d, yyyy"
						};
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_ClassDate2:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag6 = classTest != null;
					if (flag6)
					{
						Code.Item.SetMailMergeValue(classTest.StartDateTime);
						Code.Item.ValueFormat = new MailMergeValueFormat
						{
							ValueFormatType = eValueFormatType.CustomFormat,
							CustomFormat = "MM/dd/yy"
						};
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_ClassDate3:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag7 = classTest != null;
					if (flag7)
					{
						Code.Item.SetMailMergeValue(classTest.StartDateTime);
						Code.Item.ValueFormat = new MailMergeValueFormat
						{
							ValueFormatType = eValueFormatType.CustomFormat,
							CustomFormat = "yyyy-MM-dd"
						};
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_ClassStartTime:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag8 = classTest != null;
					if (flag8)
					{
						Code.Item.SetMailMergeValue(classTest.StartDateTime);
						Code.Item.ValueFormat = new MailMergeValueFormat
						{
							ValueFormatType = eValueFormatType.CustomFormat,
							CustomFormat = (Context.DefaultTimeFormat ?? "h:mm tt")
						};
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_ClassEndTime:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag9 = classTest != null;
					if (flag9)
					{
						Code.Item.SetMailMergeValue(classTest.EndDateTime);
						Code.Item.ValueFormat = new MailMergeValueFormat
						{
							ValueFormatType = eValueFormatType.CustomFormat,
							CustomFormat = (Context.DefaultTimeFormat ?? "h:mm tt")
						};
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_ClassDuration:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag10 = classTest != null;
					if (flag10)
					{
						int durationMinutes = Convert.ToInt32((classTest.EndDateTime - classTest.StartDateTime).TotalMinutes);
						Code.Item.SetMailMergeValue(this.GetDurationDescription(durationMinutes));
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_ClassDurationMinutes:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag11 = classTest != null;
					if (flag11)
					{
						TimeSpan timeSpan = classTest.EndDateTime - classTest.StartDateTime;
						Code.Item.SetMailMergeValue(Convert.ToInt32(timeSpan.TotalMinutes));
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_ClassLocation:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag12 = classTest != null;
					if (flag12)
					{
						Code.Item.SetMailMergeValue(classTest.Location ?? "");
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_ClassTypeCode:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag13 = classTest != null;
					if (flag13)
					{
						Code.Item.SetMailMergeValue((classTest.ExamType == eClassTestType.FinalExam) ? "Final exam" : "Midterm");
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_TestDelivered:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag14 = classTest != null;
					if (flag14)
					{
						Code.Item.SetMailMergeValue(classTest.TestDeliveredMessage ?? "");
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_InstructorAcknowledged:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag15 = classTest != null && classTest.InstructorAcknowledged != null;
					if (flag15)
					{
						Code.Item.SetMailMergeValue((int)classTest.InstructorAcknowledged.Value);
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_InstructorContactedDate:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag16 = classTest != null && classTest.InstructorContactedDate != null;
					if (flag16)
					{
						Code.Item.SetMailMergeValue(classTest.InstructorContactedDate.Value);
						Code.Item.ValueFormat = new MailMergeValueFormat
						{
							ValueFormatType = eValueFormatType.CustomFormat,
							CustomFormat = (Context.DefaultDateFormat ?? "yyyy-MM-dd")
						};
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_InstructorContactedNote:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag17 = classTest != null;
					if (flag17)
					{
						Code.Item.SetMailMergeValue(classTest.InstructorContactedNote ?? "");
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_ClassPrivateNote:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag18 = classTest != null;
					if (flag18)
					{
						Code.Item.SetMailMergeValue(classTest.PrivateNote ?? "");
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_ClassTestPickedUpNote:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag19 = classTest != null;
					if (flag19)
					{
						Code.Item.SetMailMergeValue(classTest.TestPickedUpNote ?? "");
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_ClassTestPickedUpDate:
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag20 = classTest != null && classTest.TestPickedUpDate != null;
					if (flag20)
					{
						Code.Item.SetMailMergeValue(classTest.TestPickedUpDate);
						Code.Item.ValueFormat = new MailMergeValueFormat
						{
							ValueFormatType = eValueFormatType.CustomFormat,
							CustomFormat = (Context.DefaultDateFormat ?? "yyyy-MM-dd")
						};
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_InstructorForm:
				{
					IList<DynamicData> instructorFormData = this.GetInstructorFormData(Context, tempCache);
					bool flag21 = instructorFormData != null;
					if (flag21)
					{
						Code.Item.SetMailMergeValue(instructorFormData.ToList<DynamicData>());
						Code.Item.ValueFormat = new MailMergeValueFormat
						{
							ValueFormatType = eValueFormatType.BulletedList
						};
					}
					result = true;
					break;
				}
				case eMailMergeCode.EXAM_InstructorUrl:
				{
					string text = this.oldUserSettingsManager.GetSettingValue_String(Context.WhoAmId, eSettingCode.SETTING_WebBaseUrl, false);
					bool flag22 = text == null;
					if (flag22)
					{
						text = "";
					}
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag23 = Context.ExamId < 1;
					if (flag23)
					{
						Appointment appointment = this.GetAppointment(Context, tempCache);
						bool flag24 = ((appointment != null) ? appointment.TestExamInfo : null) != null;
						if (flag24)
						{
							Context.ExamId = appointment.TestExamInfo.ExamId;
						}
					}
					bool flag25 = Context.ExamId > 0;
					if (flag25)
					{
						eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
						OperationContext opContext = this.OpContext;
						IEncryption encryption = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption;
						Code.Item.SetMailMergeValue(string.Format("{0}/user/instructor/examupload.aspx?examid={1}", text, ((course == null) ? 0 : course.LuCourseId).EncodeUrlVariable(encryption)));
					}
					else
					{
						Code.Item.SetMailMergeValue(string.Format("{0}/user/instructor/examupload.aspx", text));
					}
					result = true;
					break;
				}
				default:
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0002FFD0 File Offset: 0x0002E1D0
		private string GetDurationDescription(int DurationMinutes)
		{
			int num = (int)(Convert.ToDouble(DurationMinutes) / 60.0);
			int num2 = DurationMinutes - num * 60;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = num == 1;
			if (flag)
			{
				stringBuilder.Append("1 hour");
				bool flag2 = num2 > 0;
				if (flag2)
				{
					stringBuilder.Append("; ");
				}
			}
			else
			{
				bool flag3 = num > 1;
				if (flag3)
				{
					stringBuilder.Append(num);
					stringBuilder.Append(" hours");
					bool flag4 = num2 > 0;
					if (flag4)
					{
						stringBuilder.Append("; ");
					}
				}
			}
			bool flag5 = num2 == 1;
			if (flag5)
			{
				stringBuilder.Append("1 minute");
			}
			else
			{
				bool flag6 = num2 > 1;
				if (flag6)
				{
					stringBuilder.Append(num2);
					stringBuilder.Append(" minutes");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000300A8 File Offset: 0x0002E2A8
		private bool TryToMailMergeServiceProviderInfo(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper Code)
		{
			eMailMergeCode? eMailMergeCode = Code.Name.FindMailMergeCode();
			bool flag = eMailMergeCode == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				int serviceProviderId = Context.ServiceProviderId;
				bool flag2 = serviceProviderId < 1;
				if (flag2)
				{
					result = true;
				}
				else
				{
					ServiceProvider serviceProvider = this.GetServiceProvider(Context, tempCache, Code);
					bool flag3 = serviceProvider == null;
					if (flag3)
					{
						result = true;
					}
					else
					{
						switch (eMailMergeCode.Value)
						{
						case eMailMergeCode.SERVICEPROVIDERS_ServiceProviderName:
							Code.Item.SetMailMergeValue(string.Join(" ", (from g in new string[]
							{
								serviceProvider.FirstName ?? "",
								serviceProvider.LastName ?? ""
							}
							select g.Trim() into h
							where h.Length > 0
							select h).ToArray<string>()));
							result = true;
							break;
						case eMailMergeCode.SERVICEPROVIDERS_ServiceProviderFirstName:
							Code.Item.SetMailMergeValue(serviceProvider.FirstName ?? "");
							result = true;
							break;
						case eMailMergeCode.SERVICEPROVIDERS_ServiceProviderLastName:
							Code.Item.SetMailMergeValue(serviceProvider.LastName ?? "");
							result = true;
							break;
						case eMailMergeCode.SERVICEPROVIDERS_ServiceProviderMiddleName:
							Code.Item.SetMailMergeValue(serviceProvider.MiddleName ?? "");
							result = true;
							break;
						case eMailMergeCode.SERVICEPROVIDERS_ServiceProviderStudentNumber:
							Code.Item.SetMailMergeValue(serviceProvider.StudentNumber ?? "");
							result = true;
							break;
						case eMailMergeCode.SERVICEPROVIDERS_ServiceProviderUsername:
							Code.Item.SetMailMergeValue(serviceProvider.Username ?? "");
							result = true;
							break;
						case eMailMergeCode.SERVICEPROVIDERS_ServiceProviderEmail:
							Code.Item.SetMailMergeValue(serviceProvider.Email ?? "");
							result = true;
							break;
						case eMailMergeCode.SERVICEPROVIDERS_ServiceProviderPhone1:
							Code.Item.SetMailMergeValue(serviceProvider.Phone1 ?? "");
							result = true;
							break;
						case eMailMergeCode.SERVICEPROVIDERS_ServiceProviderPhone2:
							Code.Item.SetMailMergeValue(serviceProvider.Phone2 ?? "");
							result = true;
							break;
						case eMailMergeCode.SERVICEPROVIDERS_ServiceProviderEmail2:
							Code.Item.SetMailMergeValue(serviceProvider.Email2 ?? "");
							result = true;
							break;
						case eMailMergeCode.SERVICEPROVIDERS_ServiceProviderSpecialization:
							Code.Item.SetMailMergeValue(serviceProvider.Specialization ?? "");
							result = true;
							break;
						case eMailMergeCode.SERVICEPROVIDERS_ServiceProviderAddress:
							Code.Item.SetMailMergeValue(serviceProvider.Address ?? "");
							result = true;
							break;
						case eMailMergeCode.SERVICEPROVIDERS_ServiceProviderAddress2:
							Code.Item.SetMailMergeValue(serviceProvider.Address2 ?? "");
							result = true;
							break;
						case eMailMergeCode.SERVICEPROVIDERS_ServiceProviderNotes:
							Code.Item.SetMailMergeValue(serviceProvider.Notes1 ?? "");
							result = true;
							break;
						case eMailMergeCode.SERVICEPROVIDERS_ServiceProviderAdditionalNote:
							Code.Item.SetMailMergeValue(serviceProvider.Notes2 ?? "");
							result = true;
							break;
						case eMailMergeCode.SERVICEPROVIDERS_ServiceProviderRegistrationComplete:
							Code.Item.SetMailMergeValue(serviceProvider.RegistrationIsComplete);
							result = true;
							break;
						case eMailMergeCode.SERVICEPROVIDERS_ServiceProviderActiveAddress:
							Code.Item.SetMailMergeValue(serviceProvider.AddressActive ? (serviceProvider.Address ?? "") : (serviceProvider.Address2 ?? ""));
							result = true;
							break;
						default:
							result = false;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00030440 File Offset: 0x0002E640
		private bool TryToMailMergeCourseInfo(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper Code)
		{
			eMailMergeCode? eMailMergeCode = Code.Name.FindMailMergeCode();
			bool flag = eMailMergeCode == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				switch (eMailMergeCode.Value)
				{
				case eMailMergeCode.COURSE_CourseDescription:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag2 = course != null;
					if (flag2)
					{
						Code.Item.SetMailMergeValue(this.GetCourseDescription(course));
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_Term:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag3 = course != null;
					if (flag3)
					{
						Code.Item.SetMailMergeValue(course.Term ?? "");
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_Duration:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag4 = course != null;
					if (flag4)
					{
						Code.Item.SetMailMergeValue(course.Duration ?? "");
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_Subject:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag5 = course != null;
					if (flag5)
					{
						Code.Item.SetMailMergeValue((course.Subject == null || course.Subject.SubjectDescription == null) ? "" : course.Subject.SubjectDescription);
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_SubjectEmail:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag6 = course != null;
					if (flag6)
					{
						Code.Item.SetMailMergeValue((course.Subject == null || course.Subject.SubjectEmail == null) ? "" : course.Subject.SubjectEmail);
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_AlternateContactName:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag7 = course != null && course.AlternateContacts != null && course.AlternateContacts.Count > 0;
					if (flag7)
					{
						Code.Item.SetMailMergeValue(course.AlternateContacts[0].Name ?? "");
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_AlternateContactEmail:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag8 = course != null && course.AlternateContacts != null && course.AlternateContacts.Count > 0;
					if (flag8)
					{
						Code.Item.SetMailMergeValue(course.AlternateContacts[0].Email ?? "");
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_AlternateContactPhone:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag9 = course != null && course.AlternateContacts != null && course.AlternateContacts.Count > 0;
					if (flag9)
					{
						Code.Item.SetMailMergeValue(course.AlternateContacts[0].Phone ?? "");
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_AlternateContactNames:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag10 = course != null;
					if (flag10)
					{
						MailMergeCode item = Code.Item;
						string mailMergeValue;
						if (course.AlternateContacts != null)
						{
							mailMergeValue = string.Join(", ", course.AlternateContacts.ConvertAll<string>((AlternateContact f) => f.Name ?? "").ToArray());
						}
						else
						{
							mailMergeValue = "";
						}
						item.SetMailMergeValue(mailMergeValue);
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_AlternateContacts:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag11 = course != null;
					if (flag11)
					{
						MailMergeCode item2 = Code.Item;
						string mailMergeValue2;
						if (course.AlternateContacts != null)
						{
							mailMergeValue2 = string.Join(", ", course.AlternateContacts.ConvertAll<string>((AlternateContact f) => string.Format("{0}; email: {1}; phone: {2}", f.Name ?? "", f.Email ?? "", f.Phone ?? "")).ToArray());
						}
						else
						{
							mailMergeValue2 = "";
						}
						item2.SetMailMergeValue(mailMergeValue2);
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_AlternateContactEmails:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag12 = course != null;
					if (flag12)
					{
						MailMergeCode item3 = Code.Item;
						string mailMergeValue3;
						if (course.AlternateContacts != null)
						{
							mailMergeValue3 = string.Join(", ", course.AlternateContacts.ConvertAll<string>((AlternateContact f) => f.Email ?? "").ToArray());
						}
						else
						{
							mailMergeValue3 = "";
						}
						item3.SetMailMergeValue(mailMergeValue3);
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_Instructor:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag13 = course != null;
					if (flag13)
					{
						LookupInstructor primaryInstructor = course.GetPrimaryInstructor();
						bool flag14 = primaryInstructor != null;
						if (flag14)
						{
							Code.Item.SetMailMergeValue(primaryInstructor.Name ?? "");
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_InstructorFirstName:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag15 = course != null;
					if (flag15)
					{
						LookupInstructor primaryInstructor2 = course.GetPrimaryInstructor();
						bool flag16 = primaryInstructor2 != null;
						if (flag16)
						{
							string text = primaryInstructor2.Name ?? "";
							bool flag17 = text.Length > 0;
							if (flag17)
							{
								int num = text.IndexOf(",");
								bool flag18 = num > 0;
								if (flag18)
								{
									Code.Item.SetMailMergeValue(text.Substring(num + 1).Trim());
								}
								else
								{
									num = text.IndexOf(" ");
									Code.Item.SetMailMergeValue((num > 0) ? text.Substring(0, num).Trim() : text);
								}
							}
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_InstructorLastName:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag19 = course != null;
					if (flag19)
					{
						LookupInstructor primaryInstructor3 = course.GetPrimaryInstructor();
						bool flag20 = primaryInstructor3 != null;
						if (flag20)
						{
							string text2 = primaryInstructor3.Name ?? "";
							bool flag21 = text2.Length > 0;
							if (flag21)
							{
								int num2 = text2.IndexOf(",");
								bool flag22 = num2 > 0;
								if (flag22)
								{
									Code.Item.SetMailMergeValue(text2.Substring(0, num2).Trim());
								}
								else
								{
									num2 = text2.IndexOf(" ");
									bool flag23 = num2 > 0;
									if (flag23)
									{
										Code.Item.SetMailMergeValue(text2.Substring(num2 + 1).Trim());
									}
									else
									{
										Code.Item.SetMailMergeValue(text2);
									}
								}
							}
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_InstructorEmail:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag24 = course != null;
					if (flag24)
					{
						LookupInstructor primaryInstructor4 = course.GetPrimaryInstructor();
						bool flag25 = primaryInstructor4 != null;
						if (flag25)
						{
							Code.Item.SetMailMergeValue(primaryInstructor4.Email ?? "");
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_InstructorPhone:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag26 = course != null;
					if (flag26)
					{
						LookupInstructor primaryInstructor5 = course.GetPrimaryInstructor();
						bool flag27 = primaryInstructor5 != null;
						if (flag27)
						{
							Code.Item.SetMailMergeValue(primaryInstructor5.Phone ?? "");
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_PrimaryInstructorEmails:
				{
					IList<LookupCourse> courses = this.GetCourses(Context, tempCache, Code);
					bool flag28 = courses != null && courses.Count > 0;
					if (flag28)
					{
						List<string> list = new List<string>();
						foreach (LookupCourse course2 in courses)
						{
							LookupInstructor primaryInstructor6 = course2.GetPrimaryInstructor();
							bool flag29 = primaryInstructor6 != null && !string.IsNullOrEmpty(primaryInstructor6.Email);
							if (flag29)
							{
								list.Add(primaryInstructor6.Email);
							}
						}
						Code.Item.SetMailMergeValue(string.Join(",", list.ToArray()));
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_PrimaryAndSecondaryInstructorEmails:
				{
					IList<LookupCourse> courses = this.GetCourses(Context, tempCache, Code);
					bool flag30 = courses != null && courses.Count > 0;
					if (flag30)
					{
						List<string> list2 = new List<string>();
						foreach (LookupCourse lookupCourse in courses)
						{
							List<string> list3 = (from g in lookupCourse.Instructors
							select (g.Email ?? "").Trim() into h
							where h.Length > 0
							select h).ToList<string>();
							bool flag31 = list3.Count > 0;
							if (flag31)
							{
								using (List<string>.Enumerator enumerator3 = list3.GetEnumerator())
								{
									while (enumerator3.MoveNext())
									{
										string p = enumerator3.Current;
										bool flag32 = !list2.Any((string g) => g.Equals(p, StringComparison.OrdinalIgnoreCase));
										if (flag32)
										{
											list2.Add(p);
										}
									}
								}
							}
						}
						Code.Item.SetMailMergeValue(string.Join(",", list2.ToArray()));
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_PrimaryInstructorNames:
				{
					IList<LookupCourse> courses = this.GetCourses(Context, tempCache, Code);
					bool flag33 = courses != null && courses.Count > 0;
					if (flag33)
					{
						List<string> list4 = new List<string>();
						foreach (LookupCourse course3 in courses)
						{
							LookupInstructor primaryInstructor7 = course3.GetPrimaryInstructor();
							bool flag34 = primaryInstructor7 != null && !string.IsNullOrEmpty(primaryInstructor7.Email);
							if (flag34)
							{
								list4.Add(primaryInstructor7.Name ?? "");
							}
						}
						Code.Item.SetMailMergeValue(string.Join(",", list4.ToArray()));
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_InstructorEmails:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag35 = course != null;
					if (flag35)
					{
						Code.Item.SetMailMergeValue(string.Join(", ", (from h in course.Instructors
						where !string.IsNullOrEmpty(h.Email)
						select h).ToList<LookupInstructor>().ConvertAll<string>((LookupInstructor g) => g.Email ?? "").ToArray()));
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_InstructorNames:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag36 = course != null;
					if (flag36)
					{
						Code.Item.SetMailMergeValue(string.Join(", ", (from h in course.Instructors
						where !string.IsNullOrEmpty(h.Name)
						select h).ToList<LookupInstructor>().ConvertAll<string>((LookupInstructor g) => g.Name ?? "").ToArray()));
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_InstructorNamesWithEmails:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag37 = course != null;
					if (flag37)
					{
						Code.Item.SetMailMergeValue(string.Join(", ", (from h in course.Instructors
						where !string.IsNullOrEmpty(h.Email) || !string.IsNullOrEmpty(h.Name)
						select h).ToList<LookupInstructor>().ConvertAll<string>((LookupInstructor g) => string.Format("{0}{1}{2}", g.Name ?? "", string.IsNullOrEmpty(g.Email) ? "" : "; email: ", g.Email ?? "")).ToArray()));
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_CourseCode:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag38 = course != null;
					if (flag38)
					{
						Code.Item.SetMailMergeValue(course.Course ?? "");
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_Section:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag39 = course != null;
					if (flag39)
					{
						Code.Item.SetMailMergeValue(course.Section ?? "");
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_TimeOfDay:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag40 = course != null;
					if (flag40)
					{
						Code.Item.SetMailMergeValue(course.TimeOfDay ?? "");
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_Courses:
				{
					IList<LookupCourse> courses = this.GetCourses(Context, tempCache, Code);
					bool flag41 = courses != null && courses.Count > 0;
					if (flag41)
					{
						bool flag42 = Code.Item.Args.ContainsKey("row");
						if (flag42)
						{
							string str = Code.Item.Args["row"];
							StringBuilder stringBuilder = new StringBuilder();
							Type typeFromHandle = typeof(LookupCourse);
							PropertyInfo[] properties = typeFromHandle.GetProperties();
							foreach (LookupCourse lookupCourse2 in courses)
							{
								Dictionary<string, string> dictionary = new Dictionary<string, string>();
								dictionary.Add("coursedescription", lookupCourse2.GetCourseDescription());
								LookupInstructor primaryInstructor8 = lookupCourse2.GetPrimaryInstructor();
								dictionary.Add("instructorname", (primaryInstructor8 == null) ? "" : (primaryInstructor8.Name ?? ""));
								dictionary.Add("instructoremail", (primaryInstructor8 == null) ? "" : (primaryInstructor8.Email ?? ""));
								foreach (PropertyInfo propertyInfo in properties)
								{
									bool flag43 = propertyInfo.PropertyType == typeof(string);
									if (flag43)
									{
										string key = propertyInfo.Name.ToLower();
										bool flag44 = !dictionary.ContainsKey(key);
										if (flag44)
										{
											dictionary.Add(key, ((string)propertyInfo.GetValue(lookupCourse2, null)) ?? "");
										}
									}
								}
								string text3 = string.Copy(str);
								foreach (KeyValuePair<string, string> keyValuePair in dictionary)
								{
									text3 = text3.Replace(keyValuePair.Key, keyValuePair.Value ?? "");
								}
								stringBuilder.Append(text3.Replace("\\n", Environment.NewLine));
							}
							Code.Item.SetMailMergeValue(stringBuilder.ToString());
						}
						else
						{
							Code.Item.SetMailMergeValue(string.Join(", ", courses.ToList<LookupCourse>().ConvertAll<string>((LookupCourse f) => this.GetCourseDescription(f)).ToArray()));
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_StartYear:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag45 = course != null;
					if (flag45)
					{
						Code.Item.SetMailMergeValue(course.StartDate.Year.ToString());
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_StartMonth:
				case eMailMergeCode.COURSE_CourseStartDate:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag46 = course != null;
					if (flag46)
					{
						Code.Item.SetMailMergeValue(course.StartDate);
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_CourseEndDate:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag47 = course != null;
					if (flag47)
					{
						Code.Item.SetMailMergeValue(course.EndDate);
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_Campus:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag48 = course != null;
					if (flag48)
					{
						Code.Item.SetMailMergeValue(course.Campus ?? "");
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_Department:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag49 = course != null;
					if (flag49)
					{
						Code.Item.SetMailMergeValue(course.Department ?? "");
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_LuCourseId:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag50 = course != null;
					if (flag50)
					{
						Code.Item.SetMailMergeValue(course.LuCourseId);
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_Session:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					bool flag51 = course != null;
					if (flag51)
					{
						Session session = this.sessionManager.GetSession(course.StartDate.AddDays(1.0));
						Code.Item.SetMailMergeValue(session.AcademicTerm.Title);
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_CoursesSignatures:
				{
					IList<LookupCourse> courses2 = this.GetCourses(Context, tempCache, Code);
					bool flag52 = courses2 != null;
					if (flag52)
					{
						Code.Item.SetMailMergeValue(this.GetCoursesSignatures(courses2));
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_CoursesSignaturesStudents:
				{
					IList<LookupCourse> courses3 = this.GetCourses(Context, tempCache, Code);
					bool flag53 = courses3 != null;
					if (flag53)
					{
						Code.Item.SetMailMergeValue(this.GetCoursesSignaturesStudents(courses3));
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_Timetable:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					IList<string> timetableItemsDisplayStrings = MailMergingManager.GetTimetableItemsDisplayStrings((course != null) ? course.TimetableItems : null, false);
					bool flag54 = timetableItemsDisplayStrings != null;
					if (flag54)
					{
						Code.Item.SetMailMergeValue(string.Join(", ", timetableItemsDisplayStrings.ToArray<string>()));
					}
					result = true;
					break;
				}
				case eMailMergeCode.COURSE_TimetableWithLocation:
				{
					LookupCourse course = this.GetCourse(Context, tempCache, Code);
					IList<string> timetableItemsDisplayStrings2 = MailMergingManager.GetTimetableItemsDisplayStrings((course != null) ? course.TimetableItems : null, true);
					bool flag55 = timetableItemsDisplayStrings2 != null;
					if (flag55)
					{
						Code.Item.SetMailMergeValue(string.Join(", ", timetableItemsDisplayStrings2.ToArray<string>()));
					}
					result = true;
					break;
				}
				default:
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000315D4 File Offset: 0x0002F7D4
		private static IList<string> GetTimetableItemsDisplayStrings(IList<LookupTimetableItem> timetableItems, bool includeLocation)
		{
			bool flag = timetableItems == null;
			IList<string> result;
			if (flag)
			{
				result = new List<string>();
			}
			else
			{
				DateTimeFormatInfo currentInfo = DateTimeFormatInfo.CurrentInfo;
				string[] array;
				if ((array = ((currentInfo != null) ? currentInfo.ShortestDayNames : null)) == null)
				{
					string[] array2 = new string[7];
					array2[0] = "Su";
					array2[1] = "Mo";
					array2[2] = "Tu";
					array2[3] = "We";
					array2[4] = "Th";
					array2[5] = "Fr";
					array = array2;
					array2[6] = "Sa";
				}
				string[] array3 = array;
				List<string> list = new List<string>();
				foreach (LookupTimetableItem lookupTimetableItem in timetableItems)
				{
					StringBuilder stringBuilder = new StringBuilder();
					DateTime dateTime = DateTime.Now.Date.Add(lookupTimetableItem.StartTime);
					DateTime dateTime2 = DateTime.Now.Date.Add(lookupTimetableItem.EndTime);
					stringBuilder.Append(array3[(int)lookupTimetableItem.DayOfWeek]);
					stringBuilder.Append(" ");
					stringBuilder.Append(dateTime.ToString("h:mm tt"));
					stringBuilder.Append(" - ");
					stringBuilder.Append(dateTime2.ToString("h:mm tt"));
					bool flag2 = !includeLocation;
					if (!flag2)
					{
						string text = (lookupTimetableItem.Room ?? "").Trim();
						bool flag3 = text.Length < 1;
						if (!flag3)
						{
							stringBuilder.Append(" [");
							stringBuilder.Append(text);
							stringBuilder.Append("]");
							list.Add(stringBuilder.ToString());
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x000317A8 File Offset: 0x0002F9A8
		private string FormatString(string s, int numChars)
		{
			int num = numChars - s.Length;
			return (num <= 0) ? s.Substring(0, numChars) : (s + new string(' ', num));
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000317E0 File Offset: 0x0002F9E0
		private string GetCoursesSignaturesStudents(IList<LookupCourse> courseList)
		{
			bool flag = courseList == null || courseList.Count < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = false;
				string arg = "";
				int num = flag2 ? 20 : 0;
				int num2 = flag2 ? 26 : 40;
				string text = this.FormatString("Course", 15) + (flag2 ? this.FormatString(string.Format("{0} Name", arg), num) : "") + this.FormatString(string.Format("{0} Signature", arg), num2) + this.FormatString("Date", 12);
				int num3 = 0;
				foreach (LookupCourse lookupCourse in from course in courseList
				let curr_lucourseid = course.LuCourseId
				where curr_lucourseid > 0
				select course)
				{
					num3++;
					text += "\r";
					string s = ((lookupCourse.Subject == null || lookupCourse.Subject.SubjectDescription == null) ? "" : lookupCourse.Subject.SubjectDescription) + " " + (lookupCourse.Course ?? "");
					LookupInstructor primaryInstructor = lookupCourse.GetPrimaryInstructor();
					text = string.Concat(new string[]
					{
						text,
						this.FormatString(s, 15),
						flag2 ? this.FormatString((primaryInstructor == null) ? "" : (primaryInstructor.Name ?? ""), num) : "",
						new string('_', num2 - 1),
						" ",
						new string('_', 12)
					});
				}
				bool flag3 = num3 > 0;
				if (flag3)
				{
					result = text;
				}
				else
				{
					for (int i = 0; i < 5; i++)
					{
						text = string.Concat(new string[]
						{
							text,
							"\r",
							new string('_', 14),
							" ",
							flag2 ? (new string('_', num - 1) + " ") : "",
							new string('_', num2 - 1),
							" ",
							new string('_', 12)
						});
					}
					result = text;
				}
			}
			return result;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00031AA8 File Offset: 0x0002FCA8
		private string GetCoursesSignatures(IList<LookupCourse> courseList)
		{
			bool flag = courseList == null || courseList.Count < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = true;
				string arg = "Instructor";
				int num = flag2 ? 20 : 0;
				int num2 = flag2 ? 26 : 40;
				string text = this.FormatString("Course", 15) + (flag2 ? this.FormatString(string.Format("{0} Name", arg), num) : "") + this.FormatString(string.Format("{0} Signature", arg), num2) + this.FormatString("Date", 12);
				int num3 = 0;
				foreach (LookupCourse lookupCourse in from course in courseList
				let curr_lucourseid = course.LuCourseId
				where curr_lucourseid > 0
				select course)
				{
					num3++;
					text += "\r";
					string s = ((lookupCourse.Subject == null || lookupCourse.Subject.SubjectDescription == null) ? "" : lookupCourse.Subject.SubjectDescription) + " " + (lookupCourse.Course ?? "");
					LookupInstructor primaryInstructor = lookupCourse.GetPrimaryInstructor();
					text = string.Concat(new string[]
					{
						text,
						this.FormatString(s, 15),
						flag2 ? this.FormatString((primaryInstructor == null) ? "" : (primaryInstructor.Name ?? ""), num) : "",
						new string('_', num2 - 1),
						" ",
						new string('_', 12)
					});
				}
				bool flag3 = num3 > 0;
				if (flag3)
				{
					result = text;
				}
				else
				{
					for (int i = 0; i < 5; i++)
					{
						text = string.Concat(new string[]
						{
							text,
							"\r",
							new string('_', 14),
							" ",
							flag2 ? (new string('_', num - 1) + " ") : "",
							new string('_', num2 - 1),
							" ",
							new string('_', 12)
						});
					}
					result = text;
				}
			}
			return result;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00031D6C File Offset: 0x0002FF6C
		private bool TryToMailMergeBaseBuiltInCodes(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper Code)
		{
			eMailMergeCode? eMailMergeCode = Code.Name.FindMailMergeCode();
			bool flag = eMailMergeCode == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				switch (eMailMergeCode.Value)
				{
				case eMailMergeCode.BASECODES_Proctor:
				{
					string text = "ClockWork";
					bool flag2 = string.IsNullOrEmpty(text);
					if (flag2)
					{
						text = "ClockWork";
					}
					ISettingManager settingManager = new SettingManager(text, this.OpContext);
					string settingValue = settingManager.GetSettingValue<string>(Setting.GENERAL_LanguageCountryCode);
					bool flag3 = !string.IsNullOrEmpty(settingValue);
					if (flag3)
					{
						Code.Item.SetMailMergeValue(settingValue.Equals("en-us", StringComparison.OrdinalIgnoreCase) ? "Proctor" : "Invigilator");
					}
					result = true;
					break;
				}
				case eMailMergeCode.BASECODES_Date:
					Code.Item.SetMailMergeValue(DateTime.Now);
					Code.Item.ValueFormat = new MailMergeValueFormat
					{
						ValueFormatType = eValueFormatType.CustomFormat,
						CustomFormat = (Context.DefaultDateFormat ?? "MMMM d, yyyy")
					};
					result = true;
					break;
				case eMailMergeCode.BASECODES_Academic_Year:
				{
					Session currentSession = this.GetCurrentSession(Context, ref tempCache);
					bool flag4 = currentSession != null;
					if (flag4)
					{
						Code.Item.SetMailMergeValue((currentSession.StartDate.Month >= 5) ? string.Format("{0} - {1}", currentSession.StartDate.Year.ToString(), (currentSession.StartDate.Year + 1).ToString()) : string.Format("{0} - {1}", (currentSession.StartDate.Year - 1).ToString(), currentSession.StartDate.Year.ToString()));
					}
					result = true;
					break;
				}
				case eMailMergeCode.BASECODES_Semester:
				{
					Session currentSession = this.GetCurrentSession(Context, ref tempCache);
					bool flag5 = currentSession != null;
					if (flag5)
					{
						string title = currentSession.AcademicTerm.Title;
						bool flag6 = !string.IsNullOrEmpty(title) && title.Length > 1;
						if (flag6)
						{
							Code.Item.SetMailMergeValue(title.Substring(0, 2).ToUpper());
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.BASECODES_AcademicTerm:
				{
					Session currentSession = this.GetCurrentSession(Context, ref tempCache);
					bool flag7 = currentSession != null;
					if (flag7)
					{
						Code.Item.SetMailMergeValue(currentSession.AcademicTerm.Title);
					}
					result = true;
					break;
				}
				case eMailMergeCode.BASECODES_Time:
					Code.Item.SetMailMergeValue(DateTime.Now);
					Code.Item.ValueFormat = new MailMergeValueFormat
					{
						ValueFormatType = eValueFormatType.CustomFormat,
						CustomFormat = (Context.DefaultTimeFormat ?? "h:mm tt")
					};
					result = true;
					break;
				case eMailMergeCode.BASECODES_TimeMilitary:
					Code.Item.SetMailMergeValue(DateTime.Now);
					Code.Item.ValueFormat = new MailMergeValueFormat
					{
						ValueFormatType = eValueFormatType.CustomFormat,
						CustomFormat = (Context.DefaultTimeFormat ?? "H:mm")
					};
					result = true;
					break;
				case eMailMergeCode.BASECODES_ActiveUser:
				{
					PersonBase whoAmI = this.GetWhoAmI(ref tempCache);
					bool flag8 = whoAmI != null;
					if (flag8)
					{
						Code.Item.SetMailMergeValue(string.Format("{0} {1}", whoAmI.FirstName ?? "", whoAmI.LastName ?? ""));
					}
					result = true;
					break;
				}
				case eMailMergeCode.BASECODES_ActiveUserFirstName:
				{
					PersonBase whoAmI = this.GetWhoAmI(ref tempCache);
					bool flag9 = whoAmI != null;
					if (flag9)
					{
						Code.Item.SetMailMergeValue(whoAmI.FirstName);
					}
					result = true;
					break;
				}
				case eMailMergeCode.BASECODES_ActiveUserLastName:
				{
					PersonBase whoAmI = this.GetWhoAmI(ref tempCache);
					bool flag10 = whoAmI != null;
					if (flag10)
					{
						Code.Item.SetMailMergeValue(whoAmI.LastName);
					}
					result = true;
					break;
				}
				case eMailMergeCode.BASECODES_ActiveUserEmail:
					Code.Item.SetMailMergeValue(this.GetStaffEmail(this.OpContext.WhoAmI));
					result = true;
					break;
				case eMailMergeCode.BASECODES_ActiveUserPhone:
					Code.Item.SetMailMergeValue(this.GetStaffPhone(this.OpContext.WhoAmI));
					result = true;
					break;
				case eMailMergeCode.BASECODES_ActiveUserTitle:
				{
					int settingValue_Int = this.oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_CounsellorTitle_controlid);
					List<DynamicData> list = this.dynamicDataManager.LoadDataByFields(new DynamicDataContext
					{
						PrimaryId = this.OpContext.WhoAmI
					}, new List<int>
					{
						settingValue_Int
					}, eDynamicFormType.PerStaff);
					bool flag11 = list.Count > 0;
					if (flag11)
					{
						Code.Item.SetMailMergeValue(list[0].GetString());
					}
					result = true;
					break;
				}
				case eMailMergeCode.BASECODES_ActiveUserContact:
				{
					PersonBase whoAmI = this.GetWhoAmI(ref tempCache);
					bool flag12 = whoAmI != null;
					if (flag12)
					{
						string staffEmail = this.GetStaffEmail(this.OpContext.WhoAmI);
						string staffPhone = this.GetStaffPhone(this.OpContext.WhoAmI);
						bool flag13 = !string.IsNullOrEmpty(staffEmail);
						bool flag14 = !string.IsNullOrEmpty(staffPhone);
						bool flag15 = flag13 && flag14;
						if (flag15)
						{
							Code.Item.SetMailMergeValue(string.Format("{0} (phone) or {1} (email)", staffPhone, staffEmail));
						}
						else
						{
							bool flag16 = flag13;
							if (flag16)
							{
								Code.Item.SetMailMergeValue(string.Format("{0} (email)", staffEmail));
							}
							else
							{
								bool flag17 = flag14;
								if (flag17)
								{
									Code.Item.SetMailMergeValue(string.Format("{0} (email)", staffPhone));
								}
							}
						}
					}
					result = true;
					break;
				}
				case eMailMergeCode.BASECODES_ActiveUserSignature:
					Code.Item.SetMailMergeValue(this.staffCommonInfoManager.LoadStaffStoredSignatureData(this.OpContext.WhoAmI));
					result = true;
					break;
				case eMailMergeCode.BASECODES_ClockWork:
					try
					{
						IPAddress[] hostAddresses = Dns.GetHostAddresses(Dns.GetHostName());
						Code.Item.SetMailMergeValue((hostAddresses == null || hostAddresses.Length < 1) ? "" : hostAddresses[0].ToString());
					}
					catch (Exception ex)
					{
						Code.Item.SetMailMergeValue(ex.ToString());
					}
					result = true;
					break;
				default:
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00032368 File Offset: 0x00030568
		private string GetStaffEmail(int pid)
		{
			bool flag = pid < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				int settingValue_Int = this.oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_CounsellorEmail_controlid);
				List<DynamicData> list = this.dynamicDataManager.LoadDataByFields(new DynamicDataContext
				{
					PrimaryId = this.OpContext.WhoAmI
				}, new List<int>
				{
					settingValue_Int
				}, eDynamicFormType.PerStaff);
				result = ((list.Count > 0) ? list[0].GetString() : "");
			}
			return result;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x000323F4 File Offset: 0x000305F4
		private string GetStaffPhone(int pid)
		{
			int settingValue_Int = this.oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_CounsellorPhone_controlid);
			List<DynamicData> list = this.dynamicDataManager.LoadDataByFields(new DynamicDataContext
			{
				PrimaryId = this.OpContext.WhoAmI
			}, new List<int>
			{
				settingValue_Int
			}, eDynamicFormType.PerStaff);
			return (list.Count > 0) ? list[0].GetString() : "";
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00032470 File Offset: 0x00030670
		private IAccommodationsManager accommodationsManager
		{
			get
			{
				bool flag = this.am == null;
				if (flag)
				{
					this.am = new AccommodationsManager(this.OpContext);
				}
				return this.am;
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000324A8 File Offset: 0x000306A8
		private IList<DynamicData> GetInstructorFormData(MailMergeContext Context, Dictionary<string, object> tempCache)
		{
			bool flag = tempCache.ContainsKey("instructorform");
			IList<DynamicData> result;
			if (flag)
			{
				result = (IList<DynamicData>)tempCache["instructorform"];
			}
			else
			{
				IList<DynamicData> list = this.testBookingManager.LoadInstructorFormData(Context.ExamId);
				tempCache["instructorform"] = list;
				result = list;
			}
			return result;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x000324FC File Offset: 0x000306FC
		private IList<AccommodationData> GetExamAccommodations(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper code)
		{
			MailMergingManager.<>c__DisplayClass88_0 CS$<>8__locals1 = new MailMergingManager.<>c__DisplayClass88_0();
			IList<AccommodationData> list = tempCache.ContainsKey("examaccommodations") ? (tempCache["examaccommodations"] as IList<AccommodationData>) : null;
			bool flag = list != null;
			IList<AccommodationData> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				List<AccommodationForTest> list2 = this.testBookingManager.LoadTestAccommodations(Context.AppointmentId, code.GetPersonId(Context), Context.LuCourseId);
				list2 = list2.FindAll((AccommodationForTest f) => f.UseForTest);
				int[] array = (from g in list2
				select g.DynamicFieldData.Field.ControlId into g
				where g > 0
				select g).Distinct<int>().ToArray<int>();
				MailMergingManager.<>c__DisplayClass88_0 CS$<>8__locals2 = CS$<>8__locals1;
				IDictionary<int, ExtendedAccommodationInfo> detailInfo;
				if (array.Length == 0)
				{
					IDictionary<int, ExtendedAccommodationInfo> dictionary = new Dictionary<int, ExtendedAccommodationInfo>();
					detailInfo = dictionary;
				}
				else
				{
					detailInfo = this.dynamicFieldManager.LoadAccommodationShortCodes(array);
				}
				CS$<>8__locals2.detailInfo = detailInfo;
				list = (from g in list2
				select new AccommodationData
				{
					Data = g.DynamicFieldData,
					Detail = (CS$<>8__locals1.detailInfo.ContainsKey(g.DynamicFieldData.Field.ControlId) ? CS$<>8__locals1.detailInfo[g.DynamicFieldData.Field.ControlId] : new ExtendedAccommodationInfo())
				}).ToList<AccommodationData>();
				foreach (AccommodationData accommodationData in list)
				{
					bool flag2 = accommodationData.Detail == null;
					if (flag2)
					{
						accommodationData.Detail = new ExtendedAccommodationInfo();
					}
					accommodationData.Detail.Approved = true;
					accommodationData.Detail.Offline = false;
					accommodationData.Detail.ShowOnLetter = true;
				}
				tempCache["examaccommodations"] = list;
				result = list;
			}
			return result;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x000326B0 File Offset: 0x000308B0
		private IList<int> GetCheckedAccommodationTemplateCids(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper code)
		{
			bool flag = tempCache.ContainsKey("accommodationTemplateCids");
			IList<int> result;
			if (flag)
			{
				result = (IList<int>)tempCache["accommodationTemplateCids"];
			}
			else
			{
				IList<AccommodationData> source = this.accommodationsManager.LoadAccommodationsByStudentAndCourseOrTemplate(code.GetPersonId(Context), 0);
				IList<int> list = (from h in source.Where(delegate(AccommodationData g)
				{
					DynamicField field = g.Data.Field;
					bool flag2 = field.ControlCode != eControlCode.AccommodationCheckBox && field.ControlCode != eControlCode.CheckBox;
					bool result2;
					if (flag2)
					{
						result2 = false;
					}
					else
					{
						object valueForDataTable = g.Data.GetValueForDataTable(typeof(bool));
						bool flag3 = valueForDataTable == null || valueForDataTable == DBNull.Value || !(valueForDataTable is bool);
						result2 = (!flag3 && (bool)valueForDataTable);
					}
					return result2;
				})
				select h.Data.Field.ControlId).ToList<int>();
				tempCache.Add("accommodationTemplateCids", list);
				result = list;
			}
			return result;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00032758 File Offset: 0x00030958
		private List<AccommodationData> GetAccommodations(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper code)
		{
			bool flag = !tempCache.ContainsKey("accommodations");
			List<AccommodationData> list;
			if (flag)
			{
				LookupCourse course = this.GetCourse(Context, tempCache, code);
				int courseId = (course == null) ? 0 : course.LuCourseId;
				list = this.accommodationsManager.LoadAccommodationsByStudentAndCourseOrTemplate(code.GetPersonId(Context), courseId).ToList<AccommodationData>();
				list = list.FindAll((AccommodationData f) => f.Detail.ShowOnLetter && f.Detail.Group > eAccommodationGroup.None);
				tempCache.Add("accommodations", list);
			}
			else
			{
				list = (List<AccommodationData>)tempCache["accommodations"];
			}
			return list;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x000327F8 File Offset: 0x000309F8
		private List<AccommodationData> GetAllAccommodations(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper code)
		{
			int personId = code.GetPersonId(Context);
			bool flag = !tempCache.ContainsKey("allaccommodations");
			List<AccommodationData> list;
			if (flag)
			{
				LookupCourse course = this.GetCourse(Context, tempCache, code);
				int courseId = (course == null) ? 0 : course.LuCourseId;
				list = this.accommodationsManager.LoadAccommodationsByStudentAndCourseOrTemplate(personId, courseId).ToList<AccommodationData>();
				tempCache.Add("allaccommodations", list);
			}
			else
			{
				list = (List<AccommodationData>)tempCache["allaccommodations"];
			}
			return list;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00032878 File Offset: 0x00030A78
		private List<AccommodationData> GetAccommodationsList(MailMergeContext Context, List<AccommodationData> accommodations, MailMergingManager.MailMergeCodeWrapper Code, Dictionary<string, object> tempCache, params KeyValuePair<string, string>[] additionalArgs)
		{
			List<AccommodationData> list = accommodations ?? new List<AccommodationData>();
			Dictionary<string, string> dictionary = (Code == null) ? null : Code.Item.Args;
			bool flag = dictionary != null && dictionary.ContainsKey("cids");
			if (flag)
			{
				string text = dictionary["cids"] ?? "";
				bool flag2 = text.Length > 0;
				if (flag2)
				{
					string[] array = text.Split(new char[]
					{
						'.'
					}, StringSplitOptions.RemoveEmptyEntries);
					List<int> restrictCids = new List<int>();
					foreach (string text2 in array)
					{
						int item;
						bool flag3 = int.TryParse(text2.Trim(), out item);
						if (flag3)
						{
							restrictCids.Add(item);
						}
					}
					bool flag4 = restrictCids.Count > 0;
					if (flag4)
					{
						list = (from g in list
						where restrictCids.Contains(g.Data.Field.ControlId)
						select g).ToList<AccommodationData>();
					}
				}
			}
			return list;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00032988 File Offset: 0x00030B88
		private List<AccommodationData> FilterByShortCodes(List<AccommodationData> accData, MailMergingManager.MailMergeCodeWrapper Code)
		{
			MailMergingManager.<>c__DisplayClass93_0 CS$<>8__locals1 = new MailMergingManager.<>c__DisplayClass93_0();
			Dictionary<string, string> args = Code.Item.Args;
			MailMergingManager.<>c__DisplayClass93_0 CS$<>8__locals2 = CS$<>8__locals1;
			List<string> shortCodes;
			if (!args.ContainsKey("shortcodes"))
			{
				shortCodes = new List<string>();
			}
			else
			{
				shortCodes = (from g in (args["shortcodes"] ?? "").ToLower().Trim().Split(new char[]
				{
					'.'
				}).ToList<string>()
				where g.Trim().Length > 0
				select g).ToList<string>();
			}
			CS$<>8__locals2.shortCodes = shortCodes;
			bool flag = CS$<>8__locals1.shortCodes.Count < 1;
			List<AccommodationData> result;
			if (flag)
			{
				result = accData;
			}
			else
			{
				result = (from f in accData
				where f.Detail != null && f.Detail.ShortCode != null && CS$<>8__locals1.shortCodes.FirstOrDefault((string h) => h.Equals(f.Detail.ShortCode, StringComparison.OrdinalIgnoreCase)) != null
				select f).ToList<AccommodationData>();
			}
			return result;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00032A50 File Offset: 0x00030C50
		private bool TryToMailMergeBaseBulitInAccommodationRelatedCodes(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper Code)
		{
			eMailMergeCode? eMailMergeCode = Code.Name.FindMailMergeCode();
			bool flag = eMailMergeCode == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Dictionary<string, string> args = Code.Item.Args;
				switch (eMailMergeCode.Value)
				{
				case eMailMergeCode.ACCOMMODATIONS_Accommodations:
				case eMailMergeCode.ACCOMMODATIONS_AccommodationsLine:
				case eMailMergeCode.ACCOMMODATIONS_AccommodationsFr:
				{
					List<AccommodationData> list = this.GetAccommodations(Context, tempCache, Code);
					List<AccommodationData> list2 = this.FilterByShortCodes(this.GetAccommodationsList(Context, list, Code, tempCache, Array.Empty<KeyValuePair<string, string>>()), Code);
					bool flag2 = list2 != null && list2.Count > 0 && eMailMergeCode.Value == eMailMergeCode.ACCOMMODATIONS_AccommodationsFr;
					if (flag2)
					{
						List<AccommodationData> list3 = (from g in list2
						select g.Clone()).ToList<AccommodationData>();
						foreach (AccommodationData accommodationData in list3)
						{
							bool flag3 = !string.IsNullOrEmpty(accommodationData.Data.Field.Setting4String);
							if (flag3)
							{
								string setting4String = accommodationData.Data.Field.Setting4String;
								accommodationData.Data.Field.ControlCaption = setting4String;
								bool flag4 = accommodationData.Detail != null;
								if (flag4)
								{
									accommodationData.Detail.LongDescription = "";
								}
							}
						}
						Code.Item.SetMailMergeValue(list3);
					}
					else
					{
						Code.Item.SetMailMergeValue(list2);
					}
					return true;
				}
				case eMailMergeCode.ACCOMMODATIONS_AccommodationsProf:
				{
					List<AccommodationData> list = this.GetAccommodations(Context, tempCache, Code);
					List<AccommodationData> accommodations = list.FindAll((AccommodationData f) => (f.Detail.Group & eAccommodationGroup.Classroom) > eAccommodationGroup.None);
					List<AccommodationData> list4 = this.FilterByShortCodes(this.GetAccommodationsList(Context, accommodations, Code, tempCache, Array.Empty<KeyValuePair<string, string>>()), Code);
					bool flag5 = args.ContainsKey("useshortcodes");
					if (flag5)
					{
						Code.Item.SetMailMergeValue(list4.ConvertAll<string>((AccommodationData g) => g.GetStringShortCodes()).ToList<string>());
					}
					else
					{
						Code.Item.SetMailMergeValue(list4);
					}
					return true;
				}
				case eMailMergeCode.ACCOMMODATIONS_AccommodationsExam:
				{
					List<AccommodationData> list = this.GetAccommodations(Context, tempCache, Code);
					List<AccommodationData> accommodations2 = list.FindAll((AccommodationData f) => (f.Detail.Group & eAccommodationGroup.TestExam) > eAccommodationGroup.None);
					List<AccommodationData> list5 = this.FilterByShortCodes(this.GetAccommodationsList(Context, accommodations2, Code, tempCache, Array.Empty<KeyValuePair<string, string>>()), Code);
					bool flag6 = args.ContainsKey("useshortcodes");
					if (flag6)
					{
						Code.Item.SetMailMergeValue(list5.ConvertAll<string>((AccommodationData g) => g.GetStringShortCodes()).ToList<string>());
					}
					else
					{
						Code.Item.SetMailMergeValue(list5);
					}
					return true;
				}
				case eMailMergeCode.ACCOMMODATIONS_AccommodationsOther:
				{
					List<AccommodationData> list = this.GetAccommodations(Context, tempCache, Code);
					List<AccommodationData> accommodations3 = list.FindAll((AccommodationData f) => (f.Detail.Group & eAccommodationGroup.Other) > eAccommodationGroup.None);
					List<AccommodationData> list6 = this.FilterByShortCodes(this.GetAccommodationsList(Context, accommodations3, Code, tempCache, Array.Empty<KeyValuePair<string, string>>()), Code);
					bool flag7 = args.ContainsKey("useshortcodes");
					if (flag7)
					{
						Code.Item.SetMailMergeValue(list6.ConvertAll<string>((AccommodationData g) => g.GetStringShortCodes()).ToList<string>());
					}
					else
					{
						Code.Item.SetMailMergeValue(list6);
					}
					return true;
				}
				case eMailMergeCode.ACCOMMODATIONS_AccommodationsReport:
				{
					List<AccommodationData> list = this.GetAccommodations(Context, tempCache, Code);
					List<AccommodationData> accommodations4 = list.FindAll((AccommodationData f) => (f.Detail.Group & eAccommodationGroup.Report) > eAccommodationGroup.None);
					List<AccommodationData> list7 = this.FilterByShortCodes(this.GetAccommodationsList(Context, accommodations4, Code, tempCache, Array.Empty<KeyValuePair<string, string>>()), Code);
					bool flag8 = args.ContainsKey("useshortcodes");
					if (flag8)
					{
						Code.Item.SetMailMergeValue(list7.ConvertAll<string>((AccommodationData g) => g.GetStringShortCodes()).ToList<string>());
					}
					else
					{
						Code.Item.SetMailMergeValue(list7);
					}
					return true;
				}
				case eMailMergeCode.ACCOMMODATIONS_AccommodationsApproved:
				{
					List<AccommodationData> list = this.GetAccommodations(Context, tempCache, Code);
					List<AccommodationData> accommodations5 = list.FindAll((AccommodationData f) => f.Detail.Approved);
					Code.Item.SetMailMergeValue(this.FilterByShortCodes(this.GetAccommodationsList(Context, accommodations5, Code, tempCache, Array.Empty<KeyValuePair<string, string>>()), Code));
					return true;
				}
				case eMailMergeCode.ACCOMMODATIONS_AccommodationsApprovedProf:
				{
					List<AccommodationData> list = this.GetAccommodations(Context, tempCache, Code);
					List<AccommodationData> accommodations6 = list.FindAll((AccommodationData f) => f.Detail.Approved && (f.Detail.Group & eAccommodationGroup.Classroom) > eAccommodationGroup.None);
					Code.Item.SetMailMergeValue(this.FilterByShortCodes(this.GetAccommodationsList(Context, accommodations6, Code, tempCache, Array.Empty<KeyValuePair<string, string>>()), Code));
					return true;
				}
				case eMailMergeCode.ACCOMMODATIONS_AccommodationsApprovedExam:
				{
					List<AccommodationData> list = this.GetAccommodations(Context, tempCache, Code);
					List<AccommodationData> accommodations7 = list.FindAll((AccommodationData f) => f.Detail.Approved && (f.Detail.Group & eAccommodationGroup.TestExam) > eAccommodationGroup.None);
					Code.Item.SetMailMergeValue(this.FilterByShortCodes(this.GetAccommodationsList(Context, accommodations7, Code, tempCache, Array.Empty<KeyValuePair<string, string>>()), Code));
					break;
				}
				case eMailMergeCode.ACCOMMODATIONS_AccommodationsApprovedOther:
				{
					List<AccommodationData> list = this.GetAccommodations(Context, tempCache, Code);
					List<AccommodationData> accommodations8 = list.FindAll((AccommodationData f) => f.Detail.Approved && (f.Detail.Group & eAccommodationGroup.Other) > eAccommodationGroup.None);
					Code.Item.SetMailMergeValue(this.FilterByShortCodes(this.GetAccommodationsList(Context, accommodations8, Code, tempCache, Array.Empty<KeyValuePair<string, string>>()), Code));
					return true;
				}
				case eMailMergeCode.ACCOMMODATIONS_AccommodationsApprovedReport:
				{
					List<AccommodationData> list = this.GetAccommodations(Context, tempCache, Code);
					List<AccommodationData> accommodations9 = list.FindAll((AccommodationData f) => f.Detail.Approved && (f.Detail.Group & eAccommodationGroup.Report) > eAccommodationGroup.None);
					Code.Item.SetMailMergeValue(this.FilterByShortCodes(this.GetAccommodationsList(Context, accommodations9, Code, tempCache, Array.Empty<KeyValuePair<string, string>>()), Code));
					return true;
				}
				case eMailMergeCode.ACCOMMODATIONS_AccommodationCount:
				{
					List<AccommodationData> list = this.FilterByShortCodes(this.GetAccommodations(Context, tempCache, Code), Code);
					Code.Item.SetMailMergeValue(list.Count);
					return true;
				}
				case eMailMergeCode.Accommodations_AccommodationsShortCode:
				{
					List<AccommodationData> list = this.GetAccommodations(Context, tempCache, Code);
					List<AccommodationData> accommodationsList = this.GetAccommodationsList(Context, list, Code, tempCache, Array.Empty<KeyValuePair<string, string>>());
					Code.Item.SetMailMergeValue(this.FilterByShortCodes(accommodationsList, Code));
					return true;
				}
				case eMailMergeCode.Accommodations_DateLetterIssued:
				{
					CourseRegistration courseRegistration = this.GetCourseRegistration(Context, tempCache, Code);
					bool flag9 = courseRegistration != null && courseRegistration.DateLetterIssued != null;
					if (flag9)
					{
						Code.Item.SetMailMergeValue(courseRegistration.DateLetterIssued.Value);
					}
					else
					{
						Code.Item.SetMailMergeValue("");
					}
					return true;
				}
				case eMailMergeCode.Accommodations_DateStudentLastViewedLetter:
				{
					CourseRegistration courseRegistration2 = this.GetCourseRegistration(Context, tempCache, Code);
					bool flag10 = courseRegistration2 != null && courseRegistration2.DateStudentLastViewed != null;
					if (flag10)
					{
						Code.Item.SetMailMergeValue(courseRegistration2.DateStudentLastViewed.Value);
					}
					else
					{
						Code.Item.SetMailMergeValue("");
					}
					return true;
				}
				case eMailMergeCode.Accommodations_DateLetterReturned:
				{
					CourseRegistration courseRegistration3 = this.GetCourseRegistration(Context, tempCache, Code);
					bool flag11 = courseRegistration3 != null && courseRegistration3.DateLetterReturned != null;
					if (flag11)
					{
						Code.Item.SetMailMergeValue(courseRegistration3.DateLetterReturned.Value);
					}
					else
					{
						Code.Item.SetMailMergeValue("");
					}
					return true;
				}
				case eMailMergeCode.Accommodations_ExamAccommodationsShort:
				{
					List<AccommodationData> list = this.GetAccommodations(Context, tempCache, Code);
					List<AccommodationData> accommodations10 = list.FindAll((AccommodationData f) => (f.Detail.Group & eAccommodationGroup.TestExam) > eAccommodationGroup.None);
					List<AccommodationData> list8 = this.FilterByShortCodes(this.GetAccommodationsList(Context, accommodations10, Code, tempCache, Array.Empty<KeyValuePair<string, string>>()), Code);
					MailMergeCode item = Code.Item;
					string mailMergeValue;
					if (!args.ContainsKey("usebullets"))
					{
						mailMergeValue = string.Join(",", list8.ConvertAll<string>((AccommodationData g) => g.GetStringShortCodes()).ToArray());
					}
					else
					{
						mailMergeValue = string.Join(Environment.NewLine, list8.ConvertAll<string>((AccommodationData g) => "* " + g.GetStringShortCodes()).ToArray());
					}
					item.SetMailMergeValue(mailMergeValue);
					return true;
				}
				case eMailMergeCode.Accommodations_ProfAccommodationsShort:
				{
					List<AccommodationData> list = this.GetAccommodations(Context, tempCache, Code);
					List<AccommodationData> accommodations11 = list.FindAll((AccommodationData f) => (f.Detail.Group & eAccommodationGroup.Classroom) > eAccommodationGroup.None);
					List<AccommodationData> list9 = this.FilterByShortCodes(this.GetAccommodationsList(Context, accommodations11, Code, tempCache, Array.Empty<KeyValuePair<string, string>>()), Code);
					MailMergeCode item2 = Code.Item;
					string mailMergeValue2;
					if (!args.ContainsKey("usebullets"))
					{
						mailMergeValue2 = string.Join(",", list9.ConvertAll<string>((AccommodationData g) => g.GetStringShortCodes()).ToArray());
					}
					else
					{
						mailMergeValue2 = string.Join(Environment.NewLine, list9.ConvertAll<string>((AccommodationData g) => "* " + g.GetStringShortCodes()).ToArray());
					}
					item2.SetMailMergeValue(mailMergeValue2);
					return true;
				}
				case eMailMergeCode.Accommodations_AccommodationsInsert:
				{
					string text = args.ContainsKey("tid") ? args["tid"] : "";
					string text2 = args.ContainsKey("cid") ? args["cid"] : "";
					int num;
					bool flag12 = text.Length < 1 || !int.TryParse(text, out num);
					if (flag12)
					{
						num = 0;
					}
					int num2;
					bool flag13 = text2.Length < 1 || !int.TryParse(text2, out num2);
					if (flag13)
					{
						num2 = 0;
					}
					bool flag14 = num < 1 || num2 < 1;
					if (flag14)
					{
						CWLogger.Logger.Warn("Common.Core.MailMerging.MailMergingManager:AccommodationsInsertCodeIsMissingTidOrCid:code={0}:tid={1}:cid={2}", Code.Item.OriginalCode ?? "NULL", num.ToString(), num2.ToString());
						return true;
					}
					IList<int> checkedAccommodationTemplateCids = this.GetCheckedAccommodationTemplateCids(Context, tempCache, Code);
					bool flag15 = checkedAccommodationTemplateCids.Contains(num2);
					if (flag15)
					{
						Code.Item.SetMailMergeValue("");
						Code.Item.ValueFormat = new MailMergeValueFormat
						{
							ValueFormatType = eValueFormatType.InsertedMailMergeDocument,
							CustomFormat = num.ToString()
						};
					}
					return true;
				}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00033510 File Offset: 0x00031710
		private int CalculateAge(DateTime birthDate)
		{
			DateTime date = DateTime.Now.Date;
			int num = date.Year - birthDate.Year;
			bool flag = date.Month < birthDate.Month || (date.Month == birthDate.Month && date.Day < birthDate.Day);
			if (flag)
			{
				num--;
			}
			return num;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00033584 File Offset: 0x00031784
		private ServiceProvider GetServiceProvider(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper code)
		{
			int serviceProviderId = Context.ServiceProviderId;
			bool flag = serviceProviderId < 1;
			ServiceProvider result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string key = "sp" + serviceProviderId.ToString();
				bool flag2 = tempCache.ContainsKey(key);
				if (flag2)
				{
					result = (ServiceProvider)tempCache[key];
				}
				else
				{
					ServiceProviderOriginalProviderManager serviceProviderOriginalProviderManager = new ServiceProviderOriginalProviderManager(this.OpContext);
					ServiceProvider serviceProvider = serviceProviderOriginalProviderManager.LoadProviderById(serviceProviderId);
					tempCache.Add(key, serviceProvider);
					result = serviceProvider;
				}
			}
			return result;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x000335FC File Offset: 0x000317FC
		private PersonBase GetPerson(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper code)
		{
			int personId = code.GetPersonId(Context);
			string key = "pb" + code.AltPersonIdIndex.ToString() + personId.ToString();
			bool flag = tempCache.ContainsKey(key);
			PersonBase result;
			if (flag)
			{
				result = (PersonBase)tempCache[key];
			}
			else
			{
				PeopleManager peopleManager = this.peopleManager;
				PersonBase personBase = peopleManager.LoadPerson(personId);
				tempCache.Add(key, personBase);
				result = personBase;
			}
			return result;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00033670 File Offset: 0x00031870
		private MediaContentRequestedInfo GetAlternateFormatRequest(MailMergeContext context, Dictionary<string, object> tempCache)
		{
			int alternateFormatRequestId = context.AlternateFormatRequestId;
			bool flag = alternateFormatRequestId == 0;
			MediaContentRequestedInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string key = string.Format("afreqid{0}", alternateFormatRequestId);
				bool flag2 = tempCache.ContainsKey(key);
				if (flag2)
				{
					result = (MediaContentRequestedInfo)tempCache[key];
				}
				else
				{
					IStudentMediaRequestManager studentMediaRequestManager = new StudentMediaRequestManager(this.OpContext);
					MediaContentRequestedInfo mediaContentRequestedInfo = studentMediaRequestManager.LoadMediaContentRequestedInfoById(alternateFormatRequestId);
					bool flag3 = mediaContentRequestedInfo == null;
					if (flag3)
					{
						mediaContentRequestedInfo = studentMediaRequestManager.LoadArchiveMediaContentRequestedInfoById(alternateFormatRequestId);
					}
					tempCache.Add(key, mediaContentRequestedInfo);
					result = mediaContentRequestedInfo;
				}
			}
			return result;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000336FC File Offset: 0x000318FC
		private MediaContent GetAlternateFormatMediaContent(MailMergeContext context, Dictionary<string, object> tempCache)
		{
			Guid alternateFormatMediaContentId = context.AlternateFormatMediaContentId;
			bool flag = alternateFormatMediaContentId == Guid.Empty;
			MediaContent result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string key = string.Format("afmcid_{0}", alternateFormatMediaContentId);
				bool flag2 = tempCache.ContainsKey(key);
				if (flag2)
				{
					result = (MediaContent)tempCache[key];
				}
				else
				{
					IMediaContentManager mediaContentManager = new MediaContentManager(this.OpContext);
					MediaContent mediaContent = mediaContentManager.LoadMediaContentById(alternateFormatMediaContentId);
					tempCache.Add(key, mediaContent);
					result = mediaContent;
				}
			}
			return result;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0003377C File Offset: 0x0003197C
		private MediaPublisher GetAlternateFormatPublisher(MailMergeContext context, Dictionary<string, object> tempCache)
		{
			int alternateFormatPublisherId = context.AlternateFormatPublisherId;
			bool flag = alternateFormatPublisherId == 0;
			MediaPublisher result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string key = string.Format("afpubid{0}", alternateFormatPublisherId);
				bool flag2 = tempCache.ContainsKey(key);
				if (flag2)
				{
					result = (MediaPublisher)tempCache[key];
				}
				else
				{
					IMediaPublisherManager mediaPublisherManager = new MediaPublisherManager(this.OpContext);
					MediaPublisher mediaPublisher = mediaPublisherManager.LoadPublisherById(alternateFormatPublisherId);
					tempCache.Add(key, mediaPublisher);
					result = mediaPublisher;
				}
			}
			return result;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000337F4 File Offset: 0x000319F4
		private MediaVendor GetAlternateFormatVendor(MailMergeContext context, Dictionary<string, object> tempCache)
		{
			int alternateFormatVendorId = context.AlternateFormatVendorId;
			bool flag = alternateFormatVendorId == 0;
			MediaVendor result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string key = string.Format("afvendorid{0}", alternateFormatVendorId);
				bool flag2 = tempCache.ContainsKey(key);
				if (flag2)
				{
					result = (MediaVendor)tempCache[key];
				}
				else
				{
					IMediaVendorManager mediaVendorManager = new MediaVendorManager(this.OpContext);
					MediaVendor mediaVendor = mediaVendorManager.LoadMediaVendorById(alternateFormatVendorId);
					tempCache.Add(key, mediaVendor);
					result = mediaVendor;
				}
			}
			return result;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0003386C File Offset: 0x00031A6C
		private InventoryProduct GetProduct(MailMergeContext Context, Dictionary<string, object> tempCache)
		{
			string productUniqueId = Context.ProductUniqueId;
			bool flag = string.IsNullOrEmpty(productUniqueId);
			InventoryProduct result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string key = string.Format("ipuid{0}", productUniqueId);
				bool flag2 = tempCache.ContainsKey(key);
				if (flag2)
				{
					result = (InventoryProduct)tempCache[key];
				}
				else
				{
					IInventoryProductManager inventoryProductManager = new InventoryProductManager(this.OpContext);
					InventoryProduct productById = inventoryProductManager.GetProductById(Context.CatalogId, new Guid(productUniqueId));
					tempCache.Add(key, productById);
					result = productById;
				}
			}
			return result;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x000338EC File Offset: 0x00031AEC
		private InventoryLoan GetProductLoan(MailMergeContext Context, Dictionary<string, object> tempCache)
		{
			int loanId = Context.LoanId;
			bool flag = loanId == 0;
			InventoryLoan result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string key = string.Format("ilid{0}", loanId);
				bool flag2 = tempCache.ContainsKey(key);
				if (flag2)
				{
					result = (InventoryLoan)tempCache[key];
				}
				else
				{
					IInventoryLoanManager inventoryLoanManager = new InventoryLoanManager(this.OpContext);
					InventoryLoan activeLoanById = inventoryLoanManager.GetActiveLoanById(loanId);
					tempCache.Add(key, activeLoanById);
					result = activeLoanById;
				}
			}
			return result;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00033964 File Offset: 0x00031B64
		private StudentCommonInfo GetPersonCommonInfo(MailMergeContext Context, ref Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper code)
		{
			int personId = code.GetPersonId(Context);
			string key = "common_" + personId.ToString();
			bool flag = tempCache.ContainsKey(key);
			StudentCommonInfo result;
			if (flag)
			{
				result = (StudentCommonInfo)tempCache[key];
			}
			else
			{
				StudentCommonInfo studentCommonInfo = this.studentCommonInfoManager.LoadStudentCommonInfo(personId);
				tempCache.Add(key, studentCommonInfo);
				result = studentCommonInfo;
			}
			return result;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000339C8 File Offset: 0x00031BC8
		private Appointment GetAppointment(MailMergeContext Context, Dictionary<string, object> tempCache)
		{
			bool flag = Context.AppointmentId < 1;
			Appointment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = tempCache.ContainsKey("appointment");
				if (flag2)
				{
					result = (Appointment)tempCache["appointment"];
				}
				else
				{
					Appointment appointment = this.appointmentManager.LoadAppointment(Context.AppointmentId);
					bool flag3 = appointment != null;
					if (flag3)
					{
						tempCache["appointment"] = appointment;
					}
					result = appointment;
				}
			}
			return result;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00033A3C File Offset: 0x00031C3C
		private Test GetTest(MailMergeContext Context, Dictionary<string, object> tempCache)
		{
			bool flag = Context.AppointmentId < 1;
			Test result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = tempCache.ContainsKey("test");
				if (flag2)
				{
					result = (Test)tempCache["test"];
				}
				else
				{
					Test test = this.testBookingManager.LoadTestByAppointmentId(Context.AppointmentId);
					bool flag3 = test != null;
					if (flag3)
					{
						tempCache["test"] = test;
					}
					result = test;
				}
			}
			return result;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00033AB0 File Offset: 0x00031CB0
		private ClassTest GetClassTest(MailMergeContext Context, Dictionary<string, object> tempCache)
		{
			bool flag = Context.ExamId < 1 && Context.AppointmentId < 1;
			ClassTest result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = tempCache.ContainsKey("classtest");
				if (flag2)
				{
					result = (ClassTest)tempCache["classtest"];
				}
				else
				{
					ClassTest classTest = null;
					bool flag3 = Context.ExamId > 0;
					if (flag3)
					{
						classTest = this.classTestManager.LoadClassTestDefinitionById(Context.ExamId);
						bool flag4 = classTest != null;
						if (flag4)
						{
							tempCache["classtest"] = classTest;
						}
					}
					else
					{
						bool flag5 = Context.AppointmentId > 0;
						if (flag5)
						{
							classTest = this.classTestManager.LoadClassTestDefinitionByAppointmentId(Context.AppointmentId);
							bool flag6 = classTest != null;
							if (flag6)
							{
								tempCache["classtest"] = classTest;
							}
						}
					}
					result = classTest;
				}
			}
			return result;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00033B80 File Offset: 0x00031D80
		private LookupCourse GetCourse(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper code)
		{
			IList<LookupCourse> courses = this.GetCourses(Context, tempCache, code);
			bool flag = courses != null && courses.Count > 0;
			LookupCourse result;
			if (flag)
			{
				LookupCourse lookupCourse = (Context.LuCourseId > 0) ? courses.FirstOrDefault((LookupCourse f) => f.LuCourseId == Context.LuCourseId) : null;
				bool flag2 = lookupCourse != null;
				if (flag2)
				{
					result = lookupCourse;
				}
				else
				{
					result = courses[0];
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00033C04 File Offset: 0x00031E04
		private CourseRegistration GetCourseRegistration(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper code)
		{
			int personId = code.GetPersonId(Context);
			string key = "coursereg" + code.AltPersonIdIndex.ToString();
			bool flag = tempCache.ContainsKey(key);
			CourseRegistration result;
			if (flag)
			{
				result = (CourseRegistration)tempCache[key];
			}
			else
			{
				int num = (Context.LuCourseId > 0) ? Context.LuCourseId : ((Context.LuCourseIds != null && Context.LuCourseIds.Count > 0) ? Context.LuCourseIds[0] : 0);
				bool flag2 = num > 0;
				if (flag2)
				{
					ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(this.OpContext);
					CourseRegistration courseRegistration = courseRegistrationManager.LoadCourseRegistrationsByStudentAndCourse(personId, num);
					tempCache.Add(key, courseRegistration);
					result = courseRegistration;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00033CC4 File Offset: 0x00031EC4
		private IList<int> GetContextLuCourseIds(MailMergeContext Context, Dictionary<string, object> tempCache)
		{
			string key = "contextLucids";
			bool flag = tempCache.ContainsKey(key);
			IList<int> result;
			if (flag)
			{
				result = (List<int>)tempCache[key];
			}
			else
			{
				List<int> list = new List<int>();
				bool flag2 = Context.LuCourseId > 0;
				if (flag2)
				{
					list.Add(Context.LuCourseId);
				}
				bool flag3 = Context.LuCourseIds != null && Context.LuCourseIds.Count > 0;
				if (flag3)
				{
					list.AddRange(Context.LuCourseIds.ToArray());
				}
				bool flag4 = Context.ExamId > 0 || Context.AppointmentId > 0;
				if (flag4)
				{
					ClassTest classTest = this.GetClassTest(Context, tempCache);
					bool flag5 = classTest != null && classTest.Course != null && classTest.Course.LuCourseId > 0 && !list.Contains(classTest.Course.LuCourseId);
					if (flag5)
					{
						list.Add(classTest.Course.LuCourseId);
					}
				}
				tempCache.Add(key, list);
				result = list;
			}
			return result;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00033DCC File Offset: 0x00031FCC
		private IList<LookupCourse> GetCourses(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper code)
		{
			string key = "courses" + code.AltPersonIdIndex.ToString();
			bool flag = tempCache.ContainsKey(key);
			IList<LookupCourse> result;
			if (flag)
			{
				result = (IList<LookupCourse>)tempCache[key];
			}
			else
			{
				IList<int> contextLuCourseIds = this.GetContextLuCourseIds(Context, tempCache);
				IList<LookupCourse> list2;
				if (contextLuCourseIds.Count <= 0)
				{
					IList<LookupCourse> list = new List<LookupCourse>();
					list2 = list;
				}
				else
				{
					list2 = this.lookupCourseManager.LoadCoursesByIds(contextLuCourseIds);
				}
				IList<LookupCourse> list3 = list2;
				tempCache.Add(key, list3);
				result = list3;
			}
			return result;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00033E4C File Offset: 0x0003204C
		private List<DynamicData> GetPerDateData(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper code)
		{
			int personId = code.GetPersonId(Context);
			string key = "pdd" + code.AltPersonIdIndex.ToString();
			bool flag = tempCache.ContainsKey(key);
			List<DynamicData> result;
			if (flag)
			{
				result = (List<DynamicData>)tempCache[key];
			}
			else
			{
				List<DynamicData> list = this.dao.LoadAllPerDateData(personId, Context.PerDateId);
				tempCache.Add(key, list);
				result = list;
			}
			return result;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00033EBC File Offset: 0x000320BC
		private List<DynamicData> GetPerAppData(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper code)
		{
			int personId = code.GetPersonId(Context);
			string key = "pad" + code.AltPersonIdIndex.ToString();
			bool flag = tempCache.ContainsKey(key);
			List<DynamicData> result;
			if (flag)
			{
				result = (List<DynamicData>)tempCache[key];
			}
			else
			{
				List<DynamicData> list = this.dao.LoadAllPerAppointmentData(personId, Context.AppointmentId);
				tempCache.Add(key, list);
				result = list;
			}
			return result;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00033F2C File Offset: 0x0003212C
		private List<DynamicData> GetPerOnlineFormData(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper code)
		{
			int personId = code.GetPersonId(Context);
			int peopleOnlineFormId = Context.PeopleOnlineFormId;
			bool flag = peopleOnlineFormId < 1 || personId < 1;
			List<DynamicData> result;
			if (flag)
			{
				result = new List<DynamicData>();
			}
			else
			{
				string key = "ofsd" + code.AltPersonIdIndex.ToString();
				bool flag2 = tempCache.ContainsKey(key);
				if (flag2)
				{
					result = (List<DynamicData>)tempCache[key];
				}
				else
				{
					IOnlineFormQueueManager onlineFormQueueManager = new OnlineFormQueueManager(this.OpContext);
					IList<DynamicData> list = onlineFormQueueManager.LoadOnlineFormQueueItemFormDataItems(peopleOnlineFormId);
					List<DynamicData> list2 = ((list != null) ? list.ToList<DynamicData>() : null) ?? new List<DynamicData>();
					tempCache.Add(key, list2);
					result = list2;
				}
			}
			return result;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00033FD8 File Offset: 0x000321D8
		private List<DynamicData> GetPerStudentData(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper code)
		{
			int personId = code.GetPersonId(Context);
			string key = "psd" + code.AltPersonIdIndex.ToString();
			bool flag = tempCache.ContainsKey(key);
			List<DynamicData> result;
			if (flag)
			{
				result = (List<DynamicData>)tempCache[key];
			}
			else
			{
				List<DynamicData> list = this.dao.LoadAllPerStudentData(personId);
				tempCache.Add(key, list);
				result = list;
			}
			return result;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00034044 File Offset: 0x00032244
		private List<DynamicData> GetAccommodationTemplateData(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper code)
		{
			int personId = code.GetPersonId(Context);
			string key = "atd" + code.AltPersonIdIndex.ToString();
			bool flag = tempCache.ContainsKey(key);
			List<DynamicData> result;
			if (flag)
			{
				result = (List<DynamicData>)tempCache[key];
			}
			else
			{
				List<DynamicData> list = this.dao.LoadAllAccommodationTemplateData(personId);
				tempCache.Add(key, list);
				result = list;
			}
			return result;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x000340B0 File Offset: 0x000322B0
		private List<DynamicData> GetAccommodationCourseSpecificData(MailMergeContext Context, Dictionary<string, object> tempCache, MailMergingManager.MailMergeCodeWrapper code)
		{
			int personId = code.GetPersonId(Context);
			string key = "atd" + code.AltPersonIdIndex.ToString();
			bool flag = tempCache.ContainsKey(key);
			List<DynamicData> result;
			if (flag)
			{
				result = (List<DynamicData>)tempCache[key];
			}
			else
			{
				List<DynamicData> list = this.dao.LoadAllAccommodationTemplateData(personId);
				tempCache.Add(key, list);
				result = list;
			}
			return result;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0003411C File Offset: 0x0003231C
		private bool IsInteger(string s)
		{
			int num;
			return int.TryParse(s, out num);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00034140 File Offset: 0x00032340
		private Dictionary<int, string> PreProcessRawCodesToProvideBackwardCompatability(ref List<string> codeMatches)
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			for (int i = 0; i < codeMatches.Count; i++)
			{
				string text = codeMatches[i].ToLower();
				int num = text.IndexOf("...");
				string text2 = codeMatches[i];
				bool flag = text.StartsWith("startmonth") && (text.Length == 10 || num > 0);
				if (flag)
				{
					bool flag2 = num > 0;
					if (flag2)
					{
						text2 = string.Format("startmonth`formatstring={0}", codeMatches[i].Substring(num + 3));
					}
					else
					{
						text2 = "startmonth`formatstring=MMMM";
					}
				}
				else
				{
					bool flag3 = text.StartsWith("startyear") && (text.Length == 10 || num > 0);
					if (flag3)
					{
						bool flag4 = num > 0;
						if (flag4)
						{
							text2 = string.Format("startyear`formatstring={0}", codeMatches[i].Substring(num + 3));
						}
						else
						{
							text2 = "startyear`formatstring=yyyy";
						}
					}
					else
					{
						bool flag5 = num > 0;
						if (flag5)
						{
							string arg = codeMatches[i].Substring(0, num);
							text2 = string.Format("{0}`formatstring={1}", arg, codeMatches[i].Substring(num + 3));
						}
					}
				}
				bool flag6 = text.Equals("accommodations") || text.Equals("accommodationsprof") || text.Equals("accommodationsother") || text.Equals("accommodationsreport") || text.Equals("accommodationsexam");
				if (flag6)
				{
					text2 = text + "`defaultvalue=None";
				}
				bool flag7 = !text2.Equals(codeMatches[i]);
				if (flag7)
				{
					dictionary.Add(i, codeMatches[i]);
					codeMatches[i] = text2;
				}
			}
			return dictionary;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0003431C File Offset: 0x0003251C
		public IList<MailMergeCode> ExtractUniqueCodes(List<string> codeMatches, IDictionary<string, string> fieldMappings = null)
		{
			List<MailMergeCode> list = new List<MailMergeCode>();
			Dictionary<int, string> dictionary = this.PreProcessRawCodesToProvideBackwardCompatability(ref codeMatches);
			for (int i = 0; i < codeMatches.Count; i++)
			{
				string text = codeMatches[i];
				bool flag = string.IsNullOrEmpty(text);
				if (!flag)
				{
					string text2 = text;
					string[] array = (text2.IndexOf('`') > 0) ? text2.Split(new char[]
					{
						'`'
					}) : new string[0];
					string text3 = (array.Length != 0) ? array[0] : text2;
					bool flag2 = fieldMappings != null && fieldMappings.ContainsKey(text3);
					if (flag2)
					{
						text3 = fieldMappings[text3];
					}
					MailMergeCode mailMergeCode = new MailMergeCode
					{
						OriginalCode = (dictionary.ContainsKey(i) ? dictionary[i] : text2),
						Name = text3,
						Args = new Dictionary<string, string>()
					};
					for (int j = 1; j < array.Length; j++)
					{
						string text4 = array[j];
						int num = text4.IndexOf('=');
						bool flag3 = num > 0;
						if (flag3)
						{
							mailMergeCode.Args.Add(text4.Substring(0, num), text4.Substring(num + 1));
						}
						else
						{
							mailMergeCode.Args.Add(text4, "");
						}
					}
					bool flag4 = mailMergeCode.Args.ContainsKey("defaultvalue");
					if (flag4)
					{
						mailMergeCode.DefaultValue = mailMergeCode.Args["defaultvalue"];
					}
					string codeValueTrimAndNoNulls = MailMergingManager.GetCodeValueTrimAndNoNulls(mailMergeCode.Args, "customformat");
					string codeValueTrimAndNoNulls2 = MailMergingManager.GetCodeValueTrimAndNoNulls(mailMergeCode.Args, "formatstring");
					string codeValueTrimAndNoNulls3 = MailMergingManager.GetCodeValueTrimAndNoNulls(mailMergeCode.Args, "formattype");
					eValueFormatType? eValueFormatType = null;
					bool flag5 = codeValueTrimAndNoNulls3.Length > 0 && Enum.IsDefined(typeof(eValueFormatType), codeValueTrimAndNoNulls3);
					if (flag5)
					{
						eValueFormatType = new eValueFormatType?((eValueFormatType)Enum.Parse(typeof(eValueFormatType), codeValueTrimAndNoNulls3));
					}
					bool flag6 = codeValueTrimAndNoNulls.Length > 0 && eValueFormatType == null;
					if (flag6)
					{
						eValueFormatType = new eValueFormatType?(eValueFormatType.CustomFormat);
					}
					bool flag7 = codeValueTrimAndNoNulls2.Length > 0 && eValueFormatType == null;
					if (flag7)
					{
						eValueFormatType = new eValueFormatType?(eValueFormatType.CustomFormat);
					}
					bool flag8 = eValueFormatType != null;
					if (flag8)
					{
						mailMergeCode.ValueFormat = new MailMergeValueFormat
						{
							ValueFormatType = eValueFormatType.Value,
							CustomFormat = codeValueTrimAndNoNulls + codeValueTrimAndNoNulls2
						};
					}
					list.Add(mailMergeCode);
				}
			}
			return list;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x000345B8 File Offset: 0x000327B8
		private static string GetCodeValueTrimAndNoNulls(IDictionary<string, string> args, string codeName)
		{
			return (!args.ContainsKey(codeName)) ? string.Empty : (args[codeName] ?? "").Trim();
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000345F0 File Offset: 0x000327F0
		public List<string> ConvertMatchCollectionToStringList(MatchCollection mc)
		{
			List<string> list = new List<string>();
			foreach (object obj in mc)
			{
				Match match = (Match)obj;
				string text = match.Value.Trim();
				bool flag = text.StartsWith("#") && text.EndsWith("#") && text.Length > 4;
				if (flag)
				{
					text = text.Substring(2, text.Length - 4);
				}
				list.Add(text);
			}
			return list;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000346A4 File Offset: 0x000328A4
		public IList<MailMergeCode> ExtractCodes(string Template)
		{
			Regex regex = new Regex("#<[^#>]*>#");
			MatchCollection mc = regex.Matches(Template);
			return this.ExtractUniqueCodes(this.ConvertMatchCollectionToStringList(mc), null);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000346D8 File Offset: 0x000328D8
		public IList<string> OutputText(List<MailMergeCode> Codes, string Template, eMailMergeDocumentOutputFormat outputFormat)
		{
			IMailMergeOutputManager mailMergeOutputManager = MailMergeOutputFactory.GetMailMergeOutputManager(outputFormat, new MailMergeOutputOperationContext
			{
				WhoAmI = this.OpContext.WhoAmI,
				CodeLists = new List<IList<MailMergeCode>>
				{
					Codes
				},
				Template = new MailMergeTemplate
				{
					Template = Template
				}
			});
			return (IList<string>)mailMergeOutputManager.OutputMailMergeCodes();
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0003473C File Offset: 0x0003293C
		private void FixDynamicDataCaptions(IList<DynamicData> dynamicDataItems, IList<int> cidsToUseAltControlCaption)
		{
			bool flag = dynamicDataItems == null || cidsToUseAltControlCaption == null;
			if (!flag)
			{
				IEnumerable<DynamicData> enumerable = from g in dynamicDataItems
				where cidsToUseAltControlCaption.Contains(g.Field.ControlId)
				select g;
				List<DynamicData> list = new List<DynamicData>();
				List<DynamicData> list2 = new List<DynamicData>();
				foreach (DynamicData dynamicData in enumerable)
				{
					list.Add(dynamicData);
					list2.Add(this.GetDataCopyWithModifiedCaption(dynamicData, dynamicData.Field.Setting4String));
				}
				foreach (DynamicData item in list)
				{
					dynamicDataItems.Remove(item);
				}
				foreach (DynamicData item2 in list2)
				{
					dynamicDataItems.Add(item2);
				}
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00034878 File Offset: 0x00032A78
		private void FixDynamicDataCaptions(IList<AccommodationData> accommodationDataItems, IList<int> cidsToUseAltControlCaption)
		{
			bool flag = accommodationDataItems == null || cidsToUseAltControlCaption == null;
			if (!flag)
			{
				IEnumerable<AccommodationData> enumerable = from g in accommodationDataItems
				where cidsToUseAltControlCaption.Contains(g.Data.Field.ControlId)
				select g;
				List<AccommodationData> list = new List<AccommodationData>();
				List<AccommodationData> list2 = new List<AccommodationData>();
				foreach (AccommodationData accommodationData in enumerable)
				{
					list.Add(accommodationData);
					list2.Add(this.GetDataCopyWithModifiedCaption(accommodationData, accommodationData.Data.Field.Setting4String));
				}
				foreach (AccommodationData item in list)
				{
					accommodationDataItems.Remove(item);
				}
				foreach (AccommodationData item2 in list2)
				{
					accommodationDataItems.Add(item2);
				}
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x000349BC File Offset: 0x00032BBC
		private DynamicData GetDataCopyWithModifiedCaption(DynamicData dataItem, string newCaption)
		{
			DynamicData dynamicData = dataItem.Clone();
			dynamicData.Field.ControlCaption = newCaption;
			return dynamicData;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000349E4 File Offset: 0x00032BE4
		private AccommodationData GetDataCopyWithModifiedCaption(AccommodationData accDataItem, string newCaption)
		{
			AccommodationData accommodationData = accDataItem.Clone();
			accommodationData.Data.Field.ControlCaption = newCaption;
			return accommodationData;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00034A10 File Offset: 0x00032C10
		public IList<MailMergeCode> LookupCodeValues(MailMergeContextWithCustomDictionary ContextWithCustomDictionary, IList<MailMergeCode> Codes)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			MailMergeContext context = ContextWithCustomDictionary.Context;
			for (int i = 0; i < Codes.Count; i++)
			{
				MailMergingManager.MailMergeCodeWrapper mailMergeCodeWrapper = new MailMergingManager.MailMergeCodeWrapper(Codes[i]);
				try
				{
					bool flag = mailMergeCodeWrapper.Item.Args == null;
					if (flag)
					{
						mailMergeCodeWrapper.Item.Args = new Dictionary<string, string>();
					}
					bool flag2 = false;
					bool flag3 = mailMergeCodeWrapper.Item.Args.Count > 0;
					if (flag3)
					{
						Dictionary<string, string> args = mailMergeCodeWrapper.Item.Args;
						bool flag4 = args.ContainsKey("rid");
						if (flag4)
						{
							flag2 = this.MailMergeReportInfo(ref mailMergeCodeWrapper, ContextWithCustomDictionary, args, ref dictionary);
						}
						else
						{
							bool flag5 = args.ContainsKey("websettingid");
							if (flag5)
							{
								flag2 = this.MailMergeWebSettingInfo(ref mailMergeCodeWrapper, ContextWithCustomDictionary, args, ref dictionary);
							}
						}
					}
					List<int> cids = new List<int>();
					List<int> list = new List<int>();
					bool flag6 = !flag2;
					if (flag6)
					{
						string text = mailMergeCodeWrapper.Name;
						bool flag7 = !string.IsNullOrEmpty(mailMergeCodeWrapper.Name) && mailMergeCodeWrapper.Name[0] == '.';
						if (flag7)
						{
							text = text.Substring(1);
						}
						bool flag8 = ContextWithCustomDictionary.CustomDictionary != null && ContextWithCustomDictionary.CustomDictionary.Args != null;
						if (flag8)
						{
							flag2 = this.MailMergeCustomDictionary(ref mailMergeCodeWrapper, ContextWithCustomDictionary);
						}
						bool flag9 = !flag2;
						if (flag9)
						{
							bool flag10 = text.Length > 0 && char.IsDigit(text[0]);
							if (flag10)
							{
								bool flag11 = text.Contains("x");
								char c;
								if (flag11)
								{
									c = 'x';
									mailMergeCodeWrapper.Item.ValueFormat = new MailMergeValueFormat
									{
										ValueFormatType = eValueFormatType.BulletedList
									};
								}
								else
								{
									bool flag12 = text.Contains("y");
									if (flag12)
									{
										c = 'y';
										mailMergeCodeWrapper.Item.ValueFormat = new MailMergeValueFormat
										{
											ValueFormatType = eValueFormatType.CommaSeparatedList
										};
									}
									else
									{
										c = 'y';
									}
								}
								bool flag13 = text.Contains("x") || text.Contains("y");
								if (flag13)
								{
									string[] array = text.Split(new char[]
									{
										c
									});
									foreach (string text2 in array)
									{
										bool flag14 = text2.Length < 1;
										if (!flag14)
										{
											bool flag15 = text2.Length > 1 && text2[text2.Length - 1] == '!';
											bool flag16 = flag15;
											if (flag16)
											{
												int num;
												bool flag17 = int.TryParse(text2.Substring(0, text2.Length - 1), out num) && num > 0;
												if (flag17)
												{
													cids.Add(num);
													list.Add(num);
												}
											}
											else
											{
												int num;
												bool flag18 = int.TryParse(text2, out num) && num > 0;
												if (flag18)
												{
													cids.Add(num);
												}
											}
										}
									}
								}
							}
						}
						int personId = mailMergeCodeWrapper.GetPersonId(context);
						bool flag19 = !flag2;
						if (flag19)
						{
							bool flag20 = cids.Count < 1;
							if (flag20)
							{
								bool flag21 = text.Length > 1 && text.EndsWith("!");
								string s = flag21 ? text.Substring(0, text.Length - 1) : text;
								bool flag22 = this.IsInteger(s);
								if (flag22)
								{
									int num2;
									bool flag23 = int.TryParse(s, out num2) && num2 > 0;
									if (flag23)
									{
										cids.Add(num2);
										bool flag24 = flag21;
										if (flag24)
										{
											list.Add(num2);
										}
									}
								}
							}
							bool flag25 = cids.Count > 0;
							if (flag25)
							{
								bool flag26 = cids.Count == 1 && mailMergeCodeWrapper.Item.Args.ContainsKey("checkbox");
								bool flag27 = personId > 0;
								if (flag27)
								{
									List<DynamicData> perStudentData = this.GetPerStudentData(context, dictionary, mailMergeCodeWrapper);
									List<DynamicData> list2 = perStudentData.FindAll((DynamicData pd) => cids.Contains(pd.Field.ControlId));
									bool flag28 = list2.Count > 0;
									if (flag28)
									{
										this.FixDynamicDataCaptions(list2, list);
										bool flag29 = flag26;
										if (flag29)
										{
											MailMergeCode item = mailMergeCodeWrapper.Item;
											MailMergeCheckedItem mailMergeCheckedItem = new MailMergeCheckedItem();
											mailMergeCheckedItem.Title = list2[0].Field.GetCaptionForDisplay();
											mailMergeCheckedItem.IsChecked = (list2[0].Value is bool && (bool)list2[0].Value);
											MailMergeCheckedItem mailMergeCheckedItem2 = mailMergeCheckedItem;
											Dictionary<string, string> args2 = mailMergeCodeWrapper.Item.Args;
											mailMergeCheckedItem2.HideCheckboxTitle = (args2 != null && args2.ContainsTrueArg("hidetitle"));
											item.SetMailMergeValue(mailMergeCheckedItem);
										}
										else
										{
											mailMergeCodeWrapper.Item.SetMailMergeValue(list2);
										}
										flag2 = true;
									}
									List<AccommodationData> allAccommodations = this.GetAllAccommodations(context, dictionary, mailMergeCodeWrapper);
									List<AccommodationData> list3 = (allAccommodations != null) ? allAccommodations.FindAll((AccommodationData ad) => cids.Contains(ad.Data.Field.ControlId)) : null;
									bool flag30 = list3 != null && list3.Count > 0;
									if (flag30)
									{
										this.FixDynamicDataCaptions(list3, list);
										List<string> list4 = list3.ConvertAll<string>((AccommodationData g) => g.Data.GetString());
										bool flag31 = list4.Count > 0 && flag26;
										if (flag31)
										{
											string title = list4[0] ?? "";
											MailMergeCode item2 = mailMergeCodeWrapper.Item;
											MailMergeCheckedItem mailMergeCheckedItem3 = new MailMergeCheckedItem();
											mailMergeCheckedItem3.Title = title;
											mailMergeCheckedItem3.IsChecked = true;
											Dictionary<string, string> args3 = mailMergeCodeWrapper.Item.Args;
											mailMergeCheckedItem3.HideCheckboxTitle = (args3 != null && args3.ContainsTrueArg("hidetitle"));
											item2.SetMailMergeValue(mailMergeCheckedItem3);
										}
										else
										{
											mailMergeCodeWrapper.Item.SetMailMergeValue(list4);
										}
										bool flag32 = mailMergeCodeWrapper.Item.ValueFormat == null;
										if (flag32)
										{
											mailMergeCodeWrapper.Item.ValueFormat = new MailMergeValueFormat
											{
												ValueFormatType = eValueFormatType.CommaSeparatedList
											};
										}
										flag2 = true;
									}
									bool flag33 = context.PerDateId > 0;
									if (flag33)
									{
										List<DynamicData> perDateData = this.GetPerDateData(context, dictionary, mailMergeCodeWrapper);
										List<DynamicData> list5 = perDateData.FindAll((DynamicData pd) => cids.Contains(pd.Field.ControlId));
										bool flag34 = list5.Count > 0;
										if (flag34)
										{
											this.FixDynamicDataCaptions(list5, list);
											bool flag35 = flag26;
											if (flag35)
											{
												MailMergeCode item3 = mailMergeCodeWrapper.Item;
												MailMergeCheckedItem mailMergeCheckedItem = new MailMergeCheckedItem();
												mailMergeCheckedItem.Title = list5[0].Field.GetCaptionForDisplay();
												mailMergeCheckedItem.IsChecked = (list5[0].Value is bool && (bool)list5[0].Value);
												MailMergeCheckedItem mailMergeCheckedItem4 = mailMergeCheckedItem;
												Dictionary<string, string> args4 = mailMergeCodeWrapper.Item.Args;
												mailMergeCheckedItem4.HideCheckboxTitle = (args4 != null && args4.ContainsTrueArg("hidetitle"));
												item3.SetMailMergeValue(mailMergeCheckedItem);
											}
											else
											{
												mailMergeCodeWrapper.Item.SetMailMergeValue(list5);
											}
											flag2 = true;
										}
									}
									bool flag36 = context.AppointmentId > 0;
									if (flag36)
									{
										List<DynamicData> perAppData = this.GetPerAppData(context, dictionary, mailMergeCodeWrapper);
										List<DynamicData> list6 = perAppData.FindAll((DynamicData pd) => cids.Contains(pd.Field.ControlId));
										bool flag37 = list6.Count > 0;
										if (flag37)
										{
											this.FixDynamicDataCaptions(list6, list);
											bool flag38 = flag26;
											if (flag38)
											{
												MailMergeCode item4 = mailMergeCodeWrapper.Item;
												MailMergeCheckedItem mailMergeCheckedItem = new MailMergeCheckedItem();
												mailMergeCheckedItem.Title = list6[0].Field.GetCaptionForDisplay();
												mailMergeCheckedItem.IsChecked = (list6[0].Value is bool && (bool)list6[0].Value);
												MailMergeCheckedItem mailMergeCheckedItem5 = mailMergeCheckedItem;
												Dictionary<string, string> args5 = mailMergeCodeWrapper.Item.Args;
												mailMergeCheckedItem5.HideCheckboxTitle = (args5 != null && args5.ContainsTrueArg("hidetitle"));
												item4.SetMailMergeValue(mailMergeCheckedItem);
											}
											else
											{
												mailMergeCodeWrapper.Item.SetMailMergeValue(list6);
											}
											flag2 = true;
										}
									}
									bool flag39 = context.PeopleOnlineFormId > 0;
									if (flag39)
									{
										List<DynamicData> perOnlineFormData = this.GetPerOnlineFormData(context, dictionary, mailMergeCodeWrapper);
										List<DynamicData> list7 = (from g in perOnlineFormData
										where cids.Contains(g.Field.ControlId)
										select g).ToList<DynamicData>();
										bool flag40 = list7.Count > 0;
										if (flag40)
										{
											this.FixDynamicDataCaptions(list7, list);
											bool flag41 = flag26;
											if (flag41)
											{
												MailMergeCode item5 = mailMergeCodeWrapper.Item;
												MailMergeCheckedItem mailMergeCheckedItem = new MailMergeCheckedItem();
												mailMergeCheckedItem.Title = list7[0].Field.GetCaptionForDisplay();
												mailMergeCheckedItem.IsChecked = (list7[0].Value is bool && (bool)list7[0].Value);
												MailMergeCheckedItem mailMergeCheckedItem6 = mailMergeCheckedItem;
												Dictionary<string, string> args6 = mailMergeCodeWrapper.Item.Args;
												mailMergeCheckedItem6.HideCheckboxTitle = (args6 != null && args6.ContainsTrueArg("hidetitle"));
												item5.SetMailMergeValue(mailMergeCheckedItem);
											}
											else
											{
												mailMergeCodeWrapper.Item.SetMailMergeValue(list7);
											}
											flag2 = true;
										}
									}
								}
								bool flag42 = flag26 && !flag2;
								if (flag42)
								{
									string key = "Field_" + cids[0].ToString();
									bool flag43 = !dictionary.ContainsKey(key);
									if (flag43)
									{
										dictionary.Add(key, this.dynamicFieldManager.LoadFieldByControlId(cids[0]));
									}
									DynamicField dynamicField = (DynamicField)dictionary[key];
									bool flag44 = dynamicField != null;
									if (flag44)
									{
										mailMergeCodeWrapper.Item.SetMailMergeValue(new MailMergeCheckedItem
										{
											Title = ((dynamicField == null) ? "" : dynamicField.GetCaptionForDisplay()),
											IsChecked = false
										});
										flag2 = true;
									}
								}
							}
							bool flag45 = !flag2;
							if (flag45)
							{
								bool flag46 = !flag2 && personId > 0;
								if (flag46)
								{
									flag2 = this.TryToMailMergeBaseBuiltInCodesForStudent(context, dictionary, mailMergeCodeWrapper);
								}
								bool flag47 = !flag2;
								if (flag47)
								{
									flag2 = this.TryToMailMergeBaseBuiltInCodes(ContextWithCustomDictionary.Context, dictionary, mailMergeCodeWrapper);
								}
								bool flag48 = !flag2 && context.ExamId > 0;
								if (flag48)
								{
									flag2 = this.TryToMailMergeExamInfo(context, dictionary, mailMergeCodeWrapper);
								}
								bool flag49 = !flag2 && context.AppointmentId > 0;
								if (flag49)
								{
									flag2 = this.TryToMailMergeAppointment(context, dictionary, mailMergeCodeWrapper);
								}
								bool flag50 = !flag2 && context.AppointmentId > 0;
								if (flag50)
								{
									flag2 = this.TryToMailMergeTest(context, dictionary, mailMergeCodeWrapper);
								}
								IList<int> contextLuCourseIds = this.GetContextLuCourseIds(context, dictionary);
								bool flag51 = !flag2 && contextLuCourseIds.Count > 0;
								if (flag51)
								{
									flag2 = this.TryToMailMergeCourseInfo(context, dictionary, mailMergeCodeWrapper);
								}
								bool flag52 = !flag2 && personId > 0;
								if (flag52)
								{
									flag2 = this.TryToMailMergeBaseBulitInAccommodationRelatedCodes(context, dictionary, mailMergeCodeWrapper);
								}
								bool flag53 = !flag2;
								if (flag53)
								{
									List<DynamicData> accommodationTemplateData = this.GetAccommodationTemplateData(context, dictionary, mailMergeCodeWrapper);
									bool flag54 = accommodationTemplateData != null;
									if (flag54)
									{
										List<DynamicData> list8 = accommodationTemplateData.FindAll((DynamicData pd) => cids.Contains(pd.Field.ControlId));
										bool flag55 = list8.Count > 0;
										if (flag55)
										{
											mailMergeCodeWrapper.Item.SetMailMergeValue(list8);
											flag2 = true;
										}
									}
								}
								bool flag56 = !flag2 && context.InstructorId > 0;
								if (flag56)
								{
								}
								bool flag57 = !flag2 && context.AppointmentId > 0 && cids != null && cids.Count > 0;
								if (flag57)
								{
									IList<DynamicData> instructorFormData = this.GetInstructorFormData(context, dictionary);
									bool flag58 = instructorFormData != null;
									if (flag58)
									{
										List<DynamicData> list9 = (from g in instructorFormData
										where cids.Contains(g.Field.ControlId)
										select g).ToList<DynamicData>();
										bool flag59 = list9.Count > 0;
										if (flag59)
										{
											mailMergeCodeWrapper.Item.SetMailMergeValue(list9);
											bool flag60 = mailMergeCodeWrapper.Item.ValueFormat == null;
											if (flag60)
											{
												mailMergeCodeWrapper.Item.ValueFormat = new MailMergeValueFormat
												{
													ValueFormatType = eValueFormatType.CommaSeparatedList
												};
											}
											flag2 = true;
										}
									}
								}
								bool flag61 = !flag2 && ContextWithCustomDictionary.Context.ServiceProviderId > 0;
								if (flag61)
								{
									flag2 = this.TryToMailMergeServiceProviderInfo(context, dictionary, mailMergeCodeWrapper);
								}
								bool flag62 = !flag2 && !string.IsNullOrEmpty(ContextWithCustomDictionary.Context.ProductUniqueId);
								if (flag62)
								{
									flag2 = this.TryToMailMergeProduct(context, dictionary, mailMergeCodeWrapper);
								}
								bool flag63 = !flag2 && ContextWithCustomDictionary.Context.LoanId > 0;
								if (flag63)
								{
									flag2 = this.TryToMailMergeProductLoan(context, dictionary, mailMergeCodeWrapper);
								}
								bool flag64 = !flag2 && ContextWithCustomDictionary.Context.AlternateFormatRequestId > 0;
								if (flag64)
								{
									flag2 = this.TryToMailMergeAlternateFormatRequest(ContextWithCustomDictionary.Context, dictionary, mailMergeCodeWrapper);
								}
								bool flag65 = !flag2 && ContextWithCustomDictionary.Context.AlternateFormatMediaContentId != Guid.Empty;
								if (flag65)
								{
									flag2 = this.TryToMailMergeAlternateFormatMediaContent(context, dictionary, mailMergeCodeWrapper);
								}
								bool flag66 = !flag2 && ContextWithCustomDictionary.Context.AlternateFormatPublisherId > 0;
								if (flag66)
								{
									flag2 = this.TryToMailMergeAlternateFormatPublisher(context, dictionary, mailMergeCodeWrapper);
								}
								bool flag67 = !flag2 && ContextWithCustomDictionary.Context.AlternateFormatVendorId > 0;
								if (flag67)
								{
									flag2 = this.TryToMailMergeAlternateFormatVendor(context, dictionary, mailMergeCodeWrapper);
								}
							}
						}
					}
					bool flag68 = flag2;
					if (flag68)
					{
						bool flag69 = mailMergeCodeWrapper.Item.ValueFormat == null;
						if (flag69)
						{
							bool flag70 = mailMergeCodeWrapper.Item.ValueFormat == null;
							if (flag70)
							{
								mailMergeCodeWrapper.Item.ValueFormat = new MailMergeValueFormat
								{
									ValueFormatType = eValueFormatType.DefaultToStringFormat
								};
							}
						}
						MailMergeCode item6 = mailMergeCodeWrapper.Item;
						Dictionary<string, string> dictionary2 = ((item6 != null) ? item6.Args : null) ?? new Dictionary<string, string>();
						bool flag71 = dictionary2.ContainsKey("lookupvaluewebsettings");
						if (flag71)
						{
							string text3 = this.LookupValueWebSettings(dictionary2, mailMergeCodeWrapper.Item.GetFirstMailMergeValueAsString() ?? "");
							bool flag72 = text3 != null;
							if (flag72)
							{
								mailMergeCodeWrapper.Item.SetMailMergeValue(text3);
							}
						}
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("MailMergingManager:LookupCodeValues:Code={0}:Error={1}", (mailMergeCodeWrapper == null) ? "NULL" : (mailMergeCodeWrapper.Item.OriginalCode ?? "NULL."), ex.ToString());
				}
			}
			return Codes;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00035850 File Offset: 0x00033A50
		private string LookupValueWebSettings(IDictionary<string, string> codeArgs, string valueToLookup)
		{
			string s = (codeArgs["lookupvaluewebsettings"] ?? "").Trim();
			int num;
			bool flag = !int.TryParse(s, out num);
			if (flag)
			{
				num = 0;
			}
			bool flag2 = num <= 0;
			string result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				string text = codeArgs.ContainsKey("websettingcontext") ? codeArgs["websettingcontext"] : "ClockWork";
				bool flag3 = string.IsNullOrEmpty(text);
				if (flag3)
				{
					text = "ClockWork";
				}
				IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext, text));
				string value = webSettingManager.GetSettingValue<string>(num) ?? "";
				try
				{
					List<MailMergingManager.LookupValueNameValuePair> source = JsonConvert.DeserializeObject<List<MailMergingManager.LookupValueNameValuePair>>(value);
					MailMergingManager.LookupValueNameValuePair lookupValueNameValuePair = source.FirstOrDefault((MailMergingManager.LookupValueNameValuePair g) => g.Name == valueToLookup) ?? source.FirstOrDefault((MailMergingManager.LookupValueNameValuePair g) => g.Name.Equals(valueToLookup, StringComparison.OrdinalIgnoreCase));
					bool flag4 = lookupValueNameValuePair == null;
					if (flag4)
					{
						lookupValueNameValuePair = source.FirstOrDefault((MailMergingManager.LookupValueNameValuePair g) => g.Name == "default");
					}
					string text2 = (lookupValueNameValuePair != null) ? lookupValueNameValuePair.Value : null;
					bool flag5 = text2 == null;
					if (flag5)
					{
						result = null;
					}
					else
					{
						result = text2.Replace("\r\n", "\n").Replace("\n", "\r\n");
					}
				}
				catch
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000359CC File Offset: 0x00033BCC
		public IList<string> MailMergeAndReturnCodesWithValues(MailMergeContextWithCustomDictionary ContextWithCustomDictionary, string text, eMailMergeDocumentOutputFormat outputFormat, out IList<MailMergeCode> CodesWithValues)
		{
			CodesWithValues = this.ExtractCodes(text);
			CodesWithValues = this.LookupCodeValues(ContextWithCustomDictionary, CodesWithValues);
			return this.OutputText(CodesWithValues.ToList<MailMergeCode>(), text, outputFormat);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00035A08 File Offset: 0x00033C08
		public IList<string> MailMerge(MailMergeContextWithCustomDictionary ContextWithCustomDictionary, string text, eMailMergeDocumentOutputFormat outputFormat)
		{
			IList<MailMergeCode> list = this.ExtractCodes(text);
			list = this.LookupCodeValues(ContextWithCustomDictionary, list);
			return this.OutputText(list.ToList<MailMergeCode>(), text, outputFormat);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00035A3C File Offset: 0x00033C3C
		public string GetMailMergeCodeDefinitionsForDisplay()
		{
			Array values = Enum.GetValues(typeof(eMailMergeCodeGroup));
			List<eMailMergeCodeGroup> list = values.Cast<eMailMergeCodeGroup>().ToList<eMailMergeCodeGroup>();
			list.Sort((eMailMergeCodeGroup g1, eMailMergeCodeGroup g2) => g1.ToString().CompareTo(g2.ToString()));
			Dictionary<eMailMergeCodeGroup, List<eMailMergeCode>> dictionary = list.ToDictionary((eMailMergeCodeGroup cg) => cg, (eMailMergeCodeGroup cg) => new List<eMailMergeCode>());
			Array values2 = Enum.GetValues(typeof(eMailMergeCode));
			foreach (object obj in values2)
			{
				eMailMergeCode eMailMergeCode = (eMailMergeCode)obj;
				MailMergeCodeAttribute info = eMailMergeCode.GetInfo();
				bool isHidden = info.IsHidden;
				if (!isHidden)
				{
					eMailMergeCodeGroup group = info.Group;
					bool flag = dictionary.ContainsKey(group);
					if (flag)
					{
						dictionary[group].Add(eMailMergeCode);
					}
				}
			}
			foreach (KeyValuePair<eMailMergeCodeGroup, List<eMailMergeCode>> keyValuePair in dictionary)
			{
				keyValuePair.Value.Sort((eMailMergeCode m1, eMailMergeCode m2) => m1.ToString().CompareTo(m2.ToString()));
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<eMailMergeCodeGroup, List<eMailMergeCode>> keyValuePair2 in dictionary)
			{
				bool flag2 = keyValuePair2.Value.Count <= 0;
				if (!flag2)
				{
					string arg = keyValuePair2.Key.ToString();
					stringBuilder.AppendFormat("<h1>{0}</h2>", arg);
					foreach (eMailMergeCode code in keyValuePair2.Value)
					{
						stringBuilder.Append(code.GetHtmlDisplayString());
						stringBuilder.Append("<br /><br />");
					}
					stringBuilder.Append("<br />");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00035CD8 File Offset: 0x00033ED8
		public IList<string> TestAllMailMergeCodes(MailMergeContext StartingContext, string TemplateHeaderText, IList<string> CustomMailMergeCodes, out IList<MailMergeCode> CodesWithValues)
		{
			IPeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
			bool flag = !peopleGroupManager.IsAdmin(this.OpContext.WhoAmI);
			if (flag)
			{
				throw new PermissionDeniedException("Not admin");
			}
			bool flag2 = StartingContext == null;
			if (flag2)
			{
				StartingContext = new MailMergeContext();
			}
			MailMergeContext context = StartingContext;
			string text = this.GenerateSampleTemplate(TemplateHeaderText ?? "", CustomMailMergeCodes);
			return this.MailMergeAndReturnCodesWithValues(new MailMergeContextWithCustomDictionary
			{
				Context = context,
				CustomDictionary = new MailMergeCustomDictionary
				{
					Args = new Dictionary<string, string>()
				}
			}, text, eMailMergeDocumentOutputFormat.Text, out CodesWithValues);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00035D70 File Offset: 0x00033F70
		private string GenerateSampleTemplate(string header, IList<string> customMailMergeCodes)
		{
			StringBuilder stringBuilder = new StringBuilder(header);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			eMailMergeCode[] source = (eMailMergeCode[])Enum.GetValues(typeof(eMailMergeCode));
			List<MailMergingManager.MailMergeCodeDefinition> list = source.ToList<eMailMergeCode>().ConvertAll<MailMergingManager.MailMergeCodeDefinition>((eMailMergeCode g) => new MailMergingManager.MailMergeCodeDefinition(g)).ToList<MailMergingManager.MailMergeCodeDefinition>();
			list.Sort(delegate(MailMergingManager.MailMergeCodeDefinition g1, MailMergingManager.MailMergeCodeDefinition g2)
			{
				int num = g1.GroupTitle.CompareTo(g2.GroupTitle);
				return (num != 0) ? num : g1.CodeTitle.CompareTo(g2.CodeTitle);
			});
			string text = "";
			bool flag = customMailMergeCodes != null;
			if (flag)
			{
				text = "Custom mail merge codes";
				stringBuilder.AppendLine(text);
				foreach (string text2 in customMailMergeCodes)
				{
					stringBuilder.AppendLine(string.Format("  {0}=#<{1}>#", text2, text2));
				}
			}
			foreach (MailMergingManager.MailMergeCodeDefinition mailMergeCodeDefinition in list)
			{
				string groupTitle = mailMergeCodeDefinition.GroupTitle;
				bool flag2 = groupTitle != text;
				if (flag2)
				{
					stringBuilder.AppendLine(groupTitle);
					text = groupTitle;
				}
				stringBuilder.AppendLine(string.Format("  {0}=#<{1}>#", mailMergeCodeDefinition.CodeTitle, (mailMergeCodeDefinition.Attribute == null || mailMergeCodeDefinition.Attribute.CodeText == null) ? "****NULL*****" : mailMergeCodeDefinition.Attribute.CodeText));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00035F20 File Offset: 0x00034120
		public IList<MailMergeContextWithCustomDictionary> ExtractMailMergeContextFromTable(DataTable t)
		{
			List<MailMergeContextWithCustomDictionary> list = new List<MailMergeContextWithCustomDictionary>();
			bool flag = t == null || t.Rows.Count < 1;
			IList<MailMergeContextWithCustomDictionary> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				Type typeFromHandle = typeof(MailMergeContext);
				PropertyInfo[] source = (from p in typeFromHandle.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				where p.CanRead && p.CanWrite && p.PropertyType == typeof(int)
				select p).ToArray<PropertyInfo>();
				List<PropertyInfo> list2 = (from prop in source
				where t.Columns.Contains(prop.Name)
				select prop).ToList<PropertyInfo>();
				foreach (object obj in t.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					MailMergeContext mailMergeContext = new MailMergeContext();
					foreach (PropertyInfo propertyInfo in list2)
					{
						string text = (dataRow[propertyInfo.Name] is DBNull) ? "" : dataRow[propertyInfo.Name].ToString().Trim();
						int num;
						bool flag2 = text.Length > 0 && int.TryParse(text, out num) && num > 0;
						if (flag2)
						{
							propertyInfo.SetValue(mailMergeContext, num, null);
						}
					}
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					MailMergeCustomDictionary customDictionary = new MailMergeCustomDictionary
					{
						Args = dictionary
					};
					for (int i = 0; i < t.Columns.Count; i++)
					{
						string colName = t.Columns[i].ColumnName;
						bool flag3 = list2.FirstOrDefault((PropertyInfo g) => g.Name.Equals(colName, StringComparison.OrdinalIgnoreCase)) == null;
						if (flag3)
						{
							string text2 = (dataRow[i] is DBNull) ? "" : dataRow[i].ToString();
							bool flag4 = text2.Length > 0 && !dictionary.ContainsKey(colName);
							if (flag4)
							{
								dictionary.Add(colName, text2);
							}
						}
					}
					list.Add(new MailMergeContextWithCustomDictionary
					{
						Context = mailMergeContext,
						CustomDictionary = customDictionary
					});
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000361F4 File Offset: 0x000343F4
		public IList<string> TestAllMailMergeCodes(string StartingContext, string TemplateHeaderText, IList<string> CustomMailMergeCodes, out IList<MailMergeCode> CodesWithValues)
		{
			MailMergeContext mailMergeContextFromString = (StartingContext ?? "").GetMailMergeContextFromString();
			return this.TestAllMailMergeCodes(mailMergeContextFromString, TemplateHeaderText, CustomMailMergeCodes, out CodesWithValues);
		}

		// Token: 0x0400015D RID: 349
		private DynamicDataManager _dynamicDataManager;

		// Token: 0x0400015E RID: 350
		private DynamicFieldManager _dynamicFieldManager;

		// Token: 0x0400015F RID: 351
		private IAppointmentManager apm;

		// Token: 0x04000160 RID: 352
		private ClassTestDefinitionManager _classTestManager;

		// Token: 0x04000161 RID: 353
		private TestBookingManager tbm;

		// Token: 0x04000162 RID: 354
		private IReportManager rm;

		// Token: 0x04000163 RID: 355
		private ILookupCourseManager lcm;

		// Token: 0x04000164 RID: 356
		private IStudentCommonInfoManager scm;

		// Token: 0x04000165 RID: 357
		private ISessionManager snm;

		// Token: 0x04000166 RID: 358
		private IOldUserSettingManager ousm = null;

		// Token: 0x04000167 RID: 359
		private PeopleManager pm;

		// Token: 0x04000168 RID: 360
		private OldUserSettingManager om;

		// Token: 0x04000169 RID: 361
		private IStaffCommonInfoManager scim;

		// Token: 0x0400016B RID: 363
		private IStudentClassTestInfoManager _studentClassTestInfoManager;

		// Token: 0x0400016C RID: 364
		private IAccommodationsManager am;

		// Token: 0x02000291 RID: 657
		internal class MailMergeCodeWrapper : WrapperBase<MailMergeCode>
		{
			// Token: 0x06001456 RID: 5206 RVA: 0x00083411 File Offset: 0x00081611
			public MailMergeCodeWrapper()
			{
			}

			// Token: 0x06001457 RID: 5207 RVA: 0x0008341C File Offset: 0x0008161C
			public MailMergeCodeWrapper(MailMergeCode code) : base(code)
			{
				int altPersonIdIndex;
				this.Name = MailMergingManager.MailMergeCodeWrapper.ExtractAltPersonIdFromMailMergeCode(code.Name, out altPersonIdIndex);
				this.AltPersonIdIndex = altPersonIdIndex;
			}

			// Token: 0x06001458 RID: 5208 RVA: 0x00083450 File Offset: 0x00081650
			private static string ExtractAltPersonIdFromMailMergeCode(string codeName, out int altPersonIdIndex)
			{
				bool flag = codeName.Length >= 5 && codeName.StartsWith("alt") && (codeName[3] == '_' || codeName[4] == '_');
				string result;
				if (flag)
				{
					bool flag2 = codeName[3] == '_';
					if (flag2)
					{
						altPersonIdIndex = 1;
						result = codeName.Substring(4);
					}
					else
					{
						int.TryParse(codeName[3].ToString(), out altPersonIdIndex);
						result = codeName.Substring(5);
					}
				}
				else
				{
					altPersonIdIndex = 0;
					result = codeName;
				}
				return result;
			}

			// Token: 0x1700027F RID: 639
			// (get) Token: 0x06001459 RID: 5209 RVA: 0x000834DA File Offset: 0x000816DA
			// (set) Token: 0x0600145A RID: 5210 RVA: 0x000834E2 File Offset: 0x000816E2
			public int AltPersonIdIndex { get; set; }

			// Token: 0x17000280 RID: 640
			// (get) Token: 0x0600145B RID: 5211 RVA: 0x000834EB File Offset: 0x000816EB
			// (set) Token: 0x0600145C RID: 5212 RVA: 0x000834F3 File Offset: 0x000816F3
			public string Name { get; set; }

			// Token: 0x0600145D RID: 5213 RVA: 0x000834FC File Offset: 0x000816FC
			public int GetPersonId(MailMergeContext Context)
			{
				int altPersonIdIndex = this.AltPersonIdIndex;
				int num = altPersonIdIndex;
				int result;
				if (num != 1)
				{
					result = Context.PersonId;
				}
				else
				{
					result = Context.AltPersonId;
				}
				return result;
			}
		}

		// Token: 0x02000292 RID: 658
		internal class LookupValueNameValuePair
		{
			// Token: 0x17000281 RID: 641
			// (get) Token: 0x0600145E RID: 5214 RVA: 0x0008352C File Offset: 0x0008172C
			// (set) Token: 0x0600145F RID: 5215 RVA: 0x00083534 File Offset: 0x00081734
			public string Name { get; set; }

			// Token: 0x17000282 RID: 642
			// (get) Token: 0x06001460 RID: 5216 RVA: 0x0008353D File Offset: 0x0008173D
			// (set) Token: 0x06001461 RID: 5217 RVA: 0x00083545 File Offset: 0x00081745
			public string Value { get; set; }
		}

		// Token: 0x02000293 RID: 659
		internal class MailMergeCodeDefinition
		{
			// Token: 0x06001463 RID: 5219 RVA: 0x0000672B File Offset: 0x0000492B
			public MailMergeCodeDefinition()
			{
			}

			// Token: 0x06001464 RID: 5220 RVA: 0x0008354E File Offset: 0x0008174E
			public MailMergeCodeDefinition(eMailMergeCode code)
			{
				this.Code = code;
				this.Attribute = code.GetAttribute<MailMergeCodeAttribute>();
			}

			// Token: 0x17000283 RID: 643
			// (get) Token: 0x06001465 RID: 5221 RVA: 0x00083572 File Offset: 0x00081772
			// (set) Token: 0x06001466 RID: 5222 RVA: 0x0008357A File Offset: 0x0008177A
			public eMailMergeCode Code { get; set; }

			// Token: 0x17000284 RID: 644
			// (get) Token: 0x06001467 RID: 5223 RVA: 0x00083583 File Offset: 0x00081783
			// (set) Token: 0x06001468 RID: 5224 RVA: 0x0008358B File Offset: 0x0008178B
			public MailMergeCodeAttribute Attribute { get; set; }

			// Token: 0x17000285 RID: 645
			// (get) Token: 0x06001469 RID: 5225 RVA: 0x00083594 File Offset: 0x00081794
			public string GroupTitle
			{
				get
				{
					return (this.Attribute == null) ? "" : this.Attribute.Group.ToString();
				}
			}

			// Token: 0x17000286 RID: 646
			// (get) Token: 0x0600146A RID: 5226 RVA: 0x000835D0 File Offset: 0x000817D0
			public string CodeTitle
			{
				get
				{
					return this.Code.ToString();
				}
			}
		}
	}
}
