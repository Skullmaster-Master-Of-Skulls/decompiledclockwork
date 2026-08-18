using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.Web.UI.ImageEditor.Serialization
{
	// Token: 0x02000E97 RID: 3735
	public class ResizeOperationSerializer : ImageOperationSerializerBase, IImageOperationSerializer
	{
		// Token: 0x17002D26 RID: 11558
		// (get) Token: 0x06008E8F RID: 36495 RVA: 0x002023EE File Offset: 0x002005EE
		public override string Name
		{
			get
			{
				return "Resize";
			}
		}

		// Token: 0x06008E90 RID: 36496 RVA: 0x002023F8 File Offset: 0x002005F8
		public override Dictionary<string, object> ToData(IImageOperation operation)
		{
			ResizeOperation resizeOperation = operation as ResizeOperation;
			return new Dictionary<string, object>
			{
				{
					"name",
					resizeOperation.Name
				},
				{
					"width",
					resizeOperation.Size.Width
				},
				{
					"height",
					resizeOperation.Size.Height
				}
			};
		}

		// Token: 0x06008E91 RID: 36497 RVA: 0x00202460 File Offset: 0x00200660
		public override IImageOperation FromData(Dictionary<string, object> data)
		{
			if (!base.NameInDataIsCorrect(data))
			{
				return null;
			}
			int index = -1;
			if (data.ContainsKey("index"))
			{
				index = (int)data["index"];
			}
			return new ResizeOperation(new Size((int)data["width"], (int)data["height"]), index);
		}
	}
}
