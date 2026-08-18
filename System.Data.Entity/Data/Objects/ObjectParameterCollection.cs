using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Objects
{
	// Token: 0x02000146 RID: 326
	public sealed class ObjectParameterCollection : ICollection<ObjectParameter>, IEnumerable<ObjectParameter>, IEnumerable
	{
		// Token: 0x06001794 RID: 6036 RVA: 0x0004F662 File Offset: 0x0004D862
		internal ObjectParameterCollection(ClrPerspective perspective)
		{
			EntityUtil.CheckArgumentNull<ClrPerspective>(perspective, "perspective");
			this._perspective = perspective;
			this._parameters = new List<ObjectParameter>();
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x0004F688 File Offset: 0x0004D888
		public int Count
		{
			get
			{
				return this._parameters.Count;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x0004F695 File Offset: 0x0004D895
		bool ICollection<ObjectParameter>.IsReadOnly
		{
			get
			{
				return this._locked;
			}
		}

		// Token: 0x170004C5 RID: 1221
		public ObjectParameter this[string name]
		{
			get
			{
				int num = this.IndexOf(name);
				if (num == -1)
				{
					throw EntityUtil.ArgumentOutOfRange(Strings.ObjectParameterCollection_ParameterNameNotFound(name), "name");
				}
				return this._parameters[num];
			}
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0004F6D8 File Offset: 0x0004D8D8
		public void Add(ObjectParameter parameter)
		{
			EntityUtil.CheckArgumentNull<ObjectParameter>(parameter, "parameter");
			this.CheckUnlocked();
			if (this.Contains(parameter))
			{
				throw EntityUtil.Argument(Strings.ObjectParameterCollection_ParameterAlreadyExists(parameter.Name), "parameter");
			}
			if (this.Contains(parameter.Name))
			{
				throw EntityUtil.Argument(Strings.ObjectParameterCollection_DuplicateParameterName(parameter.Name), "parameter");
			}
			if (!parameter.ValidateParameterType(this._perspective))
			{
				throw EntityUtil.ArgumentOutOfRange(Strings.ObjectParameter_InvalidParameterType(parameter.ParameterType.FullName), "parameter");
			}
			this._parameters.Add(parameter);
			this._cacheKey = null;
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0004F776 File Offset: 0x0004D976
		public void Clear()
		{
			this.CheckUnlocked();
			this._parameters.Clear();
			this._cacheKey = null;
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0004F790 File Offset: 0x0004D990
		public bool Contains(ObjectParameter parameter)
		{
			EntityUtil.CheckArgumentNull<ObjectParameter>(parameter, "parameter");
			return this._parameters.Contains(parameter);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0004F7AA File Offset: 0x0004D9AA
		public bool Contains(string name)
		{
			EntityUtil.CheckArgumentNull<string>(name, "name");
			return this.IndexOf(name) != -1;
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0004F7C5 File Offset: 0x0004D9C5
		public void CopyTo(ObjectParameter[] array, int index)
		{
			this._parameters.CopyTo(array, index);
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0004F7D4 File Offset: 0x0004D9D4
		public bool Remove(ObjectParameter parameter)
		{
			EntityUtil.CheckArgumentNull<ObjectParameter>(parameter, "parameter");
			this.CheckUnlocked();
			bool flag = this._parameters.Remove(parameter);
			if (flag)
			{
				this._cacheKey = null;
			}
			return flag;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0004F80B File Offset: 0x0004DA0B
		IEnumerator<ObjectParameter> IEnumerable<ObjectParameter>.GetEnumerator()
		{
			return ((IEnumerable<ObjectParameter>)this._parameters).GetEnumerator();
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0004F818 File Offset: 0x0004DA18
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this._parameters).GetEnumerator();
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0004F828 File Offset: 0x0004DA28
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

		// Token: 0x060017A1 RID: 6049 RVA: 0x0004F939 File Offset: 0x0004DB39
		internal void SetReadOnly(bool isReadOnly)
		{
			this._locked = isReadOnly;
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0004F944 File Offset: 0x0004DB44
		internal static ObjectParameterCollection DeepCopy(ObjectParameterCollection copyParams)
		{
			if (copyParams == null)
			{
				return null;
			}
			ObjectParameterCollection objectParameterCollection = new ObjectParameterCollection(copyParams._perspective);
			foreach (ObjectParameter objectParameter in ((IEnumerable<ObjectParameter>)copyParams))
			{
				objectParameterCollection.Add(objectParameter.ShallowCopy());
			}
			return objectParameterCollection;
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0004F9A4 File Offset: 0x0004DBA4
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

		// Token: 0x060017A4 RID: 6052 RVA: 0x0004FA0C File Offset: 0x0004DC0C
		private void CheckUnlocked()
		{
			if (this._locked)
			{
				throw EntityUtil.InvalidOperation(Strings.ObjectParameterCollection_ParametersLocked);
			}
		}

		// Token: 0x04000A93 RID: 2707
		private bool _locked;

		// Token: 0x04000A94 RID: 2708
		private List<ObjectParameter> _parameters;

		// Token: 0x04000A95 RID: 2709
		private ClrPerspective _perspective;

		// Token: 0x04000A96 RID: 2710
		private string _cacheKey;
	}
}
