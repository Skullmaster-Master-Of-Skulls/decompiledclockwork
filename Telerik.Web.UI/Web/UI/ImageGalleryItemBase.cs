using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020003FC RID: 1020
	public abstract class ImageGalleryItemBase : StateManager
	{
		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x0007C2BC File Offset: 0x0007A4BC
		// (set) Token: 0x06002554 RID: 9556 RVA: 0x0007C2C4 File Offset: 0x0007A4C4
		internal RadImageGallery Gallery { get; set; }

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x0007C2CD File Offset: 0x0007A4CD
		// (set) Token: 0x06002556 RID: 9558 RVA: 0x0007C2D5 File Offset: 0x0007A4D5
		internal RadLightBoxItem LightBoxItem { get; set; }

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06002557 RID: 9559
		public abstract ImageGalleryItemType Type { get; }

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06002558 RID: 9560 RVA: 0x0007C2E0 File Offset: 0x0007A4E0
		// (set) Token: 0x06002559 RID: 9561 RVA: 0x0007C30D File Offset: 0x0007A50D
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual Unit Width
		{
			get
			{
				object obj = base.ViewState["Width"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Empty;
			}
			set
			{
				base.ViewState["Width"] = value;
				if (this.LightBoxItem != null)
				{
					this.LightBoxItem.Width = value;
				}
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x0007C33C File Offset: 0x0007A53C
		// (set) Token: 0x0600255B RID: 9563 RVA: 0x0007C369 File Offset: 0x0007A569
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual Unit Height
		{
			get
			{
				object obj = base.ViewState["Height"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Empty;
			}
			set
			{
				base.ViewState["Height"] = value;
				if (this.LightBoxItem != null)
				{
					this.LightBoxItem.Height = value;
				}
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x0007C395 File Offset: 0x0007A595
		// (set) Token: 0x0600255D RID: 9565 RVA: 0x0007C3B5 File Offset: 0x0007A5B5
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string Title
		{
			get
			{
				return (base.ViewState["Title"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["Title"] = value;
				if (this.LightBoxItem != null)
				{
					this.LightBoxItem.Title = value;
				}
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x0600255E RID: 9566 RVA: 0x0007C3DC File Offset: 0x0007A5DC
		// (set) Token: 0x0600255F RID: 9567 RVA: 0x0007C3FC File Offset: 0x0007A5FC
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string Description
		{
			get
			{
				return (base.ViewState["Description"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["Description"] = value;
				if (this.LightBoxItem != null)
				{
					this.LightBoxItem.Description = value;
				}
			}
		}

		// Token: 0x06002560 RID: 9568
		internal abstract void InstantiateIn(Control control);

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06002561 RID: 9569 RVA: 0x0007C424 File Offset: 0x0007A624
		// (set) Token: 0x06002562 RID: 9570 RVA: 0x0007C44D File Offset: 0x0007A64D
		[DefaultValue(false)]
		public virtual bool PreventDefaultGestures
		{
			get
			{
				object obj = base.ViewState["PreventDefaultGestures"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["PreventDefaultGestures"] = value;
			}
		}
	}
}
