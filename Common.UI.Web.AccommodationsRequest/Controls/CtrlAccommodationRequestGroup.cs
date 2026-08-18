using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ClockWorkLogger;
using skmValidators;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests.SelfRegProcessing;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.LookupCourses;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.Core.StudentAccommodationRequests;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.ClientManager.ICore.StudentAccommodationRequests;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegProcessing;
using TechnoPro.Common.TextFormat.Adapters;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Cache;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.AccommodationsRequest.Adapters;
using TechnoPro.Common.UI.Web.Entity.Adapters;

namespace TechnoPro.Common.UI.Web.AccommodationsRequest.Controls
{
	// Token: 0x02000004 RID: 4
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlAccommodationRequestGroup runat=server></{0}:CtrlAccommodationRequestGroup>")]
	public class CtrlAccommodationRequestGroup : WebControl, INamingContainer
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002FF8 File Offset: 0x000011F8
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			string webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(base.GetType(), "TechnoPro.Common.UI.Web.AccommodationsRequest.js.js_accommodation_request_group.js");
			this.Page.ClientScript.RegisterClientScriptInclude(base.GetType(), "cwjs", webResourceUrl);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00003047 File Offset: 0x00001247
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000304F File Offset: 0x0000124F
		public string Title { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00003058 File Offset: 0x00001258
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00003060 File Offset: 0x00001260
		public int Pid { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00003069 File Offset: 0x00001269
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00003071 File Offset: 0x00001271
		public int Lucid { get; set; }

		// Token: 0x0600002D RID: 45 RVA: 0x0000307C File Offset: 0x0000127C
		private void MyInit()
		{
			this.Pid = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid(this.Page);
			string text = this.Page.Request.QueryString["lucid"];
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				try
				{
					INavigatorClientManager navigatorClientManager = new NavigatorClientManager();
					int num = navigatorClientManager.ConvertUrlStringToIntParameter(text);
					bool flag2 = num > 0;
					if (flag2)
					{
						this.Lucid = num;
					}
				}
				catch
				{
				}
			}
			string text2 = this.Page.Request.QueryString["sd"];
			DateTime dateTime = DateTime.MinValue;
			DateTime dateTime2 = DateTime.MinValue;
			DateTime date;
			bool flag3 = !string.IsNullOrEmpty(text2) && DateTime.TryParse(text2, out date);
			if (flag3)
			{
				ISessionClientManager sessionClientManager = new SessionClientManager();
				SessionDTO sessionByDate = sessionClientManager.GetSessionByDate(date);
				bool flag4 = sessionByDate != null;
				if (flag4)
				{
					dateTime = sessionByDate.StartDate;
					dateTime2 = sessionByDate.EndDate;
				}
			}
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.TESTBOOKING_AccommodationsExpiryDateCid);
			ILookupCourseClientManager lookupCourseClientManager = new LookupCourseClientManager();
			LookupCourseDTO lookupCourseDTO = lookupCourseClientManager.LoadCourseByLuCourseId(this.Lucid);
			bool flag5 = dateTime != DateTime.MinValue && dateTime2 != DateTime.MinValue;
			if (!flag5)
			{
				bool flag6 = lookupCourseDTO != null;
				if (flag6)
				{
					dateTime = lookupCourseDTO.StartDate;
					dateTime2 = lookupCourseDTO.EndDate;
				}
				else
				{
					dateTime = DateTime.Now.Date;
					dateTime2 = DateTime.Now.Date.AddYears(2);
				}
			}
			int courseEndDateExtension = webSettingsClientManager.GetSettingValue<int>(Setting.SELFREGC_CourseEndDateAuthorizationExtensionInDays);
			DateTime today = DateTime.Now.Date;
			IStudentAccommodationReqClientManager studentAccommodationReqClientManager = new StudentAccommodationReqClientManager();
			IList<CourseRegistrationWithAccommodationRequestDTO> list = studentAccommodationReqClientManager.LoadCourseRegistrationsWithRequestByStudentAndDate(this.Pid, dateTime, dateTime2, true);
			IEnumerable<CourseRegistrationWithAccommodationRequestDTO> source = ((list != null) ? (from m in list
			where m.CourseRegistrationWithAccommodations.CourseReg.Course.EndDate.AddDays((double)courseEndDateExtension) >= today
			select m) : null) ?? new List<CourseRegistrationWithAccommodationRequestDTO>();
			List<CourseRegistrationWithAccommodationRequestDTO> list2 = source.ToList<CourseRegistrationWithAccommodationRequestDTO>();
			bool flag7 = list2.Count < 1;
			if (flag7)
			{
				this.FireButtonCancelClicked("1");
			}
			else
			{
				int settingValue2 = webSettingsClientManager.GetSettingValue<int>(Setting.INSTRUCTOR_DontShowStudentAccommodationCid);
				List<int> cidsToHide = (from g in new int[]
				{
					settingValue,
					settingValue2
				}
				where g > 0
				select g).ToList<int>();
				bool flag8 = cidsToHide.Count > 0;
				if (flag8)
				{
					Func<AccommodationDataDTO, bool> <>9__5;
					foreach (CourseRegistrationWithAccommodationRequestDTO courseRegistrationWithAccommodationRequestDTO in list2)
					{
						CourseRegistrationWithAccommodationsDTO courseRegistrationWithAccommodations = courseRegistrationWithAccommodationRequestDTO.CourseRegistrationWithAccommodations;
						IEnumerable<AccommodationDataDTO> courseOrTemplateAccommodations = courseRegistrationWithAccommodationRequestDTO.CourseRegistrationWithAccommodations.CourseOrTemplateAccommodations;
						Func<AccommodationDataDTO, bool> predicate;
						if ((predicate = <>9__5) == null)
						{
							predicate = (<>9__5 = ((AccommodationDataDTO f) => cidsToHide.All((int g) => f.Data.Field.ControlId != g)));
						}
						courseRegistrationWithAccommodations.CourseOrTemplateAccommodations = courseOrTemplateAccommodations.Where(predicate).ToList<AccommodationDataDTO>();
					}
				}
				CourseRegistrationWithAccommodationRequestDTO foundCourse = list2.Find((CourseRegistrationWithAccommodationRequestDTO f) => f.CourseRegistrationWithAccommodations.CourseReg.Course.LuCourseId == this.Lucid);
				bool flag9 = foundCourse == null;
				if (flag9)
				{
					this.AbortThisRequest();
				}
				else
				{
					DateTime? dontAllowIfCourseStartDatePastThisDate = null;
					bool settingValue3 = webSettingsClientManager.GetSettingValue<bool>(Setting.SELFREGC_DontAllowStudentsToCompleteSelfRegForCoursesStartingAfterAccommodationsExpiryDate);
					bool flag10 = settingValue3;
					if (flag10)
					{
						IAccommodationsClientManager accommodationsClientManager = new AccommodationsClientManager();
						dontAllowIfCourseStartDatePastThisDate = accommodationsClientManager.GetStudentAccommodationsExpiryDate(this.Pid);
					}
					List<CourseRegistrationWithAccommodationRequestDTO> matchingCourses = (from f in list2
					where (f.AccommodationRequest == null || f.AccommodationRequest.Status == eStudentCourseAccommodationRequestStatusDTO.PendingWaitingForStudent || f.AccommodationRequest.Status == eStudentCourseAccommodationRequestStatusDTO.Unknown) && CtrlAccommodationRequestGroup.AccommodationsMatch(f.CourseRegistrationWithAccommodations.CourseOrTemplateAccommodations, foundCourse.CourseRegistrationWithAccommodations.CourseOrTemplateAccommodations) && (dontAllowIfCourseStartDatePastThisDate == null || f.CourseRegistrationWithAccommodations.CourseReg.Course.StartDate < dontAllowIfCourseStartDatePastThisDate.Value)
					select f).ToList<CourseRegistrationWithAccommodationRequestDTO>();
					this.AddCoursesToScreen(matchingCourses, this.Lucid.ToString());
					IAccommodationsClientManager accommodationsClientManager2 = new AccommodationsClientManager();
					bool flag11;
					List<AccommodationDataDTO> list3 = (from g in accommodationsClientManager2.LoadAccommodationsByStudentAndCourseOrTemplate(this.Pid, 0, out flag11)
					where cidsToHide.All((int h) => g.Data.Field.ControlId != h)
					select g).ToList<AccommodationDataDTO>();
					bool flag12 = list3.Count < 1;
					if (flag12)
					{
						SessionCaching.CurrentInstance.Insert("selfregc_msgcode", "2");
						HttpContext.Current.Response.Redirect("courses.aspx", true);
					}
					else
					{
						this.AddAccommodationsToScreen(list3.ToList<AccommodationDataDTO>(), foundCourse.CourseRegistrationWithAccommodations.CourseOrTemplateAccommodations.ToList<AccommodationDataDTO>());
						string text3 = webSettingsClientManager.GetSettingValue<string>(Setting.SELFREGC_ConfidentialityAgreement);
						bool settingValue4 = webSettingsClientManager.GetSettingValue<bool>(Setting.SELFREGC_ShowConfidentialityAgreementAsHtml);
						bool flag13 = settingValue4;
						if (flag13)
						{
							this._agreementHtml = text3;
						}
						else
						{
							this._agreementHtml = null;
							text3 = text3.ConvertHtmlToPlainText();
							this.txt_confidentialityAgreement.Text = text3;
						}
					}
				}
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003520 File Offset: 0x00001720
		private bool AddAccommodationsToScreen(IEnumerable<AccommodationDataDTO> templateItems0, List<AccommodationDataDTO> courseSpecificItems)
		{
			bool flag = this.chks_accommodations.Items.Count > 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string text = webSettingsClientManager.GetSettingValue<string>(Setting.ACCOMMODATIONS_HiddenControlIds) ?? "";
				List<int> list = (from h in text.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries).Select(delegate(string g)
				{
					int result2;
					int.TryParse(g.Trim(), out result2);
					return result2;
				})
				where h > 0
				select h).Distinct<int>().ToList<int>();
				string text2 = (webSettingsClientManager.GetSettingValue<string>(Setting.SELFREGC_VisibleButUncheckableControlIds_AutoApproveHiddenAccommodations) ?? "").Trim().ToLower();
				bool flag2 = text2 == "all";
				int[] array;
				if (!flag2)
				{
					array = (from m in text2.Split(new char[]
					{
						','
					}).Select(delegate(string g)
					{
						string text3 = g.Trim();
						int result2 = 0;
						bool flag9 = text3.Length < 1 || !int.TryParse(text3, out result2);
						if (flag9)
						{
							result2 = 0;
						}
						return result2;
					})
					where m > 0
					select m).ToArray<int>();
				}
				else
				{
					array = null;
				}
				int[] array2 = array;
				bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.SELFREGC_AllAccommodationsShouldBeUncheckedByDefault);
				this._hidingAccommodations = new List<AccommodationDataDTO>();
				List<AccommodationDataDTO> list2 = ((templateItems0 != null) ? templateItems0.ToList<AccommodationDataDTO>() : null) ?? new List<AccommodationDataDTO>();
				list2.Sort((AccommodationDataDTO g1, AccommodationDataDTO g2) => g1.GetDisplayString().CompareTo(g2.GetDisplayString()));
				List<MyListItem> list3 = new List<MyListItem>();
				int num = 0;
				using (List<AccommodationDataDTO>.Enumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AccommodationDataDTO acc = enumerator.Current;
						bool flag3 = !list.Contains(acc.Data.Field.ControlId);
						if (flag3)
						{
							bool flag4 = courseSpecificItems.Find((AccommodationDataDTO f) => f.Data.Field.ControlId.Equals(acc.Data.Field.ControlId)) != null;
							string displayString = acc.GetDisplayString();
							int controlId = acc.Data.Field.ControlId;
							string value = controlId.ToString();
							ListItem listItem = new ListItem(displayString, value);
							bool flag5 = flag2 || (array2 != null && array2.Contains(controlId));
							if (flag5)
							{
								listItem.Enabled = false;
								listItem.Selected = false;
								listItem.Attributes.Add("Title", "'" + displayString + "' cannot be un-checked.");
								num++;
								this.chks_accommodations.Items.Add(listItem);
								listItem.Selected = true;
							}
							else
							{
								listItem.Attributes.Add("Title", displayString);
								this.chks_accommodations.Items.Add(listItem);
								listItem.Selected = (!settingValue && flag4);
							}
							list3.Add(new MyListItem
							{
								Text = displayString,
								Value = value
							});
						}
						else
						{
							this._hidingAccommodations.Add(acc);
						}
					}
				}
				int count = this.chks_accommodations.Items.Count;
				bool flag6 = count < 1;
				if (flag6)
				{
					this.chks_accommodations_validator.MinimumNumberOfSelectedCheckBoxes = 0;
				}
				else
				{
					bool flag7 = count == num;
					if (flag7)
					{
						this.btnCheckAll.Visible = false;
						this.btnCheckNone.Visible = false;
					}
				}
				IWebSettingsClientManager webSettingsClientManager2 = new WebSettingsClientManager();
				bool settingValue2 = webSettingsClientManager2.GetSettingValue<bool>(Setting.SELFREGC_DisableCheckAllCheckNoneButtonsForAccommodations);
				bool flag8 = settingValue2;
				if (flag8)
				{
					this.btnCheckAll.Visible = false;
					this.btnCheckNone.Visible = false;
				}
				result = true;
			}
			return result;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003918 File Offset: 0x00001B18
		private bool AddCoursesToScreen(IEnumerable<CourseRegistrationWithAccommodationRequestDTO> matchingCourses, string primaryLucidString)
		{
			bool flag = this.chks_courses.Items.Count > 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.chks_courses.Items.Clear();
				List<MyListItem> list = new List<MyListItem>();
				foreach (CourseRegistrationWithAccommodationRequestDTO courseRegistrationWithAccommodationRequestDTO in matchingCourses)
				{
					LookupCourseDTO course = courseRegistrationWithAccommodationRequestDTO.CourseRegistrationWithAccommodations.CourseReg.Course;
					ListItem listItem = new ListItem(course.GetCheckBoxCourseDescription(), course.LuCourseId.ToString());
					this.chks_courses.Items.Add(listItem);
					list.Add(new MyListItem
					{
						Text = listItem.Text,
						Value = listItem.Value
					});
				}
				bool flag2 = this.chks_courses.Items.Count > 0;
				if (flag2)
				{
					foreach (object obj in this.chks_courses.Items)
					{
						ListItem listItem2 = (ListItem)obj;
						bool flag3 = !listItem2.Value.Equals(primaryLucidString);
						if (!flag3)
						{
							listItem2.Selected = true;
							break;
						}
					}
				}
				else
				{
					this.chks_courses_validator.MinimumNumberOfSelectedCheckBoxes = 0;
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003AA4 File Offset: 0x00001CA4
		private static bool AccommodationsMatch(IList<AccommodationDataDTO> a1, IList<AccommodationDataDTO> a2)
		{
			bool flag = a1 == null || a2 == null || a1.Count != a2.Count;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				result = (from item in a1
				select a2.FirstOrDefault((AccommodationDataDTO f) => f.Data.Field.ControlId.Equals(item.Data.Field.ControlId))).All((AccommodationDataDTO found) => found != null);
			}
			return result;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003B25 File Offset: 0x00001D25
		private void AbortThisRequest()
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002EB0 File Offset: 0x000010B0
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.RenderBeginTag("div");
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003B28 File Offset: 0x00001D28
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003B39 File Offset: 0x00001D39
		private void RenderPageTitle(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.H1);
			writer.Write(this.Title ?? "Request Accommodations");
			writer.RenderEndTag();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003B62 File Offset: 0x00001D62
		private void RenderPageIntro(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "Intro6");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.lbl_intro.RenderControl(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003B90 File Offset: 0x00001D90
		private void RenderAccommodations(HtmlTextWriter writer)
		{
			this.btnCheckAll.Attributes.Add("alt", "Check all accommodations");
			this.btnCheckAll.Attributes.Add("onclick", "CheckAll(this,'" + this.chks_accommodations.ClientID + "'); return false;");
			this.btnCheckAll.Attributes.Add("class", "ButtonAsLink");
			this.btnCheckAll.InnerText = "check all";
			this.btnCheckNone.Attributes.Add("alt", "Un-check all courses");
			this.btnCheckNone.Attributes.Add("onclick", "CheckNone(this, '" + this.chks_accommodations.ClientID + "'); return false;");
			this.btnCheckNone.Attributes.Add("class", "ButtonAsLink");
			this.btnCheckNone.InnerText = "check none";
			this.chks_accommodations.CssClass = "checkbox checkboxlist";
			this.chks_accommodations.Width = new Unit(100.0, UnitType.Percentage);
			writer.RenderMyControl(this.chks_accommodations_validator).RenderMyCheckBoxList(this.chks_accommodations, "Your accommodations").RenderMyControl(this.btnCheckAll).WriteMyText("&nbsp;&nbsp;&nbsp;").RenderMyControl(this.btnCheckNone);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003CF8 File Offset: 0x00001EF8
		private void RenderCourses(HtmlTextWriter writer)
		{
			HtmlButton htmlButton = new HtmlButton();
			htmlButton.Attributes.Add("alt", "Check all courses");
			htmlButton.Attributes.Add("onclick", "CheckAll(this,'" + this.chks_courses.ClientID + "'); return false;");
			htmlButton.Attributes.Add("class", "ButtonAsLink");
			htmlButton.InnerText = "check all";
			HtmlButton htmlButton2 = new HtmlButton();
			htmlButton2.Attributes.Add("alt", "Un-check all courses");
			htmlButton2.Attributes.Add("onclick", "CheckNone(this, '" + this.chks_courses.ClientID + "'); return false;");
			htmlButton2.Attributes.Add("class", "ButtonAsLink");
			htmlButton2.InnerText = "check none";
			writer.RenderMyControl(this.chks_courses_validator).RenderMyCheckBoxList(this.chks_courses, "Courses to request");
			bool visible = htmlButton.Visible;
			if (visible)
			{
				writer.RenderMyControl(htmlButton).WriteMyText("&nbsp;&nbsp;&nbsp;").RenderMyControl(htmlButton2);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003E18 File Offset: 0x00002018
		private void RenderAccommodationsOptions(HtmlTextWriter writer)
		{
			this.txt_note.Style.Add(HtmlTextWriterStyle.Width, "!100%");
			this.txt_note.CssClass = "form-control";
			bool showTextNote = this._showTextNote;
			writer.RenderMyBeginTag(HtmlTextWriterTag.Fieldset, "form-group", null, new MyStyleAttribute[]
			{
				new MyStyleAttribute(HtmlTextWriterStyle.Display, "table-cell")
			}).RenderMyBeginTag(HtmlTextWriterTag.Legend, "", null, new MyStyleAttribute[]
			{
				new MyStyleAttribute(HtmlTextWriterStyle.MarginBottom, "4px")
			}).RenderMyLabel("", this.rbtns_accommodationsApproval, "Please indicate if your accommodations require any changes", new MyStyleAttribute[]
			{
				new MyStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "pre-wrap")
			}).RenderMyEndTag(true).RenderMyBeginTag(HtmlTextWriterTag.Div, "row", null, Array.Empty<MyStyleAttribute>()).RenderMyBeginTag(HtmlTextWriterTag.Div, "radio radiobuttonlist col-md-5", null, Array.Empty<MyStyleAttribute>()).RenderMyControl(this.rbtns_accommodationsApproval).RenderMyEndTag(true).RenderMyBeginTag(HtmlTextWriterTag.Div, "col-md-5", null, Array.Empty<MyStyleAttribute>()).RenderMyLabel(showTextNote, "", this.txt_note, "Optional note:<br />", new MyStyleAttribute[]
			{
				new MyStyleAttribute(HtmlTextWriterStyle.VerticalAlign, "Top")
			}).RenderMyControl(showTextNote, this.txt_note).RenderMyEndTag(true).RenderMyEndTag(true).RenderMyEndTag(true);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003F5C File Offset: 0x0000215C
		private void RenderConfidentialityAgreement(HtmlTextWriter writer)
		{
			bool flag = !string.IsNullOrEmpty(this._agreementHtml);
			if (flag)
			{
				writer.Write(this._agreementHtml);
			}
			else
			{
				this.lbl_confidentialityAgreement.RenderControl(writer);
				writer.AddAttribute(HtmlTextWriterAttribute.Style, "min-width: 100%; border: 1px solid #333; padding: 4px;");
				this.txt_confidentialityAgreement.RenderControl(writer);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003FB8 File Offset: 0x000021B8
		private void RenderIAgreeAndSubmitButtons(HtmlTextWriter writer)
		{
			this.chk_iagree_validator.RenderControl(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Align, "right");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.chk_iagree.LabelAttributes.Add("style", "margin-left: 6px");
			this.chk_iagree.RenderControl(writer);
			writer.Write("<br />");
			this.btn_Cancel.CssClass = "btn btn-lg btn-outline-secondary";
			this.btn_Cancel.Attributes.Add("onclick", "return confirm('Are you sure you want to cancel?');");
			this.btn_Cancel.RenderControl(writer);
			writer.Write("&nbsp;&nbsp;&nbsp;");
			this.btn_Submit.CssClass = "btn btn-lg btn-primary";
			this.btn_Submit.RenderControl(writer);
			writer.RenderEndTag();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004088 File Offset: 0x00002288
		protected override void Render(HtmlTextWriter writer)
		{
			this.validationSummary.RenderControl(writer);
			this.RenderPageTitle(writer);
			this.RenderPageIntro(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "container - fluid");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "row");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "col-md-6");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "well well-sm");
			writer.AddStyleAttribute(HtmlTextWriterStyle.MarginRight, "3px");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderAccommodations(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "col-md-6");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "well well-sm");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderCourses(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.Write("<br />");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "well well-lg");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderAccommodationsOptions(writer);
			writer.RenderEndTag();
			writer.Write("<br /><br />");
			this.RenderConfidentialityAgreement(writer);
			writer.Write("<br />");
			this.RenderIAgreeAndSubmitButtons(writer);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000041DF File Offset: 0x000023DF
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			this.MyInit();
			base.OnInit(e);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000041F8 File Offset: 0x000023F8
		private void InitializeControls()
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			this._showTextNote = webSettingsClientManager.GetSettingValue<bool>(Setting.SELFREGC_AllowStudentsToSubmitANoteWhenCompletingTheirSelfRegRequests);
			this.lbl_intro.ID = "lbl_intro";
			this.lbl_intro.Text = webSettingsClientManager.GetSettingValue<string>(Setting.SELFREGC_Wording_Request_IntroductionText);
			this.rbtns_accommodationsApproval.ID = "rbtns_accommodationsApproval";
			string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.SELFREGC_Wording_MyAccommodationsAreCorrectTheWayTheyAre);
			string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.SELFREGC_Wording_INeedAdditionalAccommodations);
			string settingValue3 = webSettingsClientManager.GetSettingValue<string>(Setting.SELFREGC_Wording_INeedToChangeOrRemoveAnAccommodation);
			bool flag = !string.IsNullOrEmpty(settingValue);
			if (flag)
			{
				this.rbtns_accommodationsApproval.Items.Add(new ListItem(settingValue, "0"));
			}
			bool flag2 = !string.IsNullOrEmpty(settingValue2);
			if (flag2)
			{
				this.rbtns_accommodationsApproval.Items.Add(new ListItem(settingValue2, "1"));
			}
			bool flag3 = !string.IsNullOrEmpty(settingValue3);
			if (flag3)
			{
				this.rbtns_accommodationsApproval.Items.Add(new ListItem(settingValue3, "2"));
			}
			this.rbtns_accommodationsApproval.Width = new Unit(100.0, UnitType.Percentage);
			this.lb_accommodations.ID = "lb_accommodations";
			this.lb_accommodations.Rows = 8;
			this.lb_accommodations.Width = new Unit(350.0, UnitType.Pixel);
			this.chks_accommodations.ID = "chks_accommodations";
			this.chks_accommodations.Width = new Unit(350.0, UnitType.Pixel);
			this.chk_iagree.ID = "chk_iagree";
			this.chk_iagree.Text = "I agree to the terms outlined above";
			this.chk_iagree.CssClass = "largecheckbox";
			this.chks_courses.ID = "chks_courses";
			this.chks_courses.Width = new Unit(100.0, UnitType.Percentage);
			this.btn_Submit.Text = "Submit";
			this.btn_Submit.ID = "btn_submit";
			this.btn_Cancel.ID = "btn_cancel";
			this.btn_Cancel.Text = "Cancel";
			this.btn_Cancel.CausesValidation = false;
			this.chk_iagree_validator.ID = "chk_iagree_validator";
			this.chk_iagree_validator.Display = ValidatorDisplay.Dynamic;
			this.chk_iagree_validator.ErrorMessage = "You must agree to the terms of service before you can continue.";
			this.chk_iagree_validator.Text = "*";
			this.chk_iagree_validator.ControlToValidate = "chk_iagree";
			this.chk_iagree_validator.SetFocusOnError = true;
			this.rbtns_accommodationsApproval_validator.ID = "rbtns_accommodationsApproval_validator";
			this.rbtns_accommodationsApproval_validator.Display = ValidatorDisplay.Dynamic;
			this.rbtns_accommodationsApproval_validator.Text = "*";
			this.rbtns_accommodationsApproval_validator.ErrorMessage = "Please select an option that best indicates the status of these accommodations for you.";
			this.rbtns_accommodationsApproval_validator.ControlToValidate = "rbtns_accommodationsApproval";
			this.rbtns_accommodationsApproval_validator.SetFocusOnError = true;
			this.validationSummary.ID = "validationSummary";
			this.validationSummary.ShowSummary = false;
			this.validationSummary.HeaderText = "Please correct the following problem(s) in order to continue:";
			this.validationSummary.ShowMessageBox = true;
			this.chks_courses_validator.ID = "chks_courses_validator";
			this.chks_courses_validator.Display = ValidatorDisplay.Dynamic;
			this.chks_courses_validator.Text = "*";
			this.chks_courses_validator.ErrorMessage = "Please check at least one course";
			this.chks_courses_validator.ControlToValidate = "chks_courses";
			this.chks_courses_validator.MinimumNumberOfSelectedCheckBoxes = 1;
			this.chks_courses_validator.SetFocusOnError = true;
			this.chks_accommodations_validator.ID = "chks_accommodations_validator";
			this.chks_accommodations_validator.Display = ValidatorDisplay.Dynamic;
			this.chks_accommodations_validator.Text = "*";
			this.chks_accommodations_validator.ErrorMessage = "Please check at least one accommodation.";
			this.chks_accommodations_validator.ControlToValidate = "chks_accommodations";
			this.chks_accommodations_validator.MinimumNumberOfSelectedCheckBoxes = 1;
			this.chks_accommodations_validator.SetFocusOnError = true;
			this.lbl_confidentialityAgreement.ID = "lbl_confidentialityAgreement";
			this.lbl_confidentialityAgreement.Text = "<h3>Terms</h3>";
			this.txt_confidentialityAgreement.ID = "txt_confidentialityAgreement";
			this.txt_confidentialityAgreement.TextMode = TextBoxMode.MultiLine;
			this.txt_confidentialityAgreement.Rows = 12;
			this.txt_confidentialityAgreement.Columns = 50;
			this.txt_confidentialityAgreement.Width = new Unit(100.0, UnitType.Percentage);
			this.txt_confidentialityAgreement.Text = "";
			this.txt_confidentialityAgreement.ReadOnly = true;
			this.txt_confidentialityAgreement.CausesValidation = false;
			this.lbl_confidentialityAgreement.AssociatedControlID = this.txt_confidentialityAgreement.ID;
			this.btn_Submit.Click += this.btn_Submit_Click;
			this.btn_Cancel.Click += this.btn_Cancel_Click;
			this.txt_note.ID = "txt_note";
			this.txt_note.TextMode = TextBoxMode.MultiLine;
			this.txt_note.Rows = 3;
			this.txt_note.Width = new Unit(100.0, UnitType.Percentage);
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600003E RID: 62 RVA: 0x00004728 File Offset: 0x00002928
		// (remove) Token: 0x0600003F RID: 63 RVA: 0x00004760 File Offset: 0x00002960
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<AbortSelfRegHandler> ButtonCancelClicked;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000040 RID: 64 RVA: 0x00004798 File Offset: 0x00002998
		// (remove) Token: 0x06000041 RID: 65 RVA: 0x000047D0 File Offset: 0x000029D0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler ButtonSubmitClicked;

		// Token: 0x06000042 RID: 66 RVA: 0x00004805 File Offset: 0x00002A05
		private void FireButtonCancelClicked(string selfRegCMsgCode = null)
		{
			EventHandler<AbortSelfRegHandler> buttonCancelClicked = this.ButtonCancelClicked;
			if (buttonCancelClicked != null)
			{
				buttonCancelClicked(this, new AbortSelfRegHandler
				{
					SelfRegCMsgCode = selfRegCMsgCode
				});
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004828 File Offset: 0x00002A28
		private void FireButtonSubmitClicked()
		{
			EventHandler buttonSubmitClicked = this.ButtonSubmitClicked;
			if (buttonSubmitClicked != null)
			{
				buttonSubmitClicked(this, new EventArgs());
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004843 File Offset: 0x00002A43
		public void btn_Cancel_Click(object sender, EventArgs e)
		{
			this.FireButtonCancelClicked(null);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000484E File Offset: 0x00002A4E
		public void btn_Submit_Click(object sender, EventArgs e)
		{
			this.SaveAndClose();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00004858 File Offset: 0x00002A58
		private eSelfRegCoursesAccommodationsStatus GetSelectedCoursesAccommodationsStatus()
		{
			string selectedValue = this.rbtns_accommodationsApproval.SelectedValue;
			string text = selectedValue;
			string a = text;
			eSelfRegCoursesAccommodationsStatus result;
			if (!(a == "0"))
			{
				if (!(a == "1"))
				{
					if (!(a == "2"))
					{
						result = eSelfRegCoursesAccommodationsStatus.Unknown;
					}
					else
					{
						result = eSelfRegCoursesAccommodationsStatus.INeedToChangeOrRemoveAnAccommodation;
					}
				}
				else
				{
					result = eSelfRegCoursesAccommodationsStatus.INeedAdditionalAccommodations;
				}
			}
			else
			{
				result = eSelfRegCoursesAccommodationsStatus.MyAccommodationsAreCorrectTheWayTheyAre;
			}
			return result;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000048B0 File Offset: 0x00002AB0
		private IList<SelfRegCourseInfoDTO> GetSelectedLucidsWithCourseDescriptions()
		{
			List<SelfRegCourseInfoDTO> list = new List<SelfRegCourseInfoDTO>();
			foreach (object obj in this.chks_courses.Items)
			{
				ListItem listItem = (ListItem)obj;
				bool flag = !listItem.Selected;
				if (!flag)
				{
					int clucid = int.Parse(listItem.Value);
					bool flag2 = list.All((SelfRegCourseInfoDTO g) => g.LuCourseId != clucid);
					if (flag2)
					{
						list.Add(new SelfRegCourseInfoDTO
						{
							LuCourseId = clucid,
							CourseDescription = listItem.Text,
							EncodedLucidForUrl = NavigatorClientManager.CurrentInstance.ConvertIntParameterToLongtermUrlString(clucid)
						});
					}
				}
			}
			return list;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000049A0 File Offset: 0x00002BA0
		private IList<SelfRegCheckedAccommodationDTO> GetAccommodations()
		{
			CtrlAccommodationRequestGroup.<>c__DisplayClass64_0 CS$<>8__locals1 = new CtrlAccommodationRequestGroup.<>c__DisplayClass64_0();
			CS$<>8__locals1.checkedItems = (from ListItem li in this.chks_accommodations.Items
			select new SelfRegCheckedAccommodationDTO
			{
				ControlId = int.Parse(li.Value),
				Text = li.Text,
				IsChecked = li.Selected
			}).ToList<SelfRegCheckedAccommodationDTO>();
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string text = (webSettingsClientManager.GetSettingValue<string>(Setting.SELFREGC_VisibleButUncheckableControlIds_AutoApproveHiddenAccommodations) ?? "").Trim().ToLower();
			bool flag = text == "all";
			bool flag2 = !flag && text.Length < 1;
			IList<SelfRegCheckedAccommodationDTO> checkedItems;
			if (flag2)
			{
				checkedItems = CS$<>8__locals1.checkedItems;
			}
			else
			{
				int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.TESTBOOKING_AccommodationsExpiryDateCid);
				int settingValue2 = webSettingsClientManager.GetSettingValue<int>(Setting.INSTRUCTOR_DontShowStudentAccommodationCid);
				CS$<>8__locals1.cidsToHide = (from g in new int[]
				{
					settingValue,
					settingValue2
				}
				where g > 0
				select g).ToList<int>();
				IAccommodationsClientManager accommodationsClientManager = new AccommodationsClientManager();
				bool flag3;
				List<AccommodationDataDTO> source = (from g in accommodationsClientManager.LoadAccommodationsByStudentAndCourseOrTemplate(this.Pid, 0, out flag3)
				where CS$<>8__locals1.cidsToHide.All((int h) => g.Data.Field.ControlId != h)
				select g).ToList<AccommodationDataDTO>();
				CtrlAccommodationRequestGroup.<>c__DisplayClass64_0 CS$<>8__locals2 = CS$<>8__locals1;
				List<int> cidsThatCannotBeUnchecked;
				if (!flag)
				{
					cidsThatCannotBeUnchecked = (from m in text.Split(new char[]
					{
						','
					}).Select(new Func<string, int>(CtrlAccommodationRequestGroup.TryParseInt))
					where m > 0
					select m).Distinct<int>().ToList<int>();
				}
				else
				{
					cidsThatCannotBeUnchecked = (from g in source
					select g.Data.Field.ControlId).Distinct<int>().ToList<int>();
				}
				CS$<>8__locals2.cidsThatCannotBeUnchecked = cidsThatCannotBeUnchecked;
				List<AccommodationDataDTO> source2 = (from g in source
				where CS$<>8__locals1.cidsThatCannotBeUnchecked.Contains(g.Data.Field.ControlId)
				select g).ToList<AccommodationDataDTO>();
				List<AccommodationDataDTO> list = (from g in source2
				where CS$<>8__locals1.checkedItems.All((SelfRegCheckedAccommodationDTO h) => h.ControlId != g.Data.Field.ControlId)
				select g).ToList<AccommodationDataDTO>();
				bool flag4 = list.Count < 1;
				if (flag4)
				{
					checkedItems = CS$<>8__locals1.checkedItems;
				}
				else
				{
					foreach (AccommodationDataDTO accommodationDataDTO in list)
					{
						CS$<>8__locals1.checkedItems.Add(new SelfRegCheckedAccommodationDTO
						{
							ControlId = accommodationDataDTO.Data.Field.ControlId,
							Text = accommodationDataDTO.GetDisplayString(),
							IsChecked = true
						});
					}
					checkedItems = CS$<>8__locals1.checkedItems;
				}
			}
			return checkedItems;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00004C34 File Offset: 0x00002E34
		private static int TryParseInt(string str)
		{
			string text = (str ?? "").Trim();
			bool flag = text.Length < 1;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int num;
				int.TryParse(text, out num);
				result = num;
			}
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004C74 File Offset: 0x00002E74
		private void SaveAndClose()
		{
			string text = WebClientUtilityWebClientManager.CurrentInstance.GetCurrentFullUrl();
			int length = text.ToLower().IndexOf("/user/");
			text = text.Substring(0, length);
			bool flag = text[text.Length - 1] == '/';
			if (flag)
			{
				text = text.Substring(0, text.Length - 1);
			}
			int pid = this.Pid;
			string studentPersonIdEncodedForUrl = NavigatorClientManager.CurrentInstance.ConvertIntParameterToLongtermUrlString(this.Pid);
			string ipAddressForLoggin = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
			IList<SelfRegCourseInfoDTO> selectedLucidsWithCourseDescriptions = this.GetSelectedLucidsWithCourseDescriptions();
			bool flag2 = selectedLucidsWithCourseDescriptions.Count < 1;
			if (flag2)
			{
				CWLogger.Logger.Trace("ctrlaccommodationrequestgroup:SaveAndClose:NoLucidsSelected:pid={0}", pid);
				HttpContext.Current.Response.Write("<p class='alert alert-danger' role='alert'>Please select at least one course in order to continue...</p>");
			}
			else
			{
				eSelfRegCoursesAccommodationsStatus selectedCoursesAccommodationsStatus = this.GetSelectedCoursesAccommodationsStatus();
				bool flag3 = selectedCoursesAccommodationsStatus == eSelfRegCoursesAccommodationsStatus.Unknown;
				if (flag3)
				{
					CWLogger.Logger.Trace("ctrlaccommodationrequestgroup:SaveAndClose:NoAccommodationsStatusSelected:pid={0}", pid);
					HttpContext.Current.Response.Write("<p class='alert alert-danger' role='alert'>Please select at least one option indicating if your accommodations require any changes in order to continue...</p>");
				}
				else
				{
					ISelfRegClientManager selfRegClientManager = new SelfRegClientManager();
					selfRegClientManager.ProcessSelfRegRequest(pid, selectedCoursesAccommodationsStatus, selectedLucidsWithCourseDescriptions, this.GetAccommodations().ToList<SelfRegCheckedAccommodationDTO>(), this._hidingAccommodations, this.txt_note.Text.Trim(), text, studentPersonIdEncodedForUrl, ipAddressForLoggin);
					this.FireButtonSubmitClicked();
					SessionCaching.CurrentInstance.Clear("CtrlAccommodationRequestGroup_courses");
				}
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004DD4 File Offset: 0x00002FD4
		private void BuildControlHeiarchy()
		{
			this.Controls.Add(this.validationSummary);
			this.Controls.Add(this.chks_courses_validator);
			this.Controls.Add(this.chks_courses);
			this.Controls.Add(this.lb_accommodations);
			this.Controls.Add(this.chks_accommodations);
			this.Controls.Add(this.chks_accommodations_validator);
			this.Controls.Add(this.rbtns_accommodationsApproval_validator);
			this.Controls.Add(this.rbtns_accommodationsApproval);
			this.Controls.Add(this.chk_iagree_validator);
			this.Controls.Add(this.chk_iagree);
			this.Controls.Add(this.btn_Submit);
			this.Controls.Add(this.btn_Cancel);
			this.Controls.Add(this.lbl_intro);
			this.Controls.Add(this.lbl_confidentialityAgreement);
			this.Controls.Add(this.txt_confidentialityAgreement);
			this.Controls.Add(this.txt_note);
		}

		// Token: 0x0400000E RID: 14
		private Label lbl_intro = new Label();

		// Token: 0x0400000F RID: 15
		private ListBox lb_accommodations = new ListBox();

		// Token: 0x04000010 RID: 16
		private CheckBoxList chks_accommodations = new CheckBoxList();

		// Token: 0x04000011 RID: 17
		private HtmlButton btnCheckAll = new HtmlButton();

		// Token: 0x04000012 RID: 18
		private HtmlButton btnCheckNone = new HtmlButton();

		// Token: 0x04000013 RID: 19
		private RadioButtonList rbtns_accommodationsApproval = new RadioButtonList();

		// Token: 0x04000014 RID: 20
		private TextBox txt_note = new TextBox();

		// Token: 0x04000015 RID: 21
		private CheckBox chk_iagree = new CheckBox();

		// Token: 0x04000016 RID: 22
		private CheckBoxList chks_courses = new CheckBoxList();

		// Token: 0x04000017 RID: 23
		private ValidationSummary validationSummary = new ValidationSummary();

		// Token: 0x04000018 RID: 24
		private CheckBoxValidator chk_iagree_validator = new CheckBoxValidator();

		// Token: 0x04000019 RID: 25
		private RequiredFieldValidator rbtns_accommodationsApproval_validator = new RequiredFieldValidator();

		// Token: 0x0400001A RID: 26
		private CheckBoxListValidator chks_courses_validator = new CheckBoxListValidator();

		// Token: 0x0400001B RID: 27
		private CheckBoxListValidator chks_accommodations_validator = new CheckBoxListValidator();

		// Token: 0x0400001C RID: 28
		private Button btn_Submit = new Button();

		// Token: 0x0400001D RID: 29
		private Button btn_Cancel = new Button();

		// Token: 0x0400001E RID: 30
		private Label lbl_confidentialityAgreement = new Label();

		// Token: 0x0400001F RID: 31
		private TextBox txt_confidentialityAgreement = new TextBox();

		// Token: 0x04000023 RID: 35
		private string _agreementHtml = null;

		// Token: 0x04000024 RID: 36
		private List<AccommodationDataDTO> _hidingAccommodations;

		// Token: 0x04000025 RID: 37
		private bool _showTextNote = true;
	}
}
