using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Net.Cache
{
	// Token: 0x02000319 RID: 793
	internal static class _WinInetCache
	{
		// Token: 0x06001C4B RID: 7243 RVA: 0x000863CC File Offset: 0x000845CC
		internal unsafe static _WinInetCache.Status LookupInfo(_WinInetCache.Entry entry)
		{
			byte[] array = new byte[2048];
			int num = array.Length;
			byte[] array2 = array;
			for (int i = 0; i < 64; i++)
			{
				try
				{
					byte[] array3;
					byte* ptr;
					if ((array3 = array2) == null || array3.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array3[0];
					}
					bool urlCacheEntryInfoW = UnsafeNclNativeMethods.UnsafeWinInetCache.GetUrlCacheEntryInfoW(entry.Key, ptr, ref num);
					if (urlCacheEntryInfoW)
					{
						array = array2;
						entry.MaxBufferBytes = num;
						_WinInetCache.EntryFixup(entry, (_WinInetCache.EntryBuffer*)ptr, array2);
						entry.Error = _WinInetCache.Status.Success;
						return entry.Error;
					}
					entry.Error = (_WinInetCache.Status)Marshal.GetLastWin32Error();
					if (entry.Error != _WinInetCache.Status.InsufficientBuffer || array2 != array || num > entry.MaxBufferBytes)
					{
						break;
					}
					array2 = new byte[num];
				}
				finally
				{
					byte[] array3 = null;
				}
			}
			return entry.Error;
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x000864A0 File Offset: 0x000846A0
		internal unsafe static SafeUnlockUrlCacheEntryFile LookupFile(_WinInetCache.Entry entry)
		{
			byte[] array = new byte[2048];
			int num = array.Length;
			SafeUnlockUrlCacheEntryFile safeUnlockUrlCacheEntryFile = null;
			try
			{
				for (;;)
				{
					try
					{
						byte[] array2;
						byte* ptr;
						if ((array2 = array) == null || array2.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array2[0];
						}
						entry.Error = SafeUnlockUrlCacheEntryFile.GetAndLockFile(entry.Key, ptr, ref num, out safeUnlockUrlCacheEntryFile);
						if (entry.Error == _WinInetCache.Status.Success)
						{
							entry.MaxBufferBytes = num;
							_WinInetCache.EntryFixup(entry, (_WinInetCache.EntryBuffer*)ptr, array);
							return safeUnlockUrlCacheEntryFile;
						}
						if (entry.Error == _WinInetCache.Status.InsufficientBuffer && num <= entry.MaxBufferBytes)
						{
							array = new byte[num];
							continue;
						}
					}
					finally
					{
						byte[] array2 = null;
					}
					break;
				}
			}
			catch (Exception ex)
			{
				if (safeUnlockUrlCacheEntryFile != null)
				{
					safeUnlockUrlCacheEntryFile.Close();
				}
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (entry.Error == _WinInetCache.Status.Success)
				{
					entry.Error = _WinInetCache.Status.InternalError;
				}
			}
			return null;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x00086590 File Offset: 0x00084790
		private unsafe static _WinInetCache.Status EntryFixup(_WinInetCache.Entry entry, _WinInetCache.EntryBuffer* bufferPtr, byte[] buffer)
		{
			bufferPtr->_OffsetExtension = ((bufferPtr->_OffsetExtension == IntPtr.Zero) ? IntPtr.Zero : ((IntPtr)((long)((byte*)((void*)bufferPtr->_OffsetExtension) - (byte*)bufferPtr))));
			bufferPtr->_OffsetFileName = ((bufferPtr->_OffsetFileName == IntPtr.Zero) ? IntPtr.Zero : ((IntPtr)((long)((byte*)((void*)bufferPtr->_OffsetFileName) - (byte*)bufferPtr))));
			bufferPtr->_OffsetHeaderInfo = ((bufferPtr->_OffsetHeaderInfo == IntPtr.Zero) ? IntPtr.Zero : ((IntPtr)((long)((byte*)((void*)bufferPtr->_OffsetHeaderInfo) - (byte*)bufferPtr))));
			bufferPtr->_OffsetSourceUrlName = ((bufferPtr->_OffsetSourceUrlName == IntPtr.Zero) ? IntPtr.Zero : ((IntPtr)((long)((byte*)((void*)bufferPtr->_OffsetSourceUrlName) - (byte*)bufferPtr))));
			entry.Info = *bufferPtr;
			entry.OriginalUrl = _WinInetCache.GetEntryBufferString((void*)bufferPtr, (int)bufferPtr->_OffsetSourceUrlName);
			entry.Filename = _WinInetCache.GetEntryBufferString((void*)bufferPtr, (int)bufferPtr->_OffsetFileName);
			entry.FileExt = _WinInetCache.GetEntryBufferString((void*)bufferPtr, (int)bufferPtr->_OffsetExtension);
			return _WinInetCache.GetEntryHeaders(entry, bufferPtr, buffer);
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x000866C8 File Offset: 0x000848C8
		internal static _WinInetCache.Status CreateFileName(_WinInetCache.Entry entry)
		{
			entry.Error = _WinInetCache.Status.Success;
			StringBuilder stringBuilder = new StringBuilder(260);
			if (UnsafeNclNativeMethods.UnsafeWinInetCache.CreateUrlCacheEntryW(entry.Key, entry.OptionalLength, entry.FileExt, stringBuilder, 0))
			{
				entry.Filename = stringBuilder.ToString();
				return _WinInetCache.Status.Success;
			}
			entry.Error = (_WinInetCache.Status)Marshal.GetLastWin32Error();
			return entry.Error;
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x00086724 File Offset: 0x00084924
		internal unsafe static _WinInetCache.Status Commit(_WinInetCache.Entry entry)
		{
			string text = entry.MetaInfo;
			if (text == null)
			{
				text = string.Empty;
			}
			if (text.Length + entry.Key.Length + entry.Filename.Length + ((entry.OriginalUrl == null) ? 0 : entry.OriginalUrl.Length) > entry.MaxBufferBytes / 2)
			{
				entry.Error = _WinInetCache.Status.InsufficientBuffer;
				return entry.Error;
			}
			entry.Error = _WinInetCache.Status.Success;
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				byte* headerInfo = (byte*)((text.Length == 0) ? null : ptr);
				if (!UnsafeNclNativeMethods.UnsafeWinInetCache.CommitUrlCacheEntryW(entry.Key, entry.Filename, entry.Info.ExpireTime, entry.Info.LastModifiedTime, entry.Info.EntryType, headerInfo, text.Length, null, entry.OriginalUrl))
				{
					entry.Error = (_WinInetCache.Status)Marshal.GetLastWin32Error();
				}
			}
			return entry.Error;
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x0008680C File Offset: 0x00084A0C
		internal unsafe static _WinInetCache.Status Update(_WinInetCache.Entry newEntry, _WinInetCache.Entry_FC attributes)
		{
			byte[] array = new byte[_WinInetCache.EntryBuffer.MarshalSize];
			newEntry.Error = _WinInetCache.Status.Success;
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			_WinInetCache.EntryBuffer* ptr2 = (_WinInetCache.EntryBuffer*)ptr;
			*ptr2 = newEntry.Info;
			ptr2->StructSize = _WinInetCache.EntryBuffer.MarshalSize;
			if ((attributes & _WinInetCache.Entry_FC.Headerinfo) != _WinInetCache.Entry_FC.None)
			{
				_WinInetCache.Entry entry = new _WinInetCache.Entry(newEntry.Key, newEntry.MaxBufferBytes);
				SafeUnlockUrlCacheEntryFile safeUnlockUrlCacheEntryFile = null;
				bool flag = false;
				try
				{
					safeUnlockUrlCacheEntryFile = _WinInetCache.LookupFile(entry);
					if (safeUnlockUrlCacheEntryFile == null)
					{
						newEntry.Error = entry.Error;
						return newEntry.Error;
					}
					newEntry.Filename = entry.Filename;
					newEntry.OriginalUrl = entry.OriginalUrl;
					newEntry.FileExt = entry.FileExt;
					attributes &= ~_WinInetCache.Entry_FC.Headerinfo;
					if ((attributes & _WinInetCache.Entry_FC.Exptime) == _WinInetCache.Entry_FC.None)
					{
						newEntry.Info.ExpireTime = entry.Info.ExpireTime;
					}
					if ((attributes & _WinInetCache.Entry_FC.Modtime) == _WinInetCache.Entry_FC.None)
					{
						newEntry.Info.LastModifiedTime = entry.Info.LastModifiedTime;
					}
					if ((attributes & _WinInetCache.Entry_FC.Attribute) == _WinInetCache.Entry_FC.None)
					{
						newEntry.Info.EntryType = entry.Info.EntryType;
						newEntry.Info.U.ExemptDelta = entry.Info.U.ExemptDelta;
						if ((entry.Info.EntryType & _WinInetCache.EntryType.StickyEntry) == _WinInetCache.EntryType.StickyEntry)
						{
							attributes |= (_WinInetCache.Entry_FC.Attribute | _WinInetCache.Entry_FC.ExemptDelta);
						}
					}
					attributes &= ~(_WinInetCache.Entry_FC.Modtime | _WinInetCache.Entry_FC.Exptime);
					flag = ((entry.Info.EntryType & _WinInetCache.EntryType.Edited) > (_WinInetCache.EntryType)0);
					if (!flag)
					{
						_WinInetCache.Entry entry2 = entry;
						entry2.Info.EntryType = (entry2.Info.EntryType | _WinInetCache.EntryType.Edited);
						if (_WinInetCache.Update(entry, _WinInetCache.Entry_FC.Attribute) != _WinInetCache.Status.Success)
						{
							newEntry.Error = entry.Error;
							return newEntry.Error;
						}
					}
				}
				finally
				{
					if (safeUnlockUrlCacheEntryFile != null)
					{
						safeUnlockUrlCacheEntryFile.Close();
					}
				}
				_WinInetCache.Remove(entry);
				if (_WinInetCache.Commit(newEntry) != _WinInetCache.Status.Success)
				{
					if (!flag)
					{
						_WinInetCache.Entry entry3 = entry;
						entry3.Info.EntryType = (entry3.Info.EntryType & ~_WinInetCache.EntryType.Edited);
						_WinInetCache.Update(entry, _WinInetCache.Entry_FC.Attribute);
					}
					return newEntry.Error;
				}
				if (attributes != _WinInetCache.Entry_FC.None)
				{
					_WinInetCache.Update(newEntry, attributes);
				}
				goto IL_215;
			}
			if (!UnsafeNclNativeMethods.UnsafeWinInetCache.SetUrlCacheEntryInfoW(newEntry.Key, ptr, attributes))
			{
				newEntry.Error = (_WinInetCache.Status)Marshal.GetLastWin32Error();
			}
			IL_215:
			array2 = null;
			return newEntry.Error;
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x00086A58 File Offset: 0x00084C58
		internal static _WinInetCache.Status Remove(_WinInetCache.Entry entry)
		{
			entry.Error = _WinInetCache.Status.Success;
			if (!UnsafeNclNativeMethods.UnsafeWinInetCache.DeleteUrlCacheEntryW(entry.Key))
			{
				entry.Error = (_WinInetCache.Status)Marshal.GetLastWin32Error();
			}
			return entry.Error;
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x00086A80 File Offset: 0x00084C80
		private unsafe static string GetEntryBufferString(void* bufferPtr, int offset)
		{
			if (offset == 0)
			{
				return null;
			}
			IntPtr ptr = new IntPtr((void*)((byte*)bufferPtr + offset));
			return Marshal.PtrToStringUni(ptr);
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x00086AA4 File Offset: 0x00084CA4
		private unsafe static _WinInetCache.Status GetEntryHeaders(_WinInetCache.Entry entry, _WinInetCache.EntryBuffer* bufferPtr, byte[] buffer)
		{
			entry.Error = _WinInetCache.Status.Success;
			entry.MetaInfo = null;
			if (bufferPtr->_OffsetHeaderInfo == IntPtr.Zero || bufferPtr->HeaderInfoChars == 0 || (bufferPtr->EntryType & _WinInetCache.EntryType.UrlHistory) != (_WinInetCache.EntryType)0)
			{
				return _WinInetCache.Status.Success;
			}
			int num = bufferPtr->HeaderInfoChars + (int)bufferPtr->_OffsetHeaderInfo / 2;
			if (num * 2 > entry.MaxBufferBytes)
			{
				num = entry.MaxBufferBytes / 2;
			}
			while (*(ushort*)(bufferPtr + (IntPtr)(num - 1) * 2 / (IntPtr)sizeof(_WinInetCache.EntryBuffer)) == 0)
			{
				num--;
			}
			entry.MetaInfo = Encoding.Unicode.GetString(buffer, (int)bufferPtr->_OffsetHeaderInfo, (num - (int)bufferPtr->_OffsetHeaderInfo / 2) * 2);
			return entry.Error;
		}

		// Token: 0x04001B96 RID: 7062
		private const int c_CharSz = 2;

		// Token: 0x020007B3 RID: 1971
		[Flags]
		internal enum EntryType
		{
			// Token: 0x0400341C RID: 13340
			NormalEntry = 65,
			// Token: 0x0400341D RID: 13341
			StickyEntry = 68,
			// Token: 0x0400341E RID: 13342
			Edited = 8,
			// Token: 0x0400341F RID: 13343
			TrackOffline = 16,
			// Token: 0x04003420 RID: 13344
			TrackOnline = 32,
			// Token: 0x04003421 RID: 13345
			Sparse = 65536,
			// Token: 0x04003422 RID: 13346
			Cookie = 1048576,
			// Token: 0x04003423 RID: 13347
			UrlHistory = 2097152
		}

		// Token: 0x020007B4 RID: 1972
		[Flags]
		internal enum Entry_FC
		{
			// Token: 0x04003425 RID: 13349
			None = 0,
			// Token: 0x04003426 RID: 13350
			Attribute = 4,
			// Token: 0x04003427 RID: 13351
			Hitrate = 16,
			// Token: 0x04003428 RID: 13352
			Modtime = 64,
			// Token: 0x04003429 RID: 13353
			Exptime = 128,
			// Token: 0x0400342A RID: 13354
			Acctime = 256,
			// Token: 0x0400342B RID: 13355
			Synctime = 512,
			// Token: 0x0400342C RID: 13356
			Headerinfo = 1024,
			// Token: 0x0400342D RID: 13357
			ExemptDelta = 2048
		}

		// Token: 0x020007B5 RID: 1973
		internal enum Status
		{
			// Token: 0x0400342F RID: 13359
			Success,
			// Token: 0x04003430 RID: 13360
			InsufficientBuffer = 122,
			// Token: 0x04003431 RID: 13361
			FileNotFound = 2,
			// Token: 0x04003432 RID: 13362
			NoMoreItems = 259,
			// Token: 0x04003433 RID: 13363
			NotEnoughStorage = 8,
			// Token: 0x04003434 RID: 13364
			SharingViolation = 32,
			// Token: 0x04003435 RID: 13365
			InvalidParameter = 87,
			// Token: 0x04003436 RID: 13366
			Warnings = 16777216,
			// Token: 0x04003437 RID: 13367
			FatalErrors = 16781312,
			// Token: 0x04003438 RID: 13368
			CorruptedHeaders,
			// Token: 0x04003439 RID: 13369
			InternalError
		}

		// Token: 0x020007B6 RID: 1974
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct FILETIME
		{
			// Token: 0x0600433A RID: 17210 RVA: 0x00119F66 File Offset: 0x00118166
			public FILETIME(long time)
			{
				this.Low = (uint)time;
				this.High = (uint)(time >> 32);
			}

			// Token: 0x0600433B RID: 17211 RVA: 0x00119F7B File Offset: 0x0011817B
			public long ToLong()
			{
				return (long)((ulong)this.High << 32 | (ulong)this.Low);
			}

			// Token: 0x17000F45 RID: 3909
			// (get) Token: 0x0600433C RID: 17212 RVA: 0x00119F8F File Offset: 0x0011818F
			public bool IsNull
			{
				get
				{
					return this.Low == 0U && this.High == 0U;
				}
			}

			// Token: 0x0400343A RID: 13370
			public uint Low;

			// Token: 0x0400343B RID: 13371
			public uint High;

			// Token: 0x0400343C RID: 13372
			public static readonly _WinInetCache.FILETIME Zero = new _WinInetCache.FILETIME(0L);
		}

		// Token: 0x020007B7 RID: 1975
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct EntryBuffer
		{
			// Token: 0x0400343D RID: 13373
			public static int MarshalSize = Marshal.SizeOf(typeof(_WinInetCache.EntryBuffer));

			// Token: 0x0400343E RID: 13374
			public int StructSize;

			// Token: 0x0400343F RID: 13375
			public IntPtr _OffsetSourceUrlName;

			// Token: 0x04003440 RID: 13376
			public IntPtr _OffsetFileName;

			// Token: 0x04003441 RID: 13377
			public _WinInetCache.EntryType EntryType;

			// Token: 0x04003442 RID: 13378
			public int UseCount;

			// Token: 0x04003443 RID: 13379
			public int HitRate;

			// Token: 0x04003444 RID: 13380
			public int SizeLow;

			// Token: 0x04003445 RID: 13381
			public int SizeHigh;

			// Token: 0x04003446 RID: 13382
			public _WinInetCache.FILETIME LastModifiedTime;

			// Token: 0x04003447 RID: 13383
			public _WinInetCache.FILETIME ExpireTime;

			// Token: 0x04003448 RID: 13384
			public _WinInetCache.FILETIME LastAccessTime;

			// Token: 0x04003449 RID: 13385
			public _WinInetCache.FILETIME LastSyncTime;

			// Token: 0x0400344A RID: 13386
			public IntPtr _OffsetHeaderInfo;

			// Token: 0x0400344B RID: 13387
			public int HeaderInfoChars;

			// Token: 0x0400344C RID: 13388
			public IntPtr _OffsetExtension;

			// Token: 0x0400344D RID: 13389
			public _WinInetCache.EntryBuffer.Rsv U;

			// Token: 0x02000921 RID: 2337
			[StructLayout(LayoutKind.Explicit)]
			public struct Rsv
			{
				// Token: 0x04003DAA RID: 15786
				[FieldOffset(0)]
				public int ExemptDelta;

				// Token: 0x04003DAB RID: 15787
				[FieldOffset(0)]
				public int Reserved;
			}
		}

		// Token: 0x020007B8 RID: 1976
		internal class Entry
		{
			// Token: 0x0600433F RID: 17215 RVA: 0x00119FC8 File Offset: 0x001181C8
			public Entry(string key, int maxHeadersSize)
			{
				this.Key = key;
				this.MaxBufferBytes = maxHeadersSize;
				if (maxHeadersSize != 2147483647 && 2147483647 - (key.Length + _WinInetCache.EntryBuffer.MarshalSize + 1024) * 2 > maxHeadersSize)
				{
					this.MaxBufferBytes += (key.Length + _WinInetCache.EntryBuffer.MarshalSize + 1024) * 2;
				}
				this.Info.EntryType = _WinInetCache.EntryType.NormalEntry;
			}

			// Token: 0x0400344E RID: 13390
			public const int DefaultBufferSize = 2048;

			// Token: 0x0400344F RID: 13391
			public _WinInetCache.Status Error;

			// Token: 0x04003450 RID: 13392
			public string Key;

			// Token: 0x04003451 RID: 13393
			public string Filename;

			// Token: 0x04003452 RID: 13394
			public string FileExt;

			// Token: 0x04003453 RID: 13395
			public int OptionalLength;

			// Token: 0x04003454 RID: 13396
			public string OriginalUrl;

			// Token: 0x04003455 RID: 13397
			public string MetaInfo;

			// Token: 0x04003456 RID: 13398
			public int MaxBufferBytes;

			// Token: 0x04003457 RID: 13399
			public _WinInetCache.EntryBuffer Info;
		}
	}
}
