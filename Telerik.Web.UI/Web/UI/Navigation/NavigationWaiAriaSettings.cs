using System;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI.Navigation
{
	// Token: 0x0200061A RID: 1562
	public class NavigationWaiAriaSettings : WaiAriaSettings
	{
		// Token: 0x06003892 RID: 14482 RVA: 0x000BA3EA File Offset: 0x000B85EA
		public NavigationWaiAriaSettings()
		{
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000BA3F2 File Offset: 0x000B85F2
		public NavigationWaiAriaSettings(JavaScriptConverter[] converters) : base(converters)
		{
		}

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06003894 RID: 14484 RVA: 0x000BA3FC File Offset: 0x000B85FC
		[MergableProperty(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets the object that controls the Wai-Aria settings applied on the MenuButton's element.")]
		public WaiAriaSettings MenuButton
		{
			get
			{
				WaiAriaSettings result;
				if ((result = this._menuButtonAriaSettings) == null)
				{
					result = (this._menuButtonAriaSettings = new WaiAriaSettings());
				}
				return result;
			}
		}

		// Token: 0x04000F09 RID: 3849
		private WaiAriaSettings _menuButtonAriaSettings;
	}
}
