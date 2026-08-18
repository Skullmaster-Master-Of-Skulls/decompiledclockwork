using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace System.Web.ModelBinding
{
	// Token: 0x0200066B RID: 1643
	internal static class TypeDescriptorHelper
	{
		// Token: 0x06005051 RID: 20561 RVA: 0x001155D2 File Offset: 0x001137D2
		public static ICustomTypeDescriptor Get(Type type)
		{
			return new AssociatedMetadataTypeTypeDescriptionProvider(type).GetTypeDescriptor(type);
		}
	}
}
