using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x02000494 RID: 1172
	internal class Com2ComponentEditor : WindowsFormsComponentEditor
	{
		// Token: 0x06004E67 RID: 20071 RVA: 0x00142DC8 File Offset: 0x00140FC8
		public static bool NeedsComponentEditor(object obj)
		{
			if (obj is NativeMethods.IPerPropertyBrowsing)
			{
				Guid empty = Guid.Empty;
				if (((NativeMethods.IPerPropertyBrowsing)obj).MapPropertyToPage(-1, out empty) == 0 && !empty.Equals(Guid.Empty))
				{
					return true;
				}
			}
			if (obj is NativeMethods.ISpecifyPropertyPages)
			{
				try
				{
					NativeMethods.tagCAUUID tagCAUUID = new NativeMethods.tagCAUUID();
					try
					{
						((NativeMethods.ISpecifyPropertyPages)obj).GetPages(tagCAUUID);
						if (tagCAUUID.cElems > 0)
						{
							return true;
						}
					}
					finally
					{
						if (tagCAUUID.pElems != IntPtr.Zero)
						{
							Marshal.FreeCoTaskMem(tagCAUUID.pElems);
						}
					}
				}
				catch
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x06004E68 RID: 20072 RVA: 0x00142E74 File Offset: 0x00141074
		public override bool EditComponent(ITypeDescriptorContext context, object obj, IWin32Window parent)
		{
			IntPtr handle = (parent == null) ? IntPtr.Zero : parent.Handle;
			if (obj is NativeMethods.IPerPropertyBrowsing)
			{
				Guid empty = Guid.Empty;
				if (((NativeMethods.IPerPropertyBrowsing)obj).MapPropertyToPage(-1, out empty) == 0 && !empty.Equals(Guid.Empty))
				{
					object obj2 = obj;
					SafeNativeMethods.OleCreatePropertyFrame(new HandleRef(parent, handle), 0, 0, "PropertyPages", 1, ref obj2, 1, new Guid[]
					{
						empty
					}, Application.CurrentCulture.LCID, 0, IntPtr.Zero);
					return true;
				}
			}
			if (obj is NativeMethods.ISpecifyPropertyPages)
			{
				bool flag = false;
				Exception ex2;
				try
				{
					NativeMethods.tagCAUUID tagCAUUID = new NativeMethods.tagCAUUID();
					try
					{
						((NativeMethods.ISpecifyPropertyPages)obj).GetPages(tagCAUUID);
						if (tagCAUUID.cElems <= 0)
						{
							return false;
						}
					}
					catch
					{
						return false;
					}
					try
					{
						object obj3 = obj;
						SafeNativeMethods.OleCreatePropertyFrame(new HandleRef(parent, handle), 0, 0, "PropertyPages", 1, ref obj3, tagCAUUID.cElems, new HandleRef(tagCAUUID, tagCAUUID.pElems), Application.CurrentCulture.LCID, 0, IntPtr.Zero);
						return true;
					}
					finally
					{
						if (tagCAUUID.pElems != IntPtr.Zero)
						{
							Marshal.FreeCoTaskMem(tagCAUUID.pElems);
						}
					}
				}
				catch (Exception ex)
				{
					flag = true;
					ex2 = ex;
				}
				if (!flag)
				{
					return false;
				}
				string @string = SR.GetString("ErrorPropertyPageFailed");
				IUIService iuiservice = (context != null) ? ((IUIService)context.GetService(typeof(IUIService))) : null;
				if (iuiservice == null)
				{
					RTLAwareMessageBox.Show(null, @string, SR.GetString("PropertyGridTitle"), MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
					return false;
				}
				if (ex2 != null)
				{
					iuiservice.ShowError(ex2, @string);
					return false;
				}
				iuiservice.ShowError(@string);
				return false;
			}
			return false;
		}
	}
}
