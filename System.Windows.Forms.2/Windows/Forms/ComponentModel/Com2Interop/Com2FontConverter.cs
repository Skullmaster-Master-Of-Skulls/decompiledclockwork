using System;
using System.Drawing;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x0200049A RID: 1178
	internal class Com2FontConverter : Com2DataTypeToManagedDataTypeConverter
	{
		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x06004E94 RID: 20116 RVA: 0x00013062 File Offset: 0x00011262
		public override bool AllowExpand
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x06004E95 RID: 20117 RVA: 0x00143579 File Offset: 0x00141779
		public override Type ManagedType
		{
			get
			{
				return typeof(Font);
			}
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x00143588 File Offset: 0x00141788
		public override object ConvertNativeToManaged(object nativeValue, Com2PropertyDescriptor pd)
		{
			UnsafeNativeMethods.IFont font = nativeValue as UnsafeNativeMethods.IFont;
			if (font == null)
			{
				this.lastHandle = IntPtr.Zero;
				this.lastFont = Control.DefaultFont;
				return this.lastFont;
			}
			IntPtr hfont = font.GetHFont();
			if (hfont == this.lastHandle && this.lastFont != null)
			{
				return this.lastFont;
			}
			this.lastHandle = hfont;
			try
			{
				Font font2 = Font.FromHfont(this.lastHandle);
				try
				{
					this.lastFont = ControlPaint.FontInPoints(font2);
				}
				finally
				{
					font2.Dispose();
				}
			}
			catch (ArgumentException)
			{
				this.lastFont = Control.DefaultFont;
			}
			return this.lastFont;
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x0014363C File Offset: 0x0014183C
		public override object ConvertManagedToNative(object managedValue, Com2PropertyDescriptor pd, ref bool cancelSet)
		{
			if (managedValue == null)
			{
				managedValue = Control.DefaultFont;
			}
			cancelSet = true;
			if (this.lastFont != null && this.lastFont.Equals(managedValue))
			{
				return null;
			}
			this.lastFont = (Font)managedValue;
			UnsafeNativeMethods.IFont font = (UnsafeNativeMethods.IFont)pd.GetNativeValue(pd.TargetObject);
			if (font != null)
			{
				bool flag = ControlPaint.FontToIFont(this.lastFont, font);
				if (flag)
				{
					this.lastFont = null;
					this.ConvertNativeToManaged(font, pd);
				}
			}
			return null;
		}

		// Token: 0x04003413 RID: 13331
		private IntPtr lastHandle = IntPtr.Zero;

		// Token: 0x04003414 RID: 13332
		private Font lastFont;
	}
}
