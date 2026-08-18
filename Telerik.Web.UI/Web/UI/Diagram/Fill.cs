using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000252 RID: 594
	public class Fill : StateManager, IDefaultCheck
	{
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x0004A06A File Offset: 0x0004826A
		// (set) Token: 0x060015A8 RID: 5544 RVA: 0x0004A08A File Offset: 0x0004828A
		[DefaultValue("")]
		public string Color
		{
			get
			{
				return (string)(base.ViewState["Color"] ?? "");
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x0004A09D File Offset: 0x0004829D
		// (set) Token: 0x060015AA RID: 5546 RVA: 0x0004A0C6 File Offset: 0x000482C6
		[DefaultValue(1.0)]
		public double Opacity
		{
			get
			{
				return (double)(base.ViewState["Opacity"] ?? 1.0);
			}
			set
			{
				base.ViewState["Opacity"] = value;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x0004A0DE File Offset: 0x000482DE
		public bool IsDefault
		{
			get
			{
				return this.Color == "" && this.Opacity == 1.0;
			}
		}
	}
}
