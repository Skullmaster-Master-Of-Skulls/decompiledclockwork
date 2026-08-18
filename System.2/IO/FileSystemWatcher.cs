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
	// Token: 0x020003FF RID: 1023
	[DefaultEvent("Changed")]
	[IODescription("FileSystemWatcherDesc")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class FileSystemWatcher : Component, ISupportInitialize
	{
		// Token: 0x0600266C RID: 9836 RVA: 0x000B0FA8 File Offset: 0x000AF1A8
		static FileSystemWatcher()
		{
			foreach (object obj in Enum.GetValues(typeof(NotifyFilters)))
			{
				int num = (int)obj;
				FileSystemWatcher.notifyFiltersValidMask |= num;
			}
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x000B102C File Offset: 0x000AF22C
		public FileSystemWatcher()
		{
			this.directory = string.Empty;
			this.filter = "*.*";
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000B105D File Offset: 0x000AF25D
		public FileSystemWatcher(string path) : this(path, "*.*")
		{
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x000B106C File Offset: 0x000AF26C
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

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002670 RID: 9840 RVA: 0x000B10E6 File Offset: 0x000AF2E6
		// (set) Token: 0x06002671 RID: 9841 RVA: 0x000B10EE File Offset: 0x000AF2EE
		[DefaultValue(NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite)]
		[IODescription("FSW_ChangedFilter")]
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

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06002672 RID: 9842 RVA: 0x000B1126 File Offset: 0x000AF326
		// (set) Token: 0x06002673 RID: 9843 RVA: 0x000B112E File Offset: 0x000AF32E
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

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06002674 RID: 9844 RVA: 0x000B115E File Offset: 0x000AF35E
		// (set) Token: 0x06002675 RID: 9845 RVA: 0x000B1166 File Offset: 0x000AF366
		[DefaultValue("*.*")]
		[IODescription("FSW_Filter")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
		public string Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = "*.*";
				}
				if (string.Compare(this.filter, value, StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.filter = value;
				}
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06002676 RID: 9846 RVA: 0x000B118D File Offset: 0x000AF38D
		// (set) Token: 0x06002677 RID: 9847 RVA: 0x000B1195 File Offset: 0x000AF395
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

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06002678 RID: 9848 RVA: 0x000B11AD File Offset: 0x000AF3AD
		// (set) Token: 0x06002679 RID: 9849 RVA: 0x000B11B5 File Offset: 0x000AF3B5
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

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x0600267A RID: 9850 RVA: 0x000B11DC File Offset: 0x000AF3DC
		private bool IsHandleInvalid
		{
			get
			{
				return this.directoryHandle == null || this.directoryHandle.IsInvalid;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x000B11F3 File Offset: 0x000AF3F3
		// (set) Token: 0x0600267C RID: 9852 RVA: 0x000B11FC File Offset: 0x000AF3FC
		[DefaultValue("")]
		[IODescription("FSW_Path")]
		[Editor("System.Diagnostics.Design.FSWPathEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
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

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x0600267D RID: 9853 RVA: 0x000B1299 File Offset: 0x000AF499
		// (set) Token: 0x0600267E RID: 9854 RVA: 0x000B12A1 File Offset: 0x000AF4A1
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

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x000B12C8 File Offset: 0x000AF4C8
		// (set) Token: 0x06002680 RID: 9856 RVA: 0x000B1322 File Offset: 0x000AF522
		[Browsable(false)]
		[DefaultValue(null)]
		[IODescription("FSW_SynchronizingObject")]
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

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06002681 RID: 9857 RVA: 0x000B132B File Offset: 0x000AF52B
		// (remove) Token: 0x06002682 RID: 9858 RVA: 0x000B1344 File Offset: 0x000AF544
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

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06002683 RID: 9859 RVA: 0x000B135D File Offset: 0x000AF55D
		// (remove) Token: 0x06002684 RID: 9860 RVA: 0x000B1376 File Offset: 0x000AF576
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

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06002685 RID: 9861 RVA: 0x000B138F File Offset: 0x000AF58F
		// (remove) Token: 0x06002686 RID: 9862 RVA: 0x000B13A8 File Offset: 0x000AF5A8
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

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06002687 RID: 9863 RVA: 0x000B13C1 File Offset: 0x000AF5C1
		// (remove) Token: 0x06002688 RID: 9864 RVA: 0x000B13DA File Offset: 0x000AF5DA
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

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06002689 RID: 9865 RVA: 0x000B13F3 File Offset: 0x000AF5F3
		// (remove) Token: 0x0600268A RID: 9866 RVA: 0x000B140C File Offset: 0x000AF60C
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

		// Token: 0x0600268B RID: 9867 RVA: 0x000B1428 File Offset: 0x000AF628
		public void BeginInit()
		{
			bool flag = this.enabled;
			this.StopRaisingEvents();
			this.enabled = flag;
			this.initializing = true;
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x000B1450 File Offset: 0x000AF650
		private unsafe void CompletionStatusChanged(uint errorCode, uint numBytes, NativeOverlapped* overlappedPointer)
		{
			Overlapped overlapped = Overlapped.Unpack(overlappedPointer);
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
								int num = 0;
								string text = null;
								string text2 = null;
								int num2;
								do
								{
									int num3;
									try
									{
										byte[] array;
										byte* ptr;
										if ((array = fswasyncResult.buffer) == null || array.Length == 0)
										{
											ptr = null;
										}
										else
										{
											ptr = &array[0];
										}
										num2 = *(int*)(ptr + num);
										num3 = *(int*)(ptr + num + 4);
										int num4 = *(int*)(ptr + num + 8);
										text2 = new string((char*)(ptr + num + 12), 0, num4 / 2);
									}
									finally
									{
										byte[] array = null;
									}
									if (num3 == 4)
									{
										text = text2;
									}
									else if (num3 == 5)
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
										this.NotifyFileSystemEventArgs(num3, text2);
									}
									num += num2;
								}
								while (num2 != 0);
								if (text != null)
								{
									this.NotifyRenameEventArgs(WatcherChangeTypes.Renamed, null, text);
									text = null;
								}
							}
						}
					}
				}
			}
			finally
			{
				Overlapped.Free(overlappedPointer);
				if (!this.stopListening && !this.runOnce)
				{
					this.Monitor(fswasyncResult.buffer);
				}
			}
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x000B1624 File Offset: 0x000AF824
		protected override void Dispose(bool disposing)
		{
			try
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
			}
			finally
			{
				this.disposed = true;
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x000B16A4 File Offset: 0x000AF8A4
		public void EndInit()
		{
			this.initializing = false;
			if (this.directory.Length != 0 && this.enabled)
			{
				this.StartRaisingEvents();
			}
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x000B16C8 File Offset: 0x000AF8C8
		private bool IsSuspended()
		{
			return this.initializing || base.DesignMode;
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x000B16DC File Offset: 0x000AF8DC
		private bool MatchPattern(string relativePath)
		{
			string fileName = System.IO.Path.GetFileName(relativePath);
			return fileName != null && PatternMatcher.StrictMatchPattern(this.filter.ToUpper(CultureInfo.InvariantCulture), fileName.ToUpper(CultureInfo.InvariantCulture));
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000B1718 File Offset: 0x000AF918
		private unsafe void Monitor(byte[] buffer)
		{
			if (!this.enabled || this.IsHandleInvalid)
			{
				return;
			}
			Overlapped overlapped = new Overlapped();
			if (buffer == null)
			{
				try
				{
					buffer = new byte[this.internalBufferSize];
				}
				catch (OutOfMemoryException)
				{
					throw new OutOfMemoryException(SR.GetString("BufferSizeTooLarge", new object[]
					{
						this.internalBufferSize.ToString(CultureInfo.CurrentCulture)
					}));
				}
			}
			overlapped.AsyncResult = new FileSystemWatcher.FSWAsyncResult
			{
				session = this.currentSession,
				buffer = buffer
			};
			NativeOverlapped* ptr = overlapped.Pack(new IOCompletionCallback(this.CompletionStatusChanged), buffer);
			bool flag = false;
			try
			{
				if (!this.IsHandleInvalid)
				{
					try
					{
						byte[] array;
						byte* value;
						if ((array = buffer) == null || array.Length == 0)
						{
							value = null;
						}
						else
						{
							value = &array[0];
						}
						int num;
						flag = UnsafeNativeMethods.ReadDirectoryChangesW(this.directoryHandle, new HandleRef(this, (IntPtr)((void*)value)), this.internalBufferSize, this.includeSubdirectories ? 1 : 0, (int)this.notifyFilters, out num, ptr, NativeMethods.NullHandleRef);
					}
					finally
					{
						byte[] array = null;
					}
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
					if (!this.IsHandleInvalid)
					{
						this.OnError(new ErrorEventArgs(new Win32Exception()));
					}
				}
			}
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x000B1880 File Offset: 0x000AFA80
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

		// Token: 0x06002693 RID: 9875 RVA: 0x000B18E8 File Offset: 0x000AFAE8
		private void NotifyInternalBufferOverflowEvent()
		{
			InternalBufferOverflowException exception = new InternalBufferOverflowException(SR.GetString("FSW_BufferOverflow", new object[]
			{
				this.directory
			}));
			ErrorEventArgs e = new ErrorEventArgs(exception);
			this.OnError(e);
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000B1924 File Offset: 0x000AFB24
		private void NotifyRenameEventArgs(WatcherChangeTypes action, string name, string oldName)
		{
			if (!this.MatchPattern(name) && !this.MatchPattern(oldName))
			{
				return;
			}
			RenamedEventArgs e = new RenamedEventArgs(action, this.directory, name, oldName);
			this.OnRenamed(e);
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000B195C File Offset: 0x000AFB5C
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

		// Token: 0x06002696 RID: 9878 RVA: 0x000B19AC File Offset: 0x000AFBAC
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

		// Token: 0x06002697 RID: 9879 RVA: 0x000B19FC File Offset: 0x000AFBFC
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

		// Token: 0x06002698 RID: 9880 RVA: 0x000B1A4C File Offset: 0x000AFC4C
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

		// Token: 0x06002699 RID: 9881 RVA: 0x000B1A9C File Offset: 0x000AFC9C
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

		// Token: 0x0600269A RID: 9882 RVA: 0x000B1B00 File Offset: 0x000AFD00
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

		// Token: 0x0600269B RID: 9883 RVA: 0x000B1B68 File Offset: 0x000AFD68
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

		// Token: 0x0600269C RID: 9884 RVA: 0x000B1BB8 File Offset: 0x000AFDB8
		private void Restart()
		{
			if (!this.IsSuspended() && this.enabled)
			{
				this.StopRaisingEvents();
				this.StartRaisingEvents();
			}
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x000B1BD8 File Offset: 0x000AFDD8
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
			this.Monitor(null);
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x000B1D1C File Offset: 0x000AFF1C
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

		// Token: 0x0600269F RID: 9887 RVA: 0x000B1D6E File Offset: 0x000AFF6E
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType)
		{
			return this.WaitForChanged(changeType, -1);
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x000B1D78 File Offset: 0x000AFF78
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

		// Token: 0x040020BF RID: 8383
		private string directory;

		// Token: 0x040020C0 RID: 8384
		private string filter;

		// Token: 0x040020C1 RID: 8385
		private SafeFileHandle directoryHandle;

		// Token: 0x040020C2 RID: 8386
		private const NotifyFilters defaultNotifyFilters = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;

		// Token: 0x040020C3 RID: 8387
		private NotifyFilters notifyFilters = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;

		// Token: 0x040020C4 RID: 8388
		private bool includeSubdirectories;

		// Token: 0x040020C5 RID: 8389
		private bool enabled;

		// Token: 0x040020C6 RID: 8390
		private bool initializing;

		// Token: 0x040020C7 RID: 8391
		private int internalBufferSize = 8192;

		// Token: 0x040020C8 RID: 8392
		private WaitForChangedResult changedResult;

		// Token: 0x040020C9 RID: 8393
		private bool isChanged;

		// Token: 0x040020CA RID: 8394
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x040020CB RID: 8395
		private bool readGranted;

		// Token: 0x040020CC RID: 8396
		private bool disposed;

		// Token: 0x040020CD RID: 8397
		private int currentSession;

		// Token: 0x040020CE RID: 8398
		private FileSystemEventHandler onChangedHandler;

		// Token: 0x040020CF RID: 8399
		private FileSystemEventHandler onCreatedHandler;

		// Token: 0x040020D0 RID: 8400
		private FileSystemEventHandler onDeletedHandler;

		// Token: 0x040020D1 RID: 8401
		private RenamedEventHandler onRenamedHandler;

		// Token: 0x040020D2 RID: 8402
		private ErrorEventHandler onErrorHandler;

		// Token: 0x040020D3 RID: 8403
		private bool stopListening;

		// Token: 0x040020D4 RID: 8404
		private bool runOnce;

		// Token: 0x040020D5 RID: 8405
		private static readonly char[] wildcards = new char[]
		{
			'?',
			'*'
		};

		// Token: 0x040020D6 RID: 8406
		private static int notifyFiltersValidMask = 0;

		// Token: 0x02000813 RID: 2067
		private sealed class FSWAsyncResult : IAsyncResult
		{
			// Token: 0x17000FA5 RID: 4005
			// (get) Token: 0x060044FD RID: 17661 RVA: 0x00120CFA File Offset: 0x0011EEFA
			public bool IsCompleted
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000FA6 RID: 4006
			// (get) Token: 0x060044FE RID: 17662 RVA: 0x00120D01 File Offset: 0x0011EF01
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000FA7 RID: 4007
			// (get) Token: 0x060044FF RID: 17663 RVA: 0x00120D08 File Offset: 0x0011EF08
			public object AsyncState
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000FA8 RID: 4008
			// (get) Token: 0x06004500 RID: 17664 RVA: 0x00120D0F File Offset: 0x0011EF0F
			public bool CompletedSynchronously
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x04003583 RID: 13699
			internal int session;

			// Token: 0x04003584 RID: 13700
			internal byte[] buffer;
		}
	}
}
