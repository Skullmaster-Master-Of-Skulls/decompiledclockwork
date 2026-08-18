using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.Core.Veteran;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.ClientManager.ICore.Veteran;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.LookupCourses;
using TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses;
using TechnoPro.Common.UI.Web.ClockWork.Controls;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;
using TechnoPro.Common.UI.Web.Mappers.LookupCourses;

namespace TechnoPro.Common.UI.Web.Veterans.Controls
{
	// Token: 0x02000002 RID: 2
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlTaskCheckList runat=server></{0}:CtrlTaskCheckList>")]
	public class CtrlTaskCheckList : WebControl, INamingContainer
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public bool DebugModeEnabled { get; set; }

		// Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.RenderBeginTag("div");
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002070 File Offset: 0x00000270
		protected override void CreateChildControls()
		{
			this.btnChapterSubmit.Click += this.btnChapterSubmit_Click;
			this.cmbChapter.SelectedIndexChanged += this.cmbChapter_SelectedIndexChanged;
			this.ctrlTerm.OnUserInfoRequested += this.ctrlTerm_OnUserInfoRequested;
			this.ctrlTerm.SelectedIndexChanged += this.ctrlTerm_SelectedIndexChanged;
			this.chkStep1.OnSubControlsRequired += this.chkStep1_OnSubControlsRequired;
			this.chkStep3.OnSubControlsRequired += this.chkStep3_OnSubControlsRequired;
			this.BuildControlHeiarchy();
			base.CreateChildControls();
			List<VetTaskStep> steps = new List<VetTaskStep>();
			int stepNum = 1;
			VetTaskStep.AddVetTaskStep(ref steps, stepNum++, eVetTaskStepType.ChooseTermDates);
			VetTaskStep.AddVetTaskStep(ref steps, stepNum++, eVetTaskStepType.Register);
			VetTaskStep.AddVetTaskStep(ref steps, stepNum++, eVetTaskStepType.SelectChapter);
			VetTaskStep.AddVetTaskStep(ref steps, stepNum++, eVetTaskStepType.CompleteBenefitRequestForm);
			VetTaskStep.AddVetTaskStep(ref steps, stepNum++, eVetTaskStepType.ConsentToAgreementForm);
			VetTaskStep.AddVetTaskStep(ref steps, stepNum++, eVetTaskStepType.BenefitCounselorReview);
			VetTaskStep.AddVetTaskStep(ref steps, stepNum, eVetTaskStepType.AdministratorReview);
			this.InitializeSteps(new CtrlToDoItemChecked[]
			{
				this.chkStep1,
				this.chkStep2,
				this.chkStep3,
				this.chkStep4,
				this.chkStep5,
				this.chkStep6,
				this.chkStep7
			}, steps);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021C5 File Offset: 0x000003C5
		private void chkStep3_OnSubControlsRequired(object sender, SubControlsRequiredArgs e)
		{
			e.Controls = new List<Control>
			{
				this.lblChapter,
				this.cmbChapter,
				this.btnChapterSubmit
			};
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021F6 File Offset: 0x000003F6
		private void chkStep1_OnSubControlsRequired(object sender, SubControlsRequiredArgs e)
		{
			e.Controls = new List<Control>
			{
				this.ctrlTerm
			};
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000220F File Offset: 0x0000040F
		protected void ctrlTerm_SelectedIndexChanged(object sender, EventArgs e)
		{
			HttpContext.Current.Response.Redirect(".", true);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002228 File Offset: 0x00000428
		private void RenderContentsForStep(int step, HtmlTextWriter writer)
		{
			switch (step)
			{
			case 1:
				this.chkStep1.RenderControl(writer);
				return;
			case 2:
				this.chkStep2.RenderControl(writer);
				return;
			case 3:
				this.chkStep3.RenderControl(writer);
				return;
			case 4:
				this.chkStep4.RenderControl(writer);
				return;
			case 5:
				this.chkStep5.RenderControl(writer);
				return;
			case 6:
				this.chkStep6.RenderControl(writer);
				return;
			case 7:
				this.chkStep7.RenderControl(writer);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022B4 File Offset: 0x000004B4
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (this.DebugModeEnabled)
			{
				PerDateEntryDTO perDateEntry = this.PerDateEntry;
				SessionDTO session = this.Session;
				writer.Write(string.Format("Session={0}; perdateformappid={1}", session.StartDate.ToString("yyyy-MM-dd") + " to " + session.EndDate.ToString("yyyy-MM-dd"), (perDateEntry == null) ? "NULL" : perDateEntry.AppointmentId.ToString()));
			}
			writer.Write("<ul class='InvisibleUl'");
			writer.Write(this.chkStep1.IsCurrent ? "<li class='TaskCheckItemBoxCurrent'>" : "<li>");
			this.RenderContentsForStep(1, writer);
			writer.Write("</li>");
			writer.Write(this.chkStep2.IsCurrent ? "<li class='TaskCheckItemBoxCurrent'>" : "<li>");
			this.RenderContentsForStep(2, writer);
			writer.Write("</li>");
			writer.Write(this.chkStep3.IsCurrent ? "<li class='TaskCheckItemBoxCurrent'>" : "<li>");
			this.RenderContentsForStep(3, writer);
			writer.Write("</li>");
			writer.Write(this.chkStep4.IsCurrent ? "<li class='TaskCheckItemBoxCurrent'>" : "<li>");
			this.RenderContentsForStep(4, writer);
			writer.Write("</li>");
			writer.Write(this.chkStep5.IsCurrent ? "<li class='TaskCheckItemBoxCurrent'>" : "<li>");
			this.RenderContentsForStep(5, writer);
			writer.Write("</li>");
			writer.Write(this.chkStep6.IsCurrent ? "<li class='TaskCheckItemBoxCurrent'>" : "<li>");
			this.RenderContentsForStep(6, writer);
			writer.Write("</li>");
			writer.Write(this.chkStep7.IsCurrent ? "<li class='TaskCheckItemBoxCurrent'>" : "<li>");
			this.RenderContentsForStep(7, writer);
			writer.Write("</li>");
			writer.Write("</ul>");
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000024A3 File Offset: 0x000006A3
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			base.OnInit(e);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000B RID: 11 RVA: 0x000024B4 File Offset: 0x000006B4
		// (remove) Token: 0x0600000C RID: 12 RVA: 0x000024EC File Offset: 0x000006EC
		public event StudentPidRequestEventHandler OnStudentPidRequested;

		// Token: 0x0600000D RID: 13 RVA: 0x00002524 File Offset: 0x00000724
		private int FireOnStudentPidRequested()
		{
			if (this.OnStudentPidRequested != null)
			{
				StudentPidRequestEventArgs studentPidRequestEventArgs = new StudentPidRequestEventArgs();
				this.OnStudentPidRequested(this, studentPidRequestEventArgs);
				return studentPidRequestEventArgs.Pid;
			}
			return 0;
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600000E RID: 14 RVA: 0x00002554 File Offset: 0x00000754
		// (remove) Token: 0x0600000F RID: 15 RVA: 0x0000258C File Offset: 0x0000078C
		public event SessionRequestedEventHandler OnSessionRequested;

		// Token: 0x06000010 RID: 16 RVA: 0x000025C4 File Offset: 0x000007C4
		private SessionDTO FireOnSessionRequested()
		{
			if (this.OnSessionRequested != null)
			{
				SessionRequestEventArgs sessionRequestEventArgs = new SessionRequestEventArgs();
				this.OnSessionRequested(this, sessionRequestEventArgs);
				if (sessionRequestEventArgs.Session != null)
				{
					return sessionRequestEventArgs.Session;
				}
			}
			return this.sessionClientManager.GetCurrentSession().ToDTO();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000260C File Offset: 0x0000080C
		private int pid
		{
			get
			{
				if (this._pid < 1)
				{
					this._pid = this.FireOnStudentPidRequested();
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					if (this._pid > 0 && this.veteranClientManager.HasUserCompletedAgreementForm(this._pid, this.PerDateEntry, webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_AgreementFormNum)))
					{
						this.allItemsAreDisabled = true;
					}
				}
				return this._pid;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000266E File Offset: 0x0000086E
		public bool AllItemsAreDisabled
		{
			get
			{
				return this.allItemsAreDisabled;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002676 File Offset: 0x00000876
		private IDynamicFieldClientManager dynamicFieldClientManager
		{
			get
			{
				if (this._dynamicFieldClientManager == null)
				{
					this._dynamicFieldClientManager = new DynamicFieldClientManager();
				}
				return this._dynamicFieldClientManager;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002694 File Offset: 0x00000894
		private List<DynamicListItemDTO> chapterListItems
		{
			get
			{
				if (this._chapterListItems == null)
				{
					this._chapterListItems = new List<DynamicListItemDTO>();
					object obj = HttpContext.Current.Session["chapterListItems"];
					if (obj == null)
					{
						IList<DynamicFieldDTO> list = this.dynamicFieldClientManager.LoadFieldsByControlIds(new List<int>
						{
							this.ChapterCid
						});
						if (list != null && list.Count > 0)
						{
							IList<DynamicListItemDTO> list2 = this.dynamicFieldClientManager.LoadListItems(list[0].Setting1);
							if (list2 != null)
							{
								this._chapterListItems = ((list2 != null) ? list2.ToList<DynamicListItemDTO>() : null);
							}
						}
						HttpContext.Current.Session.Add("chapterListItems", this._chapterListItems);
					}
					else
					{
						this._chapterListItems = (List<DynamicListItemDTO>)obj;
					}
				}
				return this._chapterListItems;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002753 File Offset: 0x00000953
		private IDynamicDataClientManager dynamicDataClientManager
		{
			get
			{
				if (this._dynamicDataClientManager == null)
				{
					this._dynamicDataClientManager = new DynamicDataClientManager();
				}
				return this._dynamicDataClientManager;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000276E File Offset: 0x0000096E
		public SessionView GetSelectedSession()
		{
			return this.ctrlTerm.SelectedSession;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000277C File Offset: 0x0000097C
		private void InitializeControls()
		{
			this.ctrlTerm.AvailableSessionMode = TermChooserAvailableSessionMode.CurrentTermAndNextTerm;
			this.chkStep1.ID = "chkStep1";
			this.chkStep2.ID = "chkStep2";
			this.chkStep3.ID = "chkStep3";
			this.chkStep4.ID = "chkStep4";
			this.chkStep5.ID = "chkStep5";
			this.chkStep6.ID = "chkStep6";
			this.chkStep7.ID = "chkStep7";
			this.cmbChapter.ID = "cmbChapter";
			this.lblChapter.ID = "lblChapter";
			this.lblChapter.AssociatedControlID = this.cmbChapter.ID;
			this.lblChapter.Text = "Select your Chapter: ";
			this.ctrlTerm.ID = "ctrlTerm";
			this.ctrlTerm.Caption = "Select a term: ";
			this.ctrlTerm.Title = "";
			this.ctrlTerm.RefreshButtonText = "Submit term change";
			this.ctrlTerm.DropListHorizontalAlign = HorizontalAlign.Left;
			this.ctrlTerm.DropListAutoPostBack = new bool?(true);
			this.ctrlTerm.StoreCurrentSelectedSessionInWebSessionKey = "VetSelectedSession";
			if (this.cmbChapter.Items.Count < 1)
			{
				this.cmbChapter.Items.Add(new ListItem("", ""));
				foreach (DynamicListItemDTO dynamicListItemDTO in this.chapterListItems)
				{
					this.cmbChapter.Items.Add(new ListItem(dynamicListItemDTO.LookupText, dynamicListItemDTO.LookupListId.ToString()));
				}
			}
			this.btnChapterSubmit.Text = "Submit";
			this.cmbChapter.AutoPostBack = true;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002970 File Offset: 0x00000B70
		private void ctrlTerm_OnUserInfoRequested(object sender, UserInfoForCourseArgs d)
		{
			d.Info = new UserInfoForCourses
			{
				PersonId = this.pid
			};
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002989 File Offset: 0x00000B89
		protected void cmbChapter_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.btnChapterSubmit_Click(this.btnChapterSubmit, new EventArgs());
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000299C File Offset: 0x00000B9C
		protected void btnChapterSubmit_Click(object sender, EventArgs e)
		{
			int selectedChapterLookupListId = this.GetSelectedChapterLookupListId();
			if (selectedChapterLookupListId > 0)
			{
				int chapterCid = this.ChapterCid;
				int pid = this.pid;
				if (pid > 0)
				{
					DynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
					PerDateEntryDTO perDateEntry = this.PerDateEntry;
					if (perDateEntry == null)
					{
						SessionDTO session = this.Session;
						this.dynamicDataClientManager.CreatePerDateEntry(new PerDateEntryDTO
						{
							ScreenNum = this.PackageScreenNum,
							DateEntered = session.StartDate.Date.AddDays(1.0),
							Student = new PersonBaseDTO
							{
								PersonId = this.pid
							},
							WhoEntered = new PersonBaseDTO
							{
								PersonId = this.pid
							}
						});
						this._perDateEntrySession = null;
						perDateEntry = this.PerDateEntry;
					}
					DynamicDataContextDTO context = new DynamicDataContextDTO
					{
						PrimaryId = pid,
						SecondaryId = perDateEntry.AppointmentId
					};
					List<DynamicDataBaseDTO> data = new List<DynamicDataBaseDTO>
					{
						new DynamicDataBaseDTO
						{
							ControlId = chapterCid,
							Value = selectedChapterLookupListId,
							ValueId = selectedChapterLookupListId
						}
					};
					((IDynamicDataClientManager)dynamicDataClientManager).SaveDataBase(context, data, eDynamicFormTypeDTO.PerDate);
					((IDynamicDataClientManager)dynamicDataClientManager).SaveDataBase(context, data, eDynamicFormTypeDTO.PerStudent);
				}
				else
				{
					CWLogger.Logger.Error("Common.UI.Web.Veterans.Controls.CtrlTaskCheckList:FailedToSetChapterBecausePid<0");
					TPMailMessageDTO item = new TPMailMessageDTO();
					((IEmailClientManager)new EmailClientManager()).SendEmails(new List<TPMailMessageDTO>
					{
						item
					}, "");
					HttpContext.Current.Response.Redirect("err.aspx?code=2", true);
				}
			}
			HttpContext.Current.Response.Redirect(".", true);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002B24 File Offset: 0x00000D24
		private int GetSelectedChapterLookupListId()
		{
			ListItem selectedItem = this.cmbChapter.SelectedItem;
			if (selectedItem == null)
			{
				return 0;
			}
			int result;
			if (!int.TryParse(selectedItem.Value, out result))
			{
				return 0;
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002B54 File Offset: 0x00000D54
		private void InitializeSteps(CtrlToDoItemChecked[] chks, List<VetTaskStep> steps)
		{
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < chks.Length; i++)
			{
				bool disallowEditing = false;
				VetTaskStep vetTaskStep = steps[i];
				CtrlToDoItemChecked ctrlToDoItemChecked = chks[i];
				string text = vetTaskStep.Description;
				bool flag3;
				bool isEnabled;
				if (flag)
				{
					flag3 = false;
					isEnabled = false;
					bool? flag4 = null;
				}
				else
				{
					bool? flag4;
					string text2;
					flag3 = this.GetShouldStepBeChecked(vetTaskStep, out text2, out flag4, out disallowEditing);
					isEnabled = (i == 0 || chks[i - 1].IsChecked);
					if (flag4 != null)
					{
						if (!string.IsNullOrEmpty(text2))
						{
							text += string.Format("<br /><br /><div class='AlertSmallest2'>Status: <b>{0}</b><br />* {1}</div>", flag4.Value ? "Approved" : "Denied", text2);
						}
						else
						{
							text += string.Format("<br /><br /><div class='AlertSmallest2'>Status: <b>{0}</b></div>", flag4.Value ? "Approved" : "Denied");
						}
					}
					else if (!string.IsNullOrEmpty(text2))
					{
						text += string.Format("<br /><br /><div class='AlertSmallest2'>* {0}</div>", Array.Empty<object>());
					}
				}
				bool isCurrent = false;
				if (!flag3)
				{
					if (!flag2)
					{
						flag2 = true;
						isCurrent = true;
					}
					flag = true;
				}
				if (this.allItemsAreDisabled)
				{
					isEnabled = false;
					this.cmbChapter.Enabled = false;
					this.btnChapterSubmit.Enabled = false;
				}
				ctrlToDoItemChecked.ID = vetTaskStep.Id;
				ctrlToDoItemChecked.Init(vetTaskStep.Title, vetTaskStep.Url, text, flag3, isEnabled, isCurrent, disallowEditing);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002CB1 File Offset: 0x00000EB1
		private IVeteranClientManager veteranClientManager
		{
			get
			{
				if (this._veteranClientManager == null)
				{
					this._veteranClientManager = new VeteranClientManager();
				}
				return this._veteranClientManager;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002CCC File Offset: 0x00000ECC
		private IDynamicFormsClientManager dynamicFormClientManager
		{
			get
			{
				if (this.dfm == null)
				{
					this.dfm = new DynamicFormClientManager();
				}
				return this.dfm;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002CE7 File Offset: 0x00000EE7
		private ISessionClientManager sessionClientManager
		{
			get
			{
				if (this.sm == null)
				{
					this.sm = new SessionClientManager();
				}
				return this.sm;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002D02 File Offset: 0x00000F02
		private SessionDTO Session
		{
			get
			{
				return this.ctrlTerm.SelectedSession.ToDTO();
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002D14 File Offset: 0x00000F14
		private PerDateEntryDTO PerDateEntry
		{
			get
			{
				bool flag;
				if (this._perDateEntrySession == null)
				{
					flag = true;
				}
				else
				{
					SessionDTO session = this.Session;
					flag = (session.StartDate.Date != this._perDateEntrySession.StartDate.Date || session.EndDate.Date != this._perDateEntrySession.EndDate.Date);
				}
				if (flag)
				{
					SessionDTO session2 = this.Session;
					PerDateEntryDTO existingPerDateEntry = this.dynamicDataClientManager.GetExistingPerDateEntry(this.pid, this.PackageScreenNum, session2);
					this._perDateEntry = existingPerDateEntry;
					this._perDateEntrySession = session2;
				}
				return this._perDateEntry;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002DBF File Offset: 0x00000FBF
		private int PackageScreenNum
		{
			get
			{
				return ((IWebSettingsClientManager)new WebSettingsClientManager()).GetSettingValue<int>(Setting.VETERANS_PackageFormNum);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002DD0 File Offset: 0x00000FD0
		private int ChapterCid
		{
			get
			{
				return ((IWebSettingsClientManager)new WebSettingsClientManager()).GetSettingValue<int>(Setting.VETERANS_ChapterCid);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002DE4 File Offset: 0x00000FE4
		private int screenNum
		{
			get
			{
				if (this._screenNum != null)
				{
					return this._screenNum.Value;
				}
				DynamicDataDTO studentsChapter = this.StudentsChapter;
				if (studentsChapter == null)
				{
					return 0;
				}
				string substringToMatch = studentsChapter.Value.ToString();
				IList<DynamicFormDTO> list = this.dynamicFormWebClientManager.FindFormByTitleSubstringMatch(substringToMatch, true, true);
				if (list == null || list.Count < 1)
				{
					return 0;
				}
				return list[0].ScreenNum;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002E4B File Offset: 0x0000104B
		private IDynamicFormsClientManager dynamicFormWebClientManager
		{
			get
			{
				if (this._dynamicFormWebClientManager == null)
				{
					this._dynamicFormWebClientManager = new DynamicFormClientManager();
				}
				return this._dynamicFormWebClientManager;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002E68 File Offset: 0x00001068
		private DynamicDataDTO StudentsChapter
		{
			get
			{
				bool flag;
				if (this._studentsChapterSession == null)
				{
					flag = true;
				}
				else
				{
					SessionDTO session = this.Session;
					flag = (this._studentsChapterSession.StartDate.Date != session.StartDate.Date || this._studentsChapterSession.EndDate.Date != session.EndDate.Date);
				}
				if (flag)
				{
					IDynamicDataClientManager dynamicDataClientManager = new DynamicDataClientManager();
					PerDateEntryDTO perDateEntry = this.PerDateEntry;
					DynamicDataContextDTO context = new DynamicDataContextDTO
					{
						PrimaryId = this.pid,
						SecondaryId = ((perDateEntry == null) ? 0 : perDateEntry.AppointmentId)
					};
					List<int> controlIds = new List<int>
					{
						this.ChapterCid
					};
					IList<DynamicDataDTO> list = (perDateEntry == null) ? null : dynamicDataClientManager.LoadDataByFields(context, controlIds, eDynamicFormTypeDTO.PerDate);
					if (list == null || list.Count < 1)
					{
						list = dynamicDataClientManager.LoadDataByFields(context, controlIds, eDynamicFormTypeDTO.PerStudent);
					}
					if (list == null || list.Count < 1)
					{
						return null;
					}
					this._studentsChapter = list[0];
					this._studentsChapterSession = this.Session;
				}
				return this._studentsChapter;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002F88 File Offset: 0x00001188
		private bool GetShouldStepBeChecked(VetTaskStep step, out string messageToStudent, out bool? isApproved, out bool disallowEditing)
		{
			disallowEditing = false;
			switch (step.StepType)
			{
			case eVetTaskStepType.ChooseTermDates:
				isApproved = null;
				messageToStudent = null;
				return true;
			case eVetTaskStepType.Register:
			{
				bool result = this.pid > 0;
				messageToStudent = null;
				isApproved = null;
				disallowEditing = true;
				return result;
			}
			case eVetTaskStepType.SelectChapter:
			{
				DynamicDataDTO studentsChapter = this.StudentsChapter;
				if (studentsChapter != null && studentsChapter.ValueId > 0)
				{
					string b = studentsChapter.ValueId.ToString();
					foreach (object obj in this.cmbChapter.Items)
					{
						ListItem listItem = (ListItem)obj;
						if (listItem.Value == b)
						{
							listItem.Selected = true;
							break;
						}
					}
					messageToStudent = null;
					isApproved = null;
					return true;
				}
				this.cmbChapter.SelectedIndex = -1;
				this.selectedChapterText = "";
				messageToStudent = null;
				isApproved = null;
				return false;
			}
			case eVetTaskStepType.CompleteBenefitRequestForm:
				messageToStudent = null;
				isApproved = null;
				return this.veteranClientManager.HasUserCompletedBenefitRequestForm(this.pid, this.PerDateEntry, this.screenNum);
			case eVetTaskStepType.ConsentToAgreementForm:
			{
				messageToStudent = null;
				isApproved = null;
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				return this.veteranClientManager.HasUserCompletedAgreementForm(this.pid, this.PerDateEntry, webSettingsClientManager.GetSettingValue<int>(Setting.VETERANS_AgreementFormNum));
			}
			case eVetTaskStepType.BenefitCounselorReview:
			{
				bool? flag = this.veteranClientManager.CounselorResult(this.pid, this.Session, this.PerDateEntry, out messageToStudent);
				isApproved = ((flag != null) ? new bool?(flag.Value) : null);
				return flag != null;
			}
			case eVetTaskStepType.AdministratorReview:
			{
				bool? flag2 = this.veteranClientManager.AdministratorResult(this.pid, this.Session, this.PerDateEntry, out messageToStudent);
				isApproved = ((flag2 != null) ? new bool?(flag2.Value) : null);
				return flag2 != null;
			}
			default:
				messageToStudent = null;
				isApproved = null;
				return false;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000031B4 File Offset: 0x000013B4
		private void BuildControlHeiarchy()
		{
			this.Controls.Add(this.chkStep1);
			this.Controls.Add(this.chkStep2);
			this.Controls.Add(this.chkStep3);
			this.Controls.Add(this.chkStep4);
			this.Controls.Add(this.chkStep5);
			this.Controls.Add(this.chkStep6);
			this.Controls.Add(this.chkStep7);
		}

		// Token: 0x04000001 RID: 1
		private CtrlToDoItemChecked chkStep1 = new CtrlToDoItemChecked();

		// Token: 0x04000002 RID: 2
		private CtrlToDoItemChecked chkStep2 = new CtrlToDoItemChecked();

		// Token: 0x04000003 RID: 3
		private CtrlToDoItemChecked chkStep3 = new CtrlToDoItemChecked();

		// Token: 0x04000004 RID: 4
		private CtrlToDoItemChecked chkStep4 = new CtrlToDoItemChecked();

		// Token: 0x04000005 RID: 5
		private CtrlToDoItemChecked chkStep5 = new CtrlToDoItemChecked();

		// Token: 0x04000006 RID: 6
		private CtrlToDoItemChecked chkStep6 = new CtrlToDoItemChecked();

		// Token: 0x04000007 RID: 7
		private CtrlToDoItemChecked chkStep7 = new CtrlToDoItemChecked();

		// Token: 0x04000008 RID: 8
		private Label lblChapter = new Label();

		// Token: 0x04000009 RID: 9
		private DropDownList cmbChapter = new DropDownList();

		// Token: 0x0400000A RID: 10
		private Button btnChapterSubmit = new Button();

		// Token: 0x0400000B RID: 11
		private CtrlTermChooser ctrlTerm = new CtrlTermChooser();

		// Token: 0x0400000F RID: 15
		private int _pid;

		// Token: 0x04000010 RID: 16
		private bool allItemsAreDisabled;

		// Token: 0x04000011 RID: 17
		private IDynamicFieldClientManager _dynamicFieldClientManager;

		// Token: 0x04000012 RID: 18
		private List<DynamicListItemDTO> _chapterListItems;

		// Token: 0x04000013 RID: 19
		private IDynamicDataClientManager _dynamicDataClientManager;

		// Token: 0x04000014 RID: 20
		private IVeteranClientManager _veteranClientManager;

		// Token: 0x04000015 RID: 21
		private IDynamicFormsClientManager dfm;

		// Token: 0x04000016 RID: 22
		private ISessionClientManager sm;

		// Token: 0x04000017 RID: 23
		private SessionDTO _perDateEntrySession;

		// Token: 0x04000018 RID: 24
		private PerDateEntryDTO _perDateEntry;

		// Token: 0x04000019 RID: 25
		private string selectedChapterText = "";

		// Token: 0x0400001A RID: 26
		private int? _screenNum;

		// Token: 0x0400001B RID: 27
		private IDynamicFormsClientManager _dynamicFormWebClientManager;

		// Token: 0x0400001C RID: 28
		private SessionDTO _studentsChapterSession;

		// Token: 0x0400001D RID: 29
		private DynamicDataDTO _studentsChapter;
	}
}
