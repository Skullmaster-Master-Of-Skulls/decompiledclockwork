using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Xml.Serialization;
using Telerik.Licensing;
using Telerik.Web.UI.Wizard.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000996 RID: 2454
	[RequiredScript(typeof(jQueryPlugins), 1)]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredScript(typeof(MaterialRipple))]
	[ClientScriptResource("Telerik.Web.UI.RadWizard", "Telerik.Web.UI.Wizard.RadWizardScripts.js")]
	[XmlRoot("WizardSteps")]
	[TelerikToolboxCategory("Container")]
	[Designer("Telerik.Web.Design.RadWizardDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxBitmap(typeof(RadWizard), "Telerik.Web.UI.Wizard.png")]
	[ToolboxData("<{0}:RadWizard Runat=\"server\"></{0}:RadWizard>")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RadWizard))]
	[LightweightRendering]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Mobile, typeof(RadButton))]
	[AdaptiveRendering]
	[EmbeddedSkin("Wizard", typeof(RadWizard))]
	[EmbeddedSkin("Wizard", "Default", typeof(RadWizard))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadWizard))]
	public class RadWizard : RadWebControl, IPostBackEventHandler, ILocalizableControl
	{
		// Token: 0x17001EC5 RID: 7877
		// (get) Token: 0x06005D4E RID: 23886 RVA: 0x0011CA62 File Offset: 0x0011AC62
		internal bool IsControlEnabled
		{
			get
			{
				return base.IsEnabled;
			}
		}

		// Token: 0x17001EC6 RID: 7878
		// (get) Token: 0x06005D4F RID: 23887 RVA: 0x0011CA6A File Offset: 0x0011AC6A
		internal bool IsDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x06005D50 RID: 23888 RVA: 0x0011CA72 File Offset: 0x0011AC72
		protected override ControlCollection CreateControlCollection()
		{
			return new RadWizardStepCollection(this);
		}

		// Token: 0x06005D51 RID: 23889 RVA: 0x0011CA7A File Offset: 0x0011AC7A
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Page.RegisterRequiresControlState(this);
		}

		// Token: 0x06005D52 RID: 23890 RVA: 0x0011CA8F File Offset: 0x0011AC8F
		protected override IRenderer CreateControlRenderer()
		{
			return RendererFactory.CreateWizardRenderer(this);
		}

		// Token: 0x17001EC7 RID: 7879
		// (get) Token: 0x06005D53 RID: 23891 RVA: 0x0011CA97 File Offset: 0x0011AC97
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001EC8 RID: 7880
		// (get) Token: 0x06005D54 RID: 23892 RVA: 0x0011CA9A File Offset: 0x0011AC9A
		internal Stack<int> History
		{
			get
			{
				if (this._historyStack == null)
				{
					this._historyStack = new Stack<int>();
				}
				return this._historyStack;
			}
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x0011CAB5 File Offset: 0x0011ACB5
		internal void PushHistoryIndex(int index)
		{
			if (this._historyStack == null || this._historyStack.Count == 0 || this._historyStack.Peek() != index)
			{
				this.History.Push(index);
			}
		}

		// Token: 0x06005D56 RID: 23894 RVA: 0x0011CAE8 File Offset: 0x0011ACE8
		internal RadWizardStepType GetStepType(int index)
		{
			if (index > -1 && index < this.WizardSteps.Count)
			{
				RadWizardStep wizardStep = this.WizardSteps[index];
				return this.GetStepType(wizardStep, index);
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06005D57 RID: 23895 RVA: 0x0011CB24 File Offset: 0x0011AD24
		internal RadWizardStepType GetStepType(RadWizardStep step)
		{
			int index = this.WizardSteps.IndexOf(step);
			return this.GetStepType(step, index);
		}

		// Token: 0x06005D58 RID: 23896 RVA: 0x0011CB48 File Offset: 0x0011AD48
		internal RadWizardStepType GetStepType(RadWizardStep wizardStep, int index)
		{
			if (wizardStep.StepType != RadWizardStepType.Auto)
			{
				return wizardStep.StepType;
			}
			if (this.WizardSteps.Count == 1 || (index < this.WizardSteps.Count - 1 && this.WizardSteps[index + 1].StepType == RadWizardStepType.Complete))
			{
				return RadWizardStepType.Finish;
			}
			if (index == 0)
			{
				return RadWizardStepType.Start;
			}
			if (index == this.WizardSteps.Count - 1)
			{
				return RadWizardStepType.Finish;
			}
			return RadWizardStepType.Step;
		}

		// Token: 0x06005D59 RID: 23897 RVA: 0x0011CBB4 File Offset: 0x0011ADB4
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!this.DisplayNavigationBar)
			{
				if ((this.NavigationBarPosition == RadWizardNavigationBarPosition.Top || this.NavigationBarPosition == RadWizardNavigationBarPosition.Bottom) && (this.ProgressBarPosition == RadWizardProgressBarPosition.Left || this.ProgressBarPosition == RadWizardProgressBarPosition.Right))
				{
					this.NavigationBarPosition = RadWizardNavigationBarPosition.Right;
				}
				if ((this.NavigationBarPosition == RadWizardNavigationBarPosition.Left || this.NavigationBarPosition == RadWizardNavigationBarPosition.Right) && (this.ProgressBarPosition == RadWizardProgressBarPosition.Top || this.ProgressBarPosition == RadWizardProgressBarPosition.Bottom))
				{
					throw new InvalidOperationException(string.Format("RadWizard control does not support a scenario in which NavigationBarPosition='{0}' and ProgressBarPosition='{1}' are set at the same time as in the control with ID='{2}'. NavigationBar and ProgressBar can be placed either on the same side or on opposite sides of the control.", this.NavigationBarPosition, this.ProgressBarPosition, this.ID));
				}
			}
			else
			{
				if ((this.NavigationBarPosition == RadWizardNavigationBarPosition.Top || this.NavigationBarPosition == RadWizardNavigationBarPosition.Bottom) && (this.ProgressBarPosition == RadWizardProgressBarPosition.Left || this.ProgressBarPosition == RadWizardProgressBarPosition.Right))
				{
					throw new InvalidOperationException(string.Format("RadWizard control does not support a scenario in which NavigationBarPosition='{0}' and ProgressBarPosition='{1}' are set at the same time as in the control with ID='{2}'. NavigationBar and ProgressBar can be placed either on the same side or on opposite sides of the control.", this.NavigationBarPosition, this.ProgressBarPosition, this.ID));
				}
				if ((this.NavigationBarPosition == RadWizardNavigationBarPosition.Left || this.NavigationBarPosition == RadWizardNavigationBarPosition.Right) && (this.ProgressBarPosition == RadWizardProgressBarPosition.Top || this.ProgressBarPosition == RadWizardProgressBarPosition.Bottom))
				{
					throw new InvalidOperationException(string.Format("RadWizard control does not support a scenario in which NavigationBarPosition='{0}' and ProgressBarPosition='{1}' are set at the same time as in the control with ID='{2}'. NavigationBar and ProgressBar can be placed either on the same side or on opposite sides of the control.", this.NavigationBarPosition, this.ProgressBarPosition, this.ID));
				}
			}
		}

		// Token: 0x17001EC9 RID: 7881
		// (get) Token: 0x06005D5A RID: 23898 RVA: 0x0011CCED File Offset: 0x0011AEED
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.Renderer.TagKey;
			}
		}

		// Token: 0x17001ECA RID: 7882
		// (get) Token: 0x06005D5B RID: 23899 RVA: 0x0011CCFA File Offset: 0x0011AEFA
		protected override string CssClassFormatString
		{
			get
			{
				return this.Renderer.CssClassFormatString;
			}
		}

		// Token: 0x06005D5C RID: 23900 RVA: 0x0011CD07 File Offset: 0x0011AF07
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			base.RenderBeginTag(writer);
			if (this.WizardSteps.Count == 0)
			{
				return;
			}
			((WizardRendererBase)this.Renderer).RenderBeginTag(writer);
		}

		// Token: 0x06005D5D RID: 23901 RVA: 0x0011CD2F File Offset: 0x0011AF2F
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (this.WizardSteps.Count == 0)
			{
				return;
			}
			((WizardRendererBase)this.Renderer).RenderEndTag(writer);
			base.RenderEndTag(writer);
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x0011CD58 File Offset: 0x0011AF58
		protected override void AddedControl(Control control, int index)
		{
			base.AddedControl(control, index);
			RadWizardStep radWizardStep = control as RadWizardStep;
			if (radWizardStep == null)
			{
				return;
			}
			if (radWizardStep.cashedActive)
			{
				radWizardStep.Active = true;
			}
			if (!this.IsControlEnabled)
			{
				radWizardStep.Enabled = false;
			}
			this.ApplyRenderActiveStep();
			this.OnWizardStepCreated(new WizardStepCreatedEventArgs(radWizardStep));
		}

		// Token: 0x06005D5F RID: 23903 RVA: 0x0011CDA8 File Offset: 0x0011AFA8
		protected override void AddParsedSubObject(object obj)
		{
			RadWizardStep radWizardStep = obj as RadWizardStep;
			if (radWizardStep != null)
			{
				this.WizardSteps.Add(radWizardStep);
			}
		}

		// Token: 0x06005D60 RID: 23904 RVA: 0x0011CDCC File Offset: 0x0011AFCC
		private void ApplyRenderActiveStep()
		{
			if (this.RenderedSteps == RadWizardRenderedSteps.All)
			{
				return;
			}
			foreach (object obj in this.WizardSteps)
			{
				RadWizardStep radWizardStep = (RadWizardStep)obj;
				radWizardStep.Visible = radWizardStep.Active;
			}
		}

		// Token: 0x06005D61 RID: 23905 RVA: 0x0011CE34 File Offset: 0x0011B034
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06005D62 RID: 23906 RVA: 0x0011CE40 File Offset: 0x0011B040
		protected internal virtual void RaisePostBackEvent(string eventArgument)
		{
			WizardPostBackCommand wizardPostBackCommand = null;
			try
			{
				wizardPostBackCommand = new JavaScriptSerializer().Deserialize<WizardPostBackCommand>(eventArgument);
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (wizardPostBackCommand == null)
			{
				return;
			}
			RadWizardStep radWizardStep = this.WizardSteps[this.ActiveStepIndex];
			int index = wizardPostBackCommand.Index;
			if (!this.WizardSteps[this.ActiveStepIndex].AllowReturn && this.History.Contains(this.ActiveStepIndex))
			{
				this.ActiveStepIndex = index;
			}
			if (radWizardStep != null)
			{
				this.PerformValidation(index, this.ActiveStepIndex);
				switch (wizardPostBackCommand.Type)
				{
				case RadWizardCommand.Previous:
					this.OnPreviousButtonClick(new WizardEventArgs(this.WizardSteps[index], this.WizardSteps[this.ActiveStepIndex]));
					break;
				case RadWizardCommand.Next:
					this.OnNextButtonClick(new WizardEventArgs(this.WizardSteps[index], this.WizardSteps[this.ActiveStepIndex]));
					break;
				case RadWizardCommand.Finish:
					this.OnFinishButtonClick(new WizardEventArgs(this.WizardSteps[index], this.WizardSteps[this.ActiveStepIndex]));
					break;
				case RadWizardCommand.Cancel:
					this.OnCancelButtonClick(new WizardEventArgs(this.WizardSteps[index], this.WizardSteps[this.ActiveStepIndex]));
					break;
				case RadWizardCommand.NavigationBarButtonClick:
					this.OnNavigationBarButtonClick(new WizardEventArgs(this.WizardSteps[index], this.WizardSteps[this.ActiveStepIndex]));
					break;
				}
				if (!this.ActiveStep.Enabled)
				{
					this.ActiveStepIndex = index;
				}
				if (wizardPostBackCommand.Type != RadWizardCommand.Cancel && index != this.ActiveStepIndex)
				{
					this.OnActiveStepChanged();
				}
			}
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x0011D008 File Offset: 0x0011B208
		private void PerformValidation(int lastActiveIndex, int nextActiveIndex)
		{
			RadWizardStep radWizardStep = this.WizardSteps[lastActiveIndex];
			if (!radWizardStep.CausesValidation || lastActiveIndex == nextActiveIndex || radWizardStep.ValidationGroup == this.WizardSteps[nextActiveIndex].ValidationGroup)
			{
				return;
			}
			this.Page.Validate(radWizardStep.ValidationGroup);
			if (!this.Page.IsValid)
			{
				this.ActiveStepIndex = lastActiveIndex;
				return;
			}
			if (nextActiveIndex > -1)
			{
				this.ActiveStepIndex = nextActiveIndex;
			}
		}

		// Token: 0x06005D64 RID: 23908 RVA: 0x0011D080 File Offset: 0x0011B280
		protected override object SaveViewState()
		{
			List<string> list = new List<string>();
			foreach (object obj in this.WizardSteps)
			{
				RadWizardStep radWizardStep = (RadWizardStep)obj;
				list.Add(radWizardStep.ID);
			}
			return new object[]
			{
				base.SaveViewState(),
				list.ToArray()
			};
		}

		// Token: 0x06005D65 RID: 23909 RVA: 0x0011D104 File Offset: 0x0011B304
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			string[] array2 = (string[])array[1];
			for (int i = this.WizardSteps.Count; i < array2.Length; i++)
			{
				RadWizardStep radWizardStep = this.CreateWizardStep();
				radWizardStep.ID = array2[i];
				this.WizardSteps.Add(radWizardStep);
			}
		}

		// Token: 0x06005D66 RID: 23910 RVA: 0x0011D160 File Offset: 0x0011B360
		private void RaiseEvent(object eventKey, WizardEventArgs e)
		{
			WizardEventHandler wizardEventHandler = base.Events[eventKey] as WizardEventHandler;
			if (wizardEventHandler != null)
			{
				wizardEventHandler(this, e);
			}
		}

		// Token: 0x06005D67 RID: 23911 RVA: 0x0011D18C File Offset: 0x0011B38C
		private void RaiseEvent(object eventKey, EventArgs e)
		{
			EventHandler eventHandler = base.Events[eventKey] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06005D68 RID: 23912 RVA: 0x0011D1B8 File Offset: 0x0011B3B8
		protected virtual void OnWizardStepCreated(WizardStepCreatedEventArgs eventArgs)
		{
			WizardStepCreatedEventHandler wizardStepCreatedEventHandler = base.Events[RadWizard.WizardStepCreatedEvent] as WizardStepCreatedEventHandler;
			if (wizardStepCreatedEventHandler != null)
			{
				wizardStepCreatedEventHandler(this, eventArgs);
			}
		}

		// Token: 0x06005D69 RID: 23913 RVA: 0x0011D1E8 File Offset: 0x0011B3E8
		protected override void LoadControlState(object savedState)
		{
			Array array = savedState as Array;
			if (array != null)
			{
				Array.Reverse(array);
				this._historyStack = new Stack<int>(array.Cast<int>());
			}
		}

		// Token: 0x06005D6A RID: 23914 RVA: 0x0011D218 File Offset: 0x0011B418
		protected override object SaveControlState()
		{
			this.PushHistoryIndex(this.ActiveStepIndex);
			bool flag = this._historyStack != null && this._historyStack.Count > 0;
			if (flag)
			{
				return flag ? this._historyStack.ToArray() : null;
			}
			return null;
		}

		// Token: 0x06005D6B RID: 23915 RVA: 0x0011D264 File Offset: 0x0011B464
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[base.ClientStateFieldID];
			if (!string.IsNullOrEmpty(text))
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				try
				{
					this.LoadClientState(javaScriptSerializer.Deserialize<WizardClientState>(text));
				}
				catch (InvalidOperationException)
				{
				}
				catch (ArgumentException)
				{
				}
			}
			return false;
		}

		// Token: 0x06005D6C RID: 23916 RVA: 0x0011D2C0 File Offset: 0x0011B4C0
		private void LoadClientState(WizardClientState clientState)
		{
			this.ActiveStepIndex = clientState.ActiveIndex;
			this.ProgressPercent = clientState.ProgressPercent;
			if (clientState.ChangeLog == null)
			{
				return;
			}
			foreach (ClientStateLogEntry clientStateLogEntry in clientState.ChangeLog)
			{
				switch (clientStateLogEntry.Type)
				{
				case ClientStateLogEntryType.Insert:
				{
					RadWizardStep radWizardStep = this.CreateWizardStep();
					if (clientStateLogEntry.Data != null && clientStateLogEntry.Data.ContainsKey("id"))
					{
						radWizardStep.ID = clientStateLogEntry.Data["id"].ToString();
					}
					this.WizardSteps.AddAt(Convert.ToInt32(clientStateLogEntry.Index), radWizardStep);
					break;
				}
				case ClientStateLogEntryType.Remove:
					this.WizardSteps.RemoveAt(Convert.ToInt32(clientStateLogEntry.Index));
					break;
				case ClientStateLogEntryType.Update:
					if (clientStateLogEntry.Data != null && clientStateLogEntry.Data.ContainsKey("title"))
					{
						this.WizardSteps[Convert.ToInt32(clientStateLogEntry.Index)].Title = clientStateLogEntry.Data["title"].ToString();
					}
					if (clientStateLogEntry.Data != null && clientStateLogEntry.Data.ContainsKey("enable"))
					{
						this.WizardSteps[Convert.ToInt32(clientStateLogEntry.Index)].Enabled = Convert.ToBoolean(clientStateLogEntry.Data["enable"]);
					}
					if (clientStateLogEntry.Data != null && clientStateLogEntry.Data.ContainsKey("allowReturn"))
					{
						this.WizardSteps[Convert.ToInt32(clientStateLogEntry.Index)].AllowReturn = Convert.ToBoolean(clientStateLogEntry.Data["allowReturn"]);
					}
					break;
				}
			}
		}

		// Token: 0x06005D6D RID: 23917 RVA: 0x0011D4AC File Offset: 0x0011B6AC
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			if (this.ActiveStepIndex > -1 && this.ActiveStepIndex < this.WizardSteps.Count)
			{
				descriptor.AddProperty("_activeIndex", this.ActiveStepIndex);
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new WizardStepJavaScriptConverter()
			});
			base.DescribeRenderMode(descriptor);
			if (this.RenderedSteps == RadWizardRenderedSteps.Active)
			{
				descriptor.AddProperty("_renderedSteps", this.RenderedSteps);
			}
			if (this.ProgressPercent != 0)
			{
				descriptor.AddProperty("_progressPercent", this.ProgressPercent);
			}
			if (base.Events[RadWizard.PreviousButtonClickEvent] != null)
			{
				descriptor.AddProperty("_previousPostBack", this.GetPostbackEventReference());
			}
			if (base.Events[RadWizard.NextButtonClickEvent] != null)
			{
				descriptor.AddProperty("_nextPostBack", this.GetPostbackEventReference());
			}
			if (base.Events[RadWizard.CancelButtonClickEvent] != null)
			{
				descriptor.AddProperty("_cancelPostBack", this.GetPostbackEventReference());
			}
			if (base.Events[RadWizard.FinishButtonClickEvent] != null)
			{
				descriptor.AddProperty("_finishPostBack", this.GetPostbackEventReference());
			}
			if (base.Events[RadWizard.NavigationBarButtonClickEvent] != null)
			{
				descriptor.AddProperty("_navigationBarPostBack", this.GetPostbackEventReference());
			}
			if (!this.DisplayNavigationBar)
			{
				descriptor.AddProperty("_displayNavigationBar", false);
			}
			if (!this.DisplayProgressBar)
			{
				descriptor.AddProperty("_displayProgressBar", false);
			}
			if (this.ClickActiveStep)
			{
				descriptor.AddProperty("_clickActiveStep", true);
			}
			if (this.RenderedSteps == RadWizardRenderedSteps.Active || base.Events[RadWizard.ActiveStepChangedEvent] != null)
			{
				descriptor.AddProperty("_postBackReference", this.GetPostbackEventReference());
			}
			if (this.WizardSteps.Count > 0)
			{
				descriptor.AddScriptProperty("wizardStepData", javaScriptSerializer.Serialize(this.WizardSteps));
			}
		}

		// Token: 0x06005D6E RID: 23918 RVA: 0x0011D69D File Offset: 0x0011B89D
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadWebControl.DescribeEvent(descriptor, "buttonClicking", this.OnClientButtonClicking);
			RadWebControl.DescribeEvent(descriptor, "buttonClicked", this.OnClientButtonClicked);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x06005D6F RID: 23919 RVA: 0x0011D6DC File Offset: 0x0011B8DC
		protected string GetPostbackEventReference()
		{
			string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(this, "arguments"));
			return postBackEventReference.Replace("\"", "'");
		}

		// Token: 0x06005D70 RID: 23920 RVA: 0x0011D718 File Offset: 0x0011B918
		internal PostBackOptions GetPostBackOptions(Control control, string argument)
		{
			return new PostBackOptions(control, argument)
			{
				ClientSubmit = true
			};
		}

		// Token: 0x06005D71 RID: 23921 RVA: 0x0011D737 File Offset: 0x0011B937
		internal void Describe(IScriptDescriptor descriptor)
		{
			this.DescribeComponent(descriptor);
		}

		// Token: 0x17001ECB RID: 7883
		// (get) Token: 0x06005D72 RID: 23922 RVA: 0x0011D740 File Offset: 0x0011B940
		[Description("The steps of RadWizard")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadWizardStepCollection WizardSteps
		{
			get
			{
				if (this._wizardStepCollection == null)
				{
					this._wizardStepCollection = (RadWizardStepCollection)this.Controls;
				}
				return this._wizardStepCollection;
			}
		}

		// Token: 0x17001ECC RID: 7884
		// (get) Token: 0x06005D73 RID: 23923 RVA: 0x0011D761 File Offset: 0x0011B961
		public RadWizardStep ActiveStep
		{
			get
			{
				if (this.WizardSteps.Count > 0)
				{
					return this.WizardSteps[this.ActiveStepIndex];
				}
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x17001ECD RID: 7885
		// (get) Token: 0x06005D74 RID: 23924 RVA: 0x0011D788 File Offset: 0x0011B988
		// (set) Token: 0x06005D75 RID: 23925 RVA: 0x0011D7A9 File Offset: 0x0011B9A9
		[Description("Postback to the server when navigation buttons are clicked.")]
		[DefaultValue(RadWizardRenderedSteps.All)]
		[ClientControlProperty]
		[ClientPropertyName("_renderedSteps")]
		[Category("Behavior")]
		public RadWizardRenderedSteps RenderedSteps
		{
			get
			{
				return (RadWizardRenderedSteps)(this.ViewState["RenderedSteps"] ?? RadWizardRenderedSteps.All);
			}
			set
			{
				this.ViewState["RenderedSteps"] = value;
			}
		}

		// Token: 0x17001ECE RID: 7886
		// (get) Token: 0x06005D76 RID: 23926 RVA: 0x0011D7C1 File Offset: 0x0011B9C1
		// (set) Token: 0x06005D77 RID: 23927 RVA: 0x0011D7E2 File Offset: 0x0011B9E2
		[ClientControlProperty]
		[DefaultValue(true)]
		[Category("Behavior")]
		[ClientPropertyName("_displayNavigationBar")]
		[Description("This property controls the display of the navigationBar.")]
		public bool DisplayNavigationBar
		{
			get
			{
				return (bool)(this.ViewState["DisplayNavigationBar"] ?? true);
			}
			set
			{
				this.ViewState["DisplayNavigationBar"] = value;
			}
		}

		// Token: 0x17001ECF RID: 7887
		// (get) Token: 0x06005D78 RID: 23928 RVA: 0x0011D7FA File Offset: 0x0011B9FA
		// (set) Token: 0x06005D79 RID: 23929 RVA: 0x0011D81B File Offset: 0x0011BA1B
		[DefaultValue(true)]
		[Description("This property controls the display of the navigation buttons.")]
		[Category("Behavior")]
		public bool DisplayNavigationButtons
		{
			get
			{
				return (bool)(this.ViewState["DisplayNavigationButtons"] ?? true);
			}
			set
			{
				this.ViewState["DisplayNavigationButtons"] = value;
			}
		}

		// Token: 0x17001ED0 RID: 7888
		// (get) Token: 0x06005D7A RID: 23930 RVA: 0x0011D833 File Offset: 0x0011BA33
		// (set) Token: 0x06005D7B RID: 23931 RVA: 0x0011D85E File Offset: 0x0011BA5E
		[Description("Indicating the position of the image within the item.")]
		[DefaultValue(RadWizardImagePostion.Left)]
		public RadWizardImagePostion ImagePosition
		{
			get
			{
				if (this.ViewState["ImagePosition"] == null)
				{
					return RadWizardImagePostion.Left;
				}
				return (RadWizardImagePostion)this.ViewState["ImagePosition"];
			}
			set
			{
				this.ViewState["ImagePosition"] = value;
			}
		}

		// Token: 0x17001ED1 RID: 7889
		// (get) Token: 0x06005D7C RID: 23932 RVA: 0x0011D876 File Offset: 0x0011BA76
		// (set) Token: 0x06005D7D RID: 23933 RVA: 0x0011D897 File Offset: 0x0011BA97
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(true)]
		[ClientPropertyName("_displayProgressBar")]
		[Description("This property controls the display of the progress bar.")]
		public bool DisplayProgressBar
		{
			get
			{
				return (bool)(this.ViewState["DisplayProgressBar"] ?? true);
			}
			set
			{
				this.ViewState["DisplayProgressBar"] = value;
			}
		}

		// Token: 0x17001ED2 RID: 7890
		// (get) Token: 0x06005D7E RID: 23934 RVA: 0x0011D8B0 File Offset: 0x0011BAB0
		// (set) Token: 0x06005D7F RID: 23935 RVA: 0x0011D8D9 File Offset: 0x0011BAD9
		[Description("Specifies whether Cancel button should be displayed in RadWizard.")]
		[DefaultValue(false)]
		public virtual bool DisplayCancelButton
		{
			get
			{
				object obj = this.ViewState["DisplayCancelButton"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["DisplayCancelButton"] = value;
			}
		}

		// Token: 0x17001ED3 RID: 7891
		// (get) Token: 0x06005D80 RID: 23936 RVA: 0x0011D8F1 File Offset: 0x0011BAF1
		// (set) Token: 0x06005D81 RID: 23937 RVA: 0x0011D912 File Offset: 0x0011BB12
		[ClientPropertyName("_clickActiveStep")]
		[DefaultValue(false)]
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("This property controls the click of the active step.")]
		public bool ClickActiveStep
		{
			get
			{
				return (bool)(this.ViewState["ClickActiveStep"] ?? false);
			}
			set
			{
				this.ViewState["ClickActiveStep"] = value;
			}
		}

		// Token: 0x17001ED4 RID: 7892
		// (get) Token: 0x06005D82 RID: 23938 RVA: 0x0011D92A File Offset: 0x0011BB2A
		// (set) Token: 0x06005D83 RID: 23939 RVA: 0x0011D94B File Offset: 0x0011BB4B
		[Description("The index of the currently selected WizardStep.")]
		[DefaultValue(0)]
		[Category("Behavior")]
		public int ActiveStepIndex
		{
			get
			{
				return (int)(this.ViewState["ActiveStepIndex"] ?? 0);
			}
			set
			{
				this.ViewState["ActiveStepIndex"] = value;
				this.ApplyRenderActiveStep();
				this.ProgressPercent = 0;
			}
		}

		// Token: 0x17001ED5 RID: 7893
		// (get) Token: 0x06005D84 RID: 23940 RVA: 0x0011D970 File Offset: 0x0011BB70
		// (set) Token: 0x06005D85 RID: 23941 RVA: 0x0011D991 File Offset: 0x0011BB91
		[DefaultValue(RadWizardNavigationBarPosition.Top)]
		[Description("The position of the navigation bar")]
		public RadWizardNavigationBarPosition NavigationBarPosition
		{
			get
			{
				return (RadWizardNavigationBarPosition)(this.ViewState["NavigationBarPosition"] ?? RadWizardNavigationBarPosition.Top);
			}
			set
			{
				this.ViewState["NavigationBarPosition"] = value;
			}
		}

		// Token: 0x17001ED6 RID: 7894
		// (get) Token: 0x06005D86 RID: 23942 RVA: 0x0011D9A9 File Offset: 0x0011BBA9
		// (set) Token: 0x06005D87 RID: 23943 RVA: 0x0011D9CA File Offset: 0x0011BBCA
		[Description("The position of the navigation buttons")]
		[DefaultValue(RadWizardNavigationButtonsPosition.Bottom)]
		public RadWizardNavigationButtonsPosition NavigationButtonsPosition
		{
			get
			{
				return (RadWizardNavigationButtonsPosition)(this.ViewState["NavigationButtonsPosition"] ?? RadWizardNavigationButtonsPosition.Bottom);
			}
			set
			{
				this.ViewState["NavigationButtonsPosition"] = value;
			}
		}

		// Token: 0x17001ED7 RID: 7895
		// (get) Token: 0x06005D88 RID: 23944 RVA: 0x0011D9E2 File Offset: 0x0011BBE2
		// (set) Token: 0x06005D89 RID: 23945 RVA: 0x0011DA03 File Offset: 0x0011BC03
		[DefaultValue(RadWizardProgressBarPosition.Top)]
		[Description("The position of the ProgressBar")]
		public RadWizardProgressBarPosition ProgressBarPosition
		{
			get
			{
				return (RadWizardProgressBarPosition)(this.ViewState["ProgressBarPosition"] ?? RadWizardProgressBarPosition.Top);
			}
			set
			{
				this.ViewState["ProgressBarPosition"] = value;
			}
		}

		// Token: 0x17001ED8 RID: 7896
		// (get) Token: 0x06005D8A RID: 23946 RVA: 0x0011DA1B File Offset: 0x0011BC1B
		// (set) Token: 0x06005D8B RID: 23947 RVA: 0x0011DA3C File Offset: 0x0011BC3C
		[Description("The percent of the completed progress area.")]
		[Category("Behavior")]
		[DefaultValue(0)]
		public int ProgressPercent
		{
			get
			{
				return (int)(this.ViewState["ProgressPercent"] ?? 0);
			}
			set
			{
				this.ViewState["ProgressPercent"] = value;
			}
		}

		// Token: 0x17001ED9 RID: 7897
		// (get) Token: 0x06005D8C RID: 23948 RVA: 0x0011DA54 File Offset: 0x0011BC54
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public WizardButtons Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new WizardButtons(new LocalizationProvider("RadWizard", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17001EDA RID: 7898
		// (get) Token: 0x06005D8D RID: 23949 RVA: 0x0011DA93 File Offset: 0x0011BC93
		// (set) Token: 0x06005D8E RID: 23950 RVA: 0x0011DAB4 File Offset: 0x0011BCB4
		[Description("Gets or sets a value indicating where RadWizard will look for its .resx localization files.")]
		[Category("Misc")]
		[DefaultValue("")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (text.Length > 0 && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["LocalizationPath"] = text;
			}
		}

		// Token: 0x17001EDB RID: 7899
		// (get) Token: 0x06005D8F RID: 23951 RVA: 0x0011DB07 File Offset: 0x0011BD07
		// (set) Token: 0x06005D90 RID: 23952 RVA: 0x0011DB27 File Offset: 0x0011BD27
		[Category("Misc")]
		[DefaultValue(typeof(CultureInfo), "en-US")]
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		public CultureInfo Culture
		{
			get
			{
				return ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.CurrentUICulture;
			}
			set
			{
				this.ViewState["Culture"] = value;
			}
		}

		// Token: 0x06005D91 RID: 23953 RVA: 0x0011DB3A File Offset: 0x0011BD3A
		public virtual RadWizardStep CreateWizardStep()
		{
			return new RadWizardStep();
		}

		// Token: 0x06005D92 RID: 23954 RVA: 0x0011DB44 File Offset: 0x0011BD44
		public ICollection GetHistory()
		{
			ArrayList arrayList = new ArrayList();
			foreach (int index in this.History)
			{
				arrayList.Add(this.WizardSteps[index]);
			}
			return arrayList;
		}

		// Token: 0x06005D93 RID: 23955 RVA: 0x0011DBAC File Offset: 0x0011BDAC
		public int GetPreviousStepIndex()
		{
			int num = -1;
			int activeStepIndex = this.ActiveStepIndex;
			if (this._historyStack == null || this._historyStack.Count == 0)
			{
				return num;
			}
			num = this._historyStack.Peek();
			if (num == activeStepIndex && this._historyStack.Count > 1)
			{
				int item = this._historyStack.Pop();
				num = this._historyStack.Peek();
				this._historyStack.Push(item);
			}
			if (num == activeStepIndex)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x140000D7 RID: 215
		// (add) Token: 0x06005D94 RID: 23956 RVA: 0x0011DC22 File Offset: 0x0011BE22
		// (remove) Token: 0x06005D95 RID: 23957 RVA: 0x0011DC35 File Offset: 0x0011BE35
		public event WizardEventHandler CancelButtonClick
		{
			add
			{
				base.Events.AddHandler(RadWizard.CancelButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadWizard.CancelButtonClickEvent, value);
			}
		}

		// Token: 0x06005D96 RID: 23958 RVA: 0x0011DC48 File Offset: 0x0011BE48
		protected void OnCancelButtonClick(WizardEventArgs e)
		{
			this.RaiseEvent(RadWizard.CancelButtonClickEvent, e);
		}

		// Token: 0x140000D8 RID: 216
		// (add) Token: 0x06005D97 RID: 23959 RVA: 0x0011DC56 File Offset: 0x0011BE56
		// (remove) Token: 0x06005D98 RID: 23960 RVA: 0x0011DC69 File Offset: 0x0011BE69
		public event WizardEventHandler FinishButtonClick
		{
			add
			{
				base.Events.AddHandler(RadWizard.FinishButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadWizard.FinishButtonClickEvent, value);
			}
		}

		// Token: 0x06005D99 RID: 23961 RVA: 0x0011DC7C File Offset: 0x0011BE7C
		protected void OnFinishButtonClick(WizardEventArgs e)
		{
			this.RaiseEvent(RadWizard.FinishButtonClickEvent, e);
		}

		// Token: 0x140000D9 RID: 217
		// (add) Token: 0x06005D9A RID: 23962 RVA: 0x0011DC8A File Offset: 0x0011BE8A
		// (remove) Token: 0x06005D9B RID: 23963 RVA: 0x0011DC9D File Offset: 0x0011BE9D
		public event WizardEventHandler NextButtonClick
		{
			add
			{
				base.Events.AddHandler(RadWizard.NextButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadWizard.NextButtonClickEvent, value);
			}
		}

		// Token: 0x06005D9C RID: 23964 RVA: 0x0011DCB0 File Offset: 0x0011BEB0
		protected void OnNextButtonClick(WizardEventArgs e)
		{
			this.RaiseEvent(RadWizard.NextButtonClickEvent, e);
		}

		// Token: 0x140000DA RID: 218
		// (add) Token: 0x06005D9D RID: 23965 RVA: 0x0011DCBE File Offset: 0x0011BEBE
		// (remove) Token: 0x06005D9E RID: 23966 RVA: 0x0011DCD1 File Offset: 0x0011BED1
		public event WizardEventHandler PreviousButtonClick
		{
			add
			{
				base.Events.AddHandler(RadWizard.PreviousButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadWizard.PreviousButtonClickEvent, value);
			}
		}

		// Token: 0x06005D9F RID: 23967 RVA: 0x0011DCE4 File Offset: 0x0011BEE4
		protected void OnPreviousButtonClick(WizardEventArgs e)
		{
			this.RaiseEvent(RadWizard.PreviousButtonClickEvent, e);
		}

		// Token: 0x140000DB RID: 219
		// (add) Token: 0x06005DA0 RID: 23968 RVA: 0x0011DCF2 File Offset: 0x0011BEF2
		// (remove) Token: 0x06005DA1 RID: 23969 RVA: 0x0011DD05 File Offset: 0x0011BF05
		public event WizardEventHandler NavigationBarButtonClick
		{
			add
			{
				base.Events.AddHandler(RadWizard.NavigationBarButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadWizard.NavigationBarButtonClickEvent, value);
			}
		}

		// Token: 0x06005DA2 RID: 23970 RVA: 0x0011DD18 File Offset: 0x0011BF18
		protected void OnNavigationBarButtonClick(WizardEventArgs e)
		{
			this.RaiseEvent(RadWizard.NavigationBarButtonClickEvent, e);
		}

		// Token: 0x140000DC RID: 220
		// (add) Token: 0x06005DA3 RID: 23971 RVA: 0x0011DD26 File Offset: 0x0011BF26
		// (remove) Token: 0x06005DA4 RID: 23972 RVA: 0x0011DD39 File Offset: 0x0011BF39
		public event EventHandler ActiveStepChanged
		{
			add
			{
				base.Events.AddHandler(RadWizard.ActiveStepChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadWizard.ActiveStepChangedEvent, value);
			}
		}

		// Token: 0x06005DA5 RID: 23973 RVA: 0x0011DD4C File Offset: 0x0011BF4C
		internal void OnActiveStepChanged()
		{
			this.RaiseEvent(RadWizard.ActiveStepChangedEvent, new EventArgs());
		}

		// Token: 0x140000DD RID: 221
		// (add) Token: 0x06005DA6 RID: 23974 RVA: 0x0011DD5E File Offset: 0x0011BF5E
		// (remove) Token: 0x06005DA7 RID: 23975 RVA: 0x0011DD71 File Offset: 0x0011BF71
		public event WizardStepCreatedEventHandler WizardStepCreated
		{
			add
			{
				base.Events.AddHandler(RadWizard.WizardStepCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadWizard.WizardStepCreatedEvent, value);
			}
		}

		// Token: 0x17001EDC RID: 7900
		// (get) Token: 0x06005DA8 RID: 23976 RVA: 0x0011DD84 File Offset: 0x0011BF84
		// (set) Token: 0x06005DA9 RID: 23977 RVA: 0x0011DDA4 File Offset: 0x0011BFA4
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The client-side event that is fired when some button is clicked.")]
		[ClientPropertyName("buttonClicking")]
		[DefaultValue("")]
		[Bindable(false)]
		[Category("Client-side events")]
		public string OnClientButtonClicking
		{
			get
			{
				return (string)(this.ViewState["OnClientButtonClicking"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientButtonClicking"] = value;
			}
		}

		// Token: 0x17001EDD RID: 7901
		// (get) Token: 0x06005DAA RID: 23978 RVA: 0x0011DDB7 File Offset: 0x0011BFB7
		// (set) Token: 0x06005DAB RID: 23979 RVA: 0x0011DDD7 File Offset: 0x0011BFD7
		[Bindable(false)]
		[Category("Client-side events")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The client-side event that is fired when some button is clicked.")]
		[ClientPropertyName("buttonClicked")]
		public string OnClientButtonClicked
		{
			get
			{
				return (string)(this.ViewState["OnClientButtonClicked"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientButtonClicked"] = value;
			}
		}

		// Token: 0x17001EDE RID: 7902
		// (get) Token: 0x06005DAC RID: 23980 RVA: 0x0011DDEA File Offset: 0x0011BFEA
		// (set) Token: 0x06005DAD RID: 23981 RVA: 0x0011DE0A File Offset: 0x0011C00A
		[DefaultValue("")]
		[Description("The JavaScript function executed when RadWizard is initialized")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("load")]
		[Category("Client-side events")]
		public string OnClientLoad
		{
			get
			{
				return (string)(this.ViewState["OnClientLoad"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x06005DAF RID: 23983 RVA: 0x0011DE20 File Offset: 0x0011C020
		// Note: this type is marked as 'beforefieldinit'.
		static RadWizard()
		{
			RadWizard.CancelButtonClickEvent = new object();
			RadWizard.FinishButtonClickEvent = new object();
			RadWizard.NextButtonClickEvent = new object();
			RadWizard.PreviousButtonClickEvent = new object();
			RadWizard.NavigationBarButtonClickEvent = new object();
			RadWizard.ActiveStepChangedEvent = new object();
			RadWizard.WizardStepCreatedEvent = new object();
		}

		// Token: 0x04001672 RID: 5746
		internal Stack<int> _historyStack;

		// Token: 0x04001673 RID: 5747
		private RadWizardStepCollection _wizardStepCollection;

		// Token: 0x04001674 RID: 5748
		private WizardButtons _localization;

		// Token: 0x02000997 RID: 2455
		internal static class Styles
		{
			// Token: 0x06005DB0 RID: 23984 RVA: 0x0011DE7B File Offset: 0x0011C07B
			internal static string Combine(params string[] classNames)
			{
				return string.Join(" ", classNames).Trim();
			}

			// Token: 0x0400167C RID: 5756
			internal const string RadWizardCssClass = "RadWizard";

			// Token: 0x0400167D RID: 5757
			internal const string RadWizardRTLCssClass = "RadWizard_rtl";

			// Token: 0x0400167E RID: 5758
			internal const string VerticalCssClass = "rwzVertical";

			// Token: 0x0400167F RID: 5759
			internal const string HorizontalCssClass = "rwzHorizontal";

			// Token: 0x04001680 RID: 5760
			internal const string NavigationBarCssClass = "rwzBreadCrumb";

			// Token: 0x04001681 RID: 5761
			internal const string RightNavigationBarCssClass = "rwzRightBreadCrumb";

			// Token: 0x04001682 RID: 5762
			internal const string BottomNavigationBarCssClass = "rwzBottomBreadCrumb";

			// Token: 0x04001683 RID: 5763
			internal const string LinkCssClass = "rwzLink";

			// Token: 0x04001684 RID: 5764
			internal const string FirstCssClass = "rwzFirst";

			// Token: 0x04001685 RID: 5765
			internal const string LastCssClass = "rwzLast";

			// Token: 0x04001686 RID: 5766
			internal const string ProgressBarCssClass = "rwzProgressBar";

			// Token: 0x04001687 RID: 5767
			internal const string LeftProgressBarCssClass = "rwzLeftProgressBar";

			// Token: 0x04001688 RID: 5768
			internal const string TopProgressBarCssClass = "rwzTopProgressBar";

			// Token: 0x04001689 RID: 5769
			internal const string RightProgressBarCssClass = "rwzRightProgressBar";

			// Token: 0x0400168A RID: 5770
			internal const string BottomProgressBarCssClass = "rwzBottomProgressBar";

			// Token: 0x0400168B RID: 5771
			internal const string LeftImageCssClass = "rwzLeftImages";

			// Token: 0x0400168C RID: 5772
			internal const string RightImageCssClass = "rwzRightImages";

			// Token: 0x0400168D RID: 5773
			internal const string ProgressCssClass = "rwzProgress";

			// Token: 0x0400168E RID: 5774
			internal const string StepCssClass = "rwzStep";

			// Token: 0x0400168F RID: 5775
			internal const string TextCssClass = "rwzText";

			// Token: 0x04001690 RID: 5776
			internal const string ImageCssClass = "rwzImage";

			// Token: 0x04001691 RID: 5777
			internal const string CompleteCssClass = "rwzComplete";

			// Token: 0x04001692 RID: 5778
			internal const string ContentCssClass = "rwzContent";

			// Token: 0x04001693 RID: 5779
			internal const string ContentWrapperCssClass = "rwzContentWrapper";

			// Token: 0x04001694 RID: 5780
			internal const string ButtonsCssClass = "rwzButton";

			// Token: 0x04001695 RID: 5781
			internal const string FinishCssClass = "rwzFinish";

			// Token: 0x04001696 RID: 5782
			internal const string CancelCssClass = "rwzCancel";

			// Token: 0x04001697 RID: 5783
			internal const string PreviousCssClass = "rwzPrevious";

			// Token: 0x04001698 RID: 5784
			internal const string NextCssClass = "rwzNext";

			// Token: 0x04001699 RID: 5785
			internal const string RightCssClass = "rwzRight";

			// Token: 0x0400169A RID: 5786
			internal const string NavigationButtonsCssClass = "rwzNav";

			// Token: 0x0400169B RID: 5787
			internal const string UlElementsCssClass = "rwzUL";

			// Token: 0x0400169C RID: 5788
			internal const string LiElementsCssClass = "rwzLI";

			// Token: 0x0400169D RID: 5789
			internal const string ActiveStepElementsCssClass = "rwzActive";

			// Token: 0x0400169E RID: 5790
			internal const string SelectedStepElementsCssClass = "rwzSelected";

			// Token: 0x0400169F RID: 5791
			internal const string DisabledCssClass = "rwzDisabled";

			// Token: 0x040016A0 RID: 5792
			internal const string HiddenCssClass = "rwzHidden";

			// Token: 0x040016A1 RID: 5793
			internal const string CallOutCssClass = "rwzCallout";

			// Token: 0x040016A2 RID: 5794
			internal const string PagerCssClass = "rwzPager";

			// Token: 0x040016A3 RID: 5795
			internal const string HeaderCssClass = "rwzHeader";

			// Token: 0x040016A4 RID: 5796
			internal const string SliderCssClass = "rwzSlider";

			// Token: 0x040016A5 RID: 5797
			internal const string FooterCssClass = "rwzFooter";

			// Token: 0x040016A6 RID: 5798
			internal const string MobileCancelCssClass = "rwzCancelBtn";

			// Token: 0x040016A7 RID: 5799
			internal const string MobileCancelWrapperCssClass = "rwzCancelWrapper";
		}
	}
}
