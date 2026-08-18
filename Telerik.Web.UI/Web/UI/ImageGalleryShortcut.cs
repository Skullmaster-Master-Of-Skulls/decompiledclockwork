using System;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000543 RID: 1347
	public class ImageGalleryShortcut : KeyboardNavigationShortcut
	{
		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06002F90 RID: 12176 RVA: 0x0009BDC4 File Offset: 0x00099FC4
		// (set) Token: 0x06002F91 RID: 12177 RVA: 0x0009BDED File Offset: 0x00099FED
		[ScriptIgnore]
		public ImageGalleryShortcutCommand Command
		{
			get
			{
				object obj = base.ViewState["Command"];
				if (obj != null)
				{
					return (ImageGalleryShortcutCommand)obj;
				}
				return ImageGalleryShortcutCommand.None;
			}
			set
			{
				base.ViewState["Command"] = value;
			}
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x0009BE05 File Offset: 0x0009A005
		internal override string GetName()
		{
			return this.Command.ToString();
		}
	}
}
