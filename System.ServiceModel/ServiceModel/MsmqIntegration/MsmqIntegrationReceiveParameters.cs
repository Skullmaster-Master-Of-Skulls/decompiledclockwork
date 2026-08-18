using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;

namespace System.ServiceModel.MsmqIntegration
{
	// Token: 0x020003B6 RID: 950
	internal sealed class MsmqIntegrationReceiveParameters : MsmqReceiveParameters
	{
		// Token: 0x060023A3 RID: 9123 RVA: 0x00082208 File Offset: 0x00080408
		internal MsmqIntegrationReceiveParameters(MsmqIntegrationBindingElement bindingElement) : base(bindingElement)
		{
			this.serializationFormat = bindingElement.SerializationFormat;
			List<Type> list = new List<Type>();
			if (bindingElement.TargetSerializationTypes != null)
			{
				foreach (Type item in bindingElement.TargetSerializationTypes)
				{
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			this.targetSerializationTypes = list.ToArray();
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x0008226B File Offset: 0x0008046B
		internal MsmqMessageSerializationFormat SerializationFormat
		{
			get
			{
				return this.serializationFormat;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x060023A5 RID: 9125 RVA: 0x00082273 File Offset: 0x00080473
		internal Type[] TargetSerializationTypes
		{
			get
			{
				return this.targetSerializationTypes;
			}
		}

		// Token: 0x0400201C RID: 8220
		private MsmqMessageSerializationFormat serializationFormat;

		// Token: 0x0400201D RID: 8221
		private Type[] targetSerializationTypes;
	}
}
