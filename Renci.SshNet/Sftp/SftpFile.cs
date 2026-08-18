using System;
using System.Globalization;
using Renci.SshNet.Common;

namespace Renci.SshNet.Sftp
{
	// Token: 0x02000034 RID: 52
	public class SftpFile
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000EA94 File Offset: 0x0000CC94
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000EA9C File Offset: 0x0000CC9C
		public SftpFileAttributes Attributes { get; private set; }

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000EAA8 File Offset: 0x0000CCA8
		internal SftpFile(ISftpSession sftpSession, string fullName, SftpFileAttributes attributes)
		{
			if (sftpSession == null)
			{
				throw new SshConnectionException("Client not connected.");
			}
			if (attributes == null)
			{
				throw new ArgumentNullException("attributes");
			}
			if (fullName == null)
			{
				throw new ArgumentNullException("fullName");
			}
			this._sftpSession = sftpSession;
			this.Attributes = attributes;
			this.Name = fullName.Substring(fullName.LastIndexOf('/') + 1);
			this.FullName = fullName;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000EB10 File Offset: 0x0000CD10
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x0000EB18 File Offset: 0x0000CD18
		public string FullName { get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000EB21 File Offset: 0x0000CD21
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x0000EB29 File Offset: 0x0000CD29
		public string Name { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000EB32 File Offset: 0x0000CD32
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x0000EB3F File Offset: 0x0000CD3F
		public DateTime LastAccessTime
		{
			get
			{
				return this.Attributes.LastAccessTime;
			}
			set
			{
				this.Attributes.LastAccessTime = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000EB4D File Offset: 0x0000CD4D
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0000EB5A File Offset: 0x0000CD5A
		public DateTime LastWriteTime
		{
			get
			{
				return this.Attributes.LastWriteTime;
			}
			set
			{
				this.Attributes.LastWriteTime = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000EB68 File Offset: 0x0000CD68
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0000EB88 File Offset: 0x0000CD88
		public DateTime LastAccessTimeUtc
		{
			get
			{
				return this.Attributes.LastAccessTime.ToUniversalTime();
			}
			set
			{
				this.Attributes.LastAccessTime = value.ToLocalTime();
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000EB9C File Offset: 0x0000CD9C
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0000EBBC File Offset: 0x0000CDBC
		public DateTime LastWriteTimeUtc
		{
			get
			{
				return this.Attributes.LastWriteTime.ToUniversalTime();
			}
			set
			{
				this.Attributes.LastWriteTime = value.ToLocalTime();
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		public long Length
		{
			get
			{
				return this.Attributes.Size;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000EBDD File Offset: 0x0000CDDD
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0000EBEA File Offset: 0x0000CDEA
		public int UserId
		{
			get
			{
				return this.Attributes.UserId;
			}
			set
			{
				this.Attributes.UserId = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000EBF8 File Offset: 0x0000CDF8
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x0000EC05 File Offset: 0x0000CE05
		public int GroupId
		{
			get
			{
				return this.Attributes.GroupId;
			}
			set
			{
				this.Attributes.GroupId = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000EC13 File Offset: 0x0000CE13
		public bool IsSocket
		{
			get
			{
				return this.Attributes.IsSocket;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000EC20 File Offset: 0x0000CE20
		public bool IsSymbolicLink
		{
			get
			{
				return this.Attributes.IsSymbolicLink;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000EC2D File Offset: 0x0000CE2D
		public bool IsRegularFile
		{
			get
			{
				return this.Attributes.IsRegularFile;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000EC3A File Offset: 0x0000CE3A
		public bool IsBlockDevice
		{
			get
			{
				return this.Attributes.IsBlockDevice;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000EC47 File Offset: 0x0000CE47
		public bool IsDirectory
		{
			get
			{
				return this.Attributes.IsDirectory;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000EC54 File Offset: 0x0000CE54
		public bool IsCharacterDevice
		{
			get
			{
				return this.Attributes.IsCharacterDevice;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000EC61 File Offset: 0x0000CE61
		public bool IsNamedPipe
		{
			get
			{
				return this.Attributes.IsNamedPipe;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000EC6E File Offset: 0x0000CE6E
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x0000EC7B File Offset: 0x0000CE7B
		public bool OwnerCanRead
		{
			get
			{
				return this.Attributes.OwnerCanRead;
			}
			set
			{
				this.Attributes.OwnerCanRead = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000EC89 File Offset: 0x0000CE89
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x0000EC96 File Offset: 0x0000CE96
		public bool OwnerCanWrite
		{
			get
			{
				return this.Attributes.OwnerCanWrite;
			}
			set
			{
				this.Attributes.OwnerCanWrite = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000ECA4 File Offset: 0x0000CEA4
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x0000ECB1 File Offset: 0x0000CEB1
		public bool OwnerCanExecute
		{
			get
			{
				return this.Attributes.OwnerCanExecute;
			}
			set
			{
				this.Attributes.OwnerCanExecute = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000ECBF File Offset: 0x0000CEBF
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x0000ECCC File Offset: 0x0000CECC
		public bool GroupCanRead
		{
			get
			{
				return this.Attributes.GroupCanRead;
			}
			set
			{
				this.Attributes.GroupCanRead = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000ECDA File Offset: 0x0000CEDA
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x0000ECE7 File Offset: 0x0000CEE7
		public bool GroupCanWrite
		{
			get
			{
				return this.Attributes.GroupCanWrite;
			}
			set
			{
				this.Attributes.GroupCanWrite = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000ECF5 File Offset: 0x0000CEF5
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x0000ED02 File Offset: 0x0000CF02
		public bool GroupCanExecute
		{
			get
			{
				return this.Attributes.GroupCanExecute;
			}
			set
			{
				this.Attributes.GroupCanExecute = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000ED10 File Offset: 0x0000CF10
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0000ED1D File Offset: 0x0000CF1D
		public bool OthersCanRead
		{
			get
			{
				return this.Attributes.OthersCanRead;
			}
			set
			{
				this.Attributes.OthersCanRead = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000ED2B File Offset: 0x0000CF2B
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x0000ED38 File Offset: 0x0000CF38
		public bool OthersCanWrite
		{
			get
			{
				return this.Attributes.OthersCanWrite;
			}
			set
			{
				this.Attributes.OthersCanWrite = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000ED46 File Offset: 0x0000CF46
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x0000ED53 File Offset: 0x0000CF53
		public bool OthersCanExecute
		{
			get
			{
				return this.Attributes.OthersCanExecute;
			}
			set
			{
				this.Attributes.OthersCanExecute = value;
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000ED61 File Offset: 0x0000CF61
		public void SetPermissions(short mode)
		{
			this.Attributes.SetPermissions(mode);
			this.UpdateStatus();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000ED75 File Offset: 0x0000CF75
		public void Delete()
		{
			if (this.IsDirectory)
			{
				this._sftpSession.RequestRmDir(this.FullName);
				return;
			}
			this._sftpSession.RequestRemove(this.FullName);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000EDA4 File Offset: 0x0000CFA4
		public void MoveTo(string destFileName)
		{
			if (destFileName == null)
			{
				throw new ArgumentNullException("destFileName");
			}
			this._sftpSession.RequestRename(this.FullName, destFileName);
			string canonicalPath = this._sftpSession.GetCanonicalPath(destFileName);
			this.Name = canonicalPath.Substring(canonicalPath.LastIndexOf('/') + 1);
			this.FullName = canonicalPath;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000EDFB File Offset: 0x0000CFFB
		public void UpdateStatus()
		{
			this._sftpSession.RequestSetStat(this.FullName, this.Attributes);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000EE14 File Offset: 0x0000D014
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "Name {0}, Length {1}, User ID {2}, Group ID {3}, Accessed {4}, Modified {5}", new object[]
			{
				this.Name,
				this.Length,
				this.UserId,
				this.GroupId,
				this.LastAccessTime,
				this.LastWriteTime
			});
		}

		// Token: 0x04000130 RID: 304
		private readonly ISftpSession _sftpSession;
	}
}
