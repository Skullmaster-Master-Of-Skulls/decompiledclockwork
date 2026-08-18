using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x02000492 RID: 1170
	internal class Com2AboutBoxPropertyDescriptor : Com2PropertyDescriptor
	{
		// Token: 0x06004E58 RID: 20056 RVA: 0x00142C7C File Offset: 0x00140E7C
		public Com2AboutBoxPropertyDescriptor() : base(-552, "About", new Attribute[]
		{
			new DispIdAttribute(-552),
			DesignerSerializationVisibilityAttribute.Hidden,
			new DescriptionAttribute(SR.GetString("AboutBoxDesc")),
			new ParenthesizePropertyNameAttribute(true)
		}, true, typeof(string), null, false)
		{
		}

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x06004E59 RID: 20057 RVA: 0x00142CDC File Offset: 0x00140EDC
		public override Type ComponentType
		{
			get
			{
				return typeof(UnsafeNativeMethods.IDispatch);
			}
		}

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x06004E5A RID: 20058 RVA: 0x00142CE8 File Offset: 0x00140EE8
		public override TypeConverter Converter
		{
			get
			{
				if (this.converter == null)
				{
					this.converter = new TypeConverter();
				}
				return this.converter;
			}
		}

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x06004E5B RID: 20059 RVA: 0x00013062 File Offset: 0x00011262
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x06004E5C RID: 20060 RVA: 0x00142D03 File Offset: 0x00140F03
		public override Type PropertyType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x06004E5D RID: 20061 RVA: 0x00011A20 File Offset: 0x0000FC20
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x06004E5E RID: 20062 RVA: 0x00142D0F File Offset: 0x00140F0F
		public override object GetEditor(Type editorBaseType)
		{
			if (editorBaseType == typeof(UITypeEditor) && this.editor == null)
			{
				this.editor = new Com2AboutBoxPropertyDescriptor.AboutBoxUITypeEditor();
			}
			return this.editor;
		}

		// Token: 0x06004E5F RID: 20063 RVA: 0x000F1AC4 File Offset: 0x000EFCC4
		public override object GetValue(object component)
		{
			return "";
		}

		// Token: 0x06004E60 RID: 20064 RVA: 0x000072B6 File Offset: 0x000054B6
		public override void ResetValue(object component)
		{
		}

		// Token: 0x06004E61 RID: 20065 RVA: 0x00142D3C File Offset: 0x00140F3C
		public override void SetValue(object component, object value)
		{
			throw new ArgumentException();
		}

		// Token: 0x06004E62 RID: 20066 RVA: 0x00011A20 File Offset: 0x0000FC20
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x0400340A RID: 13322
		private TypeConverter converter;

		// Token: 0x0400340B RID: 13323
		private UITypeEditor editor;

		// Token: 0x02000851 RID: 2129
		public class AboutBoxUITypeEditor : UITypeEditor
		{
			// Token: 0x06007093 RID: 28819 RVA: 0x0019CC94 File Offset: 0x0019AE94
			public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
			{
				object instance = context.Instance;
				if (Marshal.IsComObject(instance) && instance is UnsafeNativeMethods.IDispatch)
				{
					UnsafeNativeMethods.IDispatch dispatch = (UnsafeNativeMethods.IDispatch)instance;
					NativeMethods.tagEXCEPINFO pExcepInfo = new NativeMethods.tagEXCEPINFO();
					Guid empty = Guid.Empty;
					int num = dispatch.Invoke(-552, ref empty, SafeNativeMethods.GetThreadLCID(), 1, new NativeMethods.tagDISPPARAMS(), null, pExcepInfo, null);
				}
				return value;
			}

			// Token: 0x06007094 RID: 28820 RVA: 0x0001627D File Offset: 0x0001447D
			public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
			{
				return UITypeEditorEditStyle.Modal;
			}
		}
	}
}
