using System;

namespace System.Web.Http.Controllers
{
	// Token: 0x0200002E RID: 46
	internal static class HttpControllerDescriptorExtensions
	{
		// Token: 0x0600010D RID: 269 RVA: 0x00006AC4 File Offset: 0x00004CC4
		public static bool IsAttributeRouted(this HttpControllerDescriptor controllerDescriptor)
		{
			if (controllerDescriptor == null)
			{
				throw new ArgumentNullException("controllerDescriptor");
			}
			object obj;
			controllerDescriptor.Properties.TryGetValue("MS_IsAttributeRouted", out obj);
			return (obj as bool?) ?? false;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006B10 File Offset: 0x00004D10
		public static void SetIsAttributeRouted(this HttpControllerDescriptor controllerDescriptor, bool value)
		{
			if (controllerDescriptor == null)
			{
				throw new ArgumentNullException("controllerDescriptor");
			}
			controllerDescriptor.Properties["MS_IsAttributeRouted"] = value;
		}

		// Token: 0x04000062 RID: 98
		private const string AttributeRoutedPropertyKey = "MS_IsAttributeRouted";
	}
}
