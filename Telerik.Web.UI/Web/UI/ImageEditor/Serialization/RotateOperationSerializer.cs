using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.Web.UI.ImageEditor.Serialization
{
	// Token: 0x02000E96 RID: 3734
	public class RotateOperationSerializer : ImageOperationSerializerBase, IImageOperationSerializer
	{
		// Token: 0x17002D25 RID: 11557
		// (get) Token: 0x06008E8B RID: 36491 RVA: 0x00202304 File Offset: 0x00200504
		public override string Name
		{
			get
			{
				return "Rotate";
			}
		}

		// Token: 0x06008E8C RID: 36492 RVA: 0x0020230C File Offset: 0x0020050C
		public override Dictionary<string, object> ToData(IImageOperation operation)
		{
			RotateFlipOperation rotateFlipOperation = operation as RotateFlipOperation;
			return new Dictionary<string, object>
			{
				{
					"name",
					rotateFlipOperation.Name
				},
				{
					"degree",
					(rotateFlipOperation.Type == RotateFlipType.Rotate90FlipNone) ? 90 : ((rotateFlipOperation.Type == RotateFlipType.Rotate180FlipNone) ? 180 : ((rotateFlipOperation.Type == RotateFlipType.Rotate270FlipNone) ? 270 : 0))
				}
			};
		}

		// Token: 0x06008E8D RID: 36493 RVA: 0x00202378 File Offset: 0x00200578
		public override IImageOperation FromData(Dictionary<string, object> data)
		{
			if (!base.NameInDataIsCorrect(data))
			{
				return null;
			}
			int num = (int)data["degree"];
			int index = -1;
			if (data.ContainsKey("index"))
			{
				index = (int)data["index"];
			}
			return new RotateFlipOperation((num == 90) ? RotateFlipType.Rotate90FlipNone : ((num == 180) ? RotateFlipType.Rotate180FlipNone : ((num == 270) ? RotateFlipType.Rotate270FlipNone : RotateFlipType.RotateNoneFlipNone)), index);
		}
	}
}
