using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.ImageEditor.Serialization
{
	// Token: 0x02000E94 RID: 3732
	public class EmptyOperationSerializer : ImageOperationSerializerBase, IImageOperationSerializer
	{
		// Token: 0x06008E83 RID: 36483 RVA: 0x002021E0 File Offset: 0x002003E0
		public override Dictionary<string, object> ToData(IImageOperation operation)
		{
			return new Dictionary<string, object>
			{
				{
					"name",
					"Empty"
				}
			};
		}

		// Token: 0x06008E84 RID: 36484 RVA: 0x00202204 File Offset: 0x00200404
		public override IImageOperation FromData(Dictionary<string, object> data)
		{
			return new EmptyOperation();
		}

		// Token: 0x17002D23 RID: 11555
		// (get) Token: 0x06008E85 RID: 36485 RVA: 0x0020220B File Offset: 0x0020040B
		public override string Name
		{
			get
			{
				return "Empty";
			}
		}
	}
}
