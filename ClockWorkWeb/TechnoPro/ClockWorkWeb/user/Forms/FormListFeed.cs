using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.Common.ClientManager.Core.OnlineForms;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.OnlineForms;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.OnlineForms;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.Forms
{
	// Token: 0x020000E8 RID: 232
	public class FormListFeed : HttpTaskAsyncHandler, IRequiresSessionState
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x000351F4 File Offset: 0x000333F4
		public override async Task ProcessRequestAsync(HttpContext context)
		{
			int pid = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid();
			IWebSettingsClientManager wsm = new WebSettingsClientManager();
			bool isOnlineFormsModuleEnabled = pid > 0 && wsm.GetSettingValue<bool>(Setting.MODULES_ENABLED_OnlineForms);
			string loadingType = context.Request.QueryString["loadType"];
			bool flag = loadingType == "1";
			if (flag)
			{
				await this.LoadFormsAsync(context, isOnlineFormsModuleEnabled);
			}
			else
			{
				await this.LoadSubmissionsAsync(context, pid, isOnlineFormsModuleEnabled);
			}
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00035244 File Offset: 0x00033444
		private async Task LoadFormsAsync(HttpContext context, bool isOnlineFormsModuleEnabled)
		{
			IOnlineFormClientManager sm = new OnlineFormClientManager();
			IList<OnlineFormDTO> list2;
			if (isOnlineFormsModuleEnabled)
			{
				IList<OnlineFormDTO> list = await sm.GetActiveOnlineFormsAsync();
				list2 = list;
				list = null;
			}
			else
			{
				list2 = new List<OnlineFormDTO>();
			}
			IList<OnlineFormDTO> activeOnlineFormsItems0 = list2;
			list2 = null;
			IList<OnlineFormDTO> list3 = activeOnlineFormsItems0;
			List<FormListFeed.ActiveOnlineForm> list4;
			if (list3 == null)
			{
				list4 = null;
			}
			else
			{
				list4 = (from g in list3
				select new FormListFeed.ActiveOnlineForm
				{
					Title = g.Title,
					Description = g.Description,
					IdStr = g.OnlineFormId.ToString()
				}).ToList<FormListFeed.ActiveOnlineForm>();
			}
			List<FormListFeed.ActiveOnlineForm> activeOnlineFormsItems = list4 ?? new List<FormListFeed.ActiveOnlineForm>();
			FormListFeed.ActiveOnlineForms activeOnlineForms = new FormListFeed.ActiveOnlineForms
			{
				Title = "Forms",
				Forms = activeOnlineFormsItems
			};
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			string json = serializer.Serialize(activeOnlineForms);
			context.Response.ContentType = "text/json";
			context.Response.Write(json);
			context.Response.End();
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0003529C File Offset: 0x0003349C
		private async Task LoadSubmissionsAsync(HttpContext context, int pid, bool isOnlineFormsModuleEnabled)
		{
			IOnlineFormQueueClientManager sm = new OnlineFormQueueClientManager();
			IList<OnlineFormQueueItemDTO> list2;
			if (isOnlineFormsModuleEnabled)
			{
				IList<OnlineFormQueueItemDTO> list = await sm.LoadAllStudentOnlineFormsAsync(pid);
				list2 = list;
				list = null;
			}
			else
			{
				list2 = new List<OnlineFormQueueItemDTO>();
			}
			IList<OnlineFormQueueItemDTO> submissions = list2;
			list2 = null;
			bool showFormSubmissionStatus;
			if (submissions != null && submissions.Count > 0)
			{
				IWebSettingsClientManager wsm = new WebSettingsClientManager();
				showFormSubmissionStatus = wsm.GetSettingValue<bool>(Setting.ONLINEFORMS_ShowFormSubmissionStatus);
				wsm = null;
			}
			else
			{
				showFormSubmissionStatus = false;
			}
			List<FormListFeed.StudentFormSubmissions> items = new List<FormListFeed.StudentFormSubmissions>();
			foreach (OnlineFormQueueItemDTO sub in submissions)
			{
				FormListFeed.<>c__DisplayClass2_0 CS$<>8__locals1 = new FormListFeed.<>c__DisplayClass2_0();
				FormListFeed.<>c__DisplayClass2_0 CS$<>8__locals2 = CS$<>8__locals1;
				OnlineFormForDisplayDTO onlineForm = sub.OnlineForm;
				CS$<>8__locals2.id = ((onlineForm != null) ? onlineForm.OnlineFormId.ToString() : null);
				if (CS$<>8__locals1.id.Length >= 1)
				{
					FormListFeed.StudentFormSubmissions item = items.FirstOrDefault((FormListFeed.StudentFormSubmissions g) => g.FormIdStr == CS$<>8__locals1.id);
					if (item == null)
					{
						item = new FormListFeed.StudentFormSubmissions
						{
							FormIdStr = CS$<>8__locals1.id,
							Submissions = new List<FormListFeed.StudentFormSubmission>()
						};
						items.Add(item);
					}
					eOnlineFormStatusType statusType;
					string status;
					if (showFormSubmissionStatus)
					{
						OnlineFormStatusDTO status2 = sub.Status;
						statusType = ((status2 != null) ? status2.StatusType : eOnlineFormStatusType.New);
						OnlineFormStatusDTO status3 = sub.Status;
						status = (((status3 != null) ? status3.Title : null) ?? "");
					}
					else
					{
						statusType = eOnlineFormStatusType.New;
						status = "Submitted";
					}
					string badgeClass;
					switch (statusType)
					{
					case eOnlineFormStatusType.PendingWorkingOnIt:
						badgeClass = "info";
						break;
					case eOnlineFormStatusType.PendingButWaiting:
						badgeClass = "warning";
						break;
					case eOnlineFormStatusType.PendingWithProblem:
						badgeClass = "info";
						break;
					case eOnlineFormStatusType.Hold:
						badgeClass = "info";
						break;
					case eOnlineFormStatusType.ClosedComplete:
						badgeClass = "success";
						break;
					case eOnlineFormStatusType.ClosedIncomplete:
						badgeClass = "danger";
						break;
					default:
						badgeClass = "info";
						break;
					}
					if (status.Length < 1)
					{
						status = "Submitted";
					}
					item.Submissions.Add(new FormListFeed.StudentFormSubmission
					{
						DateSubmitted = sub.DateEntered,
						Status = status,
						BadgeClass = badgeClass,
						SubmissionId = sub.PeopleOnlineFormId.ToString()
					});
					CS$<>8__locals1 = null;
					item = null;
					status = null;
					badgeClass = null;
					sub = null;
				}
			}
			IEnumerator<OnlineFormQueueItemDTO> enumerator = null;
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			string json = serializer.Serialize(items);
			context.Response.ContentType = "text/json";
			context.Response.Write(json);
			context.Response.End();
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x000352F8 File Offset: 0x000334F8
		public new bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x02000213 RID: 531
		public class StudentFormSubmissions
		{
			// Token: 0x1700031A RID: 794
			// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0004FBB3 File Offset: 0x0004DDB3
			// (set) Token: 0x06000E04 RID: 3588 RVA: 0x0004FBBB File Offset: 0x0004DDBB
			public string FormIdStr { get; set; }

			// Token: 0x1700031B RID: 795
			// (get) Token: 0x06000E05 RID: 3589 RVA: 0x0004FBC4 File Offset: 0x0004DDC4
			// (set) Token: 0x06000E06 RID: 3590 RVA: 0x0004FBCC File Offset: 0x0004DDCC
			public IList<FormListFeed.StudentFormSubmission> Submissions { get; set; }
		}

		// Token: 0x02000214 RID: 532
		public class StudentFormSubmission
		{
			// Token: 0x1700031C RID: 796
			// (get) Token: 0x06000E08 RID: 3592 RVA: 0x0004FBD5 File Offset: 0x0004DDD5
			// (set) Token: 0x06000E09 RID: 3593 RVA: 0x0004FBDD File Offset: 0x0004DDDD
			public string SubmissionId { get; set; }

			// Token: 0x1700031D RID: 797
			// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0004FBE6 File Offset: 0x0004DDE6
			// (set) Token: 0x06000E0B RID: 3595 RVA: 0x0004FBEE File Offset: 0x0004DDEE
			public DateTime DateSubmitted { get; set; }

			// Token: 0x1700031E RID: 798
			// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0004FBF7 File Offset: 0x0004DDF7
			// (set) Token: 0x06000E0D RID: 3597 RVA: 0x0004FBFF File Offset: 0x0004DDFF
			public string Status { get; set; }

			// Token: 0x1700031F RID: 799
			// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0004FC08 File Offset: 0x0004DE08
			// (set) Token: 0x06000E0F RID: 3599 RVA: 0x0004FC10 File Offset: 0x0004DE10
			public string BadgeClass { get; set; }
		}

		// Token: 0x02000215 RID: 533
		public class ActiveOnlineForms
		{
			// Token: 0x17000320 RID: 800
			// (get) Token: 0x06000E11 RID: 3601 RVA: 0x0004FC19 File Offset: 0x0004DE19
			// (set) Token: 0x06000E12 RID: 3602 RVA: 0x0004FC21 File Offset: 0x0004DE21
			public string Title { get; set; }

			// Token: 0x17000321 RID: 801
			// (get) Token: 0x06000E13 RID: 3603 RVA: 0x0004FC2A File Offset: 0x0004DE2A
			// (set) Token: 0x06000E14 RID: 3604 RVA: 0x0004FC32 File Offset: 0x0004DE32
			public IList<FormListFeed.ActiveOnlineForm> Forms { get; set; }
		}

		// Token: 0x02000216 RID: 534
		public class ActiveOnlineForm
		{
			// Token: 0x17000322 RID: 802
			// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0004FC3B File Offset: 0x0004DE3B
			// (set) Token: 0x06000E17 RID: 3607 RVA: 0x0004FC43 File Offset: 0x0004DE43
			public string Title { get; set; }

			// Token: 0x17000323 RID: 803
			// (get) Token: 0x06000E18 RID: 3608 RVA: 0x0004FC4C File Offset: 0x0004DE4C
			// (set) Token: 0x06000E19 RID: 3609 RVA: 0x0004FC54 File Offset: 0x0004DE54
			public string Description { get; set; }

			// Token: 0x17000324 RID: 804
			// (get) Token: 0x06000E1A RID: 3610 RVA: 0x0004FC5D File Offset: 0x0004DE5D
			// (set) Token: 0x06000E1B RID: 3611 RVA: 0x0004FC65 File Offset: 0x0004DE65
			public string IdStr { get; set; }
		}
	}
}
