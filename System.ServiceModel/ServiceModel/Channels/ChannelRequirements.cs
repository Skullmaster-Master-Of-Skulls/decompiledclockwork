using System;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel.Description;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000998 RID: 2456
	internal struct ChannelRequirements
	{
		// Token: 0x06005FDF RID: 24543 RVA: 0x001657F0 File Offset: 0x001639F0
		public static void ComputeContractRequirements(ContractDescription contractDescription, out ChannelRequirements requirements)
		{
			requirements = default(ChannelRequirements);
			requirements.usesInput = false;
			requirements.usesReply = false;
			requirements.usesOutput = false;
			requirements.usesRequest = false;
			requirements.sessionMode = contractDescription.SessionMode;
			for (int i = 0; i < contractDescription.Operations.Count; i++)
			{
				OperationDescription operationDescription = contractDescription.Operations[i];
				bool isOneWay = operationDescription.IsOneWay;
				if (!operationDescription.IsServerInitiated())
				{
					if (isOneWay)
					{
						requirements.usesInput = true;
					}
					else
					{
						requirements.usesReply = true;
					}
				}
				else if (isOneWay)
				{
					requirements.usesOutput = true;
				}
				else
				{
					requirements.usesRequest = true;
				}
			}
		}

		// Token: 0x06005FE0 RID: 24544 RVA: 0x00165888 File Offset: 0x00163A88
		public static Type[] ComputeRequiredChannels(ref ChannelRequirements requirements)
		{
			if (requirements.usesOutput || requirements.usesRequest)
			{
				switch (requirements.sessionMode)
				{
				case SessionMode.Allowed:
					return new Type[]
					{
						typeof(IDuplexChannel),
						typeof(IDuplexSessionChannel)
					};
				case SessionMode.Required:
					return new Type[]
					{
						typeof(IDuplexSessionChannel)
					};
				case SessionMode.NotAllowed:
					return new Type[]
					{
						typeof(IDuplexChannel)
					};
				}
			}
			else if (requirements.usesInput && requirements.usesReply)
			{
				switch (requirements.sessionMode)
				{
				case SessionMode.Allowed:
					return new Type[]
					{
						typeof(IRequestChannel),
						typeof(IRequestSessionChannel),
						typeof(IDuplexChannel),
						typeof(IDuplexSessionChannel)
					};
				case SessionMode.Required:
					return new Type[]
					{
						typeof(IRequestSessionChannel),
						typeof(IDuplexSessionChannel)
					};
				case SessionMode.NotAllowed:
					return new Type[]
					{
						typeof(IRequestChannel),
						typeof(IDuplexChannel)
					};
				}
			}
			else if (requirements.usesInput)
			{
				switch (requirements.sessionMode)
				{
				case SessionMode.Allowed:
					return new Type[]
					{
						typeof(IOutputChannel),
						typeof(IOutputSessionChannel),
						typeof(IRequestChannel),
						typeof(IRequestSessionChannel),
						typeof(IDuplexChannel),
						typeof(IDuplexSessionChannel)
					};
				case SessionMode.Required:
					return new Type[]
					{
						typeof(IOutputSessionChannel),
						typeof(IRequestSessionChannel),
						typeof(IDuplexSessionChannel)
					};
				case SessionMode.NotAllowed:
					return new Type[]
					{
						typeof(IOutputChannel),
						typeof(IRequestChannel),
						typeof(IDuplexChannel)
					};
				}
			}
			else if (requirements.usesReply)
			{
				switch (requirements.sessionMode)
				{
				case SessionMode.Allowed:
					return new Type[]
					{
						typeof(IRequestChannel),
						typeof(IRequestSessionChannel),
						typeof(IDuplexChannel),
						typeof(IDuplexSessionChannel)
					};
				case SessionMode.Required:
					return new Type[]
					{
						typeof(IRequestSessionChannel),
						typeof(IDuplexSessionChannel)
					};
				case SessionMode.NotAllowed:
					return new Type[]
					{
						typeof(IRequestChannel),
						typeof(IDuplexChannel)
					};
				}
			}
			else
			{
				switch (requirements.sessionMode)
				{
				case SessionMode.Allowed:
					return new Type[]
					{
						typeof(IOutputSessionChannel),
						typeof(IOutputChannel),
						typeof(IRequestSessionChannel),
						typeof(IRequestChannel),
						typeof(IDuplexChannel),
						typeof(IDuplexSessionChannel)
					};
				case SessionMode.Required:
					return new Type[]
					{
						typeof(IOutputSessionChannel),
						typeof(IRequestSessionChannel),
						typeof(IDuplexSessionChannel)
					};
				case SessionMode.NotAllowed:
					return new Type[]
					{
						typeof(IOutputChannel),
						typeof(IRequestChannel),
						typeof(IDuplexChannel)
					};
				}
			}
			return null;
		}

		// Token: 0x06005FE1 RID: 24545 RVA: 0x00165C10 File Offset: 0x00163E10
		public static bool IsSessionful(Type channelType)
		{
			return channelType == typeof(IDuplexSessionChannel) || channelType == typeof(IOutputSessionChannel) || channelType == typeof(IInputSessionChannel) || channelType == typeof(IReplySessionChannel) || channelType == typeof(IRequestSessionChannel);
		}

		// Token: 0x06005FE2 RID: 24546 RVA: 0x00165C78 File Offset: 0x00163E78
		public static bool IsOneWay(Type channelType)
		{
			return channelType == typeof(IOutputChannel) || channelType == typeof(IInputChannel) || channelType == typeof(IInputSessionChannel) || channelType == typeof(IOutputSessionChannel);
		}

		// Token: 0x06005FE3 RID: 24547 RVA: 0x00165CD0 File Offset: 0x00163ED0
		public static bool IsRequestReply(Type channelType)
		{
			return channelType == typeof(IRequestChannel) || channelType == typeof(IReplyChannel) || channelType == typeof(IReplySessionChannel) || channelType == typeof(IRequestSessionChannel);
		}

		// Token: 0x06005FE4 RID: 24548 RVA: 0x00165D25 File Offset: 0x00163F25
		public static bool IsDuplex(Type channelType)
		{
			return channelType == typeof(IDuplexChannel) || channelType == typeof(IDuplexSessionChannel);
		}

		// Token: 0x06005FE5 RID: 24549 RVA: 0x00165D4C File Offset: 0x00163F4C
		public static Exception CantCreateListenerException(IEnumerable<Type> supportedChannels, IEnumerable<Type> requiredChannels, string bindingName)
		{
			string text = "";
			string text2 = "";
			Exception ex = ChannelRequirements.BindingContractMismatchException(supportedChannels, requiredChannels, bindingName, ref text, ref text2);
			if (ex == null)
			{
				ex = new InvalidOperationException(SR.GetString("EndpointListenerRequirementsCannotBeMetBy3", new object[]
				{
					bindingName,
					text,
					text2
				}));
			}
			return ex;
		}

		// Token: 0x06005FE6 RID: 24550 RVA: 0x00165D98 File Offset: 0x00163F98
		public static Exception CantCreateChannelException(IEnumerable<Type> supportedChannels, IEnumerable<Type> requiredChannels, string bindingName)
		{
			string text = "";
			string text2 = "";
			Exception ex = ChannelRequirements.BindingContractMismatchException(supportedChannels, requiredChannels, bindingName, ref text, ref text2);
			if (ex == null)
			{
				ex = new InvalidOperationException(SR.GetString("CouldnTCreateChannelForType2", new object[]
				{
					bindingName,
					text
				}));
			}
			return ex;
		}

		// Token: 0x06005FE7 RID: 24551 RVA: 0x00165DE0 File Offset: 0x00163FE0
		public static Exception BindingContractMismatchException(IEnumerable<Type> supportedChannels, IEnumerable<Type> requiredChannels, string bindingName, ref string contractChannelTypesString, ref string bindingChannelTypesString)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			bool flag2 = true;
			bool flag3 = true;
			bool flag4 = true;
			bool flag5 = true;
			bool flag6 = true;
			foreach (Type type in requiredChannels)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(CultureInfo.CurrentCulture.TextInfo.ListSeparator);
					stringBuilder.Append(" ");
				}
				string text = type.ToString();
				stringBuilder.Append(text.Substring(text.LastIndexOf('.') + 1));
				if (!ChannelRequirements.IsOneWay(type))
				{
					flag = false;
				}
				if (!ChannelRequirements.IsRequestReply(type))
				{
					flag2 = false;
				}
				if (!ChannelRequirements.IsDuplex(type))
				{
					flag3 = false;
				}
				if (!ChannelRequirements.IsRequestReply(type) && !ChannelRequirements.IsDuplex(type))
				{
					flag4 = false;
				}
				if (!ChannelRequirements.IsSessionful(type))
				{
					flag5 = false;
				}
				else
				{
					flag6 = false;
				}
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			bool flag11 = false;
			bool flag12 = false;
			foreach (Type type2 in supportedChannels)
			{
				flag12 = true;
				if (stringBuilder2.Length > 0)
				{
					stringBuilder2.Append(CultureInfo.CurrentCulture.TextInfo.ListSeparator);
					stringBuilder2.Append(" ");
				}
				string text2 = type2.ToString();
				stringBuilder2.Append(text2.Substring(text2.LastIndexOf('.') + 1));
				if (ChannelRequirements.IsOneWay(type2))
				{
					flag7 = true;
				}
				if (ChannelRequirements.IsRequestReply(type2))
				{
					flag8 = true;
				}
				if (ChannelRequirements.IsDuplex(type2))
				{
					flag9 = true;
				}
				if (ChannelRequirements.IsSessionful(type2))
				{
					flag10 = true;
				}
				else
				{
					flag11 = true;
				}
			}
			bool flag13 = flag8 || flag9;
			if (!flag12)
			{
				return new InvalidOperationException(SR.GetString("BindingDoesnTSupportAnyChannelTypes1", new object[]
				{
					bindingName
				}));
			}
			if (flag5 && !flag10)
			{
				return new InvalidOperationException(SR.GetString("BindingDoesnTSupportSessionButContractRequires1", new object[]
				{
					bindingName
				}));
			}
			if (flag6 && !flag11)
			{
				return new InvalidOperationException(SR.GetString("BindingDoesntSupportDatagramButContractRequires", new object[]
				{
					bindingName
				}));
			}
			if (flag3 && !flag9)
			{
				return new InvalidOperationException(SR.GetString("BindingDoesnTSupportDuplexButContractRequires1", new object[]
				{
					bindingName
				}));
			}
			if (flag2 && !flag8)
			{
				return new InvalidOperationException(SR.GetString("BindingDoesnTSupportRequestReplyButContract1", new object[]
				{
					bindingName
				}));
			}
			if (flag && !flag7)
			{
				return new InvalidOperationException(SR.GetString("BindingDoesnTSupportOneWayButContractRequires1", new object[]
				{
					bindingName
				}));
			}
			if (flag4 && !flag13)
			{
				return new InvalidOperationException(SR.GetString("BindingDoesnTSupportTwoWayButContractRequires1", new object[]
				{
					bindingName
				}));
			}
			contractChannelTypesString = stringBuilder.ToString();
			bindingChannelTypesString = stringBuilder2.ToString();
			return null;
		}

		// Token: 0x0400385C RID: 14428
		public bool usesInput;

		// Token: 0x0400385D RID: 14429
		public bool usesReply;

		// Token: 0x0400385E RID: 14430
		public bool usesOutput;

		// Token: 0x0400385F RID: 14431
		public bool usesRequest;

		// Token: 0x04003860 RID: 14432
		public SessionMode sessionMode;
	}
}
