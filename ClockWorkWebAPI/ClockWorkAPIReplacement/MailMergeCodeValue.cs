using System;
using System.Collections.Generic;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x02000062 RID: 98
	public class MailMergeCodeValue
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00021E3C File Offset: 0x0002003C
		public string CodeName
		{
			get
			{
				return this.code.Name;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00021E5C File Offset: 0x0002005C
		public object CodeValueString
		{
			get
			{
				return this.value.ValueToString;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00021E7C File Offset: 0x0002007C
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x00021E94 File Offset: 0x00020094
		public MailMergeCode Code
		{
			get
			{
				return this.code;
			}
			set
			{
				this.code = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00021EA0 File Offset: 0x000200A0
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x00021EB8 File Offset: 0x000200B8
		public MailMergeValue Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00021EC2 File Offset: 0x000200C2
		public MailMergeCodeValue()
		{
			this.code = null;
			this.value = null;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00021EDA File Offset: 0x000200DA
		public MailMergeCodeValue(MailMergeCode code)
		{
			this.code = code;
			this.value = new MailMergeValue();
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00021EF6 File Offset: 0x000200F6
		public MailMergeCodeValue(MailMergeCode code, MailMergeValue value)
		{
			this.code = code;
			this.value = value;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00021F0E File Offset: 0x0002010E
		public MailMergeCodeValue(MailMergeCode code, object value)
		{
			this.code = code;
			this.value = new MailMergeValue(value);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00021F2C File Offset: 0x0002012C
		public MailMergeCodeValue Clone()
		{
			return new MailMergeCodeValue(this.code);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00021F4C File Offset: 0x0002014C
		public MailMergeCodeValue Copy()
		{
			MailMergeValue mailMergeValue = new MailMergeValue();
			foreach (object item in this.value.Values)
			{
				mailMergeValue.Values.Add(item);
			}
			return new MailMergeCodeValue(this.code, mailMergeValue);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00021FC8 File Offset: 0x000201C8
		public static List<MailMergeCodeValue> Clone(List<MailMergeCodeValue> codes)
		{
			List<MailMergeCodeValue> list = new List<MailMergeCodeValue>(codes.Count);
			foreach (MailMergeCodeValue mailMergeCodeValue in codes)
			{
				list.Add(mailMergeCodeValue.Clone());
			}
			return list;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00022034 File Offset: 0x00020234
		public static List<MailMergeCodeValue> Copy(List<MailMergeCodeValue> codes)
		{
			List<MailMergeCodeValue> list = new List<MailMergeCodeValue>(codes.Count);
			foreach (MailMergeCodeValue mailMergeCodeValue in codes)
			{
				list.Add(mailMergeCodeValue.Copy());
			}
			return list;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000220A0 File Offset: 0x000202A0
		public static List<MailMergeCodeValue> NewListFromJustCodesList(List<MailMergeCode> mailMergeCodes)
		{
			List<MailMergeCodeValue> list = new List<MailMergeCodeValue>(mailMergeCodes.Count);
			foreach (MailMergeCode mailMergeCode in mailMergeCodes)
			{
				list.Add(new MailMergeCodeValue(mailMergeCode));
			}
			return list;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00022108 File Offset: 0x00020308
		public static void SetValue(ref List<MailMergeCodeValue> codes, string codeName, object value)
		{
			MailMergeCodeValue mailMergeCodeValue = codes.Find((MailMergeCodeValue e) => e.Code.Name.Equals(codeName));
			bool flag = mailMergeCodeValue != null;
			if (flag)
			{
				mailMergeCodeValue.Value.SetValue(value);
			}
		}

		// Token: 0x04000290 RID: 656
		private MailMergeCode code;

		// Token: 0x04000291 RID: 657
		private MailMergeValue value;
	}
}
