using System;
using System.Text;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x0200003C RID: 60
	public class TarHeader : ICloneable
	{
		// Token: 0x06000253 RID: 595 RVA: 0x0000E8E0 File Offset: 0x0000D8E0
		public TarHeader()
		{
			this.Magic = "ustar ";
			this.Version = " ";
			this.Name = "";
			this.LinkName = "";
			this.UserId = TarHeader.defaultUserId;
			this.GroupId = TarHeader.defaultGroupId;
			this.UserName = TarHeader.defaultUser;
			this.GroupName = TarHeader.defaultGroupName;
			this.Size = 0L;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000E953 File Offset: 0x0000D953
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000E95B File Offset: 0x0000D95B
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.name = value;
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000E972 File Offset: 0x0000D972
		[Obsolete("Use the Name property instead", true)]
		public string GetName()
		{
			return this.name;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000E97A File Offset: 0x0000D97A
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000E982 File Offset: 0x0000D982
		public int Mode
		{
			get
			{
				return this.mode;
			}
			set
			{
				this.mode = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000E98B File Offset: 0x0000D98B
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000E993 File Offset: 0x0000D993
		public int UserId
		{
			get
			{
				return this.userId;
			}
			set
			{
				this.userId = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000E99C File Offset: 0x0000D99C
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000E9A4 File Offset: 0x0000D9A4
		public int GroupId
		{
			get
			{
				return this.groupId;
			}
			set
			{
				this.groupId = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000E9AD File Offset: 0x0000D9AD
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000E9B5 File Offset: 0x0000D9B5
		public long Size
		{
			get
			{
				return this.size;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Cannot be less than zero");
				}
				this.size = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000E9D3 File Offset: 0x0000D9D3
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000E9DC File Offset: 0x0000D9DC
		public DateTime ModTime
		{
			get
			{
				return this.modTime;
			}
			set
			{
				if (value < TarHeader.dateTime1970)
				{
					throw new ArgumentOutOfRangeException("value", "ModTime cannot be before Jan 1st 1970");
				}
				this.modTime = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000EA3B File Offset: 0x0000DA3B
		public int Checksum
		{
			get
			{
				return this.checksum;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000EA43 File Offset: 0x0000DA43
		public bool IsChecksumValid
		{
			get
			{
				return this.isChecksumValid;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000EA4B File Offset: 0x0000DA4B
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000EA53 File Offset: 0x0000DA53
		public byte TypeFlag
		{
			get
			{
				return this.typeFlag;
			}
			set
			{
				this.typeFlag = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000EA5C File Offset: 0x0000DA5C
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0000EA64 File Offset: 0x0000DA64
		public string LinkName
		{
			get
			{
				return this.linkName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.linkName = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000EA7B File Offset: 0x0000DA7B
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000EA83 File Offset: 0x0000DA83
		public string Magic
		{
			get
			{
				return this.magic;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.magic = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000EA9A File Offset: 0x0000DA9A
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000EAA2 File Offset: 0x0000DAA2
		public string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.version = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000EAB9 File Offset: 0x0000DAB9
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000EAC4 File Offset: 0x0000DAC4
		public string UserName
		{
			get
			{
				return this.userName;
			}
			set
			{
				if (value != null)
				{
					this.userName = value.Substring(0, Math.Min(32, value.Length));
					return;
				}
				string text = Environment.UserName;
				if (text.Length > 32)
				{
					text = text.Substring(0, 32);
				}
				this.userName = text;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000EB10 File Offset: 0x0000DB10
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000EB18 File Offset: 0x0000DB18
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
			set
			{
				if (value == null)
				{
					this.groupName = "None";
					return;
				}
				this.groupName = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000EB30 File Offset: 0x0000DB30
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000EB38 File Offset: 0x0000DB38
		public int DevMajor
		{
			get
			{
				return this.devMajor;
			}
			set
			{
				this.devMajor = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000EB41 File Offset: 0x0000DB41
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000EB49 File Offset: 0x0000DB49
		public int DevMinor
		{
			get
			{
				return this.devMinor;
			}
			set
			{
				this.devMinor = value;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000EB52 File Offset: 0x0000DB52
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000EB5C File Offset: 0x0000DB5C
		public void ParseBuffer(byte[] header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			int num = 0;
			this.name = TarHeader.ParseName(header, num, 100).ToString();
			num += 100;
			this.mode = (int)TarHeader.ParseOctal(header, num, 8);
			num += 8;
			this.UserId = (int)TarHeader.ParseOctal(header, num, 8);
			num += 8;
			this.GroupId = (int)TarHeader.ParseOctal(header, num, 8);
			num += 8;
			this.Size = TarHeader.ParseBinaryOrOctal(header, num, 12);
			num += 12;
			this.ModTime = TarHeader.GetDateTimeFromCTime(TarHeader.ParseOctal(header, num, 12));
			num += 12;
			this.checksum = (int)TarHeader.ParseOctal(header, num, 8);
			num += 8;
			this.TypeFlag = header[num++];
			this.LinkName = TarHeader.ParseName(header, num, 100).ToString();
			num += 100;
			this.Magic = TarHeader.ParseName(header, num, 6).ToString();
			num += 6;
			if (this.Magic == "ustar")
			{
				this.Version = TarHeader.ParseName(header, num, 2).ToString();
				num += 2;
				this.UserName = TarHeader.ParseName(header, num, 32).ToString();
				num += 32;
				this.GroupName = TarHeader.ParseName(header, num, 32).ToString();
				num += 32;
				this.DevMajor = (int)TarHeader.ParseOctal(header, num, 8);
				num += 8;
				this.DevMinor = (int)TarHeader.ParseOctal(header, num, 8);
				num += 8;
				string text = TarHeader.ParseName(header, num, 155).ToString();
				if (!string.IsNullOrEmpty(text))
				{
					this.Name = text + '/' + this.Name;
				}
			}
			this.isChecksumValid = (this.Checksum == TarHeader.MakeCheckSum(header));
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000ED14 File Offset: 0x0000DD14
		public void WriteHeader(byte[] outBuffer)
		{
			if (outBuffer == null)
			{
				throw new ArgumentNullException("outBuffer");
			}
			int i = 0;
			i = TarHeader.GetNameBytes(this.Name, outBuffer, i, 100);
			i = TarHeader.GetOctalBytes((long)this.mode, outBuffer, i, 8);
			i = TarHeader.GetOctalBytes((long)this.UserId, outBuffer, i, 8);
			i = TarHeader.GetOctalBytes((long)this.GroupId, outBuffer, i, 8);
			i = TarHeader.GetBinaryOrOctalBytes(this.Size, outBuffer, i, 12);
			i = TarHeader.GetOctalBytes((long)TarHeader.GetCTime(this.ModTime), outBuffer, i, 12);
			int offset = i;
			for (int j = 0; j < 8; j++)
			{
				outBuffer[i++] = 32;
			}
			outBuffer[i++] = this.TypeFlag;
			i = TarHeader.GetNameBytes(this.LinkName, outBuffer, i, 100);
			i = TarHeader.GetAsciiBytes(this.Magic, 0, outBuffer, i, 6);
			i = TarHeader.GetNameBytes(this.Version, outBuffer, i, 2);
			i = TarHeader.GetNameBytes(this.UserName, outBuffer, i, 32);
			i = TarHeader.GetNameBytes(this.GroupName, outBuffer, i, 32);
			if (this.TypeFlag == 51 || this.TypeFlag == 52)
			{
				i = TarHeader.GetOctalBytes((long)this.DevMajor, outBuffer, i, 8);
				i = TarHeader.GetOctalBytes((long)this.DevMinor, outBuffer, i, 8);
			}
			while (i < outBuffer.Length)
			{
				outBuffer[i++] = 0;
			}
			this.checksum = TarHeader.ComputeCheckSum(outBuffer);
			TarHeader.GetCheckSumOctalBytes((long)this.checksum, outBuffer, offset, 8);
			this.isChecksumValid = true;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000EE70 File Offset: 0x0000DE70
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000EE80 File Offset: 0x0000DE80
		public override bool Equals(object obj)
		{
			TarHeader tarHeader = obj as TarHeader;
			return tarHeader != null && (this.name == tarHeader.name && this.mode == tarHeader.mode && this.UserId == tarHeader.UserId && this.GroupId == tarHeader.GroupId && this.Size == tarHeader.Size && this.ModTime == tarHeader.ModTime && this.Checksum == tarHeader.Checksum && this.TypeFlag == tarHeader.TypeFlag && this.LinkName == tarHeader.LinkName && this.Magic == tarHeader.Magic && this.Version == tarHeader.Version && this.UserName == tarHeader.UserName && this.GroupName == tarHeader.GroupName && this.DevMajor == tarHeader.DevMajor) && this.DevMinor == tarHeader.DevMinor;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000EFAD File Offset: 0x0000DFAD
		internal static void SetValueDefaults(int userId, string userName, int groupId, string groupName)
		{
			TarHeader.userIdAsSet = userId;
			TarHeader.defaultUserId = userId;
			TarHeader.userNameAsSet = userName;
			TarHeader.defaultUser = userName;
			TarHeader.groupIdAsSet = groupId;
			TarHeader.defaultGroupId = groupId;
			TarHeader.groupNameAsSet = groupName;
			TarHeader.defaultGroupName = groupName;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000EFDF File Offset: 0x0000DFDF
		internal static void RestoreSetValues()
		{
			TarHeader.defaultUserId = TarHeader.userIdAsSet;
			TarHeader.defaultUser = TarHeader.userNameAsSet;
			TarHeader.defaultGroupId = TarHeader.groupIdAsSet;
			TarHeader.defaultGroupName = TarHeader.groupNameAsSet;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000F00C File Offset: 0x0000E00C
		private static long ParseBinaryOrOctal(byte[] header, int offset, int length)
		{
			if (header[offset] >= 128)
			{
				long num = 0L;
				for (int i = length - 8; i < length; i++)
				{
					num = (num << 8 | (long)((ulong)header[offset + i]));
				}
				return num;
			}
			return TarHeader.ParseOctal(header, offset, length);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000F04C File Offset: 0x0000E04C
		public static long ParseOctal(byte[] header, int offset, int length)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			long num = 0L;
			bool flag = true;
			int num2 = offset + length;
			int num3 = offset;
			while (num3 < num2 && header[num3] != 0)
			{
				if (header[num3] != 32 && header[num3] != 48)
				{
					goto IL_38;
				}
				if (!flag)
				{
					if (header[num3] != 32)
					{
						goto IL_38;
					}
					break;
				}
				IL_46:
				num3++;
				continue;
				IL_38:
				flag = false;
				num = (num << 3) + (long)(header[num3] - 48);
				goto IL_46;
			}
			return num;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000F0A8 File Offset: 0x0000E0A8
		public static StringBuilder ParseName(byte[] header, int offset, int length)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Cannot be less than zero");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Cannot be less than zero");
			}
			if (offset + length > header.Length)
			{
				throw new ArgumentException("Exceeds header size", "length");
			}
			StringBuilder stringBuilder = new StringBuilder(length);
			int num = offset;
			while (num < offset + length && header[num] != 0)
			{
				stringBuilder.Append((char)header[num]);
				num++;
			}
			return stringBuilder;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000F128 File Offset: 0x0000E128
		public static int GetNameBytes(StringBuilder name, int nameOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return TarHeader.GetNameBytes(name.ToString(), nameOffset, buffer, bufferOffset, length);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000F158 File Offset: 0x0000E158
		public static int GetNameBytes(string name, int nameOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int i;
			for (i = 0; i < length; i++)
			{
				if (nameOffset + i >= name.Length)
				{
					break;
				}
				buffer[bufferOffset + i] = (byte)name[nameOffset + i];
			}
			while (i < length)
			{
				buffer[bufferOffset + i] = 0;
				i++;
			}
			return bufferOffset + length;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000F1BD File Offset: 0x0000E1BD
		public static int GetNameBytes(StringBuilder name, byte[] buffer, int offset, int length)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return TarHeader.GetNameBytes(name.ToString(), 0, buffer, offset, length);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000F1EA File Offset: 0x0000E1EA
		public static int GetNameBytes(string name, byte[] buffer, int offset, int length)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return TarHeader.GetNameBytes(name, 0, buffer, offset, length);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000F214 File Offset: 0x0000E214
		public static int GetAsciiBytes(string toAdd, int nameOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (toAdd == null)
			{
				throw new ArgumentNullException("toAdd");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = 0;
			while (num < length && nameOffset + num < toAdd.Length)
			{
				buffer[bufferOffset + num] = (byte)toAdd[nameOffset + num];
				num++;
			}
			return bufferOffset + length;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000F268 File Offset: 0x0000E268
		public static int GetOctalBytes(long value, byte[] buffer, int offset, int length)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int i = length - 1;
			buffer[offset + i] = 0;
			i--;
			if (value > 0L)
			{
				long num = value;
				while (i >= 0)
				{
					if (num <= 0L)
					{
						break;
					}
					buffer[offset + i] = 48 + (byte)(num & 7L);
					num >>= 3;
					i--;
				}
			}
			while (i >= 0)
			{
				buffer[offset + i] = 48;
				i--;
			}
			return offset + length;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000F2D0 File Offset: 0x0000E2D0
		private static int GetBinaryOrOctalBytes(long value, byte[] buffer, int offset, int length)
		{
			if (value > 8589934591L)
			{
				for (int i = length - 1; i > 0; i--)
				{
					buffer[offset + i] = (byte)value;
					value >>= 8;
				}
				buffer[offset] = 128;
				return offset + length;
			}
			return TarHeader.GetOctalBytes(value, buffer, offset, length);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000F318 File Offset: 0x0000E318
		private static void GetCheckSumOctalBytes(long value, byte[] buffer, int offset, int length)
		{
			TarHeader.GetOctalBytes(value, buffer, offset, length - 1);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000F328 File Offset: 0x0000E328
		private static int ComputeCheckSum(byte[] buffer)
		{
			int num = 0;
			for (int i = 0; i < buffer.Length; i++)
			{
				num += (int)buffer[i];
			}
			return num;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000F34C File Offset: 0x0000E34C
		private static int MakeCheckSum(byte[] buffer)
		{
			int num = 0;
			for (int i = 0; i < 148; i++)
			{
				num += (int)buffer[i];
			}
			for (int j = 0; j < 8; j++)
			{
				num += 32;
			}
			for (int k = 156; k < buffer.Length; k++)
			{
				num += (int)buffer[k];
			}
			return num;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000F39C File Offset: 0x0000E39C
		private static int GetCTime(DateTime dateTime)
		{
			return (int)((dateTime.Ticks - TarHeader.dateTime1970.Ticks) / 10000000L);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000F3C8 File Offset: 0x0000E3C8
		private static DateTime GetDateTimeFromCTime(long ticks)
		{
			DateTime result;
			try
			{
				result = new DateTime(TarHeader.dateTime1970.Ticks + ticks * 10000000L);
			}
			catch (ArgumentOutOfRangeException)
			{
				result = TarHeader.dateTime1970;
			}
			return result;
		}

		// Token: 0x04000186 RID: 390
		public const int NAMELEN = 100;

		// Token: 0x04000187 RID: 391
		public const int MODELEN = 8;

		// Token: 0x04000188 RID: 392
		public const int UIDLEN = 8;

		// Token: 0x04000189 RID: 393
		public const int GIDLEN = 8;

		// Token: 0x0400018A RID: 394
		public const int CHKSUMLEN = 8;

		// Token: 0x0400018B RID: 395
		public const int CHKSUMOFS = 148;

		// Token: 0x0400018C RID: 396
		public const int SIZELEN = 12;

		// Token: 0x0400018D RID: 397
		public const int MAGICLEN = 6;

		// Token: 0x0400018E RID: 398
		public const int VERSIONLEN = 2;

		// Token: 0x0400018F RID: 399
		public const int MODTIMELEN = 12;

		// Token: 0x04000190 RID: 400
		public const int UNAMELEN = 32;

		// Token: 0x04000191 RID: 401
		public const int GNAMELEN = 32;

		// Token: 0x04000192 RID: 402
		public const int DEVLEN = 8;

		// Token: 0x04000193 RID: 403
		public const int PREFIXLEN = 155;

		// Token: 0x04000194 RID: 404
		public const byte LF_OLDNORM = 0;

		// Token: 0x04000195 RID: 405
		public const byte LF_NORMAL = 48;

		// Token: 0x04000196 RID: 406
		public const byte LF_LINK = 49;

		// Token: 0x04000197 RID: 407
		public const byte LF_SYMLINK = 50;

		// Token: 0x04000198 RID: 408
		public const byte LF_CHR = 51;

		// Token: 0x04000199 RID: 409
		public const byte LF_BLK = 52;

		// Token: 0x0400019A RID: 410
		public const byte LF_DIR = 53;

		// Token: 0x0400019B RID: 411
		public const byte LF_FIFO = 54;

		// Token: 0x0400019C RID: 412
		public const byte LF_CONTIG = 55;

		// Token: 0x0400019D RID: 413
		public const byte LF_GHDR = 103;

		// Token: 0x0400019E RID: 414
		public const byte LF_XHDR = 120;

		// Token: 0x0400019F RID: 415
		public const byte LF_ACL = 65;

		// Token: 0x040001A0 RID: 416
		public const byte LF_GNU_DUMPDIR = 68;

		// Token: 0x040001A1 RID: 417
		public const byte LF_EXTATTR = 69;

		// Token: 0x040001A2 RID: 418
		public const byte LF_META = 73;

		// Token: 0x040001A3 RID: 419
		public const byte LF_GNU_LONGLINK = 75;

		// Token: 0x040001A4 RID: 420
		public const byte LF_GNU_LONGNAME = 76;

		// Token: 0x040001A5 RID: 421
		public const byte LF_GNU_MULTIVOL = 77;

		// Token: 0x040001A6 RID: 422
		public const byte LF_GNU_NAMES = 78;

		// Token: 0x040001A7 RID: 423
		public const byte LF_GNU_SPARSE = 83;

		// Token: 0x040001A8 RID: 424
		public const byte LF_GNU_VOLHDR = 86;

		// Token: 0x040001A9 RID: 425
		public const string TMAGIC = "ustar ";

		// Token: 0x040001AA RID: 426
		public const string GNU_TMAGIC = "ustar  ";

		// Token: 0x040001AB RID: 427
		private const long timeConversionFactor = 10000000L;

		// Token: 0x040001AC RID: 428
		private static readonly DateTime dateTime1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0);

		// Token: 0x040001AD RID: 429
		private string name;

		// Token: 0x040001AE RID: 430
		private int mode;

		// Token: 0x040001AF RID: 431
		private int userId;

		// Token: 0x040001B0 RID: 432
		private int groupId;

		// Token: 0x040001B1 RID: 433
		private long size;

		// Token: 0x040001B2 RID: 434
		private DateTime modTime;

		// Token: 0x040001B3 RID: 435
		private int checksum;

		// Token: 0x040001B4 RID: 436
		private bool isChecksumValid;

		// Token: 0x040001B5 RID: 437
		private byte typeFlag;

		// Token: 0x040001B6 RID: 438
		private string linkName;

		// Token: 0x040001B7 RID: 439
		private string magic;

		// Token: 0x040001B8 RID: 440
		private string version;

		// Token: 0x040001B9 RID: 441
		private string userName;

		// Token: 0x040001BA RID: 442
		private string groupName;

		// Token: 0x040001BB RID: 443
		private int devMajor;

		// Token: 0x040001BC RID: 444
		private int devMinor;

		// Token: 0x040001BD RID: 445
		internal static int userIdAsSet;

		// Token: 0x040001BE RID: 446
		internal static int groupIdAsSet;

		// Token: 0x040001BF RID: 447
		internal static string userNameAsSet;

		// Token: 0x040001C0 RID: 448
		internal static string groupNameAsSet = "None";

		// Token: 0x040001C1 RID: 449
		internal static int defaultUserId;

		// Token: 0x040001C2 RID: 450
		internal static int defaultGroupId;

		// Token: 0x040001C3 RID: 451
		internal static string defaultGroupName = "None";

		// Token: 0x040001C4 RID: 452
		internal static string defaultUser;
	}
}
