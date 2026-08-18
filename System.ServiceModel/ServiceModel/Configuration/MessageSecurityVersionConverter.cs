using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200063F RID: 1599
	internal class MessageSecurityVersionConverter : TypeConverter
	{
		// Token: 0x06003D8D RID: 15757 RVA: 0x000EAEAF File Offset: 0x000E90AF
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return typeof(string) == sourceType || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06003D8E RID: 15758 RVA: 0x000EAECD File Offset: 0x000E90CD
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x000EAEEC File Offset: 0x000E90EC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = (string)value;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 433860734U)
				{
					if (num != 40128293U)
					{
						if (num != 387479717U)
						{
							if (num == 433860734U)
							{
								if (text == "Default")
								{
									return MessageSecurityVersion.Default;
								}
							}
						}
						else if (text == "WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10")
						{
							return MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;
						}
					}
					else if (text == "WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11")
					{
						return MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11;
					}
				}
				else if (num <= 2676831400U)
				{
					if (num != 1222412740U)
					{
						if (num == 2676831400U)
						{
							if (text == "WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10")
							{
								return MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;
							}
						}
					}
					else if (text == "WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12")
					{
						return MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12;
					}
				}
				else if (num != 3439502152U)
				{
					if (num == 3501460461U)
					{
						if (text == "WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10")
						{
							return MessageSecurityVersion.WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10;
						}
					}
				}
				else if (text == "WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10")
				{
					return MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ConfigInvalidClassFactoryValue", new object[]
				{
					text,
					typeof(MessageSecurityVersion).FullName
				})));
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x000EB05C File Offset: 0x000E925C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (typeof(string) == destinationType && value is MessageSecurityVersion)
			{
				MessageSecurityVersion messageSecurityVersion = (MessageSecurityVersion)value;
				string result;
				if (messageSecurityVersion == MessageSecurityVersion.Default)
				{
					result = "Default";
				}
				else if (messageSecurityVersion == MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11)
				{
					result = "WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11";
				}
				else if (messageSecurityVersion == MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10)
				{
					result = "WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10";
				}
				else if (messageSecurityVersion == MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10)
				{
					result = "WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10";
				}
				else if (messageSecurityVersion == MessageSecurityVersion.WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10)
				{
					result = "WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10";
				}
				else if (messageSecurityVersion == MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12)
				{
					result = "WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12";
				}
				else
				{
					if (messageSecurityVersion != MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ConfigInvalidClassInstanceValue", new object[]
						{
							typeof(MessageSecurityVersion).FullName
						})));
					}
					result = "WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10";
				}
				return result;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
