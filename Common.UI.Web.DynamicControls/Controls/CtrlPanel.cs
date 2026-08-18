using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.UI.Web.DynamicControls.Entity;

namespace TechnoPro.Common.UI.Web.DynamicControls.Controls
{
	// Token: 0x02000009 RID: 9
	[ToolboxData("<{0}:CtrlPanel runat=server></{0}:CtrlPanel>")]
	public class CtrlPanel : Panel, IDynamicWebControl, INamingContainer
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00003B76 File Offset: 0x00001D76
		public CtrlPanel()
		{
			this.EnableViewState = false;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003B85 File Offset: 0x00001D85
		public CtrlPanel(DynamicFieldDTO Field)
		{
			this.DynamicField = Field;
			this.EnableViewState = false;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003B9B File Offset: 0x00001D9B
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003BA3 File Offset: 0x00001DA3
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003BB1 File Offset: 0x00001DB1
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			base.OnInit(e);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003883 File Offset: 0x00001A83
		private void InitializeControls()
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003883 File Offset: 0x00001A83
		private void BuildControlHeiarchy()
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003BC0 File Offset: 0x00001DC0
		protected override void Render(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "cxform");
			writer.RenderBeginTag(HtmlTextWriterTag.Ol);
			base.Render(writer);
			writer.RenderEndTag();
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003BE4 File Offset: 0x00001DE4
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00003BEC File Offset: 0x00001DEC
		public DynamicFieldDTO DynamicField { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003BF5 File Offset: 0x00001DF5
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003BFD File Offset: 0x00001DFD
		public DynamicDataDTO DynamicData { get; set; }

		// Token: 0x06000085 RID: 133 RVA: 0x00003883 File Offset: 0x00001A83
		public void ChildLoadViewState(object dataFromViewState)
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003885 File Offset: 0x00001A85
		public object ChildSaveViewState()
		{
			return null;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003885 File Offset: 0x00001A85
		public string ViewStateKey
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003883 File Offset: 0x00001A83
		public void ClearData()
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003883 File Offset: 0x00001A83
		public void ShowData(DynamicDataDTO data)
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003948 File Offset: 0x00001B48
		public DynamicDataDTO GetCurrentData(out bool isEmpty)
		{
			isEmpty = true;
			return null;
		}
	}
}
