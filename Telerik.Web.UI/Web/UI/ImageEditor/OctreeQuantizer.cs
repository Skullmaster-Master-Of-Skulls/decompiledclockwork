using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000BA2 RID: 2978
	internal class OctreeQuantizer : Quantizer
	{
		// Token: 0x0600706C RID: 28780 RVA: 0x001A3C78 File Offset: 0x001A1E78
		public OctreeQuantizer(int maxColors, int maxColorBits) : base(false)
		{
			if (maxColors > 255)
			{
				throw new ArgumentOutOfRangeException("maxColors", maxColors, "The number of colors should be less than 256");
			}
			if (maxColorBits < 1 | maxColorBits > 8)
			{
				throw new ArgumentOutOfRangeException("maxColorBits", maxColorBits, "This should be between 1 and 8");
			}
			this._octree = new OctreeQuantizer.Octree(maxColorBits);
			this._maxColors = maxColors;
		}

		// Token: 0x0600706D RID: 28781 RVA: 0x001A3CDE File Offset: 0x001A1EDE
		protected override void InitialQuantizePixel(Quantizer.Color32 pixel)
		{
			this._octree.AddColor(pixel);
		}

		// Token: 0x0600706E RID: 28782 RVA: 0x001A3CEC File Offset: 0x001A1EEC
		protected override byte QuantizePixel(Quantizer.Color32 pixel)
		{
			byte result = (byte)this._maxColors;
			if (pixel.Alpha > 0)
			{
				result = (byte)this._octree.GetPaletteIndex(pixel);
			}
			return result;
		}

		// Token: 0x0600706F RID: 28783 RVA: 0x001A3D1C File Offset: 0x001A1F1C
		protected override ColorPalette GetPalette(ColorPalette original)
		{
			ArrayList arrayList = this._octree.Palletize(this._maxColors - 1);
			for (int i = 0; i < arrayList.Count; i++)
			{
				Color color = (Color)arrayList[i];
				if (color.ToArgb() == Color.Transparent.ToArgb())
				{
					color = Color.FromArgb(0, 0, 0, 0);
				}
				original.Entries[i] = color;
			}
			for (int j = arrayList.Count; j < this._maxColors; j++)
			{
				original.Entries[j] = Color.FromArgb(255, 0, 0, 0);
			}
			original.Entries[this._maxColors] = Color.FromArgb(0, Color.Transparent);
			return original;
		}

		// Token: 0x04001E46 RID: 7750
		private OctreeQuantizer.Octree _octree;

		// Token: 0x04001E47 RID: 7751
		private int _maxColors;

		// Token: 0x02000BA3 RID: 2979
		private class Octree
		{
			// Token: 0x06007070 RID: 28784 RVA: 0x001A3DE4 File Offset: 0x001A1FE4
			public Octree(int maxColorBits)
			{
				this._maxColorBits = maxColorBits;
				this._leafCount = 0;
				this._reducibleNodes = new OctreeQuantizer.Octree.OctreeNode[9];
				this._root = new OctreeQuantizer.Octree.OctreeNode(0, this._maxColorBits, this);
				this._previousColor = 0;
				this._previousNode = null;
			}

			// Token: 0x06007071 RID: 28785 RVA: 0x001A3E34 File Offset: 0x001A2034
			public void AddColor(Quantizer.Color32 pixel)
			{
				if (this._previousColor != pixel.ARGB)
				{
					this._previousColor = pixel.ARGB;
					this._root.AddColor(pixel, this._maxColorBits, 0, this);
					return;
				}
				if (this._previousNode == null)
				{
					this._previousColor = pixel.ARGB;
					this._root.AddColor(pixel, this._maxColorBits, 0, this);
					return;
				}
				this._previousNode.Increment(pixel);
			}

			// Token: 0x06007072 RID: 28786 RVA: 0x001A3EA8 File Offset: 0x001A20A8
			public void Reduce()
			{
				int num = this._maxColorBits - 1;
				while (num > 0 && this._reducibleNodes[num] == null)
				{
					num--;
				}
				OctreeQuantizer.Octree.OctreeNode octreeNode = this._reducibleNodes[num];
				this._reducibleNodes[num] = octreeNode.NextReducible;
				this._leafCount -= octreeNode.Reduce();
				this._previousNode = null;
			}

			// Token: 0x170024C6 RID: 9414
			// (get) Token: 0x06007073 RID: 28787 RVA: 0x001A3F03 File Offset: 0x001A2103
			// (set) Token: 0x06007074 RID: 28788 RVA: 0x001A3F0B File Offset: 0x001A210B
			public int Leaves
			{
				get
				{
					return this._leafCount;
				}
				set
				{
					this._leafCount = value;
				}
			}

			// Token: 0x06007075 RID: 28789 RVA: 0x001A3F14 File Offset: 0x001A2114
			protected OctreeQuantizer.Octree.OctreeNode[] ReducibleNodes()
			{
				return this._reducibleNodes;
			}

			// Token: 0x06007076 RID: 28790 RVA: 0x001A3F1C File Offset: 0x001A211C
			protected void TrackPrevious(OctreeQuantizer.Octree.OctreeNode node)
			{
				this._previousNode = node;
			}

			// Token: 0x06007077 RID: 28791 RVA: 0x001A3F28 File Offset: 0x001A2128
			public ArrayList Palletize(int colorCount)
			{
				while (this.Leaves > colorCount)
				{
					this.Reduce();
				}
				ArrayList arrayList = new ArrayList(this.Leaves);
				int num = 0;
				this._root.ConstructPalette(arrayList, ref num);
				return arrayList;
			}

			// Token: 0x06007078 RID: 28792 RVA: 0x001A3F63 File Offset: 0x001A2163
			public int GetPaletteIndex(Quantizer.Color32 pixel)
			{
				return this._root.GetPaletteIndex(pixel, 0);
			}

			// Token: 0x04001E48 RID: 7752
			private static int[] mask = new int[]
			{
				128,
				64,
				32,
				16,
				8,
				4,
				2,
				1
			};

			// Token: 0x04001E49 RID: 7753
			private OctreeQuantizer.Octree.OctreeNode _root;

			// Token: 0x04001E4A RID: 7754
			private int _leafCount;

			// Token: 0x04001E4B RID: 7755
			private OctreeQuantizer.Octree.OctreeNode[] _reducibleNodes;

			// Token: 0x04001E4C RID: 7756
			private int _maxColorBits;

			// Token: 0x04001E4D RID: 7757
			private OctreeQuantizer.Octree.OctreeNode _previousNode;

			// Token: 0x04001E4E RID: 7758
			private int _previousColor;

			// Token: 0x02000BA4 RID: 2980
			protected class OctreeNode
			{
				// Token: 0x0600707A RID: 28794 RVA: 0x001A3FB0 File Offset: 0x001A21B0
				public OctreeNode(int level, int colorBits, OctreeQuantizer.Octree octree)
				{
					this._leaf = (level == colorBits);
					this._red = (this._green = (this._blue = 0));
					this._pixelCount = 0;
					if (this._leaf)
					{
						octree.Leaves++;
						this._nextReducible = null;
						this._children = null;
						return;
					}
					OctreeQuantizer.Octree.OctreeNode[] array = octree.ReducibleNodes();
					this._nextReducible = array[level];
					array[level] = this;
					this._children = new OctreeQuantizer.Octree.OctreeNode[8];
				}

				// Token: 0x0600707B RID: 28795 RVA: 0x001A4034 File Offset: 0x001A2234
				public void AddColor(Quantizer.Color32 pixel, int colorBits, int level, OctreeQuantizer.Octree octree)
				{
					if (this._leaf)
					{
						this.Increment(pixel);
						octree.TrackPrevious(this);
						return;
					}
					checked
					{
						int num = 7 - level;
						int num2 = ((int)pixel.Red & OctreeQuantizer.Octree.mask[level]) >> num - 2 | ((int)pixel.Green & OctreeQuantizer.Octree.mask[level]) >> num - 1 | ((int)pixel.Blue & OctreeQuantizer.Octree.mask[level]) >> num;
						OctreeQuantizer.Octree.OctreeNode octreeNode = this._children[num2];
						if (octreeNode == null)
						{
							octreeNode = new OctreeQuantizer.Octree.OctreeNode(level + 1, colorBits, octree);
							this._children[num2] = octreeNode;
						}
						octreeNode.AddColor(pixel, colorBits, level + 1, octree);
					}
				}

				// Token: 0x170024C7 RID: 9415
				// (get) Token: 0x0600707C RID: 28796 RVA: 0x001A40CE File Offset: 0x001A22CE
				public OctreeQuantizer.Octree.OctreeNode NextReducible
				{
					get
					{
						return this._nextReducible;
					}
				}

				// Token: 0x0600707D RID: 28797 RVA: 0x001A40D8 File Offset: 0x001A22D8
				public int Reduce()
				{
					this._red = (this._green = (this._blue = 0));
					int num = 0;
					for (int i = 0; i < 8; i++)
					{
						if (this._children[i] != null)
						{
							this._red += this._children[i]._red;
							this._green += this._children[i]._green;
							this._blue += this._children[i]._blue;
							this._pixelCount += this._children[i]._pixelCount;
							num++;
							this._children[i] = null;
						}
					}
					this._leaf = true;
					return num - 1;
				}

				// Token: 0x0600707E RID: 28798 RVA: 0x001A419C File Offset: 0x001A239C
				public void ConstructPalette(ArrayList palette, ref int paletteIndex)
				{
					if (this._leaf)
					{
						this._paletteIndex = paletteIndex++;
						palette.Add(Color.FromArgb(this._red / this._pixelCount, this._green / this._pixelCount, this._blue / this._pixelCount));
						return;
					}
					for (int i = 0; i < 8; i++)
					{
						if (this._children[i] != null)
						{
							this._children[i].ConstructPalette(palette, ref paletteIndex);
						}
					}
				}

				// Token: 0x0600707F RID: 28799 RVA: 0x001A4220 File Offset: 0x001A2420
				public int GetPaletteIndex(Quantizer.Color32 pixel, int level)
				{
					int paletteIndex = this._paletteIndex;
					checked
					{
						if (!this._leaf)
						{
							int num = 7 - level;
							int num2 = ((int)pixel.Red & OctreeQuantizer.Octree.mask[level]) >> num - 2 | ((int)pixel.Green & OctreeQuantizer.Octree.mask[level]) >> num - 1 | ((int)pixel.Blue & OctreeQuantizer.Octree.mask[level]) >> num;
							if (this._children[num2] == null)
							{
								throw new ArgumentException("Didn't expect this!");
							}
							paletteIndex = this._children[num2].GetPaletteIndex(pixel, level + 1);
						}
						return paletteIndex;
					}
				}

				// Token: 0x06007080 RID: 28800 RVA: 0x001A42B0 File Offset: 0x001A24B0
				public void Increment(Quantizer.Color32 pixel)
				{
					this._pixelCount++;
					this._red += (int)pixel.Red;
					this._green += (int)pixel.Green;
					this._blue += (int)pixel.Blue;
				}

				// Token: 0x04001E4F RID: 7759
				private bool _leaf;

				// Token: 0x04001E50 RID: 7760
				private int _pixelCount;

				// Token: 0x04001E51 RID: 7761
				private int _red;

				// Token: 0x04001E52 RID: 7762
				private int _green;

				// Token: 0x04001E53 RID: 7763
				private int _blue;

				// Token: 0x04001E54 RID: 7764
				private OctreeQuantizer.Octree.OctreeNode[] _children;

				// Token: 0x04001E55 RID: 7765
				private OctreeQuantizer.Octree.OctreeNode _nextReducible;

				// Token: 0x04001E56 RID: 7766
				private int _paletteIndex;
			}
		}
	}
}
