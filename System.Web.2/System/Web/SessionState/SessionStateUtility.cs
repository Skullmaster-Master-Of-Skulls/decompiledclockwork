using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.SessionState
{
	// Token: 0x02000135 RID: 309
	public static class SessionStateUtility
	{
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00034661 File Offset: 0x00032861
		// (set) Token: 0x06001285 RID: 4741 RVA: 0x00034668 File Offset: 0x00032868
		public static ISurrogateSelector SerializationSurrogateSelector { [SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)] get; [SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)] set; }

		// Token: 0x06001286 RID: 4742 RVA: 0x00034670 File Offset: 0x00032870
		public static void RaiseSessionEnd(IHttpSessionState session, object eventSource, EventArgs eventArgs)
		{
			HttpApplicationFactory.EndSession(new HttpSessionState(session), eventSource, eventArgs);
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00034680 File Offset: 0x00032880
		public static void AddHttpSessionStateToContext(HttpContext context, IHttpSessionState container)
		{
			HttpSessionState value = new HttpSessionState(container);
			try
			{
				context.Items.Add("AspSession", value);
			}
			catch (ArgumentException)
			{
				throw new HttpException(SR.GetString("Cant_have_multiple_session_module"));
			}
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000346C8 File Offset: 0x000328C8
		internal static void AddHttpSessionStateModuleToContext(HttpContext context, SessionStateModule module, bool delayed)
		{
			context.AddHttpSessionStateModule(module, delayed);
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x000346D2 File Offset: 0x000328D2
		internal static void RemoveHttpSessionStateFromContext(HttpContext context, bool delayed)
		{
			if (!delayed)
			{
				context.Items.Remove("AspSession");
			}
			context.RemoveHttpSessionStateModule();
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x000346ED File Offset: 0x000328ED
		public static void RemoveHttpSessionStateFromContext(HttpContext context)
		{
			SessionStateUtility.RemoveHttpSessionStateFromContext(context, false);
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x000346F6 File Offset: 0x000328F6
		public static IHttpSessionState GetHttpSessionStateFromContext(HttpContext context)
		{
			return context.Session.Container;
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x00034703 File Offset: 0x00032903
		public static HttpStaticObjectsCollection GetSessionStaticObjects(HttpContext context)
		{
			return context.Application.SessionStaticObjects.Clone();
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00034715 File Offset: 0x00032915
		public static bool IsSessionStateRequired(HttpContext context)
		{
			return context.RequiresSessionState;
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0003471D File Offset: 0x0003291D
		public static bool IsSessionStateReadOnly(HttpContext context)
		{
			return context.ReadOnlySessionState;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00034725 File Offset: 0x00032925
		internal static SessionStateStoreData CreateLegitStoreData(HttpContext context, ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout)
		{
			if (sessionItems == null)
			{
				sessionItems = new SessionStateItemCollection();
			}
			if (staticObjects == null && context != null)
			{
				staticObjects = SessionStateUtility.GetSessionStaticObjects(context);
			}
			return new SessionStateStoreData(sessionItems, staticObjects, timeout);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00034748 File Offset: 0x00032948
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		internal static void Serialize(SessionStateStoreData item, Stream stream)
		{
			bool flag = true;
			bool flag2 = true;
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(item.Timeout);
			if (item.Items == null || item.Items.Count == 0)
			{
				flag = false;
			}
			binaryWriter.Write(flag);
			if (item.StaticObjects == null || item.StaticObjects.NeverAccessed)
			{
				flag2 = false;
			}
			binaryWriter.Write(flag2);
			if (flag)
			{
				((SessionStateItemCollection)item.Items).Serialize(binaryWriter);
			}
			if (flag2)
			{
				item.StaticObjects.Serialize(binaryWriter);
			}
			binaryWriter.Write(byte.MaxValue);
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x000347D8 File Offset: 0x000329D8
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		internal static SessionStateStoreData Deserialize(HttpContext context, Stream stream)
		{
			int timeout;
			SessionStateItemCollection sessionItems;
			HttpStaticObjectsCollection staticObjects;
			try
			{
				BinaryReader binaryReader = new BinaryReader(stream);
				timeout = binaryReader.ReadInt32();
				bool flag = binaryReader.ReadBoolean();
				bool flag2 = binaryReader.ReadBoolean();
				if (flag)
				{
					sessionItems = SessionStateItemCollection.Deserialize(binaryReader);
				}
				else
				{
					sessionItems = new SessionStateItemCollection();
				}
				if (flag2)
				{
					staticObjects = HttpStaticObjectsCollection.Deserialize(binaryReader);
				}
				else
				{
					staticObjects = SessionStateUtility.GetSessionStaticObjects(context);
				}
				byte b = binaryReader.ReadByte();
				if (b != 255)
				{
					throw new HttpException(SR.GetString("Invalid_session_state"));
				}
			}
			catch (EndOfStreamException)
			{
				throw new HttpException(SR.GetString("Invalid_session_state"));
			}
			return new SessionStateStoreData(sessionItems, staticObjects, timeout);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0003487C File Offset: 0x00032A7C
		internal static void SerializeStoreData(SessionStateStoreData item, int initialStreamSize, out byte[] buf, out int length, bool compressionEnabled)
		{
			using (MemoryStream memoryStream = new MemoryStream(initialStreamSize))
			{
				SessionStateUtility.Serialize(item, memoryStream);
				if (compressionEnabled)
				{
					byte[] buffer = memoryStream.GetBuffer();
					int count = (int)memoryStream.Length;
					memoryStream.SetLength(0L);
					using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress, true))
					{
						deflateStream.Write(buffer, 0, count);
					}
					memoryStream.WriteByte(byte.MaxValue);
				}
				buf = memoryStream.GetBuffer();
				length = (int)memoryStream.Length;
			}
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00034914 File Offset: 0x00032B14
		internal static SessionStateStoreData DeserializeStoreData(HttpContext context, Stream stream, bool compressionEnabled)
		{
			if (compressionEnabled)
			{
				using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress, true))
				{
					return SessionStateUtility.Deserialize(context, deflateStream);
				}
			}
			return SessionStateUtility.Deserialize(context, stream);
		}

		// Token: 0x04001472 RID: 5234
		internal const string SESSION_KEY = "AspSession";
	}
}
