using System;
using System.ComponentModel;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000BAC RID: 2988
	public class ImageManagerConfiguration : FileManagerDialogConfiguration
	{
		// Token: 0x170024CE RID: 9422
		// (get) Token: 0x06007095 RID: 28821 RVA: 0x001A447D File Offset: 0x001A267D
		// (set) Token: 0x06007096 RID: 28822 RVA: 0x001A449E File Offset: 0x001A269E
		[DefaultValue(false)]
		[Description("Gets or sets a bool value that indicates whether the ImageEditor uses the specified ContentProvider to load and save the edited image.")]
		public bool EnableContentProvider
		{
			get
			{
				return (bool)(base.ViewState["EnableContentProvider"] ?? false);
			}
			set
			{
				base.ViewState["EnableContentProvider"] = value;
			}
		}

		// Token: 0x170024CF RID: 9423
		// (get) Token: 0x06007097 RID: 28823 RVA: 0x001A44B6 File Offset: 0x001A26B6
		// (set) Token: 0x06007098 RID: 28824 RVA: 0x001A44D6 File Offset: 0x001A26D6
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the fully qualified type name of the ICacheImageProvider used by the Telerik.Web.UI.RadImageEditor instance (to store the intermediary EditableImage objects")]
		public string ImageProviderTypeName
		{
			get
			{
				return ((string)base.ViewState["ImageProviderTypeName"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["ImageProviderTypeName"] = value;
				this._iCacheImageProviderType = null;
			}
		}

		// Token: 0x170024D0 RID: 9424
		// (get) Token: 0x06007099 RID: 28825 RVA: 0x001A44F0 File Offset: 0x001A26F0
		[Browsable(false)]
		[Bindable(false)]
		[Description("This property gets the current image provider type. To set the content provider type use ImageProviderTypeName")]
		public Type ICacheImageProviderType
		{
			get
			{
				if (this._iCacheImageProviderType == null)
				{
					this._iCacheImageProviderType = RadImageEditor.GetICacheImageProviderType(this.ImageProviderTypeName);
				}
				return this._iCacheImageProviderType;
			}
		}

		// Token: 0x04001E6C RID: 7788
		private Type _iCacheImageProviderType;
	}
}
