using System;
using System.Diagnostics;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000333 RID: 819
	internal class PropertyStore
	{
		// Token: 0x06003545 RID: 13637 RVA: 0x000F1BD4 File Offset: 0x000EFDD4
		public bool ContainsInteger(int key)
		{
			bool result;
			this.GetInteger(key, out result);
			return result;
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000F1BEC File Offset: 0x000EFDEC
		public bool ContainsObject(int key)
		{
			bool result;
			this.GetObject(key, out result);
			return result;
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x000F1C04 File Offset: 0x000EFE04
		public static int CreateKey()
		{
			return PropertyStore.currentKey++;
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x000F1C14 File Offset: 0x000EFE14
		public Color GetColor(int key)
		{
			bool flag;
			return this.GetColor(key, out flag);
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000F1C2C File Offset: 0x000EFE2C
		public Color GetColor(int key, out bool found)
		{
			object @object = this.GetObject(key, out found);
			if (found)
			{
				PropertyStore.ColorWrapper colorWrapper = @object as PropertyStore.ColorWrapper;
				if (colorWrapper != null)
				{
					return colorWrapper.Color;
				}
			}
			found = false;
			return Color.Empty;
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x000F1C60 File Offset: 0x000EFE60
		public Padding GetPadding(int key)
		{
			bool flag;
			return this.GetPadding(key, out flag);
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x000F1C78 File Offset: 0x000EFE78
		public Padding GetPadding(int key, out bool found)
		{
			object @object = this.GetObject(key, out found);
			if (found)
			{
				PropertyStore.PaddingWrapper paddingWrapper = @object as PropertyStore.PaddingWrapper;
				if (paddingWrapper != null)
				{
					return paddingWrapper.Padding;
				}
			}
			found = false;
			return Padding.Empty;
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x000F1CAC File Offset: 0x000EFEAC
		public Size GetSize(int key, out bool found)
		{
			object @object = this.GetObject(key, out found);
			if (found)
			{
				PropertyStore.SizeWrapper sizeWrapper = @object as PropertyStore.SizeWrapper;
				if (sizeWrapper != null)
				{
					return sizeWrapper.Size;
				}
			}
			found = false;
			return Size.Empty;
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x000F1CE0 File Offset: 0x000EFEE0
		public Rectangle GetRectangle(int key)
		{
			bool flag;
			return this.GetRectangle(key, out flag);
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x000F1CF8 File Offset: 0x000EFEF8
		public Rectangle GetRectangle(int key, out bool found)
		{
			object @object = this.GetObject(key, out found);
			if (found)
			{
				PropertyStore.RectangleWrapper rectangleWrapper = @object as PropertyStore.RectangleWrapper;
				if (rectangleWrapper != null)
				{
					return rectangleWrapper.Rectangle;
				}
			}
			found = false;
			return Rectangle.Empty;
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x000F1D2C File Offset: 0x000EFF2C
		public int GetInteger(int key)
		{
			bool flag;
			return this.GetInteger(key, out flag);
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000F1D44 File Offset: 0x000EFF44
		public int GetInteger(int key, out bool found)
		{
			int result = 0;
			short num;
			short entryKey = this.SplitKey(key, out num);
			found = false;
			int num2;
			if (this.LocateIntegerEntry(entryKey, out num2) && (1 << (int)num & (int)this.intEntries[num2].Mask) != 0)
			{
				found = true;
				switch (num)
				{
				case 0:
					result = this.intEntries[num2].Value1;
					break;
				case 1:
					result = this.intEntries[num2].Value2;
					break;
				case 2:
					result = this.intEntries[num2].Value3;
					break;
				case 3:
					result = this.intEntries[num2].Value4;
					break;
				}
			}
			return result;
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000F1DF4 File Offset: 0x000EFFF4
		public object GetObject(int key)
		{
			bool flag;
			return this.GetObject(key, out flag);
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000F1E0C File Offset: 0x000F000C
		public object GetObject(int key, out bool found)
		{
			object result = null;
			short num;
			short entryKey = this.SplitKey(key, out num);
			found = false;
			int num2;
			if (this.LocateObjectEntry(entryKey, out num2) && (1 << (int)num & (int)this.objEntries[num2].Mask) != 0)
			{
				found = true;
				switch (num)
				{
				case 0:
					result = this.objEntries[num2].Value1;
					break;
				case 1:
					result = this.objEntries[num2].Value2;
					break;
				case 2:
					result = this.objEntries[num2].Value3;
					break;
				case 3:
					result = this.objEntries[num2].Value4;
					break;
				}
			}
			return result;
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000F1EBC File Offset: 0x000F00BC
		private bool LocateIntegerEntry(short entryKey, out int index)
		{
			if (this.intEntries == null)
			{
				index = 0;
				return false;
			}
			int num = this.intEntries.Length;
			if (num > 16)
			{
				int num2 = num - 1;
				int num3 = 0;
				int num4;
				for (;;)
				{
					num4 = (num2 + num3) / 2;
					short key = this.intEntries[num4].Key;
					if (key == entryKey)
					{
						break;
					}
					if (entryKey < key)
					{
						num2 = num4 - 1;
					}
					else
					{
						num3 = num4 + 1;
					}
					if (num2 < num3)
					{
						goto Block_14;
					}
				}
				index = num4;
				return true;
				Block_14:
				index = num4;
				if (entryKey > this.intEntries[num4].Key)
				{
					index++;
				}
				return false;
			}
			index = 0;
			int num5 = num / 2;
			if (this.intEntries[num5].Key <= entryKey)
			{
				index = num5;
			}
			if (this.intEntries[index].Key == entryKey)
			{
				return true;
			}
			num5 = (num + 1) / 4;
			if (this.intEntries[index + num5].Key <= entryKey)
			{
				index += num5;
				if (this.intEntries[index].Key == entryKey)
				{
					return true;
				}
			}
			num5 = (num + 3) / 8;
			if (this.intEntries[index + num5].Key <= entryKey)
			{
				index += num5;
				if (this.intEntries[index].Key == entryKey)
				{
					return true;
				}
			}
			num5 = (num + 7) / 16;
			if (this.intEntries[index + num5].Key <= entryKey)
			{
				index += num5;
				if (this.intEntries[index].Key == entryKey)
				{
					return true;
				}
			}
			if (entryKey > this.intEntries[index].Key)
			{
				index++;
			}
			return false;
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000F2050 File Offset: 0x000F0250
		private bool LocateObjectEntry(short entryKey, out int index)
		{
			if (this.objEntries == null)
			{
				index = 0;
				return false;
			}
			int num = this.objEntries.Length;
			if (num > 16)
			{
				int num2 = num - 1;
				int num3 = 0;
				int num4;
				for (;;)
				{
					num4 = (num2 + num3) / 2;
					short key = this.objEntries[num4].Key;
					if (key == entryKey)
					{
						break;
					}
					if (entryKey < key)
					{
						num2 = num4 - 1;
					}
					else
					{
						num3 = num4 + 1;
					}
					if (num2 < num3)
					{
						goto Block_14;
					}
				}
				index = num4;
				return true;
				Block_14:
				index = num4;
				if (entryKey > this.objEntries[num4].Key)
				{
					index++;
				}
				return false;
			}
			index = 0;
			int num5 = num / 2;
			if (this.objEntries[num5].Key <= entryKey)
			{
				index = num5;
			}
			if (this.objEntries[index].Key == entryKey)
			{
				return true;
			}
			num5 = (num + 1) / 4;
			if (this.objEntries[index + num5].Key <= entryKey)
			{
				index += num5;
				if (this.objEntries[index].Key == entryKey)
				{
					return true;
				}
			}
			num5 = (num + 3) / 8;
			if (this.objEntries[index + num5].Key <= entryKey)
			{
				index += num5;
				if (this.objEntries[index].Key == entryKey)
				{
					return true;
				}
			}
			num5 = (num + 7) / 16;
			if (this.objEntries[index + num5].Key <= entryKey)
			{
				index += num5;
				if (this.objEntries[index].Key == entryKey)
				{
					return true;
				}
			}
			if (entryKey > this.objEntries[index].Key)
			{
				index++;
			}
			return false;
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000F21E4 File Offset: 0x000F03E4
		public void RemoveInteger(int key)
		{
			short num;
			short entryKey = this.SplitKey(key, out num);
			int num2;
			if (this.LocateIntegerEntry(entryKey, out num2))
			{
				if ((1 << (int)num & (int)this.intEntries[num2].Mask) == 0)
				{
					return;
				}
				PropertyStore.IntegerEntry[] array = this.intEntries;
				int num3 = num2;
				array[num3].Mask = (array[num3].Mask & ~(short)(1 << (int)num));
				if (this.intEntries[num2].Mask == 0)
				{
					PropertyStore.IntegerEntry[] array2 = new PropertyStore.IntegerEntry[this.intEntries.Length - 1];
					if (num2 > 0)
					{
						Array.Copy(this.intEntries, 0, array2, 0, num2);
					}
					if (num2 < array2.Length)
					{
						Array.Copy(this.intEntries, num2 + 1, array2, num2, this.intEntries.Length - num2 - 1);
					}
					this.intEntries = array2;
					return;
				}
				switch (num)
				{
				case 0:
					this.intEntries[num2].Value1 = 0;
					return;
				case 1:
					this.intEntries[num2].Value2 = 0;
					return;
				case 2:
					this.intEntries[num2].Value3 = 0;
					return;
				case 3:
					this.intEntries[num2].Value4 = 0;
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000F2308 File Offset: 0x000F0508
		public void RemoveObject(int key)
		{
			short num;
			short entryKey = this.SplitKey(key, out num);
			int num2;
			if (this.LocateObjectEntry(entryKey, out num2))
			{
				if ((1 << (int)num & (int)this.objEntries[num2].Mask) == 0)
				{
					return;
				}
				PropertyStore.ObjectEntry[] array = this.objEntries;
				int num3 = num2;
				array[num3].Mask = (array[num3].Mask & ~(short)(1 << (int)num));
				if (this.objEntries[num2].Mask == 0)
				{
					if (this.objEntries.Length == 1)
					{
						this.objEntries = null;
						return;
					}
					PropertyStore.ObjectEntry[] array2 = new PropertyStore.ObjectEntry[this.objEntries.Length - 1];
					if (num2 > 0)
					{
						Array.Copy(this.objEntries, 0, array2, 0, num2);
					}
					if (num2 < array2.Length)
					{
						Array.Copy(this.objEntries, num2 + 1, array2, num2, this.objEntries.Length - num2 - 1);
					}
					this.objEntries = array2;
					return;
				}
				else
				{
					switch (num)
					{
					case 0:
						this.objEntries[num2].Value1 = null;
						return;
					case 1:
						this.objEntries[num2].Value2 = null;
						return;
					case 2:
						this.objEntries[num2].Value3 = null;
						return;
					case 3:
						this.objEntries[num2].Value4 = null;
						break;
					default:
						return;
					}
				}
			}
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x000F243C File Offset: 0x000F063C
		public void SetColor(int key, Color value)
		{
			bool flag;
			object @object = this.GetObject(key, out flag);
			if (!flag)
			{
				this.SetObject(key, new PropertyStore.ColorWrapper(value));
				return;
			}
			PropertyStore.ColorWrapper colorWrapper = @object as PropertyStore.ColorWrapper;
			if (colorWrapper != null)
			{
				colorWrapper.Color = value;
				return;
			}
			this.SetObject(key, new PropertyStore.ColorWrapper(value));
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x000F2484 File Offset: 0x000F0684
		public void SetPadding(int key, Padding value)
		{
			bool flag;
			object @object = this.GetObject(key, out flag);
			if (!flag)
			{
				this.SetObject(key, new PropertyStore.PaddingWrapper(value));
				return;
			}
			PropertyStore.PaddingWrapper paddingWrapper = @object as PropertyStore.PaddingWrapper;
			if (paddingWrapper != null)
			{
				paddingWrapper.Padding = value;
				return;
			}
			this.SetObject(key, new PropertyStore.PaddingWrapper(value));
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x000F24CC File Offset: 0x000F06CC
		public void SetRectangle(int key, Rectangle value)
		{
			bool flag;
			object @object = this.GetObject(key, out flag);
			if (!flag)
			{
				this.SetObject(key, new PropertyStore.RectangleWrapper(value));
				return;
			}
			PropertyStore.RectangleWrapper rectangleWrapper = @object as PropertyStore.RectangleWrapper;
			if (rectangleWrapper != null)
			{
				rectangleWrapper.Rectangle = value;
				return;
			}
			this.SetObject(key, new PropertyStore.RectangleWrapper(value));
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000F2514 File Offset: 0x000F0714
		public void SetSize(int key, Size value)
		{
			bool flag;
			object @object = this.GetObject(key, out flag);
			if (!flag)
			{
				this.SetObject(key, new PropertyStore.SizeWrapper(value));
				return;
			}
			PropertyStore.SizeWrapper sizeWrapper = @object as PropertyStore.SizeWrapper;
			if (sizeWrapper != null)
			{
				sizeWrapper.Size = value;
				return;
			}
			this.SetObject(key, new PropertyStore.SizeWrapper(value));
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000F255C File Offset: 0x000F075C
		public void SetInteger(int key, int value)
		{
			short num2;
			short num = this.SplitKey(key, out num2);
			int num3;
			if (!this.LocateIntegerEntry(num, out num3))
			{
				if (this.intEntries != null)
				{
					PropertyStore.IntegerEntry[] destinationArray = new PropertyStore.IntegerEntry[this.intEntries.Length + 1];
					if (num3 > 0)
					{
						Array.Copy(this.intEntries, 0, destinationArray, 0, num3);
					}
					if (this.intEntries.Length - num3 > 0)
					{
						Array.Copy(this.intEntries, num3, destinationArray, num3 + 1, this.intEntries.Length - num3);
					}
					this.intEntries = destinationArray;
				}
				else
				{
					this.intEntries = new PropertyStore.IntegerEntry[1];
				}
				this.intEntries[num3].Key = num;
			}
			switch (num2)
			{
			case 0:
				this.intEntries[num3].Value1 = value;
				break;
			case 1:
				this.intEntries[num3].Value2 = value;
				break;
			case 2:
				this.intEntries[num3].Value3 = value;
				break;
			case 3:
				this.intEntries[num3].Value4 = value;
				break;
			}
			this.intEntries[num3].Mask = (short)(1 << (int)num2 | (int)((ushort)this.intEntries[num3].Mask));
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000F2688 File Offset: 0x000F0888
		public void SetObject(int key, object value)
		{
			short num2;
			short num = this.SplitKey(key, out num2);
			int num3;
			if (!this.LocateObjectEntry(num, out num3))
			{
				if (this.objEntries != null)
				{
					PropertyStore.ObjectEntry[] destinationArray = new PropertyStore.ObjectEntry[this.objEntries.Length + 1];
					if (num3 > 0)
					{
						Array.Copy(this.objEntries, 0, destinationArray, 0, num3);
					}
					if (this.objEntries.Length - num3 > 0)
					{
						Array.Copy(this.objEntries, num3, destinationArray, num3 + 1, this.objEntries.Length - num3);
					}
					this.objEntries = destinationArray;
				}
				else
				{
					this.objEntries = new PropertyStore.ObjectEntry[1];
				}
				this.objEntries[num3].Key = num;
			}
			switch (num2)
			{
			case 0:
				this.objEntries[num3].Value1 = value;
				break;
			case 1:
				this.objEntries[num3].Value2 = value;
				break;
			case 2:
				this.objEntries[num3].Value3 = value;
				break;
			case 3:
				this.objEntries[num3].Value4 = value;
				break;
			}
			this.objEntries[num3].Mask = (short)((int)((ushort)this.objEntries[num3].Mask) | 1 << (int)num2);
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000F27B4 File Offset: 0x000F09B4
		private short SplitKey(int key, out short element)
		{
			element = (short)(key & 3);
			return (short)((long)key & (long)((ulong)-4));
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000F27C4 File Offset: 0x000F09C4
		[Conditional("DEBUG_PROPERTYSTORE")]
		private void Debug_VerifyLocateIntegerEntry(int index, short entryKey, int length)
		{
			int num = length - 1;
			int num2 = 0;
			int num3;
			do
			{
				num3 = (num + num2) / 2;
				short key = this.intEntries[num3].Key;
				if (key != entryKey)
				{
					if (entryKey < key)
					{
						num = num3 - 1;
					}
					else
					{
						num2 = num3 + 1;
					}
				}
			}
			while (num >= num2);
			if (entryKey > this.intEntries[num3].Key)
			{
				num3++;
			}
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x000F2820 File Offset: 0x000F0A20
		[Conditional("DEBUG_PROPERTYSTORE")]
		private void Debug_VerifyLocateObjectEntry(int index, short entryKey, int length)
		{
			int num = length - 1;
			int num2 = 0;
			int num3;
			do
			{
				num3 = (num + num2) / 2;
				short key = this.objEntries[num3].Key;
				if (key != entryKey)
				{
					if (entryKey < key)
					{
						num = num3 - 1;
					}
					else
					{
						num2 = num3 + 1;
					}
				}
			}
			while (num >= num2);
			if (entryKey > this.objEntries[num3].Key)
			{
				num3++;
			}
		}

		// Token: 0x04001F50 RID: 8016
		private static int currentKey;

		// Token: 0x04001F51 RID: 8017
		private PropertyStore.IntegerEntry[] intEntries;

		// Token: 0x04001F52 RID: 8018
		private PropertyStore.ObjectEntry[] objEntries;

		// Token: 0x020007D4 RID: 2004
		private struct IntegerEntry
		{
			// Token: 0x040042A7 RID: 17063
			public short Key;

			// Token: 0x040042A8 RID: 17064
			public short Mask;

			// Token: 0x040042A9 RID: 17065
			public int Value1;

			// Token: 0x040042AA RID: 17066
			public int Value2;

			// Token: 0x040042AB RID: 17067
			public int Value3;

			// Token: 0x040042AC RID: 17068
			public int Value4;
		}

		// Token: 0x020007D5 RID: 2005
		private struct ObjectEntry
		{
			// Token: 0x040042AD RID: 17069
			public short Key;

			// Token: 0x040042AE RID: 17070
			public short Mask;

			// Token: 0x040042AF RID: 17071
			public object Value1;

			// Token: 0x040042B0 RID: 17072
			public object Value2;

			// Token: 0x040042B1 RID: 17073
			public object Value3;

			// Token: 0x040042B2 RID: 17074
			public object Value4;
		}

		// Token: 0x020007D6 RID: 2006
		private sealed class ColorWrapper
		{
			// Token: 0x06006DAC RID: 28076 RVA: 0x00192C99 File Offset: 0x00190E99
			public ColorWrapper(Color color)
			{
				this.Color = color;
			}

			// Token: 0x040042B3 RID: 17075
			public Color Color;
		}

		// Token: 0x020007D7 RID: 2007
		private sealed class PaddingWrapper
		{
			// Token: 0x06006DAD RID: 28077 RVA: 0x00192CA8 File Offset: 0x00190EA8
			public PaddingWrapper(Padding padding)
			{
				this.Padding = padding;
			}

			// Token: 0x040042B4 RID: 17076
			public Padding Padding;
		}

		// Token: 0x020007D8 RID: 2008
		private sealed class RectangleWrapper
		{
			// Token: 0x06006DAE RID: 28078 RVA: 0x00192CB7 File Offset: 0x00190EB7
			public RectangleWrapper(Rectangle rectangle)
			{
				this.Rectangle = rectangle;
			}

			// Token: 0x040042B5 RID: 17077
			public Rectangle Rectangle;
		}

		// Token: 0x020007D9 RID: 2009
		private sealed class SizeWrapper
		{
			// Token: 0x06006DAF RID: 28079 RVA: 0x00192CC6 File Offset: 0x00190EC6
			public SizeWrapper(Size size)
			{
				this.Size = size;
			}

			// Token: 0x040042B6 RID: 17078
			public Size Size;
		}
	}
}
