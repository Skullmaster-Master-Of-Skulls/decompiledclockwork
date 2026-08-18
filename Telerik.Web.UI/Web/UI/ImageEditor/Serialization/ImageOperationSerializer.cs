using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.ImageEditor.Serialization
{
	// Token: 0x02000E9B RID: 3739
	public class ImageOperationSerializer
	{
		// Token: 0x06008E9F RID: 36511 RVA: 0x0020274C File Offset: 0x0020094C
		public ImageOperationSerializer() : this(ImageOperationSerializer.defaultSerializers)
		{
		}

		// Token: 0x06008EA0 RID: 36512 RVA: 0x00202759 File Offset: 0x00200959
		public ImageOperationSerializer(ImageOperationSerializers serializers)
		{
			this._serializers = serializers;
		}

		// Token: 0x06008EA1 RID: 36513 RVA: 0x00202768 File Offset: 0x00200968
		public virtual string Serialize(IImageOperation operation)
		{
			IImageOperationSerializer operationSerializer = this.GetOperationSerializer(operation.Name);
			return operationSerializer.Serialize(operation);
		}

		// Token: 0x06008EA2 RID: 36514 RVA: 0x0020278C File Offset: 0x0020098C
		public virtual IImageOperation Deserialize(string value)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return this.FromData(javaScriptSerializer.DeserializeObject(value) as Dictionary<string, object>);
		}

		// Token: 0x06008EA3 RID: 36515 RVA: 0x002027B4 File Offset: 0x002009B4
		public virtual IImageOperation FromData(Dictionary<string, object> data)
		{
			IImageOperationSerializer operationSerializer = this.GetOperationSerializer(data["name"].ToString());
			return operationSerializer.FromData(data);
		}

		// Token: 0x06008EA4 RID: 36516 RVA: 0x002027DF File Offset: 0x002009DF
		private IImageOperationSerializer GetOperationSerializer(string name)
		{
			if (!this._serializers.Contains(name))
			{
				return ImageOperationSerializer.emptySerializer;
			}
			return this._serializers[name];
		}

		// Token: 0x06008EA5 RID: 36517 RVA: 0x00202804 File Offset: 0x00200A04
		private static ImageOperationSerializers RegisterDefaultSerializers()
		{
			return new ImageOperationSerializers
			{
				new OpacityOperationSerializer(),
				new ResizeOperationSerializer(),
				new RotateOperationSerializer(),
				new FlipOperationSerializer(),
				new CropOperationSerializer()
			};
		}

		// Token: 0x040027A9 RID: 10153
		private static readonly ImageOperationSerializers defaultSerializers = ImageOperationSerializer.RegisterDefaultSerializers();

		// Token: 0x040027AA RID: 10154
		private readonly ImageOperationSerializers _serializers;

		// Token: 0x040027AB RID: 10155
		private static readonly EmptyOperationSerializer emptySerializer = new EmptyOperationSerializer();
	}
}
