using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000075 RID: 117
	public class ExtendedUnixData : ITaggedData
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0001783E File Offset: 0x0001683E
		public short TagID
		{
			get
			{
				return 21589;
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00017848 File Offset: 0x00016848
		public void SetData(byte[] data, int index, int count)
		{
			using (MemoryStream memoryStream = new MemoryStream(data, index, count, false))
			{
				using (ZipHelperStream zipHelperStream = new ZipHelperStream(memoryStream))
				{
					this._flags = (ExtendedUnixData.Flags)zipHelperStream.ReadByte();
					if ((byte)(this._flags & ExtendedUnixData.Flags.ModificationTime) != 0)
					{
						int seconds = zipHelperStream.ReadLEInt();
						this._modificationTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) + new TimeSpan(0, 0, 0, seconds, 0);
						if (count <= 5)
						{
							return;
						}
					}
					if ((byte)(this._flags & ExtendedUnixData.Flags.AccessTime) != 0)
					{
						int seconds2 = zipHelperStream.ReadLEInt();
						this._lastAccessTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) + new TimeSpan(0, 0, 0, seconds2, 0);
					}
					if ((byte)(this._flags & ExtendedUnixData.Flags.CreateTime) != 0)
					{
						int seconds3 = zipHelperStream.ReadLEInt();
						this._createTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) + new TimeSpan(0, 0, 0, seconds3, 0);
					}
				}
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001795C File Offset: 0x0001695C
		public byte[] GetData()
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ZipHelperStream zipHelperStream = new ZipHelperStream(memoryStream))
				{
					zipHelperStream.IsStreamOwner = false;
					zipHelperStream.WriteByte((byte)this._flags);
					if ((byte)(this._flags & ExtendedUnixData.Flags.ModificationTime) != 0)
					{
						int value = (int)(this._modificationTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
						zipHelperStream.WriteLEInt(value);
					}
					if ((byte)(this._flags & ExtendedUnixData.Flags.AccessTime) != 0)
					{
						int value2 = (int)(this._lastAccessTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
						zipHelperStream.WriteLEInt(value2);
					}
					if ((byte)(this._flags & ExtendedUnixData.Flags.CreateTime) != 0)
					{
						int value3 = (int)(this._createTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
						zipHelperStream.WriteLEInt(value3);
					}
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00017A74 File Offset: 0x00016A74
		public static bool IsValidValue(DateTime value)
		{
			return value >= new DateTime(1901, 12, 13, 20, 45, 52) || value <= new DateTime(2038, 1, 19, 3, 14, 7);
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x00017AAB File Offset: 0x00016AAB
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x00017AB3 File Offset: 0x00016AB3
		public DateTime ModificationTime
		{
			get
			{
				return this._modificationTime;
			}
			set
			{
				if (!ExtendedUnixData.IsValidValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._flags |= ExtendedUnixData.Flags.ModificationTime;
				this._modificationTime = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00017ADE File Offset: 0x00016ADE
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x00017AE6 File Offset: 0x00016AE6
		public DateTime AccessTime
		{
			get
			{
				return this._lastAccessTime;
			}
			set
			{
				if (!ExtendedUnixData.IsValidValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._flags |= ExtendedUnixData.Flags.AccessTime;
				this._lastAccessTime = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00017B11 File Offset: 0x00016B11
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x00017B19 File Offset: 0x00016B19
		public DateTime CreateTime
		{
			get
			{
				return this._createTime;
			}
			set
			{
				if (!ExtendedUnixData.IsValidValue(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._flags |= ExtendedUnixData.Flags.CreateTime;
				this._createTime = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00017B44 File Offset: 0x00016B44
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x00017B4C File Offset: 0x00016B4C
		public ExtendedUnixData.Flags Include
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		// Token: 0x040002ED RID: 749
		private ExtendedUnixData.Flags _flags;

		// Token: 0x040002EE RID: 750
		private DateTime _modificationTime = new DateTime(1970, 1, 1);

		// Token: 0x040002EF RID: 751
		private DateTime _lastAccessTime = new DateTime(1970, 1, 1);

		// Token: 0x040002F0 RID: 752
		private DateTime _createTime = new DateTime(1970, 1, 1);

		// Token: 0x02000076 RID: 118
		[Flags]
		public enum Flags : byte
		{
			// Token: 0x040002F2 RID: 754
			ModificationTime = 1,
			// Token: 0x040002F3 RID: 755
			AccessTime = 2,
			// Token: 0x040002F4 RID: 756
			CreateTime = 4
		}
	}
}
