using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200127A RID: 4730
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class TreeListResizing : StateManager
	{
		// Token: 0x17003FA8 RID: 16296
		// (get) Token: 0x0600C53B RID: 50491 RVA: 0x002C0F14 File Offset: 0x002BF114
		// (set) Token: 0x0600C53C RID: 50492 RVA: 0x002C0F3D File Offset: 0x002BF13D
		[Description("This property is set to allow column resizing in TreeList")]
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

		// Token: 0x17003FA9 RID: 16297
		// (get) Token: 0x0600C53D RID: 50493 RVA: 0x002C0F58 File Offset: 0x002BF158
		// (set) Token: 0x0600C53E RID: 50494 RVA: 0x002C0F81 File Offset: 0x002BF181
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("This property is set to enable realtime resizing")]
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

		// Token: 0x17003FAA RID: 16298
		// (get) Token: 0x0600C53F RID: 50495 RVA: 0x002C0F9C File Offset: 0x002BF19C
		// (set) Token: 0x0600C540 RID: 50496 RVA: 0x002C0FC5 File Offset: 0x002BF1C5
		[NotifyParentProperty(true)]
		[DefaultValue(TreeListResizeMode.NoScroll)]
		[Category("Client")]
		[Description("This property sets the ResizeMode of the treeList")]
		public virtual TreeListResizeMode ResizeMode
		{
			get
			{
				object obj = base.ViewState["ResizeMode"];
				if (obj != null)
				{
					return (TreeListResizeMode)obj;
				}
				return TreeListResizeMode.NoScroll;
			}
			set
			{
				base.ViewState["ResizeMode"] = value;
			}
		}
	}
}
