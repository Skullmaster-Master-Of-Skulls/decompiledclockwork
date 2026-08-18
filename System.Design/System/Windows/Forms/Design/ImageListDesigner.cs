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
	// Token: 0x0200024D RID: 589
	internal class ImageListDesigner : ComponentDesigner
	{
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x00074C45 File Offset: 0x00073C45
		// (set) Token: 0x06001661 RID: 5729 RVA: 0x00074C52 File Offset: 0x00073C52
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

		// Token: 0x06001662 RID: 5730 RVA: 0x00074C7B File Offset: 0x00073C7B
		private bool ShouldSerializeColorDepth()
		{
			return this.Images.Count == 0;
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x00074C8B File Offset: 0x00073C8B
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

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x00074CA7 File Offset: 0x00073CA7
		internal ImageList ImageList
		{
			get
			{
				return (ImageList)base.Component;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x00074CB4 File Offset: 0x00073CB4
		// (set) Token: 0x06001666 RID: 5734 RVA: 0x00074CC1 File Offset: 0x00073CC1
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

		// Token: 0x06001667 RID: 5735 RVA: 0x00074CEA File Offset: 0x00073CEA
		private bool ShouldSerializeImageSize()
		{
			return this.Images.Count == 0;
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x00074CFA File Offset: 0x00073CFA
		// (set) Token: 0x06001669 RID: 5737 RVA: 0x00074D07 File Offset: 0x00073D07
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

		// Token: 0x0600166A RID: 5738 RVA: 0x00074D30 File Offset: 0x00073D30
		private bool ShouldSerializeTransparentColor()
		{
			return !this.TransparentColor.Equals(Color.LightGray);
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x00074D5E File Offset: 0x00073D5E
		// (set) Token: 0x0600166C RID: 5740 RVA: 0x00074D6B File Offset: 0x00073D6B
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

		// Token: 0x0600166D RID: 5741 RVA: 0x00074D84 File Offset: 0x00073D84
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

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x00074E6B File Offset: 0x00073E6B
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

		// Token: 0x040012EF RID: 4847
		private ImageListDesigner.OriginalImageCollection originalImageCollection;

		// Token: 0x040012F0 RID: 4848
		private DesignerActionListCollection _actionLists;

		// Token: 0x0200024E RID: 590
		[Editor("System.Windows.Forms.Design.ImageCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		internal class OriginalImageCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06001670 RID: 5744 RVA: 0x00074EA0 File Offset: 0x00073EA0
			internal OriginalImageCollection(ImageListDesigner owner)
			{
				this.owner = owner;
				this.ReloadFromImageList();
			}

			// Token: 0x06001671 RID: 5745 RVA: 0x00074EC0 File Offset: 0x00073EC0
			private void AssertInvariant()
			{
			}

			// Token: 0x170003D3 RID: 979
			// (get) Token: 0x06001672 RID: 5746 RVA: 0x00074EC2 File Offset: 0x00073EC2
			public int Count
			{
				get
				{
					this.AssertInvariant();
					return this.list.Count;
				}
			}

			// Token: 0x170003D4 RID: 980
			// (get) Token: 0x06001673 RID: 5747 RVA: 0x00074ED5 File Offset: 0x00073ED5
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170003D5 RID: 981
			// (get) Token: 0x06001674 RID: 5748 RVA: 0x00074ED8 File Offset: 0x00073ED8
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170003D6 RID: 982
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

			// Token: 0x170003D7 RID: 983
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

			// Token: 0x06001679 RID: 5753 RVA: 0x00075012 File Offset: 0x00074012
			public void SetKeyName(int index, string name)
			{
				this[index].Name = name;
				this.owner.ImageList.Images.SetKeyName(index, name);
			}

			// Token: 0x0600167A RID: 5754 RVA: 0x00075038 File Offset: 0x00074038
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

			// Token: 0x0600167B RID: 5755 RVA: 0x0007509C File Offset: 0x0007409C
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

			// Token: 0x0600167C RID: 5756 RVA: 0x000750D8 File Offset: 0x000740D8
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

			// Token: 0x0600167D RID: 5757 RVA: 0x0007511C File Offset: 0x0007411C
			internal void ReloadFromImageList()
			{
				this.list.Clear();
				StringCollection keys = this.owner.ImageList.Images.Keys;
				for (int i = 0; i < this.owner.ImageList.Images.Count; i++)
				{
					this.list.Add(new ImageListImage((Bitmap)this.owner.ImageList.Images[i], keys[i]));
				}
			}

			// Token: 0x0600167E RID: 5758 RVA: 0x0007519D File Offset: 0x0007419D
			public void Clear()
			{
				this.AssertInvariant();
				this.list.Clear();
				this.owner.ImageList.Images.Clear();
			}

			// Token: 0x0600167F RID: 5759 RVA: 0x000751C5 File Offset: 0x000741C5
			public bool Contains(ImageListImage value)
			{
				return this.list.Contains(value.Image);
			}

			// Token: 0x06001680 RID: 5760 RVA: 0x000751D8 File Offset: 0x000741D8
			bool IList.Contains(object value)
			{
				return value is ImageListImage && this.Contains((ImageListImage)value);
			}

			// Token: 0x06001681 RID: 5761 RVA: 0x000751F0 File Offset: 0x000741F0
			public IEnumerator GetEnumerator()
			{
				return this.list.GetEnumerator();
			}

			// Token: 0x06001682 RID: 5762 RVA: 0x000751FD File Offset: 0x000741FD
			public int IndexOf(Image value)
			{
				return this.list.IndexOf(value);
			}

			// Token: 0x06001683 RID: 5763 RVA: 0x0007520B File Offset: 0x0007420B
			int IList.IndexOf(object value)
			{
				if (value is Image)
				{
					return this.IndexOf((Image)value);
				}
				return -1;
			}

			// Token: 0x06001684 RID: 5764 RVA: 0x00075223 File Offset: 0x00074223
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06001685 RID: 5765 RVA: 0x0007522C File Offset: 0x0007422C
			internal void PopulateHandle()
			{
				for (int i = 0; i < this.list.Count; i++)
				{
					ImageListImage imageListImage = (ImageListImage)this.list[i];
					this.owner.ImageList.Images.Add(imageListImage.Name, imageListImage.Image);
				}
			}

			// Token: 0x06001686 RID: 5766 RVA: 0x00075282 File Offset: 0x00074282
			private void RecreateHandle()
			{
				this.owner.ImageList.Images.Clear();
				this.PopulateHandle();
			}

			// Token: 0x06001687 RID: 5767 RVA: 0x0007529F File Offset: 0x0007429F
			public void Remove(Image value)
			{
				this.AssertInvariant();
				this.list.Remove(value);
				this.RecreateHandle();
			}

			// Token: 0x06001688 RID: 5768 RVA: 0x000752B9 File Offset: 0x000742B9
			void IList.Remove(object value)
			{
				if (value is Image)
				{
					this.Remove((Image)value);
				}
			}

			// Token: 0x06001689 RID: 5769 RVA: 0x000752D0 File Offset: 0x000742D0
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

			// Token: 0x170003D8 RID: 984
			// (get) Token: 0x0600168A RID: 5770 RVA: 0x00075331 File Offset: 0x00074331
			int ICollection.Count
			{
				get
				{
					return this.Count;
				}
			}

			// Token: 0x170003D9 RID: 985
			// (get) Token: 0x0600168B RID: 5771 RVA: 0x00075339 File Offset: 0x00074339
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170003DA RID: 986
			// (get) Token: 0x0600168C RID: 5772 RVA: 0x0007533C File Offset: 0x0007433C
			object ICollection.SyncRoot
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0600168D RID: 5773 RVA: 0x0007533F File Offset: 0x0007433F
			void ICollection.CopyTo(Array array, int index)
			{
				this.list.CopyTo(array, index);
			}

			// Token: 0x0600168E RID: 5774 RVA: 0x0007534E File Offset: 0x0007434E
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040012F1 RID: 4849
			private ImageListDesigner owner;

			// Token: 0x040012F2 RID: 4850
			private IList list = new ArrayList();
		}
	}
}
