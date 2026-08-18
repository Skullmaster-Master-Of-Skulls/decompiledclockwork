using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Ionic.Zlib;
using Microsoft.CSharp;

namespace Ionic.Zip
{
	// Token: 0x02000034 RID: 52
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00005")]
	[ComVisible(true)]
	public class ZipFile : IEnumerable<ZipEntry>, IEnumerable, IDisposable
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x0000B813 File Offset: 0x00009A13
		public ZipEntry AddItem(string fileOrDirectoryName)
		{
			return this.AddItem(fileOrDirectoryName, null);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000B81D File Offset: 0x00009A1D
		public ZipEntry AddItem(string fileOrDirectoryName, string directoryPathInArchive)
		{
			if (File.Exists(fileOrDirectoryName))
			{
				return this.AddFile(fileOrDirectoryName, directoryPathInArchive);
			}
			if (Directory.Exists(fileOrDirectoryName))
			{
				return this.AddDirectory(fileOrDirectoryName, directoryPathInArchive);
			}
			throw new FileNotFoundException(string.Format("That file or directory ({0}) does not exist!", fileOrDirectoryName));
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000B851 File Offset: 0x00009A51
		public ZipEntry AddFile(string fileName)
		{
			return this.AddFile(fileName, null);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000B85C File Offset: 0x00009A5C
		public ZipEntry AddFile(string fileName, string directoryPathInArchive)
		{
			string nameInArchive = ZipEntry.NameInArchive(fileName, directoryPathInArchive);
			ZipEntry ze = ZipEntry.CreateFromFile(fileName, nameInArchive);
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding {0}...", fileName);
			}
			return this._InternalAddEntry(ze);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000B89C File Offset: 0x00009A9C
		public void RemoveEntries(ICollection<ZipEntry> entriesToRemove)
		{
			if (entriesToRemove == null)
			{
				throw new ArgumentNullException("entriesToRemove");
			}
			foreach (ZipEntry entry in entriesToRemove)
			{
				this.RemoveEntry(entry);
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000B8F4 File Offset: 0x00009AF4
		public void RemoveEntries(ICollection<string> entriesToRemove)
		{
			if (entriesToRemove == null)
			{
				throw new ArgumentNullException("entriesToRemove");
			}
			foreach (string fileName in entriesToRemove)
			{
				this.RemoveEntry(fileName);
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000B94C File Offset: 0x00009B4C
		public void AddFiles(IEnumerable<string> fileNames)
		{
			this.AddFiles(fileNames, null);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000B956 File Offset: 0x00009B56
		public void UpdateFiles(IEnumerable<string> fileNames)
		{
			this.UpdateFiles(fileNames, null);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000B960 File Offset: 0x00009B60
		public void AddFiles(IEnumerable<string> fileNames, string directoryPathInArchive)
		{
			this.AddFiles(fileNames, false, directoryPathInArchive);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000B96C File Offset: 0x00009B6C
		public void AddFiles(IEnumerable<string> fileNames, bool preserveDirHierarchy, string directoryPathInArchive)
		{
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			this._addOperationCanceled = false;
			this.OnAddStarted();
			if (preserveDirHierarchy)
			{
				using (IEnumerator<string> enumerator = fileNames.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						if (this._addOperationCanceled)
						{
							break;
						}
						if (directoryPathInArchive != null)
						{
							string fullPath = Path.GetFullPath(Path.Combine(directoryPathInArchive, Path.GetDirectoryName(text)));
							this.AddFile(text, fullPath);
						}
						else
						{
							this.AddFile(text, null);
						}
					}
					goto IL_AD;
				}
			}
			foreach (string fileName in fileNames)
			{
				if (this._addOperationCanceled)
				{
					break;
				}
				this.AddFile(fileName, directoryPathInArchive);
			}
			IL_AD:
			if (!this._addOperationCanceled)
			{
				this.OnAddCompleted();
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000BA50 File Offset: 0x00009C50
		public void UpdateFiles(IEnumerable<string> fileNames, string directoryPathInArchive)
		{
			if (fileNames == null)
			{
				throw new ArgumentNullException("fileNames");
			}
			this.OnAddStarted();
			foreach (string fileName in fileNames)
			{
				this.UpdateFile(fileName, directoryPathInArchive);
			}
			this.OnAddCompleted();
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		public ZipEntry UpdateFile(string fileName)
		{
			return this.UpdateFile(fileName, null);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000BAC0 File Offset: 0x00009CC0
		public ZipEntry UpdateFile(string fileName, string directoryPathInArchive)
		{
			string fileName2 = ZipEntry.NameInArchive(fileName, directoryPathInArchive);
			if (this[fileName2] != null)
			{
				this.RemoveEntry(fileName2);
			}
			return this.AddFile(fileName, directoryPathInArchive);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000BAED File Offset: 0x00009CED
		public ZipEntry UpdateDirectory(string directoryName)
		{
			return this.UpdateDirectory(directoryName, null);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000BAF7 File Offset: 0x00009CF7
		public ZipEntry UpdateDirectory(string directoryName, string directoryPathInArchive)
		{
			return this.AddOrUpdateDirectoryImpl(directoryName, directoryPathInArchive, AddOrUpdateAction.AddOrUpdate);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000BB02 File Offset: 0x00009D02
		public void UpdateItem(string itemName)
		{
			this.UpdateItem(itemName, null);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000BB0C File Offset: 0x00009D0C
		public void UpdateItem(string itemName, string directoryPathInArchive)
		{
			if (File.Exists(itemName))
			{
				this.UpdateFile(itemName, directoryPathInArchive);
				return;
			}
			if (Directory.Exists(itemName))
			{
				this.UpdateDirectory(itemName, directoryPathInArchive);
				return;
			}
			throw new FileNotFoundException(string.Format("That file or directory ({0}) does not exist!", itemName));
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000BB42 File Offset: 0x00009D42
		public ZipEntry AddEntry(string entryName, string content)
		{
			return this.AddEntry(entryName, content, Encoding.Default);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000BB54 File Offset: 0x00009D54
		public ZipEntry AddEntry(string entryName, string content, Encoding encoding)
		{
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream, encoding);
			streamWriter.Write(content);
			streamWriter.Flush();
			memoryStream.Seek(0L, SeekOrigin.Begin);
			return this.AddEntry(entryName, memoryStream);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000BB90 File Offset: 0x00009D90
		public ZipEntry AddEntry(string entryName, Stream stream)
		{
			ZipEntry zipEntry = ZipEntry.CreateForStream(entryName, stream);
			zipEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding {0}...", entryName);
			}
			return this._InternalAddEntry(zipEntry);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000BBDC File Offset: 0x00009DDC
		public ZipEntry AddEntry(string entryName, WriteDelegate writer)
		{
			ZipEntry ze = ZipEntry.CreateForWriter(entryName, writer);
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding {0}...", entryName);
			}
			return this._InternalAddEntry(ze);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000BC14 File Offset: 0x00009E14
		public ZipEntry AddEntry(string entryName, OpenDelegate opener, CloseDelegate closer)
		{
			ZipEntry zipEntry = ZipEntry.CreateForJitStreamProvider(entryName, opener, closer);
			zipEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding {0}...", entryName);
			}
			return this._InternalAddEntry(zipEntry);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000BC60 File Offset: 0x00009E60
		private ZipEntry _InternalAddEntry(ZipEntry ze)
		{
			ze._container = new ZipContainer(this);
			ze.CompressionMethod = this.CompressionMethod;
			ze.CompressionLevel = this.CompressionLevel;
			ze.ExtractExistingFile = this.ExtractExistingFile;
			ze.ZipErrorAction = this.ZipErrorAction;
			ze.SetCompression = this.SetCompression;
			ze.AlternateEncoding = this.AlternateEncoding;
			ze.AlternateEncodingUsage = this.AlternateEncodingUsage;
			ze.Password = this._Password;
			ze.Encryption = this.Encryption;
			ze.EmitTimesInWindowsFormatWhenSaving = this._emitNtfsTimes;
			ze.EmitTimesInUnixFormatWhenSaving = this._emitUnixTimes;
			this.InternalAddEntry(ze.FileName, ze);
			this.AfterAddEntry(ze);
			return ze;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000BD12 File Offset: 0x00009F12
		public ZipEntry UpdateEntry(string entryName, string content)
		{
			return this.UpdateEntry(entryName, content, Encoding.Default);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000BD21 File Offset: 0x00009F21
		public ZipEntry UpdateEntry(string entryName, string content, Encoding encoding)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, content, encoding);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000BD33 File Offset: 0x00009F33
		public ZipEntry UpdateEntry(string entryName, WriteDelegate writer)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, writer);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000BD44 File Offset: 0x00009F44
		public ZipEntry UpdateEntry(string entryName, OpenDelegate opener, CloseDelegate closer)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, opener, closer);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000BD56 File Offset: 0x00009F56
		public ZipEntry UpdateEntry(string entryName, Stream stream)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, stream);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000BD68 File Offset: 0x00009F68
		private void RemoveEntryForUpdate(string entryName)
		{
			if (string.IsNullOrEmpty(entryName))
			{
				throw new ArgumentNullException("entryName");
			}
			string directoryPathInArchive = null;
			if (entryName.IndexOf('\\') != -1)
			{
				directoryPathInArchive = Path.GetDirectoryName(entryName);
				entryName = Path.GetFileName(entryName);
			}
			string fileName = ZipEntry.NameInArchive(entryName, directoryPathInArchive);
			if (this[fileName] != null)
			{
				this.RemoveEntry(fileName);
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000BDBC File Offset: 0x00009FBC
		public ZipEntry AddEntry(string entryName, byte[] byteContent)
		{
			if (byteContent == null)
			{
				throw new ArgumentException("bad argument", "byteContent");
			}
			MemoryStream stream = new MemoryStream(byteContent);
			return this.AddEntry(entryName, stream);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000BDEB File Offset: 0x00009FEB
		public ZipEntry UpdateEntry(string entryName, byte[] byteContent)
		{
			this.RemoveEntryForUpdate(entryName);
			return this.AddEntry(entryName, byteContent);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000BDFC File Offset: 0x00009FFC
		public ZipEntry AddDirectory(string directoryName)
		{
			return this.AddDirectory(directoryName, null);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000BE06 File Offset: 0x0000A006
		public ZipEntry AddDirectory(string directoryName, string directoryPathInArchive)
		{
			return this.AddOrUpdateDirectoryImpl(directoryName, directoryPathInArchive, AddOrUpdateAction.AddOnly);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000BE14 File Offset: 0x0000A014
		public ZipEntry AddDirectoryByName(string directoryNameInArchive)
		{
			ZipEntry zipEntry = ZipEntry.CreateFromNothing(directoryNameInArchive);
			zipEntry._container = new ZipContainer(this);
			zipEntry.MarkAsDirectory();
			zipEntry.AlternateEncoding = this.AlternateEncoding;
			zipEntry.AlternateEncodingUsage = this.AlternateEncodingUsage;
			zipEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			zipEntry.EmitTimesInWindowsFormatWhenSaving = this._emitNtfsTimes;
			zipEntry.EmitTimesInUnixFormatWhenSaving = this._emitUnixTimes;
			zipEntry._Source = ZipEntrySource.Stream;
			this.InternalAddEntry(zipEntry.FileName, zipEntry);
			this.AfterAddEntry(zipEntry);
			return zipEntry;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000BE9B File Offset: 0x0000A09B
		private ZipEntry AddOrUpdateDirectoryImpl(string directoryName, string rootDirectoryPathInArchive, AddOrUpdateAction action)
		{
			if (rootDirectoryPathInArchive == null)
			{
				rootDirectoryPathInArchive = "";
			}
			return this.AddOrUpdateDirectoryImpl(directoryName, rootDirectoryPathInArchive, action, true, 0);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000BEB2 File Offset: 0x0000A0B2
		internal void InternalAddEntry(string name, ZipEntry entry)
		{
			this._entries.Add(name, entry);
			this._zipEntriesAsList = null;
			this._contentsChanged = true;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000BED0 File Offset: 0x0000A0D0
		private ZipEntry AddOrUpdateDirectoryImpl(string directoryName, string rootDirectoryPathInArchive, AddOrUpdateAction action, bool recurse, int level)
		{
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("{0} {1}...", (action == AddOrUpdateAction.AddOnly) ? "adding" : "Adding or updating", directoryName);
			}
			if (level == 0)
			{
				this._addOperationCanceled = false;
				this.OnAddStarted();
			}
			if (this._addOperationCanceled)
			{
				return null;
			}
			string text = rootDirectoryPathInArchive;
			ZipEntry zipEntry = null;
			if (level > 0)
			{
				int num = directoryName.Length;
				for (int i = level; i > 0; i--)
				{
					num = directoryName.LastIndexOfAny("/\\".ToCharArray(), num - 1, num - 1);
				}
				text = directoryName.Substring(num + 1);
				text = Path.Combine(rootDirectoryPathInArchive, text);
			}
			if (level > 0 || rootDirectoryPathInArchive != "")
			{
				zipEntry = ZipEntry.CreateFromFile(directoryName, text);
				zipEntry._container = new ZipContainer(this);
				zipEntry.AlternateEncoding = this.AlternateEncoding;
				zipEntry.AlternateEncodingUsage = this.AlternateEncodingUsage;
				zipEntry.MarkAsDirectory();
				zipEntry.EmitTimesInWindowsFormatWhenSaving = this._emitNtfsTimes;
				zipEntry.EmitTimesInUnixFormatWhenSaving = this._emitUnixTimes;
				if (!this._entries.ContainsKey(zipEntry.FileName))
				{
					this.InternalAddEntry(zipEntry.FileName, zipEntry);
					this.AfterAddEntry(zipEntry);
				}
				text = zipEntry.FileName;
			}
			if (!this._addOperationCanceled)
			{
				string[] files = Directory.GetFiles(directoryName);
				if (recurse)
				{
					foreach (string fileName in files)
					{
						if (this._addOperationCanceled)
						{
							break;
						}
						if (action == AddOrUpdateAction.AddOnly)
						{
							this.AddFile(fileName, text);
						}
						else
						{
							this.UpdateFile(fileName, text);
						}
					}
					if (!this._addOperationCanceled)
					{
						string[] directories = Directory.GetDirectories(directoryName);
						foreach (string text2 in directories)
						{
							FileAttributes attributes = File.GetAttributes(text2);
							if (this.AddDirectoryWillTraverseReparsePoints || (attributes & FileAttributes.ReparsePoint) == (FileAttributes)0)
							{
								this.AddOrUpdateDirectoryImpl(text2, rootDirectoryPathInArchive, action, recurse, level + 1);
							}
						}
					}
				}
			}
			if (level == 0)
			{
				this.OnAddCompleted();
			}
			return zipEntry;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000C0AE File Offset: 0x0000A2AE
		public static bool CheckZip(string zipFileName)
		{
			return ZipFile.CheckZip(zipFileName, false, null);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000C0B8 File Offset: 0x0000A2B8
		public static bool CheckZip(string zipFileName, bool fixIfNecessary, TextWriter writer)
		{
			ZipFile zipFile = null;
			ZipFile zipFile2 = null;
			bool flag = true;
			try
			{
				zipFile = new ZipFile();
				zipFile.FullScan = true;
				zipFile.Initialize(zipFileName);
				zipFile2 = ZipFile.Read(zipFileName);
				foreach (ZipEntry zipEntry in zipFile)
				{
					foreach (ZipEntry zipEntry2 in zipFile2)
					{
						if (zipEntry.FileName == zipEntry2.FileName)
						{
							if (zipEntry._RelativeOffsetOfLocalHeader != zipEntry2._RelativeOffsetOfLocalHeader)
							{
								flag = false;
								if (writer != null)
								{
									writer.WriteLine("{0}: mismatch in RelativeOffsetOfLocalHeader  (0x{1:X16} != 0x{2:X16})", zipEntry.FileName, zipEntry._RelativeOffsetOfLocalHeader, zipEntry2._RelativeOffsetOfLocalHeader);
								}
							}
							if (zipEntry._CompressedSize != zipEntry2._CompressedSize)
							{
								flag = false;
								if (writer != null)
								{
									writer.WriteLine("{0}: mismatch in CompressedSize  (0x{1:X16} != 0x{2:X16})", zipEntry.FileName, zipEntry._CompressedSize, zipEntry2._CompressedSize);
								}
							}
							if (zipEntry._UncompressedSize != zipEntry2._UncompressedSize)
							{
								flag = false;
								if (writer != null)
								{
									writer.WriteLine("{0}: mismatch in UncompressedSize  (0x{1:X16} != 0x{2:X16})", zipEntry.FileName, zipEntry._UncompressedSize, zipEntry2._UncompressedSize);
								}
							}
							if (zipEntry.CompressionMethod != zipEntry2.CompressionMethod)
							{
								flag = false;
								if (writer != null)
								{
									writer.WriteLine("{0}: mismatch in CompressionMethod  (0x{1:X4} != 0x{2:X4})", zipEntry.FileName, zipEntry.CompressionMethod, zipEntry2.CompressionMethod);
								}
							}
							if (zipEntry.Crc == zipEntry2.Crc)
							{
								break;
							}
							flag = false;
							if (writer != null)
							{
								writer.WriteLine("{0}: mismatch in Crc32  (0x{1:X4} != 0x{2:X4})", zipEntry.FileName, zipEntry.Crc, zipEntry2.Crc);
								break;
							}
							break;
						}
					}
				}
				zipFile2.Dispose();
				zipFile2 = null;
				if (!flag && fixIfNecessary)
				{
					string text = Path.GetFileNameWithoutExtension(zipFileName);
					text = string.Format("{0}_fixed.zip", text);
					zipFile.Save(text);
				}
			}
			finally
			{
				if (zipFile != null)
				{
					zipFile.Dispose();
				}
				if (zipFile2 != null)
				{
					zipFile2.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000C318 File Offset: 0x0000A518
		public static void FixZipDirectory(string zipFileName)
		{
			using (ZipFile zipFile = new ZipFile())
			{
				zipFile.FullScan = true;
				zipFile.Initialize(zipFileName);
				zipFile.Save(zipFileName);
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000C35C File Offset: 0x0000A55C
		public static bool CheckZipPassword(string zipFileName, string password)
		{
			bool result = false;
			try
			{
				using (ZipFile zipFile = ZipFile.Read(zipFileName))
				{
					foreach (ZipEntry zipEntry in zipFile)
					{
						if (!zipEntry.IsDirectory && zipEntry.UsesEncryption)
						{
							zipEntry.ExtractWithPassword(Stream.Null, password);
						}
					}
				}
				result = true;
			}
			catch (BadPasswordException)
			{
			}
			return result;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
		public string Info
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(string.Format("          ZipFile: {0}\n", this.Name));
				if (!string.IsNullOrEmpty(this._Comment))
				{
					stringBuilder.Append(string.Format("          Comment: {0}\n", this._Comment));
				}
				if (this._versionMadeBy != 0)
				{
					stringBuilder.Append(string.Format("  version made by: 0x{0:X4}\n", this._versionMadeBy));
				}
				if (this._versionNeededToExtract != 0)
				{
					stringBuilder.Append(string.Format("needed to extract: 0x{0:X4}\n", this._versionNeededToExtract));
				}
				stringBuilder.Append(string.Format("       uses ZIP64: {0}\n", this.InputUsesZip64));
				stringBuilder.Append(string.Format("     disk with CD: {0}\n", this._diskNumberWithCd));
				if (this._OffsetOfCentralDirectory == 4294967295U)
				{
					stringBuilder.Append(string.Format("      CD64 offset: 0x{0:X16}\n", this._OffsetOfCentralDirectory64));
				}
				else
				{
					stringBuilder.Append(string.Format("        CD offset: 0x{0:X8}\n", this._OffsetOfCentralDirectory));
				}
				stringBuilder.Append("\n");
				foreach (ZipEntry zipEntry in this._entries.Values)
				{
					stringBuilder.Append(zipEntry.Info);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000C564 File Offset: 0x0000A764
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0000C56C File Offset: 0x0000A76C
		public bool FullScan { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000C575 File Offset: 0x0000A775
		// (set) Token: 0x060001DF RID: 479 RVA: 0x0000C57D File Offset: 0x0000A77D
		public bool SortEntriesBeforeSaving { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000C586 File Offset: 0x0000A786
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000C58E File Offset: 0x0000A78E
		public bool AddDirectoryWillTraverseReparsePoints { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000C597 File Offset: 0x0000A797
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000C59F File Offset: 0x0000A79F
		public int BufferSize
		{
			get
			{
				return this._BufferSize;
			}
			set
			{
				this._BufferSize = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
		public int CodecBufferSize { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000C5B9 File Offset: 0x0000A7B9
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000C5C1 File Offset: 0x0000A7C1
		public bool FlattenFoldersOnExtract { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000C5CA File Offset: 0x0000A7CA
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000C5D2 File Offset: 0x0000A7D2
		public CompressionStrategy Strategy
		{
			get
			{
				return this._Strategy;
			}
			set
			{
				this._Strategy = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000C5DB File Offset: 0x0000A7DB
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000C5E3 File Offset: 0x0000A7E3
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000C5EC File Offset: 0x0000A7EC
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000C5F4 File Offset: 0x0000A7F4
		public CompressionLevel CompressionLevel { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000C5FD File Offset: 0x0000A7FD
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000C605 File Offset: 0x0000A805
		public CompressionMethod CompressionMethod
		{
			get
			{
				return this._compressionMethod;
			}
			set
			{
				this._compressionMethod = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000C60E File Offset: 0x0000A80E
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000C616 File Offset: 0x0000A816
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				this._Comment = value;
				this._contentsChanged = true;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000C626 File Offset: 0x0000A826
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x0000C62E File Offset: 0x0000A82E
		public bool EmitTimesInWindowsFormatWhenSaving
		{
			get
			{
				return this._emitNtfsTimes;
			}
			set
			{
				this._emitNtfsTimes = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000C637 File Offset: 0x0000A837
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000C63F File Offset: 0x0000A83F
		public bool EmitTimesInUnixFormatWhenSaving
		{
			get
			{
				return this._emitUnixTimes;
			}
			set
			{
				this._emitUnixTimes = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000C648 File Offset: 0x0000A848
		internal bool Verbose
		{
			get
			{
				return this._StatusMessageTextWriter != null;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000C656 File Offset: 0x0000A856
		public bool ContainsEntry(string name)
		{
			return this._entries.ContainsKey(SharedUtilities.NormalizePathForUseInZipFile(name));
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000C669 File Offset: 0x0000A869
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x0000C671 File Offset: 0x0000A871
		public bool CaseSensitiveRetrieval
		{
			get
			{
				return this._CaseSensitiveRetrieval;
			}
			set
			{
				if (value != this._CaseSensitiveRetrieval)
				{
					this._CaseSensitiveRetrieval = value;
					this._initEntriesDictionary();
				}
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000C689 File Offset: 0x0000A889
		// (set) Token: 0x060001FB RID: 507 RVA: 0x0000C6A8 File Offset: 0x0000A8A8
		[Obsolete("Beginning with v1.9.1.6 of DotNetZip, this property is obsolete.  It will be removed in a future version of the library. Your applications should  use AlternateEncoding and AlternateEncodingUsage instead.")]
		public bool UseUnicodeAsNecessary
		{
			get
			{
				return this._alternateEncoding == Encoding.GetEncoding("UTF-8") && this._alternateEncodingUsage == ZipOption.AsNecessary;
			}
			set
			{
				if (value)
				{
					this._alternateEncoding = Encoding.GetEncoding("UTF-8");
					this._alternateEncodingUsage = ZipOption.AsNecessary;
					return;
				}
				this._alternateEncoding = ZipFile.DefaultEncoding;
				this._alternateEncodingUsage = ZipOption.Default;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000C6D7 File Offset: 0x0000A8D7
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000C6DF File Offset: 0x0000A8DF
		public Zip64Option UseZip64WhenSaving
		{
			get
			{
				return this._zip64;
			}
			set
			{
				this._zip64 = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000C6E8 File Offset: 0x0000A8E8
		public bool? RequiresZip64
		{
			get
			{
				if (this._entries.Count > 65534)
				{
					return new bool?(true);
				}
				if (!this._hasBeenSaved || this._contentsChanged)
				{
					return null;
				}
				foreach (ZipEntry zipEntry in this._entries.Values)
				{
					if (zipEntry.RequiresZip64.Value)
					{
						return new bool?(true);
					}
				}
				return new bool?(false);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000C790 File Offset: 0x0000A990
		public bool? OutputUsedZip64
		{
			get
			{
				return this._OutputUsesZip64;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000C798 File Offset: 0x0000A998
		public bool? InputUsesZip64
		{
			get
			{
				if (this._entries.Count > 65534)
				{
					return new bool?(true);
				}
				foreach (ZipEntry zipEntry in this)
				{
					if (zipEntry.Source != ZipEntrySource.ZipFile)
					{
						return null;
					}
					if (zipEntry._InputUsesZip64)
					{
						return new bool?(true);
					}
				}
				return new bool?(false);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000C820 File Offset: 0x0000AA20
		// (set) Token: 0x06000202 RID: 514 RVA: 0x0000C833 File Offset: 0x0000AA33
		[Obsolete("use AlternateEncoding instead.")]
		public Encoding ProvisionalAlternateEncoding
		{
			get
			{
				if (this._alternateEncodingUsage == ZipOption.AsNecessary)
				{
					return this._alternateEncoding;
				}
				return null;
			}
			set
			{
				this._alternateEncoding = value;
				this._alternateEncodingUsage = ZipOption.AsNecessary;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000C843 File Offset: 0x0000AA43
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000C84B File Offset: 0x0000AA4B
		public Encoding AlternateEncoding
		{
			get
			{
				return this._alternateEncoding;
			}
			set
			{
				this._alternateEncoding = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000C854 File Offset: 0x0000AA54
		// (set) Token: 0x06000206 RID: 518 RVA: 0x0000C85C File Offset: 0x0000AA5C
		public ZipOption AlternateEncodingUsage
		{
			get
			{
				return this._alternateEncodingUsage;
			}
			set
			{
				this._alternateEncodingUsage = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000C865 File Offset: 0x0000AA65
		public static Encoding DefaultEncoding
		{
			get
			{
				return ZipFile._defaultEncoding;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000C86C File Offset: 0x0000AA6C
		// (set) Token: 0x06000209 RID: 521 RVA: 0x0000C874 File Offset: 0x0000AA74
		public TextWriter StatusMessageTextWriter
		{
			get
			{
				return this._StatusMessageTextWriter;
			}
			set
			{
				this._StatusMessageTextWriter = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000C87D File Offset: 0x0000AA7D
		// (set) Token: 0x0600020B RID: 523 RVA: 0x0000C885 File Offset: 0x0000AA85
		public string TempFileFolder
		{
			get
			{
				return this._TempFileFolder;
			}
			set
			{
				this._TempFileFolder = value;
				if (value == null)
				{
					return;
				}
				if (!Directory.Exists(value))
				{
					throw new FileNotFoundException(string.Format("That directory ({0}) does not exist.", value));
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000C8D3 File Offset: 0x0000AAD3
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000C8AB File Offset: 0x0000AAAB
		public string Password
		{
			private get
			{
				return this._Password;
			}
			set
			{
				this._Password = value;
				if (this._Password == null)
				{
					this.Encryption = EncryptionAlgorithm.None;
					return;
				}
				if (this.Encryption == EncryptionAlgorithm.None)
				{
					this.Encryption = EncryptionAlgorithm.PkzipWeak;
				}
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000C8DB File Offset: 0x0000AADB
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000C8E3 File Offset: 0x0000AAE3
		public ExtractExistingFileAction ExtractExistingFile { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000C903 File Offset: 0x0000AB03
		public ZipErrorAction ZipErrorAction
		{
			get
			{
				if (this.ZipError != null)
				{
					this._zipErrorAction = ZipErrorAction.InvokeErrorEvent;
				}
				return this._zipErrorAction;
			}
			set
			{
				this._zipErrorAction = value;
				if (this._zipErrorAction != ZipErrorAction.InvokeErrorEvent && this.ZipError != null)
				{
					this.ZipError = null;
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000C924 File Offset: 0x0000AB24
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000C92C File Offset: 0x0000AB2C
		public EncryptionAlgorithm Encryption
		{
			get
			{
				return this._Encryption;
			}
			set
			{
				if (value == EncryptionAlgorithm.Unsupported)
				{
					throw new InvalidOperationException("You may not set Encryption to that value.");
				}
				this._Encryption = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000C944 File Offset: 0x0000AB44
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000C94C File Offset: 0x0000AB4C
		public SetCompressionCallback SetCompression { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000C955 File Offset: 0x0000AB55
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000C95D File Offset: 0x0000AB5D
		public int MaxOutputSegmentSize
		{
			get
			{
				return this._maxOutputSegmentSize;
			}
			set
			{
				if (value < 65536 && value != 0)
				{
					throw new ZipException("The minimum acceptable segment size is 65536.");
				}
				this._maxOutputSegmentSize = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000C97C File Offset: 0x0000AB7C
		public int NumberOfSegmentsForMostRecentSave
		{
			get
			{
				return (int)(this._numberOfSegmentsForMostRecentSave + 1U);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000C9AD File Offset: 0x0000ABAD
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000C986 File Offset: 0x0000AB86
		public long ParallelDeflateThreshold
		{
			get
			{
				return this._ParallelDeflateThreshold;
			}
			set
			{
				if (value != 0L && value != -1L && value < 65536L)
				{
					throw new ArgumentOutOfRangeException("ParallelDeflateThreshold should be -1, 0, or > 65536");
				}
				this._ParallelDeflateThreshold = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000C9B5 File Offset: 0x0000ABB5
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000C9BD File Offset: 0x0000ABBD
		public int ParallelDeflateMaxBufferPairs
		{
			get
			{
				return this._maxBufferPairs;
			}
			set
			{
				if (value < 4)
				{
					throw new ArgumentOutOfRangeException("ParallelDeflateMaxBufferPairs", "Value must be 4 or greater.");
				}
				this._maxBufferPairs = value;
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000C9DA File Offset: 0x0000ABDA
		public override string ToString()
		{
			return string.Format("ZipFile::{0}", this.Name);
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000C9EC File Offset: 0x0000ABEC
		public static Version LibraryVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version;
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000C9FD File Offset: 0x0000ABFD
		internal void NotifyEntryChanged()
		{
			this._contentsChanged = true;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000CA06 File Offset: 0x0000AC06
		internal Stream StreamForDiskNumber(uint diskNumber)
		{
			if (diskNumber + 1U == this._diskNumberWithCd || (diskNumber == 0U && this._diskNumberWithCd == 0U))
			{
				return this.ReadStream;
			}
			return ZipSegmentedStream.ForReading(this._readName ?? this._name, diskNumber, this._diskNumberWithCd);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000CA44 File Offset: 0x0000AC44
		internal void Reset(bool whileSaving)
		{
			if (this._JustSaved)
			{
				using (ZipFile zipFile = new ZipFile())
				{
					zipFile._readName = (zipFile._name = (whileSaving ? (this._readName ?? this._name) : this._name));
					zipFile.AlternateEncoding = this.AlternateEncoding;
					zipFile.AlternateEncodingUsage = this.AlternateEncodingUsage;
					ZipFile.ReadIntoInstance(zipFile);
					foreach (ZipEntry zipEntry in zipFile)
					{
						foreach (ZipEntry zipEntry2 in this)
						{
							if (zipEntry.FileName == zipEntry2.FileName)
							{
								zipEntry2.CopyMetaData(zipEntry);
								break;
							}
						}
					}
				}
				this._JustSaved = false;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000CB54 File Offset: 0x0000AD54
		public ZipFile(string fileName)
		{
			try
			{
				this._InitInstance(fileName, null);
			}
			catch (Exception innerException)
			{
				throw new ZipException(string.Format("Could not read {0} as a zip file", fileName), innerException);
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000CBE8 File Offset: 0x0000ADE8
		public ZipFile(string fileName, Encoding encoding)
		{
			try
			{
				this.AlternateEncoding = encoding;
				this.AlternateEncodingUsage = ZipOption.Always;
				this._InitInstance(fileName, null);
			}
			catch (Exception innerException)
			{
				throw new ZipException(string.Format("{0} is not a valid zip file", fileName), innerException);
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000CC8C File Offset: 0x0000AE8C
		public ZipFile()
		{
			this._InitInstance(null, null);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000CCFC File Offset: 0x0000AEFC
		public ZipFile(Encoding encoding)
		{
			this.AlternateEncoding = encoding;
			this.AlternateEncodingUsage = ZipOption.Always;
			this._InitInstance(null, null);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000CD7C File Offset: 0x0000AF7C
		public ZipFile(string fileName, TextWriter statusMessageWriter)
		{
			try
			{
				this._InitInstance(fileName, statusMessageWriter);
			}
			catch (Exception innerException)
			{
				throw new ZipException(string.Format("{0} is not a valid zip file", fileName), innerException);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000CE10 File Offset: 0x0000B010
		public ZipFile(string fileName, TextWriter statusMessageWriter, Encoding encoding)
		{
			try
			{
				this.AlternateEncoding = encoding;
				this.AlternateEncodingUsage = ZipOption.Always;
				this._InitInstance(fileName, statusMessageWriter);
			}
			catch (Exception innerException)
			{
				throw new ZipException(string.Format("{0} is not a valid zip file", fileName), innerException);
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
		public void Initialize(string fileName)
		{
			try
			{
				this._InitInstance(fileName, null);
			}
			catch (Exception innerException)
			{
				throw new ZipException(string.Format("{0} is not a valid zip file", fileName), innerException);
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
		private void _initEntriesDictionary()
		{
			StringComparer comparer = this.CaseSensitiveRetrieval ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase;
			this._entries = ((this._entries == null) ? new Dictionary<string, ZipEntry>(comparer) : new Dictionary<string, ZipEntry>(this._entries, comparer));
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000CF34 File Offset: 0x0000B134
		private void _InitInstance(string zipFileName, TextWriter statusMessageWriter)
		{
			this._name = zipFileName;
			this._StatusMessageTextWriter = statusMessageWriter;
			this._contentsChanged = true;
			this.AddDirectoryWillTraverseReparsePoints = true;
			this.CompressionLevel = CompressionLevel.Default;
			this.ParallelDeflateThreshold = 524288L;
			this._initEntriesDictionary();
			if (File.Exists(this._name))
			{
				if (this.FullScan)
				{
					ZipFile.ReadIntoInstance_Orig(this);
				}
				else
				{
					ZipFile.ReadIntoInstance(this);
				}
				this._fileAlreadyExists = true;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000CFA0 File Offset: 0x0000B1A0
		private List<ZipEntry> ZipEntriesAsList
		{
			get
			{
				if (this._zipEntriesAsList == null)
				{
					this._zipEntriesAsList = new List<ZipEntry>(this._entries.Values);
				}
				return this._zipEntriesAsList;
			}
		}

		// Token: 0x17000089 RID: 137
		public ZipEntry this[int ix]
		{
			get
			{
				return this.ZipEntriesAsList[ix];
			}
		}

		// Token: 0x1700008A RID: 138
		public ZipEntry this[string fileName]
		{
			get
			{
				string text = SharedUtilities.NormalizePathForUseInZipFile(fileName);
				if (this._entries.ContainsKey(text))
				{
					return this._entries[text];
				}
				text = text.Replace("/", "\\");
				if (this._entries.ContainsKey(text))
				{
					return this._entries[text];
				}
				return null;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000D030 File Offset: 0x0000B230
		public ICollection<string> EntryFileNames
		{
			get
			{
				return this._entries.Keys;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000D03D File Offset: 0x0000B23D
		public ICollection<ZipEntry> Entries
		{
			get
			{
				return this._entries.Values;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000D06C File Offset: 0x0000B26C
		public ICollection<ZipEntry> EntriesSorted
		{
			get
			{
				List<ZipEntry> list = new List<ZipEntry>();
				foreach (ZipEntry item in this.Entries)
				{
					list.Add(item);
				}
				StringComparison sc = this.CaseSensitiveRetrieval ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
				list.Sort((ZipEntry x, ZipEntry y) => string.Compare(x.FileName, y.FileName, sc));
				return list.AsReadOnly();
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000D0F0 File Offset: 0x0000B2F0
		public int Count
		{
			get
			{
				return this._entries.Count;
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000D0FD File Offset: 0x0000B2FD
		public void RemoveEntry(ZipEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			this._entries.Remove(SharedUtilities.NormalizePathForUseInZipFile(entry.FileName));
			this._zipEntriesAsList = null;
			this._contentsChanged = true;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000D134 File Offset: 0x0000B334
		public void RemoveEntry(string fileName)
		{
			string fileName2 = ZipEntry.NameInArchive(fileName, null);
			ZipEntry zipEntry = this[fileName2];
			if (zipEntry == null)
			{
				throw new ArgumentException("The entry you specified was not found in the zip archive.");
			}
			this.RemoveEntry(zipEntry);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000D166 File Offset: 0x0000B366
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000D178 File Offset: 0x0000B378
		protected virtual void Dispose(bool disposeManagedResources)
		{
			if (!this._disposed)
			{
				if (disposeManagedResources)
				{
					if (this._ReadStreamIsOurs && this._readstream != null)
					{
						this._readstream.Dispose();
						this._readstream = null;
					}
					if (this._temporaryFileName != null && this._name != null && this._writestream != null)
					{
						this._writestream.Dispose();
						this._writestream = null;
					}
					if (this.ParallelDeflater != null)
					{
						this.ParallelDeflater.Dispose();
						this.ParallelDeflater = null;
					}
				}
				this._disposed = true;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000D200 File Offset: 0x0000B400
		internal Stream ReadStream
		{
			get
			{
				if (this._readstream == null && (this._readName != null || this._name != null))
				{
					this._readstream = File.Open(this._readName ?? this._name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					this._ReadStreamIsOurs = true;
				}
				return this._readstream;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000D250 File Offset: 0x0000B450
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000D2CD File Offset: 0x0000B4CD
		private Stream WriteStream
		{
			get
			{
				if (this._writestream != null)
				{
					return this._writestream;
				}
				if (this._name == null)
				{
					return this._writestream;
				}
				if (this._maxOutputSegmentSize != 0)
				{
					this._writestream = ZipSegmentedStream.ForWriting(this._name, this._maxOutputSegmentSize);
					return this._writestream;
				}
				SharedUtilities.CreateAndOpenUniqueTempFile(this.TempFileFolder ?? Path.GetDirectoryName(this._name), out this._writestream, out this._temporaryFileName);
				return this._writestream;
			}
			set
			{
				if (value != null)
				{
					throw new ZipException("Cannot set the stream to a non-null value.");
				}
				this._writestream = null;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000D2E4 File Offset: 0x0000B4E4
		private string ArchiveNameForEvent
		{
			get
			{
				if (this._name == null)
				{
					return "(stream)";
				}
				return this._name;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600023A RID: 570 RVA: 0x0000D2FC File Offset: 0x0000B4FC
		// (remove) Token: 0x0600023B RID: 571 RVA: 0x0000D334 File Offset: 0x0000B534
		public event EventHandler<SaveProgressEventArgs> SaveProgress;

		// Token: 0x0600023C RID: 572 RVA: 0x0000D36C File Offset: 0x0000B56C
		internal bool OnSaveBlock(ZipEntry entry, long bytesXferred, long totalBytesToXfer)
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs saveProgressEventArgs = SaveProgressEventArgs.ByteUpdate(this.ArchiveNameForEvent, entry, bytesXferred, totalBytesToXfer);
				saveProgress(this, saveProgressEventArgs);
				if (saveProgressEventArgs.Cancel)
				{
					this._saveOperationCanceled = true;
				}
			}
			return this._saveOperationCanceled;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000D3B0 File Offset: 0x0000B5B0
		private void OnSaveEntry(int current, ZipEntry entry, bool before)
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs saveProgressEventArgs = new SaveProgressEventArgs(this.ArchiveNameForEvent, before, this._entries.Count, current, entry);
				saveProgress(this, saveProgressEventArgs);
				if (saveProgressEventArgs.Cancel)
				{
					this._saveOperationCanceled = true;
				}
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000D3F8 File Offset: 0x0000B5F8
		private void OnSaveEvent(ZipProgressEventType eventFlavor)
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs saveProgressEventArgs = new SaveProgressEventArgs(this.ArchiveNameForEvent, eventFlavor);
				saveProgress(this, saveProgressEventArgs);
				if (saveProgressEventArgs.Cancel)
				{
					this._saveOperationCanceled = true;
				}
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000D434 File Offset: 0x0000B634
		private void OnSaveStarted()
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs saveProgressEventArgs = SaveProgressEventArgs.Started(this.ArchiveNameForEvent);
				saveProgress(this, saveProgressEventArgs);
				if (saveProgressEventArgs.Cancel)
				{
					this._saveOperationCanceled = true;
				}
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000D470 File Offset: 0x0000B670
		private void OnSaveCompleted()
		{
			EventHandler<SaveProgressEventArgs> saveProgress = this.SaveProgress;
			if (saveProgress != null)
			{
				SaveProgressEventArgs e = SaveProgressEventArgs.Completed(this.ArchiveNameForEvent);
				saveProgress(this, e);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000241 RID: 577 RVA: 0x0000D49C File Offset: 0x0000B69C
		// (remove) Token: 0x06000242 RID: 578 RVA: 0x0000D4D4 File Offset: 0x0000B6D4
		public event EventHandler<ReadProgressEventArgs> ReadProgress;

		// Token: 0x06000243 RID: 579 RVA: 0x0000D50C File Offset: 0x0000B70C
		private void OnReadStarted()
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			if (readProgress != null)
			{
				ReadProgressEventArgs e = ReadProgressEventArgs.Started(this.ArchiveNameForEvent);
				readProgress(this, e);
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000D538 File Offset: 0x0000B738
		private void OnReadCompleted()
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			if (readProgress != null)
			{
				ReadProgressEventArgs e = ReadProgressEventArgs.Completed(this.ArchiveNameForEvent);
				readProgress(this, e);
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000D564 File Offset: 0x0000B764
		internal void OnReadBytes(ZipEntry entry)
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			if (readProgress != null)
			{
				ReadProgressEventArgs e = ReadProgressEventArgs.ByteUpdate(this.ArchiveNameForEvent, entry, this.ReadStream.Position, this.LengthOfReadStream);
				readProgress(this, e);
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000D5A4 File Offset: 0x0000B7A4
		internal void OnReadEntry(bool before, ZipEntry entry)
		{
			EventHandler<ReadProgressEventArgs> readProgress = this.ReadProgress;
			if (readProgress != null)
			{
				ReadProgressEventArgs e = before ? ReadProgressEventArgs.Before(this.ArchiveNameForEvent, this._entries.Count) : ReadProgressEventArgs.After(this.ArchiveNameForEvent, entry, this._entries.Count);
				readProgress(this, e);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000D5F6 File Offset: 0x0000B7F6
		private long LengthOfReadStream
		{
			get
			{
				if (this._lengthOfReadStream == -99L)
				{
					this._lengthOfReadStream = (this._ReadStreamIsOurs ? SharedUtilities.GetFileLength(this._name) : -1L);
				}
				return this._lengthOfReadStream;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000248 RID: 584 RVA: 0x0000D628 File Offset: 0x0000B828
		// (remove) Token: 0x06000249 RID: 585 RVA: 0x0000D660 File Offset: 0x0000B860
		public event EventHandler<ExtractProgressEventArgs> ExtractProgress;

		// Token: 0x0600024A RID: 586 RVA: 0x0000D698 File Offset: 0x0000B898
		private void OnExtractEntry(int current, bool before, ZipEntry currentEntry, string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs extractProgressEventArgs = new ExtractProgressEventArgs(this.ArchiveNameForEvent, before, this._entries.Count, current, currentEntry, path);
				extractProgress(this, extractProgressEventArgs);
				if (extractProgressEventArgs.Cancel)
				{
					this._extractOperationCanceled = true;
				}
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000D6E4 File Offset: 0x0000B8E4
		internal bool OnExtractBlock(ZipEntry entry, long bytesWritten, long totalBytesToWrite)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs extractProgressEventArgs = ExtractProgressEventArgs.ByteUpdate(this.ArchiveNameForEvent, entry, bytesWritten, totalBytesToWrite);
				extractProgress(this, extractProgressEventArgs);
				if (extractProgressEventArgs.Cancel)
				{
					this._extractOperationCanceled = true;
				}
			}
			return this._extractOperationCanceled;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000D728 File Offset: 0x0000B928
		internal bool OnSingleEntryExtract(ZipEntry entry, string path, bool before)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs extractProgressEventArgs = before ? ExtractProgressEventArgs.BeforeExtractEntry(this.ArchiveNameForEvent, entry, path) : ExtractProgressEventArgs.AfterExtractEntry(this.ArchiveNameForEvent, entry, path);
				extractProgress(this, extractProgressEventArgs);
				if (extractProgressEventArgs.Cancel)
				{
					this._extractOperationCanceled = true;
				}
			}
			return this._extractOperationCanceled;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000D77C File Offset: 0x0000B97C
		internal bool OnExtractExisting(ZipEntry entry, string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs extractProgressEventArgs = ExtractProgressEventArgs.ExtractExisting(this.ArchiveNameForEvent, entry, path);
				extractProgress(this, extractProgressEventArgs);
				if (extractProgressEventArgs.Cancel)
				{
					this._extractOperationCanceled = true;
				}
			}
			return this._extractOperationCanceled;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
		private void OnExtractAllCompleted(string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs e = ExtractProgressEventArgs.ExtractAllCompleted(this.ArchiveNameForEvent, path);
				extractProgress(this, e);
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		private void OnExtractAllStarted(string path)
		{
			EventHandler<ExtractProgressEventArgs> extractProgress = this.ExtractProgress;
			if (extractProgress != null)
			{
				ExtractProgressEventArgs e = ExtractProgressEventArgs.ExtractAllStarted(this.ArchiveNameForEvent, path);
				extractProgress(this, e);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000250 RID: 592 RVA: 0x0000D818 File Offset: 0x0000BA18
		// (remove) Token: 0x06000251 RID: 593 RVA: 0x0000D850 File Offset: 0x0000BA50
		public event EventHandler<AddProgressEventArgs> AddProgress;

		// Token: 0x06000252 RID: 594 RVA: 0x0000D888 File Offset: 0x0000BA88
		private void OnAddStarted()
		{
			EventHandler<AddProgressEventArgs> addProgress = this.AddProgress;
			if (addProgress != null)
			{
				AddProgressEventArgs addProgressEventArgs = AddProgressEventArgs.Started(this.ArchiveNameForEvent);
				addProgress(this, addProgressEventArgs);
				if (addProgressEventArgs.Cancel)
				{
					this._addOperationCanceled = true;
				}
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000D8C4 File Offset: 0x0000BAC4
		private void OnAddCompleted()
		{
			EventHandler<AddProgressEventArgs> addProgress = this.AddProgress;
			if (addProgress != null)
			{
				AddProgressEventArgs e = AddProgressEventArgs.Completed(this.ArchiveNameForEvent);
				addProgress(this, e);
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000D8F0 File Offset: 0x0000BAF0
		internal void AfterAddEntry(ZipEntry entry)
		{
			EventHandler<AddProgressEventArgs> addProgress = this.AddProgress;
			if (addProgress != null)
			{
				AddProgressEventArgs addProgressEventArgs = AddProgressEventArgs.AfterEntry(this.ArchiveNameForEvent, entry, this._entries.Count);
				addProgress(this, addProgressEventArgs);
				if (addProgressEventArgs.Cancel)
				{
					this._addOperationCanceled = true;
				}
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000255 RID: 597 RVA: 0x0000D938 File Offset: 0x0000BB38
		// (remove) Token: 0x06000256 RID: 598 RVA: 0x0000D970 File Offset: 0x0000BB70
		public event EventHandler<ZipErrorEventArgs> ZipError;

		// Token: 0x06000257 RID: 599 RVA: 0x0000D9A8 File Offset: 0x0000BBA8
		internal bool OnZipErrorSaving(ZipEntry entry, Exception exc)
		{
			if (this.ZipError != null)
			{
				lock (this.LOCK)
				{
					ZipErrorEventArgs zipErrorEventArgs = ZipErrorEventArgs.Saving(this.Name, entry, exc);
					this.ZipError(this, zipErrorEventArgs);
					if (zipErrorEventArgs.Cancel)
					{
						this._saveOperationCanceled = true;
					}
				}
			}
			return this._saveOperationCanceled;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000DA14 File Offset: 0x0000BC14
		public void ExtractAll(string path)
		{
			this._InternalExtractAll(path, true);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000DA1E File Offset: 0x0000BC1E
		public void ExtractAll(string path, ExtractExistingFileAction extractExistingFile)
		{
			this.ExtractExistingFile = extractExistingFile;
			this._InternalExtractAll(path, true);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000DA30 File Offset: 0x0000BC30
		private void _InternalExtractAll(string path, bool overrideExtractExistingProperty)
		{
			bool flag = this.Verbose;
			this._inExtractAll = true;
			try
			{
				this.OnExtractAllStarted(path);
				int num = 0;
				foreach (ZipEntry zipEntry in this._entries.Values)
				{
					if (flag)
					{
						this.StatusMessageTextWriter.WriteLine("\n{1,-22} {2,-8} {3,4}   {4,-8}  {0}", new object[]
						{
							"Name",
							"Modified",
							"Size",
							"Ratio",
							"Packed"
						});
						this.StatusMessageTextWriter.WriteLine(new string('-', 72));
						flag = false;
					}
					if (this.Verbose)
					{
						this.StatusMessageTextWriter.WriteLine("{1,-22} {2,-8} {3,4:F0}%   {4,-8} {0}", new object[]
						{
							zipEntry.FileName,
							zipEntry.LastModified.ToString("yyyy-MM-dd HH:mm:ss"),
							zipEntry.UncompressedSize,
							zipEntry.CompressionRatio,
							zipEntry.CompressedSize
						});
						if (!string.IsNullOrEmpty(zipEntry.Comment))
						{
							this.StatusMessageTextWriter.WriteLine("  Comment: {0}", zipEntry.Comment);
						}
					}
					zipEntry.Password = this._Password;
					this.OnExtractEntry(num, true, zipEntry, path);
					if (overrideExtractExistingProperty)
					{
						zipEntry.ExtractExistingFile = this.ExtractExistingFile;
					}
					zipEntry.Extract(path);
					num++;
					this.OnExtractEntry(num, false, zipEntry, path);
					if (this._extractOperationCanceled)
					{
						break;
					}
				}
				if (!this._extractOperationCanceled)
				{
					foreach (ZipEntry zipEntry2 in this._entries.Values)
					{
						if (zipEntry2.IsDirectory || zipEntry2.FileName.EndsWith("/"))
						{
							string fileOrDirectory = zipEntry2.FileName.StartsWith("/") ? Path.Combine(path, zipEntry2.FileName.Substring(1)) : Path.Combine(path, zipEntry2.FileName);
							zipEntry2._SetTimes(fileOrDirectory, false);
						}
					}
					this.OnExtractAllCompleted(path);
				}
			}
			finally
			{
				this._inExtractAll = false;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000DCBC File Offset: 0x0000BEBC
		public static ZipFile Read(string fileName)
		{
			return ZipFile.Read(fileName, null, null, null);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000DCC7 File Offset: 0x0000BEC7
		public static ZipFile Read(string fileName, ReadOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return ZipFile.Read(fileName, options.StatusMessageWriter, options.Encoding, options.ReadProgress);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000DCF0 File Offset: 0x0000BEF0
		private static ZipFile Read(string fileName, TextWriter statusMessageWriter, Encoding encoding, EventHandler<ReadProgressEventArgs> readProgress)
		{
			ZipFile zipFile = new ZipFile();
			zipFile.AlternateEncoding = (encoding ?? ZipFile.DefaultEncoding);
			zipFile.AlternateEncodingUsage = ZipOption.Always;
			zipFile._StatusMessageTextWriter = statusMessageWriter;
			zipFile._name = fileName;
			if (readProgress != null)
			{
				zipFile.ReadProgress = readProgress;
			}
			if (zipFile.Verbose)
			{
				zipFile._StatusMessageTextWriter.WriteLine("reading from {0}...", fileName);
			}
			ZipFile.ReadIntoInstance(zipFile);
			zipFile._fileAlreadyExists = true;
			return zipFile;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000DD59 File Offset: 0x0000BF59
		public static ZipFile Read(Stream zipStream)
		{
			return ZipFile.Read(zipStream, null, null, null);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000DD64 File Offset: 0x0000BF64
		public static ZipFile Read(Stream zipStream, ReadOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return ZipFile.Read(zipStream, options.StatusMessageWriter, options.Encoding, options.ReadProgress);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000DD8C File Offset: 0x0000BF8C
		private static ZipFile Read(Stream zipStream, TextWriter statusMessageWriter, Encoding encoding, EventHandler<ReadProgressEventArgs> readProgress)
		{
			if (zipStream == null)
			{
				throw new ArgumentNullException("zipStream");
			}
			ZipFile zipFile = new ZipFile();
			zipFile._StatusMessageTextWriter = statusMessageWriter;
			zipFile._alternateEncoding = (encoding ?? ZipFile.DefaultEncoding);
			zipFile._alternateEncodingUsage = ZipOption.Always;
			if (readProgress != null)
			{
				zipFile.ReadProgress += readProgress;
			}
			zipFile._readstream = ((zipStream.Position == 0L) ? zipStream : new OffsetStream(zipStream));
			zipFile._ReadStreamIsOurs = false;
			if (zipFile.Verbose)
			{
				zipFile._StatusMessageTextWriter.WriteLine("reading from stream...");
			}
			ZipFile.ReadIntoInstance(zipFile);
			return zipFile;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000DE14 File Offset: 0x0000C014
		private static void ReadIntoInstance(ZipFile zf)
		{
			Stream readStream = zf.ReadStream;
			try
			{
				zf._readName = zf._name;
				if (!readStream.CanSeek)
				{
					ZipFile.ReadIntoInstance_Orig(zf);
					return;
				}
				zf.OnReadStarted();
				uint num = ZipFile.ReadFirstFourBytes(readStream);
				if (num == 101010256U)
				{
					return;
				}
				int num2 = 0;
				bool flag = false;
				long num3 = readStream.Length - 64L;
				long num4 = Math.Max(readStream.Length - 16384L, 10L);
				do
				{
					if (num3 < 0L)
					{
						num3 = 0L;
					}
					readStream.Seek(num3, SeekOrigin.Begin);
					long num5 = SharedUtilities.FindSignature(readStream, 101010256);
					if (num5 != -1L)
					{
						flag = true;
					}
					else
					{
						if (num3 == 0L)
						{
							break;
						}
						num2++;
						num3 -= (long)(32 * (num2 + 1) * num2);
					}
				}
				while (!flag && num3 > num4);
				if (flag)
				{
					zf._locEndOfCDS = readStream.Position - 4L;
					byte[] array = new byte[16];
					readStream.Read(array, 0, array.Length);
					zf._diskNumberWithCd = (uint)BitConverter.ToUInt16(array, 2);
					if (zf._diskNumberWithCd == 65535U)
					{
						throw new ZipException("Spanned archives with more than 65534 segments are not supported at this time.");
					}
					zf._diskNumberWithCd += 1U;
					int startIndex = 12;
					uint num6 = BitConverter.ToUInt32(array, startIndex);
					if (num6 == 4294967295U)
					{
						ZipFile.Zip64SeekToCentralDirectory(zf);
					}
					else
					{
						zf._OffsetOfCentralDirectory = num6;
						readStream.Seek((long)((ulong)num6), SeekOrigin.Begin);
					}
					ZipFile.ReadCentralDirectory(zf);
				}
				else
				{
					readStream.Seek(0L, SeekOrigin.Begin);
					ZipFile.ReadIntoInstance_Orig(zf);
				}
			}
			catch (Exception innerException)
			{
				if (zf._ReadStreamIsOurs && zf._readstream != null)
				{
					zf._readstream.Dispose();
					zf._readstream = null;
				}
				throw new ZipException("Cannot read that as a ZipFile", innerException);
			}
			zf._contentsChanged = false;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		private static void Zip64SeekToCentralDirectory(ZipFile zf)
		{
			Stream readStream = zf.ReadStream;
			byte[] array = new byte[16];
			readStream.Seek(-40L, SeekOrigin.Current);
			readStream.Read(array, 0, 16);
			long num = BitConverter.ToInt64(array, 8);
			zf._OffsetOfCentralDirectory = uint.MaxValue;
			zf._OffsetOfCentralDirectory64 = num;
			readStream.Seek(num, SeekOrigin.Begin);
			uint num2 = (uint)SharedUtilities.ReadInt(readStream);
			if (num2 != 101075792U)
			{
				throw new BadReadException(string.Format("  Bad signature (0x{0:X8}) looking for ZIP64 EoCD Record at position 0x{1:X8}", num2, readStream.Position));
			}
			readStream.Read(array, 0, 8);
			long num3 = BitConverter.ToInt64(array, 0);
			array = new byte[num3];
			readStream.Read(array, 0, array.Length);
			num = BitConverter.ToInt64(array, 36);
			readStream.Seek(num, SeekOrigin.Begin);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000E090 File Offset: 0x0000C290
		private static uint ReadFirstFourBytes(Stream s)
		{
			return (uint)SharedUtilities.ReadInt(s);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000E0A8 File Offset: 0x0000C2A8
		private static void ReadCentralDirectory(ZipFile zf)
		{
			bool flag = false;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ZipEntry zipEntry;
			while ((zipEntry = ZipEntry.ReadDirEntry(zf, dictionary)) != null)
			{
				zipEntry.ResetDirEntry();
				zf.OnReadEntry(true, null);
				if (zf.Verbose)
				{
					zf.StatusMessageTextWriter.WriteLine("entry {0}", zipEntry.FileName);
				}
				zf._entries.Add(zipEntry.FileName, zipEntry);
				if (zipEntry._InputUsesZip64)
				{
					flag = true;
				}
				dictionary.Add(zipEntry.FileName, null);
			}
			if (flag)
			{
				zf.UseZip64WhenSaving = Zip64Option.Always;
			}
			if (zf._locEndOfCDS > 0L)
			{
				zf.ReadStream.Seek(zf._locEndOfCDS, SeekOrigin.Begin);
			}
			ZipFile.ReadCentralDirectoryFooter(zf);
			if (zf.Verbose && !string.IsNullOrEmpty(zf.Comment))
			{
				zf.StatusMessageTextWriter.WriteLine("Zip file Comment: {0}", zf.Comment);
			}
			if (zf.Verbose)
			{
				zf.StatusMessageTextWriter.WriteLine("read in {0} entries.", zf._entries.Count);
			}
			zf.OnReadCompleted();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000E1A8 File Offset: 0x0000C3A8
		private static void ReadIntoInstance_Orig(ZipFile zf)
		{
			zf.OnReadStarted();
			zf._entries = new Dictionary<string, ZipEntry>();
			if (zf.Verbose)
			{
				if (zf.Name == null)
				{
					zf.StatusMessageTextWriter.WriteLine("Reading zip from stream...");
				}
				else
				{
					zf.StatusMessageTextWriter.WriteLine("Reading zip {0}...", zf.Name);
				}
			}
			bool first = true;
			ZipContainer zc = new ZipContainer(zf);
			ZipEntry zipEntry;
			while ((zipEntry = ZipEntry.ReadEntry(zc, first)) != null)
			{
				if (zf.Verbose)
				{
					zf.StatusMessageTextWriter.WriteLine("  {0}", zipEntry.FileName);
				}
				zf._entries.Add(zipEntry.FileName, zipEntry);
				first = false;
			}
			try
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				ZipEntry zipEntry2;
				while ((zipEntry2 = ZipEntry.ReadDirEntry(zf, dictionary)) != null)
				{
					ZipEntry zipEntry3 = zf._entries[zipEntry2.FileName];
					if (zipEntry3 != null)
					{
						zipEntry3._Comment = zipEntry2.Comment;
						if (zipEntry2.IsDirectory)
						{
							zipEntry3.MarkAsDirectory();
						}
					}
					dictionary.Add(zipEntry2.FileName, null);
				}
				if (zf._locEndOfCDS > 0L)
				{
					zf.ReadStream.Seek(zf._locEndOfCDS, SeekOrigin.Begin);
				}
				ZipFile.ReadCentralDirectoryFooter(zf);
				if (zf.Verbose && !string.IsNullOrEmpty(zf.Comment))
				{
					zf.StatusMessageTextWriter.WriteLine("Zip file Comment: {0}", zf.Comment);
				}
			}
			catch (ZipException)
			{
			}
			catch (IOException)
			{
			}
			zf.OnReadCompleted();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000E314 File Offset: 0x0000C514
		private static void ReadCentralDirectoryFooter(ZipFile zf)
		{
			Stream readStream = zf.ReadStream;
			int num = SharedUtilities.ReadSignature(readStream);
			int num2 = 0;
			byte[] array;
			if ((long)num == 101075792L)
			{
				array = new byte[52];
				readStream.Read(array, 0, array.Length);
				long num3 = BitConverter.ToInt64(array, 0);
				if (num3 < 44L)
				{
					throw new ZipException("Bad size in the ZIP64 Central Directory.");
				}
				zf._versionMadeBy = BitConverter.ToUInt16(array, num2);
				num2 += 2;
				zf._versionNeededToExtract = BitConverter.ToUInt16(array, num2);
				num2 += 2;
				zf._diskNumberWithCd = BitConverter.ToUInt32(array, num2);
				num2 += 2;
				array = new byte[num3 - 44L];
				readStream.Read(array, 0, array.Length);
				num = SharedUtilities.ReadSignature(readStream);
				if ((long)num != 117853008L)
				{
					throw new ZipException("Inconsistent metadata in the ZIP64 Central Directory.");
				}
				array = new byte[16];
				readStream.Read(array, 0, array.Length);
				num = SharedUtilities.ReadSignature(readStream);
			}
			if ((long)num != 101010256L)
			{
				readStream.Seek(-4L, SeekOrigin.Current);
				throw new BadReadException(string.Format("Bad signature ({0:X8}) at position 0x{1:X8}", num, readStream.Position));
			}
			array = new byte[16];
			zf.ReadStream.Read(array, 0, array.Length);
			if (zf._diskNumberWithCd == 0U)
			{
				zf._diskNumberWithCd = (uint)BitConverter.ToUInt16(array, 2);
			}
			ZipFile.ReadZipFileComment(zf);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000E45C File Offset: 0x0000C65C
		private static void ReadZipFileComment(ZipFile zf)
		{
			byte[] array = new byte[2];
			zf.ReadStream.Read(array, 0, array.Length);
			short num = (short)((int)array[0] + (int)array[1] * 256);
			if (num > 0)
			{
				array = new byte[(int)num];
				zf.ReadStream.Read(array, 0, array.Length);
				string @string = zf.AlternateEncoding.GetString(array, 0, array.Length);
				zf.Comment = @string;
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
		public static bool IsZipFile(string fileName)
		{
			return ZipFile.IsZipFile(fileName, false);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000E4D0 File Offset: 0x0000C6D0
		public static bool IsZipFile(string fileName, bool testExtract)
		{
			bool result = false;
			try
			{
				if (!File.Exists(fileName))
				{
					return false;
				}
				using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					result = ZipFile.IsZipFile(fileStream, testExtract);
				}
			}
			catch (IOException)
			{
			}
			catch (ZipException)
			{
			}
			return result;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000E53C File Offset: 0x0000C73C
		public static bool IsZipFile(Stream stream, bool testExtract)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			bool result = false;
			try
			{
				if (!stream.CanRead)
				{
					return false;
				}
				Stream @null = Stream.Null;
				using (ZipFile zipFile = ZipFile.Read(stream, null, null, null))
				{
					if (testExtract)
					{
						foreach (ZipEntry zipEntry in zipFile)
						{
							if (!zipEntry.IsDirectory)
							{
								zipEntry.Extract(@null);
							}
						}
					}
				}
				result = true;
			}
			catch (IOException)
			{
			}
			catch (ZipException)
			{
			}
			return result;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000E600 File Offset: 0x0000C800
		private void DeleteFileWithRetry(string filename)
		{
			bool flag = false;
			int num = 3;
			int num2 = 0;
			while (num2 < num && !flag)
			{
				try
				{
					File.Delete(filename);
					flag = true;
				}
				catch (UnauthorizedAccessException)
				{
					Console.WriteLine("************************************************** Retry delete.");
					Thread.Sleep(200 + num2 * 200);
				}
				num2++;
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000E65C File Offset: 0x0000C85C
		public void Save()
		{
			try
			{
				bool flag = false;
				this._saveOperationCanceled = false;
				this._numberOfSegmentsForMostRecentSave = 0U;
				this.OnSaveStarted();
				if (this.WriteStream == null)
				{
					throw new BadStateException("You haven't specified where to save the zip.");
				}
				if (this._name != null && this._name.EndsWith(".exe") && !this._SavingSfx)
				{
					throw new BadStateException("You specified an EXE for a plain zip file.");
				}
				if (!this._contentsChanged)
				{
					this.OnSaveCompleted();
					if (this.Verbose)
					{
						this.StatusMessageTextWriter.WriteLine("No save is necessary....");
					}
				}
				else
				{
					this.Reset(true);
					if (this.Verbose)
					{
						this.StatusMessageTextWriter.WriteLine("saving....");
					}
					if (this._entries.Count >= 65535 && this._zip64 == Zip64Option.Default)
					{
						throw new ZipException("The number of entries is 65535 or greater. Consider setting the UseZip64WhenSaving property on the ZipFile instance.");
					}
					int num = 0;
					ICollection<ZipEntry> collection = this.SortEntriesBeforeSaving ? this.EntriesSorted : this.Entries;
					foreach (ZipEntry zipEntry in collection)
					{
						this.OnSaveEntry(num, zipEntry, true);
						zipEntry.Write(this.WriteStream);
						if (this._saveOperationCanceled)
						{
							break;
						}
						num++;
						this.OnSaveEntry(num, zipEntry, false);
						if (this._saveOperationCanceled)
						{
							break;
						}
						if (zipEntry.IncludedInMostRecentSave)
						{
							flag |= zipEntry.OutputUsedZip64.Value;
						}
					}
					if (!this._saveOperationCanceled)
					{
						ZipSegmentedStream zipSegmentedStream = this.WriteStream as ZipSegmentedStream;
						this._numberOfSegmentsForMostRecentSave = ((zipSegmentedStream != null) ? zipSegmentedStream.CurrentSegment : 1U);
						bool flag2 = ZipOutput.WriteCentralDirectoryStructure(this.WriteStream, collection, this._numberOfSegmentsForMostRecentSave, this._zip64, this.Comment, new ZipContainer(this));
						this.OnSaveEvent(ZipProgressEventType.Saving_AfterSaveTempArchive);
						this._hasBeenSaved = true;
						this._contentsChanged = false;
						flag = (flag || flag2);
						this._OutputUsesZip64 = new bool?(flag);
						if (this._name != null && (this._temporaryFileName != null || zipSegmentedStream != null))
						{
							this.WriteStream.Dispose();
							if (this._saveOperationCanceled)
							{
								return;
							}
							if (this._fileAlreadyExists && this._readstream != null)
							{
								this._readstream.Close();
								this._readstream = null;
								foreach (ZipEntry zipEntry2 in collection)
								{
									ZipSegmentedStream zipSegmentedStream2 = zipEntry2._archiveStream as ZipSegmentedStream;
									if (zipSegmentedStream2 != null)
									{
										zipSegmentedStream2.Dispose();
									}
									zipEntry2._archiveStream = null;
								}
							}
							string text = null;
							if (File.Exists(this._name))
							{
								text = this._name + "." + Path.GetRandomFileName();
								if (File.Exists(text))
								{
									this.DeleteFileWithRetry(text);
								}
								File.Move(this._name, text);
							}
							this.OnSaveEvent(ZipProgressEventType.Saving_BeforeRenameTempArchive);
							File.Move((zipSegmentedStream != null) ? zipSegmentedStream.CurrentTempName : this._temporaryFileName, this._name);
							this.OnSaveEvent(ZipProgressEventType.Saving_AfterRenameTempArchive);
							if (text != null)
							{
								try
								{
									if (File.Exists(text))
									{
										File.Delete(text);
									}
								}
								catch
								{
								}
							}
							this._fileAlreadyExists = true;
						}
						ZipFile.NotifyEntriesSaveComplete(collection);
						this.OnSaveCompleted();
						this._JustSaved = true;
					}
				}
			}
			finally
			{
				this.CleanupAfterSaveOperation();
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000E9EC File Offset: 0x0000CBEC
		private static void NotifyEntriesSaveComplete(ICollection<ZipEntry> c)
		{
			foreach (ZipEntry zipEntry in c)
			{
				zipEntry.NotifySaveComplete();
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000EA34 File Offset: 0x0000CC34
		private void RemoveTempFile()
		{
			try
			{
				if (File.Exists(this._temporaryFileName))
				{
					File.Delete(this._temporaryFileName);
				}
			}
			catch (IOException ex)
			{
				if (this.Verbose)
				{
					this.StatusMessageTextWriter.WriteLine("ZipFile::Save: could not delete temp file: {0}.", ex.Message);
				}
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000EA8C File Offset: 0x0000CC8C
		private void CleanupAfterSaveOperation()
		{
			if (this._name != null)
			{
				if (this._writestream != null)
				{
					try
					{
						this._writestream.Dispose();
					}
					catch (IOException)
					{
					}
				}
				this._writestream = null;
				if (this._temporaryFileName != null)
				{
					this.RemoveTempFile();
					this._temporaryFileName = null;
				}
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
		public void Save(string fileName)
		{
			if (this._name == null)
			{
				this._writestream = null;
			}
			else
			{
				this._readName = this._name;
			}
			this._name = fileName;
			if (Directory.Exists(this._name))
			{
				throw new ZipException("Bad Directory", new ArgumentException("That name specifies an existing directory. Please specify a filename.", "fileName"));
			}
			this._contentsChanged = true;
			this._fileAlreadyExists = File.Exists(this._name);
			this.Save();
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000EB60 File Offset: 0x0000CD60
		public void Save(Stream outputStream)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (!outputStream.CanWrite)
			{
				throw new ArgumentException("Must be a writable stream.", "outputStream");
			}
			this._name = null;
			this._writestream = new CountingStream(outputStream);
			this._contentsChanged = true;
			this._fileAlreadyExists = false;
			this.Save();
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000EBBC File Offset: 0x0000CDBC
		public void SaveSelfExtractor(string exeToGenerate, SelfExtractorFlavor flavor)
		{
			this.SaveSelfExtractor(exeToGenerate, new SelfExtractorSaveOptions
			{
				Flavor = flavor
			});
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000EBE0 File Offset: 0x0000CDE0
		public void SaveSelfExtractor(string exeToGenerate, SelfExtractorSaveOptions options)
		{
			if (this._name == null)
			{
				this._writestream = null;
			}
			this._SavingSfx = true;
			this._name = exeToGenerate;
			if (Directory.Exists(this._name))
			{
				throw new ZipException("Bad Directory", new ArgumentException("That name specifies an existing directory. Please specify a filename.", "exeToGenerate"));
			}
			this._contentsChanged = true;
			this._fileAlreadyExists = File.Exists(this._name);
			this._SaveSfxStub(exeToGenerate, options);
			this.Save();
			this._SavingSfx = false;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000EC60 File Offset: 0x0000CE60
		private static void ExtractResourceToFile(Assembly a, string resourceName, string filename)
		{
			byte[] array = new byte[1024];
			using (Stream manifestResourceStream = a.GetManifestResourceStream(resourceName))
			{
				if (manifestResourceStream == null)
				{
					throw new ZipException(string.Format("missing resource '{0}'", resourceName));
				}
				using (FileStream fileStream = File.OpenWrite(filename))
				{
					int num;
					do
					{
						num = manifestResourceStream.Read(array, 0, array.Length);
						fileStream.Write(array, 0, num);
					}
					while (num > 0);
				}
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		private void _SaveSfxStub(string exeToGenerate, SelfExtractorSaveOptions options)
		{
			string text = null;
			string text2 = null;
			string dir = null;
			try
			{
				if (File.Exists(exeToGenerate) && this.Verbose)
				{
					this.StatusMessageTextWriter.WriteLine("The existing file ({0}) will be overwritten.", exeToGenerate);
				}
				if (!exeToGenerate.EndsWith(".exe") && this.Verbose)
				{
					this.StatusMessageTextWriter.WriteLine("Warning: The generated self-extracting file will not have an .exe extension.");
				}
				dir = (this.TempFileFolder ?? Path.GetDirectoryName(exeToGenerate));
				text = ZipFile.GenerateTempPathname(dir, "exe");
				Assembly assembly = typeof(ZipFile).Assembly;
				using (CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider())
				{
					ZipFile.ExtractorSettings extractorSettings = null;
					foreach (ZipFile.ExtractorSettings extractorSettings2 in ZipFile.SettingsList)
					{
						if (extractorSettings2.Flavor == options.Flavor)
						{
							extractorSettings = extractorSettings2;
							break;
						}
					}
					if (extractorSettings == null)
					{
						throw new BadStateException(string.Format("While saving a Self-Extracting Zip, Cannot find that flavor ({0})?", options.Flavor));
					}
					CompilerParameters compilerParameters = new CompilerParameters();
					compilerParameters.ReferencedAssemblies.Add(assembly.Location);
					if (extractorSettings.ReferencedAssemblies != null)
					{
						foreach (string value in extractorSettings.ReferencedAssemblies)
						{
							compilerParameters.ReferencedAssemblies.Add(value);
						}
					}
					compilerParameters.GenerateInMemory = false;
					compilerParameters.GenerateExecutable = true;
					compilerParameters.IncludeDebugInformation = false;
					compilerParameters.CompilerOptions = "";
					Assembly executingAssembly = Assembly.GetExecutingAssembly();
					StringBuilder stringBuilder = new StringBuilder();
					string text3 = ZipFile.GenerateTempPathname(dir, "cs");
					using (ZipFile zipFile = ZipFile.Read(executingAssembly.GetManifestResourceStream("Ionic.Zip.Resources.ZippedResources.zip")))
					{
						text2 = ZipFile.GenerateTempPathname(dir, "tmp");
						if (string.IsNullOrEmpty(options.IconFile))
						{
							Directory.CreateDirectory(text2);
							ZipEntry zipEntry = zipFile["zippedFile.ico"];
							if ((zipEntry.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
							{
								zipEntry.Attributes ^= FileAttributes.ReadOnly;
							}
							zipEntry.Extract(text2);
							string arg = Path.Combine(text2, "zippedFile.ico");
							CompilerParameters compilerParameters2 = compilerParameters;
							compilerParameters2.CompilerOptions += string.Format("/win32icon:\"{0}\"", arg);
						}
						else
						{
							CompilerParameters compilerParameters3 = compilerParameters;
							compilerParameters3.CompilerOptions += string.Format("/win32icon:\"{0}\"", options.IconFile);
						}
						compilerParameters.OutputAssembly = text;
						if (options.Flavor == SelfExtractorFlavor.WinFormsApplication)
						{
							CompilerParameters compilerParameters4 = compilerParameters;
							compilerParameters4.CompilerOptions += " /target:winexe";
						}
						if (!string.IsNullOrEmpty(options.AdditionalCompilerSwitches))
						{
							CompilerParameters compilerParameters5 = compilerParameters;
							compilerParameters5.CompilerOptions = compilerParameters5.CompilerOptions + " " + options.AdditionalCompilerSwitches;
						}
						if (string.IsNullOrEmpty(compilerParameters.CompilerOptions))
						{
							compilerParameters.CompilerOptions = null;
						}
						if (extractorSettings.CopyThroughResources != null && extractorSettings.CopyThroughResources.Count != 0)
						{
							if (!Directory.Exists(text2))
							{
								Directory.CreateDirectory(text2);
							}
							foreach (string text4 in extractorSettings.CopyThroughResources)
							{
								string text5 = Path.Combine(text2, text4);
								ZipFile.ExtractResourceToFile(executingAssembly, text4, text5);
								compilerParameters.EmbeddedResources.Add(text5);
							}
						}
						compilerParameters.EmbeddedResources.Add(assembly.Location);
						stringBuilder.Append("// " + Path.GetFileName(text3) + "\n").Append("// --------------------------------------------\n//\n").Append("// This SFX source file was generated by DotNetZip ").Append(ZipFile.LibraryVersion.ToString()).Append("\n//         at ").Append(DateTime.Now.ToString("yyyy MMMM dd  HH:mm:ss")).Append("\n//\n// --------------------------------------------\n\n\n");
						if (!string.IsNullOrEmpty(options.Description))
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyTitle(\"" + options.Description.Replace("\"", "") + "\")]\n");
						}
						else
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyTitle(\"DotNetZip SFX Archive\")]\n");
						}
						if (!string.IsNullOrEmpty(options.ProductVersion))
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyInformationalVersion(\"" + options.ProductVersion.Replace("\"", "") + "\")]\n");
						}
						string str = string.IsNullOrEmpty(options.Copyright) ? "Extractor: Copyright © Dino Chiesa 2008-2011" : options.Copyright.Replace("\"", "");
						if (!string.IsNullOrEmpty(options.ProductName))
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyProduct(\"").Append(options.ProductName.Replace("\"", "")).Append("\")]\n");
						}
						else
						{
							stringBuilder.Append("[assembly: System.Reflection.AssemblyProduct(\"DotNetZip\")]\n");
						}
						stringBuilder.Append("[assembly: System.Reflection.AssemblyCopyright(\"" + str + "\")]\n").Append(string.Format("[assembly: System.Reflection.AssemblyVersion(\"{0}\")]\n", ZipFile.LibraryVersion.ToString()));
						if (options.FileVersion != null)
						{
							stringBuilder.Append(string.Format("[assembly: System.Reflection.AssemblyFileVersion(\"{0}\")]\n", options.FileVersion.ToString()));
						}
						stringBuilder.Append("\n\n\n");
						string text6 = options.DefaultExtractDirectory;
						if (text6 != null)
						{
							text6 = text6.Replace("\"", "").Replace("\\", "\\\\");
						}
						string text7 = options.PostExtractCommandLine;
						if (text7 != null)
						{
							text7 = text7.Replace("\\", "\\\\");
							text7 = text7.Replace("\"", "\\\"");
						}
						foreach (string text8 in extractorSettings.ResourcesToCompile)
						{
							using (Stream stream = zipFile[text8].OpenReader())
							{
								if (stream == null)
								{
									throw new ZipException(string.Format("missing resource '{0}'", text8));
								}
								using (StreamReader streamReader = new StreamReader(stream))
								{
									while (streamReader.Peek() >= 0)
									{
										string text9 = streamReader.ReadLine();
										if (text6 != null)
										{
											text9 = text9.Replace("@@EXTRACTLOCATION", text6);
										}
										text9 = text9.Replace("@@REMOVE_AFTER_EXECUTE", options.RemoveUnpackedFilesAfterExecute.ToString());
										text9 = text9.Replace("@@QUIET", options.Quiet.ToString());
										if (!string.IsNullOrEmpty(options.SfxExeWindowTitle))
										{
											text9 = text9.Replace("@@SFX_EXE_WINDOW_TITLE", options.SfxExeWindowTitle);
										}
										text9 = text9.Replace("@@EXTRACT_EXISTING_FILE", ((int)options.ExtractExistingFile).ToString());
										if (text7 != null)
										{
											text9 = text9.Replace("@@POST_UNPACK_CMD_LINE", text7);
										}
										stringBuilder.Append(text9).Append("\n");
									}
								}
								stringBuilder.Append("\n\n");
							}
						}
					}
					string text10 = stringBuilder.ToString();
					CompilerResults compilerResults = csharpCodeProvider.CompileAssemblyFromSource(compilerParameters, new string[]
					{
						text10
					});
					if (compilerResults == null)
					{
						throw new SfxGenerationException("Cannot compile the extraction logic!");
					}
					if (this.Verbose)
					{
						foreach (string value2 in compilerResults.Output)
						{
							this.StatusMessageTextWriter.WriteLine(value2);
						}
					}
					if (compilerResults.Errors.Count != 0)
					{
						using (TextWriter textWriter = new StreamWriter(text3))
						{
							textWriter.Write(text10);
							textWriter.Write("\n\n\n// ------------------------------------------------------------------\n");
							textWriter.Write("// Errors during compilation: \n//\n");
							string fileName = Path.GetFileName(text3);
							foreach (object obj in compilerResults.Errors)
							{
								CompilerError compilerError = (CompilerError)obj;
								textWriter.Write(string.Format("//   {0}({1},{2}): {3} {4}: {5}\n//\n", new object[]
								{
									fileName,
									compilerError.Line,
									compilerError.Column,
									compilerError.IsWarning ? "Warning" : "error",
									compilerError.ErrorNumber,
									compilerError.ErrorText
								}));
							}
						}
						throw new SfxGenerationException(string.Format("Errors compiling the extraction logic!  {0}", text3));
					}
					this.OnSaveEvent(ZipProgressEventType.Saving_AfterCompileSelfExtractor);
					using (Stream stream2 = File.OpenRead(text))
					{
						byte[] array = new byte[4000];
						int num = 1;
						while (num != 0)
						{
							num = stream2.Read(array, 0, array.Length);
							if (num != 0)
							{
								this.WriteStream.Write(array, 0, num);
							}
						}
					}
				}
				this.OnSaveEvent(ZipProgressEventType.Saving_AfterSaveTempArchive);
			}
			finally
			{
				try
				{
					if (Directory.Exists(text2))
					{
						try
						{
							Directory.Delete(text2, true);
						}
						catch (IOException arg2)
						{
							this.StatusMessageTextWriter.WriteLine("Warning: Exception: {0}", arg2);
						}
					}
					if (File.Exists(text))
					{
						try
						{
							File.Delete(text);
						}
						catch (IOException arg3)
						{
							this.StatusMessageTextWriter.WriteLine("Warning: Exception: {0}", arg3);
						}
					}
				}
				catch (IOException)
				{
				}
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000F784 File Offset: 0x0000D984
		internal static string GenerateTempPathname(string dir, string extension)
		{
			string name = Assembly.GetExecutingAssembly().GetName().Name;
			string text2;
			do
			{
				string text = Guid.NewGuid().ToString();
				string path = string.Format("{0}-{1}-{2}.{3}", new object[]
				{
					name,
					DateTime.Now.ToString("yyyyMMMdd-HHmmss"),
					text,
					extension
				});
				text2 = Path.Combine(dir, path);
			}
			while (File.Exists(text2) || Directory.Exists(text2));
			return text2;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000F80C File Offset: 0x0000DA0C
		public void AddSelectedFiles(string selectionCriteria)
		{
			this.AddSelectedFiles(selectionCriteria, ".", null, false);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000F81C File Offset: 0x0000DA1C
		public void AddSelectedFiles(string selectionCriteria, bool recurseDirectories)
		{
			this.AddSelectedFiles(selectionCriteria, ".", null, recurseDirectories);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000F82C File Offset: 0x0000DA2C
		public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk)
		{
			this.AddSelectedFiles(selectionCriteria, directoryOnDisk, null, false);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000F838 File Offset: 0x0000DA38
		public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk, bool recurseDirectories)
		{
			this.AddSelectedFiles(selectionCriteria, directoryOnDisk, null, recurseDirectories);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000F844 File Offset: 0x0000DA44
		public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive)
		{
			this.AddSelectedFiles(selectionCriteria, directoryOnDisk, directoryPathInArchive, false);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000F850 File Offset: 0x0000DA50
		public void AddSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive, bool recurseDirectories)
		{
			this._AddOrUpdateSelectedFiles(selectionCriteria, directoryOnDisk, directoryPathInArchive, recurseDirectories, false);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000F85E File Offset: 0x0000DA5E
		public void UpdateSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive, bool recurseDirectories)
		{
			this._AddOrUpdateSelectedFiles(selectionCriteria, directoryOnDisk, directoryPathInArchive, recurseDirectories, true);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000F86C File Offset: 0x0000DA6C
		private string EnsureendInSlash(string s)
		{
			if (s.EndsWith("\\"))
			{
				return s;
			}
			return s + "\\";
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000F888 File Offset: 0x0000DA88
		private void _AddOrUpdateSelectedFiles(string selectionCriteria, string directoryOnDisk, string directoryPathInArchive, bool recurseDirectories, bool wantUpdate)
		{
			if (directoryOnDisk == null && Directory.Exists(selectionCriteria))
			{
				directoryOnDisk = selectionCriteria;
				selectionCriteria = "*.*";
			}
			else if (string.IsNullOrEmpty(directoryOnDisk))
			{
				directoryOnDisk = ".";
			}
			while (directoryOnDisk.EndsWith("\\"))
			{
				directoryOnDisk = directoryOnDisk.Substring(0, directoryOnDisk.Length - 1);
			}
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("adding selection '{0}' from dir '{1}'...", selectionCriteria, directoryOnDisk);
			}
			FileSelector fileSelector = new FileSelector(selectionCriteria, this.AddDirectoryWillTraverseReparsePoints);
			ReadOnlyCollection<string> readOnlyCollection = fileSelector.SelectFiles(directoryOnDisk, recurseDirectories);
			if (this.Verbose)
			{
				this.StatusMessageTextWriter.WriteLine("found {0} files...", readOnlyCollection.Count);
			}
			this.OnAddStarted();
			AddOrUpdateAction action = wantUpdate ? AddOrUpdateAction.AddOrUpdate : AddOrUpdateAction.AddOnly;
			foreach (string text in readOnlyCollection)
			{
				string text2 = (directoryPathInArchive == null) ? null : ZipFile.ReplaceLeadingDirectory(Path.GetDirectoryName(text), directoryOnDisk, directoryPathInArchive);
				if (File.Exists(text))
				{
					if (wantUpdate)
					{
						this.UpdateFile(text, text2);
					}
					else
					{
						this.AddFile(text, text2);
					}
				}
				else
				{
					this.AddOrUpdateDirectoryImpl(text, text2, action, false, 0);
				}
			}
			this.OnAddCompleted();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000F9C4 File Offset: 0x0000DBC4
		private static string ReplaceLeadingDirectory(string original, string pattern, string replacement)
		{
			string text = original.ToUpper();
			string text2 = pattern.ToUpper();
			int num = text.IndexOf(text2);
			if (num != 0)
			{
				return original;
			}
			return replacement + original.Substring(text2.Length);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000FA00 File Offset: 0x0000DC00
		public ICollection<ZipEntry> SelectEntries(string selectionCriteria)
		{
			FileSelector fileSelector = new FileSelector(selectionCriteria, this.AddDirectoryWillTraverseReparsePoints);
			return fileSelector.SelectEntries(this);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000FA24 File Offset: 0x0000DC24
		public ICollection<ZipEntry> SelectEntries(string selectionCriteria, string directoryPathInArchive)
		{
			FileSelector fileSelector = new FileSelector(selectionCriteria, this.AddDirectoryWillTraverseReparsePoints);
			return fileSelector.SelectEntries(this, directoryPathInArchive);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000FA48 File Offset: 0x0000DC48
		public int RemoveSelectedEntries(string selectionCriteria)
		{
			ICollection<ZipEntry> collection = this.SelectEntries(selectionCriteria);
			this.RemoveEntries(collection);
			return collection.Count;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000FA6C File Offset: 0x0000DC6C
		public int RemoveSelectedEntries(string selectionCriteria, string directoryPathInArchive)
		{
			ICollection<ZipEntry> collection = this.SelectEntries(selectionCriteria, directoryPathInArchive);
			this.RemoveEntries(collection);
			return collection.Count;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public void ExtractSelectedEntries(string selectionCriteria)
		{
			foreach (ZipEntry zipEntry in this.SelectEntries(selectionCriteria))
			{
				zipEntry.Password = this._Password;
				zipEntry.Extract();
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000FAEC File Offset: 0x0000DCEC
		public void ExtractSelectedEntries(string selectionCriteria, ExtractExistingFileAction extractExistingFile)
		{
			foreach (ZipEntry zipEntry in this.SelectEntries(selectionCriteria))
			{
				zipEntry.Password = this._Password;
				zipEntry.Extract(extractExistingFile);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000FB48 File Offset: 0x0000DD48
		public void ExtractSelectedEntries(string selectionCriteria, string directoryPathInArchive)
		{
			foreach (ZipEntry zipEntry in this.SelectEntries(selectionCriteria, directoryPathInArchive))
			{
				zipEntry.Password = this._Password;
				zipEntry.Extract();
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000FBA4 File Offset: 0x0000DDA4
		public void ExtractSelectedEntries(string selectionCriteria, string directoryInArchive, string extractDirectory)
		{
			foreach (ZipEntry zipEntry in this.SelectEntries(selectionCriteria, directoryInArchive))
			{
				zipEntry.Password = this._Password;
				zipEntry.Extract(extractDirectory);
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000FC00 File Offset: 0x0000DE00
		public void ExtractSelectedEntries(string selectionCriteria, string directoryPathInArchive, string extractDirectory, ExtractExistingFileAction extractExistingFile)
		{
			foreach (ZipEntry zipEntry in this.SelectEntries(selectionCriteria, directoryPathInArchive))
			{
				zipEntry.Password = this._Password;
				zipEntry.Extract(extractDirectory, extractExistingFile);
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000FD98 File Offset: 0x0000DF98
		public IEnumerator<ZipEntry> GetEnumerator()
		{
			foreach (ZipEntry e in this._entries.Values)
			{
				yield return e;
			}
			yield break;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000FDB4 File Offset: 0x0000DFB4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000FDBC File Offset: 0x0000DFBC
		[DispId(-4)]
		public IEnumerator GetNewEnum()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000105 RID: 261
		private TextWriter _StatusMessageTextWriter;

		// Token: 0x04000106 RID: 262
		private bool _CaseSensitiveRetrieval;

		// Token: 0x04000107 RID: 263
		private Stream _readstream;

		// Token: 0x04000108 RID: 264
		private Stream _writestream;

		// Token: 0x04000109 RID: 265
		private ushort _versionMadeBy;

		// Token: 0x0400010A RID: 266
		private ushort _versionNeededToExtract;

		// Token: 0x0400010B RID: 267
		private uint _diskNumberWithCd;

		// Token: 0x0400010C RID: 268
		private int _maxOutputSegmentSize;

		// Token: 0x0400010D RID: 269
		private uint _numberOfSegmentsForMostRecentSave;

		// Token: 0x0400010E RID: 270
		private ZipErrorAction _zipErrorAction;

		// Token: 0x0400010F RID: 271
		private bool _disposed;

		// Token: 0x04000110 RID: 272
		private Dictionary<string, ZipEntry> _entries;

		// Token: 0x04000111 RID: 273
		private List<ZipEntry> _zipEntriesAsList;

		// Token: 0x04000112 RID: 274
		private string _name;

		// Token: 0x04000113 RID: 275
		private string _readName;

		// Token: 0x04000114 RID: 276
		private string _Comment;

		// Token: 0x04000115 RID: 277
		internal string _Password;

		// Token: 0x04000116 RID: 278
		private bool _emitNtfsTimes = true;

		// Token: 0x04000117 RID: 279
		private bool _emitUnixTimes;

		// Token: 0x04000118 RID: 280
		private CompressionStrategy _Strategy;

		// Token: 0x04000119 RID: 281
		private CompressionMethod _compressionMethod = CompressionMethod.Deflate;

		// Token: 0x0400011A RID: 282
		private bool _fileAlreadyExists;

		// Token: 0x0400011B RID: 283
		private string _temporaryFileName;

		// Token: 0x0400011C RID: 284
		private bool _contentsChanged;

		// Token: 0x0400011D RID: 285
		private bool _hasBeenSaved;

		// Token: 0x0400011E RID: 286
		private string _TempFileFolder;

		// Token: 0x0400011F RID: 287
		private bool _ReadStreamIsOurs = true;

		// Token: 0x04000120 RID: 288
		private object LOCK = new object();

		// Token: 0x04000121 RID: 289
		private bool _saveOperationCanceled;

		// Token: 0x04000122 RID: 290
		private bool _extractOperationCanceled;

		// Token: 0x04000123 RID: 291
		private bool _addOperationCanceled;

		// Token: 0x04000124 RID: 292
		private EncryptionAlgorithm _Encryption;

		// Token: 0x04000125 RID: 293
		private bool _JustSaved;

		// Token: 0x04000126 RID: 294
		private long _locEndOfCDS = -1L;

		// Token: 0x04000127 RID: 295
		private uint _OffsetOfCentralDirectory;

		// Token: 0x04000128 RID: 296
		private long _OffsetOfCentralDirectory64;

		// Token: 0x04000129 RID: 297
		private bool? _OutputUsesZip64;

		// Token: 0x0400012A RID: 298
		internal bool _inExtractAll;

		// Token: 0x0400012B RID: 299
		private Encoding _alternateEncoding = Encoding.GetEncoding("IBM437");

		// Token: 0x0400012C RID: 300
		private ZipOption _alternateEncodingUsage;

		// Token: 0x0400012D RID: 301
		private static Encoding _defaultEncoding = Encoding.GetEncoding("IBM437");

		// Token: 0x0400012E RID: 302
		private int _BufferSize = ZipFile.BufferSizeDefault;

		// Token: 0x0400012F RID: 303
		internal ParallelDeflateOutputStream ParallelDeflater;

		// Token: 0x04000130 RID: 304
		private long _ParallelDeflateThreshold;

		// Token: 0x04000131 RID: 305
		private int _maxBufferPairs = 16;

		// Token: 0x04000132 RID: 306
		internal Zip64Option _zip64;

		// Token: 0x04000133 RID: 307
		private bool _SavingSfx;

		// Token: 0x04000134 RID: 308
		public static readonly int BufferSizeDefault = 32768;

		// Token: 0x04000137 RID: 311
		private long _lengthOfReadStream = -99L;

		// Token: 0x0400013B RID: 315
		private static ZipFile.ExtractorSettings[] SettingsList = new ZipFile.ExtractorSettings[]
		{
			new ZipFile.ExtractorSettings
			{
				Flavor = SelfExtractorFlavor.WinFormsApplication,
				ReferencedAssemblies = new List<string>
				{
					"System.dll",
					"System.Windows.Forms.dll",
					"System.Drawing.dll"
				},
				CopyThroughResources = new List<string>
				{
					"Ionic.Zip.WinFormsSelfExtractorStub.resources",
					"Ionic.Zip.Forms.PasswordDialog.resources",
					"Ionic.Zip.Forms.ZipContentsDialog.resources"
				},
				ResourcesToCompile = new List<string>
				{
					"WinFormsSelfExtractorStub.cs",
					"WinFormsSelfExtractorStub.Designer.cs",
					"PasswordDialog.cs",
					"PasswordDialog.Designer.cs",
					"ZipContentsDialog.cs",
					"ZipContentsDialog.Designer.cs",
					"FolderBrowserDialogEx.cs"
				}
			},
			new ZipFile.ExtractorSettings
			{
				Flavor = SelfExtractorFlavor.ConsoleApplication,
				ReferencedAssemblies = new List<string>
				{
					"System.dll"
				},
				CopyThroughResources = null,
				ResourcesToCompile = new List<string>
				{
					"CommandLineSelfExtractorStub.cs"
				}
			}
		};

		// Token: 0x02000035 RID: 53
		private class ExtractorSettings
		{
			// Token: 0x04000144 RID: 324
			public SelfExtractorFlavor Flavor;

			// Token: 0x04000145 RID: 325
			public List<string> ReferencedAssemblies;

			// Token: 0x04000146 RID: 326
			public List<string> CopyThroughResources;

			// Token: 0x04000147 RID: 327
			public List<string> ResourcesToCompile;
		}
	}
}
