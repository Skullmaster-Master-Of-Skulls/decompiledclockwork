using System;
using System.Text;

namespace Telerik.Web.UI.Upload
{
	// Token: 0x02001B79 RID: 7033
	internal class RequestField
	{
		// Token: 0x06011095 RID: 69781 RVA: 0x003C2A71 File Offset: 0x003C0C71
		public RequestField(Encoding encoding)
		{
			this._encoding = encoding;
		}

		// Token: 0x1700532A RID: 21290
		// (get) Token: 0x06011096 RID: 69782 RVA: 0x003C2A98 File Offset: 0x003C0C98
		// (set) Token: 0x06011097 RID: 69783 RVA: 0x003C2AA0 File Offset: 0x003C0CA0
		public FieldHeaderInfo Header
		{
			get
			{
				return this._header;
			}
			internal set
			{
				this._header = value;
			}
		}

		// Token: 0x1700532B RID: 21291
		// (get) Token: 0x06011098 RID: 69784 RVA: 0x003C2AA9 File Offset: 0x003C0CA9
		public bool Complete
		{
			get
			{
				return this._complete;
			}
		}

		// Token: 0x1700532C RID: 21292
		// (get) Token: 0x06011099 RID: 69785 RVA: 0x003C2AB1 File Offset: 0x003C0CB1
		private byte[] Body
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700532D RID: 21293
		// (get) Token: 0x0601109A RID: 69786 RVA: 0x003C2AB8 File Offset: 0x003C0CB8
		public int CompleteBodyBytes
		{
			get
			{
				return this._completeBodyBytes;
			}
		}

		// Token: 0x0601109B RID: 69787 RVA: 0x003C2AC0 File Offset: 0x003C0CC0
		public void AddData(byte[] data, bool lastData)
		{
			if (!this._headerComplete)
			{
				int num = ByteComparer.IndexOf(RequestField._headerEnd, this._bufferedBytes, data, 0);
				int headerBytesToStore = (num < 0) ? data.Length : num;
				this._headerComplete = (num >= 0);
				int num2 = this._bufferedBytes.Length;
				this.AddHeaderData(data, headerBytesToStore, !this._headerComplete);
				if (this._headerComplete)
				{
					this.Header = this.CreateHeader(this._headerData);
					this._completeBodyBytes += data.Length - num - RequestField._headerEnd.Length + num2;
				}
			}
			else
			{
				this._completeBodyBytes += data.Length;
			}
			this._complete = lastData;
		}

		// Token: 0x0601109C RID: 69788 RVA: 0x003C2B6C File Offset: 0x003C0D6C
		private void AddHeaderData(byte[] data, int headerBytesToStore, bool preserveNewBuffer)
		{
			if (headerBytesToStore == 0)
			{
				return;
			}
			bool flag = this._headerData.Length == 0 && this._bufferedBytes.Length + headerBytesToStore <= RequestField._headerEnd.Length;
			int num = this._bufferedBytes.Length;
			if (flag)
			{
				Array.Resize<byte>(ref this._bufferedBytes, this._bufferedBytes.Length + headerBytesToStore);
				Array.Copy(data, 0, this._bufferedBytes, num, headerBytesToStore);
				return;
			}
			int num2 = this._headerData.Length;
			int num3 = num2 + headerBytesToStore;
			int num4 = RequestField._headerEnd.Length;
			if (preserveNewBuffer && num != num4)
			{
				num3 -= num4;
			}
			Array.Resize<byte>(ref this._headerData, num3);
			if (this._bufferedBytes.Length > 0)
			{
				bool flag2 = headerBytesToStore <= this._bufferedBytes.Length;
				if (flag2)
				{
					Array.Copy(this._bufferedBytes, 0, this._headerData, num2, headerBytesToStore);
					return;
				}
				Array.Copy(this._bufferedBytes, 0, this._headerData, num2, this._bufferedBytes.Length);
			}
			int num5 = 0;
			int num6;
			if (preserveNewBuffer)
			{
				Array.Resize<byte>(ref this._bufferedBytes, RequestField._headerEnd.Length);
				Array.Copy(data, headerBytesToStore - this._bufferedBytes.Length, this._bufferedBytes, 0, this._bufferedBytes.Length);
				num6 = this._bufferedBytes.Length;
				if (num == num4)
				{
					num5 = num;
				}
			}
			else
			{
				this._bufferedBytes = new byte[0];
				num5 = num;
				num6 = num;
			}
			Array.Copy(data, 0, this._headerData, num2 + num5, headerBytesToStore - num6);
		}

		// Token: 0x0601109D RID: 69789 RVA: 0x003C2CC5 File Offset: 0x003C0EC5
		private FieldHeaderInfo CreateHeader(byte[] headerData)
		{
			if (FileHeaderInfo.IsFileHeaderInfo(headerData, this._encoding))
			{
				return new FileHeaderInfo(headerData, this._encoding);
			}
			return new FieldHeaderInfo(headerData, this._encoding);
		}

		// Token: 0x04004C38 RID: 19512
		private static byte[] _headerEnd = new byte[]
		{
			13,
			10,
			13,
			10
		};

		// Token: 0x04004C39 RID: 19513
		private byte[] _headerData = new byte[0];

		// Token: 0x04004C3A RID: 19514
		private byte[] _bufferedBytes = new byte[0];

		// Token: 0x04004C3B RID: 19515
		private bool _headerComplete;

		// Token: 0x04004C3C RID: 19516
		private bool _complete;

		// Token: 0x04004C3D RID: 19517
		private Encoding _encoding;

		// Token: 0x04004C3E RID: 19518
		private FieldHeaderInfo _header;

		// Token: 0x04004C3F RID: 19519
		private int _completeBodyBytes;
	}
}
