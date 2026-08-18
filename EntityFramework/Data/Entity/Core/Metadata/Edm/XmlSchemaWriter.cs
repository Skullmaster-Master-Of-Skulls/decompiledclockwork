using System;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000027 RID: 39
	internal abstract class XmlSchemaWriter
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00008895 File Offset: 0x00006A95
		internal void WriteComment(string comment)
		{
			if (!string.IsNullOrEmpty(comment))
			{
				this._xmlWriter.WriteComment(comment);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000088AB File Offset: 0x00006AAB
		internal virtual void WriteEndElement()
		{
			this._xmlWriter.WriteEndElement();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000088B8 File Offset: 0x00006AB8
		protected static string GetQualifiedTypeName(string prefix, string typeName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			return stringBuilder.Append(prefix).Append(".").Append(typeName).ToString();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000088E7 File Offset: 0x00006AE7
		internal static string GetLowerCaseStringFromBoolValue(bool value)
		{
			if (!value)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x040000B2 RID: 178
		protected XmlWriter _xmlWriter;

		// Token: 0x040000B3 RID: 179
		protected double _version;
	}
}
