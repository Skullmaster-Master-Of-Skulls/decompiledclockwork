using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200000B RID: 11
	public class ZipEntry : ICloneable
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002FEA File Offset: 0x00001FEA
		public ZipEntry(string name) : this(name, 0, 51, CompressionMethod.Deflated)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002FF7 File Offset: 0x00001FF7
		internal ZipEntry(string name, int versionRequiredToExtract) : this(name, versionRequiredToExtract, 51, CompressionMethod.Deflated)
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003004 File Offset: 0x00002004
		internal ZipEntry(string name, int versionRequiredToExtract, int madeByInfo, CompressionMethod method)
		{
			this.externalFileAttributes = -1;
			this.method = CompressionMethod.Deflated;
			this.zipFileIndex = -1L;
			base..ctor();
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length > 65535)
			{
				throw new ArgumentException("Name is too long", "name");
			}
			if (versionRequiredToExtract != 0 && versionRequiredToExtract < 10)
			{
				throw new ArgumentOutOfRangeException("versionRequiredToExtract");
			}
			this.DateTime = DateTime.Now;
			this.name = ZipEntry.CleanName(name);
			this.versionMadeBy = (ushort)madeByInfo;
			this.versionToExtract = (ushort)versionRequiredToExtract;
			this.method = method;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000309C File Offset: 0x0000209C
		[Obsolete("Use Clone instead")]
		public ZipEntry(ZipEntry entry)
		{
			this.externalFileAttributes = -1;
			this.method = CompressionMethod.Deflated;
			this.zipFileIndex = -1L;
			base..ctor();
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			this.known = entry.known;
			this.name = entry.name;
			this.size = entry.size;
			this.compressedSize = entry.compressedSize;
			this.crc = entry.crc;
			this.dosTime = entry.dosTime;
			this.dateTime = entry.dateTime;
			this.method = entry.method;
			this.comment = entry.comment;
			this.versionToExtract = entry.versionToExtract;
			this.versionMadeBy = entry.versionMadeBy;
			this.externalFileAttributes = entry.externalFileAttributes;
			this.flags = entry.flags;
			this.zipFileIndex = entry.zipFileIndex;
			this.offset = entry.offset;
			this.forceZip64_ = entry.forceZip64_;
			if (entry.extra != null)
			{
				this.extra = new byte[entry.extra.Length];
				Array.Copy(entry.extra, 0, this.extra, 0, entry.extra.Length);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000031C9 File Offset: 0x000021C9
		public bool HasCrc
		{
			get
			{
				return (byte)(this.known & ZipEntry.Known.Crc) != 0;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000031DA File Offset: 0x000021DA
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000031EA File Offset: 0x000021EA
		public bool IsCrypted
		{
			get
			{
				return (this.flags & 1) != 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 1;
					return;
				}
				this.flags &= -2;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000320D File Offset: 0x0000220D
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00003221 File Offset: 0x00002221
		public bool IsUnicodeText
		{
			get
			{
				return (this.flags & 2048) != 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 2048;
					return;
				}
				this.flags &= -2049;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000324B File Offset: 0x0000224B
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00003253 File Offset: 0x00002253
		internal byte CryptoCheckValue
		{
			get
			{
				return this.cryptoCheckValue_;
			}
			set
			{
				this.cryptoCheckValue_ = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000325C File Offset: 0x0000225C
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00003264 File Offset: 0x00002264
		public int Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000326D File Offset: 0x0000226D
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00003275 File Offset: 0x00002275
		public long ZipFileIndex
		{
			get
			{
				return this.zipFileIndex;
			}
			set
			{
				this.zipFileIndex = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000327E File Offset: 0x0000227E
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00003286 File Offset: 0x00002286
		public long Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000328F File Offset: 0x0000228F
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000032A5 File Offset: 0x000022A5
		public int ExternalFileAttributes
		{
			get
			{
				if ((byte)(this.known & ZipEntry.Known.ExternalAttributes) == 0)
				{
					return -1;
				}
				return this.externalFileAttributes;
			}
			set
			{
				this.externalFileAttributes = value;
				this.known |= ZipEntry.Known.ExternalAttributes;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000032BE File Offset: 0x000022BE
		public int VersionMadeBy
		{
			get
			{
				return (int)(this.versionMadeBy & 255);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000032CC File Offset: 0x000022CC
		public bool IsDOSEntry
		{
			get
			{
				return this.HostSystem == HostSystemID.Msdos || this.HostSystem == HostSystemID.WindowsNT;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000032E4 File Offset: 0x000022E4
		private bool HasDosAttributes(int attributes)
		{
			bool result = false;
			if ((byte)(this.known & ZipEntry.Known.ExternalAttributes) != 0 && (this.HostSystem == HostSystemID.Msdos || this.HostSystem == HostSystemID.WindowsNT) && (this.ExternalFileAttributes & attributes) == attributes)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000331F File Offset: 0x0000231F
		// (set) Token: 0x0600004A RID: 74 RVA: 0x0000332F File Offset: 0x0000232F
		public HostSystemID HostSystem
		{
			get
			{
				return (HostSystemID)(this.versionMadeBy >> 8 & 255);
			}
			set
			{
				this.versionMadeBy &= 255;
				this.versionMadeBy |= (ushort)((value & (HostSystemID)255) << 8);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000335C File Offset: 0x0000235C
		public int Version
		{
			get
			{
				if (this.versionToExtract != 0)
				{
					return (int)(this.versionToExtract & 255);
				}
				int result = 10;
				if (this.AESKeySize > 0)
				{
					result = 51;
				}
				else if (this.CentralHeaderRequiresZip64)
				{
					result = 45;
				}
				else if (CompressionMethod.Deflated == this.method)
				{
					result = 20;
				}
				else if (this.IsDirectory)
				{
					result = 20;
				}
				else if (this.IsCrypted)
				{
					result = 20;
				}
				else if (this.HasDosAttributes(8))
				{
					result = 11;
				}
				return result;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000033D4 File Offset: 0x000023D4
		public bool CanDecompress
		{
			get
			{
				return this.Version <= 51 && (this.Version == 10 || this.Version == 11 || this.Version == 20 || this.Version == 45 || this.Version == 51) && this.IsCompressionMethodSupported();
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003425 File Offset: 0x00002425
		public void ForceZip64()
		{
			this.forceZip64_ = true;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000342E File Offset: 0x0000242E
		public bool IsZip64Forced()
		{
			return this.forceZip64_;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003438 File Offset: 0x00002438
		public bool LocalHeaderRequiresZip64
		{
			get
			{
				bool flag = this.forceZip64_;
				if (!flag)
				{
					ulong num = this.compressedSize;
					if (this.versionToExtract == 0 && this.IsCrypted)
					{
						num += 12UL;
					}
					flag = ((this.size >= (ulong)-1 || num >= (ulong)-1) && (this.versionToExtract == 0 || this.versionToExtract >= 45));
				}
				return flag;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003498 File Offset: 0x00002498
		public bool CentralHeaderRequiresZip64
		{
			get
			{
				return this.LocalHeaderRequiresZip64 || this.offset >= (long)((ulong)-1);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000034B1 File Offset: 0x000024B1
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000034C8 File Offset: 0x000024C8
		public long DosTime
		{
			get
			{
				if ((byte)(this.known & ZipEntry.Known.Time) == 0)
				{
					return 0L;
				}
				return (long)((ulong)this.dosTime);
			}
			set
			{
				this.dosTime = (uint)value;
				this.known |= ZipEntry.Known.Time;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000034E1 File Offset: 0x000024E1
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000034EC File Offset: 0x000024EC
		public DateTime DateTime
		{
			get
			{
				return this.dateTime;
			}
			set
			{
				this.dateTime = value;
				uint num = (uint)value.Year;
				uint num2 = (uint)value.Month;
				uint num3 = (uint)value.Day;
				uint num4 = (uint)value.Hour;
				uint num5 = (uint)value.Minute;
				uint num6 = (uint)value.Second;
				if (num < 1980U)
				{
					num = 1980U;
					num2 = 1U;
					num3 = 1U;
					num4 = 0U;
					num5 = 0U;
					num6 = 0U;
				}
				else if (num > 2107U)
				{
					num = 2107U;
					num2 = 12U;
					num3 = 31U;
					num4 = 23U;
					num5 = 59U;
					num6 = 59U;
				}
				this.DosTime = (long)((ulong)((num - 1980U & 127U) << 25 | num2 << 21 | num3 << 16 | num4 << 11 | num5 << 5 | num6 >> 1));
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000359A File Offset: 0x0000259A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000035A2 File Offset: 0x000025A2
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000035B8 File Offset: 0x000025B8
		public long Size
		{
			get
			{
				if ((byte)(this.known & ZipEntry.Known.Size) == 0)
				{
					return -1L;
				}
				return (long)this.size;
			}
			set
			{
				this.size = (ulong)value;
				this.known |= ZipEntry.Known.Size;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000035D0 File Offset: 0x000025D0
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000035E6 File Offset: 0x000025E6
		public long CompressedSize
		{
			get
			{
				if ((byte)(this.known & ZipEntry.Known.CompressedSize) == 0)
				{
					return -1L;
				}
				return (long)this.compressedSize;
			}
			set
			{
				this.compressedSize = (ulong)value;
				this.known |= ZipEntry.Known.CompressedSize;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000035FE File Offset: 0x000025FE
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003618 File Offset: 0x00002618
		public long Crc
		{
			get
			{
				if ((byte)(this.known & ZipEntry.Known.Crc) == 0)
				{
					return -1L;
				}
				return (long)((ulong)this.crc & (ulong)-1);
			}
			set
			{
				if (((ulong)this.crc & 18446744069414584320UL) != 0UL)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.crc = (uint)value;
				this.known |= ZipEntry.Known.Crc;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003651 File Offset: 0x00002651
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00003659 File Offset: 0x00002659
		public CompressionMethod CompressionMethod
		{
			get
			{
				return this.method;
			}
			set
			{
				if (!ZipEntry.IsCompressionMethodSupported(value))
				{
					throw new NotSupportedException("Compression method not supported");
				}
				this.method = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003675 File Offset: 0x00002675
		internal CompressionMethod CompressionMethodForHeader
		{
			get
			{
				if (this.AESKeySize <= 0)
				{
					return this.method;
				}
				return CompressionMethod.WinZipAES;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003689 File Offset: 0x00002689
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00003694 File Offset: 0x00002694
		public byte[] ExtraData
		{
			get
			{
				return this.extra;
			}
			set
			{
				if (value == null)
				{
					this.extra = null;
					return;
				}
				if (value.Length > 65535)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.extra = new byte[value.Length];
				Array.Copy(value, 0, this.extra, 0, value.Length);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000036E0 File Offset: 0x000026E0
		// (set) Token: 0x06000062 RID: 98 RVA: 0x0000373C File Offset: 0x0000273C
		public int AESKeySize
		{
			get
			{
				switch (this._aesEncryptionStrength)
				{
				case 0:
					return 0;
				case 1:
					return 128;
				case 2:
					return 192;
				case 3:
					return 256;
				default:
					throw new ZipException("Invalid AESEncryptionStrength " + this._aesEncryptionStrength);
				}
			}
			set
			{
				if (value == 0)
				{
					this._aesEncryptionStrength = 0;
					return;
				}
				if (value == 128)
				{
					this._aesEncryptionStrength = 1;
					return;
				}
				if (value != 256)
				{
					throw new ZipException("AESKeySize must be 0, 128 or 256: " + value);
				}
				this._aesEncryptionStrength = 3;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000378E File Offset: 0x0000278E
		internal byte AESEncryptionStrength
		{
			get
			{
				return (byte)this._aesEncryptionStrength;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003797 File Offset: 0x00002797
		internal int AESSaltLen
		{
			get
			{
				return this.AESKeySize / 16;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000037A2 File Offset: 0x000027A2
		internal int AESOverheadSize
		{
			get
			{
				return 12 + this.AESSaltLen;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000037B0 File Offset: 0x000027B0
		internal void ProcessExtraData(bool localHeader)
		{
			ZipExtraData zipExtraData = new ZipExtraData(this.extra);
			if (zipExtraData.Find(1))
			{
				this.forceZip64_ = true;
				if (zipExtraData.ValueLength < 4)
				{
					throw new ZipException("Extra data extended Zip64 information length is invalid");
				}
				if (localHeader || this.size == (ulong)-1)
				{
					this.size = (ulong)zipExtraData.ReadLong();
				}
				if (localHeader || this.compressedSize == (ulong)-1)
				{
					this.compressedSize = (ulong)zipExtraData.ReadLong();
				}
				if (!localHeader && this.offset == (long)((ulong)-1))
				{
					this.offset = zipExtraData.ReadLong();
				}
			}
			else if ((this.versionToExtract & 255) >= 45 && (this.size == (ulong)-1 || this.compressedSize == (ulong)-1))
			{
				throw new ZipException("Zip64 Extended information required but is missing.");
			}
			this.dateTime = this.GetDateTime(zipExtraData);
			if (this.method == CompressionMethod.WinZipAES)
			{
				this.ProcessAESExtraData(zipExtraData);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003888 File Offset: 0x00002888
		private DateTime GetDateTime(ZipExtraData extraData)
		{
			ExtendedUnixData data = extraData.GetData<ExtendedUnixData>();
			if (data != null && (byte)(data.Include & ExtendedUnixData.Flags.ModificationTime) != 0 && (byte)(data.Include & ExtendedUnixData.Flags.AccessTime) != 0 && (byte)(data.Include & ExtendedUnixData.Flags.CreateTime) != 0)
			{
				return data.ModificationTime;
			}
			uint second = Math.Min(59U, 2U * (this.dosTime & 31U));
			uint minute = Math.Min(59U, this.dosTime >> 5 & 63U);
			uint hour = Math.Min(23U, this.dosTime >> 11 & 31U);
			uint month = Math.Max(1U, Math.Min(12U, this.dosTime >> 21 & 15U));
			uint year = (this.dosTime >> 25 & 127U) + 1980U;
			int day = Math.Max(1, Math.Min(DateTime.DaysInMonth((int)year, (int)month), (int)(this.dosTime >> 16 & 31U)));
			return new DateTime((int)year, (int)month, day, (int)hour, (int)minute, (int)second, DateTimeKind.Utc);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003964 File Offset: 0x00002964
		private void ProcessAESExtraData(ZipExtraData extraData)
		{
			if (!extraData.Find(39169))
			{
				throw new ZipException("AES Extra Data missing");
			}
			this.versionToExtract = 51;
			this.Flags |= 64;
			int valueLength = extraData.ValueLength;
			if (valueLength < 7)
			{
				throw new ZipException("AES Extra Data Length " + valueLength + " invalid.");
			}
			int aesVer = extraData.ReadShort();
			extraData.ReadShort();
			int aesEncryptionStrength = extraData.ReadByte();
			int num = extraData.ReadShort();
			this._aesVer = aesVer;
			this._aesEncryptionStrength = aesEncryptionStrength;
			this.method = (CompressionMethod)num;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000039F7 File Offset: 0x000029F7
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000039FF File Offset: 0x000029FF
		public string Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				if (value != null && value.Length > 65535)
				{
					throw new ArgumentOutOfRangeException("value", "cannot exceed 65535");
				}
				this.comment = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003A28 File Offset: 0x00002A28
		public bool IsDirectory
		{
			get
			{
				int length = this.name.Length;
				return (length > 0 && (this.name[length - 1] == '/' || this.name[length - 1] == '\\')) || this.HasDosAttributes(16);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003A76 File Offset: 0x00002A76
		public bool IsFile
		{
			get
			{
				return !this.IsDirectory && !this.HasDosAttributes(8);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003A8C File Offset: 0x00002A8C
		public bool IsCompressionMethodSupported()
		{
			return ZipEntry.IsCompressionMethodSupported(this.CompressionMethod);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003A9C File Offset: 0x00002A9C
		public object Clone()
		{
			ZipEntry zipEntry = (ZipEntry)base.MemberwiseClone();
			if (this.extra != null)
			{
				zipEntry.extra = new byte[this.extra.Length];
				Array.Copy(this.extra, 0, zipEntry.extra, 0, this.extra.Length);
			}
			return zipEntry;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003AEC File Offset: 0x00002AEC
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003AF4 File Offset: 0x00002AF4
		public static bool IsCompressionMethodSupported(CompressionMethod method)
		{
			return method == CompressionMethod.Deflated || method == CompressionMethod.Stored;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003B00 File Offset: 0x00002B00
		public static string CleanName(string name)
		{
			if (name == null)
			{
				return string.Empty;
			}
			if (Path.IsPathRooted(name))
			{
				name = name.Substring(Path.GetPathRoot(name).Length);
			}
			name = name.Replace("\\", "/");
			while (name.Length > 0 && name[0] == '/')
			{
				name = name.Remove(0, 1);
			}
			return name;
		}

		// Token: 0x04000035 RID: 53
		private ZipEntry.Known known;

		// Token: 0x04000036 RID: 54
		private int externalFileAttributes;

		// Token: 0x04000037 RID: 55
		private ushort versionMadeBy;

		// Token: 0x04000038 RID: 56
		private string name;

		// Token: 0x04000039 RID: 57
		private ulong size;

		// Token: 0x0400003A RID: 58
		private ulong compressedSize;

		// Token: 0x0400003B RID: 59
		private ushort versionToExtract;

		// Token: 0x0400003C RID: 60
		private uint crc;

		// Token: 0x0400003D RID: 61
		private uint dosTime;

		// Token: 0x0400003E RID: 62
		private DateTime dateTime;

		// Token: 0x0400003F RID: 63
		private CompressionMethod method;

		// Token: 0x04000040 RID: 64
		private byte[] extra;

		// Token: 0x04000041 RID: 65
		private string comment;

		// Token: 0x04000042 RID: 66
		private int flags;

		// Token: 0x04000043 RID: 67
		private long zipFileIndex;

		// Token: 0x04000044 RID: 68
		private long offset;

		// Token: 0x04000045 RID: 69
		private bool forceZip64_;

		// Token: 0x04000046 RID: 70
		private byte cryptoCheckValue_;

		// Token: 0x04000047 RID: 71
		private int _aesVer;

		// Token: 0x04000048 RID: 72
		private int _aesEncryptionStrength;

		// Token: 0x0200000C RID: 12
		[Flags]
		private enum Known : byte
		{
			// Token: 0x0400004A RID: 74
			None = 0,
			// Token: 0x0400004B RID: 75
			Size = 1,
			// Token: 0x0400004C RID: 76
			CompressedSize = 2,
			// Token: 0x0400004D RID: 77
			Crc = 4,
			// Token: 0x0400004E RID: 78
			Time = 8,
			// Token: 0x0400004F RID: 79
			ExternalAttributes = 16
		}
	}
}
