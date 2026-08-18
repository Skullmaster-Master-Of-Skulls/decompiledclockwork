using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000447 RID: 1095
	public class MapClientEvents : StateManager, IDefaultCheck
	{
		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x000800B9 File Offset: 0x0007E2B9
		// (set) Token: 0x0600276A RID: 10090 RVA: 0x000800D9 File Offset: 0x0007E2D9
		[Description("Specifies the client-side script that executes when a RadMap ClientInitialize event is raised.")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("initialize")]
		[DefaultValue("")]
		public string OnInitialize
		{
			get
			{
				return (string)(base.ViewState["OnInitialize"] ?? "");
			}
			set
			{
				base.ViewState["OnInitialize"] = value;
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x0600276B RID: 10091 RVA: 0x000800EC File Offset: 0x0007E2EC
		// (set) Token: 0x0600276C RID: 10092 RVA: 0x0008010C File Offset: 0x0007E30C
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("load")]
		[Category("Client-side events")]
		[Description("Specifies the client-side script that executes when a RadMap ClientLoad event is raised.")]
		public string OnLoad
		{
			get
			{
				return (string)(base.ViewState["OnLoad"] ?? "");
			}
			set
			{
				base.ViewState["OnLoad"] = value;
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x0600276D RID: 10093 RVA: 0x0008011F File Offset: 0x0007E31F
		// (set) Token: 0x0600276E RID: 10094 RVA: 0x0008013F File Offset: 0x0007E33F
		[DefaultValue("")]
		public string OnBeforeReset
		{
			get
			{
				return (string)(base.ViewState["OnBeforeReset"] ?? "");
			}
			set
			{
				base.ViewState["OnBeforeReset"] = value;
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x00080152 File Offset: 0x0007E352
		// (set) Token: 0x06002770 RID: 10096 RVA: 0x00080172 File Offset: 0x0007E372
		[DefaultValue("")]
		public string OnClick
		{
			get
			{
				return (string)(base.ViewState["OnClick"] ?? "");
			}
			set
			{
				base.ViewState["OnClick"] = value;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06002771 RID: 10097 RVA: 0x00080185 File Offset: 0x0007E385
		// (set) Token: 0x06002772 RID: 10098 RVA: 0x000801A5 File Offset: 0x0007E3A5
		[DefaultValue("")]
		public string OnMarkerActivate
		{
			get
			{
				return (string)(base.ViewState["OnMarkerActivate"] ?? "");
			}
			set
			{
				base.ViewState["OnMarkerActivate"] = value;
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06002773 RID: 10099 RVA: 0x000801B8 File Offset: 0x0007E3B8
		// (set) Token: 0x06002774 RID: 10100 RVA: 0x000801D8 File Offset: 0x0007E3D8
		[DefaultValue("")]
		public string OnMarkerCreated
		{
			get
			{
				return (string)(base.ViewState["OnMarkerCreated"] ?? "");
			}
			set
			{
				base.ViewState["OnMarkerCreated"] = value;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06002775 RID: 10101 RVA: 0x000801EB File Offset: 0x0007E3EB
		// (set) Token: 0x06002776 RID: 10102 RVA: 0x0008020B File Offset: 0x0007E40B
		[DefaultValue("")]
		public string OnMarkerClick
		{
			get
			{
				return (string)(base.ViewState["OnMarkerClick"] ?? "");
			}
			set
			{
				base.ViewState["OnMarkerClick"] = value;
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06002777 RID: 10103 RVA: 0x0008021E File Offset: 0x0007E41E
		// (set) Token: 0x06002778 RID: 10104 RVA: 0x0008023E File Offset: 0x0007E43E
		[DefaultValue("")]
		public string OnPan
		{
			get
			{
				return (string)(base.ViewState["OnPan"] ?? "");
			}
			set
			{
				base.ViewState["OnPan"] = value;
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06002779 RID: 10105 RVA: 0x00080251 File Offset: 0x0007E451
		// (set) Token: 0x0600277A RID: 10106 RVA: 0x00080271 File Offset: 0x0007E471
		[DefaultValue("")]
		public string OnPanEnd
		{
			get
			{
				return (string)(base.ViewState["OnPanEnd"] ?? "");
			}
			set
			{
				base.ViewState["OnPanEnd"] = value;
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x0600277B RID: 10107 RVA: 0x00080284 File Offset: 0x0007E484
		// (set) Token: 0x0600277C RID: 10108 RVA: 0x000802A4 File Offset: 0x0007E4A4
		[DefaultValue("")]
		public string OnReset
		{
			get
			{
				return (string)(base.ViewState["OnReset"] ?? "");
			}
			set
			{
				base.ViewState["OnReset"] = value;
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x0600277D RID: 10109 RVA: 0x000802B7 File Offset: 0x0007E4B7
		// (set) Token: 0x0600277E RID: 10110 RVA: 0x000802D7 File Offset: 0x0007E4D7
		[DefaultValue("")]
		public string OnShapeClick
		{
			get
			{
				return (string)(base.ViewState["OnShapeClick"] ?? "");
			}
			set
			{
				base.ViewState["OnShapeClick"] = value;
			}
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x0600277F RID: 10111 RVA: 0x000802EA File Offset: 0x0007E4EA
		// (set) Token: 0x06002780 RID: 10112 RVA: 0x0008030A File Offset: 0x0007E50A
		[DefaultValue("")]
		public string OnShapeCreated
		{
			get
			{
				return (string)(base.ViewState["OnShapeCreated"] ?? "");
			}
			set
			{
				base.ViewState["OnShapeCreated"] = value;
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x0008031D File Offset: 0x0007E51D
		// (set) Token: 0x06002782 RID: 10114 RVA: 0x0008033D File Offset: 0x0007E53D
		[DefaultValue("")]
		public string OnShapeFeatureCreated
		{
			get
			{
				return (string)(base.ViewState["OnShapeFeatureCreated"] ?? "");
			}
			set
			{
				base.ViewState["OnShapeFeatureCreated"] = value;
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06002783 RID: 10115 RVA: 0x00080350 File Offset: 0x0007E550
		// (set) Token: 0x06002784 RID: 10116 RVA: 0x00080370 File Offset: 0x0007E570
		[DefaultValue("")]
		public string OnShapeMouseEnter
		{
			get
			{
				return (string)(base.ViewState["OnShapeMouseEnter"] ?? "");
			}
			set
			{
				base.ViewState["OnShapeMouseEnter"] = value;
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06002785 RID: 10117 RVA: 0x00080383 File Offset: 0x0007E583
		// (set) Token: 0x06002786 RID: 10118 RVA: 0x000803A3 File Offset: 0x0007E5A3
		[DefaultValue("")]
		public string OnShapeMouseLeave
		{
			get
			{
				return (string)(base.ViewState["OnShapeMouseLeave"] ?? "");
			}
			set
			{
				base.ViewState["OnShapeMouseLeave"] = value;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06002787 RID: 10119 RVA: 0x000803B6 File Offset: 0x0007E5B6
		// (set) Token: 0x06002788 RID: 10120 RVA: 0x000803D6 File Offset: 0x0007E5D6
		[DefaultValue("")]
		public string OnZoomStart
		{
			get
			{
				return (string)(base.ViewState["OnZoomStart"] ?? "");
			}
			set
			{
				base.ViewState["OnZoomStart"] = value;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06002789 RID: 10121 RVA: 0x000803E9 File Offset: 0x0007E5E9
		// (set) Token: 0x0600278A RID: 10122 RVA: 0x00080409 File Offset: 0x0007E609
		[DefaultValue("")]
		public string OnZoomEnd
		{
			get
			{
				return (string)(base.ViewState["OnZoomEnd"] ?? "");
			}
			set
			{
				base.ViewState["OnZoomEnd"] = value;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x0008041C File Offset: 0x0007E61C
		public bool IsDefault
		{
			get
			{
				return this.OnBeforeReset == "" && this.OnClick == "" && this.OnMarkerActivate == "" && this.OnMarkerCreated == "" && this.OnMarkerClick == "" && this.OnPan == "" && this.OnPanEnd == "" && this.OnReset == "" && this.OnShapeClick == "" && this.OnShapeCreated == "" && this.OnShapeFeatureCreated == "" && this.OnShapeMouseEnter == "" && this.OnShapeMouseLeave == "" && this.OnZoomStart == "" && this.OnZoomEnd == "";
			}
		}
	}
}
