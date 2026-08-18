using System;
using System.Xml.Linq;
using TechnoPro.Common.Public.Entities.AlertTrigger;

namespace TechnoPro.Common.Converter.AlertTriggers
{
	// Token: 0x02000028 RID: 40
	public interface IAlertTriggerDefinitionSerializer<T> where T : AlertTriggerDefinitionBase
	{
		// Token: 0x060000D0 RID: 208
		XElement Serialize(T dataObj);

		// Token: 0x060000D1 RID: 209
		T DeSerialize(XElement element);

		// Token: 0x060000D2 RID: 210
		T[] DeSerializeLegacy(string codeStr, string[] parts);
	}
}
