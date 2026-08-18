using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009A7 RID: 2471
	internal class ActionHeader : AddressingHeader
	{
		// Token: 0x060060E3 RID: 24803 RVA: 0x00169F95 File Offset: 0x00168195
		private ActionHeader(string action, AddressingVersion version) : base(version)
		{
			this.action = action;
		}

		// Token: 0x1700174A RID: 5962
		// (get) Token: 0x060060E4 RID: 24804 RVA: 0x00169FA5 File Offset: 0x001681A5
		public string Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x1700174B RID: 5963
		// (get) Token: 0x060060E5 RID: 24805 RVA: 0x00169FAD File Offset: 0x001681AD
		public override bool MustUnderstand
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700174C RID: 5964
		// (get) Token: 0x060060E6 RID: 24806 RVA: 0x00169FB0 File Offset: 0x001681B0
		public override XmlDictionaryString DictionaryName
		{
			get
			{
				return XD.AddressingDictionary.Action;
			}
		}

		// Token: 0x060060E7 RID: 24807 RVA: 0x00169FBC File Offset: 0x001681BC
		public static ActionHeader Create(string action, AddressingVersion addressingVersion)
		{
			if (action == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("action"));
			}
			if (addressingVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("addressingVersion");
			}
			return new ActionHeader(action, addressingVersion);
		}

		// Token: 0x060060E8 RID: 24808 RVA: 0x00169FF0 File Offset: 0x001681F0
		public static ActionHeader Create(XmlDictionaryString dictionaryAction, AddressingVersion addressingVersion)
		{
			if (dictionaryAction == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("action"));
			}
			if (addressingVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("addressingVersion");
			}
			return new ActionHeader.DictionaryActionHeader(dictionaryAction, addressingVersion);
		}

		// Token: 0x060060E9 RID: 24809 RVA: 0x0016A024 File Offset: 0x00168224
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			writer.WriteString(this.action);
		}

		// Token: 0x060060EA RID: 24810 RVA: 0x0016A034 File Offset: 0x00168234
		public static string ReadHeaderValue(XmlDictionaryReader reader, AddressingVersion addressingVersion)
		{
			string text = reader.ReadElementContentAsString();
			if (text.Length > 0 && (text[0] <= ' ' || text[text.Length - 1] <= ' '))
			{
				text = XmlUtil.Trim(text);
			}
			return text;
		}

		// Token: 0x060060EB RID: 24811 RVA: 0x0016A078 File Offset: 0x00168278
		public static ActionHeader ReadHeader(XmlDictionaryReader reader, AddressingVersion version, string actor, bool mustUnderstand, bool relay)
		{
			string text = ActionHeader.ReadHeaderValue(reader, version);
			if (actor.Length == 0 && mustUnderstand && !relay)
			{
				return new ActionHeader(text, version);
			}
			return new ActionHeader.FullActionHeader(text, actor, mustUnderstand, relay, version);
		}

		// Token: 0x040038AC RID: 14508
		private string action;

		// Token: 0x040038AD RID: 14509
		private const bool mustUnderstandValue = true;

		// Token: 0x02000E33 RID: 3635
		private class DictionaryActionHeader : ActionHeader
		{
			// Token: 0x06008289 RID: 33417 RVA: 0x001E3030 File Offset: 0x001E1230
			public DictionaryActionHeader(XmlDictionaryString dictionaryAction, AddressingVersion version) : base(dictionaryAction.Value, version)
			{
				this.dictionaryAction = dictionaryAction;
			}

			// Token: 0x0600828A RID: 33418 RVA: 0x001E3046 File Offset: 0x001E1246
			protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
			{
				writer.WriteString(this.dictionaryAction);
			}

			// Token: 0x04004A1A RID: 18970
			private XmlDictionaryString dictionaryAction;
		}

		// Token: 0x02000E34 RID: 3636
		private class FullActionHeader : ActionHeader
		{
			// Token: 0x0600828B RID: 33419 RVA: 0x001E3054 File Offset: 0x001E1254
			public FullActionHeader(string action, string actor, bool mustUnderstand, bool relay, AddressingVersion version) : base(action, version)
			{
				this.actor = actor;
				this.mustUnderstand = mustUnderstand;
				this.relay = relay;
			}

			// Token: 0x17001CC9 RID: 7369
			// (get) Token: 0x0600828C RID: 33420 RVA: 0x001E3075 File Offset: 0x001E1275
			public override string Actor
			{
				get
				{
					return this.actor;
				}
			}

			// Token: 0x17001CCA RID: 7370
			// (get) Token: 0x0600828D RID: 33421 RVA: 0x001E307D File Offset: 0x001E127D
			public override bool MustUnderstand
			{
				get
				{
					return this.mustUnderstand;
				}
			}

			// Token: 0x17001CCB RID: 7371
			// (get) Token: 0x0600828E RID: 33422 RVA: 0x001E3085 File Offset: 0x001E1285
			public override bool Relay
			{
				get
				{
					return this.relay;
				}
			}

			// Token: 0x04004A1B RID: 18971
			private string actor;

			// Token: 0x04004A1C RID: 18972
			private bool mustUnderstand;

			// Token: 0x04004A1D RID: 18973
			private bool relay;
		}
	}
}
