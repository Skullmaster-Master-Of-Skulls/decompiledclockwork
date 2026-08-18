using System;
using System.Collections.Generic;
using System.Drawing;

namespace ClockWorkWebAPIWeb.AppBooking
{
	// Token: 0x0200001C RID: 28
	public class Channel
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00010800 File Offset: 0x0000EA00
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00010818 File Offset: 0x0000EA18
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00010824 File Offset: 0x0000EA24
		// (set) Token: 0x06000151 RID: 337 RVA: 0x0001083C File Offset: 0x0000EA3C
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00010848 File Offset: 0x0000EA48
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00010877 File Offset: 0x0000EA77
		public int Colour
		{
			get
			{
				return (this.colour == 0) ? Color.LightBlue.ToArgb() : this.colour;
			}
			set
			{
				this.colour = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00010884 File Offset: 0x0000EA84
		// (set) Token: 0x06000155 RID: 341 RVA: 0x0001089C File Offset: 0x0000EA9C
		public int AppTypeId
		{
			get
			{
				return this.appTypeId;
			}
			set
			{
				this.appTypeId = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000108A8 File Offset: 0x0000EAA8
		// (set) Token: 0x06000157 RID: 343 RVA: 0x000108C0 File Offset: 0x0000EAC0
		public int ScreenNum
		{
			get
			{
				return this.screenNum;
			}
			set
			{
				this.screenNum = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000108CC File Offset: 0x0000EACC
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000108E4 File Offset: 0x0000EAE4
		public int OrderNum
		{
			get
			{
				return this.orderNum;
			}
			set
			{
				this.orderNum = value;
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000108F0 File Offset: 0x0000EAF0
		public Channel(string id, string title, string colour, int appTypeId, int screenNum, int orderNum)
		{
			this.id = id;
			this.title = title;
			this.colour = Color.FromName(colour).ToArgb();
			this.appTypeId = appTypeId;
			this.screenNum = screenNum;
			this.orderNum = orderNum;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00010940 File Offset: 0x0000EB40
		public static Channel FindChannel(List<Channel> channels, string id)
		{
			foreach (Channel channel in channels)
			{
				bool flag = channel.Id.Equals(id);
				if (flag)
				{
					return channel;
				}
			}
			return null;
		}

		// Token: 0x04000085 RID: 133
		private string id;

		// Token: 0x04000086 RID: 134
		private string title;

		// Token: 0x04000087 RID: 135
		private int colour;

		// Token: 0x04000088 RID: 136
		private int appTypeId;

		// Token: 0x04000089 RID: 137
		private int screenNum;

		// Token: 0x0400008A RID: 138
		private int orderNum;
	}
}
