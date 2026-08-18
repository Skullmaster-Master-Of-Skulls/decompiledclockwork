using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;

namespace System.Drawing.Design
{
	// Token: 0x02000082 RID: 130
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class UITypeEditor
	{
		// Token: 0x060008B6 RID: 2230 RVA: 0x00021EC8 File Offset: 0x000200C8
		static UITypeEditor()
		{
			Hashtable hashtable = new Hashtable();
			hashtable[typeof(DateTime)] = "System.ComponentModel.Design.DateTimeEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			hashtable[typeof(Array)] = "System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			hashtable[typeof(IList)] = "System.ComponentModel.Design.CollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			hashtable[typeof(ICollection)] = "System.ComponentModel.Design.CollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			hashtable[typeof(byte[])] = "System.ComponentModel.Design.BinaryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			hashtable[typeof(Stream)] = "System.ComponentModel.Design.BinaryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			hashtable[typeof(string[])] = "System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			hashtable[typeof(Collection<string>)] = "System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			TypeDescriptor.AddEditorTable(typeof(UITypeEditor), hashtable);
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x0001E380 File Offset: 0x0001C580
		public virtual bool IsDropDownResizable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00021F93 File Offset: 0x00020193
		public object EditValue(IServiceProvider provider, object value)
		{
			return this.EditValue(null, provider, value);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00021F9E File Offset: 0x0002019E
		public virtual object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			return value;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00021FA1 File Offset: 0x000201A1
		public UITypeEditorEditStyle GetEditStyle()
		{
			return this.GetEditStyle(null);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00021FAA File Offset: 0x000201AA
		public bool GetPaintValueSupported()
		{
			return this.GetPaintValueSupported(null);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001E380 File Offset: 0x0001C580
		public virtual bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0000848C File Offset: 0x0000668C
		public virtual UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.None;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00021FB3 File Offset: 0x000201B3
		public void PaintValue(object value, Graphics canvas, Rectangle rectangle)
		{
			this.PaintValue(new PaintValueEventArgs(null, value, canvas, rectangle));
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00015259 File Offset: 0x00013459
		public virtual void PaintValue(PaintValueEventArgs e)
		{
		}
	}
}
