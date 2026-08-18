using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.Web.UI.ImageEditor.Serialization
{
	// Token: 0x02000E95 RID: 3733
	public class FlipOperationSerializer : ImageOperationSerializerBase, IImageOperationSerializer
	{
		// Token: 0x17002D24 RID: 11556
		// (get) Token: 0x06008E87 RID: 36487 RVA: 0x0020221A File Offset: 0x0020041A
		public override string Name
		{
			get
			{
				return "Flip";
			}
		}

		// Token: 0x06008E88 RID: 36488 RVA: 0x00202224 File Offset: 0x00200424
		public override Dictionary<string, object> ToData(IImageOperation operation)
		{
			FlipOperation flipOperation = operation as FlipOperation;
			return new Dictionary<string, object>
			{
				{
					"name",
					flipOperation.Name
				},
				{
					"direction",
					(flipOperation.Type == RotateFlipType.Rotate180FlipNone) ? 3 : ((flipOperation.Type == RotateFlipType.RotateNoneFlipX) ? 2 : ((flipOperation.Type == RotateFlipType.Rotate180FlipX) ? 1 : 0))
				}
			};
		}

		// Token: 0x06008E89 RID: 36489 RVA: 0x00202288 File Offset: 0x00200488
		public override IImageOperation FromData(Dictionary<string, object> data)
		{
			if (!base.NameInDataIsCorrect(data))
			{
				return null;
			}
			int num = 0;
			if (data.ContainsKey("direction"))
			{
				num = (int)data["direction"];
			}
			int index = -1;
			if (data.ContainsKey("index"))
			{
				index = (int)data["index"];
			}
			return new FlipOperation((num == 3) ? RotateFlipType.Rotate180FlipNone : ((num == 2) ? RotateFlipType.RotateNoneFlipX : ((num == 1) ? RotateFlipType.Rotate180FlipX : RotateFlipType.RotateNoneFlipNone)), index);
		}
	}
}
