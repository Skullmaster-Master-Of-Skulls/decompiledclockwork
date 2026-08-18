using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AC4 RID: 2756
	internal sealed class Obj : BaseBiffRecord, IRecord
	{
		// Token: 0x06006846 RID: 26694 RVA: 0x00186A80 File Offset: 0x00184C80
		public Obj(ushort objId) : base(93)
		{
			base.Length = 38;
			byte[] array = new byte[12];
			this.reserveData = array;
			this.pictureData = new byte[]
			{
				7,
				0,
				2,
				0,
				byte.MaxValue,
				byte.MaxValue,
				8,
				0,
				2,
				0,
				1,
				0,
				0,
				0,
				0,
				0
			};
			this.objId = objId;
		}

		// Token: 0x06006847 RID: 26695 RVA: 0x00186ACC File Offset: 0x00184CCC
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(21);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(18);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(8);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.objId);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(24593);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			this.reserveData.CopyTo(data, num);
			num += this.reserveData.Length;
			this.pictureData.CopyTo(data, num);
			return data;
		}

		// Token: 0x04001B7D RID: 7037
		private const ushort type = 93;

		// Token: 0x04001B7E RID: 7038
		private const ushort length = 38;

		// Token: 0x04001B7F RID: 7039
		private const ushort ftCmo = 21;

		// Token: 0x04001B80 RID: 7040
		private const ushort ftCmoSize = 18;

		// Token: 0x04001B81 RID: 7041
		private const ushort grBits = 24593;

		// Token: 0x04001B82 RID: 7042
		private const ushort objectType = 8;

		// Token: 0x04001B83 RID: 7043
		private ushort objId;

		// Token: 0x04001B84 RID: 7044
		private byte[] pictureData;

		// Token: 0x04001B85 RID: 7045
		private byte[] reserveData;
	}
}
