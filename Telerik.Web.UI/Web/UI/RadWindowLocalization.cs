using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001B83 RID: 7043
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class RadWindowLocalization : StateManager
	{
		// Token: 0x17005361 RID: 21345
		// (get) Token: 0x0601110A RID: 69898 RVA: 0x003C3690 File Offset: 0x003C1890
		// (set) Token: 0x0601110B RID: 69899 RVA: 0x003C36B0 File Offset: 0x003C18B0
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Close")]
		public string Close
		{
			get
			{
				return ((string)base.ViewState["Close"]) ?? "Close";
			}
			set
			{
				base.ViewState["Close"] = value;
			}
		}

		// Token: 0x17005362 RID: 21346
		// (get) Token: 0x0601110C RID: 69900 RVA: 0x003C36C3 File Offset: 0x003C18C3
		// (set) Token: 0x0601110D RID: 69901 RVA: 0x003C36E3 File Offset: 0x003C18E3
		[DefaultValue("Maximize")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string Maximize
		{
			get
			{
				return ((string)base.ViewState["Maximize"]) ?? "Maximize";
			}
			set
			{
				base.ViewState["Maximize"] = value;
			}
		}

		// Token: 0x17005363 RID: 21347
		// (get) Token: 0x0601110E RID: 69902 RVA: 0x003C36F6 File Offset: 0x003C18F6
		// (set) Token: 0x0601110F RID: 69903 RVA: 0x003C3716 File Offset: 0x003C1916
		[Localizable(true)]
		[DefaultValue("Minimize")]
		[NotifyParentProperty(true)]
		public string Minimize
		{
			get
			{
				return ((string)base.ViewState["Minimize"]) ?? "Minimize";
			}
			set
			{
				base.ViewState["Minimize"] = value;
			}
		}

		// Token: 0x17005364 RID: 21348
		// (get) Token: 0x06011110 RID: 69904 RVA: 0x003C3729 File Offset: 0x003C1929
		// (set) Token: 0x06011111 RID: 69905 RVA: 0x003C3749 File Offset: 0x003C1949
		[DefaultValue("Reload")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string Reload
		{
			get
			{
				return ((string)base.ViewState["Reload"]) ?? "Reload";
			}
			set
			{
				base.ViewState["Reload"] = value;
			}
		}

		// Token: 0x17005365 RID: 21349
		// (get) Token: 0x06011112 RID: 69906 RVA: 0x003C375C File Offset: 0x003C195C
		// (set) Token: 0x06011113 RID: 69907 RVA: 0x003C377C File Offset: 0x003C197C
		[DefaultValue("Pin on")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string PinOn
		{
			get
			{
				return ((string)base.ViewState["PinOn"]) ?? "Pin on";
			}
			set
			{
				base.ViewState["PinOn"] = value;
			}
		}

		// Token: 0x17005366 RID: 21350
		// (get) Token: 0x06011114 RID: 69908 RVA: 0x003C378F File Offset: 0x003C198F
		// (set) Token: 0x06011115 RID: 69909 RVA: 0x003C37AF File Offset: 0x003C19AF
		[Localizable(true)]
		[DefaultValue("Pin off")]
		[NotifyParentProperty(true)]
		public string PinOff
		{
			get
			{
				return ((string)base.ViewState["PinOff"]) ?? "Pin off";
			}
			set
			{
				base.ViewState["PinOff"] = value;
			}
		}

		// Token: 0x17005367 RID: 21351
		// (get) Token: 0x06011116 RID: 69910 RVA: 0x003C37C2 File Offset: 0x003C19C2
		// (set) Token: 0x06011117 RID: 69911 RVA: 0x003C37E2 File Offset: 0x003C19E2
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Restore")]
		public string Restore
		{
			get
			{
				return ((string)base.ViewState["Restore"]) ?? "Restore";
			}
			set
			{
				base.ViewState["Restore"] = value;
			}
		}

		// Token: 0x17005368 RID: 21352
		// (get) Token: 0x06011118 RID: 69912 RVA: 0x003C37F5 File Offset: 0x003C19F5
		// (set) Token: 0x06011119 RID: 69913 RVA: 0x003C3815 File Offset: 0x003C1A15
		[DefaultValue("OK")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string OK
		{
			get
			{
				return ((string)base.ViewState["OK"]) ?? "OK";
			}
			set
			{
				base.ViewState["OK"] = value;
			}
		}

		// Token: 0x17005369 RID: 21353
		// (get) Token: 0x0601111A RID: 69914 RVA: 0x003C3828 File Offset: 0x003C1A28
		// (set) Token: 0x0601111B RID: 69915 RVA: 0x003C3848 File Offset: 0x003C1A48
		[NotifyParentProperty(true)]
		[DefaultValue("Cancel")]
		[Localizable(true)]
		public string Cancel
		{
			get
			{
				return ((string)base.ViewState["Cancel"]) ?? "Cancel";
			}
			set
			{
				base.ViewState["Cancel"] = value;
			}
		}

		// Token: 0x1700536A RID: 21354
		// (get) Token: 0x0601111C RID: 69916 RVA: 0x003C385B File Offset: 0x003C1A5B
		// (set) Token: 0x0601111D RID: 69917 RVA: 0x003C387B File Offset: 0x003C1A7B
		[Localizable(true)]
		[DefaultValue("Yes")]
		[NotifyParentProperty(true)]
		public string Yes
		{
			get
			{
				return ((string)base.ViewState["Yes"]) ?? "Yes";
			}
			set
			{
				base.ViewState["Yes"] = value;
			}
		}

		// Token: 0x1700536B RID: 21355
		// (get) Token: 0x0601111E RID: 69918 RVA: 0x003C388E File Offset: 0x003C1A8E
		// (set) Token: 0x0601111F RID: 69919 RVA: 0x003C38AE File Offset: 0x003C1AAE
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("No")]
		public string No
		{
			get
			{
				return ((string)base.ViewState["No"]) ?? "No";
			}
			set
			{
				base.ViewState["No"] = value;
			}
		}

		// Token: 0x06011120 RID: 69920 RVA: 0x003C38C4 File Offset: 0x003C1AC4
		internal bool isDefault()
		{
			return this.Close == "Close" && this.Maximize == "Maximize" && this.Minimize == "Minimize" && this.Reload == "Reload" && this.PinOn == "Pin on" && this.PinOff == "Pin off" && this.Restore == "Restore" && this.OK == "OK" && this.Cancel == "Cancel" && this.Yes == "Yes" && this.No == "No";
		}

		// Token: 0x06011121 RID: 69921 RVA: 0x003C39A0 File Offset: 0x003C1BA0
		internal Dictionary<string, string> getLocalizationStrings()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(11);
			dictionary["Close"] = this.Close;
			dictionary["Maximize"] = this.Maximize;
			dictionary["Minimize"] = this.Minimize;
			dictionary["Reload"] = this.Reload;
			dictionary["PinOn"] = this.PinOn;
			dictionary["PinOff"] = this.PinOff;
			dictionary["Restore"] = this.Restore;
			dictionary["OK"] = this.OK;
			dictionary["Cancel"] = this.Cancel;
			dictionary["Yes"] = this.Yes;
			dictionary["No"] = this.No;
			return dictionary;
		}
	}
}
