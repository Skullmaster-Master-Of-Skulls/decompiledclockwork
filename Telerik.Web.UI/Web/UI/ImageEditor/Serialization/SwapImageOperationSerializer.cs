using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.ImageEditor.Serialization
{
	// Token: 0x02000BB0 RID: 2992
	public class SwapImageOperationSerializer : ImageOperationSerializerBase, IImageOperationSerializer
	{
		// Token: 0x1700250C RID: 9484
		// (get) Token: 0x06007191 RID: 29073 RVA: 0x001A960C File Offset: 0x001A780C
		public override string Name
		{
			get
			{
				return "SwapImage";
			}
		}

		// Token: 0x06007192 RID: 29074 RVA: 0x001A9614 File Offset: 0x001A7814
		public override Dictionary<string, object> ToData(IImageOperation operation)
		{
			SwapImageOperation swapImageOperation = operation as SwapImageOperation;
			return new Dictionary<string, object>
			{
				{
					"name",
					swapImageOperation.Name
				},
				{
					"src",
					swapImageOperation.Src
				}
			};
		}

		// Token: 0x06007193 RID: 29075 RVA: 0x001A9654 File Offset: 0x001A7854
		public override IImageOperation FromData(Dictionary<string, object> data)
		{
			if (!data.ContainsKey("name") || !data.ContainsKey("src"))
			{
				return null;
			}
			return new SwapImageOperation(data["src"].ToString(), data["name"].ToString());
		}
	}
}
