using System;
using System.Security.Permissions;

namespace System.Drawing.Design
{
	// Token: 0x02000079 RID: 121
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class PropertyValueUIItem
	{
		// Token: 0x06000860 RID: 2144 RVA: 0x00020DAB File Offset: 0x0001EFAB
		public PropertyValueUIItem(Image uiItemImage, PropertyValueUIItemInvokeHandler handler, string tooltip)
		{
			this.itemImage = uiItemImage;
			this.handler = handler;
			if (this.itemImage == null)
			{
				throw new ArgumentNullException("uiItemImage");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.tooltip = tooltip;
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00020DE9 File Offset: 0x0001EFE9
		public virtual Image Image
		{
			get
			{
				return this.itemImage;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x00020DF1 File Offset: 0x0001EFF1
		public virtual PropertyValueUIItemInvokeHandler InvokeHandler
		{
			get
			{
				return this.handler;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00020DF9 File Offset: 0x0001EFF9
		public virtual string ToolTip
		{
			get
			{
				return this.tooltip;
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00015259 File Offset: 0x00013459
		public virtual void Reset()
		{
		}

		// Token: 0x04000709 RID: 1801
		private Image itemImage;

		// Token: 0x0400070A RID: 1802
		private PropertyValueUIItemInvokeHandler handler;

		// Token: 0x0400070B RID: 1803
		private string tooltip;
	}
}
