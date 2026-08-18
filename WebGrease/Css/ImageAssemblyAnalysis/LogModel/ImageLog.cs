using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using WebGrease.Css.Extensions;
using WebGrease.Extensions;

namespace WebGrease.Css.ImageAssemblyAnalysis.LogModel
{
	// Token: 0x02000192 RID: 402
	internal class ImageLog
	{
		// Token: 0x060014C0 RID: 5312 RVA: 0x00078DA1 File Offset: 0x00076FA1
		internal ImageLog()
		{
			this.InputImages = new List<AssembledImage>();
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00078DB4 File Offset: 0x00076FB4
		internal ImageLog(XDocument imageMapDocument) : this()
		{
			if (imageMapDocument == null)
			{
				throw new ArgumentNullException("imageMapDocument");
			}
			if (imageMapDocument.Root != null)
			{
				imageMapDocument.Root.Elements("output").ForEach(new Action<XElement>(this.ProcessOutputElement));
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x00078E03 File Offset: 0x00077003
		// (set) Token: 0x060014C3 RID: 5315 RVA: 0x00078E0B File Offset: 0x0007700B
		internal List<AssembledImage> InputImages { get; private set; }

		// Token: 0x060014C4 RID: 5316 RVA: 0x00078E3C File Offset: 0x0007703C
		private void ProcessOutputElement(XElement outputElement)
		{
			int? spriteWidth = (int?)outputElement.Attribute("width");
			int? spriteHeight = (int?)outputElement.Attribute("height");
			XAttribute xattribute = outputElement.Attribute("file");
			if (xattribute == null)
			{
				return;
			}
			string outputFilePath = xattribute.Value;
			if (string.IsNullOrWhiteSpace(outputFilePath))
			{
				return;
			}
			outputFilePath = outputFilePath.GetFullPathWithLowercase();
			if (!File.Exists(outputFilePath))
			{
				throw new FileNotFoundException(string.Format(CultureInfo.CurrentUICulture, CssStrings.FileNotFoundError, new object[]
				{
					outputFilePath
				}));
			}
			outputElement.Descendants("input").ForEach(delegate(XElement inputElement)
			{
				this.ProcessInputElement(inputElement, spriteWidth, spriteHeight, outputFilePath);
			});
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00078F20 File Offset: 0x00077120
		private void ProcessInputElement(XElement inputElement, int? spriteWidth, int? spriteHeight, string outputFilePath)
		{
			this.InputImages.Add(new AssembledImage(inputElement, spriteWidth, spriteHeight)
			{
				OutputFilePath = outputFilePath
			});
		}
	}
}
