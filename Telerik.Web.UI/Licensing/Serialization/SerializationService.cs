using System;
using System.Web.Script.Serialization;

namespace Telerik.Licensing.Serialization
{
	// Token: 0x02000426 RID: 1062
	internal class SerializationService : ISerializationService
	{
		// Token: 0x0600261E RID: 9758 RVA: 0x0007D2A5 File Offset: 0x0007B4A5
		private SerializationService()
		{
			this._serializer = new JavaScriptSerializer();
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x0600261F RID: 9759 RVA: 0x0007D2B8 File Offset: 0x0007B4B8
		protected JavaScriptSerializer Serializer
		{
			get
			{
				return this._serializer;
			}
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x0007D2C0 File Offset: 0x0007B4C0
		public static ISerializationService GetInstance()
		{
			if (SerializationService.service == null)
			{
				lock (SerializationService.serviceLock)
				{
					if (SerializationService.service == null)
					{
						SerializationService.service = new SerializationService();
					}
				}
			}
			return SerializationService.service;
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x0007D318 File Offset: 0x0007B518
		public string Serialize<T>(T obj)
		{
			return this.Serializer.Serialize(obj);
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x0007D32B File Offset: 0x0007B52B
		public T Deserialize<T>(string serializedObj)
		{
			return this.Serializer.Deserialize<T>(serializedObj);
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x0007D339 File Offset: 0x0007B539
		public string SerializeToJson<T>(T obj)
		{
			return this.Serialize<T>(obj);
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x0007D342 File Offset: 0x0007B542
		public T DeserializeFromJson<T>(string serializedObj)
		{
			return this.Deserialize<T>(serializedObj);
		}

		// Token: 0x040009B6 RID: 2486
		private static readonly object serviceLock = new object();

		// Token: 0x040009B7 RID: 2487
		private static ISerializationService service;

		// Token: 0x040009B8 RID: 2488
		private readonly JavaScriptSerializer _serializer;
	}
}
