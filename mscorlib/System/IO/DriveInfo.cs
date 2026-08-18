using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.IO
{
	// Token: 0x020005B1 RID: 1457
	[ComVisible(true)]
	[Serializable]
	public sealed class DriveInfo : ISerializable
	{
		// Token: 0x0600359E RID: 13726 RVA: 0x000B2B1C File Offset: 0x000B1B1C
		public DriveInfo(string driveName)
		{
			if (driveName == null)
			{
				throw new ArgumentNullException("driveName");
			}
			if (driveName.Length == 1)
			{
				this._name = driveName + ":\\";
			}
			else
			{
				Path.CheckInvalidPathChars(driveName);
				this._name = Path.GetPathRoot(driveName);
				if (this._name == null || this._name.Length == 0 || this._name.StartsWith("\\\\", StringComparison.Ordinal))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDriveLetterOrRootDir"));
				}
			}
			if (this._name.Length == 2 && this._name[1] == ':')
			{
				this._name += "\\";
			}
			char c = driveName[0];
			if ((c < 'A' || c > 'Z') && (c < 'a' || c > 'z'))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDriveLetterOrRootDir"));
			}
			string path = this._name + '.';
			new FileIOPermission(FileIOPermissionAccess.PathDiscovery, path).Demand();
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000B2C24 File Offset: 0x000B1C24
		private DriveInfo(SerializationInfo info, StreamingContext context)
		{
			this._name = (string)info.GetValue("_name", typeof(string));
			string path = this._name + '.';
			new FileIOPermission(FileIOPermissionAccess.PathDiscovery, path).Demand();
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060035A0 RID: 13728 RVA: 0x000B2C76 File Offset: 0x000B1C76
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060035A1 RID: 13729 RVA: 0x000B2C7E File Offset: 0x000B1C7E
		public DriveType DriveType
		{
			get
			{
				return (DriveType)Win32Native.GetDriveType(this.Name);
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060035A2 RID: 13730 RVA: 0x000B2C8C File Offset: 0x000B1C8C
		public string DriveFormat
		{
			get
			{
				StringBuilder volumeName = new StringBuilder(50);
				StringBuilder stringBuilder = new StringBuilder(50);
				int errorMode = Win32Native.SetErrorMode(1);
				try
				{
					int num;
					int num2;
					int num3;
					if (!Win32Native.GetVolumeInformation(this.Name, volumeName, 50, out num, out num2, out num3, stringBuilder, 50))
					{
						int num4 = Marshal.GetLastWin32Error();
						if (num4 == 13)
						{
							num4 = 15;
						}
						__Error.WinIODriveError(this.Name, num4);
					}
				}
				finally
				{
					Win32Native.SetErrorMode(errorMode);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060035A3 RID: 13731 RVA: 0x000B2D10 File Offset: 0x000B1D10
		public bool IsReady
		{
			get
			{
				return Directory.InternalExists(this.Name);
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060035A4 RID: 13732 RVA: 0x000B2D20 File Offset: 0x000B1D20
		public long AvailableFreeSpace
		{
			get
			{
				int errorMode = Win32Native.SetErrorMode(1);
				long result;
				try
				{
					long num;
					long num2;
					if (!Win32Native.GetDiskFreeSpaceEx(this.Name, out result, out num, out num2))
					{
						__Error.WinIODriveError(this.Name);
					}
				}
				finally
				{
					Win32Native.SetErrorMode(errorMode);
				}
				return result;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060035A5 RID: 13733 RVA: 0x000B2D74 File Offset: 0x000B1D74
		public long TotalFreeSpace
		{
			get
			{
				int errorMode = Win32Native.SetErrorMode(1);
				long result;
				try
				{
					long num;
					long num2;
					if (!Win32Native.GetDiskFreeSpaceEx(this.Name, out num, out num2, out result))
					{
						__Error.WinIODriveError(this.Name);
					}
				}
				finally
				{
					Win32Native.SetErrorMode(errorMode);
				}
				return result;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060035A6 RID: 13734 RVA: 0x000B2DC8 File Offset: 0x000B1DC8
		public long TotalSize
		{
			get
			{
				int errorMode = Win32Native.SetErrorMode(1);
				long result;
				try
				{
					long num;
					long num2;
					if (!Win32Native.GetDiskFreeSpaceEx(this.Name, out num, out result, out num2))
					{
						__Error.WinIODriveError(this.Name);
					}
				}
				finally
				{
					Win32Native.SetErrorMode(errorMode);
				}
				return result;
			}
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x000B2E1C File Offset: 0x000B1E1C
		public static DriveInfo[] GetDrives()
		{
			string[] logicalDrives = Directory.GetLogicalDrives();
			DriveInfo[] array = new DriveInfo[logicalDrives.Length];
			for (int i = 0; i < logicalDrives.Length; i++)
			{
				array[i] = new DriveInfo(logicalDrives[i]);
			}
			return array;
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060035A8 RID: 13736 RVA: 0x000B2E52 File Offset: 0x000B1E52
		public DirectoryInfo RootDirectory
		{
			get
			{
				return new DirectoryInfo(this.Name);
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060035A9 RID: 13737 RVA: 0x000B2E60 File Offset: 0x000B1E60
		// (set) Token: 0x060035AA RID: 13738 RVA: 0x000B2EE4 File Offset: 0x000B1EE4
		public string VolumeLabel
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(50);
				StringBuilder fileSystemName = new StringBuilder(50);
				int errorMode = Win32Native.SetErrorMode(1);
				try
				{
					int num;
					int num2;
					int num3;
					if (!Win32Native.GetVolumeInformation(this.Name, stringBuilder, 50, out num, out num2, out num3, fileSystemName, 50))
					{
						int num4 = Marshal.GetLastWin32Error();
						if (num4 == 13)
						{
							num4 = 15;
						}
						__Error.WinIODriveError(this.Name, num4);
					}
				}
				finally
				{
					Win32Native.SetErrorMode(errorMode);
				}
				return stringBuilder.ToString();
			}
			set
			{
				string path = this._name + '.';
				new FileIOPermission(FileIOPermissionAccess.Write, path).Demand();
				int errorMode = Win32Native.SetErrorMode(1);
				try
				{
					if (!Win32Native.SetVolumeLabel(this.Name, value))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						if (lastWin32Error == 5)
						{
							throw new UnauthorizedAccessException(Environment.GetResourceString("InvalidOperation_SetVolumeLabelFailed"));
						}
						__Error.WinIODriveError(this.Name, lastWin32Error);
					}
				}
				finally
				{
					Win32Native.SetErrorMode(errorMode);
				}
			}
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x000B2F68 File Offset: 0x000B1F68
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x000B2F70 File Offset: 0x000B1F70
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_name", this._name, typeof(string));
		}

		// Token: 0x04001C2D RID: 7213
		private const string NameField = "_name";

		// Token: 0x04001C2E RID: 7214
		private string _name;
	}
}
