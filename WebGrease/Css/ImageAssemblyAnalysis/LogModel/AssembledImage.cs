using System;
using System.Globalization;
using System.Xml.Linq;
using WebGrease.Css.Extensions;
using WebGrease.Extensions;
using WebGrease.ImageAssemble;

namespace WebGrease.Css.ImageAssemblyAnalysis.LogModel
{
	// Token: 0x0200018E RID: 398
	internal class AssembledImage
	{
		// Token: 0x06001490 RID: 5264 RVA: 0x0007843F File Offset: 0x0007663F
		internal AssembledImage()
		{
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x00078447 File Offset: 0x00076647
		internal AssembledImage(XContainer element, int? spriteWidth, int? spriteHeight)
		{
			this.SpriteWidth = spriteWidth;
			this.SpriteHeight = spriteHeight;
			if (element != null)
			{
				element.Elements().ForEach(new Action<XElement>(this.ParseElement));
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x00078477 File Offset: 0x00076677
		// (set) Token: 0x06001493 RID: 5267 RVA: 0x0007847F File Offset: 0x0007667F
		internal int? SpriteWidth { get; private set; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x00078488 File Offset: 0x00076688
		// (set) Token: 0x06001495 RID: 5269 RVA: 0x00078490 File Offset: 0x00076690
		internal int? SpriteHeight { get; private set; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x00078499 File Offset: 0x00076699
		// (set) Token: 0x06001497 RID: 5271 RVA: 0x000784A1 File Offset: 0x000766A1
		internal string RelativeOutputFilePath { get; set; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x000784AA File Offset: 0x000766AA
		// (set) Token: 0x06001499 RID: 5273 RVA: 0x000784B2 File Offset: 0x000766B2
		internal string OutputFilePath { get; set; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x000784BB File Offset: 0x000766BB
		// (set) Token: 0x0600149B RID: 5275 RVA: 0x000784C3 File Offset: 0x000766C3
		internal string OriginalFilePath { get; set; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x000784CC File Offset: 0x000766CC
		// (set) Token: 0x0600149D RID: 5277 RVA: 0x000784D4 File Offset: 0x000766D4
		internal int? X { get; private set; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x000784DD File Offset: 0x000766DD
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x000784E5 File Offset: 0x000766E5
		internal int? Y { get; private set; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x000784EE File Offset: 0x000766EE
		// (set) Token: 0x060014A1 RID: 5281 RVA: 0x000784F6 File Offset: 0x000766F6
		internal ImagePosition? ImagePosition { get; private set; }

		// Token: 0x060014A2 RID: 5282 RVA: 0x00078500 File Offset: 0x00076700
		private static int LoadDimension(XElement element)
		{
			int result;
			if (int.TryParse(element.Value, out result))
			{
				return result;
			}
			throw new ImageAssembleException(string.Format(CultureInfo.CurrentUICulture, CssStrings.InvalidDimensionsError, new object[]
			{
				element.Name
			}));
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x00078544 File Offset: 0x00076744
		private void ParseElement(XElement childElement)
		{
			string text = childElement.Name.ToString();
			string a;
			if ((a = text) != null)
			{
				if (a == "originalfile")
				{
					this.OriginalFilePath = childElement.Value.GetFullPathWithLowercase();
					return;
				}
				if (a == "xposition")
				{
					this.X = new int?(AssembledImage.LoadDimension(childElement));
					return;
				}
				if (a == "yposition")
				{
					this.Y = new int?(AssembledImage.LoadDimension(childElement));
					return;
				}
				if (!(a == "positioninsprite"))
				{
					return;
				}
				this.ImagePosition = new ImagePosition?((ImagePosition)Enum.Parse(typeof(ImagePosition), childElement.Value));
			}
		}
	}
}
