using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007F0 RID: 2032
	internal sealed class ValueFixup
	{
		// Token: 0x060047C1 RID: 18369 RVA: 0x000F5FEB File Offset: 0x000F4FEB
		internal ValueFixup(Array arrayObj, int[] indexMap)
		{
			this.valueFixupEnum = ValueFixupEnum.Array;
			this.arrayObj = arrayObj;
			this.indexMap = indexMap;
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x000F6008 File Offset: 0x000F5008
		internal ValueFixup(object memberObject, string memberName, ReadObjectInfo objectInfo)
		{
			this.valueFixupEnum = ValueFixupEnum.Member;
			this.memberObject = memberObject;
			this.memberName = memberName;
			this.objectInfo = objectInfo;
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x000F602C File Offset: 0x000F502C
		internal void Fixup(ParseRecord record, ParseRecord parent)
		{
			object prnewObj = record.PRnewObj;
			switch (this.valueFixupEnum)
			{
			case ValueFixupEnum.Array:
				this.arrayObj.SetValue(prnewObj, this.indexMap);
				return;
			case ValueFixupEnum.Header:
			{
				Type typeFromHandle = typeof(Header);
				if (ValueFixup.valueInfo == null)
				{
					MemberInfo[] member = typeFromHandle.GetMember("Value");
					if (member.Length != 1)
					{
						throw new SerializationException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Serialization_HeaderReflection"), new object[]
						{
							member.Length
						}));
					}
					ValueFixup.valueInfo = member[0];
				}
				FormatterServices.SerializationSetValue(ValueFixup.valueInfo, this.header, prnewObj);
				return;
			}
			case ValueFixupEnum.Member:
			{
				if (this.objectInfo.isSi)
				{
					this.objectInfo.objectManager.RecordDelayedFixup(parent.PRobjectId, this.memberName, record.PRobjectId);
					return;
				}
				MemberInfo memberInfo = this.objectInfo.GetMemberInfo(this.memberName);
				if (memberInfo != null)
				{
					this.objectInfo.objectManager.RecordFixup(parent.PRobjectId, memberInfo, record.PRobjectId);
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x0400248F RID: 9359
		internal ValueFixupEnum valueFixupEnum;

		// Token: 0x04002490 RID: 9360
		internal Array arrayObj;

		// Token: 0x04002491 RID: 9361
		internal int[] indexMap;

		// Token: 0x04002492 RID: 9362
		internal object header;

		// Token: 0x04002493 RID: 9363
		internal object memberObject;

		// Token: 0x04002494 RID: 9364
		internal static MemberInfo valueInfo;

		// Token: 0x04002495 RID: 9365
		internal ReadObjectInfo objectInfo;

		// Token: 0x04002496 RID: 9366
		internal string memberName;
	}
}
