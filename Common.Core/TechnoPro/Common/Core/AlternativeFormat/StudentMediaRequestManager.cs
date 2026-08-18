using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.Common.Core.AlternativeFormat.Adapters;
using TechnoPro.Common.Core.AlternativeFormat.BookSearch;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.Mappers.AlternativeFormat.BookSearch;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.AlternativeFormat;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.AlternativeFormat;
using TechnoPro.Common.ICore.AlternativeFormat;
using TechnoPro.Common.ICore.AlternativeFormat.BookSearch;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat.Adapters;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Core.AlternativeFormat
{
	// Token: 0x0200015E RID: 350
	public class StudentMediaRequestManager : IStudentMediaRequestManager, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00073902 File Offset: 0x00071B02
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x0007390A File Offset: 0x00071B0A
		private IStudentMediaRequestDAO StudentMediaRequestDAO { get; set; }

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00073913 File Offset: 0x00071B13
		public StudentMediaRequestManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.StudentMediaRequestDAO = new StudentMediaRequestDAO(opContext);
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x00073932 File Offset: 0x00071B32
		// (set) Token: 0x06000FCB RID: 4043 RVA: 0x0007393A File Offset: 0x00071B3A
		public OperationContext OpContext { get; set; }

		// Token: 0x06000FCC RID: 4044 RVA: 0x00073944 File Offset: 0x00071B44
		public StudentMediaRequest CreateStudentMediaRequest(StudentMediaRequest studentMediaRequest)
		{
			studentMediaRequest = this.StudentMediaRequestDAO.CreateStudentMediaRequest(studentMediaRequest);
			Dictionary<Guid, ProofOfPurchaseInfo> dictionary = new Dictionary<Guid, ProofOfPurchaseInfo>();
			foreach (MediaContentRequestedInfo mediaContentRequestedInfo in studentMediaRequest.ContentRequestedList)
			{
				try
				{
					mediaContentRequestedInfo.CreatedDatetime = studentMediaRequest.CreatedDatetime;
					mediaContentRequestedInfo.StudentRequestId = studentMediaRequest.StudentMediaRequestId;
					bool isANewUserCreatedMediaContent = mediaContentRequestedInfo.ContentDetailRequested.IsANewUserCreatedMediaContent;
					if (isANewUserCreatedMediaContent)
					{
						MediaContent mediaContent = new MediaContent(mediaContentRequestedInfo.ContentDetailRequested.MediaContent)
						{
							DateCreated = DateTime.Now,
							WhoEntered = studentMediaRequest.RequestMadeFromStudent
						};
						IMediaContentManager mediaContentManager = new MediaContentManager(this.OpContext);
						MediaContentIdentifier mediaContentIdentifier = mediaContentRequestedInfo.ContentDetailRequested.MediaContent.Identifier = mediaContentManager.CreateMediaContent(mediaContent);
						Guid? mediaContentUniqueId = mediaContentIdentifier.MediaContentUniqueId;
						Guid empty = Guid.Empty;
						bool flag = mediaContentUniqueId != null && (mediaContentUniqueId == null || mediaContentUniqueId.GetValueOrDefault() == empty);
						if (flag)
						{
							CWLogger.Logger.Error(string.Format("StudentMediaRequestManager::CreateStudentMediaRequest:: Creating student media request detail failed: Title='{0}', ISBN='{1}', ExternalId='{2}', ExternalProvider='{3}'", new object[]
							{
								mediaContentRequestedInfo.ContentDetailRequested.MediaContent.ShortTitle ?? "NULL",
								mediaContentRequestedInfo.ContentDetailRequested.MediaContent.ISBN ?? "NULL",
								mediaContentRequestedInfo.ContentDetailRequested.MediaContent.Identifier.ExternalId ?? "NULL",
								mediaContentRequestedInfo.ContentDetailRequested.MediaContent.Identifier.ExternalSourceProvider ?? "NULL"
							}));
							continue;
						}
					}
					else
					{
						bool flag2 = mediaContentRequestedInfo.ContentDetailRequested.MediaContent.MediaContentUniqueId == Guid.Empty && !string.IsNullOrEmpty(mediaContentRequestedInfo.ContentDetailRequested.MediaContent.Identifier.ExternalSourceProvider) && Enum.IsDefined(typeof(eBookSearchProviderName), mediaContentRequestedInfo.ContentDetailRequested.MediaContent.Identifier.ExternalSourceProvider);
						if (flag2)
						{
							IBookSearchManager bookSearchManager = new BookSearchManager(this.OpContext);
							EBookSearchResult volumeById = bookSearchManager.GetVolumeById(mediaContentRequestedInfo.ContentDetailRequested.MediaContent.Identifier.ExternalId, eBookSearchProviderType.All);
							bool flag3 = volumeById == null;
							if (flag3)
							{
								CWLogger.Logger.Error(string.Format("StudentMediaRequestManager::CreateStudentMediaRequest:: Creating student media request detail failed: Title='{0}', ISBN='{1}', ExternalId='{2}', ExternalProvider='{3}'", new object[]
								{
									mediaContentRequestedInfo.ContentDetailRequested.MediaContent.ShortTitle ?? "NULL",
									mediaContentRequestedInfo.ContentDetailRequested.MediaContent.ISBN ?? "NULL",
									mediaContentRequestedInfo.ContentDetailRequested.MediaContent.Identifier.ExternalId ?? "NULL",
									mediaContentRequestedInfo.ContentDetailRequested.MediaContent.Identifier.ExternalSourceProvider ?? "NULL"
								}));
								continue;
							}
							MediaContent mediaContent = volumeById.ConvertToMediaContent();
							mediaContent.WhoEntered = studentMediaRequest.RequestMadeFromStudent;
							IMediaContentManager mediaContentManager2 = new MediaContentManager(this.OpContext);
							MediaContentIdentifier mediaContentIdentifier2 = mediaContentRequestedInfo.ContentDetailRequested.MediaContent.Identifier = mediaContentManager2.CreateMediaContent(mediaContent);
							Guid? mediaContentUniqueId = mediaContentIdentifier2.MediaContentUniqueId;
							Guid empty = Guid.Empty;
							bool flag4 = mediaContentUniqueId != null && (mediaContentUniqueId == null || mediaContentUniqueId.GetValueOrDefault() == empty);
							if (flag4)
							{
								CWLogger.Logger.Error(string.Format("StudentMediaRequestManager::CreateStudentMediaRequest:: Creating student media request detail failed: Title='{0}', ISBN='{1}', ExternalId='{2}', ExternalProvider='{3}'", new object[]
								{
									mediaContentRequestedInfo.ContentDetailRequested.MediaContent.ShortTitle ?? "NULL",
									mediaContentRequestedInfo.ContentDetailRequested.MediaContent.ISBN ?? "NULL",
									mediaContentRequestedInfo.ContentDetailRequested.MediaContent.Identifier.ExternalId ?? "NULL",
									mediaContentRequestedInfo.ContentDetailRequested.MediaContent.Identifier.ExternalSourceProvider ?? "NULL"
								}));
								continue;
							}
						}
					}
					int num = mediaContentRequestedInfo.MediaContentRequestedInfoID = this.AddStudentContentMediaRequestInfo(mediaContentRequestedInfo);
					bool flag5 = num > 0;
					if (flag5)
					{
						bool flag6 = mediaContentRequestedInfo.ProofOfPurchase != null && !dictionary.ContainsKey(mediaContentRequestedInfo.ContentDetailRequested.MediaContent.MediaContentUniqueId);
						if (flag6)
						{
							mediaContentRequestedInfo.ProofOfPurchase.MediaContentUniqueId = mediaContentRequestedInfo.ContentDetailRequested.MediaContent.MediaContentUniqueId;
							dictionary.Add(mediaContentRequestedInfo.ProofOfPurchase.MediaContentUniqueId, mediaContentRequestedInfo.ProofOfPurchase);
						}
					}
					else
					{
						CWLogger.Logger.Error(string.Format("StudentMediaRequestManager::CreateStudentMediaRequest:: Creating student media request detail failed: Title='{0}', ISBN='{1}', UniqueId='{2}', ExternalId={3}", new object[]
						{
							mediaContentRequestedInfo.ContentDetailRequested.MediaContent.ShortTitle ?? "NULL",
							mediaContentRequestedInfo.ContentDetailRequested.MediaContent.ISBN ?? "NULL",
							mediaContentRequestedInfo.ContentDetailRequested.MediaContent.Identifier.MediaContentUniqueId.GetValueOrDefault().ToString(),
							mediaContentRequestedInfo.ContentDetailRequested.MediaContent.Identifier.ExternalId ?? "NULL"
						}));
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.ErrorException(string.Format("StudentMediaRequestManager::CreateStudentMediaRequest:: Creating student media request detail failed: Title='{0}', ISBN='{1}', UniqueId='{2}', ExternalId={3}: {4}", new object[]
					{
						mediaContentRequestedInfo.ContentDetailRequested.MediaContent.ShortTitle ?? "NULL",
						mediaContentRequestedInfo.ContentDetailRequested.MediaContent.ISBN ?? "NULL",
						mediaContentRequestedInfo.ContentDetailRequested.MediaContent.Identifier.MediaContentUniqueId.GetValueOrDefault().ToString(),
						mediaContentRequestedInfo.ContentDetailRequested.MediaContent.Identifier.ExternalId ?? "NULL",
						ex.ToString()
					}), ex);
					mediaContentRequestedInfo.RequestStatus = MediaRequestStatus.Created;
					mediaContentRequestedInfo.MediaContentRequestedInfoID = 0;
				}
			}
			foreach (ProofOfPurchaseInfo proofOfPurchaseInfo in dictionary.Values.ToList<ProofOfPurchaseInfo>())
			{
				int num2 = this.UploadProofOfPurchase(proofOfPurchaseInfo);
				bool flag7 = num2 <= 0;
				if (!flag7)
				{
					ProofOfPurchaseInfo info = proofOfPurchaseInfo;
					IEnumerable<MediaContentRequestedInfo> contentRequestedList = studentMediaRequest.ContentRequestedList;
					Func<MediaContentRequestedInfo, bool> predicate;
					Func<MediaContentRequestedInfo, bool> <>9__0;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = ((MediaContentRequestedInfo r) => r.ContentDetailRequested.MediaContent.MediaContentUniqueId == info.MediaContentUniqueId));
					}
					foreach (MediaContentRequestedInfo mediaContentRequestedInfo2 in contentRequestedList.Where(predicate))
					{
						mediaContentRequestedInfo2.ProofOfPurchaseId = num2;
					}
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MediaContentRequestedInfo mediaContentRequestedInfo3 in studentMediaRequest.ContentRequestedList)
			{
				stringBuilder.AppendFormat("   - {0} {1}{2}", mediaContentRequestedInfo3.ContentDetailRequested.MediaContent.ShortTitle ?? string.Empty, (!string.IsNullOrEmpty(mediaContentRequestedInfo3.ContentDetailRequested.MediaContent.ISBN)) ? string.Format("({0})", mediaContentRequestedInfo3.ContentDetailRequested.MediaContent.ISBN.DisplayISBNFormat()) : string.Empty, Environment.NewLine);
				stringBuilder.AppendLine();
			}
			this.NotifyStudentsAsync(new MailMergeContext
			{
				PersonId = studentMediaRequest.RequestMadeFromStudent.PersonId
			}, Setting.ALTERNATEFORMAT_Email_StudentAlternateMediaRequestsNotification, new Dictionary<string, string>
			{
				{
					"alternatemediacontentlist",
					stringBuilder.ToString()
				}
			});
			return studentMediaRequest;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00074198 File Offset: 0x00072398
		public void UpdateStudentMediaRequest(StudentMediaRequest newStudentMediaRequest)
		{
			StudentMediaRequest studentMediaRequest = this.LoadStudentMediaRequestById(newStudentMediaRequest.StudentMediaRequestId);
			bool flag = studentMediaRequest == null;
			if (!flag)
			{
				this.StudentMediaRequestDAO.UpdateStudentMediaRequest(newStudentMediaRequest);
				using (IEnumerator<MediaContentRequestedInfo> enumerator = newStudentMediaRequest.ContentRequestedList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MediaContentRequestedInfo mediaContentRequestedInfo = enumerator.Current;
						bool flag2 = false;
						bool flag3 = studentMediaRequest.ContentRequestedList.Any((MediaContentRequestedInfo mcreq) => mediaContentRequestedInfo.MediaContentRequestedInfoID == mcreq.MediaContentRequestedInfoID);
						if (flag3)
						{
							this.UpdateStudentContentMediaRequestInfo(mediaContentRequestedInfo);
							flag2 = true;
						}
						bool flag4 = !flag2;
						if (flag4)
						{
							mediaContentRequestedInfo.StudentRequestId = newStudentMediaRequest.StudentMediaRequestId;
							mediaContentRequestedInfo.MediaContentRequestedInfoID = this.AddStudentContentMediaRequestInfo(mediaContentRequestedInfo);
						}
					}
				}
				using (IEnumerator<MediaContentRequestedInfo> enumerator2 = studentMediaRequest.ContentRequestedList.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						MediaContentRequestedInfo previousMediaContentDetail = enumerator2.Current;
						bool flag5 = newStudentMediaRequest.ContentRequestedList.All((MediaContentRequestedInfo mcreq) => mcreq.MediaContentRequestedInfoID != previousMediaContentDetail.MediaContentRequestedInfoID);
						if (flag5)
						{
							this.DeleteStudentContentMediaRequestInfo(previousMediaContentDetail);
						}
					}
				}
			}
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x000742FC File Offset: 0x000724FC
		public StudentMediaRequest LoadStudentMediaRequestById(int studentMediaRequestId)
		{
			return this.StudentMediaRequestDAO.LoadStudentMediaRequestById(studentMediaRequestId);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0007431C File Offset: 0x0007251C
		public MediaContentRequestedInfo LoadMediaContentRequestedInfoById(int mediaContentRequestedInfoId)
		{
			return this.StudentMediaRequestDAO.LoadMediaContentRequestInfoById(mediaContentRequestedInfoId);
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0007433C File Offset: 0x0007253C
		public MediaContentRequestedInfo LoadArchiveMediaContentRequestedInfoById(int mediaContentRequestedInfoId)
		{
			return this.StudentMediaRequestDAO.LoadArchiveMediaContentRequestInfoById(mediaContentRequestedInfoId);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0007435C File Offset: 0x0007255C
		public IList<MediaContentRequestedInfo> LoadStudentMediaRequestByStatus(MediaRequestStatus status)
		{
			return this.StudentMediaRequestDAO.LoadStudentMediaRequestByStatus(status);
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0007437C File Offset: 0x0007257C
		public IList<MediaContentRequestedInfo> LoadAllApprovedMediaRequest(int campusId = 0)
		{
			return this.StudentMediaRequestDAO.LoadAllApprovedMediaRequest(campusId);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0007439C File Offset: 0x0007259C
		public IList<MediaContentRequestedInfo> LoadAllToBeApprovedMediaRequest(int campusId = 0)
		{
			return this.StudentMediaRequestDAO.LoadAllToBeApprovedMediaRequest(campusId);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x000743BC File Offset: 0x000725BC
		public IList<MediaContentRequestedInfo> LoadAllToBeApprovedMediaRequestByStudent(int studentId, int campusId = 0)
		{
			return this.StudentMediaRequestDAO.LoadAllToBeApprovedMediaRequestByStudent(studentId, campusId);
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x000743DC File Offset: 0x000725DC
		public IList<MediaContentRequestedInfo> LoadAllCompletedStudentMediaRequest(int campusId = 0)
		{
			return this.StudentMediaRequestDAO.LoadAllCompletedStudentMediaRequest(campusId);
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x000743FC File Offset: 0x000725FC
		public IList<MediaContentRequestedInfo> LoadAllCompletedStudentMediaRequestByStudent(int studentId, int campusId = 0)
		{
			return this.StudentMediaRequestDAO.LoadAllCompletedStudentMediaRequestByStudent(studentId, campusId);
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0007441C File Offset: 0x0007261C
		public IList<MediaContentRequestedInfo> LoadAllCompletedStudentMediaRequest(DateTime startdate, DateTime endDate, int campusId = 0)
		{
			return this.StudentMediaRequestDAO.LoadAllCompletedStudentMediaRequest(startdate, endDate, campusId);
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0007443C File Offset: 0x0007263C
		public IList<MediaContentRequestedInfo> LoadAllCompletedStudentMediaRequestByStudent(int studentId, DateTime startdate, DateTime endDate, int campusId = 0)
		{
			return this.StudentMediaRequestDAO.LoadAllCompletedStudentMediaRequestByStudent(studentId, startdate, endDate, campusId);
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00074460 File Offset: 0x00072660
		public IList<MediaContentRequestedInfo> LoadAllInProgressStudentMediaRequest(int campusId = 0)
		{
			return this.StudentMediaRequestDAO.LoadAllInProgressStudentMediaRequest(campusId);
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00074480 File Offset: 0x00072680
		public IList<MediaContentRequestedInfo> LoadAllInProgressStudentMediaRequestByStudent(int studentId, int campusId = 0)
		{
			return this.StudentMediaRequestDAO.LoadAllInProgressStudentMediaRequestByStudent(studentId, campusId);
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x000744A0 File Offset: 0x000726A0
		[DebuggerStepThrough]
		public Task<IList<MediaContentRequestedInfoExtended>> LoadAllStudentMediaRequestByStudentAsync(int studentId, DateTime startdate, DateTime enddate)
		{
			StudentMediaRequestManager.<LoadAllStudentMediaRequestByStudentAsync>d__24 <LoadAllStudentMediaRequestByStudentAsync>d__ = new StudentMediaRequestManager.<LoadAllStudentMediaRequestByStudentAsync>d__24();
			<LoadAllStudentMediaRequestByStudentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<MediaContentRequestedInfoExtended>>.Create();
			<LoadAllStudentMediaRequestByStudentAsync>d__.<>4__this = this;
			<LoadAllStudentMediaRequestByStudentAsync>d__.studentId = studentId;
			<LoadAllStudentMediaRequestByStudentAsync>d__.startdate = startdate;
			<LoadAllStudentMediaRequestByStudentAsync>d__.enddate = enddate;
			<LoadAllStudentMediaRequestByStudentAsync>d__.<>1__state = -1;
			<LoadAllStudentMediaRequestByStudentAsync>d__.<>t__builder.Start<StudentMediaRequestManager.<LoadAllStudentMediaRequestByStudentAsync>d__24>(ref <LoadAllStudentMediaRequestByStudentAsync>d__);
			return <LoadAllStudentMediaRequestByStudentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x000744FC File Offset: 0x000726FC
		public bool IsMediaContentAlreadyRequested(int studentId, MediaContentIdentifier identifier)
		{
			return this.StudentMediaRequestDAO.IsMediaContentAlreadyRequested(studentId, identifier);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0007451C File Offset: 0x0007271C
		public MediaContentRequestedInfo MarkMediaContentRequestedAsCompleted(int mediaContentRequestInfoId, MediaRequestStatus status, DateTime availableStartTime, DateTime availableEndTime, int mediaContentPerFormatId)
		{
			this.StudentMediaRequestDAO.MarkMediaContentRequestedAsCompleted(mediaContentRequestInfoId, status, availableStartTime, availableEndTime, mediaContentPerFormatId);
			MediaContentRequestedInfo mediaContentRequestedInfo = this.LoadMediaContentRequestedInfoById(mediaContentRequestInfoId);
			bool flag = mediaContentRequestedInfo != null;
			if (flag)
			{
				bool flag2 = mediaContentRequestedInfo.RequestStatus == MediaRequestStatus.Ready_To_Download;
				if (flag2)
				{
					mediaContentRequestedInfo.NotifyStudentsAsync(Setting.ALTERNATEFORMAT_Email_ReadyToDownloadFileStudentNotification, this.OpContext);
				}
				else
				{
					bool flag3 = mediaContentRequestedInfo.RequestStatus == MediaRequestStatus.Completed_but_Pending_of_Proof_of_Purchase;
					if (flag3)
					{
						mediaContentRequestedInfo.NotifyStudentsAsync(Setting.ALTERNATEFORMAT_Email_FilePendingOfProofOfPurchaseStudentNotification, this.OpContext);
					}
				}
			}
			return mediaContentRequestedInfo;
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00074596 File Offset: 0x00072796
		public void UpdateStudentContentMediaRequestInfo(MediaContentRequestedInfo requestedInfo)
		{
			this.StudentMediaRequestDAO.UpdateStudentContentMediaRequestInfo(requestedInfo);
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x000745A8 File Offset: 0x000727A8
		public int AddStudentContentMediaRequestInfo(MediaContentRequestedInfo requestedInfo)
		{
			return this.StudentMediaRequestDAO.AddStudentContentMediaRequestInfo(requestedInfo);
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x000745C8 File Offset: 0x000727C8
		public void DeleteStudentContentMediaRequestInfo(int requestedInfoId)
		{
			MediaContentRequestedInfo mediaContentRequestedInfo = this.LoadMediaContentRequestedInfoById(requestedInfoId);
			bool flag = mediaContentRequestedInfo != null;
			if (flag)
			{
				this.DeleteStudentContentMediaRequestInfo(mediaContentRequestedInfo);
			}
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x000745F0 File Offset: 0x000727F0
		public void DeleteStudentContentMediaRequestInfo(MediaContentRequestedInfo requestedInfo)
		{
			requestedInfo.IsCancelled = true;
			requestedInfo.CompletedDateTime = new DateTime?(DateTime.Now);
			this.StudentMediaRequestDAO.DeleteStudentContentMediaRequestInfo(requestedInfo, MediaRequestStatus.Rejected_by_Staff);
			Task.Run(delegate()
			{
				bool flag = requestedInfo.MediaJobId > 0 && !requestedInfo.IsCompleted;
				if (flag)
				{
					IList<MediaContentRequestedInfo> list = this.StudentMediaRequestDAO.LoadAllMediaRequestInfoByJobId(requestedInfo.MediaJobId);
					bool flag2 = list == null || list.Count == 0;
					if (flag2)
					{
						MediaJobDAO mediaJobDAO = new MediaJobDAO(this.OpContext);
						mediaJobDAO.CancelMediaJob(requestedInfo.MediaJobId, "Cancelled media content request info");
					}
				}
				StudentMediaRequest studentMediaRequest = this.StudentMediaRequestDAO.LoadStudentMediaRequestById(requestedInfo.StudentRequestId);
				bool flag3 = studentMediaRequest.ContentRequestedList.All((MediaContentRequestedInfo contentRequestedInfo) => contentRequestedInfo.IsCompleted || contentRequestedInfo.IsCancelled);
				bool flag4 = flag3;
				if (flag4)
				{
					studentMediaRequest.CompletedDateTime = new DateTime?(DateTime.Now);
					this.StudentMediaRequestDAO.UpdateStudentMediaRequest(studentMediaRequest);
				}
			});
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0007465A File Offset: 0x0007285A
		public void UpdateAvailableDownloadingTime(MediaContentRequestedInfo requestedInfo)
		{
			this.StudentMediaRequestDAO.UpdateAvailableDownloadingTime(requestedInfo);
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0007466C File Offset: 0x0007286C
		public ProofOfPurchaseInfo AcceptProofOfPurchaseReceipt(ProofOfPurchaseInfo proofOfPurchaseInfo)
		{
			bool flag = proofOfPurchaseInfo.WhoAcceptedProofOfPurchase != null;
			ProofOfPurchaseInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = this.OpContext.WhoAmI <= 0;
				if (flag2)
				{
					result = null;
				}
				else
				{
					IPeopleManager peopleManager = new PeopleManager(this.OpContext);
					PersonBase personBase = peopleManager.LoadPerson(this.OpContext.WhoAmI);
					bool flag3;
					if (personBase != null)
					{
						if (personBase.CoreGroup != eCoreGroup.Staff)
						{
							flag3 = personBase.Groups.All((TechnoPro.Common.Public.Entities.People.Group g) => g.GetCoreGroup() != eCoreGroup.Staff);
						}
						else
						{
							flag3 = false;
						}
					}
					else
					{
						flag3 = true;
					}
					bool flag4 = flag3;
					if (flag4)
					{
						result = null;
					}
					else
					{
						proofOfPurchaseInfo.WhoAcceptedProofOfPurchase = personBase;
						proofOfPurchaseInfo.WhenWasAccepted = new DateTime?(DateTime.Now);
						this.StudentMediaRequestDAO.UpdateProofOfPurchase(proofOfPurchaseInfo);
						Task.Run(() => this.OnProofOfPurchaseUpdatedAsync(proofOfPurchaseInfo));
						result = proofOfPurchaseInfo;
					}
				}
			}
			return result;
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00074780 File Offset: 0x00072980
		public bool RejectProofOfPurchaseReceipt(ProofOfPurchaseInfo proofOfPurchaseInfo)
		{
			StudentMediaRequestManager.<>c__DisplayClass33_0 CS$<>8__locals1 = new StudentMediaRequestManager.<>c__DisplayClass33_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.proofOfPurchaseInfo = proofOfPurchaseInfo;
			bool flag = CS$<>8__locals1.proofOfPurchaseInfo.WhoAcceptedProofOfPurchase != null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.StudentMediaRequestDAO.DeleteProofOfPurchase(CS$<>8__locals1.proofOfPurchaseInfo.ProofOfPurchaseId);
				Task.Run(delegate()
				{
					StudentMediaRequestManager.<>c__DisplayClass33_0.<<RejectProofOfPurchaseReceipt>b__0>d <<RejectProofOfPurchaseReceipt>b__0>d = new StudentMediaRequestManager.<>c__DisplayClass33_0.<<RejectProofOfPurchaseReceipt>b__0>d();
					<<RejectProofOfPurchaseReceipt>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<RejectProofOfPurchaseReceipt>b__0>d.<>4__this = CS$<>8__locals1;
					<<RejectProofOfPurchaseReceipt>b__0>d.<>1__state = -1;
					<<RejectProofOfPurchaseReceipt>b__0>d.<>t__builder.Start<StudentMediaRequestManager.<>c__DisplayClass33_0.<<RejectProofOfPurchaseReceipt>b__0>d>(ref <<RejectProofOfPurchaseReceipt>b__0>d);
					return <<RejectProofOfPurchaseReceipt>b__0>d.<>t__builder.Task;
				}).ContinueWith(delegate(Task task)
				{
					CS$<>8__locals1.<>4__this.NotifyStudentsAsync(new MailMergeContext
					{
						PersonId = CS$<>8__locals1.proofOfPurchaseInfo.StudentPersonId,
						AlternateFormatMediaContentId = CS$<>8__locals1.proofOfPurchaseInfo.MediaContentUniqueId
					}, Setting.ALTERNATEFORMAT_Email_ProofOfPurchaseReceiptRejectedNotification, null);
				});
				result = true;
			}
			return result;
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x000747F8 File Offset: 0x000729F8
		public bool IsProofOfPurchaseAvailable(Guid mediaContentUniqueId, int studentPersonId)
		{
			return this.StudentMediaRequestDAO.IsProofOfPurchaseAvailable(mediaContentUniqueId, studentPersonId);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00074818 File Offset: 0x00072A18
		public ProofOfPurchaseInfo DownloadProofOfPurchase(Guid mediaContentUniqueId, int studentPersonId)
		{
			return this.StudentMediaRequestDAO.DownloadProofOfPurchase(mediaContentUniqueId, studentPersonId);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00074838 File Offset: 0x00072A38
		[DebuggerStepThrough]
		public Task<ProofOfPurchaseInfo> DownloadProofOfPurchaseAsync(Guid mediaContentUniqueId, int studentPersonId)
		{
			StudentMediaRequestManager.<DownloadProofOfPurchaseAsync>d__36 <DownloadProofOfPurchaseAsync>d__ = new StudentMediaRequestManager.<DownloadProofOfPurchaseAsync>d__36();
			<DownloadProofOfPurchaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ProofOfPurchaseInfo>.Create();
			<DownloadProofOfPurchaseAsync>d__.<>4__this = this;
			<DownloadProofOfPurchaseAsync>d__.mediaContentUniqueId = mediaContentUniqueId;
			<DownloadProofOfPurchaseAsync>d__.studentPersonId = studentPersonId;
			<DownloadProofOfPurchaseAsync>d__.<>1__state = -1;
			<DownloadProofOfPurchaseAsync>d__.<>t__builder.Start<StudentMediaRequestManager.<DownloadProofOfPurchaseAsync>d__36>(ref <DownloadProofOfPurchaseAsync>d__);
			return <DownloadProofOfPurchaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0007488C File Offset: 0x00072A8C
		public IList<MediaContentRequestedInfo> LoadAllMediaRequestInfoByJobId(int jobId)
		{
			return this.StudentMediaRequestDAO.LoadAllMediaRequestInfoByJobId(jobId);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x000748AC File Offset: 0x00072AAC
		public ProofOfPurchaseInfo DownloadProofOfPurchase(int proofOfPurchaseId)
		{
			return this.StudentMediaRequestDAO.DownloadProofOfPurchase(proofOfPurchaseId);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x000748CC File Offset: 0x00072ACC
		[DebuggerStepThrough]
		public Task<ProofOfPurchaseInfo> DownloadProofOfPurchaseAsync(int proofOfPurchaseId)
		{
			StudentMediaRequestManager.<DownloadProofOfPurchaseAsync>d__39 <DownloadProofOfPurchaseAsync>d__ = new StudentMediaRequestManager.<DownloadProofOfPurchaseAsync>d__39();
			<DownloadProofOfPurchaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ProofOfPurchaseInfo>.Create();
			<DownloadProofOfPurchaseAsync>d__.<>4__this = this;
			<DownloadProofOfPurchaseAsync>d__.proofOfPurchaseId = proofOfPurchaseId;
			<DownloadProofOfPurchaseAsync>d__.<>1__state = -1;
			<DownloadProofOfPurchaseAsync>d__.<>t__builder.Start<StudentMediaRequestManager.<DownloadProofOfPurchaseAsync>d__39>(ref <DownloadProofOfPurchaseAsync>d__);
			return <DownloadProofOfPurchaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00074918 File Offset: 0x00072B18
		public int UploadProofOfPurchase(ProofOfPurchaseInfo proofOfPurchaseInfo)
		{
			PersonBase personBase = null;
			bool flag = this.OpContext.WhoAmI > 0;
			if (flag)
			{
				IPeopleManager peopleManager = new PeopleManager(this.OpContext);
				personBase = peopleManager.LoadPerson(this.OpContext.WhoAmI);
				bool flag2;
				if (personBase != null)
				{
					if (personBase.CoreGroup != eCoreGroup.Staff)
					{
						flag2 = personBase.Groups.All((TechnoPro.Common.Public.Entities.People.Group g) => g.GetCoreGroup() != eCoreGroup.Staff);
					}
					else
					{
						flag2 = false;
					}
				}
				else
				{
					flag2 = false;
				}
				bool flag3 = flag2;
				if (flag3)
				{
					personBase = null;
				}
			}
			proofOfPurchaseInfo.WhoAcceptedProofOfPurchase = personBase;
			proofOfPurchaseInfo.WhenWasAccepted = ((personBase != null) ? new DateTime?(DateTime.Now) : null);
			proofOfPurchaseInfo.ProofOfPurchaseId = this.StudentMediaRequestDAO.UploadProofOfPurchase(proofOfPurchaseInfo);
			bool flag4 = proofOfPurchaseInfo.ProofOfPurchaseId > 0;
			if (flag4)
			{
				Task.Run(() => this.OnProofOfPurchaseUpdatedAsync(proofOfPurchaseInfo));
			}
			return proofOfPurchaseInfo.ProofOfPurchaseId;
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00074A38 File Offset: 0x00072C38
		[DebuggerStepThrough]
		public Task<int> UploadProofOfPurchaseAsync(ProofOfPurchaseInfo proofOfPurchaseInfo)
		{
			StudentMediaRequestManager.<UploadProofOfPurchaseAsync>d__41 <UploadProofOfPurchaseAsync>d__ = new StudentMediaRequestManager.<UploadProofOfPurchaseAsync>d__41();
			<UploadProofOfPurchaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<UploadProofOfPurchaseAsync>d__.<>4__this = this;
			<UploadProofOfPurchaseAsync>d__.proofOfPurchaseInfo = proofOfPurchaseInfo;
			<UploadProofOfPurchaseAsync>d__.<>1__state = -1;
			<UploadProofOfPurchaseAsync>d__.<>t__builder.Start<StudentMediaRequestManager.<UploadProofOfPurchaseAsync>d__41>(ref <UploadProofOfPurchaseAsync>d__);
			return <UploadProofOfPurchaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00074A83 File Offset: 0x00072C83
		public void DeleteProofOfPurchase(int proofOfPurchaseId)
		{
			this.StudentMediaRequestDAO.DeleteProofOfPurchase(proofOfPurchaseId);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00074A94 File Offset: 0x00072C94
		[DebuggerStepThrough]
		public Task DeleteProofOfPurchaseAsync(int proofOfPurchaseId)
		{
			StudentMediaRequestManager.<DeleteProofOfPurchaseAsync>d__43 <DeleteProofOfPurchaseAsync>d__ = new StudentMediaRequestManager.<DeleteProofOfPurchaseAsync>d__43();
			<DeleteProofOfPurchaseAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DeleteProofOfPurchaseAsync>d__.<>4__this = this;
			<DeleteProofOfPurchaseAsync>d__.proofOfPurchaseId = proofOfPurchaseId;
			<DeleteProofOfPurchaseAsync>d__.<>1__state = -1;
			<DeleteProofOfPurchaseAsync>d__.<>t__builder.Start<StudentMediaRequestManager.<DeleteProofOfPurchaseAsync>d__43>(ref <DeleteProofOfPurchaseAsync>d__);
			return <DeleteProofOfPurchaseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00074AE0 File Offset: 0x00072CE0
		public MediaContentFormat[] GetAllowedMediaContentFormatsForStudentToRequest(int pid, MediaContentIdentifier mediaContentIdentifier, int selectedLuCourseId = 0)
		{
			WebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			bool settingValue = webSettingManager.GetSettingValue<bool>(Setting.ALTERNATEFORMAT_AllowStudentsToSelectPreferredFormatTypeWhenSubmittingAltFormatRequest);
			bool flag = !settingValue;
			MediaContentFormat[] result;
			if (flag)
			{
				result = new MediaContentFormat[0];
			}
			else
			{
				string settingValue2 = webSettingManager.GetSettingValue<string>(Setting.ALTERNATEFORMAT_Accommodation_to_FormatTypes_Mappings);
				AccommodationAltFormatTypesMapping[] source = (settingValue2 ?? "").Trim().DeSerializeAccommodationALtFormatTypesMappings() ?? new AccommodationAltFormatTypesMapping[0];
				IAccommodationsManager accommodationsManager = new AccommodationsManager(this.OpContext);
				IList<AccommodationData> accommodations = accommodationsManager.LoadAccommodationsByStudentAndCourseOrTemplate(pid, selectedLuCourseId);
				result = (from g in source
				where accommodations.Any((AccommodationData h) => g.AccommodationControlId == h.Data.Field.ControlId)
				select g).SelectMany((AccommodationAltFormatTypesMapping m) => m.AltFormatTypes).Distinct<MediaContentFormat>().ToArray<MediaContentFormat>();
			}
			return result;
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00074BB8 File Offset: 0x00072DB8
		[DebuggerStepThrough]
		private Task NotifyStudentsAsync(MailMergeContext mailContext, Setting emailSetting, Dictionary<string, string> customDictionary = null)
		{
			StudentMediaRequestManager.<NotifyStudentsAsync>d__45 <NotifyStudentsAsync>d__ = new StudentMediaRequestManager.<NotifyStudentsAsync>d__45();
			<NotifyStudentsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyStudentsAsync>d__.<>4__this = this;
			<NotifyStudentsAsync>d__.mailContext = mailContext;
			<NotifyStudentsAsync>d__.emailSetting = emailSetting;
			<NotifyStudentsAsync>d__.customDictionary = customDictionary;
			<NotifyStudentsAsync>d__.<>1__state = -1;
			<NotifyStudentsAsync>d__.<>t__builder.Start<StudentMediaRequestManager.<NotifyStudentsAsync>d__45>(ref <NotifyStudentsAsync>d__);
			return <NotifyStudentsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00074C14 File Offset: 0x00072E14
		[DebuggerStepThrough]
		private Task OnProofOfPurchaseDeletedAsync(ProofOfPurchaseInfo proofOfPurchaseInfo)
		{
			StudentMediaRequestManager.<OnProofOfPurchaseDeletedAsync>d__46 <OnProofOfPurchaseDeletedAsync>d__ = new StudentMediaRequestManager.<OnProofOfPurchaseDeletedAsync>d__46();
			<OnProofOfPurchaseDeletedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<OnProofOfPurchaseDeletedAsync>d__.<>4__this = this;
			<OnProofOfPurchaseDeletedAsync>d__.proofOfPurchaseInfo = proofOfPurchaseInfo;
			<OnProofOfPurchaseDeletedAsync>d__.<>1__state = -1;
			<OnProofOfPurchaseDeletedAsync>d__.<>t__builder.Start<StudentMediaRequestManager.<OnProofOfPurchaseDeletedAsync>d__46>(ref <OnProofOfPurchaseDeletedAsync>d__);
			return <OnProofOfPurchaseDeletedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00074C60 File Offset: 0x00072E60
		[DebuggerStepThrough]
		private Task OnProofOfPurchaseUpdatedAsync(ProofOfPurchaseInfo proofOfPurchaseInfo)
		{
			StudentMediaRequestManager.<OnProofOfPurchaseUpdatedAsync>d__47 <OnProofOfPurchaseUpdatedAsync>d__ = new StudentMediaRequestManager.<OnProofOfPurchaseUpdatedAsync>d__47();
			<OnProofOfPurchaseUpdatedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<OnProofOfPurchaseUpdatedAsync>d__.<>4__this = this;
			<OnProofOfPurchaseUpdatedAsync>d__.proofOfPurchaseInfo = proofOfPurchaseInfo;
			<OnProofOfPurchaseUpdatedAsync>d__.<>1__state = -1;
			<OnProofOfPurchaseUpdatedAsync>d__.<>t__builder.Start<StudentMediaRequestManager.<OnProofOfPurchaseUpdatedAsync>d__47>(ref <OnProofOfPurchaseUpdatedAsync>d__);
			return <OnProofOfPurchaseUpdatedAsync>d__.<>t__builder.Task;
		}
	}
}
