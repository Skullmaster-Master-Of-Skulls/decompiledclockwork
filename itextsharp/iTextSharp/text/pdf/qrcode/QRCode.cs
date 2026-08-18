using System;
using System.Text;

namespace iTextSharp.text.pdf.qrcode
{
	// Token: 0x020004DE RID: 1246
	public sealed class QRCode
	{
		// Token: 0x06002A70 RID: 10864 RVA: 0x00103284 File Offset: 0x00102284
		public QRCode()
		{
			this.mode = null;
			this.ecLevel = null;
			this.version = -1;
			this.matrixWidth = -1;
			this.maskPattern = -1;
			this.numTotalBytes = -1;
			this.numDataBytes = -1;
			this.numECBytes = -1;
			this.numRSBlocks = -1;
			this.matrix = null;
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x001032DD File Offset: 0x001022DD
		public Mode GetMode()
		{
			return this.mode;
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x001032E5 File Offset: 0x001022E5
		public ErrorCorrectionLevel GetECLevel()
		{
			return this.ecLevel;
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x001032ED File Offset: 0x001022ED
		public int GetVersion()
		{
			return this.version;
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x001032F5 File Offset: 0x001022F5
		public int GetMatrixWidth()
		{
			return this.matrixWidth;
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x001032FD File Offset: 0x001022FD
		public int GetMaskPattern()
		{
			return this.maskPattern;
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x00103305 File Offset: 0x00102305
		public int GetNumTotalBytes()
		{
			return this.numTotalBytes;
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x0010330D File Offset: 0x0010230D
		public int GetNumDataBytes()
		{
			return this.numDataBytes;
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x00103315 File Offset: 0x00102315
		public int GetNumECBytes()
		{
			return this.numECBytes;
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x0010331D File Offset: 0x0010231D
		public int GetNumRSBlocks()
		{
			return this.numRSBlocks;
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x00103325 File Offset: 0x00102325
		public ByteMatrix GetMatrix()
		{
			return this.matrix;
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x00103330 File Offset: 0x00102330
		public int At(int x, int y)
		{
			int num = (int)this.matrix.Get(x, y);
			if (num != 0 && num != 1)
			{
				throw new ArgumentException("Bad value");
			}
			return num;
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x00103360 File Offset: 0x00102360
		public bool IsValid()
		{
			return this.mode != null && this.ecLevel != null && this.version != -1 && this.matrixWidth != -1 && this.maskPattern != -1 && this.numTotalBytes != -1 && this.numDataBytes != -1 && this.numECBytes != -1 && this.numRSBlocks != -1 && QRCode.IsValidMaskPattern(this.maskPattern) && this.numTotalBytes == this.numDataBytes + this.numECBytes && this.matrix != null && this.matrixWidth == this.matrix.GetWidth() && this.matrix.GetWidth() == this.matrix.GetHeight();
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x00103420 File Offset: 0x00102420
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			stringBuilder.Append("<<\n");
			stringBuilder.Append(" mode: ");
			stringBuilder.Append(this.mode);
			stringBuilder.Append("\n ecLevel: ");
			stringBuilder.Append(this.ecLevel);
			stringBuilder.Append("\n version: ");
			stringBuilder.Append(this.version);
			stringBuilder.Append("\n matrixWidth: ");
			stringBuilder.Append(this.matrixWidth);
			stringBuilder.Append("\n maskPattern: ");
			stringBuilder.Append(this.maskPattern);
			stringBuilder.Append("\n numTotalBytes: ");
			stringBuilder.Append(this.numTotalBytes);
			stringBuilder.Append("\n numDataBytes: ");
			stringBuilder.Append(this.numDataBytes);
			stringBuilder.Append("\n numECBytes: ");
			stringBuilder.Append(this.numECBytes);
			stringBuilder.Append("\n numRSBlocks: ");
			stringBuilder.Append(this.numRSBlocks);
			if (this.matrix == null)
			{
				stringBuilder.Append("\n matrix: null\n");
			}
			else
			{
				stringBuilder.Append("\n matrix:\n");
				stringBuilder.Append(this.matrix.ToString());
			}
			stringBuilder.Append(">>\n");
			return stringBuilder.ToString();
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x0010356B File Offset: 0x0010256B
		public void SetMode(Mode value)
		{
			this.mode = value;
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x00103574 File Offset: 0x00102574
		public void SetECLevel(ErrorCorrectionLevel value)
		{
			this.ecLevel = value;
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x0010357D File Offset: 0x0010257D
		public void SetVersion(int value)
		{
			this.version = value;
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x00103586 File Offset: 0x00102586
		public void SetMatrixWidth(int value)
		{
			this.matrixWidth = value;
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x0010358F File Offset: 0x0010258F
		public void SetMaskPattern(int value)
		{
			this.maskPattern = value;
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x00103598 File Offset: 0x00102598
		public void SetNumTotalBytes(int value)
		{
			this.numTotalBytes = value;
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x001035A1 File Offset: 0x001025A1
		public void SetNumDataBytes(int value)
		{
			this.numDataBytes = value;
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x001035AA File Offset: 0x001025AA
		public void SetNumECBytes(int value)
		{
			this.numECBytes = value;
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x001035B3 File Offset: 0x001025B3
		public void SetNumRSBlocks(int value)
		{
			this.numRSBlocks = value;
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x001035BC File Offset: 0x001025BC
		public void SetMatrix(ByteMatrix value)
		{
			this.matrix = value;
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x001035C5 File Offset: 0x001025C5
		public static bool IsValidMaskPattern(int maskPattern)
		{
			return maskPattern >= 0 && maskPattern < 8;
		}

		// Token: 0x04001D84 RID: 7556
		public const int NUM_MASK_PATTERNS = 8;

		// Token: 0x04001D85 RID: 7557
		private Mode mode;

		// Token: 0x04001D86 RID: 7558
		private ErrorCorrectionLevel ecLevel;

		// Token: 0x04001D87 RID: 7559
		private int version;

		// Token: 0x04001D88 RID: 7560
		private int matrixWidth;

		// Token: 0x04001D89 RID: 7561
		private int maskPattern;

		// Token: 0x04001D8A RID: 7562
		private int numTotalBytes;

		// Token: 0x04001D8B RID: 7563
		private int numDataBytes;

		// Token: 0x04001D8C RID: 7564
		private int numECBytes;

		// Token: 0x04001D8D RID: 7565
		private int numRSBlocks;

		// Token: 0x04001D8E RID: 7566
		private ByteMatrix matrix;
	}
}
