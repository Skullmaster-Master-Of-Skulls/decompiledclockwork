using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Renci.SshNet.Common;

namespace Renci.SshNet.Sftp
{
	// Token: 0x02000035 RID: 53
	public class SftpFileAttributes
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000EE85 File Offset: 0x0000D085
		internal bool IsLastAccessTimeChanged
		{
			get
			{
				return this._originalLastAccessTime != this.LastAccessTime;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000EE98 File Offset: 0x0000D098
		internal bool IsLastWriteTimeChanged
		{
			get
			{
				return this._originalLastWriteTime != this.LastWriteTime;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000EEAB File Offset: 0x0000D0AB
		internal bool IsSizeChanged
		{
			get
			{
				return this._originalSize != this.Size;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000EEBE File Offset: 0x0000D0BE
		internal bool IsUserIdChanged
		{
			get
			{
				return this._originalUserId != this.UserId;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000EED1 File Offset: 0x0000D0D1
		internal bool IsGroupIdChanged
		{
			get
			{
				return this._originalGroupId != this.GroupId;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000EEE4 File Offset: 0x0000D0E4
		internal bool IsPermissionsChanged
		{
			get
			{
				return this._originalPermissions != this.Permissions;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000EEF7 File Offset: 0x0000D0F7
		internal bool IsExtensionsChanged
		{
			get
			{
				return this._originalExtensions != null && this.Extensions != null && !this._originalExtensions.SequenceEqual(this.Extensions);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000EF1F File Offset: 0x0000D11F
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x0000EF27 File Offset: 0x0000D127
		public DateTime LastAccessTime { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000EF30 File Offset: 0x0000D130
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x0000EF38 File Offset: 0x0000D138
		public DateTime LastWriteTime { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000EF41 File Offset: 0x0000D141
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x0000EF49 File Offset: 0x0000D149
		public long Size { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000EF52 File Offset: 0x0000D152
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x0000EF5A File Offset: 0x0000D15A
		public int UserId { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000EF63 File Offset: 0x0000D163
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000EF6B File Offset: 0x0000D16B
		public int GroupId { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000EF74 File Offset: 0x0000D174
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000EF7C File Offset: 0x0000D17C
		public bool IsSocket { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000EF85 File Offset: 0x0000D185
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0000EF8D File Offset: 0x0000D18D
		public bool IsSymbolicLink { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000EF96 File Offset: 0x0000D196
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x0000EF9E File Offset: 0x0000D19E
		public bool IsRegularFile { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000EFA7 File Offset: 0x0000D1A7
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x0000EFAF File Offset: 0x0000D1AF
		public bool IsBlockDevice { get; private set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000EFB8 File Offset: 0x0000D1B8
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x0000EFC0 File Offset: 0x0000D1C0
		public bool IsDirectory { get; private set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000EFC9 File Offset: 0x0000D1C9
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x0000EFD1 File Offset: 0x0000D1D1
		public bool IsCharacterDevice { get; private set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000EFDA File Offset: 0x0000D1DA
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0000EFE2 File Offset: 0x0000D1E2
		public bool IsNamedPipe { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000EFEB File Offset: 0x0000D1EB
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x0000EFF3 File Offset: 0x0000D1F3
		public bool OwnerCanRead { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000EFFC File Offset: 0x0000D1FC
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x0000F004 File Offset: 0x0000D204
		public bool OwnerCanWrite { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000F00D File Offset: 0x0000D20D
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x0000F015 File Offset: 0x0000D215
		public bool OwnerCanExecute { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000F01E File Offset: 0x0000D21E
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x0000F026 File Offset: 0x0000D226
		public bool GroupCanRead { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000F02F File Offset: 0x0000D22F
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x0000F037 File Offset: 0x0000D237
		public bool GroupCanWrite { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000F040 File Offset: 0x0000D240
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x0000F048 File Offset: 0x0000D248
		public bool GroupCanExecute { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000F051 File Offset: 0x0000D251
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x0000F059 File Offset: 0x0000D259
		public bool OthersCanRead { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000F062 File Offset: 0x0000D262
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x0000F06A File Offset: 0x0000D26A
		public bool OthersCanWrite { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000F073 File Offset: 0x0000D273
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x0000F07B File Offset: 0x0000D27B
		public bool OthersCanExecute { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000F084 File Offset: 0x0000D284
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x0000F08C File Offset: 0x0000D28C
		public IDictionary<string, string> Extensions { get; private set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000F098 File Offset: 0x0000D298
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x0000F1D0 File Offset: 0x0000D3D0
		internal uint Permissions
		{
			get
			{
				uint num = 0U;
				if (this._isBitFiledsBitSet)
				{
					num |= 61440U;
				}
				if (this.IsSocket)
				{
					num |= 49152U;
				}
				if (this.IsSymbolicLink)
				{
					num |= 40960U;
				}
				if (this.IsRegularFile)
				{
					num |= 32768U;
				}
				if (this.IsBlockDevice)
				{
					num |= 24576U;
				}
				if (this.IsDirectory)
				{
					num |= 16384U;
				}
				if (this.IsCharacterDevice)
				{
					num |= 8192U;
				}
				if (this.IsNamedPipe)
				{
					num |= 4096U;
				}
				if (this._isUIDBitSet)
				{
					num |= 2048U;
				}
				if (this._isGroupIDBitSet)
				{
					num |= 1024U;
				}
				if (this._isStickyBitSet)
				{
					num |= 512U;
				}
				if (this.OwnerCanRead)
				{
					num |= 256U;
				}
				if (this.OwnerCanWrite)
				{
					num |= 128U;
				}
				if (this.OwnerCanExecute)
				{
					num |= 64U;
				}
				if (this.GroupCanRead)
				{
					num |= 32U;
				}
				if (this.GroupCanWrite)
				{
					num |= 16U;
				}
				if (this.GroupCanExecute)
				{
					num |= 8U;
				}
				if (this.OthersCanRead)
				{
					num |= 4U;
				}
				if (this.OthersCanWrite)
				{
					num |= 2U;
				}
				if (this.OthersCanExecute)
				{
					num |= 1U;
				}
				return num;
			}
			private set
			{
				this._isBitFiledsBitSet = ((value & 61440U) == 61440U);
				this.IsSocket = ((value & 49152U) == 49152U);
				this.IsSymbolicLink = ((value & 40960U) == 40960U);
				this.IsRegularFile = ((value & 32768U) == 32768U);
				this.IsBlockDevice = ((value & 24576U) == 24576U);
				this.IsDirectory = ((value & 16384U) == 16384U);
				this.IsCharacterDevice = ((value & 8192U) == 8192U);
				this.IsNamedPipe = ((value & 4096U) == 4096U);
				this._isUIDBitSet = ((value & 2048U) == 2048U);
				this._isGroupIDBitSet = ((value & 1024U) == 1024U);
				this._isStickyBitSet = ((value & 512U) == 512U);
				this.OwnerCanRead = ((value & 256U) == 256U);
				this.OwnerCanWrite = ((value & 128U) == 128U);
				this.OwnerCanExecute = ((value & 64U) == 64U);
				this.GroupCanRead = ((value & 32U) == 32U);
				this.GroupCanWrite = ((value & 16U) == 16U);
				this.GroupCanExecute = ((value & 8U) == 8U);
				this.OthersCanRead = ((value & 4U) == 4U);
				this.OthersCanWrite = ((value & 2U) == 2U);
				this.OthersCanExecute = ((value & 1U) == 1U);
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000027FD File Offset: 0x000009FD
		private SftpFileAttributes()
		{
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000F33C File Offset: 0x0000D53C
		internal SftpFileAttributes(DateTime lastAccessTime, DateTime lastWriteTime, long size, int userId, int groupId, uint permissions, IDictionary<string, string> extensions)
		{
			this._originalLastAccessTime = lastAccessTime;
			this.LastAccessTime = lastAccessTime;
			this._originalLastWriteTime = lastWriteTime;
			this.LastWriteTime = lastWriteTime;
			this._originalSize = size;
			this.Size = size;
			this._originalUserId = userId;
			this.UserId = userId;
			this._originalGroupId = groupId;
			this.GroupId = groupId;
			this._originalPermissions = permissions;
			this.Permissions = permissions;
			this._originalExtensions = extensions;
			this.Extensions = extensions;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000F3C8 File Offset: 0x0000D5C8
		public void SetPermissions(short mode)
		{
			if (mode < 0 || mode > 999)
			{
				throw new ArgumentOutOfRangeException("mode");
			}
			char[] array = mode.ToString(CultureInfo.InvariantCulture).PadLeft(3, '0').ToCharArray();
			int num = (int)((array[0] & '\u000f') * '\b' * '\b' + (array[1] & '\u000f') * '\b' + (array[2] & '\u000f'));
			this.OwnerCanRead = (((long)num & 256L) == 256L);
			this.OwnerCanWrite = (((long)num & 128L) == 128L);
			this.OwnerCanExecute = (((long)num & 64L) == 64L);
			this.GroupCanRead = (((long)num & 32L) == 32L);
			this.GroupCanWrite = (((long)num & 16L) == 16L);
			this.GroupCanExecute = (((long)num & 8L) == 8L);
			this.OthersCanRead = (((long)num & 4L) == 4L);
			this.OthersCanWrite = (((long)num & 2L) == 2L);
			this.OthersCanExecute = (((long)num & 1L) == 1L);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		public byte[] GetBytes()
		{
			SshDataStream sshDataStream = new SshDataStream(4);
			uint num = 0U;
			if (this.IsSizeChanged && this.IsRegularFile)
			{
				num |= 1U;
			}
			if (this.IsUserIdChanged || this.IsGroupIdChanged)
			{
				num |= 2U;
			}
			if (this.IsPermissionsChanged)
			{
				num |= 4U;
			}
			if (this.IsLastAccessTimeChanged || this.IsLastWriteTimeChanged)
			{
				num |= 8U;
			}
			if (this.IsExtensionsChanged)
			{
				num |= 2147483648U;
			}
			sshDataStream.Write(num);
			if (this.IsSizeChanged && this.IsRegularFile)
			{
				sshDataStream.Write((ulong)this.Size);
			}
			if (this.IsUserIdChanged || this.IsGroupIdChanged)
			{
				sshDataStream.Write((uint)this.UserId);
				sshDataStream.Write((uint)this.GroupId);
			}
			if (this.IsPermissionsChanged)
			{
				sshDataStream.Write(this.Permissions);
			}
			if (this.IsLastAccessTimeChanged || this.IsLastWriteTimeChanged)
			{
				uint value = (uint)(this.LastAccessTime.ToFileTime() / 10000000L - 11644473600L);
				sshDataStream.Write(value);
				value = (uint)(this.LastWriteTime.ToFileTime() / 10000000L - 11644473600L);
				sshDataStream.Write(value);
			}
			if (this.IsExtensionsChanged)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.Extensions)
				{
					sshDataStream.Write(keyValuePair.Key, SshData.Ascii);
					sshDataStream.Write(keyValuePair.Value, SshData.Ascii);
				}
			}
			return sshDataStream.ToArray();
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000F660 File Offset: 0x0000D860
		internal static SftpFileAttributes FromBytes(SshDataStream stream)
		{
			uint num = stream.ReadUInt32();
			long size = -1L;
			int userId = -1;
			int groupId = -1;
			uint permissions = 0U;
			DateTime lastAccessTime = DateTime.MinValue;
			DateTime lastWriteTime = DateTime.MinValue;
			IDictionary<string, string> dictionary = null;
			if ((num & 1U) == 1U)
			{
				size = (long)stream.ReadUInt64();
			}
			if ((num & 2U) == 2U)
			{
				userId = (int)stream.ReadUInt32();
				groupId = (int)stream.ReadUInt32();
			}
			if ((num & 4U) == 4U)
			{
				permissions = stream.ReadUInt32();
			}
			if ((num & 8U) == 8U)
			{
				lastAccessTime = DateTime.FromFileTime((long)(((ulong)stream.ReadUInt32() + 11644473600UL) * 10000000UL));
				lastWriteTime = DateTime.FromFileTime((long)(((ulong)stream.ReadUInt32() + 11644473600UL) * 10000000UL));
			}
			if ((num & 2147483648U) == 2147483648U)
			{
				int num2 = (int)stream.ReadUInt32();
				dictionary = new Dictionary<string, string>(num2);
				for (int i = 0; i < num2; i++)
				{
					string key = stream.ReadString(SshData.Utf8);
					string value = stream.ReadString(SshData.Utf8);
					dictionary.Add(key, value);
				}
			}
			return new SftpFileAttributes(lastAccessTime, lastWriteTime, size, userId, groupId, permissions, dictionary);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000F764 File Offset: 0x0000D964
		internal static SftpFileAttributes FromBytes(byte[] buffer)
		{
			SftpFileAttributes result;
			using (SshDataStream sshDataStream = new SshDataStream(buffer))
			{
				result = SftpFileAttributes.FromBytes(sshDataStream);
			}
			return result;
		}

		// Token: 0x04000134 RID: 308
		private const uint S_IFMT = 61440U;

		// Token: 0x04000135 RID: 309
		private const uint S_IFSOCK = 49152U;

		// Token: 0x04000136 RID: 310
		private const uint S_IFLNK = 40960U;

		// Token: 0x04000137 RID: 311
		private const uint S_IFREG = 32768U;

		// Token: 0x04000138 RID: 312
		private const uint S_IFBLK = 24576U;

		// Token: 0x04000139 RID: 313
		private const uint S_IFDIR = 16384U;

		// Token: 0x0400013A RID: 314
		private const uint S_IFCHR = 8192U;

		// Token: 0x0400013B RID: 315
		private const uint S_IFIFO = 4096U;

		// Token: 0x0400013C RID: 316
		private const uint S_ISUID = 2048U;

		// Token: 0x0400013D RID: 317
		private const uint S_ISGID = 1024U;

		// Token: 0x0400013E RID: 318
		private const uint S_ISVTX = 512U;

		// Token: 0x0400013F RID: 319
		private const uint S_IRUSR = 256U;

		// Token: 0x04000140 RID: 320
		private const uint S_IWUSR = 128U;

		// Token: 0x04000141 RID: 321
		private const uint S_IXUSR = 64U;

		// Token: 0x04000142 RID: 322
		private const uint S_IRGRP = 32U;

		// Token: 0x04000143 RID: 323
		private const uint S_IWGRP = 16U;

		// Token: 0x04000144 RID: 324
		private const uint S_IXGRP = 8U;

		// Token: 0x04000145 RID: 325
		private const uint S_IROTH = 4U;

		// Token: 0x04000146 RID: 326
		private const uint S_IWOTH = 2U;

		// Token: 0x04000147 RID: 327
		private const uint S_IXOTH = 1U;

		// Token: 0x04000148 RID: 328
		private bool _isBitFiledsBitSet;

		// Token: 0x04000149 RID: 329
		private bool _isUIDBitSet;

		// Token: 0x0400014A RID: 330
		private bool _isGroupIDBitSet;

		// Token: 0x0400014B RID: 331
		private bool _isStickyBitSet;

		// Token: 0x0400014C RID: 332
		private readonly DateTime _originalLastAccessTime;

		// Token: 0x0400014D RID: 333
		private readonly DateTime _originalLastWriteTime;

		// Token: 0x0400014E RID: 334
		private readonly long _originalSize;

		// Token: 0x0400014F RID: 335
		private readonly int _originalUserId;

		// Token: 0x04000150 RID: 336
		private readonly int _originalGroupId;

		// Token: 0x04000151 RID: 337
		private readonly uint _originalPermissions;

		// Token: 0x04000152 RID: 338
		private readonly IDictionary<string, string> _originalExtensions;

		// Token: 0x04000169 RID: 361
		internal static readonly SftpFileAttributes Empty = new SftpFileAttributes();
	}
}
