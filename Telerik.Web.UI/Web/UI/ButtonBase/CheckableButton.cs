using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI.ButtonBase
{
	// Token: 0x0200001D RID: 29
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(PostBackButtonBase))]
	[ClientScriptResource("Telerik.Web.UI.CheckableButton", "Telerik.Web.UI.Button.RadButtonScripts.js")]
	public abstract class CheckableButton : PostBackButtonBase, ICheckableButton
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00004E70 File Offset: 0x00003070
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			this.Checked = new bool?(clientState.ContainsKey("checked") && (bool)clientState["checked"]);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00004EA4 File Offset: 0x000030A4
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[base.ClientStateFieldID];
			if (!string.IsNullOrEmpty(text))
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				Dictionary<string, object> dictionary = javaScriptSerializer.DeserializeObject(text) as Dictionary<string, object>;
				if (dictionary != null && dictionary.ContainsKey("checked"))
				{
					this._checkedChangedFlag = ((this.Checked != null && this.Checked.Value) != (bool)dictionary["checked"]);
				}
			}
			return base.LoadPostData(postDataKey, postCollection) || this._checkedChangedFlag;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00004F38 File Offset: 0x00003138
		public void OnCheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CheckableButton.checkedChangedHandler];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00004F66 File Offset: 0x00003166
		protected override void RaisePostDataChangedEvent()
		{
			base.RaisePostDataChangedEvent();
			if (this._checkedChangedFlag)
			{
				this.OnCheckedChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00004F81 File Offset: 0x00003181
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00004F98 File Offset: 0x00003198
		[SimplePersistenceSetting]
		[Description("Gets or sets a value indicating whether the button is checked.")]
		[DefaultValue(false)]
		[Bindable(true, BindingDirection.TwoWay)]
		[ClientControlProperty]
		[ClientPropertyName("checked")]
		[Themeable(false)]
		public bool? Checked
		{
			get
			{
				return (bool?)this.ViewState["Checked"];
			}
			set
			{
				this.ViewState["Checked"] = value;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060001A6 RID: 422 RVA: 0x00004FB0 File Offset: 0x000031B0
		// (remove) Token: 0x060001A7 RID: 423 RVA: 0x00004FC3 File Offset: 0x000031C3
		[Description("Fired when the value of the Checked property changes between posts to the server.")]
		[Category("Action")]
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(CheckableButton.checkedChangedHandler, value);
			}
			remove
			{
				base.Events.RemoveHandler(CheckableButton.checkedChangedHandler, value);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00004FD6 File Offset: 0x000031D6
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00004FF6 File Offset: 0x000031F6
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when the button is about to be checked.")]
		[DefaultValue("")]
		[ClientPropertyName("checkedChanging")]
		public string OnClientCheckedChanging
		{
			get
			{
				return ((string)this.ViewState["OnClientCheckedChanging"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientCheckedChanging"] = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00005009 File Offset: 0x00003209
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00005029 File Offset: 0x00003229
		[Category("Client-side events")]
		[ClientPropertyName("checkedChanged")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Description("Gets or sets the name of the JavaScript function that will be called after the button has been checked.")]
		public string OnClientCheckedChanged
		{
			get
			{
				return ((string)this.ViewState["OnClientCheckedChanged"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientCheckedChanged"] = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000503C File Offset: 0x0000323C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(false)]
		[Themeable(false)]
		public override bool UseSubmitBehavior
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000503F File Offset: 0x0000323F
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool?>(descriptor, "checked", this.Checked, new bool?(false));
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005060 File Offset: 0x00003260
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "checkedChanged", this.OnClientCheckedChanged);
			RadWebControl.DescribeEvent(descriptor, "checkedChanging", this.OnClientCheckedChanging);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x0400001C RID: 28
		private bool _checkedChangedFlag;

		// Token: 0x0400001D RID: 29
		private static readonly object checkedChangedHandler = new object();
	}
}
