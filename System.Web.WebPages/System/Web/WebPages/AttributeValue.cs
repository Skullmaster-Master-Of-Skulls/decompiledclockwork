using System;
using System.Web.WebPages.Instrumentation;

namespace System.Web.WebPages
{
	// Token: 0x02000013 RID: 19
	public class AttributeValue
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x000036D1 File Offset: 0x000018D1
		public AttributeValue(PositionTagged<string> prefix, PositionTagged<object> value, bool literal)
		{
			this.Prefix = prefix;
			this.Value = value;
			this.Literal = literal;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000036EE File Offset: 0x000018EE
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000036F6 File Offset: 0x000018F6
		public PositionTagged<string> Prefix { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000036FF File Offset: 0x000018FF
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003707 File Offset: 0x00001907
		public PositionTagged<object> Value { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003710 File Offset: 0x00001910
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00003718 File Offset: 0x00001918
		public bool Literal { get; private set; }

		// Token: 0x060000AD RID: 173 RVA: 0x00003721 File Offset: 0x00001921
		public static AttributeValue FromTuple(Tuple<Tuple<string, int>, Tuple<object, int>, bool> value)
		{
			return new AttributeValue(value.Item1, value.Item2, value.Item3);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003744 File Offset: 0x00001944
		public static AttributeValue FromTuple(Tuple<Tuple<string, int>, Tuple<string, int>, bool> value)
		{
			return new AttributeValue(value.Item1, new PositionTagged<object>(value.Item2.Item1, value.Item2.Item2), value.Item3);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003777 File Offset: 0x00001977
		public static implicit operator AttributeValue(Tuple<Tuple<string, int>, Tuple<object, int>, bool> value)
		{
			return AttributeValue.FromTuple(value);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000377F File Offset: 0x0000197F
		public static implicit operator AttributeValue(Tuple<Tuple<string, int>, Tuple<string, int>, bool> value)
		{
			return AttributeValue.FromTuple(value);
		}
	}
}
