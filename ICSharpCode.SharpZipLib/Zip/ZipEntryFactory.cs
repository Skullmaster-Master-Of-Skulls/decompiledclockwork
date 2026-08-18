using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200007C RID: 124
	public class ZipEntryFactory : IEntryFactory
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x00018651 File Offset: 0x00017651
		public ZipEntryFactory()
		{
			this.nameTransform_ = new ZipNameTransform();
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00018676 File Offset: 0x00017676
		public ZipEntryFactory(ZipEntryFactory.TimeSetting timeSetting)
		{
			this.timeSetting_ = timeSetting;
			this.nameTransform_ = new ZipNameTransform();
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000186A2 File Offset: 0x000176A2
		public ZipEntryFactory(DateTime time)
		{
			this.timeSetting_ = ZipEntryFactory.TimeSetting.Fixed;
			this.FixedDateTime = time;
			this.nameTransform_ = new ZipNameTransform();
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x000186D5 File Offset: 0x000176D5
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x000186DD File Offset: 0x000176DD
		public INameTransform NameTransform
		{
			get
			{
				return this.nameTransform_;
			}
			set
			{
				if (value == null)
				{
					this.nameTransform_ = new ZipNameTransform();
					return;
				}
				this.nameTransform_ = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x000186F5 File Offset: 0x000176F5
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x000186FD File Offset: 0x000176FD
		public ZipEntryFactory.TimeSetting Setting
		{
			get
			{
				return this.timeSetting_;
			}
			set
			{
				this.timeSetting_ = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00018706 File Offset: 0x00017706
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x0001870E File Offset: 0x0001770E
		public DateTime FixedDateTime
		{
			get
			{
				return this.fixedDateTime_;
			}
			set
			{
				if (value.Year < 1970)
				{
					throw new ArgumentException("Value is too old to be valid", "value");
				}
				this.fixedDateTime_ = value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00018735 File Offset: 0x00017735
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x0001873D File Offset: 0x0001773D
		public int GetAttributes
		{
			get
			{
				return this.getAttributes_;
			}
			set
			{
				this.getAttributes_ = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00018746 File Offset: 0x00017746
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x0001874E File Offset: 0x0001774E
		public int SetAttributes
		{
			get
			{
				return this.setAttributes_;
			}
			set
			{
				this.setAttributes_ = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00018757 File Offset: 0x00017757
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x0001875F File Offset: 0x0001775F
		public bool IsUnicodeText
		{
			get
			{
				return this.isUnicodeText_;
			}
			set
			{
				this.isUnicodeText_ = value;
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00018768 File Offset: 0x00017768
		public ZipEntry MakeFileEntry(string fileName)
		{
			return this.MakeFileEntry(fileName, null, true);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00018773 File Offset: 0x00017773
		public ZipEntry MakeFileEntry(string fileName, bool useFileSystem)
		{
			return this.MakeFileEntry(fileName, null, useFileSystem);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00018780 File Offset: 0x00017780
		public ZipEntry MakeFileEntry(string fileName, string entryName, bool useFileSystem)
		{
			ZipEntry zipEntry = new ZipEntry(this.nameTransform_.TransformFile((entryName != null && entryName.Length > 0) ? entryName : fileName));
			zipEntry.IsUnicodeText = this.isUnicodeText_;
			int num = 0;
			bool flag = this.setAttributes_ != 0;
			FileInfo fileInfo = null;
			if (useFileSystem)
			{
				fileInfo = new FileInfo(fileName);
			}
			if (fileInfo != null && fileInfo.Exists)
			{
				switch (this.timeSetting_)
				{
				case ZipEntryFactory.TimeSetting.LastWriteTime:
					zipEntry.DateTime = fileInfo.LastWriteTime;
					break;
				case ZipEntryFactory.TimeSetting.LastWriteTimeUtc:
					zipEntry.DateTime = fileInfo.LastWriteTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.CreateTime:
					zipEntry.DateTime = fileInfo.CreationTime;
					break;
				case ZipEntryFactory.TimeSetting.CreateTimeUtc:
					zipEntry.DateTime = fileInfo.CreationTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.LastAccessTime:
					zipEntry.DateTime = fileInfo.LastAccessTime;
					break;
				case ZipEntryFactory.TimeSetting.LastAccessTimeUtc:
					zipEntry.DateTime = fileInfo.LastAccessTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.Fixed:
					zipEntry.DateTime = this.fixedDateTime_;
					break;
				default:
					throw new ZipException("Unhandled time setting in MakeFileEntry");
				}
				zipEntry.Size = fileInfo.Length;
				flag = true;
				num = (int)(fileInfo.Attributes & (FileAttributes)this.getAttributes_);
			}
			else if (this.timeSetting_ == ZipEntryFactory.TimeSetting.Fixed)
			{
				zipEntry.DateTime = this.fixedDateTime_;
			}
			if (flag)
			{
				num |= this.setAttributes_;
				zipEntry.ExternalFileAttributes = num;
			}
			return zipEntry;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000188C7 File Offset: 0x000178C7
		public ZipEntry MakeDirectoryEntry(string directoryName)
		{
			return this.MakeDirectoryEntry(directoryName, true);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000188D4 File Offset: 0x000178D4
		public ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem)
		{
			ZipEntry zipEntry = new ZipEntry(this.nameTransform_.TransformDirectory(directoryName));
			zipEntry.IsUnicodeText = this.isUnicodeText_;
			zipEntry.Size = 0L;
			int num = 0;
			DirectoryInfo directoryInfo = null;
			if (useFileSystem)
			{
				directoryInfo = new DirectoryInfo(directoryName);
			}
			if (directoryInfo != null && directoryInfo.Exists)
			{
				switch (this.timeSetting_)
				{
				case ZipEntryFactory.TimeSetting.LastWriteTime:
					zipEntry.DateTime = directoryInfo.LastWriteTime;
					break;
				case ZipEntryFactory.TimeSetting.LastWriteTimeUtc:
					zipEntry.DateTime = directoryInfo.LastWriteTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.CreateTime:
					zipEntry.DateTime = directoryInfo.CreationTime;
					break;
				case ZipEntryFactory.TimeSetting.CreateTimeUtc:
					zipEntry.DateTime = directoryInfo.CreationTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.LastAccessTime:
					zipEntry.DateTime = directoryInfo.LastAccessTime;
					break;
				case ZipEntryFactory.TimeSetting.LastAccessTimeUtc:
					zipEntry.DateTime = directoryInfo.LastAccessTimeUtc;
					break;
				case ZipEntryFactory.TimeSetting.Fixed:
					zipEntry.DateTime = this.fixedDateTime_;
					break;
				default:
					throw new ZipException("Unhandled time setting in MakeDirectoryEntry");
				}
				num = (int)(directoryInfo.Attributes & (FileAttributes)this.getAttributes_);
			}
			else if (this.timeSetting_ == ZipEntryFactory.TimeSetting.Fixed)
			{
				zipEntry.DateTime = this.fixedDateTime_;
			}
			num |= (this.setAttributes_ | 16);
			zipEntry.ExternalFileAttributes = num;
			return zipEntry;
		}

		// Token: 0x040002FD RID: 765
		private INameTransform nameTransform_;

		// Token: 0x040002FE RID: 766
		private DateTime fixedDateTime_ = DateTime.Now;

		// Token: 0x040002FF RID: 767
		private ZipEntryFactory.TimeSetting timeSetting_;

		// Token: 0x04000300 RID: 768
		private bool isUnicodeText_;

		// Token: 0x04000301 RID: 769
		private int getAttributes_ = -1;

		// Token: 0x04000302 RID: 770
		private int setAttributes_;

		// Token: 0x0200007D RID: 125
		public enum TimeSetting
		{
			// Token: 0x04000304 RID: 772
			LastWriteTime,
			// Token: 0x04000305 RID: 773
			LastWriteTimeUtc,
			// Token: 0x04000306 RID: 774
			CreateTime,
			// Token: 0x04000307 RID: 775
			CreateTimeUtc,
			// Token: 0x04000308 RID: 776
			LastAccessTime,
			// Token: 0x04000309 RID: 777
			LastAccessTimeUtc,
			// Token: 0x0400030A RID: 778
			Fixed
		}
	}
}
