using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x02000259 RID: 601
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ProvideProperty("FlowBreak", typeof(Control))]
	[DefaultProperty("FlowDirection")]
	[Designer("System.Windows.Forms.Design.FlowLayoutPanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Docking(DockingBehavior.Ask)]
	[SRDescription("DescriptionFlowLayoutPanel")]
	public class FlowLayoutPanel : Panel, IExtenderProvider
	{
		// Token: 0x060025BD RID: 9661 RVA: 0x000AFBDC File Offset: 0x000ADDDC
		public FlowLayoutPanel()
		{
			this._flowLayoutSettings = FlowLayout.CreateSettings(this);
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060025BE RID: 9662 RVA: 0x000AFBF0 File Offset: 0x000ADDF0
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return FlowLayout.Instance;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060025BF RID: 9663 RVA: 0x000AFBF7 File Offset: 0x000ADDF7
		// (set) Token: 0x060025C0 RID: 9664 RVA: 0x000AFC04 File Offset: 0x000ADE04
		[SRDescription("FlowPanelFlowDirectionDescr")]
		[DefaultValue(FlowDirection.LeftToRight)]
		[SRCategory("CatLayout")]
		[Localizable(true)]
		public FlowDirection FlowDirection
		{
			get
			{
				return this._flowLayoutSettings.FlowDirection;
			}
			set
			{
				this._flowLayoutSettings.FlowDirection = value;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060025C1 RID: 9665 RVA: 0x000AFC12 File Offset: 0x000ADE12
		// (set) Token: 0x060025C2 RID: 9666 RVA: 0x000AFC1F File Offset: 0x000ADE1F
		[SRDescription("FlowPanelWrapContentsDescr")]
		[DefaultValue(true)]
		[SRCategory("CatLayout")]
		[Localizable(true)]
		public bool WrapContents
		{
			get
			{
				return this._flowLayoutSettings.WrapContents;
			}
			set
			{
				this._flowLayoutSettings.WrapContents = value;
			}
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x000AFC30 File Offset: 0x000ADE30
		bool IExtenderProvider.CanExtend(object obj)
		{
			Control control = obj as Control;
			return control != null && control.Parent == this;
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x000AFC52 File Offset: 0x000ADE52
		[DefaultValue(false)]
		[DisplayName("FlowBreak")]
		public bool GetFlowBreak(Control control)
		{
			return this._flowLayoutSettings.GetFlowBreak(control);
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x000AFC60 File Offset: 0x000ADE60
		[DisplayName("FlowBreak")]
		public void SetFlowBreak(Control control, bool value)
		{
			this._flowLayoutSettings.SetFlowBreak(control, value);
		}

		// Token: 0x04000FBA RID: 4026
		private FlowLayoutSettings _flowLayoutSettings;
	}
}
