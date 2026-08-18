using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002F2 RID: 754
	internal class ImageIndexEditor : UITypeEditor
	{
		// Token: 0x06001E16 RID: 7702 RVA: 0x000B682E File Offset: 0x000B4A2E
		public ImageIndexEditor()
		{
			this.imageEditor = (UITypeEditor)TypeDescriptor.GetEditor(typeof(Image), typeof(UITypeEditor));
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x000B6865 File Offset: 0x000B4A65
		internal UITypeEditor ImageEditor
		{
			get
			{
				return this.imageEditor;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x000B686D File Offset: 0x000B4A6D
		internal string ParentImageListProperty
		{
			get
			{
				return this.parentImageListProperty;
			}
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x000B6878 File Offset: 0x000B4A78
		protected virtual Image GetImage(ITypeDescriptorContext context, int index, string key, bool useIntIndex)
		{
			Image result = null;
			object obj = context.Instance;
			if (obj is object[])
			{
				return null;
			}
			if (index >= 0 || key != null)
			{
				PropertyDescriptor propertyDescriptor = null;
				if (this.currentImageListPropRef != null)
				{
					propertyDescriptor = (this.currentImageListPropRef.Target as PropertyDescriptor);
				}
				if (this.currentImageList == null || obj != this.currentInstance || (propertyDescriptor != null && (ImageList)propertyDescriptor.GetValue(this.currentInstance) != this.currentImageList))
				{
					this.currentInstance = obj;
					PropertyDescriptor propertyDescriptor2 = ImageListUtils.GetImageListProperty(context.PropertyDescriptor, ref obj);
					while (obj != null && propertyDescriptor2 == null)
					{
						PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
						foreach (object obj2 in properties)
						{
							PropertyDescriptor propertyDescriptor3 = (PropertyDescriptor)obj2;
							if (typeof(ImageList).IsAssignableFrom(propertyDescriptor3.PropertyType))
							{
								propertyDescriptor2 = propertyDescriptor3;
								break;
							}
						}
						if (propertyDescriptor2 == null)
						{
							PropertyDescriptor propertyDescriptor4 = properties[this.ParentImageListProperty];
							if (propertyDescriptor4 != null)
							{
								obj = propertyDescriptor4.GetValue(obj);
							}
							else
							{
								obj = null;
							}
						}
					}
					if (propertyDescriptor2 != null)
					{
						this.currentImageList = (ImageList)propertyDescriptor2.GetValue(obj);
						this.currentImageListPropRef = new WeakReference(propertyDescriptor2);
						this.currentInstance = obj;
					}
				}
				if (this.currentImageList != null)
				{
					if (useIntIndex)
					{
						if (this.currentImageList != null && index < this.currentImageList.Images.Count)
						{
							index = ((index > 0) ? index : 0);
							result = this.currentImageList.Images[index];
						}
					}
					else
					{
						result = this.currentImageList.Images[key];
					}
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x000B6A28 File Offset: 0x000B4C28
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return this.imageEditor != null && this.imageEditor.GetPaintValueSupported(context);
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x000B6A40 File Offset: 0x000B4C40
		public override void PaintValue(PaintValueEventArgs e)
		{
			if (this.ImageEditor != null)
			{
				Image image = null;
				if (e.Value is int)
				{
					image = this.GetImage(e.Context, (int)e.Value, null, true);
				}
				else if (e.Value is string)
				{
					image = this.GetImage(e.Context, -1, (string)e.Value, false);
				}
				if (image != null)
				{
					this.ImageEditor.PaintValue(new PaintValueEventArgs(e.Context, image, e.Graphics, e.Bounds));
				}
			}
		}

		// Token: 0x040017BD RID: 6077
		protected ImageList currentImageList;

		// Token: 0x040017BE RID: 6078
		protected WeakReference currentImageListPropRef;

		// Token: 0x040017BF RID: 6079
		protected object currentInstance;

		// Token: 0x040017C0 RID: 6080
		protected UITypeEditor imageEditor;

		// Token: 0x040017C1 RID: 6081
		protected string parentImageListProperty = "Parent";

		// Token: 0x040017C2 RID: 6082
		protected string imageListPropertyName;
	}
}
