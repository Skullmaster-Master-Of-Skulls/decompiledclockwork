using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200016B RID: 363
	public static class ImeContext
	{
		// Token: 0x060012F9 RID: 4857 RVA: 0x0003CAF0 File Offset: 0x0003ACF0
		public static void Disable(IntPtr handle)
		{
			if (ImeModeConversion.InputLanguageTable != ImeModeConversion.UnsupportedTable)
			{
				if (ImeContext.IsOpen(handle))
				{
					ImeContext.SetOpenStatus(false, handle);
				}
				IntPtr value = UnsafeNativeMethods.ImmAssociateContext(new HandleRef(null, handle), NativeMethods.NullHandleRef);
				if (value != IntPtr.Zero)
				{
					ImeContext.originalImeContext = value;
				}
			}
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0003CB40 File Offset: 0x0003AD40
		public static void Enable(IntPtr handle)
		{
			if (ImeModeConversion.InputLanguageTable != ImeModeConversion.UnsupportedTable)
			{
				IntPtr intPtr = UnsafeNativeMethods.ImmGetContext(new HandleRef(null, handle));
				if (intPtr == IntPtr.Zero)
				{
					if (ImeContext.originalImeContext == IntPtr.Zero)
					{
						intPtr = UnsafeNativeMethods.ImmCreateContext();
						if (intPtr != IntPtr.Zero)
						{
							UnsafeNativeMethods.ImmAssociateContext(new HandleRef(null, handle), new HandleRef(null, intPtr));
						}
					}
					else
					{
						UnsafeNativeMethods.ImmAssociateContext(new HandleRef(null, handle), new HandleRef(null, ImeContext.originalImeContext));
					}
				}
				else
				{
					UnsafeNativeMethods.ImmReleaseContext(new HandleRef(null, handle), new HandleRef(null, intPtr));
				}
				if (!ImeContext.IsOpen(handle))
				{
					ImeContext.SetOpenStatus(true, handle);
				}
			}
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0003CBF0 File Offset: 0x0003ADF0
		public static ImeMode GetImeMode(IntPtr handle)
		{
			IntPtr intPtr = IntPtr.Zero;
			ImeMode[] inputLanguageTable = ImeModeConversion.InputLanguageTable;
			ImeMode result;
			if (inputLanguageTable == ImeModeConversion.UnsupportedTable)
			{
				result = ImeMode.Inherit;
			}
			else
			{
				intPtr = UnsafeNativeMethods.ImmGetContext(new HandleRef(null, handle));
				if (intPtr == IntPtr.Zero)
				{
					result = ImeMode.Disable;
				}
				else if (!ImeContext.IsOpen(handle))
				{
					result = inputLanguageTable[3];
				}
				else
				{
					int num = 0;
					int num2 = 0;
					UnsafeNativeMethods.ImmGetConversionStatus(new HandleRef(null, intPtr), ref num, ref num2);
					if ((num & 1) != 0)
					{
						if ((num & 2) != 0)
						{
							result = (((num & 8) != 0) ? inputLanguageTable[6] : inputLanguageTable[7]);
						}
						else
						{
							result = (((num & 8) != 0) ? inputLanguageTable[4] : inputLanguageTable[5]);
						}
					}
					else
					{
						result = (((num & 8) != 0) ? inputLanguageTable[8] : inputLanguageTable[9]);
					}
				}
			}
			if (intPtr != IntPtr.Zero)
			{
				UnsafeNativeMethods.ImmReleaseContext(new HandleRef(null, handle), new HandleRef(null, intPtr));
			}
			return result;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x000072B6 File Offset: 0x000054B6
		[Conditional("DEBUG")]
		internal static void TraceImeStatus(Control ctl)
		{
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x000072B6 File Offset: 0x000054B6
		[Conditional("DEBUG")]
		private static void TraceImeStatus(IntPtr handle)
		{
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0003CCB4 File Offset: 0x0003AEB4
		public static bool IsOpen(IntPtr handle)
		{
			IntPtr intPtr = UnsafeNativeMethods.ImmGetContext(new HandleRef(null, handle));
			bool result = false;
			if (intPtr != IntPtr.Zero)
			{
				result = UnsafeNativeMethods.ImmGetOpenStatus(new HandleRef(null, intPtr));
				UnsafeNativeMethods.ImmReleaseContext(new HandleRef(null, handle), new HandleRef(null, intPtr));
			}
			return result;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0003CD00 File Offset: 0x0003AF00
		public static void SetImeStatus(ImeMode imeMode, IntPtr handle)
		{
			if (imeMode != ImeMode.Inherit && imeMode != ImeMode.NoControl)
			{
				ImeMode[] inputLanguageTable = ImeModeConversion.InputLanguageTable;
				if (inputLanguageTable != ImeModeConversion.UnsupportedTable)
				{
					int num = 0;
					int sentence = 0;
					if (imeMode == ImeMode.Disable)
					{
						ImeContext.Disable(handle);
					}
					else
					{
						ImeContext.Enable(handle);
					}
					switch (imeMode)
					{
					case ImeMode.NoControl:
					case ImeMode.Disable:
						return;
					case ImeMode.On:
						imeMode = ImeMode.Hiragana;
						goto IL_78;
					case ImeMode.Off:
						if (inputLanguageTable != ImeModeConversion.JapaneseTable)
						{
							imeMode = ImeMode.Alpha;
							goto IL_78;
						}
						break;
					default:
						if (imeMode != ImeMode.Close)
						{
							goto IL_78;
						}
						break;
					}
					if (inputLanguageTable != ImeModeConversion.KoreanTable)
					{
						ImeContext.SetOpenStatus(false, handle);
						return;
					}
					imeMode = ImeMode.Alpha;
					IL_78:
					if (ImeModeConversion.ImeModeConversionBits.ContainsKey(imeMode))
					{
						ImeModeConversion imeModeConversion = ImeModeConversion.ImeModeConversionBits[imeMode];
						IntPtr handle2 = UnsafeNativeMethods.ImmGetContext(new HandleRef(null, handle));
						UnsafeNativeMethods.ImmGetConversionStatus(new HandleRef(null, handle2), ref num, ref sentence);
						num |= imeModeConversion.setBits;
						num &= ~imeModeConversion.clearBits;
						bool flag = UnsafeNativeMethods.ImmSetConversionStatus(new HandleRef(null, handle2), num, sentence);
						UnsafeNativeMethods.ImmReleaseContext(new HandleRef(null, handle), new HandleRef(null, handle2));
					}
				}
			}
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0003CDF8 File Offset: 0x0003AFF8
		public static void SetOpenStatus(bool open, IntPtr handle)
		{
			if (ImeModeConversion.InputLanguageTable != ImeModeConversion.UnsupportedTable)
			{
				IntPtr intPtr = UnsafeNativeMethods.ImmGetContext(new HandleRef(null, handle));
				if (intPtr != IntPtr.Zero)
				{
					bool flag = UnsafeNativeMethods.ImmSetOpenStatus(new HandleRef(null, intPtr), open);
					if (flag)
					{
						flag = UnsafeNativeMethods.ImmReleaseContext(new HandleRef(null, handle), new HandleRef(null, intPtr));
					}
				}
			}
		}

		// Token: 0x040008FC RID: 2300
		private static IntPtr originalImeContext;
	}
}
