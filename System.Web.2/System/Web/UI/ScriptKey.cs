using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web.Compilation;
using System.Web.Handlers;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000254 RID: 596
	[Serializable]
	internal class ScriptKey
	{
		// Token: 0x06001B97 RID: 7063 RVA: 0x00057225 File Offset: 0x00055425
		internal ScriptKey(Type type, string key) : this(type, key, false, false)
		{
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x00057231 File Offset: 0x00055431
		internal ScriptKey(Type type, string key, bool isInclude, bool isResource)
		{
			this._type = type;
			if (key == null)
			{
				key = string.Empty;
			}
			this._key = key;
			this._isInclude = isInclude;
			this._isResource = isResource;
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x00057260 File Offset: 0x00055460
		public Assembly Assembly
		{
			get
			{
				if (!(this._type == null))
				{
					return AssemblyResourceLoader.GetAssemblyFromType(this._type);
				}
				return null;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x0005727D File Offset: 0x0005547D
		public bool IsResource
		{
			get
			{
				return this._isResource;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001B9B RID: 7067 RVA: 0x00057285 File Offset: 0x00055485
		public string Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x0005728D File Offset: 0x0005548D
		public override int GetHashCode()
		{
			return HashCodeCombiner.CombineHashCodes(this._type.GetHashCode(), this._key.GetHashCode(), this._isInclude.GetHashCode());
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x000572B8 File Offset: 0x000554B8
		public override bool Equals(object o)
		{
			ScriptKey scriptKey = (ScriptKey)o;
			return scriptKey._type == this._type && scriptKey._key == this._key && scriptKey._isInclude == this._isInclude;
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x00057302 File Offset: 0x00055502
		[OnSerializing]
		private void OnSerializingMethod(StreamingContext context)
		{
			this._typeNameForSerialization = Util.GetAssemblyQualifiedTypeName(this._type);
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x00057315 File Offset: 0x00055515
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			this._type = BuildManager.GetType(this._typeNameForSerialization, true, false);
		}

		// Token: 0x040018C0 RID: 6336
		[NonSerialized]
		private Type _type;

		// Token: 0x040018C1 RID: 6337
		private string _typeNameForSerialization;

		// Token: 0x040018C2 RID: 6338
		private string _key;

		// Token: 0x040018C3 RID: 6339
		private bool _isInclude;

		// Token: 0x040018C4 RID: 6340
		private bool _isResource;
	}
}
