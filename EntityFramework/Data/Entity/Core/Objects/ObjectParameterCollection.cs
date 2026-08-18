using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Text;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005A5 RID: 1445
	public class ObjectParameterCollection : ICollection<ObjectParameter>, IEnumerable<ObjectParameter>, IEnumerable
	{
		// Token: 0x06003926 RID: 14630 RVA: 0x00110090 File Offset: 0x0010E290
		internal ObjectParameterCollection(ClrPerspective perspective)
		{
			this._perspective = perspective;
			this._parameters = new List<ObjectParameter>();
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06003927 RID: 14631 RVA: 0x001100AA File Offset: 0x0010E2AA
		public int Count
		{
			get
			{
				return this._parameters.Count;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06003928 RID: 14632 RVA: 0x001100B7 File Offset: 0x0010E2B7
		bool ICollection<ObjectParameter>.IsReadOnly
		{
			get
			{
				return this._locked;
			}
		}

		// Token: 0x170008AC RID: 2220
		public ObjectParameter this[string name]
		{
			get
			{
				int num = this.IndexOf(name);
				if (num == -1)
				{
					throw new ArgumentOutOfRangeException("name", Strings.ObjectParameterCollection_ParameterNameNotFound(name));
				}
				return this._parameters[num];
			}
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x001100F8 File Offset: 0x0010E2F8
		public void Add(ObjectParameter item)
		{
			Check.NotNull<ObjectParameter>(item, "item");
			this.CheckUnlocked();
			if (this.Contains(item))
			{
				throw new ArgumentException(Strings.ObjectParameterCollection_ParameterAlreadyExists(item.Name), "item");
			}
			if (this.Contains(item.Name))
			{
				throw new ArgumentException(Strings.ObjectParameterCollection_DuplicateParameterName(item.Name), "item");
			}
			if (!item.ValidateParameterType(this._perspective))
			{
				throw new ArgumentOutOfRangeException("item", Strings.ObjectParameter_InvalidParameterType(item.ParameterType.FullName));
			}
			this._parameters.Add(item);
			this._cacheKey = null;
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x00110196 File Offset: 0x0010E396
		public void Clear()
		{
			this.CheckUnlocked();
			this._parameters.Clear();
			this._cacheKey = null;
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x001101B0 File Offset: 0x0010E3B0
		public bool Contains(ObjectParameter item)
		{
			Check.NotNull<ObjectParameter>(item, "item");
			return this._parameters.Contains(item);
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x001101CA File Offset: 0x0010E3CA
		public bool Contains(string name)
		{
			Check.NotNull<string>(name, "name");
			return this.IndexOf(name) != -1;
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x001101E5 File Offset: 0x0010E3E5
		public void CopyTo(ObjectParameter[] array, int arrayIndex)
		{
			this._parameters.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x001101F4 File Offset: 0x0010E3F4
		public bool Remove(ObjectParameter item)
		{
			Check.NotNull<ObjectParameter>(item, "item");
			this.CheckUnlocked();
			bool flag = this._parameters.Remove(item);
			if (flag)
			{
				this._cacheKey = null;
			}
			return flag;
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x0011022B File Offset: 0x0010E42B
		public virtual IEnumerator<ObjectParameter> GetEnumerator()
		{
			return ((IEnumerable<ObjectParameter>)this._parameters).GetEnumerator();
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x00110238 File Offset: 0x0010E438
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this._parameters).GetEnumerator();
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x00110248 File Offset: 0x0010E448
		internal string GetCacheKey()
		{
			if (this._cacheKey == null && this._parameters.Count > 0)
			{
				if (1 == this._parameters.Count)
				{
					ObjectParameter objectParameter = this._parameters[0];
					this._cacheKey = "@@1" + objectParameter.Name + ":" + objectParameter.ParameterType.FullName;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder(this._parameters.Count * 20);
					stringBuilder.Append("@@");
					stringBuilder.Append(this._parameters.Count);
					for (int i = 0; i < this._parameters.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(";");
						}
						ObjectParameter objectParameter2 = this._parameters[i];
						stringBuilder.Append(objectParameter2.Name);
						stringBuilder.Append(":");
						stringBuilder.Append(objectParameter2.ParameterType.FullName);
					}
					this._cacheKey = stringBuilder.ToString();
				}
			}
			return this._cacheKey;
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x00110359 File Offset: 0x0010E559
		internal void SetReadOnly(bool isReadOnly)
		{
			this._locked = isReadOnly;
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x00110364 File Offset: 0x0010E564
		internal static ObjectParameterCollection DeepCopy(ObjectParameterCollection copyParams)
		{
			if (copyParams == null)
			{
				return null;
			}
			ObjectParameterCollection objectParameterCollection = new ObjectParameterCollection(copyParams._perspective);
			foreach (ObjectParameter objectParameter in copyParams)
			{
				objectParameterCollection.Add(objectParameter.ShallowCopy());
			}
			return objectParameterCollection;
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x001103C4 File Offset: 0x0010E5C4
		private int IndexOf(string name)
		{
			int num = 0;
			foreach (ObjectParameter objectParameter in this._parameters)
			{
				if (string.Compare(name, objectParameter.Name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x0011042C File Offset: 0x0010E62C
		private void CheckUnlocked()
		{
			if (this._locked)
			{
				throw new InvalidOperationException(Strings.ObjectParameterCollection_ParametersLocked);
			}
		}

		// Token: 0x040015DD RID: 5597
		private bool _locked;

		// Token: 0x040015DE RID: 5598
		private readonly List<ObjectParameter> _parameters;

		// Token: 0x040015DF RID: 5599
		private readonly ClrPerspective _perspective;

		// Token: 0x040015E0 RID: 5600
		private string _cacheKey;
	}
}
