using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	// Token: 0x0200072B RID: 1835
	[DefaultEvent("Changed")]
	[IODescription("FileSystemWatcherDesc")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class FileSystemWatcher : Component, ISupportInitialize
	{
		// Token: 0x06003801 RID: 14337 RVA: 0x000EC76C File Offset: 0x000EB76C
		static FileSystemWatcher()
		{
			foreach (object obj in Enum.GetValues(typeof(NotifyFilters)))
			{
				int num = (int)obj;
				FileSystemWatcher.notifyFiltersValidMask |= num;
			}
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x000EC7F0 File Offset: 0x000EB7F0
		public FileSystemWatcher()
		{
			this.directory = string.Empty;
			this.filter = "*.*";
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x000EC821 File Offset: 0x000EB821
		public FileSystemWatcher(string path) : this(path, "*.*")
		{
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x000EC830 File Offset: 0x000EB830
		public FileSystemWatcher(string path, string filter)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			if (path.Length == 0 || !Directory.Exists(path))
			{
				throw new ArgumentException(SR.GetString("InvalidDirName", new object[]
				{
					path
				}));
			}
			this.directory = path;
			this.filter = filter;
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06003805 RID: 14341 RVA: 0x000EC8AC File Offset: 0x000EB8AC
		// (set) Token: 0x06003806 RID: 14342 RVA: 0x000EC8B4 File Offset: 0x000EB8B4
		[IODescription("FSW_ChangedFilter")]
		[DefaultValue(NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite)]
		public NotifyFilters NotifyFilter
		{
			get
			{
				return this.notifyFilters;
			}
			set
			{
				if ((value & (NotifyFilters)(~(NotifyFilters)FileSystemWatcher.notifyFiltersValidMask)) != (NotifyFilters)0)
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(NotifyFilters));
				}
				if (this.notifyFilters != value)
				{
					this.notifyFilters = value;
					this.Restart();
				}
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06003807 RID: 14343 RVA: 0x000EC8EC File Offset: 0x000EB8EC
		// (set) Token: 0x06003808 RID: 14344 RVA: 0x000EC8F4 File Offset: 0x000EB8F4
		[DefaultValue(false)]
		[IODescription("FSW_Enabled")]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (this.enabled == value)
				{
					return;
				}
				this.enabled = value;
				if (!this.IsSuspended())
				{
					if (this.enabled)
					{
						this.StartRaisingEvents();
						return;
					}
					this.StopRaisingEvents();
				}
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06003809 RID: 14345 RVA: 0x000EC924 File Offset: 0x000EB924
		// (set) Token: 0x0600380A RID: 14346 RVA: 0x000EC92C File Offset: 0x000EB92C
		[DefaultValue("*.*")]
		[RecommendedAsConfigurable(true)]
		[IODescription("FSW_Filter")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					value = "*.*";
				}
				if (string.Compare(this.filter, value, StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.filter = value;
				}
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x0600380B RID: 14347 RVA: 0x000EC95B File Offset: 0x000EB95B
		// (set) Token: 0x0600380C RID: 14348 RVA: 0x000EC963 File Offset: 0x000EB963
		[DefaultValue(false)]
		[IODescription("FSW_IncludeSubdirectories")]
		public bool IncludeSubdirectories
		{
			get
			{
				return this.includeSubdirectories;
			}
			set
			{
				if (this.includeSubdirectories != value)
				{
					this.includeSubdirectories = value;
					this.Restart();
				}
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x0600380D RID: 14349 RVA: 0x000EC97B File Offset: 0x000EB97B
		// (set) Token: 0x0600380E RID: 14350 RVA: 0x000EC983 File Offset: 0x000EB983
		[Browsable(false)]
		[DefaultValue(8192)]
		public int InternalBufferSize
		{
			get
			{
				return this.internalBufferSize;
			}
			set
			{
				if (this.internalBufferSize != value)
				{
					if (value < 4096)
					{
						value = 4096;
					}
					this.internalBufferSize = value;
					this.Restart();
				}
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x0600380F RID: 14351 RVA: 0x000EC9AA File Offset: 0x000EB9AA
		private bool IsHandleInvalid
		{
			get
			{
				return this.directoryHandle == null || this.directoryHandle.IsInvalid;
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06003810 RID: 14352 RVA: 0x000EC9C1 File Offset: 0x000EB9C1
		// (set) Token: 0x06003811 RID: 14353 RVA: 0x000EC9CC File Offset: 0x000EB9CC
		[RecommendedAsConfigurable(true)]
		[DefaultValue("")]
		[Editor("System.Diagnostics.Design.FSWPathEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[IODescription("FSW_Path")]
		public string Path
		{
			get
			{
				return this.directory;
			}
			set
			{
				value = ((value == null) ? string.Empty : value);
				if (string.Compare(this.directory, value, StringComparison.OrdinalIgnoreCase) != 0)
				{
					if (base.DesignMode)
					{
						if (value.IndexOfAny(FileSystemWatcher.wildcards) != -1 || value.IndexOfAny(System.IO.Path.GetInvalidPathChars()) != -1)
						{
							throw new ArgumentException(SR.GetString("InvalidDirName", new object[]
							{
								value
							}));
						}
					}
					else if (!Directory.Exists(value))
					{
						throw new ArgumentException(SR.GetString("InvalidDirName", new object[]
						{
							value
						}));
					}
					this.directory = value;
					this.readGranted = false;
					this.Restart();
				}
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06003812 RID: 14354 RVA: 0x000ECA6D File Offset: 0x000EBA6D
		// (set) Token: 0x06003813 RID: 14355 RVA: 0x000ECA75 File Offset: 0x000EBA75
		[Browsable(false)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
				if (this.Site != null && this.Site.DesignMode)
				{
					this.EnableRaisingEvents = true;
				}
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06003814 RID: 14356 RVA: 0x000ECA9C File Offset: 0x000EBA9C
		// (set) Token: 0x06003815 RID: 14357 RVA: 0x000ECAF6 File Offset: 0x000EBAF6
		[IODescription("FSW_SynchronizingObject")]
		[DefaultValue(null)]
		[Browsable(false)]
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				if (this.synchronizingObject == null && base.DesignMode)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						object rootComponent = designerHost.RootComponent;
						if (rootComponent != null && rootComponent is ISynchronizeInvoke)
						{
							this.synchronizingObject = (ISynchronizeInvoke)rootComponent;
						}
					}
				}
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06003816 RID: 14358 RVA: 0x000ECAFF File Offset: 0x000EBAFF
		// (remove) Token: 0x06003817 RID: 14359 RVA: 0x000ECB18 File Offset: 0x000EBB18
		[IODescription("FSW_Changed")]
		public event FileSystemEventHandler Changed
		{
			add
			{
				this.onChangedHandler = (FileSystemEventHandler)Delegate.Combine(this.onChangedHandler, value);
			}
			remove
			{
				this.onChangedHandler = (FileSystemEventHandler)Delegate.Remove(this.onChangedHandler, value);
			}
		}

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06003818 RID: 14360 RVA: 0x000ECB31 File Offset: 0x000EBB31
		// (remove) Token: 0x06003819 RID: 14361 RVA: 0x000ECB4A File Offset: 0x000EBB4A
		[IODescription("FSW_Created")]
		public event FileSystemEventHandler Created
		{
			add
			{
				this.onCreatedHandler = (FileSystemEventHandler)Delegate.Combine(this.onCreatedHandler, value);
			}
			remove
			{
				this.onCreatedHandler = (FileSystemEventHandler)Delegate.Remove(this.onCreatedHandler, value);
			}
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x0600381A RID: 14362 RVA: 0x000ECB63 File Offset: 0x000EBB63
		// (remove) Token: 0x0600381B RID: 14363 RVA: 0x000ECB7C File Offset: 0x000EBB7C
		[IODescription("FSW_Deleted")]
		public event FileSystemEventHandler Deleted
		{
			add
			{
				this.onDeletedHandler = (FileSystemEventHandler)Delegate.Combine(this.onDeletedHandler, value);
			}
			remove
			{
				this.onDeletedHandler = (FileSystemEventHandler)Delegate.Remove(this.onDeletedHandler, value);
			}
		}

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x0600381C RID: 14364 RVA: 0x000ECB95 File Offset: 0x000EBB95
		// (remove) Token: 0x0600381D RID: 14365 RVA: 0x000ECBAE File Offset: 0x000EBBAE
		[Browsable(false)]
		public event ErrorEventHandler Error
		{
			add
			{
				this.onErrorHandler = (ErrorEventHandler)Delegate.Combine(this.onErrorHandler, value);
			}
			remove
			{
				this.onErrorHandler = (ErrorEventHandler)Delegate.Remove(this.onErrorHandler, value);
			}
		}

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x0600381E RID: 14366 RVA: 0x000ECBC7 File Offset: 0x000EBBC7
		// (remove) Token: 0x0600381F RID: 14367 RVA: 0x000ECBE0 File Offset: 0x000EBBE0
		[IODescription("FSW_Renamed")]
		public event RenamedEventHandler Renamed
		{
			add
			{
				this.onRenamedHandler = (RenamedEventHandler)Delegate.Combine(this.onRenamedHandler, value);
			}
			remove
			{
				this.onRenamedHandler = (RenamedEventHandler)Delegate.Remove(this.onRenamedHandler, value);
			}
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x000ECBFC File Offset: 0x000EBBFC
		public void BeginInit()
		{
			bool flag = this.enabled;
			this.StopRaisingEvents();
			this.enabled = flag;
			this.initializing = true;
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x000ECC24 File Offset: 0x000EBC24
		private unsafe void CompletionStatusChanged(uint errorCode, uint numBytes, NativeOverlapped* overlappedPointer)
		{
			Overlapped overlapped = Overlapped.Unpack(overlappedPointer);
			ulong num = (ulong)((ulong)((long)overlapped.OffsetHigh) << 32);
			num |= (ulong)overlapped.OffsetLow;
			IntPtr intPtr = (IntPtr)((long)num);
			FileSystemWatcher.FSWAsyncResult fswasyncResult = (FileSystemWatcher.FSWAsyncResult)overlapped.AsyncResult;
			try
			{
				if (!this.stopListening)
				{
					lock (this)
					{
						if (errorCode != 0U)
						{
							if (errorCode != 995U)
							{
								this.OnError(new ErrorEventArgs(new Win32Exception((int)errorCode)));
								this.EnableRaisingEvents = false;
							}
						}
						else if (fswasyncResult.session == this.currentSession)
						{
							if (numBytes == 0U)
							{
								this.NotifyInternalBufferOverflowEvent();
							}
							else
							{
								int num2 = 0;
								string text = null;
								int num3;
								do
								{
									num3 = Marshal.ReadInt32((IntPtr)((long)intPtr + (long)num2));
									int num4 = Marshal.ReadInt32((IntPtr)((long)intPtr + (long)num2 + 4L));
									int num5 = Marshal.ReadInt32((IntPtr)((long)intPtr + (long)num2 + 8L));
									string text2 = Marshal.PtrToStringUni((IntPtr)((long)intPtr + (long)num2 + 12L), num5 / 2);
									if (num4 == 4)
									{
										text = text2;
									}
									else if (num4 == 5)
									{
										if (text != null)
										{
											this.NotifyRenameEventArgs(WatcherChangeTypes.Renamed, text2, text);
											text = null;
										}
										else
										{
											this.NotifyRenameEventArgs(WatcherChangeTypes.Renamed, text2, text);
											text = null;
										}
									}
									else
									{
										if (text != null)
										{
											this.NotifyRenameEventArgs(WatcherChangeTypes.Renamed, null, text);
											text = null;
										}
										this.NotifyFileSystemEventArgs(num4, text2);
									}
									num2 += num3;
								}
								while (num3 != 0);
								if (text != null)
								{
									this.NotifyRenameEventArgs(WatcherChangeTypes.Renamed, null, text);
								}
							}
						}
					}
				}
			}
			finally
			{
				Overlapped.Free(overlappedPointer);
				if (this.stopListening || this.runOnce)
				{
					if (intPtr != (IntPtr)0)
					{
						Marshal.FreeHGlobal(intPtr);
					}
				}
				else
				{
					this.Monitor(intPtr);
				}
			}
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x000ECE14 File Offset: 0x000EBE14
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.StopRaisingEvents();
				this.onChangedHandler = null;
				this.onCreatedHandler = null;
				this.onDeletedHandler = null;
				this.onRenamedHandler = null;
				this.onErrorHandler = null;
				this.readGranted = false;
			}
			else
			{
				this.stopListening = true;
				if (!this.IsHandleInvalid)
				{
					this.directoryHandle.Close();
				}
			}
			this.disposed = true;
			base.Dispose(disposing);
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x000ECE7E File Offset: 0x000EBE7E
		public void EndInit()
		{
			this.initializing = false;
			if (this.directory.Length != 0 && this.enabled)
			{
				this.StartRaisingEvents();
			}
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x000ECEA2 File Offset: 0x000EBEA2
		private bool IsSuspended()
		{
			return this.initializing || base.DesignMode;
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x000ECEB4 File Offset: 0x000EBEB4
		private bool MatchPattern(string relativePath)
		{
			string fileName = System.IO.Path.GetFileName(relativePath);
			return fileName != null && PatternMatcher.StrictMatchPattern(this.filter.ToUpper(CultureInfo.InvariantCulture), fileName.ToUpper(CultureInfo.InvariantCulture));
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x000ECEF0 File Offset: 0x000EBEF0
		private unsafe void Monitor(IntPtr bufferPtr)
		{
			if (!this.enabled || this.IsHandleInvalid)
			{
				return;
			}
			Overlapped overlapped = new Overlapped();
			if (bufferPtr == (IntPtr)0)
			{
				try
				{
					bufferPtr = Marshal.AllocHGlobal(this.internalBufferSize);
				}
				catch (OutOfMemoryException)
				{
					throw new OutOfMemoryException(SR.GetString("BufferSizeTooLarge", new object[]
					{
						this.internalBufferSize.ToString(CultureInfo.CurrentCulture)
					}));
				}
			}
			ulong num = (ulong)((long)bufferPtr);
			overlapped.OffsetHigh = (int)(num >> 32);
			overlapped.OffsetLow = (int)num;
			overlapped.AsyncResult = new FileSystemWatcher.FSWAsyncResult
			{
				session = this.currentSession
			};
			NativeOverlapped* ptr = overlapped.Pack(new IOCompletionCallback(this.CompletionStatusChanged), this.currentSession);
			bool flag = false;
			try
			{
				if (!this.IsHandleInvalid)
				{
					int num2;
					flag = UnsafeNativeMethods.ReadDirectoryChangesW(this.directoryHandle, new HandleRef(this, bufferPtr), this.internalBufferSize, this.includeSubdirectories ? 1 : 0, (int)this.notifyFilters, out num2, ptr, NativeMethods.NullHandleRef);
				}
			}
			catch (ObjectDisposedException)
			{
			}
			catch (ArgumentNullException)
			{
			}
			finally
			{
				if (!flag)
				{
					Overlapped.Free(ptr);
					Marshal.FreeHGlobal(bufferPtr);
					if (!this.IsHandleInvalid)
					{
						this.OnError(new ErrorEventArgs(new Win32Exception()));
					}
				}
			}
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x000ED058 File Offset: 0x000EC058
		private void NotifyFileSystemEventArgs(int action, string name)
		{
			if (!this.MatchPattern(name))
			{
				return;
			}
			switch (action)
			{
			case 1:
				this.OnCreated(new FileSystemEventArgs(WatcherChangeTypes.Created, this.directory, name));
				return;
			case 2:
				this.OnDeleted(new FileSystemEventArgs(WatcherChangeTypes.Deleted, this.directory, name));
				return;
			case 3:
				this.OnChanged(new FileSystemEventArgs(WatcherChangeTypes.Changed, this.directory, name));
				return;
			default:
				return;
			}
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x000ED0C4 File Offset: 0x000EC0C4
		private void NotifyInternalBufferOverflowEvent()
		{
			InternalBufferOverflowException exception = new InternalBufferOverflowException(SR.GetString("FSW_BufferOverflow", new object[]
			{
				this.directory
			}));
			ErrorEventArgs e = new ErrorEventArgs(exception);
			this.OnError(e);
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x000ED100 File Offset: 0x000EC100
		private void NotifyRenameEventArgs(WatcherChangeTypes action, string name, string oldName)
		{
			if (!this.MatchPattern(name) && !this.MatchPattern(oldName))
			{
				return;
			}
			RenamedEventArgs e = new RenamedEventArgs(action, this.directory, name, oldName);
			this.OnRenamed(e);
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x000ED138 File Offset: 0x000EC138
		protected void OnChanged(FileSystemEventArgs e)
		{
			FileSystemEventHandler fileSystemEventHandler = this.onChangedHandler;
			if (fileSystemEventHandler != null)
			{
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.BeginInvoke(fileSystemEventHandler, new object[]
					{
						this,
						e
					});
					return;
				}
				fileSystemEventHandler(this, e);
			}
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x000ED18C File Offset: 0x000EC18C
		protected void OnCreated(FileSystemEventArgs e)
		{
			FileSystemEventHandler fileSystemEventHandler = this.onCreatedHandler;
			if (fileSystemEventHandler != null)
			{
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.BeginInvoke(fileSystemEventHandler, new object[]
					{
						this,
						e
					});
					return;
				}
				fileSystemEventHandler(this, e);
			}
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x000ED1E0 File Offset: 0x000EC1E0
		protected void OnDeleted(FileSystemEventArgs e)
		{
			FileSystemEventHandler fileSystemEventHandler = this.onDeletedHandler;
			if (fileSystemEventHandler != null)
			{
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.BeginInvoke(fileSystemEventHandler, new object[]
					{
						this,
						e
					});
					return;
				}
				fileSystemEventHandler(this, e);
			}
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x000ED234 File Offset: 0x000EC234
		protected void OnError(ErrorEventArgs e)
		{
			ErrorEventHandler errorEventHandler = this.onErrorHandler;
			if (errorEventHandler != null)
			{
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.BeginInvoke(errorEventHandler, new object[]
					{
						this,
						e
					});
					return;
				}
				errorEventHandler(this, e);
			}
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x000ED288 File Offset: 0x000EC288
		private void OnInternalFileSystemEventArgs(object sender, FileSystemEventArgs e)
		{
			lock (this)
			{
				if (!this.isChanged)
				{
					this.changedResult = new WaitForChangedResult(e.ChangeType, e.Name, false);
					this.isChanged = true;
					System.Threading.Monitor.Pulse(this);
				}
			}
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x000ED2E4 File Offset: 0x000EC2E4
		private void OnInternalRenameEventArgs(object sender, RenamedEventArgs e)
		{
			lock (this)
			{
				if (!this.isChanged)
				{
					this.changedResult = new WaitForChangedResult(e.ChangeType, e.Name, e.OldName, false);
					this.isChanged = true;
					System.Threading.Monitor.Pulse(this);
				}
			}
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x000ED348 File Offset: 0x000EC348
		protected void OnRenamed(RenamedEventArgs e)
		{
			RenamedEventHandler renamedEventHandler = this.onRenamedHandler;
			if (renamedEventHandler != null)
			{
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.BeginInvoke(renamedEventHandler, new object[]
					{
						this,
						e
					});
					return;
				}
				renamedEventHandler(this, e);
			}
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x000ED39A File Offset: 0x000EC39A
		private void Restart()
		{
			if (!this.IsSuspended() && this.enabled)
			{
				this.StopRaisingEvents();
				this.StartRaisingEvents();
			}
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x000ED3B8 File Offset: 0x000EC3B8
		private void StartRaisingEvents()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			try
			{
				new EnvironmentPermission(PermissionState.Unrestricted).Assert();
				if (Environment.OSVersion.Platform != PlatformID.Win32NT)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinNTRequired"));
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			if (this.IsSuspended())
			{
				this.enabled = true;
				return;
			}
			if (!this.readGranted)
			{
				string fullPath = System.IO.Path.GetFullPath(this.directory);
				FileIOPermission fileIOPermission = new FileIOPermission(FileIOPermissionAccess.Read, fullPath);
				fileIOPermission.Demand();
				this.readGranted = true;
			}
			if (!this.IsHandleInvalid)
			{
				return;
			}
			this.directoryHandle = NativeMethods.CreateFile(this.directory, 1, 7, null, 3, 1107296256, new SafeFileHandle(IntPtr.Zero, false));
			if (this.IsHandleInvalid)
			{
				throw new FileNotFoundException(SR.GetString("FSW_IOError", new object[]
				{
					this.directory
				}));
			}
			this.stopListening = false;
			Interlocked.Increment(ref this.currentSession);
			SecurityPermission securityPermission = new SecurityPermission(PermissionState.Unrestricted);
			securityPermission.Assert();
			try
			{
				ThreadPool.BindHandle(this.directoryHandle);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			this.enabled = true;
			this.Monitor((IntPtr)0);
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x000ED504 File Offset: 0x000EC504
		private void StopRaisingEvents()
		{
			if (this.IsSuspended())
			{
				this.enabled = false;
				return;
			}
			if (this.IsHandleInvalid)
			{
				return;
			}
			this.stopListening = true;
			this.directoryHandle.Close();
			this.directoryHandle = null;
			Interlocked.Increment(ref this.currentSession);
			this.enabled = false;
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x000ED556 File Offset: 0x000EC556
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType)
		{
			return this.WaitForChanged(changeType, -1);
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x000ED560 File Offset: 0x000EC560
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType, int timeout)
		{
			FileSystemEventHandler value = new FileSystemEventHandler(this.OnInternalFileSystemEventArgs);
			RenamedEventHandler value2 = new RenamedEventHandler(this.OnInternalRenameEventArgs);
			this.isChanged = false;
			this.changedResult = WaitForChangedResult.TimedOutResult;
			if ((changeType & WatcherChangeTypes.Created) != (WatcherChangeTypes)0)
			{
				this.Created += value;
			}
			if ((changeType & WatcherChangeTypes.Deleted) != (WatcherChangeTypes)0)
			{
				this.Deleted += value;
			}
			if ((changeType & WatcherChangeTypes.Changed) != (WatcherChangeTypes)0)
			{
				this.Changed += value;
			}
			if ((changeType & WatcherChangeTypes.Renamed) != (WatcherChangeTypes)0)
			{
				this.Renamed += value2;
			}
			bool enableRaisingEvents = this.EnableRaisingEvents;
			if (!enableRaisingEvents)
			{
				this.runOnce = true;
				this.EnableRaisingEvents = true;
			}
			WaitForChangedResult timedOutResult = WaitForChangedResult.TimedOutResult;
			lock (this)
			{
				if (timeout == -1)
				{
					while (!this.isChanged)
					{
						System.Threading.Monitor.Wait(this);
					}
				}
				else
				{
					System.Threading.Monitor.Wait(this, timeout, true);
				}
				timedOutResult = this.changedResult;
			}
			this.EnableRaisingEvents = enableRaisingEvents;
			this.runOnce = false;
			if ((changeType & WatcherChangeTypes.Created) != (WatcherChangeTypes)0)
			{
				this.Created -= value;
			}
			if ((changeType & WatcherChangeTypes.Deleted) != (WatcherChangeTypes)0)
			{
				this.Deleted -= value;
			}
			if ((changeType & WatcherChangeTypes.Changed) != (WatcherChangeTypes)0)
			{
				this.Changed -= value;
			}
			if ((changeType & WatcherChangeTypes.Renamed) != (WatcherChangeTypes)0)
			{
				this.Renamed -= value2;
			}
			return timedOutResult;
		}

		// Token: 0x04003209 RID: 12809
		private const NotifyFilters defaultNotifyFilters = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;

		// Token: 0x0400320A RID: 12810
		private string directory;

		// Token: 0x0400320B RID: 12811
		private string filter;

		// Token: 0x0400320C RID: 12812
		private SafeFileHandle directoryHandle;

		// Token: 0x0400320D RID: 12813
		private NotifyFilters notifyFilters = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;

		// Token: 0x0400320E RID: 12814
		private bool includeSubdirectories;

		// Token: 0x0400320F RID: 12815
		private bool enabled;

		// Token: 0x04003210 RID: 12816
		private bool initializing;

		// Token: 0x04003211 RID: 12817
		private int internalBufferSize = 8192;

		// Token: 0x04003212 RID: 12818
		private WaitForChangedResult changedResult;

		// Token: 0x04003213 RID: 12819
		private bool isChanged;

		// Token: 0x04003214 RID: 12820
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x04003215 RID: 12821
		private bool readGranted;

		// Token: 0x04003216 RID: 12822
		private bool disposed;

		// Token: 0x04003217 RID: 12823
		private int currentSession;

		// Token: 0x04003218 RID: 12824
		private FileSystemEventHandler onChangedHandler;

		// Token: 0x04003219 RID: 12825
		private FileSystemEventHandler onCreatedHandler;

		// Token: 0x0400321A RID: 12826
		private FileSystemEventHandler onDeletedHandler;

		// Token: 0x0400321B RID: 12827
		private RenamedEventHandler onRenamedHandler;

		// Token: 0x0400321C RID: 12828
		private ErrorEventHandler onErrorHandler;

		// Token: 0x0400321D RID: 12829
		private bool stopListening;

		// Token: 0x0400321E RID: 12830
		private bool runOnce;

		// Token: 0x0400321F RID: 12831
		private static readonly char[] wildcards = new char[]
		{
			'?',
			'*'
		};

		// Token: 0x04003220 RID: 12832
		private static int notifyFiltersValidMask = 0;

		// Token: 0x0200072C RID: 1836
		private sealed class FSWAsyncResult : IAsyncResult
		{
			// Token: 0x17000D0B RID: 3339
			// (get) Token: 0x06003836 RID: 14390 RVA: 0x000ED670 File Offset: 0x000EC670
			public bool IsCompleted
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000D0C RID: 3340
			// (get) Token: 0x06003837 RID: 14391 RVA: 0x000ED677 File Offset: 0x000EC677
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000D0D RID: 3341
			// (get) Token: 0x06003838 RID: 14392 RVA: 0x000ED67E File Offset: 0x000EC67E
			public object AsyncState
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000D0E RID: 3342
			// (get) Token: 0x06003839 RID: 14393 RVA: 0x000ED685 File Offset: 0x000EC685
			public bool CompletedSynchronously
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x04003221 RID: 12833
			internal int session;
		}
	}
}
