using System;
using System.Collections;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000571 RID: 1393
	public sealed class TransformerTypeCollection : ReadOnlyCollectionBase
	{
		// Token: 0x060046B6 RID: 18102 RVA: 0x000DCEF2 File Offset: 0x000DB0F2
		public TransformerTypeCollection()
		{
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x000E9D81 File Offset: 0x000E7F81
		public TransformerTypeCollection(ICollection transformerTypes)
		{
			this.Initialize(null, transformerTypes);
		}

		// Token: 0x060046B8 RID: 18104 RVA: 0x000E9D91 File Offset: 0x000E7F91
		public TransformerTypeCollection(TransformerTypeCollection existingTransformerTypes, ICollection transformerTypes)
		{
			this.Initialize(existingTransformerTypes, transformerTypes);
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x000E9DA1 File Offset: 0x000E7FA1
		internal int Add(Type value)
		{
			if (!value.IsSubclassOf(typeof(WebPartTransformer)))
			{
				throw new InvalidOperationException(SR.GetString("WebPartTransformerAttribute_NotTransformer", new object[]
				{
					value.Name
				}));
			}
			return base.InnerList.Add(value);
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x000E9DE0 File Offset: 0x000E7FE0
		private void Initialize(TransformerTypeCollection existingTransformerTypes, ICollection transformerTypes)
		{
			if (existingTransformerTypes != null)
			{
				foreach (object obj in existingTransformerTypes)
				{
					Type value = (Type)obj;
					base.InnerList.Add(value);
				}
			}
			if (transformerTypes != null)
			{
				foreach (object obj2 in transformerTypes)
				{
					if (obj2 == null)
					{
						throw new ArgumentException(SR.GetString("Collection_CantAddNull"), "transformerTypes");
					}
					if (!(obj2 is Type))
					{
						throw new ArgumentException(SR.GetString("Collection_InvalidType", new object[]
						{
							"Type"
						}), "transformerTypes");
					}
					if (!((Type)obj2).IsSubclassOf(typeof(WebPartTransformer)))
					{
						throw new ArgumentException(SR.GetString("WebPartTransformerAttribute_NotTransformer", new object[]
						{
							((Type)obj2).Name
						}), "transformerTypes");
					}
					base.InnerList.Add(obj2);
				}
			}
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x00043ADC File Offset: 0x00041CDC
		public bool Contains(Type value)
		{
			return base.InnerList.Contains(value);
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x00043ACE File Offset: 0x00041CCE
		public int IndexOf(Type value)
		{
			return base.InnerList.IndexOf(value);
		}

		// Token: 0x170014CE RID: 5326
		public Type this[int index]
		{
			get
			{
				return (Type)base.InnerList[index];
			}
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x000DCFA6 File Offset: 0x000DB1A6
		public void CopyTo(Type[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x040026BB RID: 9915
		public static readonly TransformerTypeCollection Empty = new TransformerTypeCollection();
	}
}
