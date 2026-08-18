using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000E0D RID: 3597
	[LightweightRendering]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredScript(typeof(RadToolTipScripts))]
	[ClientScriptResource("Telerik.Web.UI.RadToolTipManager", "Telerik.Web.UI.ToolTip.Scripts.RadToolTipManager.js")]
	[ParseChildren(true)]
	[TelerikToolboxCategory("Container")]
	[ToolboxBitmap(typeof(RadToolTipManager), "Telerik.Web.UI.ToolTip.png")]
	[Designer("Telerik.Web.Design.RadToolTipManagerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	public class RadToolTipManager : RadToolTipBase
	{
		// Token: 0x06008594 RID: 34196 RVA: 0x001E72B0 File Offset: 0x001E54B0
		public RadToolTipManager()
		{
			this._webServiceSettings = new WebServiceSettings(this.ViewState);
		}

		// Token: 0x17002A52 RID: 10834
		// (get) Token: 0x06008595 RID: 34197 RVA: 0x001E72C9 File Offset: 0x001E54C9
		[DefaultValue(null)]
		[Category("Behavior")]
		[Description("The web service to be used for populating a tooltip from WebService.")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public WebServiceSettings WebServiceSettings
		{
			get
			{
				return this._webServiceSettings;
			}
		}

		// Token: 0x17002A53 RID: 10835
		// (get) Token: 0x06008596 RID: 34198 RVA: 0x001E72D1 File Offset: 0x001E54D1
		// (set) Token: 0x06008597 RID: 34199 RVA: 0x001E7300 File Offset: 0x001E5500
		[ClientPropertyName("requestStart")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public virtual string OnClientRequestStart
		{
			get
			{
				if (this.ViewState["OnClientRequestStart"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientRequestStart"];
			}
			set
			{
				this.ViewState["OnClientRequestStart"] = value;
			}
		}

		// Token: 0x17002A54 RID: 10836
		// (get) Token: 0x06008598 RID: 34200 RVA: 0x001E7313 File Offset: 0x001E5513
		// (set) Token: 0x06008599 RID: 34201 RVA: 0x001E7342 File Offset: 0x001E5542
		[DefaultValue("")]
		[ClientPropertyName("requestEnd")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public virtual string OnClientResponseEnd
		{
			get
			{
				if (this.ViewState["OnClientResponseEnd"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["OnClientResponseEnd"];
			}
			set
			{
				this.ViewState["OnClientResponseEnd"] = value;
			}
		}

		// Token: 0x17002A55 RID: 10837
		// (get) Token: 0x0600859A RID: 34202 RVA: 0x001E7355 File Offset: 0x001E5555
		// (set) Token: 0x0600859B RID: 34203 RVA: 0x001E7375 File Offset: 0x001E5575
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("responseError")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public virtual string OnClientResponseError
		{
			get
			{
				return ((string)this.ViewState["OnClientResponseError"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientResponseError"] = value;
			}
		}

		// Token: 0x17002A56 RID: 10838
		// (get) Token: 0x0600859C RID: 34204 RVA: 0x001E7388 File Offset: 0x001E5588
		// (set) Token: 0x0600859D RID: 34205 RVA: 0x001E73A9 File Offset: 0x001E55A9
		[DefaultValue(false)]
		[ClientControlProperty]
		[Description("Specifying if the data should be cached after first load on demand.")]
		[Category("Behavior")]
		[ClientPropertyName("enableDataCaching")]
		public bool EnableDataCaching
		{
			get
			{
				return (bool)(this.ViewState["EnableDataCaching"] ?? false);
			}
			set
			{
				this.ViewState["EnableDataCaching"] = value;
			}
		}

		// Token: 0x17002A57 RID: 10839
		// (get) Token: 0x0600859E RID: 34206 RVA: 0x001E73C1 File Offset: 0x001E55C1
		// (set) Token: 0x0600859F RID: 34207 RVA: 0x001E73F0 File Offset: 0x001E55F0
		[Category("Behavior")]
		[Description("Gets or sets the id (ClientID if a runat=server is used) of a html element whose children will be tooltipified")]
		[DefaultValue("")]
		[ClientControlProperty]
		[Browsable(true)]
		[Bindable(true)]
		public string ToolTipZoneID
		{
			get
			{
				if (this.ViewState["ToolTipZoneID"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["ToolTipZoneID"];
			}
			set
			{
				this.ViewState["ToolTipZoneID"] = value;
			}
		}

		// Token: 0x17002A58 RID: 10840
		// (get) Token: 0x060085A0 RID: 34208 RVA: 0x001E7403 File Offset: 0x001E5603
		// (set) Token: 0x060085A1 RID: 34209 RVA: 0x001E742E File Offset: 0x001E562E
		[ClientControlProperty]
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Gets or sets a value whether the RadToolTipManager, when its TargetControls collection is empty will tooltipify automatically all elements on the page that have a 'title' attribute")]
		[Browsable(true)]
		public bool AutoTooltipify
		{
			get
			{
				return this.ViewState["AutoTooltipify"] != null && (bool)this.ViewState["AutoTooltipify"];
			}
			set
			{
				this.ViewState["AutoTooltipify"] = value;
			}
		}

		// Token: 0x17002A59 RID: 10841
		// (get) Token: 0x060085A2 RID: 34210 RVA: 0x001E7446 File Offset: 0x001E5646
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ToolTipTargetControlCollection TargetControls
		{
			get
			{
				if (this._targetControls == null)
				{
					this._targetControls = new ToolTipTargetControlCollection();
				}
				return this._targetControls;
			}
		}

		// Token: 0x17002A5A RID: 10842
		// (get) Token: 0x060085A3 RID: 34211 RVA: 0x001E7461 File Offset: 0x001E5661
		public UpdatePanel UpdatePanel
		{
			get
			{
				this.CreateUpdatePanel();
				return this._updatePanel;
			}
		}

		// Token: 0x060085A4 RID: 34212 RVA: 0x001E746F File Offset: 0x001E566F
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.CreateUpdatePanel();
		}

		// Token: 0x060085A5 RID: 34213 RVA: 0x001E7480 File Offset: 0x001E5680
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			string text = (string)clientState["AjaxTargetControl"];
			string text2 = (string)clientState["Value"];
			if (text2 != null || text != null)
			{
				ToolTipUpdateEventArgs e = new ToolTipUpdateEventArgs(text, text2, this.UpdatePanel);
				this.OnAjaxUpdate(e);
			}
		}

		// Token: 0x060085A6 RID: 34214 RVA: 0x001E74D4 File Offset: 0x001E56D4
		private void CreateUpdatePanel()
		{
			if (this._updatePanel != null)
			{
				return;
			}
			this._updatePanel = new UpdatePanel();
			this._updatePanel.UpdateMode = UpdatePanelUpdateMode.Conditional;
			this._updatePanel.Unload += this.RegisterUpdatePanel;
			base.EnsureID();
			this._updatePanel.ID = this.ID + "RTMPanel";
			this._updatePanel.ClientIDMode = this.ClientIDMode;
			this.Controls.Add(this._updatePanel);
		}

		// Token: 0x060085A7 RID: 34215 RVA: 0x001E7570 File Offset: 0x001E5770
		private void RegisterUpdatePanel(object sender, EventArgs e)
		{
			try
			{
				MethodInfo methodInfo = (from methods in typeof(ScriptManager).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
				where methods.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")
				select methods).First<MethodInfo>();
				methodInfo.Invoke(ScriptManager.GetCurrent(this.Page), new object[]
				{
					this.UpdatePanel
				});
			}
			catch
			{
			}
		}

		// Token: 0x14000139 RID: 313
		// (add) Token: 0x060085A8 RID: 34216 RVA: 0x001E75F0 File Offset: 0x001E57F0
		// (remove) Token: 0x060085A9 RID: 34217 RVA: 0x001E7603 File Offset: 0x001E5803
		public event ToolTipUpdateEventHandler AjaxUpdate
		{
			add
			{
				base.Events.AddHandler(RadToolTipManager.EventAjaxUpdate, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadToolTipManager.EventAjaxUpdate, value);
			}
		}

		// Token: 0x060085AA RID: 34218 RVA: 0x001E7618 File Offset: 0x001E5818
		[Category("Action")]
		protected virtual void OnAjaxUpdate(ToolTipUpdateEventArgs e)
		{
			ToolTipUpdateEventHandler toolTipUpdateEventHandler = (ToolTipUpdateEventHandler)base.Events[RadToolTipManager.EventAjaxUpdate];
			ScriptManager current = ScriptManager.GetCurrent(this.Page);
			if (toolTipUpdateEventHandler != null && (current.IsInAsyncPostBack || this.Page.IsCallback))
			{
				string asyncPostBackSourceElementID = current.AsyncPostBackSourceElementID;
				Control control = this.Page.FindControl(asyncPostBackSourceElementID);
				if (e.UpdatePanel.UniqueID == asyncPostBackSourceElementID || control == null)
				{
					toolTipUpdateEventHandler(this, e);
				}
			}
		}

		// Token: 0x060085AB RID: 34219 RVA: 0x001E7694 File Offset: 0x001E5894
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddScriptProperty("loadOnDemand", ((ToolTipUpdateEventHandler)base.Events[RadToolTipManager.EventAjaxUpdate] != null).ToString().ToLower());
			descriptor.AddScriptProperty("isToolTipFactory", "true");
			descriptor.AddProperty("_updatePanelUniqueId", this.UpdatePanel.UniqueID);
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int count = this.TargetControls.Count;
			foreach (object obj in this.TargetControls)
			{
				ToolTipTargetControl toolTipTargetControl = (ToolTipTargetControl)obj;
				string arg = string.Empty;
				string arg2 = toolTipTargetControl.Value.Replace("'", "\\\\'").Replace("\"", "\\\"");
				if (toolTipTargetControl.IsClientID)
				{
					arg = toolTipTargetControl.TargetControlID;
				}
				else
				{
					Control control = ChildControlHelper.FindControlRecursive(this, toolTipTargetControl.TargetControlID, null);
					if (control == null)
					{
						base.ThrowControlNotFound(toolTipTargetControl.TargetControlID);
					}
					arg = control.ClientID;
				}
				stringBuilder.AppendFormat("['{0}','{1}','{2}']", arg, toolTipTargetControl.TargetControlID, arg2);
				if (++num < count)
				{
					stringBuilder.Append(",");
				}
			}
			descriptor.AddScriptProperty("targetControls", "\"[" + stringBuilder.ToString() + "]\"");
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			this.WebServiceSettings.Describe("webServiceSettings", serializer, descriptor);
		}

		// Token: 0x060085AC RID: 34220 RVA: 0x001E7840 File Offset: 0x001E5A40
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.TargetControls).LoadViewState(array[1]);
		}

		// Token: 0x060085AD RID: 34221 RVA: 0x001E786C File Offset: 0x001E5A6C
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.TargetControls).SaveViewState()
			};
		}

		// Token: 0x060085AE RID: 34222 RVA: 0x001E789A File Offset: 0x001E5A9A
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.TargetControls).TrackViewState();
		}

		// Token: 0x060085AF RID: 34223 RVA: 0x001E78B0 File Offset: 0x001E5AB0
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "autoTooltipify", this.AutoTooltipify, false);
			base.DescribeProperty<bool>(descriptor, "enableDataCaching", this.EnableDataCaching, false);
			base.DescribeProperty<string>(descriptor, "toolTipZoneID", this.ToolTipZoneID, "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060085B0 RID: 34224 RVA: 0x001E7901 File Offset: 0x001E5B01
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "requestStart", this.OnClientRequestStart);
			RadWebControl.DescribeEvent(descriptor, "requestEnd", this.OnClientResponseEnd);
			RadWebControl.DescribeEvent(descriptor, "responseError", this.OnClientResponseError);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04002540 RID: 9536
		private WebServiceSettings _webServiceSettings;

		// Token: 0x04002541 RID: 9537
		private ToolTipTargetControlCollection _targetControls;

		// Token: 0x04002542 RID: 9538
		private UpdatePanel _updatePanel;

		// Token: 0x04002543 RID: 9539
		private static readonly object EventAjaxUpdate = new object();
	}
}
