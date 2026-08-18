using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using WebGrease.Css.ImageAssemblyAnalysis;
using WebGrease.Css.ImageAssemblyAnalysis.LogModel;
using WebGrease.Extensions;

namespace WebGrease.ImageAssemble
{
	// Token: 0x020001AD RID: 429
	internal static class ImageAssembleGenerator
	{
		// Token: 0x06001608 RID: 5640 RVA: 0x0007FE7C File Offset: 0x0007E07C
		internal static ImageMap AssembleImages(ReadOnlyCollection<InputImage> inputImages, SpritePackingType packingType, string assembleFileFolder, string pngOptimizerToolCommand, bool dedup, IWebGreaseContext context, int? imagePadding = null, ImageAssemblyAnalysisLog imageAssemblyAnalysisLog = null, ImageType? forcedImageType = null)
		{
			return ImageAssembleGenerator.AssembleImages(inputImages, packingType, assembleFileFolder, null, pngOptimizerToolCommand, dedup, context, imagePadding, imageAssemblyAnalysisLog, forcedImageType);
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x0008015C File Offset: 0x0007E35C
		internal static ImageMap AssembleImages(ReadOnlyCollection<InputImage> inputImages, SpritePackingType packingType, string assembleFileFolder, string mapFileName, string pngOptimizerToolCommand, bool dedup, IWebGreaseContext context, int? imagePadding = null, ImageAssemblyAnalysisLog imageAssemblyAnalysisLog = null, ImageType? forcedImageType = null)
		{
			ReadOnlyCollection<InputImage> inputImagesDeduped = dedup ? ImageAssembleGenerator.DedupImages(inputImages, context) : inputImages;
			ImageMap xmlMap = new ImageMap(mapFileName);
			Safe.LockFiles((from ii in inputImages
			select new FileInfo(ii.AbsoluteImagePath)).ToArray<FileInfo>(), delegate
			{
				Dictionary<ImageType, List<BitmapContainer>> dictionary = ImageAssembleGenerator.SeparateByImageType(inputImagesDeduped, forcedImageType);
				int paddingBetweenImages = imagePadding ?? 50;
				IEnumerable<ImageAssembleBase> enumerable = ImageAssembleGenerator.RegisterAvailableAssemblers(context);
				List<BitmapContainer> list = null;
				foreach (ImageAssembleBase imageAssembleBase in enumerable)
				{
					bool flag = false;
					try
					{
						imageAssembleBase.PackingType = packingType;
						imageAssembleBase.ImageXmlMap = xmlMap;
						xmlMap.AppendPadding(paddingBetweenImages.ToString(CultureInfo.InvariantCulture));
						imageAssembleBase.PaddingBetweenImages = paddingBetweenImages;
						imageAssembleBase.OptimizerToolCommand = pngOptimizerToolCommand;
						list = dictionary[imageAssembleBase.Type];
						if (list.Any<BitmapContainer>())
						{
							imageAssembleBase.AssembleFileName = ImageAssembleGenerator.GenerateAssembleFileName(from s in list
							select s.InputImage, assembleFileFolder) + imageAssembleBase.DefaultExtension;
							flag = imageAssembleBase.Assemble(list);
						}
					}
					finally
					{
						if (flag)
						{
							foreach (BitmapContainer bitmapContainer in list)
							{
								if (bitmapContainer.Bitmap != null)
								{
									if (imageAssemblyAnalysisLog != null)
									{
										imageAssemblyAnalysisLog.UpdateSpritedImage(imageAssembleBase.Type, bitmapContainer.InputImage.OriginalImagePath, imageAssembleBase.AssembleFileName);
									}
									context.Cache.CurrentCacheSection.AddSourceDependency(bitmapContainer.InputImage.AbsoluteImagePath);
									bitmapContainer.Bitmap.Dispose();
								}
							}
						}
					}
				}
				List<BitmapContainer> list2 = dictionary[ImageType.NotSupported];
				if (list2 != null && list2.Count > 0)
				{
					StringBuilder stringBuilder = new StringBuilder("The following files were not assembled because their formats are not supported:");
					foreach (BitmapContainer bitmapContainer2 in list2)
					{
						stringBuilder.Append(" " + bitmapContainer2.InputImage.OriginalImagePath);
					}
					throw new ImageAssembleException(stringBuilder.ToString());
				}
			});
			return xmlMap;
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00080378 File Offset: 0x0007E578
		internal static Dictionary<ImageType, List<BitmapContainer>> SeparateByImageType(IEnumerable<InputImage> inputImages, ImageType? forcedImageType = null)
		{
			Dictionary<ImageType, List<BitmapContainer>> separatedLists = new Dictionary<ImageType, List<BitmapContainer>>();
			foreach (object obj in Enum.GetValues(typeof(ImageType)))
			{
				ImageType key = (ImageType)obj;
				separatedLists[key] = new List<BitmapContainer>();
			}
			using (IEnumerator<InputImage> enumerator2 = inputImages.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					ImageAssembleGenerator.<>c__DisplayClassa CS$<>8__locals2 = new ImageAssembleGenerator.<>c__DisplayClassa();
					CS$<>8__locals2.inputImage = enumerator2.Current;
					BitmapContainer bitmapContainer = new BitmapContainer(CS$<>8__locals2.inputImage);
					bitmapContainer.BitmapAction(delegate(Bitmap b)
					{
						bitmapContainer.Bitmap = ImageAssembleGenerator.LoadBitmapFromDisk(CS$<>8__locals2.inputImage.AbsoluteImagePath, 0);
						if (bitmapContainer.Bitmap == null)
						{
							separatedLists[ImageType.NotSupported].Add(bitmapContainer);
							return;
						}
						if (forcedImageType != null)
						{
							separatedLists[forcedImageType.Value].Add(bitmapContainer);
							return;
						}
						if (ImageAssembleGenerator.IsPhoto(bitmapContainer.Bitmap))
						{
							separatedLists[ImageType.Photo].Add(bitmapContainer);
							return;
						}
						if (ImageAssembleGenerator.IsMultiframe(bitmapContainer.Bitmap))
						{
							separatedLists[ImageType.NotSupported].Add(bitmapContainer);
							return;
						}
						if (ImageAssembleGenerator.IsIndexed(bitmapContainer.Bitmap) || ImageAssembleGenerator.IsIndexable(bitmapContainer.Bitmap))
						{
							separatedLists[ImageType.NonphotoIndexed].Add(bitmapContainer);
							return;
						}
						separatedLists[ImageType.NonphotoNonindexed].Add(bitmapContainer);
					});
				}
			}
			return separatedLists;
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00080488 File Offset: 0x0007E688
		private static Bitmap LoadBitmapFromDisk(string absoluteImagePath, int retryCount = 0)
		{
			Bitmap result;
			try
			{
				result = (Image.FromFile(absoluteImagePath) as Bitmap);
			}
			catch (OutOfMemoryException)
			{
				if (retryCount < 4)
				{
					Thread.Sleep(500);
					return ImageAssembleGenerator.LoadBitmapFromDisk(absoluteImagePath, ++retryCount);
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x000804D8 File Offset: 0x0007E6D8
		private static IEnumerable<ImageAssembleBase> RegisterAvailableAssemblers(IWebGreaseContext context)
		{
			List<ImageAssembleBase> list = new List<ImageAssembleBase>();
			NotSupportedAssemble item = new NotSupportedAssemble(context);
			list.Add(item);
			PhotoAssemble item2 = new PhotoAssemble(context);
			list.Add(item2);
			NonphotoNonindexedAssemble item3 = new NonphotoNonindexedAssemble(context);
			list.Add(item3);
			NonphotoIndexedAssemble item4 = new NonphotoIndexedAssemble(context);
			list.Add(item4);
			return list;
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00080530 File Offset: 0x0007E730
		private static string GenerateAssembleFileName(IEnumerable<InputImage> inputImages, string targetFolder)
		{
			string path = WebGreaseContext.ComputeContentHash(string.Join("|", from i in inputImages
			select i.AbsoluteImagePath), null);
			return Path.GetFullPath(Path.Combine(targetFolder, path));
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x0008057D File Offset: 0x0007E77D
		private static bool IsPhoto(Bitmap bitmap)
		{
			return bitmap.RawFormat.Equals(ImageFormat.Jpeg) || bitmap.RawFormat.Equals(ImageFormat.Exif);
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x000805A3 File Offset: 0x0007E7A3
		private static bool IsIndexed(Bitmap bitmap)
		{
			return (bitmap.PixelFormat & PixelFormat.Indexed) != PixelFormat.Undefined;
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x000805B7 File Offset: 0x0007E7B7
		private static bool HasAlpha(Bitmap bitmap)
		{
			return (bitmap.PixelFormat & PixelFormat.Alpha) != PixelFormat.Undefined || (bitmap.PixelFormat & PixelFormat.PAlpha) != PixelFormat.Undefined;
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x000805DC File Offset: 0x0007E7DC
		private static bool IsIndexable(Bitmap bitmap)
		{
			bool flag = false;
			BitArray bitArray = new BitArray(16777216);
			int num = 0;
			int width = bitmap.Width;
			int height = bitmap.Height;
			if (!ImageAssembleGenerator.HasAlpha(bitmap) && width * height <= 256)
			{
				return true;
			}
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					Color pixel = bitmap.GetPixel(i, j);
					if (pixel.A == 0)
					{
						if (!flag)
						{
							num++;
							flag = true;
						}
					}
					else
					{
						if (pixel.A != 255)
						{
							return false;
						}
						int index = ((int)pixel.R << 16) + ((int)pixel.G << 8) + (int)pixel.B;
						if (!bitArray[index])
						{
							num++;
							bitArray[index] = true;
						}
					}
					if (num > 256)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x000806B4 File Offset: 0x0007E8B4
		private static bool IsMultiframe(Bitmap bitmap)
		{
			FrameDimension dimension = new FrameDimension(bitmap.FrameDimensionsList[0]);
			return bitmap.GetFrameCount(dimension) > 1;
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x000806E4 File Offset: 0x0007E8E4
		private static ReadOnlyCollection<InputImage> DedupImages(ReadOnlyCollection<InputImage> inputImages, IWebGreaseContext context)
		{
			List<InputImage> list = new List<InputImage>();
			Dictionary<string, InputImage> dictionary = new Dictionary<string, InputImage>();
			foreach (InputImage inputImage in inputImages)
			{
				if (!File.Exists(inputImage.AbsoluteImagePath))
				{
					throw new FileNotFoundException("Could not find image to sprite: {0}".InvariantFormat(new object[]
					{
						inputImage.AbsoluteImagePath
					}), inputImage.AbsoluteImagePath);
				}
				string key = context.GetFileHash(inputImage.AbsoluteImagePath) + "." + inputImage.Position;
				if (dictionary.ContainsKey(key))
				{
					InputImage inputImage2 = dictionary[key];
					inputImage2.DuplicateImagePaths.Add(inputImage.AbsoluteImagePath);
				}
				else
				{
					dictionary.Add(key, inputImage);
					list.Add(inputImage);
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x04000BA8 RID: 2984
		private const int MaxRetryCount = 4;

		// Token: 0x04000BA9 RID: 2985
		private const int RetrySleepMilliseconds = 500;

		// Token: 0x04000BAA RID: 2986
		private const int DefaultPadding = 50;
	}
}
