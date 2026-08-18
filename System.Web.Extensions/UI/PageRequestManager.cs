using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;
using System.Web.Configuration;
using System.Web.Resources;
using System.Web.UI.HtmlControls;

namespace System.Web.UI
{
	// Token: 0x0200005E RID: 94
	internal sealed class PageRequestManager
	{
		// Token: 0x06000343 RID: 835 RVA: 0x0001209B File Offset: 0x0001029B
		public PageRequestManager(ScriptManager owner)
		{
			this._owner = owner;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000344 RID: 836 RVA: 0x000120AA File Offset: 0x000102AA
		public string AsyncPostBackSourceElementID
		{
			get
			{
				if (this._asyncPostBackSourceElementID == null)
				{
					return string.Empty;
				}
				return this._asyncPostBackSourceElementID;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000345 RID: 837 RVA: 0x000120C0 File Offset: 0x000102C0
		private bool ClientSupportsFocus
		{
			get
			{
				HttpBrowserCapabilitiesBase browser = this._owner.IPage.Request.Browser;
				return browser.EcmaScriptVersion >= PageRequestManager.FocusMinimumEcmaVersion || browser.JScriptVersion >= PageRequestManager.FocusMinimumJScriptVersion;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00012107 File Offset: 0x00010307
		private bool EnableLegacyRendering
		{
			get
			{
				return this._owner.EnableLegacyRendering;
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00012114 File Offset: 0x00010314
		[SecuritySafeCritical]
		private bool CustomErrorsSectionHasRedirect(int httpCode)
		{
			bool flag = this._owner.CustomErrorsSection.DefaultRedirect != null;
			if (!flag && this._owner.CustomErrorsSection.Errors != null)
			{
				foreach (object obj in this._owner.CustomErrorsSection.Errors)
				{
					CustomError customError = (CustomError)obj;
					if (customError.StatusCode == httpCode)
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000121A8 File Offset: 0x000103A8
		internal static void EncodeString(TextWriter writer, string type, string id, string content)
		{
			if (id == null)
			{
				id = string.Empty;
			}
			if (content == null)
			{
				content = string.Empty;
			}
			writer.Write(content.Length.ToString(CultureInfo.InvariantCulture));
			writer.Write('|');
			writer.Write(type);
			writer.Write('|');
			writer.Write(id);
			writer.Write('|');
			writer.Write(content);
			writer.Write('|');
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00012217 File Offset: 0x00010417
		private string GetAllUpdatePanelIDs()
		{
			return PageRequestManager.GetUpdatePanelIDsFromList(this._allUpdatePanels, PageRequestManager.IDType.Both, true);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00012226 File Offset: 0x00010426
		private string GetAsyncPostBackControlIDs(bool includeQuotes)
		{
			return PageRequestManager.GetControlIDsFromList(this._asyncPostBackControls, includeQuotes);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00012234 File Offset: 0x00010434
		private string GetChildUpdatePanelIDs()
		{
			return PageRequestManager.GetUpdatePanelIDsFromList(this._childUpdatePanelsToRefresh, PageRequestManager.IDType.UniqueID, false);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00012244 File Offset: 0x00010444
		private static string GetControlIDsFromList(List<Control> list, bool includeQuotes)
		{
			if (list != null && list.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				for (int i = 0; i < list.Count; i++)
				{
					Control control = list[i];
					if (control.Visible)
					{
						if (!flag)
						{
							stringBuilder.Append(',');
						}
						flag = false;
						if (includeQuotes)
						{
							stringBuilder.Append('\'');
						}
						stringBuilder.Append(control.UniqueID);
						if (includeQuotes)
						{
							stringBuilder.Append('\'');
						}
						if (control.EffectiveClientIDMode == ClientIDMode.AutoID)
						{
							if (includeQuotes)
							{
								stringBuilder.Append(",''");
							}
							else
							{
								stringBuilder.Append(',');
							}
						}
						else if (includeQuotes)
						{
							stringBuilder.Append(",'");
							stringBuilder.Append(control.ClientID);
							stringBuilder.Append('\'');
						}
						else
						{
							stringBuilder.Append(',');
							stringBuilder.Append(control.ClientID);
						}
					}
				}
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00012338 File Offset: 0x00010538
		private static Exception GetControlRegistrationException(Control control)
		{
			if (control == null)
			{
				return new ArgumentNullException("control");
			}
			if (!(control is INamingContainer) && !(control is IPostBackDataHandler) && !(control is IPostBackEventHandler))
			{
				return new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.ScriptManager_InvalidControlRegistration, new object[]
				{
					control.ID
				}));
			}
			return null;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00012390 File Offset: 0x00010590
		private static int GetHttpCodeForException(Exception e)
		{
			HttpException ex = e as HttpException;
			if (ex != null)
			{
				return ex.GetHttpCode();
			}
			if (e is UnauthorizedAccessException)
			{
				return 401;
			}
			if (e is PathTooLongException)
			{
				return 414;
			}
			if (e.InnerException != null)
			{
				return PageRequestManager.GetHttpCodeForException(e.InnerException);
			}
			return 500;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000123E4 File Offset: 0x000105E4
		private static string GetMasterPageUniqueID(Page page)
		{
			MasterPage master = page.Master;
			if (master != null)
			{
				while (master.Master != null)
				{
					master = master.Master;
				}
				return master.UniqueID;
			}
			return string.Empty;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00012418 File Offset: 0x00010618
		private string GetPostBackControlIDs(bool includeQuotes)
		{
			return PageRequestManager.GetControlIDsFromList(this._postBackControls, includeQuotes);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00012426 File Offset: 0x00010626
		private string GetRefreshingUpdatePanelIDs()
		{
			return PageRequestManager.GetUpdatePanelIDsFromList(this._updatePanelsToRefresh, PageRequestManager.IDType.Both, false);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00012438 File Offset: 0x00010638
		private static string GetUpdatePanelIDsFromList(List<UpdatePanel> list, PageRequestManager.IDType idType, bool includeChildrenAsTriggersPrefix)
		{
			if (list != null && list.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				for (int i = 0; i < list.Count; i++)
				{
					UpdatePanel updatePanel = list[i];
					if (updatePanel.Visible)
					{
						if (!flag)
						{
							stringBuilder.Append(',');
						}
						flag = false;
						if (includeChildrenAsTriggersPrefix)
						{
							stringBuilder.Append(updatePanel.ChildrenAsTriggers ? 't' : 'f');
						}
						stringBuilder.Append(updatePanel.UniqueID);
						if (idType == PageRequestManager.IDType.Both)
						{
							stringBuilder.Append(',');
							if (updatePanel.EffectiveClientIDMode != ClientIDMode.AutoID)
							{
								stringBuilder.Append(updatePanel.ClientID);
							}
						}
					}
				}
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000124E4 File Offset: 0x000106E4
		internal static bool IsAsyncPostBackRequest(HttpRequestBase request)
		{
			string[] values = request.Headers.GetValues("X-MicrosoftAjax");
			if (values != null)
			{
				for (int i = 0; i < values.Length; i++)
				{
					string[] array = values[i].Split(new char[]
					{
						','
					});
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].Trim() == "Delta=true")
						{
							return true;
						}
					}
				}
			}
			string text = request.Form["__ASYNCPOST"];
			return !string.IsNullOrEmpty(text) && text.Trim() == "true";
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001257C File Offset: 0x0001077C
		internal void LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[postDataKey];
			if (text != null)
			{
				int num = text.IndexOf('|');
				string text2;
				if (num != -1)
				{
					text2 = text.Substring(0, num);
					this._asyncPostBackSourceElementID = text.Substring(num + 1);
				}
				else
				{
					text2 = text;
					this._asyncPostBackSourceElementID = string.Empty;
				}
				if (text2 != this._owner.UniqueID)
				{
					if (text2.IndexOf(',') != -1)
					{
						this._updatePanelRequiresUpdate = null;
						this._updatePanelsRequireUpdate = text2.Split(new char[]
						{
							','
						});
					}
					else
					{
						this._updatePanelRequiresUpdate = text2;
						this._updatePanelsRequireUpdate = null;
					}
				}
			}
			if (this._allUpdatePanels != null && this._allUpdatePanels.Count != 0)
			{
				foreach (UpdatePanel updatePanel in this._allUpdatePanels)
				{
					updatePanel.Initialize();
				}
			}
			this._panelsInitialized = true;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0001267C File Offset: 0x0001087C
		internal void OnInit()
		{
			if (this._owner.EnablePartialRendering && !this._owner._supportsPartialRenderingSetByUser)
			{
				HttpBrowserCapabilitiesBase browser = this._owner.IPage.Request.Browser;
				bool flag = browser.W3CDomVersion >= PageRequestManager.MinimumW3CDomVersion && browser.EcmaScriptVersion >= PageRequestManager.MinimumEcmaScriptVersion && browser.SupportsCallback;
				if (flag)
				{
					flag = !this.EnableLegacyRendering;
				}
				this._owner.SupportsPartialRendering = flag;
			}
			if (this._owner.IsInAsyncPostBack)
			{
				this._owner.IPage.Error += this.OnPageError;
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001272C File Offset: 0x0001092C
		private void OnPageError(object sender, EventArgs e)
		{
			Exception lastError = this._owner.IPage.Server.GetLastError();
			this._owner.OnAsyncPostBackError(new AsyncPostBackErrorEventArgs(lastError));
			string value = this._owner.AsyncPostBackErrorMessage;
			if (string.IsNullOrEmpty(value) && !this._owner.Control.Context.IsCustomErrorEnabled)
			{
				value = lastError.Message;
			}
			int httpCodeForException = PageRequestManager.GetHttpCodeForException(lastError);
			bool flag = false;
			if (this._owner.AllowCustomErrorsRedirect && this._owner.Control.Context.IsCustomErrorEnabled)
			{
				if (!this.CustomErrorsSectionHasRedirect(httpCodeForException))
				{
					flag = true;
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				IDictionary items = this._owner.Control.Context.Items;
				items["System.Web.UI.PageRequestManager:AsyncPostBackError"] = true;
				items["System.Web.UI.PageRequestManager:AsyncPostBackErrorMessage"] = value;
				items["System.Web.UI.PageRequestManager:AsyncPostBackErrorHttpCode"] = httpCodeForException;
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0001281D File Offset: 0x00010A1D
		internal void OnPreRender()
		{
			this._owner.IPage.SetRenderMethodDelegate(new RenderMethod(this.RenderPageCallback));
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001283C File Offset: 0x00010A3C
		private void ProcessFocus(HtmlTextWriter writer)
		{
			if (this._requireFocusScript)
			{
				string text = string.Empty;
				if (!string.IsNullOrEmpty(this._focusedControlID))
				{
					text = this._focusedControlID;
				}
				else if (this._focusedControl != null && this._focusedControl.Visible)
				{
					text = this._focusedControl.ClientID;
				}
				if (text.Length > 0)
				{
					string scriptResourceUrl = this._owner.GetScriptResourceUrl("Focus.js", typeof(HtmlForm).Assembly);
					PageRequestManager.EncodeString(writer, "scriptBlock", "ScriptPath", scriptResourceUrl);
					PageRequestManager.EncodeString(writer, "focus", string.Empty, text);
				}
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000128DC File Offset: 0x00010ADC
		private void ProcessScriptRegistration(HtmlTextWriter writer)
		{
			this._owner.ScriptRegistration.RenderActiveArrayDeclarations(this._updatePanelsToRefresh, writer);
			this._owner.ScriptRegistration.RenderActiveScripts(this._updatePanelsToRefresh, writer);
			this._owner.ScriptRegistration.RenderActiveSubmitStatements(this._updatePanelsToRefresh, writer);
			this._owner.ScriptRegistration.RenderActiveExpandos(this._updatePanelsToRefresh, writer);
			this._owner.ScriptRegistration.RenderActiveHiddenFields(this._updatePanelsToRefresh, writer);
			this._owner.ScriptRegistration.RenderActiveScriptDisposes(this._updatePanelsToRefresh, writer);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00012974 File Offset: 0x00010B74
		private void ProcessUpdatePanels()
		{
			if (this._allUpdatePanels != null)
			{
				this._updatePanelsToRefresh = new List<UpdatePanel>(this._allUpdatePanels.Count);
				this._childUpdatePanelsToRefresh = new List<UpdatePanel>(this._allUpdatePanels.Count);
				HtmlForm form = this._owner.Page.Form;
				for (int i = 0; i < this._allUpdatePanels.Count; i++)
				{
					UpdatePanel updatePanel = this._allUpdatePanels[i];
					bool flag = updatePanel.RequiresUpdate || (this._updatePanelRequiresUpdate != null && string.Equals(updatePanel.UniqueID, this._updatePanelRequiresUpdate, StringComparison.Ordinal)) || (this._updatePanelsRequireUpdate != null && Array.IndexOf<string>(this._updatePanelsRequireUpdate, updatePanel.UniqueID) != -1);
					Control parent = updatePanel.Parent;
					while (parent != form)
					{
						UpdatePanel updatePanel2 = parent as UpdatePanel;
						if (updatePanel2 != null && (this._updatePanelsToRefresh.Contains(updatePanel2) || this._childUpdatePanelsToRefresh.Contains(updatePanel2)))
						{
							flag = false;
							this._childUpdatePanelsToRefresh.Add(updatePanel);
							break;
						}
						parent = parent.Parent;
						if (parent == null)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						updatePanel.SetAsyncPostBackMode(true);
						this._updatePanelsToRefresh.Add(updatePanel);
					}
					else
					{
						updatePanel.SetAsyncPostBackMode(false);
					}
				}
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00012AB8 File Offset: 0x00010CB8
		public void RegisterAsyncPostBackControl(Control control)
		{
			Exception controlRegistrationException = PageRequestManager.GetControlRegistrationException(control);
			if (controlRegistrationException != null)
			{
				throw controlRegistrationException;
			}
			if (this._postBackControls != null && this._postBackControls.Contains(control))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.ScriptManager_CannotRegisterBothPostBacks, new object[]
				{
					control.ID
				}));
			}
			if (this._asyncPostBackControls == null)
			{
				this._asyncPostBackControls = new List<Control>();
			}
			if (!this._asyncPostBackControls.Contains(control))
			{
				this._asyncPostBackControls.Add(control);
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00012B38 File Offset: 0x00010D38
		public void RegisterDataItem(Control control, string dataItem, bool isJsonSerialized)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!this._owner.IsInAsyncPostBack)
			{
				throw new InvalidOperationException(AtlasWeb.PageRequestManager_RegisterDataItemInNonAsyncRequest);
			}
			if (this._scriptDataItems == null)
			{
				this._scriptDataItems = new PageRequestManager.ScriptDataItemCollection();
			}
			else if (this._scriptDataItems.ContainsControl(control))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.PageRequestManager_RegisterDataItemTwice, new object[]
				{
					control.ID
				}), "control");
			}
			this._scriptDataItems.Add(new PageRequestManager.ScriptDataItem(control, dataItem, isJsonSerialized));
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00012BCA File Offset: 0x00010DCA
		private void RegisterFocusScript()
		{
			if (this.ClientSupportsFocus && !this._requireFocusScript)
			{
				this._requireFocusScript = true;
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00012BE4 File Offset: 0x00010DE4
		public void RegisterPostBackControl(Control control)
		{
			Exception controlRegistrationException = PageRequestManager.GetControlRegistrationException(control);
			if (controlRegistrationException != null)
			{
				throw controlRegistrationException;
			}
			if (this._asyncPostBackControls != null && this._asyncPostBackControls.Contains(control))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.ScriptManager_CannotRegisterBothPostBacks, new object[]
				{
					control.ID
				}));
			}
			if (this._postBackControls == null)
			{
				this._postBackControls = new List<Control>();
			}
			if (!this._postBackControls.Contains(control))
			{
				this._postBackControls.Add(control);
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00012C64 File Offset: 0x00010E64
		internal void RegisterUpdatePanel(UpdatePanel updatePanel)
		{
			if (this._allUpdatePanels == null)
			{
				this._allUpdatePanels = new List<UpdatePanel>();
			}
			this._allUpdatePanels.Add(updatePanel);
			if (this._panelsInitialized)
			{
				updatePanel.Initialize();
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00012C93 File Offset: 0x00010E93
		internal void Render(HtmlTextWriter writer)
		{
			this._owner.IPage.VerifyRenderingInServerForm(this._owner);
			this.RenderPageRequestManagerScript(writer);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00012CB4 File Offset: 0x00010EB4
		private void RenderFormCallback(HtmlTextWriter writer, Control containerControl)
		{
			if (this._updatePanelsToRefresh != null)
			{
				foreach (UpdatePanel updatePanel in this._updatePanelsToRefresh)
				{
					if (updatePanel.Visible)
					{
						updatePanel.RenderControl(this._updatePanelWriter);
					}
				}
			}
			IPage ipage = this._owner.IPage;
			if (ipage.EnableEventValidation)
			{
				TextWriter writer2 = null;
				bool flag = false;
				try
				{
					writer2 = ipage.Response.SwitchWriter(TextWriter.Null);
					flag = true;
					HtmlTextWriter writer3 = new HtmlTextWriter(TextWriter.Null);
					foreach (object obj in containerControl.Controls)
					{
						Control control = (Control)obj;
						control.RenderControl(writer3);
					}
				}
				finally
				{
					if (flag)
					{
						ipage.Response.SwitchWriter(writer2);
					}
				}
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00012DC8 File Offset: 0x00010FC8
		private void RenderPageCallback(HtmlTextWriter writer, Control pageControl)
		{
			this.ProcessUpdatePanels();
			HttpResponseBase response = this._owner.IPage.Response;
			response.ContentType = "text/plain";
			response.Cache.SetNoServerCaching();
			PageRequestManager.EncodeString(writer, "#", string.Empty, "4");
			IHtmlForm form = this._owner.IPage.Form;
			form.SetRenderMethodDelegate(new RenderMethod(this.RenderFormCallback));
			this._updatePanelWriter = writer;
			PageRequestManager.ParserHtmlTextWriter parserHtmlTextWriter = new PageRequestManager.ParserHtmlTextWriter();
			form.RenderControl(parserHtmlTextWriter);
			IDictionary<string, string> hiddenFieldsToRender = this._owner.IPage.HiddenFieldsToRender;
			if (hiddenFieldsToRender != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in hiddenFieldsToRender)
				{
					if (ControlUtil.IsBuiltInHiddenField(keyValuePair.Key))
					{
						PageRequestManager.EncodeString(writer, "hiddenField", keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			PageRequestManager.EncodeString(writer, "asyncPostBackControlIDs", string.Empty, this.GetAsyncPostBackControlIDs(false));
			PageRequestManager.EncodeString(writer, "postBackControlIDs", string.Empty, this.GetPostBackControlIDs(false));
			PageRequestManager.EncodeString(writer, "updatePanelIDs", string.Empty, this.GetAllUpdatePanelIDs());
			PageRequestManager.EncodeString(writer, "childUpdatePanelIDs", string.Empty, this.GetChildUpdatePanelIDs());
			PageRequestManager.EncodeString(writer, "panelsToRefreshIDs", string.Empty, this.GetRefreshingUpdatePanelIDs());
			PageRequestManager.EncodeString(writer, "asyncPostBackTimeout", string.Empty, this._owner.AsyncPostBackTimeout.ToString(CultureInfo.InvariantCulture));
			if (parserHtmlTextWriter.FormAction != null)
			{
				PageRequestManager.EncodeString(writer, "formAction", string.Empty, parserHtmlTextWriter.FormAction);
			}
			if (this._owner.IPage.Header != null)
			{
				string title = this._owner.IPage.Title;
				if (!string.IsNullOrEmpty(title))
				{
					PageRequestManager.EncodeString(writer, "pageTitle", string.Empty, title);
				}
			}
			this.RenderDataItems(writer);
			this.ProcessScriptRegistration(writer);
			this.ProcessFocus(writer);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00012FD0 File Offset: 0x000111D0
		private void RenderDataItems(HtmlTextWriter writer)
		{
			if (this._scriptDataItems != null)
			{
				foreach (PageRequestManager.ScriptDataItem scriptDataItem in this._scriptDataItems)
				{
					PageRequestManager.EncodeString(writer, scriptDataItem.IsJsonSerialized ? "dataItemJson" : "dataItem", scriptDataItem.Control.ClientID, scriptDataItem.DataItem);
				}
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00013050 File Offset: 0x00011250
		internal void RenderPageRequestManagerScript(HtmlTextWriter writer)
		{
			writer.Write("<script type=\"text/javascript\">\r\n//<![CDATA[\r\nSys.WebForms.PageRequestManager._initialize('");
			writer.Write(this._owner.UniqueID);
			writer.Write("', '");
			writer.Write(this._owner.IPage.Form.ClientID);
			writer.Write("', [");
			PageRequestManager.RenderUpdatePanelIDsFromList(writer, this._allUpdatePanels);
			writer.Write("], [");
			writer.Write(this.GetAsyncPostBackControlIDs(true));
			writer.Write("], [");
			writer.Write(this.GetPostBackControlIDs(true));
			writer.Write("], ");
			writer.Write(this._owner.AsyncPostBackTimeout.ToString(CultureInfo.InvariantCulture));
			writer.Write(", '");
			writer.Write(PageRequestManager.GetMasterPageUniqueID(this._owner.Page));
			writer.WriteLine("');");
			writer.Write("//]]>\r\n</script>\r\n");
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00013148 File Offset: 0x00011348
		private static void RenderUpdatePanelIDsFromList(HtmlTextWriter writer, List<UpdatePanel> list)
		{
			if (list != null && list.Count > 0)
			{
				bool flag = true;
				for (int i = 0; i < list.Count; i++)
				{
					UpdatePanel updatePanel = list[i];
					if (updatePanel.Visible)
					{
						if (!flag)
						{
							writer.Write(',');
						}
						flag = false;
						writer.Write("'");
						writer.Write(updatePanel.ChildrenAsTriggers ? 't' : 'f');
						writer.Write(updatePanel.UniqueID);
						writer.Write("',");
						if (updatePanel.EffectiveClientIDMode == ClientIDMode.AutoID)
						{
							writer.Write("''");
						}
						else
						{
							writer.Write("'");
							writer.Write(updatePanel.ClientID);
							writer.Write("'");
						}
					}
				}
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001320B File Offset: 0x0001140B
		public void SetFocus(Control control)
		{
			this._owner.IPage.SetFocus(control);
			if (this._owner.IsInAsyncPostBack)
			{
				this._focusedControl = control;
				this._focusedControlID = null;
				this.RegisterFocusScript();
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001323F File Offset: 0x0001143F
		public void SetFocus(string clientID)
		{
			this._owner.IPage.SetFocus(clientID);
			this.SetFocusInternal(clientID);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00013259 File Offset: 0x00011459
		internal void SetFocusInternal(string clientID)
		{
			if (this._owner.IsInAsyncPostBack)
			{
				this._focusedControlID = clientID.Trim();
				this._focusedControl = null;
				this.RegisterFocusScript();
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00013284 File Offset: 0x00011484
		internal void UnregisterUpdatePanel(UpdatePanel updatePanel)
		{
			if (this._allUpdatePanels == null || !this._allUpdatePanels.Contains(updatePanel))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.ScriptManager_UpdatePanelNotRegistered, new object[]
				{
					updatePanel.ID
				}), "updatePanel");
			}
			this._allUpdatePanels.Remove(updatePanel);
		}

		// Token: 0x0400011F RID: 287
		internal const string UpdatePanelVersionToken = "#";

		// Token: 0x04000120 RID: 288
		internal const string UpdatePanelVersionNumber = "4";

		// Token: 0x04000121 RID: 289
		internal const string PageRedirectToken = "pageRedirect";

		// Token: 0x04000122 RID: 290
		internal const string HiddenFieldToken = "hiddenField";

		// Token: 0x04000123 RID: 291
		private const string AsyncPostBackControlIDsToken = "asyncPostBackControlIDs";

		// Token: 0x04000124 RID: 292
		private const string PostBackControlIDsToken = "postBackControlIDs";

		// Token: 0x04000125 RID: 293
		private const string UpdatePanelIDsToken = "updatePanelIDs";

		// Token: 0x04000126 RID: 294
		private const string AsyncPostBackTimeoutToken = "asyncPostBackTimeout";

		// Token: 0x04000127 RID: 295
		private const string ChildUpdatePanelIDsToken = "childUpdatePanelIDs";

		// Token: 0x04000128 RID: 296
		private const string UpdatePanelsToRefreshToken = "panelsToRefreshIDs";

		// Token: 0x04000129 RID: 297
		private const string FormActionToken = "formAction";

		// Token: 0x0400012A RID: 298
		private const string DataItemToken = "dataItem";

		// Token: 0x0400012B RID: 299
		private const string DataItemJsonToken = "dataItemJson";

		// Token: 0x0400012C RID: 300
		internal const string ArrayDeclarationToken = "arrayDeclaration";

		// Token: 0x0400012D RID: 301
		internal const string ExpandoToken = "expando";

		// Token: 0x0400012E RID: 302
		internal const string OnSubmitToken = "onSubmit";

		// Token: 0x0400012F RID: 303
		internal const string ScriptBlockToken = "scriptBlock";

		// Token: 0x04000130 RID: 304
		internal const string ScriptStartupBlockToken = "scriptStartupBlock";

		// Token: 0x04000131 RID: 305
		internal const string ScriptDisposeToken = "scriptDispose";

		// Token: 0x04000132 RID: 306
		internal const string ErrorToken = "error";

		// Token: 0x04000133 RID: 307
		internal const string AsyncPostBackErrorKey = "System.Web.UI.PageRequestManager:AsyncPostBackError";

		// Token: 0x04000134 RID: 308
		internal const string AsyncPostBackErrorMessageKey = "System.Web.UI.PageRequestManager:AsyncPostBackErrorMessage";

		// Token: 0x04000135 RID: 309
		internal const string AsyncPostBackErrorHttpCodeKey = "System.Web.UI.PageRequestManager:AsyncPostBackErrorHttpCode";

		// Token: 0x04000136 RID: 310
		internal const string AsyncPostBackRedirectLocationKey = "System.Web.UI.PageRequestManager:AsyncPostBackRedirectLocation";

		// Token: 0x04000137 RID: 311
		private const string PageTitleToken = "pageTitle";

		// Token: 0x04000138 RID: 312
		private const string FocusToken = "focus";

		// Token: 0x04000139 RID: 313
		private const string AsyncPostFormField = "__ASYNCPOST";

		// Token: 0x0400013A RID: 314
		private const char LengthEncodeDelimiter = '|';

		// Token: 0x0400013B RID: 315
		private static readonly Version MinimumW3CDomVersion = new Version(1, 0);

		// Token: 0x0400013C RID: 316
		private static readonly Version MinimumEcmaScriptVersion = new Version(1, 0);

		// Token: 0x0400013D RID: 317
		private ScriptManager _owner;

		// Token: 0x0400013E RID: 318
		private List<UpdatePanel> _allUpdatePanels;

		// Token: 0x0400013F RID: 319
		private List<UpdatePanel> _updatePanelsToRefresh;

		// Token: 0x04000140 RID: 320
		private List<UpdatePanel> _childUpdatePanelsToRefresh;

		// Token: 0x04000141 RID: 321
		private List<Control> _asyncPostBackControls;

		// Token: 0x04000142 RID: 322
		private List<Control> _postBackControls;

		// Token: 0x04000143 RID: 323
		private PageRequestManager.ScriptDataItemCollection _scriptDataItems;

		// Token: 0x04000144 RID: 324
		private string _updatePanelRequiresUpdate;

		// Token: 0x04000145 RID: 325
		private string[] _updatePanelsRequireUpdate;

		// Token: 0x04000146 RID: 326
		private HtmlTextWriter _updatePanelWriter;

		// Token: 0x04000147 RID: 327
		private bool _panelsInitialized;

		// Token: 0x04000148 RID: 328
		private string _asyncPostBackSourceElementID;

		// Token: 0x04000149 RID: 329
		private static readonly Version FocusMinimumEcmaVersion = new Version("1.4");

		// Token: 0x0400014A RID: 330
		private static readonly Version FocusMinimumJScriptVersion = new Version("3.0");

		// Token: 0x0400014B RID: 331
		private string _focusedControlID;

		// Token: 0x0400014C RID: 332
		private Control _focusedControl;

		// Token: 0x0400014D RID: 333
		private bool _requireFocusScript;

		// Token: 0x02000159 RID: 345
		private sealed class ParserHtmlTextWriter : HtmlTextWriter
		{
			// Token: 0x06000FF1 RID: 4081 RVA: 0x00037628 File Offset: 0x00035828
			public ParserHtmlTextWriter() : base(TextWriter.Null)
			{
			}

			// Token: 0x17000588 RID: 1416
			// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x00037635 File Offset: 0x00035835
			public string FormAction
			{
				get
				{
					return this._formAction;
				}
			}

			// Token: 0x06000FF3 RID: 4083 RVA: 0x0003763D File Offset: 0x0003583D
			public override void WriteBeginTag(string tagName)
			{
				base.WriteBeginTag(tagName);
				this._writingForm = (tagName == "form");
			}

			// Token: 0x06000FF4 RID: 4084 RVA: 0x00037657 File Offset: 0x00035857
			public override void WriteAttribute(string name, string value, bool fEncode)
			{
				base.WriteAttribute(name, value, fEncode);
				if (this._writingForm && name == "action")
				{
					this._formAction = value;
				}
			}

			// Token: 0x040004CE RID: 1230
			private bool _writingForm;

			// Token: 0x040004CF RID: 1231
			private string _formAction;
		}

		// Token: 0x0200015A RID: 346
		private sealed class ScriptDataItem
		{
			// Token: 0x06000FF5 RID: 4085 RVA: 0x0003767E File Offset: 0x0003587E
			public ScriptDataItem(Control control, string dataItem, bool isJsonSerialized)
			{
				this._control = control;
				this._dataItem = ((dataItem == null) ? string.Empty : dataItem);
				this._isJsonSerialized = isJsonSerialized;
			}

			// Token: 0x17000589 RID: 1417
			// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x000376A5 File Offset: 0x000358A5
			public Control Control
			{
				get
				{
					return this._control;
				}
			}

			// Token: 0x1700058A RID: 1418
			// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x000376AD File Offset: 0x000358AD
			public string DataItem
			{
				get
				{
					return this._dataItem;
				}
			}

			// Token: 0x1700058B RID: 1419
			// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x000376B5 File Offset: 0x000358B5
			public bool IsJsonSerialized
			{
				get
				{
					return this._isJsonSerialized;
				}
			}

			// Token: 0x040004D0 RID: 1232
			private Control _control;

			// Token: 0x040004D1 RID: 1233
			private string _dataItem;

			// Token: 0x040004D2 RID: 1234
			private bool _isJsonSerialized;
		}

		// Token: 0x0200015B RID: 347
		private sealed class ScriptDataItemCollection : List<PageRequestManager.ScriptDataItem>
		{
			// Token: 0x06000FF9 RID: 4089 RVA: 0x000376C0 File Offset: 0x000358C0
			public bool ContainsControl(Control control)
			{
				foreach (PageRequestManager.ScriptDataItem scriptDataItem in this)
				{
					if (scriptDataItem.Control == control)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x0200015C RID: 348
		private enum IDType
		{
			// Token: 0x040004D4 RID: 1236
			UniqueID,
			// Token: 0x040004D5 RID: 1237
			Both
		}
	}
}
