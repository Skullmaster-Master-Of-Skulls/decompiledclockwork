using System;
using System.Drawing;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Collections
{
	// Token: 0x0200002C RID: 44
	public class PicturesCollection : XlsPicturesCollection
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0001CAF0 File Offset: 0x0001BAF0
		internal PicturesCollection(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x1700011A RID: 282
		public ExcelPicture this[int Index]
		{
			get
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
				return (ExcelPicture)base.InnerList[Index];
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0001CB54 File Offset: 0x0001BB54
		public new ExcelPicture Add(Image image, string pictureName)
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
			return (ExcelPicture)base.Add(image, pictureName);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0001CB9C File Offset: 0x0001BB9C
		public new ExcelPicture Add(Image image, string pictureName, ImageFormatType imageFormat)
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
			return (ExcelPicture)base.Add(image, pictureName, imageFormat);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0001CBE8 File Offset: 0x0001BBE8
		public new ExcelPicture Add(string fileName)
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
			return (ExcelPicture)base.Add(fileName);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0001CC30 File Offset: 0x0001BC30
		public new ExcelPicture Add(string fileName, ImageFormatType imageFormat)
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
			return (ExcelPicture)base.Add(fileName, imageFormat);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0001CC78 File Offset: 0x0001BC78
		public new ExcelPicture Add(int topRow, int leftColumn, Image image)
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
			return (ExcelPicture)base.Add(topRow, leftColumn, image, ImageFormatType.Original);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0001CCC4 File Offset: 0x0001BCC4
		public new ExcelPicture Add(int topRow, int leftColumn, Image image, ImageFormatType imageFormat)
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
			return (ExcelPicture)base.Add(topRow, leftColumn, image, imageFormat);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0001CD10 File Offset: 0x0001BD10
		public new ExcelPicture Add(int topRow, int leftColumn, Stream stream)
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
			return (ExcelPicture)base.Add(topRow, leftColumn, stream, ImageFormatType.Original);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001CD5C File Offset: 0x0001BD5C
		public new ExcelPicture Add(int topRow, int leftColumn, Stream stream, ImageFormatType imageFormat)
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
			return (ExcelPicture)base.Add(topRow, leftColumn, stream, imageFormat);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0001CDA8 File Offset: 0x0001BDA8
		public new ExcelPicture Add(int topRow, int leftColumn, string fileName)
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
			return (ExcelPicture)base.Add(topRow, leftColumn, fileName, ImageFormatType.Original);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0001CDF4 File Offset: 0x0001BDF4
		public new ExcelPicture Add(int topRow, int leftColumn, string fileName, ImageFormatType imageFormat)
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
			return (ExcelPicture)base.Add(topRow, leftColumn, fileName, imageFormat);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0001CE40 File Offset: 0x0001BE40
		public new ExcelPicture Add(int topRow, int leftColumn, int bottomRow, int rightColumn, Image image)
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
			return (ExcelPicture)base.Add(topRow, leftColumn, bottomRow, rightColumn, image);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0001CE90 File Offset: 0x0001BE90
		public new ExcelPicture Add(int topRow, int leftColumn, int bottomRow, int rightColumn, Image image, ImageFormatType imageFormat)
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
			return (ExcelPicture)base.Add(topRow, leftColumn, bottomRow, rightColumn, image, imageFormat);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0001CEE0 File Offset: 0x0001BEE0
		public new ExcelPicture Add(int topRow, int leftColumn, int bottomRow, int rightColumn, Stream stream)
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
			return (ExcelPicture)base.Add(topRow, leftColumn, bottomRow, rightColumn, stream, ImageFormatType.Original);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0001CF30 File Offset: 0x0001BF30
		public new ExcelPicture Add(int topRow, int leftColumn, int bottomRow, int rightColumn, Stream stream, ImageFormatType imageFormat)
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
			return (ExcelPicture)base.Add(topRow, leftColumn, bottomRow, rightColumn, stream, imageFormat);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0001CF80 File Offset: 0x0001BF80
		public new ExcelPicture Add(int topRow, int leftColumn, int bottomRow, int rightColumn, string fileName)
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
			return (ExcelPicture)base.Add(topRow, leftColumn, bottomRow, rightColumn, fileName, ImageFormatType.Original);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0001CFD0 File Offset: 0x0001BFD0
		public new ExcelPicture Add(int topRow, int leftColumn, int bottomRow, int rightColumn, string fileName, ImageFormatType imageFormat)
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
			return (ExcelPicture)base.Add(topRow, leftColumn, bottomRow, rightColumn, fileName, imageFormat);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0001D020 File Offset: 0x0001C020
		public new ExcelPicture Add(int topRow, int leftColumn, Image image, int scaleWidth, int scaleHeight)
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
			return (ExcelPicture)base.Add(topRow, leftColumn, image, scaleWidth, scaleHeight, ImageFormatType.Original);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0001D070 File Offset: 0x0001C070
		public new ExcelPicture Add(int topRow, int leftColumn, Image image, int scaleWidth, int scaleHeight, ImageFormatType imageFormat)
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
			return (ExcelPicture)base.Add(topRow, leftColumn, image, scaleWidth, scaleHeight, imageFormat);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0001D0C0 File Offset: 0x0001C0C0
		public new ExcelPicture Add(int topRow, int leftColumn, Stream stream, int scaleWidth, int scaleHeight)
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
			return (ExcelPicture)base.Add(topRow, leftColumn, stream, scaleWidth, scaleHeight, ImageFormatType.Original);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001D110 File Offset: 0x0001C110
		public new ExcelPicture Add(int topRow, int leftColumn, Stream stream, int scaleWidth, int scaleHeight, ImageFormatType imageFormat)
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
			return (ExcelPicture)base.Add(topRow, leftColumn, stream, scaleWidth, scaleHeight, imageFormat);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001D160 File Offset: 0x0001C160
		public new ExcelPicture Add(int topRow, int leftColumn, string fileName, int scaleWidth, int scaleHeight)
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
			return (ExcelPicture)base.Add(topRow, leftColumn, fileName, scaleWidth, scaleHeight);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0001D1B0 File Offset: 0x0001C1B0
		public new ExcelPicture Add(int topRow, int leftColumn, string fileName, int scaleWidth, int scaleHeight, ImageFormatType imageFormat)
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
			return (ExcelPicture)base.Add(topRow, leftColumn, fileName, scaleWidth, scaleHeight, imageFormat);
		}
	}
}
