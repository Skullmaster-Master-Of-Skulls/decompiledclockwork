using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000832 RID: 2098
	internal sealed class CompilationMutex : IDisposable
	{
		// Token: 0x0600640F RID: 25615 RVA: 0x0015EEC8 File Offset: 0x0015D0C8
		internal CompilationMutex(string name, string comment)
		{
			string text = (string)Misc.GetAspNetRegValue("CompilationMutexName", null, null);
			if (text != null)
			{
				this._name = string.Concat(new string[]
				{
					this._name,
					"Global\\",
					name,
					"-",
					text
				});
			}
			else
			{
				this._name = this._name + "Local\\" + name;
			}
			this._comment = comment;
			this._mutexHandle = new HandleRef(this, UnsafeNativeMethods.InstrumentedMutexCreate(this._name));
			if (this._mutexHandle.Handle == IntPtr.Zero)
			{
				throw new InvalidOperationException(SR.GetString("CompilationMutex_Create"));
			}
		}

		// Token: 0x06006410 RID: 25616 RVA: 0x0015EF80 File Offset: 0x0015D180
		~CompilationMutex()
		{
			this.Close();
		}

		// Token: 0x06006411 RID: 25617 RVA: 0x0015EFAC File Offset: 0x0015D1AC
		void IDisposable.Dispose()
		{
			this.Close();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06006412 RID: 25618 RVA: 0x0015EFBA File Offset: 0x0015D1BA
		internal void Close()
		{
			if (this._mutexHandle.Handle != IntPtr.Zero)
			{
				UnsafeNativeMethods.InstrumentedMutexDelete(this._mutexHandle);
				this._mutexHandle = new HandleRef(this, IntPtr.Zero);
			}
		}

		// Token: 0x06006413 RID: 25619 RVA: 0x0015EFF0 File Offset: 0x0015D1F0
		internal void WaitOne()
		{
			if (this._mutexHandle.Handle == IntPtr.Zero)
			{
				throw new InvalidOperationException(SR.GetString("CompilationMutex_Null"));
			}
			for (;;)
			{
				int lockStatus = this._lockStatus;
				if (lockStatus == -1 || this._draining)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this._lockStatus, lockStatus + 1, lockStatus) == lockStatus)
				{
					goto Block_3;
				}
			}
			throw new InvalidOperationException(SR.GetString("CompilationMutex_Drained"));
			Block_3:
			if (UnsafeNativeMethods.InstrumentedMutexGetLock(this._mutexHandle, -1) == -1)
			{
				Interlocked.Decrement(ref this._lockStatus);
				throw new InvalidOperationException(SR.GetString("CompilationMutex_Failed"));
			}
		}

		// Token: 0x06006414 RID: 25620 RVA: 0x0015F084 File Offset: 0x0015D284
		internal void ReleaseMutex()
		{
			if (this._mutexHandle.Handle == IntPtr.Zero)
			{
				throw new InvalidOperationException(SR.GetString("CompilationMutex_Null"));
			}
			if (UnsafeNativeMethods.InstrumentedMutexReleaseLock(this._mutexHandle) != 0)
			{
				Interlocked.Decrement(ref this._lockStatus);
			}
		}

		// Token: 0x17001C3D RID: 7229
		// (get) Token: 0x06006415 RID: 25621 RVA: 0x0015F0D1 File Offset: 0x0015D2D1
		private string MutexDebugName
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x040033D1 RID: 13265
		private string _name;

		// Token: 0x040033D2 RID: 13266
		private string _comment;

		// Token: 0x040033D3 RID: 13267
		private HandleRef _mutexHandle;

		// Token: 0x040033D4 RID: 13268
		private int _lockStatus;

		// Token: 0x040033D5 RID: 13269
		private bool _draining;
	}
}
