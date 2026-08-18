using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x0200003B RID: 59
	public class TarEntry : ICloneable
	{
		// Token: 0x06000232 RID: 562 RVA: 0x0000E3F4 File Offset: 0x0000D3F4
		private TarEntry()
		{
			this.header = new TarHeader();
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000E407 File Offset: 0x0000D407
		public TarEntry(byte[] headerBuffer)
		{
			this.header = new TarHeader();
			this.header.ParseBuffer(headerBuffer);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000E426 File Offset: 0x0000D426
		public TarEntry(TarHeader header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			this.header = (TarHeader)header.Clone();
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000E450 File Offset: 0x0000D450
		public object Clone()
		{
			return new TarEntry
			{
				file = this.file,
				header = (TarHeader)this.header.Clone(),
				Name = this.Name
			};
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000E494 File Offset: 0x0000D494
		public static TarEntry CreateTarEntry(string name)
		{
			TarEntry tarEntry = new TarEntry();
			TarEntry.NameTarHeader(tarEntry.header, name);
			return tarEntry;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000E4B4 File Offset: 0x0000D4B4
		public static TarEntry CreateEntryFromFile(string fileName)
		{
			TarEntry tarEntry = new TarEntry();
			tarEntry.GetFileTarHeader(tarEntry.header, fileName);
			return tarEntry;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000E4D8 File Offset: 0x0000D4D8
		public override bool Equals(object obj)
		{
			TarEntry tarEntry = obj as TarEntry;
			return tarEntry != null && this.Name.Equals(tarEntry.Name);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000E502 File Offset: 0x0000D502
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000E50F File Offset: 0x0000D50F
		public bool IsDescendent(TarEntry toTest)
		{
			if (toTest == null)
			{
				throw new ArgumentNullException("toTest");
			}
			return toTest.Name.StartsWith(this.Name);
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000E530 File Offset: 0x0000D530
		public TarHeader TarHeader
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000E538 File Offset: 0x0000D538
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000E545 File Offset: 0x0000D545
		public string Name
		{
			get
			{
				return this.header.Name;
			}
			set
			{
				this.header.Name = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000E553 File Offset: 0x0000D553
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000E560 File Offset: 0x0000D560
		public int UserId
		{
			get
			{
				return this.header.UserId;
			}
			set
			{
				this.header.UserId = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000E56E File Offset: 0x0000D56E
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000E57B File Offset: 0x0000D57B
		public int GroupId
		{
			get
			{
				return this.header.GroupId;
			}
			set
			{
				this.header.GroupId = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000E589 File Offset: 0x0000D589
		// (set) Token: 0x06000243 RID: 579 RVA: 0x0000E596 File Offset: 0x0000D596
		public string UserName
		{
			get
			{
				return this.header.UserName;
			}
			set
			{
				this.header.UserName = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000E5A4 File Offset: 0x0000D5A4
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000E5B1 File Offset: 0x0000D5B1
		public string GroupName
		{
			get
			{
				return this.header.GroupName;
			}
			set
			{
				this.header.GroupName = value;
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000E5BF File Offset: 0x0000D5BF
		public void SetIds(int userId, int groupId)
		{
			this.UserId = userId;
			this.GroupId = groupId;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000E5CF File Offset: 0x0000D5CF
		public void SetNames(string userName, string groupName)
		{
			this.UserName = userName;
			this.GroupName = groupName;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000E5DF File Offset: 0x0000D5DF
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000E5EC File Offset: 0x0000D5EC
		public DateTime ModTime
		{
			get
			{
				return this.header.ModTime;
			}
			set
			{
				this.header.ModTime = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000E5FA File Offset: 0x0000D5FA
		public string File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000E602 File Offset: 0x0000D602
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000E60F File Offset: 0x0000D60F
		public long Size
		{
			get
			{
				return this.header.Size;
			}
			set
			{
				this.header.Size = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000E620 File Offset: 0x0000D620
		public bool IsDirectory
		{
			get
			{
				if (this.file != null)
				{
					return Directory.Exists(this.file);
				}
				return this.header != null && (this.header.TypeFlag == 53 || this.Name.EndsWith("/"));
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000E670 File Offset: 0x0000D670
		public void GetFileTarHeader(TarHeader header, string file)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}
			this.file = file;
			string text = file;
			if (text.IndexOf(Environment.CurrentDirectory) == 0)
			{
				text = text.Substring(Environment.CurrentDirectory.Length);
			}
			text = text.Replace(Path.DirectorySeparatorChar, '/');
			while (text.StartsWith("/"))
			{
				text = text.Substring(1);
			}
			header.LinkName = string.Empty;
			header.Name = text;
			if (Directory.Exists(file))
			{
				header.Mode = 1003;
				header.TypeFlag = 53;
				if (header.Name.Length == 0 || header.Name[header.Name.Length - 1] != '/')
				{
					header.Name += "/";
				}
				header.Size = 0L;
			}
			else
			{
				header.Mode = 33216;
				header.TypeFlag = 48;
				header.Size = new FileInfo(file.Replace('/', Path.DirectorySeparatorChar)).Length;
			}
			header.ModTime = System.IO.File.GetLastWriteTime(file.Replace('/', Path.DirectorySeparatorChar)).ToUniversalTime();
			header.DevMajor = 0;
			header.DevMinor = 0;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000E7BC File Offset: 0x0000D7BC
		public TarEntry[] GetDirectoryEntries()
		{
			if (this.file == null || !Directory.Exists(this.file))
			{
				return new TarEntry[0];
			}
			string[] fileSystemEntries = Directory.GetFileSystemEntries(this.file);
			TarEntry[] array = new TarEntry[fileSystemEntries.Length];
			for (int i = 0; i < fileSystemEntries.Length; i++)
			{
				array[i] = TarEntry.CreateEntryFromFile(fileSystemEntries[i]);
			}
			return array;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000E814 File Offset: 0x0000D814
		public void WriteEntryHeader(byte[] outBuffer)
		{
			this.header.WriteHeader(outBuffer);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000E822 File Offset: 0x0000D822
		public static void AdjustEntryName(byte[] buffer, string newName)
		{
			TarHeader.GetNameBytes(newName, buffer, 0, 100);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000E830 File Offset: 0x0000D830
		public static void NameTarHeader(TarHeader header, string name)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			bool flag = name.EndsWith("/");
			header.Name = name;
			header.Mode = (flag ? 1003 : 33216);
			header.UserId = 0;
			header.GroupId = 0;
			header.Size = 0L;
			header.ModTime = DateTime.UtcNow;
			header.TypeFlag = (flag ? 53 : 48);
			header.LinkName = string.Empty;
			header.UserName = string.Empty;
			header.GroupName = string.Empty;
			header.DevMajor = 0;
			header.DevMinor = 0;
		}

		// Token: 0x04000184 RID: 388
		private string file;

		// Token: 0x04000185 RID: 389
		private TarHeader header;
	}
}
