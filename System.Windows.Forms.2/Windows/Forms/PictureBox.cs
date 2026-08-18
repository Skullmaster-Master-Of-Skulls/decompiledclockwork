using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x0200031C RID: 796
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Image")]
	[DefaultBindingProperty("Image")]
	[Docking(DockingBehavior.Ask)]
	[Designer("System.Windows.Forms.Design.PictureBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionPictureBox")]
	public class PictureBox : Control, ISupportInitialize
	{
		// Token: 0x06003289 RID: 12937 RVA: 0x000E2A4C File Offset: 0x000E0C4C
		public PictureBox()
		{
			base.SetState2(2048, true);
			this.pictureBoxState = new BitVector32(12);
			base.SetStyle(ControlStyles.Opaque | ControlStyles.Selectable, false);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
			this.TabStop = false;
			this.savedSize = base.Size;
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x0600328A RID: 12938 RVA: 0x000B90B9 File Offset: 0x000B72B9
		// (set) Token: 0x0600328B RID: 12939 RVA: 0x000B90C1 File Offset: 0x000B72C1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				base.AllowDrop = value;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x0600328C RID: 12940 RVA: 0x000E2AAE File Offset: 0x000E0CAE
		// (set) Token: 0x0600328D RID: 12941 RVA: 0x000E2AB8 File Offset: 0x000E0CB8
		[DefaultValue(BorderStyle.None)]
		[SRCategory("CatAppearance")]
		[DispId(-504)]
		[SRDescription("PictureBoxBorderStyleDescr")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
				}
				if (this.borderStyle != value)
				{
					this.borderStyle = value;
					base.RecreateHandle();
					this.AdjustSize();
				}
			}
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x000E2B08 File Offset: 0x000E0D08
		private Uri CalculateUri(string path)
		{
			Uri result;
			try
			{
				result = new Uri(path);
			}
			catch (UriFormatException)
			{
				path = Path.GetFullPath(path);
				result = new Uri(path);
			}
			return result;
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000E2B44 File Offset: 0x000E0D44
		[SRCategory("CatAsynchronous")]
		[SRDescription("PictureBoxCancelAsyncDescr")]
		public void CancelAsync()
		{
			this.pictureBoxState[2] = true;
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06003290 RID: 12944 RVA: 0x000E2B53 File Offset: 0x000E0D53
		// (set) Token: 0x06003291 RID: 12945 RVA: 0x000E2B5B File Offset: 0x000E0D5B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool CausesValidation
		{
			get
			{
				return base.CausesValidation;
			}
			set
			{
				base.CausesValidation = value;
			}
		}

		// Token: 0x14000251 RID: 593
		// (add) Token: 0x06003292 RID: 12946 RVA: 0x000E2B64 File Offset: 0x000E0D64
		// (remove) Token: 0x06003293 RID: 12947 RVA: 0x000E2B6D File Offset: 0x000E0D6D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CausesValidationChanged
		{
			add
			{
				base.CausesValidationChanged += value;
			}
			remove
			{
				base.CausesValidationChanged -= value;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06003294 RID: 12948 RVA: 0x000E2B78 File Offset: 0x000E0D78
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				BorderStyle borderStyle = this.borderStyle;
				if (borderStyle != BorderStyle.FixedSingle)
				{
					if (borderStyle == BorderStyle.Fixed3D)
					{
						createParams.ExStyle |= 512;
					}
				}
				else
				{
					createParams.Style |= 8388608;
				}
				return createParams;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06003295 RID: 12949 RVA: 0x00023D73 File Offset: 0x00021F73
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06003296 RID: 12950 RVA: 0x000E2BC2 File Offset: 0x000E0DC2
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 50);
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06003297 RID: 12951 RVA: 0x000E2BD0 File Offset: 0x000E0DD0
		// (set) Token: 0x06003298 RID: 12952 RVA: 0x000E2C38 File Offset: 0x000E0E38
		[SRCategory("CatAsynchronous")]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("PictureBoxErrorImageDescr")]
		public Image ErrorImage
		{
			get
			{
				if (this.errorImage == null && this.pictureBoxState[8])
				{
					if (this.defaultErrorImage == null)
					{
						if (PictureBox.defaultErrorImageForThread == null)
						{
							PictureBox.defaultErrorImageForThread = new Bitmap(typeof(PictureBox), "ImageInError.bmp");
						}
						this.defaultErrorImage = PictureBox.defaultErrorImageForThread;
					}
					this.errorImage = this.defaultErrorImage;
				}
				return this.errorImage;
			}
			set
			{
				if (this.ErrorImage != value)
				{
					this.pictureBoxState[8] = false;
				}
				this.errorImage = value;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06003299 RID: 12953 RVA: 0x0001A283 File Offset: 0x00018483
		// (set) Token: 0x0600329A RID: 12954 RVA: 0x00013238 File Offset: 0x00011438
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x14000252 RID: 594
		// (add) Token: 0x0600329B RID: 12955 RVA: 0x0005AACE File Offset: 0x00058CCE
		// (remove) Token: 0x0600329C RID: 12956 RVA: 0x0005AAD7 File Offset: 0x00058CD7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				base.ForeColorChanged += value;
			}
			remove
			{
				base.ForeColorChanged -= value;
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x0600329D RID: 12957 RVA: 0x0001A272 File Offset: 0x00018472
		// (set) Token: 0x0600329E RID: 12958 RVA: 0x0001A27A File Offset: 0x0001847A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		// Token: 0x14000253 RID: 595
		// (add) Token: 0x0600329F RID: 12959 RVA: 0x0005AAE0 File Offset: 0x00058CE0
		// (remove) Token: 0x060032A0 RID: 12960 RVA: 0x0005AAE9 File Offset: 0x00058CE9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler FontChanged
		{
			add
			{
				base.FontChanged += value;
			}
			remove
			{
				base.FontChanged -= value;
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x060032A1 RID: 12961 RVA: 0x000E2C57 File Offset: 0x000E0E57
		// (set) Token: 0x060032A2 RID: 12962 RVA: 0x000E2C5F File Offset: 0x000E0E5F
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[Bindable(true)]
		[SRDescription("PictureBoxImageDescr")]
		public Image Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.InstallNewImage(value, PictureBox.ImageInstallationType.DirectlySpecified);
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x060032A3 RID: 12963 RVA: 0x000E2C69 File Offset: 0x000E0E69
		// (set) Token: 0x060032A4 RID: 12964 RVA: 0x000E2C74 File Offset: 0x000E0E74
		[SRCategory("CatAsynchronous")]
		[Localizable(true)]
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("PictureBoxImageLocationDescr")]
		public string ImageLocation
		{
			get
			{
				return this.imageLocation;
			}
			set
			{
				this.imageLocation = value;
				this.pictureBoxState[32] = !string.IsNullOrEmpty(this.imageLocation);
				if (string.IsNullOrEmpty(this.imageLocation) && this.imageInstallationType != PictureBox.ImageInstallationType.DirectlySpecified)
				{
					this.InstallNewImage(null, PictureBox.ImageInstallationType.DirectlySpecified);
				}
				if (this.WaitOnLoad && !this.pictureBoxState[64] && !string.IsNullOrEmpty(this.imageLocation))
				{
					this.Load();
				}
				base.Invalidate();
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x060032A5 RID: 12965 RVA: 0x000E2CF0 File Offset: 0x000E0EF0
		private Rectangle ImageRectangle
		{
			get
			{
				return this.ImageRectangleFromSizeMode(this.sizeMode);
			}
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x000E2D00 File Offset: 0x000E0F00
		private Rectangle ImageRectangleFromSizeMode(PictureBoxSizeMode mode)
		{
			Rectangle result = LayoutUtils.DeflateRect(base.ClientRectangle, base.Padding);
			if (this.image != null)
			{
				switch (mode)
				{
				case PictureBoxSizeMode.Normal:
				case PictureBoxSizeMode.AutoSize:
					result.Size = this.image.Size;
					break;
				case PictureBoxSizeMode.CenterImage:
					result.X += (result.Width - this.image.Width) / 2;
					result.Y += (result.Height - this.image.Height) / 2;
					result.Size = this.image.Size;
					break;
				case PictureBoxSizeMode.Zoom:
				{
					Size size = this.image.Size;
					float num = Math.Min((float)base.ClientRectangle.Width / (float)size.Width, (float)base.ClientRectangle.Height / (float)size.Height);
					result.Width = (int)((float)size.Width * num);
					result.Height = (int)((float)size.Height * num);
					result.X = (base.ClientRectangle.Width - result.Width) / 2;
					result.Y = (base.ClientRectangle.Height - result.Height) / 2;
					break;
				}
				}
			}
			return result;
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x060032A7 RID: 12967 RVA: 0x000E2E64 File Offset: 0x000E1064
		// (set) Token: 0x060032A8 RID: 12968 RVA: 0x000E2ECC File Offset: 0x000E10CC
		[SRCategory("CatAsynchronous")]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("PictureBoxInitialImageDescr")]
		public Image InitialImage
		{
			get
			{
				if (this.initialImage == null && this.pictureBoxState[4])
				{
					if (this.defaultInitialImage == null)
					{
						if (PictureBox.defaultInitialImageForThread == null)
						{
							PictureBox.defaultInitialImageForThread = new Bitmap(typeof(PictureBox), "PictureBox.Loading.bmp");
						}
						this.defaultInitialImage = PictureBox.defaultInitialImageForThread;
					}
					this.initialImage = this.defaultInitialImage;
				}
				return this.initialImage;
			}
			set
			{
				if (this.InitialImage != value)
				{
					this.pictureBoxState[4] = false;
				}
				this.initialImage = value;
			}
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x000E2EEC File Offset: 0x000E10EC
		private void InstallNewImage(Image value, PictureBox.ImageInstallationType installationType)
		{
			this.StopAnimate();
			this.image = value;
			LayoutTransaction.DoLayoutIf(this.AutoSize, this, this, PropertyNames.Image);
			this.Animate();
			if (installationType != PictureBox.ImageInstallationType.ErrorOrInitial)
			{
				this.AdjustSize();
			}
			this.imageInstallationType = installationType;
			base.Invalidate();
			CommonProperties.xClearPreferredSizeCache(this);
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x060032AA RID: 12970 RVA: 0x0001A1ED File Offset: 0x000183ED
		// (set) Token: 0x060032AB RID: 12971 RVA: 0x0001A1F5 File Offset: 0x000183F5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ImeMode ImeMode
		{
			get
			{
				return base.ImeMode;
			}
			set
			{
				base.ImeMode = value;
			}
		}

		// Token: 0x14000254 RID: 596
		// (add) Token: 0x060032AC RID: 12972 RVA: 0x0002410C File Offset: 0x0002230C
		// (remove) Token: 0x060032AD RID: 12973 RVA: 0x00024115 File Offset: 0x00022315
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ImeModeChanged
		{
			add
			{
				base.ImeModeChanged += value;
			}
			remove
			{
				base.ImeModeChanged -= value;
			}
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000E2F3C File Offset: 0x000E113C
		[SRCategory("CatAsynchronous")]
		[SRDescription("PictureBoxLoad0Descr")]
		public void Load()
		{
			if (this.imageLocation == null || this.imageLocation.Length == 0)
			{
				throw new InvalidOperationException(SR.GetString("PictureBoxNoImageLocation"));
			}
			this.pictureBoxState[32] = false;
			PictureBox.ImageInstallationType installationType = PictureBox.ImageInstallationType.FromUrl;
			Image value;
			try
			{
				this.DisposeImageStream();
				Uri uri = this.CalculateUri(this.imageLocation);
				if (uri.IsFile)
				{
					this.localImageStreamReader = new StreamReader(uri.LocalPath);
					value = Image.FromStream(this.localImageStreamReader.BaseStream);
				}
				else
				{
					using (WebClient webClient = new WebClient())
					{
						this.uriImageStream = webClient.OpenRead(uri.ToString());
						value = Image.FromStream(this.uriImageStream);
					}
				}
			}
			catch
			{
				if (!base.DesignMode)
				{
					throw;
				}
				value = this.ErrorImage;
				installationType = PictureBox.ImageInstallationType.ErrorOrInitial;
			}
			this.InstallNewImage(value, installationType);
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000E302C File Offset: 0x000E122C
		[SRCategory("CatAsynchronous")]
		[SRDescription("PictureBoxLoad1Descr")]
		public void Load(string url)
		{
			this.ImageLocation = url;
			this.Load();
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x000E303C File Offset: 0x000E123C
		[SRCategory("CatAsynchronous")]
		[SRDescription("PictureBoxLoadAsync0Descr")]
		public void LoadAsync()
		{
			if (this.imageLocation == null || this.imageLocation.Length == 0)
			{
				throw new InvalidOperationException(SR.GetString("PictureBoxNoImageLocation"));
			}
			if (this.pictureBoxState[1])
			{
				return;
			}
			this.pictureBoxState[1] = true;
			if ((this.Image == null || this.imageInstallationType == PictureBox.ImageInstallationType.ErrorOrInitial) && this.InitialImage != null)
			{
				this.InstallNewImage(this.InitialImage, PictureBox.ImageInstallationType.ErrorOrInitial);
			}
			this.currentAsyncLoadOperation = AsyncOperationManager.CreateOperation(null);
			if (this.loadCompletedDelegate == null)
			{
				this.loadCompletedDelegate = new SendOrPostCallback(this.LoadCompletedDelegate);
				this.loadProgressDelegate = new SendOrPostCallback(this.LoadProgressDelegate);
				this.readBuffer = new byte[4096];
			}
			this.pictureBoxState[32] = false;
			this.pictureBoxState[2] = false;
			this.contentLength = -1;
			this.tempDownloadStream = new MemoryStream();
			WebRequest state = WebRequest.Create(this.CalculateUri(this.imageLocation));
			new WaitCallback(this.BeginGetResponseDelegate).BeginInvoke(state, null, null);
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x000E314C File Offset: 0x000E134C
		private void BeginGetResponseDelegate(object arg)
		{
			WebRequest webRequest = (WebRequest)arg;
			webRequest.BeginGetResponse(new AsyncCallback(this.GetResponseCallback), webRequest);
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x000E3174 File Offset: 0x000E1374
		private void PostCompleted(Exception error, bool cancelled)
		{
			AsyncOperation asyncOperation = this.currentAsyncLoadOperation;
			this.currentAsyncLoadOperation = null;
			if (asyncOperation != null)
			{
				asyncOperation.PostOperationCompleted(this.loadCompletedDelegate, new AsyncCompletedEventArgs(error, cancelled, null));
			}
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x000E31A8 File Offset: 0x000E13A8
		private void LoadCompletedDelegate(object arg)
		{
			AsyncCompletedEventArgs asyncCompletedEventArgs = (AsyncCompletedEventArgs)arg;
			Image value = this.ErrorImage;
			PictureBox.ImageInstallationType installationType = PictureBox.ImageInstallationType.ErrorOrInitial;
			if (!asyncCompletedEventArgs.Cancelled && asyncCompletedEventArgs.Error == null)
			{
				try
				{
					value = Image.FromStream(this.tempDownloadStream);
					installationType = PictureBox.ImageInstallationType.FromUrl;
				}
				catch (Exception error)
				{
					asyncCompletedEventArgs = new AsyncCompletedEventArgs(error, false, null);
				}
			}
			if (!asyncCompletedEventArgs.Cancelled)
			{
				this.InstallNewImage(value, installationType);
			}
			this.tempDownloadStream = null;
			this.pictureBoxState[2] = false;
			this.pictureBoxState[1] = false;
			this.OnLoadCompleted(asyncCompletedEventArgs);
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x000E323C File Offset: 0x000E143C
		private void LoadProgressDelegate(object arg)
		{
			this.OnLoadProgressChanged((ProgressChangedEventArgs)arg);
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x000E324C File Offset: 0x000E144C
		private void GetResponseCallback(IAsyncResult result)
		{
			if (this.pictureBoxState[2])
			{
				this.PostCompleted(null, true);
				return;
			}
			try
			{
				WebRequest webRequest = (WebRequest)result.AsyncState;
				WebResponse webResponse = webRequest.EndGetResponse(result);
				this.contentLength = (int)webResponse.ContentLength;
				this.totalBytesRead = 0;
				Stream responseStream = webResponse.GetResponseStream();
				responseStream.BeginRead(this.readBuffer, 0, 4096, new AsyncCallback(this.ReadCallBack), responseStream);
			}
			catch (Exception error)
			{
				this.PostCompleted(error, false);
			}
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x000E32E0 File Offset: 0x000E14E0
		private void ReadCallBack(IAsyncResult result)
		{
			if (this.pictureBoxState[2])
			{
				this.PostCompleted(null, true);
				return;
			}
			Stream stream = (Stream)result.AsyncState;
			try
			{
				int num = stream.EndRead(result);
				if (num > 0)
				{
					this.totalBytesRead += num;
					this.tempDownloadStream.Write(this.readBuffer, 0, num);
					stream.BeginRead(this.readBuffer, 0, 4096, new AsyncCallback(this.ReadCallBack), stream);
					if (this.contentLength != -1)
					{
						int progressPercentage = (int)(100f * ((float)this.totalBytesRead / (float)this.contentLength));
						if (this.currentAsyncLoadOperation != null)
						{
							this.currentAsyncLoadOperation.Post(this.loadProgressDelegate, new ProgressChangedEventArgs(progressPercentage, null));
						}
					}
				}
				else
				{
					this.tempDownloadStream.Seek(0L, SeekOrigin.Begin);
					if (this.currentAsyncLoadOperation != null)
					{
						this.currentAsyncLoadOperation.Post(this.loadProgressDelegate, new ProgressChangedEventArgs(100, null));
					}
					this.PostCompleted(null, false);
					Stream stream2 = stream;
					stream = null;
					stream2.Close();
				}
			}
			catch (Exception error)
			{
				this.PostCompleted(error, false);
				if (stream != null)
				{
					stream.Close();
				}
			}
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x000E340C File Offset: 0x000E160C
		[SRCategory("CatAsynchronous")]
		[SRDescription("PictureBoxLoadAsync1Descr")]
		public void LoadAsync(string url)
		{
			this.ImageLocation = url;
			this.LoadAsync();
		}

		// Token: 0x14000255 RID: 597
		// (add) Token: 0x060032B8 RID: 12984 RVA: 0x000E341B File Offset: 0x000E161B
		// (remove) Token: 0x060032B9 RID: 12985 RVA: 0x000E342E File Offset: 0x000E162E
		[SRCategory("CatAsynchronous")]
		[SRDescription("PictureBoxLoadCompletedDescr")]
		public event AsyncCompletedEventHandler LoadCompleted
		{
			add
			{
				base.Events.AddHandler(PictureBox.loadCompletedKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(PictureBox.loadCompletedKey, value);
			}
		}

		// Token: 0x14000256 RID: 598
		// (add) Token: 0x060032BA RID: 12986 RVA: 0x000E3441 File Offset: 0x000E1641
		// (remove) Token: 0x060032BB RID: 12987 RVA: 0x000E3454 File Offset: 0x000E1654
		[SRCategory("CatAsynchronous")]
		[SRDescription("PictureBoxLoadProgressChangedDescr")]
		public event ProgressChangedEventHandler LoadProgressChanged
		{
			add
			{
				base.Events.AddHandler(PictureBox.loadProgressChangedKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(PictureBox.loadProgressChangedKey, value);
			}
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000E3467 File Offset: 0x000E1667
		private void ResetInitialImage()
		{
			this.pictureBoxState[4] = true;
			this.initialImage = this.defaultInitialImage;
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x000E3482 File Offset: 0x000E1682
		private void ResetErrorImage()
		{
			this.pictureBoxState[8] = true;
			this.errorImage = this.defaultErrorImage;
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x000E349D File Offset: 0x000E169D
		private void ResetImage()
		{
			this.InstallNewImage(null, PictureBox.ImageInstallationType.DirectlySpecified);
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060032BF RID: 12991 RVA: 0x000E34A7 File Offset: 0x000E16A7
		// (set) Token: 0x060032C0 RID: 12992 RVA: 0x000C619D File Offset: 0x000C439D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
			}
		}

		// Token: 0x14000257 RID: 599
		// (add) Token: 0x060032C1 RID: 12993 RVA: 0x000E34AF File Offset: 0x000E16AF
		// (remove) Token: 0x060032C2 RID: 12994 RVA: 0x000E34B8 File Offset: 0x000E16B8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler RightToLeftChanged
		{
			add
			{
				base.RightToLeftChanged += value;
			}
			remove
			{
				base.RightToLeftChanged -= value;
			}
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x000E34C1 File Offset: 0x000E16C1
		private bool ShouldSerializeInitialImage()
		{
			return !this.pictureBoxState[4];
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x000E34D2 File Offset: 0x000E16D2
		private bool ShouldSerializeErrorImage()
		{
			return !this.pictureBoxState[8];
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x000E34E3 File Offset: 0x000E16E3
		private bool ShouldSerializeImage()
		{
			return this.imageInstallationType == PictureBox.ImageInstallationType.DirectlySpecified && this.Image != null;
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060032C6 RID: 12998 RVA: 0x000E34F8 File Offset: 0x000E16F8
		// (set) Token: 0x060032C7 RID: 12999 RVA: 0x000E3500 File Offset: 0x000E1700
		[DefaultValue(PictureBoxSizeMode.Normal)]
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[SRDescription("PictureBoxSizeModeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public PictureBoxSizeMode SizeMode
		{
			get
			{
				return this.sizeMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 4))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(PictureBoxSizeMode));
				}
				if (this.sizeMode != value)
				{
					if (value == PictureBoxSizeMode.AutoSize)
					{
						this.AutoSize = true;
						base.SetStyle(ControlStyles.FixedWidth | ControlStyles.FixedHeight, true);
					}
					if (value != PictureBoxSizeMode.AutoSize)
					{
						this.AutoSize = false;
						base.SetStyle(ControlStyles.FixedWidth | ControlStyles.FixedHeight, false);
						this.savedSize = base.Size;
					}
					this.sizeMode = value;
					this.AdjustSize();
					base.Invalidate();
					this.OnSizeModeChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000258 RID: 600
		// (add) Token: 0x060032C8 RID: 13000 RVA: 0x000E358E File Offset: 0x000E178E
		// (remove) Token: 0x060032C9 RID: 13001 RVA: 0x000E35A1 File Offset: 0x000E17A1
		[SRCategory("CatPropertyChanged")]
		[SRDescription("PictureBoxOnSizeModeChangedDescr")]
		public event EventHandler SizeModeChanged
		{
			add
			{
				base.Events.AddHandler(PictureBox.EVENT_SIZEMODECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(PictureBox.EVENT_SIZEMODECHANGED, value);
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x060032CA RID: 13002 RVA: 0x000B2611 File Offset: 0x000B0811
		// (set) Token: 0x060032CB RID: 13003 RVA: 0x000B2619 File Offset: 0x000B0819
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		// Token: 0x14000259 RID: 601
		// (add) Token: 0x060032CC RID: 13004 RVA: 0x000B2622 File Offset: 0x000B0822
		// (remove) Token: 0x060032CD RID: 13005 RVA: 0x000B262B File Offset: 0x000B082B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabStopChanged
		{
			add
			{
				base.TabStopChanged += value;
			}
			remove
			{
				base.TabStopChanged -= value;
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x060032CE RID: 13006 RVA: 0x000B25EE File Offset: 0x000B07EE
		// (set) Token: 0x060032CF RID: 13007 RVA: 0x000B25F6 File Offset: 0x000B07F6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new int TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		// Token: 0x1400025A RID: 602
		// (add) Token: 0x060032D0 RID: 13008 RVA: 0x000B25FF File Offset: 0x000B07FF
		// (remove) Token: 0x060032D1 RID: 13009 RVA: 0x000B2608 File Offset: 0x000B0808
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabIndexChanged
		{
			add
			{
				base.TabIndexChanged += value;
			}
			remove
			{
				base.TabIndexChanged -= value;
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x060032D2 RID: 13010 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x060032D3 RID: 13011 RVA: 0x00024185 File Offset: 0x00022385
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x1400025B RID: 603
		// (add) Token: 0x060032D4 RID: 13012 RVA: 0x00046771 File Offset: 0x00044971
		// (remove) Token: 0x060032D5 RID: 13013 RVA: 0x0004677A File Offset: 0x0004497A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		// Token: 0x1400025C RID: 604
		// (add) Token: 0x060032D6 RID: 13014 RVA: 0x000E35B4 File Offset: 0x000E17B4
		// (remove) Token: 0x060032D7 RID: 13015 RVA: 0x000E35BD File Offset: 0x000E17BD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Enter
		{
			add
			{
				base.Enter += value;
			}
			remove
			{
				base.Enter -= value;
			}
		}

		// Token: 0x1400025D RID: 605
		// (add) Token: 0x060032D8 RID: 13016 RVA: 0x000B9380 File Offset: 0x000B7580
		// (remove) Token: 0x060032D9 RID: 13017 RVA: 0x000B9389 File Offset: 0x000B7589
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyUp
		{
			add
			{
				base.KeyUp += value;
			}
			remove
			{
				base.KeyUp -= value;
			}
		}

		// Token: 0x1400025E RID: 606
		// (add) Token: 0x060032DA RID: 13018 RVA: 0x000B9392 File Offset: 0x000B7592
		// (remove) Token: 0x060032DB RID: 13019 RVA: 0x000B939B File Offset: 0x000B759B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyDown
		{
			add
			{
				base.KeyDown += value;
			}
			remove
			{
				base.KeyDown -= value;
			}
		}

		// Token: 0x1400025F RID: 607
		// (add) Token: 0x060032DC RID: 13020 RVA: 0x000B93A4 File Offset: 0x000B75A4
		// (remove) Token: 0x060032DD RID: 13021 RVA: 0x000B93AD File Offset: 0x000B75AD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyPressEventHandler KeyPress
		{
			add
			{
				base.KeyPress += value;
			}
			remove
			{
				base.KeyPress -= value;
			}
		}

		// Token: 0x14000260 RID: 608
		// (add) Token: 0x060032DE RID: 13022 RVA: 0x000E35C6 File Offset: 0x000E17C6
		// (remove) Token: 0x060032DF RID: 13023 RVA: 0x000E35CF File Offset: 0x000E17CF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Leave
		{
			add
			{
				base.Leave += value;
			}
			remove
			{
				base.Leave -= value;
			}
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x000E35D8 File Offset: 0x000E17D8
		private void AdjustSize()
		{
			if (this.sizeMode == PictureBoxSizeMode.AutoSize)
			{
				base.Size = base.PreferredSize;
				return;
			}
			base.Size = this.savedSize;
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000E35FC File Offset: 0x000E17FC
		private void Animate()
		{
			this.Animate(!base.DesignMode && base.Visible && base.Enabled && this.ParentInternal != null);
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000E3628 File Offset: 0x000E1828
		private void StopAnimate()
		{
			this.Animate(false);
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x000E3634 File Offset: 0x000E1834
		private void Animate(bool animate)
		{
			if (animate != this.currentlyAnimating)
			{
				if (animate)
				{
					if (this.image != null)
					{
						ImageAnimator.Animate(this.image, new EventHandler(this.OnFrameChanged));
						this.currentlyAnimating = animate;
						return;
					}
				}
				else if (this.image != null)
				{
					ImageAnimator.StopAnimate(this.image, new EventHandler(this.OnFrameChanged));
					this.currentlyAnimating = animate;
				}
			}
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x000E369A File Offset: 0x000E189A
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.StopAnimate();
			}
			this.DisposeImageStream();
			base.Dispose(disposing);
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000E36B2 File Offset: 0x000E18B2
		private void DisposeImageStream()
		{
			if (this.localImageStreamReader != null)
			{
				this.localImageStreamReader.Dispose();
				this.localImageStreamReader = null;
			}
			if (this.uriImageStream != null)
			{
				this.uriImageStream.Dispose();
				this.localImageStreamReader = null;
			}
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000E36E8 File Offset: 0x000E18E8
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			if (this.image == null)
			{
				return CommonProperties.GetSpecifiedBounds(this).Size;
			}
			Size sz = this.SizeFromClientSize(Size.Empty) + base.Padding.Size;
			return this.image.Size + sz;
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x000E373C File Offset: 0x000E193C
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			this.Animate();
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000E374C File Offset: 0x000E194C
		private void OnFrameChanged(object o, EventArgs e)
		{
			if (base.Disposing || base.IsDisposed)
			{
				return;
			}
			if (base.InvokeRequired && base.IsHandleCreated)
			{
				object obj = this.internalSyncObject;
				lock (obj)
				{
					if (this.handleValid)
					{
						base.BeginInvoke(new EventHandler(this.OnFrameChanged), new object[]
						{
							o,
							e
						});
					}
					return;
				}
			}
			base.Invalidate();
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000E37D8 File Offset: 0x000E19D8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleDestroyed(EventArgs e)
		{
			object obj = this.internalSyncObject;
			lock (obj)
			{
				this.handleValid = false;
			}
			base.OnHandleDestroyed(e);
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x000E3820 File Offset: 0x000E1A20
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleCreated(EventArgs e)
		{
			object obj = this.internalSyncObject;
			lock (obj)
			{
				this.handleValid = true;
			}
			base.OnHandleCreated(e);
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x000E3868 File Offset: 0x000E1A68
		protected virtual void OnLoadCompleted(AsyncCompletedEventArgs e)
		{
			AsyncCompletedEventHandler asyncCompletedEventHandler = (AsyncCompletedEventHandler)base.Events[PictureBox.loadCompletedKey];
			if (asyncCompletedEventHandler != null)
			{
				asyncCompletedEventHandler(this, e);
			}
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x000E3898 File Offset: 0x000E1A98
		protected virtual void OnLoadProgressChanged(ProgressChangedEventArgs e)
		{
			ProgressChangedEventHandler progressChangedEventHandler = (ProgressChangedEventHandler)base.Events[PictureBox.loadProgressChangedKey];
			if (progressChangedEventHandler != null)
			{
				progressChangedEventHandler(this, e);
			}
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x000E38C8 File Offset: 0x000E1AC8
		protected override void OnPaint(PaintEventArgs pe)
		{
			if (this.pictureBoxState[32])
			{
				try
				{
					if (this.WaitOnLoad)
					{
						this.Load();
					}
					else
					{
						this.LoadAsync();
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
					this.image = this.ErrorImage;
				}
			}
			if (this.image != null)
			{
				this.Animate();
				ImageAnimator.UpdateFrames(this.Image);
				Rectangle rect = (this.imageInstallationType == PictureBox.ImageInstallationType.ErrorOrInitial) ? this.ImageRectangleFromSizeMode(PictureBoxSizeMode.CenterImage) : this.ImageRectangle;
				pe.Graphics.DrawImage(this.image, rect);
			}
			base.OnPaint(pe);
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x000E3970 File Offset: 0x000E1B70
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			this.Animate();
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x000E397F File Offset: 0x000E1B7F
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			this.Animate();
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x000E398E File Offset: 0x000E1B8E
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (this.sizeMode == PictureBoxSizeMode.Zoom || this.sizeMode == PictureBoxSizeMode.StretchImage || this.sizeMode == PictureBoxSizeMode.CenterImage || this.BackgroundImage != null)
			{
				base.Invalidate();
			}
			this.savedSize = base.Size;
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x000E39CC File Offset: 0x000E1BCC
		protected virtual void OnSizeModeChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[PictureBox.EVENT_SIZEMODECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x000E39FC File Offset: 0x000E1BFC
		public override string ToString()
		{
			string str = base.ToString();
			return str + ", SizeMode: " + this.sizeMode.ToString("G");
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x060032F3 RID: 13043 RVA: 0x000E3A30 File Offset: 0x000E1C30
		// (set) Token: 0x060032F4 RID: 13044 RVA: 0x000E3A3F File Offset: 0x000E1C3F
		[SRCategory("CatAsynchronous")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("PictureBoxWaitOnLoadDescr")]
		public bool WaitOnLoad
		{
			get
			{
				return this.pictureBoxState[16];
			}
			set
			{
				this.pictureBoxState[16] = value;
			}
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x000E3A4F File Offset: 0x000E1C4F
		void ISupportInitialize.BeginInit()
		{
			this.pictureBoxState[64] = true;
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x000E3A5F File Offset: 0x000E1C5F
		void ISupportInitialize.EndInit()
		{
			if (this.ImageLocation != null && this.ImageLocation.Length != 0 && this.WaitOnLoad)
			{
				this.Load();
			}
			this.pictureBoxState[64] = false;
		}

		// Token: 0x04001E7E RID: 7806
		private BorderStyle borderStyle;

		// Token: 0x04001E7F RID: 7807
		private Image image;

		// Token: 0x04001E80 RID: 7808
		private PictureBoxSizeMode sizeMode;

		// Token: 0x04001E81 RID: 7809
		private Size savedSize;

		// Token: 0x04001E82 RID: 7810
		private bool currentlyAnimating;

		// Token: 0x04001E83 RID: 7811
		private AsyncOperation currentAsyncLoadOperation;

		// Token: 0x04001E84 RID: 7812
		private string imageLocation;

		// Token: 0x04001E85 RID: 7813
		private Image initialImage;

		// Token: 0x04001E86 RID: 7814
		private Image errorImage;

		// Token: 0x04001E87 RID: 7815
		private int contentLength;

		// Token: 0x04001E88 RID: 7816
		private int totalBytesRead;

		// Token: 0x04001E89 RID: 7817
		private MemoryStream tempDownloadStream;

		// Token: 0x04001E8A RID: 7818
		private const int readBlockSize = 4096;

		// Token: 0x04001E8B RID: 7819
		private byte[] readBuffer;

		// Token: 0x04001E8C RID: 7820
		private PictureBox.ImageInstallationType imageInstallationType;

		// Token: 0x04001E8D RID: 7821
		private SendOrPostCallback loadCompletedDelegate;

		// Token: 0x04001E8E RID: 7822
		private SendOrPostCallback loadProgressDelegate;

		// Token: 0x04001E8F RID: 7823
		private bool handleValid;

		// Token: 0x04001E90 RID: 7824
		private object internalSyncObject = new object();

		// Token: 0x04001E91 RID: 7825
		private Image defaultInitialImage;

		// Token: 0x04001E92 RID: 7826
		private Image defaultErrorImage;

		// Token: 0x04001E93 RID: 7827
		[ThreadStatic]
		private static Image defaultInitialImageForThread = null;

		// Token: 0x04001E94 RID: 7828
		[ThreadStatic]
		private static Image defaultErrorImageForThread = null;

		// Token: 0x04001E95 RID: 7829
		private static readonly object defaultInitialImageKey = new object();

		// Token: 0x04001E96 RID: 7830
		private static readonly object defaultErrorImageKey = new object();

		// Token: 0x04001E97 RID: 7831
		private static readonly object loadCompletedKey = new object();

		// Token: 0x04001E98 RID: 7832
		private static readonly object loadProgressChangedKey = new object();

		// Token: 0x04001E99 RID: 7833
		private const int PICTUREBOXSTATE_asyncOperationInProgress = 1;

		// Token: 0x04001E9A RID: 7834
		private const int PICTUREBOXSTATE_cancellationPending = 2;

		// Token: 0x04001E9B RID: 7835
		private const int PICTUREBOXSTATE_useDefaultInitialImage = 4;

		// Token: 0x04001E9C RID: 7836
		private const int PICTUREBOXSTATE_useDefaultErrorImage = 8;

		// Token: 0x04001E9D RID: 7837
		private const int PICTUREBOXSTATE_waitOnLoad = 16;

		// Token: 0x04001E9E RID: 7838
		private const int PICTUREBOXSTATE_needToLoadImageLocation = 32;

		// Token: 0x04001E9F RID: 7839
		private const int PICTUREBOXSTATE_inInitialization = 64;

		// Token: 0x04001EA0 RID: 7840
		private BitVector32 pictureBoxState;

		// Token: 0x04001EA1 RID: 7841
		private StreamReader localImageStreamReader;

		// Token: 0x04001EA2 RID: 7842
		private Stream uriImageStream;

		// Token: 0x04001EA3 RID: 7843
		private static readonly object EVENT_SIZEMODECHANGED = new object();

		// Token: 0x020007CB RID: 1995
		private enum ImageInstallationType
		{
			// Token: 0x040041C7 RID: 16839
			DirectlySpecified,
			// Token: 0x040041C8 RID: 16840
			ErrorOrInitial,
			// Token: 0x040041C9 RID: 16841
			FromUrl
		}
	}
}
