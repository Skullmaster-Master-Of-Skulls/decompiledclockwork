using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000720 RID: 1824
	[Serializable]
	internal class SerializationMonkey : ISerializable, IFieldInfo
	{
		// Token: 0x06004168 RID: 16744 RVA: 0x000DEE54 File Offset: 0x000DDE54
		internal SerializationMonkey(SerializationInfo info, StreamingContext ctx)
		{
			this._obj.RootSetObjectData(info, ctx);
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x000DEE69 File Offset: 0x000DDE69
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x0600416A RID: 16746 RVA: 0x000DEE7A File Offset: 0x000DDE7A
		// (set) Token: 0x0600416B RID: 16747 RVA: 0x000DEE82 File Offset: 0x000DDE82
		public string[] FieldNames
		{
			get
			{
				return this.fieldNames;
			}
			set
			{
				this.fieldNames = value;
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x0600416C RID: 16748 RVA: 0x000DEE8B File Offset: 0x000DDE8B
		// (set) Token: 0x0600416D RID: 16749 RVA: 0x000DEE93 File Offset: 0x000DDE93
		public Type[] FieldTypes
		{
			get
			{
				return this.fieldTypes;
			}
			set
			{
				this.fieldTypes = value;
			}
		}

		// Token: 0x040020E2 RID: 8418
		internal ISerializationRootObject _obj;

		// Token: 0x040020E3 RID: 8419
		internal string[] fieldNames;

		// Token: 0x040020E4 RID: 8420
		internal Type[] fieldTypes;
	}
}
