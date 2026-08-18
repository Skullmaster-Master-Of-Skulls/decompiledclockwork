using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit.Design;

namespace AjaxControlToolkit
{
	// Token: 0x02000036 RID: 54
	[Designer(typeof(ChartBaseDesigner))]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public abstract class ChartBase : ScriptControlBase
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x00006F28 File Offset: 0x00005128
		public ChartBase() : base(true, HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00006F33 File Offset: 0x00005133
		protected bool IsDesignMode
		{
			get
			{
				return HttpContext.Current == null;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00006F3D File Offset: 0x0000513D
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00006F45 File Offset: 0x00005145
		[ClientPropertyName("chartWidth")]
		[DefaultValue(null)]
		[ExtenderControlProperty]
		public string ChartWidth { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00006F4E File Offset: 0x0000514E
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00006F56 File Offset: 0x00005156
		[DefaultValue(null)]
		[ExtenderControlProperty]
		[ClientPropertyName("chartHeight")]
		public string ChartHeight { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00006F5F File Offset: 0x0000515F
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00006F67 File Offset: 0x00005167
		[DefaultValue("")]
		[ExtenderControlProperty]
		[ClientPropertyName("chartTitle")]
		public string ChartTitle { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00006F70 File Offset: 0x00005170
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00006F78 File Offset: 0x00005178
		[ClientPropertyName("chartTitleColor")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		public string ChartTitleColor { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00006F81 File Offset: 0x00005181
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00006F89 File Offset: 0x00005189
		[DefaultValue("PieChart")]
		[ClientPropertyName("theme")]
		[ExtenderControlProperty]
		public string Theme { get; set; }

		// Token: 0x060001E5 RID: 485 RVA: 0x00006F94 File Offset: 0x00005194
		protected override void CreateChildControls()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.ID = "_ParentDiv";
			htmlGenericControl.Attributes.Add("style", "border-style:solid; border-width:1px;");
			this.Controls.Add(htmlGenericControl);
		}
	}
}
