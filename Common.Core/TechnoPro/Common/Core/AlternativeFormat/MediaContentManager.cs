using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common.Web;
using TechnoPro.Common.Core.AlternativeFormat.BookSearch;
using TechnoPro.Common.Core.Mappers.AlternativeFormat.BookSearch;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.AlternativeFormat;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.AlternativeFormat;
using TechnoPro.Common.Graphics;
using TechnoPro.Common.ICore.AlternativeFormat;
using TechnoPro.Common.ICore.AlternativeFormat.BookSearch;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.AlternativeFormat
{
	// Token: 0x02000158 RID: 344
	public class MediaContentManager : IMediaContentManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000F4B RID: 3915 RVA: 0x0000672B File Offset: 0x0000492B
		public MediaContentManager()
		{
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00071E4D File Offset: 0x0007004D
		public MediaContentManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x00071E5F File Offset: 0x0007005F
		// (set) Token: 0x06000F4E RID: 3918 RVA: 0x00071E67 File Offset: 0x00070067
		public OperationContext OpContext { get; set; }

		// Token: 0x06000F4F RID: 3919 RVA: 0x00071E70 File Offset: 0x00070070
		public IList<MediaContent> GetMediaContentMatching(string searchText, int lucourseid = 0)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
			mediaContentDAO.OpContext = this.OpContext;
			return oldUserSettingManager.GetSettingValue_Bool(this.OpContext.WhoAmI, eSettingCode.SETTING_AlternateFormat_UserDefinedEquivalentCoursesFunction) ? mediaContentDAO.GetMediaContentMatchingUsingUserDefinedEquivalentCoursesAlt(searchText, lucourseid) : mediaContentDAO.GetMediaContentMatchingUsingEquivalentCoursesAlt(searchText, lucourseid);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00071ECC File Offset: 0x000700CC
		[DebuggerStepThrough]
		public Task<IList<MediaContent>> GetMediaContentMatchingAsync(string searchText, int lucourseid = 0)
		{
			MediaContentManager.<GetMediaContentMatchingAsync>d__10 <GetMediaContentMatchingAsync>d__ = new MediaContentManager.<GetMediaContentMatchingAsync>d__10();
			<GetMediaContentMatchingAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<MediaContent>>.Create();
			<GetMediaContentMatchingAsync>d__.<>4__this = this;
			<GetMediaContentMatchingAsync>d__.searchText = searchText;
			<GetMediaContentMatchingAsync>d__.lucourseid = lucourseid;
			<GetMediaContentMatchingAsync>d__.<>1__state = -1;
			<GetMediaContentMatchingAsync>d__.<>t__builder.Start<MediaContentManager.<GetMediaContentMatchingAsync>d__10>(ref <GetMediaContentMatchingAsync>d__);
			return <GetMediaContentMatchingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00071F20 File Offset: 0x00070120
		public MediaContent LoadMediaContentById(Guid mediaContentId)
		{
			IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
			mediaContentDAO.OpContext = this.OpContext;
			return mediaContentDAO.LoadMediaContentById(mediaContentId);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00071F4C File Offset: 0x0007014C
		public MediaContent LoadMediaContentByIdentifier(MediaContentIdentifier identifier)
		{
			MediaContent mediaContent = null;
			IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
			mediaContentDAO.OpContext = this.OpContext;
			bool flag = identifier.MediaContentUniqueId != null;
			if (flag)
			{
				mediaContent = mediaContentDAO.LoadMediaContentById(identifier.MediaContentUniqueId.Value);
			}
			bool flag2 = mediaContent == null && !string.IsNullOrEmpty(identifier.ISBN);
			if (flag2)
			{
				mediaContent = mediaContentDAO.LoadMediaContentByISBN(identifier.ISBN);
			}
			bool flag3 = !string.IsNullOrEmpty(identifier.ExternalSourceProvider);
			if (flag3)
			{
				IBookSearchManager bookSearchManager = new BookSearchManager(this.OpContext);
				bool flag4 = mediaContent == null && !string.IsNullOrEmpty(identifier.ExternalId);
				if (flag4)
				{
					mediaContent = bookSearchManager.GetVolumeById(identifier.ExternalId, eBookSearchProviderType.All).ConvertToMediaContent();
				}
				bool flag5 = mediaContent == null && !string.IsNullOrEmpty(identifier.ISBN);
				if (flag5)
				{
					mediaContent = bookSearchManager.GetVolumeByISBN(identifier.ISBN, eBookSearchProviderType.All).ConvertToMediaContent();
				}
			}
			return mediaContent;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00072044 File Offset: 0x00070244
		public MediaContent LoadMediaContentByISBN(string isbn)
		{
			IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
			mediaContentDAO.OpContext = this.OpContext;
			return mediaContentDAO.LoadMediaContentByISBN(isbn);
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00072070 File Offset: 0x00070270
		public IList<MediaContent> LoadMediaContentByTitle(string title)
		{
			IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
			mediaContentDAO.OpContext = this.OpContext;
			return mediaContentDAO.LoadMediaContentByTitle(title);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0007209C File Offset: 0x0007029C
		public IList<MediaContent> LoadMediaContentByCourse(int courseId)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
			mediaContentDAO.OpContext = this.OpContext;
			return oldUserSettingManager.GetSettingValue_Bool(this.OpContext.WhoAmI, eSettingCode.SETTING_AlternateFormat_UserDefinedEquivalentCoursesFunction) ? mediaContentDAO.LoadMediaContentByCourseUsingUserDefinedEquivalentCoursesAlt(courseId) : mediaContentDAO.LoadMediaContentByCourseUsingEquivalentCoursesAlt(courseId);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x000720F8 File Offset: 0x000702F8
		public IList<MediaContent> LoadMediaContentByPublisher(int publisherId)
		{
			IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
			mediaContentDAO.OpContext = this.OpContext;
			return mediaContentDAO.LoadMediaContentByPublisher(publisherId);
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00072124 File Offset: 0x00070324
		public IList<MediaContent> LoadMediaContentByCategory(eMediaContentCategory mediaContentCategory)
		{
			IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
			mediaContentDAO.OpContext = this.OpContext;
			return mediaContentDAO.LoadMediaContentByCategory(mediaContentCategory);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00072150 File Offset: 0x00070350
		public MediaContentIdentifier CreateMediaContent(MediaContent mediaContent)
		{
			MediaContentManager.<>c__DisplayClass18_0 CS$<>8__locals1 = new MediaContentManager.<>c__DisplayClass18_0();
			CS$<>8__locals1.mediaContent = mediaContent;
			CS$<>8__locals1.<>4__this = this;
			bool flag = CS$<>8__locals1.mediaContent.Publisher != null && CS$<>8__locals1.mediaContent.Publisher.PublisherId == 0 && !string.IsNullOrEmpty(CS$<>8__locals1.mediaContent.Publisher.Name);
			if (flag)
			{
				IMediaPublisherManager mediaPublisherManager = new MediaPublisherManager(this.OpContext);
				MediaPublisher mediaPublisher = mediaPublisherManager.LoadPublisherByName(CS$<>8__locals1.mediaContent.Publisher.Name);
				bool flag2 = mediaPublisher == null;
				if (flag2)
				{
					CS$<>8__locals1.mediaContent.Publisher.PublisherId = mediaPublisherManager.CreatePublisher(CS$<>8__locals1.mediaContent.Publisher);
				}
				else
				{
					CS$<>8__locals1.mediaContent.Publisher = mediaPublisher;
				}
			}
			IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
			mediaContentDAO.OpContext = this.OpContext;
			CS$<>8__locals1.id = mediaContentDAO.CreateMediaContent(CS$<>8__locals1.mediaContent);
			bool flag3 = CS$<>8__locals1.id != null && CS$<>8__locals1.id.MediaContentUniqueId != null && !string.IsNullOrEmpty(CS$<>8__locals1.mediaContent.ThumbnailImageUrl);
			if (flag3)
			{
				Task.Run(delegate()
				{
					MediaContentManager.<>c__DisplayClass18_0.<<CreateMediaContent>b__0>d <<CreateMediaContent>b__0>d = new MediaContentManager.<>c__DisplayClass18_0.<<CreateMediaContent>b__0>d();
					<<CreateMediaContent>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<CreateMediaContent>b__0>d.<>4__this = CS$<>8__locals1;
					<<CreateMediaContent>b__0>d.<>1__state = -1;
					<<CreateMediaContent>b__0>d.<>t__builder.Start<MediaContentManager.<>c__DisplayClass18_0.<<CreateMediaContent>b__0>d>(ref <<CreateMediaContent>b__0>d);
					return <<CreateMediaContent>b__0>d.<>t__builder.Task;
				});
			}
			return CS$<>8__locals1.id;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00072294 File Offset: 0x00070494
		public void UpdateMediaContent(MediaContent mediaContent)
		{
			IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
			mediaContentDAO.OpContext = this.OpContext;
			mediaContentDAO.UpdateMediaContent(mediaContent);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x000722C0 File Offset: 0x000704C0
		public bool DeleteMediaContent(Guid mediaContentId)
		{
			IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
			mediaContentDAO.OpContext = this.OpContext;
			return mediaContentDAO.DeleteMediaContent(mediaContentId);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x000722EC File Offset: 0x000704EC
		public IList<MediaContent> GetAllMediaContent()
		{
			IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
			mediaContentDAO.OpContext = this.OpContext;
			return mediaContentDAO.GetAllMediaContent();
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00072318 File Offset: 0x00070518
		public MediaContentPerFormatInfo GetMediaContentPerFormatInfoById(int mediaContentPerFormat)
		{
			IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
			mediaContentDAO.OpContext = this.OpContext;
			return mediaContentDAO.GetMediaContentPerFormatInfoById(mediaContentPerFormat);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00072344 File Offset: 0x00070544
		public IList<MediaContentPerFormatInfo> LoadMediaContentPerFormatInfoByMediaContent(Guid mediaContentId)
		{
			IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
			mediaContentDAO.OpContext = this.OpContext;
			return mediaContentDAO.LoadMediaContentPerFormatInfoByMediaContent(mediaContentId);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00072370 File Offset: 0x00070570
		public MediaContentPerFormatStatusInfo GetMediaContentPerFormatStatus(int mediaContentPerFormatId, int studentId, bool checkIfAlreadyExits = true)
		{
			IMediaJobManager mediaJobManager = new MediaJobManager(this.OpContext);
			IMediaContentFileManager mediaContentFileManager = new MediaContentFileManager(this.OpContext);
			if (checkIfAlreadyExits)
			{
				IStudentMediaRequestDAO studentMediaRequestDAO = new StudentMediaRequestDAO(this.OpContext);
				MediaContentRequestedInfo mediaContentRequestedInfo = studentMediaRequestDAO.LoadMediaContentRequestInfoByMediaContentPerFormatAndStudent(studentId, mediaContentPerFormatId);
				bool flag = mediaContentRequestedInfo != null;
				if (flag)
				{
					return new MediaContentPerFormatStatusInfo
					{
						MediaContentPerFormatId = mediaContentPerFormatId,
						Status = eMediaContentPerFormatStatus.Request_already_done_by_student,
						MediaContentFormat = mediaContentRequestedInfo.ContentDetailRequested.MediaContentFormat
					};
				}
			}
			int countActiveMediaJobByMediaContentPerFormatId = mediaJobManager.GetCountActiveMediaJobByMediaContentPerFormatId(mediaContentPerFormatId, studentId);
			int countAvailableAlternateFormatFiles = mediaContentFileManager.GetCountAvailableAlternateFormatFiles(mediaContentPerFormatId, studentId);
			MediaContentPerFormatInfo mediaContentPerFormatInfoById = this.GetMediaContentPerFormatInfoById(mediaContentPerFormatId);
			bool flag2 = countActiveMediaJobByMediaContentPerFormatId > 0;
			MediaContentPerFormatStatusInfo result;
			if (flag2)
			{
				bool flag3 = countAvailableAlternateFormatFiles > 0;
				if (flag3)
				{
					IList<MediaJob> activeMediaJobByMediaContentPerFormatId = mediaJobManager.GetActiveMediaJobByMediaContentPerFormatId(mediaContentPerFormatId, studentId);
					IList<CompletedMediaJob> completedMediaJobByMediaContentPerFormatId = mediaJobManager.GetCompletedMediaJobByMediaContentPerFormatId(mediaContentPerFormatId, studentId);
					MediaContentPerFormatStatusInfo mediaContentPerFormatStatusInfo = new MediaContentPerFormatStatusInfo();
					mediaContentPerFormatStatusInfo.Status = eMediaContentPerFormatStatus.Partially_completed;
					IList<int> inProgressJobIds;
					if (activeMediaJobByMediaContentPerFormatId == null || activeMediaJobByMediaContentPerFormatId.Count <= 0)
					{
						inProgressJobIds = null;
					}
					else
					{
						inProgressJobIds = (from j in activeMediaJobByMediaContentPerFormatId
						select j.MediaJobId).ToList<int>();
					}
					mediaContentPerFormatStatusInfo.InProgressJobIds = inProgressJobIds;
					IList<int> completedJobIds;
					if (completedMediaJobByMediaContentPerFormatId == null || completedMediaJobByMediaContentPerFormatId.Count <= 0)
					{
						completedJobIds = null;
					}
					else
					{
						completedJobIds = (from j in completedMediaJobByMediaContentPerFormatId
						select j.MediaJobId).ToList<int>();
					}
					mediaContentPerFormatStatusInfo.CompletedJobIds = completedJobIds;
					mediaContentPerFormatStatusInfo.MediaContentPerFormatId = mediaContentPerFormatId;
					mediaContentPerFormatStatusInfo.MediaContentFormat = mediaContentPerFormatInfoById.MediaContentFormat;
					result = mediaContentPerFormatStatusInfo;
				}
				else
				{
					IList<MediaJob> activeMediaJobByMediaContentPerFormatId2 = mediaJobManager.GetActiveMediaJobByMediaContentPerFormatId(mediaContentPerFormatId, studentId);
					MediaContentPerFormatStatusInfo mediaContentPerFormatStatusInfo2 = new MediaContentPerFormatStatusInfo();
					mediaContentPerFormatStatusInfo2.Status = eMediaContentPerFormatStatus.Files_in_progress;
					IList<int> inProgressJobIds2;
					if (activeMediaJobByMediaContentPerFormatId2 == null || activeMediaJobByMediaContentPerFormatId2.Count <= 0)
					{
						inProgressJobIds2 = null;
					}
					else
					{
						inProgressJobIds2 = (from j in activeMediaJobByMediaContentPerFormatId2
						select j.MediaJobId).ToList<int>();
					}
					mediaContentPerFormatStatusInfo2.InProgressJobIds = inProgressJobIds2;
					mediaContentPerFormatStatusInfo2.MediaContentPerFormatId = mediaContentPerFormatId;
					mediaContentPerFormatStatusInfo2.MediaContentFormat = mediaContentPerFormatInfoById.MediaContentFormat;
					result = mediaContentPerFormatStatusInfo2;
				}
			}
			else
			{
				bool flag4 = countAvailableAlternateFormatFiles > 0;
				if (flag4)
				{
					result = new MediaContentPerFormatStatusInfo
					{
						Status = eMediaContentPerFormatStatus.Completed,
						MediaContentPerFormatId = mediaContentPerFormatId,
						MediaContentFormat = mediaContentPerFormatInfoById.MediaContentFormat
					};
				}
				else
				{
					result = new MediaContentPerFormatStatusInfo
					{
						Status = eMediaContentPerFormatStatus.No_files_available,
						MediaContentPerFormatId = mediaContentPerFormatId,
						MediaContentFormat = mediaContentPerFormatInfoById.MediaContentFormat
					};
				}
			}
			return result;
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x000725C0 File Offset: 0x000707C0
		public MediaContentPerFormatStatusInfo GetMediaContentPerFormatStatus(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentId, bool checkIfAlreadyExits = true)
		{
			IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
			mediaContentDAO.OpContext = this.OpContext;
			return this.GetMediaContentPerFormatStatus(mediaContentDAO.GetMediaContentPerFormatId(mediaContentId, mediaContentFormat), studentId, checkIfAlreadyExits);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x000725F8 File Offset: 0x000707F8
		public IList<MediaContentPerFormatStatusInfo> GetMediaContentPerFormatStatusList(Guid mediaContentId, int studentId)
		{
			List<MediaContentPerFormatStatusInfo> list = new List<MediaContentPerFormatStatusInfo>();
			IList<MediaContentPerFormatInfo> list2 = this.LoadMediaContentPerFormatInfoByMediaContent(mediaContentId);
			List<string> processedFormats = new List<string>();
			foreach (MediaContentPerFormatInfo mediaContentPerFormatInfo in list2)
			{
				processedFormats.Add(mediaContentPerFormatInfo.MediaContentFormat.ToString());
				MediaContentPerFormatStatusInfo mediaContentPerFormatStatus = this.GetMediaContentPerFormatStatus(mediaContentPerFormatInfo.MediaContentPerFormatId, studentId, true);
				bool flag = mediaContentPerFormatStatus != null && mediaContentPerFormatStatus.Status != eMediaContentPerFormatStatus.Request_already_done_by_student;
				if (flag)
				{
					list.Add(mediaContentPerFormatStatus);
				}
			}
			list.AddRange(from mediaContentFormat in Enum.GetNames(typeof(MediaContentFormat))
			where !processedFormats.Contains(mediaContentFormat)
			select new MediaContentPerFormatStatusInfo
			{
				MediaContentPerFormatId = 0,
				MediaContentFormat = (MediaContentFormat)Enum.Parse(typeof(MediaContentFormat), mediaContentFormat),
				Status = eMediaContentPerFormatStatus.No_files_available
			});
			return list;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00072708 File Offset: 0x00070908
		public Image GetMediaContentThumbnail(MediaContentIdentifier identifier)
		{
			Image image = null;
			bool flag = identifier.MediaContentUniqueId != null && identifier.MediaContentUniqueId.Value != Guid.Empty;
			if (flag)
			{
				IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
				mediaContentDAO.OpContext = this.OpContext;
				image = mediaContentDAO.GetMediaContentThumbnail(identifier.MediaContentUniqueId.Value);
			}
			bool flag2 = image != null;
			Image result;
			if (flag2)
			{
				result = image;
			}
			else
			{
				bool flag3 = string.IsNullOrEmpty(identifier.ExternalSourceProvider) || string.IsNullOrEmpty(identifier.ExternalId) || !Enum.IsDefined(typeof(eBookSearchProviderName), identifier.ExternalSourceProvider);
				if (flag3)
				{
					result = null;
				}
				else
				{
					IBookSearchManager bookSearchManager = new BookSearchManager(this.OpContext);
					EBookSearchResult volumeById = bookSearchManager.GetVolumeById(identifier.ExternalId, eBookSearchProviderType.All);
					result = ((volumeById != null && !string.IsNullOrEmpty(volumeById.ThumbnailUrl)) ? volumeById.ThumbnailUrl.GetImageFromUrl().ResizeImageKeepAspectRatio(MediaContentManager.THUMBNAIL_IMAGE_SIZE) : null);
				}
			}
			return result;
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x00072814 File Offset: 0x00070A14
		public byte[] GetMediaContentThumbnailBytes(MediaContentIdentifier identifier)
		{
			byte[] array = null;
			bool flag = identifier.MediaContentUniqueId != null && identifier.MediaContentUniqueId.Value != Guid.Empty;
			if (flag)
			{
				IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
				mediaContentDAO.OpContext = this.OpContext;
				array = mediaContentDAO.GetMediaContentThumbnailBytes(identifier.MediaContentUniqueId.Value);
			}
			bool flag2 = array != null;
			byte[] result;
			if (flag2)
			{
				result = array;
			}
			else
			{
				bool flag3 = string.IsNullOrEmpty(identifier.ExternalSourceProvider) || string.IsNullOrEmpty(identifier.ExternalId) || !Enum.IsDefined(typeof(eBookSearchProviderName), identifier.ExternalSourceProvider);
				if (flag3)
				{
					result = null;
				}
				else
				{
					IBookSearchManager bookSearchManager = new BookSearchManager(this.OpContext);
					EBookSearchResult volumeById = bookSearchManager.GetVolumeById(identifier.ExternalId, eBookSearchProviderType.All);
					result = ((volumeById != null && !string.IsNullOrEmpty(volumeById.ThumbnailUrl)) ? volumeById.ThumbnailUrl.GetImageFromUrl().ResizeImageKeepAspectRatio(MediaContentManager.THUMBNAIL_IMAGE_SIZE).Serialize() : null);
				}
			}
			return result;
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00072924 File Offset: 0x00070B24
		[DebuggerStepThrough]
		public Task<byte[]> GetMediaContentThumbnailBytesAsync(MediaContentIdentifier identifier)
		{
			MediaContentManager.<GetMediaContentThumbnailBytesAsync>d__29 <GetMediaContentThumbnailBytesAsync>d__ = new MediaContentManager.<GetMediaContentThumbnailBytesAsync>d__29();
			<GetMediaContentThumbnailBytesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<byte[]>.Create();
			<GetMediaContentThumbnailBytesAsync>d__.<>4__this = this;
			<GetMediaContentThumbnailBytesAsync>d__.identifier = identifier;
			<GetMediaContentThumbnailBytesAsync>d__.<>1__state = -1;
			<GetMediaContentThumbnailBytesAsync>d__.<>t__builder.Start<MediaContentManager.<GetMediaContentThumbnailBytesAsync>d__29>(ref <GetMediaContentThumbnailBytesAsync>d__);
			return <GetMediaContentThumbnailBytesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x00072970 File Offset: 0x00070B70
		public void SetMediaContentThumbnail(Guid mediaContentId, Image thumbnail)
		{
			Image thumbnail2 = null;
			bool flag = thumbnail != null;
			if (flag)
			{
				thumbnail2 = thumbnail.ResizeImageKeepAspectRatio(MediaContentManager.THUMBNAIL_IMAGE_SIZE);
			}
			IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
			mediaContentDAO.OpContext = this.OpContext;
			mediaContentDAO.SetMediaContentThumbnail(mediaContentId, thumbnail2);
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x000729B0 File Offset: 0x00070BB0
		[DebuggerStepThrough]
		public Task SetMediaContentThumbnailAsync(Guid mediaContentId, Image thumbnail)
		{
			MediaContentManager.<SetMediaContentThumbnailAsync>d__31 <SetMediaContentThumbnailAsync>d__ = new MediaContentManager.<SetMediaContentThumbnailAsync>d__31();
			<SetMediaContentThumbnailAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SetMediaContentThumbnailAsync>d__.<>4__this = this;
			<SetMediaContentThumbnailAsync>d__.mediaContentId = mediaContentId;
			<SetMediaContentThumbnailAsync>d__.thumbnail = thumbnail;
			<SetMediaContentThumbnailAsync>d__.<>1__state = -1;
			<SetMediaContentThumbnailAsync>d__.<>t__builder.Start<MediaContentManager.<SetMediaContentThumbnailAsync>d__31>(ref <SetMediaContentThumbnailAsync>d__);
			return <SetMediaContentThumbnailAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00072A04 File Offset: 0x00070C04
		public Image GetMediaContentCoverImage(MediaContentIdentifier identifier)
		{
			Image image = null;
			bool flag = identifier.MediaContentUniqueId != null && identifier.MediaContentUniqueId.Value != Guid.Empty;
			if (flag)
			{
				IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
				mediaContentDAO.OpContext = this.OpContext;
				image = mediaContentDAO.GetMediaContentCoverImage(identifier.MediaContentUniqueId.Value);
			}
			bool flag2 = image != null;
			Image result;
			if (flag2)
			{
				result = image;
			}
			else
			{
				bool flag3 = string.IsNullOrEmpty(identifier.ExternalSourceProvider) || string.IsNullOrEmpty(identifier.ExternalId) || !Enum.IsDefined(typeof(eBookSearchProviderName), identifier.ExternalSourceProvider);
				if (flag3)
				{
					result = null;
				}
				else
				{
					IBookSearchManager bookSearchManager = new BookSearchManager(this.OpContext);
					EBookSearchResult volumeById = bookSearchManager.GetVolumeById(identifier.ExternalId, eBookSearchProviderType.All);
					result = ((volumeById != null && !string.IsNullOrEmpty(volumeById.CoverImageUrl)) ? volumeById.CoverImageUrl.GetImageFromUrl() : null);
				}
			}
			return result;
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x00072B00 File Offset: 0x00070D00
		public byte[] GetMediaContentCoverImageBytes(MediaContentIdentifier identifier)
		{
			byte[] array = null;
			bool flag = identifier.MediaContentUniqueId != null && identifier.MediaContentUniqueId.Value != Guid.Empty;
			if (flag)
			{
				IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
				mediaContentDAO.OpContext = this.OpContext;
				array = mediaContentDAO.GetMediaContentCoverImageBytes(identifier.MediaContentUniqueId.Value);
			}
			bool flag2 = array != null;
			byte[] result;
			if (flag2)
			{
				result = array;
			}
			else
			{
				bool flag3 = string.IsNullOrEmpty(identifier.ExternalSourceProvider) || string.IsNullOrEmpty(identifier.ExternalId) || !Enum.IsDefined(typeof(eBookSearchProviderName), identifier.ExternalSourceProvider);
				if (flag3)
				{
					result = null;
				}
				else
				{
					IBookSearchManager bookSearchManager = new BookSearchManager(this.OpContext);
					EBookSearchResult volumeById = bookSearchManager.GetVolumeById(identifier.ExternalId, eBookSearchProviderType.All);
					result = ((volumeById != null && !string.IsNullOrEmpty(volumeById.CoverImageUrl)) ? volumeById.CoverImageUrl.GetImageFromUrl().Serialize() : null);
				}
			}
			return result;
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x00072C04 File Offset: 0x00070E04
		public void SetMediaContentCover(Guid mediaContentId, Image cover)
		{
			Image thumbnail = (cover != null) ? cover.ResizeImageKeepAspectRatio(MediaContentManager.THUMBNAIL_IMAGE_SIZE) : null;
			IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
			mediaContentDAO.OpContext = this.OpContext;
			mediaContentDAO.SetMediaContentCoverImage(mediaContentId, cover, thumbnail);
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00072C44 File Offset: 0x00070E44
		public IList<LookupCourseBase> GetMediaContentCourses(Guid mediaContentId)
		{
			IMediaContentDAO mediaContentDAO = ObjectFactory.Resolve<IMediaContentDAO>();
			mediaContentDAO.OpContext = this.OpContext;
			return mediaContentDAO.GetMediaContentCourses(mediaContentId);
		}

		// Token: 0x040002C5 RID: 709
		public const int THUMBNAIL_IMAGE_WITDH = 100;

		// Token: 0x040002C6 RID: 710
		public const int THUMBNAIL_IMAGE_HEIGHT = 100;

		// Token: 0x040002C7 RID: 711
		public static readonly Size THUMBNAIL_IMAGE_SIZE = new Size(100, 100);
	}
}
