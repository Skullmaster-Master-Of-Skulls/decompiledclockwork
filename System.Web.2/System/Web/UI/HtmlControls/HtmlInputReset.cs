using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000354 RID: 852
	[DefaultEvent("")]
	[SupportsEventValidation]
	public class HtmlInputReset : HtmlInputButton
	{
		// Token: 0x06002738 RID: 10040 RVA: 0x0007FCCF File Offset: 0x0007DECF
		public HtmlInputReset() : base("reset")
		{
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x0007FCDC File Offset: 0x0007DEDC
		public HtmlInputReset(string type) : base(type)
		{
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x0600273A RID: 10042 RVA: 0x0007FCE5 File Offset: 0x0007DEE5
		// (set) Token: 0x0600273B RID: 10043 RVA: 0x0007FCED File Offset: 0x0007DEED
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool CausesValidation
		{
			get
			{
				return base.CausesValidation;
			}
			set
			{
				base.CausesValidation = value;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x0600273C RID: 10044 RVA: 0x0007FCF6 File Offset: 0x0007DEF6
		// (set) Token: 0x0600273D RID: 10045 RVA: 0x0007FCFE File Offset: 0x0007DEFE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ValidationGroup
		{
			get
			{
				return base.ValidationGroup;
			}
			set
			{
				base.ValidationGroup = value;
			}
		}

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x0600273E RID: 10046 RVA: 0x0007FD07 File Offset: 0x0007DF07
		// (remove) Token: 0x0600273F RID: 10047 RVA: 0x0007FD10 File Offset: 0x0007DF10
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ServerClick
		{
			add
			{
				base.ServerClick += value;
			}
			remove
			{
				base.ServerClick -= value;
			}
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x00006164 File Offset: 0x00004364
		internal override void RenderAttributesInternal(HtmlTextWriter writer)
		{
		}
	}
}
