using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001764 RID: 5988
	public class GradientElement : StateManagedObject, ICloneable
	{
		// Token: 0x170046E7 RID: 18151
		// (get) Token: 0x0600E99B RID: 59803 RVA: 0x00351DED File Offset: 0x0034FFED
		// (set) Token: 0x0600E99C RID: 59804 RVA: 0x00351E12 File Offset: 0x00350012
		[TypeConverter(typeof(ColorConverter))]
		[DefaultValue(typeof(Color), "")]
		[SkinnableProperty]
		public Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? DefaultValues.DEFAULT_STYLE_COLOR);
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x170046E8 RID: 18152
		// (get) Token: 0x0600E99D RID: 59805 RVA: 0x00351E2A File Offset: 0x0035002A
		// (set) Token: 0x0600E99E RID: 59806 RVA: 0x00351E4F File Offset: 0x0035004F
		[DefaultValue(0f)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[SkinnableProperty]
		public float Position
		{
			get
			{
				return (float)(base.ViewState["Position"] ?? 0f);
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x0600E99F RID: 59807 RVA: 0x00351E67 File Offset: 0x00350067
		public GradientElement()
		{
		}

		// Token: 0x0600E9A0 RID: 59808 RVA: 0x00351E6F File Offset: 0x0035006F
		public GradientElement(Color color, float position) : this()
		{
			this.Color = color;
			this.Position = position;
		}

		// Token: 0x0600E9A1 RID: 59809 RVA: 0x00351E85 File Offset: 0x00350085
		internal void Reset()
		{
			this.Color = DefaultValues.DEFAULT_STYLE_COLOR;
			this.Position = 0f;
		}

		// Token: 0x0600E9A2 RID: 59810 RVA: 0x00351EA0 File Offset: 0x003500A0
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			GradientElement gradientElement = obj as GradientElement;
			if (gradientElement != null)
			{
				return gradientElement.Color.Equals(this.Color) && gradientElement.Position.Equals(this.Position);
			}
			return base.Equals(obj);
		}

		// Token: 0x0600E9A3 RID: 59811 RVA: 0x00351EFC File Offset: 0x003500FC
		public override int GetHashCode()
		{
			return this.Color.GetHashCode() ^ this.Position.GetHashCode();
		}

		// Token: 0x0600E9A4 RID: 59812 RVA: 0x00351F2C File Offset: 0x0035012C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Color: ",
				new ColorConverter().ConvertToInvariantString(this.Color),
				"; Position: ",
				this.Position
			});
		}

		// Token: 0x0600E9A5 RID: 59813 RVA: 0x00351F7C File Offset: 0x0035017C
		public object Clone()
		{
			GradientElement gradientElement = (GradientElement)base.MemberwiseClone();
			gradientElement.ViewState = base.CloneState();
			return gradientElement;
		}
	}
}
