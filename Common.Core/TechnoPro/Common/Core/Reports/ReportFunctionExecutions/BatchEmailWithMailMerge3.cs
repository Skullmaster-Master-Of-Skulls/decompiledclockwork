using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using ClockWorkLogger;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.AppointmentsPointOfContact;
using TechnoPro.Common.Core.Emailing;
using TechnoPro.Common.Core.MailMerging;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.Templates;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsPointOfContact;
using TechnoPro.Common.ICore.Emailing;
using TechnoPro.Common.ICore.MailMerging;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.ICore.Templates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;
using TechnoPro.Common.Public.Entities.Emailing;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.DataTableMailMerging;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.Templates;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200005F RID: 95
	public class BatchEmailWithMailMerge3 : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x0000672B File Offset: 0x0000492B
		public BatchEmailWithMailMerge3()
		{
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00015100 File Offset: 0x00013300
		public BatchEmailWithMailMerge3(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00015112 File Offset: 0x00013312
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x0001511A File Offset: 0x0001331A
		public OperationContext OpContext { get; set; }

		// Token: 0x06000400 RID: 1024 RVA: 0x00015124 File Offset: 0x00013324
		private static bool IsEmailValid(string email)
		{
			bool flag = string.IsNullOrEmpty(email);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Regex regex = new Regex("(?<user>[^@]+)@(?<host>.+)");
				Match match = regex.Match(email);
				result = match.Success;
			}
			return result;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00015160 File Offset: 0x00013360
		private static void ChangeEmailsToTestMode(ref List<BatchEmailWithMailMerge3.EmailResult> mergedEmails, string adminEmail)
		{
			string str = DateTime.Now.ToString("yyyy-MM-dd h:mm tt");
			foreach (BatchEmailWithMailMerge3.EmailResult emailResult in mergedEmails)
			{
				TPMailMessage mergedEmail = emailResult.MergedEmail;
				string text = (mergedEmail.Cc == null) ? "" : mergedEmail.Cc.GetEmailList();
				string text2 = (mergedEmail.Bcc == null) ? "" : mergedEmail.Bcc.GetEmailList();
				string text3 = (mergedEmail.To == null) ? "" : mergedEmail.To.GetEmailList();
				mergedEmail.To = new List<TPMailAddress>
				{
					new TPMailAddress
					{
						EmailAddress = (adminEmail ?? "")
					}
				};
				mergedEmail.Bcc = new List<TPMailAddress>();
				mergedEmail.Cc = new List<TPMailAddress>();
				mergedEmail.Subject = "TEST-MODE: " + str + (mergedEmail.Subject ?? "");
				string text4 = (mergedEmail.BodyType == eEmailBodyType.Html) ? "<br />" : ((mergedEmail.BodyType == eEmailBodyType.PlainText) ? "\r\n" : "<br />\r\n");
				mergedEmail.Body = string.Format("Running in test mode; original To: {0}; original Cc: {1}; original Bcc: {2}{3}", new object[]
				{
					text3,
					text,
					text2,
					text4
				}) + (mergedEmail.Body ?? "");
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00015308 File Offset: 0x00013508
		private IList<MailMergedEmailWithOriginalRowAndDictionary> GroupMailMergeAndReturnOriginalDataRows(DataTable t, string TemplateXml, string[] groupByColNames)
		{
			List<string> list = (from DataColumn dc in t.Columns
			select dc.ColumnName).ToList<string>();
			List<MailMergedEmailWithOriginalRowAndDictionary> list2 = new List<MailMergedEmailWithOriginalRowAndDictionary>();
			IMailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(this.OpContext);
			List<BatchEmailWithMailMerge3.GroupColumnInfo> list3 = (from DataColumn dc in t.Columns
			where dc.ColumnName.StartsWith("GROUP_")
			select dc.ColumnName into g
			select new BatchEmailWithMailMerge3.GroupColumnInfo(g)).ToList<BatchEmailWithMailMerge3.GroupColumnInfo>();
			foreach (BatchEmailWithMailMerge3.GroupColumnInfo groupColumnInfo2 in list3)
			{
				bool flag = !t.Columns.Contains(groupColumnInfo2.GroupedItemsColumnName);
				if (flag)
				{
					t.Columns.Add(groupColumnInfo2.GroupedItemsColumnName);
					list.Add(groupColumnInfo2.GroupedItemsColumnName);
				}
			}
			int i = 0;
			DataView dataView = new DataView
			{
				Table = t,
				Sort = string.Join(",", groupByColNames)
			};
			while (i < dataView.Count)
			{
				DataRow dr0 = dataView[i].Row;
				string[] array = (from g in groupByColNames
				select dr0[g].ToString().ToLower().Trim()).ToArray<string>();
				int j = i;
				List<DataRow> list4 = new List<DataRow>();
				while (j < dataView.Count)
				{
					DataRow dr1 = dataView[j].Row;
					string[] array2 = (from g in groupByColNames
					select dr1[g].ToString().Trim().ToLower()).ToArray<string>();
					bool flag2 = array.Length == array2.Length && array.Intersect(array2).Count<string>() == array.Length;
					bool flag3 = !flag2;
					if (flag3)
					{
						break;
					}
					list4.Add(dr1);
					j++;
				}
				using (List<BatchEmailWithMailMerge3.GroupColumnInfo>.Enumerator enumerator2 = list3.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						BatchEmailWithMailMerge3.GroupColumnInfo groupColumnInfo = enumerator2.Current;
						dr0[groupColumnInfo.GroupedItemsColumnName] = groupColumnInfo.ListPrefix + string.Join(groupColumnInfo.ListItemSeparator, (from g in list4
						select groupColumnInfo.ListItemPrefix + g[groupColumnInfo.SingleItemColumnName].ToString().Trim() + groupColumnInfo.ListItemSuffix).Distinct<string>()) + groupColumnInfo.ListSuffix;
					}
				}
				try
				{
					MailMergeContextWithCustomDictionary mailMergeContextWithCustomDictionaryFromDataRow = MailMergingEmailManager.GetMailMergeContextWithCustomDictionaryFromDataRow(dr0, list);
					TPMailMessage mergedEmail = mailMergingEmailManager.MailMerge(mailMergeContextWithCustomDictionaryFromDataRow, TemplateXml);
					list2.Add(new MailMergedEmailWithOriginalRowAndDictionary
					{
						ContextWithCustomDictionary = mailMergeContextWithCustomDictionaryFromDataRow,
						MergedEmail = mergedEmail,
						OriginalRows = list4.ToArray()
					});
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("Common.Core.MailMerging.MailMergingEmailManager:MailMergeAndReturnOriginalDataRows:Collect:err={0}", ex.ToString());
				}
				i = j;
			}
			return list2;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00015690 File Offset: 0x00013890
		private IEnumerable<BatchEmailWithMailMerge3.EmailResult> BatchEmailWithMailMergeThree(DataTable t, BatchEmailWithMailMerge3.BatchEmailSendParameters sendParameters)
		{
			string templateXml = sendParameters.TemplateXml;
			TPMailMessage mailMessage = templateXml.ConvertXmlToBatchEmail();
			string templateXml2 = mailMessage.ToEmailXml();
			IMailMergingEmailManager mailMergingEmailManager = new MailMergingEmailManager(this.OpContext);
			IList<MailMergedEmailWithOriginalRowAndDictionary> source = (sendParameters.GroupByColumnNames != null && sendParameters.GroupByColumnNames.Length != 0) ? this.GroupMailMergeAndReturnOriginalDataRows(t, templateXml2, sendParameters.GroupByColumnNames) : mailMergingEmailManager.MailMergeAndReturnOriginalDataRows(t, templateXml2);
			List<BatchEmailWithMailMerge3.EmailResult> mergedEmails = (from g in source
			select new BatchEmailWithMailMerge3.EmailResult
			{
				MergedEmail = g.MergedEmail,
				ContextWithDictionary = g.ContextWithCustomDictionary,
				OriginalRows = g.OriginalRows,
				SendResult = new TPMailResult
				{
					Status = eTPMailResultStatus.Pending
				}
			}).ToList<BatchEmailWithMailMerge3.EmailResult>();
			return this.SendMergedEmails(mergedEmails, sendParameters);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00015728 File Offset: 0x00013928
		private List<BatchEmailWithMailMerge3.EmailResult> SendMergedEmails(List<BatchEmailWithMailMerge3.EmailResult> mergedEmails, BatchEmailWithMailMerge3.BatchEmailSendParameters sendParameters)
		{
			bool testMode = sendParameters.TestMode;
			if (testMode)
			{
				BatchEmailWithMailMerge3.ChangeEmailsToTestMode(ref mergedEmails, sendParameters.AdminEmail ?? "");
			}
			bool previewMode = sendParameters.PreviewMode;
			List<BatchEmailWithMailMerge3.EmailResult> mergedEmails2;
			if (previewMode)
			{
				foreach (BatchEmailWithMailMerge3.EmailResult emailResult in mergedEmails)
				{
					emailResult.SendResult.Status = eTPMailResultStatus.NotSentBecausePreviewMode;
				}
				mergedEmails2 = mergedEmails;
			}
			else
			{
				bool sendFirstEmailOnly = sendParameters.SendFirstEmailOnly;
				if (sendFirstEmailOnly)
				{
					for (int i = 0; i < mergedEmails.Count; i++)
					{
						BatchEmailWithMailMerge3.EmailResult emailResult2 = mergedEmails[i];
						bool flag = i == 0;
						if (flag)
						{
							this.SendMergedEmails(new List<BatchEmailWithMailMerge3.EmailResult>
							{
								emailResult2
							}, sendParameters.TestMode, sendParameters.IconNum, sendParameters.DelayBetweenEmailsInSeconds, sendParameters.HistoryCode, sendParameters.TemplateId, sendParameters.SendReport, sendParameters.Title, sendParameters.AdminEmail, sendParameters.CreatePocForSuccessfulEmails, sendParameters.OverridePocAppTypeId);
						}
						else
						{
							emailResult2.SendResult.Status = eTPMailResultStatus.NotSentBecauseSendFirstEmailOnly;
						}
					}
					mergedEmails2 = mergedEmails;
				}
				else
				{
					bool flag2 = !sendParameters.SendEmailsSynchronously;
					if (flag2)
					{
						ThreadStart start = delegate()
						{
							this.SendMergedEmails(mergedEmails, sendParameters.TestMode, sendParameters.IconNum, sendParameters.DelayBetweenEmailsInSeconds, sendParameters.HistoryCode, sendParameters.TemplateId, sendParameters.SendReport, sendParameters.Title, sendParameters.AdminEmail, sendParameters.CreatePocForSuccessfulEmails, sendParameters.OverridePocAppTypeId);
						};
						Thread thread = new Thread(start);
						thread.Start();
					}
					else
					{
						this.SendMergedEmails(mergedEmails, sendParameters.TestMode, sendParameters.IconNum, sendParameters.DelayBetweenEmailsInSeconds, sendParameters.HistoryCode, sendParameters.TemplateId, sendParameters.SendReport, sendParameters.Title, sendParameters.AdminEmail, sendParameters.CreatePocForSuccessfulEmails, sendParameters.OverridePocAppTypeId);
					}
					mergedEmails2 = mergedEmails;
				}
			}
			return mergedEmails2;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000159A8 File Offset: 0x00013BA8
		private void SendMergedEmails(IList<BatchEmailWithMailMerge3.EmailResult> mergedEmails, bool testMode, int iconNum, int delayBetweenEmailsInSeconds, string historyCode, int templateId, bool sendReport, string title, string adminEmail, bool createPoc, int overridePocAppTypeId)
		{
			IAppointmentIconManager appointmentIconManager = new AppointmentIconManager(this.OpContext);
			AppointmentIcon appointmentIcon = (iconNum >= 0) ? appointmentIconManager.LoadAppointmentIconByIconNum(iconNum) : null;
			IEmailManager emailManager = new EmailManager(this.OpContext);
			IEmailHistoryLoggerManager hm = new EmailHistoryLoggerManager(this.OpContext);
			string[] array = null;
			foreach (BatchEmailWithMailMerge3.EmailResult emailResult in mergedEmails)
			{
				bool wasSuccessfullySent = false;
				try
				{
					TPMailMessage mergedEmail = emailResult.MergedEmail;
					emailResult.SendResult = emailManager.SendEmail(mergedEmail);
					wasSuccessfullySent = (emailResult.SendResult.Status == eTPMailResultStatus.CompletedSuccess || emailResult.SendResult.Status == eTPMailResultStatus.CompletedWithWarnings);
					DataRow[] array2 = emailResult.OriginalRows ?? new DataRow[0];
					List<MailMergeContext> list = new List<MailMergeContext>();
					bool flag = !testMode;
					if (flag)
					{
						bool flag2 = array2.Length == 1;
						if (flag2)
						{
							BatchEmailWithMailMerge3.LogEmailHistory(hm, mergedEmail, historyCode, templateId, wasSuccessfullySent, emailResult.SendResult.ErrorMessage, emailResult.ContextWithDictionary.Context.PerDateId, emailResult.ContextWithDictionary.Context.PersonId, emailResult.ContextWithDictionary.Context.LuCourseId);
						}
						else
						{
							DataRow[] array3 = array2;
							for (int i = 0; i < array3.Length; i++)
							{
								DataRow dataRow = array3[i];
								string[] array4;
								if ((array4 = array) == null)
								{
									array4 = (array = (from DataColumn dc in dataRow.Table.Columns
									select dc.ColumnName).ToArray<string>());
								}
								string[] colNames = array4;
								MailMergeContextWithCustomDictionary mailMergeContextWithCustomDictionaryFromDataRow = MailMergingEmailManager.GetMailMergeContextWithCustomDictionaryFromDataRow(dataRow, colNames);
								MailMergeContext context = ((mailMergeContextWithCustomDictionaryFromDataRow != null) ? mailMergeContextWithCustomDictionaryFromDataRow.Context : null) ?? new MailMergeContext();
								bool flag3 = !list.Any((MailMergeContext g) => g.PersonId == context.PersonId && g.PerDateId == context.PerDateId && g.LuCourseId == context.LuCourseId);
								if (flag3)
								{
									BatchEmailWithMailMerge3.LogEmailHistory(hm, mergedEmail, historyCode, templateId, wasSuccessfullySent, emailResult.SendResult.ErrorMessage, context.PerDateId, context.PersonId, context.LuCourseId);
								}
								list.Add(context);
							}
						}
						bool flag4 = appointmentIcon != null;
						if (flag4)
						{
							bool flag5 = array2.Length == 1;
							if (flag5)
							{
								bool flag6 = emailResult.ContextWithDictionary.Context.AppointmentId > 0;
								if (flag6)
								{
									appointmentIconManager.InsertOrUpdateAppointmentIcon(false, emailResult.ContextWithDictionary.Context.AppointmentId, appointmentIcon);
								}
							}
							else
							{
								DataRow[] array5 = array2;
								for (int j = 0; j < array5.Length; j++)
								{
									DataRow dataRow2 = array5[j];
									string[] array6;
									if ((array6 = array) == null)
									{
										array6 = (array = (from DataColumn dc in dataRow2.Table.Columns
										select dc.ColumnName).ToArray<string>());
									}
									string[] colNames2 = array6;
									MailMergeContext context = MailMergingEmailManager.GetMailMergeContextWithCustomDictionaryFromDataRow(dataRow2, colNames2).Context ?? new MailMergeContext();
									bool flag7 = context.AppointmentId > 0 && list.All((MailMergeContext g) => g.AppointmentId != context.AppointmentId);
									if (flag7)
									{
										appointmentIconManager.InsertOrUpdateAppointmentIcon(false, context.AppointmentId, appointmentIcon);
									}
									list.Add(context);
								}
							}
						}
						if (createPoc)
						{
							IPeopleGroupManager pgm = new PeopleGroupManager(this.OpContext);
							IPointOfContactManager pcm = new PointOfContactManager(this.OpContext);
							bool flag8 = array2.Length == 1;
							if (flag8)
							{
								BatchEmailWithMailMerge3.CreatePoc(emailResult.ContextWithDictionary.Context.PersonId, pgm, pcm, mergedEmail, overridePocAppTypeId);
							}
							else
							{
								DataRow[] array7 = array2;
								for (int k = 0; k < array7.Length; k++)
								{
									DataRow dataRow3 = array7[k];
									string[] array8;
									if ((array8 = array) == null)
									{
										array8 = (array = (from DataColumn dc in dataRow3.Table.Columns
										select dc.ColumnName).ToArray<string>());
									}
									string[] colNames3 = array8;
									MailMergeContext context = MailMergingEmailManager.GetMailMergeContextWithCustomDictionaryFromDataRow(dataRow3, colNames3).Context ?? new MailMergeContext();
									bool flag9 = context.PersonId > 0 && list.All((MailMergeContext g) => g.PersonId != context.PersonId);
									if (flag9)
									{
										BatchEmailWithMailMerge3.CreatePoc(context.PersonId, pgm, pcm, mergedEmail, overridePocAppTypeId);
									}
									list.Add(context);
								}
							}
						}
					}
					bool flag10 = delayBetweenEmailsInSeconds > 0;
					if (flag10)
					{
						Thread.Sleep(delayBetweenEmailsInSeconds * 1000);
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("Common.Core.Reports.ReportFunctionExecutions.BatchEmailWithMailMerge3:SingleEmailFailed:wasSuccessfullySent={0}:err={1}", wasSuccessfullySent.ToString(), ex.ToString());
				}
			}
			bool flag11 = !sendReport;
			if (!flag11)
			{
				int num = mergedEmails.Count((BatchEmailWithMailMerge3.EmailResult g) => g.SendResult.Status == eTPMailResultStatus.CompletedSuccess || g.SendResult.Status == eTPMailResultStatus.CompletedWithWarnings);
				int num2 = mergedEmails.Count - num;
				string text = string.Format("Batch email report for '{0}' ({1}) Fail count={2}\n", title ?? "", historyCode ?? "", num2.ToString());
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(text);
				stringBuilder.Append(string.Format("Date: {0}\n", DateTime.Now.ToString("yyyy-MM-dd h:mm tt")));
				stringBuilder.Append(string.Format("Successfully sent {0}\n", num));
				stringBuilder.Append("\n");
				foreach (BatchEmailWithMailMerge3.EmailResult emailResult2 in mergedEmails)
				{
					stringBuilder.Append("===================\n");
					stringBuilder.Append(string.Format("Successful: {0}\n", emailResult2.SendResult.Status.ToString()));
					bool flag12 = !string.IsNullOrEmpty(emailResult2.SendResult.ErrorMessage);
					if (flag12)
					{
						stringBuilder.Append(string.Format("Error: {0}\n", emailResult2.SendResult.ErrorMessage));
					}
					stringBuilder.Append(emailResult2.MergedEmail.ConvertToDisplayString());
					stringBuilder.Append("\n\n");
				}
				string text2 = adminEmail;
				bool flag13 = !BatchEmailWithMailMerge3.IsEmailValid(text2);
				if (flag13)
				{
					text2 = emailManager.GetDefaultFromAddress();
				}
				emailManager.SendEmail(text2, text2, text, stringBuilder.ToString(), null, null, null, null);
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000160BC File Offset: 0x000142BC
		private static void CreatePoc(int pid, IPeopleGroupManager pgm, IPointOfContactManager pcm, TPMailMessage email, int overridePocAppTypeId)
		{
			bool flag = pid > 0;
			if (flag)
			{
				IList<int> groupIdsByPersonId = pgm.GetGroupIdsByPersonId(pid);
				bool flag2 = groupIdsByPersonId.Contains(1);
				if (flag2)
				{
					pcm.SaveEmailAsPointOfContact(false, pid, 0, email, ePointOfContactContext.AutomaticSystemCreated, overridePocAppTypeId);
				}
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000160F8 File Offset: 0x000142F8
		private static void LogEmailHistory(IEmailHistoryLoggerManager hm, TPMailMessage email, string historyCode, int templateId, bool wasSuccessfullySent, string sendResultErrorMessage, int contextPerDateId, int contextPersonId, int contextLuCourseId)
		{
			hm.LogItem(new EmailHistoryLoggerItem
			{
				EmailMessage = email.ConvertToDisplayString(),
				HistoryCode = historyCode,
				InfoPcId = contextPerDateId,
				PersonId = contextPersonId,
				LuCourseId = contextLuCourseId,
				TemplateId = templateId,
				WasSuccessfullySent = wasSuccessfullySent,
				Note = (sendResultErrorMessage ?? "")
			});
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00016168 File Offset: 0x00014368
		private BatchEmailWithMailMerge3.BatchEmailSendParameters GetSendParametersFromXml(string xml)
		{
			BatchEmailWithMailMerge3.BatchEmailSendParameters batchEmailSendParameters = new BatchEmailWithMailMerge3.BatchEmailSendParameters
			{
				HistoryCode = "Unknown",
				Title = "Unknown",
				SendReport = true,
				TemplateXml = xml
			};
			BatchEmailWithMailMerge3.BatchEmailSendParameters result;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(string.IsNullOrEmpty(xml) ? "<batchemails></batchemails>" : xml);
				XmlElement documentElement = xmlDocument.DocumentElement;
				XmlNode xmlNode = (documentElement != null) ? documentElement.FirstChild : null;
				bool flag = xmlNode == null;
				if (flag)
				{
					result = batchEmailSendParameters;
				}
				else
				{
					XmlAttributeCollection attributes = xmlNode.Attributes;
					BatchEmailWithMailMerge3.FromXml(ref batchEmailSendParameters, attributes);
					bool flag2 = batchEmailSendParameters.TemplateId <= 0;
					if (flag2)
					{
						result = batchEmailSendParameters;
					}
					else
					{
						ITemplateManager templateManager = new TemplateManager(this.OpContext);
						Template template = templateManager.LoadTemplate(batchEmailSendParameters.TemplateId, true);
						TPMailMessage tpmailMessage = (template == null || (template.EmailBehindDocumentTemplate == null && template.EmailTemplate == null)) ? null : (template.EmailTemplate ?? template.EmailBehindDocumentTemplate);
						bool flag3 = tpmailMessage != null;
						if (flag3)
						{
							batchEmailSendParameters.TemplateXml = tpmailMessage.ToEmailXml();
						}
						result = batchEmailSendParameters;
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.Core.Reports.ReportFunctionsExecutions.BatchEmailWithMailMerge3.GetSendParametersFromXml:err={0}", ex.ToString());
				result = batchEmailSendParameters;
			}
			return result;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000162AC File Offset: 0x000144AC
		private static bool StringToBool(string s, bool defaultValue)
		{
			bool flag = string.IsNullOrEmpty(s);
			bool result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				bool flag2 = s.Equals("1");
				bool flag3;
				result = (flag2 || (bool.TryParse(s, out flag3) ? flag3 : defaultValue));
			}
			return result;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000162F0 File Offset: 0x000144F0
		private static int StringToInt(string s, int defaultValue)
		{
			bool flag = string.IsNullOrEmpty(s);
			int result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				int num;
				result = (int.TryParse(s, out num) ? num : defaultValue);
			}
			return result;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00016320 File Offset: 0x00014520
		private static void FromXml(ref BatchEmailWithMailMerge3.BatchEmailSendParameters bsm, XmlAttributeCollection attributes)
		{
			bool flag = true;
			bool testMode = false;
			bool flag2 = false;
			foreach (object obj in attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string text = xmlAttribute.Name.ToLower();
				string text2 = xmlAttribute.Value ?? "";
				bool flag3 = string.IsNullOrEmpty(text2);
				string text3 = text;
				string text4 = text3;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text4);
				if (num <= 2556802313U)
				{
					if (num <= 510364466U)
					{
						if (num != 19793580U)
						{
							if (num != 381759768U)
							{
								if (num == 510364466U)
								{
									if (text4 == "testmode")
									{
										testMode = BatchEmailWithMailMerge3.StringToBool(text2, false);
									}
								}
							}
							else if (text4 == "promptuser")
							{
								flag2 = BatchEmailWithMailMerge3.StringToBool(text2, false);
							}
						}
						else if (text4 == "adminemail")
						{
							bool flag4 = !flag3;
							if (flag4)
							{
								bsm.AdminEmail = text2;
							}
						}
					}
					else if (num <= 914840617U)
					{
						if (num != 779643441U)
						{
							if (num == 914840617U)
							{
								if (text4 == "groupby")
								{
									bsm.GroupByColumnNames = (from g in text2.Split(new char[]
									{
										','
									})
									select g.Trim() into h
									where h.Length > 0
									select h).ToArray<string>();
								}
							}
						}
						else if (text4 == "overrideapptypeid")
						{
							bsm.OverridePocAppTypeId = BatchEmailWithMailMerge3.StringToInt(text2, 0);
						}
					}
					else if (num != 2174115206U)
					{
						if (num == 2556802313U)
						{
							if (text4 == "title")
							{
								bool flag5 = !flag3;
								if (flag5)
								{
									bsm.Title = text2;
								}
							}
						}
					}
					else if (text4 == "templateid")
					{
						bsm.TemplateId = BatchEmailWithMailMerge3.StringToInt(text2, 0);
					}
				}
				else if (num <= 3848207249U)
				{
					if (num != 3410127167U)
					{
						if (num != 3842741008U)
						{
							if (num == 3848207249U)
							{
								if (text4 == "createpoc")
								{
									bsm.CreatePocForSuccessfulEmails = BatchEmailWithMailMerge3.StringToBool(text2, false);
								}
							}
						}
						else if (text4 == "iconnum")
						{
							bsm.IconNum = BatchEmailWithMailMerge3.StringToInt(text2, -1);
						}
					}
					else if (text4 == "isactive")
					{
						flag = BatchEmailWithMailMerge3.StringToBool(text2, false);
					}
				}
				else if (num <= 4144906025U)
				{
					if (num != 3922246785U)
					{
						if (num == 4144906025U)
						{
							if (text4 == "delaybetweenemails")
							{
								bsm.DelayBetweenEmailsInSeconds = BatchEmailWithMailMerge3.StringToInt(text2, 0);
							}
						}
					}
					else if (text4 == "sendemailssync")
					{
						bsm.SendEmailsSynchronously = BatchEmailWithMailMerge3.StringToBool(text2, false);
					}
				}
				else if (num != 4154231416U)
				{
					if (num == 4225941681U)
					{
						if (text4 == "sendreport")
						{
							bsm.SendReport = BatchEmailWithMailMerge3.StringToBool(text2, false);
						}
					}
				}
				else if (text4 == "emailhistorytypecode")
				{
					bool flag6 = !flag3;
					if (flag6)
					{
						bsm.HistoryCode = text2;
					}
					bool flag7 = string.IsNullOrEmpty(bsm.HistoryCode);
					if (flag7)
					{
						bsm.HistoryCode = "UNKNOWN";
					}
				}
			}
			bool flag8 = !flag || flag2;
			if (flag8)
			{
				bsm.PreviewMode = true;
			}
			bsm.TestMode = testMode;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000167A4 File Offset: 0x000149A4
		public void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentWholeReportResult, ReportFunction Function)
		{
			DataTable dataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = dataTable == null;
			if (flag)
			{
				dataTable = new DataTable("l");
			}
			dataTable.Columns.Add("_GroupNo", typeof(int));
			dataTable.Columns.Add("_EmailWasSent", typeof(bool));
			dataTable.Columns.Add("_Email");
			dataTable.Columns.Add("_SentErrorStatus");
			dataTable.Columns.Add("_Parameters");
			string defaultFunctionParameter = Function.GetDefaultFunctionParameter();
			BatchEmailWithMailMerge3.BatchEmailSendParameters sendParametersFromXml = this.GetSendParametersFromXml(defaultFunctionParameter);
			IEnumerable<BatchEmailWithMailMerge3.EmailResult> enumerable = this.BatchEmailWithMailMergeThree(dataTable, sendParametersFromXml);
			List<BatchEmailWithMailMerge3.EmailResult> list = (enumerable != null) ? enumerable.ToList<BatchEmailWithMailMerge3.EmailResult>() : null;
			bool flag2 = list == null;
			if (!flag2)
			{
				DataTable dataTable2 = dataTable.Clone();
				dataTable2.TableName = "t";
				string value = sendParametersFromXml.ToString();
				for (int i = 0; i < list.Count; i++)
				{
					BatchEmailWithMailMerge3.EmailResult emailResult = list[i];
					DataRow[] originalRows = emailResult.OriginalRows;
					foreach (DataRow dataRow in originalRows)
					{
						dataRow["_GroupNo"] = i;
						dataRow["_EmailWasSent"] = (emailResult.SendResult.Status == eTPMailResultStatus.CompletedSuccess || emailResult.SendResult.Status == eTPMailResultStatus.CompletedWithWarnings);
						dataRow["_Email"] = emailResult.MergedEmail.ConvertToDisplayString();
						dataRow["_SentErrorStatus"] = (emailResult.SendResult.ErrorMessage ?? "");
						dataRow["_Parameters"] = value;
						dataTable2.ImportRow(dataRow);
					}
				}
				Result.Data.Table = dataTable2;
			}
		}

		// Token: 0x02000202 RID: 514
		internal class BatchEmailSendParameters
		{
			// Token: 0x1700025B RID: 603
			// (get) Token: 0x06001258 RID: 4696 RVA: 0x0007F593 File Offset: 0x0007D793
			// (set) Token: 0x06001259 RID: 4697 RVA: 0x0007F59B File Offset: 0x0007D79B
			public string Title { get; set; }

			// Token: 0x1700025C RID: 604
			// (get) Token: 0x0600125A RID: 4698 RVA: 0x0007F5A4 File Offset: 0x0007D7A4
			// (set) Token: 0x0600125B RID: 4699 RVA: 0x0007F5AC File Offset: 0x0007D7AC
			public string HistoryCode { get; set; }

			// Token: 0x1700025D RID: 605
			// (get) Token: 0x0600125C RID: 4700 RVA: 0x0007F5B5 File Offset: 0x0007D7B5
			// (set) Token: 0x0600125D RID: 4701 RVA: 0x0007F5BD File Offset: 0x0007D7BD
			public bool SendReport { get; set; }

			// Token: 0x1700025E RID: 606
			// (get) Token: 0x0600125E RID: 4702 RVA: 0x0007F5C6 File Offset: 0x0007D7C6
			// (set) Token: 0x0600125F RID: 4703 RVA: 0x0007F5CE File Offset: 0x0007D7CE
			public int DelayBetweenEmailsInSeconds { get; set; }

			// Token: 0x1700025F RID: 607
			// (get) Token: 0x06001260 RID: 4704 RVA: 0x0007F5D7 File Offset: 0x0007D7D7
			// (set) Token: 0x06001261 RID: 4705 RVA: 0x0007F5DF File Offset: 0x0007D7DF
			public bool PreviewMode { get; set; }

			// Token: 0x17000260 RID: 608
			// (get) Token: 0x06001262 RID: 4706 RVA: 0x0007F5E8 File Offset: 0x0007D7E8
			// (set) Token: 0x06001263 RID: 4707 RVA: 0x0007F5F0 File Offset: 0x0007D7F0
			public bool SendFirstEmailOnly { get; set; }

			// Token: 0x17000261 RID: 609
			// (get) Token: 0x06001264 RID: 4708 RVA: 0x0007F5F9 File Offset: 0x0007D7F9
			// (set) Token: 0x06001265 RID: 4709 RVA: 0x0007F601 File Offset: 0x0007D801
			public string AdminEmail { get; set; }

			// Token: 0x17000262 RID: 610
			// (get) Token: 0x06001266 RID: 4710 RVA: 0x0007F60A File Offset: 0x0007D80A
			// (set) Token: 0x06001267 RID: 4711 RVA: 0x0007F612 File Offset: 0x0007D812
			public int IconNum { get; set; }

			// Token: 0x17000263 RID: 611
			// (get) Token: 0x06001268 RID: 4712 RVA: 0x0007F61B File Offset: 0x0007D81B
			// (set) Token: 0x06001269 RID: 4713 RVA: 0x0007F623 File Offset: 0x0007D823
			public string TemplateXml { get; set; }

			// Token: 0x17000264 RID: 612
			// (get) Token: 0x0600126A RID: 4714 RVA: 0x0007F62C File Offset: 0x0007D82C
			// (set) Token: 0x0600126B RID: 4715 RVA: 0x0007F634 File Offset: 0x0007D834
			public int TemplateId { get; set; }

			// Token: 0x17000265 RID: 613
			// (get) Token: 0x0600126C RID: 4716 RVA: 0x0007F63D File Offset: 0x0007D83D
			// (set) Token: 0x0600126D RID: 4717 RVA: 0x0007F645 File Offset: 0x0007D845
			public bool TestMode { get; set; }

			// Token: 0x17000266 RID: 614
			// (get) Token: 0x0600126E RID: 4718 RVA: 0x0007F64E File Offset: 0x0007D84E
			// (set) Token: 0x0600126F RID: 4719 RVA: 0x0007F656 File Offset: 0x0007D856
			public bool CreatePocForSuccessfulEmails { get; set; }

			// Token: 0x17000267 RID: 615
			// (get) Token: 0x06001270 RID: 4720 RVA: 0x0007F65F File Offset: 0x0007D85F
			// (set) Token: 0x06001271 RID: 4721 RVA: 0x0007F667 File Offset: 0x0007D867
			public int OverridePocAppTypeId { get; set; }

			// Token: 0x17000268 RID: 616
			// (get) Token: 0x06001272 RID: 4722 RVA: 0x0007F670 File Offset: 0x0007D870
			// (set) Token: 0x06001273 RID: 4723 RVA: 0x0007F678 File Offset: 0x0007D878
			public bool SendEmailsSynchronously { get; set; }

			// Token: 0x17000269 RID: 617
			// (get) Token: 0x06001274 RID: 4724 RVA: 0x0007F681 File Offset: 0x0007D881
			// (set) Token: 0x06001275 RID: 4725 RVA: 0x0007F689 File Offset: 0x0007D889
			public string[] GroupByColumnNames { get; set; }

			// Token: 0x06001276 RID: 4726 RVA: 0x0007F694 File Offset: 0x0007D894
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("Title: " + (this.Title ?? ""));
				stringBuilder.AppendLine("HistoryCode: " + (this.HistoryCode ?? ""));
				stringBuilder.AppendLine("DontSendEmails: " + this.SendReport.ToString());
				stringBuilder.AppendLine("DelayBetweenEmailsInSeconds: " + this.DelayBetweenEmailsInSeconds.ToString());
				stringBuilder.AppendLine("PreviewMode: " + this.PreviewMode.ToString());
				stringBuilder.AppendLine("TestMode: " + this.PreviewMode.ToString());
				stringBuilder.AppendLine("SendFirstEmailOnly: " + this.SendFirstEmailOnly.ToString());
				stringBuilder.AppendLine("AdminEmail: " + (this.AdminEmail ?? ""));
				stringBuilder.AppendLine("IconNum: " + this.IconNum.ToString());
				stringBuilder.AppendLine("TemplateId: " + this.TemplateId.ToString());
				stringBuilder.AppendLine("Send emails synchronously: " + this.SendEmailsSynchronously.ToString());
				stringBuilder.AppendLine("Group by col names: " + ((this.GroupByColumnNames == null) ? "NULL" : string.Join(",", this.GroupByColumnNames)));
				return stringBuilder.ToString();
			}
		}

		// Token: 0x02000203 RID: 515
		internal class EmailResult
		{
			// Token: 0x1700026A RID: 618
			// (get) Token: 0x06001278 RID: 4728 RVA: 0x0007F83A File Offset: 0x0007DA3A
			// (set) Token: 0x06001279 RID: 4729 RVA: 0x0007F842 File Offset: 0x0007DA42
			public MailMergeContextWithCustomDictionary ContextWithDictionary { get; set; }

			// Token: 0x1700026B RID: 619
			// (get) Token: 0x0600127A RID: 4730 RVA: 0x0007F84B File Offset: 0x0007DA4B
			// (set) Token: 0x0600127B RID: 4731 RVA: 0x0007F853 File Offset: 0x0007DA53
			public TPMailMessage MergedEmail { get; set; }

			// Token: 0x1700026C RID: 620
			// (get) Token: 0x0600127C RID: 4732 RVA: 0x0007F85C File Offset: 0x0007DA5C
			// (set) Token: 0x0600127D RID: 4733 RVA: 0x0007F864 File Offset: 0x0007DA64
			public DataRow[] OriginalRows { get; set; }

			// Token: 0x1700026D RID: 621
			// (get) Token: 0x0600127E RID: 4734 RVA: 0x0007F86D File Offset: 0x0007DA6D
			// (set) Token: 0x0600127F RID: 4735 RVA: 0x0007F875 File Offset: 0x0007DA75
			public TPMailResult SendResult { get; set; }
		}

		// Token: 0x02000204 RID: 516
		internal class GroupColumnInfo
		{
			// Token: 0x06001281 RID: 4737 RVA: 0x0000672B File Offset: 0x0000492B
			public GroupColumnInfo()
			{
			}

			// Token: 0x06001282 RID: 4738 RVA: 0x0007F880 File Offset: 0x0007DA80
			public GroupColumnInfo(string colName)
			{
				this.SingleItemColumnName = colName;
				this.GroupedItemsColumnName = colName.Substring(6) + "s";
				this.ListPrefix = "<ul>\r\n";
				this.ListSuffix = "\r\n</ul>";
				this.ListItemSeparator = "\r\n";
				this.ListItemPrefix = "<li>";
				this.ListItemSuffix = "</li>";
			}

			// Token: 0x1700026E RID: 622
			// (get) Token: 0x06001283 RID: 4739 RVA: 0x0007F8F1 File Offset: 0x0007DAF1
			// (set) Token: 0x06001284 RID: 4740 RVA: 0x0007F8F9 File Offset: 0x0007DAF9
			public string SingleItemColumnName { get; set; }

			// Token: 0x1700026F RID: 623
			// (get) Token: 0x06001285 RID: 4741 RVA: 0x0007F902 File Offset: 0x0007DB02
			// (set) Token: 0x06001286 RID: 4742 RVA: 0x0007F90A File Offset: 0x0007DB0A
			public string GroupedItemsColumnName { get; set; }

			// Token: 0x17000270 RID: 624
			// (get) Token: 0x06001287 RID: 4743 RVA: 0x0007F913 File Offset: 0x0007DB13
			// (set) Token: 0x06001288 RID: 4744 RVA: 0x0007F91B File Offset: 0x0007DB1B
			public string ListPrefix { get; set; }

			// Token: 0x17000271 RID: 625
			// (get) Token: 0x06001289 RID: 4745 RVA: 0x0007F924 File Offset: 0x0007DB24
			// (set) Token: 0x0600128A RID: 4746 RVA: 0x0007F92C File Offset: 0x0007DB2C
			public string ListSuffix { get; set; }

			// Token: 0x17000272 RID: 626
			// (get) Token: 0x0600128B RID: 4747 RVA: 0x0007F935 File Offset: 0x0007DB35
			// (set) Token: 0x0600128C RID: 4748 RVA: 0x0007F93D File Offset: 0x0007DB3D
			public string ListItemSeparator { get; set; }

			// Token: 0x17000273 RID: 627
			// (get) Token: 0x0600128D RID: 4749 RVA: 0x0007F946 File Offset: 0x0007DB46
			// (set) Token: 0x0600128E RID: 4750 RVA: 0x0007F94E File Offset: 0x0007DB4E
			public string ListItemPrefix { get; set; }

			// Token: 0x17000274 RID: 628
			// (get) Token: 0x0600128F RID: 4751 RVA: 0x0007F957 File Offset: 0x0007DB57
			// (set) Token: 0x06001290 RID: 4752 RVA: 0x0007F95F File Offset: 0x0007DB5F
			public string ListItemSuffix { get; set; }
		}
	}
}
