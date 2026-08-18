using System;
using System.Linq;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;

namespace TechnoPro.Common.Public.Entities.ConnectionString
{
	// Token: 0x0200043E RID: 1086
	[Serializable]
	public class ClockWorkConnectionString : BusinessBase<string>
	{
		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x00024F4C File Offset: 0x0002314C
		// (set) Token: 0x060020F4 RID: 8436 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string Name
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x00024F64 File Offset: 0x00023164
		// (set) Token: 0x060020F6 RID: 8438 RVA: 0x00024F6C File Offset: 0x0002316C
		public string Server { get; set; }

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x00024F75 File Offset: 0x00023175
		// (set) Token: 0x060020F8 RID: 8440 RVA: 0x00024F7D File Offset: 0x0002317D
		public string InstanceName { get; set; }

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x00024F86 File Offset: 0x00023186
		// (set) Token: 0x060020FA RID: 8442 RVA: 0x00024F8E File Offset: 0x0002318E
		public int Port { get; set; }

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x00024F97 File Offset: 0x00023197
		// (set) Token: 0x060020FC RID: 8444 RVA: 0x00024F9F File Offset: 0x0002319F
		public eBindingType BindingType { get; set; }

		// Token: 0x060020FD RID: 8445 RVA: 0x00024FA8 File Offset: 0x000231A8
		public ClockWorkConnectionString()
		{
			this.Name = string.Empty;
			this.Server = string.Empty;
			this.InstanceName = string.Empty;
			this.Port = 0;
			this.BindingType = eBindingType.Unspecified;
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x00024FE8 File Offset: 0x000231E8
		public ClockWorkConnectionString(string ccs)
		{
			try
			{
				string[] source = ccs.Split(new char[]
				{
					';'
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string[] array in from field in source
				select field.Split(new char[]
				{
					'='
				}))
				{
					bool flag = array.Length == 2;
					if (!flag)
					{
						throw new FormatException("ClockWork Connection String is not in a correct format");
					}
					string text = array[0].Trim();
					string text2 = array[1].Trim();
					string text3 = text.ToLower();
					string a = text3;
					if (!(a == "name"))
					{
						if (!(a == "server"))
						{
							if (!(a == "instancename"))
							{
								if (!(a == "port"))
								{
									if (a == "bindingtype")
									{
										bool flag2 = Enum.IsDefined(typeof(eBindingType), text2);
										if (flag2)
										{
											this.BindingType = (eBindingType)Enum.Parse(typeof(eBindingType), text2);
										}
									}
								}
								else
								{
									int port;
									bool flag3 = int.TryParse(text2, out port);
									if (flag3)
									{
										this.Port = port;
									}
								}
							}
							else
							{
								this.InstanceName = text2;
							}
						}
						else
						{
							this.Server = text2;
						}
					}
					else
					{
						this.Name = text2;
					}
				}
				this.AssureConnection();
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("ClockWork Connection String is invalid", "ccs", innerException);
			}
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000251AC File Offset: 0x000233AC
		public override string ToString()
		{
			return string.Format("Name={0};Server={1};InstanceName={2};Port={3};BindingType={4};", new object[]
			{
				this.Name ?? string.Empty,
				this.Server ?? string.Empty,
				this.InstanceName ?? string.Empty,
				this.Port,
				this.BindingType
			});
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x00025220 File Offset: 0x00023420
		private void AssureConnection()
		{
			bool flag = string.IsNullOrEmpty(this.Name);
			if (flag)
			{
				throw new ArgumentException("ClockWork Connection String does not contain Name field");
			}
			bool flag2 = string.IsNullOrEmpty(this.Server);
			if (flag2)
			{
				throw new ArgumentException("ClockWork Connection String does not contain Server field");
			}
			bool flag3 = string.IsNullOrEmpty(this.InstanceName);
			if (flag3)
			{
				throw new ArgumentException("ClockWork Connection String does not contain InstanceName field");
			}
			bool flag4 = this.Port <= 0;
			if (flag4)
			{
				throw new ArgumentException("ClockWork Connection String does not contain Port field");
			}
			bool flag5 = this.BindingType == eBindingType.Unspecified;
			if (flag5)
			{
				throw new ArgumentException("ClockWork Connection String does not contain BindingType field");
			}
		}
	}
}
