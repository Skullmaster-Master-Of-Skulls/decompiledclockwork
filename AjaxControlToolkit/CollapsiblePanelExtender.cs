using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x0200006A RID: 106
	[Designer(typeof(CollapsiblePanelExtenderDesigner))]
	[ClientScriptResource("Sys.Extended.UI.CollapsiblePanelBehavior", "CollapsiblePanel")]
	[ToolboxBitmap(typeof(Accessor), "CollapsiblePanel.bmp")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[RequiredScript(typeof(AnimationScripts))]
	[TargetControlType(typeof(Panel))]
	[DefaultProperty("CollapseControlID")]
	public class CollapsiblePanelExtender : ExtenderControlBase
	{
		// Token: 0x06000389 RID: 905 RVA: 0x0000AE9B File Offset: 0x0000909B
		public CollapsiblePanelExtender()
		{
			base.ClientStateValuesLoaded += this.CollapsiblePanelExtender_ClientStateValuesLoaded;
			base.EnableClientState = true;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000AEBC File Offset: 0x000090BC
		// (set) Token: 0x0600038B RID: 907 RVA: 0x0000AECE File Offset: 0x000090CE
		[IDReferenceProperty(typeof(WebControl))]
		[ExtenderControlProperty]
		[ClientPropertyName("collapseControlID")]
		[DefaultValue("")]
		public string CollapseControlID
		{
			get
			{
				return base.GetPropertyValue<string>("CollapseControlID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CollapseControlID", value);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000AEDC File Offset: 0x000090DC
		// (set) Token: 0x0600038D RID: 909 RVA: 0x0000AEEE File Offset: 0x000090EE
		[ClientPropertyName("expandControlID")]
		[ExtenderControlProperty]
		[IDReferenceProperty(typeof(WebControl))]
		[DefaultValue("")]
		public string ExpandControlID
		{
			get
			{
				return base.GetPropertyValue<string>("ExpandControlID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("ExpandControlID", value);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000AEFC File Offset: 0x000090FC
		// (set) Token: 0x0600038F RID: 911 RVA: 0x0000AF0A File Offset: 0x0000910A
		[DefaultValue(false)]
		[ClientPropertyName("autoCollapse")]
		[ExtenderControlProperty]
		public bool AutoCollapse
		{
			get
			{
				return base.GetPropertyValue<bool>("AutoCollapse", false);
			}
			set
			{
				base.SetPropertyValue<bool>("AutoCollapse", value);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000AF18 File Offset: 0x00009118
		// (set) Token: 0x06000391 RID: 913 RVA: 0x0000AF26 File Offset: 0x00009126
		[ClientPropertyName("autoExpand")]
		[ExtenderControlProperty]
		[DefaultValue(false)]
		public bool AutoExpand
		{
			get
			{
				return base.GetPropertyValue<bool>("AutoExpand", false);
			}
			set
			{
				base.SetPropertyValue<bool>("AutoExpand", value);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000AF34 File Offset: 0x00009134
		// (set) Token: 0x06000393 RID: 915 RVA: 0x0000AF42 File Offset: 0x00009142
		[DefaultValue(-1)]
		[ExtenderControlProperty]
		[ClientPropertyName("collapsedSize")]
		public int CollapsedSize
		{
			get
			{
				return base.GetPropertyValue<int>("CollapseHeight", -1);
			}
			set
			{
				base.SetPropertyValue<int>("CollapseHeight", value);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000AF50 File Offset: 0x00009150
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0000AF5E File Offset: 0x0000915E
		[DefaultValue(-1)]
		[ExtenderControlProperty]
		public int ExpandedSize
		{
			get
			{
				return base.GetPropertyValue<int>("ExpandedSize", -1);
			}
			set
			{
				base.SetPropertyValue<int>("ExpandedSize", value);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000AF6C File Offset: 0x0000916C
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0000AF7A File Offset: 0x0000917A
		[DefaultValue(false)]
		[ExtenderControlProperty]
		[ClientPropertyName("scrollContents")]
		public bool ScrollContents
		{
			get
			{
				return base.GetPropertyValue<bool>("ScrollContents", false);
			}
			set
			{
				base.SetPropertyValue<bool>("ScrollContents", value);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000AF88 File Offset: 0x00009188
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0000AF96 File Offset: 0x00009196
		[ExtenderControlProperty]
		[DefaultValue(false)]
		[ClientPropertyName("suppressPostBack")]
		public bool SuppressPostBack
		{
			get
			{
				return base.GetPropertyValue<bool>("SuppressPostBack", false);
			}
			set
			{
				base.SetPropertyValue<bool>("SuppressPostBack", value);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000AFA4 File Offset: 0x000091A4
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0000AFB2 File Offset: 0x000091B2
		[ExtenderControlProperty]
		[ClientPropertyName("collapsed")]
		[DefaultValue(false)]
		public bool Collapsed
		{
			get
			{
				return base.GetPropertyValue<bool>("Collapsed", false);
			}
			set
			{
				base.SetPropertyValue<bool>("Collapsed", value);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000AFC0 File Offset: 0x000091C0
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0000AFD2 File Offset: 0x000091D2
		[ExtenderControlProperty]
		[DefaultValue("")]
		[ClientPropertyName("collapsedText")]
		public string CollapsedText
		{
			get
			{
				return base.GetPropertyValue<string>("CollapsedText", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CollapsedText", value);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000AFE0 File Offset: 0x000091E0
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0000AFF2 File Offset: 0x000091F2
		[ClientPropertyName("expandedText")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		public string ExpandedText
		{
			get
			{
				return base.GetPropertyValue<string>("ExpandedText", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("ExpandedText", value);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000B000 File Offset: 0x00009200
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0000B012 File Offset: 0x00009212
		[IDReferenceProperty(typeof(Label))]
		[DefaultValue("")]
		[ClientPropertyName("textLabelID")]
		[ExtenderControlProperty]
		public string TextLabelID
		{
			get
			{
				return base.GetPropertyValue<string>("TextLabelID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("TextLabelID", value);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000B020 File Offset: 0x00009220
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x0000B032 File Offset: 0x00009232
		[ExtenderControlProperty]
		[ClientPropertyName("expandedImage")]
		[UrlProperty]
		[DefaultValue("")]
		public string ExpandedImage
		{
			get
			{
				return base.GetPropertyValue<string>("ExpandedImage", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("ExpandedImage", value);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000B040 File Offset: 0x00009240
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x0000B052 File Offset: 0x00009252
		[DefaultValue("")]
		[UrlProperty]
		[ExtenderControlProperty]
		[ClientPropertyName("collapsedImage")]
		public string CollapsedImage
		{
			get
			{
				return base.GetPropertyValue<string>("CollapsedImage", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("CollapsedImage", value);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000B060 File Offset: 0x00009260
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x0000B072 File Offset: 0x00009272
		[IDReferenceProperty(typeof(System.Web.UI.WebControls.Image))]
		[ExtenderControlProperty]
		[ClientPropertyName("imageControlID")]
		[DefaultValue("")]
		public string ImageControlID
		{
			get
			{
				return base.GetPropertyValue<string>("ImageControlID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("ImageControlID", value);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000B080 File Offset: 0x00009280
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x0000B08E File Offset: 0x0000928E
		[DefaultValue(CollapsiblePanelExpandDirection.Vertical)]
		[ClientPropertyName("expandDirection")]
		[ExtenderControlProperty]
		public CollapsiblePanelExpandDirection ExpandDirection
		{
			get
			{
				return base.GetPropertyValue<CollapsiblePanelExpandDirection>("ExpandDirection", CollapsiblePanelExpandDirection.Vertical);
			}
			set
			{
				base.SetPropertyValue<CollapsiblePanelExpandDirection>("ExpandDirection", value);
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000B09C File Offset: 0x0000929C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void EnsureValid()
		{
			base.EnsureValid();
			if ((this.ExpandedText != null || this.CollapsedText != null) && this.TextLabelID == null)
			{
				throw new ArgumentException("If CollapsedText or ExpandedText is set, TextLabelID must also be set.");
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000B0C8 File Offset: 0x000092C8
		private void CollapsiblePanelExtender_ClientStateValuesLoaded(object sender, EventArgs e)
		{
			WebControl webControl = this.FindControl(base.TargetControlID) as WebControl;
			if (webControl != null && !string.IsNullOrEmpty(base.ClientState))
			{
				bool flag = bool.Parse(base.ClientState);
				if (flag)
				{
					webControl.Style["display"] = "none";
					return;
				}
				webControl.Style["display"] = string.Empty;
			}
		}
	}
}
