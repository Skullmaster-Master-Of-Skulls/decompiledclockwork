using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200048D RID: 1165
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class PropertyTab : IExtenderProvider
	{
		// Token: 0x06004E34 RID: 20020 RVA: 0x00142944 File Offset: 0x00140B44
		~PropertyTab()
		{
			this.Dispose(false);
		}

		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x06004E35 RID: 20021 RVA: 0x00142974 File Offset: 0x00140B74
		public virtual Bitmap Bitmap
		{
			get
			{
				if (!this.checkedBmp && this.bitmap == null)
				{
					string resource = base.GetType().Name + ".bmp";
					try
					{
						this.bitmap = new Bitmap(base.GetType(), resource);
					}
					catch (Exception ex)
					{
					}
					this.checkedBmp = true;
				}
				return this.bitmap;
			}
		}

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x06004E36 RID: 20022 RVA: 0x001429DC File Offset: 0x00140BDC
		// (set) Token: 0x06004E37 RID: 20023 RVA: 0x001429E4 File Offset: 0x00140BE4
		public virtual object[] Components
		{
			get
			{
				return this.components;
			}
			set
			{
				this.components = value;
			}
		}

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x06004E38 RID: 20024
		public abstract string TabName { get; }

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x06004E39 RID: 20025 RVA: 0x001429ED File Offset: 0x00140BED
		public virtual string HelpKeyword
		{
			get
			{
				return this.TabName;
			}
		}

		// Token: 0x06004E3A RID: 20026 RVA: 0x00013062 File Offset: 0x00011262
		public virtual bool CanExtend(object extendee)
		{
			return true;
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x001429F5 File Offset: 0x00140BF5
		public virtual void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x00142A04 File Offset: 0x00140C04
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.bitmap != null)
			{
				this.bitmap.Dispose();
				this.bitmap = null;
			}
		}

		// Token: 0x06004E3D RID: 20029 RVA: 0x00142A23 File Offset: 0x00140C23
		public virtual PropertyDescriptor GetDefaultProperty(object component)
		{
			return TypeDescriptor.GetDefaultProperty(component);
		}

		// Token: 0x06004E3E RID: 20030 RVA: 0x00142A2B File Offset: 0x00140C2B
		public virtual PropertyDescriptorCollection GetProperties(object component)
		{
			return this.GetProperties(component, null);
		}

		// Token: 0x06004E3F RID: 20031
		public abstract PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes);

		// Token: 0x06004E40 RID: 20032 RVA: 0x00142A35 File Offset: 0x00140C35
		public virtual PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object component, Attribute[] attributes)
		{
			return this.GetProperties(component, attributes);
		}

		// Token: 0x040033FA RID: 13306
		private object[] components;

		// Token: 0x040033FB RID: 13307
		private Bitmap bitmap;

		// Token: 0x040033FC RID: 13308
		private bool checkedBmp;
	}
}
