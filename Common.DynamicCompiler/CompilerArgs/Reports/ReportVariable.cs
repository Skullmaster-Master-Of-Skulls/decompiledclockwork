using System;

namespace TechnoPro.Common.DynamicCompiler.CompilerArgs.Reports
{
	// Token: 0x02000012 RID: 18
	public class ReportVariable
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003C76 File Offset: 0x00001E76
		public ReportVariable()
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003C80 File Offset: 0x00001E80
		public ReportVariable(string name, object value)
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003C9A File Offset: 0x00001E9A
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00003CA2 File Offset: 0x00001EA2
		public string Name { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003CAB File Offset: 0x00001EAB
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003CB3 File Offset: 0x00001EB3
		public object Value { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003CBC File Offset: 0x00001EBC
		public string ValueString
		{
			get
			{
				return (this.Value == null) ? "" : this.Value.ToString();
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003CE8 File Offset: 0x00001EE8
		public bool ValueBool
		{
			get
			{
				bool flag = this.Value == null;
				bool result;
				if (flag)
				{
					result = false;
				}
				else
				{
					bool flag2 = this.Value is bool;
					if (flag2)
					{
						result = (bool)this.Value;
					}
					else
					{
						bool flag3 = this.Value is int;
						if (flag3)
						{
							result = ((int)this.Value == 1);
						}
						else
						{
							bool flag4;
							result = (bool.TryParse(this.Value.ToString(), out flag4) && flag4);
						}
					}
				}
				return result;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003D64 File Offset: 0x00001F64
		public int ValueInt
		{
			get
			{
				bool flag = this.Value == null;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					bool flag2 = this.Value is int;
					if (flag2)
					{
						result = (int)this.Value;
					}
					else
					{
						int num;
						result = (int.TryParse(this.Value.ToString(), out num) ? num : 0);
					}
				}
				return result;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003DC0 File Offset: 0x00001FC0
		public DateTime? ValueDateTime
		{
			get
			{
				bool flag = this.Value == null;
				DateTime? result;
				if (flag)
				{
					result = null;
				}
				else
				{
					bool flag2 = this.Value is DateTime;
					if (flag2)
					{
						result = new DateTime?((DateTime)this.Value);
					}
					else
					{
						DateTime value;
						bool flag3 = DateTime.TryParse(this.Value.ToString(), out value);
						if (flag3)
						{
							result = new DateTime?(value);
						}
						else
						{
							result = null;
						}
					}
				}
				return result;
			}
		}
	}
}
