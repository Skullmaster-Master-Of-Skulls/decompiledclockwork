using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x020004A4 RID: 1188
	[SupportsEventValidation]
	[DefaultEvent("")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputReset : HtmlInputButton
	{
		// Token: 0x06003787 RID: 14215 RVA: 0x000EE293 File Offset: 0x000ED293
		public HtmlInputReset() : base("reset")
		{
		}

		// Token: 0x06003788 RID: 14216 RVA: 0x000EE2A0 File Offset: 0x000ED2A0
		public HtmlInputReset(string type) : base(type)
		{
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06003789 RID: 14217 RVA: 0x000EE2A9 File Offset: 0x000ED2A9
		// (set) Token: 0x0600378A RID: 14218 RVA: 0x000EE2B1 File Offset: 0x000ED2B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
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

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x0600378B RID: 14219 RVA: 0x000EE2BA File Offset: 0x000ED2BA
		// (set) Token: 0x0600378C RID: 14220 RVA: 0x000EE2C2 File Offset: 0x000ED2C2
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

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x0600378D RID: 14221 RVA: 0x000EE2CB File Offset: 0x000ED2CB
		// (remove) Token: 0x0600378E RID: 14222 RVA: 0x000EE2D4 File Offset: 0x000ED2D4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
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

		// Token: 0x0600378F RID: 14223 RVA: 0x000EE2DD File Offset: 0x000ED2DD
		internal override void RenderAttributesInternal(HtmlTextWriter writer)
		{
		}
	}
}
