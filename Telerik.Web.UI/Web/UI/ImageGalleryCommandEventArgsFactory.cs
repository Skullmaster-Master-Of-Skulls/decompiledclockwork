using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000530 RID: 1328
	internal static class ImageGalleryCommandEventArgsFactory
	{
		// Token: 0x06002F24 RID: 12068 RVA: 0x0009A6A4 File Offset: 0x000988A4
		internal static ImageGalleryCommandEventArgs CreateCommandEventArgs(string commandName, object commandArgument)
		{
			if (string.Compare(commandName, "Page", true) == 0)
			{
				return new ImageGalleryPageIndexChangedEventArgs(commandArgument);
			}
			if (string.Compare(commandName, "ChangePageSize", true) == 0)
			{
				return new ImageGalleryPageSizeChangedEventArgs(commandArgument);
			}
			if (string.Compare(commandName, "ChangeItemIndex", true) == 0)
			{
				return new ImageGalleryChangeItemIndexEventArgs(commandArgument);
			}
			return new ImageGalleryCommandEventArgs(commandName, commandArgument);
		}
	}
}
