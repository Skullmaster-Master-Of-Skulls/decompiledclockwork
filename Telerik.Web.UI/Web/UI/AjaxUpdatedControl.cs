using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000FCA RID: 4042
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class AjaxUpdatedControl
	{
		// Token: 0x06009CE4 RID: 40164 RVA: 0x0022EE68 File Offset: 0x0022D068
		public AjaxUpdatedControl(string ctrlID, string loadPID)
		{
			this.controlID = ctrlID;
			this.loadingPanelID = loadPID;
		}

		// Token: 0x170031AD RID: 12717
		// (get) Token: 0x06009CE5 RID: 40165 RVA: 0x0022EE94 File Offset: 0x0022D094
		// (set) Token: 0x06009CE6 RID: 40166 RVA: 0x0022EE9C File Offset: 0x0022D09C
		internal AjaxSetting OwnerSetting { get; set; }

		// Token: 0x06009CE7 RID: 40167 RVA: 0x0022EEA8 File Offset: 0x0022D0A8
		public override bool Equals(object other)
		{
			AjaxUpdatedControl ajaxUpdatedControl = other as AjaxUpdatedControl;
			if (ajaxUpdatedControl != null)
			{
				return ajaxUpdatedControl.ControlID.Equals(this.ControlID);
			}
			return base.Equals(other);
		}

		// Token: 0x06009CE8 RID: 40168 RVA: 0x0022EED8 File Offset: 0x0022D0D8
		public override int GetHashCode()
		{
			return this.ControlID.GetHashCode();
		}

		// Token: 0x06009CE9 RID: 40169 RVA: 0x0022EEE5 File Offset: 0x0022D0E5
		public AjaxUpdatedControl()
		{
		}

		// Token: 0x06009CEA RID: 40170 RVA: 0x0022EF04 File Offset: 0x0022D104
		internal string SerializeToJavascript(RadAjaxManager manager)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			stringBuilder.AppendFormat("ControlID:\"{0}\",", manager.ResolveClientID(this.ControlID));
			stringBuilder.AppendFormat("PanelID:\"{0}\"", manager.ResolveClientID(this.LoadingPanelID));
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x170031AE RID: 12718
		// (get) Token: 0x06009CEB RID: 40171 RVA: 0x0022EF65 File Offset: 0x0022D165
		// (set) Token: 0x06009CEC RID: 40172 RVA: 0x0022EF6D File Offset: 0x0022D16D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string ControlID
		{
			get
			{
				return this.controlID;
			}
			set
			{
				this.controlID = value;
			}
		}

		// Token: 0x170031AF RID: 12719
		// (get) Token: 0x06009CED RID: 40173 RVA: 0x0022EF76 File Offset: 0x0022D176
		// (set) Token: 0x06009CEE RID: 40174 RVA: 0x0022EF7E File Offset: 0x0022D17E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[TypeConverter("Telerik.Web.Design.AjaxLoadingPanelIDConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		public string LoadingPanelID
		{
			get
			{
				return this.loadingPanelID;
			}
			set
			{
				this.loadingPanelID = value;
			}
		}

		// Token: 0x170031B0 RID: 12720
		// (get) Token: 0x06009CEF RID: 40175 RVA: 0x0022EF87 File Offset: 0x0022D187
		// (set) Token: 0x06009CF0 RID: 40176 RVA: 0x0022EF8F File Offset: 0x0022D18F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		[Description("Height which will be set to the generated UpdatePanel")]
		public Unit UpdatePanelHeight { get; set; }

		// Token: 0x170031B1 RID: 12721
		// (get) Token: 0x06009CF1 RID: 40177 RVA: 0x0022EF98 File Offset: 0x0022D198
		// (set) Token: 0x06009CF2 RID: 40178 RVA: 0x0022EFA0 File Offset: 0x0022D1A0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("")]
		[Description("Set class attribute to UpdatePanel that will wrap the UpdatedControl")]
		public string UpdatePanelCssClass { get; set; }

		// Token: 0x170031B2 RID: 12722
		// (get) Token: 0x06009CF3 RID: 40179 RVA: 0x0022EFA9 File Offset: 0x0022D1A9
		// (set) Token: 0x06009CF4 RID: 40180 RVA: 0x0022EFB1 File Offset: 0x0022D1B1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the render mode of the the RadAjaxPanel. The default value is Block.")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(UpdatePanelRenderMode), "Block")]
		public UpdatePanelRenderMode UpdatePanelRenderMode
		{
			get
			{
				return this.updatePanelRenderMode;
			}
			set
			{
				this.updatePanelRenderMode = value;
			}
		}

		// Token: 0x170031B3 RID: 12723
		// (get) Token: 0x06009CF5 RID: 40181 RVA: 0x0022EFBA File Offset: 0x0022D1BA
		// (set) Token: 0x06009CF6 RID: 40182 RVA: 0x0022EFC2 File Offset: 0x0022D1C2
		internal ISite Site
		{
			get
			{
				return this.site;
			}
			set
			{
				this.site = value;
			}
		}

		// Token: 0x04002C2F RID: 11311
		private string controlID = string.Empty;

		// Token: 0x04002C30 RID: 11312
		private string loadingPanelID = string.Empty;

		// Token: 0x04002C31 RID: 11313
		private UpdatePanelRenderMode updatePanelRenderMode;

		// Token: 0x04002C32 RID: 11314
		private ISite site;
	}
}
