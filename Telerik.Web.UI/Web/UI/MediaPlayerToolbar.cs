using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020005C7 RID: 1479
	public class MediaPlayerToolbar : CompositeControl
	{
		// Token: 0x06003507 RID: 13575 RVA: 0x000AF2D0 File Offset: 0x000AD4D0
		public MediaPlayerToolbar(RadMediaPlayer ownerMediaPlayer)
		{
			this.owner = ownerMediaPlayer;
			this.ID = "Toolbar";
		}

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x06003508 RID: 13576 RVA: 0x000AF2EA File Offset: 0x000AD4EA
		public RadSlider ProgressRail
		{
			get
			{
				this.EnsureChildControls();
				return this.progressRail;
			}
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x06003509 RID: 13577 RVA: 0x000AF2F8 File Offset: 0x000AD4F8
		public RadSlider VolumeControl
		{
			get
			{
				this.EnsureChildControls();
				return this.volumeControl;
			}
		}

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x0600350A RID: 13578 RVA: 0x000AF306 File Offset: 0x000AD506
		public HtmlButton PlayButton
		{
			get
			{
				this.EnsureChildControls();
				return this.playButton;
			}
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x0600350B RID: 13579 RVA: 0x000AF314 File Offset: 0x000AD514
		public HtmlButton PlayButtonCenter
		{
			get
			{
				this.EnsureChildControls();
				return this.playButtonCenter;
			}
		}

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x0600350C RID: 13580 RVA: 0x000AF322 File Offset: 0x000AD522
		public HtmlButton VolumeControlButton
		{
			get
			{
				this.EnsureChildControls();
				return this.volumeControlButton;
			}
		}

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x0600350D RID: 13581 RVA: 0x000AF330 File Offset: 0x000AD530
		public HtmlButton HDButton
		{
			get
			{
				this.EnsureChildControls();
				return this.hdButton;
			}
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x0600350E RID: 13582 RVA: 0x000AF33E File Offset: 0x000AD53E
		public HtmlButton SubtitlesButton
		{
			get
			{
				this.EnsureChildControls();
				return this.subtitlesButton;
			}
		}

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x0600350F RID: 13583 RVA: 0x000AF34C File Offset: 0x000AD54C
		public HtmlButton FullScreenButton
		{
			get
			{
				this.EnsureChildControls();
				return this.fsButton;
			}
		}

		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x06003510 RID: 13584 RVA: 0x000AF35A File Offset: 0x000AD55A
		public Label CurrentTimeDisplay
		{
			get
			{
				this.EnsureChildControls();
				return this.timeDisplay;
			}
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x06003511 RID: 13585 RVA: 0x000AF368 File Offset: 0x000AD568
		public Label DurationDisplay
		{
			get
			{
				this.EnsureChildControls();
				return this.durationDisplay;
			}
		}

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06003512 RID: 13586 RVA: 0x000AF376 File Offset: 0x000AD576
		public Literal TimeDisplaySeparator
		{
			get
			{
				this.EnsureChildControls();
				return this.timeDisplaySeparator;
			}
		}

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x000AF384 File Offset: 0x000AD584
		public RadMediaPlayer OwnerMediaPlayer
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x000AF38C File Offset: 0x000AD58C
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			this.progressRail = new RadSlider
			{
				ID = "ProgressRail",
				RenderMode = this.owner.ResolvedRenderMode,
				EnableServerSideRendering = true,
				ShowDragHandle = true,
				ShowIncreaseHandle = false,
				ShowDecreaseHandle = false,
				Skin = this.OwnerMediaPlayer.Skin,
				SmallChange = 0.01m,
				Width = Unit.Pixel((int)(this.OwnerMediaPlayer.Width.Value * 4.0 / 8.0))
			};
			this.volumeControl = new RadSlider
			{
				ID = "VolumeControl",
				RenderMode = this.owner.ResolvedRenderMode,
				EnableServerSideRendering = true,
				ShowDragHandle = true,
				ShowIncreaseHandle = false,
				ShowDecreaseHandle = false,
				Orientation = Orientation.Vertical,
				MaximumValue = 100m,
				MinimumValue = 0m,
				Height = Unit.Pixel(80),
				IsDirectionReversed = true,
				Skin = this.OwnerMediaPlayer.Skin
			};
			if (this.owner.ResolvedRenderMode == RenderMode.Mobile)
			{
				this.volumeControl.Height = Unit.Pixel(22);
				this.volumeControl.IsDirectionReversed = false;
				this.volumeControl.Width = Unit.Pixel(100);
				this.volumeControl.Orientation = Orientation.Horizontal;
				this.owner.VolumeButtonToolTip = "";
			}
			this.volumeControl.PreRender += this.OwnerMediaPlayer.HandleChildControlsPreRender;
			this.progressRail.PreRender += this.OwnerMediaPlayer.HandleChildControlsPreRender;
			this.playButton = MediaPlayerToolbar.InitializeButtonControl("PlayButton", "Play", this.OwnerMediaPlayer.PlayButtonToolTip);
			this.volumeControlButton = MediaPlayerToolbar.InitializeButtonControl("VolumeControlButton", "Volume", this.OwnerMediaPlayer.VolumeButtonToolTip);
			this.hdButton = MediaPlayerToolbar.InitializeButtonControl("HDButton", "HD", this.OwnerMediaPlayer.HDButtonToolTip);
			this.subtitlesButton = MediaPlayerToolbar.InitializeButtonControl("SubtitlesButton", "Subtitles", this.OwnerMediaPlayer.SubtitlesButtonToolTip);
			this.fsButton = MediaPlayerToolbar.InitializeButtonControl("FSButton", "FullScr", this.OwnerMediaPlayer.FullScreenButtonToolTip);
			this.playButtonCenter = MediaPlayerToolbar.InitializeButtonControl("PlayButtonCenter", "BigPlay", this.OwnerMediaPlayer.PlayButtonToolTip);
			this.timeDisplay = new Label
			{
				ID = "CurrentTimeDisplay",
				CssClass = "rmpCurrentTime",
				Text = "0:00"
			};
			this.durationDisplay = new Label
			{
				ID = "DurationDisplay",
				CssClass = "rmpDurationTime",
				Text = "0:00"
			};
			this.timeDisplaySeparator = new Literal
			{
				Text = " / "
			};
			this.Controls.Add(this.timeDisplay);
			this.Controls.Add(this.timeDisplaySeparator);
			this.Controls.Add(this.durationDisplay);
			this.Controls.Add(this.playButton);
			this.Controls.Add(this.playButtonCenter);
			this.Controls.Add(this.progressRail);
			this.Controls.Add(this.volumeControlButton);
			this.Controls.Add(this.volumeControl);
			this.Controls.Add(this.subtitlesButton);
			this.Controls.Add(this.hdButton);
			this.Controls.Add(this.fsButton);
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x000AF74C File Offset: 0x000AD94C
		internal static HtmlButton InitializeButtonControl(string ID, string prefix, string tooltip)
		{
			HtmlButton htmlButton = new HtmlButton
			{
				ID = ID
			};
			string value = string.Format("rmpActionButton rmp{0}Button", prefix);
			string cssClass = string.Format("rmpIcon rmp{0}Icon", prefix);
			htmlButton.Attributes.Add("type", "button");
			htmlButton.Attributes.Add("class", value);
			htmlButton.Attributes.Add("title", tooltip);
			htmlButton.Controls.Add(new Label
			{
				CssClass = cssClass
			});
			return htmlButton;
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x000AF7D4 File Offset: 0x000AD9D4
		protected override void Render(HtmlTextWriter writer)
		{
			this.PlayButtonCenter.RenderControl(writer);
			if (this.owner.ResolvedRenderMode != RenderMode.Mobile)
			{
				this.ClassicModeRenderContent(writer);
				return;
			}
			this.MobileModeRenderContent(writer);
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x000AF800 File Offset: 0x000ADA00
		private void ClassicModeRenderContent(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpToolbarWrapper");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_Wrapper");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpToolbar");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpLeftControlsSet");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.PlayButton.RenderControl(writer);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpSeekBar");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.ProgressRail.RenderControl(writer);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpRightControlsSet");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpProgressText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			this.CurrentTimeDisplay.RenderControl(writer);
			this.TimeDisplaySeparator.RenderControl(writer);
			this.DurationDisplay.RenderControl(writer);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpVolContr");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.VolumeControlButton.RenderControl(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpVolContrBar");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.VolumeControl.RenderControl(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
			this.SubtitlesButton.RenderControl(writer);
			this.HDButton.RenderControl(writer);
			this.FullScreenButton.RenderControl(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x000AF984 File Offset: 0x000ADB84
		private void MobileModeRenderContent(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpToolbarWrapper");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_Wrapper");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpToolbar");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpSeekBar");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.ProgressRail.RenderControl(writer);
			writer.RenderEndTag();
			this.CurrentTimeDisplay.RenderControl(writer);
			this.DurationDisplay.RenderControl(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpControlsSet");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.PlayButton.RenderControl(writer);
			this.VolumeControlButton.RenderControl(writer);
			this.SubtitlesButton.RenderControl(writer);
			this.HDButton.RenderControl(writer);
			this.FullScreenButton.RenderControl(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpVolContrBar");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.VolumeControl.RenderControl(writer);
			writer.RenderEndTag();
		}

		// Token: 0x04000E57 RID: 3671
		private RadSlider progressRail;

		// Token: 0x04000E58 RID: 3672
		private RadSlider volumeControl;

		// Token: 0x04000E59 RID: 3673
		private HtmlButton playButton;

		// Token: 0x04000E5A RID: 3674
		private HtmlButton volumeControlButton;

		// Token: 0x04000E5B RID: 3675
		private HtmlButton playButtonCenter;

		// Token: 0x04000E5C RID: 3676
		private HtmlButton subtitlesButton;

		// Token: 0x04000E5D RID: 3677
		private HtmlButton hdButton;

		// Token: 0x04000E5E RID: 3678
		private HtmlButton fsButton;

		// Token: 0x04000E5F RID: 3679
		private Label durationDisplay;

		// Token: 0x04000E60 RID: 3680
		private Label timeDisplay;

		// Token: 0x04000E61 RID: 3681
		private Literal timeDisplaySeparator;

		// Token: 0x04000E62 RID: 3682
		private RadMediaPlayer owner;
	}
}
