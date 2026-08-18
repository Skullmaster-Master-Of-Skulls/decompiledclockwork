using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000173 RID: 371
	[AttributeUsage(AttributeTargets.Field)]
	public class SoapEnumAttribute : Attribute
	{
		// Token: 0x060018B5 RID: 6325 RVA: 0x0006CA43 File Offset: 0x0006AC43
		public SoapEnumAttribute()
		{
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0006CA4B File Offset: 0x0006AC4B
		public SoapEnumAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x0006CA5A File Offset: 0x0006AC5A
		// (set) Token: 0x060018B8 RID: 6328 RVA: 0x0006CA70 File Offset: 0x0006AC70
		public string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x04000B4F RID: 2895
		private string name;
	}
}
