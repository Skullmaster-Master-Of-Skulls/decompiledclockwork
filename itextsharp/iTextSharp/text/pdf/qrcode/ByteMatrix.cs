using System;
using System.Text;

namespace iTextSharp.text.pdf.qrcode
{
	// Token: 0x02000457 RID: 1111
	public sealed class ByteMatrix
	{
		// Token: 0x06002585 RID: 9605 RVA: 0x000E39CC File Offset: 0x000E29CC
		public ByteMatrix(int width, int height)
		{
			this.bytes = new sbyte[height][];
			for (int i = 0; i < height; i++)
			{
				this.bytes[i] = new sbyte[width];
			}
			this.width = width;
			this.height = height;
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x000E3A13 File Offset: 0x000E2A13
		public int GetHeight()
		{
			return this.height;
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x000E3A1B File Offset: 0x000E2A1B
		public int GetWidth()
		{
			return this.width;
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000E3A23 File Offset: 0x000E2A23
		public sbyte Get(int x, int y)
		{
			return this.bytes[y][x];
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x000E3A2F File Offset: 0x000E2A2F
		public sbyte[][] GetArray()
		{
			return this.bytes;
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x000E3A37 File Offset: 0x000E2A37
		public void Set(int x, int y, sbyte value)
		{
			this.bytes[y][x] = value;
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x000E3A44 File Offset: 0x000E2A44
		public void Set(int x, int y, int value)
		{
			this.bytes[y][x] = (sbyte)value;
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x000E3A54 File Offset: 0x000E2A54
		public void Clear(sbyte value)
		{
			for (int i = 0; i < this.height; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					this.bytes[i][j] = value;
				}
			}
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x000E3A90 File Offset: 0x000E2A90
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(2 * this.width * this.height + 2);
			for (int i = 0; i < this.height; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					switch (this.bytes[i][j])
					{
					case 0:
						stringBuilder.Append(" 0");
						break;
					case 1:
						stringBuilder.Append(" 1");
						break;
					default:
						stringBuilder.Append("  ");
						break;
					}
				}
				stringBuilder.Append('\n');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001A2D RID: 6701
		private sbyte[][] bytes;

		// Token: 0x04001A2E RID: 6702
		private int width;

		// Token: 0x04001A2F RID: 6703
		private int height;
	}
}
