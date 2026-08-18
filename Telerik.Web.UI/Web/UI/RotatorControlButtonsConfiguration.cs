using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001A02 RID: 6658
	[ParseChildren(true)]
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class RotatorControlButtonsConfiguration : StateManager
	{
		// Token: 0x17004DC5 RID: 19909
		// (get) Token: 0x060101D4 RID: 66004 RVA: 0x0039F154 File Offset: 0x0039D354
		// (set) Token: 0x060101D5 RID: 66005 RVA: 0x0039F17F File Offset: 0x0039D37F
		private bool isValueSet
		{
			get
			{
				return base.ViewState["isVallueSet"] != null && (bool)base.ViewState["isVallueSet"];
			}
			set
			{
				base.ViewState["isVallueSet"] = value;
			}
		}

		// Token: 0x17004DC6 RID: 19910
		// (get) Token: 0x060101D6 RID: 66006 RVA: 0x0039F197 File Offset: 0x0039D397
		// (set) Token: 0x060101D7 RID: 66007 RVA: 0x0039F1B7 File Offset: 0x0039D3B7
		[Themeable(false)]
		[DefaultValue("")]
		[IDReferenceProperty]
		[TypeConverter(typeof(ControlIDConverter))]
		public string UpButtonID
		{
			get
			{
				return ((string)base.ViewState["UpButtonID"]) ?? string.Empty;
			}
			set
			{
				this.isValueSet = true;
				base.ViewState["UpButtonID"] = value;
			}
		}

		// Token: 0x17004DC7 RID: 19911
		// (get) Token: 0x060101D8 RID: 66008 RVA: 0x0039F1D1 File Offset: 0x0039D3D1
		// (set) Token: 0x060101D9 RID: 66009 RVA: 0x0039F1F1 File Offset: 0x0039D3F1
		[Themeable(false)]
		[TypeConverter(typeof(ControlIDConverter))]
		[DefaultValue("")]
		[IDReferenceProperty]
		public string DownButtonID
		{
			get
			{
				return ((string)base.ViewState["DownButtonID"]) ?? string.Empty;
			}
			set
			{
				this.isValueSet = true;
				base.ViewState["DownButtonID"] = value;
			}
		}

		// Token: 0x17004DC8 RID: 19912
		// (get) Token: 0x060101DA RID: 66010 RVA: 0x0039F20B File Offset: 0x0039D40B
		// (set) Token: 0x060101DB RID: 66011 RVA: 0x0039F22B File Offset: 0x0039D42B
		[DefaultValue("")]
		[Themeable(false)]
		[TypeConverter(typeof(ControlIDConverter))]
		[IDReferenceProperty]
		public string LeftButtonID
		{
			get
			{
				return ((string)base.ViewState["LeftButtonID"]) ?? string.Empty;
			}
			set
			{
				this.isValueSet = true;
				base.ViewState["LeftButtonID"] = value;
			}
		}

		// Token: 0x17004DC9 RID: 19913
		// (get) Token: 0x060101DC RID: 66012 RVA: 0x0039F245 File Offset: 0x0039D445
		// (set) Token: 0x060101DD RID: 66013 RVA: 0x0039F265 File Offset: 0x0039D465
		[Themeable(false)]
		[TypeConverter(typeof(ControlIDConverter))]
		[DefaultValue("")]
		[IDReferenceProperty]
		public string RightButtonID
		{
			get
			{
				return ((string)base.ViewState["RightButtonID"]) ?? string.Empty;
			}
			set
			{
				this.isValueSet = true;
				base.ViewState["RightButtonID"] = value;
			}
		}

		// Token: 0x17004DCA RID: 19914
		// (get) Token: 0x060101DE RID: 66014 RVA: 0x0039F27F File Offset: 0x0039D47F
		// (set) Token: 0x060101DF RID: 66015 RVA: 0x0039F29F File Offset: 0x0039D49F
		[Category("Client-side events")]
		[ClientPropertyName("buttonClick")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Description("The name of the javascript function called when the user clicks one of the control buttons.")]
		public string OnClientButtonClick
		{
			get
			{
				return ((string)base.ViewState["OnClientButtonClick"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnClientButtonClick"] = value;
			}
		}

		// Token: 0x17004DCB RID: 19915
		// (get) Token: 0x060101E0 RID: 66016 RVA: 0x0039F2B2 File Offset: 0x0039D4B2
		// (set) Token: 0x060101E1 RID: 66017 RVA: 0x0039F2D2 File Offset: 0x0039D4D2
		[ClientPropertyName("buttonOver")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("The name of the javascript function called when the mouse is over one of the control buttons.")]
		public string OnClientButtonOver
		{
			get
			{
				return ((string)base.ViewState["OnClientButtonOver"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnClientButtonOver"] = value;
			}
		}

		// Token: 0x17004DCC RID: 19916
		// (get) Token: 0x060101E2 RID: 66018 RVA: 0x0039F2E5 File Offset: 0x0039D4E5
		// (set) Token: 0x060101E3 RID: 66019 RVA: 0x0039F305 File Offset: 0x0039D505
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("buttonOut")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the javascript function called when the mouse leaves one of the control buttons.")]
		public string OnClientButtonOut
		{
			get
			{
				return ((string)base.ViewState["OnClientButtonOut"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["OnClientButtonOut"] = value;
			}
		}

		// Token: 0x060101E4 RID: 66020 RVA: 0x0039F318 File Offset: 0x0039D518
		private string getClientID(Control namingContainer, string p)
		{
			Control control = namingContainer.FindControl(p);
			if (control == null)
			{
				return p;
			}
			return control.ClientID;
		}

		// Token: 0x060101E5 RID: 66021 RVA: 0x0039F338 File Offset: 0x0039D538
		internal void Describe(string propertyName, IScriptDescriptor descriptor, Control namingContainer)
		{
			if (this.isValueSet)
			{
				descriptor.AddProperty(propertyName, new RotatorControlButtonsConfiguration
				{
					UpButtonID = this.getClientID(namingContainer, this.UpButtonID),
					DownButtonID = this.getClientID(namingContainer, this.DownButtonID),
					LeftButtonID = this.getClientID(namingContainer, this.LeftButtonID),
					RightButtonID = this.getClientID(namingContainer, this.RightButtonID)
				});
			}
			if (!string.IsNullOrEmpty(this.OnClientButtonClick))
			{
				descriptor.AddEvent("buttonClick", this.OnClientButtonClick);
			}
			if (!string.IsNullOrEmpty(this.OnClientButtonOver))
			{
				descriptor.AddEvent("buttonOver", this.OnClientButtonOver);
			}
			if (!string.IsNullOrEmpty(this.OnClientButtonOut))
			{
				descriptor.AddEvent("buttonOut", this.OnClientButtonOut);
			}
		}
	}
}
