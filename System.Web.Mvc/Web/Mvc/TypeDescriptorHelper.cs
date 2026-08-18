using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	// Token: 0x02000132 RID: 306
	internal static class TypeDescriptorHelper
	{
		// Token: 0x060007FF RID: 2047 RVA: 0x00015A12 File Offset: 0x00013C12
		public static ICustomTypeDescriptor Get(Type type)
		{
			return new AssociatedMetadataTypeTypeDescriptionProvider(type).GetTypeDescriptor(type);
		}
	}
}
