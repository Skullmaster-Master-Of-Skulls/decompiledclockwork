using System;
using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x02000032 RID: 50
	public sealed class SolidBrush : Brush, ISystemColorTracker
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x00017258 File Offset: 0x00015458
		public SolidBrush(Color color)
		{
			this.color = color;
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCreateSolidFill(this.color.ToArgb(), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			base.SetNativeBrushInternal(zero);
			if (color.IsSystemColor)
			{
				SystemColorTracker.Add(this);
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000172B6 File Offset: 0x000154B6
		internal SolidBrush(Color color, bool immutable) : this(color)
		{
			this.immutable = immutable;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000172C6 File Offset: 0x000154C6
		internal SolidBrush(IntPtr nativeBrush)
		{
			base.SetNativeBrushInternal(nativeBrush);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000172E0 File Offset: 0x000154E0
		public override object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCloneBrush(new HandleRef(this, base.NativeBrush), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new SolidBrush(zero);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00017317 File Offset: 0x00015517
		protected override void Dispose(bool disposing)
		{
			if (!disposing)
			{
				this.immutable = false;
			}
			else if (this.immutable)
			{
				throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[]
				{
					"Brush"
				}));
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00017354 File Offset: 0x00015554
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x000173A8 File Offset: 0x000155A8
		public Color Color
		{
			get
			{
				if (this.color == Color.Empty)
				{
					int argb = 0;
					int num = SafeNativeMethods.Gdip.GdipGetSolidFillColor(new HandleRef(this, base.NativeBrush), out argb);
					if (num != 0)
					{
						throw SafeNativeMethods.Gdip.StatusException(num);
					}
					this.color = Color.FromArgb(argb);
				}
				return this.color;
			}
			set
			{
				if (this.immutable)
				{
					throw new ArgumentException(SR.GetString("CantChangeImmutableObjects", new object[]
					{
						"Brush"
					}));
				}
				if (this.color != value)
				{
					Color color = this.color;
					this.InternalSetColor(value);
					if (value.IsSystemColor && !color.IsSystemColor)
					{
						SystemColorTracker.Add(this);
					}
				}
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00017410 File Offset: 0x00015610
		private void InternalSetColor(Color value)
		{
			int num = SafeNativeMethods.Gdip.GdipSetSolidFillColor(new HandleRef(this, base.NativeBrush), value.ToArgb());
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.color = value;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00017447 File Offset: 0x00015647
		void ISystemColorTracker.OnSystemColorChanged()
		{
			if (base.NativeBrush != IntPtr.Zero)
			{
				this.InternalSetColor(this.color);
			}
		}

		// Token: 0x0400031A RID: 794
		private Color color = Color.Empty;

		// Token: 0x0400031B RID: 795
		private bool immutable;
	}
}
