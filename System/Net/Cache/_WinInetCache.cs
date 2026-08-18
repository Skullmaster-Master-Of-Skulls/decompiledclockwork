using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Net.Cache
{
	// Token: 0x02000572 RID: 1394
	internal class _WinInetCache
	{
		// Token: 0x06002AAC RID: 10924 RVA: 0x000B53A2 File Offset: 0x000B43A2
		private _WinInetCache()
		{
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x000B53AC File Offset: 0x000B43AC
		internal unsafe static _WinInetCache.Status LookupInfo(_WinInetCache.Entry entry)
		{
			byte[] array = new byte[2048];
			int num = array.Length;
			byte[] array2 = array;
			for (int i = 0; i < 64; i++)
			{
				try
				{
					fixed (byte* ptr = array2)
					{
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
				}
				finally
				{
					byte* ptr = null;
				}
			}
			return entry.Error;
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x000B5480 File Offset: 0x000B4480
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
						fixed (byte* ptr = array)
						{
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
					}
					finally
					{
						byte* ptr = null;
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
			catch
			{
				if (safeUnlockUrlCacheEntryFile != null)
				{
					safeUnlockUrlCacheEntryFile.Close();
				}
				if (entry.Error == _WinInetCache.Status.Success)
				{
					entry.Error = _WinInetCache.Status.InternalError;
				}
			}
			return null;
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000B55A0 File Offset: 0x000B45A0
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

		// Token: 0x06002AB0 RID: 10928 RVA: 0x000B56D8 File Offset: 0x000B46D8
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

		// Token: 0x06002AB1 RID: 10929 RVA: 0x000B5734 File Offset: 0x000B4734
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
			fixed (char* ptr = text)
			{
				byte* headerInfo = (byte*)((text.Length == 0) ? null : ptr);
				if (!UnsafeNclNativeMethods.UnsafeWinInetCache.CommitUrlCacheEntryW(entry.Key, entry.Filename, entry.Info.ExpireTime, entry.Info.LastModifiedTime, entry.Info.EntryType, headerInfo, text.Length, null, entry.OriginalUrl))
				{
					entry.Error = (_WinInetCache.Status)Marshal.GetLastWin32Error();
				}
			}
			return entry.Error;
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x000B5818 File Offset: 0x000B4818
		internal unsafe static _WinInetCache.Status Update(_WinInetCache.Entry newEntry, _WinInetCache.Entry_FC attributes)
		{
			byte[] array = new byte[_WinInetCache.EntryBuffer.MarshalSize];
			newEntry.Error = _WinInetCache.Status.Success;
			fixed (byte* ptr = array)
			{
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
						flag = ((entry.Info.EntryType & _WinInetCache.EntryType.Edited) != (_WinInetCache.EntryType)0);
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
					_WinInetCache.Status error;
					if (_WinInetCache.Commit(newEntry) != _WinInetCache.Status.Success)
					{
						if (!flag)
						{
							_WinInetCache.Entry entry3 = entry;
							entry3.Info.EntryType = (entry3.Info.EntryType & ~_WinInetCache.EntryType.Edited);
							_WinInetCache.Update(entry, _WinInetCache.Entry_FC.Attribute);
						}
						error = newEntry.Error;
					}
					else
					{
						if (attributes != _WinInetCache.Entry_FC.None)
						{
							_WinInetCache.Update(newEntry, attributes);
							goto IL_213;
						}
						goto IL_213;
					}
					return error;
				}
				if (!UnsafeNclNativeMethods.UnsafeWinInetCache.SetUrlCacheEntryInfoW(newEntry.Key, ptr, attributes))
				{
					newEntry.Error = (_WinInetCache.Status)Marshal.GetLastWin32Error();
				}
				IL_213:;
			}
			return newEntry.Error;
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x000B5A60 File Offset: 0x000B4A60
		internal static _WinInetCache.Status Remove(_WinInetCache.Entry entry)
		{
			entry.Error = _WinInetCache.Status.Success;
			if (!UnsafeNclNativeMethods.UnsafeWinInetCache.DeleteUrlCacheEntryW(entry.Key))
			{
				entry.Error = (_WinInetCache.Status)Marshal.GetLastWin32Error();
			}
			return entry.Error;
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x000B5A88 File Offset: 0x000B4A88
		private unsafe static string GetEntryBufferString(void* bufferPtr, int offset)
		{
			if (offset == 0)
			{
				return null;
			}
			IntPtr ptr = new IntPtr((void*)((byte*)bufferPtr + offset));
			return Marshal.PtrToStringUni(ptr);
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x000B5AAC File Offset: 0x000B4AAC
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

		// Token: 0x04002949 RID: 10569
		private const int c_CharSz = 2;

		// Token: 0x02000573 RID: 1395
		[Flags]
		internal enum EntryType
		{
			// Token: 0x0400294B RID: 10571
			NormalEntry = 65,
			// Token: 0x0400294C RID: 10572
			StickyEntry = 68,
			// Token: 0x0400294D RID: 10573
			Edited = 8,
			// Token: 0x0400294E RID: 10574
			TrackOffline = 16,
			// Token: 0x0400294F RID: 10575
			TrackOnline = 32,
			// Token: 0x04002950 RID: 10576
			Sparse = 65536,
			// Token: 0x04002951 RID: 10577
			Cookie = 1048576,
			// Token: 0x04002952 RID: 10578
			UrlHistory = 2097152
		}

		// Token: 0x02000574 RID: 1396
		[Flags]
		internal enum Entry_FC
		{
			// Token: 0x04002954 RID: 10580
			None = 0,
			// Token: 0x04002955 RID: 10581
			Attribute = 4,
			// Token: 0x04002956 RID: 10582
			Hitrate = 16,
			// Token: 0x04002957 RID: 10583
			Modtime = 64,
			// Token: 0x04002958 RID: 10584
			Exptime = 128,
			// Token: 0x04002959 RID: 10585
			Acctime = 256,
			// Token: 0x0400295A RID: 10586
			Synctime = 512,
			// Token: 0x0400295B RID: 10587
			Headerinfo = 1024,
			// Token: 0x0400295C RID: 10588
			ExemptDelta = 2048
		}

		// Token: 0x02000575 RID: 1397
		internal enum Status
		{
			// Token: 0x0400295E RID: 10590
			Success,
			// Token: 0x0400295F RID: 10591
			InsufficientBuffer = 122,
			// Token: 0x04002960 RID: 10592
			FileNotFound = 2,
			// Token: 0x04002961 RID: 10593
			NoMoreItems = 259,
			// Token: 0x04002962 RID: 10594
			NotEnoughStorage = 8,
			// Token: 0x04002963 RID: 10595
			SharingViolation = 32,
			// Token: 0x04002964 RID: 10596
			InvalidParameter = 87,
			// Token: 0x04002965 RID: 10597
			Warnings = 16777216,
			// Token: 0x04002966 RID: 10598
			FatalErrors = 16781312,
			// Token: 0x04002967 RID: 10599
			CorruptedHeaders,
			// Token: 0x04002968 RID: 10600
			InternalError
		}

		// Token: 0x02000576 RID: 1398
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct FILETIME
		{
			// Token: 0x06002AB6 RID: 10934 RVA: 0x000B5B5E File Offset: 0x000B4B5E
			public FILETIME(long time)
			{
				this.Low = (uint)time;
				this.High = (uint)(time >> 32);
			}

			// Token: 0x06002AB7 RID: 10935 RVA: 0x000B5B73 File Offset: 0x000B4B73
			public long ToLong()
			{
				return (long)((ulong)this.High << 32 | (ulong)this.Low);
			}

			// Token: 0x170008D6 RID: 2262
			// (get) Token: 0x06002AB8 RID: 10936 RVA: 0x000B5B87 File Offset: 0x000B4B87
			public bool IsNull
			{
				get
				{
					return this.Low == 0U && this.High == 0U;
				}
			}

			// Token: 0x04002969 RID: 10601
			public uint Low;

			// Token: 0x0400296A RID: 10602
			public uint High;

			// Token: 0x0400296B RID: 10603
			public static readonly _WinInetCache.FILETIME Zero = new _WinInetCache.FILETIME(0L);
		}

		// Token: 0x02000577 RID: 1399
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct EntryBuffer
		{
			// Token: 0x0400296C RID: 10604
			public static int MarshalSize = Marshal.SizeOf(typeof(_WinInetCache.EntryBuffer));

			// Token: 0x0400296D RID: 10605
			public int StructSize;

			// Token: 0x0400296E RID: 10606
			public IntPtr _OffsetSourceUrlName;

			// Token: 0x0400296F RID: 10607
			public IntPtr _OffsetFileName;

			// Token: 0x04002970 RID: 10608
			public _WinInetCache.EntryType EntryType;

			// Token: 0x04002971 RID: 10609
			public int UseCount;

			// Token: 0x04002972 RID: 10610
			public int HitRate;

			// Token: 0x04002973 RID: 10611
			public int SizeLow;

			// Token: 0x04002974 RID: 10612
			public int SizeHigh;

			// Token: 0x04002975 RID: 10613
			public _WinInetCache.FILETIME LastModifiedTime;

			// Token: 0x04002976 RID: 10614
			public _WinInetCache.FILETIME ExpireTime;

			// Token: 0x04002977 RID: 10615
			public _WinInetCache.FILETIME LastAccessTime;

			// Token: 0x04002978 RID: 10616
			public _WinInetCache.FILETIME LastSyncTime;

			// Token: 0x04002979 RID: 10617
			public IntPtr _OffsetHeaderInfo;

			// Token: 0x0400297A RID: 10618
			public int HeaderInfoChars;

			// Token: 0x0400297B RID: 10619
			public IntPtr _OffsetExtension;

			// Token: 0x0400297C RID: 10620
			public _WinInetCache.EntryBuffer.Rsv U;

			// Token: 0x02000578 RID: 1400
			[StructLayout(LayoutKind.Explicit)]
			public struct Rsv
			{
				// Token: 0x0400297D RID: 10621
				[FieldOffset(0)]
				public int ExemptDelta;

				// Token: 0x0400297E RID: 10622
				[FieldOffset(0)]
				public int Reserved;
			}
		}

		// Token: 0x02000579 RID: 1401
		internal class Entry
		{
			// Token: 0x06002ABB RID: 10939 RVA: 0x000B5BC0 File Offset: 0x000B4BC0
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

			// Token: 0x0400297F RID: 10623
			public const int DefaultBufferSize = 2048;

			// Token: 0x04002980 RID: 10624
			public _WinInetCache.Status Error;

			// Token: 0x04002981 RID: 10625
			public string Key;

			// Token: 0x04002982 RID: 10626
			public string Filename;

			// Token: 0x04002983 RID: 10627
			public string FileExt;

			// Token: 0x04002984 RID: 10628
			public int OptionalLength;

			// Token: 0x04002985 RID: 10629
			public string OriginalUrl;

			// Token: 0x04002986 RID: 10630
			public string MetaInfo;

			// Token: 0x04002987 RID: 10631
			public int MaxBufferBytes;

			// Token: 0x04002988 RID: 10632
			public _WinInetCache.EntryBuffer Info;
		}
	}
}
