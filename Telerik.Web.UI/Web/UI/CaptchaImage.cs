using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Text;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x020016CC RID: 5836
	[Serializable]
	public class CaptchaImage
	{
		// Token: 0x170044FA RID: 17658
		// (get) Token: 0x0600E10C RID: 57612 RVA: 0x00320028 File Offset: 0x0031E228
		public string UniqueId
		{
			get
			{
				return this._guid;
			}
		}

		// Token: 0x170044FB RID: 17659
		// (get) Token: 0x0600E10D RID: 57613 RVA: 0x00320030 File Offset: 0x0031E230
		public DateTime RenderedAt
		{
			get
			{
				return this._generatedAt;
			}
		}

		// Token: 0x170044FC RID: 17660
		// (get) Token: 0x0600E10E RID: 57614 RVA: 0x00320038 File Offset: 0x0031E238
		// (set) Token: 0x0600E10F RID: 57615 RVA: 0x00320040 File Offset: 0x0031E240
		[DefaultValue(false)]
		[Description("Gets or sets bool value that indicates whether the RadCaptcha image will only be rendered on the page.")]
		public bool RenderImageOnly
		{
			get
			{
				return this._renderImageOnly;
			}
			set
			{
				this._renderImageOnly = value;
			}
		}

		// Token: 0x170044FD RID: 17661
		// (get) Token: 0x0600E110 RID: 57616 RVA: 0x00320049 File Offset: 0x0031E249
		// (set) Token: 0x0600E111 RID: 57617 RVA: 0x0032005C File Offset: 0x0031E25C
		[Description("Font used to render RadCaptcha text. If font name is blank, a random font will be chosen.")]
		[DefaultValue("Courier New")]
		public string FontFamily
		{
			get
			{
				return this._fontFamily ?? "Courier New";
			}
			set
			{
				try
				{
					Font font = new Font(value, 12f);
					this._fontFamily = value;
					font.Dispose();
				}
				catch
				{
					this._fontFamily = "Courier New";
				}
			}
		}

		// Token: 0x170044FE RID: 17662
		// (get) Token: 0x0600E112 RID: 57618 RVA: 0x003200A4 File Offset: 0x0031E2A4
		// (set) Token: 0x0600E113 RID: 57619 RVA: 0x003200AC File Offset: 0x0031E2AC
		[Description("Gets or sets a bool value indicating whether a random font will be used to generate the CaptchaImage text.")]
		[DefaultValue(false)]
		public bool UseRandomFont
		{
			get
			{
				return this._useRandomFont;
			}
			set
			{
				this._useRandomFont = value;
			}
		}

		// Token: 0x170044FF RID: 17663
		// (get) Token: 0x0600E114 RID: 57620 RVA: 0x003200B5 File Offset: 0x0031E2B5
		// (set) Token: 0x0600E115 RID: 57621 RVA: 0x003200BD File Offset: 0x0031E2BD
		[DefaultValue(typeof(CaptchaFontWarpFactor), "Medium")]
		[Description("Amount of random font warping used on the RadCaptcha text")]
		public CaptchaFontWarpFactor FontWarp
		{
			get
			{
				return this._fontWarp;
			}
			set
			{
				this._fontWarp = value;
			}
		}

		// Token: 0x17004500 RID: 17664
		// (get) Token: 0x0600E116 RID: 57622 RVA: 0x003200C6 File Offset: 0x0031E2C6
		// (set) Token: 0x0600E117 RID: 57623 RVA: 0x003200CE File Offset: 0x0031E2CE
		[DefaultValue(typeof(CaptchaBackgroundNoiseLevel), "Low")]
		[Description("Amount of background noise to generate in the RadCaptcha image")]
		public CaptchaBackgroundNoiseLevel BackgroundNoise
		{
			get
			{
				return this._backgroundNoise;
			}
			set
			{
				this._backgroundNoise = value;
			}
		}

		// Token: 0x17004501 RID: 17665
		// (get) Token: 0x0600E118 RID: 57624 RVA: 0x003200D7 File Offset: 0x0031E2D7
		// (set) Token: 0x0600E119 RID: 57625 RVA: 0x003200DF File Offset: 0x0031E2DF
		[DefaultValue(typeof(CaptchaLineNoiseLevel), "Low")]
		[Description("Add line noise to the RadCaptcha image")]
		public CaptchaLineNoiseLevel LineNoise
		{
			get
			{
				return this._lineNoise;
			}
			set
			{
				this._lineNoise = value;
			}
		}

		// Token: 0x17004502 RID: 17666
		// (get) Token: 0x0600E11A RID: 57626 RVA: 0x003200E8 File Offset: 0x0031E2E8
		// (set) Token: 0x0600E11B RID: 57627 RVA: 0x003200F0 File Offset: 0x0031E2F0
		[DefaultValue(CaptchaPossibleChars.LettersAndNumbers)]
		[Description("Characters used to render RadCaptcha text. A character will be picked randomly from the string.")]
		public CaptchaPossibleChars TextChars
		{
			get
			{
				return this._textChars;
			}
			set
			{
				this._textChars = value;
				this.GenerateCode(true);
			}
		}

		// Token: 0x17004503 RID: 17667
		// (get) Token: 0x0600E11C RID: 57628 RVA: 0x00320100 File Offset: 0x0031E300
		// (set) Token: 0x0600E11D RID: 57629 RVA: 0x00320108 File Offset: 0x0031E308
		[Description("Gets or sets a custom Character Set, from which the characters used to render RadCaptcha, are randomly chosen.")]
		[DefaultValue("ABCDEFGHJKLMNOPQRSTUVWXYZ123456789")]
		public string CharSet
		{
			get
			{
				return this._charset;
			}
			set
			{
				if (value.Length < 15)
				{
					throw new ArgumentOutOfRangeException("CharSet", value.Length, "The CharSet must contain at least 15 characters.");
				}
				this._charset = value;
				this.GenerateCode(true);
			}
		}

		// Token: 0x17004504 RID: 17668
		// (get) Token: 0x0600E11E RID: 57630 RVA: 0x0032013D File Offset: 0x0031E33D
		// (set) Token: 0x0600E11F RID: 57631 RVA: 0x00320145 File Offset: 0x0031E345
		[Description("Color used to render the RadCaptcha text.")]
		[DefaultValue(typeof(Color), "Gray")]
		public Color TextColor
		{
			get
			{
				return this._textColor;
			}
			set
			{
				this._textColor = value;
			}
		}

		// Token: 0x17004505 RID: 17669
		// (get) Token: 0x0600E120 RID: 57632 RVA: 0x0032014E File Offset: 0x0031E34E
		// (set) Token: 0x0600E121 RID: 57633 RVA: 0x00320156 File Offset: 0x0031E356
		[Description("Background color of the CaptchaImage.")]
		[DefaultValue(typeof(Color), "White")]
		public Color BackgroundColor
		{
			get
			{
				return this._backgroundColor;
			}
			set
			{
				this._backgroundColor = value;
			}
		}

		// Token: 0x17004506 RID: 17670
		// (get) Token: 0x0600E122 RID: 57634 RVA: 0x0032015F File Offset: 0x0031E35F
		// (set) Token: 0x0600E123 RID: 57635 RVA: 0x00320167 File Offset: 0x0031E367
		[Description("Number of CaptchaPossibleChars used in the RadCaptcha text")]
		[DefaultValue(5)]
		public int TextLength
		{
			get
			{
				return this._textLength;
			}
			set
			{
				this._textLength = value;
				this.GenerateCode(true);
			}
		}

		// Token: 0x17004507 RID: 17671
		// (get) Token: 0x0600E124 RID: 57636 RVA: 0x00320177 File Offset: 0x0031E377
		public string Text
		{
			get
			{
				return this._randomText;
			}
		}

		// Token: 0x17004508 RID: 17672
		// (get) Token: 0x0600E125 RID: 57637 RVA: 0x0032017F File Offset: 0x0031E37F
		// (set) Token: 0x0600E126 RID: 57638 RVA: 0x00320187 File Offset: 0x0031E387
		[Description("Width of generated RadCaptcha image.")]
		[DefaultValue(180)]
		public int Width
		{
			get
			{
				return this._width;
			}
			set
			{
				if (value <= 60)
				{
					throw new ArgumentOutOfRangeException("width", value, "Width must be greater than 60.");
				}
				this._width = value;
			}
		}

		// Token: 0x17004509 RID: 17673
		// (get) Token: 0x0600E127 RID: 57639 RVA: 0x003201AB File Offset: 0x0031E3AB
		// (set) Token: 0x0600E128 RID: 57640 RVA: 0x003201B3 File Offset: 0x0031E3B3
		[Description("Height of generated RadCaptcha image.")]
		[DefaultValue(50)]
		public int Height
		{
			get
			{
				return this._height;
			}
			set
			{
				if (value <= 30)
				{
					throw new ArgumentOutOfRangeException("height", value, "Height must be greater than 30.");
				}
				this._height = value;
			}
		}

		// Token: 0x1700450A RID: 17674
		// (get) Token: 0x0600E129 RID: 57641 RVA: 0x003201D7 File Offset: 0x0031E3D7
		// (set) Token: 0x0600E12A RID: 57642 RVA: 0x003201DF File Offset: 0x0031E3DF
		[Description("A semicolon-delimited list of valid fonts to use when no font is provided.")]
		[DefaultValue("")]
		public string FontWhitelist
		{
			get
			{
				return this._fontWhitelist;
			}
			set
			{
				this._fontWhitelist = value;
			}
		}

		// Token: 0x1700450B RID: 17675
		// (get) Token: 0x0600E12B RID: 57643 RVA: 0x003201E8 File Offset: 0x0031E3E8
		// (set) Token: 0x0600E12C RID: 57644 RVA: 0x003201F0 File Offset: 0x0031E3F0
		[Description("The RadCaptcha image alternative text.")]
		[DefaultValue("")]
		public string ImageAlternativeText
		{
			get
			{
				return this._imageAlternativeText;
			}
			set
			{
				this._imageAlternativeText = value;
			}
		}

		// Token: 0x1700450C RID: 17676
		// (get) Token: 0x0600E12D RID: 57645 RVA: 0x003201F9 File Offset: 0x0031E3F9
		// (set) Token: 0x0600E12E RID: 57646 RVA: 0x00320201 File Offset: 0x0031E401
		[Description("The RadCaptcha image CSS class.")]
		[DefaultValue("")]
		public string ImageCssClass
		{
			get
			{
				return this._imageCssClass;
			}
			set
			{
				this._imageCssClass = value;
			}
		}

		// Token: 0x1700450D RID: 17677
		// (get) Token: 0x0600E12F RID: 57647 RVA: 0x0032020A File Offset: 0x0031E40A
		// (set) Token: 0x0600E130 RID: 57648 RVA: 0x00320212 File Offset: 0x0031E412
		[Description("Gets or sets the bool value indicating whether the CaptchaAudio will be enabled.")]
		[DefaultValue(false)]
		public bool EnableCaptchaAudio
		{
			get
			{
				return this._enableCaptchaAudio;
			}
			set
			{
				this._enableCaptchaAudio = value;
			}
		}

		// Token: 0x1700450E RID: 17678
		// (get) Token: 0x0600E131 RID: 57649 RVA: 0x0032021B File Offset: 0x0031E41B
		// (set) Token: 0x0600E132 RID: 57650 RVA: 0x00320223 File Offset: 0x0031E423
		[Description("Gets or sets the path to the directory where the audio (.wav) files are located.")]
		[DefaultValue("~/App_Data/RadCaptcha")]
		public string AudioFilesPath
		{
			get
			{
				return this._audioFolderUrl;
			}
			set
			{
				this._audioFolderUrl = value;
			}
		}

		// Token: 0x1700450F RID: 17679
		// (get) Token: 0x0600E133 RID: 57651 RVA: 0x0032022C File Offset: 0x0031E42C
		// (set) Token: 0x0600E134 RID: 57652 RVA: 0x00320234 File Offset: 0x0031E434
		[Description("Gets or sets a bool value indicating whether the audio code will be generated by concatenation of the audio files from a given folder.")]
		[DefaultValue(false)]
		public bool UseAudioFiles
		{
			get
			{
				return this._useCustomAudioFiles;
			}
			set
			{
				this._useCustomAudioFiles = value;
			}
		}

		// Token: 0x17004510 RID: 17680
		// (get) Token: 0x0600E135 RID: 57653 RVA: 0x0032023D File Offset: 0x0031E43D
		// (set) Token: 0x0600E136 RID: 57654 RVA: 0x00320245 File Offset: 0x0031E445
		[DefaultValue(false)]
		[Description("Gets or sets a boolean value indicating whether a noise should be added to the CaptchaAudio.")]
		public bool EnableAudioNoise
		{
			get
			{
				return this._enableAudioNoise;
			}
			set
			{
				this._enableAudioNoise = value;
			}
		}

		// Token: 0x17004511 RID: 17681
		// (get) Token: 0x0600E137 RID: 57655 RVA: 0x0032024E File Offset: 0x0031E44E
		// (set) Token: 0x0600E138 RID: 57656 RVA: 0x00320256 File Offset: 0x0031E456
		public string PreviousText { get; private set; }

		// Token: 0x17004512 RID: 17682
		// (get) Token: 0x0600E139 RID: 57657 RVA: 0x0032025F File Offset: 0x0031E45F
		// (set) Token: 0x0600E13A RID: 57658 RVA: 0x00320267 File Offset: 0x0031E467
		[DefaultValue(false)]
		[Description("Gets or sets a bool value that indicates whether or not the Captcha will persist the code during Ajax requests that do not affect the RadCaptcha control.")]
		public bool PersistCodeDuringAjax { get; set; }

		// Token: 0x17004513 RID: 17683
		// (get) Token: 0x0600E13B RID: 57659 RVA: 0x00320270 File Offset: 0x0031E470
		internal TelerikRandom RandomGenerator
		{
			get
			{
				return this._rand;
			}
		}

		// Token: 0x0600E13C RID: 57660 RVA: 0x00320278 File Offset: 0x0031E478
		private void InitCaptchaVars()
		{
			this.InitCaptchaVars(new TelerikRandom(), DateTime.Now, Guid.NewGuid().ToString());
		}

		// Token: 0x0600E13D RID: 57661 RVA: 0x003202A8 File Offset: 0x0031E4A8
		internal void InitCaptchaVars(TelerikRandom rnd, DateTime generated, string uniqueGuid)
		{
			this._rand = rnd;
			this.GenerateCode(true);
			this._generatedAt = generated;
			this._guid = uniqueGuid;
		}

		// Token: 0x0600E13E RID: 57662 RVA: 0x003202C8 File Offset: 0x0031E4C8
		public CaptchaImage()
		{
			this.InitCaptchaVars();
		}

		// Token: 0x0600E13F RID: 57663 RVA: 0x003206E7 File Offset: 0x0031E8E7
		[Obsolete("Please use the other public constructor with TelerikRandom instead of Random as the second parameter")]
		public CaptchaImage(CaptchaImage oldImage, Random rnd, DateTime generated, string uniqueGuid) : this(oldImage, new TelerikRandom(), generated, uniqueGuid)
		{
		}

		// Token: 0x0600E140 RID: 57664 RVA: 0x003206F8 File Offset: 0x0031E8F8
		public CaptchaImage(CaptchaImage oldImage, TelerikRandom rnd, DateTime generated, string uniqueGuid)
		{
			this.InitCaptchaVars(rnd, generated, uniqueGuid);
			this.BackgroundNoise = oldImage.BackgroundNoise;
			this.FontFamily = oldImage.FontFamily;
			this.FontWarp = oldImage.FontWarp;
			this.FontWhitelist = oldImage.FontWhitelist;
			this.Height = oldImage.Height;
			this.ImageAlternativeText = oldImage.ImageAlternativeText;
			this.ImageCssClass = oldImage.ImageCssClass;
			this.LineNoise = oldImage.LineNoise;
			this.CharSet = oldImage.CharSet;
			this.TextChars = oldImage.TextChars;
			this.TextColor = oldImage.TextColor;
			this.TextLength = oldImage.TextLength;
			this.Width = oldImage.Width;
			this.RenderImageOnly = oldImage.RenderImageOnly;
			this.BackgroundColor = oldImage.BackgroundColor;
			this.UseRandomFont = oldImage.UseRandomFont;
			this.EnableCaptchaAudio = oldImage.EnableCaptchaAudio;
			this.AudioFilesPath = oldImage.AudioFilesPath;
			this.UseAudioFiles = oldImage.UseAudioFiles;
			this.EnableAudioNoise = oldImage.EnableAudioNoise;
			this.PreviousText = oldImage.Text;
			this.PersistCodeDuringAjax = oldImage.PersistCodeDuringAjax;
		}

		// Token: 0x0600E141 RID: 57665 RVA: 0x00320C23 File Offset: 0x0031EE23
		internal void OnPreRender(object args)
		{
			if (this.PersistCodeDuringAjax && !string.IsNullOrEmpty(this.PreviousText))
			{
				this._randomText = this.PreviousText;
			}
		}

		// Token: 0x0600E142 RID: 57666 RVA: 0x00320C46 File Offset: 0x0031EE46
		public Bitmap RenderImage()
		{
			if (this.PersistCodeDuringAjax)
			{
				this.GenerateCode(true);
			}
			return this.GenerateImagePrivate();
		}

		// Token: 0x0600E143 RID: 57667 RVA: 0x00320C60 File Offset: 0x0031EE60
		private string RandomFontFamily()
		{
			string text = this.FontWhitelist;
			if (string.IsNullOrEmpty(text))
			{
				text = "arial;arial black;comic sans ms;courier new;estrangelo edessa;franklin gothic medium;georgia;lucida console;lucida sans unicode;mangal;microsoft sans serif;palatino linotype;sylfaen;tahoma;times new roman;trebuchet ms;verdana";
			}
			string[] array = text.Split(";".ToCharArray());
			return array[this._rand.GetInt(0, array.Length)];
		}

		// Token: 0x0600E144 RID: 57668 RVA: 0x00320CA8 File Offset: 0x0031EEA8
		private bool IsOffensiveWord()
		{
			string text = this._randomText.ToLower(CultureInfo.InvariantCulture);
			foreach (string value in this._wordsToExclude)
			{
				if (text.Contains(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600E145 RID: 57669 RVA: 0x00320CF4 File Offset: 0x0031EEF4
		public void GenerateCode(bool filterWords)
		{
			do
			{
				this.GenerateCode();
			}
			while (filterWords && this.IsOffensiveWord());
		}

		// Token: 0x0600E146 RID: 57670 RVA: 0x00320D07 File Offset: 0x0031EF07
		public void GenerateCode()
		{
			this._randomText = this.GenerateRandomText();
		}

		// Token: 0x0600E147 RID: 57671 RVA: 0x00320D18 File Offset: 0x0031EF18
		private string GenerateRandomText()
		{
			StringBuilder stringBuilder = new StringBuilder(this.TextLength);
			string text = string.Empty;
			switch (this.TextChars)
			{
			case CaptchaPossibleChars.LettersAndNumbers:
				text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
				break;
			case CaptchaPossibleChars.Letters:
				text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
				break;
			case CaptchaPossibleChars.Numbers:
				text = "0123456789";
				break;
			case CaptchaPossibleChars.CustomCharSet:
				text = this.CharSet;
				break;
			}
			int length = text.Length;
			for (int i = 0; i <= this.TextLength - 1; i++)
			{
				stringBuilder.Append(text.Substring(this._rand.GetInt(length), 1));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600E148 RID: 57672 RVA: 0x00320DB1 File Offset: 0x0031EFB1
		private PointF RandomPoint(int xMin, int xMax, int yMin, int yMax)
		{
			return new PointF((float)this._rand.GetInt(xMin, xMax), (float)this._rand.GetInt(yMin, yMax));
		}

		// Token: 0x0600E149 RID: 57673 RVA: 0x00320DD5 File Offset: 0x0031EFD5
		private PointF RandomPoint(Rectangle rect)
		{
			return this.RandomPoint(rect.Left, rect.Width, rect.Top, rect.Bottom);
		}

		// Token: 0x0600E14A RID: 57674 RVA: 0x00320DFC File Offset: 0x0031EFFC
		private static GraphicsPath TextPath(string str, Font font, Rectangle rect)
		{
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = StringAlignment.Near;
			stringFormat.LineAlignment = StringAlignment.Near;
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddString(str, font.FontFamily, (int)font.Style, font.Size, rect, stringFormat);
			return graphicsPath;
		}

		// Token: 0x0600E14B RID: 57675 RVA: 0x00320E40 File Offset: 0x0031F040
		private Font GetFont()
		{
			float emSize = 0f;
			string text = this.FontFamily;
			if (string.IsNullOrEmpty(text) || this.UseRandomFont)
			{
				text = this.RandomFontFamily();
			}
			switch (this.FontWarp)
			{
			case CaptchaFontWarpFactor.None:
				emSize = (float)Convert.ToInt32((double)this.Height * 0.7);
				break;
			case CaptchaFontWarpFactor.Low:
				emSize = (float)Convert.ToInt32((double)this.Height * 0.8);
				break;
			case CaptchaFontWarpFactor.Medium:
				emSize = (float)Convert.ToInt32((double)this.Height * 0.85);
				break;
			case CaptchaFontWarpFactor.High:
				emSize = (float)Convert.ToInt32((double)this.Height * 0.9);
				break;
			case CaptchaFontWarpFactor.Extreme:
				emSize = (float)Convert.ToInt32((double)this.Height * 0.95);
				break;
			}
			return new Font(text, emSize, FontStyle.Bold);
		}

		// Token: 0x0600E14C RID: 57676 RVA: 0x00320F20 File Offset: 0x0031F120
		private Bitmap GenerateImagePrivate()
		{
			Font font = null;
			Bitmap bitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
			Brush brush = new SolidBrush(this.BackgroundColor);
			graphics.FillRectangle(brush, rect);
			int num = 0;
			double num2 = (double)(this.Width / this.TextLength);
			foreach (char c in this._randomText)
			{
				font = this.GetFont();
				Rectangle rect2 = new Rectangle(Convert.ToInt32((double)num * num2), 0, Convert.ToInt32(num2), this.Height);
				GraphicsPath graphicsPath = CaptchaImage.TextPath(c.ToString(), font, rect2);
				this.WarpText(graphicsPath, rect2);
				brush = new SolidBrush(this.TextColor);
				graphics.FillPath(brush, graphicsPath);
				num++;
			}
			this.AddNoise(graphics, rect);
			this.AddLine(graphics, rect);
			font.Dispose();
			brush.Dispose();
			graphics.Dispose();
			return bitmap;
		}

		// Token: 0x0600E14D RID: 57677 RVA: 0x00321040 File Offset: 0x0031F240
		private void WarpText(GraphicsPath textPath, Rectangle rect)
		{
			float num = 0f;
			float num2 = 0f;
			switch (this.FontWarp)
			{
			case CaptchaFontWarpFactor.None:
				return;
			case CaptchaFontWarpFactor.Low:
				num = 6f;
				num2 = 1f;
				break;
			case CaptchaFontWarpFactor.Medium:
				num = 5f;
				num2 = 1.3f;
				break;
			case CaptchaFontWarpFactor.High:
				num = 4.5f;
				num2 = 1.4f;
				break;
			case CaptchaFontWarpFactor.Extreme:
				num = 4f;
				num2 = 1.5f;
				break;
			}
			RectangleF srcRect = new RectangleF(Convert.ToSingle(rect.Left), 0f, Convert.ToSingle(rect.Width), (float)rect.Height);
			int num3 = Convert.ToInt32((float)rect.Height / num);
			int num4 = Convert.ToInt32((float)rect.Width / num);
			int num5 = rect.Left - Convert.ToInt32((float)num4 * num2);
			int num6 = rect.Top - Convert.ToInt32((float)num3 * num2);
			int num7 = rect.Left + rect.Width + Convert.ToInt32((float)num4 * num2);
			int num8 = rect.Top + rect.Height + Convert.ToInt32((float)num3 * num2);
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 < 0)
			{
				num6 = 0;
			}
			if (num7 > this.Width)
			{
				num7 = this.Width;
			}
			if (num8 > this.Height)
			{
				num8 = this.Height;
			}
			PointF pointF = this.RandomPoint(num5, num5 + num4, num6, num6 + num3);
			PointF pointF2 = this.RandomPoint(num7 - num4, num7, num6, num6 + num3);
			PointF pointF3 = this.RandomPoint(num5, num5 + num4, num8 - num3, num8);
			PointF pointF4 = this.RandomPoint(num7 - num4, num7, num8 - num3, num8);
			PointF[] destPoints = new PointF[]
			{
				pointF,
				pointF2,
				pointF3,
				pointF4
			};
			Matrix matrix = new Matrix();
			matrix.Translate(0f, 0f);
			textPath.Warp(destPoints, srcRect, matrix, WarpMode.Perspective, 0f);
		}

		// Token: 0x0600E14E RID: 57678 RVA: 0x0032125C File Offset: 0x0031F45C
		private void AddNoise(Graphics graphics, Rectangle rect)
		{
			int num = 0;
			int num2 = 0;
			switch (this.BackgroundNoise)
			{
			case CaptchaBackgroundNoiseLevel.None:
				return;
			case CaptchaBackgroundNoiseLevel.Low:
				num = 30;
				num2 = 40;
				break;
			case CaptchaBackgroundNoiseLevel.Medium:
				num = 18;
				num2 = 40;
				break;
			case CaptchaBackgroundNoiseLevel.High:
				num = 16;
				num2 = 39;
				break;
			case CaptchaBackgroundNoiseLevel.Extreme:
				num = 12;
				num2 = 38;
				break;
			}
			SolidBrush solidBrush = new SolidBrush(this.TextColor);
			int maxValue = Convert.ToInt32(Math.Max(rect.Width, rect.Height) / num2);
			for (int i = 0; i <= Convert.ToInt32(rect.Width * rect.Height / num); i++)
			{
				graphics.FillEllipse(solidBrush, this._rand.GetInt(rect.Width), this._rand.GetInt(rect.Height), this._rand.GetInt(maxValue), this._rand.GetInt(maxValue));
			}
			solidBrush.Dispose();
		}

		// Token: 0x0600E14F RID: 57679 RVA: 0x00321348 File Offset: 0x0031F548
		private void AddLine(Graphics graphics, Rectangle rect)
		{
			int num = 0;
			float width = 0f;
			int num2 = 0;
			switch (this.LineNoise)
			{
			case CaptchaLineNoiseLevel.None:
				return;
			case CaptchaLineNoiseLevel.Low:
				num = 4;
				width = Convert.ToSingle((double)this.Height / 31.25);
				num2 = 1;
				break;
			case CaptchaLineNoiseLevel.Medium:
				num = 5;
				width = Convert.ToSingle((double)this.Height / 27.7777);
				num2 = 1;
				break;
			case CaptchaLineNoiseLevel.High:
				num = 3;
				width = Convert.ToSingle(this.Height / 25);
				num2 = 2;
				break;
			case CaptchaLineNoiseLevel.Extreme:
				num = 3;
				width = Convert.ToSingle((double)this.Height / 22.7272);
				num2 = 3;
				break;
			}
			PointF[] array = new PointF[num];
			Pen pen = new Pen(this.TextColor, width);
			for (int i = 1; i <= num2; i++)
			{
				for (int j = 1; j < num; j++)
				{
					array[j] = this.RandomPoint(rect);
				}
				graphics.DrawCurve(pen, array, 1.75f);
			}
			pen.Dispose();
		}

		// Token: 0x0400412D RID: 16685
		private const string _defaultFontName = "Courier New";

		// Token: 0x0400412E RID: 16686
		private TelerikRandom _rand;

		// Token: 0x0400412F RID: 16687
		private DateTime _generatedAt;

		// Token: 0x04004130 RID: 16688
		private string _randomText;

		// Token: 0x04004131 RID: 16689
		private string _guid;

		// Token: 0x04004132 RID: 16690
		private string _fontFamily;

		// Token: 0x04004133 RID: 16691
		private CaptchaFontWarpFactor _fontWarp = CaptchaFontWarpFactor.Medium;

		// Token: 0x04004134 RID: 16692
		private CaptchaBackgroundNoiseLevel _backgroundNoise = CaptchaBackgroundNoiseLevel.Low;

		// Token: 0x04004135 RID: 16693
		private CaptchaLineNoiseLevel _lineNoise = CaptchaLineNoiseLevel.Low;

		// Token: 0x04004136 RID: 16694
		private CaptchaPossibleChars _textChars;

		// Token: 0x04004137 RID: 16695
		private Color _textColor = Color.Gray;

		// Token: 0x04004138 RID: 16696
		private int _textLength = 5;

		// Token: 0x04004139 RID: 16697
		private int _width = 180;

		// Token: 0x0400413A RID: 16698
		private int _height = 50;

		// Token: 0x0400413B RID: 16699
		private string _fontWhitelist = string.Empty;

		// Token: 0x0400413C RID: 16700
		private string _imageAlternativeText = string.Empty;

		// Token: 0x0400413D RID: 16701
		private string _imageCssClass = string.Empty;

		// Token: 0x0400413E RID: 16702
		private bool _renderImageOnly;

		// Token: 0x0400413F RID: 16703
		private string _charset = "ABCDEFGHJKLMNOPQRSTUVWXYZ123456789";

		// Token: 0x04004140 RID: 16704
		private Color _backgroundColor = Color.White;

		// Token: 0x04004141 RID: 16705
		private bool _useRandomFont;

		// Token: 0x04004142 RID: 16706
		private bool _enableCaptchaAudio;

		// Token: 0x04004143 RID: 16707
		private bool _enableAudioNoise;

		// Token: 0x04004144 RID: 16708
		private string _audioFolderUrl = "~/App_Data/RadCaptcha";

		// Token: 0x04004145 RID: 16709
		private bool _useCustomAudioFiles;

		// Token: 0x04004146 RID: 16710
		private readonly string[] _wordsToExclude = new string[]
		{
			"anus",
			"anvs",
			"arse",
			"ass",
			"ball",
			"butt",
			"bastard",
			"bitch",
			"blow",
			"bl0w",
			"boll",
			"bull",
			"cameltoe",
			"carpetmuncher",
			"clit",
			"cock",
			"c0ck",
			"coon",
			"cooter",
			"crap",
			"cum",
			"cunni",
			"cunt",
			"damn",
			"dick",
			"dike",
			"dildo",
			"dipshit",
			"dookie",
			"dumb",
			"ejacul",
			"erect",
			"fag",
			"fatso",
			"fellatio",
			"feltch",
			"fuck",
			"gay",
			"gooch",
			"genital",
			"handjob",
			"homo",
			"hole",
			"h0le",
			"hoe",
			"hump",
			"hussy",
			"jew",
			"jerk",
			"jizz",
			"kooch",
			"kunt",
			"klit",
			"lesb",
			"lezzie",
			"lick",
			"minge",
			"muff",
			"mung",
			"negro",
			"negr0",
			"niga",
			"nigga",
			"nigger",
			"nutsack",
			"paki",
			"penis",
			"pecker",
			"panooch",
			"piss",
			"poon",
			"poop",
			"porchmonkey",
			"prick",
			"puss",
			"puta",
			"queef",
			"queer",
			"renob",
			"rimjob",
			"schlong",
			"scrote",
			"shit",
			"shiz",
			"skank",
			"sex",
			"shag",
			"skeet",
			"semen",
			"sperm",
			"suck",
			"snatch",
			"splooge",
			"tard",
			"testi",
			"tit",
			"twat",
			"vagina",
			"wank",
			"whore"
		};
	}
}
