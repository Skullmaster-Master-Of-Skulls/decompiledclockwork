using System;
using System.Drawing.Design;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design
{
	// Token: 0x02000196 RID: 406
	public sealed class BinaryEditor : UITypeEditor
	{
		// Token: 0x06000EB2 RID: 3762 RVA: 0x0005519C File Offset: 0x0005339C
		internal object GetService(Type serviceType)
		{
			if (this.context == null)
			{
				return null;
			}
			IDesignerHost designerHost = this.context.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (designerHost == null)
			{
				return this.context.GetService(serviceType);
			}
			return designerHost.GetService(serviceType);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x000551E8 File Offset: 0x000533E8
		internal byte[] ConvertToBytes(object value)
		{
			if (value is Stream)
			{
				Stream stream = (Stream)value;
				stream.Position = 0L;
				int num = (int)(stream.Length - stream.Position);
				byte[] array = new byte[num];
				stream.Read(array, 0, num);
				return array;
			}
			if (value is byte[])
			{
				return (byte[])value;
			}
			if (value is string)
			{
				int num2 = ((string)value).Length * 2;
				byte[] array2 = new byte[num2];
				Encoding.Unicode.GetBytes(((string)value).ToCharArray(), 0, num2 / 2, array2, 0);
				return array2;
			}
			return null;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0005527C File Offset: 0x0005347C
		internal void ConvertToValue(byte[] bytes, ref object value)
		{
			if (value is Stream)
			{
				Stream stream = (Stream)value;
				stream.Position = 0L;
				stream.Write(bytes, 0, bytes.Length);
				return;
			}
			if (value is byte[])
			{
				value = bytes;
				return;
			}
			if (value is string)
			{
				value = BitConverter.ToString(bytes);
			}
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x000552CC File Offset: 0x000534CC
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				this.context = context;
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					if (this.binaryUI == null)
					{
						this.binaryUI = DpiHelper.CreateInstanceInSystemAwareContext<BinaryUI>(() => new BinaryUI(this));
					}
					this.binaryUI.Value = value;
					if (windowsFormsEditorService.ShowDialog(this.binaryUI) == DialogResult.OK)
					{
						value = this.binaryUI.Value;
					}
					this.binaryUI.Value = null;
				}
			}
			return value;
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00055350 File Offset: 0x00053550
		internal void ShowHelp()
		{
			IHelpService helpService = this.GetService(typeof(IHelpService)) as IHelpService;
			if (helpService != null)
			{
				helpService.ShowHelpFromKeyword(BinaryEditor.HELP_KEYWORD);
			}
		}

		// Token: 0x040008B2 RID: 2226
		private static readonly string HELP_KEYWORD = "System.ComponentModel.Design.BinaryEditor";

		// Token: 0x040008B3 RID: 2227
		private ITypeDescriptorContext context;

		// Token: 0x040008B4 RID: 2228
		private BinaryUI binaryUI;
	}
}
