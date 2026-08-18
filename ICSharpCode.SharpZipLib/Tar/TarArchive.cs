using System;
using System.IO;
using System.Text;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000045 RID: 69
	public class TarArchive : IDisposable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002ED RID: 749 RVA: 0x000105C0 File Offset: 0x0000F5C0
		// (remove) Token: 0x060002EE RID: 750 RVA: 0x000105F8 File Offset: 0x0000F5F8
		public event ProgressMessageHandler ProgressMessageEvent;

		// Token: 0x060002EF RID: 751 RVA: 0x00010630 File Offset: 0x0000F630
		protected virtual void OnProgressMessageEvent(TarEntry entry, string message)
		{
			ProgressMessageHandler progressMessageEvent = this.ProgressMessageEvent;
			if (progressMessageEvent != null)
			{
				progressMessageEvent(this, entry, message);
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00010650 File Offset: 0x0000F650
		protected TarArchive()
		{
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0001066E File Offset: 0x0000F66E
		protected TarArchive(TarInputStream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.tarIn = stream;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000106A1 File Offset: 0x0000F6A1
		protected TarArchive(TarOutputStream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.tarOut = stream;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000106D4 File Offset: 0x0000F6D4
		public static TarArchive CreateInputTarArchive(Stream inputStream)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			TarInputStream tarInputStream = inputStream as TarInputStream;
			TarArchive result;
			if (tarInputStream != null)
			{
				result = new TarArchive(tarInputStream);
			}
			else
			{
				result = TarArchive.CreateInputTarArchive(inputStream, 20);
			}
			return result;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0001070C File Offset: 0x0000F70C
		public static TarArchive CreateInputTarArchive(Stream inputStream, int blockFactor)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			if (inputStream is TarInputStream)
			{
				throw new ArgumentException("TarInputStream not valid");
			}
			return new TarArchive(new TarInputStream(inputStream, blockFactor));
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0001073C File Offset: 0x0000F73C
		public static TarArchive CreateOutputTarArchive(Stream outputStream)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			TarOutputStream tarOutputStream = outputStream as TarOutputStream;
			TarArchive result;
			if (tarOutputStream != null)
			{
				result = new TarArchive(tarOutputStream);
			}
			else
			{
				result = TarArchive.CreateOutputTarArchive(outputStream, 20);
			}
			return result;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00010774 File Offset: 0x0000F774
		public static TarArchive CreateOutputTarArchive(Stream outputStream, int blockFactor)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (outputStream is TarOutputStream)
			{
				throw new ArgumentException("TarOutputStream is not valid");
			}
			return new TarArchive(new TarOutputStream(outputStream, blockFactor));
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000107A3 File Offset: 0x0000F7A3
		public void SetKeepOldFiles(bool keepExistingFiles)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			this.keepOldFiles = keepExistingFiles;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x000107BF File Offset: 0x0000F7BF
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x000107DA File Offset: 0x0000F7DA
		public bool AsciiTranslate
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.asciiTranslate;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				this.asciiTranslate = value;
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000107F6 File Offset: 0x0000F7F6
		[Obsolete("Use the AsciiTranslate property")]
		public void SetAsciiTranslation(bool translateAsciiFiles)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			this.asciiTranslate = translateAsciiFiles;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00010812 File Offset: 0x0000F812
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0001082D File Offset: 0x0000F82D
		public string PathPrefix
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.pathPrefix;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				this.pathPrefix = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00010849 File Offset: 0x0000F849
		// (set) Token: 0x060002FE RID: 766 RVA: 0x00010864 File Offset: 0x0000F864
		public string RootPath
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.rootPath;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				this.rootPath = value.Replace('\\', '/').TrimEnd(new char[]
				{
					'/'
				});
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000108A6 File Offset: 0x0000F8A6
		public void SetUserInfo(int userId, string userName, int groupId, string groupName)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			this.userId = userId;
			this.userName = userName;
			this.groupId = groupId;
			this.groupName = groupName;
			this.applyUserInfoOverrides = true;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000300 RID: 768 RVA: 0x000108DF File Offset: 0x0000F8DF
		// (set) Token: 0x06000301 RID: 769 RVA: 0x000108FA File Offset: 0x0000F8FA
		public bool ApplyUserInfoOverrides
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.applyUserInfoOverrides;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				this.applyUserInfoOverrides = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00010916 File Offset: 0x0000F916
		public int UserId
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.userId;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00010931 File Offset: 0x0000F931
		public string UserName
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.userName;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0001094C File Offset: 0x0000F94C
		public int GroupId
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.groupId;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00010967 File Offset: 0x0000F967
		public string GroupName
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.groupName;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00010984 File Offset: 0x0000F984
		public int RecordSize
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				if (this.tarIn != null)
				{
					return this.tarIn.RecordSize;
				}
				if (this.tarOut != null)
				{
					return this.tarOut.RecordSize;
				}
				return 10240;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (set) Token: 0x06000307 RID: 775 RVA: 0x000109D1 File Offset: 0x0000F9D1
		public bool IsStreamOwner
		{
			set
			{
				if (this.tarIn != null)
				{
					this.tarIn.IsStreamOwner = value;
					return;
				}
				this.tarOut.IsStreamOwner = value;
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000109F4 File Offset: 0x0000F9F4
		[Obsolete("Use Close instead")]
		public void CloseArchive()
		{
			this.Close();
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000109FC File Offset: 0x0000F9FC
		public void ListContents()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			for (;;)
			{
				TarEntry nextEntry = this.tarIn.GetNextEntry();
				if (nextEntry == null)
				{
					break;
				}
				this.OnProgressMessageEvent(nextEntry, null);
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00010A38 File Offset: 0x0000FA38
		public void ExtractContents(string destinationDirectory)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			for (;;)
			{
				TarEntry nextEntry = this.tarIn.GetNextEntry();
				if (nextEntry == null)
				{
					break;
				}
				if (nextEntry.TarHeader.TypeFlag != 49 && nextEntry.TarHeader.TypeFlag != 50)
				{
					this.ExtractEntry(destinationDirectory, nextEntry);
				}
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00010A90 File Offset: 0x0000FA90
		private void ExtractEntry(string destDir, TarEntry entry)
		{
			this.OnProgressMessageEvent(entry, null);
			string text = entry.Name;
			if (Path.IsPathRooted(text))
			{
				text = text.Substring(Path.GetPathRoot(text).Length);
			}
			text = text.Replace('/', Path.DirectorySeparatorChar);
			string text2 = Path.Combine(destDir, text);
			if (entry.IsDirectory)
			{
				TarArchive.EnsureDirectoryExists(text2);
				return;
			}
			string directoryName = Path.GetDirectoryName(text2);
			TarArchive.EnsureDirectoryExists(directoryName);
			bool flag = true;
			FileInfo fileInfo = new FileInfo(text2);
			if (fileInfo.Exists)
			{
				if (this.keepOldFiles)
				{
					this.OnProgressMessageEvent(entry, "Destination file already exists");
					flag = false;
				}
				else if ((fileInfo.Attributes & FileAttributes.ReadOnly) != (FileAttributes)0)
				{
					this.OnProgressMessageEvent(entry, "Destination file already exists, and is read-only");
					flag = false;
				}
			}
			if (flag)
			{
				bool flag2 = false;
				Stream stream = File.Create(text2);
				if (this.asciiTranslate)
				{
					flag2 = !TarArchive.IsBinary(text2);
				}
				StreamWriter streamWriter = null;
				if (flag2)
				{
					streamWriter = new StreamWriter(stream);
				}
				byte[] array = new byte[32768];
				for (;;)
				{
					int num = this.tarIn.Read(array, 0, array.Length);
					if (num <= 0)
					{
						break;
					}
					if (flag2)
					{
						int num2 = 0;
						for (int i = 0; i < num; i++)
						{
							if (array[i] == 10)
							{
								string @string = Encoding.ASCII.GetString(array, num2, i - num2);
								streamWriter.WriteLine(@string);
								num2 = i + 1;
							}
						}
					}
					else
					{
						stream.Write(array, 0, num);
					}
				}
				if (flag2)
				{
					streamWriter.Close();
					return;
				}
				stream.Close();
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00010C00 File Offset: 0x0000FC00
		public void WriteEntry(TarEntry sourceEntry, bool recurse)
		{
			if (sourceEntry == null)
			{
				throw new ArgumentNullException("sourceEntry");
			}
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			try
			{
				if (recurse)
				{
					TarHeader.SetValueDefaults(sourceEntry.UserId, sourceEntry.UserName, sourceEntry.GroupId, sourceEntry.GroupName);
				}
				this.WriteEntryCore(sourceEntry, recurse);
			}
			finally
			{
				if (recurse)
				{
					TarHeader.RestoreSetValues();
				}
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00010C74 File Offset: 0x0000FC74
		private void WriteEntryCore(TarEntry sourceEntry, bool recurse)
		{
			string text = null;
			string text2 = sourceEntry.File;
			TarEntry tarEntry = (TarEntry)sourceEntry.Clone();
			if (this.applyUserInfoOverrides)
			{
				tarEntry.GroupId = this.groupId;
				tarEntry.GroupName = this.groupName;
				tarEntry.UserId = this.userId;
				tarEntry.UserName = this.userName;
			}
			this.OnProgressMessageEvent(tarEntry, null);
			if (this.asciiTranslate && !tarEntry.IsDirectory && !TarArchive.IsBinary(text2))
			{
				text = Path.GetTempFileName();
				using (StreamReader streamReader = File.OpenText(text2))
				{
					using (Stream stream = File.Create(text))
					{
						for (;;)
						{
							string text3 = streamReader.ReadLine();
							if (text3 == null)
							{
								break;
							}
							byte[] bytes = Encoding.ASCII.GetBytes(text3);
							stream.Write(bytes, 0, bytes.Length);
							stream.WriteByte(10);
						}
						stream.Flush();
					}
				}
				tarEntry.Size = new FileInfo(text).Length;
				text2 = text;
			}
			string text4 = null;
			if (this.rootPath != null && tarEntry.Name.StartsWith(this.rootPath, StringComparison.OrdinalIgnoreCase))
			{
				text4 = tarEntry.Name.Substring(this.rootPath.Length + 1);
			}
			if (this.pathPrefix != null)
			{
				text4 = ((text4 == null) ? (this.pathPrefix + "/" + tarEntry.Name) : (this.pathPrefix + "/" + text4));
			}
			if (text4 != null)
			{
				tarEntry.Name = text4;
			}
			this.tarOut.PutNextEntry(tarEntry);
			if (tarEntry.IsDirectory)
			{
				if (recurse)
				{
					TarEntry[] directoryEntries = tarEntry.GetDirectoryEntries();
					for (int i = 0; i < directoryEntries.Length; i++)
					{
						this.WriteEntryCore(directoryEntries[i], recurse);
					}
					return;
				}
			}
			else
			{
				using (Stream stream2 = File.OpenRead(text2))
				{
					byte[] array = new byte[32768];
					for (;;)
					{
						int num = stream2.Read(array, 0, array.Length);
						if (num <= 0)
						{
							break;
						}
						this.tarOut.Write(array, 0, num);
					}
				}
				if (text != null && text.Length > 0)
				{
					File.Delete(text);
				}
				this.tarOut.CloseEntry();
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00010EC0 File Offset: 0x0000FEC0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00010ED0 File Offset: 0x0000FED0
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				if (disposing)
				{
					if (this.tarOut != null)
					{
						this.tarOut.Flush();
						this.tarOut.Close();
					}
					if (this.tarIn != null)
					{
						this.tarIn.Close();
					}
				}
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00010F20 File Offset: 0x0000FF20
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00010F2C File Offset: 0x0000FF2C
		~TarArchive()
		{
			this.Dispose(false);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00010F5C File Offset: 0x0000FF5C
		private static void EnsureDirectoryExists(string directoryName)
		{
			if (!Directory.Exists(directoryName))
			{
				try
				{
					Directory.CreateDirectory(directoryName);
				}
				catch (Exception ex)
				{
					throw new TarException("Exception creating directory '" + directoryName + "', " + ex.Message, ex);
				}
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00010FA8 File Offset: 0x0000FFA8
		private static bool IsBinary(string filename)
		{
			using (FileStream fileStream = File.OpenRead(filename))
			{
				int num = Math.Min(4096, (int)fileStream.Length);
				byte[] array = new byte[num];
				int num2 = fileStream.Read(array, 0, num);
				for (int i = 0; i < num2; i++)
				{
					byte b = array[i];
					if (b < 8 || (b > 13 && b < 32) || b == 255)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040001E1 RID: 481
		private bool keepOldFiles;

		// Token: 0x040001E2 RID: 482
		private bool asciiTranslate;

		// Token: 0x040001E3 RID: 483
		private int userId;

		// Token: 0x040001E4 RID: 484
		private string userName = string.Empty;

		// Token: 0x040001E5 RID: 485
		private int groupId;

		// Token: 0x040001E6 RID: 486
		private string groupName = string.Empty;

		// Token: 0x040001E7 RID: 487
		private string rootPath;

		// Token: 0x040001E8 RID: 488
		private string pathPrefix;

		// Token: 0x040001E9 RID: 489
		private bool applyUserInfoOverrides;

		// Token: 0x040001EA RID: 490
		private TarInputStream tarIn;

		// Token: 0x040001EB RID: 491
		private TarOutputStream tarOut;

		// Token: 0x040001EC RID: 492
		private bool isDisposed;
	}
}
