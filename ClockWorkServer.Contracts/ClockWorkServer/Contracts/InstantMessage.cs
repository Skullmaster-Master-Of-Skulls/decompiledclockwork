using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000AD RID: 173
	[DataContract(Namespace = "http://tpro.ca")]
	public class InstantMessage : ICloneable<InstantMessage>, ICloneable
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x000021FA File Offset: 0x000003FA
		public InstantMessage()
		{
			this.SetDefaults();
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000220C File Offset: 0x0000040C
		public InstantMessage(InstantMessage item)
		{
			this.MessageId = item.MessageId;
			this.Code = item.Code;
			this.Message = item.Message;
			this.RequiredResponse = item.RequiredResponse;
			this.RequiredReceivingConfirmation = item.RequiredReceivingConfirmation;
			this.IssuedOn = item.IssuedOn;
			this.From = item.From;
			this.To = item.To;
			this.Parameters = item.Parameters;
			this.Type = item.Type;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x000022A3 File Offset: 0x000004A3
		// (set) Token: 0x0600051C RID: 1308 RVA: 0x000022AB File Offset: 0x000004AB
		[DataMember]
		public Guid MessageId { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x000022B4 File Offset: 0x000004B4
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x000022BC File Offset: 0x000004BC
		[DataMember]
		public MessageCode Code { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000022C5 File Offset: 0x000004C5
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x000022CD File Offset: 0x000004CD
		[DataMember]
		public string Message { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x000022D6 File Offset: 0x000004D6
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x000022DE File Offset: 0x000004DE
		[DataMember]
		public bool RequiredResponse { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x000022E7 File Offset: 0x000004E7
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x000022EF File Offset: 0x000004EF
		[DataMember]
		public bool RequiredReceivingConfirmation { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x000022F8 File Offset: 0x000004F8
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00002300 File Offset: 0x00000500
		[DataMember]
		public DateTime IssuedOn { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00002309 File Offset: 0x00000509
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x00002311 File Offset: 0x00000511
		[DataMember]
		public IM_User From { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0000231A File Offset: 0x0000051A
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x00002322 File Offset: 0x00000522
		[DataMember]
		public string To { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0000232B File Offset: 0x0000052B
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x00002333 File Offset: 0x00000533
		[DataMember]
		public MessageParameters Parameters { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0000233C File Offset: 0x0000053C
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x00002344 File Offset: 0x00000544
		[DataMember]
		public MessageType Type { get; set; }

		// Token: 0x0600052F RID: 1327 RVA: 0x00002350 File Offset: 0x00000550
		public InstantMessage Clone()
		{
			return new InstantMessage(this);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00002368 File Offset: 0x00000568
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00002380 File Offset: 0x00000580
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.SetDefaults();
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000238A File Offset: 0x0000058A
		private void SetDefaults()
		{
			this.MessageId = Guid.NewGuid();
		}
	}
}
