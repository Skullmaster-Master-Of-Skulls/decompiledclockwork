using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Telerik.Charting
{
	// Token: 0x02001765 RID: 5989
	public class ColorBlend : ChartingStateManagedCollection<GradientElement>, ICloneable
	{
		// Token: 0x0600E9A6 RID: 59814 RVA: 0x00351FA2 File Offset: 0x003501A2
		public ColorBlend()
		{
		}

		// Token: 0x0600E9A7 RID: 59815 RVA: 0x00351FAC File Offset: 0x003501AC
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ColorBlend(Color[] colors)
		{
			float num = 0f;
			float num2 = 1f / (float)(colors.Length - 1);
			for (int i = 0; i < colors.Length; i++)
			{
				this.Add(new GradientElement(colors[i], num));
				if (i < colors.Length - 2)
				{
					num += num2;
				}
				else
				{
					num = 1f;
				}
			}
		}

		// Token: 0x0600E9A8 RID: 59816 RVA: 0x0035200C File Offset: 0x0035020C
		public ColorBlend(Color[] colors, object containerObject) : this(colors)
		{
			this.colorBlendContainerObject = containerObject;
		}

		// Token: 0x0600E9A9 RID: 59817 RVA: 0x0035201C File Offset: 0x0035021C
		public ColorBlend(Color[] colors, float[] positions, object containerObject) : this(colors, positions)
		{
			this.colorBlendContainerObject = containerObject;
		}

		// Token: 0x0600E9AA RID: 59818 RVA: 0x00352030 File Offset: 0x00350230
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ColorBlend(Color[] colors, float[] positions)
		{
			for (int i = 0; i < colors.Length; i++)
			{
				this.Add(new GradientElement(colors[i], positions[i]));
			}
		}

		// Token: 0x0600E9AB RID: 59819 RVA: 0x0035206B File Offset: 0x0035026B
		public ColorBlend(object containerObject)
		{
			this.colorBlendContainerObject = containerObject;
		}

		// Token: 0x0600E9AC RID: 59820 RVA: 0x0035207C File Offset: 0x0035027C
		[Description("Adds a range of elements to the collection.")]
		public void AddRange(ColorBlend blend)
		{
			foreach (GradientElement item in blend)
			{
				base.Add(item);
			}
		}

		// Token: 0x0600E9AD RID: 59821 RVA: 0x003520C4 File Offset: 0x003502C4
		public void LoadFrom(ColorBlend blend)
		{
			if (blend.Count > 0)
			{
				base.Clear();
				this.AddRange(blend);
			}
		}

		// Token: 0x0600E9AE RID: 59822 RVA: 0x003520DC File Offset: 0x003502DC
		public Color[] GetColors()
		{
			int count = base.Count;
			Color[] array = new Color[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = this[i].Color;
			}
			return array;
		}

		// Token: 0x0600E9AF RID: 59823 RVA: 0x0035211C File Offset: 0x0035031C
		public float[] GetPositions()
		{
			int count = base.Count;
			float[] array = new float[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = this[i].Position;
			}
			return array;
		}

		// Token: 0x0600E9B0 RID: 59824 RVA: 0x00352154 File Offset: 0x00350354
		public Color GetColor(float pos)
		{
			if (base.Count < 2)
			{
				throw new ArgumentException("At least two elements must be defined in the ColorBlend");
			}
			if (this[0].Position != 0f)
			{
				throw new ArgumentException("First position value must be 0.0f");
			}
			if (this[base.Count - 1].Position != 1f)
			{
				throw new ArgumentException("Last position value must be 1.0f");
			}
			if (pos > 1f || pos < 0f)
			{
				pos -= (float)Math.Floor((double)pos);
			}
			int num = 1;
			while (num < base.Count && this[num].Position < pos)
			{
				num++;
			}
			float num2 = (pos - this[num - 1].Position) / (this[num].Position - this[num - 1].Position);
			int red = (int)Math.Round((double)((float)this[num - 1].Color.R * (1f - num2) + (float)this[num].Color.R * num2));
			int green = (int)Math.Round((double)((float)this[num - 1].Color.G * (1f - num2) + (float)this[num].Color.G * num2));
			int blue = (int)Math.Round((double)((float)this[num - 1].Color.B * (1f - num2) + (float)this[num].Color.B * num2));
			int alpha = (int)Math.Round((double)((float)this[num - 1].Color.A * (1f - num2) + (float)this[num].Color.A * num2));
			return Color.FromArgb(alpha, red, green, blue);
		}

		// Token: 0x0600E9B1 RID: 59825 RVA: 0x00352334 File Offset: 0x00350534
		public LinearGradientBrush GetBrush(RectangleF rectangle, float angle)
		{
			return new LinearGradientBrush(rectangle, Color.Black, Color.Black, angle, true)
			{
				InterpolationColors = new ColorBlend
				{
					Colors = this.GetColors(),
					Positions = this.GetPositions()
				}
			};
		}

		// Token: 0x0600E9B2 RID: 59826 RVA: 0x0035237C File Offset: 0x0035057C
		public override bool Equals(object obj)
		{
			ColorBlend colorBlend = obj as ColorBlend;
			if (colorBlend != null)
			{
				return ColorBlend.Compare(colorBlend, this);
			}
			return base.Equals(obj);
		}

		// Token: 0x0600E9B3 RID: 59827 RVA: 0x003523A2 File Offset: 0x003505A2
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600E9B4 RID: 59828 RVA: 0x003523AC File Offset: 0x003505AC
		private static bool Compare(ColorBlend a, ColorBlend b)
		{
			if (a == null && b == null)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			int count = a.Count;
			if (count != b.Count)
			{
				return false;
			}
			for (int i = 0; i < count; i++)
			{
				try
				{
					if (!a[i].Equals(b[i]))
					{
						return false;
					}
				}
				catch
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600E9B5 RID: 59829 RVA: 0x0035241C File Offset: 0x0035061C
		public object Clone()
		{
			ColorBlend colorBlend = new ColorBlend();
			foreach (GradientElement gradientElement in this)
			{
				GradientElement item = (GradientElement)gradientElement.Clone();
				colorBlend.Add(item);
			}
			return colorBlend;
		}

		// Token: 0x0400433C RID: 17212
		internal object colorBlendContainerObject;
	}
}
