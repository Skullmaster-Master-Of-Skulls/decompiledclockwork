using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Text;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000516 RID: 1302
	internal class LoadMessageLogger
	{
		// Token: 0x0600310B RID: 12555 RVA: 0x000EAAB4 File Offset: 0x000E8CB4
		internal LoadMessageLogger(Action<string> logLoadMessage)
		{
			this._logLoadMessage = logLoadMessage;
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000EAACE File Offset: 0x000E8CCE
		internal virtual void LogLoadMessage(string message, EdmType relatedType)
		{
			if (this._logLoadMessage != null)
			{
				this._logLoadMessage(message);
			}
			this.LogMessagesWithTypeInfo(message, relatedType);
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x000EAAEC File Offset: 0x000E8CEC
		internal virtual string CreateErrorMessageWithTypeSpecificLoadLogs(string errorMessage, EdmType relatedType)
		{
			return new StringBuilder(errorMessage).AppendLine(this.GetTypeRelatedLogMessage(relatedType)).ToString();
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x000EAB08 File Offset: 0x000E8D08
		private string GetTypeRelatedLogMessage(EdmType relatedType)
		{
			if (this._messages.ContainsKey(relatedType))
			{
				return new StringBuilder().AppendLine().AppendLine(Strings.ExtraInfo).AppendLine(this._messages[relatedType].ToString()).ToString();
			}
			return string.Empty;
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x000EAB58 File Offset: 0x000E8D58
		private void LogMessagesWithTypeInfo(string message, EdmType relatedType)
		{
			if (this._messages.ContainsKey(relatedType))
			{
				this._messages[relatedType].AppendLine(message);
				return;
			}
			this._messages.Add(relatedType, new StringBuilder(message));
		}

		// Token: 0x0400128A RID: 4746
		private readonly Action<string> _logLoadMessage;

		// Token: 0x0400128B RID: 4747
		private readonly Dictionary<EdmType, StringBuilder> _messages = new Dictionary<EdmType, StringBuilder>();
	}
}
