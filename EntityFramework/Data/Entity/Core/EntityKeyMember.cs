using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x0200034E RID: 846
	[DataContract]
	[Serializable]
	public class EntityKeyMember
	{
		// Token: 0x06001E3B RID: 7739 RVA: 0x00091BCC File Offset: 0x0008FDCC
		public EntityKeyMember()
		{
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x00091BD4 File Offset: 0x0008FDD4
		public EntityKeyMember(string keyName, object keyValue)
		{
			Check.NotNull<string>(keyName, "keyName");
			Check.NotNull<object>(keyValue, "keyValue");
			this._keyName = keyName;
			this._keyValue = keyValue;
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001E3D RID: 7741 RVA: 0x00091C02 File Offset: 0x0008FE02
		// (set) Token: 0x06001E3E RID: 7742 RVA: 0x00091C0A File Offset: 0x0008FE0A
		[DataMember]
		public string Key
		{
			get
			{
				return this._keyName;
			}
			set
			{
				Check.NotNull<string>(value, "value");
				EntityKeyMember.ValidateWritable(this._keyName);
				this._keyName = value;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001E3F RID: 7743 RVA: 0x00091C2A File Offset: 0x0008FE2A
		// (set) Token: 0x06001E40 RID: 7744 RVA: 0x00091C32 File Offset: 0x0008FE32
		[DataMember]
		public object Value
		{
			get
			{
				return this._keyValue;
			}
			set
			{
				Check.NotNull<object>(value, "value");
				EntityKeyMember.ValidateWritable(this._keyValue);
				this._keyValue = value;
			}
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x00091C54 File Offset: 0x0008FE54
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "[{0}, {1}]", new object[]
			{
				this._keyName,
				this._keyValue
			});
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x00091C8A File Offset: 0x0008FE8A
		private static void ValidateWritable(object instance)
		{
			if (instance != null)
			{
				throw new InvalidOperationException(Strings.EntityKey_CannotChangeKey);
			}
		}

		// Token: 0x04000A54 RID: 2644
		private string _keyName;

		// Token: 0x04000A55 RID: 2645
		private object _keyValue;
	}
}
