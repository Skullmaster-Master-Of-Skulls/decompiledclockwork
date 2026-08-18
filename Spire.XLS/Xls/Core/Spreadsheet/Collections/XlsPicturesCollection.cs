using System;
using System.Drawing;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x0200002D RID: 45
	public class XlsPicturesCollection : CollectionExtended<IPictureShape>, IPictures, IDisposable
	{
		// Token: 0x1700011B RID: 283
		internal IPictureShape this[string A_0]
		{
			get
			{
				switch (0)
				{
				default:
				{
					IPictureShape result;
					for (;;)
					{
						result = null;
						int num = 0;
						int count = base.Count;
						int num2 = 6;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								return result;
							case 1:
							{
								IPictureShape pictureShape;
								result = pictureShape;
								num2 = 2;
								continue;
							}
							case 2:
								return result;
							case 3:
							{
								if (num >= count)
								{
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										break;
									default:
										if (false)
										{
										}
										num2 = 0;
										continue;
									}
								}
								IPictureShape pictureShape = base[num];
								num2 = 4;
								continue;
							}
							case 4:
							{
								IPictureShape pictureShape;
								if (pictureShape.Name == A_0)
								{
									num2 = 1;
									continue;
								}
								if (true)
								{
								}
								num++;
								num2 = 5;
								continue;
							}
							case 5:
								goto IL_A4;
							case 6:
								goto IL_A4;
							}
							break;
							IL_A4:
							num2 = 3;
						}
					}
					return result;
				}
				}
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0001D2EC File Offset: 0x0001C2EC
		public IPictureShape Add(Image image, string pictureName)
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
			return this.Add(image, pictureName, ImageFormatType.Original);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0001D330 File Offset: 0x0001C330
		public IPictureShape Add(Image image, string pictureName, ImageFormatType imageFormat)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			IShapes shapes = this.ᜁ.Shapes;
			return shapes.AddPicture(image, pictureName, imageFormat);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0001D380 File Offset: 0x0001C380
		public IPictureShape Add(string strFileName)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return this.Add(strFileName, ImageFormatType.Original);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0001D3C4 File Offset: 0x0001C3C4
		public IPictureShape Add(string strFileName, ImageFormatType imageFormat)
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
			IShapes shapes = this.ᜁ.Shapes;
			return shapes.AddPicture(strFileName);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0001D414 File Offset: 0x0001C414
		public IPictureShape Add(int topRow, int leftColumn, Image image)
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
			return this.Add(topRow, leftColumn, image, ImageFormatType.Original);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0001D45C File Offset: 0x0001C45C
		public IPictureShape Add(int topRow, int leftColumn, Image image, ImageFormatType imageFormat)
		{
			int a_ = 12;
			if (image != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					XlsBitmapShape xlsBitmapShape = (XlsBitmapShape)this.Add(image, this.ᜀ(), imageFormat);
					xlsBitmapShape.LeftColumn = leftColumn;
					xlsBitmapShape.TopRow = topRow;
					xlsBitmapShape.EvaluateTopLeftPosition();
					return xlsBitmapShape;
				}
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("⭁⥃❅⽇⽉", a_));
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0001D4E4 File Offset: 0x0001C4E4
		public IPictureShape Add(int topRow, int leftColumn, Stream stream)
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
			return this.Add(topRow, leftColumn, stream, ImageFormatType.Original);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001D52C File Offset: 0x0001C52C
		public IPictureShape Add(int topRow, int leftColumn, Stream stream, ImageFormatType imageFormat)
		{
			int a_ = 11;
			if (stream != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					Image image = spr\u17FF.ᜀ(stream);
					return this.Add(topRow, leftColumn, image, imageFormat);
				}
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㉀㝂㝄≆⡈♊", a_));
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0001D59C File Offset: 0x0001C59C
		public IPictureShape Add(int topRow, int leftColumn, string fileName)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return this.Add(topRow, leftColumn, fileName, ImageFormatType.Original);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0001D5E4 File Offset: 0x0001C5E4
		public IPictureShape Add(int topRow, int leftColumn, string fileName, ImageFormatType imageFormat)
		{
			int a_ = 12;
			for (;;)
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (fileName.Length == 0)
						{
							num = 1;
							continue;
						}
						goto IL_A6;
					case 1:
						goto IL_74;
					case 2:
						goto IL_3C;
					case 3:
						if (true)
						{
						}
						break;
					}
					if (fileName == null)
					{
						num = 2;
					}
					else
					{
						num = 0;
					}
				}
				IL_3C:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_8C;
				}
			}
			IL_74:
			throw new ArgumentException(RecordTableEnumerator.b("⑁ⵃ⩅ⵇщⵋ⍍㕏牑㝓㝕㙗㑙㍛⩝䁟aţ䙥൧ݩᱫᩭ९", a_));
			IL_8C:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("⑁ⵃ⩅ⵇщⵋ⍍㕏", a_));
			IL_A6:
			FileStream a_2 = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			Image image = spr\u17FF.ᜀ(a_2);
			IPictureShape pictureShape = this.Add(topRow, leftColumn, image, imageFormat);
			pictureShape.Name = Path.GetFileNameWithoutExtension(fileName);
			return pictureShape;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001D6C0 File Offset: 0x0001C6C0
		public IPictureShape Add(int topRow, int leftColumn, int bottomRow, int rightColumn, Image image)
		{
			int a_ = 4;
			if (image != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					return this.Add(topRow, leftColumn, bottomRow, rightColumn, image, ImageFormatType.Original);
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("匹儻弽✿❁", a_));
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0001D72C File Offset: 0x0001C72C
		public IPictureShape Add(int topRow, int leftColumn, int bottomRow, int rightColumn, Image image, ImageFormatType imageFormat)
		{
			int a_ = 4;
			if (image != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					XlsBitmapShape xlsBitmapShape = (XlsBitmapShape)this.Add(topRow, leftColumn, image, imageFormat);
					xlsBitmapShape.RightColumn = rightColumn;
					xlsBitmapShape.BottomRow = bottomRow;
					xlsBitmapShape.UpdateHeight();
					xlsBitmapShape.UpdateWidth();
					return xlsBitmapShape;
				}
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("匹儻弽✿❁", a_));
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001D7B8 File Offset: 0x0001C7B8
		public IPictureShape Add(int topRow, int leftColumn, int bottomRow, int rightColumn, Stream stream)
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
			return this.Add(topRow, leftColumn, bottomRow, rightColumn, stream, ImageFormatType.Original);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0001D804 File Offset: 0x0001C804
		public IPictureShape Add(int topRow, int leftColumn, int bottomRow, int rightColumn, Stream stream, ImageFormatType imageFormat)
		{
			int a_ = 7;
			if (stream != null)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					Image image = spr\u17FF.ᜀ(stream);
					return this.Add(topRow, leftColumn, bottomRow, rightColumn, image, imageFormat);
				}
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("丼䬾㍀♂⑄⩆", a_));
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0001D878 File Offset: 0x0001C878
		public IPictureShape Add(int topRow, int leftColumn, int bottomRow, int rightColumn, string fileName)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return this.Add(topRow, leftColumn, bottomRow, rightColumn, fileName, ImageFormatType.Original);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0001D8C4 File Offset: 0x0001C8C4
		public IPictureShape Add(int topRow, int leftColumn, int bottomRow, int rightColumn, string fileName, ImageFormatType imageFormat)
		{
			int a_ = 15;
			for (;;)
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_6E;
					case 2:
						goto IL_35;
					case 3:
						if (fileName.Length == 0)
						{
							num = 1;
							continue;
						}
						goto IL_A8;
					}
					if (fileName == null)
					{
						num = 2;
					}
					else
					{
						num = 3;
					}
				}
				IL_35:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_86;
				}
			}
			IL_6E:
			throw new ArgumentException(RecordTableEnumerator.b("⍄⹆╈⹊͌⹎㱐㙒畔㑖㡘㕚㍜ぞᕠ䍢ݤɦ䥨๪lὮհੲ孴", a_));
			IL_86:
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("⍄⹆╈⹊͌⹎㱐㙒", a_));
			IL_A8:
			Image image = Image.FromFile(fileName);
			IPictureShape pictureShape = this.Add(topRow, leftColumn, bottomRow, rightColumn, image);
			pictureShape.Name = Path.GetFileNameWithoutExtension(fileName);
			return pictureShape;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0001D99C File Offset: 0x0001C99C
		public IPictureShape Add(int topRow, int leftColumn, Image image, int scaleWidth, int scaleHeight)
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
			return this.Add(topRow, leftColumn, image, scaleWidth, scaleHeight, ImageFormatType.Original);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0001D9E8 File Offset: 0x0001C9E8
		public IPictureShape Add(int topRow, int leftColumn, Image image, int scaleWidth, int scaleHeight, ImageFormatType imageFormat)
		{
			int a_ = 6;
			if (image != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					if (true)
					{
					}
					IPictureShape pictureShape = this.Add(topRow, leftColumn, image, imageFormat);
					pictureShape.Scale(scaleWidth, scaleHeight);
					return pictureShape;
				}
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("唻匽ℿ╁⅃", a_));
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0001DA5C File Offset: 0x0001CA5C
		public IPictureShape Add(int topRow, int leftColumn, Stream stream, int scaleWidth, int scaleHeight)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return this.Add(topRow, leftColumn, stream, scaleWidth, scaleHeight, ImageFormatType.Original);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0001DAA8 File Offset: 0x0001CAA8
		public IPictureShape Add(int topRow, int leftColumn, Stream stream, int scaleWidth, int scaleHeight, ImageFormatType imageFormat)
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
			IPictureShape pictureShape = this.Add(topRow, leftColumn, stream, imageFormat);
			pictureShape.Scale(scaleWidth, scaleHeight);
			return pictureShape;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0001DAFC File Offset: 0x0001CAFC
		public IPictureShape Add(int topRow, int leftColumn, string fileName, int scaleWidth, int scaleHeight)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return this.Add(topRow, leftColumn, fileName, scaleWidth, scaleHeight, ImageFormatType.Original);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0001DB48 File Offset: 0x0001CB48
		public IPictureShape Add(int topRow, int leftColumn, string fileName, int scaleWidth, int scaleHeight, ImageFormatType imageFormat)
		{
			int a_ = 7;
			for (;;)
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_74;
					case 1:
						goto IL_3C;
					case 2:
						if (true)
						{
						}
						break;
					case 3:
						if (fileName.Length == 0)
						{
							num = 0;
							continue;
						}
						goto IL_A6;
					}
					if (fileName == null)
					{
						num = 1;
					}
					else
					{
						num = 3;
					}
				}
				IL_3C:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_8C;
				}
			}
			IL_74:
			throw new ArgumentException(RecordTableEnumerator.b("䴼嘾≀㝂い㕆ⱈՊⱌ≎㑐獒㙔㙖㝘籚⥜罞͠٢䕤ɦѨ᭪ᥬ᙮彰", a_));
			IL_8C:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䴼嘾≀㝂い㕆ⱈՊⱌ≎㑐", a_));
			IL_A6:
			IPictureShape pictureShape = this.Add(topRow, leftColumn, fileName, imageFormat);
			pictureShape.Scale(scaleWidth, scaleHeight);
			return pictureShape;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0001DC14 File Offset: 0x0001CC14
		internal XlsPicturesCollection(spr\u1DF5 A_0, object A_1)
		{
			int a_ = 13;
			this.ᜀ = RecordTableEnumerator.b("ፂⱄ⑆㵈㹊㽌⩎", a_);
			base..ctor(A_0, A_1);
			this.ᜁ();
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0001DC4C File Offset: 0x0001CC4C
		internal new void ᜁ(IPictureShape A_0)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.InnerList.Remove(A_0);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0001DC94 File Offset: 0x0001CC94
		internal new void ᜀ(IPictureShape A_0)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.InnerList.Add(A_0);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0001DCDC File Offset: 0x0001CCDC
		private new void ᜁ()
		{
			int a_ = 13;
			this.ᜁ = (base.FindParent(typeof(XlsWorksheetBase), true) as XlsWorksheetBase);
			if (this.ᜁ != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					return;
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ፂ⑄㕆ⱈ╊㥌潎㹐ㅒ㽔㉖㩘⽚絜㱞`ൢ୤ࡦᵨ䭪ཬ੮兰ᕲᩴɶ᝸ὺ卼", a_));
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0001DD5C File Offset: 0x0001CD5C
		private new string ᜀ()
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
			return this.ᜀ + base.Count.ToString();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0001DDB0 File Offset: 0x0001CDB0
		void IDisposable.Dispose()
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
		}

		// Token: 0x0400008F RID: 143
		private string \u25D8\u00A9\u008F\u009F;

		// Token: 0x04000090 RID: 144
		private int[] \u2593\u00A4\u00A2\u008D;

		// Token: 0x04000091 RID: 145
		private new string ᜀ;

		// Token: 0x04000092 RID: 146
		private int[] \u2609\u00A8\u009A\u00A1;

		// Token: 0x04000093 RID: 147
		private new XlsWorksheetBase ᜁ;
	}
}
