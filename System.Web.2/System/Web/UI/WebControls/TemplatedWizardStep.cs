using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004F3 RID: 1267
	[Bindable(false)]
	[ControlBuilder(typeof(WizardStepControlBuilder))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[ToolboxItem(false)]
	[Themeable(true)]
	public class TemplatedWizardStep : WizardStepBase
	{
		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06003F14 RID: 16148 RVA: 0x000CAC1F File Offset: 0x000C8E1F
		// (set) Token: 0x06003F15 RID: 16149 RVA: 0x000CAC27 File Offset: 0x000C8E27
		[Browsable(false)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard))]
		[WebSysDescription("TemplatedWizardStep_ContentTemplate")]
		public virtual ITemplate ContentTemplate
		{
			get
			{
				return this._contentTemplate;
			}
			set
			{
				this._contentTemplate = value;
				if (this.Owner != null && base.ControlState > ControlState.Constructed)
				{
					this.Owner.RequiresControlsRecreation();
				}
			}
		}

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06003F16 RID: 16150 RVA: 0x000CAC4C File Offset: 0x000C8E4C
		// (set) Token: 0x06003F17 RID: 16151 RVA: 0x000CAC54 File Offset: 0x000C8E54
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control ContentTemplateContainer
		{
			get
			{
				return this._contentContainer;
			}
			internal set
			{
				this._contentContainer = value;
			}
		}

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06003F18 RID: 16152 RVA: 0x000CAC5D File Offset: 0x000C8E5D
		// (set) Token: 0x06003F19 RID: 16153 RVA: 0x000CAC65 File Offset: 0x000C8E65
		[Browsable(false)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard))]
		[WebSysDescription("TemplatedWizardStep_CustomNavigationTemplate")]
		public virtual ITemplate CustomNavigationTemplate
		{
			get
			{
				return this._navigationTemplate;
			}
			set
			{
				this._navigationTemplate = value;
				if (this.Owner != null && base.ControlState > ControlState.Constructed)
				{
					this.Owner.RequiresControlsRecreation();
				}
			}
		}

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06003F1A RID: 16154 RVA: 0x000CAC8A File Offset: 0x000C8E8A
		// (set) Token: 0x06003F1B RID: 16155 RVA: 0x000CAC92 File Offset: 0x000C8E92
		[Browsable(false)]
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control CustomNavigationTemplateContainer
		{
			get
			{
				return this._navigationContainer;
			}
			internal set
			{
				this._navigationContainer = value;
			}
		}

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06003F1C RID: 16156 RVA: 0x000B11D8 File Offset: 0x000AF3D8
		// (set) Token: 0x06003F1D RID: 16157 RVA: 0x000B11E0 File Offset: 0x000AF3E0
		[Browsable(true)]
		public override string SkinID
		{
			get
			{
				return base.SkinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		// Token: 0x0400242D RID: 9261
		private ITemplate _contentTemplate;

		// Token: 0x0400242E RID: 9262
		private Control _contentContainer;

		// Token: 0x0400242F RID: 9263
		private ITemplate _navigationTemplate;

		// Token: 0x04002430 RID: 9264
		private Control _navigationContainer;
	}
}
