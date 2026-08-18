using System;
using System.Collections;
using System.ComponentModel;
using System.Design;
using System.Drawing.Design;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002F6 RID: 758
	public class ImageListImageEditor : ImageEditor
	{
		// Token: 0x06001E40 RID: 7744 RVA: 0x000B6F1D File Offset: 0x000B511D
		protected override Type[] GetImageExtenders()
		{
			return ImageListImageEditor.imageExtenders;
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x000B6F24 File Offset: 0x000B5124
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			ArrayList arrayList = new ArrayList();
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					if (this.fileDialog == null)
					{
						this.fileDialog = new OpenFileDialog();
						this.fileDialog.Multiselect = true;
						string text = ImageEditor.CreateFilterEntry(this);
						for (int i = 0; i < this.GetImageExtenders().Length; i++)
						{
							ImageEditor imageEditor = (ImageEditor)Activator.CreateInstance(this.GetImageExtenders()[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
							Type type = base.GetType();
							Type type2 = imageEditor.GetType();
							if (!type.Equals(type2) && imageEditor != null && type.IsInstanceOfType(imageEditor))
							{
								text = text + "|" + ImageEditor.CreateFilterEntry(imageEditor);
							}
						}
						this.fileDialog.Filter = text;
					}
					IntPtr focus = UnsafeNativeMethods.GetFocus();
					try
					{
						if (this.fileDialog.ShowDialog() == DialogResult.OK)
						{
							foreach (string text2 in this.fileDialog.FileNames)
							{
								FileStream stream = new FileStream(text2, FileMode.Open, FileAccess.Read, FileShare.Read);
								ImageListImage imageListImage = this.LoadImageFromStream(stream, text2.EndsWith(".ico"));
								imageListImage.Name = Path.GetFileName(text2);
								arrayList.Add(imageListImage);
							}
						}
					}
					finally
					{
						if (focus != IntPtr.Zero)
						{
							UnsafeNativeMethods.SetFocus(new HandleRef(null, focus));
						}
					}
				}
				return arrayList;
			}
			return value;
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x000B70A4 File Offset: 0x000B52A4
		protected override string GetFileDialogDescription()
		{
			return SR.GetString("imageFileDescription");
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x000B70B0 File Offset: 0x000B52B0
		private ImageListImage LoadImageFromStream(Stream stream, bool imageIsIcon)
		{
			byte[] buffer = new byte[stream.Length];
			stream.Read(buffer, 0, (int)stream.Length);
			MemoryStream stream2 = new MemoryStream(buffer);
			return ImageListImage.ImageListImageFromStream(stream2, imageIsIcon);
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x000B70E8 File Offset: 0x000B52E8
		public override void PaintValue(PaintValueEventArgs e)
		{
			if (e.Value is ImageListImage)
			{
				e = new PaintValueEventArgs(e.Context, ((ImageListImage)e.Value).Image, e.Graphics, e.Bounds);
			}
			base.PaintValue(e);
		}

		// Token: 0x040017C8 RID: 6088
		internal static Type[] imageExtenders = new Type[]
		{
			typeof(BitmapEditor)
		};

		// Token: 0x040017C9 RID: 6089
		private OpenFileDialog fileDialog;
	}
}
