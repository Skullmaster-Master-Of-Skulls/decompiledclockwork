using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001B2 RID: 434
	internal class LoadMessageLogger
	{
		// Token: 0x06001ED7 RID: 7895 RVA: 0x0006CA78 File Offset: 0x0006AC78
		internal LoadMessageLogger(Action<string> logLoadMessage)
		{
			this._logLoadMessage = logLoadMessage;
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x0006CA92 File Offset: 0x0006AC92
		internal void LogLoadMessage(string message, EdmType relatedType)
		{
			if (this._logLoadMessage != null)
			{
				this._logLoadMessage(message);
			}
			this.LogMessagesWithTypeInfo(message, relatedType);
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x0006CAB0 File Offset: 0x0006ACB0
		internal string CreateErrorMessageWithTypeSpecificLoadLogs(string errorMessage, EdmType relatedType)
		{
			return new StringBuilder(errorMessage).AppendLine(this.GetTypeRelatedLogMessage(relatedType)).ToString();
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x0006CACC File Offset: 0x0006ACCC
		private string GetTypeRelatedLogMessage(EdmType relatedType)
		{
			if (this._messages.ContainsKey(relatedType))
			{
				return new StringBuilder().AppendLine().AppendLine(Strings.ExtraInfo).AppendLine(this._messages[relatedType].ToString()).ToString();
			}
			return string.Empty;
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x0006CB1C File Offset: 0x0006AD1C
		private void LogMessagesWithTypeInfo(string message, EdmType relatedType)
		{
			if (this._messages.ContainsKey(relatedType))
			{
				this._messages[relatedType].AppendLine(message);
				return;
			}
			this._messages.Add(relatedType, new StringBuilder(message));
		}

		// Token: 0x04000CEA RID: 3306
		private Action<string> _logLoadMessage;

		// Token: 0x04000CEB RID: 3307
		private Dictionary<EdmType, StringBuilder> _messages = new Dictionary<EdmType, StringBuilder>();
	}
}
