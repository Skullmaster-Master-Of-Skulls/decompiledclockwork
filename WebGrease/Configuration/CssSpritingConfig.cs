using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using WebGrease.Extensions;
using WebGrease.ImageAssemble;

namespace WebGrease.Configuration
{
	// Token: 0x020000F3 RID: 243
	public class CssSpritingConfig : INamedConfig
	{
		// Token: 0x06000F90 RID: 3984 RVA: 0x00047744 File Offset: 0x00045944
		public CssSpritingConfig()
		{
			this.ShouldAutoSprite = true;
			this.ImagePadding = 50;
			this.ShouldAutoVersionBackgroundImages = true;
			this.ImagesToIgnore = new string[0];
			this.DestinationImageFolder = "images";
			this.OutputUnitFactor = 1.0;
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00047794 File Offset: 0x00045994
		public CssSpritingConfig(XElement element) : this()
		{
			this.Name = (((string)element.Attribute("config")) ?? string.Empty);
			foreach (XElement xelement in element.Descendants())
			{
				string text = xelement.Name.ToString();
				string value = xelement.Value;
				string key;
				switch (key = text)
				{
				case "ForceImageType":
					this.ForceImageType = value.TryParseToEnum(null);
					break;
				case "ImagePadding":
					this.ImagePadding = value.TryParseInt32();
					break;
				case "ImagesToIgnore":
					this.ImagesToIgnore = (value.IsNullOrWhitespace() ? new string[0] : value.Split(new char[]
					{
						','
					}).Distinct<string>());
					break;
				case "AutoVersionBackgroundImages":
					this.ShouldAutoVersionBackgroundImages = value.TryParseBool();
					break;
				case "SpriteImages":
					this.ShouldAutoSprite = value.TryParseBool();
					break;
				case "WriteLogFile":
					this.WriteLogFile = value.TryParseBool();
					break;
				case "ErrorOnInvalidSprite":
					this.ErrorOnInvalidSprite = value.TryParseBool();
					break;
				case "OutputUnit":
					this.OutputUnit = value;
					break;
				case "OutputUnitFactor":
				{
					double outputUnitFactor;
					if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out outputUnitFactor))
					{
						this.OutputUnitFactor = outputUnitFactor;
					}
					break;
				}
				case "IgnoreImagesWithNonDefaultBackgroundSize":
					this.IgnoreImagesWithNonDefaultBackgroundSize = value.TryParseBool();
					break;
				}
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x000479DC File Offset: 0x00045BDC
		// (set) Token: 0x06000F93 RID: 3987 RVA: 0x000479E4 File Offset: 0x00045BE4
		public string Name { get; internal set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x000479ED File Offset: 0x00045BED
		// (set) Token: 0x06000F95 RID: 3989 RVA: 0x000479F5 File Offset: 0x00045BF5
		public int ImagePadding { get; internal set; }

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x000479FE File Offset: 0x00045BFE
		// (set) Token: 0x06000F97 RID: 3991 RVA: 0x00047A06 File Offset: 0x00045C06
		public IEnumerable<string> ImagesToIgnore { get; internal set; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x00047A0F File Offset: 0x00045C0F
		// (set) Token: 0x06000F99 RID: 3993 RVA: 0x00047A17 File Offset: 0x00045C17
		internal bool ShouldAutoVersionBackgroundImages { get; set; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x00047A20 File Offset: 0x00045C20
		// (set) Token: 0x06000F9B RID: 3995 RVA: 0x00047A28 File Offset: 0x00045C28
		internal bool ShouldAutoSprite { get; set; }

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x00047A31 File Offset: 0x00045C31
		// (set) Token: 0x06000F9D RID: 3997 RVA: 0x00047A39 File Offset: 0x00045C39
		internal string DestinationImageFolder { get; set; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x00047A42 File Offset: 0x00045C42
		// (set) Token: 0x06000F9F RID: 3999 RVA: 0x00047A4A File Offset: 0x00045C4A
		internal string OutputUnit { get; set; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x00047A53 File Offset: 0x00045C53
		// (set) Token: 0x06000FA1 RID: 4001 RVA: 0x00047A5B File Offset: 0x00045C5B
		internal double OutputUnitFactor { get; set; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x00047A64 File Offset: 0x00045C64
		// (set) Token: 0x06000FA3 RID: 4003 RVA: 0x00047A6C File Offset: 0x00045C6C
		internal bool IgnoreImagesWithNonDefaultBackgroundSize { get; set; }

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x00047A75 File Offset: 0x00045C75
		// (set) Token: 0x06000FA5 RID: 4005 RVA: 0x00047A7D File Offset: 0x00045C7D
		internal bool WriteLogFile { get; set; }

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x00047A86 File Offset: 0x00045C86
		// (set) Token: 0x06000FA7 RID: 4007 RVA: 0x00047A8E File Offset: 0x00045C8E
		internal bool ErrorOnInvalidSprite { get; set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x00047A97 File Offset: 0x00045C97
		// (set) Token: 0x06000FA9 RID: 4009 RVA: 0x00047A9F File Offset: 0x00045C9F
		internal ImageType? ForceImageType { get; set; }
	}
}
