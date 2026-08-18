using System;
using System.ComponentModel;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x02000047 RID: 71
	public sealed class StringFormat : MarshalByRefObject, ICloneable, IDisposable
	{
		// Token: 0x060006B9 RID: 1721 RVA: 0x0001B781 File Offset: 0x00019981
		private StringFormat(IntPtr format)
		{
			this.nativeFormat = format;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001B790 File Offset: 0x00019990
		public StringFormat() : this((StringFormatFlags)0, 0)
		{
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001B79A File Offset: 0x0001999A
		public StringFormat(StringFormatFlags options) : this(options, 0)
		{
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001B7A4 File Offset: 0x000199A4
		public StringFormat(StringFormatFlags options, int language)
		{
			int num = SafeNativeMethods.Gdip.GdipCreateStringFormat(options, language, out this.nativeFormat);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001B7D0 File Offset: 0x000199D0
		public StringFormat(StringFormat format)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = SafeNativeMethods.Gdip.GdipCloneStringFormat(new HandleRef(format, format.nativeFormat), out this.nativeFormat);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001B813 File Offset: 0x00019A13
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001B824 File Offset: 0x00019A24
		private void Dispose(bool disposing)
		{
			if (this.nativeFormat != IntPtr.Zero)
			{
				try
				{
					SafeNativeMethods.Gdip.GdipDeleteStringFormat(new HandleRef(this, this.nativeFormat));
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
				}
				finally
				{
					this.nativeFormat = IntPtr.Zero;
				}
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001B88C File Offset: 0x00019A8C
		public object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			int num = SafeNativeMethods.Gdip.GdipCloneStringFormat(new HandleRef(this, this.nativeFormat), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new StringFormat(zero);
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001B8C8 File Offset: 0x00019AC8
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0001B8F4 File Offset: 0x00019AF4
		public StringFormatFlags FormatFlags
		{
			get
			{
				StringFormatFlags result;
				int num = SafeNativeMethods.Gdip.GdipGetStringFormatFlags(new HandleRef(this, this.nativeFormat), out result);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return result;
			}
			set
			{
				int num = SafeNativeMethods.Gdip.GdipSetStringFormatFlags(new HandleRef(this, this.nativeFormat), value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001B920 File Offset: 0x00019B20
		public void SetMeasurableCharacterRanges(CharacterRange[] ranges)
		{
			int num = SafeNativeMethods.Gdip.GdipSetStringFormatMeasurableCharacterRanges(new HandleRef(this, this.nativeFormat), ranges.Length, ranges);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001B950 File Offset: 0x00019B50
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x0001B980 File Offset: 0x00019B80
		public StringAlignment Alignment
		{
			get
			{
				StringAlignment result = StringAlignment.Near;
				int num = SafeNativeMethods.Gdip.GdipGetStringFormatAlign(new HandleRef(this, this.nativeFormat), out result);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return result;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(StringAlignment));
				}
				int num = SafeNativeMethods.Gdip.GdipSetStringFormatAlign(new HandleRef(this, this.nativeFormat), value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001B9D0 File Offset: 0x00019BD0
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x0001BA00 File Offset: 0x00019C00
		public StringAlignment LineAlignment
		{
			get
			{
				StringAlignment result = StringAlignment.Near;
				int num = SafeNativeMethods.Gdip.GdipGetStringFormatLineAlign(new HandleRef(this, this.nativeFormat), out result);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return result;
			}
			set
			{
				if (value < StringAlignment.Near || value > StringAlignment.Far)
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(StringAlignment));
				}
				int num = SafeNativeMethods.Gdip.GdipSetStringFormatLineAlign(new HandleRef(this, this.nativeFormat), value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001BA48 File Offset: 0x00019C48
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x0001BA74 File Offset: 0x00019C74
		public HotkeyPrefix HotkeyPrefix
		{
			get
			{
				HotkeyPrefix result;
				int num = SafeNativeMethods.Gdip.GdipGetStringFormatHotkeyPrefix(new HandleRef(this, this.nativeFormat), out result);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return result;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(HotkeyPrefix));
				}
				int num = SafeNativeMethods.Gdip.GdipSetStringFormatHotkeyPrefix(new HandleRef(this, this.nativeFormat), value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001BAC4 File Offset: 0x00019CC4
		public void SetTabStops(float firstTabOffset, float[] tabStops)
		{
			if (firstTabOffset < 0f)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"firstTabOffset",
					firstTabOffset
				}));
			}
			int num = SafeNativeMethods.Gdip.GdipSetStringFormatTabStops(new HandleRef(this, this.nativeFormat), firstTabOffset, tabStops.Length, tabStops);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001BB24 File Offset: 0x00019D24
		public float[] GetTabStops(out float firstTabOffset)
		{
			int num = 0;
			int num2 = SafeNativeMethods.Gdip.GdipGetStringFormatTabStopCount(new HandleRef(this, this.nativeFormat), out num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			float[] array = new float[num];
			num2 = SafeNativeMethods.Gdip.GdipGetStringFormatTabStops(new HandleRef(this, this.nativeFormat), num, out firstTabOffset, array);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
			return array;
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001BB78 File Offset: 0x00019D78
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x0001BBA4 File Offset: 0x00019DA4
		public StringTrimming Trimming
		{
			get
			{
				StringTrimming result;
				int num = SafeNativeMethods.Gdip.GdipGetStringFormatTrimming(new HandleRef(this, this.nativeFormat), out result);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return result;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 5))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(StringTrimming));
				}
				int num = SafeNativeMethods.Gdip.GdipSetStringFormatTrimming(new HandleRef(this, this.nativeFormat), value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0001BBF4 File Offset: 0x00019DF4
		public static StringFormat GenericDefault
		{
			get
			{
				IntPtr format;
				int num = SafeNativeMethods.Gdip.GdipStringFormatGetGenericDefault(out format);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return new StringFormat(format);
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0001BC1C File Offset: 0x00019E1C
		public static StringFormat GenericTypographic
		{
			get
			{
				IntPtr format;
				int num = SafeNativeMethods.Gdip.GdipStringFormatGetGenericTypographic(out format);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return new StringFormat(format);
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001BC44 File Offset: 0x00019E44
		public void SetDigitSubstitution(int language, StringDigitSubstitute substitute)
		{
			int num = SafeNativeMethods.Gdip.GdipSetStringFormatDigitSubstitution(new HandleRef(this, this.nativeFormat), language, substitute);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001BC70 File Offset: 0x00019E70
		public StringDigitSubstitute DigitSubstitutionMethod
		{
			get
			{
				int num = 0;
				StringDigitSubstitute result;
				int num2 = SafeNativeMethods.Gdip.GdipGetStringFormatDigitSubstitution(new HandleRef(this, this.nativeFormat), out num, out result);
				if (num2 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num2);
				}
				return result;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001BCA0 File Offset: 0x00019EA0
		public int DigitSubstitutionLanguage
		{
			get
			{
				int result = 0;
				StringDigitSubstitute stringDigitSubstitute;
				int num = SafeNativeMethods.Gdip.GdipGetStringFormatDigitSubstitution(new HandleRef(this, this.nativeFormat), out result, out stringDigitSubstitute);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return result;
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001BCD0 File Offset: 0x00019ED0
		~StringFormat()
		{
			this.Dispose(false);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001BD00 File Offset: 0x00019F00
		public override string ToString()
		{
			return "[StringFormat, FormatFlags=" + this.FormatFlags.ToString() + "]";
		}

		// Token: 0x04000584 RID: 1412
		internal IntPtr nativeFormat;
	}
}
