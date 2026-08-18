using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200127C RID: 4732
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class TreeListScrolling : StateManager
	{
		// Token: 0x17003FAB RID: 16299
		// (get) Token: 0x0600C542 RID: 50498 RVA: 0x002C0FE8 File Offset: 0x002BF1E8
		// (set) Token: 0x0600C543 RID: 50499 RVA: 0x002C1011 File Offset: 0x002BF211
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("RadTreeList_AllowScroll")]
		[Category("Client")]
		public virtual bool AllowScroll
		{
			get
			{
				object obj = base.ViewState["AllowScroll"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowScroll"] = value;
			}
		}

		// Token: 0x17003FAC RID: 16300
		// (get) Token: 0x0600C544 RID: 50500 RVA: 0x002C1029 File Offset: 0x002BF229
		internal bool ShouldSerializeAllowScroll
		{
			get
			{
				return this.AllowScroll;
			}
		}

		// Token: 0x17003FAD RID: 16301
		// (get) Token: 0x0600C545 RID: 50501 RVA: 0x002C1034 File Offset: 0x002BF234
		// (set) Token: 0x0600C546 RID: 50502 RVA: 0x002C1066 File Offset: 0x002BF266
		[DefaultValue(typeof(Unit), "300px")]
		[Description("RadTreeList_ScrollHeight")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		public virtual Unit ScrollHeight
		{
			get
			{
				object obj = base.ViewState["ScrollHeight"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Pixel(300);
			}
			set
			{
				base.ViewState["ScrollHeight"] = value;
			}
		}

		// Token: 0x17003FAE RID: 16302
		// (get) Token: 0x0600C547 RID: 50503 RVA: 0x002C1080 File Offset: 0x002BF280
		// (set) Token: 0x0600C548 RID: 50504 RVA: 0x002C10A9 File Offset: 0x002BF2A9
		[NotifyParentProperty(true)]
		[Description("RadTreeList_SaveScrollPosition")]
		[DefaultValue(true)]
		[Category("Client")]
		public virtual bool SaveScrollPosition
		{
			get
			{
				object obj = base.ViewState["SaveScrollPosition"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["SaveScrollPosition"] = value;
			}
		}

		// Token: 0x17003FAF RID: 16303
		// (get) Token: 0x0600C549 RID: 50505 RVA: 0x002C10C1 File Offset: 0x002BF2C1
		internal bool ShouldSerializeSaveScrollPosition
		{
			get
			{
				return !this.SaveScrollPosition;
			}
		}

		// Token: 0x17003FB0 RID: 16304
		// (get) Token: 0x0600C54A RID: 50506 RVA: 0x002C10CC File Offset: 0x002BF2CC
		// (set) Token: 0x0600C54B RID: 50507 RVA: 0x002C10F5 File Offset: 0x002BF2F5
		[Category("Client")]
		[Description("RadTreeList_UseStaticHeaders")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool UseStaticHeaders
		{
			get
			{
				object obj = base.ViewState["UseStaticHeaders"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["UseStaticHeaders"] = value;
			}
		}

		// Token: 0x17003FB1 RID: 16305
		// (get) Token: 0x0600C54C RID: 50508 RVA: 0x002C110D File Offset: 0x002BF30D
		internal bool ShouldSerializeUseStaticHeaders
		{
			get
			{
				return this.UseStaticHeaders;
			}
		}

		// Token: 0x17003FB2 RID: 16306
		// (get) Token: 0x0600C54D RID: 50509 RVA: 0x002C1118 File Offset: 0x002BF318
		// (set) Token: 0x0600C54E RID: 50510 RVA: 0x002C1145 File Offset: 0x002BF345
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual string ScrollTop
		{
			get
			{
				object obj = base.ViewState["ScrollTop"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["ScrollTop"] = value;
			}
		}

		// Token: 0x17003FB3 RID: 16307
		// (get) Token: 0x0600C54F RID: 50511 RVA: 0x002C1158 File Offset: 0x002BF358
		internal bool ShouldSerializeScrollTop
		{
			get
			{
				return this.SaveScrollPosition;
			}
		}

		// Token: 0x17003FB4 RID: 16308
		// (get) Token: 0x0600C550 RID: 50512 RVA: 0x002C1160 File Offset: 0x002BF360
		// (set) Token: 0x0600C551 RID: 50513 RVA: 0x002C118D File Offset: 0x002BF38D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual string ScrollLeft
		{
			get
			{
				object obj = base.ViewState["ScrollLeft"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["ScrollLeft"] = value;
			}
		}

		// Token: 0x17003FB5 RID: 16309
		// (get) Token: 0x0600C552 RID: 50514 RVA: 0x002C11A0 File Offset: 0x002BF3A0
		internal bool ShouldSerializeScrollLeft
		{
			get
			{
				return this.SaveScrollPosition;
			}
		}
	}
}
