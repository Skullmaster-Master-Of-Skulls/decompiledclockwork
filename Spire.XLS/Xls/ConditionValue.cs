using System;

namespace Spire.Xls
{
	// Token: 0x0200015B RID: 347
	public class ConditionValue : IConditionValue
	{
		// Token: 0x06000F85 RID: 3973 RVA: 0x0009DA7C File Offset: 0x0009CA7C
		internal ConditionValue(IConditionValue A_0)
		{
			this.m_condtionValue = A_0;
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0009DA98 File Offset: 0x0009CA98
		// (set) Token: 0x06000F87 RID: 3975 RVA: 0x0009DAE0 File Offset: 0x0009CAE0
		public ConditionValueType Type
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.m_condtionValue.Type;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.m_condtionValue.Type = value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x0009DB28 File Offset: 0x0009CB28
		// (set) Token: 0x06000F89 RID: 3977 RVA: 0x0009DB70 File Offset: 0x0009CB70
		public string Value
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.m_condtionValue.Value;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.m_condtionValue.Value = value;
			}
		}

		// Token: 0x04000DA1 RID: 3489
		private byte \u25D8\u0087\u0091ª;

		// Token: 0x04000DA2 RID: 3490
		private bool[] \u2609\u00A1\u0096\u0085;

		// Token: 0x04000DA3 RID: 3491
		private int \u25D8\u009B\u00AB\u00A6;

		// Token: 0x04000DA4 RID: 3492
		private string[] \u25D8\u009C\u007F\u009E;

		// Token: 0x04000DA5 RID: 3493
		public IConditionValue m_condtionValue;
	}
}
