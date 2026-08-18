using System;
using System.ComponentModel;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x0200025A RID: 602
	[DefaultProperty("FlowDirection")]
	public class FlowLayoutSettings : LayoutSettings
	{
		// Token: 0x060025C6 RID: 9670 RVA: 0x000AFC6F File Offset: 0x000ADE6F
		internal FlowLayoutSettings(IArrangedElement owner) : base(owner)
		{
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x000AFBF0 File Offset: 0x000ADDF0
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return FlowLayout.Instance;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x060025C8 RID: 9672 RVA: 0x000AFC78 File Offset: 0x000ADE78
		// (set) Token: 0x060025C9 RID: 9673 RVA: 0x000AFC85 File Offset: 0x000ADE85
		[SRDescription("FlowPanelFlowDirectionDescr")]
		[DefaultValue(FlowDirection.LeftToRight)]
		[SRCategory("CatLayout")]
		public FlowDirection FlowDirection
		{
			get
			{
				return FlowLayout.GetFlowDirection(base.Owner);
			}
			set
			{
				FlowLayout.SetFlowDirection(base.Owner, value);
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x000AFC93 File Offset: 0x000ADE93
		// (set) Token: 0x060025CB RID: 9675 RVA: 0x000AFCA0 File Offset: 0x000ADEA0
		[SRDescription("FlowPanelWrapContentsDescr")]
		[DefaultValue(true)]
		[SRCategory("CatLayout")]
		public bool WrapContents
		{
			get
			{
				return FlowLayout.GetWrapContents(base.Owner);
			}
			set
			{
				FlowLayout.SetWrapContents(base.Owner, value);
			}
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x000AFCB0 File Offset: 0x000ADEB0
		public void SetFlowBreak(object child, bool value)
		{
			IArrangedElement element = FlowLayout.Instance.CastToArrangedElement(child);
			if (this.GetFlowBreak(child) != value)
			{
				CommonProperties.SetFlowBreak(element, value);
			}
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x000AFCDC File Offset: 0x000ADEDC
		public bool GetFlowBreak(object child)
		{
			IArrangedElement element = FlowLayout.Instance.CastToArrangedElement(child);
			return CommonProperties.GetFlowBreak(element);
		}
	}
}
