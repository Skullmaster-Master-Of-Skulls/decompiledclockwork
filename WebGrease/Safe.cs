using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using WebGrease.Extensions;

namespace WebGrease
{
	// Token: 0x02000109 RID: 265
	internal sealed class Safe : IDisposable
	{
		// Token: 0x060010B4 RID: 4276 RVA: 0x0004A5CC File Offset: 0x000487CC
		public Safe(object[] padlockObjects, int millisecondTimeout)
		{
			this.padlocks = padlockObjects;
			this.securedFlags = new bool[this.padlocks.Length];
			for (int i = 0; i < this.padlocks.Length; i++)
			{
				this.securedFlags[i] = Monitor.TryEnter(this.padlocks[i], millisecondTimeout);
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0004A624 File Offset: 0x00048824
		private Safe(object padlockObject, int milliSecondTimeout) : this(new object[]
		{
			padlockObject
		}, milliSecondTimeout)
		{
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x0004A647 File Offset: 0x00048847
		private bool Secured
		{
			get
			{
				return this.securedFlags.All((bool s) => s);
			}
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0004A674 File Offset: 0x00048874
		public void Dispose()
		{
			for (int i = 0; i < this.securedFlags.Length; i++)
			{
				if (this.securedFlags[i])
				{
					Monitor.Exit(this.padlocks[i]);
					this.securedFlags[i] = false;
				}
			}
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0004A6B4 File Offset: 0x000488B4
		internal static void Lock(object padlock, Action action)
		{
			Safe.Lock(padlock, 5000, action);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0004A6C2 File Offset: 0x000488C2
		internal static void FileLock(FileSystemInfo fileInfo, Action fileAction)
		{
			Safe.FileLock(fileInfo, 5000, fileAction);
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0004A758 File Offset: 0x00048958
		internal static void LockFiles(IEnumerable<FileInfo> fileInfoItems, Action fileAction)
		{
			List<object> uniqueKeyLocks = new List<object>();
			Safe.Lock(Safe.UniqueKeyLocks, delegate()
			{
				foreach (FileInfo fileInfo in fileInfoItems)
				{
					string key = fileInfo.FullName.ToUpperInvariant();
					object item;
					if (!Safe.UniqueKeyLocks.TryGetValue(key, out item))
					{
						Safe.UniqueKeyLocks.Add(key, item = new object());
					}
					uniqueKeyLocks.Add(item);
				}
			});
			Safe.Lock(uniqueKeyLocks.ToArray(), int.MaxValue, fileAction);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0004A7AC File Offset: 0x000489AC
		internal static void FileLock(FileSystemInfo fileInfo, int millisecondTimeout, Action fileAction)
		{
			string uniqueKey = fileInfo.FullName.ToUpperInvariant();
			Safe.UniqueKeyLock(uniqueKey, millisecondTimeout, fileAction);
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0004A81C File Offset: 0x00048A1C
		internal static void UniqueKeyLock(string uniqueKey, int millisecondTimeout, Action fileAction)
		{
			object uniqueKeyLock = null;
			Safe.Lock(Safe.UniqueKeyLocks, delegate()
			{
				if (!Safe.UniqueKeyLocks.TryGetValue(uniqueKey, out uniqueKeyLock))
				{
					Safe.UniqueKeyLocks.Add(uniqueKey, uniqueKeyLock = new object());
				}
			});
			Safe.Lock(uniqueKeyLock, millisecondTimeout, fileAction);
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0004A860 File Offset: 0x00048A60
		internal static TResult Lock<TResult>(object padlock, Func<TResult> action)
		{
			return Safe.Lock<TResult>(padlock, 5000, action);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0004A870 File Offset: 0x00048A70
		internal static TResult Lock<TResult>(object padlock, int millisecondTimeout, Func<TResult> action)
		{
			TResult result;
			using (Safe safe = new Safe(padlock, millisecondTimeout))
			{
				if (!safe.Secured)
				{
					throw new TimeoutException(ResourceStrings.SafeLockFailedMessage.InvariantFormat(new object[]
					{
						millisecondTimeout
					}));
				}
				result = action();
			}
			return result;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0004A8D4 File Offset: 0x00048AD4
		internal static void Lock(object[] padlocks, int millisecondTimeout, Action action)
		{
			using (Safe safe = new Safe(padlocks, millisecondTimeout))
			{
				if (!safe.Secured)
				{
					throw new TimeoutException(ResourceStrings.SafeLockFailedMessage.InvariantFormat(new object[]
					{
						millisecondTimeout
					}));
				}
				action();
			}
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0004A938 File Offset: 0x00048B38
		internal static void Lock(object padlock, int millisecondTimeout, Action action)
		{
			using (Safe safe = new Safe(padlock, millisecondTimeout))
			{
				if (!safe.Secured)
				{
					throw new TimeoutException(ResourceStrings.SafeLockFailedMessage.InvariantFormat(new object[]
					{
						millisecondTimeout
					}));
				}
				action();
			}
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0004A99C File Offset: 0x00048B9C
		internal static bool WriteToFileStream(string filePath, Action<FileStream> action)
		{
			return Safe.WriteToFileStream(filePath, 10, 500, action);
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0004A9AC File Offset: 0x00048BAC
		private static bool WriteToFileStream(string fullPath, int maxRetries, int millisecondsTimeoutBetweenTries, Action<FileStream> action)
		{
			int num = 0;
			bool result;
			for (;;)
			{
				num++;
				try
				{
					using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
					{
						fileStream.ReadByte();
						fileStream.Seek(0L, SeekOrigin.Begin);
						action(fileStream);
						result = true;
					}
				}
				catch (Exception)
				{
					if (num != maxRetries)
					{
						Thread.Sleep(millisecondsTimeoutBetweenTries);
						continue;
					}
					result = false;
				}
				break;
			}
			return result;
		}

		// Token: 0x04000687 RID: 1671
		internal const int DefaultLockTimeout = 5000;

		// Token: 0x04000688 RID: 1672
		internal const int MaxLockTimeout = 2147483647;

		// Token: 0x04000689 RID: 1673
		private static readonly IDictionary<string, object> UniqueKeyLocks = new Dictionary<string, object>();

		// Token: 0x0400068A RID: 1674
		private readonly bool[] securedFlags;

		// Token: 0x0400068B RID: 1675
		private readonly object[] padlocks;
	}
}
