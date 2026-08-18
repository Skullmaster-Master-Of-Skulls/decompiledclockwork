using System;
using System.ComponentModel;

namespace System.Drawing.Design
{
	// Token: 0x020000FF RID: 255
	internal class Com2ExtendedUITypeEditor : UITypeEditor
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x0000CDC8 File Offset: 0x0000AFC8
		public Com2ExtendedUITypeEditor(UITypeEditor baseTypeEditor)
		{
			this.innerEditor = baseTypeEditor;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000CDD7 File Offset: 0x0000AFD7
		public Com2ExtendedUITypeEditor(Type baseType)
		{
			this.innerEditor = (UITypeEditor)TypeDescriptor.GetEditor(baseType, typeof(UITypeEditor));
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000CDFA File Offset: 0x0000AFFA
		public UITypeEditor InnerEditor
		{
			get
			{
				return this.innerEditor;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000CE02 File Offset: 0x0000B002
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (this.innerEditor != null)
			{
				return this.innerEditor.EditValue(context, provider, value);
			}
			return base.EditValue(context, provider, value);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000CE24 File Offset: 0x0000B024
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			if (this.innerEditor != null)
			{
				return this.innerEditor.GetPaintValueSupported(context);
			}
			return base.GetPaintValueSupported(context);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000CE42 File Offset: 0x0000B042
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if (this.innerEditor != null)
			{
				return this.innerEditor.GetEditStyle(context);
			}
			return base.GetEditStyle(context);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000CE60 File Offset: 0x0000B060
		public override void PaintValue(PaintValueEventArgs e)
		{
			if (this.innerEditor != null)
			{
				this.innerEditor.PaintValue(e);
			}
			base.PaintValue(e);
		}

		// Token: 0x04000441 RID: 1089
		private UITypeEditor innerEditor;
	}
}
