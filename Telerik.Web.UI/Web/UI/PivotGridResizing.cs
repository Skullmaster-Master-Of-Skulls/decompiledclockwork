using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000770 RID: 1904
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PivotGridResizing : StateManager
	{
		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x0600432E RID: 17198 RVA: 0x000D22E4 File Offset: 0x000D04E4
		// (set) Token: 0x0600432F RID: 17199 RVA: 0x000D230D File Offset: 0x000D050D
		[Description("This property is set to allow column resizing in PivotGrid")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual bool AllowColumnResize
		{
			get
			{
				object obj = base.ViewState["AllowColumnResize"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowColumnResize"] = value;
			}
		}

		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x06004330 RID: 17200 RVA: 0x000D2328 File Offset: 0x000D0528
		// (set) Token: 0x06004331 RID: 17201 RVA: 0x000D2351 File Offset: 0x000D0551
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("This property is set to enable realtime resizing")]
		[DefaultValue(false)]
		public virtual bool EnableRealTimeResize
		{
			get
			{
				object obj = base.ViewState["EnableRealTimeResize"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["EnableRealTimeResize"] = value;
			}
		}

		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x06004332 RID: 17202 RVA: 0x000D236C File Offset: 0x000D056C
		// (set) Token: 0x06004333 RID: 17203 RVA: 0x000D2395 File Offset: 0x000D0595
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("Gets or sets a value determining whether the RadPivotGrid html element will be resized during column resizing.")]
		private bool ResizePivotGridOnColumnResize
		{
			get
			{
				object obj = base.ViewState["ResizePivotGridOnColumnResize"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["ResizePivotGridOnColumnResize"] = value;
			}
		}
	}
}
