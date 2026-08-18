using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WebGrease.Css.Extensions;
using WebGrease.ImageAssemble;

namespace WebGrease.Css.ImageAssemblyAnalysis.LogModel
{
	// Token: 0x02000191 RID: 401
	public class ImageAssemblyAnalysisLog
	{
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x000786D0 File Offset: 0x000768D0
		internal IEnumerable<ImageAssemblyAnalysis> FailedSprites
		{
			get
			{
				return from ln in this.logNodes
				where ln.FailureReason != null && ln.FailureReason != FailureReason.NoUrl && ln.FailureReason != FailureReason.IgnoreUrl && ln.FailureReason != FailureReason.SpritingIgnore
				select ln;
			}
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x000786FC File Offset: 0x000768FC
		internal static string GetFailureMessage(ImageAssemblyAnalysis analysis)
		{
			FailureReason valueOrDefault = analysis.FailureReason.GetValueOrDefault();
			FailureReason? failureReason;
			if (failureReason == null)
			{
				return "No failure";
			}
			switch (valueOrDefault)
			{
			case FailureReason.IncorrectPosition:
				return "No declaration with absolute vertical position found.";
			case FailureReason.BackgroundSizeIsSetToNonDefaultValue:
				return "The image url is configured to ignore in locale resx file.";
			case FailureReason.InvalidDpi:
				return "-wg-dpi was set but was invalid.";
			case FailureReason.BackgroundRepeatInvalid:
				return "Background-repeat value was invalid (only no-repeat allows spriting)";
			case FailureReason.MultipleUrls:
				return "Multiple url's in a single background are not supported by webgrease at this time.";
			case FailureReason.NoRepeat:
				return "No declaration with background 'no-repeat'.";
			case FailureReason.NoUrl:
				return "No declaration with background url.";
			case FailureReason.IgnoreUrl:
				return "The image url is configured to ignore in locale resx file.";
			case FailureReason.SpritingIgnore:
				return "-wg-spriting: ignore.";
			default:
				return "Unknown failure reason";
			}
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0007878D File Offset: 0x0007698D
		internal void Add(ImageAssemblyAnalysis logNode)
		{
			if (logNode != null)
			{
				this.logNodes.Add(logNode);
			}
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x000787F0 File Offset: 0x000769F0
		internal void UpdateSpritedImage(ImageType imageType, string imagePath, string spritedImage)
		{
			this.logNodes.Where(delegate(ImageAssemblyAnalysis ln)
			{
				string image = ln.Image;
				return image != null && image.Equals(imagePath, StringComparison.OrdinalIgnoreCase);
			}).ForEach(delegate(ImageAssemblyAnalysis i)
			{
				i.ImageType = new ImageType?(imageType);
				i.SpritedImage = spritedImage;
			});
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00078B14 File Offset: 0x00076D14
		internal void Save(string path)
		{
			if (!this.logNodes.Any<ImageAssemblyAnalysis>())
			{
				return;
			}
			IEnumerable<ImageAssemblyAnalysis> source = from ln in this.logNodes
			where ln.FailureReason == null
			select ln;
			IEnumerable<ImageAssemblyAnalysis> source2 = from ln in this.logNodes
			where ln.FailureReason != null
			select ln;
			IEnumerable<ImageAssemblyAnalysis> source3 = from ln in source2
			where ln.FailureReason != FailureReason.NoUrl
			select ln;
			IEnumerable<ImageAssemblyAnalysis> source4 = from ln in source3
			where ln.FailureReason == FailureReason.IgnoreUrl || ln.FailureReason == FailureReason.SpritingIgnore
			select ln;
			IEnumerable<ImageAssemblyAnalysis> source5 = from ln in source3
			where ln.FailureReason != FailureReason.IgnoreUrl && ln.FailureReason != FailureReason.SpritingIgnore
			select ln;
			XName name = "SpritingLog";
			object[] array = new object[3];
			array[0] = new XElement("Failed", (from i in source5
			orderby i.FailureReason
			select i).Select(new Func<ImageAssemblyAnalysis, XElement>(ImageAssemblyAnalysisLog.LogNodeToXElement)));
			array[1] = new XElement("Ignored", (from i in source4
			orderby i.FailureReason
			select i).Select(new Func<ImageAssemblyAnalysis, XElement>(ImageAssemblyAnalysisLog.LogNodeToXElement)));
			array[2] = (from ln in source
			group ln by new
			{
				ln.SpritedImage,
				ln.ImageType
			}).Select(delegate(logNode)
			{
				XElement xelement = new XElement("Sprited", logNode.Select(new Func<ImageAssemblyAnalysis, XElement>(ImageAssemblyAnalysisLog.LogNodeToXElement)));
				if (logNode.Key.SpritedImage != null)
				{
					xelement.Add(new XAttribute("SpritedImage", logNode.Key.SpritedImage));
				}
				if (logNode.Key.ImageType != null)
				{
					xelement.Add(new XAttribute("ImageType", logNode.Key.ImageType));
				}
				return xelement;
			});
			new XElement(name, array).Save(path);
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00078CE8 File Offset: 0x00076EE8
		private static XElement LogNodeToXElement(ImageAssemblyAnalysis logNode)
		{
			XElement xelement = new XElement("SpriteItem", Environment.NewLine + logNode.AstNode.PrettyPrint() + "\t");
			if (logNode.FailureReason != null)
			{
				xelement.Add(new XAttribute("FailureReason", logNode.FailureReason));
				xelement.Add(new XAttribute("FailureMessage", ImageAssemblyAnalysisLog.GetFailureMessage(logNode)));
			}
			if (logNode.Image != null)
			{
				xelement.Add(new XAttribute("Image", logNode.Image));
			}
			return xelement;
		}

		// Token: 0x04000B16 RID: 2838
		private const string PxMessage = "No declaration with absolute vertical position found.";

		// Token: 0x04000B17 RID: 2839
		private const string NoUrlMessage = "No declaration with background url.";

		// Token: 0x04000B18 RID: 2840
		private const string NoRepeatMessage = "No declaration with background 'no-repeat'.";

		// Token: 0x04000B19 RID: 2841
		private const string IgnoreUrlMessage = "The image url is configured to ignore in locale resx file.";

		// Token: 0x04000B1A RID: 2842
		private const string InvalidDpiMessage = "-wg-dpi was set but was invalid.";

		// Token: 0x04000B1B RID: 2843
		private const string SpritingIgnoredMessage = "-wg-spriting: ignore.";

		// Token: 0x04000B1C RID: 2844
		private const string MultipleUrlsMessage = "Multiple url's in a single background are not supported by webgrease at this time.";

		// Token: 0x04000B1D RID: 2845
		private const string BackgroundRepeatInvalidMessage = "Background-repeat value was invalid (only no-repeat allows spriting)";

		// Token: 0x04000B1E RID: 2846
		private readonly List<ImageAssemblyAnalysis> logNodes = new List<ImageAssemblyAnalysis>();
	}
}
