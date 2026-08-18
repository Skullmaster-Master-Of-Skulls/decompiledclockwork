using System;
using System.Linq;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002D9 RID: 729
	internal sealed class ResponseObjectsPropertyDefinition : PropertyDefinition
	{
		// Token: 0x060019D5 RID: 6613 RVA: 0x00046350 File Offset: 0x00045350
		internal ResponseObjectsPropertyDefinition(string xmlElementName, string uri, ExchangeVersion version) : base(xmlElementName, uri, version)
		{
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x0004635C File Offset: 0x0004535C
		internal sealed override void LoadPropertyValueFromXml(EwsServiceXmlReader reader, PropertyBag propertyBag)
		{
			ResponseActions responseActions = ResponseActions.None;
			reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, base.XmlElementName);
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.IsStartElement())
					{
						responseActions |= ResponseObjectsPropertyDefinition.GetResponseAction(reader.LocalName);
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Types, base.XmlElementName));
			}
			propertyBag[this] = responseActions;
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x000463B8 File Offset: 0x000453B8
		internal override void LoadPropertyValueFromJson(object value, ExchangeService service, PropertyBag propertyBag)
		{
			ResponseActions responseActions = ResponseActions.None;
			object[] array = value as object[];
			if (array != null)
			{
				foreach (JsonObject jsonObject in array.OfType<JsonObject>())
				{
					if (jsonObject.HasTypeProperty())
					{
						string text = jsonObject.ReadTypeString();
						if (!string.IsNullOrEmpty(text))
						{
							responseActions |= ResponseObjectsPropertyDefinition.GetResponseAction(text);
						}
					}
				}
			}
			propertyBag[this] = responseActions;
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x0004643C File Offset: 0x0004543C
		private static ResponseActions GetResponseAction(string responseActionString)
		{
			ResponseActions result = ResponseActions.None;
			switch (responseActionString)
			{
			case "AcceptItem":
				result = ResponseActions.Accept;
				break;
			case "TentativelyAcceptItem":
				result = ResponseActions.TentativelyAccept;
				break;
			case "DeclineItem":
				result = ResponseActions.Decline;
				break;
			case "ReplyToItem":
				result = ResponseActions.Reply;
				break;
			case "ForwardItem":
				result = ResponseActions.Forward;
				break;
			case "ReplyAllToItem":
				result = ResponseActions.ReplyAll;
				break;
			case "CancelCalendarItem":
				result = ResponseActions.Cancel;
				break;
			case "RemoveItem":
				result = ResponseActions.RemoveFromCalendar;
				break;
			case "SuppressReadReceipt":
				result = ResponseActions.SuppressReadReceipt;
				break;
			case "PostReplyItem":
				result = ResponseActions.PostReply;
				break;
			}
			return result;
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x0004655D File Offset: 0x0004555D
		internal override void WritePropertyValueToXml(EwsServiceXmlWriter writer, PropertyBag propertyBag, bool isUpdateOperation)
		{
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x0004655F File Offset: 0x0004555F
		internal override void WriteJsonValue(JsonObject jsonObject, PropertyBag propertyBag, ExchangeService service, bool isUpdateOperation)
		{
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x00046561 File Offset: 0x00045561
		internal override bool IsNullable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060019DC RID: 6620 RVA: 0x00046564 File Offset: 0x00045564
		public override Type Type
		{
			get
			{
				return typeof(ResponseActions);
			}
		}
	}
}
