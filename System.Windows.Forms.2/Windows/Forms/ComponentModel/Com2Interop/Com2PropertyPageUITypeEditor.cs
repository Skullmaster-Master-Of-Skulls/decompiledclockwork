using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004B0 RID: 1200
	internal class Com2PropertyPageUITypeEditor : Com2ExtendedUITypeEditor, ICom2PropertyPageDisplayService
	{
		// Token: 0x06004F50 RID: 20304 RVA: 0x00146496 File Offset: 0x00144696
		public Com2PropertyPageUITypeEditor(Com2PropertyDescriptor pd, Guid guid, UITypeEditor baseEditor) : base(baseEditor)
		{
			this.propDesc = pd;
			this.guid = guid;
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x001464B0 File Offset: 0x001446B0
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			IntPtr focus = UnsafeNativeMethods.GetFocus();
			try
			{
				ICom2PropertyPageDisplayService com2PropertyPageDisplayService = (ICom2PropertyPageDisplayService)provider.GetService(typeof(ICom2PropertyPageDisplayService));
				if (com2PropertyPageDisplayService == null)
				{
					com2PropertyPageDisplayService = this;
				}
				object obj = context.Instance;
				if (!obj.GetType().IsArray)
				{
					obj = this.propDesc.TargetObject;
					if (obj is ICustomTypeDescriptor)
					{
						obj = ((ICustomTypeDescriptor)obj).GetPropertyOwner(this.propDesc);
					}
				}
				com2PropertyPageDisplayService.ShowPropertyPage(this.propDesc.Name, obj, this.propDesc.DISPID, this.guid, focus);
			}
			catch (Exception ex)
			{
				if (provider != null)
				{
					IUIService iuiservice = (IUIService)provider.GetService(typeof(IUIService));
					if (iuiservice != null)
					{
						iuiservice.ShowError(ex, SR.GetString("ErrorTypeConverterFailed"));
					}
				}
			}
			return value;
		}

		// Token: 0x06004F52 RID: 20306 RVA: 0x0001627D File Offset: 0x0001447D
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x06004F53 RID: 20307 RVA: 0x00146584 File Offset: 0x00144784
		public unsafe void ShowPropertyPage(string title, object component, int dispid, Guid pageGuid, IntPtr parentHandle)
		{
			Guid[] arr = new Guid[]
			{
				pageGuid
			};
			IntPtr handle = Marshal.UnsafeAddrOfPinnedArrayElement(arr, 0);
			object[] array;
			if (!component.GetType().IsArray)
			{
				(array = new object[1])[0] = component;
			}
			else
			{
				array = (object[])component;
			}
			object[] array2 = array;
			int num = array2.Length;
			IntPtr[] array3 = new IntPtr[num];
			try
			{
				for (int i = 0; i < num; i++)
				{
					array3[i] = Marshal.GetIUnknownForObject(array2[i]);
				}
				try
				{
					IntPtr[] array4;
					IntPtr* ptr;
					if ((array4 = array3) == null || array4.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array4[0];
					}
					SafeNativeMethods.OleCreatePropertyFrame(new HandleRef(null, parentHandle), 0, 0, title, num, new HandleRef(null, (IntPtr)ptr), 1, new HandleRef(null, handle), SafeNativeMethods.GetThreadLCID(), 0, IntPtr.Zero);
				}
				finally
				{
					IntPtr[] array4 = null;
				}
			}
			finally
			{
				for (int j = 0; j < num; j++)
				{
					if (array3[j] != IntPtr.Zero)
					{
						Marshal.Release(array3[j]);
					}
				}
			}
		}

		// Token: 0x0400345B RID: 13403
		private Com2PropertyDescriptor propDesc;

		// Token: 0x0400345C RID: 13404
		private Guid guid;
	}
}
