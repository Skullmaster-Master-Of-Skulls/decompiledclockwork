using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000563 RID: 1379
	public class LightBoxClientDataBinding : StateManager
	{
		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x060031B5 RID: 12725 RVA: 0x000A330A File Offset: 0x000A150A
		// (set) Token: 0x060031B6 RID: 12726 RVA: 0x000A332A File Offset: 0x000A152A
		[Category("Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue("")]
		[Description("Gets/sets the RadLightBox client-side binding ItemTemplate")]
		[NotifyParentProperty(true)]
		public virtual string ItemTemplate
		{
			get
			{
				return (base.ViewState["ItemTemplate"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["ItemTemplate"] = value;
			}
		}

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x060031B7 RID: 12727 RVA: 0x000A333D File Offset: 0x000A153D
		// (set) Token: 0x060031B8 RID: 12728 RVA: 0x000A335D File Offset: 0x000A155D
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue("")]
		[Description("Gets/sets the RadLightBox client-side binding ItemTemplate")]
		[Category("Data")]
		public virtual string DescriptionTemplate
		{
			get
			{
				return (base.ViewState["DescriptionTemplate"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["DescriptionTemplate"] = value;
			}
		}
	}
}
