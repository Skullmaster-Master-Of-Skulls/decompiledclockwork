using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms.Internal;

namespace System.Windows.Forms
{
	// Token: 0x02000446 RID: 1094
	internal sealed class WindowsFormsUtils
	{
		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06004BF1 RID: 19441 RVA: 0x0013B71C File Offset: 0x0013991C
		public static Point LastCursorPoint
		{
			get
			{
				int messagePos = SafeNativeMethods.GetMessagePos();
				return new Point(NativeMethods.Util.SignedLOWORD(messagePos), NativeMethods.Util.SignedHIWORD(messagePos));
			}
		}

		// Token: 0x06004BF2 RID: 19442 RVA: 0x0013B740 File Offset: 0x00139940
		public static Graphics CreateMeasurementGraphics()
		{
			return Graphics.FromHdcInternal(WindowsGraphicsCacheManager.MeasurementGraphics.DeviceContext.Hdc);
		}

		// Token: 0x06004BF3 RID: 19443 RVA: 0x0013B758 File Offset: 0x00139958
		public static bool ContainsMnemonic(string text)
		{
			if (text != null)
			{
				int length = text.Length;
				int num = text.IndexOf('&', 0);
				if (num >= 0 && num <= length - 2)
				{
					int num2 = text.IndexOf('&', num + 1);
					if (num2 == -1)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004BF4 RID: 19444 RVA: 0x0013B796 File Offset: 0x00139996
		internal static Rectangle ConstrainToScreenWorkingAreaBounds(Rectangle bounds)
		{
			return WindowsFormsUtils.ConstrainToBounds(Screen.GetWorkingArea(bounds), bounds);
		}

		// Token: 0x06004BF5 RID: 19445 RVA: 0x0013B7A4 File Offset: 0x001399A4
		internal static Rectangle ConstrainToScreenBounds(Rectangle bounds)
		{
			return WindowsFormsUtils.ConstrainToBounds(Screen.FromRectangle(bounds).Bounds, bounds);
		}

		// Token: 0x06004BF6 RID: 19446 RVA: 0x0013B7B8 File Offset: 0x001399B8
		internal static Rectangle ConstrainToBounds(Rectangle constrainingBounds, Rectangle bounds)
		{
			if (!constrainingBounds.Contains(bounds))
			{
				bounds.Size = new Size(Math.Min(constrainingBounds.Width - 2, bounds.Width), Math.Min(constrainingBounds.Height - 2, bounds.Height));
				if (bounds.Right > constrainingBounds.Right)
				{
					bounds.X = constrainingBounds.Right - bounds.Width;
				}
				else if (bounds.Left < constrainingBounds.Left)
				{
					bounds.X = constrainingBounds.Left;
				}
				if (bounds.Bottom > constrainingBounds.Bottom)
				{
					bounds.Y = constrainingBounds.Bottom - 1 - bounds.Height;
				}
				else if (bounds.Top < constrainingBounds.Top)
				{
					bounds.Y = constrainingBounds.Top;
				}
			}
			return bounds;
		}

		// Token: 0x06004BF7 RID: 19447 RVA: 0x0013B898 File Offset: 0x00139A98
		internal static string EscapeTextWithAmpersands(string text)
		{
			if (text == null)
			{
				return null;
			}
			int i = text.IndexOf('&');
			if (i == -1)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(text.Substring(0, i));
			while (i < text.Length)
			{
				if (text[i] == '&')
				{
					stringBuilder.Append("&");
				}
				if (i < text.Length)
				{
					stringBuilder.Append(text[i]);
				}
				i++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004BF8 RID: 19448 RVA: 0x0013B90C File Offset: 0x00139B0C
		internal static string GetControlInformation(IntPtr hwnd)
		{
			if (hwnd == IntPtr.Zero)
			{
				return "Handle is IntPtr.Zero";
			}
			return "";
		}

		// Token: 0x06004BF9 RID: 19449 RVA: 0x0013B933 File Offset: 0x00139B33
		internal static string AssertControlInformation(bool condition, Control control)
		{
			if (condition)
			{
				return string.Empty;
			}
			return WindowsFormsUtils.GetControlInformation(control.Handle);
		}

		// Token: 0x06004BFA RID: 19450 RVA: 0x0013B94C File Offset: 0x00139B4C
		internal static int GetCombinedHashCodes(params int[] args)
		{
			int num = -757577119;
			for (int i = 0; i < args.Length; i++)
			{
				num = (args[i] ^ num) * -1640531535;
			}
			return num;
		}

		// Token: 0x06004BFB RID: 19451 RVA: 0x0013B97C File Offset: 0x00139B7C
		public static char GetMnemonic(string text, bool bConvertToUpperCase)
		{
			char result = '\0';
			if (text != null)
			{
				int length = text.Length;
				for (int i = 0; i < length - 1; i++)
				{
					if (text[i] == '&')
					{
						if (text[i + 1] == '&')
						{
							i++;
						}
						else
						{
							if (bConvertToUpperCase)
							{
								result = char.ToUpper(text[i + 1], CultureInfo.CurrentCulture);
								break;
							}
							result = char.ToLower(text[i + 1], CultureInfo.CurrentCulture);
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x0013B9F4 File Offset: 0x00139BF4
		public static HandleRef GetRootHWnd(HandleRef hwnd)
		{
			IntPtr ancestor = UnsafeNativeMethods.GetAncestor(new HandleRef(hwnd, hwnd.Handle), 2);
			return new HandleRef(hwnd.Wrapper, ancestor);
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x0013BA27 File Offset: 0x00139C27
		public static HandleRef GetRootHWnd(Control control)
		{
			return WindowsFormsUtils.GetRootHWnd(new HandleRef(control, control.Handle));
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x0013BA3C File Offset: 0x00139C3C
		public static string TextWithoutMnemonics(string text)
		{
			if (text == null)
			{
				return null;
			}
			int i = text.IndexOf('&');
			if (i == -1)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(text.Substring(0, i));
			while (i < text.Length)
			{
				if (text[i] == '&')
				{
					i++;
				}
				if (i < text.Length)
				{
					stringBuilder.Append(text[i]);
				}
				i++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x0013BAA8 File Offset: 0x00139CA8
		public static Point TranslatePoint(Point point, Control fromControl, Control toControl)
		{
			NativeMethods.POINT point2 = new NativeMethods.POINT(point.X, point.Y);
			UnsafeNativeMethods.MapWindowPoints(new HandleRef(fromControl, fromControl.Handle), new HandleRef(toControl, toControl.Handle), point2, 1);
			return new Point(point2.x, point2.y);
		}

		// Token: 0x06004C00 RID: 19456 RVA: 0x0013BAFA File Offset: 0x00139CFA
		public static bool SafeCompareStrings(string string1, string string2, bool ignoreCase)
		{
			return string1 != null && string2 != null && string1.Length == string2.Length && string.Compare(string1, string2, ignoreCase, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06004C01 RID: 19457 RVA: 0x0013BB24 File Offset: 0x00139D24
		public static int RotateLeft(int value, int nBits)
		{
			nBits %= 32;
			return value << nBits | value >> 32 - nBits;
		}

		// Token: 0x06004C02 RID: 19458 RVA: 0x0013BB3C File Offset: 0x00139D3C
		public static string GetComponentName(IComponent component, string defaultNameValue)
		{
			string text = string.Empty;
			if (string.IsNullOrEmpty(defaultNameValue))
			{
				if (component.Site != null)
				{
					text = component.Site.Name;
				}
				if (text == null)
				{
					text = string.Empty;
				}
			}
			else
			{
				text = defaultNameValue;
			}
			return text;
		}

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06004C03 RID: 19459 RVA: 0x0013BB79 File Offset: 0x00139D79
		internal static bool TargetsAtLeast_v4_5
		{
			get
			{
				return WindowsFormsUtils._targetsAtLeast_v4_5;
			}
		}

		// Token: 0x06004C04 RID: 19460 RVA: 0x0013BB80 File Offset: 0x00139D80
		[SecuritySafeCritical]
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		private static bool RunningOnCheck(string propertyName)
		{
			Type type;
			try
			{
				type = typeof(object).GetTypeInfo().Assembly.GetType("System.Runtime.Versioning.BinaryCompatibility", false);
			}
			catch (TypeLoadException)
			{
				return false;
			}
			if (type == null)
			{
				return false;
			}
			PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			return !(property == null) && (bool)property.GetValue(null);
		}

		// Token: 0x04002869 RID: 10345
		public static readonly Size UninitializedSize = new Size(-7199369, -5999471);

		// Token: 0x0400286A RID: 10346
		private static bool _targetsAtLeast_v4_5 = WindowsFormsUtils.RunningOnCheck("TargetsAtLeast_Desktop_V4_5");

		// Token: 0x0400286B RID: 10347
		public static readonly ContentAlignment AnyRightAlign = (ContentAlignment)1092;

		// Token: 0x0400286C RID: 10348
		public static readonly ContentAlignment AnyLeftAlign = (ContentAlignment)273;

		// Token: 0x0400286D RID: 10349
		public static readonly ContentAlignment AnyTopAlign = (ContentAlignment)7;

		// Token: 0x0400286E RID: 10350
		public static readonly ContentAlignment AnyBottomAlign = (ContentAlignment)1792;

		// Token: 0x0400286F RID: 10351
		public static readonly ContentAlignment AnyMiddleAlign = (ContentAlignment)112;

		// Token: 0x04002870 RID: 10352
		public static readonly ContentAlignment AnyCenterAlign = (ContentAlignment)546;

		// Token: 0x0200082D RID: 2093
		public static class EnumValidator
		{
			// Token: 0x06007044 RID: 28740 RVA: 0x0019B97C File Offset: 0x00199B7C
			public static bool IsValidContentAlignment(ContentAlignment contentAlign)
			{
				if (ClientUtils.GetBitCount((uint)contentAlign) != 1)
				{
					return false;
				}
				int num = 1911;
				return (num & (int)contentAlign) != 0;
			}

			// Token: 0x06007045 RID: 28741 RVA: 0x0019B9A0 File Offset: 0x00199BA0
			public static bool IsEnumWithinShiftedRange(Enum enumValue, int numBitsToShift, int minValAfterShift, int maxValAfterShift)
			{
				int num = Convert.ToInt32(enumValue, CultureInfo.InvariantCulture);
				int num2 = num >> numBitsToShift;
				return num2 << numBitsToShift == num && num2 >= minValAfterShift && num2 <= maxValAfterShift;
			}

			// Token: 0x06007046 RID: 28742 RVA: 0x0019B9D8 File Offset: 0x00199BD8
			public static bool IsValidTextImageRelation(TextImageRelation relation)
			{
				return ClientUtils.IsEnumValid(relation, (int)relation, 0, 8, 1);
			}

			// Token: 0x06007047 RID: 28743 RVA: 0x0019B9E9 File Offset: 0x00199BE9
			public static bool IsValidArrowDirection(ArrowDirection direction)
			{
				return direction <= ArrowDirection.Up || direction - ArrowDirection.Right <= 1;
			}
		}

		// Token: 0x0200082E RID: 2094
		public class ArraySubsetEnumerator : IEnumerator
		{
			// Token: 0x06007048 RID: 28744 RVA: 0x0019B9F9 File Offset: 0x00199BF9
			public ArraySubsetEnumerator(object[] array, int count)
			{
				this.array = array;
				this.total = count;
				this.current = -1;
			}

			// Token: 0x06007049 RID: 28745 RVA: 0x0019BA16 File Offset: 0x00199C16
			public bool MoveNext()
			{
				if (this.current < this.total - 1)
				{
					this.current++;
					return true;
				}
				return false;
			}

			// Token: 0x0600704A RID: 28746 RVA: 0x0019BA39 File Offset: 0x00199C39
			public void Reset()
			{
				this.current = -1;
			}

			// Token: 0x1700187F RID: 6271
			// (get) Token: 0x0600704B RID: 28747 RVA: 0x0019BA42 File Offset: 0x00199C42
			public object Current
			{
				get
				{
					if (this.current == -1)
					{
						return null;
					}
					return this.array[this.current];
				}
			}

			// Token: 0x04004353 RID: 17235
			private object[] array;

			// Token: 0x04004354 RID: 17236
			private int total;

			// Token: 0x04004355 RID: 17237
			private int current;
		}

		// Token: 0x0200082F RID: 2095
		internal class ReadOnlyControlCollection : Control.ControlCollection
		{
			// Token: 0x0600704C RID: 28748 RVA: 0x0019BA5C File Offset: 0x00199C5C
			public ReadOnlyControlCollection(Control owner, bool isReadOnly) : base(owner)
			{
				this._isReadOnly = isReadOnly;
			}

			// Token: 0x0600704D RID: 28749 RVA: 0x0019BA6C File Offset: 0x00199C6C
			public override void Add(Control value)
			{
				if (this.IsReadOnly)
				{
					throw new NotSupportedException(SR.GetString("ReadonlyControlsCollection"));
				}
				this.AddInternal(value);
			}

			// Token: 0x0600704E RID: 28750 RVA: 0x0019BA8D File Offset: 0x00199C8D
			internal virtual void AddInternal(Control value)
			{
				base.Add(value);
			}

			// Token: 0x0600704F RID: 28751 RVA: 0x0019BA96 File Offset: 0x00199C96
			public override void Clear()
			{
				if (this.IsReadOnly)
				{
					throw new NotSupportedException(SR.GetString("ReadonlyControlsCollection"));
				}
				base.Clear();
			}

			// Token: 0x06007050 RID: 28752 RVA: 0x00179B40 File Offset: 0x00177D40
			internal virtual void RemoveInternal(Control value)
			{
				base.Remove(value);
			}

			// Token: 0x06007051 RID: 28753 RVA: 0x0019BAB6 File Offset: 0x00199CB6
			public override void RemoveByKey(string key)
			{
				if (this.IsReadOnly)
				{
					throw new NotSupportedException(SR.GetString("ReadonlyControlsCollection"));
				}
				base.RemoveByKey(key);
			}

			// Token: 0x17001880 RID: 6272
			// (get) Token: 0x06007052 RID: 28754 RVA: 0x0019BAD7 File Offset: 0x00199CD7
			public override bool IsReadOnly
			{
				get
				{
					return this._isReadOnly;
				}
			}

			// Token: 0x04004356 RID: 17238
			private readonly bool _isReadOnly;
		}

		// Token: 0x02000830 RID: 2096
		internal class TypedControlCollection : WindowsFormsUtils.ReadOnlyControlCollection
		{
			// Token: 0x06007053 RID: 28755 RVA: 0x0019BADF File Offset: 0x00199CDF
			public TypedControlCollection(Control owner, Type typeOfControl, bool isReadOnly) : base(owner, isReadOnly)
			{
				this.typeOfControl = typeOfControl;
				this.ownerControl = owner;
			}

			// Token: 0x06007054 RID: 28756 RVA: 0x0019BAF7 File Offset: 0x00199CF7
			public TypedControlCollection(Control owner, Type typeOfControl) : base(owner, false)
			{
				this.typeOfControl = typeOfControl;
				this.ownerControl = owner;
			}

			// Token: 0x06007055 RID: 28757 RVA: 0x0019BB10 File Offset: 0x00199D10
			public override void Add(Control value)
			{
				Control.CheckParentingCycle(this.ownerControl, value);
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.IsReadOnly)
				{
					throw new NotSupportedException(SR.GetString("ReadonlyControlsCollection"));
				}
				if (!this.typeOfControl.IsAssignableFrom(value.GetType()))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("TypedControlCollectionShouldBeOfType", new object[]
					{
						this.typeOfControl.Name
					}), new object[0]), value.GetType().Name);
				}
				base.Add(value);
			}

			// Token: 0x04004357 RID: 17239
			private Type typeOfControl;

			// Token: 0x04004358 RID: 17240
			private Control ownerControl;
		}

		// Token: 0x02000831 RID: 2097
		internal struct DCMapping : IDisposable
		{
			// Token: 0x06007056 RID: 28758 RVA: 0x0019BBA8 File Offset: 0x00199DA8
			public DCMapping(HandleRef hDC, Rectangle bounds)
			{
				if (hDC.Handle == IntPtr.Zero)
				{
					throw new ArgumentNullException("hDC");
				}
				NativeMethods.POINT point = new NativeMethods.POINT();
				HandleRef handleRef = NativeMethods.NullHandleRef;
				this.translatedBounds = bounds;
				this.graphics = null;
				this.dc = DeviceContext.FromHdc(hDC.Handle);
				this.dc.SaveHdc();
				bool flag = SafeNativeMethods.GetViewportOrgEx(hDC, point);
				HandleRef handleRef2 = new HandleRef(null, SafeNativeMethods.CreateRectRgn(point.x + bounds.Left, point.y + bounds.Top, point.x + bounds.Right, point.y + bounds.Bottom));
				try
				{
					handleRef = new HandleRef(this, SafeNativeMethods.CreateRectRgn(0, 0, 0, 0));
					int clipRgn = SafeNativeMethods.GetClipRgn(hDC, handleRef);
					NativeMethods.POINT point2 = new NativeMethods.POINT();
					flag = SafeNativeMethods.SetViewportOrgEx(hDC, point.x + bounds.Left, point.y + bounds.Top, point2);
					if (clipRgn != 0)
					{
						NativeMethods.RECT rect = default(NativeMethods.RECT);
						NativeMethods.RegionFlags rgnBox = (NativeMethods.RegionFlags)SafeNativeMethods.GetRgnBox(handleRef, ref rect);
						if (rgnBox == NativeMethods.RegionFlags.SIMPLEREGION)
						{
							NativeMethods.RegionFlags regionFlags = (NativeMethods.RegionFlags)SafeNativeMethods.CombineRgn(handleRef2, handleRef2, handleRef, 1);
						}
					}
					else
					{
						SafeNativeMethods.DeleteObject(handleRef);
						handleRef = new HandleRef(null, IntPtr.Zero);
					}
					NativeMethods.RegionFlags regionFlags2 = (NativeMethods.RegionFlags)SafeNativeMethods.SelectClipRgn(hDC, handleRef2);
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
					this.dc.RestoreHdc();
					this.dc.Dispose();
				}
				finally
				{
					flag = SafeNativeMethods.DeleteObject(handleRef2);
					if (handleRef.Handle != IntPtr.Zero)
					{
						flag = SafeNativeMethods.DeleteObject(handleRef);
					}
				}
			}

			// Token: 0x06007057 RID: 28759 RVA: 0x0019BD58 File Offset: 0x00199F58
			public void Dispose()
			{
				if (this.graphics != null)
				{
					this.graphics.Dispose();
					this.graphics = null;
				}
				if (this.dc != null)
				{
					this.dc.RestoreHdc();
					this.dc.Dispose();
					this.dc = null;
				}
			}

			// Token: 0x17001881 RID: 6273
			// (get) Token: 0x06007058 RID: 28760 RVA: 0x0019BDA4 File Offset: 0x00199FA4
			public Graphics Graphics
			{
				get
				{
					if (this.graphics == null)
					{
						this.graphics = Graphics.FromHdcInternal(this.dc.Hdc);
						this.graphics.SetClip(new Rectangle(Point.Empty, this.translatedBounds.Size));
					}
					return this.graphics;
				}
			}

			// Token: 0x04004359 RID: 17241
			private DeviceContext dc;

			// Token: 0x0400435A RID: 17242
			private Graphics graphics;

			// Token: 0x0400435B RID: 17243
			private Rectangle translatedBounds;
		}
	}
}
