using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002F3 RID: 755
	internal class ImageListDesigner : ComponentDesigner
	{
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001E1C RID: 7708 RVA: 0x000B6ACD File Offset: 0x000B4CCD
		// (set) Token: 0x06001E1D RID: 7709 RVA: 0x000B6ADA File Offset: 0x000B4CDA
		private ColorDepth ColorDepth
		{
			get
			{
				return this.ImageList.ColorDepth;
			}
			set
			{
				this.ImageList.Images.Clear();
				this.ImageList.ColorDepth = value;
				this.Images.PopulateHandle();
			}
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x000B6B03 File Offset: 0x000B4D03
		private bool ShouldSerializeColorDepth()
		{
			return this.Images.Count == 0;
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x000B6B13 File Offset: 0x000B4D13
		private ImageListDesigner.OriginalImageCollection Images
		{
			get
			{
				if (this.originalImageCollection == null)
				{
					this.originalImageCollection = new ImageListDesigner.OriginalImageCollection(this);
				}
				return this.originalImageCollection;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x000B6B2F File Offset: 0x000B4D2F
		internal ImageList ImageList
		{
			get
			{
				return (ImageList)base.Component;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x000B6B3C File Offset: 0x000B4D3C
		// (set) Token: 0x06001E22 RID: 7714 RVA: 0x000B6B49 File Offset: 0x000B4D49
		private Size ImageSize
		{
			get
			{
				return this.ImageList.ImageSize;
			}
			set
			{
				this.ImageList.Images.Clear();
				this.ImageList.ImageSize = value;
				this.Images.PopulateHandle();
			}
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x000B6B03 File Offset: 0x000B4D03
		private bool ShouldSerializeImageSize()
		{
			return this.Images.Count == 0;
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x000B6B72 File Offset: 0x000B4D72
		// (set) Token: 0x06001E25 RID: 7717 RVA: 0x000B6B7F File Offset: 0x000B4D7F
		private Color TransparentColor
		{
			get
			{
				return this.ImageList.TransparentColor;
			}
			set
			{
				this.ImageList.Images.Clear();
				this.ImageList.TransparentColor = value;
				this.Images.PopulateHandle();
			}
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x000B6BA8 File Offset: 0x000B4DA8
		private bool ShouldSerializeTransparentColor()
		{
			return !this.TransparentColor.Equals(Color.LightGray);
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001E27 RID: 7719 RVA: 0x000B6BD6 File Offset: 0x000B4DD6
		// (set) Token: 0x06001E28 RID: 7720 RVA: 0x000B6BE3 File Offset: 0x000B4DE3
		private ImageListStreamer ImageStream
		{
			get
			{
				return this.ImageList.ImageStream;
			}
			set
			{
				this.ImageList.ImageStream = value;
				this.Images.ReloadFromImageList();
			}
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x000B6BFC File Offset: 0x000B4DFC
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] array = new string[]
			{
				"ColorDepth",
				"ImageSize",
				"ImageStream",
				"TransparentColor"
			};
			Attribute[] attributes = new Attribute[0];
			for (int i = 0; i < array.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[array[i]];
				if (propertyDescriptor != null)
				{
					properties[array[i]] = TypeDescriptor.CreateProperty(typeof(ImageListDesigner), propertyDescriptor, attributes);
				}
			}
			PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor)properties["Images"];
			if (propertyDescriptor2 != null)
			{
				Attribute[] array2 = new Attribute[propertyDescriptor2.Attributes.Count];
				propertyDescriptor2.Attributes.CopyTo(array2, 0);
				properties["Images"] = TypeDescriptor.CreateProperty(typeof(ImageListDesigner), "Images", typeof(ImageListDesigner.OriginalImageCollection), array2);
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001E2A RID: 7722 RVA: 0x000B6CDA File Offset: 0x000B4EDA
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				if (this._actionLists == null)
				{
					this._actionLists = new DesignerActionListCollection();
					this._actionLists.Add(new ImageListActionList(this));
				}
				return this._actionLists;
			}
		}

		// Token: 0x040017C3 RID: 6083
		private ImageListDesigner.OriginalImageCollection originalImageCollection;

		// Token: 0x040017C4 RID: 6084
		private DesignerActionListCollection _actionLists;

		// Token: 0x0200057D RID: 1405
		[Editor("System.Windows.Forms.Design.ImageCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		internal class OriginalImageCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06003246 RID: 12870 RVA: 0x00110436 File Offset: 0x0010E636
			internal OriginalImageCollection(ImageListDesigner owner)
			{
				this.owner = owner;
				this.ReloadFromImageList();
			}

			// Token: 0x06003247 RID: 12871 RVA: 0x00003937 File Offset: 0x00001B37
			private void AssertInvariant()
			{
			}

			// Token: 0x170009E3 RID: 2531
			// (get) Token: 0x06003248 RID: 12872 RVA: 0x00110456 File Offset: 0x0010E656
			public int Count
			{
				get
				{
					this.AssertInvariant();
					return this.list.Count;
				}
			}

			// Token: 0x170009E4 RID: 2532
			// (get) Token: 0x06003249 RID: 12873 RVA: 0x0000445B File Offset: 0x0000265B
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009E5 RID: 2533
			// (get) Token: 0x0600324A RID: 12874 RVA: 0x0000445B File Offset: 0x0000265B
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009E6 RID: 2534
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public ImageListImage this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException(SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return (ImageListImage)this.list[index];
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException(SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (value == null)
					{
						throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
						{
							"value",
							"null"
						}));
					}
					this.AssertInvariant();
					this.list[index] = value;
					this.RecreateHandle();
				}
			}

			// Token: 0x170009E7 RID: 2535
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (value is ImageListImage)
					{
						this[index] = (ImageListImage)value;
						return;
					}
					throw new ArgumentException(SR.GetString("ImageListDesignerBadImageListImage", new object[]
					{
						"value"
					}));
				}
			}

			// Token: 0x0600324F RID: 12879 RVA: 0x0011058B File Offset: 0x0010E78B
			public void SetKeyName(int index, string name)
			{
				this[index].Name = name;
				this.owner.ImageList.Images.SetKeyName(index, name);
			}

			// Token: 0x06003250 RID: 12880 RVA: 0x001105B4 File Offset: 0x0010E7B4
			public int Add(ImageListImage value)
			{
				int result = this.list.Add(value);
				if (value.Name != null)
				{
					this.owner.ImageList.Images.Add(value.Name, value.Image);
				}
				else
				{
					this.owner.ImageList.Images.Add(value.Image);
				}
				return result;
			}

			// Token: 0x06003251 RID: 12881 RVA: 0x00110618 File Offset: 0x0010E818
			public void AddRange(ImageListImage[] values)
			{
				if (values == null)
				{
					throw new ArgumentNullException("values");
				}
				foreach (ImageListImage imageListImage in values)
				{
					if (imageListImage != null)
					{
						this.Add(imageListImage);
					}
				}
			}

			// Token: 0x06003252 RID: 12882 RVA: 0x00110652 File Offset: 0x0010E852
			int IList.Add(object value)
			{
				if (value is ImageListImage)
				{
					return this.Add((ImageListImage)value);
				}
				throw new ArgumentException(SR.GetString("ImageListDesignerBadImageListImage", new object[]
				{
					"value"
				}));
			}

			// Token: 0x06003253 RID: 12883 RVA: 0x00110688 File Offset: 0x0010E888
			internal void ReloadFromImageList()
			{
				this.list.Clear();
				StringCollection keys = this.owner.ImageList.Images.Keys;
				for (int i = 0; i < this.owner.ImageList.Images.Count; i++)
				{
					this.list.Add(new ImageListImage((Bitmap)this.owner.ImageList.Images[i], keys[i]));
				}
			}

			// Token: 0x06003254 RID: 12884 RVA: 0x00110709 File Offset: 0x0010E909
			public void Clear()
			{
				this.AssertInvariant();
				this.list.Clear();
				this.owner.ImageList.Images.Clear();
			}

			// Token: 0x06003255 RID: 12885 RVA: 0x00110731 File Offset: 0x0010E931
			public bool Contains(ImageListImage value)
			{
				return this.list.Contains(value.Image);
			}

			// Token: 0x06003256 RID: 12886 RVA: 0x00110744 File Offset: 0x0010E944
			bool IList.Contains(object value)
			{
				return value is ImageListImage && this.Contains((ImageListImage)value);
			}

			// Token: 0x06003257 RID: 12887 RVA: 0x0011075C File Offset: 0x0010E95C
			public IEnumerator GetEnumerator()
			{
				return this.list.GetEnumerator();
			}

			// Token: 0x06003258 RID: 12888 RVA: 0x00110769 File Offset: 0x0010E969
			public int IndexOf(Image value)
			{
				return this.list.IndexOf(value);
			}

			// Token: 0x06003259 RID: 12889 RVA: 0x00110777 File Offset: 0x0010E977
			int IList.IndexOf(object value)
			{
				if (value is Image)
				{
					return this.IndexOf((Image)value);
				}
				return -1;
			}

			// Token: 0x0600325A RID: 12890 RVA: 0x0000C5AC File Offset: 0x0000A7AC
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600325B RID: 12891 RVA: 0x00110790 File Offset: 0x0010E990
			internal void PopulateHandle()
			{
				for (int i = 0; i < this.list.Count; i++)
				{
					ImageListImage imageListImage = (ImageListImage)this.list[i];
					this.owner.ImageList.Images.Add(imageListImage.Name, imageListImage.Image);
				}
			}

			// Token: 0x0600325C RID: 12892 RVA: 0x001107E6 File Offset: 0x0010E9E6
			private void RecreateHandle()
			{
				this.owner.ImageList.Images.Clear();
				this.PopulateHandle();
			}

			// Token: 0x0600325D RID: 12893 RVA: 0x00110803 File Offset: 0x0010EA03
			public void Remove(Image value)
			{
				this.AssertInvariant();
				this.list.Remove(value);
				this.RecreateHandle();
			}

			// Token: 0x0600325E RID: 12894 RVA: 0x0011081D File Offset: 0x0010EA1D
			void IList.Remove(object value)
			{
				if (value is Image)
				{
					this.Remove((Image)value);
				}
			}

			// Token: 0x0600325F RID: 12895 RVA: 0x00110834 File Offset: 0x0010EA34
			public void RemoveAt(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException(SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.AssertInvariant();
				this.list.RemoveAt(index);
				this.RecreateHandle();
			}

			// Token: 0x170009E8 RID: 2536
			// (get) Token: 0x06003260 RID: 12896 RVA: 0x00110893 File Offset: 0x0010EA93
			int ICollection.Count
			{
				get
				{
					return this.Count;
				}
			}

			// Token: 0x170009E9 RID: 2537
			// (get) Token: 0x06003261 RID: 12897 RVA: 0x0000445B File Offset: 0x0000265B
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009EA RID: 2538
			// (get) Token: 0x06003262 RID: 12898 RVA: 0x00003598 File Offset: 0x00001798
			object ICollection.SyncRoot
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06003263 RID: 12899 RVA: 0x0011089B File Offset: 0x0010EA9B
			void ICollection.CopyTo(Array array, int index)
			{
				this.list.CopyTo(array, index);
			}

			// Token: 0x06003264 RID: 12900 RVA: 0x001108AA File Offset: 0x0010EAAA
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400218A RID: 8586
			private ImageListDesigner owner;

			// Token: 0x0400218B RID: 8587
			private IList list = new ArrayList();
		}
	}
}
