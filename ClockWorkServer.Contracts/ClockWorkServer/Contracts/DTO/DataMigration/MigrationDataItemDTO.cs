using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataMigration
{
	// Token: 0x02000739 RID: 1849
	[DataContract(Namespace = "http://tpro.ca")]
	public class MigrationDataItemDTO
	{
		// Token: 0x06002630 RID: 9776 RVA: 0x000116FA File Offset: 0x0000F8FA
		public MigrationDataItemDTO()
		{
			this.DataValueType = typeof(string);
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06002631 RID: 9777 RVA: 0x00011715 File Offset: 0x0000F915
		// (set) Token: 0x06002632 RID: 9778 RVA: 0x0001171D File Offset: 0x0000F91D
		[DataMember]
		public string DataName { get; set; }

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x00011726 File Offset: 0x0000F926
		// (set) Token: 0x06002634 RID: 9780 RVA: 0x0001172E File Offset: 0x0000F92E
		[DataMember]
		private Type DataValueType { get; set; }

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x00011737 File Offset: 0x0000F937
		// (set) Token: 0x06002636 RID: 9782 RVA: 0x0001173F File Offset: 0x0000F93F
		[DataMember]
		private string DataValueSerialized { get; set; }

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06002637 RID: 9783 RVA: 0x00011748 File Offset: 0x0000F948
		// (set) Token: 0x06002638 RID: 9784 RVA: 0x0001193C File Offset: 0x0000FB3C
		public object DataValue
		{
			get
			{
				string text = this.DataValueSerialized ?? "";
				bool flag = this.DataValueType == typeof(int);
				object result;
				if (flag)
				{
					int num;
					bool flag2 = text.Length < 1 || !int.TryParse(text, out num);
					if (flag2)
					{
						result = null;
					}
					else
					{
						result = num;
					}
				}
				else
				{
					bool flag3 = this.DataValueType == typeof(DateTime);
					if (flag3)
					{
						DateTime dateTime;
						bool flag4 = text.Length < 1 || !DateTime.TryParse(text, out dateTime);
						if (flag4)
						{
							return null;
						}
					}
					bool flag5 = this.DataValueType == typeof(bool);
					if (flag5)
					{
						bool flag7;
						bool flag6 = text.Length < 1 || !bool.TryParse(text, out flag7);
						if (flag6)
						{
							result = null;
						}
						else
						{
							result = flag7;
						}
					}
					else
					{
						bool flag8 = this.DataValueType == typeof(double);
						if (flag8)
						{
							double num2;
							bool flag9 = text.Length < 1 || !double.TryParse(text, out num2);
							if (flag9)
							{
								result = null;
							}
							else
							{
								result = num2;
							}
						}
						else
						{
							bool flag10 = this.DataValueType == typeof(float);
							if (flag10)
							{
								float num3;
								bool flag11 = text.Length < 1 || !float.TryParse(text, out num3);
								if (flag11)
								{
									result = null;
								}
								else
								{
									result = num3;
								}
							}
							else
							{
								bool flag12 = this.DataValueType == typeof(byte[]);
								if (flag12)
								{
									bool flag13 = text.Length < 1;
									if (flag13)
									{
										result = null;
									}
									else
									{
										try
										{
											return Convert.FromBase64String(text);
										}
										catch
										{
										}
										result = null;
									}
								}
								else
								{
									result = this.DataValueSerialized;
								}
							}
						}
					}
				}
				return result;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this.DataValueType = typeof(string);
					this.DataValueSerialized = null;
				}
				else
				{
					bool flag2 = this.DataValueType == typeof(int);
					if (flag2)
					{
						this.DataValueType = typeof(int);
						this.DataValueSerialized = ((int)value).ToString();
					}
					else
					{
						bool flag3 = this.DataValueType == typeof(DateTime);
						if (flag3)
						{
							this.DataValueType = typeof(DateTime);
							this.DataValueSerialized = ((DateTime)value).ToString("yyyy-MM-dd H:mm");
						}
						else
						{
							bool flag4 = this.DataValueType == typeof(bool);
							if (flag4)
							{
								this.DataValueType = typeof(bool);
								this.DataValueSerialized = ((bool)value).ToString();
							}
							else
							{
								bool flag5 = this.DataValueType == typeof(double);
								if (flag5)
								{
									this.DataValueType = typeof(double);
									this.DataValueSerialized = ((double)value).ToString();
								}
								else
								{
									bool flag6 = this.DataValueType == typeof(float);
									if (flag6)
									{
										this.DataValueType = typeof(float);
										this.DataValueSerialized = ((float)value).ToString();
									}
									else
									{
										bool flag7 = this.DataValueType == typeof(byte[]);
										if (flag7)
										{
											this.DataValueType = typeof(byte[]);
											this.DataValueSerialized = Convert.ToBase64String((byte[])value);
										}
										else
										{
											this.DataValueType = typeof(string);
											this.DataValueSerialized = value.ToString();
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}
}
