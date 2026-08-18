using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Encryption;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200001D RID: 29
	public class ZipFile : IEnumerable, IDisposable
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00005E74 File Offset: 0x00004E74
		private void OnKeysRequired(string fileName)
		{
			if (this.KeysRequired != null)
			{
				KeysRequiredEventArgs keysRequiredEventArgs = new KeysRequiredEventArgs(fileName, this.key);
				this.KeysRequired(this, keysRequiredEventArgs);
				this.key = keysRequiredEventArgs.Key;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00005EAF File Offset: 0x00004EAF
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00005EB7 File Offset: 0x00004EB7
		private byte[] Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00005EC0 File Offset: 0x00004EC0
		public string Password
		{
			set
			{
				if (value == null || value.Length == 0)
				{
					this.key = null;
					return;
				}
				this.rawPassword_ = value;
				this.key = PkzipClassic.GenerateKeys(ZipConstants.ConvertToArray(value));
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005EED File Offset: 0x00004EED
		private bool HaveKeys
		{
			get
			{
				return this.key != null;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005EFC File Offset: 0x00004EFC
		public ZipFile(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name_ = name;
			this.baseStream_ = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.Read);
			this.isStreamOwner = true;
			try
			{
				this.ReadEntries();
			}
			catch
			{
				this.DisposeInternal(true);
				throw;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005F7C File Offset: 0x00004F7C
		public ZipFile(FileStream file)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}
			if (!file.CanSeek)
			{
				throw new ArgumentException("Stream is not seekable", "file");
			}
			this.baseStream_ = file;
			this.name_ = file.Name;
			this.isStreamOwner = true;
			try
			{
				this.ReadEntries();
			}
			catch
			{
				this.DisposeInternal(true);
				throw;
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006010 File Offset: 0x00005010
		public ZipFile(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanSeek)
			{
				throw new ArgumentException("Stream is not seekable", "stream");
			}
			this.baseStream_ = stream;
			this.isStreamOwner = true;
			if (this.baseStream_.Length > 0L)
			{
				try
				{
					this.ReadEntries();
					return;
				}
				catch
				{
					this.DisposeInternal(true);
					throw;
				}
			}
			this.entries_ = new ZipEntry[0];
			this.isNewArchive_ = true;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000060B8 File Offset: 0x000050B8
		internal ZipFile()
		{
			this.entries_ = new ZipEntry[0];
			this.isNewArchive_ = true;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000060F0 File Offset: 0x000050F0
		~ZipFile()
		{
			this.Dispose(false);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006120 File Offset: 0x00005120
		public void Close()
		{
			this.DisposeInternal(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006130 File Offset: 0x00005130
		public static ZipFile Create(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			FileStream fileStream = File.Create(fileName);
			return new ZipFile
			{
				name_ = fileName,
				baseStream_ = fileStream,
				isStreamOwner = true
			};
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00006170 File Offset: 0x00005170
		public static ZipFile Create(Stream outStream)
		{
			if (outStream == null)
			{
				throw new ArgumentNullException("outStream");
			}
			if (!outStream.CanWrite)
			{
				throw new ArgumentException("Stream is not writeable", "outStream");
			}
			if (!outStream.CanSeek)
			{
				throw new ArgumentException("Stream is not seekable", "outStream");
			}
			return new ZipFile
			{
				baseStream_ = outStream
			};
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000061C9 File Offset: 0x000051C9
		// (set) Token: 0x060000FC RID: 252 RVA: 0x000061D1 File Offset: 0x000051D1
		public bool IsStreamOwner
		{
			get
			{
				return this.isStreamOwner;
			}
			set
			{
				this.isStreamOwner = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000061DA File Offset: 0x000051DA
		public bool IsEmbeddedArchive
		{
			get
			{
				return this.offsetOfFirstEntry > 0L;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000061E6 File Offset: 0x000051E6
		public bool IsNewArchive
		{
			get
			{
				return this.isNewArchive_;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000061EE File Offset: 0x000051EE
		public string ZipFileComment
		{
			get
			{
				return this.comment_;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000061F6 File Offset: 0x000051F6
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000061FE File Offset: 0x000051FE
		[Obsolete("Use the Count property instead")]
		public int Size
		{
			get
			{
				return this.entries_.Length;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00006208 File Offset: 0x00005208
		public long Count
		{
			get
			{
				return (long)this.entries_.Length;
			}
		}

		// Token: 0x17000053 RID: 83
		[IndexerName("EntryByIndex")]
		public ZipEntry this[int index]
		{
			get
			{
				return (ZipEntry)this.entries_[index].Clone();
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006227 File Offset: 0x00005227
		public IEnumerator GetEnumerator()
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			return new ZipFile.ZipEntryEnumerator(this.entries_);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006248 File Offset: 0x00005248
		public int FindEntry(string name, bool ignoreCase)
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			for (int i = 0; i < this.entries_.Length; i++)
			{
				if (string.Compare(name, this.entries_[i].Name, ignoreCase, CultureInfo.InvariantCulture) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000629C File Offset: 0x0000529C
		public ZipEntry GetEntry(string name)
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			int num = this.FindEntry(name, true);
			if (num < 0)
			{
				return null;
			}
			return (ZipEntry)this.entries_[num].Clone();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000062E0 File Offset: 0x000052E0
		public Stream GetInputStream(ZipEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			long num = entry.ZipFileIndex;
			if (num < 0L || num >= (long)this.entries_.Length || this.entries_[(int)(checked((IntPtr)num))].Name != entry.Name)
			{
				num = (long)this.FindEntry(entry.Name, true);
				if (num < 0L)
				{
					throw new ZipException("Entry cannot be found");
				}
			}
			return this.GetInputStream(num);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006368 File Offset: 0x00005368
		public Stream GetInputStream(long entryIndex)
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			checked
			{
				long start = this.LocateEntry(this.entries_[(int)((IntPtr)entryIndex)]);
				CompressionMethod compressionMethod = this.entries_[(int)((IntPtr)entryIndex)].CompressionMethod;
				Stream stream = new ZipFile.PartialInputStream(this, start, this.entries_[(int)((IntPtr)entryIndex)].CompressedSize);
				if (this.entries_[(int)((IntPtr)entryIndex)].IsCrypted)
				{
					stream = this.CreateAndInitDecryptionStream(stream, this.entries_[(int)((IntPtr)entryIndex)]);
					if (stream == null)
					{
						throw new ZipException("Unable to decrypt this entry");
					}
				}
				CompressionMethod compressionMethod2 = compressionMethod;
				if (compressionMethod2 != CompressionMethod.Stored)
				{
					if (compressionMethod2 != CompressionMethod.Deflated)
					{
						throw new ZipException("Unsupported compression method " + compressionMethod);
					}
					stream = new InflaterInputStream(stream, new Inflater(true));
				}
				return stream;
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000641C File Offset: 0x0000541C
		public bool TestArchive(bool testData)
		{
			return this.TestArchive(testData, TestStrategy.FindFirstError, null);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006428 File Offset: 0x00005428
		public bool TestArchive(bool testData, TestStrategy strategy, ZipTestResultHandler resultHandler)
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			TestStatus testStatus = new TestStatus(this);
			if (resultHandler != null)
			{
				resultHandler(testStatus, null);
			}
			ZipFile.HeaderTest tests = testData ? (ZipFile.HeaderTest.Extract | ZipFile.HeaderTest.Header) : ZipFile.HeaderTest.Header;
			bool flag = true;
			try
			{
				int num = 0;
				while (flag && (long)num < this.Count)
				{
					if (resultHandler != null)
					{
						testStatus.SetEntry(this[num]);
						testStatus.SetOperation(TestOperation.EntryHeader);
						resultHandler(testStatus, null);
					}
					try
					{
						this.TestLocalHeader(this[num], tests);
					}
					catch (ZipException ex)
					{
						testStatus.AddError();
						if (resultHandler != null)
						{
							resultHandler(testStatus, string.Format("Exception during test - '{0}'", ex.Message));
						}
						if (strategy == TestStrategy.FindFirstError)
						{
							flag = false;
						}
					}
					if (flag && testData && this[num].IsFile)
					{
						if (resultHandler != null)
						{
							testStatus.SetOperation(TestOperation.EntryData);
							resultHandler(testStatus, null);
						}
						Crc32 crc = new Crc32();
						using (Stream inputStream = this.GetInputStream(this[num]))
						{
							byte[] array = new byte[4096];
							long num2 = 0L;
							int num3;
							while ((num3 = inputStream.Read(array, 0, array.Length)) > 0)
							{
								crc.Update(array, 0, num3);
								if (resultHandler != null)
								{
									num2 += (long)num3;
									testStatus.SetBytesTested(num2);
									resultHandler(testStatus, null);
								}
							}
						}
						if (this[num].Crc != crc.Value)
						{
							testStatus.AddError();
							if (resultHandler != null)
							{
								resultHandler(testStatus, "CRC mismatch");
							}
							if (strategy == TestStrategy.FindFirstError)
							{
								flag = false;
							}
						}
						if ((this[num].Flags & 8) != 0)
						{
							ZipHelperStream zipHelperStream = new ZipHelperStream(this.baseStream_);
							DescriptorData descriptorData = new DescriptorData();
							zipHelperStream.ReadDataDescriptor(this[num].LocalHeaderRequiresZip64, descriptorData);
							if (this[num].Crc != descriptorData.Crc)
							{
								testStatus.AddError();
							}
							if (this[num].CompressedSize != descriptorData.CompressedSize)
							{
								testStatus.AddError();
							}
							if (this[num].Size != descriptorData.Size)
							{
								testStatus.AddError();
							}
						}
					}
					if (resultHandler != null)
					{
						testStatus.SetOperation(TestOperation.EntryComplete);
						resultHandler(testStatus, null);
					}
					num++;
				}
				if (resultHandler != null)
				{
					testStatus.SetOperation(TestOperation.MiscellaneousTests);
					resultHandler(testStatus, null);
				}
			}
			catch (Exception ex2)
			{
				testStatus.AddError();
				if (resultHandler != null)
				{
					resultHandler(testStatus, string.Format("Exception during test - '{0}'", ex2.Message));
				}
			}
			if (resultHandler != null)
			{
				testStatus.SetOperation(TestOperation.Complete);
				testStatus.SetEntry(null);
				resultHandler(testStatus, null);
			}
			return testStatus.ErrorCount == 0;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000066EC File Offset: 0x000056EC
		private long TestLocalHeader(ZipEntry entry, ZipFile.HeaderTest tests)
		{
			long result;
			lock (this.baseStream_)
			{
				bool flag = (tests & ZipFile.HeaderTest.Header) != (ZipFile.HeaderTest)0;
				bool flag2 = (tests & ZipFile.HeaderTest.Extract) != (ZipFile.HeaderTest)0;
				this.baseStream_.Seek(this.offsetOfFirstEntry + entry.Offset, SeekOrigin.Begin);
				if (this.ReadLEUint() != 67324752U)
				{
					throw new ZipException(string.Format("Wrong local header signature @{0:X}", this.offsetOfFirstEntry + entry.Offset));
				}
				short num = (short)(this.ReadLEUshort() & 255);
				short num2 = (short)this.ReadLEUshort();
				short num3 = (short)this.ReadLEUshort();
				short num4 = (short)this.ReadLEUshort();
				short num5 = (short)this.ReadLEUshort();
				uint num6 = this.ReadLEUint();
				long num7 = (long)((ulong)this.ReadLEUint());
				long num8 = (long)((ulong)this.ReadLEUint());
				int num9 = (int)this.ReadLEUshort();
				int num10 = (int)this.ReadLEUshort();
				byte[] array = new byte[num9];
				StreamUtils.ReadFully(this.baseStream_, array);
				byte[] array2 = new byte[num10];
				StreamUtils.ReadFully(this.baseStream_, array2);
				ZipExtraData zipExtraData = new ZipExtraData(array2);
				if (zipExtraData.Find(1))
				{
					num8 = zipExtraData.ReadLong();
					num7 = zipExtraData.ReadLong();
					if ((num2 & 8) != 0)
					{
						if (num8 != -1L && num8 != entry.Size)
						{
							throw new ZipException("Size invalid for descriptor");
						}
						if (num7 != -1L && num7 != entry.CompressedSize)
						{
							throw new ZipException("Compressed size invalid for descriptor");
						}
					}
				}
				else if (num >= 45 && ((uint)num8 == 4294967295U || (uint)num7 == 4294967295U))
				{
					throw new ZipException("Required Zip64 extended information missing");
				}
				if (flag2 && entry.IsFile)
				{
					if (!entry.IsCompressionMethodSupported())
					{
						throw new ZipException("Compression method not supported");
					}
					if (num > 51 || (num > 20 && num < 45))
					{
						throw new ZipException(string.Format("Version required to extract this entry not supported ({0})", num));
					}
					if ((num2 & 12384) != 0)
					{
						throw new ZipException("The library does not support the zip version required to extract this entry");
					}
				}
				if (flag)
				{
					if (num <= 63 && num != 10 && num != 11 && num != 20 && num != 21 && num != 25 && num != 27 && num != 45 && num != 46 && num != 50 && num != 51 && num != 52 && num != 61 && num != 62 && num != 63)
					{
						throw new ZipException(string.Format("Version required to extract this entry is invalid ({0})", num));
					}
					if (((int)num2 & 49168) != 0)
					{
						throw new ZipException("Reserved bit flags cannot be set.");
					}
					if ((num2 & 1) != 0 && num < 20)
					{
						throw new ZipException(string.Format("Version required to extract this entry is too low for encryption ({0})", num));
					}
					if ((num2 & 64) != 0)
					{
						if ((num2 & 1) == 0)
						{
							throw new ZipException("Strong encryption flag set but encryption flag is not set");
						}
						if (num < 50)
						{
							throw new ZipException(string.Format("Version required to extract this entry is too low for encryption ({0})", num));
						}
					}
					if ((num2 & 32) != 0 && num < 27)
					{
						throw new ZipException(string.Format("Patched data requires higher version than ({0})", num));
					}
					if ((int)num2 != entry.Flags)
					{
						throw new ZipException("Central header/local header flags mismatch");
					}
					if (entry.CompressionMethod != (CompressionMethod)num3)
					{
						throw new ZipException("Central header/local header compression method mismatch");
					}
					if (entry.Version != (int)num)
					{
						throw new ZipException("Extract version mismatch");
					}
					if ((num2 & 64) != 0 && num < 62)
					{
						throw new ZipException("Strong encryption flag set but version not high enough");
					}
					if ((num2 & 8192) != 0 && (num4 != 0 || num5 != 0))
					{
						throw new ZipException("Header masked set but date/time values non-zero");
					}
					if ((num2 & 8) == 0 && num6 != (uint)entry.Crc)
					{
						throw new ZipException("Central header/local header crc mismatch");
					}
					if (num8 == 0L && num7 == 0L && num6 != 0U)
					{
						throw new ZipException("Invalid CRC for empty entry");
					}
					if (entry.Name.Length > num9)
					{
						throw new ZipException("File name length mismatch");
					}
					string text = ZipConstants.ConvertToStringExt((int)num2, array);
					if (text != entry.Name)
					{
						throw new ZipException("Central header and local header file name mismatch");
					}
					if (entry.IsDirectory)
					{
						if (num8 > 0L)
						{
							throw new ZipException("Directory cannot have size");
						}
						if (entry.IsCrypted)
						{
							if (num7 > 14L)
							{
								throw new ZipException("Directory compressed size invalid");
							}
						}
						else if (num7 > 2L)
						{
							throw new ZipException("Directory compressed size invalid");
						}
					}
					if (!ZipNameTransform.IsValidName(text, true))
					{
						throw new ZipException("Name is invalid");
					}
				}
				if ((num2 & 8) == 0 || ((num8 > 0L || num7 > 0L) && entry.Size > 0L))
				{
					if (num8 != 0L && num8 != entry.Size)
					{
						throw new ZipException(string.Format("Size mismatch between central header({0}) and local header({1})", entry.Size, num8));
					}
					if (num7 != 0L && num7 != entry.CompressedSize && num7 != (long)((ulong)-1) && num7 != -1L)
					{
						throw new ZipException(string.Format("Compressed size mismatch between central header({0}) and local header({1})", entry.CompressedSize, num7));
					}
				}
				int num11 = num9 + num10;
				result = this.offsetOfFirstEntry + entry.Offset + 30L + (long)num11;
			}
			return result;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00006BB8 File Offset: 0x00005BB8
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00006BC5 File Offset: 0x00005BC5
		public INameTransform NameTransform
		{
			get
			{
				return this.updateEntryFactory_.NameTransform;
			}
			set
			{
				this.updateEntryFactory_.NameTransform = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00006BD3 File Offset: 0x00005BD3
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00006BDB File Offset: 0x00005BDB
		public IEntryFactory EntryFactory
		{
			get
			{
				return this.updateEntryFactory_;
			}
			set
			{
				if (value == null)
				{
					this.updateEntryFactory_ = new ZipEntryFactory();
					return;
				}
				this.updateEntryFactory_ = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00006BF3 File Offset: 0x00005BF3
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00006BFB File Offset: 0x00005BFB
		public int BufferSize
		{
			get
			{
				return this.bufferSize_;
			}
			set
			{
				if (value < 1024)
				{
					throw new ArgumentOutOfRangeException("value", "cannot be below 1024");
				}
				if (this.bufferSize_ != value)
				{
					this.bufferSize_ = value;
					this.copyBuffer_ = null;
				}
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00006C2C File Offset: 0x00005C2C
		public bool IsUpdating
		{
			get
			{
				return this.updates_ != null;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00006C3A File Offset: 0x00005C3A
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00006C42 File Offset: 0x00005C42
		public UseZip64 UseZip64
		{
			get
			{
				return this.useZip64_;
			}
			set
			{
				this.useZip64_ = value;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006C4C File Offset: 0x00005C4C
		public void BeginUpdate(IArchiveStorage archiveStorage, IDynamicDataSource dataSource)
		{
			if (archiveStorage == null)
			{
				throw new ArgumentNullException("archiveStorage");
			}
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			if (this.IsEmbeddedArchive)
			{
				throw new ZipException("Cannot update embedded/SFX archives");
			}
			this.archiveStorage_ = archiveStorage;
			this.updateDataSource_ = dataSource;
			this.updateIndex_ = new Hashtable();
			this.updates_ = new ArrayList(this.entries_.Length);
			foreach (ZipEntry zipEntry in this.entries_)
			{
				int num = this.updates_.Add(new ZipFile.ZipUpdate(zipEntry));
				this.updateIndex_.Add(zipEntry.Name, num);
			}
			this.updates_.Sort(new ZipFile.UpdateComparer());
			int num2 = 0;
			foreach (object obj in this.updates_)
			{
				ZipFile.ZipUpdate zipUpdate = (ZipFile.ZipUpdate)obj;
				if (num2 == this.updates_.Count - 1)
				{
					break;
				}
				zipUpdate.OffsetBasedSize = ((ZipFile.ZipUpdate)this.updates_[num2 + 1]).Entry.Offset - zipUpdate.Entry.Offset;
				num2++;
			}
			this.updateCount_ = (long)this.updates_.Count;
			this.contentsEdited_ = false;
			this.commentEdited_ = false;
			this.newComment_ = null;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006DDC File Offset: 0x00005DDC
		public void BeginUpdate(IArchiveStorage archiveStorage)
		{
			this.BeginUpdate(archiveStorage, new DynamicDiskDataSource());
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006DEA File Offset: 0x00005DEA
		public void BeginUpdate()
		{
			if (this.Name == null)
			{
				this.BeginUpdate(new MemoryArchiveStorage(), new DynamicDiskDataSource());
				return;
			}
			this.BeginUpdate(new DiskArchiveStorage(this), new DynamicDiskDataSource());
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006E18 File Offset: 0x00005E18
		public void CommitUpdate()
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			this.CheckUpdating();
			try
			{
				this.updateIndex_.Clear();
				this.updateIndex_ = null;
				if (this.contentsEdited_)
				{
					this.RunUpdates();
				}
				else if (this.commentEdited_)
				{
					this.UpdateCommentOnly();
				}
				else if (this.entries_.Length == 0)
				{
					byte[] comment = (this.newComment_ != null) ? this.newComment_.RawComment : ZipConstants.ConvertToArray(this.comment_);
					using (ZipHelperStream zipHelperStream = new ZipHelperStream(this.baseStream_))
					{
						zipHelperStream.WriteEndOfCentralDirectory(0L, 0L, 0L, comment);
					}
				}
			}
			finally
			{
				this.PostUpdateCleanup();
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006EE8 File Offset: 0x00005EE8
		public void AbortUpdate()
		{
			this.PostUpdateCleanup();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006EF0 File Offset: 0x00005EF0
		public void SetComment(string comment)
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			this.CheckUpdating();
			this.newComment_ = new ZipFile.ZipString(comment);
			if (this.newComment_.RawLength > 65535)
			{
				this.newComment_ = null;
				throw new ZipException("Comment length exceeds maximum - 65535");
			}
			this.commentEdited_ = true;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006F50 File Offset: 0x00005F50
		private void AddUpdate(ZipFile.ZipUpdate update)
		{
			this.contentsEdited_ = true;
			int num = this.FindExistingUpdate(update.Entry.Name);
			if (num >= 0)
			{
				if (this.updates_[num] == null)
				{
					this.updateCount_ += 1L;
				}
				this.updates_[num] = update;
				return;
			}
			num = this.updates_.Add(update);
			this.updateCount_ += 1L;
			this.updateIndex_.Add(update.Entry.Name, num);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006FE0 File Offset: 0x00005FE0
		public void Add(string fileName, CompressionMethod compressionMethod, bool useUnicodeText)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			if (!ZipEntry.IsCompressionMethodSupported(compressionMethod))
			{
				throw new ArgumentOutOfRangeException("compressionMethod");
			}
			this.CheckUpdating();
			this.contentsEdited_ = true;
			ZipEntry zipEntry = this.EntryFactory.MakeFileEntry(fileName);
			zipEntry.IsUnicodeText = useUnicodeText;
			zipEntry.CompressionMethod = compressionMethod;
			this.AddUpdate(new ZipFile.ZipUpdate(fileName, zipEntry));
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00007058 File Offset: 0x00006058
		public void Add(string fileName, CompressionMethod compressionMethod)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (!ZipEntry.IsCompressionMethodSupported(compressionMethod))
			{
				throw new ArgumentOutOfRangeException("compressionMethod");
			}
			this.CheckUpdating();
			this.contentsEdited_ = true;
			ZipEntry zipEntry = this.EntryFactory.MakeFileEntry(fileName);
			zipEntry.CompressionMethod = compressionMethod;
			this.AddUpdate(new ZipFile.ZipUpdate(fileName, zipEntry));
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000070B4 File Offset: 0x000060B4
		public void Add(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.CheckUpdating();
			this.AddUpdate(new ZipFile.ZipUpdate(fileName, this.EntryFactory.MakeFileEntry(fileName)));
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000070E2 File Offset: 0x000060E2
		public void Add(string fileName, string entryName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			this.CheckUpdating();
			this.AddUpdate(new ZipFile.ZipUpdate(fileName, this.EntryFactory.MakeFileEntry(fileName, entryName, true)));
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00007120 File Offset: 0x00006120
		public void Add(IStaticDataSource dataSource, string entryName)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			this.CheckUpdating();
			this.AddUpdate(new ZipFile.ZipUpdate(dataSource, this.EntryFactory.MakeFileEntry(entryName, false)));
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00007160 File Offset: 0x00006160
		public void Add(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			this.CheckUpdating();
			ZipEntry zipEntry = this.EntryFactory.MakeFileEntry(entryName, false);
			zipEntry.CompressionMethod = compressionMethod;
			this.AddUpdate(new ZipFile.ZipUpdate(dataSource, zipEntry));
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000071B4 File Offset: 0x000061B4
		public void Add(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod, bool useUnicodeText)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			this.CheckUpdating();
			ZipEntry zipEntry = this.EntryFactory.MakeFileEntry(entryName, false);
			zipEntry.IsUnicodeText = useUnicodeText;
			zipEntry.CompressionMethod = compressionMethod;
			this.AddUpdate(new ZipFile.ZipUpdate(dataSource, zipEntry));
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00007210 File Offset: 0x00006210
		public void Add(ZipEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			this.CheckUpdating();
			if (entry.Size != 0L || entry.CompressedSize != 0L)
			{
				throw new ZipException("Entry cannot have any data");
			}
			this.AddUpdate(new ZipFile.ZipUpdate(ZipFile.UpdateCommand.Add, entry));
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007260 File Offset: 0x00006260
		public void AddDirectory(string directoryName)
		{
			if (directoryName == null)
			{
				throw new ArgumentNullException("directoryName");
			}
			this.CheckUpdating();
			ZipEntry entry = this.EntryFactory.MakeDirectoryEntry(directoryName);
			this.AddUpdate(new ZipFile.ZipUpdate(ZipFile.UpdateCommand.Add, entry));
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000729C File Offset: 0x0000629C
		public bool Delete(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.CheckUpdating();
			int num = this.FindExistingUpdate(fileName);
			if (num >= 0 && this.updates_[num] != null)
			{
				bool result = true;
				this.contentsEdited_ = true;
				this.updates_[num] = null;
				this.updateCount_ -= 1L;
				return result;
			}
			throw new ZipException("Cannot find entry to delete");
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000730C File Offset: 0x0000630C
		public void Delete(ZipEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			this.CheckUpdating();
			int num = this.FindExistingUpdate(entry);
			if (num >= 0)
			{
				this.contentsEdited_ = true;
				this.updates_[num] = null;
				this.updateCount_ -= 1L;
				return;
			}
			throw new ZipException("Cannot find entry to delete");
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007367 File Offset: 0x00006367
		private void WriteLEShort(int value)
		{
			this.baseStream_.WriteByte((byte)(value & 255));
			this.baseStream_.WriteByte((byte)(value >> 8 & 255));
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007391 File Offset: 0x00006391
		private void WriteLEUshort(ushort value)
		{
			this.baseStream_.WriteByte((byte)(value & 255));
			this.baseStream_.WriteByte((byte)(value >> 8));
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000073B5 File Offset: 0x000063B5
		private void WriteLEInt(int value)
		{
			this.WriteLEShort(value & 65535);
			this.WriteLEShort(value >> 16);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000073CE File Offset: 0x000063CE
		private void WriteLEUint(uint value)
		{
			this.WriteLEUshort((ushort)(value & 65535U));
			this.WriteLEUshort((ushort)(value >> 16));
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000073E9 File Offset: 0x000063E9
		private void WriteLeLong(long value)
		{
			this.WriteLEInt((int)(value & (long)((ulong)-1)));
			this.WriteLEInt((int)(value >> 32));
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007401 File Offset: 0x00006401
		private void WriteLEUlong(ulong value)
		{
			this.WriteLEUint((uint)(value & (ulong)-1));
			this.WriteLEUint((uint)(value >> 32));
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000741C File Offset: 0x0000641C
		private void WriteLocalEntryHeader(ZipFile.ZipUpdate update)
		{
			ZipEntry outEntry = update.OutEntry;
			outEntry.Offset = this.baseStream_.Position;
			if (update.Command != ZipFile.UpdateCommand.Copy)
			{
				if (outEntry.CompressionMethod == CompressionMethod.Deflated)
				{
					if (outEntry.Size == 0L)
					{
						outEntry.CompressedSize = outEntry.Size;
						outEntry.Crc = 0L;
						outEntry.CompressionMethod = CompressionMethod.Stored;
					}
				}
				else if (outEntry.CompressionMethod == CompressionMethod.Stored)
				{
					outEntry.Flags &= -9;
				}
				if (this.HaveKeys)
				{
					outEntry.IsCrypted = true;
					if (outEntry.Crc < 0L)
					{
						outEntry.Flags |= 8;
					}
				}
				else
				{
					outEntry.IsCrypted = false;
				}
				switch (this.useZip64_)
				{
				case UseZip64.On:
					outEntry.ForceZip64();
					break;
				case UseZip64.Dynamic:
					if (outEntry.Size < 0L)
					{
						outEntry.ForceZip64();
					}
					break;
				}
			}
			this.WriteLEInt(67324752);
			this.WriteLEShort(outEntry.Version);
			this.WriteLEShort(outEntry.Flags);
			this.WriteLEShort((int)((byte)outEntry.CompressionMethod));
			this.WriteLEInt((int)outEntry.DosTime);
			if (!outEntry.HasCrc)
			{
				update.CrcPatchOffset = this.baseStream_.Position;
				this.WriteLEInt(0);
			}
			else
			{
				this.WriteLEInt((int)outEntry.Crc);
			}
			if (outEntry.LocalHeaderRequiresZip64)
			{
				this.WriteLEInt(-1);
				this.WriteLEInt(-1);
			}
			else
			{
				if (outEntry.CompressedSize < 0L || outEntry.Size < 0L)
				{
					update.SizePatchOffset = this.baseStream_.Position;
				}
				this.WriteLEInt((int)outEntry.CompressedSize);
				this.WriteLEInt((int)outEntry.Size);
			}
			byte[] array = ZipConstants.ConvertToArray(outEntry.Flags, outEntry.Name);
			if (array.Length > 65535)
			{
				throw new ZipException("Entry name too long.");
			}
			ZipExtraData zipExtraData = new ZipExtraData(outEntry.ExtraData);
			if (outEntry.LocalHeaderRequiresZip64)
			{
				zipExtraData.StartNewEntry();
				zipExtraData.AddLeLong(outEntry.Size);
				zipExtraData.AddLeLong(outEntry.CompressedSize);
				zipExtraData.AddNewEntry(1);
			}
			else
			{
				zipExtraData.Delete(1);
			}
			outEntry.ExtraData = zipExtraData.GetEntryData();
			this.WriteLEShort(array.Length);
			this.WriteLEShort(outEntry.ExtraData.Length);
			if (array.Length > 0)
			{
				this.baseStream_.Write(array, 0, array.Length);
			}
			if (outEntry.LocalHeaderRequiresZip64)
			{
				if (!zipExtraData.Find(1))
				{
					throw new ZipException("Internal error cannot find extra data");
				}
				update.SizePatchOffset = this.baseStream_.Position + (long)zipExtraData.CurrentReadIndex;
			}
			if (outEntry.ExtraData.Length > 0)
			{
				this.baseStream_.Write(outEntry.ExtraData, 0, outEntry.ExtraData.Length);
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000076B8 File Offset: 0x000066B8
		private int WriteCentralDirectoryHeader(ZipEntry entry)
		{
			if (entry.CompressedSize < 0L)
			{
				throw new ZipException("Attempt to write central directory entry with unknown csize");
			}
			if (entry.Size < 0L)
			{
				throw new ZipException("Attempt to write central directory entry with unknown size");
			}
			if (entry.Crc < 0L)
			{
				throw new ZipException("Attempt to write central directory entry with unknown crc");
			}
			this.WriteLEInt(33639248);
			this.WriteLEShort(51);
			this.WriteLEShort(entry.Version);
			this.WriteLEShort(entry.Flags);
			this.WriteLEShort((int)((byte)entry.CompressionMethod));
			this.WriteLEInt((int)entry.DosTime);
			this.WriteLEInt((int)entry.Crc);
			if (entry.IsZip64Forced() || entry.CompressedSize >= (long)((ulong)-1))
			{
				this.WriteLEInt(-1);
			}
			else
			{
				this.WriteLEInt((int)(entry.CompressedSize & (long)((ulong)-1)));
			}
			if (entry.IsZip64Forced() || entry.Size >= (long)((ulong)-1))
			{
				this.WriteLEInt(-1);
			}
			else
			{
				this.WriteLEInt((int)entry.Size);
			}
			byte[] array = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
			if (array.Length > 65535)
			{
				throw new ZipException("Entry name is too long.");
			}
			this.WriteLEShort(array.Length);
			ZipExtraData zipExtraData = new ZipExtraData(entry.ExtraData);
			if (entry.CentralHeaderRequiresZip64)
			{
				zipExtraData.StartNewEntry();
				if (entry.Size >= (long)((ulong)-1) || this.useZip64_ == UseZip64.On)
				{
					zipExtraData.AddLeLong(entry.Size);
				}
				if (entry.CompressedSize >= (long)((ulong)-1) || this.useZip64_ == UseZip64.On)
				{
					zipExtraData.AddLeLong(entry.CompressedSize);
				}
				if (entry.Offset >= (long)((ulong)-1))
				{
					zipExtraData.AddLeLong(entry.Offset);
				}
				zipExtraData.AddNewEntry(1);
			}
			else
			{
				zipExtraData.Delete(1);
			}
			byte[] entryData = zipExtraData.GetEntryData();
			this.WriteLEShort(entryData.Length);
			this.WriteLEShort((entry.Comment != null) ? entry.Comment.Length : 0);
			this.WriteLEShort(0);
			this.WriteLEShort(0);
			if (entry.ExternalFileAttributes != -1)
			{
				this.WriteLEInt(entry.ExternalFileAttributes);
			}
			else if (entry.IsDirectory)
			{
				this.WriteLEUint(16U);
			}
			else
			{
				this.WriteLEUint(0U);
			}
			if (entry.Offset >= (long)((ulong)-1))
			{
				this.WriteLEUint(uint.MaxValue);
			}
			else
			{
				this.WriteLEUint((uint)((int)entry.Offset));
			}
			if (array.Length > 0)
			{
				this.baseStream_.Write(array, 0, array.Length);
			}
			if (entryData.Length > 0)
			{
				this.baseStream_.Write(entryData, 0, entryData.Length);
			}
			byte[] array2 = (entry.Comment != null) ? Encoding.ASCII.GetBytes(entry.Comment) : new byte[0];
			if (array2.Length > 0)
			{
				this.baseStream_.Write(array2, 0, array2.Length);
			}
			return 46 + array.Length + entryData.Length + array2.Length;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007953 File Offset: 0x00006953
		private void PostUpdateCleanup()
		{
			this.updateDataSource_ = null;
			this.updates_ = null;
			this.updateIndex_ = null;
			if (this.archiveStorage_ != null)
			{
				this.archiveStorage_.Dispose();
				this.archiveStorage_ = null;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007984 File Offset: 0x00006984
		private string GetTransformedFileName(string name)
		{
			INameTransform nameTransform = this.NameTransform;
			if (nameTransform == null)
			{
				return name;
			}
			return nameTransform.TransformFile(name);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000079A4 File Offset: 0x000069A4
		private string GetTransformedDirectoryName(string name)
		{
			INameTransform nameTransform = this.NameTransform;
			if (nameTransform == null)
			{
				return name;
			}
			return nameTransform.TransformDirectory(name);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000079C4 File Offset: 0x000069C4
		private byte[] GetBuffer()
		{
			if (this.copyBuffer_ == null)
			{
				this.copyBuffer_ = new byte[this.bufferSize_];
			}
			return this.copyBuffer_;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000079E8 File Offset: 0x000069E8
		private void CopyDescriptorBytes(ZipFile.ZipUpdate update, Stream dest, Stream source)
		{
			int i = this.GetDescriptorSize(update);
			if (i > 0)
			{
				byte[] buffer = this.GetBuffer();
				while (i > 0)
				{
					int count = Math.Min(buffer.Length, i);
					int num = source.Read(buffer, 0, count);
					if (num <= 0)
					{
						throw new ZipException("Unxpected end of stream");
					}
					dest.Write(buffer, 0, num);
					i -= num;
				}
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00007A40 File Offset: 0x00006A40
		private void CopyBytes(ZipFile.ZipUpdate update, Stream destination, Stream source, long bytesToCopy, bool updateCrc)
		{
			if (destination == source)
			{
				throw new InvalidOperationException("Destination and source are the same");
			}
			Crc32 crc = new Crc32();
			byte[] buffer = this.GetBuffer();
			long num = bytesToCopy;
			long num2 = 0L;
			int num4;
			do
			{
				int num3 = buffer.Length;
				if (bytesToCopy < (long)num3)
				{
					num3 = (int)bytesToCopy;
				}
				num4 = source.Read(buffer, 0, num3);
				if (num4 > 0)
				{
					if (updateCrc)
					{
						crc.Update(buffer, 0, num4);
					}
					destination.Write(buffer, 0, num4);
					bytesToCopy -= (long)num4;
					num2 += (long)num4;
				}
			}
			while (num4 > 0 && bytesToCopy > 0L);
			if (num2 != num)
			{
				throw new ZipException(string.Format("Failed to copy bytes expected {0} read {1}", num, num2));
			}
			if (updateCrc)
			{
				update.OutEntry.Crc = crc.Value;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00007AF8 File Offset: 0x00006AF8
		private int GetDescriptorSize(ZipFile.ZipUpdate update)
		{
			int result = 0;
			if ((update.Entry.Flags & 8) != 0)
			{
				result = 12;
				if (update.Entry.LocalHeaderRequiresZip64)
				{
					result = 20;
				}
			}
			return result;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007B2C File Offset: 0x00006B2C
		private void CopyDescriptorBytesDirect(ZipFile.ZipUpdate update, Stream stream, ref long destinationPosition, long sourcePosition)
		{
			int i = this.GetDescriptorSize(update);
			while (i > 0)
			{
				int count = i;
				byte[] buffer = this.GetBuffer();
				stream.Position = sourcePosition;
				int num = stream.Read(buffer, 0, count);
				if (num <= 0)
				{
					throw new ZipException("Unxpected end of stream");
				}
				stream.Position = destinationPosition;
				stream.Write(buffer, 0, num);
				i -= num;
				destinationPosition += (long)num;
				sourcePosition += (long)num;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007B98 File Offset: 0x00006B98
		private void CopyEntryDataDirect(ZipFile.ZipUpdate update, Stream stream, bool updateCrc, ref long destinationPosition, ref long sourcePosition)
		{
			long num = update.Entry.CompressedSize;
			Crc32 crc = new Crc32();
			byte[] buffer = this.GetBuffer();
			long num2 = num;
			long num3 = 0L;
			int num5;
			do
			{
				int num4 = buffer.Length;
				if (num < (long)num4)
				{
					num4 = (int)num;
				}
				stream.Position = sourcePosition;
				num5 = stream.Read(buffer, 0, num4);
				if (num5 > 0)
				{
					if (updateCrc)
					{
						crc.Update(buffer, 0, num5);
					}
					stream.Position = destinationPosition;
					stream.Write(buffer, 0, num5);
					destinationPosition += (long)num5;
					sourcePosition += (long)num5;
					num -= (long)num5;
					num3 += (long)num5;
				}
			}
			while (num5 > 0 && num > 0L);
			if (num3 != num2)
			{
				throw new ZipException(string.Format("Failed to copy bytes expected {0} read {1}", num2, num3));
			}
			if (updateCrc)
			{
				update.OutEntry.Crc = crc.Value;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007C70 File Offset: 0x00006C70
		private int FindExistingUpdate(ZipEntry entry)
		{
			int result = -1;
			string transformedFileName = this.GetTransformedFileName(entry.Name);
			if (this.updateIndex_.ContainsKey(transformedFileName))
			{
				result = (int)this.updateIndex_[transformedFileName];
			}
			return result;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007CB0 File Offset: 0x00006CB0
		private int FindExistingUpdate(string fileName)
		{
			int result = -1;
			string transformedFileName = this.GetTransformedFileName(fileName);
			if (this.updateIndex_.ContainsKey(transformedFileName))
			{
				result = (int)this.updateIndex_[transformedFileName];
			}
			return result;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007CE8 File Offset: 0x00006CE8
		private Stream GetOutputStream(ZipEntry entry)
		{
			Stream stream = this.baseStream_;
			if (entry.IsCrypted)
			{
				stream = this.CreateAndInitEncryptionStream(stream, entry);
			}
			CompressionMethod compressionMethod = entry.CompressionMethod;
			if (compressionMethod != CompressionMethod.Stored)
			{
				if (compressionMethod != CompressionMethod.Deflated)
				{
					throw new ZipException("Unknown compression method " + entry.CompressionMethod);
				}
				stream = new DeflaterOutputStream(stream, new Deflater(9, true))
				{
					IsStreamOwner = false
				};
			}
			else
			{
				stream = new ZipFile.UncompressedStream(stream);
			}
			return stream;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007D60 File Offset: 0x00006D60
		private void AddEntry(ZipFile workFile, ZipFile.ZipUpdate update)
		{
			Stream stream = null;
			if (update.Entry.IsFile)
			{
				stream = update.GetSource();
				if (stream == null)
				{
					stream = this.updateDataSource_.GetSource(update.Entry, update.Filename);
				}
			}
			if (stream != null)
			{
				using (stream)
				{
					long length = stream.Length;
					if (update.OutEntry.Size < 0L)
					{
						update.OutEntry.Size = length;
					}
					else if (update.OutEntry.Size != length)
					{
						throw new ZipException("Entry size/stream size mismatch");
					}
					workFile.WriteLocalEntryHeader(update);
					long position = workFile.baseStream_.Position;
					using (Stream outputStream = workFile.GetOutputStream(update.OutEntry))
					{
						this.CopyBytes(update, outputStream, stream, length, true);
					}
					long position2 = workFile.baseStream_.Position;
					update.OutEntry.CompressedSize = position2 - position;
					if ((update.OutEntry.Flags & 8) == 8)
					{
						ZipHelperStream zipHelperStream = new ZipHelperStream(workFile.baseStream_);
						zipHelperStream.WriteDataDescriptor(update.OutEntry);
					}
					return;
				}
			}
			workFile.WriteLocalEntryHeader(update);
			update.OutEntry.CompressedSize = 0L;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007EA0 File Offset: 0x00006EA0
		private void ModifyEntry(ZipFile workFile, ZipFile.ZipUpdate update)
		{
			workFile.WriteLocalEntryHeader(update);
			long position = workFile.baseStream_.Position;
			if (update.Entry.IsFile && update.Filename != null)
			{
				using (Stream outputStream = workFile.GetOutputStream(update.OutEntry))
				{
					using (Stream inputStream = this.GetInputStream(update.Entry))
					{
						this.CopyBytes(update, outputStream, inputStream, inputStream.Length, true);
					}
				}
			}
			long position2 = workFile.baseStream_.Position;
			update.Entry.CompressedSize = position2 - position;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00007F50 File Offset: 0x00006F50
		private void CopyEntryDirect(ZipFile workFile, ZipFile.ZipUpdate update, ref long destinationPosition)
		{
			bool flag = false;
			if (update.Entry.Offset == destinationPosition)
			{
				flag = true;
			}
			if (!flag)
			{
				this.baseStream_.Position = destinationPosition;
				workFile.WriteLocalEntryHeader(update);
				destinationPosition = this.baseStream_.Position;
			}
			long num = 0L;
			long num2 = update.Entry.Offset + 26L;
			this.baseStream_.Seek(num2, SeekOrigin.Begin);
			uint num3 = (uint)this.ReadLEUshort();
			uint num4 = (uint)this.ReadLEUshort();
			num = this.baseStream_.Position + (long)((ulong)num3) + (long)((ulong)num4);
			if (!flag)
			{
				if (update.Entry.CompressedSize > 0L)
				{
					this.CopyEntryDataDirect(update, this.baseStream_, false, ref destinationPosition, ref num);
				}
				this.CopyDescriptorBytesDirect(update, this.baseStream_, ref destinationPosition, num);
				return;
			}
			if (update.OffsetBasedSize != -1L)
			{
				destinationPosition += update.OffsetBasedSize;
				return;
			}
			destinationPosition += num - num2 + 26L + update.Entry.CompressedSize + (long)this.GetDescriptorSize(update);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00008044 File Offset: 0x00007044
		private void CopyEntry(ZipFile workFile, ZipFile.ZipUpdate update)
		{
			workFile.WriteLocalEntryHeader(update);
			if (update.Entry.CompressedSize > 0L)
			{
				long offset = update.Entry.Offset + 26L;
				this.baseStream_.Seek(offset, SeekOrigin.Begin);
				uint num = (uint)this.ReadLEUshort();
				uint num2 = (uint)this.ReadLEUshort();
				this.baseStream_.Seek((long)((ulong)(num + num2)), SeekOrigin.Current);
				this.CopyBytes(update, workFile.baseStream_, this.baseStream_, update.Entry.CompressedSize, false);
			}
			this.CopyDescriptorBytes(update, workFile.baseStream_, this.baseStream_);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000080D6 File Offset: 0x000070D6
		private void Reopen(Stream source)
		{
			if (source == null)
			{
				throw new ZipException("Failed to reopen archive - no source");
			}
			this.isNewArchive_ = false;
			this.baseStream_ = source;
			this.ReadEntries();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000080FA File Offset: 0x000070FA
		private void Reopen()
		{
			if (this.Name == null)
			{
				throw new InvalidOperationException("Name is not known cannot Reopen");
			}
			this.Reopen(File.Open(this.Name, FileMode.Open, FileAccess.Read, FileShare.Read));
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00008124 File Offset: 0x00007124
		private void UpdateCommentOnly()
		{
			long length = this.baseStream_.Length;
			ZipHelperStream zipHelperStream;
			if (this.archiveStorage_.UpdateMode == FileUpdateMode.Safe)
			{
				Stream stream = this.archiveStorage_.MakeTemporaryCopy(this.baseStream_);
				zipHelperStream = new ZipHelperStream(stream);
				zipHelperStream.IsStreamOwner = true;
				this.baseStream_.Close();
				this.baseStream_ = null;
			}
			else if (this.archiveStorage_.UpdateMode == FileUpdateMode.Direct)
			{
				this.baseStream_ = this.archiveStorage_.OpenForDirectUpdate(this.baseStream_);
				zipHelperStream = new ZipHelperStream(this.baseStream_);
			}
			else
			{
				this.baseStream_.Close();
				this.baseStream_ = null;
				zipHelperStream = new ZipHelperStream(this.Name);
			}
			using (zipHelperStream)
			{
				long num = zipHelperStream.LocateBlockWithSignature(101010256, length, 22, 65535);
				if (num < 0L)
				{
					throw new ZipException("Cannot find central directory");
				}
				zipHelperStream.Position += 16L;
				byte[] rawComment = this.newComment_.RawComment;
				zipHelperStream.WriteLEShort(rawComment.Length);
				zipHelperStream.Write(rawComment, 0, rawComment.Length);
				zipHelperStream.SetLength(zipHelperStream.Position);
			}
			if (this.archiveStorage_.UpdateMode == FileUpdateMode.Safe)
			{
				this.Reopen(this.archiveStorage_.ConvertTemporaryToFinal());
				return;
			}
			this.ReadEntries();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000827C File Offset: 0x0000727C
		private void RunUpdates()
		{
			long num = 0L;
			long length = 0L;
			bool flag = false;
			long position = 0L;
			ZipFile zipFile;
			if (this.IsNewArchive)
			{
				zipFile = this;
				zipFile.baseStream_.Position = 0L;
				flag = true;
			}
			else if (this.archiveStorage_.UpdateMode == FileUpdateMode.Direct)
			{
				zipFile = this;
				zipFile.baseStream_.Position = 0L;
				flag = true;
				this.updates_.Sort(new ZipFile.UpdateComparer());
			}
			else
			{
				zipFile = ZipFile.Create(this.archiveStorage_.GetTemporaryOutput());
				zipFile.UseZip64 = this.UseZip64;
				if (this.key != null)
				{
					zipFile.key = (byte[])this.key.Clone();
				}
			}
			try
			{
				foreach (object obj in this.updates_)
				{
					ZipFile.ZipUpdate zipUpdate = (ZipFile.ZipUpdate)obj;
					if (zipUpdate != null)
					{
						switch (zipUpdate.Command)
						{
						case ZipFile.UpdateCommand.Copy:
							if (flag)
							{
								this.CopyEntryDirect(zipFile, zipUpdate, ref position);
							}
							else
							{
								this.CopyEntry(zipFile, zipUpdate);
							}
							break;
						case ZipFile.UpdateCommand.Modify:
							this.ModifyEntry(zipFile, zipUpdate);
							break;
						case ZipFile.UpdateCommand.Add:
							if (!this.IsNewArchive && flag)
							{
								zipFile.baseStream_.Position = position;
							}
							this.AddEntry(zipFile, zipUpdate);
							if (flag)
							{
								position = zipFile.baseStream_.Position;
							}
							break;
						}
					}
				}
				if (!this.IsNewArchive && flag)
				{
					zipFile.baseStream_.Position = position;
				}
				long position2 = zipFile.baseStream_.Position;
				foreach (object obj2 in this.updates_)
				{
					ZipFile.ZipUpdate zipUpdate2 = (ZipFile.ZipUpdate)obj2;
					if (zipUpdate2 != null)
					{
						num += (long)zipFile.WriteCentralDirectoryHeader(zipUpdate2.OutEntry);
					}
				}
				byte[] comment = (this.newComment_ != null) ? this.newComment_.RawComment : ZipConstants.ConvertToArray(this.comment_);
				using (ZipHelperStream zipHelperStream = new ZipHelperStream(zipFile.baseStream_))
				{
					zipHelperStream.WriteEndOfCentralDirectory(this.updateCount_, num, position2, comment);
				}
				length = zipFile.baseStream_.Position;
				foreach (object obj3 in this.updates_)
				{
					ZipFile.ZipUpdate zipUpdate3 = (ZipFile.ZipUpdate)obj3;
					if (zipUpdate3 != null)
					{
						if (zipUpdate3.CrcPatchOffset > 0L && zipUpdate3.OutEntry.CompressedSize > 0L)
						{
							zipFile.baseStream_.Position = zipUpdate3.CrcPatchOffset;
							zipFile.WriteLEInt((int)zipUpdate3.OutEntry.Crc);
						}
						if (zipUpdate3.SizePatchOffset > 0L)
						{
							zipFile.baseStream_.Position = zipUpdate3.SizePatchOffset;
							if (zipUpdate3.OutEntry.LocalHeaderRequiresZip64)
							{
								zipFile.WriteLeLong(zipUpdate3.OutEntry.Size);
								zipFile.WriteLeLong(zipUpdate3.OutEntry.CompressedSize);
							}
							else
							{
								zipFile.WriteLEInt((int)zipUpdate3.OutEntry.CompressedSize);
								zipFile.WriteLEInt((int)zipUpdate3.OutEntry.Size);
							}
						}
					}
				}
			}
			catch
			{
				zipFile.Close();
				if (!flag && zipFile.Name != null)
				{
					File.Delete(zipFile.Name);
				}
				throw;
			}
			if (flag)
			{
				zipFile.baseStream_.SetLength(length);
				zipFile.baseStream_.Flush();
				this.isNewArchive_ = false;
				this.ReadEntries();
				return;
			}
			this.baseStream_.Close();
			this.Reopen(this.archiveStorage_.ConvertTemporaryToFinal());
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000086A8 File Offset: 0x000076A8
		private void CheckUpdating()
		{
			if (this.updates_ == null)
			{
				throw new InvalidOperationException("BeginUpdate has not been called");
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000086BD File Offset: 0x000076BD
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000086C8 File Offset: 0x000076C8
		private void DisposeInternal(bool disposing)
		{
			if (!this.isDisposed_)
			{
				this.isDisposed_ = true;
				this.entries_ = new ZipEntry[0];
				if (this.IsStreamOwner && this.baseStream_ != null)
				{
					lock (this.baseStream_)
					{
						this.baseStream_.Close();
					}
				}
				this.PostUpdateCleanup();
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00008738 File Offset: 0x00007738
		protected virtual void Dispose(bool disposing)
		{
			this.DisposeInternal(disposing);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00008744 File Offset: 0x00007744
		private ushort ReadLEUshort()
		{
			int num = this.baseStream_.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException("End of stream");
			}
			int num2 = this.baseStream_.ReadByte();
			if (num2 < 0)
			{
				throw new EndOfStreamException("End of stream");
			}
			return (ushort)num | (ushort)(num2 << 8);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000878F File Offset: 0x0000778F
		private uint ReadLEUint()
		{
			return (uint)((int)this.ReadLEUshort() | (int)this.ReadLEUshort() << 16);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000087A1 File Offset: 0x000077A1
		private ulong ReadLEUlong()
		{
			return (ulong)this.ReadLEUint() | (ulong)this.ReadLEUint() << 32;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000087B8 File Offset: 0x000077B8
		private long LocateBlockWithSignature(int signature, long endLocation, int minimumBlockSize, int maximumVariableData)
		{
			long result;
			using (ZipHelperStream zipHelperStream = new ZipHelperStream(this.baseStream_))
			{
				result = zipHelperStream.LocateBlockWithSignature(signature, endLocation, minimumBlockSize, maximumVariableData);
			}
			return result;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000087FC File Offset: 0x000077FC
		private void ReadEntries()
		{
			if (!this.baseStream_.CanSeek)
			{
				throw new ZipException("ZipFile stream must be seekable");
			}
			long num = this.LocateBlockWithSignature(101010256, this.baseStream_.Length, 22, 65535);
			if (num < 0L)
			{
				throw new ZipException("Cannot find central directory");
			}
			ushort num2 = this.ReadLEUshort();
			ushort num3 = this.ReadLEUshort();
			ulong num4 = (ulong)this.ReadLEUshort();
			ulong num5 = (ulong)this.ReadLEUshort();
			ulong num6 = (ulong)this.ReadLEUint();
			long num7 = (long)((ulong)this.ReadLEUint());
			uint num8 = (uint)this.ReadLEUshort();
			if (num8 > 0U)
			{
				byte[] array = new byte[num8];
				StreamUtils.ReadFully(this.baseStream_, array);
				this.comment_ = ZipConstants.ConvertToString(array);
			}
			else
			{
				this.comment_ = string.Empty;
			}
			bool flag = false;
			if (num2 == 65535 || num3 == 65535 || num4 == 65535UL || num5 == 65535UL || num6 == (ulong)-1 || num7 == (long)((ulong)-1))
			{
				flag = true;
				long num9 = this.LocateBlockWithSignature(117853008, num, 0, 4096);
				if (num9 < 0L)
				{
					throw new ZipException("Cannot find Zip64 locator");
				}
				this.ReadLEUint();
				ulong num10 = this.ReadLEUlong();
				this.ReadLEUint();
				this.baseStream_.Position = (long)num10;
				long num11 = (long)((ulong)this.ReadLEUint());
				if (num11 != 101075792L)
				{
					throw new ZipException(string.Format("Invalid Zip64 Central directory signature at {0:X}", num10));
				}
				this.ReadLEUlong();
				this.ReadLEUshort();
				this.ReadLEUshort();
				this.ReadLEUint();
				this.ReadLEUint();
				num4 = this.ReadLEUlong();
				num5 = this.ReadLEUlong();
				num6 = this.ReadLEUlong();
				num7 = (long)this.ReadLEUlong();
			}
			this.entries_ = new ZipEntry[num4];
			if (!flag && num7 < num - (long)(4UL + num6))
			{
				this.offsetOfFirstEntry = num - (long)(4UL + num6 + (ulong)num7);
				if (this.offsetOfFirstEntry <= 0L)
				{
					throw new ZipException("Invalid embedded zip archive");
				}
			}
			this.baseStream_.Seek(this.offsetOfFirstEntry + num7, SeekOrigin.Begin);
			for (ulong num12 = 0UL; num12 < num4; num12 += 1UL)
			{
				if (this.ReadLEUint() != 33639248U)
				{
					throw new ZipException("Wrong Central Directory signature");
				}
				int madeByInfo = (int)this.ReadLEUshort();
				int versionRequiredToExtract = (int)this.ReadLEUshort();
				int num13 = (int)this.ReadLEUshort();
				int method = (int)this.ReadLEUshort();
				uint num14 = this.ReadLEUint();
				uint num15 = this.ReadLEUint();
				long num16 = (long)((ulong)this.ReadLEUint());
				long num17 = (long)((ulong)this.ReadLEUint());
				int num18 = (int)this.ReadLEUshort();
				int num19 = (int)this.ReadLEUshort();
				int num20 = (int)this.ReadLEUshort();
				this.ReadLEUshort();
				this.ReadLEUshort();
				uint externalFileAttributes = this.ReadLEUint();
				long offset = (long)((ulong)this.ReadLEUint());
				byte[] array2 = new byte[Math.Max(num18, num20)];
				StreamUtils.ReadFully(this.baseStream_, array2, 0, num18);
				string name = ZipConstants.ConvertToStringExt(num13, array2, num18);
				ZipEntry zipEntry = new ZipEntry(name, versionRequiredToExtract, madeByInfo, (CompressionMethod)method);
				zipEntry.Crc = (long)((ulong)num15 & (ulong)-1);
				zipEntry.Size = (num17 & (long)((ulong)-1));
				zipEntry.CompressedSize = (num16 & (long)((ulong)-1));
				zipEntry.Flags = num13;
				zipEntry.DosTime = (long)((ulong)num14);
				zipEntry.ZipFileIndex = (long)num12;
				zipEntry.Offset = offset;
				zipEntry.ExternalFileAttributes = (int)externalFileAttributes;
				if ((num13 & 8) == 0)
				{
					zipEntry.CryptoCheckValue = (byte)(num15 >> 24);
				}
				else
				{
					zipEntry.CryptoCheckValue = (byte)(num14 >> 8 & 255U);
				}
				if (num19 > 0)
				{
					byte[] array3 = new byte[num19];
					StreamUtils.ReadFully(this.baseStream_, array3);
					zipEntry.ExtraData = array3;
				}
				zipEntry.ProcessExtraData(false);
				if (num20 > 0)
				{
					StreamUtils.ReadFully(this.baseStream_, array2, 0, num20);
					zipEntry.Comment = ZipConstants.ConvertToStringExt(num13, array2, num20);
				}
				this.entries_[(int)(checked((IntPtr)num12))] = zipEntry;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00008BBF File Offset: 0x00007BBF
		private long LocateEntry(ZipEntry entry)
		{
			return this.TestLocalHeader(entry, ZipFile.HeaderTest.Extract);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00008BCC File Offset: 0x00007BCC
		private Stream CreateAndInitDecryptionStream(Stream baseStream, ZipEntry entry)
		{
			CryptoStream cryptoStream;
			if (entry.Version < 50 || (entry.Flags & 64) == 0)
			{
				PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
				this.OnKeysRequired(entry.Name);
				if (!this.HaveKeys)
				{
					throw new ZipException("No password available for encrypted stream");
				}
				cryptoStream = new CryptoStream(baseStream, pkzipClassicManaged.CreateDecryptor(this.key, null), CryptoStreamMode.Read);
				ZipFile.CheckClassicPassword(cryptoStream, entry);
			}
			else
			{
				if (entry.Version != 51)
				{
					throw new ZipException("Decryption method not supported");
				}
				this.OnKeysRequired(entry.Name);
				if (!this.HaveKeys)
				{
					throw new ZipException("No password available for AES encrypted stream");
				}
				int aessaltLen = entry.AESSaltLen;
				byte[] array = new byte[aessaltLen];
				int num = baseStream.Read(array, 0, aessaltLen);
				if (num != aessaltLen)
				{
					throw new ZipException(string.Concat(new object[]
					{
						"AES Salt expected ",
						aessaltLen,
						" got ",
						num
					}));
				}
				byte[] array2 = new byte[2];
				baseStream.Read(array2, 0, 2);
				int blockSize = entry.AESKeySize / 8;
				ZipAESTransform zipAESTransform = new ZipAESTransform(this.rawPassword_, array, blockSize, false);
				byte[] pwdVerifier = zipAESTransform.PwdVerifier;
				if (pwdVerifier[0] != array2[0] || pwdVerifier[1] != array2[1])
				{
					throw new ZipException("Invalid password for AES");
				}
				cryptoStream = new ZipAESStream(baseStream, zipAESTransform, CryptoStreamMode.Read);
			}
			return cryptoStream;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00008D2C File Offset: 0x00007D2C
		private Stream CreateAndInitEncryptionStream(Stream baseStream, ZipEntry entry)
		{
			CryptoStream cryptoStream = null;
			if (entry.Version < 50 || (entry.Flags & 64) == 0)
			{
				PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
				this.OnKeysRequired(entry.Name);
				if (!this.HaveKeys)
				{
					throw new ZipException("No password available for encrypted stream");
				}
				cryptoStream = new CryptoStream(new ZipFile.UncompressedStream(baseStream), pkzipClassicManaged.CreateEncryptor(this.key, null), CryptoStreamMode.Write);
				if (entry.Crc < 0L || (entry.Flags & 8) != 0)
				{
					ZipFile.WriteEncryptionHeader(cryptoStream, entry.DosTime << 16);
				}
				else
				{
					ZipFile.WriteEncryptionHeader(cryptoStream, entry.Crc);
				}
			}
			return cryptoStream;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00008DC4 File Offset: 0x00007DC4
		private static void CheckClassicPassword(CryptoStream classicCryptoStream, ZipEntry entry)
		{
			byte[] array = new byte[12];
			StreamUtils.ReadFully(classicCryptoStream, array);
			if (array[11] != entry.CryptoCheckValue)
			{
				throw new ZipException("Invalid password");
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00008DF8 File Offset: 0x00007DF8
		private static void WriteEncryptionHeader(Stream stream, long crcValue)
		{
			byte[] array = new byte[12];
			Random random = new Random();
			random.NextBytes(array);
			array[11] = (byte)(crcValue >> 24);
			stream.Write(array, 0, array.Length);
		}

		// Token: 0x040000D4 RID: 212
		private const int DefaultBufferSize = 4096;

		// Token: 0x040000D5 RID: 213
		public ZipFile.KeysRequiredEventHandler KeysRequired;

		// Token: 0x040000D6 RID: 214
		private bool isDisposed_;

		// Token: 0x040000D7 RID: 215
		private string name_;

		// Token: 0x040000D8 RID: 216
		private string comment_;

		// Token: 0x040000D9 RID: 217
		private string rawPassword_;

		// Token: 0x040000DA RID: 218
		private Stream baseStream_;

		// Token: 0x040000DB RID: 219
		private bool isStreamOwner;

		// Token: 0x040000DC RID: 220
		private long offsetOfFirstEntry;

		// Token: 0x040000DD RID: 221
		private ZipEntry[] entries_;

		// Token: 0x040000DE RID: 222
		private byte[] key;

		// Token: 0x040000DF RID: 223
		private bool isNewArchive_;

		// Token: 0x040000E0 RID: 224
		private UseZip64 useZip64_ = UseZip64.Dynamic;

		// Token: 0x040000E1 RID: 225
		private ArrayList updates_;

		// Token: 0x040000E2 RID: 226
		private long updateCount_;

		// Token: 0x040000E3 RID: 227
		private Hashtable updateIndex_;

		// Token: 0x040000E4 RID: 228
		private IArchiveStorage archiveStorage_;

		// Token: 0x040000E5 RID: 229
		private IDynamicDataSource updateDataSource_;

		// Token: 0x040000E6 RID: 230
		private bool contentsEdited_;

		// Token: 0x040000E7 RID: 231
		private int bufferSize_ = 4096;

		// Token: 0x040000E8 RID: 232
		private byte[] copyBuffer_;

		// Token: 0x040000E9 RID: 233
		private ZipFile.ZipString newComment_;

		// Token: 0x040000EA RID: 234
		private bool commentEdited_;

		// Token: 0x040000EB RID: 235
		private IEntryFactory updateEntryFactory_ = new ZipEntryFactory();

		// Token: 0x0200001E RID: 30
		// (Invoke) Token: 0x06000152 RID: 338
		public delegate void KeysRequiredEventHandler(object sender, KeysRequiredEventArgs e);

		// Token: 0x0200001F RID: 31
		[Flags]
		private enum HeaderTest
		{
			// Token: 0x040000ED RID: 237
			Extract = 1,
			// Token: 0x040000EE RID: 238
			Header = 2
		}

		// Token: 0x02000020 RID: 32
		private enum UpdateCommand
		{
			// Token: 0x040000F0 RID: 240
			Copy,
			// Token: 0x040000F1 RID: 241
			Modify,
			// Token: 0x040000F2 RID: 242
			Add
		}

		// Token: 0x02000021 RID: 33
		private class UpdateComparer : IComparer
		{
			// Token: 0x06000155 RID: 341 RVA: 0x00008E30 File Offset: 0x00007E30
			public int Compare(object x, object y)
			{
				ZipFile.ZipUpdate zipUpdate = x as ZipFile.ZipUpdate;
				ZipFile.ZipUpdate zipUpdate2 = y as ZipFile.ZipUpdate;
				int num;
				if (zipUpdate == null)
				{
					if (zipUpdate2 == null)
					{
						num = 0;
					}
					else
					{
						num = -1;
					}
				}
				else if (zipUpdate2 == null)
				{
					num = 1;
				}
				else
				{
					int num2 = (zipUpdate.Command == ZipFile.UpdateCommand.Copy || zipUpdate.Command == ZipFile.UpdateCommand.Modify) ? 0 : 1;
					int num3 = (zipUpdate2.Command == ZipFile.UpdateCommand.Copy || zipUpdate2.Command == ZipFile.UpdateCommand.Modify) ? 0 : 1;
					num = num2 - num3;
					if (num == 0)
					{
						long num4 = zipUpdate.Entry.Offset - zipUpdate2.Entry.Offset;
						if (num4 < 0L)
						{
							num = -1;
						}
						else if (num4 == 0L)
						{
							num = 0;
						}
						else
						{
							num = 1;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x02000022 RID: 34
		private class ZipUpdate
		{
			// Token: 0x06000157 RID: 343 RVA: 0x00008ECD File Offset: 0x00007ECD
			public ZipUpdate(string fileName, ZipEntry entry)
			{
				this.command_ = ZipFile.UpdateCommand.Add;
				this.entry_ = entry;
				this.filename_ = fileName;
			}

			// Token: 0x06000158 RID: 344 RVA: 0x00008F04 File Offset: 0x00007F04
			[Obsolete]
			public ZipUpdate(string fileName, string entryName, CompressionMethod compressionMethod)
			{
				this.command_ = ZipFile.UpdateCommand.Add;
				this.entry_ = new ZipEntry(entryName);
				this.entry_.CompressionMethod = compressionMethod;
				this.filename_ = fileName;
			}

			// Token: 0x06000159 RID: 345 RVA: 0x00008F55 File Offset: 0x00007F55
			[Obsolete]
			public ZipUpdate(string fileName, string entryName) : this(fileName, entryName, CompressionMethod.Deflated)
			{
			}

			// Token: 0x0600015A RID: 346 RVA: 0x00008F60 File Offset: 0x00007F60
			[Obsolete]
			public ZipUpdate(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod)
			{
				this.command_ = ZipFile.UpdateCommand.Add;
				this.entry_ = new ZipEntry(entryName);
				this.entry_.CompressionMethod = compressionMethod;
				this.dataSource_ = dataSource;
			}

			// Token: 0x0600015B RID: 347 RVA: 0x00008FB1 File Offset: 0x00007FB1
			public ZipUpdate(IStaticDataSource dataSource, ZipEntry entry)
			{
				this.command_ = ZipFile.UpdateCommand.Add;
				this.entry_ = entry;
				this.dataSource_ = dataSource;
			}

			// Token: 0x0600015C RID: 348 RVA: 0x00008FE6 File Offset: 0x00007FE6
			public ZipUpdate(ZipEntry original, ZipEntry updated)
			{
				throw new ZipException("Modify not currently supported");
			}

			// Token: 0x0600015D RID: 349 RVA: 0x00009010 File Offset: 0x00008010
			public ZipUpdate(ZipFile.UpdateCommand command, ZipEntry entry)
			{
				this.command_ = command;
				this.entry_ = (ZipEntry)entry.Clone();
			}

			// Token: 0x0600015E RID: 350 RVA: 0x00009048 File Offset: 0x00008048
			public ZipUpdate(ZipEntry entry) : this(ZipFile.UpdateCommand.Copy, entry)
			{
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x0600015F RID: 351 RVA: 0x00009052 File Offset: 0x00008052
			public ZipEntry Entry
			{
				get
				{
					return this.entry_;
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000160 RID: 352 RVA: 0x0000905A File Offset: 0x0000805A
			public ZipEntry OutEntry
			{
				get
				{
					if (this.outEntry_ == null)
					{
						this.outEntry_ = (ZipEntry)this.entry_.Clone();
					}
					return this.outEntry_;
				}
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000161 RID: 353 RVA: 0x00009080 File Offset: 0x00008080
			public ZipFile.UpdateCommand Command
			{
				get
				{
					return this.command_;
				}
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000162 RID: 354 RVA: 0x00009088 File Offset: 0x00008088
			public string Filename
			{
				get
				{
					return this.filename_;
				}
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x06000163 RID: 355 RVA: 0x00009090 File Offset: 0x00008090
			// (set) Token: 0x06000164 RID: 356 RVA: 0x00009098 File Offset: 0x00008098
			public long SizePatchOffset
			{
				get
				{
					return this.sizePatchOffset_;
				}
				set
				{
					this.sizePatchOffset_ = value;
				}
			}

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x06000165 RID: 357 RVA: 0x000090A1 File Offset: 0x000080A1
			// (set) Token: 0x06000166 RID: 358 RVA: 0x000090A9 File Offset: 0x000080A9
			public long CrcPatchOffset
			{
				get
				{
					return this.crcPatchOffset_;
				}
				set
				{
					this.crcPatchOffset_ = value;
				}
			}

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x06000167 RID: 359 RVA: 0x000090B2 File Offset: 0x000080B2
			// (set) Token: 0x06000168 RID: 360 RVA: 0x000090BA File Offset: 0x000080BA
			public long OffsetBasedSize
			{
				get
				{
					return this._offsetBasedSize;
				}
				set
				{
					this._offsetBasedSize = value;
				}
			}

			// Token: 0x06000169 RID: 361 RVA: 0x000090C4 File Offset: 0x000080C4
			public Stream GetSource()
			{
				Stream result = null;
				if (this.dataSource_ != null)
				{
					result = this.dataSource_.GetSource();
				}
				return result;
			}

			// Token: 0x040000F3 RID: 243
			private ZipEntry entry_;

			// Token: 0x040000F4 RID: 244
			private ZipEntry outEntry_;

			// Token: 0x040000F5 RID: 245
			private ZipFile.UpdateCommand command_;

			// Token: 0x040000F6 RID: 246
			private IStaticDataSource dataSource_;

			// Token: 0x040000F7 RID: 247
			private string filename_;

			// Token: 0x040000F8 RID: 248
			private long sizePatchOffset_ = -1L;

			// Token: 0x040000F9 RID: 249
			private long crcPatchOffset_ = -1L;

			// Token: 0x040000FA RID: 250
			private long _offsetBasedSize = -1L;
		}

		// Token: 0x02000023 RID: 35
		private class ZipString
		{
			// Token: 0x0600016A RID: 362 RVA: 0x000090E8 File Offset: 0x000080E8
			public ZipString(string comment)
			{
				this.comment_ = comment;
				this.isSourceString_ = true;
			}

			// Token: 0x0600016B RID: 363 RVA: 0x000090FE File Offset: 0x000080FE
			public ZipString(byte[] rawString)
			{
				this.rawComment_ = rawString;
			}

			// Token: 0x17000060 RID: 96
			// (get) Token: 0x0600016C RID: 364 RVA: 0x0000910D File Offset: 0x0000810D
			public bool IsSourceString
			{
				get
				{
					return this.isSourceString_;
				}
			}

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x0600016D RID: 365 RVA: 0x00009115 File Offset: 0x00008115
			public int RawLength
			{
				get
				{
					this.MakeBytesAvailable();
					return this.rawComment_.Length;
				}
			}

			// Token: 0x17000062 RID: 98
			// (get) Token: 0x0600016E RID: 366 RVA: 0x00009125 File Offset: 0x00008125
			public byte[] RawComment
			{
				get
				{
					this.MakeBytesAvailable();
					return (byte[])this.rawComment_.Clone();
				}
			}

			// Token: 0x0600016F RID: 367 RVA: 0x0000913D File Offset: 0x0000813D
			public void Reset()
			{
				if (this.isSourceString_)
				{
					this.rawComment_ = null;
					return;
				}
				this.comment_ = null;
			}

			// Token: 0x06000170 RID: 368 RVA: 0x00009156 File Offset: 0x00008156
			private void MakeTextAvailable()
			{
				if (this.comment_ == null)
				{
					this.comment_ = ZipConstants.ConvertToString(this.rawComment_);
				}
			}

			// Token: 0x06000171 RID: 369 RVA: 0x00009171 File Offset: 0x00008171
			private void MakeBytesAvailable()
			{
				if (this.rawComment_ == null)
				{
					this.rawComment_ = ZipConstants.ConvertToArray(this.comment_);
				}
			}

			// Token: 0x06000172 RID: 370 RVA: 0x0000918C File Offset: 0x0000818C
			public static implicit operator string(ZipFile.ZipString zipString)
			{
				zipString.MakeTextAvailable();
				return zipString.comment_;
			}

			// Token: 0x040000FB RID: 251
			private string comment_;

			// Token: 0x040000FC RID: 252
			private byte[] rawComment_;

			// Token: 0x040000FD RID: 253
			private bool isSourceString_;
		}

		// Token: 0x02000024 RID: 36
		private class ZipEntryEnumerator : IEnumerator
		{
			// Token: 0x06000173 RID: 371 RVA: 0x0000919A File Offset: 0x0000819A
			public ZipEntryEnumerator(ZipEntry[] entries)
			{
				this.array = entries;
			}

			// Token: 0x17000063 RID: 99
			// (get) Token: 0x06000174 RID: 372 RVA: 0x000091B0 File Offset: 0x000081B0
			public object Current
			{
				get
				{
					return this.array[this.index];
				}
			}

			// Token: 0x06000175 RID: 373 RVA: 0x000091BF File Offset: 0x000081BF
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x06000176 RID: 374 RVA: 0x000091C8 File Offset: 0x000081C8
			public bool MoveNext()
			{
				return ++this.index < this.array.Length;
			}

			// Token: 0x040000FE RID: 254
			private ZipEntry[] array;

			// Token: 0x040000FF RID: 255
			private int index = -1;
		}

		// Token: 0x02000025 RID: 37
		private class UncompressedStream : Stream
		{
			// Token: 0x06000177 RID: 375 RVA: 0x000091F0 File Offset: 0x000081F0
			public UncompressedStream(Stream baseStream)
			{
				this.baseStream_ = baseStream;
			}

			// Token: 0x06000178 RID: 376 RVA: 0x000091FF File Offset: 0x000081FF
			public override void Close()
			{
			}

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x06000179 RID: 377 RVA: 0x00009201 File Offset: 0x00008201
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600017A RID: 378 RVA: 0x00009204 File Offset: 0x00008204
			public override void Flush()
			{
				this.baseStream_.Flush();
			}

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x0600017B RID: 379 RVA: 0x00009211 File Offset: 0x00008211
			public override bool CanWrite
			{
				get
				{
					return this.baseStream_.CanWrite;
				}
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x0600017C RID: 380 RVA: 0x0000921E File Offset: 0x0000821E
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x0600017D RID: 381 RVA: 0x00009221 File Offset: 0x00008221
			public override long Length
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x0600017E RID: 382 RVA: 0x00009225 File Offset: 0x00008225
			// (set) Token: 0x0600017F RID: 383 RVA: 0x00009232 File Offset: 0x00008232
			public override long Position
			{
				get
				{
					return this.baseStream_.Position;
				}
				set
				{
				}
			}

			// Token: 0x06000180 RID: 384 RVA: 0x00009234 File Offset: 0x00008234
			public override int Read(byte[] buffer, int offset, int count)
			{
				return 0;
			}

			// Token: 0x06000181 RID: 385 RVA: 0x00009237 File Offset: 0x00008237
			public override long Seek(long offset, SeekOrigin origin)
			{
				return 0L;
			}

			// Token: 0x06000182 RID: 386 RVA: 0x0000923B File Offset: 0x0000823B
			public override void SetLength(long value)
			{
			}

			// Token: 0x06000183 RID: 387 RVA: 0x0000923D File Offset: 0x0000823D
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.baseStream_.Write(buffer, offset, count);
			}

			// Token: 0x04000100 RID: 256
			private Stream baseStream_;
		}

		// Token: 0x02000026 RID: 38
		private class PartialInputStream : Stream
		{
			// Token: 0x06000184 RID: 388 RVA: 0x0000924D File Offset: 0x0000824D
			public PartialInputStream(ZipFile zipFile, long start, long length)
			{
				this.start_ = start;
				this.length_ = length;
				this.zipFile_ = zipFile;
				this.baseStream_ = this.zipFile_.baseStream_;
				this.readPos_ = start;
				this.end_ = start + length;
			}

			// Token: 0x06000185 RID: 389 RVA: 0x0000928C File Offset: 0x0000828C
			public override int ReadByte()
			{
				if (this.readPos_ >= this.end_)
				{
					return -1;
				}
				int result;
				lock (this.baseStream_)
				{
					Stream stream = this.baseStream_;
					long offset;
					this.readPos_ = (offset = this.readPos_) + 1L;
					stream.Seek(offset, SeekOrigin.Begin);
					result = this.baseStream_.ReadByte();
				}
				return result;
			}

			// Token: 0x06000186 RID: 390 RVA: 0x000092FC File Offset: 0x000082FC
			public override void Close()
			{
			}

			// Token: 0x06000187 RID: 391 RVA: 0x00009300 File Offset: 0x00008300
			public override int Read(byte[] buffer, int offset, int count)
			{
				int result;
				lock (this.baseStream_)
				{
					if ((long)count > this.end_ - this.readPos_)
					{
						count = (int)(this.end_ - this.readPos_);
						if (count == 0)
						{
							return 0;
						}
					}
					if (this.baseStream_.Position != this.readPos_)
					{
						this.baseStream_.Seek(this.readPos_, SeekOrigin.Begin);
					}
					int num = this.baseStream_.Read(buffer, offset, count);
					if (num > 0)
					{
						this.readPos_ += (long)num;
					}
					result = num;
				}
				return result;
			}

			// Token: 0x06000188 RID: 392 RVA: 0x000093A8 File Offset: 0x000083A8
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000189 RID: 393 RVA: 0x000093AF File Offset: 0x000083AF
			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600018A RID: 394 RVA: 0x000093B8 File Offset: 0x000083B8
			public override long Seek(long offset, SeekOrigin origin)
			{
				long num = this.readPos_;
				switch (origin)
				{
				case SeekOrigin.Begin:
					num = this.start_ + offset;
					break;
				case SeekOrigin.Current:
					num = this.readPos_ + offset;
					break;
				case SeekOrigin.End:
					num = this.end_ + offset;
					break;
				}
				if (num < this.start_)
				{
					throw new ArgumentException("Negative position is invalid");
				}
				if (num >= this.end_)
				{
					throw new IOException("Cannot seek past end");
				}
				this.readPos_ = num;
				return this.readPos_;
			}

			// Token: 0x0600018B RID: 395 RVA: 0x00009436 File Offset: 0x00008436
			public override void Flush()
			{
			}

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x0600018C RID: 396 RVA: 0x00009438 File Offset: 0x00008438
			// (set) Token: 0x0600018D RID: 397 RVA: 0x00009448 File Offset: 0x00008448
			public override long Position
			{
				get
				{
					return this.readPos_ - this.start_;
				}
				set
				{
					long num = this.start_ + value;
					if (num < this.start_)
					{
						throw new ArgumentException("Negative position is invalid");
					}
					if (num >= this.end_)
					{
						throw new InvalidOperationException("Cannot seek past end");
					}
					this.readPos_ = num;
				}
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x0600018E RID: 398 RVA: 0x0000948D File Offset: 0x0000848D
			public override long Length
			{
				get
				{
					return this.length_;
				}
			}

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x0600018F RID: 399 RVA: 0x00009495 File Offset: 0x00008495
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x06000190 RID: 400 RVA: 0x00009498 File Offset: 0x00008498
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x06000191 RID: 401 RVA: 0x0000949B File Offset: 0x0000849B
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x06000192 RID: 402 RVA: 0x0000949E File Offset: 0x0000849E
			public override bool CanTimeout
			{
				get
				{
					return this.baseStream_.CanTimeout;
				}
			}

			// Token: 0x04000101 RID: 257
			private ZipFile zipFile_;

			// Token: 0x04000102 RID: 258
			private Stream baseStream_;

			// Token: 0x04000103 RID: 259
			private long start_;

			// Token: 0x04000104 RID: 260
			private long length_;

			// Token: 0x04000105 RID: 261
			private long readPos_;

			// Token: 0x04000106 RID: 262
			private long end_;
		}
	}
}
