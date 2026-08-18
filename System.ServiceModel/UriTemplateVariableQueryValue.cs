using System;
using System.Collections.Specialized;
using System.Runtime;
using System.Text;

namespace System
{
	// Token: 0x02000015 RID: 21
	internal class UriTemplateVariableQueryValue : UriTemplateQueryValue
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x0000580F File Offset: 0x00003A0F
		public UriTemplateVariableQueryValue(string varName) : base(UriTemplatePartType.Variable)
		{
			this.varName = varName;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005820 File Offset: 0x00003A20
		public override void Bind(string keyName, string[] values, ref int valueIndex, StringBuilder query)
		{
			if (values[valueIndex] == null)
			{
				valueIndex++;
				return;
			}
			string format = "&{0}={1}";
			object arg = UrlUtility.UrlEncode(keyName, Encoding.UTF8);
			int num = valueIndex;
			valueIndex = num + 1;
			query.AppendFormat(format, arg, UrlUtility.UrlEncode(values[num], Encoding.UTF8));
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005867 File Offset: 0x00003A67
		public override bool IsEquivalentTo(UriTemplateQueryValue other)
		{
			return other != null && other.Nature == UriTemplatePartType.Variable;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005877 File Offset: 0x00003A77
		public override void Lookup(string value, NameValueCollection boundParameters)
		{
			boundParameters.Add(this.varName, value);
		}

		// Token: 0x04000088 RID: 136
		private readonly string varName;
	}
}
