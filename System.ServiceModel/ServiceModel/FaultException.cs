using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel
{
	// Token: 0x02000109 RID: 265
	[KnownType(typeof(FaultException.FaultCodeData))]
	[KnownType(typeof(FaultException.FaultCodeData[]))]
	[KnownType(typeof(FaultException.FaultReasonData))]
	[KnownType(typeof(FaultException.FaultReasonData[]))]
	[__DynamicallyInvokable]
	[Serializable]
	public class FaultException : CommunicationException
	{
		// Token: 0x060005F6 RID: 1526 RVA: 0x0001AD8C File Offset: 0x00018F8C
		[__DynamicallyInvokable]
		public FaultException() : base(SR.GetString("SFxFaultReason"))
		{
			this.code = FaultException.DefaultCode;
			this.reason = FaultException.DefaultReason;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001ADB4 File Offset: 0x00018FB4
		public FaultException(string reason) : base(reason)
		{
			this.code = FaultException.DefaultCode;
			this.reason = FaultException.CreateReason(reason);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001ADD4 File Offset: 0x00018FD4
		public FaultException(FaultReason reason) : base(FaultException.GetSafeReasonText(reason))
		{
			this.code = FaultException.DefaultCode;
			this.reason = FaultException.EnsureReason(reason);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001ADF9 File Offset: 0x00018FF9
		public FaultException(string reason, FaultCode code) : base(reason)
		{
			this.code = FaultException.EnsureCode(code);
			this.reason = FaultException.CreateReason(reason);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001AE1A File Offset: 0x0001901A
		public FaultException(FaultReason reason, FaultCode code) : base(FaultException.GetSafeReasonText(reason))
		{
			this.code = FaultException.EnsureCode(code);
			this.reason = FaultException.EnsureReason(reason);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001AE40 File Offset: 0x00019040
		public FaultException(string reason, FaultCode code, string action) : base(reason)
		{
			this.code = FaultException.EnsureCode(code);
			this.reason = FaultException.CreateReason(reason);
			this.action = action;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001AE68 File Offset: 0x00019068
		internal FaultException(string reason, FaultCode code, string action, Exception innerException) : base(reason, innerException)
		{
			this.code = FaultException.EnsureCode(code);
			this.reason = FaultException.CreateReason(reason);
			this.action = action;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001AE92 File Offset: 0x00019092
		[__DynamicallyInvokable]
		public FaultException(FaultReason reason, FaultCode code, string action) : base(FaultException.GetSafeReasonText(reason))
		{
			this.code = FaultException.EnsureCode(code);
			this.reason = FaultException.EnsureReason(reason);
			this.action = action;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001AEBF File Offset: 0x000190BF
		internal FaultException(FaultReason reason, FaultCode code, string action, Exception innerException) : base(FaultException.GetSafeReasonText(reason), innerException)
		{
			this.code = FaultException.EnsureCode(code);
			this.reason = FaultException.EnsureReason(reason);
			this.action = action;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001AEF0 File Offset: 0x000190F0
		public FaultException(MessageFault fault) : base(FaultException.GetSafeReasonText(FaultException.GetReason(fault)))
		{
			if (fault == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("fault");
			}
			this.code = FaultException.EnsureCode(fault.Code);
			this.reason = FaultException.EnsureReason(fault.Reason);
			this.fault = fault;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001AF4C File Offset: 0x0001914C
		[__DynamicallyInvokable]
		public FaultException(MessageFault fault, string action) : base(FaultException.GetSafeReasonText(FaultException.GetReason(fault)))
		{
			if (fault == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("fault");
			}
			this.code = fault.Code;
			this.reason = fault.Reason;
			this.fault = fault;
			this.action = action;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001AFA4 File Offset: 0x000191A4
		protected FaultException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.code = this.ReconstructFaultCode(info, "code");
			this.reason = this.ReconstructFaultReason(info, "reason");
			this.fault = (MessageFault)info.GetValue("messageFault", typeof(MessageFault));
			this.action = info.GetString("action");
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0001B00E File Offset: 0x0001920E
		[__DynamicallyInvokable]
		public string Action
		{
			[__DynamicallyInvokable]
			get
			{
				return this.action;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0001B016 File Offset: 0x00019216
		[__DynamicallyInvokable]
		public FaultCode Code
		{
			[__DynamicallyInvokable]
			get
			{
				return this.code;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0001B01E File Offset: 0x0001921E
		private static FaultReason DefaultReason
		{
			get
			{
				return new FaultReason(SR.GetString("SFxFaultReason"));
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0001B02F File Offset: 0x0001922F
		private static FaultCode DefaultCode
		{
			get
			{
				return new FaultCode("Sender");
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0001B03B File Offset: 0x0001923B
		[__DynamicallyInvokable]
		public override string Message
		{
			[__DynamicallyInvokable]
			get
			{
				return FaultException.GetSafeReasonText(this.Reason);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0001B048 File Offset: 0x00019248
		[__DynamicallyInvokable]
		public FaultReason Reason
		{
			[__DynamicallyInvokable]
			get
			{
				return this.reason;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0001B050 File Offset: 0x00019250
		internal MessageFault Fault
		{
			get
			{
				return this.fault;
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001B058 File Offset: 0x00019258
		internal void AddFaultCodeObjectData(SerializationInfo info, string key, FaultCode code)
		{
			info.AddValue(key, FaultException.FaultCodeData.GetObjectData(code));
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001B067 File Offset: 0x00019267
		internal void AddFaultReasonObjectData(SerializationInfo info, string key, FaultReason reason)
		{
			info.AddValue(key, FaultException.FaultReasonData.GetObjectData(reason));
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001B076 File Offset: 0x00019276
		private static FaultCode CreateCode(string code)
		{
			if (code == null)
			{
				return FaultException.DefaultCode;
			}
			return new FaultCode(code);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001B087 File Offset: 0x00019287
		[__DynamicallyInvokable]
		public static FaultException CreateFault(MessageFault messageFault, params Type[] faultDetailTypes)
		{
			return FaultException.CreateFault(messageFault, null, faultDetailTypes);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001B094 File Offset: 0x00019294
		[__DynamicallyInvokable]
		public static FaultException CreateFault(MessageFault messageFault, string action, params Type[] faultDetailTypes)
		{
			if (messageFault == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageFault");
			}
			if (faultDetailTypes == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("faultDetailTypes");
			}
			DataContractSerializerFaultFormatter dataContractSerializerFaultFormatter = new DataContractSerializerFaultFormatter(faultDetailTypes);
			return dataContractSerializerFaultFormatter.Deserialize(messageFault, action);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001B0D6 File Offset: 0x000192D6
		[__DynamicallyInvokable]
		public virtual MessageFault CreateMessageFault()
		{
			if (this.fault != null)
			{
				return this.fault;
			}
			return MessageFault.CreateFault(this.code, this.reason);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001B0F8 File Offset: 0x000192F8
		private static FaultReason CreateReason(string reason)
		{
			if (reason == null)
			{
				return FaultException.DefaultReason;
			}
			return new FaultReason(reason);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001B10C File Offset: 0x0001930C
		[SecurityCritical]
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			this.AddFaultCodeObjectData(info, "code", this.code);
			this.AddFaultReasonObjectData(info, "reason", this.reason);
			info.AddValue("messageFault", this.fault);
			info.AddValue("action", this.action);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001B167 File Offset: 0x00019367
		private static FaultReason GetReason(MessageFault fault)
		{
			if (fault == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("fault");
			}
			return fault.Reason;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001B182 File Offset: 0x00019382
		internal static string GetSafeReasonText(MessageFault messageFault)
		{
			if (messageFault == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageFault");
			}
			return FaultException.GetSafeReasonText(messageFault.Reason);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001B1A4 File Offset: 0x000193A4
		internal static string GetSafeReasonText(FaultReason reason)
		{
			if (reason == null)
			{
				return SR.GetString("SFxUnknownFaultNullReason0");
			}
			string result;
			try
			{
				result = reason.GetMatchingTranslation(CultureInfo.CurrentCulture).Text;
			}
			catch (ArgumentException)
			{
				if (reason.Translations.Count == 0)
				{
					result = SR.GetString("SFxUnknownFaultZeroReasons0");
				}
				else
				{
					result = SR.GetString("SFxUnknownFaultNoMatchingTranslation1", new object[]
					{
						reason.Translations[0].Text
					});
				}
			}
			return result;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001B228 File Offset: 0x00019428
		private static FaultCode EnsureCode(FaultCode code)
		{
			if (code == null)
			{
				return FaultException.DefaultCode;
			}
			return code;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001B234 File Offset: 0x00019434
		private static FaultReason EnsureReason(FaultReason reason)
		{
			if (reason == null)
			{
				return FaultException.DefaultReason;
			}
			return reason;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0001B240 File Offset: 0x00019440
		internal FaultCode ReconstructFaultCode(SerializationInfo info, string key)
		{
			FaultException.FaultCodeData[] nodes = (FaultException.FaultCodeData[])info.GetValue(key, typeof(FaultException.FaultCodeData[]));
			return FaultException.FaultCodeData.Construct(nodes);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001B26C File Offset: 0x0001946C
		internal FaultReason ReconstructFaultReason(SerializationInfo info, string key)
		{
			FaultException.FaultReasonData[] nodes = (FaultException.FaultReasonData[])info.GetValue(key, typeof(FaultException.FaultReasonData[]));
			return FaultException.FaultReasonData.Construct(nodes);
		}

		// Token: 0x04000A5E RID: 2654
		internal const string Namespace = "http://schemas.xmlsoap.org/Microsoft/WindowsCommunicationFoundation/2005/08/Faults/";

		// Token: 0x04000A5F RID: 2655
		private string action;

		// Token: 0x04000A60 RID: 2656
		private FaultCode code;

		// Token: 0x04000A61 RID: 2657
		private FaultReason reason;

		// Token: 0x04000A62 RID: 2658
		private MessageFault fault;

		// Token: 0x02000AE1 RID: 2785
		[Serializable]
		internal class FaultCodeData
		{
			// Token: 0x06006EAD RID: 28333 RVA: 0x0019C6F0 File Offset: 0x0019A8F0
			internal static FaultCode Construct(FaultException.FaultCodeData[] nodes)
			{
				FaultCode faultCode = null;
				for (int i = nodes.Length - 1; i >= 0; i--)
				{
					faultCode = new FaultCode(nodes[i].name, nodes[i].ns, faultCode);
				}
				return faultCode;
			}

			// Token: 0x06006EAE RID: 28334 RVA: 0x0019C728 File Offset: 0x0019A928
			internal static FaultException.FaultCodeData[] GetObjectData(FaultCode code)
			{
				FaultException.FaultCodeData[] array = new FaultException.FaultCodeData[FaultException.FaultCodeData.GetDepth(code)];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new FaultException.FaultCodeData();
					array[i].name = code.Name;
					array[i].ns = code.Namespace;
					code = code.SubCode;
				}
				return array;
			}

			// Token: 0x06006EAF RID: 28335 RVA: 0x0019C780 File Offset: 0x0019A980
			private static int GetDepth(FaultCode code)
			{
				int num = 0;
				while (code != null)
				{
					num++;
					code = code.SubCode;
				}
				return num;
			}

			// Token: 0x04003F25 RID: 16165
			private string name;

			// Token: 0x04003F26 RID: 16166
			private string ns;
		}

		// Token: 0x02000AE2 RID: 2786
		[Serializable]
		internal class FaultReasonData
		{
			// Token: 0x06006EB1 RID: 28337 RVA: 0x0019C7AC File Offset: 0x0019A9AC
			internal static FaultReason Construct(FaultException.FaultReasonData[] nodes)
			{
				FaultReasonText[] array = new FaultReasonText[nodes.Length];
				for (int i = 0; i < nodes.Length; i++)
				{
					array[i] = new FaultReasonText(nodes[i].text, nodes[i].xmlLang);
				}
				return new FaultReason(array);
			}

			// Token: 0x06006EB2 RID: 28338 RVA: 0x0019C7F0 File Offset: 0x0019A9F0
			internal static FaultException.FaultReasonData[] GetObjectData(FaultReason reason)
			{
				SynchronizedReadOnlyCollection<FaultReasonText> translations = reason.Translations;
				FaultException.FaultReasonData[] array = new FaultException.FaultReasonData[translations.Count];
				for (int i = 0; i < translations.Count; i++)
				{
					array[i] = new FaultException.FaultReasonData();
					array[i].xmlLang = translations[i].XmlLang;
					array[i].text = translations[i].Text;
				}
				return array;
			}

			// Token: 0x04003F27 RID: 16167
			private string xmlLang;

			// Token: 0x04003F28 RID: 16168
			private string text;
		}
	}
}
