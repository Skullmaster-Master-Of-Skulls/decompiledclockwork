using System;
using System.Linq;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x0200002F RID: 47
	internal static class ControllerDescriptorExtensions
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x0000511C File Offset: 0x0000331C
		public static string GetAreaName(this ControllerDescriptor controllerDescriptor, RouteAreaAttribute area)
		{
			if (area == null)
			{
				return null;
			}
			if (area.AreaName != null)
			{
				return area.AreaName;
			}
			if (controllerDescriptor.ControllerType.Namespace != null)
			{
				return controllerDescriptor.ControllerType.Namespace.Split(new char[]
				{
					'.'
				}).Last<string>();
			}
			throw Error.InvalidOperation(MvcResources.AttributeRouting_CouldNotInferAreaNameFromMissingNamespace, new object[]
			{
				controllerDescriptor.ControllerName
			});
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005188 File Offset: 0x00003388
		public static RouteAreaAttribute GetAreaFrom(this ControllerDescriptor controllerDescriptor)
		{
			return controllerDescriptor.GetCustomAttributes(typeof(RouteAreaAttribute), true).Cast<RouteAreaAttribute>().FirstOrDefault<RouteAreaAttribute>();
		}
	}
}
