using System;
using System.IO;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001A05 RID: 6661
	public static class LosSerializer
	{
		// Token: 0x060101E7 RID: 66023 RVA: 0x0039F409 File Offset: 0x0039D609
		public static string Serialize(object objectToSerialize)
		{
			return LosSerializer.Serialize(objectToSerialize, true);
		}

		// Token: 0x060101E8 RID: 66024 RVA: 0x0039F414 File Offset: 0x0039D614
		public static string Serialize(object objectToSerialize, bool enableMacValidation)
		{
			LosFormatter losFormatter = enableMacValidation ? new LosFormatter(true, "string") : new LosFormatter();
			StringWriter stringWriter = new StringWriter();
			losFormatter.Serialize(stringWriter, objectToSerialize);
			return stringWriter.ToString();
		}

		// Token: 0x060101E9 RID: 66025 RVA: 0x0039F44B File Offset: 0x0039D64B
		public static object Deserialize(string serializedObject)
		{
			return LosSerializer.Deserialize(serializedObject, true);
		}

		// Token: 0x060101EA RID: 66026 RVA: 0x0039F454 File Offset: 0x0039D654
		public static object Deserialize(string serializedObject, bool enableMacValidation)
		{
			LosFormatter losFormatter = enableMacValidation ? new LosFormatter(true, "string") : new LosFormatter();
			return losFormatter.Deserialize(serializedObject);
		}
	}
}
