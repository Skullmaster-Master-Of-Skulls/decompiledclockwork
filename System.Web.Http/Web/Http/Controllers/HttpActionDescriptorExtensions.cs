using System;

namespace System.Web.Http.Controllers
{
	// Token: 0x0200002D RID: 45
	internal static class HttpActionDescriptorExtensions
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00006A50 File Offset: 0x00004C50
		public static bool IsAttributeRouted(this HttpActionDescriptor actionDescriptor)
		{
			if (actionDescriptor == null)
			{
				throw new ArgumentNullException("actionDescriptor");
			}
			object obj;
			actionDescriptor.Properties.TryGetValue("MS_IsAttributeRouted", out obj);
			return (obj as bool?) ?? false;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006A9C File Offset: 0x00004C9C
		public static void SetIsAttributeRouted(this HttpActionDescriptor actionDescriptor, bool value)
		{
			if (actionDescriptor == null)
			{
				throw new ArgumentNullException("actionDescriptor");
			}
			actionDescriptor.Properties["MS_IsAttributeRouted"] = value;
		}

		// Token: 0x04000061 RID: 97
		private const string AttributeRoutedPropertyKey = "MS_IsAttributeRouted";
	}
}
