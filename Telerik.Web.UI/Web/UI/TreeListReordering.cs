using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001278 RID: 4728
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class TreeListReordering : StateManager
	{
		// Token: 0x17003FA5 RID: 16293
		// (get) Token: 0x0600C534 RID: 50484 RVA: 0x002C0E40 File Offset: 0x002BF040
		// (set) Token: 0x0600C535 RID: 50485 RVA: 0x002C0E69 File Offset: 0x002BF069
		[Category("Client")]
		[Description("Gets or sets a value indicating whether column reodering is allowed.")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool AllowColumnsReorder
		{
			get
			{
				object obj = base.ViewState["AllowColumnsReorder"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowColumnsReorder"] = value;
			}
		}

		// Token: 0x17003FA6 RID: 16294
		// (get) Token: 0x0600C536 RID: 50486 RVA: 0x002C0E84 File Offset: 0x002BF084
		// (set) Token: 0x0600C537 RID: 50487 RVA: 0x002C0EAD File Offset: 0x002BF0AD
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("Gets or sets a value indicating whether columns will be reordered on the client.")]
		[Category("Client")]
		public virtual bool ReorderColumnsOnClient
		{
			get
			{
				object obj = base.ViewState["ReorderColumnsOnClient"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["ReorderColumnsOnClient"] = value;
			}
		}

		// Token: 0x17003FA7 RID: 16295
		// (get) Token: 0x0600C538 RID: 50488 RVA: 0x002C0EC8 File Offset: 0x002BF0C8
		// (set) Token: 0x0600C539 RID: 50489 RVA: 0x002C0EF1 File Offset: 0x002BF0F1
		[DefaultValue(typeof(TreeListColumnsReorderMethod), "Swap")]
		[Description("ColumnsReorderMethod")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual TreeListColumnsReorderMethod ColumnsReorderMethod
		{
			get
			{
				object obj = base.ViewState["ColumnsReorderMethod"];
				if (obj != null)
				{
					return (TreeListColumnsReorderMethod)obj;
				}
				return TreeListColumnsReorderMethod.Swap;
			}
			set
			{
				base.ViewState["ColumnsReorderMethod"] = value;
			}
		}
	}
}
