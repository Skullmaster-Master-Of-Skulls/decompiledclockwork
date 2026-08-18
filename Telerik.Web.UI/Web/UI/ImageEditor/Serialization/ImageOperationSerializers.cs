using System;
using System.Collections.ObjectModel;

namespace Telerik.Web.UI.ImageEditor.Serialization
{
	// Token: 0x02000E9C RID: 3740
	public class ImageOperationSerializers : KeyedCollection<string, IImageOperationSerializer>
	{
		// Token: 0x06008EA7 RID: 36519 RVA: 0x00202865 File Offset: 0x00200A65
		protected override string GetKeyForItem(IImageOperationSerializer item)
		{
			return item.Name;
		}
	}
}
