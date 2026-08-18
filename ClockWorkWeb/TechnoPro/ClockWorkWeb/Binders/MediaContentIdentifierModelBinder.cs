using System;
using System.Web.Mvc;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkWeb.Binders.Adapters;

namespace TechnoPro.ClockWorkWeb.Binders
{
	// Token: 0x0200015C RID: 348
	public class MediaContentIdentifierModelBinder : DefaultModelBinder
	{
		// Token: 0x06000A98 RID: 2712 RVA: 0x00048AC4 File Offset: 0x00046CC4
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			bool flag = bindingContext.ModelType == typeof(MediaContentIdentifierDTO);
			object result;
			if (flag)
			{
				string value = bindingContext.GetValue("MediaContentIdentifier.MediaContentUniqueId");
				string value2 = bindingContext.GetValue("MediaContentIdentifier.MediaContentId");
				string value3 = bindingContext.GetValue("MediaContentIdentifier.ISBN");
				string value4 = bindingContext.GetValue("MediaContentIdentifier.ExternalId");
				result = new MediaContentIdentifierDTO
				{
					MediaContentUniqueId = (string.IsNullOrEmpty(value) ? null : new Guid?(new Guid(value))),
					MediaContentId = (string.IsNullOrEmpty(value2) ? 0 : Convert.ToInt32(value2)),
					ExternalId = value4,
					ISBN = value3
				};
			}
			else
			{
				result = base.BindModel(controllerContext, bindingContext);
			}
			return result;
		}
	}
}
