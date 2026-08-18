using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Http.Internal
{
	// Token: 0x0200012E RID: 302
	internal static class TypeDescriptorHelper
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x00018FDB File Offset: 0x000171DB
		internal static ICustomTypeDescriptor Get(Type type)
		{
			return new AssociatedMetadataTypeTypeDescriptionProvider(type).GetTypeDescriptor(type);
		}
	}
}
