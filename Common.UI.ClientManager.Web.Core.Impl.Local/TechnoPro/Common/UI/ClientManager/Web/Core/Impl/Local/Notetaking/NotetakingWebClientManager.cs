using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.Notetaking;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.Notetaking;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.TPMailMan;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email;
using TechnoPro.Common.UI.ClientManager.Web.Core.Notetaking;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Notetaking
{
	// Token: 0x02000018 RID: 24
	public class NotetakingWebClientManager : INotetakingWebClientManager
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00005158 File Offset: 0x00003358
		public void NotifyStudentsNewLectureNotesHaveBeenUploaded(int NotetakerId, int LuCourseId, DateTime LectureDate)
		{
			try
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.NOTETAKINGB_Email_NotetakerUploadedNewNotes);
				TPMailMessage tpmailMessage = settingValue.EmailFromXml();
				bool flag = tpmailMessage == null || !tpmailMessage.IsActive;
				if (flag)
				{
					CWLogger.Logger.Warn("Common.UI.ClientManager.Web.Core.Impl.Notetaking.NotetakingWebClientManager.NotifyStudentsNewLectureNotesHaveBeenUploaded:Email not sent because it is inactive");
				}
				else
				{
					INotetakingClientManager notetakingClientManager = new NotetakingClientManager();
					IList<ServiceRequestBaseDTO> list = notetakingClientManager.LoadUniqueStudentsReceivingNotes(NotetakerId, LuCourseId);
					CWLogger.Logger.Trace("Common.UI.ClientManager.Web.Core.Impl.Notetaking.NotetakingWebClientManager.NotifyStudentsNewLectureNotesHaveBeenUploaded:SendingNoticesTo:{0}", string.Join(", ", (from g in list
					select ((g.Student == null) ? "NULL" : g.Student.PersonId.ToString()) + "/" + ((g.CourseBase == null) ? "NULL" : g.CourseBase.LuCourseId.ToString())).ToArray<string>()));
					Dictionary<string, string> args = new Dictionary<string, string>
					{
						{
							"lecturedate",
							LectureDate.Date.ToString("yyyy MMM d")
						}
					}.InsertBaseUserMailMergeValues();
					IEmailClientManager emailClientManager = new EmailClientManager();
					foreach (ServiceRequestBaseDTO serviceRequestBaseDTO in list)
					{
						MailMergeContextDTO context = new MailMergeContextDTO
						{
							ServiceProviderId = NotetakerId,
							PersonId = ((serviceRequestBaseDTO.Student == null) ? 0 : serviceRequestBaseDTO.Student.PersonId),
							LuCourseId = ((serviceRequestBaseDTO.CourseBase == null) ? 0 : serviceRequestBaseDTO.CourseBase.LuCourseId)
						};
						emailClientManager.SendEmail(Setting.NOTETAKINGB_Email_NotetakerUploadedNewNotes, new MailMergeContextWithCustomDictionaryDTO
						{
							Context = context,
							CustomDictionary = new MailMergeCustomDictionaryDTO
							{
								Args = args
							}
						}, "Notetaking_NotifyStudentNotetakerUploadedNewNotes");
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.UI.ClientManager.Web.Core.Impl.Notetaking.NotetakingWebClientManager.NotifyStudentsNewLectureNotesHaveBeenUploaded:err={0}", ex.ToString());
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005340 File Offset: 0x00003540
		public bool UploadLectureNote(Stream sFile, int sizeInBytes, string docName, string notes, int notetakerID, int courseID, DateTime lectureDate, bool isSampleNotes, out Exception ex)
		{
			bool result;
			try
			{
				byte[] array = new byte[sizeInBytes];
				sFile.Read(array, 0, sizeInBytes);
				INotetakingClientManager notetakingClientManager = new NotetakingClientManager();
				int num = notetakingClientManager.CreateLectureNote(new LectureNoteDTO
				{
					LectureNoteDocument = new BinaryFileDTO
					{
						ByteArray = array,
						FileSize = sizeInBytes,
						FileName = (docName ?? "")
					},
					LectureNoteDescription = new LectureNoteDescriptionDTO
					{
						LectureDate = lectureDate,
						Comment = notes,
						DateUploaded = DateTime.Now,
						Filename = (docName ?? ""),
						CourseBaseInfo = new LookupCourseBaseDTO
						{
							LuCourseId = courseID
						},
						NotetakerBaseInfo = new NotetakerBaseDTO
						{
							ServiceProviderId = notetakerID
						}
					}
				});
				bool flag = num < 1;
				if (flag)
				{
					throw new Exception("Unable to upload lecture note.");
				}
				ex = null;
				result = true;
			}
			catch (Exception ex2)
			{
				ex = ex2;
				CWLogger.Logger.Error("NotetakingWebClientManager.UploadLectureNote:err={0}", ex2.ToString());
				result = false;
			}
			return result;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005458 File Offset: 0x00003658
		public bool DownloadLectureNoteToBrowser(int docID)
		{
			INotetakingClientManager notetakingClientManager = new NotetakingClientManager();
			LectureNoteDTO lectureNoteDTO = notetakingClientManager.LoadLectureNoteById(docID);
			bool flag = lectureNoteDTO == null || lectureNoteDTO.LectureNoteDocument == null || lectureNoteDTO.LectureNoteDocument.ByteArray == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				byte[] byteArray = lectureNoteDTO.LectureNoteDocument.ByteArray;
				string fileName = lectureNoteDTO.LectureNoteDocument.FileName;
				int fileSize = lectureNoteDTO.LectureNoteDocument.FileSize;
				HttpResponse response = HttpContext.Current.Response;
				response.Buffer = true;
				response.ClearHeaders();
				response.Clear();
				response.ContentType = "binary/octet-stream";
				response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
				response.AddHeader("Content-Length", fileSize.ToString());
				response.BinaryWrite(byteArray);
				response.Flush();
				response.Close();
				result = true;
			}
			return result;
		}
	}
}
