using System;
using System.Collections.Generic;
using System.IO;

namespace Spire.Doc.Fields
{
	// Token: 0x02000511 RID: 1297
	public class ImageTypeCheck
	{
		// Token: 0x06004312 RID: 17170 RVA: 0x003EDCD4 File Offset: 0x003ECCD4
		private static SortedDictionary<int, ImageTypeCheck.ImageType> ᜀ()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return new SortedDictionary<int, ImageTypeCheck.ImageType>
			{
				{
					19778,
					ImageTypeCheck.ImageType.BMP
				},
				{
					55551,
					ImageTypeCheck.ImageType.JPG
				},
				{
					18759,
					ImageTypeCheck.ImageType.GIF
				},
				{
					1290,
					ImageTypeCheck.ImageType.PCX
				},
				{
					20617,
					ImageTypeCheck.ImageType.PNG
				},
				{
					16952,
					ImageTypeCheck.ImageType.PSD
				},
				{
					42585,
					ImageTypeCheck.ImageType.RAS
				},
				{
					55809,
					ImageTypeCheck.ImageType.SGI
				},
				{
					18761,
					ImageTypeCheck.ImageType.TIFF
				}
			};
		}

		// Token: 0x06004313 RID: 17171 RVA: 0x003EDDA8 File Offset: 0x003ECDA8
		internal static string ᜁ(string A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return ImageTypeCheck.ᜀ(A_0).ToString();
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x003EDDF4 File Offset: 0x003ECDF4
		internal static ImageTypeCheck.ImageType ᜀ(string A_0)
		{
			switch (0)
			{
			default:
			{
				byte[] array = new byte[2];
				ImageTypeCheck.ImageType result;
				try
				{
					StreamReader streamReader = new StreamReader(A_0);
					try
					{
						for (;;)
						{
							int num = streamReader.BaseStream.Read(array, 0, array.Length);
							int num2 = 3;
							for (;;)
							{
								switch (num2)
								{
								case 0:
									goto IL_81;
								case 1:
									result = ImageTypeCheck.ImageType.None;
									num2 = 2;
									continue;
								case 2:
									goto IL_76;
								case 3:
									if (num != array.Length)
									{
										num2 = 1;
										continue;
									}
									num2 = 0;
									continue;
								}
								break;
							}
						}
						IL_76:
						goto IL_E7;
						IL_81:;
					}
					finally
					{
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								((IDisposable)streamReader).Dispose();
								num2 = 1;
								continue;
							case 1:
								goto IL_C0;
							}
							if (streamReader != null)
							{
								num2 = 0;
								continue;
							}
							IL_C0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_D6;
							}
						}
						IL_D6:
						if (false)
						{
						}
					}
					goto IL_17;
				}
				catch (Exception)
				{
					result = ImageTypeCheck.ImageType.None;
				}
				goto IL_E7;
				IL_17:
				return ImageTypeCheck.ᜀ(array);
				IL_E7:
				if (true)
				{
				}
				return result;
			}
			}
		}

		// Token: 0x06004315 RID: 17173 RVA: 0x003EDF18 File Offset: 0x003ECF18
		internal static ImageTypeCheck.ImageType ᜀ(byte[] A_0)
		{
			int num = 5;
			ImageTypeCheck.ImageType result;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.Length >= 2)
					{
						int key = ((int)A_0[1] << 8) + (int)A_0[0];
						result = ImageTypeCheck.ImageType.None;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
					}
					num = 1;
					continue;
				case 1:
					goto IL_AE;
				case 2:
				{
					int key;
					if (ImageTypeCheck.ᜀ.TryGetValue(key, out result))
					{
						num = 3;
						continue;
					}
					return ImageTypeCheck.ImageType.None;
				}
				case 3:
					return result;
				case 4:
					num = 0;
					continue;
				}
				if (A_0 == null)
				{
					goto IL_88;
				}
				num = 4;
			}
			return result;
			IL_88:
			if (true)
			{
			}
			return ImageTypeCheck.ImageType.None;
			IL_AE:
			goto IL_88;
		}

		// Token: 0x06004317 RID: 17175 RVA: 0x003EDFEC File Offset: 0x003ECFEC
		// Note: this type is marked as 'beforefieldinit'.
		static ImageTypeCheck()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			ImageTypeCheck.ᜀ = ImageTypeCheck.ᜀ();
			ImageTypeCheck.ᜁ = ImageTypeCheck.ImageType.None.ToString();
		}

		// Token: 0x04003553 RID: 13651
		private static SortedDictionary<int, ImageTypeCheck.ImageType> ᜀ;

		// Token: 0x04003554 RID: 13652
		internal static readonly string ᜁ;

		// Token: 0x02000512 RID: 1298
		internal enum ImageType
		{
			// Token: 0x04003556 RID: 13654
			None,
			// Token: 0x04003557 RID: 13655
			BMP = 19778,
			// Token: 0x04003558 RID: 13656
			JPG = 55551,
			// Token: 0x04003559 RID: 13657
			GIF = 18759,
			// Token: 0x0400355A RID: 13658
			PCX = 1290,
			// Token: 0x0400355B RID: 13659
			PNG = 20617,
			// Token: 0x0400355C RID: 13660
			PSD = 16952,
			// Token: 0x0400355D RID: 13661
			RAS = 42585,
			// Token: 0x0400355E RID: 13662
			SGI = 55809,
			// Token: 0x0400355F RID: 13663
			TIFF = 18761
		}
	}
}
