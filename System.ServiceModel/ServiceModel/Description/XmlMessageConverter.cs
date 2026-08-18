using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x0200043E RID: 1086
	internal class XmlMessageConverter : TypedMessageConverter
	{
		// Token: 0x06002A74 RID: 10868 RVA: 0x000A414A File Offset: 0x000A234A
		internal XmlMessageConverter(OperationFormatter formatter)
		{
			this.formatter = formatter;
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06002A75 RID: 10869 RVA: 0x000A4159 File Offset: 0x000A2359
		internal string Action
		{
			get
			{
				return this.formatter.RequestAction;
			}
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000A4166 File Offset: 0x000A2366
		public override Message ToMessage(object typedMessage)
		{
			return this.ToMessage(typedMessage, MessageVersion.Soap12WSAddressing10);
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000A4174 File Offset: 0x000A2374
		public override Message ToMessage(object typedMessage, MessageVersion version)
		{
			if (typedMessage == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("typedMessage"));
			}
			return this.formatter.SerializeRequest(version, new object[]
			{
				typedMessage
			});
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000A41A4 File Offset: 0x000A23A4
		public override object FromMessage(Message message)
		{
			if (this.Action != null && message.Headers.Action != null && message.Headers.Action != this.Action)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxActionMismatch", new object[]
				{
					this.Action,
					message.Headers.Action
				})));
			}
			object[] array = new object[1];
			this.formatter.DeserializeRequest(message, array);
			return array[0];
		}

		// Token: 0x040022D1 RID: 8913
		private OperationFormatter formatter;
	}
}
