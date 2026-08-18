using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using WebGrease.Css.ImageAssemblyAnalysis;
using WebGrease.Extensions;

namespace WebGrease.ImageAssemble
{
	// Token: 0x020001AC RID: 428
	internal abstract class ImageAssembleBase
	{
		// Token: 0x060015EC RID: 5612 RVA: 0x0007F1F1 File Offset: 0x0007D3F1
		public ImageAssembleBase(IWebGreaseContext context)
		{
			this.context = context;
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060015ED RID: 5613
		internal abstract ImageType Type { get; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060015EE RID: 5614
		internal abstract string DefaultExtension { get; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x0007F200 File Offset: 0x0007D400
		// (set) Token: 0x060015F0 RID: 5616 RVA: 0x0007F208 File Offset: 0x0007D408
		internal string AssembleFileName { get; set; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x0007F211 File Offset: 0x0007D411
		// (set) Token: 0x060015F2 RID: 5618 RVA: 0x0007F219 File Offset: 0x0007D419
		internal SpritePackingType PackingType { get; set; }

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x0007F222 File Offset: 0x0007D422
		// (set) Token: 0x060015F4 RID: 5620 RVA: 0x0007F22A File Offset: 0x0007D42A
		internal ImageMap ImageXmlMap { get; set; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x0007F233 File Offset: 0x0007D433
		// (set) Token: 0x060015F6 RID: 5622 RVA: 0x0007F23B File Offset: 0x0007D43B
		internal int PaddingBetweenImages { get; set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x0007F244 File Offset: 0x0007D444
		// (set) Token: 0x060015F8 RID: 5624 RVA: 0x0007F24C File Offset: 0x0007D44C
		internal string OptimizerToolCommand { get; set; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060015F9 RID: 5625
		protected abstract ImageFormat Format { get; }

		// Token: 0x060015FA RID: 5626 RVA: 0x0007F2A8 File Offset: 0x0007D4A8
		internal virtual bool Assemble(List<BitmapContainer> inputImages)
		{
			Bitmap bitmap2 = null;
			try
			{
				if (inputImages.HasAtLeast(2))
				{
					switch (this.PackingType)
					{
					case SpritePackingType.Vertical:
						bitmap2 = this.PackVertical(inputImages, true, null);
						break;
					case SpritePackingType.Horizontal:
						bitmap2 = this.PackHorizontal(inputImages, true, null);
						break;
					default:
						bitmap2 = this.PackVertical(inputImages, true, null);
						break;
					}
					if (bitmap2 != null)
					{
						this.SaveAndHashImage(bitmap2, bitmap2.Width, bitmap2.Height);
						return true;
					}
				}
				else if (inputImages.Any<BitmapContainer>())
				{
					BitmapContainer image = inputImages.First<BitmapContainer>();
					this.ImageXmlMap.AppendToXml(image.InputImage.AbsoluteImagePath, this.AssembleFileName, image.Width, image.Height, 0, 0, "passthrough", true, new ImagePosition?(image.InputImage.Position));
					image.BitmapAction(delegate(Bitmap bitmap)
					{
						this.SaveAndHashImage(image.Bitmap, image.Width, image.Height);
					});
					return true;
				}
			}
			catch (OutOfMemoryException ex)
			{
				this.context.Log.Error(ex, null, null);
				ImageAssembleException ex2 = new ImageAssembleException(ImageAssembleStrings.ImageLoadOutofMemoryExceptionMessage, ex);
				throw ex2;
			}
			catch (Exception exception)
			{
				this.context.Log.Error(exception, null, null);
				try
				{
					Safe.FileLock(new FileInfo(this.AssembleFileName), delegate()
					{
						if (File.Exists(this.AssembleFileName))
						{
							File.Delete(this.AssembleFileName);
						}
					});
				}
				catch (Exception)
				{
				}
				throw;
			}
			finally
			{
				if (bitmap2 != null)
				{
					bitmap2.Dispose();
				}
			}
			return false;
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x0007F4D0 File Offset: 0x0007D6D0
		private void SaveAndHashImage(Bitmap bitmap, int width, int height)
		{
			string bitmapHash = this.context.GetBitmapHash(bitmap, this.Format);
			this.AssembleFileName = this.HashImage(bitmapHash);
			FileInfo targetFileInfo = new FileInfo(this.AssembleFileName);
			Safe.FileLock(targetFileInfo, delegate()
			{
				if (!targetFileInfo.Exists)
				{
					this.SaveImage(bitmap);
				}
			});
			this.ImageXmlMap.UpdateSize(this.AssembleFileName, width, height);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0007F554 File Offset: 0x0007D754
		protected virtual void SaveImage(Bitmap newImage)
		{
			try
			{
				if (!File.Exists(this.AssembleFileName))
				{
					newImage.Save(this.AssembleFileName, this.Format);
				}
			}
			catch (ExternalException innerException)
			{
				ImageAssembleException ex = new ImageAssembleException(string.Format(CultureInfo.CurrentUICulture, ImageAssembleStrings.ImageSaveExternalExceptionMessage, new object[]
				{
					this.AssembleFileName
				}), innerException);
				throw ex;
			}
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x0007F5BC File Offset: 0x0007D7BC
		protected void OptimizeImage()
		{
			if (!string.IsNullOrEmpty(this.OptimizerToolCommand))
			{
				this.OptimizerToolCommand = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + this.OptimizerToolCommand;
				string text = ".exe";
				string text2 = string.Format(CultureInfo.InvariantCulture, this.OptimizerToolCommand, new object[]
				{
					this.AssembleFileName
				});
				int num = text2.IndexOf(text, StringComparison.OrdinalIgnoreCase);
				string text3 = text2.Substring(0, num + text.Length + 1);
				Trace.WriteLine("Image Optimization Executable - " + text3);
				if (!File.Exists(text3))
				{
					throw new FileNotFoundException("Could not locate the image optimization executable.", text3);
				}
				Process process = Process.Start(new ProcessStartInfo(text3)
				{
					CreateNoWindow = true,
					Arguments = text2.Replace(text3, string.Empty),
					UseShellExecute = false
				});
				process.WaitForExit();
			}
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x0007F6A4 File Offset: 0x0007D8A4
		protected string HashImage(string hash)
		{
			FileInfo fileInfo = new FileInfo(this.AssembleFileName);
			string text = hash + fileInfo.Extension;
			string directoryName = fileInfo.DirectoryName;
			string text2 = Path.Combine(directoryName, text.Substring(0, 2));
			Directory.CreateDirectory(text2);
			text2 = Path.Combine(text2, text.Remove(0, 2));
			if (!this.ImageXmlMap.UpdateAssembledImageName(this.AssembleFileName, text2))
			{
				throw new ImageAssembleException(null, this.AssembleFileName, "Operation failed while replacing assembled image name: '" + this.AssembleFileName + "' with hashed name.");
			}
			return text2;
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x0007F798 File Offset: 0x0007D998
		protected Bitmap PackHorizontal(List<BitmapContainer> originalBitmaps, bool useLogging, PixelFormat? pixelFormat)
		{
			int height = originalBitmaps.Max((BitmapContainer c) => c.Height);
			int width = originalBitmaps.Sum((BitmapContainer c) => c.Width) + originalBitmaps.Count * this.PaddingBetweenImages;
			Bitmap bitmap2 = (pixelFormat != null) ? new Bitmap(width, height, pixelFormat.Value) : new Bitmap(width, height);
			IOrderedEnumerable<BitmapContainer> orderedEnumerable = from entry in originalBitmaps
			orderby entry.Height descending
			select entry;
			using (Graphics graphics = Graphics.FromImage(bitmap2))
			{
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				int xpoint = 0;
				bool addOutputNode = true;
				using (IEnumerator<BitmapContainer> enumerator = orderedEnumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						BitmapContainer entry = enumerator.Current;
						entry.BitmapAction(delegate(Bitmap bitmap)
						{
							graphics.DrawImage(bitmap, new Rectangle(xpoint, 0, entry.Width, entry.Height));
						});
						if (useLogging)
						{
							this.ImageXmlMap.AppendToXml(entry.InputImage.AbsoluteImagePath, this.AssembleFileName, entry.Width, entry.Height, xpoint * -1, 0, null, addOutputNode, new ImagePosition?(entry.InputImage.Position));
							addOutputNode = false;
							foreach (string originalFile in entry.InputImage.DuplicateImagePaths)
							{
								this.ImageXmlMap.AppendToXml(originalFile, this.AssembleFileName, entry.Width, entry.Height, xpoint * -1, 0, "duplicate", addOutputNode, new ImagePosition?(entry.InputImage.Position));
							}
						}
						xpoint += entry.Width + this.PaddingBetweenImages;
					}
				}
			}
			return bitmap2;
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x0007FB10 File Offset: 0x0007DD10
		protected Bitmap PackVertical(List<BitmapContainer> originalBitmaps, bool useLogging, PixelFormat? pixelFormat)
		{
			int width = originalBitmaps.Max((BitmapContainer c) => c.Width);
			int num = originalBitmaps.Sum((BitmapContainer c) => c.Height);
			num += originalBitmaps.Count * this.PaddingBetweenImages;
			Bitmap bitmap2 = (pixelFormat != null) ? new Bitmap(width, num, pixelFormat.Value) : new Bitmap(width, num);
			IOrderedEnumerable<BitmapContainer> orderedEnumerable = from entry in originalBitmaps
			orderby entry.Width descending
			select entry;
			using (Graphics graphics = Graphics.FromImage(bitmap2))
			{
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				int ypoint = 0;
				bool addOutputNode = true;
				using (IEnumerator<BitmapContainer> enumerator = orderedEnumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ImageAssembleBase.<>c__DisplayClass21 CS$<>8__locals3 = new ImageAssembleBase.<>c__DisplayClass21();
						CS$<>8__locals3.entry = enumerator.Current;
						int xpoint = 0;
						switch (CS$<>8__locals3.entry.InputImage.Position)
						{
						case ImagePosition.Right:
							xpoint = bitmap2.Width - CS$<>8__locals3.entry.Width;
							break;
						case ImagePosition.Center:
							xpoint = (bitmap2.Width - CS$<>8__locals3.entry.Width + 1) / 2;
							break;
						}
						CS$<>8__locals3.entry.BitmapAction(delegate(Bitmap bitmap)
						{
							graphics.DrawImage(bitmap, new Rectangle(xpoint, ypoint, CS$<>8__locals3.entry.Width, CS$<>8__locals3.entry.Height));
						});
						if (useLogging)
						{
							this.ImageXmlMap.AppendToXml(CS$<>8__locals3.entry.InputImage.AbsoluteImagePath, this.AssembleFileName, CS$<>8__locals3.entry.Width, CS$<>8__locals3.entry.Height, xpoint * -1, ypoint * -1, null, addOutputNode, new ImagePosition?(CS$<>8__locals3.entry.InputImage.Position));
							addOutputNode = false;
							foreach (string originalFile in CS$<>8__locals3.entry.InputImage.DuplicateImagePaths)
							{
								this.ImageXmlMap.AppendToXml(originalFile, this.AssembleFileName, CS$<>8__locals3.entry.Width, CS$<>8__locals3.entry.Height, xpoint * -1, ypoint * -1, "duplicate", addOutputNode, new ImagePosition?(CS$<>8__locals3.entry.InputImage.Position));
							}
						}
						ypoint += CS$<>8__locals3.entry.Height + this.PaddingBetweenImages;
					}
				}
			}
			return bitmap2;
		}

		// Token: 0x04000B9C RID: 2972
		private readonly IWebGreaseContext context;
	}
}
