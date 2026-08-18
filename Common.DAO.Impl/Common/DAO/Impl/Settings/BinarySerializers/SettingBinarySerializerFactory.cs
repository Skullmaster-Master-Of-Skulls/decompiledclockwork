using System;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Settings.Adapters;

namespace TechnoPro.Common.DAO.Impl.Settings.BinarySerializers
{
	// Token: 0x0200004E RID: 78
	public abstract class SettingBinarySerializerFactory
	{
		// Token: 0x0600020E RID: 526 RVA: 0x000125F8 File Offset: 0x000107F8
		public static ISettingBinarySerializer GetBinarySerializer(Setting setting)
		{
			SettingSemantic semanticType = setting.GetSettingAttribute().SemanticType;
			Type binarySerializerType = SettingBinarySerializerFactory.GetBinarySerializerType(semanticType);
			return (ISettingBinarySerializer)Activator.CreateInstance(binarySerializerType);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00012628 File Offset: 0x00010828
		private static Type GetBinarySerializerType(SettingSemantic settingSemantic)
		{
			switch (settingSemantic)
			{
			case SettingSemantic.INTEGER:
				return typeof(IntBinarySerializer);
			case SettingSemantic.XML:
			case SettingSemantic.HTML:
			case SettingSemantic.TEXT:
			case SettingSemantic.EMAIL_TEMPLATE:
			case SettingSemantic.CHANNELS:
			case SettingSemantic.SCHEDULE_TYPES:
			case SettingSemantic.ASSETS:
			case SettingSemantic.TESTRULES:
			case SettingSemantic.ROOMS:
			case SettingSemantic.SPECIALACCOMMODATIONS:
			case SettingSemantic.PASSWORD:
			case SettingSemantic.CSHARPCODE:
			case SettingSemantic.LOGINAUTHENTICATIONMETHODS:
			case SettingSemantic.CLOCKWORKSYNCUSERS:
			case SettingSemantic.CUTOFFTIME:
			case SettingSemantic.CONTROLID_PERSTUDENT:
				return typeof(StringBinarySerializer);
			case SettingSemantic.IMAGE:
				return typeof(ImageBinarySerializer);
			case SettingSemantic.REFERENCE_ARRAY:
				return typeof(ReferenceArrayBinarySerializer);
			case SettingSemantic.COLOR:
				return typeof(ColorBinarySerializer);
			case SettingSemantic.DATETIME:
				return typeof(DatetimeBinarySerializer);
			case SettingSemantic.BOOLEAN:
				return typeof(BooleanBinarySerializer);
			}
			return typeof(StringBinarySerializer);
		}
	}
}
