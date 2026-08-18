using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000077 RID: 119
	public class NTTaggedData : ITaggedData
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00017B93 File Offset: 0x00016B93
		public short TagID
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00017B98 File Offset: 0x00016B98
		public void SetData(byte[] data, int index, int count)
		{
			using (MemoryStream memoryStream = new MemoryStream(data, index, count, false))
			{
				using (ZipHelperStream zipHelperStream = new ZipHelperStream(memoryStream))
				{
					zipHelperStream.ReadLEInt();
					while (zipHelperStream.Position < zipHelperStream.Length)
					{
						int num = zipHelperStream.ReadLEShort();
						int num2 = zipHelperStream.ReadLEShort();
						if (num == 1)
						{
							if (num2 >= 24)
							{
								long fileTime = zipHelperStream.ReadLELong();
								this._lastModificationTime = DateTime.FromFileTimeUtc(fileTime);
								long fileTime2 = zipHelperStream.ReadLELong();
								this._lastAccessTime = DateTime.FromFileTimeUtc(fileTime2);
								long fileTime3 = zipHelperStream.ReadLELong();
								this._createTime = DateTime.FromFileTimeUtc(fileTime3);
								break;
							}
							break;
						}
						else
						{
							zipHelperStream.Seek((long)num2, SeekOrigin.Current);
						}
					}
				}
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00017C64 File Offset: 0x00016C64
		public byte[] GetData()
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ZipHelperStream zipHelperStream = new ZipHelperStream(memoryStream))
				{
					zipHelperStream.IsStreamOwner = false;
					zipHelperStream.WriteLEInt(0);
					zipHelperStream.WriteLEShort(1);
					zipHelperStream.WriteLEShort(24);
					zipHelperStream.WriteLELong(this._lastModificationTime.ToFileTimeUtc());
					zipHelperStream.WriteLELong(this._lastAccessTime.ToFileTimeUtc());
					zipHelperStream.WriteLELong(this._createTime.ToFileTimeUtc());
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00017D08 File Offset: 0x00016D08
		public static bool IsValidValue(DateTime value)
		{
			bool result = true;
			try
			{
				value.ToFileTimeUtc();
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00017D38 File Offset: 0x00016D38
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x00017D40 File Offset: 0x00016D40
		public DateTime LastModificationTime
		{
			get
			{
				return this._lastModificationTime;
			}
			set
			{
				if (!NTTaggedData.IsValidValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lastModificationTime = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00017D5C File Offset: 0x00016D5C
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x00017D64 File Offset: 0x00016D64
		public DateTime CreateTime
		{
			get
			{
				return this._createTime;
			}
			set
			{
				if (!NTTaggedData.IsValidValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._createTime = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00017D80 File Offset: 0x00016D80
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x00017D88 File Offset: 0x00016D88
		public DateTime LastAccessTime
		{
			get
			{
				return this._lastAccessTime;
			}
			set
			{
				if (!NTTaggedData.IsValidValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lastAccessTime = value;
			}
		}

		// Token: 0x040002F5 RID: 757
		private DateTime _lastAccessTime = DateTime.FromFileTimeUtc(0L);

		// Token: 0x040002F6 RID: 758
		private DateTime _lastModificationTime = DateTime.FromFileTimeUtc(0L);

		// Token: 0x040002F7 RID: 759
		private DateTime _createTime = DateTime.FromFileTimeUtc(0L);
	}
}
