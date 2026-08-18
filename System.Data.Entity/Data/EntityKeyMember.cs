using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000019 RID: 25
	[DataContract]
	[Serializable]
	public class EntityKeyMember
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x00002050 File Offset: 0x00000250
		public EntityKeyMember()
		{
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000602D File Offset: 0x0000422D
		public EntityKeyMember(string keyName, object keyValue)
		{
			EntityUtil.CheckArgumentNull<string>(keyName, "keyName");
			EntityUtil.CheckArgumentNull<object>(keyValue, "keyValue");
			this._keyName = keyName;
			this._keyValue = keyValue;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000605B File Offset: 0x0000425B
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00006063 File Offset: 0x00004263
		[DataMember]
		public string Key
		{
			get
			{
				return this._keyName;
			}
			set
			{
				this.ValidateWritable(this._keyName);
				EntityUtil.CheckArgumentNull<string>(value, "value");
				this._keyName = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00006084 File Offset: 0x00004284
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000608C File Offset: 0x0000428C
		[DataMember]
		public object Value
		{
			get
			{
				return this._keyValue;
			}
			set
			{
				this.ValidateWritable(this._keyValue);
				EntityUtil.CheckArgumentNull<object>(value, "value");
				this._keyValue = value;
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000060AD File Offset: 0x000042AD
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "[{0}, {1}]", new object[]
			{
				this._keyName,
				this._keyValue
			});
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000060D6 File Offset: 0x000042D6
		private void ValidateWritable(object instance)
		{
			if (instance != null)
			{
				throw EntityUtil.CannotChangeEntityKey();
			}
		}

		// Token: 0x040000A2 RID: 162
		private string _keyName;

		// Token: 0x040000A3 RID: 163
		private object _keyValue;
	}
}
