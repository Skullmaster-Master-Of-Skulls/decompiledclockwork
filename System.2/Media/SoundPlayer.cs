using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Media
{
	// Token: 0x020003A4 RID: 932
	[ToolboxItem(false)]
	[HostProtection(SecurityAction.LinkDemand, UI = true)]
	[Serializable]
	public class SoundPlayer : Component, ISerializable
	{
		// Token: 0x060022B6 RID: 8886 RVA: 0x000A533F File Offset: 0x000A353F
		public SoundPlayer()
		{
			this.loadAsyncOperationCompleted = new SendOrPostCallback(this.LoadAsyncOperationCompleted);
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x000A537B File Offset: 0x000A357B
		public SoundPlayer(string soundLocation) : this()
		{
			if (soundLocation == null)
			{
				soundLocation = string.Empty;
			}
			this.SetupSoundLocation(soundLocation);
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000A5394 File Offset: 0x000A3594
		public SoundPlayer(Stream stream) : this()
		{
			this.stream = stream;
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000A53A4 File Offset: 0x000A35A4
		protected SoundPlayer(SerializationInfo serializationInfo, StreamingContext context)
		{
			foreach (SerializationEntry serializationEntry in serializationInfo)
			{
				string name = serializationEntry.Name;
				if (!(name == "SoundLocation"))
				{
					if (!(name == "Stream"))
					{
						if (name == "LoadTimeout")
						{
							this.LoadTimeout = (int)serializationEntry.Value;
						}
					}
					else
					{
						this.stream = (Stream)serializationEntry.Value;
						if (this.stream.CanSeek)
						{
							this.stream.Seek(0L, SeekOrigin.Begin);
						}
					}
				}
				else
				{
					this.SetupSoundLocation((string)serializationEntry.Value);
				}
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x060022BA RID: 8890 RVA: 0x000A547E File Offset: 0x000A367E
		// (set) Token: 0x060022BB RID: 8891 RVA: 0x000A5486 File Offset: 0x000A3686
		public int LoadTimeout
		{
			get
			{
				return this.loadTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("LoadTimeout", value, SR.GetString("SoundAPILoadTimeout"));
				}
				this.loadTimeout = value;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x060022BC RID: 8892 RVA: 0x000A54B0 File Offset: 0x000A36B0
		// (set) Token: 0x060022BD RID: 8893 RVA: 0x000A54F2 File Offset: 0x000A36F2
		public string SoundLocation
		{
			get
			{
				if (this.uri != null && this.uri.IsFile)
				{
					new FileIOPermission(PermissionState.None)
					{
						AllFiles = FileIOPermissionAccess.PathDiscovery
					}.Demand();
				}
				return this.soundLocation;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this.soundLocation.Equals(value))
				{
					return;
				}
				this.SetupSoundLocation(value);
				this.OnSoundLocationChanged(EventArgs.Empty);
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x000A551F File Offset: 0x000A371F
		// (set) Token: 0x060022BF RID: 8895 RVA: 0x000A5537 File Offset: 0x000A3737
		public Stream Stream
		{
			get
			{
				if (this.uri != null)
				{
					return null;
				}
				return this.stream;
			}
			set
			{
				if (this.stream == value)
				{
					return;
				}
				this.SetupStream(value);
				this.OnStreamChanged(EventArgs.Empty);
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x060022C0 RID: 8896 RVA: 0x000A5555 File Offset: 0x000A3755
		public bool IsLoadCompleted
		{
			get
			{
				return this.isLoadCompleted;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x000A555D File Offset: 0x000A375D
		// (set) Token: 0x060022C2 RID: 8898 RVA: 0x000A5565 File Offset: 0x000A3765
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x000A5570 File Offset: 0x000A3770
		public void LoadAsync()
		{
			if (this.uri != null && this.uri.IsFile)
			{
				this.isLoadCompleted = true;
				FileInfo fileInfo = new FileInfo(this.uri.LocalPath);
				if (!fileInfo.Exists)
				{
					throw new FileNotFoundException(SR.GetString("SoundAPIFileDoesNotExist"), this.soundLocation);
				}
				this.OnLoadCompleted(new AsyncCompletedEventArgs(null, false, null));
				return;
			}
			else
			{
				if (this.copyThread != null && this.copyThread.ThreadState == ThreadState.Running)
				{
					return;
				}
				this.isLoadCompleted = false;
				this.streamData = null;
				this.currentPos = 0;
				this.asyncOperation = AsyncOperationManager.CreateOperation(null);
				this.LoadStream(false);
				return;
			}
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000A561B File Offset: 0x000A381B
		private void LoadAsyncOperationCompleted(object arg)
		{
			this.OnLoadCompleted((AsyncCompletedEventArgs)arg);
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x000A5629 File Offset: 0x000A3829
		private void CleanupStreamData()
		{
			this.currentPos = 0;
			this.streamData = null;
			this.isLoadCompleted = false;
			this.lastLoadException = null;
			this.doesLoadAppearSynchronous = false;
			this.copyThread = null;
			this.semaphore.Set();
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x000A5664 File Offset: 0x000A3864
		public void Load()
		{
			if (!(this.uri != null) || !this.uri.IsFile)
			{
				this.LoadSync();
				return;
			}
			FileInfo fileInfo = new FileInfo(this.uri.LocalPath);
			if (!fileInfo.Exists)
			{
				throw new FileNotFoundException(SR.GetString("SoundAPIFileDoesNotExist"), this.soundLocation);
			}
			this.isLoadCompleted = true;
			this.OnLoadCompleted(new AsyncCompletedEventArgs(null, false, null));
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x000A56D8 File Offset: 0x000A38D8
		private void LoadAndPlay(int flags)
		{
			if (string.IsNullOrEmpty(this.soundLocation) && this.stream == null)
			{
				SystemSounds.Beep.Play();
				return;
			}
			if (this.uri != null && this.uri.IsFile)
			{
				string localPath = this.uri.LocalPath;
				FileIOPermission fileIOPermission = new FileIOPermission(FileIOPermissionAccess.Read, localPath);
				fileIOPermission.Demand();
				this.isLoadCompleted = true;
				SoundPlayer.IntSecurity.SafeSubWindows.Demand();
				System.ComponentModel.IntSecurity.UnmanagedCode.Assert();
				try
				{
					this.ValidateSoundFile(localPath);
					SoundPlayer.UnsafeNativeMethods.PlaySound(localPath, IntPtr.Zero, 2 | flags);
					return;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			this.LoadSync();
			SoundPlayer.ValidateSoundData(this.streamData);
			SoundPlayer.IntSecurity.SafeSubWindows.Demand();
			System.ComponentModel.IntSecurity.UnmanagedCode.Assert();
			try
			{
				SoundPlayer.UnsafeNativeMethods.PlaySound(this.streamData, IntPtr.Zero, 6 | flags);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x000A57D0 File Offset: 0x000A39D0
		private void LoadSync()
		{
			if (!this.semaphore.WaitOne(this.LoadTimeout, false))
			{
				if (this.copyThread != null)
				{
					this.copyThread.Abort();
				}
				this.CleanupStreamData();
				throw new TimeoutException(SR.GetString("SoundAPILoadTimedOut"));
			}
			if (this.streamData != null)
			{
				return;
			}
			if (this.uri != null && !this.uri.IsFile && this.stream == null)
			{
				WebPermission webPermission = new WebPermission(NetworkAccess.Connect, this.uri.AbsolutePath);
				webPermission.Demand();
				WebRequest webRequest = WebRequest.Create(this.uri);
				webRequest.Timeout = this.LoadTimeout;
				WebResponse response = webRequest.GetResponse();
				this.stream = response.GetResponseStream();
			}
			if (this.stream.CanSeek)
			{
				this.LoadStream(true);
			}
			else
			{
				this.doesLoadAppearSynchronous = true;
				this.LoadStream(false);
				if (!this.semaphore.WaitOne(this.LoadTimeout, false))
				{
					if (this.copyThread != null)
					{
						this.copyThread.Abort();
					}
					this.CleanupStreamData();
					throw new TimeoutException(SR.GetString("SoundAPILoadTimedOut"));
				}
				this.doesLoadAppearSynchronous = false;
				if (this.lastLoadException != null)
				{
					throw this.lastLoadException;
				}
			}
			this.copyThread = null;
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x000A5908 File Offset: 0x000A3B08
		private void LoadStream(bool loadSync)
		{
			if (loadSync && this.stream.CanSeek)
			{
				int num = (int)this.stream.Length;
				this.currentPos = 0;
				this.streamData = new byte[num];
				this.stream.Read(this.streamData, 0, num);
				this.isLoadCompleted = true;
				this.OnLoadCompleted(new AsyncCompletedEventArgs(null, false, null));
				return;
			}
			this.semaphore.Reset();
			this.copyThread = new Thread(new ThreadStart(this.WorkerThread));
			this.copyThread.Start();
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x000A599D File Offset: 0x000A3B9D
		public void Play()
		{
			this.LoadAndPlay(1);
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000A59A6 File Offset: 0x000A3BA6
		public void PlaySync()
		{
			this.LoadAndPlay(0);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000A59AF File Offset: 0x000A3BAF
		public void PlayLooping()
		{
			this.LoadAndPlay(9);
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x000A59BC File Offset: 0x000A3BBC
		private static Uri ResolveUri(string partialUri)
		{
			Uri uri = null;
			try
			{
				uri = new Uri(partialUri);
			}
			catch (UriFormatException)
			{
			}
			if (uri == null)
			{
				try
				{
					uri = new Uri(Path.GetFullPath(partialUri));
				}
				catch (UriFormatException)
				{
				}
			}
			return uri;
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x000A5A10 File Offset: 0x000A3C10
		private void SetupSoundLocation(string soundLocation)
		{
			if (this.copyThread != null)
			{
				this.copyThread.Abort();
				this.CleanupStreamData();
			}
			this.uri = SoundPlayer.ResolveUri(soundLocation);
			this.soundLocation = soundLocation;
			this.stream = null;
			if (this.uri == null)
			{
				if (!string.IsNullOrEmpty(soundLocation))
				{
					throw new UriFormatException(SR.GetString("SoundAPIBadSoundLocation"));
				}
			}
			else if (!this.uri.IsFile)
			{
				this.streamData = null;
				this.currentPos = 0;
				this.isLoadCompleted = false;
			}
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x000A5A98 File Offset: 0x000A3C98
		private void SetupStream(Stream stream)
		{
			if (this.copyThread != null)
			{
				this.copyThread.Abort();
				this.CleanupStreamData();
			}
			this.stream = stream;
			this.soundLocation = string.Empty;
			this.streamData = null;
			this.currentPos = 0;
			this.isLoadCompleted = false;
			if (stream != null)
			{
				this.uri = null;
			}
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x000A5AEF File Offset: 0x000A3CEF
		public void Stop()
		{
			SoundPlayer.IntSecurity.SafeSubWindows.Demand();
			SoundPlayer.UnsafeNativeMethods.PlaySound(null, IntPtr.Zero, 64);
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060022D1 RID: 8913 RVA: 0x000A5B09 File Offset: 0x000A3D09
		// (remove) Token: 0x060022D2 RID: 8914 RVA: 0x000A5B1C File Offset: 0x000A3D1C
		public event AsyncCompletedEventHandler LoadCompleted
		{
			add
			{
				base.Events.AddHandler(SoundPlayer.EventLoadCompleted, value);
			}
			remove
			{
				base.Events.RemoveHandler(SoundPlayer.EventLoadCompleted, value);
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060022D3 RID: 8915 RVA: 0x000A5B2F File Offset: 0x000A3D2F
		// (remove) Token: 0x060022D4 RID: 8916 RVA: 0x000A5B42 File Offset: 0x000A3D42
		public event EventHandler SoundLocationChanged
		{
			add
			{
				base.Events.AddHandler(SoundPlayer.EventSoundLocationChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(SoundPlayer.EventSoundLocationChanged, value);
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060022D5 RID: 8917 RVA: 0x000A5B55 File Offset: 0x000A3D55
		// (remove) Token: 0x060022D6 RID: 8918 RVA: 0x000A5B68 File Offset: 0x000A3D68
		public event EventHandler StreamChanged
		{
			add
			{
				base.Events.AddHandler(SoundPlayer.EventStreamChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(SoundPlayer.EventStreamChanged, value);
			}
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x000A5B7C File Offset: 0x000A3D7C
		protected virtual void OnLoadCompleted(AsyncCompletedEventArgs e)
		{
			AsyncCompletedEventHandler asyncCompletedEventHandler = (AsyncCompletedEventHandler)base.Events[SoundPlayer.EventLoadCompleted];
			if (asyncCompletedEventHandler != null)
			{
				asyncCompletedEventHandler(this, e);
			}
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x000A5BAC File Offset: 0x000A3DAC
		protected virtual void OnSoundLocationChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[SoundPlayer.EventSoundLocationChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x000A5BDC File Offset: 0x000A3DDC
		protected virtual void OnStreamChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[SoundPlayer.EventStreamChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x000A5C0C File Offset: 0x000A3E0C
		private void WorkerThread()
		{
			try
			{
				if (this.uri != null && !this.uri.IsFile && this.stream == null)
				{
					WebRequest webRequest = WebRequest.Create(this.uri);
					WebResponse response = webRequest.GetResponse();
					this.stream = response.GetResponseStream();
				}
				this.streamData = new byte[1024];
				int i = this.stream.Read(this.streamData, this.currentPos, 1024);
				int num = i;
				while (i > 0)
				{
					this.currentPos += i;
					if (this.streamData.Length < this.currentPos + 1024)
					{
						byte[] destinationArray = new byte[this.streamData.Length * 2];
						Array.Copy(this.streamData, destinationArray, this.streamData.Length);
						this.streamData = destinationArray;
					}
					i = this.stream.Read(this.streamData, this.currentPos, 1024);
					num += i;
				}
				this.lastLoadException = null;
			}
			catch (Exception ex)
			{
				this.lastLoadException = ex;
			}
			if (!this.doesLoadAppearSynchronous)
			{
				this.asyncOperation.PostOperationCompleted(this.loadAsyncOperationCompleted, new AsyncCompletedEventArgs(this.lastLoadException, false, null));
			}
			this.isLoadCompleted = true;
			this.semaphore.Set();
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x000A5D64 File Offset: 0x000A3F64
		private unsafe void ValidateSoundFile(string fileName)
		{
			SoundPlayer.NativeMethods.MMCKINFO mmckinfo = new SoundPlayer.NativeMethods.MMCKINFO();
			SoundPlayer.NativeMethods.MMCKINFO mmckinfo2 = new SoundPlayer.NativeMethods.MMCKINFO();
			SoundPlayer.NativeMethods.WAVEFORMATEX waveformatex = null;
			IntPtr intPtr = SoundPlayer.UnsafeNativeMethods.mmioOpen(fileName, IntPtr.Zero, 65536);
			if (intPtr == IntPtr.Zero)
			{
				throw new FileNotFoundException(SR.GetString("SoundAPIFileDoesNotExist"), this.soundLocation);
			}
			try
			{
				mmckinfo.fccType = SoundPlayer.mmioFOURCC('W', 'A', 'V', 'E');
				if (SoundPlayer.UnsafeNativeMethods.mmioDescend(intPtr, mmckinfo, null, 32) != 0)
				{
					throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveFile", new object[]
					{
						this.soundLocation
					}));
				}
				while (SoundPlayer.UnsafeNativeMethods.mmioDescend(intPtr, mmckinfo2, mmckinfo, 0) == 0)
				{
					if (mmckinfo2.dwDataOffset + mmckinfo2.cksize > mmckinfo.dwDataOffset + mmckinfo.cksize)
					{
						throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveHeader"));
					}
					if (mmckinfo2.ckID == SoundPlayer.mmioFOURCC('f', 'm', 't', ' ') && waveformatex == null)
					{
						int num = mmckinfo2.cksize;
						if (num < Marshal.SizeOf(typeof(SoundPlayer.NativeMethods.WAVEFORMATEX)))
						{
							num = Marshal.SizeOf(typeof(SoundPlayer.NativeMethods.WAVEFORMATEX));
						}
						waveformatex = new SoundPlayer.NativeMethods.WAVEFORMATEX();
						byte[] array = new byte[num];
						if (SoundPlayer.UnsafeNativeMethods.mmioRead(intPtr, array, num) != num)
						{
							throw new InvalidOperationException(SR.GetString("SoundAPIReadError", new object[]
							{
								this.soundLocation
							}));
						}
						try
						{
							byte[] array2;
							byte* value;
							if ((array2 = array) == null || array2.Length == 0)
							{
								value = null;
							}
							else
							{
								value = &array2[0];
							}
							Marshal.PtrToStructure((IntPtr)((void*)value), waveformatex);
						}
						finally
						{
							byte[] array2 = null;
						}
					}
					SoundPlayer.UnsafeNativeMethods.mmioAscend(intPtr, mmckinfo2, 0);
				}
				if (waveformatex == null)
				{
					throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveHeader"));
				}
				if (waveformatex.wFormatTag != 1 && waveformatex.wFormatTag != 2 && waveformatex.wFormatTag != 3)
				{
					throw new InvalidOperationException(SR.GetString("SoundAPIFormatNotSupported"));
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					SoundPlayer.UnsafeNativeMethods.mmioClose(intPtr, 0);
				}
			}
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x000A5F7C File Offset: 0x000A417C
		private static void ValidateSoundData(byte[] data)
		{
			short num = -1;
			bool flag = false;
			if (data.Length < 12)
			{
				throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveHeader"));
			}
			if (data[0] != 82 || data[1] != 73 || data[2] != 70 || data[3] != 70)
			{
				throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveHeader"));
			}
			if (data[8] != 87 || data[9] != 65 || data[10] != 86 || data[11] != 69)
			{
				throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveHeader"));
			}
			int num2 = 12;
			int num3 = data.Length;
			while (!flag && num2 < num3 - 8)
			{
				if (data[num2] == 102 && data[num2 + 1] == 109 && data[num2 + 2] == 116 && data[num2 + 3] == 32)
				{
					flag = true;
					int num4 = SoundPlayer.BytesToInt(data[num2 + 7], data[num2 + 6], data[num2 + 5], data[num2 + 4]);
					int num5 = 16;
					if (num4 != num5)
					{
						int num6 = 18;
						if (num3 < num2 + 8 + num6 - 1)
						{
							throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveHeader"));
						}
						short num7 = SoundPlayer.BytesToInt16(data[num2 + 8 + num6 - 1], data[num2 + 8 + num6 - 2]);
						if ((int)num7 + num6 != num4)
						{
							throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveHeader"));
						}
					}
					if (num3 < num2 + 9)
					{
						throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveHeader"));
					}
					num = SoundPlayer.BytesToInt16(data[num2 + 9], data[num2 + 8]);
					num2 += num4 + 8;
				}
				else
				{
					num2 += 8 + SoundPlayer.BytesToInt(data[num2 + 7], data[num2 + 6], data[num2 + 5], data[num2 + 4]);
				}
			}
			if (!flag)
			{
				throw new InvalidOperationException(SR.GetString("SoundAPIInvalidWaveHeader"));
			}
			if (num != 1 && num != 2 && num != 3)
			{
				throw new InvalidOperationException(SR.GetString("SoundAPIFormatNotSupported"));
			}
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x000A6140 File Offset: 0x000A4340
		private static short BytesToInt16(byte ch0, byte ch1)
		{
			int num = (int)ch1 | (int)ch0 << 8;
			return (short)num;
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x000A6157 File Offset: 0x000A4357
		private static int BytesToInt(byte ch0, byte ch1, byte ch2, byte ch3)
		{
			return SoundPlayer.mmioFOURCC((char)ch3, (char)ch2, (char)ch1, (char)ch0);
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x000A6164 File Offset: 0x000A4364
		private static int mmioFOURCC(char ch0, char ch1, char ch2, char ch3)
		{
			int num = 0;
			num |= (int)ch0;
			num |= (int)((int)ch1 << 8);
			num |= (int)((int)ch2 << 16);
			return num | (int)((int)ch3 << 24);
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x000A618C File Offset: 0x000A438C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (!string.IsNullOrEmpty(this.soundLocation))
			{
				info.AddValue("SoundLocation", this.soundLocation);
			}
			if (this.stream != null)
			{
				info.AddValue("Stream", this.stream);
			}
			info.AddValue("LoadTimeout", this.loadTimeout);
		}

		// Token: 0x04001FA2 RID: 8098
		private const int blockSize = 1024;

		// Token: 0x04001FA3 RID: 8099
		private const int defaultLoadTimeout = 10000;

		// Token: 0x04001FA4 RID: 8100
		private Uri uri;

		// Token: 0x04001FA5 RID: 8101
		private string soundLocation = string.Empty;

		// Token: 0x04001FA6 RID: 8102
		private int loadTimeout = 10000;

		// Token: 0x04001FA7 RID: 8103
		private object tag;

		// Token: 0x04001FA8 RID: 8104
		private ManualResetEvent semaphore = new ManualResetEvent(true);

		// Token: 0x04001FA9 RID: 8105
		private Thread copyThread;

		// Token: 0x04001FAA RID: 8106
		private int currentPos;

		// Token: 0x04001FAB RID: 8107
		private Stream stream;

		// Token: 0x04001FAC RID: 8108
		private bool isLoadCompleted;

		// Token: 0x04001FAD RID: 8109
		private Exception lastLoadException;

		// Token: 0x04001FAE RID: 8110
		private bool doesLoadAppearSynchronous;

		// Token: 0x04001FAF RID: 8111
		private byte[] streamData;

		// Token: 0x04001FB0 RID: 8112
		private AsyncOperation asyncOperation;

		// Token: 0x04001FB1 RID: 8113
		private readonly SendOrPostCallback loadAsyncOperationCompleted;

		// Token: 0x04001FB2 RID: 8114
		private static readonly object EventLoadCompleted = new object();

		// Token: 0x04001FB3 RID: 8115
		private static readonly object EventSoundLocationChanged = new object();

		// Token: 0x04001FB4 RID: 8116
		private static readonly object EventStreamChanged = new object();

		// Token: 0x020007E2 RID: 2018
		private class IntSecurity
		{
			// Token: 0x060043D6 RID: 17366 RVA: 0x0011DCC6 File Offset: 0x0011BEC6
			private IntSecurity()
			{
			}

			// Token: 0x17000F5A RID: 3930
			// (get) Token: 0x060043D7 RID: 17367 RVA: 0x0011DCCE File Offset: 0x0011BECE
			internal static CodeAccessPermission SafeSubWindows
			{
				get
				{
					if (SoundPlayer.IntSecurity.safeSubWindows == null)
					{
						SoundPlayer.IntSecurity.safeSubWindows = new UIPermission(UIPermissionWindow.SafeSubWindows);
					}
					return SoundPlayer.IntSecurity.safeSubWindows;
				}
			}

			// Token: 0x040034E7 RID: 13543
			private static volatile CodeAccessPermission safeSubWindows;
		}

		// Token: 0x020007E3 RID: 2019
		private class NativeMethods
		{
			// Token: 0x060043D8 RID: 17368 RVA: 0x0011DCED File Offset: 0x0011BEED
			private NativeMethods()
			{
			}

			// Token: 0x040034E8 RID: 13544
			internal const int WAVE_FORMAT_PCM = 1;

			// Token: 0x040034E9 RID: 13545
			internal const int WAVE_FORMAT_ADPCM = 2;

			// Token: 0x040034EA RID: 13546
			internal const int WAVE_FORMAT_IEEE_FLOAT = 3;

			// Token: 0x040034EB RID: 13547
			internal const int MMIO_READ = 0;

			// Token: 0x040034EC RID: 13548
			internal const int MMIO_ALLOCBUF = 65536;

			// Token: 0x040034ED RID: 13549
			internal const int MMIO_FINDRIFF = 32;

			// Token: 0x040034EE RID: 13550
			internal const int SND_SYNC = 0;

			// Token: 0x040034EF RID: 13551
			internal const int SND_ASYNC = 1;

			// Token: 0x040034F0 RID: 13552
			internal const int SND_NODEFAULT = 2;

			// Token: 0x040034F1 RID: 13553
			internal const int SND_MEMORY = 4;

			// Token: 0x040034F2 RID: 13554
			internal const int SND_LOOP = 8;

			// Token: 0x040034F3 RID: 13555
			internal const int SND_PURGE = 64;

			// Token: 0x040034F4 RID: 13556
			internal const int SND_FILENAME = 131072;

			// Token: 0x040034F5 RID: 13557
			internal const int SND_NOSTOP = 16;

			// Token: 0x02000923 RID: 2339
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
			internal class MMCKINFO
			{
				// Token: 0x04003DB1 RID: 15793
				internal int ckID;

				// Token: 0x04003DB2 RID: 15794
				internal int cksize;

				// Token: 0x04003DB3 RID: 15795
				internal int fccType;

				// Token: 0x04003DB4 RID: 15796
				internal int dwDataOffset;

				// Token: 0x04003DB5 RID: 15797
				internal int dwFlags;
			}

			// Token: 0x02000924 RID: 2340
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
			internal class WAVEFORMATEX
			{
				// Token: 0x04003DB6 RID: 15798
				internal short wFormatTag;

				// Token: 0x04003DB7 RID: 15799
				internal short nChannels;

				// Token: 0x04003DB8 RID: 15800
				internal int nSamplesPerSec;

				// Token: 0x04003DB9 RID: 15801
				internal int nAvgBytesPerSec;

				// Token: 0x04003DBA RID: 15802
				internal short nBlockAlign;

				// Token: 0x04003DBB RID: 15803
				internal short wBitsPerSample;

				// Token: 0x04003DBC RID: 15804
				internal short cbSize;
			}
		}

		// Token: 0x020007E4 RID: 2020
		private class UnsafeNativeMethods
		{
			// Token: 0x060043D9 RID: 17369 RVA: 0x0011DCF5 File Offset: 0x0011BEF5
			private UnsafeNativeMethods()
			{
			}

			// Token: 0x060043DA RID: 17370
			[DllImport("winmm.dll", CharSet = CharSet.Auto)]
			internal static extern bool PlaySound([MarshalAs(UnmanagedType.LPWStr)] string soundName, IntPtr hmod, int soundFlags);

			// Token: 0x060043DB RID: 17371
			[DllImport("winmm.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
			internal static extern bool PlaySound(byte[] soundName, IntPtr hmod, int soundFlags);

			// Token: 0x060043DC RID: 17372
			[DllImport("winmm.dll", CharSet = CharSet.Auto)]
			internal static extern IntPtr mmioOpen(string fileName, IntPtr not_used, int flags);

			// Token: 0x060043DD RID: 17373
			[DllImport("winmm.dll", CharSet = CharSet.Auto)]
			internal static extern int mmioAscend(IntPtr hMIO, SoundPlayer.NativeMethods.MMCKINFO lpck, int flags);

			// Token: 0x060043DE RID: 17374
			[DllImport("winmm.dll", CharSet = CharSet.Auto)]
			internal static extern int mmioDescend(IntPtr hMIO, [MarshalAs(UnmanagedType.LPStruct)] SoundPlayer.NativeMethods.MMCKINFO lpck, [MarshalAs(UnmanagedType.LPStruct)] SoundPlayer.NativeMethods.MMCKINFO lcpkParent, int flags);

			// Token: 0x060043DF RID: 17375
			[DllImport("winmm.dll", CharSet = CharSet.Auto)]
			internal static extern int mmioRead(IntPtr hMIO, [MarshalAs(UnmanagedType.LPArray)] byte[] wf, int cch);

			// Token: 0x060043E0 RID: 17376
			[DllImport("winmm.dll", CharSet = CharSet.Auto)]
			internal static extern int mmioClose(IntPtr hMIO, int flags);
		}
	}
}
