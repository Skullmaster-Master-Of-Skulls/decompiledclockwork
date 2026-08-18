using System;
using System.Collections;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000371 RID: 881
	public class AdCreatedEventArgs : EventArgs
	{
		// Token: 0x06002891 RID: 10385 RVA: 0x0008310A File Offset: 0x0008130A
		public AdCreatedEventArgs(IDictionary adProperties) : this(adProperties, null, null, null)
		{
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x00083118 File Offset: 0x00081318
		internal AdCreatedEventArgs(IDictionary adProperties, string imageUrlField, string navigateUrlField, string alternateTextField)
		{
			if (adProperties != null)
			{
				this.adProperties = adProperties;
				this.imageUrl = this.GetAdProperty("ImageUrl", imageUrlField);
				this.navigateUrl = this.GetAdProperty("NavigateUrl", navigateUrlField);
				this.alternateText = this.GetAdProperty("AlternateText", alternateTextField);
				this.hasWidth = this.GetUnitValue(adProperties, "Width", ref this.width);
				this.hasHeight = this.GetUnitValue(adProperties, "Height", ref this.height);
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06002893 RID: 10387 RVA: 0x000831BD File Offset: 0x000813BD
		public IDictionary AdProperties
		{
			get
			{
				return this.adProperties;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x000831C5 File Offset: 0x000813C5
		// (set) Token: 0x06002895 RID: 10389 RVA: 0x000831CD File Offset: 0x000813CD
		public string AlternateText
		{
			get
			{
				return this.alternateText;
			}
			set
			{
				this.alternateText = value;
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x000831D6 File Offset: 0x000813D6
		internal bool HasHeight
		{
			get
			{
				return this.hasHeight;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06002897 RID: 10391 RVA: 0x000831DE File Offset: 0x000813DE
		internal bool HasWidth
		{
			get
			{
				return this.hasWidth;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06002898 RID: 10392 RVA: 0x000831E6 File Offset: 0x000813E6
		internal Unit Height
		{
			get
			{
				return this.height;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06002899 RID: 10393 RVA: 0x000831EE File Offset: 0x000813EE
		// (set) Token: 0x0600289A RID: 10394 RVA: 0x000831F6 File Offset: 0x000813F6
		public string ImageUrl
		{
			get
			{
				return this.imageUrl;
			}
			set
			{
				this.imageUrl = value;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x0600289B RID: 10395 RVA: 0x000831FF File Offset: 0x000813FF
		// (set) Token: 0x0600289C RID: 10396 RVA: 0x00083207 File Offset: 0x00081407
		public string NavigateUrl
		{
			get
			{
				return this.navigateUrl;
			}
			set
			{
				this.navigateUrl = value;
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x0600289D RID: 10397 RVA: 0x00083210 File Offset: 0x00081410
		internal Unit Width
		{
			get
			{
				return this.width;
			}
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x00083218 File Offset: 0x00081418
		private string GetAdProperty(string defaultIndex, string keyIndex)
		{
			string key = string.IsNullOrEmpty(keyIndex) ? defaultIndex : keyIndex;
			string text = (this.adProperties == null) ? null : ((string)this.adProperties[key]);
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x0008325C File Offset: 0x0008145C
		private bool GetUnitValue(IDictionary properties, string keyIndex, ref Unit unitValue)
		{
			string text = properties[keyIndex] as string;
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					unitValue = Unit.Parse(text, CultureInfo.InvariantCulture);
				}
				catch
				{
					throw new FormatException(SR.GetString("AdRotator_invalid_integer_format", new object[]
					{
						text,
						keyIndex,
						typeof(Unit).FullName
					}));
				}
				return true;
			}
			return false;
		}

		// Token: 0x04001E02 RID: 7682
		internal const string ImageUrlElement = "ImageUrl";

		// Token: 0x04001E03 RID: 7683
		internal const string NavigateUrlElement = "NavigateUrl";

		// Token: 0x04001E04 RID: 7684
		internal const string AlternateTextElement = "AlternateText";

		// Token: 0x04001E05 RID: 7685
		private const string WidthElement = "Width";

		// Token: 0x04001E06 RID: 7686
		private const string HeightElement = "Height";

		// Token: 0x04001E07 RID: 7687
		private string imageUrl = string.Empty;

		// Token: 0x04001E08 RID: 7688
		private string navigateUrl = string.Empty;

		// Token: 0x04001E09 RID: 7689
		private string alternateText = string.Empty;

		// Token: 0x04001E0A RID: 7690
		private IDictionary adProperties;

		// Token: 0x04001E0B RID: 7691
		private bool hasHeight;

		// Token: 0x04001E0C RID: 7692
		private bool hasWidth;

		// Token: 0x04001E0D RID: 7693
		private Unit width;

		// Token: 0x04001E0E RID: 7694
		private Unit height;
	}
}
