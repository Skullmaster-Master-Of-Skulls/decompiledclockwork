using System;
using System.Collections;
using System.Drawing;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001745 RID: 5957
	internal class LayoutZone : CollectionBase
	{
		// Token: 0x170046BA RID: 18106
		// (get) Token: 0x0600E88A RID: 59530 RVA: 0x00342FB5 File Offset: 0x003411B5
		// (set) Token: 0x0600E88B RID: 59531 RVA: 0x00342FBD File Offset: 0x003411BD
		internal float X
		{
			get
			{
				return this.layoutZoneX;
			}
			set
			{
				this.layoutZoneX = value;
			}
		}

		// Token: 0x170046BB RID: 18107
		// (get) Token: 0x0600E88C RID: 59532 RVA: 0x00342FC6 File Offset: 0x003411C6
		// (set) Token: 0x0600E88D RID: 59533 RVA: 0x00342FCE File Offset: 0x003411CE
		internal float Y
		{
			get
			{
				return this.layoutZoneY;
			}
			set
			{
				this.layoutZoneY = value;
			}
		}

		// Token: 0x170046BC RID: 18108
		// (get) Token: 0x0600E88E RID: 59534 RVA: 0x00342FD7 File Offset: 0x003411D7
		// (set) Token: 0x0600E88F RID: 59535 RVA: 0x00342FDF File Offset: 0x003411DF
		internal float Width
		{
			get
			{
				return this.layoutZoneWidth;
			}
			set
			{
				this.layoutZoneWidth = value;
			}
		}

		// Token: 0x170046BD RID: 18109
		// (get) Token: 0x0600E890 RID: 59536 RVA: 0x00342FE8 File Offset: 0x003411E8
		// (set) Token: 0x0600E891 RID: 59537 RVA: 0x00342FF0 File Offset: 0x003411F0
		internal float Height
		{
			get
			{
				return this.layoutZoneHeight;
			}
			set
			{
				this.layoutZoneHeight = value;
			}
		}

		// Token: 0x170046BE RID: 18110
		// (get) Token: 0x0600E892 RID: 59538 RVA: 0x00342FF9 File Offset: 0x003411F9
		internal LayoutZoneType Type
		{
			get
			{
				return this.layoutZoneType;
			}
		}

		// Token: 0x170046BF RID: 18111
		internal IOrdering this[int index]
		{
			get
			{
				return (IOrdering)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x170046C0 RID: 18112
		// (get) Token: 0x0600E895 RID: 59541 RVA: 0x00343023 File Offset: 0x00341223
		// (set) Token: 0x0600E896 RID: 59542 RVA: 0x0034302B File Offset: 0x0034122B
		internal AlignedPositions AlignedPosition
		{
			get
			{
				return this.layoutZoneAlignedPosition;
			}
			set
			{
				if (value == AlignedPositions.Left || value == AlignedPositions.Right || value == AlignedPositions.Top || value == AlignedPositions.Bottom)
				{
					this.layoutZoneAlignedPosition = value;
					return;
				}
				this.layoutZoneAlignedPosition = AlignedPositions.None;
			}
		}

		// Token: 0x0600E897 RID: 59543 RVA: 0x00343052 File Offset: 0x00341252
		internal LayoutZone()
		{
			this.layoutZoneX = 0f;
			this.layoutZoneY = 0f;
			this.layoutZoneWidth = 0f;
			this.layoutZoneHeight = 0f;
		}

		// Token: 0x0600E898 RID: 59544 RVA: 0x00343086 File Offset: 0x00341286
		internal RectangleF ToRectangleF()
		{
			return new RectangleF(this.layoutZoneX, this.layoutZoneY, this.layoutZoneWidth, this.layoutZoneHeight);
		}

		// Token: 0x0600E899 RID: 59545 RVA: 0x003430A5 File Offset: 0x003412A5
		internal Position ToPosition()
		{
			return new Position(this.layoutZoneX, this.layoutZoneY);
		}

		// Token: 0x0600E89A RID: 59546 RVA: 0x003430B8 File Offset: 0x003412B8
		internal Dimensions ToDimensions()
		{
			return new Dimensions(this.layoutZoneWidth, this.layoutZoneHeight);
		}

		// Token: 0x0600E89B RID: 59547 RVA: 0x003430CC File Offset: 0x003412CC
		internal static LayoutZone FromStyle(Dimensions baseDimensions, object chartElement)
		{
			Position position = (Position)Style.GetStyleProperty(chartElement, StyleProperties.Position);
			Dimensions dimensions = (Dimensions)Style.GetStyleProperty(chartElement, StyleProperties.Dimensions);
			float? rotation = (float?)Style.GetStyleProperty(chartElement, StyleProperties.RotationAngle);
			RectangleF realBounds = Style.GetRealBounds(dimensions, rotation);
			if (position != null)
			{
				LayoutZone layoutZone = new LayoutZone();
				layoutZone.Add(chartElement);
				layoutZone.DefineType(position);
				layoutZone.AlignedPosition = position.AlignedPosition;
				layoutZone.SetMajorDimension(realBounds, dimensions.Margins);
				layoutZone.SetMinorDimension(baseDimensions);
				layoutZone.DefineBasePosition(position, baseDimensions);
				return layoutZone;
			}
			return null;
		}

		// Token: 0x0600E89C RID: 59548 RVA: 0x00343154 File Offset: 0x00341354
		internal static LayoutZone CreateFromAvailableSpace(DimensionsChart dimensionsChart, object chartElement, LayoutZone[] layoutZones)
		{
			LayoutZone layoutZone = new LayoutZone();
			layoutZone.Width = dimensionsChart.Width.PixelValue;
			layoutZone.Height = dimensionsChart.Height.PixelValue;
			layoutZone.Add(chartElement);
			int num = 0;
			foreach (LayoutZone layoutZone2 in layoutZones)
			{
				if (!LayoutZone.IsUsedBefore(layoutZones, layoutZone2, num))
				{
					AlignedPositions alignedPosition = layoutZone2.AlignedPosition;
					if (alignedPosition <= AlignedPositions.Left)
					{
						if (alignedPosition != AlignedPositions.Top)
						{
							if (alignedPosition == AlignedPositions.Left)
							{
								layoutZone.Width -= layoutZone2.Width;
								layoutZone.X += layoutZone2.Width;
							}
						}
						else
						{
							layoutZone.Height -= layoutZone2.Height;
							layoutZone.Y += layoutZone2.Height;
						}
					}
					else if (alignedPosition != AlignedPositions.Right)
					{
						if (alignedPosition == AlignedPositions.Bottom)
						{
							layoutZone.Height -= layoutZone2.Height;
						}
					}
					else
					{
						layoutZone.Width -= layoutZone2.Width;
					}
				}
				num++;
			}
			return layoutZone;
		}

		// Token: 0x0600E89D RID: 59549 RVA: 0x0034326C File Offset: 0x0034146C
		internal static void DistributeZones(ref LayoutZone titleZone, ref LayoutZone legendZone, ref LayoutZone dataTableZone)
		{
			if (titleZone.AlignedPosition == legendZone.AlignedPosition)
			{
				titleZone.FixLayoutZone(ref legendZone);
			}
			if (titleZone.AlignedPosition == dataTableZone.AlignedPosition)
			{
				titleZone.FixLayoutZone(ref dataTableZone);
			}
			if (legendZone.AlignedPosition == dataTableZone.AlignedPosition)
			{
				legendZone.FixLayoutZone(ref dataTableZone);
			}
			if (titleZone == dataTableZone && titleZone != legendZone)
			{
				titleZone.FixLayoutZone(ref legendZone);
			}
			else if (titleZone == legendZone && titleZone != dataTableZone)
			{
				titleZone.FixLayoutZone(ref dataTableZone);
			}
			else if (legendZone == dataTableZone && legendZone != titleZone)
			{
				legendZone.FixLayoutZone(ref titleZone);
			}
			else
			{
				titleZone.FixLayoutZone(ref legendZone);
				titleZone.FixLayoutZone(ref dataTableZone);
				dataTableZone.FixLayoutZone(ref legendZone);
			}
			titleZone.DistributeElements();
			legendZone.DistributeElements();
			dataTableZone.DistributeElements();
		}

		// Token: 0x0600E89E RID: 59550 RVA: 0x00343330 File Offset: 0x00341530
		internal void FixElementPosition(Position position)
		{
			position.X += this.X;
			position.Y += this.Y;
		}

		// Token: 0x0600E89F RID: 59551 RVA: 0x00343358 File Offset: 0x00341558
		internal void CalculatePosition(object element, Dimensions dimensions, Position position)
		{
			if (element == null)
			{
				return;
			}
			position.ResetGlobal();
			if (!position.Auto)
			{
				return;
			}
			float offsetY = this.GetOffsetY(element);
			RectangleF realBounds = Style.GetRealBounds(dimensions, (float?)Style.GetStyleProperty(element, StyleProperties.RotationAngle));
			position.X = this.X;
			position.Y = this.Y;
			AlignedPositions alignedPosition = position.AlignedPosition;
			if (alignedPosition <= AlignedPositions.Left)
			{
				switch (alignedPosition)
				{
				case AlignedPositions.None:
					position.X += dimensions.Margins.Left.PixelValue;
					position.Y += offsetY;
					return;
				case AlignedPositions.TopLeft:
					break;
				case AlignedPositions.Top:
					position.X += Math.Max(dimensions.Margins.Left.PixelValue, (this.Width - dimensions.Width.PixelValue) / 2f);
					position.Y += offsetY;
					break;
				default:
					if (alignedPosition != AlignedPositions.Left)
					{
						return;
					}
					position.X += dimensions.Margins.Left.PixelValue;
					position.Y += (this.Height - this.GetRealHeight()) / 2f + offsetY + (realBounds.Height - dimensions.Height.PixelValue) / 2f;
					return;
				}
				return;
			}
			if (alignedPosition == AlignedPositions.Right)
			{
				position.X += dimensions.Margins.Left.PixelValue + (realBounds.Width - dimensions.Width.PixelValue);
				position.Y += (this.Height - this.GetRealHeight()) / 2f + offsetY + (realBounds.Height - dimensions.Height.PixelValue) / 2f;
				return;
			}
			if (alignedPosition != AlignedPositions.Bottom)
			{
				return;
			}
			position.X += Math.Max(dimensions.Margins.Left.PixelValue, (this.Width - dimensions.Width.PixelValue) / 2f);
			position.Y += offsetY + (realBounds.Height - dimensions.Height.PixelValue);
		}

		// Token: 0x0600E8A0 RID: 59552 RVA: 0x00343580 File Offset: 0x00341780
		private void DistributeElements()
		{
			object dataTable = this.GetDataTable();
			object title = this.GetTitle();
			object legend = this.GetLegend();
			base.Clear();
			if (this.AlignedPosition == AlignedPositions.Bottom)
			{
				if (dataTable != null)
				{
					this.Add(dataTable);
				}
				if (title != null)
				{
					this.Add(title);
				}
				if (legend != null)
				{
					this.Add(legend);
					return;
				}
			}
			else
			{
				if (title != null)
				{
					this.Add(title);
				}
				if (legend != null)
				{
					this.Add(legend);
				}
				if (dataTable != null)
				{
					this.Add(dataTable);
				}
			}
		}

		// Token: 0x0600E8A1 RID: 59553 RVA: 0x003435F4 File Offset: 0x003417F4
		private object GetDataTable()
		{
			foreach (object obj in this)
			{
				if (obj is ChartDataTable)
				{
					return obj;
				}
			}
			return null;
		}

		// Token: 0x0600E8A2 RID: 59554 RVA: 0x0034364C File Offset: 0x0034184C
		private object GetTitle()
		{
			foreach (object obj in this)
			{
				if (obj is ChartTitle)
				{
					return obj;
				}
			}
			return null;
		}

		// Token: 0x0600E8A3 RID: 59555 RVA: 0x003436A4 File Offset: 0x003418A4
		private object GetLegend()
		{
			foreach (object obj in this)
			{
				if (obj is ChartLegend)
				{
					return obj;
				}
			}
			return null;
		}

		// Token: 0x0600E8A4 RID: 59556 RVA: 0x003436FC File Offset: 0x003418FC
		private void RemoveEquals(LayoutZone[] layoutZones)
		{
			for (int i = 0; i < layoutZones.Length - 1; i++)
			{
				for (int j = i + 1; j < layoutZones.Length; j++)
				{
					if (layoutZones[i] == layoutZones[j])
					{
						layoutZones[j] = null;
					}
				}
			}
		}

		// Token: 0x0600E8A5 RID: 59557 RVA: 0x00343738 File Offset: 0x00341938
		private void FixLayoutZone(ref LayoutZone zone)
		{
			if (this == zone)
			{
				return;
			}
			switch (this.Type)
			{
			case LayoutZoneType.Vertical:
				if (zone.Type == LayoutZoneType.Horizontal)
				{
					this.FixXAndWidth(this, zone);
					return;
				}
				if (this.AlignedPosition == zone.AlignedPosition)
				{
					this.Width = Math.Max(this.Width, zone.Width);
					this.X = Math.Min(this.X, zone.X);
					if (zone.Count > 0)
					{
						this.Add(zone[0]);
					}
					zone = this;
					return;
				}
				break;
			case LayoutZoneType.Horizontal:
				if (zone.Type == LayoutZoneType.Vertical)
				{
					this.FixXAndWidth(zone, this);
					return;
				}
				if (this.AlignedPosition == zone.AlignedPosition)
				{
					this.Height += zone.Height;
					if (this.AlignedPosition == AlignedPositions.Bottom)
					{
						this.Y -= zone.Height;
					}
					if (zone.Count > 0)
					{
						this.Add(zone[0]);
					}
					zone = this;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600E8A6 RID: 59558 RVA: 0x00343848 File Offset: 0x00341A48
		private static bool IsUsedBefore(LayoutZone[] layoutZones, LayoutZone zone, int index)
		{
			if (index < layoutZones.Length)
			{
				for (int i = 0; i < index; i++)
				{
					if (layoutZones[i] == zone)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600E8A7 RID: 59559 RVA: 0x00343870 File Offset: 0x00341A70
		private float GetOffsetY(object element)
		{
			float num = 0f;
			Dimensions dimensions = null;
			int num2 = base.List.IndexOf(element);
			for (int i = 0; i < num2 + 1; i++)
			{
				Dimensions dimensions2 = (i == 0) ? ((Dimensions)Style.GetStyleProperty(this[i], StyleProperties.Dimensions)) : dimensions;
				if (i == 0)
				{
					num += dimensions2.Margins.Top.PixelValue;
				}
				if (i == num2)
				{
					break;
				}
				dimensions = (Dimensions)Style.GetStyleProperty(this[i + 1], StyleProperties.Dimensions);
				float? rotation = (float?)Style.GetStyleProperty(this[i], StyleProperties.RotationAngle);
				num += Style.GetRealBounds(dimensions2, rotation).Height + Math.Max(dimensions2.Margins.Bottom.PixelValue, dimensions.Margins.Top.PixelValue);
			}
			return num;
		}

		// Token: 0x0600E8A8 RID: 59560 RVA: 0x0034394C File Offset: 0x00341B4C
		private float GetRealHeight()
		{
			float? rotation = (float?)Style.GetStyleProperty(this[0], StyleProperties.RotationAngle);
			Dimensions dimensions = (Dimensions)Style.GetStyleProperty(this[0], StyleProperties.Dimensions);
			RectangleF realBounds = Style.GetRealBounds(dimensions, rotation);
			Dimensions dimensions2 = new Dimensions(new ChartMargins(0f), new ChartPaddings(0f));
			dimensions2.Width.Value = 0f;
			dimensions2.Height.Value = 0f;
			float num = realBounds.Height;
			if (base.Count > 1)
			{
				for (int i = 1; i < base.Count; i++)
				{
					dimensions2 = (Dimensions)Style.GetStyleProperty(this[i], StyleProperties.Dimensions);
					float? rotation2 = (float?)Style.GetStyleProperty(this[i], StyleProperties.RotationAngle);
					num += Style.GetRealBounds(dimensions2, rotation2).Height + Math.Max(dimensions.Margins.Bottom.PixelValue, dimensions2.Margins.Top.PixelValue);
					dimensions = dimensions2;
				}
			}
			return num;
		}

		// Token: 0x0600E8A9 RID: 59561 RVA: 0x00343A5D File Offset: 0x00341C5D
		private void FixXAndWidth(LayoutZone zone1, LayoutZone zone2)
		{
			zone2.Width -= zone1.Width;
			if (zone1.X == zone2.X)
			{
				zone2.X = zone1.Width;
			}
		}

		// Token: 0x0600E8AA RID: 59562 RVA: 0x00343A8C File Offset: 0x00341C8C
		private void DefineType(Position position)
		{
			AlignedPositions alignedPosition = position.AlignedPosition;
			if (alignedPosition <= AlignedPositions.Left)
			{
				if (alignedPosition != AlignedPositions.Top)
				{
					if (alignedPosition != AlignedPositions.Left)
					{
						goto IL_2C;
					}
					goto IL_2C;
				}
			}
			else if (alignedPosition == AlignedPositions.Right || alignedPosition != AlignedPositions.Bottom)
			{
				goto IL_2C;
			}
			this.layoutZoneType = LayoutZoneType.Horizontal;
			return;
			IL_2C:
			this.layoutZoneType = LayoutZoneType.Vertical;
		}

		// Token: 0x0600E8AB RID: 59563 RVA: 0x00343ACC File Offset: 0x00341CCC
		private void SetMajorDimension(RectangleF rect, ChartMargins margins)
		{
			switch (this.Type)
			{
			case LayoutZoneType.Vertical:
				this.Width = rect.Width + margins.Left.PixelValue + margins.Right.PixelValue;
				return;
			case LayoutZoneType.Horizontal:
				this.Height = rect.Height + margins.Top.PixelValue + margins.Bottom.PixelValue;
				return;
			default:
				return;
			}
		}

		// Token: 0x0600E8AC RID: 59564 RVA: 0x00343B3C File Offset: 0x00341D3C
		private void SetMinorDimension(Dimensions dimensions)
		{
			switch (this.Type)
			{
			case LayoutZoneType.Vertical:
				this.Height = dimensions.Height.PixelValue;
				return;
			case LayoutZoneType.Horizontal:
				this.Width = dimensions.Width.PixelValue;
				return;
			default:
				return;
			}
		}

		// Token: 0x0600E8AD RID: 59565 RVA: 0x00343B84 File Offset: 0x00341D84
		private void DefineBasePosition(Position position, Dimensions baseDimensions)
		{
			AlignedPositions alignedPosition = position.AlignedPosition;
			if (alignedPosition == AlignedPositions.Right)
			{
				this.X = baseDimensions.Width.PixelValue - this.Width;
				return;
			}
			if (alignedPosition != AlignedPositions.Bottom)
			{
				return;
			}
			this.Y = baseDimensions.Height.PixelValue - this.Height;
		}

		// Token: 0x0600E8AE RID: 59566 RVA: 0x00343BD7 File Offset: 0x00341DD7
		private void Add(object element)
		{
			if (!base.List.Contains(element))
			{
				base.List.Add(element);
			}
		}

		// Token: 0x040042AB RID: 17067
		private float layoutZoneX;

		// Token: 0x040042AC RID: 17068
		private float layoutZoneY;

		// Token: 0x040042AD RID: 17069
		private float layoutZoneWidth;

		// Token: 0x040042AE RID: 17070
		private float layoutZoneHeight;

		// Token: 0x040042AF RID: 17071
		private LayoutZoneType layoutZoneType;

		// Token: 0x040042B0 RID: 17072
		private AlignedPositions layoutZoneAlignedPosition;
	}
}
