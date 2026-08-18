using System;
using System.Collections.Generic;
using ClockWorkLogger;
using TechnoPro.Common.Core.AppointmentsPointOfContact;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Core.Templates;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.AppointmentsPointOfContact;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.MailMerging;
using TechnoPro.Common.ICore.Templates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;
using TechnoPro.Common.Public.Entities.DynamicForms.AccommodationBatchLetterEmails;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Templates;
using TechnoPro.Common.Public.Entities.TPMailMan;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.SpireDoc
{
	// Token: 0x02000002 RID: 2
	public class AccommodationBatchLetterEmailsManager : IAccommodationBatchLetterEmailsManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public IAccommodationBatchLetterEmailsDAO dao { get; set; }

		// Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		public AccommodationBatchLetterEmailsManager()
		{
			this.dao = new AccommodationBatchLetterEmailsDAO(this.OpContext);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000207D File Offset: 0x0000027D
		public AccommodationBatchLetterEmailsManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new AccommodationBatchLetterEmailsDAO(opContext);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000209C File Offset: 0x0000029C
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020A4 File Offset: 0x000002A4
		public OperationContext OpContext { get; set; }

		// Token: 0x06000007 RID: 7 RVA: 0x000020AD File Offset: 0x000002AD
		public void MarkLetterSent(int PersonId, int LuCourseId, DateTime DateSent)
		{
			this.dao.MarkLetterSent(PersonId, LuCourseId, DateSent);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020C0 File Offset: 0x000002C0
		public IList<PotentialLetterToSendOut> GetPotentialLettersToSendOut(DateTime Today)
		{
			int settingValue = SettingManager.CurrentInstance.GetSettingValue<int>(Setting.TESTBOOKING_AccommodationsExpiryDateCid);
			return this.dao.GetPotentialLettersToSendOut(Today, settingValue);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020F0 File Offset: 0x000002F0
		public IList<PotentialLetterToSendOutResult> SendLetters(int TemplateId, bool TestingMode, bool ReturnAttachmentFile)
		{
			OldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(this.OpContext.WhoAmI, eSettingCode.SETTING_BATCH_ACCOMMODATION_LETTERS_ENABLED);
			bool flag = !settingValue_Bool;
			IList<PotentialLetterToSendOutResult> result;
			if (flag)
			{
				result = new List<PotentialLetterToSendOutResult>();
			}
			else
			{
				string settingValue_String = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_BATCH_ACCOMMODATION_LETTERS_TIME_FRAMES);
				IList<BatchAccommodationLetterTimeFrame> list = BatchAccommodationLetterTimeFrame.ParseFromString(settingValue_String);
				bool flag2 = list.Count < 1;
				if (flag2)
				{
					result = new List<PotentialLetterToSendOutResult>();
				}
				else
				{
					DateTime date = DateTime.Now.Date;
					bool flag3 = false;
					int year = date.Year;
					foreach (BatchAccommodationLetterTimeFrame batchAccommodationLetterTimeFrame in list)
					{
						DateTime t = new DateTime(year, batchAccommodationLetterTimeFrame.StartMonth, batchAccommodationLetterTimeFrame.StartDay);
						DateTime t2 = new DateTime(year, batchAccommodationLetterTimeFrame.EndMonth, batchAccommodationLetterTimeFrame.EndDay).AddDays(1.0);
						bool flag4 = date >= t && date < t2;
						if (flag4)
						{
							flag3 = true;
							break;
						}
					}
					IList<PotentialLetterToSendOutResult> list2;
					if (flag3)
					{
						list2 = this.SendLetters(TemplateId, date, TestingMode, ReturnAttachmentFile);
					}
					else
					{
						IList<PotentialLetterToSendOutResult> list3 = new List<PotentialLetterToSendOutResult>();
						list2 = list3;
					}
					result = list2;
				}
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002250 File Offset: 0x00000450
		public IList<PotentialLetterToSendOutResult> SendLetters(int TemplateId, DateTime Today, bool TestingMode, bool ReturnAttachmentFile)
		{
			IAccommodationsManager accommodationsManager = new AccommodationsManager(this.OpContext);
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
			IEmailManager emailManager = new EmailManager(this.OpContext);
			IPointOfContactManager pointOfContactManager = new PointOfContactManager(this.OpContext);
			bool flag = true;
			ITemplateManager templateManager = new TemplateManager(this.OpContext);
			Template template = templateManager.LoadTemplate(TemplateId, true);
			bool flag2 = template == null;
			IList<PotentialLetterToSendOutResult> result;
			if (flag2)
			{
				CWLogger.Logger.Warn("AccommodationBatchLetterEmailsManager:Template cannot be loaded:Templateid={0}:Operation was aborted.", TemplateId.ToString());
				result = null;
			}
			else
			{
				IList<PotentialLetterToSendOut> potentialLettersToSendOut = this.GetPotentialLettersToSendOut(Today);
				List<PotentialLetterToSendOutResult> list = new List<PotentialLetterToSendOutResult>();
				foreach (PotentialLetterToSendOut potentialLetterToSendOut in potentialLettersToSendOut)
				{
					PotentialLetterToSendOutResult potentialLetterToSendOutResult = new PotentialLetterToSendOutResult
					{
						PersonId = potentialLetterToSendOut.PersonId,
						LuCourseId = potentialLetterToSendOut.LuCourseId,
						AccommodationsExpiryDate = potentialLetterToSendOut.AccommodationsExpiryDate,
						DateLetterLastSent = potentialLetterToSendOut.DateLetterLastSent
					};
					try
					{
						bool flag3 = potentialLetterToSendOut.AccommodationsExpiryDate != null && potentialLetterToSendOut.AccommodationsExpiryDate.Value >= Today;
						if (flag3)
						{
							bool flag4 = potentialLetterToSendOut.DateLetterLastSent == null;
							if (flag4)
							{
								potentialLetterToSendOutResult.ShouldSend = true;
								potentialLetterToSendOutResult.Note = "Letter has never been sent";
							}
							else
							{
								bool flag5 = potentialLetterToSendOut.MaxDateAccommodationsWereModified == null;
								if (flag5)
								{
									potentialLetterToSendOutResult.ShouldSend = false;
									potentialLetterToSendOutResult.Note = "Accommodations have never been modified.";
								}
								else
								{
									bool flag6 = potentialLetterToSendOut.MaxDateAccommodationsWereModified.Value <= potentialLetterToSendOut.DateLetterLastSent.Value;
									if (flag6)
									{
										potentialLetterToSendOutResult.ShouldSend = false;
										potentialLetterToSendOutResult.Note = "Accommodations have not been modified since letter was last sent.";
									}
									else
									{
										potentialLetterToSendOutResult.ShouldSend = false;
										potentialLetterToSendOutResult.Note = "Accommodations were modified after letter was last sent";
									}
								}
							}
						}
						else
						{
							potentialLetterToSendOutResult.ShouldSend = false;
							potentialLetterToSendOutResult.Note = "Accommodation expiry missing or expired";
						}
						bool shouldSend = potentialLetterToSendOutResult.ShouldSend;
						if (shouldSend)
						{
							IMailMergingDocManager mailMergingDocManager = new MailMergingDocManager(this.OpContext);
							IAccommodationsManager accommodationsManager2 = new AccommodationsManager(this.OpContext);
							MailMergeContextWithCustomDictionary contextWithDictionary = new MailMergeContextWithCustomDictionary
							{
								Context = new MailMergeContext
								{
									PersonId = potentialLetterToSendOutResult.PersonId,
									LuCourseId = potentialLetterToSendOutResult.LuCourseId
								},
								CustomDictionary = new MailMergeCustomDictionary()
							};
							IDictionary<int, TPMailMessage> dictionary = mailMergingDocManager.MailMergeAccommodationEmailsWithLettersAsAttachments(new List<int>
							{
								potentialLetterToSendOutResult.LuCourseId
							}, contextWithDictionary, eFileFormat.PDF, TemplateId);
							bool flag7 = dictionary != null && dictionary.ContainsKey(potentialLetterToSendOutResult.LuCourseId);
							if (flag7)
							{
								TPMailMessage tpmailMessage = dictionary[potentialLetterToSendOutResult.LuCourseId];
								bool flag8 = tpmailMessage.Attachments == null;
								if (flag8)
								{
									tpmailMessage.Attachments = new List<TPMailAttachment>();
								}
								if (ReturnAttachmentFile)
								{
									potentialLetterToSendOutResult.Attachment = ((tpmailMessage.Attachments.Count > 0) ? tpmailMessage.Attachments[0] : null);
								}
								potentialLetterToSendOutResult.Email = tpmailMessage;
								bool flag9 = !TestingMode;
								if (flag9)
								{
									TPMailResult tpmailResult = emailManager.SendEmail(potentialLetterToSendOutResult.Email);
									bool flag10 = tpmailResult.Status == eTPMailResultStatus.CompletedSuccess || tpmailResult.Status == eTPMailResultStatus.CompletedWithWarnings;
									if (flag10)
									{
										potentialLetterToSendOutResult.SentSuccessfully = true;
										this.MarkLetterSent(potentialLetterToSendOutResult.PersonId, potentialLetterToSendOutResult.LuCourseId, DateTime.Now);
										pointOfContactManager.SaveEmailAsPointOfContact(false, potentialLetterToSendOutResult.PersonId, 1, potentialLetterToSendOutResult.Email, ePointOfContactContext.AutomaticSystemCreated);
										accommodationsManager.MarkAccommodationLetterIssued(potentialLetterToSendOutResult.PersonId, new int[]
										{
											potentialLetterToSendOutResult.LuCourseId
										});
										bool flag11 = flag && potentialLetterToSendOutResult.Attachment != null;
										if (flag11)
										{
											BinaryFile file = new BinaryFile
											{
												ByteArray = potentialLetterToSendOutResult.Attachment.FileBytes,
												FileName = potentialLetterToSendOutResult.Attachment.FileNameForDisplay
											};
											potentialLetterToSendOutResult.Note = "Stored file in documents list: " + dynamicDataManager.StoreFileInDocuments("Accommodations letter batch " + DateTime.Now.ToString("yyyy-MM-dd H:mm"), "Sent through batch system", file, potentialLetterToSendOutResult.PersonId, 1000).ToString();
										}
									}
									else
									{
										potentialLetterToSendOutResult.Note = string.Format("Email Failed:status={0}:errmsg={1}", tpmailResult.Status.ToString(), tpmailResult.ErrorMessage ?? "");
									}
								}
								else
								{
									potentialLetterToSendOutResult.Note = "Successful but not sent because in testing mode";
								}
							}
							else
							{
								potentialLetterToSendOutResult.Note = "Failed: Nothing returned from mail merge step.";
							}
						}
					}
					catch (Exception ex)
					{
						potentialLetterToSendOutResult.Note = potentialLetterToSendOutResult.Note + ": " + ex.ToString();
					}
					list.Add(potentialLetterToSendOutResult);
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000277C File Offset: 0x0000097C
		public IDictionary<int, DateTime?> GetBatchLetterSentDates(int PersonId, IList<int> LuCourseIds)
		{
			return this.dao.GetBatchLetterSentDates(PersonId, LuCourseIds);
		}
	}
}
