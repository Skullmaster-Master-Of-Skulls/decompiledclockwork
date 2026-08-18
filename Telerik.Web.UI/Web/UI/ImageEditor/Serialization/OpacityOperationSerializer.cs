using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.ImageEditor.Serialization
{
	// Token: 0x02000E98 RID: 3736
	public class OpacityOperationSerializer : ImageOperationSerializerBase, IImageOperationSerializer
	{
		// Token: 0x17002D27 RID: 11559
		// (get) Token: 0x06008E93 RID: 36499 RVA: 0x002024CB File Offset: 0x002006CB
		public override string Name
		{
			get
			{
				return "Opacity";
			}
		}

		// Token: 0x06008E94 RID: 36500 RVA: 0x002024D4 File Offset: 0x002006D4
		public override Dictionary<string, object> ToData(IImageOperation operation)
		{
			OpacityOperation opacityOperation = operation as OpacityOperation;
			return new Dictionary<string, object>
			{
				{
					"name",
					this.Name
				},
				{
					"value",
					(int)(opacityOperation.Opacity * 100.0)
				}
			};
		}

		// Token: 0x06008E95 RID: 36501 RVA: 0x00202524 File Offset: 0x00200724
		public override IImageOperation FromData(Dictionary<string, object> data)
		{
			if (!base.NameInDataIsCorrect(data))
			{
				return null;
			}
			double num = 1.0;
			if (data.ContainsKey("value"))
			{
				double.TryParse(data["value"].ToString(), out num);
				num /= 100.0;
			}
			int index = -1;
			if (data.ContainsKey("index"))
			{
				index = (int)data["index"];
			}
			return new OpacityOperation(num, index);
		}
	}
}
